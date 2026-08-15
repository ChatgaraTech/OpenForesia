/*
 * Function: FUN_0050a910
 * Entry:    0050a910
 * Signature forcee a quatre parametres cdecl :
 *   uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
 * Voir ForceDecompileSubStates.java pour la justification.
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_0050a910(uint *otBase,uint *otEntry,uint *primCursor,byte *pad)

{
  byte bVar1;
  undefined2 uVar2;
  short sVar3;
  int iVar4;
  uint uVar5;
  uint uVar6;
  uint uVar7;
  uint uVar8;
  uint *puVar9;
  char *pcVar10;
  char cVar11;
  int iVar12;
  int iVar13;
  byte *pbVar14;
  int iVar15;
  char *pcVar16;
  undefined4 unaff_retaddr;
  undefined4 uVar17;
  undefined4 uStack_1c;
  undefined2 uStack_18;
  undefined2 uStack_16;
  undefined4 uStack_14;
  undefined *puStack_10;
  short sStack_c;
  short sStack_a;
  short sStack_4;
  
  iVar4 = _DAT_00578e18;
  if (_DAT_00562a48 != 0) {
    _DAT_00562a48 = 0;
    _DAT_0057351c = 0xa0;
    _DAT_0057351e = 0x18;
    _DAT_00573520 = 0x68;
    _DAT_00573522 = 0x88;
    DAT_0057353b = 0;
    _DAT_00573de4 = 0x180028;
    _DAT_00573de8 = 0x78;
    _DAT_00573dea = 0x88;
    DAT_00573df4 = 3;
    DAT_00573df2 = 0x1a;
    DAT_00573df3 = 6;
    DAT_00573e02 = 7;
    DAT_00573e03 = 1;
    _DAT_00562a50 = 0;
    _DAT_00578df0 = 0xffffffff;
    _DAT_00562b68 = -1;
    _DAT_00578e0c = 0;
    _DAT_00578e10 = 0;
    _DAT_00578e14 = 0;
  }
  if (_DAT_00562b68 < 0) {
    if ((pad[6] & 0x80) != 0) {
      _DAT_00578e18 = _DAT_00578e18 + -1;
    }
    if ((pad[6] & 0x20) != 0) {
      _DAT_00578e18 = _DAT_00578e18 + 1;
    }
    func_0x00544350(0x572b40,0,0x560728,0);
  }
  _DAT_00578e20 = (uint)(_DAT_00604b0a == -0x8000);
  if (_DAT_00604b2e == -0x8000) {
    _DAT_00578e20 = (_DAT_00604b0a == -0x8000) + 2;
  }
  if (_DAT_00578e18 < 0) {
    _DAT_00578e18 = **(char **)(_DAT_00578e20 * 4 + 0x562b30) + -1;
  }
  if (**(char **)(_DAT_00578e20 * 4 + 0x562b30) <= _DAT_00578e18) {
    _DAT_00578e18 = 0;
  }
  if (iVar4 != _DAT_00578e18) {
    func_0x0053db60();
  }
  if (_DAT_00562b68 < 0) {
    _DAT_0057333c = 0;
    _DAT_00578de0 = 0;
    _DAT_00604960 = 0;
    _DAT_00578e0c = 0;
    _DAT_00604988 = _DAT_00604988 & 0xffff;
    _DAT_00562b68 = _DAT_00578e18;
    func_0x0053db80();
  }
  if (_DAT_00578e10 != _DAT_00578e0c) {
    _DAT_00578e10 = _DAT_00578e0c;
    _DAT_00578e14 = 1;
  }
  if (-1 < _DAT_00562b68) {
    if (_DAT_00562a4c == 0) {
      if (_DAT_0057333c != 0) {
        if (_DAT_0057333c == 1) {
          _DAT_00578de0 = func_0x00504820((_DAT_00604980 & 3) + (_DAT_00604980 & 4) * 4,0);
          if (_DAT_00578de0 < -1) {
            _DAT_00578e0c = -4;
          }
        }
        else if (_DAT_0057333c == 2) {
          iVar4 = func_0x00545a58((_DAT_00604980 & 3) + (_DAT_00604980 & 4) * 4);
          while (iVar4 == 0) {
            iVar4 = func_0x00545a58((_DAT_00604980 & 3) + (_DAT_00604980 & 4) * 4);
          }
          _DAT_00578de0 = func_0x00504820((_DAT_00604980 & 3) + (_DAT_00604980 & 4) * 4,0);
        }
      }
      uVar5 = _DAT_00578e0c + 5;
      pbVar14 = pad;
      bVar1 = DAT_00560fef;
      cVar11 = DAT_005e909f;
      if (uVar5 < 0x69) {
code_r0x0050abb5:
                    /* WARNING: Could not find normalized switch variable to match jumptable */
        switch((&UNK_0050bea8)[uVar5]) {
        case 0:
          if (iRam00560fd4 != 0) {
            DAT_00560fef = bVar1 & 0xfd;
            DAT_0056075b = 0;
            goto code_r0x0050ae60;
          }
          _DAT_00604980 =
               CONCAT31(_DAT_00604981,
                        *(undefined1 *)(*(int *)(_DAT_00578e20 * 4 + 0x562af8) + _DAT_00578e18));
          _DAT_00578e0c = _DAT_00578e0c + 1;
          goto LAB_0050b71b;
        case 1:
code_r0x0050ae60:
          _DAT_00604984 = 0;
          func_0x00512900(_DAT_00573338 + 0x1fd0);
          func_0x005128e0(&DAT_00604980);
          iVar4 = func_0x00504b40(0x604940,1);
          func_0x00504b70(iVar4);
          if (iVar4 < 0) {
            func_0x00544350(0x572b40,0,0x560728,0);
            uVar17 = func_0x00504b40(0x604940,0x30200);
            func_0x00504b70(uVar17);
            _DAT_00578e0c = 6;
          }
          else {
            func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1ac),0,0x560728,0);
            func_0x00504690((_DAT_00604980 & 3) + (_DAT_00604980 & 4) * 4);
            _DAT_0057333c = 1;
            _DAT_00578e0c = 0xe;
          }
          goto LAB_0050b71b;
        case 2:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) != 0) {
            _DAT_00578e0c = -5;
            goto LAB_0050b71b;
          }
          if ((bVar1 & 2) != 0) goto LAB_0050b71b;
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1b0);
          break;
        case 3:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) != 0) {
            _DAT_00578e0c = -5;
            goto LAB_0050b71b;
          }
          if ((bVar1 & 2) != 0) goto LAB_0050b71b;
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 400);
          break;
        case 4:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if ((bVar1 & 4) == 0) {
              func_0x005046c0((_DAT_00604980 & 4) << 2,1);
              func_0x00504730();
              if (DAT_005e909f == '\0') {
                uVar17 = 0x572b40;
                break;
              }
              goto code_r0x0050b711;
            }
          }
          else {
            _DAT_00578e0c = -5;
          }
          goto LAB_0050b71b;
        case 5:
          if (_DAT_00578e14 == 0) {
            _DAT_00578e0c = 0;
            _DAT_0057333c = 0;
          }
          else {
            _DAT_00578e14 = 0;
            _DAT_0057333c = 0;
          }
          goto LAB_0050b71b;
        case 6:
          iVar4 = 0;
          if (_DAT_00578e14 == 0) {
            *_DAT_00573338 = 'S';
            _DAT_00573338[1] = 'C';
            _DAT_00573338[2] = '\x11';
            _DAT_00573338[3] = '\x03';
            pcVar16 = _DAT_00573338 + 4;
            pcVar10 = *(char **)(*(int *)(_DAT_005e97c4 + 0x70) + 0x134);
            cVar11 = *pcVar10;
            while (cVar11 != '\0') {
              iVar4 = iVar4 + 1;
              pcVar10 = pcVar10 + 1;
              *pcVar16 = cVar11;
              pcVar16 = pcVar16 + 1;
              cVar11 = *pcVar10;
            }
            pad = (byte *)0x0;
            pcVar10 = _DAT_00573338 + 0x100;
            cVar11 = _DAT_00573338[0x100];
            while ((cVar11 != '\0' && ((int)pad < 0x10))) {
              uVar2 = func_0x0050a810(cVar11);
              iVar4 = iVar4 + 1;
              pad = pad + 1;
              pcVar10 = pcVar10 + 1;
              *(undefined2 *)pcVar16 = uVar2;
              pcVar16 = pcVar16 + 2;
              cVar11 = *pcVar10;
            }
            pcVar10 = *(char **)(*(int *)(_DAT_005e97c4 + 0x70) + 0x138 +
                                ((byte)_DAT_00573338[0x150] & 7) * 4);
            cVar11 = *pcVar10;
            while (cVar11 != '\0') {
              iVar4 = iVar4 + 1;
              pcVar10 = pcVar10 + 1;
              *pcVar16 = cVar11;
              pcVar16 = pcVar16 + 1;
              cVar11 = *pcVar10;
            }
            if (iVar4 < 0x40) {
              for (uVar5 = 0x40U - iVar4 >> 2; uVar5 != 0; uVar5 = uVar5 - 1) {
                pcVar16[0] = '\0';
                pcVar16[1] = '\0';
                pcVar16[2] = '\0';
                pcVar16[3] = '\0';
                pcVar16 = pcVar16 + 4;
              }
              for (uVar5 = 0x40U - iVar4 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
                *pcVar16 = '\0';
                pcVar16 = pcVar16 + 1;
              }
            }
            iVar4 = 0;
            do {
              iVar13 = iVar4 + 1;
              _DAT_00573338[iVar4 + 0x44] = '\0';
              iVar4 = iVar13;
            } while (iVar13 < 0x1c);
            uStack_1c = CONCAT22(uRam005e8f16 >> 6,
                                 ((ushort)(byte)_DAT_00573338[0x150] + (uRam005e8f16 & 0x3f)) * 0x10
                                );
            uStack_18 = 0x10;
            uStack_16 = 1;
            func_0x00545ac4(0);
            func_0x00545abe(&stack0xffffffe0,_DAT_00573338 + 0x60);
            func_0x00545ac4(0);
            func_0x00545abe(&stack0xffffffd4,_DAT_00573338 + 0x80);
            iVar13 = 0;
            pcVar16 = _DAT_00573338 + 0x2000;
            for (iVar4 = 0x800; iVar4 != 0; iVar4 = iVar4 + -1) {
              pcVar16[0] = '\0';
              pcVar16[1] = '\0';
              pcVar16[2] = '\0';
              pcVar16[3] = '\0';
              pcVar16 = pcVar16 + 4;
            }
            iVar4 = 0x200;
            pcVar16 = _DAT_00573338 + 0x4000;
            do {
              if (_DAT_00573338[iVar13 + 0x300] == '\0') {
                pcVar10 = pcVar16 + 0x10;
                pcVar16[0] = '\0';
                pcVar16[1] = '\0';
                pcVar16[2] = '\0';
                pcVar16[3] = '\0';
                pcVar16[4] = '\0';
                pcVar16[5] = '\0';
                pcVar16[6] = '\0';
                pcVar16[7] = '\0';
                pcVar16[8] = '\0';
                pcVar16[9] = '\0';
                pcVar16[10] = '\0';
                pcVar16[0xb] = '\0';
                pcVar16[0xc] = '\0';
                pcVar16[0xd] = '\0';
                pcVar16[0xe] = '\0';
                pcVar16[0xf] = '\0';
              }
              else {
                iVar12 = 4;
                iVar15 = iVar13 + 0x300;
                do {
                  pcVar10 = pcVar16 + 4;
                  iVar12 = iVar12 + -1;
                  *(undefined4 *)pcVar16 = *(undefined4 *)(_DAT_00573338 + iVar15);
                  iVar15 = iVar15 + 4;
                  pcVar16 = pcVar10;
                } while (iVar12 != 0);
                iVar13 = iVar13 + 0x280;
              }
              iVar4 = iVar4 + -1;
              pcVar16 = pcVar10;
            } while (iVar4 != 0);
            iVar4 = 0;
            iVar13 = 0x1ffc;
            pcVar16 = _DAT_00573338;
            do {
              iVar4 = iVar4 + *pcVar16;
              pcVar16 = pcVar16 + 1;
              iVar13 = iVar13 + -1;
            } while (iVar13 != 0);
            *(int *)(_DAT_00573338 + 0x1ffc) = iVar4;
            uVar17 = func_0x00504b40(0x604940,2);
            uVar5 = func_0x00504c20(uVar17,_DAT_00573338,0,0x2000);
            uVar6 = func_0x00504c20(uVar17,_DAT_00573338 + 0x2000,0x2000,0x1f00);
            uVar7 = func_0x00504c20(uVar17,_DAT_00573338 + 0x1f00,0x3f00,0x100);
            uVar8 = func_0x00504c20(uVar17,_DAT_00573338 + 0x4000,0x4000,0x2000);
            func_0x00504b70(uVar17);
            if ((int)(uVar5 | uVar6 | uVar7 | uVar8) < 0) {
              func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x178),0,0x560728,0);
              _DAT_00578e0c = 99;
            }
            else {
              func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1b8),0,0x560728,0);
              _DAT_0057333c = 0;
              _DAT_00578e0c = 0x11;
            }
          }
          else {
            _DAT_00578e14 = 0;
          }
          goto LAB_0050b71b;
        case 7:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) != 0) {
            _DAT_00578e0c = -5;
            goto LAB_0050b71b;
          }
          if ((bVar1 & 2) != 0) goto LAB_0050b71b;
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1bc);
          break;
        case 8:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) != 0) {
            _DAT_00578e0c = -5;
            goto LAB_0050b71b;
          }
          if ((bVar1 & 2) != 0) goto LAB_0050b71b;
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 400);
          break;
        case 9:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if ((bVar1 & 4) == 0) {
              if (cVar11 == '\0') goto code_r0x0050b6d0;
              _DAT_00578e0c = -5;
            }
          }
          else {
            _DAT_00578e0c = -5;
          }
          goto LAB_0050b71b;
        case 10:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if ((*(ushort *)(pbVar14 + 2) & 0x8000) == 0) {
              uVar5 = _DAT_00604984 & 0xff;
              bVar1 = DAT_00604984;
              if ((pbVar14[6] & 0x80) != 0) {
                bVar1 = DAT_00604984 - 1;
                _DAT_00604984 = CONCAT31(DAT_00604984_1,bVar1);
              }
              if ((pbVar14[6] & 0x20) != 0) {
                bVar1 = bVar1 + 1;
                _DAT_00604984 = CONCAT31(DAT_00604984_1,bVar1);
              }
              if (bVar1 == 0xff) {
                bVar1 = DAT_00604981 - 1;
                _DAT_00604984 = CONCAT31(DAT_00604984_1,bVar1);
              }
              if (DAT_00604981 <= bVar1) {
                _DAT_00604984 = _DAT_00604984 & 0xffffff00;
              }
              if (uVar5 != (_DAT_00604984 & 0xff)) {
                func_0x0053db60();
              }
              func_0x00544350(0x5606f8,0,0x560728,0);
              primCursor = (uint *)func_0x005443a0(primCursor,0x560728);
              func_0x00544350((_DAT_00604984 & 0xff) * 0x100 + 0x1c0004 + *(int *)MainMem_exref,0,
                              0x560728,0);
            }
            else {
              func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1c0),0,0x560728,0);
              *(ushort *)(pbVar14 + 2) = *(ushort *)(pbVar14 + 2) & 0x7fff;
              _DAT_00578e0c = _DAT_00578e0c + 1;
              func_0x0053db80();
            }
          }
          else {
            _DAT_00578e0c = -5;
          }
          goto LAB_0050b71b;
        case 0xb:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) != 0) {
            DAT_00560fef = bVar1 & 0xfd;
            _DAT_00578e0c = _DAT_00578e0c + -1;
            DAT_0056075b = 0;
            goto LAB_0050b71b;
          }
          if ((bVar1 & 2) != 0) goto LAB_0050b71b;
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 400);
          break;
        case 0xc:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) != 0) {
            DAT_00560fef = bVar1 & 0xfb;
            DAT_00560763 = 0;
            _DAT_00578e0c = 10;
            goto LAB_0050b71b;
          }
          if ((bVar1 & 4) != 0) goto LAB_0050b71b;
          if (cVar11 != '\0') {
            _DAT_00578e0c = 10;
            goto code_r0x0050ad53;
          }
          uVar17 = 0x572b40;
          break;
        case 0xd:
          if (_DAT_00578e14 != 0) {
            _DAT_00578e14 = 0;
            _DAT_0057333c = 0;
            goto LAB_0050b71b;
          }
          func_0x005046c0((_DAT_00604980 & 4) << 2,1);
          func_0x00504730();
          func_0x00512900(_DAT_006049bc + (_DAT_00604984 & 0xff) * 0x28);
          func_0x005128e0(&DAT_00604980);
          iVar13 = 10;
          iVar4 = 0;
          do {
            if (iVar4 != 0) goto code_r0x0050ac64;
            iVar4 = func_0x00545a9a(0x604940);
            iVar13 = iVar13 + -1;
          } while (iVar13 != 0);
          if (iVar4 == 0) {
code_r0x0050b681:
            func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1c4),0,0x560728,0);
            _DAT_00578e0c = -1;
            goto LAB_0050b71b;
          }
code_r0x0050ac64:
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x198);
code_r0x0050ad2f:
          func_0x00544350(uVar17,0,0x560728,0);
          _DAT_0057333c = 0;
          _DAT_00578e0c = 0;
          pbVar14 = pad;
          bVar1 = DAT_00560fef;
          cVar11 = DAT_005e909f;
code_r0x0050ad53:
          uVar5 = _DAT_00578e0c + 5;
          if (0x68 < uVar5) goto LAB_0050b71b;
          goto code_r0x0050abb5;
        case 0xe:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if ((bVar1 & 2) == 0) {
              func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1c8),0,0x560728,0);
              _DAT_00578e0c = _DAT_00578e0c + 1;
            }
          }
          else {
            _DAT_00578e0c = -5;
          }
          goto LAB_0050b71b;
        case 0xf:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if ((bVar1 & 2) == 0) {
              func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 400),0,0x560728,0);
              _DAT_00578e0c = _DAT_00578e0c + 1;
            }
          }
          else {
            _DAT_00578e0c = -5;
          }
          goto LAB_0050b71b;
        case 0x10:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if ((bVar1 & 4) != 0) goto LAB_0050b71b;
            if (cVar11 == '\0') {
              func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1cc),0,0x560728,0);
              func_0x005046c0((_DAT_00604980 & 4) << 2,1);
              func_0x00504730();
              func_0x00512900(_DAT_00573338 + 0x1fd0);
              func_0x005128e0(&DAT_00604980);
              iVar13 = 10;
              iVar4 = 0;
              do {
                if (iVar4 != 0) goto code_r0x0050ad18;
                iVar4 = func_0x00545a9a(0x604940);
                iVar13 = iVar13 + -1;
              } while (iVar13 != 0);
              if (iVar4 == 0) goto code_r0x0050b681;
code_r0x0050ad18:
              uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x198);
              goto code_r0x0050ad2f;
            }
          }
          _DAT_00578e0c = -5;
          goto LAB_0050b71b;
        case 0x11:
          if ((bVar1 & 2) != 0) goto LAB_0050b71b;
          uVar17 = *(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x1d8);
          break;
        case 0x12:
          if ((bVar1 & 4) == 0) {
            if (cVar11 == '\0') {
              _DAT_00578de8 = 0;
              _DAT_00578df0 = 0;
            }
            else {
              uRam005de51c = 0;
              _DAT_00578de8 = 0xffffffff;
              uRam005de510 = 1;
            }
          }
        default:
          goto LAB_0050b71b;
        case 99:
joined_r0x0050b70f:
          if ((bVar1 & 2) == 0) {
code_r0x0050b711:
            _DAT_00562b68 = -1;
          }
          goto LAB_0050b71b;
        case 0xfffffffb:
          func_0x005046c0((_DAT_00604980 & 4) << 2,1);
          func_0x00504730();
          bVar1 = DAT_00560fef;
        case 0xfffffffc:
          DAT_00560fef = bVar1 & 0xf9;
          DAT_00560763 = 0;
          DAT_0056075b = 0;
          _DAT_00562b68 = -1;
          func_0x0053dba0();
          goto LAB_0050b71b;
        case 0xfffffffd:
        case 0xfffffffe:
          if ((*(ushort *)(pbVar14 + 2) & 0x4000) == 0) {
            if (_DAT_00578de0 == -3) {
              _DAT_0057333c = 0;
              _DAT_00578e0c = 0;
            }
            else if ((bVar1 & 2) == 0) {
              func_0x005046c0((_DAT_00604980 & 4) << 2,1);
              func_0x00504730();
              goto code_r0x0050b711;
            }
          }
          else {
            _DAT_00578e0c = -5;
          }
          goto LAB_0050b71b;
        case 0xffffffff:
          goto joined_r0x0050b70f;
        }
        func_0x00544350(uVar17,0,0x560728,0);
code_r0x0050b6d0:
        _DAT_00578e0c = _DAT_00578e0c + 1;
      }
    }
LAB_0050b71b:
    if (((-1 < _DAT_00562b68) && (9 < _DAT_00578e0c)) && (_DAT_00578e0c < 0xe)) {
      puStack_10 = (undefined *)0x1000000;
      uStack_14 = *(undefined **)(*(int *)(_DAT_005e97c4 + 0x94) + 0x38);
      sStack_4 = 0;
      sStack_c = 0xb4;
      sStack_a = 0x30;
      primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
      sStack_4 = 1;
      sStack_c = 0xe8;
      primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
      primCursor = (uint *)func_0x0051f9c0(otBase,primCursor,&uStack_14);
      uStack_14 = (undefined *)0x566c30;
      sStack_c = 0xbc;
      sStack_a = 0x38;
      primCursor = (uint *)func_0x0051be90(otBase,primCursor,&uStack_14,
                                           *(undefined4 *)
                                            (_DAT_006049bc + 0x18 + (_DAT_00604984 & 0xff) * 0x28),
                                           0x82,0);
      sStack_c = 0xec;
      primCursor = (uint *)func_0x0051be90(otBase,primCursor,&uStack_14,_DAT_00604980 >> 0x10 & 0xff
                                           ,0x82,0);
      pad = (byte *)(_DAT_00604980 >> 8 & 0xff);
      sStack_4 = 0;
      _DAT_005e8ff8 = 0xf;
      _DAT_005e8ffa = 0x7f80;
      uStack_14 = &DAT_00563148;
      uVar5 = _DAT_00604988 - (int)(0x1000 / (ulonglong)(longlong)(int)pad) * (_DAT_00604984 & 0xff)
              & 0xfff;
      if (uVar5 < 2000) {
        sVar3 = (short)((int)uVar5 >> 2);
      }
      else {
        sVar3 = -(short)((int)(0x1000 - uVar5) >> 2);
      }
      _DAT_00604988 = _DAT_00604988 - sVar3;
      _DAT_00604988 = CONCAT22(_DAT_0060498a + (short)(0x1800 - _DAT_0060498a >> 2),_DAT_00604988);
      for (; pad != (byte *)0x0; pad = pad + -1) {
        uVar5 = _DAT_00604980 >> 8 & 0xff;
        iVar13 = (int)(pad + (_DAT_00604984 & 0xff)) % (int)uVar5;
        iVar15 = (int)(0x1000 / (ulonglong)(longlong)(int)uVar5) * iVar13 - (int)_DAT_00604988;
        iVar4 = func_0x00545a1c(iVar15);
        puStack_10 = (undefined *)
                     CONCAT22(puStack_10._2_2_,(short)(iVar4 * _DAT_0060498a >> 0x14) + 0xd3);
        iVar4 = func_0x00545a16(iVar15);
        sVar3 = -(short)(iVar4 * _DAT_0060498a >> 0x14);
        DAT_00563148 = (char)iVar13 << 4;
        uStack_14 = (undefined *)CONCAT22(sVar3 + 100,(short)uStack_14);
        DAT_00563149 = ((char)((_DAT_005e9960 >> 2) % (uint)*(byte *)(iVar13 + 0x60498c)) + '\x02')
                       * '\x10';
        if (otEntry == (uint *)(_DAT_00604980 >> 8 & 0xff)) {
          uStack_14 = (undefined *)CONCAT22(sVar3 + 0x6c,(short)uStack_14 + 8);
          iVar4 = func_0x00545a1c((_DAT_005e9960 & 7) << 9);
          sVar3 = (short)(iVar4 >> 2) + 0x1400;
          uStack_14 = (undefined *)CONCAT22(sVar3,sVar3);
          uStack_1c = _DAT_005e9814;
          primCursor = (uint *)func_0x00521f20(otBase,unaff_retaddr,&stack0xffffffe0,0,iVar13);
        }
        else {
          primCursor = (uint *)func_0x00521df0(otBase,otBase,&uStack_1c,3,iVar13);
        }
      }
      primCursor = (uint *)func_0x0051f9c0(otBase,primCursor,&uStack_14);
    }
  }
  func_0x0051be20(otBase,&primCursor);
  uStack_14 = *(undefined **)(*(int *)(_DAT_005e97c4 + 0x94) + 0x34);
  if (((_DAT_00562b68 < 0) || (_DAT_00578e0c < 10)) ||
     (puStack_10 = &UNK_00404040, 0xd < _DAT_00578e0c)) {
    puStack_10 = (undefined *)0x1000000;
  }
  if (_DAT_00604b0a == -0x8000) {
    sStack_4 = 1;
    sStack_c = 0xa9;
    sStack_a = 0x6e;
    primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
  }
  if (_DAT_00604b2e == -0x8000) {
    sStack_4 = 2;
    sStack_c = 0xdd;
    sStack_a = 0x6e;
    primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
  }
  sStack_4 = 0;
  sStack_c = 0xbd;
  sStack_a = 0x48;
  func_0x0051bdb0(otBase,&primCursor);
  primCursor = (uint *)func_0x00545340(otBase,primCursor,0x573518);
  func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x158),0,0x573de0,0);
  primCursor = (uint *)func_0x005443a0(primCursor,0x573de0);
  func_0x00544350(_DAT_00573338 + 0x100,0,0x573de0,0);
  primCursor = (uint *)func_0x005443a0(primCursor,0x573de0);
  func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x15c),0,0x573de0,0);
  primCursor = (uint *)func_0x005443a0(primCursor,0x573de0);
  func_0x00544350(*(undefined4 *)
                   (*(int *)(_DAT_005e97c4 + 0x70) + 0xc + (uint)(byte)_DAT_00573338[0x150] * 4),0,
                  0x573de0,0);
  primCursor = (uint *)func_0x005443a0(primCursor,0x573de0);
  DAT_005e909a = 1;
  DAT_005e909b = 0xf;
  DAT_005e909d = 0x10;
  DAT_005e909c = (char)((ulonglong)(_DAT_005e9960 >> 2 & 0x1fffffff) % 3) +
                 _DAT_00573338[0x151] * '\x03';
  DAT_005e909e = _DAT_00573338[0x150];
  func_0x00544350(*(undefined4 *)(*(int *)(_DAT_005e97c4 + 0x70) + 0x160),0,0x573de0,0);
  primCursor = (uint *)func_0x005443a0(primCursor,0x573de0);
  sStack_a = _DAT_00573de6 + 0x52;
  puStack_10 = (undefined *)0x1000000;
  uVar5 = 0;
  do {
    func_0x0051be20(otBase,&primCursor);
    sStack_4 = (short)uVar5 + 0xb;
    uStack_14 = *(undefined **)(*(int *)(_DAT_005e97c4 + 0x94) + 0x30);
    sStack_c = (-(ushort)((uVar5 & 1) != 0) & 0x38) + 0x12 + (short)_DAT_00573de4;
    primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
    primCursor = (uint *)func_0x0051f9c0(otBase,primCursor,&uStack_14);
    func_0x0051bdb0(otBase,&primCursor);
    sStack_c = sStack_c + 0x1c;
    uStack_14 = (undefined *)0x566c30;
    primCursor = (uint *)func_0x0051be90(otBase,primCursor,&uStack_14,
                                         *(undefined1 *)(uVar5 + 0x5e9067),0x82,0);
    uVar5 = uVar5 + 1;
    sStack_4 = sStack_4 + 1;
  } while ((int)uVar5 < 2);
  sStack_a = _DAT_00573de6 + 0x60;
  uVar5 = 0;
  do {
    func_0x0051be20(otBase,&primCursor);
    sStack_4 = (short)uVar5 + 4;
    uStack_14 = *(undefined **)(*(int *)(_DAT_005e97c4 + 0x94) + 0x30);
    sStack_c = (-(ushort)((uVar5 & 1) != 0) & 0x38) + 0x12 + (short)_DAT_00573de4;
    primCursor = (uint *)func_0x00521df0(otBase,primCursor,&uStack_14,0,0);
    primCursor = (uint *)func_0x0051f9c0(otBase,primCursor,&uStack_14);
    func_0x0051bdb0(otBase,&primCursor);
    sStack_c = sStack_c + 0x1c;
    uStack_14 = (undefined *)0x566c30;
    primCursor = (uint *)func_0x0051be90(otBase,primCursor,&uStack_14,
                                         *(undefined1 *)(uVar5 + 0x5e9060),0x82,0);
    sStack_a = sStack_a + (-(ushort)((uVar5 & 1) != 0) & 9);
    uVar5 = uVar5 + 1;
    sStack_4 = sStack_4 + 1;
  } while ((int)uVar5 < 7);
  puVar9 = (uint *)func_0x00545340(otBase,primCursor,0x573de0);
  return puVar9;
}


