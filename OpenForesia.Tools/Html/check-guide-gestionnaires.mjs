// check-guide-gestionnaires.mjs — le controle du guide des essais.
//
//   node OpenForesia.Tools/Html/check-guide-gestionnaires.mjs
//
// Il EXECUTE la page (LISEZMOI.md, regle 3) : le noyau sur toutes les cartes, puis la page entiere
// dans un faux DOM, onglet par onglet et zone par zone. Il echoue sur « undefined », « NaN », un
// point de plan hors du cadre, un compteur qui ne tombe pas juste, et tout jargon technique a
// l'ecran — l'interface s'adresse a un joueur.

import fs from 'node:fs';
import path from 'node:path';
import vm from 'node:vm';
import { fileURLToPath } from 'node:url';

const HERE = path.dirname(fileURLToPath(import.meta.url));
const html = fs.readFileSync(path.join(HERE, 'guide-gestionnaires.html'), 'utf8');

let failures = 0;
const fail = m => { failures++; console.log('ECHEC  ' + m); };
const ok = m => console.log('ok     ' + m);

const JARGON = /.{0,30}(\+?0x[0-9a-f]{2,}|FUN_|gestionnaire|sous-type|sous-zone|handler|table 0x|\bAD48\b|\bB[0-2]C8\b).{0,30}/i;

// ------------------------------------------------------------------ 1. le noyau et les donnees
const script = html.slice(html.lastIndexOf('<script>') + 8, html.lastIndexOf('</script>'));
const core = script.slice(script.indexOf('/*__CORE_BEGIN__*/'), script.indexOf('/*__CORE_END__*/'));
const dataSrc = /const DATA = \/\*__DATA__\*\/(\{[^\n]*\});/.exec(script);
if (!dataSrc) { fail('marqueur de donnees introuvable dans la page'); process.exit(1); }
const DATA = JSON.parse(dataSrc[1]);

const ctx = vm.createContext({});
vm.runInContext(core + '\nthis.CORE = CORE;', ctx);
const CORE = ctx.CORE;

{
  let items = 0, plans = 0;
  for (const c of DATA.cards) {
    if (!c.items.length) fail(`carte ${c.zone}:${c.sub} sans element`);
    const p = CORE.plan(c);
    if (p) {
      plans++;
      for (const d of [...p.chests, ...p.monsters, ...p.decor, ...p.stars]) {
        if (!(d.px >= 0 && d.px <= 190 && d.pz >= 0 && d.pz <= 190)) { fail(`carte ${c.zone}:${c.sub} : point hors du plan (${d.px}, ${d.pz})`); break; }
      }
    }
    c.items.forEach((it, i) => {
      items += it.count ?? 1;
      const h = CORE.hint(it, p && p.stars.some(s => s.n === i + 1) ? i + 1 : null);
      if (!h || /undefined|NaN/.test(h)) fail(`carte ${c.zone}:${c.sub}, element ${i + 1} : consigne « ${h} »`);
    });
  }
  if (items !== DATA.counts.find) fail(`compteur « a aller chercher » ${DATA.counts.find} pour ${items} elements sur les cartes`);
  const sum = DATA.counts.find + DATA.counts.impossible + DATA.counts.unknown;
  if (sum !== DATA.counts.total) fail(`les trois compteurs font ${sum}, le total ${DATA.counts.total}`);
  if (!failures) ok(`noyau : ${items} consignes sur ${DATA.cards.length} cartes, ${plans} plans, compteurs justes (${sum})`);
}

// ------------------------------------------------------------------ 2. la page, dans un faux DOM
{
  class El {
    constructor(id) { this.id = id; this._html = ''; this.style = {}; this.checked = false; this.parentElement = { style: {} }; }
    set innerHTML(v) { this._html = v; }
    get innerHTML() { return this._html; }
    get textContent() { return this._html.replace(/<svg[\s\S]*?<\/svg>/g, '').replace(/<[^>]*>/g, ' ').replace(/&[a-z]+;/g, ' '); }
  }
  const els = {};
  const document = { getElementById: id => (els[id] ??= new El(id)), addEventListener() {} };
  const storage = new Map();
  const localStorage = { getItem: k => storage.get(k) ?? null, setItem: (k, v) => storage.set(k, v) };
  const page = vm.createContext({ document, localStorage, console });
  vm.runInContext(script + '\nthis.__t = { state, render };', page);
  const t = page.__t;

  let views = 0;
  const zones = [-1, ...DATA.zones.map(z => z.zone)];
  for (const tab of ['find', 'impossible', 'unknown']) {
    for (const zone of zones) {
      t.state.tab = tab; t.state.zone = zone;
      try { t.render(); } catch (e) { fail(`onglet ${tab}, zone ${zone} : leve ${e.message}`); continue; }
      views++;
      const text = els.main.textContent + els.tabs.textContent + els.zones.textContent;
      // « Ã© » : un double encodage UTF-8 — arrive des qu'un outil relit le modele en Latin-1.
      const bad = text.match(/.{0,40}(undefined|NaN|\[object|Ã[\u0080-¿]|â€).{0,40}/);
      if (bad) fail(`onglet ${tab}, zone ${zone} : affiche « ${bad[0].trim()} »`);
      const tech = text.match(JARGON);
      if (tech) fail(`onglet ${tab}, zone ${zone} : jargon technique « ${tech[0].trim()} »`);
    }
  }
  if (!failures) ok(`page : ${views} vues rendues (3 onglets x ${zones.length} filtres), sans « undefined », « NaN » ni jargon`);
}

console.log(failures ? `\n${failures} echec(s)` : '\nTout est vert.');
process.exit(failures ? 1 : 0);
