Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00453280  section .text  offset fichier 0x52680  285 octets

  00453280  83ec08                sub       esp,8
  00453283  33c0                  xor       eax,eax
  00453285  53                    push      ebx
  00453286  55                    push      ebp
  00453287  56                    push      esi
  00453288  8b742418              mov       esi,[esp+18h]
  0045328c  57                    push      edi
  0045328d  66394610              cmp       [esi+10h],ax
  00453291  752c                  jne       short 004532BFh
  00453293  0fbf4e30              movsx     ecx,word [esi+30h]
  00453297  0fbf5634              movsx     edx,word [esi+34h]
  0045329b  c1e10c                shl       ecx,0Ch
  0045329e  894e44                mov       [esi+44h],ecx
  004532a1  66c746100100          mov       word [esi+10h],1
  004532a7  0fbf4e14              movsx     ecx,word [esi+14h]
  004532ab  c1e20c                shl       edx,0Ch
  004532ae  c1e10c                shl       ecx,0Ch
  004532b1  895648                mov       [esi+48h],edx
  004532b4  894e4c                mov       [esi+4Ch],ecx
  004532b7  66894630              mov       [esi+30h],ax
  004532bb  66894634              mov       [esi+34h],ax

LAB_004532bf:
  004532bf  a168e05d00            mov       eax,[5DE068h]
  004532c4  8b561c                mov       edx,[esi+1Ch]
  004532c7  8b0d6ce05d00          mov       ecx,[5DE06Ch]
  004532cd  8b5e20                mov       ebx,[esi+20h]
  004532d0  c1f80c                sar       eax,0Ch
  004532d3  c1fa0c                sar       edx,0Ch
  004532d6  8bf8                  mov       edi,eax
  004532d8  8b6e24                mov       ebp,[esi+24h]
  004532db  c1f90c                sar       ecx,0Ch
  004532de  2bfa                  sub       edi,edx
  004532e0  8bd1                  mov       edx,ecx
  004532e2  c1fb0c                sar       ebx,0Ch
  004532e5  2bd3                  sub       edx,ebx
  004532e7  8954241c              mov       [esp+1Ch],edx
  004532eb  8b1570e05d00          mov       edx,[5DE070h]
  004532f1  c1fa0c                sar       edx,0Ch
  004532f4  c1fd0c                sar       ebp,0Ch
  004532f7  8bda                  mov       ebx,edx
  004532f9  2bdd                  sub       ebx,ebp
  004532fb  8b6e44                mov       ebp,[esi+44h]
  004532fe  c1fd0c                sar       ebp,0Ch
  00453301  2bc5                  sub       eax,ebp
  00453303  8be8                  mov       ebp,eax
  00453305  8b4648                mov       eax,[esi+48h]
  00453308  c1f80c                sar       eax,0Ch
  0045330b  2bc8                  sub       ecx,eax
  0045330d  8b44241c              mov       eax,[esp+1Ch]
  00453311  894c2410              mov       [esp+10h],ecx
  00453315  8b4e4c                mov       ecx,[esi+4Ch]
  00453318  c1f90c                sar       ecx,0Ch
  0045331b  2bd1                  sub       edx,ecx
  0045331d  8bc8                  mov       ecx,eax
  0045331f  89542414              mov       [esp+14h],edx
  00453323  8bd7                  mov       edx,edi
  00453325  0fafc8                imul      ecx,eax
  00453328  0fafd7                imul      edx,edi
  0045332b  8bc3                  mov       eax,ebx
  0045332d  03d1                  add       edx,ecx
  0045332f  0fafc3                imul      eax,ebx
  00453332  03d0                  add       edx,eax
  00453334  52                    push      edx
  00453335  e8d6260f00            call      00545A10h
  0045333a  8bf8                  mov       edi,eax
  0045333c  8b442410              mov       eax,[esp+10h]
  00453340  8bcd                  mov       ecx,ebp
  00453342  8bd0                  mov       edx,eax
  00453344  0fafcd                imul      ecx,ebp
  00453347  0fafd0                imul      edx,eax
  0045334a  8b442414              mov       eax,[esp+14h]
  0045334e  03ca                  add       ecx,edx
  00453350  8bd0                  mov       edx,eax
  00453352  0fafd0                imul      edx,eax
  00453355  03ca                  add       ecx,edx
  00453357  51                    push      ecx
  00453358  e8b3260f00            call      00545A10h
  0045335d  3bf8                  cmp       edi,eax
  0045335f  7e34                  jle       short 00453395h
  00453361  8b5e44                mov       ebx,[esi+44h]
  00453364  8b461c                mov       eax,[esi+1Ch]
  00453367  8b4e20                mov       ecx,[esi+20h]
  0045336a  8b5624                mov       edx,[esi+24h]
  0045336d  0fbf7e32              movsx     edi,word [esi+32h]
  00453371  895e1c                mov       [esi+1Ch],ebx
  00453374  8b5e48                mov       ebx,[esi+48h]
  00453377  895e20                mov       [esi+20h],ebx
  0045337a  8b5e4c                mov       ebx,[esi+4Ch]
  0045337d  895e24                mov       [esi+24h],ebx
  00453380  668b5e16              mov       bx,[esi+16h]
  00453384  66895e32              mov       [esi+32h],bx
  00453388  894644                mov       [esi+44h],eax
  0045338b  894e48                mov       [esi+48h],ecx
  0045338e  89564c                mov       [esi+4Ch],edx
  00453391  66897e16              mov       [esi+16h],di

LAB_00453395:
  00453395  5f                    pop       edi
  00453396  5e                    pop       esi
  00453397  5d                    pop       ebp
  00453398  5b                    pop       ebx
  00453399  83c408                add       esp,8
  0045339c  c3                    ret

2 cible(s) de saut interne : 0x004532bf, 0x00453395
