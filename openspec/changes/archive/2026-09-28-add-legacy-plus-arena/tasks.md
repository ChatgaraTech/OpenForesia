## 1. La mesure (lecture seule, avant d'écrire)

- [x] 1.1 Relevé en jeu par Vincent : le téléporteur est en 5/9 (section 2), au centre de la salle aux quatre piliers — le modèle 6 du paquet de décor, rang 6 (design D8)
- [x] 1.2 La naissance d'une formation : quelle fonction fait naître les créatures d'une formation de sous-zone, ce qu'elle attend (espèce, place, cap, liste), et comment appeler la même pour une vague du directeur (D3)
- [x] 1.3 Le script de 4/9 : comment il est lancé, quelles variables et quels drapeaux il écrit, et le point unique où le taire en mode arène (D6)
- [x] 1.4 Tous les tests `zone == 4 && sous-zone == 9` (et `(4, 9)` en table) du legacy : ce qu'ils déclenchent, et s'il faut les neutraliser en mode arène
- [x] 1.5 Le champ PV d'une entité de scène (type, maximum) et l'endroit où un coup d'ennemi inflige ses dégâts au groupe — les deux points des coutures du palier (D4)
- [x] 1.6 La sauvegarde : existe-t-il un moyen de sauvegarder dans 4/9 ; sinon, confirmer que la couture `LieuSauve` suffit comme filet (D6)
- [x] 1.7 Écrire les résultats dans `Documentation/LegacyPlus/legacy.md` (LP-012, étape 1) et proposer à Vincent les noms des fichiers nouveaux (téléporteur, menu, mode arène, directeur)

## 2. Lot 1 — le téléporteur et le menu, dans le hub

- [x] 2.1 Le téléporteur posé dans le hub en (−766, 1235) par une greffe de décor (D8) : le modèle 6 de 5/9 et sa page de texture, une entrée de table de décodage à nous, dessin par `DrawDepthSortedModel`
- [x] 2.2 La question « Ce téléporteur semble dangereux, on y va ? » Oui / Non à l'approche d'un membre debout ; Non et menu la ferment ; elle se repose quand le membre revient
- [x] 2.3 L'écran de menu générique tiré de l'écran de boutique (D5) ; la boutique et le rachat passent dessus sans changer de comportement
- [x] 2.4 Le menu de l'arène en trois pas — mode, palier 1 à 10, confirmation — tenu par le seul joueur qui a répondu Oui ; menu = pas précédent, puis fermeture
- [x] 2.5 `dotnet build` ; essai en jeu par Vincent : question, menu, retour arrière, boutiques inchangées — le modèle du téléporteur confirmé en jeu le 2026-09-28

## 3. Lot 2 — l'aller et le mode arène

- [x] 3.1 L'interrupteur du mode arène : armé par la confirmation du menu, levé au chargement de 4/9 qui suit, baissé à tout autre chargement (`Arene.cs`) ; portail et défaite passent par un chargement, donc le baissent aussi
- [x] 3.2 Le transfert 0/86 → 4/9 du groupe entier par la séquence QUEST du menu de mise au point, point d'entrée 0 (la salle du boss, section 0, en (100, −2900), `OpenForesia.Tools entrees 4 9`)
- [x] 3.3 La salle vidée à la naissance (D2) : aucune formation (script d'arrivée et volumes tus, pas de rencontre), coffre, effet 56, sous-type 11 et gonds 47 à 49 rendus au néant ; la scénette de sortie gardée (son état 0 la cache), rangs inchangés
- [x] 3.4 Aucun script de la sous-zone en arène (couture `ScriptTu`, ajoutée après l'essai de Vincent : la cinématique du début du combat tournait encore) ; le script d'arrivée 0x34 remplacé par « aucun », les volumes de déclenchement tus (couture `VolumesTus`) ; en Debug, le journal `[ARENE]` compare à chaque image les huit banques d'état à leur photo d'entrée
- [x] 3.5 Pas de sauvegarde en arène : couture `LieuSauve` — le lieu écrit est le hub, donc la réception d'origine
- [x] 3.6 Essai en jeu : on arrive dans une salle vide ; la vraie tour, par l'histoire, est intacte ; journal `[ARENE]` sans écriture de drapeau

## 4. Lot 3 — le directeur, le boss et le palier

- [x] 4.1 Le directeur et le programme de vagues (D3), avec un seul programme : l'Enchanteresse (formation 0, rang 0 de 4/9), née par `SpawnEntityFormation` 30 images après l'arrivée, suivie jusqu'à sa mort
- [x] 4.2 La couture de naissance : PV courants et maximum × palier, pendant la naissance d'une vague seulement (`PvALaNaissance`, borné à 65 535)
- [x] 4.3 La couture des dégâts : coup reçu par un membre du groupe d'une créature suivie, ou d'une entité d'effet qui en vient (+0x74, +0x6c), × palier (`DegatsSubis`, borné à 32 767) ; journal Debug de chaque coup reçu en arène
- [x] 4.4 Essai en jeu aux paliers 1, 2 et 10 : PV observés (MONITOR), dégâts reçus, comportement du boss identique à l'original

## 5. Lot 4 — la fin et la défaite

- [x] 5.1 Mort de la dernière vague : le monde gelé, la scénette de sortie reçoit l'état 1 (cinématique native)
- [x] 5.2 Après la cinématique (état 0x63) : le portail est la scénette elle-même, à sa place d'origine (100, −2700) plutôt qu'au centre ; un membre debout dans son rayon ramène le groupe au hub, à l'arrivée de la porte ; le chargement baisse le mode arène
- [x] 5.3 Deux coffres de part et d'autre du boss, copies du coffre de 4/9 (liste 3) nées dans la liste 0, mises de côté puis remises à la fin ; numéros 510 et 511 (le jeu : 0 à 302), bits remis à zéro à l'entrée et rendus à la sortie ; contenu provisoire = celui du coffre d'origine
- [x] 5.4 La défaite : couture neuve `DefaiteEvitee` sur le passage à l'état 9 ; résurrection de chaque membre avec tous ses PV (les écritures de `ReviveTarget_0053CD80`), retour au hub, message du panda à l'arrivée
- [x] 5.5 Essai en jeu : victoire (cinématique, portail, coffres, retour), défaite (fondu, hub, message), trois combats d'affilée, puis la vraie tour par l'histoire

## 6. Clôture

- [x] 6.1 `legacy.md` (LP-012), `LegacyPlus.ini.md` (l'arène suit `[Auberge] Hub`), `src/LegacyPlus/LISEZMOI.md`, `HISTORY.md`
- [x] 6.2 `publish-release.ps1` : la version publiée passe, sans journal (`Diagnostic.Actif`)
