Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046abc0  section .text  offset fichier 0x69fc0  144 octets

  0046abc0  8b442404              mov       eax,[esp+4]
  0046abc4  33d2                  xor       edx,edx
  0046abc6  57                    push      edi
  0046abc7  66395010              cmp       [eax+10h],dx
  0046abcb  7534                  jne       short 0046AC01h
  0046abcd  8a4807                mov       cl,[eax+7]
  0046abd0  80e1ef                and       cl,0EFh
  0046abd3  80c920                or        cl,20h
  0046abd6  884807                mov       [eax+7],cl
  0046abd9  8b08                  mov       ecx,[eax]
  0046abdb  80cd40                or        ch,40h
  0046abde  66395014              cmp       [eax+14h],dx
  0046abe2  8908                  mov       [eax],ecx
  0046abe4  7406                  je        short 0046ABECh
  0046abe6  66c740106300          mov       word [eax+10h],63h

LAB_0046abec:
  0046abec  8b7820                mov       edi,[eax+20h]
  0046abef  c1ff0c                sar       edi,0Ch
  0046abf2  f6c110                test      cl,10h
  0046abf5  66897816              mov       [eax+16h],di
  0046abf9  7406                  je        short 0046AC01h
  0046abfb  66c740100100          mov       word [eax+10h],1

LAB_0046ac01:
  0046ac01  6683781001            cmp       word [eax+10h],1
  0046ac06  753d                  jne       short 0046AC45h
  0046ac08  8b7820                mov       edi,[eax+20h]
  0046ac0b  81c700a00000          add       edi,0A000h
  0046ac11  8bcf                  mov       ecx,edi
  0046ac13  897820                mov       [eax+20h],edi
  0046ac16  81f90040fcff          cmp       ecx,0FFFC4000h
  0046ac1c  7e27                  jle       short 0046AC45h
  0046ac1e  0fbf4816              movsx     ecx,word [eax+16h]
  0046ac22  c1e10c                shl       ecx,0Ch
  0046ac25  894820                mov       [eax+20h],ecx
  0046ac28  8a4807                mov       cl,[eax+7]
  0046ac2b  80e1df                and       cl,0DFh
  0046ac2e  66895010              mov       [eax+10h],dx
  0046ac32  80c910                or        cl,10h
  0046ac35  895050                mov       [eax+50h],edx
  0046ac38  884807                mov       [eax+7],cl
  0046ac3b  c7405414000000        mov       dword [eax+54h],14h
  0046ac42  895058                mov       [eax+58h],edx

LAB_0046ac45:
  0046ac45  5f                    pop       edi
  0046ac46  c3                    ret
  0046ac47  90                    nop
  0046ac48  90                    nop
  0046ac49  90                    nop
  0046ac4a  90                    nop
  0046ac4b  90                    nop
  0046ac4c  90                    nop
  0046ac4d  90                    nop
  0046ac4e  90                    nop
  0046ac4f  90                    nop

3 cible(s) de saut interne : 0x0046abec, 0x0046ac01, 0x0046ac45
