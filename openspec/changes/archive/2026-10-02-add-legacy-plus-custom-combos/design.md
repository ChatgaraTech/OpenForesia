## Context

Voir `proposal.md` pour le pourquoi, et `specs/gameplay-custom-combos/spec.md` pour le contrat.

Ce que le code fait aujourd'hui (relevé le 2026-10-02) :

- **Le mode combinaison** (`ApplyGroundMotion_0051A020`, `Spells/SpellCasting.cs`) : le bouton
  `0x0001` (SELECT, F1) ouvre l'incantation avec un compte à rebours nul ; tant qu'il est tenu,
  chaque appui de face est enregistré dans `entrée[0x0c + n]`, `n = entrée[0x0b]` (seize au plus),
  sous la forme `(Input.ComboButtons(mot) >> 8) & 0xf0` — le codage de 1998 (`0x10` △, `0x20` ✕,
  `0x40` ○, `0x80` □) depuis le complément de LB-058. Au relâchement, `PickCastTarget_0051A5D0`
  (`Entities/Targeting.cs`) balaye les fiches de l'école (`répertoire + 0x9c`, `0x30` octets
  chacune) et rend le **premier** indice dont la longueur (`fiche + 0x1f`) et les octets
  (`fiche + 0x20..`) égalent la saisie ; la posture vérifie ensuite que le sort est appris.
- **L'onglet magie** (`DrawSpellTab`, `WorldMapMenu.cs`) reçoit le personnage (`character`) et le
  mot de manette (`pad`) ; il construit la tranche affichée dans `s_SpellList_005de6a0` (un sort non
  appris y entre par son opposé, d'où le test `< 0x80`), déplace le curseur `s_Unknown_005de6f8`,
  mémorise au bouton de validation, et dessine la combinaison du sort visé en lisant
  `record[0x1f]` et `record[0x20 + i]`. **SELECT n'y est lu nulle part.**
- **La fiche de personnage** (`MainMem + 0xf0000 + i × 0x2000`, sauvegardée entière) a un trou :
  l'inventaire finit à `+0x1bff`, les banques d'état commencent à `+0x1d00`. Aucun décalage
  `0x1c..` dans `Blaze.c` ni dans la reconstruction (hors trois constantes sans rapport).
- **L'éditeur de sauvegarde** modifie les octets sur place : il préserve ce qu'il ne connaît pas.

## Goals / Non-Goals

**Goals :**
- Tout le tweak dans `src/LegacyPlus/CombinaisonsLibres.cs` ; `src/Blaze/` ne reçoit que des lignes
  de couture, identité par défaut.
- Aucune écriture dans les fiches de sort : les raccourcis sont **à côté**, par personnage.

**Non-Goals :**
- Pas d'aide à la saisie au-delà de l'affichage en direct (pas d'écran à part, pas de liste des
  raccourcis de tout le groupe).
- Pas de raccourci pour l'herboristerie du Chasseur (école 3) : elle n'a pas de mode combinaison.

## Decisions

### D1. Le rangement : `+0x1c00` de la fiche, cinq octets par sort

```
 +0x1c00 + indice × 5      indice = rang du sort dans l'école du personnage (0..28)
   [0]     longueur, 0..8 (0 = vide)
   [1..4]  huit coups, quatre bits chacun : le coup k dans le quartet bas (k pair) ou haut (k impair)
           de l'octet 1 + k/2 ; un coup = l'octet de saisie >> 4 (bits △ ✕ ○ □ = 1 2 4 8)
```

29 sorts × 5 = 145 octets, `+0x1c00..+0x1c90` ; le reste du trou reste à zéro. **Zéro partout =
tous vides** : une ancienne sauvegarde se lit sans conversion. Une entrée invalide (longueur > 8,
coup nul dans la longueur) est **lue comme vide** et réécrite vide à la prochaine saisie.

- *Plutôt qu'un fichier à côté* : le raccourci suit le personnage partout, y compris le personnage
  d'un ami dans les sauvegardes de l'hôte, et la somme de contrôle est recalculée par le jeu.
- *Huit coups et non seize* : seize demanderaient 9 octets × 29 = 261, plus que le trou.

### D2. Trois coutures dans le code de 1998

| couture | site | défaut |
|---|---|---|
| `CombinaisonDuSort(perso, indice, combinaison)` | la boucle de `PickCastTarget_0051A5D0`, sur le pointeur de combinaison de chaque fiche (longueur en `[-1]`) | rend le pointeur de la fiche |
| `CombinaisonAffichee(perso, indice, fiche)` | le dessin de la combinaison dans `DrawSpellTab` (`record[0x1f]`, `record[0x20 + i]`) | rend la fiche |
| `OngletSorts(perso, pad, indice, appris)` | `DrawSpellTab`, avant le déplacement du curseur | ne fait rien |

Les deux premières rendent un pointeur sur un **tampon du tweak** (mémoire native, allouée une
fois) au même format que la fiche — longueur en `+0x1f`, coups en `+0x20..` — rempli depuis la
fiche du personnage. Le code de 1998 lit alors exactement comme avant. Un raccourci vide a une
longueur nulle : il ne correspond à rien (`PickCastTarget` saute déjà une longueur différente) et
ne dessine rien.

*Alternative écartée* : réécrire les fiches de sort au chargement — elles sont partagées par école,
le Sorcier et l'Elfe auraient eu les mêmes raccourcis.

### D3. La saisie dans l'onglet

`OngletSorts` est appelée à chaque image de l'onglet avec le sort sous le curseur (`indice`, et s'il
est appris). Le tweak tient un état « saisie en cours » (personnage, indice, coups) :

- hors saisie : SELECT pressé sur un sort appris ouvre la saisie (son de validation du menu) ;
- en saisie : chaque **nouvel appui** de face est passé par `Input.ComboButtons` — le même codage
  que le mode combinaison, sans quoi ✕ et □ seraient échangés — puis ajouté (huit au plus) ;
  SELECT clôt : la suite est écrite dans la fiche du personnage (D1), et tout autre sort **du même
  personnage** qui avait la même suite est vidé (spec, règle de conflit) ;
- en saisie, **tous les appuis de l'image sont effacés** du mot de manette avant que l'onglet ne les
  lise : ni validation, ni retour, ni changement de page ou de curseur ;
- l'affichage en direct vient de `CombinaisonAffichee`, qui rend la saisie en cours pour le sort
  saisi ;
- la saisie est **abandonnée** (rien n'est écrit) si l'onglet n'est pas dessiné à l'image suivante
  — menu fermé, autre onglet —, ce que le tweak voit par un compteur d'images (ci-dessous).

*Établi à l'implémentation* (tâche 3.4) : `pad[1]` porte les **appuis de l'image** — validation
`0x8000`, retour `0x4000`, SELECT `0x0001`, directions `0x10`..`0x80` (`RunCharacterSheetPage` y lit
le changement de membre) — et `pad[3]` la **répétition** des directions, que `DrawSpellTab` lit pour
changer de page et déplacer le curseur, **avant** de construire la tranche. La couture `OngletSorts`
est donc appelée **en tête** de `DrawSpellTab`, et efface les deux mots pendant une saisie. Le sort
visé qu'elle reçoit est `s_SpellList_005de6a0[s_Unknown_005de6f8]` tel que l'image précédente l'a
construit (`-1` quand le curseur n'est pas dans la tranche) ; le tweak revérifie qu'il est appris
dans le masque `+0x160` du personnage. L'abandon se voit par un compteur que
`DessinSurImpression` avance à chaque image dessinée, gel du menu compris : `ImageSuivante` ne
tourne pas pendant le menu.

### D4. Le bestiaire

`Bestiaire.Buttons` (`OpenForesia.Tools`) rend `0x20` par `X` et `0x40` par `O` — les pictogrammes du
jeu, établis en jeu le 2026-10-02 (Balle de feu, `20 10 20`, s'affiche ✕ △ ✕). `sorts.csv` et
`SortsAtteignables.md` sont régénérés.

### D5. L'interrupteur

`[Magie] CombinaisonsLibres`, `oui` / `non`, lu dans `ReglagesLegacyPlus` comme `RamassageAuto` ;
`CombinaisonsLibres.Installer()` ne s'empile sur les coutures que s'il est actif.

## Risks / Trade-offs

- [Un code de 1998 non encore lu écrirait dans `+0x1c00..` — par exemple un onzième emplacement
  d'inventaire, `+0x300 + 10 × 0x280 = +0x1c00`] → une entrée invalide est lue vide (D1), donc pas
  de plantage ni de sort fantôme ; un raccourci perdu se verrait à l'essai.
- [La saisie n'est écrite sur le disque qu'à la sauvegarde suivante] → assumé par la spec, comme
  l'or ou l'inventaire.
- [Un raccourci peut être le préfixe d'un autre (✕ et ✕ ✕)] → sans effet : la longueur est comparée
  d'abord, les deux restent distincts.

## Migration Plan

Aucune conversion : les sauvegardes existantes ont des zéros en `+0x1c00`, c'est-à-dire tous les
raccourcis vides. Clé à `non` : le jeu de 1998, les octets écrits restent dans les sauvegardes sans
effet.
