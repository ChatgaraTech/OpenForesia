namespace OpenForesia.Tools.MonteeDeNiveau;

using System.Runtime.InteropServices;

/// <summary>
/// Les tables de <c>.rdata</c> que la montée de niveau et l'écran de création lisent, recopiées
/// depuis la reconstruction (<c>OpenForesia/src/Blaze/</c>) plutôt que relues dans le binaire :
/// l'outil doit rester autonome, sans <c>Blaze.exe</c> à côté.
/// </summary>
internal static class DonneesDeClasse
{
    internal static readonly string[] Noms =
        ["Guerrier", "Prêtre", "Voleur", "Sorcier", "Chasseur", "Elfe", "Nain", "Fée"];

    /// <summary>
    /// <b>Les statistiques de départ des huit classes, telles que l'écran de création les
    /// affiche.</b> Huit lignes de huit octets ; les sept premiers sont les valeurs, le huitième
    /// est nul dans toutes les lignes et la boucle du jeu n'en lit que sept.
    ///
    /// <para>
    /// <b>Elles ne sont pas dans <c>Blaze.exe</c> — elles sont sur le CD.</b> Le sous-état 5 de
    /// l'écran les résout par <c>**(int **)(DAT_005e97c4 + 0x74) + (classe &amp; 7) * 8</c>
    /// (<c>FUN_00509470</c>, désassemblé à <c>0x005094e0</c> : <c>MOV EAX,[EDX+0x74]</c> puis
    /// <c>MOV EDX,[EAX]</c> puis <c>LEA ECX,[EDX + ECX*8]</c>), c'est-à-dire dans une ressource
    /// chargée, pas dans une table du binaire.
    /// </para>
    /// <para>
    /// <b>Ne pas confondre avec <c>DAT_0056c7e0</c></b>, qui porte 24/8/8/16/16/8/16 pour le
    /// Guerrier : cette table-là est bien dans le binaire, mais ses deux seuls sites de lecture
    /// dans <c>Blaze.c</c> (lignes 82448 et 82449) sont l'<b>écran de groupe de mise au point</b>.
    /// Elle ne décrit aucun personnage jouable. C'est l'erreur que ce simulateur a portée jusqu'au
    /// 2026-08-29, et elle se voyait à l'œil nu : un Guerrier y démarrait à 24 de force quand
    /// l'écran en montre 13.
    /// </para>
    /// <para>
    /// Relevées dans <c>E:\French\BLAZE.ALL</c> à l'offset <c>0x0090f0b4</c> — la suite
    /// <c>0d 08 08 09 0b 08 09</c> n'y apparaît qu'<b>une seule fois</b> sur 46 Mo, et la première
    /// ligne se recoupe octet pour octet avec ce que l'écran affiche pour le Guerrier.
    /// TODO(REVERSE) : le blob qui contient la table n'est pas identifié — la remonter depuis
    /// <c>DAT_005e97c4</c> demanderait de reconstruire ce que <c>FUN_00529b00</c> retourne.
    /// </para>
    /// </summary>
    internal static readonly byte[] BaseParClasse =
    [
        //  FO  INT  VO  AGI  CON  PM   CH
        13,   8,   8,   9,  11,   8,   9, 0,   // 0 Guerrier
         9,  10,  13,   9,   8,  13,  11, 0,   // 1 Prêtre
        10,   8,   9,  13,   9,   8,  12, 0,   // 2 Voleur
         8,  13,  12,   9,   8,  14,  10, 0,   // 3 Sorcier
        12,   8,   9,  10,  11,   8,   9, 0,   // 4 Chasseur
        11,  10,  10,   9,   9,  12,   9, 0,   // 5 Elfe
        14,   8,   9,   8,  15,   8,   9, 0,   // 6 Nain
         6,  10,  10,  13,   6,  11,  16, 0,   // 7 Fée
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00562ab8</c> — l'école de sorts posée dans <c>+0x2b5</c>.</summary>
    internal static readonly byte[] EcoleParClasse_00562AB8 = [0, 1, 0, 0, 3, 0, 0, 2];

    /// <summary>Ghidra: <c>&amp;DAT_0056d308</c> — la caractéristique favorisée par un élément, un
    /// octet pour chacun des six premiers. Les éléments 6 et 7 poussent les six d'un vingtième.</summary>
    internal static readonly byte[] CaracFavoriseeParElement_0056D308 = [0, 1, 4, 3, 2, 5];

    /// <summary>Ghidra: <c>&amp;DAT_0056d310</c> — un dword par classe, lu quatre octets deux par
    /// deux : <c>(base physique, diviseur physique, base mentale, diviseur mental)</c>. Trois
    /// valeurs seulement pour huit classes, d'où les trois familles de progression.</summary>
    internal static readonly uint[] ChancesDeProgression_0056D310 =
    [
        0x04280241, 0x023c0423, 0x03320332, 0x023c0423,
        0x03320332, 0x03320332, 0x04280241, 0x023c0423,
    ];

    /// <summary>
    /// Les sept lignes de l'écran de répartition, <b>avec les libellés du jeu</b> — relevés à
    /// l'écran le 2026-08-29 par Vincent. Ce document les numérotait jusque-là, faute de les avoir
    /// vus ; les profils de classe les confirment un par un : le Guerrier est le plus fort (FO 13),
    /// le Nain le plus résistant (CON 15), le Sorcier le plus magique (PM 14), la Fée la plus
    /// chanceuse (CH 16) et le Voleur le plus agile (AGI 13).
    ///
    /// <para>Le regroupement du jeu tombe juste aussi : les physiques 0, 3, 4 sont FO, AGI, CON et
    /// les mentales 1, 2, 5 sont INT, VO, PM.</para>
    /// </summary>
    internal static readonly string[] NomsCaracs = ["FO", "INT", "VO", "AGI", "CON", "PM", "CH"];

    /// <summary>Le libellé long, pour l'infobulle et la synthèse. « PM » reste tel quel : l'écran
    /// ne dit pas s'il faut lire « puissance magique » ou « points de magie », et il nourrit bien
    /// la jauge de PM par la moitié de (VO + PM).</summary>
    internal static readonly string[] NomsLongs =
        ["Force", "Intelligence", "Volonté", "Agilité", "Constitution", "PM", "Chance"];

    /// <summary>L'ordre est en dur dans <c>FUN_005391f0</c> : physique, mentale, mentale,
    /// physique, physique, mentale. La septième — la chance — ne suit ni l'une ni l'autre.</summary>
    internal static readonly bool[] EstPhysique = [true, false, false, true, true, false, false];

    internal static string TypeCarac(int i) => i == 6 ? "" : EstPhysique[i] ? "physique" : "mental";

    /// <summary>Ghidra: <c>SetClassBaseHpMp</c> (sous-état 6 de la création) — les points de vie et
    /// de magie de base de la classe, avant la prime tirée des statistiques.</summary>
    internal static (byte Pv, byte Pm) PvPmDeBase(int classe) => (classe & 7) switch
    {
        0 or 6 => (0x10, 2),
        1 or 3 or 7 => (0x0e, 8),
        _ => (0x0f, 4),
    };

    internal static short[] StatistiquesDeBase(int classe)
    {
        var valeurs = new short[7];
        for (var i = 0; i < 7; i++) valeurs[i] = BaseParClasse[(classe & 7) * 8 + i];
        return valeurs;
    }

    /// <summary>Ce que l'élément favorise, en clair. <c>FUN_00508e00</c> montre que le joueur le
    /// <b>choisit</b> — un sélecteur gauche/droite masqué <c>&amp; 7</c> sur <c>+0x151</c> —, il ne
    /// découle donc pas de la classe.</summary>
    internal static string EffetDeLElement(int element) => element < 6
        ? $"pousse {NomsLongs[CaracFavoriseeParElement_0056D308[element]]} d'un cinquième"
        : "pousse les six d'un vingtième";

    /// <summary>Les deux probabilités du tour, telles que <c>FUN_005391f0</c> les calcule à partir
    /// de la <b>chance de départ</b>. Reproduite ici pour l'affichage ; la simulation, elle, les
    /// recalcule dans la fonction transcrite. Le test étant <c>rand() % 100 &lt;= p</c>, la
    /// probabilité réelle vaut <c>p + 1</c> pour cent.</summary>
    internal static (int Physique, int Mental) Probabilites(int classe, int chanceDeDepart)
    {
        var odds = ChancesDeProgression_0056D310[classe & 7];
        return (chanceDeDepart / (int)(odds >> 8 & 0xff) + (int)(odds & 0xff),
                chanceDeDepart / (int)(odds >> 0x18) + (int)(odds >> 0x10 & 0xff));
    }
}

/// <summary>L'état des neuf séries suivies, à un niveau donné.</summary>
internal sealed class Instantane
{
    internal int Niveau;
    internal short[] Caracs = new short[7];   // +0x280 .. +0x28c
    internal short PvMax;                     // +0x148
    internal short PmMax;                     // +0x14a
}

internal sealed class Resultat
{
    internal int Classe;
    internal byte Element;
    internal short[] Depart = new short[7];     // +0x2a0, figé à la création
    internal short[] Plafonds = new short[6];   // les six plafonds, élément compris
    internal List<Instantane> Etapes = [];      // niveau 1, puis un par montée
    internal uint Graine;
}

/// <summary>
/// Le simulateur : un vrai bloc de personnage de 0x2000 octets, aux mêmes décalages que le jeu,
/// et la transcription de <c>FUN_005391f0</c> qui le fait monter de niveau.
///
/// <para><b>Ce qui est reproduit à l'identique</b> : le palier d'expérience, les sept gains, les
/// deux jauges, les deux probabilités par classe, les plafonds et l'ajustement d'élément, les six
/// tirages de caractéristique, et le tirage de la chance — le seul qui peut reculer. La chaîne des
/// dérivées (<c>FUN_00538ab0</c>, <c>00538f50</c>, <c>00538c50</c>, <c>00538e20</c>,
/// <c>00538fa0</c>) est appelée à sa place, comme dans le jeu.</para>
///
/// <para><b>Les deux écarts, et pourquoi ils ne changent pas les courbes.</b> (1) L'équipement de
/// départ n'est pas chargé : <c>FinalizeCharacterStats</c> le recopie depuis la mémoire principale
/// du jeu, qui vient du CD. Ses bonus atterrissent dans <c>+0x120</c>, la copie de travail, jamais
/// dans <c>+0x280</c> ni <c>+0x2a0</c> — ni les gains ni les plafonds n'en dépendent. (2)
/// L'apprentissage de sort (<c>TryLearnSpell</c>) est absent, faute du répertoire de sorts, lui
/// aussi sur le CD. Il consomme des tirages, donc le <i>flux</i> du générateur diverge d'une vraie
/// partie après la première montée d'un lanceur ; la <i>loi</i> des tirages, elle, est inchangée —
/// un générateur congruentiel ne se biaise pas parce qu'on lui retire des appels.</para>
/// </summary>
internal sealed unsafe class Simulateur : IDisposable
{
    /// <summary>Le bloc de personnage — <c>scratch</c> dans l'écran de création, l'enregistrement
    /// de 0x2000 octets du groupe en jeu. Alloué à sa taille réelle pour que les décalages du haut
    /// du bloc (l'équipement à <c>+0x300</c>) restent valides.</summary>
    private byte* _c = (byte*)NativeMemory.AllocZeroed(0x2000);

    /// <summary>Ghidra: DAT_0056e9c0 — la graine de <c>rand</c> du CRT. Le jeu la pose à 1 au
    /// démarrage (<c>srand(1)</c>) ; l'outil la laisse choisir pour pouvoir rejouer un tirage.</summary>
    private uint _graine;

    /// <summary>Ghidra: DAT_005e85b0..DAT_005e85bc — les sept gains tirés pour le tour.</summary>
    private readonly short[] _gains_005e85b0 = new short[7];

    /// <summary>Ghidra: DAT_005e85a8 et DAT_005e85aa — les deux chances de progression du tour.</summary>
    private short _chancePhysique_005e85a8, _chanceMentale_005e85aa;

    public void Dispose()
    {
        if (_c != null) { NativeMemory.Free(_c); _c = null; }
    }

    /// <summary>Ghidra: FUN_00547140 — <c>rand()</c> du CRT de Microsoft, transcrit tel quel :
    /// c'est la suite exacte de valeurs que le jeu tire.</summary>
    private int CrtRand()
    {
        _graine = _graine * 0x343fd + 0x269ec3;
        return (int)((_graine >> 0x10) & 0x7fff);
    }

    /// <summary>
    /// Ghidra: la branche « confirmer » de <c>FUN_005098f0</c> (<c>FinalizeCharacterStats</c>),
    /// réduite à ce qui touche la progression : les sept valeurs vont <b>à la fois</b> dans
    /// <c>+0x280</c> (les courantes) et <c>+0x2a0</c> (la photographie de départ, que plus rien ne
    /// réécrira), le niveau et son plancher partent à 1, et les points de vie et de magie prennent
    /// la prime que <c>EditCharacterStats</c> calcule à chaque image dans <c>stats[7]</c> et
    /// <c>stats[8]</c> : <c>(carac0 + carac4) / 2 + PV de classe</c>, en arithmétique d'octet.
    /// </summary>
    private void CreerPersonnage(int classe, short[] valeurs, byte element)
    {
        NativeMemory.Clear(_c, 0x2000);

        _c[0x150] = (byte)(classe & 7);
        _c[0x151] = (byte)(element & 7);   // Ghidra: FUN_00508e00 — le joueur le choisit, masqué & 7

        *(short*)(_c + 0x144) = 1;
        *(short*)(_c + 0x146) = 1;
        *(uint*)(_c + 0x158) = 0;
        *(uint*)(_c + 0x114) = (uint)(1 << (_c[0x151] & 0x1f)) | 0x800;
        *(short*)(_c + 0x112) = (short)(1 << (_c[0x150] & 7));

        for (var i = 0; i < 7; i++)
        {
            *(ushort*)(_c + 0x280 + i * 2) = (ushort)valeurs[i];
            *(ushort*)(_c + 0x2a0 + i * 2) = (ushort)valeurs[i];
        }

        var (pvBase, pmBase) = DonneesDeClasse.PvPmDeBase(classe);
        var pv = (byte)((byte)(((byte)valeurs[0] + (byte)valeurs[4]) >> 1) + pvBase);
        var pm = (byte)((byte)(((byte)valeurs[2] + (byte)valeurs[5]) >> 1) + pmBase);

        *(ushort*)(_c + 0x148) = pv;
        *(ushort*)(_c + 0x14a) = pm;
        *(ushort*)(_c + 0x14c) = pv;
        *(ushort*)(_c + 0x14e) = pm;

        // Les cinq emplacements d'équipement et l'école de sorts, comme la création les pose.
        _c[0x2b0] = 0;
        _c[0x2b1] = 1;
        _c[0x2b2] = (byte)((classe & 7) is 0 or 6 ? 2 : 0xff);
        _c[0x2b3] = 0xff;
        _c[0x2b4] = 0xff;
        _c[0x2b5] = DonneesDeClasse.EcoleParClasse_00562AB8[classe & 7];

        _c[0x160] = 1; _c[0x168] = 1; _c[0x170] = 1;
        _c[0x178] = 3; _c[0x179] = 3; _c[0x17a] = 3; _c[0x17b] = 3;
        _c[0x17c] = 3; _c[0x17d] = 3; _c[0x17e] = 3;

        CommitStagedStatBlock(_c);              // Ghidra: FUN_00538ab0
        ScaleStatBlockByDifficulty(_c + 0x100); // Ghidra: FUN_00538f50
        ComputeDerivedStats(_c);                // Ghidra: FUN_00538c50
        ApplyEquipmentStatBonuses(_c);          // Ghidra: FUN_00538e20
        FinalizeEquipmentEffects(_c + 0x100);   // Ghidra: FUN_00538fa0
    }

    /// <summary>
    /// Ghidra: le bloc de plafonds de <c>FUN_005391f0</c> — <c>plafond = v/8 - v²/16 + (v+10)*10</c>
    /// sur la valeur de <b>départ</b>, puis l'ajustement d'élément. Extrait en méthode parce que
    /// l'écran de saisie l'affiche lui aussi, ligne par ligne, pendant qu'on répartit les points :
    /// c'est la même unité de calcul, pas une copie.
    ///
    /// <para>La courbe culmine à 510 pour un départ de 80 à 83 et redescend ensuite — un départ de
    /// 99 plafonne à 490, soit vingt de moins pour dix-neuf points de plus.</para>
    /// </summary>
    internal static void CalculerPlafonds(short[] depart, byte element, short[] plafonds)
    {
        for (var i = 0; i < 6; i++)
        {
            short v = depart[i];
            plafonds[i] = (short)((v >> 3) - (short)(v * v >> 4) + (v + 10) * 10);
        }

        if (element < 6)
        {
            int favorisee = DonneesDeClasse.CaracFavoriseeParElement_0056D308[element];
            plafonds[favorisee] += (short)(plafonds[favorisee] / 5);
        }
        else if (element is 6 or 7)
        {
            for (var i = 0; i < 6; i++) plafonds[i] += (short)(plafonds[i] / 0x14);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005391f0 @ 0x005391f0 — <b>la montée de niveau</b>, transcrite depuis
    /// <c>OpenForesia/src/Blaze/Screens/State03/WorldMapTick.cs</c>. Vue d'ensemble dans
    /// <c>Documentation/Reference/MonteeDeNiveau.md</c>.
    ///
    /// <para>Deux retraits par rapport à la reconstruction du jeu, tous deux hors du calcul :
    /// <c>TryLearnSpell</c> (le répertoire de sorts est sur le CD) et les deux entités d'effet de
    /// fin — l'auréole et la gerbe de soin — qui n'existent que pour l'écran. Le reste est mot pour
    /// mot le même corps, y compris la boucle et le fait qu'elle ne tourne jamais deux fois.</para>
    /// </summary>
    private void LevelUpCharacter_005391F0(byte* c)
    {
        short* scratch = stackalloc short[8];
        var depart = new short[7];
        var plafonds = new short[6];

        while (true)
        {
            ushort level = *(ushort*)(c + 0x146);

            if (level == 9999)
            {
                *(int*)(c + 0x158) = 0;
                break;
            }

            uint needed = (uint)((level >> 2) + 0x30 + level * 2);
            if (needed > 9999) needed = 9999;
            if (needed > *(uint*)(c + 0x158)) break;

            // ------------------------------------------------------------ le palier est franchi
            *(short*)(c + 0x144) += 1;
            *(int*)(c + 0x158) = 0;
            *(ushort*)(c + 0x146) = (ushort)(level + 1);

            if ((ushort)(level + 1) < *(ushort*)(c + 0x144))
            {
                *(ushort*)(c + 0x146) = *(ushort*)(c + 0x144);
            }

            // +0x2a0 : le bloc FIGE a la creation. Tout ce qui suit s'y refere — la taille des
            // gains ET les plafonds —, jamais aux valeurs courantes de +0x280.
            short* start = (short*)(c + 0x2a0);
            int seventh = start[6];

            for (var i = 0; i < 7; i++) scratch[i] = (short)((start[i] >> 3) + 1);

            for (var i = 0; i < 7; i++)
            {
                _gains_005e85b0[i] = (short)(
                    CrtRand() % scratch[i] + 1 + CrtRand() % ((seventh >> 3) + 1));
            }

            // ------------------------------------------------------------ les jauges
            *(short*)(c + 0x148) += (short)(CrtRand() % _gains_005e85b0[4] + 2);
            *(short*)(c + 0x14a) += (short)(CrtRand() % _gains_005e85b0[5] + 1);

            if (*(short*)(c + 0x148) > 9999) *(short*)(c + 0x148) = 9999;
            if (*(short*)(c + 0x14a) > 9999) *(short*)(c + 0x14a) = 9999;

            // ------------------------------------------------------------ les deux chances
            uint odds = DonneesDeClasse.ChancesDeProgression_0056D310[c[0x150] & 7];

            _chancePhysique_005e85a8 =
                (short)(seventh / (int)(odds >> 8 & 0xff) + (short)(odds & 0xff));
            _chanceMentale_005e85aa =
                (short)(seventh / (int)(odds >> 0x18) + (short)(odds >> 0x10 & 0xff));

            // ------------------------------------------------------------ les plafonds
            for (var i = 0; i < 7; i++) depart[i] = start[i];
            CalculerPlafonds(depart, c[0x151], plafonds);
            for (var i = 0; i < 6; i++) scratch[i] = plafonds[i];

            // ------------------------------------------------------------ les caracteristiques
            short* carac = (short*)(c + 0x280);

            GrowCharacteristic(carac + 0, scratch[0], _chancePhysique_005e85a8, 0);
            GrowCharacteristic(carac + 1, scratch[1], _chanceMentale_005e85aa, 1);
            GrowCharacteristic(carac + 2, scratch[2], _chanceMentale_005e85aa, 2);
            GrowCharacteristic(carac + 3, scratch[3], _chancePhysique_005e85a8, 3);
            GrowCharacteristic(carac + 4, scratch[4], _chancePhysique_005e85a8, 4);
            GrowCharacteristic(carac + 5, scratch[5], _chanceMentale_005e85aa, 5);

            for (var i = 0; i < 6; i++)
            {
                if (carac[i] > 999) carac[i] = 999;
            }

            // ------------------------------------------------------------ le champ qui recule
            int roll = CrtRand() % 100 + (seventh >> 2);
            int value = *(short*)(c + 0x28c);

            if (roll < 0x47)
            {
                if (roll < 0x1f) value -= _gains_005e85b0[6];
                else _gains_005e85b0[6] = 0;
            }
            else
            {
                value += _gains_005e85b0[6];
            }

            if (value < 0) value = 0;
            if (value > 200) value = 200;
            *(short*)(c + 0x28c) = (short)value;

            // ------------------------------------------------------------ les derivees, refaites
            CommitStagedStatBlock(c);                          // Ghidra: FUN_00538ab0
            ScaleStatBlockByDifficulty(c + 0x100);             // Ghidra: FUN_00538f50
            ComputeDerivedStats(c);                            // Ghidra: FUN_00538c50
            ApplyEquipmentStatBonuses(c);                      // Ghidra: FUN_00538e20
            FinalizeEquipmentEffects(c + 0x100);               // Ghidra: FUN_00538fa0

            // TryLearnSpell : absente hors du jeu, voir le commentaire de la classe.
        }
    }

    /// <summary>Une caracteristique monte si elle est sous son plafond et qu'un centieme passe. Le
    /// test precede l'addition, donc la valeur finale depasse legerement le plafond.</summary>
    private void GrowCharacteristic(short* value, short ceiling, short odds, int gain)
    {
        if (*value < ceiling && CrtRand() % 100 <= odds)
        {
            *value += _gains_005e85b0[gain];
        }
    }

    /// <summary>
    /// Fait monter le personnage de son niveau 1 jusqu'au niveau visé, et relève l'état après
    /// chaque montée.
    ///
    /// <para>La jauge d'expérience est posée à 9999 avant chaque appel : <c>FUN_005391f0</c>
    /// franchit alors le palier, remet la jauge à zéro <i>à l'intérieur</i> de sa boucle et ne peut
    /// pas repasser le test au second tour — un appel, un niveau, exactement comme en jeu où le
    /// surplus d'expérience est perdu.</para>
    /// </summary>
    internal static Resultat Simuler(int classe, short[] valeurs, byte element, int niveauVise, uint graine)
    {
        using var sim = new Simulateur();
        sim._graine = graine;
        sim.CreerPersonnage(classe, valeurs, element);

        var resultat = new Resultat
        {
            Classe = classe & 7,
            Element = (byte)(element & 7),
            Graine = graine,
        };

        for (var i = 0; i < 7; i++) resultat.Depart[i] = *(short*)(sim._c + 0x2a0 + i * 2);
        CalculerPlafonds(resultat.Depart, resultat.Element, resultat.Plafonds);

        resultat.Etapes.Add(sim.Relever());

        for (var niveau = 1; niveau < niveauVise; niveau++)
        {
            *(uint*)(sim._c + 0x158) = 9999;
            sim.LevelUpCharacter_005391F0(sim._c);
            resultat.Etapes.Add(sim.Relever());
        }

        return resultat;
    }

    private Instantane Relever()
    {
        var etape = new Instantane
        {
            Niveau = *(ushort*)(_c + 0x146),
            PvMax = *(short*)(_c + 0x148),
            PmMax = *(short*)(_c + 0x14a),
        };
        for (var i = 0; i < 7; i++) etape.Caracs[i] = *(short*)(_c + 0x280 + i * 2);
        return etape;
    }

    // ---------------------------------------------------------------------------------------
    // La chaine des derivees. Aucune ne tire au sort et aucune n'ecrit +0x280 ni +0x2a0 : elles
    // ne changent donc pas les courbes. Transcrites quand meme, parce que la montee de niveau les
    // appelle et qu'un ecart entre l'outil et le jeu doit rester un ecart *documente*.
    // ---------------------------------------------------------------------------------------

    /// <summary>Ghidra: FUN_00538ab0 — copie quatre dwords de <c>+0x280</c> vers <c>+0x120</c>.</summary>
    private static void CommitStagedStatBlock(byte* record)
    {
        *(uint*)(record + 0x120) = *(uint*)(record + 0x280);
        *(uint*)(record + 0x124) = *(uint*)(record + 0x284);
        *(uint*)(record + 0x128) = *(uint*)(record + 0x288);
        *(uint*)(record + 0x12c) = *(uint*)(record + 0x28c);
    }

    /// <summary>Ghidra: FUN_00538f50 — remet huit statistiques a l'echelle d'un facteur de
    /// difficulte. Le facteur est a zero ici, donc c'est l'identite ; transcrite quand meme.</summary>
    private static void ScaleStatBlockByDifficulty(byte* record)
    {
        ushort factor = *(ushort*)(record + 0xec);
        short* stats = (short*)(record + 0x20);
        for (var i = 0; i < 8; i++)
        {
            int scaled = ((0x1000 - factor * 0x200) * stats[i]) >> 0xc;
            if (scaled < 0) scaled = 0;
            if (scaled > 999) scaled = 999;
            stats[i] = (short)scaled;
        }
    }

    /// <summary>Ghidra: FUN_00538c50 — les huit statistiques derivees de <c>+0x290</c>, recopiees
    /// sur <c>+0x130</c>.</summary>
    private static void ComputeDerivedStats(byte* record)
    {
        int extra = *(ushort*)(record + 0x144);
        int dword1 = *(int*)(record + 0x124);
        int stat0 = *(ushort*)(record + 0x120);
        int stat1 = *(ushort*)(record + 0x122);
        int stat6 = *(ushort*)(record + 0x12c);
        int dword2 = *(int*)(record + 0x128);
        int stat3 = dword1 >> 0x10;
        int stat4 = dword2 & 0xffff;
        int stat5 = dword2 >> 0x10;

        *(short*)(record + 0x290) = (short)((extra + stat0) >> 2);
        *(short*)(record + 0x292) =
            (short)((((stat4 >> 2) + stat3 + stat4) >> 2) + extra >> 2);
        *(short*)(record + 0x294) = (short)(((stat0 + stat3) >> 1) + extra >> 2);

        int halfExtra = extra >> 1;
        *(short*)(record + 0x296) =
            (short)((((dword1 >> 0x12) + stat3 + stat4) >> 1) + halfExtra >> 2);
        *(short*)(record + 0x298) = (short)((extra + stat5) >> 2);

        int stat2 = dword1 & 0xffff;
        *(short*)(record + 0x29e) =
            (short)((((stat2 >> 2) + stat2 + stat1) >> 1) + halfExtra >> 2);
        *(short*)(record + 0x29a) =
            (short)((((stat1 >> 2) + stat2 + stat1) >> 2) + extra >> 2);
        *(short*)(record + 0x29c) = (short)(((stat5 + stat2) >> 1) + extra >> 2);

        short* derived = (short*)(record + 0x290);
        for (var i = 0; i < 8; i++)
        {
            if (derived[i] > 999) derived[i] = 999;
        }

        *(uint*)(record + 0x210) = 0;
        *(uint*)(record + 0x214) = 0;
        *(uint*)(record + 0x218) = 0;
        *(uint*)(record + 0x21c) = 0;

        int slot7 = (stat6 >> 5) + (*(short*)(record + 0x2a6) >> 3);
        *(ushort*)(record + 0x28e) = (ushort)slot7;
        if (slot7 > 0xf) *(ushort*)(record + 0x28e) = 0xf;

        *(ushort*)(record + 0x154) = (ushort)(*(short*)(record + 0x2a2) + (stat1 >> 3));
        *(short*)(record + 0x156) = (short)(*(short*)(record + 0x2aa) >> 2);

        for (var i = 0; i < 4; i++)
        {
            *(uint*)(record + 0x130 + i * 4) = *(uint*)(record + 0x290 + i * 4);
        }
    }

    /// <summary>Ghidra: <c>&amp;DAT_0056d2c0</c> — une prime en pourcent par palier de difficulte.</summary>
    private static readonly byte[] s_PrimeDeDifficulte_0056D2C0 =
        [0x00, 0x00, 0x0a, 0x14, 0x1e, 0x00, 0x00, 0x00, 0x00, 0x02, 0x03, 0x01, 0x04, 0x01, 0x00, 0x00];

    /// <summary>Ghidra: FUN_00538e20 — les bonus des cinq emplacements d'equipement. Hors du jeu,
    /// le bloc d'objets de <c>+0x300</c> est vide (il vient du CD), donc les bonus valent zero :
    /// c'est le premier des deux ecarts documentes sur cette classe.</summary>
    private static void ApplyEquipmentStatBonuses(byte* record)
    {
        byte* slots = record + 0x2b0;
        for (var i = 0; i < 5; i++)
        {
            byte itemId = slots[i];
            if (itemId == 0xff) continue;

            byte* item = record + 0x300 + itemId * 0x280;
            short* primary = (short*)(record + 0x120);
            short* bonus = (short*)(item + 0x20);
            for (var k = 0; k < 0x10; k++) primary[k] += bonus[k];

            if (i is 3 or 4)
            {
                short* extra = (short*)(record + 0x210);
                for (var k = 0; k < 8; k++) extra[k] += (sbyte)item[0x18 + k];
            }

            *(short*)(record + 0x156) += *(short*)(item + 0x8a);
        }

        // DAT_005de034 — le nombre de membres du groupe encore jouables, zero hors du jeu.
        byte prime = s_PrimeDeDifficulte_0056D2C0[0];
        int scale = prime + 100;
        short* stats = (short*)(record + 0x130);
        for (var k = 0; k < 8; k++) stats[k] = DivideRoundedBy100(scale * stats[k]);

        int finalScaled = scale * *(short*)(record + 0x156);
        *(short*)(record + 0x12e) += (short)(prime >> 3);
        short result = DivideRoundedBy100(finalScaled);
        if (result < 0) result = 0;
        if (result > 99) result = 99;
        *(short*)(record + 0x156) = result;
    }

    /// <summary>L'idiome de division par 100 signee que Ghidra reconstruit avec un multiplicateur
    /// magique, transcrit terme a terme.</summary>
    private static short DivideRoundedBy100(int value)
    {
        return (short)(((short)(value / 100) + (short)(value >> 0x1f))
            - (short)(((long)value * 0x51eb851f) >> 0x3f));
    }

    /// <summary>Ghidra: FUN_00538fa0 — les effets d'equipement, puis l'ecretage [0, 999] des seize
    /// statistiques. Tous les octets sources sont a zero ici : c'est un ecretage pur, transcrit
    /// branche pour branche comme dans la reconstruction du jeu.</summary>
    private static void FinalizeEquipmentEffects(byte* record)
    {
        uint flags = *(uint*)(record + 0x40);
        uint masked = flags & 0x10200000;
        int fill = masked switch
        {
            0 => 0,
            0x200000 => 0x1010101,
            0x10000000 => -1,
            0x10200000 => 0,
            _ => 0,
        };

        byte* counters = record + 0x108;
        *(int*)(record + 0x10c) = fill;
        *(int*)counters = fill;

        if ((flags & 0x400) != 0) counters[0]++;
        if ((flags & 0x1000) != 0) counters[1]++;
        if ((flags & 0x10000) != 0) counters[4]++;
        if ((flags & 0x40000) != 0) counters[5]++;
        if ((flags & 0x800) != 0) counters[0]--;
        if ((flags & 0x2000) != 0) counters[1]--;
        if ((flags & 0x20000) != 0) counters[4]--;
        if ((flags & 0x80000) != 0) counters[5]--;
        if ((flags & 0x200) != 0) { counters[0]++; counters[1]--; }

        if (*(short*)(record + 0x86) != 0) *(short*)(record + 0x110) += *(short*)(record + 0xe4);
        if (*(short*)(record + 0x8c) != 0) *(short*)(record + 0x112) += *(short*)(record + 0xea);
        if (*(short*)(record + 0x88) != 0) *(short*)(record + 0x114) += *(short*)(record + 0xe6);
        if (*(short*)(record + 0x8a) != 0) *(short*)(record + 0x116) += *(short*)(record + 0xe8);

        short* derived = (short*)(record + 0x30);
        for (var i = 0; i < 8; i++)
        {
            if (counters[i] == 0) continue;
            derived[i] = (counters[i] & 0x80) == 0
                ? (short)((derived[i] >> 1) + derived[i])
                : (short)(derived[i] >> 1);
        }

        if (*(short*)(record + 0xba) != 0)
        {
            ushort index = *(ushort*)(record + 0xf8);
            short delta = *(short*)(record + 0xfa);
            if (index < 7) *(short*)(record + 0x20 + index * 2) += delta;
            else if (index < 8) *(short*)(record + 0x2e) += delta;
            else if (index < 0x10) *(short*)(record + 0x20 + index * 2) += delta;
            else if (index < 0x18) *(short*)(record + 0xf0 + index * 2) += delta;
        }

        short* primary = (short*)(record + 0x20);
        for (var i = 0; i < 0x10; i++)
        {
            short value = primary[i];
            if (value < 0) value = 0;
            if (value > 999) value = 999;
            primary[i] = value;
        }
    }
}
