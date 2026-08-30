Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040edf0  section .text  offset fichier 0xe1f0  327 octets

  0040edf0  53                    push      ebx
  0040edf1  56                    push      esi
  0040edf2  8b74240c              mov       esi,[esp+0Ch]
  0040edf6  bb01000000            mov       ebx,1
  0040edfb  66837e1000            cmp       word [esi+10h],0
  0040ee00  7522                  jne       short 0040EE24h
  0040ee02  6aff                  push      0FFFFFFFFh
  0040ee04  6a00                  push      0
  0040ee06  68fa000000            push      0FAh
  0040ee0b  8d461c                lea       eax,[esi+1Ch]
  0040ee0e  6a00                  push      0
  0040ee10  50                    push      eax
  0040ee11  56                    push      esi
  0040ee12  66895e10              mov       [esi+10h],bx
  0040ee16  66c746160000          mov       word [esi+16h],0
  0040ee1c  e81fbe0600            call      0047AC40h
  0040ee21  83c418                add       esp,18h

LAB_0040ee24:
  0040ee24  66ff4616              inc       word [esi+16h]
  0040ee28  668b4616              mov       ax,[esi+16h]
  0040ee2c  663daa00              cmp       ax,0AAh
  0040ee30  7c6a                  jl        short 0040EE9Ch
  0040ee32  663dc200              cmp       ax,0C2h
  0040ee36  7f64                  jg        short 0040EE9Ch
  0040ee38  8a1528e05d00          mov       dl,[5DE028h]
  0040ee3e  8a0d29e05d00          mov       cl,[5DE029h]
  0040ee44  b00a                  mov       al,0Ah
  0040ee46  881d2be05d00          mov       [5DE02Bh],bl
  0040ee4c  02d0                  add       dl,al
  0040ee4e  02c8                  add       cl,al
  0040ee50  881528e05d00          mov       [5DE028h],dl
  0040ee56  8a152ae05d00          mov       dl,[5DE02Ah]
  0040ee5c  880d29e05d00          mov       [5DE029h],cl
  0040ee62  8a0d24e05d00          mov       cl,[5DE024h]
  0040ee68  02d0                  add       dl,al
  0040ee6a  b003                  mov       al,3
  0040ee6c  02c8                  add       cl,al
  0040ee6e  88152ae05d00          mov       [5DE02Ah],dl
  0040ee74  8a1525e05d00          mov       dl,[5DE025h]
  0040ee7a  880d24e05d00          mov       [5DE024h],cl
  0040ee80  8a0d26e05d00          mov       cl,[5DE026h]
  0040ee86  02d0                  add       dl,al
  0040ee88  02c8                  add       cl,al
  0040ee8a  881d27e05d00          mov       [5DE027h],bl
  0040ee90  881525e05d00          mov       [5DE025h],dl
  0040ee96  880d26e05d00          mov       [5DE026h],cl

LAB_0040ee9c:
  0040ee9c  668b4616              mov       ax,[esi+16h]
  0040eea0  663dc300              cmp       ax,0C3h
  0040eea4  7c6a                  jl        short 0040EF10h
  0040eea6  663ddb00              cmp       ax,0DBh
  0040eeaa  7f64                  jg        short 0040EF10h
  0040eeac  8a0d28e05d00          mov       cl,[5DE028h]
  0040eeb2  8a1529e05d00          mov       dl,[5DE029h]
  0040eeb8  b0f6                  mov       al,0F6h
  0040eeba  881d2be05d00          mov       [5DE02Bh],bl
  0040eec0  02c8                  add       cl,al
  0040eec2  02d0                  add       dl,al
  0040eec4  880d28e05d00          mov       [5DE028h],cl
  0040eeca  8a0d2ae05d00          mov       cl,[5DE02Ah]
  0040eed0  02c8                  add       cl,al
  0040eed2  881529e05d00          mov       [5DE029h],dl
  0040eed8  8a1525e05d00          mov       dl,[5DE025h]
  0040eede  881d27e05d00          mov       [5DE027h],bl
  0040eee4  8a1d24e05d00          mov       bl,[5DE024h]
  0040eeea  880d2ae05d00          mov       [5DE02Ah],cl
  0040eef0  8a0d26e05d00          mov       cl,[5DE026h]
  0040eef6  b0fd                  mov       al,0FDh
  0040eef8  02d8                  add       bl,al
  0040eefa  02d0                  add       dl,al
  0040eefc  02c8                  add       cl,al
  0040eefe  881d24e05d00          mov       [5DE024h],bl
  0040ef04  881525e05d00          mov       [5DE025h],dl
  0040ef0a  880d26e05d00          mov       [5DE026h],cl

LAB_0040ef10:
  0040ef10  66817e16dc00          cmp       word [esi+16h],0DCh
  0040ef16  751c                  jne       short 0040EF34h
  0040ef18  8b16                  mov       edx,[esi]
  0040ef1a  8b466c                mov       eax,[esi+6Ch]
  0040ef1d  81e2ffffff7f          and       edx,7FFFFFFFh
  0040ef23  85c0                  test      eax,eax
  0040ef25  8916                  mov       [esi],edx
  0040ef27  740b                  je        short 0040EF34h
  0040ef29  668b8e98000000        mov       cx,[esi+98h]
  0040ef30  66894810              mov       [eax+10h],cx

LAB_0040ef34:
  0040ef34  5e                    pop       esi
  0040ef35  5b                    pop       ebx
  0040ef36  c3                    ret

4 cible(s) de saut interne : 0x0040ee24, 0x0040ee9c, 0x0040ef10, 0x0040ef34
