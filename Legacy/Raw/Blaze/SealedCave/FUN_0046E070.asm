Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e070  section .text  offset fichier 0x6d470  144 octets

  0046e070  8b4c2404              mov       ecx,[esp+4]
  0046e074  668b4114              mov       ax,[ecx+14h]
  0046e078  6685c0                test      ax,ax
  0046e07b  7517                  jne       short 0046E094h
  0046e07d  8a1501975e00          mov       dl,[5E9701h]
  0046e083  f6c202                test      dl,2
  0046e086  740c                  je        short 0046E094h
  0046e088  f6c204                test      dl,4
  0046e08b  7407                  je        short 0046E094h
  0046e08d  8b11                  mov       edx,[ecx]
  0046e08f  80ce20                or        dh,20h
  0046e092  8911                  mov       [ecx],edx

LAB_0046e094:
  0046e094  663d0100              cmp       ax,1
  0046e098  755f                  jne       short 0046E0F9h
  0046e09a  803d63905e0001        cmp       byte [5E9063h],1
  0046e0a1  7556                  jne       short 0046E0F9h
  0046e0a3  a1cc975e00            mov       eax,[5E97CCh]
  0046e0a8  6a01                  push      1
  0046e0aa  68c0935e00            push      5E93C0h
  0046e0af  50                    push      eax
  0046e0b0  e81b5d0a00            call      00513DD0h
  0046e0b5  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046e0bb  83c40c                add       esp,0Ch
  0046e0be  6a01                  push      1
  0046e0c0  685c945e00            push      5E945Ch
  0046e0c5  51                    push      ecx
  0046e0c6  e8055d0a00            call      00513DD0h
  0046e0cb  8b15cc975e00          mov       edx,[5E97CCh]
  0046e0d1  83c40c                add       esp,0Ch
  0046e0d4  6a01                  push      1
  0046e0d6  68f8945e00            push      5E94F8h
  0046e0db  52                    push      edx
  0046e0dc  e8ef5c0a00            call      00513DD0h
  0046e0e1  a1cc975e00            mov       eax,[5E97CCh]
  0046e0e6  83c40c                add       esp,0Ch
  0046e0e9  6a01                  push      1
  0046e0eb  6894955e00            push      5E9594h
  0046e0f0  50                    push      eax
  0046e0f1  e8da5c0a00            call      00513DD0h
  0046e0f6  83c40c                add       esp,0Ch

LAB_0046e0f9:
  0046e0f9  c3                    ret
  0046e0fa  90                    nop
  0046e0fb  90                    nop
  0046e0fc  90                    nop
  0046e0fd  90                    nop
  0046e0fe  90                    nop
  0046e0ff  90                    nop

2 cible(s) de saut interne : 0x0046e094, 0x0046e0f9
