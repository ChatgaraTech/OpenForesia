Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00481250  section .text  offset fichier 0x80650  1050 octets

  00481250  83ec28                sub       esp,28h
  00481253  53                    push      ebx
  00481254  55                    push      ebp
  00481255  56                    push      esi
  00481256  8b742438              mov       esi,[esp+38h]
  0048125a  57                    push      edi
  0048125b  66c744242000fc        mov       word [esp+20h],0FC00h
  00481262  8b06                  mov       eax,[esi]
  00481264  66c744242280ff        mov       word [esp+22h],0FF80h
  0048126b  a900000042            test      eax,42000000h
  00481270  66c74424243200        mov       word [esp+24h],32h
  00481277  66c7442426c800        mov       word [esp+26h],0C8h
  0048127e  66c74424280000        mov       word [esp+28h],0
  00481285  66c744242a8000        mov       word [esp+2Ah],80h
  0048128c  743a                  je        short 004812C8h
  0048128e  668b4e2a              mov       cx,[esi+2Ah]
  00481292  668bc1                mov       ax,cx
  00481295  66c1f802              sar       ax,2
  00481299  2bc8                  sub       ecx,eax
  0048129b  668b462c              mov       ax,[esi+2Ch]
  0048129f  668bd0                mov       dx,ax
  004812a2  66894e2a              mov       [esi+2Ah],cx
  004812a6  66c1fa02              sar       dx,2
  004812aa  2bc2                  sub       eax,edx
  004812ac  6683f908              cmp       cx,8
  004812b0  6689462c              mov       [esi+2Ch],ax
  004812b4  0f8da8030000          jge       near 00481662h
  004812ba  c70600000000          mov       dword [esi],0
  004812c0  5f                    pop       edi
  004812c1  5e                    pop       esi
  004812c2  5d                    pop       ebp
  004812c3  5b                    pop       ebx
  004812c4  83c428                add       esp,28h
  004812c7  c3                    ret

LAB_004812c8:
  004812c8  a900000001            test      eax,1000000h
  004812cd  0f8434020000          je        near 00481507h
  004812d3  a801                  test      al,1
  004812d5  7410                  je        short 004812E7h
  004812d7  0fbf7e32              movsx     edi,word [esi+32h]
  004812db  0fbf4634              movsx     eax,word [esi+34h]
  004812df  81ef00040000          sub       edi,400h
  004812e5  eb6f                  jmp       short 00481356h

LAB_004812e7:
  004812e7  8b5e78                mov       ebx,[esi+78h]
  004812ea  8b4e1c                mov       ecx,[esi+1Ch]
  004812ed  53                    push      ebx
  004812ee  8b7b1c                mov       edi,[ebx+1Ch]
  004812f1  2bf9                  sub       edi,ecx
  004812f3  c1ff0c                sar       edi,0Ch
  004812f6  e8c5780b00            call      00538BC0h
  004812fb  8b5620                mov       edx,[esi+20h]
  004812fe  8be8                  mov       ebp,eax
  00481300  8b4320                mov       eax,[ebx+20h]
  00481303  8b5b24                mov       ebx,[ebx+24h]
  00481306  2bc2                  sub       eax,edx
  00481308  8b5624                mov       edx,[esi+24h]
  0048130b  2bda                  sub       ebx,edx
  0048130d  8bcf                  mov       ecx,edi
  0048130f  c1fb0c                sar       ebx,0Ch
  00481312  0fafcf                imul      ecx,edi
  00481315  8bd3                  mov       edx,ebx
  00481317  83e5fe                and       ebp,0FFFFFFFEh
  0048131a  0fafd3                imul      edx,ebx
  0048131d  83c404                add       esp,4
  00481320  03ca                  add       ecx,edx
  00481322  c1e50b                shl       ebp,0Bh
  00481325  03e8                  add       ebp,eax
  00481327  51                    push      ecx
  00481328  c1fd0c                sar       ebp,0Ch
  0048132b  e8e0460c00            call      00545A10h
  00481330  53                    push      ebx
  00481331  57                    push      edi
  00481332  89442444              mov       [esp+44h],eax
  00481336  e8e7460c00            call      00545A22h
  0048133b  8bf8                  mov       edi,eax
  0048133d  8b44243c              mov       eax,[esp+3Ch]
  00481341  50                    push      eax
  00481342  55                    push      ebp
  00481343  e8da460c00            call      00545A22h
  00481348  8d8f00040000          lea       ecx,[edi+400h]
  0048134e  66894634              mov       [esi+34h],ax
  00481352  66894e32              mov       [esi+32h],cx

LAB_00481356:
  00481356  6880000000            push      80h
  0048135b  8d6e50                lea       ebp,[esi+50h]
  0048135e  50                    push      eax
  0048135f  57                    push      edi
  00481360  55                    push      ebp
  00481361  e86a170b00            call      00532AD0h
  00481366  8b5500                mov       edx,[ebp]
  00481369  8b461c                mov       eax,[esi+1Ch]
  0048136c  8b4e58                mov       ecx,[esi+58h]
  0048136f  8d5e1c                lea       ebx,[esi+1Ch]
  00481372  8b7e20                mov       edi,[esi+20h]
  00481375  03c2                  add       eax,edx
  00481377  8b5624                mov       edx,[esi+24h]
  0048137a  83c410                add       esp,10h
  0048137d  03d1                  add       edx,ecx
  0048137f  8903                  mov       [ebx],eax
  00481381  8b4654                mov       eax,[esi+54h]
  00481384  895624                mov       [esi+24h],edx
  00481387  8d542420              lea       edx,[esp+20h]
  0048138b  03f8                  add       edi,eax
  0048138d  52                    push      edx
  0048138e  56                    push      esi
  0048138f  897e20                mov       [esi+20h],edi
  00481392  e80939ffff            call      00474CA0h
  00481397  83c408                add       esp,8
  0048139a  8944241c              mov       [esp+1Ch],eax
  0048139e  85c0                  test      eax,eax
  004813a0  0f843e010000          je        near 004814E4h
  004813a6  3b4678                cmp       eax,[esi+78h]
  004813a9  0f8535010000          jne       near 004814E4h
  004813af  8b3e                  mov       edi,[esi]
  004813b1  8b4e74                mov       ecx,[esi+74h]
  004813b4  668b5606              mov       dx,[esi+6]
  004813b8  83cf01                or        edi,1
  004813bb  893e                  mov       [esi],edi
  004813bd  6a00                  push      0
  004813bf  66895106              mov       [ecx+6],dx
  004813c3  50                    push      eax
  004813c4  8b4674                mov       eax,[esi+74h]
  004813c7  50                    push      eax
  004813c8  e823ba0b00            call      0053CDF0h
  004813cd  83c40c                add       esp,0Ch
  004813d0  85c0                  test      eax,eax
  004813d2  0f85ef000000          jne       near 004814C7h
  004813d8  8b4d00                mov       ecx,[ebp]
  004813db  8b5654                mov       edx,[esi+54h]
  004813de  8b4658                mov       eax,[esi+58h]
  004813e1  894c242c              mov       [esp+2Ch],ecx
  004813e5  8b4e28                mov       ecx,[esi+28h]
  004813e8  89542430              mov       [esp+30h],edx
  004813ec  8b562c                mov       edx,[esi+2Ch]
  004813ef  89442434              mov       [esp+34h],eax
  004813f3  8b4638                mov       eax,[esi+38h]
  004813f6  894c2410              mov       [esp+10h],ecx
  004813fa  89542414              mov       [esp+14h],edx
  004813fe  89442418              mov       [esp+18h],eax
  00481402  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0048140b  c744243c06000000      mov       dword [esp+3Ch],6

LAB_00481413:
  00481413  e8285d0c00            call      00547140h
  00481418  8bf8                  mov       edi,eax
  0048141a  57                    push      edi
  0048141b  e8fc450c00            call      00545A1Ch
  00481420  c1e005                shl       eax,5
  00481423  894500                mov       [ebp],eax
  00481426  e8155d0c00            call      00547140h
  0048142b  83e01f                and       eax,1Fh
  0048142e  57                    push      edi
  0048142f  83c01e                add       eax,1Eh
  00481432  c1e00c                shl       eax,0Ch
  00481435  894654                mov       [esi+54h],eax
  00481438  e8d9450c00            call      00545A16h
  0048143d  6a0c                  push      0Ch
  0048143f  6a00                  push      0
  00481441  c1e005                shl       eax,5
  00481444  68bf000000            push      0BFh
  00481449  894658                mov       [esi+58h],eax
  0048144c  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  00481453  c7462800200002        mov       dword [esi+28h],2002000h
  0048145a  66c7462c1f00          mov       word [esi+2Ch],1Fh
  00481460  66c7461a0100          mov       word [esi+1Ah],1
  00481466  e8d55c0c00            call      00547140h
  0048146b  99                    cdq
  0048146c  b903000000            mov       ecx,3
  00481471  f7f9                  idiv      ecx
  00481473  8d14d5881b5500        lea       edx,[edx*8+551B88h]
  0048147a  52                    push      edx
  0048147b  53                    push      ebx
  0048147c  56                    push      esi
  0048147d  e8be97ffff            call      0047AC40h
  00481482  8b442454              mov       eax,[esp+54h]
  00481486  83c418                add       esp,18h
  00481489  48                    dec       eax
  0048148a  8944243c              mov       [esp+3Ch],eax
  0048148e  7583                  jne       short 00481413h
  00481490  8b44242c              mov       eax,[esp+2Ch]
  00481494  8b4c2430              mov       ecx,[esp+30h]
  00481498  8b542434              mov       edx,[esp+34h]
  0048149c  894650                mov       [esi+50h],eax
  0048149f  8b442410              mov       eax,[esp+10h]
  004814a3  894e54                mov       [esi+54h],ecx
  004814a6  8b4c2414              mov       ecx,[esp+14h]
  004814aa  895658                mov       [esi+58h],edx
  004814ad  8b542418              mov       edx,[esp+18h]
  004814b1  894628                mov       [esi+28h],eax
  004814b4  8b44241c              mov       eax,[esp+1Ch]
  004814b8  894e2c                mov       [esi+2Ch],ecx
  004814bb  895638                mov       [esi+38h],edx
  004814be  66c780960000000100    mov       word [eax+96h],1

LAB_004814c7:
  004814c7  8b466c                mov       eax,[esi+6Ch]
  004814ca  8b08                  mov       ecx,[eax]
  004814cc  83c904                or        ecx,4
  004814cf  8908                  mov       [eax],ecx
  004814d1  c705a0935e0080000000  mov       dword [5E93A0h],80h
  004814db  66c705a4935e000000    mov       word [5E93A4h],0

LAB_004814e4:
  004814e4  66837e100c            cmp       word [esi+10h],0Ch
  004814e9  7c10                  jl        short 004814FBh
  004814eb  8b466c                mov       eax,[esi+6Ch]
  004814ee  8b08                  mov       ecx,[eax]
  004814f0  83c904                or        ecx,4
  004814f3  8908                  mov       [eax],ecx
  004814f5  c70600000000          mov       dword [esi],0

LAB_004814fb:
  004814fb  66ff4610              inc       word [esi+10h]
  004814ff  5f                    pop       edi
  00481500  5e                    pop       esi
  00481501  5d                    pop       ebp
  00481502  5b                    pop       ebx
  00481503  83c428                add       esp,28h
  00481506  c3                    ret

LAB_00481507:
  00481507  8b5e78                mov       ebx,[esi+78h]
  0048150a  8b561c                mov       edx,[esi+1Ch]
  0048150d  0c80                  or        al,80h
  0048150f  53                    push      ebx
  00481510  8906                  mov       [esi],eax
  00481512  8b7b1c                mov       edi,[ebx+1Ch]
  00481515  2bfa                  sub       edi,edx
  00481517  c1ff0c                sar       edi,0Ch
  0048151a  e8a1760b00            call      00538BC0h
  0048151f  8b4b20                mov       ecx,[ebx+20h]
  00481522  8b5b24                mov       ebx,[ebx+24h]
  00481525  8be8                  mov       ebp,eax
  00481527  8b4620                mov       eax,[esi+20h]
  0048152a  2bc8                  sub       ecx,eax
  0048152c  8b4624                mov       eax,[esi+24h]
  0048152f  2bd8                  sub       ebx,eax
  00481531  8bd7                  mov       edx,edi
  00481533  c1fb0c                sar       ebx,0Ch
  00481536  0fafd7                imul      edx,edi
  00481539  8bc3                  mov       eax,ebx
  0048153b  83e5fe                and       ebp,0FFFFFFFEh
  0048153e  0fafc3                imul      eax,ebx
  00481541  83c404                add       esp,4
  00481544  03d0                  add       edx,eax
  00481546  c1e50b                shl       ebp,0Bh
  00481549  03e9                  add       ebp,ecx
  0048154b  52                    push      edx
  0048154c  c1fd0c                sar       ebp,0Ch
  0048154f  e8bc440c00            call      00545A10h
  00481554  53                    push      ebx
  00481555  57                    push      edi
  00481556  89442444              mov       [esp+44h],eax
  0048155a  e8c3440c00            call      00545A22h
  0048155f  668b4e32              mov       cx,[esi+32h]
  00481563  668b5e34              mov       bx,[esi+34h]
  00481567  ba00040000            mov       edx,400h
  0048156c  8bf8                  mov       edi,eax
  0048156e  8b44243c              mov       eax,[esp+3Ch]
  00481572  2bd1                  sub       edx,ecx
  00481574  03fa                  add       edi,edx
  00481576  50                    push      eax
  00481577  55                    push      ebp
  00481578  81e7ff0f0000          and       edi,0FFFh
  0048157e  e89f440c00            call      00545A22h
  00481583  2bc3                  sub       eax,ebx
  00481585  25ff0f0000            and       eax,0FFFh
  0048158a  81ff00080000          cmp       edi,800h
  00481590  7d07                  jge       short 00481599h
  00481592  c1ff03                sar       edi,3
  00481595  8bcf                  mov       ecx,edi
  00481597  eb0c                  jmp       short 004815A5h

LAB_00481599:
  00481599  b900100000            mov       ecx,1000h
  0048159e  2bcf                  sub       ecx,edi
  004815a0  c1f903                sar       ecx,3
  004815a3  f7d9                  neg       ecx

LAB_004815a5:
  004815a5  66014e32              add       [esi+32h],cx
  004815a9  3d00080000            cmp       eax,800h
  004815ae  7d07                  jge       short 004815B7h
  004815b0  c1f803                sar       eax,3
  004815b3  8bc8                  mov       ecx,eax
  004815b5  eb0c                  jmp       short 004815C3h

LAB_004815b7:
  004815b7  b900100000            mov       ecx,1000h
  004815bc  2bc8                  sub       ecx,eax
  004815be  c1f903                sar       ecx,3
  004815c1  f7d9                  neg       ecx

LAB_004815c3:
  004815c3  668b4628              mov       ax,[esi+28h]
  004815c7  03d9                  add       ebx,ecx
  004815c9  0fbfc8                movsx     ecx,ax
  004815cc  ba00200000            mov       edx,2000h
  004815d1  66895e34              mov       [esi+34h],bx
  004815d5  2bd1                  sub       edx,ecx
  004815d7  c1fa02                sar       edx,2
  004815da  03d0                  add       edx,eax
  004815dc  668b462a              mov       ax,[esi+2Ah]
  004815e0  0fbfc8                movsx     ecx,ax
  004815e3  66895628              mov       [esi+28h],dx
  004815e7  ba00100000            mov       edx,1000h
  004815ec  2bd1                  sub       edx,ecx
  004815ee  c1fa02                sar       edx,2
  004815f1  03d0                  add       edx,eax
  004815f3  668b462c              mov       ax,[esi+2Ch]
  004815f7  0fbfc8                movsx     ecx,ax
  004815fa  6689562a              mov       [esi+2Ah],dx
  004815fe  ba00100000            mov       edx,1000h
  00481603  2bd1                  sub       edx,ecx
  00481605  c1fa02                sar       edx,2
  00481608  03d0                  add       edx,eax
  0048160a  8b4638                mov       eax,[esi+38h]
  0048160d  3df0000000            cmp       eax,0F0h
  00481612  6689562c              mov       [esi+2Ch],dx
  00481616  7306                  jae       short 0048161Eh
  00481618  83c010                add       eax,10h
  0048161b  894638                mov       [esi+38h],eax

LAB_0048161e:
  0048161e  66ff4610              inc       word [esi+10h]
  00481622  66837e103c            cmp       word [esi+10h],3Ch
  00481627  7d2a                  jge       short 00481653h
  00481629  8b466c                mov       eax,[esi+6Ch]
  0048162c  0fbf4812              movsx     ecx,word [eax+12h]
  00481630  b867666666            mov       eax,66666667h
  00481635  f7e9                  imul      ecx
  00481637  d1fa                  sar       edx,1
  00481639  8bca                  mov       ecx,edx
  0048163b  c1e91f                shr       ecx,1Fh
  0048163e  03d1                  add       edx,ecx
  00481640  83fa0c                cmp       edx,0Ch
  00481643  7c1d                  jl        short 00481662h
  00481645  810e00000002          or        dword [esi],2000000h
  0048164b  5f                    pop       edi
  0048164c  5e                    pop       esi
  0048164d  5d                    pop       ebp
  0048164e  5b                    pop       ebx
  0048164f  83c428                add       esp,28h
  00481652  c3                    ret

LAB_00481653:
  00481653  8b06                  mov       eax,[esi]
  00481655  66c746100000          mov       word [esi+10h],0
  0048165b  0d00000001            or        eax,1000000h
  00481660  8906                  mov       [esi],eax

LAB_00481662:
  00481662  5f                    pop       edi
  00481663  5e                    pop       esi
  00481664  5d                    pop       ebp
  00481665  5b                    pop       ebx
  00481666  83c428                add       esp,28h
  00481669  c3                    ret

15 cible(s) de saut interne : 0x004812c8, 0x004812e7, 0x00481356, 0x00481413, 0x004814c7, 0x004814e4, 0x004814fb, 0x00481507, 0x00481599, 0x004815a5, 0x004815b7, 0x004815c3, 0x0048161e, 0x00481653, 0x00481662
