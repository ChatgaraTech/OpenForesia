Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045da40  section .text  offset fichier 0x5ce40  200 octets

  0045da40  51                    push      ecx
  0045da41  668b442410            mov       ax,[esp+10h]
  0045da46  53                    push      ebx
  0045da47  55                    push      ebp
  0045da48  33ed                  xor       ebp,ebp
  0045da4a  56                    push      esi
  0045da4b  663bc5                cmp       ax,bp
  0045da4e  57                    push      edi
  0045da4f  896c2410              mov       [esp+10h],ebp
  0045da53  750e                  jne       short 0045DA63h
  0045da55  8b4c2418              mov       ecx,[esp+18h]
  0045da59  8a4909                mov       cl,[ecx+9]
  0045da5c  80e10f                and       cl,0Fh
  0045da5f  884c2420              mov       [esp+20h],cl

LAB_0045da63:
  0045da63  663d0100              cmp       ax,1
  0045da67  750e                  jne       short 0045DA77h
  0045da69  8b542418              mov       edx,[esp+18h]
  0045da6d  8a4209                mov       al,[edx+9]
  0045da70  c0e804                shr       al,4
  0045da73  88442420              mov       [esp+20h],al

LAB_0045da77:
  0045da77  8b7c2428              mov       edi,[esp+28h]
  0045da7b  33db                  xor       ebx,ebx
  0045da7d  bec0935e00            mov       esi,5E93C0h

LAB_0045da82:
  0045da82  8b06                  mov       eax,[esi]
  0045da84  a900000080            test      eax,80000000h
  0045da89  745b                  je        short 0045DAE6h
  0045da8b  a900000050            test      eax,50000000h
  0045da90  7554                  jne       short 0045DAE6h
  0045da92  8a442420              mov       al,[esp+20h]
  0045da96  8acb                  mov       cl,bl
  0045da98  d2e8                  shr       al,cl
  0045da9a  a801                  test      al,1
  0045da9c  7448                  je        short 0045DAE6h
  0045da9e  6683be9600000000      cmp       word [esi+96h],0
  0045daa6  753e                  jne       short 0045DAE6h
  0045daa8  0fbf54241c            movsx     edx,word [esp+1Ch]
  0045daad  8b4c2424              mov       ecx,[esp+24h]
  0045dab1  51                    push      ecx
  0045dab2  52                    push      edx
  0045dab3  56                    push      esi
  0045dab4  e8f7bc0d00            call      005397B0h
  0045dab9  8b0e                  mov       ecx,[esi]
  0045dabb  83c40c                add       esp,0Ch
  0045dabe  83c940                or        ecx,40h
  0045dac1  85ff                  test      edi,edi
  0045dac3  890e                  mov       [esi],ecx
  0045dac5  750b                  jne       short 0045DAD2h
  0045dac7  66c786960000003c00    mov       word [esi+96h],3Ch
  0045dad0  eb07                  jmp       short 0045DAD9h

LAB_0045dad2:
  0045dad2  6689be96000000        mov       [esi+96h],di

LAB_0045dad9:
  0045dad9  b801000000            mov       eax,1
  0045dade  8bcd                  mov       ecx,ebp
  0045dae0  d3e0                  shl       eax,cl
  0045dae2  09442410              or        [esp+10h],eax

LAB_0045dae6:
  0045dae6  43                    inc       ebx
  0045dae7  45                    inc       ebp
  0045dae8  81c69c000000          add       esi,9Ch
  0045daee  6683fb03              cmp       bx,3
  0045daf2  7e8e                  jle       short 0045DA82h
  0045daf4  8b4c2418              mov       ecx,[esp+18h]
  0045daf8  668b442410            mov       ax,[esp+10h]
  0045dafd  5f                    pop       edi
  0045dafe  5e                    pop       esi
  0045daff  5d                    pop       ebp
  0045db00  c6410900              mov       byte [ecx+9],0
  0045db04  5b                    pop       ebx
  0045db05  59                    pop       ecx
  0045db06  c3                    ret
  0045db07  90                    nop

6 cible(s) de saut interne : 0x0045da63, 0x0045da77, 0x0045da82, 0x0045dad2, 0x0045dad9, 0x0045dae6
