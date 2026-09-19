Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043c010  section .text  offset fichier 0x3b410  272 octets

  0043c010  56                    push      esi
  0043c011  8b742408              mov       esi,[esp+8]
  0043c015  b901000000            mov       ecx,1
  0043c01a  66837e1000            cmp       word [esi+10h],0
  0043c01f  7554                  jne       short 0043C075h
  0043c021  b0c8                  mov       al,0C8h
  0043c023  8b561c                mov       edx,[esi+1Ch]
  0043c026  884638                mov       [esi+38h],al
  0043c029  884639                mov       [esi+39h],al
  0043c02c  88463a                mov       [esi+3Ah],al
  0043c02f  8b4620                mov       eax,[esi+20h]
  0043c032  0500000a00            add       eax,0A0000h
  0043c037  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0043c03d  894620                mov       [esi+20h],eax
  0043c040  894654                mov       [esi+54h],eax
  0043c043  8b4624                mov       eax,[esi+24h]
  0043c046  c7463c58115500        mov       dword [esi+3Ch],551158h
  0043c04d  894658                mov       [esi+58h],eax
  0043c050  b8b80b0000            mov       eax,0BB8h
  0043c055  c6460a02              mov       byte [esi+0Ah],2
  0043c059  c6460b00              mov       byte [esi+0Bh],0
  0043c05d  895650                mov       [esi+50h],edx
  0043c060  66c7869a0000000000    mov       word [esi+9Ah],0
  0043c069  66894e10              mov       [esi+10h],cx
  0043c06d  66894628              mov       [esi+28h],ax
  0043c071  6689462a              mov       [esi+2Ah],ax

LAB_0043c075:
  0043c075  66394e10              cmp       [esi+10h],cx
  0043c079  0f8598000000          jne       near 0043C117h
  0043c07f  0fbf4e32              movsx     ecx,word [esi+32h]
  0043c083  81c100080000          add       ecx,800h
  0043c089  51                    push      ecx
  0043c08a  e88d991000            call      00545A1Ch
  0043c08f  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0043c096  0fafc2                imul      eax,edx
  0043c099  034650                add       eax,[esi+50h]
  0043c09c  89461c                mov       [esi+1Ch],eax
  0043c09f  0fbf4632              movsx     eax,word [esi+32h]
  0043c0a3  0500080000            add       eax,800h
  0043c0a8  50                    push      eax
  0043c0a9  e868991000            call      00545A16h
  0043c0ae  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0043c0b5  0fafc1                imul      eax,ecx
  0043c0b8  8b5658                mov       edx,[esi+58h]
  0043c0bb  8b4e5c                mov       ecx,[esi+5Ch]
  0043c0be  03c2                  add       eax,edx
  0043c0c0  8b5620                mov       edx,[esi+20h]
  0043c0c3  c1e10c                shl       ecx,0Ch
  0043c0c6  6a00                  push      0
  0043c0c8  6a00                  push      0
  0043c0ca  03d1                  add       edx,ecx
  0043c0cc  6a00                  push      0
  0043c0ce  6a02                  push      2
  0043c0d0  895620                mov       [esi+20h],edx
  0043c0d3  8bca                  mov       ecx,edx
  0043c0d5  8b561c                mov       edx,[esi+1Ch]
  0043c0d8  6a50                  push      50h
  0043c0da  50                    push      eax
  0043c0db  51                    push      ecx
  0043c0dc  52                    push      edx
  0043c0dd  894624                mov       [esi+24h],eax
  0043c0e0  e86bcefdff            call      00418F50h
  0043c0e5  6683869a00000023      add       word [esi+9Ah],23h
  0043c0ed  83c420                add       esp,20h
  0043c0f0  66ff4e14              dec       word [esi+14h]
  0043c0f4  66837e1400            cmp       word [esi+14h],0
  0043c0f9  7d1c                  jge       short 0043C117h
  0043c0fb  6aec                  push      0FFFFFFECh
  0043c0fd  6aec                  push      0FFFFFFECh
  0043c0ff  6aec                  push      0FFFFFFECh
  0043c101  56                    push      esi
  0043c102  e8d98cfcff            call      00404DE0h
  0043c107  8a4638                mov       al,[esi+38h]
  0043c10a  83c410                add       esp,10h
  0043c10d  84c0                  test      al,al
  0043c10f  7506                  jne       short 0043C117h
  0043c111  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043c117:
  0043c117  5e                    pop       esi
  0043c118  c3                    ret
  0043c119  90                    nop
  0043c11a  90                    nop
  0043c11b  90                    nop
  0043c11c  90                    nop
  0043c11d  90                    nop
  0043c11e  90                    nop
  0043c11f  90                    nop

2 cible(s) de saut interne : 0x0043c075, 0x0043c117
