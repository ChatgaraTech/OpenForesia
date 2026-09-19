Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004855f0  section .text  offset fichier 0x849f0  144 octets

  004855f0  56                    push      esi
  004855f1  8b742408              mov       esi,[esp+8]
  004855f5  57                    push      edi
  004855f6  8b466c                mov       eax,[esi+6Ch]
  004855f9  50                    push      eax
  004855fa  894674                mov       [esi+74h],eax
  004855fd  e8be350b00            call      00538BC0h
  00485602  8b7e20                mov       edi,[esi+20h]
  00485605  24fe                  and       al,0FEh
  00485607  c1e00b                shl       eax,0Bh
  0048560a  03f8                  add       edi,eax
  0048560c  83c404                add       esp,4
  0048560f  0fbf4632              movsx     eax,word [esi+32h]
  00485613  897e20                mov       [esi+20h],edi
  00485616  6a10                  push      10h
  00485618  8d4e50                lea       ecx,[esi+50h]
  0048561b  33ff                  xor       edi,edi
  0048561d  50                    push      eax
  0048561e  51                    push      ecx
  0048561f  c646042a              mov       byte [esi+4],2Ah
  00485623  66c746360400          mov       word [esi+36h],4
  00485629  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00485630  897e28                mov       [esi+28h],edi
  00485633  66897e2c              mov       [esi+2Ch],di
  00485637  6689be9a000000        mov       [esi+9Ah],di
  0048563e  e8cdd40a00            call      00532B10h
  00485643  897e54                mov       [esi+54h],edi
  00485646  8b15cc975e00          mov       edx,[5E97CCh]
  0048564c  83c40c                add       esp,0Ch
  0048564f  8b82d0000000          mov       eax,[edx+0D0h]
  00485655  33d2                  xor       edx,edx
  00485657  6a02                  push      2
  00485659  6a01                  push      1
  0048565b  8b08                  mov       ecx,[eax]
  0048565d  8a5101                mov       dl,[ecx+1]
  00485660  81c2c0000000          add       edx,0C0h
  00485666  52                    push      edx
  00485667  56                    push      esi
  00485668  e87354ffff            call      0047AAE0h
  0048566d  83c410                add       esp,10h
  00485670  893e                  mov       [esi],edi
  00485672  5f                    pop       edi
  00485673  5e                    pop       esi
  00485674  c3                    ret
  00485675  90                    nop
  00485676  90                    nop
  00485677  90                    nop
  00485678  90                    nop
  00485679  90                    nop
  0048567a  90                    nop
  0048567b  90                    nop
  0048567c  90                    nop
  0048567d  90                    nop
  0048567e  90                    nop
  0048567f  90                    nop
