## Fiches de sort — table `+0x9c` a 0x0001bc, 8 famille(s)

| famille | pointeur | fiches | premier | dernier |
|---:|---:|---:|---|---|
| 0 | 0x000e68 | 29 | Balle de feu | Fusion |
| 1 | 0x0013d8 | 24 | Pardon | Ange |
| 2 | 0x001858 | 20 | Dodo | Terre |
| 3 | 0x001c18 | 7 | Lavande | Belladone |
| 4 | 0x001d68 | 1 | ³ª²ò | ³ª²ò |
| 5 | 0x001d98 | 1 | ±Û | ±Û |
| 6 | 0x001dc8 | 1 | ½Àë½Ä | ½Àë½Ä |
| 7 | 0x001df8 | 40 | ù²èÝÀ¯Á | ÒÃµ½Ï¯¼­ |

## Zone 0 — 3332 octets, 105 sous-zone(s)

### Zone 0, sous-zone 0 — repertoire de 14860 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.13 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 1 — repertoire de 4392 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x00020b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 2 — repertoire de 2748 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x4000 | `00 00 00 00 00 00 00 40` |
| 1 | 1 | 1 | 1 | 0x4000 | `01 01 00 00 01 01 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00060b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00060b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00060b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00060b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Genocide `0` | Cristal de terre `1` | Cristal de feu `2` | Cristal d'air `3` | Cristal d'ombre `4` | Cristal de lumière `5` | Cristal saint `6` | Cristal du mal `7` |
| 1 | Cristal de magie `8` | Lavande `9` | Sauge `10` | Verveine `11` | Mercuriale `12` | Fenouil `13` | Hysopius `14` | Belladone `15` |

### Zone 0, sous-zone 3 — repertoire de 3188 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 4 — repertoire de 7228 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.13 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.15 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 5 — repertoire de 6156 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 6 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 7 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 8 — repertoire de 15004 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.13 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 9 — repertoire de 4192 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 10 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 11 — repertoire de 2848 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 12 — repertoire de 7264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.13 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.15 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 13 — repertoire de 5988 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 14 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 15 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 16 — repertoire de 12960 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 17 — repertoire de 4296 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 18 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 19 — repertoire de 2892 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 20 — repertoire de 7276 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 21 — repertoire de 24124 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 0 | 1200 | 0 | 500 | 1536 | 0x0000ff00 |
| 0 | 4 | 5 | 2120 | 100 | 670 | 2048 | 0x0000ff05 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 22 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 23 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 24 — repertoire de 22376 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 25 — repertoire de 4084 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 26 — repertoire de 2384 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Béquille `3` | Béquille `3` | Debacle `2` | Béquille `3` | Béquille `3` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.7 | coffre `13` | 235 | objet | rangee 2 : Debacle (25 %), Debacle (17 %), Béquille (15 %), Béquille (13 %), Debacle (11 %), Béquille (9 %), Béquille (6 %), Debacle (4 %) | -1050 / 440 / 1500 |

### Zone 0, sous-zone 27 — repertoire de 3128 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 28 — repertoire de 7264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 29 — repertoire de 6324 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 30 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 31 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 32 — repertoire de 13368 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 33 — repertoire de 4136 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 34 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 35 — repertoire de 3540 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 36 — repertoire de 7256 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 37 — repertoire de 6128 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 38 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 39 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 40 — repertoire de 12412 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 41 — repertoire de 4128 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 42 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 43 — repertoire de 4520 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 7 | 0x0420 | `00 00 00 00 00 07 20 04` |
| 1 | 0 | 0 | 7 | 0x0420 | `01 00 00 00 00 07 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 1 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 2 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Fenril `22` | Corde du retour `0` |
| 3 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 4 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 5 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Crosse de voeux `26` | Corde du retour `0` |
| 6 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 7 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 8 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Stylet de bague `29` | Corde du retour `0` |
| 9 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 10 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 11 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Bâton de cristal `30` | Corde du retour `0` |
| 12 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 13 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 14 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Flèche de faucon `31` | Corde du retour `0` |
| 15 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 16 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 17 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Rapière du chaos `32` | Corde du retour `0` |
| 18 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 19 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 20 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Hache du gel `33` | Corde du retour `0` |
| 21 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 22 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 23 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Baguette de la licorne `34` | Corde du retour `0` |

### Zone 0, sous-zone 44 — repertoire de 7248 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 45 — repertoire de 6208 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 46 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 47 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 48 — repertoire de 14180 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 49 — repertoire de 4136 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 50 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 51 — repertoire de 4420 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 7 | 0x0420 | `00 00 00 00 00 07 20 04` |
| 1 | 0 | 0 | 7 | 0x0420 | `01 00 00 00 00 07 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 1 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 2 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Fenril `22` | Corde du retour `0` |
| 3 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 4 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 5 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Crosse de voeux `26` | Corde du retour `0` |
| 6 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 7 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 8 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Stylet de bague `29` | Corde du retour `0` |
| 9 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 10 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 11 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Bâton de cristal `30` | Corde du retour `0` |
| 12 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 13 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 14 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Flèche de faucon `31` | Corde du retour `0` |
| 15 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 16 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 17 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Rapière du chaos `32` | Corde du retour `0` |
| 18 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 19 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 20 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Hache du gel `33` | Corde du retour `0` |
| 21 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 22 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 23 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Baguette de la licorne `34` | Corde du retour `0` |

### Zone 0, sous-zone 52 — repertoire de 7256 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 53 — repertoire de 6640 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 54 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 55 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 56 — repertoire de 13128 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 57 — repertoire de 4116 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 58 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 59 — repertoire de 4180 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 7 | 0x0420 | `00 00 00 00 00 07 20 04` |
| 1 | 0 | 0 | 7 | 0x0420 | `01 00 00 00 00 07 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 1 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 2 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Fenril `22` | Corde du retour `0` |
| 3 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 4 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 5 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Crosse de voeux `26` | Corde du retour `0` |
| 6 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 7 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 8 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Stylet de bague `29` | Corde du retour `0` |
| 9 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 10 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 11 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Bâton de cristal `30` | Corde du retour `0` |
| 12 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 13 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 14 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Flèche de faucon `31` | Corde du retour `0` |
| 15 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 16 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 17 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Rapière du chaos `32` | Corde du retour `0` |
| 18 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 19 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 20 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Hache du gel `33` | Corde du retour `0` |
| 21 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 22 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 23 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Baguette de la licorne `34` | Corde du retour `0` |

### Zone 0, sous-zone 60 — repertoire de 7256 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 61 — repertoire de 11612 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |
| 0 | 4 | 0 | 1200 | 0 | 500 | 1536 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 62 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 63 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 64 — repertoire de 12900 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 65 — repertoire de 4132 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 66 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 67 — repertoire de 4420 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 7 | 0x0420 | `00 00 00 00 00 07 20 04` |
| 1 | 0 | 0 | 7 | 0x0420 | `01 00 00 00 00 07 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 1 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 2 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Fenril `22` | Corde du retour `0` |
| 3 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 4 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 5 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Crosse de voeux `26` | Corde du retour `0` |
| 6 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 7 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 8 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Stylet de bague `29` | Corde du retour `0` |
| 9 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 10 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 11 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Bâton de cristal `30` | Corde du retour `0` |
| 12 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 13 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 14 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Flèche de faucon `31` | Corde du retour `0` |
| 15 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 16 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 17 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Rapière du chaos `32` | Corde du retour `0` |
| 18 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 19 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 20 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Hache du gel `33` | Corde du retour `0` |
| 21 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 22 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 23 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Baguette de la licorne `34` | Corde du retour `0` |

### Zone 0, sous-zone 68 — repertoire de 7288 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 69 — repertoire de 8232 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 70 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 71 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 72 — repertoire de 13944 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 73 — repertoire de 5896 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 74 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 75 — repertoire de 4020 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 7 | 0x0420 | `00 00 00 00 00 07 20 04` |
| 1 | 0 | 0 | 7 | 0x0420 | `01 00 00 00 00 07 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 1 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 2 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Fenril `22` | Corde du retour `0` |
| 3 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 4 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 5 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Crosse de voeux `26` | Corde du retour `0` |
| 6 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 7 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 8 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Stylet de bague `29` | Corde du retour `0` |
| 9 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 10 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 11 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Bâton de cristal `30` | Corde du retour `0` |
| 12 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 13 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 14 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Flèche de faucon `31` | Corde du retour `0` |
| 15 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 16 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 17 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Rapière du chaos `32` | Corde du retour `0` |
| 18 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 19 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 20 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Hache du gel `33` | Corde du retour `0` |
| 21 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 22 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 23 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Baguette de la licorne `34` | Corde du retour `0` |

### Zone 0, sous-zone 76 — repertoire de 7252 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 77 — repertoire de 14308 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |
| 0 | 4 | 0 | 1200 | 0 | 500 | 1536 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 78 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 79 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 80 — repertoire de 15076 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |
| 6 | 0 | 0 | 0 | 0x0420 | `06 00 00 00 00 00 20 04` |
| 7 | 0 | 0 | 0 | 0x0420 | `07 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **£** | 1 | **1** | **1** | 999 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 490 | 100 | -930 | 3584 | 0x0000ff00 |
| 0 | 1 | 1 | 85 | 100 | -700 | 1024 | 0x0000ff01 |
| 0 | 2 | 2 | 300 | 100 | -500 | 1024 | 0x0000ff02 |
| 1 | 0 | 4 | 550 | 0 | 430 | 0 | 0x0000ff04 |
| 1 | 1 | 5 | 260 | 0 | 700 | 2048 | 0x0000ff05 |
| 1 | 2 | 6 | 386 | 0 | 900 | 1024 | 0x0000ff06 |
| 1 | 3 | 7 | 670 | 0 | 550 | 3072 | 0x0000ff07 |
| 1 | 4 | 3 | 1000 | 0 | 500 | 3456 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` | Petite potion `0` |
| 1 | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` | Aiguille médicinale `1` |
| 2 | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` | Debacle `2` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.10 | coffre-18 `18` | 234 | objet | rangee 1 : Aiguille médicinale (25 %), Aiguille médicinale (17 %), Aiguille médicinale (15 %), Aiguille médicinale (13 %), Aiguille médicinale (11 %), Aiguille médicinale (9 %), Aiguille médicinale (6 %), Aiguille médicinale (4 %) | 250 / 204 / -1000 |

### Zone 0, sous-zone 81 — repertoire de 4076 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1230 | 400 | 70 | 512 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 82 — repertoire de 2244 octets

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 83 — repertoire de 4360 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 7 | 0x0420 | `00 00 00 00 00 07 20 04` |
| 1 | 0 | 0 | 7 | 0x0420 | `01 00 00 00 00 07 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -60 | 400 | 2300 | 2048 | 0x0000ff00 |
| 0 | 1 | 1 | -500 | 400 | 2100 | 1500 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 1 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 2 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Fenril `22` | Corde du retour `0` |
| 3 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 4 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 5 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Crosse de voeux `26` | Corde du retour `0` |
| 6 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 7 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 8 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Stylet de bague `29` | Corde du retour `0` |
| 9 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 10 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 11 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Bâton de cristal `30` | Corde du retour `0` |
| 12 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 13 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 14 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Flèche de faucon `31` | Corde du retour `0` |
| 15 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 16 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 17 | Bouclier de l'ombre `27` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Bouclier des élémentaux `28` | Couronne du soleil `21` | Rapière du chaos `32` | Corde du retour `0` |
| 18 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 19 | Cristal saint `8` | Cristal du mal `9` | Gantelets de chevaliers `10` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 20 | Armure mirage `16` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Armure du gardien `20` | Couronne du soleil `21` | Hache du gel `33` | Corde du retour `0` |
| 21 | Corde du retour `0` | Cristal de magie `1` | Cristal de feu `2` | Cristal d'eau `3` | Cristal de terre `4` | Cristal d'air `5` | Cristal de lumière `6` | Cristal d'ombre `7` |
| 22 | Cristal saint `8` | Cristal du mal `9` | Gants de force `23` | Panoramix `11` | Ambrosia `12` | Croix `13` | Talisman médicinal `14` | Plume dominicale `15` |
| 23 | Robe des anciens `24` | Miroir de la porte `17` | Miroir d'eau `18` | Anneau des éléments `19` | Robe magique `25` | Couronne du soleil `21` | Baguette de la licorne `34` | Corde du retour `0` |

### Zone 0, sous-zone 84 — repertoire de 7252 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 1 | 8 | 0x0420 | `00 00 00 00 01 08 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |
| 1 | **µÔç** | 1 | **76** | **1** | 2 | 999 | 160 | 0 | `999 999 999 999 999 999 999 999` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 71 | 100 | -697 | 1024 | 0x0000ff04 |
| 0 | 1 | 1 | 290 | 130 | -1180 | 0 | 0x0000ff01 |
| 0 | 2 | 3 | 118 | 100 | -457 | 1024 | 0x0000ff03 |
| 1 | 0 | 0 | 985 | 0 | 520 | 3072 | 0x0000ff00 |
| 1 | 1 | 6 | 530 | 0 | 700 | 3072 | 0x0000ff06 |
| 1 | 2 | 5 | 858 | 0 | 362 | 3072 | 0x0000ff05 |
| 1 | 3 | 7 | 300 | 100 | -100 | 0 | 0x0000ff07 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 1 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 2 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 3 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 4 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 5 | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` | Bouclier solaire `2` |
| 6 | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` | Armure anti-sortilège `0` |
| 7 | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` | Robe du gardien `1` |
| 8 | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` | Livre de la technique `3` |
| 9 | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` | Gabriel `4` |
| 10 | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` | Harpe d'or `5` |
| 11 | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` | Almader `6` |
| 12 | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` | Bottes ailées `7` |
| 13 | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` | Chaîne du souffle `8` |
| 14 | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` | Gantelets ailés `9` |
| 15 | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` | Pentacle magique `10` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.14 | coffre-17 `17` | 53 | objet | rangee 0 : Armure anti-sortilège (25 %), Armure anti-sortilège (17 %), Armure anti-sortilège (15 %), Armure anti-sortilège (13 %), Armure anti-sortilège (11 %), Armure anti-sortilège (9 %), Armure anti-sortilège (6 %), Armure anti-sortilège (4 %) | 700 / 440 / 2400 |
| 0.16 | coffre-17 `17` | 54 | objet | rangee 8 : Livre de la technique (25 %), Livre de la technique (17 %), Livre de la technique (15 %), Livre de la technique (13 %), Livre de la technique (11 %), Livre de la technique (9 %), Livre de la technique (6 %), Livre de la technique (4 %) | 430 / 440 / 2100 |

### Zone 0, sous-zone 85 — repertoire de 14720 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0420 | `00 00 00 00 00 00 20 04` |
| 1 | 0 | 0 | 0 | 0x0420 | `01 00 00 00 00 00 20 04` |
| 2 | 0 | 0 | 0 | 0x0420 | `02 00 00 00 00 00 20 04` |
| 3 | 0 | 0 | 0 | 0x0420 | `03 00 00 00 00 00 20 04` |
| 4 | 0 | 0 | 0 | 0x0420 | `04 00 00 00 00 00 20 04` |
| 5 | 0 | 0 | 0 | 0x0420 | `05 00 00 00 00 00 20 04` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **µÔç** | 1 | **1** | **1** | 2 | 999 | 79 | 0 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 1850 | 100 | 330 | 512 | 0x0000ff01 |
| 0 | 1 | 2 | 1850 | 100 | 670 | 2048 | 0x0000ff02 |
| 0 | 2 | 3 | 1500 | 100 | 1050 | 1536 | 0x0000ff03 |
| 0 | 3 | 4 | 2280 | 100 | 323 | 3500 | 0x0000ff04 |
| 0 | 4 | 0 | 1200 | 0 | 500 | 1536 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Petite potion `0` | Aiguille médicinale `1` | Debacle `2` | Béquille `3` | `4` *(hors catalogue)* | `5` *(hors catalogue)* | `6` *(hors catalogue)* | `7` *(hors catalogue)* |

### Zone 0, sous-zone 86 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 87 — repertoire de 264 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x0000 | `00 00 00 00 00 00 00 00` |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* | `0` *(hors catalogue)* |

### Zone 0, sous-zone 88 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ï©Ñ¿ï `0` | ò×¯ï¿ï `1` | à§ÙÑÝ¸ `2` | î­×ÝëÙ `3` | ´¸½¶Øð `4` | à§Ã²Ý `5` | ±Ý½³ª× `6` | ¹ÙñÑòÚï `7` |
| 1 | ï©Ñ¿ï `0` | ò×¯ï¿ï `1` | à§ÙÑÝ¸ `2` | î­×ÝëÙ `3` | ´¸½¶Øð `4` | à§Ã²Ý `5` | ±Ý½³ª× `6` | ¹ÙñÑòÚï `7` |
| 2 | ï©Ñ¿ï `0` | ò×¯ï¿ï `1` | à§ÙÑÝ¸ `2` | î­×ÝëÙ `3` | ´¸½¶Øð `4` | à§Ã²Ý `5` | ±Ý½³ª× `6` | ¹ÙñÑòÚï `7` |
| 3 | ï©Ñ¿ï `0` | ò×¯ï¿ï `1` | à§ÙÑÝ¸ `2` | î­×ÝëÙ `3` | ´¸½¶Øð `4` | à§Ã²Ý `5` | ±Ý½³ª× `6` | ¹ÙñÑòÚï `7` |
| 4 | ï©Ñ¿ï `0` | ò×¯ï¿ï `1` | à§ÙÑÝ¸ `2` | î­×ÝëÙ `3` | ´¸½¶Øð `4` | à§Ã²Ý `5` | ±Ý½³ª× `6` | ¹ÙñÑòÚï `7` |

### Zone 0, sous-zone 89 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | îñÙÒ²½ `0` | ç¬¯çÒÝÄÒ²½ `1` | ³¨¯¼­ÊÝÏ `2` | áòØ´Ù `3` | ¾×Ì¨ÑÒ²½ `4` | ¾×Ì¨ÑÒ²½ `4` | ÙÊÝÏ `5` | ÙÊÝÏ `5` |
| 1 | îñÙÒ²½ `0` | ç¬¯çÒÝÄÒ²½ `1` | ³¨¯¼­ÊÝÏ `2` | áòØ´Ù `3` | ¾×Ì¨ÑÒ²½ `4` | ¾×Ì¨ÑÒ²½ `4` | ÙÊÝÏ `5` | ÙÊÝÏ `5` |
| 2 | îñÙÒ²½ `0` | ç¬¯çÒÝÄÒ²½ `1` | ³¨¯¼­ÊÝÏ `2` | áòØ´Ù `3` | ¾×Ì¨ÑÒ²½ `4` | ¾×Ì¨ÑÒ²½ `4` | ÙÊÝÏ `5` | ÙÊÝÏ `5` |
| 3 | îñÙÒ²½ `0` | ç¬¯çÒÝÄÒ²½ `1` | ³¨¯¼­ÊÝÏ `2` | áòØ´Ù `3` | ¾×Ì¨ÑÒ²½ `4` | ¾×Ì¨ÑÒ²½ `4` | ÙÊÝÏ `5` | ÙÊÝÏ `5` |
| 4 | îñÙÒ²½ `0` | ç¬¯çÒÝÄÒ²½ `1` | ³¨¯¼­ÊÝÏ `2` | áòØ´Ù `3` | ¾×Ì¨ÑÒ²½ `4` | ¾×Ì¨ÑÒ²½ `4` | ÙÊÝÏ `5` | ÙÊÝÏ `5` |

### Zone 0, sous-zone 90 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ÷Ú½ÊÝÏ `0` | ×²Ì¿ï `1` | ±¸¾ÙòÂ `2` | Ú¯ï±² `3` | ´Åçç­´Ù `4` | ´ÝÊÝ½Ûò `5` | ×±Ï `6` | Ð×ç­¼Ùï `7` |
| 1 | ÷Ú½ÊÝÏ `0` | ×²Ì¿ï `1` | ±¸¾ÙòÂ `2` | Ú¯ï±² `3` | ´Åçç­´Ù `4` | ´ÝÊÝ½Ûò `5` | ×±Ï `6` | Ð×ç­¼Ùï `7` |
| 2 | ÷Ú½ÊÝÏ `0` | ×²Ì¿ï `1` | ±¸¾ÙòÂ `2` | Ú¯ï±² `3` | ´Åçç­´Ù `4` | ´ÝÊÝ½Ûò `5` | ×±Ï `6` | Ð×ç­¼Ùï `7` |
| 3 | ÷Ú½ÊÝÏ `0` | ×²Ì¿ï `1` | ±¸¾ÙòÂ `2` | Ú¯ï±² `3` | ´Åçç­´Ù `4` | ´ÝÊÝ½Ûò `5` | ×±Ï `6` | Ð×ç­¼Ùï `7` |
| 4 | ÷Ú½ÊÝÏ `0` | ×²Ì¿ï `1` | ±¸¾ÙòÂ `2` | Ú¯ï±² `3` | ´Åçç­´Ù `4` | ´ÝÊÝ½Ûò `5` | ×±Ï `6` | Ð×ç­¼Ùï `7` |

### Zone 0, sous-zone 91 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ÒÃµ½À¯Ì `0` | á²±½À¯Ì `1` | î¨ð²Ý½À¯Ì `2` | ´Ý¼ªÝÄ½À¯Ì `3` | ´Ý¼ªÝÄ½À¯Ì `3` | ðÊÑÄ `4` | ðÊÑÄ `4` | ðÊÑÄ `4` |
| 1 | ÒÃµ½À¯Ì `0` | á²±½À¯Ì `1` | î¨ð²Ý½À¯Ì `2` | ´Ý¼ªÝÄ½À¯Ì `3` | ´Ý¼ªÝÄ½À¯Ì `3` | ðÊÑÄ `4` | ðÊÑÄ `4` | ðÊÑÄ `4` |
| 2 | ÒÃµ½À¯Ì `0` | á²±½À¯Ì `1` | î¨ð²Ý½À¯Ì `2` | ´Ý¼ªÝÄ½À¯Ì `3` | ´Ý¼ªÝÄ½À¯Ì `3` | ðÊÑÄ `4` | ðÊÑÄ `4` | ðÊÑÄ `4` |
| 3 | ÒÃµ½À¯Ì `0` | á²±½À¯Ì `1` | î¨ð²Ý½À¯Ì `2` | ´Ý¼ªÝÄ½À¯Ì `3` | ´Ý¼ªÝÄ½À¯Ì `3` | ðÊÑÄ `4` | ðÊÑÄ `4` | ðÊÑÄ `4` |
| 4 | ÒÃµ½À¯Ì `0` | á²±½À¯Ì `1` | î¨ð²Ý½À¯Ì `2` | ´Ý¼ªÝÄ½À¯Ì `3` | ´Ý¼ªÝÄ½À¯Ì `3` | ðÊÑÄ `4` | ðÊÑÄ `4` | ðÊÑÄ `4` |

### Zone 0, sous-zone 92 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ×²Ä±Û `0` | Ì§ÙºÝ±Û `1` | ±ÙÃÐ½ `2` | ×Ú²´±Û `3` | ×Ú²´±Û `3` | Á¬è±Û `4` | Á¬è±Û `4` | Á¬è±Û `4` |
| 1 | ×²Ä±Û `0` | Ì§ÙºÝ±Û `1` | ±ÙÃÐ½ `2` | ×Ú²´±Û `3` | ×Ú²´±Û `3` | Á¬è±Û `4` | Á¬è±Û `4` | Á¬è±Û `4` |
| 2 | ×²Ä±Û `0` | Ì§ÙºÝ±Û `1` | ±ÙÃÐ½ `2` | ×Ú²´±Û `3` | ×Ú²´±Û `3` | Á¬è±Û `4` | Á¬è±Û `4` | Á¬è±Û `4` |
| 3 | ×²Ä±Û `0` | Ì§ÙºÝ±Û `1` | ±ÙÃÐ½ `2` | ×Ú²´±Û `3` | ×Ú²´±Û `3` | Á¬è±Û `4` | Á¬è±Û `4` | Á¬è±Û `4` |
| 4 | ×²Ä±Û `0` | Ì§ÙºÝ±Û `1` | ±ÙÃÐ½ `2` | ×Ú²´±Û `3` | ×Ú²´±Û `3` | Á¬è±Û `4` | Á¬è±Û `4` | Á¬è±Û `4` |

### Zone 0, sous-zone 93 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ù²èÝÚ²ö± `0` | ¿³Ù²À `1` | ¶µ½Ú²ö± `2` | ½ÛÝèÚ²ö± `3` | ½ÛÝèÚ²ö± `3` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` |
| 1 | ù²èÝÚ²ö± `0` | ¿³Ù²À `1` | ¶µ½Ú²ö± `2` | ½ÛÝèÚ²ö± `3` | ½ÛÝèÚ²ö± `3` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` |
| 2 | ù²èÝÚ²ö± `0` | ¿³Ù²À `1` | ¶µ½Ú²ö± `2` | ½ÛÝèÚ²ö± `3` | ½ÛÝèÚ²ö± `3` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` |
| 3 | ù²èÝÚ²ö± `0` | ¿³Ù²À `1` | ¶µ½Ú²ö± `2` | ½ÛÝèÚ²ö± `3` | ½ÛÝèÚ²ö± `3` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` |
| 4 | ù²èÝÚ²ö± `0` | ¿³Ù²À `1` | ¶µ½Ú²ö± `2` | ½ÛÝèÚ²ö± `3` | ½ÛÝèÚ²ö± `3` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` | áØ±Ý¿ï `4` |

### Zone 0, sous-zone 94 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ù²èÝ±¸½ `0` | ×²Ì²À `1` | à«õÙ±¸½ `2` | îñÙ±¸½ `3` | óÙ¾Ù¸ `4` | õÜè±¸½ `5` | ãÙàª²ã `6` | ãÙàª²ã `6` |
| 1 | ù²èÝ±¸½ `0` | ×²Ì²À `1` | à«õÙ±¸½ `2` | îñÙ±¸½ `3` | óÙ¾Ù¸ `4` | õÜè±¸½ `5` | ãÙàª²ã `6` | ãÙàª²ã `6` |
| 2 | ù²èÝ±¸½ `0` | ×²Ì²À `1` | à«õÙ±¸½ `2` | îñÙ±¸½ `3` | óÙ¾Ù¸ `4` | õÜè±¸½ `5` | ãÙàª²ã `6` | ãÙàª²ã `6` |
| 3 | ù²èÝ±¸½ `0` | ×²Ì²À `1` | à«õÙ±¸½ `2` | îñÙ±¸½ `3` | óÙ¾Ù¸ `4` | õÜè±¸½ `5` | ãÙàª²ã `6` | ãÙàª²ã `6` |
| 4 | ù²èÝ±¸½ `0` | ×²Ì²À `1` | à«õÙ±¸½ `2` | îñÙ±¸½ `3` | óÙ¾Ù¸ `4` | õÜè±¸½ `5` | ãÙàª²ã `6` | ãÙàª²ã `6` |

### Zone 0, sous-zone 95 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Ìª±ØÛ¯ï `0` | ÙÛ¯ï `1` | ïÚ²ÝÛ¯ï `2` | ²¼­ÀÙÛ¯ï `3` | ´ÝçªÙÛ¯ï `4` | ´ÝçªÙÛ¯ï `4` | ±Ù¹Ð½ÄÛ¯ï `5` | ±Ù¹Ð½ÄÛ¯ï `5` |
| 1 | Ìª±ØÛ¯ï `0` | ÙÛ¯ï `1` | ïÚ²ÝÛ¯ï `2` | ²¼­ÀÙÛ¯ï `3` | ´ÝçªÙÛ¯ï `4` | ´ÝçªÙÛ¯ï `4` | ±Ù¹Ð½ÄÛ¯ï `5` | ±Ù¹Ð½ÄÛ¯ï `5` |
| 2 | Ìª±ØÛ¯ï `0` | ÙÛ¯ï `1` | ïÚ²ÝÛ¯ï `2` | ²¼­ÀÙÛ¯ï `3` | ´ÝçªÙÛ¯ï `4` | ´ÝçªÙÛ¯ï `4` | ±Ù¹Ð½ÄÛ¯ï `5` | ±Ù¹Ð½ÄÛ¯ï `5` |
| 3 | Ìª±ØÛ¯ï `0` | ÙÛ¯ï `1` | ïÚ²ÝÛ¯ï `2` | ²¼­ÀÙÛ¯ï `3` | ´ÝçªÙÛ¯ï `4` | ´ÝçªÙÛ¯ï `4` | ±Ù¹Ð½ÄÛ¯ï `5` | ±Ù¹Ð½ÄÛ¯ï `5` |
| 4 | Ìª±ØÛ¯ï `0` | ÙÛ¯ï `1` | ïÚ²ÝÛ¯ï `2` | ²¼­ÀÙÛ¯ï `3` | ´ÝçªÙÛ¯ï `4` | ´ÝçªÙÛ¯ï `4` | ±Ù¹Ð½ÄÛ¯ï `5` | ±Ù¹Ð½ÄÛ¯ï `5` |

### Zone 0, sous-zone 96 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Ð×±Ï `0` | ò×¯¸±Ï `1` | ÓÝÒ²Ù `2` | ´Ý¼ªÝÄ±Ï `3` | î¨ð²Ý±Ï `4` | î¨ð²Ý±Ï `4` | õÜè±Ï `5` | õÜè±Ï `5` |
| 1 | Ð×±Ï `0` | ò×¯¸±Ï `1` | ÓÝÒ²Ù `2` | ´Ý¼ªÝÄ±Ï `3` | î¨ð²Ý±Ï `4` | î¨ð²Ý±Ï `4` | õÜè±Ï `5` | õÜè±Ï `5` |
| 2 | Ð×±Ï `0` | ò×¯¸±Ï `1` | ÓÝÒ²Ù `2` | ´Ý¼ªÝÄ±Ï `3` | î¨ð²Ý±Ï `4` | î¨ð²Ý±Ï `4` | õÜè±Ï `5` | õÜè±Ï `5` |
| 3 | Ð×±Ï `0` | ò×¯¸±Ï `1` | ÓÝÒ²Ù `2` | ´Ý¼ªÝÄ±Ï `3` | î¨ð²Ý±Ï `4` | î¨ð²Ý±Ï `4` | õÜè±Ï `5` | õÜè±Ï `5` |
| 4 | Ð×±Ï `0` | ò×¯¸±Ï `1` | ÓÝÒ²Ù `2` | ´Ý¼ªÝÄ±Ï `3` | î¨ð²Ý±Ï `4` | î¨ð²Ý±Ï `4` | õÜè±Ï `5` | õÜè±Ï `5` |

### Zone 0, sous-zone 97 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ØÌÚ¯¸½ `0` | î¨ð²Ýáï `1` | ¸áï `2` | ¿Ùáï `3` | ´ÚÒÝÀÙáï `4` | Á¬èáï `5` | Á¬èáï `5` | Á¬èáï `5` |
| 1 | ØÌÚ¯¸½ `0` | î¨ð²Ýáï `1` | ¸áï `2` | ¿Ùáï `3` | ´ÚÒÝÀÙáï `4` | Á¬èáï `5` | Á¬èáï `5` | Á¬èáï `5` |
| 2 | ØÌÚ¯¸½ `0` | î¨ð²Ýáï `1` | ¸áï `2` | ¿Ùáï `3` | ´ÚÒÝÀÙáï `4` | Á¬èáï `5` | Á¬èáï `5` | Á¬èáï `5` |
| 3 | ØÌÚ¯¸½ `0` | î¨ð²Ýáï `1` | ¸áï `2` | ¿Ùáï `3` | ´ÚÒÝÀÙáï `4` | Á¬èáï `5` | Á¬èáï `5` | Á¬èáï `5` |
| 4 | ØÌÚ¯¸½ `0` | î¨ð²Ýáï `1` | ¸áï `2` | ¿Ùáï `3` | ´ÚÒÝÀÙáï `4` | Á¬èáï `5` | Á¬èáï `5` | Á¬èáï `5` |

### Zone 0, sous-zone 98 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ÙÛò `0` | îÓÝÛò `1` | ¼¬ï³Ûò `2` | Ì§ÝÄÑÛò `3` | ´Ý¼ªÝÄÛò `4` | ´Ý¼ªÝÄÛò `4` | ½ÛÝèÛò `5` | ½ÛÝèÛò `5` |
| 1 | ÙÛò `0` | îÓÝÛò `1` | ¼¬ï³Ûò `2` | Ì§ÝÄÑÛò `3` | ´Ý¼ªÝÄÛò `4` | ´Ý¼ªÝÄÛò `4` | ½ÛÝèÛò `5` | ½ÛÝèÛò `5` |
| 2 | ÙÛò `0` | îÓÝÛò `1` | ¼¬ï³Ûò `2` | Ì§ÝÄÑÛò `3` | ´Ý¼ªÝÄÛò `4` | ´Ý¼ªÝÄÛò `4` | ½ÛÝèÛò `5` | ½ÛÝèÛò `5` |
| 3 | ÙÛò `0` | îÓÝÛò `1` | ¼¬ï³Ûò `2` | Ì§ÝÄÑÛò `3` | ´Ý¼ªÝÄÛò `4` | ´Ý¼ªÝÄÛò `4` | ½ÛÝèÛò `5` | ½ÛÝèÛò `5` |
| 4 | ÙÛò `0` | îÓÝÛò `1` | ¼¬ï³Ûò `2` | Ì§ÝÄÑÛò `3` | ´Ý¼ªÝÄÛò `4` | ´Ý¼ªÝÄÛò `4` | ½ÛÝèÛò `5` | ½ÛÝèÛò `5` |

### Zone 0, sous-zone 99 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Ý±Ð­Ú¯Ä `0` | ×²Ä±Ð­Ú¯Ä `1` | ÷ØÝ½õÝÀ¸Ù `2` | Ïç¯¸õÝÀ¸Ù `3` | ÙÀØ½ÏÝ `4` | Ð½ÄøÝëÝÄ `5` | ÷ØÝ¾½øÝëÝÄ `6` | Ú¯ïö±½ `7` |
| 1 | òÙö±½ `8` | ³¨¯ÁÊ¯Ä `9` | ´ÙàÝÊ¯Ä `10` | ÊÝÃ¨Ýã·¬¯÷ `11` | Ìªæ¼®Ù `12` | ±¸±¼®Ù `13` | ¼¬ï³óÙ `14` | Ð½Ã¨¯¸óÙ `15` |
| 2 | ñ­Ã¨óÙ `16` | ½ÀÃ¨±× `17` | ½Ã¨±× `18` | ¿Ù¸×³Ý `19` | Ý¸×³Ý `20` | Ä©ÙÐ× `21` | ³«ÀÐ× `22` | ÷ÛÃ¸ÄØÝã `23` |
| 3 | ØÌÚ¸ÄØÝã `24` | Úç½ÄØÝã `25` | ïÛ÷ÆÙ `26` | ¿ÛÓÝèØÝã `27` | ´ÚÒÝÀÙØÝã `28` | »ÀÝØÝã `29` | Ì¨Û¿ÊØÝã `30` | »¶èôÝ `31` |
| 4 | ØÌÚ¸ÄØÝã `24` | Úç½ÄØÝã `25` | ïÛ÷ÆÙ `26` | ¿ÛÓÝèØÝã `27` | ´ÚÒÝÀÙØÝã `28` | »ÀÝØÝã `29` | Ì¨Û¿ÊØÝã `30` | »¶èôÝ `31` |

### Zone 0, sous-zone 100 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | î½Ï½¶Ú²ï `0` | ëÐïÙ `1` | ëÝ¼ÝãïÙ `2` | ¸Û½ `3` | ïÐÆµÝÌªæ `4` | ÐÉÀ³Û½ÎÝ `5` | îñÙÎÝ `6` | îñÙÎÝ `6` |
| 1 | ðçØ½¸½¹²Ù `7` | ÷Úî¨¸¼®Ý÷ÚÄ `8` | ãÚ²±¸ `9` | òØ¼ÝãÈ¯¸Ú½ `10` | ¼ÙðÍÙÑ `11` | ¼ÙðáÝÄÚ¯Ä `12` | ÝãÛò `13` | ½÷ØÝã¹÷ `14` |
| 2 | ðçØ½¸½¹²Ù `7` | ÷Úî¨¸¼®Ý÷ÚÄ `8` | ãÚ²±¸ `9` | òØ¼ÝãÈ¯¸Ú½ `10` | ¼ÙðÍÙÑ `11` | ¼ÙðáÝÄÚ¯Ä `12` | ÝãÛò `13` | ½÷ØÝã¹÷ `14` |
| 3 | »Ï¹÷ `15` | ÀÑ¹÷ `16` | ³¨ÝÀ¹÷ `17` | ´ÚÒÝÀÙÏÝÄ `18` | ´ÙòÝòÂ `19` | ³²ÝãòÂ `20` | ÌÛÄ¼­è `21` | Ìªæ¼­è `22` |
| 4 | »Ï¹÷ `15` | ÀÑ¹÷ `16` | ³¨ÝÀ¹÷ `17` | ´ÚÒÝÀÙÏÝÄ `18` | ´ÙòÝòÂ `19` | ³²ÝãòÂ `20` | ÌÛÄ¼­è `21` | Ìªæ¼­è `22` |

### Zone 0, sous-zone 101 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ¸Ø½ÀÙµò `0` | ½µò `1` | ÌØèµò `2` | Ì§²±µò `3` | »Ýëµò `4` | ×²Äµò `5` | ¸µò `6` | Øµò `7` |
| 1 | ²ñÙµò `8` | ´ÚÒÝÀÙµò `9` | Ú²Ýôµò `10` | Ì§ÝÀèÑµò `11` | ´Åç¸Ø½ÀÙ `12` | Ì§²±ç­´Ù `13` | ±²½ç­´Ù `14` | ½ç­´Ù `15` |
| 2 | ³²Ýïç­´Ù `16` | ØçªÑ `17` | Ø¶ðºÛÝ `18` | ØÝãºÛÝ `19` | ´Ý÷Ú½ºÛÝ `20` | Ìª±ØºÛÝ `21` | ½öïºÛÝ `22` | î¨½Ã¨Æº²Ý `23` |
| 3 | Ð½ÃØ¸Û¯¸ `24` | Ì«Á­Ý¸Û¯¸ `25` | ´ÅçôÄÙ `26` | Ø×Ý÷ `27` | Ã×¶ï `28` | ÀÛ¯Ä¶ï `29` | Ù½ÄÝ `30` | Ïç¶Ù½ÄÝ `31` |
| 4 | Ú¯ï±¯¼­ `32` | ãÚ²±¯¼­ `33` | ÎÜ²Ä±¯¼­ `34` | òÙ±¯¼­ `35` | ãØÝ±¯¼­ `36` | ò×¯¸±¯¼­ `37` | ÌªæøÝ `38` | ò×¯ïøÝ `39` |

### Zone 0, sous-zone 102 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Ù¼Ì§³²Ýã `0` | ¾×Ì¨Ñ³²Ýã `1` | Ìª±Ø³²Ýã `2` | ç¬¯çð×Ý½ `3` | »²ÚÝÄô¯¸½ `4` | ç­´Ùô¯¸½ `5` | õÝï×ô¯¸½ `6` | ò·¬Ýî¨ `7` |
| 1 | îØ¼¬½ïÛ¯÷ `8` | Ìª±ØïÛ¯÷ `9` | ÙÆïÙ `10` | ò×¯ï´·½ `11` | ½öØ¯Ä´·½ `12` | Ì§²Ýµ²Ù `13` | ò×¯ïµ²Ù `14` | ØÀÝÛ÷ `15` |
| 2 | Ð×¸Ùõ³ë `16` | Ìª±Øõ³ë `17` | Ú¯ï½õ²½ `18` | »¶ï×¯ã `19` | Ùù¼®Ý `20` | ×²Ìù¼®Ý `21` | Ï²Ýïù¼®Ý `22` | ³¨æïù¼®Ý `23` |
| 3 | ´Ø¸¼Ù `24` | ±ÝòÛ¼± `25` | ·­±ù¼®Ý `26` | Ø³«À `27` | ÷Ø­Ä `28` | ³«ÀÏÃØ±Ù `29` | ½ÏÃØ±Ù `30` | ÌÚ²ÑÏÃØ±Ù `31` |
| 4 | ³¨ÝïÏÃØ±Ù `32` | Ïç¯¸ÏÃØ±Ù `33` | ¸ÏÃØ±Ù `34` | ×²ÄÏÃØ±Ù `35` | ØÏÃØ±Ù `36` | ²ñÙÏÃØ±Ù `37` | ÙÆïÙ `38` | Ùù¼®Ý `39` |

### Zone 0, sous-zone 103 — repertoire de 3168 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | ØÌÚ¸Ä¼Ùï `0` | ½¼Ùï `1` | Ù¼Ì§¼Ùï `2` | ¾×Ì¨Ñ¼Ùï `3` | ´±¼Ùï `4` | ±²½¼Ùï `5` | ÌÚ²Ñ¼Ùï `6` | ÌÚ²Ñ¼Ùï `6` |
| 1 | ØÌÚ¸Ä¼Ùï `0` | ½¼Ùï `1` | Ù¼Ì§¼Ùï `2` | ¾×Ì¨Ñ¼Ùï `3` | ´±¼Ùï `4` | ±²½¼Ùï `5` | ÌÚ²Ñ¼Ùï `6` | ÌÚ²Ñ¼Ùï `6` |
| 2 | ØÌÚ¸Ä¼Ùï `0` | ½¼Ùï `1` | Ù¼Ì§¼Ùï `2` | ¾×Ì¨Ñ¼Ùï `3` | ´±¼Ùï `4` | ±²½¼Ùï `5` | ÌÚ²Ñ¼Ùï `6` | ÌÚ²Ñ¼Ùï `6` |
| 3 | ØÌÚ¸Ä¼Ùï `0` | ½¼Ùï `1` | Ù¼Ì§¼Ùï `2` | ¾×Ì¨Ñ¼Ùï `3` | ´±¼Ùï `4` | ±²½¼Ùï `5` | ÌÚ²Ñ¼Ùï `6` | ÌÚ²Ñ¼Ùï `6` |
| 4 | ØÌÚ¸Ä¼Ùï `0` | ½¼Ùï `1` | Ù¼Ì§¼Ùï `2` | ¾×Ì¨Ñ¼Ùï `3` | ´±¼Ùï `4` | ±²½¼Ùï `5` | ÌÚ²Ñ¼Ùï `6` | ÌÚ²Ñ¼Ùï `6` |

### Zone 0, sous-zone 104 — repertoire de 4104 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 255 | 0x4000 | `00 00 00 00 00 ff 00 40` |
| 1 | 1 | 1 | 255 | 0x4000 | `01 01 00 00 01 ff 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **åòØÝ** | 20 | **600** | **8** | 2 | 15 | 0 | 4 | `19 18 16 16 8 8 8 6` |
| 1 | **åòØÝ¼¬ÏÝ** | 16 | **33** | **70** | 3 | 17 | 0 | 800 | `12 10 10 12 16 16 17 16` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1290 | 436 | 1479 | 0 | 0x00050b00 |
| 0 | 1 | 0 | -706 | 436 | 1498 | 0 | 0x00050b00 |
| 0 | 2 | 0 | -1000 | 400 | 1607 | 0 | 0x00050b00 |
| 0 | 3 | 0 | -698 | 436 | 1306 | 0 | 0x00050b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | î½$Ï½¶Ú²ï `0` | ýòÂ `1` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` |
| 1 | î½$Ï½¶Ú²ï `0` | ýòÂ `1` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` |
| 2 | î½$Ï½¶Ú²ï `0` | ýòÂ `1` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` |
| 3 | î½$Ï½¶Ú²ï `0` | ýòÂ `1` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` |
| 4 | î½$Ï½¶Ú²ï `0` | ýòÂ `1` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` | Ð×¸Ù¸Ù$õ³õ³ë `2` |

## Zone 1 — 2964 octets, 10 sous-zone(s)

### Zone 1, sous-zone 0 — repertoire de 14356 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2 | 0x4000 | `00 00 00 00 00 02 00 40` |
| 1 | 1 | 1 | 3 | 0x4000 | `01 01 00 00 01 03 00 40` |
| 2 | 3 | 2 | 4 | 0x4000 | `02 03 00 00 02 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 20 | **38** | **8** | 2 | 20 | 0 | 74 | `18 16 14 14 8 10 10 10` |
| 1 | **Goblin-sorcier** | 24 | **35** | **70** | 3 | 24 | 0 | 66 | `15 16 10 10 16 19 14 14` |
| 2 | **Chauve-souris** | 15 | **36** | **2** | 4 | 15 | 2 | 66 | `18 18 14 14 10 14 10 10` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 1506 | -215 | -2350 | 0 | 0x00090b00 |
| 0 | 1 | 0 | 1701 | -215 | -3436 | 0 | 0x00090b00 |
| 0 | 2 | 0 | 938 | -415 | -2808 | 0 | 0x00090b00 |
| 0 | 3 | 1 | 1119 | -415 | -2527 | 0 | 0x00040b01 |
| 0 | 4 | 1 | 577 | -615 | -2488 | 0 | 0x00040b01 |
| 1 | 0 | 0 | -361 | -215 | 2995 | 0 | 0x000a0b00 |
| 1 | 1 | 0 | 152 | -215 | 3698 | 0 | 0x000a0b00 |
| 1 | 2 | 0 | 247 | -215 | 3698 | 0 | 0x000a0b00 |
| 1 | 3 | 0 | 431 | -215 | 3232 | 0 | 0x000a0b00 |
| 1 | 4 | 1 | 19 | -215 | 3318 | 0 | 0x00050b01 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Baguette d'argent `0` | Baguette de la féerie `1` | Porte-bonheur `2` | Parfum de feu `3` | Aile de fée `4` | Aile de fée `4` | Poudre de fées `5` | Poudre de fées `5` |
| 1 | Potion med `6` | Potion med `6` | Elixir des esprits `7` | Elixir des esprits `7` | Bonbon aux plantes `8` | Gantelets `9` | Hache fine `10` | Lampe sacrée `11` |
| 2 | Casse-tête à chaîne `12` | Casse-tête à chaîne `12` | Lavande `13` | Ecu du destin `14` | Tablier de cuir `15` | Tablier de cuir `15` | Cotte de maille `16` | Cotte de maille `16` |
| 3 | Sceptre de la mort `17` | Baguette aux joyaux `18` | Sauge `19` | Verveine `20` | Mercuriale `21` | Fenouil `22` | Ecu du destin `14` | Balle de cristal `23` |
| 4 | Potion med `6` | Potion med `6` | Potion med `6` | Potion med `6` | Potion med `6` | Huile de sang `24` | Huile de sang `24` | Huile de sang `24` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 228 | objet | rangee 0 : Baguette d'argent (25 %), Baguette de la féerie (17 %), Porte-bonheur (15 %), Parfum de feu (13 %), Aile de fée (11 %), Aile de fée (9 %), Poudre de fées (6 %), Poudre de fées (4 %) | -2878 / -980 / 3678 |
| 1.0 | coffre `22` | 229 | objet | rangee 1 : Potion med (25 %), Potion med (17 %), Elixir des esprits (15 %), Elixir des esprits (13 %), Bonbon aux plantes (11 %), Gantelets (9 %), Hache fine (6 %), Lampe sacrée (4 %) | 20 / -180 / 3428 |

### Zone 1, sous-zone 1 — repertoire de 14212 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 4 | 0x4000 | `00 00 00 00 00 04 00 40` |
| 1 | 1 | 1 | 5 | 0x4000 | `01 01 00 00 01 05 00 40` |
| 2 | 3 | 2 | 6 | 0x4000 | `02 03 00 00 02 06 00 40` |
| 3 | 2 | 3 | 7 | 0x4000 | `03 02 00 00 03 07 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 20 | **38** | **8** | 2 | 20 | 0 | 64 | `18 16 14 14 8 10 10 10` |
| 1 | **Goblin-sorcier** | 24 | **35** | **70** | 3 | 24 | 0 | 66 | `15 16 10 10 16 19 14 14` |
| 2 | **Chauve-souris** | 15 | **36** | **2** | 4 | 15 | 2 | 66 | `18 18 14 14 10 14 10 10` |
| 3 | **Chef-Goblin** | 36 | **62** | **25** | 63 | 36 | 4 | 66 | `35 28 34 32 22 23 20 20` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -1875 | 605 | 5057 | 0 | 0x00060b01 |
| 0 | 1 | 1 | -1628 | 605 | 5038 | 0 | 0x00060b01 |
| 0 | 2 | 3 | -1496 | 605 | 5349 | 0 | 0x00060b03 |
| 1 | 0 | 0 | 2823 | 5 | -5669 | 0 | 0x00070b00 |
| 1 | 1 | 0 | 3164 | 5 | -5548 | 0 | 0x00070b00 |
| 1 | 2 | 3 | 2923 | 5 | -5449 | 0 | 0x00070b03 |
| 2 | 0 | 2 | -1960 | 5 | -155 | 0 | 0x00080b02 |
| 2 | 1 | 2 | -1172 | 5 | 539 | 0 | 0x00080b02 |
| 2 | 2 | 2 | -307 | 5 | 4 | 0 | 0x00080b02 |
| 2 | 3 | 2 | 588 | 5 | -1532 | 0 | 0x00080b02 |
| 2 | 4 | 2 | -1513 | 205 | 1946 | 0 | 0x00080b02 |
| 2 | 5 | 2 | 316 | -595 | 2326 | 0 | 0x00080b02 |
| 2 | 6 | 3 | 1079 | 5 | -746 | 0 | 0x00080b03 |
| 2 | 7 | 3 | 75 | -595 | 2300 | 0 | 0x00080b03 |
| 2 | 8 | 3 | -1649 | 205 | 2166 | 0 | 0x00080b03 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 10 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Potion med `0` | Potion med `0` | Balle de feu `1` | Balle de terre `2` | Gants de force `3` | Gants de force `3` | Chaîne de la terre `4` | Chaîne de la terre `4` |
| 1 | Sceptre de la mort `5` | Sceptre de la mort `5` | Sceptre de la mort `5` | Casque de cuir `6` | Casque de cuir `6` | Amulette de la lumière `7` | Amulette de la lumière `7` | Amulette de la lumière `7` |
| 2 | Médaillon de bronze `8` | Médaillon de bronze `8` | Médaillon d'argent `9` | Médaillon d'argent `9` | Médaillon d'argent `9` | Médaillon d'or `10` | Médaillon d'or `10` | Médaillon d'or `10` |
| 3 | Robe fine `11` | Robe fine `11` | Robe fine `11` | Robe fine `11` | Robe fine `11` | Robe fine `11` | Robe fine `11` | Robe fine `11` |
| 4 | Casse-tête à chaîne `12` | Casse-tête à chaîne `12` | Lavande `13` | Ecu du destin `14` | Tablier de cuir `15` | Tablier de cuir `15` | Cotte de maille `16` | Cotte de maille `16` |
| 5 | Sceptre de la mort `5` | Baguette aux joyaux `17` | Sauge `18` | Verveine `19` | Mercuriale `20` | Fenouil `21` | Ecu du destin `14` | Balle de cristal `22` |
| 6 | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Huile de sang `23` | Huile de sang `23` | Huile de sang `23` |
| 7 | Longue épée `24` | Gantelets `25` | Bouclier rond `26` | Bouclier rond `26` | Gantelets de chevaliers `27` | Gantelets de chevaliers `27` | Gantelets d'argent `28` | Gantelets d'argent `28` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 240 | message | — | 3434 / 40 / -906 |
| 0.1 | coffre `22` | 241 | objet | rangee 1 : Sceptre de la mort (25 %), Sceptre de la mort (17 %), Sceptre de la mort (15 %), Casque de cuir (13 %), Casque de cuir (11 %), Amulette de la lumière (9 %), Amulette de la lumière (6 %), Amulette de la lumière (4 %) | -1700 / 640 / 5230 |
| 0.2 | coffre `22` | 231 | objet | rangee 2 : Médaillon de bronze (25 %), Médaillon de bronze (17 %), Médaillon d'argent (15 %), Médaillon d'argent (13 %), Médaillon d'argent (11 %), Médaillon d'or (9 %), Médaillon d'or (6 %), Médaillon d'or (4 %) | 2014 / 140 / -3620 |
| 1.0 | coffre `22` | 255 | objet | rangee 0 : Potion med (25 %), Potion med (17 %), Balle de feu (15 %), Balle de terre (13 %), Gants de force (11 %), Gants de force (9 %), Chaîne de la terre (6 %), Chaîne de la terre (4 %) | -2037 / 240 / 1168 |
| 1.20 | coffre `22` | 261 | objet | rangee 1 : Sceptre de la mort (25 %), Sceptre de la mort (17 %), Sceptre de la mort (15 %), Casque de cuir (13 %), Casque de cuir (11 %), Amulette de la lumière (9 %), Amulette de la lumière (6 %), Amulette de la lumière (4 %) | -2300 / 40 / 400 |

### Zone 1, sous-zone 2 — repertoire de 18788 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 0 | 3 | 0x4000 | `00 01 00 00 00 03 00 40` |
| 1 | 3 | 1 | 4 | 0x4000 | `01 03 00 00 01 04 00 40` |
| 2 | 6 | 2 | 5 | 0x4000 | `02 06 00 00 02 05 00 40` |
| 3 | 2 | 3 | 6 | 0x4000 | `03 02 00 00 03 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin-sorcier** | 24 | **35** | **70** | 3 | 24 | 0 | 66 | `15 16 10 10 16 19 14 14` |
| 1 | **Chauve-souris** | 15 | **36** | **2** | 4 | 15 | 2 | 66 | `18 18 14 14 10 14 10 10` |
| 2 | **Vipère** | 18 | **80** | **14** | 5 | 8 | 2 | 64 | `26 24 28 22 23 20 15 15` |
| 3 | **Chef-Goblin** | 36 | **62** | **25** | 63 | 36 | 4 | 66 | `35 28 24 26 19 18 18 20` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2631 | -595 | 1206 | 0 | 0x00060b00 |
| 0 | 1 | 0 | 2498 | -595 | 1054 | 0 | 0x00060b00 |
| 0 | 2 | 0 | 2840 | -595 | 1187 | 0 | 0x00060b00 |
| 0 | 3 | 0 | 2935 | -595 | 826 | 0 | 0x00060b00 |
| 0 | 4 | 3 | 2796 | -595 | 1067 | 0 | 0x00060b03 |
| 1 | 0 | 2 | -972 | 5 | 1493 | 0 | 0x00070b02 |
| 1 | 1 | 2 | -2709 | 5 | 1802 | 0 | 0x00070b02 |
| 1 | 2 | 2 | -3502 | 5 | 2648 | 0 | 0x00070b02 |
| 1 | 3 | 2 | -3199 | 5 | 3690 | 0 | 0x00070b02 |
| 1 | 4 | 2 | -2175 | 5 | 3423 | 0 | 0x00070b02 |
| 1 | 5 | 2 | -651 | 5 | 3464 | 0 | 0x00070b02 |
| 2 | 0 | 1 | -2022 | -195 | -1602 | 0 | 0x00080b01 |
| 2 | 1 | 1 | -3289 | -195 | -1422 | 0 | 0x00080b01 |
| 2 | 2 | 1 | -2819 | -195 | -2161 | 0 | 0x00080b01 |
| 2 | 3 | 1 | -2606 | -195 | -567 | 0 | 0x00080b01 |
| 2 | 4 | 1 | -1765 | -195 | -92 | 0 | 0x00080b01 |
| 2 | 5 | 1 | -1068 | -195 | -716 | 0 | 0x00080b01 |
| 2 | 6 | 1 | -2171 | -195 | -1288 | 0 | 0x00080b01 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Bottes de cuir `0` | Bottes de cuir `0` | Manteau `1` | Manteau `1` | Joyau de terre `2` | Joyau de terre `2` | Armure `3` | Armure `3` |
| 1 | Balle de cristal `4` | Aile de Séraphin `5` | Aile de Séraphin `5` | Balle de cristal `4` | Balle de cristal `4` | Aile de Séraphin `5` | Aile de Séraphin `5` | Balle de cristal `4` |
| 2 | Bouclier rond `6` | Bouclier rond `6` | Bouclier rond `6` | Bouclier rond `6` | Bouclier rond `6` | Bouclier rond `6` | Bouclier rond `6` | Bouclier rond `6` |
| 3 | Sceptre de la mort `7` | Baguette aux joyaux `8` | Sauge `9` | Verveine `10` | Mercuriale `11` | Fenouil `12` | Ecu du destin `13` | Balle de cristal `4` |
| 4 | Potion med `14` | Potion med `14` | Potion med `14` | Potion med `14` | Potion med `14` | Huile de sang `15` | Huile de sang `15` | Huile de sang `15` |
| 5 | Potion olympique `16` | Potion olympique `16` | Elixir de vie `17` | Elixir de vie `17` | Elixir de vie `17` | Elixir de vie `17` | Elixir de vie `17` | Extrait de sang `18` |
| 6 | Longue épée `19` | Gantelets `20` | Bouclier rond `6` | Bouclier rond `6` | Gantelets de chevaliers `21` | Gantelets de chevaliers `21` | Gantelets d'argent `22` | Gantelets d'argent `22` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 242 | objet | rangee 0 : Bottes de cuir (25 %), Bottes de cuir (17 %), Manteau (15 %), Manteau (13 %), Joyau de terre (11 %), Joyau de terre (9 %), Armure (6 %), Armure (4 %) | 3122 / -560 / 861 |
| 1.0 | coffre `22` | -243 | objet | rangee 1 : Balle de cristal (25 %), Aile de Séraphin (17 %), Aile de Séraphin (15 %), Balle de cristal (13 %), Balle de cristal (11 %), Aile de Séraphin (9 %), Aile de Séraphin (6 %), Balle de cristal (4 %) | 492 / -560 / -2060 |
| 1.110 | coffre `22` | 262 | objet | rangee 2 : Bouclier rond (25 %), Bouclier rond (17 %), Bouclier rond (15 %), Bouclier rond (13 %), Bouclier rond (11 %), Bouclier rond (9 %), Bouclier rond (6 %), Bouclier rond (4 %) | -1300 / 40 / 2000 |

### Zone 1, sous-zone 3 — repertoire de 5484 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 3 | 1 | 2 | 0x4000 | `00 03 00 00 01 02 00 40` |
| 1 | 6 | 2 | 3 | 0x4000 | `01 06 00 00 02 03 00 40` |
| 2 | 5 | 0 | 4 | 0x4000 | `02 05 00 00 00 04 00 40` |
| 3 | 17 | 3 | 5 | 0x4000 | `03 11 00 00 03 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Scorpion** | 25 | **44** | **6** | 10 | 25 | 0 | 64 | `28 45 25 24 2 28 16 16` |
| 1 | **Chauve-souris** | 15 | **36** | **2** | 4 | 15 | 2 | 66 | `18 18 14 14 10 14 10 10` |
| 2 | **Vipère** | 18 | **80** | **14** | 5 | 18 | 2 | 64 | `26 24 28 22 23 20 15 15` |
| 3 | **Araignée** | 30 | **32** | **2** | 37 | 30 | 0 | 64 | `32 28 31 19 18 27 16 15` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | -1678 | 400 | -1685 | 0 | 0x00020b02 |
| 0 | 1 | 2 | -3095 | 400 | -842 | 0 | 0x00020b02 |
| 0 | 2 | 2 | -2788 | 400 | -2046 | 0 | 0x00020b02 |
| 0 | 3 | 2 | -1754 | 400 | -2215 | 0 | 0x00020b02 |
| 1 | 0 | 2 | 2286 | 1800 | -84 | 0 | 0x0000ff02 |
| 2 | 0 | 1 | 2286 | 1800 | -84 | 0 | 0x0000ff01 |
| 3 | 0 | 2 | 2286 | 1800 | -84 | 0 | 0x0000ff02 |
| 4 | 0 | 3 | 2286 | 1800 | -84 | 0 | 0x0000ff03 |
| 5 | 0 | 1 | 2300 | 1800 | -100 | 0 | 0x0000ff01 |
| 5 | 1 | 1 | 2300 | 1800 | 100 | 0 | 0x0000ff01 |
| 5 | 2 | 1 | 2300 | 1800 | -300 | 0 | 0x0000ff01 |
| 5 | 3 | 1 | 2100 | 1800 | -100 | 0 | 0x0000ff01 |
| 5 | 4 | 1 | 2500 | 1800 | -100 | 0 | 0x0000ff01 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 10 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 4 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Elixir de vie `0` | Ecailles de dragon `1` | Ecailles de dragon `1` | Ecailles de dragon `1` | Ecailles de dragon `1` | Chaîne de l'eau `2` | Chaîne de l'eau `2` | Chaîne de l'eau `2` |
| 1 | Joyau de vent `3` | Joyau de glace `4` | Joyau de vent `3` | Joyau de glace `4` | Délicieux drop `5` | Délicieux drop `5` | Ambrosia `6` | Ambrosia `6` |
| 2 | Potion med `7` | Potion med `7` | Potion med `7` | Potion med `7` | Potion med `7` | Huile de sang `8` | Huile de sang `8` | Huile de sang `8` |
| 3 | Potion olympique `9` | Potion olympique `9` | Elixir de vie `0` | Elixir de vie `0` | Elixir de vie `0` | Elixir de vie `0` | Elixir de vie `0` | Extrait de sang `10` |
| 4 | Potion olympique `9` | Minegosh `11` | Potion olympique `9` | Potion olympique `9` | Potion olympique `9` | Potion olympique `9` | Potion olympique `9` | Potion olympique `9` |
| 5 | Potion olympique `9` | Potion olympique `9` | Extrait d'esprits `12` | Extrait d'esprits `12` | Extrait d'esprits `12` | Extrait d'esprits `12` | Extrait d'esprits `12` | Extrait d'esprits `12` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 244 | objet | rangee 0 : Elixir de vie (25 %), Ecailles de dragon (17 %), Ecailles de dragon (15 %), Ecailles de dragon (13 %), Ecailles de dragon (11 %), Chaîne de l'eau (9 %), Chaîne de l'eau (6 %), Chaîne de l'eau (4 %) | 2400 / 840 / -2409 |
| 0.1 | coffre `22` | 245 | objet | rangee 1 : Joyau de vent (25 %), Joyau de glace (17 %), Joyau de vent (15 %), Joyau de glace (13 %), Délicieux drop (11 %), Délicieux drop (9 %), Ambrosia (6 %), Ambrosia (4 %) | -3048 / 240 / 4744 |

### Zone 1, sous-zone 4 — repertoire de 14460 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 5 | 0 | 9 | 0x4000 | `00 05 00 00 00 09 00 40` |
| 1 | 4 | 1 | 10 | 0x4000 | `01 04 00 00 01 0a 00 40` |
| 2 | 13 | 2 | 11 | 0x4000 | `02 0d 00 00 02 0b 00 40` |
| 3 | 17 | 3 | 12 | 0x4000 | `03 11 00 00 03 0c 00 40` |
| 4 | 11 | 4 | 13 | 0x4000 | `04 0b 00 00 04 0d 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Scorpion** | 25 | **44** | **6** | 10 | 25 | 0 | 64 | `28 45 25 24 2 28 16 16` |
| 1 | **Mille-patte** | 29 | **51** | **3** | 12 | 29 | 3 | 56 | `33 42 28 26 12 36 18 16` |
| 2 | **Jeune dragon** | 40 | **700** | **50** | 65 | 40 | 0 | 250 | `32 52 40 22 33 40 30 22` |
| 3 | **Araignée** | 30 | **32** | **2** | 37 | 37 | 0 | 64 | `32 28 31 19 18 27 16 15` |
| 4 | **Ours gris** | 54 | **81** | **19** | 6 | 54 | 3 | 62 | `73 67 59 60 57 50 52 51` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 450 | 0 | 1100 | 2048 | 0x001f0b02 |
| 1 | 0 | 1 | -637 | 0 | -1983 | 0 | 0x00200b01 |
| 1 | 1 | 1 | -376 | 0 | -1854 | 0 | 0x00200b01 |
| 2 | 0 | 4 | 1086 | 0 | 2444 | 0 | 0x00210b04 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Bague de protection `0` | Bague de résistance `1` | Aquachâle `2` | Aquachâle `2` | Ruban de force `3` | Nobles bottes `4` | Nobles bottes `4` | Nobles bottes `4` |
| 1 | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` |
| 2 | Robe épiscopale `6` | Robe épiscopale `6` | Robe épiscopale `6` | Robe épiscopale `6` | Robe épiscopale `6` | Robe épiscopale `6` | Robe épiscopale `6` | Robe épiscopale `6` |
| 3 | Manteau de brigand `7` | Manteau de brigand `7` | Manteau de brigand `7` | Manteau de brigand `7` | Manteau de brigand `7` | Manteau de brigand `7` | Manteau de brigand `7` | Manteau de brigand `7` |
| 4 | Robe magique `8` | Robe magique `8` | Robe magique `8` | Robe magique `8` | Robe magique `8` | Robe magique `8` | Robe magique `8` | Robe magique `8` |
| 5 | Manteau de cuir `9` | Manteau de cuir `9` | Manteau de cuir `9` | Manteau de cuir `9` | Manteau de cuir `9` | Manteau de cuir `9` | Manteau de cuir `9` | Manteau de cuir `9` |
| 6 | Manteau d'elfe `10` | Manteau d'elfe `10` | Manteau d'elfe `10` | Manteau d'elfe `10` | Manteau d'elfe `10` | Manteau d'elfe `10` | Manteau d'elfe `10` | Manteau d'elfe `10` |
| 7 | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` | Armure `5` |
| 8 | Robe épiscopale `6` | Robe magique `8` | Robe magique `8` | Robe épiscopale `6` | Robe épiscopale `6` | Robe magique `8` | Robe magique `8` | Robe épiscopale `6` |
| 9 | Potion olympique `11` | Minegosh `12` | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` |
| 10 | Potion olympique `11` | Potion olympique `11` | Epice rouge `13` | Epice rouge `13` | Epice rouge `13` | Elixir de vie `14` | Elixir de vie `14` | Elixir de vie `14` |
| 11 | Amulette du dragon `15` | Corne du dragon `16` | Balle de feu `17` | Queue de dragon `18` | Ecailles de dragon `19` | Armure du dragon `20` | Poignard d'Orihalcon `21` | Poignard d'Orihalcon `21` |
| 12 | Potion olympique `11` | Potion olympique `11` | Extrait d'esprits `22` | Extrait d'esprits `22` | Extrait d'esprits `22` | Extrait d'esprits `22` | Extrait d'esprits `22` | Extrait d'esprits `22` |
| 13 | Extrait de sang `23` | Extrait de sang `23` | Extrait de sang `23` | Extrait de sang `23` | Extrait de sang `23` | Extrait de sang `23` | Os de guerrier sauvage `24` | Os de guerrier sauvage `24` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 246 | objet | rangee 0 : Bague de protection (25 %), Bague de résistance (17 %), Aquachâle (15 %), Aquachâle (13 %), Ruban de force (11 %), Nobles bottes (9 %), Nobles bottes (6 %), Nobles bottes (4 %) | -1872 / 40 / -564 |
| 0.1 | coffre `22` | 247 | objet | rangee 1 : Armure (25 %), Armure (17 %), Armure (15 %), Armure (13 %), Armure (11 %), Armure (9 %), Armure (6 %), Armure (4 %) | 1503 / 40 / -565 |

### Zone 1, sous-zone 5 — repertoire de 5008 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 10 | 0 | 3 | 0x4000 | `00 0a 00 00 00 03 00 40` |
| 1 | 9 | 1 | 4 | 0x4000 | `01 09 00 00 01 04 00 40` |
| 2 | 8 | 2 | 5 | 0x4000 | `02 08 00 00 02 05 00 40` |
| 3 | 7 | 3 | 6 | 0x4000 | `03 07 00 00 03 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Crabe** | 72 | **88** | **60** | 45 | 60 | 1 | 55 | `68 90 68 64 12 68 43 41` |
| 1 | **Poisson tueur** | 68 | **81** | **35** | 50 | 43 | 0 | 50 | `72 24 62 64 67 56 52 51` |
| 2 | **Maudite** | 87 | **109** | **66** | 41 | 8 | 0 | 10 | `52 50 63 72 57 116 72 61` |
| 3 | **Belle bleue** | 61 | **85** | **13** | 7 | 55 | 0 | 40 | `56 190 51 52 51 60 60 60` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 2136 | 400 | 3419 | 0 | 0x00060b01 |
| 0 | 1 | 1 | 2493 | 400 | 3584 | 0 | 0x00060b01 |
| 0 | 2 | 1 | 2717 | 400 | 3475 | 0 | 0x00060b01 |
| 0 | 3 | 1 | 3044 | 400 | 3329 | 0 | 0x00060b01 |
| 1 | 0 | 2 | -4522 | 50 | -778 | 0 | 0x00070b02 |
| 1 | 1 | 2 | -4042 | 50 | -536 | 0 | 0x00070b02 |
| 1 | 2 | 2 | -3586 | 50 | -184 | 0 | 0x00070b02 |
| 1 | 3 | 2 | -2985 | 150 | 257 | 0 | 0x00070b02 |
| 1 | 4 | 2 | -2502 | 150 | 853 | 0 | 0x00070b02 |
| 1 | 5 | 2 | -2213 | 50 | 1340 | 0 | 0x00070b02 |
| 1 | 6 | 2 | -1940 | 50 | 1505 | 0 | 0x00070b02 |
| 2 | 0 | 0 | 505 | 0 | -508 | 0 | 0x00080b00 |
| 2 | 1 | 0 | -373 | 0 | -648 | 0 | 0x00080b00 |
| 2 | 2 | 0 | 1673 | 0 | -1519 | 0 | 0x00080b00 |
| 2 | 3 | 0 | 1929 | 0 | -994 | 0 | 0x00080b00 |
| 2 | 4 | 1 | -25 | -100 | -840 | 0 | 0x00060b01 |
| 2 | 5 | 1 | 980 | -100 | -470 | 0 | 0x00060b01 |
| 2 | 6 | 1 | 1200 | -100 | -1810 | 0 | 0x00060b01 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Pierre médicinale `0` | Pierre magique `1` | Chaussuresvol `2` | Boucle de cristal `3` | Boucle de cristal `3` | Chaussuresvol `2` | Pierre magique `1` | Pierre médicinale `0` |
| 1 | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` |
| 2 | Porte-bonheur `5` | Casque de fer `6` | Miroir de la porte `7` | Bottes de fer `8` | Bottes de fer `8` | Miroir de la porte `7` | Casque de fer `6` | Porte-bonheur `5` |
| 3 | Grande hache `9` | Potion olympique `10` | Cristal d'eau `11` | Cristal de terre `12` | Cristal de terre `12` | Cristal d'eau `11` | Panoramix `13` | Panoramix `13` |
| 4 | Potion med `14` | Potion med `14` | Cristal de terre `12` | Cristal d'air `15` | Cristal d'air `15` | Cristal de terre `12` | Panoramix `13` | Panoramix `13` |
| 5 | Cristal du mal `16` | Balle des ténèbres `17` | Cristal du mal `16` | Balle des ténèbres `17` | Balle des ténèbres `17` | Cristal du mal `16` | Balle des ténèbres `17` | Cristal du mal `16` |
| 6 | Potion med `14` | Potion med `14` | Potion med `14` | Potion med `14` | Elixir de vie `18` | Elixir de vie `18` | Elixir de vie `18` | Elixir de vie `18` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 232 | objet | rangee 0 : Pierre médicinale (25 %), Pierre magique (17 %), Chaussuresvol (15 %), Boucle de cristal (13 %), Boucle de cristal (11 %), Chaussuresvol (9 %), Pierre magique (6 %), Pierre médicinale (4 %) | 2492 / 440 / -2224 |
| 0.1 | coffre `22` | 233 | objet | rangee 1 : Ecu du destin (25 %), Ecu du destin (17 %), Ecu du destin (15 %), Ecu du destin (13 %), Ecu du destin (11 %), Ecu du destin (9 %), Ecu du destin (6 %), Ecu du destin (4 %) | -52 / 440 / -2574 |
| 0.2 | coffre `22` | 234 | objet | rangee 2 : Porte-bonheur (25 %), Casque de fer (17 %), Miroir de la porte (15 %), Bottes de fer (13 %), Bottes de fer (11 %), Miroir de la porte (9 %), Casque de fer (6 %), Porte-bonheur (4 %) | -1052 / 40 / 2400 |

### Zone 1, sous-zone 6 — repertoire de 6596 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 11 | 0 | 4 | 0x4000 | `00 0b 00 00 00 04 00 40` |
| 1 | 7 | 1 | 4 | 0x4000 | `01 07 00 00 01 04 00 40` |
| 2 | 10 | 2 | 5 | 0x4000 | `02 0a 00 00 02 05 00 40` |
| 3 | 8 | 3 | 6 | 0x4000 | `03 08 00 00 03 06 00 40` |
| 4 | 12 | 4 | 7 | 0x4000 | `04 0c 00 00 04 07 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Ours gris** | 54 | **81** | **19** | 6 | 54 | 3 | 62 | `73 67 68 64 57 50 42 41` |
| 1 | **Belle bleue** | 61 | **85** | **13** | 7 | 55 | 0 | 40 | `56 190 51 52 51 60 60 60` |
| 2 | **Crabe** | 82 | **88** | **60** | 45 | 60 | 1 | 55 | `68 90 68 64 12 68 43 41` |
| 3 | **Maudite** | 87 | **109** | **66** | 41 | 8 | 0 | 10 | `62 60 73 82 57 116 72 81` |
| 4 | **Salamandre** | 95 | **190** | **220** | 9 | 82 | 0 | 30 | `88 96 81 84 32 72 73 70` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 129 | 300 | -101 | 0 | 0x00060b01 |
| 0 | 1 | 1 | 293 | 300 | -265 | 0 | 0x00060b01 |
| 0 | 2 | 1 | 597 | 300 | -200 | 0 | 0x00060b01 |
| 0 | 3 | 1 | 658 | 300 | 45 | 0 | 0x00060b01 |
| 0 | 4 | 1 | 502 | 300 | 254 | 0 | 0x00060b01 |
| 0 | 5 | 1 | 232 | 300 | 252 | 0 | 0x00060b01 |
| 1 | 0 | 2 | -2279 | 700 | 620 | 0 | 0x00070b02 |
| 1 | 1 | 2 | -2521 | 700 | 1184 | 0 | 0x00070b02 |
| 1 | 2 | 2 | -2141 | 700 | 1374 | 0 | 0x00070b02 |
| 1 | 3 | 2 | -3024 | 700 | 2197 | 0 | 0x00070b02 |
| 1 | 4 | 2 | -2209 | 700 | 3241 | 0 | 0x00070b02 |
| 1 | 5 | 2 | -1390 | 700 | 2412 | 0 | 0x00070b02 |
| 1 | 6 | 2 | -3776 | 700 | 2949 | 0 | 0x00070b02 |
| 1 | 7 | 2 | -1576 | 700 | 1596 | 0 | 0x00070b02 |
| 2 | 0 | 2 | -2464 | 700 | -3189 | 0 | 0x00080b02 |
| 2 | 1 | 2 | -2138 | 700 | -3456 | 0 | 0x00080b02 |
| 2 | 2 | 2 | -1887 | 700 | -4029 | 0 | 0x00080b02 |
| 2 | 3 | 2 | -2324 | 700 | -3668 | 0 | 0x00080b02 |
| 2 | 4 | 2 | -2214 | 700 | -4659 | 0 | 0x00080b02 |
| 2 | 5 | 2 | -1682 | 700 | -4659 | 0 | 0x00080b02 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 3 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 4 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 5 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 7 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 4 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 7 | 5 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 8 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Talisman médicinal `0` | Bague sacrée `1` | Bouclier de glace `2` | Bouclier de glace `2` | Pendentif vaporeux `3` | Pendentif vaporeux `3` | Pendentif vaporeux `3` | Pendentif vaporeux `3` |
| 1 | Arc de bois `4` | Baguette-Misuril `5` | Baguette de la terre `6` | Hache empoisonnée `7` | Hache empoisonnée `7` | Baguette de la terre `6` | Baguette-Misuril `5` | Arc de bois `4` |
| 2 | Lame du soleil `8` | Crosse des anciens `9` | Black Jack `10` | Lame de la terre `11` | Lame de la terre `11` | Black Jack `10` | Crosse des anciens `9` | Lame du soleil `8` |
| 3 | Extrait de sang `12` | Extrait de sang `12` | Extrait de sang `12` | Extrait de sang `12` | Extrait de sang `12` | Extrait de sang `12` | Os de guerrier sauvage `13` | Os de guerrier sauvage `13` |
| 4 | Potion med `14` | Potion med `14` | Potion med `14` | Potion med `14` | Elixir de vie `15` | Elixir de vie `15` | Elixir de vie `15` | Elixir de vie `15` |
| 5 | Grande hache `16` | Potion olympique `17` | Cristal d'eau `18` | Cristal de terre `19` | Cristal de terre `19` | Cristal d'eau `18` | Panoramix `20` | Panoramix `20` |
| 6 | Cristal du mal `21` | Balle des ténèbres `22` | Cristal du mal `21` | Balle des ténèbres `22` | Balle des ténèbres `22` | Cristal du mal `21` | Balle des ténèbres `22` | Cristal du mal `21` |
| 7 | Joyau de glace `23` | Balle de gel `24` | Saphir `25` | Forgée de glace `26` | Poignard des brouillards `27` | Poignard des brouillards `27` | Hache du gel `28` | Hache du gel `28` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.0 | coffre `22` | 235 | objet | rangee 0 : Talisman médicinal (25 %), Bague sacrée (17 %), Bouclier de glace (15 %), Bouclier de glace (13 %), Pendentif vaporeux (11 %), Pendentif vaporeux (9 %), Pendentif vaporeux (6 %), Pendentif vaporeux (4 %) | -86 / 40 / -1230 |
| 1.1 | coffre `22` | 236 | objet | rangee 1 : Arc de bois (25 %), Baguette-Misuril (17 %), Baguette de la terre (15 %), Hache empoisonnée (13 %), Hache empoisonnée (11 %), Baguette de la terre (9 %), Baguette-Misuril (6 %), Arc de bois (4 %) | -4235 / 740 / 3289 |
| 2.0 | coffre `22` | 237 | objet | rangee 2 : Lame du soleil (25 %), Crosse des anciens (17 %), Black Jack (15 %), Lame de la terre (13 %), Lame de la terre (11 %), Black Jack (9 %), Crosse des anciens (6 %), Lame du soleil (4 %) | -2192 / 740 / -4861 |

### Zone 1, sous-zone 7 — repertoire de 9472 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 11 | 0 | 3 | 0x4000 | `00 0b 00 00 00 03 00 40` |
| 1 | 7 | 1 | 4 | 0x4000 | `01 07 00 00 01 04 00 40` |
| 2 | 8 | 2 | 5 | 0x4000 | `02 08 00 00 02 05 00 40` |
| 3 | 14 | 3 | 6 | 0x4000 | `03 0e 00 00 03 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Ours gris** | 54 | **81** | **19** | 6 | 54 | 3 | 62 | `73 67 68 64 57 50 42 41` |
| 1 | **Belle bleue** | 61 | **85** | **13** | 7 | 55 | 0 | 40 | `56 190 51 52 51 60 60 60` |
| 2 | **Maudite** | 87 | **69** | **66** | 41 | 8 | 0 | 10 | `62 60 73 82 57 116 72 81` |
| 3 | **Ogre** | 96 | **315** | **60** | 57 | 63 | 0 | 40 | `85 51 61 62 8 67 69 66` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | -822 | 200 | -5261 | 0 | 0x00080b02 |
| 0 | 1 | 2 | -946 | 200 | -5800 | 0 | 0x00080b02 |
| 0 | 2 | 2 | -1508 | 200 | -5978 | 0 | 0x00080b02 |
| 0 | 3 | 2 | 818 | 200 | -6009 | 0 | 0x00080b02 |
| 0 | 4 | 2 | 555 | 200 | -4812 | 0 | 0x00080b02 |
| 0 | 5 | 2 | 324 | 200 | -5420 | 0 | 0x00080b02 |
| 1 | 0 | 1 | -4256 | 900 | 1200 | 0 | 0x00180b01 |
| 1 | 1 | 1 | -4530 | 1000 | 853 | 0 | 0x00180b01 |
| 1 | 2 | 1 | -4530 | 1000 | 853 | 0 | 0x00180b01 |
| 1 | 3 | 1 | -4274 | 800 | 400 | 0 | 0x00180b01 |
| 1 | 4 | 1 | -4409 | 500 | 656 | 0 | 0x00180b01 |
| 1 | 5 | 1 | -4409 | 500 | 656 | 0 | 0x00180b01 |
| 1 | 6 | 1 | -4140 | 900 | 200 | 0 | 0x00180b01 |
| 1 | 7 | 1 | -4375 | 400 | 1274 | 0 | 0x00180b01 |
| 1 | 8 | 1 | -4375 | 400 | 1274 | 0 | 0x00180b01 |
| 2 | 0 | 1 | -4308 | 400 | 285 | 0 | 0x00140b01 |
| 2 | 1 | 1 | -4047 | 1000 | 249 | 0 | 0x00140b01 |
| 2 | 2 | 1 | -4186 | 900 | 89 | 0 | 0x00140b01 |
| 2 | 3 | 1 | -3858 | 700 | 142 | 0 | 0x00140b01 |
| 2 | 4 | 1 | -3858 | 700 | 142 | 0 | 0x00140b01 |
| 3 | 0 | 0 | 1250 | 800 | -1364 | 0 | 0x000b0b00 |
| 3 | 1 | 0 | 234 | 800 | -1720 | 0 | 0x000b0b00 |
| 3 | 2 | 0 | 1877 | 1000 | -766 | 0 | 0x000b0b00 |
| 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 4 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 5 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure mirage `0` | Bouclier de vie `1` | Robe des esprits `2` | Bracelet d'argent `3` | Bracelet d'argent `3` | Robe des esprits `2` | Bouclier de vie `1` | Armure mirage `0` |
| 1 | Balle de l'illusion `4` | Lampe sacrée `5` | Lampe sacrée `5` | Balle de l'illusion `4` | Manteau de résistance `6` | Manteau de résistance `6` | Manteau de résistance `6` | Manteau de résistance `6` |
| 2 | Panoramix `7` | Ambrosia `8` | Ambrosia `8` | Panoramix `7` | Panoramix `7` | Ambrosia `8` | Ambrosia `8` | Panoramix `7` |
| 3 | Extrait de sang `9` | Extrait de sang `9` | Extrait de sang `9` | Extrait de sang `9` | Extrait de sang `9` | Extrait de sang `9` | Os de guerrier sauvage `10` | Os de guerrier sauvage `10` |
| 4 | Potion med `11` | Potion med `11` | Potion med `11` | Potion med `11` | Elixir de vie `12` | Elixir de vie `12` | Elixir de vie `12` | Elixir de vie `12` |
| 5 | Cristal du mal `13` | Balle des ténèbres `14` | Cristal du mal `13` | Balle des ténèbres `14` | Balle des ténèbres `14` | Cristal du mal `13` | Balle des ténèbres `14` | Cristal du mal `13` |
| 6 | Elixir de vie `12` | Ruban de force `15` | Crosse d'ambre `16` | Hache d'Ouga `17` | Cendre rouge `18` | Cendre rouge `18` | Cendre rouge `18` | Cendre rouge `18` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.2 | coffre `22` | 238 | objet | rangee 0 : Armure mirage (25 %), Bouclier de vie (17 %), Robe des esprits (15 %), Bracelet d'argent (13 %), Bracelet d'argent (11 %), Robe des esprits (9 %), Bouclier de vie (6 %), Armure mirage (4 %) | 3886 / 240 / -6076 |
| 0.3 | coffre `22` | 239 | objet | rangee 1 : Balle de l'illusion (25 %), Lampe sacrée (17 %), Lampe sacrée (15 %), Balle de l'illusion (13 %), Manteau de résistance (11 %), Manteau de résistance (9 %), Manteau de résistance (6 %), Manteau de résistance (4 %) | -3900 / -160 / -4872 |
| 0.4 | coffre `22` | 224 | objet | rangee 2 : Panoramix (25 %), Ambrosia (17 %), Ambrosia (15 %), Panoramix (13 %), Panoramix (11 %), Ambrosia (9 %), Ambrosia (6 %), Panoramix (4 %) | -4340 / 240 / 2896 |

### Zone 1, sous-zone 8 — repertoire de 7616 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 11 | 0 | 3 | 0x4000 | `00 0b 00 00 00 03 00 40` |
| 1 | 7 | 1 | 4 | 0x4000 | `01 07 00 00 01 04 00 40` |
| 2 | 8 | 2 | 5 | 0x4000 | `02 08 00 00 02 05 00 40` |
| 3 | 14 | 3 | 6 | 0x4000 | `03 0e 00 00 03 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Ours gris** | 54 | **81** | **19** | 6 | 54 | 3 | 62 | `73 67 68 64 57 50 42 41` |
| 1 | **Belle bleue** | 61 | **85** | **13** | 7 | 55 | 0 | 40 | `56 190 51 52 51 60 60 60` |
| 2 | **Maudite** | 87 | **109** | **66** | 41 | 8 | 0 | 10 | `62 60 73 82 57 116 72 81` |
| 3 | **Ogre** | 96 | **315** | **60** | 57 | 63 | 0 | 40 | `85 51 61 62 8 67 69 66` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -305 | 5 | 4990 | 0 | 0x000a0b01 |
| 0 | 1 | 1 | -737 | 5 | 4958 | 0 | 0x000a0b01 |
| 0 | 2 | 1 | -1292 | 5 | 5267 | 0 | 0x000a0b01 |
| 0 | 3 | 1 | -1765 | 5 | 5010 | 0 | 0x000a0b01 |
| 0 | 4 | 1 | -2594 | 5 | 4923 | 0 | 0x000a0b01 |
| 0 | 5 | 1 | -3338 | 5 | 4362 | 0 | 0x000a0b01 |
| 0 | 6 | 1 | -3891 | 5 | 4649 | 0 | 0x000a0b01 |
| 0 | 7 | 1 | -5946 | 5 | 4740 | 0 | 0x000a0b01 |
| 1 | 0 | 1 | 2855 | 100 | 3474 | 0 | 0x000b0b01 |
| 1 | 1 | 1 | 3328 | 0 | 3128 | 0 | 0x000b0b01 |
| 2 | 0 | 1 | 1998 | -1600 | 1601 | 0 | 0x000c0b01 |
| 2 | 1 | 1 | 1439 | -1600 | 1557 | 0 | 0x000c0b01 |
| 2 | 2 | 1 | 992 | -1600 | 1550 | 0 | 0x000c0b01 |
| 2 | 3 | 1 | 298 | -1600 | 1259 | 0 | 0x000c0b01 |
| 2 | 4 | 1 | 71 | -1600 | 1165 | 0 | 0x000c0b01 |
| 3 | 0 | 2 | -3125 | -1395 | -2215 | 0 | 0x000d0b02 |
| 3 | 1 | 2 | -2618 | -1395 | -2195 | 0 | 0x000d0b02 |
| 3 | 2 | 2 | -2616 | -1395 | -2870 | 0 | 0x000d0b02 |
| 3 | 3 | 2 | -2810 | -1395 | -3262 | 0 | 0x000d0b02 |
| 3 | 4 | 2 | -2148 | -1395 | -3831 | 0 | 0x000d0b02 |
| 4 | 0 | 1 | -789 | -1800 | -3278 | 0 | 0x000e0b01 |
| 4 | 1 | 1 | -91 | -3000 | -2819 | 0 | 0x000e0b01 |
| 4 | 2 | 1 | 206 | -3000 | -3691 | 0 | 0x000e0b01 |
| 4 | 3 | 1 | 826 | -3000 | -2698 | 0 | 0x000e0b01 |
| 4 | 4 | 1 | 181 | -2800 | -3105 | 0 | 0x000e0b01 |
| 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 4 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 5 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Plume `0` | Cendre noire `1` | Corne du Minotaure `2` | Corne du Minotaure `2` | Boucle de la chance `3` | Boucle de la chance `3` | Anneau des éléments `4` | Anneau des éléments `4` |
| 1 | Cristal de magie `5` | Cristal de magie `5` | Boîte du silence `6` | Boîte du silence `6` | Boîte de Pandore `7` | Boîte de Pandore `7` | Boîte de Pandore `7` | Boîte de Pandore `7` |
| 2 | Cristal de magie `5` | Cristal de magie `5` | Ambrosia `8` | Balance des juges `9` | Balance des juges `9` | Balance des juges `9` | Balance des juges `9` | Balance des juges `9` |
| 3 | Extrait de sang `10` | Extrait de sang `10` | Extrait de sang `10` | Extrait de sang `10` | Extrait de sang `10` | Extrait de sang `10` | Os de guerrier sauvage `11` | Os de guerrier sauvage `11` |
| 4 | Potion med `12` | Potion med `12` | Potion med `12` | Potion med `12` | Elixir de vie `13` | Elixir de vie `13` | Elixir de vie `13` | Elixir de vie `13` |
| 5 | Cristal du mal `14` | Balle des ténèbres `15` | Cristal du mal `14` | Balle des ténèbres `15` | Balle des ténèbres `15` | Cristal du mal `14` | Balle des ténèbres `15` | Cristal du mal `14` |
| 6 | Elixir de vie `13` | Ruban de force `16` | Crosse d'ambre `17` | Hache d'Ouga `18` | Cendre rouge `19` | Cendre rouge `19` | Cendre rouge `19` | Cendre rouge `19` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.90 | coffre `22` | 225 | objet | rangee 0 : Plume (25 %), Cendre noire (17 %), Corne du Minotaure (15 %), Corne du Minotaure (13 %), Boucle de la chance (11 %), Boucle de la chance (9 %), Anneau des éléments (6 %), Anneau des éléments (4 %) | 2876 / 40 / -3927 |
| 0.92 | coffre `22` | 227 | objet | rangee 2 : Cristal de magie (25 %), Cristal de magie (17 %), Ambrosia (15 %), Balance des juges (13 %), Balance des juges (11 %), Balance des juges (9 %), Balance des juges (6 %), Balance des juges (4 %) | 204 / -2760 / 2098 |
| 0.94 | coffre `22` | 226 | objet | rangee 1 : Cristal de magie (25 %), Cristal de magie (17 %), Boîte du silence (15 %), Boîte du silence (13 %), Boîte de Pandore (11 %), Boîte de Pandore (9 %), Boîte de Pandore (6 %), Boîte de Pandore (4 %) | 2474 / -1360 / 1174 |

### Zone 1, sous-zone 9 — repertoire de 6652 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 15 | 0 | 2 | 0x4000 | `00 0f 00 00 00 02 00 40` |
| 1 | 8 | 1 | 3 | 0x4000 | `01 08 00 00 01 03 00 40` |
| 2 | 14 | 2 | 4 | 0x4000 | `02 0e 00 00 02 04 00 40` |
| 3 | 16 | 3 | 5 | 0x4000 | `03 10 00 00 03 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Géant vert** | 100 | **400** | **90** | 14 | 62 | 1 | 40 | `90 72 68 44 70 57 43 43` |
| 1 | **Maudite** | 87 | **69** | **66** | 41 | 8 | 0 | 10 | `62 60 73 82 57 116 72 81` |
| 2 | **Ogre** | 96 | **315** | **60** | 57 | 63 | 0 | 40 | `85 51 61 62 8 67 69 66` |
| 3 | **Troll** | 130 | **850** | **100** | 64 | 150 | 0 | 210 | `110 95 92 91 113 60 88 81` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 3 | 1200 | 0 | -700 | 3072 | 0x00040b03 |
| 0 | 1 | 0 | 800 | 0 | -300 | 3072 | 0x0000ff00 |
| 0 | 2 | 0 | 800 | 0 | -1100 | 3072 | 0x0000ff00 |
| 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 1 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 1 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 4 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 5 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 6 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 7 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 3 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 3 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Balle des éléments `0` | Robe des anciens `1` | Tiare de la terre `2` | Horloge des mystères `3` | Horloge des mystères `3` | Aile de Séraphin `4` | Aile de Séraphin `4` | Aile de Séraphin `4` |
| 1 | Pendentif de princesse `5` | Pendentif de princesse `5` | Bracelet de reine `6` | Bracelet de reine `6` | Manteau des esprits `7` | Manteau des esprits `7` | Durandal `8` | Durandal `8` |
| 2 | Cristal du mal `9` | Hallebarde `10` | Ecu du destin `11` | Casque splendide `12` | Cuirasse de maître `13` | La dévoreuse `14` | Cuirasse de maître `13` | Gulwick `15` |
| 3 | Cristal du mal `9` | Balle des ténèbres `16` | Cristal du mal `9` | Balle des ténèbres `16` | Balle des ténèbres `16` | Cristal du mal `9` | Balle des ténèbres `16` | Cristal du mal `9` |
| 4 | Elixir de vie `17` | Ruban de force `18` | Crosse d'ambre `19` | Hache d'Ouga `20` | Cendre rouge `21` | Cendre rouge `21` | Cendre rouge `21` | Cendre rouge `21` |
| 5 | Ecu du destin `11` | Talisman médicinal `22` | Talisman médicinal `22` | Gants de force `23` | Gants de force `23` | Gants de force `23` | Dropneal `24` | Dominique `25` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `22` | 253 | objet | rangee 0 : Balle des éléments (25 %), Robe des anciens (17 %), Tiare de la terre (15 %), Horloge des mystères (13 %), Horloge des mystères (11 %), Aile de Séraphin (9 %), Aile de Séraphin (6 %), Aile de Séraphin (4 %) | 1722 / -160 / -828 |
| 0.1 | coffre `22` | 254 | objet | rangee 1 : Pendentif de princesse (25 %), Pendentif de princesse (17 %), Bracelet de reine (15 %), Bracelet de reine (13 %), Manteau des esprits (11 %), Manteau des esprits (9 %), Durandal (6 %), Durandal (4 %) | -562 / -360 / -3682 |

## Zone 2 — 868 octets, 1 sous-zone(s)

### Zone 2, sous-zone 0 — repertoire de 9580 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 0 | 18 | 0x4000 | `00 01 00 00 00 12 00 40` |
| 1 | 0 | 1 | 16 | 0x4000 | `01 00 00 00 01 10 00 40` |
| 2 | 0 | 2 | 17 | 0x4000 | `01 00 01 00 02 11 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Dragon rouge** | 306 | **8000** | **740** | 67 | 200 | 0 | 500 | `181 140 98 181 162 117 98 181` |
| 1 | **Salamandre** | 115 | **190** | **220** | 8 | 82 | 0 | 30 | `88 106 81 84 92 72 90 89` |
| 2 | **Salamandre feu** | 155 | **240** | **280** | 8 | 82 | 0 | 30 | `98 106 81 124 88 82 80 129` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -200 | 0 | 550 | 0 | 0x00030b00 |
| 0 | 1 | 2 | -200 | 0 | 1050 | 0 | 0x0000ff02 |
| 0 | 2 | 2 | -200 | 0 | 150 | 0 | 0x0000ff02 |
| 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 1 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 1 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` |
| 1 | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` |
| 2 | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` |
| 3 | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` |
| 4 | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` |
| 5 | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` | Bouclier de vie `2` |
| 6 | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` | Bouclier de glace `0` |
| 7 | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` | Robe des esprits `1` |
| 8 | Epée noire `3` | Epée noire `3` | Epée noire `3` | Epée noire `3` | Epée noire `3` | Epée noire `3` | Epée noire `3` | Epée noire `3` |
| 9 | Sceptre de bataille `4` | Sceptre de bataille `4` | Sceptre de bataille `4` | Sceptre de bataille `4` | Sceptre de bataille `4` | Sceptre de bataille `4` | Sceptre de bataille `4` | Sceptre de bataille `4` |
| 10 | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` | Poignard d'Orihalcon `5` |
| 11 | Baguette spatiale `6` | Baguette spatiale `6` | Baguette spatiale `6` | Baguette spatiale `6` | Baguette spatiale `6` | Baguette spatiale `6` | Baguette spatiale `6` | Baguette spatiale `6` |
| 12 | Flèche de lumière `7` | Flèche de lumière `7` | Flèche de lumière `7` | Flèche de lumière `7` | Flèche de lumière `7` | Flèche de lumière `7` | Flèche de lumière `7` | Flèche de lumière `7` |
| 13 | Forgée de glace `8` | Forgée de glace `8` | Forgée de glace `8` | Forgée de glace `8` | Forgée de glace `8` | Forgée de glace `8` | Forgée de glace `8` | Forgée de glace `8` |
| 14 | Hache empoisonnée `9` | Hache empoisonnée `9` | Hache empoisonnée `9` | Hache empoisonnée `9` | Hache empoisonnée `9` | Hache empoisonnée `9` | Hache empoisonnée `9` | Hache empoisonnée `9` |
| 15 | Baguette guérisseuse `10` | Baguette guérisseuse `10` | Baguette guérisseuse `10` | Baguette guérisseuse `10` | Baguette guérisseuse `10` | Baguette guérisseuse `10` | Baguette guérisseuse `10` | Baguette guérisseuse `10` |
| 16 | Joyau de feu `11` | Balle de feu `12` | Cristal de feu `13` | Franberg `14` | Hache du feu `15` | Cristal de feu `13` | Rubis `16` | Rubis `16` |
| 17 | Balle de lumière `17` | Balle des éléments `18` | Cristal de lumière `19` | Hache du feu `15` | Epée de glace `20` | Cristal de lumière `19` | Rubis `16` | Rubis `16` |
| 18 | Corne du dragon `21` | Cristal de feu `13` | Robe des anciens `22` | Dropneal `23` | Mistortain `24` | Bahamut `25` | Fabnihl `26` | Lame de Calvin `27` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.0 | coffre `12` | 248 | objet | rangee 0 : Bouclier de glace (25 %), Bouclier de glace (17 %), Bouclier de glace (15 %), Bouclier de glace (13 %), Bouclier de glace (11 %), Bouclier de glace (9 %), Bouclier de glace (6 %), Bouclier de glace (4 %) | 200 / 240 / 3520 |
| 1.9 | coffre `12` | 251 | objet | rangee 8 : Epée noire (25 %), Epée noire (17 %), Epée noire (15 %), Epée noire (13 %), Epée noire (11 %), Epée noire (9 %), Epée noire (6 %), Epée noire (4 %) | 70 / 240 / 3800 |
| 4.2 | coffre `12` | -252 | objet | rangee 18 : Corne du dragon (25 %), Cristal de feu (17 %), Robe des anciens (15 %), Dropneal (13 %), Mistortain (11 %), Bahamut (9 %), Fabnihl (6 %), Lame de Calvin (4 %) | 530 / 40 / 1300 |

## Zone 3 — 1464 octets, 10 sous-zone(s)

### Zone 3, sous-zone 0 — repertoire de 18476 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 0 | 0 | 0x4000 | `00 02 00 00 00 00 00 40` |
| 1 | 0 | 2 | 1 | 0x4000 | `01 00 00 00 02 01 00 40` |
| 2 | 1 | 1 | 2 | 0x4000 | `02 01 00 00 01 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 2 | **14** | **4** | 1 | 6 | 0 | 66 | `9 3 2 3 2 3 2 3` |
| 1 | **Coccinelle** | 3 | **8** | **1** | 18 | 15 | 0 | 60 | `8 4 2 2 2 1 2 2` |
| 2 | **Fourmi géante** | 2 | **12** | **1** | 20 | 8 | 0 | 60 | `8 2 1 3 1 3 1 3` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -2500 | 5 | -3500 | 0 | 0x000f0b01 |
| 0 | 1 | 1 | -2900 | 5 | -2300 | 0 | 0x000f0b01 |
| 0 | 2 | 1 | -2300 | 5 | -1500 | 0 | 0x000f0b01 |
| 0 | 3 | 1 | -2700 | 5 | 0 | 0 | 0x000f0b01 |
| 1 | 0 | 2 | 4000 | 5 | 3200 | 0 | 0x00100b02 |
| 1 | 1 | 2 | 3800 | 5 | 4800 | 0 | 0x00100b02 |
| 1 | 2 | 2 | 3500 | 5 | 4000 | 0 | 0x00100b02 |
| 2 | 0 | 0 | 3300 | 5 | -600 | 0 | 0x00110b00 |
| 2 | 1 | 0 | 4000 | 5 | 200 | 0 | 0x00110b00 |
| 2 | 2 | 1 | 3300 | 5 | 700 | 0 | 0x00110b01 |
| 2 | 3 | 1 | 3700 | 5 | 1900 | 0 | 0x00110b01 |
| 2 | 4 | 2 | 3100 | 5 | 500 | 0 | 0x00110b02 |
| 3 | 0 | 0 | 2700 | 5 | 4000 | 0 | 0x00120b00 |
| 3 | 1 | 0 | 2200 | 5 | 3500 | 0 | 0x00120b00 |
| 3 | 2 | 0 | 2000 | 5 | 2000 | 0 | 0x00120b00 |
| 3 | 3 | 0 | 1700 | 5 | 1500 | 0 | 0x00120b00 |
| 4 | 0 | 0 | 200 | 5 | 2100 | 0 | 0x00130b00 |
| 4 | 1 | 0 | -800 | 5 | 2400 | 0 | 0x00130b00 |
| 4 | 2 | 0 | -1700 | 5 | 3300 | 0 | 0x00130b00 |
| 4 | 3 | 2 | 0 | 205 | 4200 | 0 | 0x00130b02 |
| 4 | 4 | 2 | 700 | 5 | 4000 | 0 | 0x00130b02 |
| 5 | 0 | 0 | 3500 | 5 | -1600 | 0 | 0x00140b00 |
| 5 | 1 | 0 | 3800 | 5 | -2000 | 0 | 0x00140b00 |
| 5 | 2 | 0 | 3400 | 5 | -3200 | 0 | 0x00140b00 |
| 5 | 3 | 0 | 2400 | 5 | -3500 | 0 | 0x00140b00 |
| 6 | 0 | 0 | -3700 | 5 | 3800 | 0 | 0x00150b00 |
| 6 | 1 | 0 | -3300 | 5 | 3600 | 0 | 0x00150b00 |
| 6 | 2 | 1 | -3000 | 5 | 2800 | 0 | 0x00150b01 |
| 6 | 3 | 1 | -3900 | 5 | 2700 | 0 | 0x00150b01 |
| 6 | 4 | 2 | -3600 | 5 | 4600 | 0 | 0x00150b02 |
| 6 | 5 | 2 | -3300 | 5 | 4600 | 0 | 0x00150b02 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0001ff02 |
| 10 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 11 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 11 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 11 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 12 | 4 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Potion med `0` | Rapière `1` | Glaive `2` | Mercuriale `3` | Cuir fin `4` | Cuir fin `4` | Bouclier rond `5` | Féline `6` |
| 1 | Lavande `7` | Lavande `7` | Potion med `0` | Potion med `0` | Glaive `2` | Glaive `2` | Daak `8` | Daak `8` |
| 2 | Lavande `7` | Lavande `7` | Potion med `0` | Potion med `0` | Extrait de sang `9` | Extrait de sang `9` | Extrait de sang `9` | Extrait de sang `9` |

### Zone 3, sous-zone 1 — repertoire de 14792 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 1 | 0 | 0x4000 | `00 02 00 00 01 00 00 40` |
| 1 | 1 | 2 | 2 | 0x4000 | `01 01 00 00 02 02 00 40` |
| 2 | 6 | 3 | 4 | 0x4000 | `02 06 00 00 03 04 00 40` |
| 3 | 5 | 4 | 3 | 0x4000 | `03 05 00 00 04 03 00 40` |
| 4 | 2 | 0 | 1 | 0x4000 | `00 02 01 00 00 01 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 6 | **23** | **4** | 1 | 8 | 3 | 155 | `11 11 4 4 3 4 3 4` |
| 1 | **Goblin** | 2 | **14** | **4** | 1 | 6 | 0 | 66 | `8 3 2 3 2 3 2 3` |
| 2 | **Coccinelle** | 2 | **8** | **1** | 18 | 15 | 0 | 60 | `8 4 1 1 1 3 1 1` |
| 3 | **Poisson volant** | 4 | **12** | **5** | 50 | 13 | 0 | 60 | `10 8 9 8 3 2 3 4` |
| 4 | **Crabe géant** | 5 | **21** | **15** | 45 | 11 | 0 | 60 | `11 10 10 12 3 2 4 6` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 3 | 2200 | 100 | -4800 | 0 | 0x00150b03 |
| 0 | 1 | 3 | 2300 | 105 | -4700 | 0 | 0x00150b03 |
| 1 | 0 | 2 | 1900 | 105 | -1100 | 0 | 0x00160b02 |
| 1 | 1 | 2 | 2500 | 105 | -1100 | 0 | 0x00160b02 |
| 1 | 2 | 2 | 3100 | 105 | -1100 | 0 | 0x00160b02 |
| 2 | 0 | 0 | 4000 | 505 | -900 | 0 | 0x00170b00 |
| 2 | 1 | 0 | 4300 | 105 | -900 | 0 | 0x00170b00 |
| 3 | 0 | 2 | 4200 | 105 | 800 | 0 | 0x00180b02 |
| 3 | 1 | 2 | 4300 | 105 | 0 | 0 | 0x00180b02 |
| 3 | 2 | 2 | 4200 | 105 | 850 | 0 | 0x00180b02 |
| 4 | 0 | 0 | 4100 | 505 | 1500 | 0 | 0x00190b00 |
| 4 | 1 | 0 | 4300 | 105 | 1500 | 0 | 0x00190b00 |
| 5 | 0 | 2 | 1900 | 105 | 1700 | 0 | 0x001a0b02 |
| 5 | 1 | 2 | 2500 | 105 | 1700 | 0 | 0x001a0b02 |
| 5 | 2 | 2 | 3100 | 105 | 1700 | 0 | 0x001a0b02 |
| 6 | 0 | 4 | -3700 | 105 | 300 | 0 | 0x001b0b04 |
| 6 | 1 | 4 | -3400 | 105 | 0 | 0 | 0x001b0b04 |
| 6 | 2 | 4 | -3700 | 105 | 300 | 0 | 0x001b0b04 |
| 6 | 3 | 4 | -3500 | 105 | 600 | 0 | 0x001b0b04 |
| 7 | 0 | 4 | -3600 | 105 | 2200 | 0 | 0x001c0b04 |
| 7 | 1 | 4 | -3500 | 105 | 2200 | 0 | 0x001c0b04 |
| 7 | 2 | 4 | -4000 | 105 | 2300 | 0 | 0x001c0b04 |
| 7 | 3 | 4 | -3900 | 105 | 2200 | 0 | 0x001c0b04 |
| 8 | 0 | 2 | -2500 | 105 | 3200 | 0 | 0x001d0b02 |
| 8 | 1 | 2 | -2500 | 105 | 4000 | 0 | 0x001d0b02 |
| 8 | 2 | 2 | -2500 | 105 | 4800 | 0 | 0x001d0b02 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 11 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Potion med `0` | Rapière `1` | Glaive `2` | Mercuriale `3` | Cuir fin `4` | Cuir fin `4` | Bouclier rond `5` | Féline `6` |
| 1 | Large épée `7` | Elixir de vie `8` | Elixir de vie `8` | Rapière `1` | Rapière `1` | Tablier de cuir `9` | Pavois de chevalier `10` | Féline `6` |
| 2 | Lavande `11` | Lavande `11` | Potion med `0` | Potion med `0` | Extrait de sang `12` | Extrait de sang `12` | Extrait de sang `12` | Extrait de sang `12` |
| 3 | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Cristal d'eau `13` | Cristal d'eau `13` | Cristal d'eau `13` |
| 4 | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Potion med `0` | Cristal d'eau `13` | Cristal d'eau `13` |

### Zone 3, sous-zone 2 — repertoire de 14504 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 18 | 0 | 2 | 0x4000 | `00 12 00 00 00 02 00 40` |
| 1 | 4 | 1 | 3 | 0x4000 | `01 04 00 00 01 03 00 40` |
| 2 | 3 | 2 | 4 | 0x4000 | `02 03 00 00 02 04 00 40` |
| 3 | 5 | 3 | 5 | 0x4000 | `03 05 00 00 03 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Crotale** | 8 | **30** | **3** | 5 | 8 | 3 | 60 | `14 8 13 16 8 10 12 13` |
| 1 | **Grizzly** | 18 | **36** | **9** | 6 | 19 | 0 | 60 | `18 16 12 16 2 6 4 4` |
| 2 | **Abeille** | 9 | **20** | **1** | 16 | 14 | 0 | 48 | `14 9 12 13 6 6 8 8` |
| 3 | **Crabe géant** | 5 | **21** | **15** | 45 | 11 | 0 | 68 | `11 10 10 12 3 2 4 6` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -2000 | 5 | -4300 | 0 | 0x000c0b00 |
| 0 | 1 | 0 | -2500 | 5 | -4300 | 0 | 0x000c0b00 |
| 0 | 2 | 0 | -2500 | 5 | -4800 | 0 | 0x000c0b00 |
| 0 | 3 | 0 | -2000 | 5 | -4800 | 0 | 0x000c0b00 |
| 1 | 0 | 2 | -4100 | 605 | -3300 | 0 | 0x000d0b02 |
| 1 | 1 | 2 | -3700 | 605 | -3000 | 0 | 0x000d0b02 |
| 1 | 2 | 2 | -4000 | 605 | -2000 | 0 | 0x000d0b02 |
| 2 | 0 | 3 | -4200 | 605 | -600 | 0 | 0x000e0b03 |
| 2 | 1 | 3 | -4000 | 605 | -100 | 0 | 0x000e0b03 |
| 2 | 2 | 3 | -4300 | 605 | 530 | 0 | 0x000e0b03 |
| 2 | 3 | 3 | -4300 | 605 | 1600 | 0 | 0x000e0b03 |
| 2 | 4 | 3 | -3900 | 605 | 1200 | 0 | 0x000e0b03 |
| 3 | 0 | 2 | -2300 | 5 | 4700 | 0 | 0x000f0b02 |
| 3 | 1 | 2 | -1600 | 5 | 5100 | 0 | 0x000f0b02 |
| 3 | 2 | 2 | -700 | 5 | 5400 | 0 | 0x000f0b02 |
| 3 | 3 | 2 | 0 | 5 | 5200 | 0 | 0x000f0b02 |
| 4 | 0 | 3 | 3300 | 5 | 4700 | 0 | 0x00100b03 |
| 4 | 1 | 3 | 2700 | 5 | 4900 | 0 | 0x00100b03 |
| 4 | 2 | 3 | 2079 | 5 | 4835 | 0 | 0x00100b03 |
| 5 | 0 | 3 | 400 | 5 | -3700 | 0 | 0x00110b03 |
| 5 | 1 | 3 | 1300 | 5 | -3600 | 0 | 0x00110b03 |
| 5 | 2 | 3 | 1500 | 5 | -3200 | 0 | 0x00110b03 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Arc classique `0` | Cuir fin `1` | Cotte de maille `2` | Hachette `3` | Gants de cuir `4` | Casque de cuir `5` | Grand arc `6` | Pavois fin `7` |
| 1 | Corde du retour `8` | Corde du retour `8` | Huile du pouvoir `9` | Lavande `10` | Sauge `11` | Casse-tête à chaîne `12` | Daak `13` | Manteau à plumes `14` |
| 2 | Potion med `15` | Potion med `15` | Potion med `15` | Potion olympique `16` | Potion olympique `16` | Potion olympique `16` | Potion olympique `16` | Extrait de sang `17` |
| 3 | Potion med `15` | Potion med `15` | Potion med `15` | Potion med `15` | Potion med `15` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` |
| 4 | Aiguille médicinale `18` | Aiguille médicinale `18` | Aiguille médicinale `18` | Sauvagerie `19` | Sauvagerie `19` | Sauvagerie `19` | Sauvagerie `19` | Sauvagerie `19` |
| 5 | Potion med `15` | Potion med `15` | Potion med `15` | Potion med `15` | Potion med `15` | Cristal d'eau `20` | Cristal d'eau `20` | Cristal d'eau `20` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.0 | coffre `10` | 243 | objet | rangee 1 : Corde du retour (25 %), Corde du retour (17 %), Huile du pouvoir (15 %), Lavande (13 %), Sauge (11 %), Casse-tête à chaîne (9 %), Daak (6 %), Manteau à plumes (4 %) | 900 / 40 / -1500 |
| 4.0 | coffre `10` | 242 | objet | rangee 0 : Arc classique (25 %), Cuir fin (17 %), Cotte de maille (15 %), Hachette (13 %), Gants de cuir (11 %), Casque de cuir (9 %), Grand arc (6 %), Pavois fin (4 %) | 900 / 340 / -1740 |

### Zone 3, sous-zone 3 — repertoire de 13988 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 1 | 0 | 0x4000 | `00 08 00 00 01 00 00 40` |
| 1 | 9 | 2 | 1 | 0x4000 | `01 09 00 00 02 01 00 40` |
| 2 | 7 | 3 | 2 | 0x4000 | `02 07 00 00 03 02 00 40` |
| 3 | 5 | 4 | 3 | 0x4000 | `03 05 00 00 04 03 00 40` |
| 4 | 6 | 5 | 4 | 0x4000 | `04 06 00 00 05 04 00 40` |
| 5 | 30 | 0 | 4 | 0x6000 | `05 1e 00 00 00 04 00 60` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Barricade** | 1 | **40** | **10** | 0 | 77 | 0 | 0 | `10 25 5 5 2 25 1 1` |
| 1 | **Goblin** | 16 | **33** | **8** | 2 | 8 | 0 | 156 | `14 13 13 13 8 8 7 6` |
| 2 | **Goblin-sorcier** | 16 | **33** | **70** | 3 | 17 | 0 | 206 | `11 10 10 12 14 10 17 16` |
| 3 | **Homme-lézard** | 20 | **39** | **21** | 19 | 37 | 0 | 66 | `16 18 17 17 9 9 8 7` |
| 4 | **Crabe géant** | 5 | **21** | **15** | 45 | 11 | 0 | 60 | `11 10 10 12 3 2 4 6` |
| 5 | **Poisson volant** | 4 | **12** | **5** | 50 | 13 | 0 | 60 | `10 8 9 8 3 2 3 4` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | -3200 | 5 | 4200 | 0 | 0x00100b02 |
| 0 | 1 | 3 | -3100 | 5 | 3800 | 0 | 0x00100b03 |
| 0 | 2 | 3 | -3000 | 5 | 3400 | 0 | 0x00100b03 |
| 0 | 3 | 4 | -3100 | 5 | 2500 | 0 | 0x00100b04 |
| 1 | 0 | 3 | -1100 | 5 | 2000 | 0 | 0x00110b03 |
| 1 | 1 | 3 | -1400 | 5 | 1800 | 0 | 0x00110b03 |
| 1 | 2 | 4 | -1600 | 5 | 1400 | 0 | 0x00110b04 |
| 1 | 3 | 4 | -1100 | 5 | 1500 | 0 | 0x00110b04 |
| 2 | 0 | 2 | 3500 | 5 | 400 | 0 | 0x00120b02 |
| 2 | 1 | 3 | 3300 | 5 | -100 | 0 | 0x00120b03 |
| 2 | 2 | 3 | 3200 | 5 | 1000 | 0 | 0x00120b03 |
| 2 | 3 | 3 | 3000 | 5 | 1700 | 0 | 0x00120b03 |
| 3 | 0 | 2 | -1600 | 5 | 0 | 0 | 0x00130b02 |
| 3 | 1 | 2 | -1100 | 5 | 0 | 0 | 0x00130b02 |
| 3 | 2 | 2 | -600 | 5 | 0 | 0 | 0x00130b02 |
| 3 | 3 | 2 | -200 | 5 | 0 | 0 | 0x00130b02 |
| 4 | 0 | 4 | -3100 | 5 | -100 | 0 | 0x00150b04 |
| 4 | 1 | 4 | -2800 | 5 | 600 | 0 | 0x00150b04 |
| 4 | 2 | 4 | -3800 | 5 | 600 | 0 | 0x00150b04 |
| 5 | 0 | 3 | -3300 | 5 | -3000 | 0 | 0x00160b03 |
| 5 | 1 | 3 | -2300 | 5 | -4000 | 0 | 0x00160b03 |
| 5 | 2 | 3 | -1100 | 5 | -400 | 0 | 0x00160b03 |
| 5 | 3 | 4 | -3100 | 5 | -1900 | 0 | 0x00160b04 |
| 5 | 4 | 4 | -600 | 5 | -3800 | 0 | 0x00160b04 |
| 6 | 0 | 4 | 1600 | 5 | -3700 | 0 | 0x00170b04 |
| 6 | 1 | 4 | 1500 | 5 | -4800 | 0 | 0x00170b04 |
| 6 | 2 | 4 | 1700 | 5 | -5000 | 0 | 0x00170b04 |
| 7 | 0 | 0 | 3600 | 5 | -3000 | 0 | 0x00180b00 |
| 7 | 1 | 0 | 3500 | 5 | -3700 | 0 | 0x00180b00 |
| 7 | 2 | 0 | 3100 | 5 | -5000 | 0 | 0x00180b00 |
| 7 | 3 | 0 | 3300 | 5 | -4400 | 0 | 0x00180b00 |
| 7 | 4 | 1 | 3100 | 5 | -3700 | 0 | 0x00180b01 |
| 7 | 5 | 1 | 3100 | 5 | -4400 | 0 | 0x00180b01 |
| 8 | 0 | 5 | 3400 | 0 | -4050 | 0 | 0x00210b05 |
| 9 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 10 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 10 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 11 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 11 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 11 | 2 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 11 | 3 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 12 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 12 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 12 | 2 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 12 | 3 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Casse-tête `0` | Lavande `1` | Casse-tête `0` | Cuir fin `2` | Arc noble `3` | Arc noble `3` | Marteau de guerre `4` | Marteau de guerre `4` |
| 1 | Bâton de fer `5` | Baguette aux joyaux `6` | Sauge `7` | Verveine `8` | Mercuriale `9` | Fenouil `10` | Ecu du destin `11` | Balle de cristal `12` |
| 2 | Rapière `13` | Large épée `14` | Ecu du destin `11` | Fine rapière `15` | Rapière Misuril `16` | Ecu du destin `11` | Ecu du destin `11` | Ecu du destin `11` |
| 3 | Potion med `17` | Potion med `17` | Potion med `17` | Potion med `17` | Potion med `17` | Cristal d'eau `18` | Cristal d'eau `18` | Cristal d'eau `18` |
| 4 | Potion med `17` | Potion med `17` | Potion med `17` | Potion med `17` | Potion med `17` | Potion med `17` | Cristal d'eau `18` | Cristal d'eau `18` |

### Zone 3, sous-zone 4 — repertoire de 25240 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 3 | 0 | 11 | 0x4000 | `00 03 00 00 00 0b 00 40` |
| 1 | 7 | 1 | 12 | 0x4000 | `01 07 00 00 01 0c 00 40` |
| 2 | 4 | 2 | 13 | 0x4000 | `02 04 00 00 02 0d 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Abeille tueuse** | 9 | **20** | **1** | 16 | 14 | 3 | 48 | `14 9 12 13 6 6 8 8` |
| 1 | **Homme-lézard** | 20 | **39** | **21** | 19 | 37 | 0 | 66 | `16 18 17 17 9 9 8 7` |
| 2 | **Grizzly** | 18 | **36** | **9** | 6 | 19 | 0 | 60 | `18 16 12 16 2 6 4 4` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | -2400 | 5 | -5100 | 0 | 0x000e0b02 |
| 0 | 1 | 2 | -2800 | 5 | -4500 | 0 | 0x000e0b02 |
| 0 | 2 | 1 | -3600 | 5 | -4100 | 0 | 0x000e0b01 |
| 0 | 3 | 1 | -3200 | 5 | -1900 | 0 | 0x000e0b01 |
| 1 | 0 | 2 | -1400 | 5 | -1100 | 0 | 0x000f0b02 |
| 1 | 1 | 2 | -1000 | 5 | -1500 | 0 | 0x000f0b02 |
| 1 | 2 | 0 | -1200 | 5 | -2000 | 0 | 0x000f0b00 |
| 1 | 3 | 0 | -1800 | 5 | -1600 | 0 | 0x000f0b00 |
| 1 | 4 | 1 | -1500 | 5 | -1200 | 0 | 0x000f0b01 |
| 2 | 0 | 0 | -100 | 5 | -3000 | 0 | 0x00100b00 |
| 2 | 1 | 0 | 0 | 5 | -3700 | 0 | 0x00100b00 |
| 2 | 2 | 1 | 700 | 5 | -4500 | 0 | 0x00100b01 |
| 2 | 3 | 1 | 1500 | 5 | -4700 | 0 | 0x00100b01 |
| 2 | 4 | 1 | 2700 | 5 | -5000 | 0 | 0x00100b01 |
| 3 | 0 | 0 | 2900 | 5 | -3200 | 0 | 0x00110b00 |
| 3 | 1 | 0 | 2900 | 5 | -2300 | 0 | 0x00110b00 |
| 3 | 2 | 0 | 3200 | 5 | -2100 | 0 | 0x00110b00 |
| 3 | 3 | 0 | 3300 | 5 | -700 | 0 | 0x00110b00 |
| 3 | 4 | 0 | 2900 | 5 | -1100 | 0 | 0x00110b00 |
| 4 | 0 | 0 | 2400 | 5 | 1400 | 0 | 0x00120b00 |
| 4 | 1 | 0 | 1500 | 5 | 1200 | 0 | 0x00120b00 |
| 4 | 2 | 0 | 600 | 5 | 1700 | 0 | 0x00120b00 |
| 5 | 0 | 0 | -3700 | 5 | 1900 | 0 | 0x00130b00 |
| 5 | 1 | 1 | -2800 | 5 | 3100 | 0 | 0x00130b01 |
| 5 | 2 | 1 | -1700 | 5 | 4200 | 0 | 0x00130b01 |
| 5 | 3 | 2 | -1400 | 5 | 2600 | 0 | 0x00130b02 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 4 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 5 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 6 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 7 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 4 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 5 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Longue épée `0` | Pique `1` | Sceptre de la mort `2` | Chaînette d'argent `3` | Casquette de chasseur `4` | Pierre médicinale `5` | Pierre magique `6` | Arc en croix `7` |
| 1 | Talisman médicinal `8` | Nobles bottes `9` | Bottes de sept lieux `10` | Cape du printemps `11` | Boîte à bijoux `12` | Bague sacrée `13` | Bague de protection `14` | Bague de protection `14` |
| 2 | Poudre de zombie `15` | Poudre de zombie `15` | Poudre de zombie `15` | Poudre de zombie `15` | Poudre de zombie `15` | Poudre de zombie `15` | Poudre de zombie `15` | Poudre de zombie `15` |
| 3 | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` |
| 4 | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` |
| 5 | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` |
| 6 | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` |
| 7 | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` |
| 8 | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` | Tablier de cuir `18` |
| 9 | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` | Plastron `16` |
| 10 | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` | Robe fine `17` |
| 11 | Aiguille médicinale `19` | Aiguille médicinale `19` | Aiguille médicinale `19` | Sauvagerie `20` | Sauvagerie `20` | Sauvagerie `20` | Sauvagerie `20` | Sauvagerie `20` |
| 12 | Rapière `21` | Large épée `22` | Ecu du destin `23` | Fine rapière `24` | Rapière Misuril `25` | Ecu du destin `23` | Ecu du destin `23` | Ecu du destin `23` |
| 13 | Potion med `26` | Potion med `26` | Potion med `26` | Potion med `26` | Potion med `26` | Extrait de sang `27` | Extrait de sang `27` | Extrait de sang `27` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.0 | coffre `10` | 244 | objet | rangee 0 : Longue épée (25 %), Pique (17 %), Sceptre de la mort (15 %), Chaînette d'argent (13 %), Casquette de chasseur (11 %), Pierre médicinale (9 %), Pierre magique (6 %), Arc en croix (4 %) | 1900 / 140 / 2700 |
| 3.1 | coffre `10` | 245 | message | — | -2070 / 340 / 1280 |
| 3.5 | coffre `10` | 221 | message | — | -2850 / 140 / 800 |
| 6.0 | coffre `10` | 246 | message | — | -2100 / -160 / -400 |
| 6.3 | coffre `10` | -220 | effet | — | -3100 / -160 / -500 |
| 6.5 | coffre `10` | 219 | objet | rangee 3 : Plastron (25 %), Plastron (17 %), Plastron (15 %), Plastron (13 %), Plastron (11 %), Plastron (9 %), Plastron (6 %), Plastron (4 %) | -3100 / -160 / -300 |
| 6.7 | coffre `10` | 223 | objet | rangee 2 : Poudre de zombie (25 %), Poudre de zombie (17 %), Poudre de zombie (15 %), Poudre de zombie (13 %), Poudre de zombie (11 %), Poudre de zombie (9 %), Poudre de zombie (6 %), Poudre de zombie (4 %) | -2700 / -160 / -1100 |
| 6.9 | coffre `10` | -210 | objet | rangee 1 : Talisman médicinal (25 %), Nobles bottes (17 %), Bottes de sept lieux (15 %), Cape du printemps (13 %), Boîte à bijoux (11 %), Bague sacrée (9 %), Bague de protection (6 %), Bague de protection (4 %) | -2500 / -160 / -1100 |

### Zone 3, sous-zone 5 — repertoire de 31512 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 16 | 0 | 0 | 0x4000 | `00 10 00 00 00 00 00 40` |
| 1 | 3 | 1 | 1 | 0x4000 | `01 03 00 00 01 01 00 40` |
| 2 | 4 | 2 | 2 | 0x4000 | `02 04 00 00 02 02 00 40` |
| 3 | 19 | 1 | 0 | 0x0420 | `03 13 00 00 01 00 20 04` |
| 4 | 4 | 3 | 3 | 0x4000 | `02 04 00 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Méchant ours** | 38 | **300** | **70** | 68 | 20 | 0 | 250 | `28 19 22 14 12 8 18 12` |
| 1 | **Abeille tueuse** | 9 | **26** | **1** | 16 | 14 | 4 | 48 | `14 9 26 25 6 6 8 8` |
| 2 | **Grizzly** | 18 | **41** | **9** | 6 | 19 | 0 | 60 | `18 16 12 16 2 6 4 4` |
| 3 | **Grizzly** | 25 | **80** | **9** | 6 | 19 | 0 | 60 | `23 20 12 16 2 6 4 4` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | -195 | 2200 | 2048 | 0x00060b00 |
| 0 | 1 | 4 | -200 | -195 | 2200 | 2048 | 0x0000ff04 |
| 0 | 2 | 4 | 200 | -195 | 2200 | 2048 | 0x0000ff04 |
| 1 | 0 | 3 | -300 | 0 | -1300 | 2048 | 0x0000ff03 |
| 2 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 2 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 4 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Joyau de terre `15` | Balle de terre `16` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` |
| 1 | Aiguille médicinale `18` | Aiguille médicinale `18` | Aiguille médicinale `18` | Sauvagerie `19` | Sauvagerie `19` | Sauvagerie `19` | Sauvagerie `19` | Sauvagerie `19` |
| 2 | Potion med `20` | Potion med `20` | Potion med `20` | Potion med `20` | Potion med `20` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` |
| 3 | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` | Extrait de sang `17` |
| 4 | Epée du bâtard `0` | Marteau de guerre `1` | Minegosh `2` | Simple bâton `3` | Simple bâton `3` | Minegosh `2` | Marteau de guerre `1` | Epée du bâtard `0` |
| 5 | Arc en croix `4` | Fine rapière `5` | Grande hache `6` | Baguette de cristal `7` | Baguette de cristal `7` | Grande hache `6` | Fine rapière `5` | Arc en croix `4` |
| 6 | Bouclier rond `8` | Robe fine `9` | Robe fine `9` | Bouclier rond `8` | Nobles chaussures `10` | Nobles chaussures `10` | Gants de force `11` | Gants de force `11` |
| 7 | Poudre de zombie `12` | Poudre de zombie `12` | Poudre de zombie `12` | Ambrosia `13` | Ambrosia `13` | Ambrosia `13` | Aile de Séraphin `14` | Aile de Séraphin `14` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 5.0 | coffre `10` | 253 | objet | rangee 5 : Arc en croix (25 %), Fine rapière (17 %), Grande hache (15 %), Baguette de cristal (13 %), Baguette de cristal (11 %), Grande hache (9 %), Fine rapière (6 %), Arc en croix (4 %) | 4100 / 140 / -3700 |
| 5.1 | coffre `10` | 252 | objet | rangee 4 : Epée du bâtard (25 %), Marteau de guerre (17 %), Minegosh (15 %), Simple bâton (13 %), Simple bâton (11 %), Minegosh (9 %), Marteau de guerre (6 %), Epée du bâtard (4 %) | 4300 / 140 / 1700 |
| 6.0 | coffre `10` | 254 | objet | rangee 6 : Bouclier rond (25 %), Robe fine (17 %), Robe fine (15 %), Bouclier rond (13 %), Nobles chaussures (11 %), Nobles chaussures (9 %), Gants de force (6 %), Gants de force (4 %) | -4100 / 140 / -3700 |
| 6.1 | coffre `10` | 255 | objet | rangee 7 : Poudre de zombie (25 %), Poudre de zombie (17 %), Poudre de zombie (15 %), Ambrosia (13 %), Ambrosia (11 %), Ambrosia (9 %), Aile de Séraphin (6 %), Aile de Séraphin (4 %) | -4300 / 140 / 1900 |

### Zone 3, sous-zone 6 — repertoire de 23076 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 7 | 0 | 1 | 0x4000 | `00 07 00 00 00 01 00 40` |
| 1 | 10 | 1 | 3 | 0x4000 | `01 0a 00 00 01 03 00 40` |
| 2 | 11 | 2 | 4 | 0x4000 | `02 0b 00 00 02 04 00 40` |
| 3 | 7 | 3 | 2 | 0x4000 | `00 07 03 00 03 02 00 40` |
| 4 | 15 | 4 | 5 | 0x4000 | `03 0f 00 00 04 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Homme-lézard** | 20 | **39** | **21** | 19 | 37 | 0 | 66 | `18 18 17 17 9 9 8 7` |
| 1 | **Trent** | 65 | **121** | **170** | 15 | 72 | 0 | 58 | `53 41 49 34 16 58 46 60` |
| 2 | **Fleur du mal** | 50 | **140** | **68** | 17 | 55 | 3 | 65 | `52 40 31 36 32 36 20 22` |
| 3 | **Lézard noir** | 85 | **159** | **21** | 19 | 37 | 0 | 43 | `72 71 52 56 49 49 48 47` |
| 4 | **Willie** | 67 | **80** | **26** | 41 | 8 | 3 | 15 | `18 240 32 48 37 26 30 45` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 1700 | 5 | 1200 | 0 | 0x00190b02 |
| 1 | 0 | 1 | 100 | 5 | 2000 | 0 | 0x001a0b01 |
| 1 | 1 | 4 | 100 | 5 | 2300 | 0 | 0x001a0b04 |
| 1 | 2 | 4 | 150 | 5 | 2200 | 0 | 0x001a0b04 |
| 1 | 3 | 4 | 50 | 5 | 2200 | 0 | 0x001a0b04 |
| 2 | 0 | 0 | -2700 | 5 | 1800 | 0 | 0x001b0b00 |
| 3 | 0 | 0 | 1000 | 5 | -1500 | 0 | 0x001c0b00 |
| 4 | 0 | 1 | -300 | 5 | -1600 | 0 | 0x001d0b01 |
| 5 | 0 | 0 | 500 | 5 | -3200 | 0 | 0x001e0b00 |
| 6 | 0 | 0 | -1300 | 5 | -3200 | 0 | 0x001f0b00 |
| 6 | 1 | 0 | -1000 | 5 | -3300 | 0 | 0x001f0b00 |
| 7 | 0 | 1 | 2900 | 105 | -4700 | 0 | 0x00200b01 |
| 7 | 1 | 1 | 2500 | 5 | -4500 | 0 | 0x00200b01 |
| 7 | 2 | 0 | 2500 | 5 | -3900 | 0 | 0x00200b00 |
| 7 | 3 | 0 | 2500 | 5 | -5000 | 0 | 0x00200b00 |
| 8 | 0 | 3 | 3600 | 105 | -3000 | 0 | 0x00210b03 |
| 8 | 1 | 3 | 3540 | 105 | -3100 | 0 | 0x00210b03 |
| 8 | 2 | 3 | 4100 | 5 | -3000 | 0 | 0x00210b03 |
| 8 | 3 | 3 | 3300 | 5 | -2800 | 0 | 0x00210b03 |
| 9 | 0 | 1 | -3300 | 105 | 3700 | 0 | 0x00220b01 |
| 9 | 1 | 1 | -3700 | 5 | 3000 | 0 | 0x00220b01 |
| 9 | 2 | 2 | -4000 | 5 | 3700 | 0 | 0x00220b02 |
| 9 | 3 | 2 | -2700 | 5 | 3600 | 0 | 0x00220b02 |
| 10 | 0 | 1 | 3600 | 5 | 3300 | 0 | 0x00230b01 |
| 10 | 1 | 1 | 2600 | 5 | 4200 | 0 | 0x00230b01 |
| 10 | 2 | 0 | 3200 | 5 | 3200 | 0 | 0x00230b00 |
| 10 | 3 | 0 | 3200 | 105 | 4000 | 0 | 0x00230b00 |
| 11 | 0 | 2 | -2600 | 5 | -4400 | 0 | 0x00240b02 |
| 11 | 1 | 2 | -2600 | 5 | -3800 | 0 | 0x00240b02 |
| 11 | 2 | 0 | -2900 | 105 | -4600 | 0 | 0x00240b00 |
| 11 | 3 | 0 | -3200 | 105 | -4500 | 0 | 0x00240b00 |
| 12 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 13 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 13 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 13 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 15 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 15 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 15 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 15 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 15 | 4 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 16 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 17 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 17 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 17 | 2 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 17 | 3 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Poignard d'argent `0` | Hache de Viking `1` | Robe des runes `2` | Bouclier d'argent `3` | Miroir d'eau `4` | Aquachâle `5` | Aquachâle `5` | Miroir d'eau `4` |
| 1 | Rapière `6` | Large épée `7` | Ecu du destin `8` | Fine rapière `9` | Rapière Misuril `10` | Ecu du destin `8` | Ecu du destin `8` | Ecu du destin `8` |
| 2 | Chaîne des ténèbres `11` | Fine rapière `9` | Ecu du destin `8` | Bouclier de la terre `12` | Rapière d'argent `13` | Cristal saint `14` | Cristal saint `14` | Cristal saint `14` |
| 3 | Parfum de convalescence `15` | Parfum de guérison `16` | Joyau de terre `17` | Ecu du destin `8` | Talisman `18` | Cristal de terre `19` | Cristal de terre `19` | Talisman `18` |
| 4 | Verveine `20` | Mercuriale `21` | Fenouil `22` | Hysopius `23` | Belladone `24` | Belladone `24` | Ambrosia `25` | Ambrosia `25` |
| 5 | Cristal de lumière `26` | Balle de lumière `27` | Cristal de lumière `26` | Chaîne de la lumière `28` | Chaîne de la lumière `28` | Cristal de lumière `26` | Balle de lumière `27` | Cristal de lumière `26` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.2 | coffre `10` | 224 | objet | rangee 0 : Poignard d'argent (25 %), Hache de Viking (17 %), Robe des runes (15 %), Bouclier d'argent (13 %), Miroir d'eau (11 %), Aquachâle (9 %), Aquachâle (6 %), Miroir d'eau (4 %) | 3700 / 140 / -3100 |

### Zone 3, sous-zone 7 — repertoire de 13408 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 10 | 0 | 0 | 0x4000 | `00 0a 00 00 00 00 00 40` |
| 1 | 7 | 1 | 1 | 0x4000 | `01 07 00 00 01 01 00 40` |
| 2 | 15 | 2 | 2 | 0x4000 | `02 0f 00 00 02 02 00 40` |
| 3 | 11 | 3 | 3 | 0x4000 | `03 0b 00 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Trent** | 65 | **121** | **170** | 15 | 72 | 0 | 58 | `53 41 49 34 16 58 46 60` |
| 1 | **Lézard noir** | 85 | **159** | **21** | 19 | 37 | 0 | 43 | `72 71 52 56 49 49 48 47` |
| 2 | **Willie** | 67 | **80** | **26** | 41 | 8 | 3 | 15 | `18 240 32 48 37 26 30 45` |
| 3 | **Fleur du mal** | 50 | **140** | **68** | 17 | 55 | 3 | 65 | `52 40 31 36 32 36 20 22` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -2800 | 5 | 4500 | 0 | 0x00100b00 |
| 0 | 1 | 1 | -1900 | 5 | 4700 | 0 | 0x00100b01 |
| 0 | 2 | 1 | -1000 | 5 | 4700 | 0 | 0x00100b01 |
| 1 | 0 | 0 | -2800 | 5 | -1600 | 0 | 0x00110b00 |
| 1 | 1 | 0 | -3400 | 5 | 700 | 0 | 0x00110b00 |
| 1 | 2 | 3 | -3300 | 5 | 800 | 0 | 0x00110b03 |
| 1 | 3 | 3 | -3100 | 5 | -700 | 0 | 0x00110b03 |
| 2 | 0 | 0 | 1500 | 5 | 5100 | 1024 | 0x00120b00 |
| 2 | 1 | 0 | 1900 | 5 | 5100 | 1024 | 0x00120b00 |
| 2 | 2 | 0 | 2200 | 5 | 5100 | 1024 | 0x00120b00 |
| 2 | 3 | 1 | 2600 | 5 | 5200 | 1024 | 0x00120b01 |
| 2 | 4 | 1 | 1200 | 5 | 4900 | 1024 | 0x00120b01 |
| 3 | 0 | 0 | -2200 | 5 | 2800 | 0 | 0x00130b00 |
| 3 | 1 | 0 | -600 | 5 | 3400 | 0 | 0x00130b00 |
| 3 | 2 | 3 | -2500 | 5 | 2200 | 0 | 0x00130b03 |
| 3 | 3 | 3 | -2500 | 5 | 2200 | 0 | 0x00130b03 |
| 4 | 0 | 1 | -2100 | 5 | -200 | 0 | 0x00140b01 |
| 4 | 1 | 1 | -1000 | 5 | -1500 | 0 | 0x00140b01 |
| 4 | 2 | 1 | 0 | 5 | -2600 | 0 | 0x00140b01 |
| 5 | 0 | 3 | 3100 | 5 | 1900 | 0 | 0x00150b03 |
| 5 | 1 | 3 | 4100 | 5 | 1900 | 0 | 0x00150b03 |
| 5 | 2 | 3 | 4100 | 5 | 1900 | 0 | 0x00150b03 |
| 6 | 0 | 2 | 4300 | 5 | 200 | 0 | 0x00160b02 |
| 6 | 1 | 2 | 4300 | 5 | 200 | 0 | 0x00160b02 |
| 6 | 2 | 2 | 4500 | 5 | -600 | 0 | 0x00160b02 |
| 6 | 3 | 2 | 4500 | 5 | -600 | 0 | 0x00160b02 |
| 6 | 4 | 2 | 4300 | 5 | 0 | 0 | 0x00160b02 |
| 6 | 5 | 2 | 4300 | 5 | 0 | 0 | 0x00160b02 |
| 6 | 6 | 2 | 4300 | 5 | 0 | 0 | 0x00160b02 |
| 6 | 7 | 2 | 4300 | 5 | 0 | 0 | 0x00160b02 |
| 7 | 0 | 0 | 1900 | -95 | -5200 | 0 | 0x00170b00 |
| 7 | 1 | 0 | 1900 | -95 | -5200 | 0 | 0x00170b00 |
| 7 | 2 | 1 | 1500 | -95 | -5000 | 0 | 0x00170b01 |
| 7 | 3 | 1 | 800 | -95 | -5300 | 0 | 0x00170b01 |
| 7 | 4 | 1 | 800 | -95 | -5300 | 0 | 0x00170b01 |
| 7 | 5 | 2 | 500 | -95 | -5300 | 0 | 0x00170b02 |
| 7 | 6 | 2 | 1300 | -95 | -5000 | 0 | 0x00170b02 |
| 7 | 7 | 2 | 1300 | -95 | -5000 | 0 | 0x00170b02 |
| 8 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 4 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 5 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 11 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Parfum de convalescence `0` | Parfum de guérison `1` | Joyau de terre `2` | Ecu du destin `3` | Talisman `4` | Cristal de terre `5` | Cristal de terre `5` | Talisman `4` |
| 1 | Chaîne des ténèbres `6` | Fine rapière `7` | Ecu du destin `3` | Bouclier de la terre `8` | Rapière d'argent `9` | Cristal saint `10` | Cristal saint `10` | Cristal saint `10` |
| 2 | Cristal de lumière `11` | Balle de lumière `12` | Cristal de lumière `11` | Chaîne de la lumière `13` | Chaîne de la lumière `13` | Cristal de lumière `11` | Balle de lumière `12` | Cristal de lumière `11` |
| 3 | Verveine `14` | Mercuriale `15` | Fenouil `16` | Hysopius `17` | Belladone `18` | Belladone `18` | Ambrosia `19` | Ambrosia `19` |

### Zone 3, sous-zone 8 — repertoire de 19732 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 13 | 0 | 0 | 0x4000 | `00 0d 00 00 00 00 00 40` |
| 1 | 12 | 1 | 1 | 0x4000 | `01 0c 00 00 01 01 00 40` |
| 2 | 14 | 2 | 2 | 0x4000 | `02 0e 00 00 02 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Varan** | 81 | **150** | **190** | 58 | 81 | 0 | 50 | `86 70 66 69 72 70 61 56` |
| 1 | **Wyrm** | 86 | **162** | **94** | 53 | 84 | 1 | 50 | `78 60 60 64 68 61 61 62` |
| 2 | **Géant** | 95 | **320** | **90** | 14 | 62 | 1 | 50 | `100 67 63 39 65 52 37 37` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -3886 | 5 | 4634 | 0 | 0x000a0b00 |
| 0 | 1 | 0 | -3582 | 5 | 1810 | 0 | 0x000a0b00 |
| 0 | 2 | 0 | -3910 | 5 | 3218 | 0 | 0x000a0b00 |
| 0 | 3 | 0 | -3568 | 5 | 32 | 0 | 0x000a0b00 |
| 0 | 4 | 0 | -3664 | 5 | -1100 | 0 | 0x000a0b00 |
| 1 | 0 | 0 | -2470 | 5 | -1532 | 0 | 0x000b0b00 |
| 1 | 1 | 0 | -1966 | 5 | -2544 | 0 | 0x000b0b00 |
| 1 | 2 | 1 | -758 | 5 | -2132 | 0 | 0x000b0b01 |
| 1 | 3 | 1 | -1207 | 5 | -3152 | 0 | 0x000b0b01 |
| 2 | 0 | 0 | 1785 | 105 | -4196 | 0 | 0x000c0b00 |
| 2 | 1 | 0 | -303 | 105 | -3244 | 0 | 0x000c0b00 |
| 2 | 2 | 0 | 1092 | 105 | -3801 | 0 | 0x000c0b00 |
| 3 | 0 | 2 | 1570 | 325 | 348 | 0 | 0x000d0b02 |
| 3 | 1 | 2 | 2240 | 325 | 344 | 0 | 0x000d0b02 |
| 4 | 0 | 2 | 958 | 325 | 1868 | 0 | 0x000e0b02 |
| 4 | 1 | 2 | 3495 | 325 | 4236 | 0 | 0x000e0b02 |
| 4 | 2 | 2 | 2835 | 325 | 1607 | 0 | 0x000e0b02 |
| 4 | 3 | 1 | 2155 | 325 | 4711 | 0 | 0x0000ff01 |
| 4 | 4 | 1 | 339 | 325 | 4607 | 0 | 0x0000ff01 |
| 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Ecailles de Belzébuth `0` | Ecailles de Belzébuth `0` | Harpe d'or `1` | Harpe d'or `1` | Cristal de terre `2` | Cristal de terre `2` | Cristal de terre `2` | Cristal de terre `2` |
| 1 | Manteau de cuir `3` | Manteau de cuir `3` | Joyau de vent `4` | Joyau de vent `4` | Joyau de vent `4` | Queue de dragon `5` | Ecailles de dragon `6` | Amulette du dragon `7` |
| 2 | Hache de boucher `8` | Corne du Minotaure `9` | Ecu du destin `10` | Casque splendide `11` | Cuirasse de maître `12` | La dévoreuse `13` | La dévoreuse `13` | Cuirasse de maître `12` |

### Zone 3, sous-zone 9 — repertoire de 2568 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 17 | 0 | 0 | 0x4000 | `00 11 00 00 00 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Behemoth** | 150 | **2160** | **400** | 70 | 120 | 0 | 250 | `120 96 102 80 146 120 94 80` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 205 | -400 | 0 | 0x00040b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Cristal de terre `0` | Bâton stellaire `1` | Cristal de terre `0` | Anneau des éléments `2` | Anneau des éléments `2` | Ecailles de Belzébuth `3` | Ecailles de Belzébuth `3` | Hache de Mars `4` |

## Zone 4 — 4452 octets, 11 sous-zone(s)

### Zone 4, sous-zone 0 — repertoire de 14380 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x4000 | `00 00 00 00 00 00 00 40` |
| 1 | 1 | 1 | 1 | 0x4000 | `01 01 00 00 01 01 00 40` |
| 2 | 2 | 2 | 2 | 0x4000 | `02 02 00 00 02 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 30 | **46** | **8** | 2 | 20 | 0 | 64 | `20 18 16 16 8 12 12 12` |
| 1 | **Goblin-sorcier** | 32 | **42** | **82** | 3 | 32 | 0 | 66 | `18 19 16 14 18 21 16 16` |
| 2 | **Gremlin** | 30 | **46** | **39** | 60 | 36 | 4 | 60 | `20 20 18 18 19 21 21 21` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -3505 | 400 | 141 | 0 | 0x000c0b00 |
| 0 | 1 | 0 | -3265 | 0 | 291 | 0 | 0x000c0b00 |
| 0 | 2 | 0 | -3265 | 0 | -529 | 0 | 0x000c0b00 |
| 0 | 3 | 1 | -3290 | 400 | -95 | 0 | 0x000c0b01 |
| 0 | 4 | 2 | -3530 | 0 | -125 | 0 | 0x000c0b02 |
| 1 | 0 | 0 | -74 | 0 | -2224 | 0 | 0x000d0b00 |
| 1 | 1 | 0 | 286 | 0 | -2224 | 0 | 0x000d0b00 |
| 1 | 2 | 0 | 66 | 0 | -2864 | 0 | 0x000d0b00 |
| 2 | 0 | 0 | 3446 | 0 | -524 | 0 | 0x000e0b00 |
| 2 | 1 | 0 | 3486 | 0 | 296 | 0 | 0x000e0b00 |
| 2 | 2 | 1 | 2695 | 0 | -94 | 0 | 0x000e0b01 |
| 2 | 3 | 2 | 3790 | 0 | -74 | 0 | 0x000e0b02 |
| 3 | 0 | 0 | 3707 | 400 | 2896 | 0 | 0x000f0b00 |
| 3 | 1 | 0 | 3287 | 400 | 2896 | 0 | 0x000f0b00 |
| 3 | 2 | 1 | 3286 | 400 | 3074 | 0 | 0x000f0b01 |
| 3 | 3 | 1 | 3273 | 400 | 2636 | 0 | 0x000f0b01 |
| 4 | 0 | 0 | -3075 | 400 | 2876 | 0 | 0x00100b00 |
| 4 | 1 | 0 | -3474 | 400 | 2082 | 0 | 0x00100b00 |
| 4 | 2 | 1 | -3143 | 400 | 3101 | 0 | 0x00100b01 |
| 4 | 3 | 1 | -3143 | 400 | 2681 | 0 | 0x00100b01 |
| 5 | 0 | 0 | 91 | 0 | 3118 | 0 | 0x00110b00 |
| 5 | 1 | 0 | 291 | 0 | 3058 | 0 | 0x00110b00 |
| 5 | 2 | 0 | 91 | 0 | 2638 | 0 | 0x00110b00 |
| 5 | 3 | 1 | 91 | 0 | 2858 | 0 | 0x00110b01 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Hache de guerre `0` | Lavande `1` | Arc noble `2` | Ecu du destin `3` | Elixir de vie `4` | Tablier de cuir `5` | Pavois fin `6` | Pavois fin `6` |
| 1 | Simple bâton `7` | Simple bâton `7` | Sauge `8` | Verveine `9` | Mercuriale `10` | Fenouil `11` | Ecu du destin `3` | Balle de cristal `12` |
| 2 | Baguette aux joyaux `13` | Aile de fée `14` | Poudre de fées `15` | Poudre de fées `15` | Ecu du destin `3` | Ecu du destin `3` | Poudre de fées `15` | Parfum de feu `16` |

### Zone 4, sous-zone 1 — repertoire de 19380 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 4 | 0x4000 | `00 00 00 00 00 04 00 40` |
| 1 | 1 | 1 | 5 | 0x4000 | `01 01 00 00 01 05 00 40` |
| 2 | 3 | 2 | 6 | 0x4000 | `02 03 00 00 02 06 00 40` |
| 3 | 4 | 3 | 7 | 0x4000 | `03 04 00 00 03 07 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 30 | **51** | **8** | 2 | 20 | 0 | 64 | `20 18 16 16 8 12 12 12` |
| 1 | **Goblin-sorcier** | 32 | **42** | **82** | 3 | 32 | 0 | 64 | `18 19 16 14 18 21 16 16` |
| 2 | **Belle verte** | 32 | **60** | **13** | 7 | 22 | 0 | 50 | `20 90 21 22 21 20 32 32` |
| 3 | **Garde golem** | 26 | **231** | **61** | 40 | 26 | 0 | 80 | `23 120 90 101 60 154 141 140` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 1300 | 200 | 1700 | 0 | 0x000c0b02 |
| 0 | 1 | 2 | 1700 | 200 | 2100 | 0 | 0x000c0b02 |
| 0 | 2 | 2 | 1900 | 200 | 1700 | 0 | 0x000c0b02 |
| 0 | 3 | 2 | 1900 | 200 | 1300 | 0 | 0x000c0b02 |
| 0 | 4 | 2 | 1600 | 200 | 1600 | 0 | 0x000c0b02 |
| 0 | 5 | 2 | 1800 | 200 | 1500 | 0 | 0x000c0b02 |
| 1 | 0 | 1 | -200 | 400 | -2900 | 0 | 0x000d0b01 |
| 1 | 1 | 1 | 0 | 400 | -2900 | 0 | 0x000d0b01 |
| 1 | 2 | 0 | -520 | 400 | -3400 | 0 | 0x000d0b00 |
| 1 | 3 | 0 | 420 | 400 | -3400 | 0 | 0x000d0b00 |
| 2 | 0 | 0 | -3699 | 0 | 594 | 0 | 0x000e0b00 |
| 2 | 1 | 0 | -2919 | 0 | 594 | 0 | 0x000e0b00 |
| 2 | 2 | 1 | -3699 | 0 | 400 | 0 | 0x000e0b01 |
| 2 | 3 | 1 | -2919 | 0 | 400 | 0 | 0x000e0b01 |
| 2 | 4 | 2 | -2899 | 0 | -386 | 0 | 0x000e0b02 |
| 2 | 5 | 2 | -3679 | 0 | -386 | 0 | 0x000e0b02 |
| 2 | 6 | 0 | -3100 | 0 | -386 | 0 | 0x000e0b00 |
| 2 | 7 | 0 | -3500 | 0 | -386 | 0 | 0x000e0b00 |
| 3 | 0 | 1 | 3500 | 200 | -500 | 3072 | 0x000f0b01 |
| 3 | 1 | 1 | 3500 | 200 | -600 | 3072 | 0x000f0b01 |
| 3 | 2 | 0 | 3200 | 200 | -900 | 2048 | 0x000f0b00 |
| 3 | 3 | 0 | 2800 | 200 | -900 | 2048 | 0x000f0b00 |
| 3 | 4 | 0 | 3200 | 200 | 200 | 0 | 0x000f0b00 |
| 3 | 5 | 0 | 2800 | 200 | 200 | 0 | 0x000f0b00 |
| 4 | 0 | 3 | -100 | 400 | -1450 | 0 | 0x00100b03 |
| 5 | 0 | 2 | 2300 | 200 | -3100 | 0 | 0x00110b02 |
| 5 | 1 | 2 | 2300 | 200 | -2300 | 2048 | 0x00110b02 |
| 5 | 2 | 2 | 3100 | 200 | -2300 | 0 | 0x00110b02 |
| 5 | 3 | 2 | 2900 | 200 | -3100 | 2048 | 0x00110b02 |
| 6 | 0 | 3 | -100 | 400 | 1650 | 0 | 0x00100b03 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Manteau de brigand `0` | Manteau de brigand `0` | Poignard d'argent `1` | Poignard d'argent `1` | Insigne de brigand `2` | Insigne de brigand `2` | Balle des ténèbres `3` | Balle des ténèbres `3` |
| 1 | Talisman `4` | Talisman médicinal `5` | Pendentif vaporeux `6` | Chapeau de sorcière `7` | Chapeau de sorcière `7` | Pendentif vaporeux `6` | Talisman médicinal `5` | Talisman `4` |
| 2 | Pique `8` | Fine rapière `9` | Fine rapière `9` | Arc classique `10` | Rapière d'argent `11` | Parfum de guérison `12` | Parfum de guérison `12` | Parfum de guérison `12` |
| 3 | Hachette `13` | Hache de guerre `14` | Hache fine `15` | Hache fine `15` | Daak `16` | Daak `16` | Ecu du destin `17` | Ecu du destin `17` |
| 4 | Hache de guerre `14` | Lavande `18` | Arc noble `19` | Ecu du destin `17` | Elixir de vie `20` | Tablier de cuir `21` | Pavois fin `22` | Pavois fin `22` |
| 5 | Simple bâton `23` | Simple bâton `23` | Sauge `24` | Verveine `25` | Mercuriale `26` | Fenouil `27` | Ecu du destin `17` | Balle de cristal `28` |
| 6 | Potion med `29` | Potion med `29` | Potion med `29` | Potion med `29` | Elixir de vie `20` | Elixir de vie `20` | Elixir de vie `20` | Elixir de vie `20` |
| 7 | Cuirasse de bronze `30` | Epée du bâtard `31` | Pavois de chevalier `32` | Pavois de chevalier `32` | Pavois de chevalier `32` | Armure de chevalier `33` | Armure de chevalier `33` | Armure de chevalier `33` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.32 | coffre `28` | 298 | objet | rangee 2 : Pique (25 %), Fine rapière (17 %), Fine rapière (15 %), Arc classique (13 %), Rapière d'argent (11 %), Parfum de guérison (9 %), Parfum de guérison (6 %), Parfum de guérison (4 %) | 1320 / 240 / 1420 |
| 0.33 | coffre `28` | 299 | objet | rangee 3 : Hachette (25 %), Hache de guerre (17 %), Hache fine (15 %), Hache fine (13 %), Daak (11 %), Daak (9 %), Ecu du destin (6 %), Ecu du destin (4 %) | 1950 / 240 / 1950 |
| 1.0 | coffre `28` | 228 | objet | rangee 0 : Manteau de brigand (25 %), Manteau de brigand (17 %), Poignard d'argent (15 %), Poignard d'argent (13 %), Insigne de brigand (11 %), Insigne de brigand (9 %), Balle des ténèbres (6 %), Balle des ténèbres (4 %) | 1500 / 440 / 3500 |
| 1.3 | coffre `28` | 163 | objet | rangee 1 : Talisman (25 %), Talisman médicinal (17 %), Pendentif vaporeux (15 %), Chapeau de sorcière (13 %), Chapeau de sorcière (11 %), Pendentif vaporeux (9 %), Talisman médicinal (6 %), Talisman (4 %) | 2200 / 445 / 40 |

### Zone 4, sous-zone 2 — repertoire de 11524 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 5 | 0 | 1 | 0x4000 | `00 05 00 00 00 01 00 40` |
| 1 | 6 | 1 | 2 | 0x4000 | `01 06 00 00 01 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Lézard rouge** | 34 | **101** | **21** | 19 | 37 | 4 | 60 | `50 37 36 35 39 31 31 31` |
| 1 | **Crapouille** | 38 | **71** | **4** | 21 | 62 | 3 | 50 | `44 37 61 29 31 36 31 35` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -250 | 0 | -2500 | 2048 | 0x00060b00 |
| 0 | 1 | 0 | 400 | 0 | -2500 | 2048 | 0x00060b00 |
| 0 | 2 | 0 | -250 | 0 | -3450 | 0 | 0x00060b00 |
| 0 | 3 | 0 | 400 | 0 | -3450 | 0 | 0x00060b00 |
| 1 | 0 | 1 | 2700 | 400 | -1300 | 0 | 0x00070b01 |
| 1 | 1 | 1 | 2700 | 400 | -1600 | 2048 | 0x00070b01 |
| 2 | 0 | 1 | 100 | 400 | 300 | 0 | 0x00080b01 |
| 2 | 1 | 1 | 500 | 400 | -100 | 0 | 0x00080b01 |
| 2 | 2 | 1 | 100 | 400 | -500 | 0 | 0x00080b01 |
| 2 | 3 | 1 | -300 | 400 | -100 | 0 | 0x00080b01 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure de chevalier `0` | Armure de chevalier `0` | Armure de chevalier `0` | Armure de chevalier `0` | Armure de chevalier `0` | Armure de chevalier `0` | Armure de chevalier `0` | Armure de chevalier `0` |
| 1 | Fine rapière `1` | Longue épée `2` | Ecu du destin `3` | Ecu du destin `3` | Franberg `4` | Ecu du destin `3` | Ecu du destin `3` | Ecu du destin `3` |
| 2 | Elixir des esprits `5` | Elixir des esprits `5` | Elixir des esprits `5` | Elixir des esprits `5` | Cristal de magie `6` | Cristal de magie `6` | Cristal de magie `6` | Cristal de magie `6` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.53 | coffre `28` | 278 | objet | rangee 0 : Armure de chevalier (25 %), Armure de chevalier (17 %), Armure de chevalier (15 %), Armure de chevalier (13 %), Armure de chevalier (11 %), Armure de chevalier (9 %), Armure de chevalier (6 %), Armure de chevalier (4 %) | 100 / 40 / 3200 |

### Zone 4, sous-zone 3 — repertoire de 23056 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 9 | 0 | 6 | 0x4000 | `00 09 00 00 00 06 00 40` |
| 1 | 8 | 1 | 7 | 0x4000 | `01 08 00 00 01 07 00 40` |
| 2 | 2 | 2 | 8 | 0x4000 | `02 02 00 00 02 08 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Sorcier** | 48 | **80** | **215** | 22 | 48 | 3 | 80 | `40 38 38 38 43 42 36 38` |
| 1 | **Sylphide** | 70 | **463** | **218** | 71 | 70 | 0 | 180 | `46 55 40 48 34 42 40 46` |
| 2 | **Goblin volant** | 30 | **46** | **39** | 60 | 36 | 4 | 60 | `20 20 18 18 19 21 21 21` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 100 | 200 | 5200 | 2048 | 0x00150b01 |
| 0 | 1 | 0 | 500 | 200 | 5100 | 2048 | 0x0000ff00 |
| 0 | 2 | 0 | -300 | 200 | 5100 | 2048 | 0x0000ff00 |
| 0 | 3 | 0 | 100 | 200 | 5500 | 2048 | 0x0000ff00 |
| 1 | 0 | 0 | 3300 | 200 | -2200 | 0 | 0x00160b00 |
| 1 | 1 | 0 | 3600 | 200 | -2600 | 0 | 0x00160b00 |
| 1 | 2 | 0 | 2900 | 200 | -2600 | 0 | 0x00160b00 |
| 2 | 0 | 2 | 200 | 200 | 2200 | 3584 | 0x00170b02 |
| 2 | 1 | 2 | 0 | 200 | 2200 | 512 | 0x00170b02 |
| 2 | 2 | 2 | 200 | 200 | 2400 | 2560 | 0x00170b02 |
| 2 | 3 | 2 | 0 | 200 | 2400 | 1536 | 0x00170b02 |
| 3 | 0 | 2 | 2100 | 200 | -2300 | 0 | 0x00180b02 |
| 3 | 1 | 2 | 2300 | 200 | -2300 | 0 | 0x00180b02 |
| 3 | 2 | 2 | 2200 | 200 | -2400 | 0 | 0x00180b02 |
| 3 | 3 | 2 | 2200 | 0 | 200 | 2048 | 0x00180b02 |
| 3 | 4 | 2 | 1900 | 0 | -200 | 0 | 0x00180b02 |
| 3 | 5 | 2 | 2500 | 200 | -200 | 0 | 0x00180b02 |
| 4 | 0 | 0 | 1500 | 200 | 4500 | 2048 | 0x00190b00 |
| 4 | 1 | 0 | 1900 | 200 | 4100 | 3072 | 0x00190b00 |
| 5 | 0 | 0 | -1300 | 200 | 4500 | 2048 | 0x001a0b00 |
| 5 | 1 | 0 | -1700 | 200 | 4100 | 1024 | 0x001a0b00 |
| 6 | 0 | 2 | 550 | 200 | -2700 | 0 | 0x001b0b02 |
| 6 | 1 | 2 | 450 | 200 | -2700 | 1024 | 0x001b0b02 |
| 6 | 2 | 2 | 150 | 200 | -2700 | 2048 | 0x001b0b02 |
| 6 | 3 | 2 | 50 | 200 | -2700 | 2048 | 0x001b0b02 |
| 6 | 4 | 2 | -250 | 200 | -2700 | 1024 | 0x001b0b02 |
| 6 | 5 | 2 | -350 | 200 | -2700 | 0 | 0x001b0b02 |
| 7 | 0 | 2 | -2100 | 0 | -500 | 0 | 0x001c0b02 |
| 7 | 1 | 2 | -3600 | 0 | -1500 | 0 | 0x001c0b02 |
| 7 | 2 | 2 | -4900 | 0 | -500 | 0 | 0x001c0b02 |
| 7 | 3 | 2 | -2100 | 0 | 300 | 0 | 0x001c0b02 |
| 7 | 4 | 2 | -3600 | 0 | 1300 | 0 | 0x001c0b02 |
| 7 | 5 | 2 | -4900 | 0 | 300 | 0 | 0x001c0b02 |
| 8 | 0 | 0 | -2200 | 200 | -3500 | 0 | 0x001d0b00 |
| 8 | 1 | 0 | -2200 | 200 | -3100 | 0 | 0x001d0b00 |
| 8 | 2 | 0 | -2200 | 200 | -3300 | 0 | 0x001d0b00 |
| 8 | 3 | 0 | -1950 | 200 | -3300 | 0 | 0x001d0b00 |
| 9 | 0 | 0 | 3600 | 200 | 2400 | 0 | 0x001e0b00 |
| 10 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 11 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Bague de protection `0` | Rubis `1` | Saphir `2` | Armure mirage `3` | Balle des éléments `4` | Symbole de force `5` | Table des anciens `6` | Table des anciens `6` |
| 1 | Elixir de vie `7` | Elixir de vie `7` | Elixir de vie `7` | Elixir de vie `7` | Elixir de vie `7` | Elixir de vie `7` | Panoramix `8` | Panoramix `8` |
| 2 | Elixir de magicien `9` | Elixir de magicien `9` | Elixir de magicien `9` | Elixir de magicien `9` | Elixir de magicien `9` | Elixir de magicien `9` | Panoramix `8` | Panoramix `8` |
| 3 | Poudre de zombie `10` | Poudre de zombie `10` | Poudre de zombie `10` | Poudre de zombie `10` | Ambrosia `11` | Ambrosia `11` | Ambrosia `11` | Ambrosia `11` |
| 4 | Robe épiscopale `12` | Robe magique `13` | Robe magique `13` | Robe épiscopale `12` | Lampe sacrée `14` | Lampe sacrée `14` | Bouclier réflecteur `15` | Bouclier réflecteur `15` |
| 5 | Baguette des runes `16` | Baguette des runes `16` | Bouclier des runes `17` | Amulette des runes `18` | Gants des runes `19` | Cape des runes `20` | Table des anciens `6` | Table des anciens `6` |
| 6 | Elixir de magicien `9` | Ecu du destin `21` | Robe de reine `22` | Ecu du destin `21` | Bague réflectrice `23` | Bague de résistance `24` | Bague de résistance `24` | Bague réflectrice `23` |
| 7 | Bottes d'elfes `25` | Chapeau d'elfe `26` | Manteau d'elfe `27` | Bouclier des elfes `28` | Arc des elfes `29` | Arc des elfes `29` | Arc des elfes `29` | Arc des elfes `29` |
| 8 | Baguette aux joyaux `30` | Aile de fée `31` | Poudre de fées `32` | Poudre de fées `32` | Ecu du destin `21` | Ecu du destin `21` | Poudre de fées `32` | Parfum de feu `33` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `28` | 217 | objet | rangee 0 : Bague de protection (25 %), Rubis (17 %), Saphir (15 %), Armure mirage (13 %), Balle des éléments (11 %), Symbole de force (9 %), Table des anciens (6 %), Table des anciens (4 %) | 3500 / 40 / 4100 |
| 0.1 | coffre `28` | 220 | objet | rangee 3 : Poudre de zombie (25 %), Poudre de zombie (17 %), Poudre de zombie (15 %), Poudre de zombie (13 %), Ambrosia (11 %), Ambrosia (9 %), Ambrosia (6 %), Ambrosia (4 %) | 3100 / 240 / -2300 |
| 0.2 | coffre `28` | 221 | objet | rangee 4 : Robe épiscopale (25 %), Robe magique (17 %), Robe magique (15 %), Robe épiscopale (13 %), Lampe sacrée (11 %), Lampe sacrée (9 %), Bouclier réflecteur (6 %), Bouclier réflecteur (4 %) | 3500 / 240 / -2300 |
| 2.8 | coffre `28` | 219 | objet | rangee 2 : Elixir de magicien (25 %), Elixir de magicien (17 %), Elixir de magicien (15 %), Elixir de magicien (13 %), Elixir de magicien (11 %), Elixir de magicien (9 %), Panoramix (6 %), Panoramix (4 %) | 2900 / 40 / -1500 |
| 3.8 | coffre `28` | 0 | message | — | -2000 / 200 / -3500 |
| 3.9 | coffre `28` | 0 | message | — | 3300 / 0 / 500 |
| 3.10 | coffre `28` | 0 | message | — | 3700 / 0 / 500 |
| 3.11 | coffre `28` | 0 | message | — | 1300 / 200 / -2500 |
| 3.12 | coffre `28` | 0 | message | — | 1300 / 200 / -2900 |
| 3.13 | coffre `28` | 0 | message | — | 1700 / 200 / -2500 |
| 3.14 | coffre `28` | 0 | message | — | 1700 / 200 / -2900 |
| 3.15 | coffre `28` | 0 | message | — | -2000 / 200 / -3100 |
| 4.2 | coffre `28` | 218 | objet | rangee 1 : Elixir de vie (25 %), Elixir de vie (17 %), Elixir de vie (15 %), Elixir de vie (13 %), Elixir de vie (11 %), Elixir de vie (9 %), Panoramix (6 %), Panoramix (4 %) | 1500 / 40 / -1500 |
| 4.3 | coffre `28` | 222 | objet | rangee 5 : Baguette des runes (25 %), Baguette des runes (17 %), Bouclier des runes (15 %), Amulette des runes (13 %), Gants des runes (11 %), Cape des runes (9 %), Table des anciens (6 %), Table des anciens (4 %) | -1900 / 240 / -2100 |

### Zone 4, sous-zone 4 — repertoire de 23672 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 1 | 0x4000 | `00 00 00 00 00 01 00 40` |
| 1 | 1 | 1 | 2 | 0x4000 | `01 01 00 00 01 02 00 40` |
| 2 | 3 | 2 | 3 | 0x4000 | `02 03 00 00 02 03 00 40` |
| 3 | 5 | 3 | 4 | 0x4000 | `03 05 00 00 03 04 00 40` |
| 4 | 6 | 4 | 6 | 0x4000 | `04 06 00 00 04 06 00 40` |
| 5 | 5 | 5 | 5 | 0x4000 | `03 05 01 00 05 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Goblin** | 30 | **51** | **8** | 2 | 20 | 0 | 64 | `20 18 16 16 8 12 12 12` |
| 1 | **Goblin-sorcier** | 32 | **42** | **82** | 3 | 32 | 0 | 66 | `18 19 16 14 18 21 16 16` |
| 2 | **Belle verte** | 32 | **60** | **13** | 7 | 22 | 0 | 50 | `20 90 21 22 21 20 32 32` |
| 3 | **Homme-lézard** | 30 | **39** | **21** | 19 | 37 | 0 | 66 | `16 18 17 17 9 9 8 7` |
| 4 | **Crapouille** | 38 | **71** | **4** | 21 | 62 | 3 | 50 | `44 37 61 29 31 36 31 35` |
| 5 | **Lézard rouge** | 34 | **101** | **21** | 19 | 37 | 4 | 60 | `46 32 36 35 39 26 26 26` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | -200 | 400 | -3100 | 0 | 0x00120b04 |
| 0 | 1 | 4 | 400 | 400 | -3100 | 0 | 0x00120b04 |
| 0 | 2 | 4 | 100 | 800 | -3600 | 0 | 0x00120b04 |
| 1 | 0 | 1 | -1700 | 1200 | 3300 | 0 | 0x00130b01 |
| 1 | 1 | 1 | -1700 | 1200 | 3600 | 0 | 0x00130b01 |
| 1 | 2 | 0 | -2100 | 1200 | 3300 | 0 | 0x00130b00 |
| 1 | 3 | 0 | -2100 | 1200 | 3600 | 0 | 0x00130b00 |
| 1 | 4 | 4 | -1900 | 1200 | 3400 | 0 | 0x00130b04 |
| 2 | 0 | 1 | 1900 | 1200 | 3300 | 0 | 0x00140b01 |
| 2 | 1 | 1 | 1900 | 1200 | 3600 | 0 | 0x00140b01 |
| 2 | 2 | 0 | 2200 | 1200 | 3300 | 0 | 0x00140b00 |
| 2 | 3 | 0 | 2200 | 1200 | 3600 | 0 | 0x00140b00 |
| 2 | 4 | 4 | 2100 | 1200 | 3400 | 0 | 0x00140b04 |
| 3 | 0 | 2 | -3800 | 1200 | 3550 | 0 | 0x00150b02 |
| 3 | 1 | 2 | -3600 | 1200 | 3550 | 0 | 0x00150b02 |
| 3 | 2 | 2 | -3600 | 1200 | 3200 | 0 | 0x00150b02 |
| 3 | 3 | 2 | -3800 | 1200 | 3200 | 0 | 0x00150b02 |
| 3 | 4 | 3 | -3700 | 1200 | 2725 | 0 | 0x00150b03 |
| 3 | 5 | 3 | -3300 | 1200 | 3500 | 0 | 0x00150b03 |
| 4 | 0 | 2 | 4000 | 1200 | 3550 | 0 | 0x00160b02 |
| 4 | 1 | 2 | 3800 | 1200 | 3550 | 0 | 0x00160b02 |
| 4 | 2 | 2 | 3800 | 1200 | 3200 | 0 | 0x00160b02 |
| 4 | 3 | 2 | 4000 | 1200 | 3200 | 0 | 0x00160b02 |
| 4 | 4 | 3 | 3900 | 1200 | 2725 | 0 | 0x00160b03 |
| 4 | 5 | 3 | 3400 | 1200 | 3500 | 0 | 0x00160b03 |
| 5 | 0 | 3 | 300 | 800 | -500 | 2048 | 0x00170b03 |
| 5 | 1 | 3 | -100 | 800 | -500 | 2048 | 0x00170b03 |
| 5 | 2 | 1 | 300 | 1000 | 0 | 0 | 0x00170b01 |
| 5 | 3 | 1 | -100 | 1000 | 0 | 0 | 0x00170b01 |
| 5 | 4 | 5 | 100 | 1000 | 300 | 0 | 0x00170b05 |
| 6 | 0 | 3 | -1700 | 0 | -5700 | 0 | 0x00180b03 |
| 6 | 1 | 3 | -1700 | 0 | -5200 | 0 | 0x00180b03 |
| 6 | 2 | 3 | -2500 | 0 | -5700 | 0 | 0x00180b03 |
| 6 | 3 | 3 | -2500 | 0 | -5200 | 0 | 0x00180b03 |
| 7 | 0 | 3 | 1900 | 0 | -5300 | 0 | 0x00190b03 |
| 7 | 1 | 3 | 1900 | 0 | -5700 | 0 | 0x00190b03 |
| 7 | 2 | 3 | 2700 | 0 | -5300 | 0 | 0x00190b03 |
| 7 | 3 | 3 | 2700 | 0 | -5700 | 0 | 0x00190b03 |
| 7 | 4 | 4 | 2300 | 0 | -5500 | 0 | 0x00190b04 |
| 8 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 9 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0001ff04 |
| 10 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0002ff02 |
| 10 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0002ff02 |
| 10 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0002ff02 |
| 10 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0002ff02 |
| 10 | 4 | 2 | 0 | 0 | 0 | 0 | 0x0002ff02 |
| 10 | 5 | 2 | 0 | 0 | 0 | 0 | 0x0002ff02 |
| 11 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 11 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 11 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0003ff01 |
| 11 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0003ff01 |
| 11 | 4 | 1 | 0 | 0 | 0 | 0 | 0x0003ff01 |
| 12 | 0 | 5 | 0 | 0 | 0 | 0 | 0x0000ff05 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Parfum de convalescence `0` | Parfum de guérison `1` | Pierre médicinale `2` | Pierre magique `3` | Ambrosia `4` | Ambrosia `4` | Pentacle de prince `5` | Pentacle magique `6` |
| 1 | Hache de guerre `7` | Lavande `8` | Arc noble `9` | Ecu du destin `10` | Elixir de vie `11` | Tablier de cuir `12` | Pavois fin `13` | Pavois fin `13` |
| 2 | Simple bâton `14` | Simple bâton `14` | Sauge `15` | Verveine `16` | Mercuriale `17` | Fenouil `18` | Ecu du destin `10` | Balle de cristal `19` |
| 3 | Potion med `20` | Potion med `20` | Potion med `20` | Potion med `20` | Elixir de vie `11` | Elixir de vie `11` | Elixir de vie `11` | Elixir de vie `11` |
| 4 | Rapière `21` | Large épée `22` | Ecu du destin `10` | Fine rapière `23` | Rapière Misuril `24` | Ecu du destin `10` | Ecu du destin `10` | Ecu du destin `10` |
| 5 | Fine rapière `23` | Longue épée `25` | Ecu du destin `10` | Ecu du destin `10` | Franberg `26` | Ecu du destin `10` | Ecu du destin `10` | Ecu du destin `10` |
| 6 | Elixir des esprits `27` | Elixir des esprits `27` | Elixir des esprits `27` | Elixir des esprits `27` | Cristal de magie `28` | Cristal de magie `28` | Cristal de magie `28` | Cristal de magie `28` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.8 | coffre `28` | 203 | message | — | -3500 / 40 / -5500 |
| 0.9 | coffre `28` | 204 | message | — | 3700 / 40 / -5500 |
| 2.1 | coffre `28` | 205 | objet | rangee 0 : Parfum de convalescence (25 %), Parfum de guérison (17 %), Pierre médicinale (15 %), Pierre magique (13 %), Ambrosia (11 %), Ambrosia (9 %), Pentacle de prince (6 %), Pentacle magique (4 %) | -1950 / 940 / -1500 |

### Zone 4, sous-zone 5 — repertoire de 15764 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 10 | 3 | 1 | 0x4000 | `00 0a 00 00 03 01 00 40` |
| 1 | 6 | 0 | 2 | 0x4000 | `01 06 00 00 00 02 00 40` |
| 2 | 11 | 1 | 3 | 0x4000 | `02 0b 00 00 01 03 00 40` |
| 3 | 7 | 2 | 4 | 0x4000 | `03 07 00 00 02 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Crapouille** | 38 | **71** | **4** | 21 | 62 | 3 | 50 | `44 37 61 29 31 36 31 35` |
| 1 | **Golem-sequelette** | 99 | **326** | **135** | 23 | 91 | 1 | 60 | `96 111 72 74 51 111 70 71` |
| 2 | **Rampant** | 82 | **137** | **80** | 24 | 70 | 3 | 32 | `67 60 59 53 38 54 60 65` |
| 3 | **Chevalier noir** | 87 | **151** | **94** | 25 | 45 | 1 | 40 | `75 77 71 73 48 59 41 43` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 350 | 0 | -100 | 0 | 0x000e0b01 |
| 0 | 1 | 1 | 350 | 0 | 300 | 0 | 0x000e0b01 |
| 1 | 0 | 0 | 1900 | 0 | 300 | 3072 | 0x000f0b00 |
| 1 | 1 | 0 | 2000 | 0 | 200 | 3072 | 0x000f0b00 |
| 2 | 0 | 1 | -400 | 500 | 4200 | 0 | 0x00100b01 |
| 2 | 1 | 1 | 200 | 500 | 4200 | 0 | 0x00100b01 |
| 2 | 2 | 1 | -400 | 500 | 3500 | 0 | 0x00100b01 |
| 2 | 3 | 1 | 200 | 500 | 3500 | 0 | 0x00100b01 |
| 3 | 0 | 2 | 1500 | 0 | -1700 | 512 | 0x00110b02 |
| 3 | 1 | 2 | 1900 | 0 | -1700 | 3584 | 0x00110b02 |
| 4 | 0 | 0 | -2100 | 400 | 2100 | 0 | 0x00120b00 |
| 4 | 1 | 0 | -2100 | 400 | 1300 | 0 | 0x00120b00 |
| 4 | 2 | 3 | 1000 | 0 | 1600 | 1024 | 0x00120b03 |
| 4 | 3 | 3 | 300 | 0 | 1600 | 1024 | 0x00120b03 |
| 4 | 4 | 3 | -600 | 0 | 1600 | 1024 | 0x00120b03 |
| 4 | 5 | 3 | -1400 | 0 | 1600 | 1024 | 0x00120b03 |
| 5 | 0 | 1 | -1500 | 0 | 0 | 0 | 0x00130b01 |
| 5 | 1 | 1 | -2300 | 0 | 0 | 0 | 0x00130b01 |
| 6 | 0 | 1 | 0 | 0 | -1100 | 0 | 0x00140b01 |
| 6 | 1 | 1 | -600 | 0 | -1100 | 0 | 0x00140b01 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 10 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 10 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Flèche de lumière `0` | Livre de la technique `1` | Bouclier du feu `2` | Rapière aux éclairs `3` | Rapière aux éclairs `3` | Gantelet de force `4` | Baguette du mal `5` | Baguette du mal `5` |
| 1 | Epée de feu `6` | Pavois de chevalier `7` | Cristal d'ombre `8` | Symbole de force `9` | Symbole de force `9` | Armure noire `10` | Armure noire `10` | Answerer `11` |
| 2 | Elixir des esprits `12` | Elixir des esprits `12` | Elixir des esprits `12` | Elixir des esprits `12` | Cristal de magie `13` | Cristal de magie `13` | Cristal de magie `13` | Cristal de magie `13` |
| 3 | Cristal de magie `13` | Cristal de magie `13` | Cristal de magie `13` | Corne du Minotaure `14` | Os de guerrier sauvage `15` | Cendre blanche `16` | Cendre blanche `16` | Corne du dragon `17` |
| 4 | Sauge `18` | Baguette de l'air `19` | Flèche du vent `20` | Cristal d'air `21` | Cristal d'air `21` | Cristal d'air `21` | Balle du tonnerre `22` | Balle du tonnerre `22` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.0 | coffre `28` | 192 | objet | rangee 0 : Flèche de lumière (25 %), Livre de la technique (17 %), Bouclier du feu (15 %), Rapière aux éclairs (13 %), Rapière aux éclairs (11 %), Gantelet de force (9 %), Baguette du mal (6 %), Baguette du mal (4 %) | -3100 / 440 / 125 |

### Zone 4, sous-zone 6 — repertoire de 20572 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 9 | 0 | 2 | 0x4000 | `00 09 00 00 00 02 00 40` |
| 1 | 7 | 1 | 3 | 0x4000 | `01 07 00 00 01 03 00 40` |
| 2 | 10 | 2 | 4 | 0x4000 | `02 0a 00 00 02 04 00 40` |
| 3 | 12 | 3 | 5 | 0x4000 | `03 0c 00 00 03 05 00 40` |
| 4 | 4 | 4 | 6 | 0x4000 | `04 04 00 00 04 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Sorcier** | 48 | **80** | **215** | 22 | 48 | 3 | 80 | `30 28 38 38 23 42 36 38` |
| 1 | **Rampant** | 86 | **137** | **80** | 24 | 70 | 3 | 32 | `67 60 59 53 38 54 60 65` |
| 2 | **Chevalier noir** | 87 | **151** | **94** | 25 | 45 | 1 | 40 | `75 77 71 73 48 59 41 43` |
| 3 | **Sabre volant** | 80 | **80** | **130** | 26 | 40 | 0 | 45 | `70 48 63 61 21 44 62 59` |
| 4 | **Armure volante** | 91 | **131** | **61** | 40 | 40 | 0 | 48 | `80 100 71 73 30 60 50 51` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1300 | 0 | 500 | 2048 | 0x00120b00 |
| 0 | 1 | 0 | -1700 | 0 | 500 | 2048 | 0x00120b00 |
| 0 | 2 | 2 | -1300 | 0 | 200 | 2048 | 0x00120b02 |
| 0 | 3 | 2 | -1700 | 0 | 200 | 2048 | 0x00120b02 |
| 1 | 0 | 1 | -100 | -400 | 2100 | 0 | 0x00130b01 |
| 1 | 1 | 1 | 300 | -400 | 2100 | 0 | 0x00130b01 |
| 1 | 2 | 1 | 100 | -400 | 2100 | 0 | 0x00130b01 |
| 1 | 3 | 3 | 500 | 0 | 1900 | 0 | 0x00130b03 |
| 1 | 4 | 3 | -300 | 0 | 1900 | 0 | 0x00130b03 |
| 2 | 0 | 0 | 1900 | 200 | -1500 | 0 | 0x00140b00 |
| 2 | 1 | 0 | 1500 | 200 | -1500 | 0 | 0x00140b00 |
| 2 | 2 | 2 | 2000 | 0 | -1280 | 0 | 0x00140b02 |
| 2 | 3 | 2 | 1400 | 0 | -1280 | 0 | 0x00140b02 |
| 2 | 4 | 4 | 1700 | 0 | -700 | 2048 | 0x00140b04 |
| 3 | 0 | 0 | 1900 | -400 | 2300 | 2048 | 0x00150b00 |
| 3 | 1 | 0 | 1800 | -400 | 2300 | 2048 | 0x00150b00 |
| 3 | 2 | 0 | 1700 | -400 | 2300 | 2048 | 0x00150b00 |
| 3 | 3 | 2 | 1300 | -400 | 2300 | 2048 | 0x00150b02 |
| 3 | 4 | 2 | 2100 | -400 | 2300 | 2048 | 0x00150b02 |
| 3 | 5 | 2 | 1700 | -400 | 1700 | 2048 | 0x00150b02 |
| 4 | 0 | 0 | 100 | -400 | -2700 | 0 | 0x00160b00 |
| 4 | 1 | 0 | 150 | -400 | -2500 | 0 | 0x00160b00 |
| 4 | 2 | 0 | 50 | -400 | -2500 | 0 | 0x00160b00 |
| 5 | 0 | 3 | -1800 | -200 | 2000 | 1024 | 0x00170b03 |
| 5 | 1 | 3 | -1800 | -200 | 2200 | 1024 | 0x00170b03 |
| 5 | 2 | 4 | -1500 | -200 | 1900 | 1024 | 0x00170b04 |
| 6 | 0 | 0 | 2100 | -400 | 500 | 0 | 0x00180b00 |
| 6 | 1 | 0 | 2100 | -400 | 100 | 0 | 0x00180b00 |
| 6 | 2 | 3 | 1900 | -400 | 500 | 0 | 0x00180b03 |
| 6 | 3 | 4 | 1800 | -400 | 500 | 0 | 0x00180b04 |
| 7 | 0 | 2 | -1350 | -200 | -1400 | 1024 | 0x00190b02 |
| 7 | 1 | 2 | -1500 | -200 | -1300 | 1536 | 0x00190b02 |
| 7 | 2 | 2 | -1650 | -200 | -1200 | 2048 | 0x00190b02 |
| 7 | 3 | 1 | -1700 | -200 | -900 | 2048 | 0x00190b01 |
| 7 | 4 | 1 | -1500 | -200 | -900 | 2048 | 0x00190b01 |
| 8 | 0 | 1 | -2650 | -200 | 300 | 0 | 0x001a0b01 |
| 8 | 1 | 1 | -2750 | -200 | 300 | 0 | 0x001a0b01 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 11 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 12 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 13 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 13 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Solitaire `0` | Baguette-vampire `1` | Baguette-vampire `1` | Solitaire `0` | Voile mystique `2` | Voile mystique `2` | Voile mystique `2` | Couronne de la lune `3` |
| 1 | Cape des runes `4` | Cape du printemps `5` | Cape d'été `6` | Cape d'automne `7` | Cape d'hiver `8` | Manteau des esprits `9` | Manteau des esprits `9` | Anneau de Merlin `10` |
| 2 | Elixir de magicien `11` | Ecu du destin `12` | Robe de reine `13` | Ecu du destin `12` | Bague réflectrice `14` | Bague de résistance `15` | Bague de résistance `15` | Bague réflectrice `14` |
| 3 | Sauge `16` | Baguette de l'air `17` | Flèche du vent `18` | Cristal d'air `19` | Cristal d'air `19` | Cristal d'air `19` | Balle du tonnerre `20` | Balle du tonnerre `20` |
| 4 | Epée de feu `21` | Pavois de chevalier `22` | Cristal d'ombre `23` | Symbole de force `24` | Symbole de force `24` | Armure noire `25` | Armure noire `25` | Answerer `26` |
| 5 | Epée du bâtard `27` | Craimore `28` | Cristal de magie `29` | Cristal de magie `29` | Cristal de magie `29` | Epée de la fatalité `30` | Epée de Damas `31` | Epée du gardien `32` |
| 6 | Elixir de vie `33` | Armure d'argent `34` | Cristal de magie `29` | Bouclier des runes `35` | Bouclier des runes `35` | Robe des runes `36` | Robe des runes `36` | Armure du gardien `37` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.4 | coffre `28` | 199 | objet | rangee 1 : Cape des runes (25 %), Cape du printemps (17 %), Cape d'été (15 %), Cape d'automne (13 %), Cape d'hiver (11 %), Manteau des esprits (9 %), Manteau des esprits (6 %), Anneau de Merlin (4 %) | 1700 / 240 / -1400 |
| 3.0 | coffre `28` | 198 | objet | rangee 0 : Solitaire (25 %), Baguette-vampire (17 %), Baguette-vampire (15 %), Solitaire (13 %), Voile mystique (11 %), Voile mystique (9 %), Voile mystique (6 %), Couronne de la lune (4 %) | -3700 / 240 / 300 |

### Zone 4, sous-zone 7 — repertoire de 30392 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 4 | 0 | 3 | 0x4000 | `00 04 00 00 00 03 00 40` |
| 1 | 8 | 1 | 4 | 0x4000 | `01 08 00 00 01 04 00 40` |
| 2 | 14 | 2 | 5 | 0x4000 | `02 0e 00 00 02 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Armure volante** | 91 | **131** | **61** | 40 | 40 | 0 | 48 | `80 100 71 73 30 60 50 51` |
| 1 | **Sylphide** | 100 | **563** | **218** | 71 | 70 | 1 | 180 | `84 93 68 68 40 90 60 56` |
| 2 | **Wybarn** | 96 | **700** | **43** | 72 | 109 | 0 | 40 | `106 109 70 72 32 84 54 56` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 4000 | 0 | -1000 | 2560 | 0x000e0b00 |
| 0 | 1 | 0 | 4000 | 0 | -1200 | 3584 | 0x000e0b00 |
| 0 | 2 | 0 | 4200 | 0 | -1000 | 1536 | 0x000e0b00 |
| 0 | 3 | 0 | 4200 | 0 | -1200 | 512 | 0x000e0b00 |
| 1 | 0 | 1 | -3800 | -400 | 100 | 3072 | 0x000f0b01 |
| 1 | 1 | 2 | -3800 | -400 | 100 | 3072 | 0x0000ff02 |
| 2 | 0 | 0 | 1750 | 0 | 500 | 0 | 0x00130b00 |
| 2 | 1 | 0 | 1650 | 0 | 500 | 2048 | 0x00130b00 |
| 2 | 2 | 0 | 1750 | 0 | -300 | 0 | 0x00130b00 |
| 2 | 3 | 0 | 1650 | 0 | -300 | 2048 | 0x00130b00 |
| 3 | 0 | 0 | 4500 | 4400 | -2100 | 3584 | 0x00140b00 |
| 3 | 1 | 0 | 3700 | 4400 | -2100 | 512 | 0x00140b00 |
| 3 | 2 | 0 | 3900 | 4400 | -1900 | 0 | 0x00140b00 |
| 3 | 3 | 0 | 4300 | 4400 | -1900 | 0 | 0x00140b00 |
| 4 | 0 | 0 | 4100 | 2000 | 200 | 1024 | 0x00150b00 |
| 4 | 1 | 0 | 4100 | 2000 | 0 | 1024 | 0x00150b00 |
| 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Cristal de magie `0` | Cristal de terre `1` | Cristal de feu `2` | Cristal d'air `3` | Cristal d'ombre `4` | Cristal de lumière `5` | Cristal saint `6` | Cristal du mal `7` |
| 1 | Carte de la peur `8` | Carte de tarot `9` | Carte de tarot `9` | Carte de la peur `8` | Carte de la peur `8` | Carte de tarot `9` | Carte de tarot `9` | Carte de la peur `8` |
| 2 | Bouclier de l'ombre `10` | Bouclier de l'ombre `10` | Poignard de sang `11` | Poignard de sang `11` | Bouclier de l'ombre `10` | Bouclier de l'ombre `10` | Anneau de Merlin `12` | Anneau de Merlin `12` |
| 3 | Elixir de vie `13` | Armure d'argent `14` | Cristal de magie `0` | Bouclier des runes `15` | Bouclier des runes `15` | Robe des runes `16` | Robe des runes `16` | Armure du gardien `17` |
| 4 | Bottes d'elfes `18` | Chapeau d'elfe `19` | Manteau d'elfe `20` | Bouclier des elfes `21` | Bouclier des elfes `21` | Arc des elfes `22` | Arc des elfes `22` | Arc des elfes `22` |
| 5 | Corne du dragon `23` | Armure du dragon `24` | Cendre verte `25` | Cendre verte `25` | Bottes ailées `26` | Bottes ailées `26` | Bottes ailées `26` | Bottes ailées `26` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.2 | coffre `28` | -164 | message | — | 3692 / 4440 / -2832 |
| 5.0 | coffre `28` | 190 | objet | rangee 0 : Cristal de magie (25 %), Cristal de terre (17 %), Cristal de feu (15 %), Cristal d'air (13 %), Cristal d'ombre (11 %), Cristal de lumière (9 %), Cristal saint (6 %), Cristal du mal (4 %) | 5900 / 4440 / 1700 |
| 5.1 | coffre `28` | 191 | objet | rangee 1 : Carte de la peur (25 %), Carte de tarot (17 %), Carte de tarot (15 %), Carte de la peur (13 %), Carte de la peur (11 %), Carte de tarot (9 %), Carte de tarot (6 %), Carte de la peur (4 %) | 5900 / 4440 / 1500 |
| 5.2 | coffre `28` | 176 | objet | rangee 2 : Bouclier de l'ombre (25 %), Bouclier de l'ombre (17 %), Poignard de sang (15 %), Poignard de sang (13 %), Bouclier de l'ombre (11 %), Bouclier de l'ombre (9 %), Anneau de Merlin (6 %), Anneau de Merlin (4 %) | 5900 / 4440 / 1300 |

### Zone 4, sous-zone 8 — repertoire de 14784 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 6 | 0 | 2 | 0x4000 | `00 06 00 00 00 02 00 40` |
| 1 | 9 | 1 | 3 | 0x4000 | `01 09 00 00 01 03 00 40` |
| 2 | 12 | 3 | 4 | 0x4000 | `02 0c 00 00 03 04 00 40` |
| 3 | 4 | 4 | 5 | 0x4000 | `03 04 00 00 04 05 00 40` |
| 4 | 30 | 5 | 6 | 0x6000 | `04 1e 00 00 05 06 00 60` |
| 5 | 11 | 2 | 6 | 0x4000 | `05 0b 00 00 02 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Crapouille** | 98 | **151** | **4** | 21 | 62 | 3 | 50 | `80 90 93 89 91 216 111 115` |
| 1 | **Archange noir** | 98 | **160** | **215** | 22 | 48 | 0 | 80 | `62 58 54 58 23 72 76 78` |
| 2 | **Gros dur** | 110 | **326** | **135** | 23 | 91 | 1 | 60 | `98 143 86 82 54 108 76 74` |
| 3 | **Sabre volant** | 80 | **80** | **130** | 26 | 40 | 0 | 45 | `70 48 63 61 21 44 62 59` |
| 4 | **Armure volante** | 96 | **131** | **61** | 40 | 40 | 0 | 48 | `80 100 71 73 30 60 50 51` |
| 5 | **Cristal maudit** | 255 | **900** | **1** | 0 | 0 | 0 | 0 | `0 0 0 0 0 0 0 0` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 2200 | 200 | 3000 | 0 | 0x00110a04 |
| 0 | 1 | 1 | 2200 | 300 | 3000 | 0 | 0x00100b01 |
| 0 | 2 | 2 | 2500 | 300 | 2850 | 0 | 0x00100b02 |
| 0 | 3 | 2 | 2500 | 300 | 3150 | 0 | 0x00100b02 |
| 0 | 4 | 2 | 2200 | 300 | 3300 | 0 | 0x00100b02 |
| 0 | 5 | 2 | 2200 | 300 | 2700 | 0 | 0x00100b02 |
| 0 | 6 | 2 | 1900 | 300 | 2850 | 0 | 0x00100b02 |
| 0 | 7 | 2 | 1900 | 300 | 3150 | 0 | 0x00100b02 |
| 1 | 0 | 4 | 4200 | 820 | -2200 | 0 | 0x00130a04 |
| 1 | 1 | 1 | 4200 | 840 | -1900 | 2048 | 0x00120b01 |
| 1 | 2 | 1 | 4200 | 840 | -2500 | 0 | 0x00120b01 |
| 1 | 3 | 2 | 3900 | 940 | -2050 | 0 | 0x00120b02 |
| 1 | 4 | 2 | 3900 | 940 | -2350 | 0 | 0x00120b02 |
| 1 | 5 | 2 | 4500 | 940 | -2050 | 0 | 0x00120b02 |
| 1 | 6 | 2 | 4500 | 940 | -2350 | 0 | 0x00120b02 |
| 2 | 0 | 4 | -2400 | 1060 | -4200 | 0 | 0x00150a04 |
| 2 | 1 | 3 | -2400 | 1080 | -4500 | 0 | 0x00140b03 |
| 2 | 2 | 3 | -2400 | 1080 | -3900 | 2048 | 0x00140b03 |
| 2 | 3 | 3 | -2700 | 1080 | -4200 | 1024 | 0x00140b03 |
| 2 | 4 | 3 | -2100 | 1080 | -4200 | 3072 | 0x00140b03 |
| 3 | 0 | 4 | -4400 | 420 | 1000 | 0 | 0x00170a04 |
| 3 | 1 | 2 | -4100 | 540 | 1450 | 0 | 0x00160b02 |
| 3 | 2 | 2 | -4100 | 540 | 850 | 0 | 0x00160b02 |
| 3 | 3 | 2 | -4700 | 540 | 1450 | 0 | 0x00160b02 |
| 3 | 4 | 2 | -4700 | 540 | 850 | 0 | 0x00160b02 |
| 3 | 5 | 3 | -4400 | 540 | 1300 | 2048 | 0x00160b03 |
| 3 | 6 | 3 | -4400 | 540 | 700 | 0 | 0x00160b03 |
| 4 | 0 | 4 | -2400 | 200 | 3000 | 0 | 0x00190a04 |
| 4 | 1 | 3 | -2100 | 200 | 2850 | 0 | 0x00180b03 |
| 4 | 2 | 3 | -2100 | 200 | 3150 | 0 | 0x00180b03 |
| 4 | 3 | 3 | -2700 | 200 | 2850 | 0 | 0x00180b03 |
| 4 | 4 | 3 | -2700 | 200 | 3150 | 0 | 0x00180b03 |
| 5 | 0 | 4 | 4200 | 440 | 1000 | 0 | 0x001b0a04 |
| 5 | 1 | 1 | 4050 | 540 | 800 | 512 | 0x001a0b01 |
| 5 | 2 | 1 | 4350 | 540 | 800 | 3584 | 0x001a0b01 |
| 5 | 3 | 3 | 4050 | 540 | 1100 | 2560 | 0x001a0b03 |
| 5 | 4 | 3 | 4350 | 540 | 1100 | 1536 | 0x001a0b03 |
| 6 | 0 | 4 | 2200 | 1060 | -4200 | 0 | 0x001d0a04 |
| 6 | 1 | 5 | 2000 | 1080 | -4200 | 1024 | 0x001c0b05 |
| 6 | 2 | 5 | 2400 | 1080 | -4200 | 3072 | 0x001c0b05 |
| 6 | 3 | 5 | 2200 | 1080 | -4050 | 2048 | 0x001c0b05 |
| 6 | 4 | 5 | 2200 | 1080 | -4350 | 0 | 0x001c0b05 |
| 7 | 0 | 4 | -4400 | 840 | -2200 | 0 | 0x001f0a04 |
| 7 | 1 | 2 | -4400 | 940 | -1900 | 0 | 0x001e0b02 |
| 7 | 2 | 2 | -4700 | 940 | -2050 | 0 | 0x001e0b02 |
| 7 | 3 | 2 | -4400 | 940 | -2350 | 0 | 0x001e0b02 |
| 7 | 4 | 2 | -4400 | 940 | -2500 | 0 | 0x001e0b02 |
| 7 | 5 | 2 | -4100 | 940 | -2050 | 0 | 0x001e0b02 |
| 7 | 6 | 2 | -4100 | 940 | -2350 | 0 | 0x001e0b02 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 0 | 5 | 0 | 0 | 0 | 0 | 0x0000ff05 |
| 9 | 1 | 5 | 0 | 0 | 0 | 0 | 0x0000ff05 |
| 10 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 1 | 5 | 0 | 0 | 0 | 0 | 0x0000ff05 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Flèche de faucon `0` | Casque d'argent `1` | Casque d'argent `1` | Flèche de faucon `0` | Renegeton `2` | Renegeton `2` | Renegeton `2` | Renegeton `2` |
| 1 | Talisman `3` | Talisman médicinal `4` | Bottes de sept lieux `5` | Bottes de sept lieux `5` | Aile de Séraphin `6` | Aile de Séraphin `6` | Baguette d'Ishtar `7` | Baguette d'Ishtar `7` |
| 2 | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` |
| 3 | Chaîne du mal `9` | Ecu du destin `10` | Cendre bleue `11` | Symbole des ténébres `12` | Bâton lunaire `13` | Bâton lunaire `13` | Robe des anciens `14` | Pentacle magique `15` |
| 4 | Epée du bâtard `16` | Craimore `17` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Epée de la fatalité `18` | Epée de Damas `19` | Epée du gardien `20` |
| 5 | Elixir de vie `21` | Armure d'argent `22` | Cristal de magie `8` | Bouclier des runes `23` | Bouclier des runes `23` | Robe des runes `24` | Robe des runes `24` | Armure du gardien `25` |
| 6 | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Corne du Minotaure `26` | Os de guerrier sauvage `27` | Cendre blanche `28` | Cendre blanche `28` | Corne du dragon `29` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 5.4 | coffre `28` | 182 | objet | rangee 0 : Flèche de faucon (25 %), Casque d'argent (17 %), Casque d'argent (15 %), Flèche de faucon (13 %), Renegeton (11 %), Renegeton (9 %), Renegeton (6 %), Renegeton (4 %) | -1500 / 1640 / -1500 |
| 5.5 | coffre `28` | 183 | objet | rangee 1 : Talisman (25 %), Talisman médicinal (17 %), Bottes de sept lieux (15 %), Bottes de sept lieux (13 %), Aile de Séraphin (11 %), Aile de Séraphin (9 %), Baguette d'Ishtar (6 %), Baguette d'Ishtar (4 %) | 1300 / 1640 / -1500 |

### Zone 4, sous-zone 9 — repertoire de 27672 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 10 | 0 | 1 | 0x4000 | `00 0a 00 00 00 01 00 40` |
| 1 | 15 | 1 | 2 | 0x4000 | `01 0f 00 00 01 02 00 40` |
| 2 | 13 | 2 | 3 | 0x4000 | `02 0d 00 00 02 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Platinum** | 120 | **400** | **194** | 25 | 100 | 1 | 100 | `109 117 105 157 58 199 63 164` |
| 1 | **Enchanteresse** | 140 | **910** | **840** | 73 | 100 | 3 | 200 | `90 120 76 72 130 400 120 120` |
| 2 | **Ténébreux** | 100 | **360** | **320** | 93 | 100 | 3 | 200 | `84 60 71 71 32 136 101 101` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 100 | 200 | -2200 | 2048 | 0x00060b01 |
| 0 | 1 | 2 | 600 | 200 | -2700 | 2560 | 0x0000ff02 |
| 0 | 2 | 2 | -400 | 200 | -2700 | 1536 | 0x0000ff02 |
| 1 | 0 | 0 | 200 | 200 | -200 | 0 | 0x00070b00 |
| 1 | 1 | 0 | 0 | 200 | -200 | 0 | 0x00070b00 |
| 1 | 2 | 0 | -100 | 200 | -200 | 0 | 0x00070b00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Plume dominicale `0` | Plume dominicale `0` | Flèche des Gouivres `1` | Flèche des Gouivres `1` | Bâton d'Apollon `2` | Baguette des anges `3` | Anneau des philosophes `4` | Bahil `5` |
| 1 | Ecu du destin `6` | Ecu du destin `6` | Gantelets d'argent `7` | Gantelets d'argent `7` | Epée de Damas `8` | Armure sacrée `9` | Armure sacrée `9` | Revatin `10` |
| 2 | Extrait d'esprits `11` | Baguette de l'alchimiste `12` | Anneau de Satan `13` | Anneau de Satan `13` | Robe magique `14` | Robe magique `14` | Plume dominicale `0` | Plume dominicale `0` |
| 3 | Corne du diable `15` | Corne du diable `15` | Corne du diable `15` | Corne du diable `15` | Corne du diable `15` | Corne du diable `15` | Corne du diable `15` | Corne du diable `15` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.0 | coffre `28` | 165 | objet | rangee 0 : Plume dominicale (25 %), Plume dominicale (17 %), Flèche des Gouivres (15 %), Flèche des Gouivres (13 %), Bâton d'Apollon (11 %), Baguette des anges (9 %), Anneau des philosophes (6 %), Bahil (4 %) | 600 / 240 / 2900 |

### Zone 4, sous-zone 10 — repertoire de 14680 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 6 | 0 | 2 | 0x4000 | `00 06 00 00 00 02 00 40` |
| 1 | 9 | 1 | 3 | 0x4000 | `01 09 00 00 01 03 00 40` |
| 2 | 12 | 3 | 4 | 0x4000 | `02 0c 00 00 03 04 00 40` |
| 3 | 4 | 4 | 5 | 0x4000 | `03 04 00 00 04 05 00 40` |
| 4 | 30 | 5 | 6 | 0x6000 | `04 1e 00 00 05 06 00 60` |
| 5 | 11 | 2 | 6 | 0x4000 | `05 0b 00 00 02 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Crapouille** | 98 | **151** | **204** | 21 | 62 | 3 | 50 | `80 90 93 89 91 216 111 115` |
| 1 | **Archange noir** | 98 | **160** | **215** | 22 | 48 | 0 | 80 | `62 58 54 58 23 72 76 78` |
| 2 | **Gros dur** | 110 | **326** | **135** | 23 | 91 | 1 | 60 | `102 143 102 122 104 108 90 166` |
| 3 | **Sabre volant** | 80 | **80** | **130** | 26 | 40 | 0 | 45 | `70 48 63 61 21 44 62 59` |
| 4 | **Armure volante** | 96 | **131** | **61** | 40 | 40 | 0 | 48 | `80 100 71 73 30 60 50 51` |
| 5 | **Cristal maudit** | 255 | **900** | **1** | 0 | 0 | 0 | 0 | `0 0 0 0 0 0 0 0` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | 2200 | 200 | 3000 | 0 | 0x00110a04 |
| 0 | 1 | 1 | 2200 | 300 | 3000 | 0 | 0x00100b01 |
| 0 | 2 | 2 | 2500 | 300 | 2850 | 0 | 0x00100b02 |
| 0 | 3 | 2 | 2500 | 300 | 3150 | 0 | 0x00100b02 |
| 0 | 4 | 2 | 2200 | 300 | 3300 | 0 | 0x00100b02 |
| 0 | 5 | 2 | 2200 | 300 | 2700 | 0 | 0x00100b02 |
| 0 | 6 | 2 | 1900 | 300 | 2850 | 0 | 0x00100b02 |
| 0 | 7 | 2 | 1900 | 300 | 3150 | 0 | 0x00100b02 |
| 1 | 0 | 4 | 4200 | 820 | -2200 | 0 | 0x00130a04 |
| 1 | 1 | 1 | 4200 | 840 | -1900 | 2048 | 0x00120b01 |
| 1 | 2 | 1 | 4200 | 840 | -2500 | 0 | 0x00120b01 |
| 1 | 3 | 2 | 3900 | 940 | -2050 | 0 | 0x00120b02 |
| 1 | 4 | 2 | 3900 | 940 | -2350 | 0 | 0x00120b02 |
| 1 | 5 | 2 | 4500 | 940 | -2050 | 0 | 0x00120b02 |
| 1 | 6 | 2 | 4500 | 940 | -2350 | 0 | 0x00120b02 |
| 2 | 0 | 4 | -2400 | 1060 | -4200 | 0 | 0x00150a04 |
| 2 | 1 | 3 | -2400 | 1080 | -4500 | 0 | 0x00140b03 |
| 2 | 2 | 3 | -2400 | 1080 | -3900 | 2048 | 0x00140b03 |
| 2 | 3 | 3 | -2700 | 1080 | -4200 | 1024 | 0x00140b03 |
| 2 | 4 | 3 | -2100 | 1080 | -4200 | 3072 | 0x00140b03 |
| 3 | 0 | 4 | -4400 | 420 | 1000 | 0 | 0x00170a04 |
| 3 | 1 | 2 | -4100 | 540 | 1450 | 0 | 0x00160b02 |
| 3 | 2 | 2 | -4100 | 540 | 850 | 0 | 0x00160b02 |
| 3 | 3 | 2 | -4700 | 540 | 1450 | 0 | 0x00160b02 |
| 3 | 4 | 2 | -4700 | 540 | 850 | 0 | 0x00160b02 |
| 3 | 5 | 3 | -4400 | 540 | 1300 | 2048 | 0x00160b03 |
| 3 | 6 | 3 | -4400 | 540 | 700 | 0 | 0x00160b03 |
| 4 | 0 | 4 | -2400 | 200 | 3000 | 0 | 0x00190a04 |
| 4 | 1 | 3 | -2100 | 200 | 2850 | 0 | 0x00180b03 |
| 4 | 2 | 3 | -2100 | 200 | 3150 | 0 | 0x00180b03 |
| 4 | 3 | 3 | -2700 | 200 | 2850 | 0 | 0x00180b03 |
| 4 | 4 | 3 | -2700 | 200 | 3150 | 0 | 0x00180b03 |
| 5 | 0 | 4 | 4200 | 440 | 1000 | 0 | 0x001b0a04 |
| 5 | 1 | 1 | 4050 | 540 | 800 | 512 | 0x001a0b01 |
| 5 | 2 | 1 | 4350 | 540 | 800 | 3584 | 0x001a0b01 |
| 5 | 3 | 3 | 4050 | 540 | 1100 | 2560 | 0x001a0b03 |
| 5 | 4 | 3 | 4350 | 540 | 1100 | 1536 | 0x001a0b03 |
| 6 | 0 | 4 | 2200 | 1060 | -4200 | 0 | 0x001d0a04 |
| 6 | 1 | 5 | 2000 | 1080 | -4200 | 1024 | 0x001c0b05 |
| 6 | 2 | 5 | 2400 | 1080 | -4200 | 3072 | 0x001c0b05 |
| 6 | 3 | 5 | 2200 | 1080 | -4050 | 2048 | 0x001c0b05 |
| 6 | 4 | 5 | 2200 | 1080 | -4350 | 0 | 0x001c0b05 |
| 7 | 0 | 4 | -4400 | 840 | -2200 | 0 | 0x001f0a04 |
| 7 | 1 | 2 | -4400 | 940 | -1900 | 0 | 0x001e0b02 |
| 7 | 2 | 2 | -4700 | 940 | -2050 | 0 | 0x001e0b02 |
| 7 | 3 | 2 | -4400 | 940 | -2350 | 0 | 0x001e0b02 |
| 7 | 4 | 2 | -4400 | 940 | -2500 | 0 | 0x001e0b02 |
| 7 | 5 | 2 | -4100 | 940 | -2050 | 0 | 0x001e0b02 |
| 7 | 6 | 2 | -4100 | 940 | -2350 | 0 | 0x001e0b02 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 0 | 5 | 0 | 0 | 0 | 0 | 0x0000ff05 |
| 9 | 1 | 5 | 0 | 0 | 0 | 0 | 0x0000ff05 |
| 10 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Flèche de faucon `0` | Casque d'argent `1` | Casque d'argent `1` | Flèche de faucon `0` | Renegeton `2` | Renegeton `2` | Renegeton `2` | Renegeton `2` |
| 1 | Talisman `3` | Talisman médicinal `4` | Bottes de sept lieux `5` | Bottes de sept lieux `5` | Aile de Séraphin `6` | Aile de Séraphin `6` | Baguette d'Ishtar `7` | Baguette d'Ishtar `7` |
| 2 | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` |
| 3 | Chaîne du mal `9` | Ecu du destin `10` | Cendre bleue `11` | Symbole des ténébres `12` | Bâton lunaire `13` | Bâton lunaire `13` | Robe des anciens `14` | Pentacle magique `15` |
| 4 | Epée du bâtard `16` | Craimore `17` | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Epée de la fatalité `18` | Epée de Damas `19` | Epée du gardien `20` |
| 5 | Elixir de vie `21` | Armure d'argent `22` | Cristal de magie `8` | Bouclier des runes `23` | Bouclier des runes `23` | Robe des runes `24` | Robe des runes `24` | Armure du gardien `25` |
| 6 | Cristal de magie `8` | Cristal de magie `8` | Cristal de magie `8` | Corne du Minotaure `26` | Os de guerrier sauvage `27` | Cendre blanche `28` | Cendre blanche `28` | Corne du dragon `29` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 5.4 | coffre `28` | 182 | objet | rangee 0 : Flèche de faucon (25 %), Casque d'argent (17 %), Casque d'argent (15 %), Flèche de faucon (13 %), Renegeton (11 %), Renegeton (9 %), Renegeton (6 %), Renegeton (4 %) | -1500 / 1640 / -1500 |
| 5.5 | coffre `28` | 183 | objet | rangee 1 : Talisman (25 %), Talisman médicinal (17 %), Bottes de sept lieux (15 %), Bottes de sept lieux (13 %), Aile de Séraphin (11 %), Aile de Séraphin (9 %), Baguette d'Ishtar (6 %), Baguette d'Ishtar (4 %) | 1300 / 1640 / -1500 |

## Zone 5 — 2672 octets, 10 sous-zone(s)

### Zone 5, sous-zone 0 — repertoire de 24196 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 3 | 0x4000 | `00 00 00 00 00 03 00 40` |
| 1 | 1 | 1 | 4 | 0x4000 | `01 01 00 00 01 04 00 40` |
| 2 | 2 | 2 | 5 | 0x4000 | `02 02 00 00 02 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Tas d'os** | 43 | **101** | **11** | 32 | 40 | 2 | 70 | `44 28 32 32 19 32 22 22` |
| 1 | **Zombie** | 35 | **90** | **12** | 27 | 35 | 1 | 65 | `40 35 33 33 20 36 24 24` |
| 2 | **Dunoeud** | 48 | **120** | **26** | 28 | 13 | 1 | 50 | `46 37 36 36 28 40 28 28` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -3491 | 5 | 1723 | 0 | 0x00410b01 |
| 0 | 1 | 1 | -2803 | 5 | 1792 | 0 | 0x00410b01 |
| 1 | 0 | 1 | -2880 | 5 | -1456 | 0 | 0x00420b01 |
| 1 | 1 | 1 | -3355 | 5 | -1494 | 0 | 0x00420b01 |
| 2 | 0 | 1 | 3605 | 5 | 1698 | 0 | 0x00430b01 |
| 2 | 1 | 1 | 3061 | 5 | 1858 | 0 | 0x00430b01 |
| 3 | 0 | 1 | 3013 | 5 | -1526 | 0 | 0x00440b01 |
| 3 | 1 | 1 | 3301 | 5 | -1806 | 0 | 0x00440b01 |
| 4 | 0 | 1 | -1899 | 5 | 97 | 2937 | 0x00450b01 |
| 4 | 1 | 1 | -1529 | 5 | 97 | 2937 | 0x00450b01 |
| 5 | 0 | 1 | 1433 | 5 | 113 | 847 | 0x00460b01 |
| 5 | 1 | 1 | 1977 | 5 | 81 | 1012 | 0x00460b01 |
| 6 | 0 | 1 | -1891 | 5 | 1675 | 3102 | 0x00470b01 |
| 6 | 1 | 1 | -1483 | 5 | 1699 | 2706 | 0x00470b01 |
| 7 | 0 | 1 | -1547 | 5 | -1475 | 2629 | 0x00480b01 |
| 7 | 1 | 1 | -1915 | 5 | -1507 | 3102 | 0x00480b01 |
| 8 | 0 | 1 | 1819 | 5 | 1669 | 1012 | 0x00490b01 |
| 8 | 1 | 1 | 1483 | 5 | 1693 | 1166 | 0x00490b01 |
| 9 | 0 | 1 | 1843 | 5 | -1452 | 1243 | 0x004a0b01 |
| 9 | 1 | 1 | 1483 | 5 | -1524 | 770 | 0x004a0b01 |
| 10 | 0 | 1 | -2846 | 5 | 401 | 0 | 0x004b0b01 |
| 10 | 1 | 1 | -2846 | 5 | -183 | 0 | 0x004b0b01 |
| 11 | 0 | 1 | 2918 | 5 | -284 | 0 | 0x004c0b01 |
| 11 | 1 | 1 | 3126 | 5 | 393 | 0 | 0x004c0b01 |
| 12 | 0 | 1 | 2998 | 5 | 3207 | 2552 | 0x004d0b01 |
| 12 | 1 | 1 | 3366 | 5 | 3287 | 2244 | 0x004d0b01 |
| 12 | 2 | 1 | 2406 | 5 | 4508 | 3025 | 0x004d0b01 |
| 12 | 3 | 1 | 3030 | 5 | 4596 | 3256 | 0x004d0b01 |
| 12 | 4 | 0 | 3192 | 5 | 5601 | 616 | 0x0000ff00 |
| 12 | 5 | 0 | 3256 | 5 | 5809 | 1166 | 0x0000ff00 |
| 13 | 0 | 1 | -2780 | 5 | 1400 | 0 | 0x0000ff01 |
| 14 | 0 | 1 | -2780 | 5 | 2000 | 0 | 0x0000ff01 |
| 15 | 0 | 1 | -3580 | 5 | 2000 | 0 | 0x0000ff01 |
| 16 | 0 | 1 | -3580 | 5 | 1400 | 0 | 0x0000ff01 |
| 17 | 0 | 1 | -3180 | 5 | -1800 | 0 | 0x0000ff01 |
| 18 | 0 | 1 | -2780 | 5 | -1800 | 0 | 0x0000ff01 |
| 19 | 0 | 1 | -2780 | 5 | -1200 | 0 | 0x0000ff01 |
| 20 | 0 | 1 | -3580 | 5 | -1200 | 0 | 0x0000ff01 |
| 21 | 0 | 1 | 2820 | 5 | 2000 | 0 | 0x0000ff01 |
| 22 | 0 | 1 | 3220 | 5 | 2000 | 0 | 0x0000ff01 |
| 23 | 0 | 1 | 3620 | 5 | 2000 | 0 | 0x0000ff01 |
| 24 | 0 | 1 | 3620 | 5 | 1400 | 0 | 0x0000ff01 |
| 25 | 0 | 1 | 2820 | 5 | -1200 | 0 | 0x0000ff01 |
| 26 | 0 | 1 | 2820 | 5 | -1800 | 0 | 0x0000ff01 |
| 27 | 0 | 1 | 3220 | 5 | -1800 | 0 | 0x0000ff01 |
| 28 | 0 | 1 | -2040 | 65 | 400 | 0 | 0x0000ff01 |
| 29 | 0 | 1 | -2040 | 65 | -200 | 0 | 0x0000ff01 |
| 30 | 0 | 1 | -1320 | 65 | 400 | 0 | 0x0000ff01 |
| 31 | 0 | 1 | 1720 | 65 | -200 | 0 | 0x0000ff01 |
| 32 | 0 | 1 | 2080 | 65 | 400 | 0 | 0x0000ff01 |
| 33 | 0 | 1 | -1320 | 65 | 2000 | 0 | 0x0000ff01 |
| 34 | 0 | 1 | -2040 | 65 | 2000 | 0 | 0x0000ff01 |
| 35 | 0 | 1 | -2040 | 65 | 1400 | 0 | 0x0000ff01 |
| 36 | 0 | 1 | -1680 | 65 | 1400 | 0 | 0x0000ff01 |
| 37 | 0 | 1 | -1680 | 65 | -1200 | 0 | 0x0000ff01 |
| 38 | 0 | 1 | 2080 | 65 | 2000 | 0 | 0x0000ff01 |
| 39 | 0 | 1 | 1720 | 65 | 2000 | 0 | 0x0000ff01 |
| 40 | 0 | 1 | 1360 | 65 | 2000 | 0 | 0x0000ff01 |
| 41 | 0 | 1 | 1720 | 65 | 1400 | 0 | 0x0000ff01 |
| 42 | 0 | 1 | 2080 | 65 | -1800 | 0 | 0x0000ff01 |
| 43 | 0 | 1 | 1720 | 65 | -1200 | 0 | 0x0000ff01 |
| 44 | 0 | 1 | -3580 | 5 | -300 | 0 | 0x0000ff01 |
| 45 | 0 | 1 | -2780 | 5 | -300 | 0 | 0x0000ff01 |
| 46 | 0 | 1 | -2780 | 5 | 500 | 0 | 0x0000ff01 |
| 47 | 0 | 1 | -3580 | 5 | 500 | 0 | 0x0000ff01 |
| 48 | 0 | 1 | 2820 | 5 | 500 | 0 | 0x0000ff01 |
| 49 | 0 | 1 | 2820 | 5 | -300 | 0 | 0x0000ff01 |
| 50 | 0 | 1 | 3220 | 5 | -300 | 0 | 0x0000ff01 |
| 51 | 0 | 1 | 3220 | 5 | 500 | 0 | 0x0000ff01 |
| 52 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 52 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 53 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 53 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 54 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 54 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 54 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 55 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Talisman médicinal `0` | Gants sacrés `1` | Hache en demi-lune `2` | Hache en demi-lune `2` | Gants sacrés `1` | Talisman médicinal `0` | Bâton des sages `3` | Bâton des sages `3` |
| 1 | Balle de cristal `4` | Balle de cristal `4` | Balle de cristal `4` | Balle de cristal `4` | Balle de cristal `4` | Balle de cristal `4` | Balle de cristal `4` | Balle de cristal `4` |
| 2 | Robe magique `5` | Robe magique `5` | Robe magique `5` | Robe magique `5` | Robe magique `5` | Robe magique `5` | Robe magique `5` | Robe magique `5` |
| 3 | Bouclier rond `6` | Large épée `7` | Pavois de chevalier `8` | Pavois de chevalier `8` | Ecu du destin `9` | Ecu du destin `9` | Cendre blanche `10` | Cendre blanche `10` |
| 4 | Châle précieux `11` | Potion med `12` | Potion med `12` | Talisman `13` | Ecu du destin `9` | Ecu du destin `9` | Cendre grise `14` | Cendre grise `14` |
| 5 | Potion olympique `15` | Potion olympique `15` | Potion olympique `15` | Potion olympique `15` | Corde du retour `16` | Corde du retour `16` | Cendre rouge `17` | Cendre rouge `17` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.0 | coffre `28` | 213 | objet | rangee 1 : Balle de cristal (25 %), Balle de cristal (17 %), Balle de cristal (15 %), Balle de cristal (13 %), Balle de cristal (11 %), Balle de cristal (9 %), Balle de cristal (6 %), Balle de cristal (4 %) | -2080 / 40 / -2900 |
| 1.4 | coffre `28` | 201 | objet | rangee 0 : Talisman médicinal (25 %), Gants sacrés (17 %), Hache en demi-lune (15 %), Hache en demi-lune (13 %), Gants sacrés (11 %), Talisman médicinal (9 %), Bâton des sages (6 %), Bâton des sages (4 %) | 1220 / 40 / -3200 |
| 2.48 | coffre `28` | 273 | objet | rangee 2 : Robe magique (25 %), Robe magique (17 %), Robe magique (15 %), Robe magique (13 %), Robe magique (11 %), Robe magique (9 %), Robe magique (6 %), Robe magique (4 %) | 2800 / 40 / 3500 |

### Zone 5, sous-zone 1 — repertoire de 20556 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 1 | 2 | 0x4000 | `00 01 00 00 01 02 00 40` |
| 1 | 2 | 2 | 3 | 0x4000 | `01 02 00 00 02 03 00 40` |
| 2 | 0 | 0 | 1 | 0x4000 | `02 00 00 00 00 01 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Tas d'os** | 43 | **101** | **11** | 32 | 40 | 2 | 70 | `44 28 32 32 19 32 22 22` |
| 1 | **Zombie** | 35 | **90** | **12** | 27 | 35 | 1 | 65 | `40 35 33 33 20 36 24 24` |
| 2 | **Dunoeud** | 48 | **120** | **26** | 28 | 13 | 1 | 50 | `46 37 36 36 28 40 28 28` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 303 | 5 | 36 | 1936 | 0x002c0b00 |
| 0 | 1 | 0 | 127 | 5 | 484 | 2013 | 0x002c0b00 |
| 0 | 2 | 0 | -45 | 5 | 858 | 2013 | 0x002c0b00 |
| 1 | 0 | 0 | -686 | -195 | 4390 | 1782 | 0x002d0b00 |
| 1 | 1 | 0 | -358 | -195 | 4530 | 1859 | 0x002d0b00 |
| 1 | 2 | 0 | -1518 | 5 | 4474 | 2706 | 0x002d0b00 |
| 1 | 3 | 0 | -1606 | 5 | 4142 | 2629 | 0x002d0b00 |
| 2 | 0 | 0 | -1245 | -395 | 1620 | 1243 | 0x002e0b00 |
| 2 | 1 | 0 | -1321 | -395 | 1928 | 1628 | 0x002e0b00 |
| 2 | 2 | 0 | -1901 | -395 | 1736 | 2244 | 0x002e0b00 |
| 2 | 3 | 0 | -2173 | -395 | 1828 | 2013 | 0x002e0b00 |
| 3 | 0 | 0 | 1027 | -595 | 302 | 3410 | 0x002f0b00 |
| 3 | 1 | 0 | 990 | -595 | 657 | 3102 | 0x002f0b00 |
| 3 | 2 | 0 | 1626 | -595 | 873 | 1551 | 0x002f0b00 |
| 3 | 3 | 0 | 1335 | -595 | 254 | 3410 | 0x002f0b00 |
| 4 | 0 | 1 | 609 | 5 | 4158 | 3806 | 0x00300b01 |
| 4 | 1 | 1 | 816 | 5 | 4430 | 165 | 0x00300b01 |
| 5 | 0 | 0 | 2800 | -600 | 200 | 0 | 0x00310b00 |
| 5 | 1 | 0 | 2600 | -600 | 300 | 1024 | 0x00310b00 |
| 5 | 2 | 0 | 3100 | -600 | 600 | 0 | 0x00310b00 |
| 5 | 3 | 0 | 3200 | -500 | 1000 | 2048 | 0x00310b00 |
| 5 | 4 | 0 | 3400 | -480 | 400 | 3072 | 0x00310b00 |
| 5 | 5 | 0 | 3800 | -600 | 200 | 3072 | 0x00310b00 |
| 6 | 0 | 0 | -260 | -395 | -3552 | 0 | 0x00320b00 |
| 6 | 1 | 0 | 760 | -395 | -3475 | 0 | 0x00320b00 |
| 6 | 2 | 0 | 840 | -395 | -3019 | 0 | 0x00320b00 |
| 6 | 3 | 0 | -304 | -395 | -2974 | 0 | 0x00320b00 |
| 7 | 0 | 1 | -4176 | 5 | 2688 | 0 | 0x00330b01 |
| 7 | 1 | 1 | -4023 | 5 | 3294 | 0 | 0x00330b01 |
| 8 | 0 | 0 | -3214 | -395 | 351 | 0 | 0x00340b00 |
| 8 | 1 | 0 | -2846 | -395 | -53 | 0 | 0x00340b00 |
| 9 | 0 | 0 | 1654 | 5 | -1215 | 0 | 0x00350b00 |
| 9 | 1 | 0 | 2342 | 5 | -1303 | 0 | 0x00350b00 |
| 11 | 0 | 0 | 500 | 5 | 800 | 2013 | 0x0000ff00 |
| 12 | 0 | 0 | -100 | 5 | 800 | 1936 | 0x0000ff00 |
| 13 | 0 | 0 | -100 | 5 | 400 | 2013 | 0x0000ff00 |
| 14 | 0 | 0 | -1900 | -395 | 2100 | 2244 | 0x0000ff00 |
| 15 | 0 | 0 | -1300 | -395 | 2100 | 2013 | 0x0000ff00 |
| 16 | 0 | 0 | 1040 | -595 | 300 | 1551 | 0x0000ff00 |
| 17 | 0 | 0 | 1300 | -595 | 300 | 3410 | 0x0000ff00 |
| 18 | 0 | 0 | 560 | 5 | 4100 | 1551 | 0x0000ff00 |
| 19 | 0 | 0 | 840 | 5 | 4100 | 3410 | 0x0000ff00 |
| 20 | 0 | 0 | -4240 | 5 | 2700 | 0 | 0x0000ff00 |
| 21 | 0 | 0 | -3960 | 5 | 3500 | 0 | 0x0000ff00 |
| 22 | 0 | 0 | -4240 | 5 | 3500 | 0 | 0x0000ff00 |
| 23 | 0 | 0 | -3960 | 5 | 2700 | 0 | 0x0000ff00 |
| 24 | 0 | 0 | -2600 | -395 | -50 | 1551 | 0x0000ff00 |
| 25 | 0 | 0 | -2600 | -395 | 470 | 3410 | 0x0000ff00 |
| 26 | 0 | 0 | -2760 | -395 | -1600 | 1551 | 0x0000ff00 |
| 27 | 0 | 0 | -2300 | -395 | -1600 | 3410 | 0x0000ff00 |
| 28 | 0 | 0 | -1840 | -395 | -1200 | 1551 | 0x0000ff00 |
| 29 | 0 | 0 | -2300 | -395 | -1200 | 3410 | 0x0000ff00 |
| 30 | 0 | 0 | -2760 | -395 | -1200 | 1551 | 0x0000ff00 |
| 31 | 0 | 2 | -1840 | -395 | -1600 | 3410 | 0x0000ff02 |
| 32 | 0 | 0 | 3340 | -500 | -150 | 2560 | 0x00550b00 |
| 32 | 1 | 0 | 3200 | -500 | -150 | 2048 | 0x00550b00 |
| 32 | 2 | 0 | 3050 | -550 | -60 | 1536 | 0x00550b00 |
| 32 | 3 | 0 | 3240 | -550 | -50 | 2000 | 0x00550b00 |
| 33 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 33 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 34 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 34 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 35 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 35 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 35 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 36 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 36 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Elixir de vie `0` | Elixir de magicien `1` | Elixir de magicien `1` | Elixir de vie `0` | Poudre de zombie `2` | Poudre de zombie `2` | Poudre de zombie `2` | Poudre de zombie `2` |
| 1 | Bouclier rond `3` | Large épée `4` | Pavois de chevalier `5` | Pavois de chevalier `5` | Ecu du destin `6` | Ecu du destin `6` | Cendre blanche `7` | Cendre blanche `7` |
| 2 | Châle précieux `8` | Potion med `9` | Potion med `9` | Talisman `10` | Ecu du destin `6` | Ecu du destin `6` | Cendre grise `11` | Cendre grise `11` |
| 3 | Potion olympique `12` | Potion olympique `12` | Potion olympique `12` | Potion olympique `12` | Corde du retour `13` | Corde du retour `13` | Cendre rouge `14` | Cendre rouge `14` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.3 | coffre `28` | 202 | objet | rangee 0 : Elixir de vie (25 %), Elixir de magicien (17 %), Elixir de magicien (15 %), Elixir de vie (13 %), Poudre de zombie (11 %), Poudre de zombie (9 %), Poudre de zombie (6 %), Poudre de zombie (4 %) | 850 / -560 / 1200 |

### Zone 5, sous-zone 2 — repertoire de 16352 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 1 | 2 | 0x4000 | `00 01 00 00 01 02 00 40` |
| 1 | 2 | 2 | 3 | 0x4000 | `01 02 00 00 02 03 00 40` |
| 2 | 0 | 0 | 4 | 0x4000 | `02 00 00 00 00 04 00 40` |
| 3 | 3 | 3 | 5 | 0x4000 | `03 03 00 00 03 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Tas d'os** | 43 | **101** | **11** | 32 | 40 | 2 | 70 | `44 28 32 32 19 32 22 22` |
| 1 | **Zombie** | 35 | **90** | **12** | 27 | 35 | 1 | 65 | `40 35 33 33 20 36 24 24` |
| 2 | **Dunoeud** | 48 | **120** | **26** | 28 | 13 | 1 | 50 | `46 37 36 36 28 40 28 28` |
| 3 | **Persil** | 53 | **132** | **37** | 36 | 53 | 1 | 60 | `52 45 38 36 51 45 39 35` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 700 | 5 | 0 | 0 | 0x00100b02 |
| 0 | 1 | 2 | 800 | 5 | -500 | 0 | 0x00100b02 |
| 1 | 0 | 2 | -2300 | -295 | 2700 | 0 | 0x00110b02 |
| 1 | 1 | 2 | -1800 | -295 | 2700 | 0 | 0x00110b02 |
| 1 | 2 | 2 | -2000 | -395 | 2200 | 0 | 0x00110b02 |
| 1 | 3 | 2 | -2500 | -395 | 2300 | 0 | 0x00110b02 |
| 2 | 0 | 0 | 900 | 5 | 2500 | 0 | 0x00120b00 |
| 2 | 1 | 0 | 1200 | 5 | 2600 | 0 | 0x00120b00 |
| 2 | 2 | 0 | 1300 | 5 | 2900 | 0 | 0x00120b00 |
| 2 | 3 | 0 | 800 | 5 | 4300 | 0 | 0x00120b00 |
| 2 | 4 | 0 | 2600 | 5 | 2900 | 0 | 0x00120b00 |
| 2 | 5 | 0 | 2300 | 5 | 1800 | 0 | 0x00120b00 |
| 2 | 6 | 1 | 1300 | 5 | 2600 | 0 | 0x00120b01 |
| 2 | 7 | 1 | 1200 | 5 | 1400 | 0 | 0x00120b01 |
| 2 | 8 | 1 | -300 | -200 | 5000 | 0 | 0x00120b01 |
| 3 | 0 | 0 | 1200 | -395 | -4600 | 0 | 0x00130b00 |
| 3 | 1 | 0 | 700 | -395 | -4200 | 0 | 0x00130b00 |
| 3 | 2 | 0 | 1200 | -395 | -3900 | 0 | 0x00130b00 |
| 3 | 3 | 1 | 1200 | -400 | -4300 | 0 | 0x00130b01 |
| 3 | 4 | 1 | 700 | -400 | -4600 | 0 | 0x00130b01 |
| 3 | 5 | 1 | 1000 | -200 | -4300 | 0 | 0x00130b01 |
| 3 | 6 | 1 | 500 | -200 | -4900 | 0 | 0x00130b01 |
| 4 | 0 | 1 | 200 | 0 | 1700 | 0 | 0x00140b01 |
| 4 | 1 | 1 | 400 | 0 | 1500 | 0 | 0x00140b01 |
| 5 | 0 | 0 | 300 | -200 | -1500 | 0 | 0x00150b00 |
| 5 | 1 | 0 | 600 | -200 | -1700 | 0 | 0x00150b00 |
| 5 | 2 | 0 | 900 | -200 | -1600 | 0 | 0x00150b00 |
| 5 | 3 | 0 | 600 | -200 | -2100 | 0 | 0x00150b00 |
| 5 | 4 | 2 | 300 | -200 | -2000 | 0 | 0x00150b02 |
| 5 | 5 | 1 | 400 | -200 | -1800 | 0 | 0x00150b01 |
| 6 | 0 | 0 | -2200 | -200 | 1200 | 0 | 0x00160b00 |
| 6 | 1 | 0 | -2500 | -200 | 800 | 0 | 0x00160b00 |
| 6 | 2 | 1 | -2000 | -200 | 700 | 0 | 0x00160b01 |
| 6 | 3 | 1 | -2500 | -200 | 600 | 0 | 0x00160b01 |
| 7 | 0 | 0 | 2300 | 0 | -3500 | 0 | 0x00170b00 |
| 7 | 1 | 0 | 3200 | -400 | -2700 | 0 | 0x00170b00 |
| 7 | 2 | 0 | 2000 | -400 | 0 | 0 | 0x00170b00 |
| 7 | 3 | 0 | 2500 | -400 | -300 | 0 | 0x00170b00 |
| 7 | 4 | 1 | 2000 | -200 | -3800 | 0 | 0x00170b01 |
| 7 | 5 | 1 | 3600 | -400 | -1900 | 0 | 0x00170b01 |
| 7 | 6 | 1 | 3000 | -400 | -1200 | 0 | 0x00170b01 |
| 7 | 7 | 1 | 2700 | -400 | -3000 | 0 | 0x00170b01 |
| 8 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 11 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Panoramix `0` | Panoramix `0` | Croix `1` | Croix `1` | Cendre blanche `2` | Cendre bleue `3` | Pointe de sang `4` | Pointe de sang `4` |
| 1 | Bouclier de brigand `5` | Bouclier de brigand `5` | Bouclier de brigand `5` | Bouclier de brigand `5` | Bouclier de brigand `5` | Bouclier de brigand `5` | Bouclier de brigand `5` | Bouclier de brigand `5` |
| 2 | Châle précieux `6` | Potion med `7` | Potion med `7` | Talisman `8` | Ecu du destin `9` | Ecu du destin `9` | Cendre grise `10` | Cendre grise `10` |
| 3 | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` | Corde du retour `12` | Corde du retour `12` | Cendre rouge `13` | Cendre rouge `13` |
| 4 | Bouclier rond `14` | Large épée `15` | Pavois de chevalier `16` | Pavois de chevalier `16` | Ecu du destin `9` | Ecu du destin `9` | Cendre blanche `2` | Cendre blanche `2` |
| 5 | Hache de guerre `17` | Epée du bâtard `18` | Long marteau `19` | Potion olympique `11` | Potion olympique `11` | Potion olympique `11` | Cendre verte `20` | Cendre verte `20` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.3 | coffre `28` | 203 | objet | rangee 0 : Panoramix (25 %), Panoramix (17 %), Croix (15 %), Croix (13 %), Cendre blanche (11 %), Cendre bleue (9 %), Pointe de sang (6 %), Pointe de sang (4 %) | 4112 / -560 / -3052 |
| 3.29 | coffre `28` | 271 | objet | rangee 1 : Bouclier de brigand (25 %), Bouclier de brigand (17 %), Bouclier de brigand (15 %), Bouclier de brigand (13 %), Bouclier de brigand (11 %), Bouclier de brigand (9 %), Bouclier de brigand (6 %), Bouclier de brigand (4 %) | -4100 / 40 / -3740 |

### Zone 5, sous-zone 3 — repertoire de 8428 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 5 | 0 | 2 | 0x4000 | `00 05 00 00 00 02 00 40` |
| 1 | 4 | 1 | 3 | 0x4000 | `01 04 00 00 01 03 00 40` |
| 2 | 0 | 2 | 4 | 0x4000 | `02 00 00 00 02 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Belle rouge** | 53 | **90** | **13** | 7 | 54 | 0 | 50 | `34 120 51 52 31 50 69 68` |
| 1 | **Mort-vivant** | 67 | **105** | **72** | 30 | 36 | 3 | 40 | `50 69 70 72 78 56 48 51` |
| 2 | **Sanguinaire** | 62 | **141** | **71** | 32 | 40 | 52 | 70 | `58 44 32 32 39 52 42 42` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 1800 | 0 | 2000 | 0 | 0x001f0b00 |
| 0 | 1 | 0 | -500 | 400 | -1500 | 0 | 0x001f0b00 |
| 0 | 2 | 0 | 2100 | 400 | 1300 | 0 | 0x001f0b00 |
| 0 | 3 | 0 | 2000 | 400 | 1200 | 0 | 0x001f0b00 |
| 0 | 4 | 0 | -400 | 400 | -1400 | 0 | 0x001f0b00 |
| 0 | 5 | 1 | -500 | 400 | -1500 | 0 | 0x001f0b01 |
| 0 | 6 | 1 | 2100 | 400 | 1300 | 0 | 0x001f0b01 |
| 1 | 0 | 0 | 1000 | 400 | 4500 | 0 | 0x00200b00 |
| 1 | 1 | 0 | 1600 | 400 | 4300 | 0 | 0x00200b00 |
| 1 | 2 | 0 | 1500 | 400 | 3400 | 0 | 0x00200b00 |
| 1 | 3 | 1 | 1400 | 400 | 3300 | 0 | 0x00200b01 |
| 1 | 4 | 1 | 1200 | 400 | 4500 | 0 | 0x00200b01 |
| 2 | 0 | 0 | -700 | 600 | 3400 | 0 | 0x00210b00 |
| 2 | 1 | 0 | -1000 | 600 | 2800 | 0 | 0x00210b00 |
| 2 | 2 | 1 | -500 | 600 | 2800 | 0 | 0x00210b01 |
| 2 | 3 | 2 | -700 | 600 | 3200 | 0 | 0x00210b02 |
| 3 | 0 | 0 | -2700 | 400 | -400 | 0 | 0x00220b00 |
| 3 | 1 | 0 | -2600 | 400 | -800 | 0 | 0x00220b00 |
| 3 | 2 | 0 | -2400 | 400 | -1100 | 0 | 0x00220b00 |
| 3 | 3 | 1 | -1900 | 400 | -1100 | 0 | 0x00220b01 |
| 3 | 4 | 1 | -1800 | 400 | -1000 | 0 | 0x00220b01 |
| 4 | 0 | 0 | 300 | 400 | -1300 | 0 | 0x00230b00 |
| 4 | 1 | 0 | 200 | 400 | -1200 | 0 | 0x00230b00 |
| 4 | 2 | 1 | 1100 | 400 | -2000 | 0 | 0x00230b01 |
| 4 | 3 | 1 | 1400 | 400 | -2000 | 0 | 0x00230b01 |
| 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Rapière à poison `0` | Rapière à poison `0` | Eau bénite `1` | Eau bénite `1` | Croix `2` | Croix `2` | Croix `2` | Croix `2` |
| 1 | Bracelet d'argent `3` | Balle sacrée `4` | Balle sacrée `4` | Chaîne sacrée `5` | Chaîne sacrée `5` | Chaîne sacrée `5` | Chaîne sacrée `5` | Chaîne sacrée `5` |
| 2 | Potion med `6` | Potion med `6` | Potion med `6` | Potion med `6` | Elixir de vie `7` | Elixir de vie `7` | Cristal de feu `8` | Cristal de feu `8` |
| 3 | Flacon d'énergie `9` | Flacon d'énergie `9` | Flacon d'énergie `9` | Flacon d'énergie `9` | Robe de la terre `10` | Robe de la terre `10` | Cendre noire `11` | Cendre noire `11` |
| 4 | Craimore `12` | Minegosh `13` | Ecu du destin `14` | Ecu du destin `14` | Cendre rouge `15` | Cendre rouge `15` | Epée de sang `16` | Epée de sang `16` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `28` | 193 | objet | rangee 0 : Rapière à poison (25 %), Rapière à poison (17 %), Eau bénite (15 %), Eau bénite (13 %), Croix (11 %), Croix (9 %), Croix (6 %), Croix (4 %) | 1900 / -160 / 100 |
| 1.0 | coffre `28` | 194 | objet | rangee 1 : Bracelet d'argent (25 %), Balle sacrée (17 %), Balle sacrée (15 %), Chaîne sacrée (13 %), Chaîne sacrée (11 %), Chaîne sacrée (9 %), Chaîne sacrée (6 %), Chaîne sacrée (4 %) | -700 / 640 / 3700 |

### Zone 5, sous-zone 4 — repertoire de 12740 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 3 | 0 | 2 | 0x4000 | `00 03 00 00 00 02 00 40` |
| 1 | 2 | 1 | 3 | 0x4000 | `01 02 00 00 01 03 00 40` |
| 2 | 7 | 2 | 4 | 0x4000 | `02 07 00 00 02 04 00 40` |
| 3 | 0 | 3 | 5 | 0x4000 | `03 00 00 00 03 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Persil** | 53 | **132** | **37** | 36 | 48 | 1 | 60 | `49 45 38 36 41 36 35 35` |
| 1 | **Dunoeud** | 48 | **120** | **26** | 28 | 13 | 1 | 50 | `46 37 36 36 28 40 28 28` |
| 2 | **Durahan** | 80 | **808** | **190** | 74 | 90 | 0 | 200 | `63 70 64 70 56 65 63 66` |
| 3 | **Sanguinaire** | 70 | **241** | **71** | 32 | 40 | 52 | 70 | `58 54 32 56 39 52 42 42` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 1899 | -395 | 1855 | 3072 | 0x000f0b02 |
| 0 | 1 | 3 | 1600 | -395 | 2200 | 3072 | 0x0000ff03 |
| 0 | 2 | 3 | 1600 | -395 | 1600 | 3072 | 0x0000ff03 |
| 0 | 3 | 3 | 2100 | -395 | 1900 | 3072 | 0x0000ff03 |
| 1 | 0 | 0 | -1893 | 205 | 5230 | 0 | 0x0000ff00 |
| 1 | 1 | 0 | -2434 | 205 | 6113 | 0 | 0x0000ff00 |
| 1 | 2 | 0 | -1702 | 205 | 4887 | 0 | 0x0000ff00 |
| 1 | 3 | 0 | -2713 | 205 | 6431 | 0 | 0x0000ff00 |
| 2 | 0 | 0 | -3322 | 205 | 6126 | 0 | 0x0000ff00 |
| 2 | 1 | 0 | -3681 | 205 | 5935 | 0 | 0x0000ff00 |
| 2 | 2 | 0 | -3852 | 205 | 6338 | 0 | 0x0000ff00 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Epée d'argent `0` | Hache d'argent `1` | Marteau de prêtre `2` | Baguette du feu `3` | Poupée Dummy `4` | Poupée dansante `5` | Poupée dansante `5` | Poupée Dummy `4` |
| 1 | Os de guerrier sauvage `6` | Os de guerrier sauvage `6` | Bouclier d'argent `7` | Bouclier d'argent `7` | Balance des juges `8` | Balance des juges `8` | Epée gauloise `9` | Epée gauloise `9` |
| 2 | Hache de guerre `10` | Epée du bâtard `11` | Long marteau `12` | Potion olympique `13` | Potion olympique `13` | Potion olympique `13` | Cendre verte `14` | Cendre verte `14` |
| 3 | Potion olympique `13` | Potion olympique `13` | Potion olympique `13` | Potion olympique `13` | Corde du retour `15` | Corde du retour `15` | Cendre rouge `16` | Cendre rouge `16` |
| 4 | Epée de sang `17` | Epée de sang `17` | Balle maléfique `18` | Balle maléfique `18` | Balle maléfique `18` | Armure de Satan `19` | Armure de Satan `19` | Armure de Satan `19` |
| 5 | Craimore `20` | Minegosh `21` | Ecu du destin `22` | Ecu du destin `22` | Cendre rouge `16` | Cendre rouge `16` | Epée de sang `17` | Epée de sang `17` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.0 | coffre `28` | 204 | objet | rangee 0 : Epée d'argent (25 %), Hache d'argent (17 %), Marteau de prêtre (15 %), Baguette du feu (13 %), Poupée Dummy (11 %), Poupée dansante (9 %), Poupée dansante (6 %), Poupée Dummy (4 %) | -2250 / -260 / 1700 |
| 4.0 | coffre `28` | 239 | objet | rangee 1 : Os de guerrier sauvage (25 %), Os de guerrier sauvage (17 %), Bouclier d'argent (15 %), Bouclier d'argent (13 %), Balance des juges (11 %), Balance des juges (9 %), Epée gauloise (6 %), Epée gauloise (4 %) | -3900 / 240 / 6100 |

### Zone 5, sous-zone 5 — repertoire de 14400 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 10 | 0 | 4 | 0x4000 | `00 0a 00 00 00 04 00 40` |
| 1 | 4 | 2 | 6 | 0x4000 | `01 04 00 00 02 06 00 40` |
| 2 | 5 | 3 | 7 | 0x4000 | `02 05 00 00 03 07 00 40` |
| 3 | 8 | 4 | 8 | 0x4000 | `03 08 00 00 04 08 00 40` |
| 4 | 10 | 1 | 5 | 0x4000 | `00 0a 04 00 01 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Momie** | 78 | **142** | **33** | 29 | 82 | 0 | 50 | `83 70 58 54 31 77 40 35` |
| 1 | **Sanglante** | 95 | **190** | **63** | 29 | 72 | 4 | 60 | `94 80 70 77 31 90 55 53` |
| 2 | **Mort-vivant** | 67 | **105** | **72** | 30 | 36 | 3 | 40 | `50 69 70 72 78 56 48 51` |
| 3 | **Belle rouge** | 53 | **90** | **13** | 7 | 54 | 0 | 50 | `34 120 51 52 31 50 69 68` |
| 4 | **Agonisant** | 88 | **215** | **36** | 38 | 33 | 0 | 50 | `85 74 71 72 43 60 52 57` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -2998 | -1795 | 4742 | 0 | 0x0000ff00 |
| 0 | 1 | 0 | -3574 | -1795 | 4262 | 0 | 0x0000ff00 |
| 1 | 0 | 0 | -1708 | -595 | -1912 | 0 | 0x0000ff00 |
| 1 | 1 | 0 | -1920 | -595 | -1156 | 0 | 0x0000ff00 |
| 1 | 2 | 1 | -1436 | -395 | -952 | 0 | 0x0000ff01 |
| 1 | 3 | 1 | -2252 | -395 | -1508 | 0 | 0x0000ff01 |
| 1 | 4 | 1 | -2164 | -595 | -636 | 0 | 0x0000ff01 |
| 1 | 5 | 1 | -2100 | -395 | -20 | 0 | 0x0000ff01 |
| 2 | 0 | 0 | 226 | -1795 | 4318 | 0 | 0x0000ff00 |
| 2 | 1 | 0 | 806 | -1795 | 4058 | 0 | 0x0000ff00 |
| 2 | 2 | 1 | -470 | -1795 | 4482 | 0 | 0x0000ff01 |
| 2 | 3 | 1 | 622 | -1795 | 4334 | 0 | 0x0000ff01 |
| 2 | 4 | 1 | -114 | -1795 | 4458 | 0 | 0x0000ff01 |
| 2 | 5 | 1 | -978 | -1795 | 3998 | 0 | 0x0000ff01 |
| 3 | 0 | 1 | 1291 | -195 | -4120 | 0 | 0x00220b01 |
| 3 | 1 | 1 | 1475 | -195 | -4396 | 0 | 0x00220b01 |
| 3 | 2 | 2 | 807 | -195 | -4404 | 0 | 0x00220b02 |
| 3 | 3 | 2 | 1391 | -195 | -3732 | 0 | 0x00220b02 |
| 4 | 0 | 1 | 1889 | -195 | 5472 | 0 | 0x00200b01 |
| 4 | 1 | 1 | 2037 | -195 | 5184 | 0 | 0x00200b01 |
| 4 | 2 | 1 | 1485 | -195 | 5524 | 0 | 0x00200b01 |
| 4 | 3 | 2 | 1417 | -195 | 5836 | 0 | 0x00200b02 |
| 4 | 4 | 2 | 2001 | -195 | 5788 | 0 | 0x00200b02 |
| 5 | 0 | 0 | 817 | -595 | -1346 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 1209 | -595 | -1190 | 0 | 0x0000ff00 |
| 5 | 2 | 0 | 1049 | -595 | -782 | 0 | 0x0000ff00 |
| 5 | 3 | 0 | 1997 | -595 | 18 | 0 | 0x0000ff00 |
| 5 | 4 | 1 | 1609 | -395 | -882 | 0 | 0x0000ff01 |
| 5 | 5 | 1 | 1145 | -395 | -418 | 0 | 0x0000ff01 |
| 6 | 0 | 0 | -3298 | -395 | -642 | 0 | 0x00240b00 |
| 6 | 1 | 0 | -3646 | -395 | -70 | 0 | 0x00240b00 |
| 6 | 2 | 2 | -4034 | -395 | 50 | 0 | 0x00240b02 |
| 6 | 3 | 2 | -3358 | -395 | 30 | 0 | 0x00240b02 |
| 6 | 4 | 2 | -3998 | -395 | -614 | 0 | 0x00240b02 |
| 7 | 0 | 4 | -900 | 0 | -3780 | 0 | 0x0000ff04 |
| 8 | 0 | 4 | -1500 | 0 | -3420 | 2048 | 0x0000ff04 |
| 9 | 0 | 4 | -2100 | 0 | -3780 | 0 | 0x0000ff04 |
| 10 | 0 | 4 | -2020 | -400 | 300 | 3072 | 0x0000ff04 |
| 11 | 0 | 4 | -2380 | -400 | 900 | 1024 | 0x0000ff04 |
| 12 | 0 | 4 | -3820 | -400 | -700 | 3072 | 0x0000ff04 |
| 13 | 0 | 4 | -4180 | -400 | -700 | 1024 | 0x0000ff04 |
| 14 | 0 | 4 | -4180 | -400 | 100 | 1024 | 0x0000ff04 |
| 15 | 0 | 4 | 700 | 0 | -2620 | 2048 | 0x0000ff04 |
| 16 | 0 | 4 | 1300 | 0 | -2620 | 2048 | 0x0000ff04 |
| 17 | 0 | 4 | 1300 | 0 | -2980 | 0 | 0x0000ff04 |
| 18 | 0 | 4 | 1900 | 0 | -2980 | 0 | 0x0000ff04 |
| 19 | 0 | 4 | 1500 | -200 | -3620 | 2048 | 0x0000ff04 |
| 20 | 0 | 4 | 700 | -200 | -3620 | 2048 | 0x0000ff04 |
| 21 | 0 | 4 | 700 | -200 | -3980 | 0 | 0x0000ff04 |
| 22 | 0 | 4 | 3500 | -400 | 3380 | 2048 | 0x0000ff04 |
| 23 | 0 | 4 | 2100 | -200 | 5620 | 0 | 0x0000ff04 |
| 24 | 0 | 4 | 1300 | -200 | 5380 | 2048 | 0x0000ff04 |
| 25 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 25 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 25 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 25 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 26 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 26 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 26 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 26 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 27 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 27 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 27 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 27 | 3 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Chaîne de l'eau `0` | Chaîne du feu `1` | Chaîne de la terre `2` | Chaîne de l'air `3` | Chaîne de l'air `3` | Chaîne de la terre `2` | Chaîne du feu `1` | Chaîne de l'eau `0` |
| 1 | Poudre de zombie `4` | Poudre de zombie `4` | Poudre de zombie `4` | Poudre de zombie `4` | Poudre de zombie `4` | Poudre de zombie `4` | Poudre de zombie `4` | Poudre de zombie `4` |
| 2 | Elixir de vie `5` | Elixir de vie `5` | Elixir de vie `5` | Panoramix `6` | Panoramix `6` | Panoramix `6` | Panoramix `6` | Panoramix `6` |
| 3 | Elixir de vie `5` | Elixir de vie `5` | Balle de cristal `7` | Balle de cristal `7` | Balle de cristal `7` | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` |
| 4 | Huile du pouvoir `9` | Bague au joyau `10` | Bague sacrée `11` | Cendre bleue `12` | Cendre bleue `12` | Ambrosia `13` | Ambrosia `13` | Balance des juges `14` |
| 5 | Huile de sang `15` | Bague au joyau `10` | Bague sacrée `11` | Cendre rouge `16` | Cendre rouge `16` | Ambrosia `13` | Ambrosia `13` | Balance des juges `14` |
| 6 | Flacon d'énergie `17` | Flacon d'énergie `17` | Flacon d'énergie `17` | Flacon d'énergie `17` | Robe de la terre `18` | Robe de la terre `18` | Cendre noire `19` | Cendre noire `19` |
| 7 | Potion med `20` | Potion med `20` | Potion med `20` | Potion med `20` | Elixir de vie `5` | Elixir de vie `5` | Cristal de feu `21` | Cristal de feu `21` |
| 8 | Potion olympique `22` | Ecu du destin `23` | Epée d'argent `24` | Manteau de croisé `25` | Bannière de chevalier `26` | Bannière de chevalier `26` | Epée de la fatalité `27` | Epée de la fatalité `27` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.2 | coffre `28` | 223 | objet | rangee 0 : Chaîne de l'eau (25 %), Chaîne du feu (17 %), Chaîne de la terre (15 %), Chaîne de l'air (13 %), Chaîne de l'air (11 %), Chaîne de la terre (9 %), Chaîne du feu (6 %), Chaîne de l'eau (4 %) | -100 / 40 / -900 |
| 2.93 | coffre `28` | 268 | objet | rangee 1 : Poudre de zombie (25 %), Poudre de zombie (17 %), Poudre de zombie (15 %), Poudre de zombie (13 %), Poudre de zombie (11 %), Poudre de zombie (9 %), Poudre de zombie (6 %), Poudre de zombie (4 %) | 640 / -160 / -3800 |
| 3.62 | coffre `28` | 269 | objet | rangee 2 : Elixir de vie (25 %), Elixir de vie (17 %), Elixir de vie (15 %), Panoramix (13 %), Panoramix (11 %), Panoramix (9 %), Panoramix (6 %), Panoramix (4 %) | -4000 / -360 / -740 |
| 3.63 | coffre `28` | 270 | objet | rangee 3 : Elixir de vie (25 %), Elixir de vie (17 %), Balle de cristal (15 %), Balle de cristal (13 %), Balle de cristal (11 %), Cristal d'énergie (9 %), Cristal d'énergie (6 %), Cristal d'énergie (4 %) | 1240 / -160 / 5200 |

### Zone 5, sous-zone 6 — repertoire de 10380 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 0 | 3 | 0x4000 | `00 08 00 00 00 03 00 40` |
| 1 | 9 | 1 | 4 | 0x4000 | `01 09 00 00 01 04 00 40` |
| 2 | 10 | 2 | 5 | 0x4000 | `02 0a 00 00 02 05 00 40` |
| 3 | 11 | 3 | 6 | 0x4000 | `03 0b 00 00 03 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Agonisant** | 88 | **215** | **36** | 38 | 33 | 0 | 50 | `85 74 71 72 43 60 52 57` |
| 1 | **Ombre** | 85 | **93** | **172** | 39 | 89 | 0 | 44 | `74 140 27 98 16 171 76 127` |
| 2 | **Momie** | 78 | **142** | **33** | 29 | 82 | 0 | 50 | `83 70 58 54 31 77 40 35` |
| 3 | **Revenant** | 100 | **190** | **45** | 33 | 41 | 1 | 40 | `96 60 81 83 90 86 69 57` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 186 | 1468 | -497 | 0 | 0x0000ff01 |
| 0 | 1 | 1 | -78 | 1712 | -497 | 0 | 0x0000ff01 |
| 0 | 2 | 1 | 488 | 812 | 320 | 0 | 0x0000ff01 |
| 0 | 3 | 1 | 96 | 348 | 264 | 0 | 0x0000ff01 |
| 0 | 4 | 1 | 480 | 1212 | -184 | 0 | 0x0000ff01 |
| 1 | 0 | 2 | 2670 | -395 | -1795 | 0 | 0x0000ff02 |
| 1 | 1 | 2 | 2814 | -395 | -1935 | 0 | 0x0000ff02 |
| 1 | 2 | 2 | 2796 | -395 | -1503 | 0 | 0x0000ff02 |
| 1 | 3 | 2 | 2674 | -395 | -1629 | 0 | 0x0000ff02 |
| 1 | 4 | 2 | 2812 | -395 | -1667 | 0 | 0x0000ff02 |
| 2 | 0 | 3 | -2395 | 605 | -801 | 0 | 0x0000ff03 |
| 2 | 1 | 3 | -2363 | 605 | -531 | 0 | 0x0000ff03 |
| 3 | 0 | 2 | -1464 | -395 | 2335 | 0 | 0x0000ff02 |
| 3 | 1 | 2 | -1930 | -395 | 2073 | 0 | 0x0000ff02 |
| 3 | 2 | 2 | -1932 | -395 | 2205 | 0 | 0x0000ff02 |
| 3 | 3 | 2 | -1466 | -395 | 2205 | 0 | 0x0000ff02 |
| 3 | 4 | 2 | -1934 | -395 | 1941 | 0 | 0x0000ff02 |
| 4 | 0 | 0 | -432 | 1805 | -1519 | 0 | 0x0000ff00 |
| 4 | 1 | 0 | -216 | 1805 | -1351 | 0 | 0x0000ff00 |
| 4 | 2 | 1 | -2088 | 1805 | -399 | 0 | 0x0000ff01 |
| 4 | 3 | 1 | -1688 | 1805 | -431 | 0 | 0x0000ff01 |
| 4 | 4 | 1 | -1714 | 1805 | -311 | 0 | 0x0000ff01 |
| 5 | 0 | 0 | 1395 | 1205 | -647 | 693 | 0x0000ff00 |
| 5 | 1 | 0 | 1115 | 1205 | -439 | 1320 | 0x0000ff00 |
| 5 | 2 | 1 | 387 | 1205 | -2543 | 0 | 0x0000ff01 |
| 5 | 3 | 1 | 427 | 1205 | -2103 | 0 | 0x0000ff01 |
| 5 | 4 | 1 | 723 | 1205 | -2287 | 0 | 0x0000ff01 |
| 6 | 0 | 0 | 368 | 605 | 1217 | 2167 | 0x0000ff00 |
| 6 | 1 | 0 | 648 | 605 | 1129 | 1705 | 0x0000ff00 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 3 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 4 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 5 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 9 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 9 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 10 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 10 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Robe sacrée `0` | Crosse sacrée `1` | Chaîne sacrée `2` | Robe sacrée `0` | Crosse sacrée `1` | Crosse sacrée `1` | Robe sacrée `0` | Chaîne sacrée `2` |
| 1 | Gants des runes `3` | Gants des runes `3` | Solitaire `4` | Solitaire `4` | La dévoreuse `5` | La dévoreuse `5` | Dropneal `6` | Dropneal `6` |
| 2 | Masque de chevalier `7` | Masque de chevalier `7` | Boîte à bijoux `8` | Boîte à bijoux `8` | Anneau de l'hermite `9` | Anneau de l'hermite `9` | Plume dominicale `10` | Plume dominicale `10` |
| 3 | Potion olympique `11` | Ecu du destin `12` | Epée d'argent `13` | Manteau de croisé `14` | Bannière de chevalier `15` | Bannière de chevalier `15` | Epée de la fatalité `16` | Epée de la fatalité `16` |
| 4 | Balle des ténèbres `17` | Cristal d'ombre `18` | Pendentif vaporeux `19` | Robe de l'ombre `20` | Voile des ombres `21` | Voile des ombres `21` | Voile des ombres `21` | Voile des ombres `21` |
| 5 | Huile du pouvoir `22` | Bague au joyau `23` | Bague sacrée `24` | Cendre bleue `25` | Cendre bleue `25` | Ambrosia `26` | Ambrosia `26` | Balance des juges `27` |
| 6 | Poudre de zombie `28` | Ecu du destin `12` | Sauvagerie `29` | Ecu du destin `12` | Gants de force `30` | Bottes de sept lieux `31` | Bottes de sept lieux `31` | Gants de force `30` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `28` | 200 | message | — | -2750 / 690 / -700 |
| 0.3 | coffre `28` | 205 | objet | rangee 2 : Masque de chevalier (25 %), Masque de chevalier (17 %), Boîte à bijoux (15 %), Boîte à bijoux (13 %), Anneau de l'hermite (11 %), Anneau de l'hermite (9 %), Plume dominicale (6 %), Plume dominicale (4 %) | -1450 / -360 / 1900 |
| 2.2 | coffre `28` | 215 | objet | rangee 1 : Gants des runes (25 %), Gants des runes (17 %), Solitaire (15 %), Solitaire (13 %), La dévoreuse (11 %), La dévoreuse (9 %), Dropneal (6 %), Dropneal (4 %) | -1300 / 1240 / 100 |
| 3.2 | coffre `28` | 214 | objet | rangee 0 : Robe sacrée (25 %), Crosse sacrée (17 %), Chaîne sacrée (15 %), Robe sacrée (13 %), Crosse sacrée (11 %), Crosse sacrée (9 %), Robe sacrée (6 %), Chaîne sacrée (4 %) | -2153 / 640 / 2300 |

### Zone 5, sous-zone 7 — repertoire de 12924 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 0 | 4 | 0x4000 | `00 08 00 00 00 04 00 40` |
| 1 | 11 | 1 | 5 | 0x4000 | `01 0b 00 00 01 05 00 40` |
| 2 | 9 | 2 | 6 | 0x4000 | `02 09 00 00 02 06 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Agonisant** | 88 | **215** | **36** | 38 | 33 | 0 | 50 | `85 74 71 72 43 60 52 57` |
| 1 | **Revenant** | 100 | **120** | **45** | 33 | 41 | 1 | 40 | `96 60 81 83 90 86 69 57` |
| 2 | **Ombre** | 85 | **93** | **172** | 39 | 89 | 0 | 44 | `74 190 27 98 16 171 76 127` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 3986 | -195 | -3594 | 44 | 0x0000ff00 |
| 0 | 1 | 0 | 4065 | -195 | -1777 | 44 | 0x0000ff00 |
| 0 | 2 | 0 | 3974 | -195 | -1029 | 44 | 0x0000ff00 |
| 0 | 3 | 0 | 3905 | -195 | 633 | 44 | 0x0000ff00 |
| 0 | 4 | 0 | 4099 | -195 | 1153 | 44 | 0x0000ff00 |
| 0 | 5 | 0 | 3929 | -195 | 2417 | 44 | 0x0000ff00 |
| 0 | 6 | 0 | 4011 | -195 | 3521 | 44 | 0x0000ff00 |
| 0 | 7 | 0 | 4090 | 5 | -9 | 44 | 0x0000ff00 |
| 1 | 0 | 0 | -3708 | 5 | -1089 | 44 | 0x0000ff00 |
| 1 | 1 | 0 | -4308 | 5 | -3009 | 44 | 0x0000ff00 |
| 1 | 2 | 0 | -3716 | -395 | -3125 | 44 | 0x0000ff00 |
| 1 | 3 | 0 | -4404 | 5 | 7 | 44 | 0x0000ff00 |
| 1 | 4 | 0 | -3708 | 5 | 1476 | 44 | 0x0000ff00 |
| 1 | 5 | 0 | -4308 | 5 | 2564 | 44 | 0x0000ff00 |
| 1 | 6 | 0 | -3716 | -395 | 3356 | 44 | 0x0000ff00 |
| 2 | 0 | 2 | -1128 | -4795 | 2925 | 44 | 0x0000ff02 |
| 2 | 1 | 2 | -896 | -4795 | 2781 | 44 | 0x0000ff02 |
| 2 | 2 | 2 | 1126 | -4795 | 3281 | 44 | 0x0000ff02 |
| 2 | 3 | 2 | 1106 | -4795 | 2893 | 44 | 0x0000ff02 |
| 2 | 4 | 1 | -886 | -4795 | 3177 | 44 | 0x0000ff01 |
| 2 | 5 | 1 | 878 | -4795 | 2993 | 44 | 0x0000ff01 |
| 3 | 0 | 0 | -1 | -6771 | -418 | 2013 | 0x0000ff00 |
| 3 | 1 | 0 | -1 | -6771 | -986 | 2013 | 0x0000ff00 |
| 3 | 2 | 0 | -1 | -6771 | -1286 | 2013 | 0x0000ff00 |
| 3 | 3 | 0 | 3 | -6771 | -1594 | 2013 | 0x0000ff00 |
| 4 | 0 | 0 | 685 | -195 | 5087 | 2013 | 0x0000ff00 |
| 4 | 1 | 0 | -1363 | -195 | 5055 | 2013 | 0x0000ff00 |
| 4 | 2 | 0 | 1085 | -195 | 4863 | 2013 | 0x0000ff00 |
| 4 | 3 | 0 | 1509 | -195 | 5095 | 2013 | 0x0000ff00 |
| 4 | 4 | 0 | 2213 | -195 | 4871 | 2013 | 0x0000ff00 |
| 4 | 5 | 0 | -2595 | -195 | 4887 | 2475 | 0x0000ff00 |
| 5 | 0 | 0 | -2810 | 5 | -4914 | 2013 | 0x0000ff00 |
| 5 | 1 | 0 | -2960 | 5 | -5178 | 2013 | 0x0000ff00 |
| 5 | 2 | 0 | -2058 | -595 | -5162 | 2013 | 0x0000ff00 |
| 5 | 3 | 0 | -946 | -1195 | -5234 | 2013 | 0x0000ff00 |
| 5 | 4 | 0 | 84 | -1795 | -4810 | 2013 | 0x0000ff00 |
| 5 | 5 | 0 | -54 | -1795 | -5162 | 2013 | 0x0000ff00 |
| 5 | 6 | 0 | 990 | -2395 | -5255 | -15400 | 0x0000ff00 |
| 6 | 0 | 2 | 2288 | -2995 | -3506 | -15400 | 0x0000ff02 |
| 6 | 1 | 2 | 2293 | -3395 | -1535 | -15400 | 0x0000ff02 |
| 6 | 2 | 2 | 1690 | -3195 | -2498 | -15400 | 0x0000ff02 |
| 6 | 3 | 2 | 2282 | -3595 | -503 | -15400 | 0x0000ff02 |
| 6 | 4 | 2 | 1684 | -3595 | -487 | -15400 | 0x0000ff02 |
| 6 | 5 | 2 | 1692 | -3795 | 473 | -15400 | 0x0000ff02 |
| 7 | 0 | 2 | -1710 | -5395 | 1467 | -15400 | 0x0000ff02 |
| 7 | 1 | 2 | -2296 | -5595 | 497 | -15400 | 0x0000ff02 |
| 7 | 2 | 2 | -2314 | -5795 | -516 | -15400 | 0x0000ff02 |
| 7 | 3 | 2 | -2288 | -5995 | -1503 | -15400 | 0x0000ff02 |
| 8 | 0 | 2 | 2500 | -4195 | 2502 | 44 | 0x0000ff02 |
| 8 | 1 | 2 | 1524 | -4795 | 3504 | 44 | 0x0000ff02 |
| 8 | 2 | 1 | 1966 | -4195 | 2477 | 44 | 0x0000ff01 |
| 8 | 3 | 1 | 1444 | -4795 | 2981 | 44 | 0x0000ff01 |
| 8 | 4 | 1 | 2416 | -4495 | 3413 | 44 | 0x0000ff01 |
| 9 | 0 | 2 | 1419 | -2395 | -4958 | -15400 | 0x0000ff02 |
| 9 | 1 | 2 | 1983 | -2795 | -4446 | -15400 | 0x0000ff02 |
| 9 | 2 | 1 | 1747 | -2395 | -5094 | -15400 | 0x0000ff01 |
| 9 | 3 | 1 | 1855 | -2595 | -4794 | -15400 | 0x0000ff01 |
| 10 | 0 | 2 | -1975 | -5195 | 2439 | -15400 | 0x0000ff02 |
| 10 | 1 | 2 | -1455 | -4795 | 3031 | -15400 | 0x0000ff02 |
| 10 | 2 | 1 | -2199 | -5091 | 2719 | -15400 | 0x0000ff01 |
| 10 | 3 | 1 | -1775 | -4895 | 3103 | -15400 | 0x0000ff01 |
| 11 | 0 | 0 | -1530 | -6795 | -3500 | -15400 | 0x0000ff00 |
| 11 | 1 | 0 | -1978 | -6195 | -2504 | -15400 | 0x0000ff00 |
| 11 | 2 | 1 | -1518 | -6795 | -3024 | -15400 | 0x0000ff01 |
| 11 | 3 | 1 | -2290 | -6495 | -3400 | -15400 | 0x0000ff01 |
| 12 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 12 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 13 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 13 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 13 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 15 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 15 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 15 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 16 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 16 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 16 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Cristal de magie `0` | Cristal de terre `1` | Cristal de feu `2` | Cristal d'air `3` | Cristal d'ombre `4` | Cristal de lumière `5` | Cristal saint `6` | Cristal du mal `7` |
| 1 | Balance des juges `8` | Boîte du silence `9` | Boîte à bijoux `10` | Extrait de sang `11` | Cendre bleue `12` | Cendre verte `13` | Cendre noire `14` | Durandal `15` |
| 2 | Aile de Séraphin `16` | Balle de cristal `17` | Ambrosia `18` | Balance des juges `8` | Balance des juges `8` | Ambrosia `18` | Balle de cristal `17` | Aile de Séraphin `16` |
| 3 | Plume dominicale `19` | Robe des anciens `20` | Bouclier sacré `21` | Armure sacrée `22` | Armure sacrée `22` | Bouclier sacré `21` | Robe des anciens `20` | Plume dominicale `19` |
| 4 | Potion olympique `23` | Ecu du destin `24` | Epée d'argent `25` | Manteau de croisé `26` | Bannière de chevalier `27` | Bannière de chevalier `27` | Epée de la fatalité `28` | Epée de la fatalité `28` |
| 5 | Poudre de zombie `29` | Ecu du destin `24` | Sauvagerie `30` | Ecu du destin `24` | Gants de force `31` | Bottes de sept lieux `32` | Bottes de sept lieux `32` | Gants de force `31` |
| 6 | Balle des ténèbres `33` | Cristal d'ombre `4` | Pendentif vaporeux `34` | Robe de l'ombre `35` | Voile des ombres `36` | Voile des ombres `36` | Voile des ombres `36` | Voile des ombres `36` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.0 | coffre `28` | 206 | objet | rangee 1 : Balance des juges (25 %), Boîte du silence (17 %), Boîte à bijoux (15 %), Extrait de sang (13 %), Cendre bleue (11 %), Cendre verte (9 %), Cendre noire (6 %), Durandal (4 %) | 550 / -6760 / 1300 |
| 2.1 | coffre `28` | 207 | objet | rangee 0 : Cristal de magie (25 %), Cristal de terre (17 %), Cristal de feu (15 %), Cristal d'air (13 %), Cristal d'ombre (11 %), Cristal de lumière (9 %), Cristal saint (6 %), Cristal du mal (4 %) | -1150 / -1160 / -5300 |
| 3.17 | coffre `28` | 192 | objet | rangee 2 : Aile de Séraphin (25 %), Balle de cristal (17 %), Ambrosia (15 %), Balance des juges (13 %), Balance des juges (11 %), Ambrosia (9 %), Balle de cristal (6 %), Aile de Séraphin (4 %) | -50 / -7110 / -3000 |
| 3.18 | coffre `28` | 272 | objet | rangee 3 : Plume dominicale (25 %), Robe des anciens (17 %), Bouclier sacré (15 %), Armure sacrée (13 %), Armure sacrée (11 %), Bouclier sacré (9 %), Robe des anciens (6 %), Plume dominicale (4 %) | 2280 / -60 / -5300 |

### Zone 5, sous-zone 8 — repertoire de 17528 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 0 | 4 | 0x4000 | `00 08 00 00 00 04 00 40` |
| 1 | 10 | 2 | 5 | 0x4000 | `01 0a 00 00 02 05 00 40` |
| 2 | 9 | 1 | 6 | 0x4000 | `02 09 00 00 01 06 00 40` |
| 3 | 11 | 3 | 7 | 0x4000 | `03 0b 00 00 03 07 00 40` |
| 4 | 5 | 4 | 8 | 0x4000 | `04 05 00 00 04 08 00 40` |
| 5 | 10 | 5 | 5 | 0x4000 | `01 0a 04 00 05 05 00 40` |
| 6 | 10 | 6 | 5 | 0x4000 | `01 0a 05 00 06 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Agonisant** | 88 | **215** | **36** | 38 | 33 | 0 | 50 | `85 74 71 72 43 60 52 57` |
| 1 | **Ombre** | 85 | **93** | **172** | 39 | 89 | 0 | 44 | `74 190 27 98 16 171 76 127` |
| 2 | **Momie** | 78 | **142** | **33** | 29 | 82 | 0 | 50 | `83 70 58 54 31 77 40 35` |
| 3 | **Revenant** | 100 | **120** | **45** | 33 | 41 | 1 | 40 | `96 60 81 83 90 86 69 57` |
| 4 | **Belle jaune** | 91 | **125** | **130** | 7 | 80 | 0 | 40 | `36 260 61 122 31 120 60 108` |
| 5 | **Vielle momie** | 96 | **140** | **200** | 29 | 70 | 0 | 10 | `128 91 60 64 95 86 75 77` |
| 6 | **Reine momie** | 126 | **240** | **200** | 29 | 77 | 2 | 80 | `140 121 102 90 105 106 105 107` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1060 | 5 | -713 | 0 | 0x0000ff00 |
| 0 | 1 | 0 | -756 | 5 | -615 | 0 | 0x0000ff00 |
| 0 | 2 | 3 | -1291 | -395 | -1005 | 0 | 0x0000ff03 |
| 0 | 3 | 3 | -519 | -395 | -357 | 0 | 0x0000ff03 |
| 0 | 4 | 2 | -902 | -395 | 727 | 0 | 0x0000ff02 |
| 0 | 5 | 2 | -685 | 221 | 139 | 0 | 0x0000ff02 |
| 0 | 6 | 2 | -994 | -395 | 2097 | 0 | 0x0000ff02 |
| 0 | 7 | 2 | -995 | -395 | 3388 | 0 | 0x0000ff02 |
| 0 | 8 | 2 | -767 | -395 | 3728 | 0 | 0x0000ff02 |
| 1 | 0 | 4 | -4092 | -195 | -316 | 0 | 0x0000ff04 |
| 1 | 1 | 4 | -3916 | -195 | -758 | 0 | 0x0000ff04 |
| 1 | 2 | 4 | -3900 | -195 | -1282 | 0 | 0x0000ff04 |
| 1 | 3 | 4 | -3884 | -195 | -1858 | 0 | 0x0000ff04 |
| 2 | 0 | 0 | 1073 | -395 | -1034 | 0 | 0x0000ff00 |
| 2 | 1 | 0 | 573 | -395 | -1406 | 0 | 0x0000ff00 |
| 3 | 0 | 1 | -1187 | 205 | -2880 | 0 | 0x002e0b01 |
| 3 | 1 | 1 | -1481 | 5 | -2460 | 0 | 0x002e0b01 |
| 3 | 2 | 0 | -2688 | 26 | -2448 | 0 | 0x002e0b00 |
| 4 | 0 | 1 | -4324 | -195 | 6247 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | -3768 | -195 | 6355 | 0 | 0x0000ff01 |
| 4 | 2 | 1 | -3000 | -195 | 6295 | 0 | 0x0000ff01 |
| 4 | 3 | 1 | -3168 | -195 | 6403 | 0 | 0x0000ff01 |
| 4 | 4 | 0 | -4348 | -395 | 6795 | 0 | 0x0000ff00 |
| 5 | 0 | 1 | -146 | 5 | 5278 | 0 | 0x00280b01 |
| 5 | 1 | 1 | -304 | -195 | 4600 | 0 | 0x00280b01 |
| 5 | 2 | 0 | 118 | -395 | 5748 | 0 | 0x00280b00 |
| 5 | 3 | 0 | 110 | -395 | 5370 | 0 | 0x00280b00 |
| 5 | 4 | 4 | -262 | -195 | 4445 | 0 | 0x00280b04 |
| 5 | 5 | 4 | -294 | -195 | 5741 | 0 | 0x00280b04 |
| 6 | 0 | 1 | 698 | -195 | 2400 | 0 | 0x002a0b01 |
| 6 | 1 | 1 | 1764 | -195 | 1400 | 0 | 0x002a0b01 |
| 6 | 2 | 1 | 495 | -195 | 1500 | 0 | 0x002a0b01 |
| 6 | 3 | 0 | 1743 | -395 | 1863 | 0 | 0x002a0b00 |
| 6 | 4 | 0 | 659 | -395 | 1875 | 0 | 0x002a0b00 |
| 6 | 5 | 0 | 883 | -395 | 1897 | 0 | 0x002a0b00 |
| 7 | 0 | 1 | -3200 | 205 | -407 | 0 | 0x00300b01 |
| 7 | 1 | 1 | -2215 | 205 | -1460 | 0 | 0x00300b01 |
| 7 | 2 | 1 | -2700 | 5 | -238 | 0 | 0x00300b01 |
| 7 | 3 | 0 | -2684 | 5 | -1454 | 0 | 0x00300b00 |
| 7 | 4 | 0 | -2694 | 5 | -1047 | 0 | 0x00300b00 |
| 8 | 0 | 2 | -2794 | 0 | 4775 | 0 | 0x0000ff02 |
| 8 | 1 | 2 | -2242 | 0 | 4671 | 0 | 0x0000ff02 |
| 9 | 0 | 1 | -481 | 205 | -2015 | 0 | 0x002c0b01 |
| 9 | 1 | 1 | 612 | 205 | -2000 | 0 | 0x002c0b01 |
| 9 | 2 | 1 | 0 | 5 | -2600 | 0 | 0x002c0b01 |
| 9 | 3 | 0 | -593 | 5 | -2495 | 0 | 0x002c0b00 |
| 9 | 4 | 0 | -132 | 5 | -2527 | 0 | 0x002c0b00 |
| 10 | 0 | 5 | 580 | -200 | 6100 | 3072 | 0x0000ff05 |
| 11 | 0 | 5 | -380 | -200 | 4500 | 1024 | 0x0000ff05 |
| 12 | 0 | 5 | 500 | -200 | 2380 | 2048 | 0x0000ff05 |
| 13 | 0 | 5 | 900 | -200 | 2380 | 2048 | 0x0000ff05 |
| 14 | 0 | 5 | 2100 | -200 | 2380 | 2048 | 0x0000ff05 |
| 15 | 0 | 5 | 1100 | -200 | 1420 | 0 | 0x0000ff05 |
| 16 | 0 | 5 | 1500 | -200 | 1420 | 0 | 0x0000ff05 |
| 17 | 0 | 5 | 500 | 200 | -2020 | 2048 | 0x0000ff05 |
| 18 | 0 | 5 | -300 | 200 | -2020 | 2048 | 0x0000ff05 |
| 19 | 0 | 5 | -700 | 200 | -2020 | 2048 | 0x0000ff05 |
| 20 | 0 | 5 | -1100 | 200 | -2980 | 0 | 0x0000ff05 |
| 21 | 0 | 5 | -1500 | 200 | -2980 | 0 | 0x0000ff05 |
| 22 | 0 | 5 | -2300 | 200 | -2980 | 0 | 0x0000ff05 |
| 23 | 0 | 5 | -2700 | 200 | -2980 | 0 | 0x0000ff05 |
| 24 | 0 | 5 | -2220 | 200 | -500 | 3072 | 0x0000ff05 |
| 25 | 0 | 5 | -3180 | 200 | -1100 | 1024 | 0x0000ff05 |
| 26 | 0 | 5 | -3180 | 200 | -300 | 1024 | 0x0000ff05 |
| 27 | 0 | 5 | -3180 | 200 | 100 | 1024 | 0x0000ff05 |
| 28 | 0 | 6 | 2580 | -400 | 1900 | 3072 | 0x0000ff06 |
| 29 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 29 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 30 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 30 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 31 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 31 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 31 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 32 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 32 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 32 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 32 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 33 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 33 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 34 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 34 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 34 | 2 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Panoramix `0` | Panoramix `0` | Panoramix `0` | Panoramix `0` | Panoramix `0` | Panoramix `0` | Panoramix `0` | Panoramix `0` |
| 1 | Balle de cristal `1` | Balle sacrée `2` | Balle de cristal `1` | Balle sacrée `2` | Balle sacrée `2` | Balle de cristal `1` | Balle sacrée `2` | Balle de cristal `1` |
| 2 | Couronne du soleil `3` | Sceptre de bataille `4` | Sceptre de bataille `4` | Couronne du soleil `3` | Couronne du soleil `3` | Sceptre de bataille `4` | Sceptre de bataille `4` | Couronne du soleil `3` |
| 3 | Poignard des brouillards `5` | Poignard des brouillards `5` | Poignard des brouillards `5` | Poignard des brouillards `5` | Panoramix `0` | Panoramix `0` | Panoramix `0` | Panoramix `0` |
| 4 | Potion olympique `6` | Ecu du destin `7` | Epée d'argent `8` | Manteau de croisé `9` | Bannière de chevalier `10` | Bannière de chevalier `10` | Epée de la fatalité `11` | Epée de la fatalité `11` |
| 5 | Huile du pouvoir `12` | Bague au joyau `13` | Bague sacrée `14` | Cendre bleue `15` | Cendre bleue `15` | Ambrosia `16` | Ambrosia `16` | Balance des juges `17` |
| 6 | Balle des ténèbres `18` | Cristal d'ombre `19` | Pendentif vaporeux `20` | Robe de l'ombre `21` | Voile des ombres `22` | Voile des ombres `22` | Voile des ombres `22` | Voile des ombres `22` |
| 7 | Poudre de zombie `23` | Ecu du destin `7` | Sauvagerie `24` | Ecu du destin `7` | Gants de force `25` | Bottes de sept lieux `26` | Bottes de sept lieux `26` | Gants de force `25` |
| 8 | Elixir des esprits `27` | Elixir des esprits `27` | Cristal de lumière `28` | Cristal de lumière `28` | Cristal de lumière `28` | Cristal de lumière `28` | Cristal de lumière `28` | Cristal de lumière `28` |
| 9 | Extrait d'esprits `29` | Bague au joyau `13` | Bague sacrée `14` | Cendre bleue `15` | Cendre bleue `15` | Ambrosia `16` | Ambrosia `16` | Balance des juges `17` |
| 10 | Extrait de sang `30` | Bague au joyau `13` | Bague sacrée `14` | Cendre bleue `15` | Cendre bleue `15` | Ambrosia `16` | Ambrosia `16` | Balance des juges `17` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.1 | coffre `28` | 210 | message | — | -900 / 40 / -700 |
| 2.8 | coffre `28` | 208 | objet | rangee 0 : Panoramix (25 %), Panoramix (17 %), Panoramix (15 %), Panoramix (13 %), Panoramix (11 %), Panoramix (9 %), Panoramix (6 %), Panoramix (4 %) | -3700 / 40 / 2500 |
| 2.10 | coffre `28` | 209 | objet | rangee 1 : Balle de cristal (25 %), Balle sacrée (17 %), Balle de cristal (15 %), Balle sacrée (13 %), Balle sacrée (11 %), Balle de cristal (9 %), Balle sacrée (6 %), Balle de cristal (4 %) | 500 / 240 / 5300 |
| 3.50 | coffre `28` | 266 | objet | rangee 3 : Poignard des brouillards (25 %), Poignard des brouillards (17 %), Poignard des brouillards (15 %), Poignard des brouillards (13 %), Panoramix (11 %), Panoramix (9 %), Panoramix (6 %), Panoramix (4 %) | 2900 / -360 / 1700 |
| 3.52 | coffre `28` | 267 | objet | rangee 2 : Couronne du soleil (25 %), Sceptre de bataille (17 %), Sceptre de bataille (15 %), Couronne du soleil (13 %), Couronne du soleil (11 %), Sceptre de bataille (9 %), Sceptre de bataille (6 %), Couronne du soleil (4 %) | 2900 / -360 / 2100 |

### Zone 5, sous-zone 9 — repertoire de 17256 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 7 | 0 | 0 | 0x4000 | `00 07 00 00 00 00 00 40` |
| 1 | 12 | 1 | 1 | 0x4000 | `01 0c 00 00 01 01 00 40` |
| 2 | 11 | 2 | 2 | 0x4000 | `02 0b 00 00 02 02 00 40` |
| 3 | 9 | 3 | 3 | 0x4000 | `03 09 00 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Durahan-noir** | 110 | **358** | **190** | 74 | 80 | 1 | 200 | `113 72 94 83 26 85 93 72` |
| 1 | **Roi-zombie** | 150 | **1350** | **900** | 75 | 130 | 0 | 220 | `100 200 120 108 120 200 121 108` |
| 2 | **Revenant** | 100 | **190** | **45** | 33 | 41 | 1 | 40 | `96 60 81 83 90 86 69 57` |
| 3 | **Ombre rouge** | 102 | **140** | **172** | 39 | 89 | 0 | 44 | `74 190 27 128 40 171 76 127` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -100 | 200 | 2885 | 2048 | 0x00060b01 |
| 0 | 1 | 0 | -300 | 150 | 2885 | 2048 | 0x0000ff00 |
| 0 | 2 | 0 | 100 | 150 | 2885 | 2048 | 0x0000ff00 |
| 0 | 3 | 2 | -300 | 100 | 2555 | 2048 | 0x0000ff02 |
| 0 | 4 | 2 | 100 | 100 | 2555 | 2048 | 0x0000ff02 |
| 0 | 5 | 3 | -300 | 600 | 2585 | 2048 | 0x0000ff03 |
| 0 | 6 | 3 | 100 | 600 | 2585 | 2048 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Epée de la fatalité `0` | Epée de la fatalité `0` | Balle maléfique `1` | Balle maléfique `1` | Balle maléfique `1` | Armure de Satan `2` | Armure de Satan `2` | Armure de Satan `2` |
| 1 | Cristal d'énergie `3` | Bouclier lunaire `4` | Bouclier lunaire `4` | Crosse du diable `5` | Baguette spatiale `6` | Baguette spatiale `6` | Crosse du diable `5` | Crosse de Séraphin `7` |
| 2 | Poudre de zombie `8` | Ecu du destin `9` | Sauvagerie `10` | Ecu du destin `9` | Gants de force `11` | Bottes de sept lieux `12` | Bottes de sept lieux `12` | Gants de force `11` |
| 3 | Balle maléfique `1` | Cristal du mal `13` | Chaîne du mal `14` | Robe de l'ombre `15` | Voile des ombres `16` | Voile des ombres `16` | Voile des ombres `16` | Voile des ombres `16` |

## Zone 6 — 5368 octets, 12 sous-zone(s)

### Zone 6, sous-zone 0 — repertoire de 31948 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 0 | 9 | 0x4000 | `00 02 00 00 00 09 00 40` |
| 1 | 3 | 1 | 10 | 0x4000 | `01 03 00 00 01 0a 00 40` |
| 2 | 4 | 2 | 11 | 0x4000 | `02 04 00 00 02 0b 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Zombie** | 35 | **90** | **12** | 27 | 35 | 1 | 65 | `40 35 33 33 20 36 24 24` |
| 1 | **Harpie** | 56 | **103** | **76** | 51 | 61 | 3 | 40 | `45 30 38 38 50 40 40 40` |
| 2 | **Loup** | 38 | **60** | **31** | 35 | 38 | 5 | 70 | `38 41 29 27 28 32 29 21` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2098 | 208 | -1407 | 0 | 0x002b0b00 |
| 0 | 1 | 0 | 2606 | 208 | -1095 | 308 | 0x002b0b00 |
| 1 | 0 | 1 | 2618 | 208 | -1167 | 308 | 0x002b0b01 |
| 1 | 1 | 1 | 2402 | 208 | -1591 | 0 | 0x002b0b01 |
| 2 | 0 | 2 | 2202 | 208 | -839 | 0 | 0x002b0b02 |
| 2 | 1 | 2 | 2670 | 208 | -851 | 0 | 0x002b0b02 |
| 3 | 0 | 1 | 2414 | 208 | -1323 | 0 | 0x002b0b01 |
| 3 | 1 | 2 | 2400 | 208 | -600 | 0 | 0x002b0b02 |
| 3 | 2 | 2 | 2400 | 212 | -900 | 0 | 0x002b0b02 |
| 4 | 0 | 0 | 1735 | 216 | 1259 | 1991 | 0x002d0b00 |
| 4 | 1 | 0 | 1927 | 208 | 1067 | 1991 | 0x002d0b00 |
| 5 | 0 | 0 | 2703 | 208 | 1267 | 1991 | 0x002e0b00 |
| 5 | 1 | 0 | 2503 | 208 | 1059 | 1991 | 0x002e0b00 |
| 6 | 0 | 0 | 1087 | 208 | 1275 | 1991 | 0x002f0b00 |
| 6 | 1 | 0 | 975 | 208 | 1043 | 1991 | 0x002f0b00 |
| 7 | 0 | 0 | -2766 | 9 | -1530 | 0 | 0x00300b00 |
| 7 | 1 | 0 | -2706 | 8 | -2884 | 0 | 0x00300b00 |
| 7 | 2 | 0 | -2322 | 12 | -1492 | 0 | 0x00300b00 |
| 8 | 0 | 1 | -3066 | 72 | -2884 | 0 | 0x00300b01 |
| 8 | 1 | 1 | -2230 | 72 | -2892 | 0 | 0x00300b01 |
| 8 | 2 | 1 | -3238 | 8 | -1490 | 0 | 0x00300b01 |
| 8 | 3 | 1 | -2126 | 8 | -1498 | 0 | 0x00300b01 |
| 9 | 0 | 2 | -1502 | 16 | -1922 | 0 | 0x00300b02 |
| 9 | 1 | 2 | -1494 | 8 | -1570 | 0 | 0x00300b02 |
| 9 | 2 | 2 | -1718 | 9 | -3274 | 0 | 0x00300b02 |
| 9 | 3 | 2 | -3486 | 8 | -3234 | 0 | 0x00300b02 |
| 10 | 0 | 0 | -2518 | 8 | -3290 | 0 | 0x00300b00 |
| 10 | 1 | 0 | -2774 | 8 | -1466 | 0 | 0x00300b00 |
| 10 | 2 | 1 | -2806 | 8 | -3258 | 0 | 0x00300b01 |
| 10 | 3 | 1 | -2526 | 8 | -1506 | 0 | 0x00300b01 |
| 11 | 0 | 0 | 1551 | 208 | 5072 | 1980 | 0x00310b00 |
| 11 | 1 | 0 | 1591 | 208 | 3396 | 0 | 0x00310b00 |
| 11 | 2 | 0 | 3115 | 208 | 3384 | 0 | 0x00310b00 |
| 11 | 3 | 0 | 3079 | 208 | 5080 | 1980 | 0x00310b00 |
| 12 | 0 | 1 | 1839 | 208 | 4932 | 0 | 0x00310b01 |
| 12 | 1 | 1 | 2751 | 204 | 4908 | 0 | 0x00310b01 |
| 12 | 2 | 1 | 1843 | 208 | 3488 | 0 | 0x00310b01 |
| 12 | 3 | 1 | 2751 | 208 | 3516 | 0 | 0x00310b01 |
| 13 | 0 | 2 | 1887 | 208 | 4580 | 0 | 0x00310b02 |
| 13 | 1 | 2 | 2807 | 208 | 4568 | 0 | 0x00310b02 |
| 14 | 0 | 1 | 2299 | 204 | 5000 | 0 | 0x00310b01 |
| 14 | 1 | 1 | 2307 | 208 | 3528 | 0 | 0x00310b01 |
| 14 | 2 | 2 | 1295 | 208 | 4308 | 0 | 0x00310b02 |
| 14 | 3 | 2 | 3327 | 208 | 4268 | 0 | 0x00310b02 |
| 15 | 0 | 1 | -3292 | 208 | 1302 | 2970 | 0x00320b01 |
| 15 | 1 | 1 | -2700 | 208 | 1910 | 1980 | 0x00320b01 |
| 15 | 2 | 2 | -2088 | 208 | 2074 | 1980 | 0x00320b02 |
| 15 | 3 | 2 | -3892 | 208 | 902 | 2970 | 0x00320b02 |
| 16 | 0 | 0 | -3884 | 208 | 894 | 2970 | 0x00320b00 |
| 16 | 1 | 0 | -3884 | 208 | 1318 | 2970 | 0x00320b00 |
| 16 | 2 | 0 | -3884 | 208 | 1698 | 2970 | 0x00320b00 |
| 16 | 3 | 0 | -3884 | 208 | 2086 | 2970 | 0x00320b00 |
| 17 | 0 | 0 | 295 | 208 | -193 | 1980 | 0x00340b00 |
| 17 | 1 | 0 | 427 | 208 | -513 | 990 | 0x00340b00 |
| 18 | 0 | 0 | 403 | 208 | -1233 | 0 | 0x00350b00 |
| 18 | 1 | 0 | 407 | 204 | -929 | 990 | 0x00350b00 |
| 19 | 0 | 2 | -765 | 208 | -993 | 1980 | 0x00360b02 |
| 19 | 1 | 2 | -745 | 208 | -1301 | 2970 | 0x00360b02 |
| 20 | 0 | 1 | -845 | 208 | -113 | 2970 | 0x00370b01 |
| 20 | 1 | 1 | -761 | 208 | -417 | 0 | 0x00370b01 |
| 21 | 0 | 0 | 1475 | 8 | -1885 | 990 | 0x002c0b00 |
| 21 | 1 | 0 | 1471 | 8 | -1513 | 990 | 0x002c0b00 |
| 22 | 0 | 0 | 1315 | 8 | -1917 | 770 | 0x002c0b00 |
| 22 | 1 | 0 | 1283 | 8 | -1489 | 990 | 0x002c0b00 |
| 22 | 2 | 1 | 1699 | 8 | -1637 | 990 | 0x002c0b01 |
| 22 | 3 | 1 | 1699 | 8 | -1769 | 990 | 0x002c0b01 |
| 23 | 0 | 2 | 1171 | 8 | -1705 | 990 | 0x002c0b02 |
| 23 | 1 | 2 | 1647 | 8 | -1709 | 990 | 0x002c0b02 |
| 24 | 0 | 0 | 1651 | 8 | -1697 | 990 | 0x002c0b00 |
| 24 | 1 | 2 | 1259 | 8 | -1529 | 990 | 0x002c0b02 |
| 24 | 2 | 2 | 1271 | 8 | -1817 | 990 | 0x002c0b02 |
| 25 | 0 | 2 | 1159 | 224 | -516 | 1980 | 0x00330b02 |
| 25 | 1 | 2 | 1491 | 224 | -504 | 1980 | 0x00330b02 |
| 26 | 0 | 0 | 1659 | 224 | -524 | 1980 | 0x00330b00 |
| 26 | 1 | 0 | 1383 | 224 | -516 | 1980 | 0x00330b00 |
| 26 | 2 | 0 | 1103 | 228 | -508 | 1980 | 0x00330b00 |
| 27 | 0 | 1 | 1151 | 224 | -512 | 1980 | 0x00330b01 |
| 27 | 1 | 1 | 1423 | 224 | -512 | 1980 | 0x00330b01 |
| 27 | 2 | 1 | 1691 | 224 | -500 | 1980 | 0x00330b01 |
| 28 | 0 | 1 | 1363 | 224 | -508 | 1980 | 0x00330b01 |
| 28 | 1 | 2 | 1123 | 224 | -1128 | 2970 | 0x00330b02 |
| 28 | 2 | 2 | 1695 | 224 | -1116 | 990 | 0x00330b02 |
| 29 | 0 | 0 | 2431 | 8 | -2994 | 2244 | 0x003b0b00 |
| 29 | 1 | 0 | 2647 | 8 | -2926 | 2244 | 0x003b0b00 |
| 29 | 2 | 1 | 2879 | 8 | -2486 | 1782 | 0x003b0b01 |
| 29 | 3 | 1 | 2703 | 8 | -2566 | 1980 | 0x003b0b01 |
| 30 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 30 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 31 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 31 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 32 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 32 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Balle de cristal `0` | Balle de cristal `0` | Balle sacrée `1` | Balle maléfique `2` | Balle sacrée `1` | Balle maléfique `2` | Balle sacrée `1` | Balle maléfique `2` |
| 1 | Epée de feu `3` | Epée de glace `4` | Epée du tonnerre `5` | Sabre de la terre `6` | Sabre de la terre `6` | Epée du tonnerre `5` | Epée de glace `4` | Epée de feu `3` |
| 2 | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` |
| 3 | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` |
| 4 | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` |
| 5 | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` |
| 6 | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` | Réflector `8` |
| 7 | Hache du feu `9` | Hache du feu `9` | Hache du feu `9` | Hache du feu `9` | Hache du feu `9` | Hache du feu `9` | Hache du feu `9` | Hache du feu `9` |
| 8 | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` | Robe médicinale `7` |
| 9 | Châle précieux `10` | Potion med `11` | Potion med `11` | Talisman `12` | Ecu du destin `13` | Ecu du destin `13` | Cendre grise `14` | Cendre grise `14` |
| 10 | Elixir des esprits `15` | Ecu du destin `13` | Flûte des moutons `16` | Gants ailés `17` | Manteau à plumes `18` | Chaussures à plumes `19` | Balle du tonnerre `20` | Balle du tonnerre `20` |
| 11 | Potion med `11` | Potion med `11` | Elixir de vie `21` | Elixir de vie `21` | Extrait de sang `22` | Extrait de sang `22` | Extrait de sang `22` | Extrait de sang `22` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.0 | coffre `25` | 253 | message | — | -3300 / 240 / 2700 |
| 1.5 | coffre `25` | 240 | message | — | 1700 / 200 / 900 |
| 1.6 | coffre `25` | 255 | message | — | 2900 / 40 / -2350 |
| 1.9 | coffre `25` | 205 | objet | rangee 0 : Balle de cristal (25 %), Balle de cristal (17 %), Balle sacrée (15 %), Balle maléfique (13 %), Balle sacrée (11 %), Balle maléfique (9 %), Balle sacrée (6 %), Balle maléfique (4 %) | -2900 / 240 / 3700 |
| 1.10 | coffre `25` | 206 | objet | rangee 1 : Epée de feu (25 %), Epée de glace (17 %), Epée du tonnerre (15 %), Sabre de la terre (13 %), Sabre de la terre (11 %), Epée du tonnerre (9 %), Epée de glace (6 %), Epée de feu (4 %) | -3500 / 240 / 4500 |
| 3.1 | coffre `25` | 254 | message | — | -900 / 240 / 3500 |

### Zone 6, sous-zone 1 — repertoire de 25748 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 0 | 1 | 0x4000 | `00 02 00 00 00 01 00 40` |
| 1 | 1 | 1 | 2 | 0x4000 | `01 01 00 00 01 02 00 40` |
| 2 | 0 | 2 | 3 | 0x4000 | `02 00 00 00 02 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Zombie** | 35 | **90** | **12** | 27 | 35 | 1 | 65 | `40 35 33 33 20 36 24 24` |
| 1 | **Fantôme** | 58 | **84** | **41** | 31 | 58 | 3 | 50 | `63 90 74 75 61 64 69 60` |
| 2 | **Sanguinaire** | 62 | **141** | **71** | 32 | 40 | 52 | 70 | `58 44 32 32 39 52 42 42` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | -4497 | 1408 | -1027 | 0 | 0x002b0b02 |
| 0 | 1 | 2 | -4721 | 1408 | -1035 | 0 | 0x002b0b02 |
| 1 | 0 | 0 | -1495 | 1408 | 4066 | 2552 | 0x002c0b00 |
| 1 | 1 | 0 | -1119 | 1408 | 3634 | 0 | 0x002c0b00 |
| 2 | 0 | 2 | -1091 | 1408 | 3594 | 0 | 0x002c0b02 |
| 2 | 1 | 2 | -1491 | 1408 | 3590 | 0 | 0x002c0b02 |
| 2 | 2 | 2 | -1291 | 1408 | 4250 | 1980 | 0x002c0b02 |
| 3 | 0 | 1 | -1439 | 1408 | 3894 | 2970 | 0x002c0b01 |
| 3 | 1 | 1 | -1371 | 1408 | 4310 | 2970 | 0x002c0b01 |
| 3 | 2 | 1 | -1371 | 1408 | 3522 | 2970 | 0x002c0b01 |
| 4 | 0 | 1 | -1527 | 1408 | 3486 | 3487 | 0x002c0b01 |
| 4 | 1 | 1 | -1539 | 1408 | 4326 | 2783 | 0x002c0b01 |
| 4 | 2 | 2 | -1075 | 1408 | 4322 | 1474 | 0x002c0b02 |
| 4 | 3 | 2 | -1083 | 1408 | 3478 | 462 | 0x002c0b02 |
| 5 | 0 | 0 | -4576 | 1608 | 3170 | 2321 | 0x002d0b00 |
| 5 | 1 | 0 | -3928 | 1608 | 3094 | 1551 | 0x002d0b00 |
| 6 | 0 | 1 | -4304 | 1764 | 3442 | 1980 | 0x002d0b01 |
| 6 | 1 | 1 | -3760 | 1764 | 3446 | 1980 | 0x002d0b01 |
| 6 | 2 | 2 | -3876 | 1608 | 3090 | 1551 | 0x002d0b02 |
| 6 | 3 | 2 | -4732 | 1608 | 3362 | 2244 | 0x002d0b02 |
| 7 | 0 | 1 | -2335 | 1608 | 5121 | 1980 | 0x002e0b01 |
| 7 | 1 | 1 | -3479 | 1608 | 5105 | 1980 | 0x002e0b01 |
| 7 | 2 | 1 | -2907 | 1408 | 4500 | 1980 | 0x002e0b01 |
| 8 | 0 | 0 | -2311 | 1608 | 5097 | 1980 | 0x002e0b00 |
| 8 | 1 | 0 | -3519 | 1608 | 5085 | 1980 | 0x002e0b00 |
| 8 | 2 | 0 | -3107 | 1408 | 4329 | 0 | 0x002e0b00 |
| 8 | 3 | 0 | -2619 | 1408 | 4321 | 0 | 0x002e0b00 |
| 8 | 4 | 0 | -2907 | 1608 | 5421 | 1980 | 0x002e0b00 |
| 9 | 0 | 1 | -3207 | 1408 | 4500 | 1980 | 0x002e0b01 |
| 9 | 1 | 1 | -2575 | 1408 | 4500 | 1980 | 0x002e0b01 |
| 9 | 2 | 2 | -2651 | 1408 | 4321 | 0 | 0x002e0b02 |
| 9 | 3 | 2 | -3123 | 1408 | 4329 | 0 | 0x002e0b02 |
| 10 | 0 | 1 | 1300 | 2100 | -100 | 2398 | 0x002f0b01 |
| 10 | 1 | 1 | 1100 | 2100 | 100 | 1474 | 0x002f0b01 |
| 10 | 2 | 1 | 900 | 2100 | -100 | 462 | 0x002f0b01 |
| 10 | 3 | 1 | 1100 | 2100 | -300 | 3641 | 0x002f0b01 |
| 11 | 0 | 1 | 1300 | 2100 | -100 | 2398 | 0x002f0b01 |
| 11 | 1 | 1 | 1100 | 2100 | 100 | 1474 | 0x002f0b01 |
| 11 | 2 | 2 | 900 | 2100 | -100 | 462 | 0x002f0b02 |
| 11 | 3 | 2 | 1100 | 2100 | -300 | 3641 | 0x002f0b02 |
| 12 | 0 | 0 | 586 | 2008 | -102 | 2937 | 0x002f0b00 |
| 12 | 1 | 0 | 1638 | 2008 | -114 | 990 | 0x002f0b00 |
| 12 | 2 | 0 | 1094 | 2008 | 310 | 1980 | 0x002f0b00 |
| 12 | 3 | 0 | 1102 | 2008 | -472 | 0 | 0x002f0b00 |
| 13 | 0 | 2 | 637 | 2008 | -136 | 2970 | 0x002f0b02 |
| 13 | 1 | 2 | 1567 | 2008 | -140 | 990 | 0x002f0b02 |
| 13 | 2 | 2 | 471 | 2008 | -60 | 2970 | 0x002f0b02 |
| 13 | 3 | 2 | 1709 | 2008 | -58 | 990 | 0x002f0b02 |
| 14 | 0 | 0 | -2909 | 1408 | 2334 | 0 | 0x00300b00 |
| 14 | 1 | 0 | -3181 | 1408 | 2338 | 0 | 0x00300b00 |
| 14 | 2 | 0 | -3041 | 1408 | 2570 | 0 | 0x00300b00 |
| 14 | 3 | 1 | -3193 | 1808 | 1778 | 0 | 0x00300b01 |
| 14 | 4 | 1 | -2453 | 1808 | 2118 | 990 | 0x00300b01 |
| 15 | 0 | 0 | -1300 | 2208 | -100 | 2970 | 0x00310b00 |
| 15 | 1 | 0 | -1288 | 2208 | -304 | 2970 | 0x00310b00 |
| 15 | 2 | 0 | -1296 | 2208 | 100 | 2970 | 0x00310b00 |
| 16 | 0 | 1 | -1276 | 2208 | 72 | 2970 | 0x00310b01 |
| 16 | 1 | 1 | -1268 | 2208 | -104 | 2970 | 0x00310b01 |
| 16 | 2 | 1 | -1068 | 2208 | -836 | 0 | 0x00310b01 |
| 16 | 3 | 1 | -1256 | 2208 | -912 | 0 | 0x00310b01 |
| 17 | 0 | 2 | -1460 | 2208 | 296 | 2970 | 0x00310b02 |
| 17 | 1 | 2 | -1464 | 2208 | -476 | 0 | 0x00310b02 |
| 17 | 2 | 2 | -900 | 2208 | 108 | 1980 | 0x00310b02 |
| 17 | 3 | 2 | -1568 | 2208 | -712 | 0 | 0x00310b02 |
| 18 | 0 | 0 | -305 | 1812 | 1910 | 0 | 0x00320b00 |
| 18 | 1 | 0 | 111 | 1812 | 2278 | 990 | 0x00320b00 |
| 18 | 2 | 1 | -705 | 1812 | 2238 | 2970 | 0x00320b01 |
| 18 | 3 | 1 | 27 | 1812 | 2002 | 616 | 0x00320b01 |
| 19 | 0 | 0 | 1497 | 2208 | 4497 | 2970 | 0x00330b00 |
| 19 | 1 | 0 | 1500 | 2208 | 4100 | 2970 | 0x00330b00 |
| 19 | 2 | 0 | 2713 | 2208 | 4301 | 0 | 0x00330b00 |
| 19 | 3 | 0 | 1905 | 2208 | 3705 | 0 | 0x00330b00 |
| 20 | 0 | 2 | 1901 | 2208 | 4693 | 0 | 0x00330b02 |
| 20 | 1 | 2 | 2697 | 2208 | 4693 | 0 | 0x00330b02 |
| 20 | 2 | 2 | 897 | 2208 | 4485 | 0 | 0x00330b02 |
| 20 | 3 | 2 | 1117 | 2208 | 3725 | 2970 | 0x00330b02 |
| 21 | 0 | 1 | 2245 | 2609 | 4697 | 2970 | 0x00330b01 |
| 21 | 1 | 1 | 1573 | 2609 | 4305 | 2970 | 0x00330b01 |
| 21 | 2 | 1 | 1585 | 2609 | 3889 | 2970 | 0x00330b01 |
| 21 | 3 | 1 | 2289 | 2609 | 4293 | 2970 | 0x00330b01 |
| 22 | 0 | 0 | 1929 | 2208 | 4305 | 2970 | 0x00330b00 |
| 22 | 1 | 0 | 1921 | 2208 | 3889 | 2970 | 0x00330b00 |
| 22 | 2 | 1 | 1529 | 2609 | 4709 | 2970 | 0x00330b01 |
| 22 | 3 | 1 | 1501 | 2609 | 3693 | 2970 | 0x00330b01 |
| 23 | 0 | 0 | 2469 | 2008 | 2476 | 990 | 0x00340b00 |
| 23 | 1 | 0 | 2469 | 2008 | 1700 | 990 | 0x00340b00 |
| 23 | 2 | 0 | 2101 | 2008 | 2892 | 990 | 0x00340b00 |
| 23 | 3 | 0 | 2117 | 2008 | 1316 | 990 | 0x00340b00 |
| 24 | 0 | 1 | 2477 | 2008 | 2488 | 990 | 0x00340b01 |
| 24 | 1 | 1 | 2497 | 2008 | 1704 | 990 | 0x00340b01 |
| 24 | 2 | 1 | 2493 | 2008 | 2092 | 990 | 0x00340b01 |
| 24 | 3 | 2 | 2089 | 2008 | 1304 | 990 | 0x00340b02 |
| 24 | 4 | 2 | 2065 | 2008 | 2904 | 990 | 0x00340b02 |
| 25 | 0 | 0 | -3485 | 1408 | -2143 | 0 | 0x00350b00 |
| 25 | 1 | 0 | -4337 | 1408 | -1911 | 2970 | 0x00350b00 |
| 25 | 2 | 0 | -4317 | 1408 | -2507 | 2970 | 0x00350b00 |
| 25 | 3 | 0 | -3897 | 1408 | -2703 | 0 | 0x00350b00 |
| 25 | 4 | 0 | -3901 | 1408 | -2319 | 2970 | 0x00350b00 |
| 26 | 0 | 0 | -3877 | 1408 | -2023 | 2970 | 0x00350b00 |
| 26 | 1 | 0 | -3893 | 1408 | -2627 | 2970 | 0x00350b00 |
| 26 | 2 | 1 | -3517 | 1408 | -2307 | 2970 | 0x00350b01 |
| 26 | 3 | 2 | -4317 | 1408 | -2035 | 2970 | 0x00350b02 |
| 26 | 4 | 2 | -4293 | 1408 | -2631 | 2970 | 0x00350b02 |
| 27 | 0 | 0 | -3253 | 1408 | -2315 | 2970 | 0x00350b00 |
| 27 | 1 | 1 | -2369 | 1508 | -1931 | 1012 | 0x00350b01 |
| 27 | 2 | 1 | -2321 | 1508 | -2687 | 1012 | 0x00350b01 |
| 27 | 3 | 1 | -2333 | 1508 | -2311 | 1012 | 0x00350b01 |
| 27 | 4 | 2 | -2073 | 1508 | -2111 | 1012 | 0x00350b02 |
| 27 | 5 | 2 | -2113 | 1508 | -2539 | 1012 | 0x00350b02 |
| 28 | 0 | 0 | -2426 | 2008 | -3179 | 0 | 0x00360b00 |
| 28 | 1 | 0 | -2918 | 2008 | -3183 | 0 | 0x00360b00 |
| 28 | 2 | 0 | -2682 | 2008 | -3191 | 0 | 0x00360b00 |
| 28 | 3 | 2 | -3674 | 2008 | -3215 | 0 | 0x00360b02 |
| 28 | 4 | 2 | -4086 | 2008 | -3195 | 0 | 0x00360b02 |
| 29 | 0 | 0 | -3674 | 2008 | -3179 | 0 | 0x00360b00 |
| 29 | 1 | 1 | -3906 | 2008 | -3167 | 0 | 0x00360b01 |
| 29 | 2 | 2 | -2906 | 2008 | -3183 | 0 | 0x00360b02 |
| 29 | 3 | 2 | -2458 | 2008 | -3199 | 0 | 0x00360b02 |
| 30 | 0 | 0 | -4094 | 2008 | -3175 | 0 | 0x00360b00 |
| 30 | 1 | 0 | -3670 | 2008 | -3175 | 0 | 0x00360b00 |
| 30 | 2 | 0 | -2718 | 2008 | -3207 | 0 | 0x00360b00 |
| 30 | 3 | 1 | -2938 | 2008 | -3195 | 0 | 0x00360b01 |
| 30 | 4 | 1 | -2462 | 2008 | -3203 | 0 | 0x00360b01 |
| 31 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 31 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 32 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 32 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 33 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 33 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Parfum de guérison `0` | Parfum de convalescence `1` | Parfum d'impératrice `2` | Parfum de feu `3` | Balle de l'arc-en-ciel `4` | Balle de l'arc-en-ciel `4` | Balle de l'arc-en-ciel `4` | Balle de l'arc-en-ciel `4` |
| 1 | Châle précieux `5` | Potion med `6` | Potion med `6` | Talisman `7` | Ecu du destin `8` | Ecu du destin `8` | Cendre grise `9` | Cendre grise `9` |
| 2 | Flacon d'énergie `10` | Flacon d'énergie `10` | Flacon d'énergie `10` | Flacon d'énergie `10` | Extrait d'esprits `11` | Extrait d'esprits `11` | Robe de l'ombre `12` | Robe de l'ombre `12` |
| 3 | Craimore `13` | Minegosh `14` | Ecu du destin `8` | Ecu du destin `8` | Cendre rouge `15` | Cendre rouge `15` | Epée de sang `16` | Epée de sang `16` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 4.3 | coffre `25` | 223 | objet | rangee 0 : Parfum de guérison (25 %), Parfum de convalescence (17 %), Parfum d'impératrice (15 %), Parfum de feu (13 %), Balle de l'arc-en-ciel (11 %), Balle de l'arc-en-ciel (9 %), Balle de l'arc-en-ciel (6 %), Balle de l'arc-en-ciel (4 %) | 4300 / 2640 / 4700 |
| 5.0 | coffre `25` | 243 | message | — | -2900 / 2040 / -1100 |
| 6.0 | coffre `25` | 241 | message | — | 700 / 2040 / 1300 |
| 6.1 | coffre `25` | 244 | message | — | -4300 / 1640 / 5100 |

### Zone 6, sous-zone 2 — repertoire de 29268 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 0 | 5 | 0x4000 | `00 02 00 00 00 05 00 40` |
| 1 | 5 | 1 | 3 | 0x4000 | `01 05 00 00 01 03 00 40` |
| 2 | 1 | 2 | 6 | 0x4000 | `02 01 00 00 02 06 00 40` |
| 3 | 0 | 3 | 7 | 0x4000 | `03 00 00 00 03 07 00 40` |
| 4 | 5 | 4 | 4 | 0x4000 | `01 05 03 00 04 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Zombie** | 35 | **90** | **12** | 27 | 35 | 1 | 65 | `40 35 33 33 20 36 24 24` |
| 1 | **Crapouille** | 38 | **71** | **4** | 21 | 62 | 3 | 50 | `44 37 61 29 31 36 31 35` |
| 2 | **Fantôme** | 58 | **84** | **41** | 31 | 58 | 3 | 40 | `63 90 74 75 61 64 69 60` |
| 3 | **Sanguinaire** | 62 | **141** | **71** | 32 | 40 | 52 | 70 | `58 44 32 32 39 52 42 42` |
| 4 | **Gargouille** | 68 | **121** | **100** | 21 | 68 | 1 | 50 | `54 56 58 56 28 56 40 40` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 4 | -100 | 0 | -2000 | 2048 | 0x00070b04 |
| 0 | 1 | 4 | 100 | 0 | -2000 | 2048 | 0x00070b04 |
| 0 | 2 | 4 | 300 | 0 | -3400 | 0 | 0x00070b04 |
| 0 | 3 | 4 | -300 | 0 | -3400 | 0 | 0x00070b04 |
| 1 | 0 | 2 | -1650 | 50 | -1680 | 0 | 0x00080b02 |
| 1 | 1 | 2 | -1890 | 0 | -1900 | 0 | 0x00080b02 |
| 1 | 2 | 0 | -2050 | 0 | -1700 | 0 | 0x00080b00 |
| 1 | 3 | 3 | -1900 | 0 | -2100 | 0 | 0x00080b03 |
| 2 | 0 | 2 | 1660 | 50 | -1578 | 0 | 0x00090b02 |
| 2 | 1 | 2 | 1900 | 0 | -2100 | 0 | 0x00090b02 |
| 2 | 2 | 0 | 1670 | 0 | -2100 | 0 | 0x00090b00 |
| 2 | 3 | 3 | 1623 | 0 | -1950 | 0 | 0x00090b03 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 6 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Broche au joyau `0` | Bague au joyau `1` | Tiare des étoiles `2` | Tiare de la terre `3` | Couronne de la lune `4` | Couronne de la lune `4` | Boîte à bijoux `5` | Boîte à bijoux `5` |
| 1 | Epée d'argent `6` | Féline `7` | Rapière Misuril `8` | Masse d'armes `9` | Masse d'armes `9` | Rapière Misuril `8` | Féline `7` | Epée d'argent `6` |
| 2 | Bague de protection `10` | Chaînette d'argent `11` | Chaînette d'argent `11` | Chaînette d'argent `11` | Médaillon d'or `12` | Médaillon d'or `12` | Médaillon d'or `12` | Bague de protection `10` |
| 3 | Elixir des esprits `13` | Elixir des esprits `13` | Elixir des esprits `13` | Elixir des esprits `13` | Cristal de magie `14` | Cristal de magie `14` | Cristal de magie `14` | Cristal de magie `14` |
| 4 | Elixir de magicien `15` | Elixir de magicien `15` | Elixir de magicien `15` | Elixir de magicien `15` | Cristal de magie `14` | Cristal de magie `14` | Cristal de magie `14` | Cristal de magie `14` |
| 5 | Châle précieux `16` | Potion med `17` | Potion med `17` | Talisman `18` | Ecu du destin `19` | Ecu du destin `19` | Cendre grise `20` | Cendre grise `20` |
| 6 | Flacon d'énergie `21` | Flacon d'énergie `21` | Flacon d'énergie `21` | Flacon d'énergie `21` | Extrait d'esprits `22` | Extrait d'esprits `22` | Robe de l'ombre `23` | Robe de l'ombre `23` |
| 7 | Craimore `24` | Minegosh `25` | Ecu du destin `19` | Ecu du destin `19` | Cendre rouge `26` | Cendre rouge `26` | Epée de sang `27` | Epée de sang `27` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `25` | 236 | message | — | 1756 / 40 / -862 |
| 2.4 | coffre `25` | 237 | message | — | 2100 / 40 / 300 |
| 2.6 | coffre `25` | 238 | message | — | -2100 / 40 / 300 |
| 2.8 | coffre `25` | 222 | objet | rangee 0 : Broche au joyau (25 %), Bague au joyau (17 %), Tiare des étoiles (15 %), Tiare de la terre (13 %), Couronne de la lune (11 %), Couronne de la lune (9 %), Boîte à bijoux (6 %), Boîte à bijoux (4 %) | -1300 / 40 / 2300 |
| 5.0 | coffre `25` | 239 | message | — | 2500 / 440 / 3600 |
| 5.40 | coffre `25` | 301 | objet | rangee 1 : Epée d'argent (25 %), Féline (17 %), Rapière Misuril (15 %), Masse d'armes (13 %), Masse d'armes (11 %), Rapière Misuril (9 %), Féline (6 %), Epée d'argent (4 %) | -2600 / 440 / 3700 |
| 5.41 | coffre `25` | 302 | objet | rangee 2 : Bague de protection (25 %), Chaînette d'argent (17 %), Chaînette d'argent (15 %), Chaînette d'argent (13 %), Médaillon d'or (11 %), Médaillon d'or (9 %), Médaillon d'or (6 %), Bague de protection (4 %) | -2200 / 440 / 3700 |

### Zone 6, sous-zone 3 — repertoire de 6616 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 0 | 1 | 0x4000 | `00 01 00 00 00 01 00 40` |
| 1 | 3 | 1 | 2 | 0x4000 | `01 03 00 00 01 02 00 40` |
| 2 | 0 | 2 | 3 | 0x4000 | `02 00 00 00 02 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Fantôme** | 58 | **84** | **41** | 31 | 58 | 3 | 40 | `63 90 74 75 61 64 69 60` |
| 1 | **Harpie** | 56 | **103** | **76** | 51 | 61 | 3 | 40 | `45 30 38 38 50 40 40 40` |
| 2 | **Sanguinaire** | 62 | **141** | **71** | 32 | 40 | 52 | 70 | `58 44 32 32 39 52 42 42` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 100 | 400 | -1600 | 0 | 0x00050b02 |
| 0 | 1 | 2 | 100 | 400 | -1700 | 0 | 0x00050b02 |
| 0 | 2 | 2 | 670 | 400 | -1600 | 0 | 0x00050b02 |
| 0 | 3 | 2 | 670 | 400 | -1700 | 0 | 0x00050b02 |
| 0 | 4 | 0 | 350 | 400 | -1600 | 0 | 0x00050b00 |
| 0 | 5 | 0 | 350 | 400 | -1700 | 0 | 0x00050b00 |
| 1 | 0 | 2 | 1690 | 400 | 1300 | 0 | 0x00060b02 |
| 1 | 1 | 2 | 1800 | 400 | 1300 | 0 | 0x00060b02 |
| 1 | 2 | 2 | 1690 | 400 | 700 | 0 | 0x00060b02 |
| 1 | 3 | 2 | 1800 | 400 | 700 | 0 | 0x00060b02 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 4 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 4 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Hache empoisonnée `0` | Baguette magique `1` | Arc de chasseur `2` | Baguette de l'eau `3` | Baguette de l'eau `3` | Arc de chasseur `2` | Baguette magique `1` | Hache empoisonnée `0` |
| 1 | Flacon d'énergie `4` | Flacon d'énergie `4` | Flacon d'énergie `4` | Flacon d'énergie `4` | Extrait d'esprits `5` | Extrait d'esprits `5` | Robe de l'ombre `6` | Robe de l'ombre `6` |
| 2 | Elixir des esprits `7` | Ecu du destin `8` | Flûte des moutons `9` | Gants ailés `10` | Manteau à plumes `11` | Chaussures à plumes `12` | Balle du tonnerre `13` | Balle du tonnerre `13` |
| 3 | Craimore `14` | Minegosh `15` | Ecu du destin `8` | Ecu du destin `8` | Cendre rouge `16` | Cendre rouge `16` | Epée de sang `17` | Epée de sang `17` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.2 | coffre `25` | 201 | objet | rangee 0 : Hache empoisonnée (25 %), Baguette magique (17 %), Arc de chasseur (15 %), Baguette de l'eau (13 %), Baguette de l'eau (11 %), Arc de chasseur (9 %), Baguette magique (6 %), Hache empoisonnée (4 %) | -1700 / 240 / 700 |

### Zone 6, sous-zone 4 — repertoire de 5740 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 0 | 1 | 0x4000 | `00 01 00 00 00 01 00 40` |
| 1 | 3 | 1 | 2 | 0x4000 | `01 03 00 00 01 02 00 40` |
| 2 | 0 | 2 | 3 | 0x4000 | `02 00 00 00 02 03 00 40` |
| 3 | 4 | 3 | 4 | 0x4000 | `03 04 00 00 03 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Fantôme** | 58 | **84** | **41** | 31 | 58 | 3 | 40 | `63 90 74 75 61 64 69 60` |
| 1 | **Harpie** | 56 | **103** | **76** | 51 | 61 | 3 | 40 | `55 57 38 38 50 38 38 40` |
| 2 | **Sanguinaire** | 62 | **141** | **71** | 32 | 40 | 2 | 70 | `58 44 32 32 39 52 42 42` |
| 3 | **Loup gris** | 38 | **80** | **31** | 35 | 38 | 5 | 70 | `62 41 59 57 58 52 59 51` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 3 | -300 | 340 | 1800 | 0 | 0x00080b03 |
| 0 | 1 | 3 | 0 | 340 | 1800 | 0 | 0x00080b03 |
| 0 | 2 | 3 | 100 | 340 | 1800 | 0 | 0x00080b03 |
| 1 | 0 | 2 | 200 | 540 | 100 | 0 | 0x00090b02 |
| 1 | 1 | 2 | 100 | 540 | 100 | 0 | 0x00090b02 |
| 1 | 2 | 2 | 0 | 540 | 100 | 0 | 0x00090b02 |
| 1 | 3 | 2 | -200 | 540 | 100 | 0 | 0x00090b02 |
| 2 | 0 | 3 | 2700 | 1140 | -80 | 0 | 0x000a0b03 |
| 2 | 1 | 3 | 2600 | 1140 | 360 | 0 | 0x000a0b03 |
| 2 | 2 | 3 | 2600 | 1140 | -80 | 0 | 0x000a0b03 |
| 3 | 0 | 2 | -700 | 1340 | -50 | 0 | 0x000b0b02 |
| 3 | 1 | 2 | -700 | 1340 | 200 | 0 | 0x000b0b02 |
| 3 | 2 | 2 | -700 | 1340 | 1500 | 0 | 0x000b0b02 |
| 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 5 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 6 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 6 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 7 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 7 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 7 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 8 | 3 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Cuirasse au miroir `0` | Bouclier d'argent `1` | Robe de la terre `2` | Pentacle magique `3` | Pentacle magique `3` | Robe de la terre `2` | Bouclier d'argent `1` | Cuirasse au miroir `0` |
| 1 | Flacon d'énergie `4` | Flacon d'énergie `4` | Flacon d'énergie `4` | Flacon d'énergie `4` | Extrait d'esprits `5` | Extrait d'esprits `5` | Robe de l'ombre `6` | Robe de l'ombre `6` |
| 2 | Elixir des esprits `7` | Ecu du destin `8` | Flûte des moutons `9` | Gants ailés `10` | Manteau à plumes `11` | Chaussures à plumes `12` | Balle du tonnerre `13` | Balle du tonnerre `13` |
| 3 | Craimore `15` | Minegosh `16` | Ecu du destin `8` | Ecu du destin `8` | Cendre rouge `17` | Cendre rouge `17` | Epée de sang `18` | Epée de sang `18` |
| 4 | Potion med `19` | Chaînette d'argent `20` | Poignard d'argent `21` | Poignard d'argent `21` | Extrait de sang `14` | Extrait de sang `14` | Extrait de sang `14` | Extrait de sang `14` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.1 | coffre `25` | 221 | objet | rangee 0 : Cuirasse au miroir (25 %), Bouclier d'argent (17 %), Robe de la terre (15 %), Pentacle magique (13 %), Pentacle magique (11 %), Robe de la terre (9 %), Bouclier d'argent (6 %), Cuirasse au miroir (4 %) | 2900 / 1180 / 100 |

### Zone 6, sous-zone 5 — repertoire de 12728 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 6 | 0 | 2 | 0x4000 | `00 06 00 00 00 02 00 40` |
| 1 | 7 | 1 | 3 | 0x4000 | `01 07 00 00 01 03 00 40` |
| 2 | 3 | 2 | 4 | 0x4000 | `02 03 00 00 02 04 00 40` |
| 3 | 4 | 3 | 5 | 0x4000 | `03 04 00 00 03 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Loup-garou** | 85 | **468** | **190** | 77 | 77 | 2 | 180 | `92 94 91 99 89 87 92 92` |
| 1 | **Tigre** | 85 | **500** | **40** | 78 | 85 | 2 | 180 | `89 90 88 86 80 86 88 94` |
| 2 | **Harpie** | 56 | **103** | **76** | 51 | 61 | 3 | 40 | `45 30 38 38 50 40 40 40` |
| 3 | **Loup gris** | 38 | **80** | **31** | 35 | 38 | 5 | 70 | `62 41 59 57 58 52 59 51` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -4470 | 0 | 1470 | 1536 | 0x00090b00 |
| 0 | 1 | 1 | -4130 | 0 | 1470 | 2560 | 0x000a0b01 |
| 1 | 0 | 3 | 5400 | 1400 | -250 | 1024 | 0x00030b03 |
| 1 | 1 | 3 | 5450 | 1400 | -350 | 1024 | 0x00030b03 |
| 1 | 2 | 3 | 5400 | 1400 | 450 | 1024 | 0x00030b03 |
| 1 | 3 | 3 | 5450 | 1400 | 550 | 1024 | 0x00030b03 |
| 1 | 4 | 2 | 5800 | 1400 | 250 | 1024 | 0x00030b02 |
| 2 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 2 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 2 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Armure d'argent `0` | Armure d'argent `0` | Armure d'argent `0` | Armure d'argent `0` | Armure d'argent `0` | Armure d'argent `0` | Armure d'argent `0` | Armure d'argent `0` |
| 1 | Robe de reine `1` | Robe de reine `1` | Robe de reine `1` | Robe de reine `1` | Robe de reine `1` | Robe de reine `1` | Robe de reine `1` | Robe de reine `1` |
| 2 | Sauvagerie `2` | Sauvagerie `2` | Poignard de sang `3` | Poignard de sang `3` | Bague sacrée `4` | Bague sacrée `4` | La massacreuse `5` | La massacreuse `5` |
| 3 | Epice rouge `6` | Epice rouge `6` | Poignard de sang `3` | Poignard de sang `3` | Os de guerrier sauvage `7` | Os de guerrier sauvage `7` | La massacreuse `5` | La massacreuse `5` |
| 4 | Elixir des esprits `8` | Ecu du destin `9` | Flûte des moutons `10` | Gants ailés `11` | Manteau à plumes `12` | Chaussures à plumes `13` | Balle du tonnerre `14` | Balle du tonnerre `14` |
| 5 | Potion med `15` | Chaînette d'argent `16` | Poignard d'argent `17` | Poignard d'argent `17` | Extrait de sang `18` | Extrait de sang `18` | Extrait de sang `18` | Extrait de sang `18` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.16 | coffre `25` | 299 | objet | rangee 0 : Armure d'argent (25 %), Armure d'argent (17 %), Armure d'argent (15 %), Armure d'argent (13 %), Armure d'argent (11 %), Armure d'argent (9 %), Armure d'argent (6 %), Armure d'argent (4 %) | 5800 / 40 / 300 |
| 0.17 | coffre `25` | 300 | objet | rangee 1 : Robe de reine (25 %), Robe de reine (17 %), Robe de reine (15 %), Robe de reine (13 %), Robe de reine (11 %), Robe de reine (9 %), Robe de reine (6 %), Robe de reine (4 %) | 5800 / 40 / -100 |

### Zone 6, sous-zone 6 — repertoire de 25760 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 0 | 4 | 0x4000 | `00 08 00 00 00 04 00 40` |
| 1 | 5 | 1 | 5 | 0x4000 | `01 05 00 00 01 05 00 40` |
| 2 | 10 | 2 | 6 | 0x4000 | `02 0a 00 00 02 06 00 40` |
| 3 | 9 | 3 | 7 | 0x4000 | `03 09 00 00 03 07 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Petit vampire** | 89 | **136** | **151** | 34 | 80 | 0 | 60 | `76 78 68 66 24 75 60 62` |
| 1 | **Gargouille** | 80 | **121** | **100** | 21 | 68 | 1 | 50 | `74 72 61 69 68 86 70 70` |
| 2 | **Ombre** | 85 | **93** | **172** | 39 | 89 | 0 | 44 | `74 190 27 98 16 171 76 127` |
| 3 | **Vampire** | 76 | **106** | **22** | 4 | 66 | 8 | 40 | `53 61 66 57 51 51 51 51` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2520 | 1240 | 1730 | 0 | 0x00250b00 |
| 0 | 1 | 0 | 2680 | 1240 | 1730 | 0 | 0x00250b00 |
| 0 | 2 | 0 | 2520 | 1240 | 2070 | 2048 | 0x00250b00 |
| 0 | 3 | 0 | 2680 | 1240 | 2070 | 2048 | 0x00250b00 |
| 1 | 0 | 0 | 3900 | 400 | -4300 | 0 | 0x00260b00 |
| 1 | 1 | 0 | 3500 | 400 | -4300 | 0 | 0x00260b00 |
| 1 | 2 | 2 | 3700 | 400 | -4300 | 0 | 0x00260b02 |
| 2 | 0 | 0 | 3500 | 400 | -3600 | 2048 | 0x00260b00 |
| 2 | 1 | 0 | 3900 | 400 | -3600 | 2048 | 0x00260b00 |
| 3 | 0 | 0 | 3900 | 400 | -2500 | 2048 | 0x00270b00 |
| 3 | 1 | 0 | 3500 | 400 | -2500 | 2048 | 0x00270b00 |
| 3 | 2 | 2 | 3700 | 400 | -3100 | 2048 | 0x00270b02 |
| 4 | 0 | 0 | 3500 | 400 | -3100 | 0 | 0x00270b00 |
| 4 | 1 | 0 | 3900 | 400 | -3100 | 0 | 0x00270b00 |
| 5 | 0 | 3 | 0 | 0 | -5100 | 2048 | 0x00280b03 |
| 5 | 1 | 3 | -400 | 0 | -5100 | 2048 | 0x00280b03 |
| 5 | 2 | 3 | -200 | 0 | -4300 | 2048 | 0x00280b03 |
| 5 | 3 | 0 | 500 | 400 | -4400 | 2048 | 0x00280b00 |
| 5 | 4 | 0 | -900 | 400 | -4400 | 2048 | 0x00280b00 |
| 6 | 0 | 2 | 0 | 0 | -5100 | 2048 | 0x00280b02 |
| 6 | 1 | 2 | -400 | 0 | -5100 | 2048 | 0x00280b02 |
| 6 | 2 | 2 | -200 | 0 | -4300 | 2048 | 0x00280b02 |
| 6 | 3 | 1 | 500 | 400 | -4400 | 2048 | 0x00280b01 |
| 6 | 4 | 1 | -900 | 400 | -4400 | 2048 | 0x00280b01 |
| 7 | 0 | 2 | 500 | 0 | -5000 | 2048 | 0x00280b02 |
| 7 | 1 | 2 | -900 | 0 | -5000 | 2048 | 0x00280b02 |
| 7 | 2 | 2 | 500 | 400 | -4400 | 2048 | 0x00280b02 |
| 7 | 3 | 2 | -900 | 400 | -4400 | 2048 | 0x00280b02 |
| 8 | 0 | 0 | -3730 | 448 | -2500 | 0 | 0x00290b00 |
| 8 | 1 | 0 | -3730 | 448 | -2700 | 0 | 0x00290b00 |
| 8 | 2 | 2 | -3900 | 0 | -3750 | 0 | 0x00290b02 |
| 8 | 3 | 2 | -4300 | 0 | -3750 | 0 | 0x00290b02 |
| 8 | 4 | 3 | -4100 | 300 | -3100 | 0 | 0x00290b03 |
| 8 | 5 | 3 | -4100 | 300 | -3500 | 0 | 0x00290b03 |
| 9 | 0 | 1 | -3900 | 0 | -4300 | 0 | 0x00290b01 |
| 9 | 1 | 1 | -4300 | 0 | -4300 | 0 | 0x00290b01 |
| 9 | 2 | 1 | -3900 | 0 | -3700 | 0 | 0x00290b01 |
| 9 | 3 | 1 | -4300 | 0 | -3700 | 0 | 0x00290b01 |
| 10 | 0 | 0 | -3700 | 848 | 2650 | 2048 | 0x002a0b00 |
| 10 | 1 | 0 | -3500 | 848 | 2650 | 2048 | 0x002a0b00 |
| 10 | 2 | 0 | -3300 | 848 | 2650 | 2048 | 0x002a0b00 |
| 10 | 3 | 3 | -3500 | 700 | 1900 | 0 | 0x002a0b03 |
| 10 | 4 | 3 | -3500 | 500 | 1700 | 2048 | 0x002a0b03 |
| 10 | 5 | 3 | -4150 | 400 | 1500 | 2048 | 0x002a0b03 |
| 10 | 6 | 3 | -3950 | 400 | 1500 | 2048 | 0x002a0b03 |
| 11 | 0 | 1 | -500 | 800 | -100 | 2048 | 0x002b0b01 |
| 11 | 1 | 1 | -100 | 800 | -100 | 2048 | 0x002b0b01 |
| 11 | 2 | 2 | 1300 | 1200 | -100 | 3072 | 0x002b0b02 |
| 11 | 3 | 2 | 200 | 1200 | -1200 | 0 | 0x002b0b02 |
| 11 | 4 | 2 | -1900 | 1200 | 600 | 2048 | 0x002b0b02 |
| 12 | 0 | 1 | -500 | 800 | -100 | 2048 | 0x002b0b01 |
| 12 | 1 | 1 | -100 | 800 | -100 | 2048 | 0x002b0b01 |
| 12 | 2 | 1 | -1100 | 800 | -100 | 2048 | 0x002b0b01 |
| 12 | 3 | 1 | 500 | 800 | -100 | 2048 | 0x002b0b01 |
| 13 | 0 | 1 | -500 | 800 | -100 | 2048 | 0x002b0b01 |
| 13 | 1 | 1 | -100 | 800 | -100 | 2048 | 0x002b0b01 |
| 13 | 2 | 0 | 1300 | 1200 | -100 | 3072 | 0x002b0b00 |
| 13 | 3 | 0 | 200 | 1200 | -1200 | 0 | 0x002b0b00 |
| 13 | 4 | 0 | -1900 | 1200 | 600 | 2048 | 0x002b0b00 |
| 14 | 0 | 0 | -3000 | 1200 | 100 | 0 | 0x002c0b00 |
| 14 | 1 | 0 | -2700 | 1200 | 100 | 0 | 0x002c0b00 |
| 14 | 2 | 1 | -3300 | 500 | -500 | 0 | 0x002c0b01 |
| 14 | 3 | 1 | -2500 | 500 | -500 | 0 | 0x002c0b01 |
| 15 | 0 | 0 | 2000 | 400 | -5500 | 0 | 0x002d0b00 |
| 15 | 1 | 0 | 3000 | 400 | -5500 | 0 | 0x002d0b00 |
| 15 | 2 | 1 | 2100 | 500 | -5200 | 0 | 0x002d0b01 |
| 15 | 3 | 1 | 2500 | 500 | -5300 | 0 | 0x002d0b01 |
| 15 | 4 | 1 | 2900 | 500 | -5200 | 0 | 0x002d0b01 |
| 16 | 0 | 1 | -1500 | -400 | -2700 | 0 | 0x002e0b01 |
| 16 | 1 | 1 | -1000 | -400 | -2100 | 0 | 0x002e0b01 |
| 16 | 2 | 1 | -1700 | -400 | -2350 | 0 | 0x002e0b01 |
| 16 | 3 | 1 | -1200 | -400 | -2270 | 0 | 0x002e0b01 |
| 17 | 0 | 1 | -100 | -400 | -2300 | 0 | 0x002f0b01 |
| 17 | 1 | 1 | -100 | -400 | -2600 | 0 | 0x002f0b01 |
| 17 | 2 | 1 | 900 | -400 | -2700 | 0 | 0x002f0b01 |
| 17 | 3 | 1 | 900 | -400 | -2000 | 0 | 0x002f0b01 |
| 18 | 0 | 0 | -650 | 1200 | 2450 | 512 | 0x00300b00 |
| 18 | 1 | 0 | -450 | 1200 | 2350 | 3584 | 0x00300b00 |
| 18 | 2 | 2 | 500 | 1200 | 3300 | 3072 | 0x00300b02 |
| 18 | 3 | 2 | -1400 | 1200 | 1700 | 1024 | 0x00300b02 |
| 18 | 4 | 2 | -1500 | 1200 | 2500 | 1024 | 0x00300b02 |
| 18 | 5 | 2 | -1500 | 1200 | 3300 | 1024 | 0x00300b02 |
| 19 | 0 | 1 | -2100 | 0 | -4400 | 1024 | 0x00310b01 |
| 19 | 1 | 1 | -2100 | 0 | -3800 | 1024 | 0x00310b01 |
| 19 | 2 | 0 | -1850 | 800 | -4100 | 1024 | 0x00310b00 |
| 19 | 3 | 0 | -1900 | 400 | -4100 | 1024 | 0x00310b00 |
| 19 | 4 | 0 | -1500 | 400 | -4100 | 3072 | 0x00310b00 |
| 20 | 0 | 0 | -2500 | 800 | -3500 | 0 | 0x00320b00 |
| 20 | 1 | 0 | -2700 | 800 | -3700 | 3072 | 0x00320b00 |
| 20 | 2 | 3 | -2800 | 600 | -3100 | 1024 | 0x00320b03 |
| 20 | 3 | 3 | -2900 | 600 | -3200 | 1536 | 0x00320b03 |
| 20 | 4 | 3 | -3000 | 600 | -3300 | 1536 | 0x00320b03 |
| 20 | 5 | 3 | -3100 | 600 | -3400 | 2048 | 0x00320b03 |
| 21 | 0 | 0 | 1900 | 1200 | 1100 | 2048 | 0x00330b00 |
| 21 | 1 | 3 | 1700 | 1300 | -100 | 1024 | 0x00330b03 |
| 21 | 2 | 3 | 1700 | 1300 | 500 | 1024 | 0x00330b03 |
| 21 | 3 | 3 | 1700 | 1300 | 900 | 1024 | 0x00330b03 |
| 21 | 4 | 3 | 2100 | 1300 | -100 | 3072 | 0x00330b03 |
| 21 | 5 | 3 | 2100 | 1300 | 500 | 3072 | 0x00330b03 |
| 21 | 6 | 3 | 2100 | 1300 | 900 | 3072 | 0x00330b03 |
| 22 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 22 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 22 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 23 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 23 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 23 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 24 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 24 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 24 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 24 | 3 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 25 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 25 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 25 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 25 | 3 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Parfum de convalescence `0` | Parfum de guérison `1` | Parfum d'impératrice `2` | Parfum de feu `3` | Parfum de feu `3` | Parfum d'impératrice `2` | Parfum de guérison `1` | Parfum de convalescence `0` |
| 1 | Balle de cristal `4` | Balle de cristal `4` | Horloge des mystères `5` | Horloge du bonheur `6` | Horloge du bonheur `6` | Horloge des mystères `5` | Anneau des philosophes `7` | Anneau des philosophes `7` |
| 2 | Cape du printemps `8` | Cape d'été `9` | Cape d'automne `10` | Cape d'hiver `11` | Cape d'hiver `11` | Cape d'automne `10` | Cape d'été `9` | Cape du printemps `8` |
| 3 | Manteau de croisé `12` | Armure du dragon `13` | Bouclier lunaire `14` | Robe des anciens `15` | Robe des anciens `15` | Bouclier lunaire `14` | Armure du dragon `13` | Manteau de croisé `12` |
| 4 | Broche au joyau `16` | Chaîne des ténèbres `17` | Chaîne du mal `18` | Extrait d'esprits `19` | Plume `20` | Pointe de sang `21` | Pointe de sang `21` | Plume `20` |
| 5 | Elixir de magicien `22` | Elixir de magicien `22` | Elixir de magicien `22` | Elixir de magicien `22` | Cristal de magie `23` | Cristal de magie `23` | Cristal de magie `23` | Cristal de magie `23` |
| 6 | Balle des ténèbres `24` | Cristal d'ombre `25` | Pendentif vaporeux `26` | Robe de l'ombre `27` | Voile des ombres `28` | Voile des ombres `28` | Voile des ombres `28` | Voile des ombres `28` |
| 7 | Huile de sang `29` | Huile de sang `29` | Extrait d'esprits `19` | Extrait d'esprits `19` | Extrait d'esprits `19` | Extrait d'esprits `19` | Extrait d'esprits `19` | Extrait d'esprits `19` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.1 | coffre `25` | 219 | objet | rangee 1 : Balle de cristal (25 %), Balle de cristal (17 %), Horloge des mystères (15 %), Horloge du bonheur (13 %), Horloge du bonheur (11 %), Horloge des mystères (9 %), Anneau des philosophes (6 %), Anneau des philosophes (4 %) | -2900 / 1240 / -100 |
| 3.1 | coffre `25` | 218 | objet | rangee 0 : Parfum de convalescence (25 %), Parfum de guérison (17 %), Parfum d'impératrice (15 %), Parfum de feu (13 %), Parfum de feu (11 %), Parfum d'impératrice (9 %), Parfum de guérison (6 %), Parfum de convalescence (4 %) | -1300 / -360 / -1900 |
| 4.6 | coffre `25` | 230 | objet | rangee 3 : Manteau de croisé (25 %), Armure du dragon (17 %), Bouclier lunaire (15 %), Robe des anciens (13 %), Robe des anciens (11 %), Bouclier lunaire (9 %), Armure du dragon (6 %), Manteau de croisé (4 %) | -100 / -360 / -2500 |
| 5.1 | coffre `25` | 220 | objet | rangee 2 : Cape du printemps (25 %), Cape d'été (17 %), Cape d'automne (15 %), Cape d'hiver (13 %), Cape d'hiver (11 %), Cape d'automne (9 %), Cape d'été (6 %), Cape du printemps (4 %) | -1900 / 840 / -4100 |

### Zone 6, sous-zone 7 — repertoire de 21972 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 5 | 0 | 6 | 0x4000 | `00 05 00 00 00 06 00 40` |
| 1 | 11 | 1 | 5 | 0x4000 | `01 0b 00 00 01 05 00 40` |
| 2 | 8 | 2 | 7 | 0x4000 | `02 08 00 00 02 07 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Gargouille** | 80 | **121** | **100** | 21 | 68 | 1 | 50 | `74 72 61 69 68 86 70 70` |
| 1 | **Pourpre preux** | 86 | **190** | **52** | 38 | 63 | 1 | 50 | `86 90 69 66 53 94 60 62` |
| 2 | **Petit vampire** | 89 | **136** | **151** | 34 | 80 | 0 | 60 | `76 78 68 66 24 75 60 62` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1800 | -800 | -3700 | 2048 | 0x000c0b00 |
| 0 | 1 | 0 | -1600 | -800 | -3700 | 2048 | 0x000c0b00 |
| 0 | 2 | 0 | -1800 | -800 | -4100 | 0 | 0x000c0b00 |
| 0 | 3 | 0 | -1600 | -800 | -4100 | 0 | 0x000c0b00 |
| 1 | 0 | 0 | -1800 | -800 | -3700 | 2048 | 0x000c0b00 |
| 1 | 1 | 0 | -1600 | -800 | -3700 | 2048 | 0x000c0b00 |
| 1 | 2 | 1 | -1800 | -800 | -4100 | 0 | 0x000c0b01 |
| 1 | 3 | 1 | -1600 | -800 | -4100 | 0 | 0x000c0b01 |
| 2 | 0 | 2 | -2350 | -800 | -3100 | 0 | 0x000c0b02 |
| 2 | 1 | 2 | -2250 | -800 | -3150 | 0 | 0x000c0b02 |
| 2 | 2 | 2 | -2300 | -800 | -3400 | 0 | 0x000c0b02 |
| 3 | 0 | 2 | -1800 | -800 | -3700 | 2048 | 0x000c0b02 |
| 3 | 1 | 2 | -1600 | -800 | -3700 | 2048 | 0x000c0b02 |
| 3 | 2 | 2 | -1800 | -800 | -4100 | 0 | 0x000c0b02 |
| 3 | 3 | 2 | -1600 | -800 | -4100 | 0 | 0x000c0b02 |
| 4 | 0 | 1 | 400 | 0 | 4300 | 2048 | 0x000d0b01 |
| 4 | 1 | 1 | 400 | 0 | 4000 | 0 | 0x000d0b01 |
| 4 | 2 | 1 | -300 | 0 | 3900 | 3072 | 0x000d0b01 |
| 4 | 3 | 1 | 200 | 0 | 2800 | 0 | 0x000d0b01 |
| 4 | 4 | 1 | 600 | 0 | 2800 | 0 | 0x000d0b01 |
| 5 | 0 | 1 | -2900 | -600 | -900 | 0 | 0x000e0b01 |
| 5 | 1 | 1 | -2500 | -600 | -900 | 0 | 0x000e0b01 |
| 5 | 2 | 0 | -2700 | -800 | -1500 | 0 | 0x000e0b00 |
| 5 | 3 | 2 | -3300 | -800 | -1200 | 3072 | 0x000e0b02 |
| 5 | 4 | 2 | -3300 | -800 | -1600 | 3072 | 0x000e0b02 |
| 6 | 0 | 1 | -2900 | -600 | -900 | 0 | 0x000e0b01 |
| 6 | 1 | 1 | -2500 | -600 | -900 | 0 | 0x000e0b01 |
| 6 | 2 | 1 | -2700 | -800 | -1500 | 0 | 0x000e0b01 |
| 6 | 3 | 0 | -3300 | -800 | -1200 | 3072 | 0x000e0b00 |
| 6 | 4 | 0 | -3300 | -800 | -1600 | 3072 | 0x000e0b00 |
| 7 | 0 | 1 | -2900 | -600 | -900 | 0 | 0x000e0b01 |
| 7 | 1 | 1 | -2500 | -600 | -900 | 0 | 0x000e0b01 |
| 7 | 2 | 1 | -2700 | -800 | -1500 | 0 | 0x000e0b01 |
| 7 | 3 | 2 | -3300 | -800 | -1200 | 3072 | 0x000e0b02 |
| 7 | 4 | 2 | -3300 | -800 | -1600 | 3072 | 0x000e0b02 |
| 8 | 0 | 2 | -2900 | -600 | -900 | 0 | 0x000e0b02 |
| 8 | 1 | 2 | -2500 | -600 | -900 | 0 | 0x000e0b02 |
| 8 | 2 | 2 | -2700 | -800 | -1500 | 0 | 0x000e0b02 |
| 8 | 3 | 2 | -3300 | -800 | -1200 | 3072 | 0x000e0b02 |
| 8 | 4 | 2 | -3300 | -800 | -1600 | 3072 | 0x000e0b02 |
| 9 | 0 | 1 | 3650 | 0 | -600 | 0 | 0x0000ff01 |
| 9 | 1 | 1 | 3750 | 0 | -600 | 0 | 0x0000ff01 |
| 9 | 2 | 0 | 3000 | 0 | 300 | 0 | 0x0000ff00 |
| 9 | 3 | 0 | 3200 | 0 | 300 | 0 | 0x0000ff00 |
| 10 | 0 | 1 | 3650 | 0 | -600 | 0 | 0x0000ff01 |
| 10 | 1 | 1 | 3750 | 0 | -600 | 0 | 0x0000ff01 |
| 10 | 2 | 0 | 3600 | 0 | 1150 | 0 | 0x0000ff00 |
| 10 | 3 | 0 | 3800 | 0 | 1150 | 0 | 0x0000ff00 |
| 11 | 0 | 1 | 3650 | 0 | -600 | 0 | 0x0000ff01 |
| 11 | 1 | 1 | 3750 | 0 | -600 | 0 | 0x0000ff01 |
| 11 | 2 | 0 | 4200 | 0 | 300 | 0 | 0x0000ff00 |
| 11 | 3 | 0 | 4400 | 0 | 300 | 0 | 0x0000ff00 |
| 12 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 12 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 12 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 13 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 13 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 13 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 13 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 14 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Broche d'argent `0` | Broche d'argent `0` | Carte de tarot `1` | Carte de tarot `1` | Carte de tarot `1` | Carte de tarot `1` | Chaîne du souffle `2` | Chaîne du souffle `2` |
| 1 | Boîte à bijoux `3` | Boîte à bijoux `3` | Horloge du bonheur `4` | Horloge des mystères `5` | Horloge des mystères `5` | Horloge du bonheur `4` | Dropneal `6` | Dropneal `6` |
| 2 | Walmunk `7` | Poignard de sang `8` | Rapière du chaos `9` | Bâton stellaire `10` | Crosse d'ambre `11` | Flèche de faucon `12` | Hache du gel `13` | Baguette de l'arc-en-ciel `14` |
| 3 | Elixir de vie `15` | Elixir de vie `15` | Elixir de magicien `16` | Elixir de magicien `16` | Eau bénite `17` | Eau bénite `17` | Boîte à bijoux `3` | Boîte à bijoux `3` |
| 4 | Manteau de croisé `18` | Armure du dragon `19` | Bouclier lunaire `20` | Robe des anciens `21` | Robe des anciens `21` | Bouclier lunaire `20` | Armure du dragon `19` | Manteau de croisé `18` |
| 5 | Ecu du destin `22` | Ecu du destin `22` | Casque de fer `23` | Casque de fer `23` | Bannière de chevalier `24` | Bannière de chevalier `24` | Epée de sang `25` | Epée de sang `25` |
| 6 | Elixir de magicien `16` | Elixir de magicien `16` | Elixir de magicien `16` | Elixir de magicien `16` | Cristal de magie `26` | Cristal de magie `26` | Cristal de magie `26` | Cristal de magie `26` |
| 7 | Broche au joyau `27` | Chaîne des ténèbres `28` | Chaîne du mal `29` | Extrait d'esprits `30` | Plume `31` | Pointe de sang `32` | Pointe de sang `32` | Plume `31` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.0 | coffre `25` | 228 | message | — | 1300 / -560 / 900 |
| 5.2 | coffre `25` | 227 | message | — | -1300 / -960 / 500 |
| 6.0 | coffre `25` | -202 | objet | rangee 2 : Walmunk (25 %), Poignard de sang (17 %), Rapière du chaos (15 %), Bâton stellaire (13 %), Crosse d'ambre (11 %), Flèche de faucon (9 %), Hache du gel (6 %), Baguette de l'arc-en-ciel (4 %) | 3100 / 40 / 700 |
| 6.1 | coffre `25` | -203 | objet | rangee 1 : Boîte à bijoux (25 %), Boîte à bijoux (17 %), Horloge du bonheur (15 %), Horloge des mystères (13 %), Horloge des mystères (11 %), Horloge du bonheur (9 %), Dropneal (6 %), Dropneal (4 %) | 3700 / 40 / 1500 |
| 6.2 | coffre `25` | -204 | objet | rangee 0 : Broche d'argent (25 %), Broche d'argent (17 %), Carte de tarot (15 %), Carte de tarot (13 %), Carte de tarot (11 %), Carte de tarot (9 %), Chaîne du souffle (6 %), Chaîne du souffle (4 %) | 4300 / 40 / 700 |

### Zone 6, sous-zone 8 — repertoire de 16920 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 12 | 0 | 3 | 0x4000 | `00 0c 00 00 00 03 00 40` |
| 1 | 8 | 1 | 2 | 0x4000 | `01 08 00 00 01 02 00 40` |
| 2 | 12 | 2 | 3 | 0x4000 | `00 0c 00 00 02 03 00 40` |
| 3 | 8 | 3 | 2 | 0x4000 | `01 08 00 00 03 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Chimère** | 103 | **210** | **100** | 42 | 94 | 0 | 45 | `98 96 86 84 40 76 72 70` |
| 1 | **Petit vampire** | 89 | **136** | **151** | 34 | 80 | 0 | 60 | `76 78 68 66 24 75 60 62` |
| 2 | **Chimère** | 51 | **210** | **100** | 42 | 94 | 0 | 0 | `98 96 86 84 40 76 72 70` |
| 3 | **Petit vampire** | 48 | **136** | **151** | 34 | 80 | 0 | 0 | `76 78 68 66 24 75 64 65` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2500 | 680 | -5300 | 0 | 0x00080b00 |
| 0 | 1 | 0 | 2900 | 680 | -5300 | 0 | 0x00080b00 |
| 0 | 2 | 1 | 2900 | 680 | -3700 | 0 | 0x00080b01 |
| 0 | 3 | 1 | 2500 | 680 | -3700 | 0 | 0x00080b01 |
| 2 | 0 | 0 | -500 | 80 | -700 | 2048 | 0x000a0b00 |
| 2 | 1 | 0 | -100 | 80 | -700 | 2048 | 0x000a0b00 |
| 2 | 2 | 1 | -400 | 80 | -100 | 2048 | 0x000a0b01 |
| 2 | 3 | 1 | -200 | 80 | -100 | 2048 | 0x000a0b01 |
| 3 | 0 | 0 | 2350 | 680 | -300 | 2048 | 0x000b0b00 |
| 3 | 1 | 0 | 3050 | 680 | -300 | 2048 | 0x000b0b00 |
| 3 | 2 | 1 | 2700 | 680 | 200 | 2048 | 0x000b0b01 |
| 4 | 0 | 3 | -3645 | 760 | 500 | 0 | 0x003d0b03 |
| 5 | 0 | 2 | -4160 | 760 | -75 | 0 | 0x003e0b02 |
| 6 | 0 | 3 | -4160 | 760 | 400 | 0 | 0x003f0b03 |
| 6 | 1 | 2 | -3645 | 760 | 400 | 0 | 0x003f0b02 |
| 7 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 8 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Rapière aux éclairs `0` | Rapière aux éclairs `0` | Rapière aux éclairs `0` | Rapière aux éclairs `0` | Cristal d'air `1` | Cristal d'air `1` | Cristal d'air `1` | Cristal d'air `1` |
| 1 | Pentacle magique `2` | Médaillon d'or `3` | Poupée Dummy `4` | Poupée dansante `5` | Poupée dansante `5` | Poupée Dummy `4` | Médaillon d'or `3` | Pentacle magique `2` |
| 2 | Broche au joyau `6` | Chaîne des ténèbres `7` | Chaîne du mal `8` | Extrait d'esprits `9` | Plume `10` | Pointe de sang `11` | Pointe de sang `11` | Plume `10` |
| 3 | Lavande `12` | Sauge `13` | Poudre de zombie `14` | Poudre de zombie `14` | Cristal de magie `15` | Cristal d'ombre `16` | Cristal d'ombre `16` | Cristal de magie `15` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 4.0 | coffre `25` | 207 | objet | rangee 0 : Rapière aux éclairs (25 %), Rapière aux éclairs (17 %), Rapière aux éclairs (15 %), Rapière aux éclairs (13 %), Cristal d'air (11 %), Cristal d'air (9 %), Cristal d'air (6 %), Cristal d'air (4 %) | 3700 / 720 / -280 |
| 9.3 | coffre `25` | 286 | objet | rangee 1 : Pentacle magique (25 %), Médaillon d'or (17 %), Poupée Dummy (15 %), Poupée dansante (13 %), Poupée dansante (11 %), Poupée Dummy (9 %), Médaillon d'or (6 %), Pentacle magique (4 %) | -1500 / -160 / -7900 |

### Zone 6, sous-zone 9 — repertoire de 8064 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 12 | 0 | 0 | 0x4000 | `00 0c 00 00 00 00 00 40` |
| 1 | 8 | 1 | 1 | 0x4000 | `01 08 00 00 01 01 00 40` |
| 2 | 13 | 2 | 2 | 0x4000 | `02 0d 00 00 02 02 00 40` |
| 3 | 11 | 3 | 3 | 0x4000 | `03 0b 00 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Chimère** | 103 | **210** | **100** | 42 | 94 | 0 | 45 | `98 96 86 84 40 76 72 70` |
| 1 | **Vampire** | 98 | **156** | **181** | 34 | 80 | 0 | 60 | `76 88 72 76 24 95 74 75` |
| 2 | **Sacuba** | 125 | **190** | **200** | 62 | 120 | 1 | 52 | `102 144 96 99 80 176 124 120` |
| 3 | **Pourpre preux** | 86 | **190** | **52** | 38 | 63 | 1 | 50 | `86 90 69 66 53 94 60 62` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -700 | 1200 | 5900 | 2048 | 0x000c0b01 |
| 0 | 1 | 1 | 300 | 1200 | 5900 | 2048 | 0x000c0b01 |
| 0 | 2 | 2 | -200 | 1280 | 6000 | 2048 | 0x000c0b02 |
| 0 | 3 | 3 | 350 | 1160 | 4750 | 2048 | 0x000c0b03 |
| 0 | 4 | 3 | 300 | 1160 | 5000 | 2048 | 0x000c0b03 |
| 0 | 5 | 3 | -750 | 1160 | 4750 | 2048 | 0x000c0b03 |
| 0 | 6 | 3 | -700 | 1160 | 5000 | 2048 | 0x000c0b03 |
| 1 | 0 | 3 | 2800 | 760 | -1250 | 2048 | 0x000d0b03 |
| 1 | 1 | 3 | 2600 | 760 | -1250 | 2048 | 0x000d0b03 |
| 1 | 2 | 3 | 2600 | 760 | 150 | 0 | 0x000d0b03 |
| 1 | 3 | 0 | 2700 | 760 | -1000 | 2048 | 0x000d0b00 |
| 2 | 0 | 3 | 1700 | 760 | -2500 | 3072 | 0x000e0b03 |
| 2 | 1 | 3 | 1400 | 760 | -2500 | 3072 | 0x000e0b03 |
| 2 | 2 | 1 | 800 | 760 | -2500 | 3072 | 0x000e0b01 |
| 2 | 3 | 1 | 1000 | 760 | -2500 | 3072 | 0x000e0b01 |
| 3 | 0 | 3 | -350 | 760 | -2100 | 3072 | 0x000f0b03 |
| 3 | 1 | 3 | -150 | 760 | -2100 | 3072 | 0x000f0b03 |
| 4 | 0 | 1 | -1450 | 760 | -2500 | 0 | 0x00100b01 |
| 4 | 1 | 1 | -1850 | 760 | -1850 | 3584 | 0x00100b01 |
| 5 | 0 | 3 | -4000 | 760 | -1150 | 2048 | 0x00110b03 |
| 5 | 1 | 3 | -3800 | 760 | -1150 | 2048 | 0x00110b03 |
| 5 | 2 | 3 | -4000 | 760 | -50 | 0 | 0x00110b03 |
| 5 | 3 | 3 | -3800 | 760 | -50 | 0 | 0x00110b03 |
| 5 | 4 | 1 | -3900 | 760 | -550 | 2048 | 0x00110b01 |
| 6 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 6 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 7 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 7 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 8 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 8 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Lavande `0` | Sauge `1` | Poudre de zombie `2` | Poudre de zombie `2` | Cristal de magie `3` | Cristal d'ombre `4` | Cristal d'ombre `4` | Cristal de magie `3` |
| 1 | Extrait d'esprits `5` | Chaîne des ténèbres `6` | Chaîne du mal `7` | Bouclier de l'ombre `8` | Plume `9` | Pointe de sang `10` | Pointe de sang `10` | Plume `9` |
| 2 | Voile de beauté `11` | Voile mystique `12` | Bracelet de reine `13` | Tiare de la terre `14` | Tiare des étoiles `15` | Couronne de la lune `16` | Artémis `17` | Artémis `17` |
| 3 | Ecu du destin `18` | Ecu du destin `18` | Casque de fer `19` | Casque de fer `19` | Bannière de chevalier `20` | Bannière de chevalier `20` | Epée de sang `21` | Epée de sang `21` |

### Zone 6, sous-zone 10 — repertoire de 26416 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 11 | 0 | 0 | 0x4000 | `00 0b 00 00 00 00 00 40` |
| 1 | 8 | 1 | 1 | 0x4000 | `01 08 00 00 01 01 00 40` |
| 2 | 10 | 2 | 2 | 0x4000 | `02 0a 00 00 02 02 00 40` |
| 3 | 8 | 3 | 3 | 0x4000 | `01 08 02 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Pourpre preux** | 86 | **190** | **52** | 38 | 63 | 1 | 50 | `86 90 69 66 53 94 60 62` |
| 1 | **Vampire** | 98 | **156** | **181** | 34 | 80 | 0 | 60 | `76 88 72 76 24 95 74 75` |
| 2 | **Ombre** | 85 | **93** | **172** | 39 | 89 | 0 | 44 | `74 190 27 98 16 171 76 127` |
| 3 | **Petit vampire** | 89 | **136** | **151** | 34 | 80 | 0 | 60 | `76 78 68 66 24 75 60 62` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -1100 | 0 | 3300 | 3072 | 0x00500b00 |
| 0 | 1 | 0 | -1500 | 0 | 3700 | 2048 | 0x00500b00 |
| 0 | 2 | 0 | -1900 | 0 | 3300 | 1024 | 0x00500b00 |
| 0 | 3 | 0 | -1500 | 0 | 2900 | 0 | 0x00500b00 |
| 1 | 0 | 1 | -1100 | 0 | 3300 | 3072 | 0x00500b01 |
| 1 | 1 | 1 | -1500 | 0 | 3700 | 2048 | 0x00500b01 |
| 1 | 2 | 3 | -1900 | 0 | 3300 | 1024 | 0x00500b03 |
| 1 | 3 | 3 | -1500 | 0 | 2900 | 0 | 0x00500b03 |
| 2 | 0 | 2 | -1100 | 0 | 3300 | 3072 | 0x00500b02 |
| 2 | 1 | 2 | -1500 | 0 | 3700 | 2048 | 0x00500b02 |
| 2 | 2 | 2 | -1900 | 0 | 3300 | 1024 | 0x00500b02 |
| 2 | 3 | 2 | -1500 | 0 | 2900 | 0 | 0x00500b02 |
| 3 | 0 | 0 | -1100 | 0 | 3300 | 3072 | 0x00500b00 |
| 3 | 1 | 0 | -1500 | 0 | 3700 | 2048 | 0x00500b00 |
| 3 | 2 | 1 | -1900 | 0 | 3300 | 1024 | 0x00500b01 |
| 3 | 3 | 1 | -1500 | 0 | 2900 | 0 | 0x00500b01 |
| 4 | 0 | 1 | -1100 | 0 | 3300 | 3072 | 0x00500b01 |
| 4 | 1 | 1 | -1500 | 0 | 3700 | 2048 | 0x00500b01 |
| 4 | 2 | 2 | -1900 | 0 | 3300 | 1024 | 0x00500b02 |
| 4 | 3 | 2 | -1500 | 0 | 2900 | 0 | 0x00500b02 |
| 5 | 0 | 0 | 1000 | 0 | 3000 | 1536 | 0x00510b00 |
| 5 | 1 | 0 | 1200 | 0 | 3150 | 1024 | 0x00510b00 |
| 5 | 2 | 0 | 1200 | 0 | 3450 | 1024 | 0x00510b00 |
| 5 | 3 | 0 | 1000 | 0 | 3600 | 1536 | 0x00510b00 |
| 6 | 0 | 1 | 1000 | 0 | 3000 | 1536 | 0x00510b01 |
| 6 | 1 | 1 | 1200 | 0 | 3150 | 1024 | 0x00510b01 |
| 6 | 2 | 3 | 1200 | 0 | 3450 | 1024 | 0x00510b03 |
| 6 | 3 | 3 | 1000 | 0 | 3600 | 1536 | 0x00510b03 |
| 7 | 0 | 2 | 1000 | 0 | 3000 | 1536 | 0x00510b02 |
| 7 | 1 | 2 | 1200 | 0 | 3150 | 1024 | 0x00510b02 |
| 7 | 2 | 2 | 1200 | 0 | 3450 | 1024 | 0x00510b02 |
| 7 | 3 | 2 | 1000 | 0 | 3600 | 1536 | 0x00510b02 |
| 8 | 0 | 0 | 1200 | 0 | 3150 | 1024 | 0x00510b00 |
| 8 | 1 | 0 | 1200 | 0 | 3450 | 1024 | 0x00510b00 |
| 8 | 2 | 1 | 1000 | 0 | 3000 | 1536 | 0x00510b01 |
| 8 | 3 | 1 | 1000 | 0 | 3600 | 1536 | 0x00510b01 |
| 9 | 0 | 1 | 1200 | 0 | 3150 | 1024 | 0x00510b01 |
| 9 | 1 | 1 | 1200 | 0 | 3450 | 1024 | 0x00510b01 |
| 9 | 2 | 2 | 1000 | 0 | 3000 | 1536 | 0x00510b02 |
| 9 | 3 | 2 | 1000 | 0 | 3600 | 1536 | 0x00510b02 |
| 10 | 0 | 0 | -1100 | 0 | -600 | 1536 | 0x00520b00 |
| 10 | 1 | 0 | 600 | 0 | -500 | 2560 | 0x00520b00 |
| 10 | 2 | 1 | -1500 | 0 | -200 | 1536 | 0x00520b01 |
| 10 | 3 | 1 | 900 | 0 | -200 | 2560 | 0x00520b01 |
| 11 | 0 | 0 | -2100 | 0 | 1100 | 1024 | 0x00530b00 |
| 11 | 1 | 0 | -2100 | 0 | 700 | 1024 | 0x00530b00 |
| 11 | 2 | 0 | -1100 | 0 | 700 | 2560 | 0x00530b00 |
| 12 | 0 | 3 | -2100 | 0 | 1100 | 1024 | 0x00530b03 |
| 12 | 1 | 3 | -2100 | 0 | 700 | 1024 | 0x00530b03 |
| 12 | 2 | 1 | -1100 | 0 | 700 | 2560 | 0x00530b01 |
| 13 | 0 | 2 | -2100 | 0 | 1100 | 1024 | 0x00530b02 |
| 13 | 1 | 2 | -2100 | 0 | 700 | 1024 | 0x00530b02 |
| 13 | 2 | 2 | -1100 | 0 | 700 | 2560 | 0x00530b02 |
| 14 | 0 | 0 | -2100 | 0 | 700 | 1024 | 0x00530b00 |
| 14 | 1 | 1 | -1100 | 0 | 700 | 2560 | 0x00530b01 |
| 14 | 2 | 2 | -2100 | 0 | 1100 | 1024 | 0x00530b02 |
| 15 | 0 | 0 | -1100 | 0 | 700 | 2560 | 0x00530b00 |
| 15 | 1 | 1 | -2100 | 0 | 1100 | 1024 | 0x00530b01 |
| 15 | 2 | 1 | -2100 | 0 | 700 | 1024 | 0x00530b01 |
| 16 | 0 | 1 | -2900 | 1400 | 900 | 3072 | 0x00540b01 |
| 16 | 1 | 1 | -2700 | 1400 | 1000 | 2048 | 0x00540b01 |
| 16 | 2 | 3 | -2500 | 1400 | 1100 | 0 | 0x00540b03 |
| 16 | 3 | 3 | -2700 | 1400 | 800 | 0 | 0x00540b03 |
| 17 | 0 | 1 | 2100 | 1400 | 1100 | 2048 | 0x00550b01 |
| 17 | 1 | 1 | 2050 | 1400 | 750 | 0 | 0x00550b01 |
| 17 | 2 | 3 | 2150 | 1400 | 750 | 0 | 0x00550b03 |
| 17 | 3 | 3 | 1900 | 1400 | 1050 | 2048 | 0x00550b03 |
| 18 | 0 | 1 | -300 | 2400 | 3000 | 0 | 0x00560b01 |
| 18 | 1 | 1 | -300 | 2400 | 2300 | 0 | 0x00560b01 |
| 18 | 2 | 1 | -300 | 2400 | 1700 | 0 | 0x00560b01 |
| 18 | 3 | 1 | -300 | 2400 | 1100 | 0 | 0x00560b01 |
| 19 | 0 | 0 | -300 | 2400 | 3000 | 0 | 0x00560b00 |
| 19 | 1 | 0 | -300 | 2400 | 2300 | 0 | 0x00560b00 |
| 19 | 2 | 0 | -300 | 2400 | 1700 | 0 | 0x00560b00 |
| 19 | 3 | 0 | -300 | 2400 | 1100 | 0 | 0x00560b00 |
| 20 | 0 | 2 | -300 | 2400 | 3000 | 0 | 0x00560b02 |
| 20 | 1 | 2 | -300 | 2400 | 2300 | 0 | 0x00560b02 |
| 20 | 2 | 2 | -300 | 2400 | 1700 | 0 | 0x00560b02 |
| 20 | 3 | 2 | -300 | 2400 | 1100 | 0 | 0x00560b02 |
| 21 | 0 | 0 | -320 | 1600 | 1600 | 0 | 0x00570b00 |
| 21 | 1 | 0 | -320 | 1600 | 2500 | 0 | 0x00570b00 |
| 21 | 2 | 0 | 100 | 1600 | 2100 | 0 | 0x00570b00 |
| 21 | 3 | 0 | -700 | 1600 | 2100 | 0 | 0x00570b00 |
| 22 | 0 | 1 | -320 | 1600 | 1600 | 0 | 0x00570b01 |
| 22 | 1 | 1 | -320 | 1600 | 2500 | 0 | 0x00570b01 |
| 22 | 2 | 3 | 100 | 1600 | 2100 | 0 | 0x00570b03 |
| 22 | 3 | 3 | -700 | 1600 | 2100 | 0 | 0x00570b03 |
| 23 | 0 | 2 | -320 | 1600 | 1600 | 0 | 0x00570b02 |
| 23 | 1 | 2 | -320 | 1600 | 2500 | 0 | 0x00570b02 |
| 23 | 2 | 2 | 100 | 1600 | 2100 | 0 | 0x00570b02 |
| 23 | 3 | 2 | -700 | 1600 | 2100 | 0 | 0x00570b02 |
| 24 | 0 | 0 | -320 | 1600 | 1600 | 0 | 0x00570b00 |
| 24 | 1 | 0 | -320 | 1600 | 2500 | 0 | 0x00570b00 |
| 24 | 2 | 1 | 100 | 1600 | 2100 | 0 | 0x00570b01 |
| 24 | 3 | 1 | -700 | 1600 | 2100 | 0 | 0x00570b01 |
| 25 | 0 | 1 | -320 | 1600 | 1600 | 0 | 0x00570b01 |
| 25 | 1 | 1 | -320 | 1600 | 2500 | 0 | 0x00570b01 |
| 25 | 2 | 2 | 100 | 1600 | 2100 | 0 | 0x00570b02 |
| 25 | 3 | 2 | -700 | 1600 | 2100 | 0 | 0x00570b02 |
| 26 | 0 | 1 | 1000 | 0 | 2300 | 3072 | 0x00580b01 |
| 26 | 1 | 1 | 1000 | 0 | 2100 | 3072 | 0x00580b01 |
| 26 | 2 | 2 | 1250 | 0 | 2200 | 3072 | 0x00580b02 |
| 27 | 0 | 2 | 1000 | 0 | 2300 | 3072 | 0x00580b02 |
| 27 | 1 | 2 | 1000 | 0 | 2100 | 3072 | 0x00580b02 |
| 27 | 2 | 2 | 1250 | 0 | 2200 | 3072 | 0x00580b02 |
| 28 | 0 | 1 | 1000 | 0 | 2300 | 3072 | 0x00580b01 |
| 28 | 1 | 1 | 1000 | 0 | 2100 | 3072 | 0x00580b01 |
| 28 | 2 | 1 | 1250 | 0 | 2200 | 3072 | 0x00580b01 |
| 29 | 0 | 1 | -300 | 0 | 450 | 0 | 0x00590b01 |
| 29 | 1 | 1 | -150 | 0 | 550 | 0 | 0x00590b01 |
| 29 | 2 | 3 | -50 | 0 | 900 | 3072 | 0x00590b03 |
| 29 | 3 | 3 | -150 | 0 | 1100 | 3072 | 0x00590b03 |
| 30 | 0 | 2 | -300 | 0 | 450 | 0 | 0x00590b02 |
| 30 | 1 | 2 | -150 | 0 | 550 | 0 | 0x00590b02 |
| 30 | 2 | 2 | -50 | 0 | 900 | 3072 | 0x00590b02 |
| 30 | 3 | 2 | -150 | 0 | 1100 | 3072 | 0x00590b02 |
| 31 | 0 | 0 | -300 | 0 | 450 | 0 | 0x00590b00 |
| 31 | 1 | 0 | -150 | 0 | 550 | 0 | 0x00590b00 |
| 31 | 2 | 1 | -50 | 0 | 900 | 3072 | 0x00590b01 |
| 31 | 3 | 2 | -150 | 0 | 1100 | 3072 | 0x00590b02 |
| 32 | 0 | 1 | -1700 | 0 | 2300 | 1024 | 0x005a0b01 |
| 32 | 1 | 1 | -1700 | 0 | 1900 | 1024 | 0x005a0b01 |
| 32 | 2 | 1 | -1300 | 0 | 2200 | 1024 | 0x005a0b01 |
| 32 | 3 | 2 | -100 | 0 | 3500 | 2048 | 0x005a0b02 |
| 32 | 4 | 2 | -500 | 0 | 3500 | 2048 | 0x005a0b02 |
| 33 | 0 | 0 | -1700 | 0 | 2300 | 1024 | 0x005a0b00 |
| 33 | 1 | 0 | -1700 | 0 | 1900 | 1024 | 0x005a0b00 |
| 33 | 2 | 0 | -1300 | 0 | 2200 | 1024 | 0x005a0b00 |
| 33 | 3 | 1 | -100 | 0 | 3500 | 2048 | 0x005a0b01 |
| 33 | 4 | 1 | -500 | 0 | 3500 | 2048 | 0x005a0b01 |
| 34 | 0 | 2 | -1700 | 0 | 2300 | 1024 | 0x005a0b02 |
| 34 | 1 | 2 | -1700 | 0 | 1900 | 1024 | 0x005a0b02 |
| 34 | 2 | 2 | -1300 | 0 | 2200 | 2048 | 0x005a0b02 |
| 34 | 3 | 2 | -100 | 0 | 3500 | 2048 | 0x005a0b02 |
| 34 | 4 | 2 | -500 | 0 | 3500 | 2048 | 0x005a0b02 |
| 35 | 0 | 0 | -800 | 1400 | -150 | 2560 | 0x005b0b00 |
| 35 | 1 | 0 | -450 | 1550 | 100 | 3072 | 0x005b0b00 |
| 35 | 2 | 0 | -150 | 1550 | 100 | 1024 | 0x005b0b00 |
| 35 | 3 | 0 | 200 | 1400 | -150 | 1536 | 0x005b0b00 |
| 35 | 4 | 1 | -300 | 1600 | 100 | 2048 | 0x005b0b01 |
| 36 | 0 | 2 | -800 | 1400 | -150 | 2560 | 0x005b0b02 |
| 36 | 1 | 2 | 200 | 1400 | -150 | 1536 | 0x005b0b02 |
| 36 | 2 | 0 | -450 | 1550 | 100 | 3072 | 0x005b0b00 |
| 36 | 3 | 0 | -150 | 1550 | 100 | 1024 | 0x005b0b00 |
| 36 | 4 | 1 | -300 | 1600 | 100 | 2048 | 0x005b0b01 |
| 37 | 0 | 0 | -450 | 1550 | 100 | 3072 | 0x005b0b00 |
| 37 | 1 | 0 | -150 | 1550 | 100 | 1024 | 0x005b0b00 |
| 37 | 2 | 1 | -800 | 1400 | -150 | 2560 | 0x005b0b01 |
| 37 | 3 | 1 | 200 | 1400 | -150 | 1536 | 0x005b0b01 |
| 37 | 4 | 1 | -300 | 1600 | 100 | 2048 | 0x005b0b01 |
| 38 | 0 | 1 | -450 | 1550 | 100 | 3072 | 0x005b0b01 |
| 38 | 1 | 1 | -150 | 1550 | 100 | 1024 | 0x005b0b01 |
| 38 | 2 | 2 | -800 | 1400 | -150 | 2560 | 0x005b0b02 |
| 38 | 3 | 2 | 200 | 1400 | -150 | 1536 | 0x005b0b02 |
| 38 | 4 | 0 | -300 | 1600 | 100 | 2048 | 0x005b0b00 |
| 39 | 0 | 1 | 900 | 1600 | 2900 | 1536 | 0x005c0b01 |
| 39 | 1 | 1 | 700 | 1600 | 2700 | 1024 | 0x005c0b01 |
| 39 | 2 | 3 | 1500 | 1600 | 3200 | 3072 | 0x005c0b03 |
| 39 | 3 | 3 | 1500 | 1600 | 3400 | 3072 | 0x005c0b03 |
| 39 | 4 | 3 | 1100 | 1600 | 3500 | 2048 | 0x005c0b03 |
| 40 | 0 | 0 | 900 | 1600 | 2900 | 1536 | 0x005c0b00 |
| 40 | 1 | 0 | 700 | 1600 | 2700 | 1024 | 0x005c0b00 |
| 40 | 2 | 0 | 1500 | 1600 | 3200 | 3072 | 0x005c0b00 |
| 40 | 3 | 0 | 1500 | 1600 | 3400 | 3072 | 0x005c0b00 |
| 40 | 4 | 0 | 1100 | 1600 | 3500 | 2048 | 0x005c0b00 |
| 41 | 0 | 2 | 900 | 1600 | 2900 | 1536 | 0x005c0b02 |
| 41 | 1 | 2 | 700 | 1600 | 2700 | 1024 | 0x005c0b02 |
| 41 | 2 | 1 | 1500 | 1600 | 3200 | 3072 | 0x005c0b01 |
| 41 | 3 | 1 | 1500 | 1600 | 3400 | 3072 | 0x005c0b01 |
| 41 | 4 | 3 | 1100 | 1600 | 3500 | 2048 | 0x005c0b03 |
| 42 | 0 | 1 | 900 | 1600 | 2900 | 1536 | 0x005c0b01 |
| 42 | 1 | 1 | 700 | 1600 | 2700 | 1024 | 0x005c0b01 |
| 42 | 2 | 0 | 1500 | 1600 | 3200 | 3072 | 0x005c0b00 |
| 42 | 3 | 0 | 1500 | 1600 | 3400 | 3072 | 0x005c0b00 |
| 42 | 4 | 0 | 1100 | 1600 | 3500 | 2048 | 0x005c0b00 |
| 43 | 0 | 0 | 1950 | 1400 | 50 | 512 | 0x005d0b00 |
| 43 | 1 | 1 | 1650 | 1400 | -250 | 2560 | 0x005d0b01 |
| 44 | 0 | 2 | 1950 | 1400 | 50 | 512 | 0x005d0b02 |
| 44 | 1 | 2 | 1800 | 1400 | -100 | 1536 | 0x005d0b02 |
| 44 | 2 | 2 | 1650 | 1400 | -250 | 2560 | 0x005d0b02 |
| 45 | 0 | 0 | -2550 | 1400 | 50 | 3584 | 0x005e0b00 |
| 45 | 1 | 1 | -2150 | 1400 | -350 | 1536 | 0x005e0b01 |
| 46 | 0 | 2 | -2150 | 1400 | -350 | 1536 | 0x005e0b02 |
| 46 | 1 | 2 | -2550 | 1400 | 50 | 3584 | 0x005e0b02 |
| 46 | 2 | 2 | -2400 | 1400 | -100 | 2560 | 0x005e0b02 |
| 47 | 0 | 0 | 500 | 0 | 500 | 1536 | 0x005f0b00 |
| 47 | 1 | 0 | 1500 | 0 | 700 | 3072 | 0x005f0b00 |
| 47 | 2 | 0 | 1500 | 0 | 1100 | 3072 | 0x005f0b00 |
| 48 | 0 | 1 | 500 | 0 | 500 | 1536 | 0x005f0b01 |
| 48 | 1 | 1 | 1500 | 0 | 700 | 3072 | 0x005f0b01 |
| 48 | 2 | 1 | 1500 | 0 | 1100 | 3072 | 0x005f0b01 |
| 49 | 0 | 2 | 500 | 0 | 500 | 1536 | 0x005f0b02 |
| 49 | 1 | 2 | 1500 | 0 | 700 | 3072 | 0x005f0b02 |
| 49 | 2 | 2 | 1500 | 0 | 1100 | 3072 | 0x005f0b02 |
| 50 | 0 | 0 | 1500 | 0 | 700 | 3072 | 0x005f0b00 |
| 50 | 1 | 0 | 1500 | 0 | 1100 | 3072 | 0x005f0b00 |
| 50 | 2 | 1 | 500 | 0 | 500 | 1536 | 0x005f0b01 |
| 51 | 0 | 1 | 1500 | 0 | 700 | 3072 | 0x005f0b01 |
| 51 | 1 | 1 | 1500 | 0 | 1100 | 3072 | 0x005f0b01 |
| 51 | 2 | 2 | 500 | 0 | 500 | 1536 | 0x005f0b02 |
| 52 | 0 | 1 | 950 | 1400 | 1300 | 2048 | 0x00600b01 |
| 52 | 1 | 1 | 850 | 1400 | 1300 | 2048 | 0x00600b01 |
| 52 | 2 | 3 | 1450 | 1400 | 700 | 1536 | 0x00600b03 |
| 52 | 3 | 3 | 1450 | 1400 | 1100 | 2560 | 0x00600b03 |
| 52 | 4 | 3 | 900 | 1400 | 700 | 0 | 0x00600b03 |
| 53 | 0 | 0 | 950 | 1400 | 1300 | 2048 | 0x00600b00 |
| 53 | 1 | 0 | 850 | 1400 | 1300 | 2048 | 0x00600b00 |
| 53 | 2 | 0 | 1450 | 1400 | 700 | 1536 | 0x00600b00 |
| 53 | 3 | 0 | 1450 | 1400 | 1100 | 2560 | 0x00600b00 |
| 53 | 4 | 0 | 900 | 1400 | 700 | 0 | 0x00600b00 |
| 54 | 0 | 2 | 950 | 1400 | 1300 | 2048 | 0x00600b02 |
| 54 | 1 | 2 | 850 | 1400 | 1300 | 2048 | 0x00600b02 |
| 54 | 2 | 2 | 900 | 1400 | 700 | 0 | 0x00600b02 |
| 54 | 3 | 1 | 1450 | 1400 | 700 | 1536 | 0x00600b01 |
| 54 | 4 | 1 | 1450 | 1400 | 1100 | 2560 | 0x00600b01 |
| 55 | 0 | 1 | 950 | 1400 | 1300 | 2048 | 0x00600b01 |
| 55 | 1 | 1 | 850 | 1400 | 1300 | 2048 | 0x00600b01 |
| 55 | 2 | 1 | 900 | 1400 | 700 | 0 | 0x00600b01 |
| 55 | 3 | 0 | 1450 | 1400 | 700 | 1536 | 0x00600b00 |
| 55 | 4 | 0 | 1450 | 1400 | 1100 | 2560 | 0x00600b00 |
| 56 | 0 | 1 | -1500 | 1400 | 1300 | 2048 | 0x00610b01 |
| 56 | 1 | 1 | -2100 | 1400 | 1050 | 1024 | 0x00610b01 |
| 56 | 2 | 3 | -2100 | 1400 | 950 | 1024 | 0x00610b03 |
| 56 | 3 | 3 | -1900 | 1400 | 500 | 512 | 0x00610b03 |
| 56 | 4 | 3 | -1300 | 1400 | 500 | 0 | 0x00610b03 |
| 57 | 0 | 0 | -1500 | 1400 | 1300 | 2048 | 0x00610b00 |
| 57 | 1 | 0 | -2100 | 1400 | 1050 | 1024 | 0x00610b00 |
| 57 | 2 | 0 | -2100 | 1400 | 950 | 1024 | 0x00610b00 |
| 57 | 3 | 0 | -1900 | 1400 | 500 | 512 | 0x00610b00 |
| 57 | 4 | 0 | -1300 | 1400 | 500 | 0 | 0x00610b00 |
| 58 | 0 | 2 | -1500 | 1400 | 1300 | 2048 | 0x00610b02 |
| 58 | 1 | 2 | -2100 | 1400 | 1050 | 1024 | 0x00610b02 |
| 58 | 2 | 2 | -2100 | 1400 | 950 | 1024 | 0x00610b02 |
| 58 | 3 | 2 | -1900 | 1400 | 500 | 512 | 0x00610b02 |
| 58 | 4 | 2 | -1300 | 1400 | 500 | 0 | 0x00610b02 |
| 59 | 0 | 1 | -1500 | 1400 | 1300 | 2048 | 0x00610b01 |
| 59 | 1 | 1 | -1900 | 1400 | 500 | 512 | 0x00610b01 |
| 59 | 2 | 1 | -1300 | 1400 | 500 | 0 | 0x00610b01 |
| 59 | 3 | 0 | -2100 | 1400 | 1050 | 1024 | 0x00610b00 |
| 59 | 4 | 0 | -2100 | 1400 | 950 | 1024 | 0x00610b00 |
| 60 | 0 | 0 | 3300 | 400 | -3900 | 2048 | 0x00620b00 |
| 60 | 1 | 0 | 3700 | 400 | -3900 | 2048 | 0x00620b00 |
| 60 | 2 | 0 | 4100 | 400 | -3900 | 2048 | 0x00620b00 |
| 60 | 3 | 2 | 4100 | 0 | -4400 | 2048 | 0x00620b02 |
| 60 | 4 | 2 | 3300 | 0 | -4400 | 2048 | 0x00620b02 |
| 61 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 61 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 61 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 62 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 62 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 62 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 63 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 63 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 63 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 63 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 64 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 64 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 64 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Ecu du destin `0` | Ecu du destin `0` | Casque de fer `1` | Casque de fer `1` | Bannière de chevalier `2` | Bannière de chevalier `2` | Epée de sang `3` | Epée de sang `3` |
| 1 | Extrait d'esprits `4` | Chaîne des ténèbres `5` | Chaîne du mal `6` | Bouclier de l'ombre `7` | Plume `8` | Pointe de sang `9` | Pointe de sang `9` | Plume `8` |
| 2 | Balle des ténèbres `10` | Cristal d'ombre `11` | Pendentif vaporeux `12` | Robe de l'ombre `13` | Voile des ombres `14` | Voile des ombres `14` | Voile des ombres `14` | Voile des ombres `14` |
| 3 | Broche au joyau `15` | Chaîne des ténèbres `5` | Chaîne du mal `6` | Extrait d'esprits `4` | Plume `8` | Pointe de sang `9` | Pointe de sang `9` | Plume `8` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 7.0 | coffre `25` | 216 | message | — | 1100 / 40 / 2300 |

### Zone 6, sous-zone 11 — repertoire de 10036 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 14 | 0 | 0 | 0x4000 | `01 0e 00 00 00 00 00 40` |
| 1 | 14 | 1 | 0 | 0x4000 | `01 0e 00 00 01 00 00 40` |
| 2 | 13 | 2 | 1 | 0x4000 | `02 0d 00 00 02 01 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Nocturio** | 150 | **840** | **600** | 79 | 140 | 1 | 300 | `120 120 98 98 134 129 111 110` |
| 1 | **Nocturio** | 150 | **840** | **600** | 79 | 140 | 1 | 300 | `120 210 98 98 134 199 111 110` |
| 2 | **Sacuba** | 125 | **190** | **200** | 62 | 120 | 1 | 52 | `102 144 96 99 80 176 124 120` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 1950 | 850 | 0 | 1024 | 0x00070b00 |
| 1 | 0 | 1 | -1900 | 400 | 0 | 0 | 0x00090b01 |
| 1 | 1 | 2 | -2000 | 0 | -300 | 0 | 0x0000ff02 |
| 1 | 2 | 2 | -2000 | 0 | 300 | 2048 | 0x0000ff02 |
| 2 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Pentacle de prince `0` | Robe du démon `1` | Poupée Dummy `2` | Table des anciens `3` | Table de vision `4` | Manteau des esprits `5` | Anneau de Merlin `6` | Rapière de Zeus `7` |
| 1 | Voile de beauté `8` | Voile mystique `9` | Bracelet de reine `10` | Tiare de la terre `11` | Tiare des étoiles `12` | Couronne de la lune `13` | Artémis `14` | Artémis `14` |

## Zone 7 — 848 octets, 3 sous-zone(s)

### Zone 7, sous-zone 0 — repertoire de 12124 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2 | 0x4000 | `00 00 00 00 00 02 00 40` |
| 1 | 1 | 1 | 3 | 0x4000 | `01 01 00 00 01 03 00 40` |
| 2 | 3 | 2 | 4 | 0x4000 | `02 03 00 00 02 04 00 40` |
| 3 | 3 | 2 | 4 | 0x4000 | `02 03 00 00 02 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Crapaud** | 38 | **71** | **4** | 21 | 62 | 3 | 50 | `44 37 61 29 31 36 31 35` |
| 1 | **Balle métal** | 87 | **200** | **26** | 41 | 8 | 10 | 40 | `96 200 82 200 40 200 86 200` |
| 2 | **Loup** | 38 | **60** | **31** | 35 | 38 | 5 | 70 | `38 41 29 27 28 32 29 21` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 0 | 1 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 0 | 2 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 0 | 3 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 0 | 4 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 0 | 5 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 0 | 6 | 1 | 2200 | -500 | 1400 | 0 | 0x00040b01 |
| 1 | 0 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 1 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 2 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 3 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 4 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 5 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 6 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 1 | 7 | 1 | -1800 | -500 | 1600 | 0 | 0x00050b01 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 4 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Bague réflectrice `0` | Dropneal `1` | Anneau de Merlin `2` | Robe magique `3` | Bouclier solaire `4` | Armure du gardien `5` | Table de vision `6` | Table de vision `6` |
| 1 | Cristal de magie `7` | Cristal de magie `7` | Cristal de magie `7` | Anneau des éléments `8` | Bouclier du gardien `9` | Bouclier du gardien `9` | Epée du gardien `10` | Epée du gardien `10` |
| 2 | Elixir des esprits `11` | Elixir des esprits `11` | Elixir des esprits `11` | Elixir des esprits `11` | Cristal de magie `7` | Cristal de magie `7` | Cristal de magie `7` | Cristal de magie `7` |
| 3 | Cristal de magie `7` | Cuirasse au miroir `12` | Bouclier d'argent `13` | Bouclier réflecteur `14` | Joyau de feu `15` | Joyau de glace `16` | Joyau de terre `17` | Joyau de vent `18` |
| 4 | Potion med `19` | Potion med `19` | Elixir de vie `20` | Elixir de vie `20` | Extrait de sang `21` | Extrait de sang `21` | Extrait de sang `21` | Extrait de sang `21` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.0 | coffre `8` | 250 | objet | rangee 0 : Bague réflectrice (25 %), Dropneal (17 %), Anneau de Merlin (15 %), Robe magique (13 %), Bouclier solaire (11 %), Armure du gardien (9 %), Table de vision (6 %), Table de vision (4 %) | 3150 / -420 / 1500 |
| 2.1 | coffre `8` | 251 | objet | rangee 1 : Cristal de magie (25 %), Cristal de magie (17 %), Cristal de magie (15 %), Anneau des éléments (13 %), Bouclier du gardien (11 %), Bouclier du gardien (9 %), Epée du gardien (6 %), Epée du gardien (4 %) | -2900 / -420 / 1500 |

### Zone 7, sous-zone 1 — repertoire de 6728 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 0 | 0 | 0x4000 | `00 02 00 00 00 00 00 40` |
| 1 | 4 | 1 | 1 | 0x4000 | `01 04 00 00 01 01 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Chimère** | 103 | **210** | **100** | 42 | 94 | 0 | 45 | `103 105 90 110 100 76 120 110` |
| 1 | **Balle métal** | 140 | **225** | **130** | 7 | 80 | 10 | 80 | `86 360 81 322 61 320 70 308` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 100 | 400 | 4100 | 0 | 0x00020b00 |
| 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 1 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Lavande `0` | Sauge `1` | Poudre de zombie `2` | Poudre de zombie `2` | Cristal de magie `3` | Cristal d'ombre `4` | Cristal d'ombre `4` | Cristal de magie `3` |
| 1 | Cristal de magie `3` | Cristal de magie `3` | Dropneal `5` | Dropneal `5` | Dropneal `5` | Dropneal `5` | Dropneal `5` | Arc gris `6` |

### Zone 7, sous-zone 2 — repertoire de 3632 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 5 | 0 | 0 | 0x4000 | `00 05 00 00 00 00 00 40` |
| 1 | 5 | 1 | 0 | 0x4000 | `01 05 00 00 01 00 00 40` |
| 2 | 5 | 2 | 0 | 0x4000 | `02 05 00 00 02 00 00 40` |
| 3 | 5 | 3 | 0 | 0x4000 | `03 05 00 00 03 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Sosie** | 90 | **1** | **1** | 80 | 1 | 8 | 40 | `1 1 1 1 1 1 1 1` |
| 1 | **Sosie** | 1 | **1** | **1** | 80 | 1 | 8 | 40 | `1 1 1 1 1 1 1 1` |
| 2 | **Sosie** | 1 | **1** | **1** | 80 | 1 | 8 | 40 | `1 1 1 1 1 1 1 1` |
| 3 | **Sosie** | 1 | **1** | **1** | 80 | 1 | 1 | 40 | `1 1 1 1 1 1 1 1` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -400 | 500 | 3600 | 2048 | 0x00070b00 |
| 1 | 0 | 0 | -400 | 500 | 3600 | 2048 | 0x00070b00 |
| 1 | 1 | 1 | -400 | 500 | 4400 | 2048 | 0x00070b01 |
| 2 | 0 | 0 | -400 | 500 | 3600 | 2048 | 0x00070b00 |
| 2 | 1 | 1 | -400 | 500 | 4400 | 2048 | 0x00070b01 |
| 2 | 2 | 2 | 400 | 500 | 3600 | 2048 | 0x00070b02 |
| 3 | 0 | 0 | -400 | 500 | 3600 | 2048 | 0x00070b00 |
| 3 | 1 | 1 | -400 | 500 | 4400 | 2048 | 0x00070b01 |
| 3 | 2 | 2 | 400 | 500 | 3600 | 2048 | 0x00070b02 |
| 3 | 3 | 3 | 400 | 500 | 4400 | 2048 | 0x00070b03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Poudre de zombie `0` | Elixir de magicien `1` | Panoramix `2` | Cristal de magie `3` | Cristal de magie `3` | Panoramix `2` | Elixir de magicien `1` | Poudre de zombie `0` |

## Zone 8 — 836 octets, 2 sous-zone(s)

### Zone 8, sous-zone 0 — repertoire de 7040 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 0 | 0 | 0x4000 | `00 01 00 00 00 00 00 40` |
| 1 | 0 | 1 | 1 | 0x4000 | `01 00 00 00 01 01 00 40` |
| 2 | 2 | 2 | 2 | 0x4000 | `02 02 00 00 02 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Ours blanc** | 50 | **92** | **19** | 6 | 54 | 3 | 62 | `72 67 50 46 20 30 32 31` |
| 1 | **Loup blanc** | 42 | **80** | **31** | 35 | 38 | 5 | 70 | `67 46 36 42 18 32 30 24` |
| 2 | **Grifon** | 65 | **121** | **60** | 52 | 60 | 1 | 40 | `75 72 57 56 23 51 40 39` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 1 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 1 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 2 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 3 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Extrait de sang `0` | Extrait de sang `0` | Cape d'hiver `1` | Joyau de glace `2` | Joyau de glace `2` | Balle de gel `3` | Balle de gel `3` | Cape d'hiver `1` |
| 1 | Potion med `4` | Potion med `4` | Cape d'hiver `1` | Elixir de vie `5` | Elixir de vie `5` | Extrait de sang `0` | Extrait de sang `0` | Cape d'hiver `1` |
| 2 | Chaîne de l'air `6` | Plume `7` | Gants ailés `8` | Manteau à plumes `9` | Chaîne de l'air `6` | Plume `7` | Gants ailés `8` | Manteau à plumes `9` |

### Zone 8, sous-zone 1 — repertoire de 5748 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 2 | 0 | 1 | 0x4000 | `00 02 00 00 00 01 00 40` |
| 1 | 3 | 1 | 2 | 0x4000 | `01 03 00 00 01 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Grifon** | 65 | **121** | **60** | 52 | 60 | 1 | 40 | `75 72 57 56 23 51 40 39` |
| 1 | **Fougre** | 180 | **1030** | **100** | 85 | 200 | 3 | 200 | `120 110 116 81 112 104 112 94` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -1500 | 500 | -700 | 1024 | 0x00050b01 |
| 0 | 1 | 0 | -1500 | 500 | -800 | 1024 | 0x0000ff00 |
| 0 | 2 | 0 | -1500 | 500 | -600 | 1024 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Poignard de brigand `0` | Poignard de brigand `0` | Boîte à bijoux `1` | Corde du retour `2` | Cendre grise `3` | Cendre grise `3` | Corde du retour `2` | Boîte à bijoux `1` |
| 1 | Chaîne de l'air `4` | Plume `5` | Gants ailés `6` | Manteau à plumes `7` | Chaîne de l'air `4` | Plume `5` | Gants ailés `6` | Manteau à plumes `7` |
| 2 | Balle de lumière `8` | Cristal d'énergie `9` | Balle de l'illusion `10` | Chaînette de plumes `11` | Casque ailé `12` | Bottes ailées `13` | Gantelets ailés `14` | Flèche de Persée `15` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 1.1 | coffre `18` | 251 | objet | rangee 0 : Poignard de brigand (25 %), Poignard de brigand (17 %), Boîte à bijoux (15 %), Corde du retour (13 %), Cendre grise (11 %), Cendre grise (9 %), Corde du retour (6 %), Boîte à bijoux (4 %) | 3300 / 240 / -3350 |

## Zone 9 — 836 octets, 2 sous-zone(s)

### Zone 9, sous-zone 0 — repertoire de 12936 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x4000 | `00 00 00 00 00 00 00 40` |
| 1 | 1 | 1 | 1 | 0x4000 | `01 01 00 00 01 01 00 40` |
| 2 | 2 | 2 | 2 | 0x4000 | `02 02 00 00 02 02 00 40` |
| 3 | 3 | 3 | 3 | 0x4000 | `03 03 00 00 03 03 00 40` |
| 4 | 4 | 4 | 4 | 0x4000 | `04 04 00 00 04 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Lézard noir** | 85 | **159** | **21** | 19 | 37 | 0 | 43 | `72 71 52 56 49 49 48 47` |
| 1 | **Wyrm** | 86 | **162** | **94** | 53 | 84 | 1 | 50 | `78 60 60 64 68 61 61 62` |
| 2 | **Kraaken bras** | 90 | **400** | **500** | 82 | 90 | 1 | 200 | `82 80 89 42 40 62 39 80` |
| 3 | **Kraaken pied** | 82 | **300** | **120** | 83 | 90 | 4 | 100 | `70 60 67 87 20 42 59 52` |
| 4 | **Kraaken** | 120 | **1** | **1** | 84 | 1 | 10 | 10 | `92 90 89 52 40 62 89 77` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -150 | 200 | 400 | 512 | 0x00070b00 |
| 0 | 1 | 0 | 150 | 200 | 400 | 3584 | 0x00070b00 |
| 1 | 0 | 0 | -150 | 200 | -2420 | 1536 | 0x00080b00 |
| 1 | 1 | 0 | 150 | 200 | -2420 | 2560 | 0x00080b00 |
| 2 | 0 | 2 | -190 | -395 | 5000 | 3072 | 0x00090b02 |
| 2 | 1 | 2 | -190 | -395 | 5200 | 3072 | 0x00090b02 |
| 2 | 2 | 3 | -170 | -385 | 5100 | 3072 | 0x00090b03 |
| 2 | 3 | 3 | 70 | -395 | 5100 | 1024 | 0x00090b03 |
| 2 | 4 | 3 | -70 | -395 | 5000 | 2048 | 0x00090b03 |
| 2 | 5 | 3 | -70 | -395 | 5200 | 0 | 0x00090b03 |
| 2 | 6 | 3 | -120 | -395 | 5050 | 2560 | 0x00090b03 |
| 2 | 7 | 3 | -20 | -395 | 5050 | 1536 | 0x00090b03 |
| 2 | 8 | 3 | -120 | -395 | 5150 | 3584 | 0x00090b03 |
| 2 | 9 | 3 | -20 | -395 | 5150 | 512 | 0x00090b03 |
| 2 | 10 | 4 | -70 | -395 | 5100 | 3072 | 0x00090b04 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Chaîne des ténèbres `0` | Fine rapière `1` | Ecu du destin `2` | Bouclier de la terre `3` | Rapière d'argent `4` | Cristal saint `5` | Cristal saint `5` | Cristal saint `5` |
| 1 | Manteau de cuir `6` | Manteau de cuir `6` | Joyau de vent `7` | Joyau de vent `7` | Joyau de vent `7` | Queue de dragon `8` | Ecailles de dragon `9` | Amulette du dragon `10` |
| 2 | Cristal d'eau `11` | Balle de gel `12` | Joyau de glace `13` | Aquachâle `14` | Aquachâle `14` | Balle des éléments `15` | Manteau des esprits `16` | Bouclier des élémentaux `17` |
| 3 | Cristal d'eau `11` | Balle de gel `12` | Joyau de glace `13` | Aquachâle `14` | Aquachâle `14` | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` |
| 4 | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` | Cristal d'eau `11` |

### Zone 9, sous-zone 1 — repertoire de 11688 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 4 | 0x4000 | `00 00 00 00 00 04 00 40` |
| 1 | 1 | 1 | 4 | 0x4000 | `01 01 00 00 01 04 00 40` |
| 2 | 2 | 2 | 4 | 0x4000 | `02 02 00 00 02 04 00 40` |
| 3 | 3 | 3 | 4 | 0x4000 | `03 03 00 00 03 04 00 40` |
| 4 | 4 | 4 | 4 | 0x4000 | `04 04 00 00 04 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Lézard noir** | 85 | **159** | **21** | 19 | 37 | 0 | 43 | `72 71 52 56 49 49 48 47` |
| 1 | **Wyrm** | 86 | **162** | **94** | 53 | 84 | 1 | 50 | `78 60 60 64 68 61 61 62` |
| 2 | **Kraaken bras** | 90 | **400** | **500** | 82 | 90 | 1 | 200 | `92 90 89 52 40 62 89 77` |
| 3 | **Kraaken pied** | 90 | **300** | **120** | 83 | 90 | 4 | 100 | `90 90 89 77 20 62 89 52` |
| 4 | **Kraaken** | 200 | **1** | **1** | 84 | 1 | 10 | 10 | `92 90 89 52 40 62 89 77` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -150 | 200 | 400 | 512 | 0x00070b00 |
| 0 | 1 | 0 | 150 | 200 | 400 | 3584 | 0x00070b00 |
| 1 | 0 | 0 | -150 | 200 | -2420 | 1536 | 0x00080b00 |
| 1 | 1 | 0 | 150 | 200 | -2420 | 2560 | 0x00080b00 |
| 2 | 0 | 2 | -190 | -395 | 5000 | 3072 | 0x00090b02 |
| 2 | 1 | 2 | -190 | -395 | 5200 | 3072 | 0x00090b02 |
| 2 | 2 | 3 | -170 | -395 | 5100 | 3072 | 0x00090b03 |
| 2 | 3 | 3 | 70 | -395 | 5100 | 1024 | 0x00090b03 |
| 2 | 4 | 3 | -70 | -395 | 5000 | 2048 | 0x00090b03 |
| 2 | 5 | 3 | -70 | -395 | 5200 | 0 | 0x00090b03 |
| 2 | 6 | 3 | -120 | -395 | 5050 | 2560 | 0x00090b03 |
| 2 | 7 | 3 | -20 | -395 | 5050 | 1536 | 0x00090b03 |
| 2 | 8 | 3 | -120 | -395 | 5150 | 3584 | 0x00090b03 |
| 2 | 9 | 3 | -20 | -395 | 5150 | 512 | 0x00090b03 |
| 2 | 10 | 4 | -70 | -395 | 5100 | 3072 | 0x00090b04 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Cristal d'eau `0` | Balle de gel `1` | Joyau de glace `2` | Aquachâle `3` | Aquachâle `3` | Balle des éléments `4` | Manteau des esprits `5` | Bouclier des élémentaux `6` |
| 1 | Cristal d'eau `0` | Balle de gel `1` | Joyau de glace `2` | Aquachâle `3` | Aquachâle `3` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` |
| 2 | Forgée de glace `7` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Forgée de glace `7` |
| 3 | Epée de glace `8` | Bouclier de glace `9` | Chaîne de l'eau `10` | Chaîne de l'eau `10` | Bouclier de glace `9` | Epée de glace `8` | Saphir `11` | Saphir `11` |
| 4 | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` | Cristal d'eau `0` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.35 | coffre `14` | 250 | objet | rangee 0 : Cristal d'eau (25 %), Balle de gel (17 %), Joyau de glace (15 %), Aquachâle (13 %), Aquachâle (11 %), Balle des éléments (9 %), Manteau des esprits (6 %), Bouclier des élémentaux (4 %) | 1800 / 240 / -3200 |
| 0.36 | coffre `14` | 251 | objet | rangee 1 : Cristal d'eau (25 %), Balle de gel (17 %), Joyau de glace (15 %), Aquachâle (13 %), Aquachâle (11 %), Cristal d'eau (9 %), Cristal d'eau (6 %), Cristal d'eau (4 %) | 2000 / 240 / 1200 |
| 0.37 | coffre `14` | 252 | objet | rangee 2 : Forgée de glace (25 %), Cristal d'eau (17 %), Cristal d'eau (15 %), Cristal d'eau (13 %), Cristal d'eau (11 %), Cristal d'eau (9 %), Cristal d'eau (6 %), Forgée de glace (4 %) | -1800 / 240 / 1200 |
| 0.38 | coffre `14` | 253 | objet | rangee 3 : Epée de glace (25 %), Bouclier de glace (17 %), Chaîne de l'eau (15 %), Chaîne de l'eau (13 %), Bouclier de glace (11 %), Epée de glace (9 %), Saphir (6 %), Saphir (4 %) | -1800 / 240 / -3200 |

## Zone 10 — 3104 octets, 16 sous-zone(s)

### Zone 10, sous-zone 0 — repertoire de 18008 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0x4000 | `00 00 00 00 00 00 00 40` |
| 1 | 2 | 1 | 1 | 0x4000 | `01 02 00 00 01 01 00 40` |
| 2 | 5 | 2 | 2 | 0x4000 | `02 05 00 00 02 02 00 40` |
| 3 | 3 | 3 | 3 | 0x4000 | `03 03 00 00 03 03 00 40` |
| 4 | 4 | 4 | 4 | 0x4000 | `04 04 00 00 04 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Sale rampant** | 112 | **137** | **80** | 24 | 70 | 3 | 32 | `97 90 88 94 38 94 76 64` |
| 1 | **Ombre rouge** | 102 | **140** | **172** | 39 | 89 | 0 | 44 | `74 190 27 128 40 171 76 127` |
| 2 | **Maudite** | 87 | **109** | **66** | 41 | 8 | 0 | 10 | `62 60 73 82 57 116 72 81` |
| 3 | **Bras gris** | 116 | **202** | **94** | 53 | 84 | 1 | 50 | `90 90 90 88 58 101 64 52` |
| 4 | **Gremlin** | 110 | **156** | **139** | 60 | 70 | 0 | 60 | `70 50 78 78 59 81 81 81` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 2865 | 5 | -5012 | 2970 | 0x001f0b00 |
| 0 | 1 | 0 | 2797 | 4 | -5180 | 2970 | 0x001f0b00 |
| 0 | 2 | 0 | 2639 | 5 | -4910 | 2970 | 0x001f0b00 |
| 0 | 3 | 1 | 3469 | 5 | -5278 | 2970 | 0x001f0b01 |
| 0 | 4 | 1 | 3397 | 5 | -5094 | 2970 | 0x001f0b01 |
| 1 | 0 | 0 | 3519 | 5 | -5320 | 2970 | 0x001f0b00 |
| 1 | 1 | 0 | 3509 | 5 | -5150 | 2970 | 0x001f0b00 |
| 1 | 2 | 2 | 2741 | 5 | -5100 | 2970 | 0x001f0b02 |
| 1 | 3 | 2 | 2887 | 5 | -4974 | 2970 | 0x001f0b02 |
| 1 | 4 | 2 | 2773 | 5 | -5258 | 2970 | 0x001f0b02 |
| 1 | 5 | 2 | 3115 | 5 | -5090 | 2970 | 0x001f0b02 |
| 2 | 0 | 1 | 2747 | 5 | -5078 | 2970 | 0x0000ff01 |
| 2 | 1 | 1 | 2779 | 5 | -5248 | 2970 | 0x0000ff01 |
| 2 | 2 | 1 | 3769 | 5 | -5138 | 2013 | 0x0000ff01 |
| 2 | 3 | 1 | 3235 | 5 | -4974 | 2167 | 0x0000ff01 |
| 3 | 0 | 0 | -455 | 6 | -5291 | 0 | 0x00200b00 |
| 3 | 1 | 0 | -259 | 6 | -5351 | 1980 | 0x00200b00 |
| 3 | 2 | 0 | -55 | 6 | -5331 | 0 | 0x00200b00 |
| 3 | 3 | 1 | 369 | 6 | -5231 | 3333 | 0x00200b01 |
| 3 | 4 | 1 | -867 | 6 | -5119 | 308 | 0x00200b01 |
| 4 | 0 | 0 | 461 | 6 | -5095 | 3564 | 0x00200b00 |
| 4 | 1 | 0 | -899 | 6 | -5111 | 231 | 0x00200b00 |
| 4 | 2 | 2 | 321 | 6 | -5303 | 0 | 0x00200b02 |
| 4 | 3 | 2 | 101 | 6 | -5315 | 0 | 0x00200b02 |
| 4 | 4 | 2 | -295 | 6 | -5323 | 0 | 0x00200b02 |
| 4 | 5 | 2 | -479 | 6 | -5311 | 0 | 0x00200b02 |
| 5 | 0 | 4 | 405 | 6 | -5227 | 3564 | 0x00200b04 |
| 5 | 1 | 4 | -879 | 6 | -5111 | 385 | 0x00200b04 |
| 5 | 2 | 4 | 85 | 6 | -5307 | 0 | 0x00200b04 |
| 5 | 3 | 4 | -311 | 6 | -5311 | 0 | 0x00200b04 |
| 6 | 0 | 2 | -200 | 0 | -3100 | 0 | 0x00210b02 |
| 6 | 1 | 2 | -600 | 0 | -3100 | 0 | 0x00210b02 |
| 6 | 2 | 4 | -400 | 0 | -2100 | 2048 | 0x00210b04 |
| 6 | 3 | 4 | -200 | 0 | -2100 | 2048 | 0x00210b04 |
| 7 | 0 | 3 | 2200 | -560 | 686 | 2475 | 0x00220b03 |
| 7 | 1 | 3 | 2544 | -560 | -506 | 2475 | 0x00220b03 |
| 7 | 2 | 4 | 3020 | -560 | 742 | 1705 | 0x00220b04 |
| 7 | 3 | 4 | 2976 | -560 | 362 | 1705 | 0x00220b04 |
| 7 | 4 | 4 | 2136 | -560 | 1058 | 2475 | 0x00220b04 |
| 8 | 0 | 1 | 2227 | -592 | -2941 | 462 | 0x00230b01 |
| 8 | 1 | 1 | 2515 | -592 | -1973 | 1551 | 0x00230b01 |
| 8 | 2 | 4 | 2155 | -592 | -1981 | 2321 | 0x00230b04 |
| 8 | 3 | 4 | 2963 | -592 | -1405 | 1628 | 0x00230b04 |
| 8 | 4 | 4 | 2411 | -592 | -2701 | 1628 | 0x00230b04 |
| 9 | 0 | 0 | 304 | -584 | 3699 | 1628 | 0x00240b00 |
| 9 | 1 | 0 | 296 | -584 | 3443 | 1628 | 0x00240b00 |
| 9 | 2 | 0 | 512 | -584 | 3803 | 1628 | 0x00240b00 |
| 9 | 3 | 1 | 500 | -1000 | 2900 | 1980 | 0x00240b01 |
| 9 | 4 | 1 | 0 | -1000 | 3100 | 1980 | 0x00240b01 |
| 9 | 5 | 1 | 500 | -600 | 3800 | 0 | 0x00240b01 |
| 10 | 0 | 0 | 152 | -584 | 3587 | 1980 | 0x00240b00 |
| 10 | 1 | 0 | 96 | -584 | 3091 | 1980 | 0x00240b00 |
| 10 | 2 | 2 | 300 | -600 | 2815 | 1980 | 0x00240b02 |
| 10 | 3 | 2 | 500 | -1000 | 2900 | 1980 | 0x00240b02 |
| 10 | 4 | 2 | -283 | -1000 | 3300 | 1980 | 0x00240b02 |
| 10 | 5 | 2 | 200 | -600 | 3600 | 1980 | 0x00240b02 |
| 11 | 0 | 2 | 152 | -584 | 3587 | 1980 | 0x00240b02 |
| 11 | 1 | 2 | 96 | -584 | 3091 | 1980 | 0x00240b02 |
| 11 | 2 | 2 | 300 | -600 | 2815 | 1980 | 0x00240b02 |
| 11 | 3 | 2 | 500 | -1000 | 2900 | 1980 | 0x00240b02 |
| 11 | 4 | 2 | -283 | -1000 | 3300 | 1980 | 0x00240b02 |
| 11 | 5 | 2 | 200 | -600 | 3600 | 1980 | 0x00240b02 |
| 12 | 0 | 1 | -80 | 10 | 72 | 2013 | 0x00250b01 |
| 12 | 1 | 1 | -336 | 10 | 576 | 1089 | 0x00250b01 |
| 12 | 2 | 1 | 16 | 10 | 464 | 1397 | 0x00250b01 |
| 13 | 0 | 1 | -1776 | 10 | 2000 | 2090 | 0x00260b01 |
| 13 | 1 | 1 | -1320 | 10 | 1416 | 1089 | 0x00260b01 |
| 13 | 2 | 1 | -2008 | 10 | 1632 | 2629 | 0x00260b01 |
| 14 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 15 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 15 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 16 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 16 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 17 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 17 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 17 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 17 | 3 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 18 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 18 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 19 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 19 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Fenouil `0` | Robe des anciens `1` | Flèche de l'ombre `2` | Cristal du mal `3` | Cristal du mal `3` | Balle maléfique `4` | Balle maléfique `4` | Balle maléfique `4` |
| 1 | Balle maléfique `4` | Cristal du mal `3` | Chaîne du mal `5` | Robe de l'ombre `6` | Voile des ombres `7` | Voile des ombres `7` | Voile des ombres `7` | Voile des ombres `7` |
| 2 | Cristal du mal `3` | Balle des ténèbres `8` | Cristal du mal `3` | Balle des ténèbres `8` | Balle des ténèbres `8` | Cristal du mal `3` | Balle des ténèbres `8` | Cristal du mal `3` |
| 3 | Gants de cuir `9` | Manteau de brigand `10` | Joyau de vent `11` | Joyau de vent `11` | Joyau de vent `11` | Queue de dragon `12` | Ecailles de dragon `13` | Amulette du dragon `14` |
| 4 | Aile de fée `15` | Belladone `16` | Poudre de fées `17` | Baguette de la féerie `18` | Ecu du destin `19` | Ecu du destin `19` | Ecu du destin `19` | Parfum de feu `20` |

### Zone 10, sous-zone 1 — repertoire de 8724 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 15 | 0 | 0 | 0x4000 | `00 0f 00 00 00 00 00 40` |
| 1 | 3 | 1 | 1 | 0x4000 | `01 03 00 00 01 01 00 40` |
| 2 | 16 | 2 | 2 | 0x4000 | `02 10 00 00 02 02 00 40` |
| 3 | 0 | 3 | 3 | 0x4000 | `03 00 00 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Mort volante** | 120 | **180** | **176** | 51 | 71 | 1 | 40 | `97 71 72 88 70 80 101 90` |
| 1 | **Bras gris** | 116 | **202** | **94** | 53 | 84 | 1 | 50 | `90 90 90 88 58 101 64 52` |
| 2 | **Noir goblin** | 103 | **82** | **16** | 2 | 8 | 3 | 0 | `76 66 73 73 33 54 72 71` |
| 3 | **Shamane** | 103 | **12** | **78** | 2 | 98 | 3 | 0 | `76 66 73 73 83 54 72 71` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 403 | -568 | -5011 | 231 | 0x00070b02 |
| 0 | 1 | 2 | 99 | -568 | -4883 | 231 | 0x00070b02 |
| 0 | 2 | 2 | -101 | -568 | -5011 | 231 | 0x00070b02 |
| 0 | 3 | 2 | -333 | -568 | -5091 | 231 | 0x00070b02 |
| 1 | 0 | 3 | 1896 | -384 | -3979 | 1397 | 0x00080b03 |
| 1 | 1 | 3 | 2296 | -384 | -4115 | 1397 | 0x00080b03 |
| 1 | 2 | 3 | 2248 | -184 | -3739 | 1397 | 0x00080b03 |
| 2 | 0 | 0 | -3850 | -1000 | 5950 | 2048 | 0x00090b00 |
| 2 | 1 | 0 | -3550 | -1000 | 5950 | 2048 | 0x00090b00 |
| 2 | 2 | 0 | -3900 | -1000 | 5700 | 1536 | 0x00090b00 |
| 2 | 3 | 0 | -3500 | -1000 | 5780 | 2560 | 0x00090b00 |
| 3 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 4 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 5 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Elixir des esprits `0` | Ecu du destin `1` | Flûte des moutons `2` | Gants ailés `3` | Manteau à plumes `4` | Chaussures à plumes `5` | Balle du tonnerre `6` | Balle du tonnerre `6` |
| 1 | Gants de cuir `7` | Manteau de brigand `8` | Joyau de vent `9` | Joyau de vent `9` | Joyau de vent `9` | Queue de dragon `10` | Ecailles de dragon `11` | Amulette du dragon `12` |
| 2 | Hache de guerre `13` | Lavande `14` | Arc noble `15` | Ecu du destin `1` | Elixir de vie `16` | Tablier de cuir `17` | Pavois fin `18` | Pavois fin `18` |
| 3 | Simple bâton `19` | Simple bâton `19` | Sauge `20` | Verveine `21` | Mercuriale `22` | Fenouil `23` | Ecu du destin `1` | Balle de cristal `24` |

### Zone 10, sous-zone 2 — repertoire de 12752 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 1 | 0x4000 | `00 00 00 00 00 01 00 40` |
| 1 | 1 | 1 | 2 | 0x4000 | `01 01 00 00 01 02 00 40` |
| 2 | 2 | 2 | 3 | 0x4000 | `02 02 00 00 02 03 00 40` |
| 3 | 3 | 4 | 4 | 0x4000 | `03 03 00 00 04 04 00 40` |
| 4 | 4 | 5 | 5 | 0x4000 | `04 04 00 00 05 05 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Sale rampant** | 112 | **137** | **80** | 24 | 70 | 3 | 32 | `97 90 88 94 38 94 76 64` |
| 1 | **Preux mort** | 121 | **240** | **52** | 38 | 63 | 2 | 50 | `96 92 101 89 60 94 75 72` |
| 2 | **Ombre rouge** | 102 | **140** | **172** | 39 | 89 | 0 | 44 | `74 190 27 128 40 171 76 127` |
| 4 | **Bras gris** | 116 | **202** | **94** | 53 | 84 | 1 | 50 | `90 90 90 88 58 101 64 52` |
| 5 | **Gremlin** | 110 | **156** | **139** | 60 | 70 | 0 | 60 | `70 50 78 78 59 81 81 81` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | -350 | 0 | -2350 | 0 | 0x001d0b01 |
| 0 | 1 | 1 | -650 | 0 | -2350 | 0 | 0x001d0b01 |
| 0 | 2 | 1 | -500 | 0 | -2650 | 0 | 0x001d0b01 |
| 1 | 0 | 1 | -350 | 0 | -2350 | 0 | 0x001d0b01 |
| 1 | 1 | 1 | -650 | 0 | -2350 | 0 | 0x001d0b01 |
| 1 | 2 | 0 | -500 | 0 | -2650 | 0 | 0x001d0b00 |
| 2 | 0 | 0 | -350 | 0 | -2350 | 0 | 0x001d0b00 |
| 2 | 1 | 0 | -650 | 0 | -2350 | 0 | 0x001d0b00 |
| 2 | 2 | 1 | -500 | 0 | -2650 | 0 | 0x001d0b01 |
| 3 | 0 | 1 | 1350 | 0 | -2350 | 0 | 0x001e0b01 |
| 3 | 1 | 1 | 1650 | 0 | -2350 | 0 | 0x001e0b01 |
| 3 | 2 | 1 | 1500 | 0 | -2650 | 0 | 0x001e0b01 |
| 4 | 0 | 1 | 1350 | 0 | -2350 | 0 | 0x001e0b01 |
| 4 | 1 | 1 | 1650 | 0 | -2350 | 0 | 0x001e0b01 |
| 4 | 2 | 2 | 1500 | 0 | -2650 | 0 | 0x001e0b02 |
| 5 | 0 | 0 | 1350 | 0 | -2350 | 0 | 0x001e0b00 |
| 5 | 1 | 0 | 1650 | 0 | -2350 | 0 | 0x001e0b00 |
| 5 | 2 | 1 | 1500 | 0 | -2650 | 0 | 0x001e0b01 |
| 6 | 0 | 0 | 300 | -200 | 3500 | 1536 | 0x001f0b00 |
| 6 | 1 | 0 | 700 | -200 | 3500 | 2560 | 0x001f0b00 |
| 6 | 2 | 0 | 300 | -200 | 3100 | 512 | 0x001f0b00 |
| 6 | 3 | 0 | 700 | -200 | 3100 | 3584 | 0x001f0b00 |
| 7 | 0 | 4 | -1624 | -360 | -965 | 1980 | 0x00200b04 |
| 7 | 1 | 4 | -2704 | -360 | 1971 | 0 | 0x00200b04 |
| 7 | 2 | 4 | -2912 | -360 | 2251 | 0 | 0x00200b04 |
| 7 | 3 | 4 | -1433 | -360 | -725 | 1980 | 0x00200b04 |
| 8 | 0 | 3 | -1576 | -360 | -525 | 1980 | 0x00200b03 |
| 8 | 1 | 3 | -2784 | -360 | 1299 | 0 | 0x00200b03 |
| 8 | 2 | 3 | -1352 | -360 | -189 | 1980 | 0x00200b03 |
| 8 | 3 | 3 | -2952 | -360 | 979 | 0 | 0x00200b03 |
| 9 | 0 | 2 | -1608 | -360 | -453 | 1980 | 0x00200b02 |
| 9 | 1 | 2 | -2832 | -360 | 1547 | 0 | 0x00200b02 |
| 9 | 2 | 2 | -1376 | -360 | -213 | 1980 | 0x00200b02 |
| 9 | 3 | 2 | -2704 | -360 | 1155 | 0 | 0x00200b02 |
| 10 | 0 | 1 | 2500 | -400 | -1100 | 2048 | 0x00210b01 |
| 10 | 1 | 1 | 2700 | -400 | -1100 | 2048 | 0x00210b01 |
| 10 | 2 | 2 | 4500 | -400 | -700 | 2048 | 0x00210b02 |
| 10 | 3 | 2 | 4100 | -400 | -700 | 2048 | 0x00210b02 |
| 11 | 0 | 0 | 2500 | -400 | -1100 | 2048 | 0x00210b00 |
| 11 | 1 | 0 | 2700 | -400 | -1100 | 2048 | 0x00210b00 |
| 11 | 2 | 1 | 4500 | -400 | -700 | 2048 | 0x00210b01 |
| 11 | 3 | 1 | 4100 | -400 | -700 | 2048 | 0x00210b01 |
| 12 | 0 | 4 | 2500 | -400 | -1100 | 2048 | 0x00210b04 |
| 12 | 1 | 4 | 2700 | -400 | -1100 | 2048 | 0x00210b04 |
| 12 | 2 | 4 | 4500 | -400 | -700 | 2048 | 0x00210b04 |
| 12 | 3 | 4 | 4100 | -400 | -700 | 2048 | 0x00210b04 |
| 13 | 0 | 2 | 2700 | -400 | -3100 | 3072 | 0x00220b02 |
| 13 | 1 | 2 | 2700 | -400 | -3500 | 3072 | 0x00220b02 |
| 13 | 2 | 2 | 2700 | -400 | -2300 | 2048 | 0x00220b02 |
| 13 | 3 | 2 | 3900 | -400 | -2300 | 3072 | 0x00220b02 |
| 14 | 0 | 0 | 2700 | -400 | -3100 | 3072 | 0x00220b00 |
| 14 | 1 | 0 | 2700 | -400 | -3500 | 3072 | 0x00220b00 |
| 14 | 2 | 2 | 2700 | -400 | -2300 | 2048 | 0x00220b02 |
| 14 | 3 | 2 | 3900 | -400 | -2300 | 3072 | 0x00220b02 |
| 15 | 0 | 2 | 2700 | -400 | -3100 | 3072 | 0x00220b02 |
| 15 | 1 | 2 | 2700 | -400 | -3500 | 3072 | 0x00220b02 |
| 15 | 2 | 1 | 2700 | -400 | -2300 | 2048 | 0x00220b01 |
| 15 | 3 | 1 | 3900 | -400 | -2300 | 3072 | 0x00220b01 |
| 16 | 0 | 0 | 2700 | -400 | -3100 | 3072 | 0x00220b00 |
| 16 | 1 | 0 | 2700 | -400 | -3500 | 3072 | 0x00220b00 |
| 16 | 2 | 1 | 2700 | -400 | -2300 | 2048 | 0x00220b01 |
| 16 | 3 | 1 | 3900 | -400 | -2300 | 3072 | 0x00220b01 |
| 17 | 0 | 4 | 100 | -300 | -1500 | 0 | 0x00230b04 |
| 17 | 1 | 4 | 900 | -300 | -1500 | 0 | 0x00230b04 |
| 17 | 2 | 4 | 2000 | -300 | 800 | 2048 | 0x00230b04 |
| 17 | 3 | 4 | 500 | -100 | 2000 | 2048 | 0x00230b04 |
| 18 | 0 | 3 | 100 | -300 | -1500 | 0 | 0x00230b03 |
| 18 | 1 | 3 | 900 | -300 | -1500 | 0 | 0x00230b03 |
| 18 | 2 | 4 | 2000 | -300 | 800 | 2048 | 0x00230b04 |
| 18 | 3 | 4 | 500 | -100 | 2000 | 2048 | 0x00230b04 |
| 19 | 0 | 4 | 100 | -300 | -1500 | 0 | 0x00230b04 |
| 19 | 1 | 4 | 900 | -300 | -1500 | 0 | 0x00230b04 |
| 19 | 2 | 4 | 2000 | -300 | 800 | 2048 | 0x00230b04 |
| 19 | 3 | 3 | 500 | -100 | 2000 | 2048 | 0x00230b03 |
| 20 | 0 | 4 | 100 | -300 | -1500 | 0 | 0x00230b04 |
| 20 | 1 | 4 | 900 | -300 | -1500 | 0 | 0x00230b04 |
| 20 | 2 | 4 | 2000 | -300 | 800 | 2048 | 0x00230b04 |
| 20 | 3 | 4 | 500 | -100 | 2000 | 2048 | 0x00230b04 |
| 21 | 0 | 2 | 100 | -300 | -1500 | 0 | 0x00230b02 |
| 21 | 1 | 2 | 900 | -300 | -1500 | 0 | 0x00230b02 |
| 21 | 2 | 2 | 2000 | -300 | 800 | 2048 | 0x00230b02 |
| 21 | 3 | 2 | 500 | -100 | 2000 | 2048 | 0x00230b02 |
| 22 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 22 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 23 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 23 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 24 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 24 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 24 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 25 | 0 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 25 | 1 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |
| 25 | 2 | 4 | 0 | 0 | 0 | 0 | 0x0000ff04 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Robe des runes `0` | Robe médicinale `1` | Cendre rouge `2` | Cendre grise `3` | Cendre blanche `4` | Cendre bleue `5` | Cendre verte `6` | Cendre noire `7` |
| 1 | Fenouil `8` | Robe des anciens `9` | Flèche de l'ombre `10` | Cristal du mal `11` | Cristal du mal `11` | Balle maléfique `12` | Balle maléfique `12` | Balle maléfique `12` |
| 2 | Ecu du destin `13` | Gantelet de force `14` | Rapière du chaos `15` | Armure noire `16` | Armure noire `16` | Symbole des ténébres `17` | Epée noire `18` | Ecu du destin `13` |
| 3 | Balle maléfique `12` | Cristal du mal `11` | Chaîne du mal `19` | Robe de l'ombre `20` | Voile des ombres `21` | Voile des ombres `21` | Voile des ombres `21` | Voile des ombres `21` |
| 4 | Gants de cuir `22` | Manteau de brigand `23` | Joyau de vent `24` | Joyau de vent `24` | Joyau de vent `24` | Queue de dragon `25` | Ecailles de dragon `26` | Amulette du dragon `27` |
| 5 | Aile de fée `28` | Belladone `29` | Poudre de fées `30` | Baguette de la féerie `31` | Ecu du destin `13` | Ecu du destin `13` | Ecu du destin `13` | Parfum de feu `32` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.1 | coffre `34` | 232 | message | — | 900 / -360 / 700 |
| 3.4 | coffre `34` | 267 | objet | rangee 0 : Robe des runes (25 %), Robe médicinale (17 %), Cendre rouge (15 %), Cendre grise (13 %), Cendre blanche (11 %), Cendre bleue (9 %), Cendre verte (6 %), Cendre noire (4 %) | 700 / -360 / -500 |

### Zone 10, sous-zone 3 — repertoire de 9772 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 1 | 0 | 1 | 0x4000 | `00 01 00 00 00 01 00 40` |
| 1 | 2 | 1 | 2 | 0x4000 | `01 02 00 00 01 02 00 40` |
| 2 | 0 | 2 | 0 | 0x4000 | `02 00 00 00 02 00 00 40` |
| 3 | 4 | 3 | 4 | 0x4000 | `03 04 00 00 03 04 00 40` |
| 4 | 3 | 4 | 3 | 0x4000 | `04 03 00 00 04 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Sanpeur** | 121 | **240** | **52** | 38 | 63 | 2 | 50 | `96 92 101 89 60 94 75 72` |
| 1 | **Ombre rouge** | 102 | **140** | **172** | 39 | 89 | 0 | 44 | `74 190 27 128 40 171 76 127` |
| 2 | **Sale rampant** | 112 | **137** | **80** | 24 | 70 | 3 | 32 | `97 90 88 94 38 94 76 64` |
| 3 | **Gremlin** | 110 | **156** | **139** | 60 | 70 | 0 | 60 | `70 50 78 78 59 81 81 81` |
| 4 | **Bras gris** | 116 | **202** | **94** | 53 | 84 | 1 | 50 | `90 90 90 88 58 101 64 52` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -530 | 0 | -3450 | 512 | 0x00260b00 |
| 0 | 1 | 0 | -900 | -100 | -4500 | 0 | 0x00260b00 |
| 0 | 2 | 0 | 300 | -200 | -5300 | 3072 | 0x00260b00 |
| 0 | 3 | 0 | 1100 | -200 | -4300 | 2048 | 0x00260b00 |
| 1 | 0 | 2 | -530 | 0 | -3450 | 512 | 0x00260b02 |
| 1 | 1 | 2 | -900 | -100 | -4500 | 0 | 0x00260b02 |
| 1 | 2 | 2 | 300 | -200 | -5300 | 3072 | 0x00260b02 |
| 1 | 3 | 2 | 1100 | -200 | -4300 | 2048 | 0x00260b02 |
| 2 | 0 | 3 | -435 | 0 | -4185 | 0 | 0x00260b03 |
| 2 | 1 | 3 | -171 | 0 | -3785 | 0 | 0x00260b03 |
| 2 | 2 | 3 | -155 | 0 | -4921 | 0 | 0x00260b03 |
| 2 | 3 | 3 | 493 | 0 | -3585 | 0 | 0x00260b03 |
| 6 | 0 | 3 | 1800 | 100 | 700 | 539 | 0x00280b03 |
| 6 | 1 | 3 | 1800 | 100 | 700 | 3487 | 0x00280b03 |
| 6 | 2 | 4 | 2600 | 100 | 1100 | 1089 | 0x00280b04 |
| 6 | 3 | 4 | 2600 | 100 | 1100 | 1089 | 0x00280b04 |
| 7 | 0 | 3 | -1700 | 100 | 1100 | 539 | 0x00290b03 |
| 7 | 1 | 3 | -1700 | 100 | 700 | 3025 | 0x00290b03 |
| 7 | 2 | 4 | -2500 | 100 | 700 | 539 | 0x00280b04 |
| 7 | 3 | 4 | -2500 | 100 | 1100 | 539 | 0x00280b04 |
| 8 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 9 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 10 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 11 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 11 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 12 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 12 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Fenouil `0` | Robe des anciens `1` | Flèche de l'ombre `2` | Cristal du mal `3` | Cristal du mal `3` | Balle maléfique `4` | Balle maléfique `4` | Balle maléfique `4` |
| 1 | Ecu du destin `5` | Gantelet de force `6` | Rapière du chaos `7` | Armure noire `8` | Armure noire `8` | Symbole des ténébres `9` | Epée noire `10` | Ecu du destin `5` |
| 2 | Balle maléfique `4` | Cristal du mal `3` | Chaîne du mal `11` | Robe de l'ombre `12` | Voile des ombres `13` | Voile des ombres `13` | Voile des ombres `13` | Voile des ombres `13` |
| 3 | Gants de cuir `14` | Manteau de brigand `15` | Joyau de vent `16` | Joyau de vent `16` | Joyau de vent `16` | Queue de dragon `17` | Ecailles de dragon `18` | Amulette du dragon `19` |
| 4 | Aile de fée `20` | Belladone `21` | Poudre de fées `22` | Baguette de la féerie `23` | Ecu du destin `5` | Ecu du destin `5` | Ecu du destin `5` | Parfum de feu `24` |

### Zone 10, sous-zone 4 — repertoire de 23320 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 13 | 0 | 11 | 0x4000 | `00 0d 00 00 00 0b 00 40` |
| 1 | 5 | 1 | 12 | 0x4000 | `01 05 00 00 01 0c 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Dragon zombie** | 180 | **4400** | **840** | 88 | 122 | 0 | 250 | `130 60 82 100 63 51 80 90` |
| 1 | **Maudite** | 157 | **519** | **826** | 41 | 108 | 20 | 200 | `88 240 116 98 87 240 113 100` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -500 | 0 | 200 | 2048 | 0x00040b00 |
| 0 | 1 | 1 | -700 | 100 | 100 | 2048 | 0x0000ff01 |
| 0 | 2 | 1 | -300 | 100 | 100 | 2048 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Excalibur `0` | Excalibur `0` | Excalibur `0` | Excalibur `0` | Excalibur `0` | Excalibur `0` | Excalibur `0` | Excalibur `0` |
| 1 | Stylet de bague `1` | Stylet de bague `1` | Stylet de bague `1` | Stylet de bague `1` | Stylet de bague `1` | Stylet de bague `1` | Stylet de bague `1` | Stylet de bague `1` |
| 2 | Walkyrie `2` | Walkyrie `2` | Walkyrie `2` | Walkyrie `2` | Walkyrie `2` | Walkyrie `2` | Walkyrie `2` | Walkyrie `2` |
| 3 | Bâton divin `3` | Bâton divin `3` | Bâton divin `3` | Bâton divin `3` | Bâton divin `3` | Bâton divin `3` | Bâton divin `3` | Bâton divin `3` |
| 4 | Crosse du jugement `4` | Crosse du jugement `4` | Crosse du jugement `4` | Crosse du jugement `4` | Crosse du jugement `4` | Crosse du jugement `4` | Crosse du jugement `4` | Crosse du jugement `4` |
| 5 | Artémis `5` | Artémis `5` | Artémis `5` | Artémis `5` | Artémis `5` | Artémis `5` | Artémis `5` | Artémis `5` |
| 6 | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` | Hache de la sauvagerie `6` |
| 7 | Baguette stellaire `7` | Baguette stellaire `7` | Baguette stellaire `7` | Baguette stellaire `7` | Baguette stellaire `7` | Baguette stellaire `7` | Baguette stellaire `7` | Baguette stellaire `7` |
| 8 | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` | Cristal d'énergie `8` |
| 9 | Pierre médicinale `9` | Pierre médicinale `9` | Pierre médicinale `9` | Ambrosia `10` | Ambrosia `10` | Ambrosia `10` | Anneau de Merlin `11` | Anneau de Merlin `11` |
| 10 | Pierre magique `12` | Pierre magique `12` | Pierre magique `12` | Panoramix `13` | Panoramix `13` | Panoramix `13` | Bouclier du gardien `14` | Bouclier du gardien `14` |
| 11 | Cristal d'énergie `8` | Balle de l'illusion `15` | Horloge du bonheur `16` | Robe magique `17` | Armure du gardien `18` | Bouclier du gardien `14` | Anneau de Merlin `11` | Anneau des philosophes `19` |
| 12 | Cristal du mal `20` | Balle maléfique `21` | Carte de la peur `22` | Cendre rouge `23` | Cendre rouge `23` | Carte de la peur `22` | Balle maléfique `21` | Cristal du mal `20` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.0 | coffre `34` | 217 | objet | rangee 8 : Cristal d'énergie (25 %), Cristal d'énergie (17 %), Cristal d'énergie (15 %), Cristal d'énergie (13 %), Cristal d'énergie (11 %), Cristal d'énergie (9 %), Cristal d'énergie (6 %), Cristal d'énergie (4 %) | 700 / 40 / 3100 |
| 2.1 | coffre `34` | 218 | objet | rangee 9 : Pierre médicinale (25 %), Pierre médicinale (17 %), Pierre médicinale (15 %), Ambrosia (13 %), Ambrosia (11 %), Ambrosia (9 %), Anneau de Merlin (6 %), Anneau de Merlin (4 %) | 700 / 40 / 2300 |
| 2.2 | coffre `34` | 219 | objet | rangee 10 : Pierre magique (25 %), Pierre magique (17 %), Pierre magique (15 %), Panoramix (13 %), Panoramix (11 %), Panoramix (9 %), Bouclier du gardien (6 %), Bouclier du gardien (4 %) | -1700 / 40 / 3100 |
| 2.3 | coffre `34` | 220 | objet | rangee 8 : Cristal d'énergie (25 %), Cristal d'énergie (17 %), Cristal d'énergie (15 %), Cristal d'énergie (13 %), Cristal d'énergie (11 %), Cristal d'énergie (9 %), Cristal d'énergie (6 %), Cristal d'énergie (4 %) | -1700 / 40 / 2300 |

### Zone 10, sous-zone 5 — repertoire de 12216 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 0 | 1 | 0x4000 | `00 08 00 00 00 01 00 40` |
| 1 | 6 | 1 | 2 | 0x4000 | `01 06 00 00 01 02 00 40` |
| 2 | 12 | 2 | 3 | 0x4000 | `02 0c 00 00 02 03 00 40` |
| 3 | 4 | 3 | 4 | 0x4000 | `03 04 00 00 03 04 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Infernal** | 160 | **320** | **160** | 57 | 63 | 1 | 40 | `129 86 120 82 78 77 82 102` |
| 1 | **Basirisk** | 151 | **207** | **190** | 58 | 103 | 0 | 40 | `116 102 104 102 82 94 98 96` |
| 2 | **Groklebs** | 157 | **178** | **62** | 61 | 81 | 6 | 40 | `123 91 101 98 88 84 90 90` |
| 3 | **Gremlin** | 110 | **156** | **139** | 60 | 70 | 0 | 60 | `70 50 78 78 59 81 81 81` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 1 | 0 | 0 | -2258 | 1632 | -885 | 0 | 0x00260b00 |
| 1 | 1 | 0 | -1982 | 1632 | -891 | 0 | 0x00260b00 |
| 1 | 2 | 2 | -2348 | 2010 | -1263 | 0 | 0x00260b02 |
| 1 | 3 | 2 | -1684 | 2020 | -923 | 0 | 0x00260b02 |
| 1 | 4 | 2 | -2108 | 2010 | -1277 | 0 | 0x00260b02 |
| 2 | 0 | 1 | -1500 | 800 | 2900 | 1024 | 0x00270b01 |
| 2 | 1 | 1 | -1500 | 800 | 2700 | 1024 | 0x00270b01 |
| 2 | 2 | 1 | -1450 | 800 | 2500 | 2048 | 0x00270b01 |
| 3 | 0 | 2 | -1500 | 800 | 2900 | 1024 | 0x00270b02 |
| 3 | 1 | 2 | -1450 | 800 | 2500 | 2048 | 0x00270b02 |
| 3 | 2 | 1 | -1500 | 800 | 2700 | 1024 | 0x00270b01 |
| 4 | 0 | 0 | -1500 | 800 | 2700 | 1024 | 0x00270b00 |
| 4 | 1 | 2 | -1500 | 800 | 2900 | 1024 | 0x00270b02 |
| 4 | 2 | 2 | -1450 | 800 | 2500 | 2048 | 0x00270b02 |
| 5 | 0 | 0 | 1300 | 800 | 2900 | 3072 | 0x00280b00 |
| 5 | 1 | 0 | 1300 | 800 | 2700 | 3072 | 0x00280b00 |
| 5 | 2 | 0 | 1300 | 800 | 2300 | 2048 | 0x00280b00 |
| 6 | 0 | 2 | 1300 | 800 | 2900 | 3072 | 0x00280b02 |
| 6 | 1 | 2 | 1300 | 800 | 2700 | 3072 | 0x00280b02 |
| 6 | 2 | 2 | 1300 | 800 | 2300 | 2048 | 0x00280b02 |
| 7 | 0 | 2 | 1300 | 800 | 2900 | 3072 | 0x00280b02 |
| 7 | 1 | 2 | 1300 | 800 | 2300 | 2048 | 0x00280b02 |
| 7 | 2 | 0 | 1300 | 800 | 2700 | 3072 | 0x00280b00 |
| 8 | 0 | 1 | 1696 | 1623 | -707 | 0 | 0x00290b01 |
| 8 | 1 | 1 | 2108 | 1623 | -695 | 0 | 0x00290b01 |
| 8 | 2 | 1 | 1900 | 1623 | -535 | 0 | 0x00290b01 |
| 8 | 3 | 3 | 1505 | 1536 | -709 | 0 | 0x00290b03 |
| 8 | 4 | 3 | 2308 | 1536 | -707 | 0 | 0x00290b03 |
| 9 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 10 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 11 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 11 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 12 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 12 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 13 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 13 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 13 | 2 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Crosse de voeux `0` | Robe magique `1` | Balle de cristal `2` | Panoramix `3` | Ambrosia `4` | Aile de Séraphin `5` | Aile de Séraphin `5` | Aile de Séraphin `5` |
| 1 | Hache d'Ouga `6` | Potion olympique `7` | Os de guerrier sauvage `8` | Cendre rouge `9` | Cendre verte `10` | Cristal du mal `11` | La massacreuse `12` | La massacreuse `12` |
| 2 | Bouclier de la terre `13` | Ecailles de Belzébuth `14` | Harpe d'or `15` | Cristal de terre `16` | Cristal de terre `16` | Harpe d'or `15` | Ecailles de Belzébuth `14` | Bouclier de la terre `13` |
| 3 | Huile du pouvoir `17` | Lavande `18` | Elixir de vie `19` | Cristal de feu `20` | Chaîne du feu `21` | Chaînette d'argent `22` | Chaînette d'argent `22` | Chaîne du feu `21` |
| 4 | Aile de fée `23` | Belladone `24` | Poudre de fées `25` | Baguette de la féerie `26` | Ecu du destin `27` | Ecu du destin `27` | Ecu du destin `27` | Parfum de feu `28` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 2.3 | coffre `34` | -225 | message | — | -2500 / 1640 / -900 |
| 5.1 | coffre `34` | 239 | objet | rangee 0 : Crosse de voeux (25 %), Robe magique (17 %), Balle de cristal (15 %), Panoramix (13 %), Ambrosia (11 %), Aile de Séraphin (9 %), Aile de Séraphin (6 %), Aile de Séraphin (4 %) | -1900 / 1640 / 1500 |

### Zone 10, sous-zone 6 — repertoire de 3352 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 4 | 0 | 0 | 0x4000 | `00 04 00 00 00 00 00 40` |
| 1 | 13 | 1 | 1 | 0x4000 | `01 0d 00 00 01 01 00 40` |
| 2 | 9 | 2 | 2 | 0x4000 | `02 09 00 00 02 02 00 40` |
| 3 | 9 | 3 | 3 | 0x4000 | `02 09 01 00 03 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Gremlin** | 110 | **156** | **139** | 60 | 70 | 0 | 60 | `70 50 78 78 59 81 81 81` |
| 1 | **Sacuba** | 165 | **190** | **200** | 62 | 120 | 1 | 52 | `102 144 96 99 80 176 124 120` |
| 2 | **Triton glace** | 95 | **190** | **220** | 9 | 82 | 0 | 30 | `88 96 81 84 52 72 73 70` |
| 3 | **Varan glace** | 168 | **240** | **220** | 9 | 82 | 0 | 30 | `158 96 73 74 52 92 90 120` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 3 | -100 | -800 | -500 | 0 | 0x00000b03 |
| 0 | 1 | 0 | -650 | -700 | -300 | 0 | 0x00000b00 |
| 0 | 2 | 0 | -650 | -700 | -700 | 0 | 0x00000b00 |
| 0 | 3 | 0 | 500 | -700 | -300 | 0 | 0x00000b00 |
| 0 | 4 | 0 | 500 | -700 | -700 | 0 | 0x00000b00 |
| 1 | 0 | 2 | -3700 | -900 | 1100 | 0 | 0x00000b02 |
| 1 | 1 | 0 | -3700 | -1300 | -250 | 0 | 0x00000b00 |
| 1 | 2 | 0 | -4500 | -1300 | -250 | 0 | 0x00000b00 |
| 1 | 3 | 3 | -4200 | -1300 | -500 | 0 | 0x00000b03 |
| 1 | 4 | 1 | -3700 | -1300 | -600 | 0 | 0x00000b01 |
| 1 | 5 | 1 | -4500 | -1300 | -600 | 0 | 0x00000b01 |
| 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 2 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 3 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 3 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 3 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 4 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 4 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 5 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 5 | 1 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Aile de fée `0` | Belladone `1` | Poudre de fées `2` | Baguette de la féerie `3` | Ecu du destin `4` | Ecu du destin `4` | Ecu du destin `4` | Parfum de feu `5` |
| 1 | Voile de beauté `6` | Voile mystique `7` | Bracelet de reine `8` | Tiare de la terre `9` | Tiare des étoiles `10` | Couronne de la lune `11` | Artémis `12` | Artémis `12` |
| 2 | Joyau de glace `13` | Balle de gel `14` | Saphir `15` | Forgée de glace `16` | Poignard des brouillards `17` | Poignard des brouillards `17` | Hache du gel `18` | Hache du gel `18` |
| 3 | Cristal d'eau `19` | Chaîne de l'eau `20` | Saphir `15` | Bouclier de glace `21` | Bouclier des élémentaux `22` | Bouclier des élémentaux `22` | Baguette de la licorne `23` | Baguette de la licorne `23` |

### Zone 10, sous-zone 7 — repertoire de 10960 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 9 | 0 | 0 | 0x4000 | `00 09 00 00 00 00 00 40` |
| 1 | 12 | 1 | 1 | 0x4000 | `01 0c 00 00 01 01 00 40` |
| 2 | 8 | 2 | 2 | 0x4000 | `02 08 00 00 02 02 00 40` |
| 3 | 9 | 3 | 0 | 0x4000 | `00 09 01 00 03 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Salamandre** | 115 | **190** | **220** | 8 | 82 | 3 | 30 | `88 106 81 84 92 72 90 89` |
| 1 | **Groklebs** | 157 | **178** | **62** | 61 | 81 | 6 | 40 | `123 91 101 98 88 84 90 90` |
| 2 | **Infernal** | 160 | **320** | **160** | 57 | 63 | 1 | 40 | `129 86 120 82 78 77 82 102` |
| 3 | **Salamandre feu** | 155 | **240** | **280** | 8 | 82 | 3 | 30 | `98 106 81 124 88 82 80 129` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 3 | 4189 | 416 | 5160 | 1936 | 0x00180b03 |
| 0 | 1 | 3 | 3653 | 416 | 5264 | 2167 | 0x00180b03 |
| 1 | 0 | 3 | 4141 | 416 | 5128 | 1936 | 0x00180b03 |
| 1 | 1 | 0 | 4093 | 416 | 4792 | 1936 | 0x00180b00 |
| 1 | 2 | 0 | 3589 | 416 | 5344 | 2321 | 0x00180b00 |
| 2 | 0 | 1 | 3773 | 416 | 5208 | 2321 | 0x00180b01 |
| 2 | 1 | 1 | 4205 | 416 | 5176 | 2321 | 0x00180b01 |
| 3 | 0 | 1 | 3677 | 424 | 5264 | 2244 | 0x00180b01 |
| 3 | 1 | 1 | 4085 | 424 | 5224 | 2244 | 0x00180b01 |
| 3 | 2 | 1 | 4101 | 424 | 4912 | 2013 | 0x00180b01 |
| 4 | 0 | 2 | 4213 | 424 | 5160 | 2013 | 0x00180b02 |
| 4 | 1 | 2 | 3661 | 424 | 5224 | 2013 | 0x00180b02 |
| 5 | 0 | 2 | 2893 | 424 | 5616 | 2013 | 0x00180b02 |
| 5 | 1 | 2 | 3261 | 424 | 5400 | 2013 | 0x00180b02 |
| 5 | 2 | 2 | 3637 | 424 | 5336 | 2013 | 0x00180b02 |
| 6 | 0 | 2 | 4109 | 424 | 4680 | 2013 | 0x00180b02 |
| 6 | 1 | 2 | 3645 | 424 | 5280 | 2013 | 0x00180b02 |
| 6 | 2 | 3 | 4093 | 424 | 5080 | 2013 | 0x00180b03 |
| 7 | 0 | 3 | 4077 | 424 | 2424 | 2013 | 0x00190b03 |
| 7 | 1 | 3 | 4245 | 424 | 2184 | 2013 | 0x00190b03 |
| 8 | 0 | 3 | 4013 | 424 | 2392 | 2013 | 0x00190b03 |
| 8 | 1 | 0 | 4237 | 424 | 2216 | 2013 | 0x00190b00 |
| 8 | 2 | 0 | 3645 | 424 | 1704 | 2013 | 0x00190b00 |
| 9 | 0 | 1 | 4061 | 424 | 2600 | 2013 | 0x00190b01 |
| 9 | 1 | 1 | 4341 | 424 | 2432 | 2013 | 0x00190b01 |
| 10 | 0 | 1 | 4085 | 424 | 2640 | 2013 | 0x00190b01 |
| 10 | 1 | 1 | 4109 | 424 | 3008 | 2013 | 0x00190b01 |
| 10 | 2 | 1 | 3709 | 424 | 1776 | 2013 | 0x00190b01 |
| 11 | 0 | 2 | 3725 | 424 | 2392 | 2013 | 0x00190b02 |
| 11 | 1 | 2 | 4365 | 424 | 2472 | 2013 | 0x00190b02 |
| 12 | 0 | 2 | 4077 | 424 | 3056 | 2013 | 0x00190b02 |
| 12 | 1 | 2 | 4117 | 424 | 2648 | 2013 | 0x00190b02 |
| 12 | 2 | 2 | 3669 | 424 | 1704 | 2013 | 0x00190b02 |
| 13 | 0 | 0 | 3645 | 424 | 1680 | 2013 | 0x00190b00 |
| 13 | 1 | 2 | 3381 | 424 | 1384 | 2013 | 0x00190b02 |
| 13 | 2 | 2 | 3949 | 424 | 2392 | 2013 | 0x00190b02 |
| 14 | 0 | 1 | 284 | 12 | -2043 | 2167 | 0x001a0b01 |
| 14 | 1 | 1 | -844 | 12 | -1783 | 2167 | 0x001a0b01 |
| 15 | 0 | 1 | 624 | 12 | -1903 | 1705 | 0x001a0b01 |
| 15 | 1 | 1 | -308 | 12 | -2127 | 2013 | 0x001a0b01 |
| 15 | 2 | 1 | 0 | 12 | -1815 | 2013 | 0x001a0b01 |
| 16 | 0 | 1 | 620 | 12 | -1859 | 2013 | 0x001a0b01 |
| 16 | 1 | 1 | -520 | 12 | -1931 | 2013 | 0x001a0b01 |
| 16 | 2 | 1 | -908 | 12 | -1799 | 2013 | 0x001a0b01 |
| 16 | 3 | 1 | 0 | 12 | -1815 | 2013 | 0x001a0b01 |
| 17 | 0 | 0 | 616 | 12 | -1891 | 2013 | 0x001a0b00 |
| 17 | 1 | 0 | -344 | 12 | -2163 | 2013 | 0x001a0b00 |
| 18 | 0 | 3 | -880 | 12 | -1811 | 2013 | 0x001a0b03 |
| 18 | 1 | 0 | -276 | 12 | -2099 | 2013 | 0x001a0b00 |
| 18 | 2 | 0 | 672 | 12 | -1903 | 2013 | 0x001a0b00 |
| 19 | 0 | 3 | -1606 | 824 | 4212 | 1705 | 0x001b0b03 |
| 19 | 1 | 0 | -2333 | 890 | 3928 | 1705 | 0x001b0b00 |
| 19 | 2 | 0 | -1817 | 833 | 4211 | 1705 | 0x001b0b00 |
| 19 | 3 | 0 | -1698 | 824 | 3672 | 1705 | 0x001b0b00 |
| 19 | 4 | 0 | -1918 | 824 | 3904 | 1705 | 0x001b0b00 |
| 19 | 5 | 0 | -2290 | 824 | 3696 | 1980 | 0x001b0b00 |
| 20 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 20 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 20 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 21 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 21 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 22 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 22 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 22 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 22 | 3 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 23 | 0 | 3 | 0 | 0 | 0 | 0 | 0x0000ff03 |
| 23 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 23 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Joyau de feu `0` | Balle de feu `1` | Cristal de feu `2` | Franberg `3` | Hache du feu `4` | Cristal de feu `2` | Rubis `5` | Rubis `5` |
| 1 | Huile du pouvoir `6` | Lavande `7` | Elixir de vie `8` | Cristal de feu `2` | Chaîne du feu `9` | Chaînette d'argent `10` | Chaînette d'argent `10` | Chaîne du feu `9` |
| 2 | Hache d'Ouga `11` | Potion olympique `12` | Os de guerrier sauvage `13` | Cendre rouge `14` | Cendre verte `15` | Cristal du mal `16` | La massacreuse `17` | La massacreuse `17` |
| 3 | Balle de lumière `18` | Balle des éléments `19` | Cristal de lumière `20` | Hache du feu `4` | Epée de glace `21` | Cristal de lumière `20` | Rubis `5` | Rubis `5` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.0 | coffre `34` | 229 | message | — | 2100 / 440 / 5500 |

### Zone 10, sous-zone 8 — repertoire de 16100 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 8 | 1 | 1 | 0x4000 | `00 08 00 00 01 01 00 40` |
| 1 | 13 | 2 | 2 | 0x4000 | `01 0d 00 00 02 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 1 | **Infernal** | 160 | **320** | **160** | 57 | 63 | 1 | 40 | `129 86 120 82 78 77 82 102` |
| 2 | **Sacuba** | 165 | **190** | **200** | 62 | 120 | 1 | 52 | `102 144 96 99 80 176 124 120` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 1 | 2500 | 0 | 1500 | 3072 | 0x001f0b01 |
| 0 | 1 | 1 | 2500 | 0 | 1100 | 3072 | 0x001f0b01 |
| 0 | 2 | 0 | 2900 | 0 | 1300 | 3072 | 0x001f0b00 |
| 0 | 3 | 0 | 2900 | 0 | 1700 | 3072 | 0x001f0b00 |
| 0 | 4 | 0 | 2900 | 0 | 900 | 3072 | 0x001f0b00 |
| 1 | 0 | 1 | 300 | 200 | -4500 | 2048 | 0x00200b01 |
| 1 | 1 | 1 | -500 | 200 | -4500 | 2048 | 0x00200b01 |
| 1 | 2 | 0 | 500 | 0 | -4700 | 2048 | 0x00200b00 |
| 1 | 3 | 0 | -700 | 0 | -4700 | 2048 | 0x00200b00 |
| 2 | 0 | 1 | -900 | 200 | -3740 | 1024 | 0x00210b01 |
| 2 | 1 | 1 | -700 | 200 | -3900 | 1024 | 0x00210b01 |
| 2 | 2 | 1 | 500 | 200 | -3900 | 2048 | 0x00210b01 |
| 2 | 3 | 1 | 700 | 200 | -3740 | 2048 | 0x00210b01 |
| 3 | 0 | 0 | -300 | 0 | -3100 | 3072 | 0x00220b00 |
| 3 | 1 | 0 | -500 | 0 | -3500 | 3072 | 0x00220b00 |
| 3 | 2 | 0 | -500 | 0 | -3300 | 3072 | 0x00220b00 |
| 4 | 0 | 0 | 100 | 0 | -3100 | 1024 | 0x00230b00 |
| 4 | 1 | 0 | 300 | 0 | -3300 | 1024 | 0x00230b00 |
| 4 | 2 | 0 | 300 | 0 | -3500 | 1024 | 0x00230b00 |
| 5 | 0 | 0 | -1700 | 0 | -1900 | 0 | 0x00240b00 |
| 5 | 1 | 0 | -900 | 0 | -2700 | 0 | 0x00240b00 |
| 5 | 2 | 1 | -1700 | -200 | -1500 | 1024 | 0x00240b01 |
| 5 | 3 | 1 | -500 | -200 | -1500 | 3072 | 0x00240b01 |
| 6 | 0 | 1 | 100 | 0 | -1500 | 0 | 0x00250b01 |
| 6 | 1 | 1 | 300 | 0 | -1700 | 2048 | 0x00250b01 |
| 6 | 2 | 1 | 700 | 0 | -1500 | 3072 | 0x00250b01 |
| 7 | 0 | 1 | 1700 | 0 | -1500 | 0 | 0x00260b01 |
| 7 | 1 | 1 | 1500 | 0 | -1700 | 2048 | 0x00260b01 |
| 7 | 2 | 1 | 1100 | 0 | -1500 | 3072 | 0x00260b01 |
| 8 | 0 | 0 | 3500 | 0 | -1500 | 0 | 0x00270b00 |
| 8 | 1 | 0 | 3500 | 0 | -2700 | 2048 | 0x00270b00 |
| 9 | 0 | 1 | 3500 | 0 | -500 | 0 | 0x00280b01 |
| 9 | 1 | 1 | 3700 | 0 | -300 | 0 | 0x00280b01 |
| 10 | 0 | 0 | 4100 | 0 | 4700 | 0 | 0x00290b00 |
| 10 | 1 | 0 | 4100 | 0 | 3700 | 2048 | 0x00290b00 |
| 11 | 0 | 0 | 3300 | 200 | 5700 | 1024 | 0x002a0b00 |
| 11 | 1 | 0 | 2500 | 200 | 5700 | 3072 | 0x002a0b00 |
| 12 | 0 | 0 | 1100 | 200 | 4300 | 0 | 0x002b0b00 |
| 12 | 1 | 0 | 300 | 200 | 3900 | 2048 | 0x002b0b00 |
| 13 | 0 | 0 | -2900 | -200 | -100 | 1024 | 0x002c0b00 |
| 13 | 1 | 0 | -4100 | -200 | -100 | 3072 | 0x002c0b00 |
| 14 | 0 | 0 | -3900 | -200 | 3500 | 0 | 0x002d0b00 |
| 14 | 1 | 0 | -3900 | -200 | 1700 | 2048 | 0x002d0b00 |
| 15 | 0 | 1 | -1500 | -300 | -1700 | 1024 | 0x00240b01 |
| 15 | 1 | 1 | -900 | -300 | -1700 | 1024 | 0x00240b01 |
| 16 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 16 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 16 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 17 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 17 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 18 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 18 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 19 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 19 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 19 | 2 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Balle du tonnerre `0` | Bouclier de l'air `1` | Cristal d'air `2` | Chaîne de l'air `3` | Plume dominicale `4` | Plume dominicale `4` | Plume dominicale `4` | Plume dominicale `4` |
| 1 | Hache d'Ouga `11` | Potion olympique `12` | Os de guerrier sauvage `13` | Cendre rouge `14` | Cendre verte `15` | Cristal du mal `16` | La massacreuse `17` | La massacreuse `17` |
| 2 | Voile de beauté `18` | Voile mystique `19` | Bracelet de reine `20` | Tiare de la terre `21` | Tiare des étoiles `22` | Couronne de la lune `23` | Artémis `24` | Artémis `24` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 3.0 | coffre `34` | 270 | message | — | -1500 / -260 / -1700 |
| 5.0 | coffre `34` | 272 | objet | rangee 0 : Balle du tonnerre (25 %), Bouclier de l'air (17 %), Cristal d'air (15 %), Chaîne de l'air (13 %), Plume dominicale (11 %), Plume dominicale (9 %), Plume dominicale (6 %), Plume dominicale (4 %) | -940 / -160 / -520 |
| 8.3 | coffre `34` | 269 | message | — | -4700 / -160 / -100 |
| 8.5 | coffre `34` | 271 | message | — | 300 / 40 / 2700 |

### Zone 10, sous-zone 9 — repertoire de 5528 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 14 | 0 | 0 | 0x4000 | `00 0e 00 00 00 00 00 40` |
| 1 | 12 | 1 | 1 | 0x4000 | `01 0c 00 00 01 01 00 40` |
| 2 | 7 | 2 | 2 | 0x4000 | `02 07 00 00 02 02 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Cerbère** | 200 | **2100** | **666** | 89 | 100 | 0 | 250 | `150 98 119 106 80 106 122 92` |
| 1 | **Groklebs** | 157 | **178** | **62** | 61 | 81 | 6 | 40 | `123 91 101 98 88 84 90 90` |
| 2 | **Gorgon** | 165 | **326** | **225** | 59 | 100 | 0 | 80 | `144 142 81 89 99 112 86 87` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | -200 | 800 | 3850 | 2048 | 0x00060b00 |
| 0 | 1 | 1 | -300 | 800 | 3100 | 0 | 0x0000ff01 |
| 0 | 2 | 1 | -100 | 800 | 3100 | 0 | 0x0000ff01 |
| 0 | 3 | 1 | -300 | 800 | 3700 | 2048 | 0x0000ff01 |
| 0 | 4 | 1 | -100 | 800 | 3700 | 2048 | 0x0000ff01 |
| 1 | 0 | 2 | -500 | 0 | 2400 | 2048 | 0x00050b02 |
| 1 | 1 | 2 | 100 | 0 | 2400 | 2048 | 0x00050b02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Hache du feu `0` | Robe des esprits `1` | Cendre grise `2` | Cendre grise `2` | Bouclier du feu `3` | Bouclier du feu `3` | Dropneal `4` | Fenril `5` |
| 1 | Huile du pouvoir `6` | Lavande `7` | Elixir de vie `8` | Cristal de feu `9` | Chaîne du feu `10` | Chaînette d'argent `11` | Chaînette d'argent `11` | Chaîne du feu `10` |
| 2 | Corne du Minotaure `12` | Corne du diable `13` | Fenouil `14` | Cendre noire `15` | Pierre médicinale `16` | Pierre magique `17` | Bouclier de l'ombre `18` | Chaîne du souffle `19` |

### Zone 10, sous-zone 10 — repertoire de 8012 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 3 | 0 | 1 | 0x4000 | `00 03 00 00 00 01 00 40` |
| 1 | 8 | 1 | 2 | 0x4000 | `01 08 00 00 01 02 00 40` |
| 2 | 13 | 2 | 3 | 0x4000 | `02 0d 00 00 02 03 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Bras gris** | 116 | **202** | **94** | 53 | 84 | 1 | 50 | `90 90 90 88 58 101 64 52` |
| 1 | **Infernal** | 160 | **320** | **160** | 57 | 63 | 1 | 40 | `129 86 120 82 78 77 82 102` |
| 2 | **Sacuba** | 165 | **190** | **200** | 62 | 120 | 1 | 52 | `102 144 96 99 80 176 124 120` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 2 | 3254 | 12 | 1913 | 1859 | 0x00170b02 |
| 0 | 1 | 2 | 2654 | 12 | 1857 | 1859 | 0x00170b02 |
| 0 | 2 | 2 | 2078 | 12 | 1417 | 1859 | 0x00170b02 |
| 0 | 3 | 0 | 3710 | 12 | 1017 | 1859 | 0x00170b00 |
| 0 | 4 | 0 | 1990 | 12 | 577 | 1859 | 0x00170b00 |
| 1 | 0 | 0 | 8 | 248 | 1518 | 2167 | 0x00180b00 |
| 1 | 1 | 0 | -280 | 248 | 2278 | 2321 | 0x00180b00 |
| 1 | 2 | 0 | 0 | 248 | 2182 | 2167 | 0x00180b00 |
| 2 | 0 | 0 | -40 | 248 | 1470 | 2321 | 0x00180b00 |
| 2 | 1 | 0 | 168 | 248 | 2622 | 2321 | 0x00180b00 |
| 2 | 2 | 0 | 200 | 248 | 2038 | 2321 | 0x00180b00 |
| 2 | 3 | 0 | -184 | 248 | 2030 | 2321 | 0x00180b00 |
| 3 | 0 | 0 | 96 | 248 | 2446 | 2321 | 0x00180b00 |
| 3 | 1 | 0 | -368 | 248 | 2558 | 2321 | 0x00180b00 |
| 3 | 2 | 0 | -272 | 248 | 2230 | 2321 | 0x00180b00 |
| 3 | 3 | 0 | 384 | 248 | 2246 | 2321 | 0x00180b00 |
| 3 | 4 | 0 | -8 | 248 | 1710 | 2321 | 0x00180b00 |
| 4 | 0 | 0 | 408 | 248 | 2270 | 2321 | 0x00180b00 |
| 4 | 1 | 0 | 176 | 248 | 1966 | 2321 | 0x00180b00 |
| 4 | 2 | 0 | -40 | 248 | 1198 | 2321 | 0x00180b00 |
| 4 | 3 | 0 | 72 | 248 | 1526 | 2321 | 0x00180b00 |
| 4 | 4 | 0 | 104 | 248 | 2894 | 2321 | 0x00180b00 |
| 4 | 5 | 0 | -256 | 248 | 2662 | 2321 | 0x00180b00 |
| 5 | 0 | 2 | -72 | 248 | 1198 | 2321 | 0x00180b02 |
| 5 | 1 | 2 | 40 | 248 | 1534 | 2321 | 0x00180b02 |
| 5 | 2 | 2 | 104 | 248 | 2438 | 2321 | 0x00180b02 |
| 5 | 3 | 2 | -168 | 248 | 2262 | 2321 | 0x00180b02 |
| 6 | 0 | 2 | 160 | 248 | 2382 | 2321 | 0x00180b02 |
| 6 | 1 | 2 | -128 | 248 | 2198 | 2321 | 0x00180b02 |
| 6 | 2 | 2 | 80 | 248 | 1462 | 2321 | 0x00180b02 |
| 6 | 3 | 2 | -128 | 248 | 1198 | 2321 | 0x00180b02 |
| 7 | 0 | 0 | -2080 | 248 | 1830 | 2013 | 0x00190b00 |
| 7 | 1 | 0 | -1664 | 248 | 2182 | 2013 | 0x00190b00 |
| 7 | 2 | 0 | -1256 | 248 | 2278 | 2013 | 0x00190b00 |
| 8 | 0 | 0 | -2104 | 248 | 1798 | 2013 | 0x00190b00 |
| 8 | 1 | 0 | -1864 | 248 | 2110 | 2013 | 0x00190b00 |
| 8 | 2 | 0 | -1560 | 248 | 2302 | 2013 | 0x00190b00 |
| 8 | 3 | 0 | -1256 | 248 | 2350 | 2013 | 0x00190b00 |
| 9 | 0 | 0 | -2128 | 248 | 1806 | 2013 | 0x00190b00 |
| 9 | 1 | 0 | -1920 | 248 | 2086 | 2013 | 0x00190b00 |
| 9 | 2 | 0 | -1656 | 248 | 2222 | 2013 | 0x00190b00 |
| 9 | 3 | 0 | -1376 | 248 | 2286 | 2013 | 0x00190b00 |
| 9 | 4 | 0 | -1728 | 248 | 1942 | 2013 | 0x00190b00 |
| 10 | 0 | 0 | -2104 | 248 | 1774 | 2013 | 0x00190b00 |
| 10 | 1 | 0 | -2144 | 248 | 2054 | 2013 | 0x00190b00 |
| 10 | 2 | 0 | -1880 | 248 | 2070 | 2013 | 0x00190b00 |
| 10 | 3 | 0 | -672 | 248 | 2390 | 2013 | 0x00190b00 |
| 10 | 4 | 0 | -896 | 248 | 2606 | 2013 | 0x00190b00 |
| 10 | 5 | 0 | -472 | 248 | 2662 | 2013 | 0x00190b00 |
| 11 | 0 | 2 | -2056 | 248 | 1758 | 2013 | 0x00190b02 |
| 11 | 1 | 2 | -1848 | 248 | 2054 | 2013 | 0x00190b02 |
| 11 | 2 | 2 | -1392 | 248 | 2238 | 2013 | 0x00190b02 |
| 11 | 3 | 2 | -1136 | 248 | 2310 | 2013 | 0x00190b02 |
| 12 | 0 | 2 | -1904 | 248 | 1982 | 2013 | 0x00190b02 |
| 12 | 1 | 2 | -2072 | 248 | 1630 | 2013 | 0x00190b02 |
| 12 | 2 | 2 | -880 | 248 | 2302 | 2013 | 0x00190b02 |
| 12 | 3 | 2 | -648 | 248 | 2526 | 2013 | 0x00190b02 |
| 13 | 0 | 0 | -1248 | 0 | 4715 | 0 | 0x001a0b00 |
| 13 | 1 | 0 | -1184 | 0 | 3947 | 0 | 0x001a0b00 |
| 13 | 2 | 0 | 368 | 0 | 3659 | 0 | 0x001a0b00 |
| 14 | 0 | 0 | -1296 | 0 | 4009 | 2629 | 0x001a0b00 |
| 14 | 1 | 0 | -1176 | 0 | 4865 | 2629 | 0x001a0b00 |
| 14 | 2 | 0 | -1184 | 0 | 4553 | 2629 | 0x001a0b00 |
| 14 | 3 | 0 | -1216 | 0 | 3721 | 2629 | 0x001a0b00 |
| 15 | 0 | 0 | 992 | 0 | 3513 | 2629 | 0x001a0b00 |
| 15 | 1 | 0 | 1280 | 0 | 3329 | 2629 | 0x001a0b00 |
| 15 | 2 | 0 | -1200 | 0 | 4033 | 2629 | 0x001a0b00 |
| 15 | 3 | 0 | -1288 | 0 | 3777 | 2629 | 0x001a0b00 |
| 15 | 4 | 0 | -1032 | 0 | 3817 | 2629 | 0x001a0b00 |
| 16 | 0 | 0 | -720 | 0 | 3881 | 3025 | 0x001a0b00 |
| 16 | 1 | 0 | -1000 | 0 | 3881 | 2860 | 0x001a0b00 |
| 16 | 2 | 0 | 104 | 0 | 3897 | 2860 | 0x001a0b00 |
| 16 | 3 | 0 | -1072 | 0 | 4681 | 2860 | 0x001a0b00 |
| 16 | 4 | 0 | -1224 | 0 | 4889 | 2860 | 0x001a0b00 |
| 16 | 5 | 0 | -1240 | 0 | 4505 | 2860 | 0x001a0b00 |
| 17 | 0 | 2 | -768 | 0 | 3913 | 2860 | 0x001a0b02 |
| 17 | 1 | 2 | -1184 | 0 | 3721 | 2860 | 0x001a0b02 |
| 17 | 2 | 2 | -1208 | 0 | 4073 | 2860 | 0x001a0b02 |
| 17 | 3 | 2 | -1208 | 0 | 4593 | 2860 | 0x001a0b02 |
| 18 | 0 | 2 | -992 | 0 | 3873 | 2860 | 0x001a0b02 |
| 18 | 1 | 2 | -1376 | 0 | 3889 | 2860 | 0x001a0b02 |
| 18 | 2 | 2 | -1024 | 0 | 4673 | 2860 | 0x001a0b02 |
| 18 | 3 | 2 | -1400 | 0 | 4729 | 2860 | 0x001a0b02 |
| 19 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 19 | 1 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 19 | 2 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 20 | 0 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 20 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 21 | 0 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 21 | 1 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |
| 22 | 0 | 0 | 0 | 0 | 0 | 0 | 0x0000ff00 |
| 22 | 1 | 1 | 0 | 0 | 0 | 0 | 0x0000ff01 |
| 22 | 2 | 2 | 0 | 0 | 0 | 0 | 0x0000ff02 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Balle du tonnerre `0` | Bouclier de l'air `1` | Cristal d'air `2` | Chaîne de l'air `3` | Plume dominicale `4` | Plume dominicale `4` | Plume dominicale `4` | Plume dominicale `4` |
| 1 | Gants de cuir `5` | Manteau de brigand `6` | Joyau de vent `7` | Joyau de vent `7` | Joyau de vent `7` | Queue de dragon `8` | Ecailles de dragon `9` | Amulette du dragon `10` |
| 2 | Hache d'Ouga `11` | Potion olympique `12` | Os de guerrier sauvage `13` | Cendre rouge `14` | Cendre verte `15` | Cristal du mal `16` | La massacreuse `17` | La massacreuse `17` |
| 3 | Voile de beauté `18` | Voile mystique `19` | Bracelet de reine `20` | Tiare de la terre `21` | Tiare des étoiles `22` | Couronne de la lune `23` | Artémis `24` | Artémis `24` |

**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire dans la meme table `+0xb4` que le butin des monstres, a une rangee que porte l'enregistrement de placement.

| coffre | gestionnaire | id | role | contenu | x / y / z |
|---|---:|---:|---|---|---|
| 0.28 | coffre `34` | 226 | objet | rangee 0 : Balle du tonnerre (25 %), Bouclier de l'air (17 %), Cristal d'air (15 %), Chaîne de l'air (13 %), Plume dominicale (11 %), Plume dominicale (9 %), Plume dominicale (6 %), Plume dominicale (4 %) | 3100 / -60 / 100 |

### Zone 10, sous-zone 11 — repertoire de 2468 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 17 | 0 | 0 | 0x4000 | `00 11 00 00 00 00 00 40` |
| 1 | 31 | 0 | 0 | 0x4000 | `00 1f 00 00 00 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Eifridt** | 212 | **2400** | **807** | 66 | 76 | 0 | 100 | `160 152 117 108 111 133 128 105` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0x00050b00 |
| 1 | 0 | 1 | 4100 | 200 | 500 | 2048 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Cristal de feu `0` | Balle des éléments `1` | Bouclier des élémentaux `2` | Revatin `3` | Hache du diable `4` | Marteau de Thor `5` | Armure de Pawas `6` | Armure de Pawas `6` |

### Zone 10, sous-zone 12 — repertoire de 2524 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 19 | 0 | 0 | 0x4000 | `00 13 00 00 00 00 00 40` |
| 1 | 31 | 0 | 0 | 0x4000 | `00 1f 00 00 00 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Le gros poilu** | 218 | **2300** | **1320** | 93 | 80 | 0 | 100 | `152 115 121 121 122 166 115 114` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 111 | 600 | -464 | 0 | 0x00050b00 |
| 1 | 0 | 1 | 4100 | 200 | 500 | 2048 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Corne du diable `0` | Balle maléfique `1` | Boîte de Pandore `2` | Cristal du mal `3` | Cristal du mal `3` | Aile de Lucifer `4` | Aile de Lucifer `4` | Anneau de Satan `5` |

### Zone 10, sous-zone 13 — repertoire de 2604 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 25 | 0 | 0 | 0x4000 | `00 19 00 00 00 00 00 40` |
| 1 | 31 | 0 | 0 | 0x4000 | `00 1f 00 00 00 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Lordémon** | 235 | **2763** | **666** | 98 | 300 | 0 | 200 | `101 152 109 132 111 155 111 119` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 0 | 200 | 2048 | 0x00050b00 |
| 1 | 0 | 1 | 4100 | 200 | 500 | 2048 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Gantelet de force `0` | Robe du démon `1` | Anneau de l'hermite `2` | Anneau du roi Salomon `3` | Masque de la mort `4` | Masque de la mort `4` | Bouclier de Bartschas `5` | Bouclier de Bartschas `5` |

### Zone 10, sous-zone 14 — repertoire de 2600 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 26 | 0 | 0 | 0x4000 | `00 1a 00 00 00 00 00 40` |
| 1 | 31 | 0 | 0 | 0x4000 | `00 1f 00 00 00 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **L'Ange noir** | 238 | **2391** | **1400** | 99 | 1600 | 0 | 200 | `170 174 112 122 90 190 121 122` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 111 | 600 | -464 | 0 | 0x00050b00 |
| 1 | 0 | 1 | 4100 | 200 | 500 | 2048 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Plume dominicale `0` | Faux de la mort `1` | Robe de fantôme `2` | Aile de Lucifer `3` | Aile de Séraphin `4` | Bouclier de Lucifer `5` | Bouclier de Séraphin `6` | Robe de Saulus `7` |

### Zone 10, sous-zone 15 — repertoire de 13208 octets

**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.

| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |
|---:|---:|---:|---:|---:|---|
| 0 | 27 | 0 | 0 | 0x4000 | `00 1b 00 00 00 00 00 40` |
| 1 | 30 | 0 | 0 | 0x4000 | `00 1e 00 00 00 00 00 40` |

**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.

| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | **Belzébuth** | 320 | **3000** | **1600** | 100 | 80 | 0 | 0 | `196 158 128 112 122 220 119 138` |

**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.

| formation | rang | espece | x | y | z | orientation | `kind` |
|---:|---:|---:|---:|---:|---:|---:|---|
| 0 | 0 | 0 | 0 | 10 | -50 | 0 | 0x00110b00 |
| 1 | 0 | 1 | 4100 | 200 | 500 | 2048 | 0x0000ff01 |

**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue de la sous-zone.

| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---:|---|---|---|---|---|---|---|---|
| *chance, plage 100* | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |
| 0 | Plume dominicale `0` | Plume dominicale `0` | Faux de la mort `1` | Faux de la mort `1` | Faux de la mort `1` | Robe de fantôme `2` | Robe de fantôme `2` | Robe de fantôme `2` |

