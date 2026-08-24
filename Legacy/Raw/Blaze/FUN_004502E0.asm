Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004502e0  section .text  offset fichier 0x4f6e0  1042 octets

  004502e0  51                    push      ecx
  004502e1  53                    push      ebx
  004502e2  55                    push      ebp
  004502e3  56                    push      esi
  004502e4  8b742414              mov       esi,[esp+14h]
  004502e8  33db                  xor       ebx,ebx
  004502ea  57                    push      edi
  004502eb  66817e1001ff          cmp       word [esi+10h],0FF01h
  004502f1  885c2410              mov       [esp+10h],bl
  004502f5  c644241101            mov       byte [esp+11h],1
  004502fa  c644241204            mov       byte [esp+12h],4
  004502ff  c6442413ef            mov       byte [esp+13h],0EFh
  00450304  895e50                mov       [esi+50h],ebx
  00450307  895e54                mov       [esi+54h],ebx
  0045030a  895e58                mov       [esi+58h],ebx
  0045030d  bd01000000            mov       ebp,1
  00450312  7504                  jne       short 00450318h
  00450314  66896e10              mov       [esi+10h],bp

LAB_00450318:
  00450318  668b4610              mov       ax,[esi+10h]
  0045031c  663dfeff              cmp       ax,0FFFEh
  00450320  0f84c6030000          je        near 004506ECh
  00450326  663bc3                cmp       ax,bx
  00450329  7550                  jne       short 0045037Bh
  0045032b  668b4630              mov       ax,[esi+30h]
  0045032f  66c74610ffff          mov       word [esi+10h],0FFFFh
  00450335  663bc3                cmp       ax,bx
  00450338  6689869a000000        mov       [esi+9Ah],ax
  0045033f  66899e98000000        mov       [esi+98h],bx
  00450346  7d16                  jge       short 0045035Eh
  00450348  f7d8                  neg       eax
  0045034a  66c74610feff          mov       word [esi+10h],0FFFEh
  00450350  6689869a000000        mov       [esi+9Ah],ax
  00450357  6689ae98000000        mov       [esi+98h],bp

LAB_0045035e:
  0045035e  0fbf4634              movsx     eax,word [esi+34h]
  00450362  8bc8                  mov       ecx,eax
  00450364  66895e30              mov       [esi+30h],bx
  00450368  81e1ff000000          and       ecx,0FFh
  0045036e  66895e34              mov       [esi+34h],bx
  00450372  c1f808                sar       eax,8
  00450375  894e44                mov       [esi+44h],ecx
  00450378  894648                mov       [esi+48h],eax

LAB_0045037b:
  0045037b  66837e10ff            cmp       word [esi+10h],0FFFFh
  00450380  7523                  jne       short 004503A5h
  00450382  8b06                  mov       eax,[esi]
  00450384  a840                  test      al,40h
  00450386  741d                  je        short 004503A5h
  00450388  8b4e6c                mov       ecx,[esi+6Ch]
  0045038b  3bcb                  cmp       ecx,ebx
  0045038d  7505                  jne       short 00450394h
  0045038f  b958e55d00            mov       ecx,5DE558h

LAB_00450394:
  00450394  f70100000100          test      dword [ecx],10000h
  0045039a  7509                  jne       short 004503A5h
  0045039c  80cc20                or        ah,20h
  0045039f  66896e10              mov       [esi+10h],bp
  004503a3  8906                  mov       [esi],eax

LAB_004503a5:
  004503a5  668b4610              mov       ax,[esi+10h]
  004503a9  663d01ff              cmp       ax,0FF01h
  004503ad  7429                  je        short 004503D8h
  004503af  663dfeff              cmp       ax,0FFFEh
  004503b3  7423                  je        short 004503D8h
  004503b5  663bc3                cmp       ax,bx
  004503b8  741e                  je        short 004503D8h
  004503ba  663dffff              cmp       ax,0FFFFh
  004503be  7418                  je        short 004503D8h
  004503c0  663dca00              cmp       ax,0CAh
  004503c4  7412                  je        short 004503D8h
  004503c6  53                    push      ebx
  004503c7  e8344f0e00            call      00535300h
  004503cc  8b06                  mov       eax,[esi]
  004503ce  83c404                add       esp,4
  004503d1  25fffffffb            and       eax,0FBFFFFFFh
  004503d6  8906                  mov       [esi],eax

LAB_004503d8:
  004503d8  66396e10              cmp       [esi+10h],bp
  004503dc  bf02000000            mov       edi,2
  004503e1  7559                  jne       short 0045043Ch
  004503e3  e8586d0f00            call      00547140h
  004503e8  99                    cdq
  004503e9  b92c010000            mov       ecx,12Ch
  004503ee  53                    push      ebx
  004503ef  f7f9                  idiv      ecx
  004503f1  81c2f4010000          add       edx,1F4h
  004503f7  89565c                mov       [esi+5Ch],edx
  004503fa  e8014f0e00            call      00535300h
  004503ff  8b15cc975e00          mov       edx,[5E97CCh]
  00450405  83c404                add       esp,4
  00450408  8b82a8000000          mov       eax,[edx+0A8h]
  0045040e  53                    push      ebx
  0045040f  53                    push      ebx
  00450410  6800300000            push      3000h
  00450415  8b08                  mov       ecx,[eax]
  00450417  6846010000            push      146h
  0045041c  660fb65108            movzx     dx,byte [ecx+8]
  00450421  52                    push      edx
  00450422  56                    push      esi
  00450423  e8587c0100            call      00468080h
  00450428  8b06                  mov       eax,[esi]
  0045042a  83c418                add       esp,18h
  0045042d  25fffffffb            and       eax,0FBFFFFFFh
  00450432  66897e10              mov       [esi+10h],di
  00450436  8906                  mov       [esi],eax
  00450438  66895e12              mov       [esi+12h],bx

LAB_0045043c:
  0045043c  66397e10              cmp       [esi+10h],di
  00450440  bd14000000            mov       ebp,14h
  00450445  0f85c3000000          jne       near 0045050Eh
  0045044b  8b4e5c                mov       ecx,[esi+5Ch]
  0045044e  a128155700            mov       eax,[571528h]
  00450453  2bc8                  sub       ecx,eax
  00450455  c1f902                sar       ecx,2
  00450458  03c1                  add       eax,ecx
  0045045a  a328155700            mov       [571528h],eax
  0045045f  66834634dd            add       word [esi+34h],0FFDDh
  00450464  66ff4612              inc       word [esi+12h]
  00450468  66837e121f            cmp       word [esi+12h],1Fh
  0045046d  0f8c9b000000          jl        near 0045050Eh
  00450473  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0045047a  8bd0                  mov       edx,eax
  0045047c  8bc8                  mov       ecx,eax
  0045047e  83e2f8                and       edx,0FFFFFFF8h
  00450481  2bca                  sub       ecx,edx
  00450483  ba01000000            mov       edx,1
  00450488  d3e2                  shl       edx,cl
  0045048a  c1f803                sar       eax,3
  0045048d  8a8000905e00          mov       al,[eax+5E9000h]
  00450493  84d0                  test      al,dl
  00450495  7410                  je        short 004504A7h
  00450497  66c746102c01          mov       word [esi+10h],12Ch
  0045049d  66895e12              mov       [esi+12h],bx
  004504a1  5f                    pop       edi
  004504a2  5e                    pop       esi
  004504a3  5d                    pop       ebp
  004504a4  5b                    pop       ebx
  004504a5  59                    pop       ecx
  004504a6  c3                    ret

LAB_004504a7:
  004504a7  66837e14ff            cmp       word [esi+14h],0FFFFh
  004504ac  8b4648                mov       eax,[esi+48h]
  004504af  751e                  jne       short 004504CFh
  004504b1  3bc3                  cmp       eax,ebx
  004504b3  7412                  je        short 004504C7h
  004504b5  0fbfbe98000000        movsx     edi,word [esi+98h]
  004504bc  66c786980000002800    mov       word [esi+98h],28h
  004504c5  eb1a                  jmp       short 004504E1h

LAB_004504c7:
  004504c7  66c746102800          mov       word [esi+10h],28h
  004504cd  eb3b                  jmp       short 0045050Ah

LAB_004504cf:
  004504cf  3bc3                  cmp       eax,ebx
  004504d1  7433                  je        short 00450506h
  004504d3  0fbfbe98000000        movsx     edi,word [esi+98h]
  004504da  6689ae98000000        mov       [esi+98h],bp

LAB_004504e1:
  004504e1  6aff                  push      0FFFFFFFFh
  004504e3  05c0000000            add       eax,0C0h
  004504e8  53                    push      ebx
  004504e9  50                    push      eax
  004504ea  56                    push      esi
  004504eb  e8e0a40200            call      0047A9D0h
  004504f0  83c410                add       esp,10h
  004504f3  6689be98000000        mov       [esi+98h],di
  004504fa  66895e12              mov       [esi+12h],bx
  004504fe  66c74610e703          mov       word [esi+10h],3E7h
  00450504  eb08                  jmp       short 0045050Eh

LAB_00450506:
  00450506  66896e10              mov       [esi+10h],bp

LAB_0045050a:
  0045050a  66895e12              mov       [esi+12h],bx

LAB_0045050e:
  0045050e  668b4610              mov       ax,[esi+10h]
  00450512  663bc5                cmp       ax,bp
  00450515  754f                  jne       short 00450566h
  00450517  0fbf4e14              movsx     ecx,word [esi+14h]
  0045051b  8bd1                  mov       edx,ecx
  0045051d  53                    push      ebx
  0045051e  c1fa03                sar       edx,3
  00450521  68b0335600            push      5633B0h
  00450526  8d8240985e00          lea       eax,[edx+5E9840h]
  0045052c  8bd1                  mov       edx,ecx
  0045052e  83e2f8                and       edx,0FFFFFFF8h
  00450531  2bca                  sub       ecx,edx
  00450533  b201                  mov       dl,1
  00450535  d2e2                  shl       dl,cl
  00450537  8a08                  mov       cl,[eax]
  00450539  0aca                  or        cl,dl
  0045053b  8b15cc975e00          mov       edx,[5E97CCh]
  00450541  8808                  mov       [eax],cl
  00450543  8b466c                mov       eax,[esi+6Ch]
  00450546  0fbf4e16              movsx     ecx,word [esi+16h]
  0045054a  50                    push      eax
  0045054b  8b4270                mov       eax,[edx+70h]
  0045054e  8b0c88                mov       ecx,[eax+ecx*4]
  00450551  51                    push      ecx
  00450552  e8f93d0f00            call      00544350h
  00450557  83c410                add       esp,10h
  0045055a  66c746106400          mov       word [esi+10h],64h
  00450560  5f                    pop       edi
  00450561  5e                    pop       esi
  00450562  5d                    pop       ebp
  00450563  5b                    pop       ebx
  00450564  59                    pop       ecx
  00450565  c3                    ret

LAB_00450566:
  00450566  663d2800              cmp       ax,28h
  0045056a  0f858f000000          jne       near 004505FFh
  00450570  8b566c                mov       edx,[esi+6Ch]
  00450573  6800074500            push      450700h
  00450578  895a6c                mov       [edx+6Ch],ebx
  0045057b  8b466c                mov       eax,[esi+6Ch]
  0045057e  8a4e08                mov       cl,[esi+8]
  00450581  884841                mov       [eax+41h],cl
  00450584  8b5644                mov       edx,[esi+44h]
  00450587  8a8260985e00          mov       al,[edx+5E9860h]
  0045058d  3ac3                  cmp       al,bl
  0045058f  7542                  jne       short 004505D3h
  00450591  0fbf4616              movsx     eax,word [esi+16h]
  00450595  6a64                  push      64h
  00450597  50                    push      eax
  00450598  e883a30e00            call      0053A920h
  0045059d  8b4e6c                mov       ecx,[esi+6Ch]
  004505a0  83c408                add       esp,8
  004505a3  50                    push      eax
  004505a4  51                    push      ecx
  004505a5  e876090e00            call      00530F20h
  004505aa  83c40c                add       esp,0Ch
  004505ad  663bc3                cmp       ax,bx
  004505b0  750c                  jne       short 004505BEh
  004505b2  66c74610e703          mov       word [esi+10h],3E7h
  004505b8  5f                    pop       edi
  004505b9  5e                    pop       esi
  004505ba  5d                    pop       ebp
  004505bb  5b                    pop       ebx
  004505bc  59                    pop       ecx
  004505bd  c3                    ret

LAB_004505be:
  004505be  8b5644                mov       edx,[esi+44h]
  004505c1  66c74610c800          mov       word [esi+10h],0C8h
  004505c7  888260985e00          mov       [edx+5E9860h],al
  004505cd  5f                    pop       edi
  004505ce  5e                    pop       esi
  004505cf  5d                    pop       ebp
  004505d0  5b                    pop       ebx
  004505d1  59                    pop       ecx
  004505d2  c3                    ret

LAB_004505d3:
  004505d3  25ff000000            and       eax,0FFh
  004505d8  48                    dec       eax
  004505d9  50                    push      eax
  004505da  8b466c                mov       eax,[esi+6Ch]
  004505dd  50                    push      eax
  004505de  e83d090e00            call      00530F20h
  004505e3  83c40c                add       esp,0Ch
  004505e6  66f7d8                neg       ax
  004505e9  1bc0                  sbb       eax,eax
  004505eb  25e1fcffff            and       eax,0FFFFFCE1h
  004505f0  05e7030000            add       eax,3E7h
  004505f5  66894610              mov       [esi+10h],ax
  004505f9  5f                    pop       edi
  004505fa  5e                    pop       esi
  004505fb  5d                    pop       ebp
  004505fc  5b                    pop       ebx
  004505fd  59                    pop       ecx
  004505fe  c3                    ret

LAB_004505ff:
  004505ff  663d6400              cmp       ax,64h
  00450603  bfc8000000            mov       edi,0C8h
  00450608  753d                  jne       short 00450647h
  0045060a  668b869a000000        mov       ax,[esi+9Ah]
  00450611  c644241203            mov       byte [esp+12h],3
  00450616  8bc8                  mov       ecx,eax
  00450618  c1f903                sar       ecx,3
  0045061b  884c2411              mov       [esp+11h],cl
  0045061f  0fbfc8                movsx     ecx,ax
  00450622  8bd1                  mov       edx,ecx
  00450624  b001                  mov       al,1
  00450626  83e2f8                and       edx,0FFFFFFF8h
  00450629  2bca                  sub       ecx,edx
  0045062b  8d542410              lea       edx,[esp+10h]
  0045062f  d2e0                  shl       al,cl
  00450631  8d4c2418              lea       ecx,[esp+18h]
  00450635  51                    push      ecx
  00450636  52                    push      edx
  00450637  8844241b              mov       [esp+1Bh],al
  0045063b  e8c0390c00            call      00514000h
  00450640  83c408                add       esp,8
  00450643  66897e10              mov       [esi+10h],di

LAB_00450647:
  00450647  66397e10              cmp       [esi+10h],di
  0045064b  7518                  jne       short 00450665h
  0045064d  53                    push      ebx
  0045064e  e8ad4c0e00            call      00535300h
  00450653  8b06                  mov       eax,[esi]
  00450655  83c404                add       esp,4
  00450658  25fffffffb            and       eax,0FBFFFFFFh
  0045065d  66c74610c900          mov       word [esi+10h],0C9h
  00450663  8906                  mov       [esi],eax

LAB_00450665:
  00450665  668b4610              mov       ax,[esi+10h]
  00450669  663dc900              cmp       ax,0C9h
  0045066d  7527                  jne       short 00450696h
  0045066f  6683463446            add       word [esi+34h],46h
  00450674  66395e34              cmp       [esi+34h],bx
  00450678  7c1c                  jl        short 00450696h
  0045067a  66895e34              mov       [esi+34h],bx
  0045067e  66c74610ca00          mov       word [esi+10h],0CAh
  00450684  e8774d0e00            call      00535400h
  00450689  8b06                  mov       eax,[esi]
  0045068b  80e4df                and       ah,0DFh
  0045068e  8906                  mov       [esi],eax
  00450690  5f                    pop       edi
  00450691  5e                    pop       esi
  00450692  5d                    pop       ebp
  00450693  5b                    pop       ebx
  00450694  59                    pop       ecx
  00450695  c3                    ret

LAB_00450696:
  00450696  663dca00              cmp       ax,0CAh
  0045069a  7517                  jne       short 004506B3h
  0045069c  f646090f              test      byte [esi+9],0Fh
  004506a0  7511                  jne       short 004506B3h
  004506a2  668b8698000000        mov       ax,[esi+98h]
  004506a9  66f7d8                neg       ax
  004506ac  1bc0                  sbb       eax,eax
  004506ae  48                    dec       eax
  004506af  66894610              mov       [esi+10h],ax

LAB_004506b3:
  004506b3  66817e102c01          cmp       word [esi+10h],12Ch
  004506b9  752c                  jne       short 004506E7h
  004506bb  8b15c4975e00          mov       edx,[5E97C4h]
  004506c1  8b4e6c                mov       ecx,[esi+6Ch]
  004506c4  53                    push      ebx
  004506c5  68b0335600            push      5633B0h
  004506ca  8b4270                mov       eax,[edx+70h]
  004506cd  51                    push      ecx
  004506ce  8b888c040000          mov       ecx,[eax+48Ch]
  004506d4  51                    push      ecx
  004506d5  e8763c0f00            call      00544350h
  004506da  83c410                add       esp,10h
  004506dd  66897e10              mov       [esi+10h],di
  004506e1  5f                    pop       edi
  004506e2  5e                    pop       esi
  004506e3  5d                    pop       ebp
  004506e4  5b                    pop       ebx
  004506e5  59                    pop       ecx
  004506e6  c3                    ret

LAB_004506e7:
  004506e7  e854dffbff            call      0040E640h

LAB_004506ec:
  004506ec  5f                    pop       edi
  004506ed  5e                    pop       esi
  004506ee  5d                    pop       ebp
  004506ef  5b                    pop       ebx
  004506f0  59                    pop       ecx
  004506f1  c3                    ret

24 cible(s) de saut interne : 0x00450318, 0x0045035e, 0x0045037b, 0x00450394, 0x004503a5, 0x004503d8, 0x0045043c, 0x004504a7, 0x004504c7, 0x004504cf, 0x004504e1, 0x00450506, 0x0045050a, 0x0045050e, 0x00450566, 0x004505be, 0x004505d3, 0x004505ff, 0x00450647, 0x00450665, 0x00450696, 0x004506b3, 0x004506e7, 0x004506ec
