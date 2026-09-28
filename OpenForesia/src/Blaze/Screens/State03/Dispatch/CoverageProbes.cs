// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES TROIS SONDES DE COUVERTURE — la question « celui-la, est-il seulement passe en jeu ? ».
//
// Un gestionnaire transcrit et compile n'est pas un gestionnaire eprouve. Beaucoup de corps de ce
// repertoire ont ete ecrits d'apres le desassembleur, sans qu'aucune partie ne les ait exerces —
// et rien ne distinguait les deux etats. Les sondes notent ce qui passe REELLEMENT, avec la zone
// et la sous-zone, et fusionnent le releve a la fermeture du jeu.
//
//   gestionnaires : (gestionnaire, zone, sous-zone)      -> Documentation/Etat/handlers-exerces.tsv
//   objets        : (cle de fiche, zone, sous-zone)      -> Documentation/Etat/objets-exerces.tsv
//   sorts         : (famille, indice, zone, sous-zone)   -> Documentation/Etat/sorts-exerces.tsv
//
// TROIS PROPRIETES COMMUNES, et elles comptent : la fusion est une UNION, donc deux parties
// differentes ne se marchent jamais dessus ; le fichier est suivi par git, donc il s'enrichit
// d'une partie a l'autre et ne perd jamais rien ; le diff reste lisible parce que tout est trie.
//
// La cle d'un objet est l'HEXA DE SES SEIZE PREMIERS OCTETS, pas son nom : les noms d'objet ne
// sont pas de l'ASCII. Pour les sorts, le LANCEUR n'est pas releve — une incantation, l'opcode 14
// d'un script d'objet et le mot +0x06 d'une entite de scene entrent par la meme fonction, et
// c'est la FICHE qu'on verifie.
//
// `OPENFORESIA_NO_PROBE=1` desarme les trois.
//
// Les trois recensements qui croisent ces .tsv avec les tables du binaire sont
// `OpenForesia.Tools/census-entity-handlers.sh`, `census-item-effects.sh` et `census-spells.sh` ;
// ils produisent GestionnairesEntites.md, ObjetsAtteignables.md et SortsAtteignables.md.
//
// Fichier recolle de DEUX plages (decision D7) : la sonde des gestionnaires vivait pres du
// carrefour, celles des objets et des sorts trente-huit mille lignes plus loin, pres de la
// machine a opcodes.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LA SONDE DE COUVERTURE (2026-08-30)
    //
    // Question qu'elle repond : « celui-la, est-il seulement passe en jeu ? »
    //
    // Un gestionnaire transcrit et compile n'est pas un gestionnaire eprouve. Les corps de ce
    // fichier sont ecrits d'apres le desassembleur, souvent sans qu'aucune partie ne les ait
    // exerces — et jusqu'ici rien ne distinguait les deux etats. La sonde note l'adresse de chaque
    // gestionnaire REELLEMENT appele, avec la zone et la sous-zone ou il l'a ete, et fusionne le
    // releve dans `Documentation/Etat/handlers-exerces.tsv` a la fermeture du jeu.
    //
    // Le fichier est suivi par git : il s'enrichit d'une partie a l'autre et se lit comme une
    // preuve accumulee. `OpenForesia.Tools/census-entity-handlers.sh` le croise avec les tables du
    // binaire pour dire, gestionnaire par gestionnaire, s'il est absent, ecrit, ou exerce.
    //
    // Une ligne par couple (gestionnaire, zone, sous-zone), triee : la fusion est une union, donc
    // deux parties differentes ne se marchent jamais dessus et le diff reste lisible.
    //
    // OPENFORESIA_NO_PROBE=1 la desarme.
    // =====================================================================

    // Les trois relevés vivent dans le dépôt, quatre crans au-dessus de l'exécutable. Un jeu
    // installé par OpenForesia-Setup n'a pas de dépôt au-dessus de lui : sans ce test, les sondes
    // écriraient dans le dossier parent du dossier d'installation. Le marqueur est la solution.
    // Et la version publiée (Release) n'en écrit jamais : ce sont des outils de mise au point.
    private static bool s_ProbeEnabled =
        Diagnostic.Actif &&
        Environment.GetEnvironmentVariable("OPENFORESIA_NO_PROBE") != "1" &&
        System.IO.File.Exists(System.IO.Path.Combine(
            AppContext.BaseDirectory, "..", "..", "..", "..", "OpenForesia.sln"));

    private static readonly SortedSet<string> s_HandlersSeen = new(StringComparer.Ordinal);

    private static SortedSet<string> s_ProbeAll;

    /// <summary>Le chemin du releve, deduit de l'emplacement de l'executable : quatre crans
    /// au-dessus de <c>bin/Debug/net10.0</c> ramenent a la racine du depot.</summary>
    private static string ProbePath => System.IO.Path.GetFullPath(System.IO.Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..",
        "Documentation", "Etat", "handlers-exerces.tsv"));

    /// <summary>
    /// Note qu'un gestionnaire a ete appele ici et maintenant. Appelee sur le chemin chaud — une
    /// fois par entite et par image —, donc le cas courant est un simple echec d'ajout dans un
    /// ensemble deja peuple.
    /// </summary>
    private static void NoteHandlerSeen(uint target)
    {
        if (!s_ProbeEnabled) return;

        string line = $"{target:X8}\t{Res.s_Unknown_005de528 & 0xff}\t{s_Unknown_005de52c}";

        // Deja vu cette partie : rien a faire, et c'est le cas de la quasi-totalite des appels.
        if (!s_HandlersSeen.Add(line)) return;

        LoadHandlerProbe();

        // Deja dans le fichier : la partie n'apprend rien, on n'ecrit pas.
        if (!s_ProbeAll.Add(line)) return;

        WriteHandlerProbe();

        Console.Error.WriteLine(
            $"[SONDE] nouveau : 0x{target:X8} zone={Res.s_Unknown_005de528 & 0xff} " +
            $"sous-zone={s_Unknown_005de52c} ({s_ProbeAll.Count} couples connus)");
    }

    /// <summary>Le fichier relu une fois par partie, dans l'ensemble qui sert de reference.</summary>
    private static void LoadHandlerProbe()
    {
        if (s_ProbeAll != null) return;

        s_ProbeAll = new SortedSet<string>(StringComparer.Ordinal);

        try
        {
            if (System.IO.File.Exists(ProbePath))
            {
                foreach (string line in System.IO.File.ReadAllLines(ProbePath))
                {
                    if (line.Length != 0 && line[0] != '#') s_ProbeAll.Add(line);
                }
            }
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[SONDE] fichier non relu : {error.Message}");
        }
    }

    /// <summary>
    /// Reecrit <c>Documentation/Etat/handlers-exerces.tsv</c> avec l'union courante.
    ///
    /// <para>
    /// <b>L'ecriture est immediate, pas differee a la fermeture.</b> La premiere version ne
    /// s'accrochait qu'a <c>ProcessExit</c>, et n'a rien ecrit de la premiere seance : une
    /// exception non rattrapee termine le processus <b>sans</b> lever cet evenement, et c'est
    /// precisement ce qui arrive quand on tombe sur un gestionnaire absent — le cas le plus
    /// interessant a relever. Ecrire des qu'un couple est nouveau supprime la question : un
    /// plantage, une fermeture par l'IDE ou un <c>TerminateProcess</c> laissent la preuve derriere
    /// eux.
    /// </para>
    /// <para>
    /// Le cout est nul en pratique : il n'y a de reecriture que pour un couple <b>jamais vu</b>,
    /// soit quelques dizaines de fois par seance et 1 430 fois au grand maximum sur la vie du
    /// projet.
    /// </para>
    /// </summary>
    private static void WriteHandlerProbe()
    {
        try
        {
            var text = new System.Text.StringBuilder();
            text.AppendLine("# Les gestionnaires d'entite REELLEMENT appeles en jeu, une ligne par");
            text.AppendLine("# couple (gestionnaire, zone, sous-zone). Ecrit par la sonde de");
            text.AppendLine("# Screens/State03/WorldMapTick.cs des qu'un couple est nouveau, en");
            text.AppendLine("# UNION avec ce que le fichier portait deja. Ne pas editer a la main :");
            text.AppendLine("# il se lit comme une preuve accumulee, pas comme une intention.");
            text.AppendLine("#");
            text.AppendLine("# gestionnaire\tzone\tsous-zone");
            foreach (string line in s_ProbeAll) text.AppendLine(line);

            System.IO.File.WriteAllText(ProbePath, text.ToString());
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[SONDE] releve non ecrit : {error.Message}");
            s_ProbeEnabled = false;   // inutile de reessayer a chaque image
        }
    }
    // =====================================================================
    // ECHAFAUDAGE — LA SONDE D'OBJETS (2026-08-30)
    //
    // Jumelle de NoteHandlerSeen, sur l'autre bout du meme chantier. Celle-la dit quel gestionnaire
    // d'entite a tourne ; celle-ci dit quel OBJET a ete employe. Les deux sont necessaires parce
    // qu'elles ne repondent pas a la meme question : un gestionnaire exerce prouve qu'un corps
    // tourne, un objet exerce prouve qu'un objet ENTIER se comporte bien de bout en bout.
    //
    // LA CLE EST L'HEXA DES SEIZE PREMIERS OCTETS DE LA FICHE, pas le nom lisible. La premiere
    // version relevait le nom court decode, et c'etait une faute : les noms d'objet ne sont pas de
    // l'ASCII — le jeu a son propre jeu de caracteres —, si bien que la moitie du releve sortait en
    // mojibake et que le tri n'avait plus de sens. L'hexa n'a aucun de ces problemes : il est
    // exact, stable, et `census-item-effects.sh` le recalcule a l'identique depuis la colonne
    // `octets` d'objets.csv pour retrouver le nom deja decode. La cle est technique, l'affichage
    // est lisible, et les deux ne se melangent pas.
    //
    // Releve dans Documentation/Etat/objets-exerces.tsv, meme forme et memes garanties que son
    // ainee : union avec l'existant, ecriture immediate, suivi par git. OPENFORESIA_NO_PROBE=1
    // desarme les deux.
    // =====================================================================

    private static readonly SortedSet<string> s_ItemsSeen = new(StringComparer.Ordinal);

    private static SortedSet<string> s_ItemProbeAll;

    private static string ItemProbePath => System.IO.Path.GetFullPath(System.IO.Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..",
        "Documentation", "Etat", "objets-exerces.tsv"));

    /// <summary>
    /// Note qu'un objet vient d'etre employe. Appelee sur les deux scripts — l'emploi et le
    /// declenchement — puisque tous deux passent par
    /// <see cref="RunItemScript_00523AA0"/>.
    /// </summary>
    private static void NoteItemUsed(byte* item)
    {
        if (!s_ProbeEnabled || item == null) return;

        string key = ItemKey(item);

        string line = $"{key}\t{Res.s_Unknown_005de528 & 0xff}\t{s_Unknown_005de52c}";

        if (!s_ItemsSeen.Add(line)) return;

        LoadItemProbe();
        if (!s_ItemProbeAll.Add(line)) return;

        WriteItemProbe();

        Console.Error.WriteLine(
            $"[SONDE OBJET] nouveau : {key} zone={Res.s_Unknown_005de528 & 0xff} " +
            $"sous-zone={s_Unknown_005de52c} ({s_ItemProbeAll.Count} couples connus)");
    }

    /// <summary>
    /// La <b>cle</b> d'une fiche d'objet : ses seize premiers octets en hexadecimal. C'est le champ
    /// du nom court, mais pris <b>brut</b> — voir l'en-tete de section pour pourquoi il ne faut pas
    /// le decoder ici.
    /// </summary>
    private static string ItemKey(byte* item)
    {
        var text = new System.Text.StringBuilder(0x20);
        for (int i = 0; i < 0x10; i++) text.Append(item[i].ToString("x2"));
        return text.ToString();
    }

    private static void LoadItemProbe()
    {
        if (s_ItemProbeAll != null) return;

        s_ItemProbeAll = new SortedSet<string>(StringComparer.Ordinal);

        try
        {
            if (System.IO.File.Exists(ItemProbePath))
            {
                foreach (string line in System.IO.File.ReadAllLines(ItemProbePath))
                {
                    if (line.Length != 0 && line[0] != '#') s_ItemProbeAll.Add(line);
                }
            }
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[SONDE OBJET] fichier non relu : {error.Message}");
        }
    }

    private static void WriteItemProbe()
    {
        try
        {
            var text = new System.Text.StringBuilder();
            text.AppendLine("# Les objets REELLEMENT employes en jeu, une ligne par couple");
            text.AppendLine("# (cle de fiche, zone, sous-zone). La CLE est l hexa des seize premiers");
            text.AppendLine("# octets de la fiche — le champ du nom court, pris brut : les noms ne sont");
            text.AppendLine("# pas de l ASCII. census-item-effects.sh la recalcule depuis objets.csv");
            text.AppendLine("# pour retrouver le nom lisible. Ecrit par la sonde de");
            text.AppendLine("# Screens/State03/WorldMapTick.cs des qu'un couple est nouveau, en");
            text.AppendLine("# UNION avec ce que le fichier portait deja. Ne pas editer a la main :");
            text.AppendLine("# il se lit comme une preuve accumulee, pas comme une intention.");
            text.AppendLine("#");
            text.AppendLine("# cle\tzone\tsous-zone");
            foreach (string line in s_ItemProbeAll) text.AppendLine(line);

            // UTF8Encoding(false) et non Encoding.UTF8 : celui-ci pose un BOM, et un BOM en tete
            // fait que la premiere ligne ne commence plus par '#'. Tout lecteur qui filtre les
            // commentaires par `^#` prend alors l'en-tete pour une donnee — c'est arrive.
            System.IO.File.WriteAllText(
                ItemProbePath, text.ToString(), new System.Text.UTF8Encoding(false));
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[SONDE OBJET] releve non ecrit : {error.Message}");
        }
    }

    // =====================================================================
    // ECHAFAUDAGE — LA SONDE DE SORTS (2026-08-31)
    //
    // La troisieme du meme chantier, apres NoteHandlerSeen (quel gestionnaire a tourne) et
    // NoteItemUsed (quel objet a ete employe). Celle-ci dit quelle FICHE DE SORT est partie.
    //
    // LA CLE EST LA PAIRE FAMILLE/INDICE, pas le nom : c'est exactement ce que
    // CastSpell_0053A980 recoit, et c'est ce qui adresse la fiche dans la table +0x9c du
    // repertoire d'etat. Les noms, eux, vivent sur le CD, et `census-spells.sh` les retrouve dans
    // Documentation/Reference/Bestiaire/sorts.csv a partir de la meme paire. La cle est technique,
    // l'affichage est lisible, et les deux ne se melangent pas — meme partage que pour les objets.
    //
    // ELLE NE DISTINGUE PAS LE LANCEUR, et c'est voulu. L'incantation d'un personnage, l'opcode 14
    // d'un script d'objet et le mot +0x06 d'une entite de scene entrent tous les trois par
    // CastSpell_0053A980 : ce qu'on veut savoir est si LA FICHE a ete exercee, pas par quel chemin.
    //
    // Releve dans Documentation/Etat/sorts-exerces.tsv, memes garanties que ses deux ainees :
    // union avec l'existant, ecriture immediate (une exception non rattrapee ne leve pas
    // ProcessExit, et c'est precisement le cas interessant), suivi par git. OPENFORESIA_NO_PROBE=1
    // desarme les trois.
    // =====================================================================

    private static readonly SortedSet<string> s_SpellsSeen = new(StringComparer.Ordinal);

    private static SortedSet<string> s_SpellProbeAll;

    private static string SpellProbePath => System.IO.Path.GetFullPath(System.IO.Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..",
        "Documentation", "Etat", "sorts-exerces.tsv"));

    /// <summary>
    /// Note qu'une fiche de sort vient d'etre declenchee, avec le lieu ou elle l'a ete.
    /// </summary>
    private static void NoteSpellCast(int family, int index)
    {
        if (!s_ProbeEnabled) return;

        string line = $"{family}\t{index}\t{Res.s_Unknown_005de528 & 0xff}\t{s_Unknown_005de52c}";

        if (!s_SpellsSeen.Add(line)) return;

        LoadSpellProbe();
        if (!s_SpellProbeAll.Add(line)) return;

        WriteSpellProbe();

        Console.Error.WriteLine(
            $"[SONDE SORT] nouveau : famille {family} indice {index} " +
            $"zone={Res.s_Unknown_005de528 & 0xff} sous-zone={s_Unknown_005de52c} " +
            $"({s_SpellProbeAll.Count} couples connus)");
    }

    private static void LoadSpellProbe()
    {
        if (s_SpellProbeAll != null) return;

        s_SpellProbeAll = new SortedSet<string>(StringComparer.Ordinal);

        try
        {
            if (System.IO.File.Exists(SpellProbePath))
            {
                foreach (string line in System.IO.File.ReadAllLines(SpellProbePath))
                {
                    if (line.Length != 0 && line[0] != '#') s_SpellProbeAll.Add(line);
                }
            }
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[SONDE SORT] fichier non relu : {error.Message}");
        }
    }

    private static void WriteSpellProbe()
    {
        try
        {
            var text = new System.Text.StringBuilder();
            text.AppendLine("# Les fiches de sort REELLEMENT declenchees en jeu, une ligne par");
            text.AppendLine("# couple (famille, indice, zone, sous-zone). La paire famille/indice est");
            text.AppendLine("# ce que CastSpell_0053A980 recoit ; census-spells.sh retrouve le nom");
            text.AppendLine("# lisible dans Bestiaire/sorts.csv a partir d'elle. Le lanceur n'est pas");
            text.AppendLine("# releve : une incantation, un script d'objet et une entite de scene");
            text.AppendLine("# entrent par la meme fonction, et c'est la FICHE qu'on verifie.");
            text.AppendLine("# Ecrit par la sonde de Screens/State03/WorldMapTick.cs des qu'un couple");
            text.AppendLine("# est nouveau, en UNION avec ce que le fichier portait deja. Ne pas");
            text.AppendLine("# editer a la main : il se lit comme une preuve accumulee, pas comme une");
            text.AppendLine("# intention.");
            text.AppendLine("#");
            text.AppendLine("# famille\tindice\tzone\tsous-zone");
            foreach (string line in s_SpellProbeAll) text.AppendLine(line);

            // UTF8Encoding(false) et non Encoding.UTF8 : celui-ci pose un BOM, et un BOM en tete
            // fait que la premiere ligne ne commence plus par '#'. Tout lecteur qui filtre les
            // commentaires par `^#` prend alors l'en-tete pour une donnee — c'est arrive.
            System.IO.File.WriteAllText(
                SpellProbePath, text.ToString(), new System.Text.UTF8Encoding(false));
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"[SONDE SORT] releve non ecrit : {error.Message}");
        }
    }
}
