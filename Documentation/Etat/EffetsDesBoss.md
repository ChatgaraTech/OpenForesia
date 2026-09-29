# Ce que chaque boss de l'arène emprunte à sa sous-zone

**Généré** par `OpenForesia.Tools effets-boss --sortie Documentation/Etat/EffetsDesBoss.md` ; ne pas éditer à la main.
Lecture statique de `Blaze.exe` et de l'archive (LP-013, tâche 8.1.4) : voir l'en-tête de `OpenForesia.Tools/EffetsBoss.cs` pour ce qu'elle voit et ce qu'elle ne voit pas.

Entrées de décodage : `s` = texture de la sous-zone (à greffer), `z` = texture de la zone (à greffer), `état 3` et `global` = communes à tout le jeu.

| Boss | Sous-zone | Effets à greffer | Sons de niveau | Textures à greffer (espèces comprises) | À lire à la main |
|---|---|---|---|---|---|
| Méchant ours | 3/5 | 2 | 1 + `+0xa8` | 2 | 0 |
| Behemoth | 3/9 | 2 | 0 | 1 | 0 |
| Sylphide et Wybarn | 4/7 | 2 | 1 | 2 | 0 |
| Fougre | 8/1 | 2 | 0 | 2 | 0 |
| Le gros poilu | 10/12 | 0 | 0 | 1 | 1 |
| Jeune dragon | 1/4 | 4 | 1 + `+0xa8` | 1 | 0 |
| Loup-garou et Tigre | 6/5 | 2 | 0 + `+0xa8` | 2 | 0 |
| Nocturio | 6/11 | 7 | 0 | 2 | 0 |
| Durahan | 5/4 | 2 | 1 + `+0xa8` | 2 | 1 |
| Troll | 1/9 | 2 | 1 + `+0xa8` | 2 | 0 |
| L'Ange noir | 10/14 | 8 | 0 + `+0xa8` | 1 | 0 |
| Eifridt | 10/11 | 2 | 0 | 1 | 0 |
| Kraaken | 9/0 | 2 | 0 | 3 | 0 |
| Roi-zombie | 5/9 | 13 | 1 + `+0xa8` | 5 | 1 |
| Lordémon | 10/13 | 6 | 0 + `+0xa8` | 1 | 0 |
| Cerbère | 10/9 | 9 | 0 + `+0xa8` | 2 | 1 |
| Belzébuth | 10/15 | 11 | 1 | 1 | 0 |
| Dragon zombie | 10/4 | 4 | 1 | 2 | 1 |
| Dragon rouge | 2/0 | 7 | 1 + `+0xa8` | 2 | 0 |

## Méchant ours (3/5)

- espèce 0 : texture s5 60×48 mots, 8 bits
- espèce 4 : texture s7 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00413330` (0x0056b0c8[30]), `0x004134c0` (0x0056b1c8[30])
- **sons de niveau** : 0x3f
- **champs du répertoire lus** : +0xa8, +0xcc — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x05 (s5 60×48 mots, 8 bits) ; 0x07 (s7 128×64 mots, 8 bits) ; 0x10 (état 3)

```
Méchant ours (3/5)
  espèce 0, IA 16 = 0x004a4ae0
    · son de niveau 0x3f
    · son de niveau 0x3f
    · lit le répertoire +0xcc
    · son de niveau 0x3f
    effet 0xde (0xc0 + catalogue +0xcc[0x09] = 0xde) → PAR SOUS-ZONE 0x0056b0c8[30] = 0x00413330
      · lit le répertoire +0xa8
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x004134c0
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
```

## Behemoth (3/9)

- espèce 0 : texture s5 92×48 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00483650` (0x0056b0c8[31]), `0x00483760` (0x0056b1c8[31])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xcc, +0xd0
- **entrées de décodage** : 0x05 (s5 92×48 mots, 8 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)

```
Behemoth (3/9)
  espèce 0, IA 17 = 0x004a54e0
    · lit le répertoire +0xcc
    effet 0xdf (0xc0 + catalogue +0xcc[0x02] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x00483650
      · modèle 56 (commun, état 3)
      · lit le répertoire +0xd0
      · modèle 7 (commun, état 3)
      effet 0xdf (0xc0 + catalogue +0xd0[0x00] = 0xdf) → PAR SOUS-ZONE 0x0056b1c8[31] = 0x00483760
        · lit le répertoire +0xd0
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Sylphide et Wybarn (4/7)

- espèce 1 : texture s4 128×64 mots, 8 bits
- espèce 2 : texture s5 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00483930` (0x0056b0c8[31]), `0x00483a30` (0x0056b1c8[31])
- **sons de niveau** : 0x47
- **champs du répertoire lus** : +0xcc, +0xd0
- **entrées de décodage** : 0x04 (s4 128×64 mots, 8 bits) ; 0x05 (s5 128×64 mots, 8 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)

```
Sylphide et Wybarn (4/7)
  espèce 1, IA 8 = 0x004b4570
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
  espèce 2, IA 14 = 0x004b7540
    · lit le répertoire +0xcc
    · son de niveau 0x47
    effet 0xdf (0xc0 + catalogue +0xcc[0x0c] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x00483930
      · modèle 7 (commun, état 3)
      · lit le répertoire +0xd0
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
```

## Fougre (8/1)

- espèce 1 : texture s4 104×64 mots, 8 bits
- espèce 0 : texture s3 112×48 mots, 8 bits
- **effets par sous-zone à greffer** : `0x0042f2a0` (0x0056b0c8[4]), `0x0042f3b0` (0x0056b1c8[4])
- **sons de niveau** : aucun
- **champs du répertoire lus** : aucun
- **entrées de décodage** : 0x03 (s3 112×48 mots, 8 bits) ; 0x04 (s4 104×64 mots, 8 bits) ; 0x10 (état 3)

```
Fougre (8/1)
  espèce 1, IA 3 = 0x004e20f0
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xc4 (0xc4) → PAR SOUS-ZONE 0x0056b0c8[4] = 0x0042f2a0
      effet 0xc4 (0xc4) → PAR SOUS-ZONE 0x0056b1c8[4] = 0x0042f3b0
        · modèle 42 (commun, état 3)
```

## Le gros poilu (10/12)

- espèce 0 : texture s4 96×64 mots, 8 bits
- **effets par sous-zone à greffer** : aucun
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xcc
- **entrées de décodage** : 0x04 (s4 96×64 mots, 8 bits) ; 0x10 (état 3)
- **à lire à la main** : sous-type variable (mov) (0x00500f4b)

```
Le gros poilu (10/12)
  espèce 0, IA 19 = 0x00500e10
    · lit le répertoire +0xcc
    · naissance par 0x0047a9d0 : sous-type variable (mov)
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
```

## Jeune dragon (1/4)

- espèce 2 : texture s5 96×48 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00409e40` (0x0056b0c8[32]), `0x00409f40` (0x0056b1c8[30]), `0x00482ff0` (0x0056b1c8[31]), `0x00483b50` (0x0056b0c8[31])
- **sons de niveau** : 0x41
- **champs du répertoire lus** : +0xa8, +0xcc, +0xd0 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x05 (s5 96×48 mots, 8 bits)

```
Jeune dragon (1/4)
  espèce 2, IA 13 = 0x004944e0
    · lit le répertoire +0xcc
    · son de niveau 0x41
    · lit le répertoire +0xcc
    effet 0xdf (0xc0 + catalogue +0xcc[0x0d] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x00483b50
      · modèle 30 (commun, état 3)
      · lit le répertoire +0xd0
    effet 0xe0 (0xc0 + catalogue +0xcc[0x0e] = 0xe0) → PAR SOUS-ZONE 0x0056b0c8[32] = 0x00409e40
      · lit le répertoire +0xa8
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x00409f40
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
```

## Loup-garou et Tigre (6/5)

- espèce 0 : texture s7 60×48 mots, 8 bits
- espèce 1 : texture s8 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x0042a440` (0x0056b0c8[30]), `0x0042a540` (0x0056b1c8[30])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xa8 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x07 (s7 60×48 mots, 8 bits) ; 0x08 (s8 128×64 mots, 8 bits) ; 0x10 (état 3)

```
Loup-garou et Tigre (6/5)
  espèce 0, IA 6 = 0x004d7200
    · lit le répertoire +0xa8
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b0c8[30] = 0x0042a440
      · lit le répertoire +0xa8
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x0042a540
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
  espèce 1, IA 7 = 0x004d80d0
    · lit le répertoire +0xa8
```

## Nocturio (6/11)

- espèce 0 : texture s8 116×64 mots, 8 bits
- espèce 2 : texture s9 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00429a20` (0x0056b0c8[32]), `0x00429be0` (0x0056b1c8[32]), `0x0042a350` (0x0056b2c8[33]), `0x0042a920` (0x0056b0c8[27]), `0x0042abf0` (0x0056b1c8[31]), `0x0042b0c0` (0x0056b2c8[30]), `0x0042b430` (0x0056b2c8[31])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xcc
- **entrées de décodage** : 0x08 (s8 116×64 mots, 8 bits) ; 0x09 (s9 128×64 mots, 8 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)

```
Nocturio (6/11)
  espèce 0, IA 14 = 0x004dacc0
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    effet 0xdb (0xc0 + catalogue +0xcc[0x0f] = 0xdb) → PAR SOUS-ZONE 0x0056b0c8[27] = 0x0042a920
      · modèle 63 (commun, état 3)
      effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b1c8[31] = 0x0042abf0
        · modèle 0 (commun, état 3)
        effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b2c8[30] = 0x0042b0c0
          · planche 0x005508d8 : entrées 0x10
      effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b2c8[31] = 0x0042b430
        · planche 0x005508b8 : entrées 0x10
        · planche 0x005508b8 : entrées 0x10
    effet 0xe0 (0xc0 + catalogue +0xcc[0x0b] = 0xe0) → PAR SOUS-ZONE 0x0056b0c8[32] = 0x00429a20
      · modèle 63 (commun, état 3)
      effet 0xe0 (0xe0) → PAR SOUS-ZONE 0x0056b1c8[32] = 0x00429be0
        · modèle 0 (commun, état 3)
        effet 0xe1 (0xe1) → PAR SOUS-ZONE 0x0056b2c8[33] = 0x0042a350
          · planche 0x005508d8 : entrées 0x10
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Durahan (5/4)

- espèce 2 : texture s8 104×64 mots, 8 bits
- espèce 3 : texture s9 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00422d50` (0x0056b0c8[31]), `0x00422e50` (0x0056b1c8[31])
- **sons de niveau** : 0x47
- **champs du répertoire lus** : +0xa8, +0xcc — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x08 (s8 104×64 mots, 8 bits) ; 0x09 (s9 128×64 mots, 8 bits) ; 0x10 (état 3)
- **à lire à la main** : sous-type variable ([esi + 0x14]) (0x004c5215)

```
Durahan (5/4)
  espèce 2, IA 7 = 0x004c4f90
    · naissance par 0x0047a9d0 : sous-type variable ([esi + 0x14])
    · lit le répertoire +0xcc
    · son de niveau 0x47
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xdf (0xc0 + catalogue +0xcc[0x09] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x00422d50
      · lit le répertoire +0xa8
      effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b1c8[31] = 0x00422e50
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
  espèce 3, IA 0 = 0x004c2770
    effet 0x36 (0x36) → statique 0x00551c30[0x36] = 0x00447a90
      effet 0xa1 (0xa1) → statique 0x00551f30[0xa1] = 0x00447d50
        · modèle 16 (commun, état 3)
    effet 0x4b (0x4b) → statique 0x00551c30[0x4b] = 0x004477d0
      · modèle 30 (commun, état 3)
```

## Troll (1/9)

- espèce 3 : texture s5 128×64 mots, 8 bits
- espèce 0 : texture s2 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00409e40` (0x0056b0c8[32]), `0x00409f40` (0x0056b1c8[30])
- **sons de niveau** : 0x40
- **champs du répertoire lus** : +0xa8, +0xcc — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x02 (s2 128×64 mots, 8 bits) ; 0x05 (s5 128×64 mots, 8 bits) ; 0x10 (état 3)

```
Troll (1/9)
  espèce 3, IA 16 = 0x004965b0
    · son de niveau 0x40
    · lit le répertoire +0xcc
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xe0 (0xc0 + catalogue +0xcc[0x0e] = 0xe0) → PAR SOUS-ZONE 0x0056b0c8[32] = 0x00409e40
      · lit le répertoire +0xa8
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x00409f40
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
```

## L'Ange noir (10/14)

- espèce 0 : texture s4 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x0043e820` (0x0056b2c8[30]), `0x0043eb60` (0x0056b0c8[33]), `0x0043ec60` (0x0056b1c8[33]), `0x0043f320` (0x0056b0c8[32]), `0x0043f4e0` (0x0056b1c8[32]), `0x0043fa90` (0x0056b2c8[31]), `0x00486b10` (0x0056b0c8[24]), `0x00486d60` (0x0056b1c8[24])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xa8, +0xcc, +0xd0, +0xd4 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x04 (s4 128×64 mots, 8 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)

```
L'Ange noir (10/14)
  espèce 0, IA 26 = 0x00502210
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xd4
    · lit le répertoire +0xd4
    · lit le répertoire +0xcc
    effet 0xe0 (0xc0 + catalogue +0xcc[0x0b] = 0xe0) → PAR SOUS-ZONE 0x0056b0c8[32] = 0x0043f320
      · modèle 63 (commun, état 3)
      effet 0xe0 (0xe0) → PAR SOUS-ZONE 0x0056b1c8[32] = 0x0043f4e0
        · modèle 0 (commun, état 3)
        effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b2c8[31] = 0x0043fa90
          · planche 0x00551500 : entrées 0x10
    effet 0xe1 (0xc0 + catalogue +0xcc[0x0e] = 0xe1) → PAR SOUS-ZONE 0x0056b0c8[33] = 0x0043eb60
      · lit le répertoire +0xa8
      effet 0xe1 (0xe1) → PAR SOUS-ZONE 0x0056b1c8[33] = 0x0043ec60
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
    effet 0xde (0xc0 + catalogue +0xd4[0x07] = 0xde) → PAR SOUS-ZONE 0x0056b2c8[30] = 0x0043e820
      · planche 0x00551518 : entrées 0x10
    effet 0xd8 (0xc0 + catalogue +0xcc[0x16] = 0xd8) → PAR SOUS-ZONE 0x0056b0c8[24] = 0x00486b10
      · lit le répertoire +0xd0
      · planche probable 0x00551a08 : entrées 0x10
      · planche 0x00551a08 : entrées 0x10
      · modèle 59 (commun, état 3)
      · lit le répertoire +0xd0
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Eifridt (10/11)

- espèce 0 : texture s3 128×128 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00482ff0` (0x0056b1c8[30]), `0x00483b50` (0x0056b0c8[30])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xcc, +0xd0
- **entrées de décodage** : 0x03 (s3 128×128 mots, 8 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)

```
Eifridt (10/11)
  espèce 0, IA 17 = 0x005003e0
    · lit le répertoire +0xcc
    effet 0xde (0xc0 + catalogue +0xcc[0x0d] = 0xde) → PAR SOUS-ZONE 0x0056b0c8[30] = 0x00483b50
      · modèle 30 (commun, état 3)
      · lit le répertoire +0xd0
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Kraaken (9/0)

- espèce 2 : texture s8 48×64 mots, 8 bits
- espèce 3 : texture s9 24×64 mots, 8 bits
- espèce 4 : texture s10 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x004844a0` (0x0056b0c8[31]), `0x00484560` (0x0056b1c8[31])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xcc, +0xd0
- **entrées de décodage** : 0x08 (s8 48×64 mots, 8 bits) ; 0x09 (s9 24×64 mots, 8 bits) ; 0x0a (s10 128×64 mots, 8 bits) ; 0x10 (état 3) ; 0x30 (global)
- lance des sorts (fiches communes)

```
Kraaken (9/0)
  espèce 2, IA 2 = 0x004e40e0
    · lit le répertoire +0xcc
    effet 0xdf (0xc0 + catalogue +0xcc[0x0a] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x004844a0
      · modèle 30 (commun, état 3)
      · lit le répertoire +0xd0
      effet 0xdf (0xc0 + catalogue +0xd0[0x09] = 0xdf) → PAR SOUS-ZONE 0x0056b1c8[31] = 0x00484560
        · planche probable 0x00551bb8 : entrées 0x10, 0x30
        · planche 0x00551bb8 : entrées 0x10, 0x30
  espèce 3, IA 3 = 0x004e4890
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
  espèce 4, IA 4 = 0x004e4f20
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Roi-zombie (5/9)

- espèce 1 : texture s5 128×64 mots, 8 bits
- espèce 0 : texture s4 128×64 mots, 8 bits
- espèce 2 : texture s6 96×48 mots, 8 bits
- espèce 3 : texture s7 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00421c10` (0x0056b0c8[29]), `0x00421dc0` (0x0056b2c8[45]), `0x00422330` (0x0056b0c8[30]), `0x004224f0` (0x0056b1c8[30]), `0x00422c60` (0x0056b2c8[46]), `0x00422d50` (0x0056b0c8[31]), `0x00422e50` (0x0056b1c8[31]), `0x00483c50` (0x0056b0c8[28]), `0x00483cc0` (0x0056b1c8[29]), `0x00483e90` (0x0056b1c8[28]), `0x00483f30` (0x0056b0c8[27]), `0x004840e0` (0x0056b1c8[27]), `0x00484190` (0x0056b1c8[26])
- **sons de niveau** : 0x47
- **champs du répertoire lus** : +0xa8, +0xcc, +0xd0 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x04 (s4 128×64 mots, 8 bits) ; 0x05 (s5 128×64 mots, 8 bits) ; 0x06 (s6 96×48 mots, 8 bits) ; 0x07 (s7 128×64 mots, 8 bits) ; 0x09 (s9 64×256 mots, 4 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)
- **à lire à la main** : sous-type variable ([esi + 0x14]) (0x004c5215)

```
Roi-zombie (5/9)
  espèce 1, IA 12 = 0x004c7240
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    effet 0xde (0xc0 + catalogue +0xcc[0x0b] = 0xde) → PAR SOUS-ZONE 0x0056b0c8[30] = 0x00422330
      · modèle 63 (commun, état 3)
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x004224f0
        · modèle 0 (commun, état 3)
        effet 0xee (0xee) → PAR SOUS-ZONE 0x0056b2c8[46] = 0x00422c60
          · planche 0x00550448 : entrées 0x10
    effet 0xdd (0xc0 + catalogue +0xcc[0x0f] = 0xdd) → PAR SOUS-ZONE 0x0056b0c8[29] = 0x00421c10
      effet 0xed (0xed) → PAR SOUS-ZONE 0x0056b2c8[45] = 0x00421dc0
        · planche 0x00550460 : entrées 0x09, 0x10
        · planche 0x00550460 : entrées 0x09, 0x10
    effet 0xdf (0xc0 + catalogue +0xcc[0x09] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x00422d50
      · lit le répertoire +0xa8
      effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b1c8[31] = 0x00422e50
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
    effet 0xdc (0xc0 + catalogue +0xcc[0x17] = 0xdc) → PAR SOUS-ZONE 0x0056b0c8[28] = 0x00483c50
      · modèle 35 (commun, état 3)
      · lit le répertoire +0xd0
      effet 0xdd (0xc0 + catalogue +0xd0[0x13] = 0xdd) → PAR SOUS-ZONE 0x0056b1c8[29] = 0x00483cc0
        · lit le répertoire +0xd0
        effet 0xdc (0xc0 + catalogue +0xd0[0x14] = 0xdc) → PAR SOUS-ZONE 0x0056b1c8[28] = 0x00483e90
          · modèle 53 (commun, état 3)
    effet 0xdb (0xc0 + catalogue +0xcc[0x18] = 0xdb) → PAR SOUS-ZONE 0x0056b0c8[27] = 0x00483f30
      · lit le répertoire +0xd0
      · modèle 54 (commun, état 3)
      · lit le répertoire +0xd0
      · modèle 53 (commun, état 3)
      effet 0xda (0xc0 + catalogue +0xd0[0x16] = 0xda) → PAR SOUS-ZONE 0x0056b1c8[26] = 0x00484190
        · modèle 1 (commun, état 3)
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
  espèce 0, IA 7 = 0x004c4f90
    · naissance par 0x0047a9d0 : sous-type variable ([esi + 0x14])
    · lit le répertoire +0xcc
    · son de niveau 0x47
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
```

## Lordémon (10/13)

- espèce 0 : texture s3 120×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x0043eb60` (0x0056b0c8[33]), `0x0043ec60` (0x0056b1c8[33]), `0x00486680` (0x0056b0c8[26]), `0x00486720` (0x0056b1c8[25]), `0x004868e0` (0x0056b0c8[25]), `0x004869d0` (0x0056b2c8[29])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xa8, +0xcc, +0xd0, +0xd4 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x03 (s3 120×64 mots, 8 bits) ; 0x10 (état 3)
- lance des sorts (fiches communes)

```
Lordémon (10/13)
  espèce 0, IA 25 = 0x00501840
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    effet 0xda (0xc0 + catalogue +0xcc[0x08] = 0xda) → PAR SOUS-ZONE 0x0056b0c8[26] = 0x00486680
      · modèle 56 (commun, état 3)
      · lit le répertoire +0xd0
      effet 0xd9 (0xc0 + catalogue +0xd0[0x07] = 0xd9) → PAR SOUS-ZONE 0x0056b1c8[25] = 0x00486720
        · lit le répertoire +0xd0
    effet 0xd9 (0xc0 + catalogue +0xcc[0x12] = 0xd9) → PAR SOUS-ZONE 0x0056b0c8[25] = 0x004868e0
      · lit le répertoire +0xd4
      · planche probable 0x00551a10 : entrées 0x10
      · planche 0x00551a10 : entrées 0x10
    effet 0xe1 (0xc0 + catalogue +0xcc[0x0e] = 0xe1) → PAR SOUS-ZONE 0x0056b0c8[33] = 0x0043eb60
      · lit le répertoire +0xa8
      effet 0xe1 (0xe1) → PAR SOUS-ZONE 0x0056b1c8[33] = 0x0043ec60
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Cerbère (10/9)

- espèce 0 : texture s4 128×64 mots, 8 bits
- espèce 1 : texture s5 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00435f70` (0x0056b0c8[27]), `0x00436070` (0x0056b1c8[30]), `0x004370e0` (0x0056b0c8[28]), `0x004372b0` (0x0056b2c8[30]), `0x00482ff0` (0x0056b1c8[29]), `0x00483b50` (0x0056b0c8[26]), `0x00484f80` (0x0056b0c8[22]), `0x00485220` (0x0056b0c8[21]), `0x00485330` (0x0056b1c8[26])
- **sons de niveau** : aucun
- **champs du répertoire lus** : +0xa8, +0xcc, +0xd0, +0xd4 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x04 (s4 128×64 mots, 8 bits) ; 0x05 (s5 128×64 mots, 8 bits) ; 0x10 (état 3) ; 0x30 (global)
- **à lire à la main** : sous-type introuvable (0x00485095)

```
Cerbère (10/9)
  espèce 0, IA 14 = 0x004f7980
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    effet 0xda (0xc0 + catalogue +0xcc[0x0d] = 0xda) → PAR SOUS-ZONE 0x0056b0c8[26] = 0x00483b50
      · modèle 30 (commun, état 3)
      · lit le répertoire +0xd0
    effet 0xd5 (0xc0 + catalogue +0xcc[0x15] = 0xd5) → PAR SOUS-ZONE 0x0056b0c8[21] = 0x00485220
      · lit le répertoire +0xd0
    effet 0xd6 (0xc0 + catalogue +0xcc[0x14] = 0xd6) → PAR SOUS-ZONE 0x0056b0c8[22] = 0x00484f80
      · planche probable 0x00551b10 : entrées 0x10
      · planche probable 0x00551b18 : entrées 0x10
      · planche probable 0x00551b20 : entrées 0x10
      · planche probable 0x00551bb8 : entrées 0x10, 0x30
      · planche probable 0x00551ba8 : entrées 0x10, 0x30
      · modèle 30 (commun, état 3)
      · lit le répertoire +0xd4
      · naissance par 0x0047ac40 : sous-type introuvable
    effet 0xdb (0xc0 + catalogue +0xcc[0x0e] = 0xdb) → PAR SOUS-ZONE 0x0056b0c8[27] = 0x00435f70
      · lit le répertoire +0xa8
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x00436070
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
  espèce 1, IA 12 = 0x004f5630
    effet 0xdc (0xdc) → PAR SOUS-ZONE 0x0056b0c8[28] = 0x004370e0
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b2c8[30] = 0x004372b0
        · planche 0x00550f68 : entrées 0x10
```

## Belzébuth (10/15)

- espèce 0 : texture s3 128×192 mots, 8 bits
- espèce 1 : texture s3 128×192 mots, 8 bits
- **effets par sous-zone à greffer** : `0x004855f0` (0x0056b0c8[31]), `0x00485680` (0x0056b1c8[31]), `0x00485760` (0x0056b0c8[30]), `0x00485850` (0x0056b1c8[30]), `0x00485940` (0x0056b0c8[29]), `0x00485a40` (0x0056b1c8[29]), `0x00485c00` (0x0056b0c8[28]), `0x00485ec0` (0x0056b1c8[28]), `0x00485f50` (0x0056b0c8[27]), `0x00486240` (0x0056b1c8[27]), `0x00486320` (0x0056b1c8[26])
- **sons de niveau** : 0x3c
- **champs du répertoire lus** : +0xcc, +0xd0
- **entrées de décodage** : 0x03 (s3 128×192 mots, 8 bits) ; 0x10 (état 3) ; 0x30 (global)
- lance des sorts (fiches communes)

```
Belzébuth (10/15)
  espèce 0, IA 27 = 0x00502cd0
    · son de niveau 0x3c
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xdf (0xc0 + catalogue +0xcc[0x03] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x004855f0
      · modèle 42 (commun, état 3)
      · lit le répertoire +0xd0
    effet 0xde (0xc0 + catalogue +0xcc[0x04] = 0xde) → PAR SOUS-ZONE 0x0056b0c8[30] = 0x00485760
      · modèle 30 (commun, état 3)
      · lit le répertoire +0xd0
      · lit le répertoire +0xd0
    effet 0xdd (0xc0 + catalogue +0xcc[0x05] = 0xdd) → PAR SOUS-ZONE 0x0056b0c8[29] = 0x00485940
      · modèle 56 (commun, état 3)
      · lit le répertoire +0xd0
      · modèle 3 (commun, état 3)
      effet 0xdd (0xc0 + catalogue +0xd0[0x03] = 0xdd) → PAR SOUS-ZONE 0x0056b1c8[29] = 0x00485a40
        · lit le répertoire +0xd0
    effet 0xdc (0xc0 + catalogue +0xcc[0x06] = 0xdc) → PAR SOUS-ZONE 0x0056b0c8[28] = 0x00485c00
      · planche probable 0x00551bb0 : entrées 0x10, 0x30
      · planche 0x00551bb0 : entrées 0x10, 0x30
      · planche probable 0x00551bb0 : entrées 0x10, 0x30
      · planche 0x00551bb0 : entrées 0x10, 0x30
      · lit le répertoire +0xd0
      · modèle 38 (commun, état 3)
    effet 0xdb (0xc0 + catalogue +0xcc[0x07] = 0xdb) → PAR SOUS-ZONE 0x0056b0c8[27] = 0x00485f50
      · modèle 0 (commun, état 3)
      · lit le répertoire +0xd0
      · planche probable 0x00551bb0 : entrées 0x10, 0x30
      · planche 0x00551bb0 : entrées 0x10, 0x30
      · planche probable 0x00551bb0 : entrées 0x10, 0x30
      · planche 0x00551bb0 : entrées 0x10, 0x30
      · modèle 63 (commun, état 3)
      · lit le répertoire +0xd0
      effet 0xda (0xc0 + catalogue +0xd0[0x06] = 0xda) → PAR SOUS-ZONE 0x0056b1c8[26] = 0x00486320
        · modèle 1 (commun, état 3)
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
```

## Dragon zombie (10/4)

- espèce 0 : texture s3 120×96 mots, 8 bits
- espèce 1 : texture s4 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x00436790` (0x0056b0c8[31]), `0x00436950` (0x0056b1c8[31]), `0x00436fa0` (0x0056b2c8[31]), `0x00485450` (0x0056b0c8[20])
- **sons de niveau** : 0x42
- **champs du répertoire lus** : +0xcc
- **entrées de décodage** : 0x03 (s3 120×96 mots, 8 bits) ; 0x04 (s4 128×64 mots, 8 bits) ; 0x10 (état 3)
- **à lire à la main** : sous-type variable (mov) (0x004f686b)

```
Dragon zombie (10/4)
  espèce 0, IA 13 = 0x004f6740
    · lit le répertoire +0xcc
    · naissance par 0x0047a9d0 : sous-type variable (mov)
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · son de niveau 0x42
    effet 0xdf (0xc0 + catalogue +0xcc[0x0b] = 0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x00436790
      · modèle 63 (commun, état 3)
      effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b1c8[31] = 0x00436950
        · modèle 0 (commun, état 3)
        effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b2c8[31] = 0x00436fa0
          · planche 0x00550fb8 : entrées 0x10
    effet 0xd4 (0xc0 + catalogue +0xcc[0x19] = 0xd4) → PAR SOUS-ZONE 0x0056b0c8[20] = 0x00485450
      · planche probable 0x005519d0 : entrées 0x10
      · planche 0x005519d0 : entrées 0x10
```

## Dragon rouge (2/0)

- espèce 0 : texture s4 128×64 mots, 8 bits
- espèce 2 : texture s5 128×64 mots, 8 bits
- **effets par sous-zone à greffer** : `0x0040cda0` (0x0056b0c8[30]), `0x0040cea0` (0x0056b1c8[30]), `0x0040d560` (0x0056b0c8[31]), `0x0040d6c0` (0x0056b2c8[31]), `0x004830f0` (0x0056b0c8[29]), `0x00483260` (0x0056b1c8[31]), `0x00483360` (0x0056b2c8[30])
- **sons de niveau** : 0x45
- **champs du répertoire lus** : +0xa8, +0xcc, +0xd0, +0xd4 — `+0xa8` : les sons de la sous-zone
- **entrées de décodage** : 0x04 (s4 128×64 mots, 8 bits) ; 0x05 (s5 128×64 mots, 8 bits) ; 0x10 (état 3) ; 0x30 (global)
- lance des sorts (fiches communes)

```
Dragon rouge (2/0)
  espèce 0, IA 1 = 0x00499140
    · lit le répertoire +0xcc
    · lit le répertoire +0xcc
    · son de niveau 0x45
    effet 0x39 (0x39) → statique 0x00551c30[0x39] = 0x00447280
      effet 0xa4 (0xa4) → statique 0x00551f30[0xa4] = 0x00447380
        · modèle 7 (commun, état 3)
      effet 0x98 (0x98) → statique 0x00552230[0x98] = 0x004474c0
        · planche 0x005515a0 : entrées 0x10
    effet 0xde (0xc0 + catalogue +0xcc[0x0e] = 0xde) → PAR SOUS-ZONE 0x0056b0c8[30] = 0x0040cda0
      · lit le répertoire +0xa8
      effet 0xde (0xde) → PAR SOUS-ZONE 0x0056b1c8[30] = 0x0040cea0
        · modèle 7 (commun, état 3)
        · modèle 48 (commun, état 3)
    effet 0xdd (0xc0 + catalogue +0xcc[0x13] = 0xdd) → PAR SOUS-ZONE 0x0056b0c8[29] = 0x004830f0
      · lit le répertoire +0xd0
      · lit le répertoire +0xd4
      · planche probable 0x00551bc8 : entrées 0x10, 0x30
      · planche 0x00551bc8 : entrées 0x10, 0x30
      · modèle 30 (commun, état 3)
    effet 0xbd (0xbd) → statique 0x00551f30[0xbd] = 0x004765d0
      · modèle 61 (commun, état 3)
  espèce 2, IA 0 = 0x00497ef0
    effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b0c8[31] = 0x0040d560
      effet 0xdf (0xdf) → PAR SOUS-ZONE 0x0056b2c8[31] = 0x0040d6c0
        · planche 0x0054f6d8 : entrées 0x10
```

