Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004533a0  section .text  offset fichier 0x527a0  687 octets

  004533a0  53                    push      ebx
  004533a1  56                    push      esi
  004533a2  8b74240c              mov       esi,[esp+0Ch]
  004533a6  33db                  xor       ebx,ebx
  004533a8  57                    push      edi
  004533a9  668b4630              mov       ax,[esi+30h]
  004533ad  663bc3                cmp       ax,bx
  004533b0  740a                  je        short 004533BCh
  004533b2  0fbfc0                movsx     eax,ax
  004533b5  894644                mov       [esi+44h],eax
  004533b8  66895e30              mov       [esi+30h],bx

LAB_004533bc:
  004533bc  6800000040            push      40000000h
  004533c1  6a10                  push      10h
  004533c3  56                    push      esi
  004533c4  e8371c0e00            call      00535000h
  004533c9  8bf8                  mov       edi,eax
  004533cb  83c40c                add       esp,0Ch
  004533ce  3bfb                  cmp       edi,ebx
  004533d0  743c                  je        short 0045340Eh
  004533d2  e899390100            call      00466D70h
  004533d7  0fbfc8                movsx     ecx,ax
  004533da  3bf9                  cmp       edi,ecx
  004533dc  7530                  jne       short 0045340Eh
  004533de  66395e10              cmp       [esi+10h],bx
  004533e2  752a                  jne       short 0045340Eh
  004533e4  0fbf5616              movsx     edx,word [esi+16h]
  004533e8  8b4e20                mov       ecx,[esi+20h]
  004533eb  668b4614              mov       ax,[esi+14h]
  004533ef  c1f90c                sar       ecx,0Ch
  004533f2  66c746100100          mov       word [esi+10h],1
  004533f8  66895e12              mov       [esi+12h],bx
  004533fc  89565c                mov       [esi+5Ch],edx
  004533ff  66894616              mov       [esi+16h],ax
  00453403  66894e14              mov       [esi+14h],cx
  00453407  66899e9a000000        mov       [esi+9Ah],bx

LAB_0045340e:
  0045340e  66837e1004            cmp       word [esi+10h],4
  00453413  751a                  jne       short 0045342Fh
  00453415  806607ef              and       byte [esi+7],0EFh
  00453419  e852390100            call      00466D70h
  0045341e  0fbfd0                movsx     edx,ax
  00453421  3bfa                  cmp       edi,edx
  00453423  750a                  jne       short 0045342Fh
  00453425  66c746100100          mov       word [esi+10h],1
  0045342b  66895e12              mov       [esi+12h],bx

LAB_0045342f:
  0045342f  bf03000000            mov       edi,3
  00453434  66397e10              cmp       [esi+10h],di
  00453438  0f858e000000          jne       near 004534CCh
  0045343e  8b4620                mov       eax,[esi+20h]
  00453441  8b0e                  mov       ecx,[esi]
  00453443  0500b0ffff            add       eax,0FFFFB000h
  00453448  80e5df                and       ch,0DFh
  0045344b  894620                mov       [esi+20h],eax
  0045344e  8b465c                mov       eax,[esi+5Ch]
  00453451  890e                  mov       [esi],ecx
  00453453  8d0c80                lea       ecx,[eax+eax*4]
  00453456  c1e103                shl       ecx,3
  00453459  2bc8                  sub       ecx,eax
  0045345b  6681448e2a60ff        add       word [esi+ecx*4+2Ah],0FF60h
  00453462  8b5620                mov       edx,[esi+20h]
  00453465  8d448e2a              lea       eax,[esi+ecx*4+2Ah]
  00453469  0fbf4614              movsx     eax,word [esi+14h]
  0045346d  c1fa0c                sar       edx,0Ch
  00453470  3bd0                  cmp       edx,eax
  00453472  7d1e                  jge       short 00453492h
  00453474  c1e00c                shl       eax,0Ch
  00453477  894620                mov       [esi+20h],eax
  0045347a  8b465c                mov       eax,[esi+5Ch]
  0045347d  8d0c80                lea       ecx,[eax+eax*4]
  00453480  c1e103                shl       ecx,3
  00453483  2bc8                  sub       ecx,eax
  00453485  66c7448e2a0010        mov       word [esi+ecx*4+2Ah],1000h
  0045348c  66c746100400          mov       word [esi+10h],4

LAB_00453492:
  00453492  66ff8e9a000000        dec       word [esi+9Ah]
  00453499  66399e9a000000        cmp       [esi+9Ah],bx
  004534a0  7d19                  jge       short 004534BBh
  004534a2  66899e9a000000        mov       [esi+9Ah],bx
  004534a9  8b15cc975e00          mov       edx,[5E97CCh]
  004534af  6a01                  push      1
  004534b1  56                    push      esi
  004534b2  52                    push      edx
  004534b3  e818090c00            call      00513DD0h
  004534b8  83c40c                add       esp,0Ch

LAB_004534bb:
  004534bb  b88a915e00            mov       eax,5E918Ah

LAB_004534c0:
  004534c0  8818                  mov       [eax],bl
  004534c2  83c04c                add       eax,4Ch
  004534c5  3d6e925e00            cmp       eax,5E926Eh
  004534ca  7ef4                  jle       short 004534C0h

LAB_004534cc:
  004534cc  66837e1001            cmp       word [esi+10h],1
  004534d1  b902000000            mov       ecx,2
  004534d6  7544                  jne       short 0045351Ch
  004534d8  8b4620                mov       eax,[esi+20h]
  004534db  66ff4612              inc       word [esi+12h]
  004534df  0500e0ffff            add       eax,0FFFFE000h
  004534e4  894620                mov       [esi+20h],eax
  004534e7  8b465c                mov       eax,[esi+5Ch]
  004534ea  8d1480                lea       edx,[eax+eax*4]
  004534ed  c1e203                shl       edx,3
  004534f0  2bd0                  sub       edx,eax
  004534f2  668344962a9c          add       word [esi+edx*4+2Ah],0FF9Ch
  004534f8  66837e1214            cmp       word [esi+12h],14h
  004534fd  8d44962a              lea       eax,[esi+edx*4+2Ah]
  00453501  7c08                  jl        short 0045350Bh
  00453503  66894e10              mov       [esi+10h],cx
  00453507  66895e12              mov       [esi+12h],bx

LAB_0045350b:
  0045350b  b88a915e00            mov       eax,5E918Ah

LAB_00453510:
  00453510  8818                  mov       [eax],bl
  00453512  83c04c                add       eax,4Ch
  00453515  3d6e925e00            cmp       eax,5E926Eh
  0045351a  7ef4                  jle       short 00453510h

LAB_0045351c:
  0045351c  66394e10              cmp       [esi+10h],cx
  00453520  0f8525010000          jne       near 0045364Bh
  00453526  b88a915e00            mov       eax,5E918Ah

LAB_0045352b:
  0045352b  8818                  mov       [eax],bl
  0045352d  83c04c                add       eax,4Ch
  00453530  3d6e925e00            cmp       eax,5E926Eh
  00453535  7ef4                  jle       short 0045352Bh
  00453537  8b4620                mov       eax,[esi+20h]
  0045353a  66ff4612              inc       word [esi+12h]
  0045353e  0500c00300            add       eax,3C000h
  00453543  894620                mov       [esi+20h],eax
  00453546  8b465c                mov       eax,[esi+5Ch]
  00453549  8d0c80                lea       ecx,[eax+eax*4]
  0045354c  c1e103                shl       ecx,3
  0045354f  2bc8                  sub       ecx,eax
  00453551  6681448e2a3408        add       word [esi+ecx*4+2Ah],834h
  00453558  66837e1205            cmp       word [esi+12h],5
  0045355d  8d448e2a              lea       eax,[esi+ecx*4+2Ah]
  00453561  0f8ce4000000          jl        near 0045364Bh
  00453567  8a5607                mov       dl,[esi+7]
  0045356a  8b0e                  mov       ecx,[esi]
  0045356c  6800000040            push      40000000h
  00453571  80ca10                or        dl,10h
  00453574  80cd20                or        ch,20h
  00453577  6a10                  push      10h
  00453579  56                    push      esi
  0045357a  66897e10              mov       [esi+10h],di
  0045357e  66895e12              mov       [esi+12h],bx
  00453582  885607                mov       [esi+7],dl
  00453585  890e                  mov       [esi],ecx
  00453587  895e50                mov       [esi+50h],ebx
  0045358a  895e58                mov       [esi+58h],ebx
  0045358d  e86e1a0e00            call      00535000h
  00453592  83c40c                add       esp,0Ch
  00453595  8bf8                  mov       edi,eax
  00453597  e8d4370100            call      00466D70h
  0045359c  0fbfd0                movsx     edx,ax
  0045359f  3bfa                  cmp       edi,edx
  004535a1  7567                  jne       short 0045360Ah
  004535a3  3bfb                  cmp       edi,ebx
  004535a5  7463                  je        short 0045360Ah
  004535a7  0fbf4616              movsx     eax,word [esi+16h]
  004535ab  c1e00c                shl       eax,0Ch
  004535ae  894654                mov       [esi+54h],eax
  004535b1  8b0dcc975e00          mov       ecx,[5E97CCh]
  004535b7  53                    push      ebx
  004535b8  56                    push      esi
  004535b9  51                    push      ecx
  004535ba  e811080c00            call      00513DD0h
  004535bf  0fbf4e16              movsx     ecx,word [esi+16h]
  004535c3  b893244992            mov       eax,92492493h
  004535c8  83c40c                add       esp,0Ch
  004535cb  f7e9                  imul      ecx
  004535cd  03d1                  add       edx,ecx
  004535cf  b904000000            mov       ecx,4
  004535d4  c1fa02                sar       edx,2
  004535d7  8bc2                  mov       eax,edx
  004535d9  c1e81f                shr       eax,1Fh
  004535dc  03d0                  add       edx,eax
  004535de  8b4644                mov       eax,[esi+44h]
  004535e1  6689969a000000        mov       [esi+9Ah],dx
  004535e8  8d1480                lea       edx,[eax+eax*4]
  004535eb  c1e203                shl       edx,3
  004535ee  2bd0                  sub       edx,eax
  004535f0  66894c9610            mov       [esi+edx*4+10h],cx
  004535f5  8b4644                mov       eax,[esi+44h]
  004535f8  8d1480                lea       edx,[eax+eax*4]
  004535fb  c1e203                shl       edx,3
  004535fe  2bd0                  sub       edx,eax
  00453600  66898c96ac000000      mov       [esi+edx*4+0ACh],cx
  00453608  eb19                  jmp       short 00453623h

LAB_0045360a:
  0045360a  0fbf4616              movsx     eax,word [esi+16h]
  0045360e  99                    cdq
  0045360f  2bc2                  sub       eax,edx
  00453611  d1f8                  sar       eax,1
  00453613  83f832                cmp       eax,32h
  00453616  7d05                  jge       short 0045361Dh
  00453618  b832000000            mov       eax,32h

LAB_0045361d:
  0045361d  c1e00c                shl       eax,0Ch
  00453620  894654                mov       [esi+54h],eax

LAB_00453623:
  00453623  a1cc975e00            mov       eax,[5E97CCh]
  00453628  53                    push      ebx
  00453629  53                    push      ebx
  0045362a  6800100000            push      1000h
  0045362f  8b88a8000000          mov       ecx,[eax+0A8h]
  00453635  6800040000            push      400h
  0045363a  8b11                  mov       edx,[ecx]
  0045363c  660fb64229            movzx     ax,byte [edx+29h]
  00453641  50                    push      eax
  00453642  56                    push      esi
  00453643  e8384a0100            call      00468080h
  00453648  83c418                add       esp,18h

LAB_0045364b:
  0045364b  5f                    pop       edi
  0045364c  5e                    pop       esi
  0045364d  5b                    pop       ebx
  0045364e  c3                    ret

15 cible(s) de saut interne : 0x004533bc, 0x0045340e, 0x0045342f, 0x00453492, 0x004534bb, 0x004534c0, 0x004534cc, 0x0045350b, 0x00453510, 0x0045351c, 0x0045352b, 0x0045360a, 0x0045361d, 0x00453623, 0x0045364b
