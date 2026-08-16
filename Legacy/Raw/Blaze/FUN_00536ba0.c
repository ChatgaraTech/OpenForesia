
/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00536ba0(void)

{
  code *pcVar1;
  char *pcVar2;
  byte *pbVar3;
  undefined4 *puVar4;
  undefined4 uVar5;
  int iVar6;
  char cVar7;
  uint uVar8;
  int iVar9;
  undefined2 extraout_var;
  char cVar10;
  int iVar11;
  int iVar12;
  undefined *local_34;
  undefined4 local_30;
  undefined1 local_20 [2];
  undefined2 uStack_1e;
  undefined4 uStack_1c;
  undefined4 uStack_18;
  uint uStack_14;
  undefined2 *puStack_8;
  
  iVar11 = 0;
  iVar6 = *(int *)MainMem_exref + 0xf8000;
  pcVar2 = (char *)(*(int *)MainMem_exref + 0xf02bd);
  local_30 = &DAT_005e93c4;
  local_34 = &DAT_005e9680;
  do {
    uVar8 = (uint)(byte)pcVar2[-0x16d];
    *(uint *)(local_30 + -4) = *(uint *)(local_30 + -4) & 0xefffdff9 | 0x81808a00;
    pcVar1 = MainMem_exref;
    local_30[7] = *pcVar2;
    iVar9 = 0;
    if (0x5e9680 < (int)local_34) {
      pbVar3 = (byte *)(*(int *)pcVar1 + 0xf0150);
      do {
        if (*pbVar3 == uVar8) {
          *local_30 = (char)iVar9;
          iVar9 = -1;
          break;
        }
        iVar9 = iVar9 + 1;
        pbVar3 = pbVar3 + 0x2000;
      } while (iVar9 < iVar11);
    }
    if (iVar9 != -1) {
      iVar9 = uVar8 * 8;
      cVar10 = (char)iVar11;
      iVar12 = *(int *)(DAT_005e97c4 + 0x84) + iVar9;
      uVar5 = *(undefined4 *)(iVar12 + 4);
      local_20[1] = (byte)((uint)uVar5 >> 8);
      uStack_1e._1_1_ = (undefined1)((uint)uVar5 >> 0x18);
      local_20[0] = (undefined1)uVar5;
      local_20 = (undefined1  [2])CONCAT11(local_20[1] + cVar10 * '\x04',local_20[0]);
      _local_20 = CONCAT22(CONCAT11(uStack_1e._1_1_,cVar10),local_20);
      FUN_00529b50(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x84) + iVar9),
                   *(int *)MainMem_exref + 0x1b0000);
      FUN_005322f0(*(int *)MainMem_exref + 0x1b0000,&DAT_005e8f80 + iVar11 * 4,local_20);
      uVar5 = *(undefined4 *)(iVar12 + 0x84);
      local_20[1] = (byte)((uint)uVar5 >> 8);
      cVar7 = local_20[1] + '\x10';
      uStack_1e._1_1_ = (undefined1)((uint)uVar5 >> 0x18);
      _local_20 = CONCAT13(uStack_1e._1_1_,0x30000);
      local_20[0] = (undefined1)uVar5;
      local_20 = (undefined1  [2])CONCAT11(cVar7,local_20[0]);
      FUN_00529b50(*(undefined4 *)(iVar12 + 0x80),*(int *)MainMem_exref + 0x1b0000);
      Ordinal_677(*(int *)MainMem_exref + 0x1b0000);
      Ordinal_662(&uStack_18);
      *uStack_18 = (short)(iVar11 << 6);
      uStack_18[1] = 0x1c0;
      uStack_18[2] = 0x40;
      uStack_18[3] = 1;
      Ordinal_674(uStack_18,uStack_14);
      *uStack_18 = (short)(((uint)local_30 & 0xf) * 0x40 + ((iVar11 << 6) >> 2));
      uStack_18[1] = (((ushort)local_30 & 0x10) + (ushort)local_30._2_1_ * 4) * 0x10;
      Ordinal_674(uStack_18,uStack_14);
      iVar12 = DAT_005e97c4;
      (&DAT_005e8f90)[iVar11 * 2] =
           (short)(CONCAT11((char)((ushort)puStack_8[1] >> 8),
                            (char)(CONCAT22(extraout_var,*puStack_8) >> 2)) & 0x1f0 | 0x2000) >> 4 |
           (ushort)((uStack_14 & 3) << 7);
      (&DAT_005e8f92)[iVar11 * 2] = (local_20[1] + 0x1c0) * 0x40;
      puVar4 = (undefined4 *)(*(int *)(iVar12 + 0x2c) + iVar9);
      uStack_1c = *puVar4;
      uVar5 = puVar4[1];
      uStack_18._2_1_ = (char)((uint)uVar5 >> 0x10);
      uStack_18._3_1_ = (undefined1)((uint)uVar5 >> 0x18);
      uStack_18 = (undefined2 *)
                  CONCAT22(CONCAT11(uStack_18._3_1_,uStack_18._2_1_ + cVar10),
                           CONCAT11(cVar10 << 6,(char)uVar5));
      uVar5 = FUN_00529b50(uStack_1c,iVar6);
      FUN_00532640(iVar6,&uStack_1c,&DAT_005e9800 + iVar11 * 4);
      iVar6 = FUN_00529b50(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x7c) + uVar8 * 4),uVar5);
      FUN_005326d0(uVar5,local_34);
      *local_30 = cVar10;
    }
    local_34 = local_34 + 4;
    iVar11 = iVar11 + 1;
    local_30 = local_30 + 0x9c;
    pcVar2 = pcVar2 + 0x2000;
    if (0x5e968f < (int)local_34) {
      _DAT_005de738 = iVar6;
      FUN_00535500();
      return;
    }
  } while( true );
}


