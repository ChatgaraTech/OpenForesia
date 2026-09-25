# Les outils HTML

Les outils du projet qu'on **ouvre dans un navigateur** : un fichier `.html` autonome, qui marche
d'un double-clic, sans serveur et sans rien installer. C'est le format voulu pour tout ce qui sert
à *jouer avec* le jeu plutôt qu'à le reconstruire.

## Ce qu'il y a ici

| Fichier | Rôle |
|---|---|
| `editeur-sauvegarde.html` | **L'éditeur de sauvegarde.** Généré, ne pas l'éditer à la main. |
| `editeur-sauvegarde.template.html` | Sa source : la page, avec le marqueur `/*__DATA__*/` à la place du catalogue. |
| `build-editeur-sauvegarde.ps1` | Injecte le catalogue d'objets et de sorts (`Documentation/Reference/Bestiaire/*.csv`) dans le modèle. |
| `check-editeur-sauvegarde.mjs` | Le contrôle : exécute la page sur les vraies sauvegardes de `OpenForesia\save`. |

Raccourci : `tests\editeur-sauvegarde.bat` ouvre la page.

## L'éditeur de sauvegarde

Il ouvre un ou plusieurs fichiers `Blaze-Saving…` et permet de modifier, pour chaque personnage :
- le nom, l'élément, le niveau, l'expérience, les PV et PM, l'or ;
- les sept caractéristiques, actuelles et de départ ;
- l'inventaire — ajouter depuis les 397 fiches françaises du CD, **avec leur vignette**, retirer,
  changer une quantité — et l'équipement ;
- les sorts connus (classes qui incantent) ou les herbes (Chasseur) — l'onglet n'apparaît pas pour
  les autres ;
- les destinations de la carte et les neuf cristaux, en cases à cocher.

Un onglet de **triches rapides** fait le reste d'un clic : soin complet, caractéristiques ou
niveau à une valeur, tous les sorts, toutes les destinations, tous les cristaux.

**L'interface s'adresse à un joueur** : aucun décalage, aucun octet, aucun renvoi technique à
l'écran — le contrôle échoue s'il en trouve un. Le détail vit dans `FormatSauvegarde.md` et dans
les commentaires du modèle. Les banques d'état et la vue hexadécimale ont été retirées pour cette
raison (2026-09-25) : leur contenu n'a pas de sens pour un joueur.

**Le catalogue.** Les 213 fiches japonaises jamais traduites de la zone 0 sont écartées au build.
Les vignettes viennent du CD (`Bestiaire/vignettes.csv`) : le catalogue d'objets de chaque
sous-zone porte, juste après chaque fiche, les `0x140` octets que le jeu recopie dans
l'inventaire — identiques octet pour octet à ceux d'une sauvegarde (vérifié sur la Petite potion).

La somme de contrôle et sa copie de `0x3f00` sont recalculées à chaque modification. Le format
est décrit dans `Documentation/Reference/FormatSauvegarde.md`.

**Enregistrer.** Avec « Ouvrir le dossier save » (Chrome ou Edge), le fichier est réécrit en place,
et l'original est d'abord copié dans `save\copies-editeur\`. Ce sous-dossier est voulu : un fichier
`Blaze-Saving…` posé à côté des autres serait pris par le jeu pour une sauvegarde de plus. Sans
accès au dossier (Firefox, ou fichiers ouverts un par un), « Enregistrer » télécharge le fichier,
à recopier dans `save\` sous le même nom. **Le jeu doit être fermé** : il réécrit la sauvegarde
avec ce qu'il a en mémoire.

**Ce que l'éditeur ne fait pas.** Il ne change pas la classe : un autre inventaire de départ, un
autre portrait et d'autres masques en dépendent. Il ne modifie pas le dépôt, qu'il affiche en
lecture seule. Il ne touche plus aux banques d'état ni au lieu de reprise.

## Régénérer, vérifier

```powershell
OpenForesia.Tools\Html\build-editeur-sauvegarde.ps1
node OpenForesia.Tools\Html\check-editeur-sauvegarde.mjs
```

`node` n'est pas sur le PATH de l'agent : `D:\Logiciels\nodejs` (voir `CLAUDE.md`). Le contrôle
accepte `--png <dossier>` pour écrire les icônes décodées en PNG. **Regarde-les** après avoir
touché au décodage : une palette fausse ne se voit pas dans une somme de contrôle.

## Les règles pour un nouvel outil HTML

1. **Un seul fichier, qui s'ouvre d'un double-clic.** Pas de serveur ; `fetch()` ne marche pas en
   `file://`. Les données viennent d'un modèle `*.template.html` et d'un script `build-*.ps1` qui
   remplace un marqueur `/*__DATA__*/`, le même schéma que `Documentation/Reference/Bestiaire/`.
2. **Séparer le noyau du DOM.** La logique qui touche aux octets vit entre deux marqueurs
   (`/*__CORE_BEGIN__*/` … `/*__CORE_END__*/`), sur des `Uint8Array`, pour qu'un `check-*.mjs` la
   fasse tourner sous `node` contre de vraies données.
3. **Le contrôle exécute la page**, il ne relit pas seulement les données. Il la rend dans un faux
   DOM et cherche « undefined » et « NaN » dans le texte produit — la leçon de
   `check-bestiaire-page.mjs`.
4. **Tout ce que la page affiche vient de `Documentation/`**, jamais d'une supposition. Un champ
   dont le rôle n'est pas établi est présenté comme tel.
5. **Un `.ps1` porte un BOM UTF-8**, et PowerShell 5.1 emballe les tableaux imbriqués dans des
   objets `{ value, Count }` quand il fait du JSON : les aplatir en chaînes.
