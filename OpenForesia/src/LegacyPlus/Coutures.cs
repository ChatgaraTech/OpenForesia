// =====================================================================
// Les coutures — les seuls points par lesquels le code transcrit s'ouvre aux tweaks.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-tweaks-difficulty, design D1-D2).
// Le code de `src/Blaze/` appelle une couture en une ligne, marquée `// Couture legacy ++ (LP-NNN)`,
// et ne nomme jamais un tweak. Chaque couture vaut par défaut l'identité : aucun tweak actif, c'est
// le jeu de 1998.
//
// L'installation n'a pas de ligne au démarrage : le constructeur statique lit `LegacyPlus.ini` et
// installe les tweaks au premier appel d'une couture. C'est un constructeur de type, donc son ordre
// est garanti — contrairement aux initialiseurs d'une classe partielle répartie sur plusieurs
// fichiers.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class Coutures
{
    /// <summary>Les dégâts d'un coup, de mêlée ou de sort, avant qu'ils ne soient retirés.</summary>
    internal delegate short Degats(uint* attaquant, uint* victime, short degats);

    /// <summary>Les PV d'une entité de scène à la naissance de ses statistiques.</summary>
    internal delegate ushort Pv(byte* stats, ushort pv);

    /// <summary>
    /// Couture LP-001 — à l'entrée d'<c>ApplyDamage_00539A20</c> (Ghidra: FUN_00539a20), par où
    /// passent tous les coups. Rend le montant à retirer.
    /// </summary>
    internal static Degats DegatsSubis = (attaquant, victime, degats) => degats;

    /// <summary>
    /// Couture LP-002 — dans <c>ResetEntityStats</c> (Ghidra: FUN_005389e0), sur les PV maximaux
    /// (<c>+0x48</c>) et courants (<c>+0x4c</c>). Pure : les deux appels rendent la même valeur.
    /// </summary>
    internal static Pv PvALaNaissance = (stats, pv) => pv;

    /// <summary>Le montant fixe d'une fiche de sort, avant qu'il ne s'ajoute aux dégâts.</summary>
    internal delegate ushort Montant(uint* attaquant, byte* blocAttaquant, ushort montant);

    /// <summary>
    /// Couture LP-003 — dans <c>ResolveSpellHit_0053A2B0</c> (Ghidra: FUN_0053a2b0), sur le
    /// montant de la fiche (<c>fiche + 0x18 + variante * 2</c>), à l'intérieur de la condition
    /// d'élément d'origine. Rend le montant à ajouter.
    /// </summary>
    internal static Montant MontantDeSort = (attaquant, blocAttaquant, montant) => montant;

    /// <summary>
    /// Lequel des deux balayages du groupe appelle — ils ne se ressemblent pas : la mêlée ne tourne
    /// que sur la dernière image d'un geste, un projectile cherche sa cible à chaque image.
    /// </summary>
    internal enum Balayage
    {
        /// <summary>Ghidra: FUN_00519940 — la mêlée du groupe (<c>Physics/TriggerVolumes.cs</c>).</summary>
        Melee,
        /// <summary>Ghidra: FUN_00519af0 — la cible d'un projectile ou d'un sort (<c>Combat/Projectiles.cs</c>).</summary>
        Projectile,
    }

    /// <summary>Le refus d'un coup à une victime dont le verrou d'invulnérabilité court.</summary>
    internal delegate bool Protection(Balayage balayage, byte* source, byte* victime, bool verrouOrigine);

    /// <summary>Un fait, sans valeur à rendre : un coup porté.</summary>
    internal delegate void Coup(Balayage balayage, byte* source, byte* victime);

    /// <summary>
    /// Couture LP-004 — dans les deux balayages qui cherchent un ennemi à toucher pour le groupe.
    /// Rend vrai si le coup est refusé ; par défaut, la condition d'origine : le verrou
    /// <c>+0x96</c> court.
    /// </summary>
    internal static Protection VictimeProtegee = (balayage, source, victime, verrouOrigine) => verrouOrigine;

    /// <summary>Les bits d'état qui écartent une victime d'un balayage.</summary>
    internal delegate uint Exclusion(Balayage balayage, byte* source, byte* victime, uint exclusion);

    /// <summary>
    /// Couture LP-004 — le filtre d'état des mêmes balayages, <c>mot &amp; 0x50102040</c>. Son bit
    /// <c>0x40</c> (« encaisse un coup ») est posé par chaque coup et ne retombe qu'à la fin du
    /// clignotement : c'est le second verrou. Rend les bits qui écartent la victime ; par défaut,
    /// ceux d'origine.
    /// </summary>
    internal static Exclusion VictimeExclue = (balayage, source, victime, exclusion) => exclusion;

    /// <summary>Couture LP-004 — les mêmes balayages ont trouvé leur victime. Par défaut, rien.</summary>
    internal static Coup CoupPorte = (balayage, source, victime) => { };

    /// <summary>
    /// Couture LP-004 — en tête du pas de simulation, dans la boucle par image de <c>RunWorldMapTick</c> (Ghidra: FUN_00516fc0) : une image de la
    /// carte du monde commence. Par défaut, rien.
    /// </summary>
    internal static Action ImageSuivante = () => { };

    /// <summary>L'entité qu'un membre du groupe « touche » ce tour-ci.</summary>
    internal delegate byte* Contact(uint* membre, byte* touche, byte* poolButin);

    /// <summary>
    /// Couture LP-005 — dans le pas d'un membre du groupe (<c>Tick/PartyMemberStep.cs</c>), sur
    /// l'entité que la collision désigne (Ghidra: FUN_005197d0) avant le code de ramassage.
    /// <c>poolButin</c> est la table du butin tombé (<c>0x005f7de0</c>, 0x40 × 0x9c). Par défaut,
    /// l'entité de la collision.
    /// </summary>
    internal static Contact ButinAtteint = (membre, touche, poolButin) => touche;

    /// <summary>La vitesse au sol d'un membre du groupe.</summary>
    internal delegate int Vitesse(uint* membre, int vitesse);

    /// <summary>
    /// Couture LP-006 — dans <c>ApplyGroundMotion_0051A020</c> (Ghidra: FUN_0051a020), postures
    /// d'incantation, là où le jeu remet la vitesse au sol <c>+0x5c</c> à zéro pendant qu'on
    /// incante. Reçoit la vitesse que la croix vient de poser, rend celle à garder ; par défaut,
    /// zéro — la ligne d'origine.
    /// </summary>
    internal static Vitesse VitesseEnIncantation = (membre, vitesse) => 0;

    /// <summary>La tentative de rencontre aléatoire d'une image, et le pool des entités de scène.</summary>
    internal delegate void Tentative(Action rencontre, byte* poolScene);

    /// <summary>
    /// Couture LP-007 — l'appel de <c>TrySpawnRandomEncounter</c> (Ghidra: FUN_0050ed70) dans le pas
    /// de simulation. <c>poolScene</c> est l'en-tête du pool des entités de scène (<c>0x005f2d00</c>) :
    /// son octet bas est le nombre d'entités vivantes. Par défaut, un appel.
    /// </summary>
    internal static Tentative Rencontre = (rencontre, poolScene) => rencontre();

    /// <summary>
    /// Couture LP-007 — la première garde de <c>TrySpawnRandomEncounter</c> : le minuteur court.
    /// Par défaut, la condition d'origine.
    /// </summary>
    internal static Func<bool, bool> RencontreEnAttente = attente => attente;

    /// <summary>
    /// Couture LP-007 — la garde « une entité de scène vit encore » de
    /// <c>TrySpawnRandomEncounter</c>. Par défaut, la condition d'origine.
    /// </summary>
    internal static Func<bool, bool> RencontreBloquee = bloquee => bloquee;

    /// <summary>
    /// Couture LP-007 — les deux rechargements du minuteur de rencontre (l'entrée de sous-zone, et
    /// chaque entité de scène vivante qui le trouve à zéro). Reçoit le délai tiré par le jeu, rend
    /// celui à poser ; par défaut, le même.
    /// </summary>
    internal static Func<int, int> DelaiRencontre = delai => delai;

    /// <summary>Le pas d'un membre du groupe, à son entrée : le membre, sa manette, son bloc de personnage, le descripteur de la boîte du monde.</summary>
    internal delegate void Pas(uint* membre, byte* pad, byte* perChar, byte* descripteur);

    /// <summary>
    /// Couture LP-008 — à l'entrée de <c>TickPartyMember</c> (Ghidra: FUN_0050bf20), après la garde
    /// « membre inactif » : chaque membre actif, à chaque image de simulation. <c>pad + 2</c> porte
    /// les boutons pressés à cette image ; un tweak peut y effacer un bit pour consommer l'appui.
    /// <c>descripteur</c> est <c>s_WorldMapDescriptor_005633b0</c>. Par défaut, rien.
    /// </summary>
    internal static Pas PasDuMembre = (membre, pad, perChar, descripteur) => { };

    /// <summary>Une sur-impression dessinée par un tweak : l'ordering table et le curseur de primitives, rendu avancé.</summary>
    internal delegate uint* Dessin(uint* otBase, uint* curseur);

    /// <summary>
    /// Couture LP-008 — dans <c>DrawWorldMapFrame</c> (<c>Tick/WorldMapHud.cs</c>), juste après le
    /// panneau d'interface : un tweak y ajoute ses primitives à l'ordering table de l'image. Appelée à
    /// chaque image dessinée, gel compris. Par défaut, le curseur tel quel.
    /// </summary>
    internal static Dessin DessinSurImpression = (otBase, curseur) => curseur;

    /// <summary>L'entrée d'archive d'une sous-zone, lue dans la table <c>+0x48</c> du répertoire de zone.</summary>
    internal delegate int Repertoire(int* table, int sousZone);

    /// <summary>
    /// Couture LP-009 — dans <c>LoadWorldMapSubArea</c> (Ghidra: FUN_00516350), la lecture de
    /// l'entrée d'archive de la sous-zone à charger. Par défaut, <c>table[sousZone]</c>.
    /// </summary>
    internal static Repertoire EntreeSousZone = (table, sousZone) => table[sousZone];

    /// <summary>Une liste d'apparitions de 0x20 octets par enregistrement, terminée par <c>0xffffffff</c>.</summary>
    internal delegate uint* Liste(int indice, uint* liste);

    /// <summary>
    /// Couture LP-009 — dans <c>SpawnSubAreaEntities</c> (Ghidra: FUN_005350b0), la liste que le
    /// peuplement va parcourir. <c>indice</c> peut valoir <c>0xff</c> et la liste être quelconque :
    /// l'original la lit avant de tester. Par défaut, la liste des données.
    /// </summary>
    internal static Liste ListeDApparition = (indice, liste) => liste;

    /// <summary>Une entité vient de naître d'un enregistrement d'apparition.</summary>
    internal delegate void Naissance(uint* enregistrement, uint* entite);

    /// <summary>
    /// Couture LP-009 — dans <c>SpawnSubAreaEntities</c>, juste après la pose d'une entité sur le
    /// terrain. Un tweak peut la rendre au néant (mot d'état à zéro, comme <c>ClearEntitySlots</c>)
    /// sans décaler le rang des suivantes. Par défaut, rien.
    /// </summary>
    internal static Naissance DecorNe = (enregistrement, entite) => { };

    /// <summary>
    /// Couture LP-009 — à la fin de l'opcode 17 (<c>ScriptOpTransferArea</c>, Ghidra: FUN_00515a00),
    /// quand un script vient de poser zone, sous-zone et point d'entrée d'un changement de carte.
    /// Par défaut, rien.
    /// </summary>
    internal static Action TransfertScripte = () => { };

    /// <summary>Un point d'entrée de sous-zone : huit dwords, lus par le placement du groupe.</summary>
    internal delegate uint* Point(uint* point);

    /// <summary>
    /// Couture LP-009 — dans <c>PlaceWorldMapParty</c> (Ghidra: FUN_00516350), le point d'entrée où le
    /// groupe arrive après un rechargement. Par défaut, celui des données.
    /// </summary>
    internal static Point PointDEntree = point => point;

    /// <summary>
    /// Couture LP-009 — dans le même placement, l'écart tiré au hasard autour du point d'entrée
    /// (un par axe, à chaque essai). Le tirage a lieu quoi qu'il arrive ; par défaut, l'écart tiré.
    /// </summary>
    internal static Func<int, int> EcartDeReprise = ecart => ecart;

    /// <summary>Les quatre octets du lieu de reprise d'une sauvegarde (<c>+0x1f00..+0x1f03</c>).</summary>
    internal delegate void Lieu(byte* entete);

    /// <summary>
    /// Couture LP-009 — aux deux écritures du lieu de reprise d'une sauvegarde : le menu
    /// (<c>CopyGlobalBanksIntoSaveBlock</c>) et le coffre de stockage (<c>FillDepotSaveBlock</c>),
    /// avant la somme de contrôle. Par défaut, rien.
    /// </summary>
    internal static Lieu LieuSauve = entete => { };

    /// <summary>
    /// Couture LP-012 — en tête de <c>TestTriggerVolumes_00534CD0</c> (Ghidra: FUN_00534cd0), après
    /// la garde d'origine (le bit <c>0x10</c> du monde, qui gèle aussi la boucle et ne peut donc pas
    /// servir) : vrai tait tous les volumes de déclenchement de cette image — aucun script lancé,
    /// aucun compte de passage. Par défaut, faux.
    /// </summary>
    internal static Func<bool> VolumesTus = () => false;

    /// <summary>
    /// Couture LP-012 — dans le panneau de la carte (<c>Tick/WorldMapHud.cs</c>), au passage à
    /// l'état 9 (la fin de partie) quand tout le groupe est tombé et que le fondu de mort est fini :
    /// vrai, le tweak a pris la main et le passage n'a pas lieu. Par défaut, faux.
    /// </summary>
    internal static Func<bool> DefaiteEvitee = () => false;

    /// <summary>Un script de la sous-zone sur le point d'être lancé : son indice et le nom de l'appelant.</summary>
    internal delegate bool Script(uint indice, string appelant);

    /// <summary>
    /// Couture LP-012 — en tête de <c>RunEntityScript</c> (Ghidra: FUN_00513dd0), par où passent tous
    /// les scripts de la sous-zone (table <c>contexte + 0x8c</c>) : arrivée, entrée de section,
    /// volumes, entité frappée ou morte, dialogues. Vrai : le script n'est pas lancé. Par défaut, faux.
    /// </summary>
    internal static Script ScriptTu = (indice, appelant) => false;

    static Coutures()
    {
        try
        {
            ReglagesLegacyPlus.Lire();
            Difficulte.Installer();
            ScalingMagique.Installer();
            VerrouParAttaquant.Installer();
            RamassageAuto.Installer();
            DeplacementEnIncantation.Installer();
            Rencontres.Installer();
            HubAuberge.Installer();
            MarchandsHub.Installer();
            AreneTeleporteur.Installer();
            Arene.Installer();
            AreneDirecteur.Installer();
#if DEBUG
            // Les outils de diagnostic n'existent que dans la version Debug : la version publiée
            // (publish-release.ps1, -c Release) ne les installe jamais, même avec la clé.
            TraceCombat.Installer();
            ReleveVram.Installer();
            TracePosition.Installer();                 // en dernier : il observe les décisions des tweaks
#endif
        }
        catch (Exception ex)
        {
            // Un réglage cassé ne doit jamais empêcher le jeu de tourner : retour au jeu de 1998.
            Console.Error.WriteLine($"[LEGACY++] installation des tweaks impossible ({ex.Message}) — aucun tweak actif");
            DegatsSubis = (attaquant, victime, degats) => degats;
            PvALaNaissance = (stats, pv) => pv;
            MontantDeSort = (attaquant, blocAttaquant, montant) => montant;
            VictimeProtegee = (balayage, source, victime, verrouOrigine) => verrouOrigine;
            VictimeExclue = (balayage, source, victime, exclusion) => exclusion;
            CoupPorte = (balayage, source, victime) => { };
            ImageSuivante = () => { };
            ButinAtteint = (membre, touche, poolButin) => touche;
            VitesseEnIncantation = (membre, vitesse) => 0;
            PasDuMembre = (membre, pad, perChar, descripteur) => { };
            DessinSurImpression = (otBase, curseur) => curseur;
            Rencontre = (rencontre, poolScene) => rencontre();
            RencontreEnAttente = attente => attente;
            RencontreBloquee = bloquee => bloquee;
            DelaiRencontre = delai => delai;
            EntreeSousZone = (table, sousZone) => table[sousZone];
            ListeDApparition = (indice, liste) => liste;
            DecorNe = (enregistrement, entite) => { };
            TransfertScripte = () => { };
            PointDEntree = point => point;
            EcartDeReprise = ecart => ecart;
            LieuSauve = entete => { };
            VolumesTus = () => false;
            DefaiteEvitee = () => false;
            ScriptTu = (indice, appelant) => false;
        }
    }
}
