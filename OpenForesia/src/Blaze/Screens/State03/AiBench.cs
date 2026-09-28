// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// ECHAFAUDAGE — CE FICHIER N'EST PAS UNE TRANSCRIPTION. Rien ici ne vient de Blaze.c : c'est la
// « salle d'essai », un banc qui exerce les comportements d'ennemi sans qu'il faille jouer jusqu'à
// eux. Voir Documentation/Ennemies/Comportement.md et Documentation/Tests/test-protocol.md.
//
// Le problème qu'il résout : 166 gestionnaires d'entité de scène existent, trois sont écrits, et le
// seul moyen d'en réveiller un était d'amener le groupe devant la bête correspondante. Une
// sous-zone ne montre qu'une poignée de ses comportements par partie, et une rencontre aléatoire ne
// se commande pas.
//
// Ce que le banc fait, à la place : il lit la table d'espèces de la sous-zone chargée, en retient
// UNE espèce par sous-type, et les fait toutes naître d'un coup autour du groupe. Un lieu, une
// vingtaine de secondes, et tous les comportements que ce lieu sait installer ont tourné.
//
// Il ne triche sur rien d'autre : les entités naissent par SpawnEntityFormation, la fonction du jeu
// (FUN_0051b850), avec de vraies espèces, de vraies fiches et de vrais modèles. Seule la LISTE de
// formation est fabriquée — exactement comme TrySpawnRandomEncounter (FUN_0050ed70) fabrique la
// sienne en écrivant les positions dans les enregistrements avant l'appel.

using System;
using System.Collections.Generic;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // Réglage
    // =============================================================================================

    /// <summary>
    /// <c>OPENFORESIA_AI_BENCH</c> — la salle d'essai. Trois formes :
    /// <list type="bullet">
    /// <item><c>1</c> : peupler la sous-zone courante, où que le groupe se trouve, et le refaire à
    /// chaque changement de lieu.</item>
    /// <item><c>3:0</c> : se transférer d'abord en zone 3, sous-zone 0, puis peupler.</item>
    /// <item><c>3:0:2</c> : idem, en arrivant par le point d'entrée 2.</item>
    /// </list>
    /// </summary>
    private static readonly string s_AiBenchSpec =
        Environment.GetEnvironmentVariable("OPENFORESIA_AI_BENCH") ?? "";

    /// <summary>
    /// <c>OPENFORESIA_AI_BENCH_TOUR=3:6,3:7,5:0,…</c> — <b>la tournée</b> : le banc visite ces
    /// sous-zones l'une après l'autre, peuple chacune, y reste
    /// <see cref="s_BenchTourDwellMs"/>, puis passe à la suivante. Une sous-zone finie est
    /// ajoutée à <c>tournee-faite.txt</c> à côté de l'exécutable, et sautée au lancement suivant :
    /// une tournée interrompue reprend où elle s'était arrêtée. La liste est calculée par
    /// <c>OpenForesia.Tools/tournee-auto.sh</c> depuis <c>GestionnairesEntites.md</c>.
    /// La tournée implique le banc : <c>OPENFORESIA_AI_BENCH</c> n'a pas besoin d'être posé.
    /// </summary>
    private static readonly string s_AiBenchTourSpec =
        Diagnostic.Actif ? Environment.GetEnvironmentVariable("OPENFORESIA_AI_BENCH_TOUR") ?? "" : "";   // Release : jamais de tournee-faite.txt

    /// <summary><c>OPENFORESIA_AI_BENCH_DWELL</c> — le séjour par sous-zone de la tournée, en
    /// secondes d'horloge (40 par défaut). En horloge et non en images : la cadence du jeu varie
    /// d'une machine à l'autre, et le temps que Vincent attend, lui, ne varie pas.</summary>
    private static readonly long s_BenchTourDwellMs =
        (int.TryParse(Environment.GetEnvironmentVariable("OPENFORESIA_AI_BENCH_DWELL"), out int dwell)
            && dwell > 0 ? dwell : 40) * 1000L;

    private static readonly bool s_AiBench =
        s_AiBenchSpec.Length != 0 && s_AiBenchSpec != "0" || s_AiBenchTourSpec.Length != 0;

    /// <summary>
    /// <c>OPENFORESIA_AI_BENCH_ORPHANS=1</c> — <b>greffer</b> les sous-types installés qu'aucune
    /// espèce du lieu ne porte, sur une espèce locale.
    ///
    /// <para>
    /// Pourquoi c'est nécessaire : le relevé du CD dit qu'une sous-zone ne porte que trois à huit
    /// espèces, donc trois à six comportements, alors que sa table en installe jusqu'à onze. La
    /// forêt en est l'exemple — le bloc A couvre onze sous-types, mais la sous-zone 0 n'a que les
    /// espèces 2, 0 et 1. Sans greffe, il faut visiter les cinq sous-zones du bloc pour exercer les
    /// onze.
    /// </para>
    /// <para>
    /// <b>Ce que la greffe prouve, et ce qu'elle ne prouve pas.</b> Elle exerce le corps de la
    /// routine — ses postures, ses tirages, son combat — avec de vraies statistiques. Mais le
    /// modèle est celui de l'espèce hôte, pas celui de la bête à qui ce comportement appartient :
    /// une levée venant de l'ANIMATION sous greffe peut n'être qu'un décalage d'index de geste, et
    /// demande d'être revue dans la sous-zone où l'espèce vit vraiment. Une levée venant de la
    /// routine elle-même, en revanche, est réelle.
    /// </para>
    /// </summary>
    private static readonly bool s_AiBenchOrphans =
        Environment.GetEnvironmentVariable("OPENFORESIA_AI_BENCH_ORPHANS") == "1";

    /// <summary>Images d'attente entre l'arrivée dans une sous-zone et la naissance des bêtes. Le
    /// groupe est posé au point d'entrée par le chargement ; naître avant qu'il ait sa position
    /// mettrait tout le monde au même endroit.</summary>
    private const int BenchGraceFrames = 90;

    /// <summary>Période du bilan, en images.</summary>
    private const int BenchReportPeriod = 600;

    // =============================================================================================
    // État
    // =============================================================================================

    private static bool s_BenchParsed;
    private static int s_BenchZone = -1;          // -1 = ne pas se transférer
    private static int s_BenchSubArea = -1;
    private static byte s_BenchEntryPoint;
    private static bool s_BenchTransferIssued;

    private static int s_BenchFrame;
    private static int s_BenchArrivedFrame = -1;
    private static int s_BenchLastZone = -1;
    private static int s_BenchLastSubArea = -1;
    private static bool s_BenchSpawned;
    private static int s_BenchReportedThrowCount = -1;

    private readonly record struct BenchRow(
        int SubType, uint Handler, int Species, int Model, string Name, bool Grafted);

    private static readonly List<BenchRow> s_BenchRows = [];

    /// <summary>Les étapes de la tournée qui restent, déjà privées de celles de
    /// <c>tournee-faite.txt</c>.</summary>
    private static readonly List<(int Zone, int SubArea)> s_BenchTour = [];

    private static int s_BenchTourIndex = -1;         // -1 = pas encore partie
    private static long s_BenchTourIssuedMs;          // quand le dernier transfert est parti
    private static long s_BenchTourArrivedMs = -1;    // quand l'étape courante a été atteinte
    private static bool s_BenchTourOver;

    /// <summary>Une étape jamais atteinte au bout de ce délai est abandonnée : un script d'arrivée
    /// qui renvoie ailleurs, ou une sous-zone qui ne se charge pas, ne doit pas figer la tournée.</summary>
    private const long BenchTourGiveUpMs = 60_000;

    private static string BenchTourDonePath =>
        System.IO.Path.Combine(AppContext.BaseDirectory, "tournee-faite.txt");

    /// <summary>Appels et levées par gestionnaire, tous balayeurs confondus.</summary>
    private static readonly Dictionary<uint, int> s_BenchCalls = [];
    private static readonly Dictionary<uint, int> s_BenchThrows = [];
    private static readonly Dictionary<uint, string> s_BenchFirstThrow = [];

    // =============================================================================================
    // La boucle
    // =============================================================================================

    /// <summary>
    /// Un tour de banc, appelé une fois par image depuis la boucle de la carte du monde.
    ///
    /// <para>
    /// <b>La lecture du réglage est paresseuse, et ce n'est pas un détail de style</b> : un
    /// initialiseur de champ statique qui lit un membre d'un AUTRE fichier de la classe partielle
    /// s'exécute dans un ordre non spécifié — le piège documenté dans ARCHITECTURE.md, qui avait
    /// donné huit champs à <c>null + décalage</c>. Ici tout se décide au premier tour, quand tout
    /// est en place.
    /// </para>
    /// </summary>
    private static void TickAiBench()
    {
        if (!s_AiBench) return;

        if (!s_BenchParsed)
        {
            s_BenchParsed = true;
            ParseBenchSpec();
        }

        // RIEN AVANT QU'UN GROUPE EXISTE. L'ecran-titre fait tourner la carte du monde en
        // demonstration, donc cette fonction est appelee bien avant qu'une partie soit chargee. Le
        // premier essai du 2026-08-29 l'a decouvert de la pire facon : le transfert est parti
        // pendant la demonstration et PlaceWorldMapParty a lu un point d'entree a travers un
        // repertoire qui n'etait pas le sien — 0xC0000005 avant meme la premiere frappe du script.
        if (s_PartySize_005e9020 == 0) return;

        s_BenchFrame++;

        int zone = Res.s_Unknown_005de528 & 0xff;
        int subArea = s_Unknown_005de52c;

        // ---- 1. la tournée, ou le transfert unique ----------------------------------------------
        if (s_BenchTour.Count != 0)
        {
            if (TickBenchTour()) return;
        }
        else if (s_BenchZone >= 0 && !s_BenchTransferIssued
            && (zone != s_BenchZone || subArea != s_BenchSubArea))
        {
            TransferForBench();
            return;
        }

        // ---- 2. l'arrivée : tout changement de lieu réarme le banc ------------------------------
        //
        // La détection porte sur les marqueurs du CHARGEUR (0x00566c84 et 0x00566c88), pas sur les
        // globales de destination lues ci-dessus : le chargement les pose une fois la sous-zone
        // réellement en place, et la boucle externe les remet à 0xff quand elle repart. Se fier aux
        // destinations réarmerait le banc sur l'intention plutôt que sur l'arrivée.
        zone = s_Unknown_00566c84;
        subArea = s_Unknown_00566c88;

        if (zone == 0xff || subArea == 0xff) return;

        if (zone != s_BenchLastZone || subArea != s_BenchLastSubArea)
        {
            s_BenchLastZone = zone;
            s_BenchLastSubArea = subArea;
            s_BenchArrivedFrame = s_BenchFrame;
            s_BenchSpawned = false;
            return;
        }

        // ---- 3. la naissance --------------------------------------------------------------------
        if (!s_BenchSpawned)
        {
            if (s_BenchFrame - s_BenchArrivedFrame < BenchGraceFrames) return;

            s_BenchSpawned = true;
            SpawnBenchEntities(zone, subArea);
            return;
        }

        // ---- 4. le bilan ------------------------------------------------------------------------
        if ((s_BenchFrame - s_BenchArrivedFrame) % BenchReportPeriod == 0) PrintBenchReport();
    }

    /// <summary><c>zone:sous-zone</c>, <c>zone:sous-zone:point d'entrée</c>, ou <c>1</c> pour rester
    /// sur place.</summary>
    private static void ParseBenchSpec()
    {
        if (s_AiBenchTourSpec.Length != 0)
        {
            ParseBenchTour();
            return;
        }

        string[] parts = s_AiBenchSpec.Split(':');

        if (parts.Length < 2)
        {
            Console.Error.WriteLine("[BANC] sous-zone courante, sans transfert");
            return;
        }

        if (!int.TryParse(parts[0], out int zone) || !int.TryParse(parts[1], out int sub))
        {
            Console.Error.WriteLine(
                $"[BANC] reglage illisible « {s_AiBenchSpec} » — attendu 1, zone:sous-zone, " +
                "ou zone:sous-zone:entree. Le banc reste sur place.");
            return;
        }

        s_BenchZone = zone;
        s_BenchSubArea = sub;

        if (parts.Length >= 3 && byte.TryParse(parts[2], out byte entry)) s_BenchEntryPoint = entry;
    }

    /// <summary>Le transfert unique de la forme <c>zone:sous-zone[:entrée]</c>.</summary>
    private static void TransferForBench()
    {
        BenchWarp(s_BenchZone, s_BenchSubArea, s_BenchEntryPoint);
        s_BenchTransferIssued = true;
    }

    /// <summary>
    /// Le transfert de salle, <b>par la séquence du menu de débogage</b> — la ligne QUEST de
    /// <c>WorldMapDebugMenu.cs</c> suivie de <see cref="CommitDebugWarp"/>, celle que Vincent
    /// emploie en jeu et qui marche.
    ///
    /// <para>
    /// Jusqu'au 2026-09-25 le banc imitait l'opcode 17 (<see cref="ScriptOpTransferArea"/>) et
    /// mourait en <see cref="PlaceWorldMapParty"/> dès qu'une partie était chargée (voir
    /// test-protocol.md, 2026-08-29). Ce qui manquait est ce que le menu écrit en plus : les
    /// deux <c>0xff</c> dans <c>0x00566c84</c> et <c>0x00566c88</c>, les mémoires de zone et de
    /// sous-zone chargées. Sans eux le chargeur croit la zone déjà en place, saute son
    /// chargement, et pose le groupe à travers le répertoire de l'ANCIENNE zone — exactement le
    /// « 1 primitive par image » relevé ce jour-là. La zone est donc toujours rechargée entière,
    /// même pour changer de sous-zone dans la même zone, comme QUEST le fait.
    /// </para>
    /// </summary>
    private static void BenchWarp(int zone, int subArea, byte entryPoint)
    {
        Res.s_Unknown_005de528 =
            (int)((uint)Res.s_Unknown_005de528 & 0xffffff00 | (uint)(zone & 0xff));
        s_Unknown_00566c84 = 0xff;
        s_Unknown_005de52c = (byte)subArea;

        // CommitDebugWarp, puis le point d'entree : le menu pose 0, le banc peut en demander un autre.
        CommitDebugWarp();
        s_Unknown_005de530 = entryPoint;

        // Le rechargement vide la table d'entites : meme revenu au meme lieu, il faut repeupler.
        s_BenchLastZone = -1;
        s_BenchLastSubArea = -1;

        Console.Error.WriteLine(
            $"[BANC] transfert vers zone {zone} sous-zone {subArea} point d'entree {entryPoint}");
    }

    // =============================================================================================
    // La tournée
    // =============================================================================================

    /// <summary>
    /// <c>z:s,z:s,…</c>, moins ce que <c>tournee-faite.txt</c> porte déjà.
    ///
    /// <para>
    /// Le fichier est un journal, une ligne par évènement : <c>z:s en-cours</c> au départ vers une
    /// étape, <c>z:s</c> quand elle est finie, <c>z:s interrompue</c> quand le jeu est fermé
    /// normalement pendant l'étape, <c>z:s plante</c> quand elle a fait tomber le jeu. Une étape
    /// restée « en-cours » à la relecture est donc une étape pendant laquelle le processus est
    /// mort : elle est notée <c>plante</c> et sautée, sans quoi la tournée rejouerait le même
    /// plantage à chaque lancement — ce qui s'est produit au premier essai, dans l'auberge.
    /// </para>
    /// </summary>
    private static void ParseBenchTour()
    {
        var state = new Dictionary<string, string>(StringComparer.Ordinal);

        try
        {
            if (System.IO.File.Exists(BenchTourDonePath))
            {
                foreach (string raw in System.IO.File.ReadAllLines(BenchTourDonePath))
                {
                    string[] words = raw.Trim().Split(' ', 2);
                    if (words[0].Length == 0) continue;

                    string what = words.Length > 1 ? words[1].Trim() : "faite";

                    // Une etape finie ou plantee le reste ; « interrompue » annule « en-cours ».
                    state.TryGetValue(words[0], out string before);
                    if (before == "faite" || before == "plante") continue;

                    if (what == "interrompue") state.Remove(words[0]);
                    else state[words[0]] = what;
                }
            }
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[BANC] tournee-faite.txt illisible ({error.Message}) — on repart de zero");
        }

        foreach (KeyValuePair<string, string> entry in new List<KeyValuePair<string, string>>(state))
        {
            if (entry.Value != "en-cours") continue;

            state[entry.Key] = "plante";
            BenchTourAppend($"{entry.Key} plante");
            Console.Error.WriteLine(
                $"[BANC] tournee : {entry.Key} a fait tomber le jeu au passage precedent — sautee " +
                "(la pile est dans le journal ; « neuf » pour la retenter)");
        }

        int total = 0;
        var crashed = new List<string>();

        foreach (string step in s_AiBenchTourSpec.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] parts = step.Trim().Split(':');

            if (parts.Length != 2 || !int.TryParse(parts[0], out int zone) || !int.TryParse(parts[1], out int sub))
            {
                Console.Error.WriteLine($"[BANC] etape illisible « {step} » — ignoree");
                continue;
            }

            total++;

            if (state.TryGetValue($"{zone}:{sub}", out string known))
            {
                if (known == "plante") crashed.Add($"{zone}:{sub}");
                continue;
            }

            s_BenchTour.Add((zone, sub));
        }

        Console.Error.WriteLine(
            $"[BANC] tournee : {s_BenchTour.Count} sous-zone(s) a visiter sur {total}, " +
            $"{s_BenchTourDwellMs / 1000} s chacune");

        if (crashed.Count != 0)
        {
            Console.Error.WriteLine($"[BANC] tournee : sautees pour plantage : {string.Join(" ", crashed)}");
        }

        if (s_BenchTour.Count == 0)
        {
            Console.Error.WriteLine(
                "[BANC] tournee deja faite en entier — effacer tournee-faite.txt pour la refaire");
            return;
        }

        // Une fermeture normale pendant une etape ne doit pas la faire passer pour un plantage. Un
        // plantage, lui, ne passe pas par ProcessExit : l'etape reste « en-cours ».
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            if (s_BenchTourOver || s_BenchTourIndex < 0 || s_BenchTourIndex >= s_BenchTour.Count) return;

            (int z, int s) = s_BenchTour[s_BenchTourIndex];
            BenchTourAppend($"{z}:{s} interrompue");
        };
    }

    /// <summary>
    /// Un pas de tournée. Rend <c>true</c> quand un transfert vient de partir : le reste du tour
    /// de banc n'a alors plus de sens, la sous-zone courante est sur le point d'être déchargée.
    /// </summary>
    private static bool TickBenchTour()
    {
        if (s_BenchTourOver) return false;

        int zone = s_Unknown_00566c84;
        int subArea = s_Unknown_00566c88;

        // Un chargement est en cours : on ne transfère jamais par-dessus.
        if (zone == 0xff || subArea == 0xff) return false;

        long now = Environment.TickCount64;

        if (s_BenchTourIndex < 0)
        {
            // Le depart : laisser la partie chargee s'installer avant le premier transfert.
            if (s_BenchFrame < BenchGraceFrames) return false;

            return BenchTourGoTo(0, now);
        }

        (int tz, int ts) = s_BenchTour[s_BenchTourIndex];

        if (zone == tz && subArea == ts)
        {
            if (s_BenchTourArrivedMs < 0) s_BenchTourArrivedMs = now;
            if (now - s_BenchTourArrivedMs < s_BenchTourDwellMs) return false;

            PrintBenchReport();
            BenchTourMarkDone(tz, ts);
            return BenchTourGoTo(s_BenchTourIndex + 1, now);
        }

        if (now - s_BenchTourIssuedMs > BenchTourGiveUpMs)
        {
            Console.Error.WriteLine(
                $"[BANC] tournee : zone {tz} sous-zone {ts} jamais atteinte (on est en {zone}:{subArea})" +
                " — etape abandonnee, elle sera retentee au prochain lancement");
            return BenchTourGoTo(s_BenchTourIndex + 1, now);
        }

        return false;
    }

    private static bool BenchTourGoTo(int index, long now)
    {
        s_BenchTourIndex = index;
        s_BenchTourArrivedMs = -1;

        if (index >= s_BenchTour.Count)
        {
            s_BenchTourOver = true;
            Console.Error.WriteLine(
                "[BANC] TOURNEE TERMINEE — fermer le jeu, puis tests\\update-GestionnaireEntite.bat");
            return false;
        }

        (int zone, int sub) = s_BenchTour[index];
        Console.Error.WriteLine($"[BANC] tournee : etape {index + 1}/{s_BenchTour.Count}");

        s_BenchTourIssuedMs = now;
        BenchTourAppend($"{zone}:{sub} en-cours");
        BenchWarp(zone, sub, 0);
        return true;
    }

    private static void BenchTourMarkDone(int zone, int subArea) => BenchTourAppend($"{zone}:{subArea}");

    /// <summary>Écrit tout de suite, comme les sondes : un jeu tué ne perd pas l'avancement.</summary>
    private static void BenchTourAppend(string line)
    {
        try
        {
            System.IO.File.AppendAllText(BenchTourDonePath, line + Environment.NewLine);
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[BANC] tournee-faite.txt non ecrit ({error.Message})");
        }
    }

    // =============================================================================================
    // La naissance
    // =============================================================================================

    /// <summary>
    /// Fait naître une entité par sous-type installé, autour du meneur.
    ///
    /// <para>
    /// <b>Le lien sous-type ↔ espèce vient de la donnée, pas d'une supposition.</b>
    /// <see cref="ResolveEntityBlocks"/> le dit : <c>espèce = *(répertoire + 0x4c) + index * 8</c>,
    /// et <c>espèce[1]</c> est le numéro de comportement — le même octet que
    /// <see cref="SpawnEntityFormation"/> recopie en <c>entité[5]</c>, et que la passe
    /// <see cref="TickEntityTable_005ecaa0"/> utilise pour indexer la table 0x0056ad48. Le banc
    /// parcourt donc la table d'espèces et retient la PREMIÈRE espèce de chaque sous-type.
    /// </para>
    /// <para>
    /// C'est aussi ce qui donne au bilan le nom de la bête : <c>espèce[4]</c> indexe la table de
    /// modèles (<c>+0x98</c>, pas de 0x60) dont les seize premiers octets sont le nom.
    /// </para>
    /// </summary>
    private static void SpawnBenchEntities(int zone, int subArea)
    {
        s_BenchRows.Clear();

        byte* dir = s_Unknown_005e97cc;
        if (dir == null)
        {
            Console.Error.WriteLine("[BANC] pas de repertoire de zone — rien a peupler");
            return;
        }

        byte* speciesTable = *(byte**)(dir + 0x4c);
        byte* modelTable = *(byte**)(dir + 0x98);

        if (speciesTable == null)
        {
            Console.Error.WriteLine($"[BANC] zone {zone} sous-zone {subArea} : aucune table d'especes");
            return;
        }

        if (s_PartySize_005e9020 == 0)
        {
            // SpawnEntityFormation tire le poursuivi par `rand() % taille du groupe`.
            Console.Error.WriteLine("[BANC] groupe vide — naissance impossible");
            return;
        }

        byte* speciesEnd = BenchRegionEnd(dir, speciesTable);

        byte* leader = Mem.Arena(0x005e93c0) + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c;
        int cx = *(int*)(leader + 0x1c) >> 0xc;
        int cy = *(int*)(leader + 0x20) >> 0xc;
        int cz = *(int*)(leader + 0x24) >> 0xc;

        // Une liste de formation fabriquee : jusqu'a 0x20 enregistrements de huit entiers, plus le
        // terminateur. Meme disposition que celle que TrySpawnRandomEncounter remplit.
        int* formation = stackalloc int[0x21 * 8];
        var seen = new HashSet<int>();
        int count = 0;
        int hostSpecies = -1;

        for (byte* sp = speciesTable; sp + 8 <= speciesEnd && count < 0x20; sp += 8)
        {
            int subType = sp[1];
            if (!seen.Add(subType)) continue;

            uint handler = Res.ResolveEntityHandler(0x0056ad48, subType);
            if (handler == 0x00401FA0) continue;          // souche a corps vide : rien a exercer

            int species = (int)(sp - speciesTable) / 8;
            int model = sp[4];

            if (hostSpecies < 0) hostSpecies = species;

            FillBenchFormationRecord(formation + count * 8, species, count, cx, cy, cz);

            s_BenchRows.Add(new BenchRow(
                subType, handler, species, model, BenchModelName(modelTable, model), false));

            count++;
        }

        formation[count * 8] = -1;

        Console.Error.WriteLine(
            $"[BANC] zone {zone} sous-zone {subArea} : {count} sous-type(s) a exercer");

        foreach (BenchRow row in s_BenchRows)
        {
            Console.Error.WriteLine(
                $"[BANC]   sous-type {row.SubType,3}  0x{row.Handler:X6}  espece {row.Species,3}" +
                $"  modele {row.Model,3}  « {row.Name} »{(row.Grafted ? "  [greffe]" : "")}");
        }

        List<int> orphans = BenchOrphanSubTypes(seen);

        if (orphans.Count != 0)
        {
            Console.Error.WriteLine(
                "[BANC] installes mais portes par aucune espece d'ici : "
                + string.Join(" ", orphans.ConvertAll(
                    s => $"{s}->0x{Res.ResolveEntityHandler(0x0056ad48, s):X6}")));
        }

        if (count == 0) return;

        int spawned = SpawnEntityFormation(dir, formation, 0, 0);

        Console.Error.WriteLine($"[BANC] {spawned} entite(s) engendree(s) sur {count} demandee(s)");

        if (s_AiBenchOrphans && orphans.Count != 0 && hostSpecies >= 0)
        {
            GraftBenchOrphans(dir, modelTable, orphans, hostSpecies, count, cx, cy, cz);
        }
    }

    /// <summary>
    /// Un enregistrement de formation fabriqué : l'espèce, une position sur un cercle autour du
    /// meneur, un cap tourné vers lui, et les deux mots de faces de bord laissés à <c>-1</c> —
    /// c'est cette valeur qui fait passer <see cref="SpawnEntityFormation"/> par
    /// <see cref="TryPlaceEntity"/>, donc qui pose la bête au sol.
    /// </summary>
    private static void FillBenchFormationRecord(int* record, int species, int rank, int cx, int cy, int cz)
    {
        // Seize directions autour du meneur, un second cercle au-dela de seize betes.
        int angle = rank * (0x1000 / 16) & 0xfff;
        int radius = 0x60 + rank / 16 * 0x40;

        int x = cx + ((int)Winsys3d.GetSinTableEntry(angle) * radius >> 0xc);
        int z = cz + (Winsys3d.GetCosTableEntry(angle) * radius >> 0xc);
        int facing = Winsys3d.Ordinal_457(cx - x, cz - z) & 0xffff;

        record[0] = species;
        record[1] = (cy & 0xffff) << 0x10 | (x & 0xffff);
        record[2] = z & 0xffff;
        record[3] = facing;
        record[4] = -1;
        record[5] = -1;

        // Les deux derniers mots viennent d'un VRAI placement de l'espece dans ce lieu : +0x18 est
        // recopie en +0x14/+0x16 de l'entite, et certaines betes y lisent ce qu'elles font naitre.
        // Le Durahan de 5:4 y porte 0x36 ; le banc lui donnait 0, et son second coup faisait
        // naitre le sous-type 0 — la graine de Balle de feu, sans lanceur en +0x74 (tournee du
        // 2026-09-25, LEVEE 0x47ADD0). Faute de placement, zero, comme avant.
        int* real = BenchRealPlacement(s_Unknown_005e97cc, species);
        record[6] = real != null ? real[6] : 0;
        record[7] = real != null ? real[7] : 0;
    }

    /// <summary>
    /// Le premier enregistrement de formation du lieu qui fait naitre cette espece : la table
    /// <c>repertoire + 0x34</c>, celle que <see cref="ScriptOpSpawnFormation"/> indexe, est une
    /// liste de pointeurs vers des listes d'enregistrements de huit entiers terminees par
    /// <c>-1</c>. Seul l'octet bas du premier mot designe l'espece (voir <c>Bestiaire.cs</c>).
    /// </summary>
    private static int* BenchRealPlacement(byte* dir, int species)
    {
        if (dir == null) return null;

        byte* table = *(byte**)(dir + 0x34);
        if (table == null) return null;

        byte* tableEnd = BenchRegionEnd(dir, table);

        for (byte* p = table; p + 4 <= tableEnd; p += 4)
        {
            int* list = *(int**)p;
            if ((nuint)list < 0x10000) continue;       // nul ou pas un pointeur : sans placement

            for (int r = 0; r < 64 && list[r * 8] != -1; r++)
            {
                if ((list[r * 8] & 0xff) == species) return list + r * 8;
            }
        }

        return null;
    }

    /// <summary>
    /// La greffe : une bête de l'espèce hôte par sous-type orphelin, dont on remplace ensuite
    /// l'octet <c>+5</c> — celui, et lui seul, que
    /// <see cref="TickEntityTable_005ecaa0"/> utilise pour choisir le gestionnaire. Les trois blocs
    /// de description restent ceux de l'hôte : <see cref="ResolveEntityBlocks"/> les tire de
    /// <c>+8</c> et <c>+0xc</c>, que la greffe ne touche pas.
    ///
    /// <para>
    /// Les nouvelles entités sont retrouvées en comparant les emplacements vivants avant et après
    /// l'appel : <see cref="AllocEntitySlot"/> ne rend pas les index, et l'ordre de sa liste libre
    /// n'est pas garanti. L'appariement sous-type ↔ emplacement est donc arbitraire — sans
    /// conséquence, puisque ce qui compte est que chaque sous-type tourne, pas lequel des corps le
    /// porte.
    /// </para>
    /// </summary>
    private static void GraftBenchOrphans(
        byte* dir, byte* modelTable, List<int> orphans, int hostSpecies, int rank,
        int cx, int cy, int cz)
    {
        uint before = BenchLiveSceneSlots();

        int* formation = stackalloc int[0x21 * 8];
        int count = 0;

        for (; count < orphans.Count && count < 0x20; count++)
        {
            FillBenchFormationRecord(formation + count * 8, hostSpecies, rank + count, cx, cy, cz);
        }

        formation[count * 8] = -1;

        SpawnEntityFormation(dir, formation, 0, 0);

        uint after = BenchLiveSceneSlots();
        int grafted = 0;

        for (int i = 0; i < 0x20 && grafted < orphans.Count; i++)
        {
            uint bit = 1u << i;
            if ((after & bit) == 0 || (before & bit) != 0) continue;

            byte* slot = Mem.Entity(0x005ecaa0) + i * 0x9c;
            int subType = orphans[grafted];
            slot[5] = (byte)subType;

            uint handler = Res.ResolveEntityHandler(0x0056ad48, subType);
            int model = BenchHostModel(dir, hostSpecies);

            s_BenchRows.Add(new BenchRow(
                subType, handler, hostSpecies, model,
                model >= 0 ? BenchModelName(modelTable, model) : "?", true));

            grafted++;
        }

        Console.Error.WriteLine(
            $"[BANC] greffe : {grafted} sous-type(s) orphelin(s) poses sur l'espece {hostSpecies}"
            + " — le modele n'est pas le leur, voir OPENFORESIA_AI_BENCH_ORPHANS");
    }

    /// <summary>Le modèle de l'espèce hôte, pour nommer les lignes greffées.</summary>
    private static int BenchHostModel(byte* dir, int species)
    {
        byte* speciesTable = *(byte**)(dir + 0x4c);
        return speciesTable != null ? speciesTable[species * 8 + 4] : -1;
    }

    /// <summary>Les emplacements vivants de la table d'entités de scène, en masque de 32 bits.</summary>
    private static uint BenchLiveSceneSlots()
    {
        uint mask = 0;
        byte* table = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++)
        {
            if ((*(uint*)(table + i * 0x9c) & 0x80000000) != 0) mask |= 1u << i;
        }

        return mask;
    }

    /// <summary>
    /// Les sous-types que la sous-zone <b>installe</b> mais qu'aucune de ses espèces ne porte. Ce
    /// n'est pas une anomalie — la table de 0x20 pointeurs est partagée par plusieurs sous-zones —
    /// mais c'est une information : ces comportements-là ne se réveilleront pas ici, et il faut
    /// aller ailleurs pour les exercer.
    /// </summary>
    private static List<int> BenchOrphanSubTypes(HashSet<int> carried)
    {
        var orphans = new List<int>();

        for (int subType = 0; subType < 0x20; subType++)
        {
            if (carried.Contains(subType)) continue;
            if (Res.ResolveEntityHandler(0x0056ad48, subType) == 0x00401FA0) continue;

            orphans.Add(subType);
        }

        return orphans;
    }

    /// <summary>
    /// Borne haute d'une table du répertoire : le plus petit des 60 pointeurs d'en-tête qui lui soit
    /// strictement supérieur. Les régions d'un blob se suivent sans trou, donc c'est la seule borne
    /// que la donnée fournit — compter jusqu'à un enregistrement nul se trompe dès que la table
    /// voisine commence par un zéro. Même raisonnement que <c>Bestiaire.RegionEnd</c>, transposé aux
    /// pointeurs remis en place du répertoire chargé.
    /// </summary>
    private static byte* BenchRegionEnd(byte* dir, byte* start)
    {
        byte* end = null;

        for (int i = 0; i < 0x3c; i++)
        {
            byte* p = *(byte**)(dir + 0x10 + i * 4);
            if (p <= start) continue;
            if (end == null || p < end) end = p;
        }

        // Aucune region au-dela : on se borne aux 0x20 especes qu'une table de sous-type peut
        // designer, plutot que de lire au hasard.
        return end != null ? end : start + 0x20 * 8;
    }

    /// <summary>Les seize premiers octets d'un bloc de modèle sont le nom de la créature — établi
    /// par le relevé du CD, voir <c>Documentation/Reference/Bestiaire/LISEZMOI.md</c>.</summary>
    private static string BenchModelName(byte* modelTable, int model)
    {
        if (modelTable == null) return "?";

        byte* block = modelTable + model * 0x60;
        var text = new System.Text.StringBuilder(16);

        for (int i = 0; i < 16 && block[i] != 0; i++)
        {
            text.Append(block[i] >= 0x20 && block[i] < 0x7f ? (char)block[i] : '.');
        }

        return text.Length != 0 ? text.ToString() : "?";
    }

    // =============================================================================================
    // Le relevé
    // =============================================================================================

    /// <summary>Ce gestionnaire a-t-il été relevé comme lacune par le filet (<see cref="s_AiStub"/>) ?
    /// La clé du filet porte la sous-zone, d'où le balayage — la table reste courte.</summary>
    private static bool BenchIsKnownGap(uint target)
    {
        foreach ((uint Target, int Zone, int SubArea) key in s_MissingHandlers.Keys)
        {
            if (key.Target == target) return true;
        }

        return false;
    }

    /// <summary>Compte un appel de gestionnaire. Sous le banc seulement.</summary>
    private static void BenchNoteCall(uint target)
    {
        s_BenchCalls.TryGetValue(target, out int calls);
        s_BenchCalls[target] = calls + 1;
    }

    /// <summary>
    /// Retient une levée sans la laisser tuer le run : c'est tout l'intérêt du banc, exercer vingt
    /// comportements d'un coup alors que le premier manquant aurait arrêté la partie.
    /// </summary>
    private static void BenchNoteThrow(uint target, Exception error)
    {
        s_BenchThrows.TryGetValue(target, out int thrown);
        s_BenchThrows[target] = thrown + 1;

        if (s_BenchFirstThrow.ContainsKey(target)) return;

        string message = error.Message;
        if (message.Length > 200) message = message[..200] + "…";

        s_BenchFirstThrow[target] = $"{error.GetType().Name}: {message}";

        Console.Error.WriteLine($"[BANC] LEVEE 0x{target:X6} — {s_BenchFirstThrow[target]}");
    }

    /// <summary>
    /// Le bilan périodique. La ligne courte suffit tant que rien de neuf n'est arrivé ; le tableau
    /// complet ne repart que lorsqu'un gestionnaire de plus s'est mis à lever, sinon un run long
    /// noierait le journal.
    /// </summary>
    private static void PrintBenchReport()
    {
        int frames = s_BenchFrame - s_BenchArrivedFrame;

        Console.Error.WriteLine(
            $"[BANC] image {frames} : {s_BenchRows.Count} sous-type(s) exerce(s), " +
            $"{s_BenchThrows.Count} gestionnaire(s) en levee");

        if (s_BenchThrows.Count == s_BenchReportedThrowCount) return;

        s_BenchReportedThrowCount = s_BenchThrows.Count;

        foreach (BenchRow row in s_BenchRows)
        {
            s_BenchCalls.TryGetValue(row.Handler, out int calls);
            s_BenchThrows.TryGetValue(row.Handler, out int thrown);

            // Le filet et le banc peuvent tourner ensemble, et alors la lacune ne LEVE plus : elle
            // est notee par NoteMissingHandler et le gestionnaire rend la main. Sans ce test, le
            // verdict dirait « sain » d'un gestionnaire qui n'existe pas.
            string verdict =
                thrown != 0 ? $"{thrown} levee(s)"
                : BenchIsKnownGap(row.Handler) ? "LACUNE (filet)"
                : calls != 0 ? "sain"
                : "jamais appele";

            Console.Error.WriteLine(
                $"[BANC]   sous-type {row.SubType,3}  0x{row.Handler:X6}  « {row.Name} »" +
                $"{(row.Grafted ? " [greffe]" : "")}  {calls} appel(s)  {verdict}");
        }
    }
}
