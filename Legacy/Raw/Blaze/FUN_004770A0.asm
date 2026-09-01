Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004770a0  section .text  offset fichier 0x764a0  702 octets

  004770a0  83ec18                sub       esp,18h
  004770a3  53                    push      ebx
  004770a4  55                    push      ebp
  004770a5  56                    push      esi
  004770a6  8b742428              mov       esi,[esp+28h]
  004770aa  57                    push      edi
  004770ab  8b06                  mov       eax,[esi]
  004770ad  8b6e78                mov       ebp,[esi+78h]
  004770b0  a900000002            test      eax,2000000h
  004770b5  750f                  jne       short 004770C6h
  004770b7  8b4e6c                mov       ecx,[esi+6Ch]
  004770ba  8b11                  mov       edx,[ecx]
  004770bc  81e200000002          and       edx,2000000h
  004770c2  0bd0                  or        edx,eax
  004770c4  8916                  mov       [esi],edx

LAB_004770c6:
  004770c6  8b06                  mov       eax,[esi]
  004770c8  a802                  test      al,2
  004770ca  0f84bc000000          je        near 0047718Ch
  004770d0  a900000001            test      eax,1000000h
  004770d5  741d                  je        short 004770F4h
  004770d7  a900000002            test      eax,2000000h
  004770dc  0f8464020000          je        near 00477346h
  004770e2  c7457400000000        mov       dword [ebp+74h],0
  004770e9  c70600000000          mov       dword [esi],0
  004770ef  e952020000            jmp       00477346h

LAB_004770f4:
  004770f4  0fbf4610              movsx     eax,word [esi+10h]
  004770f8  c1e008                shl       eax,8
  004770fb  50                    push      eax
  004770fc  e81be90c00            call      00545A1Ch
  00477101  c1f805                sar       eax,5
  00477104  66014632              add       [esi+32h],ax
  00477108  66837e1028            cmp       word [esi+10h],28h
  0047710d  7c5e                  jl        short 0047716Dh
  0047710f  668b4628              mov       ax,[esi+28h]
  00477113  bf00100000            mov       edi,1000h
  00477118  668bc8                mov       cx,ax
  0047711b  66c1f902              sar       cx,2
  0047711f  2bc1                  sub       eax,ecx
  00477121  b900100000            mov       ecx,1000h
  00477126  66894628              mov       [esi+28h],ax
  0047712a  6689462a              mov       [esi+2Ah],ax
  0047712e  6689462c              mov       [esi+2Ch],ax
  00477132  668b4528              mov       ax,[ebp+28h]
  00477136  0fbfd0                movsx     edx,ax
  00477139  2bca                  sub       ecx,edx
  0047713b  c1f902                sar       ecx,2
  0047713e  03c1                  add       eax,ecx
  00477140  668b4d2a              mov       cx,[ebp+2Ah]
  00477144  0fbfd1                movsx     edx,cx
  00477147  2bfa                  sub       edi,edx
  00477149  66894528              mov       [ebp+28h],ax
  0047714d  c1ff02                sar       edi,2
  00477150  03f9                  add       edi,ecx
  00477152  663d0810              cmp       ax,1008h
  00477156  66897d2a              mov       [ebp+2Ah],di
  0047715a  6689452c              mov       [ebp+2Ch],ax
  0047715e  7f0d                  jg        short 0047716Dh
  00477160  c7457400000000        mov       dword [ebp+74h],0
  00477167  c70600000000          mov       dword [esi],0

LAB_0047716d:
  0047716d  a1ace05d00            mov       eax,[5DE0ACh]
  00477172  85c0                  test      eax,eax
  00477174  0f84cc010000          je        near 00477346h
  0047717a  8bc8                  mov       ecx,eax
  0047717c  c1f902                sar       ecx,2
  0047717f  2bc8                  sub       ecx,eax
  00477181  890dace05d00          mov       [5DE0ACh],ecx
  00477187  e9ba010000            jmp       00477346h

LAB_0047718c:
  0047718c  33ff                  xor       edi,edi
  0047718e  66397e10              cmp       [esi+10h],di
  00477192  751b                  jne       short 004771AFh
  00477194  8d561c                lea       edx,[esi+1Ch]
  00477197  897574                mov       [ebp+74h],esi
  0047719a  52                    push      edx
  0047719b  6800120000            push      1200h
  004771a0  6800040000            push      400h
  004771a5  6a16                  push      16h
  004771a7  e874690c00            call      0053DB20h
  004771ac  83c410                add       esp,10h

LAB_004771af:
  004771af  668b4628              mov       ax,[esi+28h]
  004771b3  56                    push      esi
  004771b4  0fbf4e12              movsx     ecx,word [esi+12h]
  004771b8  0fbfd0                movsx     edx,ax
  004771bb  2bca                  sub       ecx,edx
  004771bd  c1f902                sar       ecx,2
  004771c0  03c1                  add       eax,ecx
  004771c2  66894628              mov       [esi+28h],ax
  004771c6  6689462a              mov       [esi+2Ah],ax
  004771ca  6689462c              mov       [esi+2Ch],ax
  004771ce  e8ade60b00            call      00535880h
  004771d3  8b4520                mov       eax,[ebp+20h]
  004771d6  8b4e20                mov       ecx,[esi+20h]
  004771d9  83c404                add       esp,4
  004771dc  3bc8                  cmp       ecx,eax
  004771de  0f8d62010000          jge       near 00477346h
  004771e4  8b16                  mov       edx,[esi]
  004771e6  668b4e06              mov       cx,[esi+6]
  004771ea  894620                mov       [esi+20h],eax
  004771ed  8b4674                mov       eax,[esi+74h]
  004771f0  83ca02                or        edx,2
  004771f3  66897e10              mov       [esi+10h],di
  004771f7  8916                  mov       [esi],edx
  004771f9  66894806              mov       [eax+6],cx
  004771fd  8b5678                mov       edx,[esi+78h]
  00477200  8b4674                mov       eax,[esi+74h]
  00477203  57                    push      edi
  00477204  52                    push      edx
  00477205  50                    push      eax
  00477206  e8954b0c00            call      0053BDA0h
  0047720b  83c40c                add       esp,0Ch
  0047720e  85c0                  test      eax,eax
  00477210  740b                  je        short 0047721Dh
  00477212  810e00400001          or        dword [esi],1004000h
  00477218  e911010000            jmp       0047732Eh

LAB_0047721d:
  0047721d  b800180000            mov       eax,1800h
  00477222  66c785960000002800    mov       word [ebp+96h],28h
  0047722b  894528                mov       [ebp+28h],eax
  0047722e  6689452c              mov       [ebp+2Ch],ax
  00477232  8b4614                mov       eax,[esi+14h]
  00477235  8b4e28                mov       ecx,[esi+28h]
  00477238  8b5638                mov       edx,[esi+38h]
  0047723b  89442418              mov       [esp+18h],eax
  0047723f  b803000000            mov       eax,3
  00477244  897e70                mov       [esi+70h],edi
  00477247  894c2410              mov       [esp+10h],ecx
  0047724b  89542414              mov       [esp+14h],edx
  0047724f  66c7461a0700          mov       word [esi+1Ah],7
  00477255  c7463860606001        mov       dword [esi+38h],1606060h
  0047725c  66894612              mov       [esi+12h],ax
  00477260  c7462800200020        mov       dword [esi+28h],20002000h
  00477267  66c746140030          mov       word [esi+14h],3000h
  0047726d  66894616              mov       [esi+16h],ax
  00477271  8d5e1c                lea       ebx,[esi+1Ch]
  00477274  c744242c06000000      mov       dword [esp+2Ch],6

LAB_0047727c:
  0047727c  e8bffe0c00            call      00547140h
  00477281  8bf8                  mov       edi,eax
  00477283  57                    push      edi
  00477284  e893e70c00            call      00545A1Ch
  00477289  c1e005                shl       eax,5
  0047728c  57                    push      edi
  0047728d  894650                mov       [esi+50h],eax
  00477290  c7465400000200        mov       dword [esi+54h],20000h
  00477297  e87ae70c00            call      00545A16h
  0047729c  c1e005                shl       eax,5
  0047729f  894658                mov       [esi+58h],eax
  004772a2  e899fe0c00            call      00547140h
  004772a7  8b13                  mov       edx,[ebx]
  004772a9  8b4e20                mov       ecx,[esi+20h]
  004772ac  83e03f                and       eax,3Fh
  004772af  894c2420              mov       [esp+20h],ecx
  004772b3  83e820                sub       eax,20h
  004772b6  c1e00c                shl       eax,0Ch
  004772b9  03c2                  add       eax,edx
  004772bb  8944241c              mov       [esp+1Ch],eax
  004772bf  e87cfe0c00            call      00547140h
  004772c4  8b4e24                mov       ecx,[esi+24h]
  004772c7  83e03f                and       eax,3Fh
  004772ca  83e820                sub       eax,20h
  004772cd  6a03                  push      3
  004772cf  6a00                  push      0
  004772d1  68be000000            push      0BEh
  004772d6  c1e00c                shl       eax,0Ch
  004772d9  8d542428              lea       edx,[esp+28h]
  004772dd  68081a5500            push      551A08h
  004772e2  03c1                  add       eax,ecx
  004772e4  52                    push      edx
  004772e5  56                    push      esi
  004772e6  8944243c              mov       [esp+3Ch],eax
  004772ea  e851390000            call      0047AC40h
  004772ef  8b442444              mov       eax,[esp+44h]
  004772f3  83c418                add       esp,18h
  004772f6  48                    dec       eax
  004772f7  8944242c              mov       [esp+2Ch],eax
  004772fb  0f857bffffff          jne       near 0047727Ch
  00477301  8b442410              mov       eax,[esp+10h]
  00477305  8b4c2414              mov       ecx,[esp+14h]
  00477309  8b542418              mov       edx,[esp+18h]
  0047730d  53                    push      ebx
  0047730e  6878185500            push      551878h
  00477313  894628                mov       [esi+28h],eax
  00477316  894e38                mov       [esi+38h],ecx
  00477319  895614                mov       [esi+14h],edx
  0047731c  e8bf680c00            call      0053DBE0h
  00477321  83c408                add       esp,8
  00477324  c705ace05d0008000000  mov       dword [5DE0ACh],8

LAB_0047732e:
  0047732e  8d461c                lea       eax,[esi+1Ch]
  00477331  50                    push      eax
  00477332  6800120000            push      1200h
  00477337  6800080000            push      800h
  0047733c  6a22                  push      22h
  0047733e  e8dd670c00            call      0053DB20h
  00477343  83c410                add       esp,10h

LAB_00477346:
  00477346  8b4d1c                mov       ecx,[ebp+1Ch]
  00477349  5f                    pop       edi
  0047734a  894e1c                mov       [esi+1Ch],ecx
  0047734d  8b5524                mov       edx,[ebp+24h]
  00477350  66ff4610              inc       word [esi+10h]
  00477354  895624                mov       [esi+24h],edx
  00477357  5e                    pop       esi
  00477358  5d                    pop       ebp
  00477359  5b                    pop       ebx
  0047735a  83c418                add       esp,18h
  0047735d  c3                    ret

9 cible(s) de saut interne : 0x004770c6, 0x004770f4, 0x0047716d, 0x0047718c, 0x004771af, 0x0047721d, 0x0047727c, 0x0047732e, 0x00477346
