# Vérifications de test remises à plus tard

Ce que l'on sait devoir tester un jour, sans que ça bloque le chantier en cours. Distinct de
`test-protocol.md`, qui dit **comment** tester ; ici on garde **ce qui reste à vérifier**.

Une entrée se retire quand elle est tranchée, et son résultat part dans `REVERSE_STATUS.md`.

---

## 1. La chaîne complète de création de personnage, à la main, sans échafaudage

**Ouvert depuis le 2026-08-17. Réduit le 2026-08-18** : tout le parcours *sauf* la répartition des
points est désormais joué en scripté, de bout en bout, jusqu'à l'écriture du fichier de sauvegarde —
la recette est dans `test-protocol.md` (« Le parcours de création complet est scriptable »). Deux
personnages de suite, deux fichiers distincts, aucun plantage. Ne reste ouvert que ce qui l'était
vraiment : relever les appuis exacts de l'écran de répartition pour se passer de
`OPENFORESIA_FORCE_CONFIRM_STATS`, et la conjonction « Gurkha » + SELECT maintenu.

### La question

Peut-on, dans OpenForesia aujourd'hui, mener une création de personnage de bout en bout **au
clavier/manette uniquement**, jusqu'à ce que la partie démarre (état de jeu 1) ?

### Pourquoi elle est ouverte

Tous les parcours qui ont atteint l'état de jeu 1 jusqu'ici passaient par les raccourcis de test
(`OPENFORESIA_FORCE_SUBSTATE=7`, `OPENFORESIA_FORCE_CONFIRM_STATS=1`, et `OPENFORESIA_FORCE_CARD_NAME=1`
jusqu'à son retrait le 2026-08-18), qui sautent la moitié des écrans. En pilotage manuel, les essais
du 2026-08-17 sont allés jusqu'au **dé de bonus** puis à la **répartition des points**, et se sont
arrêtés là — non pas sur une erreur, mais parce que la séquence `-Then` devenait trop longue pour
un test scripté (une vingtaine de points à répartir, soit une trentaine d'appuis).

Rien n'indique que ça casse. Simplement, personne ne l'a fait.

### Ce que ça débloquerait

C'est la dernière pièce manquante de la vérification du code de triche « Gurkha » (voir
`REVERSE_STATUS.md`, 2026-08-17). Les deux conditions du chemin A sont mesurées séparément dans le
jeu qui tourne :

- taper « Gurkha » sur l'écran de saisie du nom met bien `DAT_005633a8` à 0 ;
- maintenir SELECT (F1) pose bien le bit que `RunGameState1Entry` teste, **au branchement même**
  (`pad0=0x01` relevé sur place).

Ne manque que la conjonction des deux dans une même partie, et elle bute uniquement sur la longueur
de la création. Le `&&` lui-même ne fait aucun doute.

### Comment s'y prendre

À la main, c'est l'affaire de quelques secondes : lancer le jeu, créer un personnage nommé
`Gurkha`, aller jusqu'au bout de la création, et **maintenir F1** pendant que la partie se lance.
Si l'écran de composition de groupe de mise au point apparaît, c'est réglé.

En scripté, il faudrait d'abord relever les appuis exacts de l'écran de répartition des points
(ajouter un point, passer d'une statistique à l'autre, confirmer), puis les enchaîner dans un
`-Then`. Les jetons nécessaires existent déjà (`TYPE:`, `KBENTER`, `HOLD:`/`FREE:`, `-Hold`).

### Note utile en attendant

`OpenForesia/save/TEST` était **suivi par git** depuis le commit « Trouvé un code triche Gurkha », et
tant qu'il était là, tout essai de sauvegarde plantait au deuxième lancement
(`ResetCardTransferStatus`/`Ordinal_67`) — voir le piège correspondant dans `test-protocol.md`.
Supprimé de l'arbre de travail le 2026-08-18, en même temps que l'échafaudage qui le produisait ;
**la suppression reste à valider par un commit**. Le piège lui-même n'a pas disparu : il se reproduit
dès qu'on rejoue deux fois exactement le même personnage.
