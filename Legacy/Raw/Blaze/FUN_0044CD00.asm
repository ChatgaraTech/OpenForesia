Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044cd00  section .text  offset fichier 0x4c100  475 octets

  0044cd00  53                    push      ebx
  0044cd01  56                    push      esi
  0044cd02  8b74240c              mov       esi,[esp+0Ch]
  0044cd06  57                    push      edi
  0044cd07  6a00                  push      0
  0044cd09  6a00                  push      0
  0044cd0b  56                    push      esi
  0044cd0c  e82fb3ffff            call      00448040h
  0044cd11  83c40c                add       esp,0Ch
  0044cd14  66837e1000            cmp       word [esi+10h],0
  0044cd19  750d                  jne       short 0044CD28h
  0044cd1b  6a00                  push      0
  0044cd1d  6a01                  push      1
  0044cd1f  56                    push      esi
  0044cd20  e88bafffff            call      00447CB0h
  0044cd25  83c40c                add       esp,0Ch

LAB_0044cd28:
  0044cd28  8b7e78                mov       edi,[esi+78h]
  0044cd2b  85ff                  test      edi,edi
  0044cd2d  7505                  jne       short 0044CD34h
  0044cd2f  bf58e55d00            mov       edi,5DE558h

LAB_0044cd34:
  0044cd34  8b471c                mov       eax,[edi+1Ch]
  0044cd37  8d5e1c                lea       ebx,[esi+1Ch]
  0044cd3a  66837e1600            cmp       word [esi+16h],0
  0044cd3f  8903                  mov       [ebx],eax
  0044cd41  8b4f20                mov       ecx,[edi+20h]
  0044cd44  894e20                mov       [esi+20h],ecx
  0044cd47  8b5724                mov       edx,[edi+24h]
  0044cd4a  895624                mov       [esi+24h],edx
  0044cd4d  7519                  jne       short 0044CD68h
  0044cd4f  6800100000            push      1000h
  0044cd54  6800100000            push      1000h
  0044cd59  6800020000            push      200h
  0044cd5e  6a27                  push      27h
  0044cd60  e83b010f00            call      0053CEA0h
  0044cd65  83c410                add       esp,10h

LAB_0044cd68:
  0044cd68  66837e160a            cmp       word [esi+16h],0Ah
  0044cd6d  7519                  jne       short 0044CD88h
  0044cd6f  6800100000            push      1000h
  0044cd74  6800100000            push      1000h
  0044cd79  68aa020000            push      2AAh
  0044cd7e  6a27                  push      27h
  0044cd80  e81b010f00            call      0053CEA0h
  0044cd85  83c410                add       esp,10h

LAB_0044cd88:
  0044cd88  66837e1628            cmp       word [esi+16h],28h
  0044cd8d  7519                  jne       short 0044CDA8h
  0044cd8f  6800100000            push      1000h
  0044cd94  6800100000            push      1000h
  0044cd99  68d3030000            push      3D3h
  0044cd9e  6a27                  push      27h
  0044cda0  e8fb000f00            call      0053CEA0h
  0044cda5  83c410                add       esp,10h

LAB_0044cda8:
  0044cda8  66837e1632            cmp       word [esi+16h],32h
  0044cdad  7519                  jne       short 0044CDC8h
  0044cdaf  6800100000            push      1000h
  0044cdb4  6800100000            push      1000h
  0044cdb9  6829030000            push      329h
  0044cdbe  6a27                  push      27h
  0044cdc0  e8db000f00            call      0053CEA0h
  0044cdc5  83c410                add       esp,10h

LAB_0044cdc8:
  0044cdc8  66817e168000          cmp       word [esi+16h],80h
  0044cdce  7519                  jne       short 0044CDE9h
  0044cdd0  6800100000            push      1000h
  0044cdd5  6800100000            push      1000h
  0044cdda  6800020000            push      200h
  0044cddf  6a2d                  push      2Dh
  0044cde1  e8ba000f00            call      0053CEA0h
  0044cde6  83c410                add       esp,10h

LAB_0044cde9:
  0044cde9  66817e168100          cmp       word [esi+16h],81h
  0044cdef  7519                  jne       short 0044CE0Ah
  0044cdf1  6800100000            push      1000h
  0044cdf6  6800100000            push      1000h
  0044cdfb  6829030000            push      329h
  0044ce00  6a30                  push      30h
  0044ce02  e899000f00            call      0053CEA0h
  0044ce07  83c410                add       esp,10h

LAB_0044ce0a:
  0044ce0a  66837e1000            cmp       word [esi+10h],0
  0044ce0f  752e                  jne       short 0044CE3Fh
  0044ce11  6aff                  push      0FFFFFFFFh
  0044ce13  6a00                  push      0
  0044ce15  6898000000            push      98h
  0044ce1a  56                    push      esi
  0044ce1b  66c746100100          mov       word [esi+10h],1
  0044ce21  66c746160000          mov       word [esi+16h],0
  0044ce27  e8b4dc0200            call      0047AAE0h
  0044ce2c  8b4674                mov       eax,[esi+74h]
  0044ce2f  83c410                add       esp,10h
  0044ce32  8b08                  mov       ecx,[eax]
  0044ce34  80c980                or        cl,80h
  0044ce37  8908                  mov       [eax],ecx
  0044ce39  8b07                  mov       eax,[edi]
  0044ce3b  0c80                  or        al,80h
  0044ce3d  8907                  mov       [edi],eax

LAB_0044ce3f:
  0044ce3f  66837e1001            cmp       word [esi+10h],1
  0044ce44  0f858d000000          jne       near 0044CED7h
  0044ce4a  66837e1614            cmp       word [esi+16h],14h
  0044ce4f  7539                  jne       short 0044CE8Ah
  0044ce51  6aff                  push      0FFFFFFFFh
  0044ce53  6a00                  push      0
  0044ce55  6899000000            push      99h
  0044ce5a  56                    push      esi
  0044ce5b  e880dc0200            call      0047AAE0h
  0044ce60  83c410                add       esp,10h
  0044ce63  6aff                  push      0FFFFFFFFh
  0044ce65  6a00                  push      0
  0044ce67  6899000000            push      99h
  0044ce6c  56                    push      esi
  0044ce6d  e86edc0200            call      0047AAE0h
  0044ce72  83c410                add       esp,10h
  0044ce75  6aff                  push      0FFFFFFFFh
  0044ce77  6a00                  push      0
  0044ce79  6893000000            push      93h
  0044ce7e  6a00                  push      0
  0044ce80  53                    push      ebx
  0044ce81  56                    push      esi
  0044ce82  e8b9dd0200            call      0047AC40h
  0044ce87  83c418                add       esp,18h

LAB_0044ce8a:
  0044ce8a  66ff4616              inc       word [esi+16h]
  0044ce8e  66817e16b400          cmp       word [esi+16h],0B4h
  0044ce94  7541                  jne       short 0044CED7h
  0044ce96  8b4674                mov       eax,[esi+74h]
  0044ce99  8a4e06                mov       cl,[esi+6]
  0044ce9c  6a00                  push      0
  0044ce9e  884806                mov       [eax+6],cl
  0044cea1  8b5674                mov       edx,[esi+74h]
  0044cea4  8a4607                mov       al,[esi+7]
  0044cea7  884207                mov       [edx+7],al
  0044ceaa  8b4e78                mov       ecx,[esi+78h]
  0044cead  8b5674                mov       edx,[esi+74h]
  0044ceb0  51                    push      ecx
  0044ceb1  52                    push      edx
  0044ceb2  e829000000            call      0044CEE0h
  0044ceb7  83c40c                add       esp,0Ch
  0044ceba  6a00                  push      0
  0044cebc  6a00                  push      0
  0044cebe  56                    push      esi
  0044cebf  e85cb1ffff            call      00448020h
  0044cec4  8b7674                mov       esi,[esi+74h]
  0044cec7  83c40c                add       esp,0Ch
  0044ceca  8b0e                  mov       ecx,[esi]
  0044cecc  80e17f                and       cl,7Fh
  0044cecf  890e                  mov       [esi],ecx
  0044ced1  8b07                  mov       eax,[edi]
  0044ced3  247f                  and       al,7Fh
  0044ced5  8907                  mov       [edi],eax

LAB_0044ced7:
  0044ced7  5f                    pop       edi
  0044ced8  5e                    pop       esi
  0044ced9  5b                    pop       ebx
  0044ceda  c3                    ret

11 cible(s) de saut interne : 0x0044cd28, 0x0044cd34, 0x0044cd68, 0x0044cd88, 0x0044cda8, 0x0044cdc8, 0x0044cde9, 0x0044ce0a, 0x0044ce3f, 0x0044ce8a, 0x0044ced7
