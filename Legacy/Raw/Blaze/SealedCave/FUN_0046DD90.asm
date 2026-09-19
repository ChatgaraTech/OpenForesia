Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046dd90  section .text  offset fichier 0x6d190  240 octets

  0046dd90  53                    push      ebx
  0046dd91  55                    push      ebp
  0046dd92  56                    push      esi
  0046dd93  8b742410              mov       esi,[esp+10h]
  0046dd97  33db                  xor       ebx,ebx
  0046dd99  57                    push      edi
  0046dd9a  66395e10              cmp       [esi+10h],bx
  0046dd9e  751e                  jne       short 0046DDBEh
  0046dda0  668b4614              mov       ax,[esi+14h]
  0046dda4  8b4e1c                mov       ecx,[esi+1Ch]
  0046dda7  8b5624                mov       edx,[esi+24h]
  0046ddaa  66c746100100          mov       word [esi+10h],1
  0046ddb0  66894612              mov       [esi+12h],ax
  0046ddb4  66895e14              mov       [esi+14h],bx
  0046ddb8  894e44                mov       [esi+44h],ecx
  0046ddbb  89564c                mov       [esi+4Ch],edx

LAB_0046ddbe:
  0046ddbe  66837e1001            cmp       word [esi+10h],1
  0046ddc3  bd02000000            mov       ebp,2
  0046ddc8  7575                  jne       short 0046DE3Fh
  0046ddca  0fbf4632              movsx     eax,word [esi+32h]
  0046ddce  50                    push      eax
  0046ddcf  895e50                mov       [esi+50h],ebx
  0046ddd2  895e54                mov       [esi+54h],ebx
  0046ddd5  895e58                mov       [esi+58h],ebx
  0046ddd8  e83f7c0d00            call      00545A1Ch
  0046dddd  0fbf4e14              movsx     ecx,word [esi+14h]
  0046dde1  0fafc1                imul      eax,ecx
  0046dde4  0fbf5632              movsx     edx,word [esi+32h]
  0046dde8  8b4e4c                mov       ecx,[esi+4Ch]
  0046ddeb  668b7e14              mov       di,[esi+14h]
  0046ddef  03c1                  add       eax,ecx
  0046ddf1  52                    push      edx
  0046ddf2  894624                mov       [esi+24h],eax
  0046ddf5  e81c7c0d00            call      00545A16h
  0046ddfa  0fbfcf                movsx     ecx,di
  0046ddfd  0fafc1                imul      eax,ecx
  0046de00  034644                add       eax,[esi+44h]
  0046de03  89461c                mov       [esi+1Ch],eax
  0046de06  668b4612              mov       ax,[esi+12h]
  0046de0a  6603c7                add       ax,di
  0046de0d  663df000              cmp       ax,0F0h
  0046de11  66894614              mov       [esi+14h],ax
  0046de15  7e11                  jle       short 0046DE28h
  0046de17  66c74614f000          mov       word [esi+14h],0F0h
  0046de1d  66896e10              mov       [esi+10h],bp
  0046de21  6689ae9a000000        mov       [esi+9Ah],bp

LAB_0046de28:
  0046de28  66395e14              cmp       [esi+14h],bx
  0046de2c  7d11                  jge       short 0046DE3Fh
  0046de2e  66895e14              mov       [esi+14h],bx
  0046de32  66896e10              mov       [esi+10h],bp
  0046de36  66c7869a0000000f00    mov       word [esi+9Ah],0Fh

LAB_0046de3f:
  0046de3f  66396e10              cmp       [esi+10h],bp
  0046de43  7528                  jne       short 0046DE6Dh
  0046de45  66ff8e9a000000        dec       word [esi+9Ah]
  0046de4c  66399e9a000000        cmp       [esi+9Ah],bx
  0046de53  7518                  jne       short 0046DE6Dh
  0046de55  668b5612              mov       dx,[esi+12h]
  0046de59  8b06                  mov       eax,[esi]
  0046de5b  66f7da                neg       dx
  0046de5e  80e4df                and       ah,0DFh
  0046de61  66895612              mov       [esi+12h],dx
  0046de65  66c746100100          mov       word [esi+10h],1
  0046de6b  8906                  mov       [esi],eax

LAB_0046de6d:
  0046de6d  5f                    pop       edi
  0046de6e  5e                    pop       esi
  0046de6f  5d                    pop       ebp
  0046de70  5b                    pop       ebx
  0046de71  c3                    ret
  0046de72  90                    nop
  0046de73  90                    nop
  0046de74  90                    nop
  0046de75  90                    nop
  0046de76  90                    nop
  0046de77  90                    nop
  0046de78  90                    nop
  0046de79  90                    nop
  0046de7a  90                    nop
  0046de7b  90                    nop
  0046de7c  90                    nop
  0046de7d  90                    nop
  0046de7e  90                    nop
  0046de7f  90                    nop

4 cible(s) de saut interne : 0x0046ddbe, 0x0046de28, 0x0046de3f, 0x0046de6d
