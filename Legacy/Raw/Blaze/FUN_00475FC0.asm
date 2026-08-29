Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475fc0  section .text  offset fichier 0x753c0  256 octets

  00475fc0  8b442404              mov       eax,[esp+4]
  00475fc4  53                    push      ebx
  00475fc5  55                    push      ebp
  00475fc6  56                    push      esi
  00475fc7  57                    push      edi
  00475fc8  8b7870                mov       edi,[eax+70h]
  00475fcb  33c9                  xor       ecx,ecx
  00475fcd  3bf9                  cmp       edi,ecx
  00475fcf  7538                  jne       short 00476009h
  00475fd1  66394810              cmp       [eax+10h],cx
  00475fd5  7527                  jne       short 00475FFEh
  00475fd7  8b501c                mov       edx,[eax+1Ch]
  00475fda  8b5844                mov       ebx,[eax+44h]
  00475fdd  8b7848                mov       edi,[eax+48h]
  00475fe0  8b704c                mov       esi,[eax+4Ch]
  00475fe3  03da                  add       ebx,edx
  00475fe5  8b5020                mov       edx,[eax+20h]
  00475fe8  03fa                  add       edi,edx
  00475fea  8b5024                mov       edx,[eax+24h]
  00475fed  03f2                  add       esi,edx
  00475fef  66c740100100          mov       word [eax+10h],1
  00475ff5  895844                mov       [eax+44h],ebx
  00475ff8  897848                mov       [eax+48h],edi
  00475ffb  89704c                mov       [eax+4Ch],esi

LAB_00475ffe:
  00475ffe  89481c                mov       [eax+1Ch],ecx
  00476001  894820                mov       [eax+20h],ecx
  00476004  894824                mov       [eax+24h],ecx
  00476007  8bf8                  mov       edi,eax

LAB_00476009:
  00476009  0fbf4812              movsx     ecx,word [eax+12h]
  0047600d  8b5050                mov       edx,[eax+50h]
  00476010  8b6858                mov       ebp,[eax+58h]
  00476013  8bf2                  mov       esi,edx
  00476015  d3fe                  sar       esi,cl
  00476017  2bd6                  sub       edx,esi
  00476019  8b7054                mov       esi,[eax+54h]
  0047601c  8bde                  mov       ebx,esi
  0047601e  895050                mov       [eax+50h],edx
  00476021  d3fb                  sar       ebx,cl
  00476023  2bf3                  sub       esi,ebx
  00476025  8bdd                  mov       ebx,ebp
  00476027  d3fb                  sar       ebx,cl
  00476029  8bcd                  mov       ecx,ebp
  0047602b  8b6848                mov       ebp,[eax+48h]
  0047602e  03ee                  add       ebp,esi
  00476030  897054                mov       [eax+54h],esi
  00476033  896848                mov       [eax+48h],ebp
  00476036  8bf5                  mov       esi,ebp
  00476038  2bcb                  sub       ecx,ebx
  0047603a  8b5844                mov       ebx,[eax+44h]
  0047603d  03da                  add       ebx,edx
  0047603f  894858                mov       [eax+58h],ecx
  00476042  895844                mov       [eax+44h],ebx
  00476045  8bd3                  mov       edx,ebx
  00476047  8b584c                mov       ebx,[eax+4Ch]
  0047604a  03d9                  add       ebx,ecx
  0047604c  89584c                mov       [eax+4Ch],ebx
  0047604f  8bcb                  mov       ecx,ebx
  00476051  8b5f1c                mov       ebx,[edi+1Ch]
  00476054  03da                  add       ebx,edx
  00476056  89581c                mov       [eax+1Ch],ebx
  00476059  8b5720                mov       edx,[edi+20h]
  0047605c  03d6                  add       edx,esi
  0047605e  895020                mov       [eax+20h],edx
  00476061  8b5724                mov       edx,[edi+24h]
  00476064  0fbf7014              movsx     esi,word [eax+14h]
  00476068  03d1                  add       edx,ecx
  0047606a  895024                mov       [eax+24h],edx
  0047606d  668b5028              mov       dx,[eax+28h]
  00476071  0fbfca                movsx     ecx,dx
  00476074  2bf1                  sub       esi,ecx
  00476076  8a4816                mov       cl,[eax+16h]
  00476079  d3fe                  sar       esi,cl
  0047607b  8d0c16                lea       ecx,[esi+edx]
  0047607e  8a5040                mov       dl,[eax+40h]
  00476081  fec2                  inc       dl
  00476083  66894828              mov       [eax+28h],cx
  00476087  885040                mov       [eax+40h],dl
  0047608a  6689482a              mov       [eax+2Ah],cx
  0047608e  660fb6d2              movzx     dx,dl
  00476092  663b501a              cmp       dx,[eax+1Ah]
  00476096  7c04                  jl        short 0047609Ch
  00476098  c6404000              mov       byte [eax+40h],0

LAB_0047609c:
  0047609c  8b4838                mov       ecx,[eax+38h]
  0047609f  f7c1ffffff00          test      ecx,0FFFFFFh
  004760a5  740e                  je        short 004760B5h
  004760a7  81c1f0efefff          add       ecx,0FFEFEFF0h
  004760ad  894838                mov       [eax+38h],ecx
  004760b0  5f                    pop       edi
  004760b1  5e                    pop       esi
  004760b2  5d                    pop       ebp
  004760b3  5b                    pop       ebx
  004760b4  c3                    ret

LAB_004760b5:
  004760b5  5f                    pop       edi
  004760b6  5e                    pop       esi
  004760b7  5d                    pop       ebp
  004760b8  c70000000000          mov       dword [eax],0
  004760be  5b                    pop       ebx
  004760bf  c3                    ret

4 cible(s) de saut interne : 0x00475ffe, 0x00476009, 0x0047609c, 0x004760b5
