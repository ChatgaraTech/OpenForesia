Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046ea40  section .text  offset fichier 0x6de40  208 octets

  0046ea40  8b0d68e05d00          mov       ecx,[5DE068h]
  0046ea46  56                    push      esi
  0046ea47  8b742408              mov       esi,[esp+8]
  0046ea4b  8b156ce05d00          mov       edx,[5DE06Ch]
  0046ea51  c1f90c                sar       ecx,0Ch
  0046ea54  8b461c                mov       eax,[esi+1Ch]
  0046ea57  57                    push      edi
  0046ea58  c1f80c                sar       eax,0Ch
  0046ea5b  8b3d70e05d00          mov       edi,[5DE070h]
  0046ea61  2bc1                  sub       eax,ecx
  0046ea63  8b4e20                mov       ecx,[esi+20h]
  0046ea66  c1f90c                sar       ecx,0Ch
  0046ea69  c1fa0c                sar       edx,0Ch
  0046ea6c  2bca                  sub       ecx,edx
  0046ea6e  8b5624                mov       edx,[esi+24h]
  0046ea71  c1ff0c                sar       edi,0Ch
  0046ea74  c1fa0c                sar       edx,0Ch
  0046ea77  2bd7                  sub       edx,edi
  0046ea79  8bf8                  mov       edi,eax
  0046ea7b  0faff8                imul      edi,eax
  0046ea7e  8bc1                  mov       eax,ecx
  0046ea80  0fafc1                imul      eax,ecx
  0046ea83  8bca                  mov       ecx,edx
  0046ea85  03f8                  add       edi,eax
  0046ea87  0fafca                imul      ecx,edx
  0046ea8a  03f9                  add       edi,ecx
  0046ea8c  57                    push      edi
  0046ea8d  e87e6f0d00            call      00545A10h
  0046ea92  8b16                  mov       edx,[esi]
  0046ea94  80ce60                or        dh,60h
  0046ea97  8916                  mov       [esi],edx
  0046ea99  33d2                  xor       edx,edx
  0046ea9b  3d96000000            cmp       eax,96h
  0046eaa0  7e06                  jle       short 0046EAA8h
  0046eaa2  66395610              cmp       [esi+10h],dx
  0046eaa6  745a                  je        short 0046EB02h

LAB_0046eaa8:
  0046eaa8  66395610              cmp       [esi+10h],dx
  0046eaac  b801000000            mov       eax,1
  0046eab1  7508                  jne       short 0046EABBh
  0046eab3  66894610              mov       [esi+10h],ax
  0046eab7  66895612              mov       [esi+12h],dx

LAB_0046eabb:
  0046eabb  66394610              cmp       [esi+10h],ax
  0046eabf  b902000000            mov       ecx,2
  0046eac4  751a                  jne       short 0046EAE0h
  0046eac6  66ff4612              inc       word [esi+12h]
  0046eaca  66837e120a            cmp       word [esi+12h],0Ah
  0046eacf  7c0f                  jl        short 0046EAE0h
  0046ead1  66894e10              mov       [esi+10h],cx
  0046ead5  66895612              mov       [esi+12h],dx
  0046ead9  c7462400500000        mov       dword [esi+24h],5000h

LAB_0046eae0:
  0046eae0  66394e10              cmp       [esi+10h],cx
  0046eae4  751c                  jne       short 0046EB02h
  0046eae6  66ff4612              inc       word [esi+12h]
  0046eaea  66837e120a            cmp       word [esi+12h],0Ah
  0046eaef  7c11                  jl        short 0046EB02h
  0046eaf1  66c746100300          mov       word [esi+10h],3
  0046eaf7  66895612              mov       [esi+12h],dx
  0046eafb  c7462400501900        mov       dword [esi+24h],195000h

LAB_0046eb02:
  0046eb02  5f                    pop       edi
  0046eb03  5e                    pop       esi
  0046eb04  c3                    ret
  0046eb05  90                    nop
  0046eb06  90                    nop
  0046eb07  90                    nop
  0046eb08  90                    nop
  0046eb09  90                    nop
  0046eb0a  90                    nop
  0046eb0b  90                    nop
  0046eb0c  90                    nop
  0046eb0d  90                    nop
  0046eb0e  90                    nop
  0046eb0f  90                    nop

4 cible(s) de saut interne : 0x0046eaa8, 0x0046eabb, 0x0046eae0, 0x0046eb02
