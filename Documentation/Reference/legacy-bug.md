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
