
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047a880  section .text  offset fichier 0x79c80  197 octets

  0047a880  8b442404              mov       eax,[esp+4]
  0047a884  53                    push      ebx
  0047a885  56                    push      esi
  0047a886  57                    push      edi
  0047a887  6683781000            cmp       word [eax+10h],0
  0047a88c  7514                  jne       short 0047A8A2h
  0047a88e  66c740100100          mov       word [eax+10h],1
  0047a894  c74038f0f0f000        mov       dword [eax+38h],0F0F0F0h
  0047a89b  c7402800100010        mov       dword [eax+28h],10001000h

LAB_0047a8a2:
  0047a8a2  8b4850                mov       ecx,[eax+50h]
  0047a8a5  8b501c                mov       edx,[eax+1Ch]
  0047a8a8  8b7824                mov       edi,[eax+24h]
  0047a8ab  8b7054                mov       esi,[eax+54h]
  0047a8ae  8b5820                mov       ebx,[eax+20h]
  0047a8b1  03d1                  add       edx,ecx
  0047a8b3  89501c                mov       [eax+1Ch],edx
  0047a8b6  8b5058                mov       edx,[eax+58h]
  0047a8b9  03fa                  add       edi,edx
  0047a8bb  03de                  add       ebx,esi
  0047a8bd  897824                mov       [eax+24h],edi
  0047a8c0  8bf9                  mov       edi,ecx
  0047a8c2  895820                mov       [eax+20h],ebx
  0047a8c5  8bd9                  mov       ebx,ecx
  0047a8c7  c1ff08                sar       edi,8
  0047a8ca  2bdf                  sub       ebx,edi
  0047a8cc  8bfe                  mov       edi,esi
  0047a8ce  c1f903                sar       ecx,3
  0047a8d1  2bd9                  sub       ebx,ecx
  0047a8d3  0fbf889a000000        movsx     ecx,word [eax+9Ah]
  0047a8da  895850                mov       [eax+50h],ebx
  0047a8dd  8bd9                  mov       ebx,ecx
  0047a8df  c1ff08                sar       edi,8
  0047a8e2  c1fb08                sar       ebx,8
  0047a8e5  2bdf                  sub       ebx,edi
  0047a8e7  03de                  add       ebx,esi
  0047a8e9  8bf2                  mov       esi,edx
  0047a8eb  2bd9                  sub       ebx,ecx
  0047a8ed  8bca                  mov       ecx,edx
  0047a8ef  c1f908                sar       ecx,8
  0047a8f2  2bf1                  sub       esi,ecx
  0047a8f4  895854                mov       [eax+54h],ebx
  0047a8f7  c1fa03                sar       edx,3
  0047a8fa  2bf2                  sub       esi,edx
  0047a8fc  8a5040                mov       dl,[eax+40h]
  0047a8ff  fec2                  inc       dl
  0047a901  897058                mov       [eax+58h],esi
  0047a904  8aca                  mov       cl,dl
  0047a906  885040                mov       [eax+40h],dl
  0047a909  80f903                cmp       cl,3
  0047a90c  7204                  jb        short 0047A912h
  0047a90e  c6404000              mov       byte [eax+40h],0

LAB_0047a912:
  0047a912  8b4838                mov       ecx,[eax+38h]
  0047a915  85c9                  test      ecx,ecx
  0047a917  7422                  je        short 0047A93Bh
  0047a919  81c1f0efefff          add       ecx,0FFEFEFF0h
  0047a91f  894838                mov       [eax+38h],ecx
  0047a922  668b4828              mov       cx,[eax+28h]
  0047a926  668bd1                mov       dx,cx
  0047a929  66c1fa03              sar       dx,3
  0047a92d  2bca                  sub       ecx,edx
  0047a92f  66894828              mov       [eax+28h],cx
  0047a933  6689482a              mov       [eax+2Ah],cx
  0047a937  5f                    pop       edi
  0047a938  5e                    pop       esi
  0047a939  5b                    pop       ebx
  0047a93a  c3                    ret

LAB_0047a93b:
  0047a93b  5f                    pop       edi
  0047a93c  5e                    pop       esi
  0047a93d  c70000000000          mov       dword [eax],0
  0047a943  5b                    pop       ebx
  0047a944  c3                    ret

3 cible(s) de saut interne : 0x0047a8a2, 0x0047a912, 0x0047a93b


