
void FUN_00513660(void)

{
  DWORD DVar1;
  int iVar2;
  undefined1 *puVar3;
  undefined4 uVar4;
  undefined1 auStack_8 [4];
  undefined1 auStack_4 [4];
  
  while( true ) {
    DVar1 = WaitForMultipleObjects(9,(HANDLE *)&DAT_005db838,0,0xffffffff);
    if (DVar1 == 8) break;
    if ((((DAT_005db89c == 0) || (DAT_005db89c = 0, DVar1 != 0)) && (-1 < (int)DVar1)) &&
       ((int)DVar1 < 8)) {
      if (DVar1 == 0) {
        iVar2 = 7;
      }
      else {
        iVar2 = DVar1 - 1;
      }
      puVar3 = auStack_4;
      uVar4 = 0;
      (**(code **)(*DAT_005db900 + 0x2c))
                (DAT_005db900,(&DAT_005db8a0)[iVar2 * 2],DAT_005db8a8,auStack_8,puVar3,0,0,0);
      if (DAT_005db860 + 10 < DAT_005db750) {
        FUN_00531bb0(DAT_005db8f4,
                     DAT_005db860 * (uint)*(ushort *)(DAT_005db8f4 + 0xc) + DAT_005db8f8,
                     &DAT_005db820,puVar3,(uint)*(ushort *)(DAT_005db8f4 + 0xc) * 10);
        DAT_005db860 = DAT_005db860 + 10;
      }
      else {
        iVar2 = DAT_005db750 - DAT_005db860;
        FUN_00531bb0(DAT_005db8f4,
                     DAT_005db860 * (uint)*(ushort *)(DAT_005db8f4 + 0xc) + DAT_005db8f8,
                     &DAT_005db820,puVar3,iVar2 * (uint)*(ushort *)(DAT_005db8f4 + 0xc));
        DAT_005db860 = 10 - iVar2;
        FUN_00531bb0(DAT_005db8f4,DAT_005db8f8,&DAT_005db820,puVar3,
                     (uint)*(ushort *)(DAT_005db8f4 + 0xc) * DAT_005db860);
      }
      (**(code **)(*DAT_005db900 + 0x4c))(DAT_005db900,puVar3,uVar4,0,0);
    }
  }
                    /* WARNING: Subroutine does not return */
  ExitThread(0);
}

