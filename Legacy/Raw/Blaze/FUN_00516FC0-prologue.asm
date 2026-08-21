C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00516fc0  section .text  offset fichier 0x1163c0  420 octets

  00516fc0  a1a8335600            mov       eax,[5633A8h]
  00516fc5  83ec60                sub       esp,60h
  00516fc8  53                    push      ebx
  00516fc9  33db                  xor       ebx,ebx
  00516fcb  55                    push      ebp
  00516fcc  56                    push      esi
  00516fcd  3bc3                  cmp       eax,ebx
  00516fcf  57                    push      edi
  00516fd0  7522                  jne       short 00516FF4h
  00516fd2  a028e55d00            mov       al,[5DE528h]
  00516fd7  8a0d2ce55d00          mov       cl,[5DE52Ch]
  00516fdd  8a1520905e00          mov       dl,[5E9020h]
  00516fe3  a214bc5d00            mov       [5DBC14h],al
  00516fe8  880dd0df5d00          mov       [5DDFD0h],cl
  00516fee  881518bc5d00          mov       [5DBC18h],dl

LAB_00516ff4:
  00516ff4  8d442410              lea       eax,[esp+10h]
  00516ff8  8d4c2420              lea       ecx,[esp+20h]
  00516ffc  50                    push      eax
  00516ffd  8d542418              lea       edx,[esp+18h]
  00517001  51                    push      ecx
  00517002  52                    push      edx
  00517003  e8d8310100            call      0052A1E0h
  00517008  8b15b4876000          mov       edx,[6087B4h]
  0051700e  c605906c560000        mov       byte [566C90h],0
  00517015  891df02c5f00          mov       [5F2CF0h],ebx
  0051701b  891d2ce05d00          mov       [5DE02Ch],ebx
  00517021  c605e49a5e0000        mov       byte [5E9AE4h],0
  00517028  891da8e05d00          mov       [5DE0A8h],ebx
  0051702e  891dace05d00          mov       [5DE0ACh],ebx
  00517034  891db0e05d00          mov       [5DE0B0h],ebx
  0051703a  891d24e05d00          mov       [5DE024h],ebx
  00517040  891d28e05d00          mov       [5DE028h],ebx
  00517046  8b3a                  mov       edi,[edx]
  00517048  b900040000            mov       ecx,400h
  0051704d  33c0                  xor       eax,eax
  0051704f  81c700501c00          add       edi,1C5000h
  00517055  f3ab                  rep stosd
  00517057  8b15b4876000          mov       edx,[6087B4h]
  0051705d  b900040000            mov       ecx,400h
  00517062  83c40c                add       esp,0Ch
  00517065  8b3a                  mov       edi,[edx]
  00517067  81c700401c00          add       edi,1C4000h
  0051706d  f3ab                  rep stosd
  0051706f  a034e55d00            mov       al,[5DE534h]
  00517074  8a0d8c6c5600          mov       cl,[566C8Ch]
  0051707a  3ac8                  cmp       cl,al
  0051707c  7412                  je        short 00517090h
  0051707e  33c9                  xor       ecx,ecx
  00517080  a28c6c5600            mov       [566C8Ch],al
  00517085  8ac8                  mov       cl,al
  00517087  51                    push      ecx
  00517088  e8432a0100            call      00529AD0h
  0051708d  83c404                add       esp,4

LAB_00517090:
  00517090  33c0                  xor       eax,eax
  00517092  a038e55d00            mov       al,[5DE538h]
  00517097  8d1480                lea       edx,[eax+eax*4]
  0051709a  c1e203                shl       edx,3
  0051709d  2bd0                  sub       edx,eax
  0051709f  a1f8455600            mov       eax,[5645F8h]
  005170a4  25ff000000            and       eax,0FFh
  005170a9  48                    dec       eax
  005170aa  8d3495c0935e00        lea       esi,[edx*4+5E93C0h]
  005170b1  7438                  je        short 005170EBh
  005170b3  48                    dec       eax
  005170b4  7551                  jne       short 00517107h
  005170b6  e8c59feeff            call      00401080h
  005170bb  a0946c5600            mov       al,[566C94h]
  005170c0  84c0                  test      al,al
  005170c2  740c                  je        short 005170D0h
  005170c4  c605946c560000        mov       byte [566C94h],0
  005170cb  e8309feeff            call      00401000h

LAB_005170d0:
  005170d0  a148e05d00            mov       eax,[5DE048h]
  005170d5  8b0dcc975e00          mov       ecx,[5E97CCh]
  005170db  25ffff0000            and       eax,0FFFFh
  005170e0  50                    push      eax
  005170e1  56                    push      esi
  005170e2  51                    push      ecx
  005170e3  e8e8ccffff            call      00513DD0h
  005170e8  83c40c                add       esp,0Ch

LAB_005170eb:
  005170eb  8b154ce05d00          mov       edx,[5DE04Ch]
  005170f1  a1cc975e00            mov       eax,[5E97CCh]
  005170f6  81e2ffff0000          and       edx,0FFFFh
  005170fc  52                    push      edx
  005170fd  56                    push      esi
  005170fe  50                    push      eax
  005170ff  e8ccccffff            call      00513DD0h
  00517104  83c40c                add       esp,0Ch

LAB_00517107:
  00517107  a060a55f00            mov       al,[5FA560h]
  0051710c  b901000000            mov       ecx,1
  00517111  3ac1                  cmp       al,cl
  00517113  723e                  jb        short 00517153h

LAB_00517115:
  00517115  a1cc975e00            mov       eax,[5E97CCh]
  0051711a  33d2                  xor       edx,edx
  0051711c  8a9160a55f00          mov       dl,[ecx+5FA560h]
  00517122  8b8088000000          mov       eax,[eax+88h]
  00517128  8b0490                mov       eax,[eax+edx*4]
  0051712b  3bc3                  cmp       eax,ebx
  0051712d  7413                  je        short 00517142h
  0051712f  8338ff                cmp       dword [eax],0FFFFFFFFh
  00517132  740e                  je        short 00517142h

LAB_00517134:
  00517134  c60000                mov       byte [eax],0
  00517137  8b5010                mov       edx,[eax+10h]
  0051713a  83c010                add       eax,10h
  0051713d  83faff                cmp       edx,0FFFFFFFFh
  00517140  75f2                  jne       short 00517134h

LAB_00517142:
  00517142  8b1560a55f00          mov       edx,[5FA560h]
  00517148  41                    inc       ecx
  00517149  81e2ff000000          and       edx,0FFh
  0051714f  3bca                  cmp       ecx,edx
  00517151  7ec2                  jle       short 00517115h

LAB_00517153:
  00517153  6890010000            push      190h
  00517158  e84fe90200            call      00545AACh
  0051715d  53                    push      ebx
  0051715e  e8add9feff            call      00504B10h
  00517163  83                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
