C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004625b0  section .text  offset fichier 0x619b0  676 octets

  004625b0  a168e05d00            mov       eax,[5DE068h]
  004625b5  53                    push      ebx
  004625b6  56                    push      esi
  004625b7  57                    push      edi
  004625b8  a900f0ffff            test      eax,0FFFFF000h
  004625bd  0f8f8d020000          jg        near 00462850h
  004625c3  813d6ce05d0000c0e0ff  cmp       dword [5DE06Ch],0FFE0C000h
  004625cd  0f8f7d020000          jg        near 00462850h
  004625d3  8b742410              mov       esi,[esp+10h]
  004625d7  56                    push      esi
  004625d8  e8e3e9ffff            call      00460FC0h
  004625dd  33db                  xor       ebx,ebx
  004625df  83c404                add       esp,4
  004625e2  66395e10              cmp       [esi+10h],bx
  004625e6  7545                  jne       short 0046262Dh
  004625e8  66c746101000          mov       word [esi+10h],10h
  004625ee  66895e12              mov       [esi+12h],bx
  004625f2  66895e14              mov       [esi+14h],bx
  004625f6  66c746160100          mov       word [esi+16h],1
  004625fc  e83f4b0e00            call      00547140h
  00462601  99                    cdq
  00462602  b9b80b0000            mov       ecx,0BB8h
  00462607  56                    push      esi
  00462608  f7f9                  idiv      ecx
  0046260a  c7462400408300        mov       dword [esi+24h],834000h
  00462611  66895e2a              mov       [esi+2Ah],bx
  00462615  66895e28              mov       [esi+28h],bx
  00462619  81ea740e0000          sub       edx,0E74h
  0046261f  c1e20c                shl       edx,0Ch
  00462622  895620                mov       [esi+20h],edx
  00462625  e816130d00            call      00533940h
  0046262a  83c404                add       esp,4

LAB_0046262d:
  0046262d  668b4616              mov       ax,[esi+16h]
  00462631  663bc3                cmp       ax,bx
  00462634  0f8493000000          je        near 004626CDh
  0046263a  663d0100              cmp       ax,1
  0046263e  ba02000000            mov       edx,2
  00462643  7527                  jne       short 0046266Ch
  00462645  b9c8000000            mov       ecx,0C8h
  0046264a  66014e28              add       [esi+28h],cx
  0046264e  66014e2a              add       [esi+2Ah],cx
  00462652  668b4628              mov       ax,[esi+28h]
  00462656  b900100000            mov       ecx,1000h
  0046265b  663bc1                cmp       ax,cx
  0046265e  7e0c                  jle       short 0046266Ch
  00462660  66894e28              mov       [esi+28h],cx
  00462664  66894e2a              mov       [esi+2Ah],cx
  00462668  66895616              mov       [esi+16h],dx

LAB_0046266c:
  0046266c  66395616              cmp       [esi+16h],dx
  00462670  0f85da010000          jne       near 00462850h
  00462676  66ff4614              inc       word [esi+14h]
  0046267a  8b4620                mov       eax,[esi+20h]
  0046267d  0fbf5614              movsx     edx,word [esi+14h]
  00462681  66834634f0            add       word [esi+34h],0FFF0h
  00462686  c1e20c                shl       edx,0Ch
  00462689  2bc2                  sub       eax,edx
  0046268b  894620                mov       [esi+20h],eax
  0046268e  8bc8                  mov       ecx,eax
  00462690  668b4634              mov       ax,[esi+34h]
  00462694  663bc3                cmp       ax,bx
  00462697  7d09                  jge       short 004626A2h
  00462699  0500100000            add       eax,1000h
  0046269e  66894634              mov       [esi+34h],ax

LAB_004626a2:
  004626a2  81f900c0b4fe          cmp       ecx,0FEB4C000h
  004626a8  0f8fa2010000          jg        near 00462850h
  004626ae  66895e16              mov       [esi+16h],bx
  004626b2  66c74614c0ff          mov       word [esi+14h],0FFC0h
  004626b8  e8834a0e00            call      00547140h
  004626bd  99                    cdq
  004626be  b914000000            mov       ecx,14h
  004626c3  f7f9                  idiv      ecx
  004626c5  66895612              mov       [esi+12h],dx
  004626c9  5f                    pop       edi
  004626ca  5e                    pop       esi
  004626cb  5b                    pop       ebx
  004626cc  c3                    ret

LAB_004626cd:
  004626cd  0fbf5614              movsx     edx,word [esi+14h]
  004626d1  52                    push      edx
  004626d2  e845330e00            call      00545A1Ch
  004626d7  85c0                  test      eax,eax
  004626d9  7556                  jne       short 00462731h
  004626db  53                    push      ebx
  004626dc  53                    push      ebx
  004626dd  68e7030000            push      3E7h
  004626e2  53                    push      ebx
  004626e3  68e7030000            push      3E7h
  004626e8  56                    push      esi
  004626e9  e87297fbff            call      0041BE60h
  004626ee  83c418                add       esp,18h
  004626f1  bf09000000            mov       edi,9

LAB_004626f6:
  004626f6  53                    push      ebx
  004626f7  53                    push      ebx
  004626f8  6aff                  push      0FFFFFFFFh
  004626fa  53                    push      ebx
  004626fb  6a31                  push      31h
  004626fd  56                    push      esi
  004626fe  e80d5a0000            call      00468110h
  00462703  83c418                add       esp,18h
  00462706  4f                    dec       edi
  00462707  75ed                  jne       short 004626F6h
  00462709  a1cc975e00            mov       eax,[5E97CCh]
  0046270e  53                    push      ebx
  0046270f  53                    push      ebx
  00462710  6800180000            push      1800h
  00462715  8b88a8000000          mov       ecx,[eax+0A8h]
  0046271b  6800030000            push      300h
  00462720  8b11                  mov       edx,[ecx]
  00462722  660fb64219            movzx     ax,byte [edx+19h]
  00462727  50                    push      eax
  00462728  56                    push      esi
  00462729  e852590000            call      00468080h
  0046272e  83c418                add       esp,18h

LAB_00462731:
  00462731  8b4e1c                mov       ecx,[esi+1Ch]
  00462734  66ff4612              inc       word [esi+12h]
  00462738  8b4624                mov       eax,[esi+24h]
  0046273b  894e44                mov       [esi+44h],ecx
  0046273e  0fbf4e12              movsx     ecx,word [esi+12h]
  00462742  8b5620                mov       edx,[esi+20h]
  00462745  89464c                mov       [esi+4Ch],eax
  00462748  c1e107                shl       ecx,7
  0046274b  2bc1                  sub       eax,ecx
  0046274d  56                    push      esi
  0046274e  2d00400100            sub       eax,14000h
  00462753  895648                mov       [esi+48h],edx
  00462756  894624                mov       [esi+24h],eax
  00462759  e8e2110d00            call      00533940h
  0046275e  0fbf5614              movsx     edx,word [esi+14h]
  00462762  83c404                add       esp,4
  00462765  52                    push      edx
  00462766  e8b1320e00            call      00545A1Ch
  0046276b  c1e008                shl       eax,8
  0046276e  780f                  js        short 0046277Fh
  00462770  0fbf4614              movsx     eax,word [esi+14h]
  00462774  50                    push      eax
  00462775  e8a2320e00            call      00545A1Ch
  0046277a  c1e008                shl       eax,8
  0046277d  eb0f                  jmp       short 0046278Eh

LAB_0046277f:
  0046277f  0fbf4e14              movsx     ecx,word [esi+14h]
  00462783  51                    push      ecx
  00462784  e893320e00            call      00545A1Ch
  00462789  c1e008                shl       eax,8
  0046278c  f7d8                  neg       eax

LAB_0046278e:
  0046278e  8b5660                mov       edx,[esi+60h]
  00462791  6683461440            add       word [esi+14h],40h
  00462796  8d840200e00100        lea       eax,[edx+eax+1E000h]
  0046279d  894620                mov       [esi+20h],eax
  004627a0  668b4614              mov       ax,[esi+14h]
  004627a4  663d0010              cmp       ax,1000h
  004627a8  7e09                  jle       short 004627B3h
  004627aa  0500f0ffff            add       eax,0FFFFF000h
  004627af  66894614              mov       [esi+14h],ax

LAB_004627b3:
  004627b3  0fbf4e14              movsx     ecx,word [esi+14h]
  004627b7  51                    push      ecx
  004627b8  e85f320e00            call      00545A1Ch
  004627bd  85c0                  test      eax,eax
  004627bf  7534                  jne       short 004627F5h
  004627c1  6683461020            add       word [esi+10h],20h
  004627c6  53                    push      ebx
  004627c7  53                    push      ebx
  004627c8  68e7030000            push      3E7h
  004627cd  6a1e                  push      1Eh
  004627cf  68e7030000            push      3E7h
  004627d4  56                    push      esi
  004627d5  e88696fbff            call      0041BE60h
  004627da  83c418                add       esp,18h
  004627dd  bf09000000            mov       edi,9

LAB_004627e2:
  004627e2  53                    push      ebx
  004627e3  53                    push      ebx
  004627e4  6aff                  push      0FFFFFFFFh
  004627e6  53                    push      ebx
  004627e7  6a31                  push      31h
  004627e9  56                    push      esi
  004627ea  e821590000            call      00468110h
  004627ef  83c418                add       esp,18h
  004627f2  4f                    dec       edi
  004627f3  75ed                  jne       short 004627E2h

LAB_004627f5:
  004627f5  668b5610              mov       dx,[esi+10h]
  004627f9  66295634              sub       [esi+34h],dx
  004627fd  668b4634              mov       ax,[esi+34h]
  00462801  663bc3                cmp       ax,bx
  00462804  7d09                  jge       short 0046280Fh
  00462806  0500100000            add       eax,1000h
  0046280b  66894634              mov       [esi+34h],ax

LAB_0046280f:
  0046280f  66817e12aa00          cmp       word [esi+12h],0AAh
  00462815  7e2b                  jle       short 00462842h
  00462817  b938ffffff            mov       ecx,0FFFFFF38h
  0046281c  66014e2a              add       [esi+2Ah],cx
  00462820  668b462a              mov       ax,[esi+2Ah]
  00462824  66014e28              add       [esi+28h],cx
  00462828  668b4e28              mov       cx,[esi+28h]
  0046282c  663bc3                cmp       ax,bx
  0046282f  7c05                  jl        short 00462836h
  00462831  663bcb                cmp       cx,bx
  00462834  7d0c                  jge       short 00462842h

LAB_00462836:
  00462836  66895e2a              mov       [esi+2Ah],bx
  0046283a  66895e28              mov       [esi+28h],bx
  0046283e  66895e10              mov       [esi+10h],bx

LAB_00462842:
  00462842  53                    push      ebx
  00462843  53                    push      ebx
  00462844  53                    push      ebx
  00462845  6a0a                  push      0Ah
  00462847  56                    push      esi
  00462848  e8f3b1ffff            call      0045DA40h
  0046284d  83c414                add       esp,14h

LAB_00462850:
  00462850  5f                    pop       edi
  00462851  5e                    pop       esi
  00462852  5b                    pop       ebx
  00462853  c3                    ret

15 cible(s) de saut interne : 0x0046262d, 0x0046266c, 0x004626a2, 0x004626cd, 0x004626f6, 0x00462731, 0x0046277f, 0x0046278e, 0x004627b3, 0x004627e2, 0x004627f5, 0x0046280f, 0x00462836, 0x00462842, 0x00462850
