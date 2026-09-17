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
}
