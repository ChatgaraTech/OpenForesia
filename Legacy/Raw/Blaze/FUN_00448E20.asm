Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448e20  section .text  offset fichier 0x48220  194 octets

  00448e20  53                    push      ebx
  00448e21  56                    push      esi
  00448e22  8b74240c              mov       esi,[esp+0Ch]
  00448e26  33db                  xor       ebx,ebx
  00448e28  66395e10              cmp       [esi+10h],bx
  00448e2c  7577                  jne       short 00448EA5h
  00448e2e  8a465c                mov       al,[esi+5Ch]
  00448e31  c7463c28155500        mov       dword [esi+3Ch],551528h
  00448e38  88460a                mov       [esi+0Ah],al
  00448e3b  b8a00f0000            mov       eax,0FA0h
  00448e40  885e40                mov       [esi+40h],bl
  00448e43  885e0b                mov       [esi+0Bh],bl
  00448e46  66894628              mov       [esi+28h],ax
  00448e4a  6689462a              mov       [esi+2Ah],ax
  00448e4e  66c746100100          mov       word [esi+10h],1
  00448e54  e8e7e20f00            call      00547140h
  00448e59  99                    cdq
  00448e5a  b964000000            mov       ecx,64h
  00448e5f  f7f9                  idiv      ecx
  00448e61  8b461c                mov       eax,[esi+1Ch]
  00448e64  83ea32                sub       edx,32h
  00448e67  c1e20c                shl       edx,0Ch
  00448e6a  03c2                  add       eax,edx
  00448e6c  89461c                mov       [esi+1Ch],eax
  00448e6f  e8cce20f00            call      00547140h
  00448e74  99                    cdq
  00448e75  b964000000            mov       ecx,64h
  00448e7a  f7f9                  idiv      ecx
  00448e7c  8b4e20                mov       ecx,[esi+20h]
  00448e7f  83ea32                sub       edx,32h
  00448e82  c1e20c                shl       edx,0Ch
  00448e85  03ca                  add       ecx,edx
  00448e87  894e20                mov       [esi+20h],ecx
  00448e8a  e8b1e20f00            call      00547140h
  00448e8f  99                    cdq
  00448e90  b964000000            mov       ecx,64h
  00448e95  f7f9                  idiv      ecx
  00448e97  8b4624                mov       eax,[esi+24h]
  00448e9a  83ea32                sub       edx,32h
  00448e9d  c1e20c                shl       edx,0Ch
  00448ea0  03c2                  add       eax,edx
  00448ea2  894624                mov       [esi+24h],eax

LAB_00448ea5:
  00448ea5  66837e1001            cmp       word [esi+10h],1
  00448eaa  7533                  jne       short 00448EDFh
  00448eac  8b5620                mov       edx,[esi+20h]
  00448eaf  b970feffff            mov       ecx,0FFFFFE70h
  00448eb4  66014e28              add       [esi+28h],cx
  00448eb8  668b4628              mov       ax,[esi+28h]
  00448ebc  66014e2a              add       [esi+2Ah],cx
  00448ec0  81c200500000          add       edx,5000h
  00448ec6  663bc3                cmp       ax,bx
  00448ec9  895620                mov       [esi+20h],edx
  00448ecc  7d11                  jge       short 00448EDFh
  00448ece  8b06                  mov       eax,[esi]
  00448ed0  66895e28              mov       [esi+28h],bx
  00448ed4  25ffffff7f            and       eax,7FFFFFFFh
  00448ed9  66895e2a              mov       [esi+2Ah],bx
  00448edd  8906                  mov       [esi],eax

LAB_00448edf:
  00448edf  5e                    pop       esi
  00448ee0  5b                    pop       ebx
  00448ee1  c3                    ret

2 cible(s) de saut interne : 0x00448ea5, 0x00448edf
