Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470640  section .text  offset fichier 0x6fa40  272 octets

  00470640  56                    push      esi
  00470641  8b742408              mov       esi,[esp+8]
  00470645  8b06                  mov       eax,[esi]
  00470647  a900000040            test      eax,40000000h
  0047064c  0f85f1000000          jne       near 00470743h
  00470652  66837e1000            cmp       word [esi+10h],0
  00470657  7545                  jne       short 0047069Eh
  00470659  80cc20                or        ah,20h
  0047065c  66c746100100          mov       word [esi+10h],1
  00470662  8906                  mov       [esi],eax
  00470664  668b4634              mov       ax,[esi+34h]
  00470668  66894612              mov       [esi+12h],ax
  0047066c  c7465c00000000        mov       dword [esi+5Ch],0
  00470673  8b0dcc975e00          mov       ecx,[5E97CCh]
  00470679  6a00                  push      0
  0047067b  6a00                  push      0
  0047067d  6800100000            push      1000h
  00470682  8b91a8000000          mov       edx,[ecx+0A8h]
  00470688  6800040000            push      400h
  0047068d  8b02                  mov       eax,[edx]
  0047068f  660fb64824            movzx     cx,byte [eax+24h]
  00470694  51                    push      ecx
  00470695  56                    push      esi
  00470696  e8e579ffff            call      00468080h
  0047069b  83c418                add       esp,18h

LAB_0047069e:
  0047069e  8b565c                mov       edx,[esi+5Ch]
  004706a1  42                    inc       edx
  004706a2  89565c                mov       [esi+5Ch],edx
  004706a5  8bc2                  mov       eax,edx
  004706a7  0fbf5616              movsx     edx,word [esi+16h]
  004706ab  3bc2                  cmp       eax,edx
  004706ad  7c5d                  jl        short 0047070Ch
  004706af  0fbf4614              movsx     eax,word [esi+14h]
  004706b3  48                    dec       eax
  004706b4  7418                  je        short 004706CEh
  004706b6  48                    dec       eax
  004706b7  740c                  je        short 004706C5h
  004706b9  48                    dec       eax
  004706ba  751e                  jne       short 004706DAh
  004706bc  a178195700            mov       eax,[571978h]
  004706c1  0c40                  or        al,40h
  004706c3  eb10                  jmp       short 004706D5h

LAB_004706c5:
  004706c5  a178195700            mov       eax,[571978h]
  004706ca  0c08                  or        al,8
  004706cc  eb07                  jmp       short 004706D5h

LAB_004706ce:
  004706ce  a178195700            mov       eax,[571978h]
  004706d3  0c01                  or        al,1

LAB_004706d5:
  004706d5  a378195700            mov       [571978h],eax

LAB_004706da:
  004706da  0fbf4614              movsx     eax,word [esi+14h]
  004706de  48                    dec       eax
  004706df  7419                  je        short 004706FAh
  004706e1  48                    dec       eax
  004706e2  740d                  je        short 004706F1h
  004706e4  48                    dec       eax
  004706e5  751f                  jne       short 00470706h
  004706e7  a178195700            mov       eax,[571978h]
  004706ec  80cc01                or        ah,1
  004706ef  eb10                  jmp       short 00470701h

LAB_004706f1:
  004706f1  a178195700            mov       eax,[571978h]
  004706f6  0c20                  or        al,20h
  004706f8  eb07                  jmp       short 00470701h

LAB_004706fa:
  004706fa  a178195700            mov       eax,[571978h]
  004706ff  0c04                  or        al,4

LAB_00470701:
  00470701  a378195700            mov       [571978h],eax

LAB_00470706:
  00470706  810e00000040          or        dword [esi],40000000h

LAB_0047070c:
  0047070c  817e5c10270000        cmp       dword [esi+5Ch],2710h
  00470713  7c07                  jl        short 0047071Ch
  00470715  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0047071c:
  0047071c  668b4612              mov       ax,[esi+12h]
  00470720  668146348000          add       word [esi+34h],80h
  00470726  668b4e34              mov       cx,[esi+34h]
  0047072a  0fbfd0                movsx     edx,ax
  0047072d  0fbfc9                movsx     ecx,cx
  00470730  81c200040000          add       edx,400h
  00470736  3bca                  cmp       ecx,edx
  00470738  7c09                  jl        short 00470743h
  0047073a  0500040000            add       eax,400h
  0047073f  66894634              mov       [esi+34h],ax

LAB_00470743:
  00470743  5e                    pop       esi
  00470744  c3                    ret
  00470745  90                    nop
  00470746  90                    nop
  00470747  90                    nop
  00470748  90                    nop
  00470749  90                    nop
  0047074a  90                    nop
  0047074b  90                    nop
  0047074c  90                    nop
  0047074d  90                    nop
  0047074e  90                    nop
  0047074f  90                    nop

12 cible(s) de saut interne : 0x0047069e, 0x004706c5, 0x004706ce, 0x004706d5, 0x004706da, 0x004706f1, 0x004706fa, 0x00470701, 0x00470706, 0x0047070c, 0x0047071c, 0x00470743
