C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004441f0  section .text  offset fichier 0x435f0  320 octets

  004441f0  33c0                  xor       eax,eax
  004441f2  c3                    ret
  004441f3  90                    nop
  004441f4  90                    nop
  004441f5  90                    nop
  004441f6  90                    nop
  004441f7  90                    nop
  004441f8  90                    nop
  004441f9  90                    nop
  004441fa  90                    nop
  004441fb  90                    nop
  004441fc  90                    nop
  004441fd  90                    nop
  004441fe  90                    nop
  004441ff  90                    nop
  00444200  53                    push      ebx
  00444201  55                    push      ebp
  00444202  56                    push      esi
  00444203  57                    push      edi
  00444204  33ed                  xor       ebp,ebp
  00444206  33ff                  xor       edi,edi
  00444208  bb00000004            mov       ebx,4000000h

LAB_0044420d:
  0044420d  0fbfc7                movsx     eax,di
  00444210  8d3480                lea       esi,[eax+eax*4]
  00444213  c1e603                shl       esi,3
  00444216  2bf0                  sub       esi,eax
  00444218  c1e602                shl       esi,2
  0044421b  8b86c0935e00          mov       eax,[esi+5E93C0h]
  00444221  a900000080            test      eax,80000000h
  00444226  0f84ad000000          je        near 004442D9h
  0044422c  a900000050            test      eax,50000000h
  00444231  0f85a2000000          jne       near 004442D9h
  00444237  6683ff01              cmp       di,1
  0044423b  751f                  jne       short 0044425Ch
  0044423d  8b1580945e00          mov       edx,[5E9480h]
  00444243  685c945e00            push      5E945Ch
  00444248  81ea00400600          sub       edx,64000h
  0044424e  891580945e00          mov       [5E9480h],edx
  00444254  e8e7f60e00            call      00533940h
  00444259  83c404                add       esp,4

LAB_0044425c:
  0044425c  6683ff02              cmp       di,2
  00444260  752e                  jne       short 00444290h
  00444262  8b0d1c955e00          mov       ecx,[5E951Ch]
  00444268  8b1514955e00          mov       edx,[5E9514h]
  0044426e  b800200300            mov       eax,32000h
  00444273  68f8945e00            push      5E94F8h
  00444278  2bc8                  sub       ecx,eax
  0044427a  2bd0                  sub       edx,eax
  0044427c  890d1c955e00          mov       [5E951Ch],ecx
  00444282  891514955e00          mov       [5E9514h],edx
  00444288  e8b3f60e00            call      00533940h
  0044428d  83c404                add       esp,4

LAB_00444290:
  00444290  6683ff03              cmp       di,3
  00444294  752e                  jne       short 004442C4h
  00444296  8b0db8955e00          mov       ecx,[5E95B8h]
  0044429c  8b15b0955e00          mov       edx,[5E95B0h]
  004442a2  b800200300            mov       eax,32000h
  004442a7  6894955e00            push      5E9594h
  004442ac  2bc8                  sub       ecx,eax
  004442ae  03d0                  add       edx,eax
  004442b0  890db8955e00          mov       [5E95B8h],ecx
  004442b6  8915b0955e00          mov       [5E95B0h],edx
  004442bc  e87ff60e00            call      00533940h
  004442c1  83c404                add       esp,4

LAB_004442c4:
  004442c4  6689aef2935e00        mov       [esi+5E93F2h],bp
  004442cb  8b86c0935e00          mov       eax,[esi+5E93C0h]
  004442d1  0bc3                  or        eax,ebx
  004442d3  8986c0935e00          mov       [esi+5E93C0h],eax

LAB_004442d9:
  004442d9  47                    inc       edi
  004442da  6683ff03              cmp       di,3
  004442de  0f8e29ffffff          jle       near 0044420Dh
  004442e4  5f                    pop       edi
  004442e5  5e                    pop       esi
  004442e6  5d                    pop       ebp
  004442e7  33c0                  xor       eax,eax
  004442e9  5b                    pop       ebx
  004442ea  c3                    ret
  004442eb  90                    nop
  004442ec  90                    nop
  004442ed  90                    nop
  004442ee  90                    nop
  004442ef  90                    nop
  004442f0  8a0d00905e00          mov       cl,[5E9000h]
  004442f6  33d2                  xor       edx,edx
  004442f8  8ac1                  mov       al,cl
  004442fa  2410                  and       al,10h
  004442fc  7505                  jne       short 00444303h
  004442fe  f6c120                test      cl,20h
  00444301  7500                  jne       short 00444303h

LAB_00444303:
  00444303  84c0                  test      al,al
  00444305  750e                  jne       short 00444315h
  00444307  f6c120                test      cl,20h
  0044430a  7405                  je        short 00444311h
  0044430c  ba01000000            mov       edx,1

LAB_00444311:
  00444311  84c0                  test      al,al
  00444313  7418                  je        short 0044432Dh

LAB_00444315:
  00444315  f6c120                test      cl,20h
  00444318  7505                  jne       short 0044431Fh
  0044431a  ba02000000            mov       edx,2

LAB_0044431f:
  0044431f  84c0                  test      al,al
  00444321  740a                  je        short 0044432Dh
  00444323  f6c120                test      cl,20h
  00444326  7405                  je        short 0044432Dh
  00444328  ba03000000            mov       edx,3

LAB_0044432d:
  0044432d  881562                (bad)
  (octet invalide — la plage demandee deborde probablement du code)
