Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004438a0  section .text  offset fichier 0x42ca0  179 octets

  004438a0  a164905e00            mov       eax,[5E9064h]
  004438a5  8b0db4876000          mov       ecx,[6087B4h]
  004438ab  25ff000000            and       eax,0FFh
  004438b0  8b11                  mov       edx,[ecx]
  004438b2  83c078                add       eax,78h
  004438b5  c1e00d                shl       eax,0Dh
  004438b8  03c2                  add       eax,edx
  004438ba  33c9                  xor       ecx,ecx
  004438bc  8b905c010000          mov       edx,[eax+15Ch]
  004438c2  880d68905e00          mov       [5E9068h],cl
  004438c8  891564905e00          mov       [5E9064h],edx
  004438ce  8b905c010000          mov       edx,[eax+15Ch]
  004438d4  83fa0a                cmp       edx,0Ah
  004438d7  7205                  jb        short 004438DEh
  004438d9  b901000000            mov       ecx,1

LAB_004438de:
  004438de  83fa14                cmp       edx,14h
  004438e1  7201                  jb        short 004438E4h
  004438e3  41                    inc       ecx

LAB_004438e4:
  004438e4  83fa28                cmp       edx,28h
  004438e7  7201                  jb        short 004438EAh
  004438e9  41                    inc       ecx

LAB_004438ea:
  004438ea  83fa50                cmp       edx,50h
  004438ed  7201                  jb        short 004438F0h
  004438ef  41                    inc       ecx

LAB_004438f0:
  004438f0  81faa0000000          cmp       edx,0A0h
  004438f6  7201                  jb        short 004438F9h
  004438f8  41                    inc       ecx

LAB_004438f9:
  004438f9  81fa40010000          cmp       edx,140h
  004438ff  7201                  jb        short 00443902h
  00443901  41                    inc       ecx

LAB_00443902:
  00443902  81fa80020000          cmp       edx,280h
  00443908  7201                  jb        short 0044390Bh
  0044390a  41                    inc       ecx

LAB_0044390b:
  0044390b  81fa00050000          cmp       edx,500h
  00443911  7201                  jb        short 00443914h
  00443913  41                    inc       ecx

LAB_00443914:
  00443914  81fa000a0000          cmp       edx,0A00h
  0044391a  7201                  jb        short 0044391Dh
  0044391c  41                    inc       ecx

LAB_0044391d:
  0044391d  81fa00140000          cmp       edx,1400h
  00443923  7201                  jb        short 00443926h
  00443925  41                    inc       ecx

LAB_00443926:
  00443926  81fa00280000          cmp       edx,2800h
  0044392c  7201                  jb        short 0044392Fh
  0044392e  41                    inc       ecx

LAB_0044392f:
  0044392f  81fa00500000          cmp       edx,5000h
  00443935  7201                  jb        short 00443938h
  00443937  41                    inc       ecx

LAB_00443938:
  00443938  81fa00a00000          cmp       edx,0A000h
  0044393e  7201                  jb        short 00443941h
  00443940  41                    inc       ecx

LAB_00443941:
  00443941  880d68905e00          mov       [5E9068h],cl
  00443947  0fbfc9                movsx     ecx,cx
  0044394a  8988f81f0000          mov       [eax+1FF8h],ecx
  00443950  33c0                  xor       eax,eax
  00443952  c3                    ret

13 cible(s) de saut interne : 0x004438de, 0x004438e4, 0x004438ea, 0x004438f0, 0x004438f9, 0x00443902, 0x0044390b, 0x00443914, 0x0044391d, 0x00443926, 0x0044392f, 0x00443938, 0x00443941
