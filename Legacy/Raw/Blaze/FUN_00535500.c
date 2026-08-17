
/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00535500(void)

{
  int iVar1;
  short sVar2;
  int iVar3;
  int iVar4;
  int local_c;
  short local_8;
  short local_6;
  undefined2 local_4;
  undefined2 local_2;
  
  iVar4 = 0;
  iVar3 = *(int *)MainMem_exref + 0xf0440;
  do {
    sVar2 = 0;
    iVar1 = 0;
    local_c = iVar3;
    do {
      if (*(char *)(iVar3 + -0x140) != '\0') {
        local_6 = (short)iVar4 + 0x1f1;
        local_8 = sVar2 << 4;
        local_4 = 0x10;
        local_2 = 1;
        Ordinal_667(0);
        Ordinal_674(&local_c,iVar3);
        Ordinal_667(0);
        Ordinal_674(&stack0xffffffe8,iVar3 + 0x20);
      }
      iVar1 = iVar1 + 0x18;
      sVar2 = sVar2 + 1;
      iVar3 = iVar3 + 0x280;
    } while (iVar1 < 0xf0);
    iVar4 = iVar4 + 1;
    iVar3 = local_c + 0x2000;
  } while (iVar4 < 4);
  _DAT_005e8fa0 = 0x216;
  _DAT_005e8fa2 = 0x7c40;
  return;
}


