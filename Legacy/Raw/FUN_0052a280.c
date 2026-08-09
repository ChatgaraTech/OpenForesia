/*
 * Function: FUN_0052a280
 * Entry:    0052a280
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_0052a280(void)

{
  char cVar1;
  int iVar2;
  undefined *puVar3;
  undefined4 uVar4;
  undefined *puStack_2c;
  undefined *puStack_28;
  undefined4 uStack_24;
  int iStack_20;
  undefined4 *puStack_14;
  undefined4 *puStack_c;
  undefined4 *puStack_8;
  int iStack_4;
  
  iStack_20 = 0x52a28a;
  FUN_00547140();
  DAT_005e9960 = DAT_005e9960 + 1;
  iStack_20 = DAT_005e9960 * 0x200;
  uStack_24 = 0x52a29e;
  iVar2 = Ordinal_451();
  cVar1 = (char)(iVar2 >> 6) + -0x80;
  uStack_24 = 0;
  _DAT_005e8330 = 0xffffffff;
  _DAT_005e832c = 0;
  DAT_005e9814 = (uint)CONCAT21(CONCAT11(cVar1,cVar1),cVar1);
  puStack_28 = (undefined *)0x52a2d5;
  Ordinal_667();
  puVar3 = &DAT_005e9880;
  if (DAT_005e9300 == &DAT_005e9880) {
    puVar3 = &DAT_005e98f0;
  }
  puStack_28 = puVar3 + 0x5c;
  puStack_2c = (undefined *)0x52a2f7;
  DAT_005e9300 = puVar3;
  Ordinal_657();
  puStack_2c = DAT_005e9300;
  Ordinal_660();
  FUN_00545140();
  Ordinal_701(*puStack_8);
  Ordinal_132(iStack_4);
  if (iStack_4 != 0) {
    FUN_00512520(&DAT_00604a20 +
                 (uint)(byte)(&DAT_005e9188)
                             [(uint)(byte)(&DAT_005e93c8)[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4
                );
    FUN_005048f0();
    FUN_00504a30();
    FUN_005357a0();
  }
  FUN_0052a1e0(puStack_14,puStack_8,puStack_c);
  if (DAT_00571a3c != 0) {
    puStack_2c = &DAT_00566c28;
    puStack_28 = (undefined *)0x1000000;
    uStack_24 = 0x640010;
    uVar4 = FUN_0051c120(*puStack_14,*puStack_c,&puStack_2c,
                         s_Veuillez_connecter_la_manette___00568510,0,0,0xffffffff);
    *puStack_c = uVar4;
  }
  return;
}

