/*
 * Function: FUN_00508e00
 * Entry:    00508e00
 * Body:     [[00508e00, 0050946a]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00508e00(void)

{
  char cVar1;
  ushort uVar2;
  short sVar3;
  int iVar4;
  short unaff_BP;
  int iVar5;
  undefined4 *puVar6;
  undefined4 *puVar7;
  short *psVar8;
  undefined4 unaff_retaddr;
  uint uStack00000004;
  int in_stack_00000010;
  undefined1 auStack_34 [4];
  int iStack_30;
  short sStack_16;
  ushort uStack_12;
  int iStack_8;
  
  iVar5 = 0;
  if (DAT_00562a48 != 0) {
    DAT_00562a48 = 0;
    _DAT_00604928 = 0x15e;
    _DAT_0060492c = 0;
    uVar2 = *(byte *)(DAT_00573338 + 0x151) - 4 & 7;
    _DAT_0060492a = uVar2 * 0x200;
    _DAT_00572b78 = 0;
    DAT_00572b7a = uVar2 * -0x200 + 0x800;
    _DAT_00572b7c = 0;
    puVar6 = &DAT_00572e78;
    do {
      iVar4 = Ordinal_451(iVar5 << 9);
      *puVar6 = 0;
      puVar6[-1] = iVar4 * -0x78;
      iVar4 = Ordinal_450(iVar5 << 9);
      puVar6[6] = 0x1000000;
      *(undefined2 *)((int)puVar6 + 0x16) = 1;
      puVar6[1] = iVar4 * 0x78;
      *(undefined2 *)(puVar6 + 2) = 0;
      *(undefined2 *)((int)puVar6 + 10) = 0;
      *(undefined2 *)(puVar6 + 3) = 0;
      iVar5 = iVar5 + 1;
      puVar6 = puVar6 + 0x27;
    } while (iVar5 < 8);
    DAT_00562a50 = 3;
  }
  if (DAT_00562a4c == 0) {
    if (DAT_00578df0 == 0x2e) {
      uVar2 = *(ushort *)(in_stack_00000010 + 2);
      if ((uVar2 & 0x8000) == 0) {
        if ((uVar2 & 0x4000) == 0) {
          cVar1 = *(char *)(DAT_00573338 + 0x151);
          if ((*(byte *)(in_stack_00000010 + 6) & 0x80) != 0) {
            *(char *)(DAT_00573338 + 0x151) = cVar1 + -1;
          }
          if ((*(byte *)(in_stack_00000010 + 6) & 0x20) != 0) {
            *(char *)(DAT_00573338 + 0x151) = *(char *)(DAT_00573338 + 0x151) + '\x01';
          }
          *(byte *)(DAT_00573338 + 0x151) = *(byte *)(DAT_00573338 + 0x151) & 7;
          if (cVar1 != *(char *)(DAT_00573338 + 0x151)) {
            FUN_0053db60();
          }
        }
        else {
          sVar3 = *(byte *)(DAT_00573338 + 0x150) + 8;
          _DAT_0060492a = sVar3 * 0x100;
          DAT_00572b7a = sVar3 * -0x100 + 0x800;
          DAT_00578df0 = 0x5f;
          FUN_0053dba0();
          DAT_00578de8 = DAT_00578de8 + -1;
        }
      }
      else {
        *(ushort *)(in_stack_00000010 + 2) = uVar2 & 0x7fff;
        FUN_0053db80();
        DAT_00578df0 = DAT_00578df0 + 1;
      }
    }
    else if (DAT_00578df0 == 0x2f) {
      if ((DAT_00560fef & 2) == 0) {
        *(ushort *)(in_stack_00000010 + 2) = *(ushort *)(in_stack_00000010 + 2) & 0x7fff;
        DAT_00578df0 = DAT_00578df0 + 1;
      }
      else if ((*(byte *)(in_stack_00000010 + 3) & 0x40) != 0) {
        DAT_0056075b = 0;
        DAT_00560fef = DAT_00560fef & 0xfd;
        FUN_0053dba0();
        DAT_00578df0 = 0x2e;
      }
    }
    else if (DAT_00578df0 == 0x30) {
      if ((DAT_00560fef & 4) == 0) {
        if (DAT_005e909f == '\0') {
          DAT_00578df0 = 0x39;
          puVar6 = &DAT_00572c20;
          do {
            FUN_005352e0(&DAT_00572e58 + (uint)*(byte *)(DAT_00573338 + 0x151) * 0x9c,puVar6 + -0xf,
                         0x27);
            *puVar6 = *(undefined4 *)
                       (*(int *)(DAT_005e97c4 + 0x94) + 8 +
                       (uint)*(byte *)(DAT_00573338 + 0x151) * 4);
            uStack_12 = FUN_00547140();
            iVar5 = Ordinal_451((int)(short)uStack_12);
            puVar6[5] = iVar5 * 0x1e;
            iVar5 = FUN_00547140();
            puVar6[6] = iVar5 * 8 + -0x1fff8;
            iVar5 = Ordinal_450((int)sStack_16);
            puVar7 = puVar6 + 0x27;
            puVar6[7] = iVar5 * 0x1e;
            puVar6 = puVar7;
          } while ((int)puVar7 < 0x572e90);
          DAT_00578de8 = DAT_00578de8 + 1;
        }
        else {
          DAT_00578df0 = 0x2e;
        }
      }
      else if ((*(byte *)(in_stack_00000010 + 3) & 0x40) != 0) {
        DAT_00560763 = 0;
        DAT_00560fef = DAT_00560fef & 0xfb;
        FUN_0053dba0();
        DAT_00578df0 = 0x2e;
      }
    }
  }
  uStack_12 = (*(byte *)(DAT_00573338 + 0x151) - 4) * 0x200 - _DAT_0060492a & 0xfff;
  if (uStack_12 < 0x800) {
    sVar3 = (short)uStack_12 >> 2;
  }
  else {
    sVar3 = -(short)(0x1000 - (short)uStack_12 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar3;
  DAT_005e9308 = DAT_005e9308 + (short)(0x200 - DAT_005e9308 >> 2);
  DAT_005e930a = DAT_005e930a + (short)(0x380 - DAT_005e930a >> 2);
  DAT_005e930c = DAT_005e930c + (short)(-(int)DAT_005e930c >> 2);
  DAT_005e93a0 = DAT_005e93a0 +
                 (short)((int)*(short *)(&DAT_00562a58 + (uint)*(byte *)(DAT_00573338 + 0x151) * 8)
                         - (int)DAT_005e93a0 >> 2);
  DAT_005e93a2 = DAT_005e93a2 +
                 (short)((int)*(short *)(&DAT_00562a5a + (uint)*(byte *)(DAT_00573338 + 0x151) * 8)
                         - (int)DAT_005e93a2 >> 2);
  DAT_005e93a4 = DAT_005e93a4 +
                 (short)((int)*(short *)(&DAT_00562a5c + (uint)*(byte *)(DAT_00573338 + 0x151) * 8)
                         - (int)DAT_005e93a4 >> 2);
  FUN_0051c300(&DAT_00604928,&DAT_006008e0);
  Ordinal_376((int)DAT_005e93a0,(int)DAT_005e93a2,(int)DAT_005e93a4);
  uStack00000004 = 0;
  DAT_00572b7a = 0x800 - _DAT_0060492a;
  psVar8 = &DAT_00572e80;
  iVar5 = 8;
  do {
    FUN_005352e0(psVar8 + -6,auStack_34,10);
    iStack_30 = iStack_30 + 0x28000;
    sVar3 = *psVar8;
    if (uStack00000004 == *(byte *)(DAT_00573338 + 0x151)) {
      psVar8[8] = 0;
      psVar8[9] = 0x100;
      *psVar8 = (short)(0x1800 - sVar3 >> 2) + sVar3;
      psVar8[1] = (short)(0x1800 - psVar8[1] >> 2) + psVar8[1];
    }
    else {
      psVar8[8] = 0;
      psVar8[9] = 0x300;
      *psVar8 = (short)(0x1000 - sVar3 >> 2) + sVar3;
      psVar8[1] = (short)(0x1000 - psVar8[1] >> 2) + psVar8[1];
    }
    iVar4 = FUN_0051f650(auStack_34,&stack0xffffffb0);
    if (iVar4 != 0) {
      iVar4 = *(int *)(*(int *)(DAT_005e97c4 + 0x94) + iVar5);
      sVar3 = (short)((ulonglong)(DAT_005e9960 >> 1) % 6);
      *(char *)(iVar4 + 2 + sVar3 * 8) = *(char *)(iVar4 + 2 + sVar3 * 8) + -1;
      *(char *)(iVar4 + 3 + sVar3 * 8) = *(char *)(iVar4 + 3 + sVar3 * 8) + -1;
      unaff_retaddr = FUN_00521f20(iStack_8 + unaff_BP * 4,unaff_retaddr,&stack0xffffffb8,0,0);
      *(char *)(iVar4 + 2 + sVar3 * 8) = *(char *)(iVar4 + 2 + sVar3 * 8) + '\x01';
      *(char *)(iVar4 + 3 + sVar3 * 8) = *(char *)(iVar4 + 3 + sVar3 * 8) + '\x01';
    }
    iVar5 = iVar5 + 4;
    uStack00000004 = uStack00000004 + 1;
    psVar8 = psVar8 + 0x4e;
  } while (iVar5 < 0x28);
  FUN_00544350(*(undefined4 *)
                (*(int *)(DAT_005e97c4 + 0x70) + 0xc4 + (uint)*(byte *)(DAT_00573338 + 0x151) * 4),0
               ,&DAT_00562180,0);
  FUN_005443a0(unaff_retaddr,&DAT_00562180);
  return;
}


