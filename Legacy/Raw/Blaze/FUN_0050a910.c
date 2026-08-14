/*
 * Function: FUN_0050a910
 * Entry:    0050a910
 * Body:     [[0050a910, 0050be40]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_0050a910(undefined4 param_1,undefined4 param_2,undefined4 param_3,uint param_4)

{
  byte bVar1;
  undefined2 uVar2;
  int iVar3;
  uint uVar4;
  undefined4 uVar5;
  uint uVar6;
  uint uVar7;
  uint uVar8;
  char *pcVar9;
  char cVar10;
  short sVar11;
  int iVar12;
  int iVar13;
  int iVar14;
  char *pcVar15;
  undefined *puVar16;
  undefined *local_14;
  undefined1 *local_10;
  short local_c;
  short local_a;
  short sStack_8;
  short sStack_6;
  short local_4;
  
  iVar3 = DAT_00578e18;
  if (DAT_00562a48 != 0) {
    DAT_00562a48 = 0;
    DAT_0057351c._0_2_ = 0xa0;
    DAT_0057351c._2_2_ = 0x18;
    DAT_00573520._0_2_ = 0x68;
    DAT_00573520._2_2_ = 0x88;
    DAT_00573538._3_1_ = 0;
    DAT_00573de4 = 0x180028;
    DAT_00573de8._0_2_ = 0x78;
    DAT_00573de8._2_2_ = 0x88;
    DAT_00573df4._0_1_ = 3;
    DAT_00573df0._2_1_ = 0x1a;
    DAT_00573df0._3_1_ = 6;
    DAT_00573e00._2_1_ = 7;
    DAT_00573e00._3_1_ = 1;
    DAT_00562a50 = 0;
    DAT_00578df0 = 0xffffffff;
    DAT_00562b68 = -1;
    DAT_00578e0c = 0;
    DAT_00578e10 = 0;
    DAT_00578e14 = 0;
  }
  if (DAT_00562b68 < 0) {
    if ((*(byte *)(param_4 + 6) & 0x80) != 0) {
      DAT_00578e18 = DAT_00578e18 + -1;
    }
    if ((*(byte *)(param_4 + 6) & 0x20) != 0) {
      DAT_00578e18 = DAT_00578e18 + 1;
    }
    FUN_00544350(&DAT_00572b40,0,&DAT_00560728,0);
  }
  DAT_00578e20 = (uint)(DAT_00604b0a == -0x8000);
  if (DAT_00604b2e == -0x8000) {
    DAT_00578e20 = (DAT_00604b0a == -0x8000) + 2;
  }
  if (DAT_00578e18 < 0) {
    DAT_00578e18 = **(char **)(DAT_00578e20 * 4 + 0x562b30) + -1;
  }
  if (**(char **)(DAT_00578e20 * 4 + 0x562b30) <= DAT_00578e18) {
    DAT_00578e18 = 0;
  }
  if (iVar3 != DAT_00578e18) {
    FUN_0053db60();
  }
  if (DAT_00562b68 < 0) {
    DAT_0057333c = 0;
    DAT_00578de0 = 0;
    _DAT_00604960 = 0;
    DAT_00578e0c = 0;
    _DAT_00604988 = _DAT_00604988 & 0xffff;
    DAT_00562b68 = DAT_00578e18;
    FUN_0053db80();
  }
  if (DAT_00578e10 != DAT_00578e0c) {
    DAT_00578e10 = DAT_00578e0c;
    DAT_00578e14 = 1;
  }
  if (-1 < DAT_00562b68) {
    if (DAT_00562a4c == 0) {
      if (DAT_0057333c != 0) {
        if (DAT_0057333c == 1) {
          DAT_00578de0 = FUN_00504820((DAT_00604980 & 3) + (DAT_00604980 & 4) * 4,0);
          if (DAT_00578de0 < -1) {
            DAT_00578e0c = -4;
          }
        }
        else if (DAT_0057333c == 2) {
          iVar3 = Ordinal_67((DAT_00604980 & 3) + (DAT_00604980 & 4) * 4);
          while (iVar3 == 0) {
            iVar3 = Ordinal_67((DAT_00604980 & 3) + (DAT_00604980 & 4) * 4);
          }
          DAT_00578de0 = FUN_00504820((DAT_00604980 & 3) + (DAT_00604980 & 4) * 4,0);
        }
      }
      uVar4 = DAT_00578e0c + 5;
      uVar8 = param_4;
      bVar1 = DAT_00560fef;
      cVar10 = DAT_005e909f;
      if (uVar4 < 0x69) {
LAB_0050abb5:
                    /* WARNING: Could not find normalized switch variable to match jumptable */
        switch((&switchD_0050abbd::switchdataD_0050bea8)[uVar4]) {
        case 0:
          if (DAT_00560fd4 != 0) {
            DAT_00560fef = bVar1 & 0xfd;
            DAT_0056075b = 0;
            goto switchD_0050abbd_caseD_1;
          }
          DAT_00604980 = CONCAT31(DAT_00604980._1_3_,
                                  *(undefined1 *)
                                   (*(int *)(DAT_00578e20 * 4 + 0x562af8) + DAT_00578e18));
          DAT_00578e0c = DAT_00578e0c + 1;
          goto switchD_0050abbd_caseD_13;
        case 1:
switchD_0050abbd_caseD_1:
          DAT_00604984 = 0;
          FUN_00512900(DAT_00573338 + 0x1fd0);
          FUN_005128e0(&DAT_00604980);
          iVar3 = FUN_00504b40(&DAT_00604940,1);
          FUN_00504b70(iVar3);
          if (iVar3 < 0) {
            FUN_00544350(&DAT_00572b40,0,&DAT_00560728,0);
            uVar5 = FUN_00504b40(&DAT_00604940,0x30200);
            FUN_00504b70(uVar5);
            DAT_00578e0c = 6;
          }
          else {
            FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1ac),0,&DAT_00560728,0);
            FUN_00504690((DAT_00604980 & 3) + (DAT_00604980 & 4) * 4);
            DAT_0057333c = 1;
            DAT_00578e0c = 0xe;
          }
          goto switchD_0050abbd_caseD_13;
        case 2:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) != 0) {
            DAT_00578e0c = -5;
            goto switchD_0050abbd_caseD_13;
          }
          if ((bVar1 & 2) != 0) goto switchD_0050abbd_caseD_13;
          puVar16 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x70) + 0x1b0);
          break;
        case 3:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) != 0) {
            DAT_00578e0c = -5;
            goto switchD_0050abbd_caseD_13;
          }
          if ((bVar1 & 2) != 0) goto switchD_0050abbd_caseD_13;
          puVar16 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x70) + 400);
          break;
        case 4:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if ((bVar1 & 4) == 0) {
              FUN_005046c0((DAT_00604980 & 4) << 2,1);
              FUN_00504730();
              if (DAT_005e909f == '\0') {
                puVar16 = &DAT_00572b40;
                break;
              }
              goto LAB_0050b711;
            }
          }
          else {
            DAT_00578e0c = -5;
          }
          goto switchD_0050abbd_caseD_13;
        case 5:
          if (DAT_00578e14 == 0) {
            DAT_00578e0c = 0;
            DAT_0057333c = 0;
          }
          else {
            DAT_00578e14 = 0;
            DAT_0057333c = 0;
          }
          goto switchD_0050abbd_caseD_13;
        case 6:
          iVar3 = 0;
          if (DAT_00578e14 == 0) {
            *DAT_00573338 = 'S';
            DAT_00573338[1] = 'C';
            DAT_00573338[2] = '\x11';
            DAT_00573338[3] = '\x03';
            pcVar15 = DAT_00573338 + 4;
            pcVar9 = *(char **)(*(int *)(DAT_005e97c4 + 0x70) + 0x134);
            cVar10 = *pcVar9;
            while (cVar10 != '\0') {
              iVar3 = iVar3 + 1;
              pcVar9 = pcVar9 + 1;
              *pcVar15 = cVar10;
              pcVar15 = pcVar15 + 1;
              cVar10 = *pcVar9;
            }
            param_4 = 0;
            pcVar9 = DAT_00573338 + 0x100;
            cVar10 = DAT_00573338[0x100];
            while ((cVar10 != '\0' && ((int)param_4 < 0x10))) {
              uVar2 = FUN_0050a810(cVar10);
              iVar3 = iVar3 + 1;
              param_4 = param_4 + 1;
              pcVar9 = pcVar9 + 1;
              *(undefined2 *)pcVar15 = uVar2;
              pcVar15 = pcVar15 + 2;
              cVar10 = *pcVar9;
            }
            pcVar9 = *(char **)(*(int *)(DAT_005e97c4 + 0x70) + 0x138 +
                               ((byte)DAT_00573338[0x150] & 7) * 4);
            cVar10 = *pcVar9;
            while (cVar10 != '\0') {
              iVar3 = iVar3 + 1;
              pcVar9 = pcVar9 + 1;
              *pcVar15 = cVar10;
              pcVar15 = pcVar15 + 1;
              cVar10 = *pcVar9;
            }
            if (iVar3 < 0x40) {
              for (uVar8 = 0x40U - iVar3 >> 2; uVar8 != 0; uVar8 = uVar8 - 1) {
                pcVar15[0] = '\0';
                pcVar15[1] = '\0';
                pcVar15[2] = '\0';
                pcVar15[3] = '\0';
                pcVar15 = pcVar15 + 4;
              }
              for (uVar8 = 0x40U - iVar3 & 3; uVar8 != 0; uVar8 = uVar8 - 1) {
                *pcVar15 = '\0';
                pcVar15 = pcVar15 + 1;
              }
            }
            iVar3 = 0;
            do {
              iVar13 = iVar3 + 1;
              DAT_00573338[iVar3 + 0x44] = '\0';
              iVar3 = iVar13;
            } while (iVar13 < 0x1c);
            Ordinal_667(0);
            Ordinal_684(&stack0xffffffe0,DAT_00573338 + 0x60);
            Ordinal_667(0);
            Ordinal_684(&stack0xffffffd4,DAT_00573338 + 0x80);
            iVar13 = 0;
            pcVar15 = DAT_00573338 + 0x2000;
            for (iVar3 = 0x800; iVar3 != 0; iVar3 = iVar3 + -1) {
              pcVar15[0] = '\0';
              pcVar15[1] = '\0';
              pcVar15[2] = '\0';
              pcVar15[3] = '\0';
              pcVar15 = pcVar15 + 4;
            }
            iVar3 = 0x200;
            pcVar15 = DAT_00573338 + 0x4000;
            do {
              if (DAT_00573338[iVar13 + 0x300] == '\0') {
                pcVar9 = pcVar15 + 0x10;
                pcVar15[0] = '\0';
                pcVar15[1] = '\0';
                pcVar15[2] = '\0';
                pcVar15[3] = '\0';
                pcVar15[4] = '\0';
                pcVar15[5] = '\0';
                pcVar15[6] = '\0';
                pcVar15[7] = '\0';
                pcVar15[8] = '\0';
                pcVar15[9] = '\0';
                pcVar15[10] = '\0';
                pcVar15[0xb] = '\0';
                pcVar15[0xc] = '\0';
                pcVar15[0xd] = '\0';
                pcVar15[0xe] = '\0';
                pcVar15[0xf] = '\0';
              }
              else {
                iVar12 = 4;
                iVar14 = iVar13 + 0x300;
                do {
                  pcVar9 = pcVar15 + 4;
                  iVar12 = iVar12 + -1;
                  *(undefined4 *)pcVar15 = *(undefined4 *)(DAT_00573338 + iVar14);
                  iVar14 = iVar14 + 4;
                  pcVar15 = pcVar9;
                } while (iVar12 != 0);
                iVar13 = iVar13 + 0x280;
              }
              iVar3 = iVar3 + -1;
              pcVar15 = pcVar9;
            } while (iVar3 != 0);
            iVar3 = 0;
            iVar13 = 0x1ffc;
            pcVar15 = DAT_00573338;
            do {
              iVar3 = iVar3 + *pcVar15;
              pcVar15 = pcVar15 + 1;
              iVar13 = iVar13 + -1;
            } while (iVar13 != 0);
            *(int *)(DAT_00573338 + 0x1ffc) = iVar3;
            uVar5 = FUN_00504b40(&DAT_00604940,2);
            uVar8 = FUN_00504c20(uVar5,DAT_00573338,0,0x2000);
            uVar4 = FUN_00504c20(uVar5,DAT_00573338 + 0x2000,0x2000,0x1f00);
            uVar6 = FUN_00504c20(uVar5,DAT_00573338 + 0x1f00,0x3f00,0x100);
            uVar7 = FUN_00504c20(uVar5,DAT_00573338 + 0x4000,0x4000,0x2000);
            FUN_00504b70(uVar5);
            if ((int)(uVar8 | uVar4 | uVar6 | uVar7) < 0) {
              FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x178),0,&DAT_00560728,0)
              ;
              DAT_00578e0c = 99;
            }
            else {
              FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1b8),0,&DAT_00560728,0)
              ;
              DAT_0057333c = 0;
              DAT_00578e0c = 0x11;
            }
          }
          else {
            DAT_00578e14 = 0;
          }
          goto switchD_0050abbd_caseD_13;
        case 7:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) != 0) {
            DAT_00578e0c = -5;
            goto switchD_0050abbd_caseD_13;
          }
          if ((bVar1 & 2) != 0) goto switchD_0050abbd_caseD_13;
          puVar16 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x70) + 0x1bc);
          break;
        case 8:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) != 0) {
            DAT_00578e0c = -5;
            goto switchD_0050abbd_caseD_13;
          }
          if ((bVar1 & 2) != 0) goto switchD_0050abbd_caseD_13;
          puVar16 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x70) + 400);
          break;
        case 9:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if ((bVar1 & 4) == 0) {
              if (cVar10 == '\0') goto LAB_0050b6d0;
              DAT_00578e0c = -5;
            }
          }
          else {
            DAT_00578e0c = -5;
          }
          goto switchD_0050abbd_caseD_13;
        case 10:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if ((*(ushort *)(uVar8 + 2) & 0x8000) == 0) {
              uVar4 = DAT_00604984 & 0xff;
              bVar1 = (byte)DAT_00604984;
              if ((*(byte *)(uVar8 + 6) & 0x80) != 0) {
                bVar1 = (byte)DAT_00604984 - 1;
                DAT_00604984 = CONCAT31(DAT_00604984._1_3_,bVar1);
              }
              if ((*(byte *)(uVar8 + 6) & 0x20) != 0) {
                bVar1 = bVar1 + 1;
                DAT_00604984 = CONCAT31(DAT_00604984._1_3_,bVar1);
              }
              if (bVar1 == 0xff) {
                bVar1 = DAT_00604980._1_1_ - 1;
                DAT_00604984 = CONCAT31(DAT_00604984._1_3_,bVar1);
              }
              if (DAT_00604980._1_1_ <= bVar1) {
                DAT_00604984 = DAT_00604984 & 0xffffff00;
              }
              if (uVar4 != (DAT_00604984 & 0xff)) {
                FUN_0053db60();
              }
              FUN_00544350(&DAT_005606f8,0,&DAT_00560728,0);
              param_3 = FUN_005443a0(param_3,&DAT_00560728);
              FUN_00544350((DAT_00604984 & 0xff) * 0x100 + 0x1c0004 + *(int *)MainMem_exref,0,
                           &DAT_00560728,0);
            }
            else {
              FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1c0),0,&DAT_00560728,0)
              ;
              *(ushort *)(uVar8 + 2) = *(ushort *)(uVar8 + 2) & 0x7fff;
              DAT_00578e0c = DAT_00578e0c + 1;
              FUN_0053db80();
            }
          }
          else {
            DAT_00578e0c = -5;
          }
          goto switchD_0050abbd_caseD_13;
        case 0xb:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) != 0) {
            DAT_00560fef = bVar1 & 0xfd;
            DAT_00578e0c = DAT_00578e0c + -1;
            DAT_0056075b = 0;
            goto switchD_0050abbd_caseD_13;
          }
          if ((bVar1 & 2) != 0) goto switchD_0050abbd_caseD_13;
          puVar16 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x70) + 400);
          break;
        case 0xc:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) != 0) {
            DAT_00560fef = bVar1 & 0xfb;
            DAT_00560763 = 0;
            DAT_00578e0c = 10;
            goto switchD_0050abbd_caseD_13;
          }
          if ((bVar1 & 4) != 0) goto switchD_0050abbd_caseD_13;
          if (cVar10 != '\0') {
            DAT_00578e0c = 10;
            goto LAB_0050ad53;
          }
          puVar16 = &DAT_00572b40;
          break;
        case 0xd:
          if (DAT_00578e14 != 0) {
            DAT_00578e14 = 0;
            DAT_0057333c = 0;
            goto switchD_0050abbd_caseD_13;
          }
          FUN_005046c0((DAT_00604980 & 4) << 2,1);
          FUN_00504730();
          FUN_00512900(DAT_006049bc + (DAT_00604984 & 0xff) * 0x28);
          FUN_005128e0(&DAT_00604980);
          iVar13 = 10;
          iVar3 = 0;
          do {
            if (iVar3 != 0) goto LAB_0050ac64;
            iVar3 = Ordinal_20(&DAT_00604940);
            iVar13 = iVar13 + -1;
          } while (iVar13 != 0);
          if (iVar3 == 0) {
LAB_0050b681:
            FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1c4),0,&DAT_00560728,0);
            DAT_00578e0c = -1;
            goto switchD_0050abbd_caseD_13;
          }
LAB_0050ac64:
          uVar5 = *(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x198);
LAB_0050ad2f:
          FUN_00544350(uVar5,0,&DAT_00560728,0);
          DAT_0057333c = 0;
          DAT_00578e0c = 0;
          uVar8 = param_4;
          bVar1 = DAT_00560fef;
          cVar10 = DAT_005e909f;
LAB_0050ad53:
          uVar4 = DAT_00578e0c + 5;
          if (0x68 < uVar4) goto switchD_0050abbd_caseD_13;
          goto LAB_0050abb5;
        case 0xe:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if ((bVar1 & 2) == 0) {
              FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1c8),0,&DAT_00560728,0)
              ;
              DAT_00578e0c = DAT_00578e0c + 1;
            }
          }
          else {
            DAT_00578e0c = -5;
          }
          goto switchD_0050abbd_caseD_13;
        case 0xf:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if ((bVar1 & 2) == 0) {
              FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 400),0,&DAT_00560728,0);
              DAT_00578e0c = DAT_00578e0c + 1;
            }
          }
          else {
            DAT_00578e0c = -5;
          }
          goto switchD_0050abbd_caseD_13;
        case 0x10:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if ((bVar1 & 4) != 0) goto switchD_0050abbd_caseD_13;
            if (cVar10 == '\0') {
              FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1cc),0,&DAT_00560728,0)
              ;
              FUN_005046c0((DAT_00604980 & 4) << 2,1);
              FUN_00504730();
              FUN_00512900(DAT_00573338 + 0x1fd0);
              FUN_005128e0(&DAT_00604980);
              iVar13 = 10;
              iVar3 = 0;
              do {
                if (iVar3 != 0) goto LAB_0050ad18;
                iVar3 = Ordinal_20(&DAT_00604940);
                iVar13 = iVar13 + -1;
              } while (iVar13 != 0);
              if (iVar3 == 0) goto LAB_0050b681;
LAB_0050ad18:
              uVar5 = *(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x198);
              goto LAB_0050ad2f;
            }
          }
          DAT_00578e0c = -5;
          goto switchD_0050abbd_caseD_13;
        case 0x11:
          if ((bVar1 & 2) != 0) goto switchD_0050abbd_caseD_13;
          puVar16 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x70) + 0x1d8);
          break;
        case 0x12:
          if ((bVar1 & 4) == 0) {
            if (cVar10 == '\0') {
              DAT_00578de8 = 0;
              DAT_00578df0 = 0;
            }
            else {
              DAT_005de51c = 0;
              DAT_00578de8 = 0xffffffff;
              DAT_005de510 = 1;
            }
          }
        default:
          goto switchD_0050abbd_caseD_13;
        case 99:
joined_r0x0050b70f:
          if ((bVar1 & 2) == 0) {
LAB_0050b711:
            DAT_00562b68 = -1;
          }
          goto switchD_0050abbd_caseD_13;
        case 0xfffffffb:
          FUN_005046c0((DAT_00604980 & 4) << 2,1);
          FUN_00504730();
          bVar1 = DAT_00560fef;
        case 0xfffffffc:
          DAT_00560fef = bVar1 & 0xf9;
          DAT_00560763 = 0;
          DAT_0056075b = 0;
          DAT_00562b68 = -1;
          FUN_0053dba0();
          goto switchD_0050abbd_caseD_13;
        case 0xfffffffd:
        case 0xfffffffe:
          if ((*(ushort *)(uVar8 + 2) & 0x4000) == 0) {
            if (DAT_00578de0 == -3) {
              DAT_0057333c = 0;
              DAT_00578e0c = 0;
            }
            else if ((bVar1 & 2) == 0) {
              FUN_005046c0((DAT_00604980 & 4) << 2,1);
              FUN_00504730();
              goto LAB_0050b711;
            }
          }
          else {
            DAT_00578e0c = -5;
          }
          goto switchD_0050abbd_caseD_13;
        case 0xffffffff:
          goto joined_r0x0050b70f;
        }
        FUN_00544350(puVar16,0,&DAT_00560728,0);
LAB_0050b6d0:
        DAT_00578e0c = DAT_00578e0c + 1;
      }
    }
switchD_0050abbd_caseD_13:
    if (((-1 < DAT_00562b68) && (9 < DAT_00578e0c)) && (DAT_00578e0c < 0xe)) {
      local_10 = (undefined1 *)0x1000000;
      local_14 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x94) + 0x38);
      local_4 = 0;
      local_c = 0xb4;
      local_a = 0x30;
      param_3 = FUN_00521df0(param_1,param_3,&local_14,0,0);
      local_4 = 1;
      local_c = 0xe8;
      param_3 = FUN_00521df0(param_1,param_3,&local_14,0,0);
      param_3 = FUN_0051f9c0(param_1,param_3,&local_14);
      local_14 = &DAT_00566c30;
      local_c = 0xbc;
      local_a = 0x38;
      param_3 = FUN_0051be90(param_1,param_3,&local_14,
                             *(undefined4 *)(DAT_006049bc + 0x18 + (DAT_00604984 & 0xff) * 0x28),
                             0x82,0);
      local_c = 0xec;
      param_3 = FUN_0051be90(param_1,param_3,&local_14,DAT_00604980 >> 0x10 & 0xff,0x82,0);
      param_4 = DAT_00604980 >> 8 & 0xff;
      local_4 = 0;
      _DAT_005e8ff8 = 0xf;
      _DAT_005e8ffa = 0x7f80;
      local_14 = &DAT_00563148;
      uVar8 = _DAT_00604988 -
              (int)(0x1000 / (ulonglong)(longlong)(int)param_4) * (DAT_00604984 & 0xff) & 0xfff;
      if (uVar8 < 2000) {
        sVar11 = (short)((int)uVar8 >> 2);
      }
      else {
        sVar11 = -(short)((int)(0x1000 - uVar8) >> 2);
      }
      DAT_00604988 = DAT_00604988 - sVar11;
      _DAT_00604988 = CONCAT22(DAT_0060498a + (short)(0x1800 - DAT_0060498a >> 2),DAT_00604988);
      for (; param_4 != 0; param_4 = param_4 - 1) {
        uVar8 = DAT_00604980 >> 8 & 0xff;
        iVar13 = (int)(param_4 + (DAT_00604984 & 0xff)) % (int)uVar8;
        iVar14 = (int)(0x1000 / (ulonglong)(longlong)(int)uVar8) * iVar13 - (int)DAT_00604988;
        iVar3 = Ordinal_451(iVar14);
        local_10 = (undefined1 *)
                   CONCAT22(local_10._2_2_,(short)(iVar3 * DAT_0060498a >> 0x14) + 0xd3);
        iVar3 = Ordinal_450(iVar14);
        sVar11 = -(short)(iVar3 * DAT_0060498a >> 0x14);
        local_a = sVar11 + 100;
        DAT_00563148 = (char)iVar13 << 4;
        DAT_00563149 = ((char)((DAT_005e9960 >> 2) % (uint)(byte)(&DAT_0060498c)[iVar13]) + '\x02')
                       * '\x10';
        if (param_4 == (DAT_00604980 >> 8 & 0xff)) {
          local_c = local_c + 8;
          local_a = sVar11 + 0x6c;
          iVar3 = Ordinal_451((DAT_005e9960 & 7) << 9);
          sStack_8 = (short)(iVar3 >> 2) + 0x1400;
          local_10 = (undefined1 *)DAT_005e9814;
          sStack_6 = sStack_8;
          param_3 = FUN_00521f20(param_1,param_3,&local_14,0,iVar13);
        }
        else {
          param_3 = FUN_00521df0(param_1,param_3,&local_14,3,iVar13);
        }
      }
      param_3 = FUN_0051f9c0(param_1,param_3,&local_14);
    }
  }
  FUN_0051be20(param_1,&param_3);
  local_14 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x94) + 0x34);
  if (((DAT_00562b68 < 0) || (DAT_00578e0c < 10)) || (local_10 = &LAB_00404040, 0xd < DAT_00578e0c))
  {
    local_10 = (undefined1 *)0x1000000;
  }
  if (DAT_00604b0a == -0x8000) {
    local_4 = 1;
    local_c = 0xa9;
    local_a = 0x6e;
    param_3 = FUN_00521df0(param_1,param_3,&local_14,0,0);
  }
  if (DAT_00604b2e == -0x8000) {
    local_4 = 2;
    local_c = 0xdd;
    local_a = 0x6e;
    param_3 = FUN_00521df0(param_1,param_3,&local_14,0,0);
  }
  local_4 = 0;
  local_c = 0xbd;
  local_a = 0x48;
  FUN_0051bdb0(param_1,&param_3);
  param_3 = FUN_00545340(param_1,param_3,&DAT_00573518);
  FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x158),0,&DAT_00573de0,0);
  param_3 = FUN_005443a0(param_3,&DAT_00573de0);
  FUN_00544350(DAT_00573338 + 0x100,0,&DAT_00573de0,0);
  param_3 = FUN_005443a0(param_3,&DAT_00573de0);
  FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x15c),0,&DAT_00573de0,0);
  param_3 = FUN_005443a0(param_3,&DAT_00573de0);
  FUN_00544350(*(undefined4 *)
                (*(int *)(DAT_005e97c4 + 0x70) + 0xc + (uint)(byte)DAT_00573338[0x150] * 4),0,
               &DAT_00573de0,0);
  param_3 = FUN_005443a0(param_3,&DAT_00573de0);
  DAT_005e909a = 1;
  DAT_005e909b = 0xf;
  DAT_005e909d = 0x10;
  DAT_005e909c = (char)((ulonglong)(DAT_005e9960 >> 2 & 0x1fffffff) % 3) +
                 DAT_00573338[0x151] * '\x03';
  DAT_005e909e = DAT_00573338[0x150];
  FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x160),0,&DAT_00573de0,0);
  param_3 = FUN_005443a0(param_3,&DAT_00573de0);
  local_a = DAT_00573de4._2_2_ + 0x52;
  local_10 = (undefined1 *)0x1000000;
  uVar8 = 0;
  do {
    FUN_0051be20(param_1,&param_3);
    local_4 = (short)uVar8 + 0xb;
    local_14 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x94) + 0x30);
    local_c = (-(ushort)((uVar8 & 1) != 0) & 0x38) + 0x12 + (short)DAT_00573de4;
    param_3 = FUN_00521df0(param_1,param_3,&local_14,0,0);
    param_3 = FUN_0051f9c0(param_1,param_3,&local_14);
    FUN_0051bdb0(param_1,&param_3);
    local_c = local_c + 0x1c;
    local_14 = &DAT_00566c30;
    param_3 = FUN_0051be90(param_1,param_3,&local_14,*(undefined1 *)((int)&DAT_005e9064 + uVar8 + 3)
                           ,0x82,0);
    uVar8 = uVar8 + 1;
    local_4 = local_4 + 1;
  } while ((int)uVar8 < 2);
  local_a = DAT_00573de4._2_2_ + 0x60;
  uVar8 = 0;
  do {
    FUN_0051be20(param_1,&param_3);
    local_4 = (short)uVar8 + 4;
    local_14 = *(undefined **)(*(int *)(DAT_005e97c4 + 0x94) + 0x30);
    local_c = (-(ushort)((uVar8 & 1) != 0) & 0x38) + 0x12 + (short)DAT_00573de4;
    param_3 = FUN_00521df0(param_1,param_3,&local_14,0,0);
    param_3 = FUN_0051f9c0(param_1,param_3,&local_14);
    FUN_0051bdb0(param_1,&param_3);
    local_c = local_c + 0x1c;
    local_14 = &DAT_00566c30;
    param_3 = FUN_0051be90(param_1,param_3,&local_14,*(undefined1 *)((int)&DAT_005e9060 + uVar8),
                           0x82,0);
    local_a = local_a + (-(ushort)((uVar8 & 1) != 0) & 9);
    uVar8 = uVar8 + 1;
    local_4 = local_4 + 1;
  } while ((int)uVar8 < 7);
  FUN_00545340(param_1,param_3,&DAT_00573de0);
  return;
}


