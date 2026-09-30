## 1. Lot 1 — Les manettes (réseau local, image par un partage d'écran tiers)

- [x] 1.1 Mesure des touches fantômes : un petit outil émet chaque scancode candidat par `SendInput` (`KEYEVENTF_SCANCODE`, simples et étendus) vers la fenêtre du jeu et relève ce que DirectInput reçoit (journal `[INPUT]` du jeu) et ce que la saisie du nom tape ; retenir 42 codes qui passent sans caractère, les consigner dans `Documentation/Reference/OpenForesia.cfg.md` — mesuré le 2026-09-30 : 87 passent sur 90, 42 retenus
- [x] 1.2 Créer le projet `OpenForesia.Multiplayer` (exe `OpenForesiaMultiplayer.exe`), dans `OpenForesia.sln`, publication autonome x86 élaguée en un fichier, avertissements d'élagage en erreurs
- [x] 1.3 Hôte : sauvegarder puis réécrire les tables 0 et 1 des joueurs 2 à 4 dans `OpenForesia.cfg` (touches fantômes), remettre l'ancien contenu à la fin ou au lancement suivant après un plantage ; ne jamais écrire le joueur 1
- [x] 1.4 Hôte : lancer `OpenForesia.exe`, trouver sa fenêtre, suivre le premier plan (D8)
- [x] 1.5 Réseau : socket UDP, paquets `Bonjour` / `Manette` / `Contrôle`, attribution des joueurs 2 à 4, « complet », départ et silence de 500 ms (touches relâchées)
- [x] 1.6 Hôte : du mot de 16 bits aux `SendInput` enfoncé / relâché des touches fantômes du joueur
- [x] 1.7 Invité : lecture XInput, DirectInput 8 et clavier ; configuration « appuie sur CROIX… » des quatorze entrées, gardée dans `%APPDATA%\OpenForesia\manette.json`
- [x] 1.8 Invité : envoi du mot de 16 bits 120 fois par seconde avec les trois états précédents
- [x] 1.9 Essai (Vincent) : deux PC du réseau local, l'image par un partage d'écran tiers ; un invité joue le joueur 2 en partie ; la saisie d'un nom n'est pas perturbée ; Alt+Tab chez l'hôte met les invités en pause

## 2. Lot 2 — L'image

- [x] 2.1 Hôte : capture de la fenêtre du jeu par `Windows.Graphics.Capture` en interop COM sans CsWinRT, prouvée sous élagage avant le reste du lot
- [x] 2.2 Hôte : encodeur H.264 Media Foundation logiciel (révisé, design D3), basse latence, sans images B, mise à l'échelle 960 × 720, image clé toutes les deux secondes et à la demande
- [x] 2.3 Réseau : paquets `Image` fragmentés (≤ 1 200 octets), un seul encodage envoyé à tous les invités ; image incomplète jetée, image clé demandée
- [x] 2.4 Invité : fenêtre Win32, décodage H.264 Media Foundation, affichage GDI en 4:3 (révisé, design D7), plein écran par Alt+Entrée
- [x] 2.5 Essai (Vincent) : réseau local, trois invités, sans partage d'écran tiers ; relevé de la latence ressentie

## 3. Lot 3 — Le son

- [x] 3.1 Hôte : capture WASAPI « process loopback » de l'arbre de processus du jeu, PCM 16 bits 48 kHz stéréo, paquets `Son` de 5 ms (révisé, design D4)
- [x] 3.2 Invité : lecture WASAPI, réserve de 40 ms refaite quand elle est à sec, paquet jeté au-delà de 80 ms en attente (révisé, design D4)
- [x] 3.3 Essai (Vincent) : le son du jeu seul chez les invités, pas celui d'une vidéo ouverte chez l'hôte

## 4. Lot 4 — Internet

- [x] 4.1 Hôte : UPnP IGD — découverte SSDP, `AddPortMapping` du port UDP, `GetExternalIPAddress`, bail retiré en fin de session ; repli expliqué à l'écran (port à rediriger, adresse locale)
- [x] 4.2 Code d'invitation : adresse, port et clé de session de 128 bits en base32 groupé par cinq ; saisie tolérante côté invité (espaces, casse)
- [x] 4.3 Chiffrement et authentification AES-GCM de chaque paquet, nonce = émetteur, numéro et genre, un seul compteur pour tous les fils de l'hôte (révisé, design D5) ; paquet invalide ignoré sans réponse
- [x] 4.4 Reconnexion d'un invité qui perd le réseau : même joueur s'il revient dans les 30 secondes
- [ ] 4.5 Essai (Vincent) : un ami hors du réseau local rejoint par le code ; une connexion sans le code ne reçoit rien — **reporté** par Vincent le 2026-09-30, à faire plus tard avec ses amis (`--essai-upnp` d'abord)

## 5. Lot 5 — Finition et intégration

- [x] 5.1 Lanceur : écran d'accueil « Héberger / Rejoindre », l'écran hôte (code, invités connectés, débit, premier plan), l'écran invité (joueur, latence, pause) — l'écran hôte d'abord (2026-09-30, demandé par Vincent : UPnP à cocher, port, adresse publique, codes internet et réseau local avec « Copier », puis l'état de la partie), puis l'accueil (Héberger, ou Rejoindre avec le code, dernier code retenu, reconfigurer la manette) ; l'écran invité est le bandeau de sa fenêtre
- [x] 5.2 Réglage du débit et de la taille d'image chez l'hôte
- [x] 5.3 `publish-release.ps1` : publier `OpenForesiaMultiplayer.exe` élagué et l'ajouter au zip du jeu ; contrôle sans donnée du jeu — publié en 0.3.0 le 2026-09-30 : 2 fichiers dans le jeu (OpenForesia.exe, OpenForesiaMultiplayer.exe), aucune donnée du jeu, Setup de 22 Mo
- [x] 5.4 Installeur : déposer l'exe, raccourci « OpenForesia Multijoueur », désinstallation
- [x] 5.5 Documentation : `OpenForesia.Multiplayer/LISEZMOI.md`, `CLAUDE.md` (disposition du dépôt), `HISTORY.md`
- [ ] 5.6 Essai final (Vincent) : installation depuis le Setup sur deux PC, une partie à quatre à distance
