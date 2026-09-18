// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DU CHATEAU DE L'IMMORTEL — zone 6.
//
// Ouvert le 2026-09-18 au palier 2, sur le patron de `MineMecanismes.cs` et de
// `LabyrinthMecanismes.cs`, et pour la raison que le test d'appartenance d'ARCHITECTURE.md donne :
// ces quatre corps LANCENT UN SCRIPT DE ZONE. Ils ne meublent pas le chateau, ils font avancer la
// partie. Le decor, lui, est dans `ImmortalCastle.cs`.
//
// La coupure est de role et non de taille : le fichier nait a quatre corps, comme celui du
// Labyrinthe est ne a trois. Le partage se lit dans le corps — `RunEntityScript` d'un cote,
// `TryPlaySoundNearEntity` et les echelles de l'autre.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Le corps de <see cref="EntityHandler_004647F0"/> et de son miroir. Absent de
    /// <c>Blaze.c</c> ; 82 instructions relevees au desassembleur. Les deux exemplaires sont
    /// <b>identiques instruction pour instruction</b>, cibles d'appel comprises.
    ///
    /// <para>
    /// <b>Un battant qui oscille cinq fois puis rend la main au scenario.</b> A la naissance il
    /// retient son angle de repos dans <c>+0x14</c>, se donne une vitesse angulaire de
    /// <c>-0x28</c> dans <c>+0x16</c> et met son compteur de butees <c>+0x5c</c> a zero.
    /// </para>
    /// <para>
    /// Ensuite l'angle <c>+0x34</c> avance de <c>+0x16</c> par image. Des qu'il depasse
    /// <b><c>±0x64</c> autour du repos</b>, il est <b>ramene exactement sur la butee</b>, la
    /// vitesse est retournee, le compteur avance d'un, et le son <c>0x15</c> de la zone part par
    /// <see cref="TryPlaySoundNearEntity"/> — hauteur <c>0x200</c>, gain <c>0x1800</c>. Le battant
    /// claque donc a chaque bout de sa course, pas au milieu.
    /// </para>
    /// <para>
    /// <b>A la cinquieme butee</b> l'angle revient au repos, l'etat passe a 2 — plus rien ne bouge
    /// — et <see cref="RunEntityScript"/> est lance sur l'entite <c>+0x6c</c>, celle qui l'a
    /// designe. C'est la seule instruction qui fait de ce corps un mecanisme et non un decor.
    /// </para>
    /// <para>
    /// <b>Le compteur est un <c>int</c>, pas un mot</b> : <c>+0x5c</c> est lu et ecrit en 32 bits,
    /// la ou <c>+0x12</c>, <c>+0x14</c> et <c>+0x16</c> sont des <c>short</c>. Le binaire melange
    /// les deux largeurs dans le meme corps.
    /// </para>
    /// </summary>
    private static void TickCastleSwingingLeaf_004647F0(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = *(short*)(e + 0x34);   // l'angle de repos, retenu
            *(short*)(e + 0x16) = -0x28;
            *(int*)(e + 0x5c) = 0;
        }

        if (state != 1) return;

        *(short*)(e + 0x34) += *(short*)(e + 0x16);

        short rest = *(short*)(e + 0x14);

        if (*(short*)(e + 0x34) < rest - 0x64)
        {
            *(short*)(e + 0x34) = (short)(rest - 0x64);
            *(short*)(e + 0x16) = (short)-*(short*)(e + 0x16);
            (*(int*)(e + 0x5c))++;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x15), 0x200, 0x1800, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x34) > rest + 0x64)
        {
            *(short*)(e + 0x34) = (short)(rest + 0x64);
            *(short*)(e + 0x16) = (short)-*(short*)(e + 0x16);
            (*(int*)(e + 0x5c))++;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x15), 0x200, 0x1800, 0, 0);
        }

        if (*(int*)(e + 0x5c) < 5) return;

        *(short*)(e + 0x34) = rest;
        *(short*)(e + 0x10) = 2;
        RunEntityScript(s_Unknown_005e97cc, *(byte**)(e + 0x6c), 0);   // Ghidra: FUN_00513dd0
    }

    /// <summary>Ghidra: <c>0x004647f0</c> — creneau <b>15</b> du bloc <c>0x00558e28</c>
    /// (table <c>0x0056adc8</c>, sous-zones 0 a 4).</summary>
    private static void EntityHandler_004647F0(byte* e) => TickCastleSwingingLeaf_004647F0(e);

    /// <summary>Ghidra: <c>0x00466130</c> — creneau <b>15</b> du bloc <c>0x005594a8</c>
    /// (sous-zones 6 a 10). <b>Zero difference</b> avec le precedent.</summary>
    private static void EntityHandler_00466130(byte* e) => TickCastleSwingingLeaf_004647F0(e);

    /// <summary>
    /// Ghidra: FUN_00426860 @ 0x00426860 — creneau <b>43</b> du bloc <c>0x00559128</c>
    /// (table <c>0x0056b0c8</c>, sous-zones 0 a 4 et 6 a 10). Decompilee dans <c>Blaze.c</c>,
    /// 62 instructions.
    ///
    /// <para>
    /// <b>Quinze images d'attente, un coup sourd, puis une secousse qui s'eteint.</b> L'etat 1
    /// decompte <c>+0x14</c> de 15 a zero ; a la fin il joue le son <c>0x27</c> de la zone
    /// (hauteur <c>0x400</c>, gain <c>0x1000</c>) et passe a l'etat 2.
    /// </para>
    /// <para>
    /// <b>La secousse est un pendule amorti, ecrit en trois lignes.</b> A chaque image,
    /// <c>+0x12</c> est ramene d'une unite <b>vers</b> zero, puis <b>retourne</b> ; le decalage
    /// vertical de camera <c>DAT_005de0ac</c> recoit son oppose. La valeur passe donc
    /// <c>+15, -14, +13, -12…</c> et la camera tremble de moins en moins.
    /// </para>
    /// <para>
    /// <b>Quand elle atteint zero, l'entite s'eteint et le scenario repart</b> :
    /// <see cref="RunEntityScript"/> est lance sur <b>le membre du groupe designe par
    /// <c>DAT_005de538</c></b>, la memoire d'emplacement — pas sur une entite de scene. C'est ce
    /// qui le range ici et non dans <c>ImmortalCastle.cs</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00426860(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x0f;
            *(short*)(e + 0x14) = 0x0f;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x14))--;

            if (*(short*)(e + 0x14) < 0)
            {
                *(short*)(e + 0x10) = 2;
                Aud.PlaySoundEffect(ZoneSound(0x27), 0x400, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
            }

            return;
        }

        // Ghidra: l'amortissement, en deux tests separes — un pas vers zero, quel que soit le signe.
        if (*(short*)(e + 0x12) > 0) (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0) (*(short*)(e + 0x12))++;

        if (*(short*)(e + 0x12) == 0)
        {
            *(uint*)e &= 0x7fffffff;
            RunEntityScript(s_Unknown_005e97cc,
                            Inp.s_SlotFlags_005e93c0 + (Mem.s_Unknown_005de538 & 0xff) * 0x9c, 0);
        }

        *Mem.Camera(0x005de0ac) = -*(short*)(e + 0x12);
        *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
    }

    /// <summary>
    /// Ghidra: FUN_00426b10 @ 0x00426b10 — creneau <b>14</b> du bloc <c>0x00559128</c>
    /// (sous-zones 0 a 4). Decompilee dans <c>Blaze.c</c>, 94 instructions.
    ///
    /// <para>
    /// <b>Une source de quarante images, avec quatre coups et un script au milieu.</b> A chaque
    /// image elle fait naitre <b>six</b> entites sur sa propre position — trois du sous-type
    /// <c>199</c> par <see cref="SpawnStatusEffectEntity_0047AC40"/>, trois du sous-type
    /// <c>0xc3</c> par <see cref="SpawnCastEntity_0047AAE0"/> — exactement comme
    /// <see cref="EntityHandler_00429330"/>, qui en est la moitie muette et reste, elle, du cote
    /// des effets.
    /// </para>
    /// <para>
    /// Elle y ajoute le son <c>0x28</c> de la zone aux images <b>10, 20 et 30</b>, et surtout
    /// <see cref="RunEntityScript"/> a l'image <b>20</b>, sur le membre du groupe designe par
    /// <c>DAT_005de538</c>. Passe quarante images, elle s'eteint.
    /// </para>
    /// <para>
    /// <b>Une bizarrerie fidele</b> : le binaire teste aussi <c>+0x12 == 0</c> pour le son, mais le
    /// compteur est incremente <b>avant</b> les tests — il vaut 1 des la premiere image et ne
    /// repasse jamais par zero. Ce quatrieme coup ne part donc jamais. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00426B10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;

        int* position = (int*)(e + 0x1c);
        SpawnStatusEffectEntity_0047AC40(e, position, null, 199, 0, -1);
        SpawnStatusEffectEntity_0047AC40(e, position, null, 199, 0, -1);
        SpawnStatusEffectEntity_0047AC40(e, position, null, 199, 0, -1);
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);

        short beat = *(short*)(e + 0x12);
        if (beat == 0 || beat == 10 || beat == 0x14 || beat == 0x1e)
        {
            Aud.PlaySoundEffect(ZoneSound(0x28), 0x600, 0x1500, 0x1500);   // Ghidra: FUN_0053cea0
        }

        if (*(short*)(e + 0x12) == 0x14)
        {
            RunEntityScript(s_Unknown_005e97cc,
                            Inp.s_SlotFlags_005e93c0 + (Mem.s_Unknown_005de538 & 0xff) * 0x9c, 0);
        }

        if (*(short*)(e + 0x12) > 0x28) *(uint*)e &= 0x7fffffff;
    }
}
