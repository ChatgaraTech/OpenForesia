Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004746c0  section .text  offset fichier 0x73ac0  45 octets

  004746c0  e87b2a0d00            call      00547140h
  004746c5  99                    cdq
  004746c6  33c2                  xor       eax,edx
  004746c8  2bc2                  sub       eax,edx
  004746ca  83e001                and       eax,1
  004746cd  33c2                  xor       eax,edx
  004746cf  2bc2                  sub       eax,edx
  004746d1  6685c0                test      ax,ax
  004746d4  7517                  jne       short 004746EDh
  004746d6  8b442404              mov       eax,[esp+4]
  004746da  6a00                  push      0
  004746dc  6a00                  push      0
  004746de  6aff                  push      0FFFFFFFFh
  004746e0  6a00                  push      0
  004746e2  6a3f                  push      3Fh
  004746e4  50                    push      eax
  004746e5  e8263affff            call      00468110h
  004746ea  83c418                add       esp,18h
