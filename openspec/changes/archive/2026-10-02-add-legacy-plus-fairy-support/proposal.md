## Why

Dans le jeu de 1998, la Fée (classe 7, école de magie 2) est le personnage le plus faible du groupe :
ses bonus ne touchent qu'**un** allié — celui qui se tient juste devant elle, ou elle-même —, et ses
malus sont des bulles lentes, difficiles à placer, qui ne touchent qu'**un** ennemi et que le jet de
sauvegarde ou l'immunité d'espèce font souvent échouer. Vincent veut lui donner un rôle à elle :
**la soutien du groupe**, qui renforce tout le monde et affaiblit tous les ennemis. C'est un écart
d'équilibre, donc un tweak legacy ++ (LP-015), inactif par défaut.

## What Changes

- **Un interrupteur** : `LegacyPlus.ini`, `[Magie] SoutienFee`, `oui` / `non` (ou `true` /
  `false`), `non` par défaut — le jeu de 1998.
- **Les bonus touchent tout le groupe.** Vitesse, Matéria feu, Matéria terre, Matéria air, Matéria
  eau, Coussin d'air et Superman, lancés par la Fée, posent leur effet sur **chaque membre vivant du
  groupe**, sans condition de distance, au moment où le jeu d'origine le pose sur sa cible unique.
- **Les malus touchent tous les ennemis, à coup sûr.** Dodo (sommeil), Chant sirènes (confusion),
  Écrase (incantation empêchée), Gros sabots et Ralenti (lenteur), lancés par la Fée, restent
  déclenchés **au contact**, comme aujourd'hui ; mais au contact, l'effet part sur **toutes les
  entités de scène vivantes à moins de `0x5dc` de la Fée** (la portée du sort Zombie), et il passe
  **toujours** : ni jet de sauvegarde, ni immunité d'espèce, **boss compris**.
- **Quatre projectiles au lieu d'un.** Dodo, Chant sirènes et Écrase font partir quatre bulles —
  devant, derrière, à gauche, à droite —, chacune avec sa tête chercheuse.
- **La version individuelle d'une paire est divisée par deux.** Quand un sort individuel a déjà son
  équivalent de groupe dans le jeu, sa **durée** est divisée par deux : **Vitesse** (Turbo est sa
  version de groupe) et **Gros sabots** (Ralenti est sa version répétée). Turbo et Ralenti gardent
  leur durée.
- **Hors du tweak** : les écoles du Prêtre et du Sorcier, Blindé, Champ magique, Escargot, Turbo
  (déjà de groupe), et toute magie lancée par un autre que la Fée.

## Capabilities

### New Capabilities
- `gameplay-fairy-support` : la Fée en soutien du groupe — ses bonus sur tout le groupe, ses malus
  sur tous les ennemis proches et à coup sûr, quatre projectiles pour trois de ses malus, la durée
  des versions individuelles divisée par deux, et l'interrupteur qui active le tout.

### Modified Capabilities
(aucune : les tweaks existants ne changent pas ; `[Magie] ScalingMagique` et
`DeplacementEnIncantation` restent indépendants de celui-ci)

## Impact

- `OpenForesia/src/LegacyPlus/SoutienFee.cs` (nouveau, LP-015), `Coutures.cs` (quatre coutures),
  `ReglagesLegacyPlus.cs` (la clé).
- `OpenForesia/src/Blaze/Screens/State03/Spells/School2Elementalist.cs` : une ligne de couture à
  chaque pose d'effet des onze sorts concernés et aux trois engendrements de bulle.
- `OpenForesia/src/Blaze/Screens/State03/Combat/StatusEffects.cs` et `CombatPairing.cs` : une ligne
  de couture devant le masque d'immunité et le jet de sauvegarde des effets 8, 10, 11 et 20, et sur
  la durée des effets 19 et 20.
- `OpenForesia.Installer/Reglages/LegacyPlus.ini` (la clé commentée), `Documentation/Reference/LegacyPlus.ini.md`,
  `Documentation/LegacyPlus/legacy.md` (LP-015), `HISTORY.md`.
- Aucune donnée du jeu, aucun nouveau tirage aléatoire dans le code du tweak.
