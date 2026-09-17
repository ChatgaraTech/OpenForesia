C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004442f0  section .text  offset fichier 0x436f0  70 octets

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
  0044432d  881562905e00          mov       [5E9062h],dl
  00444333  33c0                  xor       eax,eax
  00444335  c3                    ret

5 cible(s) de saut interne : 0x00444303, 0x00444311, 0x00444315, 0x0044431f, 0x0044432d
