
Legacy/Binaries/wsyskern.dll  base 0x10000000
0x100044a0  section .text  offset fichier 0x38a0  112 octets

  100044a0  8b442404              mov       eax,[esp+4]
  100044a4  8b510c                mov       edx,[ecx+0Ch]
  100044a7  83ec20                sub       esp,20h
  100044aa  3bc2                  cmp       eax,edx
  100044ac  7208                  jb        short 100044B6h
  100044ae  33c0                  xor       eax,eax
  100044b0  83c420                add       esp,20h
  100044b3  c20c00                ret       0Ch

LAB_100044b6:
  100044b6  57                    push      edi
  100044b7  56                    push      esi
  100044b8  8b742430              mov       esi,[esp+30h]
  100044bc  c60600                mov       byte [esi],0
  100044bf  8b4910                mov       ecx,[ecx+10h]
  100044c2  8b1481                mov       edx,[ecx+eax*4]
  100044c5  8d442408              lea       eax,[esp+8]
  100044c9  52                    push      edx
  100044ca  68e4990410            push      100499E4h
  100044cf  50                    push      eax
  100044d0  ff15a4940410          call      dword [100494A4h]
  100044d6  8b7c2440              mov       edi,[esp+40h]
  100044da  83c40c                add       esp,0Ch
  100044dd  8d542408              lea       edx,[esp+8]
  100044e1  8d4fff                lea       ecx,[edi-1]
  100044e4  51                    push      ecx
  100044e5  52                    push      edx
  100044e6  56                    push      esi
  100044e7  e8d4e50000            call      10012AC0h
  100044ec  83c40c                add       esp,0Ch
  100044ef  c6443eff00            mov       byte [esi+edi-1],0
  100044f4  b801000000            mov       eax,1
  100044f9  5e                    pop       esi
  100044fa  5f                    pop       edi
  100044fb  83c420                add       esp,20h
  100044fe  c20c00                ret       0Ch
  10004501  90                    nop
  10004502  90                    nop
  10004503  90                    nop
  10004504  90                    nop
  10004505  90                    nop
  10004506  90                    nop
  10004507  90                    nop
  10004508  90                    nop
  10004509  90                    nop
  1000450a  90                    nop
  1000450b  90                    nop
  1000450c  90                    nop
  1000450d  90                    nop
  1000450e  90                    nop
  1000450f  90                    nop

1 cible(s) de saut interne : 0x100044b6
