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

| `guide-gestionnaires.html` | **Le guide des essais.** Généré, ne pas l'éditer à la main. |
| `guide-gestionnaires.template.html` | Sa source. |
| `guide-gestionnaires.analyse.mjs` | L'analyse : ce qui reste, où, et de quoi ça naît. `--controle` la rejoue sur ce qui a déjà tourné. |
| `build-guide-gestionnaires.mjs` | Injecte l'analyse dans le modèle. |
| `check-guide-gestionnaires.mjs` | Le contrôle : exécute la page, onglet par onglet et zone par zone. |

| `bestiairev2.html` | **Le compendium des objets.** Généré, ne pas l'éditer à la main. |
| `bestiairev2.template.html` | Sa source. |
| `build-bestiairev2.mjs` | Lit les CSV du bestiaire, déroule les scripts d'objet et injecte le tout dans le modèle. Calcule aussi les **conseils par classe** (vue « Conseils par classe ») : une **appréciation**, pas une donnée du jeu — les poids par rôle (`ADVICE_ROLE`), la valeur chiffrée à la main de chaque pouvoir (`POWER_VALUE`) le mot de chaque classe (`ADVICE_NOTE`) et ses builds — jusqu'à trois, `ADVICE_BUILDS`, chacun avec ses poids, ses points en plus et, au besoin, des pièces fixées à la main (`pin`) — sont en tête du bloc ; un build qui retombe sur les mêmes pièces qu'un autre n'est pas affiché ; seuls les objets lâchés par un monstre ou trouvés en coffre sont proposés. |
| `check-bestiairev2.mjs` | Le contrôle : exécute le noyau de la page sur chaque fiche, chaque filtre et chaque tri, plus sept valeurs connues. |

Raccourcis : `tests\editeur-sauvegarde.bat` ouvre l'éditeur, `tests\guide-essais.bat` refait le
guide depuis la dernière sonde et l'ouvre, `tests\bestiairev2.bat` refait le compendium et l'ouvre.

## Le compendium des objets

Les 397 fiches françaises du CD, une par contenu (la règle de l'éditeur de sauvegarde), chacune
avec sa vignette, sa description du jeu, et **ce qu'elle fait réellement** :

- ATT, DEF, MATT, MDEF, les sept caractéristiques, les huit éléments, les classes qui l'équipent ;
- les quatre valeurs cachées (indices 10, 11, 14, 15 de `+0x20`), nommées **force / résistance aux
  altérations I et II** : ce sont celles que lisent les deux jets de résistance du combat
  (`Bestiaire/LISEZMOI.md`) ;
- l'indice 7, s'il est non nul, est affiché « sans effet » (le jeu ne lit pas cette case) ;
- **à l'usage** et **pouvoir** : les deux scripts de la fiche (`+0x80`, `+0x82`) déroulés hors ligne
  par `build-bestiairev2.mjs`. La lecture est **étroite** : un soin de PV ou de PM chiffré (la
  banque de variables suivie en intervalles — la Petite potion donne 40 à 80, `rand() % 41 + 40`),
  un plein (« champ = maximum » **seul** ; après un ajout et une comparaison, c'est un plafond),
  une guérison ou une altération d'état (`+0x40..0x43`, `&=` / `|=`), un sort (opcode 14, nommé par
  `sorts.csv`), la consommation (opcode 0), « une chance sur N » (`rand() % N` aussitôt comparé à
  zéro). Tout le reste s'affiche « pas encore décrit » — jamais une phrase plausible ;
- le **pouvoir** dit aussi **quand** il joue (l'événement `+0x84`, table dans `Bestiaire/LISEZMOI.md`)
  et **sous quelles conditions** : une garde suivie d'un saut conditionnel ne vaut que pour la région
  qu'elle saute, donc « 1 chance sur N », une famille de monstres ou un coût en PM s'écrivent soit
  dans la condition générale, soit devant la seule ligne qu'ils gardent (Marteau de Thor, Faux de la
  mort). Les bornes (« si négatif, zéro », plafonds) ne sont pas des effets ; le vol de vie (dégâts
  infligés lus, divisés, bornés, rendus au porteur, inversé contre les morts-vivants) a son motif ;
- **où le trouver** : monstres (chance d'objet `modèle+0x22` / 10, part de l'objet à plage 56 puis
  100, rangs additionnés **une fois** par monstre, lieu et classe de butin — deux espèces de même nom
  répètent les mêmes rangs) et coffres (plage 100). Zone 0 écartée comme dans `bestiaire.html`.

**Les descriptions du jeu mentent parfois**, et la page montre les deux : la Balle de cristal se
dit « Régénération complète de l'EV » mais son script rend 200 à 319 PV, plafonnés au maximum.

Deux pièges déjà rencontrés : `lieux.csv` a attrapé deux répliques de dialogue comme noms de lieu
(filtrées par la forme du nom), et une fenêtre d'Edge sans tête ne descend pas sous ~500 px — pour
regarder le format téléphone, mettre la page dans un `<iframe>` de 390 px.

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

## Le guide des essais

Pour chaque gestionnaire d'entité jamais vu en jeu (`handlers-exerces.tsv`), il dit **où aller et
quoi faire**, en mots de joueur : le lieu, le chemin par le menu de débogage (QUEST / MAP), la
consigne (« Apparaît quand Goblin agit », « Apparaît quand Satanas est lancé ici », « Posé dans ce
lieu »), et un plan vu de dessus avec les coffres et les monstres pour repères. Trois onglets :

- **À aller chercher** — ceux dont on connaît la piste. La piste part d'une **graine** — ce qui est
  posé (`Bestiaire/decor.csv`), ce qu'une espèce porte (`especes.csv`), ce qui a déjà tourné dans ce
  lieu — et suit les **naissances** écrites dans le C# : `SpawnEffectEntity`,
  `SpawnCastEntity_0047AAE0` et `SpawnStatusEffectEntity_0047AC40` à sous-type littéral, ou passé
  par le paramètre d'une fonction d'aide. Chaque fonction remplit sa table d'entités, et un
  sous-type `>= 0xc0` y tombe dans la table de débordement **de la sous-zone** (`0x0056b0c8`,
  `b1c8`, `b2c8`) — le parent n'est donc résolu que là où il tourne.
- **Impossibles** — ce que le jeu d'origine ne peut pas faire tourner : un gestionnaire de décor
  qu'aucune liste `+0x30` ne pose (la table d'entités de décor `0x005fa5e0` n'est remplie que par
  `SpawnSubAreaEntities`), un comportement de `0x0056ad48` qu'aucune espèce ne porte.
- **Sans piste** — le reste : un sous-type calculé, un script de scène, un verbe de zone.

**La règle « impossible » est contrôlée, pas supposée.** `node guide-gestionnaires.analyse.mjs
--controle` la rejoue sur les 871 gestionnaires déjà vus, sans la graine « vu » : elle ne s'y
trompe qu'une fois (`0x004700C0`, 10:7 — une entité de la sous-zone précédente résolue une image
par la table suivante). Une troisième règle, « ne naît que de parents impossibles », a été
**retirée** : le contrôle l'a prise en défaut 87 fois.

**Deux pièges déjà rencontrés.** L'auberge (zone 0) n'a qu'un jeu de tables pour toutes ses pièces
(index 0), mais chaque pièce pose son propre décor : les lieux de la zone 0 sont les pièces de
`lieux.csv`, pas 0..15. Et un nom de créature de l'auberge est du japonais lu en Latin-1 : filtré
par la même règle que l'éditeur, sinon la consigne affichait du charabia.

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
   `file://`. Les données viennent d'un modèle `*.template.html` et d'un script `build-*` (`.ps1`, ou `.mjs`
   quand il doit lire le C# ou le binaire, comme le guide) qui
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
