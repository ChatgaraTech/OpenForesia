/*
 * Function: FUN_00507cb0
 * Entry:    00507cb0
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_00507cb0(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  char cVar1;
  byte bVar2;
  ushort uVar3;
  short sVar4;
  int iVar5;
  undefined4 uVar6;
  uint *puVar7;
  uint uVar8;
  uint uVar9;
  char *pcVar10;
  char *pcVar11;
  char *pcVar12;
  undefined4 unaff_retaddr;
  char acStack_20 [24];
  undefined4 uStack_8;
  
  if (_DAT_00562a48 != 0) {
    func_0x00545ab8(1);
    func_0x00523530();
    _DAT_00562a48 = 0;
    _DAT_00578df8 = 0;
    _DAT_00578dfc = 0;
    _DAT_00578df4 = 0;
    _DAT_0057351c = 0x380028;
    _DAT_00573520 = 0xe0;
    _DAT_00573522 = 0x68;
    DAT_0057352a = 0x20;
    _DAT_00573538 = 0x1070c28;
    _DAT_0057353c = 0x1000000;
    _DAT_00562a50 = 2;
  }
  uVar3 = (*(byte *)(_DAT_00573338 + 0x150) - 8) * 0x100 - _DAT_0060492a & 0xfff;
  if (uVar3 < 0x800) {
    sVar4 = (short)uVar3 >> 2;
  }
  else {
    sVar4 = -(short)(0x1000 - (short)uVar3 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar4;
  _DAT_005e93a0 = _DAT_005e93a0 + (short)(0x60 - _DAT_005e93a0 >> 2);
  _DAT_005e93a2 = _DAT_005e93a2 + (short)(0x60 - _DAT_005e93a2 >> 2);
  _DAT_005e93a4 = _DAT_005e93a4 + (short)(0x60 - _DAT_005e93a4 >> 2);
  func_0x0051c300(&DAT_00604928,&DAT_006008e0);
  func_0x00545aa0((int)_DAT_005e93a0,(int)_DAT_005e93a2,(int)_DAT_005e93a4);
  _DAT_00572b7a =
       _DAT_00572b7a +
       (short)((int)((uint)*(byte *)(_DAT_00573338 + 0x150) * -0x100 - (int)_DAT_00572b7a) >> 2);
  if (_DAT_00562a4c != 0) goto LAB_0050820a;
  iVar5 = func_0x00545ab2();
  if (iVar5 == 0) {
    uVar8 = (_DAT_00578df4 << 8 | _DAT_00578dfc) << 8 | _DAT_00578df8;
    if ((*otBase & 0x80000) != 0) {
      _DAT_00578df8 = 0;
      _DAT_00578dfc = 7;
    }
    if ((*(byte *)((int)otBase + 6) & 0x80) != 0) {
      if (_DAT_00578df8 == 0) {
        if ((_DAT_00578dfc != 0) || (_DAT_00578df4 != 0)) {
          _DAT_00578df8 = 9;
          *(byte *)((int)otBase + 6) = *(byte *)((int)otBase + 6) | 0x10;
        }
      }
      else {
        _DAT_00578df8 = _DAT_00578df8 - 1;
      }
    }
    if ((*(byte *)((int)otBase + 6) & 0x20) != 0) {
      if ((int)_DAT_00578df8 < 9) {
        _DAT_00578df8 = _DAT_00578df8 + 1;
      }
      else if (((int)_DAT_00578dfc < 7) || (_DAT_00578df4 < 7)) {
        _DAT_00578df8 = 0;
        *(ushort *)((int)otBase + 6) = *(ushort *)((int)otBase + 6) | 0x40;
      }
    }
    if ((*(byte *)((int)otBase + 6) & 0x10) != 0) {
      if (_DAT_00578dfc == 0) {
        if (_DAT_00578df4 != 0) {
          _DAT_00578df4 = _DAT_00578df4 + -1;
        }
      }
      else {
        _DAT_00578dfc = _DAT_00578dfc - 1;
      }
    }
    if (((*(byte *)((int)otBase + 6) & 0x40) != 0) && ((int)_DAT_00578dfc < 7)) {
      _DAT_00578dfc = _DAT_00578dfc + 1;
    }
    if (_DAT_00578dfc == 7) {
      _DAT_00578df8 = 0;
    }
    if (uVar8 != ((_DAT_00578df4 << 8 | _DAT_00578dfc) << 8 | _DAT_00578df8)) {
      func_0x0053db60();
    }
    if ((*otBase & 0x80000000) == 0) {
      if ((*otBase & 0x40000000) == 0) goto LAB_0050820a;
      if (_DAT_00578e00 != 0) {
        _DAT_00578e00 = _DAT_00578e00 + -1;
        *(undefined1 *)(_DAT_00573338 + 0x100 + _DAT_00578e00) = 0;
        func_0x0053dba0();
        goto LAB_0050820a;
      }
      if ((_DAT_00578dfc != 7) || (_DAT_00578df4 != 7)) goto LAB_0050820a;
      goto LAB_005081ec;
    }
    if (_DAT_00578dfc != 7) {
      if (7 < _DAT_00578e00) goto LAB_0050820a;
      bVar2 = *(byte *)(*(int *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x50 +
                                (_DAT_00578dfc + _DAT_00578df4) * 4) + _DAT_00578df8 * 3 +
                       (((int)_DAT_00578df8 < 5) - 1 & 2));
      if ((_DAT_00578e00 == 0) && ((uint)bVar2 == (_DAT_00562c9c & 0xffff))) {
        func_0x0053dbc0();
        goto LAB_0050820a;
      }
      func_0x0053db80();
      *(byte *)(_DAT_00573338 + 0x100 + _DAT_00578e00) = bVar2;
      _DAT_00578e00 = _DAT_00578e00 + 1;
      *(undefined1 *)(_DAT_00573338 + 0x100 + _DAT_00578e00) = 0;
LAB_005081a3:
      if (_DAT_00578e00 == 8) {
        _DAT_00578df8 = 0;
        _DAT_00578dfc = 7;
      }
      goto LAB_0050820a;
    }
    if (_DAT_00578e00 == 0) {
      pcVar10 = *(char **)(*(int *)(_DAT_005e97c4 + 0x70) + 0x208 +
                          (uint)*(byte *)(_DAT_00573338 + 0x150) * 4);
      cVar1 = *pcVar10;
      _DAT_00578e00 = 0;
      while (cVar1 != '\0') {
        pcVar10 = pcVar10 + 1;
        *(char *)(_DAT_00578e00 + 0x100 + _DAT_00573338) = cVar1;
        _DAT_00578e00 = _DAT_00578e00 + 1;
        cVar1 = *pcVar10;
      }
      *(undefined1 *)(_DAT_00578e00 + 0x100 + _DAT_00573338) = 0;
      _DAT_00578e00 = _DAT_00578e00 >> 1;
    }
    func_0x0053db80();
    _DAT_00578df0 = 0x5f;
    _DAT_00578de8 = _DAT_00578de8 + 2;
  }
  else {
    if (iVar5 == 0xd) {
      _DAT_00578dfc = 7;
      if (_DAT_00578e00 == 0) {
        pcVar10 = *(char **)(*(int *)(_DAT_005e97c4 + 0x70) + 0x208 +
                            (uint)*(byte *)(_DAT_00573338 + 0x150) * 4);
        cVar1 = *pcVar10;
        _DAT_00578e00 = 0;
        while (cVar1 != '\0') {
          pcVar10 = pcVar10 + 1;
          *(char *)(_DAT_00578e00 + 0x100 + _DAT_00573338) = cVar1;
          _DAT_00578e00 = _DAT_00578e00 + 1;
          cVar1 = *pcVar10;
        }
        *(undefined1 *)(_DAT_00578e00 + 0x100 + _DAT_00573338) = 0;
        _DAT_00578e00 = _DAT_00578e00 >> 1;
      }
      func_0x0053db80();
      _DAT_00578de8 = _DAT_00578de8 + 2;
      _DAT_00578df0 = 0x5f;
      func_0x00545ab8(0);
      func_0x00523520();
      goto LAB_0050820a;
    }
    if (iVar5 == 8) {
      if (_DAT_00578e00 != 0) {
        _DAT_00578e00 = _DAT_00578e00 + -1;
        *(undefined1 *)(_DAT_00573338 + 0x100 + _DAT_00578e00) = 0;
        func_0x0053dba0();
      }
      goto LAB_0050820a;
    }
    if (iVar5 != 0x1b) {
      if ((iVar5 < 0x20) || (((0x7f < iVar5 && (iVar5 < 0xa0)) || (7 < _DAT_00578e00))))
      goto LAB_0050820a;
      func_0x0053db80();
      *(char *)(_DAT_00573338 + 0x100 + _DAT_00578e00) = (char)iVar5;
      _DAT_00578e00 = _DAT_00578e00 + 1;
      *(undefined1 *)(_DAT_00573338 + 0x100 + _DAT_00578e00) = 0;
      goto LAB_005081a3;
    }
LAB_005081ec:
    func_0x0053dba0();
    _DAT_00578df0 = 0;
    _DAT_00578de8 = _DAT_00578de8 + -1;
  }
  func_0x00545ab8(0);
LAB_0050820a:
  func_0x00544350(0x5606f8,0,0x5618b8,0);
  uVar6 = func_0x005443a0(unaff_retaddr,0x5618b8);
  uVar8 = 0xffffffff;
  pcVar10 = (char *)(_DAT_00573338 + 0x100);
  do {
    pcVar12 = pcVar10;
    if (uVar8 == 0) break;
    uVar8 = uVar8 - 1;
    pcVar12 = pcVar10 + 1;
    cVar1 = *pcVar10;
    pcVar10 = pcVar12;
  } while (cVar1 != '\0');
  uVar8 = ~uVar8;
  pcVar10 = pcVar12 + -uVar8;
  pcVar12 = acStack_20;
  for (uVar9 = uVar8 >> 2; uVar9 != 0; uVar9 = uVar9 - 1) {
    *(undefined4 *)pcVar12 = *(undefined4 *)pcVar10;
    pcVar10 = pcVar10 + 4;
    pcVar12 = pcVar12 + 4;
  }
  for (uVar8 = uVar8 & 3; uVar8 != 0; uVar8 = uVar8 - 1) {
    *pcVar12 = *pcVar10;
    pcVar10 = pcVar10 + 1;
    pcVar12 = pcVar12 + 1;
  }
  if (_DAT_00578dfc != 7) {
    uVar8 = 0xffffffff;
    pcVar10 = (char *)0x562c98;
    do {
      pcVar12 = pcVar10;
      if (uVar8 == 0) break;
      uVar8 = uVar8 - 1;
      pcVar12 = pcVar10 + 1;
      cVar1 = *pcVar10;
      pcVar10 = pcVar12;
    } while (cVar1 != '\0');
    uVar8 = ~uVar8;
    iVar5 = -1;
    pcVar10 = acStack_20;
    do {
      pcVar11 = pcVar10;
      if (iVar5 == 0) break;
      iVar5 = iVar5 + -1;
      pcVar11 = pcVar10 + 1;
      cVar1 = *pcVar10;
      pcVar10 = pcVar11;
    } while (cVar1 != '\0');
    pcVar10 = pcVar12 + -uVar8;
    pcVar12 = pcVar11 + -1;
    for (uVar9 = uVar8 >> 2; uVar9 != 0; uVar9 = uVar9 - 1) {
      *(undefined4 *)pcVar12 = *(undefined4 *)pcVar10;
      pcVar10 = pcVar10 + 4;
      pcVar12 = pcVar12 + 4;
    }
    for (uVar8 = uVar8 & 3; uVar8 != 0; uVar8 = uVar8 - 1) {
      *pcVar12 = *pcVar10;
      pcVar10 = pcVar10 + 1;
      pcVar12 = pcVar12 + 1;
    }
  }
  func_0x00544350(acStack_20,0,0x5618b8,0);
  uVar6 = func_0x005443a0(uVar6,0x5618b8);
  func_0x0051be20(uStack_8,&stack0x00000000);
  uVar6 = func_0x00521df0(uStack_8,uVar6,&stack0xffffffcc,0,0);
  uVar6 = func_0x0051f9c0(uStack_8,uVar6,&stack0xffffffcc);
  func_0x0051bdb0(uStack_8,&stack0x00000000);
  func_0x00544350(0x5606f8,0,0x573518,0);
  uVar6 = func_0x005443a0(uVar6,0x573518);
  func_0x00507870();
  iVar5 = 0;
  do {
    func_0x00544350(*(undefined4 *)
                     (*(int *)(_DAT_005e97c4 + 0x70) + 0x50 + (iVar5 + _DAT_00578df4) * 4),0,
                    0x573518,0);
    uVar6 = func_0x005443a0(uVar6,0x573518);
    iVar5 = iVar5 + 1;
  } while (iVar5 < 8);
  puVar7 = (uint *)func_0x00545340(uStack_8,uVar6,0x573518);
  func_0x00507890();
  return puVar7;
}


