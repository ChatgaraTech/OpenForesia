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
    private static void EntityHandler_0045C7C0(byte* e) => TickScrapingPushable(e, 0x800);

    /// <summary>
    /// Ghidra: FUN_0045ec10 @ 0x0045ec10 — le creneau <b>11</b> du bloc des sous-zones 3, 7 et 9.
    /// Decompilee dans <c>Blaze.c</c>. <b>Le corps de <see cref="EntityHandler_0045C7C0"/> a une
    /// constante pres</b> : le masque de refus passe a <see cref="MoveSceneEntity_00455B30"/> vaut
    /// <c>0x882</c> au lieu de <c>0x800</c>. Le diff LOOSE ne les rapprochait pas, parce que la
    /// constante ne ressemble pas a une adresse.
    /// </summary>
    private static void EntityHandler_0045EC10(byte* e) => TickScrapingPushable(e, 0x882);

    /// <summary>Le corps partage par <see cref="EntityHandler_0045C7C0"/> et
    /// <see cref="EntityHandler_0045EC10"/> ; <paramref name="mask"/> est le masque de refus du
    /// deplacement.</summary>
    private static void TickScrapingPushable(byte* e, uint mask)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x98) = 0;

        // Ghidra: `cmp ax,1` — seul le mot bas du retour compte.
        if ((short)MoveSceneEntity_00455B30(e, mask) == 1) *(uint*)e &= 0x7fffffff;

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
    /// <b>Un detail qui s'eclaire par son jumeau.</b> <c>DAT_005de52c</c> est l'octet que le
    /// chargement de zone (<c>Resources.cs</c>) lit comme <b>numero de sous-zone</b>, et ce corps
    /// n'est installe que dans les sous-zones 3 et 5 a 10 : depuis ces blocs, le message ne s'affiche
    /// donc jamais. <see cref="EntityHandler_0045ABC0"/>, la meme machine pour les sous-zones 0, 1, 2
    /// et 4, est le seul a pouvoir l'ouvrir — dans la sous-zone 0. Le son, lui, part partout.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045C6E0(byte* e)
    {
        if (!TickFlaggedMechanism(e)) return;

        // Ghidra: `push 551720h` — le flux d'etiquettes noye dans les parametres d'entite.
        BindMenuLabels((byte*)Mem.EntityParam(0x00551720), 0, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
        *(uint*)Mem.Arena(0x005e99c8) = 0x40;
    }

    /// <summary>
    /// Ghidra: FUN_0045abc0 @ 0x0045abc0 — le creneau <b>53</b> du bloc des sous-zones 0, 1, 2 et
    /// 4. Decompilee dans <c>Blaze.c</c>, 99 instructions.
    ///
    /// <para>
    /// <b>Le mecanisme de <see cref="EntityHandler_0045C6E0"/>, avec une autre fin.</b> La machine
    /// est la meme au dernier octet (<see cref="TickFlaggedMechanism"/>) ; seule la boite de dialogue
    /// differe. Ici elle prend l'etiquette <c>+0x24</c> de la table <c>+0x70</c> du repertoire de
    /// zone, sur <b>un membre du groupe</b> : celui que designe l'octet <c>0x005de538</c> s'il est
    /// present et disponible (<c>0x80000000</c>, sans <c>0x50010000</c>), sinon le premier qui l'est.
    /// Et elle ne pose pas <c>0x40</c> en <c>0x005e99c8</c>.
    /// </para>
    /// <para>
    /// <b>Si aucun membre n'est disponible</b>, la recherche garde le numero de <c>0x005de538</c> :
    /// la boite s'ouvre sur ce membre quand meme. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045ABC0(byte* e)
    {
        if (!TickFlaggedMechanism(e)) return;

        byte* slots = Inp.s_SlotFlags_005e93c0;

        static bool Available(byte* slot)
            => (*(uint*)slot & 0x80000000) != 0 && (*(uint*)slot & 0x50010000) == 0;

        // Ghidra: `mov cl,[5DE538h]` — l'octet seul, etendu sans signe.
        short member = (byte)Mem.s_Unknown_005de538;

        if (!Available(slots + member * 0x9c))
        {
            // Ghidra: LAB_0045acb6 — la boucle s'arrete au premier trouve en poussant son compteur
            // a 0x63.
            for (short i = 0; i <= 3; i++)
            {
                if (!Available(slots + i * 0x9c)) continue;

                member = i;
                break;
            }
        }

        byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0x24);
        BindMenuLabels(labels, slots + member * 0x9c, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
    }

    /// <summary>
    /// La machine commune a <see cref="EntityHandler_0045C6E0"/> et
    /// <see cref="EntityHandler_0045ABC0"/>, instruction pour instruction jusqu'au test de
    /// <c>0x005de52c</c> : elle rend <c>true</c> quand la boite de dialogue doit s'ouvrir.
    /// </summary>
    private static bool TickFlaggedMechanism(byte* e)
    {
        if (!ScriptFlagSet(*(short*)(e + 0x14))) return false;

        short phase = *(short*)(e + 0x10);
        if (phase == 2) return false;

        if (phase == 0) *(short*)(e + 0x12) = 0;
        if (phase != 1) return false;

        *(short*)(e + 0x34) += 0x20;
        (*(short*)(e + 0x12))++;

        short beat = *(short*)(e + 0x12);
        if (beat == 1 || beat == 10 || beat == 0x14 || beat == 0x1e)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x17), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x12) <= 0x20) return false;

        *(short*)(e + 0x10) = 2;

        // Ghidra: `mov al,[5DE52Ch] ; test al,al` — l'octet que Resources.cs emploie comme numero
        // de SOUS-ZONE pour choisir les tables de sauts. Voir le resume de 0x0045C6E0.
        return s_Unknown_005de52c == 0;
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

    // =====================================================================
    // LES SONS D'AMBIANCE DES SOUS-ZONES 0, 1, 2 ET 4
    //
    // Deux d'entre eux se taisent selon des bits de la banque d'etat 0 (0x005e96e0) : l'octet 0x22
    // (0x005e9702). Ce que ces bits signalent n'est pas etabli ; le nom des corps dit ce qu'ils font.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045a9b0 @ 0x0045a9b0 — creneau <b>50</b> du bloc <c>0x00556728</c>. Decompilee
    /// dans <c>Blaze.c</c>, 31 instructions.
    ///
    /// <para>
    /// Un son d'ambiance <b>global</b> (<c>Audio.PlaySoundEffect</c> direct, voix <c>0x12</c>,
    /// gain <c>0x800</c>) toutes les dix images — <b>sauf</b> si les bits <c>0x10</c> et <c>0x20</c>
    /// de l'octet <c>0x005e9702</c> sont poses ensemble.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045A9B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 10) return;

        byte bank = Mem.s_StateBank0_005e96e0[0x22];                            // Ghidra: DAT_005e9702
        if ((bank & 0x10) == 0 || (bank & 0x20) == 0)
        {
            Aud.PlaySoundEffect(ZoneSound(0x12), 0x400, 0x800, 0x800);         // Ghidra: FUN_0053cea0
        }

        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0045aa20</c> — creneau <b>44</b> du bloc <c>0x00556728</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 30 instructions.
    ///
    /// <para>
    /// Un son a portee de camera (voix <c>0x19</c>, hauteur <c>0x300</c>) toutes les douze images.
    /// <b>Il ne fait rien du tout</b> — pas meme sa premiere image — tant que le bit <c>0x08</c> de
    /// l'octet <c>0x005e9702</c> est pose.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045AA20(byte* e)
    {
        if ((Mem.s_StateBank0_005e96e0[0x22] & 8) != 0) return;                // Ghidra: DAT_005e9702

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x0c) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x19), 0x300, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0045a310 @ 0x0045a310 — creneau <b>61</b> du bloc <c>0x00556728</c> et <b>62</b> du
    /// bloc <c>0x00557428</c> : un seul corps pour sept sous-zones. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Un son au hasard.</b> Le premier tour pose <c>0x6000</c> sur le mot d'etat. Ensuite un
    /// compteur monte d'un par image ; passe 30, une chance sur soixante par image de sonner, et au
    /// plus tard a 51. Le son est global (voix <c>0x30</c>), a une hauteur tiree entre 500 et 699
    /// et un gain tire entre <c>0x9c4</c> et <c>0x1193</c> — deux tirages, dans cet ordre.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045A310(byte* e)
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
            Aud.PlaySoundEffect(ZoneSound(0x30), pitch, gain, gain);           // Ghidra: FUN_0053cea0
        }
    }

    // =====================================================================
    // LES DECORS QUI BOUGENT, SOUS-ZONES 0, 1, 2 ET 4
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045c090 @ 0x0045c090 — creneau <b>30</b> du bloc <c>0x00556728</c>. Decompilee
    /// dans <c>Blaze.c</c>, 41 instructions.
    ///
    /// <para>
    /// <b>Un cercle vertical de rayon 100</b> : sinus sur X, cosinus sur la hauteur, pas
    /// <c>0x28</c>. <b>L'avance precede le calcul</b>, a l'inverse des tourniquets du bloc voisin, et
    /// la vitesse est remise a zero en tete de chaque image.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045C090(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                                        // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        *(short*)(e + 0x12) += 0x28;
        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));        // Ghidra: Ordinal_451
        *(int*)(e + 0x1c) = *(short*)(e + 0x14) * 0x1000 + sin * 100;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));             // Ghidra: Ordinal_450
        *(int*)(e + 0x20) = *(short*)(e + 0x16) * 0x1000 + cos * 100;
    }

    /// <summary>
    /// Ghidra: FUN_0045b290 @ 0x0045b290 — creneau <b>16</b> du bloc <c>0x00556728</c>. Decompilee
    /// dans <c>Blaze.c</c>, 98 instructions.
    ///
    /// <para>
    /// <b>Un balancier qui cogne.</b> Sa vitesse angulaire <c>+0x12</c> part a ±32 (sens tire a pile
    /// ou face, <c>+0x14</c> garde ±1) et change d'un par demi-pas ; le tangage <c>+0x34</c> gagne
    /// <c>(v / 2 + v) / 2</c>, soit les trois quarts de la vitesse. <b>Le pas est fait deux fois par
    /// image</b>, a l'identique — le compilateur a deroule la boucle. Quand la vitesse atteint ±32,
    /// l'acceleration s'inverse et le choc sonne (voix <c>0x1a</c>).
    /// </para>
    /// <para>
    /// <b>Il frappe le groupe</b> a chaque image, a tous les etats : <see cref="StrikePartyMembers_0045DA40"/>
    /// avec 10 %, selecteur 0, sans parametre d'effet ni verrou particulier. C'est un piege.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045B290(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            // Ghidra: `cdq ; xor ; sub ; and eax,1 ; xor ; sub` — le reste signe de rand() / 2, puis
            // `lea eax,[eax+eax-1]` : -1 ou +1.
            int sign = CrtRand() % 2 * 2 - 1;
            *(short*)(e + 0x14) = (short)sign;
            *(short*)(e + 0x12) = (short)(-sign << 5);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SwingKnockingPendulum(e);
            SwingKnockingPendulum(e);                                           // Ghidra: LAB_0045b339, le meme pas
        }

        // Ghidra: LAB_0045b399
        StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);
    }

    /// <summary>Ghidra: <c>0x0045b2da..0x0045b336</c> et <c>0x0045b339..0x0045b396</c> — <b>un
    /// demi-pas du balancier</b>, emis deux fois a l'identique.</summary>
    private static void SwingKnockingPendulum(byte* e)
    {
        short speed = *(short*)(e + 0x12);
        *(short*)(e + 0x34) += (short)((speed / 2 + speed) / 2);

        short push = *(short*)(e + 0x14);
        speed = (short)(speed + push);
        *(short*)(e + 0x12) = speed;

        // Ghidra: `cmp cx,20h ; jge` puis `cmp cx,0FFE0h ; jg` — le choc a ±0x20.
        if (speed < 0x20 && speed > -0x20) return;

        *(short*)(e + 0x14) = (short)-push;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1a), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
    }

    /// <summary>
    /// Ghidra: FUN_0045be00 @ 0x0045be00 — creneau <b>27</b> du bloc <c>0x00556728</c>. Decompilee
    /// dans <c>Blaze.c</c>, 72 instructions.
    ///
    /// <para>
    /// <b>Un decor qui s'etire quand un bit d'etat est leve</b> — le bit <c>0x20</c> du dword
    /// <c>0x00571690</c>, dans l'arene d'etat d'entite. Tant qu'il est absent, l'echelle verticale
    /// <c>+0x2a</c> reste a zero : le decor est invisible. Des qu'il est pose : l'etat 100 passe a 4,
    /// puis aussitot a 1 avec un son (voix <c>0x22</c>) si l'horloge <c>+0x9a</c> a depasse 20 ;
    /// l'echelle monte de 400 par image jusqu'a <c>0x2000</c>, redescend de 100 jusqu'a
    /// <c>0x1000</c>, tient vingt images, et l'etat se fige a 99.
    /// </para>
    /// <para>
    /// L'horloge <c>+0x9a</c> tourne a chaque image et revient a 30 passe 300. Le cap <c>+0x32</c> est
    /// ramene sous <c>0x1000</c> sans que ce corps ne l'avance.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045BE00(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x10) = 100;
        }

        (*(short*)(e + 0x9a))++;
        if (*(short*)(e + 0x9a) > 300) *(short*)(e + 0x9a) = 0x1e;

        if ((*Mem.EntityState(0x00571690) & 0x20) == 0)                          // Ghidra: DAT_00571690
        {
            *(short*)(e + 0x2a) = 0;
            return;
        }

        if (*(short*)(e + 0x32) > 0xfff) *(short*)(e + 0x32) -= 0x1000;

        if (*(short*)(e + 0x10) == 100)
        {
            *(short*)(e + 0x10) = 4;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 4)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;

                if (*(short*)(e + 0x9a) > 0x14)
                {
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x22), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
                }
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x2a) += 400;
            if (*(short*)(e + 0x2a) > 0x1fff) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x2a) -= 100;
            if (*(short*)(e + 0x2a) < 0x1001)
            {
                (*(short*)(e + 0x12))++;
                *(short*)(e + 0x2a) = 0x1000;
                *(short*)(e + 0x10) = 3;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 0x14) *(short*)(e + 0x10) = 99;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0045b9e0</c> — creneau <b>25</b> du bloc <c>0x00556728</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 86 instructions, <c>Legacy/Raw/Blaze/FUN_0045B9E0.asm</c>.
    ///
    /// <para>
    /// <b>Le meme bit d'etat que <see cref="EntityHandler_0045BE00"/></b> (<c>0x20</c> de
    /// <c>0x00571690</c>). Absent, l'entite porte <c>0x2000</c> et ne bouge pas. Pose, elle retire
    /// <c>0x2000</c> et vit :
    /// </para>
    /// <list type="table">
    ///   <item><term>naissance</term><description>son cap de placement est garde en <c>+0x16</c> ;
    ///     le cap <c>+0x32</c> tourne de <c>0x10</c> par image, a tous les etats.</description></item>
    ///   <item><term>etat 1</term><description>elle monte de <c>0x6000</c> par image jusqu'a
    ///     <c>0xc8000</c>, puis passe a 2 avec un rayon <c>+0x9a = 8</c>.</description></item>
    ///   <item><term>etat 2</term><description>elle derive dans son cap de depart —
    ///     <b>cosinus sur X, sinus sur Z</b>, l'inverse de l'usage — d'autant que le rayon, et monte de
    ///     <c>(rayon &amp; ~1) &lt;&lt; 11</c>. Apres 45 images, le rayon change de signe, le compteur
    ///     repart a -45 et l'etat passe a 3.</description></item>
    ///   <item><term>etat 3</term><description>quarante et une images de pause, puis retour a 2 : elle
    ///     derive dans l'autre sens, et redescend.</description></item>
    /// </list>
    /// <para>
    /// <b>Le test des etats 1 et 2 porte sur l'etat lu avant l'avance du cap</b> (<c>mov ax,[esi+10h]</c>
    /// puis <c>cmp ax</c>) ; celui de l'etat 3 le relit.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045B9E0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                                        // Ghidra: FUN_00460fc0

        uint flags = *(uint*)e | 0x2000;
        *(uint*)e = flags;

        if ((*Mem.EntityState(0x00571690) & 0x20) == 0) return;                  // Ghidra: `mov cl,[571690h]`

        *(uint*)e = flags & 0xffffdfff;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = *(short*)(e + 0x32);
        }

        // Ghidra: LAB_0045ba27
        short phase = *(short*)(e + 0x10);
        *(short*)(e + 0x32) = (short)((*(short*)(e + 0x32) + 0x10) & 0xfff);

        if (phase == 1)
        {
            int y = *(int*)(e + 0x20) + 0x6000;
            *(int*)(e + 0x20) = y;

            if (y <= 0xc8000) return;

            *(int*)(e + 0x20) = 0xc8000;
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x9a) = 8;
            return;
        }

        // Ghidra: LAB_0045ba77
        if (phase == 2)
        {
            int course = *(short*)(e + 0x16);
            short radius = *(short*)(e + 0x9a);

            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(course) * radius;        // Ghidra: 0x00545a16
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(course) * radius;   // Ghidra: 0x00545a1c
            *(int*)(e + 0x20) += (radius & ~1) << 0xb;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x2d)
            {
                *(short*)(e + 0x9a) = (short)-radius;
                *(short*)(e + 0x12) = -0x2d;
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x14) = 0;
            }
        }

        // Ghidra: LAB_0045baec
        if (*(short*)(e + 0x10) != 3) return;

        (*(short*)(e + 0x14))++;
        if (*(short*)(e + 0x14) > 0x28) *(short*)(e + 0x10) = 2;
    }

    // =====================================================================
    // LES DECORS QUI ECLAIRENT UN VOISIN
    //
    // Deux corps adressent un AUTRE enregistrement du meme tableau d'entites : celui qui est
    // +0x14 emplacements plus loin (`e + (short)+0x14 * 0x9c`). Le placement range donc en +0x14 la
    // distance a un partenaire, et le decor lit ou ecrit le mot d'etat de ce partenaire.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0045a3b0</c> — creneau <b>59</b> du bloc <c>0x00556728</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 84 instructions, <c>Legacy/Raw/Blaze/FUN_0045A3B0.asm</c>.
    ///
    /// <para>
    /// <b>Une lumiere qui compte quatre verrous.</b> Elle bourdonne toutes les soixante images (son
    /// <c>0x2d</c> — un identifiant direct, pas une voix de zone), tourne de <c>0x80</c> par image, et
    /// sa teinte tend vers une cible par huitiemes. La cible est <c>0x40</c> par bit ABSENT parmi
    /// <c>0x10</c>, <c>0x20</c>, <c>0x40</c>, <c>0x80</c> de l'octet <c>0x005e9701</c>, et <c>0xff</c>
    /// au lieu de <c>0x100</c> quand les quatre manquent. <b>Quand les quatre sont poses</b>, la cible
    /// tombe a zero et le partenaire <c>+0x14</c> recoit le bit <c>0x2000</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0045A3B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0xff;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x98) == 0) TryPlaySoundNearEntity((uint*)e, 0x2d, 0x80, 0x1800, 0, 0);   // Ghidra: FUN_00468080

        // Ghidra: LAB_0045a3f5
        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) >= 0x3c) *(short*)(e + 0x98) = 0;

        // Ghidra: LAB_0045a40f
        byte bank = Mem.s_StateBank0_005e96e0[0x21];                            // Ghidra: DAT_005e9701
        short missing = 0;
        if ((bank & 0x10) == 0) missing = 1;
        if ((bank & 0x20) == 0) missing++;
        if ((bank & 0x40) == 0) missing++;
        if ((bank & 0x80) == 0) missing++;

        if (missing == 0)
        {
            byte* partner = e + *(short*)(e + 0x14) * 0x9c;
            *(uint*)partner |= 0x2000;
        }

        // Ghidra: LAB_0045a44f — `shl eax,6` puis `cmp ax,100h`.
        short target = (short)(missing << 6);
        *(short*)(e + 0x12) = target;
        if (target == 0x100) *(short*)(e + 0x12) = 0xff;

        // Ghidra: LAB_0045a462
        int goal = *(short*)(e + 0x12);
        *(short*)(e + 0x32) += 0x80;

        for (int channel = 0x38; channel <= 0x3a; channel++)
        {
            byte current = e[channel];
            e[channel] = (byte)(current + ((goal - current) >> 3));
        }

        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: <c>0x0045e4b0</c> — creneau <b>60</b> des blocs <c>0x00556728</c> et <c>0x00557428</c>.
    /// <b>Absente de <c>Blaze.c</c></b> ; 15 instructions, <c>Legacy/Raw/Blaze/FUN_0045E4B0.asm</c>.
    /// <b>Il recopie le bit <c>0x2000</c> de son partenaire</b> <c>+0x14</c> sur lui-meme, pose ou
    /// efface. C'est la moitie qui ecoute de <see cref="EntityHandler_0045A3B0"/>.
    /// </summary>
    private static void EntityHandler_0045E4B0(byte* e)
    {
        byte* partner = e + *(short*)(e + 0x14) * 0x9c;

        if ((*(uint*)partner & 0x2000) != 0) *(uint*)e |= 0x2000;
        else *(uint*)e &= 0xffffdfff;
    }

    // =====================================================================
    // LA DALLE A POIDS
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045fa80 @ 0x0045fa80 — creneau <b>45</b> du bloc <c>0x00557428</c> (sous-zones 3,
    /// 7 et 9). Decompilee dans <c>Blaze.c</c>, 88 instructions.
    ///
    /// <para>
    /// <b>Une dalle qui s'abaisse quand tout le groupe y monte.</b> Elle suit son partenaire
    /// <c>+0x14</c> a <c>0x64000</c> sous lui. Si aucun membre ne lui est rattache
    /// (<see cref="CountPartyMembersMatching"/>, bit <c>0x20</c>, sans <c>0x40000000</c>) et que sa
    /// hauteur n'est pas <c>0xfa0000</c>, elle efface les bits <c>0x30</c> de l'octet
    /// <c>0x005e96f6</c> et remonte le partenaire a <c>0x64000</c>.
    /// </para>
    /// <para>
    /// <b>Si tous les membres actifs y sont rattaches</b>, elle les recompte a la distance : moins de
    /// 100 unites du partenaire, hauteur decalee de 100. S'ils y sont tous encore, le bit <c>0x10</c>
    /// de l'octet <c>0x005e96f6</c> est pose. Le compte de distance est un dword compare sur son mot
    /// bas (<c>cmp [esp+14h],ax</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_0045FA80(byte* e)
    {
        byte* partner = e + *(short*)(e + 0x14) * 0x9c;
        *(int*)(e + 0x20) = *(int*)(partner + 0x20) - 0x64000;

        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;

        short riders = (short)CountPartyMembersMatching(e, 0x20, 0x40000000);   // Ghidra: FUN_00535000

        if (riders == 0 && *(int*)(e + 0x20) != 0xfa0000)
        {
            Mem.s_StateBank0_005e96e0[0x16] &= 0xcf;                            // Ghidra: DAT_005e96f6
            *(int*)(e + *(short*)(e + 0x14) * 0x9c + 0x20) = 0x64000;
        }

        // Ghidra: LAB_0045fae9
        if (riders != (short)ActivePartySlotCount_00466D70()) return;

        int near = 0;
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            byte* anchor = e + *(short*)(e + 0x14) * 0x9c;
            int dx = (*(int*)(slot + 0x1c) >> 0xc) - (*(int*)(anchor + 0x1c) >> 0xc);
            int dy = (*(int*)(slot + 0x20) >> 0xc) - (*(int*)(anchor + 0x20) >> 0xc) + 100;
            int dz = (*(int*)(slot + 0x24) >> 0xc) - (*(int*)(anchor + 0x24) >> 0xc);

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) < 100) near++;
        }

        if ((short)near == (short)ActivePartySlotCount_00466D70())
        {
            Mem.s_StateBank0_005e96e0[0x16] |= 0x10;
        }
    }

    // =====================================================================
    // LES CINQ FORMES A JUMEAUX DE LA TABLE 0x0056adc8 (2026-09-17)
    //
    // Categorie C de Zone4ParComplexite.md, prise PAR FORME et non par zone : dix creneaux pour
    // cinq lectures. Le compilateur a emis chaque corps une fois par bloc de sous-zones, et le
    // desassemblage tronque a la derniere instruction `ret` ne rend, d'un jumeau a l'autre, que ce
    // qui est dit ci-dessous.
    //
    //   0x0045DF60 / 0x0045FFA0   59 instr.  banc de verrous 0x005716E0 / 0x00571730
    //   0x0045E040 / 0x00460080   47         idem
    //   0x0045E100 / 0x00460140   56         idem
    //   0x0045CF70 / 0x0045F3B0  176         AUCUNE difference : le meme corps, deux fois
    //   0x0045A630 / 0x0045E4E0  239         AUCUNE difference : le meme corps, deux fois
    //
    // LES DEUX DERNIERES PAIRES N'ONT PAS DE PARAMETRE. Leurs octets ne different que par les
    // deplacements relatifs des `call` et des `jmp`, qui resolvent vers les memes cibles absolues —
    // le meme cas que `LAB_00474320` / `FUN_00450700`. Un seul corps, deux enveloppes nues.
    //
    // LE BANC DE VERROUS. `0x005716E0` (sous-zones 5, 6, 8, 10) et `0x00571730` (3, 7, 9) sont un
    // meme octet de cinq bits, un par mecanisme. 0x0045DF60 et 0x0045E040 le POSENT en pivotant ;
    // 0x0045E100 l'ATTEND, puis l'EFFACE. Les trois lisent leur numero de mecanisme dans `+0x14`,
    // de 1 a 5 — hors de cette plage, ils ne touchent a rien.
    // =====================================================================

    /// <summary>Ghidra: FUN_0045df60 @ 0x0045df60 — le pivot sonore des sous-zones 5, 6, 8 et 10.
    /// Decompilee dans <c>Blaze.c</c>. Corps : <see cref="TickTowerHinge_0045DF60"/>.</summary>
    private static void EntityHandler_0045DF60(byte* e)
        => TickTowerHinge_0045DF60(e, Mem.EntityStateBytes(0x005716e0));

    /// <summary>Ghidra: FUN_0045ffa0 @ 0x0045ffa0 — le meme corps pour les sous-zones 3, 7 et 9 ;
    /// le diff ne rend que l'adresse du banc de verrous, <c>0x00571730</c>.</summary>
    private static void EntityHandler_0045FFA0(byte* e)
        => TickTowerHinge_0045DF60(e, Mem.EntityStateBytes(0x00571730));

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0045DF60"/> et <see cref="EntityHandler_0045FFA0"/> :
    /// <b>un pivot de quarante images qui annonce son ouverture</b>.
    ///
    /// <para>
    /// A la premiere image il joue <c>sons[0x2e]</c>, arme une vitesse de <b>15</b> en
    /// <c>+0x54</c>, pose le bit <c>0x2000</c> et <b>allume son bit du banc de verrous</b> selon son
    /// numero <c>+0x14</c> (1 a 5 ; hors plage, rien n'est pose). Puis, a chaque image, la vitesse
    /// gagne un huitieme d'elle-meme pendant les douze premieres et en perd un huitieme ensuite —
    /// la meme courbe que le tourniquet de la foret (<c>TurnstileSpin</c>), sur d'autres champs — et
    /// le cap <c>+0x32</c> <b>avance</b> d'autant. A la quarantieme image il pose
    /// <c>0x40002000</c> et <b>ne fait pas avancer le cap</b> : le binaire rend la main avant.
    /// </para>
    /// <para>
    /// Le bit <c>0x40000000</c> en tete est une sortie seche : une fois pose par la derniere image,
    /// le corps ne fait plus rien. C'est le meme verrou que le tourniquet.
    /// </para>
    /// <para>
    /// <b>Son frere silencieux est <see cref="TickTowerHingeSilent_0045E040"/></b>, qui tourne dans
    /// l'autre sens et ne joue rien : les deux battants d'une meme ouverture, un seul son pour la
    /// paire. Les deux corps sont ecrits separement parce que le desassembleur les separe —
    /// partager avec un parametre de signe effacerait le son.
    /// </para>
    /// </summary>
    private static void TickTowerHinge_0045DF60(byte* e, byte* latches)
    {
        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            // Ghidra: FUN_00468080, sons[0x2e] — le son est joue AVANT la pose des drapeaux.
            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x2e], 0x800, 0x1800, 0, 0);

            *(uint*)e |= 0x2000;
            *(int*)(e + 0x54) = 0x0f;
            SetTowerLatch(latches, *(short*)(e + 0x14));
        }

        int speed = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = *(short*)(e + 0x10) <= 0x0c ? (speed >> 3) + speed : speed - (speed >> 3);

        short frame = (short)(*(short*)(e + 0x10) + 1);
        *(short*)(e + 0x10) = frame;

        if (frame == 0x28)
        {
            *(uint*)e |= 0x40002000;
            return;
        }

        *(short*)(e + 0x32) += (short)*(int*)(e + 0x54);
    }

    /// <summary>Ghidra: <c>0x0045e040</c> — le pivot silencieux des sous-zones 5, 6, 8 et 10.
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, dump conserve dans
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045E040.asm</c>.</summary>
    private static void EntityHandler_0045E040(byte* e)
        => TickTowerHingeSilent_0045E040(e, Mem.EntityStateBytes(0x005716e0));

    /// <summary>Ghidra: <c>0x00460080</c> — le meme corps pour les sous-zones 3, 7 et 9 ; le diff ne
    /// rend que l'adresse du banc de verrous.</summary>
    private static void EntityHandler_00460080(byte* e)
        => TickTowerHingeSilent_0045E040(e, Mem.EntityStateBytes(0x00571730));

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0045E040"/> et <see cref="EntityHandler_00460080"/> :
    /// <b>le second battant</b>. Meme armement, meme courbe, meme verrou final que
    /// <see cref="TickTowerHinge_0045DF60"/>, a deux differences pres et elles sont toute la
    /// fonction :
    ///
    /// <list type="bullet">
    /// <item>il ne joue <b>aucun son</b> — une paire de battants ne sonne qu'une fois ;</item>
    /// <item>le cap <c>+0x32</c> <b>recule</b> (<c>sub [eax+32h],cx</c>) au lieu d'avancer.</item>
    /// </list>
    /// </summary>
    private static void TickTowerHingeSilent_0045E040(byte* e, byte* latches)
    {
        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x2000;
            *(int*)(e + 0x54) = 0x0f;
            SetTowerLatch(latches, *(short*)(e + 0x14));
        }

        int speed = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = *(short*)(e + 0x10) <= 0x0c ? (speed >> 3) + speed : speed - (speed >> 3);

        short frame = (short)(*(short*)(e + 0x10) + 1);
        *(short*)(e + 0x10) = frame;

        if (frame == 0x28)
        {
            *(uint*)e |= 0x40002000;
            return;
        }

        *(short*)(e + 0x32) -= (short)*(int*)(e + 0x54);
    }

    /// <summary>
    /// La table de sauts des deux pivots : le numero de mecanisme <paramref name="slot"/> allume son
    /// bit du banc de verrous. Le binaire ecrit <c>dec ecx ; cmp ecx,4 ; ja</c> puis cinq
    /// <c>or byte […],2^n</c> — un indice hors de 1..5 ne touche a rien.
    /// </summary>
    private static void SetTowerLatch(byte* latches, short slot)
    {
        switch (slot)
        {
            case 1: latches[0] |= 0x01; break;
            case 2: latches[0] |= 0x02; break;
            case 3: latches[0] |= 0x04; break;
            case 4: latches[0] |= 0x08; break;
            case 5: latches[0] |= 0x10; break;
        }
    }

    /// <summary>Ghidra: <c>0x0045e100</c> — ce qui attend le verrou, sous-zones 5, 6, 8 et 10.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045E100.asm</c>.</summary>
    private static void EntityHandler_0045E100(byte* e)
        => TickTowerLatchedGrowth_0045E100(e, Mem.EntityStateBytes(0x005716e0));

    /// <summary>Ghidra: <c>0x00460140</c> — le meme corps pour les sous-zones 3, 7 et 9.</summary>
    private static void EntityHandler_00460140(byte* e)
        => TickTowerLatchedGrowth_0045E100(e, Mem.EntityStateBytes(0x00571730));

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0045E100"/> et <see cref="EntityHandler_00460140"/> :
    /// <b>le consommateur du banc de verrous</b>, et le troisieme cote du mecanisme.
    ///
    /// <para>
    /// A la premiere image il met son echelle <c>+0x28</c> a zero et copie sa vitesse de croissance
    /// de <c>+0x16</c> — une valeur que sa fiche de placement porte — dans <c>+0x12</c>. Ensuite, a
    /// chaque image, <b>il ne fait rien tant que son bit n'est pas allume</b> : la cascade des cinq
    /// <c>cmp cx,N ; test dl,2^(N-1) ; je</c> rend la main des que le bit manque.
    /// </para>
    /// <para>
    /// Le bit pose, l'echelle monte de <c>+0x12</c> par image jusqu'a <c>0x1000</c>, ou elle est
    /// bornee ; le corps passe alors a l'etat 2, pose <c>0x40000000</c> et <b>efface son bit</b> —
    /// la croissance n'a donc lieu qu'une fois, et le verrou est rendu.
    /// </para>
    /// <para>
    /// <b>Le banc est lu une seule fois</b>, en tete (<c>mov dl,[5716E0h]</c>), avant les cinq
    /// comparaisons. Et il est <b>pose a l'octet</b> par les pivots mais <b>efface au mot</b> ici
    /// (<c>and word […],0FFFEh</c>) : les deux largeurs sont conservees telles quelles.
    /// </para>
    /// </summary>
    private static void TickTowerLatchedGrowth_0045E100(byte* e, byte* latches)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x16);
        }

        short slot = *(short*)(e + 0x14);
        byte latched = latches[0];

        if (slot == 1 && (latched & 0x01) == 0) return;
        if (slot == 2 && (latched & 0x02) == 0) return;
        if (slot == 3 && (latched & 0x04) == 0) return;
        if (slot == 4 && (latched & 0x08) == 0) return;
        if (slot == 5 && (latched & 0x10) == 0) return;

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x28) += *(short*)(e + 0x12);
        if (*(short*)(e + 0x28) < 0x1000) return;

        *(short*)(e + 0x28) = 0x1000;
        *(uint*)e |= 0x40000000;
        *(short*)(e + 0x10) = 2;

        // Ghidra: seconde table de sauts, `and word […]` — un mot, la ou la pose est un octet.
        ushort* word = (ushort*)latches;
        switch (slot)
        {
            case 1: *word &= 0xfffe; break;
            case 2: *word &= 0xfffd; break;
            case 3: *word &= 0xfffb; break;
            case 4: *word &= 0xfff7; break;
            case 5: *word &= 0xffef; break;
        }
    }

    /// <summary>Ghidra: <c>0x0045cf70</c> — le souffleur des sous-zones 5, 6, 8 et 10.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045CF70.asm</c>.</summary>
    private static void EntityHandler_0045CF70(byte* e) => TickTowerBlower_0045CF70(e);

    /// <summary>Ghidra: <c>0x0045f3b0</c> — <b>le meme corps, sans une seule difference</b>, pour
    /// les sous-zones 3, 7 et 9. Les 569 octets ne different que par les deplacements relatifs des
    /// <c>call</c>, qui resolvent vers les memes cibles.</summary>
    private static void EntityHandler_0045F3B0(byte* e) => TickTowerBlower_0045CF70(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0045CF70"/> et <see cref="EntityHandler_0045F3B0"/> :
    /// <b>un decor qui oscille et souffle deux bouffees de quatre effets</b>.
    ///
    /// <para>
    /// <b>Il se sert de lui-meme comme point d'emission</b>, et c'est la seule difficulte de la
    /// fonction : pour chaque bouffee il met de cote sa position (<c>+0x1c</c>, <c>+0x20</c>,
    /// <c>+0x24</c>), son ancre (<c>+0x50</c>, <c>+0x54</c>, <c>+0x58</c>) et son cap
    /// (<c>+0x32</c>), se transporte a <b>120 unites</b> dans la direction voulue et <b>200 unites
    /// plus haut</b>, fait naitre quatre fois le sous-type <c>0x39</c>, puis remet les sept valeurs.
    /// Les deux directions sont <c>cap + 0x80</c> et <c>cap + 0x880</c>.
    /// </para>
    /// <para>
    /// <b>Les quatre naissances d'une bouffee ne different que par <c>+0x14</c></b>, mis a 0, 1, 2
    /// puis 3 juste avant chaque appel et restaure apres, avec <c>+0x16</c> force a 10 :
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> recopie l'entite entiere, c'est ainsi que
    /// l'enfant recoit sa variante. Meme mecanique que <c>SpawnSummonChildren</c> dans la foret.
    /// </para>
    /// <para>
    /// <b>Quand il souffle.</b> Le compteur <c>+0x14</c> avance d'une image par tour ; la bouffee
    /// part quand il atteint <b>trois fois</b> la periode <c>+0x16</c>, ou bien — une chance sur
    /// cinquante par image — des qu'il depasse 10. Une periode nulle interdit tout.
    /// </para>
    /// <para>
    /// <b>Et il oscille toujours</b>, bouffee ou non : le cap avance de <c>0x44</c> par image et
    /// repasse par zero au-dela de <c>0x1000</c>, et la hauteur recoit le sinus de ce cap
    /// <b>decale de <c>+0x12</c></b> — l'octet bas, pas le mot —, c'est-a-dire une amplitude que sa
    /// fiche de placement fixe en puissance de deux.
    /// </para>
    /// </summary>
    private static void TickTowerBlower_0045CF70(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                       // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = *(short*)(e + 0x14);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;
        }

        (*(short*)(e + 0x14))++;

        short draw = (short)(CrtRand() % 0x32);                // Ghidra: FUN_00547140
        short period = *(short*)(e + 0x16);
        short elapsed = *(short*)(e + 0x14);

        bool due = elapsed >= period * 3 || (draw == 0 && elapsed > 10);

        if (due && period != 0)
        {
            int keptX = *(int*)(e + 0x1c);
            int keptY = *(int*)(e + 0x20);
            int keptZ = *(int*)(e + 0x24);
            int keptAnchorX = *(int*)(e + 0x50);
            int keptAnchorY = *(int*)(e + 0x54);
            int keptAnchorZ = *(int*)(e + 0x58);
            short keptCourse = *(short*)(e + 0x32);

            *(short*)(e + 0x14) = 0;

            *(short*)(e + 0x32) = (short)(keptCourse + 0x80);
            BlowAwayFromSelf_0045CF70(e);

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x20) = keptY;
            *(int*)(e + 0x24) = keptZ;
            *(int*)(e + 0x50) = keptAnchorX;
            *(int*)(e + 0x54) = keptAnchorY;
            *(int*)(e + 0x58) = keptAnchorZ;

            *(short*)(e + 0x32) = (short)(keptCourse + 0x880);
            BlowAwayFromSelf_0045CF70(e);

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x20) = keptY;
            *(int*)(e + 0x24) = keptZ;
            *(int*)(e + 0x50) = keptAnchorX;
            *(int*)(e + 0x54) = keptAnchorY;
            *(int*)(e + 0x58) = keptAnchorZ;
            *(short*)(e + 0x32) = keptCourse;
        }

        // Ghidra: LAB_0045d172 — l'oscillation, atteinte par tous les chemins.
        short course = (short)(*(short*)(e + 0x32) + 0x44);
        *(short*)(e + 0x32) = course;
        if (course > 0x1000) *(short*)(e + 0x32) = (short)(course - 0x1000);

        *(int*)(e + 0x20) +=
            (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) << e[0x12];   // Ghidra: 0x00545a1c
    }

    /// <summary>
    /// Une bouffee de <see cref="TickTowerBlower_0045CF70"/> : le deplacement de 120 unites dans la
    /// direction du cap courant, la montee de 200, et les quatre naissances. Le binaire ecrit ce
    /// bloc <b>deux fois</b>, a l'identique ; le facteur 120 y est <c>lea ×3</c>, <c>lea ×5</c>,
    /// <c>shl 3</c>.
    /// </summary>
    private static void BlowAwayFromSelf_0045CF70(byte* e)
    {
        short course = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 120;   // Ghidra: 0x00545a1c
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 120;        // Ghidra: 0x00545a16
        *(int*)(e + 0x20) += 0xc8000;

        for (short variant = 0; variant <= 3; variant++)
        {
            short keptVariant = *(short*)(e + 0x14);
            short keptPeriod = *(short*)(e + 0x16);

            *(short*)(e + 0x14) = variant;
            *(short*)(e + 0x16) = 10;
            SpawnEffectNearCamera_00468110(e, 0x39, 0, -1);                  // Ghidra: FUN_00468110

            *(short*)(e + 0x14) = keptVariant;
            *(short*)(e + 0x16) = keptPeriod;
        }
    }

    /// <summary>Ghidra: FUN_0045a630 @ 0x0045a630 — la balance des sous-zones 0, 1, 2 et 4.
    /// Decompilee dans <c>Blaze.c</c>.</summary>
    private static void EntityHandler_0045A630(byte* e) => TickTowerSeesaw_0045A630(e);

    /// <summary>Ghidra: FUN_0045e4e0 @ 0x0045e4e0 — <b>le meme corps, sans une seule
    /// difference</b>, pour les sous-zones 3, 7 et 9.</summary>
    private static void EntityHandler_0045E4E0(byte* e) => TickTowerSeesaw_0045A630(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0045A630"/> et <see cref="EntityHandler_0045E4E0"/> :
    /// <b>une balance a six plateaux</b>, et le plus gros decor de la tour.
    ///
    /// <para>
    /// <b>Il ne gouverne pas une entite mais six</b>, prises a la file dans la table des entites de
    /// scene (<c>+0x9c</c> de pas) a partir de la sienne. Les six se repartissent en deux camps,
    /// <b>les rangs pairs contre les rangs impairs</b> : 0, 2 et 4 d'un cote, 1, 3 et 5 de l'autre.
    /// Tout ce que la fonction fait a la hauteur d'un camp, elle le fait a l'oppose sur l'autre.
    /// </para>
    /// <para>
    /// <b>La hauteur de repos</b> est lue une seule fois, a la naissance : la hauteur du premier
    /// plateau en unites entieres, rangee dans <c>+0x10</c> — et <b>forcee a 1 si elle est
    /// nulle</b>, parce que <c>+0x10</c> nul est justement le marqueur de naissance. La balance
    /// n'a donc pas de position de repos au ras du sol.
    /// </para>
    /// <para>
    /// <b>Une variante ferme le mecanisme d'entree de jeu</b> : si <c>0x005de070</c> depasse
    /// <c>0x7cffff</c>, le corps pose <c>+0x14</c> a 1, <b>descend les plateaux 2 et 3 de 80
    /// unites</b>, met <c>+0x0a</c> a <c>0x0b</c> sur les six et <c>+0x05</c> a zero, puis rend la
    /// main pour de bon.
    /// </para>
    /// <para>
    /// <b>La pesee.</b> <see cref="CountPartyMembersMatching"/> (<c>FUN_00535000</c>, rayon
    /// <c>0x10</c>, bit <c>0x40000000</c>) est appelee <b>six fois</b>, une par plateau ; la somme
    /// de chaque camp devient un simple oui/non en <c>+0x98</c> et <c>+0x9a</c>. Quatre cas :
    /// </para>
    /// <list type="bullet">
    /// <item>les deux camps charges : <c>+0x14</c> passe a 1 et rien ne bouge ;</item>
    /// <item>aucun camp charge : <c>+0x16</c> retombe a zero et la balance <b>revient au
    /// repos</b> par pas de <c>0x4000</c> ;</item>
    /// <item>un seul camp charge : elle <b>penche de son cote</b> par pas de <c>0xc000</c>, trois
    /// fois plus vite, tant que le debattement n'a pas atteint <b>350 unites</b> (<c>0x15e</c>) de
    /// part et d'autre du repos, et <c>+0x16</c> passe a 1 au premier pas.</item>
    /// </list>
    /// <para>
    /// <b>La hauteur de reference est toujours celle du premier plateau</b> (<c>+0x20</c> de
    /// l'entite recue), jamais celle du plateau qu'on deplace : les six sont censes rester
    /// solidaires, et le binaire ne verifie pas qu'ils le sont.
    /// </para>
    /// <para>Une image sur trois, les six plateaux poussent un effet <c>0x3f</c> — la poussiere.</para>
    /// </summary>
    private static void TickTowerSeesaw_0045A630(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            short resting = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x10) = resting;
            if (resting == 0) *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 0;

            if (*Mem.Camera(0x005de070) > 0x7cffff)
            {
                *(short*)(e + 0x14) = 1;
                *(int*)(e + 0x1f4) -= 0x50000;          // Ghidra: param_1 + 500, le plateau 3
                *(int*)(e + 0x158) -= 0x50000;          // le plateau 2

                for (int i = 0; i < 6; i++) e[0x0a + i * 0x9c] = 0x0b;

                e[5] = 0;
                return;
            }

            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        if ((short)(CrtRand() % 3) == 0)                // Ghidra: FUN_00547140
        {
            byte* plate = e;
            for (int i = 6; i != 0; i--, plate += 0x9c)
                SpawnEffectNearCamera_00468110(plate, 0x3f, 0, -1);   // Ghidra: FUN_00468110
        }

        // Ghidra: FUN_00535000 six fois — les rangs pairs, puis les rangs impairs.
        int evens = CountPartyMembersMatching(e, 0x10, 0x40000000)
                  + CountPartyMembersMatching(e + 0x138, 0x10, 0x40000000)
                  + CountPartyMembersMatching(e + 0x270, 0x10, 0x40000000);
        *(short*)(e + 0x98) = (short)((short)evens == 0 ? 0 : 1);

        int odds = CountPartyMembersMatching(e + 0x9c, 0x10, 0x40000000)
                 + CountPartyMembersMatching(e + 0x1d4, 0x10, 0x40000000)
                 + CountPartyMembersMatching(e + 0x30c, 0x10, 0x40000000);
        *(short*)(e + 0x9a) = (short)((short)odds == 0 ? 0 : 1);

        short loadedEvens = *(short*)(e + 0x98);
        short loadedOdds = *(short*)(e + 0x9a);

        if (loadedEvens == 1 && loadedOdds == 1) *(short*)(e + 0x14) = 1;

        int resting1000 = *(short*)(e + 0x10) * 0x1000;

        if (loadedEvens == 0 && loadedOdds == 0)
        {
            *(short*)(e + 0x16) = 0;
            if (*(int*)(e + 0x20) < resting1000) TiltSeesaw_0045A630(e, 0x4000);
            if (resting1000 < *(int*)(e + 0x20)) TiltSeesaw_0045A630(e, -0x4000);
        }

        if (loadedEvens == 1 && loadedOdds == 0
            && (*(short*)(e + 0x10) - 0x15e) * 0x1000 < *(int*)(e + 0x20))
        {
            if (*(short*)(e + 0x16) == 0) *(short*)(e + 0x16) = 1;
            TiltSeesaw_0045A630(e, -0xc000);
        }

        if (loadedEvens == 0 && loadedOdds == 1
            && *(int*)(e + 0x20) < (*(short*)(e + 0x10) + 0x15e) * 0x1000)
        {
            if (*(short*)(e + 0x16) == 0) *(short*)(e + 0x16) = 1;
            TiltSeesaw_0045A630(e, 0xc000);
        }
    }

    /// <summary>
    /// Le pas de <see cref="TickTowerSeesaw_0045A630"/> : les rangs pairs montent de
    /// <paramref name="step"/>, les rangs impairs descendent d'autant. Le binaire ecrit ce bloc de
    /// six additions <b>quatre fois</b>, au signe et a l'amplitude pres.
    /// </summary>
    private static void TiltSeesaw_0045A630(byte* e, int step)
    {
        *(int*)(e + 0x020) += step;    // plateau 0
        *(int*)(e + 0x158) += step;    // plateau 2
        *(int*)(e + 0x290) += step;    // plateau 4

        *(int*)(e + 0x0bc) -= step;    // plateau 1
        *(int*)(e + 0x1f4) -= step;    // plateau 3
        *(int*)(e + 0x32c) -= step;    // plateau 5
    }

    // =====================================================================
    // LA CATEGORIE C DE LA TABLE 0x0056adc8 — PREMIER LOT (2026-09-17)
    //
    // Sept formes decompilees dans Blaze.c, plus la quatrieme sœur de FUN_00468110 que l'une
    // d'elles appelle. Aucune n'a de jumeau : chacune n'existe que dans son bloc de sous-zones.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045c000 @ 0x0045c000 — <b>la QUATRIEME sœur</b> de
    /// <see cref="SpawnEffectNearCamera_00468110"/>, <see cref="TryPlaySoundNearEntity"/> et
    /// <see cref="TryPublishCameraEffectNearEntity"/> : meme calcul de distance a la camera, meme
    /// rayon de <b>1800</b>, meme signature perdue par Ghidra.
    ///
    /// <para>
    /// La seule difference avec la premiere est la naissance : <c>FUN_0047a9d0</c>
    /// (<see cref="SpawnEffectEntity"/>) au lieu de <c>FUN_0047ac40</c>. Le desassemblage donne
    /// quatre parametres — apres deux <c>push</c>, le corps lit <c>[esp+10h]</c>, <c>[esp+14h]</c> et
    /// <c>[esp+18h]</c> — et c'est <c>param_2</c> qui recoit le <c>+ 0xC0</c>, pas le pointeur que la
    /// pseudo-C de Ghidra y met.
    /// </para>
    /// </summary>
    /// <returns>1 si l'entite etait assez proche pour que la naissance ait lieu, 0 sinon.</returns>
    private static ushort SpawnInheritedEffectNearCamera_0045C000(
        byte* entity, short subtype, short param3, short param4)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);

        int dx = (camera[0] >> 0xc) - (*(int*)(entity + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(entity + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(entity + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x708) return 0;

        SpawnEffectEntity(entity, (byte)(subtype + 0xc0), (byte)param3, param4);   // Ghidra: FUN_0047a9d0
        return 1;
    }

    /// <summary>
    /// Le pendant en ecriture de <see cref="ScriptFlagSet"/> : le numero <paramref name="index"/>
    /// designe un bit de la banque 0 (<c>0x005e96e0</c>), octet <c>n / 8</c> vers zero, bit
    /// <c>n % 8</c> masque a cinq bits. Les deux corps de la tour qui le posent l'ecrivent a
    /// l'identique.
    /// </summary>
    private static void SetScriptFlag(short index, bool on)
    {
        int cell = DivideTowardZeroBy8(index);
        int bit = (index - cell * 8) & 0x1f;

        if (on) Mem.s_StateBank0_005e96e0[(short)cell] |= (byte)(1 << bit);
        else Mem.s_StateBank0_005e96e0[(short)cell] &= (byte)~(1 << bit);
    }

    /// <summary>
    /// Ghidra: FUN_0045bf20 @ 0x0045bf20 — <b>le brasier qui bat et change de face</b>, sous-zones 0,
    /// 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// A la naissance il range son <c>+0x14</c> dans <c>+0x12</c> — <b>c'est une puissance de deux</b>,
    /// utilisee plus bas comme decalage — puis remet <c>+0x14</c>, <c>+0x9a</c> et <c>+0x16</c> a
    /// zero. Ensuite, a chaque image, le cap avance de <c>0x14</c> et repasse par zero au-dela de
    /// <c>0x1000</c>, et <b>la hauteur recoit le sinus de ce cap decale de <c>+0x12</c></b> : le
    /// brasier respire, d'une amplitude que sa fiche de placement fixe.
    /// </para>
    /// <para>
    /// <b>Deux regimes selon l'age.</b> Sous 31 images, il crache le sous-type <c>0x33</c> une image
    /// sur deux. Au-dela, une chance sur trente par image — ou d'office apres 90 images — il
    /// <b>bascule</b> : <c>+0x9a</c> recoit l'ancien <c>+0x16</c>, <c>0x3f</c> nait par
    /// <see cref="SpawnInheritedEffectNearCamera_0045C000"/>, le compteur repart a zero et
    /// <c>+0x16</c> passe de 0 a 1 ou de 1 a 0.
    /// </para>
    /// <para>
    /// <b>Son resultat est un reste de registre</b>, comme celui du verbe <c>0x00443CB0</c> : la
    /// fonction est declaree <c>uint</c> par Ghidra et ce que <c>EAX</c> porte au <c>ret</c> depend du
    /// chemin. Personne ne le lit — le repartiteur d'entites ignore la valeur de retour — donc il est
    /// laisse tel quel plutot que force a zero.
    /// </para>
    /// </summary>
    private static void TickBreathingBrazier_0045BF20(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                        // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(ushort*)(e + 0x12) = *(ushort*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x16) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x32) += 0x14;
        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));      // Ghidra: Ordinal_451
        (*(short*)(e + 0x14))++;
        ushort age = *(ushort*)(e + 0x14);

        *(int*)(e + 0x20) += lift << (e[0x12] & 0x1f);

        if ((short)age < 0x1f)
        {
            if ((age & 1) != 0) SpawnEffectNearCamera_00468110(e, 0x33, 0, -1);   // FUN_00468110
            return;
        }

        int roll = CrtRand();                                  // Ghidra: FUN_00547140
        if (roll % 0x1e != 1 && *(short*)(e + 0x14) <= 0x5a) return;

        *(short*)(e + 0x9a) = *(short*)(e + 0x16);
        SpawnInheritedEffectNearCamera_0045C000(e, 0x3f, 0, -1);             // Ghidra: FUN_0045c000
        *(short*)(e + 0x14) = 0;
        *(short*)(e + 0x16) = (short)(~e[0x16] & 1);
    }

    /// <summary>
    /// Ghidra: FUN_0045aa90 @ 0x0045aa90 — <b>la trappe qui bascule et secoue la tour</b>, sous-zones
    /// 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle ne fait rien si son numero de drapeau depasse 255</b> : tout le corps est sous
    /// <c>numero / 8 &lt; 0x20</c>, la borne de la banque 0. Le numero vit dans <c>+0x14</c>.
    /// </para>
    /// <para>
    /// L'etat 0 recopie le mot <c>+0x30</c> dans <c>+0x9a</c>, met <c>+0x30</c> a zero et rend la
    /// main : c'est un etat d'attente, l'entite y reste jusqu'a ce qu'un script pose son etat a 1.
    /// Des etats 1 a 6, la trappe pivote de <c>0xf0</c> par image — <b>dans le sens que
    /// <c>+0x12</c> dicte</b> : a <c>-1</c> elle s'ouvre (le cap augmente) et <b>efface</b> son
    /// drapeau, sinon elle se referme et le <b>pose</b>.
    /// </para>
    /// <para>
    /// A l'etat 13 elle tranche : si <c>+0x16</c> vaut 1 elle passe a l'etat <b>50</b>, la secousse ;
    /// sinon elle retombe a l'etat 0 et se verrouille (<c>0x40000000</c>). Dans les deux cas
    /// <c>+0x12</c> <b>bascule entre 0 et -1</b> par l'idiome <c>(x != 0) - 1</c> : le pivot suivant
    /// ira dans l'autre sens.
    /// </para>
    /// <para>
    /// L'etat 50 publie une secousse de camera (<see cref="TryPublishCameraEffectNearEntity"/>,
    /// amplitude <c>rand % 8 - 4</c>) pendant 60 images, puis publie un dernier zero et se verrouille.
    /// </para>
    /// </summary>
    private static void TickTiltingHatch_0045AA90(byte* e)
    {
        short flag = *(short*)(e + 0x14);
        if (DivideTowardZeroBy8(flag) >= 0x20) return;

        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            // Ghidra: `uVar4 = param_1[0xc]` puis l'ecriture de 16 bits a la meme adresse.
            uint parked = *(uint*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x9a) = (short)parked;
            return;
        }

        if (state < 7)
        {
            *(short*)(e + 0x10) = (short)(state + 1);

            if (*(short*)(e + 0x12) == -1)
            {
                *(short*)(e + 0x34) += 0xf0;
                SetScriptFlag(flag, false);
            }
            else
            {
                *(short*)(e + 0x34) -= 0xf0;
                SetScriptFlag(flag, true);
            }
        }

        if (*(short*)(e + 0x10) == 0x0d)
        {
            if (*(short*)(e + 0x16) == 1)
            {
                *(short*)(e + 0x10) = 0x32;
                *(int*)(e + 0x5c) = 0;
            }
            else
            {
                *(short*)(e + 0x10) = 0;
                *(uint*)e |= 0x40000000;
            }

            // Ghidra: `(x != 0) - 1` — zero devient -1, tout le reste devient 0.
            *(ushort*)(e + 0x12) = (ushort)((*(short*)(e + 0x12) != 0 ? 1 : 0) - 1);
        }

        if (*(short*)(e + 0x10) != 0x32) return;

        *(int*)(e + 0x5c) += 1;

        int jolt = CrtRand();                                  // Ghidra: FUN_00547140
        TryPublishCameraEffectNearEntity((uint*)e, 999, (short)(jolt % 8 - 4), 999, 0, 0);

        if (*(int*)(e + 0x5c) <= 0x3b) return;

        *(short*)(e + 0x10) = 0;
        TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);       // Ghidra: FUN_0041be60
        *(uint*)e |= 0x40000000;
    }

    /// <summary>
    /// Ghidra: FUN_0045ad20 @ 0x0045ad20 — <b>les deux jets de braise d'une meme bouche</b>,
    /// sous-zones 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Le meme mecanisme que le souffleur <c>0x0045CF70</c>, en plus court : le corps <b>se sert de
    /// lui-meme comme point d'emission</b>. Il met de cote sa position et son cap, tourne d'un angle
    /// tire entre <c>0x100</c> et <c>0x6ff</c>, avance de <b>60 unites</b> dans cette direction —
    /// <c>Z</c> en <b>negatif</b>, <c>X</c> en positif —, fait naitre le sous-type <c>0x2e</c>, puis
    /// remet tout. Deux fois, avec deux tirages independants.
    /// </para>
    /// <para>
    /// <b>Ghidra rend ici deux registres non affectes</b> (<c>unaff_BX</c>, <c>unaff_SI</c>) reecrits
    /// dans <c>+0x34</c> apres chaque jet. Ils ne sont jamais charges : ce sont les valeurs que
    /// l'appelant y avait laissees, donc <c>+0x34</c> est restaure a lui-meme. Transcrit comme une
    /// sauvegarde-restauration, ce qu'il est.
    /// </para>
    /// <para>Toutes les vingt images, la bouche joue <c>sons[0x11]</c>.</para>
    /// </summary>
    private static void TickTwinEmberJets_0045AD20(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int keptX = *(int*)(e + 0x1c);
        int keptZ = *(int*)(e + 0x24);
        short keptCourse = *(short*)(e + 0x32);
        short keptTilt = *(short*)(e + 0x34);

        SpitEmberJet_0045AD20(e);
        *(short*)(e + 0x32) = keptCourse;
        *(short*)(e + 0x34) = keptTilt;
        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x24) = keptZ;

        SpitEmberJet_0045AD20(e);
        *(short*)(e + 0x32) = keptCourse;
        *(short*)(e + 0x34) = keptTilt;
        (*(short*)(e + 0x12))++;
        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x24) = keptZ;

        if (*(short*)(e + 0x12) != 0x14) return;

        TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x11], 0x600, 0x800, 0, 0);   // FUN_00468080
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>Un jet de <see cref="TickTwinEmberJets_0045AD20"/> : la deviation du cap, les
    /// 60 unites d'avance et la naissance. Le binaire ecrit ce bloc deux fois, a l'identique.</summary>
    private static void SpitEmberJet_0045AD20(byte* e)
    {
        *(short*)(e + 0x32) += (short)(CrtRand() % 0x600 + 0x100);           // Ghidra: FUN_00547140

        short course = *(short*)(e + 0x32);
        *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(course) * -0x3c;  // Ghidra: Ordinal_451
        *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(course) * 0x3c;        // Ghidra: Ordinal_450

        SpawnEffectNearCamera_00468110(e, 0x2e, 0, -1);                      // Ghidra: FUN_00468110
    }

    /// <summary>
    /// Ghidra: FUN_0045ae90 @ 0x0045ae90 — <b>le bloc pousse dont la position vit dans <c>.data</c></b>,
    /// sous-zones 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Sa position n'est pas dans son creneau : elle est dans trois tables de <c>.data</c></b> —
    /// <c>0x00551660</c>, <c>0x00551680</c>, <c>0x005516a0</c>, indexees par son numero <c>+0x14</c>
    /// et exprimees en unites entieres. Le corps les lit en tete, travaille en unites de
    /// <c>0x1000</c>, et les reecrit en queue. C'est ce qui fait qu'un bloc <b>garde sa place d'une
    /// sous-zone a l'autre</b> : la table survit au creneau.
    /// </para>
    /// <para>
    /// <b>Son etat vient du bit <c>+0x14</c> de l'octet <c>0x005e96ff</c></b>, celui du radeau et du
    /// rassemblement. Bit eteint : role de dessin 7, et une image sur deux il fait naitre <c>0x2f</c>
    /// 100 a 129 unites plus haut que lui — <b>sans bouger</b>, la hauteur est remise aussitot. Bit
    /// pose : role 8, et c'est <c>0x34</c> qui nait, a sa place.
    /// </para>
    /// <para>
    /// <see cref="MoveSceneEntity_00455B30"/> avec le masque <c>0xa8c0</c> fait le deplacement ;
    /// ensuite, <b>si l'une des deux vitesses <c>+0x50</c> ou <c>+0x58</c> depasse <c>0x2000</c> en
    /// valeur absolue</b>, le raclement <c>sons[0x16]</c> part et un delai de 20 images l'empeche de
    /// repartir. Le delai descend d'une image par tour et ne passe jamais sous zero.
    /// </para>
    /// <para>Les deux vitesses et <c>+0x54</c> sont remises a zero a la toute fin : le bloc ne garde
    /// aucune inertie d'une image sur l'autre.</para>
    /// </summary>
    private static void TickPushableBlock_0045AE90(byte* e)
    {
        short index = *(short*)(e + 0x14);

        *(int*)(e + 0x1c) = *Mem.EntityParam(0x00551660 + index * 4) << 0xc;
        *(int*)(e + 0x20) = *Mem.EntityParam(0x00551680 + index * 4) << 0xc;
        *(int*)(e + 0x24) = *Mem.EntityParam(0x005516a0 + index * 4) << 0xc;

        // Ghidra: `1 << (index & 0x1f)` contre DAT_005e96ff, l'octet 0x1f de la banque 0.
        if (((byte)(1 << (*(short*)(e + 0x14) & 0x1f)) & Mem.s_StateBank0_005e96e0[0x1f]) == 0)
        {
            int keptHeight = *(int*)(e + 0x20);
            e[0x0a] = 7;

            *(int*)(e + 0x20) += (CrtRand() % 0x1e + 100) * 0x1000;          // Ghidra: FUN_00547140
            if ((CrtRand() & 1) == 0) SpawnEffectNearCamera_00468110(e, 0x2f, 0, -1);

            *(int*)(e + 0x20) = keptHeight;
        }
        else
        {
            e[0x0a] = 8;
            if ((CrtRand() & 1) == 0) SpawnEffectNearCamera_00468110(e, 0x34, 0, -1);
        }

        MoveSceneEntity_00455B30(e, 0xa8c0);                                 // Ghidra: FUN_00455b30

        int slideX = *(int*)(e + 0x50);
        if (slideX < 0) slideX = -slideX;

        bool grinding = slideX >= 0x2001;
        if (!grinding)
        {
            int slideZ = *(int*)(e + 0x58);
            if (slideZ < 0) slideZ = -slideZ;
            grinding = slideZ >= 0x2001;
        }

        if (grinding && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x16], 0x800, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0x14;
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        short back = *(short*)(e + 0x14);
        *Mem.EntityParam(0x00551660 + back * 4) = *(int*)(e + 0x1c) >> 0xc;
        *Mem.EntityParam(0x00551680 + back * 4) = *(int*)(e + 0x20) >> 0xc;
        *Mem.EntityParam(0x005516a0 + back * 4) = *(int*)(e + 0x24) >> 0xc;

        TestTriggerVolumes_00534CD0((uint*)e);                               // Ghidra: FUN_00534cd0

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0045b3b0 @ 0x0045b3b0 — <b>le piston qui monte lentement et retombe d'un coup</b>,
    /// sous-zones 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Quatre etats en boucle. <b>L'etat 0 est une attente aleatoire</b> : une chance sur quarante
    /// par image, et quarante images au plus. L'etat 1 monte de <c>0x4000</c> par image, <b>en
    /// emportant ce qui est dessus</b> (<see cref="AddEntityVelocity_00454580"/>), en cliquetant
    /// (<c>sons[0x1d]</c>) toutes les six images, jusqu'a depasser <c>0x208000</c>.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est la chute, et elle accelere</b> : la vitesse part de 4 et gagne 4 par image.
    /// Au contact du plancher <c>0x10e000</c> le piston y est cale net, <b>deux sons partent</b> — le
    /// meme <c>sons[0x1c]</c> a deux hauteurs et deux gains, ce qui donne l'impact double —, et
    /// <see cref="StrikePartyMembers_0045DA40"/> frappe pour 10. L'etat 3 attend 30 images et
    /// renvoie a l'etat 1 : <b>l'attente aleatoire de l'etat 0 n'a lieu qu'une fois</b>.
    /// </para>
    /// </summary>
    private static void TickSlammingPiston_0045B3B0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                                     // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            int roll = CrtRand();                                            // Ghidra: FUN_00547140
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x28 || (short)(roll % 0x28) == 1) *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x14))++;
            if (*(short*)(e + 0x14) > 5)
            {
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1d], 0x400, 0x1000, 0, 0);
                *(short*)(e + 0x14) = 0;
            }

            *(int*)(e + 0x20) += 0x4000;
            AddEntityVelocity_00454580(e, 0, 0x4000, 0);                     // Ghidra: FUN_00454580

            if (*(int*)(e + 0x20) > 0x207fff)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 4;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 4;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;
            AddEntityVelocity_00454580(e, 0, *(short*)(e + 0x12) * -0x1000, 0);

            if (*(int*)(e + 0x20) < 0x10e001)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                *(int*)(e + 0x20) = 0x10e000;

                // Le meme son, deux fois, a deux hauteurs et deux gains : l'impact double.
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1c], 0x800, 0x1000, 0, 0);
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1c], 0x653, 0x0a00, 0, 0);

                StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);                 // Ghidra: FUN_0045da40
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) == 0x1e) *(short*)(e + 0x10) = 1;
    }

    /// <summary>
    /// Ghidra: FUN_0045b7e0 @ 0x0045b7e0 — <b>le pilier qui s'enfonce dans le sol et remonte</b>,
    /// sous-zones 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Il commence par verifier qu'il n'a pas deja servi</b> : si son drapeau de banque 0 est
    /// pose et qu'il est encore a l'etat 0, il saute directement a l'etat <b>99</b> — l'etat mort —
    /// et se place 475 unites plus bas, la ou il a fini la fois precedente. C'est ainsi qu'un
    /// mecanisme a usage unique survit a un rechargement de sous-zone.
    /// </para>
    /// <para>
    /// Sinon l'etat 0 fait naitre <b>26</b> fois le sous-type <c>0x31</c> — avec <c>+0x12</c>
    /// temporairement a 8, la variante que l'enfant recoit —, joue le son <c>0x0b</c>, met de cote
    /// son inclinaison <c>+0x34</c> dans <c>+0x5c</c> et sa hauteur de depart dans <c>+0x16</c>, puis
    /// <b>pose son drapeau</b>.
    /// </para>
    /// <para>
    /// L'etat 1 descend, de plus en plus vite (<c>+0x12</c> gagne une unite par image), en emportant
    /// ce qui est dessus et en s'inclinant de 10 par image. <b>A la 33e image exactement</b>, il
    /// fixe son echelle a <c>0x10cc</c> sur les trois axes, joue <c>sons[0x23]</c> et crache
    /// <b>31</b> fois le sous-type <c>0x32</c>. Il passe a l'etat 2 quand il est 600 unites sous son
    /// depart.
    /// </para>
    /// <para>
    /// L'etat 2 remonte de <c>0x3000</c> par image en se redressant de 7, et s'arrete
    /// <b>475 unites</b> sous le depart : l'inclinaison revient a celle qu'il avait, le verrou
    /// <c>0x40000000</c> tombe et l'etat passe a 99.
    /// </para>
    /// </summary>
    private static void TickSinkingPillar_0045B7E0(byte* e)
    {
        short flag = *(short*)(e + 0x14);

        if (ScriptFlagSet(flag) && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 99;
            *(int*)(e + 0x20) -= 0x1db000;
        }

        ClearEntityVelocity_00460FC0(e);                                     // Ghidra: FUN_00460fc0

        if ((*(uint*)e & 0x40000000) != 0 || *(short*)(e + 0x10) == 99) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x12) = 8;
            for (int i = 0x1a; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x31, 1, -1);
            *(short*)(e + 0x12) = 0;

            *(int*)(e + 0x5c) = *(short*)(e + 0x34);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);

            TryPlaySoundNearEntity((uint*)e, 0x0b, 0x400, 0x1000, 0, 0);     // Ghidra: FUN_00468080
            SetScriptFlag(*(short*)(e + 0x14), true);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) = 0x10cc;
            *(short*)(e + 0x2c) = 0x10cc;

            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;
            AddEntityVelocity_00454580(e, 0, *(short*)(e + 0x12) * -0x1000, 0);
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 0x21)
            {
                *(short*)(e + 0x28) = 0x10cc;
                *(short*)(e + 0x2a) = 0x10cc;
                *(short*)(e + 0x2c) = 0x10cc;

                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x23], 0x400, 0x1000, 0, 0);
                for (int i = 0x1f; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x32, 1, -1);
            }

            *(short*)(e + 0x34) += 10;

            if (*(int*)(e + 0x20) >> 0xc < *(short*)(e + 0x16) - 600) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x20) += 0x3000;
        AddEntityVelocity_00454580(e, 0, 0x3000, 0);
        *(short*)(e + 0x34) -= 7;

        if (*(short*)(e + 0x16) - 0x1db >= *(int*)(e + 0x20) >> 0xc) return;

        *(short*)(e + 0x34) = (short)*(int*)(e + 0x5c);
        *(uint*)e |= 0x40000000;
        *(short*)(e + 0x10) = 99;
    }

    /// <summary>
    /// Ghidra: FUN_0045fba0 @ 0x0045fba0 — <b>le voile qui immobilise le groupe</b>, sous-zones 3, 7
    /// et 9. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Sa hauteur est celle d'une autre entite</b>, moins 100 unites : la derniere ligne du corps
    /// la relit dans le creneau <c>+0x14</c> crans plus loin, a chaque image et quel que soit l'etat.
    /// Le voile colle donc a un porteur qu'il ne connait que par un decalage.
    /// </para>
    /// <para>
    /// <b>Le mecanisme vit dans deux bits de <c>0x005e96f6</c></b> (banque 0, octet <c>0x16</c>) :
    /// <c>0x10</c> arme la montee, <c>0x20</c> l'autre moitie du mecanisme. Les etats 1 et 2 les
    /// consultent, et l'etat 0 les eteint tous les deux en relancant le script de l'entite
    /// (<see cref="RunEntityScript"/> avec le mode 1).
    /// </para>
    /// <para>
    /// <b>Et il pose le bit 3 du mot d'etat des quatre membres</b> pendant qu'il monte. Le bit 3 est
    /// celui sur lequel <c>TickPartyMember</c> rend la main tout de suite (<c>*slot &amp; 0x4000008</c>)
    /// : le groupe est fige tant que le voile n'a pas fini. Les quatre creneaux sont ecrits par leur
    /// adresse en dur — <c>0x005e93c0</c>, <c>0x005e945c</c>, <c>0x005e94f8</c>, <c>0x005e9594</c> —,
    /// donc sans tenir compte de la taille reelle du groupe.
    /// </para>
    /// <para>
    /// L'echelle <c>+0x2a</c> monte de <c>+0x12</c>, lui-meme de <c>0x40</c> par image, jusqu'a
    /// <c>0x1000</c> ; alors le groupe est rendu, le script repasse en mode 0 et l'etat passe a 2.
    /// L'etat 2 attend que les deux bits retombent pour tout recommencer. Enfin, <b>au-dessus de
    /// <c>0xf9ffff</c> de hauteur</b>, le voile se retracte de 100 par image et se reinitialise en
    /// atteignant zero — une sortie de secours independante des etats.
    /// </para>
    /// </summary>
    private static void TickFreezingVeil_0045FBA0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0;
            RunEntityScript(s_Unknown_005e97cc, e, 1);                       // Ghidra: FUN_00513dd0
            Mem.s_StateBank0_005e96e0[0x16] &= 0xcf;                         // Ghidra: DAT_005e96f6
        }

        if (*(short*)(e + 0x10) == 1 && (Mem.s_StateBank0_005e96e0[0x16] & 0x10) != 0)
        {
            for (int slot = 0; slot < 4; slot++) *(uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c) |= 8;

            *(short*)(e + 0x2a) += *(short*)(e + 0x12);
            *(short*)(e + 0x12) += 0x40;

            if (*(short*)(e + 0x2a) > 0x1000)
            {
                *(short*)(e + 0x2a) = 0x1000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;

                for (int slot = 0; slot < 4; slot++)
                    *(uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c) &= 0xfffffff7;

                RunEntityScript(s_Unknown_005e97cc, e, 0);
            }
        }

        if (*(short*)(e + 0x10) == 2 && (Mem.s_StateBank0_005e96e0[0x16] & 0x30) == 0)
        {
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            RunEntityScript(s_Unknown_005e97cc, e, 1);
            Mem.s_StateBank0_005e96e0[0x16] &= 0xcf;
        }

        if (*(int*)(e + 0x20) > 0xf9ffff)
        {
            *(short*)(e + 0x2a) -= 100;
            *(short*)(e + 0x10) = 3;

            if (*(short*)(e + 0x2a) < 0)
            {
                *(short*)(e + 0x2a) = 0;
                RunEntityScript(s_Unknown_005e97cc, e, 1);
                Mem.s_StateBank0_005e96e0[0x16] &= 0xcf;
            }
        }

        // Ghidra: la derniere ligne, hors de tout etat — la hauteur du porteur, moins 100 unites.
        *(int*)(e + 0x20) = *(int*)(e + 0x20 + *(short*)(e + 0x14) * 0x9c) - 0x64000;
    }

    // =====================================================================
    // LA CATEGORIE C DE LA TABLE 0x0056adc8 — SECOND LOT, ET LA CATEGORIE EST VIDE (2026-09-17)
    //
    // Neuf formes, dix corps. La paire 0x0045DD00 / 0x0045FD40 partage son corps : le diff ne rend
    // qu'un octet, le sous-type engendre (0x39 contre 0x38), que le recensement ne reconnait pas
    // comme un jumeau parce que ce n'est pas une adresse.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0045a4d0</c> — <b>le plateau a bascule qui arme un des quatre verrous</b>,
    /// sous-zones 0, 1, 2 et 4. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045A4D0.asm</c>.
    ///
    /// <para>
    /// <b>Il compte qui est monte dessus</b> (<see cref="CountPartyMembersMatching"/>, rayon
    /// <c>0x10</c>) : au premier arrivant il allume le bit <c>+0x14</c> de l'octet
    /// <c>0x005e9701</c> — le banc des quatre verrous de la salle — et passe a l'etat 2 ; le
    /// plateau vide remet son temoin <c>+0x16</c> a zero, mais <b>n'eteint pas le verrou</b>.
    /// </para>
    /// <para>
    /// <b>Les quatre verrous sont lus a l'envers</b> : le compte part de zero et s'incremente pour
    /// chaque bit <b>absent</b> parmi <c>0x10</c>, <c>0x20</c>, <c>0x40</c> et <c>0x80</c>. Zero
    /// veut donc dire « les quatre sont poses », et c'est ce qui fait passer le plateau a l'etat 3 —
    /// l'etat bloque, role de dessin 2.
    /// </para>
    /// <para>
    /// L'etat 2 arme un compte a rebours de 40 images et repasse a 1 ; l'etat 1 le decompte
    /// <b>jusqu'a -2999</b> (le plancher <c>0xf448</c>), et des qu'il passe sous zero le plateau
    /// remonte : role 1, <b>verrou efface</b>, une etincelle <c>0x37</c> une image sur six et le
    /// grincement <c>0x1b</c> une image sur treize.
    /// </para>
    /// </summary>
    private static void TickLatchPlate_0045A4D0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x98) = 0;
        }

        short riders = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // FUN_00535000

        if (riders != 0)
        {
            if (*(short*)(e + 0x16) == 0)
            {
                *(short*)(e + 0x10) = 2;
                Mem.s_StateBank0_005e96e0[0x21] |= (byte)(1 << *(short*)(e + 0x14));
                *(short*)(e + 0x16) = 1;
            }
        }
        else
        {
            *(short*)(e + 0x16) = 0;
        }

        // Ghidra: quatre `test cl,2^n ; jne` qui SAUTENT l'increment — le compte est celui des
        // verrous MANQUANTS.
        byte latches = Mem.s_StateBank0_005e96e0[0x21];
        int missing = 0;
        if ((latches & 0x10) == 0) missing = 1;
        if ((latches & 0x20) == 0) missing++;
        if ((latches & 0x40) == 0) missing++;
        if ((latches & 0x80) == 0) missing++;

        if ((short)missing == 0) *(short*)(e + 0x10) = 3;

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) < -2999) *(short*)(e + 0x12) = -2999;   // Ghidra: 0xf448

            if (*(short*)(e + 0x12) < 0)
            {
                e[0x0a] = 1;
                Mem.s_StateBank0_005e96e0[0x21] &= (byte)~(1 << *(short*)(e + 0x14));

                if ((short)(CrtRand() % 6) == 0)
                    SpawnEffectNearCamera_00468110(e, 0x37, 0, -1);          // Ghidra: FUN_00468110

                if (*(short*)(e + 0x98) == 0)
                    TryPlaySoundNearEntity((uint*)e, 0x1b, 0x400, 0x600, 0, 0);

                (*(short*)(e + 0x98))++;
                if (*(short*)(e + 0x98) >= 0x0d) *(short*)(e + 0x98) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            e[0x0a] = 2;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x28;
        }

        if (*(short*)(e + 0x10) == 3) e[0x0a] = 2;
    }

    /// <summary>
    /// Ghidra: <c>0x0045b540</c> — <b>le mur qui se derobe</b>, sous-zones 0, 1, 2 et 4.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045B540.asm</c>.
    ///
    /// <para>
    /// Trente images apres son declenchement — et seulement si son <c>+0x14</c> vaut 1 — il joue le
    /// son <c>0x0b</c> et crache <b>vingt et un</b> eclats du sous-type <c>0x31</c>, variante 1, en
    /// deux bouquets de variante <c>+0x12</c> : 8 puis 2.
    /// </para>
    /// <para>
    /// <b>Le bit 2 de <c>0x005e96f4</c> memorise que les deux bouquets sont deja partis.</b> A la
    /// premiere fois il n'est pas pose : les deux bouquets sortent, puis le bit est allume. Aux
    /// fois suivantes, <b>les bits 0 et 1 de <c>0x005e96fe</c> choisissent</b> quelle variante sort,
    /// et un seul bouquet de vingt et un part. Le compteur est sauvegarde et restaure autour du
    /// bloc, puisqu'il sert de variante.
    /// </para>
    /// <para>
    /// A la 30e image le mur bascule dans sa chute : il tombe de <c>+0x12</c> unites par image,
    /// <c>+0x12</c> gagnant deux, en emportant ce qui est dessus d'une vitesse <b>constante</b>
    /// <c>-(+0x16)</c> ; il pivote de <c>0x14</c> et de 4 sur ses deux axes, et se verrouille sous
    /// <c>-0xfa0000</c>.
    /// </para>
    /// </summary>
    private static void TickCollapsingWall_0045B540(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                                     // Ghidra: FUN_00460fc0
        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x14) == 1 && *(short*)(e + 0x12) > 0x1d)
            {
                TryPlaySoundNearEntity((uint*)e, 0x0b, 0x400, 0x1000, 0, 0);

                short kept = *(short*)(e + 0x12);

                if ((Mem.s_StateBank0_005e96e0[0x14] & 4) != 0)
                {
                    if ((Mem.s_StateBank0_005e96e0[0x1e] & 1) != 0) *(short*)(e + 0x12) = 8;
                    if ((Mem.s_StateBank0_005e96e0[0x1e] & 2) != 0) *(short*)(e + 0x12) = 2;
                    for (int i = 0x15; i != 0; i--)
                        SpawnEffectNearCamera_00468110(e, 0x31, 1, -1);
                }
                else
                {
                    *(short*)(e + 0x12) = 8;
                    for (int i = 0x15; i != 0; i--)
                        SpawnEffectNearCamera_00468110(e, 0x31, 1, -1);

                    *(short*)(e + 0x12) = 2;
                    for (int i = 0x15; i != 0; i--)
                        SpawnEffectNearCamera_00468110(e, 0x31, 1, -1);

                    Mem.s_StateBank0_005e96e0[0x14] |= 4;
                }

                *(short*)(e + 0x12) = kept;
            }

            if (*(short*)(e + 0x12) == 0x1e)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x12) += 2;
        *(int*)(e + 0x20) -= *(short*)(e + 0x12) << 0xc;

        AddEntityVelocity_00454580(e, 0, -*(short*)(e + 0x16) << 0xc, 0);    // Ghidra: FUN_00454580

        *(short*)(e + 0x34) += 0x14;
        *(short*)(e + 0x32) += 4;

        if (*(int*)(e + 0x20) < unchecked((int)0xff060000)) *(uint*)e |= 0x40002000;
    }

    /// <summary>
    /// Ghidra: <c>0x0045e300</c> — <b>le second bloc pousse, dont la position vit dans SIX
    /// OCTETS</b>, sous-zones 3, 7 et 9. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045E300.asm</c>.
    ///
    /// <para>
    /// Meme idee que <see cref="TickPushableBlock_0045AE90"/> — la position survit au creneau — mais
    /// rangee autrement : non dans trois tables de <c>.data</c> indexees par un numero, mais dans
    /// <b>six octets consecutifs de la banque 0</b>, <c>0x005e9706</c> a <c>0x005e970b</c>, deux par
    /// axe. Il n'y a donc qu'un seul bloc de ce genre par sous-zone.
    /// </para>
    /// <para>
    /// <b>Le bit 1 de <c>0x005e9705</c> dit si la position a deja ete semee.</b> A la toute premiere
    /// image de la sous-zone il est eteint : le bloc y ecrit sa position de fiche et l'allume. Aux
    /// entrees suivantes, c'est la paire d'octets qui gagne, et la fiche est ignoree.
    /// </para>
    /// <para>
    /// <b>La reconstitution passe par une extension de signe explicite</b> : le binaire assemble
    /// <c>(haut &lt;&lt; 8) | bas</c>, decale de 16 a gauche puis de 4 a droite — soit le mot signe
    /// remis en unites de <c>0x1000</c>. Un bloc au-dela de ±32 unites de mille serait replie ;
    /// transcrit tel quel.
    /// </para>
    /// <para>Le reste est celui de son cousin : <see cref="MoveSceneEntity_00455B30"/> (masque
    /// <c>0x8882</c> ici), les volumes de declenchement, le raclement <c>sons[0x16]</c> passe
    /// <c>0x2000</c> de vitesse avec son delai de 20 images, et les trois vitesses remises a zero.</para>
    /// </summary>
    private static void TickBytePushableBlock_0045E300(byte* e)
    {
        byte* seeded = Mem.s_StateBank0_005e96e0 + 0x25;                     // Ghidra: 0x005e9705
        byte* pos = Mem.s_StateBank0_005e96e0 + 0x26;                        // 0x005e9706..0x0b

        if ((*seeded & 2) == 0)
        {
            *seeded |= 2;

            int x = *(int*)(e + 0x1c) >> 0xc;
            int y = *(int*)(e + 0x20) >> 0xc;
            int z = *(int*)(e + 0x24) >> 0xc;

            pos[1] = (byte)x; pos[0] = (byte)(x >> 8);
            pos[3] = (byte)y; pos[2] = (byte)(y >> 8);
            pos[5] = (byte)z; pos[4] = (byte)(z >> 8);
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x98) = 0;

            // Ghidra: `shl …,10h ; sar …,4` — le mot signe, remis en unites de 0x1000.
            *(int*)(e + 0x1c) = ((pos[0] << 8) | pos[1]) << 0x10 >> 4;
            *(int*)(e + 0x20) = ((pos[2] << 8) | pos[3]) << 0x10 >> 4;
            *(int*)(e + 0x24) = ((pos[4] << 8) | pos[5]) << 0x10 >> 4;
        }

        MoveSceneEntity_00455B30(e, 0x8882);                                 // Ghidra: FUN_00455b30
        TestTriggerVolumes_00534CD0((uint*)e);                               // Ghidra: FUN_00534cd0

        int slideX = *(int*)(e + 0x50);
        if (slideX < 0) slideX = -slideX;

        bool grinding = slideX > 0x2000;
        if (!grinding)
        {
            int slideZ = *(int*)(e + 0x58);
            if (slideZ < 0) slideZ = -slideZ;
            grinding = slideZ > 0x2000;
        }

        if (grinding && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x16], 0x800, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0x14;
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        pos[0] = (byte)(*(int*)(e + 0x1c) >> 0x14);
        pos[1] = (byte)(*(int*)(e + 0x1c) >> 0x0c);
        pos[2] = (byte)(*(int*)(e + 0x20) >> 0x14);
        pos[3] = (byte)(*(int*)(e + 0x20) >> 0x0c);
        pos[4] = (byte)(*(int*)(e + 0x24) >> 0x14);
        pos[5] = (byte)(*(int*)(e + 0x24) >> 0x0c);

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>Ghidra: <c>0x0045dd00</c> — la bouche a vapeur des sous-zones 5, 6, 8 et 10.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045DD00.asm</c>.</summary>
    private static void EntityHandler_0045DD00(byte* e) => TickSteamVent_0045DD00(e, 0x39);

    /// <summary>Ghidra: <c>0x0045fd40</c> — la meme pour les sous-zones 3, 7 et 9. <b>Toute la
    /// difference tient dans un octet</b> : <c>push 38h</c> au lieu de <c>push 39h</c>, le sous-type
    /// engendre. Le recensement ne la voit pas comme un jumeau parce que ce n'est pas une adresse —
    /// c'en est un.</summary>
    private static void EntityHandler_0045FD40(byte* e) => TickSteamVent_0045DD00(e, 0x38);

    /// <summary>
    /// Le corps des deux : <b>une bouche qui s'ouvre quand la camera arrive a sa hauteur et souffle
    /// par a-coups</b>.
    ///
    /// <para>
    /// A la naissance elle <b>se pose 180 unites en avant de son cap</b> (retourne d'un demi-tour),
    /// arme son gabarit de collision a <c>0xa000</c>, retient sa hauteur dans <c>+0x60</c> et
    /// <c>+0x64</c>, appelle <see cref="TryPlaceEntity"/> — <b>puis remet sa position d'origine</b> :
    /// le deplacement n'a servi qu'a faire poser le gabarit ailleurs qu'a l'aplomb de la bouche.
    /// </para>
    /// <para>
    /// <b>Elle s'ouvre sur la hauteur de la camera</b> : tant que l'ecart entre
    /// <c>0x005de06c</c> et sa propre hauteur tient entre -400 et +100 unites, la bouche est
    /// « armee » ; sortie de cette fenetre, elle se referme. L'ouverture est une echelle
    /// <c>+0x2c</c> qui monte de <c>0x100</c> par image jusqu'a <c>0x1000</c>, la fermeture une
    /// descente de <c>0x80</c>.
    /// </para>
    /// <para>
    /// <b>Grande ouverte, elle souffle une image sur quatre-vingts</b> : quatre naissances du
    /// sous-type <paramref name="subtype"/>, emises depuis un point 180 unites en avant, avec
    /// <c>+0x14</c> mis a 0, 1, 2 puis 3 — la meme mecanique de variante que le souffleur
    /// <see cref="TickTowerBlower_0045CF70"/>. Les sept champs touches sont remis apres.
    /// </para>
    /// </summary>
    private static void TickSteamVent_0045DD00(byte* e, short subtype)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int keptX = *(int*)(e + 0x1c);
            int keptZ = *(int*)(e + 0x24);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x30) = 0;

            short ahead = (short)(*(short*)(e + 0x32) + 0x800);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(ahead) * 180;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(ahead) * 180;

            *(ushort*)(e + 0x82) = 0xa000;
            *(ushort*)(e + 0x86) = 0xa000;
            *(ushort*)(e + 0x8a) = 0xa000;
            *(ushort*)(e + 0x8e) = 0xa000;

            int height = *(int*)(e + 0x20);
            *(int*)(e + 0x64) = height;
            *(int*)(e + 0x60) = height;

            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // FUN_005330d0

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x24) = keptZ;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int gap = (*Mem.Camera(0x005de06c) >> 0xc) - (*(int*)(e + 0x20) >> 0xc);

        if (gap >= -0x190 && gap <= 100)
        {
            if (*(short*)(e + 0x12) == 0)
            {
                *(short*)(e + 0x12) = 1;
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x28], 0x400, 0x1800, 0, 0);
            }
        }
        else if (*(short*)(e + 0x12) == 2)
        {
            *(short*)(e + 0x12) = 3;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            *(short*)(e + 0x2c) += 0x100;
            if (*(short*)(e + 0x2c) >= 0x1000)
            {
                *(short*)(e + 0x2c) = 0x1000;
                *(short*)(e + 0x12) = 2;
            }
        }

        if (*(short*)(e + 0x12) == 2 && (short)(CrtRand() % 0x50) == 0)
        {
            int keptAX = *(int*)(e + 0x50);
            int keptAY = *(int*)(e + 0x54);
            int keptAZ = *(int*)(e + 0x58);
            short keptCourse = *(short*)(e + 0x32);
            int keptX = *(int*)(e + 0x1c);
            int keptY = *(int*)(e + 0x20);
            int keptZ = *(int*)(e + 0x24);

            short ahead = (short)(keptCourse + 0x800);
            *(short*)(e + 0x32) = ahead;
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(ahead) * 180;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(ahead) * 180;

            for (short variant = 0; variant <= 3; variant++)
            {
                short keptVariant = *(short*)(e + 0x14);
                short keptPeriod = *(short*)(e + 0x16);

                *(short*)(e + 0x14) = variant;
                *(short*)(e + 0x16) = 0x0a;
                SpawnEffectNearCamera_00468110(e, subtype, 0, -1);           // Ghidra: FUN_00468110

                *(short*)(e + 0x14) = keptVariant;
                *(short*)(e + 0x16) = keptPeriod;
            }

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x20) = keptY;
            *(int*)(e + 0x24) = keptZ;
            *(short*)(e + 0x32) = keptCourse;
            *(int*)(e + 0x50) = keptAX;
            *(int*)(e + 0x54) = keptAY;
            *(int*)(e + 0x58) = keptAZ;
        }

        if (*(short*)(e + 0x12) != 3) return;

        *(short*)(e + 0x2c) -= 0x80;
        if (*(short*)(e + 0x2c) > 0) return;

        *(short*)(e + 0x2c) = 0;
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0045b030</c> — <b>la bouche a vapeur qui se tait quand on s'eloigne</b>,
    /// sous-zones 0, 1, 2 et 4. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045B030.asm</c>.
    ///
    /// <para>
    /// La cousine de <see cref="TickSteamVent_0045DD00"/>, a trois differences. <b>Elle se pose a
    /// 80 unites</b> et non 180, et passe en plus par
    /// <see cref="FinalizeEntityPlacement2"/>.
    /// </para>
    /// <para>
    /// <b>Sa garde est une distance de camera sur DEUX axes</b>, prise en valeur absolue et comparee
    /// a son propre <c>+0x14</c> : si l'un des deux ecarts depasse cette portee <b>et</b> que la
    /// portee est positive, la bouche ne fait rien du tout. Une portee nulle ou negative la rend
    /// donc inconditionnelle.
    /// </para>
    /// <para>
    /// <b>Et sa cadence a deux seuils</b> : elle souffle des que son compte depasse <b>la moitie</b>
    /// de sa periode <c>+0x16</c> avec une chance sur trente, et d'office quand il l'atteint
    /// entiere.
    /// </para>
    /// <para>
    /// Les quatre naissances sont celles du souffleur, avec <c>+0x16</c> force a <b>8</b>. Le binaire
    /// ecrit la boucle <b>deux fois</b>, sous un test <c>+0x14 == 0</c> dont les deux branches sont
    /// identiques instruction pour instruction : du code duplique par le compilateur, sans
    /// difference a conserver.
    /// </para>
    /// </summary>
    private static void TickGuardedSteamVent_0045B030(byte* e)
    {
        int keptX = *(int*)(e + 0x1c);
        int keptZ = *(int*)(e + 0x24);

        int gapX = (*Mem.Camera(0x005de068) >> 0xc) - (keptX >> 0xc);
        if (gapX < 0) gapX = (keptX >> 0xc) - (*Mem.Camera(0x005de068) >> 0xc);

        int gapZ = (*Mem.Camera(0x005de070) >> 0xc) - (keptZ >> 0xc);
        if (gapZ < 0) gapZ = (keptZ >> 0xc) - (*Mem.Camera(0x005de070) >> 0xc);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            short ahead = (short)(*(short*)(e + 0x32) + 0x800);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(ahead) * 80;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(ahead) * 80;

            *(ushort*)(e + 0x82) = 0xa000;
            *(ushort*)(e + 0x86) = 0xa000;
            *(ushort*)(e + 0x8a) = 0xa000;
            *(ushort*)(e + 0x8e) = 0xa000;

            int height = *(int*)(e + 0x20);
            *(int*)(e + 0x64) = height;
            *(int*)(e + 0x60) = height;

            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // FUN_005330d0
            FinalizeEntityPlacement2((uint*)e);                                  // FUN_00533940

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x24) = keptZ;
        }

        short reach = *(short*)(e + 0x14);
        if ((gapX > reach || gapZ > reach) && reach > 0) return;

        (*(short*)(e + 0x12))++;

        short period = *(short*)(e + 0x16);
        bool due = (short)(CrtRand() % 0x1e) == 1
                   && *(short*)(e + 0x12) > (period - (period >> 31)) >> 1;

        if (!due && *(short*)(e + 0x12) < period) return;

        int keptAX = *(int*)(e + 0x50);
        int keptAY = *(int*)(e + 0x54);
        int keptAZ = *(int*)(e + 0x58);
        short keptCourse = *(short*)(e + 0x32);
        int keptY = *(int*)(e + 0x20);
        keptX = *(int*)(e + 0x1c);
        keptZ = *(int*)(e + 0x24);

        *(short*)(e + 0x12) = 0;
        short course = (short)(keptCourse + 0x800);
        *(short*)(e + 0x32) = course;

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 80;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 80;

        // Ghidra: deux boucles identiques sous `cmp [esi+14h],0` — code duplique par le compilateur.
        for (short variant = 0; variant <= 3; variant++)
        {
            short keptVariant = *(short*)(e + 0x14);
            short keptPeriod = *(short*)(e + 0x16);

            *(short*)(e + 0x14) = variant;
            *(short*)(e + 0x16) = 8;
            SpawnEffectNearCamera_00468110(e, 0x39, 0, -1);                  // Ghidra: FUN_00468110

            *(short*)(e + 0x14) = keptVariant;
            *(short*)(e + 0x16) = keptPeriod;
        }

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
        *(short*)(e + 0x32) = keptCourse;
        *(int*)(e + 0x50) = keptAX;
        *(int*)(e + 0x54) = keptAY;
        *(int*)(e + 0x58) = keptAZ;
    }

    /// <summary>
    /// Ghidra: <c>0x0045e860</c> — <b>le maitre de la salle des gardes</b>, sous-zones 3, 7 et 9.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045E860.asm</c>.
    ///
    /// <para>
    /// <b>Il gouverne les autres entites de scene par leur genre.</b> Un balayage des <c>0x20</c>
    /// creneaux cherche les entites de genre <b>4</b> : celles qui sont actives sans
    /// <c>0x40000000</c> prennent <c>0x4000</c> et peignent le fond en <b>rouge</b>
    /// (<c>0xfa</c>, 100, 100) ; celles qui l'ont deja le peignent en <b>gris</b> (<c>0x78</c>) et
    /// arment le retrait du maitre.
    /// </para>
    /// <para>
    /// <b>Puis il commute toutes les AUTRES</b> : si au moins une entite de genre 4 attendait, les
    /// entites d'un autre genre prennent le bit <c>0x2000</c> ; sinon elles le perdent. Le maitre
    /// est donc l'interrupteur d'une salle entiere.
    /// </para>
    /// <para>
    /// Son retrait (<c>+0x16 == 1</c>) fait naitre un <c>0x36</c> par image et rapetisse son echelle
    /// de <b>201</b> par image, en jouant <c>sons[0x2f]</c> tant que le bit 0 de <c>+0x28</c> est
    /// eteint ; a zero il se verrouille et passe a l'etat 2. Et <b>il respire toujours</b> : le cap
    /// avance de <c>0x44</c> et la hauteur recoit le sinus decale de <c>+0x12</c>, comme le brasier.
    /// </para>
    /// <para>
    /// <b>Le signal <c>+0x14</c> est un declencheur a usage unique</b> : non nul, il fait sortir trois
    /// <c>0x36</c> d'un coup et sonner <c>sons[0x2a]</c>, puis se remet a zero. Personne d'autre dans
    /// la zone n'emploie ce champ ainsi.
    /// </para>
    /// </summary>
    private static void TickGuardroomMaster_0045E860(byte* e)
    {
        int waiting = -1;
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            uint flags = *(uint*)scene;

            if ((flags & 0x80000000) != 0 && scene[0x0c] == 4 && (flags & 0x40000000) == 0)
            {
                *(uint*)scene = flags | 0x4000;
                SetSceneBackdrop(0xfa, 100, 100, 2);
                waiting = i;
            }

            flags = *(uint*)scene;
            if ((flags & 0x80000000) != 0 && scene[0x0c] == 4 && (flags & 0x40000000) != 0)
            {
                SetSceneBackdrop(0x78, 0x78, 0x78, 2);
                if (*(short*)(e + 0x16) == 0) *(short*)(e + 0x16) = 1;
            }
        }

        scene = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            uint flags = *(uint*)scene;
            if ((flags & 0x80000000) == 0 || scene[0x0c] == 4 || (flags & 0x40000000) != 0) continue;

            *(uint*)scene = waiting != -1 ? flags | 0x2000 : flags & 0xffffdfff;
        }

        if (*(short*)(e + 0x16) == 1)
        {
            if ((e[0x28] & 1) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x2f], 0x400, 0x1800, 0, 0);
                *(uint*)e |= 0x4000;
            }
            else
            {
                *(uint*)e &= 0xffffbfff;
            }

            SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);                  // Ghidra: FUN_00468110

            *(short*)(e + 0x28) -= 0xc9;
            *(short*)(e + 0x2c) -= 0xc9;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(uint*)e |= 0x4000;
                *(short*)(e + 0x16) = 2;
            }
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));      // Ghidra: 0x00545a1c
        *(int*)(e + 0x20) += lift << (e[0x12] & 0x1f);

        if (*(short*)(e + 0x14) == 0) return;

        for (int i = 0; i < 3; i++) SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);
        TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x2a], 0x400, 0x1000, 0, 0);
        *(short*)(e + 0x14) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0045c2e0</c> — creneau <b>9</b> du bloc <c>0x00556da8</c>, sous-zones 5, 6, 8 et
    /// 10 : <b>le maitre de salle qui rend la garde invulnerable</b>. <b>Absente de
    /// <c>Blaze.c</c></b> ; dump sous <c>Legacy/Raw/Blaze/OldTower/FUN_0045C2E0.asm</c>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickGuardroomMaster_0045E860"/> instruction pour instruction, plus trois
    /// gestes</b> (le <c>diff</c> des deux relevés normalises ne rend que ceux-la) ; la queue — le
    /// retrait, la respiration, le declencheur <c>+0x14</c> — est la meme, sons compris.
    /// </para>
    /// <para>
    /// <b>1. Il bascule deux sous-zones.</b> Deux mini-scripts de six octets sont batis sur la pile,
    /// <c>{0, 3, 0, 1, 0, v}</c> et <c>{0, 3, 1, 1, 0, v}</c>, et passes a <c>FUN_00514020</c> — une
    /// enveloppe du <b>gestionnaire de l'opcode 8</b> de l'interpreteur
    /// (<see cref="ScriptOpEnterSubArea"/>, <c>FUN_00515610</c>), le meme procede que
    /// <see cref="TickGroundContact"/> avec l'opcode 10. Ils posent l'octet <c>+1</c> — l'indice de
    /// vue — des fiches des sous-zones <b>0 et 1</b> de la zone courante a <c>v</c> : <b>1</b> tant
    /// qu'un garde de genre 4 veille, <b>0</b> quand l'un d'eux est fige. Si l'une des deux est la
    /// sous-zone courante, la vue bascule sur-le-champ. Les deux octets <c>v</c> sont reecrits a
    /// chaque passage ; le mot de resultat n'est jamais relu.
    /// </para>
    /// <para>
    /// <b>2. Tant qu'un garde veille, toutes les autres entites de scene sont invulnerables</b> :
    /// leur bloc de statistiques prend <b>30 000</b> en <c>+0x32</c> et en <c>+0x3a</c> — la
    /// defense magique que lit <see cref="ResolveSpellHit_0053A2B0"/> —, et le bit <c>2</c> de leur
    /// <c>+0x3b</c> se pose en plus du <c>0x2000</c> du maitre ordinaire.
    /// </para>
    /// <para>
    /// <b>3. Sinon, les deux valeurs reviennent de la fiche de modele</b> (<c>+0x32</c> et
    /// <c>+0x3a</c> du troisieme bloc de <see cref="ResolveEntityBlocks"/>) et le bit <c>2</c>
    /// tombe. La restauration a lieu <b>a chaque image</b> ou aucun garde ne veille, pas seulement a
    /// la bascule.
    /// </para>
    /// </summary>
    private static void TickWardingGuardroomMaster_0045C2E0(byte* e)
    {
        // Ghidra: [esp+0x20] et [esp+0x28] — les deux instructions, [esp+0x10] le resultat.
        byte* enterArea0 = stackalloc byte[6];
        byte* enterArea1 = stackalloc byte[6];
        enterArea0[0] = 0; enterArea0[1] = 3; enterArea0[2] = 0; enterArea0[3] = 1; enterArea0[4] = 0; enterArea0[5] = 0;
        enterArea1[0] = 0; enterArea1[1] = 3; enterArea1[2] = 1; enterArea1[3] = 1; enterArea1[4] = 0; enterArea1[5] = 0;
        uint result;

        int waiting = -1;
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            uint flags = *(uint*)scene;

            if ((flags & 0x80000000) != 0 && scene[0x0c] == 4 && (flags & 0x40000000) == 0)
            {
                *(uint*)scene = flags | 0x4000;
                enterArea0[5] = 1;
                enterArea1[5] = 1;
                ScriptOpEnterSubArea(enterArea0, &result);                     // Ghidra: FUN_00514020
                ScriptOpEnterSubArea(enterArea1, &result);                     // Ghidra: FUN_00514020
                SetSceneBackdrop(0xfa, 100, 100, 2);
                waiting = i;
            }

            flags = *(uint*)scene;
            if ((flags & 0x80000000) != 0 && scene[0x0c] == 4 && (flags & 0x40000000) != 0)
            {
                enterArea0[5] = 0;
                enterArea1[5] = 0;
                ScriptOpEnterSubArea(enterArea0, &result);                     // Ghidra: FUN_00514020
                ScriptOpEnterSubArea(enterArea1, &result);                     // Ghidra: FUN_00514020
                SetSceneBackdrop(0x78, 0x78, 0x78, 2);
                if (*(short*)(e + 0x16) == 0) *(short*)(e + 0x16) = 1;
            }
        }

        scene = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            uint flags = *(uint*)scene;
            if ((flags & 0x80000000) == 0 || scene[0x0c] == 4 || (flags & 0x40000000) != 0) continue;

            // Ghidra: FUN_00538980 — le binaire prete l'emplacement de son propre argument comme
            // premiere sortie ; `edi` garde l'entite, rien n'est perdu.
            ResolveEntityBlocks(scene, out byte* stats, out _, out byte* model);

            if (waiting != -1)
            {
                *(short*)(stats + 0x32) = 30000;
                *(short*)(stats + 0x3a) = 30000;
                scene[0x3b] |= 2;
                *(uint*)scene |= 0x2000;
            }
            else
            {
                *(short*)(stats + 0x32) = *(short*)(model + 0x32);
                *(short*)(stats + 0x3a) = *(short*)(model + 0x3a);
                scene[0x3b] &= 0xfd;
                *(uint*)scene &= 0xffffdfff;
            }
        }

        if (*(short*)(e + 0x16) == 1)
        {
            if ((e[0x28] & 1) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x2f], 0x400, 0x1800, 0, 0);
                *(uint*)e |= 0x4000;
            }
            else
            {
                *(uint*)e &= 0xffffbfff;
            }

            SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);                  // Ghidra: FUN_00468110

            *(short*)(e + 0x28) -= 0xc9;
            *(short*)(e + 0x2c) -= 0xc9;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(uint*)e |= 0x4000;
                *(short*)(e + 0x16) = 2;
            }
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));      // Ghidra: 0x00545a1c
        *(int*)(e + 0x20) += lift << (e[0x12] & 0x1f);

        if (*(short*)(e + 0x14) == 0) return;

        for (int i = 0; i < 3; i++) SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);
        TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x2a], 0x400, 0x1000, 0, 0);
        *(short*)(e + 0x14) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0045d750</c> — <b>le pendule qui fauche</b>, sous-zones 5, 6, 8 et 10.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0045D750.asm</c>. Le plus gros decor du lot.
    ///
    /// <para>
    /// <b>Il tire son sens de depart a pile ou face</b> : <c>+0x10</c> vaut 1 ou 2 selon un bit de
    /// hasard, et <c>+0x14</c> — la butee haute — est echange avec <c>+0x34</c>, l'angle de bras.
    /// Le bras part d'un angle tire dans les <c>0x6a4</c> sous la butee.
    /// </para>
    /// <para>
    /// <b>Son cap oscille toujours</b>, quel que soit l'etat : <c>+0x32</c> vaut le sinus de
    /// <c>+0x16</c> divise par 8, ajoute a l'orientation de depart retenue dans <c>+0x44</c>, et
    /// <c>+0x16</c> avance de <c>0x40</c> par image.
    /// </para>
    /// <para>
    /// <b>La descente (etat 1) accelere de 15 par image</b> et frappe de deux facons.
    /// <see cref="StrikePartyMembers_0045DA40"/> pour 10 a chaque image ; et surtout, passe
    /// <b>1000 unites</b> sous la butee, un balayage des quatre membres : le pendule se transporte
    /// a 150 unites d'un cap decale de <c>-0x400</c>, et tout membre a moins de 50 unites de ce
    /// point qui n'est pas deja en clignotement perd <b>10 % de ses points de vie maximum</b> par
    /// <see cref="ApplyPercentDamage_005397B0"/>, genre 9, et repart pour 60 images de clignotement.
    /// </para>
    /// <para>
    /// A <b>1792 unites</b> sous la butee c'est l'impact : <c>sons[0x26]</c>, quatre a neuf eclats
    /// du sous-type <c>0x3a</c> emis depuis le meme point decale, et le passage a l'etat 2 — la
    /// remontee, de <c>0x3c</c> par image, qui relance un cycle en atteignant la butee.
    /// </para>
    /// <para>
    /// <b>Le pendule ecrit son propre <c>+0x32</c> pour se servir de sonde</b> et le remet aussitot,
    /// exactement comme le souffleur. Il le fait <b>dans la boucle</b>, une fois par membre.
    /// </para>
    /// </summary>
    private static void TickScythePendulum_0045D750(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int toss = CrtRand() % 2;                                        // Ghidra: FUN_00547140
            short arm = *(short*)(e + 0x34);
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = (short)(toss + 1);

            short top = *(short*)(e + 0x14);
            *(short*)(e + 0x16) = top;
            *(short*)(e + 0x14) = arm;

            int drop = CrtRand() % 0x6a4;
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x44) = *(short*)(e + 0x32);
            *(short*)(e + 0x34) = (short)(*(short*)(e + 0x14) - drop);
        }

        // L'oscillation du cap, hors de tout etat.
        int sway = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x16));      // Ghidra: 0x00545a1c
        *(short*)(e + 0x16) += 0x40;
        *(short*)(e + 0x32) = (short)((sway >> 3) + *(int*)(e + 0x44));
        if (*(short*)(e + 0x16) > 0x1000) *(short*)(e + 0x16) -= 0x1000;

        if (*(short*)(e + 0x10) == 1)
        {
            StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);                     // Ghidra: FUN_0045da40

            if (*(short*)(e + 0x9a) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, 2, 0x320, 0x1000, 0, 0);
                *(short*)(e + 0x9a) = 1;
            }

            short speed = *(short*)(e + 0x12);
            *(short*)(e + 0x34) -= speed;
            *(short*)(e + 0x12) = (short)(speed + 0x0f);

            if (*(short*)(e + 0x34) <= *(short*)(e + 0x14) - 0x3e8)
            {
                byte* member = Inp.s_SlotFlags_005e93c0;

                for (int i = 0; i < 4; i++, member += 0x9c)
                {
                    uint flags = *(uint*)member;
                    if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                    short keptCourse = *(short*)(e + 0x32);
                    *(short*)(e + 0x32) = (short)(keptCourse - 0x400);

                    int bladeX = (*(int*)(e + 0x1c)
                                  + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 150) >> 0xc;
                    int bladeZ = (*(int*)(e + 0x24)
                                  + Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 150) >> 0xc;
                    *(short*)(e + 0x32) = keptCourse;

                    int dx = bladeX - (*(int*)(member + 0x1c) >> 0xc);
                    int dz = bladeZ - (*(int*)(member + 0x24) >> 0xc);

                    if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= 0x32) continue;
                    if (*(short*)(member + 0x96) != 0) continue;

                    *(uint*)member |= 0x40;
                    ApplyPercentDamage_005397B0((uint*)member, 10, 9);       // Ghidra: FUN_005397b0
                    *(uint*)member |= 0x40;
                    *(short*)(member + 0x96) = 0x3c;
                }
            }

            if (*(short*)(e + 0x34) <= *(short*)(e + 0x14) - 0x700)
            {
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x26], 0x400, 0x1000, 0, 0);

                short top = *(short*)(e + 0x14);
                int keptY = *(int*)(e + 0x20);
                short keptCourse = *(short*)(e + 0x32);
                int keptZ = *(int*)(e + 0x24);
                int keptX = *(int*)(e + 0x1c);

                *(short*)(e + 0x34) = (short)(top - 0x700);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x32) = (short)(keptCourse - 0x400);

                short course = *(short*)(e + 0x32);
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 150;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 150;

                short shards = (short)(CrtRand() % 6 + 3);
                if (shards >= 0)
                {
                    for (int i = shards + 1; i != 0; i--)
                        SpawnEffectNearCamera_00468110(e, 0x3a, 0, -1);      // Ghidra: FUN_00468110
                }

                *(int*)(e + 0x1c) = keptX;
                *(int*)(e + 0x20) = keptY;
                *(int*)(e + 0x24) = keptZ;
                *(short*)(e + 0x32) = keptCourse;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x34) += 0x3c;
        if (*(short*)(e + 0x34) <= *(short*)(e + 0x14)) return;

        *(short*)(e + 0x10) = 1;
        *(short*)(e + 0x12) = 0x64;
        *(short*)(e + 0x9a) = 0;
    }

    // =====================================================================
    // LES DEUX DERNIERS DE LA CATEGORIE C, ET LES DEUX SEULS DE BLAZE.C (2026-09-17)
    //
    // 0x0045BB10 detourne la camera vers un bloc a lui ; 0x0045F7E0 ouvre une boite de dialogue et
    // se sert de lui-meme comme sonde sur les 32 entites de scene. Avec eux, la categorie C de la
    // vieille tour est vide.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045bb10 @ 0x0045bb10 — <b>la plateforme qui monte et detourne la camera</b>,
    /// sous-zones 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle prend la camera en main</b>, et c'est tout son interet. Le temps de la montee, les
    /// trois pointeurs de camera — <c>0x005edebc</c> (la position), <c>0x005e9ae0</c> (les angles) et
    /// <c>0x005eca94</c> (la cible) — sont rebranches sur <b>son propre bloc</b>, <c>0x00571680</c>,
    /// <c>0x00571668</c> et <c>0x00571670</c>. Elle y recopie sa position de vue (<c>+0xb8</c>,
    /// <c>+0xbc</c>, <c>+0xc0</c>), ses deux angles (<c>+0xcc</c> releve de 200, <c>+0xce</c>
    /// brouille sur douze bits) et son roulis <c>+0xd0</c>, et retient l'ancienne cible pour la
    /// rendre.
    /// </para>
    /// <para>
    /// <b>Le retour est une remise en place exacte</b> : la cible retenue redevient l'ecart
    /// <c>0x00564658</c> par rapport a <c>0x005de088</c>, les trois pointeurs repartent sur les
    /// globales de monde, <c>FUN_005037b0</c> recale le mode et
    /// <see cref="UnfreezeAllEntities"/> rend la main aux entites. C'est le meme chemin que le
    /// verbe de scenette <c>0x00504200</c>, ecrit a l'envers.
    /// </para>
    /// <para>
    /// <b>Le mot <c>0x00571690</c> est l'etat du mecanisme</b> : le bit <c>0x100</c> dit que la
    /// camera est detournee, le bit <c>0x20</c> que la plateforme est arrivee. Les deux sont poses
    /// et effaces d'ici.
    /// </para>
    /// <para>
    /// <b>Pendant les images 1 a 59 la cible en profondeur glisse d'un quart vers <c>+0x5c</c></b>,
    /// arme a <b>850</b> : c'est le recul de camera qui accompagne la montee. Le retour n'a lieu
    /// qu'a la 60e image <b>et</b> apres la 80e image de vie du mecanisme.
    /// </para>
    /// <para>
    /// La montee elle-meme est ordinaire : declenchee par le bit <c>0x80</c> de <c>0x005e96f4</c>,
    /// elle accelere d'une unite par image en emportant ce qui est dessus, s'arrete a la hauteur
    /// zero, y joue <c>sons[0x21]</c> <b>si la camera a bien ete detournee</b>, puis, arrivee, seme
    /// un <c>0x3a</c> une image sur trois sur un cap qui tourne de <c>0x14</c>.
    /// </para>
    /// <para>
    /// <b>Son compteur d'age deborde a 2000 et repart a 30</b>, pas a zero : le seuil de 80 images
    /// reste donc franchi pour toujours une fois passe.
    /// </para>
    /// </summary>
    private static void TickCameraLift_0045BB10(byte* e)
    {
        (*(short*)(e + 0x14))++;
        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
        if (*(short*)(e + 0x14) > 2000) *(short*)(e + 0x14) = 0x1e;

        int* state = Mem.EntityState(0x00571690);

        if ((*state & 0x100) != 0)
        {
            if (*(short*)(e + 0x9a) == 0)
            {
                if (*(short*)(e + 0x14) < 0x15)
                {
                    *(short*)(e + 0x16) = 0;
                }
                else
                {
                    FreezeAllEntities();                                     // Ghidra: FUN_00535300

                    int* view = Mem.EntityState(0x00571680);
                    short* angles = (short*)Mem.EntityStateBytes(0x00571668);
                    int* aim = Mem.EntityState(0x00571670);

                    view[0] = *(int*)(e + 0xb8);
                    *(uint*)e &= 0xfbffffff;
                    *(uint*)(e + 0x9c) &= 0xfbffffff;
                    *(int*)(e + 0x5c) = 0x352;
                    view[1] = *(int*)(e + 0xbc);
                    view[2] = *(int*)(e + 0xc0);

                    angles[0] = (short)(*(short*)(e + 0xcc) + 200);
                    angles[1] = (short)(CrtRand() % 0x1000 + *(short*)(e + 0xce));
                    angles[2] = *(short*)(e + 0xd0);

                    int* previousAim = *(int**)Mem.Entity(0x005eca94);
                    aim[0] = previousAim[0];
                    aim[1] = previousAim[1];
                    aim[2] = previousAim[2];

                    *(byte**)Mem.Entity(0x005eca94) = (byte*)aim;
                    *(byte**)Mem.Arena(0x005e9ae0) = (byte*)angles;
                    *(byte**)Mem.Entity(0x005edebc) = (byte*)view;

                    *(short*)(e + 0x16) = 1;
                }
            }

            if (*(short*)(e + 0x9a) > 0 && *(short*)(e + 0x9a) < 0x3c)
            {
                int* aim = Mem.EntityState(0x00571670);
                aim[2] += (*(int*)(e + 0x5c) - aim[2]) >> 2;
            }

            if (*(short*)(e + 0x9a) == 0x3c && *(short*)(e + 0x14) > 0x50)
            {
                int* aim = Mem.EntityState(0x00571670);
                int* slide = (int*)(s_WorldMapCamera_00564650 + 8);

                slide[0] = aim[0];
                slide[1] = aim[1] - *Mem.Camera(0x005de08c);
                *(byte**)Mem.Entity(0x005eca94) = (byte*)Mem.Camera(0x005de078);
                slide[2] = aim[2] - *Mem.Camera(0x005de090);
                *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
                *(byte**)Mem.Entity(0x005edebc) = (byte*)Mem.Camera(0x005de068);

                CameraModeApply_005037E0(0);                                 // Ghidra: FUN_005037b0
                UnfreezeAllEntities();                                       // Ghidra: FUN_00535400
                *state &= unchecked((int)0xfffffeff);
            }

            (*(short*)(e + 0x9a))++;
        }

        if ((*state & 0x20) != 0)
        {
            *(short*)(e + 0x10) = 2;
            *(int*)(e + 0x20) = 0;
        }

        if ((Mem.s_StateBank0_005e96e0[0x14] & 0x80) == 0) return;           // Ghidra: DAT_005e96f4

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 1;
            *(short*)(e + 0x9a) = 0;
            *state |= 0x100;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;
            AddEntityVelocity_00454580(e, 0, *(short*)(e + 0x12) * -0x1000, 0);
            int height = *(int*)(e + 0x20);
            (*(short*)(e + 0x12))++;

            if (height < 1)
            {
                *(int*)(e + 0x20) = 0;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *state |= 0x20;

                if (*(short*)(e + 0x16) == 1)
                    TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x21], 0x400, 0x1000, 0, 0);
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x12) += 0x14;
        if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) -= 0x1000;

        if (CrtRand() % 3 != 1) return;

        short keptCourse = *(short*)(e + 0x32);
        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        *(short*)(e + 0x32) = *(short*)(e + 0x12);
        SpawnEffectNearCamera_00468110(e, 0x3a, 0, 9);                       // Ghidra: FUN_00468110

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
        *(short*)(e + 0x32) = keptCourse;
    }

    /// <summary>
    /// Ghidra: FUN_0045f7e0 @ 0x0045f7e0 — <b>la passerelle qui plie sous le poids et parle</b>,
    /// sous-zones 3, 7 et 9. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle s'eclaircit de 10 par canal a chaque image</b>, avant tout le reste — le seul decor
    /// de la tour a le faire vers le haut.
    /// </para>
    /// <para>
    /// <b>Son usure est un compte, et il vient des entites de scene.</b> Une fois haute et
    /// declenchee (bit <c>0x10</c> de <c>0x005e96f6</c>), elle monte de <c>0x2000</c> par image et
    /// balaie les <b>32</b> creneaux d'entite de scene : toute entite active, non figee, portant le
    /// bit 1, a moins de 100 unites d'elle — <b>hauteur relevee de 100</b> — lui ajoute <b>4</b> a
    /// son compte <c>+0x5c</c> et lui fait cracher <c>+0x5c + 1</c> etincelles du sous-type
    /// <c>0x37</c>. Un delai <c>+0x44</c> de 30 images empeche deux comptes de suite.
    /// </para>
    /// <para>
    /// <b>Passe 9, le compte la fait ceder</b> : teinte rouge (<c>0x3c</c>, <c>0x1e</c>,
    /// <c>0x1e</c>), compte remis a zero, bit <c>0x20</c> pose et bit <c>0x10</c> retire dans
    /// <c>0x005e96f6</c>, et — une seule fois, garde par <c>+0x98</c> — <b>une boite de dialogue</b>
    /// attachee au premier membre disponible, prise a l'entree <c>0x48</c> des libelles de zone.
    /// </para>
    /// <para>
    /// <b>La chute est en escalier</b> : la passerelle descend de <c>+0x12</c> unites par image,
    /// <c>+0x12</c> partant de 20 et perdant une unite <b>toutes les trois images</b>. Arrivee a
    /// vitesse nulle, elle rearme le bit <c>0x10</c> ; passee sous <c>-0x320000</c>, elle remonte
    /// d'un coup a <c>0x64000</c> et les deux bits tombent.
    /// </para>
    /// <para>
    /// <b>Le choix du parleur est la variante inlinee de <see cref="ActivePartySlot_00472770"/></b> :
    /// meme masque <c>0x50010000</c>, meme balayage, mais le repli n'est pas l'indice 4 — c'est le
    /// chef lui-meme. Ecrite ici plutot qu'appelee, parce que ce n'est pas la meme fonction.
    /// </para>
    /// </summary>
    private static void TickSaggingWalkway_0045F7E0(byte* e)
    {
        BrightenEntityTint_00404DE0(e, 10, 10, 10);                          // Ghidra: FUN_00404de0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x98) = 0;
            *(int*)(e + 0x44) = 0;
        }

        *(int*)(e + 0x44) -= 1;
        if (*(int*)(e + 0x44) < 0) *(int*)(e + 0x44) = 0;

        if (*(int*)(e + 0x5c) > 9 && *(int*)(e + 0x20) < 0x1004000)
        {
            e[0x38] = 0x3c;
            e[0x39] = 0x1e;
            e[0x3a] = 0x1e;
            *(int*)(e + 0x5c) = 0;
            Mem.s_StateBank0_005e96e0[0x16] = (byte)((Mem.s_StateBank0_005e96e0[0x16] & 0xef) | 0x20);
            *(short*)(e + 0x12) = 0x14;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            if (*(short*)(e + 0x98) == 0)
            {
                byte* speaker = Inp.s_SlotFlags_005e93c0 + SpeakingMemberSlot_0045F7E0() * 0x9c;
                byte* text = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0x48);
                BindMenuLabels(text, speaker, s_WorldMapDescriptor_005633b0, 0);
                *(short*)(e + 0x98) = 1;
            }
        }

        if (*(short*)(e + 0x10) != 1) return;

        if ((Mem.s_StateBank0_005e96e0[0x16] & 0x10) != 0)
        {
            (*(short*)(e + 0x16))++;
            if (*(short*)(e + 0x16) > 100) *(short*)(e + 0x16) = 0x15;

            if (*(short*)(e + 0x16) > 0x14)
            {
                *(int*)(e + 0x20) += 0x2000;

                byte* scene = Mem.Entity(0x005ecaa0);
                for (int i = 0x20; i != 0; i--, scene += 0x9c)
                {
                    uint flags = *(uint*)scene;
                    if ((flags & 0x80000000) == 0) continue;
                    if ((flags & 0x40000000) != 0) continue;
                    if ((flags & 2) == 0) continue;

                    int dx = (*(int*)(scene + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                    int dy = ((*(int*)(scene + 0x20) - *(int*)(e + 0x20)) + 0x64000) >> 0xc;
                    int dz = (*(int*)(scene + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                    if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 100) continue;
                    if (*(int*)(e + 0x44) != 0) continue;

                    *(int*)(e + 0x44) = 0x1e;
                    int wear = *(int*)(e + 0x5c) + 4;
                    *(int*)(e + 0x5c) = wear;

                    // Ghidra: `sVar11 = 0 ; do { … } while (sVar11 <= +0x5c)` — donc +0x5c + 1 fois.
                    if (wear >= 0)
                    {
                        for (short n = 0; n <= wear; n++)
                            SpawnEffectNearCamera_00468110(e, 0x37, 0, -1);  // Ghidra: FUN_00468110
                    }
                }
            }

            if (*(int*)(e + 0x20) > 0x1004000) *(int*)(e + 0x20) = 0x1004000;
        }

        if ((Mem.s_StateBank0_005e96e0[0x16] & 0x20) == 0) return;

        int height = *(int*)(e + 0x20) + *(short*)(e + 0x12) * -0x1000;
        *(int*)(e + 0x20) = height;

        if (height < -0x320000)
        {
            Mem.s_StateBank0_005e96e0[0x16] &= 0xcf;
            *(int*)(e + 0x20) = 0x64000;
        }

        (*(short*)(e + 0x14))++;
        if (*(short*)(e + 0x14) != 3) return;

        (*(short*)(e + 0x12))--;
        *(short*)(e + 0x14) = 0;

        if (*(short*)(e + 0x12) == 0)
            Mem.s_StateBank0_005e96e0[0x16] = (byte)((Mem.s_StateBank0_005e96e0[0x16] & 0xdf) | 0x10);
    }

    /// <summary>
    /// Le choix du parleur de <see cref="TickSaggingWalkway_0045F7E0"/> : le chef
    /// (<c>0x005de538</c>) s'il est vivant et non masque par <c>0x50010000</c>, sinon le premier des
    /// quatre qui l'est — et <b>le chef de nouveau</b> si aucun ne convient.
    ///
    /// <para>C'est <see cref="ActivePartySlot_00472770"/> inlinee, au repli pres : la fonction
    /// nommee rend l'indice <b>4</b> quand le balayage echoue, celle-ci rend le chef. La difference
    /// est reelle, donc les deux coexistent.</para>
    /// </summary>
    private static short SpeakingMemberSlot_0045F7E0()
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;
        short leader = (byte)Mem.s_Unknown_005de538;

        uint flags = *(uint*)(slots + leader * 0x9c);
        if ((flags & 0x80000000) != 0 && (flags & 0x50010000) == 0) return leader;

        short chosen = leader;
        for (short i = 0; i < 4; i++)
        {
            uint f = *(uint*)(slots + i * 0x9c);
            if ((f & 0x80000000) == 0 || (f & 0x50010000) != 0) continue;

            // Ghidra: `uVar4 = 99 ; uVar3 = uVar4 + 1` — le compteur saute a 100, donc la boucle
            // s'arrete sur le PREMIER emplacement qui convient.
            chosen = i;
            break;
        }

        return chosen;
    }

    // =====================================================================
    // CRENEAU 14 — LES DEUX WAGONNETS DE LA TOUR (2026-09-17)
    //
    // 0x0045C870 (bloc 0x00556da8, sous-zones 5, 6, 8, 10) et 0x0045ECC0 (bloc 0x00557428,
    // sous-zones 3, 7, 9) sont LE WAGONNET DE LA MINE (`MineMecanismes.cs`, 0x004538C0) retouche.
    // Les deux sont decompilees dans Blaze.c et ne different entre elles que par la tenue de leurs
    // echelles ; relevés Legacy/Raw/Blaze/OldTower/FUN_0045C870.asm et FUN_0045ECC0.asm.
    //
    // Ce que la tour change au wagonnet de la mine, sur les deux :
    //
    //   - IL SONNE : le son 0x2b de la zone au depart (hauteur 0x180), le son 0x2c a l'arrivee
    //     (hauteur 0x300), par FUN_00468080 ;
    //   - +0x98 MARQUE LE FREINAGE : pose a 1 des que la distance +0x14 est depassee, rendu a 0 a
    //     l'arret et a la naissance ;
    //   - LA HAUTEUR 600 NE GELE PLUS RIEN : le test `+0x20 & 0xfffff000 == 0x258000` de la mine a
    //     disparu des deux branches de detachement ;
    //   - IL S'INTERROMPT DES QU'UN MEMBRE MANQUE AU CONTACT, y compris quand plus AUCUN ne l'est :
    //     la mine exigeait qu'il en reste au moins un (`blocked != 0`). Sauter du wagonnet en
    //     marche le fait donc disparaitre, ce que la mine ne faisait pas.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0045c870 @ 0x0045c870 — creneau <b>14</b> du bloc <c>0x00556da8</c>, sous-zones
    /// 5, 6, 8 et 10. <b>Le wagonnet qui regrandit a l'arret</b> : ses echelles ne reviennent pas
    /// vers <c>0x1000</c> a chaque image, mais de 200 par image <b>seulement pendant qu'il attend
    /// son groupe</b>, avant de se poser a l'extremite la plus proche. Voir
    /// <see cref="TickTowerRailCart"/>.
    /// </summary>
    private static void EntityHandler_0045C870(byte* e) => TickTowerRailCart(e, easesScalesEachFrame: false);

    /// <summary>
    /// Ghidra: FUN_0045ecc0 @ 0x0045ecc0 — creneau <b>14</b> du bloc <c>0x00557428</c>, sous-zones
    /// 3, 7 et 9. <b>Le wagonnet qui regrandit toujours</b> : ses echelles reviennent vers
    /// <c>0x1000</c> par huitiemes a chaque image, hors disparition, comme celles de la mine. Voir
    /// <see cref="TickTowerRailCart"/>.
    /// </summary>
    private static void EntityHandler_0045ECC0(byte* e) => TickTowerRailCart(e, easesScalesEachFrame: true);

    /// <summary>
    /// Le corps commun de <see cref="EntityHandler_0045C870"/> et <see cref="EntityHandler_0045ECC0"/>.
    /// La charpente est celle de <see cref="TickRailCart"/> — naissance qui joue la course d'avance,
    /// repli sur l'extremite la plus proche de la camera tant que le groupe n'est pas au complet a
    /// bord, aller (etat 1) et retour (etat 3), arrivees 4 et 5, disparitions 6 et 7 — et ses trois
    /// feuilles <see cref="RailCartStep"/>, <see cref="RailCartSnapToAnchor"/> et
    /// <see cref="RailCartFade"/> s'appliquent telles quelles.
    /// </summary>
    /// <param name="easesScalesEachFrame">Vrai pour <c>0x0045ECC0</c> : le retour des echelles par
    /// huitiemes en tete de corps. Faux pour <c>0x0045C870</c> : la croissance de 200 dans les deux
    /// branches de detachement, a la place. Aucun des deux corps ne fait les deux.</param>
    private static void TickTowerRailCart(byte* e, bool easesScalesEachFrame)
    {
        short state = *(short*)(e + 0x10);

        if (easesScalesEachFrame && state != 6 && state != 7)
        {
            *(short*)(e + 0x28) += (short)((0x1000 - *(short*)(e + 0x28)) >> 3);
            *(short*)(e + 0x2a) += (short)((0x1000 - *(short*)(e + 0x2a)) >> 3);
            *(short*)(e + 0x2c) += (short)((0x1000 - *(short*)(e + 0x2c)) >> 3);
        }

        if (state == 0)
        {
            int homeX = *(int*)(e + 0x1c);
            int homeY = *(int*)(e + 0x20);
            int homeZ = *(int*)(e + 0x24);

            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x44) = homeX >> 0xc;
            *(int*)(e + 0x48) = homeY >> 0xc;
            *(int*)(e + 0x4c) = homeZ >> 0xc;

            do
            {
                if (RailCartStep(e, true, true)) *(short*)(e + 0x10) = 8;
            }
            while (*(short*)(e + 0x10) == 0);

            *(int*)(e + 0x48) = *(int*)(e + 0x1c) >> 0xc;
            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x24) >> 0xc);

            *(int*)(e + 0x1c) = homeX;
            *(int*)(e + 0x20) = homeY;
            *(int*)(e + 0x24) = homeZ;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (ActivePartySlotCount_00466D70() != aboard && *(short*)(e + 0x10) == 8)
        {
            if (!easesScalesEachFrame) GrowTowerRailCartScales(e);
            RailCartSnapToAnchor(e, true);
            return;
        }

        if (ActivePartySlotCount_00466D70() != aboard && *(short*)(e + 0x10) == 2)
        {
            if (!easesScalesEachFrame) GrowTowerRailCartScales(e);
            RailCartSnapToAnchor(e, false);
            return;
        }

        if (ActivePartySlotCount_00466D70() == aboard && *(short*)(e + 0x10) == 8)
        {
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2b), 0x180, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 1) TowerRailCartLeg(e, forward: true, arrived: 4, interrupted: 6);

        if (ActivePartySlotCount_00466D70() == aboard && *(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x12) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2b), 0x180, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 3) TowerRailCartLeg(e, forward: false, arrived: 5, interrupted: 7);

        if (*(short*)(e + 0x10) == 4 && aboard == 0) *(short*)(e + 0x10) = 2;
        if (*(short*)(e + 0x10) == 5 && aboard == 0) *(short*)(e + 0x10) = 8;

        if (*(short*)(e + 0x10) == 6 && *(int*)(e + 0x5c) == 0) RailCartFade(e, 8);
        if (*(short*)(e + 0x10) == 7 && *(int*)(e + 0x5c) == 0) RailCartFade(e, 2);
    }

    /// <summary>
    /// Une image de course du wagonnet de la tour. Le pas est <see cref="RailCartStep"/> au test
    /// d'arret exact (<c>cmp eax,-1 ; jne</c>) ; ce qui s'y ajoute est le drapeau de freinage
    /// <c>+0x98</c> et le son d'arrivee.
    ///
    /// <para>Le binaire pose le drapeau <b>entre</b> la comparaison de distance et la decrementation
    /// du compteur ; le poser apres le pas est equivalent, rien de ce que le pas ecrit ensuite ne le
    /// lit.</para>
    /// </summary>
    private static void TowerRailCartLeg(byte* e, bool forward, short arrived, short interrupted)
    {
        bool finished = RailCartStep(e, forward, false);

        if (*(short*)(e + 0x12) > *(short*)(e + 0x14) && *(short*)(e + 0x98) == 0)
        {
            *(short*)(e + 0x98) = 1;
        }

        if (finished)
        {
            *(short*)(e + 0x10) = arrived;
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2c), 0x300, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        int inContact = CountPartyMembersMatching(e, 0x20, 0x40000000);   // Ghidra: FUN_00535000
        if (ActivePartySlotCount_00466D70() != inContact)
        {
            *(short*)(e + 0x10) = interrupted;
            *(int*)(e + 0x5c) = 0;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0045c980</c> et <c>0x0045ca7a</c> — la croissance de
    /// <see cref="EntityHandler_0045C870"/> pendant qu'il attend : <c>+0x28</c> sous <c>0x1000</c>
    /// prend 200, ses deux voisines aussi, et les trois sont calees a <c>0x1000</c> si la premiere
    /// le depasse. Comparaisons sur seize bits signes.
    /// </summary>
    private static void GrowTowerRailCartScales(byte* e)
    {
        short scale = *(short*)(e + 0x28);
        if (scale >= 0x1000) return;

        scale += 200;
        *(short*)(e + 0x2a) += 200;
        *(short*)(e + 0x2c) += 200;
        *(short*)(e + 0x28) = scale;

        if (scale <= 0x1000) return;

        *(short*)(e + 0x28) = 0x1000;
        *(short*)(e + 0x2a) = 0x1000;
        *(short*)(e + 0x2c) = 0x1000;
    }
}
