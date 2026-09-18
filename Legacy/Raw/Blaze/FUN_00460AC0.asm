C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460ac0  section .text  offset fichier 0x5fec0  305 octets

  00460ac0  a0fd965e00            mov       al,[5E96FDh]
  00460ac5  53                    push      ebx
  00460ac6  56                    push      esi
  00460ac7  57                    push      edi
  00460ac8  a804                  test      al,4
  00460aca  bb01000000            mov       ebx,1
  00460acf  750c                  jne       short 00460ADDh
  00460ad1  381d2ce55d00          cmp       [5DE52Ch],bl
  00460ad7  0f8410010000          je        near 00460BEDh

LAB_00460add:
  00460add  8b742410              mov       esi,[esp+10h]
  00460ae1  bfc0935e00            mov       edi,5E93C0h

LAB_00460ae6:
  00460ae6  8b07                  mov       eax,[edi]
  00460ae8  a900000080            test      eax,80000000h
  00460aed  746c                  je        short 00460B5Bh
  00460aef  a900000050            test      eax,50000000h
  00460af4  7565                  jne       short 00460B5Bh
  00460af6  8b471c                mov       eax,[edi+1Ch]
  00460af9  8b561c                mov       edx,[esi+1Ch]
  00460afc  8b4f24                mov       ecx,[edi+24h]
  00460aff  2bc2                  sub       eax,edx
  00460b01  8b5624                mov       edx,[esi+24h]
  00460b04  c1f80c                sar       eax,0Ch
  00460b07  2bca                  sub       ecx,edx
  00460b09  8bd0                  mov       edx,eax
  00460b0b  c1f90c                sar       ecx,0Ch
  00460b0e  0fafd0                imul      edx,eax
  00460b11  8bc1                  mov       eax,ecx
  00460b13  0fafc1                imul      eax,ecx
  00460b16  03d0                  add       edx,eax
  00460b18  52                    push      edx
  00460b19  e8f24e0e00            call      00545A10h
  00460b1e  3dc8000000            cmp       eax,0C8h
  00460b23  7d36                  jge       short 00460B5Bh
  00460b25  66837e1000            cmp       word [esi+10h],0
  00460b2a  752f                  jne       short 00460B5Bh
  00460b2c  66895e10              mov       [esi+10h],bx
  00460b30  8b0dcc975e00          mov       ecx,[5E97CCh]
  00460b36  6a00                  push      0
  00460b38  6a00                  push      0
  00460b3a  8b91a8000000          mov       edx,[ecx+0A8h]
  00460b40  6800100000            push      1000h
  00460b45  6800040000            push      400h
  00460b4a  8b02                  mov       eax,[edx]
  00460b4c  660fb64827            movzx     cx,byte [eax+27h]
  00460b51  51                    push      ecx
  00460b52  56                    push      esi
  00460b53  e828750000            call      00468080h
  00460b58  83c418                add       esp,18h

LAB_00460b5b:
  00460b5b  81c79c000000          add       edi,9Ch
  00460b61  81ff94955e00          cmp       edi,5E9594h
  00460b67  0f8e79ffffff          jle       near 00460AE6h
  00460b6d  668b4610              mov       ax,[esi+10h]
  00460b71  6685c0                test      ax,ax
  00460b74  7504                  jne       short 00460B7Ah
  00460b76  66894612              mov       [esi+12h],ax

LAB_00460b7a:
  00460b7a  663bc3                cmp       ax,bx
  00460b7d  754d                  jne       short 00460BCCh
  00460b7f  66837e1200            cmp       word [esi+12h],0
  00460b84  751b                  jne       short 00460BA1h
  00460b86  bf10000000            mov       edi,10h

LAB_00460b8b:
  00460b8b  6a00                  push      0
  00460b8d  6a00                  push      0
  00460b8f  6aff                  push      0FFFFFFFFh
  00460b91  6a00                  push      0
  00460b93  6a35                  push      35h
  00460b95  56                    push      esi
  00460b96  e875750000            call      00468110h
  00460b9b  83c418                add       esp,18h
  00460b9e  4f                    dec       edi
  00460b9f  75ea                  jne       short 00460B8Bh

LAB_00460ba1:
  00460ba1  66ff4612              inc       word [esi+12h]
  00460ba5  668b4612              mov       ax,[esi+12h]
  00460ba9  8b0e                  mov       ecx,[esi]
  00460bab  84c3                  test      bl,al
  00460bad  7505                  jne       short 00460BB4h
  00460baf  80cd40                or        ch,40h
  00460bb2  eb03                  jmp       short 00460BB7h

LAB_00460bb4:
  00460bb4  80e5bf                and       ch,0BFh

LAB_00460bb7:
  00460bb7  663d0e00              cmp       ax,0Eh
  00460bbb  890e                  mov       [esi],ecx
  00460bbd  7c0d                  jl        short 00460BCCh
  00460bbf  8bc1                  mov       eax,ecx
  00460bc1  66c746100200          mov       word [esi+10h],2
  00460bc7  80cc60                or        ah,60h
  00460bca  8906                  mov       [esi],eax

LAB_00460bcc:
  00460bcc  66837e1002            cmp       word [esi+10h],2
  00460bd1  751a                  jne       short 00460BEDh
  00460bd3  0fbf5614              movsx     edx,word [esi+14h]
  00460bd7  a1cc975e00            mov       eax,[5E97CCh]
  00460bdc  52                    push      edx
  00460bdd  56                    push      esi
  00460bde  50                    push      eax
  00460bdf  e8ec310b00            call      00513DD0h
  00460be4  83c40c                add       esp,0Ch
  00460be7  66c746100300          mov       word [esi+10h],3

LAB_00460bed:
  00460bed  5f                    pop       edi
  00460bee  5e                    pop       esi
  00460bef  5b                    pop       ebx
  00460bf0  c3                    ret

10 cible(s) de saut interne : 0x00460add, 0x00460ae6, 0x00460b5b, 0x00460b7a, 0x00460b8b, 0x00460ba1, 0x00460bb4, 0x00460bb7, 0x00460bcc, 0x00460bed
