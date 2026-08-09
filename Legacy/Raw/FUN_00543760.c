
int __thiscall FUN_00543760(int param_1,int *param_2)

{
  int iVar1;
  int *unaff_ESI;
  int *unaff_EDI;
  int **ppiVar2;
  int *piVar3;
  int **ppiVar4;
  int *piVar5;
  int *piStack_34;
  int iStack_30;
  undefined1 *puStack_2c;
  int **ppiStack_28;
  int *piStack_24;
  undefined1 *puStack_20;
  int *piStack_14;
  undefined1 auStack_10 [12];
  undefined1 auStack_4 [4];
  
  if (param_2 == (int *)0x0) {
    return -0x7ff8ffa9;
  }
  puStack_20 = auStack_10;
  piStack_24 = param_2;
  ppiStack_28 = (int **)0x543787;
  iVar1 = (**(code **)(*param_2 + 0x28))();
  if (-1 < iVar1) {
    ppiStack_28 = &piStack_14;
    puStack_2c = auStack_4;
    iStack_30 = 1;
    piStack_34 = unaff_ESI;
    iVar1 = (**(code **)(*unaff_ESI + 0xc))();
    if (iVar1 == 0) {
      while (piStack_24 == (int *)0x1) {
        piVar5 = piStack_14;
        (**(code **)(*piStack_14 + 0x24))(piStack_14,&puStack_20);
        if (ppiStack_28 == (int **)0x0) {
          ppiVar2 = &piStack_34;
          iVar1 = (**(code **)*unaff_EDI)(unaff_EDI,&DAT_0054e8f8);
          if (iVar1 < 0) {
            return iVar1;
          }
          piVar3 = &iStack_30;
          ppiVar4 = ppiVar2;
          iVar1 = (*(code *)(*ppiVar2)[8])(ppiVar2,0);
          *(int **)(param_1 + 0x118) = piVar5;
          (**(code **)(*piVar3 + 0x2c))(piVar3,0);
          if (iVar1 < 0) {
            return iVar1;
          }
          (*(code *)(*ppiVar2)[2])(ppiVar2);
          (*(code *)(*ppiVar4)[2])(ppiVar4);
          break;
        }
        (**(code **)(*unaff_EDI + 8))(unaff_EDI);
        iVar1 = (**(code **)(*piStack_34 + 0xc))(piStack_34,1,&puStack_20,&iStack_30);
        if (iVar1 != 0) {
          (*(code *)(*ppiStack_28)[2])(ppiStack_28);
          return 0;
        }
      }
    }
    (*(code *)(*ppiStack_28)[2])(ppiStack_28);
    iVar1 = 0;
  }
  return iVar1;
}

