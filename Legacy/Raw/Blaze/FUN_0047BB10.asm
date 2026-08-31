
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047bb10  section .text  offset fichier 0x7af10  60 octets

  0047bb10  8b442404              mov       eax,[esp+4]
  0047bb14  668b5010              mov       dx,[eax+10h]
  0047bb18  668bca                mov       cx,dx
  0047bb1b  66d1e9                shr       cx,1
  0047bb1e  6685d2                test      dx,dx
  0047bb21  7514                  jne       short 0047BB37h
  0047bb23  c7402800100010        mov       dword [eax+28h],10001000h
  0047bb2a  c7403800000001        mov       dword [eax+38h],1000000h
  0047bb31  66c7402eecff          mov       word [eax+2Eh],0FFECh

LAB_0047bb37:
  0047bb37  6683f904              cmp       cx,4
  0047bb3b  7206                  jb        short 0047BB43h
  0047bb3d  c70000000000          mov       dword [eax],0

LAB_0047bb43:
  0047bb43  42                    inc       edx
  0047bb44  884840                mov       [eax+40h],cl
  0047bb47  66895010              mov       [eax+10h],dx
  0047bb4b  c3                    ret

2 cible(s) de saut interne : 0x0047bb37, 0x0047bb43
