## 1. La mesure (lecture seule)

- [x] 1.1 Prérequis : `add-legacy-plus-arena` terminé (directeur, programme, palier, fin, défaite)
- [x] 1.2 Lire le Grizzly et le Méchant ours et leurs appelées : chaque référence dépendante de la zone — le combat de boss de la forêt est la formation 0 de 3/5 (l'ours et deux Grizzlys), choisi entier par Vincent
- [x] 1.3 Le son 3 et l'effet 0x39 : communs à toutes les zones (banc `Base.snd`, table statique) ; le son `0x3f` et le souffle de l'ours dépendent de la zone
- [x] 1.4 Où vivent, dans le répertoire de 3/5, l'espèce, la fiche, le maillage, les animations et la texture de l'ours et du Grizzly ; leurs tailles (`OpenForesia.Tools greffe 3 5`)
- [x] 1.5 Relevé VRAM de 4/9 (`legacyplus-vram-z4-s9.txt`, 2026-09-28) : x 128..255 × y 16..223 libres, palettes lignes 288..319
- [x] 1.6 Résultats dans `legacy.md` (LP-013, étape 1) ; fichier `AreneGreffe.cs` validé par Vincent

## 2. La greffe générique

- [x] 2.1 Relire le répertoire d'une sous-zone quelconque dans l'archive, une fois, et le garder
- [x] 2.2 Charger et reloger le maillage et les animations d'une espèce dans une mémoire à nous, sous notre pagination
- [x] 2.3 Envoyer sa texture en VRAM à chaque entrée dans l'arène, et écrire l'entrée de décodage qui la désigne
- [x] 2.4 Prêter à l'espèce greffée un rang d'espèce et de fiche (tables recopiées, répertoire repointé), un maillage, un créneau d'IA ; la faire naître par le directeur depuis la formation d'origine
- [x] 2.5 Reconnaître les créatures greffées (rang d'espèce ≥ 16 dans la table de scène, en arène)

## 3. La traduction

- [x] 3.1 Couture `SonPositionnel` : un son de niveau d'une créature greffée joué sous son id greffé (échantillons ajoutés au banc de 4/9), coupé sans traduction
- [x] 3.2 Couture `EffetCoupe` : un effet `>= 0xc0` d'une créature greffée coupé (lot 1)
- [x] 3.3 Journal `[GREFFE]` : chaque référence coupée, une fois par chargement
- [x] 3.4 Couture `ButinNe` : ni objet ni pièces pour une créature greffée ; les éclats d'expérience restent

## 4. Le mode MECHANT OURS

- [x] 4.1 Le programme MECHANT OURS : une vague, l'ours et ses deux Grizzlys, à la place du boss de la tour
- [x] 4.2 Le menu de l'arène liste MECHANT OURS après ENCHANTERESSE, qui reste
- [ ] 4.3 Essai en jeu par Vincent : aspect (textures), déplacement, frappe, prise (son, éclat), cri de l'ours (le son de la forêt), souffle coupé sans plantage, PV aux paliers 1, 3 et 10, victoire (cinématique, portail, coffres), défaite, aucun butin ; journal `[GREFFE]`
- [ ] 4.4 Essai en jeu : la forêt, par l'histoire, après un combat d'arène — son combat de l'ours intact ; ENCHANTERESSE inchangée

## 5. Le souffle (lot 2)

- [ ] 5.1 Greffer `0x00413330` et `0x004134C0` dans des créneaux libres de `0x0056b0c8` / `0x0056b1c8` de 4/9, et traduire leurs deux sous-types
- [ ] 5.2 Greffer les modèles de décor 7 et `0x30` de 3/5 et leur texture ; le son `+0xa8[0x2c]`
- [ ] 5.3 Essai en jeu : le souffle comme dans la forêt (voile rouge, anneaux, effet sur le groupe)

## 6. Le recensement

- [ ] 6.1 Le script de recensement des dépendances de zone de chaque IA de monstre, en suivant les appelées (G5) — sons de niveau (id ≥ 60), effets `>= 0xc0`, sous-entités
- [ ] 6.2 Son document généré dans `Documentation/Etat/`, un boss par ligne avec son prix de greffe ; la ligne du Méchant ours confrontée à la mesure 1.2
- [ ] 6.3 Mention dans `CLAUDE.md` (tableau des documents générés)

## 7. Clôture

- [ ] 7.1 `legacy.md` (LP-013), `src/LegacyPlus/LISEZMOI.md`, `HISTORY.md`
- [ ] 7.2 `publish-release.ps1` : la version publiée passe
