namespace ExportCarte;

/// <summary>
/// La galerie des modeles : <c>index.html</c> a la racine de l'export. La liste est embarquee a la
/// place de <c>@@CATALOGUE@@</c> ; chaque modele se charge a la demande par son <c>donnees.js</c>
/// (une balise script, ce qui marche aussi en ouvrant le fichier sans serveur). Les animations sont
/// recalculees comme le jeu le fait : une pose par os et par image, composee avec celle du parent.
/// </summary>
internal static class PageModeles
{
    public const string Modele = """
<!doctype html>
<html lang="fr">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Modèles de Blaze &amp; Blade</title>
<style>
  :root { color-scheme: dark; --fond:#15161a; --panneau:#1d1f25; --bord:#2c2f38; --texte:#e8e6e1; --doux:#9a9890; --accent:#e0b44c; }
  * { box-sizing: border-box; }
  html, body { margin:0; height:100%; background:var(--fond); color:var(--texte); font:14px/1.4 system-ui, sans-serif; }
  body { display:grid; grid-template-columns: minmax(240px, 320px) 1fr; height:100vh; }
  @media (max-width: 720px) { body { grid-template-columns: 1fr; grid-template-rows: 40vh 60vh; } }
  #liste { background:var(--panneau); border-right:1px solid var(--bord); overflow:auto; padding:12px 12px 24px; }
  #liste h1 { font-size:17px; margin:2px 0 8px; }
  #filtre { width:100%; padding:7px 9px; border-radius:6px; border:1px solid var(--bord); background:#121317; color:var(--texte); margin-bottom:8px; }
  details { margin:4px 0; }
  summary { cursor:pointer; color:var(--doux); padding:4px 0; font-weight:600; }
  .m { display:block; width:100%; text-align:left; background:none; border:0; color:var(--texte); padding:4px 8px; border-radius:5px; cursor:pointer; font:inherit; }
  .m:hover { background:#2a2d36; } .m.actif { background:#3a3420; color:var(--accent); }
  .m small { color:var(--doux); }
  #scene { position:relative; overflow:hidden; }
  canvas { display:block; width:100%; height:100%; cursor:grab; }
  #infos { position:absolute; top:12px; left:12px; right:12px; pointer-events:none; }
  #infos h2 { margin:0; font-size:18px; }
  #infos p { margin:2px 0 0; color:var(--doux); font-size:12px; max-width:60ch; }
  #commandes { position:absolute; left:12px; right:12px; bottom:12px; background:#1d1f25e6; border:1px solid var(--bord); border-radius:8px; padding:8px 10px; }
  #anims { display:flex; flex-wrap:wrap; gap:4px; margin-bottom:6px; max-height:84px; overflow:auto; }
  #anims button, .b { background:#2c2f38; color:var(--texte); border:0; border-radius:5px; padding:3px 8px; cursor:pointer; font:inherit; font-size:12px; }
  #anims button.actif { background:var(--accent); color:#1b1b1b; }
  .ligne { display:flex; gap:10px; align-items:center; flex-wrap:wrap; font-size:12px; color:var(--doux); }
  .ligne input[type=range] { flex:1; min-width:120px; }
  #vide[hidden] { display:none; }
  #vide { position:absolute; inset:0; display:grid; place-items:center; color:var(--doux); }
</style>
</head>
<body>
<nav id="liste">
  <h1>Modèles</h1>
  <input id="filtre" type="search" placeholder="Chercher un modèle…">
  <div id="groupes"></div>
</nav>
<main id="scene">
  <canvas id="vue"></canvas>
  <div id="vide">Choisir un modèle dans la liste.</div>
  <div id="infos"><h2 id="nom"></h2><p id="ou"></p></div>
  <div id="commandes" hidden>
    <div id="anims"></div>
    <div class="ligne">
      <button class="b" id="lecture">Pause</button>
      <label>Vitesse <select id="vitesse"><option value="0.25">× 0,25</option><option value="0.5">× 0,5</option><option value="1" selected>× 1</option><option value="2">× 2</option></select></label>
      <input type="range" id="image" min="0" max="0" value="0">
      <span id="compteur"></span>
      <label><input type="checkbox" id="tourne"> Tourner</label>
    </div>
  </div>
</main>
<script type="application/json" id="catalogue">@@CATALOGUE@@</script>
<script>
(() => {
  const CAT = JSON.parse(document.getElementById('catalogue').textContent);
  const groupes = document.getElementById('groupes'), filtre = document.getElementById('filtre');
  let boutons = [];
  function liste() {
    groupes.innerHTML = ''; boutons = [];
    const q = filtre.value.trim().toLowerCase();
    const par = new Map();
    CAT.forEach((m, i) => { if (q && !m.nom.toLowerCase().includes(q)) return; if (!par.has(m.groupe)) par.set(m.groupe, []); par.get(m.groupe).push(i); });
    for (const [g, idx] of par) {
      const d = document.createElement('details'); d.open = !!q || g === 'Héros';
      const s = document.createElement('summary'); s.textContent = g + ' (' + idx.length + ')'; d.appendChild(s);
      idx.sort((a, b) => CAT[a].nom.localeCompare(CAT[b].nom, 'fr'));
      for (const i of idx) {
        const b = document.createElement('button'); b.className = 'm' + (i === courant ? ' actif' : '');
        b.textContent = CAT[i].nom + ' ';
        const sm = document.createElement('small'); sm.textContent = CAT[i].animations ? CAT[i].animations + ' anim.' : 'fixe'; b.appendChild(sm);
        b.onclick = () => choisir(i); d.appendChild(b); boutons[i] = b;
      }
      groupes.appendChild(d);
    }
  }
  filtre.addEventListener('input', liste);

  // ---------------------------------------------------------------- WebGL
  const cv = document.getElementById('vue'), gl = cv.getContext('webgl', { antialias: true });
  const sh = (t, s) => { const o = gl.createShader(t); gl.shaderSource(o, s); gl.compileShader(o); return o; };
  const prog = gl.createProgram();
  gl.attachShader(prog, sh(gl.VERTEX_SHADER, 'attribute vec3 p; attribute vec2 t; uniform mat4 m; varying vec2 v; void main(){ gl_Position = m * vec4(p, 1.0); v = t; }'));
  gl.attachShader(prog, sh(gl.FRAGMENT_SHADER, 'precision mediump float; uniform sampler2D s; uniform vec4 c; uniform float tx; varying vec2 v; void main(){ vec4 k = vec4(1.0); if (tx > 0.5) { k = texture2D(s, v); if (k.a < 0.5) discard; } gl_FragColor = k * c; }'));
  gl.linkProgram(prog); gl.useProgram(prog);
  const aP = gl.getAttribLocation(prog, 'p'), aT = gl.getAttribLocation(prog, 't');
  const uM = gl.getUniformLocation(prog, 'm'), uC = gl.getUniformLocation(prog, 'c'), uTx = gl.getUniformLocation(prog, 'tx');
  const bufP = gl.createBuffer(), bufT = gl.createBuffer(), bufG = gl.createBuffer();
  const tex = gl.createTexture();

  const f32 = b => { const s = atob(b), u = new Uint8Array(s.length); for (let i = 0; i < s.length; i++) u[i] = s.charCodeAt(i); return new Float32Array(u.buffer); };
  const i16 = b => { const s = atob(b), u = new Uint8Array(s.length); for (let i = 0; i < s.length; i++) u[i] = s.charCodeAt(i); return new Int16Array(u.buffer); };
  const u16 = b => { const s = atob(b), u = new Uint8Array(s.length); for (let i = 0; i < s.length; i++) u[i] = s.charCodeAt(i); return new Uint16Array(u.buffer); };

  // RotMatrix du jeu : angles sur 4096, matrice ligne par ligne.
  function rot(ax, ay, az) {
    const k = Math.PI * 2 / 4096, sx = Math.sin(ax * k), cx = Math.cos(ax * k), sy = Math.sin(ay * k), cy = Math.cos(ay * k), sz = Math.sin(az * k), cz = Math.cos(az * k);
    return [cy * cz, -sz * cy, sy, sz * cx + sx * sy * cz, -sx * sy * sz + cx * cz, -sx * cy, sx * sz - cx * sy * cz, sx * cz + cx * sy * sz, cx * cy];
  }

  let M = null, courant = -1, clip = 0, image = 0, joue = true, vitesse = 1, horloge = 0, dernier = 0;
  let sommets = null, uvs = null, nTri = 0, centre = [0, 0, 0], taille = 1, sol = 0;
  const cam = { yaw: 0.5, pitch: 0.25, dist: 3 };

  // Les matrices monde des os pour une image : W[i] = W[parent] · L[i], l'os 0 pendu a l'entite.
  function os(c, f) {
    const W = [];
    for (let i = 0; i < c.os; i++) {
      const q = c.idx[f * c.os + i] * 7, P = c.P;
      const r = rot(P[q], P[q + 1], P[q + 2]), s = (P[q + 6] & 0xffff) / 4096;
      const L = { r: r.map(v => v * s), t: [P[q + 3], P[q + 4], P[q + 5]] };
      const p = i > 0 && c.parents[i] < i ? W[c.parents[i]] : null;
      if (!p) { W.push(L); continue; }
      const R = new Array(9);
      for (let a = 0; a < 3; a++) for (let b = 0; b < 3; b++) R[a * 3 + b] = p.r[a * 3] * L.r[b] + p.r[a * 3 + 1] * L.r[3 + b] + p.r[a * 3 + 2] * L.r[6 + b];
      const T = [0, 1, 2].map(a => p.r[a * 3] * L.t[0] + p.r[a * 3 + 1] * L.t[1] + p.r[a * 3 + 2] * L.t[2] + p.t[a]);
      W.push({ r: R, t: T });
    }
    return W;
  }

  // Les sommets de l'image : chaque partie par la matrice de son os, puis vers y en haut (x, -y, -z).
  function poser() {
    const c = M.clips[clip];
    const W = c ? os(c, Math.min(image, c.images - 1)) : null;
    let k = 0;
    M.parties.forEach((pt, j) => {
      const b = c && j < c.osParties.length && c.osParties[j] < c.os ? W[c.osParties[j]] : null;
      for (let i = 0; i < pt.p.length; i += 3) {
        let x = pt.p[i], y = pt.p[i + 1], z = pt.p[i + 2];
        if (b) { const r = b.r; const X = r[0] * x + r[1] * y + r[2] * z + b.t[0], Y = r[3] * x + r[4] * y + r[5] * z + b.t[1], Z = r[6] * x + r[7] * y + r[8] * z + b.t[2]; x = X; y = Y; z = Z; }
        sommets[k++] = x / 100; sommets[k++] = -y / 100; sommets[k++] = -z / 100;
      }
    });
    gl.bindBuffer(gl.ARRAY_BUFFER, bufP); gl.bufferData(gl.ARRAY_BUFFER, sommets, gl.DYNAMIC_DRAW);
  }

  window.chargerModele = d => {
    M = d;
    M.parties.forEach(p => { p.p = f32(p.p); p.t = f32(p.t); });
    M.clips.forEach(c => { c.P = i16(c.poses); c.idx = u16(c.indices); });
    let n = 0; M.parties.forEach(p => n += p.p.length);
    sommets = new Float32Array(n); nTri = n / 3;
    uvs = new Float32Array(n / 3 * 2); let k = 0; M.parties.forEach(p => { uvs.set(p.t, k); k += p.t.length; });
    gl.bindBuffer(gl.ARRAY_BUFFER, bufT); gl.bufferData(gl.ARRAY_BUFFER, uvs, gl.STATIC_DRAW);
    gl.bindTexture(gl.TEXTURE_2D, tex);
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array([150, 150, 150, 255]));
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST); gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);
    if (M.texture) { const img = new Image(); img.onload = () => { gl.bindTexture(gl.TEXTURE_2D, tex); gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, img); }; img.src = M.texture; }
    clip = 0; image = 0; horloge = 0;
    poser();
    // Cadrage sur la premiere image.
    const mn = [Infinity, Infinity, Infinity], mx = [-Infinity, -Infinity, -Infinity];
    for (let i = 0; i < sommets.length; i++) { const a = i % 3; mn[a] = Math.min(mn[a], sommets[i]); mx[a] = Math.max(mx[a], sommets[i]); }
    if (!isFinite(mn[0])) { mn.fill(-1); mx.fill(1); }
    centre = [(mn[0] + mx[0]) / 2, (mn[1] + mx[1]) / 2, (mn[2] + mx[2]) / 2]; taille = Math.max(mx[0] - mn[0], mx[1] - mn[1], mx[2] - mn[2], 0.05); sol = mn[1];
    cam.dist = taille * 1.8;
    const g = []; const pas = Math.pow(10, Math.floor(Math.log10(taille))) / 2, ext = Math.ceil(taille * 1.5 / pas) * pas;
    for (let v = -ext; v <= ext + 1e-6; v += pas) { g.push(centre[0] + v, sol, centre[2] - ext, centre[0] + v, sol, centre[2] + ext, centre[0] - ext, sol, centre[2] + v, centre[0] + ext, sol, centre[2] + v); }
    gl.bindBuffer(gl.ARRAY_BUFFER, bufG); gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(g), gl.STATIC_DRAW); M.nGrille = g.length / 3;
    document.getElementById('nom').textContent = M.nom;
    document.getElementById('ou').textContent = M.famille + ' — vu : ' + M.vu.join(' ; ');
    const A = document.getElementById('anims'); A.innerHTML = '';
    M.clips.forEach((c, i) => { const b = document.createElement('button'); b.textContent = 'Animation ' + c.numero; b.title = c.images + ' images'; b.onclick = () => { clip = i; image = 0; horloge = 0; majAnims(); }; A.appendChild(b); });
    if (!M.clips.length) A.textContent = 'Ce modèle n’a pas d’animation.';
    document.getElementById('commandes').hidden = false; document.getElementById('vide').hidden = true;
    majAnims();
  };
  function majAnims() {
    [...document.querySelectorAll('#anims button')].forEach((b, i) => b.classList.toggle('actif', i === clip));
    const c = M.clips[clip], r = document.getElementById('image');
    r.max = c ? c.images - 1 : 0; r.value = image;
    document.getElementById('compteur').textContent = c ? (image + 1) + ' / ' + c.images : '';
  }
  function choisir(i) {
    courant = i; boutons.forEach((b, j) => b && b.classList.toggle('actif', j === i));
    const s = document.createElement('script');
    s.src = CAT[i].chemin.split('/').map(encodeURIComponent).join('/') + '/donnees.js';
    s.onerror = () => { document.getElementById('vide').hidden = false; document.getElementById('vide').textContent = 'Impossible de charger ce modèle.'; };
    document.body.appendChild(s); s.onload = () => s.remove();
    history.replaceState(null, '', '#' + encodeURIComponent(CAT[i].chemin));
  }
  document.getElementById('lecture').onclick = e => { joue = !joue; e.target.textContent = joue ? 'Pause' : 'Lecture'; };
  document.getElementById('vitesse').onchange = e => vitesse = +e.target.value;
  document.getElementById('image').oninput = e => { image = +e.target.value; joue = false; document.getElementById('lecture').textContent = 'Lecture'; poser(); majAnims(); };

  const mul = (a, b) => { const r = new Float32Array(16); for (let i = 0; i < 4; i++) for (let j = 0; j < 4; j++) { let s = 0; for (let k = 0; k < 4; k++) s += a[k * 4 + j] * b[i * 4 + k]; r[i * 4 + j] = s; } return r; };
  const persp = (f, a, n, z) => { const t = 1 / Math.tan(f / 2); return new Float32Array([t / a, 0, 0, 0, 0, t, 0, 0, 0, 0, (z + n) / (n - z), -1, 0, 0, 2 * z * n / (n - z), 0]); };
  const regard = (e, c) => {
    let z = [e[0] - c[0], e[1] - c[1], e[2] - c[2]]; let l = Math.hypot(...z); z = z.map(v => v / l);
    let x = [z[2], 0, -z[0]]; l = Math.hypot(...x) || 1; x = x.map(v => v / l);
    const y = [z[1] * x[2] - z[2] * x[1], z[2] * x[0] - z[0] * x[2], z[0] * x[1] - z[1] * x[0]];
    return new Float32Array([x[0], y[0], z[0], 0, x[1], y[1], z[1], 0, x[2], y[2], z[2], 0, -(x[0] * e[0] + x[1] * e[1] + x[2] * e[2]), -(y[0] * e[0] + y[1] * e[1] + y[2] * e[2]), -(z[0] * e[0] + z[1] * e[1] + z[2] * e[2]), 1]);
  };

  function boucle(ms) {
    const dt = Math.min(0.1, (ms - dernier) / 1000); dernier = ms;
    if (M) {
      const c = M.clips[clip];
      if (joue && c && c.images > 1) {
        horloge += dt * 30 * vitesse;
        const f = Math.floor(horloge) % c.images;
        if (f !== image) { image = f; poser(); majAnims(); }
      }
      if (document.getElementById('tourne').checked) cam.yaw += dt * 0.5;
      const w = cv.clientWidth * devicePixelRatio, h = cv.clientHeight * devicePixelRatio;
      if (cv.width !== w || cv.height !== h) { cv.width = w; cv.height = h; }
      gl.viewport(0, 0, w, h); gl.clearColor(0.08, 0.085, 0.1, 1); gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT); gl.enable(gl.DEPTH_TEST);
      const cp = Math.cos(cam.pitch), oeil = [centre[0] + cam.dist * cp * Math.sin(cam.yaw), centre[1] + cam.dist * Math.sin(cam.pitch), centre[2] + cam.dist * cp * Math.cos(cam.yaw)];
      const mvp = mul(persp(0.8, w / h, cam.dist / 100, cam.dist * 10), regard(oeil, centre));
      gl.uniformMatrix4fv(uM, false, mvp);
      gl.uniform1f(uTx, 0); gl.uniform4f(uC, 0.3, 0.32, 0.38, 1);
      gl.bindBuffer(gl.ARRAY_BUFFER, bufG); gl.enableVertexAttribArray(aP); gl.vertexAttribPointer(aP, 3, gl.FLOAT, false, 0, 0);
      gl.disableVertexAttribArray(aT); gl.vertexAttrib2f(aT, 0, 0);
      gl.drawArrays(gl.LINES, 0, M.nGrille);
      gl.bindBuffer(gl.ARRAY_BUFFER, bufP); gl.vertexAttribPointer(aP, 3, gl.FLOAT, false, 0, 0);
      gl.bindBuffer(gl.ARRAY_BUFFER, bufT); gl.enableVertexAttribArray(aT); gl.vertexAttribPointer(aT, 2, gl.FLOAT, false, 0, 0);
      gl.bindTexture(gl.TEXTURE_2D, tex); gl.uniform1f(uTx, 1); gl.uniform4f(uC, 1, 1, 1, 1);
      gl.drawArrays(gl.TRIANGLES, 0, nTri);
    }
    requestAnimationFrame(boucle);
  }
  let glisse = null;
  cv.addEventListener('mousedown', e => glisse = { x: e.clientX, y: e.clientY });
  addEventListener('mouseup', () => glisse = null);
  addEventListener('mousemove', e => { if (!glisse) return; cam.yaw -= (e.clientX - glisse.x) * 0.008; cam.pitch = Math.max(-1.4, Math.min(1.4, cam.pitch + (e.clientY - glisse.y) * 0.008)); glisse.x = e.clientX; glisse.y = e.clientY; });
  cv.addEventListener('wheel', e => { e.preventDefault(); cam.dist *= Math.exp(e.deltaY * 0.001); }, { passive: false });

  liste();
  const h = decodeURIComponent(location.hash.slice(1)), i0 = CAT.findIndex(m => m.chemin === h);
  if (i0 >= 0) choisir(i0);
  requestAnimationFrame(boucle);
})();
</script>
</body>
</html>
""";
}
