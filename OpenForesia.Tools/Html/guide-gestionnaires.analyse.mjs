// guide-gestionnaires.analyse.mjs — ce que le guide des gestionnaires sait, et d'ou il le sait.
//
// Pour chaque gestionnaire d'entite pas encore vu en jeu (handlers-exerces.tsv), dans chaque
// sous-zone qui l'installe, on cherche OU le joueur peut le faire tourner :
//
//   1. il est POSE dans la sous-zone : un enregistrement de la table +0x30 le designe
//      (Bestiaire/decor.csv, avec sa position) ;
//   2. sinon il NAIT d'un autre corps : un appel SpawnEffectEntity / SpawnCastEntity_0047AAE0 /
//      SpawnStatusEffectEntity_0047AC40 a sous-type LITTERAL, dans le C#. Chaque fonction de
//      naissance remplit sa propre table d'entites, et chaque table a sa passe :
//
//        SpawnEffectEntity               -> 0x005ff400 -> statique 0x00551c30, sinon 0x0056adc8
//        SpawnCastEntity_0047AAE0        -> 0x005edee0 -> statique 0x00551f30, sinon 0x0056aec8
//        SpawnStatusEffectEntity_0047AC40-> 0x005f2d40 -> statique 0x00552230, sinon 0x0056afc8
//
//      (CallIndexedEntityHandler, Dispatch/StaticEntityTables.cs.) Sous 0xc0 le sous-type vise
//      une table statique ; a partir de 0xc0 il deborde de trois tables et tombe dans
//      0x0056b0c8 / 0x0056b1c8 / 0x0056b2c8 — rechargees a chaque sous-zone. Le parent n'est donc
//      resolu que DANS la sous-zone ou il tourne.
//
// Ce qui n'est pas vu : un sous-type CALCULE (lu dans le placement, additionne a 0xc0…), les
// naissances par d'autres chemins (scripts de scene, verbes de zone). Ces gestionnaires-la sortent
// « origine inconnue », et c'est une reponse honnete, pas une lacune cachee.

import fs from 'node:fs';
import path from 'node:path';

import { fileURLToPath } from 'node:url';
const ROOT = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..', '..');
const R = (...p) => path.join(ROOT, ...p);

// ------------------------------------------------------------------------------------------------
// Blaze.exe : les sept tables installees par sous-zone, et les trois statiques
// ------------------------------------------------------------------------------------------------
const EXE = fs.readFileSync(R('Legacy', 'Binaries', 'Blaze.exe'));
const DATA_DELTA = 0x401a00;                       // .data : VA - offset fichier (census-entity-handlers.sh)
const u32 = va => EXE.readUInt32LE(va - DATA_DELTA);
const SEL = 0x00566d98, SEL_COUNT = 176, FATAL = 0x0056b3c8;
const EMPTY = new Set([0x00401fa0, 0x0040a600]);   // deux corps reduits a `ret`
// Un nom de creature lisible : la regle de build-editeur-sauvegarde.ps1 ($frenchName).
const FRENCH_NAME = /^[A-Za-z0-9 àâäçéèêëîïôöùûüÀÂÄÇÉÈÊËÎÏÔÖÙÛÜœŒ'’.,:!?()\-+«»/&]+$/;

export const TABLES = ['AD48', 'ADC8', 'AEC8', 'AFC8', 'B0C8', 'B1C8', 'B2C8'];
const TABLE_AT = [0x00, 0x20, 0x60, 0xa0, 0xe0, 0x120, 0x160];   // debut de chaque table, bout a bout

/** Les sept tables de la sous-zone mises bout a bout (0x1a0 entrees), ou null. */
export function installed(zone, sub) {
  // L auberge (zone 0) n a qu un jeu de tables pour toutes ses pieces — meme regle que
  // SubAreaEntityHandlers dans Bestiaire.cs.
  const i = zone === 0 ? 0 : zone * 16 + sub;
  const all = [];
  for (let col = 0; col < 7; col++) {
    const block = u32(SEL + (col * SEL_COUNT + i) * 4);
    if (block === 0 || block === FATAL) return null;
    const size = col === 0 ? 0x20 : 0x40;
    for (let k = 0; k < size; k++) all.push(u32(block + k * 4));
  }
  return all;
}

export const tableOf = slot => TABLES[TABLE_AT.findLastIndex(a => slot >= a)];

const STATIC = { C30: 0x00551c30, F30: 0x00551f30, '2230': 0x00552230 };
const staticTable = va => Array.from({ length: 0xc0 }, (_, k) => u32(va + k * 4));
const STATICS = Object.fromEntries(Object.entries(STATIC).map(([k, va]) => [k, staticTable(va)]));

// Les trois fonctions de naissance : table statique, et debut de la table haute dans le bout a bout.
const SPAWNERS = {
  SpawnEffectEntity: { arg: 1, stat: 'C30', high: 0xe0 },
  SpawnCastEntity_0047AAE0: { arg: 1, stat: 'F30', high: 0x120 },
  SpawnStatusEffectEntity_0047AC40: { arg: 3, stat: '2230', high: 0x160 },
};

// ------------------------------------------------------------------------------------------------
// Les CSV
// ------------------------------------------------------------------------------------------------
function csv(file) {
  const text = fs.readFileSync(file, 'utf8').replace(/^﻿/, '');
  const lines = text.split(/\r?\n/).filter(l => l.length);
  const head = lines.shift().split(',');
  return lines.map(line => {
    const out = []; let cur = '', q = false;
    for (let i = 0; i < line.length; i++) {
      const c = line[i];
      if (q) { if (c === '"' && line[i + 1] === '"') { cur += '"'; i++; } else if (c === '"') q = false; else cur += c; }
      else if (c === '"') q = true; else if (c === ',') { out.push(cur); cur = ''; } else cur += c;
    }
    out.push(cur);
    return Object.fromEntries(head.map((h, i) => [h, out[i]]));
  });
}

const B = (...p) => R('Documentation', 'Reference', 'Bestiaire', ...p);

// ------------------------------------------------------------------------------------------------
// Le C# : methodes, appels, naissances a sous-type litteral, et les aiguillages adresse -> methode
// ------------------------------------------------------------------------------------------------
function walk(dir, out = []) {
  for (const e of fs.readdirSync(dir, { withFileTypes: true })) {
    const p = path.join(dir, e.name);
    if (e.isDirectory()) walk(p, out); else if (e.name.endsWith('.cs')) out.push(p);
  }
  return out;
}

/** Commentaires et chaines effaces, longueurs conservees : les accolades des chaines ne comptent pas. */
function scrub(src) {
  let out = '';
  for (let i = 0; i < src.length; i++) {
    const c = src[i], n = src[i + 1];
    if (c === '/' && n === '/') { while (i < src.length && src[i] !== '\n') { out += ' '; i++; } out += '\n'; continue; }
    if (c === '/' && n === '*') { while (i < src.length && !(src[i] === '*' && src[i + 1] === '/')) { out += src[i] === '\n' ? '\n' : ' '; i++; } out += '  '; i++; continue; }
    if (c === '"' ) { out += '"'; i++; while (i < src.length && src[i] !== '"') { if (src[i] === '\\') { out += ' '; i++; } out += src[i] === '\n' ? '\n' : ' '; i++; } out += '"'; continue; }
    if (c === "'" && src[i + 2] === "'") { out += "' '"; i += 2; continue; }
    if (c === "'" && src[i + 1] === '\\' && src[i + 3] === "'") { out += "'  '"; i += 3; continue; }
    out += c;
  }
  return out;
}

function splitArgs(text, open) {        // text[open] === '(' ; rend les arguments de premier niveau
  const args = []; let depth = 0, cur = '';
  for (let i = open; i < text.length; i++) {
    const c = text[i];
    if (c === '(') { if (depth++ > 0) cur += c; continue; }
    if (c === ')') { if (--depth === 0) { args.push(cur.trim()); return args; } cur += c; continue; }
    if (c === ',' && depth === 1) { args.push(cur.trim()); cur = ''; continue; }
    cur += c;
  }
  return args;
}

function literal(arg) {
  const a = arg.replace(/\((?:byte|int|uint|ushort|short)\)/g, '').trim();
  if (/^0x[0-9a-f]+$/i.test(a)) return parseInt(a, 16);
  if (/^\d+$/.test(a)) return parseInt(a, 10);
  return null;
}

const SRC_DIR = R('OpenForesia', 'src', 'Blaze');
const methods = new Map();     // nom -> { file, body, calls:Set, spawns:[{spawner, st}] }
const dispatch = new Map();    // adresse -> Set(nom de methode)

for (const file of walk(SRC_DIR)) {
  const rel = path.relative(SRC_DIR, file).replace(/\\/g, '/');
  const text = scrub(fs.readFileSync(file, 'utf8'));

  // `case 0x…:` empiles, puis le premier appel ; et `0x… => Methode(`.
  const caseRe = /((?:\bcase\s+0x[0-9A-Fa-f]{8}\s*:\s*)+)(?:[^;{}]*?)\b([A-Za-z_]\w*)\s*\(/g;
  for (let m; (m = caseRe.exec(text));) {
    for (const a of m[1].matchAll(/0x([0-9A-Fa-f]{8})/g)) {
      const addr = parseInt(a[1], 16);
      if (!dispatch.has(addr)) dispatch.set(addr, new Set());
      dispatch.get(addr).add(m[2]);
    }
  }
  for (const m of text.matchAll(/\b0x([0-9A-Fa-f]{8})\s*=>\s*([A-Za-z_]\w*)\s*\(/g)) {
    const addr = parseInt(m[1], 16);
    if (!dispatch.has(addr)) dispatch.set(addr, new Set());
    dispatch.get(addr).add(m[2]);
  }

  // Les methodes : declaration, puis corps entre accolades ou expression jusqu'au `;`.
  const declRe = /\b(?:private|internal|public)\s+(?:static\s+)?(?:unsafe\s+)?[\w<>\[\]\*,\s]*?\b([A-Za-z_]\w*)\s*\(/g;
  for (let m; (m = declRe.exec(text));) {
    const name = m[1];
    if (/^(if|for|while|switch|return|new|sizeof|typeof)$/.test(name)) continue;
    let i = m.index + m[0].length - 1, depth = 0;
    for (; i < text.length; i++) { if (text[i] === '(') depth++; else if (text[i] === ')' && --depth === 0) break; }
    let j = i + 1;
    while (j < text.length && /\s/.test(text[j])) j++;
    let body = '';
    if (text[j] === '{') {
      let d = 0, k = j;
      for (; k < text.length; k++) { if (text[k] === '{') d++; else if (text[k] === '}' && --d === 0) break; }
      body = text.slice(j, k + 1);
      declRe.lastIndex = k;
    } else if (text.startsWith('=>', j)) {
      const k = text.indexOf(';', j);
      body = text.slice(j, k + 1);
    } else continue;

    // Les noms des parametres, dans l'ordre : le dernier identifiant de chaque argument declare.
    const params = splitArgs(text, m.index + m[0].length - 1)
      .map(p => (/([A-Za-z_]\w*)\s*(?:=[^,]*)?$/.exec(p.trim()) ?? [])[1] ?? '');

    const entry = methods.get(name) ?? { file: rel, body: '', calls: new Set(), spawns: [], params, pending: [] };
    entry.body += body;
    for (const c of body.matchAll(/\b([A-Za-z_]\w*)\s*\(/g)) entry.calls.add(c[1]);
    for (const [spawner, spec] of Object.entries(SPAWNERS)) {
      const re = new RegExp(`\\b${spawner}\\s*\\(`, 'g');
      for (let s; (s = re.exec(body));) {
        const args = splitArgs(body, s.index + s[0].length - 1);
        const arg = args[spec.arg] ?? '';
        const st = literal(arg);
        if (st !== null) { entry.spawns.push({ spawner, st }); continue; }
        // Un parametre de la methode, eventuellement plus une constante : ce sont ses appelants
        // qui donneront la valeur (voir la propagation plus bas).
        const p = paramRef(arg, params);
        if (p) entry.pending.push({ spawner, ...p });
      }
    }
    methods.set(name, entry);
  }
}

/** `subtype`, `(byte)subtype`, `(byte)(subtype + 0x10)` -> { index, add } si c'est un parametre. */
function paramRef(arg, params) {
  const a = arg.replace(/\((?:byte|int|uint|ushort|short)\)/g, '').replace(/\s+/g, '').replace(/^\((.*)\)$/, '$1');
  const m = /^([A-Za-z_]\w*)(?:\+(0x[0-9a-fA-F]+|\d+))?$/.exec(a);
  if (!m) return null;
  const index = params.indexOf(m[1]);
  if (index < 0) return null;
  return { index, add: m[2] ? Number(m[2]) : 0 };
}

// La propagation : un appel `Aide(e, 0xf3)` a une methode dont une naissance attend son parametre
// donne une naissance a sous-type litteral A L'APPELANT. Si l'appelant ne passe lui-meme qu'un de
// ses parametres, l'attente remonte d'un cran. Point fixe, borne.
for (let round = 0; round < 6; round++) {
  let grew = false;
  for (const [, caller] of methods) {
    for (const calleeName of caller.calls) {
      const callee = methods.get(calleeName);
      if (!callee || !callee.pending.length || callee === caller) continue;
      const re = new RegExp(String.raw`\b${calleeName}\s*\(`, 'g');
      for (let s; (s = re.exec(caller.body));) {
        const args = splitArgs(caller.body, s.index + s[0].length - 1);
        for (const p of callee.pending) {
          const arg = args[p.index] ?? '';
          const st = literal(arg);
          if (st !== null) {
            const v = (st + p.add) & 0xff;
            if (!caller.spawns.some(x => x.spawner === p.spawner && x.st === v)) {
              caller.spawns.push({ spawner: p.spawner, st: v }); grew = true;
            }
            continue;
          }
          const up = paramRef(arg, caller.params ?? []);
          if (up && !caller.pending.some(x => x.spawner === p.spawner && x.index === up.index && x.add === up.add + p.add)) {
            caller.pending.push({ spawner: p.spawner, index: up.index, add: up.add + p.add }); grew = true;
          }
        }
      }
    }
  }
  if (!grew) break;
}

// Une methode nommee `…_XXXXXXXX` porte l'adresse du gestionnaire qu'elle est, quand c'en est un.
for (const name of methods.keys()) {
  const m = /_([0-9A-F]{8})$/.exec(name);
  if (!m) continue;
  const addr = parseInt(m[1], 16);
  if (!dispatch.has(addr)) dispatch.set(addr, new Set());
  dispatch.get(addr).add(name);
}

/** Les naissances qu'une methode peut faire, a travers ce qu'elle appelle. */
const spawnCache = new Map();
function spawnsOf(name, seen = new Set()) {
  if (spawnCache.has(name)) return spawnCache.get(name);
  if (seen.has(name)) return [];
  seen.add(name);
  const m = methods.get(name);
  if (!m) return [];
  const out = [...m.spawns];
  for (const c of m.calls) if (c !== name && methods.has(c) && !(c in SPAWNERS)) out.push(...spawnsOf(c, seen));
  const uniq = [...new Map(out.map(s => [s.spawner + s.st, s])).values()];
  if (seen.size === 1) spawnCache.set(name, uniq);
  return uniq;
}

/** Le repertoire d'une methode, pour dire au joueur de quoi il s'agit. */
export function categoryOfHandler(addr) {
  for (const name of dispatch.get(addr) ?? []) {
    const m = methods.get(name);
    if (!m) continue;
    const dir = m.file.split('/').slice(-2, -1)[0];
    if (dir === 'Ai') return 'monstre';
    if (/Particles|AreaExitFlash|HitAndResist|TrapScripts/.test(m.file)) return 'effet';
    if (dir === 'Handlers' || /Chests/.test(m.file)) return 'mecanisme';
    if (dir === 'Cutscene') return 'scene';
    if (dir === 'Items') return 'objet';
    if (dir === 'Spells') return 'sort';
    if (dir === 'Combat') return 'combat';
    if (dir === 'Effects') return 'effet';
    if (dir === 'Entities') return 'mecanisme';
    return 'autre';
  }
  return 'autre';
}

// ------------------------------------------------------------------------------------------------
// L'assemblage
// ------------------------------------------------------------------------------------------------
export function analyse({ controle = false } = {}) {
  const places = csv(B('lieux.csv'));
  const decor = csv(B('decor.csv'));
  const species = csv(B('especes.csv'));
  const models = csv(B('modeles.csv'));
  const chests = csv(B('coffres.csv'));
  const formations = csv(B('formations.csv'));

  // Les sorts : le sous-type +0x10 d une fiche passe par la table STATIQUE 0x00551c30, la meme
  // partout (census-spells.sh). Un gestionnaire ainsi designe porte le nom du sort — lisible, il dit
  // au joueur quoi lancer au lieu de « un sort ».
  const spellNames = new Map();
  for (const s of csv(B('sorts.csv'))) {
    const h = STATICS.C30[+s.sous_type_0x10];
    const name = (s.nom ?? '').trim();
    if (h === undefined || !FRENCH_NAME.test(name)) continue;
    if (!spellNames.has(h)) spellNames.set(h, []);
    spellNames.get(h).push(name);
  }

  const tested = new Set();
  for (const line of fs.readFileSync(R('Documentation', 'Etat', 'handlers-exerces.tsv'), 'utf8').split(/\r?\n/)) {
    if (!line || line.startsWith('#')) continue;
    const [h] = line.split('\t');
    tested.add(parseInt(h, 16));   // vu N IMPORTE OU : c est le compte global de GestionnairesEntites.md
  }
  const isTested = h => tested.has(h);

  const modelName = new Map(models.map(m => [`${m.zone}:${m.sous_zone}:${m.modele}`, m.nom]));

  const subAreas = [];
  // Les sous-zones sont celles que les selecteurs connaissent (11 x 16, moins le bloc d'arret) —
  // PAS les lignes de lieux.csv, qui sont des NOMS de lieu : l'auberge en porte 65, pour une seule
  // sous-zone installee.
  const placeName = new Map(places.map(p => [`${p.zone}:${p.sous_zone}`, p.nom]));
  const innRooms = [...new Set(places.filter(p => +p.zone === 0).map(p => +p.sous_zone))];
  for (let zone = 0; zone < 11; zone++) {
    for (const sub of zone === 0 ? innRooms : [...Array(16).keys()]) {
      const all = installed(zone, sub);
      if (!all) continue;
      subAreas.push({ zone, sub, name: placeName.get(`${zone}:${sub}`) ?? '', all });
    }
  }

  // Tous les liens parent -> enfant, sous-zone par sous-zone.
  function childrenOf(parent, all) {
    const out = [];
    for (const name of dispatch.get(parent) ?? []) {
      for (const s of spawnsOf(name)) {
        const spec = SPAWNERS[s.spawner];
        const child = s.st < 0xc0 ? STATICS[spec.stat][s.st] : all[spec.high + (s.st - 0xc0)];
        if (child !== undefined && !EMPTY.has(child)) out.push(child);
      }
    }
    return out;
  }

  const allStatic = new Set(Object.values(STATICS).flat());
  const staticTodo = new Set([...allStatic].filter(h => !EMPTY.has(h) && !isTested(h)));

  // ---- 1. le releve brut, sous-zone par sous-zone --------------------------------------------
  const raw = [];
  for (const sa of subAreas) {
    const { zone, sub, all } = sa;
    const here = new Set(all);

    const parents = new Map();
    for (const p of new Set([...here, ...allStatic])) {
      if (EMPTY.has(p)) continue;
      for (const c of childrenOf(p, all)) {
        if (c === p) continue;
        if (!parents.has(c)) parents.set(c, new Set());
        parents.get(c).add(p);
      }
    }

    const monsters = new Map();
    for (const s of species.filter(s => +s.zone === zone && +s.sous_zone === sub)) {
      const h = all[+s.ia];
      // Meme regle que build-editeur-sauvegarde.ps1 : les noms japonais de l auberge, lus en
      // Latin-1, ne sont pas des noms — on les ecarte plutot que d afficher du charabia.
      const raw0 = (modelName.get(`${zone}:${sub}:${s.modele}`) ?? '').trim();
      const name = FRENCH_NAME.test(raw0) ? raw0 : '';
      if (!monsters.has(h)) monsters.set(h, new Set());
      if (name) monsters.get(h).add(name);
    }

    const placedHere = decor.filter(d => +d.zone === zone && +d.sous_zone === sub);
    const placedAt = new Map();
    for (const d of placedHere) {
      const h = parseInt(d.gestionnaire, 16);
      if (!placedAt.has(h)) placedAt.set(h, []);
      placedAt.get(h).push({ x: +d.x, z: +d.z });
    }

    const tableHere = new Map();
    all.forEach((h, slot) => { if (!tableHere.has(h)) tableHere.set(h, tableOf(slot)); });

    raw.push({ ...sa, here, parents, monsters, placedAt, tableHere,
      chests: chests.filter(c => +c.zone === zone && +c.sous_zone === sub)
        .reduce((m, c) => (m.some(o => o.x === +c.x && o.z === +c.z) ? m : [...m, { x: +c.x, z: +c.z }]), []),
      monsterSpots: formations.filter(f => +f.zone === zone && +f.sous_zone === sub && (+f.x || +f.z))
        .map(f => ({ x: +f.x, z: +f.z })),
      decorSpots: placedHere.map(d => ({ x: +d.x, z: +d.z })),
    });
  }

  // ---- 2. ce qui PEUT tourner, lieu par lieu --------------------------------------------------
  // Trois graines, toutes relevees : ce qui est pose (decor.csv), ce qu'une espece porte
  // (especes.csv), ce que la sonde a deja vu tourner ICI. Puis la fermeture par les naissances.
  //
  // La regle qui rend la reponse « inatteignable » solide : la table d'entites de decor 0x005fa5e0
  // n'est remplie que par SpawnSubAreaEntities, donc depuis les listes +0x30 — aucun
  // AllocEntitySlot ne la vise (verifie le 2026-09-25). Un gestionnaire de 0x0056adc8 que rien ne
  // pose ne tourne donc pas. Controle : sur 297 gestionnaires de decor deja vus en jeu, 296 sont
  // dans decor.csv, et l'exception (0x004700C0, 10:7) n'est posee nulle part dans la zone — une
  // entite de la sous-zone precedente resolue une image par la table suivante. Meme regle pour
  // 0x0056ad48 : le sous-type d'une entite de scene est l'octet `ia` de son espece.
  const seenAt = new Set();
  for (const line of fs.readFileSync(R('Documentation', 'Etat', 'handlers-exerces.tsv'), 'utf8').split(/\r?\n/)) {
    if (!line || line.startsWith('#')) continue;
    const [h, z, s] = line.split('\t');
    seenAt.add(`${parseInt(h, 16)}:${+z}:${+s}`);
  }

  for (const r of raw) {
    r.reach = new Map();      // h -> { seed } ou { via: parent }
    for (const h of r.placedAt.keys()) r.reach.set(h, { seed: 'pose' });
    for (const h of r.monsters.keys()) if (!r.reach.has(h)) r.reach.set(h, { seed: 'monstre' });
    for (const h of new Set([...r.here, ...allStatic])) {
      if (!controle && !r.reach.has(h) && seenAt.has(`${h}:${r.zone}:${r.sub}`)) r.reach.set(h, { seed: 'vu' });
    }
    let grew = true;
    while (grew) {
      grew = false;
      for (const p of [...r.reach.keys()]) {
        for (const c of childrenOf(p, r.all)) {
          if (c === p || r.reach.has(c)) continue;
          r.reach.set(c, { via: p });
          grew = true;
        }
      }
    }
  }

  /** La chaine qui mene a h dans le lieu r : [h, parent, …, graine]. */
  function chain(r, h) {
    const out = [h];
    for (let cur = h, guard = 0; guard < 20; guard++) {
      const step = r.reach.get(cur);
      if (!step || step.seed) break;
      cur = step.via;
      out.push(cur);
    }
    return out;
  }

  // ---- 3. chaque restant : un lieu ou il peut tourner, sinon pourquoi il ne peut pas ---------------
  const tablesOf = new Map();
  for (const r of raw) r.tableHere.forEach((t, h) => {
    if (!tablesOf.has(h)) tablesOf.set(h, new Set());
    tablesOf.get(h).add(t);
  });
  const hasAnyParent = new Set();
  for (const r of raw) for (const c of r.parents.keys()) hasAnyParent.add(c);

  const remaining = new Set();
  // En controle : on classe au contraire ce qui a DEJA tourne, sans la graine « vu » — la regle
  // « hors d atteinte » ne doit en attraper aucun.
  for (const r of raw) for (const h of r.here) if (!EMPTY.has(h) && isTested(h) === controle) remaining.add(h);
  if (!controle) for (const h of staticTodo) remaining.add(h);

  const cards = new Map();
  const card = r => {
    const k = `${r.zone}:${r.sub}`;
    if (!cards.has(k)) cards.set(k, { zone: r.zone, sub: r.sub, name: r.name, items: [],
      chests: r.chests, monsterSpots: r.monsterSpots, decorSpots: r.decorSpots });
    return cards.get(k);
  };
  const unreachable = [];
  const unknown = [];

  for (const h of remaining) {
    const cat = categoryOfHandler(h);
    const options = raw.filter(r => r.reach.has(h)).map(r => ({ r, c: chain(r, h) }));
    if (options.length) {
      // Le lieu ou la consigne sera la plus nette : un monstre qu'on peut nommer, puis un element
      // pose (il a un repere sur le plan), puis le reste ; a egalite la chaine la plus courte, et
      // un donjon plutot que l'auberge, ou le menu de debogage ne mene pas a toutes les pieces.
      const rank = o => {
        const seed = o.c.at(-1), kind = o.r.reach.get(seed).seed;
        const named = kind === 'monstre' && o.r.monsters.get(seed)?.size;
        return (named ? 0 : kind === 'pose' ? 1 : kind === 'monstre' ? 2 : 3) * 100
          + o.c.length + (o.r.zone === 0 ? 10 : 0);
      };
      options.sort((a, b) => rank(a) - rank(b));
      const { r, c } = options[0];
      const seed = c.at(-1);
      const seedKind = r.reach.get(seed).seed;
      card(r).items.push({
        h, cat,
        how: c.length === 1 ? seedKind : 'nait',
        seed: { h: seed, kind: seedKind, cat: categoryOfHandler(seed),
                names: [...(r.monsters.get(seed) ?? [])], spots: r.placedAt.get(seed) ?? null,
                spells: [...new Set(c.flatMap(x => spellNames.get(x) ?? []))] },
        spots: r.placedAt.get(h) ?? null,
        names: [...(r.monsters.get(h) ?? [])],
        steps: c.length - 1,
        also: options.slice(1).map(o => `${o.r.zone}:${o.r.sub}`),
      });
      continue;
    }

    const tables = [...(tablesOf.get(h) ?? [])];
    const zones = [...new Set(raw.filter(r => r.here.has(h)).map(r => r.zone))];
    if (tables.length && tables.every(t => t === 'ADC8')) unreachable.push({ h, cat, why: 'jamais-pose', zones });
    else if (tables.length && tables.every(t => t === 'AD48')) unreachable.push({ h, cat, why: 'aucune-espece', zones });
    // PAS de « ne nait que de parents hors d atteinte » : le controle (--controle) l a pris en
    // defaut 87 fois sur ce qui a deja tourne — ces gestionnaires ont d autres parents, a sous-type
    // calcule, que la lecture du C# ne voit pas. Seules les deux regles ci-dessus tiennent : sur 871
    // deja vus, une seule exception, 0x004700C0, expliquee plus haut.
    else unknown.push({ h, cat, zones, verbe: tables.some(t => t === 'AEC8' || t === 'AFC8') });
  }

  return {
    cards: [...cards.values()].sort((a, b) => a.zone - b.zone || a.sub - b.sub),
    unreachable, unknown, total: remaining.size,
  };
}

// `node guide-gestionnaires.analyse.mjs` : le bilan, pour la mise au point.
if (process.argv[1] && process.argv[1].endsWith('guide-gestionnaires.analyse.mjs') && !process.argv.includes('--controle')) {
  const { cards, unreachable, unknown, total } = analyse();
  const hows = {};
  let n = 0;
  for (const c of cards) for (const it of c.items) { hows[it.how + '/' + it.seed.kind] = (hows[it.how + '/' + it.seed.kind] ?? 0) + 1; n++; }
  const why = {};
  for (const u of unreachable) why[u.why] = (why[u.why] ?? 0) + 1;
  console.log(`${total} restants : ${n} atteignables dans ${cards.length} lieux, ${unreachable.length} hors d'atteinte, ${unknown.length} d'origine inconnue (dont ${unknown.filter(u => u.verbe).length} verbes/crochets)`);
  console.log('atteignables :', hows);
  console.log('hors d\'atteinte :', why);
  const ucat = {}; for (const u of unknown) ucat[u.cat] = (ucat[u.cat] ?? 0) + 1;
  console.log('inconnus par genre :', ucat);
}

// `node guide-gestionnaires.analyse.mjs --controle` : la regle appliquee a ce qui a deja tourne.
if (process.argv.includes('--controle')) {
  const { cards, unreachable, unknown, total } = analyse({ controle: true });
  const why = {};
  for (const u of unreachable) why[u.why] = (why[u.why] ?? 0) + 1;
  console.log(`CONTROLE sur ${total} gestionnaires deja vus : ${cards.reduce((n, c) => n + c.items.length, 0)} retrouves atteignables, ${unreachable.length} classes hors d'atteinte (a tort), ${unknown.length} sans origine`);
  console.log('faux « hors d atteinte » :', why);
  console.log(unreachable.map(u => `0x${u.h.toString(16).toUpperCase()} ${u.why}`).join('\n'));
}
