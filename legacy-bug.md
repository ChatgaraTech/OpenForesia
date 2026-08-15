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
