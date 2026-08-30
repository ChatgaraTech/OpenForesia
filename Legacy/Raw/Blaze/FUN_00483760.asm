; 0x00483760 — la charge qui se materialise, frappe et s'efface (140 instructions, absente de Blaze.c). Sonde FUN_00474ca0, boite (-0x400,-50,50,200,0,300).

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483760  section .text  offset fichier 0x82b60  460 octets

  00483760  83ec18                sub       esp,18h
  00483763  53                    push      ebx
  00483764  55                    push      ebp
  00483765  56                    push      esi
  00483766  8b742428              mov       esi,[esp+28h]
  0048376a  57                    push      edi
  0048376b  66c744241000fc        mov       word [esp+10h],0FC00h
  00483772  66837e1004            cmp       word [esi+10h],4
  00483777  66c7442412ceff        mov       word [esp+12h],0FFCEh
  0048377e  66c74424143200        mov       word [esp+14h],32h
  00483785  66c7442416c800        mov       word [esp+16h],0C8h
  0048378c  66c74424180000        mov       word [esp+18h],0
  00483793  66c744241a2c01        mov       word [esp+1Ah],12Ch
  0048379a  0f85c0000000          jne       near 00483860h
  004837a0  668b4612              mov       ax,[esi+12h]
  004837a4  6685c0                test      ax,ax
  004837a7  0f84b3000000          je        near 00483860h
  004837ad  0fbf6e32              movsx     ebp,word [esi+32h]
  004837b1  8b4e20                mov       ecx,[esi+20h]
  004837b4  8b5624                mov       edx,[esi+24h]
  004837b7  48                    dec       eax
  004837b8  8dbd00fcffff          lea       edi,[ebp-400h]
  004837be  66894612              mov       [esi+12h],ax
  004837c2  8b461c                mov       eax,[esi+1Ch]
  004837c5  57                    push      edi
  004837c6  89442420              mov       [esp+20h],eax
  004837ca  894c2424              mov       [esp+24h],ecx
  004837ce  89542428              mov       [esp+28h],edx
  004837d2  e845220c00            call      00545A1Ch
  004837d7  8b4e1c                mov       ecx,[esi+1Ch]
  004837da  8d0480                lea       eax,[eax+eax*4]
  004837dd  57                    push      edi
  004837de  8d0480                lea       eax,[eax+eax*4]
  004837e1  c1e003                shl       eax,3
  004837e4  03c8                  add       ecx,eax
  004837e6  894e1c                mov       [esi+1Ch],ecx
  004837e9  e828220c00            call      00545A16h
  004837ee  8d0480                lea       eax,[eax+eax*4]
  004837f1  bf02000000            mov       edi,2
  004837f6  0fbf5e32              movsx     ebx,word [esi+32h]
  004837fa  8d0c80                lea       ecx,[eax+eax*4]
  004837fd  8b4624                mov       eax,[esi+24h]
  00483800  c1e103                shl       ecx,3
  00483803  03c1                  add       eax,ecx
  00483805  894624                mov       [esi+24h],eax

LAB_00483808:
  00483808  e833390c00            call      00547140h
  0048380d  25ff010000            and       eax,1FFh
  00483812  83ff02                cmp       edi,2
  00483815  7402                  je        short 00483819h
  00483817  f7d8                  neg       eax

LAB_00483819:
  00483819  8d1403                lea       edx,[ebx+eax]
  0048381c  6a01                  push      1
  0048381e  66895632              mov       [esi+32h],dx
  00483822  a1cc975e00            mov       eax,[5E97CCh]
  00483827  6a01                  push      1
  00483829  8b88d0000000          mov       ecx,[eax+0D0h]
  0048382f  33c0                  xor       eax,eax
  00483831  8b11                  mov       edx,[ecx]
  00483833  8a02                  mov       al,[edx]
  00483835  05c0000000            add       eax,0C0h
  0048383a  50                    push      eax
  0048383b  56                    push      esi
  0048383c  e89f72ffff            call      0047AAE0h
  00483841  83c410                add       esp,10h
  00483844  4f                    dec       edi
  00483845  75c1                  jne       short 00483808h
  00483847  8b4c241c              mov       ecx,[esp+1Ch]
  0048384b  8b542420              mov       edx,[esp+20h]
  0048384f  8b442424              mov       eax,[esp+24h]
  00483853  894e1c                mov       [esi+1Ch],ecx
  00483856  895620                mov       [esi+20h],edx
  00483859  894624                mov       [esi+24h],eax
  0048385c  66896e32              mov       [esi+32h],bp

LAB_00483860:
  00483860  66837e1014            cmp       word [esi+10h],14h
  00483865  8b4638                mov       eax,[esi+38h]
  00483868  0f8d8c000000          jge       near 004838FAh
  0048386e  3d80808000            cmp       eax,808080h
  00483873  7314                  jae       short 00483889h
  00483875  0508080800            add       eax,80808h
  0048387a  66ff4610              inc       word [esi+10h]
  0048387e  894638                mov       [esi+38h],eax
  00483881  5f                    pop       edi
  00483882  5e                    pop       esi
  00483883  5d                    pop       ebp
  00483884  5b                    pop       ebx
  00483885  83c418                add       esp,18h
  00483888  c3                    ret

LAB_00483889:
  00483889  668b462a              mov       ax,[esi+2Ah]
  0048388d  ba00300000            mov       edx,3000h
  00483892  0fbfc8                movsx     ecx,ax
  00483895  2bd1                  sub       edx,ecx
  00483897  c1fa02                sar       edx,2
  0048389a  03d0                  add       edx,eax
  0048389c  8d442410              lea       eax,[esp+10h]
  004838a0  50                    push      eax
  004838a1  56                    push      esi
  004838a2  6689562a              mov       [esi+2Ah],dx
  004838a6  e8f513ffff            call      00474CA0h
  004838ab  8bf8                  mov       edi,eax
  004838ad  a1b8e05d00            mov       eax,[5DE0B8h]
  004838b2  83c408                add       esp,8
  004838b5  85c0                  test      eax,eax
  004838b7  7453                  je        short 0048390Ch
  004838b9  8b4e74                mov       ecx,[esi+74h]
  004838bc  57                    push      edi
  004838bd  51                    push      ecx
  004838be  e8bd620b00            call      00539B80h
  004838c3  8b2f                  mov       ebp,[edi]
  004838c5  83c408                add       esp,8
  004838c8  8d561c                lea       edx,[esi+1Ch]
  004838cb  83cd40                or        ebp,40h
  004838ce  52                    push      edx
  004838cf  6800180000            push      1800h
  004838d4  6800040000            push      400h
  004838d9  6a03                  push      3
  004838db  66c787960000000800    mov       word [edi+96h],8
  004838e4  892f                  mov       [edi],ebp
  004838e6  e835a20b00            call      0053DB20h
  004838eb  83c410                add       esp,10h
  004838ee  66ff4610              inc       word [esi+10h]
  004838f2  5f                    pop       edi
  004838f3  5e                    pop       esi
  004838f4  5d                    pop       ebp
  004838f5  5b                    pop       ebx
  004838f6  83c418                add       esp,18h
  004838f9  c3                    ret

LAB_004838fa:
  004838fa  85c0                  test      eax,eax
  004838fc  740e                  je        short 0048390Ch
  004838fe  05f8f7f7ff            add       eax,0FFF7F7F8h
  00483903  85c0                  test      eax,eax
  00483905  894638                mov       [esi+38h],eax
  00483908  7502                  jne       short 0048390Ch
  0048390a  8906                  mov       [esi],eax

LAB_0048390c:
  0048390c  66ff4610              inc       word [esi+10h]
  00483910  5f                    pop       edi
  00483911  5e                    pop       esi
  00483912  5d                    pop       ebp
  00483913  5b                    pop       ebx
  00483914  83c418                add       esp,18h
  00483917  c3                    ret
  00483918  90                    nop
  00483919  90                    nop
  0048391a  90                    nop
  0048391b  90                    nop
  0048391c  90                    nop
  0048391d  90                    nop
  0048391e  90                    nop
  0048391f  90                    nop
  00483920  8b442404              mov       eax,[esp+4]
  00483924  c70000000000          mov       dword [eax],0
  0048392a  c3                    ret
  0048392b  90                    nop

6 cible(s) de saut interne : 0x00483808, 0x00483819, 0x00483860, 0x00483889, 0x004838fa, 0x0048390c
