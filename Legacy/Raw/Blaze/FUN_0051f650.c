/*
 * Function: FUN_0051f650
 * Entry:    0051f650
 * Body:     [[0051f650, 0051f93a]]
 */


undefined4 FUN_0051f650(void)

{
  short sVar1;
  short sVar2;
  short sVar3;
  short sVar4;
  short sVar5;
  short sVar6;
  short sVar7;
  short sVar8;
  int iVar9;
  int iVar10;
  ushort uVar11;
  int iVar12;
  short *unaff_retaddr;
  int *piStack_4;
  
  Ordinal_366(&DAT_006008e0);
  Ordinal_369(&DAT_006008e0);
  *(short *)Ordinal_803_exref = (short)(*piStack_4 + DAT_006008d0 >> 0xc);
  *(short *)(Ordinal_803_exref + 2) = (short)(DAT_006008d4 - piStack_4[1] >> 0xc);
  *(short *)(Ordinal_803_exref + 4) = (short)(piStack_4[2] + DAT_006008d8 >> 0xc);
  *(int *)Ordinal_801_exref =
       ((int)*(short *)(Ordinal_803_exref + 2) * (int)*(short *)(Ordinal_814_exref + 2) +
        (int)*(short *)(Ordinal_803_exref + 4) * (int)*(short *)(Ordinal_814_exref + 4) +
        (int)*(short *)Ordinal_814_exref * (int)*(short *)Ordinal_803_exref >> 0xc) +
       *(int *)(Ordinal_814_exref + 0x14);
  *(int *)(Ordinal_801_exref + 4) =
       ((int)*(short *)(Ordinal_803_exref + 2) * (int)*(short *)(Ordinal_814_exref + 8) +
        (int)*(short *)(Ordinal_803_exref + 4) * (int)*(short *)(Ordinal_814_exref + 10) +
        (int)*(short *)(Ordinal_814_exref + 6) * (int)*(short *)Ordinal_803_exref >> 0xc) +
       *(int *)(Ordinal_814_exref + 0x18);
  *(int *)(Ordinal_801_exref + 8) =
       ((int)*(short *)(Ordinal_803_exref + 2) * (int)*(short *)(Ordinal_814_exref + 0xe) +
        (int)*(short *)(Ordinal_803_exref + 4) * (int)*(short *)(Ordinal_814_exref + 0x10) +
        (int)*(short *)(Ordinal_814_exref + 0xc) * (int)*(short *)Ordinal_803_exref >> 0xc) +
       *(int *)(Ordinal_814_exref + 0x1c);
  *(undefined2 *)Ordinal_813_exref = *(undefined2 *)Ordinal_801_exref;
  *(undefined2 *)(Ordinal_813_exref + 2) = *(undefined2 *)(Ordinal_801_exref + 4);
  *(undefined2 *)(Ordinal_813_exref + 4) = *(undefined2 *)(Ordinal_801_exref + 8);
  *(undefined2 *)Ordinal_802_exref = 0;
  *(undefined2 *)(Ordinal_802_exref + 2) = 0;
  *(undefined2 *)(Ordinal_802_exref + 4) = 0;
  *(undefined4 *)(Ordinal_814_exref + 0x14) = *(undefined4 *)Ordinal_801_exref;
  *(undefined4 *)(Ordinal_814_exref + 0x18) = *(undefined4 *)(Ordinal_801_exref + 4);
  *(undefined4 *)(Ordinal_814_exref + 0x1c) = *(undefined4 *)(Ordinal_801_exref + 8);
  iVar12 = (int)*(short *)(Ordinal_802_exref + 2);
  iVar9 = (int)*(short *)(Ordinal_802_exref + 4);
  iVar10 = (int)*(short *)Ordinal_802_exref;
  sVar1 = *(short *)Ordinal_814_exref;
  sVar2 = *(short *)(Ordinal_814_exref + 4);
  sVar3 = *(short *)(Ordinal_814_exref + 2);
  sVar4 = *(short *)(Ordinal_814_exref + 6);
  sVar5 = *(short *)(Ordinal_814_exref + 10);
  sVar6 = *(short *)(Ordinal_814_exref + 8);
  uVar11 = (short)(*(short *)(Ordinal_814_exref + 0x10) * iVar9 +
                   *(short *)(Ordinal_814_exref + 0xe) * iVar12 +
                   *(short *)(Ordinal_814_exref + 0xc) * iVar10 >> 0xc) +
           *(short *)(Ordinal_814_exref + 0x1c);
  sVar7 = *(short *)(Ordinal_814_exref + 0x14);
  sVar8 = *(short *)(Ordinal_814_exref + 0x18);
  *(int *)Ordinal_811_exref = (int)(short)uVar11;
  *(uint *)Ordinal_809_exref =
       (((int)(short)((short)(sVar5 * iVar9 + sVar4 * iVar10 + sVar6 * iVar12 >> 0xc) + sVar8) *
         (int)*(short *)(Ordinal_821_exref + (uint)uVar11 * 2) & 0xfffff000U) +
       *(int *)Ordinal_819_exref * 0x1000) * 0x10 +
       ((int)(short)((short)(iVar10 * sVar1 + sVar2 * iVar9 + sVar3 * iVar12 >> 0xc) + sVar7) *
        (int)*(short *)(Ordinal_821_exref + (uint)uVar11 * 2) >> 0xc) + *(int *)Ordinal_820_exref;
  *(int *)Ordinal_810_exref = *(int *)Ordinal_811_exref >> 2;
  *(undefined4 *)unaff_retaddr = *(undefined4 *)Ordinal_809_exref;
  *(undefined4 *)(unaff_retaddr + 2) = *(undefined4 *)Ordinal_810_exref;
  if ((((ushort)(*unaff_retaddr + 0x18U) < 0x170) && ((ushort)(unaff_retaddr[1] + 0x18U) < 0x120))
     && ((int)unaff_retaddr[2] - 0x58U < 0x3a0)) {
    return 1;
  }
  return 0;
}


