C:\Program Files (x86)\dotnet\sdk\10.0.401\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(325,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0051bb00  section .text  offset fichier 0x11af00  448 octets

  0051bb00  e89b91feff            call      00504CA0h
  0051bb05  e906000000            jmp       0051BB10h
  0051bb0a  90                    nop
  0051bb0b  90                    nop
  0051bb0c  90                    nop
  0051bb0d  90                    nop
  0051bb0e  90                    nop
  0051bb0f  90                    nop

LAB_0051bb10:
  0051bb10  83ec20                sub       esp,20h
  0051bb13  8d442400              lea       eax,[esp]
  0051bb17  8d4c2404              lea       ecx,[esp+4]
  0051bb1b  8d542408              lea       edx,[esp+8]
  0051bb1f  53                    push      ebx
  0051bb20  55                    push      ebp
  0051bb21  56                    push      esi
  0051bb22  57                    push      edi
  0051bb23  50                    push      eax
  0051bb24  33db                  xor       ebx,ebx
  0051bb26  51                    push      ecx
  0051bb27  52                    push      edx
  0051bb28  33f6                  xor       esi,esi
  0051bb2a  66c705fc8f5e001c01    mov       word [5E8FFCh],11Ch
  0051bb33  66c705fe8f5e00c07f    mov       word [5E8FFEh],7FC0h
  0051bb3c  881dc0e05d00          mov       [5DE0C0h],bl
  0051bb42  881dc1e05d00          mov       [5DE0C1h],bl
  0051bb48  881dc2e05d00          mov       [5DE0C2h],bl
  0051bb4e  c605c3e05d00e0        mov       byte [5DE0C3h],0E0h
  0051bb55  c605c4e05d0001        mov       byte [5DE0C4h],1
  0051bb5c  881dc5e05d00          mov       [5DE0C5h],bl
  0051bb62  c605c6e05d003f        mov       byte [5DE0C6h],3Fh
  0051bb69  881dc7e05d00          mov       [5DE0C7h],bl
  0051bb6f  e86ce60000            call      0052A1E0h
  0051bb74  33c0                  xor       eax,eax
  0051bb76  33d2                  xor       edx,edx
  0051bb78  a038e55d00            mov       al,[5DE538h]
  0051bb7d  83c40c                add       esp,0Ch
  0051bb80  c60598985e0001        mov       byte [5E9898h],1
  0051bb87  c60508995e0001        mov       byte [5E9908h],1
  0051bb8e  8d0c80                lea       ecx,[eax+eax*4]
  0051bb91  6a26                  push      26h
  0051bb93  c1e103                shl       ecx,3
  0051bb96  2bc8                  sub       ecx,eax
  0051bb98  33c0                  xor       eax,eax
  0051bb9a  8a148dc8935e00        mov       dl,[ecx*4+5E93C8h]
  0051bba1  6bd24c                imul      edx,4Ch
  0051bba4  8a8288915e00          mov       al,[edx+5E9188h]
  0051bbaa  8bf8                  mov       edi,eax
  0051bbac  c1e704                shl       edi,4
  0051bbaf  81c7204a6000          add       edi,604A20h
  0051bbb5  66895f02              mov       [edi+2],bx
  0051bbb9  e812df0000            call      00529AD0h
  0051bbbe  83c404                add       esp,4
  0051bbc1  891d60995e00          mov       [5E9960h],ebx
  0051bbc7  bd49110000            mov       ebp,1149h

LAB_0051bbcc:
  0051bbcc  a160995e00            mov       eax,[5E9960h]
  0051bbd1  83e03f                and       eax,3Fh
  0051bbd4  c1e002                shl       eax,2
  0051bbd7  3d80000000            cmp       eax,80h
  0051bbdc  7e17                  jle       short 0051BBF5h
  0051bbde  66f747020880          test      word [edi+2],8008h
  0051bbe4  b880000000            mov       eax,80h
  0051bbe9  0f85a3000000          jne       near 0051BC92h
  0051bbef  ff0d60995e00          dec       dword [5E9960h]

LAB_0051bbf5:
  0051bbf5  8bc8                  mov       ecx,eax
  0051bbf7  8b15c4975e00          mov       edx,[5E97C4h]
  0051bbfd  c1e108                shl       ecx,8
  0051bc00  03c8                  add       ecx,eax
  0051bc02  66c7442424a000        mov       word [esp+24h],0A0h
  0051bc09  c1e108                shl       ecx,8
  0051bc0c  03c8                  add       ecx,eax
  0051bc0e  66c74424267800        mov       word [esp+26h],78h
  0051bc15  66c74424282014        mov       word [esp+28h],1420h
  0051bc1c  66896c242a            mov       [esp+2Ah],bp
  0051bc21  66895c242c            mov       [esp+2Ch],bx
  0051bc26  894c2420              mov       [esp+20h],ecx
  0051bc2a  8b8294000000          mov       eax,[edx+94h]
  0051bc30  53                    push      ebx
  0051bc31  8d542420              lea       edx,[esp+20h]
  0051bc35  53                    push      ebx
  0051bc36  8b08                  mov       ecx,[eax]
  0051bc38  8b442418              mov       eax,[esp+18h]
  0051bc3c  894c2424              mov       [esp+24h],ecx
  0051bc40  8b4c241c              mov       ecx,[esp+1Ch]
  0051bc44  52                    push      edx
  0051bc45  50                    push      eax
  0051bc46  51                    push      ecx
  0051bc47  e8d4620000            call      00521F20h
  0051bc4c  83c414                add       esp,14h
  0051bc4f  8d54241c              lea       edx,[esp+1Ch]
  0051bc53  89442410              mov       [esp+10h],eax
  0051bc57  52                    push      edx
  0051bc58  50                    push      eax
  0051bc59  8b44241c              mov       eax,[esp+1Ch]
  0051bc5d  50                    push      eax
  0051bc5e  e85d3d0000            call      0051F9C0h
  0051bc63  83c40c                add       esp,0Ch
  0051bc66  8d4c2410              lea       ecx,[esp+10h]
  0051bc6a  89442410              mov       [esp+10h],eax
  0051bc6e  8d542414              lea       edx,[esp+14h]
  0051bc72  6a01                  push      1
  0051bc74  6a03                  push      3
  0051bc76  51                    push      ecx
  0051bc77  8d442424              lea       eax,[esp+24h]
  0051bc7b  52                    push      edx
  0051bc7c  50                    push      eax
  0051bc7d  e8fee50000            call      0052A280h
  0051bc82  83c414                add       esp,14h
  0051bc85  46                    inc       esi
  0051bc86  81fef4010000          cmp       esi,1F4h
  0051bc8c  0f8c3affffff          jl        near 0051BBCCh

LAB_0051bc92:
  0051bc92  6a01                  push      1
  0051bc94  8d4c2414              lea       ecx,[esp+14h]
  0051bc98  6a03                  push      3
  0051bc9a  8d54241c              lea       edx,[esp+1Ch]
  0051bc9e  51                    push      ecx
  0051bc9f  8d442424              lea       eax,[esp+24h]
  0051bca3  52                    push      edx
  0051bca4  50                    push      eax
  0051bca5  891d10e55d00          mov       [5DE510h],ebx
  0051bcab  e8d0e50000            call      0052A280h
  0051bcb0  83c414                add       esp,14h
  0051bcb3  5f                    pop       edi
  0051bcb4  5e                    pop       esi
  0051bcb5  5d                    pop       ebp
  0051bcb6  5b                    pop       ebx
  0051bcb7  83c420                add       esp,20h
  0051bcba  c3                    ret
  0051bcbb  90                    nop
  0051bcbc  90                    nop
  0051bcbd  90                    nop
  0051bcbe  90                    nop
  0051bcbf  90                    nop

4 cible(s) de saut interne : 0x0051bb10, 0x0051bbcc, 0x0051bbf5, 0x0051bc92
