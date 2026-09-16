Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00451a00  section .text  offset fichier 0x50e00  311 octets

  00451a00  a040985e00            mov       al,[5E9840h]
  00451a05  56                    push      esi
  00451a06  8b742408              mov       esi,[esp+8]
  00451a0a  57                    push      edi
  00451a0b  a810                  test      al,10h
  00451a0d  750b                  jne       short 00451A1Ah
  00451a0f  66837e1000            cmp       word [esi+10h],0
  00451a14  0f841a010000          je        near 00451B34h

LAB_00451a1a:
  00451a1a  66837e1000            cmp       word [esi+10h],0
  00451a1f  7549                  jne       short 00451A6Ah
  00451a21  8b3e                  mov       edi,[esi]
  00451a23  6a00                  push      0
  00451a25  81cf00200000          or        edi,2000h
  00451a2b  6a00                  push      0
  00451a2d  893e                  mov       [esi],edi
  00451a2f  a1cc975e00            mov       eax,[5E97CCh]
  00451a34  6800100000            push      1000h
  00451a39  6800030000            push      300h
  00451a3e  8b88a8000000          mov       ecx,[eax+0A8h]
  00451a44  8b11                  mov       edx,[ecx]
  00451a46  660fb6422b            movzx     ax,byte [edx+2Bh]
  00451a4b  50                    push      eax
  00451a4c  56                    push      esi
  00451a4d  e82e660100            call      00468080h
  00451a52  83c418                add       esp,18h
  00451a55  66c746100200          mov       word [esi+10h],2
  00451a5b  66c746120300          mov       word [esi+12h],3
  00451a61  66c786980000000000    mov       word [esi+98h],0

LAB_00451a6a:
  00451a6a  66837e1001            cmp       word [esi+10h],1
  00451a6f  0f85a1000000          jne       near 00451B16h
  00451a75  668b4612              mov       ax,[esi+12h]
  00451a79  8b5620                mov       edx,[esi+20h]
  00451a7c  0fbfc8                movsx     ecx,ax
  00451a7f  c1e10c                shl       ecx,0Ch
  00451a82  2bd1                  sub       edx,ecx
  00451a84  40                    inc       eax
  00451a85  663d0a00              cmp       ax,0Ah
  00451a89  895620                mov       [esi+20h],edx
  00451a8c  66894612              mov       [esi+12h],ax
  00451a90  7e06                  jle       short 00451A98h
  00451a92  66c746120a00          mov       word [esi+12h],0Ah

LAB_00451a98:
  00451a98  8b4620                mov       eax,[esi+20h]
  00451a9b  668346340f            add       word [esi+34h],0Fh
  00451aa0  3d0040d4ff            cmp       eax,0FFD44000h
  00451aa5  7d60                  jge       short 00451B07h
  00451aa7  3d0000ceff            cmp       eax,0FFCE0000h
  00451aac  7e59                  jle       short 00451B07h
  00451aae  6683be9800000000      cmp       word [esi+98h],0
  00451ab6  7534                  jne       short 00451AECh
  00451ab8  8b15cc975e00          mov       edx,[5E97CCh]
  00451abe  6a00                  push      0
  00451ac0  6a00                  push      0
  00451ac2  6800100000            push      1000h
  00451ac7  8b82a8000000          mov       eax,[edx+0A8h]
  00451acd  6800020000            push      200h
  00451ad2  8b08                  mov       ecx,[eax]
  00451ad4  660fb6512c            movzx     dx,byte [ecx+2Ch]
  00451ad9  52                    push      edx
  00451ada  56                    push      esi
  00451adb  e8a0650100            call      00468080h
  00451ae0  83c418                add       esp,18h
  00451ae3  66c786980000000100    mov       word [esi+98h],1

LAB_00451aec:
  00451aec  bf04000000            mov       edi,4

LAB_00451af1:
  00451af1  6a00                  push      0
  00451af3  6a00                  push      0
  00451af5  6aff                  push      0FFFFFFFFh
  00451af7  6a00                  push      0
  00451af9  6a32                  push      32h
  00451afb  56                    push      esi
  00451afc  e80f660100            call      00468110h
  00451b01  83c418                add       esp,18h
  00451b04  4f                    dec       edi
  00451b05  75ea                  jne       short 00451AF1h

LAB_00451b07:
  00451b07  817e200000b5ff        cmp       dword [esi+20h],0FFB50000h
  00451b0e  7d06                  jge       short 00451B16h
  00451b10  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00451b16:
  00451b16  66837e1002            cmp       word [esi+10h],2
  00451b1b  7517                  jne       short 00451B34h
  00451b1d  66ff4612              inc       word [esi+12h]
  00451b21  66837e120a            cmp       word [esi+12h],0Ah
  00451b26  750c                  jne       short 00451B34h
  00451b28  66c746100100          mov       word [esi+10h],1
  00451b2e  66c746120300          mov       word [esi+12h],3

LAB_00451b34:
  00451b34  5f                    pop       edi
  00451b35  5e                    pop       esi
  00451b36  c3                    ret

8 cible(s) de saut interne : 0x00451a1a, 0x00451a6a, 0x00451a98, 0x00451aec, 0x00451af1, 0x00451b07, 0x00451b16, 0x00451b34
