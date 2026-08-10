
undefined4 FUN_00512dd0(int param_1,int param_2,byte param_3)

{
  int iVar1;
  undefined4 in_stack_0000001c;
  
  iVar1 = *(int *)(param_1 + 4);
  if (iVar1 == 0) {
    return 0x80004003;
  }
  if (param_2 != 0) {
    *(undefined4 *)(iVar1 + 0x40) = 0;
    FUN_00512c80();
    return 0;
  }
  if ((param_3 & 7) != 0) {
    *(undefined4 *)(iVar1 + 0x40) = in_stack_0000001c;
  }
  return 0;
}

