# TODO

Point de reprise entre deux sessions. Lire ce fichier suffit à repartir.

---

## Le problème à régler

Le menu principal est **fidèle à l'original quand il s'affiche**, mais un lancement sur trois à
sept donne un **écran entièrement noir**. Sans plantage, sans message. Comme le menu ouvre sur
d'autres menus, cette loterie rend tout test en aval impraticable.

**La reconstruction est hors de cause, c'est mesuré.** Un run visible et sept runs noirs étiquetés
par le responsable sont rigoureusement identiques : même structure du device 3D, fenêtre au premier
plan dans tous les cas, aucune surface perdue, arène placée partout — un run noir avait même
l'adresse d'origine `0x00B6DBD8`. Détail complet et pistes écartées dans
`ANALYSE_ECRAN_NOIR_MENU.md` ; ne pas les rouvrir.

Le défaut se situe en aval de `DrawOTag`, dans la présentation par `3dhelp.dll` + Direct3D 3 +
pilote. Indice supplémentaire : le taux d'échec est passé de ~1/3 à 7/8 entre deux séances **sans
changement de code** — comportement de pile graphique, pas de logique déterministe.

---

## La décision prise

Remplacer le backend de rendu. **Pas** transcrire `3dhelp.dll` fidèlement : une transcription
appellerait les mêmes API DirectDraw/Direct3D 3 dans le même ordre et hériterait du bug.

Le périmètre réel est plus petit qu'il n'y paraît : `winsys3d` ne pilote le device qu'à travers
**34 slots de vtable distincts** (184 sites d'appel). Le travail consiste donc à implémenter ces
34 méthodes derrière la même interface binaire, sur un backend moderne — un objet C# exposant une
vtable de pointeurs de fonction que `winsys3d` appelle.

Ce chantier sert aussi l'objectif de long terme : c'est exactement la couche à remplacer pour
sortir de DirectDraw.

---

## Étape 1 — cartographier les 34 slots  ← **commencer ici**

Lecture pure, aucun code. Pour chacun des 34 slots, établir depuis `3dhelp.c` :

- quelle fonction il vise et ce qu'elle fait ;
- s'il est **critique pour afficher une frame**, ou accessoire (beaucoup seront des accesseurs
  triviaux) ;
- quelles ressources il manipule (surfaces, buffers d'exécution, matrices).

Les slots appelés, extraits de `winsys3d.c` :

```
0x0c 0x10 0x18 0x20 0x24 0x28 0x2c 0x30 0x34 0x3c 0x4c 0x50 0x5c 0x60 0x6c 0x80
0x8c 0x90 0x94 0x98 0x9c 0xa0 0xa4 0xa8 0xb0 0xb4 0xb8 0xbc 0xc4 0xcc 0xd0 0xd8
0xdc 0xe8
```

Pour les résoudre : la vtable du device est en `0x0ffe6000` (`3dhelp.dll`, base d'image
`0x0FFE0000`), lisible avec

```
OpenForesia.Tools.exe at 3dhelp.dll 0x0ffe6000 256
```

Déjà connus : `+0xb0` = `FUN_0ffe2880`, récupération de surfaces perdues (`DDERR_SURFACELOST` puis
`Restore`) ; `+0xb4` = fin de scène ; `+0x5c` et `+0x60` = mise en place du contexte, rappelées par
la récupération. `Create3DDevice` (`3dhelp.c` ligne 342) alloue 200 octets et construit l'objet
dont la vtable est `PTR_FUN_0ffe6000`.

Disposition connue de l'objet device (indices en dwords) : `[0x10]` et `[0x15]` surfaces
DirectDraw, `[0x16]` surface optionnelle (nulle en pratique), `[0x17]` `IDirect3DDevice2`.

**Livrable** : un tableau des 34 slots dans `Documentation/DLL/3dhelp.md`, avec le sous-ensemble
minimal pour afficher une frame. C'est ce chiffre qui décide de la suite.

---

## Étape 2 — device C# minimal

Implémenter uniquement le chemin d'affichage identifié à l'étape 1. Tous les autres slots lèvent
une exception nommée, comme ailleurs dans le projet. On saura vite si l'image sort.

---

## Étape 3 — étoffer

Slot par slot, guidé par ce qui lève à l'exécution.

---

## Comment lancer

Build et exécution demandent des environnements **incompatibles** (l'appli est x86, le seul SDK
est x64) :

```
Build :  $env:DOTNET_ROOT="C:\Users\Sagara\.dotnet" ; dotnet build OpenForesia.sln
Run   :  DOTNET_ROOT non défini, lancer OpenForesia\bin\Debug\net9.0\OpenForesia.exe
```

Le CD doit être monté. Test automatisable, sans appui manuel :

```powershell
$sh = New-Object -ComObject WScript.Shell
$t = Get-Process -Name OpenForesia | Where-Object { $_.MainWindowHandle -ne 0 } | Select-Object -First 1
$sh.AppActivate($t.Id); Start-Sleep -Milliseconds 700; $sh.SendKeys(" ")
```

Toujours terminer par `Get-Process -Name OpenForesia | Stop-Process -Force` : les enfants relancés
survivent au parent, verrouillent l'exécutable et font échouer le build suivant.

---

## Autres chantiers en attente

- `FUN_0052a440` — dispatcher d'état de jeu, atteint quand le menu passe la main
- `FUN_00512320` — page d'options (80 l.), stub actuel
- `FUN_00523120` — création du device DirectInput, débloquerait `FUN_005048f0`
- device DirectSound (TODO #26) — pile audio reconstruite mais inerte, aucun `DirectSoundCreate` localisé
- 26 gestionnaires de codes de contrôle du moteur de texte non reconstruits (dialogue, objets,
  combat) — le menu n'en touche aucun, adresses dans `Documentation/DataSymbols.md`
