Legacy/Binaries/Blaze.exe  base 0x00400000
0x00476500  section .text  offset fichier 0x75900  197 octets

  00476500  53                    push      ebx
  00476501  56                    push      esi
  00476502  8b74240c              mov       esi,[esp+0Ch]
  00476506  57                    push      edi
  00476507  66837e1000            cmp       word [esi+10h],0
  0047650c  754a                  jne       short 00476558h
  0047650e  8b466c                mov       eax,[esi+6Ch]
  00476511  804e3606              or        byte [esi+36h],6
  00476515  50                    push      eax
  00476516  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  0047651c  894678                mov       [esi+78h],eax
  0047651f  894670                mov       [esi+70h],eax
  00476522  e899260c00            call      00538BC0h
  00476527  8bc8                  mov       ecx,eax
  00476529  83c404                add       esp,4
  0047652c  d1f9                  sar       ecx,1
  0047652e  c1e00c                shl       eax,0Ch
  00476531  66894e12              mov       [esi+12h],cx
  00476535  8bc8                  mov       ecx,eax
  00476537  b8b5814e1b            mov       eax,1B4E81B5h
  0047653c  66c746140300          mov       word [esi+14h],3
  00476542  f7e9                  imul      ecx
  00476544  c1fa04                sar       edx,4
  00476547  8bc2                  mov       eax,edx
  00476549  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00476550  c1e81f                shr       eax,1Fh
  00476553  03d0                  add       edx,eax
  00476555  895628                mov       [esi+28h],edx

LAB_00476558:
  00476558  66837e1018            cmp       word [esi+10h],18h
  0047655d  7d4d                  jge       short 004765ACh
  0047655f  e8dc0b0d00            call      00547140h
  00476564  8bf8                  mov       edi,eax
  00476566  e8d50b0d00            call      00547140h
  0047656b  0fbf4e12              movsx     ecx,word [esi+12h]
  0047656f  99                    cdq
  00476570  f7f9                  idiv      ecx
  00476572  57                    push      edi
  00476573  66897e32              mov       [esi+32h],di
  00476577  8bda                  mov       ebx,edx
  00476579  e89ef40c00            call      00545A1Ch
  0047657e  0fafc3                imul      eax,ebx
  00476581  57                    push      edi
  00476582  894644                mov       [esi+44h],eax
  00476585  e88cf40c00            call      00545A16h
  0047658a  0fafc3                imul      eax,ebx
  0047658d  33d2                  xor       edx,edx
  0047658f  6aff                  push      0FFFFFFFFh
  00476591  8a560b                mov       dl,[esi+0Bh]
  00476594  89464c                mov       [esi+4Ch],eax
  00476597  52                    push      edx
  00476598  6a56                  push      56h
  0047659a  8d461c                lea       eax,[esi+1Ch]
  0047659d  68a0195500            push      5519A0h
  004765a2  50                    push      eax
  004765a3  56                    push      esi
  004765a4  e897460000            call      0047AC40h
  004765a9  83c418                add       esp,18h

LAB_004765ac:
  004765ac  668b4610              mov       ax,[esi+10h]
  004765b0  663d3c00              cmp       ax,3Ch
  004765b4  7c06                  jl        short 004765BCh
  004765b6  c70600000000          mov       dword [esi],0

LAB_004765bc:
  004765bc  40                    inc       eax
  004765bd  5f                    pop       edi
  004765be  66894610              mov       [esi+10h],ax
  004765c2  5e                    pop       esi
  004765c3  5b                    pop       ebx
  004765c4  c3                    ret

3 cible(s) de saut interne : 0x00476558, 0x004765ac, 0x004765bc
