/*
 * Function: FUN_0052a1e0
 * Entry:    0052a1e0
 */


void FUN_0052a1e0(int *param_1,int *param_2)

{
  int iVar1;
  
  if (DAT_005e9300 == &DAT_005e9880) {
    iVar1 = *(int *)MainMem_exref + 0x1c7000;
  }
  else {
    iVar1 = *(int *)MainMem_exref + 0x1c6000;
  }
  *param_1 = iVar1;
  *param_2 = iVar1 + 0xffc;
  Ordinal_688(*param_1,0x400);
  DAT_005e8310 = 0;
  if (DAT_005e9300 == &DAT_005e9880) {
    iVar1 = *(int *)MainMem_exref + 0x1e4000;
  }
  else {
    iVar1 = *(int *)MainMem_exref + 0x1c8000;
  }
  *param_1 = iVar1;
  DAT_005e9818 = 0x9c4;
  FUN_00533000(DAT_005e97c0);
  return;
}

