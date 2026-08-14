/*
 * Function: FUN_0051d1f0
 * Entry:    0051d1f0
 * Body:     [[0051d1f0, 0051e6f7]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * FUN_0051d1f0(int param_1,uint *param_2,int param_3,int *param_4,int *param_5,
                   undefined1 *param_6,uint param_7)

{
  short sVar1;
  short sVar2;
  short sVar3;
  short sVar4;
  short sVar5;
  short sVar6;
  short sVar7;
  short sVar8;
  short sVar9;
  uint uVar10;
  uint uVar11;
  uint *puVar12;
  uint *puVar13;
  code *pcVar14;
  uint *puVar15;
  uint uVar16;
  uint uVar17;
  uint uVar18;
  uint uVar19;
  uint *puVar20;
  uint *puVar21;
  uint uVar22;
  int iVar23;
  uint *puVar24;
  ushort uVar25;
  int iVar26;
  int iVar27;
  undefined4 *puVar28;
  ushort *puVar29;
  int iVar30;
  int iVar31;
  code *pcVar32;
  ulonglong uVar33;
  ulonglong uVar34;
  ulonglong uVar35;
  ulonglong uVar36;
  undefined8 uVar37;
  undefined8 uVar38;
  undefined8 uVar39;
  ulonglong uVar40;
  undefined1 local_d0 [20];
  undefined4 local_bc;
  undefined4 local_b8;
  undefined4 local_b4;
  undefined4 local_ac;
  undefined4 local_a8;
  undefined4 uStack_a4;
  undefined4 local_a0;
  undefined4 uStack_9c;
  uint local_98;
  uint local_94;
  ulonglong *local_90;
  undefined4 *local_8c;
  undefined4 local_88;
  undefined2 local_84;
  undefined2 local_82;
  undefined2 local_80;
  undefined2 local_7e;
  undefined2 local_7c;
  undefined2 local_7a;
  undefined2 local_78;
  int local_74;
  int local_70;
  int local_6c;
  short local_68;
  short sStack_66;
  short sStack_64;
  short sStack_62;
  short local_60;
  short local_5e;
  short local_5c;
  short sStack_5a;
  short sStack_58;
  uint local_54;
  ushort *local_50;
  uint local_4c;
  undefined2 local_48;
  undefined2 local_46;
  uint local_44;
  uint local_40;
  short local_3c;
  short sStack_3a;
  short sStack_38;
  undefined2 uStack_36;
  uint *local_34;
  uint local_30;
  int local_2c;
  uint local_28;
  int local_24;
  uint local_20;
  int local_1c;
  uint local_18;
  uint *local_14;
  uint local_10;
  ushort *local_c;
  uint local_8;
  
  pcVar32 = Ordinal_814_exref;
  uStack_36 = 0;
  *(undefined4 *)Ordinal_814_exref = DAT_006008e0;
  *(undefined4 *)(pcVar32 + 4) = DAT_006008e4;
  *(undefined4 *)(pcVar32 + 8) = DAT_006008e8;
  *(undefined4 *)(pcVar32 + 0xc) = DAT_006008ec;
  *(undefined2 *)(pcVar32 + 0x10) = DAT_006008f0;
  *(undefined4 *)(Ordinal_814_exref + 0x14) = DAT_006008f4;
  *(undefined4 *)(Ordinal_814_exref + 0x18) = DAT_006008f8;
  *(undefined4 *)(Ordinal_814_exref + 0x1c) = DAT_006008fc;
  *(short *)Ordinal_802_exref = (short)(*param_5 + DAT_006008d0 >> 0xc);
  *(short *)(Ordinal_802_exref + 2) = (short)(DAT_006008d4 - param_5[1] >> 0xc);
  *(short *)(Ordinal_802_exref + 4) = (short)(param_5[2] + DAT_006008d8 >> 0xc);
  local_3c = (short)param_5[5] + _DAT_00604928;
  *(int *)Ordinal_801_exref =
       ((int)*(short *)(Ordinal_802_exref + 2) * (int)*(short *)(Ordinal_814_exref + 2) +
        (int)*(short *)(Ordinal_802_exref + 4) * (int)*(short *)(Ordinal_814_exref + 4) +
        (int)*(short *)Ordinal_814_exref * (int)*(short *)Ordinal_802_exref >> 0xc) +
       *(int *)(Ordinal_814_exref + 0x14);
  *(undefined2 *)Ordinal_813_exref = *(undefined2 *)Ordinal_801_exref;
  *(int *)(Ordinal_801_exref + 4) =
       ((int)*(short *)(Ordinal_802_exref + 2) * (int)*(short *)(Ordinal_814_exref + 8) +
        (int)*(short *)(Ordinal_802_exref + 4) * (int)*(short *)(Ordinal_814_exref + 10) +
        (int)*(short *)(Ordinal_814_exref + 6) * (int)*(short *)Ordinal_802_exref >> 0xc) +
       *(int *)(Ordinal_814_exref + 0x18);
  *(undefined2 *)(Ordinal_813_exref + 2) = *(undefined2 *)(Ordinal_801_exref + 4);
  *(int *)(Ordinal_801_exref + 8) =
       ((int)*(short *)(Ordinal_802_exref + 2) * (int)*(short *)(Ordinal_814_exref + 0xe) +
        (int)*(short *)(Ordinal_802_exref + 4) * (int)*(short *)(Ordinal_814_exref + 0x10) +
        (int)*(short *)(Ordinal_814_exref + 0xc) * (int)*(short *)Ordinal_802_exref >> 0xc) +
       *(int *)(Ordinal_814_exref + 0x1c);
  *(undefined2 *)(Ordinal_813_exref + 4) = *(undefined2 *)(Ordinal_801_exref + 8);
  sStack_3a = *(short *)((int)param_5 + 0x16) + _DAT_0060492a;
  sStack_38 = (short)param_5[6] + _DAT_0060492c;
  local_bc = *(undefined4 *)Ordinal_801_exref;
  local_b8 = *(undefined4 *)(Ordinal_801_exref + 4);
  local_b4 = *(undefined4 *)(Ordinal_801_exref + 8);
  FUN_0051c300(&local_3c,local_d0);
  FUN_005327b0(local_d0,param_5 + 3);
  FUN_0051c300(&DAT_005e9308,&local_68);
  local_40 = (uint)*(ushort *)(param_3 + 0x12);
  local_10 = *(uint *)(param_3 + 0x20);
  puVar29 = *(ushort **)(param_3 + 0x24);
  local_50 = puVar29;
  local_c = (ushort *)(*(int *)(param_3 + 0x28) + (int)param_6 * local_40 * 2);
  iVar26 = *param_4;
  iVar23 = *(int *)MainMem_exref + 0x1ba000;
  uVar22 = *(uint *)(iVar26 + 0x3c);
  if ((DAT_00564a48 != 0) && ((uVar22 & 0x200) != 0)) {
    if (DAT_00564a4c == 0) {
      return param_2;
    }
    uVar22 = uVar22 & 0xfffffdff;
  }
  local_54 = (*(ushort *)
               (((uint)*(ushort *)(iVar26 + 0x14) + (uint)*(ushort *)(iVar26 + 0x16) +
                 (uint)*(ushort *)(iVar26 + 0x10) + (uint)*(ushort *)(iVar26 + 0x12)) * 0x10 + 8 +
               *(int *)(iVar26 + 0x28)) & 0xfffff800 | (uVar22 & 0xff | 0x1c0) << 5 |
             *(uint *)(&DAT_005e8f00 + (uVar22 >> 0x10 & 0xff) * 4)) << 0x10;
  uVar22 = (*(uint *)(&DAT_005e8f00 + (uVar22 >> 0x10 & 0xff) * 4) >> 0x10) +
           ((int)(short)((short)param_7 >> 2) & 0xffffffc0U) + (param_7 & 0xff);
  if (0x7ec0 < uVar22) {
    uVar22 = 0x7ec0;
  }
  local_4c = uVar22 << 0x10;
  param_6 = local_d0;
  param_7 = local_40;
  if (local_40 != 0) {
    do {
      iVar26 = (uint)*local_c * 0x10 + local_10;
      FUN_0051c300(iVar26,&local_88);
      local_48 = *(undefined2 *)(iVar26 + 0xe);
      local_44 = CONCAT22(local_44._2_2_,local_48);
      local_46 = local_48;
      Ordinal_794(&local_88,&local_48);
      local_74 = (int)*(short *)(iVar26 + 8);
      local_70 = (int)*(short *)(iVar26 + 10);
      local_6c = (int)*(short *)(iVar26 + 0xc);
      Ordinal_360(param_6,&local_88,iVar23);
      puVar29 = puVar29 + 1;
      local_c = local_c + 1;
      iVar23 = iVar23 + 0x20;
      param_6 = (undefined1 *)((*puVar29 + 0xdd00) * 0x20 + *(int *)MainMem_exref);
      param_7 = param_7 - 1;
    } while (param_7 != 0);
    local_50 = puVar29;
  }
  local_98 = (uint)*(short *)((int)param_5 + 0x1a);
  local_40 = (uint)*(ushort *)(param_3 + 0x14);
  if (*(ushort *)(param_3 + 0x14) == 0) {
    return param_2;
  }
  local_8c = &local_88;
  local_90 = (ulonglong *)&local_68;
  local_94 = local_98 & 1;
  puVar24 = param_2;
  local_50 = puVar29;
  do {
    puVar15 = (uint *)((*local_50 + 0xdd00) * 0x20 + *(int *)MainMem_exref);
    local_34 = puVar15;
    if (*(int *)MMXInstalled_exref == 0) {
      iVar27 = (int)local_68;
      iVar26 = (int)sStack_66;
      iVar23 = (int)sStack_64;
      local_14 = (uint *)(int)local_5e;
      local_88._0_2_ =
           (short)(iVar26 * *(short *)((int)puVar15 + 6) + iVar23 * (short)puVar15[3] +
                   (short)*puVar15 * iVar27 >> 0xc);
      iVar30 = (int)sStack_62;
      local_10 = (uint)local_60;
      iVar31 = (int)local_5c;
      local_30 = (uint)sStack_5a;
      local_82 = (short)((int)((int)local_14 * (int)(short)puVar15[3] +
                               local_10 * (int)*(short *)((int)puVar15 + 6) +
                              (short)*puVar15 * iVar30) >> 0xc);
      local_28 = (uint)sStack_58;
      local_7c = (short)((int)(local_28 * (int)(short)puVar15[3] +
                               local_30 * (int)*(short *)((int)puVar15 + 6) +
                              (short)*puVar15 * iVar31) >> 0xc);
      local_88._2_2_ =
           (short)(iVar23 * *(short *)((int)puVar15 + 0xe) + iVar26 * (short)puVar15[2] +
                   iVar27 * *(short *)((int)puVar15 + 2) >> 0xc);
      local_80 = (short)((int)(local_10 * (int)(short)puVar15[2] +
                               iVar30 * *(short *)((int)puVar15 + 2) +
                              (int)local_14 * (int)*(short *)((int)puVar15 + 0xe)) >> 0xc);
      local_7a = (short)((int)(local_30 * (int)(short)puVar15[2] +
                               local_28 * (int)*(short *)((int)puVar15 + 0xe) +
                              iVar31 * *(short *)((int)puVar15 + 2)) >> 0xc);
      local_84 = (short)(iVar26 * *(short *)((int)puVar15 + 10) + iVar27 * (short)puVar15[1] +
                         iVar23 * (short)puVar15[4] >> 0xc);
      local_7e = (short)((int)(local_10 * (int)*(short *)((int)puVar15 + 10) +
                               iVar30 * (short)puVar15[1] + (int)local_14 * (int)(short)puVar15[4])
                        >> 0xc);
      local_78 = (short)((int)(local_28 * (int)(short)puVar15[4] + iVar31 * (short)puVar15[1] +
                              local_30 * (int)*(short *)((int)puVar15 + 10)) >> 0xc);
    }
    else {
      local_a8 = 0xffffffff;
      uStack_a4 = 0xffff;
      local_a0 = 0xffff;
      uStack_9c = 0;
      uVar33 = *local_90 & 0xffffffffffff;
      uVar34 = *(ulonglong *)((int)local_90 + 6) & 0xffffffffffff;
      uVar36 = (ulonglong)*puVar15 & 0xffff | (ulonglong)(uint)(*(int *)((int)puVar15 + 6) << 0x10);
      uVar35 = *(ulonglong *)((int)local_90 + 0xc) & 0xffffffffffff;
      uVar40 = ((ulonglong)puVar15[3] & 0xffff) << 0x20;
      uVar37 = pmaddwd(uVar36 | uVar40,uVar33);
      uVar36 = uVar36 | uVar40;
      uVar39 = pmaddwd(uVar36,uVar34);
      uVar38 = pmaddwd(uVar36,uVar35);
      *(short *)local_8c = (short)((int)uVar37 + (int)((ulonglong)uVar37 >> 0x20) >> 0xc);
      *(short *)((int)local_8c + 6) = (short)((int)uVar39 + (int)((ulonglong)uVar39 >> 0x20) >> 0xc)
      ;
      *(short *)(local_8c + 3) = (short)((int)uVar38 + (int)((ulonglong)uVar38 >> 0x20) >> 0xc);
      uVar36 = (ulonglong)*(uint *)((int)puVar15 + 2) & 0xffff | (ulonglong)(puVar15[2] << 0x10);
      uVar40 = ((ulonglong)*(uint *)((int)puVar15 + 0xe) & 0xffff) << 0x20;
      uVar37 = pmaddwd(uVar36 | uVar40,uVar33);
      uVar36 = uVar36 | uVar40;
      uVar39 = pmaddwd(uVar36,uVar34);
      uVar38 = pmaddwd(uVar36,uVar35);
      *(short *)((int)local_8c + 2) = (short)((int)uVar37 + (int)((ulonglong)uVar37 >> 0x20) >> 0xc)
      ;
      *(short *)(local_8c + 2) = (short)((int)uVar39 + (int)((ulonglong)uVar39 >> 0x20) >> 0xc);
      *(short *)((int)local_8c + 0xe) =
           (short)((int)uVar38 + (int)((ulonglong)uVar38 >> 0x20) >> 0xc);
      uVar36 = (ulonglong)puVar15[1] & 0xffff |
               (ulonglong)(uint)(*(int *)((int)puVar15 + 10) << 0x10);
      uVar40 = ((ulonglong)puVar15[4] & 0xffff) << 0x20;
      uVar37 = pmaddwd(uVar36 | uVar40,uVar33);
      uVar36 = uVar36 | uVar40;
      uVar39 = pmaddwd(uVar36,uVar34);
      uVar38 = pmaddwd(uVar36,uVar35);
      *(short *)(local_8c + 1) = (short)((int)uVar37 + (int)((ulonglong)uVar37 >> 0x20) >> 0xc);
      *(short *)((int)local_8c + 10) =
           (short)((int)uVar39 + (int)((ulonglong)uVar39 >> 0x20) >> 0xc);
      *(short *)(local_8c + 4) = (short)((int)uVar38 + (int)((ulonglong)uVar38 >> 0x20) >> 0xc);
    }
    puVar28 = &local_88;
    pcVar32 = Ordinal_815_exref;
    for (iVar26 = 8; pcVar14 = Ordinal_814_exref, iVar26 != 0; iVar26 = iVar26 + -1) {
      *(undefined4 *)pcVar32 = *puVar28;
      puVar28 = puVar28 + 1;
      pcVar32 = pcVar32 + 4;
    }
    *(uint *)Ordinal_814_exref = *local_34;
    *(uint *)(pcVar14 + 4) = local_34[1];
    *(uint *)(pcVar14 + 8) = local_34[2];
    *(uint *)(pcVar14 + 0xc) = local_34[3];
    *(short *)(pcVar14 + 0x10) = (short)local_34[4];
    *(uint *)(Ordinal_814_exref + 0x14) = local_34[5];
    *(uint *)(Ordinal_814_exref + 0x18) = local_34[6];
    *(uint *)(Ordinal_814_exref + 0x1c) = local_34[7];
    if (local_94 == 0) {
LAB_0051db61:
      iVar26 = *param_4;
      uVar22 = *(uint *)(iVar26 + 0x3c);
      if ((DAT_00564a48 != 0) && ((uVar22 & 0x2000000) != 0)) {
        if (DAT_00564a4c == 0) goto LAB_0051e6c2;
        uVar22 = uVar22 & 0xfdffffff;
      }
      uVar16 = *(uint *)(iVar26 + 8) >> 0x10;
      iVar23 = *(int *)MainMem_exref;
      local_34 = (uint *)iVar26;
      if ((uVar22 & 0x400) == 0) {
        Ordinal_418(iVar26 + 0x40,iVar23 + 0x1b0000,iVar23 + 0x1b4000,iVar23,
                    *(uint *)(iVar26 + 8) & 0xffff);
      }
      else {
        Ordinal_795(iVar26 + 0x40,iVar23 + 0x1b0000);
      }
      puVar15 = (uint *)(*(int *)MainMem_exref + 0x1b8000);
      uVar17 = param_5[7] & 0x3ffffffU ^ (uVar22 & 0x300) << 0x10;
      if ((local_98 & 4) == 0) {
        *(uint *)Ordinal_817_exref = uVar17;
        Ordinal_793(*(undefined4 *)(iVar26 + 0x24),puVar15,uVar16);
      }
      else {
        iVar26 = uVar16 * 3;
        if (iVar26 != 0) {
          for (; iVar26 != 0; iVar26 = iVar26 + -1) {
            *puVar15 = uVar17;
            puVar15 = puVar15 + 1;
          }
        }
      }
      iVar26 = *(int *)MainMem_exref;
      iVar23 = iVar26 + 0x1b4000;
      if ((uVar22 & 0x400) == 0) {
        puVar15 = (uint *)(*(int *)((int)local_34 + 0x28) +
                          ((uint)*(ushort *)((int)local_34 + 0x14) +
                           (uint)*(ushort *)((int)local_34 + 0x16) +
                           (uint)*(ushort *)((int)local_34 + 0x10) +
                          (uint)*(ushort *)((int)local_34 + 0x12)) * 0x10);
        local_20 = (uint)*(ushort *)((int)local_34 + 0x1a);
        while ((local_20 != 0 && (DAT_005e9818 != 0))) {
          uVar22 = *puVar15;
          local_30 = uVar22 & 0xff;
          uVar16 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 & 0xff) * 4);
          local_28 = uVar22 >> 8 & 0xff;
          uVar17 = *(uint *)(iVar26 + 0x1b0000 + local_28 * 4);
          *(uint *)Ordinal_805_exref = uVar16;
          local_14 = (uint *)(uVar22 >> 0x10 & 0xff);
          uVar10 = *(uint *)(iVar26 + 0x1b0000 + (int)local_14 * 4);
          *(uint *)Ordinal_806_exref = uVar17;
          local_10 = uVar22 >> 0x18;
          uVar22 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 0x18) * 4);
          *(uint *)Ordinal_807_exref = uVar10;
          param_6._0_2_ = (ushort)uVar16;
          puVar21 = puVar24;
          if (((ushort)param_6 < 0x140) ||
             ((((ushort)uVar17 < 0x140 || ((ushort)uVar10 < 0x140)) ||
              (param_2._0_2_ = (ushort)uVar22, (ushort)param_2 < 0x140)))) {
            local_8 = (uint)*(short *)Ordinal_806_exref;
            local_2c = (int)*(short *)(Ordinal_807_exref + 2);
            local_1c = (int)*(short *)(Ordinal_806_exref + 2);
            local_18 = (uint)*(short *)Ordinal_807_exref;
            local_24 = (int)*(short *)Ordinal_805_exref;
            local_c = (ushort *)(int)*(short *)(Ordinal_805_exref + 2);
            local_44 = local_18 * (int)local_c + local_8 * local_2c + local_24 * local_1c;
            *(uint *)Ordinal_808_exref =
                 ((local_44 - local_24 * local_2c) - local_18 * local_1c) - (int)local_c * local_8;
            if (((((uVar16 & 0xffff0000) < 0xf00000) || ((uVar17 & 0xffff0000) < 0xf00000)) ||
                ((uVar10 & 0xffff0000) < 0xf00000)) || ((uVar22 & 0xffff0000) < 0xf00000)) {
              iVar27 = FUN_0051c2f0();
              if (-1 < iVar27) {
                *(uint *)Ordinal_807_exref = uVar22;
                iVar27 = FUN_0051c280();
                if (-1 < iVar27) goto LAB_0051e3d7;
              }
              puVar20 = *(uint **)(iVar23 + local_30 * 4);
              puVar12 = *(uint **)(iVar23 + local_28 * 4);
              local_14 = *(uint **)(iVar23 + (int)local_14 * 4);
              puVar13 = *(uint **)(iVar23 + local_10 * 4);
              if ((int)puVar20 < (int)puVar12) {
                puVar20 = puVar12;
              }
              if ((int)puVar20 < (int)local_14) {
                puVar20 = local_14;
              }
              if ((int)puVar20 < (int)puVar13) {
                puVar20 = puVar13;
              }
              if (puVar20 + -0x50 < (uint *)0xec0) {
                local_c = (ushort *)puVar15[3];
                uVar11 = puVar15[2];
                uVar18 = *(uint *)(iVar26 + 0x1b8000 + (puVar15[1] & 0xff) * 4);
                local_8 = uVar18;
                puVar21 = (uint *)(((uint)puVar20 & 0xfffc) + param_1);
                uVar19 = *puVar21;
                *puVar21 = (uint)puVar24;
                *puVar24 = uVar19 | 0x9000000;
                puVar24[2] = uVar17;
                puVar24[4] = uVar16;
                puVar24[6] = uVar10;
                puVar24[8] = uVar22;
                if (DAT_00564a48 != 0) {
                  uVar18 = uVar18 & 0xfdffffff;
                }
                puVar24[1] = uVar18 | 0x2c000000;
                puVar24[3] = uVar11 >> 0x10 | local_4c;
                puVar21 = puVar24 + 10;
                puVar24[5] = local_54 | uVar11 & 0xffff;
                puVar24[7] = (uint)local_c & 0xffff;
                puVar24[9] = (uint)local_c >> 0x10;
                DAT_005e9818 = DAT_005e9818 + -1;
              }
            }
          }
LAB_0051e3d7:
          puVar15 = puVar15 + 4;
          local_20 = local_20 - 1;
          puVar24 = puVar21;
        }
        local_20 = (uint)*(ushort *)((int)local_34 + 0x1e);
        while ((local_20 != 0 && (DAT_005e9818 != 0))) {
          uVar22 = *puVar15;
          local_30 = uVar22 & 0xff;
          uVar16 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 & 0xff) * 4);
          local_28 = uVar22 >> 8 & 0xff;
          uVar17 = *(uint *)(iVar26 + 0x1b0000 + local_28 * 4);
          *(uint *)Ordinal_805_exref = uVar16;
          local_14 = (uint *)(uVar22 >> 0x10 & 0xff);
          uVar10 = *(uint *)(iVar26 + 0x1b0000 + (int)local_14 * 4);
          *(uint *)Ordinal_806_exref = uVar17;
          local_10 = uVar22 >> 0x18;
          uVar22 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 0x18) * 4);
          *(uint *)Ordinal_807_exref = uVar10;
          param_6._0_2_ = (ushort)uVar16;
          if (((ushort)param_6 < 0x140) ||
             ((((ushort)uVar17 < 0x140 || ((ushort)uVar10 < 0x140)) ||
              (param_2._0_2_ = (ushort)uVar22, (ushort)param_2 < 0x140)))) {
            local_8 = (uint)*(short *)Ordinal_806_exref;
            local_2c = (int)*(short *)(Ordinal_807_exref + 2);
            local_1c = (int)*(short *)(Ordinal_806_exref + 2);
            local_18 = (uint)*(short *)Ordinal_807_exref;
            local_24 = (int)*(short *)Ordinal_805_exref;
            local_c = (ushort *)(int)*(short *)(Ordinal_805_exref + 2);
            local_44 = local_18 * (int)local_c + local_8 * local_2c + local_24 * local_1c;
            *(uint *)Ordinal_808_exref =
                 ((local_44 - local_24 * local_2c) - local_18 * local_1c) - (int)local_c * local_8;
            if ((((uVar16 & 0xffff0000) < 0xf00000) || ((uVar17 & 0xffff0000) < 0xf00000)) ||
               (((uVar10 & 0xffff0000) < 0xf00000 || ((uVar22 & 0xffff0000) < 0xf00000)))) {
              iVar27 = FUN_0051c2f0();
              if (-1 < iVar27) {
                *(uint *)Ordinal_807_exref = uVar22;
                iVar27 = FUN_0051c280();
                if (-1 < iVar27) goto LAB_0051e6b0;
              }
              puVar21 = *(uint **)(iVar23 + local_28 * 4);
              puVar20 = *(uint **)(iVar23 + local_30 * 4);
              local_14 = *(uint **)(iVar23 + (int)local_14 * 4);
              puVar12 = *(uint **)(iVar23 + local_10 * 4);
              if ((int)puVar20 < (int)puVar21) {
                puVar20 = puVar21;
              }
              if ((int)puVar20 < (int)local_14) {
                puVar20 = local_14;
              }
              if ((int)puVar20 < (int)puVar12) {
                puVar20 = puVar12;
              }
              if (puVar20 + -0x50 < (uint *)0xec0) {
                local_44 = puVar15[2];
                uVar11 = puVar15[1];
                local_c = (ushort *)puVar15[3];
                local_8 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 & 0xff) * 4);
                uVar18 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 >> 8 & 0xff) * 4);
                local_18 = uVar18;
                local_10 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 >> 0x18) * 4);
                uVar11 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 >> 0x10 & 0xff) * 4);
                puVar21 = (uint *)(((uint)puVar20 & 0xfffc) + param_1);
                uVar19 = *puVar21;
                *puVar21 = (uint)puVar24;
                *puVar24 = uVar19 | 0xc000000;
                puVar24[2] = uVar17;
                puVar24[5] = uVar16;
                puVar24[8] = uVar10;
                puVar24[0xb] = uVar22;
                if (DAT_00564a48 != 0) {
                  uVar18 = uVar18 & 0xfdffffff;
                }
                puVar24[1] = uVar18 | 0x3c000000;
                puVar24[7] = uVar11;
                puVar24[4] = local_8;
                puVar24[10] = local_10;
                puVar24[3] = local_44 >> 0x10 | local_4c;
                puVar24[6] = local_54 | local_44 & 0xffff;
                puVar24[9] = (uint)local_c & 0xffff;
                puVar24[0xc] = (uint)local_c >> 0x10;
                puVar24 = puVar24 + 0xd;
                DAT_005e9818 = DAT_005e9818 + -1;
              }
            }
          }
LAB_0051e6b0:
          puVar15 = puVar15 + 4;
          local_20 = local_20 - 1;
        }
      }
      else {
        local_14 = (uint *)(param_1 + sStack_38 * 4);
        local_20 = (uint)*(ushort *)((int)local_34 + 0x1a);
        puVar15 = (uint *)(*(int *)((int)local_34 + 0x28) +
                          ((uint)*(ushort *)((int)local_34 + 0x14) +
                           (uint)*(ushort *)((int)local_34 + 0x16) +
                           (uint)*(ushort *)((int)local_34 + 0x10) +
                          (uint)*(ushort *)((int)local_34 + 0x12)) * 0x10);
        while ((local_20 != 0 && (DAT_005e9818 != 0))) {
          uVar22 = *puVar15;
          uVar16 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 & 0xff) * 4);
          uVar17 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 8 & 0xff) * 4);
          *(uint *)Ordinal_805_exref = uVar16;
          uVar10 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 0x10 & 0xff) * 4);
          *(uint *)Ordinal_806_exref = uVar17;
          uVar22 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 0x18) * 4);
          *(uint *)Ordinal_807_exref = uVar10;
          param_6._0_2_ = (ushort)uVar16;
          puVar21 = puVar24;
          if (((ushort)param_6 < 0x140) ||
             ((((ushort)uVar17 < 0x140 || ((ushort)uVar10 < 0x140)) ||
              (param_2._0_2_ = (ushort)uVar22, (ushort)param_2 < 0x140)))) {
            local_8 = (uint)*(short *)Ordinal_806_exref;
            local_2c = (int)*(short *)(Ordinal_807_exref + 2);
            local_1c = (int)*(short *)(Ordinal_806_exref + 2);
            local_18 = (uint)*(short *)Ordinal_807_exref;
            local_24 = (int)*(short *)Ordinal_805_exref;
            local_c = (ushort *)(int)*(short *)(Ordinal_805_exref + 2);
            local_10 = local_18 * (int)local_c + local_8 * local_2c + local_24 * local_1c;
            *(uint *)Ordinal_808_exref =
                 ((local_10 - local_24 * local_2c) - local_18 * local_1c) - (int)local_c * local_8;
            if (((((uVar16 & 0xffff0000) < 0xf00000) || ((uVar17 & 0xffff0000) < 0xf00000)) ||
                ((uVar10 & 0xffff0000) < 0xf00000)) || ((uVar22 & 0xffff0000) < 0xf00000)) {
              iVar23 = FUN_0051c2f0();
              if (-1 < iVar23) {
                *(uint *)Ordinal_807_exref = uVar22;
                iVar23 = FUN_0051c280();
                if (-1 < iVar23) goto LAB_0051dea6;
              }
              local_c = (ushort *)puVar15[3];
              uVar11 = puVar15[2];
              uVar18 = *(uint *)(iVar26 + 0x1b8000 + (puVar15[1] & 0xff) * 4);
              local_8 = uVar18;
              uVar19 = *local_14;
              *local_14 = (uint)puVar24;
              *puVar24 = uVar19 | 0x9000000;
              puVar24[2] = uVar17;
              puVar24[4] = uVar16;
              puVar24[6] = uVar10;
              puVar24[8] = uVar22;
              if (DAT_00564a48 != 0) {
                uVar18 = uVar18 & 0xfdffffff;
              }
              puVar24[1] = uVar18 | 0x2c000000;
              puVar24[3] = uVar11 >> 0x10 | local_4c;
              puVar21 = puVar24 + 10;
              puVar24[5] = local_54 | uVar11 & 0xffff;
              puVar24[7] = (uint)local_c & 0xffff;
              puVar24[9] = (uint)local_c >> 0x10;
              DAT_005e9818 = DAT_005e9818 + -1;
            }
          }
LAB_0051dea6:
          puVar15 = puVar15 + 4;
          local_20 = local_20 - 1;
          puVar24 = puVar21;
        }
        local_20 = (uint)*(ushort *)((int)local_34 + 0x1e);
        while ((local_20 != 0 && (DAT_005e9818 != 0))) {
          uVar22 = *puVar15;
          uVar16 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 8 & 0xff) * 4);
          uVar17 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 & 0xff) * 4);
          *(uint *)Ordinal_805_exref = uVar17;
          uVar10 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 0x10 & 0xff) * 4);
          *(uint *)Ordinal_806_exref = uVar16;
          uVar22 = *(uint *)(iVar26 + 0x1b0000 + (uVar22 >> 0x18) * 4);
          *(uint *)Ordinal_807_exref = uVar10;
          param_6._0_2_ = (ushort)uVar17;
          puVar21 = puVar24;
          if (((ushort)param_6 < 0x140) ||
             ((((ushort)uVar16 < 0x140 || ((ushort)uVar10 < 0x140)) ||
              (param_2._0_2_ = (ushort)uVar22, (ushort)param_2 < 0x140)))) {
            local_8 = (uint)*(short *)Ordinal_806_exref;
            local_2c = (int)*(short *)(Ordinal_807_exref + 2);
            local_1c = (int)*(short *)(Ordinal_806_exref + 2);
            local_18 = (uint)*(short *)Ordinal_807_exref;
            local_24 = (int)*(short *)Ordinal_805_exref;
            local_c = (ushort *)(int)*(short *)(Ordinal_805_exref + 2);
            local_10 = local_18 * (int)local_c + local_8 * local_2c + local_24 * local_1c;
            *(uint *)Ordinal_808_exref =
                 ((local_10 - local_24 * local_2c) - local_18 * local_1c) - (int)local_c * local_8;
            if ((((uVar17 & 0xffff0000) < 0xf00000) || ((uVar16 & 0xffff0000) < 0xf00000)) ||
               (((uVar10 & 0xffff0000) < 0xf00000 || ((uVar22 & 0xffff0000) < 0xf00000)))) {
              iVar23 = FUN_0051c2f0();
              if (-1 < iVar23) {
                *(uint *)Ordinal_807_exref = uVar22;
                iVar23 = FUN_0051c280();
                if (-1 < iVar23) goto LAB_0051e127;
              }
              local_c = (ushort *)puVar15[3];
              uVar11 = puVar15[1];
              uVar18 = puVar15[2];
              local_8 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 & 0xff) * 4);
              uVar19 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 >> 8 & 0xff) * 4);
              local_18 = uVar19;
              local_10 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 >> 0x18) * 4);
              local_28 = *(uint *)(iVar26 + 0x1b8000 + (uVar11 >> 0x10 & 0xff) * 4);
              uVar11 = *local_14;
              *local_14 = (uint)puVar24;
              *puVar24 = uVar11 | 0xc000000;
              puVar24[5] = uVar17;
              puVar24[2] = uVar16;
              puVar24[0xb] = uVar22;
              puVar24[8] = uVar10;
              if (DAT_00564a48 != 0) {
                uVar19 = uVar19 & 0xfdffffff;
              }
              puVar24[1] = uVar19 | 0x3c000000;
              puVar24[4] = local_8;
              puVar24[7] = local_28;
              puVar24[10] = local_10;
              puVar24[3] = uVar18 >> 0x10 | local_4c;
              puVar21 = puVar24 + 0xd;
              puVar24[6] = local_54 | uVar18 & 0xffff;
              puVar24[9] = (uint)local_c & 0xffff;
              puVar24[0xc] = (uint)local_c >> 0x10;
              DAT_005e9818 = DAT_005e9818 + -1;
            }
          }
LAB_0051e127:
          puVar15 = puVar15 + 4;
          local_20 = local_20 - 1;
          puVar24 = puVar21;
        }
      }
    }
    else {
      *(undefined2 *)Ordinal_802_exref = 0;
      *(undefined2 *)(Ordinal_802_exref + 2) = 0;
      *(undefined2 *)(Ordinal_802_exref + 4) = 0;
      iVar26 = (int)*(short *)(Ordinal_802_exref + 4);
      sVar1 = *(short *)(Ordinal_802_exref + 2);
      iVar23 = (int)*(short *)Ordinal_802_exref;
      sVar2 = *(short *)(Ordinal_814_exref + 2);
      sVar3 = *(short *)Ordinal_814_exref;
      sVar4 = *(short *)(Ordinal_814_exref + 4);
      sVar5 = *(short *)(Ordinal_814_exref + 8);
      sVar6 = *(short *)(Ordinal_814_exref + 10);
      sVar7 = *(short *)(Ordinal_814_exref + 6);
      uVar25 = (short)(CONCAT22((short)((uint)(*(short *)(Ordinal_814_exref + 0xc) * iVar23) >> 0x10
                                       ),*(short *)(Ordinal_814_exref + 0xe) * sVar1 +
                                         (short)(*(short *)(Ordinal_814_exref + 0xc) * iVar23)) +
                       *(short *)(Ordinal_814_exref + 0x10) * iVar26 >> 0xc) +
               *(short *)(Ordinal_814_exref + 0x1c);
      sVar8 = *(short *)(Ordinal_814_exref + 0x14);
      local_ac = CONCAT22(local_ac._2_2_,uVar25);
      sVar9 = *(short *)(Ordinal_814_exref + 0x18);
      *(int *)Ordinal_811_exref = (int)(short)uVar25;
      *(uint *)Ordinal_809_exref =
           (((int)*(short *)(Ordinal_821_exref + (uint)uVar25 * 2) *
             (int)(short)((short)((int)sVar5 * (int)sVar1 + sVar6 * iVar26 + sVar7 * iVar23 >> 0xc)
                         + sVar9) & 0xfffff000U) + *(int *)Ordinal_819_exref * 0x1000) * 0x10 +
           ((int)*(short *)(Ordinal_821_exref + (uint)uVar25 * 2) *
            (int)(short)((short)((int)sVar2 * (int)sVar1 + iVar23 * sVar3 + sVar4 * iVar26 >> 0xc) +
                        sVar8) >> 0xc) + *(int *)Ordinal_820_exref;
      *(int *)Ordinal_810_exref = *(int *)Ordinal_811_exref >> 2;
      local_3c = (short)*(undefined4 *)Ordinal_809_exref;
      sStack_3a = (short)((uint)*(undefined4 *)Ordinal_809_exref >> 0x10);
      sStack_38 = (short)*(undefined4 *)Ordinal_810_exref;
      uStack_36 = (undefined2)((uint)*(undefined4 *)Ordinal_810_exref >> 0x10);
      if ((((ushort)(local_3c + 0x18U) < 0x170) && ((ushort)(sStack_3a + 0x18U) < 0x120)) &&
         ((int)sStack_38 - 0x58U < 0x3a0)) goto LAB_0051db61;
    }
LAB_0051e6c2:
    local_40 = local_40 - 1;
    param_4 = param_4 + 1;
    local_50 = local_50 + 1;
    if (local_40 == 0) {
      return puVar24;
    }
  } while( true );
}


