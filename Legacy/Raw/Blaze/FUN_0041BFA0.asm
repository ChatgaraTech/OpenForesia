Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041bfa0  section .text  offset fichier 0x1b3a0  320 octets

  0041bfa0  53                    push      ebx
  0041bfa1  56                    push      esi
  0041bfa2  8b74240c              mov       esi,[esp+0Ch]
  0041bfa6  33db                  xor       ebx,ebx
  0041bfa8  57                    push      edi
  0041bfa9  bf01000000            mov       edi,1
  0041bfae  66395e10              cmp       [esi+10h],bx
  0041bfb2  0f8587000000          jne       near 0041C03Fh
  0041bfb8  b800200000            mov       eax,2000h
  0041bfbd  66897e10              mov       [esi+10h],di
  0041bfc1  c6460a02              mov       byte [esi+0Ah],2
  0041bfc5  885e0b                mov       [esi+0Bh],bl
  0041bfc8  c7463ce0fd5400        mov       dword [esi+3Ch],54FDE0h
  0041bfcf  885e40                mov       [esi+40h],bl
  0041bfd2  66894628              mov       [esi+28h],ax
  0041bfd6  6689462a              mov       [esi+2Ah],ax
  0041bfda  e861b11200            call      00547140h
  0041bfdf  99                    cdq
  0041bfe0  33c2                  xor       eax,edx
  0041bfe2  2bc2                  sub       eax,edx
  0041bfe4  25ff0f0000            and       eax,0FFFh
  0041bfe9  33c2                  xor       eax,edx
  0041bfeb  2bc2                  sub       eax,edx
  0041bfed  66894612              mov       [esi+12h],ax
  0041bff1  e84ab11200            call      00547140h
  0041bff6  99                    cdq
  0041bff7  b90a000000            mov       ecx,0Ah
  0041bffc  f7f9                  idiv      ecx
  0041bffe  03d1                  add       edx,ecx
  0041c000  66895614              mov       [esi+14h],dx
  0041c004  e837b11200            call      00547140h
  0041c009  99                    cdq
  0041c00a  b905000000            mov       ecx,5
  0041c00f  895e5c                mov       [esi+5Ch],ebx
  0041c012  f7f9                  idiv      ecx
  0041c014  8b4620                mov       eax,[esi+20h]
  0041c017  8b4e24                mov       ecx,[esi+24h]
  0041c01a  894654                mov       [esi+54h],eax
  0041c01d  b0ff                  mov       al,0FFh
  0041c01f  894e58                mov       [esi+58h],ecx
  0041c022  66899e9a000000        mov       [esi+9Ah],bx
  0041c029  884638                mov       [esi+38h],al
  0041c02c  884639                mov       [esi+39h],al
  0041c02f  88463a                mov       [esi+3Ah],al
  0041c032  83c203                add       edx,3
  0041c035  66895616              mov       [esi+16h],dx
  0041c039  8b561c                mov       edx,[esi+1Ch]
  0041c03c  895650                mov       [esi+50h],edx

LAB_0041c03f:
  0041c03f  66397e10              cmp       [esi+10h],di
  0041c043  0f858d000000          jne       near 0041C0D6h
  0041c049  0fbf5612              movsx     edx,word [esi+12h]
  0041c04d  52                    push      edx
  0041c04e  e8c9991200            call      00545A1Ch
  0041c053  0faf465c              imul      eax,[esi+5Ch]
  0041c057  8b7e50                mov       edi,[esi+50h]
  0041c05a  03c7                  add       eax,edi
  0041c05c  8b7e5c                mov       edi,[esi+5Ch]
  0041c05f  89461c                mov       [esi+1Ch],eax
  0041c062  0fbf4612              movsx     eax,word [esi+12h]
  0041c066  50                    push      eax
  0041c067  e8aa991200            call      00545A16h
  0041c06c  0fafc7                imul      eax,edi
  0041c06f  8b4e58                mov       ecx,[esi+58h]
  0041c072  8b5620                mov       edx,[esi+20h]
  0041c075  03c1                  add       eax,ecx
  0041c077  0fbf4e16              movsx     ecx,word [esi+16h]
  0041c07b  c1e10c                shl       ecx,0Ch
  0041c07e  03d1                  add       edx,ecx
  0041c080  8a4e39                mov       cl,[esi+39h]
  0041c083  895620                mov       [esi+20h],edx
  0041c086  894624                mov       [esi+24h],eax
  0041c089  0fbf5614              movsx     edx,word [esi+14h]
  0041c08d  03d7                  add       edx,edi
  0041c08f  66ff869a000000        inc       word [esi+9Ah]
  0041c096  668b869a000000        mov       ax,[esi+9Ah]
  0041c09d  89565c                mov       [esi+5Ch],edx
  0041c0a0  8a5638                mov       dl,[esi+38h]
  0041c0a3  feca                  dec       dl
  0041c0a5  fec9                  dec       cl
  0041c0a7  885638                mov       [esi+38h],dl
  0041c0aa  8a563a                mov       dl,[esi+3Ah]
  0041c0ad  feca                  dec       dl
  0041c0af  663d0300              cmp       ax,3
  0041c0b3  884e39                mov       [esi+39h],cl
  0041c0b6  88563a                mov       [esi+3Ah],dl
  0041c0b9  7e1b                  jle       short 0041C0D6h
  0041c0bb  8a4e40                mov       cl,[esi+40h]
  0041c0be  66899e9a000000        mov       [esi+9Ah],bx
  0041c0c5  fec1                  inc       cl
  0041c0c7  8ac1                  mov       al,cl
  0041c0c9  884e40                mov       [esi+40h],cl
  0041c0cc  3c04                  cmp       al,4
  0041c0ce  7506                  jne       short 0041C0D6h
  0041c0d0  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041c0d6:
  0041c0d6  5f                    pop       edi
  0041c0d7  5e                    pop       esi
  0041c0d8  5b                    pop       ebx
  0041c0d9  c3                    ret
  0041c0da  90                    nop
  0041c0db  90                    nop
  0041c0dc  90                    nop
  0041c0dd  90                    nop
  0041c0de  90                    nop
  0041c0df  90                    nop

2 cible(s) de saut interne : 0x0041c03f, 0x0041c0d6
