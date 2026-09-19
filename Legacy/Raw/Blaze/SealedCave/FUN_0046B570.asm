Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046b570  section .text  offset fichier 0x6a970  160 octets

  0046b570  56                    push      esi
  0046b571  8b742408              mov       esi,[esp+8]
  0046b575  57                    push      edi
  0046b576  33ff                  xor       edi,edi
  0046b578  668b4610              mov       ax,[esi+10h]
  0046b57c  663bc7                cmp       ax,di
  0046b57f  7514                  jne       short 0046B595h
  0046b581  66897e2a              mov       [esi+2Ah],di
  0046b585  66c746128000          mov       word [esi+12h],80h
  0046b58b  6689be98000000        mov       [esi+98h],di
  0046b592  5f                    pop       edi
  0046b593  5e                    pop       esi
  0046b594  c3                    ret

LAB_0046b595:
  0046b595  663d0100              cmp       ax,1
  0046b599  7564                  jne       short 0046B5FFh
  0046b59b  6639be98000000        cmp       [esi+98h],di
  0046b5a2  7531                  jne       short 0046B5D5h
  0046b5a4  a1cc975e00            mov       eax,[5E97CCh]
  0046b5a9  57                    push      edi
  0046b5aa  57                    push      edi
  0046b5ab  6800200000            push      2000h
  0046b5b0  8b88a8000000          mov       ecx,[eax+0A8h]
  0046b5b6  6800040000            push      400h
  0046b5bb  8b11                  mov       edx,[ecx]
  0046b5bd  660fb64227            movzx     ax,byte [edx+27h]
  0046b5c2  50                    push      eax
  0046b5c3  56                    push      esi
  0046b5c4  e8b7caffff            call      00468080h
  0046b5c9  83c418                add       esp,18h
  0046b5cc  66c786980000000100    mov       word [esi+98h],1

LAB_0046b5d5:
  0046b5d5  0fbf4e12              movsx     ecx,word [esi+12h]
  0046b5d9  51                    push      ecx
  0046b5da  e83da40d00            call      00545A1Ch
  0046b5df  668146120001          add       word [esi+12h],100h
  0046b5e5  c1e00c                shl       eax,0Ch
  0046b5e8  c1f80b                sar       eax,0Bh
  0046b5eb  66817e120008          cmp       word [esi+12h],800h
  0046b5f1  6689462a              mov       [esi+2Ah],ax
  0046b5f5  7e08                  jle       short 0046B5FFh
  0046b5f7  66897e10              mov       [esi+10h],di
  0046b5fb  66897e2a              mov       [esi+2Ah],di

LAB_0046b5ff:
  0046b5ff  5f                    pop       edi
  0046b600  5e                    pop       esi
  0046b601  c3                    ret
  0046b602  90                    nop
  0046b603  90                    nop
  0046b604  90                    nop
  0046b605  90                    nop
  0046b606  90                    nop
  0046b607  90                    nop
  0046b608  90                    nop
  0046b609  90                    nop
  0046b60a  90                    nop
  0046b60b  90                    nop
  0046b60c  90                    nop
  0046b60d  90                    nop
  0046b60e  90                    nop
  0046b60f  90                    nop

3 cible(s) de saut interne : 0x0046b595, 0x0046b5d5, 0x0046b5ff
