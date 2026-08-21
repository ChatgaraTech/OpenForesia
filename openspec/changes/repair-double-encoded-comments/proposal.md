## Why

**868 lignes** de `OpenForesia/src/Blaze/` portent un **double encodage** : de l'UTF-8 relu en
cp1252 puis réencodé en UTF-8. `— ` devient `â€” `, `é` devient `Ã©`, `«` devient `Â«`.

Ce n'est pas cosmétique au sens où on pourrait l'ignorer indéfiniment : ces lignes sont des
**commentaires de reconstruction**, c'est-à-dire le produit principal du projet. Un commentaire qui
explique pourquoi une transcription est fidèle perd sa valeur quand il est illisible.

Ce qui rend la réparation faisable maintenant, et pas avant :

- **Le découpage de `Blaze.cs` est terminé** (`split-blaze-into-subsystems`). Mêler une réparation
  de contenu à un déplacement de 23 664 lignes aurait rendu les deux invérifiables : le déplacement
  se prouve par comparaison octet à octet, la réparation change justement des octets.
- **Le dégât est maintenant localisé par fichier**, ce qui permet de le traiter et de le vérifier
  par morceaux plutôt qu'en une passe sur un fichier de 23 680 lignes.

| Fichier | Lignes touchées |
|---|---:|
| `Screens/State00/Options.cs` | 230 |
| `Engine/Video.cs` | 144 |
| `Engine/Input.cs` | 74 |
| `Engine/UiText.cs` | 67 |
| `Engine/Resources.cs` | 57 |
| `Engine/Audio.cs` | 54 |
| `Screens/DebugParty.cs` | 42 |
| `Screens/State00/MainMenu.cs` | 39 |
| les 20 autres | 161 |

`src/Host/` est **propre** (0 ligne), et les en-têtes des 29 fichiers l'ont été au passage du
découpage.

## What Changes

- **Réparer les 868 lignes** : décoder la séquence mal encodée et réécrire le caractère d'origine.
  La transformation est déterministe et réversible — c'est un aller-retour cp1252 ↔ UTF-8, pas une
  devinette.
- **Vérifier par relecture, pas par comparaison d'octets.** Le déplacement se prouvait à l'octet ;
  ici les octets changent par construction. La preuve est ailleurs : le nombre de lignes doit être
  inchangé fichier par fichier, `git diff` ne doit toucher que des lignes contenant une séquence
  mojibake, et le rendu doit être identique à l'écran.
- **Ne rien réparer hors des commentaires et des journaux.** À confirmer avant d'écrire : la mesure
  faite au découpage disait « aucun texte visible en jeu », mais elle portait sur `Blaze.cs` entier.
  **La refaire par fichier** — une chaîne mojibake qui atteindrait l'affichage serait un changement
  de comportement, pas une réparation.

## Capabilities

### New Capabilities

Aucune. Réparation d'encodage dans des commentaires : aucun comportement observable ne change.

### Modified Capabilities

Aucune. `.openspec.yaml` porte `skip_specs: true`, comme les changements de reconstruction et comme
`split-blaze-into-subsystems`.

## Impact

**Touché** : 28 fichiers de `OpenForesia/src/Blaze/`, 868 lignes.

**Non touché** : `src/Host/` (déjà propre), `Blaze.c`, `Legacy/Binaries/Blaze.exe`, les `.c` des DLL, la
documentation.

**Risque principal — une chaîne visible en jeu prise pour un commentaire.** Le contrôle est de
lister, avant réparation, toutes les lignes mojibake qui ne sont **pas** des commentaires, et de les
traiter une par une. Au découpage, les 89 lignes « de code » examinées se sont révélées être des
commentaires de fin de ligne et des messages de diagnostic en anglais — mais l'échantillon n'était
pas exhaustif.

**Risque secondaire — réparer deux fois.** Une ligne déjà correcte passée une seconde fois dans la
conversion se casse. La détection doit être idempotente : ne transformer que ce qui correspond
exactement à une séquence de double encodage.

**Outillage** : lire et écrire en octets (`[IO.File]::ReadAllBytes` / `WriteAllBytes`), jamais
`Get-Content` / `Set-Content`, qui ont déjà aggravé ce problème une fois. Voir la règle en tête de
`tasks.md` du changement précédent.
