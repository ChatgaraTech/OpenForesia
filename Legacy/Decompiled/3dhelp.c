/* ===================================================== */
/* OpenForesia - Ghidra decompilation reference          */
/* Program: 3dhelp.dll */
/* ===================================================== */


/* ===================================================== */
/* FUN_0ffe1000 @ 0ffe1000 */
/* ===================================================== */

int FUN_0ffe1000(undefined4 param_1,int *param_2)

{
  char *pcVar1;
  int iVar2;
  int iVar3;
  int *piVar4;
  CHAR local_200 [256];
  CHAR local_100 [256];
  
  GetModuleFileNameA((HMODULE)0x0,local_200,0x100);
  pcVar1 = _strrchr(local_200,0x2e);
  if (pcVar1 != (char *)0x0) {
    *pcVar1 = '\0';
  }
  lstrcatA(local_200,&DAT_0ffe8018);
  GetPrivateProfileStringA
            (s_D3DDriver_0ffe8000,s_Driver_0ffe800c,&DAT_0ffe8014,local_100,0x100,local_200);
  iVar3 = 0;
  if (0 < *param_2) {
    piVar4 = param_2 + 1;
    do {
      iVar2 = lstrcmpiA(local_100,*(LPCSTR *)(*piVar4 + 0x14));
      if (iVar2 == 0) {
        return iVar3;
      }
      iVar3 = iVar3 + 1;
      piVar4 = piVar4 + 2;
    } while (iVar3 < *param_2);
  }
  WritePrivateProfileStringA
            (s_D3DDriver_0ffe8000,s_Driver_0ffe800c,*(LPCSTR *)(param_2[1] + 0x14),local_200);
  return 0;
}



/* ===================================================== */
/* FUN_0ffe10e0 @ 0ffe10e0 */
/* ===================================================== */

undefined4 FUN_0ffe10e0(int param_1,undefined4 param_2)

{
  undefined4 *puVar1;
  int iVar2;
  undefined4 local_28 [10];
  
  puVar1 = local_28;
  for (iVar2 = 10; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar1 = 0;
    puVar1 = puVar1 + 1;
  }
  local_28[1] = param_2;
  local_28[0] = 0x10;
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x24))(*(int **)(param_1 + 0x5c),&LAB_0ffe1130,local_28)
  ;
  return local_28[2];
}



/* ===================================================== */
/* FUN_0ffe11f0 @ 0ffe11f0 */
/* ===================================================== */

bool __thiscall FUN_0ffe11f0(int param_1,uint param_2)

{
  int *piVar1;
  int iVar2;
  int iVar3;
  undefined4 *puVar4;
  uint *puVar5;
  undefined4 *puVar6;
  uint local_28 [9];
  undefined4 *puStack_4;
  
  iVar3 = *(int *)(param_1 + 0x1c);
  puVar5 = local_28;
  for (iVar2 = 10; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar5 = 0;
    puVar5 = puVar5 + 1;
  }
  local_28[0] = param_2 & 0xff;
  local_28[1] = param_2 >> 8;
  piVar1 = *(int **)(iVar3 + 0x5c);
  (**(code **)(*piVar1 + 0x24))(piVar1,&LAB_0ffe1130,local_28);
  puVar4 = (undefined4 *)&stack0xffffffd4;
  puVar6 = puStack_4;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar6 = *puVar4;
    puVar4 = puVar4 + 1;
    puVar6 = puVar6 + 1;
  }
  return puStack_4[3] != 0;
}



/* ===================================================== */
/* FUN_0ffe1260 @ 0ffe1260 */
/* ===================================================== */

void __thiscall FUN_0ffe1260(int param_1,undefined4 param_2,uint *param_3)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  ushort uVar5;
  int iVar6;
  int iVar7;
  uint *puVar8;
  uint uVar9;
  uint uVar10;
  undefined4 *puVar11;
  uint *puVar12;
  uint uVar13;
  uint unaff_ESI;
  uint *unaff_EDI;
  uint *puVar14;
  uint uStack_ac;
  uint local_a8;
  uint uStack_a4;
  uint uStack_a0;
  int iStack_9c;
  int iStack_98;
  uint uStack_94;
  uint uStack_90;
  uint uStack_8c;
  int *piStack_88;
  uint uStack_84;
  uint local_80;
  int local_74;
  int iStack_70;
  undefined4 local_6c [4];
  uint *puStack_5c;
  uint uStack_2c;
  undefined4 uStack_28;
  undefined4 uStack_24;
  undefined4 uStack_20;
  uint uStack_1c;
  
  uVar9 = *param_3;
  uVar10 = param_3[3];
  local_a8 = param_3[2];
  puVar11 = local_6c;
  local_6c[0] = 0x6c;
  local_80 = uVar9;
  local_74 = param_1;
  iVar6 = (**(code **)(**(int **)(param_1 + 4) + 100))(*(int **)(param_1 + 4),0,puVar11,0x821,0);
  if (iVar6 == 0) {
    uVar13 = *(uint *)(uVar10 + 0xc) >> 3;
    iStack_98 = uVar13 * uVar9;
    puVar8 = puStack_5c;
    uStack_90 = uStack_2c >> 3;
    uStack_84 = uVar13;
    local_a8 = FUN_0ffe1530(*(undefined4 *)(uVar10 + 0x10),uStack_28,&uStack_ac);
    iVar6 = FUN_0ffe1530(*(undefined4 *)(uVar10 + 0x14),uStack_24,&uStack_a0);
    iVar7 = FUN_0ffe1530(*(undefined4 *)(uVar10 + 0x18),uStack_20,&uStack_a4);
    iStack_9c = FUN_0ffe1530(*(undefined4 *)(uVar10 + 0x1c),uStack_1c,&stack0xffffff48);
    if (((uStack_2c >> 3 == 2) && (((iStack_9c == 0 && iVar7 == 0) && iVar6 == 0) && local_a8 == 0))
       && ((unaff_ESI | uStack_a4 | uStack_a0 | uStack_ac) == 0xffff)) {
      if (puVar11 != (undefined4 *)0x0) {
        do {
          puVar12 = unaff_EDI;
          puVar14 = puVar8;
          for (uVar10 = (uVar9 & 0x7fffffff) >> 1; uVar10 != 0; uVar10 = uVar10 - 1) {
            *puVar14 = *puVar12;
            puVar12 = puVar12 + 1;
            puVar14 = puVar14 + 1;
          }
          for (uVar10 = uVar9 * 2 & 3; uVar10 != 0; uVar10 = uVar10 - 1) {
            *(char *)puVar14 = (char)*puVar12;
            puVar12 = (uint *)((int)puVar12 + 1);
            puVar14 = (uint *)((int)puVar14 + 1);
          }
          puVar8 = (uint *)((int)puVar8 + iStack_70);
          unaff_EDI = (uint *)((int)unaff_EDI + iStack_98);
          puVar11 = (undefined4 *)((int)puVar11 + -1);
        } while (puVar11 != (undefined4 *)0x0);
      }
    }
    else {
      for (; puVar11 != (undefined4 *)0x0; puVar11 = (undefined4 *)((int)puVar11 + -1)) {
        uVar10 = uVar9;
        if (uStack_90 == 2) {
          bVar1 = (byte)iStack_9c & 0x1f;
          uStack_8c = ~(unaff_ESI << bVar1 | unaff_ESI >> 0x20 - bVar1) & uStack_1c;
          puVar12 = unaff_EDI;
          puVar14 = puVar8;
          if (0 < (int)uVar9) {
            do {
              uVar10 = *puVar12;
              uVar5 = 0;
              if ((uVar10 & unaff_ESI) != 0) {
                uVar5 = (ushort)(uVar10 & unaff_ESI) | (ushort)uStack_8c;
              }
              bVar1 = (byte)local_a8 & 0x1f;
              bVar2 = (byte)iVar6 & 0x1f;
              bVar3 = (byte)iVar7 & 0x1f;
              puVar12 = (uint *)((int)puVar12 + uStack_84);
              *(ushort *)puVar14 =
                   (ushort)((uVar10 & uStack_ac) << bVar1) |
                   (ushort)((uVar10 & uStack_ac) >> 0x20 - bVar1) |
                   (ushort)((uVar10 & uStack_a0) << bVar2) |
                   (ushort)((uVar10 & uStack_a0) >> 0x20 - bVar2) |
                   (ushort)((uVar10 & uStack_a4) << bVar3) |
                   (ushort)((uVar10 & uStack_a4) >> 0x20 - bVar3) | uVar5;
              uVar9 = uVar9 - 1;
              uVar13 = uStack_84;
              uVar10 = uStack_94;
              puVar14 = (uint *)((int)puVar14 + 2);
            } while (uVar9 != 0);
          }
        }
        else {
          puVar12 = unaff_EDI;
          puVar14 = puVar8;
          if (0 < (int)uVar9) {
            do {
              uVar10 = *puVar12;
              bVar1 = (byte)local_a8 & 0x1f;
              bVar2 = (byte)iVar6 & 0x1f;
              bVar3 = (byte)iVar7 & 0x1f;
              bVar4 = (byte)iStack_9c & 0x1f;
              *puVar14 = (uVar10 & uStack_ac) << bVar1 | (uVar10 & uStack_ac) >> 0x20 - bVar1 |
                         (uVar10 & uStack_a0) << bVar2 | (uVar10 & uStack_a0) >> 0x20 - bVar2 |
                         (uVar10 & uStack_a4) << bVar3 | (uVar10 & uStack_a4) >> 0x20 - bVar3 |
                         (uVar10 & unaff_ESI) << bVar4 | (uVar10 & unaff_ESI) >> 0x20 - bVar4;
              uVar9 = uVar9 - 1;
              puVar12 = (uint *)((int)puVar12 + uVar13);
              uVar10 = uStack_94;
              puVar14 = (uint *)((int)puVar14 + uStack_90);
            } while (uVar9 != 0);
          }
        }
        puVar8 = (uint *)((int)puVar8 + iStack_70);
        unaff_EDI = (uint *)((int)unaff_EDI + iStack_98);
        uVar9 = uVar10;
      }
    }
    (**(code **)(*(int *)piStack_88[1] + 0x80))((int *)piStack_88[1],puStack_5c);
    (**(code **)(*piStack_88 + 0x14))();
  }
  return;
}



/* ===================================================== */
/* FUN_0ffe1530 @ 0ffe1530 */
/* ===================================================== */

int FUN_0ffe1530(uint param_1,uint param_2,uint *param_3)

{
  byte bVar1;
  uint uVar2;
  uint uVar3;
  int iVar4;
  
  uVar2 = GetBitCount(param_1);
  uVar3 = GetBitCount(param_2);
  if (uVar3 < uVar2) {
    uVar2 = (1 << ((byte)uVar3 & 0x1f)) - 1;
    if (uVar2 < (uVar2 ^ param_1)) {
      do {
        uVar2 = uVar2 * 2;
      } while (uVar2 < (uVar2 ^ param_1));
    }
    *param_3 = uVar2;
    param_1 = uVar2;
  }
  else {
    uVar2 = (1 << ((byte)uVar2 & 0x1f)) - 1;
    if (uVar2 < (uVar2 ^ param_2)) {
      do {
        uVar2 = uVar2 * 2;
      } while (uVar2 < (uVar2 ^ param_2));
    }
    *param_3 = param_1;
  }
  iVar4 = 0;
  while (((bVar1 = (byte)iVar4 & 0x1f, uVar2 = param_1 << bVar1 | param_1 >> 0x20 - bVar1,
          param_2 < uVar2 || ((uVar2 ^ param_2) != param_2 - uVar2)) || ((param_2 & uVar2) == 0))) {
    iVar4 = iVar4 + 1;
    if (0x1f < iVar4) {
      return 0;
    }
  }
  return iVar4;
}



/* ===================================================== */
/* FUN_0ffe1710 @ 0ffe1710 */
/* ===================================================== */

undefined4 * __fastcall FUN_0ffe1710(undefined4 *param_1)

{
  *param_1 = &PTR_LAB_0ffe60f0;
  FUN_0ffe32c0();
  FUN_0ffe54d0();
  *param_1 = &PTR_FUN_0ffe6000;
  return param_1;
}



/* ===================================================== */
/* FUN_0ffe1740 @ 0ffe1740 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe1740(undefined4 param_1,byte param_2)

{
  FUN_0ffe18b0();
  if ((param_2 & 1) != 0) {
    Free(param_1);
  }
  return param_1;
}



/* ===================================================== */
/* Create3DDevice @ 0ffe17e0 */
/* ===================================================== */

undefined4 Create3DDevice(void)

{
  int iVar1;
  undefined4 uVar2;
  
                    /* 0x17e0  1  Create3DDevice */
  iVar1 = Malloc(200);
  if (iVar1 != 0) {
    uVar2 = FUN_0ffe1710();
    return uVar2;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe18b0 @ 0ffe18b0 */
/* ===================================================== */

void __fastcall FUN_0ffe18b0(undefined4 *param_1)

{
  *param_1 = &PTR_FUN_0ffe6000;
  if (param_1[7] != 0) {
    FUN_0ffe26d0();
  }
  FUN_0ffe54e0();
  if (param_1 != (undefined4 *)0x0) {
    FUN_0ffe32f0();
    *param_1 = &PTR_LAB_0ffe60f0;
    return;
  }
  FUN_0ffe32f0();
  ppuRam00000000 = &PTR_LAB_0ffe60f0;
  return;
}



/* ===================================================== */
/* FUN_0ffe1970 @ 0ffe1970 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe1970(undefined4 param_1)

{
  int iVar1;
  undefined4 uVar2;
  
  iVar1 = Malloc(0x20);
  if (iVar1 != 0) {
    uVar2 = FUN_0ffe2af0(param_1);
    return uVar2;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe19f0 @ 0ffe19f0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe19f0(undefined4 param_1)

{
  int iVar1;
  undefined4 uVar2;
  
  iVar1 = Malloc(0x68);
  if (iVar1 != 0) {
    uVar2 = FUN_0ffe30f0(param_1);
    return uVar2;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe1b70 @ 0ffe1b70 */
/* ===================================================== */

void __thiscall FUN_0ffe1b70(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x21,param_2);
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x27,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe26d0 @ 0ffe26d0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe26d0(int param_1)

{
  int *piVar1;
  int iVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  
  if (*(undefined4 **)(param_1 + 0xac) != (undefined4 *)0x0) {
    (**(code **)**(undefined4 **)(param_1 + 0xac))(1);
  }
  if (*(undefined4 **)(param_1 + 0xb0) != (undefined4 *)0x0) {
    (**(code **)**(undefined4 **)(param_1 + 0xb0))(1);
  }
  iVar2 = DAT_0ffe80f8;
  if (DAT_0ffe80f8 != 0) {
    iVar5 = *(int *)(DAT_0ffe80f8 + 0x1f48);
    iVar4 = 0;
    if (0 < *(int *)(DAT_0ffe80f8 + 8000 + iVar5 * 4)) {
      do {
        piVar1 = *(int **)(iVar2 + (iVar4 + iVar5 * 1000) * 4);
        (**(code **)(*piVar1 + 8))(piVar1);
        iVar5 = *(int *)(iVar2 + 0x1f48);
        iVar4 = iVar4 + 1;
      } while (iVar4 < *(int *)(iVar2 + 8000 + iVar5 * 4));
    }
    *(undefined4 *)(iVar2 + 8000 + *(int *)(iVar2 + 0x1f48) * 4) = 0;
    uVar3 = (uint)(*(int *)(iVar2 + 0x1f48) == 0);
    *(uint *)(iVar2 + 0x1f48) = uVar3;
    iVar5 = 0;
    if (0 < *(int *)(iVar2 + 8000 + uVar3 * 4)) {
      do {
        piVar1 = *(int **)(iVar2 + (iVar5 + uVar3 * 1000) * 4);
        (**(code **)(*piVar1 + 8))(piVar1);
        uVar3 = *(uint *)(iVar2 + 0x1f48);
        iVar5 = iVar5 + 1;
      } while (iVar5 < *(int *)(iVar2 + 8000 + uVar3 * 4));
    }
    *(undefined4 *)(iVar2 + 8000 + *(int *)(iVar2 + 0x1f48) * 4) = 0;
    Free(iVar2);
  }
  DAT_0ffe80f8 = 0;
  FUN_0ffe34a0();
  SetWindowLongA(*(HWND *)(param_1 + 0x1c),-0x15,0);
  *(undefined4 *)(param_1 + 0x1c) = 0;
  FUN_0ffe53d0();
  return 1;
}



/* ===================================================== */
/* FUN_0ffe2880 @ 0ffe2880 */
/* ===================================================== */

void __fastcall FUN_0ffe2880(int *param_1)

{
  int *piVar1;
  int iVar2;
  int iVar3;
  
  iVar2 = (**(code **)(*(int *)param_1[0x17] + 0x28))((int *)param_1[0x17]);
  if ((iVar2 == -0x7789fe3e) &&
     (iVar2 = (**(code **)(*(int *)param_1[0x10] + 0x6c))((int *)param_1[0x10]), iVar2 == 0)) {
    iVar2 = *param_1;
    iVar3 = (**(code **)(iVar2 + 0x5c))(0,0);
    if (iVar3 == 0) {
      piVar1 = (int *)param_1[0x16];
      if (piVar1 != (int *)0x0) {
        iVar3 = (**(code **)(*piVar1 + 0x6c))(piVar1);
        if (iVar3 != 0) {
          return;
        }
        iVar2 = (**(code **)(iVar2 + 0x60))(0,0);
        if (iVar2 != 0) {
          return;
        }
      }
      (**(code **)(*(int *)param_1[0x17] + 0x28))((int *)param_1[0x17]);
    }
  }
  return;
}



/* ===================================================== */
/* FUN_0ffe28f0 @ 0ffe28f0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe28f0(int *param_1)

{
  int iVar1;
  int *piVar2;
  
  (**(code **)(*(int *)param_1[0x10] + 0x6c))((int *)param_1[0x10]);
  (**(code **)(*(int *)param_1[0x15] + 0x6c))((int *)param_1[0x15]);
  iVar1 = *param_1;
  (**(code **)(iVar1 + 0x5c))(0,0);
  piVar2 = (int *)param_1[0x16];
  if (piVar2 != (int *)0x0) {
    (**(code **)(*piVar2 + 0x6c))(piVar2);
    (**(code **)(iVar1 + 0x60))(0,0);
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe2af0 @ 0ffe2af0 */
/* ===================================================== */

void __thiscall FUN_0ffe2af0(undefined4 *param_1,undefined4 param_2)

{
  param_1[7] = param_2;
  *param_1 = &PTR_FUN_0ffe61f8;
  param_1[1] = 0;
  param_1[2] = 0;
  param_1[3] = 0;
  param_1[4] = 0;
  param_1[5] = 0;
  return;
}



/* ===================================================== */
/* FUN_0ffe2b20 @ 0ffe2b20 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe2b20(undefined4 param_1,byte param_2)

{
  FUN_0ffe2b70();
  if ((param_2 & 1) != 0) {
    Free(param_1);
  }
  return param_1;
}



/* ===================================================== */
/* FUN_0ffe2b70 @ 0ffe2b70 */
/* ===================================================== */

void __fastcall FUN_0ffe2b70(undefined4 *param_1)

{
  *param_1 = &PTR_FUN_0ffe61f8;
  FUN_0ffe3020();
  *param_1 = &PTR_LAB_0ffe6220;
  return;
}



/* ===================================================== */
/* FUN_0ffe2c40 @ 0ffe2c40 */
/* ===================================================== */

undefined4 FUN_0ffe2c40(int param_1)

{
  undefined4 *puVar1;
  int iVar2;
  undefined4 local_24 [9];
  
  puVar1 = local_24;
  for (iVar2 = 9; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar1 = 0;
    puVar1 = puVar1 + 1;
  }
  local_24[0] = 8;
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x24))(*(int **)(param_1 + 0x5c),&LAB_0ffe2bb0,local_24)
  ;
  return local_24[1];
}



/* ===================================================== */
/* FUN_0ffe2c80 @ 0ffe2c80 */
/* ===================================================== */

bool __thiscall FUN_0ffe2c80(int param_1,uint param_2)

{
  int *piVar1;
  int iVar2;
  undefined4 *puVar3;
  uint *puVar4;
  undefined4 *puVar5;
  uint local_24 [8];
  undefined4 *puStack_4;
  
  puVar4 = local_24;
  for (iVar2 = 9; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar4 = 0;
    puVar4 = puVar4 + 1;
  }
  if (param_2 == 0x18) {
    local_24[0] = 0x10;
  }
  else {
    local_24[0] = 8;
    if (3 < param_2) {
      local_24[0] = param_2;
    }
  }
  piVar1 = *(int **)(*(int *)(param_1 + 0x1c) + 0x5c);
  (**(code **)(*piVar1 + 0x24))(piVar1,&LAB_0ffe2bb0,local_24);
  puVar3 = (undefined4 *)&stack0xffffffd4;
  puVar5 = puStack_4;
  for (iVar2 = 8; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar5 = *puVar3;
    puVar3 = puVar3 + 1;
    puVar5 = puVar5 + 1;
  }
  return puStack_4[3] != 0;
}



/* ===================================================== */
/* FUN_0ffe2d10 @ 0ffe2d10 */
/* ===================================================== */

uint __thiscall FUN_0ffe2d10(int *param_1,int param_2)

{
  int *piVar1;
  uint uVar2;
  int iVar3;
  code *unaff_ESI;
  int *piVar4;
  int *piVar5;
  int aiStack_168 [17];
  uint uStack_124;
  undefined1 auStack_120 [16];
  undefined4 uStack_110;
  int iStack_104;
  int iStack_100;
  int aiStack_fc [61];
  undefined4 uStack_8;
  
  (**(code **)(*param_1 + 0x10))();
  piVar1 = aiStack_168;
  for (iVar3 = 0x1b; iVar3 != 0; iVar3 = iVar3 + -1) {
    *piVar1 = 0;
    piVar1 = piVar1 + 1;
  }
  aiStack_168[0] = 0x6c;
  if (param_1[5] == 0) {
    FUN_0ffe2c80(*(undefined2 *)(param_2 + 0xe),auStack_120);
  }
  else {
    FUN_0ffe11f0(*(undefined2 *)(param_2 + 0xe),auStack_120);
  }
  aiStack_168[3] = *(undefined4 *)(param_2 + 4);
  aiStack_168[2] = *(undefined4 *)(param_2 + 8);
  aiStack_168[1] = aiStack_168[1] | 0x1007;
  piVar1 = (int *)(**(code **)(*(int *)param_1[7] + 0x44))();
  piVar4 = aiStack_fc;
  for (iVar3 = 0x3f; iVar3 != 0; iVar3 = iVar3 + -1) {
    *piVar4 = *piVar1;
    piVar1 = piVar1 + 1;
    piVar4 = piVar4 + 1;
  }
  (**(code **)(*(int *)param_1[7] + 0x48))();
  piVar1 = param_1 + 2;
  iStack_100 = (-(uint)(aiStack_fc[2] != 0) & 0x4003800) + 0x1800;
  piVar4 = aiStack_168;
  uVar2 = (**(code **)(*DAT_0ffe80c0 + 0x18))(DAT_0ffe80c0,piVar4,piVar1,0);
  if (uVar2 != 0) {
LAB_0ffe2eca:
    (*unaff_ESI)();
    return uVar2;
  }
  if (iStack_104 == 0) {
    piVar5 = (int *)*piVar1;
    param_1[1] = (int)piVar5;
    (**(code **)(*piVar5 + 4))(piVar5);
  }
  else {
    uStack_110 = 0x1800;
    iVar3 = (**(code **)(*DAT_0ffe80c0 + 0x18))(DAT_0ffe80c0,&stack0xfffffe88,param_1 + 1,0);
    uVar2 = (uint)(iVar3 != 0);
    if (uVar2 != 0) goto LAB_0ffe2eca;
  }
  param_1[6] = 0;
  if ((uStack_124 < 9) && (param_1[5] == 0)) {
    iVar3 = DDPaletteFromBitmap(DAT_0ffe80c0,param_2);
    param_1[3] = iVar3;
    (**(code **)(*(int *)param_1[1] + 0x7c))((int *)param_1[1],iVar3);
    (**(code **)(*(int *)*piVar1 + 0x7c))((int *)*piVar1,param_1[3]);
    param_1[6] = 1;
  }
  else {
    param_1[3] = 0;
  }
  if (param_1[5] == 0) {
    iVar3 = FUN_0ffe2f20(param_2,uStack_8);
    if (iVar3 == 0) {
      (*unaff_ESI)();
      return 0x887602d5;
    }
  }
  else {
    iVar3 = FUN_0ffe1260(param_2,uStack_8);
    if (iVar3 == 0) {
      uVar2 = 0x887602d5;
      goto LAB_0ffe2eca;
    }
  }
  piVar5 = (int *)&DAT_0ffe6670;
  (*(code *)**(undefined4 **)*piVar1)((undefined4 *)*piVar1,&DAT_0ffe6670,&stack0xfffffe80);
  (**(code **)(*piVar4 + 0xc))(piVar4,*(undefined4 *)(param_1[7] + 0x5c),param_1 + 4);
  (**(code **)(*piVar5 + 8))(piVar5);
  return 0;
}



/* ===================================================== */
/* FUN_0ffe2f20 @ 0ffe2f20 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe2f20(BITMAPINFO *param_1)

{
  int *piVar1;
  int DestHeight;
  int iVar2;
  HDC unaff_EDI;
  undefined4 uVar3;
  void *unaff_retaddr;
  BITMAPINFO *local_4;
  
  piVar1 = (int *)(param_1->bmiHeader).biWidth;
  uVar3 = 0;
  local_4 = param_1;
  iVar2 = (**(code **)(*piVar1 + 0x44))(piVar1,&local_4);
  if (iVar2 == 0) {
    iVar2 = (local_4->bmiHeader).biWidth;
    DestHeight = (local_4->bmiHeader).biHeight;
    StretchDIBits(unaff_EDI,0,0,iVar2,DestHeight,0,0,iVar2,DestHeight,unaff_retaddr,local_4,0,
                  0xcc0020);
    piVar1 = (int *)(param_1->bmiHeader).biWidth;
    (**(code **)(*piVar1 + 0x68))(piVar1,unaff_EDI);
    uVar3 = (**(code **)((param_1->bmiHeader).biSize + 0x14))();
  }
  return uVar3;
}



/* ===================================================== */
/* FUN_0ffe3020 @ 0ffe3020 */
/* ===================================================== */

void __fastcall FUN_0ffe3020(int param_1)

{
  int *piVar1;
  int iVar2;
  
  piVar1 = *(int **)(param_1 + 4);
  if (piVar1 != (int *)0x0) {
    if (DAT_0ffe8084 == 0) {
      if (DAT_0ffe80f8 == 0) {
        DAT_0ffe80f8 = Malloc(0x1f4c);
        if (DAT_0ffe80f8 == 0) {
          DAT_0ffe80f8 = 0;
        }
        else {
          *(undefined4 *)(DAT_0ffe80f8 + 0x1f44) = 0;
          *(undefined4 *)(DAT_0ffe80f8 + 8000) = 0;
          *(undefined4 *)(DAT_0ffe80f8 + 0x1f48) = 0;
        }
      }
      iVar2 = DAT_0ffe80f8;
      *(undefined4 *)
       (DAT_0ffe80f8 +
       *(int *)(DAT_0ffe80f8 + 8000 + *(int *)(DAT_0ffe80f8 + 0x1f48) * 4) * 4 +
       *(int *)(DAT_0ffe80f8 + 0x1f48) * 4000) = *(undefined4 *)(param_1 + 4);
      *(int *)(iVar2 + 8000 + *(int *)(iVar2 + 0x1f48) * 4) =
           *(int *)(iVar2 + 8000 + *(int *)(iVar2 + 0x1f48) * 4) + 1;
    }
    else {
      (**(code **)(*piVar1 + 8))(piVar1);
    }
  }
  piVar1 = *(int **)(param_1 + 8);
  *(undefined4 *)(param_1 + 4) = 0;
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
  }
  piVar1 = *(int **)(param_1 + 0xc);
  *(undefined4 *)(param_1 + 8) = 0;
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
  }
  *(undefined4 *)(param_1 + 0xc) = 0;
  *(undefined4 *)(param_1 + 0x10) = 0;
  return;
}



/* ===================================================== */
/* FUN_0ffe30f0 @ 0ffe30f0 */
/* ===================================================== */

undefined4 * __thiscall FUN_0ffe30f0(undefined4 *param_1,undefined4 param_2)

{
  undefined4 *puVar1;
  int iVar2;
  undefined4 *puVar3;
  
  puVar1 = param_1 + 2;
  param_1[1] = param_2;
  *param_1 = &PTR_FUN_0ffe6248;
  param_1[0x16] = 0;
  puVar3 = puVar1;
  for (iVar2 = 0x14; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar3 = 0;
    puVar3 = puVar3 + 1;
  }
  param_1[7] = 0x3f800000;
  *puVar1 = 0x50;
  param_1[8] = 0x3f800000;
  param_1[0x15] = 0x10;
  param_1[6] = 0x3f800000;
  param_1[9] = 0x3f800000;
  puVar3 = param_1 + 0x19;
  param_1[10] = 0x3f800000;
  (**(code **)(*DAT_0ffe80c4 + 0x14))(DAT_0ffe80c4,puVar3,0);
  (**(code **)(*(int *)*puVar3 + 0xc))((int *)*puVar3,puVar1);
  (**(code **)(*(int *)*puVar3 + 0x14))
            ((int *)*puVar3,*(undefined4 *)(param_1[1] + 0x5c),param_1 + 0x18);
  param_1[0x17] = 1;
  FUN_0ffe3260();
  return param_1;
}



/* ===================================================== */
/* FUN_0ffe31c0 @ 0ffe31c0 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe31c0(undefined4 param_1,byte param_2)

{
  FUN_0ffe31e0();
  if ((param_2 & 1) != 0) {
    Free(param_1);
  }
  return param_1;
}



/* ===================================================== */
/* FUN_0ffe31e0 @ 0ffe31e0 */
/* ===================================================== */

void __fastcall FUN_0ffe31e0(undefined4 *param_1)

{
  int *piVar1;
  
  piVar1 = (int *)param_1[0x19];
  *param_1 = &PTR_FUN_0ffe6248;
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    param_1[0x19] = 0;
  }
  return;
}



/* ===================================================== */
/* FUN_0ffe3200 @ 0ffe3200 */
/* ===================================================== */

void __thiscall FUN_0ffe3200(int *param_1,int *param_2)

{
  int iVar1;
  int iVar2;
  
  (**(code **)(*param_1 + 4))();
  iVar1 = *param_2;
  iVar2 = (**(code **)(iVar1 + 100))(param_2,1,param_1[0x18]);
  if (iVar2 == 0) {
    (**(code **)(iVar1 + 0x5c))(param_2,1,param_1[0x14]);
  }
  return;
}



/* ===================================================== */
/* FUN_0ffe3230 @ 0ffe3230 */
/* ===================================================== */

void __thiscall FUN_0ffe3230(int *param_1,int *param_2)

{
  param_1[0x15] = 1;
  param_1[0x17] = 1;
  (**(code **)(*param_1 + 4))();
  (**(code **)(*param_2 + 0x20))(param_2,param_1[0x18]);
  return;
}



/* ===================================================== */
/* FUN_0ffe3260 @ 0ffe3260 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe3260(int param_1)

{
  undefined4 uVar1;
  
  if (*(int *)(param_1 + 0x5c) != 0) {
    *(undefined4 *)(param_1 + 0x5c) = 0;
    uVar1 = (**(code **)(**(int **)(param_1 + 100) + 0xc))(*(int **)(param_1 + 100),param_1 + 8);
    return uVar1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe3280 @ 0ffe3280 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe3280(int param_1,int *param_2)

{
  undefined4 uVar1;
  
  *(int **)(param_1 + 0x58) = param_2;
  if (param_2 != (int *)0x0) {
    uVar1 = (**(code **)(*param_2 + 4))();
    *(undefined4 *)(param_1 + 0x50) = uVar1;
    *(undefined4 *)(param_1 + 0x5c) = 1;
    return 0;
  }
  *(undefined4 *)(param_1 + 0x50) = 0;
  *(undefined4 *)(param_1 + 0x5c) = 1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe32c0 @ 0ffe32c0 */
/* ===================================================== */

undefined4 * __fastcall FUN_0ffe32c0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = param_1;
  for (iVar1 = 0x21; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = 0;
    puVar2 = puVar2 + 1;
  }
  param_1[2] = 0x84;
  param_1[3] = param_1[3] | 4;
  return param_1;
}



/* ===================================================== */
/* FUN_0ffe32f0 @ 0ffe32f0 */
/* ===================================================== */

void __fastcall FUN_0ffe32f0(int param_1)

{
  FUN_0ffe34a0();
  *(undefined4 *)(param_1 + 0x24) = 0;
  *(undefined4 *)(param_1 + 0x28) = 0;
  *(undefined4 *)(param_1 + 0x2c) = 0;
  *(undefined4 *)(param_1 + 0x18) = 0;
  *(undefined4 *)(param_1 + 0x80) = 0;
  return;
}



/* ===================================================== */
/* FUN_0ffe3310 @ 0ffe3310 */
/* ===================================================== */

int __thiscall
FUN_0ffe3310(int param_1,HWND param_2,undefined4 param_3,undefined4 param_4,undefined4 param_5,
            undefined4 param_6,undefined4 param_7,undefined4 param_8,int param_9)

{
  BOOL BVar1;
  uint uVar2;
  int iVar3;
  
  if ((param_2 == (HWND)0x0) || (BVar1 = IsWindow(param_2), BVar1 == 0)) {
    return -0x7789d8ee;
  }
  *(HWND *)(param_1 + 0x18) = param_2;
  if (param_9 == 0) {
    uVar2 = *(uint *)(param_1 + 0xc) & 0xfffffffb;
  }
  else {
    uVar2 = *(uint *)(param_1 + 0xc) | 4;
  }
  *(uint *)(param_1 + 0xc) = uVar2;
  iVar3 = FUN_0ffe41f0(param_3,param_4,param_5,param_6,param_7,param_8,1,param_1 + 0x24,
                       param_1 + 0x28,param_1 + 0x2c);
  if (iVar3 < 0) {
    return iVar3;
  }
  iVar3 = FUN_0ffe34e0();
  if (iVar3 < 0) {
    return iVar3;
  }
  iVar3 = FUN_0ffe35e0();
  if ((((-1 < iVar3) && (iVar3 = FUN_0ffe3620(), -1 < iVar3)) &&
      (iVar3 = FUN_0ffe3800(), -1 < iVar3)) && (iVar3 = FUN_0ffe3b30(), -1 < iVar3)) {
    SendMessageA(*(HWND *)(param_1 + 0x18),0xbd0,0,param_1);
    return 0;
  }
  FUN_0ffe34a0();
  return iVar3;
}



/* ===================================================== */
/* FUN_0ffe33f0 @ 0ffe33f0 */
/* ===================================================== */

int __thiscall
FUN_0ffe33f0(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,undefined4 param_5
            ,undefined4 param_6,undefined4 param_7,int param_8)

{
  uint uVar1;
  int iVar2;
  
  FUN_0ffe3d80();
  FUN_0ffe3880();
  FUN_0ffe37d0();
  if (param_8 == 0) {
    uVar1 = *(uint *)(param_1 + 0xc) & 0xfffffffb;
  }
  else {
    uVar1 = *(uint *)(param_1 + 0xc) | 4;
  }
  *(uint *)(param_1 + 0xc) = uVar1;
  iVar2 = FUN_0ffe41f0(param_2,param_3,param_4,param_5,param_6,param_7,1,param_1 + 0x24,
                       param_1 + 0x28,param_1 + 0x2c);
  if (iVar2 < 0) {
    return iVar2;
  }
  iVar2 = FUN_0ffe3620();
  if (((-1 < iVar2) && (iVar2 = FUN_0ffe3800(), -1 < iVar2)) && (iVar2 = FUN_0ffe3b30(), -1 < iVar2)
     ) {
    SendMessageA(*(HWND *)(param_1 + 0x18),0xbd0,0,param_1);
    return 0;
  }
  FUN_0ffe34a0();
  return iVar2;
}



/* ===================================================== */
/* FUN_0ffe34a0 @ 0ffe34a0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe34a0(int param_1)

{
  SendMessageA(*(HWND *)(param_1 + 0x18),0xbd1,0,param_1);
  FUN_0ffe3d80();
  FUN_0ffe3880();
  FUN_0ffe37d0();
  FUN_0ffe3590();
  return 0;
}



/* ===================================================== */
/* FUN_0ffe34e0 @ 0ffe34e0 */
/* ===================================================== */

int __fastcall FUN_0ffe34e0(int param_1)

{
  undefined4 *puVar1;
  int iVar2;
  undefined4 uVar3;
  
  if (*(int *)(param_1 + 0x24) == 0) {
    iVar2 = FUN_0ffe3fd0(0);
    *(int *)(param_1 + 0x24) = iVar2;
    if (iVar2 == 0) {
      return -0x7789d8ed;
    }
  }
  uVar3 = FUN_0ffe5390();
  iVar2 = DirectDrawCreate(uVar3,(undefined4 *)(param_1 + 0x30),0);
  if (-1 < iVar2) {
    puVar1 = *(undefined4 **)(param_1 + 0x30);
    iVar2 = (**(code **)*puVar1)(puVar1,&DAT_0ffe6290,(undefined4 *)(param_1 + 0x34));
    if (iVar2 < 0) {
      MessageBoxA(*(HWND *)(param_1 + 0x18),s_This_app_requires_DirectX_5_0_0ffe8088,
                  s_Error_0ffe80a8,0);
      FUN_0ffe3590();
      return iVar2;
    }
    puVar1 = *(undefined4 **)(param_1 + 0x34);
    iVar2 = (**(code **)*puVar1)(puVar1,&DAT_0ffe65b0,param_1 + 0x38);
    if (-1 < iVar2) {
      *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 1;
      return 0;
    }
  }
  FUN_0ffe3590();
  return iVar2;
}



/* ===================================================== */
/* FUN_0ffe3590 @ 0ffe3590 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe3590(int param_1)

{
  int *piVar1;
  
  piVar1 = *(int **)(param_1 + 0x38);
  *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) & 0xfffffffe;
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x38) = 0;
  }
  piVar1 = *(int **)(param_1 + 0x34);
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x34) = 0;
  }
  piVar1 = *(int **)(param_1 + 0x30);
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x30) = 0;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe35e0 @ 0ffe35e0 */
/* ===================================================== */

uint __fastcall FUN_0ffe35e0(int param_1)

{
  BOOL BVar1;
  uint uVar2;
  
  if (*(HWND *)(param_1 + 0x18) != (HWND)0x0) {
    BVar1 = IsWindow(*(HWND *)(param_1 + 0x18));
    if (BVar1 != 0) {
      uVar2 = (**(code **)(**(int **)(param_1 + 0x34) + 0x50))
                        (*(int **)(param_1 + 0x34),*(undefined4 *)(param_1 + 0x18),0x11);
      return uVar2 & (-1 < (int)uVar2) - 1;
    }
  }
  return 0x88762713;
}



/* ===================================================== */
/* FUN_0ffe3620 @ 0ffe3620 */
/* ===================================================== */

int __fastcall FUN_0ffe3620(int param_1)

{
  int iVar1;
  int iVar2;
  int unaff_EDI;
  int iVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  int local_10;
  int local_c;
  int local_8;
  int local_4;
  
  iVar3 = 0;
  if ((*(int *)(param_1 + 0x28) == 0) || (*(int *)(param_1 + 0x34) == 0)) {
    return -0x7789d8ed;
  }
  FUN_0ffe46f0(&local_10,&local_c,&local_8,&local_4);
  if (((local_10 == 0x140) && (local_c == 200)) && (local_8 == 8)) {
    iVar3 = 1;
  }
  iVar1 = (**(code **)(**(int **)(param_1 + 0x34) + 0x54))
                    (*(int **)(param_1 + 0x34),local_10,local_c,local_8);
  if (-1 < iVar1) {
    *(int *)(param_1 + 0x78) = local_4;
    *(undefined4 *)(param_1 + 0x70) = 0;
    *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 2;
    *(undefined4 *)(param_1 + 0x74) = 0;
    *(int *)(param_1 + 0x7c) = iVar3;
    return iVar1;
  }
  if ((local_4 != 0x280) || (uVar6 = 0x280, iVar3 != 0x1e0)) {
    iVar3 = 0x1e0;
    uVar6 = 0x280;
    iVar2 = FUN_0ffe3ff0(*(undefined4 *)(param_1 + 0x24),0x280,0x1e0,unaff_EDI,0,
                         *(undefined4 *)(param_1 + 0x2c));
    *(int *)(param_1 + 0x28) = iVar2;
    if (iVar2 != 0) {
      iVar1 = (**(code **)(**(int **)(param_1 + 0x34) + 0x54))
                        (*(int **)(param_1 + 0x34),uVar6,iVar3,unaff_EDI,0,0);
      if (-1 < iVar1) {
        *(undefined4 *)(param_1 + 0x78) = uVar6;
        *(undefined4 *)(param_1 + 0x70) = 0;
        *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 2;
        *(undefined4 *)(param_1 + 0x74) = 0;
        *(int *)(param_1 + 0x7c) = iVar3;
        return iVar1;
      }
    }
  }
  if (unaff_EDI != 0x10) {
    iVar2 = FUN_0ffe3ff0(*(undefined4 *)(param_1 + 0x24),uVar6,iVar3,0x10,0,
                         *(undefined4 *)(param_1 + 0x2c));
    *(int *)(param_1 + 0x28) = iVar2;
    if (iVar2 != 0) {
      uVar5 = 0;
      uVar4 = 0;
      iVar1 = (**(code **)(**(int **)(param_1 + 0x34) + 0x54))
                        (*(int **)(param_1 + 0x34),uVar6,iVar3,0x10);
      if (-1 < iVar1) {
        *(undefined4 *)(param_1 + 0x78) = uVar4;
        *(undefined4 *)(param_1 + 0x70) = 0;
        *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 2;
        *(undefined4 *)(param_1 + 0x74) = 0;
        *(undefined4 *)(param_1 + 0x7c) = uVar5;
        return iVar1;
      }
    }
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe37d0 @ 0ffe37d0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe37d0(int param_1)

{
  int *piVar1;
  
  piVar1 = *(int **)(param_1 + 0x34);
  *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) & 0xfffffffd;
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 0x50))(piVar1,*(undefined4 *)(param_1 + 0x18),8);
    (**(code **)(**(int **)(param_1 + 0x34) + 0x4c))(*(int **)(param_1 + 0x34));
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe3800 @ 0ffe3800 */
/* ===================================================== */

int __fastcall FUN_0ffe3800(int param_1)

{
  int *piVar1;
  int iVar2;
  undefined4 uStack_78;
  undefined4 uStack_74;
  
  if ((*(int *)(param_1 + 0x28) == 0) || (piVar1 = *(int **)(param_1 + 0x34), piVar1 == (int *)0x0))
  {
    iVar2 = -0x7789d8ed;
  }
  else {
    uStack_78 = *(undefined4 *)(param_1 + 0x18);
    uStack_74 = 0x11;
    (**(code **)(*piVar1 + 0x50))(piVar1);
    uStack_78 = 0x6c;
    uStack_74 = 0x21;
    iVar2 = (**(code **)(**(int **)(param_1 + 0x34) + 0x18))
                      (*(int **)(param_1 + 0x34),&uStack_78,param_1 + 0x3c,0);
    if (-1 < iVar2) {
      iVar2 = FUN_0ffe38b0();
      if (-1 < iVar2) {
        *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 4;
        return 0;
      }
    }
  }
  return iVar2;
}



/* ===================================================== */
/* FUN_0ffe3880 @ 0ffe3880 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe3880(int param_1)

{
  int *piVar1;
  
  *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) & 0xfffffffb;
  FUN_0ffe3af0();
  piVar1 = *(int **)(param_1 + 0x3c);
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x3c) = 0;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe38b0 @ 0ffe38b0 */
/* ===================================================== */

int __fastcall FUN_0ffe38b0(int param_1)

{
  int *piVar1;
  HDC hdc;
  uint uVar2;
  uint uVar3;
  int iVar4;
  undefined4 uVar5;
  undefined4 *puVar6;
  undefined4 *puVar7;
  undefined4 local_6c [16];
  undefined1 auStack_2c [4];
  uint uStack_28;
  
  FUN_0ffe3af0();
  if ((*(int *)(param_1 + 0x34) == 0) || (piVar1 = *(int **)(param_1 + 0x3c), piVar1 == (int *)0x0))
  {
    return -0x7789d8ed;
  }
  puVar6 = local_6c;
  for (iVar4 = 0x1b; iVar4 != 0; iVar4 = iVar4 + -1) {
    *puVar6 = 0;
    puVar6 = puVar6 + 1;
  }
  local_6c[0] = 0x6c;
  iVar4 = (**(code **)(*piVar1 + 0x58))(piVar1,local_6c);
  if (iVar4 < 0) {
    return iVar4;
  }
  iVar4 = FUN_0ffe43d0(auStack_2c);
  if (iVar4 == 0) {
    return 0;
  }
  hdc = GetDC((HWND)0x0);
  uVar2 = GetDeviceCaps(hdc,0x68);
  *(uint *)(param_1 + 0x44) = uVar2;
  if (uVar2 != 0) {
    if (0x100 < uVar2) {
      *(undefined4 *)(param_1 + 0x44) = 0x100;
    }
    iVar4 = Malloc(*(int *)(param_1 + 0x44) << 2);
    *(int *)(param_1 + 0x48) = iVar4;
    if (iVar4 == 0) {
      ReleaseDC((HWND)0x0,hdc);
      FUN_0ffe3af0();
      return -0x7789d8ec;
    }
    iVar4 = Malloc(*(int *)(param_1 + 0x44) << 2);
    *(int *)(param_1 + 0x4c) = iVar4;
    if (iVar4 == 0) {
      ReleaseDC((HWND)0x0,hdc);
      FUN_0ffe3af0();
      return -0x7789d8ec;
    }
    GetSystemPaletteEntries(hdc,0,*(UINT *)(param_1 + 0x44),*(LPPALETTEENTRY *)(param_1 + 0x48));
    puVar6 = *(undefined4 **)(param_1 + 0x48);
    puVar7 = *(undefined4 **)(param_1 + 0x4c);
    for (uVar2 = *(uint *)(param_1 + 0x44) & 0x3fffffff; uVar2 != 0; uVar2 = uVar2 - 1) {
      *puVar7 = *puVar6;
      puVar6 = puVar6 + 1;
      puVar7 = puVar7 + 1;
    }
    for (iVar4 = 0; iVar4 != 0; iVar4 = iVar4 + -1) {
      *(undefined1 *)puVar7 = *(undefined1 *)puVar6;
      puVar6 = (undefined4 *)((int)puVar6 + 1);
      puVar7 = (undefined4 *)((int)puVar7 + 1);
    }
  }
  ReleaseDC((HWND)0x0,hdc);
  if ((uStack_28 & 0x800) == 0) {
    if ((uStack_28 & 0x1000) == 0) {
      if ((uStack_28 & 8) == 0) {
        if ((uStack_28 & 0x20) == 0) {
          iVar4 = -0x7789d8ef;
          goto LAB_0ffe3aca;
        }
        uVar2 = 0;
        do {
          uVar3 = uVar2 + 4;
          *(undefined1 *)(uVar2 + 3 + *(int *)(param_1 + 0x4c)) = 0x40;
          *(undefined1 *)(uVar2 + 0x3db + *(int *)(param_1 + 0x4c)) = 0x40;
          uVar2 = uVar3;
        } while (uVar3 < 0x28);
        uVar2 = 0x28;
        do {
          uVar3 = uVar2 + 4;
          *(undefined1 *)(uVar2 + 3 + *(int *)(param_1 + 0x4c)) = 1;
          uVar2 = uVar3;
        } while (uVar3 < 0x3d8);
        uVar5 = 4;
      }
      else {
        *(undefined1 *)(*(int *)(param_1 + 0x4c) + 3) = 0x40;
        *(undefined1 *)(*(int *)(param_1 + 0x4c) + 0x3f) = 0x40;
        uVar2 = 4;
        do {
          uVar3 = uVar2 + 4;
          *(undefined1 *)(uVar2 + 3 + *(int *)(param_1 + 0x4c)) = 1;
          uVar2 = uVar3;
        } while (uVar3 < 0x3c);
        uVar5 = 1;
      }
    }
    else {
      uVar2 = 0;
      do {
        uVar3 = uVar2 + 4;
        *(undefined1 *)(uVar2 + 3 + *(int *)(param_1 + 0x4c)) = 1;
        uVar2 = uVar3;
      } while (uVar3 < 0x10);
      uVar5 = 0x200;
    }
  }
  else {
    uVar5 = 0x100;
    uVar2 = 0;
    do {
      uVar3 = uVar2 + 4;
      *(undefined1 *)(uVar2 + 3 + *(int *)(param_1 + 0x4c)) = 1;
      uVar2 = uVar3;
    } while (uVar3 < 8);
  }
  iVar4 = (**(code **)(**(int **)(param_1 + 0x34) + 0x14))
                    (*(int **)(param_1 + 0x34),uVar5,*(undefined4 *)(param_1 + 0x4c),
                     (undefined4 *)(param_1 + 0x40),0);
  if ((-1 < iVar4) &&
     (iVar4 = (**(code **)(**(int **)(param_1 + 0x3c) + 0x7c))
                        (*(int **)(param_1 + 0x3c),*(undefined4 *)(param_1 + 0x40)), -1 < iVar4)) {
    return 0;
  }
LAB_0ffe3aca:
  FUN_0ffe3af0();
  return iVar4;
}



/* ===================================================== */
/* FUN_0ffe3af0 @ 0ffe3af0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe3af0(int param_1)

{
  int *piVar1;
  
  piVar1 = *(int **)(param_1 + 0x40);
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x40) = 0;
  }
  if (*(int *)(param_1 + 0x4c) != 0) {
    Free(*(int *)(param_1 + 0x4c));
    *(undefined4 *)(param_1 + 0x4c) = 0;
  }
  if (*(int *)(param_1 + 0x48) != 0) {
    Free(*(int *)(param_1 + 0x48));
    *(undefined4 *)(param_1 + 0x48) = 0;
  }
  *(undefined4 *)(param_1 + 0x44) = 0;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe3b30 @ 0ffe3b30 */
/* ===================================================== */

uint __fastcall FUN_0ffe3b30(int param_1)

{
  int *piVar1;
  int *piVar2;
  BOOL BVar3;
  int iVar4;
  uint uVar5;
  uint uVar6;
  undefined4 *puVar7;
  undefined4 local_dc [2];
  undefined4 uStack_d4;
  uint uStack_84;
  undefined1 auStack_74 [4];
  undefined4 uStack_70;
  undefined4 local_6c [24];
  undefined4 uStack_c;
  
  if ((((*(HWND *)(param_1 + 0x18) == (HWND)0x0) ||
       (BVar3 = IsWindow(*(HWND *)(param_1 + 0x18)), BVar3 == 0)) || (*(int *)(param_1 + 0x2c) == 0)
      ) || (((*(int *)(param_1 + 0x28) == 0 || (*(int *)(param_1 + 0x34) == 0)) ||
            ((*(int *)(param_1 + 0x38) == 0 || (*(int *)(param_1 + 0x3c) == 0)))))) {
    return 0x88762713;
  }
  iVar4 = FUN_0ffe47f0();
  if (iVar4 == 0) {
    if (*(int *)(param_1 + 0x28) == 0) {
      return 0x88762713;
    }
    if (*(int *)(param_1 + 0x34) == 0) {
      return 0x88762713;
    }
    local_6c[0] = 0x6c;
    (**(code **)(**(int **)(param_1 + 0x3c) + 0x58))(*(int **)(param_1 + 0x3c),local_6c);
    uStack_70 = 7;
    uStack_c = 0x2840;
    uVar5 = (**(code **)(**(int **)(param_1 + 0x34) + 0x18))
                      (*(int **)(param_1 + 0x34),auStack_74,param_1 + 0x50,0);
    if ((int)uVar5 < 0) {
      return uVar5;
    }
  }
  else {
    local_dc[0] = 4;
    uVar5 = (**(code **)(**(int **)(param_1 + 0x3c) + 0x30))
                      (*(int **)(param_1 + 0x3c),local_dc,param_1 + 0x50);
    if ((int)uVar5 < 0) {
      return uVar5;
    }
  }
  iVar4 = FUN_0ffe47f0();
  if (iVar4 == 0) {
    iVar4 = *(int *)(param_1 + 0x2c) + 0x118;
    uVar5 = 0x800;
  }
  else {
    iVar4 = *(int *)(param_1 + 0x2c) + 0x1c;
    uVar5 = 0x4000;
  }
  if ((((*(byte *)(param_1 + 0xc) & 4) != 0) && (iVar4 != 0)) && (*(int *)(iVar4 + 0xa0) != 0)) {
    uVar6 = (**(code **)(**(int **)(param_1 + 0x3c) + 0x58))
                      (*(int **)(param_1 + 0x3c),&stack0xffffff1c);
    if ((int)uVar6 < 0) {
      return uVar6;
    }
    uStack_d4 = FUN_0ffe42d0(*(undefined4 *)(iVar4 + 0xa0));
    puVar7 = (undefined4 *)&stack0xffffff14;
    for (iVar4 = 0x1b; iVar4 != 0; iVar4 = iVar4 + -1) {
      *puVar7 = 0;
      puVar7 = puVar7 + 1;
    }
    piVar1 = (int *)(param_1 + 0x54);
    uStack_84 = uVar5 | 0x20000;
    iVar4 = (**(code **)(**(int **)(param_1 + 0x34) + 0x18))
                      (*(int **)(param_1 + 0x34),&stack0xffffff14,piVar1,0);
    if (((-1 < iVar4) &&
        (iVar4 = (**(code **)(**(int **)(param_1 + 0x50) + 0xc))(*(int **)(param_1 + 0x50),*piVar1),
        iVar4 < 0)) && (piVar2 = (int *)*piVar1, piVar2 != (int *)0x0)) {
      (**(code **)(*piVar2 + 8))(piVar2);
      *piVar1 = 0;
    }
  }
  uVar5 = (**(code **)(**(int **)(param_1 + 0x38) + 0x20))
                    (*(int **)(param_1 + 0x38),*(int *)(param_1 + 0x2c) + 4,
                     *(undefined4 *)(param_1 + 0x50),(undefined4 *)(param_1 + 0x58));
  if (-1 < (int)uVar5) {
    FUN_0ffe4970(*(undefined4 *)(param_1 + 0x58));
    *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 8;
    uVar5 = FUN_0ffe3df0();
    return uVar5 & (-1 < (int)uVar5) - 1;
  }
  return uVar5;
}



/* ===================================================== */
/* FUN_0ffe3d80 @ 0ffe3d80 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe3d80(int param_1)

{
  int *piVar1;
  
  FUN_0ffe3e70();
  piVar1 = *(int **)(param_1 + 0x58);
  *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) & 0xfffffff7;
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x58) = 0;
  }
  if (*(int *)(param_1 + 0x54) != 0) {
    piVar1 = *(int **)(param_1 + 0x50);
    if (piVar1 != (int *)0x0) {
      (**(code **)(*piVar1 + 0x20))(piVar1,0,*(int *)(param_1 + 0x54));
    }
    (**(code **)(**(int **)(param_1 + 0x54) + 8))(*(int **)(param_1 + 0x54));
    *(undefined4 *)(param_1 + 0x54) = 0;
  }
  piVar1 = *(int **)(param_1 + 0x50);
  if (piVar1 != (int *)0x0) {
    (**(code **)(*piVar1 + 8))(piVar1);
    *(undefined4 *)(param_1 + 0x50) = 0;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe3df0 @ 0ffe3df0 */
/* ===================================================== */

int __fastcall FUN_0ffe3df0(int param_1)

{
  undefined4 *puVar1;
  int *piVar2;
  int iVar3;
  
  piVar2 = *(int **)(param_1 + 0x38);
  if ((piVar2 == (int *)0x0) || (*(int *)(param_1 + 0x58) == 0)) {
    iVar3 = -0x7789d8ed;
  }
  else {
    puVar1 = (undefined4 *)(param_1 + 0x5c);
    iVar3 = (**(code **)(*piVar2 + 0x18))(piVar2,puVar1,0);
    if (-1 < iVar3) {
      iVar3 = (**(code **)(**(int **)(param_1 + 0x58) + 0x18))(*(int **)(param_1 + 0x58),*puVar1);
      if (-1 < iVar3) {
        iVar3 = FUN_0ffe3eb0();
        if (-1 < iVar3) {
          *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 0x10;
          return 0;
        }
        (**(code **)(**(int **)(param_1 + 0x58) + 0x1c))(*(int **)(param_1 + 0x58),*puVar1);
      }
      (**(code **)(*(int *)*puVar1 + 8))((int *)*puVar1);
      *puVar1 = 0;
      return iVar3;
    }
  }
  return iVar3;
}



/* ===================================================== */
/* FUN_0ffe3e70 @ 0ffe3e70 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe3e70(int param_1)

{
  *(uint *)(param_1 + 0x14) = *(uint *)(param_1 + 0x14) | 0x10;
  if (*(int *)(param_1 + 0x5c) != 0) {
    (**(code **)(**(int **)(param_1 + 0x58) + 0x1c))
              (*(int **)(param_1 + 0x58),*(int *)(param_1 + 0x5c));
    (**(code **)(**(int **)(param_1 + 0x5c) + 8))(*(int **)(param_1 + 0x5c));
    *(undefined4 *)(param_1 + 0x5c) = 0;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe3eb0 @ 0ffe3eb0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint __fastcall FUN_0ffe3eb0(int param_1)

{
  float fVar1;
  uint uVar2;
  int iVar3;
  uint uVar4;
  int iVar5;
  int *piVar6;
  int local_2c [4];
  uint local_1c;
  undefined4 local_18;
  float local_14;
  undefined4 local_10;
  float local_c;
  undefined4 local_8;
  undefined4 local_4;
  
  if ((*(int *)(param_1 + 0x58) == 0) || (*(int *)(param_1 + 0x5c) == 0)) {
    uVar2 = 0x88762713;
  }
  else {
    uVar2 = *(int *)(param_1 + 0x78) - *(int *)(param_1 + 0x70);
    uVar4 = (int)uVar2 >> 0x1f;
    iVar5 = (uVar2 ^ uVar4) - uVar4;
    uVar2 = *(int *)(param_1 + 0x7c) - *(int *)(param_1 + 0x74);
    uVar4 = (int)uVar2 >> 0x1f;
    uVar4 = (uVar2 ^ uVar4) - uVar4;
    fVar1 = _DAT_0ffe6250;
    if (iVar5 != 0) {
      fVar1 = (float)uVar4 / (float)iVar5;
    }
    piVar6 = local_2c;
    for (iVar3 = 0xb; iVar3 != 0; iVar3 = iVar3 + -1) {
      *piVar6 = 0;
      piVar6 = piVar6 + 1;
    }
    local_c = fVar1 + fVar1;
    local_2c[0] = 0x2c;
    local_2c[1] = 0;
    local_2c[2] = 0;
    local_18 = 0xbf800000;
    local_10 = 0x40000000;
    local_8 = 0;
    local_4 = 0x3f800000;
    local_2c[3] = iVar5;
    local_1c = uVar4;
    local_14 = fVar1;
    uVar2 = (**(code **)(**(int **)(param_1 + 0x5c) + 0x44))(*(int **)(param_1 + 0x5c),local_2c);
    if (-1 < (int)uVar2) {
      uVar2 = (**(code **)(**(int **)(param_1 + 0x58) + 0x34))
                        (*(int **)(param_1 + 0x58),*(undefined4 *)(param_1 + 0x5c));
      return uVar2 & (-1 < (int)uVar2) - 1;
    }
  }
  return uVar2;
}



/* ===================================================== */
/* entry @ 0ffe3fb0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 entry(undefined4 param_1,int param_2)

{
  if (param_2 == 1) {
    _DAT_0ffe80fc = param_1;
  }
  return 1;
}



/* ===================================================== */
/* FUN_0ffe3fd0 @ 0ffe3fd0 */
/* ===================================================== */

int FUN_0ffe3fd0(int param_1)

{
  int iVar1;
  
  iVar1 = FUN_0ffe5550(param_1,&param_1,0);
  if (iVar1 == 0) {
    iVar1 = param_1;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe3ff0 @ 0ffe3ff0 */
/* ===================================================== */

int FUN_0ffe3ff0(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,int param_5,
                int param_6)

{
  int iVar1;
  
  if (param_1 == 0) {
    return 0;
  }
  if (param_6 == 0) {
    iVar1 = FUN_0ffe4ec0(param_2,param_3,param_4,param_5,&param_5,0);
  }
  else {
    iVar1 = FUN_0ffe52b0(param_2,param_3,param_4,param_5,param_6,&param_5,0);
  }
  if (iVar1 == 0) {
    iVar1 = param_5;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe4060 @ 0ffe4060 */
/* ===================================================== */

undefined4 FUN_0ffe4060(int param_1,undefined4 param_2,int *param_3,int *param_4)

{
  HWND hWnd;
  HDC hdc;
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int local_4;
  
  if (((param_1 != 0) && (param_3 != (int *)0x0)) && (param_4 != (int *)0x0)) {
    hWnd = GetDesktopWindow();
    hdc = GetDC(hWnd);
    iVar1 = GetDeviceCaps(hdc,8);
    iVar2 = GetDeviceCaps(hdc,10);
    iVar3 = GetDeviceCaps(hdc,0xe);
    iVar4 = GetDeviceCaps(hdc,0xc);
    ReleaseDC(hWnd,hdc);
    iVar1 = FUN_0ffe4ec0(iVar1,iVar2,iVar3 * iVar4,0,0,0);
    if (iVar1 != 0) {
      iVar2 = FUN_0ffe51f0(param_2,iVar1,&local_4,0);
      if ((iVar2 != 0) || (iVar2 = local_4, local_4 != 0)) {
        *param_3 = iVar1;
        *param_4 = iVar2;
        return 1;
      }
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4140 @ 0ffe4140 */
/* ===================================================== */

undefined4
FUN_0ffe4140(int param_1,undefined4 param_2,int param_3,int param_4,int param_5,undefined4 param_6,
            undefined4 *param_7,int *param_8)

{
  int *piVar1;
  int iVar2;
  int iVar3;
  int *piVar4;
  int iVar5;
  int iVar6;
  
  piVar1 = param_8;
  if (((param_1 != 0) && (param_7 != (undefined4 *)0x0)) && (param_8 != (int *)0x0)) {
    iVar2 = FUN_0ffe50c0(param_2,&param_1,0);
    if ((iVar2 != 0) || (iVar2 = param_1, param_1 != 0)) {
      if ((param_3 == 0) ||
         ((param_4 == 0 || (iVar3 = param_5, iVar5 = param_4, iVar6 = param_3, param_5 == 0)))) {
        iVar6 = 0x280;
        iVar5 = 0x1e0;
        iVar3 = 0x10;
      }
      piVar4 = (int *)FUN_0ffe52b0(iVar6,iVar5,iVar3,param_6,iVar2,&param_8,0);
      if ((piVar4 != (int *)0x0) || (piVar4 = param_8, param_8 != (int *)0x0)) {
        *param_7 = piVar4;
        *piVar1 = iVar2;
        return 1;
      }
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe41f0 @ 0ffe41f0 */
/* ===================================================== */

int FUN_0ffe41f0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                undefined4 param_5,undefined4 param_6,int param_7,int *param_8,undefined4 *param_9,
                undefined4 *param_10)

{
  int *piVar1;
  undefined4 *puVar2;
  undefined4 *puVar3;
  int iVar4;
  int iVar5;
  
  puVar3 = param_10;
  puVar2 = param_9;
  piVar1 = param_8;
  if (((param_8 != (int *)0x0) && (param_9 != (undefined4 *)0x0)) && (param_10 != (undefined4 *)0x0)
     ) {
    if ((((byte)DAT_0ffe8104 & 1) == 0) && (iVar4 = FUN_0ffe53a0(), iVar4 < 0)) {
      return iVar4;
    }
    iVar4 = FUN_0ffe3fd0(param_1);
    if (iVar4 != 0) {
      if (param_7 == 0) {
        iVar5 = FUN_0ffe4060(iVar4,param_6,&param_8,&param_9);
        if (iVar5 == 0) {
          return -0x7789d8ef;
        }
      }
      else {
        iVar5 = FUN_0ffe4140(iVar4,param_6,param_2,param_3,param_4,param_5,&param_8,&param_9);
        if (iVar5 == 0) {
          return -0x7789d8ef;
        }
      }
      *piVar1 = iVar4;
      *puVar2 = param_8;
      *puVar3 = param_9;
      return 0;
    }
  }
  return -0x7789d8ee;
}



/* ===================================================== */
/* FUN_0ffe42d0 @ 0ffe42d0 */
/* ===================================================== */

uint FUN_0ffe42d0(uint param_1)

{
  if ((param_1 & 0x4000) != 0) {
    return 1;
  }
  if ((param_1 & 0x2000) != 0) {
    return 2;
  }
  if ((param_1 & 0x1000) != 0) {
    return 4;
  }
  if ((param_1 & 0x800) != 0) {
    return 8;
  }
  if ((param_1 & 0x400) != 0) {
    return 0x10;
  }
  if ((param_1 & 0x200) != 0) {
    return 0x18;
  }
  return param_1 >> 3 & 0x20;
}



/* ===================================================== */
/* FUN_0ffe4330 @ 0ffe4330 */
/* ===================================================== */

undefined4 FUN_0ffe4330(undefined4 param_1)

{
  switch(param_1) {
  case 1:
    return 0x4000;
  case 2:
    return 0x2000;
  default:
    return 0;
  case 4:
    return 0x1000;
  case 8:
    return 0x800;
  case 0x10:
    return 0x400;
  case 0x18:
    return 0x200;
  case 0x20:
    return 0x100;
  }
}



/* ===================================================== */
/* FUN_0ffe43d0 @ 0ffe43d0 */
/* ===================================================== */

uint FUN_0ffe43d0(int param_1)

{
  uint uVar1;
  
  if (param_1 == 0) {
    return 0;
  }
  uVar1 = *(uint *)(param_1 + 4);
  if ((uVar1 & 0x800) != 0) {
    return 1;
  }
  if ((uVar1 & 0x1000) != 0) {
    return 1;
  }
  if ((uVar1 & 8) != 0) {
    return 1;
  }
  return (uVar1 & 0xff) >> 5 & 1;
}



/* ===================================================== */
/* FUN_0ffe4600 @ 0ffe4600 */
/* ===================================================== */

undefined4
FUN_0ffe4600(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
            undefined4 param_5,int param_6)

{
  uint *puVar1;
  int iVar2;
  uint *puVar3;
  
  if ((param_6 != 0) && (*(int *)(param_6 + 8) != 0)) {
    puVar1 = (uint *)Malloc(0x228);
    if (puVar1 == (uint *)0x0) {
      puVar1 = (uint *)0x0;
    }
    else {
      *puVar1 = *puVar1 & 0xfffffffc;
      puVar1[5] = 0;
      puVar1[6] = 0;
      puVar3 = puVar1 + 7;
      for (iVar2 = 0x3f; iVar2 != 0; iVar2 = iVar2 + -1) {
        *puVar3 = 0;
        puVar3 = puVar3 + 1;
      }
      puVar1[7] = 0xfc;
      puVar3 = puVar1 + 0x46;
      for (iVar2 = 0x3f; iVar2 != 0; iVar2 = iVar2 + -1) {
        *puVar3 = 0;
        puVar3 = puVar3 + 1;
      }
      puVar1[0x46] = 0xfc;
      puVar1[0x85] = 0;
      puVar1[0x86] = 0;
      puVar1[0x87] = 0;
      puVar1[0x88] = 0;
      puVar1[0x89] = 0;
    }
    if (puVar1 != (uint *)0x0) {
      iVar2 = FUN_0ffe4840(param_1,param_2,param_3,param_4,param_5);
      if (-1 < iVar2) {
        iVar2 = FUN_0ffe5060(puVar1);
        if (-1 < iVar2) {
          *(int *)(param_6 + 4) = *(int *)(param_6 + 4) + 1;
        }
      }
    }
  }
  return 1;
}



/* ===================================================== */
/* FUN_0ffe46d0 @ 0ffe46d0 */
/* ===================================================== */

int __fastcall FUN_0ffe46d0(int *param_1)

{
  if (*param_1 != 0x6c) {
    return 0;
  }
  if ((param_1[1] & 0x1000U) == 0) {
    return 0;
  }
  if (param_1[0x12] != 0x20) {
    return 0;
  }
  return param_1[0x15];
}



/* ===================================================== */
/* FUN_0ffe46f0 @ 0ffe46f0 */
/* ===================================================== */

undefined4 __thiscall
FUN_0ffe46f0(int *param_1,int *param_2,int *param_3,int *param_4,undefined4 *param_5)

{
  uint uVar1;
  
  if (*param_1 != 0x6c) {
    return 0x80004005;
  }
  uVar1 = param_1[1];
  if ((uVar1 & 4) == 0) {
    return 0x80004005;
  }
  if ((uVar1 & 2) == 0) {
    return 0x80004005;
  }
  if ((uVar1 & 0x1000) == 0) {
    return 0x80004005;
  }
  if (param_1[0x12] != 0x20) {
    return 0x80004005;
  }
  *param_2 = param_1[3];
  *param_3 = param_1[2];
  *param_4 = param_1[0x15];
  *param_5 = 0;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4760 @ 0ffe4760 */
/* ===================================================== */

bool FUN_0ffe4760(int param_1)

{
  undefined4 uVar1;
  uint uVar2;
  int iVar3;
  
  if (param_1 == 0) {
    return false;
  }
  uVar1 = FUN_0ffe46d0();
  uVar2 = FUN_0ffe4330(uVar1);
  iVar3 = FUN_0ffe47f0();
  if (iVar3 != 0) {
    return (*(uint *)(param_1 + 0xb8) & uVar2) != 0;
  }
  return (*(uint *)(param_1 + 0x1b4) & uVar2) != 0;
}



/* ===================================================== */
/* FUN_0ffe47b0 @ 0ffe47b0 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe47b0(int *param_1,int param_2,int param_3,int param_4)

{
  if ((((*param_1 == 0x6c) && (param_1[3] == param_2)) && (param_1[2] == param_3)) &&
     (param_1[0x15] == param_4)) {
    return 1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe47f0 @ 0ffe47f0 */
/* ===================================================== */

bool __fastcall FUN_0ffe47f0(int param_1)

{
  return *(int *)(param_1 + 0x24) != 0;
}



/* ===================================================== */
/* FUN_0ffe4800 @ 0ffe4800 */
/* ===================================================== */

undefined1 __thiscall FUN_0ffe4800(byte *param_1,byte *param_2)

{
  byte bVar1;
  byte bVar2;
  int iVar3;
  byte *pbVar4;
  
  if (param_2 == (byte *)0x0) {
    return 0;
  }
  if ((*param_1 & 1) != 0) {
    iVar3 = 0x10;
    pbVar4 = param_1 + 4;
    do {
      if (iVar3 == 0) {
        return 1;
      }
      iVar3 = iVar3 + -1;
      bVar2 = *param_2;
      bVar1 = *pbVar4;
      pbVar4 = pbVar4 + 1;
      param_2 = param_2 + 1;
    } while (bVar1 == bVar2);
    return 0;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4840 @ 0ffe4840 */
/* ===================================================== */

undefined4 __thiscall
FUN_0ffe4840(uint *param_1,uint *param_2,char *param_3,char *param_4,uint *param_5,uint *param_6)

{
  char cVar1;
  char *pcVar2;
  uint uVar3;
  size_t sVar4;
  int iVar5;
  uint *puVar6;
  
  if (param_2 == (uint *)0x0) {
    return 0x80070057;
  }
  param_1[1] = *param_2;
  param_1[2] = param_2[1];
  param_1[3] = param_2[2];
  param_1[4] = param_2[3];
  if (param_3 == (char *)0x0) {
    param_3 = s_UNKNOWN_0ffe80b0;
  }
  uVar3 = 0xffffffff;
  pcVar2 = param_3;
  do {
    if (uVar3 == 0) break;
    uVar3 = uVar3 - 1;
    cVar1 = *pcVar2;
    pcVar2 = pcVar2 + 1;
  } while (cVar1 != '\0');
  sVar4 = ~uVar3 - 1;
  pcVar2 = (char *)Malloc(~uVar3);
  param_1[5] = (uint)pcVar2;
  if (pcVar2 != (char *)0x0) {
    _strncpy(pcVar2,param_3,sVar4);
    *(undefined1 *)(sVar4 + param_1[5]) = 0;
  }
  if (param_4 == (char *)0x0) {
    param_4 = s_UNKNOWN_0ffe80b0;
  }
  uVar3 = 0xffffffff;
  pcVar2 = param_4;
  do {
    if (uVar3 == 0) break;
    uVar3 = uVar3 - 1;
    cVar1 = *pcVar2;
    pcVar2 = pcVar2 + 1;
  } while (cVar1 != '\0');
  sVar4 = ~uVar3 - 1;
  pcVar2 = (char *)Malloc(~uVar3);
  param_1[6] = (uint)pcVar2;
  if (pcVar2 != (char *)0x0) {
    _strncpy(pcVar2,param_4,sVar4);
    *(undefined1 *)(sVar4 + param_1[6]) = 0;
  }
  if (param_5 != (uint *)0x0) {
    puVar6 = param_1 + 7;
    for (iVar5 = 0x3f; iVar5 != 0; iVar5 = iVar5 + -1) {
      *puVar6 = *param_5;
      param_5 = param_5 + 1;
      puVar6 = puVar6 + 1;
    }
  }
  if (param_6 != (uint *)0x0) {
    puVar6 = param_1 + 0x46;
    for (iVar5 = 0x3f; iVar5 != 0; iVar5 = iVar5 + -1) {
      *puVar6 = *param_6;
      param_6 = param_6 + 1;
      puVar6 = puVar6 + 1;
    }
  }
  param_1[0x85] = 0;
  *param_1 = *param_1 & 0xfffffffd | 1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4930 @ 0ffe4930 */
/* ===================================================== */

void __fastcall FUN_0ffe4930(int param_1)

{
  FUN_0ffe4a00();
  if (*(int *)(param_1 + 0x18) != 0) {
    Free(*(int *)(param_1 + 0x18));
    *(undefined4 *)(param_1 + 0x18) = 0;
  }
  if (*(int *)(param_1 + 0x14) != 0) {
    Free(*(int *)(param_1 + 0x14));
    *(undefined4 *)(param_1 + 0x14) = 0;
  }
  *(undefined4 *)(param_1 + 0x220) = 0;
  *(undefined4 *)(param_1 + 0x224) = 0;
  return;
}



/* ===================================================== */
/* FUN_0ffe4970 @ 0ffe4970 */
/* ===================================================== */

int __thiscall FUN_0ffe4970(uint *param_1,int *param_2)

{
  int iVar1;
  undefined4 local_c;
  undefined4 local_8;
  uint *local_4;
  
  if ((*param_1 & 2) == 0) {
    if (param_2 == (int *)0x0) {
      return -0x7789d8ee;
    }
    local_c = 1;
    local_8 = 0;
    local_4 = param_1;
    iVar1 = (**(code **)(*param_2 + 0x24))(param_2);
    if (-1 < iVar1) {
      if ((&stack0x00000000 != (undefined1 *)0xc) && ((undefined4 *)param_1[0x85] == &local_c)) {
        *param_1 = *param_1 | 2;
        return 0;
      }
      return -0x7789d8ef;
    }
  }
  else {
    iVar1 = 0;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe4a00 @ 0ffe4a00 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe4a00(uint *param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if ((*param_1 & 2) != 0) {
    uVar2 = param_1[0x86];
    while (uVar3 = uVar2, uVar3 != 0) {
      uVar1 = *(uint *)(uVar3 + 0x6c);
      uVar2 = *(uint *)(uVar3 + 0x70);
      if (uVar1 == 0) {
        param_1[0x86] = uVar2;
      }
      else {
        *(uint *)(uVar1 + 0x70) = uVar2;
      }
      if (uVar2 == 0) {
        param_1[0x87] = uVar1;
      }
      else {
        *(uint *)(uVar2 + 0x6c) = uVar1;
      }
      *(undefined4 *)(uVar3 + 0x70) = 0;
      *(undefined4 *)(uVar3 + 0x6c) = 0;
      if (uVar3 != 0) {
        *(undefined4 *)(uVar3 + 0x6c) = 0;
        *(undefined4 *)(uVar3 + 0x70) = 0;
        Free(uVar3);
      }
    }
    param_1[0x85] = 0;
    param_1[0x86] = 0;
    param_1[0x87] = 0;
    *param_1 = *param_1 & 0xfffffffd;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4a80 @ 0ffe4a80 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe4a80(int param_1,int param_2)

{
  undefined4 uVar1;
  
  if (param_2 == 0) {
    return 0x80070057;
  }
  uVar1 = *(undefined4 *)(param_1 + 0x21c);
  *(undefined4 *)(param_2 + 0x70) = 0;
  *(undefined4 *)(param_2 + 0x6c) = uVar1;
  if (*(int *)(param_1 + 0x21c) != 0) {
    *(int *)(*(int *)(param_1 + 0x21c) + 0x70) = param_2;
  }
  *(int *)(param_1 + 0x21c) = param_2;
  if (*(int *)(param_1 + 0x218) == 0) {
    *(int *)(param_1 + 0x218) = param_2;
  }
  *(int *)(param_1 + 0x214) = *(int *)(param_1 + 0x214) + 1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4ae0 @ 0ffe4ae0 */
/* ===================================================== */

int __thiscall FUN_0ffe4ae0(uint *param_1,uint *param_2,char *param_3,char *param_4)

{
  char cVar1;
  char *pcVar2;
  int iVar3;
  uint uVar4;
  size_t sVar5;
  int *local_c;
  int *local_8;
  int *local_4;
  
  local_4 = (int *)0x0;
  local_c = (int *)0x0;
  local_8 = (int *)0x0;
  if ((*param_1 & 1) != 0) {
    return 0;
  }
  if (param_2 == (uint *)0x0) {
    *param_1 = *param_1 | 2;
  }
  else {
    param_1[1] = *param_2;
    param_1[2] = param_2[1];
    param_1[3] = param_2[2];
    param_1[4] = param_2[3];
  }
  if (param_3 == (char *)0x0) {
    param_3 = s_UNKNOWN_0ffe80b0;
  }
  uVar4 = 0xffffffff;
  pcVar2 = param_3;
  do {
    if (uVar4 == 0) break;
    uVar4 = uVar4 - 1;
    cVar1 = *pcVar2;
    pcVar2 = pcVar2 + 1;
  } while (cVar1 != '\0');
  sVar5 = ~uVar4 - 1;
  pcVar2 = (char *)Malloc(~uVar4);
  param_1[5] = (uint)pcVar2;
  if (pcVar2 != (char *)0x0) {
    _strncpy(pcVar2,param_3,sVar5);
    *(undefined1 *)(sVar5 + param_1[5]) = 0;
  }
  if (param_4 == (char *)0x0) {
    param_4 = s_UNKNOWN_0ffe80b0;
  }
  uVar4 = 0xffffffff;
  pcVar2 = param_4;
  do {
    if (uVar4 == 0) break;
    uVar4 = uVar4 - 1;
    cVar1 = *pcVar2;
    pcVar2 = pcVar2 + 1;
  } while (cVar1 != '\0');
  sVar5 = ~uVar4 - 1;
  pcVar2 = (char *)Malloc(~uVar4);
  param_1[6] = (uint)pcVar2;
  if (pcVar2 != (char *)0x0) {
    _strncpy(pcVar2,param_4,sVar5);
    *(undefined1 *)(sVar5 + param_1[6]) = 0;
  }
  iVar3 = DirectDrawCreate(param_2,&local_4,0);
  if (-1 < iVar3) {
    iVar3 = (**(code **)*local_4)(local_4,&DAT_0ffe6290,&local_c);
    if (-1 < iVar3) {
      iVar3 = (**(code **)*local_4)(local_4,&DAT_0ffe65b0,&local_8);
      if (-1 < iVar3) {
        iVar3 = *local_c;
        param_1[7] = 0x17c;
        param_1[0x66] = 0x17c;
        iVar3 = (**(code **)(iVar3 + 0x2c))(local_c,param_1 + 7,param_1 + 0x66);
        if (-1 < iVar3) {
          param_1[0xc5] = 0;
          *param_1 = *param_1 & 0xfffffff7;
          iVar3 = FUN_0ffe4d50(local_c);
          if (-1 < iVar3) {
            param_1[200] = 0;
            *param_1 = *param_1 & 0xffffffef;
            iVar3 = FUN_0ffe4f40(local_8);
            if (-1 < iVar3) {
              iVar3 = 0;
              *param_1 = *param_1 | 1;
            }
          }
        }
      }
    }
  }
  if (local_8 != (int *)0x0) {
    (**(code **)(*local_8 + 8))(local_8);
    local_8 = (int *)0x0;
  }
  if (local_c != (int *)0x0) {
    (**(code **)(*local_c + 8))(local_c);
    local_c = (int *)0x0;
  }
  if (local_4 != (int *)0x0) {
    (**(code **)(*local_4 + 8))(local_4);
  }
  return iVar3;
}



/* ===================================================== */
/* FUN_0ffe4cb0 @ 0ffe4cb0 */
/* ===================================================== */

void __fastcall FUN_0ffe4cb0(uint *param_1)

{
  FUN_0ffe4fd0();
  FUN_0ffe4de0();
  if (param_1[6] != 0) {
    Free(param_1[6]);
    param_1[6] = 0;
  }
  if (param_1[5] != 0) {
    Free(param_1[5]);
    param_1[5] = 0;
  }
  param_1[0xcb] = 0;
  param_1[0xcc] = 0;
  *param_1 = *param_1 & 0xfffffffe;
  return;
}



/* ===================================================== */
/* FUN_0ffe4d00 @ 0ffe4d00 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe4d00(uint *param_1,char *param_2)

{
  int iVar1;
  uint *puVar2;
  bool bVar3;
  
  if ((*param_1 & 1) != 0) {
    if (param_2 == (char *)0x0) {
      if ((*param_1 & 2) != 0) {
        return 1;
      }
    }
    else {
      iVar1 = 0x10;
      bVar3 = true;
      puVar2 = param_1 + 1;
      do {
        if (iVar1 == 0) break;
        iVar1 = iVar1 + -1;
        bVar3 = *param_2 == (char)*puVar2;
        param_2 = param_2 + 1;
        puVar2 = (uint *)((int)puVar2 + 1);
      } while (bVar3);
      if (bVar3) {
        return 1;
      }
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4d50 @ 0ffe4d50 */
/* ===================================================== */

int __thiscall FUN_0ffe4d50(uint *param_1,int *param_2)

{
  int iVar1;
  int iVar2;
  uint uVar3;
  undefined4 local_c;
  undefined4 local_8;
  uint *local_4;
  
  if ((*param_1 & 8) == 0) {
    if (param_2 == (int *)0x0) {
      return -0x7789d8ee;
    }
    uVar3 = 0;
    iVar2 = 0;
    local_c = 1;
    local_8 = 0;
    local_4 = param_1;
    iVar1 = (**(code **)(*param_2 + 0x20))(param_2,0,0,&local_c,&LAB_0ffe44e0);
    if (-1 < iVar1) {
      if (((iVar2 != 0) && (uVar3 != 0)) && (param_1[0xc5] == uVar3)) {
        *param_1 = *param_1 | 8;
        return 0;
      }
      return -0x7789d8ef;
    }
  }
  else {
    iVar1 = 0;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe4de0 @ 0ffe4de0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe4de0(uint *param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar2 = param_1[0xc6];
  while (uVar3 = uVar2, uVar3 != 0) {
    uVar1 = *(uint *)(uVar3 + 0x6c);
    uVar2 = *(uint *)(uVar3 + 0x70);
    if (uVar1 == 0) {
      param_1[0xc6] = uVar2;
    }
    else {
      *(uint *)(uVar1 + 0x70) = uVar2;
    }
    if (uVar2 == 0) {
      param_1[199] = uVar1;
    }
    else {
      *(uint *)(uVar2 + 0x6c) = uVar1;
    }
    *(undefined4 *)(uVar3 + 0x70) = 0;
    *(undefined4 *)(uVar3 + 0x6c) = 0;
    if (uVar3 != 0) {
      *(undefined4 *)(uVar3 + 0x6c) = 0;
      *(undefined4 *)(uVar3 + 0x70) = 0;
      Free(uVar3);
    }
  }
  param_1[0xc5] = 0;
  param_1[0xc6] = 0;
  param_1[199] = 0;
  *param_1 = *param_1 & 0xfffffff7;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4e60 @ 0ffe4e60 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe4e60(int param_1,int param_2)

{
  undefined4 uVar1;
  
  if (param_2 == 0) {
    return 0x80070057;
  }
  uVar1 = *(undefined4 *)(param_1 + 0x31c);
  *(undefined4 *)(param_2 + 0x70) = 0;
  *(undefined4 *)(param_2 + 0x6c) = uVar1;
  if (*(int *)(param_1 + 0x31c) != 0) {
    *(int *)(*(int *)(param_1 + 0x31c) + 0x70) = param_2;
  }
  *(int *)(param_1 + 0x31c) = param_2;
  if (*(int *)(param_1 + 0x318) == 0) {
    *(int *)(param_1 + 0x318) = param_2;
  }
  *(int *)(param_1 + 0x314) = *(int *)(param_1 + 0x314) + 1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4ec0 @ 0ffe4ec0 */
/* ===================================================== */

int __thiscall
FUN_0ffe4ec0(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,undefined4 param_5
            ,int *param_6,int param_7)

{
  int iVar1;
  int iVar2;
  
  if (param_7 == 0) {
    param_7 = *(int *)(param_1 + 0x318);
  }
  if (param_6 != (int *)0x0) {
    *param_6 = param_7;
  }
  if (param_7 == 0) {
    return 0;
  }
  do {
    iVar1 = *(int *)(param_7 + 0x70);
    iVar2 = FUN_0ffe47b0(param_2,param_3,param_4);
    if (iVar2 != 0) {
      return param_7;
    }
    iVar2 = FUN_0ffe47b0(0x280,0x1e0,8);
    if ((iVar2 != 0) && (param_6 != (int *)0x0)) {
      *param_6 = param_7;
    }
    param_7 = iVar1;
  } while (iVar1 != 0);
  return 0;
}



/* ===================================================== */
/* FUN_0ffe4f40 @ 0ffe4f40 */
/* ===================================================== */

int __thiscall FUN_0ffe4f40(uint *param_1,int *param_2)

{
  int iVar1;
  undefined4 local_c;
  undefined4 local_8;
  uint *local_4;
  
  if ((*param_1 & 0x10) == 0) {
    if (param_2 == (int *)0x0) {
      return -0x7789d8ee;
    }
    local_c = 1;
    local_8 = 0;
    local_4 = param_1;
    iVar1 = (**(code **)(*param_2 + 0xc))(param_2);
    if (-1 < iVar1) {
      if ((&stack0x00000000 != (undefined1 *)0xc) && ((undefined4 *)param_1[200] == &local_c)) {
        *param_1 = *param_1 | 0x10;
        return 0;
      }
      return -0x7789d8ef;
    }
  }
  else {
    iVar1 = 0;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe4fd0 @ 0ffe4fd0 */
/* ===================================================== */

undefined4 __fastcall FUN_0ffe4fd0(uint *param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar2 = param_1[0xc9];
  while (uVar3 = uVar2, uVar3 != 0) {
    uVar1 = *(uint *)(uVar3 + 0x220);
    uVar2 = *(uint *)(uVar3 + 0x224);
    if (uVar1 == 0) {
      param_1[0xc9] = uVar2;
    }
    else {
      *(uint *)(uVar1 + 0x224) = uVar2;
    }
    if (uVar2 == 0) {
      param_1[0xca] = uVar1;
    }
    else {
      *(uint *)(uVar2 + 0x220) = uVar1;
    }
    *(undefined4 *)(uVar3 + 0x224) = 0;
    *(undefined4 *)(uVar3 + 0x220) = 0;
    if (uVar3 != 0) {
      FUN_0ffe4930();
      Free(uVar3);
    }
  }
  param_1[200] = 0;
  param_1[0xc9] = 0;
  param_1[0xca] = 0;
  *param_1 = *param_1 & 0xffffffef;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe5060 @ 0ffe5060 */
/* ===================================================== */

undefined4 __thiscall FUN_0ffe5060(int param_1,int param_2)

{
  undefined4 uVar1;
  
  if (param_2 == 0) {
    return 0x80070057;
  }
  uVar1 = *(undefined4 *)(param_1 + 0x328);
  *(undefined4 *)(param_2 + 0x224) = 0;
  *(undefined4 *)(param_2 + 0x220) = uVar1;
  if (*(int *)(param_1 + 0x328) != 0) {
    *(int *)(*(int *)(param_1 + 0x328) + 0x224) = param_2;
  }
  *(int *)(param_1 + 0x328) = param_2;
  if (*(int *)(param_1 + 0x324) == 0) {
    *(int *)(param_1 + 0x324) = param_2;
  }
  *(int *)(param_1 + 800) = *(int *)(param_1 + 800) + 1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe50c0 @ 0ffe50c0 */
/* ===================================================== */

int __thiscall FUN_0ffe50c0(int param_1,undefined4 param_2,int *param_3,int param_4)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  char *pcVar5;
  char *pcVar6;
  bool bVar7;
  int local_c;
  int local_4;
  
  local_c = 0;
  local_4 = 0;
  if (param_3 != (int *)0x0) {
    *param_3 = 0;
  }
  iVar2 = 0;
  iVar1 = param_4;
  if (param_4 == 0) {
    param_4 = *(int *)(param_1 + 0x324);
    iVar1 = param_4;
  }
  do {
    iVar3 = iVar1;
    if (iVar3 == 0) {
      if (param_3 != (int *)0x0) {
        if (local_c != 0) {
          *param_3 = local_c;
          return 0;
        }
        if (local_4 != 0) {
          *param_3 = local_4;
          return 0;
        }
        if (iVar2 != 0) {
          *param_3 = iVar2;
          return 0;
        }
        if (param_4 != 0) {
          *param_3 = param_4;
        }
      }
      return 0;
    }
    iVar1 = *(int *)(iVar3 + 0x224);
    iVar4 = FUN_0ffe4800(param_2);
    if (iVar4 != 0) {
      return iVar3;
    }
    iVar4 = FUN_0ffe47f0();
    if ((iVar4 != 0) && (local_c == 0)) {
      local_c = iVar3;
    }
    iVar4 = 0x10;
    bVar7 = true;
    pcVar5 = (char *)(iVar3 + 4);
    pcVar6 = &DAT_0ffe65e0;
    do {
      if (iVar4 == 0) break;
      iVar4 = iVar4 + -1;
      bVar7 = *pcVar5 == *pcVar6;
      pcVar5 = pcVar5 + 1;
      pcVar6 = pcVar6 + 1;
    } while (bVar7);
    if ((bVar7) && (local_4 == 0)) {
      local_4 = iVar3;
    }
    iVar4 = 0x10;
    bVar7 = true;
    pcVar5 = (char *)(iVar3 + 4);
    pcVar6 = &DAT_0ffe6600;
    do {
      if (iVar4 == 0) break;
      iVar4 = iVar4 + -1;
      bVar7 = *pcVar5 == *pcVar6;
      pcVar5 = pcVar5 + 1;
      pcVar6 = pcVar6 + 1;
    } while (bVar7);
    if ((bVar7) && (iVar2 == 0)) {
      iVar2 = iVar3;
    }
  } while( true );
}



/* ===================================================== */
/* FUN_0ffe51f0 @ 0ffe51f0 */
/* ===================================================== */

int __thiscall FUN_0ffe51f0(int param_1,undefined4 param_2,int param_3,int *param_4,int param_5)

{
  int iVar1;
  int iVar2;
  int *piVar3;
  
  if (param_3 == 0) {
    if (param_4 != (int *)0x0) {
      *param_4 = 0;
      return 0;
    }
  }
  else {
    if (param_5 == 0) {
      param_5 = *(int *)(param_1 + 0x324);
    }
    if ((param_4 != (int *)0x0) && (iVar1 = FUN_0ffe4760(param_5), iVar1 != 0)) {
      *param_4 = param_5;
    }
    if (param_5 != 0) {
      do {
        iVar1 = *(int *)(param_5 + 0x224);
        iVar2 = FUN_0ffe4800(param_2);
        if (iVar2 == 0) {
          iVar2 = FUN_0ffe47f0();
          if (iVar2 == 0) {
            iVar2 = FUN_0ffe4760(param_5);
            piVar3 = param_4;
            if (iVar2 != 0) goto joined_r0x0ffe528a;
          }
          else if (param_4 != (int *)0x0) {
            piVar3 = (int *)FUN_0ffe4760(param_5);
joined_r0x0ffe528a:
            if (piVar3 != (int *)0x0) {
              *param_4 = param_5;
            }
          }
        }
        else {
          iVar2 = FUN_0ffe4760(param_5);
          if (iVar2 != 0) {
            return param_5;
          }
        }
        param_5 = iVar1;
        if (iVar1 == 0) {
          return 0;
        }
      } while( true );
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe52b0 @ 0ffe52b0 */
/* ===================================================== */

int __thiscall
FUN_0ffe52b0(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,undefined4 param_5
            ,int param_6,int *param_7,int param_8)

{
  int iVar1;
  int iVar2;
  int *piVar3;
  
  if (param_6 == 0) {
    if (param_7 != (int *)0x0) {
      *param_7 = 0;
      return 0;
    }
  }
  else {
    if (param_8 == 0) {
      param_8 = *(int *)(param_1 + 0x318);
    }
    if ((param_7 != (int *)0x0) && (iVar1 = FUN_0ffe4760(param_6), iVar1 != 0)) {
      *param_7 = param_8;
    }
    if (param_8 != 0) {
      do {
        iVar1 = *(int *)(param_8 + 0x70);
        iVar2 = FUN_0ffe47b0(param_2,param_3,param_4);
        if (iVar2 == 0) {
          iVar2 = FUN_0ffe47b0(0x280,0x1e0,0x10);
          if (iVar2 == 0) {
            iVar2 = FUN_0ffe4760(param_6);
            piVar3 = param_7;
            if (iVar2 != 0) goto joined_r0x0ffe535d;
          }
          else if (param_7 != (int *)0x0) {
            piVar3 = (int *)FUN_0ffe4760(param_6);
joined_r0x0ffe535d:
            if (piVar3 != (int *)0x0) {
              *param_7 = param_8;
            }
          }
        }
        else {
          iVar2 = FUN_0ffe4760(param_6);
          if (iVar2 != 0) {
            return param_8;
          }
        }
        param_8 = iVar1;
        if (iVar1 == 0) {
          return 0;
        }
      } while( true );
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe5390 @ 0ffe5390 */
/* ===================================================== */

byte * __fastcall FUN_0ffe5390(byte *param_1)

{
  if ((*param_1 & 2) != 0) {
    return (byte *)0x0;
  }
  return param_1 + 4;
}



/* ===================================================== */
/* FUN_0ffe53a0 @ 0ffe53a0 */
/* ===================================================== */

int FUN_0ffe53a0(void)

{
  int iVar1;
  
  if ((DAT_0ffe8104 & 1) == 0) {
    DAT_0ffe8108 = 0;
    iVar1 = FUN_0ffe53f0();
    if (iVar1 < 0) {
      return iVar1;
    }
    DAT_0ffe8104 = DAT_0ffe8104 | 1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe53d0 @ 0ffe53d0 */
/* ===================================================== */

undefined4 FUN_0ffe53d0(void)

{
  if ((DAT_0ffe8104 & 1) != 0) {
    FUN_0ffe5450();
    DAT_0ffe8104 = DAT_0ffe8104 & 0xfffffffe;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe53f0 @ 0ffe53f0 */
/* ===================================================== */

int FUN_0ffe53f0(void)

{
  int iVar1;
  int local_c;
  int local_8;
  undefined4 local_4;
  
  local_c = 1;
  local_8 = 0;
  local_4 = 0;
  iVar1 = DirectDrawEnumerateA(&LAB_0ffe4420,&local_c);
  if (-1 < iVar1) {
    if (((local_c != 0) && (local_8 != 0)) && (DAT_0ffe8108 == local_8)) {
      return 0;
    }
    iVar1 = -0x7789d8ef;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_0ffe5450 @ 0ffe5450 */
/* ===================================================== */

undefined4 FUN_0ffe5450(void)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  
  iVar2 = DAT_0ffe810c;
  while (iVar3 = iVar2, iVar3 != 0) {
    iVar1 = *(int *)(iVar3 + 0x32c);
    iVar2 = *(int *)(iVar3 + 0x330);
    iVar4 = iVar2;
    if (iVar1 != 0) {
      *(int *)(iVar1 + 0x330) = iVar2;
      iVar4 = DAT_0ffe810c;
    }
    DAT_0ffe810c = iVar4;
    if (iVar2 != 0) {
      *(int *)(iVar2 + 0x32c) = iVar1;
      iVar1 = DAT_0ffe8110;
    }
    DAT_0ffe8110 = iVar1;
    *(undefined4 *)(iVar3 + 0x330) = 0;
    *(undefined4 *)(iVar3 + 0x32c) = 0;
    if (iVar3 != 0) {
      FUN_0ffe4cb0();
      Free(iVar3);
    }
  }
  DAT_0ffe8108 = 0;
  DAT_0ffe810c = 0;
  DAT_0ffe8110 = 0;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe54d0 @ 0ffe54d0 */
/* ===================================================== */

void __fastcall FUN_0ffe54d0(undefined4 *param_1)

{
  *param_1 = 0;
  param_1[1] = 0;
  param_1[2] = 0;
  return;
}



/* ===================================================== */
/* FUN_0ffe54e0 @ 0ffe54e0 */
/* ===================================================== */

void FUN_0ffe54e0(void)

{
  return;
}



/* ===================================================== */
/* FUN_0ffe54f0 @ 0ffe54f0 */
/* ===================================================== */

undefined4 FUN_0ffe54f0(int param_1)

{
  int iVar1;
  
  iVar1 = DAT_0ffe8110;
  if (param_1 == 0) {
    return 0x80070057;
  }
  *(undefined4 *)(param_1 + 0x330) = 0;
  *(int *)(param_1 + 0x32c) = iVar1;
  if (DAT_0ffe8110 != 0) {
    *(int *)(DAT_0ffe8110 + 0x330) = param_1;
  }
  DAT_0ffe8110 = param_1;
  if (DAT_0ffe810c == 0) {
    DAT_0ffe810c = param_1;
  }
  DAT_0ffe8108 = DAT_0ffe8108 + 1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe5550 @ 0ffe5550 */
/* ===================================================== */

byte * FUN_0ffe5550(undefined4 param_1,undefined4 *param_2,byte *param_3)

{
  byte *pbVar1;
  int iVar2;
  
  if (param_3 == (byte *)0x0) {
    param_3 = DAT_0ffe810c;
  }
  if (param_2 != (undefined4 *)0x0) {
    *param_2 = param_3;
  }
  if (param_3 == (byte *)0x0) {
    return (byte *)0x0;
  }
  do {
    pbVar1 = *(byte **)(param_3 + 0x330);
    iVar2 = FUN_0ffe4d00(param_1);
    if (iVar2 != 0) {
      return param_3;
    }
    if (((*param_3 & 2) != 0) && (param_2 != (undefined4 *)0x0)) {
      *param_2 = param_3;
    }
    param_3 = pbVar1;
  } while (pbVar1 != (byte *)0x0);
  return (byte *)0x0;
}



/* ===================================================== */
/* DirectDrawCreate @ 0ffe55c0 */
/* ===================================================== */

void DirectDrawCreate(void)

{
                    /* WARNING: Could not recover jumptable at 0x0ffe55c0. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  DirectDrawCreate();
  return;
}



/* ===================================================== */
/* DirectDrawEnumerateA @ 0ffe55c6 */
/* ===================================================== */

void DirectDrawEnumerateA(void)

{
                    /* WARNING: Could not recover jumptable at 0x0ffe55c6. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  DirectDrawEnumerateA();
  return;
}



/* ===================================================== */
/* _strrchr @ 0ffe55d0 */
/* ===================================================== */

/* Library Function - Single Match
    _strrchr
   
   Libraries: Visual Studio 1998 Debug, Visual Studio 1998 Release */

char * __cdecl _strrchr(char *_Str,int _Ch)

{
  char cVar1;
  int iVar2;
  char *pcVar3;
  char *pcVar4;
  
  iVar2 = -1;
  do {
    pcVar4 = _Str;
    if (iVar2 == 0) break;
    iVar2 = iVar2 + -1;
    pcVar4 = _Str + 1;
    cVar1 = *_Str;
    _Str = pcVar4;
  } while (cVar1 != '\0');
  iVar2 = -(iVar2 + 1);
  pcVar4 = pcVar4 + -1;
  do {
    pcVar3 = pcVar4;
    if (iVar2 == 0) break;
    iVar2 = iVar2 + -1;
    pcVar3 = pcVar4 + -1;
    cVar1 = *pcVar4;
    pcVar4 = pcVar3;
  } while ((char)_Ch != cVar1);
  pcVar3 = pcVar3 + 1;
  if (*pcVar3 != (char)_Ch) {
    pcVar3 = (char *)0x0;
  }
  return pcVar3;
}



/* ===================================================== */
/* FUN_0ffe5600 @ 0ffe5600 */
/* ===================================================== */

uint FUN_0ffe5600(byte *param_1,byte *param_2)

{
  byte bVar1;
  byte bVar2;
  int iVar3;
  byte *pbVar4;
  byte local_20 [32];
  
  pbVar4 = local_20;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    pbVar4[0] = 0;
    pbVar4[1] = 0;
    pbVar4[2] = 0;
    pbVar4[3] = 0;
    pbVar4 = pbVar4 + 4;
  }
  do {
    bVar1 = *param_2;
    param_2 = param_2 + 1;
    local_20[bVar1 >> 3] = local_20[bVar1 >> 3] | '\x01' << (bVar1 & 7);
  } while (bVar1 != 0);
  if (param_1 == (byte *)0x0) {
    param_1 = DAT_0ffe8114;
  }
  bVar1 = *param_1;
  bVar2 = local_20[bVar1 >> 3] & (byte)(1 << (bVar1 & 7));
  while ((bVar2 != 0 && (bVar1 != 0))) {
    bVar1 = param_1[1];
    param_1 = param_1 + 1;
    bVar2 = local_20[bVar1 >> 3] & (byte)(1 << (bVar1 & 7));
  }
  bVar1 = *param_1;
  DAT_0ffe8114 = param_1;
  do {
    if (bVar1 == 0) {
LAB_0ffe56c7:
      return -(uint)(param_1 != DAT_0ffe8114) & (uint)param_1;
    }
    if ((local_20[bVar1 >> 3] & (byte)(1 << (bVar1 & 7))) != 0) {
      *DAT_0ffe8114 = 0;
      DAT_0ffe8114 = DAT_0ffe8114 + 1;
      goto LAB_0ffe56c7;
    }
    bVar1 = DAT_0ffe8114[1];
    DAT_0ffe8114 = DAT_0ffe8114 + 1;
  } while( true );
}



/* ===================================================== */
/* _strncpy @ 0ffe56e0 */
/* ===================================================== */

/* Library Function - Single Match
    _strncpy
   
   Libraries: Visual Studio 1998 Debug, Visual Studio 1998 Release */

char * __cdecl _strncpy(char *_Dest,char *_Source,size_t _Count)

{
  uint uVar1;
  uint uVar2;
  char cVar3;
  uint uVar4;
  uint *puVar5;
  
  if (_Count == 0) {
    return _Dest;
  }
  puVar5 = (uint *)_Dest;
  if (((uint)_Source & 3) != 0) {
    while( true ) {
      uVar4 = *(uint *)_Source;
      _Source = (char *)((int)_Source + 1);
      *(char *)puVar5 = (char)uVar4;
      puVar5 = (uint *)((int)puVar5 + 1);
      _Count = _Count - 1;
      if (_Count == 0) {
        return _Dest;
      }
      if ((char)uVar4 == '\0') break;
      if (((uint)_Source & 3) == 0) {
        uVar4 = _Count >> 2;
        goto joined_r0x0ffe571e;
      }
    }
    do {
      if (((uint)puVar5 & 3) == 0) {
        uVar4 = _Count >> 2;
        cVar3 = '\0';
        if (uVar4 == 0) goto LAB_0ffe575b;
        goto LAB_0ffe57c9;
      }
      *(char *)puVar5 = '\0';
      puVar5 = (uint *)((int)puVar5 + 1);
      _Count = _Count - 1;
    } while (_Count != 0);
    return _Dest;
  }
  uVar4 = _Count >> 2;
  if (uVar4 != 0) {
    do {
      uVar1 = *(uint *)_Source;
      uVar2 = *(uint *)_Source;
      _Source = (char *)((int)_Source + 4);
      if (((uVar1 ^ 0xffffffff ^ uVar1 + 0x7efefeff) & 0x81010100) != 0) {
        if ((char)uVar2 == '\0') {
          *puVar5 = 0;
joined_r0x0ffe57c5:
          while( true ) {
            uVar4 = uVar4 - 1;
            puVar5 = puVar5 + 1;
            if (uVar4 == 0) break;
LAB_0ffe57c9:
            *puVar5 = 0;
          }
          cVar3 = '\0';
          _Count = _Count & 3;
          if (_Count != 0) goto LAB_0ffe575b;
          return _Dest;
        }
        if ((char)(uVar2 >> 8) == '\0') {
          *puVar5 = uVar2 & 0xff;
          goto joined_r0x0ffe57c5;
        }
        if ((uVar2 & 0xff0000) == 0) {
          *puVar5 = uVar2 & 0xffff;
          goto joined_r0x0ffe57c5;
        }
        if ((uVar2 & 0xff000000) == 0) {
          *puVar5 = uVar2;
          goto joined_r0x0ffe57c5;
        }
      }
      *puVar5 = uVar2;
      puVar5 = puVar5 + 1;
      uVar4 = uVar4 - 1;
joined_r0x0ffe571e:
    } while (uVar4 != 0);
    _Count = _Count & 3;
    if (_Count == 0) {
      return _Dest;
    }
  }
  do {
    cVar3 = (char)*(uint *)_Source;
    _Source = (char *)((int)_Source + 1);
    *(char *)puVar5 = cVar3;
    puVar5 = (uint *)((int)puVar5 + 1);
    if (cVar3 == '\0') {
      while (_Count = _Count - 1, _Count != 0) {
LAB_0ffe575b:
        *(char *)puVar5 = cVar3;
        puVar5 = (uint *)((int)puVar5 + 1);
      }
      return _Dest;
    }
    _Count = _Count - 1;
  } while (_Count != 0);
  return _Dest;
}


