// build-guide-gestionnaires.mjs — assemble guide-gestionnaires.html.
//
//   node OpenForesia.Tools/Html/build-guide-gestionnaires.mjs
//   node OpenForesia.Tools/Html/check-guide-gestionnaires.mjs
//
// Lit tout par guide-gestionnaires.analyse.mjs (Blaze.exe, Bestiaire/*.csv, handlers-exerces.tsv,
// le C#) et injecte le resultat dans guide-gestionnaires.template.html a la place du marqueur
// /*__DATA__*/. En node plutot qu'en PowerShell : l'analyse lit le C# et le binaire, et elle doit
// tourner a l'identique sous le controle.
//
// Aucune adresse ne part dans la page : l'interface parle a un joueur (LISEZMOI.md).

import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';
import { analyse } from './guide-gestionnaires.analyse.mjs';

const HERE = path.dirname(fileURLToPath(import.meta.url));
const ROOT = path.resolve(HERE, '..', '..');

// Les noms de zone de GestionnairesEntites.md (« ## Zone N — … »), accents rendus : le recensement
// les ecrit en ASCII pour cmd.exe.
const ACCENTS = { foret: 'forêt', vallee: 'vallée', chateau: 'château', scellee: 'scellée', abandonnee: 'abandonnée' };
const census = fs.readFileSync(path.join(ROOT, 'Documentation', 'Etat', 'GestionnairesEntites.md'), 'utf8');
const zones = [...census.matchAll(/^## Zone (\d+) — (.+)$/gm)].map(m => ({
  zone: +m[1],
  name: m[2].trim().replace(/\b\w+\b/g, w => ACCENTS[w] ?? w).replace(/'/g, '’'),
}));

const { cards, unreachable, unknown, total } = analyse();
const control = analyse({ controle: true });

const placeName = new Map(cards.map(c => [`${c.zone}:${c.sub}`, c.name]));
const zoneName = z => zones.find(x => x.zone === z)?.name ?? `Zone ${z}`;
const nice = key => {
  const [z, s] = key.split(':').map(Number);
  const name = placeName.get(key);
  return name ? `${name} (QUEST ${z}, MAP ${s})` : `${zoneName(z)}, QUEST ${z} MAP ${s}`;
};

function mergeSame(items) {
  const out = new Map();
  for (const it of items) {
    const key = JSON.stringify([it.cat, it.how, it.names, it.spots, it.seed.kind, it.seed.cat,
      it.seed.names, it.seed.spots, it.seed.spells]);
    if (out.has(key)) out.get(key).count++;
    else out.set(key, { ...it, count: 1 });
  }
  return [...out.values()];
}

const data = {
  generated: new Date().toISOString().slice(0, 10),
  zones,
  cards: cards.map(c => ({
    zone: c.zone, sub: c.sub, name: c.name,
    chests: c.chests, monsters: c.monsterSpots, decor: c.decorSpots,
    // Deux elements qui recevraient la MEME consigne n'en font qu'une ligne, avec leur nombre : la
    // mine en alignait quatre « Apparait quand Goblin agit » a la suite.
    items: mergeSame(c.items).map(it => ({
      cat: it.cat, how: it.how, names: it.names, spots: it.spots, count: it.count,
      seed: { kind: it.seed.kind, cat: it.seed.cat, names: it.seed.names, spots: it.seed.spots, spells: it.seed.spells },
      // Trois lieux au plus : un effet commun peut naitre presque partout, et la liste entiere
      // noyait la consigne.
      also: it.also.length > 3
        ? [...it.also.slice(0, 3).map(nice), `et ${it.also.length - 3} autres lieux`]
        : it.also.map(nice),
    })),
  })),
  impossible: zones.map(({ zone }) => {
    const here = unreachable.filter(u => u.zones.includes(zone));
    const decor = here.filter(u => u.why === 'jamais-pose').length;
    const monster = here.filter(u => u.why === 'aucune-espece').length;
    return { zone, decor, monster, total: decor + monster };
  }).filter(r => r.total),
  unknown: zones.map(({ zone }) => {
    const here = unknown.filter(u => u.zones.includes(zone));
    const byCat = {};
    for (const u of here) byCat[u.cat] = (byCat[u.cat] ?? 0) + 1;
    return { zone, byCat, total: here.length };
  }).filter(r => r.total),
  counts: {
    total,
    find: cards.reduce((n, c) => n + c.items.length, 0),
    impossible: unreachable.length,
    unknown: unknown.length,
  },
  control: { seen: control.total, wrong: control.unreachable.length },
};

// Un element sans zone (table statique, parent introuvable) n'apparait dans aucune ligne par zone :
// on le compte a part pour que les totaux restent justes.
const orphans = unknown.filter(u => !u.zones.length).length;
if (orphans) {
  data.unknown.push({ zone: -1, byCat: Object.fromEntries(Object.entries(
    unknown.filter(u => !u.zones.length).reduce((m, u) => (m[u.cat] = (m[u.cat] ?? 0) + 1, m), {}))), total: orphans });
  data.zones.push({ zone: -1, name: 'Partout (effets communs)' });
}

const template = fs.readFileSync(path.join(HERE, 'guide-gestionnaires.template.html'), 'utf8');
const marker = /\/\*__DATA__\*\/\{[^\n]*\};/;
if (!marker.test(template)) throw new Error('marqueur /*__DATA__*/ introuvable dans le modele');
const out = template.replace(marker, () => `/*__DATA__*/${JSON.stringify(data)};`);
fs.writeFileSync(path.join(HERE, 'guide-gestionnaires.html'), out);

console.log(`guide-gestionnaires.html : ${data.counts.find} a aller chercher dans ${data.cards.length} lieux, `
  + `${data.counts.impossible} impossibles, ${data.counts.unknown} sans piste (sur ${total}) ; `
  + `controle ${data.control.wrong} erreur(s) sur ${data.control.seen}`);
