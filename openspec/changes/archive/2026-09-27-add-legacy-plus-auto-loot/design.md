## Context

Voir `proposal.md`, et `add-legacy-plus-tweaks-difficulty/design.md` pour le cadre. Relevé :

- **Le butin naît dans `DespawnSceneEntity_0051a740`** (`Tick/WorldSimulationStep.cs`), dans le
  pool `0x005f7b60` / table `0x005f7de0` (0x40 emplacements de 0x9c octets), copie de l'entité
  morte, donc elle porte en `+9` le **tueur** que le jeu retient (le même octet sert au tirage du
  butin). Trois sortes, par leurs drapeaux :
  - `0x80081000` — **le cristal d'expérience** (un, quand aucun objet ne tombe) ;
  - `0x90081000` — **les pièces** (zéro à trois « éclats ») ;
  - `0x80089000` — **l'objet**, c'est-à-dire le coffre (bit `0x8000`).

  Le commentaire « rien que de l'argent » sur le premier est trompeur : c'est le ramassage qui fait
  foi, et il donne de l'expérience au premier et de l'argent au second.
- **Le ramassage est côté joueur** (`PartyMemberStep.cs:1351`) : chaque membre, à chaque image,
  reçoit de `CollectEntityHit_005197D0` l'entité que sa collision désigne. Si
  `drapeaux & 0x80080000 == 0x80080000` et que `0x8000` est absent, le jeu donne le butin (bit
  `0x10000000` absent : expérience partagée par les membres debout ; posé : argent dans la bourse
  `+0x26c` du membre, bornée à 99 999), éteint l'entité, rend l'emplacement et joue le son 7. Avec
  `0x8000`, c'est l'ouverture du coffre.
- **Le vol** (`TickEntityTable_005f7de0`) : vitesse verticale `+0x54` ramenée à zéro quand le rebond
  devient trop faible ; durée de vie `+0x10` (200 à 229 images pour les pièces et le cristal)
  décomptée jusqu'à l'extinction, avec un fondu sous 12.

## Goals / Non-Goals

**Goals :** ramasser pièces et cristaux sans marcher dessus, avec les récompenses exactes du jeu.

**Non-Goals :** les coffres (jamais) ; les objets posés par les scripts ; un effet visuel
d'aimant (le butin ne bouge pas, il est ramassé là où il est).

## Decisions

### D1. Désigner le butin au lieu de le donner

```csharp
byte* hit = LegacyPlus.Coutures.ButinAtteint(slot, CollectEntityHit_005197D0(slot), Mem.Entity(0x005f7de0));   // LP-005
```

La couture rend l'entité que le membre « touche ». Par défaut, celle de la collision. Le tweak n'y
substitue un butin que si la collision n'a rien rendu de ramassable. Tout le reste — le partage de
l'expérience, la bourse, le son, la libération — est **le code d'origine**, qui s'exécute juste
après sur l'entité désignée. *Écarté* : réécrire la récompense dans le tweak (deux copies d'une règle
du jeu, qui divergeraient) ; attirer le butin vers les joueurs (il faudrait leurs positions, et
une collision sur le trajet reste aléatoire).

La table du pool est passée par la ligne legacy (`Mem.Entity(0x005f7de0)`) : le tweak ne va pas
chercher les globales du jeu lui-même.

### D2. Ce qui est ramassable

`drapeaux & 0x80088000 == 0x80080000` : actif, ramassable, **pas un coffre**. Et **posé** : vitesse
verticale `+0x54` nulle — le rebond est fini, on a vu les pièces jaillir.

### D3. À qui

**Le cristal** est désigné à n'importe quel membre debout : son expérience est partagée par le code
d'origine, qui la répartit lui-même.

**Les pièces vont à tour de rôle** aux membres debout (le « debout » du partage d'expérience :
`0x80000000` posé, `0x50000000` absent), de 1 à 4 selon l'équipe. Le tweak retient l'image où
chaque membre est passé debout par le ramassage, et le membre dont c'est le tour est le premier vu
debout à cette image ou à la précédente, en partant du suivant du dernier servi. Un KO saute son
tour, et retrouve sa place relevé. Filet : une pièce qui commence à s'estomper (durée de vie
`+0x10` sous 60 images) est à qui la prend. Un butin par membre et par image.

*Première version, écartée au premier essai* : les pièces au **tueur** retenu en `+9`. En jeu à
quatre, Vincent a vu le joueur 1 tout ramasser : l'octet `+9` est justement le point
`TODO(REVERSE)` de `DespawnSceneEntity_0051a740`, dont l'écrivain n'est pas identifié partout.

L'horloge est `ImageSuivante` (la couture de LP-004, appelée par le legacy en tête du pas de
simulation quel que soit le réglage de LP-004), à laquelle le tweak s'empile.

### D4. `[Butin] RamassageAuto = non | oui`

Un booléen ; `non` = jeu d'origine.

## Risks / Trade-offs

- **[Portée]** : le ramassage vaut pour toute la sous-zone, même hors de l'écran. C'est voulu, et
  c'est ce qui évite la corvée.
