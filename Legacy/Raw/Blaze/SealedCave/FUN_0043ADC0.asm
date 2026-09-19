Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043adc0  section .text  offset fichier 0x3a1c0  304 octets

  0043adc0  53                    push      ebx
  0043adc1  56                    push      esi
  0043adc2  8b74240c              mov       esi,[esp+0Ch]
  0043adc6  33db                  xor       ebx,ebx
  0043adc8  57                    push      edi
  0043adc9  bf01000000            mov       edi,1
  0043adce  66395e10              cmp       [esi+10h],bx
  0043add2  0f8587000000          jne       near 0043AE5Fh
  0043add8  b800200000            mov       eax,2000h
  0043addd  66897e10              mov       [esi+10h],di
  0043ade1  c6460a02              mov       byte [esi+0Ah],2
  0043ade5  885e0b                mov       [esi+0Bh],bl
  0043ade8  c7463ca8105500        mov       dword [esi+3Ch],5510A8h
  0043adef  885e40                mov       [esi+40h],bl
  0043adf2  66894628              mov       [esi+28h],ax
  0043adf6  6689462a              mov       [esi+2Ah],ax
  0043adfa  e841c31000            call      00547140h
  0043adff  99                    cdq
  0043ae00  33c2                  xor       eax,edx
  0043ae02  2bc2                  sub       eax,edx
  0043ae04  25ff0f0000            and       eax,0FFFh
  0043ae09  33c2                  xor       eax,edx
  0043ae0b  2bc2                  sub       eax,edx
  0043ae0d  66894612              mov       [esi+12h],ax
  0043ae11  e82ac31000            call      00547140h
  0043ae16  99                    cdq
  0043ae17  b90a000000            mov       ecx,0Ah
  0043ae1c  f7f9                  idiv      ecx
  0043ae1e  03d1                  add       edx,ecx
  0043ae20  66895614              mov       [esi+14h],dx
  0043ae24  e817c31000            call      00547140h
  0043ae29  99                    cdq
  0043ae2a  b905000000            mov       ecx,5
  0043ae2f  895e5c                mov       [esi+5Ch],ebx
  0043ae32  f7f9                  idiv      ecx
  0043ae34  8b4620                mov       eax,[esi+20h]
  0043ae37  8b4e24                mov       ecx,[esi+24h]
  0043ae3a  894654                mov       [esi+54h],eax
  0043ae3d  b080                  mov       al,80h
  0043ae3f  894e58                mov       [esi+58h],ecx
  0043ae42  66899e9a000000        mov       [esi+9Ah],bx
  0043ae49  884638                mov       [esi+38h],al
  0043ae4c  884639                mov       [esi+39h],al
  0043ae4f  88463a                mov       [esi+3Ah],al
  0043ae52  83c203                add       edx,3
  0043ae55  66895616              mov       [esi+16h],dx
  0043ae59  8b561c                mov       edx,[esi+1Ch]
  0043ae5c  895650                mov       [esi+50h],edx

LAB_0043ae5f:
  0043ae5f  66397e10              cmp       [esi+10h],di
  0043ae63  0f8581000000          jne       near 0043AEEAh
  0043ae69  0fbf5612              movsx     edx,word [esi+12h]
  0043ae6d  52                    push      edx
  0043ae6e  e8a9ab1000            call      00545A1Ch
  0043ae73  0faf465c              imul      eax,[esi+5Ch]
  0043ae77  8b7e50                mov       edi,[esi+50h]
  0043ae7a  03c7                  add       eax,edi
  0043ae7c  8b7e5c                mov       edi,[esi+5Ch]
  0043ae7f  89461c                mov       [esi+1Ch],eax
  0043ae82  0fbf4612              movsx     eax,word [esi+12h]
  0043ae86  50                    push      eax
  0043ae87  e88aab1000            call      00545A16h
  0043ae8c  0fafc7                imul      eax,edi
  0043ae8f  8b4e58                mov       ecx,[esi+58h]
  0043ae92  8b5620                mov       edx,[esi+20h]
  0043ae95  03c1                  add       eax,ecx
  0043ae97  6afe                  push      0FFFFFFFEh
  0043ae99  0fbf4e16              movsx     ecx,word [esi+16h]
  0043ae9d  c1e10c                shl       ecx,0Ch
  0043aea0  03d1                  add       edx,ecx
  0043aea2  6afe                  push      0FFFFFFFEh
  0043aea4  895620                mov       [esi+20h],edx
  0043aea7  6afe                  push      0FFFFFFFEh
  0043aea9  0fbf5614              movsx     edx,word [esi+14h]
  0043aead  03d7                  add       edx,edi
  0043aeaf  66ff869a000000        inc       word [esi+9Ah]
  0043aeb6  56                    push      esi
  0043aeb7  894624                mov       [esi+24h],eax
  0043aeba  89565c                mov       [esi+5Ch],edx
  0043aebd  e81e9ffcff            call      00404DE0h
  0043aec2  83c410                add       esp,10h
  0043aec5  6683be9a00000003      cmp       word [esi+9Ah],3
  0043aecd  7e1b                  jle       short 0043AEEAh
  0043aecf  8a4e40                mov       cl,[esi+40h]
  0043aed2  66899e9a000000        mov       [esi+9Ah],bx
  0043aed9  fec1                  inc       cl
  0043aedb  8ac1                  mov       al,cl
  0043aedd  884e40                mov       [esi+40h],cl
  0043aee0  3c04                  cmp       al,4
  0043aee2  7506                  jne       short 0043AEEAh
  0043aee4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043aeea:
  0043aeea  5f                    pop       edi
  0043aeeb  5e                    pop       esi
  0043aeec  5b                    pop       ebx
  0043aeed  c3                    ret
  0043aeee  90                    nop
  0043aeef  90                    nop

2 cible(s) de saut interne : 0x0043ae5f, 0x0043aeea
