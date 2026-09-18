C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00420860  section .text  offset fichier 0x1fc60  169 octets

  00420860  56                    push      esi
  00420861  8b742408              mov       esi,[esp+8]
  00420865  66837e1000            cmp       word [esi+10h],0
  0042086a  7536                  jne       short 004208A2h
  0042086c  66c746100100          mov       word [esi+10h],1
  00420872  66c746120a00          mov       word [esi+12h],0Ah
  00420878  a1cc975e00            mov       eax,[5E97CCh]
  0042087d  6800100000            push      1000h
  00420882  6800100000            push      1000h
  00420887  6800020000            push      200h
  0042088c  8b88a8000000          mov       ecx,[eax+0A8h]
  00420892  33c0                  xor       eax,eax
  00420894  8b11                  mov       edx,[ecx]
  00420896  8a423e                mov       al,[edx+3Eh]
  00420899  50                    push      eax
  0042089a  e801c61100            call      0053CEA0h
  0042089f  83c410                add       esp,10h

LAB_004208a2:
  004208a2  0fbf4e12              movsx     ecx,word [esi+12h]
  004208a6  890dace05d00          mov       [5DE0ACh],ecx
  004208ac  668b4612              mov       ax,[esi+12h]
  004208b0  6685c0                test      ax,ax
  004208b3  7e05                  jle       short 004208BAh
  004208b5  48                    dec       eax
  004208b6  66894612              mov       [esi+12h],ax

LAB_004208ba:
  004208ba  668b4612              mov       ax,[esi+12h]
  004208be  6685c0                test      ax,ax
  004208c1  7d05                  jge       short 004208C8h
  004208c3  40                    inc       eax
  004208c4  66894612              mov       [esi+12h],ax

LAB_004208c8:
  004208c8  668b4612              mov       ax,[esi+12h]
  004208cc  66f7d8                neg       ax
  004208cf  66894612              mov       [esi+12h],ax
  004208d3  7532                  jne       short 00420907h
  004208d5  8b0e                  mov       ecx,[esi]
  004208d7  33c0                  xor       eax,eax
  004208d9  81e1ffffff7f          and       ecx,7FFFFFFFh
  004208df  6a15                  push      15h
  004208e1  890e                  mov       [esi],ecx
  004208e3  a038e55d00            mov       al,[5DE538h]
  004208e8  8b0dcc975e00          mov       ecx,[5E97CCh]
  004208ee  8d1480                lea       edx,[eax+eax*4]
  004208f1  c1e203                shl       edx,3
  004208f4  2bd0                  sub       edx,eax
  004208f6  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004208fd  50                    push      eax
  004208fe  51                    push      ecx
  004208ff  e8cc340f00            call      00513DD0h
  00420904  83c40c                add       esp,0Ch

LAB_00420907:
  00420907  5e                    pop       esi
  00420908  c3                    ret

4 cible(s) de saut interne : 0x004208a2, 0x004208ba, 0x004208c8, 0x00420907
