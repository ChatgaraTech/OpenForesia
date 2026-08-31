Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448c40  section .text  offset fichier 0x48040  510 octets

  00448c40  53                    push      ebx
  00448c41  55                    push      ebp
  00448c42  56                    push      esi
  00448c43  8b742410              mov       esi,[esp+10h]
  00448c47  57                    push      edi
  00448c48  8b6e78                mov       ebp,[esi+78h]
  00448c4b  85ed                  test      ebp,ebp
  00448c4d  7505                  jne       short 00448C54h
  00448c4f  bd58e55d00            mov       ebp,5DE558h

LAB_00448c54:
  00448c54  f7450000000050        test      dword [ebp],50000000h
  00448c5b  7523                  jne       short 00448C80h
  00448c5d  8b451c                mov       eax,[ebp+1Ch]
  00448c60  89461c                mov       [esi+1Ch],eax
  00448c63  8b4d60                mov       ecx,[ebp+60h]
  00448c66  81c100500000          add       ecx,5000h
  00448c6c  894e20                mov       [esi+20h],ecx
  00448c6f  8b5524                mov       edx,[ebp+24h]
  00448c72  895624                mov       [esi+24h],edx
  00448c75  8b4500                mov       eax,[ebp]
  00448c78  80cc10                or        ah,10h
  00448c7b  894500                mov       [ebp],eax
  00448c7e  eb06                  jmp       short 00448C86h

LAB_00448c80:
  00448c80  810e00000002          or        dword [esi],2000000h

LAB_00448c86:
  00448c86  55                    push      ebp
  00448c87  e834ff0e00            call      00538BC0h
  00448c8c  8d0440                lea       eax,[eax+eax*2]
  00448c8f  83c404                add       esp,4
  00448c92  8d3c80                lea       edi,[eax+eax*4]
  00448c95  d1e7                  shl       edi,1
  00448c97  81ff00100000          cmp       edi,1000h
  00448c9d  7d05                  jge       short 00448CA4h
  00448c9f  bf00100000            mov       edi,1000h

LAB_00448ca4:
  00448ca4  81ff30750000          cmp       edi,7530h
  00448caa  7e05                  jle       short 00448CB1h
  00448cac  bf30750000            mov       edi,7530h

LAB_00448cb1:
  00448cb1  33c9                  xor       ecx,ecx
  00448cb3  bb01000000            mov       ebx,1
  00448cb8  66394e10              cmp       [esi+10h],cx
  00448cbc  752e                  jne       short 00448CECh
  00448cbe  8a465c                mov       al,[esi+5Ch]
  00448cc1  c646042b              mov       byte [esi+4],2Bh
  00448cc5  88460a                mov       [esi+0Ah],al
  00448cc8  884e0b                mov       [esi+0Bh],cl
  00448ccb  66895e10              mov       [esi+10h],bx
  00448ccf  66894e16              mov       [esi+16h],cx
  00448cd3  66894e28              mov       [esi+28h],cx
  00448cd7  66c7462a0010          mov       word [esi+2Ah],1000h
  00448cdd  66894e2c              mov       [esi+2Ch],cx
  00448ce1  66894e32              mov       [esi+32h],cx
  00448ce5  66898e98000000        mov       [esi+98h],cx

LAB_00448cec:
  00448cec  66395e10              cmp       [esi+10h],bx
  00448cf0  0f85a4000000          jne       near 00448D9Ah
  00448cf6  66398e98000000        cmp       [esi+98h],cx
  00448cfd  7522                  jne       short 00448D21h
  00448cff  6800120000            push      1200h
  00448d04  6800120000            push      1200h
  00448d09  687f020000            push      27Fh
  00448d0e  6a2c                  push      2Ch
  00448d10  e88b410f00            call      0053CEA0h
  00448d15  83c410                add       esp,10h
  00448d18  66ff8698000000        inc       word [esi+98h]
  00448d1f  33c9                  xor       ecx,ecx

LAB_00448d21:
  00448d21  66399e98000000        cmp       [esi+98h],bx
  00448d28  7522                  jne       short 00448D4Ch
  00448d2a  6800120000            push      1200h
  00448d2f  6800120000            push      1200h
  00448d34  6800020000            push      200h
  00448d39  6a2f                  push      2Fh
  00448d3b  e860410f00            call      0053CEA0h
  00448d40  83c410                add       esp,10h
  00448d43  66ff8698000000        inc       word [esi+98h]
  00448d4a  33c9                  xor       ecx,ecx

LAB_00448d4c:
  00448d4c  668b4632              mov       ax,[esi+32h]
  00448d50  bb00080000            mov       ebx,800h
  00448d55  0fbfd0                movsx     edx,ax
  00448d58  2bda                  sub       ebx,edx
  00448d5a  c1fb02                sar       ebx,2
  00448d5d  03d8                  add       ebx,eax
  00448d5f  668b4628              mov       ax,[esi+28h]
  00448d63  0fbfd0                movsx     edx,ax
  00448d66  66895e32              mov       [esi+32h],bx
  00448d6a  8bdf                  mov       ebx,edi
  00448d6c  2bda                  sub       ebx,edx
  00448d6e  c1fb02                sar       ebx,2
  00448d71  03d8                  add       ebx,eax
  00448d73  668b462c              mov       ax,[esi+2Ch]
  00448d77  0fbfd0                movsx     edx,ax
  00448d7a  2bfa                  sub       edi,edx
  00448d7c  66895e28              mov       [esi+28h],bx
  00448d80  c1ff02                sar       edi,2
  00448d83  03f8                  add       edi,eax
  00448d85  66837e160e            cmp       word [esi+16h],0Eh
  00448d8a  66897e2c              mov       [esi+2Ch],di
  00448d8e  7c0a                  jl        short 00448D9Ah
  00448d90  66c746100200          mov       word [esi+10h],2
  00448d96  66894e16              mov       [esi+16h],cx

LAB_00448d9a:
  00448d9a  66837e1002            cmp       word [esi+10h],2
  00448d9f  b803000000            mov       eax,3
  00448da4  750b                  jne       short 00448DB1h
  00448da6  66837e1664            cmp       word [esi+16h],64h
  00448dab  7e04                  jle       short 00448DB1h
  00448dad  66894610              mov       [esi+10h],ax

LAB_00448db1:
  00448db1  66394610              cmp       [esi+10h],ax
  00448db5  7536                  jne       short 00448DEDh
  00448db7  668b4632              mov       ax,[esi+32h]
  00448dbb  0fbfc8                movsx     ecx,ax
  00448dbe  f7d9                  neg       ecx
  00448dc0  c1f902                sar       ecx,2
  00448dc3  03c8                  add       ecx,eax
  00448dc5  668b4628              mov       ax,[esi+28h]
  00448dc9  0fbfd0                movsx     edx,ax
  00448dcc  f7da                  neg       edx
  00448dce  c1fa03                sar       edx,3
  00448dd1  03d0                  add       edx,eax
  00448dd3  668b462c              mov       ax,[esi+2Ch]
  00448dd7  66894e32              mov       [esi+32h],cx
  00448ddb  66895628              mov       [esi+28h],dx
  00448ddf  0fbfc8                movsx     ecx,ax
  00448de2  f7d9                  neg       ecx
  00448de4  c1f903                sar       ecx,3
  00448de7  03c8                  add       ecx,eax
  00448de9  66894e2c              mov       [esi+2Ch],cx

LAB_00448ded:
  00448ded  66ff4616              inc       word [esi+16h]
  00448df1  66817e169600          cmp       word [esi+16h],96h
  00448df7  7c13                  jl        short 00448E0Ch
  00448df9  8b0e                  mov       ecx,[esi]
  00448dfb  81e1ffffff7f          and       ecx,7FFFFFFFh
  00448e01  890e                  mov       [esi],ecx
  00448e03  8b4500                mov       eax,[ebp]
  00448e06  80e4ef                and       ah,0EFh
  00448e09  894500                mov       [ebp],eax

LAB_00448e0c:
  00448e0c  5f                    pop       edi
  00448e0d  5e                    pop       esi
  00448e0e  5d                    pop       ebp
  00448e0f  5b                    pop       ebx
  00448e10  c3                    ret
  00448e11  90                    nop
  00448e12  90                    nop
  00448e13  90                    nop
  00448e14  90                    nop
  00448e15  90                    nop
  00448e16  90                    nop
  00448e17  90                    nop
  00448e18  90                    nop
  00448e19  90                    nop
  00448e1a  90                    nop
  00448e1b  90                    nop
  00448e1c  90                    nop
  00448e1d  90                    nop
  00448e1e  90                    nop
  00448e1f  90                    nop
  00448e20  53                    push      ebx
  00448e21  56                    push      esi
  00448e22  8b74240c              mov       esi,[esp+0Ch]
  00448e26  33db                  xor       ebx,ebx
  00448e28  66395e10              cmp       [esi+10h],bx
  00448e2c  7577                  jne       short 00448EA5h
  00448e2e  8a465c                mov       al,[esi+5Ch]
  00448e31  c7463c28155500        mov       dword [esi+3Ch],551528h
  00448e38  88460a                mov       [esi+0Ah],al
  00448e3b  b8a00f                (bad)
  (octet invalide — la plage demandee deborde probablement du code)
