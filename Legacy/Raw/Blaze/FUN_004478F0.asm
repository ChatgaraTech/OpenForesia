Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004478f0  section .text  offset fichier 0x46cf0  416 octets

  004478f0  83ec0c                sub       esp,0Ch
  004478f3  53                    push      ebx
  004478f4  55                    push      ebp
  004478f5  56                    push      esi
  004478f6  8b74241c              mov       esi,[esp+1Ch]
  004478fa  33c0                  xor       eax,eax
  004478fc  66c744240eceff        mov       word [esp+0Eh],0FFCEh
  00447903  8b5e6c                mov       ebx,[esi+6Ch]
  00447906  668944240c            mov       [esp+0Ch],ax
  0044790b  66c74424103200        mov       word [esp+10h],32h
  00447912  66c74424128200        mov       word [esp+12h],82h
  00447919  6689442414            mov       [esp+14h],ax
  0044791e  66c74424166400        mov       word [esp+16h],64h
  00447925  8b4b6c                mov       ecx,[ebx+6Ch]
  00447928  57                    push      edi
  00447929  894e74                mov       [esi+74h],ecx
  0044792c  8b0b                  mov       ecx,[ebx]
  0044792e  f7c100000040          test      ecx,40000000h
  00447934  7406                  je        short 0044793Ch
  00447936  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044793c:
  0044793c  66394610              cmp       [esi+10h],ax
  00447940  7506                  jne       short 00447948h
  00447942  66816612ff7f          and       word [esi+12h],7FFFh

LAB_00447948:
  00447948  8b4e58                mov       ecx,[esi+58h]
  0044794b  8b5624                mov       edx,[esi+24h]
  0044794e  8b4650                mov       eax,[esi+50h]
  00447951  8b6e1c                mov       ebp,[esi+1Ch]
  00447954  8d7e1c                lea       edi,[esi+1Ch]
  00447957  03d1                  add       edx,ecx
  00447959  03e8                  add       ebp,eax
  0044795b  895624                mov       [esi+24h],edx
  0044795e  8bd0                  mov       edx,eax
  00447960  892f                  mov       [edi],ebp
  00447962  c1fa03                sar       edx,3
  00447965  8be8                  mov       ebp,eax
  00447967  6683462890            add       word [esi+28h],0FF90h
  0044796c  2bea                  sub       ebp,edx
  0044796e  6681462c10ff          add       word [esi+2Ch],0FF10h
  00447974  c1f802                sar       eax,2
  00447977  2be8                  sub       ebp,eax
  00447979  8bc1                  mov       eax,ecx
  0044797b  c1f802                sar       eax,2
  0044797e  8bd1                  mov       edx,ecx
  00447980  896e50                mov       [esi+50h],ebp
  00447983  2bd0                  sub       edx,eax
  00447985  8a4638                mov       al,[esi+38h]
  00447988  c1f903                sar       ecx,3
  0044798b  2bd1                  sub       edx,ecx
  0044798d  3c0f                  cmp       al,0Fh
  0044798f  895658                mov       [esi+58h],edx
  00447992  7217                  jb        short 004479ABh
  00447994  8a5639                mov       dl,[esi+39h]
  00447997  8a4e3a                mov       cl,[esi+3Ah]
  0044799a  2c0f                  sub       al,0Fh
  0044799c  884638                mov       [esi+38h],al
  0044799f  b0f1                  mov       al,0F1h
  004479a1  02d0                  add       dl,al
  004479a3  02c8                  add       cl,al
  004479a5  885639                mov       [esi+39h],dl
  004479a8  884e3a                mov       [esi+3Ah],cl

LAB_004479ab:
  004479ab  bd00800000            mov       ebp,8000h
  004479b0  66856e12              test      [esi+12h],bp
  004479b4  7554                  jne       short 00447A0Ah
  004479b6  56                    push      esi
  004479b7  e884bf0e00            call      00533940h
  004479bc  8b8690000000          mov       eax,[esi+90h]
  004479c2  83c404                add       esp,4
  004479c5  a902080000            test      eax,802h
  004479ca  7529                  jne       short 004479F5h
  004479cc  8b4e60                mov       ecx,[esi+60h]
  004479cf  8b4620                mov       eax,[esi+20h]
  004479d2  8d9100e0fcff          lea       edx,[ecx-32000h]
  004479d8  3bc2                  cmp       eax,edx
  004479da  7c19                  jl        short 004479F5h
  004479dc  f6460340              test      byte [esi+3],40h
  004479e0  7513                  jne       short 004479F5h
  004479e2  66856b12              test      [ebx+12h],bp
  004479e6  750d                  jne       short 004479F5h
  004479e8  3bc1                  cmp       eax,ecx
  004479ea  7f1e                  jg        short 00447A0Ah
  004479ec  3bc2                  cmp       eax,edx
  004479ee  7e1a                  jle       short 00447A0Ah
  004479f0  894e20                mov       [esi+20h],ecx
  004479f3  eb15                  jmp       short 00447A0Ah

LAB_004479f5:
  004479f5  b80080ffff            mov       eax,0FFFF8000h
  004479fa  33c9                  xor       ecx,ecx
  004479fc  66094312              or        [ebx+12h],ax
  00447a00  66094612              or        [esi+12h],ax
  00447a04  894e50                mov       [esi+50h],ecx
  00447a07  894e58                mov       [esi+58h],ecx

LAB_00447a0a:
  00447a0a  668b4612              mov       ax,[esi+12h]
  00447a0e  2500400000            and       eax,4000h
  00447a13  6685c0                test      ax,ax
  00447a16  7552                  jne       short 00447A6Ah
  00447a18  f70600006000          test      dword [esi],600000h
  00447a1e  7416                  je        short 00447A36h
  00447a20  8b5674                mov       edx,[esi+74h]
  00447a23  8d4c2410              lea       ecx,[esp+10h]
  00447a27  6a00                  push      0
  00447a29  51                    push      ecx
  00447a2a  57                    push      edi
  00447a2b  52                    push      edx
  00447a2c  e8ff210d00            call      00519C30h
  00447a31  83c410                add       esp,10h
  00447a34  eb12                  jmp       short 00447A48h

LAB_00447a36:
  00447a36  8b4e74                mov       ecx,[esi+74h]
  00447a39  8d442410              lea       eax,[esp+10h]
  00447a3d  50                    push      eax
  00447a3e  57                    push      edi
  00447a3f  51                    push      ecx
  00447a40  e8ab200d00            call      00519AF0h
  00447a45  83c40c                add       esp,0Ch

LAB_00447a48:
  00447a48  8bf8                  mov       edi,eax
  00447a4a  85ff                  test      edi,edi
  00447a4c  741c                  je        short 00447A6Ah
  00447a4e  8b5674                mov       edx,[esi+74h]
  00447a51  57                    push      edi
  00447a52  52                    push      edx
  00447a53  e828210f00            call      00539B80h
  00447a58  8b07                  mov       eax,[edi]
  00447a5a  83c408                add       esp,8
  00447a5d  0c40                  or        al,40h
  00447a5f  66c787960000001400    mov       word [edi+96h],14h
  00447a68  8907                  mov       [edi],eax

LAB_00447a6a:
  00447a6a  66ff4610              inc       word [esi+10h]
  00447a6e  668b4610              mov       ax,[esi+10h]
  00447a72  663d0800              cmp       ax,8
  00447a76  7c0f                  jl        short 00447A87h
  00447a78  66c743100800          mov       word [ebx+10h],8
  00447a7e  8b06                  mov       eax,[esi]
  00447a80  25ffffff7f            and       eax,7FFFFFFFh
  00447a85  8906                  mov       [esi],eax

LAB_00447a87:
  00447a87  5f                    pop       edi
  00447a88  5e                    pop       esi
  00447a89  5d                    pop       ebp
  00447a8a  5b                    pop       ebx
  00447a8b  83c40c                add       esp,0Ch
  00447a8e  c3                    ret
  00447a8f  90                    nop

9 cible(s) de saut interne : 0x0044793c, 0x00447948, 0x004479ab, 0x004479f5, 0x00447a0a, 0x00447a36, 0x00447a48, 0x00447a6a, 0x00447a87
