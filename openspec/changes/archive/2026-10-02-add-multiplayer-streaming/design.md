## Context

Voir `proposal.md` pour le pourquoi, `specs/multiplayer-streaming/spec.md` pour le contrat.

Ce que le jeu offre déjà (exploration du 2026-09-30) :

- **Quatre manettes PS1 virtuelles** : `FoldKeyboardIntoPadState` (`Engine/Input.cs`, Ghidra
  `FUN_00523660`) fabrique pour chaque joueur un mot de 16 bits actif bas au format du pad PS1, à
  partir de deux tables de touches (`[Controls]`, tables 0 et 1, scancodes DirectInput 0..255) et
  d'une table de boutons de manette (table 2). Par défaut, les joueurs 2 à 4 n'ont aucune touche.
- **Les vraies manettes ne sont pas lues** : `FUN_00523230` (création d'un device par manette) n'est
  pas transcrit. Le multijoueur n'en dépend pas.
- **Deux interrupteurs** dans la même fonction : `DAT_005de168` débranche les joueurs 2 à 4 dans le
  seul état 6 (Menu objets de l'écran titre), comme en 1998 ; `DAT_00564a50` coupe la traduction
  clavier → manette pendant la saisie du nom d'un personnage, où le clavier tape des caractères.
- **Le clavier est acquis `DISCL_FOREGROUND | DISCL_NONEXCLUSIVE`** : lu seulement quand la fenêtre
  du jeu est au premier plan.
- **L'image** sort par notre swapchain D3D11 (`Host/D3D11Backend.cs`), **le son** par DirectSound.

## Goals / Non-Goals

**Goals:**
- Jouer à quatre (l'hôte + trois amis) à distance, clé en main, sans logiciel tiers ni serveur.
- Ne pas toucher au jeu : ni `src/Blaze/`, ni `src/Host/`, ni `src/LegacyPlus/`.
- Latence de l'ordre de 50 à 100 ms sur une connexion domestique.

**Non-Goals:**
- Des spectateurs au-delà des trois joueurs ; un client autre que Windows (navigateur, mobile).
- Les vraies manettes branchées chez l'hôte (le joueur 1 joue au clavier, comme aujourd'hui).
- Un serveur de mise en relation, un annuaire de parties, une traversée de NAT par poinçonnage.
- La synchronisation d'état façon netplay : un seul jeu tourne, chez l'hôte.

## Decisions

### D1 — Un programme à part, un exe pour héberger et rejoindre

`OpenForesia.Multiplayer/`, projet .NET à côté de `OpenForesia.Installer`, mêmes règles : code
d'aujourd'hui, **pas de WinForms** (fenêtres Win32 natives, comme l'installeur), interop COM en
`[GeneratedComInterface]`, JSON par `JsonSerializerContext`, publication autonome x86 élaguée en un
seul fichier. D3D11 par Silk.NET, déjà une dépendance du jeu. Le mode hôte lance `OpenForesia.exe`
comme processus enfant et le surveille.

*Alternative écartée* : un module dans le jeu — contraire à « rien changer au jeu », et il ferait
dépendre le jeu de tout le réseau.

### D2 — Les entrées : touches fantômes simulées par `SendInput`

- L'hôte réécrit, dans `OpenForesia.cfg`, les tables 0 et 1 des joueurs 2 à 4 (`K1…`, `K2…`, `K3…`)
  avec 14 touches fantômes chacun (les 14 boutons ; les actions sans bit et les combinaisons restent libres) — 42 scancodes que ni un clavier AZERTY ni un QWERTY ne portent et
  qui ne produisent aucun caractère. **Mesuré le 2026-09-30** (tâche 1.1) : 87 candidats sur 90
  passent ; retenus, les 42 premiers codes **étendus sans touche virtuelle** (DIK `0x81`..`0xBA`),
  en table 0, la table 1 vidée — la liste est dans `Documentation/Reference/OpenForesia.cfg.md`.
  Les actions 16 à 19 (combinaisons) restent sans touche. La table du joueur 1 n'est
  jamais écrite ; l'ancien contenu des joueurs 2 à 4 est sauvegardé et remis à la fin de la session.
- Un invité envoie son mot de 16 bits ; l'hôte compare au précédent et émet, pour chaque bit qui
  change, `SendInput` avec `KEYEVENTF_SCANCODE` (enfoncé ou relâché) sur la touche fantôme du
  joueur et du bouton.
- Un invité qui part, ou qui se tait plus de 500 ms, voit toutes ses touches relâchées.

**La porte de sortie** (écart legacy ++ déclaré, seulement si la mesure 1.1 l'impose) : une couture
dans `FoldKeyboardIntoPadState`, identité par défaut, qui remplace le mot d'un joueur par celui qu'un
fichier partagé (`MemoryMappedFile`) lui donne. Elle lèverait aussi la contrainte du premier plan.

*Alternative écartée* : une manette virtuelle (ViGEm) — un pilote à installer, et le jeu ne lit pas
les manettes.

### D3 — L'image : capture de la fenêtre, H.264 matériel

- **Capture** : `Windows.Graphics.Capture` sur la fenêtre du jeu (Windows 10 1903+), textures D3D11
  sur le GPU, sans lecture du bureau. Accès WinRT par appels de vtable (`Natif/Com.cs`), sans
  projection CsWinRT — **prouvé sous élagage** le 2026-09-30 (`--essai-image` sur l'exe publié).
  Seule la zone client est gardée (cadre étendu DWM), copiée dans une texture STAGING et lue par le
  processeur. La fenêtre capturée est la plus grande du lanceur ou de ses enfants (D8), revue toutes
  les deux secondes : elle change au démarrage du jeu.
- **Encodage** (révisé à l'écriture) : l'encodeur H.264 **logiciel** de Windows
  (`CLSID_MSH264EncoderMFT`), une transformation **synchrone**. Les encodeurs matériels de Media
  Foundation sont asynchrones (file d'événements) ; le logiciel suffit — 2 ms par image en
  960 × 720 conversion comprise, mesuré. Profil Baseline (pas d'images B), basse latence, débit
  constant 5 Mbit/s, image clé toutes les deux secondes et à la demande, 60 images par seconde au
  plus. L'image est mise à l'échelle **960 × 720** (le jeu est en 4:3 ; bandes noires sinon),
  BGRA → NV12 au plus proche voisin, BT.601. Le matériel reste une amélioration possible si la
  charge du processeur de l'hôte le demande.
  **Piège** (essai 2.5, près d'une seconde de retard) : `CODECAPI_AVLowLatencyMode` est un
  `VT_BOOL` (vrai = `0xFFFF`) et se règle **avant** les types ; envoyé en `VT_UI4`, il était refusé
  sans bruit et l'encodeur gardait six images en réserve. `--essai-image` compte ce retard
  (encodeur et décodeur : 0 image chacun depuis la correction).
- **Un seul encodage**, le même flux envoyé aux trois invités.

### D4 — Le son : capture du seul processus du jeu, PCM

WASAPI en « process loopback » (`ActivateAudioInterfaceAsync`,
`AUDIOCLIENT_ACTIVATION_TYPE_PROCESS_LOOPBACK`, Windows 10 2004+) sur l'arbre de processus du jeu :
le son de la partie seul. Envoyé en PCM 16 bits 48 kHz stéréo (1,5 Mbit/s) par paquets de **5 ms**
(960 octets : 10 ms dépasseraient un datagramme) — aucune latence d'encodeur, aucun codec à
embarquer. La cible est le **lanceur** (`INCLUDE_TARGET_PROCESS_TREE`) : son arbre porte le vrai jeu
(D8). L'activation est asynchrone : l'objet de rappel COM (`IActivateAudioInterfaceCompletionHandler`,
qui se dit aussi `IAgileObject`) est une vtable écrite à la main (`Natif/Audio.cs`).

Côté invité, sortie WASAPI partagée sur le périphérique par défaut. Sa file est la réserve :
**40 ms de silence** posées chaque fois qu'elle se trouve à sec (au premier paquet, et après un trou
du réseau), et un paquet **jeté** si plus de 80 ms attendent déjà — le retard reste borné. Sans la
réserve, la moindre avance perdue vidait la sortie entre deux paquets (trous, craquements : mesuré
par `--essai-son`, qui joue une note de 440 Hz et la retrouve par la capture de processus).

### D5 — Le transport : UDP, un protocole à nous

Un seul port UDP chez l'hôte. Paquets datagrammes, chiffrés et authentifiés un par un
(AES-GCM, clé de session ; `Commun/Chiffrement.cs`). Sur le fil : l'en-tête en clair (8 octets),
l'**émetteur** (4 : 0 pour l'hôte, l'identifiant de l'invité sinon), le contenu chiffré, l'étiquette
(16). En-tête et émetteur sont authentifiés. **Nonce = émetteur, numéro, genre** : unique tant qu'un
émetteur ne réutilise pas un numéro — d'où, à l'écriture, **un seul compteur atomique** pour les trois
fils de l'hôte (général, image, son), qui en avaient chacun un. Un paquet envoyé à trois invités est
scellé une fois. Un invité ne parle qu'en son nom : l'identifiant du contenu doit être l'émetteur.
Les genres (dix à l'écriture, avec l'image et le son) :

| genre | sens | contenu |
|---|---|---|
| `Bonjour` | invité → hôte | preuve de la clé, version ; réponse : joueur attribué ou « complet » |
| `Manette` | invité → hôte | les 16 bits, et les trois états précédents (une perte ne coûte rien), 120 fois par seconde |
| `Image` | hôte → invité | un fragment d'image H.264 (≤ 1 200 octets), numéro d'image et de fragment |
| `Son` | hôte → invité | 10 ms de PCM |
| `Contrôle` | les deux | image clé demandée (image incomplète), présence, pause (fenêtre en arrière), au revoir |

Une image dont un fragment manque est jetée et l'invité demande une image clé : pas de
retransmission, qui coûterait plus qu'elle ne rapporte à 60 images par seconde.

*Alternative écartée* : WebRTC — une bibliothèque tierce (SIPSorcery) et un serveur de signalisation,
pour un client qui serait de toute façon natif.

### D6 — Internet : UPnP et code d'invitation

- L'hôte demande à la box, par UPnP IGD (découverte SSDP, `AddPortMapping`), d'ouvrir le port UDP
  et obtient l'adresse publique (`GetExternalIPAddress`). Le bail est retiré à la fin de la session.
  Sans bibliothèque : HTTP et SOAP à la main (`Hote/Upnp.cs`), WANIPConnection ou WANPPPConnection.
  Une adresse « publique » privée (10/8, 172.16/12, 192.168/16, 100.64/10) est signalée comme un
  double NAT. `--essai-upnp` fait le tour complet (trouver, ouvrir, lire, refermer) sans le jeu.
- Le **code d'invitation** : adresse IPv4, port, clé de session de 128 bits, soit 22 octets en base32
  de Crockford (sans I, L, O, U) — **36 caractères**, groupés par cinq. Saisie tolérante : espaces,
  tirets et casse ignorés, O lu 0, I et L lus 1. L'hôte affiche un code pour internet et un par
  adresse locale.
- Repli sans UPnP : l'écran de l'hôte indique le port à rediriger ; `--adresse-publique` fait alors
  le code internet.
- **Reconnexion** (4.4) : un invité est reconnu à son identifiant, pas à son adresse ; revenu d'une
  autre adresse, il retrouve son joueur et reçoit une image clé. L'hôte libère un joueur après 30 s
  de silence ; l'invité qui n'entend plus l'hôte depuis 3 s refait sa socket et se ré-annonce
  (`--refaire-a` le simule à l'essai).

### D7 — Le client

- **Décodage** (révisé à l'écriture) : le décodeur H.264 de Windows (`CLSID_CMSH264DecoderMFT`),
  synchrone, en `MF_LOW_LATENCY` (sans quoi il garde des images en réserve) ; NV12 → BGRA, puis
  **affichage GDI** (`StretchDIBits`) plutôt que D3D11 : sans dépendance, 5 ms par image décodage
  et conversion compris, mesuré. Fenêtre redimensionnable, 4:3 gardé (bandes noires), plein écran
  par Alt+Entrée, un bandeau d'état en haut de l'image.
- **Manettes** : XInput (manettes Xbox et compatibles) et l'API joystick de winmm (`joyGetPosEx` :
  le reste, dont les manettes PlayStation branchées sans surcouche — plus simple que DirectInput 8,
  sans COM), et le clavier. Configuration « appuie sur CROIX… » bouton par
  bouton, gardée dans `%APPDATA%\OpenForesia\manette.json`, réinitialisable.
- **Affichage des états** : connexion, joueur attribué, latence mesurée (aller-retour des
  `Contrôle`), « l'hôte a mis le jeu en arrière-plan ».

### D8 — La fenêtre du jeu au premier plan

L'hôte compare `GetForegroundWindow()` à la fenêtre du jeu à chaque image ; quand elle n'est plus
devant, il relâche toutes les touches fantômes, ignore les manettes et prévient les invités.

**Le jeu n'est pas le processus lancé** (appris à l'essai 1.9, 2026-09-30) : depuis LB-056
(`Host/ReserveBasse.cs`), `OpenForesia.exe` est un lanceur qui crée le vrai jeu en processus
**enfant** et l'attend. La comparaison porte donc sur le processus de la fenêtre au premier plan :
le lanceur ou un de ses enfants directs. La capture d'image (D3) devra viser de même la fenêtre de
l'enfant.

## Risks / Trade-offs

- [Les touches fantômes filtrées quelque part entre `SendInput` et DirectInput] → c'est la mesure 1.1,
  première tâche du change : un petit outil émet chaque candidat et lit ce que DirectInput reçoit.
  S'il en passe moins de 42, repli sur des touches ordinaires peu employées puis, en dernier
  recours, la couture de D2.
- [Débit montant de l'hôte : ~5 Mbit/s par invité, ~15 pour trois] → le débit se règle à
  l'écran de l'hôte ; l'ADSL demandera 720p et un débit plus bas.
- [Box sans UPnP, ou double NAT (4G, certaines offres fibre)] → repli sur la redirection manuelle ;
  le double NAT reste hors de portée sans serveur, dit clairement à l'écran.
- [Un antivirus qui voit un programme injectant des frappes clavier] → programme publié, signé si
  possible plus tard ; `SendInput` est une API ordinaire, sans crochet ni injection de code.
- [La capture WinRT sans CsWinRT, sous élagage] → interop COM à la main, comme l'installeur ; à
  prouver au lot 2 avant d'y bâtir le reste.
- [Windows 10 2004+ obligatoire pour le son d'un processus] → dit à l'écran ; sans lui, session sans
  son.

## Migration Plan

Aucune migration : un programme nouveau. `OpenForesia.cfg` retrouve à la fin d'une session les
touches des joueurs 2 à 4 d'avant ; si l'hôte plante, elles sont remises au lancement suivant (une
copie datée reste à côté du cfg tant qu'une session est ouverte).
