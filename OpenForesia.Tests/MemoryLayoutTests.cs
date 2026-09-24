using static OpenForesia.Tests.MemoryLayout;

namespace OpenForesia.Tests;

/// <summary>
/// Les cinq assertions de disposition mémoire, plus leur garde-fou.
///
/// <para>
/// <b>Ces tests ne testent pas un comportement.</b> L'oracle de la logique transcrite reste l'écran,
/// via <c>playtest.ps1</c>. Ceux-ci vérifient la <i>carte mémoire</i>, qui est une propriété statique
/// et donc vérifiable sans lancer le jeu.
/// </para>
/// <para>
/// <b>Certains démarrent en échec, et c'est voulu.</b> Le défaut d'aliasing a été corrigé seize fois
/// dans ce dépôt, toujours en le voyant à l'écran, jamais avant. Ces assertions le prennent avant.
/// Elles échouent quand le dépôt porte des défauts réels — les corriger change le comportement, ce
/// qui relève d'un changement séparé. <b>Un test qui passerait tout de suite ne prouverait rien.</b>
/// </para>
/// <para>
/// <b>État au 2026-09-24 : quatre verts, deux rouges.</b> Le 2026-08-22, l'analyseur a été rendu
/// plus juste (déclarations sur plusieurs lignes, vues posées sur d'autres vues, nom déclaré
/// distingué d'une simple mention, surcharge <c>Alloc(compte, taille)</c>), ce qui a rendu visibles
/// des défauts qu'il ne voyait pas — aucun n'était nouveau dans le code. Il en reste deux :
/// </para>
/// <list type="bullet">
/// <item>test 3 : <c>s_UiLabelWidths_00589758</c> couvre 0x400 octets et déborde sur
/// <c>s_UiFrameInfo_00589840</c> — sa déclaration dit que le recouvrement est voulu, donc c'est le
/// modèle qu'il faut trancher, pas forcément le code ;</item>
/// <item>test 5 : <b>39</b> champs de valeur simple doublent une arène (42 au 2026-08-22). C'est le
/// manque nommé dans <c>REVERSE_STATUS.md</c> depuis le 2026-08-19.</item>
/// </list>
/// <para>
/// Le test 2 (<c>s_EntityStatBlocks_005ea560</c>, 0x2400 octets alloués dans <c>GameArena</c>),
/// rouge le 2026-08-22, est vert depuis.
/// </para>
/// <para>
/// Voir <c>REVERSE_STATUS.md</c>, section du 2026-08-19, et <c>ARCHITECTURE.md</c> pour la règle
/// d'état.
/// </para>
/// </summary>
public class MemoryLayoutTests
{
    // =======================================================================================
    // Test 1 - une vue d'arène désigne bien l'arène qui couvre son adresse.
    // =======================================================================================
    /// <summary>
    /// Un symbole déclaré <c>=> Arena(0x…)</c> doit avoir son adresse dans l'étendue de
    /// <c>GameArena</c>, et non dans celle d'un autre bloc. Un accesseur qui ne correspond pas à
    /// l'arène couvrante rend un pointeur dans la mauvaise allocation.
    /// </summary>
    [Fact]
    public void Une_vue_designe_l_arene_qui_couvre_son_adresse()
    {
        var accessorOf = new Dictionary<string, string>
        {
            ["GameArena"]      = "Arena",
            ["EntityArena"]    = "Entity",
            ["EntityParams"]   = "EntityParam",
            ["EntityState"]    = "EntityState",
            ["CameraBlock"]    = "Camera",
            ["ViewBlock"]      = "View",
            ["ItemMenuArena"]  = "ItemMenuState",
        };

        var wrong = Symbols
            .Where(s => s.Kind == SymbolKind.ArenaView)
            .Where(s =>
            {
                var arena = ArenaCovering(s.Address);
                if (arena is null) return true;                       // vue hors de toute arène
                if (!accessorOf.TryGetValue(arena.Name, out var expected)) return false;
                // ItemMenuBytes et ItemMenuState adressent la même arène
                if (s.ViaAccessor == "ItemMenuBytes" && expected == "ItemMenuState") return false;
                return s.ViaAccessor != expected;
            })
            .ToList();

        Assert.True(wrong.Count == 0,
            $"{wrong.Count} vue(s) d'arene passent par un accesseur qui ne correspond pas a " +
            $"l'arene couvrant leur adresse :{Environment.NewLine}{Describe(wrong)}");
    }

    // =======================================================================================
    // Test 2 - aucune allocation propre a une adresse deja couverte par une arene.
    // =======================================================================================
    /// <summary>
    /// C'est la forme la plus directe du défaut d'aliasing : un symbole qui s'alloue sa propre
    /// mémoire alors qu'une arène couvre déjà son adresse. L'arène et le symbole désignent alors
    /// deux blocs distincts, et une écriture par l'un n'est jamais lue par l'autre.
    ///
    /// <para>
    /// <b>ÉCHEC ATTENDU au 2026-08-19</b> sur quatre symboles constatés au découpage :
    /// <c>s_EntityList_005de558</c> (dans <c>ItemMenuArena</c>), et
    /// <c>s_ModelResultB_005f7b40</c>, <c>s_ModelResultA_005fa588</c>,
    /// <c>s_CameraMatrix_006008e0</c> (dans <c>EntityArena</c>). Les trois derniers sont dans le
    /// voisinage que <c>FUN_00533940</c> doit lire.
    /// </para>
    /// </summary>
    [Fact]
    public void Aucune_allocation_propre_dans_l_etendue_d_une_arene()
    {
        var arenaBases = Arenas.Select(a => a.Base).ToHashSet();

        var offenders = Symbols
            .Where(s => s.Kind == SymbolKind.OwnAllocation)
            .Where(s => !arenaBases.Contains(s.Address))      // l'arene elle-meme est legitime
            .Where(s => ArenaCovering(s.Address) is not null)
            .ToList();

        Assert.True(offenders.Count == 0,
            $"{offenders.Count} symbole(s) s'allouent leur propre memoire a une adresse qu'une " +
            $"arene couvre deja. L'arene et le symbole designent deux blocs distincts : une " +
            $"ecriture par l'un n'est jamais lue par l'autre.{Environment.NewLine}" +
            Describe(offenders.Select(s =>
                s with { Name = $"{s.Name} -> dans {ArenaCovering(s.Address)!.Name}" })));
    }

    // =======================================================================================
    // Test 3 - la taille declaree ne deborde pas sur le voisin d'adresse.
    // =======================================================================================
    /// <summary>
    /// Deux symboles voisins dont le premier est déclaré plus grand que l'écart qui les sépare se
    /// recouvrent. C'est ce qui avait fait déborder <c>s_PartyAbilityMask</c> (0x100 déclarés pour
    /// 0x60 disponibles) avant que l'arène ne soit modélisée.
    /// </summary>
    [Fact]
    public void Une_taille_declaree_ne_deborde_pas_sur_le_voisin()
    {
        // on ne compare qu'entre symboles a allocation propre, hors arenes : a l'interieur d'une
        // arene, le recouvrement est le sujet du test 2.
        var arenaBases = Arenas.Select(a => a.Base).ToHashSet();
        var standalone = Symbols
            .Where(s => s.Kind == SymbolKind.OwnAllocation && s.DeclaredSize > 0)
            .Where(s => !arenaBases.Contains(s.Address))
            .Where(s => ArenaCovering(s.Address) is null)
            .OrderBy(s => s.Address)
            .ToList();

        var overlaps = new List<string>();
        for (int i = 0; i < standalone.Count - 1; i++)
        {
            var a = standalone[i];
            var b = standalone[i + 1];
            if (a.Address == b.Address) continue;                 // c'est le sujet du test 4
            var gap = b.Address - a.Address;
            if (a.DeclaredSize > gap)
                overlaps.Add($"    {a} declare 0x{a.DeclaredSize:x} octets, " +
                             $"mais 0x{gap:x} seulement le separent de {b}");
        }

        Assert.True(overlaps.Count == 0,
            $"{overlaps.Count} symbole(s) declarent plus d'octets que l'ecart qui les separe de " +
            $"leur voisin d'adresse :{Environment.NewLine}{string.Join(Environment.NewLine, overlaps)}");
    }

    // =======================================================================================
    // Test 4 - deux symboles a la meme adresse partagent bien leur memoire.
    // =======================================================================================
    /// <summary>
    /// Deux noms pour une adresse est légitime — l'original nomme parfois deux fois le même bloc.
    /// Ce qui ne l'est pas, c'est que les deux <b>s'allouent chacun leur mémoire</b>.
    ///
    /// <para>
    /// <b>ÉCHEC ATTENDU au 2026-08-19</b> sur <c>0x00566c70</c> :
    /// <c>s_MenuRenderBlock_00566c70</c> alloue 0x40 octets côté menu principal et
    /// <c>s_ViewRect_00566c70</c> en alloue 0x10 côté carte du monde. Défaut <b>dormant</b> : il
    /// mordra quand la carte du monde affichera quelque chose, donc à traiter avant la reprise de
    /// <c>FUN_00533940</c>.
    /// </para>
    /// <para>
    /// Les deux autres adresses à double déclaration sont bénignes et doivent passer :
    /// <c>0x005e9180</c> (deux vues de la même arène) et <c>0x00562c9c</c> (deux constantes de même
    /// valeur, aucune mémoire en jeu).
    /// </para>
    /// </summary>
    [Fact]
    public void Deux_symboles_a_la_meme_adresse_partagent_leur_memoire()
    {
        var conflicts = Symbols
            .GroupBy(s => s.Address)
            .Where(g => g.Count(s => s.Kind == SymbolKind.OwnAllocation) > 1)
            .ToList();

        var report = conflicts.Select(g =>
            $"  0x{g.Key:x8} :{Environment.NewLine}" +
            Describe(g.Where(s => s.Kind == SymbolKind.OwnAllocation)));

        Assert.True(conflicts.Count == 0,
            $"{conflicts.Count} adresse(s) portent PLUSIEURS allocations independantes. Dans le " +
            $"binaire c'est une seule memoire ; ici c'en est deux, et une ecriture par un bout " +
            $"n'est jamais lue par l'autre.{Environment.NewLine}" +
            string.Join(Environment.NewLine, report));
    }

    // =======================================================================================
    // Test 5 - aucun champ de valeur simple a une adresse deja couverte par une arene.
    // =======================================================================================
    /// <summary>
    /// La forme <b>silencieuse</b> du défaut d'aliasing, et celle que les quatre tests ci-dessus
    /// ne voyaient pas : un <c>static int</c> ou <c>static byte</c> ordinaire dont l'adresse tombe
    /// dans l'étendue d'une arène. Il n'alloue rien, donc le test 2 l'ignore ; il ne déclare pas de
    /// taille, donc le test 3 l'ignore ; et tant qu'aucun site n'écrit l'arène à cette VA, rien ne
    /// se voit. Le premier qui le fera lira une autre mémoire.
    ///
    /// <para>
    /// Ce manque était nommé dans <c>REVERSE_STATUS.md</c> depuis le 2026-08-19 et a coûté trois
    /// vérifications manuelles : <c>s_PartySize_005e9020</c> (2026-08-21, vu de justesse en
    /// écrivant <c>FreezeAllEntities</c>), <c>s_Unknown_005de034</c> et le triplet
    /// <c>s_ScenePitch_005e9308</c> / <c>s_SceneYaw_005e930a</c> / <c>s_SceneRoll_005e930c</c>
    /// (2026-08-22, vus en transcrivant la boucle par image de <c>FUN_00516fc0</c>). Le dernier
    /// n'était <b>pas</b> latent : la carte du monde écrivait déjà l'arène pendant que
    /// <c>Engine/Model3d.cs</c> lisait les champs.
    /// </para>
    /// <para>
    /// <b>Une exception légitime, et une seule :</b> une propriété <c>=&gt;</c> qui LIT déjà
    /// l'arène est une vue, pas un doublon — mais l'analyseur la classe déjà en
    /// <see cref="MemoryLayout.SymbolKind.ArenaView"/>, donc elle n'arrive pas ici.
    /// </para>
    /// </summary>
    [Fact]
    public void Aucun_champ_simple_dans_l_etendue_d_une_arene()
    {
        var arenaBases = Arenas.Select(a => a.Base).ToHashSet();

        var offenders = Symbols
            .Where(s => s.Kind == SymbolKind.PlainField)
            .Where(s => !arenaBases.Contains(s.Address))
            .Where(s => ArenaCovering(s.Address) is not null)
            .OrderBy(s => s.Address)
            .ToList();

        Assert.True(offenders.Count == 0,
            $"{offenders.Count} champ(s) de valeur simple portent une adresse qu'une arene couvre " +
            $"deja. Rien ne les alloue et rien ne les relie a l'arene : une ecriture par un bout " +
            $"n'est jamais lue par l'autre, et c'est SILENCIEUX tant que les deux bouts ne sont " +
            $"pas exerces le meme jour.{Environment.NewLine}" +
            Describe(offenders.Select(s =>
                s with { Name = $"{s.Name} -> dans {ArenaCovering(s.Address)!.Name}" })));
    }

    // =======================================================================================
    // Garde-fou : l'analyseur voit-il encore quelque chose ?
    // =======================================================================================
    /// <summary>
    /// Si un refactor casse l'analyse des sources, les quatre tests ci-dessus passeraient sur un
    /// ensemble vide et ne prouveraient plus rien. Ce test-ci les protège.
    /// </summary>
    [Fact]
    public void L_analyseur_releve_bien_la_disposition()
    {
        Assert.True(Symbols.Count > 300,
            $"seulement {Symbols.Count} symboles releves : l'analyse des sources est cassee.");
        Assert.True(Arenas.Count >= 7,
            $"seulement {Arenas.Count} arenes relevees : l'analyse de GameMemory.cs est cassee.");
    }
}
