
undefined4 __fastcall FUN_00543230(int param_1)

{
  HRESULT HVar1;
  
  HVar1 = CoCreateInstance((IID *)&DAT_0054e8a8,(LPUNKNOWN)0x0,3,(IID *)&DAT_0054e8b8,
                           (LPVOID *)(param_1 + 4));
  if ((-1 < HVar1) && (*(LPVOID *)(param_1 + 4) != (LPVOID)0x0)) {
    return 1;
  }
  return 0;
}

