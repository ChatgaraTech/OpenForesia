// Fabrique des PERSONNAGES DE TEST : chacun porte jusqu'a cinq equipements encore jamais essayes
// en jeu et cinq consommables a employer, et demarre dans la foret.
//
//   node OpenForesia.Tools/generer-personnages-test.mjs [--jeu <dossier du jeu>] [--par-lot 24]
//
// Le dossier du jeu est celui ou vit `save\` (par defaut OpenForesia\bin\Debug\net10.0). Il lit :
//   - les huit MODELES, un par classe, dans `save\` : de vrais personnages tout juste crees, dont on
//     garde le portrait, l'inventaire de depart retire, les masques — tout ce que la classe impose ;
//   - `Documentation/Reference/Bestiaire/objets.csv` et `vignettes.csv` : les fiches et leurs icones ;
//   - `Documentation/Etat/objets-exerces.tsv` : ce qui a deja ete essaye, et qu'on n'emporte plus.
//
// Il ecrit `save-tests\lot-01\`, `lot-02\`… a cote de `save\` — des lots de 24 personnages au plus,
// parce que l'ecran de constitution du groupe n'en liste que 32 (FUN_005126c0, 0x20 entrees) —
// et, dans chaque lot, `FICHE.txt` : les groupes de quatre, ce que chacun porte et quoi faire.
// `tests\lot-suivant.bat` fait passer les lots dans `save\` l'un apres l'autre.
//
// Les regles, toutes relues dans le code :
//   - un objet porte n'est essaye que si son SCRIPT DECLENCHE part : c'est l'evenement de sa fiche
//     (+0x84) qui dit quand — 0 toutes les vingt images, les autres au combat
//     (TriggerEquippedItems_005238E0) ;
//   - la CLASSE est respectee (octet bas du mot +0x12) et le TYPE d'emplacement aussi : une arme,
//     une armure, un bouclier, deux accessoires ;
//   - le nom de fichier est celui de +0x1fd0 : le jeu reecrit toujours sous ce nom
//     (Documentation/Reference/FormatSauvegarde.md).
//
// Le noyau du format est celui de l'editeur de sauvegarde, execute tel quel : la somme de controle
// et sa copie de 0x3f00, le titre de carte memoire, le rangement d'un objet.

import { readFileSync, writeFileSync, readdirSync, mkdirSync, rmSync, existsSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import vm from 'node:vm';

const root = join(dirname(fileURLToPath(import.meta.url)), '..');
const args = process.argv.slice(2);
const opt = (name, fallback) => { const i = args.indexOf(name); return i >= 0 ? args[i + 1] : fallback; };
const gameDir = opt('--jeu', join(root, 'OpenForesia', 'bin', 'Debug', 'net10.0'));
const perLot = Number(opt('--par-lot', 24));
// Le lieu de depart, « zone/sous-zone » : par defaut la foret, sous-zone 2 — la seule ou le CD place
// des Crotales (Bestiaire/formations.csv, sept placements), dont la morsure sert aux evenements 17
// et 18. Point d entree 0.
const [startZone, startSub] = opt("--depart", "3/2").split("/").map(Number);
const saveDir = join(gameDir, 'save');
const outDir = join(gameDir, 'save-tests');

// ------------------------------------------------------------------ le noyau de l'editeur
const html = readFileSync(join(root, 'OpenForesia.Tools', 'Html', 'editeur-sauvegarde.template.html'), 'utf8');
const at = html.lastIndexOf('<script>');
const coreSrc = html.slice(html.indexOf('/*__CORE_BEGIN__*/', at), html.indexOf('/*__CORE_END__*/', at));
const ctx = vm.createContext({});
vm.runInContext(coreSrc + '\nthis.Core = Core;', ctx);
const Core = ctx.Core;

const CLASSES = ['Guerrier', 'Pretre', 'Voleur', 'Sorcier', 'Chasseur', 'Elfe', 'Nain', 'Fee'];

// Les evenements du script declenche (+0x84), et le geste qui les produit — les appels de
// TriggerEquippedItems_005238E0 dans Combat/, Spells/ et Tick/.
const EVENTS = {
  0: "porter (se declenche seul, toutes les vingt images)",
  1: "bouton d'action de l'arme",
  2: "frapper un monstre", 3: "frapper un monstre", 20: "frapper un monstre",
  7: "toucher un monstre avec un sort ou une alteration d'etat",
  8: "lancer un sort d'etat sur un monstre (il tente d'y resister)",
  9: "lancer un sort d'etat sur un monstre (il tente d'y resister)",
  10: "se faire frapper", 12: "se faire frapper", 13: "se faire frapper",
  17: "se faire mordre par un Crotale (foret, sous-zone 2 : le depart ; 3 prises sur 10 tentent d empoisonner)",
  18: "se faire mordre par un Crotale (foret, sous-zone 2 : le depart ; 3 prises sur 10 tentent d empoisonner)",
  19: "subir l etat d une Momie (Labyrinthe des morts, sous-zones 5 a 8 : menu de mise au point, QUEST 5 puis MAP 5)",
  5: "lancer un sort", 6: "lancer un sort",
  14: "etre vise par un monstre qui choisit sa proie",
  21: "tomber a 0 PV (se faire tuer)",
};

// Les classes qui incantent (Documentation/Reference/Sorts.md) : Pretre, Sorcier, Elfe, Fee. Un objet
// que le lancement d'un sort declenche (evenements 5 et 6) ne va qu'a elles.
const CASTERS = new Set([1, 3, 5, 7]);
const SPELL_COUNT = [29, 24, 20];                     // DAT_00564600, par ecole

// ------------------------------------------------------------------ les modeles
// Un personnage neuf par classe. Ils sont gardes dans save-testsmodeles, que la regeneration
// n'efface pas ; a defaut, pris dans save, puis dans les lots deja fabriques — un personnage de
// test est un clone de son modele, dont on reecrit tout sauf ce que la classe impose.
const modelDir = join(outDir, 'modeles');
const models = {};
const lookIn = (dir) => {
  if (!existsSync(dir)) return;
  for (const f of readdirSync(dir).filter(n => n.startsWith('Blaze-Saving'))) {
    const b = new Uint8Array(readFileSync(join(dir, f)));
    if (!Core.verify(b).ok || Core.u16(b, 0x144) !== 1) continue;         // un personnage neuf
    const cls = b[0x150] & 7;
    if (!models[cls]) models[cls] = { file: f, bytes: b };
  }
};
lookIn(modelDir);
lookIn(saveDir);
if (existsSync(outDir)) for (const d of readdirSync(outDir).filter(n => /^lot-[0-9]+$/.test(n)).sort()) lookIn(join(outDir, d));
const missing = CLASSES.filter((_, c) => !models[c]);
if (missing.length) { console.log('Modeles manquants (' + modelDir + ', ' + saveDir + ') : ' + missing.join(', ')); process.exit(1); }
mkdirSync(modelDir, { recursive: true });
for (const m of Object.values(models)) if (!existsSync(join(modelDir, m.file))) writeFileSync(join(modelDir, m.file), m.bytes);

// ------------------------------------------------------------------ ce qui reste a essayer
const French = /^[A-Za-z0-9 àâäçéèêëîïôöùûüÀÂÄÇÉÈÊËÎÏÔÖÙÛÜœŒ'’.,:!?()\-+«»\/&]*$/;
const tested = new Set(readFileSync(join(root, 'Documentation', 'Etat', 'objets-exerces.tsv'), 'utf8')
  .split('\n').map(l => l.split('\t')[0].trim().toLowerCase()));
const icons = new Map(readFileSync(join(root, 'Documentation', 'Reference', 'Bestiaire', 'vignettes.csv'), 'utf8')
  .split('\n').slice(1).filter(Boolean).map(l => { const p = l.trim().split(','); return [`${p[0]}/${p[1]}/${p[2]}`, p[3]]; }));

const items = [];
const seen = new Set();
const herbs = [];
const testedBehaviour = new Map();   // comportement deja essaye -> le nom de l'objet qui l'a ete
for (const line of readFileSync(join(root, 'Documentation', 'Reference', 'Bestiaire', 'objets.csv'), 'utf8').split('\n').slice(1)) {
  const m = line.match(/^(\d+),(\d+),(\d+),"((?:[^"]|"")*)",/);
  if (!m || !French.test(m[4])) continue;                                  // les fiches japonaises
  const hex = line.slice(line.lastIndexOf(',') + 1).trim();
  if (hex.length < 0x140 * 2) continue;
  const key = hex.slice(0, 32);
  if (seen.has(key)) continue;
  seen.add(key);
  const sheet = Core.hexToBytes(hex.slice(0, 0x140 * 2));
  // Le comportement : les scripts et leur donnee, de l'entete +0x80 a la fin de la fiche.
  const behaviour = hex.slice(0x80 * 2, 0x140 * 2);
  if (tested.has(key)) { if (!testedBehaviour.has(behaviour)) testedBehaviour.set(behaviour, m[4]); continue; }
  // Les herbes du Chasseur (famille +0x86 de 0x32c9 a 0x32cf) ne s'emploient pas comme des objets :
  // ramassees par un Chasseur, elles vont dans sa reserve et partent par la posture d'herboriste,
  // comme des sorts de l'ecole 3 (StoreItemInInventory_005312D0). C'est la sonde des sorts qui les voit.
  const family = Core.u16(sheet, 0x86);
  if (family >= 0x32c9 && family <= 0x32cf) { herbs.push(m[4]); continue; }
  const icon = icons.get(`${m[1]}/${m[2]}/${m[3]}`);
  const flags = Core.u16(sheet, 0x12);
  items.push({
    name: m[4], sheet, icon: icon ? Core.hexToBytes(icon) : null, classes: flags & 0xff,
    kind: flags & 0x100 ? 'arme' : flags & 0x200 ? 'armure' : flags & 0x400 ? 'bouclier' : flags & 0x800 ? 'accessoire' : 'objet',
    event: Core.u16(sheet, 0x84), stack: Core.stackable(sheet, 0), behaviour,
  });
}

// ------------------------------------------------------------------ la repartition
// Une arme, une armure, un bouclier, deux accessoires par personnage, de sa classe ; a chaque
// tour, la classe qui emporte le plus d'objets encore libres.
const SLOTS = [['arme', 1], ['armure', 1], ['bouclier', 1], ['accessoire', 2]];
const pool = Object.fromEntries(SLOTS.map(([k]) => [k, items.filter(i => i.kind === k)]));
const consumables = items.filter(i => i.kind === 'objet');
const characters = [];

// La classe doit pouvoir porter l'objet ; un objet que le lancement d'un sort declenche va en
// priorite a un lanceur — a n'importe quelle classe permise seulement s'il n'en admet aucun.
const castTriggered = (i) => i.event === 5 || i.event === 6;
const fits = (i, c) => (i.classes >> c & 1) !== 0
  && (!castTriggered(i) || CASTERS.has(c) || ![...CASTERS].some(k => i.classes >> k & 1));

while (SLOTS.some(([k]) => pool[k].length) || consumables.length) {
  let best = -1, score = -1;
  for (let c = 0; c < 8; c++) {
    let s = 0;
    for (const [k, n] of SLOTS) s += Math.min(n, pool[k].filter(i => fits(i, c)).length);
    if (s > score) { score = s; best = c; }
  }
  if (score <= 0 && !consumables.length) break;
  const worn = [];
  for (const [k, n] of SLOTS) {
    let left = n;
    pool[k] = pool[k].filter(i => { if (left > 0 && fits(i, best)) { left--; worn.push(i); return false; } return true; });
  }
  if (score <= 0) best = characters.length % 8;          // plus d'equipement : un porteur quelconque
  characters.push({ cls: best, worn, used: consumables.splice(0, 5) });
}
// Les personnages qui ont besoin de la Momie (evenement 19) passent en dernier : un seul voyage au
// Labyrinthe suffit alors pour les derniers groupes.
{
  const needsMummy = (c) => c.worn.some(i => i.event === 19);
  characters.sort((a, b) => needsMummy(a) - needsMummy(b));
}

// ------------------------------------------------------------------ l'ecriture
const CODE = '@ABCDEFGHIJKLMNOPQRSTUVWXYZ';         // 0x40..0x5a : des lettres sures dans un nom de fichier
const code = (n) => 'TST' + CODE[1 + Math.floor(n / 676) % 26] + CODE[1 + Math.floor(n / 26) % 26] + CODE[1 + n % 26] + '_';

// Les lots precedents partent ; les modeles restent.
for (const d of readdirSync(outDir).filter(n => n.startsWith('lot-'))) rmSync(join(outDir, d), { recursive: true });
const lots = [];

characters.forEach((ch, n) => {
  const b = models[ch.cls].bytes.slice();
  const name = `T${String(n + 1).padStart(3, '0')}`;
  const fileName = 'Blaze-Saving' + code(n);

  Core.setName(b, name);
  for (let i = 0; i < 0x14; i++) b[0x1fd0 + i] = i < fileName.length ? fileName.charCodeAt(i) : 0;

  // Un lanceur connait tous les sorts de son ecole : les objets declenches par un sort le demandent.
  if (CASTERS.has(ch.cls)) {
    const school = b[0x2b5];
    for (let i = 0; i < (SPELL_COUNT[school] ?? 0); i++) b[0x160 + school * 8 + (i >> 3)] |= 1 << (i & 7);
  }

  // 100 PV et 100 PM, pleins.
  for (const o of [0x148, 0x14a, 0x14c, 0x14e]) Core.w16(b, o, 100);

  // L'inventaire de depart part ; les cinq equipements, puis les consommables, prennent sa place.
  for (let i = 0; i < Core.SLOTS; i++) b[Core.slotOff(i)] = 0;
  for (let k = 0; k < 5; k++) b[0x2b0 + k] = 0xff;
  const wornSlot = { arme: 0, armure: 1, bouclier: 2 };
  let accessory = 3, slot = 0;
  for (const it of ch.worn) {
    Core.putItem(b, Core.slotOff(slot), it.sheet, it.stack ? 1 : 0, it.icon);
    const eq = it.kind === 'accessoire' ? accessory++ : wornSlot[it.kind];
    b[0x2b0 + eq] = slot++;
  }
  for (const it of ch.used) Core.putItem(b, Core.slotOff(slot++), it.sheet, it.stack ? 3 : 1, it.icon);

  // Le lieu de reprise : --depart, point d entree 0. Seul le premier membre du
  // groupe est lu (RestorePartyGlobals) ; tous le portent, donc n'importe lequel peut mener.
  b[0x1f00] = 0; b[0x1f01] = startZone; b[0x1f02] = startSub; b[0x1f03] = 0;

  Core.finalize(b);
  if (!Core.verify(b).ok) throw new Error(`${name} : fichier invalide`);

  const lot = Math.floor(n / perLot);
  (lots[lot] ||= []).push({ ...ch, name, fileName, bytes: b });
});

mkdirSync(outDir, { recursive: true });
lots.forEach((chars, l) => {
  const dir = join(outDir, `lot-${String(l + 1).padStart(2, '0')}`);
  mkdirSync(dir, { recursive: true });
  const out = [`LOT ${l + 1} - ${chars.length} personnages, depart en zone ${startZone}, sous-zone ${startSub}`, '',
    'Crotale : autour du point de depart (foret, sous-zone 2). Momie : menu de mise au point, ligne QUEST sur 5, puis MAP sur 5. Les groupes qui en ont besoin sont a la fin.'];
  if (l === 0 && herbs.length) out.push('', `Hors de ce lot : les ${herbs.length} herbes du Chasseur (${herbs.join(', ')}) — elles partent par sa posture d'herboriste, comme des sorts ; c'est la sonde des sorts qui les voit.`);
  chars.forEach((ch, i) => {
    writeFileSync(join(dir, ch.fileName), ch.bytes);
    if (i % 4 === 0) out.push('', `==== GROUPE ${i / 4 + 1} : ${chars.slice(i, i + 4).map(c => c.name).join(', ')} ====`);
    out.push('', `${ch.name}  (${CLASSES[ch.cls]})`);
    for (const it of ch.worn) out.push(`   porte  ${it.kind.padEnd(10)} ${it.name.padEnd(28)} -> ${EVENTS[it.event] ?? `evenement ${it.event}`}`);
    for (const it of ch.used) out.push(`   emploie             ${it.name}${testedBehaviour.has(it.behaviour) ? `   (jumeau de « ${testedBehaviour.get(it.behaviour)} », deja teste)` : ''}`);
  });
  writeFileSync(join(dir, 'FICHE.txt'), out.join('\r\n') + '\r\n', 'latin1');
});

const worn = characters.reduce((s, c) => s + c.worn.length, 0), used = characters.reduce((s, c) => s + c.used.length, 0);
console.log(`${characters.length} personnages en ${lots.length} lots (${perLot} au plus) — ${worn} equipements a porter, ${used} consommables`);
console.log(`Ecrits dans ${outDir}`);
