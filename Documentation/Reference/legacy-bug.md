# Bugs du jeu d'origine

Catalogue des défauts **présents dans `Blaze.exe` de 1998**, constatés en jouant au jeu officiel et
recoupés dans le code décompilé.

Il existe parce que la distinction compte : une bizarrerie observée dans OpenForesia peut être une
transcription **fidèle** d'un défaut d'origine plutôt qu'une erreur de reconstruction. Avant de
« corriger » quoi que ce soit, vérifier ici.

**Règle du projet — mode « legacy ++ » depuis le 2026-09-26** : ces défauts **peuvent être
corrigés**, et c'est le but. Une correction suit les règles de `Strategy.md`, section « Mode legacy
++ » : on transcrit d'abord fidèlement, on corrige ensuite ; l'écart est commenté sur place
(`// Écart legacy ++ (LB-NNN) : …`) ; il passe derrière un interrupteur quand le joueur voit ou vit
le jeu autrement ; et il s'applique quand Vincent le demande ou le valide.

**Statut d'une entrée.** Chaque entrée porte, quand il y a lieu, une ligne `**Statut**` :

| statut | sens |
|---|---|
| *(absent)* ou `Reproduit` | le défaut est transcrit tel quel — l'état de toutes les entrées écrites avant le 2026-09-26, sauf mention contraire |
| `Contourné` | le comportement visible est réparé ailleurs (l'hôte, par exemple), le corps transcrit reste fidèle |
| `Corrigé` | le corps transcrit porte l'écart ; l'entrée dit où, et le nom de l'interrupteur s'il y en a un |
| `Retiré` | le code en cause a disparu avec une dépendance de 1998 (le CD, la protection) |

Les entrées LB-030, LB-051 et LB-052, écrites avant le changement de règle, sont déjà des
contournements déclarés.

> **Historique.** Jusqu'au 2026-09-26, la règle était : « ces bugs sont reproduits tels quels. La
> fidélité prime, et un correctif serait une déviation à déclarer explicitement (voir
> `Strategy.md`). Chaque entrée note tout de même où porterait le correctif, si l'envie venait un
> jour d'un mode « corrigé ». » C'est pourquoi la plupart des entrées décrivent le correctif sans
> l'appliquer. Voir `Strategy.md`, « Historique des règles ».

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
d'affichage de `FUN_00516fc0`. **Latent dans l'original** : la valeur que la pile porte à cet
endroit est stable et correcte d'une image à l'autre. **Il ne l'était pas dans OpenForesia** —
voir « Ce que la première analyse a manqué » plus bas.

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

L'indicateur émet donc sa primitive avec un mot de base qui vaut ce que l'image **précédente** avait
laissé sur la pile. Le sous-bloc du compteur de mise au point, juste avant, a le même problème pour
ses deux appels à `FUN_0051c120`.

### Ce que la première analyse a manqué, et ce que ça a coûté

La rédaction du 2026-08-22 concluait « bénin : ce que `winsys3d` en fait est une teinte, pas une
adresse ; le résultat est un indicateur dont la couleur dépend du résidu ». **Les deux moitiés de
cette phrase sont fausses**, et il a fallu voir l'écran pour s'en apercevoir.

D'abord, **le bit 24 du mot de base n'est pas un bit de couleur** : il tombe dans l'octet de
commande de la primitive, celui que `FUN_00a13400` lit en `*(byte *)(param_1 + 7)` pour choisir son
gestionnaire. Il fait basculer la commande **0x2c** en **0x2d**, et les deux ne font pas la même
chose :

| commande | gestionnaire | couleur des quatre sommets |
|---|---|---|
| `0x2c` | `FUN_00a0f570` | les octets `+4`/`+5`/`+6` de `prim[1]`, doublés et bornés — **la teinte** |
| `0x2d` | `FUN_00a0f780` | `0xffffffff` posé en dur — **pas de teinte**, la texture telle quelle |

Ensuite, le résidu n'est pas indéterminé : il est **connu**. `0x0051833d` porte
`mov dword [esp+38h], 1000000h`, et les autres sous-blocs de la fonction écrivent la même valeur au
même emplacement. À la fin de chaque image, `[esp+0x38]` vaut donc `0x1000000` — bit 24 levé — et
l'image suivante le relit tel quel. L'original affiche un indicateur **sans teinte**, image après
image, dès la seconde.

Un `stackalloc` C# est zéro. Zéro, c'est la commande `0x2c` avec une teinte `(0, 0, 0)` : **un carré
noir opaque de quinze pixels en haut à droite de l'écran**, à `(0x130, 0x10)`. Il est resté visible
à l'auberge du 22 au 26 août, décrit dans ce fichier comme « bénin ».

### Dans OpenForesia

`DrawWorldMapFrame` pose `block[1] = 0x1000000` **à l'entrée**, avec le raisonnement ci-dessus en
commentaire. Ce n'est pas combler un trou « par cohérence avec les voisins » — c'est modéliser le
contenu réel de l'emplacement de pile, établi au désassembleur. Les deux sous-blocs qui ne
l'écrivent pas le relisent donc comme dans l'original.

Reste une seule image non modélisée : **la toute première**, où l'original lit ce que le démarrage
avait laissé là. Elle n'est pas déterminable, et une image de teinte fausse ne se voit pas.

### La leçon

**Un défaut classé « latent » n'a pas été observé — il a été raisonné.** Celui-ci était visible à
l'écran depuis quatre jours. Quand une analyse conclut « bénin » sur un chemin que le jeu emprunte à
chaque image, la conclusion se vérifie par une capture, pas par la lecture seule.

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

---

## LB-013 — Le balayage par cellules de `DrawChunk` lit une clé de saut **après la fin** de sa table

**Constaté** le 2026-08-25 par Vincent dans OpenForesia — plantage en parlant à un PNJ de la
deuxième pièce **en biais** (bas + droite ensemble), jamais en face à face. Tracé ensuite dans
`Blaze.c` et dans le binaire.

### Le défaut

`FUN_0051fa40` (notre `Model3d.DrawChunk`, chemin « balayage par cellules ») choisit un des huit
octants d'après le cap de la caméra, puis parcourt les 49 cellules d'une grille 7×7 en sautant
celles que l'octant met hors du cône de vue. La liste des cellules à sauter est une **tranche de
seize octets** — huit clés de 16 bits `rang << 8 | colonne` — et le code y avance un `short*` sans
aucune borne :

```c
sStack_4a  = *(short *)(&PTR_DAT_005645d8)[iVar10];        // Blaze.c 68683
psStack_58 = (short *)((int)(&PTR_DAT_005645d8)[iVar10] + 2);
...
if ((int)sStack_4a == ((int)sStack_40 << 8 | (int)sVar6)) {
  sStack_4a  = *psStack_58;                                 // lit la clé suivante
  psStack_58 = psStack_58 + 1;                              // …même après la huitième
}
```

Chaque correspondance consomme une clé **et lit d'avance la suivante**. Or la huitième tranche —
celle de l'**octant 7**, en `0x005645c8` — se termine sur la clé `01 01`, c'est-à-dire la cellule
`(rang 1, colonne 1)` : la **dernière cellule visitée** du balayage. La huitième correspondance lit
donc une neuvième clé deux octets après la fin du bloc, en `0x005645d8` :

```
0x005645C8: 0707 0107 0106 0102 0701 0601 0201 0101   <- la tranche de l'octant 7
0x005645D8: 5845 5600 6845 5600 …                     <- PTR_DAT_005645d8 elle-même
```

Les deux octets lus sont `58 45` — la moitié basse du premier pointeur, `0x00564558` — donc la clé
vaut `0x4558`.

### Pourquoi c'est bien un défaut

Les huit tranches occupent `0x00564558`..`0x005645d7` et la table de pointeurs qui les désigne
commence **exactement** à `0x005645d8`. Il n'y a donc pas de neuvième tranche : la lecture sort de
l'objet. Les sept autres octants ne sortent pas — leur débordement tombe dans la tranche suivante,
qui existe.

### Pourquoi c'est bénin dans le jeu d'origine

La clé lue n'est **jamais consultée** : elle est lue en `(1,1)`, la dernière cellule ; la boucle
interne se termine sur le `col--` qui suit, puis l'externe sur le `row--`. Et même si elle l'était,
`0x4558` ne peut égaler aucun `rang << 8 | colonne`, tous deux bornés à 7. Sur x86 une lecture de
deux octets dans la `.data` voisine ne coûte rien. Le défaut est donc invisible en 1998.

### Portée

L'octant 7 seulement, et seulement sur le chemin `cellSweep` de `FUN_0051fa40` — le rendu d'un
morceau de décor porteur d'une grille de cellules. C'est ce qui explique la forme du symptôme :
approcher un PNJ **en biais** amène la caméra dans cet octant, l'approcher de face non.

L'autre balayage, `DrawWorldCells` (`FUN_00516fc0`), n'est **pas** concerné : sa lecture de clé
suivante est celle de [LB-009](#lb-009--le-balayage-des-cellules-du-monde-ne-saute-quune-case-sur-les-huit-prévues),
sur **un seul octet**, ce qui rend toute correspondance ultérieure impossible et arrête le curseur
bien avant la fin de la tranche. Un défaut en masque un autre.

### Dans OpenForesia

C'est le seul de ce catalogue qui **plantait** la reconstruction : `s_CellSkipTables_00564558` est
un `ReadOnlySpan<byte>` de 128 octets, et C# lève `IndexOutOfRangeException` là où x86 lisait
paisiblement chez le voisin.

`GameMemory.NextCellSkipKey` restitue la lecture telle qu'elle est : dans la table tant qu'on y est,
et **les vrais octets de `PTR_DAT_005645d8`** au-delà — huit pointeurs vers les huit tranches,
espacés de `0x10`, relevés dans le binaire. Le débordement est donc reproduit, pas gardé : la clé
rendue vaut bien `0x4558`.

Le correctif d'un mode « corrigé » serait d'arrêter le curseur à la fin de la tranche.

---

## LB-014 — La gerbe de la forêt sème trente-trois entités avec un descripteur jamais rempli

**Relevé** le 2026-08-30 dans `FUN_004118a0` (la gerbe aux cinq phases de la forêt,
`0x00411860` / `0x00414cd0`), en lisant la pseudo-C puis en confirmant au désassembleur.

Ghidra déclare dans le prologue :

```c
undefined1 auStack_8 [8];
```

et **aucune écriture ne le précède**. Le tampon part pourtant comme troisième argument de
`FUN_0047ac40`, trente-trois fois à l'entrée de la phase 2, une fois par image ensuite, puis
trente-deux fois à la fermeture :

```c
FUN_0047ac40(param_1, puVar1, auStack_8, (short)param_1[0x26] + 0xc0, 0, 0xffffffff);
```

Le désassemblage confirme : après `sub esp,8` et les quatre `push`, le `lea ecx,[esp+18h]` désigne
bien ces huit octets, et rien ne les a écrits. Ce troisième argument est le **descripteur
d'atlas** : `FUN_0047ac40` le range tel quel dans le `+0x3c` de l'entité qui naît. Les nouvelles
entités partent donc avec un pointeur vers de la pile morte.

**Pourquoi le jeu ne plante pas.** Chaque corps d'effet pose sa propre planche dans `+0x3c` à son
état 0, c'est-à-dire à sa toute première image — avant que quoi que ce soit ne lise le champ. La
valeur héritée est écrasée sans jamais avoir servi. Le défaut est donc invisible en jeu, et il
l'était déjà en 1998 : il tient sur une coïncidence entre deux fonctions qui ne se connaissent
pas.

**Ce que fait OpenForesia.** Le tampon est reproduit tel quel — huit octets de pile passés au
même endroit — plutôt que remplacé par `null`. Passer `null` serait *moins* fidèle : si un jour
un corps d'effet omettait de poser sa planche, l'original afficherait n'importe quoi et la
reconstruction déréférencerait un pointeur nul. Le comportement doit rester le même dans les deux
cas.

Le correctif d'un mode « corrigé » serait de passer explicitement la planche que l'effet attend,
ou `null` si le champ est de toute façon réécrit.

---

## LB-015 — L'invocation de la forêt lit un modèle par un registre non initialisé

**Relevé** le 2026-08-30 dans `0x00411f80` et ses trois copies (`0x00412460`, `0x004153f0`,
`0x00415990`), relevés au désassembleur — la fonction est absente de `Blaze.c`.

Le prologue réserve `0xa0` octets, en met `0x9c` à zéro à partir de `[esp+0x14]`, et lit ensuite :

```asm
mov  esi,[esp+0B4h]      ; l'entité, l'argument
mov  ebp,[esp+10h]       ; <- QUATRE OCTETS SOUS le tampon mis à zéro
lea  edi,[esp+14h]       ; le tampon de secours
```

`[esp+0x10]` n'est **pas** dans la zone mise à zéro : c'est le mot juste en dessous, laissé par
l'appelant. Le balayage des trente-deux entités de scène qui suit écrase `edi` et `ebp` à chaque
entité qui répond — mais seulement si l'une répond :

```asm
mov  edx,[edx*4+5E9640h]
mov  ebp,[edx+8]         ; le modèle d'animation de l'entité trouvée
```

Si **aucune** entité de scène n'a `+0x0c == 0` et le bit `0x80000000`, `edi` reste le tampon nul —
inoffensif, tous les champs qu'on y écrit sont perdus — et `ebp` reste les quatre octets de pile
d'origine. Le corps lit ensuite, dès que la tenue d'image est atteinte :

```asm
mov  ax,[ebp+16h]        ; le nombre d'images de l'animation
```

C'est une lecture à travers un pointeur arbitraire. En pratique le gestionnaire n'est installé que
là où une entité de scène l'attend, ce qui explique que le jeu de 1998 n'en soit jamais mort.

**Ce que fait OpenForesia.** Le pointeur de modèle est déclaré **nul** plutôt que rempli d'une
valeur inventée. Le chemin fautif est le même, il se produit à la même condition, et il se
signale au lieu de lire silencieusement de la pile morte. C'est une déviation assumée : la valeur
d'origine n'existe pas — elle dépend de ce que l'appelant avait laissé — donc aucune transcription
ne peut la restituer.

Le correctif d'un mode « corrigé » serait de sortir sans rien faire quand le balayage ne trouve
personne.

---

## LB-016 — Le verbe « fais parler le membre désigné » teste un cinquième emplacement

**Relevé** le 2026-08-30 dans `0x00443300`, absent de `Blaze.c`, relevé au désassembleur.

Le verbe cherche le dernier emplacement vivant, puis lit dans la banque d'état 2 le numéro du
membre que le script veut faire parler. Si ce numéro dépasse 3 — donc s'il ne désigne aucun des
quatre emplacements — il vérifie sa validité avant de retomber sur le dernier vivant :

```asm
0044333a  cmp  dx,3
0044333e  jle  00443372            ; <= 3 : on l'emploie tel quel
00443340  movsx ecx,dx             ; sinon, on le "verifie"
0044334b  test dword [edi*4+5E93C0h],80000000h   ; edi = dx * 0x27  -> OK
00443358  movsx eax,ax             ; <-- ax vaut 4, le compteur laisse par la boucle
00443363  test dword [ecx*4+5E93C0h],50000000h   ; ecx = 4 * 0x27  -> HORS TABLE
```

Le second test devait porter sur le même numéro que le premier. Le compilateur y a employé `eax`,
qui contient encore la valeur de sortie de la boucle de balayage, c'est-à-dire **4**. La table
`0x005e93c0` n'a que quatre fiches de `0x9c` octets : la lecture tombe `0x270` octets après son
début, sur ce que l'arène contient ensuite.

**Pourquoi c'est sans conséquence en pratique.** Le chemin n'est atteint que si l'octet de banque
dépasse déjà 3, c'est-à-dire quand le script désigne un membre qui n'existe pas — un cas qui
finit de toute façon par retomber sur le dernier vivant. La valeur lue ne fait que choisir
**laquelle** des deux branches de repli est prise, et les deux mènent au même endroit dès que le
premier test échoue.

**Ce que fait OpenForesia.** La lecture est reproduite telle quelle. `0x005e93c0` est une vue dans
l'arène contiguë, pas une allocation de `0x270` octets : lire le cinquième emplacement tombe donc
sur les octets suivants de l'arène, exactement comme en 1998, sans débordement de tas.

Le correctif d'un mode « corrigé » serait de tester le même numéro dans les deux branches.

---

## Note — ceci n'est PAS un bug d'origine : deux fautes de transcription du 2026-08-25

Consignees ici parce que leur symptome ressemblait a un defaut de 1998, et que la prochaine
sauvegarde bizarre appellera la meme verification. **Les deux etaient de nous.**

### 1. `DAT_005dff3c[1]` n'est pas l'octet de +1

Ghidra type `DAT_005dff3c` en **pointeur de dword** — la preuve est deux lignes plus haut,
`*DAT_005dff3c = *DAT_005dff3c & 0xfffeffff`. Son `[1]` designe donc le dword de **+4**, dont
l'expression prend l'octet bas. Transcrit sur un `byte*` en C#, `[1]` lisait l'octet de +1 : trois
octets a cote.

Le desassemblage tranche sans appel (`0x005407f3`) :

```
movzx cx,byte [eax+4]      ; l'octet de +4
movzx dx,byte [eax+0Bh]
lea   ecx,[edx+ecx*4]
shl   ecx,4
```

Cet octet choisit la page de l'icone de sauvegarde. Faux, il capture un rectangle de VRAM au mauvais
endroit — d'ou un **portrait bariole** a l'ecran « constituer un groupe », qui se lit tres bien comme
un bug de 1998.

**La regle** : avant d'indexer un `DAT_` de Ghidra, chercher un `*DAT_ = *DAT_ & …` dans le meme
corps. C'est lui qui donne la largeur du pointeur, et donc le pas de l'indice.

### 2. Le succes et l'echec ne posent pas le meme etat

L'etat 8 du coffre finit par `DAT_005dff7c = DAT_005dff7c + 1` quand l'ecriture a reussi — 8 vers 9,
la sortie — et par `DAT_005dff7c = 99` quand elle a echoue. Les deux chemins partagent le
`FUN_00544350` du libelle, ce qui invite a partager aussi la queue ; c'est ce que la transcription
avait fait, en posant 99 dans les deux cas.

Consequence : apres une ecriture **reussie**, l'etat 99 remettait le magasin d'avant la tentative et
renvoyait a l'etat 4, donc a l'ecriture. Le panneau ne se fermait plus et le fichier etait reecrit a
chaque tour, avec un contenu qui derivait — **la sauvegarde de Vincent en est morte**.

**La regle** : quand deux chemins d'un `switch` partagent leurs dernieres lignes, verifier qu'ils
partagent aussi celles d'avant. Ici le `break` du chemin de succes etait a l'interieur du `if`, et
c'est tout ce qui les separait.

---

## LB-017 — La flèche magique qui perd sa cible se réoriente sur son propre pointeur

**Relevé** le 2026-08-31 au désassembleur, en transcrivant `FUN_0047ca50` (le sous-type `0x6d`,
la flèche de *Flêches magiques*). Absent de `Blaze.c` — Ghidra n'a pas décompilé ce corps.

### Symptôme

Une flèche magique dont la cible meurt **en vol** part dans une direction arbitraire au lieu de
continuer sur sa lancée. La direction est toujours la même pour un même emplacement de la table des
entités, ce qui la rend reproductible mais dénuée de sens.

### La cause

Le corps garde son cap et son tangage dans deux registres, `ebp` et `eax`, avant d'appeler
`FUN_00532ad0` pour en tirer un vecteur de vitesse. Trois chemins y mènent :

| chemin | cap et tangage |
|---|---|
| il poursuit sa cible | deux `ratan2` sur les écarts — correct |
| il n'a plus de cible du tout | `+0x32 - 0x400` et `+0x34` — correct |
| **sa cible vient de mourir** | `[esp+0x44]`, **lu deux fois, jamais écrit** |

```
0047cae2  mov ebp,[esp+44h]     <- jamais ecrit sur ce chemin
0047cae6  mov [eax+74h],edi
0047cae9  mov eax,[esp+44h]     <- le meme
0047caed  mov [esi+78h],edi
0047caf4  jmp 0047CB8A          <- FUN_00532ad0(dest, ebp, eax, 0x10)
```

`[esp+0x44]` est, à cet instant, **l'emplacement du premier argument de la fonction** — c'est-à-dire
le pointeur de l'enregistrement de la flèche elle-même. Le cap et le tangage valent donc une
adresse, que la table de trigonométrie ramène à douze bits. Les deux autres chemins écrivent bien
`ebp` et `eax` ; celui-là a été oublié.

C'est le même genre de défaut que **LB-008** et **LB-015** : une valeur prise dans une pile ou un
registre que le chemin emprunté n'a pas renseigné. Trois occurrences maintenant, toutes dans des
corps que Ghidra n'a pas décompilés — ce n'est pas un hasard, ce sont les corps que personne n'a
relus.

### Ce que la transcription fait

`TickMagicArrowShaft_0047CA50` reproduit la lecture telle quelle, en prenant le pointeur de
l'enregistrement comme angle. La valeur numérique diffère de celle de 1998 — les adresses ne sont
pas les mêmes — mais elle l'était déjà d'une partie à l'autre : ce qui est fidèle, c'est que la
direction ne veut rien dire.

### Où porterait le correctif

Sur ce chemin, poser `ebp = +0x32 - 0x400` et `eax = +0x34` comme le fait le chemin « plus de
cible » : la flèche continuerait tout droit, ce qui est visiblement l'intention.

---

## LB-018 — Boule de feu pousse sa victime dans la direction de sa propre adresse

**Relevé** le 2026-08-31 au désassembleur, en transcrivant `FUN_0047f570` (le sous-type `0x14`).
Absent de `Blaze.c`. **Même défaut que LB-017, dans un autre corps.**

### Symptôme

Une Boule de feu qui touche quelqu'un **entre sa dixième et sa dix-neuvième image** — la phase où
elle est formée mais n'a pas encore commencé à voler — projette sa victime dans une direction
arbitraire au lieu de l'écarter du point d'impact. Comme pour LB-017, la direction est stable pour
un même emplacement de la table des entités.

### La cause

Le cap de la poussée vit dans `ebp` jusqu'à la fin du corps. Quatre chemins y mènent :

| âge | `ebp` |
|---|---|
| 0 (naissance) | `+0x32`, puis réécrit par la branche « moins de dix » |
| 1 à 9 | `+0x32` — correct |
| **10 à 19** | **`[esp+0x38]`, jamais écrit sur ce chemin** |
| 20 et au-delà | `+0x32` — correct |

```
0047f78e  mov ebp,[esp+38h]      <- jamais ecrit quand on arrive ici
...
0047fac4  push ebp
0047fac5  call 00545A1Ch         <- rsin(ebp) : la poussee
```

`[esp+0x38]` est, à cet instant, **l'emplacement du premier argument de la fonction** — le pointeur
de l'enregistrement. Les deux seules écritures dans cette case (`0047f91a` et `0047f9f1`) sont sur
des chemins que les images 10 à 19 ne prennent pas, et la pile est réécrite par l'appelant à chaque
image : la valeur lue est donc toujours l'adresse, jamais un reste de l'image précédente.

C'est la **quatrième** occurrence de cette famille, après LB-008, LB-015 et LB-017 — et toutes
les quatre sont dans des corps que Ghidra n'a pas décompilés.

### Ce que la transcription fait

`TickFireOrb_0047F570` reproduit la lecture, en prenant le pointeur de l'enregistrement comme cap.
La valeur diffère de celle de 1998 ; ce qui est fidèle, c'est que la direction ne veut rien dire.

### Où porterait le correctif

Poser `ebp = +0x32` sur ce chemin comme sur les trois autres.

---

## LB-019 — L'état de fin de « Pardon » est écrit puis effacé une instruction plus loin

**Fonctions** : `FUN_0044f060` (Pardon, école 1 indice 0, sous-type `0x1e`) **et `FUN_0044f930`**
(Zombie, école 1 indice 15, sous-type `0x2d`), relevées le 2026-08-31. Toutes deux absentes de
`Blaze.c` — trouvées au désassembleur.

**Le défaut n'est pas propre à Pardon** (constaté le 2026-08-31, en transcrivant Zombie). Les deux
corps sont bâtis sur le même patron — même balayage des trente-deux entités de scène, même critère
d'espèce, même trait `0x9e` — et ils portent la même chute. Zombie y ajoute un second appel
d'extinction sans garde, décrit en fin d'entrée.

### Le symptôme

Quand le balayage des trente-deux entités de scène ne trouve aucune victime éligible, le sort est
censé se terminer : la branche pose `+0x10 = 0x63`, l'état de fin, et `+0x16 = 0x46`. Mais elle ne
saute pas — elle **tombe** dans l'instruction suivante, qui pose `+0x10 = 2` inconditionnellement :

```
0044f23c  jmp  0044F24A          <- la branche « cible trouvée » saute ici
LAB_0044f23e:                     <- la branche « aucune cible » arrive ici
0044f23e  mov  word [edi+10h],63h    <- l'état de fin…
0044f244  mov  word [edi+16h],46h
LAB_0044f24a:
0044f24a  mov  word [edi+10h],2      <- …effacé ici, six octets plus loin
```

Le `0x63` est donc **mort** : les deux chemins sortent en état 2.

### Pourquoi c'en est un

Le `jmp` de la branche « trouvée » vise `LAB_0044f24a`, pas `LAB_0044f23e` : les deux blocs sont
bien distincts, et le second n'est atteint que par l'échec. Écrire l'état de fin pour l'écraser
aussitôt n'a aucune lecture cohérente ; l'intention lisible est de terminer sur-le-champ.

### Ce que ça change en jeu

Peu, et c'est sans doute pourquoi personne ne l'a vu. Le `+0x16 = 0x46` de la même branche, lui,
survit ; or l'état 2 passe en `0x63` dès que `+0x16` atteint `0x5a`. Un Pardon sans cible attend
donc **vingt images au lieu de quatre-vingt-dix** — un raccourci, là où le code voulait zéro.

### Ce que la transcription fait

`TickForgive_0044F060` écrit les deux affectations dans l'ordre du binaire, `0x63` compris, et
laisse le `2` les suivre. `TickZombie_0044F930` fait de même.

### Le supplément de Zombie : un second appel d'extinction sans garde

Son bloc de fin appelle `FUN_00448020` — l'extinction avec remise du décor — **deux fois** :

```
0044fada  cmp  word [edi+10h],63h
0044fae3  jne  0044FB05
0044fae5  cmp  ax,5Ah
0044fae9  jl   0044FAF8          <- ne saute QUE le premier appel
0044faeb  call 00448020h         <- premier, sous garde
LAB_0044faf8:
0044fafd  call 00448020h         <- second, sans garde
```

**Sans conséquence** : l'état `0x63` n'est atteint que par ce même test `+0x16 >= 0x5a`, donc la
garde est toujours vraie quand le bloc s'exécute, et l'extinction est idempotente. La transcription
garde les deux appels.

### Où porterait le correctif

Remplacer la chute par un saut par-dessus `LAB_0044f24a` (et par-dessus `LAB_0044faf8` pour Zombie).

---

## LB-020 — Le fusible de secours du trait de « Sonde » ne peut pas sauter

**Fonction** : `FUN_004487f0` (le trait de Sonde, sous-type `0xa2` de la table `0x00551f30`),
relevé le 2026-08-31. Décompilé dans `Blaze.c`, confirmé au désassembleur.

### Le symptôme

La dernière instruction de la fonction est une sécurité : si le trait vit plus de 5 000 images
(environ une minute et demie), il s'éteint de lui-même. Mais le compteur qu'elle lit est un
**octet** :

```
00448c1d  mov  bl,[esi+43h]        <- lecture sur 8 bits
00448c20  inc  bl
00448c22  mov  al,bl
00448c24  mov  [esi+43h],bl
00448c27  and  eax,0FFh            <- et la comparaison est faite sur cet octet
00448c2c  cmp  eax,1388h           <- 5000
00448c31  jl   00448C39
00448c33  and  dword [esi],7FFFFFFFh   <- inatteignable
```

`eax` est masqué à `0xff` juste avant la comparaison : la valeur comparée vaut au plus 255, donc
jamais 5 000. La branche d'extinction est morte, et le compteur repasse par zéro toutes les 256
images.

### Pourquoi c'en est un

Une comparaison à 5 000 n'a de sens que si le compteur peut l'atteindre. Le champ `+0x43` est
manifestement un octet du bloc — les deux autres lectures de la fonction (`cmp byte [esi+43h],64h`)
le traitent aussi comme tel —, mais la constante de garde, elle, a été écrite pour un compteur de
16 bits. C'est un seuil qui n'a jamais été relu après un changement de largeur de champ.

### Ce que ça change en jeu

Rien de visible, et c'est probablement pourquoi personne ne l'a vu. Le chemin normal éteint le trait
bien avant : passé la centième image, l'état `0x63` réduit son échelle de 200 par image et le tue en
une trentaine d'images de plus. La sécurité ne servait qu'au cas où un trait resterait coincé dans un
état antérieur — et dans ce cas-là, elle ne le sauverait pas.

### Ce que la transcription fait

`TickProbeRing_004487F0` écrit la comparaison telle quelle, masque compris, et laisse la branche
inatteignable en place.

### Où porterait le correctif

Comparer le compteur à 255, ou l'élargir à 16 bits — un champ libre voisin serait nécessaire, le
`+0x43` est un octet du bloc.

---

## LB-021 — « Bobos finis » décide d'une étincelle sur un mot de pile que rien n'a écrit

**Fonction** : `FUN_004491f0` (Bobos finis, école 1 indice 5, sous-type `0x23`), relevé le
2026-08-31. Absente de `Blaze.c` — trouvée au désassembleur.

### Le symptôme

Le prologue réserve huit octets de locaux, et la fonction n'en écrit qu'un :

```asm
004491f0  sub  esp,8              <- deux mots de locaux : [esp+10h] et [esp+14h]
...
0044927b  mov  dword [esp+10h],4  <- le premier est le compteur de boucle
```

`[esp+0x14]` n'est écrit **nulle part** dans les 822 octets. Il est lu une seule fois, en mot, et
sert de condition à un semis :

```asm
00449421  cmp  word [esp+14h],3Ch     <- lecture d'un local jamais écrit
...
00449436  jge  0044944Bh              <- ≥ 60 : pas d'étincelle
00449438  push 0FFFFFFFFh             <- < 60 : une étincelle 0xa9
```

### Pourquoi c'en est un

Un local jamais écrit vaut ce que le dernier appel de même profondeur a laissé sur la pile. La
comparaison à `0x3c` — soixante, la valeur d'une temporisation — a toutes les allures d'un test
écrit pour un compteur qui existait ailleurs et qui n'a jamais été relié ici. Aucune lecture ne rend
ce code correct : la branche est décidée par du bruit.

### Ce que ça change en jeu

Peu de chose, et de façon invisible. La seconde étincelle du même bloc est tirée juste après, une
fois sur sept ; celle-ci fait donc varier la densité du scintillement autour d'un membre soigné,
sans jamais toucher au soin lui-même. Le résultat est stable pour un chemin d'appel donné — la pile
est déterministe —, ce qui explique que ça n'ait pas l'air aléatoire en jouant.

### Ce que la transcription fait

`TickHealAll_004491F0` déclare ce mot à **zéro** plutôt que d'inventer une valeur, sur le même
principe que LB-015 : le chemin fautif est conservé, la condition reste visible, et la branche est
donc toujours prise dans OpenForesia. C'est une déviation assumée — la valeur d'origine n'existe
pas, elle dépend de ce que l'appelant avait laissé.

### Où porterait le correctif

Initialiser le local, ou supprimer le test et semer l'étincelle inconditionnellement — ce que fait
déjà le port, par construction.

---

## LB-022 — Le dôme de « Lapêche » ne balaie que la moitié de la quatrième table

**Fonctions** : `FUN_0044a880` (le dôme de Lapêche) et sa jumelle `FUN_0044af70` (le dôme de
Prière), relevées le 2026-08-31. Toutes deux décompilées dans `Blaze.c`, confirmées au
désassembleur.

### Le symptôme

Les deux dômes balaient les mêmes quatre tables d'entités à chaque image pour attraper ce qui passe
à portée. Sur les trois premières les deux comptent pareil. Sur la quatrième, `0x005ff400`, ils ne
comptent pas la même chose :

```
Lapêche  0044ad68  mov  ebp,5FF400h
         0044ad6d  mov  dword [esp+14h],10h      <- seize créneaux

Prière   0044b3f7  mov  ebp,5FF400h
         0044b3fc  mov  dword [esp+14h],20h      <- trente-deux
```

### Pourquoi c'en est un

La table `0x005ff400` compte **trente-deux** créneaux, et tout le reste du jeu la traite comme telle
— `ClearEntitySlots(0x005ff400, 0x20)` à l'entrée de la carte, `ApplyFlagToSlots(0x005ff400, 0x20,
…)` dans les verbes de script. Prière la balaie entièrement ; Lapêche s'arrête à la moitié. Les deux
corps sont par ailleurs le même à cinq valeurs près, et les quatre autres comptes de boucle sont
identiques : `0x20`, `0x80`, `0x80`. Le `0x10` est isolé.

### Ce que ça change en jeu

Une entité logée dans les seize derniers créneaux de cette table traverse le dôme de Lapêche sans
être attrapée, alors que celui de Prière l'aurait prise. L'effet est invisible tant que la table
reste peu remplie — les créneaux sont alloués depuis le début —, et ne se manifeste que dans une
scène chargée. C'est probablement pourquoi personne ne l'a vu.

### Ce que la transcription fait

`TickHolyDome_0044A880` balaie `0x10` créneaux, `TickHolyWard_0044AF70` en balaie `0x20`, comme
leurs binaires respectifs.

### Où porterait le correctif

Passer le compte de Lapêche à `0x20`, comme sa jumelle.

---

## LB-023 — L'éviction de « Ange » marque une proie et en démarque une autre

**Fonction** : `FUN_0044ded0` (la silhouette d'Ange, sous-type `0x9c`), 2 919 octets, absente de
`Blaze.c` — relevée au désassembleur le 2026-09-01. Le relevé est gardé sous
`Legacy/Raw/Blaze/FUN_0044DED0.asm`.

### Le symptôme

L'ange retient jusqu'à six proies dans un tableau de pile, et **marque chacune** en lui écrivant le
sort dans son `+0x74` — c'est ce marquage qui dit « celle-ci est prise ». Quand une septième proie
passe le tri, il compare sa distance à celle de chaque proie déjà retenue et remplace la première
qu'il trouve plus lointaine. Les trois écritures de ce remplacement partent dans deux directions :

```
0044e515  mov  dword [ebp+54h],0        <- la NOUVELLE proie est démarquée
0044e51c  mov  edx,[esi+6ch]
0044e51f  mov  [eax+74h],edx            <- l'ANCIENNE est marquée
0044e52c  mov  [edx],cx                 <- et c'est la NOUVELLE qui prend sa place dans le tableau
```

`ebp` pointe la nouvelle proie (à `+0x20` près), `eax` l'ancienne, que la boucle vient de résoudre.
La nouvelle entre dans le tableau et sort du marquage ; l'ancienne quitte le tableau et y reste.

### Pourquoi c'en est un

Les deux moitiés du corps — celle qui chasse les entités de scène et celle qui chasse le groupe —
portent la même inversion, aux mêmes trois instructions près. Et le marquage n'est pas décoratif :
la plume relâche sa proie en remettant ce même `+0x74` à zéro quand elle s'éteint, et la boucle de
tri refuse une proie déjà prise. Une entité peut donc rester marquée alors que plus aucune plume ne
la vise, et une autre être visée sans porter la marque.

### Ce que ça change en jeu

Peu, et seulement au-delà de six ennemis simultanés à portée de l'ange — le cas est rare. La proie
évincée garde une marque que rien n'effacera avant la fin du sort ; la proie qui la remplace est
attaquée sans être marquée, et peut donc être retenue une seconde fois par la même passe.

### Où porterait le correctif

Échanger les deux destinations : `[ebp+54h]` reçoit le sort, `[eax+74h]` reçoit zéro.

---

## LB-024 — « Ange » ne balaie que trois des quatre membres du groupe

**Fonction** : `FUN_0044ded0`, la moitié qui chasse le groupe (celle qu'un lanceur **hors** groupe
emprunte), relevée le 2026-09-01.

### Le symptôme

La moitié qui balaie les entités de scène compte `0x20` créneaux, ce qui est le compte de la table.
Celle qui balaie le groupe s'arrête à trois :

```
0044e8af  cmp  ax,3
0044e8b3  ...
0044e8c3  jl   0044E5DCh      <- indices 0, 1 et 2 seulement
```

### Pourquoi c'en est un

Le groupe compte **quatre** emplacements, et tout le reste du jeu les traite comme quatre —
`AcquireHolyTarget_00447CB0` en mode 1 en parcourt quatre, « Coup de pouce » en parcourt quatre.
Le `3` est isolé, dans une moitié de corps par ailleurs identique à sa jumelle.

### Ce que ça change en jeu

**Rien d'observable, sans doute.** Cette moitié n'est empruntée que si le lanceur ne porte pas
`0x800000`, c'est-à-dire s'il n'est pas un membre du groupe — un monstre lançant Ange. Aucune fiche
de monstre relevée jusqu'ici ne le fait. Le quatrième personnage serait à l'abri.

### Où porterait le correctif

Passer le compte à `4`.

---

## LB-025 — La moitié « groupe » de « Ange » relit ses indices dans la table de scène

**Fonction** : `FUN_0044ded0`, même moitié que LB-024, relevée le 2026-09-01.

### Le symptôme

Les deux moitiés retiennent des **indices de créneau** dans le même tableau de pile, et les
reconvertissent en adresses au moment de comparer les distances. Les deux le font avec la même base :

```
0044e4bb  mov  ecx,[edx*4+5ECA20h]      <- moitié SCÈNE
0044e7b2  mov  ecx,[edx*4+5ECA20h]      <- moitié GROUPE, la même
```

`0x005eca04 + indice * 0x9c` est la table des entités de scène. Les indices que la moitié « groupe »
y a rangés désignent pourtant des emplacements de `0x005e93c0`.

### Pourquoi c'en est un

La plume, elle, choisit la bonne table selon le camp du lanceur — `0x5eca04` d'un côté, `0x5e9324`
de l'autre (`FUN_0044ea60`, 0x0044ec19 et 0x0044ec2d). Les deux corps lisent le même tableau
d'indices et n'en tirent pas la même chose.

### Ce que ça change en jeu

**Rien d'observable, pour la même raison que LB-024** : seule la moitié « groupe » est touchée, et
elle n'est atteinte que par un lanceur hors groupe. Si elle l'était, la comparaison de distance
porterait sur des entités quelconques, et l'éviction choisirait mal — sans jamais planter, la table
de scène étant de même taille et de même disposition.

### Où porterait le correctif

Employer `0x005e9324` dans la moitié « groupe », comme le fait la plume.

---

## LB-026 — « Champ magique » et « Escargot » visent le dernier candidat balayé, pas le plus proche

**Relevé** le 2026-09-01 au désassembleur, en transcrivant `FUN_004780b0`
(`TickMagicField_004780B0`). Le corps est absent de `Blaze.c` ; le relevé est sous
`Legacy/Raw/Blaze/FUN_004780B0.asm`. **Le défaut est double** : `FUN_004786c0` (« Escargot »,
`TickSnail_004786C0`) est la jumelle recompilée de ce corps et le porte à l'identique — seuls
le son de départ, un second son à la douzième image et la classe d'entités chassée les
séparent. Ce qui suit vaut mot pour mot pour les deux.

### Symptôme

Le champ est une sentinelle : à chaque image il balaie les trois tables d'entités, retient la plus
proche du porteur, et tire dessus quand elle est à portée et qu'il est bien pointé. **La direction
vers laquelle il se tourne n'est pas celle de cette entité-là** : c'est celle du dernier candidat
rencontré pendant le balayage, qu'il soit le plus proche ou non.

### Cause

Le balayage range les trois écarts de position dans des emplacements de pile **avant** la
comparaison de distance, donc pour *chaque* candidat retenu :

```
0047832c  mov [esp+28h],eax      <- écart X, inconditionnel
00478336  mov [esp+14h],ecx      <- écart Y, inconditionnel
0047833d  mov [esp+18h],edx      <- écart Z, inconditionnel
00478355  call 00545A22h         <- la distance
0047835e  cmp  ebp,eax
00478360  jle  0047836Ch         <- pas meilleur : on passe au suivant
00478364  mov  [esp+10h],edi     <- meilleur : on garde LE POINTEUR
00478368  mov  [esp+1Ch],ebp     <- et LA DISTANCE
```

La branche « celui-ci est meilleur » ne garde que le pointeur et la distance. Les trois écarts, eux,
ont déjà été écrasés par le candidat suivant. Le bloc de visée (`0x004783a1`) les relit tels quels
pour calculer le lacet et le tangage.

### Ce que ça change en jeu

**Le champ tire quand même sur la bonne cible** — le pointeur retenu est bien le plus proche, et
c'est lui qui reçoit les quatre étincelles et le bit `0x40000000`. Ce qui est faux, c'est
l'orientation visible du champ et, par contrecoup, **le moment** du tir : les deux conditions
d'angle (`±0x200` en lacet et en tangage) portent sur l'écart vers le *dernier* candidat. Avec une
seule entité en vue — le cas ordinaire, un projectile isolé — les deux coïncident et rien ne se
voit. Avec plusieurs, le champ se tourne vers la mauvaise et refuse de tirer sur la bonne jusqu'à ce
que les deux directions se rapprochent par hasard.

### Où porterait le correctif

Déplacer les trois `mov [esp+…]` d'écart dans la branche « celui-ci est meilleur », à côté du
pointeur et de la distance.

---

## LB-027 — Le script de sons de « Liquide » n'a pas de terminateur

**Relevé** le 2026-09-01 dans les données du binaire, en transcrivant `FUN_00479f70`
(« Liquide », `TickLiquid_00479F70`), qui met ce script en file à sa première image.

### Symptôme

`&DAT_00551978` est un script de sons différés de cinq enregistrements. **Aucun des cinq ne porte
un octet de délai nul**, alors que c'est la seule chose qui termine un script. Les six autres
scripts du dépôt en ont un ; celui-là n'en a pas.

### Cause

Les octets, relevés à `at 0x00551978 40` :

```
0x00551978: 11 01 01 00 bf 01 00 12     <- délai 1
0x00551980: 18 04 01 00 a6 0c 00 12     <- délai 4
0x00551988: 18 04 01 00 50 0d 00 12     <- délai 4
0x00551990: 18 04 01 00 fa 0d 00 12     <- délai 4
0x00551998: 18 04 01 00 a4 0e 00 12     <- délai 4, et c'est le dernier
0x005519a0: 00 c0 10 10 01 02 10 00     <- &DAT_005519a0, la table de descripteurs
```

`Audio.TickDelayedSounds` lit le délai en `[1]` et le nombre de sons en `[2]`, puis avance de huit
octets par son. Après le cinquième pas il avance donc dans **la table de descripteurs**, dont le
premier enregistrement se lit comme « seize sons d'identifiant 0, puis 192 images d'attente », et
repart 128 octets plus loin — au milieu de `&DAT_00551a08`. Rien ne l'arrête ensuite : il continue
à marcher dans `.data`.

### Ce que ça change en jeu

**Difficile à dire sans l'entendre.** L'identifiant 0 est vraisemblablement inerte, auquel cas le
seul effet est qu'un emplacement de la file des sons différés reste occupé indéfiniment — la file
en a trente-deux et n'a pas de test d'occupation (voir la note portée sur
`s_DelayedSounds_005dfb90`), donc l'emplacement finit par être réécrasé. Si l'identifiant 0 produit
un son, en revanche, chaque lancer de Liquide, Flamme, Zéphé ou Terre en déclenche seize d'un coup
cinq images après le début.

### Ce que ce port en fait

**Il ne reproduit pas la marche.** `s_DelayedSoundScript_00551978` s'arrête à ses cinq
enregistrements ; les deux tables étant ici deux allocations distinctes, la lecture au-delà tomberait
sur de la mémoire quelconque plutôt que sur les descripteurs. Reproduire fidèlement demanderait de
recopier 168 octets de tables sans rapport à la suite du script. **C'est une divergence déclarée**,
notée ici et dans le commentaire du tableau.

### Où porterait le correctif

Mettre l'octet `[1]` du cinquième enregistrement à zéro.

### Les trois autres élémentaires, vérifiés depuis

**Ajout du 2026-09-01, en transcrivant `FUN_00479300` (« Flamme »).** Le titre d'origine de cette
entrée disait « les quatre élémentaires » ; c'était une extrapolation, et elle est fausse. Les
quatre sorts empilent **quatre scripts différents**, relevés un par un dans leur corps respectif :

| sort | corps | script | dernier délai |
|---|---|---|---|
| Liquide | `0x00479F70` | `0x00551978` | **4 — aucun terminateur** |
| Flamme | `0x00479300` | `0x005518F8` | 0 — se termine proprement |
| Terre | `0x00479C50` | `0x00551940` | **10 — pas de terminateur non plus** |
| Zéphé | `0x00479DE0` | `0x00551968` | 0 — se termine proprement |

**Terre déborde aussi, mais sans conséquence** : ses cinq pas sont suivis immédiatement par le
script de Zéphé (`0x00551968`), dont le second pas porte un délai nul. Terre joue donc **deux sons
de trop** — le `0x11` d'ouverture puis un `0x23` — dix images après le dernier des siens, et
s'arrête là. Ce n'est pas la marche dans `.data` de Liquide : c'est un script qui en avale un autre
et qui s'arrête au bout.

Le défaut décrit ci-dessus **ne concerne donc que Liquide**, et c'est le seul des quatre dont le
débordement sorte de la zone des scripts.

**Et celui de Terre, ce port le reproduit** (2026-09-01, en transcrivant `FUN_00479c50`). Il est
borné : les deux scripts se suivent sans trou dans `.data`, donc les sept pas sont déclarés d'un
seul bloc dans `s_DelayedSoundScript_00551940`, et `DelayedSoundScript00551968` est une **vue à
`+0x28`** de ce même bloc plutôt qu'une allocation séparée — la même façon de faire que
`Descriptors00551BC0`. Les séparer casserait le débordement. Terre joue donc bien ses deux sons de
trop, comme en 1998. **Seul le débordement de Liquide reste une divergence déclarée**, parce que
lui seul sortirait de la zone des scripts.

---

## LB-028 — La braise des élémentaires ne peut jamais s'éclaircir

**Relevé** le 2026-09-01 en transcrivant `FUN_00479bc0` (`TickFlameEmber_00479BC0`), la braise que
sèment les élémentaires de « Flamme » et de « Terre ».

### Symptôme

La braise porte un éclaircissement progressif : `+0x38` gagne `0x202020` par image tant qu'il reste
sous `0xe0e0e0`. **Il ne s'applique jamais.** La braise garde d'un bout à l'autre la teinte que son
parent lui a donnée à la naissance.

### Cause

`+0x38` n'est pas une couleur pleine : les vingt-quatre bits bas sont la teinte, l'octet de poids
fort porte un indicateur d'affichage. L'élémentaire lui-même le sait — `FUN_00479490`, pour le même
champ, fait son test **masqué** :

```c
if ((tint & 0x00ffffff) < 0x00f0f0f0) tint += 0x00080808;   // 0x00479b21
```

La braise, écrite pour le même champ, fait le sien **sur le mot entier** :

```asm
00479bf9  mov       ecx,[eax+38h]
00479bfe  cmp       ecx,0E0E0E0h        ; pas de masque
00479c07  jae       short 00479C12h     ; -> on saute l'addition
```

Or les **deux** semeurs de braise la font naître avec `+0x38 = 0x01000000` — la branche « Flamme »
et la branche « Terre » de `FUN_00479490` écrivent toutes deux cette constante. `0x01000000` est
au-dessus de `0x00e0e0e0` dès la première image : la comparaison non signée est vraie tout de
suite, et l'addition est **du code mort pour les deux seuls appelants qui existent**.

### Ce que ça change en jeu

La braise reste noire au lieu de virer au clair en cinq images. C'est visible sur l'élémentaire de
feu et sur celui de terre pendant les vingt images de leur levée, et nulle part ailleurs — c'est le
seul emploi du sous-type `0x57`.

### Ce que ce port en fait

**Il le reproduit tel quel** : la comparaison est écrite sans masque, exactement comme le binaire,
avec la note qui dit pourquoi elle ne passe jamais.

### Où porterait le correctif

Masquer le poids fort dans le test de la braise, comme le fait déjà l'élémentaire :
`if ((tint & 0x00ffffff) < 0x00e0e0e0)`.

---

## LB-029 — Le trait des sorts de monstre appelle un balayage avec trois arguments sur quatre

**Relevé** le 2026-09-01 en transcrivant `FUN_004478f0` (`TickMonsterBolt_004478F0`), le trait que
lance le sort de la famille 4.

### Symptôme

Quand le trait cherche une victime du côté des entités de scène, il appelle `FUN_00519af0` — le
balayage de la grille spatiale — **en n'empilant que trois arguments**. La fonction en lit
**quatre**. Le quatrième vaut ce que la pile portait déjà.

### Cause

Le site d'appel, à `0x00447a36` :

```asm
00447a36  mov       ecx,[esi+74h]
00447a39  lea       eax,[esp+10h]
00447a3d  push      eax              ; probe
00447a3e  push      edi              ; position
00447a3f  push      ecx              ; entité
00447a40  call      00519AF0h
00447a45  add       esp,0Ch          ; <- TROIS arguments, pas quatre
```

Les six autres sites d'appel du dépôt en passent quatre — `0` ou `0x800` selon le cas. Et le
quatrième sert : c'est lui qui décide si la **parade** compte.

```csharp
if ((*(uint*)found & 0x10) != 0 && (flags & 0x800) == 0 …   // 0x00519af0
```

L'emplacement du quatrième argument, à `esp+0x10` vu du callee, tombe sur le mot où le prologue de
`FUN_004478f0` a sauvé **`edi`** — le registre tel qu'il arrivait de l'appelant de la passe
d'entités. Ce n'est ni nul ni constant : ça dépend de ce que la fonction précédente a laissé.

### Ce que ça change en jeu

**Une victime en parade est parfois touchée quand même.** Si le mot laissé sur la pile porte le bit
`0x800`, le test de parade est sauté et le trait passe au travers ; sinon il se comporte
normalement. Le résultat dépend du chemin d'exécution qui a précédé, donc il n'est pas reproductible
d'une image à l'autre. C'est le même genre de défaut que LB-008 et LB-021 : une décision prise sur
un mot de pile que personne n'a écrit.

**Aucune observation en jeu**, et pour une bonne raison : ce sort appartient aux familles hors école,
celles que seuls les monstres lancent, et la sonde ne l'a encore jamais vu partir.

### Ce que ce port en fait

**Il passe `0`.** Reproduire fidèlement demanderait de simuler la pile de l'appelant, ce que la
transcription ne fait nulle part ailleurs et ce qui n'aurait aucun sens ici — la valeur d'origine
n'est pas une donnée, c'est un accident. `0` est la valeur qu'ont **six des sept** sites d'appel, et
c'est la branche qui respecte la parade. **Divergence déclarée**, notée ici et en commentaire sur le
site d'appel.

### Où porterait le correctif

Empiler `0` avant les trois autres, et corriger le `add esp,0Ch` en `add esp,10h`.

---

## LB-030 — Le souffle du Méchant ours fait clignoter tout l'écran en rouge

**Constaté** le 2026-09-15 par Vincent dans OpenForesia, au combat contre le Méchant ours (forêt,
sous-zone 5, « Le lieu de coupe »), puis **confirmé sur le jeu officiel** : même clignotement.

### Symptôme

Pendant le souffle — l'attaque de zone aux grands cercles —, **l'écran entier passe au rouge sombre
une image sur deux**, pendant trente images. L'effet est agressif pour les yeux, et d'autant plus
que l'écran rafraîchit vite (voir plus bas).

### Cause

Deux pièces, dont aucune n'est fautive seule.

**1. L'effet pousse le voile plein écran une image sur deux.** Le gestionnaire de sous-zone
`0x00413330` (`EntityHandler_00413330`, `WorldMapTick.cs`), qui accompagne le souffle, écrit le
voile `DAT_005de024` à chaque image :

```
0041343a  mov   byte [5DE027h],1     ; voile armé
00413441  test  al,1                 ; al = +0x12, le compteur d'images
00413443  mov   [5DE024h],cl         ; rouge = 0x1e ...
00413449  jne   00413451h
0041344b  mov   [5DE024h],bl         ; ... ou 0 une image sur deux
```

Le vert et le bleu restent à zéro. Une image sur deux le voile vaut donc `0x0100001e`, l'autre
`0x01000000` — couleur nulle, et `FUN_0051f940` ne dessine alors rien.

**2. Le voile est un rectangle « semi-transparent » que la DLL PC peint opaque.**
`FUN_0051f940` construit une primitive PlayStation de code **`0x62`** — rectangle plat,
**semi-transparent** — avec la page `0xe1000200` (mode de mélange 0, moitié-moitié). Sur la console,
c'était une teinte rouge légère posée sur la scène.

Mais `winsys3d.dll` range `0x60`, `0x61`, `0x62` et `0x63` dans **le même cas** du répartiteur de
`DrawOTag` (`FUN_00a13400`), servi par `FUN_00a10690`, dont la première chose est
`FUN_00a03db0(1)` : `ALPHABLENDENABLE = 0`, `SRCBLEND = ONE`, `DESTBLEND = ZERO`. **Le bit de
semi-transparence est ignoré** et le rectangle est peint opaque. Chaîné en `ot[0]` — le dernier
maillon parcouru —, il recouvre toute la scène.

C'est donc un **défaut du portage PC** : la semi-transparence du voile a été perdue dans la DLL, et
un effet pensé comme une pulsation rouge devient un stroboscope plein écran.

### Portée

Tous les voiles plein écran passent par `FUN_0051f940` et donc par ce même rectangle opaque — le
fondu de transition (`EntityHandler_0040EDF0`), les éclairs clairs (`0x01b4c896`), le voile rouge
sombre `0x019696ff`. Le stroboscope du Méchant ours est simplement le seul relevé qui **alterne**
d'une image à l'autre, et c'est l'alternance qui rend l'opacité insupportable.

**Le Labyrinthe des morts porte le même stroboscope** (relevé le 2026-09-18) : `0x0041FCA0`
(sous-zones 5 à 8), quand son `+0x9a` vaut 0, exécute la même branche — sortie de
`EntityHandler_00413330` en `StepCarrierStrobe(e, spawn)` pour servir aux deux. Le symptôme y est
donc attendu à l'identique, et la déviation éventuelle se ferait en un seul endroit.

### Ce que ce port en fait

**Il le reproduit tel quel** : la transcription de `0x00413330` et de `FUN_0051f940` est fidèle, et
la DLL d'origine fait le reste.

Une aggravation propre à OpenForesia : **le jeu n'a pas de limiteur de cadence**. Il avance d'une
image par `Present(1, 0)`, c'est-à-dire au rafraîchissement de l'écran ; le jeu de 1998 était calé
sur 60 Hz. Sur un écran à 120 ou 144 Hz, les trente images du stroboscope défilent deux fois plus
vite ou davantage (réserve déjà notée dans `Host/D3D11Backend.cs`, `Present`).

### Où porterait le correctif

Deux endroits possibles, du plus au moins fidèle :

- **Côté hôte** (non tenu à la fidélité) : honorer le bit `0x02` des codes `0x60..0x63` en posant le
  mélange du mode de la page au lieu de l'opaque — c'est-à-dire rendre au voile le comportement de la
  console. Il faudrait intercepter avant `FUN_00a10690`, la DLL ne transmettant plus l'information au
  périphérique.
- **Côté jeu** : dans `EntityHandler_00413330`, ne pas remettre le rouge à zéro une image sur deux.
  Ce serait une déviation du binaire, à déclarer.

Indépendamment, un limiteur à 60 images par seconde dans `Present` supprimerait l'aggravation liée
aux écrans rapides.

---

## LB-031 — Le projectile à tête chercheuse du Labyrinthe déréférence la cible qu'il vient d'effacer

**Repéré le 2026-09-18**, en transcrivant `0x00484190` (le sous-type `0xda`, créneau 26 du bloc
`0x00558ba8`, table `0x0056b1c8`, Labyrinthe des morts, sous-zones 4 et 9). La fonction est
**absente de `Blaze.c`** ; le défaut ne se voit qu'au désassemblage.

### Ce que fait le binaire

À chaque image, tant qu'il n'est pas verrouillé, le projectile relit sa cible `+0x78` et vérifie
qu'elle est encore en vie :

```
004841df  mov   ebp,[esi+78h]           ; ebp = la cible
004841ec  cmp   ebp,eax                 ; eax = 0
004841fa  je    00484281                ; pas de cible : on file droit
00484205  test  dword [ebp],50000000h
0048420c  je    0048421C                ; vivante : on s'inscrit chez elle
0048420e  mov   [ebp+74h],eax           ; morte : on défait son lien vers nous,
00484211  xor   ebp,ebp                 ;         on oublie la cible,
00484213  mov   [esi+78h],eax           ;         on défait notre lien vers elle,
00484216  mov   [esp+44h],ebp
0048421a  jmp   0048421F                ; ... et on saute DANS le calcul de visée
LAB_0048421c:
0048421c  mov   [ebp+74h],esi
LAB_0048421f:
0048421f  mov   edi,[ebp+1Ch]           ; lecture a l'adresse 0x1c quand ebp vaut 0
```

Le chemin « cible morte » fait tout ce qu'il faut pour oublier la cible, puis rejoint le chemin
« cible vivante » **au lieu du chemin « pas de cible »** (`0x00484281`), qui était juste à côté. Le
calcul de visée lit alors `[0 + 0x1c]`, puis appelle `FUN_00538bc0` sur un pointeur nul.

### Portée

Le chemin demande qu'une cible meure — bit `0x40000000` ou `0x10000000` — pendant que le
projectile vole encore vers elle **sans l’avoir verrouillée**. Rien ne l’empêche en jeu. Sous
Windows 9x comme sous NT, la page zéro n’est en principe pas lisible : le jeu d’origine devrait tomber sur une
violation d'accès. Le défaut est **tracé, pas observé**.

### Dans OpenForesia

`EntityHandler_00484190` le transcrit tel quel : la cible est mise à `null`, puis la visée la
déréférence. Comme pour LB-007 : **ne pas rediriger le saut vers le chemin sans cible**. Le faire
serait probablement l'intention de l'auteur, mais c'est une déviation, et elle se déclarerait ici.

## LB-032 — Le second coup de la Sacuba ne part jamais

**Repéré le 2026-09-18**, en transcrivant `0x004D9D90` (la Sacuba, château de l'immortel,
sous-zones 5 et 11) et ses trois copies `0x004F40A0`, `0x004D3EE0`, `0x004FBA80`. La fonction est
**absente de `Blaze.c`** ; le défaut ne se voit qu'au désassemblage.

### Ce que fait le binaire

Le bloc du coup contient deux frappes : l'une à l'image 13 (clignotement 3), l'autre à l'image 19
(clignotement `0x10`, puis l'effet d'état une fois sur trois). La seconde est écrite **à
l'intérieur** de la garde de la première :

```
004d9f74  mov   ebx,0Dh
004d9f79  cmp   [esi+1Ah],bx            ; image 13 ?
004d9f7d  jne   004DA03E                ; non : on saute les DEUX frappes
004d9f8c  call  00519C30                ; la sonde
...                                     ; frappe de l'image 13
LAB_004d9fda:
004d9fe3  cmp   word [esi+1Ah],13h      ; image 19 ?  -- on sait deja qu'elle vaut 13
004d9fe8  jne   004DA03A
...                                     ; frappe de l'image 19
004da032  call  004DAC80                ; l'effet d'etat, 33 fois sur 100
```

Entre les deux comparaisons, rien n'écrit `+0x1a` : ni `FUN_00539b80` (vérifié dans `Blaze.c`), ni
le son. La seconde comparaison est donc toujours fausse.

### Portée

La seconde frappe est **le seul moyen qu'a la Sacuba d'appliquer un effet d'état au corps à
corps**. Dans le jeu vendu, elle ne frappe qu'une fois par geste et n'empoisonne jamais au contact.
Le défaut est **tracé, pas observé** : à confirmer en jeu en regardant si un personnage touché par
une Sacuba reçoit un jour une altération.

### Dans OpenForesia

`TickSceneEntityAi_004D9D90` (`Ai/ImmortalCastle/Sacuba.cs`) le transcrit tel quel, garde comprise.
**Ne pas sortir le second bloc de la garde** : c'est vraisemblablement l'intention de l'auteur,
mais ce serait une déviation.

## LB-033 — `LoadBitmapFile` ferme deux fois le même handle sur le chemin compressé

**Où** : `wsyskern.c`, `LoadBitmapFile` @ `0x10010500` (export 115) et `FUN_10003260` @ `0x10003260`.

Quand le fichier n'est pas un `.BMP` brut, `LoadBitmapFile` le confie à `FUN_10003260`, qui lit le
fichier en entier puis appelle `CloseHandle` lui-même. De retour, `LoadBitmapFile` rappelle
`CloseHandle` sur le même handle :

```c
psVar3 = (short *)FUN_10003260(hFile);   /* ferme deja hFile */
CloseHandle(hFile);                      /* une seconde fois */
```

**Conséquence** : aucune, en pratique. Sous Win32, fermer un handle déjà fermé échoue avec
`ERROR_INVALID_HANDLE` et le code ne teste pas le retour. Le seul risque théorique serait qu'un
autre thread se voie attribuer la même valeur de handle entre les deux appels — le jeu ne charge
pas d'image depuis deux threads.

**Reproduit tel quel** dans `src/Libraries/wsyskern/Fichiers.cs`. Le chemin `BM` direct, lui, ne
ferme qu'une fois : la double fermeture ne concerne que les fichiers compressés.

## LB-034 — Le mode « stocké » du décompresseur est inatteignable

**Où** : `wsyskern.c`, `FUN_10003480` @ `0x10003480`.

Le pilote de décompression porte deux chemins : décoder par Huffman, ou **recopier tel quel** quand
le code de format vaut `'0'` (`_Val == 0x30`). Mais la garde placée juste avant n'accepte que trois
codes :

```c
pcVar1 = _strchr("45x", _Val);
if (pcVar1 != (char *)0x0) { ... if (_Val == 0x30) { /* recopie directe */ } ... }
```

`strchr("45x", '0')` rend `NULL`, donc un fichier marqué `'0'` est refusé avant d'atteindre la
branche qui saurait le traiter. **La recopie directe n'est donc jamais exécutée** : c'est du code
mort dans le binaire vendu, vraisemblablement un mode retiré de l'outil de compression sans que la
garde soit mise à jour.

**Reproduit tel quel** : la branche est transcrite dans `Codec.cs` avec sa garde, parce que ce n'est
pas à la transcription de décider si c'est un oubli.

## LB-035 — L'ordinal 29 de `winsys3d` renomme un fichier en lui-même

**Où** : `winsys3d.c`, `FUN_00a04700` @ `0x00a04700` (ordinal 29), et son appelée
`FUN_00a03f80` @ `0x00a03f80`.

`FUN_00a03f80` résout un descripteur de carte mémoire en chemin Win32, **dans un casier statique**
— `DAT_00dedc58`, 0x100 octets — dont il rend l'adresse. L'ordinal 29 l'appelle deux fois de
suite, une par descripteur :

```c
lpExistingFileName = (LPCSTR)FUN_00a03f80(param_1);   /* ecrit DAT_00dedc58 */
lpNewFileName      = (LPCSTR)FUN_00a03f80(param_2);   /* le REECRIT */
MoveFileA(lpExistingFileName,lpNewFileName);          /* deux fois le meme pointeur */
```

Les deux pointeurs valent `&DAT_00dedc58`, et le contenu est celui de la **seconde** résolution.
`MoveFileA` reçoit donc deux fois le nom d'arrivée, et renomme ce fichier en lui-même — ou échoue
s'il n'existe pas encore, ce qui est le cas nominal d'un renommage.

**Conséquence** : aucune, parce que rien n'appelle cet ordinal. Ni `Blaze.c` ni la reconstruction
ne l'importent, et c'est vraisemblablement la raison : le défaut aurait sauté aux yeux au premier
essai. Le même piège guette l'ordinal 30 (`SetCurrentDirectoryA`), qui n'a qu'un descripteur et
s'en tire donc indemne.

**Reproduit tel quel** dans `src/Libraries/winsys3d/CarteMemoire.cs` : les deux appels y sont, dans
l'ordre, sur le même casier. Le correctif, s'il en fallait un, tiendrait en une copie du premier
chemin dans un tampon local avant la seconde résolution.

## LB-036 — `LINE_G3` et `LINE_G4` ne tracent que leur premier segment

**Où** : `winsys3d.c`, `FUN_00a0eaf0` @ `0x00a0eaf0` (`LINE_G3`, codes 0x58..0x5b) et
`FUN_00a0ecf0` @ `0x00a0ecf0` (`LINE_G4`, codes 0x5c..0x5f). **Trouvé à la lecture** le
2026-09-23, en portant les primitives GPU — pas constaté en jeu.

Une ligne brisée PlayStation de trois ou quatre points devient chez Direct3D un
`DrawPrimitive(2 = D3DPT_LINELIST, ..., 3 sommets)` : `push 3 ; push 2` à `0x00a0ec7d` et à
`0x00a0eed1`. Une *liste* de lignes prend les sommets deux par deux ; avec trois sommets, elle en
trace **un** segment, du sommet 0 au sommet 1, et ignore le troisième. Leurs jumelles à couleur
unique, `LINE_F3` et `LINE_F4`, passent bien `3 = D3DPT_LINESTRIP`. `LINE_G4` n'écrit d'ailleurs
que trois positions pour quatre couleurs.

**Conséquence** : non observée. Toute ligne brisée dégradée que le jeu enverrait perdrait tous ses
segments sauf le premier ; reste à savoir si le jeu en envoie.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Primitives.cs` (`LineG3`, `LineG4`). Le
correctif tiendrait en un type de primitive `LINESTRIP`, et une quatrième position pour `LINE_G4`.

## LB-037 — `POLY_G4` échange les couleurs de deux coins

**Où** : `winsys3d.c`, `FUN_00a0ff50` @ `0x00a0ff50` (`POLY_G4`, codes 0x38..0x3b). **Trouvé à la
lecture** le 2026-09-23 — pas constaté en jeu.

La PlayStation range un quadrilatère en « Z » ; Direct3D, en éventail, veut le tour du contour. Le
corps croise donc les sommets 2 et 3… **pour les positions seulement** : le sommet D3D 2 prend la
position du sommet PS 3 (mots 0x20/0x22, `fstp [11CB178h]` à `0x00a10175`) mais la couleur du
sommet PS 2 (octets 0x14..0x16, `mov [11CB188h]` à `0x00a1009f`), et le sommet D3D 3 l'inverse.
Le dégradé sort avec les couleurs de deux coins échangées.

`POLY_GT4` (`FUN_00a101c0`, `FUN_00a12350`) croise, lui, couleur, texture et position ensemble :
le défaut est propre à la variante non texturée.

**Conséquence** : non observée. Un quadrilatère uni n'en montre rien ; seul un dégradé dont les
deux coins en question diffèrent le trahit.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Primitives.cs` (`PolyG4`). Le correctif
tiendrait en l'échange des octets de couleur 0x14 et 0x1c.

## LB-038 — Le sprite tourné de `winsys3d` prend la largeur pour la hauteur de texture

**Où** : `winsys3d.c`, `FUN_00a0e0c0` @ `0x00a0e0c0` (opcode 0xe5 du répartiteur). **Trouvé à la
lecture** le 2026-09-23 — pas constaté en jeu.

Le paquet porte une largeur (mot `+8`) et une hauteur (mot `+0xa`), et la géométrie emploie bien
les deux. Mais les coordonnées de texture s'étendent de `v` à `v + largeur`, pas `v + hauteur` :
`add ecx,eax` à `0x00a0e4f3` et `0x00a0e51e`, où `eax` est le mot `+8` chargé plus haut
(`mov ax,[esi+8]`). Ghidra le montre aussi (`uVar8`, la largeur, dans les deux branches en v).

**Conséquence** : non observée. Un sprite carré n'en montre rien ; un sprite plus haut que large
écrase sa texture verticalement, un plus large l'étire.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Primitives.cs` (`SpriteTourne`). Le correctif
tiendrait en la hauteur à la place de la largeur dans les deux lectures de la table `v`.

## LB-039 — Une cellule `0xffff` fige le fond en tuiles

**Où** : `winsys3d.c`, `FUN_00a142a0` @ `0x00a142a0` (opcode 0xe8 du répartiteur). **Trouvé à la
lecture** le 2026-09-23 — pas constaté en jeu.

La lecture de la carte de cellules est une boucle qui relit le même mot tant qu'il vaut `0xffff` :

```
00a144f5  ... mov ax,[esi+edx*2]      ; carte[ligne * largeur + colonne]
00a14513  cmp eax,0FFFFh
00a14518  je  short 00A144F5h         ; ligne, colonne : inchangées
```

Rien ne change d'un tour à l'autre. Une carte qui contiendrait ce code — vraisemblablement prévu
pour « cellule vide, à sauter » — bloquerait l'image pour toujours.

**Conséquence** : non observée ; les cartes du jeu n'en contiennent sans doute pas, sans quoi le
jeu se figerait. À vérifier si un fond ne s'affiche jamais.

**Reproduit tel quel** dans `src/Libraries/winsys3d/FondTuiles.cs`. Le correctif serait de passer
à la cellule suivante, en avançant la colonne.

## LB-040 — Une texture 16 bits en mode soustractif noircit la fin de ses lignes

**Où** : `winsys3d.c`, `FUN_00a03460` @ `0x00a03460` (la conversion d'une page de texture en image
DIB, chemin 16 → 24 bits). **Trouvé à la lecture** le 2026-09-23 — pas constaté en jeu.

Quand la CLUT porte le marquage `0x8000` — le mode de semi-transparence soustractif —, un pixel
dont le bit 15 est posé doit sortir noir. Le binaire l'écrit ainsi :

```
00a0364d  test ah,80h
00a03650  jne  00A0367Bh              ; pixel « transparent »
...                                   ; chemin normal : trois octets, puis add esi,2
00a0367b  mov dword [edi],0           ; QUATRE octets à zéro sur un pixel de TROIS
00a03681  add edi,3                   ; la destination avance... pas la source
```

Deux défauts en un : l'écriture de 32 bits déborde d'un octet sur le pixel suivant (ou, pour le
dernier d'une ligne, sur la ligne voisine de l'image montante) ; et surtout **`esi` n'avance pas**.
Le pixel suivant relit le même pixel transparent, et ainsi de suite : **toute la fin de la ligne
sort noire**, à partir du premier pixel semi-transparent.

**Conséquence** : non observée. Elle ne touche que les pages 16 bits directes employées en mode
soustractif, sur un device de moins de 16 bits ou pour une page marquée 0x1000 — un cas rare.

**Reproduit tel quel** dans `src/Libraries/winsys3d/ChargementTextures.cs` (`Dib24`). Le correctif
tiendrait en un `add esi,2` dans les deux branches et une écriture de trois octets.

## LB-041 — Un quadrilatère TMD plat sans éclairage ne trace qu'un triangle

**Où** : `winsys3d.c`, `FUN_00a08510` @ `0x00a08510` (le producteur du mode TMD `0x29`, F4 sans
source de lumière). **Trouvé à la lecture** le 2026-09-23 — pas constaté en jeu.

Les trois autres producteurs de quadrilatères (`F4` éclairé, `G4`, `GT4`, `FT4`) rangent leurs
quatre sommets dans l'ordre 0, 1, 3, 2 et émettent la commande **6** — `D3DPT_TRIANGLEFAN`. Celui-ci
range ses sommets de la même façon, mais émet la commande **4**, `D3DPT_TRIANGLELIST` :

```
00a087aa  mov [0A2C578h],edi        ; edi = 4
00a087b0  mov [edx+ecx*4],edi       ; l'opcode du DrawPrimitive
...
00a08828  add edx,edi               ; compte += 4 sommets
```

Une liste de triangles à quatre sommets n'en trace qu'un — les sommets 0, 1 et 3 — et le quatrième
est ignoré : **la moitié du quadrilatère manque**. Et comme la commande est écrite sans consulter
le mémo, deux F4 sans lumière de suite font deux enregistrements, là où les triangles s'allongent.

**Conséquence** : non observée. Elle ne touche que les faces F4 non éclairées d'un modèle TMD, ou
les paquets construits par les adaptateurs des genres 5 et 12/13 de l'ordinal 752.

**Reproduit tel quel** dans `src/Libraries/winsys3d/ConstructionPaquets.cs`
(`F4SansLumiere_00a08510`). Le correctif tiendrait en une constante : 6 au lieu de 4.

## LB-042 — Les producteurs TMD texturés mettent toujours u = v = 0

**Où** : `winsys3d.c`, les huit producteurs texturés des modes TMD `0x24`, `0x25`, `0x2c`,
`0x2d`, `0x34`, `0x35`, `0x3c`, `0x3d` (`FUN_00a08850`, `08c80`, `094d0`, `09070`, `0a1a0`,
`0a670`, `0b390`, `0b930`). **Trouvé à la lecture** le 2026-09-23 — pas constaté en jeu.

Chaque coordonnée de texture part d'un octet de la primitive (0 à 255) et passe par une
**division entière** que le compilateur a faite en multiplication :

```
00a08af5  mov  eax,3D2C183Dh
00a08b09  mov  dl,[esi+4]          ; u0, un octet
00a08b0c  imul edx                 ; edx = (u0 * 0x3D2C183D) >> 32
00a08b0e  sar  edx,19h
00a08b13  shr  eax,1Fh ; add edx,eax
00a08b23  fild dword [esp+10h]
00a08b27  fstp dword [ecx+18h]     ; tu du premier sommet
```

Le diviseur que donnent ces constantes est voisin de **140 422 000** — elles varient d'un
producteur à l'autre (0x3D2C183D, 0x7A582FCB, 0x1E960961, 0x0F4AFC53...), mais toutes tombent
là. Pour un octet, le quotient vaut toujours **0**. Ghidra ne s'y trompe pas : il replie le
calcul et écrit `0.0`.

**Conséquence** : non observée. Un modèle TMD texturé converti par ce chemin tirerait toutes ses
faces du texel (0, 0) de sa page — une couleur unie. Que les modèles du jeu s'affichent texturés
laisse penser qu'ils passent ailleurs (les primitives GPU du répartiteur, qui portent leurs `u v`
en clair) ; c'est à confirmer.

**Reproduit tel quel** dans `src/Libraries/winsys3d/ConstructionPaquets.cs`
(`QuotientMagique`), constante par constante. L'intention probable était `octet / 256.0f`.

## LB-043 — La manette 2 ignore son joystick et hérite des boutons de la manette 1

**Où** : `winsys3d.c`, `FUN_00a049c0` @ `0x00a049c0` (la lecture des manettes, appelée en fin
d'image par `PutDispEnv`, ordinal 657). **Trouvé à la lecture** le 2026-09-23 — pas constaté en jeu.

Deux défauts dans la même fonction, qui ne touchent que la seconde manette :

```
00a04a39  call joyGetPos(0, &joy)
00a04a3d  je   00A04A56h            ; manette 1 : valeurs neutres si la lecture ÉCHOUE
...
00a04cf9  call joyGetPos(1, &joy)
00a04cfd  jne  00A04D1Ah            ; manette 2 : valeurs neutres si la lecture RÉUSSIT
```

**Le test du joystick 2 est inversé.** Branché, il est remplacé par les valeurs neutres : ses
axes et ses boutons ne comptent jamais. Absent, `joyGetPos` échoue sans remplir la structure,
qui garde ce que le joystick 1 y a laissé — **la manette 2 recopie le premier joystick**.

**Les bits ne sont pas remis à zéro entre les deux manettes.** Les deux s'accumulent dans `bx`
(`or bh,80h` à 0x00a04d4f, sur le registre qui porte encore la manette 1) : chaque bouton pressé
sur la manette 1 l'est aussi sur la manette 2.

**Conséquence** : non observée. En jeu à un joueur, sans effet visible si la manette 2 n'est pas
lue ; à deux, le second joueur ne pourrait jouer qu'au clavier, et ses personnages recevraient
aussi les commandes du premier.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Manettes.cs`. Le correctif tiendrait en un
`je` au lieu du `jne` et une remise à zéro de `bx` avant la seconde lecture.

## LB-044 — `ClearImage` décale la composante bleue au lieu d'assembler la couleur

**Où** : `winsys3d.c`, `FUN_00a05e90` @ `0x00a05e90` (ordinal 665, `ClearImage(RECT*, r, g, b)` de
la `libgpu`). **Trouvé à la lecture** le 2026-09-23 — sans effet dans le jeu.

La couleur de remplissage devrait être un pixel 15 bits, `(b >> 3) << 10 | (g >> 3) << 5 | r >> 3`.
Le binaire calcule autre chose :

```
00a05e90  mov cl,[esp+0Ch]         ; g
00a05e94  mov eax,[esp+10h]        ; b
00a05e98  add ecx,0Ah
00a05e9b  and eax,0FFh
00a05ea0  shl eax,cl               ; b << (g + 10)
00a05ea2  mov cl,[esp+8]           ; r
00a05ea7  add ecx,5
00a05eb0  shl eax,cl               ; ... << (r + 5)
```

Les deux autres composantes servent de **décalages** à la bleue (masqués à cinq bits par le
processeur). Le résultat est ensuite doublé dans les deux moitiés d'un mot de 32 bits.

**Conséquence** : aucune. Le seul appel du jeu (`FUN_00529eb0`, à l'initialisation de la vue)
efface en `(0, 0, 0)`, et `0` décalé reste `0`. Toute autre couleur sortirait fausse.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Vram.cs` (`ClearImage`).

## LB-045 — `OpenEvent` n'enchaîne pas le nouvel événement : seul le premier est jamais livré

**Où** : `winsys3d.c`, `FUN_00a040d0` @ `0x00a040d0` (ordinal 6, `OpenEvent` de la `libapi`),
et ce qu'en voit `FUN_00a04270` (ordinal 12, `DeliverEvent`). **Trouvé à la lecture** le
2026-09-23 — sans effet visible.

Un événement est un nœud de sept mots, dont `[5]` le précédent et `[6]` le suivant. `OpenEvent`
remplit le nouveau nœud et l'ajoute en fin de liste :

```
00a04104  mov ecx,[0DEDC20h]        ; l'ancien dernier
00a0410a  mov [eax+14h],ecx         ; nouveau[5] = ancien dernier
00a0410d  mov dword [eax+18h],0     ; nouveau[6] = 0
...
00a04123  mov [0DEDC20h],eax        ; dernier = nouveau
```

mais n'écrit jamais `ancien[6] = nouveau`, et aucun autre corps de la famille ne le fait.
`DeliverEvent` part de la tête et suit `[6]` : il ne voit que le **premier événement ouvert**.
Si celui-ci est fermé (ordinal 7), la tête devient son suivant, nul, et la liste paraît vide alors
que d'autres événements sont ouverts.

**Conséquence** : aucune observée. Les seuls événements livrés en jeu sont les fins
d'entrée-sortie de la carte mémoire (ordinaux 67, 68, 69), et `TestEvent` (9) les déclare arrivées
d'office, quel que soit l'état du nœud — la carte n'attend donc jamais une livraison perdue.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Evenements.cs`.

## LB-046 — La projection MMX tronque la translation à 16 bits et arrondit deux fois

**Où** : `winsys3d.c`, `FUN_00a22010` @ `0x00a22010` (ordinal 418) et sa jumelle `FUN_00a226c0`
@ `0x00a226c0` (ordinal 795), la projection des sommets de modèle par lots. **Trouvé à la
lecture** le 2026-09-23 — effet non observé.

Les deux corps ont une branche scalaire et une branche MMX, choisies par `MMXInstalled`, que
l'initialisation pose à 1 sur tout processeur postérieur au Pentium MMX. **Les deux branches ne
calculent pas la même chose.** La scalaire fait, par composante :

```
x = (m0.vx + m1.vy + m2.vz) >> 12  +  tx          ; un arrondi, tx sur 32 bits
```

La MMX met la translation dans le quatrième mot du `pmaddwd`, en face d'un `0x1000` constant
(`DAT_00a2a0d0`), puis décale chaque moitié avant de les additionner :

```
00a225ad  movd  mm3,[esi+14h]      ; tx
00a225bb  psllq mm3,30h            ; ... ses 16 bits BAS seulement, dans le mot 3
00a22615  pmaddwd mm4,mm0          ; (vx.m0 + vy.m1 , vz.m2 + 0x1000.(short)tx)
00a2261b  psrad mm4,0Ch            ; chaque moitié décalée
00a22630  paddd mm3,mm4            ; puis additionnée
```

soit `((vx.m0 + vy.m1) >> 12) + ((vz.m2) >> 12) + (short)tx`. Deux écarts :

- **la translation perd ses bits hauts** : une composante hors de ±32 767 repart de l'autre côté ;
- **deux arrondis au lieu d'un** : le résultat peut valoir un de moins que la branche scalaire.

**Conséquence** : aucune constatée. Le second écart déplace un sommet d'au plus une unité avant la
perspective. Le premier ne mordrait que sur un modèle placé à plus de 32 767 unités de la caméra
sur un axe, ce qui le sortirait vraisemblablement de l'écran de toute façon — non vérifié.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Matrices.cs`, les deux branches, avec le
drapeau aliasé : c'est la MMX qui tourne.

## LB-047 — Le journal des attentes d'image déborde à la 100 000ᵉ image sur les globales de la cadence

**Où** : `winsys3d.c`, `FUN_00a053e0` @ `0x00a053e0` (ordinal 132, le `VSync(n)` de la `libgpu`),
que le jeu appelle **une fois par image** depuis `FrameEnd` (`Screens/State00/MainMenu.cs`).
**Trouvé à la lecture** le 2026-09-23 — non observé.

À chaque appel, 132 incrémente `DAT_011be69c` puis écrit un enregistrement de 0x28 octets (compteur
d'images, image attendue, compteur de performance, échéance, rattrapage, attente, retard, `n`) à
`0x00dedd78 + indice * 0x28`. La zone fait **exactement 100 000 enregistrements** —
`0x011be678 − 0x00dedd78 = 4 000 000` — et **l'indice n'est jamais borné ni remis à zéro** : aucune
autre écriture de `DAT_011be69c` dans toute la DLL.

Le 100 000ᵉ appel (≈ 27 min 47 s à 60 images/s) écrit donc son enregistrement **sur les globales
de la cadence elles-mêmes** :

| champ écrit | globale recouverte | conséquence |
|---|---|---|
| image attendue | `0x011be67c`, **`VSyncDelayed`** | non nul → `DrawOTag` et `PutDispEnv` sautent tout |
| compteur, mot bas | `0x011be680`, l'image attendue | valeur sans rapport |
| compteur, mot haut | `0x011be684`, **les tops par image** | quelques unités au lieu de ~166 667 |
| échéance / rattrapage | `0x011be688 .. 0x011be694` | mélangés |
| `n` | `0x011be69c`, **l'indice lui-même** | le journal repart de `n + 1` |

**Conséquence** : en **mode de cadence 0**, le mode par défaut, l'image attendue vaut 0 tant que
`SetTimingMode` n'a jamais été appelé, donc `VSyncDelayed` reçoit 0 et rien ne se voit : le mode 0
ne consulte ni les tops ni le rattrapage. **Si le joueur est passé par la page « Timing »** des
options, l'image attendue est non nulle : `VSyncDelayed` reste alors non nul pour toujours en
mode 0 — plus aucune image dessinée ni présentée, **l'écran se fige**. En modes 1 à 3, les tops
par image deviennent minuscules et la cadence est perdue. Non vérifié en jeu : il faut jouer près
de 28 minutes d'affilée.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Cadence.cs`, jusqu'à l'ordre des lectures :
les valeurs sont prises avant la première écriture, comme dans le binaire, pour que le
recouvrement produise les mêmes valeurs.

## LB-048 — Le nom du pilote 3D tient sur 20 octets, mais se copie sans borne

**Où** : `winsys3d.c`, `Setup3DDevice` @ `0x00a01910` (ordinal 823) et `FUN_00a010d0` @
`0x00a010d0`, le lanceur qui choisit le pilote. **Trouvé à la lecture** le 2026-09-23 — non observé.

Le bloc que `Setup3DDevice` tient sur sa pile et passe au dialogue réserve **20 octets** au nom du
pilote (`+4`), puis la description (`+0x18`). Deux copies y écrivent un nom **sans borne**, par
`strlen` puis `rep movsd`/`rep movsb` :

- `Setup3DDevice` y copie la clé `Driver` de `Blaze.cfg`, lue dans un tampon de 256 octets ;
- `FUN_00a010d0` y copie le nom du pilote choisi dans le dialogue.

Un nom de 20 caractères ou plus déborde sur la description. Comme la description est écrite
**juste après**, elle écrase la fin du nom et son NUL : le nom lu ensuite devient les 20 premiers
caractères suivis de la description. C'est ce texte que `Setup3DDevice` compare à « GLIDE » et
« SOFTWARE », puis réécrit dans `Blaze.cfg` sous `Driver` — le fichier garde le nom corrompu, et
le lancement suivant le relit tel quel.

**Conséquence** : aucune observée. Les noms en jeu — « Software », « Glide », et les noms de
pilote DirectDraw que le rappel d'énumération range — sont courts. Il faudrait un pilote au nom de
20 caractères ou plus, ou un `Blaze.cfg` écrit à la main.

**Reproduit tel quel** dans `src/Libraries/winsys3d/Device.cs` : les deux corps prennent leur cadre
de pile d'un seul `stackalloc`, aux mêmes décalages, pour que le débordement tombe au même endroit.

## LB-049 — La ligne PLAYER du menu de mise au point crée des membres sans fiche

**Où** : `FUN_0051aa30` @ `0x0051aa30`, cas 2 (PLAYER) — `RebuildDebugParty` dans
`Screens/State03/WorldMapDebugMenu.cs` — puis le panneau des membres, `0x005185f0`..`0x00518e60`
(`DrawPartyPanels`, `WorldMapHud.cs`). **Observé** le 2026-09-25 : Labyrinthe des morts, groupe
porté à 4 par le menu de mise au point (F1 puis F2), plantage immédiat ; **reproduit** le même jour
avec un personnage tout juste créé, à l'auberge, jamais passé par l'éditeur de sauvegarde — la
modification de la sauvegarde n'y est pour rien. **Confirmé sur le jeu officiel** par Vincent : même
plantage avec un personnage chargé depuis une sauvegarde, aucun avec le groupe de mise au point.

PLAYER change la taille du groupe, et fabrique les membres 2 à 4 en **recopiant le seul
emplacement de jeu du meneur** : l'enregistrement de `0x9c` octets en `0x005e93c0` et sa ligne de
manette. La **fiche du personnage**, elle — les `0x2000` octets de `MainMem + 0xf0000 + i * 0x2000`,
statistiques, PV, inventaire — n'est ni recopiée ni initialisée. Un membre qui n'a jamais été
chargé garde donc une fiche vide, avec **0 PV maximum**.

Le panneau suivant calcule la couleur de la jauge de vie par `PV × 255 / PV max`, sans garde
(`Blaze.c` ligne 64570) :

```c
iVar13 = (*(short *)(iVar12 + 0x14c) * 0xff) / (int)*(short *)(iVar12 + 0x148);
```

Avec un maximum nul, c'est une **division entière par zéro** : `idiv` lève `#DE` dans le jeu de
1998, `DivideByZeroException` dans le port.

**Conséquence** : la ligne n'est utilisable qu'avec un groupe dont les quatre fiches existent — le
groupe de mise au point (Gurkha + F1 maintenu), qui les initialise toutes à 9000 PV, ou quatre
personnages réellement chargés. Avec moins de personnages chargés, passer à un groupe plus grand
plante dès l'image suivante. C'est un outil de développeur pensé pour le groupe de mise au point ;
personne n'y avait accès dans le jeu vendu sans « Gurkha ».

**Reproduit tel quel** : `GaugeColour` (`WorldMapHud.cs`) divise sans garde. Un correctif porterait
sur `RebuildDebugParty` — recopier aussi la fiche du meneur — plutôt que sur la jauge.

## LB-050 — Un monstre qui cherche sa proie parmi un groupe indisponible tourne sans fin

**Où** : `FUN_004a1020` @ `0x004a1020` et ses copies par zone (dont `FUN_004bde40`, l'Ombre du
Labyrinthe) — `PickChasedPartyMember_004A1020`, `Ai/Forest/Varan.cs`, partagée par 38 appels.
**Observé** le 2026-09-25 : Labyrinthe des morts, seconde partie, joueur seul qui ressortait aussitôt
par la porte d'une salle (course avec Maj), à plusieurs reprises ; le jeu s'est figé. Pile relevée
à chaud (`dotnet-stack`), puis image mémoire.

Six fois sur dix, la fonction remplace le mode demandé par `0xffff`, « n'importe qui » : elle tire
alors un membre au hasard **jusqu'à en trouver un** présent (`0x80000000`) et disponible (ni
`0x40000000`, mort, ni `0x10000000`), sans borne :

```c
do {
  uVar9 = rand() % 4;
  if ((slot[uVar9] & 0x80000000) != 0 && (slot[uVar9] & 0x50000000) == 0) { bVar4 = true; ... }
} while (!bVar4);
```

Si aucun membre n'est éligible, la boucle ne finit jamais. L'image mémoire montre ce cas : un seul
membre, mot d'état `0x91822f00`, compteur `+0x12` à `0x14` — le personnage était **en plein
transfert de sous-zone**, l'état que la sortie marchée pose (`Blaze.c` 57354) et que le rechargement
efface (`Blaze.c` 63541). Une Ombre qui s'éveille dans cette fenêtre, pendant le fondu de sortie,
n'a personne à poursuivre.

**Non confirmé sur le jeu officiel** : la condition est fugace (il a fallu plusieurs allers-retours
rapides par la porte), et il reste possible que le jeu de 1998 gèle les entités de scène pendant la
sortie d'une façon que le port ne reproduit pas. Si c'était le cas, le défaut serait dans le port.

**Neutralisé — écart déclaré** (demande de Vincent, 2026-09-25) : faute de candidat, le monstre
garde sa cible précédente, comme les modes 3 à 7 quand personne ne correspond. Quand un candidat
existe, le tirage reste celui du binaire. Le commentaire `ECART DECLARE` dans `Varan.cs` le signale.

## LB-051 — Le sort « spirale et éclair » des boss fait clignoter tout l'écran

**Où** : trois copies du même corps — `FUN_0043b870` @ `0x0043b870` (grotte scellée, salle de la
machine, sous-zone 1 : la Mort volante ; `Combat/Projectiles.cs`), `0x0043e820` (grotte scellée,
sous-zones 13 à 15, absente de `Blaze.c` ; `Effects/SealedCaveParticles.cs`) et `0x00425e50` (château
de l'immortel, `StepCastleRedFlash`, `Effects/ImmortalCastleParticles.cs`).
**Signalé** le 2026-09-25 par Vincent, en jeu : « des cercles autour du boss, mais aussi des flashs
lumineux, presque épileptiques ».

Le corps sert trois rôles selon son `+0x14` : la **spirale** qui monte autour du lanceur (les
cercles), la **masse** qui tombe et blesse, et l'**éclair** (rôle 2). Celui-ci compte quinze images ;
aux images 10, 9, 8 et 7 il pose le voile plein cadre `DAT_005de024` en quatre rouges de plus en plus
sombres (`0x016464fa`, `0x015050b4`, `0x013c3c78`, `0x011e1e50`), à l'image 6 il le rend noir et
**l'effet d'état 11 frappe chaque membre debout**. Quatre images de rouge vif suivies d'un noir, et un
boss qui relance le sort : l'écran stroboscope.

Ce n'est pas un défaut de programmation — c'est ce que le binaire fait, transcrit fidèlement —, mais
un effet pénible, et potentiellement dangereux pour une personne photosensible.

**Neutralisé — écart déclaré** (demande de Vincent, 2026-09-25) : les quatre écritures colorées
sont gardées derrière `s_BossDebuffScreenFlash` (`Combat/Projectiles.cs`), à `false`. La spirale, la
masse, le moment où l'effet 11 frappe et l'extinction du voile à l'image 6 sont ceux du binaire, à
l'identique. Remettre `true` rend l'éclair d'origine. Le commentaire `ECART DECLARE` le signale aux
trois endroits.

**Deuxième famille, relevée le même jour** (« Grisly le fait toujours », puis le second gros boss de la
forêt) : **le stroboscope des invocations**. Trois corps — `StepCarrierStrobe` (`0x00413330`, forêt,
sous-zones 5 et 9, où vit le Grizzly ; et `0x0041fca0`, Labyrinthe 5 à 8), `TickSummonBurstShake`
(`0x00422d50`, Labyrinthe 4 et 9) et `0x00409e40` (mine 5 à 8) — font naître onze entités
d'incantation autour du porteur (les cercles), puis posent le voile rouge (`0x1e`) **une image sur
deux pendant trente images**. C'est le vrai stroboscope. Même interrupteur : éteint, le voile garde la
valeur de ses images paires, `0x01000000` — couleur nulle, que `FillScreenQuad_0051F940` ne dessine
pas. Les invocations, le son et les naissances aux images 0, 3 et 7 sont ceux du binaire.

## LB-052 — Le voile plein cadre est dessiné opaque : un fondu vers le blanc devient un écran blanc

**Où** : le voile `DAT_005de024` (et son second, `DAT_005de028`) part en `TILE` par
`FillScreenQuad_0051F940` (commande `0x62`, page `0xe1000200`, donc semi-transparent, mode `abr` 0).
Côté DLL, `winsys3d.c FUN_00a10690` (`TILE`) passe par `PreparerEtatPlat`, qui appelle
`ChoisirMelange(1)` : **mélange coupé**, alpha `0xff`, couleur doublée. Le bit de
semi-transparence de la commande n'est pas consulté.
**Signalé** le 2026-09-26 par Vincent : « un effet lumineux blanc trop prononcé, l'écran devient
full blanc » — à la récupération d'un cristal en fin de boss, et à la première obtention de la
tablette des anciens.

Sur PlayStation, `abr` 0 mélange le voile à moitié (`B/2 + F/2`). Sur PC, il **remplace l'image par
un aplat**. Le flash de fin de zone (`FUN_004183d0`) monte le voile de `+8` par image pendant trente
images puis le redescend de `-6` : conçu comme un fondu, il donne à l'écran un aplat qui passe du
quasi-noir au **blanc plein**, puis retombe. C'est le défaut du portage PC de 1998, transcrit
fidèlement ; il explique aussi pourquoi les stroboscopes de LB-051 étaient si agressifs.

**Contourné — écart déclaré** (demande de Vincent, 2026-09-26), sans toucher la DLL reconstruite :
l'hôte Direct3D 11 gagne un **voile de confort** (`D3D11Backend.SetComfortVeil`) — une couleur posée
par-dessus l'image présentée avec un vrai alpha, valable une image. Deux effets l'emploient à la
place du voile opaque, derrière `s_ComfortWhiteFlash` (`Effects/AreaExitFlash.cs`, à `true`) :

- **le flash de fin de zone** `FUN_004183d0` (le cristal, la fin d'un boss ; 18 sous-zones de la
  forêt, de la mine, de la vieille tour et du Labyrinthe) : opacité 0 → 1 en trente images,
  1 → 0 en quarante et une, courbe lissée ;
- **la transition de la forêt** `0x0040edf0` (sous-zones 0 à 4) — **la tablette des anciens**,
  confirmé en jeu le 2026-09-26 : 0 → 1 en vingt-cinq images, 1 → 0 en vingt-cinq ;
- **la scène du cristal** — `ShiftCutsceneVeils`, partagée par les scènes de la forêt
  (`0x00410820`, `0x00413c90`, et celle à dialogue) et de la montagne du dragon (`0x0040d930`) :
  mêmes vingt-cinq images de montée (`0x82..0x9a`) et de descente (`0x9b..0xb3`). L'opacité vaut 1
  à l'image `0x9b`, celle où la scène change la pose du groupe — le blanc plein est là pour la
  cacher. Le premier correctif n'avait traité que `FUN_004183d0` : Vincent a signalé que le
  cristal restait « tout blanc », et c'était cette scène-ci.

- **le portail qui suit un boss** — la scénette de sortie `FUN_00474380` (sous-type 15, écrite dans
  `Handlers/Forest.cs` mais installée dans 23 emplacements des tables d'entités), trouvée le
  2026-09-26 par `trace-voile` sur la fin d'Eifrit (10:11) : son état 4 pousse le **second** voile
  `DAT_005de028` de `+10` pendant huit images jusqu'à `0x50`, puis de `-5` pendant treize. Vincent :
  « l'écran se met à blanc puis redevient normal, sans passage ». Même voile de confort, même
  cadence : opacité 1 à la huitième image, 0 à la vingt et unième.

Les états, sons, naissances, gel des entités et sorties de zone sont ceux du binaire. `false` rend
l'aplat d'origine. **Les autres emplois du voile restent opaques** — fondus de scène de la grotte
scellée (`0x004384f0`, `0x00440840`), sorts du Prêtre : à traiter
au cas par cas s'ils gênent.
