// =====================================================================
// Décodeur MS-ADPCM — le son des cinématiques.
//
// Notre échafaudage, pas une transcription : l'original ne décode rien lui-même, il laisse
// DirectShow s'en charger avec le reste du film. Le format est documenté et tient en une boucle ;
// contrairement à l'Indeo 4, il n'y avait aucune raison de dépendre d'une bibliothèque pour ça.
//
// Un flux MS-ADPCM est une suite de **blocs indépendants** de `nBlockAlign` octets. Chaque bloc
// porte son propre amorçage — l'indice de prédicteur, le pas de quantification et les deux premiers
// échantillons — puis un demi-octet par échantillon suivant. Les blocs étant indépendants, une
// erreur ne se propage jamais au-delà de son bloc, et on peut entrer dans le flux n'importe où.
// =====================================================================

namespace OpenForesia.Blaze;

internal static class MsAdpcm
{
    /// <summary>
    /// La table d'adaptation du pas de quantification, indexée par le demi-octet lu. C'est la
    /// seule constante du format qui ne soit pas dans le fichier ; les coefficients du prédicteur,
    /// eux, sont transportés par l'en-tête et lus par <see cref="AviReader"/>.
    /// </summary>
    private static readonly int[] Adaptation =
    [
        230, 230, 230, 230, 307, 409, 512, 614,
        768, 614, 512, 409, 307, 230, 230, 230,
    ];

    /// <summary>
    /// Décode un bloc en PCM 16 bits signé. Rend le nombre d'échantillons écrits (par canal ×
    /// canaux), ou <c>0</c> si le bloc est trop court pour porter son propre amorçage.
    /// </summary>
    internal static int DecodeBlock(
        ReadOnlySpan<byte> block, int channels, short[,] coefficients, short[] destination, int offset)
    {
        if (channels is not (1 or 2)) return 0;
        int headerBytes = 7 * channels;
        if (block.Length < headerBytes || coefficients.GetLength(0) == 0) return 0;

        Span<int> coef1 = stackalloc int[2];
        Span<int> coef2 = stackalloc int[2];
        Span<int> delta = stackalloc int[2];
        Span<int> sample1 = stackalloc int[2];
        Span<int> sample2 = stackalloc int[2];

        int p = 0;
        for (int c = 0; c < channels; c++)
        {
            // Un indice hors table rendrait le bloc absurde ; on le borne plutôt que de sortir du
            // tableau. Un fichier sain n'y passe jamais.
            int predictor = Math.Min(block[p++], coefficients.GetLength(0) - 1);
            coef1[c] = coefficients[predictor, 0];
            coef2[c] = coefficients[predictor, 1];
        }
        for (int c = 0; c < channels; c++) { delta[c] = BitConverter.ToInt16(block[p..]); p += 2; }
        for (int c = 0; c < channels; c++) { sample1[c] = BitConverter.ToInt16(block[p..]); p += 2; }
        for (int c = 0; c < channels; c++) { sample2[c] = BitConverter.ToInt16(block[p..]); p += 2; }

        int written = 0;

        // Les deux échantillons d'amorçage font partie du son : ils sortent en premier, et dans
        // l'ordre inverse de leur rangement — le plus ancien est `sample2`.
        for (int c = 0; c < channels; c++)
        {
            if (offset + written >= destination.Length) return written;
            destination[offset + written++] = (short)sample2[c];
        }
        for (int c = 0; c < channels; c++)
        {
            if (offset + written >= destination.Length) return written;
            destination[offset + written++] = (short)sample1[c];
        }

        int channel = 0;
        for (; p < block.Length; p++)
        {
            for (int half = 0; half < 2; half++)
            {
                int nibble = half == 0 ? block[p] >> 4 : block[p] & 0x0f;
                if (offset + written >= destination.Length) return written;

                int c = channels == 1 ? 0 : channel;

                // Le prédicteur est une combinaison linéaire des deux échantillons précédents, en
                // virgule fixe sur huit bits, à laquelle s'ajoute l'erreur transmise.
                //
                // **`>> 8` et non `/ 256`.** Les deux ne diffèrent que d'une unité, et seulement
                // sur les valeurs négatives — le décalage arrondit vers moins l'infini, la division
                // vers zéro. Mais le résultat repart dans le prédicteur de l'échantillon suivant :
                // l'écart s'accumule au lieu de s'annuler. Mesuré contre le décodeur de Windows,
                // `/ 256` faisait diverger **64 % des échantillons**, d'un écart allant jusqu'à
                // 309 — assez pour que le son sonne faux sans qu'aucune forme d'onde paraisse
                // anormale. Voir OpenForesia.Tools\check-movie-audio.ps1.
                int predicted = (sample1[c] * coef1[c] + sample2[c] * coef2[c]) >> 8;
                int signed = nibble >= 8 ? nibble - 16 : nibble;   // le demi-octet est signé
                predicted += signed * delta[c];
                predicted = Math.Clamp(predicted, short.MinValue, short.MaxValue);

                destination[offset + written++] = (short)predicted;
                sample2[c] = sample1[c];
                sample1[c] = predicted;

                // Le pas suit l'amplitude de l'erreur, et ne descend jamais sous 16 : sans ce
                // plancher il s'effondrerait à zéro sur un passage silencieux et ne pourrait plus
                // remonter, le produit restant nul.
                delta[c] = Adaptation[nibble] * delta[c] / 256;
                if (delta[c] < 16) delta[c] = 16;

                if (channels == 2) channel ^= 1;
            }
        }

        return written;
    }
}
