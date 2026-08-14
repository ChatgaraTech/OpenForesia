/*
 * Function: FUN_005098f0
 * Entry:    005098f0
 * Body:     [[005098f0, 0050a73b]]
 */


void FUN_005098f0(undefined4 param_1,undefined4 param_2,undefined4 param_3,int param_4)

{
  char *pcVar1;
  byte *pbVar2;
  char cVar3;
  byte bVar4;
  ushort uVar5;
  short sVar6;
  uint uVar7;
  undefined4 *puVar8;
  int iVar9;
  short *psVar10;
  int iVar11;
  undefined4 *puVar12;
  char *pcVar13;
  int iVar14;
  undefined4 uStack_14;
  undefined4 uStack_10;
  short sStack_c;
  short sStack_a;
  short sStack_4;
  
  if (DAT_00562a48 != 0) {
    iVar11 = 0;
    DAT_00562a48 = 0;
    DAT_00578e08 = 0;
    iVar14 = 0;
    puVar8 = &DAT_00573518;
    do {
      puVar12 = puVar8;
      iVar9 = Ordinal_450(iVar14);
      *(short *)(puVar12 + 1) = (short)(iVar9 * -0x58 >> 0xc) + 0x88;
      iVar9 = Ordinal_451(iVar14);
      *(short *)((int)puVar12 + 6) = (short)((iVar9 << 6) >> 0xc) + 0x48;
      *(undefined2 *)(puVar12 + 2) = 0x20;
      *(undefined2 *)((int)puVar12 + 10) = 0x18;
      puVar12[4] = 0x1180000;
      puVar12[5] = iVar11 + 3;
      puVar12[6] = 0x10f0808;
      puVar12[7] = 0x2cf1020;
      puVar12[8] = 0x1070c10;
      puVar12[9] = 0x1000000;
      puVar12[10] = 0;
      puVar12[0xc] = 0;
      puVar12[0xe] = 0;
      puVar12[0x10] = 0x10f0000;
      puVar12[0x11] = 0x1000000;
      puVar12[0x12] = 0;
      puVar12[0x14] = 0;
      puVar12[0x16] = 0;
      puVar12[0x18] = 0;
      iVar14 = iVar14 + 0x155;
      iVar11 = iVar11 + 1;
      puVar8 = puVar12 + 0x232;
    } while (iVar14 < 0x953);
    *(undefined2 *)(puVar12 + 0x233) = 0x58;
    *(undefined2 *)((int)puVar12 + 0x8ce) = 0x38;
    *(undefined2 *)(puVar12 + 0x234) = 0x28;
    *(undefined2 *)((int)puVar12 + 0x8d2) = 0x18;
    *(undefined1 *)(puVar12 + 0x23a) = 0x10;
    puVar12[0x237] = 10;
    *(undefined2 *)(puVar12 + 0x465) = 0xb0;
    *(undefined2 *)((int)puVar12 + 0x1196) = 0x38;
    *(undefined2 *)(puVar12 + 0x466) = 0x28;
    *(undefined2 *)((int)puVar12 + 0x119a) = 0x18;
    *(undefined1 *)(puVar12 + 0x46c) = 0x10;
    puVar12[0x469] = 0xb;
    switch(DAT_00573338[0x150] & 7) {
    case 0:
    case 6:
      *(undefined2 *)(DAT_00573338 + 0x148) = 0x10;
      *(undefined2 *)(DAT_00573338 + 0x14a) = 2;
      break;
    case 1:
    case 3:
    case 7:
      *(undefined2 *)(DAT_00573338 + 0x148) = 0xe;
      *(undefined2 *)(DAT_00573338 + 0x14a) = 8;
      break;
    case 2:
    case 4:
    case 5:
      *(undefined2 *)(DAT_00573338 + 0x148) = 0xf;
      *(undefined2 *)(DAT_00573338 + 0x14a) = 4;
    }
  }
  iVar14 = param_4;
  iVar11 = DAT_00578e08;
  if (DAT_00562a4c != 0) goto switchD_00509aa1_caseD_43;
  switch(DAT_00578df0) {
  case 0x3e:
    uVar5 = *(ushort *)(param_4 + 2);
    if ((uVar5 & 0x4000) == 0) {
      if (((uVar5 & 0x8000) == 0) || (DAT_005e906a != '\0')) {
        if (((*(byte *)(param_4 + 6) & 0x80) != 0) && (DAT_00578e08 != 0)) {
          DAT_00578e08 = DAT_00578e08 + -1;
        }
        if (((*(byte *)(param_4 + 6) & 0x20) != 0) && (DAT_00578e08 < 6)) {
          DAT_00578e08 = DAT_00578e08 + 1;
        }
        if (iVar11 != DAT_00578e08) {
          FUN_0053db60();
        }
        if ((((*(byte *)(iVar14 + 6) & 0x10) != 0) && (DAT_005e906a != '\0')) &&
           (*(byte *)((int)&DAT_005e9060 + DAT_00578e08) < 99)) {
          DAT_005e906a = DAT_005e906a + -1;
          *(char *)((int)&DAT_005e9060 + DAT_00578e08) =
               *(char *)((int)&DAT_005e9060 + DAT_00578e08) + '\x01';
          FUN_0053db80();
        }
        if (((*(byte *)(iVar14 + 6) & 0x40) != 0) &&
           (*(byte *)(DAT_00578de4 + DAT_00578e08) < *(byte *)((int)&DAT_005e9060 + DAT_00578e08)))
        {
          *(byte *)((int)&DAT_005e9060 + DAT_00578e08) =
               *(byte *)((int)&DAT_005e9060 + DAT_00578e08) - 1;
          DAT_005e906a = DAT_005e906a + '\x01';
          FUN_0053dba0();
        }
        uVar7 = DAT_005e9064 >> 8;
        DAT_005e9064 = CONCAT13((char)((int)((DAT_005e9060 & 0xff) + (DAT_005e9064 & 0xff)) >> 1) +
                                DAT_00573338[0x148],(undefined3)DAT_005e9064);
        DAT_005e9068 = (char)((int)((DAT_005e9060 >> 0x10 & 0xff) + (uVar7 & 0xff)) >> 1) +
                       DAT_00573338[0x14a];
        FUN_0051be20(param_1,&param_3);
        uStack_14 = **(undefined4 **)(DAT_005e97c0 + 0x94);
        sStack_4 = 0x18;
        uStack_10 = 0x1000000;
        sStack_c = *(short *)(&DAT_0057351c + DAT_00578e08 * 0x232);
        sStack_a = *(short *)((int)&DAT_0057351c + DAT_00578e08 * 0x8c8 + 2) + 0xc;
        param_3 = FUN_00521df0(param_1,param_3,&uStack_14,0,0);
        param_3 = FUN_0051f9c0(param_1,param_3,&uStack_14);
        FUN_0051bdb0(param_1,&param_3);
        FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x1e0 + DAT_00578e08 * 4),0,
                     &DAT_00560728,0);
        param_3 = FUN_005443a0(param_3,&DAT_00560728);
      }
      else {
        *(ushort *)(param_4 + 2) = uVar5 & 0x7fff;
        FUN_0053db80();
        DAT_00578df0 = 0x81;
        *DAT_00573338 = 0;
        DAT_00573338[1] = 0;
        DAT_00573338[2] = 0;
        DAT_00573338[3] = 0;
        *(undefined4 *)(DAT_00573338 + 0x140) = 0;
        *(undefined2 *)(DAT_00573338 + 0x144) = 1;
        *(undefined2 *)(DAT_00573338 + 0x146) = 1;
        *(undefined4 *)(DAT_00573338 + 0x158) = 0;
        *(undefined4 *)(DAT_00573338 + 0x15c) = 0;
        *(uint *)(DAT_00573338 + 0x114) = 1 << (DAT_00573338[0x151] & 0x1f) | 0x800;
        *(short *)(DAT_00573338 + 0x112) = (short)(1 << (DAT_00573338[0x150] & 7));
        iVar11 = 0x2a0;
        iVar14 = 0;
        do {
          iVar9 = iVar11 + 2;
          *(ushort *)(DAT_00573338 + iVar11 + -0x20) =
               (ushort)*(byte *)((int)&DAT_005e9060 + iVar14);
          *(ushort *)(DAT_00573338 + iVar11) = (ushort)*(byte *)((int)&DAT_005e9060 + iVar14);
          iVar11 = iVar9;
          iVar14 = iVar14 + 1;
        } while (iVar9 < 0x2ae);
        *(ushort *)(DAT_00573338 + 0x148) = (ushort)DAT_005e9064._3_1_;
        *(ushort *)(DAT_00573338 + 0x14a) = (ushort)DAT_005e9068;
        *(undefined2 *)(DAT_00573338 + 0x14c) = *(undefined2 *)(DAT_00573338 + 0x148);
        *(undefined2 *)(DAT_00573338 + 0x14e) = *(undefined2 *)(DAT_00573338 + 0x14a);
        *(undefined4 *)(DAT_00573338 + 0x26c) = 0;
        DAT_00573338[0x2b0] = 0;
        DAT_00573338[0x2b1] = 1;
        if (((DAT_00573338[0x150] & 7) == 0) || ((DAT_00573338[0x150] & 7) == 6)) {
          DAT_00573338[0x2b2] = 2;
        }
        else {
          DAT_00573338[0x2b2] = 0xff;
        }
        DAT_00573338[0x2b3] = 0xff;
        DAT_00573338[0x2b4] = 0xff;
        DAT_00573338[0x2b5] = (&DAT_00562ab8)[(byte)DAT_00573338[0x150] & 7];
        DAT_00573338[0x2b6] = 0;
        DAT_00573338[0x2b7] = 0;
        DAT_00573338[0x2b8] = 0;
        DAT_00573338[0x2b9] = 0;
        DAT_00573338[0x2ba] = 0;
        DAT_00573338[699] = 0;
        DAT_00573338[0x160] = 1;
        DAT_00573338[0x161] = 0;
        DAT_00573338[0x162] = 0;
        DAT_00573338[0x163] = 0;
        DAT_00573338[0x168] = 1;
        DAT_00573338[0x169] = 0;
        DAT_00573338[0x16a] = 0;
        DAT_00573338[0x16b] = 0;
        DAT_00573338[0x170] = 1;
        DAT_00573338[0x171] = 0;
        DAT_00573338[0x172] = 0;
        DAT_00573338[0x173] = 0;
        DAT_00573338[0x178] = 3;
        DAT_00573338[0x179] = 3;
        DAT_00573338[0x17a] = 3;
        DAT_00573338[0x17b] = 3;
        DAT_00573338[0x17c] = 3;
        DAT_00573338[0x17d] = 3;
        DAT_00573338[0x17e] = 3;
        DAT_00573338[0x1f00] = 0;
        DAT_00573338[0x1f01] = 0;
        DAT_00573338[0x1f02] = 0;
        DAT_00573338[0x1f03] = 0;
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1d00] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1d40] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1d80] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1dc0] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1e00] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1e40] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1e80] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x1ec0] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 8;
          *(undefined4 *)(DAT_00573338 + iVar11 + 0x1f04) = 0;
          *(undefined4 *)(DAT_00573338 + iVar11 + 0x1f08) = 0xffffffff;
          iVar11 = iVar14;
        } while (iVar14 < 0x40);
        iVar14 = 0;
        iVar11 = 0x1fc0;
        do {
          iVar9 = iVar11 + 2;
          pbVar2 = &DAT_00562ac0 + iVar14;
          iVar14 = iVar14 + 1;
          *(undefined2 *)(DAT_00573338 + iVar11) = *(undefined2 *)((uint)*pbVar2 * 2 + 0x562ac8);
          iVar11 = iVar9;
        } while (iVar9 < 0x1fd0);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 1;
          DAT_00573338[iVar11 + 0x2e0] = 0;
          iVar11 = iVar14;
        } while (iVar14 < 0x20);
        FUN_005352e0((((byte)DAT_00573338[0x150] & 7) + 0x7c) * 0x2000 + *(int *)MainMem_exref,
                     DAT_00573338 + 0x300,0x640);
        iVar11 = 0;
        do {
          iVar14 = iVar11 + 0x280;
          *(ushort *)(DAT_00573338 + iVar11 + 0x312) =
               *(ushort *)(DAT_00573338 + iVar11 + 0x312) | 0x8000;
          iVar11 = iVar14;
        } while (iVar14 < 0x1900);
        FUN_00538ab0(DAT_00573338);
        FUN_00538f50(DAT_00573338 + 0x100);
        FUN_00538c50(DAT_00573338);
        FUN_00538e20(DAT_00573338);
        FUN_00538fa0(DAT_00573338 + 0x100);
        iVar11 = 0;
        iVar14 = 0;
        do {
          iVar9 = iVar14 + 0x100;
          iVar14 = iVar14 + 1;
          iVar11 = (iVar11 + (uint)(byte)DAT_00573338[iVar9]) * 2;
        } while (iVar14 < 0x10);
        psVar10 = (short *)(DAT_00573338 + 0x280);
        iVar14 = 7;
        do {
          sVar6 = *psVar10;
          psVar10 = psVar10 + 1;
          iVar11 = (iVar11 + sVar6) * 2;
          iVar14 = iVar14 + -1;
        } while (iVar14 != 0);
        iVar14 = ((uint)(byte)DAT_00573338[0x153] +
                 ((uint)(byte)DAT_00573338[0x152] +
                 ((uint)(byte)DAT_00573338[0x151] + ((uint)(byte)DAT_00573338[0x150] + iVar11) * 2)
                 * 2) * 2) * 2;
        iVar11 = 0;
        do {
          iVar9 = iVar11;
          PTR_DAT_00566bc0[iVar9 + 0xc] = ((byte)iVar14 & 0x1f) + 0x40;
          if (PTR_DAT_00566bc0[iVar9 + 0xc] == '\\') {
            PTR_DAT_00566bc0[iVar9 + 0xc] = 0x37;
          }
          iVar14 = iVar14 >> 5;
          iVar11 = iVar9 + 1;
        } while (iVar9 + 1 < 7);
        if (PTR_DAT_00566bc0[iVar9 + 0xd] == '\\') {
          PTR_DAT_00566bc0[iVar9 + 0xd] = 0x37;
        }
        PTR_DAT_00566bc0[iVar9 + 0xd] = 0;
        cVar3 = *PTR_DAT_00566bc0;
        iVar11 = 0;
        pcVar13 = PTR_DAT_00566bc0;
        while (cVar3 != '\0') {
          pcVar13 = pcVar13 + 1;
          DAT_00573338[iVar11 + 0x1fd0] = cVar3;
          iVar11 = iVar11 + 1;
          cVar3 = *pcVar13;
        }
        DAT_00573338[iVar11 + 0x1fd0] = 0;
      }
      goto switchD_00509aa1_caseD_43;
    }
    break;
  case 0x3f:
    if ((DAT_00560fef & 2) == 0) {
LAB_0050a2a8:
      *(ushort *)(param_4 + 2) = *(ushort *)(param_4 + 2) & 0x7fff;
      DAT_00578df0 = DAT_00578df0 + 1;
      goto switchD_00509aa1_caseD_43;
    }
    bVar4 = *(byte *)(param_4 + 3);
    goto LAB_0050a2c6;
  case 0x40:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        DAT_00578df0 = 0x39;
        DAT_00578de8 = DAT_00578de8 + -1;
      }
      else {
        DAT_00578df0 = 0x3e;
      }
      goto switchD_00509aa1_caseD_43;
    }
    bVar4 = *(byte *)(param_4 + 3);
    goto LAB_0050a31a;
  case 0x41:
    if ((DAT_00560fef & 2) == 0) goto LAB_0050a2a8;
    bVar4 = *(byte *)(param_4 + 3);
LAB_0050a2c6:
    if ((bVar4 & 0x40) != 0) {
      DAT_0056075b = 0;
      DAT_00560fef = DAT_00560fef & 0xfd;
      FUN_0053dba0();
      DAT_00578df0 = 0x3e;
    }
    goto switchD_00509aa1_caseD_43;
  case 0x42:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f != '\0') {
        DAT_00578df0 = 0x3e;
        goto switchD_00509aa1_caseD_43;
      }
      goto LAB_0050a366;
    }
    bVar4 = *(byte *)(param_4 + 3);
LAB_0050a31a:
    if ((bVar4 & 0x40) != 0) {
      DAT_00560763 = 0;
      DAT_00560fef = DAT_00560fef & 0xfb;
      FUN_0053dba0();
      DAT_00578df0 = 0x3e;
    }
  default:
    goto switchD_00509aa1_caseD_43;
  case 0x5d:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        iVar11 = 0;
        do {
          iVar14 = iVar11;
          PTR_DAT_00566bc0[iVar14 + 0xc] = 0x30;
          iVar11 = iVar14 + 1;
        } while (iVar14 + 1 < 7);
        PTR_DAT_00566bc0[iVar14 + 0xd] = 0;
        cVar3 = *PTR_DAT_00566bc0;
        iVar11 = 0;
        pcVar13 = PTR_DAT_00566bc0;
        while (cVar3 != '\0') {
          pcVar13 = pcVar13 + 1;
          DAT_00573338[iVar11 + 0x1fd0] = cVar3;
          iVar11 = iVar11 + 1;
          cVar3 = *pcVar13;
        }
        DAT_00573338[iVar11 + 0x1fd0] = 0;
        DAT_005de51c = 1;
        DAT_00578de8 = -1;
        DAT_005de510 = 1;
      }
      else {
        DAT_00578df0 = 0x3e;
        switch(DAT_00573338[0x150] & 7) {
        case 0:
        case 6:
          *(undefined2 *)(DAT_00573338 + 0x148) = 0x10;
          *(undefined2 *)(DAT_00573338 + 0x14a) = 2;
          break;
        case 1:
        case 3:
        case 7:
          *(undefined2 *)(DAT_00573338 + 0x148) = 0xe;
          *(undefined2 *)(DAT_00573338 + 0x14a) = 8;
          break;
        case 2:
        case 4:
        case 5:
          *(undefined2 *)(DAT_00573338 + 0x148) = 0xf;
          *(undefined2 *)(DAT_00573338 + 0x14a) = 4;
        }
      }
      goto switchD_00509aa1_caseD_43;
    }
    if ((*(byte *)(param_4 + 3) & 0x40) == 0) goto switchD_00509aa1_caseD_43;
    DAT_00560763 = 0;
    DAT_00560fef = DAT_00560fef & 0xfb;
    goto LAB_0050a5d1;
  case 0x7f:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)(param_4 + 2) = *(ushort *)(param_4 + 2) & 0x7fff;
      DAT_00578df0 = 0x5d;
      goto switchD_00509aa1_caseD_43;
    }
    if ((*(byte *)(param_4 + 3) & 0x40) == 0) goto switchD_00509aa1_caseD_43;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
LAB_0050a5d1:
    FUN_0053dba0();
    DAT_00578df0 = 0x81;
    goto switchD_00509aa1_caseD_43;
  case 0x80:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)(param_4 + 2) = *(ushort *)(param_4 + 2) & 0x7fff;
      DAT_00578df0 = DAT_00578df0 + -1;
      goto switchD_00509aa1_caseD_43;
    }
    if ((*(byte *)(param_4 + 3) & 0x40) == 0) goto switchD_00509aa1_caseD_43;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
    break;
  case 0x81:
    if ((DAT_00560fef & 4) != 0) {
      if ((*(byte *)(param_4 + 3) & 0x40) != 0) {
        DAT_00560763 = 0;
        DAT_00560fef = DAT_00560fef & 0xfb;
        FUN_0053dba0();
        DAT_00578df0 = 0x3e;
        switch(DAT_00573338[0x150] & 7) {
        case 0:
        case 6:
          *(undefined2 *)(DAT_00573338 + 0x148) = 0x10;
          *(undefined2 *)(DAT_00573338 + 0x14a) = 2;
          break;
        case 1:
        case 3:
        case 7:
          *(undefined2 *)(DAT_00573338 + 0x148) = 0xe;
          *(undefined2 *)(DAT_00573338 + 0x14a) = 8;
          break;
        case 2:
        case 4:
        case 5:
          *(undefined2 *)(DAT_00573338 + 0x148) = 0xf;
          *(undefined2 *)(DAT_00573338 + 0x14a) = 4;
        }
      }
      goto switchD_00509aa1_caseD_43;
    }
    if (DAT_005e909f != '\0') {
      DAT_00578df0 = 0x80;
      goto switchD_00509aa1_caseD_43;
    }
LAB_0050a366:
    DAT_00578df0 = 0x6e;
    DAT_00578de8 = DAT_00578de8 + 1;
    goto switchD_00509aa1_caseD_43;
  }
  FUN_0053dba0();
  DAT_00578df0 = DAT_00578df0 + 1;
switchD_00509aa1_caseD_43:
  FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0x10c),0,&DAT_00562180,0);
  param_3 = FUN_005443a0(param_3,&DAT_00562180);
  iVar11 = 0;
  pcVar13 = (char *)&DAT_00573538;
  param_4 = 0x110;
  uStack_14 = *(undefined4 *)(*(int *)(DAT_005e97c4 + 0x94) + 0x30);
  uStack_10 = 0x1000000;
  do {
    sStack_c = *(short *)(pcVar13 + -0x1c) + 0xc;
    sStack_a = *(short *)(pcVar13 + -0x1a) + 4;
    sStack_4 = (short)iVar11 + 4;
    FUN_0051be20(param_1,&param_3);
    param_3 = FUN_00521df0(param_1,param_3,&uStack_14,0,0);
    param_3 = FUN_0051f9c0(param_1,param_3,&uStack_14);
    FUN_0051bdb0(param_1,&param_3);
    *pcVar13 = ((6 < iVar11) - 1U & 0xf8) + 0x10;
    bVar4 = *(byte *)((int)&DAT_005e9060 + iVar11);
    if (bVar4 < 100) {
      *pcVar13 = *pcVar13 + '\b';
    }
    if (bVar4 < 10) {
      *pcVar13 = *pcVar13 + '\b';
    }
    pcVar1 = pcVar13 + -0x20;
    FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + param_4),0,pcVar1,0);
    param_3 = FUN_005443a0(param_3,pcVar1);
    param_3 = FUN_00545340(param_1,param_3,pcVar1);
    param_4 = param_4 + 4;
    iVar11 = iVar11 + 1;
    pcVar13 = pcVar13 + 0x8c8;
  } while (param_4 < 0x134);
  return;
}


