
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b340  section .text  offset fichier 0x7a740  88 octets

  0047b340  8b442404              mov       eax,[esp+4]
  0047b344  668b4810              mov       cx,[eax+10h]
  0047b348  6685c9                test      cx,cx
  0047b34b  7510                  jne       short 0047B35Dh
  0047b34d  c7402800100010        mov       dword [eax+28h],10001000h
  0047b354  c74038ffffff00        mov       dword [eax+38h],0FFFFFFh
  0047b35b  eb18                  jmp       short 0047B375h

LAB_0047b35d:
  0047b35d  8b5028                mov       edx,[eax+28h]
  0047b360  81c200fffffe          add       edx,0FEFFFF00h
  0047b366  895028                mov       [eax+28h],edx
  0047b369  8b5038                mov       edx,[eax+38h]
  0047b36c  81c2e0dfdfff          add       edx,0FFDFDFE0h
  0047b372  895038                mov       [eax+38h],edx

LAB_0047b375:
  0047b375  8a5040                mov       dl,[eax+40h]
  0047b378  fec2                  inc       dl
  0047b37a  80fa03                cmp       dl,3
  0047b37d  885040                mov       [eax+40h],dl
  0047b380  7204                  jb        short 0047B386h
  0047b382  c6404000              mov       byte [eax+40h],0

LAB_0047b386:
  0047b386  41                    inc       ecx
  0047b387  6683f906              cmp       cx,6
  0047b38b  66894810              mov       [eax+10h],cx
  0047b38f  7c06                  jl        short 0047B397h
  0047b391  c70000000000          mov       dword [eax],0

LAB_0047b397:
  0047b397  c3                    ret

4 cible(s) de saut interne : 0x0047b35d, 0x0047b375, 0x0047b386, 0x0047b397
