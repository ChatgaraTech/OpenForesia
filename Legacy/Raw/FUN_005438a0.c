
void __thiscall FUN_005438a0(int param_1,int param_2,int param_3,int param_4,int param_5)

{
  tagRECT tStack_10;
  
  SetRect(&tStack_10,param_2,param_3,param_4 + param_2,param_5 + param_3);
  (**(code **)(**(int **)(param_1 + 0x11c) + 0x1c))(*(int **)(param_1 + 0x11c),0,&tStack_10);
  return;
}

