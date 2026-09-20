  004384f0  53                    push      ebx
  004384f1  56                    push      esi
  004384f2  8b74240c              mov       esi,[esp+0Ch]
  004384f6  57                    push      edi
  004384f7  33ff                  xor       edi,edi
  004384f9  bb01000000            mov       ebx,1
  004384fe  66397e10              cmp       [esi+10h],di
  00438502  7556                  jne       short 0043855Ah
  00438504  b8401f0000            mov       eax,1F40h
  00438509  c6460408              mov       byte [esi+4],8
  0043850d  66894628              mov       [esi+28h],ax
  00438511  6689462c              mov       [esi+2Ch],ax
  00438515  6689462a              mov       [esi+2Ah],ax
  00438519  b0fa                  mov       al,0FAh
  0043851b  c6460a09              mov       byte [esi+0Ah],9
  0043851f  c6460b00              mov       byte [esi+0Bh],0
  00438523  66895e10              mov       [esi+10h],bx
  00438527  66c74634000c          mov       word [esi+34h],0C00h
  0043852d  66c746320004          mov       word [esi+32h],400h
  00438533  884638                mov       [esi+38h],al
  00438536  884639                mov       [esi+39h],al
  00438539  88463a                mov       [esi+3Ah],al
  0043853c  e8ffeb1000            call      00547140h
  00438541  99                    cdq
  00438542  b91e000000            mov       ecx,1Eh
  00438547  897e5c                mov       [esi+5Ch],edi
  0043854a  f7f9                  idiv      ecx
  0043854c  6689be9a000000        mov       [esi+9Ah],di
  00438553  66899698000000        mov       [esi+98h],dx
  0043855a  66ff869a000000        inc       word [esi+9Ah]
  00438561  bf02000000            mov       edi,2
  00438566  66397e10              cmp       [esi+10h],di
  0043856a  7523                  jne       short 0043858Fh
  0043856c  837e5c14              cmp       dword [esi+5Ch],14h
  00438570  7e17                  jle       short 00438589h
  00438572  b0aa                  mov       al,0AAh
  00438574  881d27e05d00          mov       [5DE027h],bl
  0043857a  a224e05d00            mov       [5DE024h],al
  0043857f  a225e05d00            mov       [5DE025h],al
  00438584  a226e05d00            mov       [5DE026h],al
  00438589  66c746100300          mov       word [esi+10h],3
  0043858f  66395e10              cmp       [esi+10h],bx
  00438593  0f85ba000000          jne       near 00438653h
  00438599  e8a2eb1000            call      00547140h
  0043859e  99                    cdq
  0043859f  33c2                  xor       eax,edx
  004385a1  0fbf4e14              movsx     ecx,word [esi+14h]
  004385a5  2bc2                  sub       eax,edx
  004385a7  83e003                and       eax,3
  004385aa  33c2                  xor       eax,edx
  004385ac  2bc2                  sub       eax,edx
  004385ae  0408                  add       al,8
  004385b0  884604                mov       [esi+4],al
  004385b3  8bc1                  mov       eax,ecx
  004385b5  99                    cdq
  004385b6  83e207                and       edx,7
  004385b9  03c2                  add       eax,edx
  004385bb  c1f803                sar       eax,3
  004385be  0fbfc0                movsx     eax,ax
  004385c1  8d14c500000000        lea       edx,[eax*8]
  004385c8  8a80e0965e00          mov       al,[eax+5E96E0h]
  004385ce  2bca                  sub       ecx,edx
  004385d0  d3e3                  shl       ebx,cl
  004385d2  84d8                  test      al,bl
  004385d4  7438                  je        short 0043860Eh
  004385d6  6683be9a0000000a      cmp       word [esi+9Ah],0Ah
  004385de  66897e10              mov       [esi+10h],di
  004385e2  7d08                  jge       short 004385ECh
  004385e4  8126ffffff7f          and       dword [esi],7FFFFFFFh
  004385ea  eb67                  jmp       short 00438653h
  004385ec  b9c0935e00            mov       ecx,5E93C0h
  004385f1  ba04000000            mov       edx,4
  004385f6  8b01                  mov       eax,[ecx]
  004385f8  a900000080            test      eax,80000000h
  004385fd  7404                  je        short 00438603h
  004385ff  0c08                  or        al,8
  00438601  8901                  mov       [ecx],eax
  00438603  81c19c000000          add       ecx,9Ch
  00438609  4a                    dec       edx
  0043860a  75ea                  jne       short 004385F6h
  0043860c  eb45                  jmp       short 00438653h
  0043860e  66ff8698000000        inc       word [esi+98h]
  00438615  6683be9800000027      cmp       word [esi+98h],27h
  0043861d  7534                  jne       short 00438653h
  0043861f  66c786980000000000    mov       word [esi+98h],0
  00438628  8b0dcc975e00          mov       ecx,[5E97CCh]
  0043862e  6a00                  push      0
  00438630  6a00                  push      0
  00438632  8b91a8000000          mov       edx,[ecx+0A8h]
  00438638  6800100000            push      1000h
  0043863d  6800040000            push      400h
  00438642  8b02                  mov       eax,[edx]
  00438644  660fb64833            movzx     cx,byte [eax+33h]
  00438649  51                    push      ecx
  0043864a  56                    push      esi
  0043864b  e830fa0200            call      00468080h
  00438650  83c418                add       esp,18h
  00438653  66837e1003            cmp       word [esi+10h],3
  00438658  0f8591000000          jne       near 004386EFh
  0043865e  6af9                  push      0FFFFFFF9h
  00438660  6af6                  push      0FFFFFFF6h
  00438662  6af6                  push      0FFFFFFF6h
  00438664  56                    push      esi
  00438665  e876c7fcff            call      00404DE0h
  0043866a  8b465c                mov       eax,[esi+5Ch]
  0043866d  bf14000000            mov       edi,14h
  00438672  83c410                add       esp,10h
  00438675  3bc7                  cmp       eax,edi
  00438677  7e2c                  jle       short 004386A5h
  00438679  8a1d24e05d00          mov       bl,[5DE024h]
  0043867f  8a1525e05d00          mov       dl,[5DE025h]
  00438685  8a0d26e05d00          mov       cl,[5DE026h]
  0043868b  b0fc                  mov       al,0FCh
  0043868d  02d8                  add       bl,al
  0043868f  02d0                  add       dl,al
  00438691  02c8                  add       cl,al
  00438693  881d24e05d00          mov       [5DE024h],bl
  00438699  881525e05d00          mov       [5DE025h],dl
  0043869f  880d26e05d00          mov       [5DE026h],cl
  004386a5  8a463a                mov       al,[esi+3Ah]
  004386a8  84c0                  test      al,al
  004386aa  7543                  jne       short 004386EFh
  004386ac  8b06                  mov       eax,[esi]
  004386ae  b9c0935e00            mov       ecx,5E93C0h
  004386b3  25ffffff7f            and       eax,7FFFFFFFh
  004386b8  ba04000000            mov       edx,4
  004386bd  8906                  mov       [esi],eax
  004386bf  8b01                  mov       eax,[ecx]
  004386c1  a900000080            test      eax,80000000h
  004386c6  7404                  je        short 004386CCh
  004386c8  24f7                  and       al,0F7h
  004386ca  8901                  mov       [ecx],eax
  004386cc  81c19c000000          add       ecx,9Ch
  004386d2  4a                    dec       edx
  004386d3  75ea                  jne       short 004386BFh
  004386d5  397e5c                cmp       [esi+5Ch],edi
  004386d8  7e15                  jle       short 004386EFh
  004386da  c60524e05d0000        mov       byte [5DE024h],0
  004386e1  c60525e05d0000        mov       byte [5DE025h],0
  004386e8  c60526e05d0000        mov       byte [5DE026h],0
  004386ef  8b565c                mov       edx,[esi+5Ch]
  004386f2  42                    inc       edx
  004386f3  8bc2                  mov       eax,edx
  004386f5  89565c                mov       [esi+5Ch],edx
  004386f8  3db80b0000            cmp       eax,0BB8h
  004386fd  7e07                  jle       short 00438706h
  004386ff  c7465cb80b0000        mov       dword [esi+5Ch],0BB8h
  00438706  5f                    pop       edi
  00438707  5e                    pop       esi
  00438708  5b                    pop       ebx
  00438709  c3                    ret
