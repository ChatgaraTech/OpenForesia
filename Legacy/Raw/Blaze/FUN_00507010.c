/*
 * Function: FUN_00507010
 * Entry:    00507010
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_00507010(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  char *pcVar1;
  char cVar2;
  byte bVar3;
  ushort uVar4;
  short sVar5;
  int iVar6;
  int iVar7;
  undefined4 uVar8;
  uint *puVar9;
  short unaff_BX;
  int iVar10;
  undefined4 unaff_EBP;
  undefined2 *puVar11;
  short *psVar12;
  uint uVar13;
  undefined4 unaff_retaddr;
  int aiStack_48 [2];
  short sStack_40;
  undefined2 uStack_3e;
  short sStack_3c;
  short sStack_3a;
  short sStack_38;
  undefined1 auStack_34 [4];
  int iStack_30;
  ushort uStack_12;
  int iStack_8;
  
  if (_DAT_005633a8 == 0) {
    uVar4 = *(ushort *)pad;
    if ((uVar4 & 1) == 0) {
      if ((uVar4 & 0x400) != 0) {
        _DAT_005e930a = _DAT_005e930a - 0x40;
      }
      if ((pad[1] & 8) != 0) {
        _DAT_005e930a = _DAT_005e930a + 0x40;
      }
      if ((pad[1] & 1) != 0) {
        _DAT_005e930c = _DAT_005e930c - 0x40;
      }
      if ((pad[1] & 2) != 0) {
        _DAT_005e930c = _DAT_005e930c + 0x40;
      }
      if ((pad[1] & 0x10) != 0) {
        _DAT_005e9308 = _DAT_005e9308 + 0x40;
      }
      if ((pad[1] & 0x40) != 0) {
        _DAT_005e9308 = _DAT_005e9308 - 0x40;
      }
      _DAT_005e9308 = _DAT_005e9308 & 0xfff;
      _DAT_005e930a = _DAT_005e930a & 0xfff;
      _DAT_005e930c = _DAT_005e930c & 0xfff;
    }
    else {
      if ((uVar4 & 0x400) != 0) {
        _DAT_005e93a2 = _DAT_005e93a2 - 8;
      }
      if ((pad[1] & 8) != 0) {
        _DAT_005e93a2 = _DAT_005e93a2 + 8;
      }
      if ((pad[1] & 1) != 0) {
        _DAT_005e93a4 = _DAT_005e93a4 - 8;
      }
      if ((pad[1] & 2) != 0) {
        _DAT_005e93a4 = _DAT_005e93a4 + 8;
      }
      if ((pad[1] & 0x10) != 0) {
        _DAT_005e93a0 = _DAT_005e93a0 + 8;
      }
      if ((pad[1] & 0x40) != 0) {
        _DAT_005e93a0 = _DAT_005e93a0 - 8;
      }
      _DAT_005e93a0 = _DAT_005e93a0 & 0xff;
      _DAT_005e93a2 = _DAT_005e93a2 & 0xff;
      _DAT_005e93a4 = _DAT_005e93a4 & 0xff;
    }
  }
  if (_DAT_00562a48 != 0) {
    _DAT_00562a48 = 0;
    _DAT_005e93a0 = 0x60;
    _DAT_005e93a2 = 0x60;
    _DAT_005e93a4 = 0x60;
    _DAT_005e9308 = 0x200;
    _DAT_005e930a = 0x380;
    _DAT_005e930c = 0;
    _DAT_00604928 = 0x15e;
    _DAT_0060492c = 0;
    _DAT_0060492a = (*(byte *)(_DAT_00573338 + 0x150) - 8 & 0xf) << 8;
    _DAT_006008d0 = 0;
    _DAT_006008d4 = 0;
    _DAT_006008d8 = 0;
    _DAT_006008f4 = 0;
    _DAT_006008f8 = 0;
    _DAT_006008fc = 0;
    iVar10 = 0;
    puVar11 = (undefined2 *)0x578454;
    do {
      puVar11[-2] = 0;
      *puVar11 = 0;
      iVar6 = func_0x00545a1c(iVar10 << 8);
      *(undefined4 *)(puVar11 + -10) = 0;
      *(int *)(puVar11 + -0xc) = iVar6 * -0x96;
      iVar6 = func_0x00545a16(iVar10 << 8);
      *(byte *)(puVar11 + -0x18) = (byte)iVar10;
      puVar11[-6] = 0;
      puVar11[-5] = 0;
      puVar11[-4] = 0;
      *(undefined4 *)(puVar11 + 2) = 0xffffff;
      *(int *)(puVar11 + -8) = iVar6 * 0x96;
      puVar11[1] = 1;
      puVar11[-0x12] = 0;
      puVar11[-0x11] = 0;
      puVar11[-0xe] = 0;
      puVar11[-0xd] = 0;
      *(undefined1 *)(puVar11 + -0x15) = 0;
      *(byte *)((int)puVar11 + -0x29) = (byte)iVar10 & 3;
      *(undefined4 *)(puVar11 + -0x1a) = 0x800000;
      *(undefined1 *)(puVar11 + -0x16) = 0;
      iVar10 = iVar10 + 1;
      puVar11 = puVar11 + 0x4e;
    } while (iVar10 < 0x10);
    _DAT_00562a50 = 1;
  }
  if (_DAT_00562a4c != 0) goto LAB_00507443;
  switch(_DAT_00578df0) {
  case 0:
    cVar2 = *(char *)(_DAT_00573338 + 0x150);
    if ((pad[6] & 0x80) != 0) {
      *(char *)(_DAT_00573338 + 0x150) = cVar2 + -1;
    }
    if ((pad[6] & 0x20) != 0) {
      *(char *)(_DAT_00573338 + 0x150) = *(char *)(_DAT_00573338 + 0x150) + '\x01';
    }
    *(byte *)(_DAT_00573338 + 0x150) = *(byte *)(_DAT_00573338 + 0x150) & 0xf;
    if (cVar2 != *(char *)(_DAT_00573338 + 0x150)) {
      func_0x0053db60();
    }
    uVar4 = *(ushort *)(pad + 2);
    if ((uVar4 & 0x8000) == 0) {
      if ((uVar4 & 0x4000) != 0) {
        *(ushort *)(pad + 2) = uVar4 & 0xbfff;
        func_0x0053dba0();
        _DAT_00578df0 = 0x77;
      }
    }
    else {
      *(ushort *)(pad + 2) = uVar4 & 0x7fff;
      func_0x0053db80();
      _DAT_00578df0 = _DAT_00578df0 + 1;
    }
    goto LAB_00507443;
  case 1:
    if ((bRam00560fef & 2) == 0) {
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = _DAT_00578df0 + 1;
      goto LAB_00507443;
    }
    break;
  case 2:
    if ((bRam00560fef & 4) == 0) {
      if (cRam005e909f == '\0') {
        func_0x005352e0((uint)*(byte *)(_DAT_00573338 + 0x150) * 0x9c + 0x578420,0x572b48,0x27);
        iRam00578de8 = iRam00578de8 + 1;
        _DAT_00578df0 = 0x13;
        *(undefined1 *)(_DAT_00573338 + 0x100) = 0;
        uRam00578e00 = 0;
        goto LAB_00507443;
      }
      goto code_r0x0050743d;
    }
    bVar3 = pad[3];
joined_r0x00507423:
    if ((bVar3 & 0x40) == 0) goto LAB_00507443;
    uRam00560763 = 0;
    bRam00560fef = bRam00560fef & 0xfb;
    goto code_r0x00507438;
  default:
    goto LAB_00507443;
  case 0x59:
    if ((bRam00560fef & 4) != 0) {
      bVar3 = pad[3];
      goto joined_r0x00507423;
    }
    if (cRam005e909f == '\0') {
      iRam00578de8 = -1;
      uRam005de510 = 0;
      goto LAB_00507443;
    }
    goto code_r0x0050743d;
  case 0x77:
    if ((bRam00560fef & 2) == 0) {
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = 0x59;
      goto LAB_00507443;
    }
  }
  if ((pad[3] & 0x40) != 0) {
    uRam0056075b = 0;
    bRam00560fef = bRam00560fef & 0xfd;
code_r0x00507438:
    func_0x0053dba0();
code_r0x0050743d:
    _DAT_00578df0 = 0;
  }
LAB_00507443:
  uStack_12 = (*(byte *)(_DAT_00573338 + 0x150) - 8) * 0x100 - _DAT_0060492a & 0xfff;
  if (uStack_12 < 0x800) {
    sVar5 = (short)uStack_12 >> 2;
  }
  else {
    sVar5 = -(short)(0x1000 - (short)uStack_12 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar5;
  func_0x0051c300(&DAT_00604928,&DAT_006008e0);
  func_0x00545aa0((int)(short)_DAT_005e93a0,(int)(short)_DAT_005e93a2,(int)(short)_DAT_005e93a4);
  uVar13 = 0;
  psVar12 = (short *)0x57844a;
  otBase = (uint *)0x0;
  iVar10 = 0x2000;
  do {
    sVar5 = psVar12[4];
    if (uVar13 == *(byte *)(_DAT_00573338 + 0x150)) {
      psVar12[4] = (short)(-((int)sVar5 + (int)otBase) >> 2) + sVar5;
      psVar12[-1] = (short)(0x1800 - psVar12[-1] >> 2) + psVar12[-1];
      *psVar12 = (short)(0x1800 - *psVar12 >> 2) + *psVar12;
      psVar12[1] = (short)(0x1800 - psVar12[1] >> 2) + psVar12[1];
      psVar12[-0xd] = (short)(-(int)psVar12[-0xd] >> 2) + psVar12[-0xd];
      psVar12[-0xc] = (short)(-(int)psVar12[-0xc] >> 2) + psVar12[-0xc];
    }
    else {
      psVar12[4] = (short)(iVar10 - sVar5 >> 2) + sVar5;
      psVar12[-1] = (short)(-(int)psVar12[-1] >> 2) + psVar12[-1];
      *psVar12 = (short)(-(int)*psVar12 >> 2) + *psVar12;
      psVar12[1] = (short)(-(int)psVar12[1] >> 2) + psVar12[1];
      psVar12[-0xd] = (short)(0x1000 - psVar12[-0xd] >> 2) + psVar12[-0xd];
      psVar12[-0xc] = (short)(0x1000 - psVar12[-0xc] >> 2) + psVar12[-0xc];
    }
    if (0xff < psVar12[-1]) {
      iVar6 = *(int *)(*(int *)((uint)*(byte *)(psVar12 + -0x13) * 4 + 0x5e9640) + psVar12[-9] * 4);
      psVar12[-8] = psVar12[-8] + 1;
      if ((int)(uint)*(ushort *)(iVar6 + 0x16) <= (int)psVar12[-8]) {
        psVar12[-8] = 0;
      }
      unaff_retaddr =
           func_0x0051d1f0(iStack_8,unaff_retaddr,
                           *(undefined4 *)
                            (*(int *)((uint)*(byte *)(psVar12 + -0x13) * 4 + 0x5e9640) +
                            psVar12[-9] * 4),
                           *(undefined4 *)((uint)*(byte *)(psVar12 + -0x13) * 4 + 0x5e96a0),
                           psVar12 + -7,(int)psVar12[-8],CONCAT22(psVar12[-9] >> 0xf,psVar12[-0x10])
                          );
    }
    func_0x005352e0(psVar12 + -7,auStack_34,10);
    iStack_30 = iStack_30 + 0x3c000;
    iVar7 = func_0x0051f650(auStack_34,&stack0xffffffb0);
    iVar6 = iStack_8;
    if (iVar7 != 0) {
      sStack_38 = (short)uVar13;
      aiStack_48[0] = *(int *)(*(int *)(_DAT_005e97c4 + 0x94) + 0x28);
      *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) =
           *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) + -1;
      *(char *)(aiStack_48[0] + 3 + sStack_38 * 8) =
           *(char *)(aiStack_48[0] + 3 + sStack_38 * 8) + -1;
      sStack_3a = psVar12[-0xc];
      sStack_40 = (short)unaff_EBP;
      sStack_3c = psVar12[-0xd];
      uStack_3e = (undefined2)((uint)unaff_EBP >> 0x10);
      aiStack_48[1] = 0x1000000;
      uVar8 = func_0x00521f20(iStack_8 + 0x50 + unaff_BX * 4,unaff_retaddr,aiStack_48,2,0);
      sStack_40 = sStack_40 + -1;
      unaff_retaddr = func_0x00521f20(iVar6 + 0x50 + unaff_BX * 4,uVar8,aiStack_48,2,0);
      *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) =
           *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) + '\x01';
      pcVar1 = (char *)(aiStack_48[0] + 3 + sStack_38 * 8);
      *pcVar1 = *pcVar1 + '\x01';
    }
    iVar10 = iVar10 + -0x100;
    uVar13 = uVar13 + 1;
    otBase = otBase + 0x40;
    psVar12 = psVar12 + 0x4e;
  } while (0x1000 < iVar10);
  func_0x00544350(0x5606f8,0,0x560ff0,0);
  uVar8 = func_0x005443a0(unaff_retaddr,0x560ff0);
  func_0x00544350(*(undefined4 *)
                   (*(int *)(_DAT_005e97c4 + 0x70) + 0xc +
                   (uint)*(byte *)(_DAT_00573338 + 0x150) * 4),0,0x560ff0,0);
  puVar9 = (uint *)func_0x005443a0(uVar8,0x560ff0);
  return puVar9;
}


