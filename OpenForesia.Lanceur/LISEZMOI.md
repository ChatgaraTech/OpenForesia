# OpenForesia.Lanceur — jouer seul à la manette, ou à quatre à distance

`OpenForesiaLanceur.exe` (`OpenForesiaMultiplayer.exe` jusqu'au 2026-10-02) est la porte d'entrée du
jeu, **sans rien changer au jeu** :

- **jouer seul** avec n'importe quelle manette que Windows connaît : le jeu ne lit aucune manette
  (le chemin DirectInput de 1998 n'est pas reconstruit), le lanceur la relaie au joueur 1 en touches
  fantômes ;
- **héberger** : jusqu'à trois amis rejoignent la partie, par internet ou en réseau local ; l'image et
  le son du jeu partent chez eux, leurs manettes pilotent les joueurs 2 à 4, et celle de l'hôte le
  joueur 1 ;
- **rejoindre** la partie d'un ami — avec le lanceur seul, sans le jeu ni l'ISO.

Aucun logiciel tiers, aucun serveur à nous. Changes (archivés) : `openspec/changes/archive/2026-10-02-add-multiplayer-streaming`
(le multijoueur, design D1 à D8) et `add-launcher-solo-gamepad` (le lanceur et la manette du
joueur 1, design D1 à D8).

Hors reconstruction : du code d'aujourd'hui, aux règles de l'installeur — pas de WinForms (fenêtres
Win32 natives), COM et WinRT par appels de vtable, JSON par sérialiseur généré, publication autonome
x86 **élaguée en un seul fichier** (≈ 10 Mo), avertissements d'élagage traités en erreurs.

## Pour le joueur

Lancé sans rien (le raccourci « OpenForesia Lanceur ») : l'**accueil**, avec quatre choix et une
ligne qui dit quelle manette est vue. **Sans `OpenForesia.exe` à côté** (un ami qui n'a reçu que le
lanceur), il n'offre que Rejoindre et Configurer ma manette ; le raccourci « OpenForesia », lui,
lance le jeu directement, au clavier seul.

- **Configurer ma manette** : l'écran de la manette (ci-dessous, celui de F1 chez l'ami), dans sa
  propre fenêtre. **Une seule configuration** par PC (`%APPDATA%\OpenForesia\manette.json`) : elle
  sert au joueur 1 en solo et en hébergement, et à l'ami qui rejoint depuis ce PC. Chaque changement
  est enregistré aussitôt. Le bouton est aussi dans la fenêtre de la partie solo et dans celle de
  l'hôte : on y passe par Alt+Tab en pleine partie, et le changement compte dès la fermeture de
  l'écran, sans relancer le jeu.
- **Jouer seul** : le jeu démarre, et une petite fenêtre reste ouverte tant qu'il tourne (la
  manette vue, « Configurer ma manette »). **Le clavier marche en même temps que la manette**, avec
  les touches réglées dans les Options du jeu. La manette n'agit que si la fenêtre du jeu est au
  premier plan et l'écran de configuration fermé ; sinon tout est relâché. Fermer la petite fenêtre
  arrête la manette, le jeu continue au clavier.
- **Héberger** : la fenêtre de l'hôte cherche la box (UPnP, à cocher) et lui fait ouvrir le port
  UDP 47200, affiche l'adresse publique et deux **codes d'invitation** (internet, réseau local) avec
  un bouton « Copier ». On choisit la **qualité de l'image** (Haute par défaut, Normale, Économe — le débit
  montant par ami), puis « Lancer la partie ». La fenêtre reste ouverte pendant la partie et dit
  qui est connecté et quelle manette l'hôte tient ; elle referme le port à la fin. L'hôte joue le
  joueur 1 au clavier et à la manette, comme en solo.
- **Rejoindre** : on colle le code reçu ; la première fois, l'écran de la manette s'ouvre et enchaîne les
  quatorze boutons (« Continuer » quand c'est fini), et l'on joue. Alt+Entrée : plein écran.
  **F1**, à tout moment, ouvre l'écran de la manette façon émulateur : une manette PlayStation
  dessinée, une case par bouton — clic pour changer, clic droit pour effacer, « Tout reconfigurer »,
  et ce qu'on presse s'allume. Pendant qu'il est ouvert, le personnage de l'ami ne reçoit rien.
  **F2** montre la qualité du flux : images par seconde et pire écart, débit, ping, taille du flux,
  durée du décodage, pertes, son, et deux courbes (l'écart entre deux images sur 3 s, le débit sur
  30 s). **F3** cache le bandeau du haut — il revient de lui-même pour une pause ou une coupure.

Trois limites à connaître : **la fenêtre du jeu doit rester au premier plan** chez l'hôte comme en
solo (les amis sont mis en pause et la manette relâchée sinon) ; une box derrière une autre (double
NAT : 4G, certaines fibres) ne peut pas héberger pour internet ; et **pendant une partie lancée par
le lanceur, le clavier de secours du joueur 1** (la seconde colonne de touches des Options : pavé
numérique, Inser, Suppr… par défaut) **ne marche pas** — c'est sa place que prend la manette.

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
  Un compagnon IA passe au joueur quand **sa** manette presse une direction (la règle du jeu).
- **Le relais du joueur 1** (`Hote/Relais.cs`, `add-launcher-solo-gamepad` D2, D3) : l'ami sans le
  réseau. Toutes les 4 ms : les manettes relevées (jamais le clavier, que le jeu lit lui-même), le
  mot de 16 bits composé par `manette.json`, rejoué en touches fantômes dans **la table 1** du
  joueur 1 — sa table 0, le clavier principal, n'est jamais écrite. Mot nul si l'écran de
  configuration est ouvert ou si le jeu n'est pas au premier plan.
- **La remise du cfg** (D6) : seuls les joueurs d'une session sont écrits (le joueur 1 en solo, les
  quatre en hébergement), leur ancien contenu sauvegardé dans `OpenForesia.cfg.lanceur`. Il est
  remis **après la fin du jeu**, jamais pendant : le jeu réécrit `[Controls]` depuis sa mémoire en
  sortant de ses Options, et y remettrait les touches fantômes sans sauvegarde. Si le lanceur
  s'arrête avant, son lancement suivant remet tout, avant le reste (sauf si le jeu tourne encore).
  L'ancienne sauvegarde `OpenForesia.cfg.multijoueur` est relue aussi.
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
| `Hote/` | la session, le jeu (trouvé, lancé, premier plan), les touches fantômes, le serveur, l'injection, le relais du joueur 1, la diffusion de l'image et du son, l'UPnP, l'hôte en console |
| `Solo/` | la partie solo : touches du joueur 1, jeu, relais, remise du cfg — en fenêtre ou en console |
| `Invite/` | la connexion, les entrées (XInput, winmm, clavier), la configuration de la manette, la fenêtre et la boucle de l'ami |
| `Video/` | l'encodeur et le décodeur H.264, les conversions BGRA/NV12, les morceaux d'image |
| `Natif/` | les appels Windows : Win32, COM et WinRT, la capture, Media Foundation, WASAPI, DirectInput, les contrôles |
| `Interface/` | l'accueil, la fenêtre de la partie solo, celle de l'hôte, et « Configurer ma manette » |
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

Deux modes de plus servent à essayer le lanceur lui-même : `--solo [--jeu <dossier>]` (la partie
solo dans la console, journal du relais compris — elle lance le jeu) et `--invite-seul` (l'accueil
réduit d'un ami qui n'a que le lanceur, dans le dépôt).

`--rejoindre` accepte aussi `--refaire-a <ms>` (simule une coupure du réseau) et
`--derniere-image <bmp>` ; `--heberger` accepte `--sans-upnp`, `--adresse-publique <ip>` et
`--sans-fenetre` (tout dans la console).

## Pièges déjà rencontrés

- Une session Windows affichée à distance plafonne la capture à sa propre fréquence (32 Hz ici) :
  compter les images de la mire dans ces conditions ne mesure pas le programme.
- `Thread.Sleep(1)` dort 15,6 ms sans `timeBeginPeriod(1)` : l'hôte et l'ami la demandent.
- L'encodeur refuse **sans bruit** un réglage mal typé : `MF.Regler` rend faux, le journal le dit.
