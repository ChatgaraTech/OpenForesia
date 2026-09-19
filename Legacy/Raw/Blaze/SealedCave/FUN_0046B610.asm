Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046b610  section .text  offset fichier 0x6aa10  208 octets

  0046b610  8b442404              mov       eax,[esp+4]
  0046b614  55                    push      ebp
  0046b615  56                    push      esi
  0046b616  6683781000            cmp       word [eax+10h],0
  0046b61b  7515                  jne       short 0046B632h
  0046b61d  668b4832              mov       cx,[eax+32h]
  0046b621  66c740100100          mov       word [eax+10h],1
  0046b627  66894812              mov       [eax+12h],cx
  0046b62b  c7405c00000000        mov       dword [eax+5Ch],0

LAB_0046b632:
  0046b632  6683781001            cmp       word [eax+10h],1
  0046b637  ba02000000            mov       edx,2
  0046b63c  7513                  jne       short 0046B651h
  0046b63e  8b485c                mov       ecx,[eax+5Ch]
  0046b641  0fbf7016              movsx     esi,word [eax+16h]
  0046b645  41                    inc       ecx
  0046b646  3bce                  cmp       ecx,esi
  0046b648  89485c                mov       [eax+5Ch],ecx
  0046b64b  7c04                  jl        short 0046B651h
  0046b64d  66895010              mov       [eax+10h],dx

LAB_0046b651:
  0046b651  66395010              cmp       [eax+10h],dx
  0046b655  be03000000            mov       esi,3
  0046b65a  7535                  jne       short 0046B691h
  0046b65c  668b5014              mov       dx,[eax+14h]
  0046b660  668b4812              mov       cx,[eax+12h]
  0046b664  66015032              add       [eax+32h],dx
  0046b668  668b5032              mov       dx,[eax+32h]
  0046b66c  0fbfe9                movsx     ebp,cx
  0046b66f  0fbfd2                movsx     edx,dx
  0046b672  81c500080000          add       ebp,800h
  0046b678  3bd5                  cmp       edx,ebp
  0046b67a  7c15                  jl        short 0046B691h
  0046b67c  81c100080000          add       ecx,800h
  0046b682  66897010              mov       [eax+10h],si
  0046b686  66894832              mov       [eax+32h],cx
  0046b68a  c7405c00000000        mov       dword [eax+5Ch],0

LAB_0046b691:
  0046b691  66397010              cmp       [eax+10h],si
  0046b695  ba04000000            mov       edx,4
  0046b69a  7513                  jne       short 0046B6AFh
  0046b69c  8b485c                mov       ecx,[eax+5Ch]
  0046b69f  0fbf7016              movsx     esi,word [eax+16h]
  0046b6a3  41                    inc       ecx
  0046b6a4  3bce                  cmp       ecx,esi
  0046b6a6  89485c                mov       [eax+5Ch],ecx
  0046b6a9  7c04                  jl        short 0046B6AFh
  0046b6ab  66895010              mov       [eax+10h],dx

LAB_0046b6af:
  0046b6af  66395010              cmp       [eax+10h],dx
  0046b6b3  7526                  jne       short 0046B6DBh
  0046b6b5  668b4814              mov       cx,[eax+14h]
  0046b6b9  66294832              sub       [eax+32h],cx
  0046b6bd  668b5032              mov       dx,[eax+32h]
  0046b6c1  668b4812              mov       cx,[eax+12h]
  0046b6c5  663bd1                cmp       dx,cx
  0046b6c8  7f11                  jg        short 0046B6DBh
  0046b6ca  66894832              mov       [eax+32h],cx
  0046b6ce  66c740100100          mov       word [eax+10h],1
  0046b6d4  c7405c00000000        mov       dword [eax+5Ch],0

LAB_0046b6db:
  0046b6db  5e                    pop       esi
  0046b6dc  5d                    pop       ebp
  0046b6dd  c3                    ret
  0046b6de  90                    nop
  0046b6df  90                    nop

5 cible(s) de saut interne : 0x0046b632, 0x0046b651, 0x0046b691, 0x0046b6af, 0x0046b6db
