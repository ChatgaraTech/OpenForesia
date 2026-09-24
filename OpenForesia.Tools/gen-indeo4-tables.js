// Genere OpenForesia/src/Host/Indeo4/Indeo4Tables.cs depuis les sources de FFmpeg : aucune table
// n'est recopiee a la main, et la longueur de chacune est verifiee.
//
//   node OpenForesia.Tools/gen-indeo4-tables.js <dossier des sources FFmpeg> <Indeo4Tables.cs>
//
// Le dossier doit contenir ivi.c, indeo4data.h et mathtables.c (de libavcodec/), a la revision
// nommee dans l'en-tete genere. Pour les obtenir :
//
//   for f in ivi.c indeo4data.h mathtables.c; do
//     curl -sSfLO https://raw.githubusercontent.com/FFmpeg/FFmpeg/b0d154591161153b0ec55d9527535bc776504899/libavcodec/$f
//   done
//
// Voir OpenForesia/src/Host/Indeo4/LISEZMOI.md.

const fs = require('fs');

const REVISION = 'b0d154591161153b0ec55d9527535bc776504899';
const [dir, target] = process.argv.slice(2);
if (!dir || !target) {
  console.error('usage : node gen-indeo4-tables.js <dossier FFmpeg> <Indeo4Tables.cs>');
  process.exit(2);
}

const ivi = fs.readFileSync(dir + '/ivi.c', 'utf8');
const data = fs.readFileSync(dir + '/indeo4data.h', 'utf8');
const math = fs.readFileSync(dir + '/mathtables.c', 'utf8');

// Le corps `{ ... }` du tableau C nomme, commentaires retires, en liste d'entiers.
function block(src, name) {
  const i = src.indexOf(name);
  if (i < 0) throw new Error('absent : ' + name);
  const start = src.indexOf('{', src.indexOf('=', i));
  let depth = 0, j = start;
  for (; j < src.length; j++) {
    if (src[j] === '{') depth++;
    else if (src[j] === '}') { depth--; if (depth === 0) break; }
  }
  const body = src.slice(start, j + 1).replace(/\/\*[\s\S]*?\*\//g, '').replace(/\/\/.*$/gm, '');
  return body.match(/-?\d+/g).map(Number);
}

function expect(values, n, name) {
  if (values.length !== n) throw new Error(`${name} : ${values.length} valeurs, ${n} attendues`);
  return values;
}

function rows(nums, per, indent) {
  let s = '';
  for (let i = 0; i < nums.length; i += per)
    s += indent + nums.slice(i, i + per).map(v => String(v).padStart(5)).join(',') + ',\n';
  return s;
}

function arr(type, name, nums, per = 16) {
  return `    internal static readonly ${type}[] ${name} =\n    {\n${rows(nums, per, '        ')}    };\n\n`;
}

function arr2(type, name, nums, n, len, per) {
  let s = `    internal static readonly ${type}[][] ${name} =\n    {\n`;
  for (let k = 0; k < n; k++)
    s += `        new ${type}[]\n        {\n${rows(nums.slice(k * len, (k + 1) * len), per, '            ')}        },\n`;
  return s + '    };\n\n';
}

// IVIHuffDesc : { num_rows, { xbits[num_rows] } }, huit par table.
function huff(name, nums) {
  let s = `    internal static readonly byte[][] ${name} =\n    {\n`, p = 0, count = 0;
  while (p < nums.length) {
    const n = nums[p++];
    s += `        new byte[] { ${nums.slice(p, p + n).join(', ')} },\n`;
    p += n;
    count++;
  }
  if (count !== 8) throw new Error(`${name} : ${count} dictionnaires, 8 attendus`);
  return s + '    };\n\n';
}

let out = '';
out += huff('MbHuffDesc', block(ivi, 'ivi_mb_huff_desc[8]'));
out += huff('BlkHuffDesc', block(ivi, 'ivi_blk_huff_desc[8]'));
out += arr('byte', 'ZigzagDirect', expect(block(math, 'ff_zigzag_direct[64]'), 64, 'ff_zigzag_direct'), 8);
out += arr('byte', 'VerticalScan8x8', expect(block(ivi, 'ff_ivi_vertical_scan_8x8[64]'), 64, 'vertical_scan_8x8'), 8);
out += arr('byte', 'HorizontalScan8x8', expect(block(ivi, 'ff_ivi_horizontal_scan_8x8[64]'), 64, 'horizontal_scan_8x8'), 8);
out += arr('byte', 'DirectScan4x4', expect(block(ivi, 'ff_ivi_direct_scan_4x4[16]'), 16, 'direct_scan_4x4'), 16);
out += arr('byte', 'AlternateScan8x8', expect(block(data, 'ivi4_alternate_scan_8x8[64]'), 64, 'alternate_scan_8x8'), 16);
out += arr('byte', 'AlternateScan4x4', expect(block(data, 'ivi4_alternate_scan_4x4[16]'), 16, 'alternate_scan_4x4'), 16);
out += arr('byte', 'VerticalScan4x4', expect(block(data, 'ivi4_vertical_scan_4x4[16]'), 16, 'vertical_scan_4x4'), 16);
out += arr('byte', 'HorizontalScan4x4', expect(block(data, 'ivi4_horizontal_scan_4x4[16]'), 16, 'horizontal_scan_4x4'), 16);
out += arr('ushort', 'CommonPicSizes', expect(block(data, 'ivi4_common_pic_sizes[14]'), 14, 'common_pic_sizes'), 14);
out += arr2('ushort', 'Quant8x8Intra', expect(block(data, 'ivi4_quant_8x8_intra[9][64]'), 576, 'quant_8x8_intra'), 9, 64, 8);
out += arr2('ushort', 'Quant8x8Inter', expect(block(data, 'ivi4_quant_8x8_inter[9][64]'), 576, 'quant_8x8_inter'), 9, 64, 8);
out += arr2('ushort', 'Quant4x4Intra', expect(block(data, 'ivi4_quant_4x4_intra[5][16]'), 80, 'quant_4x4_intra'), 5, 16, 4);
out += arr2('ushort', 'Quant4x4Inter', expect(block(data, 'ivi4_quant_4x4_inter[5][16]'), 80, 'quant_4x4_inter'), 5, 16, 4);
out += arr('byte', 'QuantIndexToTab', expect(block(data, 'quant_index_to_tab[22]'), 22, 'quant_index_to_tab'), 22);

// RVMapDesc : eob_sym, esc_sym, runtab[256], valtab[256] — neuf tables.
const rv = expect(block(ivi, 'ff_ivi_rvmap_tabs[9]'), 9 * 514, 'ff_ivi_rvmap_tabs');
const eob = [], esc = [], run = [], val = [];
for (let k = 0; k < 9; k++) {
  const b = rv.slice(k * 514, (k + 1) * 514);
  eob.push(b[0]);
  esc.push(b[1]);
  run.push(...b.slice(2, 258));
  val.push(...b.slice(258));
}
if (run.some(v => v < 0 || v > 255) || val.some(v => v < -128 || v > 127)) throw new Error('rvmap hors bornes');
out += arr('byte', 'RvMapEob', eob, 9);
out += arr('byte', 'RvMapEsc', esc, 9);
out += arr2('byte', 'RvMapRun', run, 9, 256, 16);
out += arr2('sbyte', 'RvMapVal', val, 9, 256, 16);

const head = `// =====================================================================
// Les tables du décodeur Indeo 4 — GÉNÉRÉES, ne pas éditer à la main.
//
// Traduites de FFmpeg (https://ffmpeg.org), révision ${REVISION} :
// \`libavcodec/ivi.c\` (dictionnaires de Huffman, balayages communs, tables run/valeur),
// \`libavcodec/indeo4data.h\` (balayages et matrices de quantification propres à Indeo 4),
// \`libavcodec/mathtables.c\` (le zigzag). Copyright (c) 2009-2011 Maxim Poliakovski.
//
// Ce fichier est distribué sous la GNU Lesser General Public License, version 2.1 ou (à votre
// choix) toute version ultérieure, comme les sources dont il est tiré. Voir \`LICENSE-LGPL.txt\`
// dans ce répertoire.
//
// Le générateur relit les tableaux C tels quels et vérifie leur longueur : c'est ce qui garantit
// qu'aucune valeur n'a été recopiée de travers. Voir \`LISEZMOI.md\`.
// =====================================================================

namespace OpenForesia.Blaze;

internal static class Indeo4Tables
{
`;

fs.writeFileSync(target, '﻿' + head + out.trimEnd() + '\n}\n');
console.log(`ecrit ${target} (eob ${eob.join(',')} ; esc ${esc.join(',')})`);
