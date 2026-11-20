// check-bestiairev2.mjs — le controle du compendium des objets.
//
//   node OpenForesia.Tools/Html/check-bestiairev2.mjs
//
// Il EXECUTE la page, il ne relit pas seulement ses donnees (la lecon de check-bestiaire-page.mjs) :
// le noyau (/*__CORE_BEGIN__*/ … /*__CORE_END__*/) tourne sous node, et tout ce qu'il produit —
// carte, ligne de tableau, fiche, comparaison, sous chaque filtre et chaque tri — est relu. Echoue sur :
//   - « undefined », « NaN », « [object Object] », « null » dans le texte produit ;
//   - tout renvoi technique a l'ecran (decalage hexa, « octet ») : la page parle a un joueur ;
//   - une vignette qui ne se decode pas, un pourcentage hors de [0, 100] ;
//   - un script de page qui ne se compile pas.

import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const here = path.dirname(fileURLToPath(import.meta.url));
const html = fs.readFileSync(path.join(here, 'bestiairev2.html'), 'utf8');

let failures = 0;
const fail = (msg) => { failures++; if (failures <= 40) console.error('ECHEC : ' + msg); };

// ---------------------------------------------------------------- le script entier se compile
const script = html.slice(html.indexOf('<script>') + 8, html.lastIndexOf('</script>'));
try { new Function(script); } catch (e) { fail(`le script de la page ne se compile pas : ${e.message}`); }

// ---------------------------------------------------------------- le noyau et les donnees
const core = html.slice(html.indexOf('/*__CORE_BEGIN__*/'), html.indexOf('/*__CORE_END__*/'));
const dataStart = html.indexOf('/*__DATA__*/') + '/*__DATA__*/'.length;
const dataEnd = html.indexOf(';\n', dataStart);
const DATA = JSON.parse(html.slice(dataStart, dataEnd));
const Core = new Function(core + '\nreturn Core;')();
const items = DATA.items;

if (items.length < 300) fail(`seulement ${items.length} objets`);

// Les texte produits, aplatis : on retire les balises pour ne relire que ce qu'un joueur voit, mais on
// garde les attributs a part (title, alt) qui s'affichent aussi.
const visible = (h) => h.replace(/<[^>]*>/g, ' ');
const attrs = (h) => [...h.matchAll(/\s(?:title|alt)="([^"]*)"/g)].map((m) => m[1]).join(' ');
const BAD = [/undefined/, /\bNaN\b/, /\[object Object\]/, /\bnull\b/];
const JARGON = [/0x[0-9a-f]+/i, /\+0x/, /\boctets?\b/i, /\bopcode\b/i, /\bFUN_/, /\bDAT_/];
function scan(where, h) {
  const text = visible(h) + ' ' + attrs(h);
  for (const r of BAD) if (r.test(h)) fail(`${where} : ${r} dans le rendu`);
  for (const r of JARGON) if (r.test(text)) fail(`${where} : renvoi technique ${r} a l'ecran`);
}

const url = (it) => it.i ? 'data:image/png;base64,AAAA' : '';
let decoded = 0;
for (const it of items) {
  const name = Core.displayName(it);
  if (!it.n) fail(`objet ${it.id} sans nom`);
  if (Core.decodeIcon(it.i)) decoded++; else fail(`${name} : vignette illisible`);
  scan(`carte de ${name}`, Core.cardHtml(it, url(it), false));
  scan(`fiche de ${name}`, Core.detailHtml(it, url(it), false));
  scan(`fiche de ${name} (en comparaison)`, Core.detailHtml(it, '', true));
  for (const d of it.drops) {
    if (!(d.r56 >= 0 && d.r56 <= 100 && d.r100 >= 0 && d.r100 <= 100)) fail(`${name} : part de butin hors bornes chez ${d.m} (${d.r56} / ${d.r100})`);
    if (!(d.o >= 0 && d.o <= 100)) fail(`${name} : chance d'objet hors bornes chez ${d.m} (${d.o})`);
  }
  for (const c of it.chests) if (!(c.p > 0 && c.p <= 100)) fail(`${name} : chance de coffre hors bornes (${c.p})`);
}

// ---------------------------------------------------------------- chaque filtre, chaque tri
const base = { q: '', type: '', cls: '', elt: '', src: '', zone: '' };
const states = [base];
for (const [t] of Core.TYPES) states.push({ ...base, type: t });
Core.CLASSES.forEach((_, k) => states.push({ ...base, cls: String(k) }));
Core.ELEMENTS.forEach((_, k) => states.push({ ...base, elt: String(k) }));
for (const [s] of Core.SOURCES) states.push({ ...base, src: s });
Core.ZONES.forEach((_, k) => states.push({ ...base, zone: String(k) }));
for (const q of ['potion', 'epee', 'Épée', 'feu', 'Goblin', 'zzzz']) states.push({ ...base, q });

for (const st of states) {
  const list = Core.filter(items, st);
  const label = JSON.stringify(Object.fromEntries(Object.entries(st).filter(([, v]) => v !== '')));
  if (st.q !== 'zzzz' && st.src !== 'p' && !list.length) fail(`filtre ${label} : aucun objet`);
  for (const key of Object.keys(Core.SORTS)) for (const dir of [1, -1]) {
    const sorted = Core.sort(list, key, dir);
    if (sorted.length !== list.length) fail(`tri ${key} perd des objets`);
  }
  scan(`tableau ${label}`, Core.tableHtml(Core.sort(list, 'att', -1), url, null, 'att'));
}
if (Core.filter(items, { ...base, q: 'epee' }).length !== Core.filter(items, { ...base, q: 'Épée' }).length)
  fail(`la recherche ne fait pas abstraction des accents`);

// Le tri par ATT met bien la plus forte arme en tete.
const top = Core.sort(Core.filter(items, { ...base, type: 'Arme' }), 'att', -1);
if (top[0].a < top[top.length - 1].a) fail('le tri par ATT est inverse');

// ---------------------------------------------------------------- la comparaison
for (let i = 0; i + 4 < items.length; i += 37) scan(`comparaison a partir de ${items[i].n}`, Core.compareHtml(items.slice(i, i + 4), url));

// ---------------------------------------------------------------- des valeurs connues
// Relevees par lecture des scripts et des fiches (Bestiaire/LISEZMOI.md) : elles fixent le
// derouleur d'effets et la jointure du butin.
const find = (n) => items.find((it) => it.n === n);
const expect = (n, pred, what) => { const it = find(n); if (!it) fail(`${n} absent`); else if (!pred(it)) fail(`${n} : ${what}`); };
expect('Petite potion', (it) => it.use && it.use.includes('Rend 40 à 80 PV à un allié'), 'devrait rendre 40 à 80 PV');
expect('Elixir des esprits', (it) => it.use && it.use.includes('Rend 20 à 40 PM à un allié'), 'devrait rendre 20 à 40 PM');
expect('Aile de Séraphin', (it) => it.use && it.use.includes('Rend tous les PV à tout le groupe'), 'devrait tout rendre au groupe');
expect('Bonbon aux plantes', (it) => it.use && !it.use.some((l) => l.startsWith('Rend tous')), 'un soin plafonne lu comme un plein');
expect('Gulwick', (it) => it.el[2] === 25 && it.c === 0x40, 'Terre +25, Nain seul');
expect('Médaillon d\'or', (it) => it.res[1] === 24 && it.res[3] === 24, 'resistances 24 / 24');
// Les pouvoirs : un releve fait a la main sur le script de chacun (2026-11-20).
const pw = (n, w, c, l) => expect(n, (it) => it.sec && it.sec.w === w && it.sec.c === c && it.sec.l.includes(l), `pouvoir : ${JSON.stringify(find(n) && find(n).sec)}`);
pw('Pentacle de prince', 'Au début d’une incantation', '', 'Durée d’incantation réduite de 25 %');
pw('Pentacle magique', 'Au début d’une incantation', '', 'Durée d’incantation réduite de 50 %');
pw('Couronne du soleil', 'Quand le sort part', '', 'Coût en PM du sort réduit de 50 %');
pw('Corne du diable', 'Quand le sort part', '', 'Coût en PM du sort réduit de 10 %');
pw('Gulwick', 'Au moment de frapper au corps à corps', '1 chance sur 5 · coûte 8 PM', 'Pétrifie la cible, à coup sûr sauf si elle y est immunisée');
pw('Plume dominicale', 'Quand le porteur tombe au combat', '', 'Ressuscite le porteur avec tous ses PV');
pw('Aquachâle', 'Quand le porteur subit des dégâts', 'face aux dégâts de Feu', 'Réduit les dégâts reçus : on leur retire l’affinité de l’assaillant pour cet élément, puis on les divise par 2 (il en reste au moins 1)');
pw('Béquille', 'Au moment de frapper au corps à corps', '', 'Pétrifie la cible à chaque coup, sans jet de résistance ni immunité possible');
pw('Gants sacrés', 'Au moment de frapper au corps à corps', '', 'Les coups portent pleinement, même contre les monstres que l’arme ne blesse d’ordinaire qu’au quart');
pw('La massacreuse', 'Au moment de frapper au corps à corps', '1 chance sur 12 · sauf contre Jeune dragon, Troll, Dragon rouge, Méchant ours, Behemoth, Sylphide, Enchanteresse, Durahan… (56 monstres)', 'Retire 10 PM au porteur');
pw('Hache de Mars', 'Au moment de frapper au corps à corps', 'contre Ténébreux, Sacuba, Le gros poilu, Lordémon', '+55 ATT');
pw('Harpe d\'or', 'Au moment de frapper au corps à corps', 'contre Gorgon, Varan, Basirisk', '+60 ATT');
pw('Bâton des sages', 'Quand un sort ou un effet du porteur vise une cible', 'coûte 3 PM', '+30 MATT');
pw('Plume dominicale', 'Quand le porteur tombe au combat', '', '1 chance sur 2 : l’objet est consommé');
pw('Bouclier réflecteur', 'Quand un sort ou un effet vise le porteur', '1 chance sur 4 · contre un sort de magie (Sorcier, Prêtre ou Fée)', 'Le sort est annulé : il n’a aucun effet sur le porteur');
pw('Bouclier du feu', 'Quand un monstre attaque le porteur au corps à corps', '1 chance sur 5 · contre un monstre dont l’élément dominant est le Feu', 'Rend des PV au porteur : un huitième de l’affinité du monstre pour cet élément, plus 1');
pw('Durandal', 'Au moment de frapper au corps à corps', '1 chance sur 4', 'Repousse la cible : une onde de choc la projette en l’air (moins un gros monstre)');
pw('Armure de Satan', 'Quand le porteur va être frappé au corps à corps', '', 'DEF augmentée du nombre de monstres tués par le porteur, divisé par 20');
pw('Baguette de la féerie', 'Au moment de frapper au corps à corps', '', 'ATT augmentée d’un tiers du niveau de la cible');
pw('Marteau de Thor', 'Au moment de frapper au corps à corps', '', 'coûte 8 PM : ATT augmentée de 20');

// ---------------------------------------------------------------- les conseils par classe
const byId = new Map(items.map((it) => [it.id, it]));
if (!Array.isArray(DATA.advice) || DATA.advice.length !== 8) fail('les conseils ne couvrent pas les huit classes');
else {
  scan('conseils, toutes les classes', Core.adviceHtml(DATA.advice, byId, url, ''));
  for (const a of DATA.advice) {
    const who = Core.CLASSES[a.c];
    scan(`conseils pour ${who}`, Core.adviceHtml(DATA.advice, byId, url, String(a.c)));
    if (!a.builds.length || a.builds.length > 3) fail(`${who} : ${a.builds.length} builds`);
    for (const b of a.builds) for (const t of ['Arme', 'Armure', 'Accessoire']) if (!b.slots.some((sl) => sl.t === t)) fail(`${who} / ${b.name} : aucun conseil pour l'emplacement ${t}`);
    for (const b of a.builds) for (const sl of b.slots) for (const p of sl.picks) {
      const it = byId.get(p.id);
      if (!it) { fail(`${who} : objet conseille inconnu`); continue; }
      if (!(it.c & (1 << a.c))) fail(`${who} : ${it.n} conseille sans pouvoir s'equiper`);
      if (it.t !== sl.t) fail(`${who} : ${it.n} n'est pas un(e) ${sl.t}`);
      if (!it.drops.length && !it.chests.length) fail(`${who} : ${it.n} conseille alors qu'il ne s'obtient pas en jeu`);
      if (it.n === 'Masque de la mort') fail(`${who} : le Masque de la mort tue son porteur`);
      if (it.n === 'Anneau de Satan' && (![3, 7].includes(a.c) || /malédiction/.test(b.name))) fail(`${who} : l'Anneau de Satan l'empeche d'attaquer`);
    }
  }
  const first = (c, t) => DATA.advice[c].builds[0].slots.find((sl) => sl.t === t);
  const nain = first(6, 'Arme');
  if (!nain || byId.get(nain.picks[0].id).n !== 'Gulwick') fail('Nain : Gulwick devrait etre la premiere arme');
  const sorcier = first(3, 'Arme');
  const elfe = DATA.advice[5].builds[0];
  const names = (t) => elfe.slots.find((sl) => sl.t === t).picks.slice(0, t === 'Accessoire' ? 2 : 1).map((p) => byId.get(p.id).n).join(' + ');
  if (elfe.name !== 'Polyvalente' || names('Arme') !== 'Rapière de Zeus' || names('Armure') !== 'Bouclier des élémentaux' || names('Accessoire') !== 'Amulette des runes + Anneau des éléments')
    fail('Elfe : la Sylphide polyvalente validee a change');
  if (!sorcier || byId.get(sorcier.picks[0].id).n !== 'Bâton d\x27Apollon') fail('Sorcier : le Bâton d\x27Apollon devrait etre la premiere arme (MATT +82)');
}

console.log(`${items.length} objets, ${decoded} vignettes decodees, ${states.length} filtres x ${Object.keys(Core.SORTS).length} tris rendus.`);
if (failures) { console.error(`${failures} echec(s).`); process.exit(1); }
console.log('Controle reussi.');
