// Verifie l'editeur de sauvegarde en l'EXECUTANT, sur les vraies sauvegardes.
//
//   node OpenForesia.Tools/Html/check-editeur-sauvegarde.mjs [dossier-save] [--png dossier]
//
// Trois etages :
//   1. le noyau, seul, contre chaque fichier Blaze-Saving* : la somme recalculee egale la somme
//      stockee, la copie de 0x3f00 est a jour, et une edition nulle (finalize, renommer a
//      l'identique) rend le fichier octet pour octet ;
//   2. des editions reelles — renommer, ajouter puis retirer un objet — et leur relecture ;
//   3. la page entiere, dans un faux DOM : chaque onglet est rendu, et le texte produit ne doit
//      contenir ni « undefined » ni « NaN ».
//
// --png ecrit les icones (carte memoire et objets) en PNG, pour les regarder : un decodage de
// palette faux ne se voit pas dans les chiffres.
//
// Sort en code 1 au premier echec.

import { readFileSync, readdirSync, writeFileSync, mkdirSync } from 'node:fs';
import { fileURLToPath } from 'node:url';
import { dirname, join } from 'node:path';
import { deflateSync } from 'node:zlib';
import vm from 'node:vm';

const here = dirname(fileURLToPath(import.meta.url));
const root = join(here, '..', '..');
const args = process.argv.slice(2);
const pngAt = args.indexOf('--png');
const pngDir = pngAt >= 0 ? args[pngAt + 1] : null;
const saveDir = args.find((a, i) => !a.startsWith('--') && (pngAt < 0 || i !== pngAt + 1)) || join(root, 'OpenForesia', 'save');

const html = readFileSync(join(here, 'editeur-sauvegarde.html'), 'utf8');
let failures = 0;
const fail = (msg) => { console.log('ECHEC  ' + msg); failures++; };
const ok = (msg) => console.log('ok     ' + msg);

// ------------------------------------------------------------------ 1. le noyau
const scriptAt = html.lastIndexOf('<script>');
const coreSrc = html.slice(html.indexOf('/*__CORE_BEGIN__*/', scriptAt), html.indexOf('/*__CORE_END__*/', scriptAt));
const ctx = vm.createContext({});
vm.runInContext(coreSrc + '\nthis.Core = Core;', ctx);
const Core = ctx.Core;

const files = readdirSync(saveDir).filter(f => f.startsWith('Blaze-Saving'));
if (!files.length) { console.log(`Aucune sauvegarde dans ${saveDir}`); process.exit(1); }

const png = (w, h, rgba) => {
  const crcTable = Array.from({ length: 256 }, (_, n) => { let c = n; for (let k = 0; k < 8; k++) c = c & 1 ? 0xedb88320 ^ (c >>> 1) : c >>> 1; return c >>> 0; });
  const crc = (buf) => { let c = 0xffffffff; for (const x of buf) c = crcTable[(c ^ x) & 255] ^ (c >>> 8); return (c ^ 0xffffffff) >>> 0; };
  const chunk = (type, data) => { const len = Buffer.alloc(4); len.writeUInt32BE(data.length); const td = Buffer.concat([Buffer.from(type), data]); const c = Buffer.alloc(4); c.writeUInt32BE(crc(td)); return Buffer.concat([len, td, c]); };
  const raw = Buffer.alloc((w * 4 + 1) * h);
  for (let y = 0; y < h; y++) { raw[y * (w * 4 + 1)] = 0; Buffer.from(rgba.buffer, rgba.byteOffset + y * w * 4, w * 4).copy(raw, y * (w * 4 + 1) + 1); }
  const ihdr = Buffer.alloc(13); ihdr.writeUInt32BE(w, 0); ihdr.writeUInt32BE(h, 4); ihdr[8] = 8; ihdr[9] = 6;
  return Buffer.concat([Buffer.from([137, 80, 78, 71, 13, 10, 26, 10]), chunk('IHDR', ihdr), chunk('IDAT', deflateSync(raw)), chunk('IEND', Buffer.alloc(0))]);
};
if (pngDir) mkdirSync(pngDir, { recursive: true });

for (const f of files) {
  const b = new Uint8Array(readFileSync(join(saveDir, f)));
  const v = Core.verify(b);
  if (!v.ok) { fail(`${f} : ${v.problems.join(', ')}`); continue; }
  ok(`${f} : somme ${v.stored} juste, copie 0x3f00 a jour — « ${Core.name(b)} »`);

  const same = b.slice(); Core.finalize(same);
  if (Buffer.compare(Buffer.from(same), Buffer.from(b)) !== 0) fail(`${f} : finalize modifie un fichier deja juste`);

  const renamed = b.slice(); Core.setName(renamed, Core.name(b)); Core.finalize(renamed);
  if (Buffer.compare(Buffer.from(renamed), Buffer.from(b)) !== 0) fail(`${f} : renommer a l'identique modifie le fichier`);

  // L'inventaire, lu comme la page le lit.
  const names = [];
  for (let i = 0; i < Core.SLOTS; i++) {
    const off = Core.slotOff(i);
    if (!Core.used(b, off)) continue;
    names.push(`${Core.itemName(b, off)} [${Core.itemKind(Core.u16(b, off + 0x12))}${Core.hasIcon(b, off) ? '' : ', sans icone'}]`);
    if (pngDir) writeFileSync(join(pngDir, `${f.replace(/[^\w@-]/g, '_')}-objet${i}.png`), png(24, 24, Core.itemIcon(b, off).rgba));
  }
  console.log(`         inventaire : ${names.join(' · ') || '(vide)'}`);
  const icon = Core.cardIcon(b);
  if (icon.opaque === 0) fail(`${f} : l'icone de carte memoire est entierement transparente`);
  if (pngDir) writeFileSync(join(pngDir, `${f.replace(/[^\w@-]/g, '_')}-carte.png`), png(16, 16, icon.rgba));
}

// ------------------------------------------------------------------ 2. des editions reelles
{
  const f = files[0];
  const b = new Uint8Array(readFileSync(join(saveDir, f)));
  const old = Core.name(b);
  Core.setName(b, 'Gurkha');
  Core.finalize(b);
  const v = Core.verify(b);
  if (Core.name(b) !== 'Gurkha') fail(`renommer : le nom lu est « ${Core.name(b)} »`);
  else if (!v.ok) fail(`renommer : ${v.problems.join(', ')}`);
  else {
    const enc = Core.encodeName('Gurkha');
    let found = false;
    for (let i = 4; i < 0x44 - enc.length && !found; i++) found = enc.every((x, k) => b[i + k] === x);
    if (found) ok(`renommer « ${old} » en « Gurkha » : nom, titre de carte memoire et somme a jour`);
    else fail('renommer : le titre de la carte memoire ne porte pas le nouveau nom');
  }

  // Ajouter un objet du catalogue dans un emplacement libre, le relire, le retirer.
  const dataSrc = html.slice(html.indexOf('/*__DATA__*/', scriptAt) + '/*__DATA__*/'.length);
  const payload = JSON.parse(dataSrc.slice(0, dataSrc.indexOf(';\n')));
  const it = payload.items.find(x => x.n === 'Armure du dragon') || payload.items[0];
  const c = new Uint8Array(readFileSync(join(saveDir, f)));
  let free = -1;
  for (let i = 0; i < Core.SLOTS; i++) if (!Core.used(c, Core.slotOff(i))) { free = i; break; }
  if (free < 0) fail('ajout : pas d\'emplacement libre dans ' + f);
  else {
    const before = c.slice();
    Core.putItem(c, Core.slotOff(free), Core.hexToBytes(it.h), 1, null);
    Core.finalize(c);
    const nm = Core.itemName(c, Core.slotOff(free));
    if (nm !== it.n) fail(`ajout : l'objet relu s'appelle « ${nm} », pas « ${it.n} »`);
    else if (!Core.verify(c).ok) fail('ajout : fichier invalide apres ajout');
    else ok(`ajout de « ${it.n} » a l'emplacement ${free + 1} : relu, somme juste`);
    // Le retrait fait comme le jeu : premier octet a zero, residus laisses. Hors de l'emplacement
    // (et de la somme, et de sa copie), rien ne doit avoir bouge.
    Core.removeItem(c, free);
    Core.finalize(c);
    const lo = Core.slotOff(free), hi = lo + 0x280;
    let stray = -1;
    for (let i = 0; i < c.length && stray < 0; i++) {
      if (i >= lo && i < hi) continue;
      if ((i >= Core.SUM && i < Core.SUM + 4) || (i >= 0x3ffc && i < 0x4000)) continue;
      if (c[i] !== before[i]) stray = i;
    }
    if (c[lo] !== 0) fail('retrait : le premier octet de l\'emplacement n\'est pas a zero');
    else if (stray >= 0) fail(`retrait : l'octet 0x${stray.toString(16)} a change hors de l'emplacement`);
    else if (!Core.verify(c).ok) fail('retrait : fichier invalide');
    else ok('retrait : emplacement libere comme le jeu le fait, rien d\'autre n\'a bouge');
  }
  if (payload.items.length < 350) fail(`catalogue : ${payload.items.length} objets seulement`);
  else ok(`catalogue : ${payload.items.length} objets, sorts ${payload.spells.map(s => s.length).join('/')}`);
  // Les fiches japonaises de la zone 0 sont ecartees par le build : aucun nom ne doit sortir
  // en charabia (du Shift-JIS lu comme du Latin-1).
  const garbled = payload.items.filter(x => !/^[A-Za-z0-9 àâäçéèêëîïôöùûüÀÂÄÇÉÈÊËÎÏÔÖÙÛÜœŒ'’.,:!?()\-+«»\/&]*$/.test(x.n));
  if (garbled.length) fail(`catalogue : ${garbled.length} nom(s) illisible(s), dont « ${garbled[0].n} »`);
  else ok('catalogue : aucun nom illisible');
  // Chaque fiche du catalogue doit se relire sous son nom, sinon l'inventaire afficherait autre chose.
  let mismatch = 0;
  for (const x of payload.items) { const s = Core.hexToBytes(x.h); if (Core.itemName(s, 0) !== x.n) mismatch++; }
  if (mismatch) fail(`catalogue : ${mismatch} fiche(s) dont le nom relu differe du CSV`);
  else ok('catalogue : chaque fiche se relit sous le nom du CSV');
  // Chaque objet du catalogue porte sa vignette du CD, et elle n'est pas vide.
  let noIcon = 0, blank = 0;
  for (const x of payload.items) {
    if (!x.i || x.i.length !== 0x140 * 2) { noIcon++; continue; }
    const buf = new Uint8Array(0x280); buf.set(Core.hexToBytes(x.i), 0x140);
    if (Core.itemIcon(buf, 0).opaque === 0) blank++;
  }
  if (noIcon || blank) fail(`catalogue : ${noIcon} objet(s) sans vignette, ${blank} vignette(s) vide(s)`);
  else ok(`catalogue : ${payload.items.length} vignettes du CD, aucune vide`);
  if (pngDir) {
    // Une planche des 40 premieres vignettes du catalogue, a regarder.
    const n = Math.min(40, payload.items.length), cols = 10, W = cols * 26, H = Math.ceil(n / cols) * 26;
    const sheet = new Uint8ClampedArray(W * H * 4);
    payload.items.slice(0, n).forEach((x, k) => {
      const buf = new Uint8Array(0x280); buf.set(Core.hexToBytes(x.i), 0x140);
      const px = Core.itemIcon(buf, 0).rgba, ox = (k % cols) * 26 + 1, oy = Math.floor(k / cols) * 26 + 1;
      for (let y = 0; y < 24; y++) for (let xx = 0; xx < 24; xx++) for (let c = 0; c < 4; c++) sheet[((oy + y) * W + ox + xx) * 4 + c] = px[(y * 24 + xx) * 4 + c];
    });
    writeFileSync(join(pngDir, 'catalogue.png'), png(W, H, sheet));
  }
}

// ------------------------------------------------------------------ 3. la page, dans un faux DOM
{
  class ClassList {
    constructor(el) { this.el = el; }
    add(...c) { const s = new Set(this.el.className.split(/\s+/).filter(Boolean)); c.forEach(x => s.add(x)); this.el.className = [...s].join(' '); }
    remove(...c) { this.el.className = this.el.className.split(/\s+/).filter(x => !c.includes(x)).join(' '); }
  }
  class Node_ {
    constructor(tag) { this.tagName = tag.toUpperCase(); this.children = []; this.attrs = {}; this.className = ''; this.style = {}; this._html = ''; this._text = ''; this.listeners = {}; this.value = ''; this.nodeType = 1; this.classList = new ClassList(this); }
    setAttribute(k, v) { this.attrs[k] = String(v); if (k === 'value') this.value = String(v); if (k === 'id') this.id = v; }
    getAttribute(k) { return this.attrs[k]; }
    append(...k) { for (const x of k) this.children.push(typeof x === 'string' ? new Text_(x) : x); }
    remove() {}
    click() {}
    addEventListener(t, f) { (this.listeners[t] ||= []).push(f); }
    set innerHTML(v) { this._html = v; this.children = []; }
    get innerHTML() { return this._html + this.children.map(c => c.innerHTML ?? c.text).join(''); }
    set textContent(v) { this._text = v; this.children = []; }
    get textContent() { return this._text + this._html.replace(/<[^>]*>/g, '') + this.children.map(c => c.textContent ?? c.text).join(''); }
    getContext() { return { putImageData() {} }; }
    querySelector(sel) {
      const m = sel.match(/^option\[value="(.*)"\]$/);
      const walk = (n) => { for (const c of n.children || []) { if (m && c.tagName === 'OPTION' && c.attrs.value === m[1]) return c; const r = walk(c); if (r) return r; } return null; };
      return walk(this);
    }
  }
  class Text_ { constructor(t) { this.text = String(t); this.nodeType = 3; } get textContent() { return this.text; } get innerHTML() { return this.text; } }
  const byId = {};
  for (const id of ['saveList', 'main', 'status', 'btnRevert', 'btnDownload', 'btnSave', 'btnDir', 'btnFiles', 'fileInput', 'toast']) byId[id] = new Node_('div');
  const document = {
    createElement: (t) => new Node_(t), createTextNode: (t) => new Text_(t),
    querySelector: (s) => byId[s.slice(1)], addEventListener() {}, body: new Node_('body'),
  };
  const pageSrc = html.slice(html.lastIndexOf('<script>') + 8, html.lastIndexOf('</script>'));
  const page = vm.createContext({ document, window: { addEventListener() {} }, ImageData: class { constructor(d, w, h) { this.data = d; } }, URL: { createObjectURL: () => '' }, Blob: class {}, setTimeout, clearTimeout, console });
  vm.runInContext(pageSrc + '\nthis.__t = { saves, addSave, renderAll, setTab: (t) => { tab = t; }, setCur: (i) => { current = i; } };', page);
  const t = page.__t;
  for (const f of files) t.addSave(f, readFileSync(join(saveDir, f)), null, null);
  const tabs = ['resume', 'perso', 'carac', 'inv', 'sorts', 'prog', 'depot'];
  for (let i = 0; i < files.length; i++) {
    t.setCur(i);
    for (const tb of tabs) {
      t.setTab(tb);
      try { t.renderAll(); } catch (e) { fail(`page : ${files[i]}, onglet ${tb} leve ${e.message}`); continue; }
      const text = byId.main.textContent + byId.saveList.textContent + byId.status.textContent;
      const bad = text.match(/.{0,40}(undefined|NaN).{0,40}/);
      if (bad) fail(`page : ${files[i]}, onglet ${tb} affiche « ${bad[0]} »`);
      // L'interface s'adresse a un joueur : aucun decalage ni octet brut a l'ecran.
      const tech = text.match(/.{0,30}([+]0x[0-9a-f]+|0x[0-9a-f]{2,}).{0,30}/i);
      if (tech) fail(`page : ${files[i]}, onglet ${tb} affiche du jargon technique « ${tech[0]} »`);
    }
  }
  if (!failures) ok(`page : ${files.length} sauvegardes x ${tabs.length} onglets rendus, sans « undefined » ni « NaN »`);
}

console.log(failures ? `\n${failures} echec(s)` : '\nTout est vert.');
process.exit(failures ? 1 : 0);
