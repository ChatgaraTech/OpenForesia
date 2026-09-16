Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454610  section .text  offset fichier 0x53a10  496 octets

  00454610  51                    push      ecx
  00454611  53                    push      ebx
  00454612  55                    push      ebp
  00454613  56                    push      esi
  00454614  8b742414              mov       esi,[esp+14h]
  00454618  33db                  xor       ebx,ebx
  0045461a  57                    push      edi
  0045461b  66395e10              cmp       [esi+10h],bx
  0045461f  bf01000000            mov       edi,1
  00454624  750b                  jne       short 00454631h
  00454626  66897e10              mov       [esi+10h],di
  0045462a  66899e98000000        mov       [esi+98h],bx

LAB_00454631:
  00454631  66ff8698000000        inc       word [esi+98h]
  00454638  668b8698000000        mov       ax,[esi+98h]
  0045463f  663d1400              cmp       ax,14h
  00454643  7531                  jne       short 00454676h
  00454645  a1cc975e00            mov       eax,[5E97CCh]
  0045464a  6800080000            push      800h
  0045464f  6800080000            push      800h
  00454654  6800060000            push      600h
  00454659  8b88a8000000          mov       ecx,[eax+0A8h]
  0045465f  33c0                  xor       eax,eax
  00454661  8b11                  mov       edx,[ecx]
  00454663  8a421d                mov       al,[edx+1Dh]
  00454666  50                    push      eax
  00454667  e834880e00            call      0053CEA0h
  0045466c  83c410                add       esp,10h
  0045466f  66899e98000000        mov       [esi+98h],bx

LAB_00454676:
  00454676  8b4e24                mov       ecx,[esi+24h]
  00454679  668b5632              mov       dx,[esi+32h]
  0045467d  8b5e1c                mov       ebx,[esi+1Ch]
  00454680  8b6e20                mov       ebp,[esi+20h]
  00454683  6a00                  push      0
  00454685  6a00                  push      0
  00454687  6aff                  push      0FFFFFFFFh
  00454689  66897e14              mov       [esi+14h],di
  0045468d  6a00                  push      0
  0045468f  bf0080f3ff            mov       edi,0FFF38000h
  00454694  6a3c                  push      3Ch
  00454696  56                    push      esi
  00454697  894c2430              mov       [esp+30h],ecx
  0045469b  6689542428            mov       [esp+28h],dx
  004546a0  c7461c00701f00        mov       dword [esi+1Ch],1F7000h
  004546a7  897e20                mov       [esi+20h],edi
  004546aa  c746240010fdff        mov       dword [esi+24h],0FFFD1000h
  004546b1  66c74632440a          mov       word [esi+32h],0A44h
  004546b7  e8543a0100            call      00468110h
  004546bc  83c418                add       esp,18h
  004546bf  66c746324402          mov       word [esi+32h],244h
  004546c5  6a00                  push      0
  004546c7  6a00                  push      0
  004546c9  6aff                  push      0FFFFFFFFh
  004546cb  6a00                  push      0
  004546cd  6a3c                  push      3Ch
  004546cf  56                    push      esi
  004546d0  e83b3a0100            call      00468110h
  004546d5  83c418                add       esp,18h
  004546d8  c7461c00304400        mov       dword [esi+1Ch],443000h
  004546df  897e20                mov       [esi+20h],edi
  004546e2  c7462400e00a00        mov       dword [esi+24h],0AE000h
  004546e9  6a00                  push      0
  004546eb  6a00                  push      0
  004546ed  6aff                  push      0FFFFFFFFh
  004546ef  6a00                  push      0
  004546f1  6a3c                  push      3Ch
  004546f3  56                    push      esi
  004546f4  66c74632c804          mov       word [esi+32h],4C8h
  004546fa  e8113a0100            call      00468110h
  004546ff  83c418                add       esp,18h
  00454702  66c74632c80c          mov       word [esi+32h],0CC8h
  00454708  6a00                  push      0
  0045470a  6a00                  push      0
  0045470c  6aff                  push      0FFFFFFFFh
  0045470e  6a00                  push      0
  00454710  6a3c                  push      3Ch
  00454712  56                    push      esi
  00454713  e8f8390100            call      00468110h
  00454718  83c418                add       esp,18h
  0045471b  c7461c00706200        mov       dword [esi+1Ch],627000h
  00454722  897e20                mov       [esi+20h],edi
  00454725  c7462400a0e0ff        mov       dword [esi+24h],0FFE0A000h
  0045472c  6a00                  push      0
  0045472e  6a00                  push      0
  00454730  6aff                  push      0FFFFFFFFh
  00454732  6a00                  push      0
  00454734  6a3c                  push      3Ch
  00454736  56                    push      esi
  00454737  66c74632c808          mov       word [esi+32h],8C8h
  0045473d  e8ce390100            call      00468110h
  00454742  83c418                add       esp,18h
  00454745  66c74632c800          mov       word [esi+32h],0C8h
  0045474b  6a00                  push      0
  0045474d  6a00                  push      0
  0045474f  6aff                  push      0FFFFFFFFh
  00454751  6a00                  push      0
  00454753  6a3c                  push      3Ch
  00454755  56                    push      esi
  00454756  e8b5390100            call      00468110h
  0045475b  83c418                add       esp,18h
  0045475e  c7461c00a06300        mov       dword [esi+1Ch],63A000h
  00454765  897e20                mov       [esi+20h],edi
  00454768  c7462400a0baff        mov       dword [esi+24h],0FFBAA000h
  0045476f  6a00                  push      0
  00454771  6a00                  push      0
  00454773  6aff                  push      0FFFFFFFFh
  00454775  6a00                  push      0
  00454777  6a3c                  push      3Ch
  00454779  66c746320008          mov       word [esi+32h],800h
  0045477f  56                    push      esi
  00454780  e88b390100            call      00468110h
  00454785  83c418                add       esp,18h
  00454788  66c746320000          mov       word [esi+32h],0
  0045478e  6a00                  push      0
  00454790  6a00                  push      0
  00454792  6aff                  push      0FFFFFFFFh
  00454794  6a00                  push      0
  00454796  6a3c                  push      3Ch
  00454798  56                    push      esi
  00454799  e872390100            call      00468110h
  0045479e  83c418                add       esp,18h
  004547a1  c7461c00801900        mov       dword [esi+1Ch],198000h
  004547a8  897e20                mov       [esi+20h],edi
  004547ab  c7462400a0aeff        mov       dword [esi+24h],0FFAEA000h
  004547b2  6a00                  push      0
  004547b4  6a00                  push      0
  004547b6  6aff                  push      0FFFFFFFFh
  004547b8  6a00                  push      0
  004547ba  6a3c                  push      3Ch
  004547bc  56                    push      esi
  004547bd  66c746329c07          mov       word [esi+32h],79Ch
  004547c3  e848390100            call      00468110h
  004547c8  83c418                add       esp,18h
  004547cb  66c746329cff          mov       word [esi+32h],0FF9Ch
  004547d1  6a00                  push      0
  004547d3  6a00                  push      0
  004547d5  6aff                  push      0FFFFFFFFh
  004547d7  6a00                  push      0
  004547d9  6a3c                  push      3Ch
  004547db  56                    push      esi
  004547dc  e82f390100            call      00468110h
  004547e1  8b442430              mov       eax,[esp+30h]
  004547e5  668b4c2428            mov       cx,[esp+28h]
  004547ea  83c418                add       esp,18h
  004547ed  895e1c                mov       [esi+1Ch],ebx
  004547f0  896e20                mov       [esi+20h],ebp
  004547f3  894624                mov       [esi+24h],eax
  004547f6  66894e32              mov       [esi+32h],cx
  004547fa  5f                    pop       edi
  004547fb  5e                    pop       esi
  004547fc  5d                    pop       ebp
  004547fd  5b                    pop       ebx
  004547fe  59                    pop       ecx
  004547ff  c3                    ret

2 cible(s) de saut interne : 0x00454631, 0x00454676
