C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00462990  section .text  offset fichier 0x61d90  237 octets

  00462990  56                    push      esi
  00462991  8b742408              mov       esi,[esp+8]
  00462995  668b4612              mov       ax,[esi+12h]
  00462999  6685c0                test      ax,ax
  0046299c  750e                  jne       short 004629ACh
  0046299e  0fbf4e30              movsx     ecx,word [esi+30h]
  004629a2  c1f908                sar       ecx,8
  004629a5  894e44                mov       [esi+44h],ecx
  004629a8  66894630              mov       [esi+30h],ax

LAB_004629ac:
  004629ac  40                    inc       eax
  004629ad  b914000000            mov       ecx,14h
  004629b2  663dc800              cmp       ax,0C8h
  004629b6  66894612              mov       [esi+12h],ax
  004629ba  7e04                  jle       short 004629C0h
  004629bc  66894e12              mov       [esi+12h],cx

LAB_004629c0:
  004629c0  8b06                  mov       eax,[esi]
  004629c2  a900000040            test      eax,40000000h
  004629c7  0f85ae000000          jne       near 00462A7Bh
  004629cd  66837e1000            cmp       word [esi+10h],0
  004629d2  7558                  jne       short 00462A2Ch
  004629d4  80cc20                or        ah,20h
  004629d7  66837e120f            cmp       word [esi+12h],0Fh
  004629dc  66c746140a00          mov       word [esi+14h],0Ah
  004629e2  8906                  mov       [esi],eax
  004629e4  7d52                  jge       short 00462A38h
  004629e6  8b4e44                mov       ecx,[esi+44h]
  004629e9  85c9                  test      ecx,ecx
  004629eb  744b                  je        short 00462A38h
  004629ed  33c0                  xor       eax,eax
  004629ef  6a00                  push      0
  004629f1  a038e55d00            mov       al,[5DE538h]
  004629f6  68b0335600            push      5633B0h
  004629fb  8d1480                lea       edx,[eax+eax*4]
  004629fe  c1e203                shl       edx,3
  00462a01  2bd0                  sub       edx,eax
  00462a03  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00462a0a  8b15cc975e00          mov       edx,[5E97CCh]
  00462a10  50                    push      eax
  00462a11  8b4270                mov       eax,[edx+70h]
  00462a14  8b0c88                mov       ecx,[eax+ecx*4]
  00462a17  51                    push      ecx
  00462a18  e833190e00            call      00544350h
  00462a1d  83c410                add       esp,10h
  00462a20  c705c8995e0040000000  mov       dword [5E99C8h],40h
  00462a2a  eb0c                  jmp       short 00462A38h

LAB_00462a2c:
  00462a2c  66014e14              add       [esi+14h],cx
  00462a30  668b4614              mov       ax,[esi+14h]
  00462a34  66014632              add       [esi+32h],ax

LAB_00462a38:
  00462a38  66837e1001            cmp       word [esi+10h],1
  00462a3d  752b                  jne       short 00462A6Ah
  00462a3f  8b15cc975e00          mov       edx,[5E97CCh]
  00462a45  6a00                  push      0
  00462a47  6a00                  push      0
  00462a49  6800100000            push      1000h
  00462a4e  8b82a8000000          mov       eax,[edx+0A8h]
  00462a54  6800040000            push      400h
  00462a59  8b08                  mov       ecx,[eax]
  00462a5b  660fb65104            movzx     dx,byte [ecx+4]
  00462a60  52                    push      edx
  00462a61  56                    push      esi
  00462a62  e819560000            call      00468080h
  00462a67  83c418                add       esp,18h

LAB_00462a6a:
  00462a6a  66ff4610              inc       word [esi+10h]
  00462a6e  66837e100a            cmp       word [esi+10h],0Ah
  00462a73  7c06                  jl        short 00462A7Bh
  00462a75  810e00200040          or        dword [esi],40002000h

LAB_00462a7b:
  00462a7b  5e                    pop       esi
  00462a7c  c3                    ret

6 cible(s) de saut interne : 0x004629ac, 0x004629c0, 0x00462a2c, 0x00462a38, 0x00462a6a, 0x00462a7b
