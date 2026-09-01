Legacy/Binaries/Blaze.exe  base 0x00400000
0x00446620  section .text  offset fichier 0x45a20  358 octets

  00446620  53                    push      ebx
  00446621  56                    push      esi
  00446622  8b74240c              mov       esi,[esp+0Ch]
  00446626  33db                  xor       ebx,ebx
  00446628  3bf3                  cmp       esi,ebx
  0044662a  57                    push      edi
  0044662b  0f8451010000          je        near 00446782h
  00446631  8b4678                mov       eax,[esi+78h]
  00446634  3bc3                  cmp       eax,ebx
  00446636  7505                  jne       short 0044663Dh
  00446638  b858e55d00            mov       eax,5DE558h

LAB_0044663d:
  0044663d  8b481c                mov       ecx,[eax+1Ch]
  00446640  894e1c                mov       [esi+1Ch],ecx
  00446643  8b5020                mov       edx,[eax+20h]
  00446646  81c200000500          add       edx,50000h
  0044664c  66395e10              cmp       [esi+10h],bx
  00446650  895620                mov       [esi+20h],edx
  00446653  8b4024                mov       eax,[eax+24h]
  00446656  894624                mov       [esi+24h],eax
  00446659  b801000000            mov       eax,1
  0044665e  7542                  jne       short 004466A2h
  00446660  0fbf4e28              movsx     ecx,word [esi+28h]
  00446664  0fbf562a              movsx     edx,word [esi+2Ah]
  00446668  894e50                mov       [esi+50h],ecx
  0044666b  895654                mov       [esi+54h],edx
  0044666e  0fbf4e2c              movsx     ecx,word [esi+2Ch]
  00446672  668b5644              mov       dx,[esi+44h]
  00446676  894e58                mov       [esi+58h],ecx
  00446679  668b4e48              mov       cx,[esi+48h]
  0044667d  66895628              mov       [esi+28h],dx
  00446681  668b564c              mov       dx,[esi+4Ch]
  00446685  c6460422              mov       byte [esi+4],22h
  00446689  66894610              mov       [esi+10h],ax
  0044668d  66895e12              mov       [esi+12h],bx
  00446691  885e38                mov       [esi+38h],bl
  00446694  885e39                mov       [esi+39h],bl
  00446697  885e3a                mov       [esi+3Ah],bl
  0044669a  66894e2a              mov       [esi+2Ah],cx
  0044669e  6689562c              mov       [esi+2Ch],dx

LAB_004466a2:
  004466a2  668b4e32              mov       cx,[esi+32h]
  004466a6  6681c1c800            add       cx,0C8h
  004466ab  81e1ff0f0000          and       ecx,0FFFh
  004466b1  66394610              cmp       [esi+10h],ax
  004466b5  66894e32              mov       [esi+32h],cx
  004466b9  b902000000            mov       ecx,2
  004466be  0f859c000000          jne       near 00446760h
  004466c4  8a4638                mov       al,[esi+38h]
  004466c7  bac8000000            mov       edx,0C8h
  004466cc  8bf8                  mov       edi,eax
  004466ce  81e7ff000000          and       edi,0FFh
  004466d4  2bd7                  sub       edx,edi
  004466d6  c1fa03                sar       edx,3
  004466d9  02d0                  add       dl,al
  004466db  8a4639                mov       al,[esi+39h]
  004466de  8bf8                  mov       edi,eax
  004466e0  885638                mov       [esi+38h],dl
  004466e3  81e7ff000000          and       edi,0FFh
  004466e9  bac8000000            mov       edx,0C8h
  004466ee  2bd7                  sub       edx,edi
  004466f0  c1fa03                sar       edx,3
  004466f3  02d0                  add       dl,al
  004466f5  8a463a                mov       al,[esi+3Ah]
  004466f8  8bf8                  mov       edi,eax
  004466fa  885639                mov       [esi+39h],dl
  004466fd  81e7ff000000          and       edi,0FFh
  00446703  bac8000000            mov       edx,0C8h
  00446708  2bd7                  sub       edx,edi
  0044670a  8b7e50                mov       edi,[esi+50h]
  0044670d  c1fa03                sar       edx,3
  00446710  02d0                  add       dl,al
  00446712  668b4628              mov       ax,[esi+28h]
  00446716  88563a                mov       [esi+3Ah],dl
  00446719  0fbfd0                movsx     edx,ax
  0044671c  2bfa                  sub       edi,edx
  0044671e  c1ff03                sar       edi,3
  00446721  03f8                  add       edi,eax
  00446723  668b462a              mov       ax,[esi+2Ah]
  00446727  0fbfd0                movsx     edx,ax
  0044672a  66897e28              mov       [esi+28h],di
  0044672e  8b7e54                mov       edi,[esi+54h]
  00446731  2bfa                  sub       edi,edx
  00446733  c1ff03                sar       edi,3
  00446736  03f8                  add       edi,eax
  00446738  668b462c              mov       ax,[esi+2Ch]
  0044673c  0fbfd0                movsx     edx,ax
  0044673f  66897e2a              mov       [esi+2Ah],di
  00446743  8b7e58                mov       edi,[esi+58h]
  00446746  2bfa                  sub       edi,edx
  00446748  c1ff03                sar       edi,3
  0044674b  03f8                  add       edi,eax
  0044674d  66ff4612              inc       word [esi+12h]
  00446751  66837e121e            cmp       word [esi+12h],1Eh
  00446756  66897e2c              mov       [esi+2Ch],di
  0044675a  7c04                  jl        short 00446760h
  0044675c  66894e10              mov       [esi+10h],cx

LAB_00446760:
  00446760  66394e10              cmp       [esi+10h],cx
  00446764  751c                  jne       short 00446782h
  00446766  6aec                  push      0FFFFFFECh
  00446768  6aec                  push      0FFFFFFECh
  0044676a  6aec                  push      0FFFFFFECh
  0044676c  56                    push      esi
  0044676d  e86ee6fbff            call      00404DE0h
  00446772  8a4638                mov       al,[esi+38h]
  00446775  83c410                add       esp,10h
  00446778  3ac3                  cmp       al,bl
  0044677a  7506                  jne       short 00446782h
  0044677c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00446782:
  00446782  5f                    pop       edi
  00446783  5e                    pop       esi
  00446784  5b                    pop       ebx
  00446785  c3                    ret

4 cible(s) de saut interne : 0x0044663d, 0x004466a2, 0x00446760, 0x00446782
