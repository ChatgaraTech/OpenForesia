/*
 * Function: FUN_00507010
 * Entry:    00507010
 * Body:     [[00507010, 005077df]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00507010(void)

{
  char *pcVar1;
  char cVar2;
  byte bVar3;
  ushort uVar4;
  short sVar5;
  int iVar6;
  int iVar7;
  undefined4 uVar8;
  short unaff_BX;
  int iVar9;
  undefined4 unaff_EBP;
  undefined2 *puVar10;
  short *psVar11;
  uint uVar12;
  undefined4 unaff_retaddr;
  int iStack00000004;
  ushort *in_stack_00000010;
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
  
  if (DAT_005633a8 == 0) {
    uVar4 = *in_stack_00000010;
    if ((uVar4 & 1) == 0) {
      if ((uVar4 & 0x400) != 0) {
        DAT_005e930a = DAT_005e930a - 0x40;
      }
      if ((*in_stack_00000010 & 0x800) != 0) {
        DAT_005e930a = DAT_005e930a + 0x40;
      }
      if ((*in_stack_00000010 & 0x100) != 0) {
        DAT_005e930c = DAT_005e930c - 0x40;
      }
      if ((*in_stack_00000010 & 0x200) != 0) {
        DAT_005e930c = DAT_005e930c + 0x40;
      }
      if ((*in_stack_00000010 & 0x1000) != 0) {
        DAT_005e9308 = DAT_005e9308 + 0x40;
      }
      if ((*in_stack_00000010 & 0x4000) != 0) {
        DAT_005e9308 = DAT_005e9308 - 0x40;
      }
      DAT_005e9308 = DAT_005e9308 & 0xfff;
      DAT_005e930a = DAT_005e930a & 0xfff;
      DAT_005e930c = DAT_005e930c & 0xfff;
    }
    else {
      if ((uVar4 & 0x400) != 0) {
        DAT_005e93a2 = DAT_005e93a2 - 8;
      }
      if ((*in_stack_00000010 & 0x800) != 0) {
        DAT_005e93a2 = DAT_005e93a2 + 8;
      }
      if ((*in_stack_00000010 & 0x100) != 0) {
        DAT_005e93a4 = DAT_005e93a4 - 8;
      }
      if ((*in_stack_00000010 & 0x200) != 0) {
        DAT_005e93a4 = DAT_005e93a4 + 8;
      }
      if ((*in_stack_00000010 & 0x1000) != 0) {
        DAT_005e93a0 = DAT_005e93a0 + 8;
      }
      if ((*in_stack_00000010 & 0x4000) != 0) {
        DAT_005e93a0 = DAT_005e93a0 - 8;
      }
      DAT_005e93a0 = DAT_005e93a0 & 0xff;
      DAT_005e93a2 = DAT_005e93a2 & 0xff;
      DAT_005e93a4 = DAT_005e93a4 & 0xff;
    }
  }
  if (DAT_00562a48 != 0) {
    DAT_00562a48 = 0;
    DAT_005e93a0 = 0x60;
    DAT_005e93a2 = 0x60;
    DAT_005e93a4 = 0x60;
    DAT_005e9308 = 0x200;
    DAT_005e930a = 0x380;
    DAT_005e930c = 0;
    _DAT_00604928 = 0x15e;
    _DAT_0060492c = 0;
    _DAT_0060492a = (*(byte *)(DAT_00573338 + 0x150) - 8 & 0xf) << 8;
    DAT_006008d0 = 0;
    DAT_006008d4 = 0;
    DAT_006008d8 = 0;
    DAT_006008f4 = 0;
    DAT_006008f8 = 0;
    DAT_006008fc = 0;
    iVar9 = 0;
    puVar10 = &DAT_00578454;
    do {
      puVar10[-2] = 0;
      *puVar10 = 0;
      iVar6 = Ordinal_451(iVar9 << 8);
      *(undefined4 *)(puVar10 + -10) = 0;
      *(int *)(puVar10 + -0xc) = iVar6 * -0x96;
      iVar6 = Ordinal_450(iVar9 << 8);
      *(byte *)(puVar10 + -0x18) = (byte)iVar9;
      puVar10[-6] = 0;
      puVar10[-5] = 0;
      puVar10[-4] = 0;
      *(undefined4 *)(puVar10 + 2) = 0xffffff;
      *(int *)(puVar10 + -8) = iVar6 * 0x96;
      puVar10[1] = 1;
      puVar10[-0x12] = 0;
      puVar10[-0x11] = 0;
      puVar10[-0xe] = 0;
      puVar10[-0xd] = 0;
      *(undefined1 *)(puVar10 + -0x15) = 0;
      *(byte *)((int)puVar10 + -0x29) = (byte)iVar9 & 3;
      *(undefined4 *)(puVar10 + -0x1a) = 0x800000;
      *(undefined1 *)(puVar10 + -0x16) = 0;
      iVar9 = iVar9 + 1;
      puVar10 = puVar10 + 0x4e;
    } while (iVar9 < 0x10);
    DAT_00562a50 = 1;
  }
  if (DAT_00562a4c != 0) goto switchD_0050726d_caseD_3;
  switch(DAT_00578df0) {
  case 0:
    cVar2 = *(char *)(DAT_00573338 + 0x150);
    if ((in_stack_00000010[3] & 0x80) != 0) {
      *(char *)(DAT_00573338 + 0x150) = cVar2 + -1;
    }
    if ((in_stack_00000010[3] & 0x20) != 0) {
      *(char *)(DAT_00573338 + 0x150) = *(char *)(DAT_00573338 + 0x150) + '\x01';
    }
    *(byte *)(DAT_00573338 + 0x150) = *(byte *)(DAT_00573338 + 0x150) & 0xf;
    if (cVar2 != *(char *)(DAT_00573338 + 0x150)) {
      FUN_0053db60();
    }
    uVar4 = in_stack_00000010[1];
    if ((uVar4 & 0x8000) == 0) {
      if ((uVar4 & 0x4000) != 0) {
        in_stack_00000010[1] = uVar4 & 0xbfff;
        FUN_0053dba0();
        DAT_00578df0 = 0x77;
      }
    }
    else {
      in_stack_00000010[1] = uVar4 & 0x7fff;
      FUN_0053db80();
      DAT_00578df0 = DAT_00578df0 + 1;
    }
    goto switchD_0050726d_caseD_3;
  case 1:
    if ((DAT_00560fef & 2) == 0) {
      in_stack_00000010[1] = in_stack_00000010[1] & 0x7fff;
      DAT_00578df0 = DAT_00578df0 + 1;
      goto switchD_0050726d_caseD_3;
    }
    break;
  case 2:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        FUN_005352e0(&DAT_00578420 + (uint)*(byte *)(DAT_00573338 + 0x150) * 0x27,&DAT_00572b48,0x27
                    );
        DAT_00578de8 = DAT_00578de8 + 1;
        DAT_00578df0 = 0x13;
        *(undefined1 *)(DAT_00573338 + 0x100) = 0;
        DAT_00578e00 = 0;
        goto switchD_0050726d_caseD_3;
      }
      goto LAB_0050743d;
    }
    bVar3 = *(byte *)((int)in_stack_00000010 + 3);
joined_r0x00507423:
    if ((bVar3 & 0x40) == 0) goto switchD_0050726d_caseD_3;
    DAT_00560763 = 0;
    DAT_00560fef = DAT_00560fef & 0xfb;
    goto LAB_00507438;
  default:
    goto switchD_0050726d_caseD_3;
  case 0x59:
    if ((DAT_00560fef & 4) != 0) {
      bVar3 = *(byte *)((int)in_stack_00000010 + 3);
      goto joined_r0x00507423;
    }
    if (DAT_005e909f == '\0') {
      DAT_00578de8 = -1;
      DAT_005de510 = 0;
      goto switchD_0050726d_caseD_3;
    }
    goto LAB_0050743d;
  case 0x77:
    if ((DAT_00560fef & 2) == 0) {
      in_stack_00000010[1] = in_stack_00000010[1] & 0x7fff;
      DAT_00578df0 = 0x59;
      goto switchD_0050726d_caseD_3;
    }
  }
  if ((*(byte *)((int)in_stack_00000010 + 3) & 0x40) != 0) {
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
LAB_00507438:
    FUN_0053dba0();
LAB_0050743d:
    DAT_00578df0 = 0;
  }
switchD_0050726d_caseD_3:
  uStack_12 = (*(byte *)(DAT_00573338 + 0x150) - 8) * 0x100 - _DAT_0060492a & 0xfff;
  if (uStack_12 < 0x800) {
    sVar5 = (short)uStack_12 >> 2;
  }
  else {
    sVar5 = -(short)(0x1000 - (short)uStack_12 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar5;
  FUN_0051c300(&DAT_00604928,&DAT_006008e0);
  Ordinal_376((int)(short)DAT_005e93a0,(int)(short)DAT_005e93a2,(int)(short)DAT_005e93a4);
  uVar12 = 0;
  psVar11 = &DAT_0057844a;
  iStack00000004 = 0;
  iVar9 = 0x2000;
  do {
    sVar5 = psVar11[4];
    if (uVar12 == *(byte *)(DAT_00573338 + 0x150)) {
      psVar11[4] = (short)(-(sVar5 + iStack00000004) >> 2) + sVar5;
      psVar11[-1] = (short)(0x1800 - psVar11[-1] >> 2) + psVar11[-1];
      *psVar11 = (short)(0x1800 - *psVar11 >> 2) + *psVar11;
      psVar11[1] = (short)(0x1800 - psVar11[1] >> 2) + psVar11[1];
      psVar11[-0xd] = (short)(-(int)psVar11[-0xd] >> 2) + psVar11[-0xd];
      psVar11[-0xc] = (short)(-(int)psVar11[-0xc] >> 2) + psVar11[-0xc];
    }
    else {
      psVar11[4] = (short)(iVar9 - sVar5 >> 2) + sVar5;
      psVar11[-1] = (short)(-(int)psVar11[-1] >> 2) + psVar11[-1];
      *psVar11 = (short)(-(int)*psVar11 >> 2) + *psVar11;
      psVar11[1] = (short)(-(int)psVar11[1] >> 2) + psVar11[1];
      psVar11[-0xd] = (short)(0x1000 - psVar11[-0xd] >> 2) + psVar11[-0xd];
      psVar11[-0xc] = (short)(0x1000 - psVar11[-0xc] >> 2) + psVar11[-0xc];
    }
    if (0xff < psVar11[-1]) {
      iVar6 = *(int *)(*(int *)(&DAT_005e9640 + (uint)*(byte *)(psVar11 + -0x13) * 4) +
                      psVar11[-9] * 4);
      psVar11[-8] = psVar11[-8] + 1;
      if ((int)(uint)*(ushort *)(iVar6 + 0x16) <= (int)psVar11[-8]) {
        psVar11[-8] = 0;
      }
      unaff_retaddr =
           FUN_0051d1f0(iStack_8,unaff_retaddr,
                        *(undefined4 *)
                         (*(int *)(&DAT_005e9640 + (uint)*(byte *)(psVar11 + -0x13) * 4) +
                         psVar11[-9] * 4),
                        *(undefined4 *)(&DAT_005e96a0 + (uint)*(byte *)(psVar11 + -0x13) * 4),
                        psVar11 + -7,(int)psVar11[-8],CONCAT22(psVar11[-9] >> 0xf,psVar11[-0x10]));
    }
    FUN_005352e0(psVar11 + -7,auStack_34,10);
    iStack_30 = iStack_30 + 0x3c000;
    iVar7 = FUN_0051f650(auStack_34,&stack0xffffffb0);
    iVar6 = iStack_8;
    if (iVar7 != 0) {
      sStack_38 = (short)uVar12;
      aiStack_48[0] = *(int *)(*(int *)(DAT_005e97c4 + 0x94) + 0x28);
      *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) =
           *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) + -1;
      *(char *)(aiStack_48[0] + 3 + sStack_38 * 8) =
           *(char *)(aiStack_48[0] + 3 + sStack_38 * 8) + -1;
      sStack_3a = psVar11[-0xc];
      sStack_40 = (short)unaff_EBP;
      sStack_3c = psVar11[-0xd];
      uStack_3e = (undefined2)((uint)unaff_EBP >> 0x10);
      aiStack_48[1] = 0x1000000;
      uVar8 = FUN_00521f20(iStack_8 + 0x50 + unaff_BX * 4,unaff_retaddr,aiStack_48,2,0);
      sStack_40 = sStack_40 + -1;
      unaff_retaddr = FUN_00521f20(iVar6 + 0x50 + unaff_BX * 4,uVar8,aiStack_48,2,0);
      *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) =
           *(char *)(aiStack_48[0] + 2 + sStack_38 * 8) + '\x01';
      pcVar1 = (char *)(aiStack_48[0] + 3 + sStack_38 * 8);
      *pcVar1 = *pcVar1 + '\x01';
    }
    iVar9 = iVar9 + -0x100;
    uVar12 = uVar12 + 1;
    iStack00000004 = iStack00000004 + 0x100;
    psVar11 = psVar11 + 0x4e;
  } while (0x1000 < iVar9);
  FUN_00544350(&DAT_005606f8,0,&DAT_00560ff0,0);
  uVar8 = FUN_005443a0(unaff_retaddr,&DAT_00560ff0);
  FUN_00544350(*(undefined4 *)
                (*(int *)(DAT_005e97c4 + 0x70) + 0xc + (uint)*(byte *)(DAT_00573338 + 0x150) * 4),0,
               &DAT_00560ff0,0);
  FUN_005443a0(uVar8,&DAT_00560ff0);
  return;
}


