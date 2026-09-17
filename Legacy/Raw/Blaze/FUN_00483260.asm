Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483260  section .text  offset fichier 0x82660  256 octets

  00483260  83ec0c                sub       esp,0Ch
  00483263  53                    push      ebx
  00483264  55                    push      ebp
  00483265  56                    push      esi
  00483266  8b74241c              mov       esi,[esp+1Ch]
  0048326a  33c0                  xor       eax,eax
  0048326c  66c744240e38ff        mov       word [esp+0Eh],0FF38h
  00483273  8b5e70                mov       ebx,[esi+70h]
  00483276  668944240c            mov       [esp+0Ch],ax
  0048327b  66c7442410c800        mov       word [esp+10h],0C8h
  00483282  66c74424123200        mov       word [esp+12h],32h
  00483289  6689442414            mov       [esp+14h],ax
  0048328e  66c74424169001        mov       word [esp+16h],190h
  00483295  8b13                  mov       edx,[ebx]
  00483297  57                    push      edi
  00483298  83e2fb                and       edx,0FFFFFFFBh
  0048329b  b890010000            mov       eax,190h
  004832a0  8913                  mov       [ebx],edx
  004832a2  0fbf7e12              movsx     edi,word [esi+12h]
  004832a6  0fbf6e32              movsx     ebp,word [esi+32h]
  004832aa  2bc7                  sub       eax,edi
  004832ac  55                    push      ebp
  004832ad  c1f803                sar       eax,3
  004832b0  03f8                  add       edi,eax
  004832b2  66897e12              mov       [esi+12h],di
  004832b6  e861270c00            call      00545A1Ch
  004832bb  0fafc7                imul      eax,edi
  004832be  8b4b1c                mov       ecx,[ebx+1Ch]
  004832c1  55                    push      ebp
  004832c2  03c1                  add       eax,ecx
  004832c4  89461c                mov       [esi+1Ch],eax
  004832c7  e84a270c00            call      00545A16h
  004832cc  0fafc7                imul      eax,edi
  004832cf  034324                add       eax,[ebx+24h]
  004832d2  894624                mov       [esi+24h],eax
  004832d5  0fbf4628              movsx     eax,word [esi+28h]
  004832d9  8bc8                  mov       ecx,eax
  004832db  c1f904                sar       ecx,4
  004832de  2bc1                  sub       eax,ecx
  004832e0  66894628              mov       [esi+28h],ax
  004832e4  6689462c              mov       [esi+2Ch],ax
  004832e8  668b462a              mov       ax,[esi+2Ah]
  004832ec  668bd0                mov       dx,ax
  004832ef  66c1fa04              sar       dx,4
  004832f3  2bc2                  sub       eax,edx
  004832f5  6689462a              mov       [esi+2Ah],ax
  004832f9  668b4610              mov       ax,[esi+10h]
  004832fd  663d0a00              cmp       ax,0Ah
  00483301  7d3d                  jge       short 00483340h
  00483303  40                    inc       eax
  00483304  66894610              mov       [esi+10h],ax
  00483308  8d442410              lea       eax,[esp+10h]
  0048330c  50                    push      eax
  0048330d  56                    push      esi
  0048330e  e88d19ffff            call      00474CA0h
  00483313  8bf8                  mov       edi,eax
  00483315  83c408                add       esp,8
  00483318  85ff                  test      edi,edi
  0048331a  7437                  je        short 00483353h
  0048331c  8b4e74                mov       ecx,[esi+74h]
  0048331f  57                    push      edi
  00483320  51                    push      ecx
  00483321  e85a680b00            call      00539B80h
  00483326  8b07                  mov       eax,[edi]
  00483328  83c408                add       esp,8
  0048332b  0c40                  or        al,40h
  0048332d  66c787960000000800    mov       word [edi+96h],8
  00483336  8907                  mov       [edi],eax
  00483338  5f                    pop       edi
  00483339  5e                    pop       esi
  0048333a  5d                    pop       ebp
  0048333b  5b                    pop       ebx
  0048333c  83c40c                add       esp,0Ch
  0048333f  c3                    ret

LAB_00483340:
  00483340  8b4638                mov       eax,[esi+38h]
  00483343  05f8f7f7ff            add       eax,0FFF7F7F8h
  00483348  894638                mov       [esi+38h],eax
  0048334b  7506                  jne       short 00483353h
  0048334d  c70600000000          mov       dword [esi],0

LAB_00483353:
  00483353  5f                    pop       edi
  00483354  5e                    pop       esi
  00483355  5d                    pop       ebp
  00483356  5b                    pop       ebx
  00483357  83c40c                add       esp,0Ch
  0048335a  c3                    ret
  0048335b  90                    nop
  0048335c  90                    nop
  0048335d  90                    nop
  0048335e  90                    nop
  0048335f  90                    nop

2 cible(s) de saut interne : 0x00483340, 0x00483353
