Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485850  section .text  offset fichier 0x84c50  240 octets

  00485850  83ec0c                sub       esp,0Ch
  00485853  53                    push      ebx
  00485854  56                    push      esi
  00485855  8b742418              mov       esi,[esp+18h]
  00485859  57                    push      edi
  0048585a  33ff                  xor       edi,edi
  0048585c  56                    push      esi
  0048585d  66897c2410            mov       [esp+10h],di
  00485862  66897c2412            mov       [esp+12h],di
  00485867  66897c2414            mov       [esp+14h],di
  0048586c  66c7442416c800        mov       word [esp+16h],0C8h
  00485873  66897c2418            mov       [esp+18h],di
  00485878  66897c241a            mov       [esp+1Ah],di
  0048587d  e8feff0a00            call      00535880h
  00485882  668b462a              mov       ax,[esi+2Ah]
  00485886  83c404                add       esp,4
  00485889  0fbf4e12              movsx     ecx,word [esi+12h]
  0048588d  0fbfd0                movsx     edx,ax
  00485890  2bca                  sub       ecx,edx
  00485892  c1f902                sar       ecx,2
  00485895  03c8                  add       ecx,eax
  00485897  0fbf4628              movsx     eax,word [esi+28h]
  0048589b  66894e2a              mov       [esi+2Ah],cx
  0048589f  8d0480                lea       eax,[eax+eax*4]
  004858a2  8d0480                lea       eax,[eax+eax*4]
  004858a5  d1e0                  shl       eax,1
  004858a7  c1f80c                sar       eax,0Ch
  004858aa  6689442410            mov       [esp+10h],ax
  004858af  0fbf462c              movsx     eax,word [esi+2Ch]
  004858b3  8d0480                lea       eax,[eax+eax*4]
  004858b6  8d0c80                lea       ecx,[eax+eax*4]
  004858b9  8b4638                mov       eax,[esi+38h]
  004858bc  d1e1                  shl       ecx,1
  004858be  c1f90c                sar       ecx,0Ch
  004858c1  3d20202000            cmp       eax,202020h
  004858c6  66894c2416            mov       [esp+16h],cx
  004858cb  724e                  jb        short 0048591Bh
  004858cd  8d54240c              lea       edx,[esp+0Ch]
  004858d1  52                    push      edx
  004858d2  56                    push      esi
  004858d3  e898f3feff            call      00474C70h
  004858d8  8bd8                  mov       ebx,eax
  004858da  a1b8e05d00            mov       eax,[5DE0B8h]
  004858df  83c408                add       esp,8
  004858e2  3bc7                  cmp       eax,edi
  004858e4  7435                  je        short 0048591Bh
  004858e6  8b4674                mov       eax,[esi+74h]
  004858e9  53                    push      ebx
  004858ea  50                    push      eax
  004858eb  e890420b00            call      00539B80h
  004858f0  8b0b                  mov       ecx,[ebx]
  004858f2  83c408                add       esp,8
  004858f5  83c940                or        ecx,40h
  004858f8  66c783960000001000    mov       word [ebx+96h],10h
  00485901  890b                  mov       [ebx],ecx
  00485903  8d4e1c                lea       ecx,[esi+1Ch]
  00485906  51                    push      ecx
  00485907  6800180000            push      1800h
  0048590c  6800040000            push      400h
  00485911  6a03                  push      3
  00485913  e808820b00            call      0053DB20h
  00485918  83c410                add       esp,10h

LAB_0048591b:
  0048591b  8b4638                mov       eax,[esi+38h]
  0048591e  05f8f7f7ff            add       eax,0FFF7F7F8h
  00485923  894638                mov       [esi+38h],eax
  00485926  7502                  jne       short 0048592Ah
  00485928  893e                  mov       [esi],edi

LAB_0048592a:
  0048592a  5f                    pop       edi
  0048592b  5e                    pop       esi
  0048592c  5b                    pop       ebx
  0048592d  83c40c                add       esp,0Ch
  00485930  c3                    ret
  00485931  90                    nop
  00485932  90                    nop
  00485933  90                    nop
  00485934  90                    nop
  00485935  90                    nop
  00485936  90                    nop
  00485937  90                    nop
  00485938  90                    nop
  00485939  90                    nop
  0048593a  90                    nop
  0048593b  90                    nop
  0048593c  90                    nop
  0048593d  90                    nop
  0048593e  90                    nop
  0048593f  90                    nop

2 cible(s) de saut interne : 0x0048591b, 0x0048592a
