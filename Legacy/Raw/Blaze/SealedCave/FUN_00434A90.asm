Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00434a90  section .text  offset fichier 0x33e90  336 octets

  00434a90  53                    push      ebx
  00434a91  56                    push      esi
  00434a92  8b74240c              mov       esi,[esp+0Ch]
  00434a96  66837e1000            cmp       word [esi+10h],0
  00434a9b  0f85fb000000          jne       near 00434B9Ch
  00434aa1  b8c8000000            mov       eax,0C8h
  00434aa6  c6460400              mov       byte [esi+4],0
  00434aaa  c6460a03              mov       byte [esi+0Ah],3
  00434aae  c6460b01              mov       byte [esi+0Bh],1
  00434ab2  66894628              mov       [esi+28h],ax
  00434ab6  6689462a              mov       [esi+2Ah],ax
  00434aba  e881261100            call      00547140h
  00434abf  99                    cdq
  00434ac0  b9b80b0000            mov       ecx,0BB8h
  00434ac5  bbb4000000            mov       ebx,0B4h
  00434aca  f7f9                  idiv      ecx
  00434acc  885e38                mov       [esi+38h],bl
  00434acf  885e39                mov       [esi+39h],bl
  00434ad2  c6463afa              mov       byte [esi+3Ah],0FAh
  00434ad6  81c2a00f0000          add       edx,0FA0h
  00434adc  6689562c              mov       [esi+2Ch],dx
  00434ae0  a002975e00            mov       al,[5E9702h]
  00434ae5  a808                  test      al,8
  00434ae7  c7461c00403800        mov       dword [esi+1Ch],384000h
  00434aee  7452                  je        short 00434B42h
  00434af0  e84b261100            call      00547140h
  00434af5  99                    cdq
  00434af6  f7fb                  idiv      ebx
  00434af8  c1e20c                shl       edx,0Ch
  00434afb  895620                mov       [esi+20h],edx
  00434afe  e83d261100            call      00547140h
  00434b03  99                    cdq
  00434b04  b958020000            mov       ecx,258h
  00434b09  f7f9                  idiv      ecx
  00434b0b  b888faffff            mov       eax,0FFFFFA88h
  00434b10  2bc2                  sub       eax,edx
  00434b12  c1e00c                shl       eax,0Ch
  00434b15  894624                mov       [esi+24h],eax
  00434b18  e823261100            call      00547140h
  00434b1d  99                    cdq
  00434b1e  b932000000            mov       ecx,32h
  00434b23  66c746320004          mov       word [esi+32h],400h
  00434b29  f7f9                  idiv      ecx
  00434b2b  66c746340008          mov       word [esi+34h],800h
  00434b31  66c746120807          mov       word [esi+12h],708h
  00434b37  83c214                add       edx,14h
  00434b3a  c1e20c                shl       edx,0Ch
  00434b3d  895650                mov       [esi+50h],edx
  00434b40  eb54                  jmp       short 00434B96h

LAB_00434b42:
  00434b42  e8f9251100            call      00547140h
  00434b47  99                    cdq
  00434b48  f7fb                  idiv      ebx
  00434b4a  c1e20c                shl       edx,0Ch
  00434b4d  895620                mov       [esi+20h],edx
  00434b50  e8eb251100            call      00547140h
  00434b55  99                    cdq
  00434b56  b958020000            mov       ecx,258h
  00434b5b  f7f9                  idiv      ecx
  00434b5d  b888faffff            mov       eax,0FFFFFA88h
  00434b62  2bc2                  sub       eax,edx
  00434b64  c1e00c                shl       eax,0Ch
  00434b67  894624                mov       [esi+24h],eax
  00434b6a  e8d1251100            call      00547140h
  00434b6f  99                    cdq
  00434b70  b932000000            mov       ecx,32h
  00434b75  66c74632000c          mov       word [esi+32h],0C00h
  00434b7b  f7f9                  idiv      ecx
  00434b7d  b8ecffffff            mov       eax,0FFFFFFECh
  00434b82  66c746340008          mov       word [esi+34h],800h
  00434b88  66c7461238ff          mov       word [esi+12h],0FF38h
  00434b8e  2bc2                  sub       eax,edx
  00434b90  c1e00c                shl       eax,0Ch
  00434b93  894650                mov       [esi+50h],eax

LAB_00434b96:
  00434b96  66c746100100          mov       word [esi+10h],1

LAB_00434b9c:
  00434b9c  66837e1001            cmp       word [esi+10h],1
  00434ba1  7539                  jne       short 00434BDCh
  00434ba3  8b4e50                mov       ecx,[esi+50h]
  00434ba6  8b5e1c                mov       ebx,[esi+1Ch]
  00434ba9  668b4612              mov       ax,[esi+12h]
  00434bad  03d9                  add       ebx,ecx
  00434baf  663d38ff              cmp       ax,0FF38h
  00434bb3  895e1c                mov       [esi+1Ch],ebx
  00434bb6  8bcb                  mov       ecx,ebx
  00434bb8  750e                  jne       short 00434BC8h
  00434bba  81f90080f3ff          cmp       ecx,0FFF38000h
  00434bc0  7d06                  jge       short 00434BC8h
  00434bc2  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00434bc8:
  00434bc8  663d0807              cmp       ax,708h
  00434bcc  750e                  jne       short 00434BDCh
  00434bce  81f900807000          cmp       ecx,708000h
  00434bd4  7e06                  jle       short 00434BDCh
  00434bd6  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00434bdc:
  00434bdc  5e                    pop       esi
  00434bdd  5b                    pop       ebx
  00434bde  c3                    ret
  00434bdf  90                    nop

5 cible(s) de saut interne : 0x00434b42, 0x00434b96, 0x00434b9c, 0x00434bc8, 0x00434bdc
