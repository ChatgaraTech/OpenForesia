
undefined4
FUN_0051c120(undefined4 param_1,undefined4 param_2,int *param_3,byte *param_4,undefined4 param_5,
            undefined4 param_6,int param_7)

{
  uint *puVar1;
  uint uVar2;
  int *piVar3;
  byte bVar4;
  uint uVar5;
  int iVar6;
  byte *pbVar7;
  char local_1c;
  char local_1b;
  undefined2 local_1a;
  uint local_18;
  char *local_14;
  int local_10;
  int local_c;
  undefined4 local_4;
  
  FUN_0051be20(param_1,&param_2);
  piVar3 = param_3;
  puVar1 = (uint *)*param_3;
  uVar2 = *puVar1;
  uVar5 = uVar2 >> 0x14;
  param_3 = (int *)CONCAT31(param_3._1_3_,(char)uVar5);
  local_18 = puVar1[1];
  local_c = piVar3[2];
  local_10 = piVar3[1];
  local_14 = &local_1c;
  bVar4 = *param_4;
  local_4 = 0;
  local_1a = 0x707;
  iVar6 = param_7;
  pbVar7 = param_4;
  if (bVar4 != 0) {
    while (iVar6 != 0) {
      local_1c = (bVar4 & 0xf) * (char)uVar5 + (char)uVar2;
      local_1b = (bVar4 >> 4) * (byte)(uVar2 >> 0x1b) + (char)(uVar2 >> 8);
      param_2 = FUN_00521df0(param_1,param_2,&local_14,param_5,param_6);
      local_c = CONCAT22(local_c._2_2_,(short)local_c + ((ushort)(uVar2 >> 0x14) & 0xff));
      FUN_0051c0e0(*pbVar7);
      bVar4 = pbVar7[1];
      if (bVar4 == 0) break;
      uVar5 = (uint)param_3 & 0xff;
      iVar6 = iVar6 + -1;
      pbVar7 = pbVar7 + 1;
    }
  }
  param_2 = FUN_0051f9c0(param_1,param_2,&local_14);
  FUN_0051bdb0(param_1,&param_2);
  return param_2;
}


