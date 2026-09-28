// Génère les quatre fichiers de boutique des marchands du hub (LP-010, legacy ++) depuis le bestiaire.
//
//   node OpenForesia.Tools/generer-boutique.mjs
//
// Lit Documentation/Reference/Bestiaire/{objets,butin_par_objet,coffres,modeles}.csv (écrits par
// `OpenForesia.Tools bestiaire`) et écrit OpenForesia.Installer/Reglages/LegacyPlus/Boutique*.ini.
// Les fichiers produits sont un POINT DE DÉPART : Vincent y retouche les prix et les lignes à la
// main. Ne pas relancer par-dessus des fichiers retouchés sans comparer.
//
// La catégorie d'un objet est lue dans sa fiche (colonne `octets`, octet +0x13) : 0x01 arme,
// 0x02 armure, 0x04 bouclier, 0x08 accessoire ; aucun des quatre : objet utilisable. Le jeu refait
// ce tri à la lecture (un objet mis dans le mauvais fichier n'est pas proposé).
//
// Les prix. Un équipement : l'or gagné par monstre dans la zone où l'objet apparaît en premier,
// multiplié par (20 + puissance), puissance = ATT + DEF + la moitié des bonus positifs. Un objet
// utilisable : cet or × 12. L'or par monstre = 1,5 pièce en moyenne × (rang + 9) / 10 par pièce (le
// ramassage, PartyMemberStep.cs), le rang étant le rang médian des monstres de la zone (modeles.csv,
// +0x10). Un prix déjà présent dans l'ancien Reglages/Boutique.ini (LP-008) est repris tel quel.
import fs from "fs";

const B = "Documentation/Reference/Bestiaire/";
const SORTIE = "OpenForesia.Installer/Reglages/LegacyPlus/";
const ANCIEN = "OpenForesia.Installer/Reglages/Boutique.ini";

function parse(l) { const o = []; let c = "", q = false; for (const ch of l) { if (ch === "\"") q = !q; else if (ch === "," && !q) { o.push(c); c = ""; } else c += ch; } o.push(c); return o; }
const lire = f => { const L = fs.readFileSync(B + f, "utf8").split(/\r?\n/).filter(Boolean); return { h: parse(L[0]), r: L.slice(1).map(parse) }; };

const objets = lire("objets.csv"), butin = lire("butin_par_objet.csv"), coffres = lire("coffres.csv"), modeles = lire("modeles.csv");
const NOMS_ZONES = { 1: "La mine abandonnée", 2: "La montagne du dragon de feu", 3: "La forêt", 4: "La vieille tour",
  5: "Le labyrinthe des morts", 6: "Le château de l'immortel", 7: "Les ruines", 8: "La vallée d'argent",
  9: "Les ruines dans le lac", 10: "La grotte scellée" };

// Les prix déjà écrits — retouchés par Vincent — sont repris : ceux des quatre fichiers s'ils
// existent, sinon ceux de l'ancienne boutique de l'armoire (LP-008, retirée).
const anciens = new Map();
for (const f of [ANCIEN, ...["BoutiqueObjets", "BoutiqueAccessoires", "BoutiqueArmures", "BoutiqueArmes"].map(n => `${SORTIE}${n}.ini`)]) {
  if (!fs.existsSync(f)) continue;
  for (const l of fs.readFileSync(f, "utf8").split(/\r?\n/)) {
    const m = /^([^;\[][^=]*?)\s*=\s*(\d+)/.exec(l);
    if (m && m[1].trim() !== "Actif") anciens.set(m[1].trim(), +m[2]);
  }
}

// Rang médian des monstres (PV > 1) par zone.
const rangs = {};
for (const m of modeles.r) { const z = +m[0]; if (z < 1 || +m[5] <= 1) continue; (rangs[z] ??= []).push(+m[4]); }
const rang = {};
for (const z in rangs) { const a = rangs[z].sort((x, y) => x - y); rang[z] = a[Math.floor(a.length / 2)]; }
const orParMonstre = z => 1.5 * (rang[z] + 9) / 10;
const zoneFacile = Object.keys(rang).sort((a, b) => rang[a] - rang[b])[0];
const zoneDure = Object.keys(rang).sort((a, b) => rang[b] - rang[a])[0];

// La zone où l'objet s'obtient en premier dans la progression ; « 0 » = seulement dans l'auberge.
const zones = {};
const noter = (nom, z) => { z = +z; if (z >= 0) (zones[nom] ??= new Set()).add(z); };
for (const b of butin.r) noter(b[0], b[1]);
const iObjet = coffres.h.indexOf("objet");
for (const c of coffres.r) noter(c[iObjet], c[0]);
const premiereZone = nom => {
  if (!zones[nom]) return null;
  const hors = [...zones[nom]].filter(z => z >= 1).sort((a, b) => rang[a] - rang[b]);
  return hors.length ? hors[0] : +zoneFacile;            // trouvé seulement à l'auberge : prix de la forêt
};

// Un objet utilisable n'a ni ATT ni DEF, et la zone ne dit rien de sa valeur : les objets courants
// sont au catalogue de presque toutes les zones. Son prix vient d'un barème par FAMILLE, reconnue
// dans la description de sa fiche — un point de départ, à retoucher.
const FAMILLES = [
  { nom: "effets permanents", prix: 1500, re: /Augmente (la FO|VO|INT|RAP|PM MAX|l'EV au maximum|le PM au maximum)|Augmentation de l'AT - usage unique|\(Augmente/ },
  { nom: "cristaux d'arme", prix: 400, re: /Arme reçoit l'élément|puissance d'une arme/ },
  { nom: "résurrection", prix: 300, re: /Revitalisation|revitalisant/i },
  { nom: "soins complets", prix: 250, re: /totale|complète|au maximum|de l'EV et du PM|PM et l'EV/ },
  { nom: "objets de combat", prix: 150, re: /pouvoir destructif|Active|Provoque|Paralyse|Hypnose|Effet|Anti-sortilèges|Barrière|Augmente|Transfert|Peut causer|renommée|Médecine du guerrier/ },
  { nom: "soins", prix: 60, re: /Régén|Restaure|Petite Potion|potion/i },
];
const HERBE = { nom: "herbes du Chasseur", prix: 20 };
const famille = e => (e.b13 & 0x20) ? HERBE : FAMILLES.find(f => f.re.test(e.desc)) ?? { nom: "divers", prix: 100 };

// Les objets à fiche française, un par nom, avec leur catégorie.
const iOctets = objets.h.indexOf("octets");
const vus = new Map();
for (const o of objets.r) {
  const nom = o[3];
  if (!/[a-zàâçéèêëîïôûùüÿœ]/i.test(nom) || vus.has(nom) || /[=;\[\]]/.test(nom) || /[\u0080-¿×÷]/.test(nom)) continue;
  const b13 = parseInt(o[iOctets].substr(0x13 * 2, 2), 16);
  const att = +o[5], def = +o[6];
  const bonus = o[8].split(";").map(Number).filter(x => x > 0).reduce((a, b) => a + b, 0);
  vus.set(nom, { nom, b13, att, def, bonus, desc: o[4], zone: premiereZone(nom) });
}

const categorie = e => (e.b13 & 1) ? "armes" : (e.b13 & 6) ? "armures" : (e.b13 & 8) ? "accessoires" : "objets";
const equipement = e => categorie(e) !== "objets";

const arrondi = p => p < 100 ? Math.round(p / 5) * 5 : p < 1000 ? Math.round(p / 10) * 10 : Math.round(p / 50) * 50;
const calcule = (e, z) => equipement(e)
  ? Math.min(99999, Math.max(5, arrondi(orParMonstre(z) * (20 + e.att + e.def + e.bonus / 2))))
  : famille(e).prix;
const prix = e => anciens.get(e.nom) ?? calcule(e, e.zone);
const detail = e => equipement(e) ? `ATT ${e.att}, DEF ${e.def}, bonus ${e.bonus}` : e.desc.replace(/\s+/g, " ").slice(0, 60);

const BOUTIQUES = [
  { cat: "objets", fichier: "BoutiqueObjets.ini", marchand: "le Lutin", quoi: "les objets utilisables (potions, herbes, cristaux, balles…)" },
  { cat: "accessoires", fichier: "BoutiqueAccessoires.ini", marchand: "le Voleur", quoi: "les accessoires" },
  { cat: "armures", fichier: "BoutiqueArmures.ini", marchand: "le Kobold", quoi: "les armures et les boucliers" },
  { cat: "armes", fichier: "BoutiqueArmes.ini", marchand: "la Sylphide", quoi: "les armes" },
];

fs.mkdirSync(SORTIE, { recursive: true });
for (const b of BOUTIQUES) {
  const tous = [...vus.values()].filter(e => categorie(e) === b.cat);
  const parFamille = b.cat === "objets";
  const obtenables = tous.filter(e => e.zone).sort(parFamille
    ? (x, y) => prix(x) - prix(y) || famille(x).nom.localeCompare(famille(y).nom) || x.nom.localeCompare(y.nom, "fr")
    : (x, y) => rang[x.zone] - rang[y.zone] || prix(x) - prix(y));
  const jamais = tous.filter(e => !e.zone).sort((x, y) => x.nom.localeCompare(y.nom, "fr"));

  const L = [];
  L.push(`; OpenForesia — la boutique de ${b.marchand}, dans le hub de l'auberge (« legacy ++ », LP-010)`);
  L.push(";");
  L.push(`; ${b.marchand[0].toUpperCase()}${b.marchand.slice(1)} vend ${b.quoi}. On lui parle en se tenant devant lui, tourné vers`);
  L.push("; lui, avec le bouton d'action. Le hub s'active dans LegacyPlus.ini ([Auberge] Hub = oui).");
  L.push("; Ce fichier est lu par le jeu, jamais écrit : vos modifications sont conservées, y compris quand");
  L.push("; vous réinstallez. Le joueur qui achète paie avec SA bourse et reçoit l'objet, identifié.");
  L.push(";");
  L.push("; Le jeu relit la catégorie de chaque objet dans sa fiche : un objet d'une autre catégorie, listé");
  L.push("; ici, n'est pas proposé.");
  if (b.cat === "armes" || b.cat === "armures")
    L.push("; Chaque joueur ne voit que ce que SA classe peut porter (la fiche dit quelles classes le peuvent).");
  L.push(";");
  L.push("; Comment les prix ont été calculés (OpenForesia.Tools/generer-boutique.mjs) :");
  if (b.cat === "objets") {
    L.push(";   un barème par famille, reconnue dans la description de l'objet :");
    for (const f of [HERBE, ...[...FAMILLES].reverse()]) L.push(`;     ${f.nom} : ${f.prix}`);
    L.push(";   (les prix de l'ancienne boutique de l'armoire, s'il y en avait, sont repris)");
  } else {
    L.push(";   or par monstre = 1,5 pièce en moyenne × (rang des monstres de la zone + 9) / 10");
    L.push(";   prix = or par monstre de la zone où l'objet apparaît en premier × (20 + ATT + DEF + bonus / 2)");
  }
  L.push("; La bourse d'un personnage plafonne à 99 999.");
  L.push(";");
  L.push("; Pour retirer un objet de la vente : mettre « ; » devant sa ligne. Pour changer un prix :");
  L.push("; changer le nombre. Le nom doit rester exactement celui du jeu.");
  L.push("");
  L.push("[Boutique]");
  L.push(`; oui = ${b.marchand} se tient dans le hub et vend ; non = pas de marchand.`);
  L.push("Actif = non");
  L.push("");
  L.push("[Catalogue]");
  L.push("; Nom de l'objet = prix en pièces d'or");
  let zc = null;
  for (const e of obtenables) {
    const groupe = parFamille ? famille(e).nom : e.zone;
    if (groupe !== zc) {
      zc = groupe;
      L.push("");
      L.push(parFamille ? `; --- ${groupe}`
        : `; --- ${NOMS_ZONES[e.zone]} (zone ${e.zone}, monstres de rang ~${rang[e.zone]}, ~${orParMonstre(e.zone).toFixed(1)} or par monstre)`);
    }
    L.push(`${e.nom} = ${prix(e)}`.padEnd(48) + `; ${detail(e)}`);
  }
  if (jamais.length) {
    L.push("");
    L.push("; --- Objets qu'on ne trouve NULLE PART dans le jeu d'origine (ni coffre ni butin) : objets de");
    L.push("; test ou jamais distribués. Désactivés ; retirer le « ; » pour les vendre quand même. Le prix");
    L.push("; indiqué suppose la zone la plus difficile.");
    for (const e of jamais) L.push(`; ${e.nom} = ${anciens.get(e.nom) ?? calcule(e, zoneDure)}`.padEnd(48) + `; ${detail(e)}`);
  }
  L.push("");
  fs.writeFileSync(SORTIE + b.fichier, L.join("\r\n"), "utf8");
  console.log(`${SORTIE}${b.fichier} : ${obtenables.length} en vente, ${jamais.length} désactivés`);
}
