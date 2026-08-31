
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047bdf0  section .text  offset fichier 0x7b1f0  270 octets

  0047bdf0  56                    push      esi
  0047bdf1  8b742408              mov       esi,[esp+8]
  0047bdf5  57                    push      edi
  0047bdf6  668b4610              mov       ax,[esi+10h]
  0047bdfa  8b7e78                mov       edi,[esi+78h]
  0047bdfd  6685c0                test      ax,ax
  0047be00  751f                  jne       short 0047BE21h
  0047be02  c7462800000000        mov       dword [esi+28h],0
  0047be09  c7463800000000        mov       dword [esi+38h],0
  0047be10  66c7460a0700          mov       word [esi+0Ah],7
  0047be16  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047be1c  e99a000000            jmp       0047BEBBh

LAB_0047be21:
  0047be21  663d2000              cmp       ax,20h
  0047be25  7d23                  jge       short 0047BE4Ah
  0047be27  66817e280010          cmp       word [esi+28h],1000h
  0047be2d  7d07                  jge       short 0047BE36h
  0047be2f  81462800010001        add       dword [esi+28h],1000100h

LAB_0047be36:
  0047be36  8b4638                mov       eax,[esi+38h]
  0047be39  3dd0d0d000            cmp       eax,0D0D0D0h
  0047be3e  737b                  jae       short 0047BEBBh
  0047be40  0508080800            add       eax,80808h
  0047be45  894638                mov       [esi+38h],eax
  0047be48  eb71                  jmp       short 0047BEBBh

LAB_0047be4a:
  0047be4a  7532                  jne       short 0047BE7Eh
  0047be4c  33c0                  xor       eax,eax
  0047be4e  6aff                  push      0FFFFFFFFh
  0047be50  8a460b                mov       al,[esi+0Bh]
  0047be53  66c746120000          mov       word [esi+12h],0
  0047be59  50                    push      eax
  0047be5a  6a68                  push      68h
  0047be5c  56                    push      esi
  0047be5d  66c746140020          mov       word [esi+14h],2000h
  0047be63  e878ecffff            call      0047AAE0h
  0047be68  83c410                add       esp,10h
  0047be6b  33c9                  xor       ecx,ecx
  0047be6d  8a4e0b                mov       cl,[esi+0Bh]
  0047be70  6aff                  push      0FFFFFFFFh
  0047be72  51                    push      ecx
  0047be73  6a68                  push      68h
  0047be75  56                    push      esi
  0047be76  e865ecffff            call      0047AAE0h
  0047be7b  83c410                add       esp,10h

LAB_0047be7e:
  0047be7e  8b4638                mov       eax,[esi+38h]
  0047be81  85c0                  test      eax,eax
  0047be83  7408                  je        short 0047BE8Dh
  0047be85  05f8f7f7ff            add       eax,0FFF7F7F8h
  0047be8a  894638                mov       [esi+38h],eax

LAB_0047be8d:
  0047be8d  668b4628              mov       ax,[esi+28h]
  0047be91  b900200000            mov       ecx,2000h
  0047be96  0fbfd0                movsx     edx,ax
  0047be99  2bca                  sub       ecx,edx
  0047be9b  c1f902                sar       ecx,2
  0047be9e  03c8                  add       ecx,eax
  0047bea0  668b462a              mov       ax,[esi+2Ah]
  0047bea4  0fbfd0                movsx     edx,ax
  0047bea7  66894e28              mov       [esi+28h],cx
  0047beab  b900200000            mov       ecx,2000h
  0047beb0  2bca                  sub       ecx,edx
  0047beb2  c1f902                sar       ecx,2
  0047beb5  03c8                  add       ecx,eax
  0047beb7  66894e2a              mov       [esi+2Ah],cx

LAB_0047bebb:
  0047bebb  8b571c                mov       edx,[edi+1Ch]
  0047bebe  8b4e74                mov       ecx,[esi+74h]
  0047bec1  89561c                mov       [esi+1Ch],edx
  0047bec4  8b4724                mov       eax,[edi+24h]
  0047bec7  51                    push      ecx
  0047bec8  894624                mov       [esi+24h],eax
  0047becb  e8f0cc0b00            call      00538BC0h
  0047bed0  8b5720                mov       edx,[edi+20h]
  0047bed3  24fe                  and       al,0FEh
  0047bed5  c1e00b                shl       eax,0Bh
  0047bed8  03c2                  add       eax,edx
  0047beda  83c404                add       esp,4
  0047bedd  894620                mov       [esi+20h],eax
  0047bee0  668b4610              mov       ax,[esi+10h]
  0047bee4  6685c0                test      ax,ax
  0047bee7  740d                  je        short 0047BEF6h
  0047bee9  8b4e38                mov       ecx,[esi+38h]
  0047beec  85c9                  test      ecx,ecx
  0047beee  7506                  jne       short 0047BEF6h
  0047bef0  c70600000000          mov       dword [esi],0

LAB_0047bef6:
  0047bef6  40                    inc       eax
  0047bef7  5f                    pop       edi
  0047bef8  66894610              mov       [esi+10h],ax
  0047befc  5e                    pop       esi
  0047befd  c3                    ret

7 cible(s) de saut interne : 0x0047be21, 0x0047be36, 0x0047be4a, 0x0047be7e, 0x0047be8d, 0x0047bebb, 0x0047bef6
