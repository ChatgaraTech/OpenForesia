// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES POSTURES ET L'ENTREE DIRECTIONNELLE.
//
// `DispatchInputHandler` aiguille l'entree, `HandleDirectionalInput_0050E140` la traduit en
// deplacement, `DispatchPostureHandler` choisit la posture, et les quatre `PostureHandler_0051A6xx`
// sont les postures elles-memes — dont trois etaient absentes de `Blaze.c` et ont ete sorties du
// desassembleur.
//
// Ces quatre corps sont ce que le bandeau « LES TROIS DERNIERES POSTURES » annoncait dans le
// fichier d'origine. Il couvrait en realite 2 379 lignes dont les postures n'occupaient que 86 :
// le reste — mouvement, contact, ombre, volumes de declenchement, montee de niveau — est
// aujourd'hui dans `Physics/` et `Combat/`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_FUN_00562ce0)[bloc + 0x2b7])(…)</c> — quatre gestionnaires de
    /// commande, releves dans le binaire sous <see cref="s_InputHandlers_00562ce0"/>. Aucun n'est
    /// le corps vide.
    /// </summary>
    private static void DispatchInputHandler(byte index, uint* slot, byte* perChar, byte* pad)
    {
        uint target = index < s_InputHandlers_00562ce0.Length
            ? s_InputHandlers_00562ce0[index]
            : 0;

        switch (target)
        {
            case 0x0050E140: HandleDirectionalInput_0050E140(slot, perChar, pad); return;

            default:
                throw new NotImplementedException(
                    $"FUN_0050bf20 : le gestionnaire de commande d'indice {index} (table " +
                    $"0x00562ce0) vaut 0x{target:X6} et n'est pas transcrit. Les quatre entrees de " +
                    "la table sont relevees dans le binaire : 0x0050E140, 0x0050DE90 (toutes deux " +
                    "dans Blaze.c), 0x0050DD00 et 0x0050DFF0 (absentes, a desassembler). Il prend " +
                    "(entite, bloc par personnage, manette).");
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e140 @ 0x0050e140 — l'entree <b>0</b> de la table de commande
    /// <c>PTR_FUN_00562ce0</c> : <b>la marche a la croix directionnelle</b>. C'est le premier
    /// gestionnaire que la carte du monde atteint, et le seul chemin par lequel le joueur fait
    /// bouger son personnage.
    ///
    /// <para>
    /// Trois façons d'obtenir une direction, selon le bit 3 de <c>bloc + 0x140</c> : normalement
    /// la croix, lue a travers <see cref="s_PadDirection_00562dc8"/> ; quand le bit est pose — le
    /// personnage est desoriente — <b>une direction tiree au sort</b> des qu'une touche est
    /// enfoncee, et rien du tout sinon.
    /// </para>
    /// <para>
    /// La direction n'est convertie en cap qu'a la <b>transition</b> de la croix (bit 1 de
    /// <c>manette + 0xb</c>, consomme au passage), pas a chaque image : c'est ce qui empeche le
    /// personnage de tourner en continu tant qu'une direction est tenue. Le cap est relatif a la
    /// camera, arrondi au seizieme de tour (<c>(angle + 0x80) &amp; 0xf00</c>) — et la meme
    /// desorientation remplace cette reference par un tirage.
    /// </para>
    /// <para>
    /// La vitesse vient de <see cref="s_PostureSpeed_00562cc0"/>, sauf en mode de mise au point
    /// avec le modificateur tenu, ou elle passe a <c>0x3c000</c> — la marche rapide. Elle est
    /// ensuite mise a l'echelle par les memes trois drapeaux de <c>bloc + 0x140</c> que le membre
    /// suivi utilise pour sa distance d'arret.
    /// </para>
    /// </summary>
    private static void HandleDirectionalInput_0050E140(uint* slot, byte* perChar, byte* pad)
    {
        byte* s = (byte*)slot;
        int direction;

        if ((perChar[0x140] & 8) == 0)
        {
            direction = s_PadDirection_00562dc8[*pad >> 4];
        }
        else if ((*pad & 0xf0) == 0)
        {
            direction = 0;
        }
        else
        {
            direction = (CrtRand() & 7) + 1;
        }

        if (direction == 0)
        {
            if ((*slot & 0x47) == 0 && (*pad & 0xf0) == 0 && *(short*)(s + 0x18) != 0)
            {
                *(uint*)(s + 0x18) = 0;
                *slot |= 0x1000000;
            }

            return;
        }

        if ((*slot & 0x8000047) == 0 && *(short*)(s + 0x18) != 1)
        {
            *(uint*)(s + 0x18) = 1;
            *slot |= 0x1000000;
        }

        if ((pad[0x0b] & 2) != 0)
        {
            pad[0x0b] &= 0xfd;

            short reference = (perChar[0x140] & 8) == 0
                ? (short)((*(ushort*)(Mem.s_CameraVector_00604928 + 2) + 0x80u) & 0xf00)
                : (short)(((short)CrtRand() + 0x80u) & 0xf00);

            *(short*)(s + 0x32) = (short)((short)(direction * 0x200) - reference - 0x200);
        }

        *(short*)(s + 0x98) = *(short*)(s + 0x32);

        if (Mem.s_DebugModeLatch_005633a8 == 0)
        {
            *(int*)(s + 0x5c) = (pad[1] & 0x20) == 0
                ? s_PostureSpeed_00562cc0[perChar[0x150] & 7] << 0xc
                : 0x3c000;
        }
        else
        {
            *(int*)(s + 0x5c) = s_PostureSpeed_00562cc0[perChar[0x150] & 7] << 0xc;
        }

        uint posture = *(uint*)(perChar + 0x140);
        int scale = 0x1000;
        if ((posture & 0x4000) != 0) scale = 0x1333;
        if ((posture & 0x8000) != 0) scale = 0xccc;
        if ((posture & 4) != 0) scale = 0x800;

        *(int*)(s + 0x5c) = scale * *(int*)(s + 0x5c) >> 0xc;
    }

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_FUN_00562ca0)[bloc + 0x150 &amp; 7])(…)</c> — huit gestionnaires
    /// de posture, releves sous <see cref="s_PostureHandlers_00562ca0"/>. <b>Quatre sont le corps
    /// vide <c>0x0040A600</c></b>, deja verifie au desassembleur : les postures 1, 2, 3 et 5 ne
    /// font rien.
    ///
    /// <para>
    /// <b>Les quatre autres sont maintenant ecrites</b> (2026-09-01). Le site d'appel empile cinq
    /// arguments — entite, entree d'emplacement, bloc par personnage, manette, animation courante —
    /// mais le desassemblage montre que trois des quatre corps ne lisent que le premier ; seule la
    /// posture 4 va chercher le troisieme. L'appel est en cdecl, donc les arguments non lus sont
    /// sans consequence.
    /// </para>
    /// </summary>
    private static void DispatchPostureHandler(
        byte posture, uint* slot, byte* entry, byte* perChar, byte* pad, byte* anim)
    {
        uint target = s_PostureHandlers_00562ca0[posture];

        switch (target)
        {
            case 0x0040A600: return;   // corps vide verifie au desassembleur
            case 0x0051A670: PostureHandler_0051A670(slot); return;
            case 0x0051A6B0: PostureHandler_0051A6B0(slot, perChar); return;
            case 0x0051A700: PostureHandler_0051A700(slot); return;
            case 0x0051A710: PostureHandler_0051A710(slot); return;

            default:
                throw new NotImplementedException(
                    $"FUN_0050bf20 : le gestionnaire de posture {posture} (table 0x00562ca0) vaut " +
                    $"0x{target:X6}, qui n'est aucune des cinq adresses relevees dans la table. " +
                    "La table est fausse, ou l'indice deborde.");
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051a670 @ 0x0051a670 — la posture <b>0</b>, la marche : quatorze lignes.
    ///
    /// <para>
    /// Elle ne fait qu'une chose : a la <b>cinquieme image de l'etat 6</b>, elle fait naitre
    /// l'entite d'effet <c>0xba</c> — la trainee de l'arme, dont la table 0x005ff400 porte la
    /// duree de vie. Le test <c>param_1[6] == 0x50006</c> compare l'etat ET l'image en un seul
    /// dword, ce qui est la forme compacte de « etat 6, image 5 » : les deux <c>short</c> de
    /// <c>+0x18</c> et <c>+0x1a</c> lus ensemble.
    /// </para>
    /// <para>
    /// <b>Ghidra ne lui donne qu'un parametre</b> alors que le site d'appel en empile cinq. Ce
    /// n'est pas une signature manquee : le corps ne touche jamais aux quatre autres, et l'appel
    /// est en cdecl, donc sans consequence. Meme precedent que <c>FreezeAllEntities</c>.
    /// </para>
    /// </summary>
    private static void PostureHandler_0051A670(uint* slot)
    {
        byte* s = (byte*)slot;

        if ((*slot & 2) != 0 && *(uint*)(s + 0x18) == 0x50006)
        {
            SpawnEffectEntity(s, 0xba, 0, 0);
        }

        if ((*slot & 1) != 0) *slot &= 0xfffffffb;
    }

    // =====================================================================
    // LES TROIS DERNIERES POSTURES — 4, 6 et 7, absentes de `Blaze.c`, sorties du desassembleur
    // le 2026-09-01. Elles tiennent en 208 octets a elles trois.
    //
    // LES QUATRE GESTIONNAIRES DE POSTURE SONT LE MEME GESTE. Tous comparent le dword `+0x18` de
    // l'entite a une valeur exacte, et agissent a cette seule image. Ce dword est la paire
    // (etat d'animation, image) que `PostureHandler_0051A670` decrit : le `short` bas est l'etat,
    // le haut l'image. Les trois nouvelles visent toutes **l'etat 3**, celui du deplacement, a une
    // image precise du cycle — le moment ou le pied touche, ou l'aile bat.
    //
    // Et deux d'entre elles ne font pas naitre une entite d'effet : elles **lancent un sort**, par
    // le meme `CastSpell_0053A980` qu'une incantation. Les familles 5 et 6, indice 0 — deux des
    // quatre familles hors ecole, celles que `SortsAtteignables.md` compte a part.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051a6b0 @ 0x0051a6b0 — la posture <b>4</b>, 77 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// A une image precise de l'etat d'animation 3, elle leve le bit <c>0x800</c> de l'entite et
    /// lance la fiche de sort <b>famille 5, indice 0</b> (sous-type <c>0x49</c>). L'image visee
    /// depend du bit <c>8</c> de <c>+0x150</c> du bloc par personnage — <b>l'image 0x10 quand il
    /// est eteint, l'image 0x0c quand il est allume</b> : deux cycles de longueurs differentes pour
    /// la meme posture, et le meme effet pose au bon moment dans chacun.
    /// </para>
    /// <para>
    /// La cascade du binaire est ecrite « en losange » — le premier test retombe dans le second
    /// quand l'image est la bonne mais le bit aussi —, ce qui revient exactement aux deux
    /// conjonctions ci-dessous.
    /// </para>
    /// </summary>
    private static void PostureHandler_0051A6B0(uint* slot, byte* perChar)
    {
        if ((*slot & 2) == 0) return;

        uint frame = *(uint*)((byte*)slot + 0x18);
        bool alternate = (perChar[0x150] & 8) != 0;

        if ((frame == 0x100003 && !alternate) || (frame == 0x000c0003 && alternate))
        {
            // TODO(REVERSE): le bit 0x800 du mot d'etat de l'entite — pose ici et par la posture 7,
            // aucun lecteur identifie dans le transcrit. Voir REVERSE_STATUS.md, 2026-09-01.
            *slot |= 0x800;
            CastSpell_0053A980(slot, 5, 0, -1, -1);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051a700 @ 0x0051a700 — la posture <b>6</b>, <b>16 octets</b>, la plus courte du
    /// depot apres les corps vides, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle est la <b>queue de <see cref="PostureHandler_0051A670"/></b>, seule : le compilateur a
    /// emis deux fois la meme fin. Cette posture ne fait donc que retomber le bit <c>0x04</c> quand
    /// le bit <c>0x01</c> est leve, sans rien engendrer.
    /// </para>
    /// </summary>
    private static void PostureHandler_0051A700(uint* slot)
    {
        if ((*slot & 1) != 0) *slot &= 0xfffffffb;
    }

    /// <summary>
    /// Ghidra: FUN_0051a710 @ 0x0051a710 — la posture <b>7</b>, 42 octets, <b>absente de
    /// <c>Blaze.c</c></b>. <b>C'est le vol</b> : la posture du Lutin, et celle qui plantait des
    /// l'entree en foret.
    ///
    /// <para>
    /// A l'image 3 de l'etat d'animation 3 — le battement d'aile —, elle leve le bit <c>0x800</c>
    /// et lance la fiche de sort <b>famille 6, indice 0</b> (sous-type <c>0x4a</c>, le gestionnaire
    /// <c>0x004769a0</c>). Rien d'autre : pas de queue en <c>0x04</c> comme les postures 0 et 6.
    /// </para>
    /// </summary>
    private static void PostureHandler_0051A710(uint* slot)
    {
        if ((*slot & 2) != 0 && *(uint*)((byte*)slot + 0x18) == 0x30003)
        {
            *slot |= 0x800;
            CastSpell_0053A980(slot, 6, 0, -1, -1);
        }
    }
}
