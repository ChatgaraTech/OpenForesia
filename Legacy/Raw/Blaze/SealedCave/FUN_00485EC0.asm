Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485ec0  section .text  offset fichier 0x852c0  144 octets

  00485ec0  83ec0c                sub       esp,0Ch
  00485ec3  56                    push      esi
  00485ec4  8b742414              mov       esi,[esp+14h]
  00485ec8  b864000000            mov       eax,64h
  00485ecd  57                    push      edi
  00485ece  804e3604              or        byte [esi+36h],4
  00485ed2  668944240c            mov       [esp+0Ch],ax
  00485ed7  6689442412            mov       [esp+12h],ax
  00485edc  8b4638                mov       eax,[esi+38h]
  00485edf  3d40404000            cmp       eax,404040h
  00485ee4  66c74424080000        mov       word [esp+8],0
  00485eeb  66c744240a9cff        mov       word [esp+0Ah],0FF9Ch
  00485ef2  66c744240ec800        mov       word [esp+0Eh],0C8h
  00485ef9  66c74424100000        mov       word [esp+10h],0
  00485f00  7230                  jb        short 00485F32h
  00485f02  8d442408              lea       eax,[esp+8]
  00485f06  50                    push      eax
  00485f07  56                    push      esi
  00485f08  e893edfeff            call      00474CA0h
  00485f0d  8bf8                  mov       edi,eax
  00485f0f  83c408                add       esp,8
  00485f12  85ff                  test      edi,edi
  00485f14  741c                  je        short 00485F32h
  00485f16  8b4e74                mov       ecx,[esi+74h]
  00485f19  57                    push      edi
  00485f1a  51                    push      ecx
  00485f1b  e8603c0b00            call      00539B80h
  00485f20  8b07                  mov       eax,[edi]
  00485f22  83c408                add       esp,8
  00485f25  0c40                  or        al,40h
  00485f27  66c787960000000c00    mov       word [edi+96h],0Ch
  00485f30  8907                  mov       [edi],eax

LAB_00485f32:
  00485f32  8b4638                mov       eax,[esi+38h]
  00485f35  05f0efefff            add       eax,0FFEFEFF0h
  00485f3a  894638                mov       [esi+38h],eax
  00485f3d  7506                  jne       short 00485F45h
  00485f3f  c70600000000          mov       dword [esi],0

LAB_00485f45:
  00485f45  5f                    pop       edi
  00485f46  5e                    pop       esi
  00485f47  83c40c                add       esp,0Ch
  00485f4a  c3                    ret
  00485f4b  90                    nop
  00485f4c  90                    nop
  00485f4d  90                    nop
  00485f4e  90                    nop
  00485f4f  90                    nop

2 cible(s) de saut interne : 0x00485f32, 0x00485f45
