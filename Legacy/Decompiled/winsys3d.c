/* ===================================================== */
/* OpenForesia - Ghidra decompilation reference          */
/* Program: winsys3d.dll */
/* ===================================================== */


/* ===================================================== */
/* FUN_00a010d0 @ 00a010d0 */
/* ===================================================== */

undefined4 FUN_00a010d0(char *param_1)

{
  char cVar1;
  HMODULE hModule;
  FARPROC pFVar2;
  undefined4 uVar3;
  INT_PTR IVar4;
  uint uVar5;
  uint uVar6;
  char *pcVar7;
  char *pcVar8;
  undefined4 *puVar9;
  char *pcVar10;
  int iVar11;
  int local_1dc;
  undefined4 local_1d8 [20];
  undefined4 local_188 [20];
  undefined4 local_138 [20];
  undefined4 local_e8 [20];
  char *local_98;
  int local_94 [37];
  
  local_1d8[0] = Strdup(s_Software_00a2c0b8);
  local_188[0] = Strdup(s_Direct3D_Software_Emulation_00a2c09c);
  local_138[0] = 0;
  local_e8[0] = 0;
  FUN_00a017d0(0,local_e8);
  local_1dc = 1;
  DirectDrawEnumerateA(&LAB_00a01810,&local_1dc);
  hModule = LoadLibraryA(s_GLIDE2X_DLL_00a2c090);
  if (hModule != (HMODULE)0x0) {
    pFVar2 = GetProcAddress(hModule,s__grSstQueryBoards_4_00a2c07c);
    (*pFVar2)(local_94);
    FreeLibrary(hModule);
    if (local_94[0] != 0) {
      uVar3 = Strdup(s_Glide_00a2c074);
      local_1d8[local_1dc] = uVar3;
      uVar3 = Strdup(s_3Dfx_Glide_00a2c068);
      local_188[local_1dc] = uVar3;
      local_138[local_1dc] = 0xffffffff;
      local_e8[local_1dc] = 4;
      local_1dc = local_1dc + 1;
    }
  }
  DAT_00a2e8e0 = param_1;
  local_98 = param_1;
  IVar4 = DialogBoxParamA(DAT_00a2e8e8,&DAT_00000066,*(HWND *)param_1,FUN_00a012f0,
                          (LPARAM)&local_1dc);
  if (IVar4 == -1) {
    DeleteObject(*(HGDIOBJ *)(param_1 + 0x11c));
    DeleteObject(*(HGDIOBJ *)(param_1 + 0x120));
                    /* WARNING: Subroutine does not return */
    ExitProcess(0);
  }
  if (local_1dc == 0) {
    uVar5 = 0xffffffff;
    pcVar8 = s_display_00a2c060;
    do {
      pcVar10 = pcVar8;
      if (uVar5 == 0) break;
      uVar5 = uVar5 - 1;
      pcVar10 = pcVar8 + 1;
      cVar1 = *pcVar8;
      pcVar8 = pcVar10;
    } while (cVar1 != '\0');
    uVar5 = ~uVar5;
    pcVar8 = param_1 + 0x18;
    pcVar10 = pcVar10 + -uVar5;
    for (uVar6 = uVar5 >> 2; param_1 = param_1 + 4, uVar6 != 0; uVar6 = uVar6 - 1) {
      *(undefined4 *)param_1 = *(undefined4 *)pcVar10;
      pcVar10 = pcVar10 + 4;
    }
    for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
      *param_1 = *pcVar10;
      pcVar10 = pcVar10 + 1;
      param_1 = param_1 + 1;
    }
    uVar5 = 0xffffffff;
    pcVar10 = s_unknown_00a2c058;
    do {
      pcVar7 = pcVar10;
      if (uVar5 == 0) break;
      uVar5 = uVar5 - 1;
      pcVar7 = pcVar10 + 1;
      cVar1 = *pcVar10;
      pcVar10 = pcVar7;
    } while (cVar1 != '\0');
    uVar5 = ~uVar5;
    pcVar10 = pcVar7 + -uVar5;
    for (uVar6 = uVar5 >> 2; uVar6 != 0; uVar6 = uVar6 - 1) {
      *(undefined4 *)pcVar8 = *(undefined4 *)pcVar10;
      pcVar10 = pcVar10 + 4;
      pcVar8 = pcVar8 + 4;
    }
    for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
      *pcVar8 = *pcVar10;
      pcVar10 = pcVar10 + 1;
      pcVar8 = pcVar8 + 1;
    }
    return 0;
  }
  uVar5 = 0xffffffff;
  pcVar8 = (char *)local_1d8[IVar4];
  do {
    pcVar10 = pcVar8;
    if (uVar5 == 0) break;
    uVar5 = uVar5 - 1;
    pcVar10 = pcVar8 + 1;
    cVar1 = *pcVar8;
    pcVar8 = pcVar10;
  } while (cVar1 != '\0');
  uVar5 = ~uVar5;
  pcVar7 = param_1 + 0x18;
  pcVar8 = pcVar10 + -uVar5;
  for (uVar6 = uVar5 >> 2; param_1 = param_1 + 4, uVar6 != 0; uVar6 = uVar6 - 1) {
    *(undefined4 *)param_1 = *(undefined4 *)pcVar8;
    pcVar8 = pcVar8 + 4;
  }
  for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
    *param_1 = *pcVar8;
    pcVar8 = pcVar8 + 1;
    param_1 = param_1 + 1;
  }
  uVar5 = 0xffffffff;
  pcVar8 = (char *)local_188[IVar4];
  do {
    pcVar10 = pcVar8;
    if (uVar5 == 0) break;
    uVar5 = uVar5 - 1;
    pcVar10 = pcVar8 + 1;
    cVar1 = *pcVar8;
    pcVar8 = pcVar10;
  } while (cVar1 != '\0');
  uVar5 = ~uVar5;
  pcVar8 = pcVar10 + -uVar5;
  for (uVar6 = uVar5 >> 2; uVar6 != 0; uVar6 = uVar6 - 1) {
    *(undefined4 *)pcVar7 = *(undefined4 *)pcVar8;
    pcVar8 = pcVar8 + 4;
    pcVar7 = pcVar7 + 4;
  }
  for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
    *pcVar7 = *pcVar8;
    pcVar8 = pcVar8 + 1;
    pcVar7 = pcVar7 + 1;
  }
  iVar11 = 0;
  if (0 < local_1dc) {
    puVar9 = local_188;
    do {
      Free(puVar9[-0x14]);
      Free(*puVar9);
      iVar11 = iVar11 + 1;
      puVar9 = puVar9 + 1;
    } while (iVar11 < local_1dc);
  }
  return 1;
}



/* ===================================================== */
/* FUN_00a012f0 @ 00a012f0 */
/* ===================================================== */

undefined4 FUN_00a012f0(HWND param_1,int param_2,uint param_3,int *param_4)

{
  byte bVar1;
  uint uVar2;
  HWND pHVar3;
  HWND pHVar4;
  LRESULT LVar5;
  WPARAM wParam;
  byte *pbVar6;
  int iVar7;
  int iVar8;
  byte *pbVar9;
  undefined **ppuVar10;
  bool bVar11;
  byte local_50 [80];
  
  if (param_2 == 0x2b) {
    FUN_00a01640(param_4);
  }
  else {
    if (param_2 == 0x110) {
      pHVar3 = GetDlgItem(param_1,1000);
      pHVar4 = GetDlgItem(param_1,0x3eb);
      DAT_00a2e8e4 = param_4;
      SendMessageA(pHVar3,0x184,0,0);
      iVar8 = 0;
      if (0 < *DAT_00a2e8e4) {
        iVar7 = 0x54;
        do {
          SendMessageA(pHVar3,0x180,0,*(LPARAM *)((int)DAT_00a2e8e4 + iVar7));
          iVar8 = iVar8 + 1;
          iVar7 = iVar7 + 4;
        } while (iVar8 < *DAT_00a2e8e4);
      }
      LVar5 = SendMessageA(pHVar3,0x18c,0,DAT_00a2e8e4[0x51] + 0x18);
      if (LVar5 == -1) {
        SendMessageA(pHVar3,0x186,*DAT_00a2e8e4 - 1,0);
      }
      iVar8 = DAT_00a2e8e4[0x51] + 0x118;
      LVar5 = SendMessageA(pHVar3,0x188,0,0);
      FUN_00a01740(pHVar4,DAT_00a2e8e4[LVar5 + 0x3d],iVar8);
      if (*(undefined4 **)(DAT_00a2e8e4[0x51] + 0x124) != (undefined4 *)0x0) {
        SetWindowTextA(param_1,(LPCSTR)**(undefined4 **)(DAT_00a2e8e4[0x51] + 0x124));
        SetDlgItemTextA(param_1,0x3e9,*(LPCSTR *)(*(int *)(DAT_00a2e8e4[0x51] + 0x124) + 4));
        SetDlgItemTextA(param_1,0x3ea,*(LPCSTR *)(*(int *)(DAT_00a2e8e4[0x51] + 0x124) + 8));
        SetDlgItemTextA(param_1,1,*(LPCSTR *)(*(int *)(DAT_00a2e8e4[0x51] + 0x124) + 0xc));
        SetDlgItemTextA(param_1,2,*(LPCSTR *)(*(int *)(DAT_00a2e8e4[0x51] + 0x124) + 0x10));
      }
      SetFocus(pHVar3);
      return 0;
    }
    if (param_2 != 0x111) {
      return 0;
    }
    uVar2 = param_3 & 0xffff;
    if (uVar2 < 0x3e9) {
      if (uVar2 == 1000) {
        if (param_3 >> 0x10 == 2) goto LAB_00a013d5;
        if (param_3 >> 0x10 == 1) {
          pHVar3 = GetDlgItem(param_1,1000);
          pHVar4 = GetDlgItem(param_1,0x3eb);
          iVar8 = *(int *)((int)DAT_00a2e8e4 + 0x144) + 0x118;
          LVar5 = SendMessageA(pHVar3,0x188,0,0);
          FUN_00a01740(pHVar4,*(undefined4 *)((int)DAT_00a2e8e4 + 0xf4 + LVar5 * 4),iVar8);
          return 1;
        }
      }
      else {
        if (uVar2 == 1) {
LAB_00a013d5:
          LVar5 = SendDlgItemMessageA(param_1,1000,0x19f,0,0);
          EndDialog(param_1,LVar5);
          return 1;
        }
        if (uVar2 == 2) {
          EndDialog(param_1,-1);
          return 1;
        }
      }
    }
    else if (uVar2 == 0x3eb) {
      pHVar3 = GetDlgItem(param_1,0x3eb);
      wParam = SendMessageA(pHVar3,0x188,0,0);
      SendMessageA(pHVar3,0x189,wParam,(LPARAM)local_50);
      iVar8 = 0;
      ppuVar10 = &PTR_DAT_00a2c000;
      do {
        pbVar9 = *ppuVar10;
        pbVar6 = local_50;
        do {
          bVar1 = *pbVar6;
          bVar11 = bVar1 < *pbVar9;
          if (bVar1 != *pbVar9) {
LAB_00a01473:
            iVar7 = (1 - (uint)bVar11) - (uint)(bVar11 != 0);
            goto LAB_00a01478;
          }
          if (bVar1 == 0) break;
          bVar1 = pbVar6[1];
          bVar11 = bVar1 < pbVar9[1];
          if (bVar1 != pbVar9[1]) goto LAB_00a01473;
          pbVar6 = pbVar6 + 2;
          pbVar9 = pbVar9 + 2;
        } while (bVar1 != 0);
        iVar7 = 0;
LAB_00a01478:
        if (iVar7 == 0) {
          *(int *)(*(int *)((int)DAT_00a2e8e4 + 0x144) + 0x118) = iVar8;
        }
        ppuVar10 = ppuVar10 + 1;
        iVar8 = iVar8 + 1;
        if (0xa2c013 < (int)ppuVar10) {
          return 1;
        }
      } while( true );
    }
  }
  return 1;
}



/* ===================================================== */
/* FUN_00a01640 @ 00a01640 */
/* ===================================================== */

void FUN_00a01640(int param_1)

{
  HDC hdc;
  HGDIOBJ h;
  HPALETTE hPal;
  HPALETTE hPal_00;
  
  hdc = CreateCompatibleDC(*(HDC *)(param_1 + 0x18));
  h = SelectObject(hdc,*(HGDIOBJ *)(DAT_00a2e8e0 + 0x11c));
  hPal = SelectPalette(hdc,*(HPALETTE *)(DAT_00a2e8e0 + 0x120),0);
  if (*(int *)(DAT_00a2e8e0 + 0x11c) == 0) {
    PatBlt(*(HDC *)(param_1 + 0x18),*(int *)(param_1 + 0x1c),*(int *)(param_1 + 0x20),
           *(int *)(param_1 + 0x24) - *(int *)(param_1 + 0x1c),
           *(int *)(param_1 + 0x28) - *(int *)(param_1 + 0x20),0x42);
  }
  else {
    hPal_00 = SelectPalette(*(HDC *)(param_1 + 0x18),*(HPALETTE *)(DAT_00a2e8e0 + 0x120),0);
    RealizePalette(*(HDC *)(param_1 + 0x18));
    SetStretchBltMode(*(HDC *)(param_1 + 0x18),4);
    StretchBlt(*(HDC *)(param_1 + 0x18),*(int *)(param_1 + 0x1c),*(int *)(param_1 + 0x20),
               *(int *)(param_1 + 0x24) - *(int *)(param_1 + 0x1c),
               *(int *)(param_1 + 0x28) - *(int *)(param_1 + 0x20),hdc,0,0,0x140,0xf0,0xcc0020);
    SelectPalette(*(HDC *)(param_1 + 0x18),hPal_00,0);
  }
  SelectPalette(hdc,hPal,0);
  SelectObject(hdc,h);
  DeleteDC(hdc);
  return;
}



/* ===================================================== */
/* FUN_00a01740 @ 00a01740 */
/* ===================================================== */

void FUN_00a01740(HWND param_1,uint param_2,int *param_3)

{
  undefined **ppuVar1;
  byte bVar2;
  
  if ((param_2 & 1 << ((byte)*param_3 & 0x1f)) == 0) {
    *param_3 = 2;
  }
  SendMessageA(param_1,0x184,0,0);
  bVar2 = 0;
  ppuVar1 = &PTR_DAT_00a2c000;
  do {
    if ((param_2 & 1 << (bVar2 & 0x1f)) != 0) {
      SendMessageA(param_1,0x180,0,(LPARAM)*ppuVar1);
    }
    ppuVar1 = ppuVar1 + 1;
    bVar2 = bVar2 + 1;
  } while ((int)ppuVar1 < 0xa2c014);
  SendMessageA(param_1,0x18c,0,(LPARAM)(&PTR_DAT_00a2c000)[*param_3]);
  return;
}



/* ===================================================== */
/* FUN_00a017d0 @ 00a017d0 */
/* ===================================================== */

void FUN_00a017d0(undefined4 param_1,undefined4 *param_2)

{
  int iVar1;
  int *local_4;
  
  DirectDrawCreate(0,&local_4,0);
  iVar1 = *local_4;
  *param_2 = 0;
  (**(code **)(iVar1 + 0x20))(local_4,0,0,param_2,&LAB_00a01000);
  (**(code **)(*local_4 + 8))(local_4);
  return;
}



/* ===================================================== */
/* Setup3DDevice @ 00a01910 */
/* ===================================================== */

void Setup3DDevice(HGDIOBJ param_1,HGDIOBJ param_2,undefined4 param_3)

{
  char cVar1;
  char *pcVar2;
  UINT UVar3;
  int iVar4;
  LPCSTR lpString;
  uint uVar5;
  uint uVar6;
  char *pcVar7;
  undefined4 local_328;
  char local_324 [20];
  char local_310 [256];
  UINT local_210;
  HGDIOBJ local_20c;
  HGDIOBJ local_208;
  undefined4 local_204;
  CHAR local_200 [256];
  CHAR local_100 [256];
  
                    /* 0x1910  823  Setup3DDevice */
  GetModuleFileNameA((HMODULE)0x0,local_200,0x100);
  pcVar2 = _strrchr(local_200,0x2e);
  if (pcVar2 != (char *)0x0) {
    *pcVar2 = '\0';
  }
  lstrcatA(local_200,&DAT_00a2c13c);
  local_208 = param_2;
  local_328 = 0;
  local_20c = param_1;
  local_204 = param_3;
  GetPrivateProfileStringA
            (s_D3DDriver_00a2c124,s_Driver_00a2c130,&DAT_00a2c138,local_100,0x100,local_200);
  uVar5 = 0xffffffff;
  pcVar2 = local_100;
  do {
    pcVar7 = pcVar2;
    if (uVar5 == 0) break;
    uVar5 = uVar5 - 1;
    pcVar7 = pcVar2 + 1;
    cVar1 = *pcVar2;
    pcVar2 = pcVar7;
  } while (cVar1 != '\0');
  uVar5 = ~uVar5;
  pcVar2 = pcVar7 + -uVar5;
  pcVar7 = local_324;
  for (uVar6 = uVar5 >> 2; uVar6 != 0; uVar6 = uVar6 - 1) {
    *(undefined4 *)pcVar7 = *(undefined4 *)pcVar2;
    pcVar2 = pcVar2 + 4;
    pcVar7 = pcVar7 + 4;
  }
  for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
    *pcVar7 = *pcVar2;
    pcVar2 = pcVar2 + 1;
    pcVar7 = pcVar7 + 1;
  }
  GetPrivateProfileStringA
            (s_D3DDriver_00a2c124,s_Description_00a2c118,&DAT_00a2c138,local_100,0x100,local_200);
  uVar5 = 0xffffffff;
  pcVar2 = local_100;
  do {
    pcVar7 = pcVar2;
    if (uVar5 == 0) break;
    uVar5 = uVar5 - 1;
    pcVar7 = pcVar2 + 1;
    cVar1 = *pcVar2;
    pcVar2 = pcVar7;
  } while (cVar1 != '\0');
  uVar5 = ~uVar5;
  pcVar2 = pcVar7 + -uVar5;
  pcVar7 = local_310;
  for (uVar6 = uVar5 >> 2; uVar6 != 0; uVar6 = uVar6 - 1) {
    *(undefined4 *)pcVar7 = *(undefined4 *)pcVar2;
    pcVar2 = pcVar2 + 4;
    pcVar7 = pcVar7 + 4;
  }
  for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
    *pcVar7 = *pcVar2;
    pcVar2 = pcVar2 + 1;
    pcVar7 = pcVar7 + 1;
  }
  UVar3 = GetPrivateProfileIntA(s_Options_00a2c104,s_Resolution_00a2c10c,2,local_200);
  local_210 = UVar3;
  if (param_1 != (HGDIOBJ)0x0) {
    iVar4 = FUN_00a010d0(&local_328);
    if (iVar4 == -1) {
      DeleteObject(param_1);
      DeleteObject(param_2);
                    /* WARNING: Subroutine does not return */
      ExitProcess(1);
    }
    if (local_210 != UVar3) {
      wsprintfA(local_100,&DAT_00a2c100,local_210);
      WritePrivateProfileStringA(s_Options_00a2c104,s_Resolution_00a2c10c,local_100,local_200);
      wsprintfA(local_100,&DAT_00a2c100,*(undefined4 *)(&DAT_00a2c018 + local_210 * 4));
      WritePrivateProfileStringA(s_Options_00a2c104,&DAT_00a2c0f8,local_100,local_200);
      lpString = &DAT_00a2c0f4;
      if (1 < (int)local_210) {
        lpString = &DAT_00a2c0f0;
      }
      WritePrivateProfileStringA(s_Options_00a2c104,s_Small_00a2c0e8,lpString,local_200);
    }
  }
  iVar4 = __strcmpi(local_324,s_GLIDE_00a2c0e0);
  DAT_011be738 = (uint)(iVar4 == 0);
  iVar4 = __strcmpi(local_324,s_SOFTWARE_00a2c0d4);
  DAT_011be73c = (uint)(iVar4 == 0);
  WritePrivateProfileStringA(s_D3DDriver_00a2c124,s_Driver_00a2c130,local_324,local_200);
  WritePrivateProfileStringA(s_D3DDriver_00a2c124,s_Description_00a2c118,local_310,local_200);
  return;
}



/* ===================================================== */
/* entry @ 00a01bb0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 entry(HMODULE param_1,int param_2)

{
  float fVar1;
  float fVar2;
  float fVar3;
  float *pfVar4;
  int iVar5;
  HRSRC pHVar6;
  HGLOBAL pvVar7;
  int iVar8;
  undefined4 *puVar9;
  float *pfVar10;
  
  if (param_2 == 0) {
    timeKillEvent(DAT_011be6d0);
    Free(EXT_00b6dbd0);
    FUN_00a052e0();
  }
  else if (param_2 == 1) {
    pfVar10 = (float *)&DAT_011c4cd0;
    param_2 = 0;
    do {
      iVar5 = param_2;
      iVar8 = 8;
      *pfVar10 = _DAT_00a2a004 - (float)param_2 * _DAT_00a2a000;
      pfVar4 = pfVar10 + 0x909;
      do {
        fVar1 = (float)param_2;
        param_2 = param_2 + -0x20;
        iVar8 = iVar8 + -1;
        fVar2 = fVar1 * _DAT_00a2a018;
        fVar3 = _DAT_00a2a014 - fVar1 * _DAT_00a2a010;
        pfVar4[-0x808] = _DAT_00a2a00c - fVar1 * _DAT_00a2a008;
        fVar2 = _DAT_00a2a01c - fVar2;
        *pfVar4 = fVar3;
        pfVar4[0x808] = fVar2;
        pfVar4 = pfVar4 + 0x101;
      } while (iVar8 != 0);
      param_2 = iVar5 + 1;
      pfVar10 = pfVar10 + 1;
    } while (param_2 < 0x101);
    _DAT_011c50d0 = 0x3f7fe666;
    puVar9 = &DAT_011c71f4;
    iVar5 = 0;
    do {
      if (iVar5 < 0x1694) {
        puVar9[-0x7c8] = 0x3f7fcccd;
      }
      if (iVar5 < 0x1f9c) {
        *puVar9 = 0x3f7f999a;
      }
      puVar9[0x7e8] = 0x3f7f3333;
      iVar5 = iVar5 + 0x484;
      puVar9 = puVar9 + 0x121;
    } while (iVar5 < 0x2420);
    FUN_00a04ff0();
    EXT_00b6dbd0 = Malloc(0x100000);
    MainMem = &DAT_00b6dbd8;
    pHVar6 = FindResourceA(param_1,(LPCSTR)0x65,&DAT_00a2c170);
    pvVar7 = LoadResource(param_1,pHVar6);
    DAT_011be6d4 = LockResource(pvVar7);
    pHVar6 = FindResourceA(param_1,(LPCSTR)0x70,s_KANJI_00a2c168);
    pvVar7 = LoadResource(param_1,pHVar6);
    DAT_00b6d5b0 = LockResource(pvVar7);
    DAT_00a2e8e8 = param_1;
    return 1;
  }
  return 1;
}



/* ===================================================== */
/* FUN_00a01d60 @ 00a01d60 */
/* ===================================================== */

undefined4 FUN_00a01d60(void)

{
  return 0xffffffff;
}



/* ===================================================== */
/* FUN_00a01d70 @ 00a01d70 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a01d70(void)

{
  BOOL BVar1;
  int iVar2;
  LPTOP_LEVEL_EXCEPTION_FILTER lpTopLevelExceptionFilter;
  tagMSG local_1c;
  
  do {
    BVar1 = PeekMessageA(&local_1c,(HWND)0x0,0,0,1);
    if (BVar1 == 0) {
      iVar2 = (**(code **)(*DAT_011be760 + 0x3c))();
      if (iVar2 == 0) {
        WaitMessage();
        BVar1 = 1;
      }
    }
    else {
      if (local_1c.message == 0x12) {
        _DAT_00dedbf0 = 1;
        FUN_00a069b0(0xfffffffe);
        lpTopLevelExceptionFilter = SetUnhandledExceptionFilter(FUN_00a01d60);
        RaiseException(0xe0000001,0,0,(ULONG_PTR *)0x0);
        SetUnhandledExceptionFilter(lpTopLevelExceptionFilter);
                    /* WARNING: Subroutine does not return */
        ExitProcess(1);
      }
      TranslateMessage(&local_1c);
      DispatchMessageA(&local_1c);
    }
  } while (BVar1 != 0);
  return;
}



/* ===================================================== */
/* PollMessages @ 00a01e20 */
/* ===================================================== */

void PollMessages(void)

{
                    /* 0x1e20  784  PollMessages */
  FUN_00a01d70();
  FUN_00a049c0(1);
  return;
}



/* ===================================================== */
/* FUN_00a01e30 @ 00a01e30 */
/* ===================================================== */

undefined4 FUN_00a01e30(void)

{
                    /* 0x1e30  750   */
  return DAT_011be760;
}



/* ===================================================== */
/* FUN_00a01e40 @ 00a01e40 */
/* ===================================================== */

undefined4 FUN_00a01e40(void)

{
                    /* 0x1e40  751   */
  return 0;
}



/* ===================================================== */
/* WinCreateFont @ 00a01e50 */
/* ===================================================== */

void WinCreateFont(int param_1,int param_2,int param_3,int param_4,int param_5,LPCSTR param_6,
                  uint param_7,uint param_8,undefined1 param_9)

{
  HFONT pHVar1;
  
                    /* 0x1e50  828  WinCreateFont */
  if (*(HGDIOBJ *)(&DAT_00a2ea40 + param_1 * 4) != (HGDIOBJ)0x0) {
    DeleteObject(*(HGDIOBJ *)(&DAT_00a2ea40 + param_1 * 4));
  }
  pHVar1 = CreateFontA(param_3,param_2,0,0,(-(uint)(param_4 != 0) & 300) + 400,0,0,0,
                       -(uint)(param_5 != 0) & 0x80,0,0,0,0,param_6);
  *(HFONT *)(&DAT_00a2ea40 + param_1 * 4) = pHVar1;
  *(int *)(&DAT_00a2ea68 + param_1 * 4) = param_3;
  *(uint *)(&DAT_00a2c1a0 + param_1 * 4) =
       ((uint)CONCAT11(2,param_9) << 8 | param_8 & 0xff) << 8 | param_7 & 0xff;
  return;
}



/* ===================================================== */
/* FUN_00a01f00 @ 00a01f00 */
/* ===================================================== */

void FUN_00a01f00(HDC param_1,int *param_2)

{
  int iVar1;
  byte bVar2;
  int c;
  int y;
  LPCSTR lpString;
  char *pcVar3;
  
  SetBkMode(param_1,1);
  do {
    if (*(int *)(&DAT_00a2ea40 + param_2[1] * 4) != 0) {
      SetTextColor(param_1,*(COLORREF *)(&DAT_00a2c1a0 + param_2[1] * 4));
      SelectObject(param_1,*(HGDIOBJ *)(&DAT_00a2ea40 + param_2[1] * 4));
      y = param_2[3];
      pcVar3 = (char *)((int)param_2 + 0x13);
      do {
        lpString = pcVar3 + 1;
        c = 0;
        bVar2 = pcVar3[1];
        pcVar3 = lpString;
        while (0xd < bVar2) {
          iVar1 = c + 1;
          c = c + 1;
          pcVar3 = pcVar3 + 1;
          bVar2 = lpString[iVar1];
        }
        TextOutA(param_1,param_2[2],y,lpString,c);
        y = y + *(int *)(&DAT_00a2ea68 + param_2[1] * 4);
      } while (*pcVar3 != '\0');
    }
    param_2 = (int *)*param_2;
  } while (param_2 != (int *)0x0);
  return;
}



/* ===================================================== */
/* WinDrawText @ 00a01f90 */
/* ===================================================== */

void WinDrawText(undefined4 param_1,int param_2,undefined4 param_3,undefined4 param_4,char *param_5)

{
  int iVar1;
  char cVar2;
  undefined4 *puVar3;
  int iVar4;
  uint uVar5;
  uint uVar6;
  undefined4 unaff_EBX;
  undefined4 *puVar7;
  char *pcVar8;
  char *pcVar9;
  undefined1 local_118 [4];
  undefined4 local_114 [69];
  
                    /* 0x1f90  829  WinDrawText */
  puVar7 = DAT_00dedbd8;
  iVar4 = 0;
  cVar2 = *param_5;
  while (cVar2 != '\0') {
    iVar1 = iVar4 + 1;
    iVar4 = iVar4 + 1;
    cVar2 = param_5[iVar1];
  }
  if (param_2 == 0) {
    puVar7 = local_114;
  }
  else if (DAT_00a2ea90 == (undefined4 *)0x0) {
    DAT_00a2ea90 = DAT_00dedbd8;
    DAT_00a2ea94 = DAT_00dedbd8;
    DAT_00dedbd8 = DAT_00dedbd8 + (iVar4 >> 2) + 6;
  }
  else {
    puVar3 = DAT_00dedbd8 + (iVar4 >> 2) + 6;
    *DAT_00a2ea94 = DAT_00dedbd8;
    DAT_00dedbd8 = puVar3;
    DAT_00a2ea94 = puVar7;
  }
  puVar7[4] = iVar4;
  puVar7[1] = param_1;
  puVar7[2] = param_3;
  puVar7[3] = param_4;
  uVar5 = 0xffffffff;
  *puVar7 = 0;
  do {
    pcVar8 = param_5;
    if (uVar5 == 0) break;
    uVar5 = uVar5 - 1;
    pcVar8 = param_5 + 1;
    cVar2 = *param_5;
    param_5 = pcVar8;
  } while (cVar2 != '\0');
  uVar5 = ~uVar5;
  pcVar8 = pcVar8 + -uVar5;
  pcVar9 = (char *)(puVar7 + 5);
  for (uVar6 = uVar5 >> 2; uVar6 != 0; uVar6 = uVar6 - 1) {
    *(undefined4 *)pcVar9 = *(undefined4 *)pcVar8;
    pcVar8 = pcVar8 + 4;
    pcVar9 = pcVar9 + 4;
  }
  for (uVar5 = uVar5 & 3; uVar5 != 0; uVar5 = uVar5 - 1) {
    *pcVar9 = *pcVar8;
    pcVar8 = pcVar8 + 1;
    pcVar9 = pcVar9 + 1;
  }
  if ((param_2 == 0) && (iVar4 = (**(code **)(*DAT_011be760 + 0xcc))(local_118), -1 < iVar4)) {
    FUN_00a01f00(unaff_EBX,puVar7);
    (**(code **)(*DAT_011be760 + 0xd0))(unaff_EBX);
  }
  return;
}



/* ===================================================== */
/* FUN_00a02090 @ 00a02090 */
/* ===================================================== */

undefined4 FUN_00a02090(void)

{
                    /* 0x2090  487  
                       0x2090  488  
                       0x2090  672   */
  return 0;
}



/* ===================================================== */
/* FUN_00a020a0 @ 00a020a0 */
/* ===================================================== */

int FUN_00a020a0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                undefined4 param_5,int param_6)

{
  int iVar1;
  undefined4 uVar2;
  int iVar3;
  
                    /* 0x20a0  668   */
  iVar3 = DAT_00a2ea98;
  if (DAT_00a2ea98 < 8) {
    iVar1 = DAT_00a2ea98 * 0x24;
    *(undefined4 *)(&DAT_00a2e8f0 + iVar1) = param_1;
    *(undefined4 *)(&DAT_00a2e8f4 + iVar1) = param_2;
    *(undefined4 *)(&DAT_00a2e8f8 + iVar1) = param_3;
    *(undefined4 *)(&DAT_00a2e8fc + iVar1) = param_4;
    *(undefined4 *)(&DAT_00a2e900 + iVar1) = param_5;
    (&DAT_00a2e904)[iVar3 * 9] = param_6;
    (&DAT_00a2e908)[iVar3 * 9] = 0;
    *(undefined4 *)(&DAT_00a2e90c + iVar1) = 0;
    uVar2 = Malloc(param_6 + 1);
    iVar3 = DAT_00a2ea98 + 1;
    iVar1 = DAT_00a2ea98 * 9;
    DAT_00a2ea98 = iVar3;
    (&DAT_00a2e910)[iVar1] = uVar2;
  }
  return iVar3;
}



/* ===================================================== */
/* FUN_00a02120 @ 00a02120 */
/* ===================================================== */

int FUN_00a02120(uint param_1,undefined4 param_2)

{
  int iVar1;
  uint uVar2;
  undefined4 *puVar3;
  undefined4 *puVar4;
  undefined1 local_100 [248];
  int iStack_8;
  
                    /* 0x2120  654   */
  if (DAT_00a2c198 == -1) {
    FUN_00a026e0(0,0);
  }
  if (param_1 < 10) {
    iVar1 = Ordinal_2(local_100,param_2,&stack0x0000000c);
    if (VSyncDelayed == 0) {
      if (DAT_00a2e904 < iVar1 + (&DAT_00a2e8e4)[iStack_8 * 9]) {
        return 0;
      }
      puVar3 = (undefined4 *)&stack0xfffffef4;
      puVar4 = (undefined4 *)
               (*(int *)(&DAT_00a2e8ec + iStack_8 * 0x24) + (&DAT_00a2e8e4)[iStack_8 * 9]);
      for (uVar2 = iVar1 + 1U >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
        *puVar4 = *puVar3;
        puVar3 = puVar3 + 1;
        puVar4 = puVar4 + 1;
      }
      for (uVar2 = iVar1 + 1U & 3; uVar2 != 0; uVar2 = uVar2 - 1) {
        *(undefined1 *)puVar4 = *(undefined1 *)puVar3;
        puVar3 = (undefined4 *)((int)puVar3 + 1);
        puVar4 = (undefined4 *)((int)puVar4 + 1);
      }
      (&DAT_00a2e8e4)[iStack_8 * 9] = (&DAT_00a2e8e4)[iStack_8 * 9] + iVar1;
      return iVar1;
    }
  }
  else {
    iVar1 = Ordinal_2(local_100,param_1,&param_2);
    if (VSyncDelayed == 0) {
      if (DAT_00a2e904 < iVar1 + DAT_00a2e908) {
        return 0;
      }
      puVar3 = (undefined4 *)&stack0xfffffef4;
      puVar4 = (undefined4 *)(DAT_00a2e910 + DAT_00a2e908);
      for (uVar2 = iVar1 + 1U >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
        *puVar4 = *puVar3;
        puVar3 = puVar3 + 1;
        puVar4 = puVar4 + 1;
      }
      for (uVar2 = iVar1 + 1U & 3; uVar2 != 0; uVar2 = uVar2 - 1) {
        *(undefined1 *)puVar4 = *(undefined1 *)puVar3;
        puVar3 = (undefined4 *)((int)puVar3 + 1);
        puVar4 = (undefined4 *)((int)puVar4 + 1);
      }
      DAT_00a2e908 = DAT_00a2e908 + iVar1;
    }
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_00a02250 @ 00a02250 */
/* ===================================================== */

void FUN_00a02250(undefined4 param_1)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  undefined1 local_100 [256];
  
                    /* 0x2250  652   */
  if (DAT_00a2c198 == -1) {
    FUN_00a026e0(0,0);
  }
  iVar1 = Ordinal_2(local_100,param_1,&stack0x00000008);
  iVar4 = 0;
  if (0 < iVar1) {
    do {
      if ((0x4f < DAT_00a2f428) ||
         (iVar2 = DAT_00a2f420, iVar3 = DAT_00a2f428, local_100[iVar4 + -0xc] == '\n')) {
        iVar3 = 0;
        iVar2 = DAT_00a2f420 + 1;
        (&DAT_00a2eaa0)[DAT_00a2f428 + DAT_00a2f420 * 0x51] = 0;
        if (0x1d < iVar2) {
          iVar2 = 0;
        }
        if (iVar2 == DAT_00a2f424) {
          DAT_00a2f424 = DAT_00a2f424 + 1;
        }
        if (0x1d < DAT_00a2f424) {
          DAT_00a2f424 = 0;
        }
      }
      DAT_00a2f428 = iVar3;
      if ('\x1f' < (char)local_100[iVar4 + -0xc]) {
        DAT_00a2f428 = iVar3 + 1;
        (&DAT_00a2eaa0)[iVar3 + iVar2 * 0x51] = local_100[iVar4 + -0xc];
      }
      iVar4 = iVar4 + 1;
      DAT_00a2f420 = iVar2;
    } while (iVar4 < iVar1);
  }
  (&DAT_00a2eaa0)[DAT_00a2f428 + DAT_00a2f420 * 0x51] = 0;
  return;
}



/* ===================================================== */
/* FUN_00a02320 @ 00a02320 */
/* ===================================================== */

void FUN_00a02320(void)

{
                    /* 0x2320  653   */
  DAT_00a2f424 = 0;
  DAT_00a2f420 = 0;
  DAT_00a2f428 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a02340 @ 00a02340 */
/* ===================================================== */

void FUN_00a02340(void)

{
  if ((DAT_00a2f424 != DAT_00a2f420) || (DAT_00a2f428 != 0)) {
    FUN_00a264f0(DAT_00a2c198,DAT_00a2ea38,0,0,0xff,0x7f,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a02380 @ 00a02380 */
/* ===================================================== */

void FUN_00a02380(void)

{
  byte bVar1;
  short sVar2;
  short sVar3;
  byte *pbVar4;
  int local_2c;
  undefined1 local_28 [4];
  undefined1 local_24;
  undefined1 local_23;
  undefined1 local_22;
  short local_20;
  short local_1e;
  char local_1c;
  byte local_1b;
  undefined2 local_1a;
  short local_18;
  short local_16;
  char local_14;
  byte local_13;
  undefined2 local_12;
  short local_10;
  short local_e;
  char local_c;
  char local_b;
  short local_8;
  short local_6;
  char local_4;
  char local_3;
  
  if ((DAT_00a2f424 != DAT_00a2f420) || (DAT_00a2f428 != 0)) {
    local_22 = 0x80;
    local_23 = 0x80;
    local_24 = 0x80;
    local_1a = (undefined2)DAT_00a2ea38;
    local_12 = (undefined2)DAT_00a2c198;
    local_2c = DAT_00a2f424 + -1;
    sVar2 = 0;
    do {
      sVar3 = 0;
      local_2c = local_2c + 1;
      if (0x1d < local_2c) {
        local_2c = 0;
      }
      pbVar4 = &DAT_00a2eaa0 + local_2c * 0x51;
      if ((&DAT_00a2eaa0)[local_2c * 0x51] != '\0') {
        do {
          bVar1 = *pbVar4;
          local_18 = sVar3 + 8;
          pbVar4 = pbVar4 + 1;
          local_1c = bVar1 * '\b';
          local_14 = local_1c + '\b';
          local_1b = bVar1 >> 1 & 0x70;
          local_b = local_1b + 0x10;
          local_20 = sVar3;
          local_1e = sVar2;
          local_16 = sVar2;
          local_13 = local_1b;
          local_10 = sVar3;
          local_e = sVar2 + 0x10;
          local_c = local_1c;
          local_8 = local_18;
          local_6 = sVar2 + 0x10;
          local_4 = local_14;
          local_3 = local_b;
          FUN_00a0f570(local_28);
          sVar3 = sVar3 + 8;
        } while (*pbVar4 != 0);
      }
      sVar2 = sVar2 + 0x10;
    } while (local_2c != DAT_00a2f420);
  }
  return;
}



/* ===================================================== */
/* FUN_00a02490 @ 00a02490 */
/* ===================================================== */

undefined4 FUN_00a02490(void)

{
                    /* 0x2490  46  
                       0x2490  47  
                       0x2490  48  
                       0x2490  49  
                       0x2490  50  
                       0x2490  53  
                       0x2490  64  
                       0x2490  65  
                       0x2490  116  
                       0x2490  118  
                       0x2490  119  
                       0x2490  120  
                       0x2490  121  
                       0x2490  123  
                       0x2490  127  
                       0x2490  129  
                       0x2490  130  
                       0x2490  131  
                       0x2490  330  
                       0x2490  655  
                       0x2490  669  
                       0x2490  747   */
  return 0;
}



/* ===================================================== */
/* FUN_00a024a0 @ 00a024a0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a024a0(int param_1)

{
  undefined4 uVar1;
  
  uVar1 = DAT_00a2ea38;
  if (*(int *)(&DAT_00a2e900 + param_1 * 0x24) != 1) {
    uVar1 = _DAT_00a2ea34;
  }
  FUN_00a264f0(DAT_00a2c198,uVar1,0,0,0xff,0x7f,0);
  return;
}



/* ===================================================== */
/* FUN_00a024e0 @ 00a024e0 */
/* ===================================================== */

void FUN_00a024e0(int param_1)

{
  byte bVar1;
  int iVar2;
  int iVar3;
  undefined4 uVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  int iVar8;
  undefined2 local_18;
  undefined1 local_14 [4];
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined2 local_c;
  undefined2 local_a;
  char local_8;
  byte local_7;
  undefined2 local_6;
  undefined2 local_4;
  undefined2 local_2;
  
  uVar4 = DAT_011be6ec;
  local_e = 0x80;
  iVar7 = param_1 * 0x24;
  local_f = 0x80;
  local_10 = 0x80;
  local_4 = 8;
  local_2 = 0x10;
  if (*(int *)(&DAT_00a2e900 + iVar7) == 1) {
    local_6 = (undefined2)DAT_00a2ea38;
  }
  else {
    local_6 = DAT_00a2ea34;
  }
  iVar2 = *(int *)(&DAT_00a2e8f8 + iVar7);
  iVar8 = *(int *)(&DAT_00a2e8f0 + iVar7);
  iVar5 = *(int *)(&DAT_00a2e8f4 + iVar7);
  DAT_011be6ec._0_2_ = (undefined2)DAT_00a2c198;
  iVar3 = *(int *)(&DAT_00a2e8fc + iVar7);
  iVar6 = 0;
  if (0 < (int)(&DAT_00a2e908)[param_1 * 9]) {
    do {
      bVar1 = *(byte *)(iVar6 + (&DAT_00a2e910)[param_1 * 9]);
      local_c = (undefined2)iVar8;
      local_7 = bVar1 >> 1 & 0x70;
      local_a = (undefined2)iVar5;
      local_8 = bVar1 << 3;
      if (bVar1 == 8) {
        iVar8 = iVar8 + -8;
      }
      else if (bVar1 == 10) {
        iVar8 = *(int *)(&DAT_00a2e8f0 + iVar7);
        iVar5 = iVar5 + 0x10;
      }
      else {
        iVar8 = iVar8 + 8;
        if (iVar2 < iVar8 - *(int *)(&DAT_00a2e8f0 + iVar7)) {
          iVar5 = iVar5 + 0x10;
          iVar8 = *(int *)(&DAT_00a2e8f0 + iVar7);
        }
        if ((iVar5 - *(int *)(&DAT_00a2e8f4 + iVar7) <= iVar3) && (0x20 < bVar1)) {
          FUN_00a107e0(local_14);
        }
      }
      iVar6 = iVar6 + 1;
    } while (iVar6 < (int)(&DAT_00a2e908)[param_1 * 9]);
  }
  local_18 = (undefined2)uVar4;
  (&DAT_00a2e908)[param_1 * 9] = 0;
  *(undefined4 *)(&DAT_00a2e90c + iVar7) = 0;
  DAT_011be6ec = CONCAT22(DAT_011be6ec._2_2_,local_18);
  return;
}



/* ===================================================== */
/* FUN_00a02640 @ 00a02640 */
/* ===================================================== */

undefined4 FUN_00a02640(int param_1)

{
  int iVar1;
  int iVar2;
  int *piVar3;
  
                    /* 0x2640  689   */
  if (param_1 == -1) {
    iVar2 = 0;
    if (0 < DAT_00a2ea98) {
      piVar3 = &DAT_00a2e908;
      do {
        FUN_00a26490();
        if (*piVar3 != 0) {
          FUN_00a024a0(iVar2);
        }
        FUN_00a26f20();
        iVar2 = iVar2 + 1;
        piVar3 = piVar3 + 9;
      } while (iVar2 < DAT_00a2ea98);
    }
    iVar2 = 0;
    if (0 < DAT_00a2ea98) {
      piVar3 = &DAT_00a2e908;
      iVar1 = DAT_00a2ea98;
      do {
        if (*piVar3 != 0) {
          (**(code **)(*DAT_011be760 + 0xb0))();
          FUN_00a024e0(iVar2);
          (**(code **)(*DAT_011be760 + 0xb4))();
          iVar1 = DAT_00a2ea98;
        }
        iVar2 = iVar2 + 1;
        piVar3 = piVar3 + 9;
      } while (iVar2 < iVar1);
    }
    return 0;
  }
  (&DAT_00a2e8e8)[param_1 * 9] = 1;
  return 0;
}



/* ===================================================== */
/* FUN_00a026e0 @ 00a026e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a026e0(int param_1,undefined4 param_2)

{
  uint uVar1;
  int iVar2;
  undefined4 *puVar3;
  byte local_3c [12];
  undefined4 local_30;
  undefined4 *local_24;
  undefined4 local_20 [8];
  
                    /* 0x26e0  710   */
  FUN_00a1d8b0(DAT_011be6d4 + 4,local_3c);
  uVar1 = FUN_00a06070(local_30,0,0,param_1,param_2,0x100,0x80);
  DAT_00a2c198 = uVar1 & 0xffff;
  iVar2 = 8;
  if ((local_3c[0] & 8) != 0) {
    puVar3 = local_20;
    for (; iVar2 != 0; iVar2 = iVar2 + -1) {
      *puVar3 = *local_24;
      local_24 = local_24 + 1;
      puVar3 = puVar3 + 1;
    }
    local_20[0]._0_2_ = 0x8000;
    _DAT_00a2ea34 = FUN_00a06020(local_20,param_1,param_2);
    _DAT_00a2ea34 = _DAT_00a2ea34 & 0xffff;
    local_20[0]._0_2_ = 0;
    uVar1 = FUN_00a06020(local_20,param_1 + 0x20,param_2);
    DAT_00a2ea38 = uVar1 & 0xffff;
  }
  return;
}



/* ===================================================== */
/* FUN_00a02790 @ 00a02790 */
/* ===================================================== */

undefined4 FUN_00a02790(void)

{
                    /* 0x2790  673   */
  return 0;
}



/* ===================================================== */
/* FUN_00a027a0 @ 00a027a0 */
/* ===================================================== */

void FUN_00a027a0(void)

{
  int iVar1;
  int *piVar2;
  
  iVar1 = 0;
  if (0 < DAT_00a2ea98) {
    piVar2 = &DAT_00a2e908;
    do {
      if (*piVar2 != 0) {
        FUN_00a024a0(iVar1);
      }
      iVar1 = iVar1 + 1;
      piVar2 = piVar2 + 9;
    } while (iVar1 < DAT_00a2ea98);
  }
  FUN_00a02340();
  return;
}



/* ===================================================== */
/* FUN_00a027e0 @ 00a027e0 */
/* ===================================================== */

void FUN_00a027e0(void)

{
  int iVar1;
  int *piVar2;
  
  iVar1 = 0;
  if (0 < DAT_00a2ea98) {
    piVar2 = &DAT_00a2e908;
    do {
      if (*piVar2 != 0) {
        FUN_00a024e0(iVar1);
      }
      iVar1 = iVar1 + 1;
      piVar2 = piVar2 + 9;
    } while (iVar1 < DAT_00a2ea98);
  }
  FUN_00a02380();
  return;
}



/* ===================================================== */
/* FUN_00a02820 @ 00a02820 */
/* ===================================================== */

void FUN_00a02820(void)

{
  int iVar1;
  undefined1 *puVar2;
  undefined1 local_4 [4];
  
  puVar2 = local_4;
  if (DAT_00a2ea90 != 0) {
    iVar1 = (**(code **)(*DAT_011be760 + 0xc4))(local_4);
    if (-1 < iVar1) {
      FUN_00a01f00(puVar2,DAT_00a2ea90);
      (**(code **)(*DAT_011be760 + 200))(puVar2);
    }
    DAT_00a2ea90 = 0;
  }
  return;
}



/* ===================================================== */
/* FUN_00a02870 @ 00a02870 */
/* ===================================================== */

void FUN_00a02870(void)

{
                    /* 0x2870  56  
                       0x2870  62  
                       0x2870  63  
                       0x2870  139  
                       0x2870  157  
                       0x2870  161  
                       0x2870  170  
                       0x2870  171  
                       0x2870  172  
                       0x2870  179  
                       0x2870  190  
                       0x2870  194  
                       0x2870  196  
                       0x2870  197  
                       0x2870  202  
                       0x2870  238  
                       0x2870  239  
                       0x2870  240  
                       0x2870  241  
                       0x2870  244  
                       0x2870  375  
                       0x2870  491  
                       0x2870  494  
                       0x2870  502  
                       0x2870  567  
                       0x2870  570  
                       0x2870  605  
                       0x2870  619  
                       0x2870  629  
                       0x2870  630  
                       0x2870  637  
                       0x2870  651  
                       0x2870  705  
                       0x2870  706  
                       0x2870  707  
                       0x2870  708  
                       0x2870  709  
                       0x2870  712  
                       0x2870  720  
                       0x2870  762  
                       0x2870  774   */
  return;
}



/* ===================================================== */
/* FUN_00a02880 @ 00a02880 */
/* ===================================================== */

void FUN_00a02880(undefined4 *param_1,undefined4 param_2)

{
                    /* 0x2880  713   */
  param_1[1] = 0xf7000000;
  param_1[2] = param_2;
  *param_1 = 0x1000000;
  return;
}



/* ===================================================== */
/* FUN_00a028a0 @ 00a028a0 */
/* ===================================================== */

void FUN_00a028a0(undefined4 *param_1,undefined4 *param_2)

{
  int iVar1;
  undefined4 *puVar2;
  
                    /* 0x28a0  714   */
  *param_1 = 0;
  param_1[1] = 0xe1000000;
  puVar2 = param_1 + 2;
  for (iVar1 = 0x17; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = *param_2;
    param_2 = param_2 + 1;
    puVar2 = puVar2 + 1;
  }
  *param_1 = 0xe000000;
  return;
}



/* ===================================================== */
/* FUN_00a028d0 @ 00a028d0 */
/* ===================================================== */

void FUN_00a028d0(undefined4 *param_1,undefined4 *param_2)

{
                    /* 0x28d0  715   */
  *param_1 = 0;
  param_1[1] = 0xf3000000;
  param_1[2] = *param_2;
  param_1[3] = param_2[1];
  *param_1 = 0xf000000;
  return;
}



/* ===================================================== */
/* FUN_00a02900 @ 00a02900 */
/* ===================================================== */

void FUN_00a02900(undefined4 *param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                 undefined4 *param_5)

{
                    /* 0x2900  716   */
  param_1[3] = param_2;
  param_1[5] = param_4;
  *param_1 = 0;
  param_1[1] = 0xf4000000;
  param_1[4] = param_3;
  param_1[6] = *param_5;
  param_1[7] = param_5[1];
  *param_1 = 0x6000000;
  return;
}



/* ===================================================== */
/* FUN_00a02940 @ 00a02940 */
/* ===================================================== */

void FUN_00a02940(undefined4 *param_1,int param_2,int param_3,int param_4)

{
                    /* 0x2940  717   */
  param_1[1] = (param_2 + 0x7080000 + param_3 * 2) * 0x20 + param_4;
  *param_1 = 0x1000000;
  return;
}



/* ===================================================== */
/* FUN_00a02970 @ 00a02970 */
/* ===================================================== */

void FUN_00a02970(undefined4 *param_1)

{
                    /* 0x2970  718   */
  param_1[1] = 0xf5000000;
  *param_1 = 0x4000000;
  return;
}



/* ===================================================== */
/* FUN_00a02990 @ 00a02990 */
/* ===================================================== */

void FUN_00a02990(undefined4 *param_1,ushort *param_2)

{
  ushort uVar1;
  
                    /* 0x2990  719   */
  *param_1 = 0;
  param_1[1] = 0xf6000000;
  uVar1 = *param_2;
  *param_1 = 0x1000000;
  param_1[2] = (uint)uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a029c0 @ 00a029c0 */
/* ===================================================== */

void FUN_00a029c0(uint *param_1)

{
                    /* 0x29c0  721   */
  *(undefined1 *)((int)param_1 + 7) = 0x40;
  *param_1 = *param_1 & 0xffffff | 0x2000000;
  return;
}



/* ===================================================== */
/* FUN_00a029e0 @ 00a029e0 */
/* ===================================================== */

void FUN_00a029e0(undefined4 *param_1)

{
                    /* 0x29e0  722   */
  *(undefined1 *)((int)param_1 + 7) = 0x48;
  *param_1 = 0x4000000;
  param_1[5] = 0x55555555;
  return;
}



/* ===================================================== */
/* FUN_00a02a00 @ 00a02a00 */
/* ===================================================== */

void FUN_00a02a00(undefined4 *param_1)

{
                    /* 0x2a00  723   */
  *(undefined1 *)((int)param_1 + 7) = 0x4c;
  *param_1 = 0x5000000;
  param_1[6] = 0x55555555;
  return;
}



/* ===================================================== */
/* FUN_00a02a20 @ 00a02a20 */
/* ===================================================== */

void FUN_00a02a20(undefined4 *param_1)

{
                    /* 0x2a20  724   */
  *(undefined1 *)((int)param_1 + 7) = 0x50;
  *param_1 = 0x3000000;
  return;
}



/* ===================================================== */
/* FUN_00a02a40 @ 00a02a40 */
/* ===================================================== */

void FUN_00a02a40(undefined4 *param_1)

{
                    /* 0x2a40  725   */
  *(undefined1 *)((int)param_1 + 7) = 0x58;
  *param_1 = 0x6000000;
  param_1[7] = 0x55555555;
  return;
}



/* ===================================================== */
/* FUN_00a02a60 @ 00a02a60 */
/* ===================================================== */

void FUN_00a02a60(undefined4 *param_1)

{
                    /* 0x2a60  726   */
  *(undefined1 *)((int)param_1 + 7) = 0x5c;
  *param_1 = 0x8000000;
  param_1[9] = 0x55555555;
  return;
}



/* ===================================================== */
/* FUN_00a02a80 @ 00a02a80 */
/* ===================================================== */

void FUN_00a02a80(undefined4 *param_1)

{
                    /* 0x2a80  727   */
  *(undefined1 *)((int)param_1 + 7) = 0x20;
  *param_1 = 0x3000000;
  return;
}



/* ===================================================== */
/* FUN_00a02aa0 @ 00a02aa0 */
/* ===================================================== */

void FUN_00a02aa0(undefined4 *param_1)

{
                    /* 0x2aa0  728   */
  *(undefined1 *)((int)param_1 + 7) = 0x28;
  *param_1 = 0x4000000;
  return;
}



/* ===================================================== */
/* FUN_00a02ac0 @ 00a02ac0 */
/* ===================================================== */

void FUN_00a02ac0(undefined4 *param_1)

{
                    /* 0x2ac0  729   */
  *(undefined1 *)((int)param_1 + 7) = 0x24;
  *param_1 = 0x6000000;
  return;
}



/* ===================================================== */
/* FUN_00a02ae0 @ 00a02ae0 */
/* ===================================================== */

void FUN_00a02ae0(undefined4 *param_1)

{
                    /* 0x2ae0  730   */
  *(undefined1 *)((int)param_1 + 7) = 0x2c;
  *param_1 = 0x8000000;
  return;
}



/* ===================================================== */
/* FUN_00a02b00 @ 00a02b00 */
/* ===================================================== */

void FUN_00a02b00(undefined4 *param_1)

{
                    /* 0x2b00  731   */
  *(undefined1 *)((int)param_1 + 7) = 0x30;
  *param_1 = 0x5000000;
  return;
}



/* ===================================================== */
/* FUN_00a02b20 @ 00a02b20 */
/* ===================================================== */

void FUN_00a02b20(undefined4 *param_1)

{
                    /* 0x2b20  732   */
  *(undefined1 *)((int)param_1 + 7) = 0x38;
  *param_1 = 0x7000000;
  return;
}



/* ===================================================== */
/* FUN_00a02b40 @ 00a02b40 */
/* ===================================================== */

void FUN_00a02b40(undefined4 *param_1)

{
                    /* 0x2b40  733   */
  *(undefined1 *)((int)param_1 + 7) = 0x34;
  *param_1 = 0x8000000;
  return;
}



/* ===================================================== */
/* FUN_00a02b60 @ 00a02b60 */
/* ===================================================== */

void FUN_00a02b60(undefined4 *param_1)

{
                    /* 0x2b60  734   */
  *(undefined1 *)((int)param_1 + 7) = 0x3c;
  *param_1 = 0xb000000;
  return;
}



/* ===================================================== */
/* FUN_00a02b80 @ 00a02b80 */
/* ===================================================== */

void FUN_00a02b80(undefined4 *param_1)

{
                    /* 0x2b80  738   */
  *(undefined1 *)((int)param_1 + 7) = 100;
  *param_1 = 0x3000000;
  return;
}



/* ===================================================== */
/* FUN_00a02ba0 @ 00a02ba0 */
/* ===================================================== */

void FUN_00a02ba0(undefined4 *param_1)

{
                    /* 0x2ba0  739   */
  *(undefined1 *)((int)param_1 + 7) = 0x7c;
  *param_1 = 0x2000000;
  return;
}



/* ===================================================== */
/* FUN_00a02bc0 @ 00a02bc0 */
/* ===================================================== */

void FUN_00a02bc0(undefined4 *param_1)

{
                    /* 0x2bc0  740   */
  *(undefined1 *)((int)param_1 + 7) = 0x74;
  *param_1 = 0x2000000;
  return;
}



/* ===================================================== */
/* FUN_00a02be0 @ 00a02be0 */
/* ===================================================== */

void FUN_00a02be0(undefined4 *param_1,undefined4 param_2)

{
                    /* 0x2be0  741   */
  param_1[1] = 0xf8;
  param_1[2] = param_2;
  *param_1 = 0x1000000;
  return;
}



/* ===================================================== */
/* FUN_00a02c00 @ 00a02c00 */
/* ===================================================== */

void FUN_00a02c00(undefined4 *param_1)

{
                    /* 0x2c00  742   */
  *(undefined1 *)((int)param_1 + 7) = 0x60;
  *param_1 = 0x2000000;
  return;
}



/* ===================================================== */
/* FUN_00a02c20 @ 00a02c20 */
/* ===================================================== */

void FUN_00a02c20(undefined4 *param_1)

{
                    /* 0x2c20  743   */
  *(undefined1 *)((int)param_1 + 7) = 0x68;
  *param_1 = 0x1000000;
  return;
}



/* ===================================================== */
/* FUN_00a02c40 @ 00a02c40 */
/* ===================================================== */

void FUN_00a02c40(undefined4 *param_1)

{
                    /* 0x2c40  744   */
  *(undefined1 *)((int)param_1 + 7) = 0x78;
  *param_1 = 0x1000000;
  return;
}



/* ===================================================== */
/* FUN_00a02c60 @ 00a02c60 */
/* ===================================================== */

void FUN_00a02c60(undefined4 *param_1)

{
                    /* 0x2c60  745   */
  *(undefined1 *)((int)param_1 + 7) = 0x70;
  *param_1 = 0x1000000;
  return;
}



/* ===================================================== */
/* FUN_00a02c80 @ 00a02c80 */
/* ===================================================== */

void FUN_00a02c80(uint *param_1)

{
                    /* 0x2c80  746   */
  *param_1 = *param_1 | 0xffffff;
  return;
}



/* ===================================================== */
/* FUN_00a02c90 @ 00a02c90 */
/* ===================================================== */

void FUN_00a02c90(int param_1,int param_2)

{
                    /* 0x2c90  736   */
  if (param_2 != 0) {
    *(byte *)(param_1 + 7) = *(byte *)(param_1 + 7) | 2;
    return;
  }
  *(byte *)(param_1 + 7) = *(byte *)(param_1 + 7) & 0xfd;
  return;
}



/* ===================================================== */
/* FUN_00a02cc0 @ 00a02cc0 */
/* ===================================================== */

void FUN_00a02cc0(int param_1,int param_2)

{
                    /* 0x2cc0  737   */
  if (param_2 != 0) {
    *(byte *)(param_1 + 7) = *(byte *)(param_1 + 7) | 1;
    return;
  }
  *(byte *)(param_1 + 7) = *(byte *)(param_1 + 7) & 0xfe;
  return;
}



/* ===================================================== */
/* FUN_00a02cf0 @ 00a02cf0 */
/* ===================================================== */

uint FUN_00a02cf0(uint *param_1)

{
                    /* 0x2cf0  697   */
  return *param_1 & 0xffffff | 0x80000000;
}



/* ===================================================== */
/* FUN_00a02d10 @ 00a02d10 */
/* ===================================================== */

void FUN_00a02d10(uint *param_1,uint *param_2)

{
                    /* 0x2d10  698   */
  *param_2 = (*param_1 ^ *param_2) & 0xffffff ^ *param_2;
  *param_1 = ((uint)param_2 ^ *param_1) & 0xffffff ^ *param_1;
  return;
}



/* ===================================================== */
/* FUN_00a02d40 @ 00a02d40 */
/* ===================================================== */

void FUN_00a02d40(uint *param_1,uint param_2,uint *param_3)

{
                    /* 0x2d40  699   */
  *param_3 = (*param_1 ^ *param_3) & 0xffffff ^ *param_3;
  *param_1 = (param_2 ^ *param_1) & 0xffffff ^ *param_1;
  return;
}



/* ===================================================== */
/* FUN_00a02d70 @ 00a02d70 */
/* ===================================================== */

void FUN_00a02d70(uint *param_1,uint param_2)

{
                    /* 0x2d70  700   */
  *param_1 = (param_2 ^ *param_1) & 0xffffff ^ *param_1;
  return;
}



/* ===================================================== */
/* FUN_00a02d90 @ 00a02d90 */
/* ===================================================== */

uint FUN_00a02d90(int param_1,int param_2)

{
                    /* 0x2d90  692   */
  return param_1 >> 4 & 0x3fU | param_2 << 6;
}



/* ===================================================== */
/* FUN_00a02db0 @ 00a02db0 */
/* ===================================================== */

uint FUN_00a02db0(uint param_1,uint param_2,int param_3,uint param_4)

{
                    /* 0x2db0  693   */
  return ((param_1 & 3) << 2 | param_2 & 3) << 5 |
         (int)(param_4 & 0x100 | param_3 >> 2 & 0xf0U) >> 4;
}



/* ===================================================== */
/* FUN_00a02df0 @ 00a02df0 */
/* ===================================================== */

bool FUN_00a02df0(uint *param_1)

{
                    /* 0x2df0  671   */
  return (*param_1 & 0xffffff) == 0xffffff;
}



/* ===================================================== */
/* FUN_00a02e10 @ 00a02e10 */
/* ===================================================== */

undefined4 FUN_00a02e10(void)

{
                    /* 0x2e10  31  
                       0x2e10  32  
                       0x2e10  83  
                       0x2e10  84  
                       0x2e10  93  
                       0x2e10  98  
                       0x2e10  99  
                       0x2e10  108  
                       0x2e10  109  
                       0x2e10  111  
                       0x2e10  134  
                       0x2e10  143  
                       0x2e10  192  
                       0x2e10  248  
                       0x2e10  255  
                       0x2e10  559  
                       0x2e10  560  
                       0x2e10  664  
                       0x2e10  675  
                       0x2e10  763  
                       0x2e10  765  
                       0x2e10  772   */
  return 0;
}



/* ===================================================== */
/* FUN_00a02e20 @ 00a02e20 */
/* ===================================================== */

undefined4 FUN_00a02e20(void)

{
                    /* 0x2e20  2  
                       0x2e20  3  
                       0x2e20  5  
                       0x2e20  27  
                       0x2e20  45  
                       0x2e20  54  
                       0x2e20  70  
                       0x2e20  72  
                       0x2e20  82  
                       0x2e20  89  
                       0x2e20  90  
                       0x2e20  95  
                       0x2e20  100  
                       0x2e20  101  
                       0x2e20  103  
                       0x2e20  104  
                       0x2e20  106  
                       0x2e20  110  
                       0x2e20  112  
                       0x2e20  113  
                       0x2e20  114  
                       0x2e20  115  
                       0x2e20  117  
                       0x2e20  122  
                       0x2e20  124  
                       0x2e20  125  
                       0x2e20  140  
                       0x2e20  141  
                       0x2e20  142  
                       0x2e20  146  
                       0x2e20  147  
                       0x2e20  148  
                       0x2e20  149  
                       0x2e20  150  
                       0x2e20  162  
                       0x2e20  247  
                       0x2e20  606  
                       0x2e20  635  
                       0x2e20  667  
                       0x2e20  670  
                       0x2e20  679  
                       0x2e20  681  
                       0x2e20  682  
                       0x2e20  683  
                       0x2e20  685  
                       0x2e20  686  
                       0x2e20  690  
                       0x2e20  764  
                       0x2e20  766  
                       0x2e20  768  
                       0x2e20  769  
                       0x2e20  770  
                       0x2e20  773   */
  return 0;
}



/* ===================================================== */
/* FUN_00a02e30 @ 00a02e30 */
/* ===================================================== */

void FUN_00a02e30(void)

{
                    /* 0x2e30  87  
                       0x2e30  160  
                       0x2e30  169  
                       0x2e30  173  
                       0x2e30  175  
                       0x2e30  176  
                       0x2e30  177  
                       0x2e30  178  
                       0x2e30  198  
                       0x2e30  200  
                       0x2e30  208  
                       0x2e30  209  
                       0x2e30  211  
                       0x2e30  212  
                       0x2e30  243  
                       0x2e30  252  
                       0x2e30  257  
                       0x2e30  258  
                       0x2e30  260  
                       0x2e30  262  
                       0x2e30  263  
                       0x2e30  264  
                       0x2e30  265  
                       0x2e30  266  
                       0x2e30  267  
                       0x2e30  268  
                       0x2e30  269  
                       0x2e30  270  
                       0x2e30  271  
                       0x2e30  294  
                       0x2e30  295  
                       0x2e30  297  
                       0x2e30  299  
                       0x2e30  300  
                       0x2e30  301  
                       0x2e30  302  
                       0x2e30  303  
                       0x2e30  304  
                       0x2e30  305  
                       0x2e30  306  
                       0x2e30  307  
                       0x2e30  308  
                       0x2e30  380  
                       0x2e30  382  
                       0x2e30  383  
                       0x2e30  444  
                       0x2e30  448  
                       0x2e30  449  
                       0x2e30  492  
                       0x2e30  495  
                       0x2e30  496  
                       0x2e30  497  
                       0x2e30  500  
                       0x2e30  503  
                       0x2e30  504  
                       0x2e30  578  
                       0x2e30  580  
                       0x2e30  614  
                       0x2e30  620  
                       0x2e30  625  
                       0x2e30  649  
                       0x2e30  735   */
  return;
}



/* ===================================================== */
/* FUN_00a02e40 @ 00a02e40 */
/* ===================================================== */

void FUN_00a02e40(void)

{
                    /* 0x2e40  55  
                       0x2e40  77  
                       0x2e40  86  
                       0x2e40  144  
                       0x2e40  145  
                       0x2e40  174  
                       0x2e40  182  
                       0x2e40  183  
                       0x2e40  184  
                       0x2e40  193  
                       0x2e40  195  
                       0x2e40  201  
                       0x2e40  204  
                       0x2e40  237  
                       0x2e40  259  
                       0x2e40  261  
                       0x2e40  272  
                       0x2e40  296  
                       0x2e40  298  
                       0x2e40  309  
                       0x2e40  363  
                       0x2e40  364  
                       0x2e40  365  
                       0x2e40  446  
                       0x2e40  447  
                       0x2e40  506  
                       0x2e40  582  
                       0x2e40  587  
                       0x2e40  589  
                       0x2e40  607  
                       0x2e40  608  
                       0x2e40  609  
                       0x2e40  611  
                       0x2e40  612  
                       0x2e40  613  
                       0x2e40  615  
                       0x2e40  621  
                       0x2e40  622  
                       0x2e40  711  
                       0x2e40  748  
                       0x2e40  767   */
  return;
}



/* ===================================================== */
/* GetD3DDC @ 00a02e50 */
/* ===================================================== */

uint GetD3DDC(void)

{
  int iVar1;
  undefined1 local_4 [4];
  
                    /* 0x2e50  777  GetD3DDC */
  iVar1 = (**(code **)(*DAT_011be760 + 0xc4))();
  return ~-(uint)(iVar1 != 0) & (uint)local_4;
}



/* ===================================================== */
/* Release3DDC @ 00a02e80 */
/* ===================================================== */

void Release3DDC(undefined4 param_1)

{
                    /* 0x2e80  786  Release3DDC */
  (**(code **)(*DAT_011be760 + 200))(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a02ea0 @ 00a02ea0 */
/* ===================================================== */

void FUN_00a02ea0(undefined1 param_1,int param_2)

{
  uint *puVar1;
  
  puVar1 = DAT_00dedbd8;
  *DAT_00dedbd8 = param_2 << 0x18 | *DAT_00dedbd8 & 0xffffff;
  *(undefined1 *)((int)puVar1 + 7) = param_1;
  DAT_00dedbd8 = DAT_00dedbd8 + param_2 + 1;
  return;
}



/* ===================================================== */
/* CreateTranslationTables @ 00a02f50 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void CreateTranslationTables(float param_1)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  ushort uVar4;
  undefined1 uVar5;
  uint uVar6;
  int iVar7;
  uint uVar8;
  ushort *local_1c;
  short *local_18;
  uint local_14;
  
                    /* 0x2f50  756  CreateTranslationTables */
  if (param_1 != _DAT_00a2c228) {
    iVar7 = 0;
    _DAT_00a2c228 = param_1;
    _DAT_00a8d078 = 0;
    _DAT_00aad078 = 0;
    _DAT_00acd078 = 0;
    _DAT_00add078 = 0;
    do {
      uVar5 = Gamma(iVar7,(double)param_1);
      (&DAT_00b2d080)[iVar7] = uVar5;
      iVar7 = iVar7 + 1;
    } while (iVar7 < 0x100);
    local_18 = &DAT_00add07a;
    local_14 = 1;
    local_1c = &DAT_00acd07a;
    param_1 = 0.0;
    do {
      uVar6 = (int)local_14 >> 2 & 0xf8;
      bVar1 = (&DAT_00b2d080)[uVar6];
      uVar8 = (int)local_14 >> 7 & 0xf8;
      bVar2 = (&DAT_00b2d080)[uVar8];
      iVar7 = (local_14 & 0x1f) * 8;
      bVar3 = (&DAT_00b2d080)[iVar7];
      uVar4 = (ushort)((int)(uint)bVar2 >> 4) | bVar1 & 0xf0;
      *(ushort *)(&DAT_00a8d07a + (int)param_1) =
           CONCAT11(7,bVar3 & 0xf0) << 4 | ~(ushort)local_14 & 0xf000 | uVar4;
      *(ushort *)(&DAT_00aad07a + (int)param_1) =
           (bVar1 & 0xf8 | CONCAT11(0xff,bVar3 & 0xf8) << 5) << 2 | (ushort)((int)(uint)bVar2 >> 3);
      if ((int)param_1 < 0xfffe) {
        *local_1c = CONCAT11(0xff,bVar3 & 0xf0) << 4 | uVar4;
        *local_18 = (short)(((uVar8 + uVar6 + iVar7) / 3 & 0xf0) << 8) + 1;
      }
      local_14 = local_14 + 1;
      param_1 = (float)((int)param_1 + 2);
      local_1c = local_1c + 1;
      local_18 = local_18 + 1;
    } while ((int)param_1 < 0x1fffe);
  }
  return;
}



/* ===================================================== */
/* FUN_00a03110 @ 00a03110 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a03110(void)

{
  float fVar1;
  float *pfVar2;
  float *pfVar3;
  uint uVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  undefined4 *puVar8;
  undefined4 *puVar9;
  HWND unaff_retaddr;
  int local_180 [13];
  uint uStack_14c;
  
  GetCommandLineA();
  pfVar2 = (float *)&DAT_00a2f478;
  iVar7 = 0x120;
  iVar6 = -0x81600;
  do {
    iVar5 = 4000;
    pfVar3 = pfVar2;
    local_180[0] = iVar6;
    do {
      fVar1 = (float)local_180[0];
      pfVar2 = pfVar3 + 1;
      local_180[0] = local_180[0] + iVar7;
      iVar5 = iVar5 + -1;
      *pfVar3 = fVar1 * _DAT_00a2a024;
      pfVar3 = pfVar2;
    } while (iVar5 != 0);
    iVar6 = iVar6 + -0xe600;
    iVar7 = iVar7 + 0x20;
  } while (-0x1da600 < iVar6);
  (**(code **)(*DAT_011be760 + 8))(0);
  iVar6 = Malloc(0x21020);
  if (iVar6 == 0) {
    DAT_012141d4 = 0;
  }
  else {
    DAT_012141d4 = FUN_00a27650();
  }
  DAT_00a2f470 = unaff_retaddr;
  DAT_00b6d194 = SetWindowLongA(unaff_retaddr,-4,0xa02ee0);
  if (_DAT_00a2c228 == _DAT_00a2a028) {
    CreateTranslationTables(0x3f800000);
  }
  if (DAT_011be738 == 0) {
    local_180[0] = 0x17c;
    (**(code **)(*DAT_011be760 + 0xdc))(local_180,0);
    DAT_00b2d07c = (**(code **)(*DAT_011be760 + 0x28))();
    iVar6 = (**(code **)(*DAT_011be760 + 0x24))();
    iVar7 = (**(code **)(*DAT_011be760 + 0x20))();
    if (((iVar7 < 0x11) && (0x10 < iVar6)) && (uStack_14c < 0x700000)) {
      iVar6 = 0;
    }
    iVar7 = (**(code **)(*DAT_011be760 + 0x4c))();
    puVar8 = (undefined4 *)(iVar7 + 100);
    puVar9 = &DAT_00a2f438;
    for (iVar5 = 0xe; iVar5 != 0; iVar5 = iVar5 + -1) {
      *puVar9 = *puVar8;
      puVar8 = puVar8 + 1;
      puVar9 = puVar9 + 1;
    }
    DAT_00b2d180 = DAT_00a2f458 & 0x20;
    uVar4 = DAT_00a2f448 & 0x10;
    if ((uVar4 == 0) || (DAT_00a2f430 = '\x01', (DAT_00a2f44c & 0x20) == 0)) {
      DAT_00a2f430 = '\0';
    }
    if (((uVar4 == 0) || ((DAT_00a2f44c & 0x20) == 0)) || (DAT_00a2f432 = 1, iVar6 == 0)) {
      DAT_00a2f432 = 0;
    }
    if (((DAT_00a2f448 & 2) == 0) || (DAT_00a2f431 = '\x01', (DAT_00a2f44c & 2) == 0)) {
      DAT_00a2f431 = '\0';
    }
    if ((uVar4 == 0) || (DAT_00a2f433 = 1, (DAT_00a2f44c & 2) == 0)) {
      DAT_00a2f433 = 0;
    }
    FUN_00a03db0(0);
    if ((DAT_00a2f454 & 0xc000) == 0) {
      _DAT_00b2d184 = 1;
    }
    if ((DAT_00a2f460 & 8) == 0) {
      (**(code **)(*DAT_011be760 + 0x94))(2);
      DAT_00b2d188 = 1;
    }
    else {
      (**(code **)(*DAT_011be760 + 0x94))(4);
    }
    if (((DAT_00a2f454 & 0x1000) == 0) && ((DAT_00a2f454 & 0x2000) != 0)) {
      DAT_00a2c5a9 = 0x5f;
      return;
    }
    if ((DAT_00a2f430 != '\0') && (DAT_00a2f431 == '\0')) {
      DAT_00a2c5a9 = 0x5f;
      return;
    }
  }
  else {
    puVar8 = &DAT_00a2f438;
    for (iVar6 = 0xe; iVar6 != 0; iVar6 = iVar6 + -1) {
      *puVar8 = 0;
      puVar8 = puVar8 + 1;
    }
    DAT_00b2d07c = 0x10;
    DAT_00a2f433 = 1;
    DAT_00a2f432 = 1;
    DAT_00a2f431 = '\x01';
    DAT_00a2f430 = '\x01';
    DAT_00a2f454 = 0x5000;
    DAT_00b2d180 = 0;
  }
  return;
}



/* ===================================================== */
/* FUN_00a033c0 @ 00a033c0 */
/* ===================================================== */

void FUN_00a033c0(void)

{
  int iVar1;
  
  if (DAT_00b6d194 != 0) {
    SetWindowLongA(DAT_00a2f470,-4,DAT_00b6d194);
  }
  iVar1 = DAT_012141d4;
  if (DAT_012141d4 != 0) {
    FUN_00a276d0();
    Free(iVar1);
  }
  DAT_012141d4 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a03400 @ 00a03400 */
/* ===================================================== */

int FUN_00a03400(int param_1)

{
  if (((&DAT_00a2f430)[param_1] == '\0') &&
     (param_1 = *(int *)(&DAT_00a2c218 + param_1 * 4), (&DAT_00a2f430)[param_1] == '\0')) {
    param_1 = -1;
  }
  return param_1;
}



/* ===================================================== */
/* FUN_00a03430 @ 00a03430 */
/* ===================================================== */

int FUN_00a03430(int param_1)

{
  int iVar1;
  
  iVar1 = FUN_00a03400(param_1 >> 5 & 3);
  if (iVar1 == -1) {
    return -1;
  }
  return (-(uint)(iVar1 != 2) & 0xfffffffe) + 2;
}



/* ===================================================== */
/* FUN_00a03460 @ 00a03460 */
/* ===================================================== */

int * FUN_00a03460(uint param_1,uint param_2,ushort *param_3)

{
  undefined1 uVar1;
  undefined4 uVar2;
  byte bVar3;
  ushort uVar4;
  uint uVar5;
  int iVar6;
  uint *puVar7;
  byte bVar9;
  int *piVar8;
  undefined1 *puVar10;
  ushort *puVar11;
  uint uVar12;
  uint *puVar13;
  undefined4 *puVar14;
  int iVar15;
  char *pcVar16;
  undefined4 *puVar17;
  uint *puVar18;
  short *psVar19;
  undefined4 local_444;
  uint local_440;
  int local_43c;
  undefined2 local_438;
  ushort local_436;
  undefined2 uStack_434;
  undefined2 uStack_432;
  int local_430;
  undefined4 local_42c;
  undefined4 local_428;
  undefined4 local_424;
  undefined4 local_420;
  undefined4 local_41c;
  uint local_18;
  int local_14;
  char *local_10;
  char *local_c;
  uint *local_8;
  
  uVar12 = param_2;
  uVar5 = (int)param_2 >> 7 & 3;
  if (uVar5 == 0) {
    param_2 = 4;
  }
  else {
    param_2 = (-(uint)(uVar5 != 1) & 8) + 8;
  }
  local_8 = *(uint **)((int)param_3 + 4);
  iVar6 = *(int *)param_3;
  local_43c = *(int *)((int)param_3 + 0xc) - (int)local_8;
  local_18 = *(int *)((int)param_3 + 8) - iVar6;
  param_3 = (ushort *)0x0;
  if (((uVar12 & 0x1000) != 0) && (Zoom8BitTextures != 0)) {
    local_18 = (int)local_18 >> ((byte)Zoom8BitTextures & 0x1f);
    param_3 = (ushort *)Zoom8BitTextures;
    local_43c = local_43c >> ((byte)Zoom8BitTextures & 0x1f);
  }
  local_440 = local_18;
  local_444 = 0x28;
  local_438 = 1;
  local_436 = 0x18;
  if (param_2 != 0x10) {
    local_436 = (ushort)param_2;
  }
  uStack_434 = 0;
  uStack_432 = 0;
  local_42c = 0;
  local_428 = 0;
  local_424 = 0;
  local_420 = 0;
  iVar6 = param_2 * iVar6;
  local_430 = (uint)(local_436 >> 3) * local_43c * local_18;
  puVar7 = local_8 + (uVar12 & 0x10) * 4;
  iVar6 = ((int)(iVar6 + (iVar6 >> 0x1f & 7U)) >> 3) + (uVar12 & 0xf) * 0x80;
  if (param_2 == 4) {
    local_10 = (char *)((int)local_18 >> 1);
    local_8 = (uint *)((int)&DAT_00aed078 + (local_43c + -1) * (int)local_10);
    puVar14 = (undefined4 *)((int)puVar7 * 0x800 + iVar6 + EXT_00b6dbd0);
    for (iVar6 = local_43c; local_c = local_10, iVar6 != 0; iVar6 = iVar6 + -1) {
      uVar12 = (uint)local_10 >> 2;
      puVar7 = local_8;
      puVar17 = puVar14;
      do {
        uVar2 = *puVar17;
        puVar17 = puVar17 + 1;
        bVar3 = (byte)((uint)uVar2 >> 8);
        uVar4 = CONCAT11(bVar3 << 4 | bVar3 >> 4,(byte)uVar2 << 4 | (byte)uVar2 >> 4);
        bVar3 = (byte)((uint)uVar2 >> 0x10);
        bVar9 = (byte)((uint)uVar2 >> 0x18);
        uVar12 = uVar12 - 1;
        *puVar7 = CONCAT22(uVar4,CONCAT11(bVar9 << 4 | bVar9 >> 4,bVar3 << 4 | bVar3 >> 4)) << 0x10
                  | (uint)uVar4;
        puVar7 = puVar7 + 1;
      } while (uVar12 != 0);
      local_8 = (uint *)((int)local_8 - (int)local_10);
      puVar14 = puVar14 + 0x200;
    }
  }
  else if (param_2 == 8) {
    local_8 = (uint *)((int)puVar7 * 0x800 + iVar6 + EXT_00b6dbd0);
    puVar7 = (uint *)((int)&DAT_00aed078 + (local_43c + -1) * local_18);
    iVar6 = local_43c;
    if (param_3 == (ushort *)0x0) {
      for (; iVar6 != 0; iVar6 = iVar6 + -1) {
        puVar13 = local_8;
        puVar18 = puVar7;
        for (uVar12 = local_18 >> 2; uVar12 != 0; uVar12 = uVar12 - 1) {
          *puVar18 = *puVar13;
          puVar13 = puVar13 + 1;
          puVar18 = puVar18 + 1;
        }
        puVar7 = (uint *)((int)puVar7 - local_18);
        for (uVar12 = local_18 & 3; uVar12 != 0; uVar12 = uVar12 - 1) {
          *(char *)puVar18 = (char)*puVar13;
          puVar13 = (uint *)((int)puVar13 + 1);
          puVar18 = (uint *)((int)puVar18 + 1);
        }
        local_8 = local_8 + 0x200;
      }
    }
    else if (param_3 == (ushort *)0x1) {
      puVar13 = local_8;
      param_3 = (ushort *)local_43c;
      if (0 < local_43c) {
        do {
          iVar6 = 0;
          puVar18 = puVar13;
          if (0 < (int)local_18) {
            do {
              *(char *)((int)puVar7 + iVar6) = (char)*puVar18;
              iVar6 = iVar6 + 1;
              puVar18 = (uint *)((int)puVar18 + 2);
            } while (iVar6 < (int)local_18);
          }
          puVar7 = (uint *)((int)puVar7 - local_18);
          param_3 = (ushort *)((int)param_3 + -1);
          puVar13 = puVar13 + 0x400;
        } while (param_3 != (ushort *)0x0);
      }
    }
    else if ((param_3 == (ushort *)0x2) &&
            (puVar13 = local_8, param_3 = (ushort *)local_43c, 0 < local_43c)) {
      do {
        iVar6 = 0;
        puVar18 = puVar13;
        if (0 < (int)local_18) {
          do {
            *(char *)(iVar6 + (int)puVar7) = (char)*puVar18;
            iVar6 = iVar6 + 1;
            puVar18 = puVar18 + 1;
          } while (iVar6 < (int)local_18);
        }
        puVar7 = (uint *)((int)puVar7 - local_18);
        param_3 = (ushort *)((int)param_3 + -1);
        puVar13 = puVar13 + 0x800;
      } while (param_3 != (ushort *)0x0);
    }
  }
  else if (param_2 == 0x10) {
    local_10 = (char *)(local_18 * 3);
    if ((param_1 & 0x8000) == 0) {
      local_c = (char *)((int)&DAT_00aed078 + (local_43c + -1) * (int)local_10);
      puVar11 = (ushort *)((int)puVar7 * 0x800 + iVar6 + EXT_00b6dbd0);
      uVar12 = local_18;
      puVar7 = (uint *)local_43c;
      param_3 = puVar11;
      pcVar16 = local_c;
      if (local_43c != 0) {
        do {
          do {
            local_8 = puVar7;
            uVar4 = *puVar11;
            uVar5 = uVar4 & 0x7c1f;
            local_c[2] = (char)uVar5 << 3;
            *local_c = (char)(uVar5 >> 8) << 1;
            puVar11 = puVar11 + 1;
            local_c[1] = (char)((uVar4 & 0x3e0) >> 2);
            uVar12 = uVar12 - 1;
            local_c = local_c + 3;
            puVar7 = local_8;
          } while (uVar12 != 0);
          local_c = pcVar16 + local_18 * -3;
          puVar11 = param_3 + 0x400;
          uVar12 = local_18;
          puVar7 = (uint *)((int)local_8 + -1);
          param_3 = puVar11;
          pcVar16 = local_c;
        } while ((int)local_8 + -1 != 0);
        local_8 = (uint *)0x0;
      }
    }
    else {
      local_8 = (uint *)((int)&DAT_00aed078 + (local_43c + -1) * (int)local_10);
      puVar11 = (ushort *)((int)puVar7 * 0x800 + iVar6 + EXT_00b6dbd0);
      uVar12 = local_18;
      pcVar16 = (char *)local_43c;
      param_3 = puVar11;
      puVar7 = local_8;
      if (local_43c != 0) {
        do {
          do {
            local_c = pcVar16;
            uVar4 = *puVar11;
            if ((uVar4 & 0x8000) == 0) {
              uVar5 = uVar4 & 0x7c1f;
              *(char *)((int)local_8 + 2) = (char)uVar5 << 3;
              *(char *)local_8 = (char)(uVar5 >> 8) << 1;
              puVar11 = puVar11 + 1;
              *(char *)((int)local_8 + 1) = (char)((uVar4 & 0x3e0) >> 2);
            }
            else {
              *local_8 = 0;
            }
            uVar12 = uVar12 - 1;
            local_8 = (uint *)((int)local_8 + 3);
            pcVar16 = local_c;
          } while (uVar12 != 0);
          local_8 = (uint *)((int)puVar7 + local_18 * -3);
          puVar11 = param_3 + 0x400;
          uVar12 = local_18;
          pcVar16 = (char *)((int)local_c + -1);
          param_3 = puVar11;
          puVar7 = local_8;
        } while ((int)local_c + -1 != 0);
        local_c = (char *)0x0;
      }
    }
  }
  if (param_2 == 0x10) {
    param_2 = 0;
  }
  else {
    iVar15 = 1 << (sbyte)param_2;
    psVar19 = (short *)((((int)param_1 >> 6 & 0x1ffU) * 0x40 + (param_1 & 0x3f)) * 0x20 +
                       EXT_00b6dbd0);
    param_2 = -1;
    puVar14 = &local_41c;
    for (iVar6 = 0x100; iVar6 != 0; iVar6 = iVar6 + -1) {
      *puVar14 = 0;
      puVar14 = puVar14 + 1;
    }
    if ((param_1 & 0x8000) == 0) {
      iVar6 = 0;
      if (0 < iVar15) {
        puVar10 = (undefined1 *)((int)&local_41c + 1);
        do {
          uVar12 = (uint)*psVar19;
          psVar19 = psVar19 + 1;
          if ((uVar12 == 0) && (param_2 == -1)) {
            param_2 = iVar6;
          }
          puVar10[1] = (&DAT_00b2d080)[(uVar12 & 0x1f) * 8];
          iVar6 = iVar6 + 1;
          uVar1 = (&DAT_00b2d080)[(int)uVar12 >> 7 & 0xf8];
          *puVar10 = (&DAT_00b2d080)[(int)uVar12 >> 2 & 0xf8];
          puVar10[-1] = uVar1;
          puVar10 = puVar10 + 4;
        } while (iVar6 < iVar15);
      }
    }
    else {
      iVar6 = 0;
      if (0 < iVar15) {
        puVar10 = (undefined1 *)((int)&local_41c + 1);
        do {
          uVar12 = (uint)*psVar19;
          psVar19 = psVar19 + 1;
          if ((uVar12 == 0) && (param_2 == -1)) {
            param_2 = iVar6;
          }
          if ((uVar12 & 0x8000) == 0) {
            puVar10[1] = (&DAT_00b2d080)[(uVar12 & 0x1f) * 8];
            uVar1 = (&DAT_00b2d080)[(int)uVar12 >> 7 & 0xf8];
            *puVar10 = (&DAT_00b2d080)[(int)uVar12 >> 2 & 0xf8];
            puVar10[-1] = uVar1;
          }
          else {
            puVar10[-1] = 0;
            *puVar10 = 0;
            puVar10[1] = 0;
          }
          iVar6 = iVar6 + 1;
          puVar10 = puVar10 + 4;
        } while (iVar6 < iVar15);
      }
    }
  }
  piVar8 = (int *)(**(code **)(*DAT_011be760 + 0x10))();
  puVar14 = (undefined4 *)*piVar8;
  iVar6 = (*(code *)puVar14[2])(&local_444,&DAT_00aed078);
  if (iVar6 < 0) {
    if (piVar8 != (int *)0x0) {
      (*(code *)*puVar14)(1);
    }
    piVar8 = (int *)0x0;
  }
  if (((piVar8 != (int *)0x0) && (param_2 != -1)) && (DAT_00b2d07c < 0x10)) {
    iVar6 = *piVar8;
    iVar15 = (**(code **)(iVar6 + 0x1c))();
    if (iVar15 == 0) {
      param_2 = 0;
    }
    local_14 = param_2;
    local_10 = (char *)param_2;
    (**(code **)(iVar6 + 0x18))(&local_14);
  }
  return piVar8;
}



/* ===================================================== */
/* FUN_00a03980 @ 00a03980 */
/* ===================================================== */

int * FUN_00a03980(uint param_1,uint param_2,int *param_3,undefined *param_4)

{
  byte *pbVar1;
  undefined4 *puVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  undefined2 *puVar6;
  int *piVar7;
  int iVar8;
  undefined2 *puVar9;
  int iVar10;
  int iVar11;
  int iVar12;
  int local_230;
  undefined4 local_220;
  undefined4 local_21c;
  undefined4 local_214;
  undefined4 local_210;
  undefined4 local_20c;
  undefined4 local_208;
  undefined4 local_204;
  undefined2 local_200 [256];
  
  uVar3 = (int)param_2 >> 7 & 3;
  if (uVar3 == 0) {
    iVar10 = 4;
  }
  else {
    iVar10 = (-(uint)(uVar3 != 1) & 8) + 8;
  }
  local_220 = 0x20;
  local_21c = 0x41;
  iVar8 = (((int)param_1 >> 6 & 0x1ffU) * 0x40 + (param_1 & 0x3f)) * 0x20;
  iVar4 = param_3[3] - param_3[1];
  iVar12 = param_3[2] - *param_3;
  iVar5 = iVar10 * *param_3;
  local_214 = 0x10;
  iVar11 = (param_2 & 0x10) * 0x10 + param_3[1];
  iVar5 = ((int)(iVar5 + (iVar5 >> 0x1f & 7U)) >> 3) + (param_2 & 0xf) * 0x80;
  if (param_4 == &DAT_00aad078) {
    local_210 = 0x7c00;
    local_20c = 0x3e0;
    local_208 = 0x1f;
    local_204 = 0x8000;
  }
  else {
    local_210 = 0xf00;
    local_20c = 0xf0;
    local_208 = 0xf;
    local_204 = 0xf000;
  }
  if (iVar10 == 4) {
    local_230 = 0x100;
    puVar9 = local_200;
    do {
      *puVar9 = *(undefined2 *)
                 (param_4 +
                 (uint)*(ushort *)(((EXT_00b6dbd0 + iVar8) - (int)local_200) + (int)puVar9) * 2);
      local_230 = local_230 + -1;
      puVar9 = puVar9 + 1;
    } while (local_230 != 0);
    puVar9 = &DAT_00aed078;
    iVar10 = iVar11 * 0x800 + iVar5 + EXT_00b6dbd0;
    if (iVar4 != 0) {
      local_230 = iVar4;
      do {
        iVar5 = 0;
        puVar6 = puVar9;
        if (0 < iVar12 >> 1) {
          do {
            *puVar6 = local_200[*(byte *)(iVar5 + iVar10) & 0xf];
            pbVar1 = (byte *)(iVar5 + iVar10);
            iVar5 = iVar5 + 1;
            puVar6[1] = local_200[*pbVar1 >> 4];
            puVar6 = puVar6 + 2;
          } while (iVar5 < iVar12 >> 1);
        }
        iVar10 = iVar10 + 0x800;
        local_230 = local_230 + -1;
        puVar9 = puVar9 + iVar12;
      } while (local_230 != 0);
    }
  }
  else if (iVar10 == 8) {
    local_230 = 0x100;
    puVar9 = local_200;
    do {
      *puVar9 = *(undefined2 *)
                 (param_4 +
                 (uint)*(ushort *)(((EXT_00b6dbd0 + iVar8) - (int)local_200) + (int)puVar9) * 2);
      local_230 = local_230 + -1;
      puVar9 = puVar9 + 1;
    } while (local_230 != 0);
    puVar9 = &DAT_00aed078;
    iVar5 = iVar11 * 0x800 + iVar5 + EXT_00b6dbd0;
    for (iVar10 = iVar4; iVar10 != 0; iVar10 = iVar10 + -1) {
      iVar8 = 0;
      puVar6 = puVar9;
      if (0 < iVar12) {
        do {
          pbVar1 = (byte *)(iVar8 + iVar5);
          iVar8 = iVar8 + 1;
          *puVar6 = local_200[*pbVar1];
          puVar6 = puVar6 + 1;
        } while (iVar8 < iVar12);
      }
      iVar5 = iVar5 + 0x800;
      puVar9 = puVar9 + iVar12;
    }
  }
  else if (iVar10 == 0x10) {
    puVar9 = &DAT_00aed078;
    iVar5 = iVar11 * 0x800 + iVar5 + EXT_00b6dbd0;
    for (iVar10 = iVar4; iVar10 != 0; iVar10 = iVar10 + -1) {
      if (0 < iVar12) {
        puVar6 = puVar9;
        iVar8 = iVar12;
        do {
          iVar8 = iVar8 + -1;
          *puVar6 = *(undefined2 *)
                     (param_4 + (uint)*(ushort *)((iVar5 - (int)puVar9) + (int)puVar6) * 2);
          puVar6 = puVar6 + 1;
        } while (iVar8 != 0);
      }
      iVar5 = iVar5 + 0x800;
      puVar9 = puVar9 + iVar12;
    }
  }
  piVar7 = (int *)(**(code **)(*DAT_011be760 + 0x10))();
  puVar2 = (undefined4 *)*piVar7;
  iVar10 = (*(code *)puVar2[3])(iVar12,iVar4,&DAT_00aed078,&local_220);
  if ((piVar7 != (int *)0x0) && (DAT_00b2d07c < 0x10)) {
    (*(code *)puVar2[6])(&stack0xfffffdc0);
  }
  if (iVar10 < 0) {
    if (piVar7 != (int *)0x0) {
      (*(code *)*puVar2)(1);
    }
    piVar7 = (int *)0x0;
  }
  return piVar7;
}



/* ===================================================== */
/* FUN_00a03c80 @ 00a03c80 */
/* ===================================================== */

bool FUN_00a03c80(uint param_1)

{
  int iVar1;
  int iVar2;
  undefined4 uVar3;
  
  iVar1 = FUN_00a03400((int)param_1 >> 5 & 3);
  if (iVar1 == -1) {
    FUN_00a03db0(param_1 >> 0xd & 1);
    iVar1 = FUN_00a03430(param_1);
    return iVar1 != -1;
  }
  if (DAT_00a2c210 == iVar1) {
switchD_00a03d11_default:
    return true;
  }
  if ((DAT_00a2f454 & 0x1000) == 0) {
    if ((DAT_00a2f454 & 0x2000) == 0) {
      return false;
    }
    (**(code **)(*DAT_011be760 + 0x9c))(1);
    iVar2 = 0;
  }
  else {
    (**(code **)(*DAT_011be760 + 0x98))(1);
    iVar2 = iVar1;
  }
  DAT_00a2c210 = iVar1;
  switch(iVar2) {
  case 0:
  case 2:
    (**(code **)(*DAT_011be760 + 0xa0))(5);
    (**(code **)(*DAT_011be760 + 0xa4))(6);
    return true;
  case 1:
    uVar3 = 2;
    iVar1 = *DAT_011be760;
    break;
  case 3:
    uVar3 = 5;
    iVar1 = *DAT_011be760;
    break;
  case 4:
    uVar3 = 3;
    iVar1 = *DAT_011be760;
    break;
  default:
    goto switchD_00a03d11_default;
  }
  (**(code **)(iVar1 + 0xa0))(uVar3);
  (**(code **)(*DAT_011be760 + 0xa4))(2);
  return true;
}



/* ===================================================== */
/* FUN_00a03db0 @ 00a03db0 */
/* ===================================================== */

void FUN_00a03db0(int param_1)

{
  if ((DAT_00b2d07c < 0x10) || (param_1 != 0)) {
    if (DAT_00a2c210 != -1) {
      DAT_00a2c210 = -1;
      (**(code **)(*DAT_011be760 + 0x98))(0);
      (**(code **)(*DAT_011be760 + 0x9c))(0);
      (**(code **)(*DAT_011be760 + 0xa0))(2);
      (**(code **)(*DAT_011be760 + 0xa4))(1);
    }
  }
  else if (DAT_00a2c210 != -2) {
    DAT_00a2c210 = 0xfffffffe;
    (**(code **)(*DAT_011be760 + 0x98))(1);
    (**(code **)(*DAT_011be760 + 0xa0))(5);
    (**(code **)(*DAT_011be760 + 0xa4))(6);
    return;
  }
  return;
}



/* ===================================================== */
/* FUN_00a03e70 @ 00a03e70 */
/* ===================================================== */

void FUN_00a03e70(int param_1,undefined4 *param_2,int param_3)

{
  int iVar1;
  undefined4 *puVar2;
  
                    /* 0x3e70  793   */
  if (Elo_NoLighting == 0) {
    if (param_3 != 0) {
      iVar1 = param_1 + 8;
      puVar2 = param_2 + 1;
      do {
        FUN_00a24000(param_1,iVar1,iVar1 + 8,&EXT_00b6d198,param_2,puVar2,puVar2 + 1);
        param_1 = param_1 + 0x18;
        iVar1 = iVar1 + 0x18;
        param_2 = param_2 + 3;
        puVar2 = puVar2 + 3;
        param_3 = param_3 + -1;
      } while (param_3 != 0);
    }
  }
  else {
    param_3 = param_3 * 3;
    if (param_3 != 0) {
      for (; param_3 != 0; param_3 = param_3 + -1) {
        *param_2 = 0;
        param_2 = param_2 + 1;
      }
      return;
    }
  }
  return;
}



/* ===================================================== */
/* EnableKBInput @ 00a03ef0 */
/* ===================================================== */

undefined4 EnableKBInput(undefined4 param_1)

{
  undefined4 uVar1;
  
                    /* 0x3ef0  775  EnableKBInput */
  uVar1 = DAT_00b6dbcc;
  DAT_00b6d5a0 = 0;
  DAT_00b6d5a4 = 0;
  DAT_00b6dbcc = param_1;
  return uVar1;
}



/* ===================================================== */
/* GetKBChar @ 00a03f10 */
/* ===================================================== */

undefined1 GetKBChar(void)

{
  undefined *puVar1;
  
                    /* 0x3f10  778  GetKBChar */
  if (DAT_00b6dbcc == 0) {
    return 0;
  }
  if (DAT_00b6d5a0 == DAT_00b6d5a4) {
    return 0;
  }
  puVar1 = &DAT_00b6d1a0 + DAT_00b6d5a0;
  DAT_00b6d5a0 = DAT_00b6d5a0 + 1 & 0x3ff;
  return *puVar1;
}



/* ===================================================== */
/* FUN_00a03f50 @ 00a03f50 */
/* ===================================================== */

void FUN_00a03f50(undefined1 param_1)

{
  if (DAT_00b6dbcc != 0) {
    (&DAT_00b6d1a0)[DAT_00b6d5a4] = param_1;
    DAT_00b6d5a4 = DAT_00b6d5a4 + 1 & 0x3ff;
  }
  return;
}



/* ===================================================== */
/* FUN_00a03f80 @ 00a03f80 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 * FUN_00a03f80(char *param_1)

{
  char cVar1;
  int iVar2;
  char *pcVar3;
  uint uVar4;
  uint uVar5;
  char *pcVar6;
  undefined1 *puStack_11c;
  undefined1 local_108 [4];
  undefined1 local_104 [260];
  
  DAT_00dedc58 = *(undefined4 *)param_1;
  _DAT_00dedc5c = *(undefined2 *)(param_1 + 4);
  DAT_00dedc5e = 0;
  puStack_11c = (undefined1 *)0xa03fbb;
  iVar2 = lstrcmpiA((LPCSTR)&DAT_00dedc58,s_cdrom__00a2c3a8);
  if (iVar2 == 0) {
    puStack_11c = local_104;
    Ordinal_4(param_1,local_108);
    Ordinal_5(&DAT_00dedc58,&puStack_11c,&stack0xfffffee8,param_1 + 6,0);
    return &DAT_00dedc58;
  }
  uVar4 = 0xffffffff;
  do {
    pcVar3 = param_1;
    if (uVar4 == 0) break;
    uVar4 = uVar4 - 1;
    pcVar3 = param_1 + 1;
    cVar1 = *param_1;
    param_1 = pcVar3;
  } while (cVar1 != '\0');
  uVar4 = ~uVar4;
  pcVar3 = pcVar3 + -uVar4;
  pcVar6 = (char *)&DAT_00dedc58;
  for (uVar5 = uVar4 >> 2; uVar5 != 0; uVar5 = uVar5 - 1) {
    *(undefined4 *)pcVar6 = *(undefined4 *)pcVar3;
    pcVar3 = pcVar3 + 4;
    pcVar6 = pcVar6 + 4;
  }
  for (uVar4 = uVar4 & 3; uVar4 != 0; uVar4 = uVar4 - 1) {
    *pcVar6 = *pcVar3;
    pcVar3 = pcVar3 + 1;
    pcVar6 = pcVar6 + 1;
  }
  puStack_11c = (undefined1 *)0xa04029;
  pcVar3 = _strchr((char *)((int)&DAT_00dedc58 + 2),0x3a);
  if (pcVar3 != (char *)0x0) {
    *pcVar3 = '~';
  }
  return &DAT_00dedc58;
}



/* ===================================================== */
/* FUN_00a04050 @ 00a04050 */
/* ===================================================== */

undefined4 FUN_00a04050(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x4050  1   */
  switch(param_1) {
  default:
    return 0;
  case 0xf2000003:
    DAT_00dedc14 = 0;
    DAT_00dedc08 = param_3;
    return 1;
  }
}



/* ===================================================== */
/* FUN_00a040a0 @ 00a040a0 */
/* ===================================================== */

undefined4 FUN_00a040a0(int param_1)

{
                    /* 0x40a0  4   */
  if (((param_1 != -0xe000000) && (param_1 != -0xdffffff)) && (param_1 == -0xdfffffd)) {
    DAT_00dedc14 = 1;
    return 1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a040d0 @ 00a040d0 */
/* ===================================================== */

undefined4 *
FUN_00a040d0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
  undefined4 *puVar1;
  
                    /* 0x40d0  6   */
  puVar1 = (undefined4 *)Malloc(0x1c);
  if (puVar1 == (undefined4 *)0x0) {
    return (undefined4 *)0xffffffff;
  }
  *puVar1 = param_1;
  puVar1[2] = param_2;
  puVar1[1] = 0x1000;
  puVar1[3] = param_3;
  puVar1[4] = param_4;
  puVar1[5] = DAT_00dedc20;
  puVar1[6] = 0;
  if (DAT_00dedc24 == (undefined4 *)0x0) {
    DAT_00dedc24 = puVar1;
  }
  DAT_00dedc20 = puVar1;
  return puVar1;
}



/* ===================================================== */
/* FUN_00a04130 @ 00a04130 */
/* ===================================================== */

undefined4 FUN_00a04130(int param_1)

{
  int iVar1;
  int iVar2;
  
                    /* 0x4130  7   */
  iVar1 = *(int *)(param_1 + 0x18);
  iVar2 = *(int *)(param_1 + 0x14);
  if (iVar1 != 0) {
    *(int *)(iVar1 + 0x14) = iVar2;
  }
  if (iVar2 != 0) {
    *(int *)(iVar2 + 0x18) = iVar1;
  }
  if (DAT_00dedc24 == param_1) {
    DAT_00dedc24 = iVar1;
  }
  if (DAT_00dedc20 == param_1) {
    DAT_00dedc20 = iVar2;
  }
  Free(param_1);
  return 1;
}



/* ===================================================== */
/* FUN_00a04180 @ 00a04180 */
/* ===================================================== */

undefined4 FUN_00a04180(int param_1)

{
                    /* 0x4180  8   */
  if (*(int *)(param_1 + 4) != 0x4000) {
    if (*(int *)(param_1 + 4) != 0x2000) {
      return 0;
    }
    if (*(int *)(param_1 + 4) != 0x4000) {
      WaitForSingleObject(DAT_011be794,0xffffffff);
    }
  }
  *(undefined4 *)(param_1 + 4) = 0x2000;
  return 1;
}



/* ===================================================== */
/* FUN_00a041d0 @ 00a041d0 */
/* ===================================================== */

undefined4 FUN_00a041d0(int *param_1)

{
                    /* 0x41d0  9   */
  ResetEvent(DAT_011be794);
  if ((param_1[1] != 0x4000) &&
     (((*param_1 != -0xbffffff && (*param_1 != -0xfffffef)) || (param_1[2] != 4)))) {
    return 0;
  }
  param_1[1] = 0x2000;
  return 1;
}



/* ===================================================== */
/* FUN_00a04220 @ 00a04220 */
/* ===================================================== */

undefined4 FUN_00a04220(int param_1)

{
                    /* 0x4220  10   */
  if (*(int *)(param_1 + 4) == 0x1000) {
    *(undefined4 *)(param_1 + 4) = 0x2000;
    return 1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a04250 @ 00a04250 */
/* ===================================================== */

undefined4 FUN_00a04250(int param_1)

{
                    /* 0x4250  11   */
  if (*(int *)(param_1 + 4) != 0x1000) {
    *(undefined4 *)(param_1 + 4) = 0x1000;
    return 1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a04270 @ 00a04270 */
/* ===================================================== */

void FUN_00a04270(int param_1,int param_2)

{
  int *piVar1;
  int *piVar2;
  
                    /* 0x4270  12   */
  piVar2 = DAT_00dedc24;
  if (DAT_00dedc24 != (int *)0x0) {
    while (((*piVar2 != param_1 || (piVar2[2] != param_2)) || (piVar2[1] != 0x2000))) {
      piVar1 = piVar2 + 6;
      piVar2 = (int *)*piVar1;
      if ((int *)*piVar1 == (int *)0x0) {
        return;
      }
    }
    if (piVar2[3] == 0x2000) {
      piVar2[1] = 0x4000;
      SetEvent(DAT_011be794);
      return;
    }
    (*(code *)piVar2[4])();
  }
  return;
}



/* ===================================================== */
/* FUN_00a042d0 @ 00a042d0 */
/* ===================================================== */

void FUN_00a042d0(int param_1,int param_2)

{
  int *piVar1;
  
                    /* 0x42d0  13   */
  piVar1 = DAT_00dedc24;
  ResetEvent(DAT_011be794);
  if (piVar1 != (int *)0x0) {
    while (((piVar1[1] != 0x4000 || (*piVar1 != param_1)) || (piVar1[2] != param_2))) {
      piVar1 = (int *)piVar1[6];
      if (piVar1 == (int *)0x0) {
        return;
      }
    }
    if (piVar1[3] == 0x2000) {
      piVar1[1] = 0x2000;
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a04320 @ 00a04320 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 * FUN_00a04320(void)

{
  undefined4 *puVar1;
  HANDLE pvVar2;
  DWORD DVar3;
  
                    /* 0x4320  14   */
  puVar1 = (undefined4 *)Malloc(0xc0);
  if (puVar1 == (undefined4 *)0x0) {
    return (undefined4 *)0xffffffff;
  }
  *puVar1 = 0x1000;
  puVar1[1] = 0;
  puVar1[0x2b] = 0;
  puVar1[0x2a] = DAT_00dedc28;
  if (DAT_00dedc2c == (undefined4 *)0x0) {
    DAT_00dedc2c = puVar1;
  }
  DAT_00dedc28 = puVar1;
  pvVar2 = GetCurrentThread();
  puVar1[0x2c] = pvVar2;
  DVar3 = GetCurrentThreadId();
  puVar1[0x2d] = DVar3;
  _DAT_00dedc34 = puVar1;
  return puVar1;
}



/* ===================================================== */
/* FUN_00a04390 @ 00a04390 */
/* ===================================================== */

undefined4 * FUN_00a04390(LPTHREAD_START_ROUTINE param_1)

{
  undefined4 *puVar1;
  HANDLE pvVar2;
  
                    /* 0x4390  15   */
  puVar1 = (undefined4 *)Malloc(0xc0);
  if (puVar1 == (undefined4 *)0x0) {
    return (undefined4 *)0xffffffff;
  }
  *puVar1 = 0x1000;
  puVar1[1] = 0;
  puVar1[0x2b] = 0;
  puVar1[0x2a] = DAT_00dedc28;
  if (DAT_00dedc2c == (undefined4 *)0x0) {
    DAT_00dedc2c = puVar1;
  }
  DAT_00dedc28 = puVar1;
  pvVar2 = CreateThread((LPSECURITY_ATTRIBUTES)0x0,0,param_1,(LPVOID)0x0,4,puVar1 + 0x2d);
  puVar1[0x2c] = pvVar2;
  return puVar1;
}



/* ===================================================== */
/* FUN_00a04400 @ 00a04400 */
/* ===================================================== */

undefined4 FUN_00a04400(undefined4 *param_1)

{
                    /* 0x4400  16   */
  *param_1 = 0x8000;
  ResumeThread((HANDLE)param_1[0x2c]);
  return 1;
}



/* ===================================================== */
/* FUN_00a04420 @ 00a04420 */
/* ===================================================== */

undefined4 FUN_00a04420(int *param_1)

{
  int *piVar1;
  int *piVar2;
  int *piVar3;
  
                    /* 0x4420  17   */
  piVar3 = DAT_00dedc30;
  *param_1 = 0x4000;
  *DAT_00dedc30 = 0x1000;
  DAT_00dedc30 = param_1;
  ResumeThread((HANDLE)param_1[0x2c]);
  SuspendThread((HANDLE)piVar3[0x2c]);
  Sleep(0);
  if (*piVar3 == 0x8000) {
    piVar1 = (int *)piVar3[0x2b];
    piVar2 = (int *)piVar3[0x2a];
    if (piVar1 != (int *)0x0) {
      piVar1[0x2a] = (int)piVar2;
    }
    if (piVar2 != (int *)0x0) {
      piVar2[0x2b] = (int)piVar1;
    }
    if (DAT_00dedc2c == piVar3) {
      DAT_00dedc2c = piVar1;
    }
    if (DAT_00dedc28 == piVar3) {
      DAT_00dedc28 = piVar2;
    }
    Free(piVar3);
                    /* WARNING: Subroutine does not return */
    ExitThread(0);
  }
  return 1;
}



/* ===================================================== */
/* FUN_00a044c0 @ 00a044c0 */
/* ===================================================== */

char * FUN_00a044c0(undefined4 param_1,char *param_2)

{
  char cVar1;
  LPCSTR lpFileName;
  uint uVar2;
  uint uVar3;
  CHAR *pCVar4;
  CHAR *pCVar5;
  char *pcVar6;
  _WIN32_FIND_DATAA *lpFindFileData;
  _WIN32_FIND_DATAA local_140;
  
  lpFindFileData = &local_140;
                    /* 0x44c0  18   */
  FindClose(DAT_00a2c250);
  lpFileName = (LPCSTR)FUN_00a03f80(param_1);
  DAT_00a2c250 = FindFirstFileA(lpFileName,lpFindFileData);
  if (DAT_00a2c250 == (HANDLE)0xffffffff) {
    return (char *)0x0;
  }
  pCVar4 = local_140.cFileName;
  if (local_140.cFileName[4] == '~') {
    pCVar4 = local_140.cFileName + 5;
  }
  else if (local_140.cFileName[5] == '~') {
    pCVar4 = local_140.cFileName + 6;
  }
  uVar2 = 0xffffffff;
  pCVar4[0x13] = '\0';
  do {
    pCVar5 = pCVar4;
    if (uVar2 == 0) break;
    uVar2 = uVar2 - 1;
    pCVar5 = pCVar4 + 1;
    cVar1 = *pCVar4;
    pCVar4 = pCVar5;
  } while (cVar1 != '\0');
  uVar2 = ~uVar2;
  pCVar4 = pCVar5 + -uVar2;
  pcVar6 = param_2;
  for (uVar3 = uVar2 >> 2; uVar3 != 0; uVar3 = uVar3 - 1) {
    *(undefined4 *)pcVar6 = *(undefined4 *)pCVar4;
    pCVar4 = pCVar4 + 4;
    pcVar6 = pcVar6 + 4;
  }
  for (uVar2 = uVar2 & 3; uVar2 != 0; uVar2 = uVar2 - 1) {
    *pcVar6 = *pCVar4;
    pCVar4 = pCVar4 + 1;
    pcVar6 = pcVar6 + 1;
  }
  *(DWORD *)(param_2 + 0x18) = local_140.nFileSizeLow;
  return param_2;
}



/* ===================================================== */
/* FUN_00a04560 @ 00a04560 */
/* ===================================================== */

void FUN_00a04560(char *param_1)

{
  char cVar1;
  BOOL BVar2;
  uint uVar3;
  uint uVar4;
  CHAR *pCVar5;
  CHAR *pCVar6;
  char *pcVar7;
  _WIN32_FIND_DATAA local_140;
  
                    /* 0x4560  19   */
  BVar2 = FindNextFileA(DAT_00a2c250,&local_140);
  if (BVar2 == 0) {
    return;
  }
  pCVar5 = local_140.cFileName;
  if (local_140.cFileName[4] == '~') {
    pCVar5 = local_140.cFileName + 5;
  }
  else if (local_140.cFileName[5] == '~') {
    pCVar5 = local_140.cFileName + 6;
  }
  uVar3 = 0xffffffff;
  pCVar5[0x13] = '\0';
  do {
    pCVar6 = pCVar5;
    if (uVar3 == 0) break;
    uVar3 = uVar3 - 1;
    pCVar6 = pCVar5 + 1;
    cVar1 = *pCVar5;
    pCVar5 = pCVar6;
  } while (cVar1 != '\0');
  uVar3 = ~uVar3;
  pCVar5 = pCVar6 + -uVar3;
  pcVar7 = param_1;
  for (uVar4 = uVar3 >> 2; uVar4 != 0; uVar4 = uVar4 - 1) {
    *(undefined4 *)pcVar7 = *(undefined4 *)pCVar5;
    pCVar5 = pCVar5 + 4;
    pcVar7 = pcVar7 + 4;
  }
  for (uVar3 = uVar3 & 3; uVar3 != 0; uVar3 = uVar3 - 1) {
    *pcVar7 = *pCVar5;
    pCVar5 = pCVar5 + 1;
    pcVar7 = pcVar7 + 1;
  }
  *(DWORD *)(param_1 + 0x18) = local_140.nFileSizeLow;
  return;
}



/* ===================================================== */
/* FUN_00a045e0 @ 00a045e0 */
/* ===================================================== */

bool FUN_00a045e0(undefined4 param_1)

{
  LPCSTR lpFileName;
  BOOL BVar1;
  
                    /* 0x45e0  20   */
  lpFileName = (LPCSTR)FUN_00a03f80(param_1);
  BVar1 = DeleteFileA(lpFileName);
  return BVar1 == 0;
}



/* ===================================================== */
/* FUN_00a04600 @ 00a04600 */
/* ===================================================== */

void FUN_00a04600(undefined4 param_1,uint param_2)

{
  LPCSTR lpFileName;
  
                    /* 0x4600  21   */
  lpFileName = (LPCSTR)FUN_00a03f80(param_1);
  CreateFileA(lpFileName,(param_2 * 4 | param_2 & 2) << 0x1d,0,(LPSECURITY_ATTRIBUTES)0x0,
              ((param_2 & 0x200) != 0) + 3,0,(HANDLE)0x0);
  return;
}



/* ===================================================== */
/* FUN_00a04650 @ 00a04650 */
/* ===================================================== */

void FUN_00a04650(HANDLE param_1)

{
                    /* 0x4650  22   */
  CloseHandle(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a04660 @ 00a04660 */
/* ===================================================== */

void FUN_00a04660(HANDLE param_1,LONG param_2,DWORD param_3)

{
                    /* 0x4660  23  
                       0x4660  333   */
  SetFilePointer(param_1,param_2,(PLONG)0x0,param_3);
  return;
}



/* ===================================================== */
/* FUN_00a04680 @ 00a04680 */
/* ===================================================== */

DWORD FUN_00a04680(HANDLE param_1,LPVOID param_2,DWORD param_3)

{
  BOOL BVar1;
  
                    /* 0x4680  24   */
  BVar1 = ReadFile(param_1,param_2,param_3,&param_3,(LPOVERLAPPED)0x0);
  if (BVar1 != 0) {
    return param_3;
  }
  return 0xffffffff;
}



/* ===================================================== */
/* FUN_00a046b0 @ 00a046b0 */
/* ===================================================== */

DWORD FUN_00a046b0(HANDLE param_1,LPCVOID param_2,DWORD param_3)

{
  BOOL BVar1;
  
                    /* 0x46b0  25   */
  BVar1 = WriteFile(param_1,param_2,param_3,&param_3,(LPOVERLAPPED)0x0);
  if (BVar1 != 0) {
    return param_3;
  }
  return 0xffffffff;
}



/* ===================================================== */
/* FUN_00a046e0 @ 00a046e0 */
/* ===================================================== */

undefined4 FUN_00a046e0(void)

{
                    /* 0x46e0  26  
                       0x46e0  33  
                       0x46e0  34  
                       0x46e0  97  
                       0x46e0  102  
                       0x46e0  105  
                       0x46e0  107  
                       0x46e0  126  
                       0x46e0  249   */
  return 0;
}



/* ===================================================== */
/* FUN_00a046f0 @ 00a046f0 */
/* ===================================================== */

undefined4 FUN_00a046f0(void)

{
                    /* 0x46f0  28  
                       0x46f0  73   */
  return 1;
}



/* ===================================================== */
/* FUN_00a04700 @ 00a04700 */
/* ===================================================== */

void FUN_00a04700(undefined4 param_1,undefined4 param_2)

{
  LPCSTR lpExistingFileName;
  LPCSTR lpNewFileName;
  
                    /* 0x4700  29   */
  lpExistingFileName = (LPCSTR)FUN_00a03f80(param_1);
  lpNewFileName = (LPCSTR)FUN_00a03f80(param_2);
  MoveFileA(lpExistingFileName,lpNewFileName);
  return;
}



/* ===================================================== */
/* FUN_00a04730 @ 00a04730 */
/* ===================================================== */

void FUN_00a04730(undefined4 param_1)

{
  LPCSTR lpPathName;
  
                    /* 0x4730  30   */
  lpPathName = (LPCSTR)FUN_00a03f80(param_1);
  SetCurrentDirectoryA(lpPathName);
  return;
}



/* ===================================================== */
/* FUN_00a04750 @ 00a04750 */
/* ===================================================== */

void FUN_00a04750(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x4750  760   */
  FUN_00a04770(param_1,7,param_3,7);
  return;
}



/* ===================================================== */
/* FUN_00a04770 @ 00a04770 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_00a04770(undefined4 *param_1,uint param_2,undefined4 *param_3,uint param_4)

{
  MMRESULT MVar1;
  uint uVar2;
  undefined4 *puVar3;
  
                    /* 0x4770  35   */
  if (param_1 != (undefined4 *)0x0) {
    _DAT_00b6dbc8 = param_2;
    DAT_00dedc38 = param_1;
    for (uVar2 = param_2 >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
      *param_1 = 0;
      param_1 = param_1 + 1;
    }
    for (param_2 = param_2 & 3; param_2 != 0; param_2 = param_2 - 1) {
      *(undefined1 *)param_1 = 0;
      param_1 = (undefined4 *)((int)param_1 + 1);
    }
    *(undefined1 *)((int)DAT_00dedc38 + 1) = 0x41;
  }
  MVar1 = joyGetDevCapsA(0,(LPJOYCAPSA)&DAT_00b6d890,0x194);
  if (MVar1 != 0) {
    _DAT_00b6d6f0 = 0;
    DAT_00b6d5b8 = 0;
    DAT_00b6d5bc = 0;
    DAT_00b6dbbc = 0;
    DAT_00b6d5b4 = 0;
    DAT_00b6d5cc = 0;
    DAT_00b6dbb8 = 0;
    if (param_3 == (undefined4 *)0x0) {
      return 0;
    }
    puVar3 = param_3;
    for (uVar2 = param_4 >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
      *puVar3 = 0;
      puVar3 = puVar3 + 1;
    }
    for (param_4 = param_4 & 3; param_4 != 0; param_4 = param_4 - 1) {
      *(undefined1 *)puVar3 = 0;
      puVar3 = (undefined4 *)((int)puVar3 + 1);
    }
    DAT_00dedc3c = param_3;
    *(undefined1 *)param_3 = 0xff;
    *(undefined1 *)((int)DAT_00dedc3c + 1) = 0xff;
    return 1;
  }
  DAT_00b6d5b8 = (uint)(DAT_00b6d8b8 - DAT_00b6d8b4) >> 1;
  DAT_00b6d5bc = (uint)(DAT_00b6d8c0 - DAT_00b6d8bc) >> 1;
  DAT_00b6d5b4 = (DAT_00b6d8b8 - DAT_00b6d5b8 >> 1) + DAT_00b6d5b8;
  DAT_00b6dbbc = DAT_00b6d5b8 - (DAT_00b6d5b8 - DAT_00b6d8b4 >> 1);
  DAT_00b6dbb8 = (DAT_00b6d8c0 - DAT_00b6d5bc >> 1) + DAT_00b6d5bc;
  DAT_00b6d5cc = DAT_00b6d5bc - (DAT_00b6d5bc - DAT_00b6d8bc >> 1);
  _DAT_00b6d6f0 = 1;
  DAT_00dedc3c = param_3;
  if (param_3 == (undefined4 *)0x0) {
    return 0;
  }
  _DAT_00b6d5ac = param_4;
  for (uVar2 = param_4 >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
    *param_3 = 0;
    param_3 = param_3 + 1;
  }
  for (param_4 = param_4 & 3; param_4 != 0; param_4 = param_4 - 1) {
    *(undefined1 *)param_3 = 0;
    param_3 = (undefined4 *)((int)param_3 + 1);
  }
  *(undefined1 *)((int)DAT_00dedc3c + 1) = 0x41;
  MVar1 = joyGetDevCapsA(1,(LPJOYCAPSA)&DAT_00b6d6f8,0x194);
  if (MVar1 == 0) {
    DAT_00b6d5c0 = (uint)(DAT_00b6d720 - DAT_00b6d71c) >> 1;
    DAT_00b6d5c4 = (uint)(DAT_00b6d728 - DAT_00b6d724) >> 1;
    DAT_00b6d5a8 = (DAT_00b6d720 - DAT_00b6d5c0 >> 1) + DAT_00b6d5c0;
    DAT_00b6d5d0 = DAT_00b6d5c4 - (DAT_00b6d5c4 - DAT_00b6d724 >> 1);
    DAT_00b6d5c8 = DAT_00b6d5c0 - (DAT_00b6d5c0 - DAT_00b6d71c >> 1);
    DAT_00b6dbc4 = (DAT_00b6d728 - DAT_00b6d5c4 >> 1) + DAT_00b6d5c4;
    _DAT_00b6dbc0 = 1;
    return 1;
  }
  *(undefined1 *)DAT_00dedc3c = 0xff;
  *(undefined1 *)((int)DAT_00dedc3c + 1) = 0xff;
  _DAT_00b6dbc0 = 0;
  DAT_00b6d5c0 = 0;
  DAT_00b6d5c4 = 0;
  DAT_00b6d5c8 = 0;
  DAT_00b6d5a8 = 0;
  DAT_00b6d5d0 = 0;
  DAT_00b6dbc4 = 0;
  return 1;
}



/* ===================================================== */
/* FUN_00a049c0 @ 00a049c0 */
/* ===================================================== */

uint FUN_00a049c0(int param_1)

{
  SHORT SVar1;
  MMRESULT MVar2;
  ushort uVar3;
  uint uVar4;
  joyinfo_tag local_10;
  
  if ((DAT_00dedc40 == 0) && (param_1 != 0)) {
    return 0;
  }
  SVar1 = GetAsyncKeyState(0x79);
  if (SVar1 != 0) {
    TexFilterPossible = (uint)(TexFilterPossible == 0);
    do {
      Sleep(100);
      SVar1 = GetAsyncKeyState(0x79);
    } while (SVar1 != 0);
  }
  SVar1 = GetAsyncKeyState(0x77);
  if (SVar1 != 0) {
    ReInitializeAllTextures();
    do {
      Sleep(100);
      SVar1 = GetAsyncKeyState(0x77);
    } while (SVar1 != 0);
  }
  uVar4 = 0;
  MVar2 = joyGetPos(0,&local_10);
  if (MVar2 != 0) {
    local_10.wXpos = DAT_00b6d5b8;
    local_10.wYpos = DAT_00b6d5bc;
    local_10.wButtons = 0;
  }
  if (DAT_00b6dbcc == 0) {
    if (((local_10.wXpos < DAT_00b6dbbc) || (SVar1 = GetAsyncKeyState(DAT_00a2c2d0), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2d4), SVar1 < 0)) {
      uVar4 = 0x8000;
    }
    if (((DAT_00b6dbb8 < local_10.wYpos) || (SVar1 = GetAsyncKeyState(DAT_00a2c2e8), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2ec), SVar1 < 0)) {
      uVar4 = (uint)(ushort)((ushort)uVar4 | 0x4000);
    }
    if (((DAT_00b6d5b4 < local_10.wXpos) || (SVar1 = GetAsyncKeyState(DAT_00a2c2d8), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2dc), SVar1 < 0)) {
      uVar4 = (uint)(ushort)((ushort)uVar4 | 0x2000);
    }
    if (((local_10.wYpos < DAT_00b6d5cc) || (SVar1 = GetAsyncKeyState(DAT_00a2c2e0), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2e4), SVar1 < 0)) {
      uVar4 = (uint)(ushort)((ushort)uVar4 | 0x1000);
    }
    if ((((local_10.wButtons & DAT_00a2c258) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c25c), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c260), SVar1 < 0)) {
      uVar4 = uVar4 | 0x80;
    }
    if ((((local_10.wButtons & DAT_00a2c264) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c268), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c26c), SVar1 < 0)) {
      uVar4 = uVar4 | 0x40;
    }
    if ((((local_10.wButtons & DAT_00a2c270) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c274), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c278), SVar1 < 0)) {
      uVar4 = uVar4 | 0x20;
    }
    if ((((local_10.wButtons & DAT_00a2c27c) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c280), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c284), SVar1 < 0)) {
      uVar4 = uVar4 | 0x10;
    }
    if ((((local_10.wButtons & DAT_00a2c288) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c28c), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c290), SVar1 < 0)) {
      uVar4 = uVar4 | 4;
    }
    if ((((local_10.wButtons & DAT_00a2c294) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c298), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c29c), SVar1 < 0)) {
      uVar4 = uVar4 | 8;
    }
    if ((((local_10.wButtons & DAT_00a2c2a0) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c2a4), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2a8), SVar1 < 0)) {
      uVar4 = uVar4 | 1;
    }
    if ((((local_10.wButtons & DAT_00a2c2ac) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c2b0), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2b4), SVar1 < 0)) {
      uVar4 = uVar4 | 2;
    }
    if ((((local_10.wButtons & DAT_00a2c2b8) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c2bc), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2c0), SVar1 < 0)) {
      uVar4 = (uint)(ushort)((ushort)uVar4 | 0x800);
    }
    if ((((local_10.wButtons & DAT_00a2c2c4) != 0) ||
        (SVar1 = GetAsyncKeyState(DAT_00a2c2c8), SVar1 < 0)) ||
       (SVar1 = GetAsyncKeyState(DAT_00a2c2cc), SVar1 < 0)) {
      uVar4 = (uint)(ushort)((ushort)uVar4 | 0x100);
    }
  }
  uVar3 = (ushort)uVar4;
  if (param_1 != 0) {
    if (DAT_00dedc38 != 0) {
      *(byte *)(DAT_00dedc38 + 2) = ~(byte)(uVar4 >> 8);
      *(byte *)(DAT_00dedc38 + 3) = ~(byte)uVar4;
    }
    MVar2 = joyGetPos(1,&local_10);
    if (MVar2 == 0) {
      local_10.wXpos = DAT_00b6d5c0;
      local_10.wYpos = DAT_00b6d5c4;
      local_10.wButtons = 0;
    }
    if (DAT_00b6dbcc == 0) {
      if (((local_10.wXpos < DAT_00b6d5c8) || (SVar1 = GetAsyncKeyState(DAT_00a2c368), SVar1 < 0))
         || (SVar1 = GetAsyncKeyState(DAT_00a2c36c), SVar1 < 0)) {
        uVar3 = uVar3 | 0x8000;
      }
      if (((DAT_00b6dbc4 < local_10.wYpos) || (SVar1 = GetAsyncKeyState(DAT_00a2c380), SVar1 < 0))
         || (SVar1 = GetAsyncKeyState(DAT_00a2c384), SVar1 < 0)) {
        uVar3 = uVar3 | 0x4000;
      }
      if (((DAT_00b6d5a8 < local_10.wXpos) || (SVar1 = GetAsyncKeyState(DAT_00a2c370), SVar1 < 0))
         || (SVar1 = GetAsyncKeyState(DAT_00a2c374), SVar1 < 0)) {
        uVar3 = uVar3 | 0x2000;
      }
      if (((local_10.wYpos < DAT_00b6d5d0) || (SVar1 = GetAsyncKeyState(DAT_00a2c378), SVar1 < 0))
         || (SVar1 = GetAsyncKeyState(DAT_00a2c37c), SVar1 < 0)) {
        uVar3 = uVar3 | 0x1000;
      }
      if ((((local_10.wButtons & DAT_00a2c2f0) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c2f4), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c2f8), SVar1 < 0)) {
        uVar3 = uVar3 | 0x80;
      }
      if ((((local_10.wButtons & DAT_00a2c2fc) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c300), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c304), SVar1 < 0)) {
        uVar3 = uVar3 | 0x40;
      }
      if ((((local_10.wButtons & DAT_00a2c308) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c30c), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c310), SVar1 < 0)) {
        uVar3 = uVar3 | 0x20;
      }
      if ((((local_10.wButtons & DAT_00a2c314) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c318), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c31c), SVar1 < 0)) {
        uVar3 = uVar3 | 0x10;
      }
      if ((((local_10.wButtons & DAT_00a2c320) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c324), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c328), SVar1 < 0)) {
        uVar3 = uVar3 | 4;
      }
      if ((((local_10.wButtons & DAT_00a2c32c) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c330), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c334), SVar1 < 0)) {
        uVar3 = uVar3 | 8;
      }
      if ((((local_10.wButtons & DAT_00a2c338) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c33c), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c340), SVar1 < 0)) {
        uVar3 = uVar3 | 1;
      }
      if ((((local_10.wButtons & DAT_00a2c344) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c348), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c34c), SVar1 < 0)) {
        uVar3 = uVar3 | 2;
      }
      if ((((local_10.wButtons & DAT_00a2c350) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c354), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c358), SVar1 < 0)) {
        uVar3 = uVar3 | 0x800;
      }
      if ((((local_10.wButtons & DAT_00a2c35c) != 0) ||
          (SVar1 = GetAsyncKeyState(DAT_00a2c360), SVar1 < 0)) ||
         (SVar1 = GetAsyncKeyState(DAT_00a2c364), SVar1 < 0)) {
        uVar3 = uVar3 | 0x100;
      }
    }
    if (DAT_00dedc3c != 0) {
      *(byte *)(DAT_00dedc3c + 2) = ~(byte)(uVar3 >> 8);
      *(byte *)(DAT_00dedc3c + 3) = ~(byte)uVar3;
    }
    return 0;
  }
  return uVar4;
}



/* ===================================================== */
/* FUN_00a04fb0 @ 00a04fb0 */
/* ===================================================== */

void FUN_00a04fb0(void)

{
                    /* 0x4fb0  36  
                       0x4fb0  759   */
  DAT_00dedc40 = 1;
  return;
}



/* ===================================================== */
/* FUN_00a04fc0 @ 00a04fc0 */
/* ===================================================== */

void FUN_00a04fc0(void)

{
                    /* 0x4fc0  37   */
  DAT_00dedc40 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a04fd0 @ 00a04fd0 */
/* ===================================================== */

void FUN_00a04fd0(void)

{
                    /* 0x4fd0  40   */
  EnterCriticalSection((LPCRITICAL_SECTION)&DAT_00b6d6d8);
  return;
}



/* ===================================================== */
/* FUN_00a04fe0 @ 00a04fe0 */
/* ===================================================== */

void FUN_00a04fe0(void)

{
                    /* 0x4fe0  41   */
  LeaveCriticalSection((LPCRITICAL_SECTION)&DAT_00b6d6d8);
  return;
}



/* ===================================================== */
/* FUN_00a04ff0 @ 00a04ff0 */
/* ===================================================== */

void FUN_00a04ff0(void)

{
  InitializeCriticalSection((LPCRITICAL_SECTION)&DAT_00b6d6d8);
  return;
}



/* ===================================================== */
/* FUN_00a05000 @ 00a05000 */
/* ===================================================== */

undefined4 FUN_00a05000(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x5000  51   */
  DAT_00dedc48 = param_1;
  DAT_00dedc4c = param_2;
  DAT_00dedc50 = param_3;
  return 1;
}



/* ===================================================== */
/* FUN_00a05030 @ 00a05030 */
/* ===================================================== */

void FUN_00a05030(undefined4 *param_1,undefined4 *param_2,undefined4 *param_3)

{
                    /* 0x5030  52   */
  *param_1 = DAT_00dedc48;
  *param_2 = DAT_00dedc4c;
  *param_3 = DAT_00dedc50;
  return;
}



/* ===================================================== */
/* FUN_00a05060 @ 00a05060 */
/* ===================================================== */

undefined * FUN_00a05060(uint param_1)

{
  int iVar1;
  
                    /* 0x5060  57   */
  if ((0x813f < param_1) && (param_1 < 0x9873)) {
    iVar1 = (int)*(short *)(DAT_00b6d5b0 + -0x10280 + param_1 * 2);
    if (iVar1 != -1) {
      return (undefined *)(iVar1 * 0x20 + 0x2e64 + DAT_00b6d5b0);
    }
  }
  return &DAT_00a2c388;
}



/* ===================================================== */
/* FUN_00a050a0 @ 00a050a0 */
/* ===================================================== */

void FUN_00a050a0(undefined2 param_1)

{
                    /* 0x50a0  58   */
  FUN_00a05060(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a050c0 @ 00a050c0 */
/* ===================================================== */

void FUN_00a050c0(void)

{
                    /* 0x50c0  61   */
                    /* WARNING: Subroutine does not return */
  ExitProcess(0);
}



/* ===================================================== */
/* FUN_00a050d0 @ 00a050d0 */
/* ===================================================== */

undefined4 FUN_00a050d0(void)

{
                    /* 0x50d0  68   */
  FUN_00a04270(0xf0000011,4);
  return 1;
}



/* ===================================================== */
/* FUN_00a050f0 @ 00a050f0 */
/* ===================================================== */

undefined4 FUN_00a050f0(void)

{
                    /* 0x50f0  67  
                       0x50f0  69   */
  FUN_00a04270(0xf4000001,4);
  return 1;
}



/* ===================================================== */
/* FUN_00a05110 @ 00a05110 */
/* ===================================================== */

undefined4 FUN_00a05110(void)

{
                    /* 0x5110  75  
                       0x5110  76   */
  FUN_00a04270(0xf0000011,4);
  return 1;
}



/* ===================================================== */
/* FUN_00a05130 @ 00a05130 */
/* ===================================================== */

void FUN_00a05130(void)

{
                    /* 0x5130  80  
                       0x5130  505  
                       0x5130  644   */
  return;
}



/* ===================================================== */
/* FUN_00a05140 @ 00a05140 */
/* ===================================================== */

void FUN_00a05140(void)

{
                    /* 0x5140  81  
                       0x5140  474  
                       0x5140  558   */
  return;
}



/* ===================================================== */
/* FUN_00a05150 @ 00a05150 */
/* ===================================================== */

undefined2 FUN_00a05150(void)

{
                    /* 0x5150  85  
                       0x5150  151  
                       0x5150  153  
                       0x5150  168  
                       0x5150  210  
                       0x5150  221  
                       0x5150  222  
                       0x5150  253   */
  return 0;
}



/* ===================================================== */
/* FUN_00a05160 @ 00a05160 */
/* ===================================================== */

undefined * FUN_00a05160(void)

{
                    /* 0x5160  92   */
  return &DAT_00dedd58;
}



/* ===================================================== */
/* FUN_00a05170 @ 00a05170 */
/* ===================================================== */

char * FUN_00a05170(undefined1 param_1)

{
                    /* 0x5170  94   */
  switch(param_1) {
  case 1:
    return "CdlNop";
  case 2:
    return "CdlSetloc";
  case 3:
    return "CdlPlay";
  case 4:
    return "CdlForward";
  case 5:
    return "CdlBackward";
  default:
    return (char *)0x0;
  }
}



/* ===================================================== */
/* FUN_00a051d0 @ 00a051d0 */
/* ===================================================== */

undefined4 FUN_00a051d0(undefined1 param_1)

{
                    /* 0x51d0  96   */
  switch(param_1) {
  case 1:
  case 2:
  case 3:
  case 4:
  case 5:
  case 6:
  case 7:
  case 8:
  case 9:
  case 0xb:
  case 0xc:
  case 0xd:
  case 0xe:
  case 0x10:
  case 0x11:
  case 0x15:
  case 0x16:
  case 0x1b:
    return 1;
  default:
    return 0;
  }
}



/* ===================================================== */
/* SetTimingMode @ 00a05260 */
/* ===================================================== */

undefined4 SetTimingMode(undefined4 param_1)

{
  uint uVar1;
  undefined4 uVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  
                    /* 0x5260  822  SetTimingMode */
  uVar2 = DAT_00a2c3b0;
  DAT_00a2c3b0 = param_1;
  QueryPerformanceCounter((LARGE_INTEGER *)&DAT_00dedd70);
  iVar5 = DAT_011be684;
  iVar4 = DAT_00dedd74;
  uVar3 = DAT_00dedd70;
  VSyncCounter = __alldiv(DAT_00dedd70,DAT_00dedd74,DAT_011be684,DAT_011be684 >> 0x1f);
  uVar1 = iVar5 * 3;
  DAT_011be680 = VSyncCounter + 3;
  DAT_011be690 = uVar1 + uVar3;
  DAT_011be694 = ((int)uVar1 >> 0x1f) + iVar4 + (uint)CARRY4(uVar1,uVar3);
  return uVar2;
}



/* ===================================================== */
/* GetTimingMode @ 00a052d0 */
/* ===================================================== */

undefined4 GetTimingMode(void)

{
                    /* 0x52d0  780  GetTimingMode */
  return DAT_00a2c3b0;
}



/* ===================================================== */
/* FUN_00a052e0 @ 00a052e0 */
/* ===================================================== */

void FUN_00a052e0(void)

{
  HANDLE hFile;
  DWORD DVar1;
  undefined4 *puVar2;
  int local_110;
  DWORD DStack_10c;
  DWORD DStack_108;
  DWORD local_104;
  undefined1 local_100 [256];
  
  hFile = CreateFileA(s_VS_Log_00a2c44c,0xc0000000,0,(LPSECURITY_ATTRIBUTES)0x0,2,0x80,(HANDLE)0x0);
  local_110 = 0;
  if (0 < DAT_011be69c) {
    puVar2 = &DAT_00dedd80;
    do {
      WriteFile(hFile,s__________________________00a2c430,0x1a,&local_104,(LPOVERLAPPED)0x0);
      DVar1 = Ordinal_1(local_100,s_VSyncCounter__d_VSyncShouldBe__d_00a2c3e4,puVar2[-2],puVar2[-1],
                        *puVar2,puVar2[1],puVar2[2],puVar2[3]);
      WriteFile(hFile,local_100,DVar1,&DStack_10c,(LPOVERLAPPED)0x0);
      DVar1 = Ordinal_1(local_100,s_LastDelayed__d_Wait__d_Delayed___00a2c3b4,puVar2[4],puVar2[5],
                        puVar2[6],puVar2[7]);
      WriteFile(hFile,local_100,DVar1,&DStack_108,(LPOVERLAPPED)0x0);
      puVar2 = puVar2 + 10;
      local_110 = local_110 + 1;
    } while (local_110 < DAT_011be69c);
  }
  CloseHandle(hFile);
  return;
}



/* ===================================================== */
/* FUN_00a053e0 @ 00a053e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

int FUN_00a053e0(int param_1)

{
  int iVar1;
  int iVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  uint uVar6;
  int iVar7;
  uint uVar8;
  int local_4;
  
                    /* 0x53e0  132   */
  local_4 = param_1;
  if (param_1 == 0) {
    local_4 = 1;
  }
  QueryPerformanceCounter((LARGE_INTEGER *)&DAT_00dedd70);
  iVar7 = DAT_011be684;
  iVar4 = DAT_00dedd74;
  uVar8 = DAT_00dedd70;
  VSyncCounter = __alldiv(DAT_00dedd70,DAT_00dedd74,DAT_011be684,DAT_011be684 >> 0x1f);
  uVar6 = DAT_011be690;
  iVar1 = DAT_011be680;
  if (param_1 < 0) {
    return VSyncCounter;
  }
  iVar2 = DAT_011be69c + 1;
  DAT_011be69c = iVar2;
  (&DAT_00dedd78)[iVar2 * 10] = VSyncCounter;
  iVar5 = DAT_011be694;
  (&DAT_00dedd7c)[iVar2 * 10] = iVar1;
  iVar1 = DAT_011be688;
  (&DAT_00dedd80)[iVar2 * 10] = uVar8;
  (&DAT_00dedd84)[iVar2 * 10] = iVar4;
  (&DAT_00dedd88)[iVar2 * 10] = uVar6;
  (&DAT_00dedd8c)[iVar2 * 10] = iVar5;
  (&DAT_00dedd90)[iVar2 * 10] = iVar1;
  (&DAT_00dedd98)[iVar2 * 10] = 0;
  (&DAT_00dedd94)[iVar2 * 10] = 0;
  (&DAT_00dedd9c)[iVar2 * 10] = local_4;
  switch(DAT_00a2c3b0) {
  case 0:
switchD_00a054b8_caseD_0:
    return (-(uint)(param_1 != 0) & 0xee) + 1;
  case 1:
    if ((DAT_011be688 == 0) && (iVar4 <= iVar5)) {
      if ((iVar4 < iVar5) || (uVar8 < uVar6)) {
        (&DAT_00dedd94)[iVar2 * 10] = uVar6 - uVar8;
      }
      if ((iVar4 <= iVar5) && ((iVar4 < iVar5 || (uVar8 < uVar6)))) {
        do {
          do {
            PollMessages();
            QueryPerformanceCounter((LARGE_INTEGER *)&DAT_00dedd70);
            iVar7 = DAT_011be684;
            iVar4 = DAT_00dedd74;
            uVar8 = DAT_00dedd70;
            VSyncCounter = __alldiv(DAT_00dedd70,DAT_00dedd74,DAT_011be684,DAT_011be684 >> 0x1f);
          } while (iVar4 < DAT_011be694);
          iVar5 = DAT_011be694;
          uVar6 = DAT_011be690;
        } while ((iVar4 <= DAT_011be694) && (uVar8 < DAT_011be690));
      }
    }
    DAT_011be680 = DAT_011be680 + local_4;
    uVar3 = local_4 * iVar7;
    DAT_011be694 = iVar5 + ((int)uVar3 >> 0x1f) + (uint)CARRY4(uVar6,uVar3);
    if (DAT_011be680 + 0x1e < VSyncCounter) {
      DAT_011be680 = local_4 + VSyncCounter;
      DAT_011be694 = iVar4 + ((int)uVar3 >> 0x1f) + (uint)CARRY4(uVar8,uVar3);
      uVar6 = uVar8;
    }
    DAT_011be690 = uVar6 + uVar3;
    if (param_1 != 1) {
      PollMessages();
      if (VSyncCounter <= DAT_011be680) {
        if (DAT_011be688 != 0) {
          DAT_011be688 = DAT_011be688 + -1;
        }
        VSyncDelayed = 0;
        _DAT_011be678 = VSyncCounter;
        return 1;
      }
      DAT_011be688 = 3;
      VSyncDelayed = 1;
      _DAT_011be678 = VSyncCounter;
      (&DAT_00dedd98)[DAT_011be69c * 10] = VSyncCounter - DAT_011be680;
      return 1;
    }
    return 0xef;
  case 2:
    goto switchD_00a054b8_caseD_2;
  case 3:
    VSyncDelayed = (uint)(DAT_011be698 == 0);
    DAT_011be698 = VSyncDelayed;
    if (VSyncDelayed != 0) goto switchD_00a054b8_caseD_0;
    local_4 = local_4 * 2;
    VSyncDelayed = 0;
    DAT_011be698 = 0;
switchD_00a054b8_caseD_2:
    if (((DAT_011be688 == 0) && (iVar4 <= iVar5)) && ((iVar4 < iVar5 || (uVar8 < uVar6)))) {
      do {
        do {
          PollMessages();
          QueryPerformanceCounter((LARGE_INTEGER *)&DAT_00dedd70);
          iVar7 = DAT_011be684;
          iVar4 = DAT_00dedd74;
          uVar8 = DAT_00dedd70;
          VSyncCounter = __alldiv(DAT_00dedd70,DAT_00dedd74,DAT_011be684,DAT_011be684 >> 0x1f);
        } while (iVar4 < DAT_011be694);
        iVar5 = DAT_011be694;
        uVar6 = DAT_011be690;
      } while ((iVar4 <= DAT_011be694) && (uVar8 < DAT_011be690));
    }
    DAT_011be680 = DAT_011be680 + local_4;
    uVar3 = local_4 * iVar7;
    DAT_011be690 = uVar6 + uVar3;
    DAT_011be694 = iVar5 + ((int)uVar3 >> 0x1f) + (uint)CARRY4(uVar6,uVar3);
    if ((iVar4 <= DAT_011be694) && ((iVar4 < DAT_011be694 || (uVar8 < DAT_011be690)))) {
      DAT_011be690 = uVar8 + uVar3;
      DAT_011be694 = iVar4 + ((int)uVar3 >> 0x1f) + (uint)CARRY4(uVar8,uVar3);
      DAT_011be680 = local_4 + VSyncCounter;
    }
    iVar4 = 0xef;
    if (param_1 != 1) {
switchD_00a054b8_default:
      iVar4 = 1;
    }
    return iVar4;
  default:
    goto switchD_00a054b8_default;
  }
}



/* ===================================================== */
/* FUN_00a05740 @ 00a05740 */
/* ===================================================== */

undefined4 FUN_00a05740(undefined4 param_1)

{
                    /* 0x5740  133   */
  DAT_011be6a0 = param_1;
  return 1;
}



/* ===================================================== */
/* FUN_00a05760 @ 00a05760 */
/* ===================================================== */

undefined4 FUN_00a05760(void)

{
                    /* 0x5760  135   */
  return DAT_011be6a4;
}



/* ===================================================== */
/* FUN_00a05770 @ 00a05770 */
/* ===================================================== */

undefined4 FUN_00a05770(undefined4 param_1)

{
  undefined4 uVar1;
  
                    /* 0x5770  136   */
  uVar1 = DAT_011be6a4;
  DAT_011be6a4 = param_1;
  return uVar1;
}



/* ===================================================== */
/* FUN_00a05790 @ 00a05790 */
/* ===================================================== */

void FUN_00a05790(void)

{
                    /* 0x5790  128   */
  FUN_00a04770(0,0,0,0);
  return;
}



/* ===================================================== */
/* FUN_00a057a0 @ 00a057a0 */
/* ===================================================== */

void FUN_00a057a0(void)

{
                    /* 0x57a0  137   */
  FUN_00a049c0(0);
  return;
}



/* ===================================================== */
/* ShiftVertices @ 00a057b0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void ShiftVertices(float *param_1,int param_2)

{
  float fVar1;
  
                    /* 0x57b0  824  ShiftVertices */
  fVar1 = (float)((DAT_00a2c498 + 9) * 0x20) * _DAT_00a2a06c;
  if (0 < param_2) {
    do {
      param_2 = param_2 + -1;
      *param_1 = (float)DAT_011be770 + fVar1 * *param_1;
      param_1[1] = (float)DAT_011be774 + fVar1 * param_1[1];
      param_1 = param_1 + 8;
    } while (param_2 != 0);
  }
  return;
}



/* ===================================================== */
/* SetShift @ 00a05810 */
/* ===================================================== */

void SetShift(int param_1,int param_2,int param_3,int param_4)

{
                    /* 0x5810  796  SetShift */
  if (param_3 < 0) {
    param_3 = 0;
  }
  if (param_4 == 0) {
    (**(code **)(*DAT_011be760 + 0x50))
              (*(int *)(DAT_00a2c49c * 4 + 0xa2c4d8) + param_1,
               *(int *)(DAT_00a2c49c * 4 + 0xa2c4f0) + param_2,(param_3 + 9) * 0x20,
               (param_3 + 9) * 0x18);
  }
  DAT_00a2c498 = param_3;
  DAT_011be77c = param_2;
  DAT_011be778 = param_1;
  PTR_DAT_00a2c4a0 = &DAT_00a31138 + param_3 * 16000;
  PTR_DAT_00a2c4a4 = &DAT_00a31138 + param_3 * 16000;
  DAT_011be770 = *(int *)(DAT_00a2c49c * 4 + 0xa2c4d8) +
                 (DAT_011be780 * (param_3 + 9) * 0x20) / 0x140 + param_1;
  DAT_011be774 = *(int *)(DAT_00a2c49c * 4 + 0xa2c4f0) +
                 (DAT_011be784 * (param_3 + 9) * 0x20) / 0x140 + param_2;
  return;
}



/* ===================================================== */
/* GetShift @ 00a05900 */
/* ===================================================== */

void GetShift(undefined4 *param_1,undefined4 *param_2,undefined4 *param_3)

{
                    /* 0x5900  779  GetShift */
  *param_1 = DAT_011be778;
  *param_2 = DAT_011be77c;
  *param_3 = DAT_00a2c498;
  return;
}



/* ===================================================== */
/* GetAbsShift @ 00a05930 */
/* ===================================================== */

void GetAbsShift(undefined4 *param_1,undefined4 *param_2)

{
                    /* 0x5930  776  GetAbsShift */
  *param_1 = DAT_011be780;
  *param_2 = DAT_011be784;
  return;
}



/* ===================================================== */
/* SetAbsShift @ 00a05950 */
/* ===================================================== */

void SetAbsShift(int param_1,int param_2)

{
                    /* 0x5950  791  SetAbsShift */
  DAT_011be780 = param_1;
  DAT_011be784 = param_2;
  DAT_011be770 = (param_1 * (DAT_00a2c498 + 9) * 0x20) / 0x140 +
                 *(int *)(DAT_00a2c49c * 4 + 0xa2c4d8) + DAT_011be778;
  DAT_011be774 = (param_2 * (DAT_00a2c498 + 9) * 0x20) / 0x140 +
                 *(int *)(DAT_00a2c49c * 4 + 0xa2c4f0) + DAT_011be77c;
  return;
}



/* ===================================================== */
/* FUN_00a059e0 @ 00a059e0 */
/* ===================================================== */

void FUN_00a059e0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &DAT_011be6b8;
                    /* 0x59e0  656   */
  for (iVar1 = 5; iVar1 != 0; iVar1 = iVar1 + -1) {
    *param_1 = *puVar2;
    puVar2 = puVar2 + 1;
    param_1 = param_1 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a05a00 @ 00a05a00 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_00a05a00(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  undefined4 *puVar3;
  
  puVar2 = param_1;
  puVar3 = &DAT_011be6b8;
                    /* 0x5a00  657   */
  for (iVar1 = 5; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar3 = *puVar2;
    puVar2 = puVar2 + 1;
    puVar3 = puVar3 + 1;
  }
  if (*(short *)(param_1 + 1) < 0x141) {
    SetAbsShift(0x140 - DAT_011be6bc >> 1,0xf0 - _DAT_011be6be >> 1);
  }
  else {
    DAT_00a2c498 = 0;
    DAT_011be778 = 0;
    DAT_011be77c = 0;
    PTR_DAT_00a2c4a0 = &DAT_00a34fb8;
    PTR_DAT_00a2c4a4 = &DAT_00a5c0b8;
    DAT_011be770 = 0;
    DAT_011be774 = 0;
  }
  if ((DAT_00dedc44 == 0) && (VSyncDelayed == 0)) {
    (**(code **)(*DAT_011be760 + 0xd8))(1,0);
    (**(code **)(*DAT_011be760 + 0xb8))();
  }
  FUN_00a049c0(1);
  return 0;
}



/* ===================================================== */
/* FUN_00a05ac0 @ 00a05ac0 */
/* ===================================================== */

void FUN_00a05ac0(undefined2 *param_1,undefined2 param_2,undefined2 param_3,undefined2 param_4,
                 undefined2 param_5)

{
                    /* 0x5ac0  658   */
  *param_1 = param_2;
  param_1[1] = param_3;
  param_1[2] = param_4;
  param_1[3] = param_5;
  param_1[4] = 0;
  param_1[5] = 0;
  param_1[6] = 0x100;
  param_1[7] = 0xf0;
  *(undefined1 *)(param_1 + 8) = 0;
  *(undefined1 *)((int)param_1 + 0x11) = 0;
  return;
}



/* ===================================================== */
/* FUN_00a05b10 @ 00a05b10 */
/* ===================================================== */

void FUN_00a05b10(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &DAT_011be6d8;
                    /* 0x5b10  659   */
  for (iVar1 = 0x17; iVar1 != 0; iVar1 = iVar1 + -1) {
    *param_1 = *puVar2;
    puVar2 = puVar2 + 1;
    param_1 = param_1 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a05b30 @ 00a05b30 */
/* ===================================================== */

undefined4 FUN_00a05b30(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &DAT_011be6d8;
                    /* 0x5b30  660   */
  for (iVar1 = 0x17; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = *param_1;
    param_1 = param_1 + 1;
    puVar2 = puVar2 + 1;
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a05b50 @ 00a05b50 */
/* ===================================================== */

void FUN_00a05b50(undefined2 *param_1,undefined2 param_2,undefined2 param_3,undefined2 param_4,
                 undefined2 param_5)

{
                    /* 0x5b50  661   */
  *param_1 = param_2;
  param_1[2] = param_4;
  param_1[1] = param_3;
  param_1[4] = param_2;
  param_1[5] = param_3;
  param_1[3] = param_5;
  param_1[6] = 0;
  param_1[7] = 0;
  param_1[8] = 0;
  param_1[9] = 0;
  param_1[10] = 10;
  *(undefined1 *)(param_1 + 0xb) = 1;
  *(undefined1 *)((int)param_1 + 0x17) = 1;
  *(undefined1 *)(param_1 + 0xc) = 0;
  *(undefined1 *)((int)param_1 + 0x19) = 0;
  *(undefined1 *)(param_1 + 0xd) = 0;
  *(undefined1 *)((int)param_1 + 0x1b) = 0;
  return;
}



/* ===================================================== */
/* FUN_00a05bb0 @ 00a05bb0 */
/* ===================================================== */

undefined4 FUN_00a05bb0(int param_1)

{
                    /* 0x5bb0  677   */
  DAT_011be790 = param_1 + 4;
  return 0;
}



/* ===================================================== */
/* FUN_00a05bd0 @ 00a05bd0 */
/* ===================================================== */

void FUN_00a05bd0(uint *param_1)

{
  uint uVar1;
  int iVar2;
  
                    /* 0x5bd0  662   */
  if (DAT_011be790 == (uint *)0x0) {
    return;
  }
  if ((*DAT_011be790 & 8) == 0) {
    iVar2 = 1;
    *param_1 = 2;
    param_1[1] = 0;
    param_1[2] = 0;
  }
  else {
    *param_1 = *DAT_011be790 & 7;
    uVar1 = DAT_011be790[1];
    param_1[1] = (uint)(DAT_011be790 + 2);
    iVar2 = (uVar1 >> 2) + 1;
    param_1[2] = (uint)(DAT_011be790 + 4);
  }
  param_1[3] = (uint)(DAT_011be790 + iVar2 + 1);
  param_1[4] = (uint)(DAT_011be790 + iVar2 + 3);
  DAT_011be790 = (uint *)0x0;
  return;
}



/* ===================================================== */
/* FUN_00a05c60 @ 00a05c60 */
/* ===================================================== */

void FUN_00a05c60(int param_1,int param_2,int param_3,int param_4)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  
  iVar1 = 0;
  if (0 < param_4 << 6) {
    do {
      if (0 < param_3 >> 4) {
        iVar2 = iVar1 + (param_1 >> 4 & 0x3fU | param_2 << 6);
        iVar4 = param_3 >> 4;
        do {
          FUN_00a27770(iVar2,0x100);
          iVar2 = iVar2 + 1;
          iVar4 = iVar4 + -1;
        } while (iVar4 != 0);
      }
      iVar1 = iVar1 + 0x40;
    } while (iVar1 < param_4 << 6);
  }
  iVar1 = param_4 >> 8;
  if (iVar1 * 0x100 < param_4) {
    iVar1 = iVar1 + 1;
  }
  iVar2 = param_3 >> 6;
  if (iVar2 * 0x40 < param_3) {
    iVar2 = iVar2 + 1;
  }
  iVar4 = 0;
  if (0 < iVar1 << 4) {
    do {
      if (0 < iVar2) {
        iVar3 = iVar4 + (param_2 >> 4 & 0xfffffff0U) + (param_1 >> 6);
        iVar5 = iVar2;
        do {
          FUN_00a27700(iVar3);
          iVar3 = iVar3 + 1;
          iVar5 = iVar5 + -1;
        } while (iVar5 != 0);
      }
      iVar4 = iVar4 + 0x10;
    } while (iVar4 < iVar1 << 4);
  }
  return;
}



/* ===================================================== */
/* FUN_00a05d50 @ 00a05d50 */
/* ===================================================== */

undefined4 FUN_00a05d50(short *param_1,int param_2)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  undefined4 *puVar4;
  undefined4 *puVar5;
  undefined4 *puVar6;
  
                    /* 0x5d50  674   */
  if (0x1ff < param_1[1]) {
    return 0;
  }
  FUN_00a05c60((int)*param_1,(int)param_1[1],(int)param_1[2],(int)param_1[3]);
  iVar1 = 0;
  puVar4 = (undefined4 *)(EXT_00b6dbd0 + (param_1[1] * 0x400 + (int)*param_1) * 2);
  if (0 < param_1[3]) {
    do {
      uVar3 = (uint)param_1[2];
      puVar5 = (undefined4 *)(param_2 + iVar1 * uVar3 * 2);
      puVar6 = puVar4;
      for (uVar2 = (uVar3 & 0x7fffffff) >> 1; uVar2 != 0; uVar2 = uVar2 - 1) {
        *puVar6 = *puVar5;
        puVar5 = puVar5 + 1;
        puVar6 = puVar6 + 1;
      }
      iVar1 = iVar1 + 1;
      for (uVar2 = uVar3 * 2 & 3; uVar2 != 0; uVar2 = uVar2 - 1) {
        *(undefined1 *)puVar6 = *(undefined1 *)puVar5;
        puVar5 = (undefined4 *)((int)puVar5 + 1);
        puVar6 = (undefined4 *)((int)puVar6 + 1);
      }
      puVar4 = puVar4 + 0x200;
    } while (iVar1 < param_1[3]);
  }
  if (DAT_011be754 != (code *)0x0) {
    (*DAT_011be754)();
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a05df0 @ 00a05df0 */
/* ===================================================== */

undefined4 FUN_00a05df0(short *param_1,int param_2,int param_3)

{
  short *psVar1;
  short *psVar2;
  int iVar3;
  uint uVar4;
  uint uVar5;
  int iVar6;
  undefined4 *puVar7;
  undefined4 *puVar8;
  undefined4 *puVar9;
  
                    /* 0x5df0  676   */
  psVar2 = param_1;
  puVar7 = (undefined4 *)(EXT_00b6dbd0 + (param_1[1] * 0x400 + (int)*param_1) * 2);
  puVar8 = (undefined4 *)(EXT_00b6dbd0 + (param_3 * 0x400 + param_2) * 2);
  FUN_00a05c60(param_2,param_3,(int)param_1[2],(int)param_1[3]);
  iVar3 = 0;
  psVar1 = param_1 + 3;
  param_1 = (short *)puVar8;
  if (0 < *psVar1) {
    do {
      uVar4 = (uint)psVar2[2];
      puVar8 = puVar7;
      puVar9 = (undefined4 *)param_1;
      for (uVar5 = (uVar4 & 0x7fffffff) >> 1; uVar5 != 0; uVar5 = uVar5 - 1) {
        *puVar9 = *puVar8;
        puVar8 = puVar8 + 1;
        puVar9 = puVar9 + 1;
      }
      for (iVar6 = (uVar4 & 1) << 1; iVar6 != 0; iVar6 = iVar6 + -1) {
        *(undefined1 *)puVar9 = *(undefined1 *)puVar8;
        puVar8 = (undefined4 *)((int)puVar8 + 1);
        puVar9 = (undefined4 *)((int)puVar9 + 1);
      }
      param_1 = (short *)((int)param_1 + 0x800);
      iVar3 = iVar3 + 1;
      puVar7 = puVar7 + 0x200;
    } while (iVar3 < psVar2[3]);
  }
  if (DAT_011be754 != (code *)0x0) {
    (*DAT_011be754)();
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a05e90 @ 00a05e90 */
/* ===================================================== */

undefined4 FUN_00a05e90(short *param_1,char param_2,char param_3,uint param_4)

{
  int iVar1;
  int *piVar2;
  int iVar3;
  int *piVar4;
  
                    /* 0x5e90  665   */
  piVar4 = (int *)(EXT_00b6dbd0 + (param_1[1] * 0x400 + (int)*param_1) * 2);
  FUN_00a05c60((int)*param_1,(int)param_1[1],(int)param_1[2],(int)param_1[3]);
  iVar3 = 0;
  if (0 < param_1[3]) {
    do {
      iVar1 = 0;
      piVar2 = piVar4;
      if (0 < (short)(param_1[2] & 0xfffeU)) {
        do {
          *piVar2 = (((param_4 & 0xff) << (param_3 + 10U & 0x1f)) << (param_2 + 5U & 0x1f)) *
                    0x10001;
          iVar1 = iVar1 + 1;
          piVar2 = piVar2 + 1;
        } while (iVar1 < (int)param_1[2] >> 1);
      }
      piVar4 = piVar4 + 0x200;
      iVar3 = iVar3 + 1;
    } while (iVar3 < param_1[3]);
  }
  if (DAT_011be754 != (code *)0x0) {
    (*DAT_011be754)();
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a05f30 @ 00a05f30 */
/* ===================================================== */

void FUN_00a05f30(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
                    /* 0x5f30  666   */
  FUN_00a05e90(param_1,param_2,param_3,param_4);
  return;
}



/* ===================================================== */
/* FUN_00a05f50 @ 00a05f50 */
/* ===================================================== */

undefined4 FUN_00a05f50(short *param_1,int param_2)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  undefined4 *puVar4;
  undefined4 *puVar5;
  undefined4 *puVar6;
  
                    /* 0x5f50  684   */
  iVar1 = 0;
  puVar4 = (undefined4 *)(EXT_00b6dbd0 + (param_1[1] * 0x400 + (int)*param_1) * 2);
  if (0 < param_1[3]) {
    do {
      uVar3 = (uint)param_1[2];
      puVar5 = puVar4;
      puVar6 = (undefined4 *)(param_2 + iVar1 * uVar3 * 2);
      for (uVar2 = (uVar3 & 0x7fffffff) >> 1; uVar2 != 0; uVar2 = uVar2 - 1) {
        *puVar6 = *puVar5;
        puVar5 = puVar5 + 1;
        puVar6 = puVar6 + 1;
      }
      iVar1 = iVar1 + 1;
      for (uVar2 = uVar3 * 2 & 3; uVar2 != 0; uVar2 = uVar2 - 1) {
        *(undefined1 *)puVar6 = *(undefined1 *)puVar5;
        puVar5 = (undefined4 *)((int)puVar5 + 1);
        puVar6 = (undefined4 *)((int)puVar6 + 1);
      }
      puVar4 = puVar4 + 0x200;
    } while (iVar1 < param_1[3]);
  }
  if (DAT_011be754 != (code *)0x0) {
    (*DAT_011be754)();
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a05fc0 @ 00a05fc0 */
/* ===================================================== */

uint FUN_00a05fc0(undefined4 *param_1,int param_2,int param_3)

{
  uint uVar1;
  int iVar2;
  undefined4 *puVar3;
  
                    /* 0x5fc0  694   */
  uVar1 = param_2 >> 4 & 0x3f;
  puVar3 = (undefined4 *)(EXT_00b6dbd0 + (param_3 * 0x400 + param_2) * 2);
  for (iVar2 = 0x80; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar3 = *param_1;
    param_1 = param_1 + 1;
    puVar3 = puVar3 + 1;
  }
  iVar2 = 0;
  do {
    FUN_00a27770(iVar2 + (uVar1 | param_3 << 6),0x100);
    iVar2 = iVar2 + 1;
  } while (iVar2 < 0x10);
  return uVar1 | param_3 << 6 & 0xffffU;
}



/* ===================================================== */
/* FUN_00a06020 @ 00a06020 */
/* ===================================================== */

uint FUN_00a06020(undefined4 *param_1,int param_2,int param_3)

{
  uint uVar1;
  int iVar2;
  undefined4 *puVar3;
  
                    /* 0x6020  695   */
  uVar1 = param_2 >> 4 & 0x3f;
  puVar3 = (undefined4 *)(EXT_00b6dbd0 + (param_3 * 0x400 + param_2) * 2);
  for (iVar2 = 8; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar3 = *param_1;
    param_1 = param_1 + 1;
    puVar3 = puVar3 + 1;
  }
  FUN_00a27770(uVar1 | param_3 << 6,0x10);
  return uVar1 | param_3 << 6 & 0xffffU;
}



/* ===================================================== */
/* FUN_00a06070 @ 00a06070 */
/* ===================================================== */

uint FUN_00a06070(undefined4 param_1,uint param_2,uint param_3,int param_4,uint param_5,int param_6,
                 undefined2 param_7)

{
  uint uVar1;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  undefined2 local_2;
  
                    /* 0x6070  696   */
  local_8 = (undefined2)param_4;
  local_6 = (undefined2)param_5;
  local_4 = (undefined2)(param_6 >> (2U - (char)param_2 & 0x1f));
  local_2 = param_7;
  FUN_00a05d50(&local_8,param_1);
  FUN_00a02e20(0);
  uVar1 = ((param_2 & 3) << 2 | param_3 & 3) << 5 |
          (int)(param_5 & 0x100 | param_4 >> 2 & 0xf0U) >> 4;
  FUN_00a27700(uVar1);
  return uVar1;
}



/* ===================================================== */
/* FUN_00a06100 @ 00a06100 */
/* ===================================================== */

undefined4 FUN_00a06100(undefined4 param_1)

{
                    /* 0x6100  691   */
  DAT_011be754 = param_1;
  return 0;
}



/* ===================================================== */
/* FUN_00a06110 @ 00a06110 */
/* ===================================================== */

void FUN_00a06110(uint *param_1,int param_2)

{
  uint *puVar1;
  int iVar2;
  int iVar3;
  
                    /* 0x6110  687   */
  puVar1 = param_1;
  iVar2 = 0;
  iVar3 = param_2;
  if (0 < param_2) {
    do {
      iVar3 = iVar3 + -1;
      *puVar1 = ((uint)(puVar1 + 1) ^ *puVar1 & 0xffffff) & 0xffffff ^ *puVar1 & 0xffffff;
      puVar1 = puVar1 + 1;
      iVar2 = param_2;
    } while (iVar3 != 0);
  }
  param_1[iVar2 + -1] = 0xffffff;
  return;
}



/* ===================================================== */
/* FUN_00a06160 @ 00a06160 */
/* ===================================================== */

uint * FUN_00a06160(uint *param_1,int param_2)

{
  uint *puVar1;
  uint *puVar2;
  int iVar3;
  
                    /* 0x6160  688   */
  iVar3 = param_2 + -1;
  puVar1 = param_1;
  if (0 < iVar3) {
    do {
      puVar2 = puVar1 + 1;
      iVar3 = iVar3 + -1;
      *puVar2 = ((uint)puVar1 ^ *puVar2 & 0xffffff) & 0xffffff ^ *puVar2 & 0xffffff;
      puVar1 = puVar2;
    } while (iVar3 != 0);
  }
  *param_1 = 0xffffff;
  return param_1 + param_2 + -1;
}



/* ===================================================== */
/* FUN_00a061b0 @ 00a061b0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a061b0(uint *param_1)

{
  int iVar1;
  uint *puVar2;
  
                    /* 0x61b0  701   */
  if (DAT_00dedc44 == 0) {
    DAT_00a2c484 = 0xffffffff;
    DAT_00a2c488 = 0xffffffff;
    DAT_00a2c48c = 0xffffffff;
    iVar1 = -DAT_00dedbe4;
    DAT_00dedbe4 = iVar1 + 0x40000;
    DAT_00dedbd8 = iVar1 + 0x240000 + MainMem;
    _DAT_00dedbdc = DAT_00dedbd8;
    if (VSyncDelayed == 0) {
      DAT_00a2c480 = 8;
      DAT_011be75c = DAT_011be75c + 1;
      FUN_00a26490();
      puVar2 = param_1;
      do {
        if ((char)(*puVar2 >> 0x18) != '\0') {
          FUN_00a26b20(puVar2);
        }
        puVar2 = (uint *)(*puVar2 & 0xffffff);
      } while (puVar2 != (uint *)&DAT_00ffffff);
      FUN_00a027a0();
      FUN_00a26f20();
      if (DAT_011be6f0 == '\0') {
        (**(code **)(*DAT_011be760 + 0x60))(0,0);
      }
      else {
        (**(code **)(*DAT_011be760 + 0x5c))(0,0);
      }
      (**(code **)(*DAT_011be760 + 0xb0))();
      do {
        if ((char)(*param_1 >> 0x18) != '\0') {
          FUN_00a13400(param_1);
        }
        param_1 = (uint *)(*param_1 & 0xffffff);
      } while (param_1 != (uint *)&DAT_00ffffff);
      FUN_00a027e0();
      (**(code **)(*DAT_011be760 + 0xb4))();
      FUN_00a02820();
      if (DAT_011be754 != (code *)0x0) {
        (*DAT_011be754)();
      }
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a062d0 @ 00a062d0 */
/* ===================================================== */

void FUN_00a062d0(undefined4 param_1)

{
                    /* 0x62d0  702   */
  FUN_00a061b0(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a062e0 @ 00a062e0 */
/* ===================================================== */

void FUN_00a062e0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x62e0  703   */
  FUN_00a05b30(param_2);
  FUN_00a061b0(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a06300 @ 00a06300 */
/* ===================================================== */

void FUN_00a06300(undefined4 *param_1)

{
                    /* 0x6300  704   */
  DAT_00a2c480 = 8;
  if ((char)((uint)*param_1 >> 0x18) != '\0') {
    FUN_00a26490();
    FUN_00a26b20(param_1);
    FUN_00a26f20();
    (**(code **)(*DAT_011be760 + 0xb0))();
    FUN_00a13400(param_1);
    (**(code **)(*DAT_011be760 + 0xb4))();
  }
  if (DAT_011be754 != (code *)0x0) {
    (*DAT_011be754)();
  }
  return;
}



/* ===================================================== */
/* SetTextureFilterMode @ 00a063d0 */
/* ===================================================== */

void SetTextureFilterMode(int param_1)

{
  undefined4 uVar1;
  
                    /* 0x63d0  798  SetTextureFilterMode */
  if (param_1 != DAT_00a2c508) {
    DAT_00a2c508 = param_1;
    if ((param_1 == 0) || (uVar1 = 2, TexFilterPossible == 0)) {
      uVar1 = 1;
    }
    (**(code **)(*DAT_011be760 + 0x90))(uVar1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a06410 @ 00a06410 */
/* ===================================================== */

void FUN_00a06410(undefined4 param_1)

{
                    /* 0x6410  790   */
  DAT_011be798 = param_1;
  return;
}



/* ===================================================== */
/* FUN_00a06420 @ 00a06420 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_00a06420(int param_1,int param_2)

{
  float fVar1;
  float fVar2;
  float fVar3;
  int iVar4;
  undefined4 uVar5;
  float *pfVar6;
  int iVar7;
  undefined4 *puVar8;
  float *pfVar9;
  undefined4 uVar10;
  undefined4 uVar11;
  char *lpLibFileName;
  int iStack_18;
  
                    /* 0x6420  789   */
  DAT_00a2c49c = param_2;
  if (param_1 != 0) {
    if ((param_1 == -1) || (param_1 == -2)) {
      timeKillEvent(DAT_011be6d0);
      FUN_00a033c0();
      if ((DAT_011be738 != 0) && (param_1 != -2)) {
        (**(code **)(*DAT_011be760 + 4))();
        return 1;
      }
      (**(code **)(*DAT_011be760 + 0x34))();
      if (param_1 == -2) {
        DestroyWindow(DAT_011be79c);
        DAT_011be79c = (HWND)0x0;
        if (DAT_011be760 != (int *)0x0) {
          (**(code **)*DAT_011be760)();
        }
        DAT_011be760 = (int *)0x0;
        FreeLibrary(DAT_011be6cc);
        DAT_011be6cc = (HMODULE)0x0;
      }
    }
    return 1;
  }
  if (((DAT_011be738 != 0) && (DAT_011be760 != (int *)0x0)) && (DAT_011be79c != (HWND)0x0)) {
    DAT_011be6d0 = timeSetEvent(0x11,0,&LAB_00a06360,0,0);
    if (DAT_011be798 != (code *)0x0) {
      (*DAT_011be798)();
    }
    FUN_00a03110();
    (**(code **)(*DAT_011be760 + 4))();
    return 1;
  }
  MMXInstalled = FUN_00a06930();
  DAT_00dedbd8 = MainMem + 0x200000;
  _DAT_00dedbdc = DAT_00dedbd8;
  _DAT_00dedbe0 = DAT_00dedbd8;
  DAT_011be6d0 = timeSetEvent(0x11,0,&LAB_00a06360,0,0);
  if (DAT_011be760 == (int *)0x0) {
    if (DAT_011be738 == 0) {
      lpLibFileName = s_3dhelp_dll_00a2c54c;
    }
    else {
      lpLibFileName = s_gldhelp_dll_00a2c540;
    }
    DAT_011be6cc = LoadLibraryA(lpLibFileName);
    _DAT_011be7a0 = GetProcAddress(DAT_011be6cc,s_Create3DDevice_00a2c530);
    DAT_011be760 = (int *)(*_DAT_011be7a0)();
    (**(code **)(*DAT_011be760 + 0x18))();
    DAT_011be794 = CreateEventA((LPSECURITY_ATTRIBUTES)0x0,0,0,(LPCSTR)0x0);
  }
  (**(code **)(*DAT_011be760 + 0xc))();
  uVar11 = DAT_011be73c;
  (**(code **)(*DAT_011be760 + 0x2c))();
  uVar10 = *(undefined4 *)(&DAT_00a2c4c0 + param_2 * 4);
  iVar4 = (**(code **)(*DAT_011be760 + 0x30))(0,*(undefined4 *)(&DAT_00a2c4a8 + param_2 * 4));
  if (iVar4 != 0) {
    iVar4 = *DAT_011be760;
    uVar5 = ScaleMatrix(&stack0xffffffa8,0x3f800000,0xbf800000,0x3f800000);
    (**(code **)(iVar4 + 0x70))(uVar5);
    iVar4 = *DAT_011be760;
    uVar5 = ScaleMatrix(&stack0xffffffa4,0x3f000000,0x3f000000,0x3f000000);
    (**(code **)(iVar4 + 0x68))(uVar5);
    FUN_00a1dbc0(1000);
    if (DAT_011be798 != (code *)0x0) {
      (*DAT_011be798)(DAT_011be79c);
    }
    pfVar9 = (float *)&DAT_011c4cd0;
    iStack_18 = 0;
    do {
      iVar4 = iStack_18;
      iVar7 = 8;
      *pfVar9 = _DAT_00a2a074 - (float)iStack_18 * _DAT_00a2a070;
      pfVar6 = pfVar9 + 0x909;
      do {
        fVar1 = (float)iStack_18;
        iStack_18 = iStack_18 + -0x20;
        iVar7 = iVar7 + -1;
        fVar2 = fVar1 * _DAT_00a2a088;
        fVar3 = _DAT_00a2a084 - fVar1 * _DAT_00a2a080;
        pfVar6[-0x808] = _DAT_00a2a07c - fVar1 * _DAT_00a2a078;
        fVar2 = _DAT_00a2a08c - fVar2;
        *pfVar6 = fVar3;
        pfVar6[0x808] = fVar2;
        pfVar6 = pfVar6 + 0x101;
      } while (iVar7 != 0);
      iStack_18 = iVar4 + 1;
      pfVar9 = pfVar9 + 1;
    } while (iStack_18 < 0x101);
    _DAT_011c50d0 = 0x3f7fe666;
    puVar8 = &DAT_011c71f4;
    iVar4 = 0;
    do {
      if (iVar4 < 0x1694) {
        puVar8[-0x7c8] = 0x3f7fcccd;
      }
      if (iVar4 < 0x1f9c) {
        *puVar8 = 0x3f7f999a;
      }
      puVar8[0x7e8] = 0x3f7f3333;
      iVar4 = iVar4 + 0x484;
      puVar8 = puVar8 + 0x121;
    } while (iVar4 < 0x2420);
    pfVar9 = (float *)&DAT_011be868;
    iStack_18 = 0;
    do {
      iVar4 = iStack_18;
      iVar7 = 8;
      *pfVar9 = _DAT_00a2a094 - (float)iStack_18 * _DAT_00a2a070;
      pfVar6 = pfVar9 + 0x909;
      do {
        fVar1 = (float)iStack_18;
        iStack_18 = iStack_18 + -0x20;
        iVar7 = iVar7 + -1;
        fVar2 = fVar1 * _DAT_00a2a088;
        fVar3 = _DAT_00a2a09c - fVar1 * _DAT_00a2a080;
        pfVar6[-0x808] = _DAT_00a2a098 - fVar1 * _DAT_00a2a078;
        fVar2 = _DAT_00a2a0a0 - fVar2;
        *pfVar6 = fVar3;
        pfVar6[0x808] = fVar2;
        pfVar6 = pfVar6 + 0x101;
      } while (iVar7 != 0);
      iStack_18 = iVar4 + 1;
      pfVar9 = pfVar9 + 1;
    } while (iStack_18 < 0x101);
    FUN_00a03110(DAT_011be79c);
    if (DAT_011be73c != 0) {
      TexFilterPossible = 0;
      SetTextureFilterMode(0);
    }
    FUN_00a1df00();
    DAT_011be6bc = 0x140;
    _DAT_011be6be = 0xf0;
    DAT_011be6c8 = 0;
    FUN_00a020a0(0,0,0x140,0xf0,1,0x50);
    iVar4 = DAT_011be738;
    iVar7 = 0;
    do {
      if (iVar4 == 0) {
        *(undefined4 *)((int)&DAT_011cb144 + iVar7) = 0x38d1b717;
        *(undefined4 *)((int)&DAT_011be7f4 + iVar7) = 0x38d1b717;
        *(undefined4 *)((int)&DAT_011cb140 + iVar7) = 0x38d1b717;
        *(undefined4 *)((int)&DAT_011be7f0 + iVar7) = 0x38d1b717;
      }
      else {
        *(undefined4 *)((int)&DAT_011cb144 + iVar7) = 0x3f800000;
        *(undefined4 *)((int)&DAT_011be7f4 + iVar7) = 0x3f800000;
        *(undefined4 *)((int)&DAT_011cb140 + iVar7) = 0x3f800000;
        *(undefined4 *)((int)&DAT_011be7f0 + iVar7) = 0x3f800000;
      }
      *(undefined4 *)((int)&DAT_011be7f8 + iVar7) = 0x7f7f7f;
      iVar7 = iVar7 + 0x20;
    } while (iVar7 < 0x80);
    QueryPerformanceFrequency((LARGE_INTEGER *)&stack0xffffff98);
    DAT_011be684 = __alldiv(uVar10 + 0x1e,uVar11 + (uint)(0xffffffe1 < (uint)uVar10),0x3c,0);
    return 1;
  }
  MessageBoxA(DAT_011be79c,s_3D_Graphics_initialisation_error_00a2c50c,(LPCSTR)0x0,0);
                    /* WARNING: Subroutine does not return */
  ExitProcess(3);
}



/* ===================================================== */
/* FUN_00a06930 @ 00a06930 */
/* ===================================================== */

undefined4 FUN_00a06930(void)

{
  undefined4 uVar1;
  undefined4 *unaff_FS_OFFSET;
  undefined1 local_14 [16];
  
  uVar1 = *unaff_FS_OFFSET;
  *unaff_FS_OFFSET = local_14;
  *unaff_FS_OFFSET = uVar1;
  return 1;
}



/* ===================================================== */
/* FUN_00a069b0 @ 00a069b0 */
/* ===================================================== */

void FUN_00a069b0(undefined4 param_1)

{
                    /* 0x69b0  680   */
  FUN_00a06420(param_1,2);
  return;
}



/* ===================================================== */
/* ChangeResolution @ 00a069c0 */
/* ===================================================== */

int ChangeResolution(int param_1)

{
  int iVar1;
  int iVar2;
  
                    /* 0x69c0  749  ChangeResolution */
  iVar1 = (**(code **)(*DAT_011be760 + 0xe8))
                    (*(undefined4 *)(&DAT_00a2c4a8 + param_1 * 4),
                     *(undefined4 *)(&DAT_00a2c4c0 + param_1 * 4));
  if (iVar1 != 0) {
    iVar2 = (**(code **)(*DAT_011be760 + 0x4c))();
    DAT_00a2c49c = param_1;
    if ((*(byte *)(iVar2 + 0x8c) & 8) != 0) {
      (**(code **)(*DAT_011be760 + 0x94))(4);
      return iVar1;
    }
    (**(code **)(*DAT_011be760 + 0x94))(2);
    DAT_00b2d188 = 1;
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_00a06a40 @ 00a06a40 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

int FUN_00a06a40(int *param_1,int param_2)

{
  int iVar1;
  
                    /* 0x6a40  678   */
  if (param_2 < 0) {
    _DAT_011be7b0 = 0;
    _DAT_011be7b4 = 1;
    iVar1 = 0;
  }
  else {
    if (*param_1 != 0x41) {
      return -1;
    }
    _DAT_011be7b0 = param_2;
    _DAT_011be7b4 = param_1[2];
    iVar1 = param_2 * 7 + 3;
    FUN_00a1cff0(param_1 + 1);
  }
  DAT_011be7b8 = param_1[iVar1];
  _DAT_011be7bc = param_1[iVar1 + 1];
  DAT_011be7c0 = param_1[iVar1 + 2];
  _DAT_011be7c4 = param_1[iVar1 + 3];
  _DAT_011be7c8 = param_1[iVar1 + 4];
  DAT_011be7cc = param_1[iVar1 + 5];
  _DAT_011be7d0 = param_1[iVar1 + 6];
  DAT_011be7d4 = 0;
  DAT_011be7d8 = param_1[iVar1 + 4];
  return param_1[iVar1 + 5];
}



/* ===================================================== */
/* FUN_00a06af0 @ 00a06af0 */
/* ===================================================== */

void FUN_00a06af0(int param_1,int *param_2,int param_3,int param_4)

{
  short sVar1;
  ushort uVar2;
  int iVar3;
  
  iVar3 = *param_2;
  *param_2 = iVar3 + 0x10;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar3 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar3 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar3 + 6);
  sVar1 = *(short *)(iVar3 + 8);
  *(undefined4 *)(param_1 + 0x40) = *(undefined4 *)(param_4 + sVar1 * 8);
  *(undefined4 *)(param_1 + 0x44) = *(undefined4 *)(param_4 + 4 + sVar1 * 8);
  uVar2 = *(ushort *)(iVar3 + 10);
  *(ushort *)(param_1 + 0x68) = uVar2;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar2 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar2 * 8);
  uVar2 = *(ushort *)(iVar3 + 0xc);
  *(ushort *)(param_1 + 0x6a) = uVar2;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar2 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar2 * 8);
  uVar2 = *(ushort *)(iVar3 + 0xe);
  *(ushort *)(param_1 + 0x6c) = uVar2;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar2 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar2 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a06b90 @ 00a06b90 */
/* ===================================================== */

void FUN_00a06b90(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x10;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 6);
  uVar1 = *(ushort *)(iVar2 + 8);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 10);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0xc);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a06c10 @ 00a06c10 */
/* ===================================================== */

void FUN_00a06c10(int param_1,int *param_2,int param_3,undefined4 param_4)

{
  ushort uVar1;
  
  uVar1 = *(ushort *)(*param_2 + 0x10);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  FUN_00a06af0(param_1,param_2,param_3,param_4);
  *param_2 = *param_2 + 4;
  return;
}



/* ===================================================== */
/* FUN_00a06c60 @ 00a06c60 */
/* ===================================================== */

void FUN_00a06c60(int param_1,int *param_2,int param_3,undefined4 param_4)

{
  ushort uVar1;
  
  uVar1 = *(ushort *)(*param_2 + 0xe);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  FUN_00a06b90(param_1,param_2,param_3,param_4);
  return;
}



/* ===================================================== */
/* FUN_00a06ca0 @ 00a06ca0 */
/* ===================================================== */

void FUN_00a06ca0(int param_1,int *param_2,int param_3,int param_4)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x18;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  uVar1 = *(ushort *)(iVar2 + 0x10);
  *(ushort *)(param_1 + 0x70) = uVar1;
  *(undefined4 *)(param_1 + 0x40) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x44) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x12);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a06d60 @ 00a06d60 */
/* ===================================================== */

void FUN_00a06d60(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x1c;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 0x11);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 0x12);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x18);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a06e20 @ 00a06e20 */
/* ===================================================== */

void FUN_00a06e20(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x20;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 0x1e) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 0x1f) = *(undefined1 *)(iVar2 + 0x11);
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 0x14);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 0x15);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 0x16);
  uVar1 = *(ushort *)(iVar2 + 0x18);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1a);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1c);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1e);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a06f00 @ 00a06f00 */
/* ===================================================== */

void FUN_00a06f00(int param_1,int *param_2,int param_3,int param_4)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x20;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 0x1e) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 0x1f) = *(undefined1 *)(iVar2 + 0x11);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x70) = uVar1;
  *(undefined4 *)(param_1 + 0x40) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x44) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x18);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1a);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1c);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a06ff0 @ 00a06ff0 */
/* ===================================================== */

void FUN_00a06ff0(int param_1,int *param_2,int param_3,int param_4)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x14;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 6);
  uVar1 = *(ushort *)(iVar2 + 8);
  *(ushort *)(param_1 + 0x70) = uVar1;
  *(undefined4 *)(param_1 + 0x40) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x44) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 10);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0xc);
  *(ushort *)(param_1 + 0x72) = uVar1;
  *(undefined4 *)(param_1 + 0x48) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x4c) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0xe);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x10);
  *(ushort *)(param_1 + 0x74) = uVar1;
  *(undefined4 *)(param_1 + 0x50) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x54) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x12);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a070d0 @ 00a070d0 */
/* ===================================================== */

void FUN_00a070d0(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x18;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 6);
  *(undefined1 *)(param_1 + 8) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 9) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 10) = *(undefined1 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0xc) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0xd) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 0xe) = *(undefined1 *)(iVar2 + 0xe);
  uVar1 = *(ushort *)(iVar2 + 0x10);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x12);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a07180 @ 00a07180 */
/* ===================================================== */

void FUN_00a07180(int param_1,int *param_2,int param_3,int param_4)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x1c;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  uVar1 = *(ushort *)(iVar2 + 0x10);
  *(ushort *)(param_1 + 0x70) = uVar1;
  *(undefined4 *)(param_1 + 0x40) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x44) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x12);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x72) = uVar1;
  *(undefined4 *)(param_1 + 0x48) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x4c) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x18);
  *(ushort *)(param_1 + 0x74) = uVar1;
  *(undefined4 *)(param_1 + 0x50) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x54) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1a);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a07280 @ 00a07280 */
/* ===================================================== */

void FUN_00a07280(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x24;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 0x11);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 0x12);
  *(undefined1 *)(param_1 + 8) = *(undefined1 *)(iVar2 + 0x14);
  *(undefined1 *)(param_1 + 9) = *(undefined1 *)(iVar2 + 0x15);
  *(undefined1 *)(param_1 + 10) = *(undefined1 *)(iVar2 + 0x16);
  *(undefined1 *)(param_1 + 0xc) = *(undefined1 *)(iVar2 + 0x18);
  *(undefined1 *)(param_1 + 0xd) = *(undefined1 *)(iVar2 + 0x19);
  *(undefined1 *)(param_1 + 0xe) = *(undefined1 *)(iVar2 + 0x1a);
  uVar1 = *(ushort *)(iVar2 + 0x1c);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1e);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x20);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a07360 @ 00a07360 */
/* ===================================================== */

void FUN_00a07360(int param_1,int *param_2,int param_3,int param_4)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x76) = uVar1;
  *(undefined4 *)(param_1 + 0x58) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x5c) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  FUN_00a06ff0(param_1,param_2,param_3,param_4);
  *param_2 = *param_2 + 4;
  return;
}



/* ===================================================== */
/* FUN_00a073d0 @ 00a073d0 */
/* ===================================================== */

void FUN_00a073d0(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x1c;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 6);
  *(undefined1 *)(param_1 + 8) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 9) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 10) = *(undefined1 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0xc) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0xd) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 0xe) = *(undefined1 *)(iVar2 + 0xe);
  *(undefined1 *)(param_1 + 0x10) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 0x11) = *(undefined1 *)(iVar2 + 0x11);
  *(undefined1 *)(param_1 + 0x12) = *(undefined1 *)(iVar2 + 0x12);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x18);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1a);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a074a0 @ 00a074a0 */
/* ===================================================== */

void FUN_00a074a0(int param_1,int *param_2,int param_3,int param_4)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x24;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 0x1e) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 0x1f) = *(undefined1 *)(iVar2 + 0x11);
  uVar1 = *(ushort *)(iVar2 + 0x14);
  *(ushort *)(param_1 + 0x70) = uVar1;
  *(undefined4 *)(param_1 + 0x40) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x44) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x16);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x18);
  *(ushort *)(param_1 + 0x72) = uVar1;
  *(undefined4 *)(param_1 + 0x48) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x4c) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1a);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1c);
  *(ushort *)(param_1 + 0x74) = uVar1;
  *(undefined4 *)(param_1 + 0x50) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x54) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x1e);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x20);
  *(ushort *)(param_1 + 0x76) = uVar1;
  *(undefined4 *)(param_1 + 0x58) = *(undefined4 *)(param_4 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x5c) = *(undefined4 *)(param_4 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x22);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a075e0 @ 00a075e0 */
/* ===================================================== */

void FUN_00a075e0(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x2c;
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 6);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 0x1a) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x1b) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 0x1c) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0x1d) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 0x10);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 0x11);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 0x12);
  *(undefined1 *)(param_1 + 8) = *(undefined1 *)(iVar2 + 0x14);
  *(undefined1 *)(param_1 + 9) = *(undefined1 *)(iVar2 + 0x15);
  *(undefined1 *)(param_1 + 10) = *(undefined1 *)(iVar2 + 0x16);
  *(undefined1 *)(param_1 + 0xc) = *(undefined1 *)(iVar2 + 0x18);
  *(undefined1 *)(param_1 + 0xd) = *(undefined1 *)(iVar2 + 0x19);
  *(undefined1 *)(param_1 + 0xe) = *(undefined1 *)(iVar2 + 0x1a);
  *(undefined1 *)(param_1 + 0x10) = *(undefined1 *)(iVar2 + 0x1c);
  *(undefined1 *)(param_1 + 0x11) = *(undefined1 *)(iVar2 + 0x1d);
  *(undefined1 *)(param_1 + 0x12) = *(undefined1 *)(iVar2 + 0x1e);
  uVar1 = *(ushort *)(iVar2 + 0x20);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x22);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x24);
  *(ushort *)(param_1 + 0x6c) = uVar1;
  *(undefined4 *)(param_1 + 0x30) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x34) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x24);
  *(ushort *)(param_1 + 0x6e) = uVar1;
  *(undefined4 *)(param_1 + 0x38) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x3c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a076f0 @ 00a076f0 */
/* ===================================================== */

void FUN_00a076f0(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0xc;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 6);
  uVar1 = *(ushort *)(iVar2 + 8);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 10);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a07760 @ 00a07760 */
/* ===================================================== */

void FUN_00a07760(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x14;
  *(undefined1 *)(param_1 + 4) = *(undefined1 *)(iVar2 + 4);
  *(undefined1 *)(param_1 + 5) = *(undefined1 *)(iVar2 + 5);
  *(undefined1 *)(param_1 + 6) = *(undefined1 *)(iVar2 + 6);
  *(undefined1 *)(param_1 + 8) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 9) = *(undefined1 *)(iVar2 + 9);
  *(undefined1 *)(param_1 + 10) = *(undefined1 *)(iVar2 + 10);
  *(undefined1 *)(param_1 + 0xc) = *(undefined1 *)(iVar2 + 0xc);
  *(undefined1 *)(param_1 + 0xd) = *(undefined1 *)(iVar2 + 0xd);
  *(undefined1 *)(param_1 + 0xe) = *(undefined1 *)(iVar2 + 0xe);
  uVar1 = *(ushort *)(iVar2 + 0x10);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  uVar1 = *(ushort *)(iVar2 + 0x12);
  *(ushort *)(param_1 + 0x6a) = uVar1;
  *(undefined4 *)(param_1 + 0x28) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x2c) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  return;
}



/* ===================================================== */
/* FUN_00a077f0 @ 00a077f0 */
/* ===================================================== */

void FUN_00a077f0(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0x10;
  uVar1 = *(ushort *)(iVar2 + 4);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 6);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 9);
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 10);
  *(undefined2 *)(param_1 + 0x70) = *(undefined2 *)(iVar2 + 0xc);
  *(undefined2 *)(param_1 + 0x72) = *(undefined2 *)(iVar2 + 0xe);
  return;
}



/* ===================================================== */
/* FUN_00a07860 @ 00a07860 */
/* ===================================================== */

void FUN_00a07860(int param_1,int *param_2,int param_3)

{
  ushort uVar1;
  int iVar2;
  
  iVar2 = *param_2;
  *param_2 = iVar2 + 0xc;
  uVar1 = *(ushort *)(iVar2 + 4);
  *(ushort *)(param_1 + 0x68) = uVar1;
  *(undefined4 *)(param_1 + 0x20) = *(undefined4 *)(param_3 + (uint)uVar1 * 8);
  *(undefined4 *)(param_1 + 0x24) = *(undefined4 *)(param_3 + 4 + (uint)uVar1 * 8);
  *(undefined2 *)(param_1 + 0x14) = *(undefined2 *)(iVar2 + 6);
  *(undefined1 *)(param_1 + 0x18) = *(undefined1 *)(iVar2 + 8);
  *(undefined1 *)(param_1 + 0x19) = *(undefined1 *)(iVar2 + 9);
  *(undefined2 *)(param_1 + 0x16) = *(undefined2 *)(iVar2 + 10);
  return;
}



/* ===================================================== */
/* FUN_00a078c0 @ 00a078c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a078c0(int param_1,int param_2,int param_3)

{
  short sVar1;
  float fVar2;
  float *pfVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  
  if (((*(byte *)(param_1 + 4) == DAT_011be764) && (*(byte *)(param_1 + 5) == DAT_011be768)) &&
     (*(byte *)(param_1 + 6) == DAT_011be76c)) {
    if (DAT_00a2c488 != -1) {
      if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
        DAT_00a2c578 = -5;
        *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = 0;
        *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
      }
      DAT_00a2c488 = -1;
    }
  }
  else {
    DAT_011be76c = (ushort)*(byte *)(param_1 + 6);
    iVar5 = (int)(short)DAT_011be76c;
    DAT_011be768 = (ushort)*(byte *)(param_1 + 5);
    DAT_011be764 = (ushort)*(byte *)(param_1 + 4);
    iVar6 = (int)(short)DAT_011be768;
    iVar4 = (int)(short)DAT_011be764;
    if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != iVar4)) {
      DAT_00a2c578 = -3;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffd;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = iVar4;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
         *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + iVar5;
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    FUN_00a07ca0(0xfffffffe,1,0,0);
  }
  pfVar3 = DAT_011be7a8;
  DAT_00a2c480 = 0;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar3[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar3[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar3[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar3[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar3[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar3[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar3[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar3[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  fVar2 = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 8) * 8) * _DAT_00a2a0b4;
  pfVar3[0x13] = fVar2;
  pfVar3[0xb] = fVar2;
  pfVar3[3] = fVar2;
  fVar2 = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 8) * 8) * _DAT_00a2a0b4;
  pfVar3[0x14] = fVar2;
  pfVar3[0xc] = fVar2;
  pfVar3[4] = fVar2;
  sVar1 = *(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar3[0x17] = 0.0;
  pfVar3[0x16] = 0.0;
  pfVar3[0xf] = 0.0;
  pfVar3[0xe] = 0.0;
  pfVar3[7] = 0.0;
  fVar2 = (float)(int)sVar1 * _DAT_00a2a0b4;
  pfVar3[6] = 0.0;
  pfVar3[0x15] = fVar2;
  pfVar3[0xd] = fVar2;
  pfVar3[5] = fVar2;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 1)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a07ca0 @ 00a07ca0 */
/* ===================================================== */

void FUN_00a07ca0(int param_1,int param_2,undefined4 param_3,int param_4)

{
  if (((DAT_00a2c578 != param_1) || (DAT_00a2c57c != param_2)) || ((-1 < param_1 && (param_4 == 4)))
     ) {
    DAT_00a2c578 = param_1;
    *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = param_1;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = param_2;
    *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = param_2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = param_3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + param_4;
  if (-1 < param_1) {
    DAT_011be7a8 = DAT_011be7a8 + param_4 * 0x20;
  }
  return;
}



/* ===================================================== */
/* FUN_00a07d80 @ 00a07d80 */
/* ===================================================== */

void FUN_00a07d80(int param_1,int param_2)

{
  float fVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  float *pfVar5;
  
  if (DAT_00a2c488 != -2) {
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    DAT_00a2c488 = -2;
  }
  if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar5 = DAT_011be7a8;
  DAT_00a2c480 = 4;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar5[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar5[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar5[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar5[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar5[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar5[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar5[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar5[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  bVar2 = *(byte *)(param_1 + 4);
  bVar3 = *(byte *)(param_1 + 5);
  bVar4 = *(byte *)(param_1 + 6);
  pfVar5[0x17] = 0.0;
  pfVar5[0x16] = 0.0;
  pfVar5[0xf] = 0.0;
  fVar1 = (float)(((uint)bVar2 * 0x100 + (uint)bVar3) * 0x100 + (uint)bVar4);
  pfVar5[0xe] = 0.0;
  pfVar5[0x14] = fVar1;
  pfVar5[0xc] = fVar1;
  pfVar5[4] = fVar1;
  pfVar5[7] = 0.0;
  pfVar5[6] = 0.0;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 2)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar5;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a08080 @ 00a08080 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a08080(int param_1,int param_2,int param_3)

{
  int *piVar1;
  short sVar2;
  float fVar3;
  float *pfVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  
  if (((*(byte *)(param_1 + 4) == DAT_011be764) && (*(byte *)(param_1 + 5) == DAT_011be768)) &&
     (*(byte *)(param_1 + 6) == DAT_011be76c)) {
    if (DAT_00a2c488 == -1) goto LAB_00a08226;
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
  }
  else {
    DAT_011be76c = (ushort)*(byte *)(param_1 + 6);
    iVar6 = (int)(short)DAT_011be76c;
    DAT_011be768 = (ushort)*(byte *)(param_1 + 5);
    DAT_011be764 = (ushort)*(byte *)(param_1 + 4);
    iVar7 = (int)(short)DAT_011be768;
    iVar5 = (int)(short)DAT_011be764;
    if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != iVar5)) {
      DAT_00a2c578 = -3;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffd;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = iVar5;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar7;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + iVar6;
  }
  DAT_00a2c488 = -1;
LAB_00a08226:
  if ((((byte)DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar4 = DAT_011be7a8;
  DAT_00a2c480 = 0;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar4[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar4[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar4[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar4[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar4[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar4[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar4[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar4[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar4[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar4[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar4[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  fVar3 = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 8) * 8) * _DAT_00a2a0b4;
  pfVar4[0x1b] = fVar3;
  pfVar4[0x13] = fVar3;
  pfVar4[0xb] = fVar3;
  pfVar4[3] = fVar3;
  fVar3 = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 8) * 8) * _DAT_00a2a0b4;
  pfVar4[0x1c] = fVar3;
  pfVar4[0x14] = fVar3;
  pfVar4[0xc] = fVar3;
  pfVar4[4] = fVar3;
  sVar2 = *(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar4[0x1f] = 0.0;
  pfVar4[0x1e] = 0.0;
  pfVar4[0x17] = 0.0;
  pfVar4[0x16] = 0.0;
  pfVar4[0xf] = 0.0;
  fVar3 = (float)(int)sVar2 * _DAT_00a2a0b4;
  pfVar4[0xe] = 0.0;
  pfVar4[7] = 0.0;
  pfVar4[6] = 0.0;
  pfVar4[0x1d] = fVar3;
  pfVar4[0x15] = fVar3;
  pfVar4[0xd] = fVar3;
  pfVar4[5] = fVar3;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar4;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a08510 @ 00a08510 */
/* ===================================================== */

void FUN_00a08510(int param_1,int param_2)

{
  float fVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  float *pfVar5;
  
  if (DAT_00a2c488 != -2) {
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    DAT_00a2c488 = -2;
  }
  if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar5 = DAT_011be7a8;
  DAT_00a2c480 = 4;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar5[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar5[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar5[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar5[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar5[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar5[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar5[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar5[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar5[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar5[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar5[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  bVar2 = *(byte *)(param_1 + 4);
  bVar3 = *(byte *)(param_1 + 5);
  bVar4 = *(byte *)(param_1 + 6);
  pfVar5[0x1f] = 0.0;
  pfVar5[0x1e] = 0.0;
  pfVar5[0x17] = 0.0;
  fVar1 = (float)(((uint)bVar2 * 0x100 + (uint)bVar3) * 0x100 + (uint)bVar4);
  pfVar5[0x16] = 0.0;
  pfVar5[0x1c] = fVar1;
  pfVar5[0x14] = fVar1;
  pfVar5[0xc] = fVar1;
  pfVar5[4] = fVar1;
  pfVar5[0xf] = 0.0;
  pfVar5[0xe] = 0.0;
  pfVar5[7] = 0.0;
  pfVar5[6] = 0.0;
  DAT_00a2c578 = 4;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 2;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar5;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a08850 @ 00a08850 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a08850(int param_1,int param_2,int param_3)

{
  int *piVar1;
  float fVar2;
  float *pfVar3;
  uint uVar4;
  uint uVar5;
  
  uVar4 = (uint)*(ushort *)(param_1 + 10);
  uVar5 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar4 != DAT_00a2c488) || (uVar5 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar5;
    DAT_00a2c488 = uVar4;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar4;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar5;
  }
  if ((((byte)DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar3 = DAT_011be7a8;
  DAT_00a2c480 = 2;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar3[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar3[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar3[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar3[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar3[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar3[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar3[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar3[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  fVar2 = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x10) * 8) * _DAT_00a2a0b4;
  pfVar3[0x13] = fVar2;
  pfVar3[0xb] = fVar2;
  pfVar3[3] = fVar2;
  fVar2 = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
          _DAT_00a2a0b4;
  pfVar3[0x14] = fVar2;
  pfVar3[0xc] = fVar2;
  pfVar3[4] = fVar2;
  fVar2 = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
          _DAT_00a2a0b4;
  pfVar3[0x15] = fVar2;
  pfVar3[0xd] = fVar2;
  pfVar3[5] = fVar2;
  pfVar3[6] = 0.0;
  pfVar3[7] = 0.0;
  pfVar3[0xe] = 0.0;
  pfVar3[0xf] = 0.0;
  pfVar3[0x16] = 0.0;
  pfVar3[0x17] = 0.0;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 1)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a08c80 @ 00a08c80 */
/* ===================================================== */

void FUN_00a08c80(int param_1,int param_2)

{
  int *piVar1;
  float *pfVar2;
  uint uVar3;
  float fVar4;
  uint uVar5;
  
  uVar3 = (uint)*(ushort *)(param_1 + 10);
  uVar5 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar3 != DAT_00a2c488) || (uVar5 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar5;
    DAT_00a2c488 = uVar3;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar5;
  }
  if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 6;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  fVar4 = (float)((uint)*(byte *)(param_1 + 0x12) +
                 ((uint)*(byte *)(param_1 + 0x10) * 0x100 + (uint)*(byte *)(param_1 + 0x11)) * 0x100
                 );
  pfVar2[0x14] = fVar4;
  pfVar2[0xc] = fVar4;
  pfVar2[4] = fVar4;
  pfVar2[6] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0x17] = 0.0;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 2)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a09070 @ 00a09070 */
/* ===================================================== */

void FUN_00a09070(int param_1,int param_2)

{
  int *piVar1;
  float *pfVar2;
  uint uVar3;
  float fVar4;
  uint uVar5;
  
  uVar3 = (uint)*(ushort *)(param_1 + 10);
  uVar5 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar3 != DAT_00a2c488) || (uVar5 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar5;
    DAT_00a2c488 = uVar3;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar5;
  }
  if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 6;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar2[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar2[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  fVar4 = (float)((uint)*(byte *)(param_1 + 0x16) +
                 ((uint)*(byte *)(param_1 + 0x14) * 0x100 + (uint)*(byte *)(param_1 + 0x15)) * 0x100
                 );
  pfVar2[0x1c] = fVar4;
  pfVar2[0x14] = fVar4;
  pfVar2[0xc] = fVar4;
  pfVar2[4] = fVar4;
  pfVar2[6] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0x17] = 0.0;
  pfVar2[0x1e] = 0.0;
  pfVar2[0x1f] = 0.0;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 2;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a094d0 @ 00a094d0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a094d0(int param_1,int param_2,int param_3)

{
  int *piVar1;
  float fVar2;
  float *pfVar3;
  uint uVar4;
  uint uVar5;
  
  uVar4 = (uint)*(ushort *)(param_1 + 10);
  uVar5 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar4 != DAT_00a2c488) || (uVar5 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar5;
    DAT_00a2c488 = uVar4;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar4;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar5;
  }
  if ((((byte)DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar3 = DAT_011be7a8;
  DAT_00a2c480 = 2;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar3[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar3[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar3[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar3[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar3[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar3[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar3[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar3[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar3[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar3[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar3[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  fVar2 = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x14) * 8) * _DAT_00a2a0b4;
  pfVar3[0x1b] = fVar2;
  pfVar3[0x13] = fVar2;
  pfVar3[0xb] = fVar2;
  pfVar3[3] = fVar2;
  fVar2 = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
          _DAT_00a2a0b4;
  pfVar3[0x1c] = fVar2;
  pfVar3[0x14] = fVar2;
  pfVar3[0xc] = fVar2;
  pfVar3[4] = fVar2;
  fVar2 = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
          _DAT_00a2a0b4;
  pfVar3[0x1d] = fVar2;
  pfVar3[0x15] = fVar2;
  pfVar3[0xd] = fVar2;
  pfVar3[5] = fVar2;
  pfVar3[6] = 0.0;
  pfVar3[7] = 0.0;
  pfVar3[0xe] = 0.0;
  pfVar3[0xf] = 0.0;
  pfVar3[0x16] = 0.0;
  pfVar3[0x17] = 0.0;
  pfVar3[0x1e] = 0.0;
  pfVar3[0x1f] = 0.0;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar3;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a09980 @ 00a09980 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a09980(int param_1,int param_2,int param_3)

{
  int *piVar1;
  short sVar2;
  float fVar3;
  float *pfVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  
  if (((*(byte *)(param_1 + 4) == DAT_011be764) && (*(byte *)(param_1 + 5) == DAT_011be768)) &&
     (*(byte *)(param_1 + 6) == DAT_011be76c)) {
    if (DAT_00a2c488 == -1) goto LAB_00a09b26;
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
  }
  else {
    DAT_011be76c = (ushort)*(byte *)(param_1 + 6);
    iVar6 = (int)(short)DAT_011be76c;
    DAT_011be768 = (ushort)*(byte *)(param_1 + 5);
    DAT_011be764 = (ushort)*(byte *)(param_1 + 4);
    iVar7 = (int)(short)DAT_011be768;
    iVar5 = (int)(short)DAT_011be764;
    if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != iVar5)) {
      DAT_00a2c578 = -3;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffd;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = iVar5;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar7;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + iVar6;
  }
  DAT_00a2c488 = -1;
LAB_00a09b26:
  iVar5 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = iVar5;
    *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar5;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar4 = DAT_011be7a8;
  DAT_00a2c480 = 1;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar4[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar4[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar4[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar4[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar4[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar4[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar4[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar4[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar4[3] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 8) * 8) * _DAT_00a2a0b4;
  pfVar4[4] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 8) * 8) *
              _DAT_00a2a0b4;
  pfVar4[5] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 8) * 8) *
              _DAT_00a2a0b4;
  pfVar4[0xb] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0xc) * 8) *
                _DAT_00a2a0b4;
  pfVar4[0xc] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8) *
                _DAT_00a2a0b4;
  pfVar4[0xd] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8) *
                _DAT_00a2a0b4;
  pfVar4[0x13] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
                 _DAT_00a2a0b4;
  pfVar4[0x14] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
                 _DAT_00a2a0b4;
  sVar2 = *(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar4[0x17] = 0.0;
  pfVar4[0x16] = 0.0;
  pfVar4[0xf] = 0.0;
  pfVar4[0xe] = 0.0;
  pfVar4[7] = 0.0;
  fVar3 = (float)(int)sVar2 * _DAT_00a2a0b4;
  pfVar4[6] = 0.0;
  pfVar4[0x15] = fVar3;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 1)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar4;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a09e60 @ 00a09e60 */
/* ===================================================== */

void FUN_00a09e60(int param_1,int param_2)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  int iVar4;
  float *pfVar5;
  
  if (DAT_00a2c488 != -2) {
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    DAT_00a2c488 = -2;
  }
  iVar4 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = iVar4;
    *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar4;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar5 = DAT_011be7a8;
  DAT_00a2c480 = 5;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar5[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar5[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x10) * 8);
  pfVar5[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar5[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar5[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar5[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar5[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar5[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar5[4] = (float)(((uint)*(byte *)(param_1 + 4) * 0x100 + (uint)*(byte *)(param_1 + 5)) * 0x100
                     + (uint)*(byte *)(param_1 + 6));
  pfVar5[0xc] = (float)(((uint)*(byte *)(param_1 + 8) * 0x100 + (uint)*(byte *)(param_1 + 9)) *
                        0x100 + (uint)*(byte *)(param_1 + 10));
  bVar1 = *(byte *)(param_1 + 0xc);
  bVar2 = *(byte *)(param_1 + 0xd);
  bVar3 = *(byte *)(param_1 + 0xe);
  pfVar5[0x17] = 0.0;
  pfVar5[0x16] = 0.0;
  pfVar5[0x14] = (float)(((uint)bVar1 * 0x100 + (uint)bVar2) * 0x100 + (uint)bVar3);
  pfVar5[0xf] = 0.0;
  pfVar5[0xe] = 0.0;
  pfVar5[7] = 0.0;
  pfVar5[6] = 0.0;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 2)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar5;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a0a1a0 @ 00a0a1a0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0a1a0(int param_1,int param_2,int param_3)

{
  int *piVar1;
  float *pfVar2;
  uint uVar3;
  uint uVar4;
  
  uVar3 = (uint)*(ushort *)(param_1 + 10);
  uVar4 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar3 != DAT_00a2c488) || (uVar4 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar4;
    DAT_00a2c488 = uVar3;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar4;
  }
  uVar3 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = uVar3;
    *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 3;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[3] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
              _DAT_00a2a0b4;
  pfVar2[4] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
              _DAT_00a2a0b4;
  pfVar2[5] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
              _DAT_00a2a0b4;
  pfVar2[0xb] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0xc] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0xd] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0x13] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x18) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x14] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x18) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x15] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x18) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[6] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0x17] = 0.0;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 1)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a0a670 @ 00a0a670 */
/* ===================================================== */

void FUN_00a0a670(int param_1,int param_2)

{
  int *piVar1;
  float *pfVar2;
  uint uVar3;
  uint uVar4;
  
  uVar3 = (uint)*(ushort *)(param_1 + 10);
  uVar4 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar3 != DAT_00a2c488) || (uVar4 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar4;
    DAT_00a2c488 = uVar3;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar4;
  }
  uVar3 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    DAT_00a2c578 = -2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = uVar3;
    *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 7;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1c) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x20) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x20) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x20) * 8);
  pfVar2[4] = (float)(((uint)*(byte *)(param_1 + 0x10) * 0x100 + (uint)*(byte *)(param_1 + 0x11)) *
                      0x100 + (uint)*(byte *)(param_1 + 0x12));
  pfVar2[0xc] = (float)(((uint)*(byte *)(param_1 + 0x14) * 0x100 + (uint)*(byte *)(param_1 + 0x15))
                        * 0x100 + (uint)*(byte *)(param_1 + 0x16));
  pfVar2[0x14] = (float)(((uint)*(byte *)(param_1 + 0x18) * 0x100 + (uint)*(byte *)(param_1 + 0x19))
                         * 0x100 + (uint)*(byte *)(param_1 + 0x1a));
  pfVar2[6] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0x17] = 0.0;
  if ((DAT_00a2c578 != 4) || (DAT_00a2c57c != 2)) {
    DAT_00a2c578 = 4;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 4;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 3;
  DAT_011be7a8 = DAT_011be7a8 + 0x18;
  return;
}



/* ===================================================== */
/* FUN_00a0aa90 @ 00a0aa90 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0aa90(int param_1,int param_2,int param_3)

{
  int *piVar1;
  float *pfVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  
  if (((*(byte *)(param_1 + 4) == DAT_011be764) && (*(byte *)(param_1 + 5) == DAT_011be768)) &&
     (*(byte *)(param_1 + 6) == DAT_011be76c)) {
    if (DAT_00a2c488 == -1) goto LAB_00a0ac36;
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
  }
  else {
    DAT_011be76c = (ushort)*(byte *)(param_1 + 6);
    iVar4 = (int)(short)DAT_011be76c;
    DAT_011be768 = (ushort)*(byte *)(param_1 + 5);
    DAT_011be764 = (ushort)*(byte *)(param_1 + 4);
    iVar5 = (int)(short)DAT_011be768;
    iVar3 = (int)(short)DAT_011be764;
    if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != iVar3)) {
      DAT_00a2c578 = -3;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffd;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = iVar3;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + iVar4;
  }
  DAT_00a2c488 = -1;
LAB_00a0ac36:
  iVar3 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 1;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar2[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar2[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x12) * 8);
  pfVar2[3] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 8) * 8) * _DAT_00a2a0b4;
  pfVar2[4] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 8) * 8) *
              _DAT_00a2a0b4;
  pfVar2[5] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 8) * 8) *
              _DAT_00a2a0b4;
  pfVar2[0xb] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0xc) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0xc] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0xd] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0x13] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x14] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x15] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1b] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1c] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1d] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x10) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1f] = 0.0;
  pfVar2[0x1e] = 0.0;
  pfVar2[0x17] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[6] = 0.0;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a0b000 @ 00a0b000 */
/* ===================================================== */

void FUN_00a0b000(int param_1,int param_2)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  int iVar4;
  float *pfVar5;
  
  if (DAT_00a2c488 != -2) {
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    DAT_00a2c488 = -2;
  }
  iVar4 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar4;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar5 = DAT_011be7a8;
  DAT_00a2c480 = 5;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar5[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar5[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8);
  pfVar5[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar5[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar5[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar5[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar5[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar5[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar5[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar5[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar5[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x18) * 8);
  pfVar5[4] = (float)(((uint)*(byte *)(param_1 + 4) * 0x100 + (uint)*(byte *)(param_1 + 5)) * 0x100
                     + (uint)*(byte *)(param_1 + 6));
  pfVar5[0xc] = (float)(((uint)*(byte *)(param_1 + 8) * 0x100 + (uint)*(byte *)(param_1 + 9)) *
                        0x100 + (uint)*(byte *)(param_1 + 10));
  pfVar5[0x14] = (float)(((uint)*(byte *)(param_1 + 0x10) * 0x100 + (uint)*(byte *)(param_1 + 0x11))
                         * 0x100 + (uint)*(byte *)(param_1 + 0x12));
  bVar1 = *(byte *)(param_1 + 0xd);
  bVar2 = *(byte *)(param_1 + 0xc);
  bVar3 = *(byte *)(param_1 + 0xe);
  pfVar5[0x1f] = 0.0;
  pfVar5[0x1e] = 0.0;
  pfVar5[0x1c] = (float)(((uint)bVar2 * 0x100 + (uint)bVar1) * 0x100 + (uint)bVar3);
  pfVar5[0x17] = 0.0;
  pfVar5[0x16] = 0.0;
  pfVar5[0xf] = 0.0;
  pfVar5[0xe] = 0.0;
  pfVar5[7] = 0.0;
  pfVar5[6] = 0.0;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 2;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar5;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a0b390 @ 00a0b390 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0b390(int param_1,int param_2,int param_3)

{
  int *piVar1;
  float *pfVar2;
  uint uVar3;
  uint uVar4;
  
  uVar3 = (uint)*(ushort *)(param_1 + 10);
  uVar4 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar3 != DAT_00a2c488) || (uVar4 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar4;
    DAT_00a2c488 = uVar3;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar4;
  }
  uVar3 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 3;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x16) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1a) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x22) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x22) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x22) * 8);
  pfVar2[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x1e) * 8);
  pfVar2[3] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
              _DAT_00a2a0b4;
  pfVar2[4] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
              _DAT_00a2a0b4;
  pfVar2[5] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x14) * 8) *
              _DAT_00a2a0b4;
  pfVar2[0xb] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x18) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0xc] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x18) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0xd] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x18) * 8) *
                _DAT_00a2a0b4;
  pfVar2[0x13] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x20) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x14] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x20) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x15] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x20) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1b] = (float)(int)*(short *)(param_3 + (uint)*(ushort *)(param_1 + 0x1c) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1c] = (float)(int)*(short *)(param_3 + 2 + (uint)*(ushort *)(param_1 + 0x1c) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[0x1d] = (float)(int)*(short *)(param_3 + 4 + (uint)*(ushort *)(param_1 + 0x1c) * 8) *
                 _DAT_00a2a0b4;
  pfVar2[6] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0x17] = 0.0;
  pfVar2[0x1e] = 0.0;
  pfVar2[0x1f] = 0.0;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a0b930 @ 00a0b930 */
/* ===================================================== */

void FUN_00a0b930(int param_1,int param_2)

{
  int *piVar1;
  float *pfVar2;
  uint uVar3;
  uint uVar4;
  
  uVar3 = (uint)*(ushort *)(param_1 + 10);
  uVar4 = (uint)*(ushort *)(param_1 + 6);
  if ((uVar3 != DAT_00a2c488) || (uVar4 != DAT_00a2c484)) {
    DAT_00a2c484 = uVar4;
    DAT_00a2c488 = uVar3;
    if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
      DAT_00a2c578 = -4;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffc;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + uVar4;
  }
  uVar3 = DAT_00a2c5a4;
  if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffe;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(uint *)(DAT_011be7ac + DAT_011be7dc * 4) = uVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  pfVar2 = DAT_011be7a8;
  DAT_00a2c480 = 7;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x24) * 8);
  pfVar2[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x24) * 8);
  pfVar2[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x24) * 8);
  pfVar2[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x26) * 8);
  pfVar2[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x26) * 8);
  pfVar2[10] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x26) * 8);
  pfVar2[0x10] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x2a) * 8);
  pfVar2[0x11] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x2a) * 8);
  pfVar2[0x12] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x2a) * 8);
  pfVar2[0x18] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0x28) * 8);
  pfVar2[0x19] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0x28) * 8);
  pfVar2[0x1a] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0x28) * 8);
  pfVar2[4] = (float)(((uint)*(byte *)(param_1 + 0x14) * 0x100 + (uint)*(byte *)(param_1 + 0x15)) *
                      0x100 + (uint)*(byte *)(param_1 + 0x16));
  pfVar2[0xc] = (float)(((uint)*(byte *)(param_1 + 0x18) * 0x100 + (uint)*(byte *)(param_1 + 0x19))
                        * 0x100 + (uint)*(byte *)(param_1 + 0x1a));
  pfVar2[0x14] = (float)(((uint)*(byte *)(param_1 + 0x20) * 0x100 + (uint)*(byte *)(param_1 + 0x21))
                         * 0x100 + (uint)*(byte *)(param_1 + 0x22));
  pfVar2[0x1c] = (float)(((uint)*(byte *)(param_1 + 0x1c) * 0x100 + (uint)*(byte *)(param_1 + 0x1d))
                         * 0x100 + (uint)*(byte *)(param_1 + 0x1e));
  pfVar2[6] = 0.0;
  pfVar2[7] = 0.0;
  pfVar2[0xe] = 0.0;
  pfVar2[0xf] = 0.0;
  pfVar2[0x16] = 0.0;
  pfVar2[0x17] = 0.0;
  pfVar2[0x1e] = 0.0;
  pfVar2[0x1f] = 0.0;
  DAT_00a2c578 = 6;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 6;
  DAT_011be7dc = DAT_011be7dc + 1;
  DAT_00a2c57c = 2;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar2;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
  DAT_011be7dc = DAT_011be7dc + 1;
  *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 4;
  DAT_011be7a8 = DAT_011be7a8 + 0x20;
  return;
}



/* ===================================================== */
/* FUN_00a0bdf0 @ 00a0bdf0 */
/* ===================================================== */

void FUN_00a0bdf0(int param_1,int param_2)

{
  int *piVar1;
  short sVar2;
  float *pfVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  
  if (((*(byte *)(param_1 + 4) == DAT_011be764) && (*(byte *)(param_1 + 5) == DAT_011be768)) &&
     (*(byte *)(param_1 + 6) == DAT_011be76c)) {
    if (DAT_00a2c488 == -1) goto LAB_00a0bf97;
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
  }
  else {
    DAT_011be76c = (ushort)*(byte *)(param_1 + 6);
    iVar5 = (int)(short)DAT_011be76c;
    DAT_011be768 = (ushort)*(byte *)(param_1 + 5);
    DAT_011be764 = (ushort)*(byte *)(param_1 + 4);
    iVar6 = (int)(short)DAT_011be768;
    iVar4 = (int)(short)DAT_011be764;
    if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != iVar4)) {
      DAT_00a2c578 = -3;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffd;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = iVar4;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + iVar5;
  }
  DAT_00a2c488 = -1;
LAB_00a0bf97:
  if ((DAT_00a2c480 & 9) != 0) {
    FUN_00a07ca0(0xfffffffe,1,0,0);
  }
  pfVar3 = DAT_011be7a8;
  DAT_00a2c480 = 0;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar3[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar3[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 8) * 8);
  pfVar3[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar3[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 10) * 8);
  sVar2 = *(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 10) * 8);
  pfVar3[0xc] = 0.0;
  pfVar3[4] = 0.0;
  pfVar3[0xb] = 0.0;
  pfVar3[3] = 0.0;
  pfVar3[0xf] = 0.0;
  pfVar3[10] = (float)(int)sVar2;
  pfVar3[0xe] = 0.0;
  pfVar3[7] = 0.0;
  pfVar3[6] = 0.0;
  if ((DAT_00a2c578 != 2) || (DAT_00a2c57c != 1)) {
    DAT_00a2c578 = 2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 2;
  DAT_011be7a8 = DAT_011be7a8 + 0x10;
  return;
}



/* ===================================================== */
/* FUN_00a0c110 @ 00a0c110 */
/* ===================================================== */

void FUN_00a0c110(int param_1,int param_2)

{
  int *piVar1;
  short sVar2;
  float *pfVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  
  if (((*(byte *)(param_1 + 4) == DAT_011be764) && (*(byte *)(param_1 + 5) == DAT_011be768)) &&
     (*(byte *)(param_1 + 6) == DAT_011be76c)) {
    if (DAT_00a2c488 == -1) goto LAB_00a0c2b7;
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
      DAT_00a2c578 = -5;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffb;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 0;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
  }
  else {
    DAT_011be76c = (ushort)*(byte *)(param_1 + 6);
    iVar5 = (int)(short)DAT_011be76c;
    DAT_011be768 = (ushort)*(byte *)(param_1 + 5);
    DAT_011be764 = (ushort)*(byte *)(param_1 + 4);
    iVar6 = (int)(short)DAT_011be768;
    iVar4 = (int)(short)DAT_011be764;
    if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != iVar4)) {
      DAT_00a2c578 = -3;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xfffffffd;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = iVar4;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(int *)(DAT_011be7ac + DAT_011be7dc * 4) = iVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
    }
    piVar1 = (int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4);
    *piVar1 = *piVar1 + iVar5;
  }
  DAT_00a2c488 = -1;
LAB_00a0c2b7:
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    FUN_00a07ca0(0xfffffffe,DAT_00a2c5a4,0,0);
  }
  pfVar3 = DAT_011be7a8;
  DAT_00a2c480 = 1;
  *DAT_011be7a8 = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar3[1] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar3[2] = (float)(int)*(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xc) * 8);
  pfVar3[8] = (float)(int)*(short *)(param_2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar3[9] = (float)(int)*(short *)(param_2 + 2 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  sVar2 = *(short *)(param_2 + 4 + (uint)*(ushort *)(param_1 + 0xe) * 8);
  pfVar3[0xc] = 0.0;
  pfVar3[4] = 0.0;
  pfVar3[0xb] = 0.0;
  pfVar3[3] = 0.0;
  pfVar3[0xf] = 0.0;
  pfVar3[10] = (float)(int)sVar2;
  pfVar3[0xe] = 0.0;
  pfVar3[7] = 0.0;
  pfVar3[6] = 0.0;
  if ((DAT_00a2c578 != 2) || (DAT_00a2c57c != 1)) {
    DAT_00a2c578 = 2;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 2;
    DAT_011be7dc = DAT_011be7dc + 1;
    DAT_00a2c57c = 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 1;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(float **)(DAT_011be7ac + DAT_011be7dc * 4) = pfVar3;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0;
    DAT_011be7dc = DAT_011be7dc + 1;
    *(undefined4 *)(DAT_011be7ac + DAT_011be7dc * 4) = 0xffffffff;
  }
  *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) =
       *(int *)(DAT_011be7ac + -4 + DAT_011be7dc * 4) + 2;
  DAT_011be7a8 = DAT_011be7a8 + 0x10;
  return;
}



/* ===================================================== */
/* FUN_00a0c440 @ 00a0c440 */
/* ===================================================== */

void FUN_00a0c440(int param_1)

{
  undefined4 *puVar1;
  
                    /* 0xc440  754   */
  puVar1 = *(undefined4 **)(param_1 + 8);
  if (puVar1[1] == -1) {
    DAT_011be7ac = (undefined4 *)*puVar1;
    *puVar1 = *DAT_011be7ac;
    puVar1[1] = DAT_011be7ac[1];
    Free(DAT_011be7ac);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0c480 @ 00a0c480 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0c480(int param_1)

{
  DAT_011be7ac = Malloc(param_1 * 0x94);
  DAT_011be7dc = 2;
  DAT_00a2c480 = 8;
  DAT_011be7a8 = DAT_011be7ac + param_1 * 0x14;
  _DAT_011be7a4 = DAT_011be7a8;
  DAT_00a2c578 = 0xffffffff;
  DAT_00a2c57c = 0xffffffff;
  DAT_011be764 = 0xffff;
  DAT_011be768 = 0xffff;
  DAT_011be76c = 0xffff;
  DAT_00a2c484 = 0xffffffff;
  DAT_00a2c488 = 0xffffffff;
  DAT_00a2c48c = 0xffffffff;
  return;
}



/* ===================================================== */
/* FUN_00a0c4f0 @ 00a0c4f0 */
/* ===================================================== */

void FUN_00a0c4f0(int param_1)

{
  undefined4 *puVar1;
  int iVar2;
  uint uVar3;
  uint uVar4;
  uint uVar5;
  undefined4 *puVar6;
  
                    /* 0xc4f0  755   */
  puVar6 = *(undefined4 **)(param_1 + 8);
  if (puVar6[1] != -1) {
    FUN_00a06a40(puVar6,0xffffffff);
    FUN_00a0c480(DAT_011be7cc);
    *DAT_011be7ac = *puVar6;
    DAT_011be7ac[1] = puVar6[1];
    puVar6[1] = 0xffffffff;
    *puVar6 = DAT_011be7ac;
    uVar5 = 0;
    puVar1 = (undefined4 *)Malloc(DAT_011be7cc * 2);
    iVar2 = Malloc(DAT_011be7cc * 4);
    uVar4 = DAT_011be7cc * 2;
    puVar6 = puVar1;
    for (uVar3 = (DAT_011be7cc & 0x7fffffff) >> 1; uVar3 != 0; uVar3 = uVar3 - 1) {
      *puVar6 = 0;
      puVar6 = puVar6 + 1;
    }
    for (uVar4 = uVar4 & 3; uVar4 != 0; uVar4 = uVar4 - 1) {
      *(undefined1 *)puVar6 = 0;
      puVar6 = (undefined4 *)((int)puVar6 + 1);
    }
    DAT_011be7d4 = 0;
    uVar4 = DAT_011be7cc;
    if (DAT_011be7cc != 0) {
      do {
        *(int *)(iVar2 + DAT_011be7d4 * 4) = DAT_011be7d8;
        switch(*(byte *)(DAT_011be7d8 + 3) & 0xfd) {
        case 0x20:
        case 0x21:
        case 0x29:
        case 0x50:
          DAT_011be7d8 = DAT_011be7d8 + 0x10;
          break;
        case 0x24:
        case 0x31:
        case 0x38:
          DAT_011be7d8 = DAT_011be7d8 + 0x18;
          break;
        case 0x25:
        case 0x34:
        case 0x39:
          DAT_011be7d8 = DAT_011be7d8 + 0x1c;
          break;
        case 0x28:
        case 0x30:
          DAT_011be7d8 = DAT_011be7d8 + 0x14;
          break;
        case 0x2c:
        case 0x2d:
          DAT_011be7d8 = DAT_011be7d8 + 0x20;
          break;
        case 0x35:
        case 0x3c:
          DAT_011be7d8 = DAT_011be7d8 + 0x24;
          break;
        case 0x3d:
          DAT_011be7d8 = DAT_011be7d8 + 0x2c;
          break;
        case 0x40:
          DAT_011be7d8 = DAT_011be7d8 + 0xc;
        }
        DAT_011be7d4 = DAT_011be7d4 + 1;
        uVar4 = DAT_011be7cc;
      } while (DAT_011be7d4 < DAT_011be7cc);
    }
    do {
      uVar3 = uVar5;
      DAT_011be7d4 = uVar5;
      if (uVar5 < uVar4) {
        do {
          DAT_011be7d8 = *(int *)(iVar2 + uVar5 * 4);
          uVar4 = uVar5;
          if (*(short *)((int)puVar1 + uVar5 * 2) == 0) {
            DAT_011be7d4 = uVar5;
            switch(*(byte *)(DAT_011be7d8 + 3) & 0xfd) {
            case 0x20:
              if ((uVar3 == uVar5) ||
                 ((((DAT_00a2c480 == 0 && (*(byte *)(DAT_011be7d8 + 4) == DAT_011be764)) &&
                   (*(byte *)(DAT_011be7d8 + 5) == DAT_011be768)) &&
                  (*(byte *)(DAT_011be7d8 + 6) == DAT_011be76c)))) {
                FUN_00a078c0(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
LAB_00a0cb60:
                *(undefined2 *)((int)puVar1 + DAT_011be7d4 * 2) = 1;
joined_r0x00a0cbdd:
                uVar5 = DAT_011be7d4;
                uVar4 = DAT_011be7d4 - 1;
                goto joined_r0x00a0cbdd;
              }
              break;
            case 0x21:
              if ((uVar3 == uVar5) || (DAT_00a2c480 == 4)) {
                FUN_00a07d80(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x24:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 2 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a08850(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x25:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 6 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a08c80(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x28:
              if ((uVar3 == uVar5) ||
                 ((((DAT_00a2c480 == 0 && (*(byte *)(DAT_011be7d8 + 4) == DAT_011be764)) &&
                   (*(byte *)(DAT_011be7d8 + 5) == DAT_011be768)) &&
                  (*(byte *)(DAT_011be7d8 + 6) == DAT_011be76c)))) {
                FUN_00a08080(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x29:
              if ((uVar3 == uVar5) || (DAT_00a2c480 == 4)) {
                FUN_00a08510(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x2c:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 2 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a094d0(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x2d:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 6 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a09070(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x30:
              if ((uVar3 == uVar5) ||
                 ((((DAT_00a2c480 == 1 && (*(byte *)(DAT_011be7d8 + 4) == DAT_011be764)) &&
                   (*(byte *)(DAT_011be7d8 + 5) == DAT_011be768)) &&
                  (*(byte *)(DAT_011be7d8 + 6) == DAT_011be76c)))) {
                FUN_00a09980(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x31:
              if ((uVar3 == uVar5) || (DAT_00a2c480 == 5)) {
                FUN_00a09e60(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x34:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 3 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a0a1a0(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x35:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 7 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a0a670(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x38:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 1 && (*(byte *)(DAT_011be7d8 + 4) == DAT_011be764)) &&
                  ((*(byte *)(DAT_011be7d8 + 5) == DAT_011be768 &&
                   (*(byte *)(DAT_011be7d8 + 6) == DAT_011be76c)))))) {
                FUN_00a0aa90(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x39:
              if ((uVar3 == uVar5) || (DAT_00a2c480 == 5)) {
                FUN_00a0b000(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x3c:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 3 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a0b390(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x3d:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 7 && (*(ushort *)(DAT_011be7d8 + 6) == DAT_00a2c484)) &&
                  (*(ushort *)(DAT_011be7d8 + 10) == DAT_00a2c488)))) {
                FUN_00a0b930(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x40:
              if ((uVar3 == uVar5) ||
                 (((DAT_00a2c480 == 0 && (*(byte *)(DAT_011be7d8 + 4) == DAT_011be764)) &&
                  ((*(byte *)(DAT_011be7d8 + 5) == DAT_011be768 &&
                   (*(byte *)(DAT_011be7d8 + 6) == DAT_011be76c)))))) {
                FUN_00a0bdf0(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                goto LAB_00a0cb60;
              }
              break;
            case 0x50:
              if ((uVar3 == uVar5) ||
                 ((((DAT_00a2c480 == 1 && (*(byte *)(DAT_011be7d8 + 4) == DAT_011be764)) &&
                   (*(byte *)(DAT_011be7d8 + 5) == DAT_011be768)) &&
                  (*(byte *)(DAT_011be7d8 + 6) == DAT_011be76c)))) {
                FUN_00a0c110(DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
                *(undefined2 *)((int)puVar1 + DAT_011be7d4 * 2) = 1;
                goto joined_r0x00a0cbdd;
              }
            }
          }
          else {
joined_r0x00a0cbdd:
            if (uVar3 == uVar4) {
              uVar3 = uVar3 + 1;
            }
          }
          uVar5 = uVar5 + 1;
          uVar4 = DAT_011be7cc;
          DAT_011be7d4 = uVar5;
        } while (uVar5 < DAT_011be7cc);
      }
      uVar5 = uVar3 + 1;
    } while (uVar5 < uVar4);
    Free(puVar1);
    Free(iVar2);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0cd20 @ 00a0cd20 */
/* ===================================================== */

void FUN_00a0cd20(void)

{
  int *piVar1;
  undefined4 uVar2;
  int iVar3;
  int iVar4;
  
  if (*(int *)(DAT_011be7ac + 8) != -1) {
    iVar4 = 0xc;
    iVar3 = DAT_011be7ac;
    do {
      uVar2 = *(undefined4 *)(iVar3 + -4 + iVar4);
      switch(uVar2) {
      case 0xfffffffb:
        (**(code **)(*DAT_011be760 + 100))(0);
        iVar3 = DAT_011be7ac;
        break;
      case 0xfffffffc:
        FUN_00a27880(*(undefined4 *)(iVar3 + iVar4),*(undefined4 *)(iVar3 + 8 + iVar4),0,0);
        iVar3 = DAT_011be7ac;
        break;
      case 0xfffffffd:
        break;
      case 0xfffffffe:
        (**(code **)(*DAT_011be760 + 0x8c))(*(undefined4 *)(iVar3 + iVar4));
        iVar3 = DAT_011be7ac;
        break;
      default:
        (**(code **)(*DAT_011be760 + 0xbc))
                  (uVar2,*(undefined4 *)(iVar3 + iVar4),*(undefined4 *)(iVar3 + 4 + iVar4),
                   *(undefined4 *)(iVar3 + 8 + iVar4),0);
        iVar3 = DAT_011be7ac;
      }
      piVar1 = (int *)(iVar3 + 0xc + iVar4);
      iVar4 = iVar4 + 0x10;
    } while (*piVar1 != -1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0cdd0 @ 00a0cdd0 */
/* ===================================================== */

void FUN_00a0cdd0(int param_1)

{
  undefined4 *puVar1;
  
  puVar1 = *(undefined4 **)(param_1 + 8);
  if (puVar1[1] != -1) {
    FUN_00a0c4f0(param_1);
  }
  DAT_011be7ac = *puVar1;
  FUN_00a0cd20();
  return;
}



/* ===================================================== */
/* FUN_00a0ce00 @ 00a0ce00 */
/* ===================================================== */

uint * FUN_00a0ce00(uint *param_1)

{
  byte bVar1;
  
                    /* 0xce00  663   */
  if (DAT_011be7cc <= DAT_011be7d4) {
    return (uint *)0x0;
  }
  *param_1 = 0;
  bVar1 = *DAT_011be7d8;
  *param_1 = (uint)bVar1;
  param_1[0x18] = DAT_011be7b8;
  param_1[0x19] = DAT_011be7c0;
  switch(bVar1 & 0xfd) {
  case 0x20:
    FUN_00a06af0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x21:
    FUN_00a06b90(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x24:
    FUN_00a06ca0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x25:
    FUN_00a06d60(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x28:
    FUN_00a06c10(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x29:
    FUN_00a06c60(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x2c:
    FUN_00a06f00(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x2d:
    FUN_00a06e20(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x30:
    FUN_00a06ff0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x31:
    FUN_00a070d0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x34:
    FUN_00a07180(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x35:
    FUN_00a07280(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x38:
    FUN_00a07360(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x39:
    FUN_00a073d0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x3c:
    FUN_00a074a0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x3d:
    FUN_00a075e0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x40:
    FUN_00a076f0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x50:
    FUN_00a07760(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 100:
    FUN_00a077f0(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
    DAT_011be7d4 = DAT_011be7d4 + 1;
    return param_1;
  case 0x6c:
  case 0x74:
  case 0x7c:
    FUN_00a07860(param_1,&DAT_011be7d8,DAT_011be7b8,DAT_011be7c0);
  }
  DAT_011be7d4 = DAT_011be7d4 + 1;
  return param_1;
}



/* ===================================================== */
/* FUN_00a0d260 @ 00a0d260 */
/* ===================================================== */

void FUN_00a0d260(short *param_1)

{
  short *psVar1;
  int iVar2;
  undefined1 local_1c [4];
  undefined1 local_18;
  undefined1 local_17;
  short local_16;
  undefined1 local_14;
  undefined1 local_13;
  short local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xd;
    do {
      local_16 = psVar1[-4];
      local_18 = (undefined1)psVar1[-5];
      local_12 = *psVar1;
      local_17 = *(undefined1 *)((int)psVar1 + -9);
      local_13 = *(undefined1 *)((int)psVar1 + -1);
      local_10 = (undefined1)psVar1[3];
      local_14 = (undefined1)psVar1[-1];
      local_c = (undefined1)psVar1[-9];
      local_b = *(undefined1 *)((int)psVar1 + -0x11);
      local_f = *(undefined1 *)((int)psVar1 + 7);
      local_a = (undefined1)psVar1[-8];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      FUN_00a08c80(local_1c,psVar1 + 0x15,0);
      psVar1 = psVar1 + 0x2c;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d310 @ 00a0d310 */
/* ===================================================== */

void FUN_00a0d310(short *param_1)

{
  short *psVar1;
  int iVar2;
  undefined1 local_20 [4];
  undefined1 local_1c;
  undefined1 local_1b;
  short local_1a;
  undefined1 local_18;
  undefined1 local_17;
  short local_16;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  undefined2 local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xd;
    do {
      local_16 = *psVar1;
      local_1c = (undefined1)psVar1[-5];
      local_1a = psVar1[-4];
      local_18 = (undefined1)psVar1[-1];
      local_17 = *(undefined1 *)((int)psVar1 + -1);
      local_1b = *(undefined1 *)((int)psVar1 + -9);
      local_13 = *(undefined1 *)((int)psVar1 + 7);
      local_10 = (undefined1)psVar1[7];
      local_14 = (undefined1)psVar1[3];
      local_c = (undefined1)psVar1[-9];
      local_b = *(undefined1 *)((int)psVar1 + -0x11);
      local_f = *(undefined1 *)((int)psVar1 + 0xf);
      local_a = (undefined1)psVar1[-8];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      local_2 = 3;
      FUN_00a09070(local_20,psVar1 + 0x1d,0);
      psVar1 = psVar1 + 0x38;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d3d0 @ 00a0d3d0 */
/* ===================================================== */

void FUN_00a0d3d0(short *param_1)

{
  short *psVar1;
  int iVar2;
  undefined1 local_24 [4];
  undefined1 local_20;
  undefined1 local_1f;
  short local_1e;
  undefined1 local_1c;
  undefined1 local_1b;
  short local_1a;
  undefined1 local_18;
  undefined1 local_17;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xf;
    do {
      local_1e = psVar1[-6];
      local_20 = (undefined1)psVar1[-7];
      local_1a = *psVar1;
      local_1f = *(undefined1 *)((int)psVar1 + -0xd);
      local_1b = *(undefined1 *)((int)psVar1 + -1);
      local_18 = (undefined1)psVar1[5];
      local_1c = (undefined1)psVar1[-1];
      local_14 = (undefined1)psVar1[-0xb];
      local_13 = *(undefined1 *)((int)psVar1 + -0x15);
      local_17 = *(undefined1 *)((int)psVar1 + 0xb);
      local_10 = (undefined1)psVar1[-5];
      local_f = *(undefined1 *)((int)psVar1 + -9);
      local_12 = (undefined1)psVar1[-10];
      local_c = (undefined1)psVar1[1];
      local_b = *(undefined1 *)((int)psVar1 + 3);
      local_e = (undefined1)psVar1[-4];
      local_a = (undefined1)psVar1[2];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      FUN_00a0a670(local_24,psVar1 + 0x1b,0);
      psVar1 = psVar1 + 0x34;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d4b0 @ 00a0d4b0 */
/* ===================================================== */

void FUN_00a0d4b0(short *param_1)

{
  short *psVar1;
  int iVar2;
  undefined1 local_2c [4];
  undefined1 local_28;
  undefined1 local_27;
  short local_26;
  undefined1 local_24;
  undefined1 local_23;
  short local_22;
  undefined1 local_20;
  undefined1 local_1f;
  undefined1 local_1c;
  undefined1 local_1b;
  undefined1 local_18;
  undefined1 local_17;
  undefined1 local_16;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  undefined2 local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xf;
    do {
      local_22 = *psVar1;
      local_28 = (undefined1)psVar1[-7];
      local_26 = psVar1[-6];
      local_24 = (undefined1)psVar1[-1];
      local_23 = *(undefined1 *)((int)psVar1 + -1);
      local_27 = *(undefined1 *)((int)psVar1 + -0xd);
      local_1f = *(undefined1 *)((int)psVar1 + 0xb);
      local_1c = (undefined1)psVar1[0xb];
      local_20 = (undefined1)psVar1[5];
      local_18 = (undefined1)psVar1[-0xb];
      local_17 = *(undefined1 *)((int)psVar1 + -0x15);
      local_1b = *(undefined1 *)((int)psVar1 + 0x17);
      local_14 = (undefined1)psVar1[-5];
      local_13 = *(undefined1 *)((int)psVar1 + -9);
      local_16 = (undefined1)psVar1[-10];
      local_10 = (undefined1)psVar1[1];
      local_f = *(undefined1 *)((int)psVar1 + 3);
      local_12 = (undefined1)psVar1[-4];
      local_c = (undefined1)psVar1[7];
      local_b = *(undefined1 *)((int)psVar1 + 0xf);
      local_e = (undefined1)psVar1[2];
      local_a = (undefined1)psVar1[8];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      local_2 = 3;
      FUN_00a0b930(local_2c,psVar1 + 0x27,0);
      psVar1 = psVar1 + 0x44;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d5c0 @ 00a0d5c0 */
/* ===================================================== */

void FUN_00a0d5c0(short *param_1)

{
  undefined1 *puVar1;
  int iVar2;
  undefined1 local_10 [4];
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    puVar1 = (undefined1 *)((int)param_1 + 9);
    do {
      local_c = puVar1[-1];
      local_b = *puVar1;
      local_a = puVar1[1];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      FUN_00a07d80(local_10,puVar1 + 0x23,0);
      puVar1 = puVar1 + 0x40;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d630 @ 00a0d630 */
/* ===================================================== */

void FUN_00a0d630(short *param_1)

{
  undefined1 *puVar1;
  int iVar2;
  undefined1 local_10 [4];
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  undefined2 local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    puVar1 = (undefined1 *)((int)param_1 + 9);
    do {
      local_c = puVar1[-1];
      local_b = *puVar1;
      local_a = puVar1[1];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      local_2 = 3;
      FUN_00a08510(local_10,puVar1 + 0x2b,0);
      puVar1 = puVar1 + 0x50;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d6a0 @ 00a0d6a0 */
/* ===================================================== */

void FUN_00a0d6a0(short *param_1)

{
  undefined1 *puVar1;
  int iVar2;
  undefined1 local_18 [4];
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    puVar1 = (undefined1 *)((int)param_1 + 9);
    do {
      local_14 = puVar1[-1];
      local_13 = *puVar1;
      local_12 = puVar1[1];
      local_10 = puVar1[7];
      local_f = puVar1[8];
      local_c = puVar1[0xf];
      local_b = puVar1[0x10];
      local_e = puVar1[9];
      local_a = puVar1[0x11];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      FUN_00a09e60(local_18,puVar1 + 0x33,0);
      puVar1 = puVar1 + 0x50;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d730 @ 00a0d730 */
/* ===================================================== */

void FUN_00a0d730(short *param_1)

{
  undefined1 *puVar1;
  int iVar2;
  undefined1 local_1c [4];
  undefined1 local_18;
  undefined1 local_17;
  undefined1 local_16;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  undefined2 local_8;
  undefined2 local_6;
  undefined2 local_4;
  undefined2 local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    puVar1 = (undefined1 *)((int)param_1 + 9);
    do {
      local_18 = puVar1[-1];
      local_17 = *puVar1;
      local_16 = puVar1[1];
      local_14 = puVar1[7];
      local_13 = puVar1[8];
      local_10 = puVar1[0xf];
      local_f = puVar1[0x10];
      local_12 = puVar1[9];
      local_c = puVar1[0x17];
      local_b = puVar1[0x18];
      local_e = puVar1[0x11];
      local_a = puVar1[0x19];
      local_8 = 0;
      local_6 = 1;
      local_4 = 2;
      local_2 = 3;
      FUN_00a0b000(local_1c,puVar1 + 0x43,0);
      puVar1 = puVar1 + 0x68;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d7f0 @ 00a0d7f0 */
/* ===================================================== */

void FUN_00a0d7f0(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_1c [4];
  undefined1 local_18;
  undefined1 local_17;
  short local_16;
  undefined1 local_14;
  undefined1 local_13;
  short local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xd;
    do {
      local_16 = psVar1[-4];
      local_12 = *psVar1;
      local_8 = psVar1[0x15] >> 3;
      local_18 = (undefined1)psVar1[-5];
      local_6 = psVar1[0x17] >> 3;
      local_4 = psVar1[0x19] >> 3;
      local_13 = *(undefined1 *)((int)psVar1 + -1);
      local_17 = *(undefined1 *)((int)psVar1 + -9);
      local_14 = (undefined1)psVar1[-1];
      local_c = (undefined1)psVar1[-9];
      local_10 = (undefined1)psVar1[3];
      local_f = *(undefined1 *)((int)psVar1 + 7);
      local_b = *(undefined1 *)((int)psVar1 + -0x11);
      local_a = (undefined1)psVar1[-8];
      FUN_00a08c80(local_1c,param_2,0);
      psVar1 = psVar1 + 0x26;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d8b0 @ 00a0d8b0 */
/* ===================================================== */

void FUN_00a0d8b0(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_20 [4];
  undefined1 local_1c;
  undefined1 local_1b;
  short local_1a;
  undefined1 local_18;
  undefined1 local_17;
  short local_16;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  short local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xd;
    do {
      local_1a = psVar1[-4];
      local_16 = *psVar1;
      local_8 = psVar1[0x1d] >> 3;
      local_2 = psVar1[0x23] >> 3;
      local_6 = psVar1[0x1f] >> 3;
      local_4 = psVar1[0x21] >> 3;
      local_18 = (undefined1)psVar1[-1];
      local_1c = (undefined1)psVar1[-5];
      local_1b = *(undefined1 *)((int)psVar1 + -9);
      local_13 = *(undefined1 *)((int)psVar1 + 7);
      local_17 = *(undefined1 *)((int)psVar1 + -1);
      local_14 = (undefined1)psVar1[3];
      local_c = (undefined1)psVar1[-9];
      local_10 = (undefined1)psVar1[7];
      local_f = *(undefined1 *)((int)psVar1 + 0xf);
      local_b = *(undefined1 *)((int)psVar1 + -0x11);
      local_a = (undefined1)psVar1[-8];
      FUN_00a09070(local_20,param_2,0);
      psVar1 = psVar1 + 0x30;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0d990 @ 00a0d990 */
/* ===================================================== */

void FUN_00a0d990(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_24 [4];
  undefined1 local_20;
  undefined1 local_1f;
  short local_1e;
  undefined1 local_1c;
  undefined1 local_1b;
  short local_1a;
  undefined1 local_18;
  undefined1 local_17;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xf;
    do {
      local_1e = psVar1[-6];
      local_1a = *psVar1;
      local_8 = psVar1[0x1b] >> 3;
      local_20 = (undefined1)psVar1[-7];
      local_6 = psVar1[0x1d] >> 3;
      local_4 = psVar1[0x1f] >> 3;
      local_1b = *(undefined1 *)((int)psVar1 + -1);
      local_1f = *(undefined1 *)((int)psVar1 + -0xd);
      local_1c = (undefined1)psVar1[-1];
      local_14 = (undefined1)psVar1[-0xb];
      local_18 = (undefined1)psVar1[5];
      local_17 = *(undefined1 *)((int)psVar1 + 0xb);
      local_10 = (undefined1)psVar1[-5];
      local_13 = *(undefined1 *)((int)psVar1 + -0x15);
      local_12 = (undefined1)psVar1[-10];
      local_c = (undefined1)psVar1[1];
      local_f = *(undefined1 *)((int)psVar1 + -9);
      local_e = (undefined1)psVar1[-4];
      local_b = *(undefined1 *)((int)psVar1 + 3);
      local_a = (undefined1)psVar1[2];
      FUN_00a0a670(local_24,param_2,0);
      psVar1 = psVar1 + 0x2e;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0da80 @ 00a0da80 */
/* ===================================================== */

void FUN_00a0da80(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_2c [4];
  undefined1 local_28;
  undefined1 local_27;
  short local_26;
  undefined1 local_24;
  undefined1 local_23;
  short local_22;
  undefined1 local_20;
  undefined1 local_1f;
  undefined1 local_1c;
  undefined1 local_1b;
  undefined1 local_18;
  undefined1 local_17;
  undefined1 local_16;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  short local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0xf;
    do {
      local_26 = psVar1[-6];
      local_22 = *psVar1;
      local_8 = psVar1[0x27] >> 3;
      local_2 = psVar1[0x2d] >> 3;
      local_6 = psVar1[0x29] >> 3;
      local_4 = psVar1[0x2b] >> 3;
      local_24 = (undefined1)psVar1[-1];
      local_28 = (undefined1)psVar1[-7];
      local_27 = *(undefined1 *)((int)psVar1 + -0xd);
      local_1f = *(undefined1 *)((int)psVar1 + 0xb);
      local_23 = *(undefined1 *)((int)psVar1 + -1);
      local_20 = (undefined1)psVar1[5];
      local_18 = (undefined1)psVar1[-0xb];
      local_1c = (undefined1)psVar1[0xb];
      local_1b = *(undefined1 *)((int)psVar1 + 0x17);
      local_14 = (undefined1)psVar1[-5];
      local_17 = *(undefined1 *)((int)psVar1 + -0x15);
      local_16 = (undefined1)psVar1[-10];
      local_10 = (undefined1)psVar1[1];
      local_13 = *(undefined1 *)((int)psVar1 + -9);
      local_12 = (undefined1)psVar1[-4];
      local_c = (undefined1)psVar1[7];
      local_f = *(undefined1 *)((int)psVar1 + 3);
      local_e = (undefined1)psVar1[2];
      local_b = *(undefined1 *)((int)psVar1 + 0xf);
      local_a = (undefined1)psVar1[8];
      FUN_00a0b930(local_2c,param_2,0);
      psVar1 = psVar1 + 0x3c;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0dba0 @ 00a0dba0 */
/* ===================================================== */

void FUN_00a0dba0(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_10 [4];
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  short local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0x1c;
    do {
      local_8 = psVar1[-2] >> 3;
      local_6 = *psVar1 >> 3;
      local_c = (undefined1)psVar1[-0x18];
      local_4 = psVar1[2] >> 3;
      local_2 = psVar1[4] >> 3;
      local_b = *(undefined1 *)((int)psVar1 + -0x2f);
      local_a = (undefined1)psVar1[-0x17];
      FUN_00a08510(local_10,param_2,0);
      psVar1 = psVar1 + 0x20;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0dc30 @ 00a0dc30 */
/* ===================================================== */

void FUN_00a0dc30(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_18 [4];
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0x20;
    do {
      local_8 = psVar1[-2] >> 3;
      local_14 = (undefined1)psVar1[-0x1c];
      local_6 = *psVar1 >> 3;
      local_4 = psVar1[2] >> 3;
      local_10 = (undefined1)psVar1[-0x18];
      local_13 = *(undefined1 *)((int)psVar1 + -0x37);
      local_12 = (undefined1)psVar1[-0x1b];
      local_c = (undefined1)psVar1[-0x14];
      local_f = *(undefined1 *)((int)psVar1 + -0x2f);
      local_e = (undefined1)psVar1[-0x17];
      local_b = *(undefined1 *)((int)psVar1 + -0x27);
      local_a = (undefined1)psVar1[-0x13];
      FUN_00a09e60(local_18,param_2,0);
      psVar1 = psVar1 + 0x22;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0dce0 @ 00a0dce0 */
/* ===================================================== */

void FUN_00a0dce0(short *param_1,undefined4 param_2)

{
  short *psVar1;
  int iVar2;
  undefined1 local_1c [4];
  undefined1 local_18;
  undefined1 local_17;
  undefined1 local_16;
  undefined1 local_14;
  undefined1 local_13;
  undefined1 local_12;
  undefined1 local_10;
  undefined1 local_f;
  undefined1 local_e;
  undefined1 local_c;
  undefined1 local_b;
  undefined1 local_a;
  short local_8;
  short local_6;
  short local_4;
  short local_2;
  
  iVar2 = 0;
  if (0 < *param_1) {
    psVar1 = param_1 + 0x28;
    do {
      local_8 = psVar1[-2] >> 3;
      local_6 = *psVar1 >> 3;
      local_18 = (undefined1)psVar1[-0x24];
      local_4 = psVar1[2] >> 3;
      local_2 = psVar1[4] >> 3;
      local_14 = (undefined1)psVar1[-0x20];
      local_17 = *(undefined1 *)((int)psVar1 + -0x47);
      local_16 = (undefined1)psVar1[-0x23];
      local_10 = (undefined1)psVar1[-0x1c];
      local_13 = *(undefined1 *)((int)psVar1 + -0x3f);
      local_12 = (undefined1)psVar1[-0x1f];
      local_c = (undefined1)psVar1[-0x18];
      local_f = *(undefined1 *)((int)psVar1 + -0x37);
      local_e = (undefined1)psVar1[-0x1b];
      local_b = *(undefined1 *)((int)psVar1 + -0x2f);
      local_a = (undefined1)psVar1[-0x17];
      FUN_00a0b000(local_1c,param_2,0);
      psVar1 = psVar1 + 0x2c;
      iVar2 = iVar2 + 1;
    } while (iVar2 < *param_1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0ddb0 @ 00a0ddb0 */
/* ===================================================== */

void FUN_00a0ddb0(uint *param_1)

{
                    /* 0xddb0  753   */
  if ((int)*param_1 < 0) {
    DAT_011be7ac = (uint *)(*param_1 & 0x7fffffff);
    *param_1 = *DAT_011be7ac;
    Free(DAT_011be7ac);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0dde0 @ 00a0dde0 */
/* ===================================================== */

void FUN_00a0dde0(uint *param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0xdde0  752   */
  if (-1 < (int)*param_1) {
    FUN_00a0c480((int)(short)*param_1);
    *DAT_011be7ac = *param_1;
    switch(param_3) {
    case 0:
      FUN_00a0d260(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 1:
      FUN_00a0d310(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 2:
      FUN_00a0d3d0(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 3:
      FUN_00a0d4b0(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 4:
      FUN_00a0d5c0(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 5:
      FUN_00a0d630(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 6:
      FUN_00a0d6a0(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 7:
      FUN_00a0d730(param_1);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 8:
      FUN_00a0d7f0(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 9:
      FUN_00a0d8b0(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 10:
      FUN_00a0d990(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 0xb:
      FUN_00a0da80(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 0xc:
      FUN_00a0dba0(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 0xd:
      FUN_00a0dba0(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 0xe:
      FUN_00a0dc30(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
      return;
    case 0xf:
      FUN_00a0dce0(param_1,param_2);
      *param_1 = (uint)DAT_011be7ac | 0x80000000;
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a0e000 @ 00a0e000 */
/* ===================================================== */

void FUN_00a0e000(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  if (-1 < (int)*param_1) {
    FUN_00a0dde0(param_1,param_2,param_3);
  }
  DAT_011be7ac = *param_1 & 0x7fffffff;
  FUN_00a0cd20();
  return;
}



/* ===================================================== */
/* FUN_00a0e040 @ 00a0e040 */
/* ===================================================== */

void FUN_00a0e040(int param_1)

{
  int iVar1;
  int iVar2;
  int iVar3;
  
  iVar3 = 1;
  iVar1 = **(int **)(param_1 + 8);
  if (0 < iVar1) {
    do {
      iVar2 = *(int *)(*(int *)(param_1 + 8) + iVar3 * 4) + *(int *)(param_1 + 0xc);
      FUN_00a0e000(iVar2,*(undefined4 *)(param_1 + 0x10),*(byte *)(iVar2 + 2) & 0xf);
      iVar3 = iVar3 + 1;
    } while (iVar3 <= iVar1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0e0c0 @ 00a0e0c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0e0c0(uint *param_1)

{
  float fVar1;
  float fVar2;
  float fVar3;
  float fVar4;
  float fVar5;
  float fVar6;
  float fVar7;
  uint uVar8;
  int iVar9;
  float fStack_80;
  float fStack_7c;
  undefined4 uStack_70;
  undefined4 uStack_68;
  undefined4 uStack_64;
  float fStack_60;
  float fStack_5c;
  undefined4 uStack_50;
  undefined4 uStack_48;
  undefined4 uStack_44;
  float fStack_40;
  float fStack_3c;
  undefined4 uStack_30;
  undefined4 uStack_28;
  undefined4 uStack_24;
  float fStack_20;
  float fStack_1c;
  undefined4 uStack_10;
  undefined4 uStack_8;
  undefined4 uStack_4;
  
  if ((((short)param_1[2] != 0) && (*(short *)((int)param_1 + 10) != 0)) && (*param_1 < 0x80000000))
  {
    iVar9 = ((int)(short)param_1[4] >> 5) + *(short *)((int)param_1 + 0x12) * 0x40;
    uVar8 = (uint)(ushort)param_1[3];
    if (((uVar8 == DAT_00a2c488) && (iVar9 == DAT_00a2c484)) && (DAT_00a2c48c == 0)) {
      FUN_00a03db0((uVar8 & 0x2000) >> 0xd);
    }
    else {
      FUN_00a27880(uVar8,iVar9,0,0);
    }
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    if ((*param_1 & 0x8000000) == 0) {
      uVar8 = (uint)(ushort)param_1[2];
      fStack_80 = *(float *)(PTR_DAT_00a2c4a0 +
                            ((int)(short)param_1[1] - (int)(short)param_1[6]) * 4) +
                  (float)DAT_011be770;
      fStack_60 = *(float *)(PTR_DAT_00a2c4a4 +
                            ((uVar8 - (int)(short)param_1[6]) + (int)(short)param_1[1]) * 4) +
                  (float)DAT_011be770;
      fStack_7c = *(float *)(PTR_DAT_00a2c4a0 +
                            ((int)*(short *)((int)param_1 + 6) -
                            (int)*(short *)((int)param_1 + 0x1a)) * 4) + (float)DAT_011be774;
      fStack_1c = *(float *)(PTR_DAT_00a2c4a4 +
                            (((int)*(short *)((int)param_1 + 6) -
                             (int)*(short *)((int)param_1 + 0x1a)) +
                            (uint)*(ushort *)((int)param_1 + 10)) * 4) + (float)DAT_011be774;
      fStack_5c = fStack_7c;
      fStack_40 = fStack_60;
      fStack_3c = fStack_1c;
      fStack_20 = fStack_80;
    }
    else {
      iVar9 = FUN_00a25c40((int)param_1[8] / 0x168);
      fVar6 = (float)iVar9 * _DAT_00a2a0b8;
      iVar9 = FUN_00a25c20((int)param_1[8] / 0x168);
      fVar7 = (float)iVar9 * _DAT_00a2a0b8;
      fVar3 = (float)(int)(short)param_1[7] * _DAT_00a2a0b8;
      fVar4 = (float)(int)*(short *)((int)param_1 + 0x1e) * _DAT_00a2a0b8;
      fVar1 = (float)-(int)(short)param_1[6] * fVar3;
      fVar2 = (float)-(int)*(short *)((int)param_1 + 0x1a) * fVar4;
      uVar8 = (uint)(ushort)param_1[2];
      fStack_20 = (float)((short)param_1[1] + DAT_011be770);
      fStack_80 = fVar2 * fVar6 + fVar1 * fVar7 + fStack_20;
      fVar3 = (float)(int)(uVar8 - (int)(short)param_1[6]) * fVar3;
      fVar4 = (float)(int)((uint)*(ushort *)((int)param_1 + 10) -
                          (int)*(short *)((int)param_1 + 0x1a)) * fVar4;
      fStack_60 = fVar2 * fVar6 + fVar3 * fVar7 + fStack_20;
      fStack_40 = fVar4 * fVar6 + fVar3 * fVar7 + fStack_20;
      fVar5 = (float)(*(short *)((int)param_1 + 6) + DAT_011be774);
      fStack_20 = fVar4 * fVar6 + fVar1 * fVar7 + fStack_20;
      fStack_7c = (fVar2 * fVar7 - fVar1 * fVar6) + fVar5;
      fStack_5c = (fVar2 * fVar7 - fVar3 * fVar6) + fVar5;
      fStack_1c = (fVar4 * fVar7 - fVar1 * fVar6) + fVar5;
      fStack_3c = (fVar4 * fVar7 - fVar3 * fVar6) + fVar5;
    }
    if ((*param_1 & 0x400000) == 0) {
      uStack_8 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)((int)param_1 + 0xe) * 4);
      uStack_28 = *(undefined4 *)(DAT_011cb1c0 + (*(byte *)((int)param_1 + 0xe) + uVar8) * 4);
    }
    else {
      uStack_28 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)((int)param_1 + 0xe) * 4);
      uStack_8 = *(undefined4 *)(DAT_011cb1c0 + (*(byte *)((int)param_1 + 0xe) + uVar8) * 4);
    }
    if ((*param_1 & 0x800000) == 0) {
      uStack_44 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)((int)param_1 + 0xf) * 4);
      uStack_4 = *(undefined4 *)(DAT_011cb1c4 + (*(byte *)((int)param_1 + 0xf) + uVar8) * 4);
    }
    else {
      uStack_4 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)((int)param_1 + 0xf) * 4);
      uStack_44 = *(undefined4 *)(DAT_011cb1c4 + (*(byte *)((int)param_1 + 0xf) + uVar8) * 4);
    }
    uStack_10 = 0xffffffff;
    uStack_30 = 0xffffffff;
    uStack_50 = 0xffffffff;
    uStack_70 = 0xffffffff;
    uStack_68 = uStack_8;
    uStack_64 = uStack_44;
    uStack_48 = uStack_28;
    uStack_24 = uStack_4;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&fStack_80,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a0e580 @ 00a0e580 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0e580(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0xc) * 4) + (float)DAT_011be770
  ;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0xe) * 4) + (float)DAT_011be774
  ;
  _DAT_011cb168 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(2,3,&DAT_011cb138,2,0);
  return;
}



/* ===================================================== */
/* FUN_00a0e6a0 @ 00a0e6a0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0e6a0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0xc) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0xe) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb188 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(2,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0e7f0 @ 00a0e7f0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0e7f0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb198 = (float)DAT_011be770;
  _DAT_011cb19c = (float)DAT_011be774;
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb198;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb19c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0xc) * 4) + _DAT_011cb198;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0xe) * 4) + _DAT_011cb19c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb198;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb19c;
  _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011cb198;
  _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011cb19c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(2,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a0e970 @ 00a0e970 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0e970(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 5;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0xc) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0xd) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0xe) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + (float)DAT_011be770;
  _DAT_011cb15c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + (float)DAT_011be774;
  (**(code **)(*DAT_011be760 + 0xbc))(2,3,&DAT_011cb138,2,0);
  return;
}



/* ===================================================== */
/* FUN_00a0eaf0 @ 00a0eaf0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0eaf0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 5;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0xc) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0xd) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0xe) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0x14) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x15) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x16) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb188 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb17c;
  (**(code **)(*DAT_011be760 + 0xbc))(2,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0ecf0 @ 00a0ecf0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0ecf0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 5;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0xc) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0xd) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0xe) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0x14) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x15) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x16) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb188 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0x1c) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x1d) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x1e) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb1a8 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb17c;
  (**(code **)(*DAT_011be760 + 0xbc))(2,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0ef40 @ 00a0ef40 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0ef40(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0xc) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0xe) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb188 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0f090 @ 00a0f090 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0f090(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x16);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
  _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
  _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
  _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb17c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb188 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0f260 @ 00a0f260 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0f260(int param_1)

{
  uint uVar1;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x16);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  _DAT_011be828 = (float)DAT_011be770;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
  _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
  _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
  _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be828;
  _DAT_011be82c = (float)DAT_011be774;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be82c;
  _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011be828;
  _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011be82c;
  _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011be828;
  _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011be82c;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011be7e8,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0f3f0 @ 00a0f3f0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0f3f0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb198 = (float)DAT_011be770;
  _DAT_011cb19c = (float)DAT_011be774;
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb198;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb19c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0xc) * 4) + _DAT_011cb198;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0xe) * 4) + _DAT_011cb19c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011cb198;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011cb19c;
  _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb198;
  _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb19c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a0f570 @ 00a0f570 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0f570(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x16);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb198 = (float)DAT_011be770;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
  _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
  _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
  _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
  _DAT_011cb1b0 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
  _DAT_011cb1b4 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb198;
  _DAT_011cb19c = (float)DAT_011be774;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb19c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb198;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb19c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011cb198;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011cb19c;
  _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb198;
  _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb19c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a0f780 @ 00a0f780 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0f780(int param_1)

{
  uint uVar1;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x16);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  _DAT_011be848 = (float)DAT_011be770;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
  _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
  _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
  _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
  _DAT_011be860 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
  _DAT_011be864 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be848;
  _DAT_011be84c = (float)DAT_011be774;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be84c;
  _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011be848;
  _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011be84c;
  _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011be848;
  _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011be84c;
  _DAT_011be848 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011be848;
  _DAT_011be84c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011be84c;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a0f940 @ 00a0f940 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0f940(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 5;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0xc) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0xd) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0xe) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0x14) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x15) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x16) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb188 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb17c;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0fb40 @ 00a0fb40 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0fb40(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x1a);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  DAT_00a2c480 = 7;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  uVar1 = (uint)*(byte *)(param_1 + 0x10) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x11) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x12) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x18) * 4);
  _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x19) * 4);
  uVar1 = (uint)*(byte *)(param_1 + 0x1c) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x1d) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x1e) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb188 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb178 = (float)DAT_011be770;
  _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
  _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
  _DAT_011cb17c = (float)DAT_011be774;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011cb178;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011cb17c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011cb178;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011cb17c;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0fdc0 @ 00a0fdc0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0fdc0(int param_1)

{
  uint uVar1;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x1a);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  DAT_00a2c480 = 7;
  _DAT_011be828 = (float)DAT_011be770;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x18) * 4);
  _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x19) * 4);
  _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
  _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be828;
  _DAT_011be82c = (float)DAT_011be774;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be82c;
  _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011be828;
  _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011be82c;
  _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011be828;
  _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011be82c;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011be7e8,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a0ff50 @ 00a0ff50 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a0ff50(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 5;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0xc) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0xd) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0xe) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0x14) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x15) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x16) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb188 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  uVar1 = (uint)*(byte *)(param_1 + 0x1c) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x1d) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x1e) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb198 = (float)DAT_011be770;
  _DAT_011cb19c = (float)DAT_011be774;
  _DAT_011cb1a8 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb198;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb19c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb198;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb19c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011cb198;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011cb19c;
  _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb198;
  _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb19c;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a101c0 @ 00a101c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a101c0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x1a);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  DAT_00a2c480 = 7;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  uVar1 = (uint)*(byte *)(param_1 + 0x10) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x11) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x12) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb168 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x18) * 4);
  _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x19) * 4);
  uVar1 = (uint)*(byte *)(param_1 + 0x28) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x29) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x2a) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb188 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x30) * 4);
  _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x31) * 4);
  uVar1 = (uint)*(byte *)(param_1 + 0x1c) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 0x1d) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 0x1e) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb1a8 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  _DAT_011cb1b0 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
  _DAT_011cb198 = (float)DAT_011be770;
  _DAT_011cb1b4 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb198;
  _DAT_011cb19c = (float)DAT_011be774;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb19c;
  _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011cb198;
  _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011cb19c;
  _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x2c) * 4) + _DAT_011cb198;
  _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x2e) * 4) + _DAT_011cb19c;
  _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011cb198;
  _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011cb19c;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a104c0 @ 00a104c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a104c0(int param_1)

{
  uint uVar1;
  
  uVar1 = (uint)*(ushort *)(param_1 + 0x1a);
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 0)) {
    FUN_00a03db0((uVar1 & 0x2000) >> 0xd);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,0);
  }
  if (((DAT_00a2c480 ^ 1) & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
  }
  DAT_00a2c480 = 7;
  _DAT_011be848 = (float)DAT_011be770;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x18) * 4);
  _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x19) * 4);
  _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x30) * 4);
  _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x31) * 4);
  _DAT_011be860 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
  _DAT_011be864 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be848;
  _DAT_011be84c = (float)DAT_011be774;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be84c;
  _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011be848;
  _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011be84c;
  _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x2c) * 4) + _DAT_011be848;
  _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x2e) * 4) + _DAT_011be84c;
  _DAT_011be848 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011be848;
  _DAT_011be84c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011be84c;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a10690 @ 00a10690 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a10690(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 +
       *(float *)(PTR_DAT_00a2c4a0 +
                 ((int)*(short *)(param_1 + 0xc) + (int)*(short *)(param_1 + 8)) * 4);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 +
                 ((int)*(short *)(param_1 + 0xe) + (int)*(short *)(param_1 + 10)) * 4) +
       (float)DAT_011be774;
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb198 = DAT_011cb138;
  _DAT_011cb19c = _DAT_011cb17c;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a107e0 @ 00a107e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a107e0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = DAT_011be6ec & 0xffff;
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 1)) {
    FUN_00a03db0(uVar1 >> 0xd & 1);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,1);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 +
       *(float *)(PTR_DAT_00a2c4a0 +
                 ((int)*(short *)(param_1 + 8) + (int)*(short *)(param_1 + 0x10)) * 4);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 +
                 ((int)*(short *)(param_1 + 0x12) + (int)*(short *)(param_1 + 10)) * 4) +
       (float)DAT_011be774;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb170 =
       *(undefined4 *)
        (DAT_011cb1c0 + ((uint)*(byte *)(param_1 + 0xc) + (int)*(short *)(param_1 + 0x10)) * 4);
  _DAT_011cb194 =
       *(undefined4 *)
        (DAT_011cb1c4 + ((int)*(short *)(param_1 + 0x12) + (uint)*(byte *)(param_1 + 0xd)) * 4);
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb174 = _DAT_011cb154;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb190 = _DAT_011cb170;
  _DAT_011cb198 = DAT_011cb138;
  _DAT_011cb19c = _DAT_011cb17c;
  _DAT_011cb1a8 = _DAT_011cb148;
  _DAT_011cb1b0 = _DAT_011cb150;
  _DAT_011cb1b4 = _DAT_011cb194;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a109c0 @ 00a109c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a109c0(int param_1)

{
  uint uVar1;
  
  uVar1 = DAT_011be6ec & 0xffff;
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 1)) {
    FUN_00a03db0(uVar1 >> 0xd & 1);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,1);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011be808 =
       (float)DAT_011be770 +
       *(float *)(PTR_DAT_00a2c4a0 +
                 ((int)*(short *)(param_1 + 0x10) + (int)*(short *)(param_1 + 8)) * 4);
  _DAT_011be82c =
       *(float *)(PTR_DAT_00a2c4a4 +
                 ((int)*(short *)(param_1 + 0x12) + (int)*(short *)(param_1 + 10)) * 4) +
       (float)DAT_011be774;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 =
       *(undefined4 *)
        (DAT_011cb1c0 + ((int)*(short *)(param_1 + 0x10) + (uint)*(byte *)(param_1 + 0xc)) * 4);
  _DAT_011be844 =
       *(undefined4 *)
        (DAT_011cb1c4 + ((uint)*(byte *)(param_1 + 0xd) + (int)*(short *)(param_1 + 0x12)) * 4);
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  _DAT_011be80c = DAT_011be7ec;
  _DAT_011be824 = _DAT_011be804;
  _DAT_011be828 = _DAT_011be808;
  _DAT_011be840 = _DAT_011be820;
  _DAT_011be848 = DAT_011be7e8;
  _DAT_011be84c = _DAT_011be82c;
  _DAT_011be860 = _DAT_011be800;
  _DAT_011be864 = _DAT_011be844;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a10b60 @ 00a10b60 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a10b60(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 4);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 4) + (float)DAT_011be774;
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  return;
}



/* ===================================================== */
/* FUN_00a10ca0 @ 00a10ca0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a10ca0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x20);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x20) + (float)DAT_011be774;
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb198 = DAT_011cb138;
  _DAT_011cb19c = _DAT_011cb17c;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a10df0 @ 00a10df0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a10df0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = DAT_011be6ec & 0xffff;
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 1)) {
    FUN_00a03db0(uVar1 >> 0xd & 1);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,1);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x20);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x20) + (float)DAT_011be774;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c0 + 0x20 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb194 = *(undefined4 *)(DAT_011cb1c4 + 0x20 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb174 = _DAT_011cb154;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb190 = _DAT_011cb170;
  _DAT_011cb198 = DAT_011cb138;
  _DAT_011cb19c = _DAT_011cb17c;
  _DAT_011cb1a8 = _DAT_011cb148;
  _DAT_011cb1b0 = _DAT_011cb150;
  _DAT_011cb1b4 = _DAT_011cb194;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a11000 @ 00a11000 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a11000(int param_1)

{
  uint uVar1;
  
  uVar1 = DAT_011be6ec & 0xffff;
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 1)) {
    FUN_00a03db0(uVar1 >> 0xd & 1);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,1);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011be808 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x20);
  _DAT_011be82c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x20) + (float)DAT_011be774;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 = *(undefined4 *)(DAT_011cb1c0 + 0x20 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be844 = *(undefined4 *)(DAT_011cb1c4 + 0x20 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be80c = DAT_011be7ec;
  _DAT_011be824 = _DAT_011be804;
  _DAT_011be828 = _DAT_011be808;
  _DAT_011be840 = _DAT_011be820;
  _DAT_011be848 = DAT_011be7e8;
  _DAT_011be84c = _DAT_011be82c;
  _DAT_011be860 = _DAT_011be800;
  _DAT_011be864 = _DAT_011be844;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a111c0 @ 00a111c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a111c0(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  if (DAT_00a2c488 != -1) {
    (**(code **)(*DAT_011be760 + 100))(0);
    DAT_00a2c488 = -1;
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  FUN_00a03db0(1);
  DAT_00a2c480 = 4;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x40);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x40) + (float)DAT_011be774;
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb198 = DAT_011cb138;
  _DAT_011cb19c = _DAT_011cb17c;
  _DAT_011cb1a8 = _DAT_011cb148;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a11310 @ 00a11310 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a11310(int param_1)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  
  uVar1 = DAT_011be6ec & 0xffff;
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 1)) {
    FUN_00a03db0(uVar1 >> 0xd & 1);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,1);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  uVar1 = (uint)*(byte *)(param_1 + 4) * 2;
  if (0xfe < uVar1) {
    uVar1 = 0xff;
  }
  uVar2 = (uint)*(byte *)(param_1 + 5) * 2;
  if (0xfe < uVar2) {
    uVar2 = 0xff;
  }
  uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
  if (0xfe < uVar3) {
    uVar3 = 0xff;
  }
  _DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;
  DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011cb158 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x40);
  _DAT_011cb17c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x40) + (float)DAT_011be774;
  _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb154 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c0 + 0x40 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011cb194 = *(undefined4 *)(DAT_011cb1c4 + 0x40 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011cb15c = _DAT_011cb13c;
  _DAT_011cb168 = _DAT_011cb148;
  _DAT_011cb174 = _DAT_011cb154;
  _DAT_011cb178 = _DAT_011cb158;
  _DAT_011cb188 = _DAT_011cb148;
  _DAT_011cb190 = _DAT_011cb170;
  _DAT_011cb198 = DAT_011cb138;
  _DAT_011cb19c = _DAT_011cb17c;
  _DAT_011cb1a8 = _DAT_011cb148;
  _DAT_011cb1b0 = _DAT_011cb150;
  _DAT_011cb1b4 = _DAT_011cb194;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a11520 @ 00a11520 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a11520(int param_1)

{
  uint uVar1;
  
  uVar1 = DAT_011be6ec & 0xffff;
  if (((uVar1 == DAT_00a2c488) && (*(ushort *)(param_1 + 0xe) == DAT_00a2c484)) &&
     (DAT_00a2c48c == 1)) {
    FUN_00a03db0(uVar1 >> 0xd & 1);
  }
  else {
    FUN_00a27880(uVar1,(uint)*(ushort *)(param_1 + 0xe),0,1);
  }
  if ((DAT_00a2c480 & 9) != 0) {
    (**(code **)(*DAT_011be760 + 0x8c))(1);
  }
  DAT_00a2c480 = 6;
  _DAT_011be858 = 0xffffffff;
  _DAT_011be838 = 0xffffffff;
  DAT_011be818 = 0xffffffff;
  DAT_011be7f8 = 0xffffffff;
  DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
  DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
  _DAT_011be808 =
       (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x40);
  _DAT_011be82c =
       *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x40) + (float)DAT_011be774;
  _DAT_011be800 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be804 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be820 = *(undefined4 *)(DAT_011cb1c0 + 0x40 + (uint)*(byte *)(param_1 + 0xc) * 4);
  _DAT_011be844 = *(undefined4 *)(DAT_011cb1c4 + 0x40 + (uint)*(byte *)(param_1 + 0xd) * 4);
  _DAT_011be80c = DAT_011be7ec;
  _DAT_011be824 = _DAT_011be804;
  _DAT_011be828 = _DAT_011be808;
  _DAT_011be840 = _DAT_011be820;
  _DAT_011be848 = DAT_011be7e8;
  _DAT_011be84c = _DAT_011be82c;
  _DAT_011be860 = _DAT_011be800;
  _DAT_011be864 = _DAT_011be844;
  (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  return;
}



/* ===================================================== */
/* FUN_00a116e0 @ 00a116e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a116e0(int param_1)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  uint uVar4;
  
  uVar3 = (uint)*(ushort *)(param_1 + 0x16);
  uVar2 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x16) & 0x60) == 0x40) {
    uVar2 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar2 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar1 = FUN_00a27880(uVar3,uVar2,1,0), iVar1 == 0)) {
    return;
  }
  iVar1 = FUN_00a03c80(uVar3);
  if (iVar1 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    uVar2 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar2) {
      uVar2 = 0xff;
    }
    uVar3 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb178 = (float)DAT_011be770;
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x16) & 0x60) >> 5] * 0x100 + uVar2) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
    _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
    _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
    _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb178;
    _DAT_011cb17c = (float)DAT_011be774;
    _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb17c;
    _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb178;
    _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb17c;
    _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb178;
    _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb17c;
    _DAT_011cb168 = _DAT_011cb148;
    _DAT_011cb188 = _DAT_011cb148;
    (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a118e0 @ 00a118e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a118e0(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = (uint)*(ushort *)(param_1 + 0x16);
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x16) & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,0), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    _DAT_011be828 = (float)DAT_011be770;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
    _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
    _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
    _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x16) & 0x60) >> 5] * 0x1000000
                   + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be828;
    _DAT_011be82c = (float)DAT_011be774;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be82c;
    _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011be828;
    _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011be82c;
    _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011be828;
    _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011be82c;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be858 = DAT_011be7f8;
    (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011be7e8,3,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a11a90 @ 00a11a90 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a11a90(int param_1)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  uint uVar4;
  
  uVar3 = (uint)*(ushort *)(param_1 + 0x16);
  uVar2 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x16) & 0x60) == 0x40) {
    uVar2 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar2 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar1 = FUN_00a27880(uVar3,uVar2,1,0), iVar1 == 0)) {
    return;
  }
  iVar1 = FUN_00a03c80(uVar3);
  if (iVar1 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    uVar2 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar2) {
      uVar2 = 0xff;
    }
    uVar3 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb198 = (float)DAT_011be770;
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x16) & 0x60) >> 5] * 0x100 + uVar2) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
    _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
    _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
    _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
    _DAT_011cb1b0 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
    _DAT_011cb1b4 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011cb198;
    _DAT_011cb19c = (float)DAT_011be774;
    _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011cb19c;
    _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011cb198;
    _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011cb19c;
    _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011cb198;
    _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011cb19c;
    _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011cb198;
    _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011cb19c;
    _DAT_011cb168 = _DAT_011cb148;
    _DAT_011cb188 = _DAT_011cb148;
    _DAT_011cb1a8 = _DAT_011cb148;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a11cd0 @ 00a11cd0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a11cd0(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = (uint)*(ushort *)(param_1 + 0x16);
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x16) & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,0), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    _DAT_011be848 = (float)DAT_011be770;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x14) * 4);
    _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x15) * 4);
    _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
    _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
    _DAT_011be860 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x1c) * 4);
    _DAT_011be864 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x1d) * 4);
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x16) & 0x60) >> 5] * 0x1000000
                   + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be848;
    _DAT_011be84c = (float)DAT_011be774;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be84c;
    _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x10) * 4) + _DAT_011be848;
    _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x12) * 4) + _DAT_011be84c;
    _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011be848;
    _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011be84c;
    _DAT_011be848 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x18) * 4) + _DAT_011be848;
    _DAT_011be84c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x1a) * 4) + _DAT_011be84c;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be858 = DAT_011be7f8;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a11ec0 @ 00a11ec0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a11ec0(uint param_1)

{
  int iVar1;
  int iVar2;
  uint uVar3;
  uint uVar4;
  uint uVar5;
  
  iVar1 = param_1;
  uVar4 = (uint)*(ushort *)(param_1 + 0x1a);
  uVar3 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x1a) & 0x60) == 0x40) {
    uVar3 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar4 != DAT_00a2c488) || (uVar3 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar2 = FUN_00a27880(uVar4,uVar3,1,0), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar4);
  if (iVar2 != 0) {
    if (((DAT_00a2c480 ^ 1) & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
    }
    DAT_00a2c480 = 7;
    uVar3 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    uVar5 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar5) {
      uVar5 = 0xff;
    }
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x1a) & 0x60) >> 5] * 0x100 + uVar3) *
          0x100 + uVar4) * 0x100 + uVar5;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    param_1 = (uint)*(byte *)(param_1 + 0x10) * 2;
    if (0xfe < param_1) {
      param_1 = 0xff;
    }
    uVar3 = (uint)*(byte *)(iVar1 + 0x11) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(iVar1 + 0x12) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb168 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(iVar1 + 0x1a) & 0x60) >> 5] * 0x100 + param_1) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(iVar1 + 0x18) * 4);
    _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(iVar1 + 0x19) * 4);
    param_1 = (uint)*(byte *)(iVar1 + 0x1c) * 2;
    if (0xfe < param_1) {
      param_1 = 0xff;
    }
    uVar3 = (uint)*(byte *)(iVar1 + 0x1d) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(iVar1 + 0x1e) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb178 = (float)DAT_011be770;
    _DAT_011cb188 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(iVar1 + 0x1a) & 0x60) >> 5] * 0x100 + param_1) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(iVar1 + 0x24) * 4);
    _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(iVar1 + 0x25) * 4);
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 8) * 4) + _DAT_011cb178;
    _DAT_011cb17c = (float)DAT_011be774;
    _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 10) * 4) + _DAT_011cb17c;
    _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 0x14) * 4) + _DAT_011cb178;
    _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 0x16) * 4) + _DAT_011cb17c;
    _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 0x20) * 4) + _DAT_011cb178;
    _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 0x22) * 4) + _DAT_011cb17c;
    (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011cb138,3,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a12190 @ 00a12190 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a12190(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = (uint)*(ushort *)(param_1 + 0x1a);
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x1a) & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,0), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if (((DAT_00a2c480 ^ 1) & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
    }
    DAT_00a2c480 = 7;
    _DAT_011be828 = (float)DAT_011be770;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x18) * 4);
    _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x19) * 4);
    _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
    _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x1a) & 0x60) >> 5] * 0x1000000
                   + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be828;
    _DAT_011be82c = (float)DAT_011be774;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be82c;
    _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011be828;
    _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011be82c;
    _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011be828;
    _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011be82c;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be858 = DAT_011be7f8;
    (**(code **)(*DAT_011be760 + 0xbc))(4,3,&DAT_011be7e8,3,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a12350 @ 00a12350 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a12350(uint param_1)

{
  int iVar1;
  int iVar2;
  uint uVar3;
  uint uVar4;
  uint uVar5;
  
  iVar1 = param_1;
  uVar4 = (uint)*(ushort *)(param_1 + 0x1a);
  uVar3 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x1a) & 0x60) == 0x40) {
    uVar3 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar4 != DAT_00a2c488) || (uVar3 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar2 = FUN_00a27880(uVar4,uVar3,1,0), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar4);
  if (iVar2 != 0) {
    if (((DAT_00a2c480 ^ 1) & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
    }
    DAT_00a2c480 = 7;
    uVar3 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    uVar5 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar5) {
      uVar5 = 0xff;
    }
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x1a) & 0x60) >> 5] * 0x100 + uVar3) *
          0x100 + uVar4) * 0x100 + uVar5;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    param_1 = (uint)*(byte *)(param_1 + 0x10) * 2;
    if (0xfe < param_1) {
      param_1 = 0xff;
    }
    uVar3 = (uint)*(byte *)(iVar1 + 0x11) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(iVar1 + 0x12) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb168 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(iVar1 + 0x1a) & 0x60) >> 5] * 0x100 + param_1) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(iVar1 + 0x18) * 4);
    _DAT_011cb174 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(iVar1 + 0x19) * 4);
    param_1 = (uint)*(byte *)(iVar1 + 0x28) * 2;
    if (0xfe < param_1) {
      param_1 = 0xff;
    }
    uVar3 = (uint)*(byte *)(iVar1 + 0x29) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(iVar1 + 0x2a) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb188 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(iVar1 + 0x1a) & 0x60) >> 5] * 0x100 + param_1) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb190 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(iVar1 + 0x30) * 4);
    _DAT_011cb194 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(iVar1 + 0x31) * 4);
    param_1 = (uint)*(byte *)(iVar1 + 0x1c) * 2;
    if (0xfe < param_1) {
      param_1 = 0xff;
    }
    uVar3 = (uint)*(byte *)(iVar1 + 0x1d) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    uVar4 = (uint)*(byte *)(iVar1 + 0x1e) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    _DAT_011cb198 = (float)DAT_011be770;
    _DAT_011cb1a8 =
         (((uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(iVar1 + 0x1a) & 0x60) >> 5] * 0x100 + param_1) *
          0x100 + uVar3) * 0x100 + uVar4;
    _DAT_011cb1b0 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(iVar1 + 0x24) * 4);
    _DAT_011cb1b4 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(iVar1 + 0x25) * 4);
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 8) * 4) + _DAT_011cb198;
    _DAT_011cb19c = (float)DAT_011be774;
    _DAT_011cb13c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 10) * 4) + _DAT_011cb19c;
    _DAT_011cb158 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 0x14) * 4) + _DAT_011cb198;
    _DAT_011cb15c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 0x16) * 4) + _DAT_011cb19c;
    _DAT_011cb178 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 0x2c) * 4) + _DAT_011cb198;
    _DAT_011cb17c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 0x2e) * 4) + _DAT_011cb19c;
    _DAT_011cb198 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(iVar1 + 0x20) * 4) + _DAT_011cb198;
    _DAT_011cb19c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(iVar1 + 0x22) * 4) + _DAT_011cb19c;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a126d0 @ 00a126d0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a126d0(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = (uint)*(ushort *)(param_1 + 0x1a);
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)*(ushort *)(param_1 + 0x1a) & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 0)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,0), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if (((DAT_00a2c480 ^ 1) & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(DAT_00a2c5a4);
    }
    DAT_00a2c480 = 7;
    _DAT_011be848 = (float)DAT_011be770;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x18) * 4);
    _DAT_011be824 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x19) * 4);
    _DAT_011be840 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x30) * 4);
    _DAT_011be844 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x31) * 4);
    _DAT_011be860 = *(undefined4 *)(DAT_011cb1c8 + (uint)*(byte *)(param_1 + 0x24) * 4);
    _DAT_011be864 = *(undefined4 *)(DAT_011cb1cc + (uint)*(byte *)(param_1 + 0x25) * 4);
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[(*(byte *)(param_1 + 0x1a) & 0x60) >> 5] * 0x1000000
                   + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + _DAT_011be848;
    _DAT_011be84c = (float)DAT_011be774;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + _DAT_011be84c;
    _DAT_011be808 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x14) * 4) + _DAT_011be848;
    _DAT_011be80c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x16) * 4) + _DAT_011be84c;
    _DAT_011be828 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x2c) * 4) + _DAT_011be848;
    _DAT_011be82c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x2e) * 4) + _DAT_011be84c;
    _DAT_011be848 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 0x20) * 4) + _DAT_011be848;
    _DAT_011be84c = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 0x22) * 4) + _DAT_011be84c;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be858 = DAT_011be7f8;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a128d0 @ 00a128d0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a128d0(int param_1)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  uint uVar4;
  
  uVar2 = (uint)*(ushort *)(param_1 + 0xe);
  uVar4 = DAT_011be6ec & 0xffff;
  if (((byte)uVar4 & 0x60) == 0x40) {
    uVar2 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar4 != DAT_00a2c488) || (uVar2 != DAT_00a2c484)) || (DAT_00a2c48c != 1)) &&
     (iVar1 = FUN_00a27880(uVar4,uVar2,1,1), iVar1 == 0)) {
    return;
  }
  iVar1 = FUN_00a03c80(uVar4);
  if (iVar1 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    uVar2 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar2) {
      uVar2 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[DAT_011be6ec >> 5 & 3] * 0x100 + uVar2) * 0x100 + uVar4) *
         0x100 + uVar3;
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
    _DAT_011cb13c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
    _DAT_011cb158 =
         (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x20);
    _DAT_011cb17c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x20) + (float)DAT_011be774;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c0 + 0x20 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb194 = *(undefined4 *)(DAT_011cb1c4 + 0x20 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb15c = _DAT_011cb13c;
    _DAT_011cb168 = _DAT_011cb148;
    _DAT_011cb174 = _DAT_011cb154;
    _DAT_011cb178 = _DAT_011cb158;
    _DAT_011cb188 = _DAT_011cb148;
    _DAT_011cb190 = _DAT_011cb170;
    _DAT_011cb198 = DAT_011cb138;
    _DAT_011cb19c = _DAT_011cb17c;
    _DAT_011cb1a8 = _DAT_011cb148;
    _DAT_011cb1b0 = _DAT_011cb150;
    _DAT_011cb1b4 = _DAT_011cb194;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a12ac0 @ 00a12ac0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a12ac0(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = DAT_011be6ec & 0xffff;
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)uVar3 & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 1)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,1), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[DAT_011be6ec >> 5 & 3] * 0x1000000 + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774
    ;
    _DAT_011be808 =
         (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x20);
    _DAT_011be82c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x20) + (float)DAT_011be774;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 = *(undefined4 *)(DAT_011cb1c0 + 0x20 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be844 = *(undefined4 *)(DAT_011cb1c4 + 0x20 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be80c = DAT_011be7ec;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be824 = _DAT_011be804;
    _DAT_011be828 = _DAT_011be808;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be840 = _DAT_011be820;
    _DAT_011be848 = DAT_011be7e8;
    _DAT_011be84c = _DAT_011be82c;
    _DAT_011be858 = DAT_011be7f8;
    _DAT_011be860 = _DAT_011be800;
    _DAT_011be864 = _DAT_011be844;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a12c70 @ 00a12c70 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a12c70(int param_1)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  uint uVar4;
  
  uVar2 = (uint)*(ushort *)(param_1 + 0xe);
  uVar4 = DAT_011be6ec & 0xffff;
  if (((byte)uVar4 & 0x60) == 0x40) {
    uVar2 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar4 != DAT_00a2c488) || (uVar2 != DAT_00a2c484)) || (DAT_00a2c48c != 1)) &&
     (iVar1 = FUN_00a27880(uVar4,uVar2,1,1), iVar1 == 0)) {
    return;
  }
  iVar1 = FUN_00a03c80(uVar4);
  if (iVar1 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    uVar2 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar2) {
      uVar2 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[DAT_011be6ec >> 5 & 3] * 0x100 + uVar2) * 0x100 + uVar4) *
         0x100 + uVar3;
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
    _DAT_011cb13c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
    _DAT_011cb158 =
         (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x40);
    _DAT_011cb17c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x40) + (float)DAT_011be774;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb170 = *(undefined4 *)(DAT_011cb1c0 + 0x40 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb194 = *(undefined4 *)(DAT_011cb1c4 + 0x40 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb15c = _DAT_011cb13c;
    _DAT_011cb168 = _DAT_011cb148;
    _DAT_011cb174 = _DAT_011cb154;
    _DAT_011cb178 = _DAT_011cb158;
    _DAT_011cb188 = _DAT_011cb148;
    _DAT_011cb190 = _DAT_011cb170;
    _DAT_011cb198 = DAT_011cb138;
    _DAT_011cb19c = _DAT_011cb17c;
    _DAT_011cb1a8 = _DAT_011cb148;
    _DAT_011cb1b0 = _DAT_011cb150;
    _DAT_011cb1b4 = _DAT_011cb194;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a12e60 @ 00a12e60 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a12e60(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = DAT_011be6ec & 0xffff;
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)uVar3 & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 1)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,1), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[DAT_011be6ec >> 5 & 3] * 0x1000000 + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774
    ;
    _DAT_011be808 =
         (float)DAT_011be770 + *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4 + 0x40);
    _DAT_011be82c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4 + 0x40) + (float)DAT_011be774;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 = *(undefined4 *)(DAT_011cb1c0 + 0x40 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be844 = *(undefined4 *)(DAT_011cb1c4 + 0x40 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be80c = DAT_011be7ec;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be824 = _DAT_011be804;
    _DAT_011be828 = _DAT_011be808;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be840 = _DAT_011be820;
    _DAT_011be848 = DAT_011be7e8;
    _DAT_011be84c = _DAT_011be82c;
    _DAT_011be858 = DAT_011be7f8;
    _DAT_011be860 = _DAT_011be800;
    _DAT_011be864 = _DAT_011be844;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a13010 @ 00a13010 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a13010(int param_1)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  uint uVar4;
  
  uVar2 = (uint)*(ushort *)(param_1 + 0xe);
  uVar4 = DAT_011be6ec & 0xffff;
  if (((byte)uVar4 & 0x60) == 0x40) {
    uVar2 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar4 != DAT_00a2c488) || (uVar2 != DAT_00a2c484)) || (DAT_00a2c48c != 1)) &&
     (iVar1 = FUN_00a27880(uVar4,uVar2,1,1), iVar1 == 0)) {
    return;
  }
  iVar1 = FUN_00a03c80(uVar4);
  if (iVar1 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    uVar2 = (uint)*(byte *)(param_1 + 4) * 2;
    if (0xfe < uVar2) {
      uVar2 = 0xff;
    }
    uVar4 = (uint)*(byte *)(param_1 + 5) * 2;
    if (0xfe < uVar4) {
      uVar4 = 0xff;
    }
    uVar3 = (uint)*(byte *)(param_1 + 6) * 2;
    if (0xfe < uVar3) {
      uVar3 = 0xff;
    }
    _DAT_011cb148 =
         (((uint)(byte)(&DAT_00a2c5a8)[DAT_011be6ec >> 5 & 3] * 0x100 + uVar2) * 0x100 + uVar4) *
         0x100 + uVar3;
    DAT_011cb138 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
    _DAT_011cb13c =
         *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774;
    _DAT_011cb158 =
         (float)DAT_011be770 +
         *(float *)(PTR_DAT_00a2c4a0 +
                   ((int)*(short *)(param_1 + 0x10) + (int)*(short *)(param_1 + 8)) * 4);
    _DAT_011cb17c =
         *(float *)(PTR_DAT_00a2c4a4 +
                   ((int)*(short *)(param_1 + 0x12) + (int)*(short *)(param_1 + 10)) * 4) +
         (float)DAT_011be774;
    _DAT_011cb150 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011cb154 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011cb170 =
         *(undefined4 *)
          (DAT_011cb1c0 + ((int)*(short *)(param_1 + 0x10) + (uint)*(byte *)(param_1 + 0xc)) * 4);
    _DAT_011cb194 =
         *(undefined4 *)
          (DAT_011cb1c4 + ((uint)*(byte *)(param_1 + 0xd) + (int)*(short *)(param_1 + 0x12)) * 4);
    _DAT_011cb15c = _DAT_011cb13c;
    _DAT_011cb168 = _DAT_011cb148;
    _DAT_011cb174 = _DAT_011cb154;
    _DAT_011cb178 = _DAT_011cb158;
    _DAT_011cb188 = _DAT_011cb148;
    _DAT_011cb190 = _DAT_011cb170;
    _DAT_011cb198 = DAT_011cb138;
    _DAT_011cb19c = _DAT_011cb17c;
    _DAT_011cb1a8 = _DAT_011cb148;
    _DAT_011cb1b0 = _DAT_011cb150;
    _DAT_011cb1b4 = _DAT_011cb194;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011cb138,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a13220 @ 00a13220 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a13220(int param_1)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  
  uVar3 = DAT_011be6ec & 0xffff;
  uVar1 = (uint)*(ushort *)(param_1 + 0xe);
  if (((byte)uVar3 & 0x60) == 0x40) {
    uVar1 = *(ushort *)(param_1 + 0xe) | 0x8000;
  }
  if ((((uVar3 != DAT_00a2c488) || (uVar1 != DAT_00a2c484)) || (DAT_00a2c48c != 1)) &&
     (iVar2 = FUN_00a27880(uVar3,uVar1,1,1), iVar2 == 0)) {
    return;
  }
  iVar2 = FUN_00a03c80(uVar3);
  if (iVar2 != 0) {
    if ((DAT_00a2c480 & 9) != 0) {
      (**(code **)(*DAT_011be760 + 0x8c))(1);
    }
    DAT_00a2c480 = 6;
    DAT_011be7f8 = (uint)(byte)(&DAT_00a2c5a8)[DAT_011be6ec >> 5 & 3] * 0x1000000 + 0xffffff;
    DAT_011be7e8 = *(float *)(PTR_DAT_00a2c4a0 + *(short *)(param_1 + 8) * 4) + (float)DAT_011be770;
    DAT_011be7ec = *(float *)(PTR_DAT_00a2c4a4 + *(short *)(param_1 + 10) * 4) + (float)DAT_011be774
    ;
    _DAT_011be808 =
         (float)DAT_011be770 +
         *(float *)(PTR_DAT_00a2c4a0 +
                   ((int)*(short *)(param_1 + 0x10) + (int)*(short *)(param_1 + 8)) * 4);
    _DAT_011be82c =
         *(float *)(PTR_DAT_00a2c4a4 +
                   ((int)*(short *)(param_1 + 10) + (int)*(short *)(param_1 + 0x12)) * 4) +
         (float)DAT_011be774;
    _DAT_011be800 = *(undefined4 *)(DAT_011cb1c0 + (uint)*(byte *)(param_1 + 0xc) * 4);
    _DAT_011be804 = *(undefined4 *)(DAT_011cb1c4 + (uint)*(byte *)(param_1 + 0xd) * 4);
    _DAT_011be820 =
         *(undefined4 *)
          (DAT_011cb1c0 + ((uint)*(byte *)(param_1 + 0xc) + (int)*(short *)(param_1 + 0x10)) * 4);
    _DAT_011be844 =
         *(undefined4 *)
          (DAT_011cb1c4 + ((uint)*(byte *)(param_1 + 0xd) + (int)*(short *)(param_1 + 0x12)) * 4);
    _DAT_011be80c = DAT_011be7ec;
    DAT_011be818 = DAT_011be7f8;
    _DAT_011be824 = _DAT_011be804;
    _DAT_011be828 = _DAT_011be808;
    _DAT_011be838 = DAT_011be7f8;
    _DAT_011be840 = _DAT_011be820;
    _DAT_011be848 = DAT_011be7e8;
    _DAT_011be84c = _DAT_011be82c;
    _DAT_011be858 = DAT_011be7f8;
    _DAT_011be860 = _DAT_011be800;
    _DAT_011be864 = _DAT_011be844;
    (**(code **)(*DAT_011be760 + 0xbc))(6,3,&DAT_011be7e8,4,0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a133e0 @ 00a133e0 */
/* ===================================================== */

void FUN_00a133e0(uint *param_1)

{
  DAT_011be7ac = *param_1 & 0x7fffffff;
  FUN_00a0cd20();
  return;
}



/* ===================================================== */
/* FUN_00a13400 @ 00a13400 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a13400(uint *param_1)

{
  int iVar1;
  short sVar2;
  short sVar3;
  short sVar4;
  short sVar5;
  short sVar6;
  uint uVar7;
  uint uVar8;
  int iVar9;
  uint uVar10;
  short sVar11;
  uint uVar12;
  int local_50;
  float local_40;
  float local_3c;
  float local_38;
  undefined4 local_34;
  float local_30;
  float local_2c;
  float local_28;
  undefined4 local_24;
  float local_20;
  float local_1c;
  float local_18;
  undefined4 local_14;
  float local_10;
  float local_c;
  float local_8;
  undefined4 local_4;
  
  uVar12 = *param_1 >> 0x18;
  do {
    uVar7 = DAT_011cb1dc;
    uVar10 = (uint)*(byte *)((int)param_1 + 7);
    uVar8 = uVar10;
    if (*(byte *)((int)param_1 + 7) < 0x80) {
      uVar8 = uVar10 | Elo_NoLighting;
    }
    switch(uVar8) {
    default:
      goto switchD_00a13437_caseD_4;
    case 0x20:
    case 0x21:
    case 0x22:
    case 0x23:
      FUN_00a0ef40(param_1);
      param_1 = param_1 + 4;
      uVar12 = uVar12 - 4;
      break;
    case 0x24:
      FUN_00a0f090(param_1);
      param_1 = param_1 + 7;
      uVar12 = uVar12 - 7;
      break;
    case 0x25:
      FUN_00a0f260(param_1);
      param_1 = param_1 + 7;
      uVar12 = uVar12 - 7;
      break;
    case 0x26:
      FUN_00a116e0(param_1);
      param_1 = param_1 + 7;
      uVar12 = uVar12 - 7;
      break;
    case 0x27:
      FUN_00a118e0(param_1);
      param_1 = param_1 + 7;
      uVar12 = uVar12 - 7;
      break;
    case 0x28:
    case 0x29:
    case 0x2a:
    case 0x2b:
      FUN_00a0f3f0(param_1);
      param_1 = param_1 + 5;
      uVar12 = uVar12 - 5;
      break;
    case 0x2c:
      FUN_00a0f570(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x2d:
      FUN_00a0f780(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x2e:
      FUN_00a11a90(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x2f:
      FUN_00a11cd0(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x30:
    case 0x31:
    case 0x32:
    case 0x33:
      FUN_00a0f940(param_1);
      param_1 = param_1 + 6;
      uVar12 = uVar12 - 6;
      break;
    case 0x34:
      FUN_00a0fb40(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x35:
      FUN_00a0fdc0(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x36:
      FUN_00a11ec0(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x37:
      FUN_00a12190(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x38:
    case 0x39:
    case 0x3a:
    case 0x3b:
      FUN_00a0ff50(param_1);
      param_1 = param_1 + 8;
      uVar12 = uVar12 - 8;
      break;
    case 0x3c:
      FUN_00a101c0(param_1);
      param_1 = param_1 + 0xc;
      uVar12 = uVar12 - 0xc;
      break;
    case 0x3d:
      FUN_00a104c0(param_1);
      param_1 = param_1 + 0xc;
      uVar12 = uVar12 - 0xc;
      break;
    case 0x3e:
      FUN_00a12350(param_1);
      param_1 = param_1 + 0xc;
      uVar12 = uVar12 - 0xc;
      break;
    case 0x3f:
      FUN_00a126d0(param_1);
      param_1 = param_1 + 0xc;
      uVar12 = uVar12 - 0xc;
      break;
    case 0x40:
    case 0x41:
    case 0x42:
    case 0x43:
    case 0x44:
    case 0x45:
    case 0x46:
    case 0x47:
      FUN_00a0e580(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x48:
    case 0x49:
    case 0x4a:
    case 0x4b:
      FUN_00a0e6a0(param_1);
      param_1 = param_1 + 5;
      uVar12 = uVar12 - 5;
      break;
    case 0x4c:
    case 0x4d:
    case 0x4e:
    case 0x4f:
      FUN_00a0e7f0(param_1);
      param_1 = param_1 + 6;
      uVar12 = uVar12 - 6;
      break;
    case 0x50:
    case 0x51:
    case 0x52:
    case 0x53:
    case 0x54:
    case 0x55:
    case 0x56:
    case 0x57:
      FUN_00a0e970(param_1);
      param_1 = param_1 + 4;
      uVar12 = uVar12 - 4;
      break;
    case 0x58:
    case 0x59:
    case 0x5a:
    case 0x5b:
      FUN_00a0eaf0(param_1);
      param_1 = param_1 + 7;
      uVar12 = uVar12 - 7;
      break;
    case 0x5c:
    case 0x5d:
    case 0x5e:
    case 0x5f:
      FUN_00a0ecf0(param_1);
      param_1 = param_1 + 9;
      uVar12 = uVar12 - 9;
      break;
    case 0x60:
    case 0x61:
    case 0x62:
    case 99:
      FUN_00a10690(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 100:
    case 0x65:
    case 0x66:
    case 0x67:
      if (DAT_011cb1e0 == 0) {
        switch(uVar10 & 3) {
        case 0:
          FUN_00a107e0(param_1);
          param_1 = param_1 + 4;
          uVar12 = uVar12 - 4;
          break;
        case 1:
          FUN_00a109c0(param_1);
          param_1 = param_1 + 4;
          uVar12 = uVar12 - 4;
          break;
        case 2:
          FUN_00a13010(param_1);
          param_1 = param_1 + 4;
          uVar12 = uVar12 - 4;
          break;
        case 3:
          FUN_00a13220(param_1);
        default:
          param_1 = param_1 + 4;
          uVar12 = uVar12 - 4;
        }
      }
      else {
        sVar2 = *(short *)((int)param_1 + 0x12);
        sVar3 = (short)param_1[4];
        sVar4 = (short)param_1[2];
        local_50 = (int)DAT_011cb1d8 >> 1;
        sVar5 = *(short *)((int)param_1 + 10);
        *(short *)(param_1 + 4) = (short)local_50;
        iVar1 = (int)sVar4 + (int)sVar3;
        if ((short)param_1[2] < iVar1) {
          do {
            iVar9 = ((local_50 - sVar3) - (int)sVar4) + (int)(short)param_1[2];
            if (0 < iVar9) {
              *(short *)(param_1 + 4) = (short)param_1[4] - (short)iVar9;
            }
            sVar11 = (short)((int)uVar7 >> 1);
            *(short *)((int)param_1 + 0x12) = sVar11;
            *(short *)((int)param_1 + 10) = sVar5;
            sVar6 = sVar5;
            while ((int)sVar6 < (int)sVar5 + (int)sVar2) {
              iVar9 = ((((int)uVar7 >> 1) - (int)sVar5) - (int)sVar2) +
                      (int)*(short *)((int)param_1 + 10);
              if (0 < iVar9) {
                *(short *)((int)param_1 + 0x12) = *(short *)((int)param_1 + 0x12) - (short)iVar9;
              }
              switch(*(byte *)((int)param_1 + 7) & 3) {
              case 0:
                FUN_00a107e0(param_1);
                break;
              case 1:
                FUN_00a109c0(param_1);
                break;
              case 2:
                FUN_00a13010(param_1);
                break;
              case 3:
                FUN_00a13220(param_1);
              }
              *(short *)((int)param_1 + 10) = *(short *)((int)param_1 + 10) + sVar11;
              sVar6 = *(short *)((int)param_1 + 10);
            }
            *(short *)(param_1 + 2) = (short)param_1[2] + (short)local_50;
          } while ((short)param_1[2] < iVar1);
        }
        *(short *)(param_1 + 4) = sVar3;
        *(short *)((int)param_1 + 0x12) = sVar2;
        *(short *)(param_1 + 2) = sVar4;
        *(short *)((int)param_1 + 10) = sVar5;
        param_1 = param_1 + 4;
        uVar12 = uVar12 - 4;
      }
      break;
    case 0x68:
    case 0x69:
    case 0x6a:
    case 0x6b:
    case 0x6c:
    case 0x6d:
    case 0x6e:
    case 0x6f:
      FUN_00a10b60(param_1);
      param_1 = param_1 + 2;
      uVar12 = uVar12 - 2;
      break;
    case 0x70:
    case 0x71:
    case 0x72:
    case 0x73:
      FUN_00a10ca0(param_1);
      param_1 = param_1 + 2;
      uVar12 = uVar12 - 2;
      break;
    case 0x74:
      FUN_00a10df0(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x75:
      FUN_00a11000(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x76:
      FUN_00a128d0(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x77:
      FUN_00a12ac0(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x78:
    case 0x79:
    case 0x7a:
    case 0x7b:
      FUN_00a111c0(param_1);
      param_1 = param_1 + 2;
      uVar12 = uVar12 - 2;
      break;
    case 0x7c:
      FUN_00a11310(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x7d:
      FUN_00a11520(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x7e:
      FUN_00a12c70(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0x7f:
      FUN_00a12e60(param_1);
      param_1 = param_1 + 3;
      uVar12 = uVar12 - 3;
      break;
    case 0xe1:
      DAT_011be6ec._0_2_ = (undefined2)param_1[1];
      param_1 = param_1 + 1;
      uVar12 = uVar12 - 1;
      break;
    case 0xe2:
      DAT_011cb1e0 = param_1[1] & 0xffffff;
      if (DAT_011cb1e0 != 0) {
        DAT_011cb1d8 = param_1[1] & 0x1f;
        DAT_011cb1dc = (int)DAT_011cb1e0 >> 5 & 0x1f;
        _DAT_011cb1d0 = (int)DAT_011cb1e0 >> 10 & 0x1f;
        _DAT_011cb1d4 = (int)DAT_011cb1e0 >> 0xf & 0x1f;
      }
      param_1 = param_1 + 1;
    case 0:
    case 1:
    case 2:
    case 3:
      uVar12 = uVar12 - 1;
      break;
    case 0xe3:
      if (param_1[0x10] != DAT_00a2c5a0) {
        local_10 = (float)(int)param_1[0xd];
        local_c = (float)(int)param_1[0xe];
        local_8 = (float)(int)param_1[0xf];
        local_40 = (float)(int)(short)param_1[8] * _DAT_00a2a0b8;
        local_30 = (float)(int)*(short *)((int)param_1 + 0x22) * _DAT_00a2a0b8;
        local_20 = (float)(int)(short)param_1[9] * _DAT_00a2a0b8;
        local_3c = (float)(int)*(short *)((int)param_1 + 0x26) * _DAT_00a2a0b8;
        local_2c = (float)(int)(short)param_1[10] * _DAT_00a2a0b8;
        local_1c = (float)(int)*(short *)((int)param_1 + 0x2a) * _DAT_00a2a0b8;
        local_14 = 0;
        local_38 = (float)(int)(short)param_1[0xb] * _DAT_00a2a0b8;
        local_28 = (float)(int)*(short *)((int)param_1 + 0x2e) * _DAT_00a2a0b8;
        local_18 = (float)(int)(short)param_1[0xc] * _DAT_00a2a0b8;
        local_24 = 0;
        local_34 = 0;
        local_4 = 0x3f800000;
        (**(code **)(*DAT_011be760 + 0x6c))(&local_40);
        FUN_00a0cdd0(local_50);
        return;
      }
      goto LAB_00a13b64;
    case 0xe4:
      if (param_1[0x10] != DAT_00a2c5a0) {
        local_10 = (float)(int)param_1[0xd];
        local_c = (float)(int)param_1[0xe];
        local_8 = (float)(int)param_1[0xf];
        local_40 = (float)(int)(short)param_1[8] * _DAT_00a2a0b8;
        local_30 = (float)(int)*(short *)((int)param_1 + 0x22) * _DAT_00a2a0b8;
        local_20 = (float)(int)(short)param_1[9] * _DAT_00a2a0b8;
        local_3c = (float)(int)*(short *)((int)param_1 + 0x26) * _DAT_00a2a0b8;
        local_2c = (float)(int)(short)param_1[10] * _DAT_00a2a0b8;
        local_1c = (float)(int)*(short *)((int)param_1 + 0x2a) * _DAT_00a2a0b8;
        local_14 = 0;
        local_38 = (float)(int)(short)param_1[0xb] * _DAT_00a2a0b8;
        local_28 = (float)(int)*(short *)((int)param_1 + 0x2e) * _DAT_00a2a0b8;
        local_18 = (float)(int)(short)param_1[0xc] * _DAT_00a2a0b8;
        local_24 = 0;
        local_34 = 0;
        local_4 = 0x3f800000;
        (**(code **)(*DAT_011be760 + 0x6c))(&local_40);
      }
LAB_00a13b64:
      FUN_00a0cdd0(param_1 + 2);
      return;
    case 0xe5:
      FUN_00a0e0c0(param_1 + 2);
      return;
    case 0xe6:
      if (param_1[0xd] != DAT_00a2c5a0) {
        local_10 = (float)(int)param_1[10];
        local_c = (float)(int)param_1[0xb];
        local_8 = (float)(int)param_1[0xc];
        local_40 = (float)(int)(short)param_1[5] * _DAT_00a2a0b8;
        local_30 = (float)(int)*(short *)((int)param_1 + 0x16) * _DAT_00a2a0b8;
        local_20 = (float)(int)(short)param_1[6] * _DAT_00a2a0b8;
        local_3c = (float)(int)*(short *)((int)param_1 + 0x1a) * _DAT_00a2a0b8;
        local_2c = (float)(int)(short)param_1[7] * _DAT_00a2a0b8;
        local_1c = (float)(int)*(short *)((int)param_1 + 0x1e) * _DAT_00a2a0b8;
        local_14 = 0;
        local_38 = (float)(int)(short)param_1[8] * _DAT_00a2a0b8;
        local_28 = (float)(int)*(short *)((int)param_1 + 0x22) * _DAT_00a2a0b8;
        local_18 = (float)(int)(short)param_1[9] * _DAT_00a2a0b8;
        local_24 = 0;
        local_34 = 0;
        local_4 = 0x3f800000;
        (**(code **)(*DAT_011be760 + 0x6c))(&local_40);
      }
      FUN_00a0e000(param_1[2],param_1[3],(int)(short)param_1[4]);
      return;
    case 0xe7:
      if (param_1[0xd] != DAT_00a2c5a0) {
        local_10 = (float)(int)param_1[10];
        local_c = (float)(int)param_1[0xb];
        local_8 = (float)(int)param_1[0xc];
        local_40 = (float)(int)(short)param_1[5] * _DAT_00a2a0b8;
        local_30 = (float)(int)*(short *)((int)param_1 + 0x16) * _DAT_00a2a0b8;
        local_20 = (float)(int)(short)param_1[6] * _DAT_00a2a0b8;
        local_3c = (float)(int)*(short *)((int)param_1 + 0x1a) * _DAT_00a2a0b8;
        local_2c = (float)(int)(short)param_1[7] * _DAT_00a2a0b8;
        local_1c = (float)(int)*(short *)((int)param_1 + 0x1e) * _DAT_00a2a0b8;
        local_14 = 0;
        local_38 = (float)(int)(short)param_1[8] * _DAT_00a2a0b8;
        local_28 = (float)(int)*(short *)((int)param_1 + 0x22) * _DAT_00a2a0b8;
        local_18 = (float)(int)(short)param_1[9] * _DAT_00a2a0b8;
        local_24 = 0;
        local_34 = 0;
        local_4 = 0x3f800000;
        (**(code **)(*DAT_011be760 + 0x6c))(&local_40);
      }
      FUN_00a133e0(param_1[2],param_1[3],(int)(short)param_1[4]);
      return;
    case 0xe8:
      FUN_00a142a0(param_1 + 2);
      return;
    case 0xf1:
    case 0xf3:
    case 0xf4:
    case 0xf5:
    case 0xf6:
    case 0xf7:
    case 0xf8:
      uVar12 = 0xffffffff;
      break;
    case 0xf2:
      (**(code **)(*DAT_011be760 + 0x5c))(0,0);
      return;
    case 0xf9:
      if (param_1[0x10] != DAT_00a2c5a0) {
        local_10 = (float)(int)param_1[0xd];
        local_c = (float)(int)param_1[0xe];
        local_8 = (float)(int)param_1[0xf];
        local_40 = (float)(int)(short)param_1[8] * _DAT_00a2a0b8;
        local_30 = (float)(int)*(short *)((int)param_1 + 0x22) * _DAT_00a2a0b8;
        local_20 = (float)(int)(short)param_1[9] * _DAT_00a2a0b8;
        local_3c = (float)(int)*(short *)((int)param_1 + 0x26) * _DAT_00a2a0b8;
        local_2c = (float)(int)(short)param_1[10] * _DAT_00a2a0b8;
        local_1c = (float)(int)*(short *)((int)param_1 + 0x2a) * _DAT_00a2a0b8;
        local_14 = 0;
        local_38 = (float)(int)(short)param_1[0xb] * _DAT_00a2a0b8;
        local_28 = (float)(int)*(short *)((int)param_1 + 0x2e) * _DAT_00a2a0b8;
        local_18 = (float)(int)(short)param_1[0xc] * _DAT_00a2a0b8;
        local_24 = 0;
        local_34 = 0;
        local_4 = 0x3f800000;
        (**(code **)(*DAT_011be760 + 0x6c))(&local_40);
      }
      FUN_00a0e040(param_1 + 2);
      return;
    case 0xfb:
      if (param_1[2] == 0xffffffff) {
        SetShift(DAT_011cb1bc,DAT_011cb1b8,DAT_011be7e0,1);
        return;
      }
      DAT_011cb1bc = DAT_011be778;
      DAT_011cb1b8 = DAT_011be77c;
      DAT_011be7e0 = DAT_00a2c498;
      SetShift(param_1[2],param_1[3],param_1[1] & 0xff,1);
      return;
    }
  } while (0 < (int)uVar12);
switchD_00a13437_caseD_4:
  return;
}



/* ===================================================== */
/* FUN_00a142a0 @ 00a142a0 */
/* ===================================================== */

void FUN_00a142a0(uint *param_1)

{
  float fVar1;
  float fVar2;
  byte *pbVar3;
  float fVar4;
  float fVar5;
  int iVar6;
  uint uVar7;
  uint uVar8;
  short sVar9;
  uint uVar10;
  int iVar11;
  int iVar12;
  short sVar13;
  float local_d4;
  uint local_d0;
  uint local_cc;
  float local_c4;
  int local_c0;
  uint local_b8;
  int local_ac;
  int local_a8;
  int local_a4;
  float local_9c;
  float local_98;
  undefined4 local_8c;
  undefined4 uStack_84;
  undefined4 uStack_80;
  float local_7c;
  float local_78;
  undefined4 local_6c;
  undefined4 uStack_64;
  undefined4 uStack_60;
  float fStack_5c;
  float fStack_58;
  undefined4 local_4c;
  undefined4 uStack_44;
  undefined4 uStack_40;
  float local_3c;
  float fStack_38;
  undefined4 local_2c;
  undefined4 uStack_24;
  undefined4 uStack_20;
  uint local_1c;
  float local_18;
  float local_14;
  float local_10;
  uint local_c;
  int local_8;
  float local_4;
  
  local_ac = 0;
  if ((*param_1 & 0x80000000) == 0) {
    pbVar3 = (byte *)param_1[5];
    local_cc = (uint)*pbVar3;
    local_d0 = (uint)pbVar3[1];
    uVar10 = (uint)*(ushort *)(pbVar3 + 2);
    local_c = (uint)*(ushort *)(pbVar3 + 4);
    if (local_d0 == 0) {
      local_d0 = 0x100;
    }
    if (local_cc == 0) {
      local_cc = 0x100;
    }
    iVar11 = uVar10 * local_cc;
    iVar6 = local_c * local_d0;
    local_4 = *(float *)(PTR_DAT_00a2c4a0 + ((int)(short)param_1[1] - (int)(short)param_1[6]) * 4);
    local_c4 = *(float *)(PTR_DAT_00a2c4a4 +
                         ((int)*(short *)((int)param_1 + 6) - (int)*(short *)((int)param_1 + 0x1a))
                         * 4);
    if ((short)param_1[3] < 0) {
      *(short *)(param_1 + 3) = (short)iVar11 - (short)(-(int)(short)param_1[3] % iVar11);
    }
    sVar13 = (short)((int)(short)param_1[3] % iVar11);
    *(short *)(param_1 + 3) = sVar13;
    if (*(short *)((int)param_1 + 0xe) < 0) {
      *(short *)((int)param_1 + 0xe) =
           (short)iVar6 - (short)(-(int)*(short *)((int)param_1 + 0xe) % iVar6);
    }
    sVar9 = (short)((int)*(short *)((int)param_1 + 0xe) % iVar6);
    *(short *)((int)param_1 + 0xe) = sVar9;
    fVar1 = *(float *)(PTR_DAT_00a2c4a0 + local_cc * 4);
    fVar2 = *(float *)(PTR_DAT_00a2c4a4 + local_d0 * 4);
    iVar6 = local_cc - (int)sVar13 % (int)local_cc;
    iVar11 = (int)*(short *)((int)param_1 + 10);
    iVar12 = local_d0 - (int)sVar9 % (int)local_d0;
    local_10 = ((float)iVar6 / (float)local_cc) * fVar1;
    fVar4 = ((float)iVar12 / (float)local_d0) * fVar2;
    local_1c = (int)(short)param_1[3] / (int)local_cc;
    local_18 = ((float)(((short)param_1[2] - iVar6) % (int)local_cc) / (float)local_cc) * fVar1;
    local_c0 = (int)*(short *)((int)param_1 + 0xe) % (int)local_d0;
    local_14 = ((float)((iVar11 - iVar12) % (int)local_d0) / (float)local_d0) * fVar2;
    local_8 = (int)(short)param_1[3] % (int)local_cc;
    local_2c = 0xffffff;
    local_4c = 0xffffff;
    local_6c = 0xffffff;
    local_8c = 0xffffff;
    local_b8 = (int)*(short *)((int)param_1 + 0xe) / (int)local_d0;
    while ((iVar11 = iVar11 + (local_c0 - local_d0), (int)local_d0 <= iVar11 ||
           (local_ac = iVar11, 0 < iVar11))) {
      local_a4 = (int)(short)param_1[2];
      local_a8 = 0;
      local_d4 = local_4;
      uVar8 = local_1c;
      iVar6 = local_8;
      while( true ) {
        do {
          uVar7 = (uint)*(ushort *)(*(int *)(param_1[5] + 0xc) + (local_b8 * uVar10 + uVar8) * 2);
        } while (uVar7 == 0xffff);
        local_a4 = local_a4 + (iVar6 - local_cc);
        if ((local_a4 < (int)local_cc) && (local_a8 = local_a4, local_a4 < 1)) break;
        pbVar3 = (byte *)(*(int *)(param_1[5] + 8) + uVar7 * 8);
        uVar7 = pbVar3[6] & 0x1f | *param_1 >> 0x12 & 0xc0;
        if (((uVar7 == DAT_00a2c488) && (*(ushort *)(pbVar3 + 2) == DAT_00a2c484)) &&
           (DAT_00a2c48c == 0)) {
          FUN_00a03db0(0);
        }
        else {
          FUN_00a27880(uVar7,(uint)*(ushort *)(pbVar3 + 2),0,0);
        }
        if ((DAT_00a2c480 & 9) != 0) {
          (**(code **)(*DAT_011be760 + 0x8c))(1);
        }
        local_9c = local_d4;
        DAT_00a2c480 = 6;
        local_3c = local_d4;
        local_98 = local_c4;
        local_78 = local_c4;
        if (iVar6 == 0) {
          if (local_a8 == 0) {
            fStack_5c = fVar1 + local_d4;
            if ((pbVar3[4] & 1) == 0) {
              uVar7 = (uint)*pbVar3;
              goto LAB_00a1472f;
            }
            uStack_44 = *(undefined4 *)(DAT_011cb1c0 + (uint)*pbVar3 * 4);
            uStack_24 = *(undefined4 *)(DAT_011cb1c0 + (*pbVar3 + local_cc) * 4);
          }
          else {
            fStack_5c = local_18 + local_d4;
            if ((pbVar3[4] & 1) == 0) {
              uStack_24 = *(undefined4 *)(DAT_011cb1c0 + (uint)*pbVar3 * 4);
              uStack_44 = *(undefined4 *)(DAT_011cb1c0 + ((uint)*pbVar3 + local_a8) * 4);
            }
            else {
              uStack_44 = *(undefined4 *)(DAT_011cb1c0 + ((*pbVar3 + local_cc) - local_a8) * 4);
              uStack_24 = *(undefined4 *)(DAT_011cb1c0 + (*pbVar3 + local_cc) * 4);
            }
          }
        }
        else {
          fStack_5c = local_10 + local_d4;
          if ((pbVar3[4] & 1) == 0) {
            uVar7 = (uint)*pbVar3 + iVar6;
LAB_00a1472f:
            uStack_24 = *(undefined4 *)(DAT_011cb1c0 + uVar7 * 4);
            uStack_44 = *(undefined4 *)(DAT_011cb1c0 + (*pbVar3 + local_cc) * 4);
          }
          else {
            uStack_44 = *(undefined4 *)(DAT_011cb1c0 + (uint)*pbVar3 * 4);
            uStack_24 = *(undefined4 *)(DAT_011cb1c0 + ((*pbVar3 + local_cc) - iVar6) * 4);
          }
        }
        local_d4 = fStack_5c;
        if (local_c0 == 0) {
          if (local_ac == 0) {
            fStack_38 = fVar2 + local_c4;
            if ((pbVar3[4] & 2) == 0) {
              uStack_60 = *(undefined4 *)(DAT_011cb1c4 + (uint)pbVar3[1] * 4);
              goto LAB_00a148bf;
            }
            uStack_20 = *(undefined4 *)(DAT_011cb1c4 + (uint)pbVar3[1] * 4);
            uStack_60 = *(undefined4 *)(DAT_011cb1c4 + (local_d0 + pbVar3[1]) * 4);
          }
          else {
            fStack_38 = local_14 + local_c4;
            if ((pbVar3[4] & 2) == 0) {
              uStack_60 = *(undefined4 *)(DAT_011cb1c4 + (uint)pbVar3[1] * 4);
              uStack_20 = *(undefined4 *)(DAT_011cb1c4 + (local_ac + (uint)pbVar3[1]) * 4);
            }
            else {
              uStack_20 = *(undefined4 *)
                           (DAT_011cb1c4 + ((local_d0 - local_ac) + (uint)pbVar3[1]) * 4);
              uStack_60 = *(undefined4 *)(DAT_011cb1c4 + (local_d0 + pbVar3[1]) * 4);
            }
          }
        }
        else {
          fStack_38 = fVar4 + local_c4;
          if ((pbVar3[4] & 2) == 0) {
            uStack_60 = *(undefined4 *)(DAT_011cb1c4 + (local_c0 + (uint)pbVar3[1]) * 4);
LAB_00a148bf:
            uStack_20 = *(undefined4 *)(DAT_011cb1c4 + (local_d0 + pbVar3[1]) * 4);
          }
          else {
            uStack_20 = *(undefined4 *)(DAT_011cb1c4 + (uint)pbVar3[1] * 4);
            uStack_60 = *(undefined4 *)
                         (DAT_011cb1c4 + ((local_d0 - local_c0) + (uint)pbVar3[1]) * 4);
          }
        }
        uStack_84 = uStack_24;
        uStack_80 = uStack_60;
        local_7c = fStack_5c;
        uStack_64 = uStack_44;
        fStack_58 = fStack_38;
        uStack_40 = uStack_20;
        (**(code **)(*DAT_011be760 + 0xbc))(6,3,&local_9c,4,0);
        iVar6 = 0;
        uVar8 = uVar8 + 1;
        if (uVar8 == uVar10) {
          uVar8 = 0;
        }
      }
      fVar5 = fVar2;
      if (local_c0 != 0) {
        fVar5 = fVar4;
      }
      local_c4 = local_c4 + fVar5;
      local_b8 = local_b8 + 1;
      local_c0 = 0;
      if (local_b8 == local_c) {
        local_b8 = 0;
      }
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a14980 @ 00a14980 */
/* ===================================================== */

void FUN_00a14980(uint param_1,uint param_2,uint *param_3,undefined2 param_4)

{
  uint *puVar1;
  int iVar2;
  uint *puVar3;
  uint *puVar4;
  
  puVar1 = (uint *)FUN_00a02ea0(0xe7,0xd);
  puVar1[2] = param_1;
  *(undefined2 *)(puVar1 + 4) = param_4;
  puVar1[3] = param_2;
  puVar3 = &EXT_011d0870;
  puVar4 = puVar1 + 5;
  for (iVar2 = 8; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar4 = *puVar3;
    puVar3 = puVar3 + 1;
    puVar4 = puVar4 + 1;
  }
  puVar1[0xd] = DAT_011cb208;
  *puVar1 = (*param_3 ^ *puVar1) & 0xffffff ^ *puVar1;
  *param_3 = ((uint)puVar1 ^ *param_3) & 0xffffff ^ *param_3;
  return;
}



/* ===================================================== */
/* FUN_00a149f0 @ 00a149f0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a149f0(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte *pbVar4;
  float *pfVar5;
  float *pfVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  uint uVar10;
  uint local_4;
  
                    /* 0x149f0  549   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar5 = DAT_011be7a8;
    pbVar4 = (byte *)param_1[3];
    if (((*pbVar4 == DAT_011be764) && (pbVar4[1] == DAT_011be768)) && (pbVar4[2] == DAT_011be76c)) {
      if (DAT_00a2c488 != -1) {
        if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
          DAT_00a2c578 = -5;
          DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_00a2c57c = 0;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
        }
        DAT_00a2c488 = -1;
      }
    }
    else {
      DAT_011be76c = (ushort)pbVar4[2];
      DAT_011be768 = (ushort)pbVar4[1];
      iVar9 = (int)(short)DAT_011be76c;
      uVar10 = (uint)(short)DAT_011be768;
      DAT_011be764 = (ushort)*pbVar4;
      uVar8 = (uint)(short)DAT_011be764;
      if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
        DAT_00a2c578 = -3;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar8;
        DAT_011be7ac[DAT_011be7dc] = uVar8;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = uVar10;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
      DAT_00a2c488 = -1;
    }
    if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 0;
    *pfVar5 = (float)(int)*(short *)*param_1;
    pfVar5[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar5[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar5[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar5[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar5[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar5[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar5[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar5[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar5[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar5[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar5[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    uVar8 = param_1[4];
    local_4 = 2;
    if (2 < uVar8) {
      pfVar6 = pfVar5 + 0x10;
      do {
        iVar9 = param_1[3] + local_4 * 4;
        if (((*(byte *)(param_1[3] + local_4 * 4) != DAT_011be764) ||
            (*(byte *)(iVar9 + 1) != DAT_011be768)) ||
           (pfVar7 = pfVar6, *(byte *)(iVar9 + 2) != DAT_011be76c)) {
          if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
          bVar1 = *(byte *)(param_1[3] + 2 + local_4 * 4);
          pbVar4 = (byte *)(param_1[3] + local_4 * 4);
          bVar2 = pbVar4[1];
          bVar3 = *pbVar4;
          if (((bVar3 == DAT_011be764) && (bVar2 == DAT_011be768)) && (bVar1 == DAT_011be76c)) {
            if (DAT_00a2c488 != -1) {
              if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
                DAT_00a2c578 = -5;
                DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_00a2c57c = 0;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
              }
              DAT_00a2c488 = -1;
            }
          }
          else {
            DAT_011be76c = (ushort)bVar1;
            DAT_011be768 = (ushort)bVar2;
            iVar9 = (int)(short)DAT_011be76c;
            uVar10 = (uint)(short)DAT_011be768;
            DAT_011be764 = (ushort)bVar3;
            uVar8 = (uint)(short)DAT_011be764;
            if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
              DAT_00a2c578 = -3;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar8;
              DAT_011be7ac[DAT_011be7dc] = uVar8;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = uVar10;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
            DAT_00a2c488 = -1;
          }
          if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 0;
          pfVar7 = pfVar6 + 0x10;
          *pfVar6 = (float)(int)*(short *)*param_1;
          pfVar6[1] = (float)(int)*(short *)(*param_1 + 2);
          pfVar6[2] = (float)(int)*(short *)(*param_1 + 4);
          pfVar6[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
          pfVar6[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
          pfVar6[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
          pfVar6[8] = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
          pfVar6[9] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
          pfVar6[10] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
          pfVar6[0xb] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xc] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xd] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        }
        local_4 = local_4 + 1;
        pfVar6 = pfVar7 + 8;
        *pfVar7 = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
        pfVar7[1] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
        pfVar7[2] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
        pfVar7[3] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[4] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[5] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        uVar8 = param_1[4];
      } while (local_4 < uVar8);
    }
    uVar8 = param_1[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,1);
  return;
}



/* ===================================================== */
/* FUN_00a153d0 @ 00a153d0 */
/* ===================================================== */

void FUN_00a153d0(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  byte *pbVar3;
  float *pfVar4;
  float *pfVar5;
  uint uVar6;
  
                    /* 0x153d0  551   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar4 = DAT_011be7a8;
    if (DAT_00a2c488 != -2) {
      if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
        DAT_00a2c578 = -5;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = 0;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_00a2c488 = -2;
    }
    if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 0;
    *pfVar4 = (float)(int)*(short *)*param_1;
    pfVar4[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar4[2] = (float)(int)*(short *)(*param_1 + 4);
    pbVar3 = (byte *)param_1[3];
    pfVar4[4] = (float)(((uint)pbVar3[1] + (uint)*pbVar3 * 0x100) * 0x100 + (uint)pbVar3[2]);
    pfVar4[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar4[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar4[10] = (float)(int)*(short *)(*param_1 + 0xc);
    uVar6 = param_1[3];
    pfVar4[0xc] = (float)(((uint)*(byte *)(uVar6 + 4) * 0x100 + (uint)*(byte *)(uVar6 + 5)) * 0x100
                         + (uint)*(byte *)(uVar6 + 6));
    uVar6 = 2;
    if (2 < param_1[4]) {
      pfVar5 = pfVar4 + 0x12;
      do {
        pfVar5[-2] = (float)(int)*(short *)(*param_1 + uVar6 * 8);
        pfVar5[-1] = (float)(int)*(short *)(*param_1 + 2 + uVar6 * 8);
        *pfVar5 = (float)(int)*(short *)(*param_1 + 4 + uVar6 * 8);
        iVar1 = uVar6 * 4;
        iVar2 = param_1[3] + uVar6 * 4;
        uVar6 = uVar6 + 1;
        pfVar5[2] = (float)(((uint)*(byte *)(iVar2 + 1) +
                            (uint)*(byte *)(param_1[3] + iVar1) * 0x100) * 0x100 +
                           (uint)*(byte *)(iVar2 + 2));
        pfVar5 = pfVar5 + 8;
      } while (uVar6 < param_1[4]);
    }
    uVar6 = param_1[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 2)) || (uVar6 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,2);
  return;
}



/* ===================================================== */
/* FUN_00a15740 @ 00a15740 */
/* ===================================================== */

void FUN_00a15740(uint *param_1,int param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  byte *pbVar3;
  float *pfVar4;
  uint *puVar5;
  uint uVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  
                    /* 0x15740  555   */
  puVar5 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar4 = DAT_011be7a8;
    uVar6 = (uint)*(ushort *)(param_2 + 0x16);
    uVar8 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar8;
      DAT_00a2c488 = uVar6;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar6;
        DAT_011be7ac[DAT_011be7dc] = uVar6;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    }
    uVar6 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar6;
      DAT_011be7ac[DAT_011be7dc] = uVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 7;
    *pfVar4 = (float)(int)*(short *)*param_1;
    pfVar4[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar4[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar4[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar4[9] = (float)(int)*(short *)(*param_1 + 10);
    uVar6 = *param_1;
    param_1 = (uint *)0x2;
    pfVar4[10] = (float)(int)*(short *)(uVar6 + 0xc);
    pbVar3 = (byte *)puVar5[3];
    pfVar4[4] = (float)(((uint)pbVar3[1] + (uint)*pbVar3 * 0x100) * 0x100 + (uint)pbVar3[2]);
    uVar6 = puVar5[3];
    pfVar4[0xc] = (float)(((uint)*(byte *)(uVar6 + 4) * 0x100 + (uint)*(byte *)(uVar6 + 5)) * 0x100
                         + (uint)*(byte *)(uVar6 + 6));
    pfVar4[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0xc) * 4);
    pfVar4[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0xd) * 4);
    pfVar4[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x14) * 4);
    pfVar4[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x15) * 4);
    uVar6 = puVar5[4];
    if (2 < uVar6) {
      pfVar7 = pfVar4 + 0x10;
      iVar9 = param_2 + 0x40;
      do {
        if ((*(ushort *)(iVar9 + -0x2a) != DAT_00a2c488) ||
           (*(ushort *)(iVar9 + -0x32) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 6) || ((DAT_00a2c57c != 2 || (uVar6 == 4)))) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 2;
            DAT_011be7ac[DAT_011be7dc] = 2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
          DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
          uVar6 = (uint)*(ushort *)(iVar9 + 0x16);
          uVar8 = (uint)*(ushort *)(iVar9 + 0xe);
          if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar8;
            DAT_00a2c488 = uVar6;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar6;
              DAT_011be7ac[DAT_011be7dc] = uVar6;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          }
          uVar6 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar6;
            DAT_011be7ac[DAT_011be7dc] = uVar6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 7;
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x34) * 4);
          pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x33) * 4);
          *pfVar7 = (float)(int)*(short *)*puVar5;
          pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2);
          pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4);
          iVar1 = puVar5[3] + (int)param_1 * 4;
          pfVar7[4] = (float)(((uint)*(byte *)(iVar1 + 1) +
                              (uint)*(byte *)(puVar5[3] + (int)param_1 * 4) * 0x100) * 0x100 +
                             (uint)*(byte *)(iVar1 + 2));
          pfVar7[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x2c) * 4);
          pfVar7[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x2b) * 4);
          pfVar7[8] = (float)(int)*(short *)((*puVar5 - 8) + (int)param_1 * 8);
          pfVar7[9] = (float)(int)*(short *)((*puVar5 - 6) + (int)param_1 * 8);
          pfVar7[10] = (float)(int)*(short *)((*puVar5 - 4) + (int)param_1 * 8);
          iVar1 = puVar5[3] + (int)param_1 * 4;
          pfVar7[0xc] = (float)(((uint)*(byte *)(iVar1 + 1) +
                                (uint)*(byte *)(puVar5[3] + (int)param_1 * 4) * 0x100) * 0x100 +
                               (uint)*(byte *)(iVar1 + 2));
          pfVar7 = pfVar7 + 0x10;
        }
        pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x24) * 4);
        pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x23) * 4);
        *pfVar7 = (float)(int)*(short *)(*puVar5 + (int)param_1 * 8);
        pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2 + (int)param_1 * 8);
        pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4 + (int)param_1 * 8);
        iVar1 = (int)param_1 * 4;
        iVar2 = puVar5[3] + (int)param_1 * 4;
        param_1 = (uint *)((int)param_1 + 1);
        pfVar7[4] = (float)(((uint)*(byte *)(iVar2 + 1) + (uint)*(byte *)(puVar5[3] + iVar1) * 0x100
                            ) * 0x100 + (uint)*(byte *)(iVar2 + 2));
        uVar6 = puVar5[4];
        pfVar7 = pfVar7 + 8;
        iVar9 = iVar9 + 0x20;
      } while (param_1 < uVar6);
    }
    uVar6 = puVar5[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 2)) || (uVar6 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
    *puVar5 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar5,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a15f10 @ 00a15f10 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a15f10(uint *param_1,int param_2,undefined4 param_3)

{
  float *pfVar1;
  uint *puVar2;
  uint uVar3;
  float *pfVar4;
  float *pfVar5;
  uint uVar6;
  int iVar7;
  
                    /* 0x15f10  553   */
  puVar2 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar1 = DAT_011be7a8;
    uVar3 = (uint)*(ushort *)(param_2 + 0x16);
    uVar6 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar3 != DAT_00a2c488) || (uVar6 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar6;
      DAT_00a2c488 = uVar3;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar3;
        DAT_011be7ac[DAT_011be7dc] = uVar3;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    }
    if ((((byte)DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 2;
    *pfVar1 = (float)(int)*(short *)*param_1;
    pfVar1[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar1[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar1[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar1[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar1[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar1[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar1[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar1[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar1[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar1[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar1[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    pfVar1[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0xc) * 4);
    pfVar1[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0xd) * 4);
    pfVar1[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x14) * 4);
    param_1 = (uint *)0x2;
    pfVar1[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x15) * 4);
    uVar3 = puVar2[4];
    if (2 < uVar3) {
      pfVar4 = pfVar1 + 0x10;
      iVar7 = param_2 + 0x40;
      do {
        if ((*(ushort *)(iVar7 + -0x2a) != DAT_00a2c488) ||
           (pfVar5 = pfVar4, *(ushort *)(iVar7 + -0x32) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 6) || ((DAT_00a2c57c != 1 || (uVar3 == 4)))) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar3;
          DAT_011be7a8 = DAT_011be7a8 + uVar3 * 8;
          uVar3 = (uint)*(ushort *)(iVar7 + 0x16);
          uVar6 = (uint)*(ushort *)(iVar7 + 0xe);
          if ((uVar3 != DAT_00a2c488) || (uVar6 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar6;
            DAT_00a2c488 = uVar3;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar3;
              DAT_011be7ac[DAT_011be7dc] = uVar3;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
          }
          if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 2;
          pfVar4[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar7 + -0x34) * 4);
          pfVar4[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar7 + -0x33) * 4);
          *pfVar4 = (float)(int)*(short *)*puVar2;
          pfVar4[1] = (float)(int)*(short *)(*puVar2 + 2);
          pfVar4[2] = (float)(int)*(short *)(*puVar2 + 4);
          pfVar4[3] = (float)(int)*(short *)puVar2[1] * _DAT_00a2a0c4;
          pfVar4[4] = (float)(int)*(short *)(puVar2[1] + 2) * _DAT_00a2a0c4;
          pfVar4[5] = (float)(int)*(short *)(puVar2[1] + 4) * _DAT_00a2a0c4;
          pfVar4[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar7 + -0x2c) * 4);
          pfVar4[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar7 + -0x2b) * 4);
          pfVar4[8] = (float)(int)*(short *)((*puVar2 - 8) + (int)param_1 * 8);
          pfVar4[9] = (float)(int)*(short *)((*puVar2 - 6) + (int)param_1 * 8);
          pfVar4[10] = (float)(int)*(short *)((*puVar2 - 4) + (int)param_1 * 8);
          pfVar4[0xb] = (float)(int)*(short *)((puVar2[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar5 = pfVar4 + 0x10;
          pfVar4[0xc] = (float)(int)*(short *)((puVar2[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar4[0xd] = (float)(int)*(short *)((puVar2[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        }
        pfVar4 = pfVar5 + 8;
        pfVar5[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar7 + -0x24) * 4);
        param_1 = (uint *)((int)param_1 + 1);
        pfVar5[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar7 + -0x23) * 4);
        *pfVar5 = (float)(int)*(short *)((*puVar2 - 8) + (int)param_1 * 8);
        pfVar5[1] = (float)(int)*(short *)((*puVar2 - 6) + (int)param_1 * 8);
        pfVar5[2] = (float)(int)*(short *)((*puVar2 - 4) + (int)param_1 * 8);
        pfVar5[3] = (float)(int)*(short *)((puVar2[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar5[4] = (float)(int)*(short *)((puVar2[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar5[5] = (float)(int)*(short *)((puVar2[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        uVar3 = puVar2[4];
        iVar7 = iVar7 + 0x20;
      } while (param_1 < uVar3);
    }
    uVar3 = puVar2[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 1)) || (uVar3 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar3;
    DAT_011be7a8 = DAT_011be7a8 + uVar3 * 8;
    *puVar2 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar2,param_2,param_3,4);
  return;
}



/* ===================================================== */
/* FUN_00a16780 @ 00a16780 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a16780(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte *pbVar4;
  float *pfVar5;
  float *pfVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  uint uVar10;
  uint local_4;
  
                    /* 0x16780  550   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar5 = DAT_011be7a8;
    pbVar4 = (byte *)param_1[3];
    if (((*pbVar4 == DAT_011be764) && (pbVar4[1] == DAT_011be768)) && (pbVar4[2] == DAT_011be76c)) {
      if (DAT_00a2c488 != -1) {
        if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
          DAT_00a2c578 = -5;
          DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_00a2c57c = 0;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
        }
        DAT_00a2c488 = -1;
      }
    }
    else {
      DAT_011be76c = (ushort)pbVar4[2];
      DAT_011be768 = (ushort)pbVar4[1];
      iVar9 = (int)(short)DAT_011be76c;
      uVar10 = (uint)(short)DAT_011be768;
      DAT_011be764 = (ushort)*pbVar4;
      uVar8 = (uint)(short)DAT_011be764;
      if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
        DAT_00a2c578 = -3;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar8;
        DAT_011be7ac[DAT_011be7dc] = uVar8;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = uVar10;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
      DAT_00a2c488 = -1;
    }
    uVar8 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar8;
      DAT_011be7ac[DAT_011be7dc] = uVar8;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 1;
    *pfVar5 = (float)(int)*(short *)*param_1;
    pfVar5[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar5[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar5[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar5[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar5[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar5[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar5[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar5[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar5[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar5[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar5[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    uVar8 = param_1[4];
    local_4 = 2;
    if (2 < uVar8) {
      pfVar6 = pfVar5 + 0x10;
      do {
        iVar9 = param_1[3] + local_4 * 4;
        if (((*(byte *)(param_1[3] + local_4 * 4) != DAT_011be764) ||
            (*(byte *)(iVar9 + 1) != DAT_011be768)) ||
           (pfVar7 = pfVar6, *(byte *)(iVar9 + 2) != DAT_011be76c)) {
          if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
          bVar1 = *(byte *)(param_1[3] + 2 + local_4 * 4);
          pbVar4 = (byte *)(param_1[3] + local_4 * 4);
          bVar2 = pbVar4[1];
          bVar3 = *pbVar4;
          if (((bVar3 == DAT_011be764) && (bVar2 == DAT_011be768)) && (bVar1 == DAT_011be76c)) {
            if (DAT_00a2c488 != -1) {
              if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
                DAT_00a2c578 = -5;
                DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_00a2c57c = 0;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
              }
              DAT_00a2c488 = -1;
            }
          }
          else {
            DAT_011be76c = (ushort)bVar1;
            DAT_011be768 = (ushort)bVar2;
            iVar9 = (int)(short)DAT_011be76c;
            uVar10 = (uint)(short)DAT_011be768;
            DAT_011be764 = (ushort)bVar3;
            uVar8 = (uint)(short)DAT_011be764;
            if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
              DAT_00a2c578 = -3;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar8;
              DAT_011be7ac[DAT_011be7dc] = uVar8;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = uVar10;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
            DAT_00a2c488 = -1;
          }
          uVar8 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar8;
            DAT_011be7ac[DAT_011be7dc] = uVar8;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 1;
          pfVar7 = pfVar6 + 0x10;
          *pfVar6 = (float)(int)*(short *)*param_1;
          pfVar6[1] = (float)(int)*(short *)(*param_1 + 2);
          pfVar6[2] = (float)(int)*(short *)(*param_1 + 4);
          pfVar6[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
          pfVar6[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
          pfVar6[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
          pfVar6[8] = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
          pfVar6[9] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
          pfVar6[10] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
          pfVar6[0xb] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xc] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xd] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        }
        local_4 = local_4 + 1;
        pfVar6 = pfVar7 + 8;
        *pfVar7 = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
        pfVar7[1] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
        pfVar7[2] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
        pfVar7[3] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[4] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[5] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        uVar8 = param_1[4];
      } while (local_4 < uVar8);
    }
    uVar8 = param_1[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,5);
  return;
}



/* ===================================================== */
/* FUN_00a17150 @ 00a17150 */
/* ===================================================== */

void FUN_00a17150(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  byte *pbVar3;
  uint uVar4;
  float *pfVar5;
  float *pfVar6;
  uint uVar7;
  
                    /* 0x17150  552   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar5 = DAT_011be7a8;
    if (DAT_00a2c488 != -2) {
      if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
        DAT_00a2c578 = -5;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = 0;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_00a2c488 = -2;
    }
    uVar4 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar4;
      DAT_011be7ac[DAT_011be7dc] = uVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 1;
    *pfVar5 = (float)(int)*(short *)*param_1;
    pfVar5[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar5[2] = (float)(int)*(short *)(*param_1 + 4);
    pbVar3 = (byte *)param_1[3];
    pfVar5[4] = (float)(((uint)pbVar3[1] + (uint)*pbVar3 * 0x100) * 0x100 + (uint)pbVar3[2]);
    pfVar5[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar5[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar5[10] = (float)(int)*(short *)(*param_1 + 0xc);
    uVar4 = param_1[3];
    uVar7 = 2;
    pfVar5[0xc] = (float)(((uint)*(byte *)(uVar4 + 4) * 0x100 + (uint)*(byte *)(uVar4 + 5)) * 0x100
                         + (uint)*(byte *)(uVar4 + 6));
    if (2 < param_1[4]) {
      pfVar6 = pfVar5 + 0x12;
      do {
        pfVar6[-2] = (float)(int)*(short *)(*param_1 + uVar7 * 8);
        pfVar6[-1] = (float)(int)*(short *)(*param_1 + 2 + uVar7 * 8);
        *pfVar6 = (float)(int)*(short *)(*param_1 + 4 + uVar7 * 8);
        iVar1 = uVar7 * 4;
        iVar2 = param_1[3] + uVar7 * 4;
        uVar7 = uVar7 + 1;
        pfVar6[2] = (float)(((uint)*(byte *)(iVar2 + 1) +
                            (uint)*(byte *)(param_1[3] + iVar1) * 0x100) * 0x100 +
                           (uint)*(byte *)(iVar2 + 2));
        pfVar6 = pfVar6 + 8;
      } while (uVar7 < param_1[4]);
    }
    uVar4 = param_1[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 2)) || (uVar4 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
    DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a174e0 @ 00a174e0 */
/* ===================================================== */

void FUN_00a174e0(uint *param_1,int param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  byte *pbVar3;
  float *pfVar4;
  uint *puVar5;
  uint uVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  
                    /* 0x174e0  556   */
  puVar5 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar4 = DAT_011be7a8;
    uVar6 = (uint)*(ushort *)(param_2 + 0x1a);
    uVar8 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar8;
      DAT_00a2c488 = uVar6;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar6;
        DAT_011be7ac[DAT_011be7dc] = uVar6;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    }
    uVar6 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar6;
      DAT_011be7ac[DAT_011be7dc] = uVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 7;
    *pfVar4 = (float)(int)*(short *)*param_1;
    pfVar4[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar4[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar4[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar4[9] = (float)(int)*(short *)(*param_1 + 10);
    uVar6 = *param_1;
    param_1 = (uint *)0x2;
    pfVar4[10] = (float)(int)*(short *)(uVar6 + 0xc);
    pbVar3 = (byte *)puVar5[3];
    pfVar4[4] = (float)(((uint)pbVar3[1] + (uint)*pbVar3 * 0x100) * 0x100 + (uint)pbVar3[2]);
    uVar6 = puVar5[3];
    pfVar4[0xc] = (float)(((uint)*(byte *)(uVar6 + 4) * 0x100 + (uint)*(byte *)(uVar6 + 5)) * 0x100
                         + (uint)*(byte *)(uVar6 + 6));
    pfVar4[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0xc) * 4);
    pfVar4[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0xd) * 4);
    pfVar4[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x18) * 4);
    pfVar4[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x19) * 4);
    uVar6 = puVar5[4];
    if (2 < uVar6) {
      pfVar7 = pfVar4 + 0x10;
      iVar9 = param_2 + 0x50;
      do {
        if ((*(ushort *)(iVar9 + -0x36) != DAT_00a2c488) ||
           (*(ushort *)(iVar9 + -0x42) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 6) || ((DAT_00a2c57c != 2 || (uVar6 == 4)))) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 2;
            DAT_011be7ac[DAT_011be7dc] = 2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
          DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
          uVar6 = (uint)*(ushort *)(iVar9 + 0x1a);
          uVar8 = (uint)*(ushort *)(iVar9 + 0xe);
          if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar8;
            DAT_00a2c488 = uVar6;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar6;
              DAT_011be7ac[DAT_011be7dc] = uVar6;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          }
          uVar6 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar6;
            DAT_011be7ac[DAT_011be7dc] = uVar6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 7;
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x44) * 4);
          pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x43) * 4);
          *pfVar7 = (float)(int)*(short *)*puVar5;
          pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2);
          pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4);
          iVar1 = puVar5[3] + (int)param_1 * 4;
          pfVar7[4] = (float)(((uint)*(byte *)(iVar1 + 1) +
                              (uint)*(byte *)(puVar5[3] + (int)param_1 * 4) * 0x100) * 0x100 +
                             (uint)*(byte *)(iVar1 + 2));
          pfVar7[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x38) * 4);
          pfVar7[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x37) * 4);
          pfVar7[8] = (float)(int)*(short *)((*puVar5 - 8) + (int)param_1 * 8);
          pfVar7[9] = (float)(int)*(short *)((*puVar5 - 6) + (int)param_1 * 8);
          pfVar7[10] = (float)(int)*(short *)((*puVar5 - 4) + (int)param_1 * 8);
          iVar1 = puVar5[3] + (int)param_1 * 4;
          pfVar7[0xc] = (float)(((uint)*(byte *)(iVar1 + 1) +
                                (uint)*(byte *)(puVar5[3] + (int)param_1 * 4) * 0x100) * 0x100 +
                               (uint)*(byte *)(iVar1 + 2));
          pfVar7 = pfVar7 + 0x10;
        }
        pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x2c) * 4);
        pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x2b) * 4);
        *pfVar7 = (float)(int)*(short *)(*puVar5 + (int)param_1 * 8);
        pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2 + (int)param_1 * 8);
        pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4 + (int)param_1 * 8);
        iVar1 = (int)param_1 * 4;
        iVar2 = puVar5[3] + (int)param_1 * 4;
        param_1 = (uint *)((int)param_1 + 1);
        pfVar7[4] = (float)(((uint)*(byte *)(iVar2 + 1) + (uint)*(byte *)(puVar5[3] + iVar1) * 0x100
                            ) * 0x100 + (uint)*(byte *)(iVar2 + 2));
        uVar6 = puVar5[4];
        pfVar7 = pfVar7 + 8;
        iVar9 = iVar9 + 0x28;
      } while (param_1 < uVar6);
    }
    uVar6 = puVar5[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 2)) || (uVar6 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
    *puVar5 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar5,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a17cb0 @ 00a17cb0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a17cb0(uint *param_1,int param_2,undefined4 param_3)

{
  float *pfVar1;
  uint *puVar2;
  uint uVar3;
  float *pfVar4;
  float *pfVar5;
  uint uVar6;
  int iVar7;
  
                    /* 0x17cb0  554   */
  puVar2 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar1 = DAT_011be7a8;
    uVar3 = (uint)*(ushort *)(param_2 + 0x1a);
    uVar6 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar3 != DAT_00a2c488) || (uVar6 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar6;
      DAT_00a2c488 = uVar3;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar3;
        DAT_011be7ac[DAT_011be7dc] = uVar3;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    }
    uVar3 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar3;
      DAT_011be7ac[DAT_011be7dc] = uVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 3;
    *pfVar1 = (float)(int)*(short *)*param_1;
    pfVar1[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar1[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar1[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar1[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar1[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar1[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar1[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar1[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar1[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar1[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar1[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    pfVar1[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0xc) * 4);
    pfVar1[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0xd) * 4);
    pfVar1[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x18) * 4);
    param_1 = (uint *)0x2;
    pfVar1[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x19) * 4);
    uVar3 = puVar2[4];
    if (2 < uVar3) {
      pfVar4 = pfVar1 + 0x10;
      iVar7 = param_2 + 0x50;
      do {
        if ((*(ushort *)(iVar7 + -0x36) != DAT_00a2c488) ||
           (pfVar5 = pfVar4, *(ushort *)(iVar7 + -0x42) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 6) || ((DAT_00a2c57c != 1 || (uVar3 == 4)))) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar3;
          DAT_011be7a8 = DAT_011be7a8 + uVar3 * 8;
          uVar3 = (uint)*(ushort *)(iVar7 + 0x1a);
          uVar6 = (uint)*(ushort *)(iVar7 + 0xe);
          if ((uVar3 != DAT_00a2c488) || (uVar6 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar6;
            DAT_00a2c488 = uVar3;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar3)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar3;
              DAT_011be7ac[DAT_011be7dc] = uVar3;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
          }
          uVar3 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar3;
            DAT_011be7ac[DAT_011be7dc] = uVar3;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 3;
          pfVar4[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar7 + -0x44) * 4);
          pfVar4[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar7 + -0x43) * 4);
          *pfVar4 = (float)(int)*(short *)*puVar2;
          pfVar4[1] = (float)(int)*(short *)(*puVar2 + 2);
          pfVar4[2] = (float)(int)*(short *)(*puVar2 + 4);
          pfVar4[3] = (float)(int)*(short *)puVar2[1] * _DAT_00a2a0c4;
          pfVar4[4] = (float)(int)*(short *)(puVar2[1] + 2) * _DAT_00a2a0c4;
          pfVar4[5] = (float)(int)*(short *)(puVar2[1] + 4) * _DAT_00a2a0c4;
          pfVar4[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar7 + -0x38) * 4);
          pfVar4[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar7 + -0x37) * 4);
          pfVar4[8] = (float)(int)*(short *)((*puVar2 - 8) + (int)param_1 * 8);
          pfVar4[9] = (float)(int)*(short *)((*puVar2 - 6) + (int)param_1 * 8);
          pfVar4[10] = (float)(int)*(short *)((*puVar2 - 4) + (int)param_1 * 8);
          pfVar4[0xb] = (float)(int)*(short *)((puVar2[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar5 = pfVar4 + 0x10;
          pfVar4[0xc] = (float)(int)*(short *)((puVar2[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar4[0xd] = (float)(int)*(short *)((puVar2[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        }
        pfVar4 = pfVar5 + 8;
        pfVar5[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar7 + -0x2c) * 4);
        param_1 = (uint *)((int)param_1 + 1);
        pfVar5[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar7 + -0x2b) * 4);
        *pfVar5 = (float)(int)*(short *)((*puVar2 - 8) + (int)param_1 * 8);
        pfVar5[1] = (float)(int)*(short *)((*puVar2 - 6) + (int)param_1 * 8);
        pfVar5[2] = (float)(int)*(short *)((*puVar2 - 4) + (int)param_1 * 8);
        pfVar5[3] = (float)(int)*(short *)((puVar2[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar5[4] = (float)(int)*(short *)((puVar2[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar5[5] = (float)(int)*(short *)((puVar2[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        uVar3 = puVar2[4];
        iVar7 = iVar7 + 0x28;
      } while (param_1 < uVar3);
    }
    uVar3 = puVar2[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 1)) || (uVar3 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar3;
    DAT_011be7a8 = DAT_011be7a8 + uVar3 * 8;
    *puVar2 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar2,param_2,param_3,8);
  return;
}



/* ===================================================== */
/* FUN_00a18530 @ 00a18530 */
/* ===================================================== */

void FUN_00a18530(uint *param_1,int param_2,undefined4 param_3)

{
  short sVar1;
  float *pfVar2;
  uint *puVar3;
  uint uVar4;
  float *pfVar5;
  float *pfVar6;
  uint uVar7;
  int iVar8;
  
                    /* 0x18530  557   */
  puVar3 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar2 = DAT_011be7a8;
    uVar4 = (uint)*(ushort *)(param_2 + 0x16);
    uVar7 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar4 != DAT_00a2c488) || (uVar7 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar7;
      DAT_00a2c488 = uVar4;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar4;
        DAT_011be7ac[DAT_011be7dc] = uVar4;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar7;
    }
    uVar4 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar4;
      DAT_011be7ac[DAT_011be7dc] = uVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 7;
    *pfVar2 = (float)(int)*(short *)*param_1;
    pfVar2[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar2[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar2[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar2[9] = (float)(int)*(short *)(*param_1 + 10);
    sVar1 = *(short *)(*param_1 + 0xc);
    pfVar2[4] = 1.180104e-38;
    pfVar2[0xc] = 1.180104e-38;
    param_1 = (uint *)0x2;
    pfVar2[10] = (float)(int)sVar1;
    pfVar2[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0xc) * 4);
    pfVar2[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0xd) * 4);
    pfVar2[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x14) * 4);
    pfVar2[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x15) * 4);
    uVar4 = puVar3[4];
    if (2 < uVar4) {
      pfVar5 = pfVar2 + 0x10;
      iVar8 = param_2 + 0x40;
      do {
        if ((*(ushort *)(iVar8 + -0x2a) != DAT_00a2c488) ||
           (pfVar6 = pfVar5, *(ushort *)(iVar8 + -0x32) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 6) || ((DAT_00a2c57c != 2 || (uVar4 == 4)))) {
            DAT_00a2c578 = 6;
            DAT_011be7ac[DAT_011be7dc] = 6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 2;
            DAT_011be7ac[DAT_011be7dc] = 2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
          DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
          uVar4 = (uint)*(ushort *)(iVar8 + 0x16);
          uVar7 = (uint)*(ushort *)(iVar8 + 0xe);
          if ((uVar4 != DAT_00a2c488) || (uVar7 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar7;
            DAT_00a2c488 = uVar4;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar4;
              DAT_011be7ac[DAT_011be7dc] = uVar4;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar7;
          }
          uVar4 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar4;
            DAT_011be7ac[DAT_011be7dc] = uVar4;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 7;
          pfVar5[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x34) * 4);
          pfVar5[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x33) * 4);
          *pfVar5 = (float)(int)*(short *)*puVar3;
          pfVar5[1] = (float)(int)*(short *)(*puVar3 + 2);
          pfVar5[2] = (float)(int)*(short *)(*puVar3 + 4);
          pfVar5[4] = 1.180104e-38;
          pfVar5[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x2c) * 4);
          pfVar6 = pfVar5 + 0x10;
          pfVar5[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x2b) * 4);
          pfVar5[8] = (float)(int)*(short *)((*puVar3 - 8) + (int)param_1 * 8);
          pfVar5[9] = (float)(int)*(short *)((*puVar3 - 6) + (int)param_1 * 8);
          pfVar5[10] = (float)(int)*(short *)((*puVar3 - 4) + (int)param_1 * 8);
          pfVar5[0xc] = 1.180104e-38;
        }
        pfVar5 = pfVar6 + 8;
        pfVar6[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x24) * 4);
        param_1 = (uint *)((int)param_1 + 1);
        pfVar6[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x23) * 4);
        *pfVar6 = (float)(int)*(short *)((*puVar3 - 8) + (int)param_1 * 8);
        pfVar6[1] = (float)(int)*(short *)((*puVar3 - 6) + (int)param_1 * 8);
        pfVar6[2] = (float)(int)*(short *)((*puVar3 - 4) + (int)param_1 * 8);
        pfVar6[4] = 1.180104e-38;
        uVar4 = puVar3[4];
        iVar8 = iVar8 + 0x20;
      } while (param_1 < uVar4);
    }
    uVar4 = puVar3[4];
    if (((DAT_00a2c578 != 6) || (DAT_00a2c57c != 2)) || (uVar4 == 4)) {
      DAT_00a2c578 = 6;
      DAT_011be7ac[DAT_011be7dc] = 6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
    DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
    *puVar3 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar3,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a18c70 @ 00a18c70 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a18c70(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte *pbVar4;
  float *pfVar5;
  float *pfVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  uint uVar10;
  uint local_4;
  
                    /* 0x18c70  540   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar5 = DAT_011be7a8;
    pbVar4 = (byte *)param_1[3];
    if (((*pbVar4 == DAT_011be764) && (pbVar4[1] == DAT_011be768)) && (pbVar4[2] == DAT_011be76c)) {
      if (DAT_00a2c488 != -1) {
        if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
          DAT_00a2c578 = -5;
          DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_00a2c57c = 0;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
        }
        DAT_00a2c488 = -1;
      }
    }
    else {
      DAT_011be76c = (ushort)pbVar4[2];
      DAT_011be768 = (ushort)pbVar4[1];
      iVar9 = (int)(short)DAT_011be76c;
      uVar10 = (uint)(short)DAT_011be768;
      DAT_011be764 = (ushort)*pbVar4;
      uVar8 = (uint)(short)DAT_011be764;
      if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
        DAT_00a2c578 = -3;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar8;
        DAT_011be7ac[DAT_011be7dc] = uVar8;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = uVar10;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
      DAT_00a2c488 = -1;
    }
    if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 0;
    *pfVar5 = (float)(int)*(short *)*param_1;
    pfVar5[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar5[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar5[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar5[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar5[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar5[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar5[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar5[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar5[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar5[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar5[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    uVar8 = param_1[4];
    local_4 = 2;
    if (2 < uVar8) {
      pfVar6 = pfVar5 + 0x10;
      do {
        iVar9 = param_1[3] + local_4 * 4;
        if (((*(byte *)(param_1[3] + local_4 * 4) != DAT_011be764) ||
            (*(byte *)(iVar9 + 1) != DAT_011be768)) ||
           (pfVar7 = pfVar6, *(byte *)(iVar9 + 2) != DAT_011be76c)) {
          if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
          bVar1 = *(byte *)(param_1[3] + 2 + local_4 * 4);
          pbVar4 = (byte *)(param_1[3] + local_4 * 4);
          bVar2 = pbVar4[1];
          bVar3 = *pbVar4;
          if (((bVar3 == DAT_011be764) && (bVar2 == DAT_011be768)) && (bVar1 == DAT_011be76c)) {
            if (DAT_00a2c488 != -1) {
              if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
                DAT_00a2c578 = -5;
                DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_00a2c57c = 0;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
              }
              DAT_00a2c488 = -1;
            }
          }
          else {
            DAT_011be76c = (ushort)bVar1;
            DAT_011be768 = (ushort)bVar2;
            iVar9 = (int)(short)DAT_011be76c;
            uVar10 = (uint)(short)DAT_011be768;
            DAT_011be764 = (ushort)bVar3;
            uVar8 = (uint)(short)DAT_011be764;
            if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
              DAT_00a2c578 = -3;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar8;
              DAT_011be7ac[DAT_011be7dc] = uVar8;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = uVar10;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
            DAT_00a2c488 = -1;
          }
          if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 0;
          pfVar7 = pfVar6 + 0x10;
          *pfVar6 = (float)(int)*(short *)*param_1;
          pfVar6[1] = (float)(int)*(short *)(*param_1 + 2);
          pfVar6[2] = (float)(int)*(short *)(*param_1 + 4);
          pfVar6[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
          pfVar6[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
          pfVar6[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
          pfVar6[8] = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
          pfVar6[9] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
          pfVar6[10] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
          pfVar6[0xb] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xc] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xd] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        }
        local_4 = local_4 + 1;
        pfVar6 = pfVar7 + 8;
        *pfVar7 = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
        pfVar7[1] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
        pfVar7[2] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
        pfVar7[3] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[4] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[5] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        uVar8 = param_1[4];
      } while (local_4 < uVar8);
    }
    uVar8 = param_1[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,0x11);
  return;
}



/* ===================================================== */
/* FUN_00a19640 @ 00a19640 */
/* ===================================================== */

void FUN_00a19640(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  byte *pbVar3;
  float *pfVar4;
  float *pfVar5;
  uint uVar6;
  
                    /* 0x19640  542   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar4 = DAT_011be7a8;
    if (DAT_00a2c488 != -2) {
      if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
        DAT_00a2c578 = -5;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = 0;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_00a2c488 = -2;
    }
    if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 0;
    *pfVar4 = (float)(int)*(short *)*param_1;
    pfVar4[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar4[2] = (float)(int)*(short *)(*param_1 + 4);
    pbVar3 = (byte *)param_1[3];
    pfVar4[4] = (float)(((uint)pbVar3[1] + (uint)*pbVar3 * 0x100) * 0x100 + (uint)pbVar3[2]);
    pfVar4[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar4[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar4[10] = (float)(int)*(short *)(*param_1 + 0xc);
    uVar6 = param_1[3];
    pfVar4[0xc] = (float)(((uint)*(byte *)(uVar6 + 4) * 0x100 + (uint)*(byte *)(uVar6 + 5)) * 0x100
                         + (uint)*(byte *)(uVar6 + 6));
    uVar6 = 2;
    if (2 < param_1[4]) {
      pfVar5 = pfVar4 + 0x12;
      do {
        pfVar5[-2] = (float)(int)*(short *)(*param_1 + uVar6 * 8);
        pfVar5[-1] = (float)(int)*(short *)(*param_1 + 2 + uVar6 * 8);
        *pfVar5 = (float)(int)*(short *)(*param_1 + 4 + uVar6 * 8);
        iVar1 = uVar6 * 4;
        iVar2 = param_1[3] + uVar6 * 4;
        uVar6 = uVar6 + 1;
        pfVar5[2] = (float)(((uint)*(byte *)(iVar2 + 1) +
                            (uint)*(byte *)(param_1[3] + iVar1) * 0x100) * 0x100 +
                           (uint)*(byte *)(iVar2 + 2));
        pfVar5 = pfVar5 + 8;
      } while (uVar6 < param_1[4]);
    }
    uVar6 = param_1[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 2)) || (uVar6 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,0x12);
  return;
}



/* ===================================================== */
/* FUN_00a199a0 @ 00a199a0 */
/* ===================================================== */

void FUN_00a199a0(uint *param_1,int param_2,undefined4 param_3)

{
  undefined1 *puVar1;
  byte bVar2;
  byte *pbVar3;
  float *pfVar4;
  uint *puVar5;
  uint uVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  
                    /* 0x199a0  546   */
  puVar5 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar4 = DAT_011be7a8;
    uVar6 = (uint)*(ushort *)(param_2 + 0x16);
    uVar8 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar8;
      DAT_00a2c488 = uVar6;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar6;
        DAT_011be7ac[DAT_011be7dc] = uVar6;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    }
    uVar6 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar6;
      DAT_011be7ac[DAT_011be7dc] = uVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 7;
    *pfVar4 = (float)(int)*(short *)*param_1;
    pfVar4[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar4[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar4[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar4[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar4[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pbVar3 = (byte *)param_1[3];
    pfVar4[4] = (float)(((uint)*pbVar3 * 0x100 + (uint)pbVar3[1]) * 0x100 + (uint)pbVar3[2]);
    uVar6 = param_1[3];
    pfVar4[0xc] = (float)(((uint)*(byte *)(uVar6 + 4) * 0x100 + (uint)*(byte *)(uVar6 + 5)) * 0x100
                         + (uint)*(byte *)(uVar6 + 6));
    param_1 = (uint *)0x2;
    pfVar4[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x1c) * 4);
    pfVar4[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x1d) * 4);
    pfVar4[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x14) * 4);
    pfVar4[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x15) * 4);
    uVar6 = puVar5[4];
    if (2 < uVar6) {
      iVar9 = param_2 + 0x40;
      pfVar7 = pfVar4 + 0x10;
      do {
        if ((*(ushort *)(iVar9 + -0x2a) != DAT_00a2c488) ||
           (*(ushort *)(iVar9 + -0x32) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 5) || ((DAT_00a2c57c != 2 || (uVar6 == 4)))) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 2;
            DAT_011be7ac[DAT_011be7dc] = 2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
          DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
          uVar8 = (uint)*(ushort *)(iVar9 + 0xe);
          uVar6 = (uint)*(ushort *)(iVar9 + 0x16);
          if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar8;
            DAT_00a2c488 = uVar6;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar6;
              DAT_011be7ac[DAT_011be7dc] = uVar6;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          }
          uVar6 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar6;
            DAT_011be7ac[DAT_011be7dc] = uVar6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 7;
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x34) * 4);
          pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x33) * 4);
          *pfVar7 = (float)(int)*(short *)*puVar5;
          pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2);
          pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4);
          puVar1 = (undefined1 *)(puVar5[3] + (int)param_1 * 4);
          pfVar7[4] = (float)((uint)CONCAT11(*puVar1,puVar1[1]) * 0x100 + (uint)(byte)puVar1[2]);
          pfVar7[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x2c) * 4);
          pfVar7[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x2b) * 4);
          pfVar7[8] = (float)(int)*(short *)((*puVar5 - 8) + (int)param_1 * 8);
          pfVar7[9] = (float)(int)*(short *)((*puVar5 - 6) + (int)param_1 * 8);
          pfVar7[10] = (float)(int)*(short *)((*puVar5 - 4) + (int)param_1 * 8);
          puVar1 = (undefined1 *)(puVar5[3] + (int)param_1 * 4);
          pfVar7[0xc] = (float)((uint)CONCAT11(*puVar1,puVar1[1]) * 0x100 + (uint)(byte)puVar1[2]);
          pfVar7 = pfVar7 + 0x10;
        }
        if (((uint)param_1 & 1) == 0) {
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x34) * 4);
          bVar2 = *(byte *)(iVar9 + -0x33);
        }
        else {
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x24) * 4);
          bVar2 = *(byte *)(iVar9 + -0x23);
        }
        iVar9 = iVar9 + 0x20;
        pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)bVar2 * 4);
        *pfVar7 = (float)(int)*(short *)(*puVar5 + (int)param_1 * 8);
        pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2 + (int)param_1 * 8);
        pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4 + (int)param_1 * 8);
        puVar1 = (undefined1 *)(puVar5[3] + (int)param_1 * 4);
        param_1 = (uint *)((int)param_1 + 1);
        pfVar7[4] = (float)((uint)CONCAT11(*puVar1,puVar1[1]) * 0x100 + (uint)(byte)puVar1[2]);
        uVar6 = puVar5[4];
        pfVar7 = pfVar7 + 8;
      } while (param_1 < uVar6);
    }
    uVar6 = puVar5[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 2)) || (uVar6 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
    *puVar5 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar5,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a1a190 @ 00a1a190 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1a190(uint *param_1,int param_2,undefined4 param_3)

{
  byte bVar1;
  float *pfVar2;
  uint *puVar3;
  uint uVar4;
  float *pfVar5;
  float *pfVar6;
  uint uVar7;
  int iVar8;
  
                    /* 0x1a190  544   */
  puVar3 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar2 = DAT_011be7a8;
    uVar4 = (uint)*(ushort *)(param_2 + 0x16);
    uVar7 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar4 != DAT_00a2c488) || (uVar7 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar7;
      DAT_00a2c488 = uVar4;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar4;
        DAT_011be7ac[DAT_011be7dc] = uVar4;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar7;
    }
    if ((((byte)DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 2;
    *pfVar2 = (float)(int)*(short *)*param_1;
    pfVar2[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar2[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar2[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar2[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar2[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar2[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar2[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar2[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar2[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar2[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar2[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    pfVar2[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x1c) * 4);
    pfVar2[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x1d) * 4);
    pfVar2[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x14) * 4);
    param_1 = (uint *)0x2;
    pfVar2[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x15) * 4);
    uVar4 = puVar3[4];
    if (2 < uVar4) {
      pfVar5 = pfVar2 + 0x10;
      iVar8 = param_2 + 0x40;
      do {
        if ((*(ushort *)(iVar8 + -0x2a) != DAT_00a2c488) ||
           (pfVar6 = pfVar5, *(ushort *)(iVar8 + -0x32) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 5) || ((DAT_00a2c57c != 1 || (uVar4 == 4)))) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
          DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
          uVar4 = (uint)*(ushort *)(iVar8 + 0x16);
          uVar7 = (uint)*(ushort *)(iVar8 + 0xe);
          if ((uVar4 != DAT_00a2c488) || (uVar7 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar7;
            DAT_00a2c488 = uVar4;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar4;
              DAT_011be7ac[DAT_011be7dc] = uVar4;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar7;
          }
          if (((DAT_00a2c480 & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != 1)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 2;
          pfVar5[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x34) * 4);
          pfVar5[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x33) * 4);
          *pfVar5 = (float)(int)*(short *)*puVar3;
          pfVar5[1] = (float)(int)*(short *)(*puVar3 + 2);
          pfVar5[2] = (float)(int)*(short *)(*puVar3 + 4);
          pfVar5[3] = (float)(int)*(short *)puVar3[1] * _DAT_00a2a0c4;
          pfVar5[4] = (float)(int)*(short *)(puVar3[1] + 2) * _DAT_00a2a0c4;
          pfVar5[5] = (float)(int)*(short *)(puVar3[1] + 4) * _DAT_00a2a0c4;
          pfVar5[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x2c) * 4);
          pfVar5[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x2b) * 4);
          pfVar5[8] = (float)(int)*(short *)((*puVar3 - 8) + (int)param_1 * 8);
          pfVar5[9] = (float)(int)*(short *)((*puVar3 - 6) + (int)param_1 * 8);
          pfVar5[10] = (float)(int)*(short *)((*puVar3 - 4) + (int)param_1 * 8);
          pfVar5[0xb] = (float)(int)*(short *)((puVar3[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar6 = pfVar5 + 0x10;
          pfVar5[0xc] = (float)(int)*(short *)((puVar3[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar5[0xd] = (float)(int)*(short *)((puVar3[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        }
        if (((uint)param_1 & 1) == 0) {
          pfVar6[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x34) * 4);
          bVar1 = *(byte *)(iVar8 + -0x33);
        }
        else {
          pfVar6[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x24) * 4);
          bVar1 = *(byte *)(iVar8 + -0x23);
        }
        param_1 = (uint *)((int)param_1 + 1);
        iVar8 = iVar8 + 0x20;
        pfVar5 = pfVar6 + 8;
        pfVar6[7] = *(float *)(DAT_011cb1cc + (uint)bVar1 * 4);
        *pfVar6 = (float)(int)*(short *)((*puVar3 - 8) + (int)param_1 * 8);
        pfVar6[1] = (float)(int)*(short *)((*puVar3 - 6) + (int)param_1 * 8);
        pfVar6[2] = (float)(int)*(short *)((*puVar3 - 4) + (int)param_1 * 8);
        pfVar6[3] = (float)(int)*(short *)((puVar3[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar6[4] = (float)(int)*(short *)((puVar3[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar6[5] = (float)(int)*(short *)((puVar3[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        uVar4 = puVar3[4];
      } while (param_1 < uVar4);
    }
    uVar4 = puVar3[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 1)) || (uVar4 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
    DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
    *puVar3 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar3,param_2,param_3,0x14);
  return;
}



/* ===================================================== */
/* FUN_00a1aa20 @ 00a1aa20 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1aa20(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte *pbVar4;
  float *pfVar5;
  float *pfVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  uint uVar10;
  uint local_4;
  
                    /* 0x1aa20  541   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar5 = DAT_011be7a8;
    pbVar4 = (byte *)param_1[3];
    if (((*pbVar4 == DAT_011be764) && (pbVar4[1] == DAT_011be768)) && (pbVar4[2] == DAT_011be76c)) {
      if (DAT_00a2c488 != -1) {
        if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
          DAT_00a2c578 = -5;
          DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_00a2c57c = 0;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0;
          DAT_011be7dc = DAT_011be7dc + 1;
          DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
        }
        DAT_00a2c488 = -1;
      }
    }
    else {
      DAT_011be76c = (ushort)pbVar4[2];
      DAT_011be768 = (ushort)pbVar4[1];
      iVar9 = (int)(short)DAT_011be76c;
      uVar10 = (uint)(short)DAT_011be768;
      DAT_011be764 = (ushort)*pbVar4;
      uVar8 = (uint)(short)DAT_011be764;
      if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
        DAT_00a2c578 = -3;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar8;
        DAT_011be7ac[DAT_011be7dc] = uVar8;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = uVar10;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
      DAT_00a2c488 = -1;
    }
    uVar8 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar8;
      DAT_011be7ac[DAT_011be7dc] = uVar8;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 1;
    *pfVar5 = (float)(int)*(short *)*param_1;
    pfVar5[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar5[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar5[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar5[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar5[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar5[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar5[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar5[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar5[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar5[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar5[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    uVar8 = param_1[4];
    local_4 = 2;
    if (2 < uVar8) {
      pfVar6 = pfVar5 + 0x10;
      do {
        iVar9 = param_1[3] + local_4 * 4;
        if (((*(byte *)(param_1[3] + local_4 * 4) != DAT_011be764) ||
            (*(byte *)(iVar9 + 1) != DAT_011be768)) ||
           (pfVar7 = pfVar6, *(byte *)(iVar9 + 2) != DAT_011be76c)) {
          if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
          bVar1 = *(byte *)(param_1[3] + 2 + local_4 * 4);
          pbVar4 = (byte *)(param_1[3] + local_4 * 4);
          bVar2 = pbVar4[1];
          bVar3 = *pbVar4;
          if (((bVar3 == DAT_011be764) && (bVar2 == DAT_011be768)) && (bVar1 == DAT_011be76c)) {
            if (DAT_00a2c488 != -1) {
              if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
                DAT_00a2c578 = -5;
                DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_00a2c57c = 0;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0;
                DAT_011be7dc = DAT_011be7dc + 1;
                DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
              }
              DAT_00a2c488 = -1;
            }
          }
          else {
            DAT_011be76c = (ushort)bVar1;
            DAT_011be768 = (ushort)bVar2;
            iVar9 = (int)(short)DAT_011be76c;
            uVar10 = (uint)(short)DAT_011be768;
            DAT_011be764 = (ushort)bVar3;
            uVar8 = (uint)(short)DAT_011be764;
            if ((DAT_00a2c578 != -3) || (DAT_00a2c57c != uVar8)) {
              DAT_00a2c578 = -3;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffd;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar8;
              DAT_011be7ac[DAT_011be7dc] = uVar8;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = uVar10;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + iVar9;
            DAT_00a2c488 = -1;
          }
          uVar8 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar8;
            DAT_011be7ac[DAT_011be7dc] = uVar8;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 1;
          pfVar7 = pfVar6 + 0x10;
          *pfVar6 = (float)(int)*(short *)*param_1;
          pfVar6[1] = (float)(int)*(short *)(*param_1 + 2);
          pfVar6[2] = (float)(int)*(short *)(*param_1 + 4);
          pfVar6[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
          pfVar6[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
          pfVar6[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
          pfVar6[8] = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
          pfVar6[9] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
          pfVar6[10] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
          pfVar6[0xb] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xc] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
          pfVar6[0xd] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        }
        local_4 = local_4 + 1;
        pfVar6 = pfVar7 + 8;
        *pfVar7 = (float)(int)*(short *)((*param_1 - 8) + local_4 * 8);
        pfVar7[1] = (float)(int)*(short *)((*param_1 - 6) + local_4 * 8);
        pfVar7[2] = (float)(int)*(short *)((*param_1 - 4) + local_4 * 8);
        pfVar7[3] = (float)(int)*(short *)((param_1[1] - 8) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[4] = (float)(int)*(short *)((param_1[1] - 6) + local_4 * 8) * _DAT_00a2a0c4;
        pfVar7[5] = (float)(int)*(short *)((param_1[1] - 4) + local_4 * 8) * _DAT_00a2a0c4;
        uVar8 = param_1[4];
      } while (local_4 < uVar8);
    }
    uVar8 = param_1[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 1)) || (uVar8 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    DAT_011be7a8 = DAT_011be7a8 + uVar8 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,0x15);
  return;
}



/* ===================================================== */
/* FUN_00a1b3f0 @ 00a1b3f0 */
/* ===================================================== */

void FUN_00a1b3f0(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  byte *pbVar3;
  uint uVar4;
  float *pfVar5;
  float *pfVar6;
  uint uVar7;
  
                    /* 0x1b3f0  543   */
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar5 = DAT_011be7a8;
    if (DAT_00a2c488 != -2) {
      if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) {
        DAT_00a2c578 = -5;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffb;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = 0;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_00a2c488 = -2;
    }
    uVar4 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar4;
      DAT_011be7ac[DAT_011be7dc] = uVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 1;
    *pfVar5 = (float)(int)*(short *)*param_1;
    pfVar5[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar5[2] = (float)(int)*(short *)(*param_1 + 4);
    pbVar3 = (byte *)param_1[3];
    pfVar5[4] = (float)(((uint)pbVar3[1] + (uint)*pbVar3 * 0x100) * 0x100 + (uint)pbVar3[2]);
    pfVar5[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar5[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar5[10] = (float)(int)*(short *)(*param_1 + 0xc);
    uVar4 = param_1[3];
    uVar7 = 2;
    pfVar5[0xc] = (float)(((uint)*(byte *)(uVar4 + 4) * 0x100 + (uint)*(byte *)(uVar4 + 5)) * 0x100
                         + (uint)*(byte *)(uVar4 + 6));
    if (2 < param_1[4]) {
      pfVar6 = pfVar5 + 0x12;
      do {
        pfVar6[-2] = (float)(int)*(short *)(*param_1 + uVar7 * 8);
        pfVar6[-1] = (float)(int)*(short *)(*param_1 + 2 + uVar7 * 8);
        *pfVar6 = (float)(int)*(short *)(*param_1 + 4 + uVar7 * 8);
        iVar1 = uVar7 * 4;
        iVar2 = param_1[3] + uVar7 * 4;
        uVar7 = uVar7 + 1;
        pfVar6[2] = (float)(((uint)*(byte *)(iVar2 + 1) +
                            (uint)*(byte *)(param_1[3] + iVar1) * 0x100) * 0x100 +
                           (uint)*(byte *)(iVar2 + 2));
        pfVar6 = pfVar6 + 8;
      } while (uVar7 < param_1[4]);
    }
    uVar4 = param_1[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 2)) || (uVar4 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
    DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
    *param_1 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(param_1,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a1b770 @ 00a1b770 */
/* ===================================================== */

void FUN_00a1b770(uint *param_1,int param_2,undefined4 param_3)

{
  undefined1 *puVar1;
  byte bVar2;
  byte *pbVar3;
  float *pfVar4;
  uint *puVar5;
  uint uVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  
                    /* 0x1b770  547   */
  puVar5 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar4 = DAT_011be7a8;
    uVar6 = (uint)*(ushort *)(param_2 + 0x1a);
    uVar8 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar8;
      DAT_00a2c488 = uVar6;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar6;
        DAT_011be7ac[DAT_011be7dc] = uVar6;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    }
    uVar6 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar6;
      DAT_011be7ac[DAT_011be7dc] = uVar6;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 7;
    *pfVar4 = (float)(int)*(short *)*param_1;
    pfVar4[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar4[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar4[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar4[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar4[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pbVar3 = (byte *)param_1[3];
    pfVar4[4] = (float)(((uint)*pbVar3 * 0x100 + (uint)pbVar3[1]) * 0x100 + (uint)pbVar3[2]);
    uVar6 = param_1[3];
    pfVar4[0xc] = (float)(((uint)*(byte *)(uVar6 + 4) * 0x100 + (uint)*(byte *)(uVar6 + 5)) * 0x100
                         + (uint)*(byte *)(uVar6 + 6));
    param_1 = (uint *)0x2;
    pfVar4[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x24) * 4);
    pfVar4[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x25) * 4);
    pfVar4[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x18) * 4);
    pfVar4[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x19) * 4);
    uVar6 = puVar5[4];
    if (2 < uVar6) {
      iVar9 = param_2 + 0x50;
      pfVar7 = pfVar4 + 0x10;
      do {
        if ((*(ushort *)(iVar9 + -0x36) != DAT_00a2c488) ||
           (*(ushort *)(iVar9 + -0x42) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 5) || ((DAT_00a2c57c != 2 || (uVar6 == 4)))) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 2;
            DAT_011be7ac[DAT_011be7dc] = 2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
          DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
          uVar8 = (uint)*(ushort *)(iVar9 + 0xe);
          uVar6 = (uint)*(ushort *)(iVar9 + 0x1a);
          if ((uVar6 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar8;
            DAT_00a2c488 = uVar6;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar6)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar6;
              DAT_011be7ac[DAT_011be7dc] = uVar6;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          }
          uVar6 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar6;
            DAT_011be7ac[DAT_011be7dc] = uVar6;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 7;
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x44) * 4);
          pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x43) * 4);
          *pfVar7 = (float)(int)*(short *)*puVar5;
          pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2);
          pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4);
          puVar1 = (undefined1 *)(puVar5[3] + (int)param_1 * 4);
          pfVar7[4] = (float)((uint)CONCAT11(*puVar1,puVar1[1]) * 0x100 + (uint)(byte)puVar1[2]);
          pfVar7[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x38) * 4);
          pfVar7[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x37) * 4);
          pfVar7[8] = (float)(int)*(short *)((*puVar5 - 8) + (int)param_1 * 8);
          pfVar7[9] = (float)(int)*(short *)((*puVar5 - 6) + (int)param_1 * 8);
          pfVar7[10] = (float)(int)*(short *)((*puVar5 - 4) + (int)param_1 * 8);
          puVar1 = (undefined1 *)(puVar5[3] + (int)param_1 * 4);
          pfVar7[0xc] = (float)((uint)CONCAT11(*puVar1,puVar1[1]) * 0x100 + (uint)(byte)puVar1[2]);
          pfVar7 = pfVar7 + 0x10;
        }
        if (((uint)param_1 & 1) == 0) {
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x44) * 4);
          bVar2 = *(byte *)(iVar9 + -0x43);
        }
        else {
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x2c) * 4);
          bVar2 = *(byte *)(iVar9 + -0x2b);
        }
        iVar9 = iVar9 + 0x28;
        pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)bVar2 * 4);
        *pfVar7 = (float)(int)*(short *)(*puVar5 + (int)param_1 * 8);
        pfVar7[1] = (float)(int)*(short *)(*puVar5 + 2 + (int)param_1 * 8);
        pfVar7[2] = (float)(int)*(short *)(*puVar5 + 4 + (int)param_1 * 8);
        puVar1 = (undefined1 *)(puVar5[3] + (int)param_1 * 4);
        param_1 = (uint *)((int)param_1 + 1);
        pfVar7[4] = (float)((uint)CONCAT11(*puVar1,puVar1[1]) * 0x100 + (uint)(byte)puVar1[2]);
        uVar6 = puVar5[4];
        pfVar7 = pfVar7 + 8;
      } while (param_1 < uVar6);
    }
    uVar6 = puVar5[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 2)) || (uVar6 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar6;
    DAT_011be7a8 = DAT_011be7a8 + uVar6 * 8;
    *puVar5 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar5,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a1bf60 @ 00a1bf60 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1bf60(uint *param_1,int param_2,undefined4 param_3)

{
  byte bVar1;
  float *pfVar2;
  uint *puVar3;
  uint uVar4;
  float *pfVar5;
  float *pfVar6;
  uint uVar7;
  int iVar8;
  
                    /* 0x1bf60  545   */
  puVar3 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar2 = DAT_011be7a8;
    uVar4 = (uint)*(ushort *)(param_2 + 0x1a);
    uVar7 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar4 != DAT_00a2c488) || (uVar7 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar7;
      DAT_00a2c488 = uVar4;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar4;
        DAT_011be7ac[DAT_011be7dc] = uVar4;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar7;
    }
    uVar4 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar4;
      DAT_011be7ac[DAT_011be7dc] = uVar4;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 3;
    *pfVar2 = (float)(int)*(short *)*param_1;
    pfVar2[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar2[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar2[3] = (float)(int)*(short *)param_1[1] * _DAT_00a2a0c4;
    pfVar2[4] = (float)(int)*(short *)(param_1[1] + 2) * _DAT_00a2a0c4;
    pfVar2[5] = (float)(int)*(short *)(param_1[1] + 4) * _DAT_00a2a0c4;
    pfVar2[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar2[9] = (float)(int)*(short *)(*param_1 + 10);
    pfVar2[10] = (float)(int)*(short *)(*param_1 + 0xc);
    pfVar2[0xb] = (float)(int)*(short *)(param_1[1] + 8) * _DAT_00a2a0c4;
    pfVar2[0xc] = (float)(int)*(short *)(param_1[1] + 10) * _DAT_00a2a0c4;
    pfVar2[0xd] = (float)(int)*(short *)(param_1[1] + 0xc) * _DAT_00a2a0c4;
    pfVar2[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x24) * 4);
    pfVar2[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x25) * 4);
    pfVar2[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x18) * 4);
    pfVar2[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x19) * 4);
    uVar4 = param_1[4];
    param_1 = (uint *)0x2;
    if (2 < uVar4) {
      pfVar5 = pfVar2 + 0x10;
      iVar8 = param_2 + 0x50;
      do {
        if ((*(ushort *)(iVar8 + -0x36) != DAT_00a2c488) ||
           (pfVar6 = pfVar5, *(ushort *)(iVar8 + -0x42) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 5) || ((DAT_00a2c57c != 1 || (uVar4 == 4)))) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 1;
            DAT_011be7ac[DAT_011be7dc] = 1;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
          DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
          uVar4 = (uint)*(ushort *)(iVar8 + 0x1a);
          uVar7 = (uint)*(ushort *)(iVar8 + 0xe);
          if ((uVar4 != DAT_00a2c488) || (uVar7 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar7;
            DAT_00a2c488 = uVar4;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar4)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar4;
              DAT_011be7ac[DAT_011be7dc] = uVar4;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar7;
          }
          uVar4 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar4;
            DAT_011be7ac[DAT_011be7dc] = uVar4;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 3;
          pfVar5[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x44) * 4);
          pfVar5[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x43) * 4);
          *pfVar5 = (float)(int)*(short *)*puVar3;
          pfVar5[1] = (float)(int)*(short *)(*puVar3 + 2);
          pfVar5[2] = (float)(int)*(short *)(*puVar3 + 4);
          pfVar5[3] = (float)(int)*(short *)puVar3[1] * _DAT_00a2a0c4;
          pfVar5[4] = (float)(int)*(short *)(puVar3[1] + 2) * _DAT_00a2a0c4;
          pfVar5[5] = (float)(int)*(short *)(puVar3[1] + 4) * _DAT_00a2a0c4;
          pfVar5[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x38) * 4);
          pfVar5[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar8 + -0x37) * 4);
          pfVar5[8] = (float)(int)*(short *)((*puVar3 - 8) + (int)param_1 * 8);
          pfVar5[9] = (float)(int)*(short *)((*puVar3 - 6) + (int)param_1 * 8);
          pfVar5[10] = (float)(int)*(short *)((*puVar3 - 4) + (int)param_1 * 8);
          pfVar5[0xb] = (float)(int)*(short *)((puVar3[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar6 = pfVar5 + 0x10;
          pfVar5[0xc] = (float)(int)*(short *)((puVar3[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
          pfVar5[0xd] = (float)(int)*(short *)((puVar3[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        }
        if (((uint)param_1 & 1) == 0) {
          pfVar6[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x44) * 4);
          bVar1 = *(byte *)(iVar8 + -0x43);
        }
        else {
          pfVar6[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar8 + -0x2c) * 4);
          bVar1 = *(byte *)(iVar8 + -0x2b);
        }
        param_1 = (uint *)((int)param_1 + 1);
        iVar8 = iVar8 + 0x28;
        pfVar5 = pfVar6 + 8;
        pfVar6[7] = *(float *)(DAT_011cb1cc + (uint)bVar1 * 4);
        *pfVar6 = (float)(int)*(short *)((*puVar3 - 8) + (int)param_1 * 8);
        pfVar6[1] = (float)(int)*(short *)((*puVar3 - 6) + (int)param_1 * 8);
        pfVar6[2] = (float)(int)*(short *)((*puVar3 - 4) + (int)param_1 * 8);
        pfVar6[3] = (float)(int)*(short *)((puVar3[1] - 8) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar6[4] = (float)(int)*(short *)((puVar3[1] - 6) + (int)param_1 * 8) * _DAT_00a2a0c4;
        pfVar6[5] = (float)(int)*(short *)((puVar3[1] - 4) + (int)param_1 * 8) * _DAT_00a2a0c4;
        uVar4 = puVar3[4];
      } while (param_1 < uVar4);
    }
    uVar4 = puVar3[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 1)) || (uVar4 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 1;
      DAT_011be7ac[DAT_011be7dc] = 1;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar4;
    DAT_011be7a8 = DAT_011be7a8 + uVar4 * 8;
    *puVar3 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar3,param_2,param_3,0x18);
  return;
}



/* ===================================================== */
/* FUN_00a1c800 @ 00a1c800 */
/* ===================================================== */

void FUN_00a1c800(uint *param_1,int param_2,undefined4 param_3)

{
  byte bVar1;
  short sVar2;
  float *pfVar3;
  uint *puVar4;
  uint uVar5;
  float *pfVar6;
  float *pfVar7;
  uint uVar8;
  int iVar9;
  
                    /* 0x1c800  548   */
  puVar4 = param_1;
  if (0 < (int)*param_1) {
    FUN_00a0c480(param_1[4]);
    *DAT_011be7ac = *param_1;
    pfVar3 = DAT_011be7a8;
    uVar5 = (uint)*(ushort *)(param_2 + 0x16);
    uVar8 = (uint)*(ushort *)(param_2 + 0xe);
    if ((uVar5 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
      DAT_00a2c484 = uVar8;
      DAT_00a2c488 = uVar5;
      if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar5)) {
        DAT_00a2c578 = -4;
        DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_00a2c57c = uVar5;
        DAT_011be7ac[DAT_011be7dc] = uVar5;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0;
        DAT_011be7dc = DAT_011be7dc + 1;
        DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
      }
      DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
    }
    uVar5 = DAT_00a2c5a4;
    if ((((DAT_00a2c480 ^ 1) & 9) != 0) && ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4))))
    {
      DAT_00a2c578 = -2;
      DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = uVar5;
      DAT_011be7ac[DAT_011be7dc] = uVar5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_00a2c480 = 7;
    *pfVar3 = (float)(int)*(short *)*param_1;
    pfVar3[1] = (float)(int)*(short *)(*param_1 + 2);
    pfVar3[2] = (float)(int)*(short *)(*param_1 + 4);
    pfVar3[8] = (float)(int)*(short *)(*param_1 + 8);
    pfVar3[9] = (float)(int)*(short *)(*param_1 + 10);
    sVar2 = *(short *)(*param_1 + 0xc);
    pfVar3[4] = 1.180104e-38;
    pfVar3[0xc] = 1.180104e-38;
    pfVar3[10] = (float)(int)sVar2;
    pfVar3[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x1c) * 4);
    pfVar3[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x1d) * 4);
    pfVar3[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(param_2 + 0x14) * 4);
    pfVar3[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(param_2 + 0x15) * 4);
    uVar5 = param_1[4];
    param_1 = (uint *)0x2;
    if (2 < uVar5) {
      pfVar6 = pfVar3 + 0x10;
      iVar9 = param_2 + 0x40;
      do {
        if ((*(ushort *)(iVar9 + -0x2a) != DAT_00a2c488) ||
           (pfVar7 = pfVar6, *(ushort *)(iVar9 + -0x32) != DAT_00a2c484)) {
          if ((DAT_00a2c578 != 5) || ((DAT_00a2c57c != 2 || (uVar5 == 4)))) {
            DAT_00a2c578 = 5;
            DAT_011be7ac[DAT_011be7dc] = 5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = 2;
            DAT_011be7ac[DAT_011be7dc] = 2;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = (uint)pfVar3;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar5;
          DAT_011be7a8 = DAT_011be7a8 + uVar5 * 8;
          uVar5 = (uint)*(ushort *)(iVar9 + 0x16);
          uVar8 = (uint)*(ushort *)(iVar9 + 0xe);
          if ((uVar5 != DAT_00a2c488) || (uVar8 != DAT_00a2c484)) {
            DAT_00a2c484 = uVar8;
            DAT_00a2c488 = uVar5;
            if ((DAT_00a2c578 != -4) || (DAT_00a2c57c != uVar5)) {
              DAT_00a2c578 = -4;
              DAT_011be7ac[DAT_011be7dc] = 0xfffffffc;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_00a2c57c = uVar5;
              DAT_011be7ac[DAT_011be7dc] = uVar5;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0;
              DAT_011be7dc = DAT_011be7dc + 1;
              DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
            }
            DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;
          }
          uVar5 = DAT_00a2c5a4;
          if ((((DAT_00a2c480 ^ 1) & 9) != 0) &&
             ((DAT_00a2c578 != -2 || (DAT_00a2c57c != DAT_00a2c5a4)))) {
            DAT_00a2c578 = -2;
            DAT_011be7ac[DAT_011be7dc] = 0xfffffffe;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_00a2c57c = uVar5;
            DAT_011be7ac[DAT_011be7dc] = uVar5;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0;
            DAT_011be7dc = DAT_011be7dc + 1;
            DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
          }
          DAT_00a2c480 = 7;
          pfVar6[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x34) * 4);
          pfVar6[7] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x33) * 4);
          *pfVar6 = (float)(int)*(short *)*puVar4;
          pfVar6[1] = (float)(int)*(short *)(*puVar4 + 2);
          pfVar6[2] = (float)(int)*(short *)(*puVar4 + 4);
          pfVar6[4] = 1.180104e-38;
          pfVar6[0xe] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x2c) * 4);
          pfVar7 = pfVar6 + 0x10;
          pfVar6[0xf] = *(float *)(DAT_011cb1cc + (uint)*(byte *)(iVar9 + -0x2b) * 4);
          pfVar6[8] = (float)(int)*(short *)((*puVar4 - 8) + (int)param_1 * 8);
          pfVar6[9] = (float)(int)*(short *)((*puVar4 - 6) + (int)param_1 * 8);
          pfVar6[10] = (float)(int)*(short *)((*puVar4 - 4) + (int)param_1 * 8);
          pfVar6[0xc] = 1.180104e-38;
        }
        if (((uint)param_1 & 1) == 0) {
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x34) * 4);
          bVar1 = *(byte *)(iVar9 + -0x33);
        }
        else {
          pfVar7[6] = *(float *)(DAT_011cb1c8 + (uint)*(byte *)(iVar9 + -0x24) * 4);
          bVar1 = *(byte *)(iVar9 + -0x23);
        }
        param_1 = (uint *)((int)param_1 + 1);
        iVar9 = iVar9 + 0x20;
        pfVar6 = pfVar7 + 8;
        pfVar7[7] = *(float *)(DAT_011cb1cc + (uint)bVar1 * 4);
        *pfVar7 = (float)(int)*(short *)((*puVar4 - 8) + (int)param_1 * 8);
        pfVar7[1] = (float)(int)*(short *)((*puVar4 - 6) + (int)param_1 * 8);
        pfVar7[2] = (float)(int)*(short *)((*puVar4 - 4) + (int)param_1 * 8);
        pfVar7[4] = 1.180104e-38;
        uVar5 = puVar4[4];
      } while (param_1 < uVar5);
    }
    uVar5 = puVar4[4];
    if (((DAT_00a2c578 != 5) || (DAT_00a2c57c != 2)) || (uVar5 == 4)) {
      DAT_00a2c578 = 5;
      DAT_011be7ac[DAT_011be7dc] = 5;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_00a2c57c = 2;
      DAT_011be7ac[DAT_011be7dc] = 2;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = (uint)pfVar3;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0;
      DAT_011be7dc = DAT_011be7dc + 1;
      DAT_011be7ac[DAT_011be7dc] = 0xffffffff;
    }
    DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar5;
    DAT_011be7a8 = DAT_011be7a8 + uVar5 * 8;
    *puVar4 = (uint)DAT_011be7ac | 0x80000000;
  }
  FUN_00a14980(puVar4,param_2,param_3,6);
  return;
}



/* ===================================================== */
/* FUN_00a1cf60 @ 00a1cf60 */
/* ===================================================== */

int FUN_00a1cf60(int *param_1,int *param_2)

{
  return param_2[1] * param_1[1] + param_2[2] * param_1[2] + *param_1 * *param_2 >> 0xc;
}



/* ===================================================== */
/* FUN_00a1cf90 @ 00a1cf90 */
/* ===================================================== */

void FUN_00a1cf90(undefined2 param_1,undefined2 param_2,byte param_3,undefined4 param_4,
                 undefined1 param_5)

{
  undefined2 local_14;
  undefined2 local_12;
  undefined2 local_10;
  undefined2 local_e;
  undefined2 local_c;
  undefined2 local_a;
  undefined2 local_8;
  undefined2 local_6;
  byte local_4;
  undefined1 local_3;
  
                    /* 0x1cf90  561   */
  local_e = param_2;
  local_14 = 0;
  local_12 = 0;
  local_c = 0;
  local_a = 0;
  local_10 = param_1;
  local_4 = param_3 & 1;
  local_8 = 0x100;
  local_6 = 0xf0;
  local_3 = param_5;
  FUN_00a05a00(&local_14);
  return;
}



/* ===================================================== */
/* FUN_00a1cff0 @ 00a1cff0 */
/* ===================================================== */

void FUN_00a1cff0(int *param_1)

{
  int *piVar1;
  uint uVar2;
  
                    /* 0x1cff0  563   */
  if (*param_1 != 1) {
    uVar2 = 0;
    *param_1 = 1;
    if (param_1[1] != 0) {
      piVar1 = param_1 + 6;
      do {
        piVar1[-4] = (int)param_1 + piVar1[-4] + 8;
        piVar1[-2] = (int)param_1 + piVar1[-2] + 8;
        *piVar1 = (int)param_1 + *piVar1 + 8;
        piVar1 = piVar1 + 7;
        uVar2 = uVar2 + 1;
      } while (uVar2 < (uint)param_1[1]);
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a1d040 @ 00a1d040 */
/* ===================================================== */

void FUN_00a1d040(int param_1)

{
                    /* 0x1d040  569   */
  FUN_00a061b0(*(undefined4 *)(param_1 + 0x10));
  return;
}



/* ===================================================== */
/* FUN_00a1d050 @ 00a1d050 */
/* ===================================================== */

void FUN_00a1d050(int param_1,int param_2)

{
  int *piVar1;
  int *piVar2;
  int iVar3;
  int iVar4;
  
                    /* 0x1d050  626   */
  iVar4 = 4;
  iVar3 = *(int *)(param_1 + 0xc);
  if (0 < iVar3) {
    piVar2 = (int *)(param_2 + 8);
    do {
      piVar1 = (int *)(param_1 + iVar4 * 4);
      piVar2[1] = param_1;
      *piVar2 = (int)piVar1;
      iVar3 = iVar3 + -1;
      piVar2[2] = param_1 + *(int *)(param_1 + 8);
      iVar4 = iVar4 + 1 + *piVar1;
      piVar2 = piVar2 + 6;
    } while (iVar3 != 0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a1d090 @ 00a1d090 */
/* ===================================================== */

void FUN_00a1d090(int param_1,int param_2,int param_3)

{
                    /* 0x1d090  627  
                       0x1d090  628   */
  *(int *)(param_2 + 8) = param_1 + param_3 * 0x1c;
  FUN_00a0c4f0(param_2);
  return;
}



/* ===================================================== */
/* FUN_00a1d0c0 @ 00a1d0c0 */
/* ===================================================== */

void FUN_00a1d0c0(uint *param_1,int param_2)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  uint *puVar5;
  
                    /* 0x1d0c0  571   */
  puVar2 = (uint *)FUN_00a02ea0(0xf9,0x10);
  puVar4 = puVar2 + 2;
  for (iVar3 = 6; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar4 = *param_1;
    param_1 = param_1 + 1;
    puVar4 = puVar4 + 1;
  }
  puVar4 = &EXT_011d0870;
  puVar5 = puVar2 + 8;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar5 = *puVar4;
    puVar4 = puVar4 + 1;
    puVar5 = puVar5 + 1;
  }
  puVar2[0x10] = DAT_011cb208;
  *puVar2 = (**(uint **)(param_2 + 0x10) ^ *puVar2) & 0xffffff ^ *puVar2;
  uVar1 = **(uint **)(param_2 + 0x10);
  **(uint **)(param_2 + 0x10) = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d130 @ 00a1d130 */
/* ===================================================== */

void FUN_00a1d130(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
                    /* 0x1d130  572   */
  FUN_00a1d150(param_1,param_2,param_3,param_4);
  return;
}



/* ===================================================== */
/* FUN_00a1d150 @ 00a1d150 */
/* ===================================================== */

void FUN_00a1d150(uint *param_1,int param_2)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  uint *puVar5;
  
                    /* 0x1d150  645   */
  puVar2 = (uint *)FUN_00a02ea0(0xe3,0x10);
  puVar2[2] = *param_1;
  puVar2[3] = param_1[1];
  puVar2[4] = param_1[2];
  puVar2[5] = param_1[3];
  puVar4 = &EXT_011d0870;
  puVar5 = puVar2 + 8;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar5 = *puVar4;
    puVar4 = puVar4 + 1;
    puVar5 = puVar5 + 1;
  }
  puVar2[0x10] = DAT_011cb208;
  *puVar2 = (**(uint **)(param_2 + 0x10) ^ *puVar2) & 0xffffff ^ *puVar2;
  uVar1 = **(uint **)(param_2 + 0x10);
  **(uint **)(param_2 + 0x10) = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d1d0 @ 00a1d1d0 */
/* ===================================================== */

void FUN_00a1d1d0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
                    /* 0x1d1d0  573   */
  FUN_00a1d1f0(param_1,param_2,param_3,param_4);
  return;
}



/* ===================================================== */
/* FUN_00a1d1f0 @ 00a1d1f0 */
/* ===================================================== */

void FUN_00a1d1f0(uint *param_1,int param_2)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  uint *puVar5;
  
                    /* 0x1d1f0  574   */
  puVar2 = (uint *)FUN_00a02ea0(0xe4,0x10);
  puVar4 = puVar2 + 2;
  for (iVar3 = 5; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar4 = *param_1;
    param_1 = param_1 + 1;
    puVar4 = puVar4 + 1;
  }
  puVar4 = &EXT_011d0870;
  puVar5 = puVar2 + 8;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar5 = *puVar4;
    puVar4 = puVar4 + 1;
    puVar5 = puVar5 + 1;
  }
  puVar2[0x10] = DAT_011cb208;
  *puVar2 = (**(uint **)(param_2 + 0x10) ^ *puVar2) & 0xffffff ^ *puVar2;
  uVar1 = **(uint **)(param_2 + 0x10);
  **(uint **)(param_2 + 0x10) = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d260 @ 00a1d260 */
/* ===================================================== */

void FUN_00a1d260(uint *param_1,int param_2,uint param_3)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  
                    /* 0x1d260  575   */
  puVar2 = (uint *)FUN_00a02ea0(0xe5,10);
  uVar1 = *puVar2;
  puVar4 = puVar2 + 2;
  for (iVar3 = 9; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar4 = *param_1;
    param_1 = param_1 + 1;
    puVar4 = puVar4 + 1;
  }
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar2 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ uVar1) & 0xffffff ^ uVar1;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar1 = *puVar4;
  *puVar4 = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d2c0 @ 00a1d2c0 */
/* ===================================================== */

void FUN_00a1d2c0(uint *param_1,int param_2,uint param_3)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  uint *puVar5;
  
                    /* 0x1d2c0  576   */
  puVar2 = (uint *)FUN_00a02ea0(0xe5,10);
  puVar4 = puVar2 + 2;
  puVar5 = puVar4;
  for (iVar3 = 9; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar5 = *param_1;
    param_1 = param_1 + 1;
    puVar5 = puVar5 + 1;
  }
  *(undefined2 *)((int)puVar2 + 0x22) = 0;
  *(undefined2 *)(puVar2 + 8) = 0;
  *puVar4 = *puVar4 & 0xf73fffff;
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar2 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ *puVar2) & 0xffffff ^ *puVar2;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar1 = *puVar4;
  *puVar4 = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d330 @ 00a1d330 */
/* ===================================================== */

void FUN_00a1d330(uint *param_1,int param_2,uint param_3)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  uint *puVar5;
  
                    /* 0x1d330  577   */
  puVar2 = (uint *)FUN_00a02ea0(0xe5,10);
  puVar4 = puVar2 + 2;
  puVar5 = puVar4;
  for (iVar3 = 9; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar5 = *param_1;
    param_1 = param_1 + 1;
    puVar5 = puVar5 + 1;
  }
  *(undefined2 *)((int)puVar2 + 0x22) = 0;
  *(undefined2 *)(puVar2 + 8) = 0;
  *puVar4 = *puVar4 & 0xf7ffffff;
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar2 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ *puVar2) & 0xffffff ^ *puVar2;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar1 = *puVar4;
  *puVar4 = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d3a0 @ 00a1d3a0 */
/* ===================================================== */

void FUN_00a1d3a0(uint *param_1,int param_2,uint param_3)

{
  byte *pbVar1;
  int iVar2;
  uint *puVar3;
  undefined4 *puVar4;
  int iVar5;
  uint *puVar6;
  short *psVar7;
  uint uVar8;
  undefined4 *puVar9;
  uint uVar10;
  uint *puVar11;
  undefined4 *puVar12;
  
                    /* 0x1d3a0  585   */
  iVar2 = -1;
  pbVar1 = (byte *)param_1[5];
  uVar10 = (uint)pbVar1[1] * (uint)*pbVar1;
  if (uVar10 != 0) {
    psVar7 = *(short **)(pbVar1 + 0xc);
    uVar8 = uVar10;
    do {
      if (iVar2 < *psVar7) {
        iVar2 = (int)*psVar7;
      }
      psVar7 = psVar7 + 1;
      uVar8 = uVar8 - 1;
    } while (uVar8 != 0);
  }
  puVar3 = (uint *)FUN_00a02ea0(0xe8,((uVar10 + (iVar2 + 1U) * 4) * 2 + 0x34 >> 2) + 1);
  puVar4 = (undefined4 *)(uVar10 * 2 + (int)(puVar3 + 0xf));
  puVar6 = param_1;
  puVar11 = puVar3 + 2;
  for (iVar5 = 9; iVar5 != 0; iVar5 = iVar5 + -1) {
    *puVar11 = *puVar6;
    puVar6 = puVar6 + 1;
    puVar11 = puVar11 + 1;
  }
  puVar6 = (uint *)param_1[5];
  puVar3[0xb] = *puVar6;
  puVar3[0xc] = puVar6[1];
  puVar3[0xd] = puVar6[2];
  puVar3[0xe] = puVar6[3];
  puVar6 = *(uint **)(param_1[5] + 0xc);
  puVar11 = puVar3 + 0xf;
  for (uVar8 = uVar10 >> 1; uVar8 != 0; uVar8 = uVar8 - 1) {
    *puVar11 = *puVar6;
    puVar6 = puVar6 + 1;
    puVar11 = puVar11 + 1;
  }
  for (uVar10 = uVar10 * 2 & 3; uVar10 != 0; uVar10 = uVar10 - 1) {
    *(char *)puVar11 = (char)*puVar6;
    puVar6 = (uint *)((int)puVar6 + 1);
    puVar11 = (uint *)((int)puVar11 + 1);
  }
  puVar9 = *(undefined4 **)(param_1[5] + 8);
  puVar12 = puVar4;
  for (iVar2 = (iVar2 + 1U & 0x1fffffff) << 1; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar12 = *puVar9;
    puVar9 = puVar9 + 1;
    puVar12 = puVar12 + 1;
  }
  for (iVar2 = 0; iVar2 != 0; iVar2 = iVar2 + -1) {
    *(undefined1 *)puVar12 = *(undefined1 *)puVar9;
    puVar9 = (undefined4 *)((int)puVar9 + 1);
    puVar12 = (undefined4 *)((int)puVar12 + 1);
  }
  puVar3[7] = (uint)(puVar3 + 0xb);
  puVar3[0xe] = (uint)(puVar3 + 0xf);
  puVar3[0xd] = (uint)puVar4;
  iVar2 = (param_3 & 0xffff) * 4;
  *puVar3 = (*(uint *)(iVar2 + *(int *)(param_2 + 0x10)) ^ *puVar3) & 0xffffff ^ *puVar3;
  puVar6 = (uint *)(iVar2 + *(int *)(param_2 + 0x10));
  uVar10 = *puVar6;
  *puVar6 = ((uint)puVar3 ^ uVar10) & 0xffffff ^ uVar10;
  return;
}



/* ===================================================== */
/* FUN_00a1d4d0 @ 00a1d4d0 */
/* ===================================================== */

void FUN_00a1d4d0(uint *param_1,undefined4 param_2,undefined4 param_3)

{
  undefined2 uVar1;
  uint uVar2;
  uint uVar3;
  
                    /* 0x1d4d0  586   */
  uVar2 = *param_1;
  uVar3 = param_1[6];
  uVar1 = *(undefined2 *)((int)param_1 + 0x1a);
  *(undefined2 *)((int)param_1 + 0x1a) = 0;
  *(undefined2 *)(param_1 + 6) = 0;
  *param_1 = uVar2 & 0xf7ffffff;
  FUN_00a1d3a0(param_1,param_2,param_3);
  *param_1 = uVar2;
  *(short *)(param_1 + 6) = (short)uVar3;
  *(undefined2 *)((int)param_1 + 0x1a) = uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d520 @ 00a1d520 */
/* ===================================================== */

void FUN_00a1d520(int param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
  undefined1 uVar1;
  undefined1 uVar2;
  undefined1 *puVar3;
  
                    /* 0x1d520  588  
                       0x1d520  590   */
  puVar3 = *(undefined1 **)(param_1 + 0x14);
  uVar1 = puVar3[1];
  uVar2 = *puVar3;
  puVar3[1] = 0x10;
  **(undefined1 **)(param_1 + 0x14) = 0x10;
  FUN_00a1d4d0(param_1,param_3,param_4);
  **(undefined1 **)(param_1 + 0x14) = uVar2;
  *(undefined1 *)(*(int *)(param_1 + 0x14) + 1) = uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d570 @ 00a1d570 */
/* ===================================================== */

void FUN_00a1d570(uint *param_1,int param_2,uint param_3)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  
                    /* 0x1d570  591   */
  puVar2 = (uint *)FUN_00a02ea0((*param_1 >> 0x1e & 1) * 2 + 0x40,3);
  *(short *)(puVar2 + 2) = (short)param_1[1];
  *(undefined2 *)((int)puVar2 + 10) = *(undefined2 *)((int)param_1 + 6);
  *(short *)(puVar2 + 3) = (short)param_1[2];
  *(undefined2 *)((int)puVar2 + 0xe) = *(undefined2 *)((int)param_1 + 10);
  *(char *)(puVar2 + 1) = (char)param_1[3];
  *(undefined1 *)((int)puVar2 + 5) = *(undefined1 *)((int)param_1 + 0xd);
  *(undefined1 *)((int)puVar2 + 6) = *(undefined1 *)((int)param_1 + 0xe);
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar2 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ *puVar2) & 0xffffff ^ *puVar2;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar1 = *puVar4;
  *puVar4 = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d600 @ 00a1d600 */
/* ===================================================== */

void FUN_00a1d600(uint *param_1,int param_2,uint param_3)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  
                    /* 0x1d600  592   */
  puVar2 = (uint *)FUN_00a02ea0((*param_1 >> 0x1e & 1) * 2 + 0x50,4);
  *(short *)(puVar2 + 2) = (short)param_1[1];
  *(undefined2 *)((int)puVar2 + 10) = *(undefined2 *)((int)param_1 + 6);
  *(short *)(puVar2 + 4) = (short)param_1[2];
  *(undefined2 *)((int)puVar2 + 0x12) = *(undefined2 *)((int)param_1 + 10);
  *(char *)(puVar2 + 1) = (char)param_1[3];
  *(undefined1 *)((int)puVar2 + 5) = *(undefined1 *)((int)param_1 + 0xd);
  *(undefined1 *)((int)puVar2 + 6) = *(undefined1 *)((int)param_1 + 0xe);
  *(undefined1 *)(puVar2 + 3) = *(undefined1 *)((int)param_1 + 0xf);
  *(char *)((int)puVar2 + 0xd) = (char)param_1[4];
  *(undefined1 *)((int)puVar2 + 0xe) = *(undefined1 *)((int)param_1 + 0x11);
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar2 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ *puVar2) & 0xffffff ^ *puVar2;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar1 = *puVar4;
  *puVar4 = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d6a0 @ 00a1d6a0 */
/* ===================================================== */

void FUN_00a1d6a0(uint *param_1,int param_2,uint param_3)

{
  uint uVar1;
  uint *puVar2;
  int iVar3;
  uint *puVar4;
  
                    /* 0x1d6a0  593   */
  puVar2 = (uint *)FUN_00a02ea0((*param_1 >> 0x1e & 1) * 2 + 0x60,3);
  *(short *)(puVar2 + 2) = (short)param_1[1];
  *(undefined2 *)((int)puVar2 + 10) = *(undefined2 *)((int)param_1 + 6);
  *(short *)(puVar2 + 3) = (short)param_1[2];
  *(undefined2 *)((int)puVar2 + 0xe) = *(undefined2 *)((int)param_1 + 10);
  *(char *)(puVar2 + 1) = (char)param_1[3];
  *(undefined1 *)((int)puVar2 + 5) = *(undefined1 *)((int)param_1 + 0xd);
  *(undefined1 *)((int)puVar2 + 6) = *(undefined1 *)((int)param_1 + 0xe);
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar2 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ *puVar2) & 0xffffff ^ *puVar2;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar1 = *puVar4;
  *puVar4 = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d730 @ 00a1d730 */
/* ===================================================== */

void FUN_00a1d730(uint *param_1,int param_2,uint param_3)

{
  uint *puVar1;
  uint uVar2;
  int iVar3;
  uint *puVar4;
  
                    /* 0x1d730  594   */
  uVar2 = *param_1;
  puVar1 = (uint *)FUN_00a02ea0(0xfa,uVar2 >> 0x18);
  puVar4 = puVar1;
  for (uVar2 = (uVar2 >> 0x18) * 4 + 4 >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
    *puVar4 = *param_1;
    param_1 = param_1 + 1;
    puVar4 = puVar4 + 1;
  }
  for (iVar3 = 0; iVar3 != 0; iVar3 = iVar3 + -1) {
    *(char *)puVar4 = (char)*param_1;
    param_1 = (uint *)((int)param_1 + 1);
    puVar4 = (uint *)((int)puVar4 + 1);
  }
  iVar3 = (param_3 & 0xffff) * 4;
  *puVar1 = (*(uint *)(*(int *)(param_2 + 0x10) + iVar3) ^ *puVar1) & 0xffffff ^ *puVar1;
  puVar4 = (uint *)(iVar3 + *(int *)(param_2 + 0x10));
  uVar2 = *puVar4;
  *puVar4 = ((uint)puVar1 ^ uVar2) & 0xffffff ^ uVar2;
  return;
}



/* ===================================================== */
/* FUN_00a1d7a0 @ 00a1d7a0 */
/* ===================================================== */

void FUN_00a1d7a0(uint param_1,uint param_2,undefined4 *param_3)

{
                    /* 0x1d7a0  595   */
  param_3[2] = param_1 & 0xffff;
  param_3[3] = param_2 & 0xffff;
  param_3[4] = param_3[1];
  FUN_00a06110(param_3[1],1 << ((byte)*param_3 & 0x1f));
  return;
}



/* ===================================================== */
/* FUN_00a1d7e0 @ 00a1d7e0 */
/* ===================================================== */

void FUN_00a1d7e0(undefined4 *param_1,int param_2)

{
  uint uVar1;
  int iVar2;
  
                    /* 0x1d7e0  596   */
  iVar2 = 1 << ((byte)*param_1 & 0x1f);
  uVar1 = *(uint *)(param_1[4] + iVar2 * 4);
  *(uint *)(param_1[4] + iVar2 * 4) = (**(uint **)(param_2 + 0x10) ^ uVar1) & 0xffffff ^ uVar1;
  uVar1 = **(uint **)(param_2 + 0x10);
  **(uint **)(param_2 + 0x10) = (param_1[4] ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d830 @ 00a1d830 */
/* ===================================================== */

void FUN_00a1d830(undefined4 *param_1,undefined4 *param_2)

{
                    /* 0x1d830  597   */
  *param_2 = *param_1;
  param_2[2] = param_1[2];
  param_2[3] = param_1[3];
  param_2[4] = param_1[4];
  *param_1 = 0;
  param_1[4] = 0;
  return;
}



/* ===================================================== */
/* FUN_00a1d860 @ 00a1d860 */
/* ===================================================== */

void FUN_00a1d860(undefined1 param_1,undefined1 param_2,undefined1 param_3,int param_4)

{
  uint uVar1;
  uint *puVar2;
  
                    /* 0x1d860  599   */
  puVar2 = (uint *)FUN_00a02ea0(0xf2,1);
  *(undefined1 *)(puVar2 + 1) = param_1;
  *(undefined1 *)((int)puVar2 + 5) = param_2;
  *(undefined1 *)((int)puVar2 + 6) = param_3;
  *puVar2 = (**(uint **)(param_4 + 0x10) ^ *puVar2) & 0xffffff ^ *puVar2;
  uVar1 = **(uint **)(param_4 + 0x10);
  **(uint **)(param_4 + 0x10) = ((uint)puVar2 ^ uVar1) & 0xffffff ^ uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a1d8b0 @ 00a1d8b0 */
/* ===================================================== */

void FUN_00a1d8b0(uint *param_1,uint *param_2)

{
  uint uVar1;
  int iVar2;
  uint *puVar3;
  
                    /* 0x1d8b0  600   */
  if ((*param_1 & 8) == 0) {
    puVar3 = (uint *)0x0;
    iVar2 = 1;
    *param_2 = 0;
    *(undefined2 *)(param_2 + 4) = 0;
    *(undefined2 *)((int)param_2 + 0x12) = 0;
    *(undefined2 *)(param_2 + 5) = 0;
    *(undefined2 *)((int)param_2 + 0x16) = 0;
  }
  else {
    *param_2 = *param_1;
    uVar1 = param_1[1];
    *(short *)(param_2 + 4) = (short)param_1[2];
    *(undefined2 *)((int)param_2 + 0x12) = *(undefined2 *)((int)param_1 + 10);
    *(short *)(param_2 + 5) = (short)param_1[3];
    *(undefined2 *)((int)param_2 + 0x16) = *(undefined2 *)((int)param_1 + 0xe);
    iVar2 = (uVar1 >> 2) + 1;
    puVar3 = param_1 + 4;
  }
  param_2[6] = (uint)puVar3;
  *(short *)(param_2 + 1) = (short)param_1[iVar2 + 1];
  *(undefined2 *)((int)param_2 + 6) = *(undefined2 *)((int)param_1 + iVar2 * 4 + 6);
  *(short *)(param_2 + 2) = (short)param_1[iVar2 + 2];
  *(undefined2 *)((int)param_2 + 10) = *(undefined2 *)((int)param_1 + iVar2 * 4 + 10);
  param_2[3] = (uint)(param_1 + iVar2 + 3);
  return;
}



/* ===================================================== */
/* FUN_00a1d940 @ 00a1d940 */
/* ===================================================== */

void FUN_00a1d940(void)

{
                    /* 0x1d940  602   */
  DAT_011cb20c = 1 - DAT_011cb20c;
  return;
}



/* ===================================================== */
/* FUN_00a1d960 @ 00a1d960 */
/* ===================================================== */

void FUN_00a1d960(void)

{
                    /* 0x1d960  498  
                       0x1d960  499  
                       0x1d960  610   */
  return;
}



/* ===================================================== */
/* FUN_00a1d970 @ 00a1d970 */
/* ===================================================== */

void FUN_00a1d970(undefined4 param_1,undefined4 *param_2)

{
  int iVar1;
  undefined4 *puVar2;
  undefined4 *puVar3;
  
                    /* 0x1d970  618   */
  param_2[0x12] = param_1;
  puVar2 = &DAT_00a2c5d0;
  puVar3 = param_2;
  for (iVar1 = 8; puVar3 = puVar3 + 1, iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar3 = *puVar2;
    puVar2 = puVar2 + 1;
  }
  puVar3 = &DAT_00a2c5d0;
  puVar2 = param_2 + 9;
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = *puVar3;
    puVar3 = puVar3 + 1;
    puVar2 = puVar2 + 1;
  }
  *param_2 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a1d9b0 @ 00a1d9b0 */
/* ===================================================== */

void FUN_00a1d9b0(undefined4 *param_1,undefined4 *param_2)

{
  int *piVar1;
  undefined4 *puVar2;
  int iVar3;
  undefined4 *puVar4;
  
                    /* 0x1d9b0  623   */
  piVar1 = (int *)param_1[0x12];
  if ((piVar1 != (int *)0x0) && (*piVar1 == 0)) {
    FUN_00a1d9b0(piVar1,param_2);
    FUN_00a1e010(param_1[0x12] + 4,param_1 + 1,param_2);
    param_2[5] = *(undefined4 *)(param_1[0x12] + 0x18);
    param_2[6] = *(undefined4 *)(param_1[0x12] + 0x1c);
    param_2[7] = *(undefined4 *)(param_1[0x12] + 0x20);
    puVar2 = (undefined4 *)param_1[0x12];
    puVar4 = param_1 + 9;
    for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
      *puVar4 = *param_2;
      param_2 = param_2 + 1;
      puVar4 = puVar4 + 1;
    }
    *param_1 = 5;
    *puVar2 = 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a1da20 @ 00a1da20 */
/* ===================================================== */

void FUN_00a1da20(uint *param_1,undefined4 *param_2)

{
  uint uVar1;
  int iVar2;
  uint *puVar3;
  undefined4 *puVar4;
  undefined4 *puVar5;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x1da20  624   */
  uVar1 = *param_1;
  if (uVar1 != 1) {
    puVar4 = &DAT_011cb1e8;
    puVar5 = param_2;
    for (iVar2 = 8; iVar2 != 0; iVar2 = iVar2 + -1) {
      *puVar5 = *puVar4;
      puVar4 = puVar4 + 1;
      puVar5 = puVar5 + 1;
    }
    if (uVar1 < 2) {
      FUN_00a1fba0(&DAT_011cb1e8,param_1 + 6,&local_10);
      param_2[5] = param_2[5] + local_10;
      param_2[7] = param_2[7] + local_8;
      param_2[6] = param_2[6] + local_c;
      puVar3 = param_1 + 1;
    }
    else {
      FUN_00a1fba0(&DAT_011cb1e8,param_1 + 0xe,&local_10);
      param_2[5] = param_2[5] + local_10;
      param_2[7] = param_2[7] + local_8;
      param_2[6] = param_2[6] + local_c;
      puVar3 = param_1 + 9;
    }
    FUN_00a1e720(param_2,puVar3);
    *param_1 = 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a1dae0 @ 00a1dae0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1dae0(int param_1)

{
  int iVar1;
  undefined4 uVar2;
  float10 fVar3;
  undefined1 local_40 [64];
  
                    /* 0x1dae0  631   */
  _DAT_00a2c5f0 = param_1;
  iVar1 = *DAT_011be760;
  fVar3 = (float10)fpatan((float10)((int)DAT_011be6bc / 2) / (float10)_DAT_00a2c5f8,(float10)1);
  uVar2 = ProjectionMatrix(local_40,(float)param_1,(float)_DAT_00a2c5f4,(float)(fVar3 + fVar3));
  (**(code **)(iVar1 + 0x78))(uVar2);
  return;
}



/* ===================================================== */
/* FUN_00a1db50 @ 00a1db50 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1db50(int param_1)

{
  int iVar1;
  undefined4 uVar2;
  float10 fVar3;
  undefined1 local_40 [64];
  
                    /* 0x1db50  632   */
  _DAT_00a2c5f4 = param_1;
  iVar1 = *DAT_011be760;
  fVar3 = (float10)fpatan((float10)((int)DAT_011be6bc / 2) / (float10)_DAT_00a2c5f8,(float10)1);
  uVar2 = ProjectionMatrix(local_40,(float)_DAT_00a2c5f0,(float)param_1,(float)(fVar3 + fVar3));
  (**(code **)(iVar1 + 0x78))(uVar2);
  return;
}



/* ===================================================== */
/* FUN_00a1dbc0 @ 00a1dbc0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1dbc0(int param_1)

{
  int iVar1;
  undefined4 uVar2;
  float10 fVar3;
  undefined1 local_40 [64];
  
                    /* 0x1dbc0  564   */
  _DAT_00a2c5f8 = param_1;
  iVar1 = *DAT_011be760;
  fVar3 = (float10)fpatan((float10)((int)DAT_011be6bc / 2) / (float10)param_1,(float10)1);
  uVar2 = ProjectionMatrix(local_40,(float)_DAT_00a2c5f0,(float)_DAT_00a2c5f4,(float)(fVar3 + fVar3)
                          );
  (**(code **)(iVar1 + 0x78))(uVar2);
  return;
}



/* ===================================================== */
/* FUN_00a1dc30 @ 00a1dc30 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_00a1dc30(int *param_1)

{
  undefined2 local_60 [2];
  undefined2 local_5c;
  undefined2 local_58;
  undefined2 local_50 [2];
  undefined2 local_4c;
  undefined2 local_48;
  undefined2 local_40 [2];
  undefined2 local_3c;
  undefined2 local_38;
  int local_30;
  int local_2c;
  int local_28;
  int local_20;
  int local_1c;
  int local_18;
  undefined4 local_10;
  undefined4 local_c;
  undefined4 local_8;
  
                    /* 0x1dc30  633   */
  local_10 = 0;
  local_c = 0x1000;
  local_8 = 0;
  local_30 = *param_1;
  local_2c = param_1[1];
  local_28 = param_1[2];
  local_20 = param_1[3] - local_30;
  local_1c = param_1[4] - local_2c;
  local_18 = param_1[5] - local_28;
  FUN_00a25910(&local_20,local_40);
  FUN_00a24500(&local_10,local_40,local_60);
  FUN_00a24500(local_40,local_60,local_50);
  FUN_00a25910(local_60,local_60);
  FUN_00a25910(local_50,local_50);
  DAT_011cb1e8._0_2_ = local_60[0];
  DAT_011cb1ec._2_2_ = local_5c;
  DAT_011cb1e8._2_2_ = local_50[0];
  _DAT_011cb1f0 = local_4c;
  _DAT_011cb1f4 = local_58;
  DAT_011cb1ec._0_2_ = local_40[0];
  _DAT_011cb1f2 = local_3c;
  _DAT_011cb1f6 = local_48;
  _DAT_011cb1f8 = local_38;
  _DAT_011cb1fc = FUN_00a1cf60(local_60,&local_30);
  _DAT_011cb1fc = -_DAT_011cb1fc;
  _DAT_011cb200 = FUN_00a1cf60(local_50,&local_30);
  _DAT_011cb200 = -_DAT_011cb200;
  _DAT_011cb204 = FUN_00a1cf60(local_40,&local_30);
  _DAT_011cb204 = -_DAT_011cb204;
  if (param_1[6] != 0) {
    FUN_00a202d0(-param_1[6],&DAT_011cb1e8);
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a1dda0 @ 00a1dda0 */
/* ===================================================== */

void FUN_00a1dda0(undefined4 param_1)

{
                    /* 0x1dda0  634   */
  FUN_00a1dc30(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a1ddb0 @ 00a1ddb0 */
/* ===================================================== */

void FUN_00a1ddb0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &EXT_011d0870;
                    /* 0x1ddb0  636   */
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = *param_1;
    param_1 = param_1 + 1;
    puVar2 = puVar2 + 1;
  }
  DAT_011cb208 = DAT_011cb208 + 1;
  return;
}



/* ===================================================== */
/* FUN_00a1dde0 @ 00a1dde0 */
/* ===================================================== */

undefined4 FUN_00a1dde0(void)

{
                    /* 0x1dde0  639   */
  return VSyncCounter;
}



/* ===================================================== */
/* FUN_00a1ddf0 @ 00a1ddf0 */
/* ===================================================== */

void FUN_00a1ddf0(void)

{
                    /* 0x1ddf0  638  
                       0x1ddf0  640   */
  VSyncCounter = 0;
  return;
}



/* ===================================================== */
/* FUN_00a1de00 @ 00a1de00 */
/* ===================================================== */

void FUN_00a1de00(undefined4 param_1)

{
                    /* 0x1de00  642   */
  FUN_00a1d040(param_1);
  FUN_00a02e20(0);
  return;
}



/* ===================================================== */
/* FUN_00a1de20 @ 00a1de20 */
/* ===================================================== */

undefined4 FUN_00a1de20(void)

{
                    /* 0x1de20  643   */
  return DAT_00dedbd8;
}



/* ===================================================== */
/* FUN_00a1de30 @ 00a1de30 */
/* ===================================================== */

void FUN_00a1de30(void)

{
                    /* 0x1de30  583  
                       0x1de30  647   */
  return;
}



/* ===================================================== */
/* FUN_00a1de40 @ 00a1de40 */
/* ===================================================== */

void FUN_00a1de40(void)

{
                    /* 0x1de40  181  
                       0x1de40  185  
                       0x1de40  186  
                       0x1de40  187  
                       0x1de40  188  
                       0x1de40  189  
                       0x1de40  250  
                       0x1de40  251  
                       0x1de40  256  
                       0x1de40  293  
                       0x1de40  381  
                       0x1de40  493  
                       0x1de40  507  
                       0x1de40  579  
                       0x1de40  581  
                       0x1de40  584  
                       0x1de40  598  
                       0x1de40  641  
                       0x1de40  646  
                       0x1de40  648   */
  return;
}



/* ===================================================== */
/* FUN_00a1de50 @ 00a1de50 */
/* ===================================================== */

void FUN_00a1de50(void)

{
                    /* 0x1de50  601   */
  if ((DAT_00dedc44 == 0) && (VSyncDelayed == 0)) {
    (**(code **)(*DAT_011be760 + 0xb8))();
  }
  FUN_00a049c0(1);
  return;
}



/* ===================================================== */
/* FUN_00a1de80 @ 00a1de80 */
/* ===================================================== */

undefined4 FUN_00a1de80(void)

{
                    /* 0x1de80  565   */
  return 1;
}



/* ===================================================== */
/* FUN_00a1de90 @ 00a1de90 */
/* ===================================================== */

void FUN_00a1de90(uint param_1)

{
                    /* 0x1de90  566   */
  (**(code **)(*DAT_011be760 + 0xa8))(0x1c,param_1 & 1);
  return;
}



/* ===================================================== */
/* FUN_00a1deb0 @ 00a1deb0 */
/* ===================================================== */

void FUN_00a1deb0(uint param_1,uint param_2,int param_3)

{
                    /* 0x1deb0  568   */
  (**(code **)(*DAT_011be760 + 0x80))
            (((param_1 & 0xfffffff0) << 8 | param_2 & 0xfffffff0) << 4 | param_3 >> 4);
  return;
}



/* ===================================================== */
/* FUN_00a1def0 @ 00a1def0 */
/* ===================================================== */

undefined4 FUN_00a1def0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x1def0  650   */
  return param_2;
}



/* ===================================================== */
/* FUN_00a1df00 @ 00a1df00 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a1df00(void)

{
  undefined2 uVar1;
  undefined4 uVar2;
  int iVar3;
  undefined2 *puVar4;
  int iVar5;
  undefined4 *puVar6;
  undefined2 *puVar7;
  undefined4 *puVar8;
  int local_14;
  
                    /* 0x1df00  337   */
  iVar5 = 0;
  puVar6 = &EXT_011d0870;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar6 = 0;
    puVar6 = puVar6 + 1;
  }
  DAT_011f0ecc = 0;
  local_14 = 0;
  puVar6 = &DAT_011cb420;
  do {
    fsin((float10)local_14 * (float10)_DAT_00a2a0d8);
    uVar2 = __ftol();
    *puVar6 = uVar2;
    puVar6 = puVar6 + 1;
    local_14 = local_14 + 1;
  } while ((int)puVar6 < 0x11d0420);
  puVar4 = &DAT_011cb210;
  puVar7 = &DAT_011f0c90;
  do {
    uVar1 = __ftol();
    (&DAT_011d0620)[iVar5] = uVar1;
    uVar1 = __ftol();
    (&DAT_011d0420)[iVar5] = uVar1;
    uVar1 = __ftol();
    *puVar7 = uVar1;
    uVar1 = __ftol();
    *puVar4 = uVar1;
    puVar7 = puVar7 + 1;
    iVar5 = iVar5 + 1;
    puVar4 = puVar4 + 1;
  } while ((int)puVar7 < 0x11f0e90);
  puVar6 = &DAT_00a2c600;
  puVar8 = &EXT_011d0870;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar8 = *puVar6;
    puVar6 = puVar6 + 1;
    puVar8 = puVar8 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a1e010 @ 00a1e010 */
/* ===================================================== */

undefined2 * FUN_00a1e010(ulonglong *param_1,uint *param_2,undefined2 *param_3)

{
  ulonglong uVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  ulonglong uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  ulonglong uVar8;
  
                    /* 0x1e010  338   */
  if (MMXInstalled == 0) {
    *param_3 = (short)((int)(short)param_2[3] * (int)*(short *)((int)param_1 + 4) +
                       (int)*(short *)((int)param_1 + 2) * (int)*(short *)((int)param_2 + 6) +
                       (int)(short)*param_2 * (int)(short)*param_1 >> 0xc);
    param_3[3] = (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_1 + 10) * (int)(short)param_2[3] +
                         (int)*(short *)((int)param_2 + 6) * (int)(short)param_1[1] >> 0xc);
    param_3[6] = (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 0xc) +
                         (int)*(short *)((int)param_1 + 0xe) * (int)*(short *)((int)param_2 + 6) +
                         (int)(short)param_1[2] * (int)(short)param_2[3] >> 0xc);
    param_3[1] = (short)((int)(short)*param_1 * (int)*(short *)((int)param_2 + 2) +
                         (int)(short)param_2[2] * (int)*(short *)((int)param_1 + 2) +
                         (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 4) >>
                        0xc);
    param_3[4] = (short)((int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 10) +
                         (int)(short)param_2[2] * (int)(short)param_1[1] >> 0xc);
    param_3[7] = (short)((int)*(short *)((int)param_2 + 0xe) * (int)(short)param_1[2] +
                         (int)(short)param_2[2] * (int)*(short *)((int)param_1 + 0xe) +
                         (int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 0xc) >>
                        0xc);
    param_3[2] = (short)((int)(short)*param_1 * (int)(short)param_2[1] +
                         (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 4) +
                         (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 2) >>
                        0xc);
    param_3[5] = (short)((int)*(short *)((int)param_2 + 10) * (int)(short)param_1[1] +
                         (int)(short)param_2[1] * (int)*(short *)((int)param_1 + 6) +
                         (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 10) >> 0xc);
    param_3[8] = (short)((int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 0xe) +
                         (int)(short)param_2[4] * (int)(short)param_1[2] +
                         (int)(short)param_2[1] * (int)*(short *)((int)param_1 + 0xc) >> 0xc);
    return param_3;
  }
  uVar1 = *param_1 & 0xffffffffffff;
  uVar2 = *(ulonglong *)((int)param_1 + 6) & 0xffffffffffff;
  uVar4 = (ulonglong)*param_2 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 6) << 0x10);
  uVar3 = *(ulonglong *)((int)param_1 + 0xc) & 0xffffffffffff;
  uVar8 = ((ulonglong)param_2[3] & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  *param_3 = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  param_3[3] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  param_3[6] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  uVar4 = (ulonglong)*(uint *)((int)param_2 + 2) & 0xffff | (ulonglong)(param_2[2] << 0x10);
  uVar8 = ((ulonglong)*(uint *)((int)param_2 + 0xe) & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  param_3[1] = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  param_3[4] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  param_3[7] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  uVar4 = (ulonglong)param_2[1] & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 10) << 0x10);
  uVar8 = ((ulonglong)param_2[4] & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  param_3[2] = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  param_3[5] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  param_3[8] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  return param_3;
}



/* ===================================================== */
/* FUN_00a1e370 @ 00a1e370 */
/* ===================================================== */

undefined2 * FUN_00a1e370(uint *param_1,undefined2 *param_2)

{
  ulonglong uVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  ulonglong uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  ulonglong uVar8;
  
                    /* 0x1e370  339   */
  if (MMXInstalled == 0) {
    *param_2 = (short)((int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 6) +
                       (int)(short)DAT_011d0874 * (int)(short)param_1[3] +
                       (int)(short)*param_1 * (int)(short)EXT_011d0870 >> 0xc);
    param_2[3] = (short)((int)DAT_011d0878 * (int)*(short *)((int)param_1 + 6) +
                         (int)DAT_011d087a * (int)(short)param_1[3] +
                         (int)(short)*param_1 * (int)DAT_011d0874._2_2_ >> 0xc);
    param_2[6] = (short)((int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 6) +
                         (int)DAT_011d087c._4_2_ * (int)(short)param_1[3] +
                         (int)(short)*param_1 * (int)(short)DAT_011d087c >> 0xc);
    param_2[1] = (short)((int)(short)EXT_011d0870 * (int)*(short *)((int)param_1 + 2) +
                         (int)EXT_011d0870._2_2_ * (int)(short)param_1[2] +
                         (int)(short)DAT_011d0874 * (int)*(short *)((int)param_1 + 0xe) >> 0xc);
    param_2[4] = (short)((int)DAT_011d0874._2_2_ * (int)*(short *)((int)param_1 + 2) +
                         (int)DAT_011d0878 * (int)(short)param_1[2] +
                         (int)DAT_011d087a * (int)*(short *)((int)param_1 + 0xe) >> 0xc);
    param_2[7] = (short)((int)(short)DAT_011d087c * (int)*(short *)((int)param_1 + 2) +
                         (int)DAT_011d087c._2_2_ * (int)(short)param_1[2] +
                         (int)DAT_011d087c._4_2_ * (int)*(short *)((int)param_1 + 0xe) >> 0xc);
    param_2[2] = (short)((int)(short)EXT_011d0870 * (int)(short)param_1[1] +
                         (int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 10) +
                         (int)(short)DAT_011d0874 * (int)(short)param_1[4] >> 0xc);
    param_2[5] = (short)((int)DAT_011d0874._2_2_ * (int)(short)param_1[1] +
                         (int)DAT_011d0878 * (int)*(short *)((int)param_1 + 10) +
                         (int)DAT_011d087a * (int)(short)param_1[4] >> 0xc);
    param_2[8] = (short)((int)(short)DAT_011d087c * (int)(short)param_1[1] +
                         (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 10) +
                         (int)DAT_011d087c._4_2_ * (int)(short)param_1[4] >> 0xc);
    return param_2;
  }
  uVar1 = (ulonglong)CONCAT24((short)DAT_011d0874,CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870));
  uVar2 = (ulonglong)CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_));
  uVar4 = (ulonglong)*param_1 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_1 + 6) << 0x10);
  uVar3 = (ulonglong)CONCAT24(DAT_011d087c._4_2_,CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c));
  uVar8 = ((ulonglong)param_1[3] & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  *param_2 = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  param_2[3] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  param_2[6] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  uVar4 = (ulonglong)*(uint *)((int)param_1 + 2) & 0xffff | (ulonglong)(param_1[2] << 0x10);
  uVar8 = ((ulonglong)*(uint *)((int)param_1 + 0xe) & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  param_2[1] = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  param_2[4] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  param_2[7] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  uVar4 = (ulonglong)param_1[1] & 0xffff | (ulonglong)(uint)(*(int *)((int)param_1 + 10) << 0x10);
  uVar8 = ((ulonglong)param_1[4] & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  param_2[2] = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  param_2[5] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  param_2[8] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  return param_2;
}



/* ===================================================== */
/* FUN_00a1e720 @ 00a1e720 */
/* ===================================================== */

ulonglong * FUN_00a1e720(ulonglong *param_1,uint *param_2)

{
  uint uVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  undefined2 uVar7;
  int iVar8;
  ulonglong uVar9;
  ulonglong uVar10;
  ulonglong uVar11;
  ulonglong uVar12;
  undefined8 uVar13;
  undefined8 uVar14;
  undefined8 uVar15;
  ulonglong uVar16;
  undefined4 local_38;
  undefined4 local_34;
  undefined4 local_30;
  undefined4 local_2c;
  
                    /* 0x1e720  340   */
  if (MMXInstalled == 0) {
    iVar3 = (int)(short)param_2[2];
    iVar4 = (int)*(short *)((int)param_2 + 0xe);
    iVar5 = (int)*(short *)((int)param_2 + 2);
    local_38 = CONCAT22((short)(iVar5 * (short)*param_1 + iVar3 * *(short *)((int)param_1 + 2) +
                                iVar4 * *(short *)((int)param_1 + 4) >> 0xc),
                        (short)((int)*(short *)((int)param_2 + 6) *
                                (int)*(short *)((int)param_1 + 2) +
                                (int)*(short *)((int)param_1 + 4) * (int)(short)param_2[3] +
                                (int)(short)*param_1 * (int)(short)*param_2 >> 0xc));
    iVar8 = (int)*(short *)((int)param_2 + 10);
    local_2c = CONCAT22((short)(iVar5 * *(short *)((int)param_1 + 0xc) + iVar4 * (short)param_1[2] +
                                iVar3 * *(short *)((int)param_1 + 0xe) >> 0xc),
                        (short)((int)(short)param_1[2] * (int)(short)param_2[3] +
                                (int)*(short *)((int)param_1 + 0xe) *
                                (int)*(short *)((int)param_2 + 6) +
                                (int)*(short *)((int)param_1 + 0xc) * (int)(short)*param_2 >> 0xc));
    iVar6 = (int)(short)param_2[4];
    iVar2 = (int)(short)param_2[1];
    local_34 = CONCAT22((short)((int)*(short *)((int)param_1 + 6) * (int)(short)*param_2 +
                                (int)(short)param_1[1] * (int)*(short *)((int)param_2 + 6) +
                                (int)*(short *)((int)param_1 + 10) * (int)(short)param_2[3] >> 0xc),
                        (short)(iVar8 * *(short *)((int)param_1 + 2) +
                                iVar6 * *(short *)((int)param_1 + 4) + iVar2 * (short)*param_1 >>
                               0xc));
    local_30 = CONCAT22((short)(iVar2 * *(short *)((int)param_1 + 6) + iVar8 * (short)param_1[1] +
                                iVar6 * *(short *)((int)param_1 + 10) >> 0xc),
                        (short)(iVar5 * *(short *)((int)param_1 + 6) +
                                iVar4 * *(short *)((int)param_1 + 10) + iVar3 * (short)param_1[1] >>
                               0xc));
    uVar7 = (undefined2)
            (iVar2 * *(short *)((int)param_1 + 0xc) + iVar6 * (short)param_1[2] +
             iVar8 * *(short *)((int)param_1 + 0xe) >> 0xc);
  }
  else {
    uVar9 = *param_1 & 0xffffffffffff;
    uVar10 = *(ulonglong *)((int)param_1 + 6) & 0xffffffffffff;
    uVar12 = (ulonglong)*param_2 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 6) << 0x10);
    uVar11 = *(ulonglong *)((int)param_1 + 0xc) & 0xffffffffffff;
    uVar16 = ((ulonglong)param_2[3] & 0xffff) << 0x20;
    uVar13 = pmaddwd(uVar12 | uVar16,uVar9);
    uVar12 = uVar12 | uVar16;
    uVar15 = pmaddwd(uVar12,uVar10);
    uVar14 = pmaddwd(uVar12,uVar11);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_38._0_2_ = (short)((int)uVar13 + (int)((ulonglong)uVar13 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_34._2_2_ = (short)((int)uVar15 + (int)((ulonglong)uVar15 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_2c._0_2_ = (short)((int)uVar14 + (int)((ulonglong)uVar14 >> 0x20) >> 0xc);
    uVar1 = *(uint *)((int)param_2 + 2) & 0xffff | param_2[2] << 0x10;
    uVar13 = pmaddwd(CONCAT44(*(undefined4 *)((int)param_2 + 0xe),uVar1) & 0xffffffffffff,uVar9);
    uVar12 = CONCAT44(*(undefined4 *)((int)param_2 + 0xe),uVar1) & 0xffffffffffff;
    uVar15 = pmaddwd(uVar12,uVar10);
    uVar14 = pmaddwd(uVar12,uVar11);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_38._2_2_ = (short)((int)uVar13 + (int)((ulonglong)uVar13 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_30._0_2_ = (short)((int)uVar15 + (int)((ulonglong)uVar15 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_2c._2_2_ = (short)((int)uVar14 + (int)((ulonglong)uVar14 >> 0x20) >> 0xc);
    uVar1 = param_2[1] & 0xffff | *(int *)((int)param_2 + 10) << 0x10;
    uVar13 = pmaddwd(CONCAT44(param_2[4],uVar1) & 0xffffffffffff,uVar9);
    uVar9 = CONCAT44(param_2[4],uVar1) & 0xffffffffffff;
    uVar15 = pmaddwd(uVar9,uVar10);
    uVar14 = pmaddwd(uVar9,uVar11);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_34._0_2_ = (short)((int)uVar13 + (int)((ulonglong)uVar13 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_30._2_2_ = (short)((int)uVar15 + (int)((ulonglong)uVar15 >> 0x20) >> 0xc);
    uVar7 = (undefined2)((int)uVar14 + (int)((ulonglong)uVar14 >> 0x20) >> 0xc);
  }
  *(undefined4 *)param_1 = local_38;
  *(undefined4 *)((int)param_1 + 4) = local_34;
  *(undefined4 *)(param_1 + 1) = local_30;
  *(undefined4 *)((int)param_1 + 0xc) = local_2c;
  *(undefined2 *)(param_1 + 2) = uVar7;
  return param_1;
}



/* ===================================================== */
/* FUN_00a1ea90 @ 00a1ea90 */
/* ===================================================== */

uint * FUN_00a1ea90(ulonglong *param_1,uint *param_2)

{
  uint uVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  short sVar8;
  int iVar9;
  int iVar10;
  int iVar11;
  int iVar12;
  int iVar13;
  int iVar14;
  int iVar15;
  int iVar16;
  int iVar17;
  int iVar18;
  ulonglong uVar19;
  ulonglong uVar20;
  ulonglong uVar21;
  ulonglong uVar22;
  undefined8 uVar23;
  undefined8 uVar24;
  undefined8 uVar25;
  ulonglong uVar26;
  undefined4 local_4c;
  undefined4 local_48;
  undefined4 local_44;
  undefined4 local_40;
  
                    /* 0x1ea90  341   */
  if (MMXInstalled == 0) {
    iVar5 = (int)(short)param_2[3];
    iVar15 = (int)(short)*param_1;
    iVar9 = (int)*(short *)((int)param_1 + 4);
    iVar12 = (int)*(short *)((int)param_1 + 2);
    iVar16 = (int)*(short *)((int)param_1 + 10);
    iVar13 = (int)(short)param_1[1];
    iVar17 = (int)*(short *)((int)param_1 + 6);
    iVar14 = (int)(short)param_1[2];
    iVar18 = (int)*(short *)((int)param_1 + 0xe);
    iVar2 = (int)*(short *)((int)param_1 + 0xc);
    iVar10 = (int)*(short *)((int)param_2 + 0xe);
    iVar3 = (int)*(short *)((int)param_2 + 2);
    iVar6 = (int)(short)param_2[2];
    local_4c = CONCAT22((short)(iVar3 * iVar15 + iVar6 * iVar12 + iVar10 * iVar9 >> 0xc),
                        (short)(*(short *)((int)param_2 + 6) * iVar12 + iVar9 * iVar5 +
                                iVar15 * (short)*param_2 >> 0xc));
    local_40 = CONCAT22((short)(iVar3 * iVar2 + iVar10 * iVar14 + iVar6 * iVar18 >> 0xc),
                        (short)(iVar14 * iVar5 + iVar18 * *(short *)((int)param_2 + 6) +
                                iVar2 * (short)*param_2 >> 0xc));
    iVar7 = (int)(short)param_2[4];
    iVar11 = (int)*(short *)((int)param_2 + 10);
    iVar4 = (int)(short)param_2[1];
    local_48 = CONCAT22((short)(iVar17 * (short)*param_2 + iVar13 * *(short *)((int)param_2 + 6) +
                                iVar16 * iVar5 >> 0xc),
                        (short)(iVar11 * iVar12 + iVar7 * iVar9 + iVar4 * iVar15 >> 0xc));
    local_44 = CONCAT22((short)(iVar4 * iVar17 + iVar11 * iVar13 + iVar7 * iVar16 >> 0xc),
                        (short)(iVar3 * iVar17 + iVar10 * iVar16 + iVar6 * iVar13 >> 0xc));
    sVar8 = (short)(iVar4 * iVar2 + iVar7 * iVar14 + iVar11 * iVar18 >> 0xc);
  }
  else {
    uVar19 = *param_1 & 0xffffffffffff;
    uVar20 = *(ulonglong *)((int)param_1 + 6) & 0xffffffffffff;
    uVar22 = (ulonglong)*param_2 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 6) << 0x10);
    uVar21 = *(ulonglong *)((int)param_1 + 0xc) & 0xffffffffffff;
    uVar26 = ((ulonglong)param_2[3] & 0xffff) << 0x20;
    uVar23 = pmaddwd(uVar22 | uVar26,uVar19);
    uVar22 = uVar22 | uVar26;
    uVar25 = pmaddwd(uVar22,uVar20);
    uVar24 = pmaddwd(uVar22,uVar21);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_4c._0_2_ = (short)((int)uVar23 + (int)((ulonglong)uVar23 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_48._2_2_ = (short)((int)uVar25 + (int)((ulonglong)uVar25 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_40._0_2_ = (short)((int)uVar24 + (int)((ulonglong)uVar24 >> 0x20) >> 0xc);
    uVar1 = *(uint *)((int)param_2 + 2) & 0xffff | param_2[2] << 0x10;
    uVar23 = pmaddwd(CONCAT44(*(undefined4 *)((int)param_2 + 0xe),uVar1) & 0xffffffffffff,uVar19);
    uVar22 = CONCAT44(*(undefined4 *)((int)param_2 + 0xe),uVar1) & 0xffffffffffff;
    uVar25 = pmaddwd(uVar22,uVar20);
    uVar24 = pmaddwd(uVar22,uVar21);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_4c._2_2_ = (short)((int)uVar23 + (int)((ulonglong)uVar23 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_44._0_2_ = (short)((int)uVar25 + (int)((ulonglong)uVar25 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_40._2_2_ = (short)((int)uVar24 + (int)((ulonglong)uVar24 >> 0x20) >> 0xc);
    uVar1 = param_2[1] & 0xffff | *(int *)((int)param_2 + 10) << 0x10;
    uVar23 = pmaddwd(CONCAT44(param_2[4],uVar1) & 0xffffffffffff,uVar19);
    uVar19 = CONCAT44(param_2[4],uVar1) & 0xffffffffffff;
    uVar25 = pmaddwd(uVar19,uVar20);
    uVar24 = pmaddwd(uVar19,uVar21);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_48._0_2_ = (short)((int)uVar23 + (int)((ulonglong)uVar23 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_44._2_2_ = (short)((int)uVar25 + (int)((ulonglong)uVar25 >> 0x20) >> 0xc);
    sVar8 = (short)((int)uVar24 + (int)((ulonglong)uVar24 >> 0x20) >> 0xc);
  }
  *param_2 = local_4c;
  param_2[1] = local_48;
  param_2[2] = local_44;
  param_2[3] = local_40;
  *(short *)(param_2 + 4) = sVar8;
  return param_2;
}



/* ===================================================== */
/* FUN_00a1ede0 @ 00a1ede0 */
/* ===================================================== */

uint * FUN_00a1ede0(uint *param_1)

{
  ulonglong uVar1;
  ulonglong uVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  int iVar8;
  short sVar9;
  int iVar10;
  ulonglong uVar11;
  undefined8 uVar12;
  undefined8 uVar13;
  ulonglong uVar14;
  undefined8 uVar15;
  ulonglong uVar16;
  undefined4 local_38;
  undefined4 local_34;
  undefined4 local_30;
  undefined4 local_2c;
  
                    /* 0x1ede0  342   */
  if (MMXInstalled == 0) {
    iVar4 = (int)(short)param_1[2];
    iVar7 = (int)*(short *)((int)param_1 + 2);
    iVar5 = (int)*(short *)((int)param_1 + 0xe);
    local_38 = CONCAT22((short)(iVar7 * (short)EXT_011d0870 + iVar4 * EXT_011d0870._2_2_ +
                                iVar5 * (short)DAT_011d0874 >> 0xc),
                        (short)((int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 6) +
                                (int)(short)DAT_011d0874 * (int)(short)param_1[3] +
                                (int)(short)EXT_011d0870 * (int)(short)*param_1 >> 0xc));
    iVar10 = (int)*(short *)((int)param_1 + 10);
    local_2c = CONCAT22((short)(iVar5 * DAT_011d087c._4_2_ + iVar4 * DAT_011d087c._2_2_ +
                                iVar7 * (short)DAT_011d087c >> 0xc),
                        (short)((int)(short)DAT_011d087c * (int)(short)*param_1 +
                                (int)DAT_011d087c._4_2_ * (int)(short)param_1[3] +
                                (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 6) >> 0xc))
    ;
    iVar6 = (int)(short)param_1[4];
    iVar8 = (int)(short)param_1[1];
    local_34 = CONCAT22((short)((int)DAT_011d0878 * (int)*(short *)((int)param_1 + 6) +
                                (int)DAT_011d087a * (int)(short)param_1[3] +
                                (int)DAT_011d0874._2_2_ * (int)(short)*param_1 >> 0xc),
                        (short)(iVar10 * EXT_011d0870._2_2_ + iVar6 * (short)DAT_011d0874 +
                                iVar8 * (short)EXT_011d0870 >> 0xc));
    local_30 = CONCAT22((short)(iVar10 * DAT_011d0878 + iVar8 * DAT_011d0874._2_2_ +
                                iVar6 * DAT_011d087a >> 0xc),
                        (short)(iVar7 * DAT_011d0874._2_2_ + iVar4 * DAT_011d0878 +
                                iVar5 * DAT_011d087a >> 0xc));
    sVar9 = (short)(iVar6 * DAT_011d087c._4_2_ + iVar10 * DAT_011d087c._2_2_ +
                    iVar8 * (short)DAT_011d087c >> 0xc);
  }
  else {
    uVar14 = (ulonglong)
             CONCAT24((short)DAT_011d0874,CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870));
    uVar1 = (ulonglong)CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_));
    uVar11 = (ulonglong)*param_1 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_1 + 6) << 0x10);
    uVar2 = (ulonglong)CONCAT24(DAT_011d087c._4_2_,CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c))
    ;
    uVar16 = ((ulonglong)param_1[3] & 0xffff) << 0x20;
    uVar12 = pmaddwd(uVar11 | uVar16,uVar14);
    uVar11 = uVar11 | uVar16;
    uVar15 = pmaddwd(uVar11,uVar1);
    uVar13 = pmaddwd(uVar11,uVar2);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_38._0_2_ = (short)((int)uVar12 + (int)((ulonglong)uVar12 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_34._2_2_ = (short)((int)uVar15 + (int)((ulonglong)uVar15 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_2c._0_2_ = (short)((int)uVar13 + (int)((ulonglong)uVar13 >> 0x20) >> 0xc);
    uVar3 = *(uint *)((int)param_1 + 2) & 0xffff | param_1[2] << 0x10;
    uVar12 = pmaddwd(CONCAT44(*(undefined4 *)((int)param_1 + 0xe),uVar3) & 0xffffffffffff,uVar14);
    uVar11 = CONCAT44(*(undefined4 *)((int)param_1 + 0xe),uVar3) & 0xffffffffffff;
    uVar15 = pmaddwd(uVar11,uVar1);
    uVar13 = pmaddwd(uVar11,uVar2);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_38._2_2_ = (short)((int)uVar12 + (int)((ulonglong)uVar12 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_30._0_2_ = (short)((int)uVar15 + (int)((ulonglong)uVar15 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_2c._2_2_ = (short)((int)uVar13 + (int)((ulonglong)uVar13 >> 0x20) >> 0xc);
    uVar3 = param_1[1] & 0xffff | *(int *)((int)param_1 + 10) << 0x10;
    uVar12 = pmaddwd(CONCAT44(param_1[4],uVar3) & 0xffffffffffff,uVar14);
    uVar14 = CONCAT44(param_1[4],uVar3) & 0xffffffffffff;
    uVar15 = pmaddwd(uVar14,uVar1);
    uVar13 = pmaddwd(uVar14,uVar2);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_34._0_2_ = (short)((int)uVar12 + (int)((ulonglong)uVar12 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_30._2_2_ = (short)((int)uVar15 + (int)((ulonglong)uVar15 >> 0x20) >> 0xc);
    sVar9 = (short)((int)uVar13 + (int)((ulonglong)uVar13 >> 0x20) >> 0xc);
  }
  *param_1 = local_38;
  param_1[1] = local_34;
  param_1[2] = local_30;
  param_1[3] = local_2c;
  *(short *)(param_1 + 4) = sVar9;
  return param_1;
}



/* ===================================================== */
/* FUN_00a1f1a0 @ 00a1f1a0 */
/* ===================================================== */

ulonglong * FUN_00a1f1a0(ulonglong *param_1,uint *param_2)

{
  ulonglong uVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  ulonglong uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  ulonglong uVar8;
  
                    /* 0x1f1a0  343   */
  if (MMXInstalled == 0) {
    EXT_011d0870._0_2_ =
         (short)((int)(short)param_2[3] * (int)*(short *)((int)param_1 + 4) +
                 (int)*(short *)((int)param_1 + 2) * (int)*(short *)((int)param_2 + 6) +
                 (int)(short)*param_2 * (int)(short)*param_1 >> 0xc);
    DAT_011d0874._2_2_ =
         (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 6) +
                 (int)*(short *)((int)param_1 + 10) * (int)(short)param_2[3] +
                 (int)*(short *)((int)param_2 + 6) * (int)(short)param_1[1] >> 0xc);
    DAT_011d087c._0_2_ =
         (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 0xc) +
                 (int)*(short *)((int)param_1 + 0xe) * (int)*(short *)((int)param_2 + 6) +
                 (int)(short)param_1[2] * (int)(short)param_2[3] >> 0xc);
    EXT_011d0870._2_2_ =
         (short)((int)(short)*param_1 * (int)*(short *)((int)param_2 + 2) +
                 (int)(short)param_2[2] * (int)*(short *)((int)param_1 + 2) +
                 (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 4) >> 0xc);
    DAT_011d0878 = (short)((int)(short)param_2[2] * (int)(short)param_1[1] +
                           (int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 6) +
                           (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 10)
                          >> 0xc);
    DAT_011d087c._2_2_ =
         (short)((int)(short)param_2[2] * (int)*(short *)((int)param_1 + 0xe) +
                 (int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 0xc) +
                 (int)*(short *)((int)param_2 + 0xe) * (int)(short)param_1[2] >> 0xc);
    DAT_011d0874._0_2_ =
         (short)((int)(short)*param_1 * (int)(short)param_2[1] +
                 (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 2) +
                 (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 4) >> 0xc);
    DAT_011d087a = (short)((int)*(short *)((int)param_2 + 10) * (int)(short)param_1[1] +
                           (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 10) +
                           (int)(short)param_2[1] * (int)*(short *)((int)param_1 + 6) >> 0xc);
    DAT_011d087c._4_2_ =
         (short)((int)(short)param_2[4] * (int)(short)param_1[2] +
                 (int)(short)param_2[1] * (int)*(short *)((int)param_1 + 0xc) +
                 (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 0xe) >> 0xc);
    return param_1;
  }
  uVar1 = *param_1 & 0xffffffffffff;
  uVar2 = *(ulonglong *)((int)param_1 + 6) & 0xffffffffffff;
  uVar4 = (ulonglong)*param_2 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 6) << 0x10);
  uVar3 = *(ulonglong *)((int)param_1 + 0xc) & 0xffffffffffff;
  uVar8 = ((ulonglong)param_2[3] & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  EXT_011d0870._0_2_ = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  DAT_011d0874._2_2_ = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  DAT_011d087c._0_2_ = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  uVar4 = (ulonglong)*(uint *)((int)param_2 + 2) & 0xffff | (ulonglong)(param_2[2] << 0x10);
  uVar8 = ((ulonglong)*(uint *)((int)param_2 + 0xe) & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  EXT_011d0870._2_2_ = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  DAT_011d0878 = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  DAT_011d087c._2_2_ = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  uVar4 = (ulonglong)param_2[1] & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 10) << 0x10);
  uVar8 = ((ulonglong)param_2[4] & 0xffff) << 0x20;
  uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
  uVar4 = uVar4 | uVar8;
  uVar7 = pmaddwd(uVar4,uVar2);
  uVar6 = pmaddwd(uVar4,uVar3);
  DAT_011d0874._0_2_ = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
  DAT_011d087a = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
  DAT_011d087c._4_2_ = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  return param_1;
}



/* ===================================================== */
/* FUN_00a1f520 @ 00a1f520 */
/* ===================================================== */

uint * FUN_00a1f520(uint *param_1)

{
  ulonglong uVar1;
  ulonglong uVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  int iVar8;
  int iVar9;
  ulonglong uVar10;
  undefined8 uVar11;
  undefined8 uVar12;
  ulonglong uVar13;
  undefined8 uVar14;
  ulonglong uVar15;
  undefined4 local_38;
  undefined4 local_34;
  undefined4 local_30;
  undefined4 local_2c;
  
                    /* 0x1f520  344   */
  if (MMXInstalled == 0) {
    iVar4 = (int)(short)param_1[2];
    iVar7 = (int)*(short *)((int)param_1 + 2);
    iVar5 = (int)*(short *)((int)param_1 + 0xe);
    local_38 = CONCAT22((short)(iVar7 * (short)EXT_011d0870 + iVar4 * EXT_011d0870._2_2_ +
                                iVar5 * (short)DAT_011d0874 >> 0xc),
                        (short)((int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 6) +
                                (int)(short)DAT_011d0874 * (int)(short)param_1[3] +
                                (int)(short)EXT_011d0870 * (int)(short)*param_1 >> 0xc));
    iVar9 = (int)*(short *)((int)param_1 + 10);
    local_2c = CONCAT22((short)(iVar5 * DAT_011d087c._4_2_ + iVar4 * DAT_011d087c._2_2_ +
                                iVar7 * (short)DAT_011d087c >> 0xc),
                        (short)((int)(short)DAT_011d087c * (int)(short)*param_1 +
                                (int)DAT_011d087c._4_2_ * (int)(short)param_1[3] +
                                (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 6) >> 0xc))
    ;
    iVar6 = (int)(short)param_1[4];
    iVar8 = (int)(short)param_1[1];
    local_34 = CONCAT22((short)((int)DAT_011d0878 * (int)*(short *)((int)param_1 + 6) +
                                (int)DAT_011d087a * (int)(short)param_1[3] +
                                (int)DAT_011d0874._2_2_ * (int)(short)*param_1 >> 0xc),
                        (short)(iVar9 * EXT_011d0870._2_2_ + iVar6 * (short)DAT_011d0874 +
                                iVar8 * (short)EXT_011d0870 >> 0xc));
    local_30 = CONCAT22((short)(iVar9 * DAT_011d0878 + iVar8 * DAT_011d0874._2_2_ +
                                iVar6 * DAT_011d087a >> 0xc),
                        (short)(iVar7 * DAT_011d0874._2_2_ + iVar4 * DAT_011d0878 +
                                iVar5 * DAT_011d087a >> 0xc));
    DAT_011d087c._4_2_ =
         (short)(iVar6 * DAT_011d087c._4_2_ + iVar9 * DAT_011d087c._2_2_ +
                 iVar8 * (short)DAT_011d087c >> 0xc);
  }
  else {
    uVar13 = (ulonglong)
             CONCAT24((short)DAT_011d0874,CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870));
    uVar1 = (ulonglong)CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_));
    uVar10 = (ulonglong)*param_1 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_1 + 6) << 0x10);
    uVar2 = (ulonglong)CONCAT24(DAT_011d087c._4_2_,CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c))
    ;
    uVar15 = ((ulonglong)param_1[3] & 0xffff) << 0x20;
    uVar11 = pmaddwd(uVar10 | uVar15,uVar13);
    uVar10 = uVar10 | uVar15;
    uVar14 = pmaddwd(uVar10,uVar1);
    uVar12 = pmaddwd(uVar10,uVar2);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_38._0_2_ = (short)((int)uVar11 + (int)((ulonglong)uVar11 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_34._2_2_ = (short)((int)uVar14 + (int)((ulonglong)uVar14 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_2c._0_2_ = (short)((int)uVar12 + (int)((ulonglong)uVar12 >> 0x20) >> 0xc);
    uVar3 = *(uint *)((int)param_1 + 2) & 0xffff | param_1[2] << 0x10;
    uVar11 = pmaddwd(CONCAT44(*(undefined4 *)((int)param_1 + 0xe),uVar3) & 0xffffffffffff,uVar13);
    uVar10 = CONCAT44(*(undefined4 *)((int)param_1 + 0xe),uVar3) & 0xffffffffffff;
    uVar14 = pmaddwd(uVar10,uVar1);
    uVar12 = pmaddwd(uVar10,uVar2);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_38._2_2_ = (short)((int)uVar11 + (int)((ulonglong)uVar11 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_30._0_2_ = (short)((int)uVar14 + (int)((ulonglong)uVar14 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_2c._2_2_ = (short)((int)uVar12 + (int)((ulonglong)uVar12 >> 0x20) >> 0xc);
    uVar3 = param_1[1] & 0xffff | *(int *)((int)param_1 + 10) << 0x10;
    uVar11 = pmaddwd(CONCAT44(param_1[4],uVar3) & 0xffffffffffff,uVar13);
    uVar13 = CONCAT44(param_1[4],uVar3) & 0xffffffffffff;
    uVar14 = pmaddwd(uVar13,uVar1);
    uVar12 = pmaddwd(uVar13,uVar2);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_34._0_2_ = (short)((int)uVar11 + (int)((ulonglong)uVar11 >> 0x20) >> 0xc);
                    /* WARNING: Ignoring partial resolution of indirect */
    local_30._2_2_ = (short)((int)uVar14 + (int)((ulonglong)uVar14 >> 0x20) >> 0xc);
    DAT_011d087c._4_2_ = (short)((int)uVar12 + (int)((ulonglong)uVar12 >> 0x20) >> 0xc);
  }
  EXT_011d0870._0_2_ = (short)local_38;
  EXT_011d0870._2_2_ = (short)((uint)local_38 >> 0x10);
  DAT_011d0874._0_2_ = (short)local_34;
  DAT_011d0874._2_2_ = (short)((uint)local_34 >> 0x10);
  DAT_011d0878 = (short)local_30;
  DAT_011d087a = (short)((uint)local_30 >> 0x10);
  DAT_011d087c._0_2_ = (short)local_2c;
  DAT_011d087c._2_2_ = (short)((uint)local_2c >> 0x10);
  return param_1;
}



/* ===================================================== */
/* FUN_00a1f8f0 @ 00a1f8f0 */
/* ===================================================== */

void FUN_00a1f8f0(short *param_1,short *param_2,int *param_3)

{
  short sVar1;
  short sVar2;
  short sVar3;
  short sVar4;
  short sVar5;
  short sVar6;
  int iVar7;
  int iVar8;
  int iVar9;
  int local_4;
  
                    /* 0x1f8f0  345   */
  iVar7 = (int)param_2[2];
  iVar8 = (int)*param_2;
  iVar9 = (int)param_2[1];
  sVar1 = param_1[5];
  sVar2 = param_1[4];
  sVar3 = param_1[3];
  sVar4 = param_1[7];
  sVar5 = param_1[8];
  sVar6 = param_1[6];
  *param_3 = *param_1 * iVar8 + param_1[1] * iVar9 + param_1[2] * iVar7 >> 0xc;
  param_3[1] = sVar2 * iVar9 + sVar1 * iVar7 + sVar3 * iVar8 >> 0xc;
  param_3[2] = sVar4 * iVar9 + sVar5 * iVar7 + sVar6 * iVar8 >> 0xc;
  param_3[3] = local_4;
  return;
}



/* ===================================================== */
/* FUN_00a1f980 @ 00a1f980 */
/* ===================================================== */

void FUN_00a1f980(short *param_1,int *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  int iVar8;
  int iVar9;
  int local_4;
  
                    /* 0x1f980  346   */
  iVar1 = (int)param_1[1];
  iVar5 = (int)*param_1;
  iVar9 = (int)param_1[2];
  iVar6 = (int)DAT_011d0874._2_2_;
  iVar4 = (int)DAT_011d0878;
  iVar7 = (int)DAT_011d087a;
  iVar8 = (int)DAT_011d087c._2_2_;
  iVar2 = (int)(short)DAT_011d087c;
  iVar3 = (int)DAT_011d087c._4_2_;
  *param_2 = EXT_011d0870._2_2_ * iVar1 + (short)EXT_011d0870 * iVar5 + (short)DAT_011d0874 * iVar9
             >> 0xc;
  param_2[1] = iVar4 * iVar1 + iVar6 * iVar5 + iVar7 * iVar9 >> 0xc;
  param_2[2] = iVar8 * iVar1 + iVar2 * iVar5 + iVar3 * iVar9 >> 0xc;
  param_2[3] = local_4;
  return;
}



/* ===================================================== */
/* FUN_00a1fa30 @ 00a1fa30 */
/* ===================================================== */

undefined4 * FUN_00a1fa30(int *param_1,undefined4 *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  int iVar7;
  int iVar8;
  int iVar9;
  undefined4 local_4;
  
                    /* 0x1fa30  347   */
  iVar1 = param_1[1];
  iVar7 = iVar1 >> 0x1f;
  iVar2 = param_1[2];
  iVar8 = iVar2 >> 0x1f;
  iVar3 = *param_1;
  iVar9 = iVar3 >> 0x1f;
  __allmul((int)(short)EXT_011d0870,(int)(short)EXT_011d0870 >> 0x1f,iVar3,iVar9);
  __allmul((int)EXT_011d0870._2_2_,(int)EXT_011d0870._2_2_ >> 0x1f,iVar1,iVar7);
  __allmul((int)(short)DAT_011d0874,(int)(short)DAT_011d0874 >> 0x1f,iVar2,iVar8);
  uVar4 = __allshr();
  __allmul((int)DAT_011d0874._2_2_,(int)DAT_011d0874._2_2_ >> 0x1f,iVar3,iVar9);
  __allmul((int)DAT_011d0878,(int)DAT_011d0878 >> 0x1f,iVar1,iVar7);
  __allmul((int)DAT_011d087a,(int)DAT_011d087a >> 0x1f,iVar2,iVar8);
  uVar5 = __allshr();
  __allmul((int)(short)DAT_011d087c,(int)(short)DAT_011d087c >> 0x1f,iVar3,iVar9);
  __allmul((int)DAT_011d087c._2_2_,(int)DAT_011d087c._2_2_ >> 0x1f,iVar1,iVar7);
  __allmul((int)DAT_011d087c._4_2_,(int)DAT_011d087c._4_2_ >> 0x1f,iVar2,iVar8);
  uVar6 = __allshr();
  *param_2 = uVar4;
  param_2[1] = uVar5;
  param_2[2] = uVar6;
  param_2[3] = local_4;
  return param_2;
}



/* ===================================================== */
/* FUN_00a1fba0 @ 00a1fba0 */
/* ===================================================== */

undefined4 * FUN_00a1fba0(short *param_1,int *param_2,undefined4 *param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  int iVar7;
  int iVar8;
  int iVar9;
  undefined4 local_4;
  
                    /* 0x1fba0  348   */
  iVar1 = param_2[2];
  iVar7 = iVar1 >> 0x1f;
  iVar2 = param_2[1];
  iVar8 = iVar2 >> 0x1f;
  iVar3 = *param_2;
  iVar9 = iVar3 >> 0x1f;
  __allmul((int)*param_1,(int)*param_1 >> 0x1f,iVar3,iVar9);
  __allmul((int)param_1[2],(int)param_1[2] >> 0x1f,iVar1,iVar7);
  __allmul((int)param_1[1],(int)param_1[1] >> 0x1f,iVar2,iVar8);
  uVar4 = __allshr();
  __allmul((int)param_1[5],(int)param_1[5] >> 0x1f,iVar1,iVar7);
  __allmul((int)param_1[4],(int)param_1[4] >> 0x1f,iVar2,iVar8);
  __allmul((int)param_1[3],(int)param_1[3] >> 0x1f,iVar3,iVar9);
  uVar5 = __allshr();
  __allmul((int)param_1[6],(int)param_1[6] >> 0x1f,iVar3,iVar9);
  __allmul((int)param_1[7],(int)param_1[7] >> 0x1f,iVar2,iVar8);
  __allmul((int)param_1[8],(int)param_1[8] >> 0x1f,iVar1,iVar7);
  uVar6 = __allshr();
  *param_3 = uVar4;
  param_3[1] = uVar5;
  param_3[2] = uVar6;
  param_3[3] = local_4;
  return param_3;
}



/* ===================================================== */
/* FUN_00a1fd10 @ 00a1fd10 */
/* ===================================================== */

undefined4 * FUN_00a1fd10(short *param_1,int *param_2,undefined4 *param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  undefined4 uVar4;
  undefined4 uVar5;
  undefined4 uVar6;
  int iVar7;
  int iVar8;
  int iVar9;
  undefined4 local_4;
  
                    /* 0x1fd10  349   */
  iVar1 = param_2[2];
  iVar7 = iVar1 >> 0x1f;
  iVar2 = param_2[1];
  iVar8 = iVar2 >> 0x1f;
  iVar3 = *param_2;
  iVar9 = iVar3 >> 0x1f;
  __allmul((int)*param_1,(int)*param_1 >> 0x1f,iVar3,iVar9);
  __allmul((int)param_1[3],(int)param_1[3] >> 0x1f,iVar2,iVar8);
  __allmul((int)param_1[6],(int)param_1[6] >> 0x1f,iVar1,iVar7);
  uVar4 = __allshr();
  __allmul((int)param_1[7],(int)param_1[7] >> 0x1f,iVar1,iVar7);
  __allmul((int)param_1[4],(int)param_1[4] >> 0x1f,iVar2,iVar8);
  __allmul((int)param_1[1],(int)param_1[1] >> 0x1f,iVar3,iVar9);
  uVar5 = __allshr();
  __allmul((int)param_1[5],(int)param_1[5] >> 0x1f,iVar2,iVar8);
  __allmul((int)param_1[8],(int)param_1[8] >> 0x1f,iVar1,iVar7);
  __allmul((int)param_1[2],(int)param_1[2] >> 0x1f,iVar3,iVar9);
  uVar6 = __allshr();
  *param_3 = uVar4;
  param_3[1] = uVar5;
  param_3[2] = uVar6;
  param_3[3] = local_4;
  return param_3;
}



/* ===================================================== */
/* FUN_00a1fe80 @ 00a1fe80 */
/* ===================================================== */

void FUN_00a1fe80(short *param_1,undefined2 *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  
                    /* 0x1fe80  350   */
  iVar1 = (&DAT_011cb420)[(int)*param_1 & 0xfff];
  iVar2 = (&DAT_011cb420)[(int)*param_1 + 0x400U & 0xfff];
  iVar3 = (&DAT_011cb420)[(int)param_1[2] & 0xfff];
  iVar4 = (&DAT_011cb420)[(int)param_1[2] + 0x400U & 0xfff];
  iVar5 = (&DAT_011cb420)[(int)param_1[1] + 0x400U & 0xfff];
  iVar6 = (&DAT_011cb420)[(int)param_1[1] & 0xfff];
  *param_2 = (short)(iVar4 * iVar5 >> 0xc);
  param_2[2] = (short)iVar6;
  param_2[1] = (short)(-(iVar3 * iVar5) >> 0xc);
  param_2[3] = (short)((iVar6 * iVar1 >> 0xc) * iVar4 + iVar3 * iVar2 >> 0xc);
  param_2[4] = (short)((-(iVar6 * iVar1) >> 0xc) * iVar3 + iVar4 * iVar2 >> 0xc);
  param_2[5] = (short)(-(iVar5 * iVar1) >> 0xc);
  param_2[6] = (short)((-(iVar6 * iVar2) >> 0xc) * iVar4 + iVar3 * iVar1 >> 0xc);
  param_2[7] = (short)((iVar6 * iVar2 >> 0xc) * iVar3 + iVar4 * iVar1 >> 0xc);
  param_2[8] = (short)(iVar5 * iVar2 >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a1ffc0 @ 00a1ffc0 */
/* ===================================================== */

void FUN_00a1ffc0(short *param_1,undefined2 *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  int iVar7;
  
                    /* 0x1ffc0  351   */
  iVar1 = (&DAT_011cb420)[(int)*param_1 + 0x400U & 0xfff];
  iVar7 = (&DAT_011cb420)[(int)*param_1 & 0xfff];
  iVar2 = (&DAT_011cb420)[(int)param_1[1] & 0xfff];
  iVar3 = (&DAT_011cb420)[(int)param_1[1] + 0x400U & 0xfff];
  iVar6 = iVar2 * iVar7 >> 0xc;
  iVar4 = (&DAT_011cb420)[(int)param_1[2] & 0xfff];
  iVar5 = (&DAT_011cb420)[(int)param_1[2] + 0x400U & 0xfff];
  *param_2 = (short)(iVar4 * iVar6 + iVar5 * iVar3 >> 0xc);
  param_2[1] = (short)(iVar5 * iVar6 - iVar4 * iVar3 >> 0xc);
  param_2[2] = (short)(iVar2 * iVar1 >> 0xc);
  param_2[3] = (short)(iVar4 * iVar1 >> 0xc);
  param_2[5] = -(short)iVar7;
  param_2[4] = (short)(iVar5 * iVar1 >> 0xc);
  iVar7 = iVar3 * iVar7 >> 0xc;
  param_2[6] = (short)(iVar4 * iVar7 - iVar5 * iVar2 >> 0xc);
  param_2[7] = (short)(iVar5 * iVar7 + iVar4 * iVar2 >> 0xc);
  param_2[8] = (short)(iVar3 * iVar1 >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a200f0 @ 00a200f0 */
/* ===================================================== */

void FUN_00a200f0(uint param_1,undefined4 *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  undefined4 *puVar4;
  undefined4 *puVar5;
  undefined4 local_20;
  short local_1a;
  short local_18;
  short local_16;
  short local_14;
  short local_12;
  short local_10;
  
                    /* 0x200f0  352   */
  iVar1 = (&DAT_011cb420)[param_1 + 0x400 & 0xfff];
  iVar2 = (&DAT_011cb420)[param_1 & 0xfff];
  puVar4 = param_2;
  puVar5 = &local_20;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *puVar5 = *puVar4;
    puVar4 = puVar4 + 1;
    puVar5 = puVar5 + 1;
  }
  *(short *)((int)param_2 + 6) = (short)(iVar1 * local_1a - iVar2 * local_14 >> 0xc);
  *(short *)(param_2 + 2) = (short)(iVar1 * local_18 - iVar2 * local_12 >> 0xc);
  *(short *)((int)param_2 + 10) = (short)(iVar1 * local_16 - iVar2 * local_10 >> 0xc);
  *(short *)(param_2 + 3) = (short)(iVar1 * local_14 + iVar2 * local_1a >> 0xc);
  *(short *)((int)param_2 + 0xe) = (short)(iVar1 * local_12 + iVar2 * local_18 >> 0xc);
  *(short *)(param_2 + 4) = (short)(iVar1 * local_10 + iVar2 * local_16 >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a201e0 @ 00a201e0 */
/* ===================================================== */

void FUN_00a201e0(uint param_1,undefined4 *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  undefined4 *puVar4;
  short *psVar5;
  short local_20;
  short local_1e;
  short local_1c;
  short local_14;
  short local_12;
  short local_10;
  
                    /* 0x201e0  353   */
  iVar1 = (&DAT_011cb420)[param_1 + 0x400 & 0xfff];
  iVar2 = (&DAT_011cb420)[param_1 & 0xfff];
  puVar4 = param_2;
  psVar5 = &local_20;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *(undefined4 *)psVar5 = *puVar4;
    puVar4 = puVar4 + 1;
    psVar5 = psVar5 + 2;
  }
  *(short *)(param_2 + 3) = (short)(iVar2 * local_14 - iVar1 * local_20 >> 0xc);
  *(short *)((int)param_2 + 0xe) = (short)(iVar1 * local_1e - iVar2 * local_12 >> 0xc);
  *(short *)(param_2 + 4) = (short)(iVar2 * local_10 - iVar1 * local_1c >> 0xc);
  *(short *)param_2 = (short)(-(iVar1 * local_14 + iVar2 * local_20) >> 0xc);
  *(short *)((int)param_2 + 2) = (short)(iVar1 * local_12 + iVar2 * local_1e >> 0xc);
  *(short *)(param_2 + 1) = (short)(-(iVar1 * local_10 + iVar2 * local_1c) >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a202d0 @ 00a202d0 */
/* ===================================================== */

void FUN_00a202d0(uint param_1,undefined4 *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  undefined4 *puVar4;
  short *psVar5;
  short local_20;
  short local_1e;
  short local_1c;
  short local_1a;
  short local_18;
  short local_16;
  
                    /* 0x202d0  354   */
  iVar1 = (&DAT_011cb420)[param_1 + 0x400 & 0xfff];
  iVar2 = (&DAT_011cb420)[param_1 & 0xfff];
  puVar4 = param_2;
  psVar5 = &local_20;
  for (iVar3 = 8; iVar3 != 0; iVar3 = iVar3 + -1) {
    *(undefined4 *)psVar5 = *puVar4;
    puVar4 = puVar4 + 1;
    psVar5 = psVar5 + 2;
  }
  *(short *)param_2 = (short)(iVar1 * local_20 - iVar2 * local_1a >> 0xc);
  *(short *)((int)param_2 + 2) = (short)(iVar1 * local_1e - iVar2 * local_18 >> 0xc);
  *(short *)(param_2 + 1) = (short)(iVar1 * local_1c - iVar2 * local_16 >> 0xc);
  *(short *)((int)param_2 + 6) = (short)(iVar1 * local_1a + iVar2 * local_20 >> 0xc);
  *(short *)(param_2 + 2) = (short)(iVar1 * local_18 + iVar2 * local_1e >> 0xc);
  *(short *)((int)param_2 + 10) = (short)(iVar1 * local_16 + iVar2 * local_1c >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a203c0 @ 00a203c0 */
/* ===================================================== */

void FUN_00a203c0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x203c0  355   */
  FUN_00a1fe80(param_1,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a203e0 @ 00a203e0 */
/* ===================================================== */

void FUN_00a203e0(int param_1,undefined4 *param_2)

{
                    /* 0x203e0  356   */
  *(undefined4 *)(param_1 + 0x14) = *param_2;
  *(undefined4 *)(param_1 + 0x18) = param_2[1];
  *(undefined4 *)(param_1 + 0x1c) = param_2[2];
  return;
}



/* ===================================================== */
/* FUN_00a20400 @ 00a20400 */
/* ===================================================== */

void FUN_00a20400(short *param_1,int *param_2)

{
                    /* 0x20400  358   */
  *param_1 = (short)((int)*param_1 * *param_2 >> 0xc);
  param_1[1] = (short)(*param_2 * (int)param_1[1] >> 0xc);
  param_1[2] = (short)(*param_2 * (int)param_1[2] >> 0xc);
  param_1[3] = (short)(param_2[1] * (int)param_1[3] >> 0xc);
  param_1[4] = (short)(param_2[1] * (int)param_1[4] >> 0xc);
  param_1[5] = (short)(param_2[1] * (int)param_1[5] >> 0xc);
  param_1[6] = (short)(param_2[2] * (int)param_1[6] >> 0xc);
  param_1[7] = (short)(param_2[2] * (int)param_1[7] >> 0xc);
  param_1[8] = (short)(param_2[2] * (int)param_1[8] >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a204a0 @ 00a204a0 */
/* ===================================================== */

void FUN_00a204a0(short *param_1,int *param_2)

{
                    /* 0x204a0  357   */
  *param_1 = (short)((int)*param_1 * *param_2 >> 0xc);
  param_1[3] = (short)(*param_2 * (int)param_1[3] >> 0xc);
  param_1[6] = (short)(*param_2 * (int)param_1[6] >> 0xc);
  param_1[1] = (short)(param_2[1] * (int)param_1[1] >> 0xc);
  param_1[4] = (short)(param_2[1] * (int)param_1[4] >> 0xc);
  param_1[7] = (short)(param_2[1] * (int)param_1[7] >> 0xc);
  param_1[2] = (short)(param_2[2] * (int)param_1[2] >> 0xc);
  param_1[5] = (short)(param_2[2] * (int)param_1[5] >> 0xc);
  param_1[8] = (short)(param_2[2] * (int)param_1[8] >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a20540 @ 00a20540 */
/* ===================================================== */

void FUN_00a20540(short *param_1,short *param_2)

{
                    /* 0x20540  794   */
  *param_1 = (short)((int)*param_2 * (int)*param_1 >> 0xc);
  param_1[3] = (short)((int)*param_2 * (int)param_1[3] >> 0xc);
  param_1[6] = (short)((int)*param_2 * (int)param_1[6] >> 0xc);
  param_1[1] = (short)((int)param_2[1] * (int)param_1[1] >> 0xc);
  param_1[4] = (short)((int)param_2[1] * (int)param_1[4] >> 0xc);
  param_1[7] = (short)((int)param_1[7] * (int)param_2[1] >> 0xc);
  param_1[2] = (short)((int)param_1[2] * (int)param_2[2] >> 0xc);
  param_1[5] = (short)((int)param_2[2] * (int)param_1[5] >> 0xc);
  param_1[8] = (short)((int)param_2[2] * (int)param_1[8] >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a205f0 @ 00a205f0 */
/* ===================================================== */

void FUN_00a205f0(undefined2 *param_1,undefined2 *param_2)

{
  undefined2 uVar1;
  undefined2 *puVar2;
  int iVar3;
  int iVar4;
  
                    /* 0x205f0  359   */
  iVar3 = 3;
  do {
    iVar4 = 3;
    puVar2 = param_1;
    do {
      uVar1 = *puVar2;
      puVar2 = puVar2 + 3;
      *param_2 = uVar1;
      param_2 = param_2 + 1;
      iVar4 = iVar4 + -1;
    } while (iVar4 != 0);
    param_1 = param_1 + 1;
    iVar3 = iVar3 + -1;
  } while (iVar3 != 0);
  return;
}



/* ===================================================== */
/* FUN_00a20630 @ 00a20630 */
/* ===================================================== */

void FUN_00a20630(ulonglong *param_1,uint *param_2,undefined2 *param_3)

{
  ulonglong uVar1;
  ulonglong uVar2;
  ulonglong uVar3;
  ulonglong uVar4;
  undefined8 uVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  ulonglong uVar8;
  int local_34;
  int local_30;
  int local_2c;
  undefined4 local_24;
  undefined4 uStack_20;
  undefined4 local_1c;
  undefined4 uStack_18;
  short local_14;
  short local_12;
  short local_10;
  undefined2 local_e;
  short local_c;
  short local_a;
  short local_8;
  undefined2 local_6;
  
                    /* 0x20630  360   */
  local_14 = *(short *)((int)param_1 + 0x14);
  local_10 = *(short *)((int)param_1 + 0x1c);
  local_12 = (short)param_1[3];
  local_c = (short)param_2[5];
  local_a = (short)param_2[6];
  local_8 = (short)param_2[7];
  local_e = 0;
  local_6 = 0;
  if (MMXInstalled == 0) {
    *param_3 = (short)((int)*(short *)((int)param_2 + 6) * (int)*(short *)((int)param_1 + 2) +
                       (int)(short)param_2[3] * (int)*(short *)((int)param_1 + 4) +
                       (int)(short)*param_2 * (int)(short)*param_1 >> 0xc);
    param_3[3] = (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_1 + 10) * (int)(short)param_2[3] +
                         (int)*(short *)((int)param_2 + 6) * (int)(short)param_1[1] >> 0xc);
    param_3[6] = (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 0xc) +
                         (int)*(short *)((int)param_1 + 0xe) * (int)*(short *)((int)param_2 + 6) +
                         (int)(short)param_1[2] * (int)(short)param_2[3] >> 0xc);
    param_3[1] = (short)((int)(short)*param_1 * (int)*(short *)((int)param_2 + 2) +
                         (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 4) +
                         (int)(short)param_2[2] * (int)*(short *)((int)param_1 + 2) >> 0xc);
    param_3[4] = (short)((int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 10) +
                         (int)(short)param_2[2] * (int)(short)param_1[1] +
                         (int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 6) >>
                        0xc);
    param_3[7] = (short)((int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 0xc) +
                         (int)(short)param_1[2] * (int)*(short *)((int)param_2 + 0xe) +
                         (int)*(short *)((int)param_1 + 0xe) * (int)(short)param_2[2] >> 0xc);
    param_3[2] = (short)((int)(short)*param_1 * (int)(short)param_2[1] +
                         (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 2) +
                         (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 4) >> 0xc);
    param_3[5] = (short)((int)(short)param_2[1] * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_2 + 10) * (int)(short)param_1[1] +
                         (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 10) >> 0xc);
    param_3[8] = (short)((int)(short)param_2[4] * (int)(short)param_1[2] +
                         (int)(short)param_2[1] * (int)*(short *)((int)param_1 + 0xc) +
                         (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 0xe) >>
                        0xc);
  }
  else {
    local_24 = 0xffffffff;
    uStack_20 = 0xffff;
    local_1c = 0xffff;
    uStack_18 = 0;
    uVar1 = *param_1 & 0xffffffffffff;
    uVar2 = *(ulonglong *)((int)param_1 + 6) & 0xffffffffffff;
    uVar4 = (ulonglong)*param_2 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 6) << 0x10);
    uVar3 = *(ulonglong *)((int)param_1 + 0xc) & 0xffffffffffff;
    uVar8 = ((ulonglong)param_2[3] & 0xffff) << 0x20;
    uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
    uVar4 = uVar4 | uVar8;
    uVar7 = pmaddwd(uVar4,uVar2);
    uVar6 = pmaddwd(uVar4,uVar3);
    *param_3 = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
    param_3[3] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
    param_3[6] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
    uVar4 = (ulonglong)*(uint *)((int)param_2 + 2) & 0xffff | (ulonglong)(param_2[2] << 0x10);
    uVar8 = ((ulonglong)*(uint *)((int)param_2 + 0xe) & 0xffff) << 0x20;
    uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
    uVar4 = uVar4 | uVar8;
    uVar7 = pmaddwd(uVar4,uVar2);
    uVar6 = pmaddwd(uVar4,uVar3);
    param_3[1] = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
    param_3[4] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
    param_3[7] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
    uVar4 = (ulonglong)param_2[1] & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 10) << 0x10);
    uVar8 = ((ulonglong)param_2[4] & 0xffff) << 0x20;
    uVar5 = pmaddwd(uVar4 | uVar8,uVar1);
    uVar4 = uVar4 | uVar8;
    uVar7 = pmaddwd(uVar4,uVar2);
    uVar6 = pmaddwd(uVar4,uVar3);
    param_3[2] = (short)((int)uVar5 + (int)((ulonglong)uVar5 >> 0x20) >> 0xc);
    param_3[5] = (short)((int)uVar7 + (int)((ulonglong)uVar7 >> 0x20) >> 0xc);
    param_3[8] = (short)((int)uVar6 + (int)((ulonglong)uVar6 >> 0x20) >> 0xc);
  }
  FUN_00a1f8f0(param_1,&local_c,&local_34);
  local_34 = local_34 + local_14;
  local_2c = local_2c + local_10;
  local_30 = local_30 + local_12;
  FUN_00a203e0(param_3,&local_34);
  return;
}



/* ===================================================== */
/* FUN_00a20a00 @ 00a20a00 */
/* ===================================================== */

void FUN_00a20a00(ulonglong *param_1,uint *param_2,undefined2 *param_3)

{
  int iVar1;
  int iVar2;
  ulonglong uVar3;
  ulonglong uVar4;
  ulonglong uVar5;
  ulonglong uVar6;
  ulonglong uVar7;
  undefined8 uVar8;
  undefined8 uVar9;
  undefined8 uVar10;
  ulonglong uVar11;
  uint local_34;
  uint local_30;
  uint local_2c;
  undefined4 local_28;
  int local_24;
  int local_20;
  int local_1c;
  undefined4 local_14;
  undefined4 uStack_10;
  undefined4 local_c;
  undefined4 uStack_8;
  
                    /* 0x20a00  361   */
  iVar1 = *(int *)((int)param_1 + 0x14);
  iVar2 = *(int *)((int)param_1 + 0x1c);
  uVar3 = param_1[3];
  local_34 = param_2[5];
  local_30 = param_2[6];
  local_2c = param_2[7];
  local_28 = 0;
  if (MMXInstalled == 0) {
    *param_3 = (short)((int)(short)param_2[3] * (int)*(short *)((int)param_1 + 4) +
                       (int)(short)*param_2 * (int)(short)*param_1 +
                       (int)*(short *)((int)param_2 + 6) * (int)*(short *)((int)param_1 + 2) >> 0xc)
    ;
    param_3[3] = (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_2 + 6) * (int)(short)param_1[1] +
                         (int)*(short *)((int)param_1 + 10) * (int)(short)param_2[3] >> 0xc);
    param_3[6] = (short)((int)(short)*param_2 * (int)*(short *)((int)param_1 + 0xc) +
                         (int)*(short *)((int)param_1 + 0xe) * (int)*(short *)((int)param_2 + 6) +
                         (int)(short)param_1[2] * (int)(short)param_2[3] >> 0xc);
    param_3[1] = (short)((int)(short)*param_1 * (int)*(short *)((int)param_2 + 2) +
                         (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 4) +
                         (int)(short)param_2[2] * (int)*(short *)((int)param_1 + 2) >> 0xc);
    param_3[4] = (short)((int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_2 + 0xe) * (int)*(short *)((int)param_1 + 10) +
                         (int)(short)param_2[2] * (int)(short)param_1[1] >> 0xc);
    param_3[7] = (short)((int)*(short *)((int)param_2 + 2) * (int)*(short *)((int)param_1 + 0xc) +
                         (int)(short)param_1[2] * (int)*(short *)((int)param_2 + 0xe) +
                         (int)*(short *)((int)param_1 + 0xe) * (int)(short)param_2[2] >> 0xc);
    param_3[2] = (short)((int)(short)*param_1 * (int)(short)param_2[1] +
                         (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 4) +
                         (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 2) >>
                        0xc);
    param_3[5] = (short)((int)(short)param_2[1] * (int)*(short *)((int)param_1 + 6) +
                         (int)*(short *)((int)param_2 + 10) * (int)(short)param_1[1] +
                         (int)(short)param_2[4] * (int)*(short *)((int)param_1 + 10) >> 0xc);
    param_3[8] = (short)((int)(short)param_2[4] * (int)(short)param_1[2] +
                         (int)(short)param_2[1] * (int)*(short *)((int)param_1 + 0xc) +
                         (int)*(short *)((int)param_2 + 10) * (int)*(short *)((int)param_1 + 0xe) >>
                        0xc);
  }
  else {
    local_14 = 0xffffffff;
    uStack_10 = 0xffff;
    local_c = 0xffff;
    uStack_8 = 0;
    uVar4 = *param_1 & 0xffffffffffff;
    uVar5 = *(ulonglong *)((int)param_1 + 6) & 0xffffffffffff;
    uVar7 = (ulonglong)*param_2 & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 6) << 0x10);
    uVar6 = *(ulonglong *)((int)param_1 + 0xc) & 0xffffffffffff;
    uVar11 = ((ulonglong)param_2[3] & 0xffff) << 0x20;
    uVar8 = pmaddwd(uVar7 | uVar11,uVar4);
    uVar7 = uVar7 | uVar11;
    uVar10 = pmaddwd(uVar7,uVar5);
    uVar9 = pmaddwd(uVar7,uVar6);
    *param_3 = (short)((int)uVar8 + (int)((ulonglong)uVar8 >> 0x20) >> 0xc);
    param_3[3] = (short)((int)uVar10 + (int)((ulonglong)uVar10 >> 0x20) >> 0xc);
    param_3[6] = (short)((int)uVar9 + (int)((ulonglong)uVar9 >> 0x20) >> 0xc);
    uVar7 = (ulonglong)*(uint *)((int)param_2 + 2) & 0xffff | (ulonglong)(param_2[2] << 0x10);
    uVar11 = ((ulonglong)*(uint *)((int)param_2 + 0xe) & 0xffff) << 0x20;
    uVar8 = pmaddwd(uVar7 | uVar11,uVar4);
    uVar7 = uVar7 | uVar11;
    uVar10 = pmaddwd(uVar7,uVar5);
    uVar9 = pmaddwd(uVar7,uVar6);
    param_3[1] = (short)((int)uVar8 + (int)((ulonglong)uVar8 >> 0x20) >> 0xc);
    param_3[4] = (short)((int)uVar10 + (int)((ulonglong)uVar10 >> 0x20) >> 0xc);
    param_3[7] = (short)((int)uVar9 + (int)((ulonglong)uVar9 >> 0x20) >> 0xc);
    uVar7 = (ulonglong)param_2[1] & 0xffff | (ulonglong)(uint)(*(int *)((int)param_2 + 10) << 0x10);
    uVar11 = ((ulonglong)param_2[4] & 0xffff) << 0x20;
    uVar8 = pmaddwd(uVar7 | uVar11,uVar4);
    uVar7 = uVar7 | uVar11;
    uVar10 = pmaddwd(uVar7,uVar5);
    uVar9 = pmaddwd(uVar7,uVar6);
    param_3[2] = (short)((int)uVar8 + (int)((ulonglong)uVar8 >> 0x20) >> 0xc);
    param_3[5] = (short)((int)uVar10 + (int)((ulonglong)uVar10 >> 0x20) >> 0xc);
    param_3[8] = (short)((int)uVar9 + (int)((ulonglong)uVar9 >> 0x20) >> 0xc);
  }
  FUN_00a1fba0(param_1,&local_34,&local_24);
  local_24 = local_24 + iVar1;
  local_20 = local_20 + (int)uVar3;
  local_1c = local_1c + iVar2;
  FUN_00a203e0(param_3,&local_24);
  return;
}



/* ===================================================== */
/* FUN_00a20dc0 @ 00a20dc0 */
/* ===================================================== */

void FUN_00a20dc0(short *param_1,short *param_2)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  
                    /* 0x20dc0  362   */
  iVar1 = (int)param_1[4] * (int)param_1[3] + (int)*param_1 * (int)param_1[1];
  *param_2 = *param_1;
  param_2[3] = param_1[3];
  param_2[2] = param_1[2];
  param_2[5] = param_1[5];
  param_2[6] = param_1[6];
  param_2[7] = param_1[7];
  param_2[8] = param_1[8];
  iVar2 = (int)param_1[1] - *param_1 * iVar1;
  iVar3 = (int)param_1[4] - iVar1 * param_1[3];
  iVar4 = iVar2 * iVar2 + iVar3 * iVar3;
  iVar1 = FUN_00a25c80(iVar4);
  param_2[1] = (short)(iVar2 / iVar1);
  iVar1 = FUN_00a25c80(iVar4);
  param_2[4] = (short)(iVar3 / iVar1);
  return;
}



/* ===================================================== */
/* FUN_00a20e70 @ 00a20e70 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a20e70(undefined4 *param_1)

{
                    /* 0x20e70  366   */
  EXT_011d0870 = *param_1;
  DAT_011d0874 = param_1[1];
  _DAT_011d0878 = param_1[2];
  DAT_011d087c._0_4_ = param_1[3];
  DAT_011d087c._4_2_ = *(undefined2 *)(param_1 + 4);
  return;
}



/* ===================================================== */
/* FUN_00a20eb0 @ 00a20eb0 */
/* ===================================================== */

void FUN_00a20eb0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &EXT_011d0850;
                    /* 0x20eb0  367   */
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = *param_1;
    param_1 = param_1 + 1;
    puVar2 = puVar2 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a20ed0 @ 00a20ed0 */
/* ===================================================== */

void FUN_00a20ed0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &EXT_011d0828;
                    /* 0x20ed0  368   */
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = *param_1;
    param_1 = param_1 + 1;
    puVar2 = puVar2 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a20ef0 @ 00a20ef0 */
/* ===================================================== */

void FUN_00a20ef0(int param_1)

{
                    /* 0x20ef0  369   */
  DAT_011d0884 = *(undefined4 *)(param_1 + 0x14);
  DAT_011d0888 = *(undefined4 *)(param_1 + 0x18);
  DAT_011d088c = *(undefined4 *)(param_1 + 0x1c);
  return;
}



/* ===================================================== */
/* FUN_00a20f20 @ 00a20f20 */
/* ===================================================== */

void FUN_00a20f20(void)

{
  int iVar1;
  undefined4 *puVar2;
  undefined4 *puVar3;
  
                    /* 0x20f20  370   */
  if (DAT_011f0ecc < 0x1d) {
    puVar2 = &EXT_011d0870;
    puVar3 = (undefined4 *)(&DAT_011f08b0 + DAT_011f0ecc * 0x20);
    for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
      *puVar3 = *puVar2;
      puVar2 = puVar2 + 1;
      puVar3 = puVar3 + 1;
    }
    DAT_011f0ecc = DAT_011f0ecc + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a20f50 @ 00a20f50 */
/* ===================================================== */

void FUN_00a20f50(void)

{
  int iVar1;
  undefined4 *puVar2;
  undefined4 *puVar3;
  
                    /* 0x20f50  371   */
  if (0 < DAT_011f0ecc) {
    DAT_011f0ecc = DAT_011f0ecc + -1;
    puVar2 = (undefined4 *)(&DAT_011f08b0 + DAT_011f0ecc * 0x20);
    puVar3 = &EXT_011d0870;
    for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
      *puVar3 = *puVar2;
      puVar2 = puVar2 + 1;
      puVar3 = puVar3 + 1;
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a20f80 @ 00a20f80 */
/* ===================================================== */

void FUN_00a20f80(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &EXT_011d0870;
                    /* 0x20f80  372   */
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *param_1 = *puVar2;
    puVar2 = puVar2 + 1;
    param_1 = param_1 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a20fa0 @ 00a20fa0 */
/* ===================================================== */

void FUN_00a20fa0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &EXT_011d0850;
                    /* 0x20fa0  373   */
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *param_1 = *puVar2;
    puVar2 = puVar2 + 1;
    param_1 = param_1 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a20fc0 @ 00a20fc0 */
/* ===================================================== */

void FUN_00a20fc0(undefined4 *param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  puVar2 = &EXT_011d0828;
                    /* 0x20fc0  374   */
  for (iVar1 = 8; iVar1 != 0; iVar1 = iVar1 + -1) {
    *param_1 = *puVar2;
    puVar2 = puVar2 + 1;
    param_1 = param_1 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a20fe0 @ 00a20fe0 */
/* ===================================================== */

void FUN_00a20fe0(undefined1 param_1,undefined1 param_2,undefined1 param_3)

{
                    /* 0x20fe0  376   */
  DAT_011f0898._0_1_ = param_1;
  DAT_011f0898._1_1_ = param_2;
  DAT_011f0898._2_1_ = param_3;
  return;
}



/* ===================================================== */
/* FUN_00a21000 @ 00a21000 */
/* ===================================================== */

void FUN_00a21000(undefined1 param_1,undefined1 param_2,undefined1 param_3)

{
                    /* 0x21000  377   */
  DAT_011f0ec8._0_1_ = param_1;
  DAT_011f0ec8._1_1_ = param_2;
  DAT_011f0ec8._2_1_ = param_3;
  return;
}



/* ===================================================== */
/* FUN_00a21020 @ 00a21020 */
/* ===================================================== */

void FUN_00a21020(undefined4 param_1,undefined4 param_2)

{
                    /* 0x21020  378   */
  EXT_011cb414 = param_1;
  EXT_011cb410 = param_2;
  return;
}



/* ===================================================== */
/* FUN_00a21040 @ 00a21040 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a21040(int param_1)

{
  uint uVar1;
  uint uVar2;
  
                    /* 0x21040  379   */
  DAT_011d0820 = param_1;
  uVar2 = 0xffff8000;
  do {
    if (uVar2 == 0) {
      _EXT_011d0898 = 0x7fff;
    }
    else {
      uVar1 = (param_1 << 0xc) / (int)uVar2;
      if ((uVar1 & 0xffff0000) == 0) {
        *(short *)(&EXT_011d0898 + (uVar2 & 0xffff) * 2) = (short)uVar1;
      }
      else {
        *(undefined2 *)(&EXT_011d0898 + (uVar2 & 0xffff) * 2) = 0x7fff;
      }
    }
    uVar2 = uVar2 + 1;
  } while ((int)uVar2 < 0x8000);
  FUN_00a1dbc0(param_1);
  return;
}



/* ===================================================== */
/* FUN_00a210b0 @ 00a210b0 */
/* ===================================================== */

void FUN_00a210b0(undefined4 *param_1,undefined4 *param_2)

{
                    /* 0x210b0  384   */
  *param_1 = EXT_011cb414;
  *param_2 = EXT_011cb410;
  return;
}



/* ===================================================== */
/* FUN_00a210d0 @ 00a210d0 */
/* ===================================================== */

undefined4 FUN_00a210d0(void)

{
                    /* 0x210d0  385   */
  return DAT_011d0820;
}



/* ===================================================== */
/* FUN_00a210e0 @ 00a210e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

int FUN_00a210e0(ulonglong *param_1,int *param_2,undefined4 *param_3,undefined4 *param_4)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int iVar5;
  undefined8 uVar6;
  undefined8 uVar7;
  ulonglong uVar8;
  
                    /* 0x210e0  386   */
  *param_3 = 1;
  *param_4 = 0;
  if (MMXInstalled == 0) {
    iVar4 = (int)*(short *)((int)param_1 + 2);
    iVar3 = (int)(short)*param_1;
    iVar5 = (int)*(short *)((int)param_1 + 4);
    iVar2 = ((short)EXT_011d0870 * iVar3 + EXT_011d0870._2_2_ * iVar4 + (short)DAT_011d0874 * iVar5
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d0874._2_2_ * iVar3 + DAT_011d0878 * iVar4 + DAT_011d087a * iVar5 >> 0xc) +
            DAT_011d0888;
    _EXT_011f0e90 =
         ((short)DAT_011d087c * iVar3 + DAT_011d087c._2_2_ * iVar4 + DAT_011d087c._4_2_ * iVar5 >>
         0xc) + DAT_011d088c;
  }
  else {
    uVar8 = *param_1 & 0xffffffffffff | DAT_00a2a0d0;
    uVar6 = pmaddwd(uVar8,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar7 = pmaddwd(uVar8,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar6 >> 0xc) + (int)((longlong)uVar6 >> 0x2c);
    uVar6 = pmaddwd(uVar8,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    _EXT_011f0e90 = ((int)uVar6 >> 0xc) + (int)((longlong)uVar6 >> 0x2c);
  }
  _EXT_011f0e90 = _EXT_011f0e90 & 0xffff;
  _EXT_011d0890 =
       ((iVar1 * *(short *)(&EXT_011d0898 + _EXT_011f0e90 * 2) & 0xfffff000U) +
       EXT_011cb410 * 0x1000) * 0x10 +
       (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + _EXT_011f0e90 * 2) >> 0xc) +
                   (short)EXT_011cb414);
  *param_2 = _EXT_011d0890;
  return (int)_EXT_011f0e90 >> 2;
}



/* ===================================================== */
/* FUN_00a212a0 @ 00a212a0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

int FUN_00a212a0(ulonglong *param_1,ulonglong *param_2,ulonglong *param_3,int *param_4,int *param_5,
                int *param_6,undefined4 *param_7,undefined4 *param_8)

{
  int iVar1;
  int iVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  undefined8 uVar7;
  undefined8 uVar8;
  ulonglong uVar9;
  
                    /* 0x212a0  387   */
  *param_7 = 1;
  *param_8 = 0;
  if (MMXInstalled == 0) {
    iVar5 = (int)*(short *)((int)param_1 + 4);
    iVar4 = (int)(short)*param_1;
    iVar6 = (int)*(short *)((int)param_1 + 2);
    iVar2 = ((short)EXT_011d0870 * iVar4 + (short)DAT_011d0874 * iVar5 + EXT_011d0870._2_2_ * iVar6
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d0874._2_2_ * iVar4 + DAT_011d087a * iVar5 + DAT_011d0878 * iVar6 >> 0xc) +
            DAT_011d0888;
    uVar3 = ((short)DAT_011d087c * iVar4 + DAT_011d087c._4_2_ * iVar5 + DAT_011d087c._2_2_ * iVar6
            >> 0xc) + DAT_011d088c;
  }
  else {
    uVar9 = *param_1 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    uVar3 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
  }
  *param_4 = ((iVar1 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) & 0xfffff000U) +
             EXT_011cb410 * 0x1000) * 0x10 +
             (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) >> 0xc) +
                         (short)EXT_011cb414);
  if (MMXInstalled == 0) {
    iVar5 = (int)*(short *)((int)param_2 + 4);
    iVar6 = (int)*(short *)((int)param_2 + 2);
    iVar4 = (int)(short)*param_2;
    iVar2 = (EXT_011d0870._2_2_ * iVar6 + (short)DAT_011d0874 * iVar5 + (short)EXT_011d0870 * iVar4
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d0878 * iVar6 + DAT_011d087a * iVar5 + DAT_011d0874._2_2_ * iVar4 >> 0xc) +
            DAT_011d0888;
    uVar3 = (DAT_011d087c._2_2_ * iVar6 + DAT_011d087c._4_2_ * iVar5 + (short)DAT_011d087c * iVar4
            >> 0xc) + DAT_011d088c;
  }
  else {
    uVar9 = *param_2 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    uVar3 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
  }
  *param_5 = ((iVar1 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) & 0xfffff000U) +
             EXT_011cb410 * 0x1000) * 0x10 +
             (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) >> 0xc) +
                         (short)EXT_011cb414);
  if (MMXInstalled == 0) {
    iVar5 = (int)*(short *)((int)param_3 + 4);
    iVar6 = (int)*(short *)((int)param_3 + 2);
    iVar4 = (int)(short)*param_3;
    iVar2 = (EXT_011d0870._2_2_ * iVar6 + (short)DAT_011d0874 * iVar5 + (short)EXT_011d0870 * iVar4
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d0878 * iVar6 + DAT_011d087a * iVar5 + DAT_011d0874._2_2_ * iVar4 >> 0xc) +
            DAT_011d0888;
    _EXT_011f0e90 =
         (DAT_011d087c._2_2_ * iVar6 + DAT_011d087c._4_2_ * iVar5 + (short)DAT_011d087c * iVar4 >>
         0xc) + DAT_011d088c;
  }
  else {
    uVar9 = *param_3 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    _EXT_011f0e90 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
  }
  _EXT_011f0e90 = _EXT_011f0e90 & 0xffff;
  _EXT_011d0890 =
       ((iVar1 * *(short *)(&EXT_011d0898 + _EXT_011f0e90 * 2) & 0xfffff000U) +
       EXT_011cb410 * 0x1000) * 0x10 +
       (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + _EXT_011f0e90 * 2) >> 0xc) +
                   (short)EXT_011cb414);
  *param_6 = _EXT_011d0890;
  return (int)_EXT_011f0e90 >> 2;
}



/* ===================================================== */
/* FUN_00a21770 @ 00a21770 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4
FUN_00a21770(ulonglong *param_1,ulonglong *param_2,ulonglong *param_3,ulonglong *param_4,
            int *param_5,int *param_6,int *param_7,int *param_8,undefined4 *param_9,
            undefined4 *param_10)

{
  int iVar1;
  int iVar2;
  uint uVar3;
  int iVar4;
  int iVar5;
  int iVar6;
  undefined8 uVar7;
  undefined8 uVar8;
  ulonglong uVar9;
  int local_14;
  int local_10;
  uint local_c;
  
                    /* 0x21770  416   */
  *param_9 = 1;
  *param_10 = 0;
  if (MMXInstalled == 0) {
    iVar5 = (int)*(short *)((int)param_1 + 2);
    iVar4 = (int)(short)*param_1;
    iVar6 = (int)*(short *)((int)param_1 + 4);
    iVar2 = ((short)EXT_011d0870 * iVar4 + EXT_011d0870._2_2_ * iVar5 + (short)DAT_011d0874 * iVar6
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d0874._2_2_ * iVar4 + DAT_011d0878 * iVar5 + DAT_011d087a * iVar6 >> 0xc) +
            DAT_011d0888;
    uVar3 = ((short)DAT_011d087c * iVar4 + DAT_011d087c._2_2_ * iVar5 + DAT_011d087c._4_2_ * iVar6
            >> 0xc) + DAT_011d088c;
  }
  else {
    uVar9 = *param_1 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    uVar3 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    local_14 = iVar2;
    local_10 = iVar1;
    local_c = uVar3;
  }
  *param_5 = ((iVar1 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) & 0xfffff000U) +
             EXT_011cb410 * 0x1000) * 0x10 +
             (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) >> 0xc) +
                         (short)EXT_011cb414);
  if (MMXInstalled == 0) {
    iVar5 = (int)*(short *)((int)param_2 + 2);
    iVar6 = (int)*(short *)((int)param_2 + 4);
    iVar4 = (int)(short)*param_2;
    iVar2 = ((short)DAT_011d0874 * iVar6 + EXT_011d0870._2_2_ * iVar5 + (short)EXT_011d0870 * iVar4
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d087a * iVar6 + DAT_011d0878 * iVar5 + DAT_011d0874._2_2_ * iVar4 >> 0xc) +
            DAT_011d0888;
    uVar3 = (DAT_011d087c._4_2_ * iVar6 + DAT_011d087c._2_2_ * iVar5 + (short)DAT_011d087c * iVar4
            >> 0xc) + DAT_011d088c;
  }
  else {
    uVar9 = *param_2 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    uVar3 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    local_14 = iVar2;
    local_10 = iVar1;
    local_c = uVar3;
  }
  _EXT_011d0890 =
       ((iVar1 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) & 0xfffff000U) +
       EXT_011cb410 * 0x1000) * 0x10 +
       (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) >> 0xc) +
                   (short)EXT_011cb414);
  *param_6 = _EXT_011d0890;
  if (MMXInstalled == 0) {
    FUN_00a21d40(param_3,&local_14);
  }
  else {
    uVar9 = *param_3 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    local_14 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    local_10 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    local_c = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
  }
  *param_7 = (int)(short)((short)(local_14 * *(short *)(&EXT_011d0898 + (local_c & 0xffff) * 2) >>
                                 0xc) + (short)EXT_011cb414) +
             ((local_10 * *(short *)(&EXT_011d0898 + (local_c & 0xffff) * 2) & 0xfffff000U) +
             EXT_011cb410 * 0x1000) * 0x10;
  if (MMXInstalled == 0) {
    iVar5 = (int)*(short *)((int)param_4 + 4);
    iVar4 = (int)(short)*param_4;
    iVar6 = (int)*(short *)((int)param_4 + 2);
    iVar2 = ((short)DAT_011d0874 * iVar5 + (short)EXT_011d0870 * iVar4 + EXT_011d0870._2_2_ * iVar6
            >> 0xc) + DAT_011d0884;
    iVar1 = (DAT_011d087a * iVar5 + DAT_011d0874._2_2_ * iVar4 + DAT_011d0878 * iVar6 >> 0xc) +
            DAT_011d0888;
    uVar3 = (DAT_011d087c._4_2_ * iVar5 + (short)DAT_011d087c * iVar4 + DAT_011d087c._2_2_ * iVar6
            >> 0xc) + DAT_011d088c;
  }
  else {
    uVar9 = *param_4 & 0xffffffffffff | DAT_00a2a0d0;
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24((short)DAT_011d0874,
                                   CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                          (ulonglong)DAT_011d0884 << 0x30);
    uVar8 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                          (ulonglong)DAT_011d0888 << 0x30);
    iVar2 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
    uVar7 = pmaddwd(uVar9,(ulonglong)
                          CONCAT24(DAT_011d087c._4_2_,
                                   CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                          (ulonglong)DAT_011d088c << 0x30);
    iVar1 = ((int)uVar8 >> 0xc) + (int)((longlong)uVar8 >> 0x2c);
    uVar3 = ((int)uVar7 >> 0xc) + (int)((longlong)uVar7 >> 0x2c);
  }
  _EXT_011d0890 =
       ((iVar1 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) & 0xfffff000U) +
       EXT_011cb410 * 0x1000) * 0x10 +
       (int)(short)((short)(iVar2 * *(short *)(&EXT_011d0898 + (uVar3 & 0xffff) * 2) >> 0xc) +
                   (short)EXT_011cb414);
  *param_8 = _EXT_011d0890;
  return 1;
}



/* ===================================================== */
/* FUN_00a21d40 @ 00a21d40 */
/* ===================================================== */

void FUN_00a21d40(short *param_1,int *param_2)

{
  *param_2 = ((int)EXT_011d0870._2_2_ * (int)param_1[1] + (int)(short)DAT_011d0874 * (int)param_1[2]
              + (int)*param_1 * (int)(short)EXT_011d0870 >> 0xc) + DAT_011d0884;
  param_2[1] = ((int)DAT_011d0878 * (int)param_1[1] + (int)DAT_011d087a * (int)param_1[2] +
                (int)*param_1 * (int)DAT_011d0874._2_2_ >> 0xc) + DAT_011d0888;
  param_2[2] = ((int)DAT_011d087c._2_2_ * (int)param_1[1] +
                (int)DAT_011d087c._4_2_ * (int)param_1[2] + (int)*param_1 * (int)(short)DAT_011d087c
               >> 0xc) + DAT_011d088c;
  return;
}



/* ===================================================== */
/* FUN_00a21e00 @ 00a21e00 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a21e00(int param_1,int param_2,uint *param_3,undefined2 *param_4,undefined2 *param_5,
                 int param_6)

{
  short *psVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  uint uVar5;
  int iVar6;
  int iVar7;
  uint *puVar8;
  undefined8 uVar9;
  undefined8 uVar10;
  ulonglong uVar11;
  
                    /* 0x21e00  417   */
  *param_4 = 1;
  *param_5 = 0;
  if (0 < param_6) {
    psVar1 = (short *)(param_1 + 2);
    param_5 = (undefined2 *)param_6;
    puVar8 = param_3;
    do {
      if (MMXInstalled == 0) {
        iVar4 = (int)(short)*(ulonglong *)(psVar1 + -1);
        iVar7 = (int)psVar1[1];
        iVar6 = (int)*psVar1;
        iVar3 = (EXT_011d0870._2_2_ * iVar6 + (short)DAT_011d0874 * iVar7 +
                 (short)EXT_011d0870 * iVar4 >> 0xc) + DAT_011d0884;
        iVar2 = (DAT_011d0878 * iVar6 + DAT_011d087a * iVar7 + DAT_011d0874._2_2_ * iVar4 >> 0xc) +
                DAT_011d0888;
        uVar5 = (DAT_011d087c._2_2_ * iVar6 + DAT_011d087c._4_2_ * iVar7 +
                 (short)DAT_011d087c * iVar4 >> 0xc) + DAT_011d088c;
      }
      else {
        uVar11 = *(ulonglong *)(psVar1 + -1) & 0xffffffffffff | DAT_00a2a0d0;
        uVar9 = pmaddwd(uVar11,(ulonglong)
                               CONCAT24((short)DAT_011d0874,
                                        CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                               (ulonglong)DAT_011d0884 << 0x30);
        uVar10 = pmaddwd(uVar11,(ulonglong)
                                CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                (ulonglong)DAT_011d0888 << 0x30);
        iVar3 = ((int)uVar9 >> 0xc) + (int)((longlong)uVar9 >> 0x2c);
        uVar9 = pmaddwd(uVar11,(ulonglong)
                               CONCAT24(DAT_011d087c._4_2_,
                                        CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                               (ulonglong)DAT_011d088c << 0x30);
        iVar2 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
        uVar5 = ((int)uVar9 >> 0xc) + (int)((longlong)uVar9 >> 0x2c);
      }
      _EXT_011d0890 =
           ((iVar2 * *(short *)(&EXT_011d0898 + (uVar5 & 0xffff) * 2) & 0xfffff000U) +
           EXT_011cb410 * 0x1000) * 0x10 +
           (int)(short)((short)(iVar3 * *(short *)(&EXT_011d0898 + (uVar5 & 0xffff) * 2) >> 0xc) +
                       (short)EXT_011cb414);
      *(int *)((param_2 - (int)param_3) + -4 + (int)(puVar8 + 1)) = _EXT_011d0890;
      *puVar8 = uVar5 & 0xffff;
      psVar1 = psVar1 + 4;
      param_5 = (undefined2 *)((int)param_5 + -1);
      puVar8 = puVar8 + 1;
    } while (param_5 != (undefined2 *)0x0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a22010 @ 00a22010 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a22010(ulonglong *param_1,int *param_2,uint *param_3,undefined2 *param_4,uint *param_5)

{
  uint6 uVar1;
  uint6 uVar2;
  uint6 uVar3;
  int iVar4;
  int iVar5;
  ulonglong *puVar6;
  int iVar7;
  uint uVar8;
  int *piVar9;
  int *piVar10;
  ulonglong uVar11;
  undefined8 uVar12;
  ulonglong uVar13;
  undefined8 uVar14;
  ulonglong uVar15;
  ulonglong uVar16;
  undefined8 uVar17;
  
  iVar4 = (int)param_5;
                    /* 0x22010  418   */
  piVar9 = param_2;
  *param_4 = 0;
  param_4 = (undefined2 *)0x0;
  if (MMXInstalled == 0) {
    if (param_5 != (uint *)0x0) {
      param_5 = param_3;
      param_2 = (int *)iVar4;
      do {
        if (MMXInstalled == 0) {
          iVar5 = ((int)(short)EXT_011d0870 * (int)(short)*param_1 +
                   (int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 2) +
                   (int)(short)DAT_011d0874 * (int)*(short *)((int)param_1 + 4) >> 0xc) +
                  DAT_011d0884;
          iVar4 = ((int)DAT_011d0874._2_2_ * (int)(short)*param_1 +
                   (int)DAT_011d0878 * (int)*(short *)((int)param_1 + 2) +
                   (int)DAT_011d087a * (int)*(short *)((int)param_1 + 4) >> 0xc) + DAT_011d0888;
          uVar8 = ((int)(short)DAT_011d087c * (int)(short)*param_1 +
                   (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 2) +
                   (int)DAT_011d087c._4_2_ * (int)*(short *)((int)param_1 + 4) >> 0xc) +
                  DAT_011d088c;
        }
        else {
          uVar15 = *param_1 & 0xffffffffffff | DAT_00a2a0d0;
          uVar12 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24((short)DAT_011d0874,
                                           CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                                  (ulonglong)DAT_011d0884 << 0x30);
          uVar14 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                  (ulonglong)DAT_011d0888 << 0x30);
          iVar5 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
          uVar12 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24(DAT_011d087c._4_2_,
                                           CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                                  (ulonglong)DAT_011d088c << 0x30);
          iVar4 = ((int)uVar14 >> 0xc) + (int)((longlong)uVar14 >> 0x2c);
          uVar8 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
        }
        puVar6 = param_1 + 1;
        *piVar9 = ((iVar4 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) & 0xfffff000U) +
                  EXT_011cb410 * 0x1000) * 0x10 +
                  (int)(short)((short)(iVar5 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) >>
                                      0xc) + (short)EXT_011cb414);
        *param_5 = uVar8 & 0xffff;
        if (MMXInstalled == 0) {
          iVar5 = ((int)(short)EXT_011d0870 * (int)(short)*puVar6 +
                   (int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 10) +
                   (int)(short)DAT_011d0874 * (int)*(short *)((int)param_1 + 0xc) >> 0xc) +
                  DAT_011d0884;
          iVar4 = ((int)DAT_011d0874._2_2_ * (int)(short)*puVar6 +
                   (int)DAT_011d0878 * (int)*(short *)((int)param_1 + 10) +
                   (int)DAT_011d087a * (int)*(short *)((int)param_1 + 0xc) >> 0xc) + DAT_011d0888;
          uVar8 = ((int)(short)DAT_011d087c * (int)(short)*puVar6 +
                   (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 10) +
                   (int)DAT_011d087c._4_2_ * (int)*(short *)((int)param_1 + 0xc) >> 0xc) +
                  DAT_011d088c;
        }
        else {
          uVar15 = *puVar6 & 0xffffffffffff | DAT_00a2a0d0;
          uVar12 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24((short)DAT_011d0874,
                                           CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                                  (ulonglong)DAT_011d0884 << 0x30);
          uVar14 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                  (ulonglong)DAT_011d0888 << 0x30);
          iVar5 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
          uVar12 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24(DAT_011d087c._4_2_,
                                           CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                                  (ulonglong)DAT_011d088c << 0x30);
          iVar4 = ((int)uVar14 >> 0xc) + (int)((longlong)uVar14 >> 0x2c);
          uVar8 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
        }
        puVar6 = param_1 + 2;
        piVar10 = piVar9 + 2;
        piVar9[1] = ((iVar4 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) & 0xfffff000U) +
                    EXT_011cb410 * 0x1000) * 0x10 +
                    (int)(short)((short)(iVar5 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) >>
                                        0xc) + (short)EXT_011cb414);
        param_5[1] = uVar8 & 0xffff;
        if (MMXInstalled == 0) {
          iVar7 = (int)*(short *)((int)param_1 + 0x12);
          iVar5 = ((int)(short)EXT_011d0870 * (int)(short)*puVar6 + EXT_011d0870._2_2_ * iVar7 +
                   (int)(short)DAT_011d0874 * (int)*(short *)((int)param_1 + 0x14) >> 0xc) +
                  DAT_011d0884;
          iVar4 = ((int)DAT_011d0874._2_2_ * (int)(short)*puVar6 + DAT_011d0878 * iVar7 +
                   (int)DAT_011d087a * (int)*(short *)((int)param_1 + 0x14) >> 0xc) + DAT_011d0888;
          uVar8 = ((int)(short)DAT_011d087c * (int)(short)*puVar6 + DAT_011d087c._2_2_ * iVar7 +
                   (int)DAT_011d087c._4_2_ * (int)*(short *)((int)param_1 + 0x14) >> 0xc) +
                  DAT_011d088c;
        }
        else {
          uVar15 = *puVar6 & 0xffffffffffff | DAT_00a2a0d0;
          uVar12 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24((short)DAT_011d0874,
                                           CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                                  (ulonglong)DAT_011d0884 << 0x30);
          uVar14 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                  (ulonglong)DAT_011d0888 << 0x30);
          iVar5 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
          uVar12 = pmaddwd(uVar15,(ulonglong)
                                  CONCAT24(DAT_011d087c._4_2_,
                                           CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                                  (ulonglong)DAT_011d088c << 0x30);
          iVar4 = ((int)uVar14 >> 0xc) + (int)((longlong)uVar14 >> 0x2c);
          uVar8 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
        }
        param_1 = param_1 + 3;
        piVar9 = piVar9 + 3;
        _EXT_011d0890 =
             ((iVar4 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) & 0xfffff000U) +
             EXT_011cb410 * 0x1000) * 0x10 +
             (int)(short)((short)(iVar5 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) >> 0xc) +
                         (short)EXT_011cb414);
        *piVar10 = _EXT_011d0890;
        param_5[2] = uVar8 & 0xffff;
        param_5 = param_5 + 3;
        param_2 = (int *)((int)param_2 + -1);
      } while (param_2 != (int *)0x0);
      return;
    }
  }
  else {
    uVar15 = (ulonglong)DAT_011d0884;
    uVar1 = CONCAT24((short)DAT_011d0874,CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870));
    uVar2 = CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_));
    uVar11 = (ulonglong)DAT_011d0888;
    uVar3 = CONCAT24(DAT_011d087c._4_2_,CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c));
    uVar13 = (ulonglong)DAT_011d088c;
    for (iVar4 = (int)param_5 * 3; iVar4 != 0; iVar4 = iVar4 + -1) {
      uVar16 = param_1[(int)param_4] & 0xffffffffffff | DAT_00a2a0d0;
      uVar12 = pmaddwd(uVar16,(ulonglong)uVar1 | uVar15 << 0x30);
      uVar14 = pmaddwd(uVar16,(ulonglong)uVar2 | uVar11 << 0x30);
      uVar17 = pmaddwd(uVar16,(ulonglong)uVar3 | uVar13 << 0x30);
      uVar8 = ((int)uVar17 >> 0xc) + (int)((longlong)uVar17 >> 0x2c) & 0xffff;
      param_2[(int)param_4] =
           (((int)*(short *)(&EXT_011d0898 + uVar8 * 2) *
             (((int)uVar14 >> 0xc) + (int)((longlong)uVar14 >> 0x2c)) & 0xfffff000U) +
           EXT_011cb410 * 0x1000) * 0x10 +
           (int)(short)((short)((int)*(short *)(&EXT_011d0898 + uVar8 * 2) *
                                (((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c)) >> 0xc) +
                       (short)EXT_011cb414);
      param_3[(int)param_4] = uVar8;
      param_4 = (undefined2 *)((int)param_4 + 1);
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a226c0 @ 00a226c0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a226c0(ulonglong *param_1,int *param_2,int *param_3)

{
  uint6 uVar1;
  uint6 uVar2;
  uint6 uVar3;
  int iVar4;
  int iVar5;
  ulonglong *puVar6;
  int iVar7;
  uint uVar8;
  ulonglong uVar9;
  undefined8 uVar10;
  ulonglong uVar11;
  undefined8 uVar12;
  ulonglong uVar13;
  ulonglong uVar14;
  undefined8 uVar15;
  int local_8;
  
                    /* 0x226c0  795   */
  iVar4 = (int)param_3;
  local_8 = 0;
  if (MMXInstalled == 0) {
    if (param_3 != (int *)0x0) {
      param_3 = param_2;
      param_2 = (int *)iVar4;
      do {
        if (MMXInstalled == 0) {
          iVar7 = (int)*(short *)((int)param_1 + 4);
          iVar5 = ((int)(short)EXT_011d0870 * (int)(short)*param_1 + (short)DAT_011d0874 * iVar7 +
                   (int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 2) >> 0xc) +
                  DAT_011d0884;
          iVar4 = ((int)DAT_011d0874._2_2_ * (int)(short)*param_1 + DAT_011d087a * iVar7 +
                   (int)DAT_011d0878 * (int)*(short *)((int)param_1 + 2) >> 0xc) + DAT_011d0888;
          uVar8 = ((int)(short)DAT_011d087c * (int)(short)*param_1 + DAT_011d087c._4_2_ * iVar7 +
                   (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 2) >> 0xc) +
                  DAT_011d088c;
        }
        else {
          uVar13 = *param_1 & 0xffffffffffff | DAT_00a2a0d0;
          uVar10 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24((short)DAT_011d0874,
                                           CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                                  (ulonglong)DAT_011d0884 << 0x30);
          uVar12 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                  (ulonglong)DAT_011d0888 << 0x30);
          iVar5 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
          uVar10 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24(DAT_011d087c._4_2_,
                                           CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                                  (ulonglong)DAT_011d088c << 0x30);
          iVar4 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
          uVar8 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
        }
        puVar6 = param_1 + 1;
        *param_3 = ((iVar4 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) & 0xfffff000U) +
                   EXT_011cb410 * 0x1000) * 0x10 +
                   (int)(short)((short)(iVar5 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) >>
                                       0xc) + (short)EXT_011cb414);
        if (MMXInstalled == 0) {
          iVar7 = (int)*(short *)((int)param_1 + 0xc);
          iVar5 = ((int)(short)EXT_011d0870 * (int)(short)*puVar6 + (short)DAT_011d0874 * iVar7 +
                   (int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 10) >> 0xc) +
                  DAT_011d0884;
          iVar4 = ((int)DAT_011d0874._2_2_ * (int)(short)*puVar6 + DAT_011d087a * iVar7 +
                   (int)DAT_011d0878 * (int)*(short *)((int)param_1 + 10) >> 0xc) + DAT_011d0888;
          uVar8 = ((int)(short)DAT_011d087c * (int)(short)*puVar6 + DAT_011d087c._4_2_ * iVar7 +
                   (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 10) >> 0xc) +
                  DAT_011d088c;
        }
        else {
          uVar13 = *puVar6 & 0xffffffffffff | DAT_00a2a0d0;
          uVar10 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24((short)DAT_011d0874,
                                           CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                                  (ulonglong)DAT_011d0884 << 0x30);
          uVar12 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                  (ulonglong)DAT_011d0888 << 0x30);
          iVar5 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
          uVar10 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24(DAT_011d087c._4_2_,
                                           CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                                  (ulonglong)DAT_011d088c << 0x30);
          iVar4 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
          uVar8 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
        }
        puVar6 = param_1 + 2;
        param_3[1] = ((iVar4 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) & 0xfffff000U) +
                     EXT_011cb410 * 0x1000) * 0x10 +
                     (int)(short)((short)(iVar5 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2)
                                         >> 0xc) + (short)EXT_011cb414);
        if (MMXInstalled == 0) {
          iVar7 = (int)*(short *)((int)param_1 + 0x14);
          iVar5 = ((int)(short)EXT_011d0870 * (int)(short)*puVar6 + (short)DAT_011d0874 * iVar7 +
                   (int)EXT_011d0870._2_2_ * (int)*(short *)((int)param_1 + 0x12) >> 0xc) +
                  DAT_011d0884;
          iVar4 = ((int)DAT_011d0874._2_2_ * (int)(short)*puVar6 + DAT_011d087a * iVar7 +
                   (int)DAT_011d0878 * (int)*(short *)((int)param_1 + 0x12) >> 0xc) + DAT_011d0888;
          uVar8 = ((int)(short)DAT_011d087c * (int)(short)*puVar6 + DAT_011d087c._4_2_ * iVar7 +
                   (int)DAT_011d087c._2_2_ * (int)*(short *)((int)param_1 + 0x12) >> 0xc) +
                  DAT_011d088c;
        }
        else {
          uVar13 = *puVar6 & 0xffffffffffff | DAT_00a2a0d0;
          uVar10 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24((short)DAT_011d0874,
                                           CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870)) |
                                  (ulonglong)DAT_011d0884 << 0x30);
          uVar12 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_)) |
                                  (ulonglong)DAT_011d0888 << 0x30);
          iVar5 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
          uVar10 = pmaddwd(uVar13,(ulonglong)
                                  CONCAT24(DAT_011d087c._4_2_,
                                           CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c)) |
                                  (ulonglong)DAT_011d088c << 0x30);
          iVar4 = ((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c);
          uVar8 = ((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c);
        }
        param_1 = param_1 + 3;
        _EXT_011d0890 =
             ((iVar4 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) & 0xfffff000U) +
             EXT_011cb410 * 0x1000) * 0x10 +
             (int)(short)((short)(iVar5 * *(short *)(&EXT_011d0898 + (uVar8 & 0xffff) * 2) >> 0xc) +
                         (short)EXT_011cb414);
        param_3[2] = _EXT_011d0890;
        param_3 = param_3 + 3;
        param_2 = (int *)((int)param_2 + -1);
      } while (param_2 != (int *)0x0);
      return;
    }
  }
  else {
    uVar13 = (ulonglong)DAT_011d0884;
    uVar1 = CONCAT24((short)DAT_011d0874,CONCAT22(EXT_011d0870._2_2_,(short)EXT_011d0870));
    uVar2 = CONCAT24(DAT_011d087a,CONCAT22(DAT_011d0878,DAT_011d0874._2_2_));
    uVar9 = (ulonglong)DAT_011d0888;
    uVar3 = CONCAT24(DAT_011d087c._4_2_,CONCAT22(DAT_011d087c._2_2_,(short)DAT_011d087c));
    uVar11 = (ulonglong)DAT_011d088c;
    for (iVar4 = (int)param_3 * 3; iVar4 != 0; iVar4 = iVar4 + -1) {
      uVar14 = param_1[local_8] & 0xffffffffffff | DAT_00a2a0d0;
      uVar10 = pmaddwd(uVar14,(ulonglong)uVar1 | uVar13 << 0x30);
      uVar12 = pmaddwd(uVar14,(ulonglong)uVar2 | uVar9 << 0x30);
      uVar15 = pmaddwd(uVar14,(ulonglong)uVar3 | uVar11 << 0x30);
      param_2[local_8] =
           (((int)*(short *)(&EXT_011d0898 +
                            (((int)uVar15 >> 0xc) + (int)((longlong)uVar15 >> 0x2c) & 0xffffU) * 2)
             * (((int)uVar12 >> 0xc) + (int)((longlong)uVar12 >> 0x2c)) & 0xfffff000U) +
           EXT_011cb410 * 0x1000) * 0x10 +
           (int)(short)((short)((int)*(short *)(&EXT_011d0898 +
                                               (((int)uVar15 >> 0xc) +
                                                (int)((longlong)uVar15 >> 0x2c) & 0xffffU) * 2) *
                                (((int)uVar10 >> 0xc) + (int)((longlong)uVar10 >> 0x2c)) >> 0xc) +
                       (short)EXT_011cb414);
      local_8 = local_8 + 1;
    }
  }
  return;
}



/* ===================================================== */
/* FUN_00a22d00 @ 00a22d00 */
/* ===================================================== */

void FUN_00a22d00(short *param_1,int *param_2,undefined4 *param_3)

{
                    /* 0x22d00  388   */
  *param_2 = ((int)EXT_011d0870._2_2_ * (int)param_1[1] + (int)(short)DAT_011d0874 * (int)param_1[2]
              + (int)*param_1 * (int)(short)EXT_011d0870 >> 0xc) + DAT_011d0884;
  param_2[1] = ((int)DAT_011d0878 * (int)param_1[1] + (int)DAT_011d087a * (int)param_1[2] +
                (int)*param_1 * (int)DAT_011d0874._2_2_ >> 0xc) + DAT_011d0888;
  param_2[2] = ((int)DAT_011d087c._2_2_ * (int)param_1[1] +
                (int)DAT_011d087c._4_2_ * (int)param_1[2] + (int)*param_1 * (int)(short)DAT_011d087c
               >> 0xc) + DAT_011d088c;
  *param_3 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a22dd0 @ 00a22dd0 */
/* ===================================================== */

void FUN_00a22dd0(short *param_1,short *param_2,uint param_3)

{
                    /* 0x22dd0  792   */
  for (param_3 = param_3 & 0xffff; param_3 != 0; param_3 = param_3 - 1) {
    *param_2 = (short)((int)EXT_011d0870._2_2_ * (int)param_1[1] +
                       (int)(short)DAT_011d0874 * (int)param_1[2] +
                       (int)*param_1 * (int)(short)EXT_011d0870 >> 0xc) + (short)DAT_011d0884;
    param_2[1] = (short)((int)DAT_011d0878 * (int)param_1[1] + (int)DAT_011d087a * (int)param_1[2] +
                         (int)*param_1 * (int)DAT_011d0874._2_2_ >> 0xc) + (short)DAT_011d0888;
    param_2[2] = (short)((int)DAT_011d087c._2_2_ * (int)param_1[1] +
                         (int)DAT_011d087c._4_2_ * (int)param_1[2] +
                         (int)*param_1 * (int)(short)DAT_011d087c >> 0xc) + (short)DAT_011d088c;
    param_2 = param_2 + 4;
    param_1 = param_1 + 4;
  }
  return;
}



/* ===================================================== */
/* FUN_00a22ec0 @ 00a22ec0 */
/* ===================================================== */

void FUN_00a22ec0(short *param_1,short *param_2,undefined4 *param_3)

{
                    /* 0x22ec0  389   */
  *param_2 = (short)((int)EXT_011d0870._2_2_ * (int)param_1[1] +
                     (int)(short)DAT_011d0874 * (int)param_1[2] +
                     (int)*param_1 * (int)(short)EXT_011d0870 >> 0xc) + (short)DAT_011d0884;
  param_2[1] = (short)((int)DAT_011d0878 * (int)param_1[1] + (int)DAT_011d087a * (int)param_1[2] +
                       (int)*param_1 * (int)DAT_011d0874._2_2_ >> 0xc) + (short)DAT_011d0888;
  param_2[2] = (short)((int)DAT_011d087c._2_2_ * (int)param_1[1] +
                       (int)DAT_011d087c._4_2_ * (int)param_1[2] +
                       (int)*param_1 * (int)(short)DAT_011d087c >> 0xc) + (short)DAT_011d088c;
  *param_3 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a22f90 @ 00a22f90 */
/* ===================================================== */

void FUN_00a22f90(undefined4 param_1,int *param_2)

{
                    /* 0x22f90  390   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,param_2);
  if (*param_2 < 0) {
    *param_2 = 0;
  }
  if (param_2[1] < 0) {
    param_2[1] = 0;
  }
  if (param_2[2] < 0) {
    param_2[2] = 0;
  }
  return;
}



/* ===================================================== */
/* FUN_00a22fd0 @ 00a22fd0 */
/* ===================================================== */

void FUN_00a22fd0(undefined4 param_1,int *param_2)

{
                    /* 0x22fd0  391   */
  FUN_00a1fba0(&EXT_011d0828,param_1,param_2);
  if (*param_2 < 0) {
    *param_2 = 0;
  }
  if (param_2[1] < 0) {
    param_2[1] = 0;
  }
  if (param_2[2] < 0) {
    param_2[2] = 0;
  }
  return;
}



/* ===================================================== */
/* FUN_00a23010 @ 00a23010 */
/* ===================================================== */

void FUN_00a23010(int *param_1,byte *param_2,int param_3,undefined1 *param_4)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  int iVar4;
  
                    /* 0x23010  392   */
  uVar1 = (int)((uint)*param_2 * *param_1) >> 0xc;
  uVar3 = (int)((uint)param_2[1] * param_1[1]) >> 0xc;
  uVar2 = (int)((uint)param_2[2] * param_1[2]) >> 0xc;
  if ((int)uVar1 < 0) {
    uVar1 = 0;
  }
  else if (0xff < (int)uVar1) {
    uVar1 = 0xff;
  }
  if ((int)uVar3 < 0) {
    uVar3 = 0;
  }
  else if (0xff < (int)uVar3) {
    uVar3 = 0xff;
  }
  if ((int)uVar2 < 0) {
    uVar2 = 0;
  }
  else if (0xff < (int)uVar2) {
    uVar2 = 0xff;
  }
  *param_4 = (char)uVar1;
  param_4[1] = (char)uVar3;
  param_4[2] = (char)uVar2;
  param_4[3] = param_2[3];
  iVar4 = 0x1000 - param_3;
  *param_4 = (char)((int)(param_3 * (DAT_011f0ec8 & 0xff) + (uVar1 & 0xff) * iVar4) >> 0xc);
  param_4[1] = (char)((int)(param_3 * (DAT_011f0ec8 >> 8 & 0xff) + (uVar3 & 0xff) * iVar4) >> 0xc);
  param_4[2] = (char)((int)(param_3 * (DAT_011f0ec8 >> 0x10 & 0xff) + (uVar2 & 0xff) * iVar4) >> 0xc
                     );
  return;
}



/* ===================================================== */
/* FUN_00a23110 @ 00a23110 */
/* ===================================================== */

void FUN_00a23110(byte *param_1,int param_2,undefined1 *param_3)

{
  int iVar1;
  
                    /* 0x23110  393   */
  iVar1 = 0x1000 - param_2;
  *param_3 = (char)((int)((uint)*param_1 * iVar1 + param_2 * (DAT_011f0ec8 & 0xff)) >> 0xc);
  param_3[1] = (char)((int)((uint)param_1[1] * iVar1 + param_2 * (DAT_011f0ec8 >> 8 & 0xff)) >> 0xc)
  ;
  param_3[2] = (char)((int)((uint)param_1[2] * iVar1 + param_2 * (DAT_011f0ec8 >> 0x10 & 0xff)) >>
                     0xc);
  return;
}



/* ===================================================== */
/* FUN_00a23190 @ 00a23190 */
/* ===================================================== */

void FUN_00a23190(byte *param_1,byte *param_2,byte *param_3,int param_4,undefined1 *param_5,
                 undefined1 *param_6,undefined1 *param_7)

{
  int iVar1;
  
                    /* 0x23190  394   */
  iVar1 = 0x1000 - param_4;
  *param_5 = (char)((int)((uint)*param_1 * iVar1 + param_4 * (DAT_011f0ec8 & 0xff)) >> 0xc);
  param_5[1] = (char)((int)((uint)param_1[1] * iVar1 + param_4 * (DAT_011f0ec8 >> 8 & 0xff)) >> 0xc)
  ;
  param_5[2] = (char)((int)((uint)param_1[2] * iVar1 + param_4 * (DAT_011f0ec8 >> 0x10 & 0xff)) >>
                     0xc);
  *param_6 = (char)((int)((uint)*param_2 * iVar1 + param_4 * (DAT_011f0ec8 & 0xff)) >> 0xc);
  param_6[1] = (char)((int)((uint)param_2[1] * iVar1 + param_4 * (DAT_011f0ec8 >> 8 & 0xff)) >> 0xc)
  ;
  param_6[2] = (char)((int)((uint)param_2[2] * iVar1 + param_4 * (DAT_011f0ec8 >> 0x10 & 0xff)) >>
                     0xc);
  *param_7 = (char)((int)((uint)*param_3 * iVar1 + param_4 * (DAT_011f0ec8 & 0xff)) >> 0xc);
  param_7[1] = (char)((int)((uint)param_3[1] * iVar1 + param_4 * (DAT_011f0ec8 >> 8 & 0xff)) >> 0xc)
  ;
  param_7[2] = (char)((int)((uint)param_3[2] * iVar1 + param_4 * (DAT_011f0ec8 >> 0x10 & 0xff)) >>
                     0xc);
  return;
}



/* ===================================================== */
/* FUN_00a232d0 @ 00a232d0 */
/* ===================================================== */

void FUN_00a232d0(int *param_1,int param_2,undefined1 *param_3)

{
  int iVar1;
  int iVar2;
  
                    /* 0x232d0  395   */
  iVar1 = *param_1;
  iVar2 = 0x1000 - param_2;
  if (iVar1 < 0) {
    *param_3 = 0;
  }
  else if (iVar1 < 0x1000) {
    *param_3 = (char)((int)(param_2 * (DAT_011f0ec8 & 0xff) + (iVar2 * iVar1 >> 4)) >> 0xc);
  }
  else {
    *param_3 = 0xff;
  }
  iVar1 = param_1[1];
  if (iVar1 < 0) {
    param_3[1] = 0;
  }
  else if (iVar1 < 0x1000) {
    param_3[1] = (char)((int)(param_2 * (DAT_011f0ec8 >> 8 & 0xff) + (iVar2 * iVar1 >> 4)) >> 0xc);
  }
  else {
    param_3[1] = 0xff;
  }
  iVar1 = param_1[2];
  if (iVar1 < 0) {
    param_3[2] = 0;
    return;
  }
  if (0xfff < iVar1) {
    param_3[2] = 0xff;
    return;
  }
  param_3[2] = (char)((int)(param_2 * (DAT_011f0ec8 >> 0x10 & 0xff) + (iVar2 * iVar1 >> 4)) >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a233b0 @ 00a233b0 */
/* ===================================================== */

undefined4 * FUN_00a233b0(int *param_1,undefined4 *param_2)

{
  int iVar1;
  undefined4 uVar2;
  
                    /* 0x233b0  396   */
  iVar1 = *param_1;
  __allmul(iVar1,iVar1 >> 0x1f,iVar1,iVar1 >> 0x1f);
  uVar2 = __allshr();
  *param_2 = uVar2;
  iVar1 = param_1[1];
  __allmul(iVar1,iVar1 >> 0x1f,iVar1,iVar1 >> 0x1f);
  uVar2 = __allshr();
  param_2[1] = uVar2;
  iVar1 = param_1[2];
  __allmul(iVar1,iVar1 >> 0x1f,iVar1,iVar1 >> 0x1f);
  uVar2 = __allshr();
  param_2[2] = uVar2;
  return param_2;
}



/* ===================================================== */
/* FUN_00a23410 @ 00a23410 */
/* ===================================================== */

void FUN_00a23410(int *param_1,int *param_2)

{
                    /* 0x23410  397   */
  *param_2 = *param_1 * *param_1;
  param_2[1] = param_1[1] * param_1[1];
  param_2[2] = param_1[2] * param_1[2];
  return;
}



/* ===================================================== */
/* FUN_00a23440 @ 00a23440 */
/* ===================================================== */

int * FUN_00a23440(short *param_1,int *param_2)

{
  int iVar1;
  
                    /* 0x23440  398   */
  iVar1 = (int)*param_1 * (int)*param_1;
  *param_2 = (int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc;
  iVar1 = (int)param_1[1] * (int)param_1[1];
  param_2[1] = (int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc;
  iVar1 = (int)param_1[2] * (int)param_1[2];
  param_2[2] = (int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc;
  return param_2;
}



/* ===================================================== */
/* FUN_00a23490 @ 00a23490 */
/* ===================================================== */

void FUN_00a23490(short *param_1,int *param_2)

{
                    /* 0x23490  399   */
  *param_2 = (int)*param_1 * (int)*param_1;
  param_2[1] = (int)param_1[1] * (int)param_1[1];
  param_2[2] = (int)param_1[2] * (int)param_1[2];
  return;
}



/* ===================================================== */
/* FUN_00a234c0 @ 00a234c0 */
/* ===================================================== */

undefined2 * FUN_00a234c0(short *param_1,undefined2 *param_2)

{
  int iVar1;
  
                    /* 0x234c0  400   */
  iVar1 = (int)*param_1 * (int)*param_1;
  *param_2 = (short)((int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc);
  iVar1 = (int)param_1[1] * (int)param_1[1];
  param_2[1] = (short)((int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc);
  iVar1 = (int)param_1[2] * (int)param_1[2];
  param_2[2] = (short)((int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc);
  return param_2;
}



/* ===================================================== */
/* FUN_00a23520 @ 00a23520 */
/* ===================================================== */

void FUN_00a23520(short *param_1,short *param_2)

{
                    /* 0x23520  401   */
  *param_2 = *param_1 * *param_1;
  param_2[1] = param_1[1] * param_1[1];
  param_2[2] = param_1[2] * param_1[2];
  return;
}



/* ===================================================== */
/* FUN_00a23560 @ 00a23560 */
/* ===================================================== */

void FUN_00a23560(undefined4 param_1,undefined1 *param_2)

{
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x23560  402   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  if (0xfff < local_10) {
    local_10 = 0xfff;
  }
  if (0xfff < local_c) {
    local_c = 0xfff;
  }
  if (0xfff < local_8) {
    local_8 = 0xfff;
  }
  *param_2 = (char)(local_10 >> 4);
  param_2[1] = (char)(local_c >> 4);
  param_2[2] = (char)(local_8 >> 4);
  return;
}



/* ===================================================== */
/* FUN_00a23650 @ 00a23650 */
/* ===================================================== */

void FUN_00a23650(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined1 *param_4,
                 undefined1 *param_5,undefined1 *param_6)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x23650  403   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a23810(&local_20,&local_10,1);
  iVar1 = local_10;
  if (0xfff < local_10) {
    iVar1 = 0xfff;
  }
  iVar3 = local_c;
  if (0xfff < local_c) {
    iVar3 = 0xfff;
  }
  iVar2 = local_8;
  if (0xfff < local_8) {
    iVar2 = 0xfff;
  }
  *param_4 = (char)(iVar1 >> 4);
  param_4[1] = (char)(iVar3 >> 4);
  param_4[2] = (char)(iVar2 >> 4);
  FUN_00a1f8f0(&EXT_011d0850,param_2,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a23810(&local_20,&local_10,1);
  iVar1 = local_10;
  if (0xfff < local_10) {
    iVar1 = 0xfff;
  }
  iVar3 = local_c;
  if (0xfff < local_c) {
    iVar3 = 0xfff;
  }
  iVar2 = local_8;
  if (0xfff < local_8) {
    iVar2 = 0xfff;
  }
  *param_5 = (char)(iVar1 >> 4);
  param_5[1] = (char)(iVar3 >> 4);
  param_5[2] = (char)(iVar2 >> 4);
  FUN_00a1f8f0(&EXT_011d0850,param_3,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a23810(&local_20,&local_10,1);
  if (0xfff < local_10) {
    local_10 = 0xfff;
  }
  if (0xfff < local_c) {
    local_c = 0xfff;
  }
  if (0xfff < local_8) {
    local_8 = 0xfff;
  }
  *param_6 = (char)(local_10 >> 4);
  param_6[1] = (char)(local_c >> 4);
  param_6[2] = (char)(local_8 >> 4);
  return;
}



/* ===================================================== */
/* FUN_00a23810 @ 00a23810 */
/* ===================================================== */

void FUN_00a23810(undefined4 param_1,int *param_2,char param_3)

{
  FUN_00a1fba0(&EXT_011d0828,param_1,param_2);
  if (*param_2 < 0) {
    *param_2 = 0;
  }
  if (param_2[1] < 0) {
    param_2[1] = 0;
  }
  if (param_2[2] < 0) {
    param_2[2] = 0;
  }
  if (param_3 != '\0') {
    *param_2 = *param_2 + (DAT_011f0898 & 0xff) * 0x10;
    param_2[1] = param_2[1] + (DAT_011f0898 >> 8 & 0xff) * 0x10;
    param_2[2] = param_2[2] + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  }
  return;
}



/* ===================================================== */
/* FUN_00a23890 @ 00a23890 */
/* ===================================================== */

void FUN_00a23890(undefined4 param_1,byte *param_2,int param_3,undefined1 *param_4)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  int iVar4;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x23890  404   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  uVar1 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_2) >> 0xc;
  uVar3 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + local_c) * (uint)param_2[1]) >> 0xc;
  uVar2 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + local_8) * (uint)param_2[2]) >> 0xc;
  if ((int)uVar1 < 0) {
    uVar1 = 0;
  }
  else if (0xff < (int)uVar1) {
    uVar1 = 0xff;
  }
  if ((int)uVar3 < 0) {
    uVar3 = 0;
  }
  else if (0xff < (int)uVar3) {
    uVar3 = 0xff;
  }
  if ((int)uVar2 < 0) {
    uVar2 = 0;
  }
  else if (0xff < (int)uVar2) {
    uVar2 = 0xff;
  }
  *param_4 = (char)uVar1;
  param_4[1] = (char)uVar3;
  param_4[2] = (char)uVar2;
  param_4[3] = param_2[3];
  iVar4 = 0x1000 - param_3;
  *param_4 = (char)((int)(param_3 * (DAT_011f0ec8 & 0xff) + iVar4 * (uVar1 & 0xff)) >> 0xc);
  param_4[1] = (char)((int)(param_3 * (DAT_011f0ec8 >> 8 & 0xff) + iVar4 * (uVar3 & 0xff)) >> 0xc);
  param_4[2] = (char)((int)(param_3 * (DAT_011f0ec8 >> 0x10 & 0xff) + iVar4 * (uVar2 & 0xff)) >> 0xc
                     );
  return;
}



/* ===================================================== */
/* FUN_00a23a20 @ 00a23a20 */
/* ===================================================== */

void FUN_00a23a20(undefined4 param_1,undefined4 param_2,undefined4 param_3,byte *param_4,int param_5
                 ,undefined1 *param_6,byte *param_7,undefined4 param_8)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  int iVar4;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x23a20  405   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  uVar1 = (int)((uint)*param_4 * local_10) >> 0xc;
  uVar3 = (int)((uint)param_4[1] * local_c) >> 0xc;
  uVar2 = (int)((uint)param_4[2] * local_8) >> 0xc;
  if ((int)uVar1 < 0) {
    uVar1 = 0;
  }
  else if (0xff < (int)uVar1) {
    uVar1 = 0xff;
  }
  if ((int)uVar3 < 0) {
    uVar3 = 0;
  }
  else if (0xff < (int)uVar3) {
    uVar3 = 0xff;
  }
  if ((int)uVar2 < 0) {
    uVar2 = 0;
  }
  else if (0xff < (int)uVar2) {
    uVar2 = 0xff;
  }
  *param_6 = (char)uVar1;
  param_6[1] = (char)uVar3;
  param_6[2] = (char)uVar2;
  param_6[3] = param_4[3];
  iVar4 = 0x1000 - param_5;
  *param_6 = (char)((int)(param_5 * (DAT_011f0ec8 & 0xff) + iVar4 * (uVar1 & 0xff)) >> 0xc);
  param_6[1] = (char)((int)(param_5 * (DAT_011f0ec8 >> 8 & 0xff) + iVar4 * (uVar3 & 0xff)) >> 0xc);
  param_6[2] = (char)((int)(param_5 * (DAT_011f0ec8 >> 0x10 & 0xff) + iVar4 * (uVar2 & 0xff)) >> 0xc
                     );
  FUN_00a1f8f0(&EXT_011d0850,param_2,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  FUN_00a23dd0(param_4,&local_10,param_7);
  *param_7 = (byte)((int)((uint)*param_7 * iVar4 + param_5 * (DAT_011f0ec8 & 0xff)) >> 0xc);
  param_7[1] = (byte)((int)(param_5 * (DAT_011f0ec8 >> 8 & 0xff) + iVar4 * (uint)param_7[1]) >> 0xc)
  ;
  param_7[2] = (byte)((int)(param_5 * (DAT_011f0ec8 >> 0x10 & 0xff) + iVar4 * (uint)param_7[2]) >>
                     0xc);
  FUN_00a23cf0(param_3,&local_20,0);
  FUN_00a23810(&local_20,&local_10,1);
  FUN_00a23dd0(param_4,&local_10,param_8);
  FUN_00a23e60(param_8,param_5,param_8);
  return;
}



/* ===================================================== */
/* FUN_00a23cf0 @ 00a23cf0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a23cf0(undefined4 param_1,int *param_2,int param_3)

{
  double dVar1;
  int iVar2;
  
  FUN_00a1f8f0();
  if (*param_2 < 0) {
    *param_2 = 0;
  }
  if (param_2[1] < 0) {
    param_2[1] = 0;
  }
  if (param_2[2] < 0) {
    param_2[2] = 0;
  }
  if (0 < param_3) {
    if (0x1f < param_3) {
      param_3 = 0x1f;
    }
    dVar1 = (double)(1 << ((byte)param_3 & 0x1f));
    Ordinal_3((double)*param_2 * _DAT_00a2a0e8,dVar1);
    iVar2 = __ftol();
    *param_2 = iVar2;
    Ordinal_3((double)param_2[1] * _DAT_00a2a0e8,dVar1);
    iVar2 = __ftol();
    param_2[1] = iVar2;
    Ordinal_3((double)param_2[2] * _DAT_00a2a0e8,dVar1);
    iVar2 = __ftol();
    param_2[2] = iVar2;
  }
  return;
}



/* ===================================================== */
/* FUN_00a23dd0 @ 00a23dd0 */
/* ===================================================== */

void FUN_00a23dd0(byte *param_1,int *param_2,undefined1 *param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  
  iVar1 = (int)((uint)*param_1 * *param_2) >> 0xc;
  iVar3 = (int)((uint)param_1[1] * param_2[1]) >> 0xc;
  iVar2 = (int)((uint)param_1[2] * param_2[2]) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  *param_3 = (char)iVar1;
  param_3[1] = (char)iVar3;
  param_3[2] = (char)iVar2;
  param_3[3] = param_1[3];
  return;
}



/* ===================================================== */
/* FUN_00a23e60 @ 00a23e60 */
/* ===================================================== */

void FUN_00a23e60(byte *param_1,int param_2,undefined1 *param_3)

{
  int iVar1;
  
  iVar1 = 0x1000 - param_2;
  *param_3 = (char)((int)(param_2 * (DAT_011f0ec8 & 0xff) + (uint)*param_1 * iVar1) >> 0xc);
  param_3[1] = (char)((int)(param_2 * (DAT_011f0ec8 >> 8 & 0xff) + (uint)param_1[1] * iVar1) >> 0xc)
  ;
  param_3[2] = (char)((int)(param_2 * (DAT_011f0ec8 >> 0x10 & 0xff) + (uint)param_1[2] * iVar1) >>
                     0xc);
  return;
}



/* ===================================================== */
/* FUN_00a23ee0 @ 00a23ee0 */
/* ===================================================== */

void FUN_00a23ee0(undefined4 param_1,byte *param_2,undefined1 *param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x23ee0  406   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  iVar1 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_2) >> 0xc;
  iVar3 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + local_c) * (uint)param_2[1]) >> 0xc;
  iVar2 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + local_8) * (uint)param_2[2]) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  *param_3 = (char)iVar1;
  param_3[1] = (char)iVar3;
  param_3[2] = (char)iVar2;
  param_3[3] = param_2[3];
  return;
}



/* ===================================================== */
/* FUN_00a24000 @ 00a24000 */
/* ===================================================== */

void FUN_00a24000(undefined4 param_1,undefined4 param_2,undefined4 param_3,byte *param_4,
                 undefined1 *param_5,undefined1 *param_6,undefined4 param_7)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x24000  407   */
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  iVar3 = (int)(local_c * (uint)param_4[1]) >> 0xc;
  iVar1 = (int)((uint)*param_4 * local_10) >> 0xc;
  iVar2 = (int)(local_8 * (uint)param_4[2]) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  *param_5 = (char)iVar1;
  param_5[1] = (char)iVar3;
  param_5[2] = (char)iVar2;
  param_5[3] = param_4[3];
  FUN_00a1f8f0(&EXT_011d0850,param_2,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  iVar1 = (int)((uint)*param_4 * local_10) >> 0xc;
  iVar3 = (int)(local_c * (uint)param_4[1]) >> 0xc;
  iVar2 = (int)(local_8 * (uint)param_4[2]) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  *param_6 = (char)iVar1;
  param_6[1] = (char)iVar3;
  param_6[2] = (char)iVar2;
  param_6[3] = param_4[3];
  FUN_00a23cf0(param_3,&local_20,0);
  FUN_00a23810(&local_20,&local_10,1);
  FUN_00a23dd0(param_4,&local_10,param_7);
  return;
}



/* ===================================================== */
/* FUN_00a24260 @ 00a24260 */
/* ===================================================== */

void FUN_00a24260(undefined4 param_1,byte *param_2,int param_3,undefined1 *param_4)

{
  uint uVar1;
  uint uVar2;
  uint uVar3;
  int iVar4;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x24260  408   */
  FUN_00a1fba0(&EXT_011d0828,param_1,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  uVar1 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_2) >> 0xc;
  uVar3 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + local_c) * (uint)param_2[1]) >> 0xc;
  uVar2 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + local_8) * (uint)param_2[2]) >> 0xc;
  if ((int)uVar1 < 0) {
    uVar1 = 0;
  }
  else if (0xff < (int)uVar1) {
    uVar1 = 0xff;
  }
  if ((int)uVar3 < 0) {
    uVar3 = 0;
  }
  else if (0xff < (int)uVar3) {
    uVar3 = 0xff;
  }
  if ((int)uVar2 < 0) {
    uVar2 = 0;
  }
  else if (0xff < (int)uVar2) {
    uVar2 = 0xff;
  }
  *param_4 = (char)uVar1;
  param_4[1] = (char)uVar3;
  param_4[2] = (char)uVar2;
  param_4[3] = param_2[3];
  iVar4 = 0x1000 - param_3;
  *param_4 = (char)((int)(param_3 * (DAT_011f0ec8 & 0xff) + iVar4 * (uVar1 & 0xff)) >> 0xc);
  param_4[1] = (char)((int)(param_3 * (DAT_011f0ec8 >> 8 & 0xff) + iVar4 * (uVar3 & 0xff)) >> 0xc);
  param_4[2] = (char)((int)(param_3 * (DAT_011f0ec8 >> 0x10 & 0xff) + iVar4 * (uVar2 & 0xff)) >> 0xc
                     );
  return;
}



/* ===================================================== */
/* FUN_00a243c0 @ 00a243c0 */
/* ===================================================== */

void FUN_00a243c0(undefined4 param_1,byte *param_2,undefined1 *param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x243c0  409   */
  FUN_00a1fba0(&EXT_011d0828,param_1,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  iVar1 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_2) >> 0xc;
  iVar3 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + local_c) * (uint)param_2[1]) >> 0xc;
  iVar2 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + local_8) * (uint)param_2[2]) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  *param_3 = (char)iVar1;
  param_3[1] = (char)iVar3;
  param_3[2] = (char)iVar2;
  param_3[3] = param_2[3];
  return;
}



/* ===================================================== */
/* FUN_00a244a0 @ 00a244a0 */
/* ===================================================== */

undefined4 FUN_00a244a0(void)

{
                    /* 0x244a0  410   */
  return 1;
}



/* ===================================================== */
/* FUN_00a244b0 @ 00a244b0 */
/* ===================================================== */

int FUN_00a244b0(int param_1,int param_2,int param_3)

{
                    /* 0x244b0  411   */
  return (param_1 + param_2 + param_3) / 0xc;
}



/* ===================================================== */
/* FUN_00a244e0 @ 00a244e0 */
/* ===================================================== */

int FUN_00a244e0(int param_1,int param_2,int param_3,int param_4)

{
                    /* 0x244e0  412   */
  param_4 = param_2 + param_1 + param_3 + param_4;
  return (int)(param_4 + (param_4 >> 0x1f & 0xfU)) >> 4;
}



/* ===================================================== */
/* FUN_00a24500 @ 00a24500 */
/* ===================================================== */

void FUN_00a24500(undefined4 param_1,undefined4 param_2,undefined4 *param_3)

{
  undefined4 uVar1;
  
                    /* 0x24500  413   */
  uVar1 = __allshr();
  *param_3 = uVar1;
  uVar1 = __allshr();
  param_3[1] = uVar1;
  uVar1 = __allshr();
  param_3[2] = uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a245a0 @ 00a245a0 */
/* ===================================================== */

void FUN_00a245a0(int *param_1,int *param_2,int *param_3)

{
                    /* 0x245a0  414   */
  *param_3 = param_1[1] * param_2[2] - param_2[1] * param_1[2];
  param_3[1] = *param_2 * param_1[2] - *param_1 * param_2[2];
  param_3[2] = *param_1 * param_2[1] - *param_2 * param_1[1];
  return;
}



/* ===================================================== */
/* FUN_00a245f0 @ 00a245f0 */
/* ===================================================== */

int FUN_00a245f0(int param_1)

{
  int iVar1;
  int iVar2;
  bool bVar3;
  
                    /* 0x245f0  415   */
  iVar1 = param_1 * 2;
  iVar2 = 1;
  if (param_1 < 0 == iVar1 < 0) {
    do {
      iVar2 = iVar2 + 1;
      bVar3 = iVar1 < 0;
      iVar1 = iVar1 * 2;
    } while (bVar3 == iVar1 < 0);
  }
  return iVar2;
}



/* ===================================================== */
/* FUN_00a24630 @ 00a24630 */
/* ===================================================== */

void FUN_00a24630(void)

{
                    /* 0x24630  419   */
  return;
}



/* ===================================================== */
/* FUN_00a24640 @ 00a24640 */
/* ===================================================== */

void FUN_00a24640(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                 undefined4 param_5,undefined4 param_6,undefined4 param_7,undefined4 param_8)

{
                    /* 0x24640  420   */
  FUN_00a212a0(param_1,param_2,param_3,param_4,param_5,param_6,param_7,param_8);
  return;
}



/* ===================================================== */
/* FUN_00a24670 @ 00a24670 */
/* ===================================================== */

void FUN_00a24670(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                 undefined4 param_5,undefined4 param_6,undefined4 param_7,undefined4 param_8,
                 undefined4 param_9,undefined4 param_10)

{
                    /* 0x24670  421   */
  FUN_00a21770(param_1,param_2,param_3,param_4,param_5,param_6,param_7,param_8,param_9,param_10);
  return;
}



/* ===================================================== */
/* FUN_00a246b0 @ 00a246b0 */
/* ===================================================== */

void FUN_00a246b0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                 undefined4 param_5,undefined4 param_6,undefined4 param_7,undefined4 param_8,
                 undefined4 param_9)

{
                    /* 0x246b0  424   */
  FUN_00a24730(param_1,param_2,param_3,param_4,param_5,param_6,param_7,param_8,param_9);
  return;
}



/* ===================================================== */
/* FUN_00a246f0 @ 00a246f0 */
/* ===================================================== */

void FUN_00a246f0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                 undefined4 param_5,undefined4 param_6,undefined4 param_7,undefined4 param_8,
                 undefined4 param_9,undefined4 param_10,undefined4 param_11)

{
                    /* 0x246f0  425   */
  FUN_00a24780(param_1,param_2,param_3,param_4,param_5,param_6,param_7,param_8,param_9,param_10,
               param_11);
  return;
}



/* ===================================================== */
/* FUN_00a24730 @ 00a24730 */
/* ===================================================== */

void FUN_00a24730(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 *param_4,
                 undefined4 *param_5,undefined4 *param_6,undefined4 param_7,undefined4 *param_8,
                 undefined4 param_9)

{
                    /* 0x24730  422   */
  FUN_00a212a0(param_1,param_2,param_3,param_4,param_5,param_6,param_7,param_9);
  *param_8 = 0;
  FUN_00a244a0(*param_4,*param_5,*param_6);
  return;
}



/* ===================================================== */
/* FUN_00a24780 @ 00a24780 */
/* ===================================================== */

void FUN_00a24780(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
                 undefined4 *param_5,undefined4 *param_6,undefined4 *param_7,undefined4 param_8,
                 undefined4 param_9,undefined4 *param_10,undefined4 param_11)

{
                    /* 0x24780  423   */
  FUN_00a21770(param_1,param_2,param_3,param_4,param_5,param_6,param_7,param_8,param_9,param_11);
  *param_10 = 0;
  FUN_00a244a0(*param_5,*param_6,*param_7);
  return;
}



/* ===================================================== */
/* FUN_00a247e0 @ 00a247e0 */
/* ===================================================== */

undefined4
FUN_00a247e0(undefined4 param_1,undefined4 param_2,byte *param_3,undefined4 param_4,
            undefined1 *param_5,undefined4 param_6)

{
  int iVar1;
  int iVar2;
  int iVar3;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x247e0  426   */
  param_6 = FUN_00a210e0(param_1,param_4,&param_4,param_6);
  FUN_00a1f8f0(&EXT_011d0850,param_2,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_20,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  iVar1 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_3) >> 0xc;
  iVar3 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + local_c) * (uint)param_3[1]) >> 0xc;
  iVar2 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + local_8) * (uint)param_3[2]) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  *param_5 = (char)iVar1;
  param_5[1] = (char)iVar3;
  param_5[2] = (char)iVar2;
  param_5[3] = param_3[3];
  return param_6;
}



/* ===================================================== */
/* FUN_00a24920 @ 00a24920 */
/* ===================================================== */

undefined4
FUN_00a24920(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
            undefined4 param_5,undefined4 param_6,byte *param_7,undefined4 param_8,
            undefined4 param_9,undefined4 param_10,undefined1 *param_11,undefined4 param_12,
            undefined4 param_13,undefined4 param_14)

{
  undefined4 uVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int local_60;
  int local_5c;
  int local_58;
  int local_50;
  int local_4c;
  int local_48;
  int local_40;
  int local_3c;
  int local_38;
  int local_30;
  int local_2c;
  int local_28;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x24920  427   */
  uVar1 = FUN_00a212a0(param_1,param_2,param_3,param_8,param_9,param_10,&param_14,param_14);
  FUN_00a1f8f0(&EXT_011d0850,param_4,&local_50);
  if (local_50 < 0) {
    local_50 = 0;
  }
  if (local_4c < 0) {
    local_4c = 0;
  }
  if (local_48 < 0) {
    local_48 = 0;
  }
  FUN_00a1f8f0(&EXT_011d0850,param_5,&local_40);
  if (local_40 < 0) {
    local_40 = 0;
  }
  if (local_3c < 0) {
    local_3c = 0;
  }
  if (local_38 < 0) {
    local_38 = 0;
  }
  FUN_00a1f8f0(&EXT_011d0850,param_6,&local_30);
  if (local_30 < 0) {
    local_30 = 0;
  }
  if (local_2c < 0) {
    local_2c = 0;
  }
  if (local_28 < 0) {
    local_28 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_50,&local_60);
  if (local_60 < 0) {
    local_60 = 0;
  }
  if (local_5c < 0) {
    local_5c = 0;
  }
  if (local_58 < 0) {
    local_58 = 0;
  }
  local_60 = local_60 + (DAT_011f0898 & 0xff) * 0x10;
  local_58 = local_58 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  local_5c = local_5c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  FUN_00a1fba0(&EXT_011d0828,&local_40,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  local_20 = local_20 + (DAT_011f0898 & 0xff) * 0x10;
  local_18 = local_18 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  local_1c = local_1c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  FUN_00a1fba0(&EXT_011d0828,&local_30,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  iVar2 = (int)(local_60 * (uint)*param_7) >> 0xc;
  iVar4 = (int)((uint)param_7[1] * local_5c) >> 0xc;
  iVar3 = (int)((uint)param_7[2] * local_58) >> 0xc;
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  if (iVar4 < 0) {
    iVar4 = 0;
  }
  else if (0xff < iVar4) {
    iVar4 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  *param_11 = (char)iVar2;
  param_11[2] = (char)iVar3;
  param_11[1] = (char)iVar4;
  param_11[3] = param_7[3];
  FUN_00a23dd0(param_7,&local_20,param_12);
  FUN_00a23dd0(param_7,&local_10,param_13);
  return uVar1;
}



/* ===================================================== */
/* FUN_00a24bf0 @ 00a24bf0 */
/* ===================================================== */

undefined4
FUN_00a24bf0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
            undefined4 param_5,undefined4 param_6,byte *param_7,undefined4 param_8,
            undefined4 param_9,undefined4 param_10,undefined1 *param_11,undefined4 param_12,
            undefined4 param_13,undefined4 param_14,undefined4 param_15)

{
  undefined4 uVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int local_60;
  int local_5c;
  int local_58;
  int local_50;
  int local_4c;
  int local_48;
  int local_40;
  int local_3c;
  int local_38;
  int local_30;
  int local_2c;
  int local_28;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x24bf0  428   */
  uVar1 = FUN_00a246b0(param_1,param_2,param_3,param_8,param_9,param_10,&param_15,param_14,param_15)
  ;
  FUN_00a1f8f0(&EXT_011d0850,param_4,&local_50);
  if (local_50 < 0) {
    local_50 = 0;
  }
  if (local_4c < 0) {
    local_4c = 0;
  }
  if (local_48 < 0) {
    local_48 = 0;
  }
  FUN_00a1f8f0(&EXT_011d0850,param_5,&local_40);
  if (local_40 < 0) {
    local_40 = 0;
  }
  if (local_3c < 0) {
    local_3c = 0;
  }
  if (local_38 < 0) {
    local_38 = 0;
  }
  FUN_00a1f8f0(&EXT_011d0850,param_6,&local_30);
  if (local_30 < 0) {
    local_30 = 0;
  }
  if (local_2c < 0) {
    local_2c = 0;
  }
  if (local_28 < 0) {
    local_28 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_50,&local_60);
  if (local_60 < 0) {
    local_60 = 0;
  }
  if (local_5c < 0) {
    local_5c = 0;
  }
  if (local_58 < 0) {
    local_58 = 0;
  }
  local_60 = local_60 + (DAT_011f0898 & 0xff) * 0x10;
  local_58 = local_58 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  local_5c = local_5c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  FUN_00a1fba0(&EXT_011d0828,&local_40,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  local_20 = local_20 + (DAT_011f0898 & 0xff) * 0x10;
  local_18 = local_18 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  local_1c = local_1c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  FUN_00a1fba0(&EXT_011d0828,&local_30,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  iVar2 = (int)((uint)*param_7 * local_60) >> 0xc;
  iVar4 = (int)((uint)param_7[1] * local_5c) >> 0xc;
  iVar3 = (int)((uint)param_7[2] * local_58) >> 0xc;
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  if (iVar4 < 0) {
    iVar4 = 0;
  }
  else if (0xff < iVar4) {
    iVar4 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  param_11[1] = (char)iVar4;
  *param_11 = (char)iVar2;
  param_11[2] = (char)iVar3;
  param_11[3] = param_7[3];
  FUN_00a23dd0(param_7,&local_20,param_12);
  FUN_00a23dd0(param_7,&local_10,param_13);
  return uVar1;
}



/* ===================================================== */
/* FUN_00a24ed0 @ 00a24ed0 */
/* ===================================================== */

undefined4 FUN_00a24ed0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
  int iVar1;
  int iVar2;
  int iVar3;
  byte *in_stack_0000001c;
  undefined4 in_stack_00000020;
  undefined4 in_stack_00000024;
  undefined4 in_stack_00000028;
  undefined1 *in_stack_0000002c;
  undefined4 in_stack_00000030;
  undefined4 in_stack_00000034;
  undefined4 in_stack_00000038;
  undefined4 in_stack_0000003c;
  int local_60;
  int local_5c;
  int local_58;
  int local_50;
  int local_4c;
  int local_48;
  int local_40;
  int local_3c;
  int local_38;
  int local_30;
  int local_2c;
  int local_28;
  int local_20;
  int local_1c;
  int local_18;
  int local_10;
  int local_c;
  int local_8;
  
                    /* 0x24ed0  429   */
  in_stack_0000003c =
       FUN_00a246b0(param_1,param_2,param_3,in_stack_00000020,in_stack_00000024,in_stack_00000028,
                    &stack0x00000038,in_stack_00000038,in_stack_0000003c);
  FUN_00a1f8f0(&EXT_011d0850,param_1,&local_50);
  if (local_50 < 0) {
    local_50 = 0;
  }
  if (local_4c < 0) {
    local_4c = 0;
  }
  if (local_48 < 0) {
    local_48 = 0;
  }
  FUN_00a1f8f0(&EXT_011d0850,param_2,&local_40);
  if (local_40 < 0) {
    local_40 = 0;
  }
  if (local_3c < 0) {
    local_3c = 0;
  }
  if (local_38 < 0) {
    local_38 = 0;
  }
  FUN_00a1f8f0(&EXT_011d0850,param_3,&local_30);
  if (local_30 < 0) {
    local_30 = 0;
  }
  if (local_2c < 0) {
    local_2c = 0;
  }
  if (local_28 < 0) {
    local_28 = 0;
  }
  FUN_00a1fba0(&EXT_011d0828,&local_50,&local_60);
  if (local_60 < 0) {
    local_60 = 0;
  }
  if (local_5c < 0) {
    local_5c = 0;
  }
  if (local_58 < 0) {
    local_58 = 0;
  }
  local_60 = local_60 + (DAT_011f0898 & 0xff) * 0x10;
  local_58 = local_58 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  local_5c = local_5c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  FUN_00a1fba0(&EXT_011d0828,&local_40,&local_20);
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (local_1c < 0) {
    local_1c = 0;
  }
  if (local_18 < 0) {
    local_18 = 0;
  }
  local_20 = local_20 + (DAT_011f0898 & 0xff) * 0x10;
  local_18 = local_18 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  local_1c = local_1c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  FUN_00a1fba0(&EXT_011d0828,&local_30,&local_10);
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (local_c < 0) {
    local_c = 0;
  }
  if (local_8 < 0) {
    local_8 = 0;
  }
  local_10 = local_10 + (DAT_011f0898 & 0xff) * 0x10;
  local_c = local_c + (DAT_011f0898 >> 8 & 0xff) * 0x10;
  local_8 = local_8 + (DAT_011f0898 >> 0x10 & 0xff) * 0x10;
  iVar1 = (int)((uint)*in_stack_0000001c * local_60) >> 0xc;
  iVar3 = (int)((uint)in_stack_0000001c[1] * local_5c) >> 0xc;
  iVar2 = (int)((uint)in_stack_0000001c[2] * local_58) >> 0xc;
  if (iVar1 < 0) {
    iVar1 = 0;
  }
  else if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  in_stack_0000002c[1] = (char)iVar3;
  *in_stack_0000002c = (char)iVar1;
  in_stack_0000002c[2] = (char)iVar2;
  in_stack_0000002c[3] = in_stack_0000001c[3];
  FUN_00a23dd0(in_stack_0000001c,&local_20,in_stack_00000030);
  FUN_00a23dd0(in_stack_0000001c,&local_10,in_stack_00000034);
  return in_stack_0000003c;
}



/* ===================================================== */
/* FUN_00a251a0 @ 00a251a0 */
/* ===================================================== */

undefined4
FUN_00a251a0(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,
            undefined4 param_5,undefined4 param_6)

{
  undefined4 uVar1;
  
                    /* 0x251a0  430   */
  uVar1 = FUN_00a210e0(param_1,param_4,&param_4,&param_1);
  FUN_00a251e0(param_2,param_3,param_4,param_5,param_6);
  return uVar1;
}



/* ===================================================== */
/* FUN_00a251e0 @ 00a251e0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a251e0(undefined4 param_1,byte *param_2,int param_3,undefined1 *param_4,int param_5)

{
  double dVar1;
  uint uVar2;
  uint uVar3;
  uint uVar4;
  int unaff_EBX;
  int unaff_EDI;
  int iVar5;
  int local_20;
  int local_10;
  int iStack_c;
  int iStack_8;
  
                    /* 0x251e0  431   */
  FUN_00a1f8f0();
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (0 < param_5) {
    if (0x1f < param_5) {
      param_5 = 0x1f;
    }
    dVar1 = (double)(1 << ((byte)param_5 & 0x1f));
    Ordinal_3((double)local_20 * _DAT_00a2a0e8,dVar1);
    __ftol();
    Ordinal_3((double)unaff_EBX * _DAT_00a2a0e8,dVar1);
    __ftol();
    Ordinal_3((double)unaff_EDI * _DAT_00a2a0e8,dVar1);
    __ftol();
  }
  FUN_00a1fba0();
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (iStack_c < 0) {
    iStack_c = 0;
  }
  if (iStack_8 < 0) {
    iStack_8 = 0;
  }
  uVar2 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_2) >> 0xc;
  uVar4 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + iStack_c) * (uint)param_2[1]) >> 0xc;
  uVar3 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + iStack_8) * (uint)param_2[2]) >> 0xc;
  if ((int)uVar2 < 0) {
    uVar2 = 0;
  }
  else if (0xff < (int)uVar2) {
    uVar2 = 0xff;
  }
  if ((int)uVar4 < 0) {
    uVar4 = 0;
  }
  else if (0xff < (int)uVar4) {
    uVar4 = 0xff;
  }
  if ((int)uVar3 < 0) {
    uVar3 = 0;
  }
  else if (0xff < (int)uVar3) {
    uVar3 = 0xff;
  }
  *param_4 = (char)uVar2;
  param_4[1] = (char)uVar4;
  param_4[2] = (char)uVar3;
  param_4[3] = param_2[3];
  iVar5 = 0x1000 - param_3;
  *param_4 = (char)((int)(param_3 * (DAT_011f0ec8 & 0xff) + (uVar2 & 0xff) * iVar5) >> 0xc);
  param_4[1] = (char)((int)(param_3 * (DAT_011f0ec8 >> 8 & 0xff) + (uVar4 & 0xff) * iVar5) >> 0xc);
  param_4[2] = (char)((int)(param_3 * (DAT_011f0ec8 >> 0x10 & 0xff) + (uVar3 & 0xff) * iVar5) >> 0xc
                     );
  return;
}



/* ===================================================== */
/* FUN_00a25410 @ 00a25410 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a25410(undefined4 param_1,byte *param_2,undefined1 *param_3,int param_4)

{
  double dVar1;
  int iVar2;
  int iVar3;
  int iVar4;
  int unaff_EBX;
  int unaff_EDI;
  int local_20;
  int local_10;
  int iStack_c;
  int iStack_8;
  
                    /* 0x25410  432   */
  FUN_00a1f8f0();
  if (local_20 < 0) {
    local_20 = 0;
  }
  if (0 < param_4) {
    if (0x1f < param_4) {
      param_4 = 0x1f;
    }
    dVar1 = (double)(1 << ((byte)param_4 & 0x1f));
    Ordinal_3((double)local_20 * _DAT_00a2a0e8,dVar1);
    __ftol();
    Ordinal_3((double)unaff_EBX * _DAT_00a2a0e8,dVar1);
    __ftol();
    Ordinal_3((double)unaff_EDI * _DAT_00a2a0e8,dVar1);
    __ftol();
  }
  FUN_00a1fba0();
  if (local_10 < 0) {
    local_10 = 0;
  }
  if (iStack_c < 0) {
    iStack_c = 0;
  }
  if (iStack_8 < 0) {
    iStack_8 = 0;
  }
  iVar2 = (int)(((DAT_011f0898 & 0xff) * 0x10 + local_10) * (uint)*param_2) >> 0xc;
  iVar4 = (int)(((DAT_011f0898 >> 8 & 0xff) * 0x10 + iStack_c) * (uint)param_2[1]) >> 0xc;
  iVar3 = (int)(((DAT_011f0898 >> 0x10 & 0xff) * 0x10 + iStack_8) * (uint)param_2[2]) >> 0xc;
  if (iVar2 < 0) {
    iVar2 = 0;
  }
  else if (0xff < iVar2) {
    iVar2 = 0xff;
  }
  if (iVar4 < 0) {
    iVar4 = 0;
  }
  else if (0xff < iVar4) {
    iVar4 = 0xff;
  }
  if (iVar3 < 0) {
    iVar3 = 0;
  }
  else if (0xff < iVar3) {
    iVar3 = 0xff;
  }
  *param_3 = (char)iVar2;
  param_3[1] = (char)iVar4;
  param_3[2] = (char)iVar3;
  param_3[3] = param_2[3];
  return;
}



/* ===================================================== */
/* FUN_00a255d0 @ 00a255d0 */
/* ===================================================== */

void FUN_00a255d0(int *param_1,int *param_2,int param_3,int param_4,undefined4 *param_5)

{
  undefined4 uVar1;
  int iVar2;
  int iVar3;
  longlong lVar4;
  longlong lVar5;
  
                    /* 0x255d0  433   */
  iVar2 = param_3 >> 0x1f;
  iVar3 = param_4 >> 0x1f;
  lVar4 = __allmul(*param_2,*param_2 >> 0x1f,param_4,iVar3);
  lVar5 = __allmul(*param_1,*param_1 >> 0x1f,param_3,iVar2);
  uVar1 = __alldiv(lVar4 + lVar5,0x1000,0);
  *param_5 = uVar1;
  lVar4 = __allmul(param_2[1],param_2[1] >> 0x1f,param_4,iVar3);
  lVar5 = __allmul(param_1[1],param_1[1] >> 0x1f,param_3,iVar2);
  uVar1 = __alldiv(lVar4 + lVar5,0x1000,0);
  param_5[1] = uVar1;
  lVar4 = __allmul(param_1[2],param_1[2] >> 0x1f,param_3,iVar2);
  lVar5 = __allmul(param_2[2],param_2[2] >> 0x1f,param_4,iVar3);
  uVar1 = __alldiv(lVar4 + lVar5,0x1000,0);
  param_5[2] = uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a256d0 @ 00a256d0 */
/* ===================================================== */

void FUN_00a256d0(short *param_1,short *param_2,int param_3,int param_4,undefined2 *param_5)

{
  undefined2 uVar1;
  int iVar2;
  int iVar3;
  longlong lVar4;
  longlong lVar5;
  
                    /* 0x256d0  434   */
  iVar2 = param_3 >> 0x1f;
  iVar3 = param_4 >> 0x1f;
  lVar4 = __allmul((int)*param_2,(int)*param_2 >> 0x1f,param_4,iVar3);
  lVar5 = __allmul((int)*param_1,(int)*param_1 >> 0x1f,param_3,iVar2);
  uVar1 = __alldiv(lVar4 + lVar5,0x1000,0);
  *param_5 = uVar1;
  lVar4 = __allmul((int)param_2[1],(int)param_2[1] >> 0x1f,param_4,iVar3);
  lVar5 = __allmul((int)param_1[1],(int)param_1[1] >> 0x1f,param_3,iVar2);
  uVar1 = __alldiv(lVar4 + lVar5,0x1000,0);
  param_5[1] = uVar1;
  lVar4 = __allmul((int)param_2[2],(int)param_2[2] >> 0x1f,param_4,iVar3);
  lVar5 = __allmul((int)param_1[2],(int)param_1[2] >> 0x1f,param_3,iVar2);
  uVar1 = __alldiv(lVar4 + lVar5,0x1000,0);
  param_5[2] = uVar1;
  return;
}



/* ===================================================== */
/* FUN_00a257e0 @ 00a257e0 */
/* ===================================================== */

void FUN_00a257e0(int *param_1,int *param_2,int param_3,int param_4,int *param_5)

{
                    /* 0x257e0  435   */
  *param_5 = param_3 * *param_1 + param_4 * *param_2;
  param_5[1] = param_2[1] * param_4 + param_1[1] * param_3;
  param_5[2] = param_1[2] * param_3 + param_2[2] * param_4;
  return;
}



/* ===================================================== */
/* FUN_00a25830 @ 00a25830 */
/* ===================================================== */

void FUN_00a25830(short *param_1,short *param_2,short param_3,short param_4,short *param_5)

{
                    /* 0x25830  436   */
  *param_5 = param_3 * *param_1 + param_4 * *param_2;
  param_5[1] = param_1[1] * param_3 + param_2[1] * param_4;
  param_5[2] = param_2[2] * param_4 + param_1[2] * param_3;
  return;
}



/* ===================================================== */
/* FUN_00a25890 @ 00a25890 */
/* ===================================================== */

void FUN_00a25890(byte *param_1,byte *param_2,int param_3,int param_4,undefined1 *param_5)

{
  int iVar1;
  
                    /* 0x25890  437  
                       0x25890  438   */
  iVar1 = (uint)*param_2 * param_4 + (uint)*param_1 * param_3;
  *param_5 = (char)((int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc);
  iVar1 = (uint)param_2[1] * param_4 + (uint)param_1[1] * param_3;
  param_5[1] = (char)((int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc);
  iVar1 = (uint)param_2[2] * param_4 + (uint)param_1[2] * param_3;
  param_5[2] = (char)((int)(iVar1 + (iVar1 >> 0x1f & 0xfffU)) >> 0xc);
  return;
}



/* ===================================================== */
/* FUN_00a25910 @ 00a25910 */
/* ===================================================== */

uint FUN_00a25910(int *param_1,int *param_2)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  
                    /* 0x25910  439   */
  iVar1 = *param_1;
  uVar2 = param_1[1] * param_1[1] + param_1[2] * param_1[2] + iVar1 * iVar1;
  if (uVar2 < 0x10000) {
    if (uVar2 < 0x100) {
      uVar2 = (uint)(ushort)(&DAT_011d0620)[uVar2];
      goto LAB_00a25987;
    }
    uVar3 = (uint)(ushort)(&DAT_011d0420)[uVar2 >> 8];
  }
  else if (uVar2 < 0x1000000) {
    uVar3 = (uint)(ushort)(&DAT_011f0c90)[uVar2 >> 0x10];
  }
  else {
    uVar3 = (uint)(ushort)(&DAT_011cb210)[uVar2 >> 0x18];
  }
  uVar2 = uVar2 / uVar3 + uVar3 >> 1;
LAB_00a25987:
  uVar2 = uVar2 & 0xffff;
  if (uVar2 == 0) {
    return 0;
  }
  *param_2 = (iVar1 << 0xc) / (int)uVar2;
  param_2[1] = (param_1[1] << 0xc) / (int)uVar2;
  param_2[2] = (param_1[2] << 0xc) / (int)uVar2;
  return uVar2;
}



/* ===================================================== */
/* FUN_00a259d0 @ 00a259d0 */
/* ===================================================== */

uint FUN_00a259d0(int *param_1,undefined2 *param_2)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  
                    /* 0x259d0  440   */
  iVar1 = *param_1;
  uVar2 = param_1[1] * param_1[1] + param_1[2] * param_1[2] + iVar1 * iVar1;
  if (uVar2 < 0x10000) {
    if (uVar2 < 0x100) {
      uVar2 = (uint)(ushort)(&DAT_011d0620)[uVar2];
      goto LAB_00a25a47;
    }
    uVar3 = (uint)(ushort)(&DAT_011d0420)[uVar2 >> 8];
  }
  else if (uVar2 < 0x1000000) {
    uVar3 = (uint)(ushort)(&DAT_011f0c90)[uVar2 >> 0x10];
  }
  else {
    uVar3 = (uint)(ushort)(&DAT_011cb210)[uVar2 >> 0x18];
  }
  uVar2 = uVar2 / uVar3 + uVar3 >> 1;
LAB_00a25a47:
  uVar2 = uVar2 & 0xffff;
  if (uVar2 == 0) {
    return 0;
  }
  *param_2 = (short)((iVar1 << 0xc) / (int)uVar2);
  param_2[1] = (short)((param_1[1] << 0xc) / (int)uVar2);
  param_2[2] = (short)((param_1[2] << 0xc) / (int)uVar2);
  return uVar2;
}



/* ===================================================== */
/* FUN_00a25a90 @ 00a25a90 */
/* ===================================================== */

uint FUN_00a25a90(short *param_1,undefined2 *param_2)

{
  uint uVar1;
  uint uVar2;
  int iVar3;
  
                    /* 0x25a90  441   */
  iVar3 = (int)*param_1;
  uVar1 = (int)param_1[1] * (int)param_1[1] + (int)param_1[2] * (int)param_1[2] + iVar3 * iVar3;
  if (uVar1 < 0x10000) {
    if (uVar1 < 0x100) {
      uVar1 = (uint)(ushort)(&DAT_011d0620)[uVar1];
      goto LAB_00a25b0a;
    }
    uVar2 = (uint)(ushort)(&DAT_011d0420)[uVar1 >> 8];
  }
  else if (uVar1 < 0x1000000) {
    uVar2 = (uint)(ushort)(&DAT_011f0c90)[uVar1 >> 0x10];
  }
  else {
    uVar2 = (uint)(ushort)(&DAT_011cb210)[uVar1 >> 0x18];
  }
  uVar1 = uVar1 / uVar2 + uVar2 >> 1;
LAB_00a25b0a:
  uVar1 = uVar1 & 0xffff;
  *param_2 = (short)((iVar3 << 0xc) / (int)uVar1);
  param_2[1] = (short)(((int)param_1[1] << 0xc) / (int)uVar1);
  param_2[2] = (short)(((int)param_1[2] << 0xc) / (int)uVar1);
  return uVar1;
}



/* ===================================================== */
/* FUN_00a25b50 @ 00a25b50 */
/* ===================================================== */

uint FUN_00a25b50(uint param_1)

{
  uint uVar1;
  
                    /* 0x25b50  442   */
  if (param_1 < 0x10000) {
    if (param_1 < 0x100) {
      return (uint)(ushort)(&DAT_011d0620)[param_1];
    }
    uVar1 = (uint)(ushort)(&DAT_011d0420)[param_1 >> 8];
  }
  else if (param_1 < 0x1000000) {
    uVar1 = (uint)(ushort)(&DAT_011f0c90)[param_1 >> 0x10];
  }
  else {
    uVar1 = (uint)(ushort)(&DAT_011cb210)[param_1 >> 0x18];
  }
  return param_1 / uVar1 + uVar1 >> 1 & 0xffff;
}



/* ===================================================== */
/* FUN_00a25bc0 @ 00a25bc0 */
/* ===================================================== */

void FUN_00a25bc0(int param_1,short *param_2,int param_3,int param_4)

{
  short *psVar1;
  short *psVar2;
  
                    /* 0x25bc0  445   */
  if (0 < param_3) {
    psVar1 = (short *)(param_1 + 2);
    psVar2 = param_2;
    do {
      psVar1[-1] = psVar1[-1] + (short)(param_4 * *psVar2 >> 0xc);
      *psVar1 = *psVar1 + (short)(*(short *)(((int)param_2 - param_1) + (int)psVar1) * param_4 >>
                                 0xc);
      psVar1[1] = psVar1[1] + (short)(psVar2[2] * param_4 >> 0xc);
      param_3 = param_3 + -1;
      psVar1 = psVar1 + 4;
      psVar2 = psVar2 + 4;
    } while (param_3 != 0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a25c20 @ 00a25c20 */
/* ===================================================== */

undefined4 FUN_00a25c20(int param_1)

{
                    /* 0x25c20  450  
                       0x25c20  452   */
  return (&DAT_011cb420)[param_1 + 0x400U & 0xfff];
}



/* ===================================================== */
/* FUN_00a25c40 @ 00a25c40 */
/* ===================================================== */

undefined4 FUN_00a25c40(uint param_1)

{
                    /* 0x25c40  451  
                       0x25c40  453   */
  return (&DAT_011cb420)[param_1 & 0xfff];
}



/* ===================================================== */
/* FUN_00a25c60 @ 00a25c60 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a25c60(int param_1)

{
                    /* 0x25c60  454   */
  log2((float10)param_1 * (float10)_DAT_00a2a0e8);
  __ftol();
  return;
}



/* ===================================================== */
/* FUN_00a25c80 @ 00a25c80 */
/* ===================================================== */

int FUN_00a25c80(uint param_1)

{
  uint uVar1;
  
                    /* 0x25c80  443  
                       0x25c80  455   */
  if (param_1 < 0x10000) {
    if (param_1 < 0x100) {
      return (uint)(ushort)(&DAT_011d0620)[param_1] << 6;
    }
    uVar1 = (uint)(ushort)(&DAT_011d0420)[param_1 >> 8];
  }
  else if (param_1 < 0x1000000) {
    uVar1 = (uint)(ushort)(&DAT_011f0c90)[param_1 >> 0x10];
  }
  else {
    uVar1 = (uint)(ushort)(&DAT_011cb210)[param_1 >> 0x18];
  }
  return (param_1 / uVar1 + uVar1 >> 1 & 0xffff) << 6;
}



/* ===================================================== */
/* FUN_00a25cf0 @ 00a25cf0 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a25cf0(int param_1)

{
                    /* 0x25cf0  456   */
  fpatan((float10)param_1 * (float10)_DAT_00a2a0e8,(float10)1);
  __ftol();
  return;
}



/* ===================================================== */
/* FUN_00a25d10 @ 00a25d10 */
/* ===================================================== */

void FUN_00a25d10(int param_1,int param_2)

{
                    /* 0x25d10  457   */
  fpatan((float10)param_1,(float10)param_2);
  __ftol();
  return;
}



/* ===================================================== */
/* FUN_00a25d30 @ 00a25d30 */
/* ===================================================== */

void FUN_00a25d30(uint param_1,uint param_2,uint *param_3,undefined2 param_4)

{
  uint *puVar1;
  int iVar2;
  uint *puVar3;
  uint *puVar4;
  
  puVar1 = (uint *)FUN_00a02ea0(0xe6,0xd);
  puVar1[2] = param_1;
  *(undefined2 *)(puVar1 + 4) = param_4;
  puVar1[3] = param_2;
  puVar3 = &EXT_011d0870;
  puVar4 = puVar1 + 5;
  for (iVar2 = 8; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar4 = *puVar3;
    puVar3 = puVar3 + 1;
    puVar4 = puVar4 + 1;
  }
  puVar1[0xd] = DAT_011cb208;
  *puVar1 = (*param_3 ^ *puVar1) & 0xffffff ^ *puVar1;
  *param_3 = ((uint)puVar1 ^ *param_3) & 0xffffff ^ *param_3;
  return;
}



/* ===================================================== */
/* FUN_00a25da0 @ 00a25da0 */
/* ===================================================== */

void FUN_00a25da0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25da0  458   */
  FUN_00a25d30(param_1,0,param_2,4);
  return;
}



/* ===================================================== */
/* FUN_00a25dc0 @ 00a25dc0 */
/* ===================================================== */

void FUN_00a25dc0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25dc0  459   */
  FUN_00a25d30(param_1,0,param_2,6);
  return;
}



/* ===================================================== */
/* FUN_00a25de0 @ 00a25de0 */
/* ===================================================== */

void FUN_00a25de0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25de0  460   */
  FUN_00a25d30(param_1,0,param_2,0);
  return;
}



/* ===================================================== */
/* FUN_00a25e00 @ 00a25e00 */
/* ===================================================== */

void FUN_00a25e00(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25e00  461   */
  FUN_00a25d30(param_1,0,param_2,2);
  return;
}



/* ===================================================== */
/* FUN_00a25e20 @ 00a25e20 */
/* ===================================================== */

void FUN_00a25e20(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25e20  462   */
  FUN_00a25d30(param_1,0,param_2,0x2d);
  return;
}



/* ===================================================== */
/* FUN_00a25e40 @ 00a25e40 */
/* ===================================================== */

void FUN_00a25e40(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25e40  463   */
  FUN_00a25d30(param_1,0,param_2,7);
  return;
}



/* ===================================================== */
/* FUN_00a25e60 @ 00a25e60 */
/* ===================================================== */

void FUN_00a25e60(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25e60  464   */
  FUN_00a25d30(param_1,0,param_2,1);
  return;
}



/* ===================================================== */
/* FUN_00a25e80 @ 00a25e80 */
/* ===================================================== */

void FUN_00a25e80(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25e80  465   */
  FUN_00a25d30(param_1,0,param_2,3);
  return;
}



/* ===================================================== */
/* FUN_00a25ea0 @ 00a25ea0 */
/* ===================================================== */

void FUN_00a25ea0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25ea0  466   */
  FUN_00a25d30(param_1,param_2,param_3,0xc);
  return;
}



/* ===================================================== */
/* FUN_00a25ec0 @ 00a25ec0 */
/* ===================================================== */

void FUN_00a25ec0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25ec0  467   */
  FUN_00a25d30(param_1,param_2,param_3,0xe);
  return;
}



/* ===================================================== */
/* FUN_00a25ee0 @ 00a25ee0 */
/* ===================================================== */

void FUN_00a25ee0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25ee0  468   */
  FUN_00a25d30(param_1,param_2,param_3,8);
  return;
}



/* ===================================================== */
/* FUN_00a25f00 @ 00a25f00 */
/* ===================================================== */

void FUN_00a25f00(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25f00  469   */
  FUN_00a25d30(param_1,param_2,param_3,10);
  return;
}



/* ===================================================== */
/* FUN_00a25f20 @ 00a25f20 */
/* ===================================================== */

void FUN_00a25f20(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25f20  470   */
  FUN_00a25d30(param_1,param_2,param_3,0xd);
  return;
}



/* ===================================================== */
/* FUN_00a25f40 @ 00a25f40 */
/* ===================================================== */

void FUN_00a25f40(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25f40  471   */
  FUN_00a25d30(param_1,param_2,param_3,0xf);
  return;
}



/* ===================================================== */
/* FUN_00a25f60 @ 00a25f60 */
/* ===================================================== */

void FUN_00a25f60(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25f60  472   */
  FUN_00a25d30(param_1,param_2,param_3,9);
  return;
}



/* ===================================================== */
/* FUN_00a25f80 @ 00a25f80 */
/* ===================================================== */

void FUN_00a25f80(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x25f80  473   */
  FUN_00a25d30(param_1,param_2,param_3,0xb);
  return;
}



/* ===================================================== */
/* FUN_00a25fa0 @ 00a25fa0 */
/* ===================================================== */

undefined4 FUN_00a25fa0(void)

{
                    /* 0x25fa0  477  
                       0x25fa0  478   */
  return 0;
}



/* ===================================================== */
/* FUN_00a25fb0 @ 00a25fb0 */
/* ===================================================== */

undefined4 FUN_00a25fb0(void)

{
                    /* 0x25fb0  481  
                       0x25fb0  482  
                       0x25fb0  485  
                       0x25fb0  486   */
  return 0;
}



/* ===================================================== */
/* FUN_00a25fc0 @ 00a25fc0 */
/* ===================================================== */

undefined4 FUN_00a25fc0(void)

{
                    /* 0x25fc0  489  
                       0x25fc0  490   */
  return 0;
}



/* ===================================================== */
/* FUN_00a25fd0 @ 00a25fd0 */
/* ===================================================== */

undefined4 FUN_00a25fd0(void)

{
                    /* 0x25fd0  479  
                       0x25fd0  480  
                       0x25fd0  483  
                       0x25fd0  484  
                       0x25fd0  501   */
  return 0;
}



/* ===================================================== */
/* FUN_00a25fe0 @ 00a25fe0 */
/* ===================================================== */

void FUN_00a25fe0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x25fe0  513  
                       0x25fe0  529   */
  FUN_00a25d30(param_1,0,param_2,7);
  return;
}



/* ===================================================== */
/* FUN_00a26000 @ 00a26000 */
/* ===================================================== */

void FUN_00a26000(undefined4 param_1,undefined4 param_2)

{
                    /* 0x26000  514  
                       0x26000  530   */
  FUN_00a25d30(param_1,0,param_2,1);
  return;
}



/* ===================================================== */
/* FUN_00a26020 @ 00a26020 */
/* ===================================================== */

void FUN_00a26020(undefined4 param_1,undefined4 param_2)

{
                    /* 0x26020  515  
                       0x26020  531   */
  FUN_00a25d30(param_1,0,param_2,3);
  return;
}



/* ===================================================== */
/* FUN_00a26040 @ 00a26040 */
/* ===================================================== */

void FUN_00a26040(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x26040  516  
                       0x26040  532   */
  FUN_00a25d30(param_1,param_2,param_3,0xc);
  return;
}



/* ===================================================== */
/* FUN_00a26060 @ 00a26060 */
/* ===================================================== */

void FUN_00a26060(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x26060  517  
                       0x26060  533   */
  FUN_00a25d30(param_1,param_2,param_3,0xe);
  return;
}



/* ===================================================== */
/* FUN_00a26080 @ 00a26080 */
/* ===================================================== */

void FUN_00a26080(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x26080  518  
                       0x26080  534   */
  FUN_00a25d30(param_1,param_2,param_3,8);
  return;
}



/* ===================================================== */
/* FUN_00a260a0 @ 00a260a0 */
/* ===================================================== */

void FUN_00a260a0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x260a0  519  
                       0x260a0  535   */
  FUN_00a25d30(param_1,param_2,param_3,10);
  return;
}



/* ===================================================== */
/* FUN_00a260c0 @ 00a260c0 */
/* ===================================================== */

void FUN_00a260c0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x260c0  520  
                       0x260c0  536   */
  FUN_00a25d30(param_1,param_2,param_3,0xd);
  return;
}



/* ===================================================== */
/* FUN_00a260e0 @ 00a260e0 */
/* ===================================================== */

void FUN_00a260e0(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x260e0  521  
                       0x260e0  537   */
  FUN_00a25d30(param_1,param_2,param_3,0xf);
  return;
}



/* ===================================================== */
/* FUN_00a26100 @ 00a26100 */
/* ===================================================== */

void FUN_00a26100(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x26100  522  
                       0x26100  538   */
  FUN_00a25d30(param_1,param_2,param_3,9);
  return;
}



/* ===================================================== */
/* FUN_00a26120 @ 00a26120 */
/* ===================================================== */

void FUN_00a26120(undefined4 param_1,undefined4 param_2,undefined4 param_3)

{
                    /* 0x26120  523  
                       0x26120  539   */
  FUN_00a25d30(param_1,param_2,param_3,0xb);
  return;
}



/* ===================================================== */
/* FUN_00a26140 @ 00a26140 */
/* ===================================================== */

void FUN_00a26140(undefined4 param_1,undefined4 param_2)

{
                    /* 0x26140  508  
                       0x26140  524   */
  FUN_00a25d30(param_1,0,param_2,4);
  return;
}



/* ===================================================== */
/* FUN_00a26160 @ 00a26160 */
/* ===================================================== */

void FUN_00a26160(undefined4 param_1,undefined4 param_2)

{
                    /* 0x26160  509  
                       0x26160  525   */
  FUN_00a25d30(param_1,0,param_2,6);
  return;
}



/* ===================================================== */
/* FUN_00a26180 @ 00a26180 */
/* ===================================================== */

void FUN_00a26180(undefined4 param_1,undefined4 param_2)

{
                    /* 0x26180  510  
                       0x26180  526   */
  FUN_00a25d30(param_1,0,param_2,0);
  return;
}



/* ===================================================== */
/* FUN_00a261a0 @ 00a261a0 */
/* ===================================================== */

void FUN_00a261a0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x261a0  511  
                       0x261a0  527   */
  FUN_00a25d30(param_1,0,param_2,2);
  return;
}



/* ===================================================== */
/* FUN_00a261c0 @ 00a261c0 */
/* ===================================================== */

void FUN_00a261c0(undefined4 param_1,undefined4 param_2)

{
                    /* 0x261c0  512  
                       0x261c0  528   */
  FUN_00a25d30(param_1,0,param_2,0x2d);
  return;
}



/* ===================================================== */
/* FUN_00a261e0 @ 00a261e0 */
/* ===================================================== */

undefined1 * FUN_00a261e0(char *param_1)

{
  char *pcVar1;
  char cVar2;
  int iVar3;
  
  param_1 = param_1 + (*param_1 == '\\');
  iVar3 = 0;
  cVar2 = *param_1;
  while ((cVar2 != '\0' && (cVar2 != ';'))) {
    (&DAT_011f0ed0)[iVar3] = *param_1;
    pcVar1 = param_1 + 1;
    iVar3 = iVar3 + 1;
    param_1 = param_1 + 1;
    cVar2 = *pcVar1;
  }
  (&DAT_011f0ed0)[iVar3] = 0;
  return &DAT_011f0ed0;
}



/* ===================================================== */
/* FUN_00a26220 @ 00a26220 */
/* ===================================================== */

void FUN_00a26220(undefined4 param_1,int param_2)

{
  LPCSTR lpFileName;
  DWORD dwDesiredAccess;
  DWORD dwShareMode;
  LPSECURITY_ATTRIBUTES lpSecurityAttributes;
  DWORD dwCreationDisposition;
  DWORD dwFlagsAndAttributes;
  HANDLE hTemplateFile;
  
                    /* 0x26220  331   */
  dwDesiredAccess = 0;
  if (param_2 == 0) {
    dwDesiredAccess = 0x80000000;
  }
  else if (param_2 == 1) {
    dwDesiredAccess = 0x40000000;
  }
  else if (param_2 == 2) {
    dwDesiredAccess = 0xc0000000;
  }
  hTemplateFile = (HANDLE)0x0;
  dwFlagsAndAttributes = 0x80;
  dwCreationDisposition = 3;
  lpSecurityAttributes = (LPSECURITY_ATTRIBUTES)0x0;
  dwShareMode = 1;
  lpFileName = (LPCSTR)FUN_00a261e0(param_1);
  CreateFileA(lpFileName,dwDesiredAccess,dwShareMode,lpSecurityAttributes,dwCreationDisposition,
              dwFlagsAndAttributes,hTemplateFile);
  return;
}



/* ===================================================== */
/* FUN_00a26270 @ 00a26270 */
/* ===================================================== */

void FUN_00a26270(undefined4 param_1)

{
  LPCSTR lpFileName;
  DWORD dwDesiredAccess;
  DWORD dwShareMode;
  LPSECURITY_ATTRIBUTES lpSecurityAttributes;
  DWORD dwCreationDisposition;
  DWORD dwFlagsAndAttributes;
  HANDLE hTemplateFile;
  
                    /* 0x26270  332   */
  hTemplateFile = (HANDLE)0x0;
  dwFlagsAndAttributes = 0x80;
  dwCreationDisposition = 2;
  lpSecurityAttributes = (LPSECURITY_ATTRIBUTES)0x0;
  dwShareMode = 1;
  dwDesiredAccess = 0xc0000000;
  lpFileName = (LPCSTR)FUN_00a261e0(param_1);
  CreateFileA(lpFileName,dwDesiredAccess,dwShareMode,lpSecurityAttributes,dwCreationDisposition,
              dwFlagsAndAttributes,hTemplateFile);
  return;
}



/* ===================================================== */
/* FUN_00a262a0 @ 00a262a0 */
/* ===================================================== */

uint FUN_00a262a0(HANDLE param_1,LPVOID param_2,DWORD param_3)

{
  BOOL BVar1;
  
                    /* 0x262a0  334   */
  BVar1 = ReadFile(param_1,param_2,param_3,&param_3,(LPOVERLAPPED)0x0);
  return -(uint)(BVar1 != 0) & param_3;
}



/* ===================================================== */
/* FUN_00a262d0 @ 00a262d0 */
/* ===================================================== */

uint FUN_00a262d0(HANDLE param_1,LPCVOID param_2,DWORD param_3)

{
  BOOL BVar1;
  
                    /* 0x262d0  335   */
  BVar1 = WriteFile(param_1,param_2,param_3,&param_3,(LPOVERLAPPED)0x0);
  return -(uint)(BVar1 != 0) & param_3;
}



/* ===================================================== */
/* FUN_00a26300 @ 00a26300 */
/* ===================================================== */

undefined4 FUN_00a26300(HANDLE param_1)

{
                    /* 0x26300  336   */
  CloseHandle(param_1);
  return 0;
}



/* ===================================================== */
/* FUN_00a26310 @ 00a26310 */
/* ===================================================== */

void FUN_00a26310(undefined4 param_1)

{
  undefined4 uVar1;
  
                    /* 0x26310  761   */
  uVar1 = FUN_00a26270(param_1,0);
  FUN_00a262d0(uVar1,EXT_00b6dbd0,0x100000);
  FUN_00a26300(uVar1);
  return;
}



/* ===================================================== */
/* PCsaveTexture @ 00a26340 */
/* ===================================================== */

void PCsaveTexture(uint param_1)

{
  undefined4 uVar1;
  int iVar2;
  int iVar3;
  undefined1 local_34 [52];
  
                    /* 0x26340  783  PCsaveTexture */
  Ordinal_1(local_34,s_Tex0x_x_bin_00a2c620,param_1);
  uVar1 = FUN_00a26270(local_34,0);
  iVar3 = (param_1 & 0xf) << 7;
  iVar2 = 0x100;
  do {
    FUN_00a262d0(uVar1,EXT_00b6dbd0 + iVar3,0x100);
    iVar3 = iVar3 + 0x800;
    iVar2 = iVar2 + -1;
  } while (iVar2 != 0);
  FUN_00a26300(uVar1);
  return;
}



/* ===================================================== */
/* FUN_00a263b0 @ 00a263b0 */
/* ===================================================== */

undefined4 FUN_00a263b0(void)

{
                    /* 0x263b0  191  
                       0x263b0  475  
                       0x263b0  476  
                       0x263b0  617   */
  return 0;
}



/* ===================================================== */
/* FUN_00a263c0 @ 00a263c0 */
/* ===================================================== */

undefined1 FUN_00a263c0(void)

{
                    /* 0x263c0  199   */
  return 0;
}



/* ===================================================== */
/* FUN_00a263d0 @ 00a263d0 */
/* ===================================================== */

undefined1 FUN_00a263d0(void)

{
                    /* 0x263d0  213   */
  return 0;
}



/* ===================================================== */
/* FUN_00a263e0 @ 00a263e0 */
/* ===================================================== */

undefined2 FUN_00a263e0(void)

{
                    /* 0x263e0  216   */
  return 0;
}



/* ===================================================== */
/* FUN_00a263f0 @ 00a263f0 */
/* ===================================================== */

undefined2 FUN_00a263f0(void)

{
                    /* 0x263f0  215  
                       0x263f0  218   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26400 @ 00a26400 */
/* ===================================================== */

undefined2 FUN_00a26400(void)

{
                    /* 0x26400  214  
                       0x26400  219   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26410 @ 00a26410 */
/* ===================================================== */

undefined2 FUN_00a26410(void)

{
                    /* 0x26410  220   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26420 @ 00a26420 */
/* ===================================================== */

undefined2 FUN_00a26420(void)

{
                    /* 0x26420  155  
                       0x26420  225  
                       0x26420  226  
                       0x26420  231  
                       0x26420  232   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26430 @ 00a26430 */
/* ===================================================== */

undefined2 FUN_00a26430(void)

{
                    /* 0x26430  203  
                       0x26430  236   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26440 @ 00a26440 */
/* ===================================================== */

undefined2 FUN_00a26440(void)

{
                    /* 0x26440  156  
                       0x26440  217  
                       0x26440  235  
                       0x26440  246   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26450 @ 00a26450 */
/* ===================================================== */

undefined2 FUN_00a26450(void)

{
                    /* 0x26450  152  
                       0x26450  154  
                       0x26450  180  
                       0x26450  223  
                       0x26450  224  
                       0x26450  227  
                       0x26450  228  
                       0x26450  229  
                       0x26450  230  
                       0x26450  245  
                       0x26450  254   */
  return 0;
}



/* ===================================================== */
/* FUN_00a26460 @ 00a26460 */
/* ===================================================== */

void FUN_00a26460(void)

{
                    /* 0x26460  273  
                       0x26460  274  
                       0x26460  275  
                       0x26460  276  
                       0x26460  277  
                       0x26460  278  
                       0x26460  279  
                       0x26460  280  
                       0x26460  281  
                       0x26460  282  
                       0x26460  283  
                       0x26460  284  
                       0x26460  285  
                       0x26460  286  
                       0x26460  287  
                       0x26460  288  
                       0x26460  289  
                       0x26460  290  
                       0x26460  291  
                       0x26460  292  
                       0x26460  310  
                       0x26460  311  
                       0x26460  312  
                       0x26460  313  
                       0x26460  314  
                       0x26460  315  
                       0x26460  316  
                       0x26460  317  
                       0x26460  318  
                       0x26460  319  
                       0x26460  320  
                       0x26460  321  
                       0x26460  322  
                       0x26460  323  
                       0x26460  324  
                       0x26460  325  
                       0x26460  326  
                       0x26460  327  
                       0x26460  328  
                       0x26460  329   */
  return;
}



/* ===================================================== */
/* FUN_00a26470 @ 00a26470 */
/* ===================================================== */

void FUN_00a26470(void)

{
                    /* 0x26470  38  
                       0x26470  39  
                       0x26470  42  
                       0x26470  43  
                       0x26470  44  
                       0x26470  59  
                       0x26470  60  
                       0x26470  66  
                       0x26470  71  
                       0x26470  74  
                       0x26470  78  
                       0x26470  79  
                       0x26470  88  
                       0x26470  91  
                       0x26470  138  
                       0x26470  158  
                       0x26470  159  
                       0x26470  163  
                       0x26470  164  
                       0x26470  165  
                       0x26470  166  
                       0x26470  167  
                       0x26470  205  
                       0x26470  206  
                       0x26470  207  
                       0x26470  233  
                       0x26470  234  
                       0x26470  242  
                       0x26470  562  
                       0x26470  603  
                       0x26470  604  
                       0x26470  616  
                       0x26470  771   */
  return;
}



/* ===================================================== */
/* FUN_00a26490 @ 00a26490 */
/* ===================================================== */

void FUN_00a26490(void)

{
  undefined1 uVar1;
  int iVar2;
  undefined4 *puVar3;
  
  if (DAT_00a2c654 == -2) {
    iVar2 = 0;
    do {
      uVar1 = FUN_00a03400(iVar2);
      (&DAT_00a2c654)[iVar2] = uVar1;
      iVar2 = iVar2 + 1;
    } while (iVar2 < 4);
  }
  puVar3 = &DAT_011f4db8;
  for (iVar2 = 32000; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar3 = 0xffffffff;
    puVar3 = puVar3 + 1;
  }
  DAT_012141b8 = 0;
  puVar3 = &DAT_011f0f38;
  for (iVar2 = 3000; iVar2 != 0; iVar2 = iVar2 + -1) {
    *puVar3 = 0;
    puVar3 = puVar3 + 1;
  }
  DAT_00a2c650 = DAT_011be6ec & 0xffff;
  return;
}



/* ===================================================== */
/* FUN_00a264f0 @ 00a264f0 */
/* ===================================================== */

/* WARNING: Control flow encountered bad instruction data */
/* WARNING: Switch with 1 destination removed at 0x00a2651b : 4 cases all go to same destination */

void FUN_00a264f0(uint param_1,undefined4 param_2,LONG param_3,LONG param_4,LONG param_5,
                 LONG param_6,int param_7)

{
  int iVar1;
  int iVar2;
  RECT local_10;
  
  if (param_7 == 0) {
    iVar1 = FUN_00a278c0(param_2);
    iVar1 = (param_1 & 0x1f) * 2000 + iVar1;
    iVar2 = (int)*(short *)((int)&DAT_011f4db8 + iVar1 * 2);
    if (iVar2 == -1) {
      *(short *)((int)&DAT_011f4db8 + iVar1 * 2) = (short)DAT_012141b8;
      iVar1 = (int)(short)DAT_012141b8;
      DAT_012141b8 = DAT_012141b8 + 1;
      (&DAT_011f0f3c)[iVar1 * 6] = param_4;
      (&DAT_011f0f38)[iVar1 * 6] = param_3;
      (&DAT_011f0f44)[iVar1 * 6] = param_6;
      (&DAT_011f0f40)[iVar1 * 6] = param_5;
      (&DAT_011f0f48)[iVar1 * 0x18] = 0;
      (&DAT_011f0f49)[iVar1 * 0x18] = (byte)param_1 & 0x1f;
      (&DAT_011f0f4a)[iVar1 * 0xc] = (short)param_2;
      (&DAT_011f0f4c)[iVar1 * 0xc] = (ushort)param_1 & 0x1180;
      return;
    }
    local_10.left = param_3;
    local_10.top = param_4;
    local_10.right = param_5;
    local_10.bottom = param_6;
    UnionRect((LPRECT)(&DAT_011f0f38 + iVar2 * 6),&local_10,(LPRECT)(&DAT_011f0f38 + iVar2 * 6));
    (&DAT_011f0f48)[iVar2 * 0x18] = (&DAT_011f0f48)[iVar2 * 0x18];
    if ((param_1 & 0x1000) == 0) {
      (&DAT_011f0f4c)[iVar2 * 0xc] = (&DAT_011f0f4c)[iVar2 * 0xc] & 0x180;
    }
  }
  else if ((byte)(&DAT_00a2c654)[(int)param_1 >> 5 & 3] < 5) {
                    /* WARNING: Bad instruction - Truncating control flow here */
    halt_baddata();
  }
  return;
}



/* ===================================================== */
/* FUN_00a26670 @ 00a26670 */
/* ===================================================== */

void FUN_00a26670(int param_1,undefined4 param_2)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  byte bVar5;
  byte bVar6;
  byte bVar7;
  
  bVar1 = *(byte *)(param_1 + 0x1d);
  bVar6 = *(byte *)(param_1 + 0x1c);
  bVar4 = *(byte *)(param_1 + 0x14);
  bVar2 = *(byte *)(param_1 + 0xc);
  bVar3 = *(byte *)(param_1 + 0xd);
  bVar5 = *(byte *)(param_1 + 0x15);
  bVar7 = bVar4;
  if (bVar4 < bVar2) {
    bVar7 = bVar2;
    bVar2 = bVar4;
  }
  bVar4 = bVar3;
  if (bVar3 <= bVar5) {
    bVar4 = bVar5;
    bVar5 = bVar3;
  }
  bVar3 = bVar6;
  if ((bVar2 <= bVar6) && (bVar3 = bVar2, bVar7 < bVar6)) {
    bVar7 = bVar6;
  }
  bVar6 = bVar1;
  if ((bVar5 <= bVar1) && (bVar6 = bVar5, bVar4 < bVar1)) {
    bVar4 = bVar1;
  }
  FUN_00a264f0(*(undefined2 *)(param_1 + 0x16),*(undefined2 *)(param_1 + 0xe),bVar3,bVar6,bVar7,
               bVar4,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a26720 @ 00a26720 */
/* ===================================================== */

void FUN_00a26720(int param_1,undefined4 param_2)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  byte bVar5;
  byte bVar6;
  byte bVar7;
  
  bVar1 = *(byte *)(param_1 + 0x25);
  bVar6 = *(byte *)(param_1 + 0x24);
  bVar4 = *(byte *)(param_1 + 0x18);
  bVar2 = *(byte *)(param_1 + 0xc);
  bVar3 = *(byte *)(param_1 + 0xd);
  bVar5 = *(byte *)(param_1 + 0x19);
  bVar7 = bVar4;
  if (bVar4 < bVar2) {
    bVar7 = bVar2;
    bVar2 = bVar4;
  }
  bVar4 = bVar3;
  if (bVar3 <= bVar5) {
    bVar4 = bVar5;
    bVar5 = bVar3;
  }
  bVar3 = bVar6;
  if ((bVar2 <= bVar6) && (bVar3 = bVar2, bVar7 < bVar6)) {
    bVar7 = bVar6;
  }
  bVar6 = bVar1;
  if ((bVar5 <= bVar1) && (bVar6 = bVar5, bVar4 < bVar1)) {
    bVar4 = bVar1;
  }
  FUN_00a264f0(*(undefined2 *)(param_1 + 0x1a),*(undefined2 *)(param_1 + 0xe),bVar3,bVar6,bVar7,
               bVar4,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a267d0 @ 00a267d0 */
/* ===================================================== */

void FUN_00a267d0(int param_1,undefined4 param_2)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  byte bVar5;
  byte bVar6;
  byte bVar7;
  byte bVar8;
  byte bVar9;
  
  bVar1 = *(byte *)(param_1 + 0x25);
  bVar8 = *(byte *)(param_1 + 0x24);
  bVar2 = *(byte *)(param_1 + 0x1d);
  bVar7 = *(byte *)(param_1 + 0x1c);
  bVar3 = *(byte *)(param_1 + 0x15);
  bVar9 = *(byte *)(param_1 + 0x14);
  bVar6 = *(byte *)(param_1 + 0xc);
  bVar5 = *(byte *)(param_1 + 0xd);
  bVar4 = bVar6;
  if (bVar9 < bVar6) {
    bVar4 = bVar9;
    bVar9 = bVar6;
  }
  bVar6 = bVar3;
  if (bVar5 <= bVar3) {
    bVar6 = bVar5;
    bVar5 = bVar3;
  }
  bVar3 = bVar7;
  if ((bVar4 <= bVar7) && (bVar3 = bVar4, bVar9 < bVar7)) {
    bVar9 = bVar7;
  }
  bVar7 = bVar2;
  if ((bVar6 <= bVar2) && (bVar7 = bVar6, bVar5 < bVar2)) {
    bVar5 = bVar2;
  }
  bVar2 = bVar8;
  if ((bVar3 <= bVar8) && (bVar2 = bVar3, bVar9 < bVar8)) {
    bVar9 = bVar8;
  }
  bVar8 = bVar1;
  if ((bVar7 <= bVar1) && (bVar8 = bVar7, bVar5 < bVar1)) {
    bVar5 = bVar1;
  }
  FUN_00a264f0(*(undefined2 *)(param_1 + 0x16),*(undefined2 *)(param_1 + 0xe),bVar2,bVar8,bVar9,
               bVar5,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a268c0 @ 00a268c0 */
/* ===================================================== */

void FUN_00a268c0(int param_1,undefined4 param_2)

{
  byte bVar1;
  byte bVar2;
  byte bVar3;
  byte bVar4;
  byte bVar5;
  byte bVar6;
  byte bVar7;
  byte bVar8;
  byte bVar9;
  
  bVar1 = *(byte *)(param_1 + 0x31);
  bVar8 = *(byte *)(param_1 + 0x30);
  bVar2 = *(byte *)(param_1 + 0x25);
  bVar7 = *(byte *)(param_1 + 0x24);
  bVar3 = *(byte *)(param_1 + 0x19);
  bVar9 = *(byte *)(param_1 + 0x18);
  bVar6 = *(byte *)(param_1 + 0xc);
  bVar5 = *(byte *)(param_1 + 0xd);
  bVar4 = bVar6;
  if (bVar9 < bVar6) {
    bVar4 = bVar9;
    bVar9 = bVar6;
  }
  bVar6 = bVar3;
  if (bVar5 <= bVar3) {
    bVar6 = bVar5;
    bVar5 = bVar3;
  }
  bVar3 = bVar7;
  if ((bVar4 <= bVar7) && (bVar3 = bVar4, bVar9 < bVar7)) {
    bVar9 = bVar7;
  }
  bVar7 = bVar2;
  if ((bVar6 <= bVar2) && (bVar7 = bVar6, bVar5 < bVar2)) {
    bVar5 = bVar2;
  }
  bVar2 = bVar8;
  if ((bVar3 <= bVar8) && (bVar2 = bVar3, bVar9 < bVar8)) {
    bVar9 = bVar8;
  }
  bVar8 = bVar1;
  if ((bVar7 <= bVar1) && (bVar8 = bVar7, bVar5 < bVar1)) {
    bVar5 = bVar1;
  }
  FUN_00a264f0(*(undefined2 *)(param_1 + 0x1a),*(undefined2 *)(param_1 + 0xe),bVar2,bVar8,bVar9,
               bVar5,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a269b0 @ 00a269b0 */
/* ===================================================== */

void FUN_00a269b0(int param_1,undefined4 param_2)

{
  byte bVar1;
  uint uVar2;
  int iVar3;
  int iVar4;
  
  if (DAT_012141cc == 0) {
    uVar2 = (uint)*(byte *)(param_1 + 0xd);
    bVar1 = *(byte *)(param_1 + 0xc);
    iVar4 = *(short *)(param_1 + 0x12) + -1 + uVar2;
    iVar3 = (int)*(short *)(param_1 + 0x10);
  }
  else {
    uVar2 = (uint)*(byte *)(param_1 + 0xd);
    bVar1 = *(byte *)(param_1 + 0xc);
    iVar4 = (DAT_012141c8 >> 1) + -1 + uVar2;
    iVar3 = DAT_012141c4 >> 1;
  }
  FUN_00a264f0(DAT_00a2c650,*(undefined2 *)(param_1 + 0xe),(uint)bVar1,uVar2,
               iVar3 + -1 + (uint)bVar1,iVar4,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a26a30 @ 00a26a30 */
/* ===================================================== */

void FUN_00a26a30(int param_1,undefined4 param_2)

{
  FUN_00a264f0(DAT_00a2c650,*(undefined2 *)(param_1 + 0xe),(uint)*(byte *)(param_1 + 0xc),
               (uint)*(byte *)(param_1 + 0xd),*(byte *)(param_1 + 0xc) + 7,
               *(byte *)(param_1 + 0xd) + 7,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a26a70 @ 00a26a70 */
/* ===================================================== */

void FUN_00a26a70(int param_1,undefined4 param_2)

{
  FUN_00a264f0(DAT_00a2c650,*(undefined2 *)(param_1 + 0xe),(uint)*(byte *)(param_1 + 0xc),
               (uint)*(byte *)(param_1 + 0xd),*(byte *)(param_1 + 0xc) + 0xf,
               *(byte *)(param_1 + 0xd) + 0xf,param_2);
  return;
}



/* ===================================================== */
/* FUN_00a26ab0 @ 00a26ab0 */
/* ===================================================== */

void FUN_00a26ab0(uint *param_1)

{
  if ((((ushort)param_1[2] != 0) && (*(ushort *)((int)param_1 + 10) != 0)) &&
     (*param_1 < 0x80000000)) {
    FUN_00a264f0((short)param_1[3],
                 ((int)(short)param_1[4] >> 5) + *(short *)((int)param_1 + 0x12) * 0x40,
                 (uint)*(byte *)((int)param_1 + 0xe),(uint)*(byte *)((int)param_1 + 0xf),
                 ((ushort)param_1[2] - 1) + (uint)*(byte *)((int)param_1 + 0xe),
                 (*(ushort *)((int)param_1 + 10) - 1) + (uint)*(byte *)((int)param_1 + 0xf),0);
  }
  return;
}



/* ===================================================== */
/* FUN_00a26b20 @ 00a26b20 */
/* ===================================================== */

/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00a26b20(uint *param_1)

{
  uint *puVar1;
  uint uVar2;
  uint uVar3;
  
  uVar2 = (uint)*(byte *)((int)param_1 + 7);
  uVar3 = *param_1 >> 0x18;
  if (0xf8 < uVar2) {
switchD_00a26b43_caseD_4:
    return;
  }
  do {
                    /* WARNING: Could not find normalized switch variable to match jumptable */
    switch((&switchD_00a26b43::switchdataD_00a26d14)[uVar2]) {
    case 0:
    case 1:
    case 2:
    case 3:
    case 0xf1:
    case 0xf3:
    case 0xf4:
    case 0xf5:
    case 0xf6:
    case 0xf7:
    case 0xf8:
      uVar3 = uVar3 - 1;
      break;
    default:
      goto switchD_00a26b43_caseD_4;
    case 0x20:
    case 0x21:
    case 0x22:
    case 0x23:
    case 0x50:
    case 0x51:
    case 0x52:
    case 0x53:
    case 0x54:
    case 0x55:
    case 0x56:
    case 0x57:
      param_1 = param_1 + 4;
      uVar3 = uVar3 - 4;
      break;
    case 0x24:
    case 0x25:
    case 0x26:
    case 0x27:
      FUN_00a26670(param_1,uVar2 & 2);
      param_1 = param_1 + 7;
      uVar3 = uVar3 - 7;
      break;
    case 0x28:
    case 0x29:
    case 0x2a:
    case 0x2b:
    case 0x48:
    case 0x49:
    case 0x4a:
    case 0x4b:
      param_1 = param_1 + 5;
      uVar3 = uVar3 - 5;
      break;
    case 0x2c:
    case 0x2d:
    case 0x2e:
    case 0x2f:
      FUN_00a267d0(param_1,uVar2 & 2);
      param_1 = param_1 + 9;
      uVar3 = uVar3 - 9;
      break;
    case 0x30:
    case 0x31:
    case 0x32:
    case 0x33:
    case 0x4c:
    case 0x4d:
    case 0x4e:
    case 0x4f:
      param_1 = param_1 + 6;
      uVar3 = uVar3 - 6;
      break;
    case 0x34:
    case 0x35:
    case 0x36:
    case 0x37:
      FUN_00a26720(param_1,uVar2 & 2);
      param_1 = param_1 + 9;
      uVar3 = uVar3 - 9;
      break;
    case 0x38:
    case 0x39:
    case 0x3a:
    case 0x3b:
      param_1 = param_1 + 8;
      uVar3 = uVar3 - 8;
      break;
    case 0x3c:
    case 0x3d:
    case 0x3e:
    case 0x3f:
      FUN_00a268c0(param_1,uVar2 & 2);
      param_1 = param_1 + 0xc;
      uVar3 = uVar3 - 0xc;
      break;
    case 0x40:
    case 0x41:
    case 0x42:
    case 0x43:
    case 0x44:
    case 0x45:
    case 0x46:
    case 0x47:
    case 0x60:
    case 0x61:
    case 0x62:
    case 99:
      param_1 = param_1 + 3;
      uVar3 = uVar3 - 3;
      break;
    case 0x58:
    case 0x59:
    case 0x5a:
    case 0x5b:
      param_1 = param_1 + 7;
      uVar3 = uVar3 - 7;
      break;
    case 0x5c:
    case 0x5d:
    case 0x5e:
    case 0x5f:
      param_1 = param_1 + 9;
      uVar3 = uVar3 - 9;
      break;
    case 100:
    case 0x65:
    case 0x66:
    case 0x67:
      FUN_00a269b0(param_1,uVar2 & 2);
      param_1 = param_1 + 4;
      uVar3 = uVar3 - 4;
      break;
    case 0x68:
    case 0x69:
    case 0x6a:
    case 0x6b:
    case 0x6c:
    case 0x6d:
    case 0x6e:
    case 0x6f:
    case 0x70:
    case 0x71:
    case 0x72:
    case 0x73:
    case 0x78:
    case 0x79:
    case 0x7a:
    case 0x7b:
      param_1 = param_1 + 2;
      uVar3 = uVar3 - 2;
      break;
    case 0x74:
    case 0x75:
    case 0x76:
    case 0x77:
      FUN_00a26a30(param_1,uVar2 & 2);
      param_1 = param_1 + 3;
      uVar3 = uVar3 - 3;
      break;
    case 0x7c:
    case 0x7d:
    case 0x7e:
    case 0x7f:
      FUN_00a26a70(param_1,uVar2 & 2);
      param_1 = param_1 + 3;
      uVar3 = uVar3 - 3;
      break;
    case 0xe1:
      puVar1 = param_1 + 1;
      param_1 = param_1 + 1;
      DAT_00a2c650 = *puVar1 & 0xffffff;
      uVar3 = uVar3 - 1;
      break;
    case 0xe2:
      DAT_012141cc = param_1[1] & 0xffffff;
      if (DAT_012141cc != 0) {
        DAT_012141c4 = param_1[1] & 0x1f;
        DAT_012141c8 = (int)DAT_012141cc >> 5 & 0x1f;
        _DAT_012141bc = (int)DAT_012141cc >> 10 & 0x1f;
        _DAT_012141c0 = (int)DAT_012141cc >> 0xf & 0x1f;
      }
      param_1 = param_1 + 1;
      uVar3 = uVar3 - 1;
      break;
    case 0xe5:
      FUN_00a26ab0(param_1);
      goto switchD_00a26b43_caseD_4;
    }
    if ((int)uVar3 < 1) {
      return;
    }
    uVar2 = (uint)*(byte *)((int)param_1 + 7);
    if (0xf8 < uVar2) {
      return;
    }
  } while( true );
}



/* ===================================================== */
/* FUN_00a26e10 @ 00a26e10 */
/* ===================================================== */

void FUN_00a26e10(uint *param_1)

{
  int iVar1;
  uint uVar2;
  uint uVar3;
  int iVar4;
  uint uVar5;
  uint uVar6;
  uint uVar7;
  
  uVar5 = *param_1 & 0xffffffe0;
  uVar3 = param_1[1] & 0xffffffe0;
  param_1[1] = uVar3;
  iVar1 = param_1[2] - uVar5;
  iVar4 = param_1[3] - uVar3;
  *param_1 = uVar5;
  if (0xff < iVar1) {
    iVar1 = 0xff;
  }
  if (0xff < iVar4) {
    iVar4 = 0xff;
  }
  switch(iVar1 >> 5 & 7) {
  case 0:
    uVar6 = 0x20;
    break;
  case 1:
    uVar6 = 0x40;
    break;
  case 2:
  case 3:
    uVar6 = 0x80;
    break;
  default:
    uVar6 = 0x100;
  }
  switch(iVar4 >> 5 & 7) {
  case 0:
    uVar2 = 0x20;
    break;
  case 1:
    uVar2 = 0x40;
    break;
  case 2:
  case 3:
    uVar2 = 0x80;
    break;
  default:
    uVar2 = 0x100;
  }
  uVar7 = uVar6;
  if ((DAT_00b2d180 != 0) && (uVar7 = uVar2, uVar2 < uVar6)) {
    uVar2 = uVar6;
    uVar7 = uVar6;
  }
  if (0x100 < (int)(uVar2 + uVar3)) {
    param_1[1] = 0x100 - uVar2;
  }
  if (0x100 < (int)(uVar7 + uVar5)) {
    *param_1 = 0x100 - uVar7;
  }
  param_1[2] = uVar7 + *param_1;
  param_1[3] = uVar2 + param_1[1];
  return;
}



/* ===================================================== */
/* FUN_00a26f20 @ 00a26f20 */
/* ===================================================== */

void FUN_00a26f20(void)

{
  int *piVar1;
  uint uVar2;
  bool bVar3;
  int iVar4;
  undefined2 *puVar5;
  int iVar6;
  bool bVar7;
  short sVar8;
  int *piVar9;
  short *psVar10;
  undefined4 *puVar11;
  uint uVar12;
  int local_c;
  int local_4;
  
  local_c = 0;
  do {
    iVar6 = 0;
    bVar3 = false;
    if (0 < DAT_012141b8) {
      piVar9 = &DAT_011f0f3c;
      do {
        if ((char)piVar9[3] != -1) {
          piVar1 = piVar9 + -1;
          bVar7 = true;
          FUN_00a26e10(piVar1);
          iVar4 = FUN_00a27920(piVar1,(int)*(char *)((int)piVar9 + 0xd),
                               *(undefined2 *)((int)piVar9 + 0xe),(int)(char)piVar9[3]);
          if ((iVar4 == 0) &&
             (iVar4 = FUN_00a27850(piVar1,(int)(short)piVar9[4] | (int)*(char *)((int)piVar9 + 0xd),
                                   *(undefined2 *)((int)piVar9 + 0xe),(int)(char)piVar9[3]),
             iVar4 == 0)) {
            bVar7 = false;
            local_c = local_c + (piVar9[1] - *piVar1) * (piVar9[2] - *piVar9);
            bVar3 = true;
          }
          if (bVar7) {
            *(undefined1 *)(piVar9 + 3) = 0xff;
          }
        }
        iVar6 = iVar6 + 1;
        piVar9 = piVar9 + 6;
      } while (iVar6 < DAT_012141b8);
    }
    if (bVar3) {
      iVar4 = 0;
      puVar11 = &DAT_011f3e18;
      for (iVar6 = 1000; iVar6 != 0; iVar6 = iVar6 + -1) {
        *puVar11 = 0;
        puVar11 = puVar11 + 1;
      }
      uVar12 = 0;
      do {
        sVar8 = 0;
        piVar9 = (int *)(DAT_012141d4 + (uVar12 & 0x1f) * 4);
        puVar5 = (undefined2 *)((int)&DAT_011f3e18 + iVar4 * 8 + 2);
        iVar6 = 4;
        do {
          uVar2 = *(uint *)(*(int *)(iVar6 + *piVar9) + 0x28);
          if (uVar2 < DAT_011be75c) {
            puVar5[-1] = sVar8;
            *puVar5 = (short)uVar12;
            *(uint *)(puVar5 + 1) = uVar2;
            iVar4 = iVar4 + 1;
            puVar5 = puVar5 + 4;
          }
          iVar6 = iVar6 + 4;
          sVar8 = sVar8 + 1;
        } while (iVar6 < 0x1f44);
        uVar12 = uVar12 + 1;
      } while ((int)uVar12 < 0x20);
      if (iVar4 == 0) {
        bVar3 = false;
      }
      else {
        FUN_00a27e00(&DAT_011f3e18,iVar4,8,&LAB_00a27100);
        local_4 = 0;
        if (0 < iVar4) {
          psVar10 = (short *)((int)&DAT_011f3e18 + 2);
          do {
            if (local_c < 1) break;
            piVar9 = *(int **)(*(int *)(DAT_012141d4 + ((int)*psVar10 & 0x1fU) * 4) + 4 +
                              psVar10[-1] * 4);
            if (*piVar9 == 0) {
              iVar6 = 0;
            }
            else {
              iVar6 = (piVar9[5] - piVar9[3]) * (piVar9[4] - piVar9[2]);
            }
            local_c = local_c - iVar6;
            FUN_00a27820((int)*psVar10,(int)psVar10[-1]);
            psVar10 = psVar10 + 4;
            local_4 = local_4 + 1;
          } while (local_4 < iVar4);
        }
      }
    }
    if (!bVar3) {
      return;
    }
  } while( true );
}



/* ===================================================== */
/* FUN_00a27120 @ 00a27120 */
/* ===================================================== */

void FUN_00a27120(void)

{
  (**(code **)(*DAT_011be760 + 100))(0);
  DAT_00a2c488 = 0xffffffff;
  DAT_00a2c484 = 0xffffffff;
  return;
}



/* ===================================================== */
/* FUN_00a27140 @ 00a27140 */
/* ===================================================== */

void __fastcall FUN_00a27140(undefined4 *param_1)

{
  *param_1 = 0;
  param_1[1] = 0xffffffff;
  param_1[5] = 0;
  param_1[4] = 0;
  param_1[3] = 0;
  param_1[2] = 0;
  param_1[7] = &DAT_011c4cd0;
  param_1[6] = &DAT_011c4cd0;
  param_1[10] = 0x7fffffff;
  param_1[9] = &DAT_011be868;
  param_1[8] = &DAT_011be868;
  param_1[0xb] = 0;
  return;
}



/* ===================================================== */
/* FUN_00a27180 @ 00a27180 */
/* ===================================================== */

void __fastcall FUN_00a27180(int *param_1)

{
  if (param_1 != DAT_012141d0) {
    if ((undefined4 *)*param_1 != (undefined4 *)0x0) {
      (*(code *)**(undefined4 **)*param_1)(1);
    }
    param_1[1] = -1;
    param_1[5] = 0;
    param_1[4] = 0;
    param_1[3] = 0;
    param_1[2] = 0;
    param_1[7] = (int)&DAT_011c4cd0;
    param_1[6] = (int)&DAT_011c4cd0;
    param_1[0xb] = 0;
    param_1[9] = (int)&DAT_011be868;
    param_1[8] = (int)&DAT_011be868;
    param_1[10] = 0x7fffffff;
  }
  return;
}



/* ===================================================== */
/* FUN_00a271e0 @ 00a271e0 */
/* ===================================================== */

undefined4 __thiscall FUN_00a271e0(int *param_1,int *param_2,uint param_3,int param_4,uint param_5)

{
  uint uVar1;
  int iVar2;
  undefined4 *puVar3;
  int iVar4;
  
  uVar1 = (int)param_3 >> 7 & 3;
  if ((param_4 == -1) && (uVar1 < 2)) {
    return 0;
  }
  if ((param_5 == 1) && (DAT_00b2d188 == 0)) {
    param_5 = 0;
  }
  if (param_5 == 0) {
    if ((DAT_00b2d07c < 0x10) || ((param_3 & 0x1000) != 0)) {
LAB_00a2727a:
      iVar2 = FUN_00a03460(param_4,param_3,param_2);
    }
    else {
      iVar2 = FUN_00a03980(param_4,param_3,param_2,&DAT_00aad078);
    }
LAB_00a27282:
    *param_1 = iVar2;
  }
  else {
    if (param_5 == 1) {
      if (DAT_00b2d07c < 0x10) goto LAB_00a2727a;
      iVar2 = FUN_00a03980(param_4,param_3,param_2,&DAT_00a8d078);
      goto LAB_00a27282;
    }
    if (param_5 == 2) {
      iVar2 = FUN_00a03980(param_4,param_3,param_2,&DAT_00acd078);
      goto LAB_00a27282;
    }
  }
  if (*param_1 == 0) {
    return 0;
  }
  param_1[1] = param_4;
  param_1[2] = *param_2;
  param_1[3] = param_2[1];
  param_1[4] = param_2[2];
  param_1[5] = param_2[3];
  iVar4 = param_2[2] - *param_2;
  iVar2 = *param_2 >> 5;
  if (iVar4 == 0x20) {
    iVar2 = iVar2 + 0x11;
LAB_00a272ea:
    puVar3 = &DAT_011c4cd0 + iVar2 * 0x101;
  }
  else if (iVar4 == 0x40) {
    puVar3 = &DAT_011c4cd0 + (iVar2 + 9) * 0x101;
  }
  else {
    if (iVar4 == 0x80) {
      iVar2 = iVar2 + 1;
      goto LAB_00a272ea;
    }
    puVar3 = &DAT_011c4cd0;
  }
  param_1[6] = (int)puVar3;
  iVar4 = param_2[3] - param_2[1];
  iVar2 = param_2[1] >> 5;
  if (iVar4 == 0x20) {
    iVar2 = iVar2 + 0x11;
LAB_00a2733b:
    puVar3 = &DAT_011c4cd0 + iVar2 * 0x101;
  }
  else if (iVar4 == 0x40) {
    puVar3 = &DAT_011c4cd0 + (iVar2 + 9) * 0x101;
  }
  else {
    if (iVar4 == 0x80) {
      iVar2 = iVar2 + 1;
      goto LAB_00a2733b;
    }
    puVar3 = &DAT_011c4cd0;
  }
  param_1[7] = (int)puVar3;
  iVar4 = param_2[2] - *param_2;
  iVar2 = *param_2 >> 5;
  if (iVar4 == 0x20) {
    iVar2 = iVar2 + 0x11;
LAB_00a2738b:
    puVar3 = &DAT_011be868 + iVar2 * 0x101;
  }
  else if (iVar4 == 0x40) {
    puVar3 = &DAT_011be868 + (iVar2 + 9) * 0x101;
  }
  else {
    if (iVar4 == 0x80) {
      iVar2 = iVar2 + 1;
      goto LAB_00a2738b;
    }
    puVar3 = &DAT_011be868;
  }
  param_1[8] = (int)puVar3;
  iVar4 = param_2[3] - param_2[1];
  iVar2 = param_2[1] >> 5;
  if (iVar4 == 0x20) {
    iVar2 = iVar2 + 0x11;
  }
  else {
    if (iVar4 == 0x40) {
      puVar3 = &DAT_011be868 + (iVar2 + 9) * 0x101;
      goto LAB_00a273ea;
    }
    if (iVar4 != 0x80) {
      puVar3 = &DAT_011be868;
      goto LAB_00a273ea;
    }
    iVar2 = iVar2 + 1;
  }
  puVar3 = &DAT_011be868 + iVar2 * 0x101;
LAB_00a273ea:
  param_1[9] = (int)puVar3;
  param_1[0xb] = param_5 & 2;
  param_1[10] = 0;
  param_1[0xc] = param_3 & 0x1000 | uVar1;
  return 1;
}



/* ===================================================== */
/* FUN_00a27430 @ 00a27430 */
/* ===================================================== */

undefined4 __thiscall FUN_00a27430(int *param_1,uint param_2,int param_3,int param_4)

{
  int iVar1;
  undefined4 uVar2;
  
  if (*param_1 == 0) {
    return 0;
  }
  iVar1 = 0;
  if (param_3 != 0) {
    iVar1 = FUN_00a03430(param_2);
    if (iVar1 == -1) {
      return 0;
    }
    iVar1 = FUN_00a03400(param_3);
  }
  if ((param_4 == 0) && (((param_2 & 0x1000) == 0 || (Zoom8BitTextures == 0)))) {
    uVar2 = 1;
  }
  else {
    uVar2 = 0;
  }
  SetTextureFilterMode(uVar2);
  DAT_00a2c484 = param_1[1];
  DAT_00a2c488 = param_2;
  DAT_00a2c48c = param_4;
  if (iVar1 == 0) {
    FUN_00a03db0(param_2 >> 0xd & 1);
  }
  else {
    FUN_00a03c80(param_2);
  }
  iVar1 = (**(code **)(*DAT_011be760 + 100))(*param_1);
  DAT_011cb1c0 = param_1[6];
  DAT_011cb1c4 = param_1[7];
  DAT_011cb1c8 = param_1[8];
  DAT_011cb1cc = param_1[9];
  if (iVar1 < 0) {
    (**(code **)(*DAT_011be760 + 100))(0);
    return 0;
  }
  param_1[10] = DAT_011be75c;
  return 1;
}



/* ===================================================== */
/* FUN_00a27530 @ 00a27530 */
/* ===================================================== */

void __fastcall FUN_00a27530(undefined4 *param_1)

{
  undefined4 *puVar1;
  int iVar2;
  
  iVar2 = 2000;
  puVar1 = param_1;
  do {
    puVar1 = puVar1 + 1;
    *puVar1 = DAT_012141d0;
    iVar2 = iVar2 + -1;
  } while (iVar2 != 0);
  *param_1 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a27560 @ 00a27560 */
/* ===================================================== */

void __fastcall FUN_00a27560(undefined4 *param_1)

{
  undefined4 *puVar1;
  int iVar2;
  
  iVar2 = 0;
  do {
    FUN_00a275a0(iVar2);
    iVar2 = iVar2 + 1;
  } while (iVar2 < 2000);
  iVar2 = 2000;
  puVar1 = param_1;
  do {
    puVar1 = puVar1 + 1;
    *puVar1 = DAT_012141d0;
    iVar2 = iVar2 + -1;
  } while (iVar2 != 0);
  *param_1 = 0;
  return;
}



/* ===================================================== */
/* FUN_00a275a0 @ 00a275a0 */
/* ===================================================== */

void __thiscall FUN_00a275a0(int *param_1,int param_2)

{
  int iVar1;
  
  iVar1 = param_1[param_2 + 1];
  if (iVar1 != DAT_012141d0) {
    if (iVar1 != 0) {
      FUN_00a27180();
      Free(iVar1);
    }
    param_1[param_2 + 1] = DAT_012141d0;
    *param_1 = *param_1 + -1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a275e0 @ 00a275e0 */
/* ===================================================== */

int __thiscall
FUN_00a275e0(int *param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4,int param_5,
            undefined4 param_6)

{
  int iVar1;
  
  FUN_00a275a0(param_5);
  iVar1 = Malloc(0x34);
  if (iVar1 == 0) {
    iVar1 = 0;
  }
  else {
    iVar1 = FUN_00a27140();
  }
  param_1[param_5 + 1] = iVar1;
  *param_1 = *param_1 + 1;
  iVar1 = FUN_00a271e0(param_2,param_3,param_4,param_6);
  if (iVar1 == 0) {
    FUN_00a275a0(param_5);
  }
  return iVar1;
}



/* ===================================================== */
/* FUN_00a27650 @ 00a27650 */
/* ===================================================== */

undefined4 * __fastcall FUN_00a27650(undefined4 *param_1)

{
  int iVar1;
  int iVar2;
  undefined4 uVar3;
  undefined4 *puVar4;
  
  if (DAT_012141d0 == 0) {
    iVar1 = Malloc(0x34);
    if (iVar1 == 0) {
      DAT_012141d0 = 0;
    }
    else {
      DAT_012141d0 = FUN_00a27140();
    }
  }
  puVar4 = param_1 + 0x20;
  for (iVar1 = 0x8000; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar4 = 0;
    puVar4 = puVar4 + 1;
  }
  puVar4 = param_1 + 0x8020;
  for (iVar1 = 1000; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar4 = 0xffffffff;
    puVar4 = puVar4 + 1;
  }
  iVar1 = 0x20;
  puVar4 = param_1;
  do {
    iVar2 = Malloc(0x1f44);
    if (iVar2 == 0) {
      uVar3 = 0;
    }
    else {
      uVar3 = FUN_00a27530();
    }
    *puVar4 = uVar3;
    puVar4 = puVar4 + 1;
    iVar1 = iVar1 + -1;
  } while (iVar1 != 0);
  return param_1;
}



/* ===================================================== */
/* FUN_00a276d0 @ 00a276d0 */
/* ===================================================== */

void __fastcall FUN_00a276d0(int *param_1)

{
  int iVar1;
  int iVar2;
  
  iVar2 = 0x20;
  do {
    iVar1 = *param_1;
    if (iVar1 != 0) {
      FUN_00a27560();
      Free(iVar1);
    }
    param_1 = param_1 + 1;
    iVar2 = iVar2 + -1;
  } while (iVar2 != 0);
  return;
}



/* ===================================================== */
/* FUN_00a27700 @ 00a27700 */
/* ===================================================== */

void FUN_00a27700(void)

{
  int iVar1;
  
  iVar1 = 0;
  do {
    FUN_00a275a0(iVar1);
    iVar1 = iVar1 + 1;
  } while (iVar1 < 2000);
  return;
}



/* ===================================================== */
/* FUN_00a27730 @ 00a27730 */
/* ===================================================== */

void __fastcall FUN_00a27730(int param_1)

{
  int iVar1;
  undefined4 *puVar2;
  
  FUN_00a27120();
  iVar1 = 0;
  do {
    FUN_00a27700(iVar1);
    iVar1 = iVar1 + 1;
  } while (iVar1 < 0x20);
  puVar2 = (undefined4 *)(param_1 + 0x80);
  for (iVar1 = 0x8000; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = 0;
    puVar2 = puVar2 + 1;
  }
  puVar2 = (undefined4 *)(param_1 + 0x20080);
  for (iVar1 = 1000; iVar1 != 0; iVar1 = iVar1 + -1) {
    *puVar2 = 0xffffffff;
    puVar2 = puVar2 + 1;
  }
  return;
}



/* ===================================================== */
/* FUN_00a27770 @ 00a27770 */
/* ===================================================== */

void __thiscall FUN_00a27770(int param_1,int param_2,int param_3)

{
  short sVar1;
  short *psVar2;
  int iVar3;
  
  FUN_00a27120();
  param_3 = (param_3 >> 4) + param_2;
  if (param_2 < param_3) {
    psVar2 = (short *)(param_1 + 0x80 + param_2 * 2);
    do {
      sVar1 = *psVar2;
      if (sVar1 == 0) {
        return;
      }
      iVar3 = 0x20;
      do {
        FUN_00a275a0((int)sVar1);
        iVar3 = iVar3 + -1;
      } while (iVar3 != 0);
      psVar2 = psVar2 + 1;
      param_3 = param_3 + 1;
    } while (param_2 < param_3);
  }
  return;
}



/* ===================================================== */
/* FUN_00a277f0 @ 00a277f0 */
/* ===================================================== */

void __thiscall FUN_00a277f0(int param_1,undefined4 param_2,int param_3)

{
  int iVar1;
  
  FUN_00a27120();
  iVar1 = (int)*(short *)(param_1 + 0x80 + param_3 * 2);
  if (iVar1 != 0) {
    FUN_00a275a0(iVar1);
  }
  return;
}



/* ===================================================== */
/* FUN_00a27820 @ 00a27820 */
/* ===================================================== */

void FUN_00a27820(undefined4 param_1,int param_2)

{
  FUN_00a27120();
  if (param_2 != 0) {
    FUN_00a275a0(param_2);
  }
  return;
}



/* ===================================================== */
/* FUN_00a27850 @ 00a27850 */
/* ===================================================== */

void FUN_00a27850(undefined4 param_1,undefined4 param_2,undefined4 param_3,undefined4 param_4)

{
  undefined4 uVar1;
  
  uVar1 = FUN_00a278c0(param_3);
  FUN_00a275e0(param_1,param_2,param_3,uVar1,param_4);
  return;
}



/* ===================================================== */
/* FUN_00a27880 @ 00a27880 */
/* ===================================================== */

undefined4 __thiscall
FUN_00a27880(int param_1,undefined4 param_2,int param_3,undefined4 param_4,undefined4 param_5)

{
  undefined4 uVar1;
  
  if (*(short *)(param_1 + 0x80 + param_3 * 2) == 0) {
    return 0;
  }
  uVar1 = FUN_00a27430(param_2,param_4,param_5);
  return uVar1;
}



/* ===================================================== */
/* FUN_00a278c0 @ 00a278c0 */
/* ===================================================== */

int __thiscall FUN_00a278c0(int param_1,int param_2)

{
  short sVar1;
  int iVar2;
  short *psVar3;
  
  sVar1 = *(short *)(param_1 + 0x80 + param_2 * 2);
  if (sVar1 != 0) {
    return (int)sVar1;
  }
  iVar2 = 1;
  psVar3 = (short *)(param_1 + 0x20082);
  do {
    if (*psVar3 == -1) {
      *(short *)(param_1 + 0x20080 + iVar2 * 2) = (short)param_2;
      *(short *)(param_1 + 0x80 + param_2 * 2) = (short)iVar2;
      return iVar2;
    }
    iVar2 = iVar2 + 1;
    psVar3 = psVar3 + 1;
  } while (iVar2 < 2000);
  return 1999;
}



/* ===================================================== */
/* FUN_00a27920 @ 00a27920 */
/* ===================================================== */

bool __thiscall FUN_00a27920(int param_1,RECT *param_2,uint param_3,int param_4)

{
  int iVar1;
  BOOL BVar2;
  tagRECT local_10;
  
  iVar1 = (int)*(short *)(param_1 + 0x80 + param_4 * 2);
  if (iVar1 == 0) {
    return false;
  }
  iVar1 = *(int *)(*(int *)(param_1 + (param_3 & 0x1f) * 4) + 4 + iVar1 * 4);
  if (iVar1 != DAT_012141d0) {
    *(undefined4 *)(iVar1 + 0x28) = DAT_011be75c;
  }
  BVar2 = SubtractRect(&local_10,param_2,(RECT *)(iVar1 + 8));
  return BVar2 == 0;
}



/* ===================================================== */
/* ReInitializeAllTextures @ 00a27980 */
/* ===================================================== */

void ReInitializeAllTextures(void)

{
                    /* 0x27980  785  ReInitializeAllTextures */
  FUN_00a27730();
  return;
}



/* ===================================================== */
/* ReleaseTexture @ 00a27990 */
/* ===================================================== */

void ReleaseTexture(undefined4 param_1)

{
                    /* 0x27990  788  ReleaseTexture */
  FUN_00a27700(param_1);
  return;
}



/* ===================================================== */
/* ReleaseSingleTexture @ 00a279b0 */
/* ===================================================== */

undefined4 ReleaseSingleTexture(undefined4 param_1,undefined4 param_2)

{
                    /* 0x279b0  787  ReleaseSingleTexture */
  FUN_00a277f0(param_1,param_2);
  return 1;
}



/* ===================================================== */
/* DirectDrawEnumerateA @ 00a279d0 */
/* ===================================================== */

void DirectDrawEnumerateA(void)

{
                    /* WARNING: Could not recover jumptable at 0x00a279d0. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  DirectDrawEnumerateA();
  return;
}



/* ===================================================== */
/* DirectDrawCreate @ 00a279d6 */
/* ===================================================== */

void DirectDrawCreate(void)

{
                    /* WARNING: Could not recover jumptable at 0x00a279d6. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  DirectDrawCreate();
  return;
}



/* ===================================================== */
/* _strrchr @ 00a279e0 */
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
/* _strchr @ 00a27a20 */
/* ===================================================== */

/* Library Function - Single Match
    _strchr
   
   Libraries: Visual Studio 1998 Debug, Visual Studio 1998 Release */

char * __cdecl _strchr(char *_Str,int _Val)

{
  uint uVar1;
  char cVar2;
  uint uVar3;
  uint uVar4;
  uint *puVar5;
  
  while (((uint)_Str & 3) != 0) {
    uVar1 = *(uint *)_Str;
    if ((char)uVar1 == (char)_Val) {
      return (char *)(uint *)_Str;
    }
    _Str = (char *)((int)_Str + 1);
    if ((char)uVar1 == '\0') {
      return (char *)0x0;
    }
  }
  while( true ) {
    while( true ) {
      uVar1 = *(uint *)_Str;
      uVar4 = uVar1 ^ CONCAT22(CONCAT11((char)_Val,(char)_Val),CONCAT11((char)_Val,(char)_Val));
      uVar3 = uVar1 ^ 0xffffffff ^ uVar1 + 0x7efefeff;
      puVar5 = (uint *)((int)_Str + 4);
      if (((uVar4 ^ 0xffffffff ^ uVar4 + 0x7efefeff) & 0x81010100) != 0) break;
      _Str = (char *)puVar5;
      if ((uVar3 & 0x81010100) != 0) {
        if ((uVar3 & 0x1010100) != 0) {
          return (char *)0x0;
        }
        if ((uVar1 + 0x7efefeff & 0x80000000) == 0) {
          return (char *)0x0;
        }
      }
    }
    uVar1 = *(uint *)_Str;
    if ((char)uVar1 == (char)_Val) {
      return (char *)(uint *)_Str;
    }
    if ((char)uVar1 == '\0') {
      return (char *)0x0;
    }
    cVar2 = (char)(uVar1 >> 8);
    if (cVar2 == (char)_Val) {
      return (char *)((int)_Str + 1);
    }
    if (cVar2 == '\0') {
      return (char *)0x0;
    }
    cVar2 = (char)(uVar1 >> 0x10);
    if (cVar2 == (char)_Val) {
      return (char *)((int)_Str + 2);
    }
    if (cVar2 == '\0') break;
    cVar2 = (char)(uVar1 >> 0x18);
    if (cVar2 == (char)_Val) {
      return (char *)((int)_Str + 3);
    }
    _Str = (char *)puVar5;
    if (cVar2 == '\0') {
      return (char *)0x0;
    }
  }
  return (char *)0x0;
}



/* ===================================================== */
/* __alldiv @ 00a27ae0 */
/* ===================================================== */

/* Library Function - Single Match
    __alldiv
   
   Library: Visual Studio */

undefined8 __alldiv(uint param_1,uint param_2,uint param_3,uint param_4)

{
  ulonglong uVar1;
  longlong lVar2;
  uint uVar3;
  int iVar4;
  uint uVar5;
  uint uVar6;
  uint uVar7;
  uint uVar8;
  bool bVar10;
  char cVar11;
  uint uVar9;
  
  cVar11 = (int)param_2 < 0;
  if ((bool)cVar11) {
    bVar10 = param_1 != 0;
    param_1 = -param_1;
    param_2 = -(uint)bVar10 - param_2;
  }
  if ((int)param_4 < 0) {
    cVar11 = cVar11 + '\x01';
    bVar10 = param_3 != 0;
    param_3 = -param_3;
    param_4 = -(uint)bVar10 - param_4;
  }
  uVar3 = param_1;
  uVar5 = param_3;
  uVar6 = param_2;
  uVar9 = param_4;
  if (param_4 == 0) {
    uVar3 = param_2 / param_3;
    iVar4 = (int)(((ulonglong)param_2 % (ulonglong)param_3 << 0x20 | (ulonglong)param_1) /
                 (ulonglong)param_3);
  }
  else {
    do {
      uVar8 = uVar9 >> 1;
      uVar5 = uVar5 >> 1 | (uint)((uVar9 & 1) != 0) << 0x1f;
      uVar7 = uVar6 >> 1;
      uVar3 = uVar3 >> 1 | (uint)((uVar6 & 1) != 0) << 0x1f;
      uVar6 = uVar7;
      uVar9 = uVar8;
    } while (uVar8 != 0);
    uVar1 = CONCAT44(uVar7,uVar3) / (ulonglong)uVar5;
    iVar4 = (int)uVar1;
    lVar2 = (ulonglong)param_3 * (uVar1 & 0xffffffff);
    uVar3 = (uint)((ulonglong)lVar2 >> 0x20);
    uVar5 = uVar3 + iVar4 * param_4;
    if (((CARRY4(uVar3,iVar4 * param_4)) || (param_2 < uVar5)) ||
       ((param_2 <= uVar5 && (param_1 < (uint)lVar2)))) {
      iVar4 = iVar4 + -1;
    }
    uVar3 = 0;
  }
  if (cVar11 == '\x01') {
    bVar10 = iVar4 != 0;
    iVar4 = -iVar4;
    uVar3 = -(uint)bVar10 - uVar3;
  }
  return CONCAT44(uVar3,iVar4);
}



/* ===================================================== */
/* __global_unwind2 @ 00a27b8c */
/* ===================================================== */

/* Library Function - Single Match
    __global_unwind2
   
   Library: Visual Studio */

void __global_unwind2(PVOID param_1)

{
  RtlUnwind(param_1,(PVOID)0xa27ba4,(PEXCEPTION_RECORD)0x0,(PVOID)0x0);
  return;
}



/* ===================================================== */
/* __local_unwind2 @ 00a27bce */
/* ===================================================== */

/* Library Function - Single Match
    __local_unwind2
   
   Libraries: Visual Studio 1998 Debug, Visual Studio 1998 Release, Visual Studio 2003 Debug, Visual
   Studio 2003 Release */

void __local_unwind2(int param_1,int param_2)

{
  int iVar1;
  int iVar2;
  undefined4 *unaff_FS_OFFSET;
  undefined4 uStack_1c;
  undefined1 *puStack_18;
  undefined4 local_14;
  int iStack_10;
  
  iStack_10 = param_1;
  puStack_18 = &LAB_00a27bac;
  uStack_1c = *unaff_FS_OFFSET;
  *unaff_FS_OFFSET = &uStack_1c;
  while( true ) {
    iVar1 = *(int *)(param_1 + 8);
    iVar2 = *(int *)(param_1 + 0xc);
    if ((iVar2 == -1) || (iVar2 == param_2)) break;
    local_14 = *(undefined4 *)(iVar1 + iVar2 * 0xc);
    *(undefined4 *)(param_1 + 0xc) = local_14;
    if (*(int *)(iVar1 + 4 + iVar2 * 0xc) == 0) {
      FUN_00a27c62(0x101);
      (**(code **)(iVar1 + 8 + iVar2 * 0xc))();
    }
  }
  *unaff_FS_OFFSET = uStack_1c;
  return;
}



/* ===================================================== */
/* FUN_00a27c62 @ 00a27c62 */
/* ===================================================== */

void FUN_00a27c62(void)

{
  undefined4 in_EAX;
  int unaff_EBP;
  
  DAT_00a2c688 = *(undefined4 *)(unaff_EBP + 8);
  DAT_00a2c684 = in_EAX;
  DAT_00a2c68c = unaff_EBP;
  return;
}



/* ===================================================== */
/* FUN_00a27d41 @ 00a27d41 */
/* ===================================================== */

void FUN_00a27d41(int param_1)

{
  __local_unwind2(*(undefined4 *)(param_1 + 0x18),*(undefined4 *)(param_1 + 0x1c));
  return;
}



/* ===================================================== */
/* __ftol @ 00a27d5c */
/* ===================================================== */

/* Library Function - Single Match
    __ftol
   
   Library: Visual Studio */

longlong __ftol(void)

{
  float10 in_ST0;
  
  return (longlong)ROUND(in_ST0);
}



/* ===================================================== */
/* __allshr @ 00a27d90 */
/* ===================================================== */

/* Library Function - Single Match
    __allshr
   
   Library: Visual Studio */

undefined8 __fastcall __allshr(byte param_1,int param_2)

{
  uint in_EAX;
  int iVar1;
  
  iVar1 = param_2 >> 0x1f;
  if (0x3f < param_1) {
    return CONCAT44(iVar1,iVar1);
  }
  if (param_1 < 0x20) {
    return CONCAT44(param_2 >> (param_1 & 0x1f),
                    in_EAX >> (param_1 & 0x1f) | param_2 << 0x20 - (param_1 & 0x1f));
  }
  return CONCAT44(iVar1,param_2 >> (param_1 & 0x1f));
}



/* ===================================================== */
/* __allmul @ 00a27dc0 */
/* ===================================================== */

/* Library Function - Single Match
    __allmul
   
   Library: Visual Studio */

longlong __allmul(uint param_1,int param_2,uint param_3,int param_4)

{
  if (param_4 == 0 && param_2 == 0) {
    return (ulonglong)param_1 * (ulonglong)param_3;
  }
  return CONCAT44((int)((ulonglong)param_1 * (ulonglong)param_3 >> 0x20) +
                  param_2 * param_3 + param_1 * param_4,
                  (int)((ulonglong)param_1 * (ulonglong)param_3));
}



/* ===================================================== */
/* FUN_00a27e00 @ 00a27e00 */
/* ===================================================== */

void FUN_00a27e00(uint param_1,uint param_2,uint param_3,code *param_4)

{
  uint uVar1;
  int iVar2;
  uint uVar3;
  uint local_100;
  uint *local_fc;
  uint *local_f8;
  int local_f4;
  uint local_f0 [30];
  uint local_78 [30];
  
  if ((param_2 < 2) || (param_3 == 0)) {
    return;
  }
  local_100 = (param_2 - 1) * param_3 + param_1;
  local_fc = local_f0;
  local_f8 = local_78;
  local_f4 = 0;
LAB_00a27e54:
  uVar1 = (local_100 - param_1) / param_3 + 1;
  if (8 < uVar1) {
    FUN_00a28010((uVar1 >> 1) * param_3 + param_1,param_1,param_3);
    uVar3 = local_100 + param_3;
    uVar1 = param_1;
LAB_00a27ece:
    uVar1 = uVar1 + param_3;
    if (uVar1 <= local_100) goto code_r0x00a27ed8;
    goto LAB_00a27ee8;
  }
  FUN_00a27fb0(param_1,local_100,param_3,param_4);
  goto LAB_00a27e75;
code_r0x00a27ed8:
  iVar2 = (*param_4)(uVar1,param_1);
  if (iVar2 < 1) goto LAB_00a27ece;
LAB_00a27ee8:
  do {
    uVar3 = uVar3 - param_3;
    if (uVar3 <= param_1) break;
    iVar2 = (*param_4)(uVar3,param_1);
  } while (-1 < iVar2);
  if (uVar1 <= uVar3) {
    FUN_00a28010(uVar1,uVar3,param_3);
    goto LAB_00a27ece;
  }
  FUN_00a28010(param_1,uVar3,param_3);
  if ((int)((uVar3 - param_1) + -1) < (int)(local_100 - uVar1)) {
    if (uVar1 < local_100) {
      *local_f8 = uVar1;
      *local_fc = local_100;
      local_f4 = local_f4 + 1;
      local_f8 = local_f8 + 1;
      local_fc = local_fc + 1;
    }
    if (param_1 + param_3 < uVar3) {
      local_100 = uVar3 - param_3;
      goto LAB_00a27e54;
    }
  }
  else {
    if (param_1 + param_3 < uVar3) {
      *local_f8 = param_1;
      *local_fc = uVar3 - param_3;
      local_f4 = local_f4 + 1;
      local_f8 = local_f8 + 1;
      local_fc = local_fc + 1;
    }
    param_1 = uVar1;
    if (uVar1 < local_100) goto LAB_00a27e54;
  }
LAB_00a27e75:
  local_f4 = local_f4 + -1;
  local_f8 = local_f8 + -1;
  local_fc = local_fc + -1;
  if (local_f4 < 0) {
    return;
  }
  local_100 = *local_fc;
  param_1 = *local_f8;
  goto LAB_00a27e54;
}



/* ===================================================== */
/* FUN_00a27fb0 @ 00a27fb0 */
/* ===================================================== */

void FUN_00a27fb0(uint param_1,uint param_2,int param_3,code *param_4)

{
  uint uVar1;
  uint uVar2;
  int iVar3;
  
  for (; uVar2 = param_1, uVar1 = param_1, param_1 < param_2; param_2 = param_2 - param_3) {
    while (uVar1 = param_3 + uVar1, uVar1 <= param_2) {
      iVar3 = (*param_4)(uVar1,uVar2);
      if (0 < iVar3) {
        uVar2 = uVar1;
      }
    }
    FUN_00a28010(uVar2,param_2,param_3);
  }
  return;
}



/* ===================================================== */
/* FUN_00a28010 @ 00a28010 */
/* ===================================================== */

void FUN_00a28010(undefined1 *param_1,undefined1 *param_2,int param_3)

{
  undefined1 uVar1;
  
  if (param_1 != param_2) {
    for (; param_3 != 0; param_3 = param_3 + -1) {
      uVar1 = *param_1;
      *param_1 = *param_2;
      param_1 = param_1 + 1;
      *param_2 = uVar1;
      param_2 = param_2 + 1;
    }
  }
  return;
}



/* ===================================================== */
/* RtlUnwind @ 00a28040 */
/* ===================================================== */

void RtlUnwind(PVOID TargetFrame,PVOID TargetIp,PEXCEPTION_RECORD ExceptionRecord,PVOID ReturnValue)

{
                    /* WARNING: Could not recover jumptable at 0x00a28040. Too many branches */
                    /* WARNING: Treating indirect jump as call */
  RtlUnwind(TargetFrame,TargetIp,ExceptionRecord,ReturnValue);
  return;
}



/* ===================================================== */
/* __strcmpi @ 00a28050 */
/* ===================================================== */

/* Library Function - Single Match
    __strcmpi
   
   Libraries: Visual Studio 1998 Debug, Visual Studio 1998 Release */

int __cdecl __strcmpi(char *_Str1,char *_Str2)

{
  char cVar1;
  uint uVar2;
  byte bVar3;
  byte bVar4;
  byte bVar5;
  char cVar6;
  int iVar7;
  
  if (DAT_012141e0 == 0) {
    bVar5 = 0xff;
    do {
      do {
        cVar6 = '\0';
        if (bVar5 == 0) goto LAB_00a2809e;
        bVar5 = *_Str2;
        _Str2 = _Str2 + 1;
        bVar4 = *_Str1;
        _Str1 = _Str1 + 1;
      } while (bVar4 == bVar5);
      bVar3 = bVar5 + 0xbf + (-((byte)(bVar5 + 0xbf) < 0x1a) & 0x20U) + 0x41;
      bVar4 = bVar4 + 0xbf;
      bVar5 = bVar4 + (-(bVar4 < 0x1a) & 0x20U) + 0x41;
    } while (bVar5 == bVar3);
    cVar6 = (bVar5 < bVar3) * -2 + '\x01';
LAB_00a2809e:
    iVar7 = (int)cVar6;
  }
  else {
    iVar7 = 0xff;
    do {
      do {
        if ((char)iVar7 == '\0') {
          return iVar7;
        }
        cVar6 = *_Str2;
        iVar7 = CONCAT31((int3)((uint)iVar7 >> 8),cVar6);
        _Str2 = _Str2 + 1;
        cVar1 = *_Str1;
        _Str1 = _Str1 + 1;
      } while (cVar6 == cVar1);
      bVar5 = FUN_00a280e0(cVar1,iVar7);
      iVar7 = FUN_00a280e0();
    } while (bVar5 == (byte)iVar7);
    uVar2 = (uint)(bVar5 < (byte)iVar7);
    iVar7 = (1 - uVar2) - (uint)(uVar2 != 0);
  }
  return iVar7;
}



/* ===================================================== */
/* FUN_00a280e0 @ 00a280e0 */
/* ===================================================== */

uint FUN_00a280e0(uint param_1)

{
  uint uVar1;
  uint uVar2;
  undefined4 uVar3;
  int iVar4;
  uint local_8 [2];
  
  uVar1 = param_1;
  if (DAT_012141e0 == 0) {
    if ((0x40 < (int)param_1) && ((int)param_1 < 0x5b)) {
      return param_1 + 0x20;
    }
  }
  else {
    if ((int)param_1 < 0x100) {
      if (DAT_00a2c89c < 2) {
        uVar2 = (byte)PTR_DAT_00a2c690[param_1 * 2] & 1;
      }
      else {
        uVar2 = FUN_00a28430(param_1,1);
      }
      if (uVar2 == 0) {
        return uVar1;
      }
    }
    uVar2 = param_1;
    if ((PTR_DAT_00a2c690[((int)uVar1 >> 8 & 0xffU) * 2 + 1] & 0x80) == 0) {
      param_1._0_2_ = (ushort)(byte)uVar1;
      uVar3 = 1;
    }
    else {
      param_1._0_2_ = CONCAT11((byte)uVar1,(char)(uVar1 >> 8));
      param_1._3_1_ = SUB41(uVar2,3);
      param_1._0_3_ = (uint3)(ushort)param_1;
      uVar3 = 2;
    }
    iVar4 = FUN_00a281e0(DAT_012141e0,0x100,&param_1,uVar3,local_8,3,0);
    if (iVar4 == 0) {
      return uVar1;
    }
    if (iVar4 == 1) {
      return local_8[0] & 0xff;
    }
    param_1 = (local_8[0] >> 8 & 0xff) << 8 | local_8[0] & 0xff;
  }
  return param_1;
}



/* ===================================================== */
/* FUN_00a281e0 @ 00a281e0 */
/* ===================================================== */

int FUN_00a281e0(LCID param_1,uint param_2,LPCSTR param_3,LPCWSTR param_4,LPWSTR param_5,int param_6
                ,UINT param_7)

{
  int iVar1;
  int iVar2;
  LPCWSTR lpWideCharStr;
  
  if (DAT_012141f8 == 0) {
    iVar1 = LCMapStringA(0,0x100,"",1,(LPSTR)0x0,0);
    if (iVar1 == 0) {
      iVar1 = LCMapStringW(0,0x100,L"",1,(LPWSTR)0x0,0);
      if (iVar1 == 0) {
        return 0;
      }
      DAT_012141f8 = 1;
    }
    else {
      DAT_012141f8 = 2;
    }
  }
  iVar1 = (int)param_4;
  if (0 < (int)param_4) {
    iVar1 = FUN_00a28400(param_3,param_4);
  }
  if (DAT_012141f8 == 2) {
    iVar1 = LCMapStringA(param_1,param_2,param_3,iVar1,(LPSTR)param_5,param_6);
    return iVar1;
  }
  if (DAT_012141f8 != 1) {
    return DAT_012141f8;
  }
  param_4 = (LPCWSTR)0x0;
  if (param_7 == 0) {
    param_7 = DAT_012141f0;
  }
  iVar2 = MultiByteToWideChar(param_7,9,param_3,iVar1,(LPWSTR)0x0,0);
  if (iVar2 == 0) {
    return 0;
  }
  lpWideCharStr = (LPCWSTR)FUN_00a28620(iVar2 * 2);
  if (lpWideCharStr == (LPCWSTR)0x0) {
    return 0;
  }
  iVar1 = MultiByteToWideChar(param_7,1,param_3,iVar1,lpWideCharStr,iVar2);
  if ((iVar1 != 0) &&
     (iVar1 = LCMapStringW(param_1,param_2,lpWideCharStr,iVar2,(LPWSTR)0x0,0), iVar1 != 0)) {
    if ((param_2 & 0x400) == 0) {
      param_4 = (LPCWSTR)FUN_00a28620(iVar1 * 2);
      if ((param_4 == (LPCWSTR)0x0) ||
         (iVar2 = LCMapStringW(param_1,param_2,lpWideCharStr,iVar2,param_4,iVar1), iVar2 == 0))
      goto LAB_00a283df;
      if (param_6 == 0) {
        iVar1 = WideCharToMultiByte(param_7,0x220,param_4,iVar1,(LPSTR)0x0,0,(LPCSTR)0x0,(LPBOOL)0x0
                                   );
        iVar2 = iVar1;
      }
      else {
        iVar1 = WideCharToMultiByte(param_7,0x220,param_4,iVar1,(LPSTR)param_5,param_6,(LPCSTR)0x0,
                                    (LPBOOL)0x0);
        iVar2 = iVar1;
      }
    }
    else {
      if (param_6 == 0) goto LAB_00a28344;
      if (param_6 < iVar1) goto LAB_00a283df;
      iVar2 = LCMapStringW(param_1,param_2,lpWideCharStr,iVar2,param_5,param_6);
    }
    if (iVar2 != 0) {
LAB_00a28344:
      FUN_00a284d0(lpWideCharStr);
      FUN_00a284d0(param_4);
      return iVar1;
    }
  }
LAB_00a283df:
  FUN_00a284d0(lpWideCharStr);
  FUN_00a284d0(param_4);
  return 0;
}



/* ===================================================== */
/* FUN_00a28400 @ 00a28400 */
/* ===================================================== */

int FUN_00a28400(char *param_1,int param_2)

{
  char *pcVar1;
  int iVar2;
  
  iVar2 = param_2;
  for (pcVar1 = param_1; (iVar2 != 0 && (iVar2 = iVar2 + -1, *pcVar1 != '\0')); pcVar1 = pcVar1 + 1)
  {
  }
  if (*pcVar1 != '\0') {
    return param_2;
  }
  return (int)pcVar1 - (int)param_1;
}



/* ===================================================== */
/* FUN_00a28430 @ 00a28430 */
/* ===================================================== */

uint FUN_00a28430(int param_1,uint param_2)

{
  undefined4 uVar1;
  int iVar2;
  uint local_4;
  
  iVar2 = param_1;
  if (param_1 + 1U < 0x101) {
    return *(ushort *)(PTR_DAT_00a2c690 + param_1 * 2) & param_2;
  }
  if ((PTR_DAT_00a2c690[(param_1 >> 8 & 0xffU) * 2 + 1] & 0x80) == 0) {
    param_1._0_2_ = (ushort)(byte)param_1;
    uVar1 = 1;
  }
  else {
    param_1._0_2_ = CONCAT11((byte)param_1,(char)((uint)param_1 >> 8));
    param_1._3_1_ = SUB41(iVar2,3);
    param_1._0_3_ = (uint3)(ushort)param_1;
    uVar1 = 2;
  }
  iVar2 = FUN_00a286d0(1,&param_1,uVar1,&local_4,0,0);
  if (iVar2 == 0) {
    return 0;
  }
  return local_4 & 0xffff & param_2;
}



/* ===================================================== */
/* FUN_00a284d0 @ 00a284d0 */
/* ===================================================== */

void FUN_00a284d0(LPVOID param_1)

{
  LPVOID lpMem;
  int iVar1;
  undefined4 local_4;
  
  lpMem = param_1;
  if (param_1 != (LPVOID)0x0) {
    iVar1 = FUN_00a28aa0(param_1,&local_4,&param_1);
    if (iVar1 != 0) {
      FUN_00a28b00(local_4,param_1,iVar1);
      return;
    }
    HeapFree(DAT_01214210,0,lpMem);
  }
  return;
}



/* ===================================================== */
/* FUN_00a28620 @ 00a28620 */
/* ===================================================== */

void FUN_00a28620(undefined4 param_1)

{
  FUN_00a28640(param_1,DAT_01214208);
  return;
}



/* ===================================================== */
/* FUN_00a28640 @ 00a28640 */
/* ===================================================== */

int FUN_00a28640(uint param_1,int param_2)

{
  int iVar1;
  
  if (param_1 < 0xffffffe1) {
    if (param_1 == 0) {
      param_1 = 1;
    }
    do {
      if (param_1 < 0xffffffe1) {
        iVar1 = FUN_00a28690(param_1);
      }
      else {
        iVar1 = 0;
      }
      if (iVar1 != 0) {
        return iVar1;
      }
      if (param_2 == 0) {
        return 0;
      }
      iVar1 = FUN_00a28f20(param_1);
    } while (iVar1 != 0);
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a28690 @ 00a28690 */
/* ===================================================== */

void FUN_00a28690(int param_1)

{
  int iVar1;
  uint dwBytes;
  
  dwBytes = param_1 + 0xfU & 0xfffffff0;
  if ((dwBytes <= DAT_00a2e8dc) && (iVar1 = FUN_00a28b60(param_1 + 0xfU >> 4), iVar1 != 0)) {
    return;
  }
  HeapAlloc(DAT_01214210,0,dwBytes);
  return;
}



/* ===================================================== */
/* FUN_00a286d0 @ 00a286d0 */
/* ===================================================== */

BOOL FUN_00a286d0(DWORD param_1,LPCSTR param_2,int param_3,LPWORD param_4,UINT param_5,LCID param_6)

{
  BOOL BVar1;
  int iVar2;
  LPCWSTR lpWideCharStr;
  WORD local_2;
  
  lpWideCharStr = (LPCWSTR)0x0;
  if (DAT_01214200 == 0) {
    BVar1 = GetStringTypeA(0,1,"",1,&local_2);
    if (BVar1 == 0) {
      BVar1 = GetStringTypeW(1,L"",1,&local_2);
      if (BVar1 == 0) {
        return 0;
      }
      DAT_01214200 = 1;
    }
    else {
      DAT_01214200 = 2;
    }
  }
  if (DAT_01214200 == 2) {
    if (param_6 == 0) {
      param_6 = DAT_012141e0;
    }
    BVar1 = GetStringTypeA(param_6,param_1,param_2,param_3,param_4);
    return BVar1;
  }
  param_6 = DAT_01214200;
  if (DAT_01214200 == 1) {
    param_6 = 0;
    if (param_5 == 0) {
      param_5 = DAT_012141f0;
    }
    iVar2 = MultiByteToWideChar(param_5,9,param_2,param_3,(LPWSTR)0x0,0);
    if (iVar2 != 0) {
      lpWideCharStr = (LPCWSTR)FUN_00a29280(2,iVar2);
      if (lpWideCharStr != (LPCWSTR)0x0) {
        iVar2 = MultiByteToWideChar(param_5,1,param_2,param_3,lpWideCharStr,iVar2);
        if (iVar2 != 0) {
          BVar1 = GetStringTypeW(param_1,lpWideCharStr,iVar2,param_4);
          FUN_00a284d0(lpWideCharStr);
          return BVar1;
        }
      }
    }
    FUN_00a284d0(lpWideCharStr);
  }
  return param_6;
}



/* ===================================================== */
/* FUN_00a28800 @ 00a28800 */
/* ===================================================== */

undefined ** FUN_00a28800(void)

{
  bool bVar1;
  undefined4 *lpAddress;
  LPVOID pvVar2;
  int iVar3;
  undefined **ppuVar4;
  undefined **lpMem;
  undefined4 *puVar5;
  
  if (DAT_00a2c8c8 == -1) {
    lpMem = &PTR_LOOP_00a2c8b8;
  }
  else {
    lpMem = HeapAlloc(DAT_01214210,0,0x2020);
    if (lpMem == (undefined **)0x0) {
      return (undefined **)0x0;
    }
  }
  lpAddress = VirtualAlloc((LPVOID)0x0,0x400000,0x2000,4);
  if (lpAddress != (undefined4 *)0x0) {
    pvVar2 = VirtualAlloc(lpAddress,0x10000,0x1000,4);
    if (pvVar2 != (LPVOID)0x0) {
      if (lpMem == &PTR_LOOP_00a2c8b8) {
        if (PTR_LOOP_00a2c8b8 == (undefined *)0x0) {
          PTR_LOOP_00a2c8b8 = (undefined *)&PTR_LOOP_00a2c8b8;
        }
        if (PTR_LOOP_00a2c8bc == (undefined *)0x0) {
          PTR_LOOP_00a2c8bc = (undefined *)&PTR_LOOP_00a2c8b8;
        }
      }
      else {
        *lpMem = (undefined *)&PTR_LOOP_00a2c8b8;
        lpMem[1] = PTR_LOOP_00a2c8bc;
        PTR_LOOP_00a2c8bc = (undefined *)lpMem;
        *(undefined ***)lpMem[1] = lpMem;
      }
      lpMem[5] = (undefined *)(lpAddress + 0x100000);
      lpMem[4] = (undefined *)lpAddress;
      lpMem[2] = (undefined *)(lpMem + 6);
      lpMem[3] = (undefined *)(lpMem + 0x26);
      iVar3 = 0;
      ppuVar4 = lpMem + 6;
      do {
        bVar1 = 0xf < iVar3;
        iVar3 = iVar3 + 1;
        *ppuVar4 = (undefined *)((bVar1 - 1 & 0xf1) - 1);
        ppuVar4[1] = (undefined *)0xf1;
        ppuVar4 = ppuVar4 + 2;
      } while (iVar3 < 0x400);
      puVar5 = lpAddress;
      for (iVar3 = 0x4000; iVar3 != 0; iVar3 = iVar3 + -1) {
        *puVar5 = 0;
        puVar5 = puVar5 + 1;
      }
      if (lpAddress < lpMem[4] + 0x10000) {
        do {
          lpAddress[1] = 0xf0;
          *lpAddress = lpAddress + 2;
          *(undefined1 *)(lpAddress + 0x3e) = 0xff;
          lpAddress = lpAddress + 0x400;
        } while (lpAddress < lpMem[4] + 0x10000);
      }
      return lpMem;
    }
    VirtualFree(lpAddress,0,0x8000);
  }
  if (lpMem != &PTR_LOOP_00a2c8b8) {
    HeapFree(DAT_01214210,0,lpMem);
  }
  return (undefined **)0x0;
}



/* ===================================================== */
/* FUN_00a28970 @ 00a28970 */
/* ===================================================== */

void FUN_00a28970(undefined **param_1)

{
  VirtualFree(param_1[4],0,0x8000);
  if ((undefined **)PTR_LOOP_00a2e8d8 == param_1) {
    PTR_LOOP_00a2e8d8 = param_1[1];
  }
  if (param_1 != &PTR_LOOP_00a2c8b8) {
    *(undefined **)param_1[1] = *param_1;
    *(undefined **)(*param_1 + 4) = param_1[1];
    HeapFree(DAT_01214210,0,param_1);
    return;
  }
  DAT_00a2c8c8 = 0xffffffff;
  return;
}



/* ===================================================== */
/* FUN_00a289d0 @ 00a289d0 */
/* ===================================================== */

void FUN_00a289d0(int param_1)

{
  BOOL BVar1;
  int *piVar2;
  int iVar3;
  int iVar4;
  undefined *puVar5;
  undefined *puVar6;
  
  puVar5 = PTR_LOOP_00a2c8bc;
  do {
    puVar6 = puVar5;
    if (*(int *)(puVar5 + 0x10) != -1) {
      iVar4 = 0;
      piVar2 = (int *)(puVar5 + 0x2010);
      iVar3 = 0x3ff000;
      do {
        if (*piVar2 == 0xf0) {
          BVar1 = VirtualFree((LPVOID)(*(int *)(puVar5 + 0x10) + iVar3),0x1000,0x4000);
          if (BVar1 != 0) {
            *piVar2 = -1;
            DAT_01214204 = DAT_01214204 + -1;
            if ((*(int **)(puVar5 + 0xc) == (int *)0x0) || (piVar2 < *(int **)(puVar5 + 0xc))) {
              *(int **)(puVar5 + 0xc) = piVar2;
            }
            iVar4 = iVar4 + 1;
            param_1 = param_1 + -1;
            if (param_1 == 0) break;
          }
        }
        iVar3 = iVar3 + -0x1000;
        piVar2 = piVar2 + -2;
      } while (-1 < iVar3);
      puVar6 = *(undefined **)(puVar5 + 4);
      if ((iVar4 != 0) && (*(int *)(puVar5 + 0x18) == -1)) {
        iVar3 = 1;
        piVar2 = (int *)(puVar5 + 0x20);
        do {
          if (*piVar2 != -1) break;
          iVar3 = iVar3 + 1;
          piVar2 = piVar2 + 2;
        } while (iVar3 < 0x400);
        if (iVar3 == 0x400) {
          FUN_00a28970(puVar5);
        }
      }
    }
    if ((puVar6 == PTR_LOOP_00a2c8bc) || (puVar5 = puVar6, param_1 < 1)) {
      return;
    }
  } while( true );
}



/* ===================================================== */
/* FUN_00a28aa0 @ 00a28aa0 */
/* ===================================================== */

int FUN_00a28aa0(undefined *param_1,undefined4 *param_2,uint *param_3)

{
  undefined **ppuVar1;
  uint uVar2;
  
  ppuVar1 = &PTR_LOOP_00a2c8b8;
  while ((param_1 <= ppuVar1[4] || (ppuVar1[5] <= param_1))) {
    ppuVar1 = (undefined **)*ppuVar1;
    if (ppuVar1 == &PTR_LOOP_00a2c8b8) {
      return 0;
    }
  }
  if (((uint)param_1 & 0xf) != 0) {
    return 0;
  }
  if (((uint)param_1 & 0xfff) < 0x100) {
    return 0;
  }
  *param_2 = ppuVar1;
  uVar2 = (uint)param_1 & 0xfffff000;
  *param_3 = uVar2;
  return ((int)(param_1 + (-0x100 - uVar2)) >> 4) + 8 + uVar2;
}



/* ===================================================== */
/* FUN_00a28b00 @ 00a28b00 */
/* ===================================================== */

void FUN_00a28b00(int param_1,int param_2,byte *param_3)

{
  int *piVar1;
  int iVar2;
  
  iVar2 = param_2 - *(int *)(param_1 + 0x10) >> 0xc;
  piVar1 = (int *)(param_1 + 0x18 + iVar2 * 8);
  *piVar1 = *(int *)(param_1 + 0x18 + iVar2 * 8) + (uint)*param_3;
  *param_3 = 0;
  piVar1[1] = 0xf1;
  if ((*piVar1 == 0xf0) && (DAT_01214204 = DAT_01214204 + 1, DAT_01214204 == 0x20)) {
    FUN_00a289d0(0x10);
  }
  return;
}



/* ===================================================== */
/* FUN_00a28b60 @ 00a28b60 */
/* ===================================================== */

int * FUN_00a28b60(uint param_1)

{
  undefined **ppuVar1;
  undefined **ppuVar2;
  int *piVar3;
  int *piVar4;
  int *piVar5;
  undefined *puVar6;
  undefined **ppuVar7;
  undefined **ppuVar8;
  int iVar9;
  int *piVar10;
  bool bVar11;
  
  piVar10 = (int *)PTR_LOOP_00a2e8d8;
  do {
    if (piVar10[4] != -1) {
      piVar5 = (int *)piVar10[2];
      iVar9 = ((int)piVar5 + (-0x18 - (int)piVar10) >> 3) * 0x1000 + piVar10[4];
      for (; piVar5 < piVar10 + 0x806; piVar5 = piVar5 + 2) {
        if (((int)param_1 <= *piVar5) && (param_1 < (uint)piVar5[1])) {
          piVar3 = (int *)FUN_00a28da0(iVar9,*piVar5,param_1);
          if (piVar3 != (int *)0x0) {
            PTR_LOOP_00a2e8d8 = (undefined *)piVar10;
            *piVar5 = *piVar5 - param_1;
            piVar10[2] = (int)piVar5;
            return piVar3;
          }
          piVar5[1] = param_1;
        }
        iVar9 = iVar9 + 0x1000;
      }
      piVar3 = (int *)piVar10[2];
      iVar9 = piVar10[4];
      for (piVar5 = piVar10 + 6; piVar5 < piVar3; piVar5 = piVar5 + 2) {
        if (((int)param_1 <= *piVar5) && (param_1 < (uint)piVar5[1])) {
          piVar4 = (int *)FUN_00a28da0(iVar9,*piVar5,param_1);
          if (piVar4 != (int *)0x0) {
            PTR_LOOP_00a2e8d8 = (undefined *)piVar10;
            *piVar5 = *piVar5 - param_1;
            piVar10[2] = (int)piVar5;
            return piVar4;
          }
          piVar5[1] = param_1;
        }
        iVar9 = iVar9 + 0x1000;
      }
    }
    piVar10 = (int *)*piVar10;
  } while (piVar10 != (int *)PTR_LOOP_00a2e8d8);
  ppuVar8 = &PTR_LOOP_00a2c8b8;
  while ((ppuVar8[4] == (undefined *)0xffffffff || (ppuVar8[3] == (undefined *)0x0))) {
    ppuVar8 = (undefined **)*ppuVar8;
    if (ppuVar8 == &PTR_LOOP_00a2c8b8) {
      puVar6 = (undefined *)FUN_00a28800();
      if (puVar6 == (undefined *)0x0) {
        return (int *)0x0;
      }
      piVar10 = *(int **)(puVar6 + 0x10);
      *(char *)(piVar10 + 2) = (char)param_1;
      PTR_LOOP_00a2e8d8 = puVar6;
      *piVar10 = (int)piVar10 + param_1 + 8;
      piVar10[1] = 0xf0 - param_1;
      *(uint *)(puVar6 + 0x18) = *(int *)(puVar6 + 0x18) - (param_1 & 0xff);
      return piVar10 + 0x40;
    }
  }
  ppuVar2 = (undefined **)ppuVar8[3];
  puVar6 = *ppuVar2;
  piVar10 = (int *)(ppuVar8[4] + ((int)ppuVar2 + (-0x18 - (int)ppuVar8) >> 3) * 0x1000);
  ppuVar7 = ppuVar2;
  for (iVar9 = 0; (puVar6 == (undefined *)0xffffffff && (iVar9 < 0x10)); iVar9 = iVar9 + 1) {
    puVar6 = ppuVar7[2];
    ppuVar7 = ppuVar7 + 2;
  }
  piVar5 = VirtualAlloc(piVar10,iVar9 << 0xc,0x1000,4);
  if (piVar5 != piVar10) {
    return (int *)0x0;
  }
  ppuVar7 = ppuVar2;
  if (0 < iVar9) {
    piVar5 = piVar10 + 1;
    do {
      *piVar5 = 0xf0;
      piVar5[-1] = (int)(piVar5 + 1);
      *(undefined1 *)(piVar5 + 0x3d) = 0xff;
      *ppuVar7 = (undefined *)0xf0;
      ppuVar7[1] = (undefined *)0xf1;
      piVar5 = piVar5 + 0x400;
      ppuVar7 = ppuVar7 + 2;
      iVar9 = iVar9 + -1;
    } while (iVar9 != 0);
  }
  ppuVar1 = ppuVar8 + 0x806;
  bVar11 = false;
  if (ppuVar7 < ppuVar1) {
    do {
      if (*ppuVar7 == (undefined *)0xffffffff) break;
      ppuVar7 = ppuVar7 + 2;
    } while (ppuVar7 < ppuVar1);
    bVar11 = ppuVar7 < ppuVar1;
  }
  PTR_LOOP_00a2e8d8 = (undefined *)ppuVar8;
  ppuVar8[3] = (undefined *)(-(uint)bVar11 & (uint)ppuVar7);
  *(char *)(piVar10 + 2) = (char)param_1;
  ppuVar8[2] = (undefined *)ppuVar2;
  *ppuVar2 = *ppuVar2 + -param_1;
  piVar10[1] = piVar10[1] - param_1;
  *piVar10 = (int)piVar10 + param_1 + 8;
  return piVar10 + 0x40;
}



/* ===================================================== */
/* FUN_00a28da0 @ 00a28da0 */
/* ===================================================== */

int FUN_00a28da0(int *param_1,uint param_2,uint param_3)

{
  byte bVar1;
  byte *pbVar2;
  byte *pbVar3;
  byte *pbVar4;
  uint uVar5;
  byte *pbVar6;
  
  pbVar2 = (byte *)*param_1;
  if (param_3 <= (uint)param_1[1]) {
    *pbVar2 = (byte)param_3;
    if (pbVar2 + param_3 < param_1 + 0x3e) {
      *param_1 = *param_1 + param_3;
      param_1[1] = param_1[1] - param_3;
    }
    else {
      param_1[1] = 0;
      *param_1 = (int)(param_1 + 2);
    }
    return (int)(pbVar2 + 8) * 0x10 + (int)param_1 * -0xf;
  }
  pbVar6 = pbVar2;
  if (pbVar2[param_1[1]] != 0) {
    pbVar6 = pbVar2 + param_1[1];
  }
  if (pbVar6 + param_3 < param_1 + 0x3e) {
    do {
      if (*pbVar6 == 0) {
        pbVar3 = pbVar6 + 1;
        uVar5 = 1;
        bVar1 = pbVar6[1];
        while (bVar1 == 0) {
          pbVar3 = pbVar3 + 1;
          uVar5 = uVar5 + 1;
          bVar1 = *pbVar3;
        }
        if (param_3 <= uVar5) {
          if (param_1 + 0x3e <= pbVar6 + param_3) {
            *param_1 = (int)(param_1 + 2);
            goto LAB_00a28eef;
          }
          *param_1 = (int)(pbVar6 + param_3);
          param_1[1] = uVar5 - param_3;
          goto LAB_00a28ef6;
        }
        if (pbVar6 == pbVar2) {
          param_1[1] = uVar5;
        }
        else {
          param_2 = param_2 - uVar5;
          if (param_2 < param_3) {
            return 0;
          }
        }
      }
      else {
        pbVar3 = pbVar6 + *pbVar6;
      }
      pbVar6 = pbVar3;
    } while (pbVar3 + param_3 < param_1 + 0x3e);
  }
  pbVar3 = (byte *)(param_1 + 2);
  pbVar6 = pbVar3;
  if (pbVar3 < pbVar2) {
    while (pbVar6 + param_3 < param_1 + 0x3e) {
      if (*pbVar6 == 0) {
        pbVar4 = pbVar6 + 1;
        uVar5 = 1;
        bVar1 = pbVar6[1];
        while (bVar1 == 0) {
          pbVar4 = pbVar4 + 1;
          uVar5 = uVar5 + 1;
          bVar1 = *pbVar4;
        }
        if (param_3 <= uVar5) {
          if (pbVar6 + param_3 < param_1 + 0x3e) {
            *param_1 = (int)(pbVar6 + param_3);
            param_1[1] = uVar5 - param_3;
          }
          else {
            *param_1 = (int)pbVar3;
LAB_00a28eef:
            param_1[1] = 0;
          }
LAB_00a28ef6:
          *pbVar6 = (byte)param_3;
          return (int)(pbVar6 + 8) * 0x10 + (int)param_1 * -0xf;
        }
        param_2 = param_2 - uVar5;
        if (param_2 < param_3) {
          return 0;
        }
      }
      else {
        pbVar4 = pbVar6 + *pbVar6;
      }
      pbVar6 = pbVar4;
      if (pbVar2 <= pbVar4) {
        return 0;
      }
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a28f20 @ 00a28f20 */
/* ===================================================== */

undefined4 FUN_00a28f20(undefined4 param_1)

{
  int iVar1;
  
  if (DAT_0121420c != (code *)0x0) {
    iVar1 = (*DAT_0121420c)(param_1);
    if (iVar1 != 0) {
      return 1;
    }
  }
  return 0;
}



/* ===================================================== */
/* FUN_00a28f40 @ 00a28f40 */
/* ===================================================== */

undefined4 * FUN_00a28f40(undefined4 *param_1,undefined4 *param_2,uint param_3)

{
  uint uVar1;
  undefined4 *puVar2;
  
  if ((param_2 < param_1) && (param_1 < (undefined4 *)(param_3 + (int)param_2))) {
    param_2 = (undefined4 *)((param_3 - 4) + (int)param_2);
    puVar2 = (undefined4 *)((param_3 - 4) + (int)param_1);
    if (((uint)puVar2 & 3) == 0) {
      uVar1 = param_3 >> 2;
      param_3 = param_3 & 3;
      if (7 < uVar1) {
        for (; uVar1 != 0; uVar1 = uVar1 - 1) {
          *puVar2 = *param_2;
          param_2 = param_2 + -1;
          puVar2 = puVar2 + -1;
        }
        switch(param_3) {
        case 0:
          return param_1;
        case 2:
          goto switchD_00a290f7_caseD_2;
        case 3:
          goto switchD_00a290f7_caseD_3;
        }
        goto switchD_00a290f7_caseD_1;
      }
    }
    else {
      switch(param_3) {
      case 0:
        goto switchD_00a290f7_caseD_0;
      case 1:
        goto switchD_00a290f7_caseD_1;
      case 2:
        goto switchD_00a290f7_caseD_2;
      case 3:
        goto switchD_00a290f7_caseD_3;
      default:
        uVar1 = param_3 - ((uint)puVar2 & 3);
        switch((uint)puVar2 & 3) {
        case 1:
          param_3 = uVar1 & 3;
          *(undefined1 *)((int)puVar2 + 3) = *(undefined1 *)((int)param_2 + 3);
          param_2 = (undefined4 *)((int)param_2 + -1);
          uVar1 = uVar1 >> 2;
          puVar2 = (undefined4 *)((int)puVar2 - 1);
          if (7 < uVar1) {
            for (; uVar1 != 0; uVar1 = uVar1 - 1) {
              *puVar2 = *param_2;
              param_2 = param_2 + -1;
              puVar2 = puVar2 + -1;
            }
            switch(param_3) {
            case 0:
              return param_1;
            case 2:
              goto switchD_00a290f7_caseD_2;
            case 3:
              goto switchD_00a290f7_caseD_3;
            }
            goto switchD_00a290f7_caseD_1;
          }
          break;
        case 2:
          param_3 = uVar1 & 3;
          *(undefined1 *)((int)puVar2 + 3) = *(undefined1 *)((int)param_2 + 3);
          uVar1 = uVar1 >> 2;
          *(undefined1 *)((int)puVar2 + 2) = *(undefined1 *)((int)param_2 + 2);
          param_2 = (undefined4 *)((int)param_2 + -2);
          puVar2 = (undefined4 *)((int)puVar2 - 2);
          if (7 < uVar1) {
            for (; uVar1 != 0; uVar1 = uVar1 - 1) {
              *puVar2 = *param_2;
              param_2 = param_2 + -1;
              puVar2 = puVar2 + -1;
            }
            switch(param_3) {
            case 0:
              return param_1;
            case 2:
              goto switchD_00a290f7_caseD_2;
            case 3:
              goto switchD_00a290f7_caseD_3;
            }
            goto switchD_00a290f7_caseD_1;
          }
          break;
        case 3:
          param_3 = uVar1 & 3;
          *(undefined1 *)((int)puVar2 + 3) = *(undefined1 *)((int)param_2 + 3);
          *(undefined1 *)((int)puVar2 + 2) = *(undefined1 *)((int)param_2 + 2);
          uVar1 = uVar1 >> 2;
          *(undefined1 *)((int)puVar2 + 1) = *(undefined1 *)((int)param_2 + 1);
          param_2 = (undefined4 *)((int)param_2 + -3);
          puVar2 = (undefined4 *)((int)puVar2 - 3);
          if (7 < uVar1) {
            for (; uVar1 != 0; uVar1 = uVar1 - 1) {
              *puVar2 = *param_2;
              param_2 = param_2 + -1;
              puVar2 = puVar2 + -1;
            }
            switch(param_3) {
            case 0:
              return param_1;
            case 2:
              goto switchD_00a290f7_caseD_2;
            case 3:
              goto switchD_00a290f7_caseD_3;
            }
            goto switchD_00a290f7_caseD_1;
          }
        }
      }
    }
    switch(uVar1) {
    case 7:
      puVar2[7 - uVar1] = param_2[7 - uVar1];
    case 6:
      puVar2[6 - uVar1] = param_2[6 - uVar1];
    case 5:
      puVar2[5 - uVar1] = param_2[5 - uVar1];
    case 4:
      puVar2[4 - uVar1] = param_2[4 - uVar1];
    case 3:
      puVar2[3 - uVar1] = param_2[3 - uVar1];
    case 2:
      puVar2[2 - uVar1] = param_2[2 - uVar1];
    case 1:
      puVar2[1 - uVar1] = param_2[1 - uVar1];
      param_2 = param_2 + -uVar1;
      puVar2 = puVar2 + -uVar1;
    }
    switch(param_3) {
    case 1:
switchD_00a290f7_caseD_1:
      *(undefined1 *)((int)puVar2 + 3) = *(undefined1 *)((int)param_2 + 3);
      return param_1;
    case 2:
switchD_00a290f7_caseD_2:
      *(undefined1 *)((int)puVar2 + 3) = *(undefined1 *)((int)param_2 + 3);
      *(undefined1 *)((int)puVar2 + 2) = *(undefined1 *)((int)param_2 + 2);
      return param_1;
    case 3:
switchD_00a290f7_caseD_3:
      *(undefined1 *)((int)puVar2 + 3) = *(undefined1 *)((int)param_2 + 3);
      *(undefined1 *)((int)puVar2 + 2) = *(undefined1 *)((int)param_2 + 2);
      *(undefined1 *)((int)puVar2 + 1) = *(undefined1 *)((int)param_2 + 1);
      return param_1;
    }
switchD_00a290f7_caseD_0:
    return param_1;
  }
  puVar2 = param_1;
  if (((uint)param_1 & 3) == 0) {
    uVar1 = param_3 >> 2;
    param_3 = param_3 & 3;
    if (7 < uVar1) {
      for (; uVar1 != 0; uVar1 = uVar1 - 1) {
        *puVar2 = *param_2;
        param_2 = param_2 + 1;
        puVar2 = puVar2 + 1;
      }
      switch(param_3) {
      case 0:
        return param_1;
      case 2:
        goto switchD_00a28f75_caseD_2;
      case 3:
        goto switchD_00a28f75_caseD_3;
      }
      goto switchD_00a28f75_caseD_1;
    }
  }
  else {
    switch(param_3) {
    case 0:
      goto switchD_00a28f75_caseD_0;
    case 1:
      goto switchD_00a28f75_caseD_1;
    case 2:
      goto switchD_00a28f75_caseD_2;
    case 3:
      goto switchD_00a28f75_caseD_3;
    default:
      uVar1 = (param_3 - 4) + ((uint)param_1 & 3);
      switch((uint)param_1 & 3) {
      case 1:
        param_3 = uVar1 & 3;
        *(undefined1 *)param_1 = *(undefined1 *)param_2;
        *(undefined1 *)((int)param_1 + 1) = *(undefined1 *)((int)param_2 + 1);
        uVar1 = uVar1 >> 2;
        *(undefined1 *)((int)param_1 + 2) = *(undefined1 *)((int)param_2 + 2);
        param_2 = (undefined4 *)((int)param_2 + 3);
        puVar2 = (undefined4 *)((int)param_1 + 3);
        if (7 < uVar1) {
          for (; uVar1 != 0; uVar1 = uVar1 - 1) {
            *puVar2 = *param_2;
            param_2 = param_2 + 1;
            puVar2 = puVar2 + 1;
          }
          switch(param_3) {
          case 0:
            return param_1;
          case 2:
            goto switchD_00a28f75_caseD_2;
          case 3:
            goto switchD_00a28f75_caseD_3;
          }
          goto switchD_00a28f75_caseD_1;
        }
        break;
      case 2:
        param_3 = uVar1 & 3;
        *(undefined1 *)param_1 = *(undefined1 *)param_2;
        uVar1 = uVar1 >> 2;
        *(undefined1 *)((int)param_1 + 1) = *(undefined1 *)((int)param_2 + 1);
        param_2 = (undefined4 *)((int)param_2 + 2);
        puVar2 = (undefined4 *)((int)param_1 + 2);
        if (7 < uVar1) {
          for (; uVar1 != 0; uVar1 = uVar1 - 1) {
            *puVar2 = *param_2;
            param_2 = param_2 + 1;
            puVar2 = puVar2 + 1;
          }
          switch(param_3) {
          case 0:
            return param_1;
          case 2:
            goto switchD_00a28f75_caseD_2;
          case 3:
            goto switchD_00a28f75_caseD_3;
          }
          goto switchD_00a28f75_caseD_1;
        }
        break;
      case 3:
        param_3 = uVar1 & 3;
        *(undefined1 *)param_1 = *(undefined1 *)param_2;
        param_2 = (undefined4 *)((int)param_2 + 1);
        uVar1 = uVar1 >> 2;
        puVar2 = (undefined4 *)((int)param_1 + 1);
        if (7 < uVar1) {
          for (; uVar1 != 0; uVar1 = uVar1 - 1) {
            *puVar2 = *param_2;
            param_2 = param_2 + 1;
            puVar2 = puVar2 + 1;
          }
          switch(param_3) {
          case 0:
            return param_1;
          case 2:
            goto switchD_00a28f75_caseD_2;
          case 3:
            goto switchD_00a28f75_caseD_3;
          }
          goto switchD_00a28f75_caseD_1;
        }
      }
    }
  }
  switch(uVar1) {
  case 7:
    puVar2[uVar1 - 7] = param_2[uVar1 - 7];
  case 6:
    puVar2[uVar1 - 6] = param_2[uVar1 - 6];
  case 5:
    puVar2[uVar1 - 5] = param_2[uVar1 - 5];
  case 4:
    puVar2[uVar1 - 4] = param_2[uVar1 - 4];
  case 3:
    puVar2[uVar1 - 3] = param_2[uVar1 - 3];
  case 2:
    puVar2[uVar1 - 2] = param_2[uVar1 - 2];
  case 1:
    puVar2[uVar1 - 1] = param_2[uVar1 - 1];
    param_2 = param_2 + uVar1;
    puVar2 = puVar2 + uVar1;
  }
  switch(param_3) {
  case 1:
switchD_00a28f75_caseD_1:
    *(undefined1 *)puVar2 = *(undefined1 *)param_2;
    return param_1;
  case 2:
switchD_00a28f75_caseD_2:
    *(undefined1 *)puVar2 = *(undefined1 *)param_2;
    *(undefined1 *)((int)puVar2 + 1) = *(undefined1 *)((int)param_2 + 1);
    return param_1;
  case 3:
switchD_00a28f75_caseD_3:
    *(undefined1 *)puVar2 = *(undefined1 *)param_2;
    *(undefined1 *)((int)puVar2 + 1) = *(undefined1 *)((int)param_2 + 1);
    *(undefined1 *)((int)puVar2 + 2) = *(undefined1 *)((int)param_2 + 2);
    return param_1;
  }
switchD_00a28f75_caseD_0:
  return param_1;
}



/* ===================================================== */
/* FUN_00a29280 @ 00a29280 */
/* ===================================================== */

undefined4 * FUN_00a29280(int param_1,int param_2)

{
  int iVar1;
  uint uVar2;
  undefined4 *puVar3;
  uint dwBytes;
  undefined4 *puVar4;
  
  dwBytes = param_2 * param_1;
  if (dwBytes < 0xffffffe1) {
    if (dwBytes == 0) {
      dwBytes = 0x10;
    }
    else {
      dwBytes = dwBytes + 0xf & 0xfffffff0;
    }
  }
  do {
    puVar3 = (undefined4 *)0x0;
    if (dwBytes < 0xffffffe1) {
      if (DAT_00a2e8dc < dwBytes) {
LAB_00a292e0:
        if (puVar3 != (undefined4 *)0x0) {
          return puVar3;
        }
      }
      else {
        puVar3 = (undefined4 *)FUN_00a28b60(dwBytes >> 4);
        if (puVar3 != (undefined4 *)0x0) {
          puVar4 = puVar3;
          for (uVar2 = dwBytes >> 2; uVar2 != 0; uVar2 = uVar2 - 1) {
            *puVar4 = 0;
            puVar4 = puVar4 + 1;
          }
          for (uVar2 = dwBytes & 3; uVar2 != 0; uVar2 = uVar2 - 1) {
            *(undefined1 *)puVar4 = 0;
            puVar4 = (undefined4 *)((int)puVar4 + 1);
          }
          goto LAB_00a292e0;
        }
      }
      puVar3 = HeapAlloc(DAT_01214210,8,dwBytes);
    }
    if ((puVar3 != (undefined4 *)0x0) || (DAT_01214208 == 0)) {
      return puVar3;
    }
    iVar1 = FUN_00a28f20(dwBytes);
    if (iVar1 == 0) {
      return (undefined4 *)0x0;
    }
  } while( true );
}


