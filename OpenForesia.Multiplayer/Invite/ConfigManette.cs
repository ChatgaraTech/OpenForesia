// La configuration de la manette de l'invite : quelle entree pour chaque bouton PlayStation,
// gardee d'une session a l'autre dans %APPDATA%\OpenForesia\manette.json (tache 1.7, design D7).

using System.Text.Json;
using System.Text.Json.Serialization;
using OpenForesia.Multiplayer.Commun;

namespace OpenForesia.Multiplayer.Invite;

internal sealed class ConfigManette
{
    /// <summary>Le nom du bouton (<see cref="Pad.Bouton.Nom"/>) → l'entrée qui le presse ; un bouton absent n'a pas d'entrée.</summary>
    public Dictionary<string, Entree> Liaisons { get; set; } = new();

    internal static string Chemin => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenForesia", "manette.json");

    internal static ConfigManette? Charger()
    {
        try
        {
            if (!File.Exists(Chemin)) return null;
            return JsonSerializer.Deserialize(File.ReadAllText(Chemin), ContexteJson.Default.ConfigManette);
        }
        catch (JsonException)
        {
            return null;                          // abîmée : on reconfigure
        }
    }

    internal void Enregistrer()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(Chemin)!);
        File.WriteAllText(Chemin, JsonSerializer.Serialize(this, ContexteJson.Default.ConfigManette));
    }

    /// <summary>Le mot de 16 bits (actif haut) que les entrées actives composent.</summary>
    internal ushort Mot(HashSet<Entree> actives)
    {
        ushort mot = 0;
        foreach (Pad.Bouton b in Pad.Boutons)
            if (Liaisons.TryGetValue(b.Nom, out Entree e) && actives.Contains(e)) mot |= b.Masque;
        return mot;
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ConfigManette))]
internal sealed partial class ContexteJson : JsonSerializerContext;
