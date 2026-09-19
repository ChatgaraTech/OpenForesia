Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485330  section .text  offset fichier 0x84730  288 octets

  00485330  83ec0c                sub       esp,0Ch
  00485333  56                    push      esi
  00485334  8b742414              mov       esi,[esp+14h]
  00485338  57                    push      edi
  00485339  66c74424080000        mov       word [esp+8],0
  00485340  66837e1000            cmp       word [esi+10h],0
  00485345  66c744240a0000        mov       word [esp+0Ah],0
  0048534c  66c744240c3c00        mov       word [esp+0Ch],3Ch
  00485353  66c744240ec800        mov       word [esp+0Eh],0C8h
  0048535a  66c74424100000        mov       word [esp+10h],0
  00485361  66c74424127800        mov       word [esp+12h],78h
  00485368  744a                  je        short 004853B4h
  0048536a  0fbf462a              movsx     eax,word [esi+2Ah]
  0048536e  8bc8                  mov       ecx,eax
  00485370  8b7e1c                mov       edi,[esi+1Ch]
  00485373  d1f9                  sar       ecx,1
  00485375  2bc1                  sub       eax,ecx
  00485377  b900400000            mov       ecx,4000h
  0048537c  6689462a              mov       [esi+2Ah],ax
  00485380  6689462c              mov       [esi+2Ch],ax
  00485384  668b4628              mov       ax,[esi+28h]
  00485388  0fbfd0                movsx     edx,ax
  0048538b  2bca                  sub       ecx,edx
  0048538d  8b5650                mov       edx,[esi+50h]
  00485390  c1f902                sar       ecx,2
  00485393  03c8                  add       ecx,eax
  00485395  8b4654                mov       eax,[esi+54h]
  00485398  66894e28              mov       [esi+28h],cx
  0048539c  8b4e20                mov       ecx,[esi+20h]
  0048539f  03c8                  add       ecx,eax
  004853a1  8b4624                mov       eax,[esi+24h]
  004853a4  894e20                mov       [esi+20h],ecx
  004853a7  8b4e58                mov       ecx,[esi+58h]
  004853aa  03fa                  add       edi,edx
  004853ac  03c1                  add       eax,ecx
  004853ae  897e1c                mov       [esi+1Ch],edi
  004853b1  894624                mov       [esi+24h],eax

LAB_004853b4:
  004853b4  8b7e20                mov       edi,[esi+20h]
  004853b7  56                    push      esi
  004853b8  e883e50a00            call      00533940h
  004853bd  8b4664                mov       eax,[esi+64h]
  004853c0  83c404                add       esp,4
  004853c3  3bf8                  cmp       edi,eax
  004853c5  897e20                mov       [esi+20h],edi
  004853c8  7f0d                  jg        short 004853D7h
  004853ca  894620                mov       [esi+20h],eax
  004853cd  c7869000000000080000  mov       dword [esi+90h],800h

LAB_004853d7:
  004853d7  f7869000000002880000  test      dword [esi+90h],8802h
  004853e1  7557                  jne       short 0048543Ah
  004853e3  8d542408              lea       edx,[esp+8]
  004853e7  52                    push      edx
  004853e8  56                    push      esi
  004853e9  e882f8feff            call      00474C70h
  004853ee  8bf8                  mov       edi,eax
  004853f0  83c408                add       esp,8
  004853f3  85ff                  test      edi,edi
  004853f5  7425                  je        short 0048541Ch
  004853f7  a1b8e05d00            mov       eax,[5DE0B8h]
  004853fc  85c0                  test      eax,eax
  004853fe  743a                  je        short 0048543Ah
  00485400  8b4674                mov       eax,[esi+74h]
  00485403  57                    push      edi
  00485404  50                    push      eax
  00485405  e876470b00            call      00539B80h
  0048540a  8b07                  mov       eax,[edi]
  0048540c  83c408                add       esp,8
  0048540f  0c40                  or        al,40h
  00485411  66c787960000001000    mov       word [edi+96h],10h
  0048541a  8907                  mov       [edi],eax

LAB_0048541c:
  0048541c  814638e8e7e7ff        add       dword [esi+38h],0FFE7E7E8h
  00485423  e8181d0c00            call      00547140h
  00485428  2403                  and       al,3
  0048542a  0408                  add       al,8
  0048542c  66ff4610              inc       word [esi+10h]
  00485430  66837e1008            cmp       word [esi+10h],8
  00485435  884604                mov       [esi+4],al
  00485438  7c06                  jl        short 00485440h

LAB_0048543a:
  0048543a  c70600000000          mov       dword [esi],0

LAB_00485440:
  00485440  5f                    pop       edi
  00485441  5e                    pop       esi
  00485442  83c40c                add       esp,0Ch
  00485445  c3                    ret
  00485446  90                    nop
  00485447  90                    nop
  00485448  90                    nop
  00485449  90                    nop
  0048544a  90                    nop
  0048544b  90                    nop
  0048544c  90                    nop
  0048544d  90                    nop
  0048544e  90                    nop
  0048544f  90                    nop

5 cible(s) de saut interne : 0x004853b4, 0x004853d7, 0x0048541c, 0x0048543a, 0x00485440
