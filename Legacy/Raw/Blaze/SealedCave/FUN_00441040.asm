Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00441040  section .text  offset fichier 0x40440  320 octets

  00441040  56                    push      esi
  00441041  8b742408              mov       esi,[esp+8]
  00441045  66837e1000            cmp       word [esi+10h],0
  0044104a  0f8597000000          jne       near 004410E7h
  00441050  c7463cf0145500        mov       dword [esi+3Ch],5514F0h
  00441057  c6464000              mov       byte [esi+40h],0
  0044105b  e8e0601000            call      00547140h
  00441060  99                    cdq
  00441061  33c2                  xor       eax,edx
  00441063  2bc2                  sub       eax,edx
  00441065  25ff0f0000            and       eax,0FFFh
  0044106a  33c2                  xor       eax,edx
  0044106c  2bc2                  sub       eax,edx
  0044106e  66894632              mov       [esi+32h],ax
  00441072  e8c9601000            call      00547140h
  00441077  99                    cdq
  00441078  33c2                  xor       eax,edx
  0044107a  2bc2                  sub       eax,edx
  0044107c  25ff0f0000            and       eax,0FFFh
  00441081  33c2                  xor       eax,edx
  00441083  2bc2                  sub       eax,edx
  00441085  66894614              mov       [esi+14h],ax
  00441089  e8b2601000            call      00547140h
  0044108e  99                    cdq
  0044108f  b990010000            mov       ecx,190h
  00441094  f7f9                  idiv      ecx
  00441096  8b4e20                mov       ecx,[esi+20h]
  00441099  83c264                add       edx,64h
  0044109c  c1e20c                shl       edx,0Ch
  0044109f  03ca                  add       ecx,edx
  004410a1  894e20                mov       [esi+20h],ecx
  004410a4  e897601000            call      00547140h
  004410a9  99                    cdq
  004410aa  b92c010000            mov       ecx,12Ch
  004410af  c6460a02              mov       byte [esi+0Ah],2
  004410b3  f7f9                  idiv      ecx
  004410b5  8b4620                mov       eax,[esi+20h]
  004410b8  8b4e24                mov       ecx,[esi+24h]
  004410bb  894654                mov       [esi+54h],eax
  004410be  894e58                mov       [esi+58h],ecx
  004410c1  c6460b01              mov       byte [esi+0Bh],1
  004410c5  66c746100100          mov       word [esi+10h],1
  004410cb  c6463828              mov       byte [esi+38h],28h
  004410cf  c6463900              mov       byte [esi+39h],0
  004410d3  c6463a80              mov       byte [esi+3Ah],80h
  004410d7  81c2f4010000          add       edx,1F4h
  004410dd  66895612              mov       [esi+12h],dx
  004410e1  8b561c                mov       edx,[esi+1Ch]
  004410e4  895650                mov       [esi+50h],edx

LAB_004410e7:
  004410e7  66837e1001            cmp       word [esi+10h],1
  004410ec  0f8587000000          jne       near 00441179h
  004410f2  0fbf5632              movsx     edx,word [esi+32h]
  004410f6  52                    push      edx
  004410f7  e820491000            call      00545A1Ch
  004410fc  0fbf4e12              movsx     ecx,word [esi+12h]
  00441100  0fafc1                imul      eax,ecx
  00441103  0fbf5632              movsx     edx,word [esi+32h]
  00441107  8b4e50                mov       ecx,[esi+50h]
  0044110a  52                    push      edx
  0044110b  03c1                  add       eax,ecx
  0044110d  89461c                mov       [esi+1Ch],eax
  00441110  e801491000            call      00545A16h
  00441115  0fbf4e12              movsx     ecx,word [esi+12h]
  00441119  0fafc1                imul      eax,ecx
  0044111c  034658                add       eax,[esi+58h]
  0044111f  0fbf5632              movsx     edx,word [esi+32h]
  00441123  52                    push      edx
  00441124  894624                mov       [esi+24h],eax
  00441127  e8f0481000            call      00545A1Ch
  0044112c  8b4e54                mov       ecx,[esi+54h]
  0044112f  8d0480                lea       eax,[eax+eax*4]
  00441132  6683463264            add       word [esi+32h],64h
  00441137  66834612f6            add       word [esi+12h],0FFF6h
  0044113c  6683461464            add       word [esi+14h],64h
  00441141  8d0480                lea       eax,[eax+eax*4]
  00441144  6afe                  push      0FFFFFFFEh
  00441146  6a00                  push      0
  00441148  8d14c1                lea       edx,[ecx+eax*8]
  0044114b  6a02                  push      2
  0044114d  56                    push      esi
  0044114e  895620                mov       [esi+20h],edx
  00441151  e88a3cfcff            call      00404DE0h
  00441156  668b4614              mov       ax,[esi+14h]
  0044115a  83c410                add       esp,10h
  0044115d  663d0010              cmp       ax,1000h
  00441161  7c09                  jl        short 0044116Ch
  00441163  0500f0ffff            add       eax,0FFFFF000h
  00441168  66894614              mov       [esi+14h],ax

LAB_0044116c:
  0044116c  66837e1200            cmp       word [esi+12h],0
  00441171  7d06                  jge       short 00441179h
  00441173  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00441179:
  00441179  5e                    pop       esi
  0044117a  c3                    ret
  0044117b  90                    nop
  0044117c  90                    nop
  0044117d  90                    nop
  0044117e  90                    nop
  0044117f  90                    nop

3 cible(s) de saut interne : 0x004410e7, 0x0044116c, 0x00441179
