C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00417420  section .text  offset fichier 0x16820  560 octets

  00417420  53                    push      ebx
  00417421  56                    push      esi
  00417422  8b74240c              mov       esi,[esp+0Ch]
  00417426  33db                  xor       ebx,ebx
  00417428  66395e10              cmp       [esi+10h],bx
  0041742c  7576                  jne       short 004174A4h
  0041742e  0fbf4632              movsx     eax,word [esi+32h]
  00417432  50                    push      eax
  00417433  66c746100100          mov       word [esi+10h],1
  00417439  c7463ca0fa5400        mov       dword [esi+3Ch],54FAA0h
  00417440  885e40                mov       [esi+40h],bl
  00417443  e8d4e51200            call      00545A1Ch
  00417448  8bc8                  mov       ecx,eax
  0041744a  8b561c                mov       edx,[esi+1Ch]
  0041744d  f7d9                  neg       ecx
  0041744f  c1e102                shl       ecx,2
  00417452  2bc8                  sub       ecx,eax
  00417454  c1e104                shl       ecx,4
  00417457  03d1                  add       edx,ecx
  00417459  89561c                mov       [esi+1Ch],edx
  0041745c  0fbf5632              movsx     edx,word [esi+32h]
  00417460  52                    push      edx
  00417461  e8b0e51200            call      00545A16h
  00417466  8bc8                  mov       ecx,eax
  00417468  8b5624                mov       edx,[esi+24h]
  0041746b  f7d9                  neg       ecx
  0041746d  c1e102                shl       ecx,2
  00417470  2bc8                  sub       ecx,eax
  00417472  8b4620                mov       eax,[esi+20h]
  00417475  c1e104                shl       ecx,4
  00417478  03d1                  add       edx,ecx
  0041747a  0500000500            add       eax,50000h
  0041747f  894620                mov       [esi+20h],eax
  00417482  b800080000            mov       eax,800h
  00417487  895624                mov       [esi+24h],edx
  0041748a  66894628              mov       [esi+28h],ax
  0041748e  6689462a              mov       [esi+2Ah],ax
  00417492  66895e12              mov       [esi+12h],bx
  00417496  c6460a05              mov       byte [esi+0Ah],5
  0041749a  885e0b                mov       [esi+0Bh],bl
  0041749d  66899e9a000000        mov       [esi+9Ah],bx

LAB_004174a4:
  004174a4  66837e1001            cmp       word [esi+10h],1
  004174a9  0f8587000000          jne       near 00417536h
  004174af  0fbf5632              movsx     edx,word [esi+32h]
  004174b3  52                    push      edx
  004174b4  e863e51200            call      00545A1Ch
  004174b9  8b561c                mov       edx,[esi+1Ch]
  004174bc  8bc8                  mov       ecx,eax
  004174be  c1e104                shl       ecx,4
  004174c1  2bc8                  sub       ecx,eax
  004174c3  f7d9                  neg       ecx
  004174c5  03d1                  add       edx,ecx
  004174c7  89561c                mov       [esi+1Ch],edx
  004174ca  0fbf5632              movsx     edx,word [esi+32h]
  004174ce  52                    push      edx
  004174cf  e842e51200            call      00545A16h
  004174d4  8b5624                mov       edx,[esi+24h]
  004174d7  8bc8                  mov       ecx,eax
  004174d9  c1e104                shl       ecx,4
  004174dc  2bc8                  sub       ecx,eax
  004174de  f7d9                  neg       ecx
  004174e0  03d1                  add       edx,ecx
  004174e2  66ff4612              inc       word [esi+12h]
  004174e6  66837e120a            cmp       word [esi+12h],0Ah
  004174eb  895624                mov       [esi+24h],edx
  004174ee  7c46                  jl        short 00417536h
  004174f0  66c746100200          mov       word [esi+10h],2
  004174f6  66895e12              mov       [esi+12h],bx
  004174fa  c7465000808900        mov       dword [esi+50h],898000h
  00417501  c7465400401a00        mov       dword [esi+54h],1A4000h
  00417508  895e58                mov       [esi+58h],ebx
  0041750b  e830fc1200            call      00547140h
  00417510  99                    cdq
  00417511  33c2                  xor       eax,edx
  00417513  2bc2                  sub       eax,edx
  00417515  25ff0f0000            and       eax,0FFFh
  0041751a  33c2                  xor       eax,edx
  0041751c  2bc2                  sub       eax,edx
  0041751e  66894614              mov       [esi+14h],ax
  00417522  e819fc1200            call      00547140h
  00417527  99                    cdq
  00417528  b964000000            mov       ecx,64h
  0041752d  f7f9                  idiv      ecx
  0041752f  83c232                add       edx,32h
  00417532  66895616              mov       [esi+16h],dx

LAB_00417536:
  00417536  66837e1002            cmp       word [esi+10h],2
  0041753b  0f8585000000          jne       near 004175C6h
  00417541  0fbf5614              movsx     edx,word [esi+14h]
  00417545  52                    push      edx
  00417546  e8d1e41200            call      00545A1Ch
  0041754b  8b4e50                mov       ecx,[esi+50h]
  0041754e  8d0440                lea       eax,[eax+eax*2]
  00417551  8d0480                lea       eax,[eax+eax*4]
  00417554  8d0480                lea       eax,[eax+eax*4]
  00417557  8d1441                lea       edx,[ecx+eax*2]
  0041755a  0fbf4614              movsx     eax,word [esi+14h]
  0041755e  50                    push      eax
  0041755f  89561c                mov       [esi+1Ch],edx
  00417562  e8afe41200            call      00545A16h
  00417567  8b5658                mov       edx,[esi+58h]
  0041756a  8d0440                lea       eax,[eax+eax*2]
  0041756d  8d0480                lea       eax,[eax+eax*4]
  00417570  8d0c80                lea       ecx,[eax+eax*4]
  00417573  8d044a                lea       eax,[edx+ecx*2]
  00417576  668b4e16              mov       cx,[esi+16h]
  0041757a  66014e14              add       [esi+14h],cx
  0041757e  894624                mov       [esi+24h],eax
  00417581  8b4620                mov       eax,[esi+20h]
  00417584  0500800000            add       eax,8000h
  00417589  894620                mov       [esi+20h],eax
  0041758c  668b4614              mov       ax,[esi+14h]
  00417590  663d0010              cmp       ax,1000h
  00417594  7c09                  jl        short 0041759Fh
  00417596  0500f0ffff            add       eax,0FFFFF000h
  0041759b  66894614              mov       [esi+14h],ax

LAB_0041759f:
  0041759f  66ff4612              inc       word [esi+12h]
  004175a3  66837e121c            cmp       word [esi+12h],1Ch
  004175a8  7c1c                  jl        short 004175C6h
  004175aa  b960ffffff            mov       ecx,0FFFFFF60h
  004175af  66014e28              add       [esi+28h],cx
  004175b3  668b4628              mov       ax,[esi+28h]
  004175b7  66014e2a              add       [esi+2Ah],cx
  004175bb  663bc3                cmp       ax,bx
  004175be  7f06                  jg        short 004175C6h
  004175c0  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004175c6:
  004175c6  66ff869a000000        inc       word [esi+9Ah]
  004175cd  6683be9a00000003      cmp       word [esi+9Ah],3
  004175d5  7c18                  jl        short 004175EFh
  004175d7  8a4e40                mov       cl,[esi+40h]
  004175da  66899e9a000000        mov       [esi+9Ah],bx
  004175e1  fec1                  inc       cl
  004175e3  8ac1                  mov       al,cl
  004175e5  884e40                mov       [esi+40h],cl
  004175e8  3c04                  cmp       al,4
  004175ea  7503                  jne       short 004175EFh
  004175ec  885e40                mov       [esi+40h],bl

LAB_004175ef:
  004175ef  e84cfb1200            call      00547140h
  004175f4  99                    cdq
  004175f5  b914000000            mov       ecx,14h
  004175fa  f7f9                  idiv      ecx
  004175fc  8b461c                mov       eax,[esi+1Ch]
  004175ff  83ea0a                sub       edx,0Ah
  00417602  c1e20c                shl       edx,0Ch
  00417605  03c2                  add       eax,edx
  00417607  89461c                mov       [esi+1Ch],eax
  0041760a  e831fb1200            call      00547140h
  0041760f  99                    cdq
  00417610  b914000000            mov       ecx,14h
  00417615  f7f9                  idiv      ecx
  00417617  8b4620                mov       eax,[esi+20h]
  0041761a  83ea0a                sub       edx,0Ah
  0041761d  c1e20c                shl       edx,0Ch
  00417620  03c2                  add       eax,edx
  00417622  894620                mov       [esi+20h],eax
  00417625  e816fb1200            call      00547140h
  0041762a  99                    cdq
  0041762b  b914000000            mov       ecx,14h
  00417630  f7f9                  idiv      ecx
  00417632  8b4624                mov       eax,[esi+24h]
  00417635  83ea0a                sub       edx,0Ah
  00417638  c1e20c                shl       edx,0Ch
  0041763b  03c2                  add       eax,edx
  0041763d  894624                mov       [esi+24h],eax
  00417640  5e                    pop       esi
  00417641  5b                    pop       ebx
  00417642  c3                    ret
  00417643  90                    nop
  00417644  90                    nop
  00417645  90                    nop
  00417646  90                    nop
  00417647  90                    nop
  00417648  90                    nop
  00417649  90                    nop
  0041764a  90                    nop
  0041764b  90                    nop
  0041764c  90                    nop
  0041764d  90                    nop
  0041764e  90                    nop
  0041764f  90                    nop

5 cible(s) de saut interne : 0x004174a4, 0x00417536, 0x0041759f, 0x004175c6, 0x004175ef
