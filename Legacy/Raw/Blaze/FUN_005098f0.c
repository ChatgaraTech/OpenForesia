/*
 * Function: FUN_005098f0
 * Entry:    005098f0
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_005098f0(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  char *pcVar1;
  byte *pbVar2;
  char cVar3;
  byte bVar4;
  ushort uVar5;
  short sVar6;
  uint uVar7;
  int iVar8;
  short *psVar9;
  int iVar10;
  int iVar11;
  char *pcVar12;
  int iVar13;
  undefined4 uStack_14;
  undefined4 uStack_10;
  short sStack_c;
  short sStack_a;
  short sStack_4;
  
  if (_DAT_00562a48 != 0) {
    iVar10 = 0;
    _DAT_00562a48 = 0;
    _DAT_00578e08 = 0;
    iVar13 = 0;
    iVar8 = 0x573518;
    do {
      iVar11 = iVar8;
      iVar8 = func_0x00545a16(iVar13);
      *(short *)(iVar11 + 4) = (short)(iVar8 * -0x58 >> 0xc) + 0x88;
      iVar8 = func_0x00545a1c(iVar13);
      *(short *)(iVar11 + 6) = (short)((iVar8 << 6) >> 0xc) + 0x48;
      *(undefined2 *)(iVar11 + 8) = 0x20;
      *(undefined2 *)(iVar11 + 10) = 0x18;
      *(undefined4 *)(iVar11 + 0x10) = 0x1180000;
      *(int *)(iVar11 + 0x14) = iVar10 + 3;
      *(undefined4 *)(iVar11 + 0x18) = 0x10f0808;
      *(undefined4 *)(iVar11 + 0x1c) = 0x2cf1020;
      *(undefined4 *)(iVar11 + 0x20) = 0x1070c10;
      *(undefined4 *)(iVar11 + 0x24) = 0x1000000;
      *(undefined4 *)(iVar11 + 0x28) = 0;
      *(undefined4 *)(iVar11 + 0x30) = 0;
      *(undefined4 *)(iVar11 + 0x38) = 0;
      *(undefined4 *)(iVar11 + 0x40) = 0x10f0000;
      *(undefined4 *)(iVar11 + 0x44) = 0x1000000;
      *(undefined4 *)(iVar11 + 0x48) = 0;
      *(undefined4 *)(iVar11 + 0x50) = 0;
      *(undefined4 *)(iVar11 + 0x58) = 0;
      *(undefined4 *)(iVar11 + 0x60) = 0;
      iVar13 = iVar13 + 0x155;
      iVar10 = iVar10 + 1;
      iVar8 = iVar11 + 0x8c8;
    } while (iVar13 < 0x953);
    *(undefined2 *)(iVar11 + 0x8cc) = 0x58;
    *(undefined2 *)(iVar11 + 0x8ce) = 0x38;
    *(undefined2 *)(iVar11 + 0x8d0) = 0x28;
    *(undefined2 *)(iVar11 + 0x8d2) = 0x18;
    *(undefined1 *)(iVar11 + 0x8e8) = 0x10;
    *(undefined4 *)(iVar11 + 0x8dc) = 10;
    *(undefined2 *)(iVar11 + 0x1194) = 0xb0;
    *(undefined2 *)(iVar11 + 0x1196) = 0x38;
    *(undefined2 *)(iVar11 + 0x1198) = 0x28;
    *(undefined2 *)(iVar11 + 0x119a) = 0x18;
    *(undefined1 *)(iVar11 + 0x11b0) = 0x10;
    *(undefined4 *)(iVar11 + 0x11a4) = 0xb;
    switch(_DAT_00573338[0x150] & 7) {
    case 0:
    case 6:
      *(undefined2 *)(_DAT_00573338 + 0x148) = 0x10;
      *(undefined2 *)(_DAT_00573338 + 0x14a) = 2;
      break;
    case 1:
    case 3:
    case 7:
      *(undefined2 *)(_DAT_00573338 + 0x148) = 0xe;
      *(undefined2 *)(_DAT_00573338 + 0x14a) = 8;
      break;
    case 2:
    case 4:
    case 5:
      *(undefined2 *)(_DAT_00573338 + 0x148) = 0xf;
      *(undefined2 *)(_DAT_00573338 + 0x14a) = 4;
    }
  }
  pbVar2 = pad;
  iVar8 = _DAT_00578e08;
  if (_DAT_00562a4c != 0) goto LAB_0050a5e0;
  switch(_DAT_00578df0) {
  case 0x3e:
    uVar5 = *(ushort *)(pad + 2);
    if ((uVar5 & 0x4000) == 0) {
      if (((uVar5 & 0x8000) == 0) || (DAT_005e906a != '\0')) {
        if (((pad[6] & 0x80) != 0) && (_DAT_00578e08 != 0)) {
          _DAT_00578e08 = _DAT_00578e08 + -1;
        }
        if (((pad[6] & 0x20) != 0) && (_DAT_00578e08 < 6)) {
          _DAT_00578e08 = _DAT_00578e08 + 1;
        }
        if (iVar8 != _DAT_00578e08) {
          func_0x0053db60();
        }
        if ((((pbVar2[6] & 0x10) != 0) && (DAT_005e906a != '\0')) &&
           (*(byte *)(_DAT_00578e08 + 0x5e9060) < 99)) {
          DAT_005e906a = DAT_005e906a + -1;
          *(char *)(_DAT_00578e08 + 0x5e9060) = *(char *)(_DAT_00578e08 + 0x5e9060) + '\x01';
          func_0x0053db80();
        }
        if (((pbVar2[6] & 0x40) != 0) &&
           (*(byte *)(_DAT_00578de4 + _DAT_00578e08) < *(byte *)(_DAT_00578e08 + 0x5e9060))) {
          *(byte *)(_DAT_00578e08 + 0x5e9060) = *(byte *)(_DAT_00578e08 + 0x5e9060) - 1;
          DAT_005e906a = DAT_005e906a + '\x01';
          func_0x0053dba0();
        }
        uVar7 = uRam005e9064 >> 8;
        uRam005e9064 = CONCAT13((char)((int)((uRam005e9060 & 0xff) + (uRam005e9064 & 0xff)) >> 1) +
                                _DAT_00573338[0x148],(undefined3)uRam005e9064);
        bRam005e9068 = (char)((int)((uRam005e9060 >> 0x10 & 0xff) + (uVar7 & 0xff)) >> 1) +
                       _DAT_00573338[0x14a];
        func_0x0051be20(otBase,&primCursor);
        uStack_14 = **(undefined4 **)(_DAT_005e97c0 + 0x94);
        sStack_4 = 0x18;
        uStack_10 = 0x1000000;
        sStack_c = *(short *)(&DAT_0057351c + _DAT_00578e08 * 0x8c8);
        sStack_a = *(short *)(&DAT_0057351e + _DAT_00578e08 * 0x8c8) + 0xc;
        primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
        primCursor = (uint *)func_0x0051f9c0(otBase,primCursor,&uStack_14);
        func_0x0051bdb0(otBase,&primCursor);
        func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1e0 + _DAT_00578e08 * 4),
                        0,0x560728,0);
        primCursor = (uint *)func_0x005443a0(primCursor,0x560728);
      }
      else {
        *(ushort *)(pad + 2) = uVar5 & 0x7fff;
        func_0x0053db80();
        _DAT_00578df0 = 0x81;
        *_DAT_00573338 = 0;
        _DAT_00573338[1] = 0;
        _DAT_00573338[2] = 0;
        _DAT_00573338[3] = 0;
        *(undefined4 *)(_DAT_00573338 + 0x140) = 0;
        *(undefined2 *)(_DAT_00573338 + 0x144) = 1;
        *(undefined2 *)(_DAT_00573338 + 0x146) = 1;
        *(undefined4 *)(_DAT_00573338 + 0x158) = 0;
        *(undefined4 *)(_DAT_00573338 + 0x15c) = 0;
        *(uint *)(_DAT_00573338 + 0x114) = 1 << (_DAT_00573338[0x151] & 0x1f) | 0x800;
        *(short *)(_DAT_00573338 + 0x112) = (short)(1 << (_DAT_00573338[0x150] & 7));
        iVar8 = 0x2a0;
        iVar10 = 0;
        do {
          iVar13 = iVar8 + 2;
          *(ushort *)(_DAT_00573338 + iVar8 + -0x20) = (ushort)*(byte *)(iVar10 + 0x5e9060);
          *(ushort *)(_DAT_00573338 + iVar8) = (ushort)*(byte *)(iVar10 + 0x5e9060);
          iVar8 = iVar13;
          iVar10 = iVar10 + 1;
        } while (iVar13 < 0x2ae);
        *(ushort *)(_DAT_00573338 + 0x148) = (ushort)uRam005e9064._3_1_;
        *(ushort *)(_DAT_00573338 + 0x14a) = (ushort)bRam005e9068;
        *(undefined2 *)(_DAT_00573338 + 0x14c) = *(undefined2 *)(_DAT_00573338 + 0x148);
        *(undefined2 *)(_DAT_00573338 + 0x14e) = *(undefined2 *)(_DAT_00573338 + 0x14a);
        *(undefined4 *)(_DAT_00573338 + 0x26c) = 0;
        _DAT_00573338[0x2b0] = 0;
        _DAT_00573338[0x2b1] = 1;
        if (((_DAT_00573338[0x150] & 7) == 0) || ((_DAT_00573338[0x150] & 7) == 6)) {
          _DAT_00573338[0x2b2] = 2;
        }
        else {
          _DAT_00573338[0x2b2] = 0xff;
        }
        _DAT_00573338[0x2b3] = 0xff;
        _DAT_00573338[0x2b4] = 0xff;
        _DAT_00573338[0x2b5] = *(undefined1 *)(((byte)_DAT_00573338[0x150] & 7) + 0x562ab8);
        _DAT_00573338[0x2b6] = 0;
        _DAT_00573338[0x2b7] = 0;
        _DAT_00573338[0x2b8] = 0;
        _DAT_00573338[0x2b9] = 0;
        _DAT_00573338[0x2ba] = 0;
        _DAT_00573338[699] = 0;
        _DAT_00573338[0x160] = 1;
        _DAT_00573338[0x161] = 0;
        _DAT_00573338[0x162] = 0;
        _DAT_00573338[0x163] = 0;
        _DAT_00573338[0x168] = 1;
        _DAT_00573338[0x169] = 0;
        _DAT_00573338[0x16a] = 0;
        _DAT_00573338[0x16b] = 0;
        _DAT_00573338[0x170] = 1;
        _DAT_00573338[0x171] = 0;
        _DAT_00573338[0x172] = 0;
        _DAT_00573338[0x173] = 0;
        _DAT_00573338[0x178] = 3;
        _DAT_00573338[0x179] = 3;
        _DAT_00573338[0x17a] = 3;
        _DAT_00573338[0x17b] = 3;
        _DAT_00573338[0x17c] = 3;
        _DAT_00573338[0x17d] = 3;
        _DAT_00573338[0x17e] = 3;
        _DAT_00573338[0x1f00] = 0;
        _DAT_00573338[0x1f01] = 0;
        _DAT_00573338[0x1f02] = 0;
        _DAT_00573338[0x1f03] = 0;
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1d00] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1d40] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1d80] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1dc0] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1e00] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1e40] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1e80] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x1ec0] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 8;
          *(undefined4 *)(_DAT_00573338 + iVar8 + 0x1f04) = 0;
          *(undefined4 *)(_DAT_00573338 + iVar8 + 0x1f08) = 0xffffffff;
          iVar8 = iVar10;
        } while (iVar10 < 0x40);
        iVar10 = 0;
        iVar8 = 0x1fc0;
        do {
          iVar13 = iVar8 + 2;
          pbVar2 = (byte *)(iVar10 + 0x562ac0);
          iVar10 = iVar10 + 1;
          *(undefined2 *)(_DAT_00573338 + iVar8) = *(undefined2 *)((uint)*pbVar2 * 2 + 0x562ac8);
          iVar8 = iVar13;
        } while (iVar13 < 0x1fd0);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 1;
          _DAT_00573338[iVar8 + 0x2e0] = 0;
          iVar8 = iVar10;
        } while (iVar10 < 0x20);
        func_0x005352e0((((byte)_DAT_00573338[0x150] & 7) + 0x7c) * 0x2000 + *(int *)MainMem_exref,
                        _DAT_00573338 + 0x300,0x640);
        iVar8 = 0;
        do {
          iVar10 = iVar8 + 0x280;
          *(ushort *)(_DAT_00573338 + iVar8 + 0x312) =
               *(ushort *)(_DAT_00573338 + iVar8 + 0x312) | 0x8000;
          iVar8 = iVar10;
        } while (iVar10 < 0x1900);
        func_0x00538ab0(_DAT_00573338);
        func_0x00538f50(_DAT_00573338 + 0x100);
        func_0x00538c50(_DAT_00573338);
        func_0x00538e20(_DAT_00573338);
        func_0x00538fa0(_DAT_00573338 + 0x100);
        iVar8 = 0;
        iVar10 = 0;
        do {
          iVar13 = iVar10 + 0x100;
          iVar10 = iVar10 + 1;
          iVar8 = (iVar8 + (uint)(byte)_DAT_00573338[iVar13]) * 2;
        } while (iVar10 < 0x10);
        psVar9 = (short *)(_DAT_00573338 + 0x280);
        iVar10 = 7;
        do {
          sVar6 = *psVar9;
          psVar9 = psVar9 + 1;
          iVar8 = (iVar8 + sVar6) * 2;
          iVar10 = iVar10 + -1;
        } while (iVar10 != 0);
        iVar10 = ((uint)(byte)_DAT_00573338[0x153] +
                 ((uint)(byte)_DAT_00573338[0x152] +
                 ((uint)(byte)_DAT_00573338[0x151] + ((uint)(byte)_DAT_00573338[0x150] + iVar8) * 2)
                 * 2) * 2) * 2;
        iVar8 = 0;
        do {
          iVar13 = iVar8;
          pcRam00566bc0[iVar13 + 0xc] = ((byte)iVar10 & 0x1f) + 0x40;
          if (pcRam00566bc0[iVar13 + 0xc] == '\\') {
            pcRam00566bc0[iVar13 + 0xc] = '7';
          }
          iVar10 = iVar10 >> 5;
          iVar8 = iVar13 + 1;
        } while (iVar13 + 1 < 7);
        if (pcRam00566bc0[iVar13 + 0xd] == '\\') {
          pcRam00566bc0[iVar13 + 0xd] = '7';
        }
        pcRam00566bc0[iVar13 + 0xd] = '\0';
        cVar3 = *pcRam00566bc0;
        iVar8 = 0;
        pcVar12 = pcRam00566bc0;
        while (cVar3 != '\0') {
          pcVar12 = pcVar12 + 1;
          _DAT_00573338[iVar8 + 0x1fd0] = cVar3;
          iVar8 = iVar8 + 1;
          cVar3 = *pcVar12;
        }
        _DAT_00573338[iVar8 + 0x1fd0] = 0;
      }
      goto LAB_0050a5e0;
    }
    break;
  case 0x3f:
    if ((DAT_00560fef & 2) == 0) {
code_r0x0050a2a8:
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = _DAT_00578df0 + 1;
      goto LAB_0050a5e0;
    }
    bVar4 = pad[3];
    goto code_r0x0050a2c6;
  case 0x40:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        _DAT_00578df0 = 0x39;
        _DAT_00578de8 = _DAT_00578de8 + -1;
      }
      else {
        _DAT_00578df0 = 0x3e;
      }
      goto LAB_0050a5e0;
    }
    bVar4 = pad[3];
    goto code_r0x0050a31a;
  case 0x41:
    if ((DAT_00560fef & 2) == 0) goto code_r0x0050a2a8;
    bVar4 = pad[3];
code_r0x0050a2c6:
    if ((bVar4 & 0x40) != 0) {
      DAT_0056075b = 0;
      DAT_00560fef = DAT_00560fef & 0xfd;
      func_0x0053dba0();
      _DAT_00578df0 = 0x3e;
    }
    goto LAB_0050a5e0;
  case 0x42:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f != '\0') {
        _DAT_00578df0 = 0x3e;
        goto LAB_0050a5e0;
      }
      goto code_r0x0050a366;
    }
    bVar4 = pad[3];
code_r0x0050a31a:
    if ((bVar4 & 0x40) != 0) {
      DAT_00560763 = 0;
      DAT_00560fef = DAT_00560fef & 0xfb;
      func_0x0053dba0();
      _DAT_00578df0 = 0x3e;
    }
  default:
    goto LAB_0050a5e0;
  case 0x5d:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        iVar8 = 0;
        do {
          iVar10 = iVar8;
          pcRam00566bc0[iVar10 + 0xc] = '0';
          iVar8 = iVar10 + 1;
        } while (iVar10 + 1 < 7);
        pcRam00566bc0[iVar10 + 0xd] = '\0';
        cVar3 = *pcRam00566bc0;
        iVar8 = 0;
        pcVar12 = pcRam00566bc0;
        while (cVar3 != '\0') {
          pcVar12 = pcVar12 + 1;
          _DAT_00573338[iVar8 + 0x1fd0] = cVar3;
          iVar8 = iVar8 + 1;
          cVar3 = *pcVar12;
        }
        _DAT_00573338[iVar8 + 0x1fd0] = 0;
        uRam005de51c = 1;
        _DAT_00578de8 = -1;
        uRam005de510 = 1;
      }
      else {
        _DAT_00578df0 = 0x3e;
        switch(_DAT_00573338[0x150] & 7) {
        case 0:
        case 6:
          *(undefined2 *)(_DAT_00573338 + 0x148) = 0x10;
          *(undefined2 *)(_DAT_00573338 + 0x14a) = 2;
          break;
        case 1:
        case 3:
        case 7:
          *(undefined2 *)(_DAT_00573338 + 0x148) = 0xe;
          *(undefined2 *)(_DAT_00573338 + 0x14a) = 8;
          break;
        case 2:
        case 4:
        case 5:
          *(undefined2 *)(_DAT_00573338 + 0x148) = 0xf;
          *(undefined2 *)(_DAT_00573338 + 0x14a) = 4;
        }
      }
      goto LAB_0050a5e0;
    }
    if ((pad[3] & 0x40) == 0) goto LAB_0050a5e0;
    DAT_00560763 = 0;
    DAT_00560fef = DAT_00560fef & 0xfb;
    goto code_r0x0050a5d1;
  case 0x7f:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = 0x5d;
      goto LAB_0050a5e0;
    }
    if ((pad[3] & 0x40) == 0) goto LAB_0050a5e0;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
code_r0x0050a5d1:
    func_0x0053dba0();
    _DAT_00578df0 = 0x81;
    goto LAB_0050a5e0;
  case 0x80:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)(pad + 2) = *(ushort *)(pad + 2) & 0x7fff;
      _DAT_00578df0 = _DAT_00578df0 + -1;
      goto LAB_0050a5e0;
    }
    if ((pad[3] & 0x40) == 0) goto LAB_0050a5e0;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
    break;
  case 0x81:
    if ((DAT_00560fef & 4) != 0) {
      if ((pad[3] & 0x40) != 0) {
        DAT_00560763 = 0;
        DAT_00560fef = DAT_00560fef & 0xfb;
        func_0x0053dba0();
        _DAT_00578df0 = 0x3e;
        switch(_DAT_00573338[0x150] & 7) {
        case 0:
        case 6:
          *(undefined2 *)(_DAT_00573338 + 0x148) = 0x10;
          *(undefined2 *)(_DAT_00573338 + 0x14a) = 2;
          break;
        case 1:
        case 3:
        case 7:
          *(undefined2 *)(_DAT_00573338 + 0x148) = 0xe;
          *(undefined2 *)(_DAT_00573338 + 0x14a) = 8;
          break;
        case 2:
        case 4:
        case 5:
          *(undefined2 *)(_DAT_00573338 + 0x148) = 0xf;
          *(undefined2 *)(_DAT_00573338 + 0x14a) = 4;
        }
      }
      goto LAB_0050a5e0;
    }
    if (DAT_005e909f != '\0') {
      _DAT_00578df0 = 0x80;
      goto LAB_0050a5e0;
    }
code_r0x0050a366:
    _DAT_00578df0 = 0x6e;
    _DAT_00578de8 = _DAT_00578de8 + 1;
    goto LAB_0050a5e0;
  }
  func_0x0053dba0();
  _DAT_00578df0 = _DAT_00578df0 + 1;
LAB_0050a5e0:
  func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x10c),0,0x562180,0);
  primCursor = (uint *)func_0x005443a0(primCursor,0x562180);
  iVar8 = 0;
  pcVar12 = &DAT_00573538;
  pad = (byte *)0x110;
  uStack_14 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x94) + 0x30);
  uStack_10 = 0x1000000;
  do {
    sStack_c = *(short *)(pcVar12 + -0x1c) + 0xc;
    sStack_a = *(short *)(pcVar12 + -0x1a) + 4;
    sStack_4 = (short)iVar8 + 4;
    func_0x0051be20(otBase,&primCursor);
    primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
    primCursor = (uint *)func_0x0051f9c0(otBase,primCursor,&uStack_14);
    func_0x0051bdb0(otBase,&primCursor);
    *pcVar12 = ((6 < iVar8) - 1U & 0xf8) + 0x10;
    bVar4 = *(byte *)(iVar8 + 0x5e9060);
    if (bVar4 < 100) {
      *pcVar12 = *pcVar12 + '\b';
    }
    if (bVar4 < 10) {
      *pcVar12 = *pcVar12 + '\b';
    }
    pcVar1 = pcVar12 + -0x20;
    func_0x00544350(*(undefined4 *)(pad + *(int *)(_DAT_005e97c4 + 0x70)),0,pcVar1,0);
    primCursor = (uint *)func_0x005443a0(primCursor,pcVar1);
    primCursor = (uint *)func_0x00545340(otBase,primCursor,pcVar1);
    pad = pad + 4;
    iVar8 = iVar8 + 1;
    pcVar12 = pcVar12 + 0x8c8;
  } while ((int)pad < 0x134);
  return primCursor;
}


