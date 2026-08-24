# Bugs du jeu d'origine

Catalogue des défauts **présents dans `Blaze.exe` de 1998**, constatés en jouant au jeu officiel et
recoupés dans le code décompilé.

Il existe parce que la distinction compte : une bizarrerie observée dans OpenForesia peut être une
transcription **fidèle** d'un défaut d'origine plutôt qu'une erreur de reconstruction. Avant de
« corriger » quoi que ce soit, vérifier ici.

**Règle du projet** : ces bugs sont reproduits tels quels. La fidélité prime, et un correctif serait
une déviation à déclarer explicitement (voir `Strategy.md`). Chaque entrée note tout de même où
porterait le correctif, si l'envie venait un jour d'un mode « corrigé ».

---

## LB-001 — Sortir de la saisie du nom par Échap bloque l'écran de sélection

**Constaté** le 2026-08-15 par Vincent sur le jeu officiel, puis reproduit à l'identique dans
OpenForesia.

### Symptôme

Sélection des personnages → on valide un personnage → l'écran de saisie du nom s'ouvre → on appuie
sur **Échap** pour revenir au carrousel. Le carrousel revient bien à l'écran, mais **plus aucune
touche ne répond** : on ne peut ni changer de personnage, ni valider, ni même quitter l'écran. Il
faut tuer le jeu.

### Reproduction

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Then ENTER,WAIT3,ESC,WAIT3,ENTER,WAIT2,ENTER -Seconds 10 -Shot
```

Le débit retombe au régime du carrousel (~385 primitives par image) et les deux `ENTER` qui suivent
l'`ESC` ne produisent rien — l'écran est vivant mais sourd.

### Cause

Un drapeau désarmé sur un chemin et jamais réarmé sur l'autre.

`DAT_00564a50` commande la traduction **clavier → manette**. `FUN_00523660` le lit à chaque image
(`Blaze.c:70725`) et, quand il vaut 0, **efface tout l'état clavier DirectInput** avant de le plier
en état de manette :

```c
if (DAT_00564a50 == 0) {
    /* les 0xFC premiers octets de l'état DirectInput sont mis à zéro */
}
```

Ce drapeau n'est écrit que par deux fonctions d'une ligne chacune, et chacune n'a **qu'un seul site
d'appel dans tout le binaire** :

| fonction | effet | appelée depuis |
|---|---|---|
| `FUN_00523530` | `DAT_00564a50 = 0` | `FUN_00507cb0:30` — première image de la saisie du nom |
| `FUN_00523520` | `DAT_00564a50 = 1` | `FUN_00507cb0:176` — **uniquement** le chemin « Entrée » |

L'écran de saisie a quatre sorties, et une seule réarme :

| sortie | réarme `DAT_00564a50` ? |
|---|---|
| **Entrée** (touche clavier `0x0d`) | oui — `EnableKBInput(0)` puis `FUN_00523520()` |
| **Échap** (touche clavier `0x1b`) | **non** — `LAB_005081ec` puis `EnableKBInput(0)` seul |
| **Annulation manette** (tampon vide, curseur sur `FIN`) | **non** — saute à `LAB_005081ec` |
| **Validation manette** (curseur sur `FIN`) | **non** — tombe sur le même `EnableKBInput(0)` seul |

Après Échap, on revient donc au sous-état 0 avec la traduction clavier→manette éteinte pour de bon :
`FUN_00507010` (le carrousel) ne la rallume pas, ni aucun autre sous-état, ni la boucle de frame.
Le carrousel lit une manette qui reste à zéro à chaque image, y compris pour la combinaison de
sortie de l'écran — d'où le blocage complet.

### Portée

- Le blocage frappe le **joueur au clavier**. La remise à zéro ne couvre que les 0xFC premiers
  octets de l'état DirectInput, donc la zone manette à `+0x100` survit : avec une vraie manette
  branchée, l'écran reste jouable.
- La **validation à la manette** souffre du même oubli. Elle mène au sous-état 3, qui devient
  sourd au clavier de la même façon. Valider avec la touche Entrée est le seul chemin qui laisse
  l'écran dans un état sain — ce qui explique qu'on ne rencontre pas le bug en jouant au clavier
  « normalement ».

### Dans OpenForesia

Reproduit fidèlement : `HandleTypedNameEntry` (chemin `0x1b`) et `HandlePadNameEntry` (chemin
d'annulation) rendent tous deux `true`, ce que l'appelant traduit par `EnableKBInput(0)` seul, sans
`EnablePadKeyboard()`. C'est la transcription exacte du `goto LAB_005081ec` de l'original.

**Où porterait un correctif** : un appel à `EnablePadKeyboard()` (`FUN_00523520`) à côté du
`EnableKBInput(0)` de `CharacterSelectSubState1`, ce qui couvrirait les trois sorties d'un coup.
Une ligne. Non appliqué — ce serait une déviation par rapport à 1998.

---

## LB-002 — Un code dérivé du personnage sert de seul identifiant de sauvegarde

**Repéré** le 2026-08-16 en lisant `FUN_0050a910` (la logique de phase de l'écran de sauvegarde)
pendant la reconstruction du cas 6 (écriture). **Pas encore constaté sur le jeu officiel** — à
vérifier avec un jeu réel avant de le considérer confirmé, contrairement à LB-001 ci-dessus.

**Révisé le 2026-08-18** : la première rédaction disait « le nom tapé sert de nom de fichier ».
C'est faux, et l'erreur venait d'un défaut de portage (gabarit de nom vide) depuis corrigé. Le nom
de fichier réel est `Blaze-Saving` suivi de **sept caractères de code** — le nom tapé n'y entre que
par le hachage décrit ci-dessous. Constaté en jeu : `save\Blaze-Saving@MJAXY_`.

### Symptôme (attendu, à confirmer)

Créer un personnage dont le **code** coïncide avec celui d'un personnage déjà sauvegardé ne crée pas
un second fichier. L'écran de sauvegarde/chargement bascule à la place sur le chemin **chargement**
de l'ancienne sauvegarde, au lieu d'écrire les données du nouveau personnage.

### Cause

La finalisation de la création (`FUN_005098f0`, branche « confirmer ») calcule un code à sept
caractères par un repliement de type somme de contrôle (`hash = (hash + valeur) * 2`, puis cinq bits
à la fois dans la plage `'@'..'_'`) sur :

- les seize octets du **nom tapé** (`DAT_00573338+0x100`) ;
- les **sept statistiques** réparties (`DAT_00573338+0x280`) ;
- la **classe**, l'**élément**, le **style** et le quatrième champ d'identité (`+0x150..0x153`).

Ce code, préfixé de `Blaze-Saving`, **est** le nom de fichier. `case 1` de `FUN_0050a910` (transcrit
en C# dans `Blaze.cs`, voir « La sous-couche carte mémoire ») teste ensuite l'existence du fichier,
sans autre identifiant (ni numéro de slot ni port distinct) :

```
ouvre "save\Blaze-Saving<code>" en lecture
  → échec (fichier absent)  : crée un fichier vide, phase 6 (écriture — nouvelle sauvegarde)
  → succès (fichier existe) : phase 0xe (chargement de la sauvegarde existante)
```

Aucune vérification d'unicité au-delà de ce test d'existence.

### Portée

Plus étroite que ce que la première rédaction laissait croire : une collision demande **tout** à
l'identique — même nom, même classe, même élément, même style et exactement la même répartition de
points. Deux personnages qui diffèrent par un seul de ces éléments produisent deux fichiers séparés
dans `save\`. À l'inverse, le repliement `*2` n'est pas injectif : rien ne garantit qu'il n'existe
pas de collision entre deux personnages *différents*, elle est simplement improbable.

### Dans OpenForesia

Transcrit fidèlement (`RetryOpenCardFile(s_CardPathDescriptor_00604940, 1)` puis branchement sur le
handle, `Blaze.cs`, `case 1:` de `StepSubState7Phase`) — comportement hérité tel quel, pas introduit
par le portage.

**Où porterait un correctif** : ajouter un identifiant supplémentaire (numéro de slot/port, horodatage,
etc.) à la construction du chemin dans `SetCardFileName`/`SetCardPathDeviceFields`, ou détecter la
collision et proposer un choix au joueur avant d'écraser/charger. Non appliqué — décision reportée
par Vincent le 2026-08-16 (« on verra ça plus tard »).

**Ce que fait réellement le chemin « fichier déjà présent »** (transcrit le 2026-08-18) : les phases
0xe et 0xf affichent chacune un message, puis la phase 0x10 **supprime** la sauvegarde existante
(`Ordinal_20`, `DeleteFileA`) et remet la phase à 0. Le cas 1 ne trouve alors plus de fichier et
l'écriture normale reprend. Autrement dit, resauvegarder un personnage identique **écrase** sa
sauvegarde — ce n'est pas un blocage, et le symptôme décrit plus haut ne se manifeste que si les
boîtes de dialogue modales (absentes de ce port) laissent le joueur répondre autrement.

Un défaut du **portage**, distinct de celui-ci, masquait ce parcours jusqu'au 2026-08-18 : le même
`case 1` plantait en `0xC0000005` dans `ResetCardTransferStatus`/`Ordinal_67`, parce que les huit
gestionnaires que `SetupJoystickHandlers` enregistre auprès de winsys3d.dll l'étaient avec un
pointeur `null`. Corrigé, voir `REVERSE_STATUS.md` et `Documentation/DLL/winsys3d.md`.

---

## LB-003 — « Invertaire » : la faute de frappe du menu objets

**Repérée** le 2026-08-18 en reconstruisant l'écran racine du menu objets (`FUN_00524680`).
**Confirmée dans le binaire**, pas seulement à l'écran.

### Symptôme

La première ligne du menu objets s'intitule **« Invertaire »** au lieu d'« Inventaire ».

### Cause

Ce n'est pas une erreur de relevé ni un défaut d'affichage : la chaîne est écrite ainsi dans les
données du jeu. `Legacy/Binaries/Blaze.exe`, adresse virtuelle `0x00566B74` :

```
49 6e 76 65 72 74 61 69 72 65 00     I  n  v  e  r  t  a  i  r  e
```

Les trois libellés de cet écran sont pointés par `PTR_s_Invertaire_00566b58` — nom que Ghidra a
lui-même dérivé de la chaîne, ce qui la confirme une seconde fois. Ses voisines, « Enchère » et
« Quitter », sont correctes.

### Portée

Purement cosmétique, une seule chaîne, un seul écran.

### Dans OpenForesia

Transcrite telle quelle dans `s_InventoryLabels_00566b58`. **Ne pas la corriger** : le projet
reproduit les défauts d'origine plutôt que de les réparer, et une « correction » ici ferait diverger
l'affichage du jeu de 1998 sans aucun gain.

---

## LB-004 — La fiche d'objet et le renommage sont injoignables

**Repérée** le 2026-08-18 en transcrivant `FUN_00524880`, le sous-écran « Invertaire ».
**Confirmée dans le binaire**, à l'octet.

### Symptôme

Le sous-écran « Invertaire » ne sait rien faire d'autre que **lister** les objets du personnage
choisi. Ni la fiche détaillée (icône, quantité, huit caractéristiques, deux valeurs longues, huit
témoins d'élément) ni le clavier de renommage d'un objet ne peuvent être atteints, alors que les
deux sont entièrement codés dans le binaire.

### Cause

La machine à phases de `FUN_00524880` est pilotée par `DAT_005de4d4`. La fiche est la phase 9, le
clavier la phase 10. Or :

* dans la fonction, le seul chemin vers 9 est le décrément de la phase 10, et le seul chemin vers
  10 est l'incrément de la phase 9 — **la paire est fermée sur elle-même** ;
* la phase 8 (la liste), qui devrait ouvrir la fiche, sort vers l'écran racine aussi bien sur
  CARRÉ que sur CROIX ; seul le son diffère (`FUN_0053db80` contre `FUN_0053dba0`).

Vérifié dans `Legacy/Binaries/Blaze.exe` en balayant les 46 instructions qui référencent `0x005de4d4` : les
seuls immédiats écrits sont `0`, `5`, `8`, `0xb`, `0xd`, `0xe`, `0x5e`, `0x60`, `0x62`, `-1` et
`-5`. **Aucun site n'écrit 9 ni 10.** Les écritures depuis un registre se trouvent dans
`FUN_00524680` (l'écran racine) et `FUN_00527e30` (« Utilisation »), et la pseudo-C montre qu'elles
posent zéro.

### Portée

Par ricochet, toute la boucle de réécriture du fichier de sauvegarde (phases `7`, `0xb`..`0x12`,
`0x5e`..`0x62` et les négatives) est elle aussi morte : elle n'est empruntée que si
`DAT_005de508` a été levé, ce que seul le renommage fait.

### Dans OpenForesia

Les phases 9, 10 et toute la boucle de sauvegarde sont **transcrites quand même** — le projet
reconstruit le binaire, pas seulement ses chemins vivants — avec la note en tête de la phase 9. Ne
pas « réparer » la sortie de la phase 8 pour les rendre accessibles : cela ferait diverger le port
du jeu de 1998.

---

## LB-005 — Le refus de premier caractère du renommage ne peut pas se déclencher

**Repérée** le 2026-08-18, dans la phase 10 de `FUN_00524880`. **Confirmée au désassemblage.**

### Symptôme

Le clavier de renommage est censé refuser un certain glyphe en première position, avec le bip
`FUN_0053dbc0`. La comparaison ne peut jamais être vraie.

### Cause

`Legacy/Binaries/Blaze.exe`, `0x005266A7` et suivants :

```
005266A7   8A 18               MOV   BL,[EAX]          ; le glyphe de la case visée
005266A9   81 E3 FF FF 00 00   AND   EBX,0x0000FFFF
005266AF   85 F6               TEST  ESI,ESI           ; la longueur déjà saisie
005266B1   75 17               JNZ   ...
005266B3   8B 15 9C 2C 56 00   MOV   EDX,[0x00562C9C]
005266B9   81 E2 FF FF 00 00   AND   EDX,0x0000FFFF
005266BF   3B DA               CMP   EBX,EDX
```

`EBX` est mis à zéro juste avant (`XOR EBX,EBX` à `0x0052669E`) puis ne reçoit qu'un **octet** :
il vaut donc 0..255. `[0x00562C9C]` vaut `0x00004081`, soit `0x4081` après masque. L'égalité est
impossible. Le code compare un glyphe sur un octet à une valeur sur deux.

### Portée

Nulle en pratique : la phase 10 est elle-même injoignable (LB-004).

### Dans OpenForesia

Transcrite telle quelle, avec `s_Unknown_00562c9c = 0x00004081`. La branche est présente et morte,
comme dans l'original.

---

## LB-006 — L'installation des tables de sauts de zone déborde du double

**Repérée** le 2026-08-18 en ouvrant l'état de jeu 3 (la carte du monde).
**Confirmée dans le binaire**, sur les deux tables.

### Symptôme

`FUN_0052a660`, branche `DAT_00604930`, installe la table de sauts de la zone courante. Elle copie
**0x20 dwords** (128 octets) alors que la table de destination n'en compte que **0x10** (64 octets).
La copie déborde donc des deux côtés à la fois : en lecture comme en écriture.

### Cause

Trois relevés concordants dans `Legacy/Binaries/Blaze.exe` :

1. **La destination fait seize entrées.** À `0x0056b4d0`, les seize premiers dwords valent
   `0x00401FA0` (une fonction au corps vide) et les seize suivants valent **zéro**. La frontière est
   nette.
2. **Les tables sources sont espacées de 0x40.** `PTR_PTR_00566d68` liste onze zones :
   `0x55e8a8`, `0x55e8e8`, `0x55e928` … `0x55eb28`, puis un dword nul. Soit 0x40 d'écart, donc
   seize dwords chacune.
3. **La boucle en copie trente-deux.** `for (iVar1 = 0x20; iVar1 != 0; iVar1--)`.

Conséquences mesurées :

* **En lecture**, la seconde moitié de chaque zone est en réalité la première moitié de la zone
  suivante. La zone 0 le montre : son entrée 24 vaut `0x0040A600`, qui est l'entrée 8 de la zone 1.
  Pour la zone 10, la dernière, la copie va chercher seize fonctions (`0x5037D0`…`0x503E80`) qui ne
  font partie d'aucune table.
* **En écriture**, seize dwords atterrissent en `0x0056b510..0x0056b54f`.

### Portée

Faible en pratique. La zone écrasée (`0x0056b510`) n'est référencée nulle part dans `Blaze.c` — ce
sont 64 octets de données inutilisées, mises à zéro au chargement. Et les entrées 16 à 31 de la
table de destination ne sont jamais appelées : sur les onze zones, seules trois entrées ne sont pas
le corps vide `FUN_00401FA0`, et la seule que le code atteigne est l'entrée 8.

### Dans OpenForesia

`InstallAreaHandlerTable` copie bien 0x20 dwords, et `s_AreaHandlerTable_0056b4d0` alloue
trente-deux dwords pour que le débordement ait où tomber, exactement comme dans l'original. **Ne pas
« corriger » le compte à 0x10** : cela ferait diverger le contenu des entrées 16 à 31, et surtout
cela masquerait le défaut au lieu de le reproduire.


---

## LB-007 — Le coffre déréférence un pointeur nul là où il vient justement de se garder d'un nul

**Repéré le 2026-08-22**, en transcrivant `FUN_00450D30` (l'entrée 18 de `PTR_FUN_0056adc8`, le
gestionnaire de coffre). La fonction est **absente de `Blaze.c`** — Ghidra ne décompile pas un corps
dont seule l'adresse est prise — donc le défaut ne se voit qu'au désassemblage.

### Ce que fait le binaire

Dans l'état `0x28`, le coffre a besoin de l'entité qui l'a ouvert. Elle vit dans son champ `+0x6c`,
et le code **prévoit explicitement** qu'elle soit nulle :

```
00450fae  mov  ebx,[esi+6Ch]        ; ebx = l'entite liee
00450fb1  mov  eax,ebx
00450fb3  cmp  eax,edi              ; edi = 0
00450fb5  jne  00450FBC
00450fb7  mov  eax,5DE558h          ; nulle -> on retombe sur s_EntityList_005de558
LAB_00450fbc:
00450fbc  ...                       ; la suite lit [eax+8], donc le repli est bien utilise
```

Le repli n'est appliqué qu'à **`EAX`**. `EBX` garde la valeur nulle d'origine. Une trentaine
d'instructions plus loin, après la boucle qui balaie l'inventaire :

```
00451009  mov  dword [ebx+6Ch],0    ; ecriture a l'adresse 0x6c si ebx etait nul
```

Puis, immédiatement après, deux relectures de `[esi+6Ch]` sans repli du tout :

```
00451010  mov  ecx,[esi+6Ch]
0045101b  mov  [ecx+41h],dl
```

### Pourquoi c'est un défaut et pas une lecture erronée de notre part

Les deux moitiés se contredisent dans la même fonction : le repli sur `s_EntityList_005de558`
n'existe que parce que l'auteur savait que `+0x6c` peut être nul à ce point. S'il ne pouvait pas
l'être, le repli serait mort. L'écriture par `EBX` — et les deux déréférencements qui suivent —
ignorent la précaution que la ligne d'avant vient de prendre.

### Portée

Nulle en pratique dans le jeu de 1998, pour autant qu'on puisse en juger : l'état `0x28` n'est
atteint qu'après que le coffre a été touché par un membre du groupe, et c'est ce contact qui écrit
`+0x6c`. Le chemin où `+0x6c` reste nul jusqu'à l'état `0x28` n'a pas été exhibé. Le défaut est donc
**latent**, pas observé.

### Dans OpenForesia

`EntityHandler_00450D30` transcrit les deux moitiés telles quelles : le repli sur `EAX`, puis
l'écriture par le pointeur non replié. Sur une machine moderne cela lèverait une violation d'accès
au lieu d'écrire dans la page zéro, mais c'est la bonne façon de reproduire le défaut — **ne pas
étendre le repli à la seconde moitié**. Le faire rendrait le code plus sûr que l'original et
masquerait la contradiction, qui est précisément ce que ce fichier existe pour conserver.

---

## LB-008 — L'indicateur d'écran de la carte du monde émet son mot de base depuis une pile non initialisée

**Repéré** le 2026-08-22, en relevant au désassembleur la disposition du bloc de sprite du bloc
d'affichage de `FUN_00516fc0`. **Latent** : il produit des bits de couleur indéterminés, pas un
plantage.

### Le défaut

Le bloc de sprite que les fonctions de dessin reçoivent en troisième argument a sept champs (voir
`DataSymbols.md`). Son champ `+0x04`, le « mot de base », part dans `prim[1]` sur ses 26 bits bas :

```
prim[1] = ((attributs & 0xffffff00) | (((drapeaux & 0xff) | 0x2c) << 8)) << 0x10
        | (motDeBase & 0x3ffffff);
```

Ce bloc vit à `esp+0x34` dans le cadre de `FUN_00516fc0`. Le désassemblage montre que **la première
écriture de `[esp+0x38]` — donc de `+0x04` — a lieu en `0x0051833d`**, alors que le sous-bloc de
l'indicateur d'écran appelle `FUN_00521f20` en `0x00518038`, trois cents octets plus tôt. Entre
l'entrée de la fonction et cet appel, aucune instruction n'écrit cet emplacement.

L'indicateur émet donc sa primitive avec un mot de base qui vaut ce que l'appelant avait laissé sur
la pile. Le sous-bloc du compteur de mise au point, juste avant, a le même problème pour ses deux
appels à `FUN_0051c120`.

### Pourquoi c'est bénin

Les 26 bits en question se chevauchent avec la moitié haute décalée de seize crans, et ce que
`winsys3d` en fait est une teinte, pas une adresse. Le résultat est un indicateur dont la couleur
dépend du contenu résiduel de la pile — stable en pratique, puisque le même chemin de code repasse
au même endroit à chaque image, mais non défini par le programme.

Ce n'est pas non plus un cas isolé de négligence : dans le même bloc, les autres sous-blocs
écrivent bien `+0x04` avant d'appeler (`uStack_3c = 0x1000000` un peu partout). Celui-ci a été
oublié.

### Dans OpenForesia

`DrawWorldMapFrame` **n'écrit pas** `+0x04` pour l'indicateur, exactement comme l'original. Le bloc
étant un `stackalloc`, le champ y vaut zéro plutôt qu'un résidu — c'est la traduction la plus proche
possible, et surtout il ne faut pas inventer une valeur. Écrire `0x1000000` « par cohérence avec les
voisins » rendrait le rendu plus joli que celui de 1998 et effacerait le défaut.

---

## LB-009 — Le balayage des cellules du monde ne saute qu'une case sur les huit prévues

**Repéré** le 2026-08-23, en transcrivant le balayage 7×7 de `FUN_00516fc0`. **Latent** : il fait
dessiner sept cellules de décor que la table de visibilité désignait comme hors du cône de vue.

### Le défaut

Chacune des huit tranches de `0x00564558` (celles que `PTR_DAT_005645d8` désigne) est une suite de
**huit clés de seize bits** `rang << 8 | colonne`, rangées dans l'ordre exact où le balayage visite
ses 49 cellules. La boucle compare la clé courante à `rang << 8 | colonne` et, à l'égalité, saute la
cellule puis charge la clé suivante.

La première clé se lit bien sur seize bits :

```
005190c5  66 8b 17           mov  dx,[edi]        ; la clé, entière
005190e4  83 c7 02           add  edi,2
```

Les suivantes, non :

```
00519143  66 0f b6 07        movzx ax,byte [edi]  ; un OCTET, étendu de zéro
00519147  83 c7 02           add   edi,2          ; mais le curseur avance de deux
```

Le curseur avance du bon pas, mais la valeur lue est l'octet de poids faible de la paire — donc la
**colonne** seule. La clé vaut alors au plus `0xff`, tandis que `rang << 8 | colonne` vaut au moins
`0x0101` puisque les deux compteurs descendent de 7 à 1. **Plus aucune égalité n'est possible.**

Une seule cellule est donc sautée par image, celle de la première clé, au lieu des huit que la table
prévoit.

### Pourquoi c'est bien un défaut

La même passe, écrite dans les trois listes de morceaux, lit correctement :

```
0051fde5  66 8b 08           mov  cx,[eax]        ; FUN_0051fa40, même table, même clé
0051fde8  83 c0 02           add  eax,2
```

`FUN_0051fa40` et `FUN_00521000` refont le balayage à l'intérieur de chaque morceau, sur les mêmes
deux tables, et consomment leurs huit clés. C'est la même intention écrite deux fois, correcte une
fois sur deux.

### Portée

Sept cellules de plus dans la liste d'affichage par image, aux quatre coins du carré de 7×7. Elles
sont hors du cône de vue de la caméra, donc la plupart de leurs faces tombent de toute façon au
rejet écran ou au rejet de face arrière ; le coût est du travail perdu, pas un artefact visible.

### Dans OpenForesia

`DrawWorldCells` (`Screens/State03/WorldMapTick.cs`) lit **un octet et avance de deux**, comme
l'original, et le dit sur place. Les trois listes de morceaux (`Engine/Model3d.cs`, `DrawChunk`)
lisent seize bits, comme leur original à elles. Corriger `DrawWorldCells` changerait le contenu de
la liste d'affichage — donc le comportement.

---

## LB-010 — La deuxième liste de morceaux réduit un indice de couleur à un booléen

**Repéré** le 2026-08-23, en transcrivant `FUN_00521000`. **Latent** : une seule des quatre formes
est touchée, et la couleur reste dans le tampon d'éclairage.

### Le défaut

Les trois listes de morceaux lisent la couleur d'une face plate dans le tampon d'éclairage, à
l'indice que porte le champ `+0x08` de la face :

```
005217ae  81 e3 ff ff 00 00  and  ebx,0FFFFh            ; FUN_00521000, seau des quads plats
005217de  8b 9c 9f 00 80 1b  mov  ebx,[edi+ebx*4+1B8000h]
```

Le seau des **triangles plats** de la même fonction fait autre chose :

```
00521539  8b 5f 08           mov   ebx,[edi+8]          ; le champ d'indices
00521542  33 d2              xor   edx,edx
00521544  85 db              test  ebx,ebx
0048154f  0f 95 c2           setne dl                   ; dl = (champ != 0) → 0 ou 1
00521582  8b bc 97 00 80 1b  mov   edi,[edi+edx*4+1B8000h]
```

L'indice est remplacé par le **prédicat** « le champ est non nul ». Tous les triangles plats de
cette liste prennent donc la couleur du sommet 0 ou celle du sommet 1, jamais la leur.

### Pourquoi c'est bien un défaut

Les trois autres seaux de `FUN_00521000` indexent normalement, et les seaux homologues de
`FUN_0051fa40` et `FUN_00522140` — le même code écrit trois fois — indexent normalement eux aussi.
Seul ce seau-là diffère.

### Portée

Les triangles plats de la deuxième liste de morceaux ont une teinte plate prise dans les deux
premières entrées du tampon d'éclairage. Comme la source d'éclairage part à `0xffffff` pour les
morceaux, les valeurs restent claires : l'effet est un aplat, pas une couleur fausse et criarde.

### Dans OpenForesia

`EmitChunkTriangles` (`Engine/Model3d.cs`) porte un paramètre `booleanFlatColour`, posé pour la
seule `DrawChunkCells` (`FUN_00521000`), et le commente sur place.

---

## LB-011 — Au bouclage d'un morceau, la tête du flux écrase sa propre queue

**Relevé** le 2026-08-24 en transcrivant `LAB_00513660`, le thread de flux de musique, depuis le
binaire (`Legacy/Raw/Blaze/LAB_00513660.asm`). Non constaté à l'oreille pour l'instant : la
mesure est statique.

### Symptôme attendu

À chaque fois qu'un morceau repasse par son début — donc toutes les quelques minutes, en boucle —
un des huit segments du tampon de flux contient les blocs de tête du morceau **là où auraient dû
se trouver ses derniers blocs**. Il manque donc une fraction de seconde de fin de morceau, et la
reprise se fait un peu trop tôt.

### Mécanisme

Le thread recharge dix blocs source par notification. Quand le curseur arrive à moins de dix blocs
de la fin, il décode en deux fois : ce qui reste jusqu'au bout, puis le complément repris au début
des données. Les deux appels à `FUN_00531bb0` (le décodeur MS-ADPCM) reçoivent la **même**
destination :

```
0051373d  push ebp          ; 1er appel : dest = [esp+14h], le pointeur verrouille
...
0051376d  mov eax,[esp+10h] ; 2e appel : dest = le MEME local, non avance
00513778  push eax
```

Le second décodage aurait dû partir de `dest + remaining * wSamplesPerBlock * 4`. Rien n'ajoute
cette longueur : le binaire relit le local tel quel.

### Dans OpenForesia

`RunMusicStreamWorker` (`Engine/Audio.cs`) transcrit les deux appels avec la même destination et le
signale sur place. Le correctif, si un mode « corrigé » voyait le jour, tiendrait en un terme
ajouté au second `dest`.

---

## LB-012 — L'état 0xd2 de l'automate d'entité rend une valeur jamais écrite

**Relevé** le 2026-08-24 en transcrivant `FUN_00472e80`, la queue de l'automate commun des entités
de zone, depuis le binaire (`Legacy/Raw/Blaze/FUN_00472E80.asm`). Non constaté en jeu : la mesure
est statique, et l'état 0xd2 n'apparaît pas dans la zone de départ.

### Symptôme attendu

Au pas où une entité passe par l'état **0xd2** — celui qui met son sujet en posture 6 et joue le
son d'ouverture — le gestionnaire de l'entité peut, une fois sur un nombre indéterminé, **sauter
entièrement son propre corps** pour ce pas-là. L'entité paraît alors marquer un temps d'arrêt d'une
image avant de reprendre.

### Mécanisme

`FUN_00472e80` rend un `short` dans AX, et son appelant `FUN_004727d0` le propage tel quel jusqu'au
gestionnaire, qui en fait `if (… == 1) return;`. **Toutes** les sorties du corps posent AX
explicitement — `mov ax,1` ou `xor ax,ax` — sauf les deux de l'état 0xd2 :

```
00473364  push 6
00473366  call 0053CEA0h          ; le son
0047336b  add  esp,10h
0047336e  pop  edi                ; ni `mov ax,1`, ni `xor ax,ax`
0047336f  pop  esi
00473370  pop  ebp
00473371  pop  ebx
00473372  ret
```

La branche voisine (`LAB_00473373`, le son 7) a exactement la même forme. Pour comparaison, la
sortie du même appel dans l'état 0xd3 fait bien `xor ax,ax` avant de dépiler.

La valeur rendue est donc celle que `FUN_0053cea0` a laissée dans EAX — et cette fonction est
elle-même déclarée `void` : ses propres sorties rendent ce que `FUN_0053d640` ou `FUN_0053d880` y
ont laissé. Il n'y a aucune valeur définie au bout de la chaîne.

C'est le motif classique du `return` d'une fonction déclarée sans prototype : le compilateur a
traité `PlaySound(...)` comme rendant un `int` et a laissé passer le résultat.

### Dans OpenForesia

`EntityAnimTail_00472E80` (`Screens/State03/WorldMapTick.cs`) rend **0** sur ce chemin, avec la
mesure en commentaire sur place. Ce n'est pas une reproduction du défaut : reproduire une valeur
indéterminée demanderait de transcrire la discipline de registres de `FUN_0053cea0` et de tout ce
qu'elle appelle. Zéro est la valeur des sorties voisines et le seul choix qui ne soit pas un
tirage — mais c'est bien une **déviation déclarée**, pas une transcription.

Le correctif d'un mode « corrigé » serait identique : rendre 0.
