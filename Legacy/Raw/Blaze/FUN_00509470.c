/*
 * Function: FUN_00509470
 * Entry:    00509470
 * Body:     [[00509470, 005098d7]]
 */


void FUN_00509470(void)

{
  char cVar1;
  short sVar2;
  undefined2 *puVar3;
  int iVar4;
  char *pcVar5;
  char cVar6;
  int iVar7;
  int iVar8;
  short *psVar9;
  undefined4 unaff_retaddr;
  int in_stack_00000010;
  undefined4 uStack_8;
  
  iVar8 = 0;
  iVar7 = DAT_00578e04;
  if (DAT_00562a48 != 0) {
    DAT_00562a48 = 0;
    DAT_005e906a = 0xf;
    DAT_005e909f = '\0';
    DAT_00578e04 = 0;
    puVar3 = &DAT_0057336a;
    iVar7 = 3;
    do {
      puVar3[-1] = 0x1000;
      *puVar3 = 0x1000;
      *(undefined4 *)(puVar3 + 7) = 0x1000000;
      *(undefined1 *)(puVar3 + 0xb) = 0;
      puVar3[-0xd] = 0;
      puVar3 = puVar3 + 0x4e;
      iVar7 = iVar7 + -1;
    } while (iVar7 != 0);
    iVar4 = 0;
    iVar7 = **(int **)(DAT_005e97c4 + 0x74) + (*(byte *)(DAT_00573338 + 0x150) & 7) * 8;
    DAT_00578de4 = iVar7;
    do {
      *(undefined1 *)((int)&DAT_005e9060 + iVar4) = *(undefined1 *)(iVar7 + iVar4);
      iVar4 = iVar4 + 1;
    } while (iVar4 < 7);
    DAT_00562a50 = 3;
    iVar7 = 0;
  }
  if (DAT_00562a4c != 0) goto switchD_0050953b_default;
  switch(DAT_00578df0) {
  case 0x39:
    if (((*(ushort *)(in_stack_00000010 + 2) & 0x4000) == 0) || (iVar7 != 0)) {
      if (((*(ushort *)(in_stack_00000010 + 2) & 0x8000) != 0) && (iVar7 < 3)) {
        FUN_0053db80();
        (&DAT_00573350)[DAT_00578e04 * 0x4e] = 0;
        iVar7 = DAT_00578e04 + 1;
        DAT_00578e04 = iVar7;
      }
      if (0 < iVar7) {
        psVar9 = &DAT_00573350;
        do {
          sVar2 = Ordinal_450((int)*psVar9);
          psVar9[0xc] = sVar2;
          if (*psVar9 == 0x400) {
            iVar7 = FUN_00547140();
            *(char *)(psVar9 + 0x18) = (char)(iVar7 % 6) + '\x01';
          }
          if (*psVar9 < 0x800) {
            *psVar9 = *psVar9 + 0x100;
          }
          iVar8 = iVar8 + 1;
          psVar9 = psVar9 + 0x4e;
          iVar7 = DAT_00578e04;
        } while (iVar8 < DAT_00578e04);
      }
      if (iVar7 == 3) {
        iVar7 = 0;
        cVar6 = '\0';
        pcVar5 = &DAT_00573380;
        do {
          if (*(short *)(pcVar5 + -0x30) != 0x800) break;
          cVar1 = *pcVar5;
          pcVar5 = pcVar5 + 0x9c;
          cVar6 = cVar6 + cVar1;
          iVar7 = iVar7 + 1;
        } while (iVar7 < 3);
        if (iVar7 == 3) {
          DAT_005e906a = DAT_005e906a + cVar6;
          if (((DAT_00573380 == DAT_0057341c) && (DAT_0057341c == DAT_005734b8)) &&
             (DAT_005e906a < 99)) {
            DAT_00578df0 = DAT_00578df0 + 1;
          }
          else {
            if (99 < DAT_005e906a) {
              DAT_005e906a = 99;
            }
            DAT_00578df0 = DAT_00578df0 + 2;
          }
        }
      }
    }
    else {
      FUN_0053dba0();
      DAT_00578df0 = 0x2e;
      DAT_00578de8 = DAT_00578de8 + -1;
    }
    break;
  case 0x3a:
    if ((DAT_00560fef & 2) == 0) {
      iVar7 = 3;
      *(ushort *)(in_stack_00000010 + 2) = *(ushort *)(in_stack_00000010 + 2) & 0x7fff;
      DAT_00578df0 = 0x39;
      DAT_00578e04 = 0;
      puVar3 = &DAT_0057336a;
      do {
        puVar3[-1] = 0x1000;
        *puVar3 = 0x1000;
        *(undefined4 *)(puVar3 + 7) = 0x1000000;
        *(undefined1 *)(puVar3 + 0xb) = 0;
        puVar3[-0xd] = 0;
        puVar3 = puVar3 + 0x4e;
        iVar7 = iVar7 + -1;
      } while (iVar7 != 0);
    }
    break;
  case 0x3b:
    if ((DAT_00560fef & 2) == 0) {
      *(ushort *)(in_stack_00000010 + 2) = *(ushort *)(in_stack_00000010 + 2) & 0x7fff;
      DAT_00578df0 = DAT_00578df0 + 1;
      break;
    }
    if ((*(byte *)(in_stack_00000010 + 3) & 0x40) == 0) break;
    DAT_0056075b = 0;
    DAT_00560fef = DAT_00560fef & 0xfd;
    goto LAB_005098ba;
  case 0x3c:
    if ((DAT_00560fef & 4) == 0) {
      if (DAT_005e909f == '\0') {
        DAT_00578df0 = 0x3e;
        DAT_00578de8 = DAT_00578de8 + 1;
      }
      else {
        DAT_00578df0 = 0x39;
        DAT_00562a48 = 1;
      }
      break;
    }
    if ((*(byte *)(in_stack_00000010 + 3) & 0x40) == 0) break;
    DAT_00560763 = 0;
    DAT_00560fef = DAT_00560fef & 0xfb;
LAB_005098ba:
    FUN_0053dba0();
    DAT_00578df0 = 0x39;
    DAT_00562a48 = 1;
  }
switchD_0050953b_default:
  DAT_005e93a0 = DAT_005e93a0 + (short)(0x60 - DAT_005e93a0 >> 2);
  DAT_005e93a2 = DAT_005e93a2 + (short)(0x60 - DAT_005e93a2 >> 2);
  DAT_005e93a4 = DAT_005e93a4 + (short)(0x60 - DAT_005e93a4 >> 2);
  Ordinal_376((int)DAT_005e93a0,(int)DAT_005e93a2,(int)DAT_005e93a4);
  iVar7 = 0;
  do {
    Ordinal_451(iVar7 + -400);
    unaff_retaddr = FUN_00521f20(uStack_8,unaff_retaddr,&stack0xffffffe0,0,0);
    iVar7 = iVar7 + 400;
  } while (iVar7 < 0x4b0);
  FUN_00544350(*(undefined4 *)(*(int *)(DAT_005e97c4 + 0x70) + 0xf4),0,&DAT_00562180,0);
  FUN_005443a0(unaff_retaddr,&DAT_00562180);
  return;
}


