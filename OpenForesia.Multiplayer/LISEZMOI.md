# OpenForesia.Multiplayer — jouer à quatre à distance

`OpenForesiaMultiplayer.exe` fait jouer jusqu'à trois amis à la partie de l'hôte, par internet ou
en réseau local, **sans rien changer au jeu** : l'hôte joue le joueur 1 sur son PC, l'image et le
son du jeu partent chez les amis, et leurs manettes pilotent les joueurs 2 à 4. Aucun logiciel tiers,
aucun serveur à nous. Change : `openspec/changes/add-multiplayer-streaming` (proposition, spec,
design D1 à D8, tâches).

Hors reconstruction : du code d'aujourd'hui, aux règles de l'installeur — pas de WinForms (fenêtres
Win32 natives), COM et WinRT par appels de vtable, JSON par sérialiseur généré, publication autonome
x86 **élaguée en un seul fichier** (≈ 10 Mo), avertissements d'élagage traités en erreurs.

## Pour le joueur

Lancé sans rien (le raccourci « OpenForesia Multijoueur ») : l'**accueil**.

- **Héberger** : la fenêtre de l'hôte cherche la box (UPnP, à cocher) et lui fait ouvrir le port
  UDP 47200, affiche l'adresse publique et deux **codes d'invitation** (internet, réseau local) avec
  un bouton « Copier ». On choisit la **qualité de l'image** (Haute par défaut, Normale, Économe — le débit
  montant par ami), puis « Lancer la partie ». La fenêtre reste ouverte pendant la partie et dit
  qui est connecté ; elle referme le port à la fin.
- **Rejoindre** : on colle le code reçu ; la première fois, l'écran de la manette s'ouvre et enchaîne les
  quatorze boutons (« Continuer » quand c'est fini), et l'on joue. Alt+Entrée : plein écran.
  **F1**, à tout moment, ouvre l'écran de la manette façon émulateur : une manette PlayStation
  dessinée, une case par bouton — clic pour changer, clic droit pour effacer, « Tout reconfigurer »,
  et ce qu'on presse s'allume. Pendant qu'il est ouvert, le personnage de l'ami ne reçoit rien.
  **F2** montre la qualité du flux : images par seconde et pire écart, débit, ping, taille du flux,
  durée du décodage, pertes, son, et deux courbes (l'écart entre deux images sur 3 s, le débit sur
  30 s). **F3** cache le bandeau du haut — il revient de lui-même pour une pause ou une coupure.

Deux limites à connaître : **la fenêtre du jeu doit rester au premier plan chez l'hôte** (les amis
sont mis en pause sinon), et une box derrière une autre (double NAT : 4G, certaines fibres) ne peut
pas héberger pour internet.

## Comment ça marche

```
 PC hôte                                                         PC d'un ami
 OpenForesia.exe (inchangé)  ◄── touches fantômes (SendInput) ◄─ 14 boutons PS1, 120×/s
   fenêtre du jeu ─► capture (Windows.Graphics.Capture) ─► H.264 ─► morceaux UDP ─► décodage ─► fenêtre
   son du jeu seul ─► capture « process loopback » (WASAPI) ─► PCM 5 ms ─────────► sortie WASAPI
                         tout chiffré en AES-GCM par la clé du code d'invitation
```

- **Les entrées** (design D2) : le jeu traduit le clavier en quatre manettes PlayStation
  (`FoldKeyboardIntoPadState`). L'hôte écrit, pour les joueurs 2 à 4, des **touches fantômes** dans
  `OpenForesia.cfg` — des codes étendus qu'aucun clavier ne porte et qui ne tapent aucun caractère,
  mesurés par `--mesurer-touches` (`Documentation/Reference/OpenForesia.cfg.md`) — et les simule.
  Les touches du joueur 1 ne sont jamais touchées ; celles des joueurs 2 à 4 sont remises à la fin.
  Un compagnon IA passe au joueur quand **sa** manette presse une direction (la règle du jeu).
- **Le jeu n'est pas le processus lancé** : depuis LB-056, `OpenForesia.exe` crée le vrai jeu en
  processus enfant. Le premier plan, la fenêtre capturée et le son visent le lanceur **et ses
  enfants**.
- **L'image** (D3) : l'encodeur H.264 logiciel de Windows, synchrone, basse latence — un
  **booléen COM** (`VT_BOOL`), réglé **avant** les formats, sans quoi il garde six images en réserve.
  Avant l'encodage, la capture est **recadrée** au 4:3 du flux (en fenêtre sans bordure, la
  fenêtre du jeu fait tout l'écran, 16:9, bandes noires comprises) puis **réduite par un filtre
  triangle** (`Video/Conversion.cs`) — au plus proche voisin, les traits fins des textes sautaient.
  Chez l'ami, la fenêtre s'ouvre à la taille exacte du flux (1:1, la seule image parfaitement nette).
- **Le son** (D4) : réserve de 40 ms chez l'ami, bornée à 80 ms.
- **Le réseau** (D5, D6) : un protocole UDP à nous (`Commun/Protocole.cs`), chaque paquet scellé en
  AES-GCM (`Commun/Chiffrement.cs`), le nonce unique par un seul compteur pour tous les fils de
  l'hôte ; UPnP à la main (`Hote/Upnp.cs`) ; un ami reconnu à son identifiant, qui retrouve son
  joueur après un changement de réseau.

## Les répertoires

| Répertoire | Contenu |
|---|---|
| `Commun/` | le pad PS1, le protocole, le chiffrement, le code d'invitation, les qualités d'image |
| `Hote/` | la session, le jeu (trouvé, lancé, premier plan), les touches fantômes, le serveur, l'injection, la diffusion de l'image et du son, l'UPnP, l'hôte en console |
| `Invite/` | la connexion, les entrées (XInput, winmm, clavier), la configuration de la manette, la fenêtre et la boucle de l'ami |
| `Video/` | l'encodeur et le décodeur H.264, les conversions BGRA/NV12, les morceaux d'image |
| `Natif/` | les appels Windows : Win32, COM et WinRT, la capture, Media Foundation, WASAPI, DirectInput, les contrôles |
| `Interface/` | l'accueil et la fenêtre de l'hôte |
| `Mesure/` | les essais sans le jeu (ci-dessous) |

## Les essais sans le jeu

Aucun ne lance le jeu ; `Program.cs` décrit leurs options.

| Mode | Ce qu'il vérifie |
|---|---|
| `--mesurer-touches` | quelles touches fantômes passent de `SendInput` à DirectInput sans caractère (simule des frappes dans sa fenêtre) |
| `--essai-image [--qualite …]` | capture, encodage, morceaux, décodage d'une mire ; durées, retard des codecs, dernière image en BMP |
| `--manette` | l'écran de la manette seul (celui de F1), sans réseau : pour le voir, ou configurer sa manette hors partie |
| `--essai-son` | une note de 440 Hz jouée puis retrouvée par la capture de processus (fréquence, niveau) |
| `--essai-diffusion` | un hôte qui diffuse la mire et la note, jamais au premier plan (aucune touche simulée) ; il affiche le code à donner à `--rejoindre … --sans-manette --duree <s>` |
| `--essai-upnp` | trouve la box, ouvre le port, lit l'adresse publique, **referme** le port |

`--rejoindre` accepte aussi `--refaire-a <ms>` (simule une coupure du réseau) et
`--derniere-image <bmp>` ; `--heberger` accepte `--sans-upnp`, `--adresse-publique <ip>` et
`--sans-fenetre` (tout dans la console).

## Pièges déjà rencontrés

- Une session Windows affichée à distance plafonne la capture à sa propre fréquence (32 Hz ici) :
  compter les images de la mire dans ces conditions ne mesure pas le programme.
- `Thread.Sleep(1)` dort 15,6 ms sans `timeBeginPeriod(1)` : l'hôte et l'ami la demandent.
- L'encodeur refuse **sans bruit** un réglage mal typé : `MF.Regler` rend faux, le journal le dit.
