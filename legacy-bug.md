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
données du jeu. `Legacy/Blaze.exe`, adresse virtuelle `0x00566B74` :

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
