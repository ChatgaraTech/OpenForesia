/*
 * Function: FUN_00544150
 * Entry:    00544150
 */


void FUN_00544150(undefined4 param_1,uint param_2)

{
  byte *pbVar1;
  bool bVar2;
  uint *puVar3;
  uint uVar4;
  undefined2 *puVar5;
  uint uVar6;
  ushort uVar7;
  int iVar8;
  byte *pbVar9;
  int unaff_retaddr;
  
  if (DAT_005dffd0 != 1) {
    param_2 = param_2 & 0xffff;
    uVar4 = param_2 >> 8 | param_2 << 8;
    DAT_005dffd0 = 2;
    if (((uVar4 == (DAT_0056e9bc & 0xffff)) || (uVar4 == (DAT_0056e9b8 & 0xffff))) ||
       (uVar4 == (DAT_0056e9b4 & 0xffff))) {
      bVar2 = true;
    }
    else {
      bVar2 = false;
    }
    puVar5 = (undefined2 *)Ordinal_57(param_2);
    iVar8 = 0xf;
    pbVar9 = &DAT_005dffd8;
    do {
      param_1._1_1_ = (undefined1)((ushort)*puVar5 >> 8);
      param_1._0_1_ = (byte)*puVar5;
      uVar7 = CONCAT11((byte)param_1,param_1._1_1_);
      if (bVar2) {
        uVar7 = uVar7 >> 1;
      }
      param_1._0_1_ = (byte)(uVar7 >> 1) | (byte)uVar7 & 1;
      uVar4 = (uVar7 >> 1 & 0xfffc) >> 1;
      uVar4 = (uVar4 & 0xe000) >> 6 |
              (uVar4 & 0xf800) >> 5 & 0xff |
              (uVar4 & 0xfe00) >> 4 & 0x7f |
              (uVar4 & 0xff80) >> 3 & 0x3f |
              (uVar4 & 0xffe0) >> 2 & 0x1f |
              (uVar4 & 0xfff8) >> 1 & 0xf | uVar4 & 7 | (byte)param_1 & 3;
      param_1._0_1_ = (byte)uVar4;
      if (((iVar8 == 4) || (iVar8 == 8)) || (iVar8 == 0xc)) {
        pbVar1 = pbVar9 + -1;
        pbVar9 = pbVar9 + -1;
        param_1._0_1_ = *pbVar1 | (byte)param_1;
      }
      *pbVar9 = (byte)param_1;
      iVar8 = iVar8 + -1;
      pbVar9 = pbVar9 + 1;
      puVar5 = puVar5 + 1;
    } while (iVar8 != 0);
    uVar6 = 0xf;
    pbVar9 = &DAT_005dffd8;
    puVar3 = DAT_005e8304;
    do {
      DAT_005e8304 = puVar3;
      param_1 = (uint)*pbVar9;
      iVar8 = 8;
      do {
        uVar4 = uVar4 << 4;
        if ((param_1 & 1) != 0) {
          uVar4 = uVar4 | uVar6;
        }
        param_1 = param_1 >> 1;
        iVar8 = iVar8 + -1;
      } while (iVar8 != 0);
      *DAT_005e8304 = ~uVar4;
      uVar6 = uVar6 - 1;
      pbVar9 = pbVar9 + 1;
      puVar3 = DAT_005e8304 + 0x20;
    } while (uVar6 != 0);
    DAT_005e8304 = DAT_005e8304 + -0x1bf;
    *(char *)(unaff_retaddr + 0xe) = *(char *)(unaff_retaddr + 0xe) + '\b';
    DAT_005e8320 = DAT_005e8320 + 8;
    *(char *)(unaff_retaddr + 0x10) = *(char *)(unaff_retaddr + 0x10) + '\x01';
  }
  return;
}

