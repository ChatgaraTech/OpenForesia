# Les sorts du jeu, un par un

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-spells.sh > Documentation/Etat/SortsAtteignables.md
```

Le troisieme document de la meme famille, apres `GestionnairesEntites.md` (ce que les
tables de sauts atteignent) et `ObjetsAtteignables.md` (ce qu'utiliser un objet
atteint). Celui-ci couvre la **magie** : les fiches de sort du CD, ce que lancer
chacune fait naitre, et si ce corps existe et a tourne.

Il croise cinq sources, **aucune ecrite a la main** : les fiches de
`Documentation/Reference/Bestiaire/sorts.csv` (lues sur le CD par le verbe
`bestiaire`), la table de sauts statique de `Blaze.exe`, les `case` du repartiteur
dans les sources C#, et les deux sondes `sorts-exerces.tsv` et `handlers-exerces.tsv`.
**Aucune partie n'est jouee pour le produire.**

## Ce que veulent dire les deux colonnes

| colonne | ce qu'elle dit | d'ou elle vient |
|---|---|---|
| **implemente** | le gestionnaire d'entite que le sort fait naitre existe | le sous-type `+0x10` resolu, croise avec les `case` du repartiteur |
| **teste** | le sort a reellement ete lance en jeu | la sonde de sorts, `sorts-exerces.tsv` |

**Elles sont independantes.** Un corps transcrit d'apres le desassembleur et qui
compile n'a rien prouve ; « implemente = oui, teste = non » est une dette.

> **`implemente` ne va pas plus loin que l'ENFANT DIRECT.** La colonne resout le
> sous-type `+0x10` de la fiche, et s'arrete la. Elle ne dit **rien** de ce que ce
> corps engendre a son tour : un sort peut se lire « implemente » et planter quand
> meme, parce qu'un petit-enfant manque. C'est arrive a Pardon et a Ange, dont les
> trois derniers trous ont ete trouves le 2026-09-01 en depliant a la main tous les
> engendrements du code ecrit. **Ce document n'est pas une preuve d'atteignabilite ;
> seule la sonde en est une.**

## Comment un sort atteint du code

`CastSpell_0053A980` prend une **famille** et un **indice**, va chercher la fiche de
`0x30` octets dans la table `+0x9c` du repertoire d'etat, et fait naitre une entite
dont le sous-type est l'octet **`+0x10`** de la fiche :

```
fiche = *(*(repertoire + 0x9c) + famille * 4) + indice * 0x30
SpawnEffectEntity(lanceur, fiche[0x10], fiche[0x11], fiche[0x12])   // FUN_0047a9d0
```

C'est **exactement le mecanisme des opcodes 9 et 10 du script d'objet**, et la
resolution est la meme : sous-type `< 0xc0` par la table statique `0x00551c30`, la
meme dans les onze zones ; `>= 0xc0` par la table `0x0056b0c8` installee pour la
sous-zone. Un sort ne depend d'aucune sous-zone — **il fait la meme chose partout**.

**Le lanceur n'est pas toujours un personnage.** L'opcode 14 du script d'objet
appelle la meme fonction avec sa propre paire famille/indice : une potion et un sort
de soin passent par la meme fiche. La sonde ne distingue pas les deux, et c'est
voulu — ce qu'on veut savoir est si **la fiche** a ete exercee.

---

## Les quatre ecoles

L'ecole d'un personnage est `+0x2b5`, posee a la creation depuis la table
`0x00562ab8` relevee ici meme :

| classe | ecole | fiches sur le CD | apprenables au palier 4 |
|---|---:|---:|---:|
| 0 Guerrier | 0 | 29 | 26 |
| 1 Pretre | 1 | 24 | 22 |
| 2 Voleur | 0 | 29 | 26 |
| 3 Sorcier | 0 | 29 | 26 |
| 4 Chasseur | 3 | 7 | 8 |
| 5 Elfe | 0 | 29 | 26 |
| 6 Nain | 0 | 29 | 26 |
| 7 Fee | 2 | 20 | 16 |

**Quatre classes sur huit lancent quelque chose** — 1, 3, 5 et 7 : ce sont les seules
que `TryLearnSpell` laisse passer, et les seules dont `ApplyGroundMotion_0051A020`
ouvre la posture d'incantation. Les quatre autres partagent l'ecole 0 sans jamais
y toucher.

**Le compte du CD recoupe le binaire.** `DAT_00564600` porte `29 24 20` pour les trois
ecoles reelles ; les listes du CD en donnent 29, 24 et 20. Les deux releves sont
independants — l'un vient de `.data`, l'autre de `BLAZE.ALL` — et c'est ce qui
valide la lecture des bornes de liste.

**L'ecole 3 n'est pas morte, elle est herboriste.** Le Chasseur s'en voit
attribuer une, et le CD y porte 7 fiches aux noms de plantes. Mais `DAT_00564600`
n'en declare que 1, la creation n'initialise le masque des sorts connus que pour les
ecoles 0, 1 et 2, et le Chasseur n'entre jamais dans la posture d'incantation :
**rien n'y mene en jeu normal**. Elles sont listees quand meme, parce qu'elles
existent.

---

## Les sorts, un par un

**Les quatre ecoles seulement** — familles 0 a 3, celles qu'une classe peut se voir
attribuer. Les quatre autres familles de la table sont a la fin du document : elles ne
sont pas de la magie de personnage, et les melanger fausserait le compte.

`PM` est le cout en points de magie (`+0x14`), `appr.` le cout d'apprentissage
(`+0x1d`) — celui que la portee du tirage doit couvrir a la montee de niveau —, et
`duree` l'incantation en images (`+0x13`).

**La combinaison n'est pas faite de directions mais des quatre boutons de face** :
`T` triangle, `O` rond, `X` croix, `C` carre. La saisie est
`(manette[2..3] >> 8) & 0xf0` et le masque teste `0xf000` — les quatre bits que
`s_PadButtonMasks_00564f18` nomme ainsi. Deux lettres collees sont **un seul coup a
deux boutons** ; les coups sont separes par un tiret.

| ecole | # | sort | combinaison | PM | appr. | duree | sous-type | gestionnaire | implemente | teste |
|---:|---:|---|---|---:|---:|---:|---:|---|---|---|
| 0 | 0 | Balle de feu | O-T-O | 2 | 10 | 4 | 0x00 | `0x0047ADD0` | oui | oui |
| 0 | 1 | Boule de lumière | T-C-O | 4 | 20 | 4 | 0x01 | `0x0047B0B0` | oui | oui |
| 0 | 2 | Pieds mouillés | X-T-X | 3 | 23 | 4 | 0x02 | `0x0047B460` | oui | oui |
| 0 | 3 | Rocher | C-T-C | 6 | 26 | 4 | 0x03 | `0x0047B7A0` | oui | oui |
| 0 | 4 | Feu d'enfer | C-X-O | 10 | 24 | 20 | 0x04 | `0x0047BB50` | oui | oui |
| 0 | 5 | Wonder | TO-T-O | 8 | 37 | 8 | 0x05 | `0x0047BF00` | oui | oui |
| 0 | 6 | Ténèbres | XC-C-X | 10 | 43 | 8 | 0x06 | `0x0047C3A0` | oui | oui |
| 0 | 7 | Onde de choc | X-X-T-T | 8 | 31 | 8 | 0x07 | `0x0047C660` | oui | oui |
| 0 | 8 | Flêches magiques | T-O-X-C | 12 | 40 | 8 | 0x08 | `0x0047C910` | oui | oui |
| 0 | 9 | Enchant | O-C-X-T | 15 | 35 | 24 | 0x09 | `0x0047CE40` | oui | oui |
| 0 | 10 | Mur de flammes | O-T-O-C-C-O | 16 | 44 | 20 | 0x0a | `0x0047CFF0` | oui | oui |
| 0 | 11 | Coup de jus | T-C-O-T-C-T | 16 | 47 | 20 | 0x0b | `0x0047D330` | oui | oui |
| 0 | 12 | Congèlo | X-T-X-C-C-X | 16 | 50 | 32 | 0x0c | `0x0047D600` | oui | oui |
| 0 | 13 | Atchoum | C-T-C-X-T-O | 20 | 52 | 40 | 0x0d | `0x0047DB50` | oui | oui |
| 0 | 14 | Assaut magique | C-T-O-C-X-O | 32 | 55 | 40 | 0x0e | `0x0047DF70` | oui | oui |
| 0 | 15 | Lumière | T-O-X-C-X-O-T-O | 24 | 58 | 60 | 0x0f | `0x0047E260` | oui | oui |
| 0 | 16 | Eclair | TO-T-O-T-C-T-X-T-O | 40 | 68 | 60 | 0x10 | `0x0047E590` | oui | **non** |
| 0 | 17 | Trouille | XC-C-X-O-T-C-O-T-O | 38 | 60 | 60 | 0x11 | `0x0047EAE0` | oui | **non** |
| 0 | 18 | Exorciste | C-O-T-X-C-O-T-O | 40 | 62 | 45 | 0x12 | `0x0047F070` | oui | **non** |
| 0 | 19 | Durdur | C-T-X-O-O-X-T-C | 38 | 64 | 36 | 0x13 | `0x0047F2B0` | oui | **non** |
| 0 | 20 | Boule de feu | O-T-O-C-C-O-T-TO-O | 45 | 68 | 60 | 0x14 | `0x0047F570` | oui | **non** |
| 0 | 21 | Ventilo | T-C-O-T-C-T-O-TO | 45 | 70 | 60 | 0x15 | `0x0047FB80` | oui | **non** |
| 0 | 22 | Démon polaire | X-T-X-C-C-X-C-XC-O | 30 | 75 | 60 | 0x16 | `0x0047FE60` | oui | **non** |
| 0 | 23 | Stalagtite | C-T-C-X-T-C-O-X-O | 45 | 72 | 60 | 0x17 | `0x00480600` | oui | **non** |
| 0 | 24 | Satanas | C-X-O-T-C-X-O-T-C-O-X-T-O | 60 | 74 | 60 | 0x18 | `0x00480B90` | oui | **non** |
| 0 | 25 | Au sec | T-X-O-C-XC-TO-TOXC | 80 | 76 | 120 | 0x19 | `0x00481670` | oui | **non** |
| 0 | 26 | Colère | XC-C-X-O-T-C-O-T-C-O-X-O | 96 | 0 | 132 | 0x1a | `0x004819D0` | oui | **non** |
| 0 | 27 | Météores | X-T-C-X-T-O-X-C-O-X-C-O-T | 82 | 0 | 96 | 0x1b | `0x00482370` | oui | **non** |
| 0 | 28 | Fusion | TO-T-O-T-C-T-X-T-X-T-T-TO | 100 | 0 | 120 | 0x1c | `0x004828F0` | oui | **non** |
| 1 | 0 | Pardon | X-T-T | 0 | 0 | 20 | 0x1e | `0x0044F060` | oui | **non** |
| 1 | 1 | Sang pur | T-O-O | 12 | 23 | 12 | 0x1f | `0x00447DF0` | oui | **non** |
| 1 | 2 | Renfort | C-O-C | 8 | 27 | 8 | 0x20 | `0x00448060` | oui | **non** |
| 1 | 3 | Gnoukoulé | X-O-X | 8 | 30 | 8 | 0x21 | `0x00448300` | oui | **non** |
| 1 | 4 | Sonde | C-X-O | 2 | 26 | 8 | 0x37 | `0x004484F0` | oui | **non** |
| 1 | 5 | Bobos finis | T-O-O-O-X-C-T | 42 | 35 | 32 | 0x23 | `0x004491F0` | oui | **non** |
| 1 | 6 | 3ème oeil | O-X-C | 2 | 34 | 16 | 0x38 | `0x00448EF0` | oui | **non** |
| 1 | 7 | Antidote | T-O-O-X-X-O | 16 | 37 | 16 | 0x25 | `0x00449590` | oui | **non** |
| 1 | 8 | Lourdes | T-O-O-C-C-O | 16 | 39 | 16 | 0x26 | `0x004498B0` | oui | **non** |
| 1 | 9 | Antisort | T-O-O-T-T-O | 16 | 41 | 16 | 0x27 | `0x00449C30` | oui | **non** |
| 1 | 10 | Marteau | T-O-O-O-O-O | 16 | 43 | 16 | 0x28 | `0x00449F90` | oui | **non** |
| 1 | 11 | Sang d'ange | T-O-O-O-O-T-T-O | 40 | 45 | 36 | 0x29 | `0x0044A4B0` | oui | **non** |
| 1 | 12 | Lapêche | O-T-C-X-O-T-C-X-O | 22 | 55 | 20 | 0x2a | `0x0044A7A0` | oui | **non** |
| 1 | 13 | Prière | O-X-C-T-O-X-C-T-O | 22 | 55 | 20 | 0x2b | `0x0044AE90` | oui | **non** |
| 1 | 14 | Calgon | C-O-X-T-C-O-X-T | 26 | 51 | 40 | 0x2c | `0x0044B510` | oui | **non** |
| 1 | 15 | Zombie | X-T-T-T-O-X-C | 10 | 48 | 60 | 0x2d | `0x0044F930` | oui | **non** |
| 1 | 16 | Parole sainte | X-T-T-XC-TO | 60 | 58 | 12 | 0x2e | `0x0044D370` | oui | **non** |
| 1 | 17 | Délivrance | T-O-O-O-T-O-TO-C-O-T-X | 40 | 63 | 36 | 0x2f | `0x0044B7B0` | oui | **non** |
| 1 | 18 | Micro | T-O-O-O-X-C-XC-C-O-T-X | 40 | 63 | 36 | 0x30 | `0x0044BAF0` | oui | **non** |
| 1 | 19 | Coup de pouce | T-O-O-O-O-T-T-O-T-O | 86 | 72 | 60 | 0x31 | `0x0044BF00` | oui | **non** |
| 1 | 20 | Remontant | T-O-O-C-TO-XC-O-O | 62 | 70 | 60 | 0x32 | `0x0044C3A0` | oui | **non** |
| 1 | 21 | Miracle | C-X-T-O-C-X-T-O-C-X-T-O | 39 | 66 | 60 | 0x33 | `0x0044C8A0` | oui | **non** |
| 1 | 22 | Résurection | T-O-X-C-TO-XC-TO-XC-XC | 100 | 0 | 40 | 0x34 | `0x0044CD00` | oui | **non** |
| 1 | 23 | Ange | T-O-C-T-O-X-C-O-X-C-T-O | 108 | 0 | 120 | 0x35 | `0x0044DC60` | oui | **non** |
| 2 | 0 | Dodo | C-C-O-O | 2 | 10 | 8 | 0xa0 | `0x00476C70` | oui | **non** |
| 2 | 1 | Gros sabots | C-O-X-X | 2 | 28 | 8 | 0xa1 | `0x00476FD0` | oui | **non** |
| 2 | 2 | Vitesse | C-O-T-T | 2 | 26 | 8 | 0xa2 | `0x00477360` | oui | **non** |
| 2 | 3 | Matéria feu | X-T-O-X-T | 16 | 33 | 24 | 0xa3 | `0x004774C0` | oui | **non** |
| 2 | 4 | Matéria terre | X-T-C-X-T | 16 | 33 | 24 | 0xa4 | `0x00477660` | oui | **non** |
| 2 | 5 | Matéria air | X-T-T-X-T | 16 | 33 | 24 | 0xa5 | `0x004777A0` | oui | **non** |
| 2 | 6 | Matéria eau | X-T-X-X-T | 16 | 33 | 24 | 0xa6 | `0x004778E0` | oui | **non** |
| 2 | 7 | Chant sirènes | C-C-T-O-X-C | 8 | 42 | 16 | 0xa7 | `0x00477A20` | oui | **non** |
| 2 | 8 | Ecrase | X-X-C-T-O-X | 8 | 48 | 16 | 0xa8 | `0x00477D20` | oui | **non** |
| 2 | 9 | Champ magique | T-C-X-X-C-T-O | 12 | 63 | 24 | 0xa9 | `0x004780B0` | oui | **non** |
| 2 | 10 | Coussin d'air | C-O-X-X-T-T-T-T | 30 | 54 | 24 | 0xaa | `0x004785F0` | oui | **non** |
| 2 | 11 | Escargot | X-O-T-T-O-X-O | 12 | 59 | 24 | 0xab | `0x004786C0` | oui | **non** |
| 2 | 12 | Ralenti | C-O-X-X-C-T-O-X | 40 | 67 | 40 | 0xac | `0x00478C20` | oui | **non** |
| 2 | 13 | Turbo | C-O-T-T-C-T-O-X | 40 | 65 | 40 | 0xad | `0x00478D10` | oui | **non** |
| 2 | 14 | Superman | X-T-X-T-X-C-T-X-O-T | 60 | 68 | 60 | 0xae | `0x00478DF0` | oui | **non** |
| 2 | 15 | Blindé | C-T-O-X-C-T-O-X-C-O | 80 | 63 | 20 | 0xaf | `0x004790C0` | oui | **non** |
| 2 | 16 | Liquide | XC-TO-X-X-X-X-X-X-X-O | 50 | 0 | 70 | 0xb0 | `0x00479F70` | oui | **non** |
| 2 | 17 | Flamme | XC-TO-O-O-O-O-O-O-O-O | 50 | 0 | 70 | 0xb1 | `0x00479300` | oui | **non** |
| 2 | 18 | Zéphe | XC-TO-T-T-T-T-T-T-T-O | 50 | 0 | 70 | 0xb2 | `0x00479DE0` | oui | **non** |
| 2 | 19 | Terre | XC-TO-C-C-C-C-C-C-C-O | 50 | 0 | 70 | 0xb3 | `0x00479C50` | oui | **non** |
| 3 | 0 | Lavande | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |
| 3 | 1 | Sauge | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |
| 3 | 2 | Verveine | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |
| 3 | 3 | Mercuriale | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |
| 3 | 4 | Fenouil | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |
| 3 | 5 | Hysopius | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |
| 3 | 6 | Belladone | — | 1 | 0 | 0 | 0x4f | `0x0047A5D0` | oui | **non** |

---

## Le compte

| | |
|---|---|
| fiches des quatre ecoles | **80** |
| dont les trois ecoles jouables | **73** |
| implementees (le gestionnaire existe) | **80** |
| a transcrire | **0** |
| lancees en jeu au moins une fois | **16** |
| dette : implementees mais jamais lancees | **64** |
| sous-types non resolvables (>= 0xc0, dependent de la sous-zone) | **0** |
| fiches des familles 4 a 7, comptees a part | 43 |

**La derniere ligne est le resultat le plus utile du releve.** Aucune fiche de sort
n'emploie un sous-type superieur ou egal a `0xc0` : tout passe par la table
statique `0x00551c30`, la meme dans les onze zones. **Un sort fait donc exactement
la meme chose ou qu'on soit** — ecrire ces corps une fois les couvre partout. La
branche par sous-zone reste dans le script parce que c'est la regle du binaire ;
elle ne se declenche simplement jamais ici.

---

## Les gestionnaires d'entite que les sorts font naitre

Colonne **emplois** : le nombre de fiches qui menent a ce corps. C'est l'ordre de
travail — le plus employe d'abord. La colonne **teste** est ici celle de la sonde
des *gestionnaires* (`handlers-exerces.tsv`), pas celle des sorts : elle dit que le
corps a tourne, par un sort ou par autre chose.

| gestionnaire | emplois | implemente | teste | sorts |
|---|---:|---|---|---|
| `0x0047A5D0` | 7 | oui | **non** | Lavande, Sauge, Verveine, Mercuriale, Fenouil, Hysopius, Belladone |
| `0x00447DF0` | 1 | oui | **non** | Sang pur |
| `0x00448060` | 1 | oui | **non** | Renfort |
| `0x00448300` | 1 | oui | **non** | Gnoukoulé |
| `0x004484F0` | 1 | oui | **non** | Sonde |
| `0x00448EF0` | 1 | oui | **non** | 3ème oeil |
| `0x004491F0` | 1 | oui | **non** | Bobos finis |
| `0x00449590` | 1 | oui | **non** | Antidote |
| `0x004498B0` | 1 | oui | **non** | Lourdes |
| `0x00449C30` | 1 | oui | **non** | Antisort |
| `0x00449F90` | 1 | oui | **non** | Marteau |
| `0x0044A4B0` | 1 | oui | **non** | Sang d'ange |
| `0x0044A7A0` | 1 | oui | **non** | Lapêche |
| `0x0044AE90` | 1 | oui | **non** | Prière |
| `0x0044B510` | 1 | oui | **non** | Calgon |
| `0x0044B7B0` | 1 | oui | **non** | Délivrance |
| `0x0044BAF0` | 1 | oui | **non** | Micro |
| `0x0044BF00` | 1 | oui | **non** | Coup de pouce |
| `0x0044C3A0` | 1 | oui | **non** | Remontant |
| `0x0044C8A0` | 1 | oui | **non** | Miracle |
| `0x0044CD00` | 1 | oui | **non** | Résurection |
| `0x0044D370` | 1 | oui | **non** | Parole sainte |
| `0x0044DC60` | 1 | oui | **non** | Ange |
| `0x0044F060` | 1 | oui | **non** | Pardon |
| `0x0044F930` | 1 | oui | **non** | Zombie |
| `0x00476C70` | 1 | oui | **non** | Dodo |
| `0x00476FD0` | 1 | oui | **non** | Gros sabots |
| `0x00477360` | 1 | oui | **non** | Vitesse |
| `0x004774C0` | 1 | oui | **non** | Matéria feu |
| `0x00477660` | 1 | oui | **non** | Matéria terre |
| `0x004777A0` | 1 | oui | **non** | Matéria air |
| `0x004778E0` | 1 | oui | **non** | Matéria eau |
| `0x00477A20` | 1 | oui | **non** | Chant sirènes |
| `0x00477D20` | 1 | oui | **non** | Ecrase |
| `0x004780B0` | 1 | oui | **non** | Champ magique |
| `0x004785F0` | 1 | oui | **non** | Coussin d'air |
| `0x004786C0` | 1 | oui | **non** | Escargot |
| `0x00478C20` | 1 | oui | **non** | Ralenti |
| `0x00478D10` | 1 | oui | **non** | Turbo |
| `0x00478DF0` | 1 | oui | **non** | Superman |
| `0x004790C0` | 1 | oui | **non** | Blindé |
| `0x00479300` | 1 | oui | **non** | Flamme |
| `0x00479C50` | 1 | oui | **non** | Terre |
| `0x00479DE0` | 1 | oui | **non** | Zéphe |
| `0x00479F70` | 1 | oui | **non** | Liquide |
| `0x0047ADD0` | 1 | oui | oui | Balle de feu |
| `0x0047B0B0` | 1 | oui | oui | Boule de lumière |
| `0x0047B460` | 1 | oui | oui | Pieds mouillés |
| `0x0047B7A0` | 1 | oui | oui | Rocher |
| `0x0047BB50` | 1 | oui | oui | Feu d'enfer |
| `0x0047BF00` | 1 | oui | oui | Wonder |
| `0x0047C3A0` | 1 | oui | oui | Ténèbres |
| `0x0047C660` | 1 | oui | oui | Onde de choc |
| `0x0047C910` | 1 | oui | oui | Flêches magiques |
| `0x0047CE40` | 1 | oui | oui | Enchant |
| `0x0047CFF0` | 1 | oui | oui | Mur de flammes |
| `0x0047D330` | 1 | oui | oui | Coup de jus |
| `0x0047D600` | 1 | oui | oui | Congèlo |
| `0x0047DB50` | 1 | oui | oui | Atchoum |
| `0x0047DF70` | 1 | oui | oui | Assaut magique |
| `0x0047E260` | 1 | oui | oui | Lumière |
| `0x0047E590` | 1 | oui | **non** | Eclair |
| `0x0047EAE0` | 1 | oui | **non** | Trouille |
| `0x0047F070` | 1 | oui | **non** | Exorciste |
| `0x0047F2B0` | 1 | oui | **non** | Durdur |
| `0x0047F570` | 1 | oui | **non** | Boule de feu |
| `0x0047FB80` | 1 | oui | **non** | Ventilo |
| `0x0047FE60` | 1 | oui | **non** | Démon polaire |
| `0x00480600` | 1 | oui | **non** | Stalagtite |
| `0x00480B90` | 1 | oui | **non** | Satanas |
| `0x00481670` | 1 | oui | **non** | Au sec |
| `0x004819D0` | 1 | oui | **non** | Colère |
| `0x00482370` | 1 | oui | **non** | Météores |
| `0x004828F0` | 1 | oui | **non** | Fusion |

**74 gestionnaires distincts**, dont **0** a transcrire.

**Presque un corps par sort.** Ce n'est pas un repartiteur a quelques branches
partagees comme la table des 48 effets : chaque sort des trois ecoles jouables a le
sien, et ces corps se suivent dans le binaire par blocs de quelques centaines
d'octets. Les sept herbes du Chasseur sont la seule exception — elles partagent un
seul corps, ce qui recoupe leurs fiches, identiques a un parametre pres.

---

## Les familles au-dela des ecoles

La table `+0x9c` porte huit familles ; les quatre dernieres — 43 fiches — ne sont
designees ni par une classe (`0x00562ab8` n'en nomme que 0, 1, 2 et 3) ni par un
script d'objet (l'opcode 14 n'emploie que les familles 0, 1 et 2). Le seul autre
appelant de `CastSpell_0053A980` est le mot `+0x06` d'une **entite de scene**, qui
porte sa propre paire famille/indice : ce sont donc, selon toute vraisemblance, les
sorts que les monstres lancent. Leurs noms sont restes dans le jeu de caracteres
d'origine — jamais traduits, jamais montres au joueur — ce qui va dans le meme sens.

**Leur etendue n'est pas etablie.** La derniere liste n'a pas de successeur pour la
borner, et le releve s'arrete a la premiere fiche dont la combinaison depasse `0x10`
coups — la borne du tampon de saisie. C'est un plafond, pas un compte, et c'est
pourquoi ces lignes sont hors du compte principal.

| famille | # | nom | sous-type | gestionnaire | implemente | teste |
|---:|---:|---|---:|---|---|---|
| 4 | 0 | ³ª²ò | 0x4b | `0x004477D0` | oui | **non** |
| 5 | 0 | ±Û | 0x49 | `0x0047A100` | oui | **non** |
| 6 | 0 | ½Àë½Ä | 0x4a | `0x004769A0` | oui | **non** |
| 7 | 0 | ù²èÝÀ¯Á | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 1 | õ××²èÀ¯Á | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 2 | õ××²è±² | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 3 | ºÝÌ­ç®Ý±² | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 4 | ½Ø÷±² | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 5 | Ì§²±òÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 6 | ºÙïòÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 7 | »ÝëòÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 8 | ±¼¯ïòÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 9 | ù²êÝòÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 10 | ½ÀÝòÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 11 | Ï¯ï½¸ØÑ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 12 | ïÚ²Ý | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 13 | Ê³ØÝã | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 14 | É¯¸ð¯¸ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 15 | ½ÀÝ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 16 | ½Ø÷¿Ýã | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 17 | ²ñÙ±² | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 18 | ²ñÙÊ³ØÝã | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 19 | ²ñÙÌ¨Ùï | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 20 | ²ÝñÆ¼òÙ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 21 | ØçªÈÚ¼®Ý | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 22 | ±½¸´²¸ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 23 | ÌÚ±òÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 24 | î¯ïäÄ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 25 | î½ÄÛ²Ô | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 26 | ³¨Ýï½Ï¯¼­ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 27 | À²ëÙ³ª²ò | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 28 | ÍÙÊ³ØÝã | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 29 | ë¸òÚ²è | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 30 | øÄØÌ§¸¼®Ý | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 31 | ¸Ø´²Ä±Ýî¯Ä | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 32 | »ÓÝ²ñÙ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 33 | ½ÄÝòÚ½ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 34 | ½Û³Û¯¸ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 35 | ³ª²ò | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 36 | ¿Æ¯¸òÑ | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 37 | á½ðÚ¯Ä | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 38 | õÜ³ª²ò | 0x1d | `0x0047ADC0` | oui | **non** |
| 7 | 39 | ÒÃµ½Ï¯¼­ | 0x1b | `0x00482370` | oui | **non** |

---

## Ce que ce releve ne dit pas

**Il ne dit pas ce que le sort FAIT**, seulement quel corps il fait naitre. Les degats
et les alterations d'etat vivent plus loin, dans la table des 48 effets
(`0x0056d4e0`) que le gestionnaire finit par appeler — les 48 sont transcrites, et
`ObjetsAtteignables.md` dit lesquelles un objet peut exercer.

**Il ne dit pas si le sort est ATTEIGNABLE.** Un sort au-dela du palier 4 de son
ecole existe, a une fiche complete, et sa combinaison est reconnue — mais rien en jeu
normal ne pose son bit dans le masque des sorts connus. Voir
`Documentation/Reference/Sorts.md`, qui traite l'apprentissage ; ce document-ci
traite la **couverture de la reconstruction**, ce qui est une autre question.

**Il ne dit pas non plus quel personnage a lance quoi.** La sonde releve la paire
famille/indice avec la zone et la sous-zone, pas le lanceur : une fiche exercee par un
objet compte comme exercee. C'est voulu — ce qui est verifie est le corps, pas le
chemin qui y mene.
