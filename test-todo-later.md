# Vérifications de test remises à plus tard

Ce que l'on sait devoir tester un jour, sans que ça bloque le chantier en cours. Distinct de
`test-protocol.md`, qui dit **comment** tester ; ici on garde **ce qui reste à vérifier**.

Une entrée se retire quand elle est tranchée, et son résultat part dans `REVERSE_STATUS.md`.

---

## 1. La chaîne complète de création de personnage, à la main, sans échafaudage

**Ouvert depuis le 2026-08-17.**

### La question

Peut-on, dans OpenForesia aujourd'hui, mener une création de personnage de bout en bout **au
clavier/manette uniquement**, jusqu'à ce que la partie démarre (état de jeu 1) ?

### Pourquoi elle est ouverte

Tous les parcours qui ont atteint l'état de jeu 1 jusqu'ici passaient par les raccourcis de test
(`OPENFORESIA_FORCE_SUBSTATE=7`, `OPENFORESIA_FORCE_CONFIRM_STATS=1`,
`OPENFORESIA_FORCE_CARD_NAME=1`), qui sautent la moitié des écrans. En pilotage manuel, les essais
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

`OpenForesia/save/TEST` est **suivi par git** depuis le commit « Trouvé un code triche Gurkha ».
Tant qu'il est là, tout essai de sauvegarde forcée plante au deuxième lancement
(`ResetCardTransferStatus`/`Ordinal_67`) — voir le piège correspondant dans `test-protocol.md`. Le
sortir du dépôt réglerait la gêne.
