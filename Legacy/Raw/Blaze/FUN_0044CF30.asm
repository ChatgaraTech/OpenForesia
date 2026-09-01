Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044cf30  section .text  offset fichier 0x4c330  249 octets

  0044cf30  53                    push      ebx
  0044cf31  56                    push      esi
  0044cf32  8b74240c              mov       esi,[esp+0Ch]
  0044cf36  33db                  xor       ebx,ebx
  0044cf38  8b4678                mov       eax,[esi+78h]
  0044cf3b  3bc3                  cmp       eax,ebx
  0044cf3d  7505                  jne       short 0044CF44h
  0044cf3f  b858e55d00            mov       eax,5DE558h

LAB_0044cf44:
  0044cf44  8b481c                mov       ecx,[eax+1Ch]
  0044cf47  66395e10              cmp       [esi+10h],bx
  0044cf4b  894e1c                mov       [esi+1Ch],ecx
  0044cf4e  8b5060                mov       edx,[eax+60h]
  0044cf51  895620                mov       [esi+20h],edx
  0044cf54  8b4024                mov       eax,[eax+24h]
  0044cf57  894624                mov       [esi+24h],eax
  0044cf5a  b801000000            mov       eax,1
  0044cf5f  7531                  jne       short 0044CF92h
  0044cf61  668b4e36              mov       cx,[esi+36h]
  0044cf65  c646042d              mov       byte [esi+4],2Dh
  0044cf69  83e1fe                and       ecx,0FFFFFFFEh
  0044cf6c  c6460a07              mov       byte [esi+0Ah],7
  0044cf70  83c904                or        ecx,4
  0044cf73  885e0b                mov       [esi+0Bh],bl
  0044cf76  66894610              mov       [esi+10h],ax
  0044cf7a  66895e28              mov       [esi+28h],bx
  0044cf7e  66895e2c              mov       [esi+2Ch],bx
  0044cf82  66895e2a              mov       [esi+2Ah],bx
  0044cf86  66895e34              mov       [esi+34h],bx
  0044cf8a  66895e16              mov       [esi+16h],bx
  0044cf8e  66894e36              mov       [esi+36h],cx

LAB_0044cf92:
  0044cf92  66394610              cmp       [esi+10h],ax
  0044cf96  7567                  jne       short 0044CFFFh
  0044cf98  668b4628              mov       ax,[esi+28h]
  0044cf9c  b900100000            mov       ecx,1000h
  0044cfa1  0fbfd0                movsx     edx,ax
  0044cfa4  2bca                  sub       ecx,edx
  0044cfa6  6a0a                  push      0Ah
  0044cfa8  c1f902                sar       ecx,2
  0044cfab  03c8                  add       ecx,eax
  0044cfad  668b462c              mov       ax,[esi+2Ch]
  0044cfb1  0fbfd0                movsx     edx,ax
  0044cfb4  66894e28              mov       [esi+28h],cx
  0044cfb8  b900100000            mov       ecx,1000h
  0044cfbd  2bca                  sub       ecx,edx
  0044cfbf  6a0a                  push      0Ah
  0044cfc1  c1f902                sar       ecx,2
  0044cfc4  03c8                  add       ecx,eax
  0044cfc6  6a0a                  push      0Ah
  0044cfc8  56                    push      esi
  0044cfc9  66894e2c              mov       [esi+2Ch],cx
  0044cfcd  66c7462aa861          mov       word [esi+2Ah],61A8h
  0044cfd3  e8087efbff            call      00404DE0h
  0044cfd8  8a4e38                mov       cl,[esi+38h]
  0044cfdb  b064                  mov       al,64h
  0044cfdd  83c410                add       esp,10h
  0044cfe0  3ac8                  cmp       cl,al
  0044cfe2  7209                  jb        short 0044CFEDh
  0044cfe4  884638                mov       [esi+38h],al
  0044cfe7  884639                mov       [esi+39h],al
  0044cfea  88463a                mov       [esi+3Ah],al

LAB_0044cfed:
  0044cfed  66817e168c00          cmp       word [esi+16h],8Ch
  0044cff3  7c0a                  jl        short 0044CFFFh
  0044cff5  66c746100200          mov       word [esi+10h],2
  0044cffb  66895e12              mov       [esi+12h],bx

LAB_0044cfff:
  0044cfff  66837e1002            cmp       word [esi+10h],2
  0044d004  751c                  jne       short 0044D022h
  0044d006  6aec                  push      0FFFFFFECh
  0044d008  6aec                  push      0FFFFFFECh
  0044d00a  6aec                  push      0FFFFFFECh
  0044d00c  56                    push      esi
  0044d00d  e8ce7dfbff            call      00404DE0h
  0044d012  8a4638                mov       al,[esi+38h]
  0044d015  83c410                add       esp,10h
  0044d018  3ac3                  cmp       al,bl
  0044d01a  7506                  jne       short 0044D022h
  0044d01c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044d022:
  0044d022  66ff4616              inc       word [esi+16h]
  0044d026  5e                    pop       esi
  0044d027  5b                    pop       ebx
  0044d028  c3                    ret

5 cible(s) de saut interne : 0x0044cf44, 0x0044cf92, 0x0044cfed, 0x0044cfff, 0x0044d022
