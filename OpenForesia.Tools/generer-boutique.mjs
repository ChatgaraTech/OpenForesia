// Génère le modèle Boutique.ini (le catalogue de la boutique de l'auberge, legacy ++) depuis le bestiaire.
//
//   node OpenForesia.Tools/generer-boutique.mjs
//
// Lit Documentation/Reference/Bestiaire/{objets,butin_par_objet,coffres,modeles}.csv (écrits par
// `OpenForesia.Tools bestiaire`) et écrit OpenForesia.Installer/Reglages/Boutique.ini. Le fichier
// produit est un POINT DE DÉPART : Vincent y retouche les prix et les lignes à la main. Ne pas
// relancer par-dessus un fichier retouché sans comparer.
//
// Le prix d'un objet = l'or qu'on gagne par monstre dans la zone où l'objet apparaît en premier,
// multiplié par (20 + puissance), arrondi. La puissance = ATT + DEF + la moitié des bonus positifs.
// L'or par monstre = 1,5 pièce en moyenne (rand() & 3) × (rang + 9) / 10 par pièce (le ramassage,
// PartyMemberStep.cs), le rang étant le rang médian des monstres de la zone (modeles.csv, +0x10).
import fs from "fs";

const B = "Documentation/Reference/Bestiaire/";
const SORTIE = "OpenForesia.Installer/Reglages/Boutique.ini";

function parse(l) { const o = []; let c = "", q = false; for (const ch of l) { if (ch === "\"") q = !q; else if (ch === "," && !q) { o.push(c); c = ""; } else c += ch; } o.push(c); return o; }
const lire = f => { const L = fs.readFileSync(B + f, "utf8").split(/\r?\n/).filter(Boolean); return { h: parse(L[0]), r: L.slice(1).map(parse) }; };

const objets = lire("objets.csv"), butin = lire("butin_par_objet.csv"), coffres = lire("coffres.csv"), modeles = lire("modeles.csv");
const NOMS_ZONES = { 1: "La mine abandonnée", 2: "La montagne du dragon de feu", 3: "La forêt", 4: "La vieille tour",
  5: "Le labyrinthe des morts", 6: "Le château de l'immortel", 7: "Les ruines", 8: "La vallée d'argent",
  9: "Les ruines dans le lac", 10: "La grotte scellée" };

// Rang médian des monstres (PV > 1) par zone.
const rangs = {};
for (const m of modeles.r) { const z = +m[0]; if (z < 1 || +m[5] <= 1) continue; (rangs[z] ??= []).push(+m[4]); }
const rang = {};
for (const z in rangs) { const a = rangs[z].sort((x, y) => x - y); rang[z] = a[Math.floor(a.length / 2)]; }
const orParMonstre = z => 1.5 * (rang[z] + 9) / 10;

// La zone où l'objet s'obtient en premier dans la progression (le plus petit rang de zone).
const zones = {};
const noter = (nom, z) => { z = +z; if (z >= 1) (zones[nom] ??= new Set()).add(z); };
for (const b of butin.r) noter(b[0], b[1]);
const iObjet = coffres.h.indexOf("objet");
for (const c of coffres.r) noter(c[iObjet], c[0]);
const premiereZone = nom => zones[nom] ? [...zones[nom]].sort((a, b) => rang[a] - rang[b])[0] : null;

// Les équipements à fiche française, un par nom.
const vus = new Map();
for (const o of objets.r) {
  const nom = o[3];
  if (!/[a-zàâçéèêëîïôûùüÿœ]/i.test(nom) || vus.has(nom) || /[=;\[\]]/.test(nom)) continue;
  const att = +o[5], def = +o[6];
  const bonus = o[8].split(";").map(Number).filter(x => x > 0).reduce((a, b) => a + b, 0);
  if (att <= 0 && def <= 0 && bonus <= 0) continue;
  vus.set(nom, { nom, att, def, bonus, zone: premiereZone(nom) });
}

const arrondi = p => p < 100 ? Math.round(p / 5) * 5 : p < 1000 ? Math.round(p / 10) * 10 : Math.round(p / 50) * 50;
const prix = e => Math.min(99999, Math.max(5, arrondi(orParMonstre(e.zone) * (20 + e.att + e.def + e.bonus / 2))));
const detail = e => `ATT ${e.att}, DEF ${e.def}, bonus ${e.bonus}`;

const obtenables = [...vus.values()].filter(e => e.zone).sort((a, b) => rang[a.zone] - rang[b.zone] || prix(a) - prix(b));
const jamais = [...vus.values()].filter(e => !e.zone).sort((a, b) => a.nom.localeCompare(b.nom, "fr"));

const L = [];
L.push("; OpenForesia — la boutique de l'auberge (« legacy ++ »)");
L.push(";");
L.push("; Une boutique dans l'auberge (quête 0, carte 82 « Le coffre ») : l'armoire, à gauche du coffre,");
L.push("; vend les objets listés ici. Dans le jeu de 1998, rien ne s'achète : l'or que l'on");
L.push("; ramasse ne sert à rien. Ce fichier est lu par le jeu, jamais écrit : vos modifications sont");
L.push("; conservées, y compris quand vous réinstallez.");
L.push(";");
L.push("; Décisions (Vincent, 2026-09-27) :");
L.push(";  - le joueur qui ouvre la boutique paie avec SA bourse et reçoit l'objet ;");
L.push(";  - l'objet est remis comme par un coffre (la boîte « vous obtenez… », inventaire plein géré) ;");
L.push(";  - pas de personnage : le point de vente est l'ARMOIRE de la carte 82 — on s'en approche et on");
L.push(";    appuie sur le bouton d'action (la carte 82 ne charge aucun modèle de personnage) ;");
L.push(";  - le catalogue et les prix viennent de ce fichier, et se retouchent à la main.");
L.push(";");
L.push("; Comment les prix ont été calculés (OpenForesia.Tools/generer-boutique.mjs) :");
L.push(";   prix = or gagné par monstre dans la zone où l'objet apparaît en premier × (20 + puissance)");
L.push(";   puissance = ATT + DEF + la moitié des bonus de caractéristiques");
L.push(";   or par monstre = 1,5 pièce en moyenne × (rang des monstres de la zone + 9) / 10");
L.push("; Un objet de la forêt coûte donc quelques dizaines à quelques centaines de pièces, un objet");
L.push("; de la fin du jeu quelques milliers. La bourse d'un personnage plafonne à 99 999.");
L.push(";");
L.push("; Pour retirer un objet de la vente : mettre « ; » devant sa ligne. Pour changer un prix :");
L.push("; changer le nombre. Le nom doit rester exactement celui du jeu.");
L.push("");
L.push("[Boutique]");
L.push("; oui = l'armoire de la carte 82 est une boutique ; non = jeu d'origine.");
L.push("Actif = non");
L.push("");
L.push("[Catalogue]");
L.push("; Nom de l'objet = prix en pièces d'or");
let zoneCourante = null;
for (const e of obtenables) {
  if (e.zone !== zoneCourante) {
    zoneCourante = e.zone;
    L.push("");
    L.push(`; --- ${NOMS_ZONES[e.zone]} (zone ${e.zone}, monstres de rang ~${rang[e.zone]}, ~${orParMonstre(e.zone).toFixed(1)} or par monstre)`);
  }
  L.push(`${e.nom} = ${prix(e)}`.padEnd(48) + `; ${detail(e)}`);
}
L.push("");
L.push("; --- Objets qu'on ne trouve NULLE PART dans le jeu d'origine (ni coffre ni butin) : objets de");
L.push("; test ou jamais distribués. Désactivés ; retirer le « ; » pour les vendre quand même. Le prix");
L.push("; indiqué suppose la zone la plus difficile.");
const zMax = Object.keys(rang).sort((a, b) => rang[b] - rang[a])[0];
for (const e of jamais) L.push(`; ${e.nom} = ${prix({ ...e, zone: zMax })}`.padEnd(48) + `; ${detail(e)}`);
L.push("");

fs.writeFileSync(SORTIE, L.join("\r\n"), "utf8");
console.log(`${SORTIE} : ${obtenables.length} objets en vente, ${jamais.length} désactivés`);
