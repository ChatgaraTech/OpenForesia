// build-bestiairev2.mjs — assemble bestiairev2.html, le compendium des objets.
//
//   node OpenForesia.Tools/Html/build-bestiairev2.mjs
//   node OpenForesia.Tools/Html/check-bestiairev2.mjs
//
// Lit les CSV que le verbe `bestiaire` d'OpenForesia.Tools a ecrits dans
// Documentation/Reference/Bestiaire/ (objets, vignettes, butin_par_objet, coffres, lieux, sorts) et
// injecte le resultat dans bestiairev2.template.html a la place du marqueur /*__DATA__*/. Il ne lit
// jamais les donnees du jeu : regenerer les CSV d'abord si elles ont change.
//
// TOUT LE VOCABULAIRE VIENT DE Documentation/ — rien n'est devine :
//   - les champs de la fiche : Bestiaire/LISEZMOI.md, « Les valeurs d'un objet » ;
//   - les noms de caracteristique et d'element : ceux de l'editeur de sauvegarde (MonteeDeNiveau.md) ;
//   - les huit classes, bit par bit : LISEZMOI.md, tableau de +0x12 ;
//   - les quatre « contreparties » (indices 10, 11, 14, 15) : une HYPOTHESE de resistance, presentee
//     comme telle ;
//   - le butin : la loi de FUN_0053a920 (sept seuils, plage 0x38 + ecart, 56 a 100) et le seuil de
//     modele+0x22 contre rand() % 1000 (WorldSimulationStep.cs).
//
// LES SCRIPTS D'OBJET. Chaque fiche porte deux scripts, en +0x80 (l'emploi) et +0x82 (le second,
// dont le moment de declenchement n'est pas etabli). On les deroule ici hors ligne, avec les
// largeurs d'opcode de census-item-effects.sh, et une lecture ETROITE : seuls les motifs dont le
// sens est sur sont traduits en phrase (soin de PV/PM chiffre, guerison d'etat, sort lance,
// consommation, une chance sur N). Tout le reste devient « un effet que le compendium ne sait pas
// encore decrire » — jamais une phrase plausible.
//
// Les fiches japonaises de la zone 0 sont ecartees par la regle de build-editeur-sauvegarde.ps1.
// Le butin et les coffres de la zone 0 sont ecartes comme dans bestiaire.html (LISEZMOI.md).

import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const here = path.dirname(fileURLToPath(import.meta.url));
const root = path.resolve(here, '..', '..');
const dataDir = path.join(root, 'Documentation', 'Reference', 'Bestiaire');
const template = path.join(here, 'bestiairev2.template.html');
const out = path.join(here, 'bestiairev2.html');

// ------------------------------------------------------------------------------------------ CSV
function parseCsv(file) {
  const p = path.join(dataDir, file);
  if (!fs.existsSync(p)) throw new Error(`Manquant : ${p} — relancer le verbe bestiaire avec --sortie Documentation\\Reference\\Bestiaire`);
  const text = fs.readFileSync(p, 'utf8').replace(/^﻿/, '');
  const rows = [];
  let row = [], cell = '', q = false;
  for (let i = 0; i < text.length; i++) {
    const c = text[i];
    if (q) {
      if (c === '"') { if (text[i + 1] === '"') { cell += '"'; i++; } else q = false; }
      else cell += c;
    } else if (c === '"') q = true;
    else if (c === ',') { row.push(cell); cell = ''; }
    else if (c === '\n') { row.push(cell.replace(/\r$/, '')); rows.push(row); row = []; cell = ''; }
    else cell += c;
  }
  if (cell || row.length) { row.push(cell); rows.push(row); }
  const head = rows.shift();
  return rows.filter(r => r.length === head.length).map(r => Object.fromEntries(head.map((h, i) => [h, r[i]])));
}

const objets = parseCsv('objets.csv');
const vignettes = parseCsv('vignettes.csv');
const drops = parseCsv('butin_par_objet.csv');
const coffres = parseCsv('coffres.csv');
const lieux = parseCsv('lieux.csv');
const sorts = parseCsv('sorts.csv');

const icons = new Map(vignettes.map(v => [`${v.zone}/${v.sous_zone}/${v.objet_id}`, v.vignette]));
// lieux.csv est releve par motif dans les textes de la sous-zone : deux lignes y ont attrape une
// replique de dialogue. On ne garde que ce qui a la forme d'un nom de lieu.
const placeName = /^[A-Za-z0-9 àâäçéèêëîïôöùûüÀÂÄÇÉÈÊËÎÏÔÖÙÛÜœŒ'’.,-]{2,40}$/;
const lieu = new Map(lieux.filter(l => placeName.test(l.nom)).map(l => [`${l.zone}/${l.sous_zone}`, l.nom]));
const spellName = new Map(sorts.map(s => [`${s.famille}/${s.indice}`, s.nom]));

// La regle de build-editeur-sauvegarde.ps1 : tout autre caractere trahit une fiche japonaise.
const frenchName = /^[A-Za-z0-9 àâäçéèêëîïôöùûüÀÂÄÇÉÈÊËÎÏÔÖÙÛÜœŒ'’.,:!?()\-+«»/&]*$/;

// ------------------------------------------------------------------------------------------ les scripts
// Largeur de chaque opcode, relevee sur les 23 gestionnaires de DispatchItemOp (census-item-effects.sh).
const W = { 0: 2, 1: 3, 2: 3, 3: 4, 4: 4, 5: 4, 6: 4, 7: 4, 8: 4, 9: 4, 10: 4, 11: 4, 12: 4, 13: 5, 14: 5, 15: 1,
  16: 3, 17: 2, 18: 2, 19: 4, 20: 4, 21: 6, 22: 1 };

function reachable(b, start) {
  const seen = new Set();
  if (start <= 0 || start >= 0x140) return [];
  const stack = [start];
  while (stack.length) {
    let pc = stack.pop();
    while (pc > 0 && pc < 0x140 && !seen.has(pc)) {
      const op = b[pc];
      if (op === 0xff || op === 22 || !(op in W)) break;
      seen.add(pc);
      if (op === 1) { pc = b[pc + 1] | b[pc + 2] << 8; continue; }
      if (op === 2) stack.push(b[pc + 1] | b[pc + 2] << 8);
      pc += W[op];
    }
  }
  return [...seen].sort((x, y) => x - y).map(pc => ({ pc, op: b[pc], s: b.subarray(pc, pc + W[b[pc]]) }));
}

// L'unite arithmetique (ApplyScriptOp, FUN_00514040) : groupes de 0x40 (octet, mot, double mot),
// et dans chaque groupe 0x24..0x3b sont des comparaisons.
const isCompare = (op) => (op & 0x3f) >= 0x24 && (op & 0x3f) <= 0x3b;

// Deroule un script et rend des faits : { heal: {pv, pm}, full: {pv, pm}, cure, inflict, spells,
// consume, chance, group, ally, special, unknown }. `self` dit si les soins de l'opcode 5 (le
// bloc par personnage de celui qui se sert de l'objet) sont lus — +0x14c / +0x14e, FormatSauvegarde.md.
function analyseScript(b, start) {
  const ins = reachable(b, start);
  const f = { heal: {}, full: {}, selfHeal: {}, cure: false, inflict: false, spells: [], consume: null,
    chance: null, group: false, ally: false, special: false, unknown: false, empty: ins.length === 0 };
  const vars = new Map();
  const range = (k) => vars.get(k);
  const addHeal = (bag, which, r) => {
    if (!r || r.lo === undefined) { f.unknown = true; return; }
    const cur = bag[which] || { lo: 0, hi: 0 };
    bag[which] = { lo: cur.lo + r.lo, hi: cur.hi + r.hi };
  };

  for (let i = 0; i < ins.length; i++) {
    const { op, s } = ins[i];
    switch (op) {
      case 0: f.consume = s[1]; break;
      case 1: case 2: break;
      case 11: {                                         // la banque de variables de script
        const k = s[1], o = s[2], imm = s[3], r = range(k);
        if (o === 0x40) vars.set(k, { lo: imm, hi: imm });
        else if (o === 0x41 && r && r.lo !== undefined) vars.set(k, { lo: r.lo + imm, hi: r.hi + imm });
        else if (o === 0x42 && r && r.lo !== undefined) vars.set(k, { lo: r.lo - imm, hi: r.hi - imm });
        else if (o === 0x46 && r && r.lo !== undefined) vars.set(k, { lo: r.lo * imm, hi: r.hi * imm });
        else if (o === 0x4b) {
          vars.set(k, { lo: 0, hi: imm - 1 });
          // `rand() % N` aussitot compare a zero (0x24 : « different », qui arrete le script) :
          // la suite n'est atteinte qu'une fois sur N.
          const nx = ins[i + 1];
          if (nx && nx.op === 11 && nx.s[1] === k && nx.s[2] === 0x24 && nx.s[3] === 0 && imm > 1) f.chance = imm;
        }
        else if (o === 0x4c) vars.set(imm, r ? { ...r } : {});
        else if (o === 0x58) vars.set(k, range(imm) ? { ...range(imm) } : {});
        else if (o === 0x59 && r && r.lo !== undefined && range(imm) && range(imm).lo !== undefined)
          vars.set(k, { lo: r.lo + range(imm).lo, hi: r.hi + range(imm).hi });
        else if (!isCompare(o)) vars.set(k, {});
        break;
      }
      case 5: case 6: {                                  // 5 : soi-meme (+0x100) ; 6 : la cible (bloc de stats)
        const field = s[1], o = s[2], imm = s[3];
        const pv = field === 0x4c, pm = field === 0x4e;
        if (o === 0x4c && (field === 0x48 || field === 0x4a)) { vars.set(imm, { max: field === 0x48 ? 'pv' : 'pm' }); break; }
        if (pv || pm) {
          const which = pv ? 'pv' : 'pm';
          const bag = op === 5 ? f.selfHeal : f.heal;
          if (o === 0x59) addHeal(bag, which, range(imm));
          else if (o === 0x41) addHeal(bag, which, { lo: imm, hi: imm });
          else if (o === 0x58) {
            const r = range(imm);
            // « champ = maximum » est un PLEIN s'il est seul, un PLAFOND s'il suit un ajout et une
            // comparaison sur le meme champ (la Petite potion : +40..80, puis borne au maximum).
            const clamp = ins.some(x => (x.op === op) && x.s[1] === field && (x.s[2] === 0x59 || x.s[2] === 0x41 || isCompare(x.s[2])));
            if (r && r.max === which && !clamp && op === 6) f.full[which] = true;
            else if (!(r && r.max === which)) f.unknown = true;
          } else if (!isCompare(o)) f.unknown = true;
          break;
        }
        if (op === 6 && field >= 0x40 && field <= 0x43) {   // les etats (+0x40, Sorts.md / FormatSauvegarde.md)
          if (o === 0x04) f.cure = true;
          else if (o === 0x03) f.inflict = true;
          else if (!isCompare(o)) f.unknown = true;
          break;
        }
        if (!isCompare(o)) f.unknown = true;
        break;
      }
      case 14: f.spells.push(spellName.get(`${s[1]}/${s[2]}`) || null); break;
      case 16: f.group = true; break;
      case 17: f.ally = true; break;
      case 18: f.special = true; break;
      case 9: case 10: case 12: case 19: case 20: case 21: case 15: break;   // particules, sons, apparitions
      case 3: case 4: case 13: break;                    // tests sur l'entite ou l'equipement
      default: f.unknown = true;
    }

  }
  return f;
}

const span = (r) => r.lo === r.hi ? `${r.lo}` : `${r.lo} à ${r.hi}`;

// Les faits en phrases de joueur.
function describeUse(f) {
  if (f.empty) return null;
  const who = f.group ? 'à tout le groupe' : f.ally ? 'à un allié' : '';
  const lines = [];
  for (const w of ['pv', 'pm']) {
    const W2 = w.toUpperCase();
    if (f.full[w]) lines.push(`Rend tous les ${W2} ${who}`.trim());
    else if (f.heal[w]) lines.push(`Rend ${span(f.heal[w])} ${W2} ${who}`.trim());
    if (f.selfHeal[w]) lines.push(`Rend ${span(f.selfHeal[w])} ${W2} à celui qui s'en sert`);
  }
  if (f.cure) lines.push(`Guérit les altérations d'état${who ? ' ' + who : ''}`);
  if (f.inflict) lines.push(`Provoque une altération d'état`);
  for (const sp of f.spells) lines.push(sp ? `Lance le sort « ${sp} »` : `Lance un sort`);
  if (!lines.length && (f.special || f.unknown)) lines.push(`Effet particulier, pas encore décrit par le compendium`);
  if (!lines.length) lines.push(`Effet particulier, pas encore décrit par le compendium`);
  return lines;
}

// ------------------------------------------------------------------------------------------ les pouvoirs
// Le second script (+0x82) ne tourne pas a l'emploi : TriggerEquippedItems_005238E0 le lance pour
// chaque objet EQUIPE dont le mot +0x84 vaut l'evenement emis. Les evenements, releves site par site
// dans la reconstruction (2026-11-20) :
const EVENTS = {
  0: 'Toutes les 20 images, tant qu’il est équipé',            // WorldSimulationStep, toutes les 20 images
  1: 'Quand le porteur attaque',                               // TickPlayableMember / TickFollowingMember, le coup part
  2: 'Au moment de frapper au corps à corps',                  // ResolveMeleeHit_00539B80, assaillant, avant le calcul
  3: 'Après un coup porté au corps à corps',                   // ResolveMeleeHit_00539B80, assaillant, apres les degats
  5: 'Au début d’une incantation',                       // ApplyGroundMotion_0051A020, juste apres +0x14 = duree du sort
  6: 'Quand le sort part',                                     // idem, juste avant le prelevement du cout +0x204
  7: 'Quand un sort ou un effet du porteur vise une cible',    // PrepareCombatPair_00538870 / ResolveSpellHit_0053A2B0, assaillant
  8: 'Quand le porteur tente d’infliger une altération', // RollSavingThrow_0053A6C0, assaillant
  9: 'Quand le porteur tente d’infliger une altération', // RollSavingThrowB_0053A790, assaillant
  10: 'Quand le porteur subit des dégâts',                     // ApplyDamage_00539A20, victime du groupe
  12: 'Quand le porteur va être frappé au corps à corps',      // ResolveMeleeHit_00539B80, victime, avant le calcul
  13: 'Après avoir été frappé au corps à corps',               // ResolveMeleeHit_00539B80, victime, apres les degats
  14: 'Quand un monstre attaque le porteur au corps à corps',  // SweepPartyForMeleeHit_00519C30
  17: 'Quand un sort ou un effet vise le porteur',             // PrepareCombatPair_00538870 / ResolveSpellHit_0053A2B0, victime
  18: 'Quand le porteur doit résister à une altération',       // RollSavingThrow_0053A6C0, victime (+0x36)
  19: 'Quand le porteur doit résister à une altération',       // RollSavingThrowB_0053A790, victime (+0x3e)
  20: 'Quand le porteur inflige des dégâts',                   // ApplyDamage_00539A20, assaillant du groupe
  21: 'Quand le porteur tombe au combat',                      // TickPartyMember, bit 0x40000000 (PV a zero)
};

// Les familles de monstre : un bit du mot de modele+0x24, recopie en stats+0x14 par ResetEntityStats.
// On ne les NOMME pas : on cite les monstres qui portent le bit (modeles.csv, zones 1 a 10).
const modeles = parseCsv('modeles.csv').filter(m => m.zone !== '0');
const familyOf = new Map();       // "octet:bit" -> noms
const speciesOf = new Map();      // valeur de modele+0x12 (stats+0x10) -> noms
for (const m of modeles) {
  const b = Buffer.from(m.octets, 'hex');
  for (let by = 0; by < 4; by++) for (let k = 0; k < 8; k++) if (b[0x24 + by] & 1 << k) {
    const key = `${0x14 + by}:${1 << k}`;
    if (!familyOf.has(key)) familyOf.set(key, new Set());
    familyOf.get(key).add(m.nom);
  }
  const v = b[0x12] | b[0x13] << 8;
  if (!speciesOf.has(v)) speciesOf.set(v, new Set());
  speciesOf.get(v).add(m.nom);
}
const listNames = (set) => { const a = [...set]; return a.length > 8 ? `${a.slice(0, 8).join(', ')}… (${a.length} monstres)` : a.join(', '); };

// Les noms d'element, ceux de l'editeur de sauvegarde (MonteeDeNiveau.md).
const ELEMENTS = ['Feu', 'Eau', 'Terre', 'Air', 'Lumière', 'Nuit', 'Divin', 'Maléfique'];
const STATS = ['Force', 'Intelligence', 'Volonté', 'Agilité', 'Constitution', 'Puissance magique', 'Chance'];
const SCHOOLS = ['magie du Sorcier', 'magie du Prêtre', 'magie de la Fée'];   // Sorts.md
// Les genres de la table des 48 effets dont le sens est etabli (Combat/EffectTable.cs, StatusEffects.cs).
// Les autres sens viennent de ce que le jeu fait du bit d'etat : 8 brouille la direction
// (HandleDirectionalInput lit une direction au hasard), 0x10 bloque le bouton d'attaque
// (PartyMemberStep), 0x80 figure dans les masques 0x1c0 / 0x3c0 d'incapacite, avec le sommeil et la
// petrification, et c'est aussi ce que subit un personnage dont les PM tombent a zero.
// LES NOMS VIENNENT DU JEU (2026-11-20) : les descriptions des sorts du Pretre, dans l'ordre des
// fiches, s'alignent une a une sur les levees qu'ils appellent — Antidote (7) leve le bit 1
// « Neutralise un empoisonnement », Lourdes (8) le bit 4 « Libere totalement d'une paralysie »,
// Antisort (9) le bit 2 « Libere d'un ensorcellement », Marteau (10) les bits 8 et 0x200 « Libere
// d'un etat de confusion psychique », Calgon (14) le bit 0x100 « Libere de la petrification »,
// Delivrance (17) le bit 0x10 « Libere d'une malediction », Micro (18) le bit 0x20 « Guerit une
// personne muette ». La fontaine de 3e zone dit « Tu es paralyse », « Tu perds la tete », « Tu es
// muet ». Ce que chaque bit FAIT vient du code : 2 retire 3 % des PM max toutes les 30 images et
// coupe la regeneration des PM, 0x10 bloque le bouton d'attaque, 0x20 (comme 8) interdit
// d'incanter (masque 0x3e8) mais pas les objets (0x3c0). La paralysie (bit 4) n'a pas de lecteur
// dans la reconstruction : on la nomme sans dire ce qu'elle fait.
const GENRE_VERB = {
  7: 'paralyser la cible',                                               // effet 7, bit 4
  8: 'semer la confusion chez la cible (elle se déplace au hasard)',     // effet 8, bit 8
  9: 'maudire la cible (elle ne peut plus attaquer)',                    // effet 9, bit 0x10
  10: 'rendre la cible muette (elle ne peut plus lancer de sorts)',      // effet 10, bit 0x20
  31: 'ensorceler la cible (elle perd des PM)',                          // effet 31, bit 2
  // 12, bit 0x80 : sa marque est quatre eclats qui tournent au-dessus de la tete (TickStatusBurst /
  // TickStatusMote), la victime ne peut plus agir, la duree est la plus courte de la table ; c est
  // aussi l etat d un personnage dont les PM tombent a zero. Nom deduit, le jeu ne l ecrit nulle part.
  12: 'étourdir la cible (elle ne peut plus agir un moment)',
  // 20, bit 0x8000 : le sort de Fee Gros sabots, « Des esprits freinent les mouvements de l adversaire ».
  20: 'ralentir la cible',
  13: 'pétrifier la cible',           // ApplyStatusEffect_0053B8F0, bit 0x100 — la petrification (Sorts.md)
  30: 'empoisonner la cible',         // ApplyTrapEffect_0053C420, +0xe0 / bit 1 — le poison (WorldSimulationStep)
  29: 'dissiper les bonus et malus de caractéristiques', 41: 'lever les altérations',
  43: 'soigner', 45: 'soigner entièrement', 46: 'ressusciter', 47: 'tuer la cible sur le coup',
};
const GENRE_NAME = { 7: 'la paralysie', 8: 'la confusion', 9: 'la malédiction', 10: 'le mutisme', 11: 'le sommeil', 12: 'l’étourdissement', 20: 'le ralentissement', 13: 'la pétrification', 30: 'le poison', 31: 'l’ensorcellement',
  // 29 = ClearAllStatusEffects_0053C330 : elle efface les bits 0x400 a 0x80000, les hausses et baisses de
  // caracteristiques que FinalizeEquipmentEffects lit — une dissipation, pas une guerison.
  29: 'la dissipation de ses bonus de caractéristiques' };
// 11, bit 0x40 : ApplyDamage l efface a chaque coup recu et il figure dans les masques d incapacite.

// Les champs nommes. op5 : le bloc par personnage du porteur, +0x100 ; op7 : +0x200, ou vivent la
// copie du bloc de combat (+0x100 de ce bloc = les degats, +0x102 l'element, +0x103 l'effet en cours)
// et le cout du sort (+0x204) ; op8 : le bloc de combat de la cible, +0x100 ; op3 : l'entite.
const DERIVED = { 0x30: 'ATT', 0x32: 'DEF', 0x38: 'MATT', 0x3a: 'MDEF',
  0x34: 'Force d’altération I', 0x36: 'Résistance aux altérations I', 0x3c: 'Force d’altération II', 0x3e: 'Résistance aux altérations II' };
function sourceName(op, f) {
  if (op === 5) {
    if (f === 0x48) return 'PV max'; if (f === 0x4a) return 'PM max';
    if (f === 0x4c) return 'PV'; if (f === 0x4e) return 'PM';
    if (f >= 0x20 && f <= 0x2c && !(f & 1)) return STATS[(f - 0x20) >> 1];
    if (DERIVED[f]) return DERIVED[f];
  }
  if (op === 7 && f === 0) return 'dégâts reçus';
  if (op === 7 && f === 4) return 'coût';
  if (op === 8 && f === 0) return 'dégâts infligés';
  if (op === 3 && f === 0x14) return 'durée';
  return null;
}

const FRACTION = { 2: 'la moitié', 4: 'un quart', 8: 'un huitième', 10: 'un dixième', 16: 'un seizième', 20: 'un vingtième' };
function exprText(e) {
  if (!e) return null;
  if (e.c !== undefined) return `${e.c}`;
  if (e.lo !== undefined) return span(e);
  if (!e.src) return null;
  const part = e.den === 1 ? e.src : `${FRACTION[e.den] || '1/' + e.den} ${/^[AEIOUÉ]/.test(e.src) ? 'de l’' : e.src.startsWith('dégâts') ? 'des ' : 'de la '}${e.src.startsWith('dégâts') ? e.src : e.src}`;
  const txt = e.den === 1 ? `${e.src}` : part.replace('de la PV', 'des PV').replace('de la PM', 'des PM');
  return e.add ? `${txt} + ${e.add}` : txt;
}
const pctOff = (den) => Math.round(100 / den);

// LES POUVOIRS QUI NE SE LISENT PAS LIGNE A LIGNE. Chaque motif a ete releve a la main sur le
// script (2026-11-20) et sur le code qui lit ce que le script ecrit ; il ne s'applique que si
// TOUTES les instructions qui le caracterisent sont presentes. Les sources :
//  - +0x140 bit 0x100000 (octet +0x142, bit 0x10) : PrepareCombatPair et ResolveSpellHit rendent
//    « le coup ne passe pas » (marque 0x29) quand la victime le porte ; sa duree est +0x1b2 ;
//  - bit 0x400000 (octet +0x142, 0x40) : le sondage de terrain laisse passer la Fee (classe 7) ou ce
//    bit sur les sols marques 0x400 ;
//  - bits 0x2000000 / 0x4000000 (octet +0x143, 2 / 4), durees +0x182 / +0x184 : au coup porte,
//    ResolveMeleeHit pose la marque de l'element le plus fort de la victime, ou affiche son nom et
//    ses PV (le bandeau d'ordinaire reserve a la mise au point) ;
//  - bit 0x200 (octet +0x141, 2), duree +0x19c : la rage de FinalizeEquipmentEffects, ATT x1,5 et
//    DEF /2 ;
//  - +0x15c : le compte des victimes (ResolveMeleeHit l'incremente a chaque mise a mort) ;
//  - +0x158 : la jauge d'experience ; ResolveMeleeHit y ajoute (rang de la victime - niveau) / 32 + 1
//    a chaque coup, et Dropneal refait exactement ce calcul.
function specialPower(ins) {
  const has = (op, ...bytes) => ins.find(x => x.op === op && bytes.every((v, k) => v === null || x.s[k + 1] === v));
  const chanceOf = () => { const r = ins.find(x => x.op === 11 && x.s[2] === 0x4b); return r ? r.s[3] : 0; };

  // La reduction des degats d'un element (Amulette de la lumiere, Aquachale, Tiares, Robes…) :
  // degats - affinite de l'assaillant pour l'element, plancher 0, divises, plancher 1.
  const elTests = ins.filter(x => x.op === 7 && x.s[1] === 2 && x.s[2] === 0x24).map(x => x.s[3]);
  const shr = ins.find(x => x.op === 11 && x.s[2] === 0x4a && x.s[1] === 0x20);
  if (elTests.length && has(7, 0, 0x4c, null) && has(7, 0, 0x58, null) && shr && has(6, null, 0x0c, null)) {
    const named = elTests.map(damageElement);
    const l = [`Réduit les dégâts reçus : on leur retire l’affinité de l’assaillant pour cet élément, puis on les divise par ${1 << shr.s[3]} (il en reste au moins 1)`];
    // l'affinite lue apres chaque test : la premiere lecture op6 qui suit le test dans le script
    for (const t of ins.filter(x => x.op === 7 && x.s[1] === 2 && x.s[2] === 0x24)) {
      const rd = ins.slice(ins.indexOf(t) + 1).find(x => x.op === 6 && x.s[2] === 0x0c);
      if (rd && rd.s[1] - 0x18 !== t.s[3] - 2 && ELEMENTS[rd.s[1] - 0x18])
        l.push(`Défaut du jeu d’origine : face aux dégâts ${damageElement(t.s[3])}, c’est l’affinité pour l’élément ${ELEMENTS[rd.s[1] - 0x18]} qui est retirée`);
    }
    return { c: [`face aux dégâts ${named.join(' ou ')}`], l };
  }
  // Les poupees et la plume : relever le porteur, pas le groupe (l'opcode 16 choisit le porteur
  // lui-meme, par son propre indice pris en +0x08).
  const rv = has(18, 46);
  if (rv && has(3, 8, 0x0c, null)) {
    const third = ins.find(x => x.op === 11 && x.s[2] === 0x47 && x.s[1] === (has(5, 0x48, 0x4c, null) || {s: []}).s[3]);
    const l = [third ? `Ressuscite le porteur avec ${FRACTION[third.s[3]] || (third.s[3] === 3 ? 'un tiers' : '1/' + third.s[3])} de ses PV max` : 'Ressuscite le porteur avec tous ses PV'];
    const n = chanceOf(); if (has(0, null)) l.push(n > 1 ? `1 chance sur ${n} : l’objet est consommé` : 'L’objet est consommé');
    return { c: [], l };
  }
  if (has(5, 0x4e, 0x64, 0) && has(5, 0x40, 0x04, 0x7f)) {
    const l = ['Rend au porteur un vingtième de ses PM max',
      'Le libère de l’étourdissement qui frappe un personnage à court de PM'];
    if (has(18, 36)) l.push('Le libère aussi d’un ensorcellement, s’il en est frappé');
    const n = chanceOf(); if (has(0, null)) l.push(n > 1 ? `1 chance sur ${n} : l’objet est consommé` : 'L’objet est consommé');
    return { c: ['quand ses PM tombent à zéro'], l };
  }
  // Les gants : l'effet 0 sur soi, le bit 0x8000000 qui leve la penalite du quart (ResolveMeleeHit).
  if (has(18, 0) && has(5, 0x80, 0x64, 0))
    return { c: [], l: ['Les coups portent pleinement, même contre les monstres que l’arme ne blesse d’ordinaire qu’au quart'] };
  // Bequille : le bit de petrification pose directement, sans jet ni masque d'immunite.
  if (has(6, 0x41, 0x03, 0x01) && has(6, 0x9a, 0x58, null))
    return { c: [], l: ['Pétrifie la cible à chaque coup, sans jet de résistance ni immunité possible',
      'La pétrification dure d’autant plus que la Force d’altération I du porteur est haute'] };
  // Epee de la fatalite : 6 PV pour +60 ATT, et sinon l'interdiction d'attaquer.
  if (has(5, 0x40, 0x03, 0x10) && has(5, 0x4c, 0x75, null) && has(5, 0x30, 0x41, null)) {
    const pv = (has(11, 0x22, 0x40, null) || { s: [0, 0, 0, 7] }).s[3], cost = (has(11, 0x20, 0x40, null) || { s: [0, 0, 0, 6] }).s[3];
    return { c: [], l: [`S’il a plus de ${pv} PV : le porteur en perd ${cost} et gagne +${has(5, 0x30, 0x41, null).s[3]} ATT pour ce coup`,
      `Sinon : l’épée le maudit, il ne peut plus attaquer pendant un moment`] };
  }
  // Hache du diable : 6 PM pour +30 ATT ; sous la moitie des PV, l'etat du bit 4.
  if (has(5, 0x40, 0x03, 0x04) && has(5, 0x4c, 0x74, null) && has(5, 0x30, 0x41, null)) {
    const pm = (has(11, 0x26, 0x40, null) || { s: [0, 0, 0, 6] }).s[3];
    return { c: [], l: [`S’il a plus de ${pm} PM : le porteur en perd ${pm} et gagne +${has(5, 0x30, 0x41, null).s[3]} ATT pour ce coup`,
      'Sous la moitié de ses PV, la hache paralyse son porteur un moment'] };
  }
  // Anneau de Satan : des PM, et l'interdiction d'attaquer tenue tant qu'il est porte.
  if (has(5, 0x40, 0x03, 0x10) && has(5, 0x92, 0x40, null) && has(5, 0x4e, 0x59, null)) {
    const v = has(5, 0x4e, 0x59, null).s[3]; const n = (ins.find(x => x.op === 11 && x.s[1] === v && x.s[2] === 0x40) || { s: [] }).s[3];
    return { c: [], l: [`Rend ${n} PM au porteur`, 'Mais il maudit son porteur : celui-ci ne peut plus attaquer tant qu’il le porte'] };
  }
  // Masque de la mort : un vingtieme des PV max retire, jusqu'a zero.
  if (has(5, 0x43, 0x2d, 0x40) && has(5, 0x4c, 0x5a, null) && has(5, 0x48, 0x4c, null) && !has(5, 0x4c, 0x59, null)) {
    const d = (has(11, null, 0x47, null) || { s: [] }).s[3];
    return { c: [], l: [`Retire au porteur ${FRACTION[d] || '1/' + d} de ses PV max, jusqu’à zéro`] };
  }
  // La regeneration : quelques PV ou PM, tant que le porteur est debout.
  if (has(5, 0x43, 0x2d, 0x40) && (has(5, 0x4c, 0x59, null) || has(5, 0x4e, 0x59, null)) && !has(5, 0x4c, 0x5a, null)) {
    const l = [];
    for (const fld of [0x4c, 0x4e]) {
      const w = has(5, fld, 0x59, null); if (!w) continue;
      const at = ins.indexOf(w);
      const set = ins.slice(0, at).reverse().find(x => x.op === 11 && x.s[1] === w.s[3] && x.s[2] === 0x40);
      const amount = set && set.s[3];
      if (amount === undefined) return null;
      l.push(`Rend ${amount} ${fld === 0x4c ? 'PV' : 'PM'} au porteur`);
    }
    const c = ['tant que le porteur est debout'];
    if (has(5, 0x4c, 0x72, null)) c.push('quand ses PV sont à la moitié ou moins');
    return { c, l };
  }
  // Les objets « reflecteurs » : ils ne renvoient rien, ils annulent.
  if (has(5, 0x42, 0x03, 0x10) && has(5, 0xb2, 0x40, null)) {
    const c = [];
    const n = chanceOf(); if (n > 1) c.push(`1 chance sur ${n}`);
    if (has(4, 0x07, 0x26, 2)) c.push('contre un sort de magie (Sorcier, Prêtre ou Fée)');
    return { c, l: ['Le sort est annulé : il n’a aucun effet sur le porteur'] };
  }
  // Les quatre boucliers elementaires : l'element dominant du monstre, puis un soin.
  const cmpEl = ins.find(x => x.op === 11 && x.s[2] === 0x30 && x.s[3] >= 0x20 && x.s[3] <= 0x27);
  if (has(6, 0x18, 0x8c, null) && has(6, 0x1c, 0x8c, null) && cmpEl && has(5, 0x4c, 0x59, null)) {
    const n = chanceOf();
    return { c: [...(n > 1 ? [`1 chance sur ${n}`] : []), `contre un monstre dont l’élément dominant est ${['le Feu', 'l’Eau', 'la Terre', 'l’Air', 'la Lumière', 'la Nuit', 'le Divin', 'le Maléfique'][cmpEl.s[3] - 0x20]}`],
             l: ['Rend des PV au porteur : un huitième de l’affinité du monstre pour cet élément, plus 1'] };
  }
  // Bouclier de l'ombre : la moitie des PM max du lanceur, bornee a [1, 8].
  if (has(6, 0x4a, 0x4c, null) && has(5, 0x4e, 0x59, null) && has(11, null, 0x4a, 1) && has(11, null, 0x40, 8))
    return { c: [], l: ['Rend des PM au porteur : la moitié des PM max du lanceur, entre 1 et 8'] };
  // Chaussuresvol : l'etat de vol.
  if (has(5, 0x42, 0x03, 0x40))
    return { c: [], l: ['Le porteur flotte : il passe là où seule la Fée peut passer'] };
  // Les deux miroirs.
  if (has(5, 0x43, 0x03, 2) && has(5, 0x82, 0x40, null))
    return { c: [], l: ['Révèle l’élément dominant de la cible frappée (une marque à sa couleur)'] };
  if (has(5, 0x43, 0x03, 4) && has(5, 0x84, 0x40, null))
    return { c: [], l: ['Affiche le nom et les PV de la cible frappée'] };
  // Dropneal, Baguette de l'alchimiste : l'experience du coup, une seconde fois.
  if (has(5, 0x44, 0x4c, null) && has(6, 0x44, 0x4c, null) && has(5, 0x58, 0x99, null))
    return { c: [], l: ['Double l’expérience de chaque coup : le porteur reçoit une seconde fois ce que le coup rapporte (davantage contre un monstre de niveau supérieur au sien)'] };
  // Baguette de la feerie : un tiers du niveau de la cible en ATT.
  const lv = has(6, 0x44, 0x4c, null);
  if (lv && ins.length <= 4 && has(11, lv.s[3], 0x47, null) && has(5, 0x30, 0x59, lv.s[3])) {
    const d = has(11, lv.s[3], 0x47, null).s[3];
    return { c: [], l: [`ATT augmentée de ${FRACTION[d] || d === 3 && 'un tiers' || '1/' + d} du niveau de la cible`] };
  }
  // Armure de Satan, Robe du demon : le compte des victimes.
  if (has(5, 0x5c, 0x8c, null) && has(5, 0x32, 0x59, null)) {
    const d1 = ins.filter(x => x.op === 11 && x.s[2] === 0x87).map(x => x.s[3]);
    return { c: [], l: [
      `DEF augmentée du nombre de monstres tués par le porteur, divisé par ${d1[0]}`,
      `Résistance aux altérations I et II augmentées du même nombre divisé par ${d1[1]}`,
      'Le compte s’arrête à 9 999 victimes, et chaque valeur à 999' ] };
  }
  // La rage : sous la moitie des PV.
  if (has(5, 0x41, 0x03, 0x02) && has(5, 0x9c, 0x58, null)) {
    const l = ['Quand ses PV passent sous la moitié, le porteur entre en rage : ATT ×1,5 et DEF ÷2, pour une durée qui croît avec sa Force d’altération I'];
    const pushes = ins.filter(x => (x.op === 9 || x.op === 10) && x.s[1] === 0x3b);
    if (pushes.length === 2) {
      const rs = ins.filter(x => x.op === 11 && x.s[2] === 0x4b).map(x => x.s[3]);
      l.push(`1 chance sur ${rs[0]} de repousser la cible, 1 chance sur ${rs[1]} pendant la rage`);
    }
    return { c: [], l };
  }
  return null;
}

// Les effets que les scripts font naitre et qui changent le combat (les autres sont des eclats).
const SPAWN_TEXT = {
  0x3b: 'Repousse la cible : une onde de choc la projette en l’air (moins un gros monstre)',
  0x49: 'Tire un projectile vers l’ennemi le plus proche devant le porteur',
  0x4b: 'Projette un trait d’énergie droit devant le porteur',
  0x55: 'Immobilise la cible quelques instants',
};

// Le code d'element d'un coup (+0x202) : DominantElement_005399E0 rend l'indice de l'element le plus
// fort decale de deux, et zero quand il n'y en a aucun.
const damageElement = (e) => { const n = ELEMENTS[e - 2]; return !n ? 'sans élément' : /^[AEIOUÉ]/.test(n) ? `d’${n}` : `de ${n}`; };

const elide = (t) => t.replace(/\bde un /g, 'd’un ').replace(/\bde une /g, 'd’une ');

function describeEvent(b) {
  const start = u16(b, 0x82);
  const ins = reachable(b, start);
  if (!ins.length) return null;
  const ev = u16(b, 0x84);
  const when = EVENTS[ev] || 'À un moment que le compendium n’a pas encore identifié';
  const sp = specialPower(ins);
  if (sp) return { w: when, c: sp.c.join(' · '), l: sp.l.map(elide) };

  const vars = new Map();        // cle de variable -> expression
  const bytes = new Map();       // octets poses un par un (0x00), pour les mots de deux octets
  const word = (k) => vars.get(k) ?? (bytes.has(k) && bytes.has(k + 1) ? { c: bytes.get(k) | bytes.get(k + 1) << 8 } : undefined);
  const conds = [];              // conditions, en mots
  const out = [];                // effets, en mots
  let partial = false, chance = 0, chanceGlobal = false, cost = null, group = false;
  let costScope = null;
  const fam = new Set(), famNot = new Set(), elems = new Set(), genres = new Set(), species = new Set();
  let school = null, spellCond = null;
  const writeCount = new Map();
  const prevReal = (i) => { for (let j = i - 1; j >= 0; j--) if (ins[j].op !== 1 && ins[j].op !== 2) return ins[j]; return null; };
  const isCmp = (x) => x && x.op >= 3 && x.op <= 11 && (x.s[2] & 0x3f) >= 0x24 && (x.s[2] & 0x3f) <= 0x3b;
  // Un test de signe (bit 0x80 de l'octet haut) juste avant : « si negatif, ramener a zero ».
  const signFloor = (i) => ins.slice(Math.max(0, i - 3), i).some(x => x.s[3] === 0x80 && [0x2c, 0x2d, 0x6c, 0x6d].includes(x.s[2]));
  const lc = (t) => /^[A-ZÉ]{2}/.test(t) || /^\+/.test(t) ? t : t[0].toLowerCase() + t.slice(1);
  // Une garde suivie de l'opcode 2 saute une region quand elle est vraie : la region n'est donc
  // executee que sous la condition. Si la region n'est qu'un saut ferme (opcode 1), c'est un « ou »
  // vers un effet commun — la condition reste alors globale (`global` = true).
  const scopes = [];
  let curPc = 0;
  function guard(i, label) {
    const j = ins[i + 1];
    if (!j || j.op !== 2) return true;
    const end = j.s[1] | j.s[2] << 8;
    const region = ins.filter(x => x.pc > j.pc && x.pc < end);
    if (region.length === 1 && region[0].op === 1) return true;
    const sc = { label, start: j.pc, end };
    scopes.push(sc);
    return sc;
  }
  // Les gardes sont gardees telles quelles : l'etiquette d'un cout n'est connue qu'apres coup.
  const push = (t) => out.push({ t, labels: scopes.filter(sc => curPc > sc.start && curPc < sc.end) });

  // LE VOL DE VIE (Epee de sang, Baguette-vampire…), un motif a part : les degats infliges lus dans
  // une variable, divises, bornes, ajoutes au porteur ; et, si la cible est d'une famille donnee,
  // le chemin inverse — le porteur perd, la cible gagne.
  const rd = ins.find(x => x.op === 8 && x.s[1] === 0 && x.s[2] === 0x4c);
  const gain = rd && ins.find(x => x.op === 5 && (x.s[1] === 0x4c || x.s[1] === 0x4e) && x.s[2] === 0x59 && x.s[3] === rd.s[3]);
  if (gain) {
    const v = rd.s[3];
    let den = 1; const bounds = [];
    for (const x of ins) if (x.op === 11 && x.s[1] === v) {
      if (x.s[2] === 0x4a) den *= 1 << x.s[3];
      if (x.s[2] === 0x47) den *= x.s[3];
      if (x.s[2] === 0x40) bounds.push(x.s[3]);
    }
    const what = gain.s[1] === 0x4c ? 'PV' : 'PM';
    const lines = [`Vole ${FRACTION[den] || '1/' + den} des dégâts infligés et les rend au porteur en ${what}${bounds.length === 2 ? ` (entre ${Math.min(...bounds)} et ${Math.max(...bounds)})` : ''}`];
    const t = ins.find(x => x.op === 6 && x.s[1] >= 0x14 && x.s[1] <= 0x17 && x.s[2] === 0x2c);
    const names = t && familyOf.get(`${t.s[1]}:${t.s[3]}`);
    if (names) lines.push(`Contre ${listNames(names)}, l’effet s’inverse : le porteur perd ces ${what} et la cible les gagne`);
    return { w: when, c: '', l: lines };
  }

  for (let i = 0; i < ins.length; i++) {
    const { op, s } = ins[i];
    const nx = ins[i + 1];
    curPc = ins[i].pc;
    if (op === 11) {
      const k = s[1], o = s[2], imm = s[3], e = word(k);
      if (o === 0x40) { const p = prevReal(i); if (!(p && p.op === 11 && (p.s[1] === k || p.s[1] === k + 1) && isCmp(p))) vars.set(k, { c: imm }); }
      else if (o === 0x00) { bytes.set(k, imm); vars.delete(k); vars.delete(k - 1); }
      else if (o === 0x4b) {
        vars.set(k, { lo: 0, hi: imm - 1 });
        const jc = ins.slice(i + 1, i + 5).findIndex(x => x.op === 11 && x.s[1] === k && (x.s[2] === 0x24 || x.s[2] === 0x64) && x.s[3] === 0);
        if (jc >= 0 && imm > 1) {
          chance = imm;
          const g = guard(i + 1 + jc, { kind: 'text', text: `1 chance sur ${imm}` });
          if (g === true) chanceGlobal = true;
        }
      }
      else if (o === 0x41 && e) vars.set(k, e.c !== undefined ? { c: e.c + imm } : e.lo !== undefined ? { lo: e.lo + imm, hi: e.hi + imm } : { ...e, add: (e.add || 0) + imm });
      else if (o === 0x42 && e) vars.set(k, e.c !== undefined ? { c: e.c - imm } : { ...e, add: (e.add || 0) - imm });
      else if ((o === 0x4a || o === 0x47) && e) {
        const d = o === 0x4a ? 1 << imm : imm;
        vars.set(k, e.c !== undefined ? { c: Math.floor(e.c / d) } : e.src ? { ...e, den: e.den * d } : {});
      }
      else if (o === 0x4c) vars.set(imm, e ? { ...e } : {});
      else if (o === 0x58) vars.set(k, word(imm) ? { ...word(imm) } : {});
      else if (o === 0x5a && e && e.src) vars.set(k, { ...e, approx: true });
      else if (o === 0x24 || o === 0x64) {
        // une variable comparee a une constante : l'effet d'etat en cours (capes), sinon rien de nomme
        if (e && e.src === 'effet') genres.add(imm);
      }
      else if ((o & 0x3f) >= 0x24 && (o & 0x3f) <= 0x3b) { /* comparaison : une garde */ }
      else vars.set(k, {});
      continue;
    }
    if (op >= 3 && op <= 8) {
      const f = s[1], o = s[2], imm = s[3];
      if (op === 3 && f === 8 && o === 0x0c) continue;                                  // l'indice du porteur
      if (op === 3 && f === 7 && (o === 0x0c || o === 0x18 || (o === 0 && ins[i + 1] && ins[i + 1].op === 18))) continue;
      if (op === 5 && f === 0x42 && (o === 0x0c || o === 0x18 || (o === 4 && imm === 0xef))) continue;
      if ((op === 3 || op === 4) && ((f === 0 && o === 3 && imm === 0x40) || (f === 0x96 && o === 0x40))) continue;   // le clignotement d'un coup recu
      if (s[3] === 0x80 && (o === 0x2c || o === 0x2d) && (f & 1) && !(op === 6 && f >= 0x14 && f <= 0x17)) continue;             // un test de signe
      if (op === 6 && o === 0x58 && f >= 0x80 && f <= 0xe2 && ins.slice(Math.max(0, i - 8), i).some(x => x.op === 18)) continue;   // la duree de l'effet pose

      const cmp = (o & 0x3f) >= 0x24 && (o & 0x3f) <= 0x3b;
      // --- les conditions
      if (op === 6 && f >= 0x14 && f <= 0x17 && (o === 0x2c || o === 0x2d)) {
        const names = familyOf.get(`${f}:${imm}`);
        if (names) {
          const g = guard(i, { kind: o === 0x2c ? 'fam' : 'not', names });
          if (g === true) names.forEach(n => (o === 0x2c ? fam : famNot).add(n));
        } else partial = true;
        continue;
      }
      if (op === 6 && f === 0x10 && o === 0x64) {
        const names = speciesOf.get(imm) || new Set();
        const g = guard(i, { kind: 'fam', names });
        if (g === true) names.forEach(n => species.add(n));
        continue;
      }
      if ((op === 7 || op === 8) && f === 2 && o === 0x24) { elems.add(imm); continue; }
      if (op === 3 && f === 7 && o === 0x24) { school = imm; continue; }
      if (op === 3 && f === 6 && o === 0x24) { spellCond = imm; continue; }
      if (op === 7 && f === 3 && o === 0x0c) { vars.set(imm, { src: 'effet', den: 1 }); continue; }
      if (cmp) {
        if (op === 5 && (f === 0x4c || f === 0x4e)) {
          const g = guard(i, null);
          if (g !== true) costScope = g;
          continue;
        }
        // une garde sur un champ non nomme
        partial = true;
        continue;
      }
      // --- les lectures
      if (o === 0x4c || o === 0x0c) {
        const nm = sourceName(op, f);
        vars.set(imm, nm ? { src: nm, den: 1 } : {});
        if (!nm) partial = true;
        continue;
      }
      // --- les ecritures
      const target = op === 5 && (f === 0x4c || f === 0x4e) ? (f === 0x4c ? 'PV' : 'PM')
        : op === 5 && DERIVED[f] ? DERIVED[f]
        : op === 5 && f >= 0x20 && f <= 0x2c && !(f & 1) ? STATS[(f - 0x20) >> 1]
        : op === 7 && f === 0 ? 'dégâts reçus' : op === 7 && f === 4 ? 'coût'
        : op === 8 && f === 0 ? 'dégâts infligés' : op === 3 && f === 0x14 ? 'durée'
        : op === 6 && f === 0x4c ? 'PV de la cible' : op === 6 && f === 0x4e ? 'PM de la cible' : null;
      if (!target) { partial = true; continue; }
      if (o === 0x40 && imm === 0 && signFloor(i)) continue;                   // « si negatif, zero »
      { const p = prevReal(i); if (o === 0x58 && p && p.op === op && p.s[1] === f && isCmp(p)) continue; }   // plafond
      const val = o === 0x41 || o === 0x40 ? { c: imm } : word(imm);
      const key = `${target}|${o}`;
      writeCount.set(key, (writeCount.get(key) || 0) + 1);
      const v = exprText(val);
      const nextIsEffect = ins.slice(i + 1, i + 3).some(x => x.op === 18);

      if (target === 'PV' || target === 'PM') {
        if (o === 0x5a && val && (val.c !== undefined || val.src)) {
          const txt = val.c !== undefined ? `${val.c} ${target}` : `${exprText(val)}`;
          if (costScope && curPc > costScope.start && curPc < costScope.end) costScope.label = { kind: 'cost', text: `coûte ${txt}` };
          else push(`Retire ${txt} au porteur`);
          continue;
        }
        if (o === 0x5a) { partial = true; continue; }
        if (o === 0x59 || o === 0x41) {
          if (v) push(`Rend ${v} ${val.src ? '(en ' + target + ')' : target} au porteur`);
          else partial = true;
          continue;
        }
        if (o === 0x58 && val && val.src && /max/.test(val.src)) continue;    // le plafond au maximum
        partial = true; continue;
      }
      if ((DERIVED[f] || (f >= 0x20 && f <= 0x2c && !(f & 1))) && op === 5) {
        if ((f === 0x34 || f === 0x3c) && nextIsEffect) continue;            // force du jet, le temps de l'effet
        if (o === 0x41) { push(`+${imm} ${target}`); continue; }
        if (o === 0x59 && v) { push(`${target} augmentée de ${v}`); continue; }
        if (o === 0x58 && val && val.c >= 999) {
          push(f === 0x36 || f === 0x3e ? `${target} au maximum : immunité` : f === 0x34 || f === 0x3c ? `${target} au maximum : l’altération réussit à coup sûr` : `${target} portée à ${val.c}`);
          continue;
        }
        if (o === 0x58 && val && val.src === target) continue;               // restauration
        partial = true; continue;
      }
      if (target === 'coût' || target === 'durée') {
        const what = target === 'coût' ? 'Coût en PM du sort' : 'Durée d’incantation';
        if (val && val.src === target && val.den > 1) {
          if (o === 0x5a) { push(`${what} réduit${target === 'durée' ? 'e' : ''} de ${pctOff(val.den)} %`); continue; }
          if (o === 0x58) { push(`${what} réduit${target === 'durée' ? 'e' : ''} de ${100 - pctOff(val.den)} %`); continue; }
        }
        partial = true; continue;
      }
      if (target === 'dégâts reçus' || target === 'dégâts infligés') {
        const recv = target === 'dégâts reçus';
        if (val && val.src === target) {
          if (o === 0x58 && val.den > 1) { push(`${recv ? 'Dégâts reçus' : 'Dégâts infligés'} divisés par ${val.den}${val.approx ? ' (environ)' : ''}`); continue; }
          if (o === 0x59 && val.den > 1) { push(`${recv ? 'Dégâts reçus' : 'Dégâts infligés'} augmentés de ${pctOff(val.den)} %`); continue; }
        }
        partial = true; continue;
      }
      if (target === 'PV de la cible' && o === 0x40 && imm === 0) { push('Tue la cible sur le coup'); continue; }
      partial = true;
      continue;
    }
    if (op === 18) {
      const g = s[1];
      if (group) push(g === 46 ? 'Ressuscite tout le groupe' : g === 43 || g === 45 ? 'Soigne tout le groupe' : 'Applique un effet d’état à tout le groupe');
      else {
        const sure = ins.slice(Math.max(0, i - 4), i).some(x => x.op === 5 && (x.s[1] === 0x34 || x.s[1] === 0x3c) && x.s[2] === 0x58);
        const verb = GENRE_VERB[g] || 'infliger une altération d’état (non identifiée) à la cible';
        const PRESENT = { semer: 'Sème', empêcher: 'Empêche', paralyser: 'Paralyse', maudire: 'Maudit', rendre: 'Rend', ensorceler: 'Ensorcelle', immobiliser: 'Immobilise', étourdir: 'Étourdit', ralentir: 'Ralentit', pétrifier: 'Pétrifie', empoisonner: 'Empoisonne', infliger: 'Inflige' };
        const [w0, ...rest] = verb.split(' ');
        push(sure && PRESENT[w0] ? `${PRESENT[w0]} ${rest.join(' ')}, à coup sûr sauf si elle y est immunisée` : `Peut ${verb}`);
      }
      continue;
    }
    if (op === 14) { const sp = spellName.get(`${s[1]}/${s[2]}`); push(sp ? `Lance le sort « ${sp} »` : 'Lance un sort'); continue; }
    if (op === 16) continue;            // choisit une cible par son indice — ici le porteur
    if (op === 0) { push('L’objet est consommé'); continue; }
    if ((op === 9 || op === 10) && SPAWN_TEXT[s[1]]) {
      if (s[1] === 0x55 && ins.slice(Math.max(0, i - 4), i).some(x => x.op === 6 && x.s[1] === 0x4c && x.s[2] === 0x40 && x.s[3] === 0)) continue;
      push(SPAWN_TEXT[s[1]]); continue;
    }
    if ([1, 2, 9, 10, 12, 17, 19, 20, 21, 15].includes(op)) continue;
    partial = true;
  }

  // Une etiquette posee avant que le cout soit connu peut etre restee vide.
  for (const o of out) o.labels = o.labels.map(sc => sc.label || sc).filter(l => l && l.kind);
  const common = out.length ? out[0].labels.filter(l => out.every(o => o.labels.includes(l))) : [];
  const hoisted = [];
  for (const l of common) { hoisted.push(l); for (const o of out) o.labels = o.labels.filter(x => x !== l); }
  const render = (labels) => {
    const parts = [];
    const union = (k) => { const u = new Set(); labels.filter(l => l.kind === k).forEach(l => l.names.forEach(n => u.add(n))); return u; };
    for (const l of labels) if (l.kind === 'text') parts.push(l.text);
    const yes = union('fam'), no = union('not');
    if (yes.size) parts.push(`contre ${listNames(yes)}`);
    if (no.size) parts.push(`sauf contre ${listNames(no)}`);
    for (const l of labels) if (l.kind === 'cost') parts.push(l.text);
    return parts;
  };
  for (let i = 0; i < out.length; i++) out[i] = out[i].labels.length ? `${render(out[i].labels).join(', ')} : ${lc(out[i].t)}` : out[i].t;
  if (!out.length && cost) out.push(`Prélève ${cost} au porteur`), cost = null;

  // Les degats lus pour etre rendus au porteur : le vol de vie.
  for (let i = 0; i < out.length; i++) {
    const m = out[i].match(/^(.*?)[Rr]end (.*dégâts (?:infligés|reçus).*) \(en (PV|PM)\) au porteur$/);
    if (m) out[i] = `${m[1]}${m[1] ? 'r' : 'R'}end au porteur ${m[2]} en ${m[3]}`;
  }

  const c = [];
  if (chanceGlobal) c.push(`1 chance sur ${chance}`);
  for (const l of hoisted) if (l.kind === 'fam') l.names.forEach(n => fam.add(n));
  for (const l of hoisted) if (l.kind === 'not') l.names.forEach(n => famNot.add(n));
  c.push(...hoisted.filter(l => l.kind === 'text').map(l => l.text));
  for (const n of species) fam.add(n);
  species.clear();
  if (fam.size) c.push(`contre ${listNames(fam)}`);
  if (species.size) c.push(`contre ${listNames(species)}`);
  if (famNot.size) c.push(`sauf contre ${listNames(famNot)}`);
  if (elems.size) {
    const named = [...elems].map(damageElement);
    c.push(`face aux dégâts ${named.join(' ou ')}`);
  }
  if (school !== null && spellCond !== null) c.push(`pour le sort « ${spellName.get(`${school}/${spellCond}`) || '?'} »`);
  else if (school !== null) c.push(`pour la ${SCHOOLS[school] || 'magie'}`);
  if (genres.size) c.push([...genres].some(g => GENRE_NAME[g]) ? `contre ${[...genres].map(g => GENRE_NAME[g] || 'une autre altération (non identifiée)').join(' et ')}` : 'contre une altération d’état précise');
  if (cost) c.push(`coûte ${cost}`);
  c.push(...hoisted.filter(l => l.kind === 'cost').map(l => l.text));

  const lines = [...new Set(out)];
  if (!lines.length) lines.push(chance && !chanceGlobal ? `1 chance sur ${chance} de déclencher un effet que le compendium ne sait pas encore décrire` : 'Effet que le compendium ne sait pas encore décrire');
  else if (partial) lines.push('Et d’autres effets que le compendium ne sait pas encore décrire');
  return { w: when, c: c.join(' · '), l: lines.map(elide) };
}

// ------------------------------------------------------------------------------------------ le butin
// FUN_0053a920 : rand() % plage contre sept seuils stricts. Effectif de chaque rang a une plage donnee.
const TH = [0x18, 0x29, 0x38, 0x45, 0x50, 0x59, 0x5f];
function rankShare(rank, plage) {
  let n = 0;
  for (let roll = 0; roll < plage; roll++) {
    let r = 0;
    while (r < 7 && roll > TH[r]) r++;
    if (r === rank) n++;
  }
  return n / plage;
}

// ------------------------------------------------------------------------------------------ les objets
const s16 = (b, o) => (b[o] | b[o + 1] << 8) << 16 >> 16;
const u16 = (b, o) => b[o] | b[o + 1] << 8;
const i8 = (b, o) => b[o] << 24 >> 24;

function kindOf(flags) {        // l'octet haut de +0x12 (editeur-sauvegarde, itemKind)
  if (flags & 0x100) return 'Arme';
  if (flags & 0x200) return 'Armure';
  if (flags & 0x400) return 'Bouclier';
  if (flags & 0x800) return 'Accessoire';
  return 'Objet';
}

const byHex = new Map();
const keyOfSlot = new Map();       // zone/sz/id -> cle de fiche
let japanese = 0;
for (const o of objets) {
  if (!o.nom || o.octets.length < 0x140 * 2) continue;
  if (!frenchName.test(o.nom)) {
    if (o.zone !== '0') throw new Error(`Nom illisible hors de la zone 0 : « ${o.nom} » (zone ${o.zone}) — revoir la regle`);
    japanese++;
    continue;
  }
  const h = o.octets.slice(0, 0x140 * 2);
  keyOfSlot.set(`${o.zone}/${o.sous_zone}/${o.objet_id}`, h);
  if (!byHex.has(h)) byHex.set(h, { name: o.nom, desc: o.description, hex: h, icon: '', zones: new Set(), drops: new Map(), chests: new Map() });
  const it = byHex.get(h);
  if (!it.icon) it.icon = icons.get(`${o.zone}/${o.sous_zone}/${o.objet_id}`) || '';
  it.zones.add(+o.zone);
}

// Plusieurs fiches pour un meme nom : le nom seul ne suffit pas a joindre le butin.
const byName = new Map();
for (const it of byHex.values()) { if (!byName.has(it.name)) byName.set(it.name, []); byName.get(it.name).push(it); }
function itemFor(name, zone, sz) {
  const list = byName.get(name);
  if (!list) return null;
  if (list.length === 1) return list[0];
  for (const it of list) for (const [k, h] of keyOfSlot) if (h === it.hex && k.startsWith(`${zone}/${sz}/`)) return it;
  return null;
}

// Les monstres : une ligne par rang dans le CSV, on les additionne (LISEZMOI.md, « Les rangs se cumulent »).
for (const d of drops) {
  if (d.zone === '0') continue;
  const it = itemFor(d.objet, d.zone, d.sous_zone);
  if (!it) continue;
  // Deux especes d'un meme nom et d'une meme classe de butin repetent les memes rangs : un rang ne
  // compte qu'une fois par (monstre, lieu, classe).
  const k = `${d.monstre}|${d.zone}|${d.sous_zone}|${d.classe}`;
  if (!it.drops.has(k)) it.drops.set(k, { m: d.monstre, z: +d.zone, sz: +d.sous_zone, odds: +d.seuil_0x22, r56: 0, r100: 0, pl: 0, ranks: new Set() });
  const e = it.drops.get(k);
  e.pl = Math.max(e.pl, +d.placements);
  if (e.ranks.has(d.rang)) continue;
  e.ranks.add(d.rang);
  e.r56 += rankShare(+d.rang, 56);
  e.r100 += rankShare(+d.rang, 100);
}

// Les coffres : meme table, plage 100 (ChestGrantContents).
for (const c of coffres) {
  if (c.zone === '0' || c.role !== 'objet' || c.objet === '') continue;
  const h = keyOfSlot.get(`${c.zone}/${c.sous_zone}/${c.objet_id}`);
  const it = h ? byHex.get(h) : itemFor(c.objet, c.zone, c.sous_zone);
  if (!it) continue;
  const k = `${c.zone}|${c.sous_zone}|${c.coffre_id}|${c.liste}|${c.index}`;
  if (!it.chests.has(k)) it.chests.set(k, { z: +c.zone, sz: +c.sous_zone, p: 0 });
  it.chests.get(k).p += +c.pourcent_plage_100;
}

const pct = (x) => Math.round(x * 1000) / 10;
function merge(list, keyOf, fold) {
  const m = new Map();
  for (const e of list) { const k = keyOf(e); if (m.has(k)) fold(m.get(k), e); else m.set(k, e); }
  return [...m.values()];
}
const items = [];
let idx = 0;
const variants = new Map();
for (const it of [...byHex.values()].sort((a, b) => a.name.localeCompare(b.name, 'fr'))) {
  const b = Buffer.from(it.hex, 'hex');
  const flags = u16(b, 0x12);
  const bonus = Array.from({ length: 16 }, (_, k) => s16(b, 0x20 + k * 2));
  const el = Array.from({ length: 8 }, (_, k) => i8(b, 0x18 + k));
  const kind = kindOf(flags);
  const use = describeUse(analyseScript(b, u16(b, 0x80)));
  const second = describeEvent(b);
  const useFacts = analyseScript(b, u16(b, 0x80));
  const n = (variants.get(it.name) || 0) + 1;
  variants.set(it.name, n);
  const zones = [...it.zones].filter(z => z > 0).sort((a, c) => a - c);
  items.push({
    id: idx++,
    n: it.name,
    v: byName.get(it.name).length > 1 ? n : 0,
    d: it.desc,
    t: kind,
    c: kind === 'Objet' ? 0 : flags & 0xff,
    st: (b[0x13] & 0x30) !== 0 ? 1 : 0,
    a: bonus[8], df: bonus[9], ma: bonus[12], md: bonus[13],
    car: bonus.slice(0, 7),
    x7: bonus[7],
    res: [bonus[10], bonus[11], bonus[14], bonus[15]],
    el,
    use,
    cons: use ? (useFacts.consume === null ? -1 : useFacts.consume) : null,
    sec: second,
    zones,
    town: it.zones.has(0) ? 1 : 0,
    drops: merge([...it.drops.values()].sort((a, c) => c.r100 * c.odds - a.r100 * a.odds || a.z - c.z)
      .map(e => ({ m: e.m, z: e.z, l: lieu.get(`${e.z}/${e.sz}`) || '', o: Math.round(e.odds) / 10, r56: pct(e.r56), r100: pct(e.r100), pl: e.pl })),
      (e) => `${e.m}|${e.z}|${e.l}|${e.o}|${e.r56}|${e.r100}`, (a, e) => { a.pl += e.pl; }),
    chests: merge([...it.chests.values()].sort((a, c) => a.z - c.z || a.sz - c.sz)
      .map(e => ({ z: e.z, l: lieu.get(`${e.z}/${e.sz}`) || '', p: e.p, n: 1 })),
      (e) => `${e.z}|${e.l}|${e.p}`, (a) => { a.n++; }),
    i: it.icon,
  });
}


// ------------------------------------------------------------------------------------------ les conseils
// L'EQUIPEMENT CONSEILLE PAR CLASSE. Ce n'est PAS une donnee du jeu : c'est une APPRECIATION, et la
// page le dit. Elle repose sur trois faits releves dans le code, et sur une table de valeurs choisie
// a la main pour les pouvoirs :
//  - les degats d'un coup sont (ATT - DEF) + ATT/6 - DEF/32 (ResolveMeleeHit_00539B80) : un point
//    d'ATT en vaut un de DEF adverse, et l'ATT de l'arme compte plein ;
//  - ATT, DEF, MATT, MDEF sont tirees des caracteristiques AVANT que l'equipement ne s'ajoute
//    (ComputeDerivedStats puis ApplyEquipmentStatBonuses, FinalizeCharacterStats) : un « FOR +20 »
//    sur un objet ne fait PAS monter l'ATT. Les bonus de caracteristiques ne pesent donc presque rien ;
//  - les affinites elementaires d'un objet ne comptent que dans les deux emplacements d'accessoire
//    (ApplyEquipmentStatBonuses, i == 3 || i == 4).
// Seuls sont conseilles les objets qu'un monstre lache ou qu'un coffre contient : les autres ne
// s'obtiennent pas en jeu normal (Debacle, Bequille, Genocide…, vraisemblablement des objets de mise
// au point).
const ADVICE_ROLE = {
  0: { a: 1, df: 1, ma: 0, md: 0.6 },        // Guerrier
  1: { a: 0.7, df: 1, ma: 0.9, md: 1 },      // Pretre : frappe et soigne
  2: { a: 1, df: 1, ma: 0, md: 0.6 },        // Voleur
  // Sorcier et Fee restent en retrait et vivent de leurs degats : la defense pese moitie moins que la MATT
  // (correction du 2026-11-20 — le Baton des sages passait devant le Baton d'Apollon sur sa seule defense).
  3: { a: 0.2, df: 0.5, ma: 1.3, md: 0.6 },   // Sorcier
  4: { a: 1, df: 1, ma: 0, md: 0.6 },        // Chasseur
  5: { a: 0.9, df: 1, ma: 0.7, md: 0.8 },    // Elfe : l'arc et l'ecole du Sorcier
  6: { a: 1, df: 1, ma: 0, md: 0.6 },        // Nain
  7: { a: 0.2, df: 0.5, ma: 1.3, md: 0.6 },   // Fee
};
const CASTER = new Set([1, 3, 5, 7]);
const SCHOOL_OF = { 1: 1, 3: 0, 5: 0, 7: 2 };   // s_ClassStartIcon_00562ab8 (Sorts.md)
const MELEE = new Set([0, 2, 4, 6]);
// La valeur d'un pouvoir, en points d'ATT/DEF equivalents — l'appreciation. `cls` la module par classe.
const POWER_VALUE = {
  'Gulwick': () => 15, 'Fabnihl': () => 8, 'Flèche des Gouivres': () => 10,
  'Faux de la mort': () => 18, 'La massacreuse': () => 18,
  'Black Jack': () => 6, 'Rapière du chaos': () => 6, 'Féline': () => 6, 'Epée gauloise': () => 6,
  'Hache empoisonnée': () => 5, 'Rapière à poison': () => 4,
  'Durandal': () => 5, 'Corne du Minotaure': () => 6, 'Bahamut': () => 8,
  'Revatin': () => 5, 'Bâton stellaire': () => 4, 'Bâton divin': () => 4, 'Bouclier de Lucifer': () => 5,
  'Gantelets d’argent': (c) => MELEE.has(c) ? 15 : 5, "Gantelets d'argent": (c) => MELEE.has(c) ? 15 : 5,
  'Gants des runes': (c) => MELEE.has(c) ? 15 : 5, 'Gants sacrés': (c) => MELEE.has(c) ? 15 : 5,
  'Hache de la sauvagerie': () => 10, 'Os de guerrier sauvage': (c) => MELEE.has(c) ? 8 : 2,
  'Epée de la fatalité': () => 25, 'Hache du diable': () => 6,
  // Maudit : le bouton d'attaque ne repond plus (bit 0x10) — mais l'incantation ne lit pas ce bit
  // (masque 0x3e8) : un lanceur pur n'y perd presque rien.
  'Anneau de Satan': (c) => c === 3 || c === 7 ? 20 : -400,
  // Un vingtieme des PV max toutes les 20 images, jusqu'a zero, et zero PV met a terre.
  'Masque de la mort': () => -1000,
  'Chaîne du souffle': () => 30, 'Bouclier de Séraphin': () => 12, 'Bouclier solaire': () => 14,
  'Bottes d’elfes': () => 8, "Bottes d'elfes": () => 8, 'Robe médicinale': () => 8, 'Talisman médicinal': () => 8, 'Baguette guérisseuse': () => 5,
  'Couronne du soleil': (c) => CASTER.has(c) ? 45 : 0, 'Couronne de la lune': (c) => CASTER.has(c) ? 18 : 0, 'Corne du diable': (c) => CASTER.has(c) ? 7 : 0,
  'Pentacle magique': (c) => CASTER.has(c) ? 30 : 0, 'Pentacle de prince': (c) => CASTER.has(c) ? 16 : 0,
  // MATT x1,5 pour les sorts de son ecole : compte comme 60 MATT, la moitie d'une MATT de milieu de partie.
  'Gabriel': (c) => c === 1 ? 60 * ADVICE_ROLE[c].ma : 0,
  'Amulette des runes': (c) => SCHOOL_OF[c] === 0 ? 60 * ADVICE_ROLE[c].ma : 0, 'Voile de beauté': (c) => c === 1 ? 60 * ADVICE_ROLE[c].ma : 0, 'Voile mystique': (c) => c === 7 ? 60 * ADVICE_ROLE[c].ma : 0,
  'Anneau des éléments': (c) => c === 7 || c === 5 ? 25 : 8,
  'Anneau des philosophes': (c) => CASTER.has(c) ? 10 : 4, 'Rubis': (c) => CASTER.has(c) ? 5 : 2, 'Saphir': (c) => CASTER.has(c) ? 5 : 2, 'Voile des ombres': (c) => CASTER.has(c) ? 5 : 2,
  'Bague réflectrice': () => 12, 'Anneau du roi Salomon': () => 12, 'Robe de l’ombre': () => 12, "Robe de l'ombre": () => 12,
  'Cuirasse au miroir': () => 12, 'Réflector': () => 12, 'Bouclier réflecteur': () => 12,
  'Manteau des esprits': () => 25, 'Robe de Saulus': () => 20, 'Bouclier des élémentaux': () => 18, 'Tiare de la terre': () => 16, 'Tiare des étoiles': () => 16,
  'Armure de Pawas': () => 12, 'Armure sacrée': () => 10, 'Bouclier sacré': () => 10, 'Robe du gardien': () => 12, 'Bouclier de Bartschas': () => 10,
  'Amulette de la lumière': () => 8, 'Aquachâle': () => 8,
  'Robe de fantôme': () => 20, 'Armure noire': () => 25, 'Armure de Satan': () => 30, 'Robe du démon': () => 30,
  'Bague de protection': (c) => CASTER.has(c) ? 20 : 10, 'Bague de résistance': (c) => CASTER.has(c) ? 20 : 10,
  'Bouclier de l’ombre': (c) => CASTER.has(c) ? 10 : 4, "Bouclier de l'ombre": (c) => CASTER.has(c) ? 10 : 4,
  'Baguette de l’alchimiste': () => 10, "Baguette de l'alchimiste": () => 10, 'Dropneal': () => 10,
  'Baguette de la féerie': () => 10, 'Poignard d’Orihalcon': () => 12, "Poignard d'Orihalcon": () => 12,
  'Chaussuresvol': (c) => c === 7 ? 0 : 6,
};
// Les « +N ATT » d'un pouvoir qu'aucune table ne chiffre : plein sans condition, moins s'il coute des
// PM, un cinquieme s'il ne vaut que contre quelques monstres.
function genericPower(it, role) {
  if (!it.sec) return 0;
  let v = 0;
  const cond = it.sec.c || '';
  const chance = cond.match(/1 chance sur (d+)/);
  const odds = chance ? 1 / +chance[1] : 1;
  for (const line of it.sec.l || []) {
    const m = line.match(/^\+(\d+) (ATT|DEF|MATT|MDEF)/);
    if (!m) continue;
    const w = { ATT: role.a, DEF: role.df, MATT: role.ma, MDEF: role.md }[m[2]];
    v += +m[1] * w * odds * (/contre/.test(cond) ? 0.2 : /coûte/.test(cond) ? 0.6 : 1);
  }
  // L'immunite a une alteration : utile, rarement decisive.
  if ((it.sec.l || []).some(l => /immunité/.test(l))) v += 5;
  return v;
}
const obtainable = (it) => it.drops.length > 0 || it.chests.length > 0;
function adviceScore(it, cls, role = ADVICE_ROLE[cls]) {
  let s = it.a * role.a + it.df * role.df + it.ma * role.ma + it.md * role.md;
  s += (it.res[1] + it.res[3]) * 0.15 + (CASTER.has(cls) ? it.res[2] : it.res[0]) * 0.05;
  s += it.car.reduce((a, b) => a + b, 0) * 0.03;
  if (it.t === 'Accessoire') s += it.el.reduce((a, b) => a + Math.max(0, b), 0) * 0.2;
  const pv = POWER_VALUE[it.n];
  s += pv ? pv(cls) : genericPower(it, role);
  return Math.round(s * 10) / 10;
}
function adviceWhy(it, cls) {
  const parts = [];
  for (const [k, lab] of [['a', 'ATT'], ['df', 'DEF'], ['ma', 'MATT'], ['md', 'MDEF']]) if (it[k]) parts.push(`${lab} ${it[k] > 0 ? '+' : ''}${it[k]}`);
  const pow = it.sec && it.sec.l && it.sec.l[0] ? it.sec.l[0].replace(/\s*\/.*$/, '') : '';
  if (pow) parts.push((it.sec.c ? it.sec.c + ' : ' : '') + (/^[A-ZÉ][a-zàâäçéèêëîïôöùûü]/.test(pow) ? pow.charAt(0).toLowerCase() + pow.slice(1) : pow));
  return parts.join(' · ');
}
const SLOT_TYPES = [['Arme', 1], ['Armure', 1], ['Bouclier', 1], ['Accessoire', 2]];
// Le mot de chaque classe — l'appreciation, ecrite a la main sur ce que le classement fait sortir.
const ADVICE_NOTE = {
  0: 'Le Guerrier frappe et encaisse : on cherche l’ATT la plus haute, puis la DEF. Une paire de gants qui « porte pleinement » vaut plus que ses chiffres : sans eux, son arme ne fait qu’un quart des dégâts contre les familles qu’elle ne mord pas.',
  1: 'Le Prêtre frappe correctement avec ses crosses et soigne. Le Voile de beauté multiplie sa MATT par 1,5 pour ses propres sorts : c’est l’accessoire à trouver. Une couronne qui réduit le coût en PM compte autant qu’une grosse arme.',
  2: 'Le Voleur a peu d’ATT : Dominique passe devant parce qu’elle gagne +60 ATT contre vingt-six monstres, Fabnihl parce qu’elle pétrifie une fois sur douze. Sa protection est un bouclier, qu’il porte à la place d’une armure.',
  3: 'Le Sorcier ne vit que de sa MATT : le Bâton d’Apollon (+82) passe devant, malgré la défense qu’il fait perdre. Les bonus de Puissance magique d’un objet ne font pas monter la MATT. Il ne frappe presque pas, et c’est ce qui rend l’Anneau de Satan intéressant pour lui : la malédiction bloque le bouton d’attaque, pas l’incantation, et l’anneau donne +80 MATT et +80 MDEF.',
  4: 'Le Chasseur tire à l’arc : ATT d’abord. Il n’a pas de sorts, donc les accessoires magiques ne lui servent à rien. Comme le Voleur, il porte un bouclier à la place d’une armure.',
  5: 'L’Elfe (la Sylphide, au féminin) mélange la rapière et l’école du Sorcier. Aucune armure ni arme qu’on trouve ne mêle les deux : l’équilibre se fait par les accessoires. L’Amulette des runes lui donne la même MATT ×1,5 qu’au Sorcier ; l’Anneau de Satan est à fuir, il lui ôterait l’épée.',
  6: 'Le Nain est le meilleur combattant au corps à corps : Gulwick (146 ATT, pétrifie une fois sur cinq) est la plus grosse arme obtenable du jeu. Gros bouclier et grosse armure.',
  7: 'La Fée lance des sorts : MATT et MDEF d’abord. Le Voile mystique multiplie sa MATT par 1,5, et l’Anneau de Satan lui va comme au Sorcier.',
};
// LES BUILDS : jusqu'a trois facons d'equiper une classe. Chacun change les poids (`role`), ajoute des
// points a quelques objets (`plus`, l'appreciation, toujours) ou FIXE un emplacement (`pin`, un choix
// valide a la main — la Sylphide polyvalente, 2026-11-20). Un build qui retombe sur les memes pieces
// qu'un build deja retenu n'est pas affiche : il ne dirait rien de plus.
const ADVICE_BUILDS = {
  0: [
    { name: 'Force brute', desc: 'L’ATT la plus haute, la DEF ensuite.' },
    { name: 'Robuste', desc: 'Pour tenir la première ligne : DEF et MDEF d’abord, et de quoi regagner des PV.',
      role: { a: 0.5, df: 1.3, ma: 0, md: 1.2 }, plus: { 'Chaîne du souffle': 20 } },
  ],
  1: [
    { name: 'Soutien', desc: 'Il soigne et lance : MATT d’abord, et des PM qui durent.',
      role: { a: 0.3, df: 0.8, ma: 1.3, md: 0.8 }, plus: { 'Couronne du soleil': 15 } },
    { name: 'Combat', desc: 'Il frappe avec ses crosses : ATT et DEF, des gants pour mordre sur toutes les familles.',
      role: { a: 1, df: 1, ma: 0.3, md: 0.6 }, plus: { 'Gants sacrés': 10, 'Gants des runes': 10 } },
    { name: 'Tank', desc: 'Pour encaisser à la place du groupe : DEF et MDEF d’abord, des résistances aux altérations, et de quoi regagner des PV et des PM.',
      role: { a: 0.3, df: 1.4, ma: 0.3, md: 1.3 }, plus: { 'Chaîne du souffle': 25, 'Manteau de résistance': 10 } },
  ],
  2: [
    { name: 'Coups', desc: 'L’ATT la plus haute.' },
    { name: 'Altérations', desc: 'Moins d’ATT, mais des armes qui pétrifient, étourdissent ou tuent sur le coup.',
      plus: { 'Fabnihl': 40, 'Faux de la mort': 40, 'Black Jack': 30, 'Féline': 25 } },
  ],
  3: [
    { name: 'Puissance', desc: 'Le plus de MATT possible.' },
    { name: 'Sans malédiction', desc: 'Pour qui veut garder le coup de bâton : pas d’Anneau de Satan, des sorts moins chers et plus rapides, une défense qui ne s’effondre pas.',
      role: { a: 0.2, df: 0.9, ma: 1, md: 1 }, plus: { 'Anneau de Satan': -1000, 'Couronne du soleil': 30, 'Pentacle magique': 20, 'Bâton des sages': 15 } },
  ],
  4: [
    { name: 'Arc', desc: 'L’ATT la plus haute, la DEF ensuite.' },
  ],
  5: [
    { name: 'Polyvalente', desc: 'Moitié épée, moitié magie : la plus grosse rapière, et les deux accessoires pour les sorts.',
      pin: { Arme: ['Rapière de Zeus'], Armure: ['Bouclier des élémentaux'], Accessoire: ['Amulette des runes', 'Anneau des éléments'] } },
    { name: 'Plutôt magie', desc: 'Les sorts d’abord : MATT, sorts moins chers et plus rapides, et la seule protection avec de la MDEF.',
      role: { a: 0.3, df: 0.8, ma: 1.3, md: 1 }, plus: { 'Couronne du soleil': 20, 'Pentacle magique': 20, 'Bouclier des elfes': 40 } },
    { name: 'Plutôt combat', desc: 'L’épée d’abord : ATT, DEF, et des gants pour mordre sur toutes les familles.',
      role: { a: 1, df: 1, ma: 0.2, md: 0.6 }, plus: { 'Gants sacrés': 15, 'Gants des runes': 10, 'Couronne du soleil': -45, 'Amulette du dragon': 10 } },
  ],
  6: [
    { name: 'Force brute', desc: 'L’ATT la plus haute, la DEF ensuite.' },
    { name: 'Robuste', desc: 'Pour tenir la première ligne : DEF et MDEF d’abord, et de quoi regagner des PV.',
      role: { a: 0.5, df: 1.3, ma: 0, md: 1.2 }, plus: { 'Chaîne du souffle': 20 } },
  ],
  7: [
    { name: 'Dégâts', desc: 'Le plus de MATT possible.' },
    { name: 'Sans malédiction', desc: 'Pour qui veut garder le coup de baguette : pas d’Anneau de Satan, des sorts moins chers, et des dégâts élémentaires augmentés de moitié.',
      role: { a: 0.2, df: 0.9, ma: 1, md: 1 }, plus: { 'Anneau de Satan': -1000, 'Couronne du soleil': 30, 'Pentacle magique': 20, 'Anneau des éléments': 15 } },
  ],
};
const advice = [];
for (let cls = 0; cls < 8; cls++) {
  const builds = [];
  const seen = new Set();
  for (const b of ADVICE_BUILDS[cls]) {
    const role = b.role || ADVICE_ROLE[cls];
    const score = (it) => Math.round((adviceScore(it, cls, role) + ((b.plus || {})[it.n] || 0)) * 10) / 10;
    const slots = [];
    for (const [t, n] of SLOT_TYPES) {
      const pool = items.filter(it => it.t === t && (it.c & (1 << cls)) && obtainable(it))
        .map(it => ({ it, s: score(it) })).filter(x => x.s > 0)
        .sort((a, b) => b.s - a.s || a.it.n.localeCompare(b.it.n, 'fr'));
      if (!pool.length) continue;
      // Deux accessoires au meme pouvoir ne cumulent pas ce pouvoir (les gants posent tous le meme
      // effet 0) : le second emplacement prend le meilleur dont le pouvoir differe.
      if (n === 2) {
        const powerOf = (x) => x.it.sec ? JSON.stringify(x.it.sec.l) : '';
        const first = pool[0];
        const k = pool.findIndex((x, i) => i > 0 && (!powerOf(x) || powerOf(x) !== powerOf(first)));
        if (k > 1) pool.splice(1, 0, pool.splice(k, 1)[0]);
        for (let i = pool.length - 1; i > 1; i--) if (powerOf(pool[i]) && powerOf(pool[i]) === powerOf(first)) pool.splice(i, 1);
      }
      // Un emplacement fixe a la main : ses pieces en tete, dans l'ordre donne.
      const pinned = (b.pin || {})[t];
      if (pinned) {
        for (const name of [...pinned].reverse()) {
          const k = pool.findIndex(x => x.it.n === name);
          if (k < 0) throw new Error(`Conseils : « ${name} » ne peut pas etre fixe pour ${cls} (${t})`);
          pool.unshift(pool.splice(k, 1)[0]);
        }
      }
      slots.push({ t, n, picks: pool.slice(0, n + 2).map(x => ({ id: x.it.id, s: x.s, why: adviceWhy(x.it, cls) })) });
    }
    const key = slots.map(sl => sl.picks.slice(0, sl.n).map(p => p.id).sort().join(',')).join('|');
    if (seen.has(key)) continue;
    seen.add(key);
    builds.push({ name: b.name, desc: b.desc, slots });
  }
  advice.push({ c: cls, note: ADVICE_NOTE[cls], builds });
}
for (const k of Object.keys(POWER_VALUE)) if (!items.some(it => it.n === k) && !/’/.test(k)) console.warn(`Conseils : « ${k} » n'existe pas`);

const payload = { items, advice, generated: new Date().toISOString().slice(0, 10) };
let html = fs.readFileSync(template, 'utf8');
const marker = "/*__DATA__*/{ items: [], generated: '' }";
if (!html.includes(marker)) throw new Error(`Marqueur /*__DATA__*/ introuvable dans ${template}`);
html = html.replace(marker, () => '/*__DATA__*/' + JSON.stringify(payload).replace(/</g, '\\u003c'));
fs.writeFileSync(out, html, 'utf8');

const count = (f) => items.filter(f).length;
console.log(`${items.length} objets (${japanese} lignes japonaises ecartees) : ` +
  ['Arme', 'Armure', 'Bouclier', 'Accessoire', 'Objet'].map(k => `${count(x => x.t === k)} ${k.toLowerCase()}`).join(', ') +
  ` ; ${count(x => x.i)} avec vignette, ${count(x => x.drops.length)} sur un monstre, ${count(x => x.chests.length)} en coffre, ` +
  `${count(x => x.use)} utilisables, ${count(x => x.sec)} avec un pouvoir -> ${path.relative(root, out)}`);
