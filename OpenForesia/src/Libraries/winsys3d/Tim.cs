// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur pour les deux corps (arites, valeur d'EAX, arithmetique de pointeurs).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE LECTEUR DE CONTENEUR TIM — ordinaux 677 et 662, la composante 3
//
// `OpenTIM` et `ReadTIM` de la libgpu PlayStation. Ce sont deux corps de 24 et 37 lignes, et ils
// ne font **aucune lecture de fichier** : un TIM est deja en memoire quand ils le voient. Tout
// leur travail est de l'arithmetique de pointeurs sur un en-tete.
//
//     ordinal 677   FUN_00a05bb0   `OpenTIM(void*)`   retient l'adresse, rend toujours 0
//     ordinal 662   FUN_00a05bd0   `ReadTIM(TIM_IMAGE*)`   remplit les cinq champs, puis oublie
//
// ============================================================================================
// POURQUOI LA PAIRE SE PORTE, ET POURQUOI ELLE SE PORTE ENSEMBLE
//
// Une seule globale les relie, `DAT_011be790` — l'adresse retenue entre les deux appels. Elle a
// **dix references dans les 27 000 lignes de `winsys3d.c`, et elles tiennent dans ces deux
// corps** : une dans 677 (l'ecriture), neuf dans 662 (les lectures et la remise a zero). Rien
// d'autre dans la DLL ne la nomme, aucune fonction restee native ne peut donc la voir changer.
//
// C'est exactement la situation que `ListeAffichage.cs` decrit pour 659/660 : un casier que la
// DLL prete au jeu, pas de l'etat qu'elle consulte. **Mais les deux corps partent ensemble** —
// porter 677 seul ecrirait notre copie pendant que 662, reste natif, relirait la sienne, et
// `ReadTIM` rendrait alors l'adresse du TIM precedent. Le genre de defaut qui donne une texture
// plausible mais fausse, sans plantage ni message.
//
// **L'etat est a UN SEUL appel de profondeur.** `ReadTIM` remet `DAT_011be790` a zero en
// sortant : deux `ReadTIM` de suite sans `OpenTIM` entre les deux, et le second ne fait rien du
// tout — il sort par son premier test, sans rien ecrire dans la structure de l'appelant, qui
// garde donc les valeurs du TIM precedent. Les deux sites de la reconstruction appellent bien la
// paire dans l'ordre (`Resources.DecodeResourceChunk`, `Screens/DebugParty.cs`).
//
// ============================================================================================
// LA CARTE D'UN TIM, TELLE QUE CES DEUX CORPS LA LISENT
//
// `OpenTIM` retient `donnees + 4` : il saute le mot d'identification (0x00000010) sans le lire.
// Tout ce qui suit est compte depuis la, en mots de 32 bits, et c'est le pointeur que la
// transcription appelle `p`.
//
//     p[0]        le drapeau : le mode de pixel sur trois bits, et le bit 3 pose s'il y a une CLUT
//     p[1]        la longueur EN OCTETS du bloc qui suit, son propre mot compris
//     p+2         le rectangle du bloc — x, y, w, h, quatre shorts
//     p+4         les donnees du bloc
//
// Quand le bit 3 est pose, le premier bloc est la CLUT et le bloc d'image commence a
// `p + p[1]/4 + 1` ; sinon il n'y a qu'un bloc, a `p + 1`. Le corps calcule les deux cas avec la
// meme expression en posant `saut = 1` dans le cas sans CLUT : c'est la seule astuce du corps, et
// elle est reproduite telle quelle plutot que depliee en deux branches.
//
// Le mode force a 2 dans le cas sans CLUT n'est pas une valeur par defaut prudente : c'est
// `TIM_16BIT` de la libgpu. Un TIM sans palette est un TIM 16 bits directs, la question ne se
// pose pas.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dTim
{
    /// <summary>
    /// DAT_011be790 — l'adresse du TIM ouvert, ou zero. Le seul etat de la paire, et il ne
    /// survit pas a un <see cref="ReadTim"/>. Voir le pave de tete.
    /// </summary>
    private static uint* s_TimOuvert_011be790;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05bb0 @ 0x00a05bb0 (ordinal 677) — `OpenTIM` : retient
    /// <c>donnees + 4</c>, c'est-a-dire le TIM prive de son mot d'identification.
    ///
    /// **Rend toujours zero** (`xor eax,eax ; ret 4` dans le binaire), sans avoir rien verifie :
    /// ni le mot d'identification, ni la nullite de l'argument. Un appelant qui teste ce retour
    /// teste une constante.
    /// </summary>
    internal static int OpenTim(void* donnees)
    {
        s_TimOuvert_011be790 = (uint*)((byte*)donnees + 4);
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05bd0 @ 0x00a05bd0 (ordinal 662) — `ReadTIM` : remplit les cinq
    /// mots de la structure <c>TIM_IMAGE</c> de la libgpu, puis **oublie le TIM**.
    ///
    /// Les cinq mots, dans l'ordre : mode, rectangle de CLUT, donnees de CLUT, rectangle de
    /// pixels, donnees de pixels. Sans CLUT, les deux mots de CLUT sortent a zero et le mode a 2
    /// (<c>TIM_16BIT</c>).
    ///
    /// **Sans TIM ouvert, ce corps ne fait rien** — il sort avant d'avoir touche un seul des cinq
    /// mots, qui gardent donc ce qu'ils portaient. C'est le piege de la paire : la structure de
    /// l'appelant n'est pas remise a zero, elle est laissee telle quelle.
    /// </summary>
    internal static void ReadTim(uint* image)
    {
        uint* p = s_TimOuvert_011be790;
        if (p == null) return;

        // `saut` est le nombre de mots du bloc de CLUT, son mot de longueur compris. A 1 quand il
        // n'y a pas de CLUT, ce qui fait retomber les deux dernieres lignes sur le premier bloc.
        int saut;

        if ((*p & 8) == 0)
        {
            saut = 1;
            image[0] = 2;   // TIM_16BIT
            image[1] = 0;   // pas de rectangle de CLUT
            image[2] = 0;   // pas de donnees de CLUT
        }
        else
        {
            image[0] = *p & 7;
            uint longueurBloc = p[1];
            image[1] = (uint)(p + 2);
            saut = (int)(longueurBloc >> 2) + 1;
            image[2] = (uint)(p + 4);
        }

        image[3] = (uint)(p + saut + 1);
        image[4] = (uint)(p + saut + 3);

        s_TimOuvert_011be790 = null;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a1d8b0 @ 0x00a1d8b0 (ordinal 600) — `GetTimInfo(tim, info)` : decrit
    /// une TIM (<paramref name="tim"/> pointe sur ses drapeaux, apres l'identifiant) dans un
    /// `TIM_IMAGE` de 0x1c octets — mode `+0`, rectangle d'image `+4`, pixels `+0xc`, rectangle de
    /// CLUT `+0x10`, CLUT `+0x18`. Sans lecture de flux ni etat : c'est `ReadTim` sur un pointeur
    /// donne. `ret 8`, verifie au desassembleur.
    ///
    /// **Sans CLUT (bit 3 nul), le MODE est mis a zero** avec le rectangle et l'adresse de CLUT :
    /// l'appelant ne voit plus la profondeur de l'image. Transcrit tel quel.
    /// </summary>
    internal static void GetTimInfo(uint* tim, byte* info)
    {
        uint drapeaux = tim[0];
        int saut;
        uint* clut;
        if ((drapeaux & 8) == 0)
        {
            clut = null;
            saut = 1;
            *(uint*)info = 0;
            *(ushort*)(info + 0x10) = 0;
            *(ushort*)(info + 0x12) = 0;
            *(ushort*)(info + 0x14) = 0;
            *(ushort*)(info + 0x16) = 0;
        }
        else
        {
            *(uint*)info = drapeaux;
            *(ushort*)(info + 0x10) = *(ushort*)((byte*)tim + 8);
            *(ushort*)(info + 0x12) = *(ushort*)((byte*)tim + 0xa);
            *(ushort*)(info + 0x14) = *(ushort*)((byte*)tim + 0xc);
            *(ushort*)(info + 0x16) = *(ushort*)((byte*)tim + 0xe);
            saut = (int)(tim[1] >> 2) + 1;
            clut = tim + 4;
        }
        *(uint**)(info + 0x18) = clut;
        byte* bloc = (byte*)tim + saut * 4;
        *(ushort*)(info + 4) = *(ushort*)(bloc + 4);
        *(ushort*)(info + 6) = *(ushort*)(bloc + 6);
        *(ushort*)(info + 8) = *(ushort*)(bloc + 8);
        *(ushort*)(info + 0xa) = *(ushort*)(bloc + 0xa);
        *(byte**)(info + 0xc) = bloc + 0xc;
    }
}
