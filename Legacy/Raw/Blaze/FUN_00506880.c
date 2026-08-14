/*
 * Function: FUN_00506880
 * Entry:    00506880
 * Body:     [[00506880, 0050700e]]
 */


void FUN_00506880(void)

{
  int *piVar1;
  uint uVar2;
  short sVar3;
  undefined4 *puVar4;
  undefined4 uVar5;
  short sVar6;
  undefined4 *puVar7;
  int iVar8;
  undefined4 unaff_EBX;
  undefined4 unaff_EBP;
  undefined4 unaff_ESI;
  int iVar9;
  int *piVar10;
  undefined **ppuVar11;
  undefined4 uVar12;
  int iVar13;
  undefined4 local_20;
  undefined4 local_1c;
  short local_18;
  short sStack_16;
  undefined2 uStack_14;
  undefined2 uStack_12;
  short sStack_10;
  
  FUN_0052a1e0(&local_1c,&local_18,&local_20);
  iVar13 = 400;
  Ordinal_379(400);
  uVar12 = 0x78;
  Ordinal_378(0xa0,0x78);
  FUN_00529ad0(4);
  iVar9 = 0;
  puVar4 = &DAT_00573520;
  do {
    puVar4[-1] = 0;
    puVar4[3] = iVar9 + 3;
    *puVar4 = 0;
    puVar4[1] = 0;
    puVar4[7] = 0x1000000;
    puVar4[8] = 0;
    puVar4[9] = 0x1000000;
    puVar4[0xb] = 0x1000000;
    puVar4[0xd] = 0x1000000;
    puVar4[0xf] = 0x1000000;
    puVar4[-2] = 0x31;
    puVar4[2] = 0x10180000;
    puVar4[4] = 0x10f0808;
    puVar4[5] = 0x2cf1020;
    puVar4[6] = 0x1070c10;
    puVar4[10] = 0xf0000;
    puVar4[0xc] = 0xf0c00;
    puVar4[0xe] = 0x10f0000;
    puVar7 = puVar4 + 0x11;
    iVar8 = 4;
    do {
      puVar7[-1] = 0xf0000;
      *puVar7 = 0x1000000;
      puVar7 = puVar7 + 2;
      iVar8 = iVar8 + -1;
    } while (iVar8 != 0);
    iVar9 = iVar9 + 1;
    puVar4 = puVar4 + 0x232;
  } while (iVar9 < 9);
  DAT_00573338 = *(int *)MainMem_exref + 0xf0000;
  iVar9 = 0;
  do {
    *(undefined4 *)(DAT_00573338 + iVar9) = 0;
    iVar9 = iVar9 + 4;
  } while (iVar9 < 0x2000);
  FUN_00504b10(0);
  while( true ) {
    DAT_005de15c = 1;
    if ((DAT_00564a38 == 0) && (7 < DAT_005de160)) {
      DAT_005de158 = 0;
      DAT_005de154 = 0;
    }
    else {
      DAT_005de154 = 0xa0;
      DAT_005de158 = (0xb - DAT_005de160) * 0xc;
    }
    FUN_0040a600(4);
    if (((*(byte *)((int)&DAT_00604a20 +
                   (uint)(byte)(&DAT_005e9188)
                               [(uint)(byte)(&DAT_005e93c8)[(uint)DAT_005de538 * 0x9c] * 0x4c] *
                   0x10 + 2) & 9) == 9) && ((DAT_00578de8 != 7 || (DAT_00562b68 < 0)))) break;
    if (DAT_00578dec != DAT_00578de8) {
      DAT_00578dec = DAT_00578de8;
      DAT_00562a48 = 1;
      DAT_00562a4c = 1;
    }
    uVar5 = (**(code **)(&DAT_00560708 + DAT_00578de8 * 4))
                      (unaff_EBP,unaff_EBX,unaff_ESI,
                       &DAT_00604a20 +
                       (uint)(byte)(&DAT_005e9188)
                                   [(uint)(byte)(&DAT_005e93c8)[(uint)DAT_005de538 * 0x9c] * 0x4c] *
                       4);
    if (DAT_00578de8 == -1) goto LAB_00506fe7;
    DAT_00562a4c = 0;
    if ((DAT_00578de8 != 0) && (DAT_00578de8 < 7)) {
      if (4 < DAT_00578de8) {
        DAT_005e93a0 = DAT_005e93a0 +
                       (short)((int)*(short *)(&DAT_00562a58 +
                                              (uint)*(byte *)(DAT_00573338 + 0x151) * 8) -
                               (int)DAT_005e93a0 >> 2);
        DAT_005e93a2 = DAT_005e93a2 +
                       (short)((int)*(short *)(&DAT_00562a5a +
                                              (uint)*(byte *)(DAT_00573338 + 0x151) * 8) -
                               (int)DAT_005e93a2 >> 2);
        DAT_005e93a4 = DAT_005e93a4 +
                       (short)((int)*(short *)(&DAT_00562a5c +
                                              (uint)*(byte *)(DAT_00573338 + 0x151) * 8) -
                               (int)DAT_005e93a4 >> 2);
        Ordinal_376((int)DAT_005e93a0,(int)DAT_005e93a2,(int)DAT_005e93a4);
        piVar10 = &DAT_00572c34;
        do {
          piVar1 = piVar10 + -0xd;
          iVar8 = FUN_00547140();
          iVar9 = *piVar10;
          if (((*piVar1 - DAT_00572b64) + 0x3fff) - iVar8 < 0) {
            if (iVar9 < 0x19000) {
              iVar9 = iVar9 + 0x2000;
LAB_00506b5a:
              *piVar10 = iVar9;
            }
          }
          else if (-0x19000 < iVar9) {
            iVar9 = iVar9 + -0x2000;
            goto LAB_00506b5a;
          }
          iVar8 = FUN_00547140();
          iVar9 = piVar10[1];
          if (((piVar10[-0xc] - DAT_00572b68) + -0x88001) - iVar8 < 0) {
            if (iVar9 < 0x19000) {
              iVar9 = iVar9 + 0x2000;
LAB_00506b93:
              piVar10[1] = iVar9;
            }
          }
          else if (-0x19000 < iVar9) {
            iVar9 = iVar9 + -0x2000;
            goto LAB_00506b93;
          }
          iVar8 = FUN_00547140();
          iVar9 = piVar10[2];
          if (((piVar10[-0xb] - DAT_00572b6c) + 0x3fff) - iVar8 < 0) {
            if (iVar9 < 0x19000) {
              iVar9 = iVar9 + 0x2000;
LAB_00506bcd:
              piVar10[2] = iVar9;
            }
          }
          else if (-0x19000 < iVar9) {
            iVar9 = iVar9 + -0x2000;
            goto LAB_00506bcd;
          }
          *piVar1 = *piVar1 + *piVar10;
          uVar2 = DAT_005e9960;
          piVar10[-0xc] = piVar10[-0xc] + piVar10[1];
          piVar10[-0xb] = piVar10[-0xb] + piVar10[2];
          *(undefined2 *)(piVar10 + -10) = 0x640;
          *(undefined2 *)((int)piVar10 + -0x26) = 0x640;
          *(undefined2 *)(piVar10 + -9) = 0x640;
          *(char *)(piVar10 + -4) = (char)((ulonglong)(uVar2 >> 1) % 6);
          uVar12 = FUN_00520ae0(iVar13 + 0x14,uVar12,piVar1,0,0);
          piVar10 = piVar10 + 0x27;
        } while ((int)piVar10 < 0x572ea4);
        DAT_005e9308 = 0;
        sVar6 = (short)(DAT_00572b6c - DAT_00572c08 >> 0xc);
        sVar3 = (short)(DAT_00572b64 - DAT_00572c00 >> 0xc);
        unaff_EBX = CONCAT22(sVar6,sVar3);
        sVar3 = Ordinal_457((int)sVar3,(int)sVar6);
        DAT_005e930a = -sVar3;
        DAT_005e930c = 0;
      }
      DAT_00572b70 = DAT_00572b70 + (short)(0x1e00 - DAT_00572b70 >> 2);
      DAT_00572b72 = DAT_00572b72 + (short)(0x1e00 - DAT_00572b72 >> 2);
      DAT_00572b74 = DAT_00572b74 + (short)(0x1e00 - DAT_00572b74 >> 2);
      DAT_00572b64 = DAT_00572b64 + (-DAT_00572b64 >> 2);
      DAT_00572b68 = DAT_00572b68 + (-0x64000 - DAT_00572b68 >> 2);
      DAT_00572b6c = DAT_00572b6c + (-DAT_00572b6c >> 2);
      iVar9 = DAT_00572b64;
      if (DAT_00572b64 < 0) {
        iVar9 = -DAT_00572b64;
      }
      if (iVar9 < 0x1000) {
        DAT_00572b64 = 0;
      }
      iVar9 = DAT_00572b6c;
      if (DAT_00572b6c < 0) {
        iVar9 = -DAT_00572b6c;
      }
      if (iVar9 < 0x1000) {
        DAT_00572b6c = 0;
      }
      iVar9 = (DAT_00572b4c & 0xff) * 4;
      sVar3 = DAT_00572b62 + 1;
      DAT_00572b62 = sVar3;
      if ((int)(uint)*(ushort *)
                      (*(int *)(*(int *)(&DAT_005e9640 + iVar9) + DAT_00572b60 * 4) + 0x16) <=
          (int)sVar3) {
        DAT_00572b62 = 0;
      }
      uVar5 = FUN_0051d1f0(unaff_EBP,uVar5,
                           *(undefined4 *)(*(int *)(&DAT_005e9640 + iVar9) + DAT_00572b60 * 4),
                           *(undefined4 *)(&DAT_005e96a0 + iVar9),&DAT_00572b64,(int)DAT_00572b62,
                           CONCAT22(sVar3 >> 0xf,DAT_00572b52));
    }
    iVar9 = 0;
    DAT_006008fc = DAT_006008fc + (800 - DAT_006008fc >> 2);
    local_20 = *(undefined4 *)(*(int *)(DAT_005e97c4 + 0x94) + 0x30);
    if (0 < DAT_00562a50) {
      ppuVar11 = &PTR_LAB_00560ff6;
      do {
        local_18 = *(short *)((int)ppuVar11 + -2) + 0xc;
        sStack_16 = *(short *)ppuVar11 + 4;
        sStack_10 = (short)(char)(&DAT_00562a98)[DAT_00578de8 * 4 + iVar9];
        local_1c = 0x1000000;
        FUN_0051be20(unaff_EBP,&stack0xffffffd4);
        uVar5 = FUN_00521df0(unaff_EBP,uVar5,&local_20,0,0);
        uVar5 = FUN_0051f9c0(unaff_EBP,uVar5,&local_20);
        FUN_0051bdb0(unaff_EBP,&stack0xffffffd4);
        uVar5 = FUN_00545340(unaff_EBP,uVar5,(short *)((int)ppuVar11 + -6));
        iVar9 = iVar9 + 1;
        ppuVar11 = ppuVar11 + 0x232;
      } while (iVar9 < DAT_00562a50);
    }
    if ((DAT_00564a38 == 0) && (7 < DAT_005de160)) {
      DAT_005de158 = DAT_005de160 * 0xc + -0x70;
    }
    else if (DAT_005de160 < 1) {
      DAT_005de158 = DAT_005de158 + -8;
    }
    else {
      DAT_005de158 = (DAT_005de160 + 9) * 0xc;
    }
    if (-1 < DAT_00578df0) {
      FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + DAT_00578df0 * 4),0,&DAT_00560728
                   ,0);
    }
    uVar5 = FUN_005443a0(uVar5,&DAT_00560728);
    uVar5 = FUN_00545340(unaff_EBP,uVar5,&DAT_00560728);
    DAT_005de15c = 0;
    local_18 = 0xa0;
    sStack_16 = 0x78;
    uStack_14 = 0x1420;
    uStack_12 = 0x1149;
    sStack_10 = 0;
    local_20 = **(undefined4 **)(DAT_005e97c4 + 0x94);
    local_1c = 0x1000000;
    uVar5 = FUN_00521f20(unaff_EBX,uVar5,&local_20,0,0);
    unaff_ESI = FUN_0051f9c0(unaff_EBX,uVar5,&local_20);
    FUN_0052a280(&stack0xffffffd8,&stack0xffffffdc,&stack0xffffffd4,3,1);
  }
  DAT_005de510 = 0;
LAB_00506fe7:
  FUN_0052a280(&stack0xffffffd8,&stack0xffffffdc,&stack0xffffffd4,3,1);
  DAT_005de15c = 0;
  return;
}


