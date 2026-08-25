# Notes de développeur

**Les traces humaines restées dans `Blaze.exe`.** Ce fichier n'a aucune utilité technique : c'est le
registre des choses que l'équipe de 1998 a laissées dans le binaire vendu et que la reconstruction a
retrouvées en chemin. Un mot à un collègue, un écran d'outil qu'on n'a pas retiré, une faute de
frappe partie en pressage.

Les défauts de comportement vont dans `Documentation/Reference/legacy-bug.md` ; les valeurs relevées
dans `.data` vont dans `Documentation/Reference/DataSymbols.md`. **Ici, on ne garde que ce qui est
drôle ou humain.** Une entrée par trouvaille, la plus récente en bas, avec l'adresse pour que
n'importe qui puisse aller vérifier.

---

## 1. « Contactez Imai » — la note interne expédiée dans la version vendue

**`0x00551814`**, relevée le 2026-08-24.

> 片開きのドアは現在使用出来ません。今井まで連絡ください。
>
> « La porte à un battant n'est pas utilisable pour l'instant. Contactez Imai. »

C'est le message qu'affichent les sous-types **6 et 7** de la table d'entités `0x0056adc8`
(`FUN_00473a70`, vingt-trois octets : il ignore complètement son entité et affiche l'énoncé).

**今井 est un nom propre** — celui d'un membre de l'équipe. Ce n'est donc pas une phrase de jeu
oubliée en japonais : c'est une note laissée à qui construisait les niveaux, pour qu'il sache que
ces deux types de porte n'étaient pas finis. Elle a survécu à la traduction française, au pressage
et à la distribution, parce que **personne n'était censé la lire**.

Elle répond aussi à une question posée le même jour : « pourquoi du Shift-JIS dans le binaire
français ? » Parce que ce texte-là n'a jamais été soumis à la traduction.

Et ce n'est pas un simple libellé : le flux est une scénette complète — il place la boîte, **gèle le
monde**, l'ouvre, écrit, **attend que le joueur appuie**, dégèle. Quelqu'un a pris le temps de la
mettre en forme correctement.

---

## 2. « Ça ne s'ouvre pas ! » et « Un bruit, au loin »

**`0x00551648`** et **`0x00551720`**, relevées le 2026-08-25.

Deux autres flux d'étiquettes, noyés dans la plage de paramètres d'entité `0x00551640`, et trouvés
parce qu'un gestionnaire (`FUN_00445860`) y pose un pointeur :

| Adresse | Shift-JIS | Sens |
|---|---|---|
| `0x00551648` | 開かない！？ | « Ça ne s'ouvre pas ! » |
| `0x00551720` | 遠くで音がした。 | « Un bruit, au loin. » |

Ceux-là, contrairement au précédent, sont de vraies phrases de jeu — la porte fermée à clé, et
l'indice sonore quand un mécanisme se déclenche ailleurs. **Elles sont restées en japonais dans le
binaire français.** Ce n'est pas la même histoire que la note à Imai : ici, deux chaînes de jeu ont
simplement échappé au passage de traduction, probablement parce qu'elles ne vivent pas dans le
fichier de textes mais **en dur au milieu d'un tableau de nombres**.

C'est d'ailleurs ce qui les avait rendues invisibles à la reconstruction : la plage `0x00551640`
était modélisée comme 512 octets nuls, sur l'hypothèse que les onze initialiseurs la remplissaient
entièrement. Ils remplissent tout, sauf ces trois trous-là.

---

## 3. « Invertaire »

**`0x00566B74`**, relevée le 2026-08-18. Détail complet en `legacy-bug.md`, LB-003.

La première ligne du menu objets s'intitule **« Invertaire »**. La faute est dans les données :

```
49 6e 76 65 72 74 61 69 72 65 00     I  n  v  e  r  t  a  i  r  e
```

Ses deux voisines, « Enchère » et « Quitter », sont correctes. Le détail qui achève l'affaire :
Ghidra a nommé le pointeur `PTR_s_Invertaire_00566b58` — il dérive ses noms de symbole du contenu
des chaînes, donc **la faute est confirmée une seconde fois par un outil qui ne savait pas qu'il
témoignait**.

Elle est transcrite telle quelle. Le projet reproduit les défauts d'origine ; la corriger ferait
diverger l'affichage du jeu de 1998 sans aucun gain.

---

## 4. « Gurkha » — le mode développeur ouvert en écrivant un mot

**`0x00564a40`**, relevée le 2026-08-17.

Sept octets, `47 75 72 6b 68 61 00`, logés juste après trois étiquettes de mise au point
« DPT » / « LOC » / « ANG » à `0x00564a20`. C'est une **séquence de triche**, et son mécanisme vaut
le détour.

`CheatCodeProbe` (`FUN_0051c0e0`) n'est pas branchée sur une saisie clavier. Elle est appelée sur
chaque caractère **dessiné**, par les deux moteurs de texte du jeu. Quand le filage atteint le
terminateur de « Gurkha », le verrou `DAT_005633a8` passe à 0.

Autrement dit : **il suffit de faire apparaître le mot « Gurkha » à l'écran**. Nommer un personnage
ainsi suffit — l'écran de saisie du nom le dessine caractère par caractère, et la sonde compte.

Ce que le verrou ouvre, une fois à 0 :

* le damier de mise au point dans le tampon de rendu ;
* le pilotage libre de la caméra ;
* et surtout **`RunDebugPartySelect`** (`FUN_00536270`), voir ci-dessous.

---

## 5. L'écran d'outil resté dans le jeu vendu

**`FUN_00536270`**, reconstruit le 2026-08-17.

Le chemin que « Gurkha » débloque n'est pas l'écran de création de personnage du jeu. C'est un
**outil de développeur** : deux colonnes (« MALE / FEMALE », les huit classes), un curseur « > »,
Carré ajoute un membre, Croix le retire. Le groupe de quatre est composé en deux clics, et chaque
membre naît avec **9000 points de vie et 200 points de magie**.

Il porte même son message d'erreur à lui : quand le chargement des ressources déborde, l'écran
affiche **« BUFFER OVER »** en boucle. Le libellé est dans `.data`, à côté de « FEMALE » et
« MALE ».

Un écran entier, avec sa mise en page, ses libellés et sa gestion d'erreur, laissé dans le code
pressé sur le CD — simplement gardé derrière un mot.

---

## 6. Le créneau « sous-type inconnu » qui n'est pas un `ret`

**`0x0047adc0`**, relevé le 2026-08-24.

Trois instructions, et 69 des 192 créneaux d'une table de sauts y pointent :

```
0047adc0  mov   eax,[esp+4]
0047adc4  mov   dword [eax],0
0047adca  ret
```

Ce n'est pas une souche vide. Elle **éteint** l'entité : le mot d'état part à zéro, le bit de vie
tombe, et la passe qui appelait rend le créneau au pool au tour suivant. Quelqu'un a décidé que « je
ne sais pas ce que c'est » se traiterait par « alors ça disparaît » — et l'a écrit une fois, pour
303 créneaux sur 576 répartis sur trois tables.

C'est la plus économe des décisions de conception qu'on ait croisées dans ce binaire.

---

## Ce qu'on cherche pour la suite

Rien de systématique — ces trouvailles arrivent en travaillant sur autre chose. Mais deux endroits
ont déjà rendu quelque chose deux fois, et méritent un coup d'œil quand on passe à côté :

* **les tableaux de nombres en `.data`** : les trois flux d'étiquettes de `0x00551640` y dormaient
  entre des paramètres de classe. Un `at` sur une plage supposée numérique coûte une commande ;
* **les chaînes voisines d'un libellé connu** : « Gurkha » était collée aux étiquettes de mise au
  point, « BUFFER OVER » à « MALE » / « FEMALE ». Ce qui n'était pas destiné au joueur a tendance à
  être rangé ensemble.
