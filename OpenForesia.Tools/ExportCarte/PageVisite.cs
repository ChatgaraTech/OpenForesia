namespace ExportCarte;

/// <summary>
/// La page de visite d'une carte : un fichier HTML autonome, sans serveur ni bibliotheque, qui
/// dessine la carte texturee en WebGL. Les donnees (triangles, textures, reperes) sont embarquees
/// a la place de <c>@@DONNEES@@</c>.
/// </summary>
internal static class PageVisite
{
    public const string Modele = """
<!doctype html>
<html lang="fr">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>@@TITRE@@</title>
<style>
  :root { color-scheme: dark; --fond:#15161a; --panneau:#22242bdd; --texte:#e8e6e1; --doux:#9a9890; }
  html, body { margin:0; height:100%; background:var(--fond); color:var(--texte); font:14px/1.4 system-ui, sans-serif; overflow:hidden; }
  canvas { display:block; width:100vw; height:100vh; cursor:grab; }
  canvas:active { cursor:grabbing; }
  #panneau { position:fixed; top:12px; left:12px; background:var(--panneau); padding:10px 14px; border-radius:8px; max-width:min(360px, calc(100vw - 48px)); }
  #panneau h1 { font-size:16px; margin:0 0 6px; }
  #panneau label { display:block; margin:3px 0; cursor:pointer; }
  #panneau p { margin:6px 0 0; color:var(--doux); font-size:12px; }
  #panneau button { margin-top:6px; background:#3a3d47; color:var(--texte); border:0; border-radius:5px; padding:4px 10px; cursor:pointer; }
  #reperes { position:fixed; inset:0; pointer-events:none; }
  .rep { position:absolute; width:12px; height:12px; margin:-6px 0 0 -6px; border-radius:50%; border:2px solid #000; pointer-events:auto; cursor:help; }
  .rep.decor { width:8px; height:8px; margin:-4px 0 0 -4px; border-width:1px; }
  .monstre { background:#eb3c32; } .coffre { background:#fad228; } .entree { background:#3cdcf0; } .decor { background:#e6e6e6; }
  #bulle { position:fixed; pointer-events:none; background:#000d; padding:6px 9px; border-radius:6px; display:none; max-width:320px; }
  #bulle b { display:block; }
</style>
</head>
<body>
<canvas id="vue"></canvas>
<div id="reperes"></div>
<div id="bulle"></div>
<div id="panneau">
  <h1 id="titre"></h1>
  <label><input type="checkbox" id="vCollision"> Collisions (sols en vert)</label>
  <label><input type="checkbox" id="vMonstres" checked> Monstres et personnages</label>
  <label><input type="checkbox" id="vCoffres" checked> Coffres</label>
  <label><input type="checkbox" id="vEntrees" checked> Entrées</label>
  <label><input type="checkbox" id="vDecor"> Décor actif</label>
  <button id="dessus">Vue du dessus</button> <button id="biais">Vue de biais</button>
  <p>Glisser pour tourner, clic droit (ou Maj) pour se déplacer, molette pour avancer.</p>
</div>
<script type="application/json" id="donnees">@@DONNEES@@</script>
<script>
(() => {
  const D = JSON.parse(document.getElementById('donnees').textContent);
  document.getElementById('titre').textContent = D.titre;
  const cv = document.getElementById('vue');
  const gl = cv.getContext('webgl', { antialias: true });
  if (!gl) { document.body.insertAdjacentHTML('beforeend', '<p style="position:fixed;top:50%;width:100%;text-align:center">Ce navigateur n’affiche pas la 3D (WebGL).</p>'); return; }

  const f32 = b64 => { const s = atob(b64), b = new Uint8Array(s.length); for (let i = 0; i < s.length; i++) b[i] = s.charCodeAt(i); return new Float32Array(b.buffer); };
  const sh = (type, src) => { const s = gl.createShader(type); gl.shaderSource(s, src); gl.compileShader(s); return s; };
  const prog = gl.createProgram();
  gl.attachShader(prog, sh(gl.VERTEX_SHADER, 'attribute vec3 p; attribute vec2 t; uniform mat4 m; varying vec2 v; void main(){ gl_Position = m * vec4(p, 1.0); v = t; }'));
  gl.attachShader(prog, sh(gl.FRAGMENT_SHADER, 'precision mediump float; uniform sampler2D s; uniform vec4 c; uniform float tx; varying vec2 v; void main(){ vec4 k = vec4(1.0); if (tx > 0.5) { k = texture2D(s, v); if (k.a < 0.5) discard; } gl_FragColor = k * c; }'));
  gl.linkProgram(prog); gl.useProgram(prog);
  const aP = gl.getAttribLocation(prog, 'p'), aT = gl.getAttribLocation(prog, 't');
  const uM = gl.getUniformLocation(prog, 'm'), uC = gl.getUniformLocation(prog, 'c'), uTx = gl.getUniformLocation(prog, 'tx');

  const tex = D.textures.map(src => {
    const t = gl.createTexture();
    gl.bindTexture(gl.TEXTURE_2D, t);
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, new Uint8Array([128, 128, 128, 255]));
    const img = new Image();
    img.onload = () => { gl.bindTexture(gl.TEXTURE_2D, t); gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, img);
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST); gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST); dessiner(); };
    img.src = src;
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST);
    return t;
  });
  const tampon = a => { const b = gl.createBuffer(); gl.bindBuffer(gl.ARRAY_BUFFER, b); gl.bufferData(gl.ARRAY_BUFFER, a, gl.STATIC_DRAW); return b; };

  let min = [Infinity, Infinity, Infinity], max = [-Infinity, -Infinity, -Infinity];
  const groupes = D.groupes.map(g => {
    const p = f32(g.p);
    for (let i = 0; i < p.length; i++) { const k = i % 3; if (p[i] < min[k]) min[k] = p[i]; if (p[i] > max[k]) max[k] = p[i]; }
    return { tex: g.tex, semi: g.semi, n: p.length / 3, bp: tampon(p), bt: tampon(f32(g.t)) };
  });
  const collision = D.collision.map(c => { const p = f32(c.p); return { sol: c.sol, n: p.length / 3, bp: tampon(p) }; });
  if (!isFinite(min[0])) { min = [-1, -1, -1]; max = [1, 1, 1]; }
  const centre = [(min[0] + max[0]) / 2, (min[1] + max[1]) / 2, (min[2] + max[2]) / 2];
  const taille = Math.max(max[0] - min[0], max[1] - min[1], max[2] - min[2], 0.5);

  const cam = { yaw: 0.6, pitch: 0.9, dist: taille * 1.1, cible: centre.slice() };
  const vueDessus = () => { cam.yaw = 0; cam.pitch = Math.PI / 2 - 0.001; cam.dist = taille * 1.2; cam.cible = centre.slice(); dessiner(); };
  const vueBiais = () => { cam.yaw = 0.6; cam.pitch = 0.9; cam.dist = taille * 1.1; cam.cible = centre.slice(); dessiner(); };

  const mul = (a, b) => { const r = new Float32Array(16); for (let i = 0; i < 4; i++) for (let j = 0; j < 4; j++) { let s = 0; for (let k = 0; k < 4; k++) s += a[k * 4 + j] * b[i * 4 + k]; r[i * 4 + j] = s; } return r; };
  const persp = (fov, asp, n, f) => { const t = 1 / Math.tan(fov / 2); return new Float32Array([t / asp, 0, 0, 0, 0, t, 0, 0, 0, 0, (f + n) / (n - f), -1, 0, 0, 2 * f * n / (n - f), 0]); };
  const regard = (e, c, u) => {
    let z = [e[0] - c[0], e[1] - c[1], e[2] - c[2]]; let l = Math.hypot(...z); z = z.map(v => v / l);
    let x = [u[1] * z[2] - u[2] * z[1], u[2] * z[0] - u[0] * z[2], u[0] * z[1] - u[1] * z[0]]; l = Math.hypot(...x); x = x.map(v => v / l);
    const y = [z[1] * x[2] - z[2] * x[1], z[2] * x[0] - z[0] * x[2], z[0] * x[1] - z[1] * x[0]];
    return new Float32Array([x[0], y[0], z[0], 0, x[1], y[1], z[1], 0, x[2], y[2], z[2], 0,
      -(x[0] * e[0] + x[1] * e[1] + x[2] * e[2]), -(y[0] * e[0] + y[1] * e[1] + y[2] * e[2]), -(z[0] * e[0] + z[1] * e[1] + z[2] * e[2]), 1]);
  };

  const calques = document.getElementById('reperes');
  const elems = D.reperes.map(r => {
    const d = document.createElement('div');
    d.className = 'rep ' + r.genre;
    d.addEventListener('mouseenter', ev => montrer(r, ev)); d.addEventListener('mousemove', ev => montrer(r, ev));
    d.addEventListener('mouseleave', () => bulle.style.display = 'none');
    calques.appendChild(d);
    return d;
  });
  const bulle = document.getElementById('bulle');
  const libelles = { monstre: 'Monstre ou personnage', coffre: 'Coffre', entree: 'Entrée', decor: 'Décor actif' };
  function montrer(r, ev) {
    bulle.innerHTML = '';
    const b = document.createElement('b'); b.textContent = r.nom; bulle.appendChild(b);
    const s = document.createElement('span'); s.textContent = libelles[r.genre] + ' — ' + r.code + (r.detail ? ' — ' + r.detail : ''); bulle.appendChild(s);
    bulle.style.display = 'block';
    bulle.style.left = Math.min(ev.clientX + 14, innerWidth - bulle.offsetWidth - 8) + 'px';
    bulle.style.top = (ev.clientY + 14) + 'px';
  }
  const coche = id => document.getElementById(id).checked;
  ['vCollision', 'vMonstres', 'vCoffres', 'vEntrees', 'vDecor'].forEach(id => document.getElementById(id).addEventListener('change', () => dessiner()));
  document.getElementById('dessus').onclick = vueDessus;
  document.getElementById('biais').onclick = vueBiais;

  function dessiner() {
    const w = cv.clientWidth * devicePixelRatio, h = cv.clientHeight * devicePixelRatio;
    if (cv.width !== w || cv.height !== h) { cv.width = w; cv.height = h; }
    gl.viewport(0, 0, w, h);
    gl.clearColor(0.08, 0.085, 0.1, 1); gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
    gl.enable(gl.DEPTH_TEST);
    const cp = Math.cos(cam.pitch), oeil = [cam.cible[0] + cam.dist * cp * Math.sin(cam.yaw), cam.cible[1] + cam.dist * Math.sin(cam.pitch), cam.cible[2] + cam.dist * cp * Math.cos(cam.yaw)];
    const haut = cam.pitch > 1.5 ? [-Math.sin(cam.yaw), 0, -Math.cos(cam.yaw)] : [0, 1, 0];
    const mvp = mul(persp(0.9, w / h, cam.dist / 200, cam.dist * 4 + taille * 2), regard(oeil, cam.cible, haut));
    gl.uniformMatrix4fv(uM, false, mvp);
    gl.enableVertexAttribArray(aP);

    const passe = semi => {
      for (const g of groupes) {
        if (g.semi !== semi) continue;
        gl.bindBuffer(gl.ARRAY_BUFFER, g.bp); gl.vertexAttribPointer(aP, 3, gl.FLOAT, false, 0, 0);
        gl.bindBuffer(gl.ARRAY_BUFFER, g.bt); gl.enableVertexAttribArray(aT); gl.vertexAttribPointer(aT, 2, gl.FLOAT, false, 0, 0);
        if (g.tex >= 0) { gl.bindTexture(gl.TEXTURE_2D, tex[g.tex]); gl.uniform1f(uTx, 1); gl.uniform4f(uC, 1, 1, 1, semi ? 0.5 : 1); }
        else { gl.uniform1f(uTx, 0); gl.uniform4f(uC, 0.5, 0.5, 0.5, semi ? 0.5 : 1); }
        gl.drawArrays(gl.TRIANGLES, 0, g.n);
      }
    };
    gl.disable(gl.BLEND); gl.depthMask(true); passe(false);
    gl.enable(gl.BLEND); gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA); gl.depthMask(false); passe(true);
    if (coche('vCollision')) {
      gl.disableVertexAttribArray(aT); gl.vertexAttrib2f(aT, 0, 0); gl.uniform1f(uTx, 0);
      gl.enable(gl.POLYGON_OFFSET_FILL); gl.polygonOffset(-1, -2);
      for (const c of collision) {
        gl.bindBuffer(gl.ARRAY_BUFFER, c.bp); gl.vertexAttribPointer(aP, 3, gl.FLOAT, false, 0, 0);
        if (c.sol) gl.uniform4f(uC, 0.2, 0.9, 0.3, 0.45); else gl.uniform4f(uC, 0.95, 0.3, 0.2, 0.45);
        gl.drawArrays(gl.TRIANGLES, 0, c.n);
      }
      gl.disable(gl.POLYGON_OFFSET_FILL);
    }
    gl.depthMask(true);

    const montres = { monstre: coche('vMonstres'), coffre: coche('vCoffres'), entree: coche('vEntrees'), decor: coche('vDecor') };
    D.reperes.forEach((r, i) => {
      const e = elems[i];
      const x = mvp[0] * r.x + mvp[4] * r.y + mvp[8] * r.z + mvp[12], y = mvp[1] * r.x + mvp[5] * r.y + mvp[9] * r.z + mvp[13], ww = mvp[3] * r.x + mvp[7] * r.y + mvp[11] * r.z + mvp[15];
      if (!montres[r.genre] || ww <= 0) { e.style.display = 'none'; return; }
      e.style.display = 'block';
      e.style.left = ((x / ww) * 0.5 + 0.5) * cv.clientWidth + 'px';
      e.style.top = (0.5 - (y / ww) * 0.5) * cv.clientHeight + 'px';
    });
  }

  let glisse = null;
  cv.addEventListener('contextmenu', e => e.preventDefault());
  cv.addEventListener('mousedown', e => { glisse = { x: e.clientX, y: e.clientY, depl: e.button === 2 || e.shiftKey }; });
  addEventListener('mouseup', () => glisse = null);
  addEventListener('mousemove', e => {
    if (!glisse) return;
    const dx = e.clientX - glisse.x, dy = e.clientY - glisse.y; glisse.x = e.clientX; glisse.y = e.clientY;
    if (glisse.depl) {
      const k = cam.dist / cv.clientHeight, cy = Math.cos(cam.yaw), sy = Math.sin(cam.yaw);
      cam.cible[0] -= (dx * cy + dy * sy * Math.sin(cam.pitch)) * k; cam.cible[2] -= (-dx * sy + dy * cy * Math.sin(cam.pitch)) * k;
      if (cam.pitch < 1.5) cam.cible[1] += dy * Math.cos(cam.pitch) * k;
    } else { cam.yaw -= dx * 0.006; cam.pitch = Math.max(-1.55, Math.min(Math.PI / 2 - 0.001, cam.pitch + dy * 0.006)); }
    dessiner();
  });
  cv.addEventListener('wheel', e => { e.preventDefault(); cam.dist *= Math.exp(e.deltaY * 0.001); dessiner(); }, { passive: false });
  addEventListener('resize', dessiner);
  // Un angle de vue dans l'adresse (#angle=lacet,tangage,distance relative), pour retrouver une vue.
  const m = /angle=([-\d.]+),([-\d.]+),([-\d.]+)/.exec(location.hash);
  if (m) { cam.yaw = +m[1]; cam.pitch = +m[2]; cam.dist = taille * +m[3]; }
  dessiner();
})();
</script>
</body>
</html>
""";
}
