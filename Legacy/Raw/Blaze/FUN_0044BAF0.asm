Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044baf0  section .text  offset fichier 0x4aef0  391 octets

  0044baf0  53                    push      ebx
  0044baf1  56                    push      esi
  0044baf2  8b74240c              mov       esi,[esp+0Ch]
  0044baf6  33db                  xor       ebx,ebx
  0044baf8  57                    push      edi
  0044baf9  66395e10              cmp       [esi+10h],bx
  0044bafd  750b                  jne       short 0044BB0Ah
  0044baff  53                    push      ebx
  0044bb00  53                    push      ebx
  0044bb01  56                    push      esi
  0044bb02  e839c5ffff            call      00448040h
  0044bb07  83c40c                add       esp,0Ch

LAB_0044bb0a:
  0044bb0a  53                    push      ebx
  0044bb0b  53                    push      ebx
  0044bb0c  56                    push      esi
  0044bb0d  e89ec1ffff            call      00447CB0h
  0044bb12  8b4678                mov       eax,[esi+78h]
  0044bb15  83c40c                add       esp,0Ch
  0044bb18  3bc3                  cmp       eax,ebx
  0044bb1a  7505                  jne       short 0044BB21h
  0044bb1c  b858e55d00            mov       eax,5DE558h

LAB_0044bb21:
  0044bb21  8b481c                mov       ecx,[eax+1Ch]
  0044bb24  8d7e1c                lea       edi,[esi+1Ch]
  0044bb27  66395e10              cmp       [esi+10h],bx
  0044bb2b  890f                  mov       [edi],ecx
  0044bb2d  8b5020                mov       edx,[eax+20h]
  0044bb30  895620                mov       [esi+20h],edx
  0044bb33  8b4024                mov       eax,[eax+24h]
  0044bb36  894624                mov       [esi+24h],eax
  0044bb39  750e                  jne       short 0044BB49h
  0044bb3b  66c746100100          mov       word [esi+10h],1
  0044bb41  66895e16              mov       [esi+16h],bx
  0044bb45  66895e14              mov       [esi+14h],bx

LAB_0044bb49:
  0044bb49  66837e1403            cmp       word [esi+14h],3
  0044bb4e  7c3a                  jl        short 0044BB8Ah
  0044bb50  6aff                  push      0FFFFFFFFh
  0044bb52  53                    push      ebx
  0044bb53  6890000000            push      90h
  0044bb58  53                    push      ebx
  0044bb59  57                    push      edi
  0044bb5a  56                    push      esi
  0044bb5b  66895e14              mov       [esi+14h],bx
  0044bb5f  66899e9a000000        mov       [esi+9Ah],bx
  0044bb66  e8d5f00200            call      0047AC40h
  0044bb6b  83c418                add       esp,18h
  0044bb6e  66c7869a0000000008    mov       word [esi+9Ah],800h
  0044bb77  6aff                  push      0FFFFFFFFh
  0044bb79  53                    push      ebx
  0044bb7a  6890000000            push      90h
  0044bb7f  53                    push      ebx
  0044bb80  57                    push      edi
  0044bb81  56                    push      esi
  0044bb82  e8b9f00200            call      0047AC40h
  0044bb87  83c418                add       esp,18h

LAB_0044bb8a:
  0044bb8a  668b4616              mov       ax,[esi+16h]
  0044bb8e  663bc3                cmp       ax,bx
  0044bb91  7406                  je        short 0044BB99h
  0044bb93  663d0400              cmp       ax,4
  0044bb97  751e                  jne       short 0044BBB7h

LAB_0044bb99:
  0044bb99  6aff                  push      0FFFFFFFFh
  0044bb9b  53                    push      ebx
  0044bb9c  b8ac0d0000            mov       eax,0DACh
  0044bba1  6892000000            push      92h
  0044bba6  56                    push      esi
  0044bba7  8907                  mov       [edi],eax
  0044bba9  894620                mov       [esi+20h],eax
  0044bbac  894624                mov       [esi+24h],eax
  0044bbaf  e82cef0200            call      0047AAE0h
  0044bbb4  83c410                add       esp,10h

LAB_0044bbb7:
  0044bbb7  66395e16              cmp       [esi+16h],bx
  0044bbbb  7519                  jne       short 0044BBD6h
  0044bbbd  6800100000            push      1000h
  0044bbc2  6800100000            push      1000h
  0044bbc7  68a60c0000            push      0CA6h
  0044bbcc  6a2f                  push      2Fh
  0044bbce  e8cd120f00            call      0053CEA0h
  0044bbd3  83c410                add       esp,10h

LAB_0044bbd6:
  0044bbd6  66837e1601            cmp       word [esi+16h],1
  0044bbdb  7519                  jne       short 0044BBF6h
  0044bbdd  6800100000            push      1000h
  0044bbe2  6800100000            push      1000h
  0044bbe7  68500d0000            push      0D50h
  0044bbec  6a2f                  push      2Fh
  0044bbee  e8ad120f00            call      0053CEA0h
  0044bbf3  83c410                add       esp,10h

LAB_0044bbf6:
  0044bbf6  66837e1602            cmp       word [esi+16h],2
  0044bbfb  7519                  jne       short 0044BC16h
  0044bbfd  6800100000            push      1000h
  0044bc02  6800100000            push      1000h
  0044bc07  6894010000            push      194h
  0044bc0c  6a31                  push      31h
  0044bc0e  e88d120f00            call      0053CEA0h
  0044bc13  83c410                add       esp,10h

LAB_0044bc16:
  0044bc16  66837e1603            cmp       word [esi+16h],3
  0044bc1b  7519                  jne       short 0044BC36h
  0044bc1d  6800100000            push      1000h
  0044bc22  6800100000            push      1000h
  0044bc27  68ca000000            push      0CAh
  0044bc2c  6a16                  push      16h
  0044bc2e  e86d120f00            call      0053CEA0h
  0044bc33  83c410                add       esp,10h

LAB_0044bc36:
  0044bc36  66ff4614              inc       word [esi+14h]
  0044bc3a  66ff4616              inc       word [esi+16h]
  0044bc3e  66837e161c            cmp       word [esi+16h],1Ch
  0044bc43  752e                  jne       short 0044BC73h
  0044bc45  8b4e74                mov       ecx,[esi+74h]
  0044bc48  8a5606                mov       dl,[esi+6]
  0044bc4b  53                    push      ebx
  0044bc4c  885106                mov       [ecx+6],dl
  0044bc4f  8b4674                mov       eax,[esi+74h]
  0044bc52  8a4e07                mov       cl,[esi+7]
  0044bc55  884807                mov       [eax+7],cl
  0044bc58  8b5678                mov       edx,[esi+78h]
  0044bc5b  8b4674                mov       eax,[esi+74h]
  0044bc5e  52                    push      edx
  0044bc5f  50                    push      eax
  0044bc60  e81b000000            call      0044BC80h
  0044bc65  83c40c                add       esp,0Ch
  0044bc68  53                    push      ebx
  0044bc69  53                    push      ebx
  0044bc6a  56                    push      esi
  0044bc6b  e8b0c3ffff            call      00448020h
  0044bc70  83c40c                add       esp,0Ch

LAB_0044bc73:
  0044bc73  5f                    pop       edi
  0044bc74  5e                    pop       esi
  0044bc75  5b                    pop       ebx
  0044bc76  c3                    ret

11 cible(s) de saut interne : 0x0044bb0a, 0x0044bb21, 0x0044bb49, 0x0044bb8a, 0x0044bb99, 0x0044bbb7, 0x0044bbd6, 0x0044bbf6, 0x0044bc16, 0x0044bc36, 0x0044bc73
