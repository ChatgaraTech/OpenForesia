
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00406970  section .text  offset fichier 0x5d70  304 octets

  00406970  53                    push      ebx
  00406971  56                    push      esi
  00406972  8b74240c              mov       esi,[esp+0Ch]
  00406976  33db                  xor       ebx,ebx
  00406978  57                    push      edi
  00406979  bf01000000            mov       edi,1
  0040697e  66395e10              cmp       [esi+10h],bx
  00406982  7570                  jne       short 004069F4h
  00406984  b800100000            mov       eax,1000h
  00406989  c7463c80f25400        mov       dword [esi+3Ch],54F280h
  00406990  66894628              mov       [esi+28h],ax
  00406994  6689462a              mov       [esi+2Ah],ax
  00406998  c6460a05              mov       byte [esi+0Ah],5
  0040699c  885e0b                mov       [esi+0Bh],bl
  0040699f  66897e10              mov       [esi+10h],di
  004069a3  e898071400            call      00547140h
  004069a8  99                    cdq
  004069a9  33c2                  xor       eax,edx
  004069ab  2bc2                  sub       eax,edx
  004069ad  25ff0f0000            and       eax,0FFFh
  004069b2  33c2                  xor       eax,edx
  004069b4  2bc2                  sub       eax,edx
  004069b6  66894612              mov       [esi+12h],ax
  004069ba  e881071400            call      00547140h
  004069bf  99                    cdq
  004069c0  b905000000            mov       ecx,5
  004069c5  885e40                mov       [esi+40h],bl
  004069c8  f7f9                  idiv      ecx
  004069ca  895e5c                mov       [esi+5Ch],ebx
  004069cd  8d420a                lea       eax,[edx+0Ah]
  004069d0  0fbfc8                movsx     ecx,ax
  004069d3  66894614              mov       [esi+14h],ax
  004069d7  b856555555            mov       eax,55555556h
  004069dc  f7e9                  imul      ecx
  004069de  8bc2                  mov       eax,edx
  004069e0  c1e81f                shr       eax,1Fh
  004069e3  03d0                  add       edx,eax
  004069e5  b05a                  mov       al,5Ah
  004069e7  66895616              mov       [esi+16h],dx
  004069eb  884638                mov       [esi+38h],al
  004069ee  884639                mov       [esi+39h],al
  004069f1  88463a                mov       [esi+3Ah],al

LAB_004069f4:
  004069f4  66397e10              cmp       [esi+10h],di
  004069f8  0f8594000000          jne       near 00406A92h
  004069fe  81462000a00000        add       dword [esi+20h],0A000h
  00406a05  0fbf4e12              movsx     ecx,word [esi+12h]
  00406a09  51                    push      ecx
  00406a0a  e80df01300            call      00545A1Ch
  00406a0f  0fbf5614              movsx     edx,word [esi+14h]
  00406a13  d1e2                  shl       edx,1
  00406a15  0fafc2                imul      eax,edx
  00406a18  8b561c                mov       edx,[esi+1Ch]
  00406a1b  668b7e14              mov       di,[esi+14h]
  00406a1f  03d0                  add       edx,eax
  00406a21  0fbf4612              movsx     eax,word [esi+12h]
  00406a25  50                    push      eax
  00406a26  89561c                mov       [esi+1Ch],edx
  00406a29  e8e8ef1300            call      00545A16h
  00406a2e  0fbfcf                movsx     ecx,di
  00406a31  d1e1                  shl       ecx,1
  00406a33  0fafc1                imul      eax,ecx
  00406a36  8b5624                mov       edx,[esi+24h]
  00406a39  03d0                  add       edx,eax
  00406a3b  8d47ff                lea       eax,[edi-1]
  00406a3e  663bc3                cmp       ax,bx
  00406a41  895624                mov       [esi+24h],edx
  00406a44  66894614              mov       [esi+14h],ax
  00406a48  7f06                  jg        short 00406A50h
  00406a4a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00406a50:
  00406a50  6afa                  push      0FFFFFFFAh
  00406a52  6afa                  push      0FFFFFFFAh
  00406a54  6afa                  push      0FFFFFFFAh
  00406a56  56                    push      esi
  00406a57  e884e3ffff            call      00404DE0h
  00406a5c  8b565c                mov       edx,[esi+5Ch]
  00406a5f  b864000000            mov       eax,64h
  00406a64  66014628              add       [esi+28h],ax
  00406a68  6601462a              add       [esi+2Ah],ax
  00406a6c  83c410                add       esp,10h
  00406a6f  42                    inc       edx
  00406a70  89565c                mov       [esi+5Ch],edx
  00406a73  8bc2                  mov       eax,edx
  00406a75  0fbf5616              movsx     edx,word [esi+16h]
  00406a79  3bc2                  cmp       eax,edx
  00406a7b  7c15                  jl        short 00406A92h
  00406a7d  8a4e40                mov       cl,[esi+40h]
  00406a80  895e5c                mov       [esi+5Ch],ebx
  00406a83  fec1                  inc       cl
  00406a85  8ac1                  mov       al,cl
  00406a87  884e40                mov       [esi+40h],cl
  00406a8a  3c04                  cmp       al,4
  00406a8c  7204                  jb        short 00406A92h
  00406a8e  c6464003              mov       byte [esi+40h],3

LAB_00406a92:
  00406a92  5f                    pop       edi
  00406a93  5e                    pop       esi
  00406a94  5b                    pop       ebx
  00406a95  c3                    ret
  00406a96  90                    nop
  00406a97  90                    nop
  00406a98  90                    nop
  00406a99  90                    nop
  00406a9a  90                    nop
  00406a9b  90                    nop
  00406a9c  90                    nop
  00406a9d  90                    nop
  00406a9e  90                    nop
  00406a9f  90                    nop

3 cible(s) de saut interne : 0x004069f4, 0x00406a50, 0x00406a92


