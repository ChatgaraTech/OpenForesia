
/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00537da0(void)

{
  int *piVar1;
  byte bVar2;
  undefined1 *puVar3;
  uint uVar4;
  int iVar5;
  short sVar6;
  uint uVar7;
  int iVar8;
  uint *puVar9;
  undefined4 unaff_EBP;
  int *piVar10;
  int iVar11;
  uint uVar12;
  undefined4 *puVar13;
  uint *puVar14;
  uint local_44;
  undefined4 local_40;
  uint local_3c;
  undefined4 local_38;
  undefined4 local_34;
  int local_30;
  int local_2c;
  uint local_28;
  uint local_24;
  int local_20;
  undefined1 auStack_1c [8];
  int aiStack_14 [2];
  short sStack_c;
  short sStack_a;
  undefined2 local_8;
  undefined2 local_6;
  short sStack_4;
  
  local_30 = 0;
  FUN_0052a1e0(&local_34,&local_38,&local_40);
  FUN_00529ad0(5);
  iVar11 = 0x20;
  piVar10 = &DAT_005de828;
  do {
    uVar4 = FUN_00547140();
    piVar10[-1] = ((uVar4 & 0x3ff) - 0x1ff) * 0x1000;
    uVar4 = FUN_00547140();
    *piVar10 = ((uVar4 & 0x3ff) - 0x1ff) * 0x1000;
    uVar4 = FUN_00547140();
    piVar10[4] = 0;
    piVar10[5] = 0;
    piVar10[1] = ((uVar4 & 0xfff) - 0x7ff) * 0x1000;
    *(undefined2 *)(piVar10 + 2) = 0x1000;
    *(undefined2 *)((int)piVar10 + 10) = 0x1000;
    *(undefined2 *)(piVar10 + 3) = 0x1000;
    *(undefined2 *)((int)piVar10 + 0xe) = 0;
    piVar10[6] = 0;
    iVar5 = FUN_00547140();
    iVar11 = iVar11 + -1;
    *(char *)(piVar10 + 8) = (char)(iVar5 % 7);
    piVar10[7] = *(int *)(*(int *)(DAT_005e97c4 + 0x94) + 0xc);
    piVar10 = piVar10 + 0x27;
  } while (iVar11 != 0);
  FUN_00504b10(0);
  uVar7 = (uint)DAT_005e901e;
  uVar4 = (uVar7 & 0xfc) << 1;
  local_24 = (uVar4 | uVar7 & 3) << 1 | 9;
  local_44 = 1;
  if ((DAT_005e901e & 1) != 0) {
    local_44 = 3;
  }
  local_20 = (uVar7 & 2) << 1;
  if ((DAT_005e901e & 2) != 0) {
    local_44 = local_44 | 0x10;
  }
  uVar7 = local_44 | 8;
  if ((uVar4 & 8) != 0) {
    uVar7 = local_44 | 0xc;
  }
  local_44 = uVar7;
  if ((uVar4 & 0x10) != 0) {
    local_44 = local_44 | 0x20;
  }
  if ((uVar4 & 0x20) != 0) {
    local_44 = local_44 | 0x80;
  }
  if ((uVar4 & 0x40) != 0) {
    local_44 = local_44 | 0x200;
  }
  if ((uVar4 & 0x80) != 0) {
    local_44 = local_44 | 0x40;
  }
  if ((uVar4 & 0x100) != 0) {
    local_44 = local_44 | 0x100;
  }
  local_3c = (uint)(byte)(&DAT_0056d2a0)[(byte)DAT_005de528];
  local_2c = (int)*(short *)(&DAT_0056d272 + (uint)(byte)(&DAT_0056d2b0)[local_3c] * 4);
  uVar4 = (int)*(short *)(&DAT_0056d270 + (uint)(byte)(&DAT_0056d2b0)[local_3c] * 4);
  while( true ) {
    uVar7 = local_3c;
    DAT_005de15c = 1;
    if ((DAT_00564a38 == 0) && (7 < DAT_005de160)) {
      DAT_005de154 = 0;
      DAT_005de158 = (DAT_005de160 + -0xb) * 0xc;
    }
    else {
      DAT_005de154 = 0xa0;
      DAT_005de158 = DAT_005de160 * 0xc + 0x76;
    }
    local_8 = 0x1400;
    local_6 = 0x1149;
    FUN_0040a600(5);
    FUN_00533000(DAT_005e97c4);
    bVar2 = (&DAT_005e9188)[(uint)(byte)(&DAT_005e93c8)[(uint)DAT_005de538 * 0x9c] * 0x4c];
    uVar12 = uVar7;
    if ((*(ushort *)(&DAT_00604a26 + (uint)bVar2 * 0x10) & 0xc0) != 0) {
      do {
        local_3c = uVar12 - 1;
        if ((int)local_3c < 0) {
          local_3c = 0xb;
        }
        uVar12 = local_3c;
      } while ((local_44 & 1 << ((byte)local_3c & 0x1f)) == 0);
    }
    if ((*(ushort *)(&DAT_00604a26 + (uint)bVar2 * 0x10) & 0x30) != 0) {
      do {
        local_3c = uVar12 + 1;
        if (10 < (int)local_3c) {
          local_3c = 0;
        }
        uVar12 = local_3c;
      } while ((local_44 & 1 << ((byte)local_3c & 0x1f)) == 0);
    }
    local_28 = uVar7;
    if (uVar7 != uVar12) {
      FUN_0053db60();
    }
    if (((&DAT_00604a22)[(uint)bVar2 * 8] & 0x8008) != 0) break;
    _DAT_00604928 = 0x200;
    _DAT_0060492a = 0x800;
    iVar11 = uVar4 - ((int)(uVar4 - (int)*(short *)(&DAT_0056d270 +
                                                   (uint)(byte)(&DAT_0056d2b0)[uVar12] * 4)) >> 2);
    iVar5 = local_2c -
            (local_2c - *(short *)(&DAT_0056d272 + (uint)(byte)(&DAT_0056d2b0)[uVar12] * 4) >> 2);
    _DAT_0060492c = 0;
    DAT_006008d0 = 0;
    DAT_006008d4 = 0;
    DAT_006008d8 = 0;
    DAT_006008f4 = 0;
    DAT_006008f8 = 0;
    DAT_006008fc = 0x5dc;
    local_2c = iVar5;
    local_28 = iVar11;
    Ordinal_379(400);
    Ordinal_378(iVar11,iVar5);
    FUN_0051c300(&DAT_00604928,&DAT_006008e0);
    if (iVar11 < 0x81) {
      sVar6 = (short)((0xe8 - DAT_0056c980) - (int)DAT_0056c97c >> 2);
    }
    else {
      sVar6 = (short)(8 - DAT_0056c97c >> 2);
    }
    DAT_0056c97c = DAT_0056c97c + sVar6;
    uVar4 = (uint)DAT_005de538;
    iVar11 = uVar4 * 0x9c;
    *(undefined2 *)(&DAT_005e93e8 + iVar11) = 0x1000;
    *(undefined2 *)(&DAT_005e93ea + iVar11) = 0x1000;
    *(undefined2 *)(&DAT_005e93ec + iVar11) = 0x1000;
    iVar8 = (uint)(byte)(&DAT_005e93c4)[iVar11] * 4;
    (&DAT_005e93f6)[uVar4 * 0x4e] = 1;
    *(undefined2 *)(&DAT_005e93f0 + iVar11) = 0;
    (&DAT_005e93f2)[uVar4 * 0x4e] = 0;
    *(undefined2 *)(&DAT_005e93f4 + iVar11) = 0;
    (&DAT_005e93dc)[uVar4 * 0x27] = 0;
    (&DAT_005e93e0)[uVar4 * 0x27] = 0;
    (&DAT_005e93e4)[uVar4 * 0x27] = 0;
    *(undefined4 *)(&DAT_005e93f8 + iVar11) = 0x1000000;
    (&DAT_005e93d8)[uVar4 * 0x4e] = 1;
    iVar5 = *(int *)(*(int *)(&DAT_005e9680 + iVar8) + 4);
    *(short *)(&DAT_005e93da + iVar11) = *(short *)(&DAT_005e93da + iVar11) + 1;
    sVar6 = *(short *)(&DAT_005e93da + iVar11);
    if ((int)(uint)*(ushort *)(iVar5 + 0x16) <= (int)sVar6) {
      *(undefined2 *)(&DAT_005e93da + iVar11) = 0;
    }
    unaff_EBP = FUN_0051d1f0(local_40,unaff_EBP,*(undefined4 *)(*(int *)(&DAT_005e9680 + iVar8) + 4)
                             ,*(undefined4 *)(&DAT_005e9800 + iVar8),&DAT_005e93dc + uVar4 * 0x27,
                             (int)*(short *)(&DAT_005e93da + iVar11),
                             CONCAT22(sVar6 >> 0xf,*(undefined2 *)(&DAT_005e93ca + iVar11)));
    _DAT_00604928 = 700;
    _DAT_0060492a = 500;
    _DAT_0060492c = 0;
    DAT_006008d0 = 0;
    DAT_006008d4 = 0;
    DAT_006008d8 = 0;
    DAT_006008f4 = 0;
    DAT_006008f8 = 0;
    DAT_006008fc = 0x5dc;
    Ordinal_379(400);
    Ordinal_378(0xa0,0x78);
    FUN_0051c300(&DAT_00604928,&DAT_006008e0);
    piVar10 = &DAT_005de82c;
    iVar11 = 0x20;
    do {
      piVar1 = piVar10 + -2;
      iVar5 = FUN_0051f650(piVar1,auStack_1c);
      if ((iVar5 != 0) && (piVar10[5] != 0x808080)) {
        piVar10[5] = piVar10[5] + 0x80808;
      }
      local_40 = FUN_00520ae0(local_34,local_40,piVar1,0,0);
      iVar5 = *piVar10;
      *piVar10 = iVar5 + 0x4000;
      if (0x7fffff < iVar5 + 0x4000) {
        uVar4 = FUN_00547140();
        *piVar1 = ((uVar4 & 0x3ff) - 0x1ff) * 0x1000;
        uVar4 = FUN_00547140();
        piVar10[-1] = ((uVar4 & 0x3ff) - 0x1ff) * 0x1000;
        *piVar10 = *piVar10 + -0x1000000;
        iVar5 = FUN_00547140();
        piVar10[5] = 0;
        *(char *)(piVar10 + 7) = (char)(iVar5 % 7);
      }
      piVar10 = piVar10 + 0x27;
      iVar11 = iVar11 + -1;
    } while (iVar11 != 0);
    aiStack_14[1] = 0x1000000;
    sVar6 = 0;
    puVar13 = &DAT_0056d240;
    aiStack_14[0] = *(int *)(*(int *)(DAT_005e97c4 + 0x94) + 4);
    do {
      if ((local_24 & 1 << ((byte)sVar6 & 0x1f)) != 0) {
        sStack_c = (short)*puVar13;
        sStack_a = (short)((uint)*puVar13 >> 0x10);
        iVar11 = aiStack_14[0] + sVar6 * 8;
        iVar5 = ((short)(sStack_c + (ushort)(*(byte *)(aiStack_14[0] + 2 + sVar6 * 8) >> 1)) + -0x80
                ) * 0x140;
        sStack_c = (short)(iVar5 + (iVar5 >> 0x1f & 0xffU) >> 8) + 0x80;
        local_8 = 0x1400;
        local_6 = 0x1149;
        iVar5 = ((short)(((*(byte *)(iVar11 + 3) >> 1) - 1) + sStack_a) + -0x70) * 0xf2;
        sStack_a = ((short)(iVar5 / 0xe0) + (short)(iVar5 >> 0x1f) + 0x70) -
                   (short)((longlong)iVar5 * 0x92492493 >> 0x3f);
        if (((puVar13 == (undefined4 *)&DAT_0056d250) || (puVar13 == (undefined4 *)&DAT_0056d258))
           || (puVar13 == (undefined4 *)&DAT_0056d25c)) {
          sStack_a = sStack_a + 1;
          local_6 = 0x116d;
        }
        if (*(char *)(iVar11 + 2) != '\0') {
          *(char *)(iVar11 + 2) = *(char *)(iVar11 + 2) + -1;
          *(char *)(iVar11 + 3) = *(char *)(iVar11 + 3) + -1;
        }
        sStack_4 = sVar6;
        local_40 = FUN_00521f20(local_38,local_40,aiStack_14,0,0);
        local_40 = FUN_0051f9c0(local_38,local_40,aiStack_14);
        if (*(char *)(iVar11 + 2) != '\0') {
          *(char *)(iVar11 + 2) = *(char *)(iVar11 + 2) + '\x01';
          *(char *)(iVar11 + 3) = *(char *)(iVar11 + 3) + '\x01';
        }
      }
      puVar13 = puVar13 + 1;
      sVar6 = sVar6 + 1;
    } while ((int)puVar13 < 0x56d26d);
    if (local_20 != 0) {
      sStack_4 = (short)(local_30 >> 2);
      aiStack_14[0] = *(int *)(*(int *)(DAT_005e97c4 + 0x94) + 8);
      iVar11 = aiStack_14[0] + sStack_4 * 8;
      iVar5 = ((short)((*(byte *)(aiStack_14[0] + 2 + sStack_4 * 8) >> 1) + 0xa0) + -0x80) * 0x140;
      sStack_c = (short)(iVar5 + (iVar5 >> 0x1f & 0xffU) >> 8) + 0x80;
      iVar5 = ((short)((*(byte *)(iVar11 + 3) >> 1) - 1) + -0x70) * 0xf2;
      sStack_a = ((short)(iVar5 / 0xe0) + (short)(iVar5 >> 0x1f) + 0x70) -
                 (short)((longlong)iVar5 * 0x92492493 >> 0x3f);
      *(char *)(iVar11 + 2) = *(char *)(iVar11 + 2) + -1;
      *(char *)(iVar11 + 3) = *(char *)(iVar11 + 3) + -1;
      local_40 = FUN_00521f20(local_38,local_40,aiStack_14,0,0);
      local_40 = FUN_0051f9c0(local_38,local_40,aiStack_14);
      *(char *)(iVar11 + 2) = *(char *)(iVar11 + 2) + '\x01';
      *(char *)(iVar11 + 3) = *(char *)(iVar11 + 3) + '\x01';
    }
    sStack_c = 0x80;
    sStack_a = 0x70;
    sStack_4 = 0;
    aiStack_14[0] = **(int **)(DAT_005e97c4 + 0x94);
    local_40 = FUN_00521f20(local_38,local_40,aiStack_14,0,0);
    local_40 = FUN_0051f9c0(local_38,local_40,aiStack_14);
    FUN_00544350(*(undefined4 *)
                  (*(int *)(DAT_005e97c4 + 0x70) + (uint)(byte)(&DAT_0056d2b0)[local_3c] * 4),0,
                 &DAT_0056c978,0);
    local_40 = FUN_005443a0(local_40,&DAT_0056c978);
    local_40 = FUN_00545340(local_34,local_40,&DAT_0056c978);
    _DAT_005e9950 = 0x100;
    _DAT_005e98e0 = 0x100;
    _DAT_005e9952 = 0xe0;
    _DAT_005e98e2 = 0xe0;
    DAT_005de15c = 0;
    FUN_0052a280(&local_34,&local_38,&local_40,3,1);
    _DAT_005e9950 = 0x140;
    _DAT_005e98e0 = 0x140;
    _DAT_005e9952 = 0xf0;
    _DAT_005e98e2 = 0xf0;
    local_30 = local_30 + 1;
    uVar4 = local_28;
    if (0x17 < local_30) {
      local_30 = 0;
    }
  }
  FUN_0053db80();
  DAT_005de528._0_1_ = (&DAT_0056d2b0)[uVar12];
  DAT_00566c84 = 0xff;
  puVar3 = *(undefined1 **)(*(int *)(DAT_005e97c4 + 0x74) + (uint)(byte)DAT_005de528 * 4);
  DAT_00566c88 = 0xff;
  DAT_005de52c = *puVar3;
  DAT_005de530 = puVar3[1];
  FUN_0052a280(&local_34,&local_38,&local_40,3,1);
  puVar13 = &DAT_005e9840;
  for (iVar11 = 0x10; iVar11 != 0; iVar11 = iVar11 + -1) {
    *puVar13 = 0;
    puVar13 = puVar13 + 1;
  }
  puVar13 = &DAT_005e9060;
  for (iVar11 = 0x10; iVar11 != 0; iVar11 = iVar11 + -1) {
    *puVar13 = 0;
    puVar13 = puVar13 + 1;
  }
  puVar13 = &DAT_005e9780;
  for (iVar11 = 0x10; iVar11 != 0; iVar11 = iVar11 + -1) {
    *puVar13 = 0;
    puVar13 = puVar13 + 1;
  }
  puVar13 = &DAT_005e96e0;
  for (iVar11 = 0x10; iVar11 != 0; iVar11 = iVar11 + -1) {
    *puVar13 = 0;
    puVar13 = puVar13 + 1;
  }
  puVar13 = (undefined4 *)&DAT_005e9324;
  do {
    puVar13[-1] = 0;
    *puVar13 = 0xffffffff;
    puVar13 = puVar13 + 2;
  } while ((int)puVar13 < 0x5e9364);
  if (((byte)DAT_005de528 == 0) && (DAT_005e9020 != 0)) {
    puVar9 = (uint *)(*(int *)MainMem_exref + 0xf0140);
    iVar11 = DAT_005e9020;
    do {
      *puVar9 = *puVar9 & 0xbfffffff;
      *(short *)(puVar9 + 3) = (short)puVar9[2];
      *(undefined2 *)((int)puVar9 + 0xe) = *(undefined2 *)((int)puVar9 + 10);
      puVar14 = puVar9 + 0x10;
      puVar9 = puVar9 + 0x800;
      iVar11 = iVar11 + -1;
      for (iVar5 = 0x20; iVar5 != 0; iVar5 = iVar5 + -1) {
        *puVar14 = 0;
        puVar14 = puVar14 + 1;
      }
    } while (iVar11 != 0);
  }
  return;
}


