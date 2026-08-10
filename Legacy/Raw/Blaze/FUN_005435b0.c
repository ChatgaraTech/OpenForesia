
int __thiscall FUN_005435b0(int *param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
  int iVar1;
  int iVar2;
  int *unaff_ESI;
  undefined4 unaff_EDI;
  WCHAR *pWVar3;
  undefined4 *puVar4;
  undefined1 auStack_210 [8];
  WCHAR aWStack_208 [256];
  undefined4 uStack_8;
  
  iVar2 = *param_1;
  (**(code **)(iVar2 + 0x1c))();
  param_1[0x46] = 0xfd;
  iVar1 = lstrlenA((LPCSTR)(param_1 + 3));
  if (0 < iVar1) {
    MultiByteToWideChar(0,0,(LPCSTR)(param_1 + 3),-1,aWStack_208,0x104);
    iVar1 = FUN_00543320(auStack_210,param_3,param_4);
    if (-1 < iVar1) {
      pWVar3 = aWStack_208;
      puVar4 = (undefined4 *)0x0;
      iVar1 = (**(code **)(*(int *)param_1[1] + 0x34))((int *)param_1[1]);
      if (iVar1 != 0) {
        (**(code **)(*(int *)param_1[1] + 0x10))((int *)param_1[1],unaff_EDI);
        (**(code **)(*(int *)pWVar3 + 8))(pWVar3);
        return -0x7fffbffb;
      }
      iVar1 = FUN_00543260(unaff_EDI);
      if (iVar1 == 0) {
        iVar1 = FUN_00543300(&stack0xfffffde8);
        if (iVar1 < 0) {
          (**(code **)(*(int *)param_1[1] + 0x10))((int *)param_1[1],unaff_EDI);
          (**(code **)(*(int *)pWVar3 + 8))(pWVar3);
          return -0x7fffbffb;
        }
        iVar1 = FUN_00543450(unaff_EDI,unaff_ESI);
        if (iVar1 < 0) {
          (**(code **)(*(int *)param_1[1] + 0x10))((int *)param_1[1],unaff_EDI);
          (**(code **)(*(int *)pWVar3 + 8))(pWVar3);
          (**(code **)(*(int *)pWVar3 + 8))(pWVar3);
          return -0x7fffbffb;
        }
        (**(code **)(*unaff_ESI + 8))(unaff_ESI);
      }
      (**(code **)(iVar2 + 0x14))(uStack_8);
      param_1 = param_1 + 0x47;
      iVar1 = (**(code **)*puVar4)(puVar4,&DAT_0054e8e8);
      if (iVar1 < 0) {
        return iVar1;
      }
      iVar2 = (**(code **)(iVar2 + 0x20))(uStack_8);
      if (iVar2 < 0) {
        return iVar2;
      }
      (**(code **)(*param_1 + 8))(param_1);
      return 0;
    }
  }
  return -0x7fffbffb;
}

