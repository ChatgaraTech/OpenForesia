Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00418370  section .text  offset fichier 0x17770  96 octets

  00418370  8b442404              mov       eax,[esp+4]
  00418374  50                    push      eax
  00418375  c7405c3b000000        mov       dword [eax+5Ch],3Bh
  0041837c  8b0dfca55f00          mov       ecx,[5FA5FCh]
  00418382  89481c                mov       [eax+1Ch],ecx
  00418385  8b1500a65f00          mov       edx,[5FA600h]
  0041838b  895020                mov       [eax+20h],edx
  0041838e  8b0d04a65f00          mov       ecx,[5FA604h]
  00418394  81c100400600          add       ecx,64000h
  0041839a  894824                mov       [eax+24h],ecx
  0041839d  668b1510a65f00        mov       dx,[5FA610h]
  004183a4  66895030              mov       [eax+30h],dx
  004183a8  668b0d12a65f00        mov       cx,[5FA612h]
  004183af  66894832              mov       [eax+32h],cx
  004183b3  668b1514a65f00        mov       dx,[5FA614h]
  004183ba  66895034              mov       [eax+34h],dx
  004183be  e80d000000            call      004183D0h
  004183c3  83c404                add       esp,4
  004183c6  c3                    ret
  004183c7  90                    nop
  004183c8  90                    nop
  004183c9  90                    nop
  004183ca  90                    nop
  004183cb  90                    nop
  004183cc  90                    nop
  004183cd  90                    nop
  004183ce  90                    nop
  004183cf  90                    nop
