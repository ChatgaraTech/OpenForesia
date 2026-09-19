Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e6d0  section .text  offset fichier 0x6dad0  160 octets

  0046e6d0  56                    push      esi
  0046e6d1  8b742408              mov       esi,[esp+8]
  0046e6d5  57                    push      edi
  0046e6d6  33ff                  xor       edi,edi
  0046e6d8  668b4610              mov       ax,[esi+10h]
  0046e6dc  663bc7                cmp       ax,di
  0046e6df  7514                  jne       short 0046E6F5h
  0046e6e1  66897e2a              mov       [esi+2Ah],di
  0046e6e5  66c746128000          mov       word [esi+12h],80h
  0046e6eb  6689be98000000        mov       [esi+98h],di
  0046e6f2  5f                    pop       edi
  0046e6f3  5e                    pop       esi
  0046e6f4  c3                    ret

LAB_0046e6f5:
  0046e6f5  663d0100              cmp       ax,1
  0046e6f9  7564                  jne       short 0046E75Fh
  0046e6fb  6639be98000000        cmp       [esi+98h],di
  0046e702  7531                  jne       short 0046E735h
  0046e704  a1cc975e00            mov       eax,[5E97CCh]
  0046e709  57                    push      edi
  0046e70a  57                    push      edi
  0046e70b  6800200000            push      2000h
  0046e710  8b88a8000000          mov       ecx,[eax+0A8h]
  0046e716  6800040000            push      400h
  0046e71b  8b11                  mov       edx,[ecx]
  0046e71d  660fb64227            movzx     ax,byte [edx+27h]
  0046e722  50                    push      eax
  0046e723  56                    push      esi
  0046e724  e85799ffff            call      00468080h
  0046e729  83c418                add       esp,18h
  0046e72c  66c786980000000100    mov       word [esi+98h],1

LAB_0046e735:
  0046e735  0fbf4e12              movsx     ecx,word [esi+12h]
  0046e739  51                    push      ecx
  0046e73a  e8dd720d00            call      00545A1Ch
  0046e73f  668146120001          add       word [esi+12h],100h
  0046e745  c1e00c                shl       eax,0Ch
  0046e748  c1f80b                sar       eax,0Bh
  0046e74b  66817e120008          cmp       word [esi+12h],800h
  0046e751  6689462a              mov       [esi+2Ah],ax
  0046e755  7e08                  jle       short 0046E75Fh
  0046e757  66897e10              mov       [esi+10h],di
  0046e75b  66897e2a              mov       [esi+2Ah],di

LAB_0046e75f:
  0046e75f  5f                    pop       edi
  0046e760  5e                    pop       esi
  0046e761  c3                    ret
  0046e762  90                    nop
  0046e763  90                    nop
  0046e764  90                    nop
  0046e765  90                    nop
  0046e766  90                    nop
  0046e767  90                    nop
  0046e768  90                    nop
  0046e769  90                    nop
  0046e76a  90                    nop
  0046e76b  90                    nop
  0046e76c  90                    nop
  0046e76d  90                    nop
  0046e76e  90                    nop
  0046e76f  90                    nop

3 cible(s) de saut interne : 0x0046e6f5, 0x0046e735, 0x0046e75f
