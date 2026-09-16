
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004071e0  section .text  offset fichier 0x65e0  1568 octets

  004071e0  51                    push      ecx
  004071e1  53                    push      ebx
  004071e2  55                    push      ebp
  004071e3  56                    push      esi
  004071e4  8b742414              mov       esi,[esp+14h]
  004071e8  57                    push      edi
  004071e9  66837e1000            cmp       word [esi+10h],0
  004071ee  0f8533050000          jne       near 00407727h
  004071f4  66c7462e1400          mov       word [esi+2Eh],14h
  004071fa  c7463cc8f15400        mov       dword [esi+3Ch],54F1C8h
  00407201  e83aff1300            call      00547140h
  00407206  99                    cdq
  00407207  b958020000            mov       ecx,258h
  0040720c  f7f9                  idiv      ecx
  0040720e  81c270170000          add       edx,1770h
  00407214  66895628              mov       [esi+28h],dx
  00407218  e823ff1300            call      00547140h
  0040721d  99                    cdq
  0040721e  b958020000            mov       ecx,258h
  00407223  f7f9                  idiv      ecx
  00407225  81c270170000          add       edx,1770h
  0040722b  66837e1202            cmp       word [esi+12h],2
  00407230  6689562a              mov       [esi+2Ah],dx
  00407234  7411                  je        short 00407247h
  00407236  e805ff1300            call      00547140h
  0040723b  99                    cdq
  0040723c  b906000000            mov       ecx,6
  00407241  f7f9                  idiv      ecx
  00407243  8bca                  mov       ecx,edx
  00407245  eb13                  jmp       short 0040725Ah

LAB_00407247:
  00407247  e8f4fe1300            call      00547140h
  0040724c  99                    cdq
  0040724d  33c2                  xor       eax,edx
  0040724f  2bc2                  sub       eax,edx
  00407251  83e007                and       eax,7
  00407254  33c2                  xor       eax,edx
  00407256  8bc8                  mov       ecx,eax
  00407258  2bca                  sub       ecx,edx

LAB_0040725a:
  0040725a  6685c9                test      cx,cx
  0040725d  894c2418              mov       [esp+18h],ecx
  00407261  740a                  je        short 0040726Dh
  00407263  6683f904              cmp       cx,4
  00407267  0f85db000000          jne       near 00407348h

LAB_0040726d:
  0040726d  0fbf5632              movsx     edx,word [esi+32h]
  00407271  81c200020000          add       edx,200h
  00407277  52                    push      edx
  00407278  e89fe71300            call      00545A1Ch
  0040727d  8d0440                lea       eax,[eax+eax*2]
  00407280  8d0480                lea       eax,[eax+eax*4]
  00407283  8d3c80                lea       edi,[eax+eax*4]
  00407286  e8b5fe1300            call      00547140h
  0040728b  99                    cdq
  0040728c  b914000000            mov       ecx,14h
  00407291  f7f9                  idiv      ecx
  00407293  83ea0a                sub       edx,0Ah
  00407296  c1e20c                shl       edx,0Ch
  00407299  8d3c7a                lea       edi,[edx+edi*2]
  0040729c  0fbf5632              movsx     edx,word [esi+32h]
  004072a0  81c200020000          add       edx,200h
  004072a6  52                    push      edx
  004072a7  e86ae71300            call      00545A16h
  004072ac  8d0440                lea       eax,[eax+eax*2]
  004072af  8d0480                lea       eax,[eax+eax*4]
  004072b2  8d1c80                lea       ebx,[eax+eax*4]
  004072b5  e886fe1300            call      00547140h
  004072ba  99                    cdq
  004072bb  b914000000            mov       ecx,14h
  004072c0  f7f9                  idiv      ecx
  004072c2  83ea0a                sub       edx,0Ah
  004072c5  c1e20c                shl       edx,0Ch
  004072c8  8d2c5a                lea       ebp,[edx+ebx*2]
  004072cb  0fbf5632              movsx     edx,word [esi+32h]
  004072cf  81ea00020000          sub       edx,200h
  004072d5  52                    push      edx
  004072d6  e841e71300            call      00545A1Ch
  004072db  8d0440                lea       eax,[eax+eax*2]
  004072de  8d0480                lea       eax,[eax+eax*4]
  004072e1  8d1c80                lea       ebx,[eax+eax*4]
  004072e4  e857fe1300            call      00547140h
  004072e9  99                    cdq
  004072ea  b914000000            mov       ecx,14h
  004072ef  f7f9                  idiv      ecx
  004072f1  0fbf4632              movsx     eax,word [esi+32h]
  004072f5  2d00020000            sub       eax,200h
  004072fa  50                    push      eax
  004072fb  83ea0a                sub       edx,0Ah
  004072fe  c1e20c                shl       edx,0Ch
  00407301  8d145a                lea       edx,[edx+ebx*2]
  00407304  89542414              mov       [esp+14h],edx
  00407308  e809e71300            call      00545A16h
  0040730d  8d0440                lea       eax,[eax+eax*2]
  00407310  8d0480                lea       eax,[eax+eax*4]
  00407313  8d1c80                lea       ebx,[eax+eax*4]
  00407316  e825fe1300            call      00547140h
  0040731b  99                    cdq
  0040731c  b914000000            mov       ecx,14h
  00407321  f7f9                  idiv      ecx
  00407323  83ea0a                sub       edx,0Ah
  00407326  c1e20c                shl       edx,0Ch
  00407329  8d1c5a                lea       ebx,[edx+ebx*2]
  0040732c  e80ffe1300            call      00547140h
  00407331  99                    cdq
  00407332  33c2                  xor       eax,edx
  00407334  8b4c2418              mov       ecx,[esp+18h]
  00407338  2bc2                  sub       eax,edx
  0040733a  83e007                and       eax,7
  0040733d  33c2                  xor       eax,edx
  0040733f  2bc2                  sub       eax,edx
  00407341  ba08000000            mov       edx,8
  00407346  eb14                  jmp       short 0040735Ch

LAB_00407348:
  00407348  8b7c2418              mov       edi,[esp+18h]
  0040734c  8b6c2418              mov       ebp,[esp+18h]
  00407350  8b5c2418              mov       ebx,[esp+18h]
  00407354  8b542418              mov       edx,[esp+18h]
  00407358  8b442418              mov       eax,[esp+18h]

LAB_0040735c:
  0040735c  6683f901              cmp       cx,1
  00407360  7415                  je        short 00407377h
  00407362  66837e1202            cmp       word [esi+12h],2
  00407367  0f85e3000000          jne       near 00407450h
  0040736d  6683f906              cmp       cx,6
  00407371  0f8cd9000000          jl        near 00407450h

LAB_00407377:
  00407377  0fbf5632              movsx     edx,word [esi+32h]
  0040737b  81ea00020000          sub       edx,200h
  00407381  52                    push      edx
  00407382  e895e61300            call      00545A1Ch
  00407387  8d0440                lea       eax,[eax+eax*2]
  0040738a  8d0480                lea       eax,[eax+eax*4]
  0040738d  8d3c80                lea       edi,[eax+eax*4]
  00407390  e8abfd1300            call      00547140h
  00407395  99                    cdq
  00407396  b914000000            mov       ecx,14h
  0040739b  f7f9                  idiv      ecx
  0040739d  83ea0a                sub       edx,0Ah
  004073a0  c1e20c                shl       edx,0Ch
  004073a3  8d3c7a                lea       edi,[edx+edi*2]
  004073a6  0fbf5632              movsx     edx,word [esi+32h]
  004073aa  81ea00020000          sub       edx,200h
  004073b0  52                    push      edx
  004073b1  e860e61300            call      00545A16h
  004073b6  8d0440                lea       eax,[eax+eax*2]
  004073b9  8d0480                lea       eax,[eax+eax*4]
  004073bc  8d1c80                lea       ebx,[eax+eax*4]
  004073bf  e87cfd1300            call      00547140h
  004073c4  99                    cdq
  004073c5  b914000000            mov       ecx,14h
  004073ca  f7f9                  idiv      ecx
  004073cc  83ea0a                sub       edx,0Ah
  004073cf  c1e20c                shl       edx,0Ch
  004073d2  8d2c5a                lea       ebp,[edx+ebx*2]
  004073d5  0fbf5632              movsx     edx,word [esi+32h]
  004073d9  81ea00040000          sub       edx,400h
  004073df  52                    push      edx
  004073e0  e837e61300            call      00545A1Ch
  004073e5  8d0440                lea       eax,[eax+eax*2]
  004073e8  8d0480                lea       eax,[eax+eax*4]
  004073eb  8d1c80                lea       ebx,[eax+eax*4]
  004073ee  e84dfd1300            call      00547140h
  004073f3  99                    cdq
  004073f4  b914000000            mov       ecx,14h
  004073f9  f7f9                  idiv      ecx
  004073fb  0fbf4632              movsx     eax,word [esi+32h]
  004073ff  2d00040000            sub       eax,400h
  00407404  50                    push      eax
  00407405  83ea0a                sub       edx,0Ah
  00407408  c1e20c                shl       edx,0Ch
  0040740b  8d145a                lea       edx,[edx+ebx*2]
  0040740e  89542414              mov       [esp+14h],edx
  00407412  e8ffe51300            call      00545A16h
  00407417  8d0440                lea       eax,[eax+eax*2]
  0040741a  8d0480                lea       eax,[eax+eax*4]
  0040741d  8d1c80                lea       ebx,[eax+eax*4]
  00407420  e81bfd1300            call      00547140h
  00407425  99                    cdq
  00407426  b914000000            mov       ecx,14h
  0040742b  f7f9                  idiv      ecx
  0040742d  83ea0a                sub       edx,0Ah
  00407430  c1e20c                shl       edx,0Ch
  00407433  8d1c5a                lea       ebx,[edx+ebx*2]
  00407436  e805fd1300            call      00547140h
  0040743b  99                    cdq
  0040743c  33c2                  xor       eax,edx
  0040743e  8b4c2418              mov       ecx,[esp+18h]
  00407442  2bc2                  sub       eax,edx
  00407444  83e003                and       eax,3
  00407447  33c2                  xor       eax,edx
  00407449  2bc2                  sub       eax,edx
  0040744b  ba04000000            mov       edx,4

LAB_00407450:
  00407450  6683f902              cmp       cx,2
  00407454  740a                  je        short 00407460h
  00407456  6683f905              cmp       cx,5
  0040745a  0f85d9000000          jne       near 00407539h

LAB_00407460:
  00407460  0fbf5632              movsx     edx,word [esi+32h]
  00407464  81ea00040000          sub       edx,400h
  0040746a  52                    push      edx
  0040746b  e8ace51300            call      00545A1Ch
  00407470  8d0440                lea       eax,[eax+eax*2]
  00407473  8d0480                lea       eax,[eax+eax*4]
  00407476  8d3c80                lea       edi,[eax+eax*4]
  00407479  e8c2fc1300            call      00547140h
  0040747e  99                    cdq
  0040747f  b914000000            mov       ecx,14h
  00407484  f7f9                  idiv      ecx
  00407486  83ea0a                sub       edx,0Ah
  00407489  c1e20c                shl       edx,0Ch
  0040748c  8d3c7a                lea       edi,[edx+edi*2]
  0040748f  0fbf5632              movsx     edx,word [esi+32h]
  00407493  81ea00040000          sub       edx,400h
  00407499  52                    push      edx
  0040749a  e877e51300            call      00545A16h
  0040749f  8d0440                lea       eax,[eax+eax*2]
  004074a2  8d0480                lea       eax,[eax+eax*4]
  004074a5  8d1c80                lea       ebx,[eax+eax*4]
  004074a8  e893fc1300            call      00547140h
  004074ad  99                    cdq
  004074ae  b914000000            mov       ecx,14h
  004074b3  f7f9                  idiv      ecx
  004074b5  83ea0a                sub       edx,0Ah
  004074b8  c1e20c                shl       edx,0Ch
  004074bb  8d2c5a                lea       ebp,[edx+ebx*2]
  004074be  0fbf5632              movsx     edx,word [esi+32h]
  004074c2  81c200040000          add       edx,400h
  004074c8  52                    push      edx
  004074c9  e84ee51300            call      00545A1Ch
  004074ce  8d0440                lea       eax,[eax+eax*2]
  004074d1  8d0480                lea       eax,[eax+eax*4]
  004074d4  8d1c80                lea       ebx,[eax+eax*4]
  004074d7  e864fc1300            call      00547140h
  004074dc  99                    cdq
  004074dd  b914000000            mov       ecx,14h
  004074e2  f7f9                  idiv      ecx
  004074e4  0fbf4632              movsx     eax,word [esi+32h]
  004074e8  0500040000            add       eax,400h
  004074ed  50                    push      eax
  004074ee  83ea0a                sub       edx,0Ah
  004074f1  c1e20c                shl       edx,0Ch
  004074f4  8d145a                lea       edx,[edx+ebx*2]
  004074f7  89542414              mov       [esp+14h],edx
  004074fb  e816e51300            call      00545A16h
  00407500  8d0440                lea       eax,[eax+eax*2]
  00407503  8d0480                lea       eax,[eax+eax*4]
  00407506  8d1c80                lea       ebx,[eax+eax*4]
  00407509  e832fc1300            call      00547140h
  0040750e  99                    cdq
  0040750f  b914000000            mov       ecx,14h
  00407514  f7f9                  idiv      ecx
  00407516  83ea0a                sub       edx,0Ah
  00407519  c1e20c                shl       edx,0Ch
  0040751c  8d1c5a                lea       ebx,[edx+ebx*2]
  0040751f  e81cfc1300            call      00547140h
  00407524  99                    cdq
  00407525  33c2                  xor       eax,edx
  00407527  8b4c2418              mov       ecx,[esp+18h]
  0040752b  2bc2                  sub       eax,edx
  0040752d  83e007                and       eax,7
  00407530  33c2                  xor       eax,edx
  00407532  2bc2                  sub       eax,edx
  00407534  ba08000000            mov       edx,8

LAB_00407539:
  00407539  6683f903              cmp       cx,3
  0040753d  0f85d5000000          jne       near 00407618h
  00407543  0fbf5632              movsx     edx,word [esi+32h]
  00407547  81c200040000          add       edx,400h
  0040754d  52                    push      edx
  0040754e  e8c9e41300            call      00545A1Ch
  00407553  8d0440                lea       eax,[eax+eax*2]
  00407556  8d0480                lea       eax,[eax+eax*4]
  00407559  8d3c80                lea       edi,[eax+eax*4]
  0040755c  e8dffb1300            call      00547140h
  00407561  99                    cdq
  00407562  b914000000            mov       ecx,14h
  00407567  f7f9                  idiv      ecx
  00407569  83ea0a                sub       edx,0Ah
  0040756c  c1e20c                shl       edx,0Ch
  0040756f  8d3c7a                lea       edi,[edx+edi*2]
  00407572  0fbf5632              movsx     edx,word [esi+32h]
  00407576  81c200040000          add       edx,400h
  0040757c  52                    push      edx
  0040757d  e894e41300            call      00545A16h
  00407582  8d0440                lea       eax,[eax+eax*2]
  00407585  8d0480                lea       eax,[eax+eax*4]
  00407588  8d1c80                lea       ebx,[eax+eax*4]
  0040758b  e8b0fb1300            call      00547140h
  00407590  99                    cdq
  00407591  b914000000            mov       ecx,14h
  00407596  f7f9                  idiv      ecx
  00407598  83ea0a                sub       edx,0Ah
  0040759b  c1e20c                shl       edx,0Ch
  0040759e  8d2c5a                lea       ebp,[edx+ebx*2]
  004075a1  0fbf5632              movsx     edx,word [esi+32h]
  004075a5  81c200020000          add       edx,200h
  004075ab  52                    push      edx
  004075ac  e86be41300            call      00545A1Ch
  004075b1  8d0440                lea       eax,[eax+eax*2]
  004075b4  8d0480                lea       eax,[eax+eax*4]
  004075b7  8d1c80                lea       ebx,[eax+eax*4]
  004075ba  e881fb1300            call      00547140h
  004075bf  99                    cdq
  004075c0  b914000000            mov       ecx,14h
  004075c5  f7f9                  idiv      ecx
  004075c7  0fbf4632              movsx     eax,word [esi+32h]
  004075cb  0500020000            add       eax,200h
  004075d0  50                    push      eax
  004075d1  83ea0a                sub       edx,0Ah
  004075d4  c1e20c                shl       edx,0Ch
  004075d7  8d145a                lea       edx,[edx+ebx*2]
  004075da  89542414              mov       [esp+14h],edx
  004075de  e833e41300            call      00545A16h
  004075e3  8d0440                lea       eax,[eax+eax*2]
  004075e6  8d0480                lea       eax,[eax+eax*4]
  004075e9  8d1c80                lea       ebx,[eax+eax*4]
  004075ec  e84ffb1300            call      00547140h
  004075f1  99                    cdq
  004075f2  b914000000            mov       ecx,14h
  004075f7  f7f9                  idiv      ecx
  004075f9  83ea0a                sub       edx,0Ah
  004075fc  c1e20c                shl       edx,0Ch
  004075ff  8d1c5a                lea       ebx,[edx+ebx*2]
  00407602  e839fb1300            call      00547140h
  00407607  99                    cdq
  00407608  33c2                  xor       eax,edx
  0040760a  2bc2                  sub       eax,edx
  0040760c  83e003                and       eax,3
  0040760f  33c2                  xor       eax,edx
  00407611  2bc2                  sub       eax,edx
  00407613  ba04000000            mov       edx,4

LAB_00407618:
  00407618  0fbfc0                movsx     eax,ax
  0040761b  c1ff0c                sar       edi,0Ch
  0040761e  c1fd0c                sar       ebp,0Ch
  00407621  40                    inc       eax
  00407622  0fbfca                movsx     ecx,dx
  00407625  8b542410              mov       edx,[esp+10h]
  00407629  89442418              mov       [esp+18h],eax
  0040762d  c1fa0c                sar       edx,0Ch
  00407630  8bc7                  mov       eax,edi
  00407632  2bc2                  sub       eax,edx
  00407634  99                    cdq
  00407635  f7f9                  idiv      ecx
  00407637  8b561c                mov       edx,[esi+1Ch]
  0040763a  c1fb0c                sar       ebx,0Ch
  0040763d  0faf442418            imul      eax,[esp+18h]
  00407642  2bf8                  sub       edi,eax
  00407644  8bc5                  mov       eax,ebp
  00407646  c1e70c                shl       edi,0Ch
  00407649  03d7                  add       edx,edi
  0040764b  2bc3                  sub       eax,ebx
  0040764d  89561c                mov       [esi+1Ch],edx
  00407650  99                    cdq
  00407651  f7f9                  idiv      ecx
  00407653  0faf442418            imul      eax,[esp+18h]
  00407658  2be8                  sub       ebp,eax
  0040765a  8b4624                mov       eax,[esi+24h]
  0040765d  c1e50c                shl       ebp,0Ch
  00407660  03c5                  add       eax,ebp
  00407662  894624                mov       [esi+24h],eax
  00407665  e8d6fa1300            call      00547140h
  0040766a  99                    cdq
  0040766b  b914000000            mov       ecx,14h
  00407670  33ed                  xor       ebp,ebp
  00407672  f7f9                  idiv      ecx
  00407674  b80060ffff            mov       eax,0FFFF6000h
  00407679  8b4e1c                mov       ecx,[esi+1Ch]
  0040767c  bb01000000            mov       ebx,1
  00407681  894e50                mov       [esi+50h],ecx
  00407684  c6460a09              mov       byte [esi+0Ah],9
  00407688  c6460b00              mov       byte [esi+0Bh],0
  0040768c  66895e10              mov       [esi+10h],bx
  00407690  c1e20c                shl       edx,0Ch
  00407693  2bc2                  sub       eax,edx
  00407695  8b5620                mov       edx,[esi+20h]
  00407698  03d0                  add       edx,eax
  0040769a  8bc2                  mov       eax,edx
  0040769c  895620                mov       [esi+20h],edx
  0040769f  8b5624                mov       edx,[esi+24h]
  004076a2  894654                mov       [esi+54h],eax
  004076a5  668b4612              mov       ax,[esi+12h]
  004076a9  895658                mov       [esi+58h],edx
  004076ac  663bc5                cmp       ax,bp
  004076af  7507                  jne       short 004076B8h
  004076b1  6689ae9a000000        mov       [esi+9Ah],bp

LAB_004076b8:
  004076b8  663bc3                cmp       ax,bx
  004076bb  7514                  jne       short 004076D1h
  004076bd  b900040000            mov       ecx,400h
  004076c2  66899e9a000000        mov       [esi+9Ah],bx
  004076c9  66014e28              add       [esi+28h],cx
  004076cd  66014e2a              add       [esi+2Ah],cx

LAB_004076d1:
  004076d1  bf02000000            mov       edi,2
  004076d6  663bc7                cmp       ax,di
  004076d9  7514                  jne       short 004076EFh
  004076db  b8e0fcffff            mov       eax,0FFFFFCE0h
  004076e0  6689be9a000000        mov       [esi+9Ah],di
  004076e7  66014628              add       [esi+28h],ax
  004076eb  6601462a              add       [esi+2Ah],ax

LAB_004076ef:
  004076ef  668b869a000000        mov       ax,[esi+9Ah]
  004076f6  663bc3                cmp       ax,bx
  004076f9  7405                  je        short 00407700h
  004076fb  663bc7                cmp       ax,di
  004076fe  7502                  jne       short 00407702h

LAB_00407700:
  00407700  8bfb                  mov       edi,ebx

LAB_00407702:
  00407702  e839fa1300            call      00547140h
  00407707  99                    cdq
  00407708  b905000000            mov       ecx,5
  0040770d  66896e14              mov       [esi+14h],bp
  00407711  f7f9                  idiv      ecx
  00407713  66896e16              mov       [esi+16h],bp
  00407717  c6464000              mov       byte [esi+40h],0
  0040771b  83c208                add       edx,8
  0040771e  0fafd7                imul      edx,edi
  00407721  66895612              mov       [esi+12h],dx
  00407725  eb05                  jmp       short 0040772Ch

LAB_00407727:
  00407727  bb01000000            mov       ebx,1

LAB_0040772c:
  0040772c  66395e10              cmp       [esi+10h],bx
  00407730  7567                  jne       short 00407799h
  00407732  8b2e                  mov       ebp,[esi]
  00407734  81cd00400000          or        ebp,4000h
  0040773a  66ff4614              inc       word [esi+14h]
  0040773e  668b4e14              mov       cx,[esi+14h]
  00407742  892e                  mov       [esi],ebp
  00407744  6683f903              cmp       cx,3
  00407748  8bc5                  mov       eax,ebp
  0040774a  7e16                  jle       short 00407762h
  0040774c  668b8e9a000000        mov       cx,[esi+9Ah]
  00407753  6685c9                test      cx,cx
  00407756  7405                  je        short 0040775Dh
  00407758  663bcb                cmp       cx,bx
  0040775b  7505                  jne       short 00407762h

LAB_0040775d:
  0040775d  80e4bf                and       ah,0BFh
  00407760  8906                  mov       [esi],eax

LAB_00407762:
  00407762  e8d9f91300            call      00547140h
  00407767  99                    cdq
  00407768  b90a000000            mov       ecx,0Ah
  0040776d  f7f9                  idiv      ecx
  0040776f  66837e1408            cmp       word [esi+14h],8
  00407774  7f05                  jg        short 0040777Bh
  00407776  6685d2                test      dx,dx
  00407779  751e                  jne       short 00407799h

LAB_0040777b:
  0040777b  8b0e                  mov       ecx,[esi]
  0040777d  8b4620                mov       eax,[esi+20h]
  00407780  80e5bf                and       ch,0BFh
  00407783  050010ffff            add       eax,0FFFF1000h
  00407788  66c746100200          mov       word [esi+10h],2
  0040778e  66c746140000          mov       word [esi+14h],0
  00407794  890e                  mov       [esi],ecx
  00407796  894620                mov       [esi+20h],eax

LAB_00407799:
  00407799  66837e1002            cmp       word [esi+10h],2
  0040779e  754f                  jne       short 004077EFh
  004077a0  e89bf91300            call      00547140h
  004077a5  99                    cdq
  004077a6  b914000000            mov       ecx,14h
  004077ab  f7f9                  idiv      ecx
  004077ad  8b4e1c                mov       ecx,[esi+1Ch]
  004077b0  83ea0a                sub       edx,0Ah
  004077b3  c1e20c                shl       edx,0Ch
  004077b6  03ca                  add       ecx,edx
  004077b8  894e1c                mov       [esi+1Ch],ecx
  004077bb  e880f91300            call      00547140h
  004077c0  99                    cdq
  004077c1  b914000000            mov       ecx,14h
  004077c6  8b5e24                mov       ebx,[esi+24h]
  004077c9  f7f9                  idiv      ecx
  004077cb  83ea0a                sub       edx,0Ah
  004077ce  c1e20c                shl       edx,0Ch
  004077d1  03da                  add       ebx,edx
  004077d3  66ff4616              inc       word [esi+16h]
  004077d7  668b4616              mov       ax,[esi+16h]
  004077db  895e24                mov       [esi+24h],ebx
  004077de  66c1f802              sar       ax,2
  004077e2  3c04                  cmp       al,4
  004077e4  884640                mov       [esi+40h],al
  004077e7  7506                  jne       short 004077EFh
  004077e9  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004077ef:
  004077ef  5f                    pop       edi
  004077f0  5e                    pop       esi
  004077f1  5d                    pop       ebp
  004077f2  5b                    pop       ebx
  004077f3  59                    pop       ecx
  004077f4  c3                    ret
  004077f5  90                    nop
  004077f6  90                    nop
  004077f7  90                    nop
  004077f8  90                    nop
  004077f9  90                    nop
  004077fa  90                    nop
  004077fb  90                    nop
  004077fc  90                    nop
  004077fd  90                    nop
  004077fe  90                    nop
  004077ff  90                    nop

22 cible(s) de saut interne : 0x00407247, 0x0040725a, 0x0040726d, 0x00407348, 0x0040735c, 0x00407377, 0x00407450, 
0x00407460, 0x00407539, 0x00407618, 0x004076b8, 0x004076d1, 0x004076ef, 0x00407700, 0x00407702, 0x00407727, 
0x0040772c, 0x0040775d, 0x00407762, 0x0040777b, 0x00407799, 0x004077ef


