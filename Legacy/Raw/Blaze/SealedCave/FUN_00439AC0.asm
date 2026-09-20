C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00439ac0  section .text  offset fichier 0x38ec0  600 octets

  00439ac0  56                    push      esi
  00439ac1  8b74240c              mov       esi,[esp+0Ch]
  00439ac5  57                    push      edi
  00439ac6  8b7c240c              mov       edi,[esp+0Ch]
  00439aca  56                    push      esi
  00439acb  57                    push      edi
  00439acc  e8dfc9ffff            call      004364B0h
  00439ad1  83c408                add       esp,8
  00439ad4  6685c0                test      ax,ax
  00439ad7  751d                  jne       short 00439AF6h
  00439ad9  8b442414              mov       eax,[esp+14h]
  00439add  50                    push      eax
  00439ade  56                    push      esi
  00439adf  57                    push      edi
  00439ae0  e8cb191000            call      0053B4B0h
  00439ae5  83c40c                add       esp,0Ch
  00439ae8  33c9                  xor       ecx,ecx
  00439aea  6685c0                test      ax,ax
  00439aed  0f95c1                setne     cl
  00439af0  668bc1                mov       ax,cx
  00439af3  5f                    pop       edi
  00439af4  5e                    pop       esi
  00439af5  c3                    ret

LAB_00439af6:
  00439af6  5f                    pop       edi
  00439af7  66b80100              mov       ax,1
  00439afb  5e                    pop       esi
  00439afc  c3                    ret
  00439afd  90                    nop
  00439afe  90                    nop
  00439aff  90                    nop
  00439b00  56                    push      esi
  00439b01  8b742408              mov       esi,[esp+8]
  00439b05  57                    push      edi
  00439b06  8b7e74                mov       edi,[esi+74h]
  00439b09  8b565c                mov       edx,[esi+5Ch]
  00439b0c  0fbf4e32              movsx     ecx,word [esi+32h]
  00439b10  8b4720                mov       eax,[edi+20h]
  00439b13  0500300700            add       eax,73000h
  00439b18  894620                mov       [esi+20h],eax
  00439b1b  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  00439b22  50                    push      eax
  00439b23  e8f4be1000            call      00545A1Ch
  00439b28  8b571c                mov       edx,[edi+1Ch]
  00439b2b  8bc8                  mov       ecx,eax
  00439b2d  c1e103                shl       ecx,3
  00439b30  2bc8                  sub       ecx,eax
  00439b32  8d044a                lea       eax,[edx+ecx*2]
  00439b35  8b565c                mov       edx,[esi+5Ch]
  00439b38  0fbf4e32              movsx     ecx,word [esi+32h]
  00439b3c  89461c                mov       [esi+1Ch],eax
  00439b3f  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  00439b46  50                    push      eax
  00439b47  e8cabe1000            call      00545A16h
  00439b4c  8b5724                mov       edx,[edi+24h]
  00439b4f  8bc8                  mov       ecx,eax
  00439b51  c1e103                shl       ecx,3
  00439b54  2bc8                  sub       ecx,eax
  00439b56  66837e1000            cmp       word [esi+10h],0
  00439b5b  bf01000000            mov       edi,1
  00439b60  8d044a                lea       eax,[edx+ecx*2]
  00439b63  894624                mov       [esi+24h],eax
  00439b66  7536                  jne       short 00439B9Eh
  00439b68  c7463c18115500        mov       dword [esi+3Ch],551118h
  00439b6f  e8ccd51000            call      00547140h
  00439b74  99                    cdq
  00439b75  b903000000            mov       ecx,3
  00439b7a  c6460a02              mov       byte [esi+0Ah],2
  00439b7e  f7f9                  idiv      ecx
  00439b80  b8a00f0000            mov       eax,0FA0h
  00439b85  c6460b00              mov       byte [esi+0Bh],0
  00439b89  66894628              mov       [esi+28h],ax
  00439b8d  6689462a              mov       [esi+2Ah],ax
  00439b91  66897e10              mov       [esi+10h],di
  00439b95  66c746120b00          mov       word [esi+12h],0Bh
  00439b9b  885640                mov       [esi+40h],dl

LAB_00439b9e:
  00439b9e  66397e10              cmp       [esi+10h],di
  00439ba2  7526                  jne       short 00439BCAh
  00439ba4  8a5640                mov       dl,[esi+40h]
  00439ba7  66ff4e12              dec       word [esi+12h]
  00439bab  668b4612              mov       ax,[esi+12h]
  00439baf  fec2                  inc       dl
  00439bb1  8aca                  mov       cl,dl
  00439bb3  885640                mov       [esi+40h],dl
  00439bb6  80f903                cmp       cl,3
  00439bb9  7204                  jb        short 00439BBFh
  00439bbb  c6464000              mov       byte [esi+40h],0

LAB_00439bbf:
  00439bbf  6685c0                test      ax,ax
  00439bc2  7f06                  jg        short 00439BCAh
  00439bc4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00439bca:
  00439bca  5f                    pop       edi
  00439bcb  5e                    pop       esi
  00439bcc  c3                    ret
  00439bcd  90                    nop
  00439bce  90                    nop
  00439bcf  90                    nop
  00439bd0  53                    push      ebx
  00439bd1  55                    push      ebp
  00439bd2  56                    push      esi
  00439bd3  8b742410              mov       esi,[esp+10h]
  00439bd7  57                    push      edi
  00439bd8  8b466c                mov       eax,[esi+6Ch]
  00439bdb  8bf8                  mov       edi,eax
  00439bdd  85ff                  test      edi,edi
  00439bdf  7505                  jne       short 00439BE6h
  00439be1  bf58e55d00            mov       edi,5DE558h

LAB_00439be6:
  00439be6  894674                mov       [esi+74h],eax
  00439be9  8b471c                mov       eax,[edi+1Ch]
  00439bec  8d6e1c                lea       ebp,[esi+1Ch]
  00439bef  bb01000000            mov       ebx,1
  00439bf4  894500                mov       [ebp],eax
  00439bf7  8b4f20                mov       ecx,[edi+20h]
  00439bfa  81c100000500          add       ecx,50000h
  00439c00  66837e1000            cmp       word [esi+10h],0
  00439c05  894e20                mov       [esi+20h],ecx
  00439c08  8b5724                mov       edx,[edi+24h]
  00439c0b  895624                mov       [esi+24h],edx
  00439c0e  668b4732              mov       ax,[edi+32h]
  00439c12  66894632              mov       [esi+32h],ax
  00439c16  7546                  jne       short 00439C5Eh
  00439c18  668b4716              mov       ax,[edi+16h]
  00439c1c  6685c0                test      ax,ax
  00439c1f  740e                  je        short 00439C2Fh
  00439c21  663bc3                cmp       ax,bx
  00439c24  7409                  je        short 00439C2Fh
  00439c26  66899e9a000000        mov       [esi+9Ah],bx
  00439c2d  eb09                  jmp       short 00439C38h

LAB_00439c2f:
  00439c2f  66c7869a0000000200    mov       word [esi+9Ah],2

LAB_00439c38:
  00439c38  6a00                  push      0
  00439c3a  6a00                  push      0
  00439c3c  6800100000            push      1000h
  00439c41  68fd060000            push      6FDh
  00439c46  6a1f                  push      1Fh
  00439c48  56                    push      esi
  00439c49  66895e10              mov       [esi+10h],bx
  00439c4d  e82ee40200            call      00468080h
  00439c52  83c418                add       esp,18h
  00439c55  66c786980000000000    mov       word [esi+98h],0

LAB_00439c5e:
  00439c5e  66399e98000000        cmp       [esi+98h],bx
  00439c65  7526                  jne       short 00439C8Dh
  00439c67  66399e9a000000        cmp       [esi+9Ah],bx
  00439c6e  6a00                  push      0
  00439c70  6a00                  push      0
  00439c72  6800080000            push      800h
  00439c77  6800100000            push      1000h
  00439c7c  7504                  jne       short 00439C82h
  00439c7e  6a23                  push      23h
  00439c80  eb02                  jmp       short 00439C84h

LAB_00439c82:
  00439c82  6a20                  push      20h

LAB_00439c84:
  00439c84  56                    push      esi
  00439c85  e8f6e30200            call      00468080h
  00439c8a  83c418                add       esp,18h

LAB_00439c8d:
  00439c8d  6683be9800000005      cmp       word [esi+98h],5
  00439c95  7509                  jne       short 00439CA0h
  00439c97  66c786980000000000    mov       word [esi+98h],0

LAB_00439ca0:
  00439ca0  66ff8698000000        inc       word [esi+98h]
  00439ca7  8a4743                mov       al,[edi+43h]
  00439caa  3ac3                  cmp       al,bl
  00439cac  7506                  jne       short 00439CB4h
  00439cae  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00439cb4:
  00439cb4  8b3f                  mov       edi,[edi]
  00439cb6  f7c700000040          test      edi,40000000h
  00439cbc  7508                  jne       short 00439CC6h
  00439cbe  f7c700000080          test      edi,80000000h
  00439cc4  7506                  jne       short 00439CCCh

LAB_00439cc6:
  00439cc6  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00439ccc:
  00439ccc  0fbf4e32              movsx     ecx,word [esi+32h]
  00439cd0  51                    push      ecx
  00439cd1  e846bd1000            call      00545A1Ch
  00439cd6  8b7d00                mov       edi,[ebp]
  00439cd9  8d0480                lea       eax,[eax+eax*4]
  00439cdc  8d14c0                lea       edx,[eax+eax*8]
  00439cdf  0fbf4632              movsx     eax,word [esi+32h]
  00439ce3  c1e202                shl       edx,2
  00439ce6  03fa                  add       edi,edx
  00439ce8  50                    push      eax
  00439ce9  897d00                mov       [ebp],edi
  00439cec  e825bd1000            call      00545A16h
  00439cf1  8b7e24                mov       edi,[esi+24h]
  00439cf4  8b5620                mov       edx,[esi+20h]
  00439cf7  8d0480                lea       eax,[eax+eax*4]
  00439cfa  6aff                  push      0FFFFFFFFh
  00439cfc  6a00                  push      0
  00439cfe  68df000000            push      0DFh
  00439d03  8d0cc0                lea       ecx,[eax+eax*8]
  00439d06  6a00                  push      0
  00439d08  c1e102                shl       ecx,2
  00439d0b  03f9                  add       edi,ecx
  00439d0d  81c20060ffff          add       edx,0FFFF6000h
  00439d13  55                    push      ebp
  00439d14  56                    push      esi
  00439d15  897e24                mov       [esi+24h],edi

15 cible(s) de saut interne : 0x00439af6, 0x00439b9e, 0x00439bbf, 0x00439bca, 0x00439be6, 0x00439c2f, 0x00439c38, 0x00439c5e, 0x00439c82, 0x00439c84, 0x00439c8d, 0x00439ca0, 0x00439cb4, 0x00439cc6, 0x00439ccc
