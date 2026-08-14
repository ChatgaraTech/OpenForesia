/*
 * Function: FUN_00545340
 * Entry:    00545340
 * Body:     [[00545340, 00545a08]]
 */


uint * FUN_00545340(uint *param_1,uint *param_2,ushort *param_3)

{
  byte bVar1;
  byte bVar2;
  ushort uVar3;
  int iVar4;
  undefined4 uVar5;
  uint uVar6;
  int iVar7;
  uint *puVar8;
  ushort *puVar9;
  int iVar10;
  uint *puVar11;
  uint uVar12;
  uint uVar13;
  uint uVar14;
  uint uVar15;
  ushort *puVar16;
  int iVar17;
  undefined4 *puVar18;
  int local_c;
  
  FUN_0051be20(param_1,&param_2);
  puVar9 = param_3;
  puVar11 = *(uint **)(param_3 + 0x458);
  if (puVar11 != (uint *)0x0) {
    uVar12 = *puVar11 & 0xfc0000;
    if (uVar12 == 0x200000) {
      if (puVar11[0x1b] != 0) {
        uVar12 = (uint)*(byte *)(puVar11[0x1b] + 8);
        goto LAB_005453c4;
      }
    }
    else {
      if (uVar12 == 0x400000) {
        uVar12 = (uint)*(byte *)((int)puVar11 + 9);
        goto LAB_005453c4;
      }
      if (uVar12 == 0x800000) {
        uVar12 = (uint)(byte)puVar11[2];
        goto LAB_005453c4;
      }
    }
  }
  uVar12 = (uint)(byte)(&DAT_005e93c8)[(uint)DAT_005de538 * 0x9c];
LAB_005453c4:
  if (((&DAT_005e93c0)[uVar12 * 0x27] & 0x10000) != 0) {
    uVar12 = (uint)(byte)(&DAT_005e93c8)[(uint)DAT_005de538 * 0x9c];
  }
  bVar1 = (&DAT_005e9189)[uVar12 * 0x4c];
  iVar4 = **(int **)(DAT_005e97c0 + 0x94);
  uVar5 = *(undefined4 *)(&DAT_005e8f00 + (uint)*param_3 * 4);
  uVar6 = *(uint *)(param_3 + 4);
  iVar17 = (uint)(byte)(&DAT_005e9188)[uVar12 * 0x4c] * 0x10;
  uVar12 = *(uint *)(param_3 + 0x1c);
  iVar7 = *(int *)(param_3 + 2);
  if (((uVar12 & 0x1000000) != 0) && ((*(byte *)((int)param_3 + 0x8c7) & 0x20) == 0)) {
    if (((char)param_3[0x462] != '\0') &&
       (((*(byte *)((int)&DAT_00604a24 + iVar17 + 2) & 0x10) != 0 &&
        ((*(byte *)((int)param_3 + 0x8c7) & 0x40) != 0)))) {
      *(char *)(param_3 + 0x462) = (char)param_3[0x462] + -2;
      param_3 = (ushort *)uVar5;
      FUN_0053db60();
      uVar5 = param_3;
    }
    param_3 = (ushort *)uVar5;
    if ((((char)puVar9[0x462] != '\0') && ((*(byte *)((int)&DAT_00604a24 + iVar17 + 2) & 0x10) != 0)
        ) && ((*(byte *)((int)puVar9 + 0x8c7) & 0x80) != 0)) {
      *(char *)(puVar9 + 0x462) = (char)puVar9[0x462] + -3;
      FUN_0053db60();
    }
    if ((((char)puVar9[0x462] != '\0') && ((*(byte *)((int)&DAT_00604a24 + iVar17 + 2) & 0x10) != 0)
        ) && ((*(byte *)((int)puVar9 + 0x8c7) & 0xc0) == 0)) {
      *(char *)(puVar9 + 0x462) = (char)puVar9[0x462] + -1;
      FUN_0053db60();
    }
    if ((((int)(uint)(byte)puVar9[0x462] < (int)(*(byte *)((int)puVar9 + 0x11) - 2)) &&
        ((*(byte *)((int)&DAT_00604a24 + iVar17 + 2) & 0x40) != 0)) &&
       ((*(byte *)((int)puVar9 + 0x8c7) & 0x40) != 0)) {
      *(byte *)(puVar9 + 0x462) = (byte)puVar9[0x462] + 2;
      FUN_0053db60();
    }
    if ((((int)(uint)(byte)puVar9[0x462] < (int)(*(byte *)((int)puVar9 + 0x11) - 3)) &&
        ((*(byte *)((int)&DAT_00604a24 + iVar17 + 2) & 0x40) != 0)) &&
       ((*(byte *)((int)puVar9 + 0x8c7) & 0x80) != 0)) {
      *(byte *)(puVar9 + 0x462) = (byte)puVar9[0x462] + 3;
      FUN_0053db60();
    }
    if ((((int)(uint)(byte)puVar9[0x462] < (int)(*(byte *)((int)puVar9 + 0x11) - 1)) &&
        ((*(byte *)((int)&DAT_00604a24 + iVar17 + 2) & 0x40) != 0)) &&
       ((*(byte *)((int)puVar9 + 0x8c7) & 0xc0) == 0)) {
      *(byte *)(puVar9 + 0x462) = (byte)puVar9[0x462] + 1;
      FUN_0053db60();
    }
    uVar3 = *(ushort *)((int)&DAT_00604a20 + iVar17 + 2);
    if ((uVar3 & 0x8000) != 0) {
      *(ushort *)((int)&DAT_00604a20 + iVar17 + 2) = uVar3 & 0x7fff;
      bVar2 = *(byte *)((int)puVar9 + 0x8c7);
      *(undefined1 *)((int)puVar9 + 0x3b) = 0;
      *(byte *)((int)puVar9 + 0x8c7) = bVar2 & 0xfb;
      if ((bVar2 & 0x40) != 0) {
        *(byte *)((int)&DAT_005e9060 + (uint)*(byte *)((int)puVar9 + 0x8c5)) =
             (byte)puVar9[0x462] >> 1;
      }
      if ((*(byte *)((int)puVar9 + 0x8c7) & 0x80) != 0) {
        *(byte *)((int)&DAT_005e9060 + (uint)*(byte *)((int)puVar9 + 0x8c5)) =
             (byte)puVar9[0x462] / 3;
      }
      if ((*(byte *)((int)puVar9 + 0x8c7) & 0xc0) == 0) {
        *(char *)((int)&DAT_005e9060 + (uint)*(byte *)((int)puVar9 + 0x8c5)) = (char)puVar9[0x462];
      }
      FUN_0053db80();
      *(byte *)((int)puVar9 + 0x8c7) = *(byte *)((int)puVar9 + 0x8c7) & 0x3f;
    }
    uVar5 = *(undefined4 *)(puVar9 + 0x1e);
    iVar10 = uVar12 + ((byte)puVar9[0x462] + 0x155555) * 0xc00;
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,iVar7,iVar10,uVar5,
                                   *(undefined4 *)(iVar4 + 0xd8 + (uint)bVar1 * 8));
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,iVar7,iVar10 + 0x400,uVar5,
                                   *(undefined4 *)(iVar4 + 0xc0));
    uVar5 = param_3;
  }
  param_3 = (ushort *)uVar5;
  uVar12 = *(uint *)(puVar9 + 0x18);
  if (((uVar12 & 0x1000000) != 0) && ((*(byte *)((int)puVar9 + 0x8c7) & 0x20) == 0)) {
    uVar3 = *(ushort *)((int)&DAT_00604a20 + iVar17 + 2);
    if ((uVar3 & 0x8000) != 0) {
      *(ushort *)((int)&DAT_00604a20 + iVar17 + 2) = uVar3 & 0x7fff;
      *(undefined1 *)((int)puVar9 + 0x33) = 0;
      *(byte *)((int)puVar9 + 0x8c7) = *(byte *)((int)puVar9 + 0x8c7) & 0xfd;
      FUN_0053db80();
    }
    uVar5 = *(undefined4 *)(puVar9 + 0x1a);
    uVar12 = CONCAT22((short)(uVar12 >> 0x10),(ushort)(byte)uVar12) | (uVar6 >> 0x10 & 0xff) << 8;
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,iVar7,uVar12 + 0x40c,uVar5,
                                   *(undefined4 *)(iVar4 + 0xd8 + (uint)bVar1 * 8));
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,iVar7,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + (DAT_005e9960 >> 1 & 3) * 8));
  }
  puVar16 = puVar9 + 0x26;
  local_c = 4;
  puVar18 = (undefined4 *)(iVar4 + 0x20);
  puVar11 = param_2;
  do {
    if ((*(uint *)(puVar16 + -2) & 0x1000000) != 0) {
      puVar11 = (uint *)FUN_00545160(param_1,puVar11,param_3,iVar7,*(uint *)(puVar16 + -2),
                                     *(undefined4 *)puVar16,*puVar18);
      param_2 = puVar11;
    }
    puVar18 = puVar18 + 2;
    puVar16 = puVar16 + 4;
    local_c = local_c + -1;
  } while (local_c != 0);
  uVar12 = *(uint *)(puVar9 + 0x20);
  if ((uVar12 & 0x1000000) != 0) {
    uVar5 = *(undefined4 *)(puVar9 + 0x22);
    param_2 = (uint *)FUN_00545160(param_1,puVar11,param_3,iVar7,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x60));
    uVar13 = uVar6 & 0xffff;
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,uVar13 + iVar7,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x68));
    uVar14 = uVar6 & 0xffff0000;
    iVar17 = uVar13 + iVar7 + uVar14;
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,iVar17,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x78));
    param_2 = (uint *)FUN_00545160(param_1,param_2,param_3,iVar17 - uVar13,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x70));
    uVar15 = uVar6 - 0x10 & 0xffff | 0x100000;
    param_2 = (uint *)FUN_00545220(param_1,param_2,param_3,iVar7 + 0x10,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x40),uVar15);
    param_2 = (uint *)FUN_00545220(param_1,param_2,param_3,iVar7 + 0x10 + uVar14,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x48),uVar15);
    uVar14 = uVar14 - 0x100000 | 0x10;
    param_2 = (uint *)FUN_00545220(param_1,param_2,param_3,iVar7 + 0x100000,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x50),uVar14);
    puVar11 = (uint *)FUN_00545220(param_1,param_2,param_3,iVar7 + 0x100000 + uVar13,uVar12,uVar5,
                                   *(undefined4 *)(iVar4 + 0x58),uVar14);
    param_2 = puVar11;
  }
  if ((*(uint *)(puVar9 + 0x10) & 0x1000000) != 0) {
    uVar12 = *(uint *)(puVar9 + 0x52);
    while (puVar8 = (uint *)(uVar12 & 0xffffff), puVar8 != (uint *)0xffffff) {
      if ((*puVar8 & 0xff000000) != 0) {
        puVar11[1] = puVar8[1];
        param_2[2] = puVar8[2];
        param_2[3] = puVar8[3] + iVar7;
        param_2[4] = puVar8[4];
        param_2[5] = puVar8[5];
        *param_2 = *param_1 | 0x5000000;
        *param_1 = (uint)param_2;
        puVar11 = param_2 + 6;
        param_2 = puVar11;
      }
      uVar12 = *puVar8;
    }
  }
  if ((*(uint *)(puVar9 + 0xc) & 0x1000000) != 0) {
    param_2 = (uint *)FUN_00545220(param_1,puVar11,param_3,iVar7,*(uint *)(puVar9 + 0xc),
                                   *(undefined4 *)(puVar9 + 0xe),*(undefined4 *)(iVar4 + 0x80),uVar6
                                  );
  }
  FUN_0051bdb0(param_1,&param_2);
  return param_2;
}


