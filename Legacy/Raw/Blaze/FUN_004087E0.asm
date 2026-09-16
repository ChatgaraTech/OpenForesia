Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004087e0  section .text  offset fichier 0x7be0  304 octets

  004087e0  53                    push      ebx
  004087e1  56                    push      esi
  004087e2  8b74240c              mov       esi,[esp+0Ch]
  004087e6  33db                  xor       ebx,ebx
  004087e8  57                    push      edi
  004087e9  bf01000000            mov       edi,1
  004087ee  66395e10              cmp       [esi+10h],bx
  004087f2  7570                  jne       short 00408864h
  004087f4  b800100000            mov       eax,1000h
  004087f9  c7463cd8f35400        mov       dword [esi+3Ch],54F3D8h
  00408800  66894628              mov       [esi+28h],ax
  00408804  6689462a              mov       [esi+2Ah],ax
  00408808  c6460a05              mov       byte [esi+0Ah],5
  0040880c  885e0b                mov       [esi+0Bh],bl
  0040880f  66897e10              mov       [esi+10h],di
  00408813  e828e91300            call      00547140h
  00408818  99                    cdq
  00408819  33c2                  xor       eax,edx
  0040881b  2bc2                  sub       eax,edx
  0040881d  25ff0f0000            and       eax,0FFFh
  00408822  33c2                  xor       eax,edx
  00408824  2bc2                  sub       eax,edx
  00408826  66894612              mov       [esi+12h],ax
  0040882a  e811e91300            call      00547140h
  0040882f  99                    cdq
  00408830  b905000000            mov       ecx,5
  00408835  885e40                mov       [esi+40h],bl
  00408838  f7f9                  idiv      ecx
  0040883a  895e5c                mov       [esi+5Ch],ebx
  0040883d  8d420a                lea       eax,[edx+0Ah]
  00408840  0fbfc8                movsx     ecx,ax
  00408843  66894614              mov       [esi+14h],ax
  00408847  b856555555            mov       eax,55555556h
  0040884c  f7e9                  imul      ecx
  0040884e  8bc2                  mov       eax,edx
  00408850  c1e81f                shr       eax,1Fh
  00408853  03d0                  add       edx,eax
  00408855  b05a                  mov       al,5Ah
  00408857  66895616              mov       [esi+16h],dx
  0040885b  884638                mov       [esi+38h],al
  0040885e  884639                mov       [esi+39h],al
  00408861  88463a                mov       [esi+3Ah],al

LAB_00408864:
  00408864  66397e10              cmp       [esi+10h],di
  00408868  0f8594000000          jne       near 00408902h
  0040886e  81462000a00000        add       dword [esi+20h],0A000h
  00408875  0fbf4e12              movsx     ecx,word [esi+12h]
  00408879  51                    push      ecx
  0040887a  e89dd11300            call      00545A1Ch
  0040887f  0fbf5614              movsx     edx,word [esi+14h]
  00408883  d1e2                  shl       edx,1
  00408885  0fafc2                imul      eax,edx
  00408888  8b561c                mov       edx,[esi+1Ch]
  0040888b  668b7e14              mov       di,[esi+14h]
  0040888f  03d0                  add       edx,eax
  00408891  0fbf4612              movsx     eax,word [esi+12h]
  00408895  50                    push      eax
  00408896  89561c                mov       [esi+1Ch],edx
  00408899  e878d11300            call      00545A16h
  0040889e  0fbfcf                movsx     ecx,di
  004088a1  d1e1                  shl       ecx,1
  004088a3  0fafc1                imul      eax,ecx
  004088a6  8b5624                mov       edx,[esi+24h]
  004088a9  03d0                  add       edx,eax
  004088ab  8d47ff                lea       eax,[edi-1]
  004088ae  663bc3                cmp       ax,bx
  004088b1  895624                mov       [esi+24h],edx
  004088b4  66894614              mov       [esi+14h],ax
  004088b8  7f06                  jg        short 004088C0h
  004088ba  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004088c0:
  004088c0  6afa                  push      0FFFFFFFAh
  004088c2  6afa                  push      0FFFFFFFAh
  004088c4  6afa                  push      0FFFFFFFAh
  004088c6  56                    push      esi
  004088c7  e814c5ffff            call      00404DE0h
  004088cc  8b565c                mov       edx,[esi+5Ch]
  004088cf  b864000000            mov       eax,64h
  004088d4  66014628              add       [esi+28h],ax
  004088d8  6601462a              add       [esi+2Ah],ax
  004088dc  83c410                add       esp,10h
  004088df  42                    inc       edx
  004088e0  89565c                mov       [esi+5Ch],edx
  004088e3  8bc2                  mov       eax,edx
  004088e5  0fbf5616              movsx     edx,word [esi+16h]
  004088e9  3bc2                  cmp       eax,edx
  004088eb  7c15                  jl        short 00408902h
  004088ed  8a4e40                mov       cl,[esi+40h]
  004088f0  895e5c                mov       [esi+5Ch],ebx
  004088f3  fec1                  inc       cl
  004088f5  8ac1                  mov       al,cl
  004088f7  884e40                mov       [esi+40h],cl
  004088fa  3c04                  cmp       al,4
  004088fc  7204                  jb        short 00408902h
  004088fe  c6464003              mov       byte [esi+40h],3

LAB_00408902:
  00408902  5f                    pop       edi
  00408903  5e                    pop       esi
  00408904  5b                    pop       ebx
  00408905  c3                    ret
  00408906  90                    nop
  00408907  90                    nop
  00408908  90                    nop
  00408909  90                    nop
  0040890a  90                    nop
  0040890b  90                    nop
  0040890c  90                    nop
  0040890d  90                    nop
  0040890e  90                    nop
  0040890f  90                    nop

3 cible(s) de saut interne : 0x00408864, 0x004088c0, 0x00408902
