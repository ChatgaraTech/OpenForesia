// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DE LA VIEILLE TOUR — zone 4, table 0x0056adc8.
//
// Ouvert le 2026-09-17 sur le patron de `Mine.cs` et de `FireMountain.cs`. La tour installe trois
// blocs dans cette table, un par groupe de sous-zones :
//
//   0x00556728  sous-zones 0, 1, 2, 4
//   0x00556da8  sous-zones 5, 6, 8, 10
//   0x00557428  sous-zones 3, 7, 9
//
// Les creneaux 0 a 7 sont les decors communs de `Forest.cs` ; le creneau 20 est 0x00450BD0
// (Forest.cs), recopie trois fois. Ce qui est propre a la tour est ici, par ordre de creneau dans le
// bloc 0x00556da8, avec les jumeaux des deux autres blocs quand le compilateur en a emis.
//
// Le son d'une zone se lit par `ZoneSound` (`Mine.cs`), le drapeau de script par `ScriptFlagSet`
// (`Effects/ForestParticlesA.cs`) : meme classe partielle.
// Le tri de la zone par complexite est dans `Documentation/Etat/Zone4ParComplexite.md`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // CRENEAU 11 — L'OBJET QUI RACLE QUAND ON LE POUSSE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045c7c0 @ 0x0045c7c0 — creneau <b>11</b> du bloc <c>0x00556da8</c>. Decompilee
    /// dans <c>Blaze.c</c>, 51 instructions.
    ///
    /// <para>
    /// <b>Un objet que le groupe peut pousser.</b> Chaque image il se deplace par
    /// <see cref="MoveSceneEntity_00455B30"/> (masque <c>0x800</c>), et <b>disparait</b> (bit
    /// <c>0x80000000</c> retire) si le deplacement rend 1. Tant que l'une des deux vitesses
    /// horizontales depasse <c>0x2000</c> en valeur absolue, il <b>racle</b> : voix <c>0x16</c> de la
    /// zone, puis vingt images de silence avant le raclement suivant (<c>+0x98</c>). La vitesse est
    /// remise a zero a la fin de chaque image : il ne bouge que tant qu'on le pousse.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045C7C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x98) = 0;

        // Ghidra: `cmp ax,1` — seul le mot bas du retour compte.
        if ((short)MoveSceneEntity_00455B30(e, 0x800) == 1) *(uint*)e &= 0x7fffffff;

        int vx = *(int*)(e + 0x50);
        if (vx < 0) vx = -vx;
        int vz = *(int*)(e + 0x58);
        if (vz < 0) vz = -vz;

        if ((vx > 0x2000 || vz > 0x2000) && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x16), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0x14;
        }

        // Ghidra: LAB_0045c849
        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    // =====================================================================
    // CRENEAU 23 — LE DECOR QUI RESPIRE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045cee0 @ 0x0045cee0 — creneau <b>23</b> des trois blocs de la tour : cette
    /// adresse pour 0x00556da8, <c>0x0045B750</c> et <c>0x0045F320</c> pour les deux autres, au
    /// desassemblage STRICT identique. Decompilee dans <c>Blaze.c</c>, 40 instructions.
    ///
    /// <para>
    /// <b>Il pulse.</b> Sa phase <c>+0x12</c> avance de <c>0x100</c> par image — un battement en
    /// seize images —, et ses echelles suivent le sinus : <c>+0x2a</c> d'un seizieme, <c>+0x28</c> et
    /// <c>+0x2c</c> d'un trente-deuxieme autour de <c>0x1000</c>. Sa vitesse est remise a zero en
    /// tete de chaque image. Si le placement a mis <c>+0x14</c> a 1, il fait aussi naitre a chaque
    /// image un effet <c>0x35</c> (<c>0xf5</c>), a moins de 1800 unites de la camera.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045CEE0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);          // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 0x100;

            int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451
            *(short*)(e + 0x2a) = (short)((sin >> 4) + 0x1000);

            sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
            short scale = (short)((sin >> 5) + 0x1000);
            *(short*)(e + 0x2c) = scale;
            *(short*)(e + 0x28) = scale;

            if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x14) == 1) SpawnEffectNearCamera_00468110(e, 0x35, 0, -1);
    }

    // =====================================================================
    // CRENEAUX 31 A 38 — LES DECORS QUI TOURNENT
    //
    // Sept corps de la meme famille que 0x00456270 (FireMountain.cs) : une phase +0x12 qui avance
    // d'un pas fixe et revient sous 0x1000, et une position recalculee a chaque image autour du point
    // de naissance — sinus sur un axe, cosinus sur l'autre, produit non recadre. Ils ne different
    // que par les axes, les rayons, le pas et la phase de depart. Ecrits un par un, parce que
    // chacun a sa propre combinaison et que les factoriser cacherait laquelle.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045d1b0 @ 0x0045d1b0 — creneau <b>31</b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Un cercle horizontal de rayon 100</b>, pas <c>0x20</c>, phase de depart tiree sur 4000.
    /// Contrairement a son cousin de la montagne, <b>il ne teste pas son etat apres la naissance</b> :
    /// il tourne a chaque image, quel que soit <c>+0x10</c>.
    /// </summary>
    private static void EntityHandler_0045D1B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4000);                 // Ghidra: FUN_00547140
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));       // Ghidra: Ordinal_451
        *(int*)(e + 0x1c) = *(short*)(e + 0x14) * 0x1000 + sin * 100;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));            // Ghidra: Ordinal_450
        *(short*)(e + 0x12) += 0x20;
        *(int*)(e + 0x24) = *(short*)(e + 0x16) * 0x1000 + cos * 100;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_0045d240 @ 0x0045d240 — creneau <b>32</b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Un va-et-vient sur X seulement</b>, amplitude 100, pas <c>0x30</c>, phase de depart tiree
    /// sur 4000.
    /// </summary>
    private static void EntityHandler_0045D240(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4000);
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x30;
        *(int*)(e + 0x1c) = *(short*)(e + 0x14) * 0x1000 + sin * 100;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_0045d2b0 @ 0x0045d2b0 — creneau <b>33</b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Un cercle vertical</b> : sinus sur Z (rayon 300) et cosinus sur la hauteur (rayon 200),
    /// pas <c>0x20</c>. <c>+0x14</c> garde la base en Z, <c>+0x16</c> la base en hauteur.
    /// </summary>
    private static void EntityHandler_0045D2B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4000);
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x24) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x24) = *(short*)(e + 0x14) * 0x1000 + sin * 300;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x20;
        *(int*)(e + 0x20) = cos * 200 + *(short*)(e + 0x16) * 0x1000;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_0045d400 @ 0x0045d400 — creneau <b>36</b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Deux mouvements superposes</b> : un cercle horizontal de rayon <c>0xaa</c> (pas
    /// <c>0x20</c>) autour du point de naissance range en <c>+0x44</c>/<c>+0x4c</c>, et une seconde
    /// oscillation sur Z d'amplitude 200 et de pas <c>0x30</c>. Les deux phases partent de 0, ou de
    /// <c>0x800</c> — un demi-tour — si le cap de placement <c>+0x32</c> n'est pas nul.
    /// </summary>
    private static void EntityHandler_0045D400(byte* e)
    {
        short start = 0;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            if (*(short*)(e + 0x32) != 0) start = 0x800;

            *(short*)(e + 0x12) = start;
            *(short*)(e + 0x14) = start;
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + sin * 0xaa;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + cos * 0xaa;

        int sway = Winsys3d.GetCosTableEntry(*(short*)(e + 0x14));
        *(short*)(e + 0x12) += 0x20;
        *(int*)(e + 0x24) += sway * 200;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;

        *(short*)(e + 0x14) += 0x30;
        if (*(short*)(e + 0x14) > 0x1000) *(short*)(e + 0x14) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_0045d4c0 @ 0x0045d4c0 — creneau <b>37</b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Une ellipse horizontale</b> : rayon <c>0xfa</c> sur X, 100 sur Z, pas <c>0x20</c>, phase de
    /// depart <b>0</b> — tous les exemplaires battent ensemble.
    /// </summary>
    private static void EntityHandler_0045D4C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x1c) = *(short*)(e + 0x14) * 0x1000 + sin * 0xfa;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x20;
        *(int*)(e + 0x24) = *(short*)(e + 0x16) * 0x1000 + cos * 100;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_0045d550 @ 0x0045d550 — creneau <b>38</b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Un cercle horizontal de rayon <c>0x118</c></b>, pas <c>0x20</c>, parti a <c>0x800</c> : le
    /// demi-tour oppose de <see cref="EntityHandler_0045D4C0"/>.
    /// </summary>
    private static void EntityHandler_0045D550(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x800;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x1c) = *(short*)(e + 0x14) * 0x1000 + sin * 0x118;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x20;
        *(int*)(e + 0x24) = *(short*)(e + 0x16) * 0x1000 + cos * 0x118;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    // =====================================================================
    // CRENEAUX 34, 35, 39 — CE QUI MONTE ET DESCEND
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0045d340</c> — creneau <b>34</b>. <b>Absente de <c>Blaze.c</c></b> ; 27
    /// instructions.
    ///
    /// <para>
    /// <b>Un tour sur lui-meme, puis il s'endort.</b> Tant que le mot d'etat ne porte pas
    /// <c>0x40000000</c> : trois images d'attente (<c>+0x14</c>), puis le tangage <c>+0x34</c> gagne
    /// <c>0x90</c> par image ; au-dela de <c>0x1000</c> il est remis a zero, l'etat aussi, et le bit
    /// <c>0x40000000</c> est pose. Quelqu'un d'autre doit le retirer pour qu'il recommence.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045D340(byte* e)
    {
        uint state = *(uint*)e;
        if ((state & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
        }

        // Ghidra: LAB_0045d365
        short wait = *(short*)(e + 0x14);
        if (wait < 3)
        {
            *(short*)(e + 0x14) = (short)(wait + 1);
            return;
        }

        // Ghidra: LAB_0045d376
        *(short*)(e + 0x34) += 0x90;
        if (*(short*)(e + 0x34) <= 0x1000) return;

        *(uint*)e = state | 0x40000000;
        *(short*)(e + 0x34) = 0;
        *(short*)(e + 0x10) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0045d3a0</c> — creneau <b>35</b>. <b>Absente de <c>Blaze.c</c></b> ; 25
    /// instructions.
    ///
    /// <para>
    /// <b>Une plate-forme qui s'enfonce sous le poids.</b> Elle descend de <c>0xa000</c> par image
    /// jusqu'a la hauteur <c>0xc8000</c> tant que le mot d'etat porte <c>0x10</c> alors qu'elle est
    /// en haut (<c>+0x10 == 0</c>), ou <c>0x20</c> alors qu'elle est deja en train de descendre
    /// (<c>+0x10 == 1</c>). Sinon elle remonte du meme pas jusqu'a <c>0x190000</c>. Ce que les bits
    /// <c>0x10</c> et <c>0x20</c> signalent n'est pas etabli ici ; le comportement se lit comme un
    /// contact.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045D3A0(byte* e)
    {
        uint flags = *(uint*)e;
        short phase = *(short*)(e + 0x10);

        bool sink = ((flags & 0x10) != 0 && phase == 0) || ((flags & 0x20) != 0 && phase == 1);

        int height = *(int*)(e + 0x20);

        if (sink)
        {
            // Ghidra: LAB_0045d3c2
            if (height <= 0xc8000) return;

            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) = height - 0xa000;
            return;
        }

        // Ghidra: LAB_0045d3db
        if (height >= 0x190000) return;

        *(short*)(e + 0x10) = 0;
        *(int*)(e + 0x20) = height + 0xa000;
    }

    /// <summary>
    /// Ghidra: <c>0x0045d5e0</c> — creneau <b>39</b>. <b>Absente de <c>Blaze.c</c></b> ; 78
    /// instructions.
    ///
    /// <para>
    /// <b>Un objet qui tombe, disparait, et revient d'en haut.</b> Il reste immobile tant que le mot
    /// d'etat ne porte pas <c>0x20</c> ; une fois parti, trois temps dans <c>+0x14</c> :
    /// </para>
    /// <list type="table">
    ///   <item><term>0, la chute</term><description>une vitesse <c>+0x12</c> qui gagne un par image,
    ///     retranchee de la hauteur. Sous <c>-0x12c000</c>, il tourne de <c>0x40</c> et retrecit de
    ///     200 ; quand l'echelle passe sous zero, il est replace a la hauteur <c>0x384000</c>, echelle
    ///     nulle, et passe au temps 1.</description></item>
    ///   <item><term>1, la pause</term><description>vingt et une images.</description></item>
    ///   <item><term>2, le retour</term><description>il regrossit de 200 par image jusqu'a
    ///     <c>0x1000</c> en tournant en sens inverse, et redescend de <c>0xa000</c> par image jusqu'a
    ///     <c>0x190000</c>. La, il retire <c>0x2000</c>, pose <c>0x40000000</c> et remet son etat a
    ///     zero.</description></item>
    /// </list>
    /// </summary>
    private static void EntityHandler_0045D5E0(byte* e)
    {
        uint flags = *(uint*)e;

        if ((flags & 0x20) == 0 && *(short*)(e + 0x10) == 0) return;

        // Ghidra: LAB_0045d5fa
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
        }

        // Ghidra: LAB_0045d611 — la chute.
        if (*(short*)(e + 0x14) == 0)
        {
            (*(short*)(e + 0x12))++;
            int height = *(int*)(e + 0x20) - (*(short*)(e + 0x12) << 0xc);
            *(int*)(e + 0x20) = height;

            if (height < -0x12c000)
            {
                *(short*)(e + 0x32) += 0x40;
                *(short*)(e + 0x28) -= 200;
                *(short*)(e + 0x2c) -= 200;

                if (*(short*)(e + 0x2c) < 0)
                {
                    *(short*)(e + 0x28) = 0;
                    *(short*)(e + 0x2c) = 0;
                    *(int*)(e + 0x20) = 0x384000;
                    *(short*)(e + 0x12) = 0;
                    *(short*)(e + 0x14) = 1;
                }
            }
        }

        // Ghidra: LAB_0045d666 — la pause.
        if (*(short*)(e + 0x14) == 1)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x14)
            {
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = 2;
            }
        }

        // Ghidra: LAB_0045d684 — le retour.
        if (*(short*)(e + 0x14) != 2) return;

        short scale = *(short*)(e + 0x28);
        if (scale < 0x1000)
        {
            *(short*)(e + 0x2c) += 200;
            *(short*)(e + 0x32) -= 0x40;
            *(short*)(e + 0x28) = (short)(scale + 200);
        }
        else
        {
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2c) = 0x1000;
        }

        // Ghidra: LAB_0045d6b7
        int y = *(int*)(e + 0x20);
        if (y > 0x190000)
        {
            *(int*)(e + 0x20) = y - 0xa000;
            return;
        }

        // Ghidra: LAB_0045d6cf — `and dh,0DFh` sur le mot d'etat lu en tete.
        *(int*)(e + 0x20) = 0x190000;
        *(short*)(e + 0x10) = 0;
        *(uint*)e = (flags & 0xffffdfff) | 0x40000000;
    }

    // =====================================================================
    // CRENEAU 40 — LE BALANCIER
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045d6f0 @ 0x0045d6f0 — creneau <b>40</b>. Decompilee dans <c>Blaze.c</c>, 25
    /// instructions.
    ///
    /// <para>
    /// <b>Il oscille autour de son cap.</b> A la naissance, le cap de placement <c>+0x32</c> est mis
    /// de cote dans <c>+0x10</c> (1 s'il etait nul — l'etat ne peut pas valoir zero), et la phase
    /// <c>+0x16</c> part de <c>+0x14</c>. Ensuite <c>+0x32 = cap + sin(phase) / 8</c>, soit
    /// ±<c>0x200</c>, et la phase avance de <c>0x40</c> par image. L'addition se fait sur seize bits
    /// (<c>add ax,[esi+10h]</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_0045D6F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            short heading = *(short*)(e + 0x32);
            *(short*)(e + 0x10) = heading;
            if (heading == 0) *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x16) = *(short*)(e + 0x14);
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x16));       // Ghidra: 0x00545a1c
        *(short*)(e + 0x16) += 0x40;
        *(short*)(e + 0x32) = (short)((sin >> 3) + *(short*)(e + 0x10));

        if (*(short*)(e + 0x16) > 0x1000) *(short*)(e + 0x16) -= 0x1000;
    }

    // =====================================================================
    // CRENEAUX 42 ET 43 — LES DEUX CHUTES QUI FONT TREMBLER
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045db10 @ 0x0045db10 — creneau <b>42</b> des trois blocs : cette adresse,
    /// <c>0x0045C120</c> et <c>0x0045F5F0</c>, au desassemblage STRICT identique. Decompilee dans
    /// <c>Blaze.c</c>, 61 instructions.
    ///
    /// <para>
    /// <b>Il ne part pas seul</b> : l'etat 0 remet seulement <c>+0x12</c> a zero, et c'est un autre
    /// qui pose l'etat 1. Alors, pendant 64 images, le tangage <c>+0x34</c> perd <c>0x10</c> par
    /// image ; a la 64e, voix <c>0x20</c> de la zone, et <b>la camera tremble</b> sept images
    /// (<see cref="TryPublishCameraEffectNearEntity"/>, second mot a ±7, ±6… en alternant le signe
    /// selon la parite) avant d'etre remise a zero. L'entite se rendort : <c>0x50000000</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045DB10(byte* e)
    {
        short phase = *(short*)(e + 0x10);
        if (phase == 0) *(short*)(e + 0x12) = 0;

        // Ghidra: `cmp ax,1` sur l'etat lu en tete.
        if (phase == 1)
        {
            *(short*)(e + 0x34) -= 0x10;
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 0x40)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 7;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            }
        }

        // Ghidra: LAB_0045db6e
        if (*(short*)(e + 0x10) != 2) return;

        short count = *(short*)(e + 0x12);
        int jolt = (count & 1) == 0 ? count : -count;
        TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);          // Ghidra: FUN_0041be60

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) != 0) return;

        *(short*)(e + 0x10) = 3;
        TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);
        *(uint*)e |= 0x50000000;
    }

    /// <summary>
    /// Ghidra: FUN_0045dbe0 @ 0x0045dbe0 — creneau <b>43</b> des blocs 0x00556da8 et 0x00557428
    /// (<c>0x0045F6C0</c>, identique). Decompilee dans <c>Blaze.c</c>, 82 instructions.
    ///
    /// <para>
    /// <b>Un objet qui bascule sur un quart de cercle.</b> Rien ne se passe si <c>+0x14</c> est nul.
    /// Sinon, a la premiere image, il retient sa base : X entier dans <c>+0x10</c> (1 s'il etait
    /// nul), hauteur entiere <b>plus 150</b> dans <c>+0x12</c>, et son cap dans <c>+0x5c</c> comme
    /// angle de depart. Puis, soixante-quatre images durant, l'angle avance de <c>0x10</c> :
    /// <c>X = base - sin(angle) × 150</c>, <c>hauteur = base + 150 - cos(angle') × 150</c>, ou
    /// <c>angle'</c> est l'angle ramene d'un demi-tour quand il depasse <c>0x7ff</c>. Le tangage
    /// gagne 4 par image, un raclement (voix <c>0x20</c>) part toutes les cinq images. A la 64e, il
    /// se rendort : <c>0x50000000</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045DBE0(byte* e)
    {
        if (*(short*)(e + 0x14) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            if (*(short*)(e + 0x16) == 0)
            {
                short baseX = (short)(*(int*)(e + 0x1c) >> 0xc);
                *(short*)(e + 0x10) = baseX;
                if (baseX == 0) *(short*)(e + 0x10) = 1;

                *(short*)(e + 0x16) = 0;
                *(int*)(e + 0x5c) = *(short*)(e + 0x32);
                *(short*)(e + 0x12) = (short)((*(int*)(e + 0x20) >> 0xc) + 0x96);
                *(short*)(e + 0x98) = 0;
            }

            // Ghidra: LAB_0045dc34
            if (*(short*)(e + 0x10) == 0) return;
        }

        // Ghidra: LAB_0045dc3e
        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) == 5)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        // Ghidra: LAB_0045dc82
        int angle = *(int*)(e + 0x5c);
        int sin = (int)Winsys3d.GetSinTableEntry(angle);                     // Ghidra: 0x00545a1c
        *(int*)(e + 0x1c) = (*(short*)(e + 0x10) << 0xc) - sin * 150;

        if (angle >= 0x800) angle -= 0x800;
        int cos = Winsys3d.GetCosTableEntry(angle);                          // Ghidra: 0x00545a16

        *(short*)(e + 0x34) += 4;
        *(int*)(e + 0x20) = (*(short*)(e + 0x12) << 0xc) - cos * 150;
        *(int*)(e + 0x5c) += 0x10;

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) != 0x40) return;

        *(short*)(e + 0x10) = 0;
        *(uint*)e |= 0x50000000;
    }

    // =====================================================================
    // CRENEAUX 53 ET 55
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045c6e0 @ 0x0045c6e0 — creneau <b>53</b> des blocs 0x00556da8 et 0x00557428
    /// (<c>0x0045EB30</c>, identique). Decompilee dans <c>Blaze.c</c>, 61 instructions.
    ///
    /// <para>
    /// <b>Un mecanisme qui tourne quand un drapeau de script est leve</b>, et <b>fait entendre « un
    /// bruit, au loin »</b>. Il ne fait rien tant que le bit de la banque d'etat 0 que designe son
    /// <c>+0x14</c> n'est pas pose (<see cref="ScriptFlagSet"/>), ni une fois a l'etat 2. L'etat 0
    /// remet seulement <c>+0x12</c> a zero : c'est un autre qui pose l'etat 1.
    /// </para>
    /// <para>
    /// A l'etat 1, le tangage <c>+0x34</c> gagne <c>0x20</c> par image, la voix <c>0x17</c> de la
    /// zone sonne aux images 1, 10, 20 et 30, et passe la 32e l'etat passe a 2 et — si
    /// <c>DAT_005de52c</c> est nul — la boite de dialogue s'ouvre sur le flux
    /// <c>0x00551720</c>, « 遠くで音がした。 » (« on a entendu un bruit au loin »).
    /// </para>
    /// <para>
    /// <b>Un detail qui interroge, transcrit tel quel.</b> <c>DAT_005de52c</c> est l'octet que le
    /// chargement de zone (<c>Resources.cs</c>) lit comme <b>numero de sous-zone</b>. Or ce corps
    /// n'est installe que dans les sous-zones 3, 5 a 10 : si l'octet a bien ce sens au moment ou le
    /// corps tourne, le message ne s'affiche jamais depuis ces blocs. Ce que vaut l'octet a cet
    /// instant n'est pas etabli.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045C6E0(byte* e)
    {
        if (!ScriptFlagSet(*(short*)(e + 0x14))) return;

        short phase = *(short*)(e + 0x10);
        if (phase == 2) return;

        if (phase == 0) *(short*)(e + 0x12) = 0;
        if (phase != 1) return;

        *(short*)(e + 0x34) += 0x20;
        (*(short*)(e + 0x12))++;

        short beat = *(short*)(e + 0x12);
        if (beat == 1 || beat == 10 || beat == 0x14 || beat == 0x1e)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x17), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        // Ghidra: LAB_0045c783
        if (*(short*)(e + 0x12) <= 0x20) return;

        *(short*)(e + 0x10) = 2;

        // Ghidra: `mov al,[5DE52Ch] ; test al,al` — l'octet que Resources.cs emploie comme numero
        // de SOUS-ZONE pour choisir les tables de sauts. Voir le resume.
        if (s_Unknown_005de52c != 0) return;

        // Ghidra: `push 551720h` — le flux d'etiquettes noye dans les parametres d'entite.
        BindMenuLabels((byte*)Mem.EntityParam(0x00551720), 0, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
        *(uint*)Mem.Arena(0x005e99c8) = 0x40;
    }

    /// <summary>
    /// Ghidra: FUN_0045c660 @ 0x0045c660 — creneau <b>55</b> des blocs 0x00556da8 et 0x00557428
    /// (<c>0x0045EAB0</c>, identique). Decompilee dans <c>Blaze.c</c>, 47 instructions.
    ///
    /// <para>
    /// <b>Une source d'effets diffuse.</b> Une image sur trois en moyenne (<c>rand() % 3 == 1</c>),
    /// elle se deplace un instant — jusqu'a <c>0xa0</c> vers le haut, ±<c>0x50</c> en X — pour faire
    /// naitre un effet <c>0x30</c> (<c>0xf0</c>) a cet endroit, puis reprend sa position exacte. Les
    /// trois coordonnees sont sauvees et rendues, meme celle qui n'a pas bouge.
    /// </para>
    /// <para>
    /// Le corps rend une valeur (le quotient du tirage, ou le retour de la naissance) que le
    /// repartiteur ignore ; elle n'est pas transcrite.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045C660(byte* e)
    {
        int x = *(int*)(e + 0x1c);
        int y = *(int*)(e + 0x20);
        int z = *(int*)(e + 0x24);

        if ((short)(CrtRand() % 3) == 1)
        {
            *(int*)(e + 0x20) += (CrtRand() % 0xa0) * 0x1000;
            *(int*)(e + 0x1c) += (CrtRand() % 0xa0 - 0x50) * 0x1000;
            SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
        }

        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) = z;
    }
}
