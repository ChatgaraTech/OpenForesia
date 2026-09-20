C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046a3f0  section .text  offset fichier 0x697f0  432 octets

  0046a3f0  a1d0175700            mov       eax,[5717D0h]
  0046a3f5  53                    push      ebx
  0046a3f6  8b5c2410              mov       ebx,[esp+10h]
  0046a3fa  55                    push      ebp
  0046a3fb  8b6c2410              mov       ebp,[esp+10h]
  0046a3ff  56                    push      esi
  0046a400  8b742410              mov       esi,[esp+10h]
  0046a404  57                    push      edi
  0046a405  83fd01                cmp       ebp,1
  0046a408  8bf8                  mov       edi,eax
  0046a40a  7545                  jne       short 0046A451h
  0046a40c  8bc8                  mov       ecx,eax
  0046a40e  8bd0                  mov       edx,eax
  0046a410  c1e906                shr       ecx,6
  0046a413  c1ea03                shr       edx,3
  0046a416  03c1                  add       eax,ecx
  0046a418  03d0                  add       edx,eax
  0046a41a  8915f4175700          mov       [5717F4h],edx
  0046a420  0fbf4632              movsx     eax,word [esi+32h]
  0046a424  0500040000            add       eax,400h
  0046a429  c705ec17570000000000  mov       dword [5717ECh],0
  0046a433  a3e8175700            mov       [5717E8h],eax
  0046a438  891df0175700          mov       [5717F0h],ebx
  0046a43e  e8fdcc0d00            call      00547140h
  0046a443  99                    cdq
  0046a444  b909000000            mov       ecx,9
  0046a449  f7f9                  idiv      ecx
  0046a44b  8915e4175700          mov       [5717E4h],edx

LAB_0046a451:
  0046a451  83fdff                cmp       ebp,0FFFFFFFFh
  0046a454  7548                  jne       short 0046A49Eh
  0046a456  8bd7                  mov       edx,edi
  0046a458  8bc7                  mov       eax,edi
  0046a45a  c1ea06                shr       edx,6
  0046a45d  c1e803                shr       eax,3
  0046a460  03fa                  add       edi,edx
  0046a462  03c7                  add       eax,edi
  0046a464  f7d8                  neg       eax
  0046a466  a3f4175700            mov       [5717F4h],eax
  0046a46b  0fbf4e32              movsx     ecx,word [esi+32h]
  0046a46f  81e900040000          sub       ecx,400h
  0046a475  c705ec17570000000000  mov       dword [5717ECh],0
  0046a47f  890de8175700          mov       [5717E8h],ecx
  0046a485  891df0175700          mov       [5717F0h],ebx
  0046a48b  e8b0cc0d00            call      00547140h
  0046a490  99                    cdq
  0046a491  b909000000            mov       ecx,9
  0046a496  f7f9                  idiv      ecx
  0046a498  8915e4175700          mov       [5717E4h],edx

LAB_0046a49e:
  0046a49e  85ed                  test      ebp,ebp
  0046a4a0  0f85ef000000          jne       near 0046A595h
  0046a4a6  a1ec175700            mov       eax,[5717ECh]
  0046a4ab  85c0                  test      eax,eax
  0046a4ad  0f85e2000000          jne       near 0046A595h
  0046a4b3  8b3df4175700          mov       edi,[5717F4h]
  0046a4b9  a1e4175700            mov       eax,[5717E4h]
  0046a4be  33d2                  xor       edx,edx
  0046a4c0  85ff                  test      edi,edi
  0046a4c2  0f9ec2                setle     dl
  0046a4c5  48                    dec       eax
  0046a4c6  8bfa                  mov       edi,edx
  0046a4c8  83f8ff                cmp       eax,0FFFFFFFFh
  0046a4cb  a3e4175700            mov       [5717E4h],eax
  0046a4d0  7533                  jne       short 0046A505h
  0046a4d2  e869cc0d00            call      00547140h
  0046a4d7  99                    cdq
  0046a4d8  33c2                  xor       eax,edx
  0046a4da  2bc2                  sub       eax,edx
  0046a4dc  83e007                and       eax,7
  0046a4df  33c2                  xor       eax,edx
  0046a4e1  2bc2                  sub       eax,edx
  0046a4e3  40                    inc       eax
  0046a4e4  a3e4175700            mov       [5717E4h],eax
  0046a4e9  8d443f01              lea       eax,[edi+edi+1]
  0046a4ed  50                    push      eax
  0046a4ee  56                    push      esi
  0046a4ef  e83c010000            call      0046A630h
  0046a4f4  83c408                add       esp,8
  0046a4f7  8d4c3f02              lea       ecx,[edi+edi+2]
  0046a4fb  51                    push      ecx
  0046a4fc  56                    push      esi
  0046a4fd  e82e010000            call      0046A630h
  0046a502  83c408                add       esp,8

LAB_0046a505:
  0046a505  668b15f4175700        mov       dx,[5717F4h]
  0046a50c  66015632              add       [esi+32h],dx
  0046a510  8b0df4175700          mov       ecx,[5717F4h]
  0046a516  668b4632              mov       ax,[esi+32h]
  0046a51a  8b15e8175700          mov       edx,[5717E8h]
  0046a520  85c9                  test      ecx,ecx
  0046a522  7d07                  jge       short 0046A52Bh
  0046a524  0fbff8                movsx     edi,ax
  0046a527  3bfa                  cmp       edi,edx
  0046a529  7e0b                  jle       short 0046A536h

LAB_0046a52b:
  0046a52b  85c9                  test      ecx,ecx
  0046a52d  7e66                  jle       short 0046A595h
  0046a52f  0fbfc0                movsx     eax,ax
  0046a532  3bc2                  cmp       eax,edx
  0046a534  7c5f                  jl        short 0046A595h

LAB_0046a536:
  0046a536  66895632              mov       [esi+32h],dx
  0046a53a  c705ec17570001000000  mov       dword [5717ECh],1
  0046a544  668b4632              mov       ax,[esi+32h]
  0046a548  663d0010              cmp       ax,1000h
  0046a54c  7c09                  jl        short 0046A557h
  0046a54e  0500f0ffff            add       eax,0FFFFF000h
  0046a553  66894632              mov       [esi+32h],ax

LAB_0046a557:
  0046a557  668b4632              mov       ax,[esi+32h]
  0046a55b  6685c0                test      ax,ax
  0046a55e  7d09                  jge       short 0046A569h
  0046a560  0500100000            add       eax,1000h
  0046a565  66894632              mov       [esi+32h],ax

LAB_0046a569:
  0046a569  668b4632              mov       ax,[esi+32h]
  0046a56d  6685c0                test      ax,ax
  0046a570  7417                  je        short 0046A589h
  0046a572  663d0008              cmp       ax,800h
  0046a576  7411                  je        short 0046A589h
  0046a578  8b0df0175700          mov       ecx,[5717F0h]
  0046a57e  c1e10c                shl       ecx,0Ch
  0046a581  894e1c                mov       [esi+1Ch],ecx
  0046a584  5f                    pop       edi
  0046a585  5e                    pop       esi
  0046a586  5d                    pop       ebp
  0046a587  5b                    pop       ebx
  0046a588  c3                    ret

LAB_0046a589:
  0046a589  8b15f0175700          mov       edx,[5717F0h]
  0046a58f  c1e20c                shl       edx,0Ch
  0046a592  895624                mov       [esi+24h],edx

LAB_0046a595:
  0046a595  5f                    pop       edi
  0046a596  5e                    pop       esi
  0046a597  5d                    pop       ebp
  0046a598  5b                    pop       ebx
  0046a599  c3                    ret
  0046a59a  90                    nop
  0046a59b  90                    nop
  0046a59c  90                    nop
  0046a59d  90                    nop
  0046a59e  90                    nop
  0046a59f  90                    nop

9 cible(s) de saut interne : 0x0046a451, 0x0046a49e, 0x0046a505, 0x0046a52b, 0x0046a536, 0x0046a557, 0x0046a569, 0x0046a589, 0x0046a595
