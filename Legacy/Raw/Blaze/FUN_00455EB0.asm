Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00455eb0  section .text  offset fichier 0x552b0  350 octets

  00455eb0  53                    push      ebx
  00455eb1  56                    push      esi
  00455eb2  8b74240c              mov       esi,[esp+0Ch]
  00455eb6  33db                  xor       ebx,ebx
  00455eb8  57                    push      edi
  00455eb9  66395e10              cmp       [esi+10h],bx
  00455ebd  7504                  jne       short 00455EC3h
  00455ebf  66895e12              mov       [esi+12h],bx

LAB_00455ec3:
  00455ec3  6800080000            push      800h
  00455ec8  56                    push      esi
  00455ec9  e862fcffff            call      00455B30h
  00455ece  8b4644                mov       eax,[esi+44h]
  00455ed1  83c408                add       esp,8
  00455ed4  3bc3                  cmp       eax,ebx
  00455ed6  7d02                  jge       short 00455EDAh
  00455ed8  f7d8                  neg       eax

LAB_00455eda:
  00455eda  3d00100000            cmp       eax,1000h
  00455edf  7f10                  jg        short 00455EF1h
  00455ee1  8b464c                mov       eax,[esi+4Ch]
  00455ee4  3bc3                  cmp       eax,ebx
  00455ee6  7d02                  jge       short 00455EEAh
  00455ee8  f7d8                  neg       eax

LAB_00455eea:
  00455eea  3d00100000            cmp       eax,1000h
  00455eef  7e34                  jle       short 00455F25h

LAB_00455ef1:
  00455ef1  66395e12              cmp       [esi+12h],bx
  00455ef5  752e                  jne       short 00455F25h
  00455ef7  a1cc975e00            mov       eax,[5E97CCh]
  00455efc  53                    push      ebx
  00455efd  53                    push      ebx
  00455efe  6800100000            push      1000h
  00455f03  8b88a8000000          mov       ecx,[eax+0A8h]
  00455f09  6800080000            push      800h
  00455f0e  8b11                  mov       edx,[ecx]
  00455f10  660fb6422d            movzx     ax,byte [edx+2Dh]
  00455f15  50                    push      eax
  00455f16  56                    push      esi
  00455f17  e864210100            call      00468080h
  00455f1c  83c418                add       esp,18h
  00455f1f  66c746121400          mov       word [esi+12h],14h

LAB_00455f25:
  00455f25  66ff4e12              dec       word [esi+12h]
  00455f29  66395e12              cmp       [esi+12h],bx
  00455f2d  7d04                  jge       short 00455F33h
  00455f2f  66895e12              mov       [esi+12h],bx

LAB_00455f33:
  00455f33  817e200080daff        cmp       dword [esi+20h],0FFDA8000h
  00455f3a  0f8dab000000          jge       near 00455FEBh
  00455f40  33d2                  xor       edx,edx
  00455f42  bf00c01200            mov       edi,12C000h

LAB_00455f47:
  00455f47  0fbfca                movsx     ecx,dx
  00455f4a  8d0489                lea       eax,[ecx+ecx*4]
  00455f4d  c1e003                shl       eax,3
  00455f50  2bc1                  sub       eax,ecx
  00455f52  c1e002                shl       eax,2
  00455f55  8b88c0935e00          mov       ecx,[eax+5E93C0h]
  00455f5b  f7c100000080          test      ecx,80000000h
  00455f61  7410                  je        short 00455F73h
  00455f63  f7c100000050          test      ecx,50000000h
  00455f69  7508                  jne       short 00455F73h
  00455f6b  39b8e0935e00          cmp       [eax+5E93E0h],edi
  00455f71  7c78                  jl        short 00455FEBh

LAB_00455f73:
  00455f73  42                    inc       edx
  00455f74  6683fa03              cmp       dx,3
  00455f78  7ecd                  jle       short 00455F47h
  00455f7a  f605773c560001        test      byte [563C77h],1
  00455f81  7568                  jne       short 00455FEBh
  00455f83  8b3e                  mov       edi,[esi]
  00455f85  6633c0                xor       ax,ax
  00455f88  81e7ffffff7f          and       edi,7FFFFFFFh
  00455f8e  893e                  mov       [esi],edi
  00455f90  8a0dfa965e00          mov       cl,[5E96FAh]
  00455f96  80e10f                and       cl,0Fh
  00455f99  8ac1                  mov       al,cl
  00455f9b  663bc3                cmp       ax,bx
  00455f9e  7f02                  jg        short 00455FA2h
  00455fa0  33c0                  xor       eax,eax

LAB_00455fa2:
  00455fa2  663d0400              cmp       ax,4
  00455fa6  7c05                  jl        short 00455FADh
  00455fa8  b804000000            mov       eax,4

LAB_00455fad:
  00455fad  8b566c                mov       edx,[esi+6Ch]
  00455fb0  53                    push      ebx
  00455fb1  0fbff8                movsx     edi,ax
  00455fb4  a1cc975e00            mov       eax,[5E97CCh]
  00455fb9  68b0335600            push      5633B0h
  00455fbe  52                    push      edx
  00455fbf  8b4870                mov       ecx,[eax+70h]
  00455fc2  8b54b904              mov       edx,[ecx+edi*4+4]
  00455fc6  52                    push      edx
  00455fc7  e884e30e00            call      00544350h
  00455fcc  a1cc975e00            mov       eax,[5E97CCh]
  00455fd1  83c410                add       esp,10h
  00455fd4  57                    push      edi
  00455fd5  56                    push      esi
  00455fd6  50                    push      eax
  00455fd7  e8f4dd0b00            call      00513DD0h
  00455fdc  a0fa965e00            mov       al,[5E96FAh]
  00455fe1  83c40c                add       esp,0Ch
  00455fe4  fec0                  inc       al
  00455fe6  a2fa965e00            mov       [5E96FAh],al

LAB_00455feb:
  00455feb  8b4e20                mov       ecx,[esi+20h]
  00455fee  b80000ceff            mov       eax,0FFCE0000h
  00455ff3  3bc8                  cmp       ecx,eax
  00455ff5  7d0a                  jge       short 00456001h
  00455ff7  894620                mov       [esi+20h],eax
  00455ffa  8b06                  mov       eax,[esi]
  00455ffc  80cc60                or        ah,60h
  00455fff  8906                  mov       [esi],eax

LAB_00456001:
  00456001  895e50                mov       [esi+50h],ebx
  00456004  895e54                mov       [esi+54h],ebx
  00456007  895e58                mov       [esi+58h],ebx
  0045600a  5f                    pop       edi
  0045600b  5e                    pop       esi
  0045600c  5b                    pop       ebx
  0045600d  c3                    ret

12 cible(s) de saut interne : 0x00455ec3, 0x00455eda, 0x00455eea, 0x00455ef1, 0x00455f25, 0x00455f33, 0x00455f47, 0x00455f73, 0x00455fa2, 0x00455fad, 0x00455feb, 0x00456001
