
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b050  section .text  offset fichier 0x7a450  91 octets

  0047b050  8b442404              mov       eax,[esp+4]
  0047b054  668b5010              mov       dx,[eax+10h]
  0047b058  6685d2                test      dx,dx
  0047b05b  7510                  jne       short 0047B06Dh
  0047b05d  c7402800180018        mov       dword [eax+28h],18001800h
  0047b064  c74038ffffff00        mov       dword [eax+38h],0FFFFFFh
  0047b06b  eb18                  jmp       short 0047B085h

LAB_0047b06d:
  0047b06d  8b4828                mov       ecx,[eax+28h]
  0047b070  81c100fffffe          add       ecx,0FEFFFF00h
  0047b076  894828                mov       [eax+28h],ecx
  0047b079  8b4838                mov       ecx,[eax+38h]
  0047b07c  81c1e8e7e7ff          add       ecx,0FFE7E7E8h
  0047b082  894838                mov       [eax+38h],ecx

LAB_0047b085:
  0047b085  8b4820                mov       ecx,[eax+20h]
  0047b088  81c100a00000          add       ecx,0A000h
  0047b08e  894820                mov       [eax+20h],ecx
  0047b091  668bca                mov       cx,dx
  0047b094  66d1f9                sar       cx,1
  0047b097  80f904                cmp       cl,4
  0047b09a  884840                mov       [eax+40h],cl
  0047b09d  7206                  jb        short 0047B0A5h
  0047b09f  c70000000000          mov       dword [eax],0

LAB_0047b0a5:
  0047b0a5  42                    inc       edx
  0047b0a6  66895010              mov       [eax+10h],dx
  0047b0aa  c3                    ret

3 cible(s) de saut interne : 0x0047b06d, 0x0047b085, 0x0047b0a5
