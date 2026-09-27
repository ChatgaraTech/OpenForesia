## 1. Étape 2 — l'ouverture

- [x] 1.1 `ReglagesBoutique.cs` : lecture de `Boutique.ini` — `[Boutique] Actif` (oui/non, défaut non), `[Catalogue]` nom = prix, compté et journalisé
- [x] 1.2 `Coutures.cs` : `PasDuMembre(membre, pad, perChar, descripteur)`, défaut : rien ; rétablie en cas d'échec
- [x] 1.3 `Boutique.cs` (LP-008) : déclenchement (design D2), boîte (D3), mise en scène (D4), appui consommé ; journal `legacyplus-boutique.log`
- [x] 1.4 La couture dans `TickPartyMember`, marquée `// Couture legacy ++ (LP-008)` ; `git diff` ne montre qu'elle
- [x] 1.5 `dotnet build OpenForesia.sln` sans nouvel avertissement ; `Boutique.ini` avec `Actif = oui` dans `bin\Debug\net10.0\`
- [x] 1.6 Documentation : `legacy.md`, `LegacyPlus.ini.md` (ou une référence `Boutique.ini.md`), `ARCHITECTURE.md`, `LISEZMOI.md`, `HISTORY.md`

## 2. Étape 2 — la vérification en jeu (Vincent, depuis Rider)

- [x] 2.1 Devant l'armoire, face à elle, bouton d'action : la boîte s'ouvre, le jeu est gelé, la main revient à la fermeture
- [x] 2.2 Dos à l'armoire, ou ailleurs : le bouton d'action fait comme avant
- [x] 2.3 `Actif = non` : rien ne change

## 3. Étape 3a — l'écran

- [x] 3.1 `BoutiqueEcran.cs` : liste de 10 objets par page, curseur, prix, bourse, message, pied de page ; police 8×8 et rectangle semi-transparent (design D6)
- [x] 3.2 `Coutures.cs` : `DessinSurImpression(otBase, curseur)` ; la couture dans `DrawWorldMapFrame` (D7)
- [x] 3.3 Manette lue dans le dessin (D8) ; ouverture par `Boutique.cs` au lieu de la boîte ; gel à l'ouverture, dégel à la sortie
- [x] 3.4 Achat vérifié et journalisé seulement (D9)
- [x] 3.5 Vérification en jeu (Vincent) : l'écran s'affiche lisible, se parcourt (haut/bas/pages), l'achat est refusé sans or et journalisé sinon, la sortie rend le jeu

## 4. Étape 3b — les fiches et les icônes

- [x] 4.1 `CatalogueObjets.cs` : l'index nom → fiche et vignette, lu dans `FrenchSYSTEM.INI` et `BLAZE.ALL` au premier accès (design D10)
- [x] 4.2 L'aperçu de l'icône de l'objet sous le curseur, en rectangles plats (D11)
- [x] 4.3 Vérification en jeu (Vincent) : le journal annonce 272 objets trouvés ; l'icône de l'objet sous le curseur est reconnaissable

## 5. Étape 3c — l'achat réel

- [x] 5.1 `BoutiqueAchat.cs` : le choix de l'emplacement et le rangement repris du coffre, l'icône envoyée à la place du personnage, la bourse débitée, l'objet identifié (design D12)
- [x] 5.2 L'écran appelle l'achat réel et affiche son résultat (acheté, pas assez d'or, inventaire plein, introuvable)
- [x] 5.4 L'installeur dépose `Boutique.ini` (désactivé), sans écraser celui du joueur
- [x] 5.3 Vérification en jeu (Vincent) : la bourse baisse ; l'objet apparaît dans l'inventaire avec son icône et son nom ; il s'équipe ; un second exemplaire d'un objet empilable s'empile ; inventaire plein refusé ; l'objet survit à une sauvegarde puis un rechargement
