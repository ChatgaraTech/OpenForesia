Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456f40  section .text  offset fichier 0x56340  234 octets

  00456f40  53                    push      ebx
  00456f41  56                    push      esi
  00456f42  8b74240c              mov       esi,[esp+0Ch]
  00456f46  57                    push      edi
  00456f47  bf63000000            mov       edi,63h
  00456f4c  66837e1000            cmp       word [esi+10h],0
  00456f51  7540                  jne       short 00456F93h
  00456f53  66897e10              mov       [esi+10h],di
  00456f57  e8e4010f00            call      00547140h
  00456f5c  99                    cdq
  00456f5d  b905000000            mov       ecx,5
  00456f62  f7f9                  idiv      ecx
  00456f64  03d1                  add       edx,ecx
  00456f66  66895612              mov       [esi+12h],dx
  00456f6a  e8d1010f00            call      00547140h
  00456f6f  99                    cdq
  00456f70  b90f000000            mov       ecx,0Fh
  00456f75  f7f9                  idiv      ecx
  00456f77  66895614              mov       [esi+14h],dx
  00456f7b  e8c0010f00            call      00547140h
  00456f80  99                    cdq
  00456f81  b964000000            mov       ecx,64h
  00456f86  f7f9                  idiv      ecx
  00456f88  8b06                  mov       eax,[esi]
  00456f8a  80cc40                or        ah,40h
  00456f8d  8906                  mov       [esi],eax
  00456f8f  66895616              mov       [esi+16h],dx

LAB_00456f93:
  00456f93  66397e10              cmp       [esi+10h],di
  00456f97  7518                  jne       short 00456FB1h
  00456f99  66ff4e16              dec       word [esi+16h]
  00456f9d  66837e1600            cmp       word [esi+16h],0
  00456fa2  7d0d                  jge       short 00456FB1h
  00456fa4  8b06                  mov       eax,[esi]
  00456fa6  66c746100100          mov       word [esi+10h],1
  00456fac  80e4bf                and       ah,0BFh
  00456faf  8906                  mov       [esi],eax

LAB_00456fb1:
  00456fb1  66837e1001            cmp       word [esi+10h],1
  00456fb6  bf00100000            mov       edi,1000h
  00456fbb  756d                  jne       short 0045702Ah
  00456fbd  e87e010f00            call      00547140h
  00456fc2  99                    cdq
  00456fc3  33c2                  xor       eax,edx
  00456fc5  2bc2                  sub       eax,edx
  00456fc7  83e001                and       eax,1
  00456fca  33c2                  xor       eax,edx
  00456fcc  2bc2                  sub       eax,edx
  00456fce  663d0100              cmp       ax,1
  00456fd2  7513                  jne       short 00456FE7h
  00456fd4  6a00                  push      0
  00456fd6  6a00                  push      0
  00456fd8  6aff                  push      0FFFFFFFFh
  00456fda  6a00                  push      0
  00456fdc  6a32                  push      32h
  00456fde  56                    push      esi
  00456fdf  e82c110100            call      00468110h
  00456fe4  83c418                add       esp,18h

LAB_00456fe7:
  00456fe7  0fbf5612              movsx     edx,word [esi+12h]
  00456feb  8b461c                mov       eax,[esi+1Ch]
  00456fee  c1e20c                shl       edx,0Ch
  00456ff1  03c2                  add       eax,edx
  00456ff3  89461c                mov       [esi+1Ch],eax
  00456ff6  8bc8                  mov       ecx,eax
  00456ff8  668b4614              mov       ax,[esi+14h]
  00456ffc  66014632              add       [esi+32h],ax
  00457000  668b4632              mov       ax,[esi+32h]
  00457004  663bc7                cmp       ax,di
  00457007  7e09                  jle       short 00457012h
  00457009  0500f0ffff            add       eax,0FFFFF000h
  0045700e  66894632              mov       [esi+32h],ax

LAB_00457012:
  00457012  81f9002004ff          cmp       ecx,0FF042000h
  00457018  7e10                  jle       short 0045702Ah
  0045701a  66c746100200          mov       word [esi+10h],2
  00457020  66c746160500          mov       word [esi+16h],5
  00457026  5f                    pop       edi
  00457027  5e                    pop       esi
  00457028  5b                    pop       ebx
  00457029  c3                    ret

4 cible(s) de saut interne : 0x00456f93, 0x00456fb1, 0x00456fe7, 0x00457012
