
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047ce40  section .text  offset fichier 0x7c240  240 octets

  0047ce40  53                    push      ebx
  0047ce41  56                    push      esi
  0047ce42  8b74240c              mov       esi,[esp+0Ch]
  0047ce46  33db                  xor       ebx,ebx
  0047ce48  57                    push      edi
  0047ce49  66395e10              cmp       [esi+10h],bx
  0047ce4d  754b                  jne       short 0047CE9Ah
  0047ce4f  804e3606              or        byte [esi+36h],6
  0047ce53  53                    push      ebx
  0047ce54  56                    push      esi
  0047ce55  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  0047ce5b  e8407dffff            call      00474BA0h
  0047ce60  894678                mov       [esi+78h],eax
  0047ce63  894670                mov       [esi+70h],eax
  0047ce66  83c408                add       esp,8
  0047ce69  33c0                  xor       eax,eax
  0047ce6b  8a460b                mov       al,[esi+0Bh]
  0047ce6e  8d7e1c                lea       edi,[esi+1Ch]
  0047ce71  6aff                  push      0FFFFFFFFh
  0047ce73  50                    push      eax
  0047ce74  6a4c                  push      4Ch
  0047ce76  68a01b5500            push      551BA0h
  0047ce7b  57                    push      edi
  0047ce7c  56                    push      esi
  0047ce7d  e8beddffff            call      0047AC40h
  0047ce82  83c418                add       esp,18h
  0047ce85  57                    push      edi
  0047ce86  6800120000            push      1200h
  0047ce8b  68d4030000            push      3D4h
  0047ce90  6a10                  push      10h
  0047ce92  e8890c0c00            call      0053DB20h
  0047ce97  83c410                add       esp,10h

LAB_0047ce9a:
  0047ce9a  66837e1018            cmp       word [esi+10h],18h
  0047ce9f  7d62                  jge       short 0047CF03h
  0047cea1  e89aa20c00            call      00547140h
  0047cea6  0500040000            add       eax,400h
  0047ceab  66894632              mov       [esi+32h],ax
  0047ceaf  e88ca20c00            call      00547140h
  0047ceb4  8b4e78                mov       ecx,[esi+78h]
  0047ceb7  66894634              mov       [esi+34h],ax
  0047cebb  51                    push      ecx
  0047cebc  c74628ff7f0000        mov       dword [esi+28h],7FFFh
  0047cec3  66c7462c0004          mov       word [esi+2Ch],400h
  0047cec9  895e38                mov       [esi+38h],ebx
  0047cecc  66c74612f401          mov       word [esi+12h],1F4h
  0047ced2  66895e14              mov       [esi+14h],bx
  0047ced6  895e44                mov       [esi+44h],ebx
  0047ced9  e8e2bc0b00            call      00538BC0h
  0047cede  83c404                add       esp,4
  0047cee1  33d2                  xor       edx,edx
  0047cee3  8a560b                mov       dl,[esi+0Bh]
  0047cee6  24fe                  and       al,0FEh
  0047cee8  6aff                  push      0FFFFFFFFh
  0047ceea  52                    push      edx
  0047ceeb  c1e00b                shl       eax,0Bh
  0047ceee  6a70                  push      70h
  0047cef0  56                    push      esi
  0047cef1  894648                mov       [esi+48h],eax
  0047cef4  895e4c                mov       [esi+4Ch],ebx
  0047cef7  c646043f              mov       byte [esi+4],3Fh
  0047cefb  e8e0dbffff            call      0047AAE0h
  0047cf00  83c410                add       esp,10h

LAB_0047cf03:
  0047cf03  66837e103c            cmp       word [esi+10h],3Ch
  0047cf08  7c1e                  jl        short 0047CF28h
  0047cf0a  8b4674                mov       eax,[esi+74h]
  0047cf0d  668b4e06              mov       cx,[esi+6]
  0047cf11  53                    push      ebx
  0047cf12  66894806              mov       [eax+6],cx
  0047cf16  8b5678                mov       edx,[esi+78h]
  0047cf19  8b4674                mov       eax,[esi+74h]
  0047cf1c  52                    push      edx
  0047cf1d  50                    push      eax
  0047cf1e  e88de00b00            call      0053AFB0h
  0047cf23  83c40c                add       esp,0Ch
  0047cf26  891e                  mov       [esi],ebx

LAB_0047cf28:
  0047cf28  66ff4610              inc       word [esi+10h]
  0047cf2c  5f                    pop       edi
  0047cf2d  5e                    pop       esi
  0047cf2e  5b                    pop       ebx
  0047cf2f  c3                    ret

3 cible(s) de saut interne : 0x0047ce9a, 0x0047cf03, 0x0047cf28
