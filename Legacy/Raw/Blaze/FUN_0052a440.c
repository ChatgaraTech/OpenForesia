/*
 * Function: FUN_0052a440
 * Entry:    0052a440
 * Body:     [[0052a440, 0052a650]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_0052a440(void)

{
  int *piVar1;
  int *piVar2;
  int *piVar3;
  uint uVar4;
  int iVar5;
  undefined *puStack_54;
  int *piStack_50;
  int iVar6;
  undefined1 *puStack_2c;
  undefined4 uStack_28;
  undefined2 local_24;
  undefined2 local_22;
  undefined2 local_20;
  undefined2 local_1e;
  undefined2 local_1c;
  undefined2 local_1a;
  undefined2 local_18;
  undefined2 local_16;
  
  local_24 = 0x380;
  local_22 = 0;
  local_20 = 0x80;
  local_1e = 0xe0;
  local_1c = 0;
  local_1a = 0x1ff;
  local_18 = 0x100;
  local_16 = 1;
  piStack_50 = (int *)0x52a482;
  FUN_0040a600();
  piVar2 = *(int **)(DAT_005e97c0 + 0x84);
  iVar6 = *piVar2;
  piVar3 = piVar2;
  while (iVar6 != 0) {
    piVar1 = piVar3 + 2;
    piVar3 = piVar3 + 2;
    iVar6 = *piVar1;
  }
  if (DAT_005de510 == 3) {
    uVar4 = DAT_005de528 & 0xff;
  }
  else {
    uVar4 = 0xb;
  }
  puStack_54 = (undefined *)piVar2[uVar4 * 2];
  piStack_50 = (int *)(*(int *)MainMem_exref + 0x1b0000);
  FUN_00529b50();
  piStack_50 = piVar2 + uVar4 * 2 + 1;
  puStack_54 = &DAT_005e8ffc;
  FUN_005322f0(*(int *)MainMem_exref + 0x1b0000);
  piStack_50 = (int *)0x0;
  puStack_54 = (undefined *)0x0;
  Ordinal_676(&local_24);
  Ordinal_676(&uStack_28,0,0);
  FUN_0052a1e0(&stack0xffffffb8,&piStack_50,&puStack_54);
  uStack_28 = 0x1000000;
  iVar5 = -1;
  iVar6 = 0x96;
  while ((iVar6 != 0 || (iVar5 != 0))) {
    puStack_2c = &stack0xffffffbc;
    local_24 = 0xa0;
    local_22 = 0x80;
    local_20 = 0x1400;
    local_1e = 0x1249;
    local_1c = 0;
    puStack_54 = (undefined *)FUN_00521f20(piStack_50,puStack_54,&puStack_2c,0,0);
    puStack_54 = (undefined *)FUN_0051f9c0(piStack_50,puStack_54,&puStack_2c);
    FUN_0052a280(&stack0xffffffb8,&piStack_50,&puStack_54,1,1);
    if (((*(ushort *)
           (&DAT_00604a20 +
           (uint)(byte)(&DAT_005e9188)
                       [(uint)(byte)(&DAT_005e93c8)[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4) &
         0x8008) == 0) && (iVar5 == -1)) {
      iVar5 = 0;
    }
    if ((*(ushort *)
          (&DAT_00604a20 +
          (uint)(byte)(&DAT_005e9188)
                      [(uint)(byte)(&DAT_005e93c8)[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4) &
        0x8008) == 0) {
      if (iVar5 == 1) {
        return;
      }
    }
    else if (iVar5 == 0) {
      iVar5 = 1;
      FUN_0053db80();
    }
    iVar6 = iVar6 + -1;
  }
  return;
}


