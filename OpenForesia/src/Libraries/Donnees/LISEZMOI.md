# `src/Libraries/Donnees/` — les deux polices de 1998

Ce dossier contient les deux seules données de 1998 qui restent en **binaire**. Tout le reste de ce
que le programme lisait dans les DLL d'aide est transcrit en C# lisible (voir plus bas).

| Fichier | Taille | Ce que c'est | Qui s'en sert |
|---|---:|---|---|
| `winsys3d.TIM-65.bin` | 16 Ko | la police 8 × 16 du texte à l'écran, une image TIM PlayStation (ressource « TIM » 0x65 de `winsys3d.dll`) | `FntLoad` (ordinal 710, `winsys3d/TexteEcran.cs`) |
| `winsys3d.KANJI-70.bin` | 166 Ko | la police kanji 16 × 16 (ressource « KANJI » 0x70) : un index par code Shift-JIS, puis les dessins | `Krom2RawAdd` (ordinal 57), pour chaque caractère Shift-JIS que la petite table de glyphes du jeu ne connaît pas — dont les deux phrases restées en japonais dans la version française (voir `Documentation/Etat/NOTE_DEVELOPPEUR.md`) |

Ils sont **embarqués dans l'assemblage** (`EmbeddedResource` dans `OpenForesia.csproj`) : aucun
fichier n'est livré à côté de l'exécutable. `winsys3d/Image.cs` les lit au chargement de l'image.

**Pourquoi pas en C#** : ce sont des images. En tableau d'octets, les kanji feraient ~10 600 lignes
de source ; en glyphes dessinés, ~85 000. Le binaire ne dit rien de moins.

## Le reste, en C# lisible — et GÉNÉRÉ

Les autres données de `winsys3d.dll` et `wsyskern.dll` sont des fichiers C# **générés** :

- `winsys3d/ConstantesRdata.cs` — ce que `.rdata` porte hors de la table d'exports ;
- `winsys3d/ValeursInitiales.cs` — les valeurs initiales de `.data` ;
- `winsys3d/Exports.cs` — les 32 exports de données ;
- `winsys3d/ModeleDialogue.cs` — le dialogue de choix du pilote, contrôle par contrôle ;
- `wsyskern/Curseur.cs` — le curseur de la fenêtre des films, dessiné (il est entièrement
  transparent : il cache la souris pendant les cinématiques).

## Régénérer

```
dotnet run --project OpenForesia.Tools -- extraire-donnees Legacy/Binaries OpenForesia/src/Libraries
```

L'extraction lit les DLL originales de `Legacy/Binaries/` (qui restent dans le dépôt, source de
vérité), réécrit les deux polices et les cinq fichiers C#, et **vérifie chacun octet par octet
contre la DLL** : une génération qui ne reproduirait pas exactement les données de 1998 s'arrête en
erreur. Ne pas éditer ces fichiers à la main.
