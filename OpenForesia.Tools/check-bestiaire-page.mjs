// Verifie la page du bestiaire en EXECUTANT son rendu, pas en relisant ses donnees.
//
// La distinction n'est pas theorique : la vue Objets a affiche « undefined » pour la description,
// l'ATT et la DEF alors qu'un controle sur la charge utile passait. La ligne construite par
// `render()` ne transportait pas ces champs, et seul un controle qui appelle vraiment `render()`
// puis relit le HTML produit pouvait le voir.
//
//   node OpenForesia.Tools/check-bestiaire-page.mjs
//
// Sort en code 1 des qu'un « undefined » ou un « NaN » apparait dans le HTML rendu.

import { readFileSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import vm from 'node:vm';

const root = join(dirname(fileURLToPath(import.meta.url)), '..');
const html = readFileSync(join(root, 'Documentation/Reference/Bestiaire/bestiaire.html'), 'utf8');

const open = html.lastIndexOf('<script>');
const script = html.slice(open + '<script>'.length, html.lastIndexOf('</script>'));

// Un DOM minimal : assez pour que le script s'execute, pas plus. Ce qui nous interesse est la
// chaine posee dans body.innerHTML.
class El {
  constructor(tag = 'div') {
    this.tag = tag;
    this.children = [];
    this.dataset = {};
    this.style = { setProperty() {} };
    this.innerHTML = '';
    this.textContent = '';
    this.placeholder = '';
    this.offsetHeight = 60;
  }
  appendChild(c) { this.children.push(c); return c; }
  setAttribute(k, v) { this[k] = v; }
  removeAttribute(k) { delete this[k]; }
  addEventListener() {}
  querySelectorAll() { return []; }
  getBoundingClientRect() { return { top: 0 }; }
}

const byId = {};
const document = {
  documentElement: new El('html'),
  getElementById(id) { return (byId[id] ??= new El()); },
  querySelector() { return new El(); },
  querySelectorAll() { return []; },
  createElement(tag) { return new El(tag); },
};

const sandbox = {
  document,
  console,
  addEventListener() {},
  scrollTo() {},
  scrollY: 0,
  Math, JSON, Set, Map, Array, Object, String, Number, Boolean,
};
sandbox.window = sandbox;

const context = vm.createContext(sandbox);
// Le harnais est concatene au script : celui-ci est du code de plus haut niveau, donc ses `let`
// et ses fonctions restent accessibles.
const harness = `
  const out = [];
  function grab(label) { out.push([label, byIdBody().innerHTML]); }
  function byIdBody() { return document.getElementById('body'); }

  // Tout n'est pas pose par innerHTML : les puces de zone sont construites element par element,
  // et c'est ainsi qu'une puce « Zundefined » a echappe a un controle qui ne lisait que le
  // tableau. On aplatit donc aussi les elements crees par script.
  function flatten(el) {
    return (el.textContent || '') + (el.innerHTML || '') +
           el.children.map(flatten).join(' ');
  }
  out.push(['puces de zone', flatten(document.getElementById('zones'))]);
  out.push(['compteurs', flatten(document.getElementById('counters'))]);

  setView('mon');
  grab('monstres, liste');
  openKey = MON[0].nom; render();
  grab('monstres, deplie ' + MON[0].nom);

  setView('obj');
  grab('objets, liste');
  openKey = OBJ[0].nom; render();
  grab('objets, deplie ' + OBJ[0].nom);
  const glaive = OBJ.find(o => o.nom === 'Glaive');
  if (glaive) { openKey = 'Glaive'; render(); grab('objets, deplie Glaive'); }

  // Un objet qu'AUCUN monstre ne lache : il n'existe que par un coffre, et la vue Objets doit
  // quand meme le porter. C'est le cas que le filtre sur le nombre de sources perdait en silence.
  const chestOnly = OBJ.find(o => o.src.length === 0 && (o.csrc || []).length > 0);
  if (chestOnly) {
    openKey = chestOnly.nom; render();
    grab('objets, deplie objet de coffre seul (' + chestOnly.nom + ')');
  }

  // La vue des coffres, liste puis depliage du premier qui contient quelque chose.
  openKey = null;
  setView('cof');
  grab('coffres, liste');
  const withLot = COF.find(c => (c.lots || []).length > 0);
  if (withLot) {
    openKey = withLot.zone + '/' + withLot.sz + '/' + withLot.id + '/' + withLot.mem;
    render();
    grab('coffres, deplie ' + withLot.id);
  }
  openKey = null;
  setView('obj');

  // Un filtre de zone doit laisser passer des lignes. Une puce qui vide le tableau est un
  // symptome de valeur de filtre inexistante, pas d'un jeu de donnees vide.
  openKey = null;
  for (const v of ['obj', 'mon']) {
    setView(v);
    for (const z of ZONES) {
      zoneFilter = z; render();
      const markup = byIdBody().innerHTML;
      out.push([v + ', zone Z' + z, markup.length ? markup : 'VIDE']);
    }
  }
  zoneFilter = null;

  out;
`;

const rendered = vm.runInContext(script + harness, context, { filename: 'bestiaire.html' });

let failed = false;
for (const [label, markup] of rendered) {
  const bad = ['undefined', 'NaN', '[object Object]', 'VIDE'].filter(t => markup.includes(t));
  const status = bad.length ? 'ECHEC — ' + bad.join(', ') : 'ok';
  if (bad.length) failed = true;
  console.log(String(markup.length).padStart(7), 'octets  ', label.padEnd(32), status);
}

// Des controles de fond : le depliage doit porter ce que le jeu affiche.
function expect(label, needles) {
  const found = rendered.find(([l]) => l.includes(label));
  if (!found) { console.log('        introuvable', label); failed = true; return; }
  for (const needle of needles) {
    const ok = found[1].includes(needle);
    if (!ok) failed = true;
    console.log('        attendu   ', JSON.stringify(needle).padEnd(34), ok ? 'ok' : 'ECHEC');
  }
}

expect('Glaive', ['Courte épée à deux tranchants.', '>9<', 'ATT', 'DEF']);

// Les zones se nomment, elles ne se numerotent plus. Le releve du CD donne l'orthographe.
expect('puces de zone', ['La mine abandonnée', 'La forêt', 'Le labyrinthe des morts', 'La grotte scellée']);
for (const [label, markup] of rendered) {
  const leftover = markup.match(/\bZ\d+\b/);
  if (leftover) {
    failed = true;
    console.log('        ECHEC     ', label.padEnd(32), 'indice de zone non traduit : ' + leftover[0]);
  }
}
// Les huit elements doivent apparaitre nommes, cote objet comme cote monstre.
const ELEMENTS = ['Feu', 'Eau', 'Terre', 'Air', 'Lumière', 'Ténèbres', 'Sacré', 'Mal'];
expect('objets, deplie', ELEMENTS);
expect('monstres, deplie', ELEMENTS);

// Les huit classes aussi : la fiche disait « 0 1 2 3 4 5 6 7 », elle nomme desormais qui equipe.
const CLASSES = ['Guerrier', 'Prêtre', 'Voleur', 'Sorcier', 'Chasseur', 'Elfe', 'Nain', 'Fée'];
expect('objets, deplie Glaive', CLASSES);

// Et la bonne doit etre allumee : le Glaive est un poignard, +0x12 = 0x04, donc le seul bit 2.
{
  const found = rendered.find(([l]) => l.includes('objets, deplie Glaive'));
  const on = found ? [...found[1].matchAll(/<span class="kls on"[^>]*>([^<]*)</g)].map(m => m[1]) : [];
  const ok = on.length === 1 && on[0] === 'Voleur';
  if (!ok) failed = true;
  console.log('        attendu   ', '"Glaive → Voleur seul"'.padEnd(34), ok ? 'ok' : 'ECHEC : ' + JSON.stringify(on));
}

// La vue des coffres : elle existe, elle est peuplee, et son depliage porte un contenu chiffre.
expect('coffres, liste', ['Coffre ', 'donne un objet']);
expect('coffres, deplie', ['Contenu possible', 'numéro de coffre', 'chance']);

// Un objet de coffre seul doit dire d'ou il vient — et dire qu'aucun monstre ne le lache.
expect('objet de coffre seul', ['Aucun monstre', 'Dans les coffres', 'Coffre ']);

// Aucune vue ne doit produire de pourcentage vide ou de nom manquant.
for (const [label, markup] of rendered) {
  if (!label.startsWith('coffres')) continue;
  if (/>s*%/.test(markup) || markup.includes('Coffre NaN')) {
    failed = true;
    console.log('        ECHEC     ', label.padEnd(32), 'valeur manquante dans la vue des coffres');
  }
}

process.exit(failed ? 1 : 0);
