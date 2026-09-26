// La fenetre d'installation : quatre etapes — l'image du disque, le dossier, l'installation, la fin.
//
// Pensee pour un joueur (regle des outils pour joueurs, voir OpenForesia.Tools/Html/LISEZMOI.md) :
// des phrases courtes, aucun terme technique, et rien a l'ecran qui ne serve a ce moment-la.

using System.Diagnostics;
using OpenForesia.Installer.Editions;
using OpenForesia.Installer.Installation;
using OpenForesia.Installer.Iso;
using Inst = OpenForesia.Installer.Installation.Installation;

namespace OpenForesia.Installer.Interface;

internal sealed class FenetreInstallation : Form
{
    private enum Etape { Image, Dossier, Installation, Fin }

    private readonly SourceDuJeu _jeu;
    private readonly CatalogueEditions _catalogue = CatalogueEditions.Embarque();

    private ImageIso? _image;
    private Edition? _edition;
    private CancellationTokenSource? _annulation;
    private Etape _etape;
    private bool _fermetureAutorisee;

    // --- les controles --------------------------------------------------------------------------
    private readonly Label _titre = new() { AutoSize = false, Font = new Font("Segoe UI", 14f), Location = new(24, 18), Size = new(512, 32) };
    private readonly Panel _pageImage = Page(), _pageDossier = Page(), _pageInstallation = Page(), _pageFin = Page();

    private readonly TextBox _cheminImage = new() { Location = new(0, 70), Size = new(400, 23), ReadOnly = true };
    private readonly Button _parcourirImage = new() { Text = "Parcourir…", Location = new(412, 69), Size = new(100, 26) };
    private readonly Label _etatImage = Texte("", 0, 108, 512, 60);

    private readonly TextBox _cheminDossier = new() { Location = new(0, 70), Size = new(400, 23) };
    private readonly Button _parcourirDossier = new() { Text = "Parcourir…", Location = new(412, 69), Size = new(100, 26) };
    private readonly Label _espace = Texte("", 0, 106, 512, 20);
    private readonly Label _etatDossier = Texte("", 0, 132, 512, 60);

    private readonly ProgressBar _barre = new() { Location = new(0, 60), Size = new(512, 22) };
    private readonly Label _etapeInstallation = Texte("", 0, 92, 512, 20);

    private readonly CheckBox _lancer = new() { Text = "Lancer OpenForesia", Location = new(0, 96), AutoSize = true, Checked = true };

    private readonly Button _precedent = new() { Text = "< Précédent", Size = new(100, 28) };
    private readonly Button _suivant = new() { Text = "Suivant >", Size = new(100, 28) };
    private readonly Button _annuler = new() { Text = "Annuler", Size = new(100, 28) };

    internal FenetreInstallation(SourceDuJeu jeu)
    {
        _jeu = jeu;

        Text = "Installation d'OpenForesia";
        AutoScaleDimensions = new SizeF(96f, 96f);
        AutoScaleMode = AutoScaleMode.Dpi;
        Font = new Font("Segoe UI", 9f);
        ClientSize = new Size(560, 330);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        var separateur = new Label { BorderStyle = BorderStyle.Fixed3D, Location = new(0, 272), Size = new(560, 2) };
        _precedent.Location = new(228, 288);
        _suivant.Location = new(336, 288);
        _annuler.Location = new(448, 288);
        Controls.AddRange([_titre, _pageImage, _pageDossier, _pageInstallation, _pageFin, separateur, _precedent, _suivant, _annuler]);

        _pageImage.Controls.AddRange([
            Texte("OpenForesia a besoin des données du jeu d'origine, Blaze & Blade (1998). " +
                  "Choisissez l'image ISO de votre disque.", 0, 0, 512, 44),
            _cheminImage, _parcourirImage, _etatImage,
        ]);
        _pageDossier.Controls.AddRange([
            Texte("OpenForesia sera installé dans ce dossier. Vos sauvegardes y seront aussi enregistrées.", 0, 0, 512, 44),
            _cheminDossier, _parcourirDossier, _espace, _etatDossier,
        ]);
        _pageInstallation.Controls.AddRange([
            Texte("Patientez pendant l'installation. Cela ne prend que quelques secondes.", 0, 0, 512, 44),
            _barre, _etapeInstallation,
        ]);
        _pageFin.Controls.AddRange([
            Texte("OpenForesia est installé. Un raccourci a été ajouté sur le bureau et dans le menu Démarrer.", 0, 0, 512, 44),
            _lancer,
        ]);

        _parcourirImage.Click += (_, _) => ChoisirImage();
        _parcourirDossier.Click += (_, _) => ChoisirDossier();
        _cheminDossier.TextChanged += (_, _) => VerifierDossier();
        _precedent.Click += (_, _) => Afficher(_etape - 1);
        _suivant.Click += (_, _) => Suivant();
        _annuler.Click += (_, _) => Annuler();
        FormClosing += Fermeture;

        _cheminDossier.Text = Inst.DossierExistant() ?? Inst.DossierParDefaut;
        Afficher(Etape.Image);
    }

    private static Panel Page() => new() { Location = new(24, 64), Size = new(512, 196), Visible = false };

    private static Label Texte(string texte, int x, int y, int largeur, int hauteur) =>
        new() { Text = texte, Location = new(x, y), Size = new(largeur, hauteur), AutoSize = false };

    // --- navigation -------------------------------------------------------------------------------

    private void Afficher(Etape etape)
    {
        _etape = etape;
        _pageImage.Visible = etape == Etape.Image;
        _pageDossier.Visible = etape == Etape.Dossier;
        _pageInstallation.Visible = etape == Etape.Installation;
        _pageFin.Visible = etape == Etape.Fin;

        _titre.Text = etape switch
        {
            Etape.Image => "Le disque du jeu",
            Etape.Dossier => "Où installer OpenForesia ?",
            Etape.Installation => "Installation en cours",
            _ => "C'est prêt",
        };

        _precedent.Visible = etape == Etape.Dossier;
        _suivant.Text = etape switch { Etape.Dossier => "Installer", Etape.Fin => "Terminer", _ => "Suivant >" };
        _suivant.Visible = etape != Etape.Installation;
        _annuler.Visible = etape != Etape.Fin;
        _suivant.Enabled = etape switch
        {
            Etape.Image => _edition != null,
            Etape.Dossier => VerifierDossier(),
            _ => true,
        };
        AcceptButton = _suivant;
    }

    private void Suivant()
    {
        switch (_etape)
        {
            case Etape.Image: Afficher(Etape.Dossier); break;
            case Etape.Dossier: _ = LancerInstallation(); break;
            case Etape.Fin: Terminer(); break;
        }
    }

    // --- etape 1 : l'image ------------------------------------------------------------------------

    private async void ChoisirImage()
    {
        using var choix = new OpenFileDialog
        {
            Title = "Choisir l'image du disque",
            Filter = "Image de disque (*.iso)|*.iso|Tous les fichiers (*.*)|*.*",
        };
        if (choix.ShowDialog(this) != DialogResult.OK) return;

        _image?.Dispose();
        _image = null;
        _edition = null;
        _cheminImage.Text = choix.FileName;
        Etat(_etatImage, "Vérification de l'image…", SystemColors.ControlText);
        _parcourirImage.Enabled = _suivant.Enabled = false;

        try
        {
            (_image, _edition) = await Task.Run(() =>
            {
                ImageIso image = ImageIso.Ouvrir(choix.FileName);
                try { return (image, _catalogue.Reconnaitre(image)); }
                catch { image.Dispose(); throw; }
            });
            Etat(_etatImage, $"✓  {_edition.Nom} — image reconnue.", Color.DarkGreen);
        }
        catch (ImageIsoInvalideException e)
        {
            Etat(_etatImage, e.Message, Color.Firebrick);
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            Etat(_etatImage, "Ce fichier ne peut pas être lu : " + e.Message, Color.Firebrick);
        }
        finally
        {
            _parcourirImage.Enabled = true;
            _suivant.Enabled = _etape == Etape.Image && _edition != null;
        }
    }

    private static void Etat(Label etiquette, string texte, Color couleur)
    {
        etiquette.Text = texte;
        etiquette.ForeColor = couleur;
    }

    // --- etape 2 : le dossier ---------------------------------------------------------------------

    private void ChoisirDossier()
    {
        using var choix = new FolderBrowserDialog { Description = "Choisir le dossier d'installation", UseDescriptionForTitle = true };
        if (choix.ShowDialog(this) != DialogResult.OK) return;

        // Comme tout installeur : on installe DANS un dossier OpenForesia du dossier choisi.
        string dossier = choix.SelectedPath;
        if (!string.Equals(Path.GetFileName(dossier.TrimEnd('\\')), Inst.NomJeu, StringComparison.OrdinalIgnoreCase))
            dossier = Path.Combine(dossier, Inst.NomJeu);
        _cheminDossier.Text = dossier;
    }

    private bool VerifierDossier()
    {
        if (_edition == null) return false;
        long requis = Inst.TailleRequise(_edition, _jeu);
        _espace.Text = $"Espace nécessaire : {Inst.Mo(requis)}";

        string? probleme = Inst.Verifier(_cheminDossier.Text.Trim(), requis, out _);
        Etat(_etatDossier, probleme ?? "", Color.Firebrick);
        bool valide = probleme == null;
        if (_etape == Etape.Dossier) _suivant.Enabled = valide;
        return valide;
    }

    private bool ConfirmerDossier(string dossier)
    {
        Inst.Verifier(dossier, 0, out bool protege);
        if (protege && Question(
                "Ce dossier est protégé par Windows : OpenForesia ne pourra pas y enregistrer vos " +
                "sauvegardes. Installer quand même ?") != DialogResult.Yes)
            return false;

        bool installationExistante = File.Exists(Path.Combine(dossier, Inst.Executable));
        bool nonVide = Directory.Exists(dossier) && Directory.EnumerateFileSystemEntries(dossier).Any();
        if (nonVide && !installationExistante && Question(
                "Ce dossier contient déjà d'autres fichiers. Installer OpenForesia dedans quand même ?") != DialogResult.Yes)
            return false;

        return true;
    }

    private DialogResult Question(string texte) =>
        MessageBox.Show(this, texte, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

    // --- etape 3 : l'installation -----------------------------------------------------------------

    private async Task LancerInstallation()
    {
        string dossier = Path.GetFullPath(_cheminDossier.Text.Trim());
        if (_image == null || _edition == null || !VerifierDossier() || !ConfirmerDossier(dossier)) return;

        Afficher(Etape.Installation);
        _barre.Value = 0;
        _annulation = new CancellationTokenSource();
        var avancement = new Progress<Avancement>(a =>
        {
            _barre.Value = (int)(a.Fait * 100 / Math.Max(1, a.Total));
            _etapeInstallation.Text = a.Etape;
        });

        ImageIso image = _image;
        Edition edition = _edition;
        CancellationToken jeton = _annulation.Token;
        try
        {
            await Task.Run(() => Inst.Installer(image, edition, _jeu, dossier, avancement, jeton));
            _cheminInstalle = dossier;
            Afficher(Etape.Fin);
        }
        catch (OperationCanceledException)
        {
            MessageBox.Show(this, "L'installation a été annulée. Rien n'a été laissé sur le disque.", Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (_fermetureAutorisee) { Close(); return; }
            Afficher(Etape.Dossier);
        }
        catch (Exception e)
        {
            string raison = e is IOException or UnauthorizedAccessException
                ? "Un fichier n'a pas pu être écrit. Si OpenForesia est ouvert, fermez-le, puis réessayez."
                : e.Message;
            MessageBox.Show(this, "L'installation n'a pas abouti. Rien n'a été laissé sur le disque.\n\n" + raison,
                Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Afficher(Etape.Dossier);
        }
        finally
        {
            _annulation.Dispose();
            _annulation = null;
        }
    }

    private string? _cheminInstalle;

    // --- fin et annulation ------------------------------------------------------------------------

    private void Terminer()
    {
        if (_lancer.Checked && _cheminInstalle != null)
        {
            Process.Start(new ProcessStartInfo(Path.Combine(_cheminInstalle, Inst.Executable))
            {
                WorkingDirectory = _cheminInstalle,
                UseShellExecute = true,
            });
        }
        _fermetureAutorisee = true;
        Close();
    }

    private void Annuler()
    {
        if (_etape == Etape.Installation)
        {
            if (Question("Arrêter l'installation ?") == DialogResult.Yes) _annulation?.Cancel();
            return;
        }
        _fermetureAutorisee = true;
        Close();
    }

    private void Fermeture(object? sender, FormClosingEventArgs e)
    {
        // Pendant l'installation, la croix vaut « Annuler » : on attend le nettoyage avant de fermer.
        if (_etape == Etape.Installation && _annulation != null)
        {
            e.Cancel = true;
            if (Question("Arrêter l'installation ?") == DialogResult.Yes)
            {
                _fermetureAutorisee = true;
                _annulation.Cancel();
            }
            return;
        }
        _image?.Dispose();
    }
}
