# La magie

Sources : `Documentation/Reference/Sorts.md` (le système), `Documentation/Etat/SortsAtteignables.md`
et `Documentation/Reference/Bestiaire/sorts.csv` (les 123 fiches, générées depuis le jeu).

## Qui lance

Quatre classes sur huit, une école chacune (**établi**) :

| école | classes | fiches | accessibles par la montée de niveau |
|---|---|---|---|
| 0 — destruction (élémentaire) | Sorcier, Elfe | 29 | 26 (l'Elfe : **20**, son niveau d'apprentissage est bridé) |
| 1 — soins et sacré | Prêtre | 24 | 22 |
| 2 — soutien et altérations | Fée | 20 | 16 |
| 3 — herboristerie | Chasseur (inatteignable) | 7 plantes | 0 — écrite puis débranchée |

Les noms d'école ne sont pas dans le jeu ; ceux de la colonne sont une **lecture** d'après les sorts.
Quatre autres familles de fiches sont vraisemblablement les sorts des monstres.

## Apprendre (établi)

**Uniquement à la montée de niveau**, au plus un sort par niveau.

1. Le **palier** dépend du niveau : 1-19 → 0, 20-49 → 1, 50-79 → 2, 80-109 → 3, 110+ → 4.
2. Le palier dit combien de sorts de l'école sont ouverts :

   | école | palier 0 | 1 | 2 | 3 | 4 |
   |---|---|---|---|---|---|
   | 0 | 5 | 10 | 15 | 20 | 26 |
   | 1 | 5 | 11 | 16 | 19 | 22 |
   | 2 | 3 | 7 | 9 | 12 | 16 |

3. Une **portée** est tirée sur l'**INT** (valeur de départ + valeur courante / 8 + un petit tirage).
4. On parcourt les sorts ouverts **dans l'ordre** et l'on apprend le **premier** non connu dont le
   coût d'apprentissage tient dans la portée. Un sort cher **bloque tout ce qui le suit** tant qu'il
   n'est pas franchi.

## Lancer (établi)

Deux façons, toutes deux en combat, sans menu :

- **le sort mémorisé** : tenir le bouton de classe ; le personnage incante pendant la durée du sort,
  et le sort part à la fin. Relâcher avant abandonne. Le sort mémorisé se choisit dans un anneau
  au-dessus du personnage ou dans le menu.
- **la combinaison** : tenir SELECT, taper une séquence de boutons de face (△ ✕ ○ □, parfois deux à
  la fois), relâcher. Le jeu cherche le sort de l'école dont la combinaison correspond **et** qui est
  connu ; sinon rien ne se passe. La durée d'incantation est alors celle du sort **mémorisé**, pas
  celle du sort lancé.

**Le coût en PM n'est prélevé qu'à la fin**, et s'il n'est pas couvert **rien ne sort**, sans message
ni son : le personnage a l'air de rater.

Un sort fait naître une **entité d'effet** (projectile, zone, aura) qui porte ensuite le coup magique
([03-Combat](03-Combat.md)) ou l'effet de soutien. **Un sort fait exactement la même chose partout**
dans le jeu.

## Les sorts

Combinaisons : **T** △, **X** ✕, **O** ○, **C** □ ; deux lettres collées = deux boutons à la fois.
Coût en PM. Ordre = ordre d'apprentissage.

**École 0 (29)** : Balle de feu 2 (X-T-X) · Boule de lumière 4 · Pieds mouillés 3 · Rocher 6 · Feu
d'enfer 10 · Wonder 8 · Ténèbres 10 · Onde de choc 8 · Flèches magiques 12 · Enchant 15 · Mur de
flammes 16 · Coup de jus 16 · Congèlo 16 · Atchoum 20 · Assaut magique 32 · Lumière 24 · Éclair 40 ·
Trouille 38 · Exorciste 40 · Durdur 38 · Boule de feu 45 · Ventilo 45 · Démon polaire 30 · Stalagtite
45 · Satanas 60 · Au sec 80 · Colère 96 · Météores 82 · Fusion 100 (douze coups).

**École 1 (24)** : Pardon 0 · Sang pur 12 · Renfort 8 · Gnoukoulé 8 · Sonde 2 · Bobos finis 42 ·
3ème œil 2 · Antidote 16 · Lourdes 16 · Antisort 16 · Marteau 16 · Sang d'ange 40 · Lapêche 22 ·
Prière 22 · Calgon 26 · Zombie 10 · Parole sainte 60 · Délivrance 40 · Micro 40 · Coup de pouce 86 ·
Remontant 62 · Miracle 39 · Résurrection 100 · Ange 108.

**École 2 (20)** : Dodo 2 · Gros sabots 2 · Vitesse 2 · Matéria feu / terre / air / eau 16 · Chant
sirènes 8 · Écrase 8 · Champ magique 12 · Coussin d'air 30 · Escargot 12 · Ralenti 40 · Turbo 40 ·
Superman 60 · Blindé 80 · Liquide / Flamme / Zéphe / Terre 50.

Les combinaisons complètes, les durées et les sous-types d'effet sont dans `sorts.csv`. Ce que fait
chaque sort en détail (dégâts, zone, effet) se lit dans `Screens/State03/Spells/` — un fichier par
école ; le résumer sort par sort est un travail **à faire**.

> **Pour un jeu neuf.** Deux idées fortes à garder : apprendre en montant de niveau selon une
> statistique, et lancer sans quitter l'action. Trois à revoir : les combinaisons de neuf à treize
> coups (personnalisables, ou remplacées par une roue de raccourcis — le portage actuel a essayé les
> raccourcis par personnage, LP-016) ; le sort qui « rate » en silence faute de PM ; la durée
> d'incantation prise sur le mauvais sort.
