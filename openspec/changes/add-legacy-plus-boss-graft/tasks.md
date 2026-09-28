## 1. La mesure (lecture seule)

- [ ] 1.1 Prérequis : `add-legacy-plus-arena` terminé (directeur, programme, palier, fin, défaite)
- [ ] 1.2 Lire `Ai/Forest/Grizzly.cs` et toutes ses appelées : chaque référence dépendante de la zone (sons, effets, sous-entités, tables d'animation, variables de script)
- [ ] 1.3 Le son 3 et l'effet 0x39 de la forêt : quel fichier du banc de sons, quel gestionnaire d'effet et quelle planche de texture (G3)
- [ ] 1.4 Où, dans le répertoire d'état de la zone 3, vivent le modèle, les animations, la texture et la fiche de l'espèce 3 de 3/5 ; leurs tailles
- [ ] 1.5 Relevé VRAM de 4/9 en mode arène (`ReleveVram`, Debug) : la place libre pour la texture du Grizzly et, s'il est greffé, celle de l'effet 0x39
- [ ] 1.6 Écrire les résultats dans `legacy.md` (LP-013, étape 1) et proposer à Vincent le nom du fichier de la greffe

## 2. La greffe générique

- [ ] 2.1 Relire le répertoire d'état d'une zone quelconque dans l'archive, une fois, et le garder (G2)
- [ ] 2.2 Charger et reloger le modèle et les animations d'une espèce dans une mémoire à nous
- [ ] 2.3 Envoyer sa texture en VRAM à chaque entrée dans l'arène, et écrire l'entrée de décodage qui la désigne
- [ ] 2.4 Prêter à l'espèce greffée un emplacement d'espèce de l'arène, avec sa fiche et l'adresse de son IA ; la faire naître par le directeur
- [ ] 2.5 Marquer les créatures greffées (G4)

## 3. La traduction

- [ ] 3.1 Couture sur le son positionnel : numéro traduit pour une créature greffée, identité sinon
- [ ] 3.2 Couture sur la naissance d'un effet : sous-type traduit ou coupé pour une créature greffée, identité sinon
- [ ] 3.3 Journal `[GREFFE]` (Debug) : toute référence d'une créature greffée qui n'a pas de traduction
- [ ] 3.4 La table du Grizzly : son 3 et effet 0x39 (greffés, ou coupés selon 1.3)

## 4. Le mode GRIZZLY

- [ ] 4.1 Le programme GRIZZLY : une vague, le Grizzly boss seul, au centre de l'arène
- [ ] 4.2 Le menu de l'arène liste GRIZZLY à côté d'ENCHANTERESSE, qui reste (Vincent, 2026-09-28)
- [ ] 4.3 Essai en jeu par Vincent : aspect, déplacement, frappe, prise (son, effet), PV aux paliers 1, 3 et 10, victoire (cinématique, portail, coffres), défaite ; journal `[GREFFE]` vide
- [ ] 4.4 Essai en jeu : la forêt, par l'histoire, après un combat d'arène — son Grizzly intact

## 5. Le recensement

- [ ] 5.1 Le script de recensement des dépendances de zone de chaque IA de monstre, en suivant les appelées (G5)
- [ ] 5.2 Son document généré dans `Documentation/Etat/`, un boss par ligne avec son prix de greffe ; la ligne du Grizzly confrontée à la mesure 1.2
- [ ] 5.3 Mention dans `CLAUDE.md` (tableau des documents générés)

## 6. Clôture

- [ ] 6.1 `legacy.md` (LP-013), `src/LegacyPlus/LISEZMOI.md`, `HISTORY.md`
- [ ] 6.2 `publish-release.ps1` : la version publiée passe, sans journal
