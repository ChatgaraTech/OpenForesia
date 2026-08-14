/*
 * Function: FUN_00512520
 * Entry:    00512520
 * Body:     [[00512520, 005126bf]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

undefined4 FUN_00512520(undefined4 param_1)

{
  int iVar1;
  short sVar2;
  undefined4 unaff_EDI;
  undefined1 *puVar3;
  undefined1 auStack_4 [4];
  
  DAT_00579270 = param_1;
  if (DAT_005de554 == 0) {
    sVar2 = GetAsyncKeyState(0x72);
    iVar1 = DAT_005d9de0;
    if (sVar2 < 0) {
      ReInitializeAllTextures();
      puVar3 = auStack_4;
      GetAbsShift(puVar3,&param_1);
      SetAbsShift(0,0);
      if (DAT_005de160 < 7) {
        DAT_00564a38 = 1;
      }
      FUN_00512320();
      SetAbsShift(puVar3,unaff_EDI);
      FUN_0050f300();
      if (iVar1 != DAT_005d9de0) {
        CreateTranslationTables((float)DAT_005d9de0 * _DAT_0054d000);
      }
      return 1;
    }
  }
  sVar2 = GetAsyncKeyState(0x7a);
  if (sVar2 == 0) {
    sVar2 = GetAsyncKeyState(0x7b);
    if (sVar2 == 0) {
      sVar2 = GetAsyncKeyState(0x78);
      if ((sVar2 != 0) && (6 < DAT_005de160)) {
        DAT_00564a38 = (uint)(DAT_00564a38 == 0);
        FUN_0050f300();
        do {
          Sleep(100);
          sVar2 = GetAsyncKeyState(0x78);
        } while (sVar2 < 0);
      }
    }
    else if (DAT_005de160 < *(int *)(&DAT_00562e00 + DAT_00566cc4 * 4)) {
      DAT_005de160 = DAT_005de160 + 1;
      SetShift((0xb - DAT_005de160) * 0x10,(0xb - DAT_005de160) * 0xc,DAT_005de160,0);
      FUN_0050f300();
      do {
        Sleep(100);
        sVar2 = GetAsyncKeyState(0x7b);
      } while (sVar2 < 0);
      return 0;
    }
  }
  else if (DAT_005de160 != 0) {
    DAT_005de160 = DAT_005de160 + -1;
    SetShift((0xb - DAT_005de160) * 0x10,(0xb - DAT_005de160) * 0xc,DAT_005de160,0);
    if (DAT_005de160 < 7) {
      DAT_00564a38 = 1;
    }
    FUN_0050f300();
    do {
      Sleep(100);
      sVar2 = GetAsyncKeyState(0x7a);
    } while (sVar2 < 0);
    return 0;
  }
  return 0;
}


