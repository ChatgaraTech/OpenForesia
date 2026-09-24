# `Host/Indeo4/` — le décodeur Indeo Video 4.1 en C#

Il décode les quatre films du CD (`Teps.AVI`, `opening.avi`, `ani01.avi`, `ending.avi`, tous en
`IV41` 256×224) **sans `ir41_32.ax`**, le codec d'Intel que Windows ne fournit plus.

C'est de l'échafaudage 2026, pas une transcription de `Blaze.exe` : le jeu d'origine confiait ses
films à DirectShow et ne contient aucune ligne de décodage vidéo.

## D'où il vient, et sous quelle licence

C'est un **portage en C# du décodeur Indeo 4 de FFmpeg** — celui qui permet à VLC de lire ces
films —, écrit par Maxim Poliakovski (2009-2011), à la révision
`b0d154591161153b0ec55d9527535bc776504899` :

| C# | source FFmpeg (`libavcodec/`) |
|---|---|
| `Indeo4Decoder.cs` | `indeo4.c` (en-têtes d'image et de bande, macroblocs) et `ivi.c` (bandes, tuiles, blocs, image) |
| `Indeo4Dsp.cs` | `ivi_dsp.c` (transformées inverses, compensation de mouvement, recomposition) |
| `Indeo4Bits.cs` | `ivi.c` (dictionnaires de Huffman) et le lecteur de bits petit-boutiste de `get_bits.h` |
| `Indeo4Tables.cs` | `ivi.c`, `indeo4data.h`, `mathtables.c` — **généré**, voir plus bas |

**Ces quatre fichiers sont sous LGPL 2.1 ou ultérieure**, comme leurs sources ; le texte est dans
`LICENSE-LGPL.txt`. La LGPL ne s'étend pas au reste du projet : elle demande que ce code-ci, et
ses modifications, restent disponibles en source et sous la même licence.

La structure suit FFmpeg fonction pour fonction, et le nom d'origine de chaque fonction est rappelé
dans son commentaire, bizarreries comprises (`mcType` qui survit d'un macrobloc à l'autre, `FFSIGN(0)`
qui vaut -1, les `int8_t` qui tronquent). C'est voulu : si un écart apparaît un jour, il se cherche
ligne à ligne contre la source.

Seul écart de forme : là où FFmpeg rend un code d'erreur, le portage lève une
`InvalidDataException`. Le mode Indeo 5 (`ff_ivi_recompose53`, `qdelta_present`, les échelles de
quantification) n'est pas porté.

## Comment il est vérifié

Contre `ir41_32.ax` lui-même, **à l'octet près**, sur les plans YVU9 (le codec d'Intel les rend
quand on lui demande `IF09` ; son RGB24 est tramé et ne sert pas de référence) :

```powershell
OpenForesia.Tools\bin\Debug\net9.0-windows\OpenForesia.Tools.exe indeo E:\French\ending.avi
```

**Résultat au 2026-09-24 : IDENTIQUE sur les quatre films, 8 849 images sur 8 849**, plans Y, V et U.
Environ 2,3 ms par image en Debug, pour des films à 15 images/s.

Ce que ces films **exercent** : images `INTRA`, `INTRA1`, `INTER`, `INTER_NOREF` ; transformées
slant ; vecteurs au demi-pixel. Ce qu'ils **n'exercent pas**, et qui est donc porté mais pas
vérifié : les images bidirectionnelles et vides, les tuiles, le mode échelonnable (quatre bandes de
luminance, recomposition de Haar), les transformées de Haar, les vecteurs au pixel plein, les images
INTER glissées après une INTRA. L'outil affiche ces indicateurs à chaque passage.

## Régénérer les tables

`Indeo4Tables.cs` n'est pas écrit à la main : `OpenForesia.Tools/gen-indeo4-tables.js` relit les
tableaux C de FFmpeg et vérifie la longueur de chacun. Les sources se récupèrent à la révision
ci-dessus (la commande est en tête du générateur), puis :

```powershell
node OpenForesia.Tools\gen-indeo4-tables.js <dossier des sources> OpenForesia\src\Host\Indeo4\Indeo4Tables.cs
```

Il doit reproduire le fichier en place à l'octet près.
