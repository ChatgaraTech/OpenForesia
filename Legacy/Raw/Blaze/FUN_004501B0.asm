Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004501b0  section .text  offset fichier 0x4f5b0  275 octets

  004501b0  53                    push      ebx
  004501b1  33db                  xor       ebx,ebx
  004501b3  56                    push      esi
  004501b4  8b74240c              mov       esi,[esp+0Ch]
  004501b8  53                    push      ebx
  004501b9  53                    push      ebx
  004501ba  56                    push      esi
  004501bb  e8807effff            call      00448040h
  004501c0  8b4678                mov       eax,[esi+78h]
  004501c3  83c40c                add       esp,0Ch
  004501c6  3bc3                  cmp       eax,ebx
  004501c8  7505                  jne       short 004501CFh
  004501ca  b858e55d00            mov       eax,5DE558h

LAB_004501cf:
  004501cf  8b481c                mov       ecx,[eax+1Ch]
  004501d2  66395e10              cmp       [esi+10h],bx
  004501d6  894e1c                mov       [esi+1Ch],ecx
  004501d9  8b5060                mov       edx,[eax+60h]
  004501dc  895620                mov       [esi+20h],edx
  004501df  8b4024                mov       eax,[eax+24h]
  004501e2  894624                mov       [esi+24h],eax
  004501e5  b901000000            mov       ecx,1
  004501ea  7536                  jne       short 00450222h
  004501ec  8a969a000000          mov       dl,[esi+9Ah]
  004501f2  668b4614              mov       ax,[esi+14h]
  004501f6  66894e10              mov       [esi+10h],cx
  004501fa  c6460405              mov       byte [esi+4],5
  004501fe  885e40                mov       [esi+40h],bl
  00450201  88560a                mov       [esi+0Ah],dl
  00450204  885e0b                mov       [esi+0Bh],bl
  00450207  66894628              mov       [esi+28h],ax
  0045020b  6689462c              mov       [esi+2Ch],ax
  0045020f  66895e2a              mov       [esi+2Ah],bx
  00450213  885e38                mov       [esi+38h],bl
  00450216  885e39                mov       [esi+39h],bl
  00450219  885e3a                mov       [esi+3Ah],bl
  0045021c  66c746340008          mov       word [esi+34h],800h

LAB_00450222:
  00450222  66394e10              cmp       [esi+10h],cx
  00450226  0f8594000000          jne       near 004502C0h
  0045022c  668146329600          add       word [esi+32h],96h
  00450232  668b4632              mov       ax,[esi+32h]
  00450236  663d0010              cmp       ax,1000h
  0045023a  7c09                  jl        short 00450245h
  0045023c  0500f0ffff            add       eax,0FFFFF000h
  00450241  66894632              mov       [esi+32h],ax

LAB_00450245:
  00450245  668b465c              mov       ax,[esi+5Ch]
  00450249  6601462a              add       [esi+2Ah],ax
  0045024d  8b4644                mov       eax,[esi+44h]
  00450250  0fbf4e2a              movsx     ecx,word [esi+2Ah]
  00450254  3bc8                  cmp       ecx,eax
  00450256  7e08                  jle       short 00450260h
  00450258  668b5644              mov       dx,[esi+44h]
  0045025c  6689562a              mov       [esi+2Ah],dx

LAB_00450260:
  00450260  0fbf462a              movsx     eax,word [esi+2Ah]
  00450264  8b4e20                mov       ecx,[esi+20h]
  00450267  8d0440                lea       eax,[eax+eax*2]
  0045026a  8d0480                lea       eax,[eax+eax*4]
  0045026d  8d0480                lea       eax,[eax+eax*4]
  00450270  d1e0                  shl       eax,1
  00450272  03c8                  add       ecx,eax
  00450274  8b4648                mov       eax,[esi+48h]
  00450277  48                    dec       eax
  00450278  894e20                mov       [esi+20h],ecx
  0045027b  894648                mov       [esi+48h],eax
  0045027e  791f                  jns       short 0045029Fh
  00450280  6af6                  push      0FFFFFFF6h
  00450282  6af6                  push      0FFFFFFF6h
  00450284  6af6                  push      0FFFFFFF6h
  00450286  56                    push      esi
  00450287  e8544bfbff            call      00404DE0h
  0045028c  8a4638                mov       al,[esi+38h]
  0045028f  83c410                add       esp,10h
  00450292  3ac3                  cmp       al,bl
  00450294  752a                  jne       short 004502C0h
  00450296  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0045029c  5e                    pop       esi
  0045029d  5b                    pop       ebx
  0045029e  c3                    ret

LAB_0045029f:
  0045029f  6a0a                  push      0Ah
  004502a1  6a0a                  push      0Ah
  004502a3  6a0a                  push      0Ah
  004502a5  56                    push      esi
  004502a6  e8354bfbff            call      00404DE0h
  004502ab  8a4e38                mov       cl,[esi+38h]
  004502ae  b0b4                  mov       al,0B4h
  004502b0  83c410                add       esp,10h
  004502b3  3ac8                  cmp       cl,al
  004502b5  7609                  jbe       short 004502C0h
  004502b7  884638                mov       [esi+38h],al
  004502ba  884639                mov       [esi+39h],al
  004502bd  88463a                mov       [esi+3Ah],al

LAB_004502c0:
  004502c0  5e                    pop       esi
  004502c1  5b                    pop       ebx
  004502c2  c3                    ret

6 cible(s) de saut interne : 0x004501cf, 0x00450222, 0x00450245, 0x00450260, 0x0045029f, 0x004502c0
