/*
 * Function: FUN_00508e00
 * Entry:    00508e00
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_00508e00(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  char cVar1;
  ushort uVar2;
  short sVar3;
  int iVar4;
  uint *puVar5;
  short unaff_BP;
  int iVar6;
  undefined4 *puVar7;
  undefined4 *puVar8;
  short *psVar9;
  undefined4 unaff_retaddr;
  undefined1 auStack_34 [4];
  int iStack_30;
  short sStack_16;
  ushort uStack_12;
  int iStack_8;
  
  iVar6 = 0;
  if (_DAT_00562a48 != 0) {
    _DAT_00562a48 = 0;
    _DAT_00604928 = 0x15e;
    _DAT_0060492c = 0;
    uVar2 = *(byte *)(_DAT_00573338 + 0x151) - 4 & 7;
    _DAT_0060492a = uVar2 * 0x200;
    _DAT_00572b78 = 0;
    _DAT_00572b7a = uVar2 * -0x200 + 0x800;
    _DAT_00572b7c = 0;
    puVar7 = (undefined4 *)0x572e78;
    do {
      iVar4 = func_0x00545a1c(iVar6 << 9);
      *puVar7 = 0;
      puVar7[-1] = iVar4 * -0x78;
      iVar4 = func_0x00545a16(iVar6 << 9);
      puVar7[6] = 0x1000000;
      *(undefined2 *)((int)puVar7 + 0x16) = 1;
      puVar7[1] = iVar4 * 0x78;
      *(undefined2 *)(puVar7 + 2) = 0;
      *(undefined2 *)((int)puVar7 + 10) = 0;
      *(undefined2 *)(puVar7 + 3) = 0;
      iVar6 = iVar6 + 1;
      puVar7 = puVar7 + 0x27;
    } while (iVar6 < 8);
    _DAT_00562a50 = 3;
  }
  if (_DAT_00562a4c == 0) {
    if (_DAT_00578df0 == 0x2e) {
      uVar2 = *(ushort *)(pad + 2);
      if ((uVar2 & 0x8000) == 0) {
        if ((uVar2 & 0x4000) == 0) {
          cVar1 = *(char *)(_DAT_00573338 + 0x151);
          if ((pad[6] & 0x80) != 0) {
            *(char *)(_DAT_00573338 + 0x151) = cVar1 + -1;
          }
          if ((pad[6] & 0x20) != 0) {
            *(char *)(_DAT_00573338 + 0x151) = *(char *)(_DAT_00573338 + 0x151) + '\x01';
          }
          *(byte *)(_DAT_00573338 + 0x151) = *(byte *)(_DAT_00573338 + 0x151) & 7;
          if (cVar1 != *(char *)(_DAT_00573338 + 0x151)) {
            func_0x0053db60();
          }
        }
        else {
          sVar3 = *(byte *)(_DAT_00573338 + 0x150) + 8;
          _DAT_0060492a = sVar3 * 0x100;
          _DAT_00572b7a = sVar3 * -0x100 + 0x800;
          _DAT_00578df0 = 0x5f;
          func_0x0053dba0();
          _DAT_00578de8 = _DAT_00578de8 + -1;
        }
      }
      else {
        *(ushort *)(pad + 2) = uVar2 & 0x7fff;
        func_0x0053db80();
        _DAT_00578df0 = _DAT_00578df0 + 1;
      }
    }
    else if (_DAT_00578df0 == 0x2f) {
      if ((DAT_00560fef & 2) == 0) {
        *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
        _DAT_00578df0 = _DAT_00578df0 + 1;
      }
      else if ((pad[3] & 0x40) != 0) {
        DAT_0056075b = 0;
        DAT_00560fef = DAT_00560fef & 0xfd;
        func_0x0053dba0();
        _DAT_00578df0 = 0x2e;
      }
    }
    else if (_DAT_00578df0 == 0x30) {
      if ((DAT_00560fef & 4) == 0) {
        if (DAT_005e909f == '\0') {
          _DAT_00578df0 = 0x39;
          puVar7 = (undefined4 *)0x572c20;
          do {
            func_0x005352e0((uint)*(byte *)(_DAT_00573338 + 0x151) * 0x9c + 0x572e58,puVar7 + -0xf,
                            0x27);
            *puVar7 = *(undefined4 *)
                       (*(int *)(_DAT_005e97c4 + 0x94) + 8 +
                       (uint)*(byte *)(_DAT_00573338 + 0x151) * 4);
            uStack_12 = func_0x00547140();
            iVar6 = func_0x00545a1c((int)(short)uStack_12);
            puVar7[5] = iVar6 * 0x1e;
            iVar6 = func_0x00547140();
            puVar7[6] = iVar6 * 8 + -0x1fff8;
            iVar6 = func_0x00545a16((int)sStack_16);
            puVar8 = puVar7 + 0x27;
            puVar7[7] = iVar6 * 0x1e;
            puVar7 = puVar8;
          } while ((int)puVar8 < 0x572e90);
          _DAT_00578de8 = _DAT_00578de8 + 1;
        }
        else {
          _DAT_00578df0 = 0x2e;
        }
      }
      else if ((pad[3] & 0x40) != 0) {
        DAT_00560763 = 0;
        DAT_00560fef = DAT_00560fef & 0xfb;
        func_0x0053dba0();
        _DAT_00578df0 = 0x2e;
      }
    }
  }
  uStack_12 = (*(byte *)(_DAT_00573338 + 0x151) - 4) * 0x200 - _DAT_0060492a & 0xfff;
  if (uStack_12 < 0x800) {
    sVar3 = (short)uStack_12 >> 2;
  }
  else {
    sVar3 = -(short)(0x1000 - (short)uStack_12 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar3;
  _DAT_005e9308 = _DAT_005e9308 + (short)(0x200 - _DAT_005e9308 >> 2);
  _DAT_005e930a = _DAT_005e930a + (short)(0x380 - _DAT_005e930a >> 2);
  _DAT_005e930c = _DAT_005e930c + (short)(-(int)_DAT_005e930c >> 2);
  _DAT_005e93a0 =
       _DAT_005e93a0 +
       (short)((int)*(short *)((uint)*(byte *)(_DAT_00573338 + 0x151) * 8 + 0x562a58) -
               (int)_DAT_005e93a0 >> 2);
  _DAT_005e93a2 =
       _DAT_005e93a2 +
       (short)((int)*(short *)((uint)*(byte *)(_DAT_00573338 + 0x151) * 8 + 0x562a5a) -
               (int)_DAT_005e93a2 >> 2);
  _DAT_005e93a4 =
       _DAT_005e93a4 +
       (short)((int)*(short *)((uint)*(byte *)(_DAT_00573338 + 0x151) * 8 + 0x562a5c) -
               (int)_DAT_005e93a4 >> 2);
  func_0x0051c300(&DAT_00604928,&DAT_006008e0);
  func_0x00545aa0((int)_DAT_005e93a0,(int)_DAT_005e93a2,(int)_DAT_005e93a4);
  otBase = (uint *)0x0;
  _DAT_00572b7a = 0x800 - _DAT_0060492a;
  psVar9 = (short *)0x572e80;
  iVar6 = 8;
  do {
    func_0x005352e0(psVar9 + -6,auStack_34,10);
    iStack_30 = iStack_30 + 0x28000;
    sVar3 = *psVar9;
    if (otBase == (uint *)(uint)*(byte *)(_DAT_00573338 + 0x151)) {
      psVar9[8] = 0;
      psVar9[9] = 0x100;
      *psVar9 = (short)(0x1800 - sVar3 >> 2) + sVar3;
      psVar9[1] = (short)(0x1800 - psVar9[1] >> 2) + psVar9[1];
    }
    else {
      psVar9[8] = 0;
      psVar9[9] = 0x300;
      *psVar9 = (short)(0x1000 - sVar3 >> 2) + sVar3;
      psVar9[1] = (short)(0x1000 - psVar9[1] >> 2) + psVar9[1];
    }
    iVar4 = func_0x0051f650(auStack_34,&stack0xffffffb0);
    if (iVar4 != 0) {
      iVar4 = *(int *)(*(int *)(_DAT_005e97c4 + 0x94) + iVar6);
      sVar3 = (short)((ulonglong)(_DAT_005e9960 >> 1) % 6);
      *(char *)(iVar4 + 2 + sVar3 * 8) = *(char *)(iVar4 + 2 + sVar3 * 8) + -1;
      *(char *)(iVar4 + 3 + sVar3 * 8) = *(char *)(iVar4 + 3 + sVar3 * 8) + -1;
      unaff_retaddr = func_0x00521f20(iStack_8 + unaff_BP * 4,unaff_retaddr,&stack0xffffffb8,0,0);
      *(char *)(iVar4 + 2 + sVar3 * 8) = *(char *)(iVar4 + 2 + sVar3 * 8) + '\x01';
      *(char *)(iVar4 + 3 + sVar3 * 8) = *(char *)(iVar4 + 3 + sVar3 * 8) + '\x01';
    }
    iVar6 = iVar6 + 4;
    otBase = (uint *)((int)otBase + 1);
    psVar9 = psVar9 + 0x4e;
  } while (iVar6 < 0x28);
  func_0x00544350(*(undefined4 *)
                   (*(int *)(_DAT_005e97c4 + 0x70) + 0xc4 +
                   (uint)*(byte *)(_DAT_00573338 + 0x151) * 4),0,0x562180,0);
  puVar5 = (uint *)func_0x005443a0(unaff_retaddr,0x562180);
  return puVar5;
}


