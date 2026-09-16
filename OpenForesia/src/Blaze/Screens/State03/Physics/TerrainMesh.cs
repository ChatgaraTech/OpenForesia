// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE PAS SUR LE MAILLAGE DU TERRAIN — 0x00541550, creneau 63 de la table 0x0056afc8.
//
// Une entite ne se deplace pas dans le vide : elle traverse un maillage de triangles, et chaque
// pas peut franchir une arete. `SegmentCrossesEdge` teste, `FindCrossedEdge` trouve laquelle, et
// `StepEntityAcrossTerrain_00541550` fait passer l'entite de l'un a l'autre en mettant a jour son
// triangle courant.
//
// C'est ce qui rend la hauteur du sol correcte sans qu'aucun gestionnaire ait a s'en occuper.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LE PAS SUR LE MAILLAGE DU TERRAIN (2026-09-01) — 0x00541550, creneau 63 de 0x0056afc8.
    //
    // 1584 octets, ABSENTS de `Blaze.c`, trente-sept etiquettes de saut, un seul appelle :
    // `GroundHeightOnFace`. Releve garde sous `Legacy/Raw/Blaze/FUN_00541550.asm`.
    //
    // C'est ce que `SweepEntitySurroundings_00458A30` appelle six fois par image pour l'embarcation
    // de la foret, et c'est la piece qui manquait pour qu'elle avance vraiment.
    // =====================================================================

    /// <summary>
    /// Le test « le deplacement traverse-t-il cette arete ? », ecrit <b>quatre fois</b> dans le
    /// binaire avec les memes trois produits en croix et les memes trois comparaisons.
    ///
    /// <para>
    /// Le premier dit de quel cote de l'arete <c>A→B</c> se trouve la position d'arrivee : positif
    /// ou nul, on est dedans, il n'y a rien a faire. Les deux autres disent si le segment
    /// <c>precedent → maintenant</c> passe entre <c>A</c> et <c>B</c> — le test d'intersection de
    /// deux segments, dans le plan horizontal seulement. Les hauteurs ne participent pas.
    /// </para>
    /// <para>
    /// <b>Les trois comparaisons ne sont pas symetriques</b> : <c>&gt;= 0</c>, <c>&gt; 0</c> et
    /// <c>&lt; 0</c>. Un point exactement sur une arete est donc traite differemment selon
    /// l'extremite, et c'est ce qui evite qu'un sommet compte deux fois pour deux aretes voisines.
    /// </para>
    /// </summary>
    private static bool SegmentCrossesEdge(
        int xA, int zA, int xB, int zB, int xPrev, int zPrev, int xNow, int zNow)
    {
        if ((zNow - zA) * (xB - xA) - (xNow - xA) * (zB - zA) >= 0) return false;

        int dx = xNow - xPrev;
        int dz = zNow - zPrev;

        if ((zA - zPrev) * dx - (xA - xPrev) * dz > 0) return false;
        if ((zB - zPrev) * dx - (xB - xPrev) * dz < 0) return false;

        return true;
    }

    /// <summary>
    /// Laquelle des trois (ou quatre) aretes de <paramref name="face"/> le deplacement traverse.
    ///
    /// <para>
    /// Les aretes sont essayees dans l'ordre de bobinage <c>v0→v2</c>, <c>v2→v1</c>, puis
    /// <c>v1→v0</c> pour un triangle ou <c>v1→v3</c> et <c>v3→v0</c> pour un quadrilatere — le bit
    /// <c>1</c> des attributs distingue les deux, exactement comme dans
    /// <see cref="TryPlaceEntity"/>. <b>La premiere qui repond arrete la recherche.</b>
    /// </para>
    /// <para>
    /// Les indices rendus sont ceux des deux sommets de l'arete, dans l'ordre du bobinage : c'est
    /// ce couple qui sert de clef pour retrouver la face d'en face, qui porte la meme arete a
    /// l'envers.
    /// </para>
    /// </summary>
    /// <param name="isQuad">Le bit <c>1</c> tel qu'il est range dans le <b>creneau</b> de l'entite,
    /// pas tel qu'il est dans la face. Les deux disent la meme chose — le creneau est une copie des
    /// seize bits bas de <c>face[3]</c>, et rien de ce qui le retouche ne va jusqu'au bit 1 — mais
    /// c'est le creneau que le binaire lit, et c'est donc lui qu'on passe.</param>
    private static bool FindCrossedEdge(
        uint* face, byte* vertices, bool isQuad, int xPrev, int zPrev, int xNow, int zNow,
        out int from, out int to)
    {
        int i0 = (int)(face[0] & 0xffff);
        int i2 = (int)(face[0] >> 0x10);
        int i1 = (int)(face[1] & 0xffff);
        int i3 = (int)(face[1] >> 0x10);

        // Un sommet fait huit octets : x et y dans les deux moities du dword de tete, z dans le
        // short de +4. Meme convention que TryPlaceEntity et GroundHeightOnFace.
        int x0 = *(short*)(vertices + i0 * 8), z0 = *(short*)(vertices + i0 * 8 + 4);
        int x1 = *(short*)(vertices + i1 * 8), z1 = *(short*)(vertices + i1 * 8 + 4);
        int x2 = *(short*)(vertices + i2 * 8), z2 = *(short*)(vertices + i2 * 8 + 4);

        if (SegmentCrossesEdge(x0, z0, x2, z2, xPrev, zPrev, xNow, zNow))
        {
            from = i0; to = i2; return true;
        }

        if (SegmentCrossesEdge(x2, z2, x1, z1, xPrev, zPrev, xNow, zNow))
        {
            from = i2; to = i1; return true;
        }

        if (isQuad)
        {
            int x3 = *(short*)(vertices + i3 * 8), z3 = *(short*)(vertices + i3 * 8 + 4);

            if (SegmentCrossesEdge(x1, z1, x3, z3, xPrev, zPrev, xNow, zNow))
            {
                from = i1; to = i3; return true;
            }

            if (SegmentCrossesEdge(x3, z3, x0, z0, xPrev, zPrev, xNow, zNow))
            {
                from = i3; to = i0; return true;
            }
        }
        else if (SegmentCrossesEdge(x1, z1, x0, z0, xPrev, zPrev, xNow, zNow))
        {
            from = i1; to = i0; return true;
        }

        from = 0; to = 0; return false;
    }

    /// <summary>
    /// Ghidra: FUN_00541550 @ 0x00541550 — <b>le pas d'une entite sur le maillage du terrain</b>,
    /// et ce qu'elle y heurte. 1584 octets, <b>absents de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00541550.asm</c>.
    ///
    /// <para>
    /// C'est le <b>creneau 63 de la table installee <c>0x0056afc8</c></b> — voir
    /// <see cref="CallSubAreaMoveProbe"/> —, appele avec trois arguments la ou les sept tables
    /// portent partout ailleurs des fonctions a un seul. En foret 6-8 c'est
    /// <see cref="SweepEntitySurroundings_00458A30"/> qui l'appelle, six fois par image, pour
    /// l'embarcation.
    /// </para>
    ///
    /// <para>
    /// <b>Il ne deplace rien.</b> La position est deja ecrite par l'appelant ; cette fonction dit
    /// <b>sur quelles faces</b> l'entite se retrouve, <b>quel plancher et quel plafond</b> elle a,
    /// et <b>ce qui l'a arretee</b>. Elle ecrit quatre choses : les quatre creneaux de face
    /// <c>+0x80</c>..<c>+0x8c</c>, la face courante <c>+0x7c</c>, le plancher <c>+0x60</c> et le
    /// plafond <c>+0x64</c>, et le mot de contact <c>+0x90</c>.
    /// </para>
    ///
    /// <para>
    /// <b>Trois passes.</b>
    /// </para>
    /// <list type="number">
    /// <item>
    /// <b>La marche sur le maillage.</b> Pour chacun des quatre creneaux — un par <i>genre</i> de
    /// face, l'octet <c>+0x0e</c> de la face, le meme index que <see cref="TryPlaceEntity"/>
    /// emploie —, jusqu'a <b>seize</b> traversees d'arete d'affilee. A chaque tour :
    /// <see cref="FindCrossedEdge"/> dit quelle arete le deplacement franchit, puis la liste
    /// d'adjacence <b>par sommet</b> du morceau (<c>morceau + 0x30</c>, une nouveaute pour le
    /// depot) est parcourue pour trouver la face qui porte la meme arete <b>a l'envers</b>. Si
    /// elle existe et que ses attributs rencontrent <paramref name="mask"/>, l'entite passe
    /// dessus ; sinon le bit <c>0x0800</c> est pose et la marche s'arrete.
    /// </item>
    /// <item>
    /// <b>Le plancher.</b> Les quatre creneaux sont relus <b>du dernier au premier</b>. Le premier
    /// qui porte une vraie face donne <c>+0x7c</c> et <c>+0x60</c> par
    /// <see cref="GroundHeightOnFace"/>, et arrete la passe. Si ce sol est a plus de
    /// <c>0x32000</c> au-dessus de l'entite, la marche est trop haute : ou bien le creneau portait
    /// deja <c>0x2000</c> et il est efface au profit du suivant, ou bien c'est un blocage franc.
    /// </item>
    /// <item>
    /// <b>Le plafond.</b> Les creneaux restants recoivent les attributs de leur face, et le
    /// premier d'entre eux qui ne porte pas <c>0x1000</c> donne <c>+0x64</c>. Si aucun ne
    /// convient, <c>+0x64</c> vaut <c>0x80000000</c> et le contact recoit <c>0x20001000</c>.
    /// </item>
    /// </list>
    ///
    /// <para>
    /// <b>Le masque est ce qui rend l'embarcation flottante.</b> Une face n'est traversable que si
    /// <c>attributs &amp; masque</c> est non nul, et l'embarcation passe <c>0x2000</c> : elle ne
    /// peut aller que sur les faces marquees <c>0x2000</c> — les memes que
    /// <see cref="TickPartyRaft_00458200"/> teste sur chaque membre pour savoir qui est a bord.
    /// Les deux bouts du mecanisme se recoupent sur ce bit-la.
    /// </para>
    /// </summary>
    /// <param name="entity">L'entite, deja deplacee.</param>
    /// <param name="previous">Sa position d'avant le pas, en 20.12 ; x et z seulement.</param>
    /// <param name="mask">Les bits d'attribut qu'une face doit porter pour etre traversable.</param>
    private static void StepEntityAcrossTerrain_00541550(byte* entity, int* previous, int mask)
    {
        byte* chunk = *(byte**)Mem.Entity(0x005f7bc0);
        uint* faces = *(uint**)(chunk + 0x28);
        byte* vertices = *(byte**)Mem.Entity(0x005f7cc0);
        short** vertexFaces = *(short***)(chunk + 0x30);

        int xPrev = previous[0] >> 0xc;
        int zPrev = previous[2] >> 0xc;
        int xNow = *(int*)(entity + 0x1c) >> 0xc;
        int zNow = *(int*)(entity + 0x24) >> 0xc;

        // --- 1. La marche sur le maillage, creneau par creneau ---------------------------------
        for (int kind = 0; kind < 4; kind++)
        {
            byte* slot = entity + 0x80 + kind * 4;
            int transitions = 0x10;

            while (true)
            {
                uint before = *(uint*)slot;

                // Le bit 0x8000 des attributs : « ce genre de face n'est pas sous moi ».
                if ((before & 0x80000000) != 0) break;

                uint* face = faces + (before & 0xffff) * 4;

                if (!FindCrossedEdge(face, vertices, (before & 0x10000) != 0,
                                     xPrev, zPrev, xNow, zNow, out int from, out int to))
                {
                    break;                                // Ghidra: LAB_00541a15
                }

                // Ghidra: LAB_005418b6 — la face d'en face porte la meme arete a l'envers.
                uint edgeKey = (uint)((from << 16) | to);

                for (short* cursor = vertexFaces[from]; *cursor >= 0; cursor++)
                {
                    uint* other = faces + *cursor * 4;
                    if (other == face) continue;

                    uint o0 = other[0];
                    uint o1 = other[1];
                    uint attributes = other[3];

                    bool shares = edgeKey == o0
                               || edgeKey == ((o1 << 16) | (o0 >> 16));

                    if (!shares)
                    {
                        shares = (attributes & 1) == 0
                            ? edgeKey == ((o0 << 16) | (o1 & 0xffff))
                            : edgeKey == o1 || edgeKey == ((o0 << 16) | (o1 >> 16));
                    }

                    if (!shares) continue;

                    // Ghidra: LAB_00541969 — la face voisine est-elle traversable ?
                    if (mask != 0 && (mask & (attributes & 0xffff)) == 0)
                    {
                        slot[3] |= 8;                     // le bit 0x0800 : bloque
                        break;
                    }

                    // Le genre de la face d'arrivee decide DANS QUEL creneau elle est ecrite —
                    // pas forcement celui qu'on est en train de parcourir.
                    byte* destination = entity + 0x80 + ((attributes >> 0x10) & 0xff) * 4;

                    if (destination == slot)
                    {
                        attributes |= *(ushort*)(destination + 2) & 0x2800u;
                    }

                    // Ghidra: LAB_005419a5 — deux bits survivent a la transition.
                    attributes |= slot[2] & 2u;
                    if (((byte*)other)[0x0f] != 0) attributes |= 2;

                    *(uint*)destination = ((attributes & 0xffff) << 0x10) | (uint)(ushort)*cursor;
                }

                // Ghidra: LAB_005419eb
                if (*(uint*)slot == before)
                {
                    *(ushort*)(slot + 2) = 0x8000;        // sortie par une arete sans voisin
                    break;
                }

                if ((slot[3] & 8) != 0) break;
                if (--transitions == 0) break;
            }

            // Seize traversees sans se poser : le bit 0x0008.
            if (transitions == 0) slot[2] |= 8;
        }

        // --- 2. Le plancher, du dernier creneau au premier -------------------------------------
        uint contact = 0;
        uint carried = 0;
        int remaining = 4;
        byte* walk = entity + 0x90;

        while (true)
        {
            walk -= 4;
            uint record = *(uint*)walk;
            contact = record >> 0x10;

            if ((contact & 0x8000) != 0)
            {
                // Pas de face de ce genre : le creneau herite du drapeau que la marche trop haute
                // a laisse derriere elle.
                *(ushort*)(walk + 2) |= (ushort)carried;
                contact |= carried;
            }
            else
            {
                uint* face = faces + (record & 0xffff) * 4;
                *(uint**)(entity + 0x7c) = face;

                int ground = GroundHeightOnFace(face, (int*)(entity + 0x1c));
                *(int*)(entity + 0x60) = ground;

                if (ground - 0x32000 <= *(int*)(entity + 0x20))
                {
                    // Ghidra: LAB_00541ab8 — le sol est atteignable, la passe s'arrete ici.
                    if ((((byte*)*(uint**)(entity + 0x7c))[0x0d] & 0x20) == 0) contact &= 0xdfff;
                    break;
                }

                if ((contact & 0x2000) == 0)
                {
                    // Ghidra: LAB_00541aaa — marche infranchissable, blocage franc.
                    contact |= 0x800;
                    *(int*)(entity + 0x60) = *(int*)(entity + 0x20);
                    break;
                }

                // Le creneau portait 0x2000 : on l'efface et on essaie le suivant, en emportant
                // le drapeau.
                walk[3] |= 0x80;
                contact |= 0x8000;
                carried = 0x2000;
            }

            if (--remaining == 0) break;
        }

        // Ghidra: LAB_00541ac7 — la seule lecture de la fiche de terrain de la sous-zone.
        if ((contact & 0x8000) != 0
            && (contact & 0x2000) == 0
            && (**(byte**)Mem.Entity(0x005eca80) & 0x20) != 0)
        {
            contact = 0x800;
        }

        *(uint*)(entity + 0x90) = contact & 0xffff;

        // --- 3. Le plafond, sur les creneaux que la passe du plancher n'a pas consommes ---------
        bool ceilingMissing = true;

        for (int left = remaining; left != 0; left--, walk -= 4)
        {
            uint record = *(uint*)walk;
            if ((record & 0x80000000) != 0) continue;

            uint* face = faces + (record & 0xffff) * 4;
            bool wanted = ceilingMissing;

            *(ushort*)(walk + 2) = (ushort)face[3];

            if (!wanted || (face[3] & 0x1000) != 0) continue;

            *(int*)(entity + 0x64) = GroundHeightOnFace(face, (int*)(entity + 0x1c));
            ceilingMissing = false;
        }

        // Ghidra: LAB_00541b43
        uint settled = *(uint*)(entity + 0x90);

        if (ceilingMissing)
        {
            *(uint*)(entity + 0x90) = settled | 0x20001000;
            *(int*)(entity + 0x64) = unchecked((int)0x80000000);
        }
        else
        {
            *(uint*)(entity + 0x90) = settled & 0xffffefff;   // Ghidra: `and ah,0EFh`
        }
    }
}
