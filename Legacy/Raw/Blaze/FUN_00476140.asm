Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00476140  section .text  offset fichier 0x75540  144 octets

  00476140  53                    push      ebx
  00476141  55                    push      ebp
  00476142  56                    push      esi
  00476143  8b742410              mov       esi,[esp+10h]
  00476147  57                    push      edi
  00476148  0fbf7e12              movsx     edi,word [esi+12h]
  0047614c  0fbf6e32              movsx     ebp,word [esi+32h]
  00476150  8b5e74                mov       ebx,[esi+74h]
  00476153  8bc7                  mov       eax,edi
  00476155  c1f802                sar       eax,2
  00476158  55                    push      ebp
  00476159  89442418              mov       [esp+18h],eax
  0047615d  e8baf80c00            call      00545A1Ch
  00476162  0faf442414            imul      eax,[esp+14h]
  00476167  8b4b1c                mov       ecx,[ebx+1Ch]
  0047616a  55                    push      ebp
  0047616b  03c1                  add       eax,ecx
  0047616d  89461c                mov       [esi+1Ch],eax
  00476170  8b4b20                mov       ecx,[ebx+20h]
  00476173  c1e70c                shl       edi,0Ch
  00476176  03f9                  add       edi,ecx
  00476178  897e20                mov       [esi+20h],edi
  0047617b  e896f80c00            call      00545A16h
  00476180  0faf442414            imul      eax,[esp+14h]
  00476185  8b7b24                mov       edi,[ebx+24h]
  00476188  8a5640                mov       dl,[esi+40h]
  0047618b  03c7                  add       eax,edi
  0047618d  81c580000000          add       ebp,80h
  00476193  fec2                  inc       dl
  00476195  894624                mov       [esi+24h],eax
  00476198  660fb6c2              movzx     ax,dl
  0047619c  663b461a              cmp       ax,[esi+1Ah]
  004761a0  66896e32              mov       [esi+32h],bp
  004761a4  885640                mov       [esi+40h],dl
  004761a7  7c04                  jl        short 004761ADh
  004761a9  c6464000              mov       byte [esi+40h],0

LAB_004761ad:
  004761ad  53                    push      ebx
  004761ae  e8cd250c00            call      00538780h
  004761b3  8b4040                mov       eax,[eax+40h]
  004761b6  83c404                add       esp,4
  004761b9  a880                  test      al,80h
  004761bb  7407                  je        short 004761C4h
  004761bd  a900000040            test      eax,40000000h
  004761c2  7406                  je        short 004761CAh

LAB_004761c4:
  004761c4  c70600000000          mov       dword [esi],0

LAB_004761ca:
  004761ca  5f                    pop       edi
  004761cb  5e                    pop       esi
  004761cc  5d                    pop       ebp
  004761cd  5b                    pop       ebx
  004761ce  c3                    ret
  004761cf  90                    nop

3 cible(s) de saut interne : 0x004761ad, 0x004761c4, 0x004761ca
