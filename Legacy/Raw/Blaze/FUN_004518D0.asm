Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004518d0  section .text  offset fichier 0x50cd0  304 octets

  004518d0  a040985e00            mov       al,[5E9840h]
  004518d5  56                    push      esi
  004518d6  8b742408              mov       esi,[esp+8]
  004518da  57                    push      edi
  004518db  a810                  test      al,10h
  004518dd  750b                  jne       short 004518EAh
  004518df  66837e1000            cmp       word [esi+10h],0
  004518e4  0f8413010000          je        near 004519FDh

LAB_004518ea:
  004518ea  66837e1000            cmp       word [esi+10h],0
  004518ef  bf01000000            mov       edi,1
  004518f4  755c                  jne       short 00451952h
  004518f6  8b06                  mov       eax,[esi]
  004518f8  6a00                  push      0
  004518fa  80cc20                or        ah,20h
  004518fd  6a00                  push      0
  004518ff  8906                  mov       [esi],eax
  00451901  a1cc975e00            mov       eax,[5E97CCh]
  00451906  6800100000            push      1000h
  0045190b  6800020000            push      200h
  00451910  8b88a8000000          mov       ecx,[eax+0A8h]
  00451916  8b11                  mov       edx,[ecx]
  00451918  660fb6422b            movzx     ax,byte [edx+2Bh]
  0045191d  50                    push      eax
  0045191e  56                    push      esi
  0045191f  e85c670100            call      00468080h
  00451924  83c418                add       esp,18h
  00451927  66897e10              mov       [esi+10h],di
  0045192b  66c746120300          mov       word [esi+12h],3
  00451931  e80a580f00            call      00547140h
  00451936  99                    cdq
  00451937  b91e000000            mov       ecx,1Eh
  0045193c  66c786980000000000    mov       word [esi+98h],0
  00451945  f7f9                  idiv      ecx
  00451947  b8f6ffffff            mov       eax,0FFFFFFF6h
  0045194c  2bc2                  sub       eax,edx
  0045194e  66894614              mov       [esi+14h],ax

LAB_00451952:
  00451952  66397e10              cmp       [esi+10h],di
  00451956  0f85a1000000          jne       near 004519FDh
  0045195c  668b4612              mov       ax,[esi+12h]
  00451960  8b5620                mov       edx,[esi+20h]
  00451963  0fbfc8                movsx     ecx,ax
  00451966  c1e10c                shl       ecx,0Ch
  00451969  2bd1                  sub       edx,ecx
  0045196b  40                    inc       eax
  0045196c  663d0a00              cmp       ax,0Ah
  00451970  895620                mov       [esi+20h],edx
  00451973  66894612              mov       [esi+12h],ax
  00451977  7e06                  jle       short 0045197Fh
  00451979  66c746120a00          mov       word [esi+12h],0Ah

LAB_0045197f:
  0045197f  668b5614              mov       dx,[esi+14h]
  00451983  8b4620                mov       eax,[esi+20h]
  00451986  66015634              add       [esi+34h],dx
  0045198a  3d0040d4ff            cmp       eax,0FFD44000h
  0045198f  7d5d                  jge       short 004519EEh
  00451991  3d0000ceff            cmp       eax,0FFCE0000h
  00451996  7e56                  jle       short 004519EEh
  00451998  6683be9800000000      cmp       word [esi+98h],0
  004519a0  7531                  jne       short 004519D3h
  004519a2  a1cc975e00            mov       eax,[5E97CCh]
  004519a7  6a00                  push      0
  004519a9  6a00                  push      0
  004519ab  6800100000            push      1000h
  004519b0  8b88a8000000          mov       ecx,[eax+0A8h]
  004519b6  6800030000            push      300h
  004519bb  8b11                  mov       edx,[ecx]
  004519bd  660fb6422c            movzx     ax,byte [edx+2Ch]
  004519c2  50                    push      eax
  004519c3  56                    push      esi
  004519c4  e8b7660100            call      00468080h
  004519c9  83c418                add       esp,18h
  004519cc  6689be98000000        mov       [esi+98h],di

LAB_004519d3:
  004519d3  bf04000000            mov       edi,4

LAB_004519d8:
  004519d8  6a00                  push      0
  004519da  6a00                  push      0
  004519dc  6aff                  push      0FFFFFFFFh
  004519de  6a00                  push      0
  004519e0  6a32                  push      32h
  004519e2  56                    push      esi
  004519e3  e828670100            call      00468110h
  004519e8  83c418                add       esp,18h
  004519eb  4f                    dec       edi
  004519ec  75ea                  jne       short 004519D8h

LAB_004519ee:
  004519ee  817e200000b5ff        cmp       dword [esi+20h],0FFB50000h
  004519f5  7d06                  jge       short 004519FDh
  004519f7  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004519fd:
  004519fd  5f                    pop       edi
  004519fe  5e                    pop       esi
  004519ff  c3                    ret

7 cible(s) de saut interne : 0x004518ea, 0x00451952, 0x0045197f, 0x004519d3, 0x004519d8, 0x004519ee, 0x004519fd
