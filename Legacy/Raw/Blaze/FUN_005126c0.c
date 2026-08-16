
/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_005126c0(void)

{
  ushort uVar1;
  byte bVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  undefined *puVar6;
  int iVar7;
  uint uVar8;
  undefined4 uStack_3c;
  undefined4 uStack_38;
  undefined4 uStack_34;
  undefined4 uStack_30;
  undefined *puStack_2c;
  undefined *puStack_28;
  undefined *puStack_24;
  undefined *puStack_20;
  
  DAT_006049bc = &DAT_005d9e50;
  DAT_00604982 = 0xf;
  iVar7 = 0;
  puStack_20 = PTR_s_Blaze_Saving__0056315c;
  _DAT_00604960 = 0;
  DAT_00604981 = 0;
  puStack_24 = (undefined *)0x5126f1;
  FUN_00512900();
  puStack_20 = &DAT_00604980;
  puStack_24 = (undefined *)0x5126fe;
  FUN_005128e0();
  puVar6 = DAT_006049bc;
  puStack_20 = DAT_006049bc;
  puStack_24 = &DAT_00604940;
  puStack_28 = (undefined *)0x512712;
  iVar3 = Ordinal_18();
  if (iVar3 != 0) {
    do {
      *(int *)(puVar6 + 0x18) = *(int *)(puVar6 + 0x18) >> 0xd;
      DAT_00604981 = DAT_00604981 + 1;
      puStack_2c = (undefined *)0x512734;
      puStack_28 = puVar6 + 0x28;
      iVar3 = Ordinal_19();
      if (iVar3 == 0) break;
      puVar6 = puVar6 + 0x28;
    } while (DAT_00604981 < 0x20);
  }
  uVar8 = 0;
  puVar6 = DAT_006049bc;
  if (DAT_00604981 != 0) {
    do {
      puStack_2c = (undefined *)0x512764;
      puStack_28 = puVar6;
      FUN_00512900();
      puStack_28 = (undefined *)0x1;
      puStack_2c = &DAT_00604940;
      uStack_30 = 0x512773;
      iVar3 = Ordinal_21();
      if (iVar3 < 0) {
        return 0;
      }
      uStack_30 = 0;
      uStack_34 = 0;
      uStack_3c = 0x512787;
      uStack_38 = iVar3;
      Ordinal_23();
      uStack_3c = 0x100;
      iVar4 = Ordinal_24(iVar3,iVar7 + 0x1c0000 + *(int *)MainMem_exref);
      if (-1 < iVar4) {
        bVar2 = *(byte *)(iVar7 + 0x1c0002 + *(int *)MainMem_exref) & 0xf;
        (&DAT_0060498c)[uVar8] = bVar2;
        iVar5 = bVar2 - 1;
        if (iVar5 != 0) {
          iVar4 = Ordinal_24(iVar3,iVar7 + 0x1c0100 + *(int *)MainMem_exref,iVar5 * 0x80);
        }
      }
      Ordinal_22(iVar3);
      if (iVar4 < 0) {
        return 0;
      }
      iVar3 = iVar7 + 0x1c007e;
      iVar4 = 0x10;
      do {
        uVar1 = *(ushort *)(*(int *)MainMem_exref + iVar3);
        if (uVar1 != 0) {
          *(ushort *)(*(int *)MainMem_exref + iVar3) = uVar1 | 0x8000;
        }
        iVar3 = iVar3 + -2;
        iVar4 = iVar4 + -1;
      } while (iVar4 != 0);
      iVar3 = (int)(uVar8 + ((int)uVar8 >> 0x1f & 0xfU)) >> 4;
      uStack_30 = 0x10010;
      uStack_34 = CONCAT22((short)iVar3 + 0x1fe,(short)((uVar8 & 0xf) << 4));
      Ordinal_674(&uStack_34,iVar7 + 0x1c0060 + *(int *)MainMem_exref);
      uStack_3c = CONCAT22((short)(iVar3 << 6) + 0x20,(short)(uVar8 & 0xf) * 4 + 0x3c0);
      uStack_38 = CONCAT22((ushort)(byte)(&DAT_0060498c)[uVar8] << 4,4);
      Ordinal_674(&uStack_3c,iVar7 + 0x1c0080 + *(int *)MainMem_exref);
      uVar8 = uVar8 + 1;
      puVar6 = puVar6 + 0x28;
      iVar7 = iVar7 + 0x100;
    } while ((int)uVar8 < (int)(uint)DAT_00604981);
  }
  return 0;
}


