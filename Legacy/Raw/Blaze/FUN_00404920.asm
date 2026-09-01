Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00404920  section .text  offset fichier 0x3d20  298 octets

  00404920  83ec0c                sub       esp,0Ch
  00404923  55                    push      ebp
  00404924  56                    push      esi
  00404925  57                    push      edi
  00404926  8b7c241c              mov       edi,[esp+1Ch]
  0040492a  8b776c                mov       esi,[edi+6Ch]
  0040492d  85f6                  test      esi,esi
  0040492f  0f840e010000          je        near 00404A43h
  00404935  8b461c                mov       eax,[esi+1Ch]
  00404938  8d6f1c                lea       ebp,[edi+1Ch]
  0040493b  894500                mov       [ebp],eax
  0040493e  8b4e20                mov       ecx,[esi+20h]
  00404941  894f20                mov       [edi+20h],ecx
  00404944  8b5624                mov       edx,[esi+24h]
  00404947  33c0                  xor       eax,eax
  00404949  895724                mov       [edi+24h],edx
  0040494c  8a4604                mov       al,[esi+4]
  0040494f  66c746180200          mov       word [esi+18h],2
  00404955  8b0c8540965e00        mov       ecx,[eax*4+5E9640h]
  0040495c  8b5108                mov       edx,[ecx+8]
  0040495f  8b0e                  mov       ecx,[esi]
  00404961  83c908                or        ecx,8
  00404964  8954240c              mov       [esp+0Ch],edx
  00404968  890e                  mov       [esi],ecx
  0040496a  66837f1000            cmp       word [edi+10h],0
  0040496f  7574                  jne       short 004049E5h
  00404971  66c747100100          mov       word [edi+10h],1
  00404977  e8c4271400            call      00547140h
  0040497c  99                    cdq
  0040497d  b905000000            mov       ecx,5
  00404982  f7f9                  idiv      ecx
  00404984  8d442410              lea       eax,[esp+10h]
  00404988  8d4c2414              lea       ecx,[esp+14h]
  0040498c  83c206                add       edx,6
  0040498f  66895712              mov       [edi+12h],dx
  00404993  8d54241c              lea       edx,[esp+1Ch]
  00404997  52                    push      edx
  00404998  50                    push      eax
  00404999  51                    push      ecx
  0040499a  56                    push      esi
  0040499b  c6460a00              mov       byte [esi+0Ah],0
  0040499f  66c7461a0000          mov       word [esi+1Ah],0
  004049a5  e8d63f1300            call      00538980h
  004049aa  8b44242c              mov       eax,[esp+2Ch]
  004049ae  83c410                add       esp,10h
  004049b1  0fbf481c              movsx     ecx,word [eax+1Ch]
  004049b5  0fbf501e              movsx     edx,word [eax+1Eh]
  004049b9  0fbf4020              movsx     eax,word [eax+20h]
  004049bd  03ca                  add       ecx,edx
  004049bf  6aff                  push      0FFFFFFFFh
  004049c1  03c8                  add       ecx,eax
  004049c3  b856555555            mov       eax,55555556h
  004049c8  f7e9                  imul      ecx
  004049ca  8bca                  mov       ecx,edx
  004049cc  6a00                  push      0
  004049ce  c1e91f                shr       ecx,1Fh
  004049d1  03d1                  add       edx,ecx
  004049d3  6a50                  push      50h
  004049d5  66895714              mov       [edi+14h],dx
  004049d9  8b576c                mov       edx,[edi+6Ch]
  004049dc  52                    push      edx
  004049dd  e8ee5f0700            call      0047A9D0h
  004049e2  83c410                add       esp,10h

LAB_004049e5:
  004049e5  66837f1001            cmp       word [edi+10h],1
  004049ea  7557                  jne       short 00404A43h
  004049ec  66ff4f12              dec       word [edi+12h]
  004049f0  6aff                  push      0FFFFFFFFh
  004049f2  6a00                  push      0
  004049f4  6a37                  push      37h
  004049f6  6a00                  push      0
  004049f8  55                    push      ebp
  004049f9  57                    push      edi
  004049fa  e841620700            call      0047AC40h
  004049ff  83c418                add       esp,18h
  00404a02  66837f1200            cmp       word [edi+12h],0
  00404a07  7d10                  jge       short 00404A19h
  00404a09  8b0f                  mov       ecx,[edi]
  00404a0b  81e1ffffff7f          and       ecx,7FFFFFFFh
  00404a11  890f                  mov       [edi],ecx
  00404a13  8b06                  mov       eax,[esi]
  00404a15  24f7                  and       al,0F7h
  00404a17  8906                  mov       [esi],eax

LAB_00404a19:
  00404a19  f6471201              test      byte [edi+12h],1
  00404a1d  7424                  je        short 00404A43h
  00404a1f  8b44240c              mov       eax,[esp+0Ch]
  00404a23  66ff461a              inc       word [esi+1Ah]
  00404a27  668b4e1a              mov       cx,[esi+1Ah]
  00404a2b  668b4016              mov       ax,[eax+16h]
  00404a2f  0fbfc9                movsx     ecx,cx
  00404a32  8bd0                  mov       edx,eax
  00404a34  81e2ffff0000          and       edx,0FFFFh
  00404a3a  3bca                  cmp       ecx,edx
  00404a3c  7c05                  jl        short 00404A43h
  00404a3e  48                    dec       eax
  00404a3f  6689461a              mov       [esi+1Ah],ax

LAB_00404a43:
  00404a43  5f                    pop       edi
  00404a44  5e                    pop       esi
  00404a45  5d                    pop       ebp
  00404a46  83c40c                add       esp,0Ch
  00404a49  c3                    ret

3 cible(s) de saut interne : 0x004049e5, 0x00404a19, 0x00404a43
