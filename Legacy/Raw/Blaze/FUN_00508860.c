/*
 * Function: FUN_00508860
 * Entry:    00508860
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_00508860(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  ushort uVar1;
  short sVar2;
  undefined4 uVar3;
  uint *puVar4;
  uint uVar5;
  int iVar6;
  undefined4 unaff_retaddr;
  undefined4 uStack_8;
  
  if (_DAT_00562a48 != 0) {
    _DAT_00573522 = 0x28;
    _DAT_00562a48 = 0;
    _DAT_00573de4 = 0x68;
    _DAT_00573de6 = 0x68;
    _DAT_00578df8 = 0;
    _DAT_00578dfc = 0;
    _DAT_0057351c = 0x380028;
    _DAT_00573520 = 0xe0;
    _DAT_00573538 = 0x1070c24;
    _DAT_0057353c = 0x1000000;
    _DAT_00573de8 = 0x60;
    DAT_00573df2 = 0xb;
    _DAT_00573dea = 0x18;
    DAT_00573df4 = 5;
    _DAT_00573e00 = 0x1070c10;
    _DAT_00573e04 = 0x1000000;
    _DAT_00562a50 = 2;
    *(undefined1 *)(_DAT_00573338 + 0x153) =
         *(undefined1 *)(*(byte *)(_DAT_00573338 + 0x150) + 0x562b70);
  }
  uVar1 = (*(byte *)(_DAT_00573338 + 0x150) - 8) * 0x100 - _DAT_0060492a & 0xfff;
  if (uVar1 < 0x800) {
    sVar2 = (short)uVar1 >> 2;
  }
  else {
    sVar2 = -(short)(0x1000 - (short)uVar1 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar2;
  _DAT_005e93a0 = _DAT_005e93a0 + (short)(0x60 - _DAT_005e93a0 >> 2);
  _DAT_005e93a2 = _DAT_005e93a2 + (short)(0x60 - _DAT_005e93a2 >> 2);
  _DAT_005e93a4 = _DAT_005e93a4 + (short)(0x60 - _DAT_005e93a4 >> 2);
  func_0x0051c300(&DAT_00604928,&DAT_006008e0);
  func_0x00545aa0((int)_DAT_005e93a0,(int)_DAT_005e93a2,(int)_DAT_005e93a4);
  _DAT_00572b7a =
       _DAT_00572b7a +
       (short)((int)((uint)*(byte *)(_DAT_00573338 + 0x150) * -0x100 - (int)_DAT_00572b7a) >> 2);
  if (_DAT_00562a4c != 0) goto LAB_00508c6b;
  switch(_DAT_00578df0) {
  case 0x5f:
    uVar1 = *(ushort *)((int)otBase + 2);
    if ((uVar1 & 8) == 0) {
      if ((uVar1 & 0x4000) == 0) {
        if ((uVar1 & 0x8000) == 0) {
          uVar5 = _DAT_00578dfc << 8 | _DAT_00578df8;
          if (((*(byte *)((int)otBase + 6) & 0x10) != 0) && (_DAT_00578dfc != 0)) {
            _DAT_00578dfc = _DAT_00578dfc + -1;
          }
          if (((*(byte *)((int)otBase + 6) & 0x40) != 0) && (_DAT_00578dfc < 1)) {
            _DAT_00578dfc = _DAT_00578dfc + 1;
          }
          if ((*(byte *)((int)otBase + 6) & 0x80) != 0) {
            if (_DAT_00578df8 == 0) {
              if (_DAT_00578dfc != 0) {
                _DAT_00578dfc = _DAT_00578dfc + -1;
                _DAT_00578df8 = 3;
              }
            }
            else {
              _DAT_00578df8 = _DAT_00578df8 - 1;
            }
          }
          if ((*(byte *)((int)otBase + 6) & 0x20) != 0) {
            if ((int)_DAT_00578df8 < 3) {
              _DAT_00578df8 = _DAT_00578df8 + 1;
            }
            else if (_DAT_00578dfc < 1) {
              _DAT_00578dfc = _DAT_00578dfc + 1;
              _DAT_00578df8 = 0;
            }
          }
          if (uVar5 != (_DAT_00578dfc << 8 | _DAT_00578df8)) {
            func_0x0053db60();
          }
        }
        else {
          *(ushort *)((int)otBase + 2) = uVar1 & 0x7fff;
          func_0x0053db80();
          _DAT_00578df0 = 0x61;
          *(byte *)(_DAT_00573338 + 0x153) =
               (DAT_00578dfc << 2 | DAT_00578df8) << 1 | *(byte *)(_DAT_00573338 + 0x153) & 1;
        }
      }
      else {
        func_0x0053dba0();
        _DAT_00578df0 = 0x13;
        _DAT_00578de8 = _DAT_00578de8 + -2;
      }
    }
    else {
      func_0x0053db80();
      _DAT_00578df0 = 0x61;
    }
    break;
  case 0x60:
    if ((DAT_00560fef & 4) == 0) {
      *(ushort *)((int)otBase + 2) = *(ushort *)((int)otBase + 2) & 0x7fff;
      _DAT_00578df0 = _DAT_00578df0 + 1;
      *(byte *)(_DAT_00573338 + 0x153) = (DAT_00578dfc << 2 | DAT_00578df8) << 1 | DAT_005e909f;
    }
    else if ((*otBase & 0x40000000) != 0) {
      DAT_00560763 = 0;
      DAT_00560fef = DAT_00560fef & 0xfb;
      func_0x0053dba0();
      _DAT_00578df0 = _DAT_00578df0 + -1;
    }
    break;
  case 0x61:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)((int)otBase + 2) = *(ushort *)((int)otBase + 2) & 0x7fff;
      _DAT_00578df0 = _DAT_00578df0 + 1;
      break;
    }
    if ((*otBase & 0x40000000) == 0) break;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
code_r0x00508c5c:
    func_0x0053dba0();
    goto code_r0x00508c61;
  case 0x62:
    if ((DAT_00560fef & 4) != 0) {
      if ((*otBase & 0x40000000) == 0) break;
      DAT_00560763 = 0;
      DAT_00560fef = DAT_00560fef & 0xfb;
      goto code_r0x00508c5c;
    }
    if (DAT_005e909f == 0) {
      DAT_00573df4 = 4;
      _DAT_00578de8 = _DAT_00578de8 + 1;
      _DAT_00578df0 = 0x2e;
      break;
    }
code_r0x00508c61:
    _DAT_00578df0 = 0x5f;
  }
LAB_00508c6b:
  func_0x0051be20(uStack_8,&stack0x00000000);
  uVar3 = func_0x00521df0(uStack_8,unaff_retaddr,&stack0xffffffe0,0,0);
  uVar3 = func_0x0051f9c0(uStack_8,uVar3,&stack0xffffffe0);
  func_0x0051bdb0(uStack_8,&stack0x00000000);
  func_0x00544350(0x5606f8,0,0x573518,0);
  uVar3 = func_0x005443a0(uVar3,0x573518);
  iVar6 = 0x500;
  do {
    func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + iVar6),0,0x573518,0);
    uVar3 = func_0x005443a0(uVar3,0x573518);
    iVar6 = iVar6 + 4;
  } while (iVar6 < 0x508);
  puVar4 = (uint *)func_0x00545340(uStack_8,uVar3,0x573518);
  if (_DAT_00578df0 != 0x2e) {
    func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x18c),0x572b48,0x573de0,0);
    uVar3 = func_0x005443a0(puVar4,0x573de0);
    puVar4 = (uint *)func_0x00545340(uStack_8,uVar3,0x573de0);
  }
  return puVar4;
}


