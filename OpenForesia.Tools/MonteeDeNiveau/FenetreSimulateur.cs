namespace OpenForesia.Tools.MonteeDeNiveau;

/// <summary>
/// La fenêtre du simulateur : en haut à gauche l'écran de création — classe, sept lignes à
/// répartir, pool de points —, en dessous le niveau visé et la graine ; à droite le tracé et les
/// deux tableaux.
/// </summary>
internal sealed class FenetreSimulateur : Form
{
    private static readonly Color[] Couleurs =
    [
        Color.FromArgb(0xd6, 0x27, 0x28),   // 0
        Color.FromArgb(0x1f, 0x77, 0xb4),   // 1
        Color.FromArgb(0x17, 0xbe, 0xcf),   // 2 Volonté
        Color.FromArgb(0xff, 0x7f, 0x0e),   // 3
        Color.FromArgb(0x8c, 0x56, 0x4b),   // 4
        Color.FromArgb(0x94, 0x67, 0xbd),   // 5
        Color.FromArgb(0x2c, 0xa0, 0x2c),   // 6 Chance
        Color.FromArgb(0xe3, 0x77, 0xc2),   // PV
        Color.FromArgb(0x7f, 0x7f, 0x7f),   // PM
    ];

    private readonly ComboBox _classe = new();
    private readonly ComboBox _element = new();
    private readonly Label _infoClasse = new();
    private readonly Label _infoProgression = new();
    private readonly NumericUpDown[] _valeurs = new NumericUpDown[7];
    private readonly Label[] _plafonds = new Label[7];
    private readonly Label _pool = new();
    private readonly CheckBox _poolLibre = new();
    private readonly NumericUpDown _niveauVise = new();
    private readonly NumericUpDown _graine = new();
    private readonly Button _valider = new();

    private readonly GraphiqueProgression _graphique = new();
    private readonly CheckBox _montrerPlafonds = new();
    private readonly Label _titreResultat = new();
    private readonly DataGridView _detail = new();
    private readonly DataGridView _synthese = new();
    private readonly RadioButton _modeEcarts = new();
    private readonly RadioButton _modeCumule = new();

    private short[] _base = DonneesDeClasse.StatistiquesDeBase(0);
    private int _poolTotal;
    private bool _enMiseAJour;
    private Resultat? _resultat;

    internal FenetreSimulateur()
    {
        Text = "OpenForesia — simulateur de montée de niveau";
        MinimumSize = new Size(1020, 700);
        Size = new Size(1240, 860);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(248, 248, 250);
        Font = new Font("Segoe UI", 9f);

        Controls.Add(ConstruireResultats());
        Controls.Add(new Splitter { Dock = DockStyle.Left, Width = 4, BackColor = Color.FromArgb(228, 228, 232) });
        Controls.Add(ConstruireSaisie());

        ChangerDeClasse();
        LancerLesDes();
    }

    // -----------------------------------------------------------------------------------------
    // La saisie
    // -----------------------------------------------------------------------------------------

    private Control ConstruireSaisie()
    {
        var colonne = new Panel
        {
            Dock = DockStyle.Left,
            Width = 372,
            Padding = new Padding(12, 12, 12, 12),
            AutoScroll = true,
            BackColor = Color.FromArgb(248, 248, 250),
        };

        var lancer = new Button
        {
            Text = "Lancer les dés",
            Dock = DockStyle.Top,
            Height = 28,
            FlatStyle = FlatStyle.System,
        };
        lancer.Click += (_, _) => LancerLesDes();

        _poolLibre.Text = "Pool libre (ignorer les dés)";
        _poolLibre.Dock = DockStyle.Top;
        _poolLibre.Height = 24;
        _poolLibre.CheckedChanged += (_, _) => { RafraichirPool(); };

        _pool.Dock = DockStyle.Top;
        _pool.Height = 26;
        _pool.Font = new Font(Font, FontStyle.Bold);
        _pool.TextAlign = ContentAlignment.MiddleLeft;

        var remise = new Button
        {
            Text = "Tout remettre aux valeurs de classe",
            Dock = DockStyle.Top,
            Height = 26,
            FlatStyle = FlatStyle.System,
        };
        remise.Click += (_, _) => RemettreALaBase();

        var grille = ConstruireGrilleDeStats();

        _infoClasse.Dock = DockStyle.Top;
        _infoClasse.Height = 20;
        _infoClasse.ForeColor = Color.FromArgb(90, 90, 98);

        // L'element se CHOISIT dans le jeu (FUN_00508e00 : un selecteur gauche/droite masque & 7),
        // il ne decoule pas de la classe. Il decide seul de l'ajustement des plafonds.
        _element.DropDownStyle = ComboBoxStyle.DropDownList;
        _element.Dock = DockStyle.Top;
        _element.Items.AddRange([.. Enumerable.Range(0, 8)
            .Select(e => (object)DonneesDeClasse.LibelleElement(e))]);
        _element.SelectedIndex = 0;
        _element.SelectedIndexChanged += (_, _) => RafraichirPool();

        _classe.DropDownStyle = ComboBoxStyle.DropDownList;
        _classe.Dock = DockStyle.Top;
        _classe.Items.AddRange([.. DonneesDeClasse.Noms.Select((n, i) => (object)$"{i} — {n}")]);
        _classe.SelectedIndex = 0;
        _classe.SelectedIndexChanged += (_, _) => { ChangerDeClasse(); LancerLesDes(); };

        var personnage = new GroupBox
        {
            Text = " 1 · Le personnage ",
            Dock = DockStyle.Top,
            // Large d'avance : un enfant en Dock=Top que le conteneur ne peut pas loger se fait
            // rogner par la mise en page, et AjusterHauteur relirait alors une hauteur fausse.
            Height = 800,
            Padding = new Padding(10, 6, 10, 10),
        };
        // Ajoutés à l'envers : un Dock=Top empile du dernier ajouté vers le premier.
        personnage.Controls.Add(remise);
        personnage.Controls.Add(_poolLibre);
        personnage.Controls.Add(lancer);
        personnage.Controls.Add(_pool);
        personnage.Controls.Add(grille);
        personnage.Controls.Add(_infoClasse);
        personnage.Controls.Add(_element);
        personnage.Controls.Add(_classe);
        AjusterHauteur(personnage);

        var simulation = ConstruireBlocSimulation();

        _infoProgression.Dock = DockStyle.Top;
        _infoProgression.Height = 190;
        _infoProgression.Padding = new Padding(2, 8, 2, 0);
        _infoProgression.ForeColor = Color.FromArgb(90, 90, 98);

        colonne.Controls.Add(_infoProgression);
        colonne.Controls.Add(simulation);
        colonne.Controls.Add(personnage);
        return colonne;
    }

    private Control ConstruireGrilleDeStats()
    {
        var grille = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 172,
            ColumnCount = 4,
            RowCount = 8,
            Padding = new Padding(0, 4, 0, 4),
        };
        grille.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92));
        grille.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 68));
        grille.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78));
        grille.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        Label Entete(string texte) => new()
        {
            Text = texte,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = Color.FromArgb(130, 130, 140),
        };

        grille.Controls.Add(Entete(""), 0, 0);
        grille.Controls.Add(Entete("type"), 1, 0);
        grille.Controls.Add(Entete("valeur"), 2, 0);
        grille.Controls.Add(Entete("plafond"), 3, 0);

        for (var i = 0; i < 7; i++)
        {
            var rang = i;
            grille.RowStyles.Add(new RowStyle(SizeType.Absolute, 21));

            grille.Controls.Add(new Label
            {
                Text = $"{DonneesDeClasse.NomsCaracs[i]}  ({i})",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Couleurs[i],
                Font = new Font(Font, FontStyle.Bold),
            }, 0, i + 1);

            grille.Controls.Add(new Label
            {
                Text = DonneesDeClasse.TypeCarac(i),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(130, 130, 140),
            }, 1, i + 1);

            _valeurs[i] = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 99,
                TextAlign = HorizontalAlignment.Right,
            };
            _valeurs[i].ValueChanged += (_, _) => ValeurModifiee(rang);
            grille.Controls.Add(_valeurs[i], 2, i + 1);

            _plafonds[i] = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(110, 110, 120),
            };
            grille.Controls.Add(_plafonds[i], 3, i + 1);
        }

        return grille;
    }

    private Control ConstruireBlocSimulation()
    {
        _valider.Text = "Valider et simuler";
        _valider.Dock = DockStyle.Top;
        _valider.Height = 34;
        _valider.FlatStyle = FlatStyle.System;
        _valider.Font = new Font(Font, FontStyle.Bold);
        _valider.Click += (_, _) => Simuler();

        var ligneGraine = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 28,
            ColumnCount = 3,
            RowCount = 1,
        };
        ligneGraine.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        ligneGraine.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
        ligneGraine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        _graine.Minimum = 0;
        _graine.Maximum = uint.MaxValue;
        _graine.Value = 1;
        _graine.Dock = DockStyle.Fill;

        var hasard = new Button { Text = "au hasard", Dock = DockStyle.Fill, FlatStyle = FlatStyle.System };
        hasard.Click += (_, _) => _graine.Value = Random.Shared.Next(1, int.MaxValue);

        ligneGraine.Controls.Add(new Label
        {
            Text = "Graine (srand)",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
        }, 0, 0);
        ligneGraine.Controls.Add(_graine, 1, 0);
        ligneGraine.Controls.Add(hasard, 2, 0);

        var ligneNiveau = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 28,
            ColumnCount = 2,
            RowCount = 1,
        };
        ligneNiveau.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        ligneNiveau.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));

        _niveauVise.Minimum = 2;
        _niveauVise.Maximum = 9999;
        _niveauVise.Value = 100;
        _niveauVise.Dock = DockStyle.Fill;
        _niveauVise.TextAlign = HorizontalAlignment.Right;

        ligneNiveau.Controls.Add(new Label
        {
            Text = "Niveau visé",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
        }, 0, 0);
        ligneNiveau.Controls.Add(_niveauVise, 1, 0);

        var bloc = new GroupBox
        {
            Text = " 2 · La simulation ",
            Dock = DockStyle.Top,
            Height = 800,   // voir le commentaire du cadre 1
            Padding = new Padding(10, 8, 10, 10),
        };
        bloc.Controls.Add(_valider);
        bloc.Controls.Add(ligneGraine);
        bloc.Controls.Add(ligneNiveau);
        AjusterHauteur(bloc);
        return bloc;
    }

    /// <summary>
    /// Donne au cadre la hauteur exacte de ce qu'il contient : la somme des enfants empilés en
    /// <c>Dock=Top</c>, plus le decor du <c>GroupBox</c> (titre, bordure, marge interieure), que
    /// <c>Height - DisplayRectangle.Height</c> mesure sans avoir a le deviner.
    ///
    /// <para>Ecrit apres coup : les deux cadres portaient une hauteur en dur, et celle du premier
    /// etait trop courte de trente pixels — le bouton du bas passait sous le cadre suivant. Une
    /// constante ne survit pas a l'ajout d'une ligne, ce calcul si.</para>
    /// </summary>
    private static void AjusterHauteur(GroupBox cadre)
    {
        var contenu = 0;
        foreach (Control enfant in cadre.Controls) contenu += enfant.Height;
        cadre.Height = cadre.Height - cadre.DisplayRectangle.Height + contenu;
    }

    // -----------------------------------------------------------------------------------------
    // Les résultats
    // -----------------------------------------------------------------------------------------

    private Control ConstruireResultats()
    {
        var separation = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 420,
            BackColor = Color.FromArgb(248, 248, 250),
        };

        _graphique.Dock = DockStyle.Fill;

        _montrerPlafonds.Text = "Afficher les plafonds (pointillés)";
        _montrerPlafonds.Checked = true;
        _montrerPlafonds.Dock = DockStyle.Right;
        _montrerPlafonds.Width = 210;
        _montrerPlafonds.CheckedChanged += (_, _) =>
        {
            _graphique.AfficherPlafonds = _montrerPlafonds.Checked;
            _graphique.Invalidate();
        };

        _titreResultat.Dock = DockStyle.Fill;
        _titreResultat.TextAlign = ContentAlignment.MiddleLeft;
        _titreResultat.Padding = new Padding(8, 0, 0, 0);
        _titreResultat.Font = new Font(Font, FontStyle.Bold);

        var barre = new Panel { Dock = DockStyle.Top, Height = 28 };
        barre.Controls.Add(_titreResultat);
        barre.Controls.Add(_montrerPlafonds);

        separation.Panel1.Controls.Add(_graphique);
        separation.Panel1.Controls.Add(barre);
        separation.Panel1.Padding = new Padding(6, 6, 6, 0);

        // --- le bas : les deux tableaux
        PreparerGrille(_detail);
        PreparerGrille(_synthese);

        _detail.VirtualMode = true;
        _detail.CellValueNeeded += DetailCellValueNeeded;

        _modeEcarts.Text = "Écarts par niveau";
        _modeEcarts.Checked = true;
        _modeEcarts.AutoSize = true;
        _modeEcarts.Location = new Point(10, 5);
        _modeEcarts.CheckedChanged += (_, _) => _detail.Invalidate();

        _modeCumule.Text = "Valeurs cumulées";
        _modeCumule.AutoSize = true;
        _modeCumule.Location = new Point(150, 5);

        var options = new Panel { Dock = DockStyle.Top, Height = 26 };
        options.Controls.Add(_modeEcarts);
        options.Controls.Add(_modeCumule);

        var pageDetail = new TabPage("Détail par niveau");
        pageDetail.Controls.Add(_detail);
        pageDetail.Controls.Add(options);

        var pageSynthese = new TabPage("Synthèse");
        pageSynthese.Controls.Add(_synthese);

        var onglets = new TabControl { Dock = DockStyle.Fill };
        onglets.TabPages.Add(pageDetail);
        onglets.TabPages.Add(pageSynthese);

        separation.Panel2.Controls.Add(onglets);
        separation.Panel2.Padding = new Padding(6, 6, 6, 6);

        return separation;
    }

    private static void PreparerGrille(DataGridView grille)
    {
        grille.Dock = DockStyle.Fill;
        grille.ReadOnly = true;
        grille.AllowUserToAddRows = false;
        grille.AllowUserToDeleteRows = false;
        grille.AllowUserToResizeRows = false;
        grille.RowHeadersVisible = false;
        grille.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grille.BackgroundColor = Color.White;
        grille.BorderStyle = BorderStyle.None;
        grille.EnableHeadersVisualStyles = false;
        grille.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 246);
        grille.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        grille.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grille.RowTemplate.Height = 20;
        grille.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252);
    }

    // -----------------------------------------------------------------------------------------
    // Le pool de points, tel que l'écran de création le distribue
    // -----------------------------------------------------------------------------------------

    private void ChangerDeClasse()
    {
        _enMiseAJour = true;
        _base = DonneesDeClasse.StatistiquesDeBase(_classe.SelectedIndex);

        for (var i = 0; i < 7; i++)
        {
            _valeurs[i].Minimum = _base[i];
            _valeurs[i].Value = _base[i];
        }

        var (pv, pm) = DonneesDeClasse.PvPmDeBase(_classe.SelectedIndex);
        _infoClasse.Text = $"« {DonneesDeClasse.NomsFeminins[_classe.SelectedIndex]} » au féminin · "
            + $"PV de base {pv} · PM de base {pm}";
        _enMiseAJour = false;

        RafraichirPool();
    }

    private void RemettreALaBase()
    {
        _enMiseAJour = true;
        for (var i = 0; i < 7; i++) _valeurs[i].Value = _base[i];
        _enMiseAJour = false;
        RafraichirPool();
    }

    /// <summary>Ghidra: la phase 0x39 du sous-état 5 de la création — le pool part de 15 et reçoit
    /// la somme de trois dés à six faces ; un triple offre un relancer dont la somme s'ajoute, et
    /// le total est plafonné à 99. Le tirage passe ici par <see cref="Random"/> et non par
    /// <c>CrtRand</c> : c'est une commodité de l'outil, pas une partie de la simulation — le
    /// résultat n'entre dans <c>FUN_005391f0</c> que par les valeurs qu'on choisit d'en dépenser.</summary>
    private void LancerLesDes()
    {
        var pool = 15;
        while (true)
        {
            int d1 = Random.Shared.Next(1, 7), d2 = Random.Shared.Next(1, 7), d3 = Random.Shared.Next(1, 7);
            pool += d1 + d2 + d3;

            if (d1 == d2 && d2 == d3 && pool < 99) continue;   // un triple : on relance
            if (pool > 99) pool = 99;
            break;
        }

        _poolTotal = pool;
        RemettreALaBase();
    }

    private int PointsDepenses()
    {
        var total = 0;
        for (var i = 0; i < 7; i++) total += (int)_valeurs[i].Value - _base[i];
        return total;
    }

    private void ValeurModifiee(int rang)
    {
        if (_enMiseAJour) return;

        if (!_poolLibre.Checked && PointsDepenses() > _poolTotal)
        {
            // Le pool est vide : l'écran d'origine refuse simplement le point.
            _enMiseAJour = true;
            _valeurs[rang].Value = Math.Max(_valeurs[rang].Minimum, _valeurs[rang].Value - 1);
            _enMiseAJour = false;
        }

        RafraichirPool();
    }

    private void RafraichirPool()
    {
        var depenses = PointsDepenses();
        _pool.Text = _poolLibre.Checked
            ? $"Points dépensés : {depenses}   (pool libre)"
            : $"Points à répartir : {_poolTotal - depenses} / {_poolTotal}";

        var valeurs = new short[7];
        for (var i = 0; i < 7; i++) valeurs[i] = (short)_valeurs[i].Value;

        var element = (byte)_element.SelectedIndex;

        var plafonds = new short[6];
        Simulateur.CalculerPlafonds(valeurs, element, plafonds);

        // Les mêmes plafonds sans élément : l'écart est ce que le choix a rapporté, et c'est la
        // seule chose chiffrée que l'élément apporte au personnage (voir MonteeDeNiveau.md,
        // « À quoi sert vraiment l'élément »).
        var sansElement = new short[6];
        Simulateur.CalculerPlafonds(valeurs, 0xff, sansElement);

        for (var i = 0; i < 6; i++)
        {
            var prime = plafonds[i] - sansElement[i];
            _plafonds[i].Text = prime == 0 ? plafonds[i].ToString() : $"{plafonds[i]}  (+{prime})";
            _plafonds[i].Font = new Font(Font, prime == 0 ? FontStyle.Regular : FontStyle.Bold);
            _plafonds[i].ForeColor = prime == 0 ? Color.FromArgb(110, 110, 120) : Couleurs[i];
        }
        _plafonds[6].Text = "0 à 200";

        var (phys, ment) = DonneesDeClasse.Probabilites(_classe.SelectedIndex, valeurs[6]);
        var gainMax = (valeurs[6] >> 3) + 1;

        var total = 0;
        for (var i = 0; i < 6; i++) total += plafonds[i] - sansElement[i];

        _infoProgression.Text =
            $"Avec une chance de départ de {valeurs[6]} :\r\n" +
            $"    physique (FO, AGI, CON) : {phys + 1} %\r\n" +
            $"    mental (INT, VO, PM) : {ment + 1} %\r\n\r\n" +
            $"Le gain vaut 1 + rand%(départ/8+1) + rand%{gainMax}.\r\n" +
            "Seuls les franchissements de multiple de 8 élargissent le tirage :\r\n" +
            "une valeur de départ sous 8 n'apporte rien à sa propre croissance.\r\n\r\n" +
            $"Élément {DonneesDeClasse.NomsElements[element]} : {total} points de plafond,\r\n" +
            "pour aucun point de pool. C'est son seul effet chiffré —\r\n" +
            "le reste est une teinte de fond et une couleur de nom.";
    }

    // -----------------------------------------------------------------------------------------
    // La simulation et son affichage
    // -----------------------------------------------------------------------------------------

    private void Simuler()
    {
        var valeurs = new short[7];
        for (var i = 0; i < 7; i++) valeurs[i] = (short)_valeurs[i].Value;

        var niveau = (int)_niveauVise.Value;
        Cursor = Cursors.WaitCursor;
        try
        {
            _resultat = Simulateur.Simuler(
                _classe.SelectedIndex, valeurs, (byte)_element.SelectedIndex, niveau, (uint)_graine.Value);
        }
        finally
        {
            Cursor = Cursors.Default;
        }

        AfficherResultat(_resultat);
    }

    private void AfficherResultat(Resultat resultat)
    {
        var etapes = resultat.Etapes;
        var series = new List<SerieCourbe>();

        for (var i = 0; i < 7; i++)
        {
            var carac = i;
            series.Add(new SerieCourbe
            {
                Nom = DonneesDeClasse.NomsCaracs[i],
                Couleur = Couleurs[i],
                Valeurs = [.. etapes.Select(e => (int)e.Caracs[carac])],
                Plafond = i < 6 ? resultat.Plafonds[i] : 0,
            });
        }

        series.Add(new SerieCourbe
        {
            Nom = "Points de vie",
            Couleur = Couleurs[7],
            Valeurs = [.. etapes.Select(e => (int)e.PvMax)],
        });
        series.Add(new SerieCourbe
        {
            Nom = "Points de magie",
            Couleur = Couleurs[8],
            Valeurs = [.. etapes.Select(e => (int)e.PmMax)],
        });

        _graphique.Charger(series);

        var (phys, ment) = DonneesDeClasse.Probabilites(resultat.Classe, resultat.Depart[6]);
        _titreResultat.Text =
            $"{DonneesDeClasse.Noms[resultat.Classe]} · niveau 1 → {etapes[^1].Niveau} · " +
            $"élément {DonneesDeClasse.NomsElements[resultat.Element]} " +
            $"({DonneesDeClasse.EffetDeLElement(resultat.Element)}) · " +
            $"CH de départ {resultat.Depart[6]} " +
            $"({phys + 1} % physique / {ment + 1} % mental) · graine {resultat.Graine}";

        RemplirDetail(resultat);
        RemplirSynthese(resultat);
    }

    private void RemplirDetail(Resultat resultat)
    {
        _detail.RowCount = 0;
        _detail.Columns.Clear();

        void Colonne(string nom, Color couleur, int largeur)
        {
            var colonne = new DataGridViewTextBoxColumn
            {
                HeaderText = nom,
                Width = largeur,
                SortMode = DataGridViewColumnSortMode.NotSortable,
            };
            colonne.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colonne.DefaultCellStyle.ForeColor = couleur;
            colonne.HeaderCell.Style.ForeColor = couleur;
            _detail.Columns.Add(colonne);
        }

        Colonne("Niv", Color.FromArgb(60, 60, 68), 52);
        for (var i = 0; i < 7; i++) Colonne(DonneesDeClasse.NomsCaracs[i], Couleurs[i], 74);
        Colonne("PV max", Couleurs[7], 66);
        Colonne("PM max", Couleurs[8], 66);

        _detail.RowCount = resultat.Etapes.Count;
        _detail.Invalidate();
    }

    private void DetailCellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
    {
        if (_resultat is null || e.RowIndex < 0 || e.RowIndex >= _resultat.Etapes.Count) return;

        var etape = _resultat.Etapes[e.RowIndex];

        if (e.ColumnIndex == 0)
        {
            e.Value = etape.Niveau;
            return;
        }

        var colonne = e.ColumnIndex - 1;
        var valeur = colonne < 7 ? etape.Caracs[colonne] : colonne == 7 ? etape.PvMax : etape.PmMax;

        if (_modeCumule.Checked || e.RowIndex == 0)
        {
            e.Value = valeur;
            return;
        }

        var avant = _resultat.Etapes[e.RowIndex - 1];
        var precedent = colonne < 7 ? avant.Caracs[colonne] : colonne == 7 ? avant.PvMax : avant.PmMax;
        var ecart = valeur - precedent;

        // Un point médian plutôt qu'un zéro : sur cent lignes, ce qui se lit doit être ce qui bouge.
        e.Value = ecart switch
        {
            0 => "·",
            > 0 => $"+{ecart}",
            _ => ecart.ToString(),
        };
    }

    private void RemplirSynthese(Resultat resultat)
    {
        _synthese.Rows.Clear();
        _synthese.Columns.Clear();

        _synthese.Columns.Add("serie", "Série");
        _synthese.Columns.Add("depart", "Départ");
        _synthese.Columns.Add("final", "Final");
        _synthese.Columns.Add("gagne", "Gagné");
        _synthese.Columns.Add("plafond", "Plafond");
        _synthese.Columns.Add("atteint", "Plafond atteint au niveau");
        _synthese.Columns.Add("montees", "Niveaux où elle a bougé");
        _synthese.Columns.Add("moyenne", "Points par niveau");

        for (var i = 1; i < _synthese.Columns.Count; i++)
        {
            _synthese.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        _synthese.Columns[0].Width = 148;
        _synthese.Columns[5].Width = 168;
        _synthese.Columns[6].Width = 158;
        _synthese.Columns[7].Width = 122;

        var etapes = resultat.Etapes;
        var montees = etapes.Count - 1;

        void Ligne(string nom, Color couleur, Func<Instantane, int> lire, int plafond)
        {
            var depart = lire(etapes[0]);
            var final = lire(etapes[^1]);

            var bouges = 0;
            var atteint = "—";
            for (var n = 1; n < etapes.Count; n++)
            {
                if (lire(etapes[n]) != lire(etapes[n - 1])) bouges++;
                if (atteint == "—" && plafond > 0 && lire(etapes[n]) >= plafond)
                {
                    atteint = etapes[n].Niveau.ToString();
                }
            }

            var index = _synthese.Rows.Add(
                nom, depart, final, final - depart,
                plafond > 0 ? plafond.ToString() : "—",
                plafond > 0 ? atteint : "—",
                montees > 0 ? $"{bouges} / {montees}" : "—",
                montees > 0 ? ((final - depart) / (double)montees).ToString("0.00") : "—");

            _synthese.Rows[index].Cells[0].Style.ForeColor = couleur;
            _synthese.Rows[index].Cells[0].Style.Font = new Font(Font, FontStyle.Bold);
        }

        for (var i = 0; i < 7; i++)
        {
            var carac = i;
            Ligne($"{DonneesDeClasse.NomsCaracs[i]} — {DonneesDeClasse.NomsLongs[i]}", Couleurs[i],
                e => e.Caracs[carac], i < 6 ? resultat.Plafonds[i] : 0);
        }

        Ligne("Points de vie", Couleurs[7], e => e.PvMax, 0);
        Ligne("Points de magie", Couleurs[8], e => e.PmMax, 0);
    }
}
