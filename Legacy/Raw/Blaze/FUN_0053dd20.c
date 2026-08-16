
/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_0053dd20(int param_1)

{
  char cVar1;
  ushort uVar2;
  char *pcVar3;
  char *pcVar4;
  char *pcVar5;
  int iVar6;
  char *pcVar7;
  int iVar8;
  int iVar9;
  
  iVar6 = 0;
  pcVar4 = (char *)((*(byte *)(param_1 + 8) + 0x78) * 0x2000 + *(int *)MainMem_exref + 0x300);
  pcVar7 = pcVar4;
  while ((*pcVar7 == '\0' || ((*(ushort *)(pcVar7 + 0x12) & 0x8000) != 0))) {
    iVar6 = iVar6 + 1;
    pcVar7 = pcVar7 + 0x280;
    if (9 < iVar6) {
      DAT_005e909f = 0;
      return 1;
    }
  }
  *(ushort *)(pcVar7 + 0x12) = *(ushort *)(pcVar7 + 0x12) | 0x8000;
  pcVar3 = (char *)FUN_0051bd30(pcVar7);
  pcVar5 = *(char **)(*(int *)(DAT_005e97cc + 0x70) + 4);
  cVar1 = *pcVar3;
  while (cVar1 != '\0') {
    pcVar3 = pcVar3 + 1;
    *pcVar5 = cVar1;
    pcVar5 = pcVar5 + 1;
    cVar1 = *pcVar3;
  }
  *pcVar5 = '\0';
  _DAT_005e8ffc =
       CONCAT22((ushort)*(byte *)(param_1 + 8) * 0x40 + DAT_005e8fa0._2_2_,(short)DAT_005e8fa0);
  if (((*(uint *)(pcVar7 + 0x14) & 0x400) == 0) ||
     (DAT_005e909a = 1, *(uint *)(pcVar7 + 0x14) == 0x4000400)) {
    DAT_005e909a = 0;
  }
  uVar2 = *(ushort *)(pcVar7 + 0x88);
  DAT_005e909b = 0x15e < uVar2;
  if (1000 < uVar2) {
    DAT_005e909b = DAT_005e909b + '\x01';
  }
  if (5000 < uVar2) {
    DAT_005e909b = DAT_005e909b + '\x01';
  }
  DAT_005e909c = 3;
  DAT_005e909d = 1;
  DAT_005e909e = 0;
  pcVar5 = *(char **)(*(int *)(DAT_005e97cc + 0x94) + 4);
  *pcVar5 = (char)iVar6 * '\x18';
  pcVar5[1] = *(char *)(param_1 + 8) * '\x18';
  pcVar5[7] = (char)iVar6;
  DAT_005e909f = 1;
  iVar8 = (int)pcVar7 - (int)pcVar4;
  iVar6 = 10;
  do {
    if ((*pcVar4 != '\0') && ((*(ushort *)(pcVar4 + 0x12) & 0x8000) == 0)) {
      iVar9 = 0;
      pcVar7 = pcVar4;
      do {
        if (*pcVar7 != pcVar7[iVar8]) break;
        iVar9 = iVar9 + 1;
        pcVar7 = pcVar7 + 1;
      } while (iVar9 < 0x10);
      if (iVar9 == 0x10) {
        *(ushort *)(pcVar4 + 0x12) = *(ushort *)(pcVar4 + 0x12) | 0x8000;
      }
    }
    pcVar4 = pcVar4 + 0x280;
    iVar8 = iVar8 + -0x280;
    iVar6 = iVar6 + -1;
    if (iVar6 == 0) {
      return 0;
    }
  } while( true );
}


