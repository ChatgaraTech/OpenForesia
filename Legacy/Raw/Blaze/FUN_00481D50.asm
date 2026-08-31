Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00481d50  section .text  offset fichier 0x81150  212 octets

  00481d50  8b442404              mov       eax,[esp+4]
  00481d54  53                    push      ebx
  00481d55  56                    push      esi
  00481d56  57                    push      edi
  00481d57  8b08                  mov       ecx,[eax]
  00481d59  f7c100000002          test      ecx,2000000h
  00481d5f  7423                  je        short 00481D84h
  00481d61  8b4844                mov       ecx,[eax+44h]
  00481d64  8b5038                mov       edx,[eax+38h]
  00481d67  2bd1                  sub       edx,ecx
  00481d69  8bca                  mov       ecx,edx
  00481d6b  895038                mov       [eax+38h],edx
  00481d6e  f7c1ffffff00          test      ecx,0FFFFFFh
  00481d74  0f85a2000000          jne       near 00481E1Ch
  00481d7a  c70000000000          mov       dword [eax],0
  00481d80  5f                    pop       edi
  00481d81  5e                    pop       esi
  00481d82  5b                    pop       ebx
  00481d83  c3                    ret

LAB_00481d84:
  00481d84  f6c102                test      cl,2
  00481d87  741f                  je        short 00481DA8h
  00481d89  8b506c                mov       edx,[eax+6Ch]
  00481d8c  8b12                  mov       edx,[edx]
  00481d8e  81e200000002          and       edx,2000000h
  00481d94  0bd1                  or        edx,ecx
  00481d96  668b4816              mov       cx,[eax+16h]
  00481d9a  66014832              add       [eax+32h],cx
  00481d9e  66ff4010              inc       word [eax+10h]
  00481da2  8910                  mov       [eax],edx
  00481da4  5f                    pop       edi
  00481da5  5e                    pop       esi
  00481da6  5b                    pop       ebx
  00481da7  c3                    ret

LAB_00481da8:
  00481da8  0fbf4812              movsx     ecx,word [eax+12h]
  00481dac  0fbf5028              movsx     edx,word [eax+28h]
  00481db0  0fbf7814              movsx     edi,word [eax+14h]
  00481db4  2bca                  sub       ecx,edx
  00481db6  668b502a              mov       dx,[eax+2Ah]
  00481dba  0fbfda                movsx     ebx,dx
  00481dbd  2bfb                  sub       edi,ebx
  00481dbf  c1f903                sar       ecx,3
  00481dc2  66014828              add       [eax+28h],cx
  00481dc6  668b7028              mov       si,[eax+28h]
  00481dca  c1ff03                sar       edi,3
  00481dcd  03fa                  add       edi,edx
  00481dcf  83f920                cmp       ecx,20h
  00481dd2  6689782a              mov       [eax+2Ah],di
  00481dd6  6689702c              mov       [eax+2Ch],si
  00481dda  7d38                  jge       short 00481E14h
  00481ddc  8b4838                mov       ecx,[eax+38h]
  00481ddf  8b7048                mov       esi,[eax+48h]
  00481de2  8bd1                  mov       edx,ecx
  00481de4  81e2ffffff00          and       edx,0FFFFFFh
  00481dea  3bd6                  cmp       edx,esi
  00481dec  7318                  jae       short 00481E06h
  00481dee  8b5044                mov       edx,[eax+44h]
  00481df1  03d1                  add       edx,ecx
  00481df3  668b4816              mov       cx,[eax+16h]
  00481df7  66014832              add       [eax+32h],cx
  00481dfb  66ff4010              inc       word [eax+10h]
  00481dff  895038                mov       [eax+38h],edx
  00481e02  5f                    pop       edi
  00481e03  5e                    pop       esi
  00481e04  5b                    pop       ebx
  00481e05  c3                    ret

LAB_00481e06:
  00481e06  8b486c                mov       ecx,[eax+6Ch]
  00481e09  66ff4916              dec       word [ecx+16h]
  00481e0d  8b08                  mov       ecx,[eax]
  00481e0f  83c902                or        ecx,2
  00481e12  8908                  mov       [eax],ecx

LAB_00481e14:
  00481e14  668b4816              mov       cx,[eax+16h]
  00481e18  66014832              add       [eax+32h],cx

LAB_00481e1c:
  00481e1c  66ff4010              inc       word [eax+10h]
  00481e20  5f                    pop       edi
  00481e21  5e                    pop       esi
  00481e22  5b                    pop       ebx
  00481e23  c3                    ret

5 cible(s) de saut interne : 0x00481d84, 0x00481da8, 0x00481e06, 0x00481e14, 0x00481e1c
