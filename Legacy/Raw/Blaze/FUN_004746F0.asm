Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004746f0  section .text  offset fichier 0x73af0  250 octets

  004746f0  53                    push      ebx
  004746f1  56                    push      esi
  004746f2  8b74240c              mov       esi,[esp+0Ch]
  004746f6  33db                  xor       ebx,ebx
  004746f8  66395e10              cmp       [esi+10h],bx
  004746fc  7512                  jne       short 00474710h
  004746fe  668b4614              mov       ax,[esi+14h]
  00474702  66c746100100          mov       word [esi+10h],1
  00474708  66895e12              mov       [esi+12h],bx
  0047470c  66894616              mov       [esi+16h],ax

LAB_00474710:
  00474710  668b4616              mov       ax,[esi+16h]
  00474714  663bc3                cmp       ax,bx
  00474717  7438                  je        short 00474751h
  00474719  0fbfc8                movsx     ecx,ax
  0047471c  8bc1                  mov       eax,ecx
  0047471e  99                    cdq
  0047471f  83e207                and       edx,7
  00474722  03c2                  add       eax,edx
  00474724  c1f803                sar       eax,3
  00474727  0fbfc0                movsx     eax,ax
  0047472a  8d14c500000000        lea       edx,[eax*8]
  00474731  8a8040985e00          mov       al,[eax+5E9840h]
  00474737  2bca                  sub       ecx,edx
  00474739  ba01000000            mov       edx,1
  0047473e  d3e2                  shl       edx,cl
  00474740  84d0                  test      al,dl
  00474742  740d                  je        short 00474751h
  00474744  8b06                  mov       eax,[esi]
  00474746  66c746100200          mov       word [esi+10h],2
  0047474c  80cc40                or        ah,40h
  0047474f  8906                  mov       [esi],eax

LAB_00474751:
  00474751  66837e1001            cmp       word [esi+10h],1
  00474756  7579                  jne       short 004747D1h
  00474758  66395e12              cmp       [esi+12h],bx
  0047475c  7535                  jne       short 00474793h
  0047475e  8b0e                  mov       ecx,[esi]
  00474760  80cd40                or        ch,40h
  00474763  890e                  mov       [esi],ecx
  00474765  e8d6290d00            call      00547140h
  0047476a  99                    cdq
  0047476b  b90d000000            mov       ecx,0Dh
  00474770  f7f9                  idiv      ecx
  00474772  6683fa01              cmp       dx,1
  00474776  7559                  jne       short 004747D1h
  00474778  8b06                  mov       eax,[esi]
  0047477a  66895612              mov       [esi+12h],dx
  0047477e  80e4bf                and       ah,0BFh
  00474781  66895e28              mov       [esi+28h],bx
  00474785  66895e2a              mov       [esi+2Ah],bx
  00474789  66c746140002          mov       word [esi+14h],200h
  0047478f  8906                  mov       [esi],eax
  00474791  eb3e                  jmp       short 004747D1h

LAB_00474793:
  00474793  668b4614              mov       ax,[esi+14h]
  00474797  66014628              add       [esi+28h],ax
  0047479b  668b4e28              mov       cx,[esi+28h]
  0047479f  6601462a              add       [esi+2Ah],ax
  004747a3  663bcb                cmp       cx,bx
  004747a6  7d0b                  jge       short 004747B3h
  004747a8  8b06                  mov       eax,[esi]
  004747aa  66895e12              mov       [esi+12h],bx
  004747ae  80cc40                or        ah,40h
  004747b1  8906                  mov       [esi],eax

LAB_004747b3:
  004747b3  6681f90010            cmp       cx,1000h
  004747b8  7e06                  jle       short 004747C0h
  004747ba  66c7461400fc          mov       word [esi+14h],0FC00h

LAB_004747c0:
  004747c0  8a4e40                mov       cl,[esi+40h]
  004747c3  fec1                  inc       cl
  004747c5  8ac1                  mov       al,cl
  004747c7  884e40                mov       [esi+40h],cl
  004747ca  3c03                  cmp       al,3
  004747cc  7503                  jne       short 004747D1h
  004747ce  885e40                mov       [esi+40h],bl

LAB_004747d1:
  004747d1  66837e1002            cmp       word [esi+10h],2
  004747d6  7507                  jne       short 004747DFh
  004747d8  8b06                  mov       eax,[esi]
  004747da  80cc40                or        ah,40h
  004747dd  8906                  mov       [esi],eax

LAB_004747df:
  004747df  5e                    pop       esi
  004747e0  5b                    pop       ebx
  004747e1  c3                    ret
  004747e2  90                    nop
  004747e3  90                    nop
  004747e4  90                    nop
  004747e5  90                    nop
  004747e6  90                    nop
  004747e7  90                    nop
  004747e8  90                    nop
  004747e9  90                    nop

7 cible(s) de saut interne : 0x00474710, 0x00474751, 0x00474793, 0x004747b3, 0x004747c0, 0x004747d1, 0x004747df
