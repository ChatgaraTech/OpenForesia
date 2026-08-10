/* ===================================================== */
/* OpenForesia - cibles de la vtable du device 3dhelp     */
/* Program: 3dhelp.dll                                    */
/* ===================================================== */

/* ===================================================== */
/* FUN_0ffe1740 @ 0ffe1740  (28 octets) */
/* ===================================================== */


undefined4 * __thiscall FUN_0ffe1740(void *this,byte param_1)

{
  FUN_0ffe18b0(this);
  if ((param_1 & 1) != 0) {
    Free(this);
  }
  return this;
}



/* ===================================================== */
/* FUN_0ffe1760 @ 0ffe1760  (30 octets) */
/* ===================================================== */


undefined4 * __thiscall FUN_0ffe1760(undefined4 *param_1,byte param_2)

{
  *param_1 = &PTR_FUN_0ffe60f0;
  if ((param_2 & 1) != 0) {
    Free(param_1);
  }
  return param_1;
}



/* ===================================================== */
/* FUN_0ffe1780 @ 0ffe1780  (8 octets) */
/* ===================================================== */


undefined4 FUN_0ffe1780(void)

{
  return 0x80004001;
}



/* ===================================================== */
/* FUN_0ffe1790 @ 0ffe1790  (7 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe1790(int param_1)

{
  return *(undefined4 *)(param_1 + 0xb4);
}



/* ===================================================== */
/* FUN_0ffe17a0 @ 0ffe17a0  (7 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe17a0(int param_1)

{
  return *(undefined4 *)(param_1 + 0xb8);
}



/* ===================================================== */
/* FUN_0ffe17b0 @ 0ffe17b0  (13 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe17b0(int param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0xa0) = param_2;
  return;
}



/* ===================================================== */
/* FUN_0ffe17c0 @ 0ffe17c0  (7 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe17c0(int param_1)

{
  return *(undefined4 *)(param_1 + 0xa0);
}



/* ===================================================== */
/* FUN_0ffe17d0 @ 0ffe17d0  (7 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe17d0(int param_1)

{
  return *(undefined4 *)(param_1 + 0xa4);
}



/* ===================================================== */
/* FUN_0ffe1800 @ 0ffe1800  (10 octets) */
/* ===================================================== */


int __fastcall FUN_0ffe1800(int param_1)

{
  return *(int *)(param_1 + 0x9c) + 0x1c;
}



/* ===================================================== */
/* FUN_0ffe1810 @ 0ffe1810  (12 octets) */
/* ===================================================== */


int __fastcall FUN_0ffe1810(int param_1)

{
  return *(int *)(param_1 + 0x9c) + 0x118;
}



/* ===================================================== */
/* FUN_0ffe1820 @ 0ffe1820  (22 octets) */
/* ===================================================== */


void __fastcall FUN_0ffe1820(int *param_1)

{
  if (param_1[0x29] != 0) {
    (**(code **)(*param_1 + 0x44))();
    return;
  }
  (**(code **)(*param_1 + 0x48))();
  return;
}



/* ===================================================== */
/* FUN_0ffe1840 @ 0ffe1840  (37 octets) */
/* ===================================================== */


undefined4 __thiscall FUN_0ffe1840(int param_1,undefined4 param_2)

{
  *(undefined4 *)(param_1 + 0xa0) = 0;
  *(undefined4 *)(param_1 + 0xb8) = 0;
  *(undefined4 *)(param_1 + 0xb4) = 0;
  *(undefined4 *)(param_1 + 0x1c) = param_2;
  FUN_0ffe53a0();
  return 0;
}



/* ===================================================== */
/* FUN_0ffe1870 @ 0ffe1870  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1870(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x60) + 0x44))(*(int **)(param_1 + 0x60),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1890 @ 0ffe1890  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1890(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x60) + 0x14))(*(int **)(param_1 + 0x60),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1900 @ 0ffe1900  (38 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1900(int param_1,int *param_2)

{
  FUN_0ffe3280(*(void **)(param_1 + 0xac),param_2);
  FUN_0ffe3200(*(void **)(param_1 + 0xac),*(int **)(param_1 + 0x5c));
  return;
}



/* ===================================================== */
/* FUN_0ffe1930 @ 0ffe1930  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1930(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x68))(*(int **)(param_1 + 0x5c),2,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1950 @ 0ffe1950  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1950(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x68))(*(int **)(param_1 + 0x5c),3,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1970 @ 0ffe1970  (29 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe1970(undefined4 param_1)

{
  void *this;
  undefined4 uVar1;
  
  this = (void *)Malloc(0x20);
  if (this != (void *)0x0) {
    uVar1 = FUN_0ffe2af0(this,param_1);
    return uVar1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe1990 @ 0ffe1990  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1990(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x68))(*(int **)(param_1 + 0x5c),1,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe19b0 @ 0ffe19b0  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe19b0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 100))(*(int **)(param_1 + 0x5c),2,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe19d0 @ 0ffe19d0  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe19d0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),3,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1a10 @ 0ffe1a10  (39 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1a10(int param_1,undefined4 param_2)

{
  undefined4 unaff_ESI;
  
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),5,param_2);
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),6,unaff_ESI);
  return;
}



/* ===================================================== */
/* FUN_0ffe1a40 @ 0ffe1a40  (130 octets) */
/* ===================================================== */


undefined4 FUN_0ffe1a40(void)

{
  LPSTR _Source;
  LPCSTR lpString1;
  int iVar1;
  byte abStack_100 [255];
  undefined1 uStack_1;
  
  _Source = GetCommandLineA();
  _strncpy((char *)abStack_100,_Source,0xff);
  uStack_1 = 0;
  lpString1 = (LPCSTR)FUN_0ffe5600(abStack_100,&DAT_0ffe802c);
  if (lpString1 == (LPCSTR)0x0) {
    return 0;
  }
  do {
    iVar1 = lstrcmpiA(lpString1,s_emulation_0ffe8020);
    if (iVar1 == 0) {
      return 1;
    }
    lpString1 = (LPCSTR)FUN_0ffe5600((byte *)0x0,&DAT_0ffe802c);
  } while (lpString1 != (LPCSTR)0x0);
  return 0;
}



/* ===================================================== */
/* FUN_0ffe1ad0 @ 0ffe1ad0  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1ad0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),9,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1af0 @ 0ffe1af0  (49 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1af0(int param_1,uint param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))
            (*(int **)(param_1 + 0x5c),0x11,2 - (uint)((param_2 & 1) != 0));
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x12,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1b30 @ 0ffe1b30  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1b30(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x15,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1b50 @ 0ffe1b50  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1b50(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x1b,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1b70 @ 0ffe1b70  (37 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1b70(void *this,undefined4 param_1)

{
  (**(code **)(**(int **)((int)this + 0x5c) + 0x5c))(*(int **)((int)this + 0x5c),0x21,param_1);
  (**(code **)(**(int **)((int)this + 0x5c) + 0x5c))(*(int **)((int)this + 0x5c),0x27,param_1);
  return;
}



/* ===================================================== */
/* FUN_0ffe1ba0 @ 0ffe1ba0  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1ba0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x13,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1bc0 @ 0ffe1bc0  (19 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1bc0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),0x14,param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1be0 @ 0ffe1be0  (22 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1be0(int param_1,undefined4 param_2,undefined4 param_3)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x5c))(*(int **)(param_1 + 0x5c),param_2,param_3);
  return;
}



/* ===================================================== */
/* FUN_0ffe1c00 @ 0ffe1c00  (22 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1c00(int param_1,undefined4 param_2,undefined4 param_3)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 100))(*(int **)(param_1 + 0x5c),param_2,param_3);
  return;
}



/* ===================================================== */
/* FUN_0ffe1c20 @ 0ffe1c20  (185 octets) */
/* ===================================================== */


undefined4 FUN_0ffe1c20(undefined4 *param_1)

{
  HMODULE hInstance;
  HWND pHVar1;
  LPCSTR lpCursorName;
  WNDCLASSA WStack_28;
  
  WStack_28.style = 3;
  WStack_28.lpfnWndProc = (WNDPROC)&LAB_0ffe1660;
  WStack_28.cbClsExtra = 0;
  WStack_28.cbWndExtra = 0;
  WStack_28.hInstance = GetModuleHandleA((LPCSTR)0x0);
  WStack_28.hIcon = LoadIconA(WStack_28.hInstance,(LPCSTR)0x0);
  lpCursorName = (LPCSTR)0xa;
  hInstance = GetModuleHandleA(s_WSYSKERN_DLL_0ffe8044);
  WStack_28.hCursor = LoadCursorA(hInstance,lpCursorName);
  WStack_28.hbrBackground = GetStockObject(4);
  WStack_28.lpszMenuName = (LPCSTR)0x0;
  WStack_28.lpszClassName = s_Example_0ffe803c;
  RegisterClassA(&WStack_28);
  pHVar1 = CreateWindowExA(8,s_Example_0ffe803c,s_Spieltitel_0ffe8030,0x80000000,0,0,0x280,0x1e0,
                           (HWND)0x0,(HMENU)0x0,WStack_28.hInstance,(LPVOID)0x0);
  *param_1 = pHVar1;
  return 0;
}



/* ===================================================== */
/* FUN_0ffe1ce0 @ 0ffe1ce0  (37 octets) */
/* ===================================================== */


void __thiscall
FUN_0ffe1ce0(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,undefined4 param_5
            ,undefined4 param_6)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x74))
            (*(int **)(param_1 + 0x5c),param_2,param_3,param_4,param_5,param_6);
  return;
}



/* ===================================================== */
/* FUN_0ffe1d10 @ 0ffe1d10  (47 octets) */
/* ===================================================== */


void __thiscall
FUN_0ffe1d10(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,undefined4 param_5
            ,undefined4 param_6,undefined4 param_7,undefined4 param_8)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x78))
            (*(int **)(param_1 + 0x5c),param_2,param_3,param_4,param_5,param_6,param_7,param_8);
  return;
}



/* ===================================================== */
/* FUN_0ffe1d40 @ 0ffe1d40  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1d40(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x54) + 0x44))(*(int **)(param_1 + 0x54),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1d60 @ 0ffe1d60  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1d60(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x54) + 0x68))(*(int **)(param_1 + 0x54),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1d80 @ 0ffe1d80  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1d80(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x40) + 0x44))(*(int **)(param_1 + 0x40),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1da0 @ 0ffe1da0  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1da0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x40) + 0x68))(*(int **)(param_1 + 0x40),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1dc0 @ 0ffe1dc0  (17 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1dc0(int param_1,undefined4 param_2)

{
  (**(code **)(**(int **)(param_1 + 0x40) + 0x54))(*(int **)(param_1 + 0x40),param_2);
  return;
}



/* ===================================================== */
/* FUN_0ffe1de0 @ 0ffe1de0  (22 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1de0(int param_1,undefined4 param_2,undefined4 param_3)

{
  (**(code **)(**(int **)(param_1 + 0x34) + 0x58))(*(int **)(param_1 + 0x34),param_2,param_3);
  return;
}



/* ===================================================== */
/* FUN_0ffe1e00 @ 0ffe1e00  (22 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1e00(int param_1,undefined4 param_2,undefined4 param_3)

{
  (**(code **)(**(int **)(param_1 + 0x34) + 0x2c))(*(int **)(param_1 + 0x34),param_2,param_3);
  return;
}



/* ===================================================== */
/* FUN_0ffe1e20 @ 0ffe1e20  (4 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe1e20(int param_1)

{
  return *(undefined4 *)(param_1 + 0x34);
}



/* ===================================================== */
/* FUN_0ffe1e30 @ 0ffe1e30  (4 octets) */
/* ===================================================== */


undefined4 __fastcall FUN_0ffe1e30(int param_1)

{
  return *(undefined4 *)(param_1 + 0x5c);
}



/* ===================================================== */
/* FUN_0ffe1e40 @ 0ffe1e40  (420 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe1e40(int param_1,int param_2,int param_3)

{
  undefined4 uVar1;
  byte *pbVar2;
  void *pvVar3;
  int iVar4;
  byte *pbVar5;
  uint uVar6;
  int iStack_a4;
  undefined4 auStack_a0 [40];
  
  pbVar5 = DAT_0ffe810c;
  if ((param_2 != 0) && (uVar6 = 0, DAT_0ffe8108 != 0)) {
    do {
      pbVar2 = FUN_0ffe5390(pbVar5);
      if (pbVar2 == (byte *)0x0) {
        pvVar3 = FUN_0ffe50c0(pbVar5,&DAT_0ffe6600,(int *)0x0,(void *)0x0);
        if (pvVar3 == (void *)0x0) {
          pvVar3 = FUN_0ffe50c0(pbVar5,&DAT_0ffe65d0,(int *)0x0,(void *)0x0);
        }
        *(byte **)(param_1 + 0x98) = pbVar5;
        *(void **)(param_1 + 0x9c) = pvVar3;
        return;
      }
      pbVar5 = *(byte **)(pbVar5 + 0x330);
      uVar6 = uVar6 + 1;
    } while (uVar6 < DAT_0ffe8108);
  }
  uVar6 = 0;
  iStack_a4 = 0;
  if (DAT_0ffe8108 != 0) {
    do {
      pvVar3 = FUN_0ffe50c0(pbVar5,&DAT_0ffe65f0,(int *)0x0,(void *)0x0);
      if ((pvVar3 != (void *)0x0) ||
         ((param_3 != 0 &&
          (pvVar3 = FUN_0ffe50c0(pbVar5,&DAT_0ffe6600,(int *)0x0,(void *)0x0), pvVar3 != (void *)0x0
          )))) {
        auStack_a0[iStack_a4 * 2] = pbVar5;
        auStack_a0[iStack_a4 * 2 + 1] = pvVar3;
        iStack_a4 = iStack_a4 + 1;
      }
      pbVar5 = *(byte **)(pbVar5 + 0x330);
      uVar6 = uVar6 + 1;
    } while (uVar6 < DAT_0ffe8108);
  }
  if (iStack_a4 < 2) {
    if (iStack_a4 != 1) {
      pbVar5 = FUN_0ffe5550((char *)0x0,(undefined4 *)0x0,(byte *)0x0);
      *(byte **)(param_1 + 0x98) = pbVar5;
      pvVar3 = FUN_0ffe50c0(pbVar5,&DAT_0ffe6600,(int *)0x0,(void *)0x0);
      *(void **)(param_1 + 0x9c) = pvVar3;
      if (pvVar3 == (void *)0x0) {
        pvVar3 = FUN_0ffe50c0(*(void **)(param_1 + 0x98),&DAT_0ffe65d0,(int *)0x0,(void *)0x0);
        *(void **)(param_1 + 0x9c) = pvVar3;
      }
      if (*(int *)(param_1 + 0x98) != 0) {
        return;
      }
      MessageBoxA(*(HWND *)(param_1 + 0x1c),s_DirectDraw_initialization_failed_0ffe8054,
                  s_Fatal_error_0ffe8078,0x10);
                    /* WARNING: Subroutine does not return */
      ExitProcess(3);
    }
    iVar4 = 0;
  }
  else {
    iVar4 = FUN_0ffe1000(*(undefined4 *)(param_1 + 0x1c),&iStack_a4);
  }
  uVar1 = auStack_a0[iVar4 * 2 + 1];
  *(undefined4 *)(param_1 + 0x98) = auStack_a0[iVar4 * 2];
  *(undefined4 *)(param_1 + 0x9c) = uVar1;
  return;
}



/* ===================================================== */
/* FUN_0ffe20c0 @ 0ffe20c0  (38 octets) */
/* ===================================================== */


int * __fastcall FUN_0ffe20c0(int param_1)

{
  int *piVar1;
  undefined4 uStack_4;
  
  piVar1 = *(int **)(param_1 + 0x34);
  uStack_4 = 0;
  (**(code **)(*piVar1 + 0x20))(piVar1,0,0,&uStack_4,&LAB_0ffe1ff0);
  return piVar1;
}



/* ===================================================== */
/* FUN_0ffe20f0 @ 0ffe20f0  (785 octets) */
/* ===================================================== */


undefined4 __thiscall FUN_0ffe20f0(int *param_1,int param_2,int param_3,int param_4)

{
  int iVar1;
  byte *pbVar2;
  uint uVar3;
  undefined4 *puVar4;
  int iVar5;
  byte *pbVar6;
  char *pcVar7;
  char *pcVar8;
  bool bVar9;
  bool bVar10;
  int *piStack_138;
  undefined4 uStack_134;
  undefined4 uStack_130;
  int *piStack_12c;
  undefined4 uStack_128;
  uint uStack_124;
  int *piStack_120;
  undefined1 *puStack_11c;
  undefined4 uStack_118;
  undefined4 uStack_114;
  int iVar11;
  undefined4 uVar12;
  undefined1 auStack_98 [4];
  undefined1 auStack_94 [64];
  undefined4 uStack_54;
  
  if ((param_1[0xd] == 0) && (param_1[0xf] == 0)) {
    param_1[0x31] = param_4;
    iVar5 = 0x10;
    bVar9 = false;
    iVar1 = 0;
    bVar10 = true;
    param_1[0x30] = param_3;
    pbVar2 = (byte *)(param_1[0x27] + 4);
    pbVar6 = &DAT_0ffe65f0;
    do {
      if (iVar5 == 0) break;
      iVar5 = iVar5 + -1;
      bVar9 = *pbVar2 < *pbVar6;
      bVar10 = *pbVar2 == *pbVar6;
      pbVar2 = pbVar2 + 1;
      pbVar6 = pbVar6 + 1;
    } while (bVar10);
    if (!bVar10) {
      iVar1 = (1 - (uint)bVar9) - (uint)(bVar9 != 0);
    }
    param_1[0x29] = (uint)(iVar1 == 0);
    param_1[0x2a] = param_2;
    ShowWindow((HWND)param_1[7],5);
    pbVar6 = (byte *)(param_1[0x27] + 4);
    uVar12 = 0;
    iVar11 = 0x10;
    iVar1 = param_3;
    iVar5 = param_4;
    pbVar2 = FUN_0ffe5390((byte *)param_1[0x26]);
    uStack_114 = 0xffe21a9;
    uVar3 = FUN_0ffe3310(param_1 + 1,(HWND)param_1[7],pbVar2,iVar1,iVar5,iVar11,uVar12,pbVar6,
                         param_2);
    if (-1 < (int)uVar3) {
      SetWindowPos((HWND)param_1[7],(HWND)0x0,0,0,param_3,param_4,4);
      DAT_0ffe80c0 = param_1[0xe];
      DAT_0ffe80c4 = param_1[0xf];
      uStack_54 = 0;
      (**(code **)(*(int *)param_1[0x10] + 0x14))();
      piStack_120 = (int *)param_1[0x15];
      uStack_114 = 0;
      puStack_11c = &stack0xffffff1c;
      uStack_118 = 0;
      uStack_124 = 0xffe2227;
      (**(code **)(*piStack_120 + 0x14))();
      iVar1 = 0x10;
      bVar9 = true;
      pcVar7 = (char *)(param_1[0x27] + 4);
      pcVar8 = &DAT_0ffe65f0;
      do {
        if (iVar1 == 0) break;
        iVar1 = iVar1 + -1;
        bVar9 = *pcVar7 == *pcVar8;
        pcVar7 = pcVar7 + 1;
        pcVar8 = pcVar8 + 1;
      } while (bVar9);
      piStack_12c = (int *)param_1[0x17];
      uStack_124 = (uint)bVar9;
      iVar1 = *piStack_12c;
      uStack_128 = 0x1a;
      param_1[0x29] = uStack_124;
      uStack_130 = 0xffe2253;
      (**(code **)(iVar1 + 0x5c))();
      piStack_138 = (int *)param_1[0x17];
      uStack_130 = 0;
      uStack_134 = 0x1d;
      (**(code **)(*piStack_138 + 0x5c))();
      (**(code **)(*(int *)param_1[0x17] + 0x5c))((int *)param_1[0x17],0x29,1);
      if (param_1[0x16] != 0) {
        (**(code **)(*(int *)param_1[0x17] + 0x5c))((int *)param_1[0x17],7,1);
        (**(code **)(*(int *)param_1[0x17] + 0x5c))((int *)param_1[0x17],0xe,1);
      }
      iVar1 = *param_1;
      uVar12 = ProjectionMatrix(auStack_94,0x3f800000,0x43fa0000,0x3fc90e56);
      (**(code **)(iVar1 + 0x78))(uVar12);
      uStack_114 = 0;
      piStack_138 = (int *)0x42c80000;
      uStack_134 = 0;
      uStack_130 = 0;
      uVar12 = ViewMatrix(auStack_98,&piStack_138,&uStack_114,&stack0xfffffef8,0);
      (**(code **)(iVar1 + 0x70))(uVar12);
      SetWindowLongA((HWND)param_1[7],-0x15,(LONG)param_1);
      (**(code **)(iVar1 + 0x38))(1);
      iVar1 = FUN_0ffe2c40((int)param_1);
      param_1[0x2d] = iVar1;
      iVar1 = FUN_0ffe10e0((int)param_1,4);
      param_1[0x2e] = iVar1;
      iVar1 = FUN_0ffe10e0((int)param_1,1);
      param_1[0x2f] = iVar1;
      puVar4 = FUN_0ffe19f0(param_1);
      param_1[0x2c] = (int)puVar4;
      FUN_0ffe3230(puVar4,(int *)param_1[0x18]);
      puVar4 = FUN_0ffe19f0(param_1);
      param_1[0x2b] = (int)puVar4;
      FUN_0ffe3200(puVar4,(int *)param_1[0x17]);
      iVar1 = param_1[0x2b];
      *(undefined4 *)(iVar1 + 0xc) = 0x3fc00000;
      *(undefined4 *)(iVar1 + 0x5c) = 1;
      *(undefined4 *)(iVar1 + 0x10) = 0x3fc00000;
      *(undefined4 *)(iVar1 + 0x14) = 0x3fc00000;
      *(undefined4 *)(iVar1 + 0x18) = 0x3f800000;
      return 1;
    }
    FUN_0ffe34a0((int)(param_1 + 1));
  }
  return 0;
}



/* ===================================================== */
/* FUN_0ffe2410 @ 0ffe2410  (689 octets) */
/* ===================================================== */


undefined4 __thiscall FUN_0ffe2410(int *param_1,int param_2,int param_3)

{
  byte *pbVar1;
  byte *pbVar2;
  uint uVar3;
  undefined4 *puVar4;
  int iVar5;
  char *pcVar6;
  char *pcVar7;
  bool bVar8;
  undefined4 uStack_138;
  int *piStack_134;
  undefined4 uStack_130;
  undefined4 uStack_12c;
  int *piStack_128;
  undefined4 uStack_124;
  uint uStack_120;
  int *piStack_11c;
  undefined1 *puStack_118;
  undefined4 uStack_114;
  undefined4 uStack_110;
  undefined4 uStack_10c;
  int iVar9;
  int iVar10;
  int iVar11;
  undefined4 uVar12;
  undefined1 auStack_98 [4];
  undefined1 auStack_94 [64];
  undefined4 uStack_54;
  
  ShowWindow((HWND)param_1[7],5);
  iVar5 = param_1[0x2a];
  pbVar1 = (byte *)(param_1[0x27] + 4);
  uVar12 = 0;
  iVar11 = 0x10;
  iVar9 = param_2;
  iVar10 = param_3;
  pbVar2 = FUN_0ffe5390((byte *)param_1[0x26]);
  uStack_10c = 0xffe2468;
  uVar3 = FUN_0ffe33f0(param_1 + 1,pbVar2,iVar9,iVar10,iVar11,uVar12,pbVar1,iVar5);
  if ((int)uVar3 < 0) {
    FUN_0ffe34a0((int)(param_1 + 1));
    return 0;
  }
  uStack_10c = 0xffe2484;
  SetWindowPos((HWND)param_1[7],(HWND)0x0,0,0,param_2,param_3,4);
  uStack_54 = 0;
  (**(code **)(*(int *)param_1[0x10] + 0x14))();
  piStack_11c = (int *)param_1[0x15];
  uStack_10c = 0x1000400;
  uStack_110 = 0;
  puStack_118 = &stack0xffffff1c;
  uStack_114 = 0;
  uStack_120 = 0xffe24e7;
  (**(code **)(*piStack_11c + 0x14))();
  iVar5 = 0x10;
  bVar8 = true;
  pcVar6 = (char *)(param_1[0x27] + 4);
  pcVar7 = &DAT_0ffe65f0;
  do {
    if (iVar5 == 0) break;
    iVar5 = iVar5 + -1;
    bVar8 = *pcVar6 == *pcVar7;
    pcVar6 = pcVar6 + 1;
    pcVar7 = pcVar7 + 1;
  } while (bVar8);
  piStack_128 = (int *)param_1[0x17];
  uStack_120 = (uint)bVar8;
  iVar5 = *piStack_128;
  uStack_124 = 0x1a;
  param_1[0x29] = uStack_120;
  uStack_12c = 0xffe2515;
  (**(code **)(iVar5 + 0x5c))();
  piStack_134 = (int *)param_1[0x17];
  uStack_12c = 0;
  uStack_130 = 0x1d;
  uStack_138 = 0xffe2522;
  (**(code **)(*piStack_134 + 0x5c))();
  uStack_138 = 1;
  (**(code **)(*(int *)param_1[0x17] + 0x5c))((int *)param_1[0x17],0x29);
  if (param_1[0x16] != 0) {
    (**(code **)(*(int *)param_1[0x17] + 0x5c))((int *)param_1[0x17],7,1);
    (**(code **)(*(int *)param_1[0x17] + 0x5c))((int *)param_1[0x17],0xe,1);
  }
  iVar5 = *param_1;
  uVar12 = ProjectionMatrix(auStack_94,0x3f800000,0x43fa0000,0x3fc90e56);
  (**(code **)(iVar5 + 0x78))(uVar12);
  uStack_114 = 0;
  uStack_110 = 0;
  uStack_10c = 0;
  uStack_138 = 0x42c80000;
  piStack_134 = (int *)0x0;
  uStack_130 = 0;
  uVar12 = ViewMatrix(auStack_98,&uStack_138,&uStack_114,&stack0xfffffef8,0);
  (**(code **)(iVar5 + 0x70))(uVar12);
  SetWindowLongA((HWND)param_1[7],-0x15,(LONG)param_1);
  (**(code **)(iVar5 + 0x38))(1);
  iVar5 = FUN_0ffe2c40((int)param_1);
  param_1[0x2d] = iVar5;
  iVar5 = FUN_0ffe10e0((int)param_1,4);
  param_1[0x2e] = iVar5;
  iVar5 = FUN_0ffe10e0((int)param_1,1);
  param_1[0x2f] = iVar5;
  puVar4 = FUN_0ffe19f0(param_1);
  param_1[0x2c] = (int)puVar4;
  FUN_0ffe3230(puVar4,(int *)param_1[0x18]);
  puVar4 = FUN_0ffe19f0(param_1);
  param_1[0x2b] = (int)puVar4;
  FUN_0ffe3200(puVar4,(int *)param_1[0x17]);
  iVar5 = param_1[0x2b];
  *(undefined4 *)(iVar5 + 0xc) = 0x3fc00000;
  *(undefined4 *)(iVar5 + 0x5c) = 1;
  *(undefined4 *)(iVar5 + 0x10) = 0x3fc00000;
  *(undefined4 *)(iVar5 + 0x14) = 0x3fc00000;
  *(undefined4 *)(iVar5 + 0x18) = 0x3f800000;
  return 1;
}



/* ===================================================== */
/* FUN_0ffe26d0 @ 0ffe26d0  (253 octets) */
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
  FUN_0ffe34a0(param_1 + 4);
  SetWindowLongA(*(HWND *)(param_1 + 0x1c),-0x15,0);
  *(undefined4 *)(param_1 + 0x1c) = 0;
  FUN_0ffe53d0();
  return 1;
}



/* ===================================================== */
/* FUN_0ffe27d0 @ 0ffe27d0  (95 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe27d0(int param_1,undefined4 *param_2,int param_3)

{
  undefined4 uStack_10;
  undefined4 uStack_c;
  undefined4 uStack_8;
  undefined4 uStack_4;
  
  if (*(int *)(param_1 + 0xa8) == 0) {
    return;
  }
  if ((param_3 == 0) || (param_2 == (undefined4 *)0x0)) {
    uStack_4 = *(undefined4 *)(param_1 + 0xc4);
    uStack_c = 0;
    uStack_10 = 0;
    uStack_8 = *(undefined4 *)(param_1 + 0xc0);
    param_3 = 1;
    param_2 = &uStack_10;
  }
  (**(code **)(**(int **)(param_1 + 0x60) + 0x30))(*(int **)(param_1 + 0x60),param_3,param_2,2);
  return;
}



/* ===================================================== */
/* FUN_0ffe2830 @ 0ffe2830  (79 octets) */
/* ===================================================== */


void __thiscall FUN_0ffe2830(int param_1,undefined4 *param_2,int param_3)

{
  undefined4 uStack_10;
  undefined4 uStack_c;
  undefined4 uStack_8;
  undefined4 uStack_4;
  
  if ((param_3 == 0) || (param_2 == (undefined4 *)0x0)) {
    uStack_8 = *(undefined4 *)(param_1 + 0xc0);
    uStack_4 = *(undefined4 *)(param_1 + 0xc4);
    uStack_c = 0;
    uStack_10 = 0;
    param_3 = 1;
    param_2 = &uStack_10;
  }
  (**(code **)(**(int **)(param_1 + 0x60) + 0x30))(*(int **)(param_1 + 0x60),param_3,param_2,1);
  return;
}



/* ===================================================== */
/* FUN_0ffe2880 @ 0ffe2880  (86 octets) */
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
/* FUN_0ffe28e0 @ 0ffe28e0  (10 octets) */
/* ===================================================== */


void __fastcall FUN_0ffe28e0(int param_1)

{
  (**(code **)(**(int **)(param_1 + 0x5c) + 0x2c))(*(int **)(param_1 + 0x5c));
  return;
}



/* ===================================================== */
/* FUN_0ffe2930 @ 0ffe2930  (161 octets) */
/* ===================================================== */


void __fastcall FUN_0ffe2930(int param_1)

{
  int *piVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  
  if (DAT_0ffe80f8 != 0) {
    *(uint *)(DAT_0ffe80f8 + 0x1f48) = (uint)(*(int *)(DAT_0ffe80f8 + 0x1f48) == 0);
    iVar2 = DAT_0ffe80f8;
    iVar4 = 0;
    iVar3 = *(int *)(DAT_0ffe80f8 + 0x1f48);
    if (0 < *(int *)(DAT_0ffe80f8 + 8000 + iVar3 * 4)) {
      do {
        piVar1 = *(int **)(iVar2 + (iVar4 + iVar3 * 1000) * 4);
        (**(code **)(*piVar1 + 8))(piVar1);
        iVar3 = *(int *)(iVar2 + 0x1f48);
        iVar4 = iVar4 + 1;
      } while (iVar4 < *(int *)(iVar2 + 8000 + iVar3 * 4));
    }
    *(undefined4 *)(iVar2 + 8000 + *(int *)(iVar2 + 0x1f48) * 4) = 0;
  }
  piVar1 = *(int **)(param_1 + 0x40);
  if (*(int *)(param_1 + 0xa4) != 0) {
    (**(code **)(*piVar1 + 0x2c))(piVar1,0,1);
    return;
  }
  (**(code **)(*piVar1 + 0x1c))(piVar1,0,0,*(undefined4 *)(param_1 + 0x54),0,0x10);
  return;
}



/* ===================================================== */
/* FUN_0ffe29e0 @ 0ffe29e0  (254 octets) */
/* ===================================================== */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

uint __fastcall FUN_0ffe29e0(int *param_1)

{
  code *pcVar1;
  int *piVar2;
  uint uVar3;
  int iVar4;
  undefined4 *puVar5;
  
  iVar4 = *param_1;
  pcVar1 = *(code **)(iVar4 + 0x5c);
  (*pcVar1)(0,0);
  (**(code **)(iVar4 + 0xb8))();
  (*pcVar1)(0,0);
  if ((param_1[0x17] == 0) || (piVar2 = (int *)param_1[0x18], piVar2 == (int *)0x0)) {
    uVar3 = 0x88762713;
  }
  else {
    puVar5 = (undefined4 *)&stack0xffffffc4;
    for (iVar4 = 0xb; iVar4 != 0; iVar4 = iVar4 + -1) {
      *puVar5 = 0;
      puVar5 = puVar5 + 1;
    }
    uVar3 = (**(code **)(*piVar2 + 0x44))(piVar2,&stack0xffffffc4);
    if (-1 < (int)uVar3) {
      uVar3 = (**(code **)(*(int *)param_1[0x17] + 0x34))((int *)param_1[0x17],param_1[0x18]);
      return uVar3 & (-1 < (int)uVar3) - 1;
    }
  }
  return uVar3;
}



/* ===================================================== */
/* FUN_0ffe2ae0 @ 0ffe2ae0  (12 octets) */
/* ===================================================== */


void FUN_0ffe2ae0(undefined4 param_1)

{
  DAT_0ffe8084 = param_1;
  return;
}



/* ===================================================== */
/* FUN_0ffe3fa0 @ 0ffe3fa0  (8 octets) */
/* ===================================================== */


void FUN_0ffe3fa0(void)

{
                    /* WARNING: Subroutine does not return */
  ExitProcess(1);
}



