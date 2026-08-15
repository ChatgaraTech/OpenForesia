/*
 * Function: FUN_00520ae0
 * Entry:    00520ae0
 * Signature forcee a cinq parametres cdecl (voir
 * ForceDecompileParticlePass.java pour la justification).
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint * __cdecl FUN_00520ae0(uint *otEntry,uint *primCursor,int *particle,int arg4,int arg5)

{
  ushort uVar1;
  short sVar2;
  short sVar3;
  short sVar4;
  short sVar5;
  short sVar6;
  short sVar7;
  short sVar8;
  short sVar9;
  uint uVar10;
  uint uVar11;
  int iVar12;
  uint uVar13;
  uint uVar14;
  uint uVar15;
  uint uVar16;
  uint uVar17;
  int iVar18;
  int iVar19;
  uint uVar20;
  ushort uVar21;
  int iVar22;
  int iVar23;
  uint uVar24;
  uint *unaff_retaddr;
  int iStack_18;
  short sStack_10;
  short sStack_e;
  short sStack_c;
  int iStack_4;
  
  if (particle[8] == 0) {
    return primCursor;
  }
  uVar20 = *(uint *)(particle[8] + 4);
  if ((_DAT_00564a48 != 0) && ((uVar20 & 0x200) != 0)) {
    if (_DAT_00564a4c == 0) {
      return primCursor;
    }
    uVar20 = uVar20 & 0xfffffdff;
  }
  func_0x00545aca(0x6008e0);
  func_0x00545b90(0x6008e0);
  *(short *)Ordinal_803_exref = (short)(*particle + _DAT_006008d0 >> 0xc);
  *(short *)(Ordinal_803_exref + 2) = (short)(_DAT_006008d4 - particle[1] >> 0xc);
  *(short *)(Ordinal_803_exref + 4) = (short)(particle[2] + _DAT_006008d8 >> 0xc);
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
  uVar1 = *(ushort *)((int)particle + 0x1a);
  *(undefined2 *)Ordinal_802_exref = 0;
  *(undefined2 *)(Ordinal_802_exref + 2) = 0;
  *(undefined2 *)(Ordinal_802_exref + 4) = 0;
  *(undefined4 *)(Ordinal_814_exref + 0x14) = *(undefined4 *)Ordinal_801_exref;
  *(undefined4 *)(Ordinal_814_exref + 0x18) = *(undefined4 *)(Ordinal_801_exref + 4);
  *(undefined4 *)(Ordinal_814_exref + 0x1c) = *(undefined4 *)(Ordinal_801_exref + 8);
  iVar18 = (int)*(short *)(Ordinal_802_exref + 2);
  iVar22 = (int)*(short *)(Ordinal_802_exref + 4);
  iVar19 = (int)*(short *)Ordinal_802_exref;
  sVar2 = *(short *)Ordinal_814_exref;
  sVar3 = *(short *)(Ordinal_814_exref + 4);
  sVar4 = *(short *)(Ordinal_814_exref + 2);
  sVar5 = *(short *)(Ordinal_814_exref + 10);
  sVar6 = *(short *)(Ordinal_814_exref + 6);
  sVar7 = *(short *)(Ordinal_814_exref + 8);
  uVar21 = (short)(*(short *)(Ordinal_814_exref + 0xe) * iVar18 +
                   *(short *)(Ordinal_814_exref + 0xc) * iVar19 +
                   *(short *)(Ordinal_814_exref + 0x10) * iVar22 >> 0xc) +
           *(short *)(Ordinal_814_exref + 0x1c);
  sVar8 = *(short *)(Ordinal_814_exref + 0x14);
  sVar9 = *(short *)(Ordinal_814_exref + 0x18);
  *(int *)Ordinal_811_exref = (int)(short)uVar21;
  *(uint *)Ordinal_809_exref =
       (((int)(short)((short)(sVar5 * iVar22 + sVar6 * iVar19 + sVar7 * iVar18 >> 0xc) + sVar9) *
         (int)*(short *)(Ordinal_821_exref + (uint)uVar21 * 2) & 0xfffff000U) +
       *(int *)Ordinal_819_exref * 0x1000) * 0x10 +
       ((int)(short)((short)(iVar19 * sVar2 + sVar3 * iVar22 + sVar4 * iVar18 >> 0xc) + sVar8) *
        (int)*(short *)(Ordinal_821_exref + (uint)uVar21 * 2) >> 0xc) + *(int *)Ordinal_820_exref;
  *(int *)Ordinal_810_exref = *(int *)Ordinal_811_exref >> 2;
  iVar18 = *(int *)Ordinal_809_exref;
  sStack_10 = (short)iVar18;
  sStack_e = (short)((uint)iVar18 >> 0x10);
  sStack_c = (short)*(undefined4 *)Ordinal_811_exref;
  iVar19 = (int)(sStack_c >> 2);
  if (0x39f < iVar19 - 0x58U) {
    return unaff_retaddr;
  }
  if (((uVar1 & 1) != 0) &&
     ((0x16f < (ushort)(sStack_10 + 0x18U) || (0x11f < (ushort)(sStack_e + 0x18U))))) {
    return unaff_retaddr;
  }
  uVar14 = otEntry[7];
  uVar10 = *(uint *)(iStack_18 + (uint)(byte)otEntry[9] * 8);
  iVar22 = (uVar10 >> 0x10 & 0xff) - 1;
  iVar23 = (uVar10 >> 0x18) - 1;
  iVar12 = 0x190000;
  if (sStack_c >> 2 != 0) {
    iVar12 = (int)(0x190000 / (longlong)iVar19);
  }
  sVar2 = *(short *)((int)otEntry + 0xe);
  uVar13 = (uint)(short)(((uint)((short)otEntry[3] * iVar22) >> 0xc) * iVar12 >> 0xc);
  uVar11 = *(uint *)((uVar20 >> 0x10 & 0xff) * 4 + 0x5e8f00);
  *unaff_retaddr = *(uint *)(iStack_4 + -0x28 + iVar19 * 4) | 0x9000000;
  *(uint **)(iStack_4 + -0x28 + iVar19 * 4) = unaff_retaddr;
  unaff_retaddr[1] = ((uint)primCursor & 0xff | 0x2c | uVar20 >> 8) << 0x18 | uVar14 & 0x3ffffff;
  uVar14 = iVar22 + (uVar10 & 0xff);
  uVar24 = iVar23 * 0x100 + (uVar10 & 0xff00);
  uVar15 = iVar18 - (uVar13 >> 1) & 0xffff;
  uVar17 = (uint)(short)(((uint)(sVar2 * iVar23) >> 0xc) * iVar12 >> 0xc);
  uVar13 = uVar15 + uVar13 & 0xffff;
  uVar16 = ((int)sStack_e - (uVar17 >> 1)) * 0x10000;
  uVar17 = uVar17 * 0x10000 + uVar16;
  unaff_retaddr[2] = uVar16 | uVar15;
  unaff_retaddr[3] =
       (((ushort)((short)particle >> 2) & 0xffc0) + ((uint)particle & 0xff)) * 0x10000 +
       (uVar11 & 0xffff0000) + (uVar20 >> 8 & 0xff0000) | uVar10 & 0xffff;
  unaff_retaddr[4] = uVar13 | uVar16;
  unaff_retaddr[5] = ((uVar20 & 0xff) << 5 | uVar11) << 0x10 | uVar14 | uVar10 & 0xff00;
  unaff_retaddr[6] = uVar17 | uVar15;
  unaff_retaddr[7] = uVar24 | uVar10 & 0xff;
  unaff_retaddr[8] = uVar17 | uVar13;
  unaff_retaddr[9] = uVar24 | uVar14;
  return unaff_retaddr + 10;
}


