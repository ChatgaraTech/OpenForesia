/*
 * Function: FUN_00507cb0
 * Entry:    00507cb0
 * Body:     [[00507cb0, 00508406]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_00507cb0(int param_1)

{
  char cVar1;
  byte bVar2;
  ushort uVar3;
  short sVar4;
  int iVar5;
  undefined4 uVar6;
  uint uVar7;
  uint uVar8;
  char *pcVar9;
  char *pcVar10;
  char *pcVar11;
  undefined4 unaff_retaddr;
  char acStack_20 [24];
  undefined4 uStack_8;
  
  if (DAT_00562a48 != 0) {
    EnableKBInput(1);
    FUN_00523530();
    DAT_00562a48 = 0;
    DAT_00578df8 = 0;
    DAT_00578dfc = 0;
    DAT_00578df4 = 0;
    DAT_0057351c = 0x380028;
    DAT_00573520._0_2_ = 0xe0;
    DAT_00573520._2_2_ = 0x68;
    DAT_00573528._2_1_ = 0x20;
    DAT_00573538 = 0x1070c28;
    DAT_0057353c = 0x1000000;
    DAT_00562a50 = 2;
  }
  uVar3 = (*(byte *)(DAT_00573338 + 0x150) - 8) * 0x100 - _DAT_0060492a & 0xfff;
  if (uVar3 < 0x800) {
    sVar4 = (short)uVar3 >> 2;
  }
  else {
    sVar4 = -(short)(0x1000 - (short)uVar3 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar4;
  DAT_005e93a0 = DAT_005e93a0 + (short)(0x60 - DAT_005e93a0 >> 2);
  DAT_005e93a2 = DAT_005e93a2 + (short)(0x60 - DAT_005e93a2 >> 2);
  DAT_005e93a4 = DAT_005e93a4 + (short)(0x60 - DAT_005e93a4 >> 2);
  FUN_0051c300(&DAT_00604928,&DAT_006008e0);
  Ordinal_376((int)DAT_005e93a0,(int)DAT_005e93a2,(int)DAT_005e93a4);
  DAT_00572b7a = DAT_00572b7a +
                 (short)((int)((uint)*(byte *)(DAT_00573338 + 0x150) * -0x100 - (int)DAT_00572b7a)
                        >> 2);
  if (DAT_00562a4c != 0) goto LAB_0050820a;
  iVar5 = GetKBChar();
  if (iVar5 == 0) {
    uVar7 = (DAT_00578df4 << 8 | DAT_00578dfc) << 8 | DAT_00578df8;
    if ((*(byte *)(param_1 + 2) & 8) != 0) {
      DAT_00578df8 = 0;
      DAT_00578dfc = 7;
    }
    if ((*(byte *)(param_1 + 6) & 0x80) != 0) {
      if (DAT_00578df8 == 0) {
        if ((DAT_00578dfc != 0) || (DAT_00578df4 != 0)) {
          DAT_00578df8 = 9;
          *(byte *)(param_1 + 6) = *(byte *)(param_1 + 6) | 0x10;
        }
      }
      else {
        DAT_00578df8 = DAT_00578df8 - 1;
      }
    }
    if ((*(byte *)(param_1 + 6) & 0x20) != 0) {
      if ((int)DAT_00578df8 < 9) {
        DAT_00578df8 = DAT_00578df8 + 1;
      }
      else if (((int)DAT_00578dfc < 7) || (DAT_00578df4 < 7)) {
        DAT_00578df8 = 0;
        *(ushort *)(param_1 + 6) = *(ushort *)(param_1 + 6) | 0x40;
      }
    }
    if ((*(byte *)(param_1 + 6) & 0x10) != 0) {
      if (DAT_00578dfc == 0) {
        if (DAT_00578df4 != 0) {
          DAT_00578df4 = DAT_00578df4 + -1;
        }
      }
      else {
        DAT_00578dfc = DAT_00578dfc - 1;
      }
    }
    if (((*(byte *)(param_1 + 6) & 0x40) != 0) && ((int)DAT_00578dfc < 7)) {
      DAT_00578dfc = DAT_00578dfc + 1;
    }
    if (DAT_00578dfc == 7) {
      DAT_00578df8 = 0;
    }
    if (uVar7 != ((DAT_00578df4 << 8 | DAT_00578dfc) << 8 | DAT_00578df8)) {
      FUN_0053db60();
    }
    if ((*(ushort *)(param_1 + 2) & 0x8000) == 0) {
      if ((*(ushort *)(param_1 + 2) & 0x4000) == 0) goto LAB_0050820a;
      if (DAT_00578e00 != 0) {
        DAT_00578e00 = DAT_00578e00 + -1;
        *(undefined1 *)(DAT_00573338 + 0x100 + DAT_00578e00) = 0;
        FUN_0053dba0();
        goto LAB_0050820a;
      }
      if ((DAT_00578dfc != 7) || (DAT_00578df4 != 7)) goto LAB_0050820a;
      goto LAB_005081ec;
    }
    if (DAT_00578dfc != 7) {
      if (7 < DAT_00578e00) goto LAB_0050820a;
      bVar2 = *(byte *)(*(int *)(*(int *)(DAT_005e97c4 + 0x70) + 0x50 +
                                (DAT_00578dfc + DAT_00578df4) * 4) + DAT_00578df8 * 3 +
                       (((int)DAT_00578df8 < 5) - 1 & 2));
      if ((DAT_00578e00 == 0) && ((uint)bVar2 == (DAT_00562c9c & 0xffff))) {
        FUN_0053dbc0();
        goto LAB_0050820a;
      }
      FUN_0053db80();
      *(byte *)(DAT_00573338 + 0x100 + DAT_00578e00) = bVar2;
      DAT_00578e00 = DAT_00578e00 + 1;
      *(undefined1 *)(DAT_00573338 + 0x100 + DAT_00578e00) = 0;
LAB_005081a3:
      if (DAT_00578e00 == 8) {
        DAT_00578df8 = 0;
        DAT_00578dfc = 7;
      }
      goto LAB_0050820a;
    }
    if (DAT_00578e00 == 0) {
      pcVar9 = *(char **)(*(int *)(DAT_005e97c4 + 0x70) + 0x208 +
                         (uint)*(byte *)(DAT_00573338 + 0x150) * 4);
      cVar1 = *pcVar9;
      iVar5 = 0;
      while (cVar1 != '\0') {
        pcVar9 = pcVar9 + 1;
        *(char *)(iVar5 + 0x100 + DAT_00573338) = cVar1;
        iVar5 = iVar5 + 1;
        cVar1 = *pcVar9;
      }
      *(undefined1 *)(iVar5 + 0x100 + DAT_00573338) = 0;
      DAT_00578e00 = iVar5 >> 1;
    }
    FUN_0053db80();
    DAT_00578df0 = 0x5f;
    DAT_00578de8 = DAT_00578de8 + 2;
  }
  else {
    if (iVar5 == 0xd) {
      DAT_00578dfc = 7;
      if (DAT_00578e00 == 0) {
        pcVar9 = *(char **)(*(int *)(DAT_005e97c4 + 0x70) + 0x208 +
                           (uint)*(byte *)(DAT_00573338 + 0x150) * 4);
        cVar1 = *pcVar9;
        iVar5 = 0;
        while (cVar1 != '\0') {
          pcVar9 = pcVar9 + 1;
          *(char *)(iVar5 + 0x100 + DAT_00573338) = cVar1;
          iVar5 = iVar5 + 1;
          cVar1 = *pcVar9;
        }
        *(undefined1 *)(iVar5 + 0x100 + DAT_00573338) = 0;
        DAT_00578e00 = iVar5 >> 1;
      }
      FUN_0053db80();
      DAT_00578de8 = DAT_00578de8 + 2;
      DAT_00578df0 = 0x5f;
      EnableKBInput(0);
      FUN_00523520();
      goto LAB_0050820a;
    }
    if (iVar5 == 8) {
      if (DAT_00578e00 != 0) {
        DAT_00578e00 = DAT_00578e00 + -1;
        *(undefined1 *)(DAT_00573338 + 0x100 + DAT_00578e00) = 0;
        FUN_0053dba0();
      }
      goto LAB_0050820a;
    }
    if (iVar5 != 0x1b) {
      if ((iVar5 < 0x20) || (((0x7f < iVar5 && (iVar5 < 0xa0)) || (7 < DAT_00578e00))))
      goto LAB_0050820a;
      FUN_0053db80();
      *(char *)(DAT_00573338 + 0x100 + DAT_00578e00) = (char)iVar5;
      DAT_00578e00 = DAT_00578e00 + 1;
      *(undefined1 *)(DAT_00573338 + 0x100 + DAT_00578e00) = 0;
      goto LAB_005081a3;
    }
LAB_005081ec:
    FUN_0053dba0();
    DAT_00578df0 = 0;
    DAT_00578de8 = DAT_00578de8 + -1;
  }
  EnableKBInput(0);
LAB_0050820a:
  FUN_00544350(&DAT_005606f8,0,&DAT_005618b8,0);
  uVar6 = FUN_005443a0(unaff_retaddr,&DAT_005618b8);
  uVar7 = 0xffffffff;
  pcVar9 = (char *)(DAT_00573338 + 0x100);
  do {
    pcVar11 = pcVar9;
    if (uVar7 == 0) break;
    uVar7 = uVar7 - 1;
    pcVar11 = pcVar9 + 1;
    cVar1 = *pcVar9;
    pcVar9 = pcVar11;
  } while (cVar1 != '\0');
  uVar7 = ~uVar7;
  pcVar9 = pcVar11 + -uVar7;
  pcVar11 = acStack_20;
  for (uVar8 = uVar7 >> 2; uVar8 != 0; uVar8 = uVar8 - 1) {
    *(undefined4 *)pcVar11 = *(undefined4 *)pcVar9;
    pcVar9 = pcVar9 + 4;
    pcVar11 = pcVar11 + 4;
  }
  for (uVar7 = uVar7 & 3; uVar7 != 0; uVar7 = uVar7 - 1) {
    *pcVar11 = *pcVar9;
    pcVar9 = pcVar9 + 1;
    pcVar11 = pcVar11 + 1;
  }
  if (DAT_00578dfc != 7) {
    uVar7 = 0xffffffff;
    pcVar9 = &DAT_00562c98;
    do {
      pcVar11 = pcVar9;
      if (uVar7 == 0) break;
      uVar7 = uVar7 - 1;
      pcVar11 = pcVar9 + 1;
      cVar1 = *pcVar9;
      pcVar9 = pcVar11;
    } while (cVar1 != '\0');
    uVar7 = ~uVar7;
    iVar5 = -1;
    pcVar9 = acStack_20;
    do {
      pcVar10 = pcVar9;
      if (iVar5 == 0) break;
      iVar5 = iVar5 + -1;
      pcVar10 = pcVar9 + 1;
      cVar1 = *pcVar9;
      pcVar9 = pcVar10;
    } while (cVar1 != '\0');
    pcVar9 = pcVar11 + -uVar7;
    pcVar11 = pcVar10 + -1;
    for (uVar8 = uVar7 >> 2; uVar8 != 0; uVar8 = uVar8 - 1) {
      *(undefined4 *)pcVar11 = *(undefined4 *)pcVar9;
      pcVar9 = pcVar9 + 4;
      pcVar11 = pcVar11 + 4;
    }
    for (uVar7 = uVar7 & 3; uVar7 != 0; uVar7 = uVar7 - 1) {
      *pcVar11 = *pcVar9;
      pcVar9 = pcVar9 + 1;
      pcVar11 = pcVar11 + 1;
    }
  }
  FUN_00544350(acStack_20,0,&DAT_005618b8,0);
  uVar6 = FUN_005443a0(uVar6,&DAT_005618b8);
  FUN_0051be20(uStack_8,&stack0x00000000);
  uVar6 = FUN_00521df0(uStack_8,uVar6,&stack0xffffffcc,0,0);
  uVar6 = FUN_0051f9c0(uStack_8,uVar6,&stack0xffffffcc);
  FUN_0051bdb0(uStack_8,&stack0x00000000);
  FUN_00544350(&DAT_005606f8,0,&DAT_00573518,0);
  uVar6 = FUN_005443a0(uVar6,&DAT_00573518);
  FUN_00507870();
  iVar5 = 0;
  do {
    FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x50 + (iVar5 + DAT_00578df4) * 4),
                 0,&DAT_00573518,0);
    uVar6 = FUN_005443a0(uVar6,&DAT_00573518);
    iVar5 = iVar5 + 1;
  } while (iVar5 < 8);
  uVar6 = FUN_00545340(uStack_8,uVar6,&DAT_00573518);
  FUN_00507890();
  return uVar6;
}


