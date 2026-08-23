# OpenForesia — Relevé caméra Blaze.exe avec x32dbg

## Objectif

Comparer l'état mémoire de la caméra du jeu original `Blaze.exe` avec l'échafaudage OpenForesia, au chargement de zone puis en régime établi.

## Contexte

- `Blaze.exe` chargé à sa base préférée `0x00400000`.
- Premier breakpoint : `0x00516A83`, juste après le bloc caméra du chargement de zone.
- Second breakpoint : `0x004021E0`, entrée de la fonction caméra appelée très fréquemment / à chaque frame.
- Pour le second relevé, le breakpoint `0x004021E0` a été activé une fois **« La réception »** chargée et la caméra visuellement stabilisée.
- Dumps lus dans x32dbg en **Hexa / ASCII**.
- Architecture x86 **little-endian**.

---

## 1. Chargement de zone — breakpoint `0x00516A83`

### Dumps bruts Blaze.exe

```text
00604928  00 00 00 04 00 00 00 00 02 00 00 00 00 00 00 00
00564650  00 00 00 04 00 00 00 00 00 00 00 00 00 00 00 00
00564660  F4 01 00 00 00 00 00 00 80 00 00 00 C0 26 00 00
005DE068  00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
005DE078  00 00 00 00 00 00 00 00 84 03 00 00 00 00 00 00
005DE088  00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
006067C0  00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
006067D0  00 00 00 00 00 00 00 00 84 03 00 00 00 00 00 00
006067E8  00 00 00 04 00 00 00 00 00 00 00 00 00 00 00 00
```

### Valeurs décodées

```text
00604928 : cap=0, tangage=1024, roulis=0

00564650 :
  +0  = 0
  +2  = 1024
  +4  = 0
  +8  = 0
  +C  = 0
  +10 = 500   (distance)

005DE068 : cible=(0, 0, 0)
005DE078 : dec=(0, 0, 900)
005DE088 : src=(0, 0, 0)
006067C0 : position groupe=(0, 0, 0)
006067D0 : (0, 0, 900)
006067E8 : angles de vue=(0, 1024, 0)
```

### Constat

Les valeurs attendues par l'échafaudage OpenForesia au chargement correspondent aux valeurs observées dans `Blaze.exe` sur les blocs comparés.

---

## 2. Régime établi — breakpoint `0x004021E0`

Relevé effectué une fois **« La réception »** chargée et la caméra stabilisée.

### Dumps bruts Blaze.exe

```text
00604928  55 02 00 04 00 00 00 00 02 00 00 00 00 00 00 00
00564650  00 00 00 04 00 00 00 00 00 00 00 00 00 00 00 00
00564660  23 02 00 00 00 00 00 00 80 00 00 00 C0 26 00 00
005DE068  00 40 38 00 00 40 06 00 00 40 D4 FF 00 00 00 00
005DE078  00 00 00 00 4D 00 00 00 81 04 00 00 00 00 00 00
005DE088  00 00 00 00 4D 00 00 00 5E 02 00 00 00 00 00 00
006067C0  00 40 38 00 00 40 06 00 00 40 D4 FF 00 00 00 00
006067D0  00 00 00 00 4D 00 00 00 84 03 00 00 00 00 00 00
006067E8  58 02 00 04 00 00 02 00 00 00 00 00 4D 00 00 00
```

### Valeurs décodées dans Blaze.exe

```text
00604928 : cap=597, tangage=1024, roulis=0

00564650 :
  +0  = 0
  +2  = 1024
  +4  = 0
  +8  = 0
  +C  = 0
  +10 = 547   (distance)

005DE068 : cible=(3686400, 409600, -2867200)
005DE078 : dec=(0, 77, 1153)
005DE088 : src=(0, 77, 606)
006067C0 : (3686400, 409600, -2867200)
006067D0 : (0, 77, 900)
006067E8 : angles de vue=(600, 1024, 0)
```

---

## 3. Comparaison avec l'échafaudage OpenForesia

| Valeur | Blaze.exe original | OpenForesia (log Claude) | État |
|---|---:|---:|---|
| Distance `+0x10` | `547` | `547` | MATCH |
| Cible | `(3686400, 409600, -2867200)` | `(3686400, 409600, -2867200)` | MATCH |
| `src` | `(0, 77, 606)` | `(0, 114, 334)` | DIFF |
| `dec` | `(0, 77, 1153)` | `(0, 114, 881)` | DIFF |
| `006067D0` | `(0, 77, 900)` | `(0, 114, 900)` | DIFF sur Y |
| Angles de vue | `(600, 1024, 0)` | `(300, 0, 0)` | DIFF |
| `00604928` | `(597, 1024, 0)` | `(297, 3, 0)` | DIFF |

---

## 4. Constats utiles pour la suite

- L'initialisation observée à `0x00516A83` correspond aux valeurs OpenForesia sur les blocs comparés.
- En régime établi, la **distance caméra (`547`)** et la **cible** sont identiques entre `Blaze.exe` et OpenForesia.
- La divergence est déjà présente dans `0x005DE088` (`src`). Elle apparaît donc **en amont de `0x005DE078`** (`dec`) et pas uniquement pendant la transformation `src -> dec`.
- Dans `Blaze.exe`, la valeur `Y=77` se propage :

```text
005DE088 Y = 77
      ↓
005DE078 Y = 77
      ↓
006067D0 Y = 77
```

- OpenForesia semble faire la même propagation mais avec `Y=114`.
- Les angles de vue divergent fortement : `Blaze.exe` donne environ `(600, 1024, 0)`, alors que le log OpenForesia fourni indiquait `(300, 0, 0)`.
- Le cap de `00604928` vaut `597` dans ce relevé Blaze.exe tandis que `006067E8` donne `600`. Le breakpoint `0x004021E0` étant appelé très fréquemment, quelques unités d'écart peuvent dépendre du moment exact dans la frame.

### À investiguer en priorité

1. La production des entrées de `0x005DE088`.
2. Le chemin qui met à jour les angles de vue.
3. Pourquoi Blaze produit `Y=77 / Z=606` là où OpenForesia produit `Y=114 / Z=334`.
4. Pourquoi `006067E8` donne `(600,1024,0)` dans Blaze contre `(300,0,0)` dans OpenForesia.

---

## 5. Breakpoints utilisés

```text
0x00516A83
Première instruction après le bloc caméra du chargement de zone.

0x004021E0
Entrée de la fonction caméra appelée en régime établi / très fréquemment.

0x005169E2
Non utilisé pendant ce relevé.
Point supplémentaire proposé pour observer l'écriture de view[0] vers 0x00604928.
```
