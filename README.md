# OpenForesia

**Le jeu PC *Blaze & Blade* (1998), connu en France sous le nom *Legend of Foresia*, reconstruit
fonction par fonction en C#, et rendu jouable sur un Windows d'aujourd'hui.**

- Il s'installe **depuis votre propre ISO** du jeu, en quelques clics : plus de CD, plus de DLL de
  1998, rien d'autre à installer.
- Il se joue **au clavier ou à la manette**, seul ou **jusqu'à quatre à distance**.
- Il propose deux façons de jouer : le **jeu d'origine** tel qu'en 1998, ou **Legacy Plus**, avec
  des options d'équilibrage et de confort, toutes désactivées par défaut.

> **Aucune donnée du jeu n'est fournie ici**, ni dans le dépôt ni dans les versions publiées. Le
> projet n'en a pas les droits : il vous faut votre propre exemplaire du jeu, sous forme d'image ISO.

---

## Sommaire

- [Pourquoi ce projet](#pourquoi-ce-projet)
- [Comment il est fait](#comment-il-est-fait)
- [Où en est le projet, et la suite](#où-en-est-le-projet-et-la-suite)
- [Installer et jouer](#installer-et-jouer)
- [Le lanceur : jouer à la manette](#le-lanceur--jouer-à-la-manette)
- [Le multijoueur](#le-multijoueur)
- [Legacy et Legacy Plus](#legacy-et-legacy-plus)
- [L'affichage](#laffichage)
- [L'éditeur de sauvegarde](#léditeur-de-sauvegarde)
- [Le bestiaire](#le-bestiaire)
- [Bugs connus et contributions](#bugs-connus-et-contributions)
- [Pour les développeurs](#pour-les-développeurs)
- [Licence](#licence)
- [Mentions](#mentions)

---

## Pourquoi ce projet

**Préserver le jeu.** *Blaze & Blade* est un action-RPG de 1998, jouable à quatre, sorti sur
PlayStation et sur PC. La version PC ne tourne plus correctement sur un Windows moderne : elle
dépend de DirectX 5, de DLL d'époque, d'un codec vidéo disparu (Indeo 4) et d'un CD protégé
contre la copie. Chaque nouvelle version de Windows la rend un peu plus difficile à lancer.

Plutôt que d'empiler les correctifs autour de l'exécutable d'origine, OpenForesia le **réécrit** :
le comportement du jeu est retrouvé dans le binaire, puis transcrit en C# lisible. Le résultat ne
dépend plus de rien de 1998, et peut continuer à tourner, à être corrigé et à être compris
longtemps après que le binaire d'origine aura cessé de fonctionner.

## Comment il est fait

C'est un travail de **rétro-ingénierie** : le code du jeu est reconstruit fonction par fonction,
en suivant le graphe d'appels, à partir de sa décompilation. Chaque fonction reconstruite garde
l'adresse de son origine (`// Ghidra: FUN_00513900 @ 0x00513900`), si bien qu'on peut toujours
vérifier une ligne de C# contre le binaire.

Les outils :

| Outil | Rôle |
|---|---|
| **[Claude Code](https://claude.com/claude-code)** (modèles **Claude Opus**, d'Anthropic) | la lecture de la décompilation, la transcription en C#, l'enquête sur les bugs, les outils du projet et la documentation, sous la direction de ChatgaraTech |
| **[Ghidra](https://ghidra-sre.org/)** (NSA) | la décompilation de `Blaze.exe` et de ses cinq DLL d'aide : la source de vérité du projet |
| **[Iced](https://github.com/icedland/iced)** | le désassembleur x86 de l'outil maison `OpenForesia.Tools disasm`, qui atteint les fonctions que Ghidra n'a pas décompilées |
| **[x32dbg](https://x64dbg.com/)** | l'observation du jeu d'origine en cours d'exécution, pour comparer |
| **[FFmpeg](https://ffmpeg.org/)** | son décodeur Indeo 4, porté en C# pour les cinématiques, identique à l'octet près au codec d'Intel sur les 8 849 images des films |
| **.NET 10 / C#** et **Direct3D 11** | la reconstruction elle-même, et le remplacement de la chaîne d'affichage de 1998 |
| **[OpenSpec](https://github.com/Fission-AI/OpenSpec)** | le suivi des chantiers : chaque fonctionnalité un peu grosse est proposée, conçue, puis archivée |
| **Node.js** et des scripts shell | les recensements : ce qui est écrit, ce qui a été vu en jeu, ce qui reste à faire |

La méthode est écrite en détail (en français) dans
[`Documentation/Methode/Strategy.md`](Documentation/Methode/Strategy.md) et
[`Documentation/Methode/Prompt.md`](Documentation/Methode/Prompt.md).

## Où en est le projet, et la suite

**OpenForesia est aujourd'hui très proche de l'exécutable d'origine.** C'est une rétro-ingénierie
brute, améliorée (« legacy ++ ») : le jeu marche bien, mais son code reste celui de 1998, transcrit.

- **Le code a la forme du binaire, pas celle d'un jeu conçu en C#.** L'essentiel est une seule
  grosse classe partielle, `Blaze`, découpée en près de 200 fichiers (environ 210 000 lignes). Ses
  fonctions gardent leurs adresses d'origine, et la mémoire du jeu est reproduite telle quelle,
  avec ses pointeurs et ses tables.
- **Les DLL du jeu d'origine ont disparu, mais pas leur comportement.** Les cinq DLL d'aide de 1998
  sont remplacées par du code C# (`OpenForesia/src/Libraries/`), qui reproduit fidèlement ce
  qu'elles faisaient. Elles gardent donc aussi **leurs limites**, en bonne partie héritées du fait
  que le jeu PC est à l'origine un **portage de la version PlayStation** : liste d'affichage façon
  PS1, petite résolution logique, nombre limité de monstres à l'écran, etc.
- Les apports modernes (affichage Direct3D 11, installeur, lanceur, multijoueur, Legacy Plus) sont
  **posés autour** de ce noyau, sans le réécrire.

**La prochaine étape possible : une réécriture complète.** Maintenant que le jeu existe sous forme
de sources lisibles, et que son comportement est documenté fonction par fonction, il devient
possible de le **réécrire entièrement sur un vrai moteur de jeu**, avec des technologies récentes.
On se débarrasserait ainsi des contraintes de 1998 et de la PlayStation, plutôt que de les
reproduire. OpenForesia servirait alors de référence : la description exacte de ce que fait le jeu
d'origine, contre laquelle vérifier la nouvelle version.

---

## Installer et jouer

**Il vous faut** : Windows 10 ou 11, et une image ISO de la **version CD-ROM** du jeu
(*Legend of Foresia*, version française). Rien d'autre : ni .NET, ni DirectX, ni le CD.

> **Où trouver l'ISO ?** Le jeu est disponible en abandonware sur
> [Abandonware France](https://www.abandonware-france.org/ltf_abandon/ltf_jeu.php?id=1974&fic=liens).
> Prenez bien la **version CD-ROM** du jeu : c'est son image ISO que l'installeur attend. Les
> autres versions proposées ne contiennent pas les fichiers qu'il vérifie.

1. Téléchargez **`OpenForesia-Setup.exe`** depuis la page
   [Releases](https://github.com/ChatgaraTech/OpenForesia/releases).
2. Lancez-le, choisissez votre fichier **ISO**, puis le dossier d'installation (par défaut
   `%LOCALAPPDATA%\Programs\OpenForesia`).
3. C'est tout. L'installeur extrait les données du jeu depuis votre ISO, vérifie chacun des
   133 fichiers attendus, et crée deux raccourcis :
   - **OpenForesia** : lance le jeu directement, au clavier ;
   - **OpenForesia Lanceur** : la porte d'entrée conseillée, pour jouer à la manette ou en
     multijoueur (voir ci-dessous).

Le jeu apparaît aussi dans « Applications installées » de Windows, avec son désinstalleur. Vos
sauvegardes sont dans le dossier `save\` de l'installation, et vos réglages (`Affichage.ini`,
`LegacyPlus.ini`) ne sont **jamais écrasés** par une réinstallation.

**Les touches par défaut** (modifiables dans les Options du jeu) :

| Touche | Bouton PlayStation | Dans les menus |
|---|---|---|
| Flèches, ou 8 4 6 2 du pavé numérique | croix directionnelle | se déplacer |
| Alt gauche | ✕ croix | valider |
| Maj gauche | ○ rond | revenir |
| Ctrl gauche | □ carré | |
| Espace | △ triangle | |
| F2 / F1 | START / SELECT | |
| F11 / F12 | | taille de l'image du jeu |
| Alt + Entrée | | plein écran |

---

## Le lanceur : jouer à la manette

**Pour jouer à la manette, passez par le lanceur** (raccourci *OpenForesia Lanceur*). Le jeu, lui,
ne lit que le clavier. Le lanceur lit votre manette et la transmet au jeu.

À l'ouverture, il propose quatre choix :

- **Jouer seul** : le jeu démarre et votre manette pilote le joueur 1. Le clavier marche en même
  temps. Une petite fenêtre reste ouverte pendant la partie ; la fermer arrête la manette, le jeu
  continue au clavier.
- **Héberger** et **Rejoindre** : le multijoueur, ci-dessous.
- **Configurer ma manette** : une manette PlayStation dessinée, un bouton à la fois. Un clic sur
  une case pour changer le bouton, un clic droit pour l'effacer. Tout est enregistré tout de suite.
  On peut y revenir en pleine partie, par Alt+Tab.

N'importe quelle manette reconnue par Windows convient (Xbox, PlayStation, génériques).

Deux choses à savoir :

- **La fenêtre du jeu doit rester au premier plan.** Si vous passez sur une autre fenêtre, la
  manette est relâchée.
- Pendant une partie lancée par le lanceur, la **seconde colonne de touches** du joueur 1 (dans
  les Options du jeu : les flèches, Inser, Suppr, Origine, Fin…) ne marche plus, parce que c'est
  elle qui transporte la manette. Au clavier, la première colonne reste active : pavé numérique
  pour se déplacer, Alt, Maj, Ctrl et Espace pour les boutons.

---

## Le multijoueur

Jusqu'à **quatre joueurs**, par internet ou en réseau local, **sans serveur** et sans logiciel
tiers.

### Comment ça marche : le streaming

Le jeu ne tourne **que chez l'hôte**. Le lanceur de l'hôte filme la fenêtre du jeu, capture son
son, et envoie l'image (en vidéo H.264) et le son à chaque ami. Les amis renvoient seulement les
boutons de leur manette, qui pilotent les joueurs 2 à 4.

- **L'hôte** a besoin du jeu installé.
- **Les amis n'ont besoin que de `OpenForesiaLanceur.exe`**, publié seul à côté de l'installeur
  (~10 Mo) : ni le jeu, ni l'ISO, ni installation.
- Tout passe **chiffré** (AES-GCM) avec une clé tirée au hasard pour la partie, transmise dans le
  code d'invitation.

### Héberger

1. Ouvrez le lanceur, puis **Héberger**.
2. Laissez cochée l'ouverture automatique du port : le lanceur demande à votre box d'ouvrir le
   **port UDP 47200** (par UPnP), puis le referme à la fin de la partie.
3. Choisissez la qualité de l'image (**Haute** par défaut, Normale, Économe). C'est le débit envoyé
   à **chaque** ami : baissez-la si votre connexion montante est faible.
4. Envoyez à vos amis le **code d'invitation** (bouton « Copier ») : le code *internet*, ou le code
   *réseau local* s'ils sont chez vous.
5. **Lancer la partie.** Vous jouez le joueur 1, au clavier ou à la manette.

**Si l'ouverture automatique échoue** (UPnP désactivé sur la box), ouvrez le port à la main dans
l'interface de votre box : une redirection **UDP 47200** vers l'adresse locale du PC qui héberge.
Si Windows demande l'autorisation du pare-feu pour le lanceur, acceptez-la.

**Limite** : une box derrière une autre box (« double NAT » : certaines connexions 4G/5G ou fibre)
ne peut pas héberger pour internet. Le réseau local marche toujours.

### Rejoindre

1. Ouvrez `OpenForesiaLanceur.exe`, collez le code reçu, **Rejoindre**.
2. La première fois, l'écran de la manette s'ouvre : appuyez sur les quatorze boutons demandés.
3. Pendant la partie : **F1** reconfigure la manette, **F2** affiche la qualité du flux (images
   par seconde, ping, débit, pertes), **F3** masque le bandeau du haut, **Alt+Entrée** passe en
   plein écran.

Sur Windows 10, un **cadre jaune-orange** entoure la fenêtre du jeu chez l'hôte pendant la partie :
c'est Windows qui signale qu'une fenêtre est filmée. Vos amis ne le voient pas, et Windows 11
permet de s'en passer (le lanceur le fait).

---

## Legacy et Legacy Plus

OpenForesia se joue de deux façons, et **par défaut c'est le jeu d'origine**.

### Legacy : le jeu de 1998

Le jeu tel qu'il était : même contenu, même équilibrage, mêmes règles. Ce qui a changé ne touche
pas à la partie elle-même :

- **ce qui ne marche plus sur un PC actuel est remplacé** : le CD et sa protection anticopie, les
  DLL de 1998, le codec des cinématiques, DirectX 5 ;
- **certains défauts du jeu d'origine sont corrigés** quand ils bloquent le joueur. Par exemple :
  revenir de la saisie du nom figeait l'écran de sélection des personnages. Chaque correction est
  déclarée dans [`legacy-bug.md`](Documentation/Reference/legacy-bug.md), avec ce que faisait
  l'original ;
- **quelques conforts** : un affichage net en haute résolution, la croix qui valide dans les menus
  comme sur PlayStation, le nom du personnage saisissable à la manette.

### Legacy Plus : des options pour changer le jeu

Des réglages d'équilibrage et des ajouts, **tous désactivés par défaut**. On les active dans le
fichier **`LegacyPlus.ini`**, à côté de `OpenForesia.exe` dans le dossier d'installation
(par défaut `%LOCALAPPDATA%\Programs\OpenForesia\LegacyPlus.ini`).

**Comment faire :**

1. Fermez le jeu.
2. Ouvrez `LegacyPlus.ini` avec le Bloc-notes. Chaque réglage y est expliqué en commentaire
   (lignes qui commencent par `;`).
3. Changez une valeur, par exemple `RamassageAuto = oui`, puis enregistrez.
4. Relancez le jeu.

Le jeu lit ce fichier au démarrage et ne l'écrit jamais. Une valeur mal écrite est ignorée (la
valeur par défaut s'applique), et une réinstallation conserve votre fichier. Pour revenir au jeu
d'origine, remettez les valeurs par défaut, ou supprimez le fichier.

**Ce que Legacy Plus permet :**

| Section et clé | Défaut | Ce que ça change |
|---|---|---|
| `[Difficulte] PvMonstres` | `1` | les PV des monstres, multipliés (de 0.1 à 10) |
| `[Difficulte] DegatsMonstres` | `1` | les dégâts que les monstres infligent au groupe, multipliés (de 0.1 à 10) |
| `[Magie] ScalingMagique` | `0` | les sorts du groupe montent avec la puissance magique du lanceur ; `1` est conseillé |
| `[Magie] DeplacementEnIncantation` | `0` | se déplacer en lançant un sort, à une part de la vitesse de marche ; `0.5` est conseillé |
| `[Magie] SoutienFee` | `non` | les bonus de la Fée touchent tout le groupe, ses malus touchent tous les ennemis proches |
| `[Magie] CombinaisonsLibres` | `non` | chaque personnage choisit ses propres raccourcis de sort (menu START, onglet magie, SELECT) |
| `[Combat] InvulnerabiliteEnnemis` | `oui` | `non` : un ennemi qui clignote reste touchable par les autres joueurs et les autres attaques |
| `[Butin] RamassageAuto` | `non` | les pièces et l'expérience se ramassent seules, sans marcher dessus |
| `[Rencontres] Frequence` | `1` | la fréquence des rencontres aléatoires, multipliée |
| `[Rencontres] MonstresMax` | `0` | une nouvelle rencontre peut arriver en plein combat, jusqu'à ce nombre de monstres (12 au plus) |
| `[Rencontres] Formations` | `1` | jusqu'à 4 groupes de monstres par rencontre |
| `[Auberge] Hub` | `non` | un téléporteur à l'auberge mène à une salle à vous : coffre, **marchands** et **arène** |

Avec **`Hub = oui`**, la salle du hub ouvre deux ajouts :

- **les marchands** : objets, accessoires, armures et armes à acheter, et une voleuse qui rachète
  votre butin. Leurs catalogues et leurs prix se règlent dans les fichiers
  `LegacyPlus\Boutique*.ini` (un par marchand, dans le dossier `LegacyPlus\` de l'installation) ;
- **l'arène** : rejouer les combats de boss du jeu, chacun avec son escorte, sur quatre paliers
  de difficulté (Normal à Impossible), et quatre **boss rush** à enchaîner, contre une bourse en
  pièces d'or.

Le détail de chaque clé : [`Documentation/Reference/LegacyPlus.ini.md`](Documentation/Reference/LegacyPlus.ini.md).

---

## L'affichage

Un second fichier, **`Affichage.ini`**, à côté de `OpenForesia.exe`, règle l'image. Il se modifie
de la même façon que `LegacyPlus.ini` :

| Clé | Valeurs | Ce que ça change |
|---|---|---|
| `Rendu` | `moderne` (défaut), `origine` | image fine à la taille de l'écran avec bords lissés, ou les pixels du jeu d'origine |
| `Mode` | `fenetre`, `fenetre-sans-bordure`, `plein-ecran` | le mode de la fenêtre au démarrage |
| `Fenetre` | `auto`, ou par exemple `1280x960` | la taille de la fenêtre |
| `MiseAEchelle` | `ajustee`, `entiere` | image étirée au format 4:3, ou agrandie d'un nombre entier de fois |

Référence : [`Documentation/Reference/Affichage.ini.md`](Documentation/Reference/Affichage.ini.md).

---

## L'éditeur de sauvegarde

**[`OpenForesia.Tools/Html/editeur-sauvegarde.html`](OpenForesia.Tools/Html/editeur-sauvegarde.html)**

Une page web autonome pour modifier vos sauvegardes, pensée pour un joueur (aucun octet ni
décalage à l'écran). Pour chaque personnage :

- le nom, l'élément, le niveau, l'expérience, les PV et PM, l'or ;
- les sept caractéristiques ;
- l'inventaire et l'équipement, depuis les 397 objets du jeu, **avec leurs icônes** ;
- les sorts connus (ou les herbes du Chasseur) ;
- les destinations de la carte et les neuf cristaux ;
- un onglet de **triches rapides** : soin complet, tous les sorts, toutes les destinations…

**L'utiliser :**

1. Téléchargez la page : ouvrez le lien ci-dessus sur GitHub, puis le bouton **« Download raw
   file »** (la flèche vers le bas, en haut à droite du fichier).
2. Double-cliquez sur le fichier téléchargé : il s'ouvre dans votre navigateur. Rien à installer,
   rien n'est envoyé sur internet.
3. **Fermez le jeu** : il réécrit la sauvegarde en quittant, et effacerait vos modifications.
4. Avec **Chrome ou Edge**, cliquez sur **« Ouvrir le dossier save »** et choisissez le dossier
   `save\` de votre installation : l'éditeur y réécrit les fichiers directement, après en avoir
   rangé une copie dans `save\copies-editeur\`.
   Avec Firefox, ouvrez les fichiers `Blaze-Saving…` un par un : « Enregistrer » télécharge le
   fichier modifié, à recopier dans `save\` sous le même nom.

L'éditeur ne change pas la classe d'un personnage. Détails :
[`OpenForesia.Tools/Html/LISEZMOI.md`](OpenForesia.Tools/Html/LISEZMOI.md).

---

## Le bestiaire

**[`Documentation/Reference/Bestiaire/bestiaire.html`](Documentation/Reference/Bestiaire/bestiaire.html)**

> **Version de travail, pas encore terminée.** Les chiffres sont lus directement dans les données
> du jeu, mais certains champs ne sont pas encore identifiés et restent numérotés, et la
> présentation va encore évoluer.

Une page autonome qui recense ce que contient le jeu, en trois vues :

- **Monstres** : chaque monstre avec ses PV, PM, caractéristiques, l'endroit où il apparaît et ce
  qu'il laisse tomber. Un même monstre a souvent plusieurs fiches, une par lieu, de plus en plus
  fort : dépliez la ligne pour voir chaque variante ;
- **Objets** : chaque objet, et quels monstres ou quels coffres le donnent, où, et avec quelle
  chance ;
- **Coffres** : le contenu des coffres, lieu par lieu.

**L'utiliser** : comme l'éditeur, téléchargez le fichier (« Download raw file ») et ouvrez-le d'un
double-clic. Les filtres par zone et la recherche se trouvent en haut de la page.

---

## Bugs connus et contributions

**Des bugs subsistent certainement.** ChatgaraTech a fait un bon paquet de tests, mais le jeu est
grand, et une reconstruction fonction par fonction laisse forcément passer des choses : un
comportement de monstre qui diffère de l'original, un sort qui ne fait pas exactement ce qu'il
devrait, un plantage dans un recoin rarement visité…

**N'hésitez pas à ouvrir une [issue](https://github.com/ChatgaraTech/OpenForesia/issues)**. Le plus
utile :

- **ce qui s'est passé**, et ce que vous attendiez ;
- **où** : la zone, ce que vous faisiez juste avant ;
- si possible, une **capture d'écran** et la **sauvegarde** qui permet de reproduire le problème ;
- si vous connaissez le jeu d'origine : est-ce que **l'original faisait pareil** ? Certains défauts
  viennent du jeu de 1998 lui-même (voir [`legacy-bug.md`](Documentation/Reference/legacy-bug.md)).

Les retours sur Legacy Plus (équilibrage, idées d'options) sont les bienvenus aussi.

---

## Pour les développeurs

| Dossier | Contenu |
|---|---|
| `OpenForesia/` | le jeu : la reconstruction (`src/Blaze/`), les DLL de 1998 réécrites (`src/Libraries/`), l'affichage Direct3D 11 (`src/Host/`), Legacy Plus (`src/LegacyPlus/`) |
| `OpenForesia.Installer/` | l'installeur depuis l'ISO |
| `OpenForesia.Lanceur/` | le lanceur : manette, hébergement, streaming |
| `OpenForesia.Tools/` | désassembleur, lecteur des données du jeu, simulateur de montée de niveau, scripts de recensement et de release |
| `Legacy/` | ce qui vient de 1998 : le binaire et sa décompilation Ghidra (en lecture seule) |
| `Documentation/` | tout ce que le projet a appris, en français |

Pour compiler : le SDK .NET 10, puis `dotnet build OpenForesia.sln`. Pour lancer le jeu en
développement, les données doivent être extraites une fois dans le dossier de sortie :

```powershell
OpenForesia.Installer\bin\Debug\net10.0-windows\OpenForesia-Setup.exe --extraire-donnees <iso> OpenForesia\bin\Debug\net10.0
```

Avant de toucher au code, lisez [`Documentation/ARCHITECTURE.md`](Documentation/ARCHITECTURE.md)
et [`Documentation/Methode/Strategy.md`](Documentation/Methode/Strategy.md). L'avancement est suivi
dans [`Documentation/Etat/HISTORY.md`](Documentation/Etat/HISTORY.md) et
[`Documentation/Etat/REVERSE_STATUS.md`](Documentation/Etat/REVERSE_STATUS.md).

---

## Licence

**Le code d'OpenForesia est sous licence [MIT](LICENSE).** Vous pouvez l'utiliser, le modifier et
le redistribuer librement, à condition de conserver la mention de copyright et le texte de la
licence.

**Une exception : le décodeur Indeo 4**, dans
[`OpenForesia/src/Host/Indeo4/`](OpenForesia/src/Host/Indeo4/). C'est un portage en C# du
décodeur Indeo 4 de [FFmpeg](https://ffmpeg.org) (écrit par Maxim Poliakovski, 2009-2011), le code
qui permet de lire les cinématiques du jeu sans le codec d'Intel que Windows ne fournit plus. Comme
sa source, il reste sous **GNU LGPL 2.1 ou ultérieure** : texte dans
[`LICENSE-LGPL.txt`](OpenForesia/src/Host/Indeo4/LICENSE-LGPL.txt), origine et révision exacte de
FFmpeg dans son [`LISEZMOI.md`](OpenForesia/src/Host/Indeo4/LISEZMOI.md). En pratique :

- ces quatre fichiers, et toute modification qu'on leur apporte, doivent rester disponibles en
  source et sous LGPL ;
- la LGPL ne s'étend pas au reste du projet, qui reste sous MIT ;
- `OpenForesia.exe` intègre ce décodeur. Comme tout le code source du projet est public, chacun
  peut reconstruire le jeu avec une version modifiée du décodeur, ce que la LGPL demande.

Les deux textes de licence sont installés avec le jeu, dans le dossier `Licences\` de
l'installation.

**Ce que la licence MIT ne couvre pas** : ce qui vient du jeu de 1998 et appartient à ses ayants
droit. C'est le binaire d'origine et ses DLL (`Legacy/Binaries/`), leur décompilation par Ghidra
(`Legacy/Decompiled/`), et les noms, valeurs et icônes lus dans les données du jeu par le
bestiaire et l'éditeur de sauvegarde. Ils sont dans le dépôt pour l'étude et la préservation, et
ne sont pas relicenciés.

## Mentions

*Blaze & Blade* / *Legend of Foresia* et tout ce qui s'y rattache appartiennent à leurs ayants
droit. OpenForesia est un projet de préservation, non officiel et non commercial, qui n'a aucun
lien avec eux. **Il ne contient aucune donnée du jeu** et ne sert à rien sans un exemplaire du jeu.
