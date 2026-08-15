/*
 * Function: FUN_00509470
 * Entry:    00509470
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_00509470(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  char cVar1;
  short sVar2;
  undefined2 *puVar3;
  int iVar4;
  char *pcVar5;
  uint *puVar6;
  char cVar7;
  int iVar8;
  int iVar9;
  short *psVar10;
  undefined4 unaff_retaddr;
  undefined4 uStack_c;
  
  iVar9 = 0;
  iVar8 = _DAT_00578e04;
  if (_DAT_00562a48 != 0) {
    _DAT_00562a48 = 0;
    DAT_005e906a = 0xf;
    DAT_005e909f = '\0';
    _DAT_00578e04 = 0;
    puVar3 = (undefined2 *)0x57336a;
    iVar8 = 3;
    do {
      puVar3[-1] = 0x1000;
      *puVar3 = 0x1000;
      *(undefined4 *)(puVar3 + 7) = 0x1000000;
      *(undefined1 *)(puVar3 + 0xb) = 0;
      puVar3[-0xd] = 0;
      puVar3 = puVar3 + 0x4e;
      iVar8 = iVar8 + -1;
    } while (iVar8 != 0);
    iVar4 = 0;
    iVar8 = **(int **)(_DAT_005e97c4 + 0x74) + (*(byte *)(_DAT_00573338 + 0x150) & 7) * 8;
    _DAT_00578de4 = iVar8;
    do {
      *(undefined1 *)(iVar4 + 0x5e9060) = *(undefined1 *)(iVar8 + iVar4);
      iVar4 = iVar4 + 1;
    } while (iVar4 < 7);
    _DAT_00562a50 = 3;
    iVar8 = 0;
  }
  if (_DAT_00562a4c != 0) goto LAB_0050965e;
  switch(_DAT_00578df0) {
  case 0x39:
    if (((*(ushort *)(pad + 2) & 0x4000) == 0) || (iVar8 != 0)) {
      if (((*(ushort *)(pad + 2) & 0x8000) != 0) && (iVar8 < 3)) {
        func_0x0053db80();
        *(undefined2 *)(_DAT_00578e04 * 0x9c + 0x573350) = 0;
        iVar8 = _DAT_00578e04 + 1;
        _DAT_00578e04 = iVar8;
      }
      if (0 < iVar8) {
        psVar10 = (short *)0x573350;
        do {
          sVar2 = func_0x00545a16((int)*psVar10);
          psVar10[0xc] = sVar2;
          if (*psVar10 == 0x400) {
            iVar8 = func_0x00547140();
            *(char *)(psVar10 + 0x18) = (char)(iVar8 % 6) + '\x01';
          }
          if (*psVar10 < 0x800) {
            *psVar10 = *psVar10 + 0x100;
          }
          iVar9 = iVar9 + 1;
          psVar10 = psVar10 + 0x4e;
          iVar8 = _DAT_00578e04;
        } while (iVar9 < _DAT_00578e04);
      }
      if (iVar8 == 3) {
        iVar8 = 0;
        cVar7 = '\0';
        pcVar5 = (char *)0x573380;
        do {
          if (*(short *)(pcVar5 + -0x30) != 0x800) break;
          cVar1 = *pcVar5;
          pcVar5 = pcVar5 + 0x9c;
          cVar7 = cVar7 + cVar1;
          iVar8 = iVar8 + 1;
        } while (iVar8 < 3);
        if (iVar8 == 3) {
          DAT_005e906a = DAT_005e906a + cVar7;
          if (((cRam00573380 == cRam0057341c) && (cRam0057341c == cRam005734b8)) &&
             (DAT_005e906a < 99)) {
            _DAT_00578df0 = _DAT_00578df0 + 1;
          }
          else {
            if (99 < DAT_005e906a) {
              DAT_005e906a = 99;
            }
            _DAT_00578df0 = _DAT_00578df0 + 2;
          }
        }
      }
    }
    else {
      func_0x0053dba0();
      _DAT_00578df0 = 0x2e;
      _DAT_00578de8 = _DAT_00578de8 + -1;
    }
    break;
  case 0x3a:
    if ((DAT_00560fef & 2) == 0) {
      iVar8 = 3;
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = 0x39;
      _DAT_00578e04 = 0;
      puVar3 = (undefined2 *)0x57336a;
      do {
        puVar3[-1] = 0x1000;
        *puVar3 = 0x1000;
        *(undefined4 *)(puVar3 + 7) = 0x1000000;
        *(undefined1 *)(puVar3 + 0xb) = 0;
        puVar3[-0xd] = 0;
        puVar3 = puVar3 + 0x4e;
        iVar8 = iVar8 + -1;
      } while (iVar8 != 0);
    }
    break;
  case 0x3b:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = _DAT_00578df0 + 1;
      break;
    }
    if ((pad[3] & 0x40) == 0) break;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
    goto code_r0x005098ba;
  case 0x3c:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        _DAT_00578df0 = 0x3e;
        _DAT_00578de8 = _DAT_00578de8 + 1;
      }
      else {
        _DAT_00578df0 = 0x39;
        _DAT_00562a48 = 1;
      }
      break;
    }
    if ((pad[3] & 0x40) == 0) break;
    DAT_00560763 = 0;
    DAT_00560fef = DAT_00560fef & 0xfb;
code_r0x005098ba:
    func_0x0053dba0();
    _DAT_00578df0 = 0x39;
    _DAT_00562a48 = 1;
  }
LAB_0050965e:
  _DAT_005e93a0 = _DAT_005e93a0 + (short)(0x60 - _DAT_005e93a0 >> 2);
  _DAT_005e93a2 = _DAT_005e93a2 + (short)(0x60 - _DAT_005e93a2 >> 2);
  _DAT_005e93a4 = _DAT_005e93a4 + (short)(0x60 - _DAT_005e93a4 >> 2);
  func_0x00545aa0((int)_DAT_005e93a0,(int)_DAT_005e93a2,(int)_DAT_005e93a4);
  iVar8 = 0;
  do {
    func_0x00545a1c(iVar8 + -400);
    unaff_retaddr = func_0x00521f20(uStack_c,unaff_retaddr,&stack0xffffffdc,0,0);
    iVar8 = iVar8 + 400;
  } while (iVar8 < 0x4b0);
  func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0xf4),0,0x562180,0);
  puVar6 = (uint *)func_0x005443a0(unaff_retaddr,0x562180);
  return puVar6;
}


