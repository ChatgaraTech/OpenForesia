namespace OpenForesia.Tools.MonteeDeNiveau;

using System.ComponentModel;
using System.Drawing.Drawing2D;

internal sealed class SerieCourbe
{
    internal required string Nom { get; init; }
    internal required Color Couleur { get; init; }
    internal required int[] Valeurs { get; init; }   // indexé par (niveau - 1)
    internal int Plafond { get; init; }              // 0 = aucun plafond à tracer
    internal bool Visible { get; set; } = true;
}

/// <summary>
/// Le tracé : une ligne par caractéristique sur l'axe des niveaux, plus le plafond de chacune en
/// pointillés. La légende de droite se clique pour masquer une courbe, et survoler le tracé
/// affiche les valeurs du niveau pointé.
/// </summary>
internal sealed class GraphiqueProgression : Control
{
    private const int MargeGauche = 62;
    private const int MargeHaut = 18;
    private const int MargeBas = 42;
    private const int LargeurLegende = 132;

    private List<SerieCourbe> _series = [];
    private int _niveauSurvole = -1;
    private readonly List<(Rectangle Zone, SerieCourbe Serie)> _boutonsLegende = [];

    /// <summary>Le concepteur visuel ne sert jamais cette classe (la fenêtre est écrite à la main),
    /// mais l'analyseur WinForms exige que toute propriété à setter dise ce qu'elle fait à la
    /// sérialisation.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool AfficherPlafonds { get; set; } = true;

    internal GraphiqueProgression()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        BackColor = Color.White;
        Cursor = Cursors.Cross;
    }

    internal void Charger(List<SerieCourbe> series)
    {
        _series = series;
        _niveauSurvole = -1;
        Invalidate();
    }

    private int NombreDeNiveaux => _series.Count == 0 ? 0 : _series[0].Valeurs.Length;

    private Rectangle ZoneTrace => new(
        MargeGauche, MargeHaut,
        Math.Max(10, Width - MargeGauche - LargeurLegende - 12),
        Math.Max(10, Height - MargeHaut - MargeBas));

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        g.Clear(BackColor);

        if (_series.Count == 0 || NombreDeNiveaux < 2)
        {
            TextRenderer.DrawText(g, "Choisis une classe, répartis les points, puis « Valider ».",
                Font, ClientRectangle, Color.Gray,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            return;
        }

        var zone = ZoneTrace;
        int niveaux = NombreDeNiveaux;

        // L'échelle verticale suit les seules courbes visibles, plafonds compris : masquer les PV
        // pour regarder les caractéristiques doit vraiment redonner de la place.
        int maxi = 1;
        foreach (var serie in _series)
        {
            if (!serie.Visible) continue;
            foreach (var v in serie.Valeurs) maxi = Math.Max(maxi, v);
            if (AfficherPlafonds) maxi = Math.Max(maxi, serie.Plafond);
        }

        int pas = ChoisirPas(maxi);
        int haut = (maxi / pas + 1) * pas;

        float X(int niveau) => zone.Left + (niveau - 1) * (float)zone.Width / Math.Max(1, niveaux - 1);
        float Y(int valeur) => zone.Bottom - valeur * (float)zone.Height / haut;

        using var stylo = new Pen(Color.FromArgb(232, 232, 236));
        using var styloAxe = new Pen(Color.FromArgb(140, 140, 148));
        using var pinceauTexte = new SolidBrush(Color.FromArgb(90, 90, 98));

        // --- la grille et l'axe des ordonnées
        for (var v = 0; v <= haut; v += pas)
        {
            float y = Y(v);
            g.DrawLine(stylo, zone.Left, y, zone.Right, y);
            TextRenderer.DrawText(g, v.ToString(), Font,
                new Rectangle(0, (int)y - 9, MargeGauche - 8, 18), pinceauTexte.Color,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        }

        // --- l'axe des niveaux
        int pasNiveau = ChoisirPasNiveau(niveaux);
        for (var n = 1; n <= niveaux; n += pasNiveau)
        {
            float x = X(n);
            g.DrawLine(stylo, x, zone.Top, x, zone.Bottom);
            TextRenderer.DrawText(g, n.ToString(), Font,
                new Rectangle((int)x - 30, zone.Bottom + 5, 60, 18), pinceauTexte.Color,
                TextFormatFlags.HorizontalCenter);
        }

        g.DrawLine(styloAxe, zone.Left, zone.Top, zone.Left, zone.Bottom);
        g.DrawLine(styloAxe, zone.Left, zone.Bottom, zone.Right, zone.Bottom);
        TextRenderer.DrawText(g, "niveau", Font,
            new Rectangle(zone.Left, zone.Bottom + 22, zone.Width, 18), pinceauTexte.Color,
            TextFormatFlags.HorizontalCenter);

        // --- les plafonds, sous les courbes
        if (AfficherPlafonds)
        {
            foreach (var serie in _series)
            {
                if (!serie.Visible || serie.Plafond <= 0 || serie.Plafond > haut) continue;
                using var pointille = new Pen(Color.FromArgb(70, serie.Couleur), 1f)
                {
                    DashStyle = DashStyle.Dash,
                };
                float y = Y(serie.Plafond);
                g.DrawLine(pointille, zone.Left, y, zone.Right, y);
            }
        }

        // --- les courbes
        foreach (var serie in _series)
        {
            if (!serie.Visible) continue;

            var points = new PointF[niveaux];
            for (var i = 0; i < niveaux; i++) points[i] = new PointF(X(i + 1), Y(serie.Valeurs[i]));

            using var trait = new Pen(serie.Couleur, 1.8f)
            {
                LineJoin = LineJoin.Round,
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
            };
            g.DrawLines(trait, points);
        }

        // --- le repère de survol
        if (_niveauSurvole >= 1 && _niveauSurvole <= niveaux)
        {
            float x = X(_niveauSurvole);
            using var repere = new Pen(Color.FromArgb(120, 40, 40, 48)) { DashStyle = DashStyle.Dot };
            g.DrawLine(repere, x, zone.Top, x, zone.Bottom);

            foreach (var serie in _series)
            {
                if (!serie.Visible) continue;
                float y = Y(serie.Valeurs[_niveauSurvole - 1]);
                using var point = new SolidBrush(serie.Couleur);
                g.FillEllipse(point, x - 3, y - 3, 6, 6);
                g.DrawEllipse(Pens.White, x - 3, y - 3, 6, 6);
            }

            DessinerInfobulle(g, zone, x);
        }

        DessinerLegende(g, zone);
    }

    private void DessinerInfobulle(Graphics g, Rectangle zone, float x)
    {
        var lignes = new List<(string Texte, Color Couleur)>
        {
            ($"Niveau {_niveauSurvole}", Color.FromArgb(40, 40, 48)),
        };

        foreach (var serie in _series)
        {
            if (!serie.Visible) continue;
            var valeur = serie.Valeurs[_niveauSurvole - 1];
            var ecart = _niveauSurvole > 1 ? valeur - serie.Valeurs[_niveauSurvole - 2] : 0;
            var suffixe = ecart == 0 ? "" : ecart > 0 ? $"  (+{ecart})" : $"  ({ecart})";
            lignes.Add(($"{serie.Nom} : {valeur}{suffixe}", serie.Couleur));
        }

        const int hauteurLigne = 16;
        var largeur = 0;
        foreach (var (texte, _) in lignes)
        {
            largeur = Math.Max(largeur, TextRenderer.MeasureText(texte, Font).Width);
        }

        var boite = new Rectangle(0, 0, largeur + 16, lignes.Count * hauteurLigne + 10);
        boite.X = (int)(x + 12 + boite.Width < zone.Right ? x + 12 : x - 12 - boite.Width);
        boite.Y = Math.Min(zone.Top + 8, zone.Bottom - boite.Height);

        using var fond = new SolidBrush(Color.FromArgb(244, 255, 255, 255));
        using var bord = new Pen(Color.FromArgb(200, 200, 208));
        g.FillRectangle(fond, boite);
        g.DrawRectangle(bord, boite);

        for (var i = 0; i < lignes.Count; i++)
        {
            TextRenderer.DrawText(g, lignes[i].Texte, Font,
                new Rectangle(boite.X + 8, boite.Y + 5 + i * hauteurLigne, boite.Width - 16, hauteurLigne),
                lignes[i].Couleur, TextFormatFlags.Left);
        }
    }

    private void DessinerLegende(Graphics g, Rectangle zone)
    {
        _boutonsLegende.Clear();

        int x = zone.Right + 12;
        int y = zone.Top;

        TextRenderer.DrawText(g, "Courbes", Font, new Rectangle(x, y, LargeurLegende, 18),
            Color.FromArgb(90, 90, 98), TextFormatFlags.Left);
        y += 22;

        foreach (var serie in _series)
        {
            var ligne = new Rectangle(x, y, LargeurLegende - 12, 20);
            _boutonsLegende.Add((ligne, serie));

            using var pastille = new SolidBrush(serie.Visible ? serie.Couleur : Color.FromArgb(210, 210, 216));
            g.FillRectangle(pastille, x, y + 6, 14, 8);

            var final = serie.Valeurs[^1];
            TextRenderer.DrawText(g, $"{serie.Nom}  {final}", Font,
                new Rectangle(x + 20, y, LargeurLegende - 32, 20),
                serie.Visible ? Color.FromArgb(40, 40, 48) : Color.FromArgb(170, 170, 178),
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            y += 20;
        }

        y += 8;
        TextRenderer.DrawText(g, "Clic : masquer", Font,
            new Rectangle(x, y, LargeurLegende, 18), Color.FromArgb(150, 150, 158), TextFormatFlags.Left);
    }

    /// <summary>Un pas de grille rond — 10, 20, 25, 50, 100… — pour que l'axe reste lisible quelle
    /// que soit l'amplitude.</summary>
    private static int ChoisirPas(int maxi)
    {
        int[] pas = [5, 10, 20, 25, 50, 100, 200, 250, 500, 1000, 2000, 2500];
        foreach (var p in pas)
        {
            if (maxi / p <= 10) return p;
        }
        return 5000;
    }

    private static int ChoisirPasNiveau(int niveaux)
    {
        int[] pas = [1, 2, 5, 10, 25, 50, 100, 250, 500, 1000];
        foreach (var p in pas)
        {
            if (niveaux / p <= 12) return p;
        }
        return 2000;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (_series.Count == 0 || NombreDeNiveaux < 2) return;

        var zone = ZoneTrace;
        int precedent = _niveauSurvole;

        if (e.X < zone.Left - 4 || e.X > zone.Right + 4 || e.Y < zone.Top || e.Y > zone.Bottom)
        {
            _niveauSurvole = -1;
        }
        else
        {
            var ratio = (e.X - zone.Left) / (float)zone.Width;
            _niveauSurvole = Math.Clamp(
                1 + (int)MathF.Round(ratio * (NombreDeNiveaux - 1)), 1, NombreDeNiveaux);
        }

        if (precedent != _niveauSurvole) Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (_niveauSurvole == -1) return;
        _niveauSurvole = -1;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        foreach (var (bouton, serie) in _boutonsLegende)
        {
            if (!bouton.Contains(e.Location)) continue;
            serie.Visible = !serie.Visible;
            Invalidate();
            return;
        }
    }
}
