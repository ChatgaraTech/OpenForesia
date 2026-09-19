Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484560  section .text  offset fichier 0x83960  512 octets

  00484560  83ec18                sub       esp,18h
  00484563  53                    push      ebx
  00484564  55                    push      ebp
  00484565  56                    push      esi
  00484566  8b742428              mov       esi,[esp+28h]
  0048456a  33c0                  xor       eax,eax
  0048456c  57                    push      edi
  0048456d  8b0e                  mov       ecx,[esi]
  0048456f  6689442410            mov       [esp+10h],ax
  00484574  f7c100000002          test      ecx,2000000h
  0048457a  66c74424126aff        mov       word [esp+12h],0FF6Ah
  00484581  66c74424146400        mov       word [esp+14h],64h
  00484588  66c74424169600        mov       word [esp+16h],96h
  0048458f  6689442418            mov       [esp+18h],ax
  00484594  66c744241a2c01        mov       word [esp+1Ah],12Ch
  0048459b  0f843e010000          je        near 004846DFh
  004845a1  0fbf5610              movsx     edx,word [esi+10h]
  004845a5  0fbf5e28              movsx     ebx,word [esi+28h]
  004845a9  0fbf4e12              movsx     ecx,word [esi+12h]
  004845ad  0fbf7e2a              movsx     edi,word [esi+2Ah]
  004845b1  b800200000            mov       eax,2000h
  004845b6  c1e208                shl       edx,8
  004845b9  2bc3                  sub       eax,ebx
  004845bb  03ca                  add       ecx,edx
  004845bd  c1f803                sar       eax,3
  004845c0  51                    push      ecx
  004845c1  03d8                  add       ebx,eax
  004845c3  e854140c00            call      00545A1Ch
  004845c8  8d840000300000        lea       eax,[eax+eax+3000h]
  004845cf  8b5624                mov       edx,[esi+24h]
  004845d2  2bc7                  sub       eax,edi
  004845d4  8b6e20                mov       ebp,[esi+20h]
  004845d7  c1f802                sar       eax,2
  004845da  03f8                  add       edi,eax
  004845dc  8bcb                  mov       ecx,ebx
  004845de  c1e710                shl       edi,10h
  004845e1  0bfb                  or        edi,ebx
  004845e3  89542424              mov       [esp+24h],edx
  004845e7  897e28                mov       [esi+28h],edi
  004845ea  0fbf7e32              movsx     edi,word [esi+32h]
  004845ee  d1f9                  sar       ecx,1
  004845f0  03cb                  add       ecx,ebx
  004845f2  8b5e1c                mov       ebx,[esi+1Ch]
  004845f5  57                    push      edi
  004845f6  66894e2c              mov       [esi+2Ch],cx
  004845fa  e81d140c00            call      00545A1Ch
  004845ff  8b4e1c                mov       ecx,[esi+1Ch]
  00484602  57                    push      edi
  00484603  c1e004                shl       eax,4
  00484606  03c8                  add       ecx,eax
  00484608  894e1c                mov       [esi+1Ch],ecx
  0048460b  e806140c00            call      00545A16h
  00484610  8b4e24                mov       ecx,[esi+24h]
  00484613  8b7e20                mov       edi,[esi+20h]
  00484616  c1e004                shl       eax,4
  00484619  03c8                  add       ecx,eax
  0048461b  56                    push      esi
  0048461c  894e24                mov       [esi+24h],ecx
  0048461f  e81cf30a00            call      00533940h
  00484624  8b4660                mov       eax,[esi+60h]
  00484627  83c404                add       esp,4
  0048462a  3bf8                  cmp       edi,eax
  0048462c  897e20                mov       [esi+20h],edi
  0048462f  7c2e                  jl        short 0048465Fh
  00484631  8b8e90000000          mov       ecx,[esi+90h]
  00484637  8bd1                  mov       edx,ecx
  00484639  81e200220000          and       edx,2200h
  0048463f  81fa00200000          cmp       edx,2000h
  00484645  7418                  je        short 0048465Fh
  00484647  8d9700a00000          lea       edx,[edi+0A000h]
  0048464d  3bd7                  cmp       edx,edi
  0048464f  7f0b                  jg        short 0048465Ch
  00484651  80cd08                or        ch,8
  00484654  898e90000000          mov       [esi+90h],ecx
  0048465a  eb03                  jmp       short 0048465Fh

LAB_0048465c:
  0048465c  894620                mov       [esi+20h],eax

LAB_0048465f:
  0048465f  f7869000000002080000  test      dword [esi+90h],802h
  00484669  740d                  je        short 00484678h
  0048466b  8b442424              mov       eax,[esp+24h]
  0048466f  895e1c                mov       [esi+1Ch],ebx
  00484672  896e20                mov       [esi+20h],ebp
  00484675  894624                mov       [esi+24h],eax

LAB_00484678:
  00484678  817e3820202000        cmp       dword [esi+38h],202020h
  0048467f  7230                  jb        short 004846B1h
  00484681  8d4c2410              lea       ecx,[esp+10h]
  00484685  51                    push      ecx
  00484686  56                    push      esi
  00484687  e81406ffff            call      00474CA0h
  0048468c  8bf8                  mov       edi,eax
  0048468e  83c408                add       esp,8
  00484691  85ff                  test      edi,edi
  00484693  741c                  je        short 004846B1h
  00484695  8b5674                mov       edx,[esi+74h]
  00484698  57                    push      edi
  00484699  52                    push      edx
  0048469a  e8e1540b00            call      00539B80h
  0048469f  8b07                  mov       eax,[edi]
  004846a1  83c408                add       esp,8
  004846a4  0c40                  or        al,40h
  004846a6  66c787960000000800    mov       word [edi+96h],8
  004846af  8907                  mov       [edi],eax

LAB_004846b1:
  004846b1  66ff4610              inc       word [esi+10h]
  004846b5  66837e1014            cmp       word [esi+10h],14h
  004846ba  0f8c97000000          jl        near 00484757h
  004846c0  8b4638                mov       eax,[esi+38h]
  004846c3  05f8f7f7ff            add       eax,0FFF7F7F8h
  004846c8  894638                mov       [esi+38h],eax
  004846cb  0f8586000000          jne       near 00484757h
  004846d1  c70600000000          mov       dword [esi],0
  004846d7  5f                    pop       edi
  004846d8  5e                    pop       esi
  004846d9  5d                    pop       ebp
  004846da  5b                    pop       ebx
  004846db  83c418                add       esp,18h
  004846de  c3                    ret

LAB_004846df:
  004846df  66394612              cmp       [esi+12h],ax
  004846e3  7552                  jne       short 00484737h
  004846e5  e8562a0c00            call      00547140h
  004846ea  8bf8                  mov       edi,eax
  004846ec  57                    push      edi
  004846ed  e82a130c00            call      00545A1Ch
  004846f2  8d0480                lea       eax,[eax+eax*4]
  004846f5  c1e002                shl       eax,2
  004846f8  894650                mov       [esi+50h],eax
  004846fb  e8402a0c00            call      00547140h
  00484700  83e00f                and       eax,0Fh
  00484703  57                    push      edi
  00484704  83c014                add       eax,14h
  00484707  c1e00c                shl       eax,0Ch
  0048470a  894654                mov       [esi+54h],eax
  0048470d  e804130c00            call      00545A16h
  00484712  33d2                  xor       edx,edx
  00484714  6aff                  push      0FFFFFFFFh
  00484716  8a560b                mov       dl,[esi+0Bh]
  00484719  8d0c80                lea       ecx,[eax+eax*4]
  0048471c  52                    push      edx
  0048471d  6a4a                  push      4Ah
  0048471f  8d461c                lea       eax,[esi+1Ch]
  00484722  68b81b5500            push      551BB8h
  00484727  c1e102                shl       ecx,2
  0048472a  50                    push      eax
  0048472b  56                    push      esi
  0048472c  894e58                mov       [esi+58h],ecx
  0048472f  e80c65ffff            call      0047AC40h
  00484734  83c418                add       esp,18h

LAB_00484737:
  00484737  8b4638                mov       eax,[esi+38h]
  0048473a  3d80808000            cmp       eax,808080h
  0048473f  7310                  jae       short 00484751h
  00484741  0508080800            add       eax,80808h
  00484746  894638                mov       [esi+38h],eax
  00484749  5f                    pop       edi
  0048474a  5e                    pop       esi
  0048474b  5d                    pop       ebp
  0048474c  5b                    pop       ebx
  0048474d  83c418                add       esp,18h
  00484750  c3                    ret

LAB_00484751:
  00484751  810e00000002          or        dword [esi],2000000h

LAB_00484757:
  00484757  5f                    pop       edi
  00484758  5e                    pop       esi
  00484759  5d                    pop       ebp
  0048475a  5b                    pop       ebx
  0048475b  83c418                add       esp,18h
  0048475e  c3                    ret
  0048475f  90                    nop

8 cible(s) de saut interne : 0x0048465c, 0x0048465f, 0x00484678, 0x004846b1, 0x004846df, 0x00484737, 0x00484751, 0x00484757
