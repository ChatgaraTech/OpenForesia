
undefined4 FUN_00544e80(byte *param_1,uint param_2)

{
  uint *puVar1;
  int iVar2;
  
  iVar2 = param_2;
  puVar1 = *(uint **)(param_2 + 0x8b0);
  if ((puVar1 == (uint *)0x0) || ((*puVar1 & 0xfc0000) != 0x800000)) {
    param_2 = 0;
  }
  else {
    param_2 = (uint)*(byte *)((uint)(byte)puVar1[2] * 0x2000 + 0xf0153 + *(int *)MainMem_exref);
  }
  *(byte **)(iVar2 + 0x8b4 + (uint)*(byte *)(iVar2 + 0x15) * 4) = param_1 + 3;
  *(char *)(iVar2 + 0x15) = *(char *)(iVar2 + 0x15) + '\x01';
  return *(undefined4 *)
          (*(int *)(*(int *)(&DAT_005e97c0 + (uint)*param_1 * 4) + 0x70) +
          (*(ushort *)(param_1 + 1) + param_2) * 4);
}


