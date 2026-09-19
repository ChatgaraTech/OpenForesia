Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004421b0  section .text  offset fichier 0x415b0  224 octets

  004421b0  56                    push      esi
  004421b1  8b742408              mov       esi,[esp+8]
  004421b5  57                    push      edi
  004421b6  66837e1000            cmp       word [esi+10h],0
  004421bb  7569                  jne       short 00442226h
  004421bd  66c746100100          mov       word [esi+10h],1
  004421c3  c7463cb8135500        mov       dword [esi+3Ch],5513B8h
  004421ca  c6464000              mov       byte [esi+40h],0
  004421ce  c6460a02              mov       byte [esi+0Ah],2
  004421d2  c6460b01              mov       byte [esi+0Bh],1
  004421d6  e8654f1000            call      00547140h
  004421db  99                    cdq
  004421dc  33c2                  xor       eax,edx
  004421de  66c74614d007          mov       word [esi+14h],7D0h
  004421e4  2bc2                  sub       eax,edx
  004421e6  25ff0f0000            and       eax,0FFFh
  004421eb  33c2                  xor       eax,edx
  004421ed  2bc2                  sub       eax,edx
  004421ef  66894612              mov       [esi+12h],ax
  004421f3  e8484f1000            call      00547140h
  004421f8  99                    cdq
  004421f9  b9e8030000            mov       ecx,3E8h
  004421fe  f7f9                  idiv      ecx
  00442200  8b4620                mov       eax,[esi+20h]
  00442203  8b4e24                mov       ecx,[esi+24h]
  00442206  894654                mov       [esi+54h],eax
  00442209  b800100000            mov       eax,1000h
  0044220e  894e58                mov       [esi+58h],ecx
  00442211  66894628              mov       [esi+28h],ax
  00442215  6689462a              mov       [esi+2Ah],ax
  00442219  6689969a000000        mov       [esi+9Ah],dx
  00442220  8b561c                mov       edx,[esi+1Ch]
  00442223  895650                mov       [esi+50h],edx

LAB_00442226:
  00442226  66837e1001            cmp       word [esi+10h],1
  0044222b  755e                  jne       short 0044228Bh
  0044222d  0fbf5612              movsx     edx,word [esi+12h]
  00442231  52                    push      edx
  00442232  e8e5371000            call      00545A1Ch
  00442237  0fbf4e14              movsx     ecx,word [esi+14h]
  0044223b  0fafc1                imul      eax,ecx
  0044223e  0fbf5612              movsx     edx,word [esi+12h]
  00442242  8b4e50                mov       ecx,[esi+50h]
  00442245  668b7e14              mov       di,[esi+14h]
  00442249  03c1                  add       eax,ecx
  0044224b  52                    push      edx
  0044224c  89461c                mov       [esi+1Ch],eax
  0044224f  e8c2371000            call      00545A16h
  00442254  0fbfcf                movsx     ecx,di
  00442257  0fafc1                imul      eax,ecx
  0044225a  8b5658                mov       edx,[esi+58h]
  0044225d  8b4e54                mov       ecx,[esi+54h]
  00442260  03c2                  add       eax,edx
  00442262  6683461264            add       word [esi+12h],64h
  00442267  894624                mov       [esi+24h],eax
  0044226a  8b4620                mov       eax,[esi+20h]
  0044226d  8bd0                  mov       edx,eax
  0044226f  2bd1                  sub       edx,ecx
  00442271  c1fa04                sar       edx,4
  00442274  2bc2                  sub       eax,edx
  00442276  894620                mov       [esi+20h],eax
  00442279  8d47e2                lea       eax,[edi-1Eh]
  0044227c  6685c0                test      ax,ax
  0044227f  66894614              mov       [esi+14h],ax
  00442283  7d06                  jge       short 0044228Bh
  00442285  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044228b:
  0044228b  5f                    pop       edi
  0044228c  5e                    pop       esi
  0044228d  c3                    ret
  0044228e  90                    nop
  0044228f  90                    nop

2 cible(s) de saut interne : 0x00442226, 0x0044228b
