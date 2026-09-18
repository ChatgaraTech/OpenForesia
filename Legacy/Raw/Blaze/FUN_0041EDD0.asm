
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041edd0  section .text  offset fichier 0x1e1d0  1296 octets

  0041edd0  83ec20                sub       esp,20h
  0041edd3  53                    push      ebx
  0041edd4  55                    push      ebp
  0041edd5  56                    push      esi
  0041edd6  8b742430              mov       esi,[esp+30h]
  0041edda  33d2                  xor       edx,edx
  0041eddc  57                    push      edi
  0041eddd  8b5e74                mov       ebx,[esi+74h]
  0041ede0  8b6e6c                mov       ebp,[esi+6Ch]
  0041ede3  66395610              cmp       [esi+10h],dx
  0041ede7  895c2410              mov       [esp+10h],ebx
  0041edeb  896c241c              mov       [esp+1Ch],ebp
  0041edef  756c                  jne       short 0041EE5Dh
  0041edf1  b8fa000000            mov       eax,0FAh
  0041edf6  885604                mov       [esi+4],dl
  0041edf9  66894628              mov       [esi+28h],ax
  0041edfd  6689462c              mov       [esi+2Ch],ax
  0041ee01  668b869a000000        mov       ax,[esi+9Ah]
  0041ee08  c6460a03              mov       byte [esi+0Ah],3
  0041ee0c  663bc2                cmp       ax,dx
  0041ee0f  c6460b01              mov       byte [esi+0Bh],1
  0041ee13  66c746100100          mov       word [esi+10h],1
  0041ee19  6689562a              mov       [esi+2Ah],dx
  0041ee1d  66c746120a00          mov       word [esi+12h],0Ah
  0041ee23  b196                  mov       cl,96h
  0041ee25  750a                  jne       short 0041EE31h
  0041ee27  885638                mov       [esi+38h],dl
  0041ee2a  c6463982              mov       byte [esi+39h],82h
  0041ee2e  884e3a                mov       [esi+3Ah],cl

LAB_0041ee31:
  0041ee31  663d0100              cmp       ax,1
  0041ee35  750b                  jne       short 0041EE42h
  0041ee37  884e38                mov       [esi+38h],cl
  0041ee3a  c6463900              mov       byte [esi+39h],0
  0041ee3e  c6463a00              mov       byte [esi+3Ah],0

LAB_0041ee42:
  0041ee42  663d0200              cmp       ax,2
  0041ee46  750a                  jne       short 0041EE52h
  0041ee48  884e38                mov       [esi+38h],cl
  0041ee4b  c6463900              mov       byte [esi+39h],0
  0041ee4f  884e3a                mov       [esi+3Ah],cl

LAB_0041ee52:
  0041ee52  806636fe              and       byte [esi+36h],0FEh
  0041ee56  66895616              mov       [esi+16h],dx
  0041ee5a  895644                mov       [esi+44h],edx

LAB_0041ee5d:
  0041ee5d  66837e1001            cmp       word [esi+10h],1
  0041ee62  0f8526030000          jne       near 0041F18Eh
  0041ee68  668b4332              mov       ax,[ebx+32h]
  0041ee6c  66c746340004          mov       word [esi+34h],400h
  0041ee72  6605000c              add       ax,0C00h
  0041ee76  66894632              mov       [esi+32h],ax
  0041ee7a  8b4b20                mov       ecx,[ebx+20h]
  0041ee7d  8b7d54                mov       edi,[ebp+54h]
  0041ee80  0fbfd0                movsx     edx,ax
  0041ee83  8b465c                mov       eax,[esi+5Ch]
  0041ee86  03cf                  add       ecx,edi
  0041ee88  894e20                mov       [esi+20h],ecx
  0041ee8b  8d7e1c                lea       edi,[esi+1Ch]
  0041ee8e  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0041ee95  51                    push      ecx
  0041ee96  e8816b1200            call      00545A1Ch
  0041ee9b  8b4d50                mov       ecx,[ebp+50h]
  0041ee9e  8d0440                lea       eax,[eax+eax*2]
  0041eea1  8d1480                lea       edx,[eax+eax*4]
  0041eea4  8b431c                mov       eax,[ebx+1Ch]
  0041eea7  03d0                  add       edx,eax
  0041eea9  0fbf4632              movsx     eax,word [esi+32h]
  0041eead  03d1                  add       edx,ecx
  0041eeaf  8b4e5c                mov       ecx,[esi+5Ch]
  0041eeb2  8917                  mov       [edi],edx
  0041eeb4  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  0041eebb  52                    push      edx
  0041eebc  e8556b1200            call      00545A16h
  0041eec1  8b5324                mov       edx,[ebx+24h]
  0041eec4  8b4d58                mov       ecx,[ebp+58h]
  0041eec7  8d0440                lea       eax,[eax+eax*2]
  0041eeca  8d0480                lea       eax,[eax+eax*4]
  0041eecd  03c2                  add       eax,edx
  0041eecf  03c1                  add       eax,ecx
  0041eed1  66837e120a            cmp       word [esi+12h],0Ah
  0041eed6  894624                mov       [esi+24h],eax
  0041eed9  7515                  jne       short 0041EEF0h
  0041eedb  6aff                  push      0FFFFFFFFh
  0041eedd  6a00                  push      0
  0041eedf  68ee000000            push      0EEh
  0041eee4  6a00                  push      0
  0041eee6  57                    push      edi
  0041eee7  56                    push      esi
  0041eee8  e853bd0500            call      0047AC40h
  0041eeed  83c418                add       esp,18h

LAB_0041eef0:
  0041eef0  8b0f                  mov       ecx,[edi]
  0041eef2  8b5620                mov       edx,[esi+20h]
  0041eef5  8b4624                mov       eax,[esi+24h]
  0041eef8  894c2424              mov       [esp+24h],ecx
  0041eefc  0fbf4e32              movsx     ecx,word [esi+32h]
  0041ef00  81e9000c0000          sub       ecx,0C00h
  0041ef06  89542428              mov       [esp+28h],edx
  0041ef0a  51                    push      ecx
  0041ef0b  89442430              mov       [esp+30h],eax
  0041ef0f  e8086b1200            call      00545A1Ch
  0041ef14  0fbf5616              movsx     edx,word [esi+16h]
  0041ef18  0fafc2                imul      eax,edx
  0041ef1b  8b1f                  mov       ebx,[edi]
  0041ef1d  03d8                  add       ebx,eax
  0041ef1f  0fbf4632              movsx     eax,word [esi+32h]
  0041ef23  2d000c0000            sub       eax,0C00h
  0041ef28  891f                  mov       [edi],ebx
  0041ef2a  50                    push      eax
  0041ef2b  e8e66a1200            call      00545A16h
  0041ef30  0fbf4e16              movsx     ecx,word [esi+16h]
  0041ef34  0fafc1                imul      eax,ecx
  0041ef37  8b4e24                mov       ecx,[esi+24h]
  0041ef3a  c744241400000000      mov       dword [esp+14h],0
  0041ef42  03c8                  add       ecx,eax
  0041ef44  b8c0935e00            mov       eax,5E93C0h
  0041ef49  894e24                mov       [esi+24h],ecx
  0041ef4c  89442434              mov       [esp+34h],eax
  0041ef50  c744241804000000      mov       dword [esp+18h],4

LAB_0041ef58:
  0041ef58  8b08                  mov       ecx,[eax]
  0041ef5a  f7c100000080          test      ecx,80000000h
  0041ef60  0f8483010000          je        near 0041F0E9h
  0041ef66  f7c100000050          test      ecx,50000000h
  0041ef6c  0f8577010000          jne       near 0041F0E9h
  0041ef72  8b781c                mov       edi,[eax+1Ch]
  0041ef75  8b6e1c                mov       ebp,[esi+1Ch]
  0041ef78  8b5820                mov       ebx,[eax+20h]
  0041ef7b  8b5620                mov       edx,[esi+20h]
  0041ef7e  2bfd                  sub       edi,ebp
  0041ef80  8b6824                mov       ebp,[eax+24h]
  0041ef83  8b4624                mov       eax,[esi+24h]
  0041ef86  2bda                  sub       ebx,edx
  0041ef88  2be8                  sub       ebp,eax
  0041ef8a  c1fd0c                sar       ebp,0Ch
  0041ef8d  c1ff0c                sar       edi,0Ch
  0041ef90  55                    push      ebp
  0041ef91  57                    push      edi
  0041ef92  c1fb0c                sar       ebx,0Ch
  0041ef95  e8886a1200            call      00545A22h
  0041ef9a  8b542410              mov       edx,[esp+10h]
  0041ef9e  0fbf4a32              movsx     ecx,word [edx+32h]
  0041efa2  2bc1                  sub       eax,ecx
  0041efa4  8bd7                  mov       edx,edi
  0041efa6  25ff0f0000            and       eax,0FFFh
  0041efab  0fafd7                imul      edx,edi
  0041efae  89442420              mov       [esp+20h],eax
  0041efb2  8bc3                  mov       eax,ebx
  0041efb4  0fafc3                imul      eax,ebx
  0041efb7  8bcd                  mov       ecx,ebp
  0041efb9  03d0                  add       edx,eax
  0041efbb  0fafcd                imul      ecx,ebp
  0041efbe  03d1                  add       edx,ecx
  0041efc0  52                    push      edx
  0041efc1  e84a6a1200            call      00545A10h
  0041efc6  3dc8000000            cmp       eax,0C8h
  0041efcb  0f8d14010000          jge       near 0041F0E5h
  0041efd1  8b442420              mov       eax,[esp+20h]
  0041efd5  3d20030000            cmp       eax,320h
  0041efda  7d04                  jge       short 0041EFE0h
  0041efdc  85c0                  test      eax,eax
  0041efde  7f16                  jg        short 0041EFF6h

LAB_0041efe0:
  0041efe0  3d00100000            cmp       eax,1000h
  0041efe5  0f8dfa000000          jge       near 0041F0E5h
  0041efeb  3de00c0000            cmp       eax,0CE0h
  0041eff0  0f8eef000000          jle       near 0041F0E5h

LAB_0041eff6:
  0041eff6  8b4c2414              mov       ecx,[esp+14h]
  0041effa  b801000000            mov       eax,1
  0041efff  d3e0                  shl       eax,cl
  0041f001  8b4e44                mov       ecx,[esi+44h]
  0041f004  85c1                  test      ecx,eax
  0041f006  0f85d9000000          jne       near 0041F0E5h
  0041f00c  0bc1                  or        eax,ecx
  0041f00e  6683be9a00000000      cmp       word [esi+9Ah],0
  0041f016  894644                mov       [esi+44h],eax
  0041f019  7516                  jne       short 0041F031h
  0041f01b  8b7c2434              mov       edi,[esp+34h]
  0041f01f  8b5c2410              mov       ebx,[esp+10h]
  0041f023  6a00                  push      0
  0041f025  57                    push      edi
  0041f026  53                    push      ebx
  0041f027  e8b4020000            call      0041F2E0h
  0041f02c  83c40c                add       esp,0Ch
  0041f02f  eb08                  jmp       short 0041F039h

LAB_0041f031:
  0041f031  8b5c2410              mov       ebx,[esp+10h]
  0041f035  8b7c2434              mov       edi,[esp+34h]

LAB_0041f039:
  0041f039  6683be9a00000001      cmp       word [esi+9Ah],1
  0041f041  0f8580000000          jne       near 0041F0C7h
  0041f047  e8f4801200            call      00547140h
  0041f04c  99                    cdq
  0041f04d  b909000000            mov       ecx,9
  0041f052  f7f9                  idiv      ecx
  0041f054  0fbfc2                movsx     eax,dx
  0041f057  83f808                cmp       eax,8
  0041f05a  776b                  ja        short 0041F0C7h
  0041f05c  ff2485b0f24100        jmp       dword [eax*4+41F2B0h]
  0041f063  6a00                  push      0
  0041f065  57                    push      edi
  0041f066  53                    push      ebx
  0041f067  e834040000            call      0041F4A0h
  0041f06c  eb56                  jmp       short 0041F0C4h
  0041f06e  6a00                  push      0
  0041f070  57                    push      edi
  0041f071  53                    push      ebx
  0041f072  e869020000            call      0041F2E0h
  0041f077  eb4b                  jmp       short 0041F0C4h
  0041f079  6a00                  push      0
  0041f07b  57                    push      edi
  0041f07c  53                    push      ebx
  0041f07d  e8de030000            call      0041F460h
  0041f082  eb40                  jmp       short 0041F0C4h
  0041f084  6a00                  push      0
  0041f086  57                    push      edi
  0041f087  53                    push      ebx
  0041f088  e893020000            call      0041F320h
  0041f08d  eb35                  jmp       short 0041F0C4h
  0041f08f  6a00                  push      0
  0041f091  57                    push      edi
  0041f092  53                    push      ebx
  0041f093  e808030000            call      0041F3A0h
  0041f098  eb2a                  jmp       short 0041F0C4h
  0041f09a  6a00                  push      0
  0041f09c  57                    push      edi
  0041f09d  53                    push      ebx
  0041f09e  e8bd020000            call      0041F360h
  0041f0a3  eb1f                  jmp       short 0041F0C4h
  0041f0a5  6a00                  push      0
  0041f0a7  57                    push      edi
  0041f0a8  53                    push      ebx
  0041f0a9  e832030000            call      0041F3E0h
  0041f0ae  eb14                  jmp       short 0041F0C4h
  0041f0b0  6a00                  push      0
  0041f0b2  57                    push      edi
  0041f0b3  53                    push      ebx
  0041f0b4  e827040000            call      0041F4E0h
  0041f0b9  eb09                  jmp       short 0041F0C4h
  0041f0bb  6a00                  push      0
  0041f0bd  57                    push      edi
  0041f0be  53                    push      ebx
  0041f0bf  e85c030000            call      0041F420h

LAB_0041f0c4:
  0041f0c4  83c40c                add       esp,0Ch

LAB_0041f0c7:
  0041f0c7  6683be9a00000002      cmp       word [esi+9Ah],2
  0041f0cf  7514                  jne       short 0041F0E5h
  0041f0d1  8b542434              mov       edx,[esp+34h]
  0041f0d5  8b442410              mov       eax,[esp+10h]
  0041f0d9  6a00                  push      0
  0041f0db  52                    push      edx
  0041f0dc  50                    push      eax
  0041f0dd  e83e020000            call      0041F320h
  0041f0e2  83c40c                add       esp,0Ch

LAB_0041f0e5:
  0041f0e5  8b442434              mov       eax,[esp+34h]

LAB_0041f0e9:
  0041f0e9  8b7c2414              mov       edi,[esp+14h]
  0041f0ed  8b4c2418              mov       ecx,[esp+18h]
  0041f0f1  47                    inc       edi
  0041f0f2  059c000000            add       eax,9Ch
  0041f0f7  49                    dec       ecx
  0041f0f8  897c2414              mov       [esp+14h],edi
  0041f0fc  89442434              mov       [esp+34h],eax
  0041f100  894c2418              mov       [esp+18h],ecx
  0041f104  0f854efeffff          jne       near 0041EF58h
  0041f10a  56                    push      esi
  0041f10b  e830481100            call      00533940h
  0041f110  8b442430              mov       eax,[esp+30h]
  0041f114  8b4c2428              mov       ecx,[esp+28h]
  0041f118  8b54242c              mov       edx,[esp+2Ch]
  0041f11c  894624                mov       [esi+24h],eax
  0041f11f  668b462a              mov       ax,[esi+2Ah]
  0041f123  894e1c                mov       [esi+1Ch],ecx
  0041f126  0fbfc8                movsx     ecx,ax
  0041f129  895620                mov       [esi+20h],edx
  0041f12c  ba00400000            mov       edx,4000h
  0041f131  2bd1                  sub       edx,ecx
  0041f133  83c404                add       esp,4
  0041f136  c1fa02                sar       edx,2
  0041f139  03d0                  add       edx,eax
  0041f13b  668b4616              mov       ax,[esi+16h]
  0041f13f  0fbfc8                movsx     ecx,ax
  0041f142  6689562a              mov       [esi+2Ah],dx
  0041f146  ba58020000            mov       edx,258h
  0041f14b  2bd1                  sub       edx,ecx
  0041f14d  c1fa02                sar       edx,2
  0041f150  03d0                  add       edx,eax
  0041f152  8b8690000000          mov       eax,[esi+90h]
  0041f158  a902880000            test      eax,8802h
  0041f15d  66895616              mov       [esi+16h],dx
  0041f161  740c                  je        short 0041F16Fh
  0041f163  66c746100200          mov       word [esi+10h],2
  0041f169  66c746120a00          mov       word [esi+12h],0Ah

LAB_0041f16f:
  0041f16f  66ff4e12              dec       word [esi+12h]
  0041f173  66837e1200            cmp       word [esi+12h],0
  0041f178  7f0c                  jg        short 0041F186h
  0041f17a  66c746100200          mov       word [esi+10h],2
  0041f180  66c746120a00          mov       word [esi+12h],0Ah

LAB_0041f186:
  0041f186  8b5c2410              mov       ebx,[esp+10h]
  0041f18a  8b6c241c              mov       ebp,[esp+1Ch]

LAB_0041f18e:
  0041f18e  66837e1002            cmp       word [esi+10h],2
  0041f193  0f850f010000          jne       near 0041F2A8h
  0041f199  668b4332              mov       ax,[ebx+32h]
  0041f19d  66c746340004          mov       word [esi+34h],400h
  0041f1a3  6605000c              add       ax,0C00h
  0041f1a7  66894632              mov       [esi+32h],ax
  0041f1ab  8b4b20                mov       ecx,[ebx+20h]
  0041f1ae  8b7d54                mov       edi,[ebp+54h]
  0041f1b1  0fbfd0                movsx     edx,ax
  0041f1b4  8b465c                mov       eax,[esi+5Ch]
  0041f1b7  03cf                  add       ecx,edi
  0041f1b9  894e20                mov       [esi+20h],ecx
  0041f1bc  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0041f1c3  51                    push      ecx
  0041f1c4  e853681200            call      00545A1Ch
  0041f1c9  8b4b1c                mov       ecx,[ebx+1Ch]
  0041f1cc  8d0440                lea       eax,[eax+eax*2]
  0041f1cf  8d1480                lea       edx,[eax+eax*4]
  0041f1d2  8b4550                mov       eax,[ebp+50h]
  0041f1d5  03d1                  add       edx,ecx
  0041f1d7  8b4e5c                mov       ecx,[esi+5Ch]
  0041f1da  03d0                  add       edx,eax
  0041f1dc  0fbf4632              movsx     eax,word [esi+32h]
  0041f1e0  89561c                mov       [esi+1Ch],edx
  0041f1e3  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  0041f1ea  52                    push      edx
  0041f1eb  e826681200            call      00545A16h
  0041f1f0  8b5324                mov       edx,[ebx+24h]
  0041f1f3  8b4d58                mov       ecx,[ebp+58h]
  0041f1f6  8d0440                lea       eax,[eax+eax*2]
  0041f1f9  8d0480                lea       eax,[eax+eax*4]
  0041f1fc  03c2                  add       eax,edx
  0041f1fe  03c1                  add       eax,ecx
  0041f200  0fbf4e32              movsx     ecx,word [esi+32h]
  0041f204  81e9000c0000          sub       ecx,0C00h
  0041f20a  894624                mov       [esi+24h],eax
  0041f20d  51                    push      ecx
  0041f20e  e809681200            call      00545A1Ch
  0041f213  0fbf5616              movsx     edx,word [esi+16h]
  0041f217  0fafc2                imul      eax,edx
  0041f21a  8b7e1c                mov       edi,[esi+1Ch]
  0041f21d  03f8                  add       edi,eax
  0041f21f  0fbf4632              movsx     eax,word [esi+32h]
  0041f223  2d000c0000            sub       eax,0C00h
  0041f228  897e1c                mov       [esi+1Ch],edi
  0041f22b  50                    push      eax
  0041f22c  e8e5671200            call      00545A16h
  0041f231  0fbf4e16              movsx     ecx,word [esi+16h]
  0041f235  0fafc1                imul      eax,ecx
  0041f238  8b6e24                mov       ebp,[esi+24h]
  0041f23b  66c74634000c          mov       word [esi+34h],0C00h
  0041f241  03e8                  add       ebp,eax
  0041f243  668b462a              mov       ax,[esi+2Ah]
  0041f247  0fbfd0                movsx     edx,ax
  0041f24a  f7da                  neg       edx
  0041f24c  c1fa02                sar       edx,2
  0041f24f  03d0                  add       edx,eax
  0041f251  8a4638                mov       al,[esi+38h]
  0041f254  8bc8                  mov       ecx,eax
  0041f256  6689562a              mov       [esi+2Ah],dx
  0041f25a  81e1ff000000          and       ecx,0FFh
  0041f260  896e24                mov       [esi+24h],ebp
  0041f263  f7d9                  neg       ecx
  0041f265  c1f903                sar       ecx,3
  0041f268  02c8                  add       cl,al
  0041f26a  8a4639                mov       al,[esi+39h]
  0041f26d  8bd0                  mov       edx,eax
  0041f26f  884e38                mov       [esi+38h],cl
  0041f272  81e2ff000000          and       edx,0FFh
  0041f278  f7da                  neg       edx
  0041f27a  c1fa03                sar       edx,3
  0041f27d  02d0                  add       dl,al
  0041f27f  8a463a                mov       al,[esi+3Ah]
  0041f282  8bc8                  mov       ecx,eax
  0041f284  885639                mov       [esi+39h],dl
  0041f287  81e1ff000000          and       ecx,0FFh
  0041f28d  f7d9                  neg       ecx
  0041f28f  c1f903                sar       ecx,3
  0041f292  02c8                  add       cl,al
  0041f294  66ff4e12              dec       word [esi+12h]
  0041f298  66837e1200            cmp       word [esi+12h],0
  0041f29d  884e3a                mov       [esi+3Ah],cl
  0041f2a0  7f06                  jg        short 0041F2A8h
  0041f2a2  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041f2a8:
  0041f2a8  5f                    pop       edi
  0041f2a9  5e                    pop       esi
  0041f2aa  5d                    pop       ebp
  0041f2ab  5b                    pop       ebx
  0041f2ac  83c420                add       esp,20h
  0041f2af  c3                    ret
  0041f2b0  63f0                  arpl      ax,si
  0041f2b2  41                    inc       ecx
  0041f2b3  006ef0                add       [esi-10h],ch
  0041f2b6  41                    inc       ecx
  0041f2b7  0079f0                add       [ecx-10h],bh
  0041f2ba  41                    inc       ecx
  0041f2bb  0084f041008ff0        add       [eax+esi*8-0F70FFBFh],al
  0041f2c2  41                    inc       ecx
  0041f2c3  009af04100a5          add       [edx-5AFFBE10h],bl
  0041f2c9  f041                  (bad)
  (octet invalide - la plage demandee deborde probablement du code)
