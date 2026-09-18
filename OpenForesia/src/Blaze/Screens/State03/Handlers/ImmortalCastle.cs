// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DU CHATEAU DE L'IMMORTEL — zone 6, table 0x0056adc8.
//
// Ouvert le 2026-09-18 avec le palier 1, sur le patron de `Mine.cs` et d'`OldTower.cs` : ce qui
// meuble le chateau et ce qui y sonne. Ce que le decor DECLENCHE — les mecanismes, ce qui consulte
// le groupe — ira dans un fichier separe le jour ou il s'en presentera, comme au Labyrinthe
// (`Labyrinth.cs` / `LabyrinthMecanismes.cs`).
//
// LES QUATRE CORPS DE CE PALIER SONT DES VARIANTES DE CORPS DEJA ECRITS AILLEURS, et chacun a
// exactement UN ecart avec son modele. Ils valent surtout par ce que leur relevé a coute :
//
//   0x00463C80  <- 0x004508B0 (foret)    l'echelle est posee en +0x2a, pas en +0x2c, et vaut 9000
//   0x00465180  <- 0x0045A310 (tour)     le son de zone est le creneau 0x28, pas 0x30
//   0x00466A10  <- 0x00452A90 (mine)     l'appel de tete, ClearEntityVelocity, N'EST PAS LA
//   0x00463E80  <- 0x00453000 (mine)     le son part par 0x00468080 et NON par 0x004580e0
//
// LE DERNIER EST LE PIEGE DU LOT, ET IL SE REPETERA. La normalisation qui sert a apparier les corps
// masque les cibles d'appel — c'est ce qui rend un corps recompile comparable a son modele. Mais
// elle masque AUSSI un changement de callee : 0x00463E80 et 0x00453000 se ressemblent a six lignes
// pres, et ces six lignes ne disent rien de l'essentiel, qui est que l'un appelle le son de
// proximite a portee variable et l'autre le son a rayon fixe de 2000. Contre un corps « presque
// identique », RESOUDRE LES CIBLES D'APPEL A PART avant de conclure.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00463c80 @ 0x00463c80 — creneau <b>40</b> du bloc <c>0x00558e28</c>
    /// (table <c>0x0056adc8</c>, sous-zones 0 a 4). Decompilee dans <c>Blaze.c</c> ; six
    /// instructions.
    ///
    /// <para>
    /// <b>La forme de <see cref="EntityHandler_004508B0"/></b> (foret), a une instruction pres, et
    /// les deux moities de cette instruction different : l'echelle est posee en <c>+0x2a</c> et non
    /// en <c>+0x2c</c>, et elle vaut <c>0x2328</c> — 9000 — la ou la foret pose <c>0x320</c>.
    /// C'est donc un decor <b>tres haut</b>, pas une copie a l'echelle pres.
    /// </para>
    /// <para>
    /// Le bit est pose par <c>or ch,40h</c>, sur le <b>deuxieme octet</b> de <c>ecx</c> :
    /// <c>0x4000</c>, pas <c>0x40</c>. Meme piege de lecture que chez son modele.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463C80(byte* e)
    {
        *(ushort*)(e + 0x2a) = 0x2328;
        *(uint*)e |= 0x4000;
    }

    /// <summary>
    /// Ghidra: FUN_00465180 @ 0x00465180 — creneau <b>49</b> du bloc <c>0x005594a8</c>
    /// (sous-zones 6 a 10). Decompilee dans <c>Blaze.c</c> ; 49 instructions.
    ///
    /// <para>
    /// <b>Le son au hasard de <see cref="EntityHandler_0045A310"/></b> (vieille tour), au creneau
    /// de son sonore pres : <c>0x28</c> au lieu de <c>0x30</c>. Tout le reste est identique
    /// instruction pour instruction — le premier tour pose <c>0x6000</c> sur le mot d'etat, un
    /// compteur monte d'un par image, et passe 30 il y a une chance sur soixante par image de
    /// sonner, au plus tard a 51. Hauteur tiree entre 500 et 699, gain entre <c>0x9c4</c> et
    /// <c>0x1193</c>, deux tirages dans cet ordre.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465180(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x6000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;

        if (((short)(CrtRand() % 0x3c) == 1 && *(short*)(e + 0x12) > 0x1e) || *(short*)(e + 0x12) > 0x32)
        {
            *(short*)(e + 0x12) = 0;

            int pitch = (short)(CrtRand() % 200 + 500);
            int gain = (short)(CrtRand() % 2000 + 0x9c4);
            Aud.PlaySoundEffect(ZoneSound(0x28), pitch, gain, gain);           // Ghidra: FUN_0053cea0
        }
    }

    /// <summary>
    /// Ghidra: FUN_00466a10 @ 0x00466a10 — creneau <b>20</b> du bloc <c>0x00559b28</c>
    /// (sous-zones 6 a 10). Decompilee dans <c>Blaze.c</c> ; 44 instructions.
    ///
    /// <para>
    /// <b>Le halo vacillant de <see cref="TickFlickeringGlow"/></b> (mine), <b>moins son appel de
    /// tete</b> : les 41 instructions qui suivent sont les memes une a une, mais le chateau
    /// n'annule pas la vitesse de l'entite avant de commencer. Trois instructions de moins, et la
    /// seule difference du corps.
    /// </para>
    /// <para>
    /// Deux gestionnaires du chateau portent l'autre forme, celle QUI annule la vitesse :
    /// <c>0x00464B30</c> et <c>0x00466470</c> sont des doublons octet pour octet de
    /// <c>0x00452A90</c> — memes 47 instructions, memes trois cibles d'appel — et le repartiteur
    /// les envoie directement sur le corps de la mine.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466A10(byte* e) => TickFlickeringGlow(e, clearVelocity: false);

    /// <summary>
    /// Ghidra: FUN_00463e80 @ 0x00463e80 — creneau <b>30</b> du bloc <c>0x00558e28</c>
    /// (sous-zones 0 a 4). Decompilee dans <c>Blaze.c</c> ; 31 instructions.
    ///
    /// <para>
    /// <b>L'ossature de <see cref="TickRepeatingDistanceSound"/></b> (mine) : le premier tour pose
    /// l'etat a 1, le bit <c>0x4000</c> sur le mot d'etat et le compteur a zero ; ensuite le
    /// compteur avance d'un par image et, a la dixieme, revient a zero et lache le son. Le premier
    /// son part donc a la dixieme image, pas a la naissance.
    /// </para>
    /// <para>
    /// <b>MAIS CE N'EST PAS LE MEME SON.</b> La mine appelle <c>0x004580e0</c>, le son de
    /// proximite a <b>portee variable</b>, non positionnel, attenue lineairement ; le chateau
    /// appelle <c>0x00468080</c>, <see cref="TryPlaySoundNearEntity"/>, dont le rayon est
    /// <b>fige a 2000</b>, qui joue un son <b>positionnel</b> et qui <b>double le gain</b>. La
    /// normalisation qui appariait les deux corps masquait la cible de l'appel : c'est en la
    /// resolvant a part que l'ecart est apparu. Les trois autres immediates suivent — creneau
    /// sonore <c>0x18</c> au lieu de <c>0x16</c>, gain <c>0x1000</c> au lieu de <c>0x2000</c>, et
    /// le cinquieme argument vaut <b>zero</b> au lieu de <c>0x7d0</c> ; celui-la n'est lu nulle
    /// part, ni ici ni chez sa jumelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463E80(byte* e) => TickCastleRepeatingSound(e);

    /// <summary>Ghidra: FUN_004657b0 @ 0x004657b0 — creneau <b>30</b> du bloc <c>0x005594a8</c>,
    /// la copie du precedent pour les sous-zones 6 a 10. Comparee instruction par instruction a
    /// <see cref="EntityHandler_00463E80"/>, cible d'appel comprise : <b>zero
    /// difference</b>.</summary>
    private static void EntityHandler_004657B0(byte* e) => TickCastleRepeatingSound(e);

    /// <summary>Le corps commun de <see cref="EntityHandler_00463E80"/> et de
    /// <see cref="EntityHandler_004657B0"/>. Voir le resume du premier pour ce qui le separe de
    /// <see cref="TickRepeatingDistanceSound"/>.</summary>
    private static void TickCastleRepeatingSound(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x0a) return;

        *(short*)(e + 0x12) = 0;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x18), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
    }
}
