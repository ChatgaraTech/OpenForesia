/* ===================================================== */
/* OpenForesia - Ghidra decompilation reference          */
/* Program: gldhelp.dll */
/* ===================================================== */


/* ===================================================== */
/* entry @ 10001010 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 entry(undefined4 param_1,int param_2)

{
  if (param_2 == 1) {
    _DAT_1000404c = param_1;
  }
  return 1;
}



/* ===================================================== */
/* Create3DDevice @ 100010f0 */
/* ===================================================== */

undefined4 * Create3DDevice(void)

{
  undefined4 *puVar1;
  int iVar2;
  undefined4 *puVar3;
  
                    /* 0x10f0  1  Create3DDevice */
  grSstQueryBoards();
  if (&stack0x00000000 != (undefined1 *)0x94) {
    puVar1 = (undefined4 *)Malloc(0x128);
    if (puVar1 != (undefined4 *)0x0) {
      *puVar1 = &PTR_FUN_10003048;
      puVar1[0x48] = 0;
      puVar1[0x42] = 0;
      puVar1[1] = 0;
      puVar3 = puVar1 + 3;
      for (iVar2 = 0x3f; iVar2 != 0; iVar2 = iVar2 + -1) {
        *puVar3 = 0;
        puVar3 = puVar3 + 1;
      }
      puVar1[0x23] = 0x5000;
      return puVar1;
    }
  }
  return (undefined4 *)0x0;
}



/* ===================================================== */
/* FUN_10001170 @ 10001170 */
/* ===================================================== */

undefined4 __thiscall FUN_10001170(undefined4 param_1,byte param_2)

{
  FUN_100011c0();
  if ((param_2 & 1) != 0) {
    Free(param_1);
  }
  return param_1;
}



/* ===================================================== */
/* FUN_100011c0 @ 100011c0 */
/* ===================================================== */

void __fastcall FUN_100011c0(undefined4 *param_1)

{
  *param_1 = &PTR_FUN_10003048;
  if (param_1[0x42] != 0) {
    FUN_100014d0();
  }
  *param_1 = &PTR_LAB_10003138;
  return;
}



/* ===================================================== */
/* FUN_10001240 @ 10001240 */
/* ===================================================== */

undefined4 __fastcall FUN_10001240(undefined4 param_1)

{
  int iVar1;
  undefined4 uVar2;
  
  iVar1 = Malloc(0x38);
  if (iVar1 != 0) {
    uVar2 = FUN_10001c40(param_1);
    return uVar2;
  }
  return 0;
}



/* ===================================================== */
/* FUN_100014d0 @ 100014d0 */
/* ===================================================== */

undefined4 FUN_100014d0(void)

{
  grGlideShutdown();
  return 1;
}



/* ===================================================== */
/* FUN_10001530 @ 10001530 */
/* ===================================================== */

undefined4 __fastcall FUN_10001530(int *param_1)

{
  int iVar1;
  code *pcVar2;
  int unaff_EBX;
  int unaff_ESI;
  int unaff_EDI;
  int unaff_retaddr;
  
  iVar1 = *param_1;
  pcVar2 = *(code **)(iVar1 + 0x5c);
  (*pcVar2)(0,0);
  (**(code **)(iVar1 + 0xb8))();
  (*pcVar2)(0,0);
  grClipWindow(unaff_EDI,unaff_ESI,unaff_EBX + unaff_EDI,unaff_ESI + unaff_retaddr);
  return 0;
}



/* ===================================================== */
/* FUN_100015d0 @ 100015d0 */
/* ===================================================== */

undefined4 __fastcall FUN_100015d0(int param_1)

{
  *(undefined4 *)(param_1 + 0x10c) = 0;
  guColorCombineFunction(2);
  guAlphaSource(1);
  return 0;
}



/* ===================================================== */
/* FUN_10001790 @ 10001790 */
/* ===================================================== */

undefined4 __thiscall FUN_10001790(int *param_1,int param_2)

{
  param_1[0x49] = param_2;
  if (param_2 == 0) {
    FUN_100015d0();
    return 0;
  }
  FUN_10001cd0();
  param_1[0x43] = 1;
  (**(code **)(*param_1 + 0x94))(param_1[0x44]);
  return 0;
}



/* ===================================================== */
/* FUN_10001810 @ 10001810 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void __thiscall FUN_10001810(int param_1,undefined4 *param_2,undefined4 *param_3,int param_4)

{
  if (0 < param_4) {
    do {
      *param_3 = *param_2;
      param_3[1] = param_2[1];
      param_3[6] = _DAT_1000303c / ((float)param_2[2] - _DAT_10003038);
      param_3[8] = param_2[3];
      param_3[3] = (float)((uint)param_2[4] >> 0x10 & 0xff);
      param_3[4] = (float)((uint)param_2[4] >> 8 & 0xff);
      param_3[5] = (float)(param_2[4] & 0xff);
      param_3[7] = (float)((uint)param_2[4] >> 0x18);
      if (*(int *)(param_1 + 0x124) != 0) {
        param_3[0xb] = 0x3f800000;
        param_3[9] = *(float *)(*(int *)(param_1 + 0x124) + 0x30) * (float)param_2[6] *
                     (float)param_2[3] * _DAT_10003040;
        param_3[10] = *(float *)(*(int *)(param_1 + 0x124) + 0x34) * (float)param_2[7] *
                      (float)param_2[3] * _DAT_10003040;
      }
      param_2 = param_2 + 8;
      param_3 = param_3 + 0xf;
      param_4 = param_4 + -1;
    } while (param_4 != 0);
  }
  return;
}



/* ===================================================== */
/* FUN_10001c40 @ 10001c40 */
/* ===================================================== */

void __thiscall FUN_10001c40(undefined4 *param_1,undefined4 param_2)

{
  param_1[1] = param_2;
  *param_1 = &PTR_FUN_10003228;
  param_1[2] = 0;
  param_1[8] = 0xffffffff;
  param_1[9] = 0;
  param_1[10] = 0xffffffff;
  param_1[0xb] = 0;
  return;
}



/* ===================================================== */
/* FUN_10001c70 @ 10001c70 */
/* ===================================================== */

undefined4 __thiscall FUN_10001c70(undefined4 param_1,byte param_2)

{
  FUN_10001c90();
  if ((param_2 & 1) != 0) {
    Free(param_1);
  }
  return param_1;
}



/* ===================================================== */
/* FUN_10001c90 @ 10001c90 */
/* ===================================================== */

void __fastcall FUN_10001c90(undefined4 *param_1)

{
  *param_1 = &PTR_FUN_10003228;
  FUN_100024b0();
  *param_1 = &PTR_LAB_10003250;
  return;
}



/* ===================================================== */
/* FUN_10001cd0 @ 10001cd0 */
/* ===================================================== */

void __fastcall FUN_10001cd0(int param_1)

{
  if (*(int *)(param_1 + 0x20) != -1) {
    grTexSource(0,*(int *)(param_1 + 0x20),3,param_1 + 0xc);
    if (*(int *)(param_1 + 0x24) != 0) {
      grTexDownloadTable(0,2,*(int *)(param_1 + 0x24));
    }
  }
  return;
}



/* ===================================================== */
/* FUN_10001d00 @ 10001d00 */
/* ===================================================== */

undefined4 __thiscall FUN_10001d00(int param_1,int param_2,int param_3)

{
  undefined4 uVar1;
  int iVar2;
  
  if (param_2 < param_3) {
    if (param_3 % param_2 != 0) {
      return 0x80070057;
    }
    switch(param_3) {
    case 4:
      uVar1 = 6;
      break;
    default:
      return 0x80070057;
    case 8:
      uVar1 = 5;
      break;
    case 0x10:
      uVar1 = 4;
      break;
    case 0x20:
      *(undefined4 *)(param_1 + 0x10) = 3;
      *(undefined4 *)(param_1 + 0xc) = 3;
      goto LAB_10001e43;
    case 0x40:
      uVar1 = 2;
      break;
    case 0x80:
      *(undefined4 *)(param_1 + 0x10) = 1;
      *(undefined4 *)(param_1 + 0xc) = 1;
      goto LAB_10001e43;
    case 0x100:
      *(undefined4 *)(param_1 + 0x10) = 0;
      *(undefined4 *)(param_1 + 0xc) = 0;
      goto LAB_10001e43;
    }
    *(undefined4 *)(param_1 + 0x10) = uVar1;
    *(undefined4 *)(param_1 + 0xc) = uVar1;
LAB_10001e43:
    iVar2 = param_3 / param_2;
    if (iVar2 == 2) {
      *(undefined4 *)(param_1 + 0x14) = 4;
    }
    else if (iVar2 == 4) {
      *(undefined4 *)(param_1 + 0x14) = 5;
    }
    else {
      if (iVar2 != 8) {
        return 0x80070057;
      }
      *(undefined4 *)(param_1 + 0x14) = 6;
    }
    *(undefined4 *)(param_1 + 0x34) = 0x3f800000;
    *(float *)(param_1 + 0x30) = (float)param_2 / (float)param_3;
    return 0;
  }
  if (param_2 % param_3 != 0) {
    return 0x80070057;
  }
  switch(param_2) {
  case 4:
    uVar1 = 6;
    goto LAB_10001d82;
  default:
    goto switchD_10001d48_caseD_5;
  case 8:
    uVar1 = 5;
    goto LAB_10001d82;
  case 0x10:
    uVar1 = 4;
LAB_10001d82:
    *(undefined4 *)(param_1 + 0x10) = uVar1;
    *(undefined4 *)(param_1 + 0xc) = uVar1;
    break;
  case 0x20:
    *(undefined4 *)(param_1 + 0x10) = 3;
    *(undefined4 *)(param_1 + 0xc) = 3;
    break;
  case 0x40:
    *(undefined4 *)(param_1 + 0x10) = 2;
    *(undefined4 *)(param_1 + 0xc) = 2;
    break;
  case 0x80:
    *(undefined4 *)(param_1 + 0x10) = 1;
    *(undefined4 *)(param_1 + 0xc) = 1;
    break;
  case 0x100:
    *(undefined4 *)(param_1 + 0x10) = 0;
    *(undefined4 *)(param_1 + 0xc) = 0;
  }
  switch(param_2 / param_3) {
  case 1:
    *(undefined4 *)(param_1 + 0x14) = 3;
    break;
  case 2:
    *(undefined4 *)(param_1 + 0x14) = 2;
    break;
  default:
switchD_10001d48_caseD_5:
    return 0x80070057;
  case 4:
    *(undefined4 *)(param_1 + 0x14) = 1;
    break;
  case 8:
    *(undefined4 *)(param_1 + 0x14) = 0;
  }
  *(undefined4 *)(param_1 + 0x30) = 0x3f800000;
  *(float *)(param_1 + 0x34) = (float)param_3 / (float)param_2;
  return 0;
}



/* ===================================================== */
/* FUN_100024b0 @ 100024b0 */
/* ===================================================== */

void __fastcall FUN_100024b0(int param_1)

{
  FUN_100028f0(param_1);
  FUN_10002680(*(undefined4 *)(param_1 + 0x20),param_1 + 0xc);
  *(undefined4 *)(param_1 + 0x20) = 0xffffffff;
  if (*(int *)(param_1 + 0x24) != 0) {
    Free(*(int *)(param_1 + 0x24));
  }
  if (*(int *)(param_1 + 8) != 0) {
    Free(*(int *)(param_1 + 8));
  }
  return;
}



/* ===================================================== */
/* FUN_10002500 @ 10002500 */
/* ===================================================== */

undefined4 __fastcall FUN_10002500(int param_1)

{
  int iVar1;
  
  if (*(int *)(param_1 + 0x20) == -1) {
    iVar1 = FUN_10002560(param_1 + 0xc);
    *(int *)(param_1 + 0x20) = iVar1;
    if (iVar1 < 0) {
      return 0;
    }
  }
  grTexDownloadMipMap(0,*(undefined4 *)(param_1 + 0x20),3,param_1 + 0xc);
  return 1;
}



/* ===================================================== */
/* FUN_10002540 @ 10002540 */
/* ===================================================== */

int __fastcall FUN_10002540(int param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = (undefined4 *)(param_1 + 0x324);
  for (iVar1 = 200; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = 0;
    puVar2 = puVar2 + 1;
  }
  FUN_100027b0(0);
  return param_1;
}



/* ===================================================== */
/* FUN_10002560 @ 10002560 */
/* ===================================================== */

int __thiscall FUN_10002560(int *param_1,undefined4 param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  uint uVar5;
  int iVar6;
  int iVar7;
  int *piVar8;
  int *piVar9;
  int iVar10;
  
  iVar1 = grTexTextureMemRequired(3,param_2);
  iVar3 = -1;
  iVar7 = 0;
  iVar6 = 0;
  iVar2 = 0x7fffffff;
  iVar10 = -1;
  piVar8 = param_1;
  if (0 < param_1[200]) {
    do {
      iVar4 = piVar8[100] - *piVar8;
      iVar3 = iVar10;
      if (iVar4 < iVar1) {
        iVar7 = iVar7 + iVar4;
      }
      else {
        if (iVar4 == iVar1) {
          iVar2 = param_1[iVar6];
          piVar8 = param_1 + iVar6 + 1;
          piVar9 = param_1 + iVar6;
          for (uVar5 = 99U - iVar6 & 0x3fffffff; uVar5 != 0; uVar5 = uVar5 - 1) {
            *piVar9 = *piVar8;
            piVar8 = piVar8 + 1;
            piVar9 = piVar9 + 1;
          }
          for (iVar3 = 0; iVar3 != 0; iVar3 = iVar3 + -1) {
            *(char *)piVar9 = (char)*piVar8;
            piVar8 = (int *)((int)piVar8 + 1);
            piVar9 = (int *)((int)piVar9 + 1);
          }
          piVar8 = param_1 + iVar6 + 0x65;
          piVar9 = param_1 + iVar6 + 100;
          for (uVar5 = 99U - iVar6 & 0x3fffffff; uVar5 != 0; uVar5 = uVar5 - 1) {
            *piVar9 = *piVar8;
            piVar8 = piVar8 + 1;
            piVar9 = piVar9 + 1;
          }
          for (iVar3 = 0; iVar3 != 0; iVar3 = iVar3 + -1) {
            *(char *)piVar9 = (char)*piVar8;
            piVar8 = (int *)((int)piVar8 + 1);
            piVar9 = (int *)((int)piVar9 + 1);
          }
          param_1[200] = param_1[200] + -1;
          return iVar2;
        }
        if (iVar4 < iVar2) {
          iVar2 = iVar4;
          iVar3 = iVar6;
        }
      }
      iVar6 = iVar6 + 1;
      piVar8 = piVar8 + 1;
      iVar10 = iVar3;
    } while (iVar6 < param_1[200]);
  }
  if (iVar3 == -1) {
    if ((iVar1 <= iVar7) && (DAT_10004058 == 0)) {
      DAT_10004058 = 1;
      FUN_100027b0(1);
      iVar2 = FUN_10002560(param_1);
      DAT_10004058 = DAT_10004058 + -1;
      return iVar2;
    }
    return -1;
  }
  iVar2 = param_1[iVar3];
  param_1[iVar3] = iVar1 + iVar2;
  return iVar2;
}



/* ===================================================== */
/* FUN_10002680 @ 10002680 */
/* ===================================================== */

void __thiscall FUN_10002680(int *param_1,undefined4 param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  uint uVar4;
  int *piVar5;
  int iVar6;
  int iVar7;
  int *piVar8;
  
  iVar2 = grTexTextureMemRequired(3,param_3);
  iVar7 = -1;
  iVar6 = -1;
  if (param_1 != (int *)0xffffffff) {
    iVar1 = param_1[200];
    iVar3 = 0;
    if (0 < iVar1) {
      piVar5 = param_1;
      iVar7 = -1;
      do {
        if (*piVar5 == iVar2 + (int)param_1) {
          iVar6 = iVar3;
        }
        if ((int *)piVar5[100] == param_1) {
          iVar7 = iVar3;
        }
        iVar3 = iVar3 + 1;
        piVar5 = piVar5 + 1;
      } while (iVar3 < iVar1);
    }
    if (iVar6 == -1) {
      if (iVar7 != -1) {
        param_1[iVar7 + 100] = iVar2 + (int)param_1;
        return;
      }
    }
    else if (iVar7 != -1) {
      param_1[iVar7 + 100] = param_1[iVar6 + 100];
      piVar5 = param_1 + iVar6 + 1;
      piVar8 = param_1 + iVar6;
      for (uVar4 = 99U - iVar6 & 0x3fffffff; uVar4 != 0; uVar4 = uVar4 - 1) {
        *piVar8 = *piVar5;
        piVar5 = piVar5 + 1;
        piVar8 = piVar8 + 1;
      }
      for (iVar7 = 0; iVar7 != 0; iVar7 = iVar7 + -1) {
        *(char *)piVar8 = (char)*piVar5;
        piVar5 = (int *)((int)piVar5 + 1);
        piVar8 = (int *)((int)piVar8 + 1);
      }
      piVar5 = param_1 + iVar6 + 0x65;
      piVar8 = param_1 + iVar6 + 100;
      for (uVar4 = 99U - iVar6 & 0x3fffffff; uVar4 != 0; uVar4 = uVar4 - 1) {
        *piVar8 = *piVar5;
        piVar5 = piVar5 + 1;
        piVar8 = piVar8 + 1;
      }
      for (iVar6 = 0; iVar6 != 0; iVar6 = iVar6 + -1) {
        *(char *)piVar8 = (char)*piVar5;
        piVar5 = (int *)((int)piVar5 + 1);
        piVar8 = (int *)((int)piVar8 + 1);
      }
      param_1[200] = param_1[200] + -1;
      return;
    }
    if (iVar6 != -1) {
      param_1[iVar6] = (int)param_1;
      return;
    }
    param_1[iVar1] = (int)param_1;
    param_1[param_1[200] + 100] = iVar2 + (int)param_1;
    param_1[200] = param_1[200] + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_100027b0 @ 100027b0 */
/* ===================================================== */

void __fastcall FUN_100027b0(int *param_1)

{
  int iVar1;
  int unaff_EBX;
  
  iVar1 = grTexMinAddress(0);
  *param_1 = iVar1;
  iVar1 = grTexMaxAddress(0);
  param_1[100] = iVar1;
  param_1[200] = 1;
  if ((int)(param_1[100] + (param_1[100] >> 0x1f & 0x1fffffU)) >> 0x15 !=
      (int)(*param_1 + (*param_1 >> 0x1f & 0x1fffffU)) >> 0x15) {
    do {
      param_1[param_1[200] + 100] = param_1[param_1[200] + 99];
      param_1[param_1[200] + 99] =
           ((int)(param_1[param_1[200] + -1] + (param_1[param_1[200] + -1] >> 0x1f & 0x1fffffU)) >>
           0x15) * 0x200000 + 0x1ffff8;
      param_1[param_1[200]] = param_1[param_1[200] + 99] + 8;
      iVar1 = param_1[200];
      param_1[200] = iVar1 + 1;
    } while ((int)(param_1[iVar1 + 100] + (param_1[iVar1 + 100] >> 0x1f & 0x1fffffU)) >> 0x15 !=
             (int)(param_1[iVar1] + (param_1[iVar1] >> 0x1f & 0x1fffffU)) >> 0x15);
  }
  if (unaff_EBX != 0) {
    param_1 = param_1 + 0xc9;
    iVar1 = 200;
    do {
      if (*param_1 != 0) {
        *(undefined4 *)(*param_1 + 0x20) = 0xffffffff;
        (**(code **)(*(int *)*param_1 + 0x14))();
      }
      param_1 = param_1 + 1;
      iVar1 = iVar1 + -1;
    } while (iVar1 != 0);
  }
  return;
}



/* ===================================================== */
/* FUN_100028c0 @ 100028c0 */
/* ===================================================== */

void __thiscall FUN_100028c0(int param_1,undefined4 param_2)

{
  int iVar1;
  int *piVar2;
  
  iVar1 = 0;
  piVar2 = (int *)(param_1 + 0x324);
  do {
    if (*piVar2 == 0) {
      *(undefined4 *)(param_1 + 0x324 + iVar1 * 4) = param_2;
      return;
    }
    iVar1 = iVar1 + 1;
    piVar2 = piVar2 + 1;
  } while (iVar1 < 200);
  return;
}



/* ===================================================== */
/* FUN_100028f0 @ 100028f0 */
/* ===================================================== */

void __thiscall FUN_100028f0(int param_1,int param_2)

{
  int iVar1;
  int *piVar2;
  
  iVar1 = 0;
  piVar2 = (int *)(param_1 + 0x324);
  do {
    if (param_2 == *piVar2) {
      *(undefined4 *)(param_1 + 0x324 + iVar1 * 4) = 0;
      return;
    }
    iVar1 = iVar1 + 1;
    piVar2 = piVar2 + 1;
  } while (iVar1 < 200);
  return;
}



/* ===================================================== */
/* grSstControl @ 10002920 */
/* ===================================================== */

void grSstControl(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002920. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grSstControl();
  return;
}



/* ===================================================== */
/* grSstQueryBoards @ 10002926 */
/* ===================================================== */

void grSstQueryBoards(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002926. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grSstQueryBoards();
  return;
}



/* ===================================================== */
/* grChromakeyValue @ 1000292c */
/* ===================================================== */

void grChromakeyValue(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000292c. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grChromakeyValue();
  return;
}



/* ===================================================== */
/* grChromakeyMode @ 10002932 */
/* ===================================================== */

void grChromakeyMode(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002932. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grChromakeyMode();
  return;
}



/* ===================================================== */
/* grTexCombine @ 10002938 */
/* ===================================================== */

void grTexCombine(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002938. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexCombine();
  return;
}



/* ===================================================== */
/* grDepthMask @ 1000293e */
/* ===================================================== */

void grDepthMask(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000293e. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grDepthMask();
  return;
}



/* ===================================================== */
/* grColorMask @ 10002944 */
/* ===================================================== */

void grColorMask(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002944. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grColorMask();
  return;
}



/* ===================================================== */
/* grDepthBufferMode @ 1000294a */
/* ===================================================== */

void grDepthBufferMode(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000294a. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grDepthBufferMode();
  return;
}



/* ===================================================== */
/* grSstWinOpen @ 10002950 */
/* ===================================================== */

void grSstWinOpen(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002950. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grSstWinOpen();
  return;
}



/* ===================================================== */
/* grSstSelect @ 10002956 */
/* ===================================================== */

void grSstSelect(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002956. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grSstSelect();
  return;
}



/* ===================================================== */
/* grSstQueryHardware @ 1000295c */
/* ===================================================== */

void grSstQueryHardware(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000295c. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grSstQueryHardware();
  return;
}



/* ===================================================== */
/* grGlideInit @ 10002962 */
/* ===================================================== */

void grGlideInit(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002962. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grGlideInit();
  return;
}



/* ===================================================== */
/* grGlideShutdown @ 10002968 */
/* ===================================================== */

void grGlideShutdown(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002968. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grGlideShutdown();
  return;
}



/* ===================================================== */
/* grBufferClear @ 1000296e */
/* ===================================================== */

void grBufferClear(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000296e. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grBufferClear();
  return;
}



/* ===================================================== */
/* grClipWindow @ 10002974 */
/* ===================================================== */

void grClipWindow(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002974. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grClipWindow();
  return;
}



/* ===================================================== */
/* grTexFilterMode @ 1000297a */
/* ===================================================== */

void grTexFilterMode(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000297a. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexFilterMode();
  return;
}



/* ===================================================== */
/* guAlphaSource @ 10002980 */
/* ===================================================== */

void guAlphaSource(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002980. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  guAlphaSource();
  return;
}



/* ===================================================== */
/* guColorCombineFunction @ 10002986 */
/* ===================================================== */

void guColorCombineFunction(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002986. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  guColorCombineFunction();
  return;
}



/* ===================================================== */
/* grColorCombine @ 1000298c */
/* ===================================================== */

void grColorCombine(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000298c. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grColorCombine();
  return;
}



/* ===================================================== */
/* grAlphaBlendFunction @ 10002992 */
/* ===================================================== */

void grAlphaBlendFunction(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002992. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grAlphaBlendFunction();
  return;
}



/* ===================================================== */
/* grSstIdle @ 10002998 */
/* ===================================================== */

void grSstIdle(void)

{
                    /* WARNING: Could not recover jumptable at 0x10002998. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grSstIdle();
  return;
}



/* ===================================================== */
/* grBufferSwap @ 1000299e */
/* ===================================================== */

void grBufferSwap(void)

{
                    /* WARNING: Could not recover jumptable at 0x1000299e. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grBufferSwap();
  return;
}



/* ===================================================== */
/* guDrawTriangleWithClip @ 100029a4 */
/* ===================================================== */

void guDrawTriangleWithClip(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029a4. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  guDrawTriangleWithClip();
  return;
}



/* ===================================================== */
/* grDrawLine @ 100029aa */
/* ===================================================== */

void grDrawLine(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029aa. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grDrawLine();
  return;
}



/* ===================================================== */
/* grDrawPoint @ 100029b0 */
/* ===================================================== */

void grDrawPoint(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029b0. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grDrawPoint();
  return;
}



/* ===================================================== */
/* grTexDownloadTable @ 100029b6 */
/* ===================================================== */

void grTexDownloadTable(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029b6. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexDownloadTable();
  return;
}



/* ===================================================== */
/* grTexSource @ 100029bc */
/* ===================================================== */

void grTexSource(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029bc. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexSource();
  return;
}



/* ===================================================== */
/* grTexTextureMemRequired @ 100029c2 */
/* ===================================================== */

void grTexTextureMemRequired(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029c2. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexTextureMemRequired();
  return;
}



/* ===================================================== */
/* grTexDownloadMipMap @ 100029c8 */
/* ===================================================== */

void grTexDownloadMipMap(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029c8. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexDownloadMipMap();
  return;
}



/* ===================================================== */
/* grTexMaxAddress @ 100029ce */
/* ===================================================== */

void grTexMaxAddress(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029ce. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexMaxAddress();
  return;
}



/* ===================================================== */
/* grTexMinAddress @ 100029d4 */
/* ===================================================== */

void grTexMinAddress(void)

{
                    /* WARNING: Could not recover jumptable at 0x100029d4. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  grTexMinAddress();
  return;
}


