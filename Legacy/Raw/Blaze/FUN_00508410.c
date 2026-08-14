/*
 * Function: FUN_00508410
 * Entry:    00508410
 * Body:     [[00508410, 00508854]]
 */


/* WARNING: Globals starting with '_' overlap smaller symbols at the same address */

void FUN_00508410(int param_1)

{
  ushort uVar1;
  short sVar2;
  undefined4 uVar3;
  uint uVar4;
  int iVar5;
  undefined4 unaff_retaddr;
  undefined4 uStack_8;
  
  if (DAT_00562a48 != 0) {
    DAT_00562a48 = 0;
    DAT_00578df8 = 0;
    DAT_00578dfc = 0;
    DAT_0057351c = 0x380028;
    DAT_00573520._0_2_ = 0xe0;
    DAT_00573520._2_2_ = 0x48;
    DAT_00573538 = 0x1070c24;
    DAT_0057353c = 0x1000000;
    DAT_00562a50 = 2;
  }
  uVar1 = (*(byte *)(DAT_00573338 + 0x150) - 8) * 0x100 - _DAT_0060492a & 0xfff;
  if (uVar1 < 0x800) {
    sVar2 = (short)uVar1 >> 2;
  }
  else {
    sVar2 = -(short)(0x1000 - (short)uVar1 >> 2);
  }
  _DAT_0060492a = _DAT_0060492a + sVar2;
  DAT_005e93a0 = DAT_005e93a0 + (short)(0x60 - DAT_005e93a0 >> 2);
  DAT_005e93a2 = DAT_005e93a2 + (short)(0x60 - DAT_005e93a2 >> 2);
  DAT_005e93a4 = DAT_005e93a4 + (short)(0x60 - DAT_005e93a4 >> 2);
  FUN_0051c300(&DAT_00604928,&DAT_006008e0);
  Ordinal_376((int)DAT_005e93a0,(int)DAT_005e93a2,(int)DAT_005e93a4);
  DAT_00572b7a = DAT_00572b7a +
                 (short)((int)((uint)*(byte *)(DAT_00573338 + 0x150) * -0x100 - (int)DAT_00572b7a)
                        >> 2);
  if (DAT_00562a4c == 0) {
    if (DAT_00578df0 == 0x5a) {
      uVar1 = *(ushort *)(param_1 + 2);
      if ((uVar1 & 0x4000) == 0) {
        if ((uVar1 & 0x8000) == 0) {
          uVar4 = DAT_00578dfc << 8 | DAT_00578df8;
          if (((*(byte *)(param_1 + 6) & 0x10) != 0) && (DAT_00578dfc != 0)) {
            DAT_00578dfc = DAT_00578dfc + -1;
          }
          if (((*(byte *)(param_1 + 6) & 0x40) != 0) && (DAT_00578dfc < 3)) {
            DAT_00578dfc = DAT_00578dfc + 1;
          }
          if ((*(byte *)(param_1 + 6) & 0x80) != 0) {
            if (DAT_00578df8 == 0) {
              if (DAT_00578dfc != 0) {
                DAT_00578dfc = DAT_00578dfc + -1;
                DAT_00578df8 = 3;
              }
            }
            else {
              DAT_00578df8 = DAT_00578df8 - 1;
            }
          }
          if ((*(byte *)(param_1 + 6) & 0x20) != 0) {
            if ((int)DAT_00578df8 < 3) {
              DAT_00578df8 = DAT_00578df8 + 1;
            }
            else if (DAT_00578dfc < 3) {
              DAT_00578dfc = DAT_00578dfc + 1;
              DAT_00578df8 = 0;
            }
          }
          if (uVar4 != (DAT_00578dfc << 8 | DAT_00578df8)) {
            FUN_0053db60();
          }
        }
        else {
          *(ushort *)(param_1 + 2) = uVar1 & 0x7fff;
          FUN_0053db80();
          DAT_00578df0 = DAT_00578df0 + 1;
        }
      }
      else {
        FUN_0053dba0();
        DAT_00578df0 = 0x13;
        DAT_00578de8 = DAT_00578de8 + -1;
      }
    }
    else if (DAT_00578df0 == 0x5b) {
      if ((DAT_00560fef & 2) == 0) {
        *(ushort *)(param_1 + 2) = *(ushort *)(param_1 + 2) & 0x7fff;
        DAT_00578df0 = DAT_00578df0 + 1;
      }
      else if ((*(byte *)(param_1 + 3) & 0x40) != 0) {
        DAT_0056075b = 0;
        DAT_00560fef = DAT_00560fef & 0xfd;
        FUN_0053dba0();
        DAT_00578df0 = DAT_00578df0 + -1;
      }
    }
    else if (DAT_00578df0 == 0x5c) {
      if ((DAT_00560fef & 4) == 0) {
        if (DAT_005e909f == '\0') {
          DAT_00578de8 = DAT_00578de8 + 1;
          DAT_00578df0 = 0x5f;
          *(byte *)(DAT_00573338 + 0x152) = (char)DAT_00578dfc << 2 | (byte)DAT_00578df8;
        }
        else {
          DAT_00578df0 = 0x5a;
        }
      }
      else if ((*(byte *)(param_1 + 3) & 0x40) != 0) {
        DAT_00560763 = 0;
        DAT_00560fef = DAT_00560fef & 0xfb;
        FUN_0053dba0();
        DAT_00578df0 = 0x5a;
      }
    }
  }
  FUN_0051be20(uStack_8,&stack0x00000000);
  uVar3 = FUN_00521df0(uStack_8,unaff_retaddr,&stack0xffffffe0,0,0);
  uVar3 = FUN_0051f9c0(uStack_8,uVar3,&stack0xffffffe0);
  FUN_0051bdb0(uStack_8,&stack0x00000000);
  FUN_00544350(&DAT_005606f8,0,&DAT_00573518,0);
  uVar3 = FUN_005443a0(uVar3,&DAT_00573518);
  iVar5 = 0x4d8;
  do {
    FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + iVar5),0,&DAT_00573518,0);
    uVar3 = FUN_005443a0(uVar3,&DAT_00573518);
    iVar5 = iVar5 + 4;
  } while (iVar5 < 0x4e8);
  FUN_00545340(uStack_8,uVar3,&DAT_00573518);
  return;
}


