Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x005037d0  section .text  offset fichier 0x102bd0  5300 octets

  005037d0  8b442408              mov       eax,[esp+8]
  005037d4  c70000000000          mov       dword [eax],0
  005037da  33c0                  xor       eax,eax
  005037dc  c3                    ret
  005037dd  90                    nop
  005037de  90                    nop
  005037df  90                    nop
  005037e0  a14ce55d00            mov       eax,[5DE54Ch]
  005037e5  83f802                cmp       eax,2
  005037e8  7405                  je        short 005037EFh
  005037ea  83f803                cmp       eax,3
  005037ed  750a                  jne       short 005037F9h

LAB_005037ef:
  005037ef  a17c4b6000            mov       eax,[604B7Ch]
  005037f4  a34ce55d00            mov       [5DE54Ch],eax

LAB_005037f9:
  005037f9  83e800                sub       eax,0
  005037fc  7427                  je        short 00503825h
  005037fe  48                    dec       eax
  005037ff  7542                  jne       short 00503843h
  00503801  8b442404              mov       eax,[esp+4]
  00503805  c705e09a5e00e8676000  mov       dword [5E9AE0h],6067E8h
  0050380f  c705bcde5e0068e05d00  mov       dword [5EDEBCh],5DE068h
  00503819  c70594ca5e00d0676000  mov       dword [5ECA94h],6067D0h
  00503823  40                    inc       eax
  00503824  c3                    ret

LAB_00503825:
  00503825  c705e09a5e0050465600  mov       dword [5E9AE0h],564650h
  0050382f  c705bcde5e0068e05d00  mov       dword [5EDEBCh],5DE068h
  00503839  c70594ca5e0078e05d00  mov       dword [5ECA94h],5DE078h

LAB_00503843:
  00503843  8b442404              mov       eax,[esp+4]
  00503847  40                    inc       eax
  00503848  c3                    ret
  00503849  90                    nop
  0050384a  90                    nop
  0050384b  90                    nop
  0050384c  90                    nop
  0050384d  90                    nop
  0050384e  90                    nop
  0050384f  90                    nop
  00503850  8b0dc4195700          mov       ecx,[5719C4h]
  00503856  56                    push      esi
  00503857  8b742408              mov       esi,[esp+8]
  0050385b  8b15cc975e00          mov       edx,[5E97CCh]
  00503861  33c0                  xor       eax,eax
  00503863  57                    push      edi
  00503864  8a4601                mov       al,[esi+1]
  00503867  8b3db4195700          mov       edi,[5719B4h]
  0050386d  8a6602                mov       ah,[esi+2]
  00503870  50                    push      eax
  00503871  51                    push      ecx
  00503872  52                    push      edx
  00503873  e858050100            call      00513DD0h
  00503878  a1b4195700            mov       eax,[5719B4h]
  0050387d  83c40c                add       esp,0Ch
  00503880  3bf8                  cmp       edi,eax
  00503882  7508                  jne       short 0050388Ch
  00503884  83c603                add       esi,3
  00503887  8bc6                  mov       eax,esi
  00503889  5f                    pop       edi
  0050388a  5e                    pop       esi
  0050388b  c3                    ret

LAB_0050388c:
  0050388c  8bf0                  mov       esi,eax
  0050388e  5f                    pop       edi
  0050388f  5e                    pop       esi
  00503890  c3                    ret
  00503891  90                    nop
  00503892  90                    nop
  00503893  90                    nop
  00503894  90                    nop
  00503895  90                    nop
  00503896  90                    nop
  00503897  90                    nop
  00503898  90                    nop
  00503899  90                    nop
  0050389a  90                    nop
  0050389b  90                    nop
  0050389c  90                    nop
  0050389d  90                    nop
  0050389e  90                    nop
  0050389f  90                    nop
  005038a0  a0341a5700            mov       al,[571A34h]
  005038a5  53                    push      ebx
  005038a6  56                    push      esi
  005038a7  57                    push      edi
  005038a8  84c0                  test      al,al
  005038aa  742a                  je        short 005038D6h
  005038ac  fec8                  dec       al
  005038ae  a2341a5700            mov       [571A34h],al
  005038b3  750f                  jne       short 005038C4h
  005038b5  8b442410              mov       eax,[esp+10h]
  005038b9  83c002                add       eax,2
  005038bc  89442410              mov       [esp+10h],eax
  005038c0  5f                    pop       edi
  005038c1  5e                    pop       esi
  005038c2  5b                    pop       ebx
  005038c3  c3                    ret

LAB_005038c4:
  005038c4  8b442414              mov       eax,[esp+14h]
  005038c8  c70000000000          mov       dword [eax],0
  005038ce  8b442410              mov       eax,[esp+10h]
  005038d2  5f                    pop       edi
  005038d3  5e                    pop       esi
  005038d4  5b                    pop       ebx
  005038d5  c3                    ret

LAB_005038d6:
  005038d6  a1f8195700            mov       eax,[5719F8h]
  005038db  8b1d281a5700          mov       ebx,[571A28h]
  005038e1  668b15f0195700        mov       dx,[5719F0h]
  005038e8  8b0d001a5700          mov       ecx,[571A00h]
  005038ee  668b35f2195700        mov       si,[5719F2h]
  005038f5  8b3dfc195700          mov       edi,[5719FCh]
  005038fb  a3181a5700            mov       [571A18h],eax
  00503900  891d081a5700          mov       [571A08h],ebx
  00503906  8b1d2c1a5700          mov       ebx,[571A2Ch]
  0050390c  668915e8195700        mov       [5719E8h],dx
  00503913  f7d8                  neg       eax
  00503915  890d201a5700          mov       [571A20h],ecx
  0050391b  891d0c1a5700          mov       [571A0Ch],ebx
  00503921  8b1d301a5700          mov       ebx,[571A30h]
  00503927  66891528496000        mov       [604928h],dx
  0050392e  668b15ec195700        mov       dx,[5719ECh]
  00503935  a3d0086000            mov       [6008D0h],eax
  0050393a  8b442410              mov       eax,[esp+10h]
  0050393e  668935ea195700        mov       [5719EAh],si
  00503945  f7d9                  neg       ecx
  00503947  893d1c1a5700          mov       [571A1Ch],edi
  0050394d  891d101a5700          mov       [571A10h],ebx
  00503953  6689352a496000        mov       [60492Ah],si
  0050395a  6689152c496000        mov       [60492Ch],dx
  00503961  8b542414              mov       edx,[esp+14h]
  00503965  893dd4086000          mov       [6008D4h],edi
  0050396b  890dd8086000          mov       [6008D8h],ecx
  00503971  8a4801                mov       cl,[eax+1]
  00503974  5f                    pop       edi
  00503975  880d341a5700          mov       [571A34h],cl
  0050397b  5e                    pop       esi
  0050397c  c70200000000          mov       dword [edx],0
  00503982  5b                    pop       ebx
  00503983  c3                    ret
  00503984  90                    nop
  00503985  90                    nop
  00503986  90                    nop
  00503987  90                    nop
  00503988  90                    nop
  00503989  90                    nop
  0050398a  90                    nop
  0050398b  90                    nop
  0050398c  90                    nop
  0050398d  90                    nop
  0050398e  90                    nop
  0050398f  90                    nop
  00503990  66a1f2195700          mov       ax,[5719F2h]
  00503996  83ec14                sub       esp,14h
  00503999  662b05ea195700        sub       ax,[5719EAh]
  005039a0  53                    push      ebx
  005039a1  55                    push      ebp
  005039a2  8b2de8195700          mov       ebp,[5719E8h]
  005039a8  56                    push      esi
  005039a9  8b35f0195700          mov       esi,[5719F0h]
  005039af  25ff0f0000            and       eax,0FFFh
  005039b4  2bf5                  sub       esi,ebp
  005039b6  57                    push      edi
  005039b7  81e6ff0f0000          and       esi,0FFFh
  005039bd  6681fe0008            cmp       si,800h
  005039c2  7d06                  jge       short 005039CAh
  005039c4  66c1fe02              sar       si,2
  005039c8  eb0f                  jmp       short 005039D9h

LAB_005039ca:
  005039ca  0fbfce                movsx     ecx,si
  005039cd  be00100000            mov       esi,1000h
  005039d2  2bf1                  sub       esi,ecx
  005039d4  c1fe02                sar       esi,2
  005039d7  f7de                  neg       esi

LAB_005039d9:
  005039d9  663d0008              cmp       ax,800h
  005039dd  7d06                  jge       short 005039E5h
  005039df  66c1f802              sar       ax,2
  005039e3  eb0f                  jmp       short 005039F4h

LAB_005039e5:
  005039e5  0fbfd0                movsx     edx,ax
  005039e8  b800100000            mov       eax,1000h
  005039ed  2bc2                  sub       eax,edx
  005039ef  c1f802                sar       eax,2
  005039f2  f7d8                  neg       eax

LAB_005039f4:
  005039f4  660105ea195700        add       [5719EAh],ax
  005039fb  8b3d181a5700          mov       edi,[571A18h]
  00503a01  8b15001a5700          mov       edx,[571A00h]
  00503a07  8b2d201a5700          mov       ebp,[571A20h]
  00503a0d  8b0dfc195700          mov       ecx,[5719FCh]
  00503a13  8b1d1c1a5700          mov       ebx,[571A1Ch]
  00503a19  6689442412            mov       [esp+12h],ax
  00503a1e  a1f8195700            mov       eax,[5719F8h]
  00503a23  2bc7                  sub       eax,edi
  00503a25  2bd5                  sub       edx,ebp
  00503a27  c1f802                sar       eax,2
  00503a2a  c1fa02                sar       edx,2
  00503a2d  2bcb                  sub       ecx,ebx
  00503a2f  03f8                  add       edi,eax
  00503a31  03ea                  add       ebp,edx
  00503a33  893d181a5700          mov       [571A18h],edi
  00503a39  8b3d281a5700          mov       edi,[571A28h]
  00503a3f  892d201a5700          mov       [571A20h],ebp
  00503a45  8b2d081a5700          mov       ebp,[571A08h]
  00503a4b  660135e8195700        add       [5719E8h],si
  00503a52  c1f902                sar       ecx,2
  00503a55  03d9                  add       ebx,ecx
  00503a57  2bfd                  sub       edi,ebp
  00503a59  8b2d0c1a5700          mov       ebp,[571A0Ch]
  00503a5f  891d1c1a5700          mov       [571A1Ch],ebx
  00503a65  8b1d2c1a5700          mov       ebx,[571A2Ch]
  00503a6b  2bdd                  sub       ebx,ebp
  00503a6d  8b2d301a5700          mov       ebp,[571A30h]
  00503a73  2b2d101a5700          sub       ebp,[571A10h]
  00503a79  c1ff02                sar       edi,2
  00503a7c  c1fd02                sar       ebp,2
  00503a7f  896c2420              mov       [esp+20h],ebp
  00503a83  8b2d081a5700          mov       ebp,[571A08h]
  00503a89  03ef                  add       ebp,edi
  00503a8b  892d081a5700          mov       [571A08h],ebp
  00503a91  8b2d0c1a5700          mov       ebp,[571A0Ch]
  00503a97  c1fb02                sar       ebx,2
  00503a9a  03eb                  add       ebp,ebx
  00503a9c  895c241c              mov       [esp+1Ch],ebx
  00503aa0  8b5c2420              mov       ebx,[esp+20h]
  00503aa4  892d0c1a5700          mov       [571A0Ch],ebp
  00503aaa  8b2d101a5700          mov       ebp,[571A10h]
  00503ab0  03eb                  add       ebp,ebx
  00503ab2  6685f6                test      si,si
  00503ab5  892d101a5700          mov       [571A10h],ebp
  00503abb  0fbff6                movsx     esi,si
  00503abe  7d02                  jge       short 00503AC2h
  00503ac0  f7de                  neg       esi

LAB_00503ac2:
  00503ac2  83fe04                cmp       esi,4
  00503ac5  7d6d                  jge       short 00503B34h
  00503ac7  668b742412            mov       si,[esp+12h]
  00503acc  6685f6                test      si,si
  00503acf  0fbff6                movsx     esi,si
  00503ad2  7d02                  jge       short 00503AD6h
  00503ad4  f7de                  neg       esi

LAB_00503ad6:
  00503ad6  83fe04                cmp       esi,4
  00503ad9  7d59                  jge       short 00503B34h
  00503adb  85c0                  test      eax,eax
  00503add  7d02                  jge       short 00503AE1h
  00503adf  f7d8                  neg       eax

LAB_00503ae1:
  00503ae1  83f804                cmp       eax,4
  00503ae4  7d4e                  jge       short 00503B34h
  00503ae6  85c9                  test      ecx,ecx
  00503ae8  7d02                  jge       short 00503AECh
  00503aea  f7d9                  neg       ecx

LAB_00503aec:
  00503aec  83f904                cmp       ecx,4
  00503aef  7d43                  jge       short 00503B34h
  00503af1  85d2                  test      edx,edx
  00503af3  7d02                  jge       short 00503AF7h
  00503af5  f7da                  neg       edx

LAB_00503af7:
  00503af7  83fa04                cmp       edx,4
  00503afa  7d38                  jge       short 00503B34h
  00503afc  85ff                  test      edi,edi
  00503afe  7d02                  jge       short 00503B02h
  00503b00  f7df                  neg       edi

LAB_00503b02:
  00503b02  83ff04                cmp       edi,4
  00503b05  7d2d                  jge       short 00503B34h
  00503b07  8b44241c              mov       eax,[esp+1Ch]
  00503b0b  85c0                  test      eax,eax
  00503b0d  7d02                  jge       short 00503B11h
  00503b0f  f7d8                  neg       eax

LAB_00503b11:
  00503b11  83f804                cmp       eax,4
  00503b14  7d1e                  jge       short 00503B34h
  00503b16  85db                  test      ebx,ebx
  00503b18  7d02                  jge       short 00503B1Ch
  00503b1a  f7db                  neg       ebx

LAB_00503b1c:
  00503b1c  8bc3                  mov       eax,ebx
  00503b1e  83f804                cmp       eax,4
  00503b21  7d11                  jge       short 00503B34h
  00503b23  8b442428              mov       eax,[esp+28h]
  00503b27  40                    inc       eax
  00503b28  89442428              mov       [esp+28h],eax
  00503b2c  5f                    pop       edi
  00503b2d  5e                    pop       esi
  00503b2e  5d                    pop       ebp
  00503b2f  5b                    pop       ebx
  00503b30  83c414                add       esp,14h
  00503b33  c3                    ret

LAB_00503b34:
  00503b34  8b44242c              mov       eax,[esp+2Ch]
  00503b38  5f                    pop       edi
  00503b39  5e                    pop       esi
  00503b3a  5d                    pop       ebp
  00503b3b  c70000000000          mov       dword [eax],0
  00503b41  8b44241c              mov       eax,[esp+1Ch]
  00503b45  5b                    pop       ebx
  00503b46  83c414                add       esp,14h
  00503b49  c3                    ret
  00503b4a  90                    nop
  00503b4b  90                    nop
  00503b4c  90                    nop
  00503b4d  90                    nop
  00503b4e  90                    nop
  00503b4f  90                    nop
  00503b50  a0341a5700            mov       al,[571A34h]
  00503b55  56                    push      esi
  00503b56  84c0                  test      al,al
  00503b58  0f84ae000000          je        near 00503C0Ch
  00503b5e  fec8                  dec       al
  00503b60  a2341a5700            mov       [571A34h],al
  00503b65  7507                  jne       short 00503B6Eh
  00503b67  8344240802            add       dword [esp+8],2
  00503b6c  eb0a                  jmp       short 00503B78h

LAB_00503b6e:
  00503b6e  8b44240c              mov       eax,[esp+0Ch]
  00503b72  c70000000000          mov       dword [eax],0

LAB_00503b78:
  00503b78  668b0da8195700        mov       cx,[5719A8h]
  00503b7f  668b15aa195700        mov       dx,[5719AAh]
  00503b86  66010de8195700        add       [5719E8h],cx
  00503b8d  660115ea195700        add       [5719EAh],dx
  00503b94  8b0dbc195700          mov       ecx,[5719BCh]
  00503b9a  8b151c1a5700          mov       edx,[571A1Ch]
  00503ba0  a1b8195700            mov       eax,[5719B8h]
  00503ba5  8b35181a5700          mov       esi,[571A18h]
  00503bab  03d1                  add       edx,ecx
  00503bad  8b0dcc195700          mov       ecx,[5719CCh]
  00503bb3  03f0                  add       esi,eax
  00503bb5  a1201a5700            mov       eax,[571A20h]
  00503bba  89151c1a5700          mov       [571A1Ch],edx
  00503bc0  8b15c0195700          mov       edx,[5719C0h]
  00503bc6  03c2                  add       eax,edx
  00503bc8  8b150c1a5700          mov       edx,[571A0Ch]
  00503bce  8935181a5700          mov       [571A18h],esi
  00503bd4  8b35081a5700          mov       esi,[571A08h]
  00503bda  a3201a5700            mov       [571A20h],eax
  00503bdf  a1c8195700            mov       eax,[5719C8h]
  00503be4  03d1                  add       edx,ecx
  00503be6  03f0                  add       esi,eax
  00503be8  a1101a5700            mov       eax,[571A10h]
  00503bed  89150c1a5700          mov       [571A0Ch],edx
  00503bf3  8b15d0195700          mov       edx,[5719D0h]
  00503bf9  8935081a5700          mov       [571A08h],esi
  00503bff  03c2                  add       eax,edx
  00503c01  a3101a5700            mov       [571A10h],eax
  00503c06  8b442408              mov       eax,[esp+8]
  00503c0a  5e                    pop       esi
  00503c0b  c3                    ret

LAB_00503c0c:
  00503c0c  8b442408              mov       eax,[esp+8]
  00503c10  8a4801                mov       cl,[eax+1]
  00503c13  a1f0195700            mov       eax,[5719F0h]
  00503c18  880d341a5700          mov       [571A34h],cl
  00503c1e  8b0de8195700          mov       ecx,[5719E8h]
  00503c24  2bc1                  sub       eax,ecx
  00503c26  668b0df2195700        mov       cx,[5719F2h]
  00503c2d  662b0dea195700        sub       cx,[5719EAh]
  00503c34  8b35341a5700          mov       esi,[571A34h]
  00503c3a  25ff0f0000            and       eax,0FFFh
  00503c3f  81e1ff0f0000          and       ecx,0FFFh
  00503c45  663d0008              cmp       ax,800h
  00503c49  7d0e                  jge       short 00503C59h
  00503c4b  0fbfc0                movsx     eax,ax
  00503c4e  99                    cdq
  00503c4f  81e6ff000000          and       esi,0FFh
  00503c55  f7fe                  idiv      esi
  00503c57  eb15                  jmp       short 00503C6Eh

LAB_00503c59:
  00503c59  0fbfd0                movsx     edx,ax
  00503c5c  b800100000            mov       eax,1000h
  00503c61  81e6ff000000          and       esi,0FFh
  00503c67  2bc2                  sub       eax,edx
  00503c69  99                    cdq
  00503c6a  f7fe                  idiv      esi
  00503c6c  f7d8                  neg       eax

LAB_00503c6e:
  00503c6e  6681f90008            cmp       cx,800h
  00503c73  66a3a8195700          mov       [5719A8h],ax
  00503c79  7d08                  jge       short 00503C83h
  00503c7b  0fbfc1                movsx     eax,cx
  00503c7e  99                    cdq
  00503c7f  f7fe                  idiv      esi
  00503c81  eb0f                  jmp       short 00503C92h

LAB_00503c83:
  00503c83  0fbfc9                movsx     ecx,cx
  00503c86  b800100000            mov       eax,1000h
  00503c8b  2bc1                  sub       eax,ecx
  00503c8d  99                    cdq
  00503c8e  f7fe                  idiv      esi
  00503c90  f7d8                  neg       eax

LAB_00503c92:
  00503c92  8b15181a5700          mov       edx,[571A18h]
  00503c98  66a3aa195700          mov       [5719AAh],ax
  00503c9e  a1f8195700            mov       eax,[5719F8h]
  00503ca3  8b0d1c1a5700          mov       ecx,[571A1Ch]
  00503ca9  2bc2                  sub       eax,edx
  00503cab  99                    cdq
  00503cac  f7fe                  idiv      esi
  00503cae  a3b8195700            mov       [5719B8h],eax
  00503cb3  a1fc195700            mov       eax,[5719FCh]
  00503cb8  2bc1                  sub       eax,ecx
  00503cba  8b0d081a5700          mov       ecx,[571A08h]
  00503cc0  99                    cdq
  00503cc1  f7fe                  idiv      esi
  00503cc3  8b15201a5700          mov       edx,[571A20h]
  00503cc9  a3bc195700            mov       [5719BCh],eax
  00503cce  a1001a5700            mov       eax,[571A00h]
  00503cd3  2bc2                  sub       eax,edx
  00503cd5  99                    cdq
  00503cd6  f7fe                  idiv      esi
  00503cd8  a3c0195700            mov       [5719C0h],eax
  00503cdd  a1281a5700            mov       eax,[571A28h]
  00503ce2  2bc1                  sub       eax,ecx
  00503ce4  8b0d101a5700          mov       ecx,[571A10h]
  00503cea  99                    cdq
  00503ceb  f7fe                  idiv      esi
  00503ced  8b150c1a5700          mov       edx,[571A0Ch]
  00503cf3  a3c8195700            mov       [5719C8h],eax
  00503cf8  a12c1a5700            mov       eax,[571A2Ch]
  00503cfd  2bc2                  sub       eax,edx
  00503cff  99                    cdq
  00503d00  f7fe                  idiv      esi
  00503d02  a3cc195700            mov       [5719CCh],eax
  00503d07  a1301a5700            mov       eax,[571A30h]
  00503d0c  2bc1                  sub       eax,ecx
  00503d0e  99                    cdq
  00503d0f  f7fe                  idiv      esi
  00503d11  8b54240c              mov       edx,[esp+0Ch]
  00503d15  5e                    pop       esi
  00503d16  a3d0195700            mov       [5719D0h],eax
  00503d1b  8b442404              mov       eax,[esp+4]
  00503d1f  c70200000000          mov       dword [edx],0
  00503d25  c3                    ret
  00503d26  90                    nop
  00503d27  90                    nop
  00503d28  90                    nop
  00503d29  90                    nop
  00503d2a  90                    nop
  00503d2b  90                    nop
  00503d2c  90                    nop
  00503d2d  90                    nop
  00503d2e  90                    nop
  00503d2f  90                    nop
  00503d30  a0341a5700            mov       al,[571A34h]
  00503d35  84c0                  test      al,al
  00503d37  7424                  je        short 00503D5Dh
  00503d39  fec8                  dec       al
  00503d3b  a2341a5700            mov       [571A34h],al
  00503d40  750c                  jne       short 00503D4Eh
  00503d42  8b442404              mov       eax,[esp+4]
  00503d46  83c002                add       eax,2
  00503d49  89442404              mov       [esp+4],eax
  00503d4d  c3                    ret

LAB_00503d4e:
  00503d4e  8b442408              mov       eax,[esp+8]
  00503d52  c70000000000          mov       dword [eax],0
  00503d58  8b442404              mov       eax,[esp+4]
  00503d5c  c3                    ret

LAB_00503d5d:
  00503d5d  8b4c2404              mov       ecx,[esp+4]
  00503d61  8bc1                  mov       eax,ecx
  00503d63  8a5101                mov       dl,[ecx+1]
  00503d66  8815341a5700          mov       [571A34h],dl
  00503d6c  c3                    ret
  00503d6d  90                    nop
  00503d6e  90                    nop
  00503d6f  90                    nop
  00503d70  8b442404              mov       eax,[esp+4]
  00503d74  40                    inc       eax
  00503d75  a3dc195700            mov       [5719DCh],eax
  00503d7a  c3                    ret
  00503d7b  90                    nop
  00503d7c  90                    nop
  00503d7d  90                    nop
  00503d7e  90                    nop
  00503d7f  90                    nop
  00503d80  a1dc195700            mov       eax,[5719DCh]
  00503d85  c3                    ret
  00503d86  90                    nop
  00503d87  90                    nop
  00503d88  90                    nop
  00503d89  90                    nop
  00503d8a  90                    nop
  00503d8b  90                    nop
  00503d8c  90                    nop
  00503d8d  90                    nop
  00503d8e  90                    nop
  00503d8f  90                    nop
  00503d90  8b442404              mov       eax,[esp+4]
  00503d94  8a4801                mov       cl,[eax+1]
  00503d97  83c002                add       eax,2
  00503d9a  880de0195700          mov       [5719E0h],cl
  00503da0  a3d4195700            mov       [5719D4h],eax
  00503da5  c3                    ret
  00503da6  90                    nop
  00503da7  90                    nop
  00503da8  90                    nop
  00503da9  90                    nop
  00503daa  90                    nop
  00503dab  90                    nop
  00503dac  90                    nop
  00503dad  90                    nop
  00503dae  90                    nop
  00503daf  90                    nop
  00503db0  a0e0195700            mov       al,[5719E0h]
  00503db5  fec8                  dec       al
  00503db7  a2e0195700            mov       [5719E0h],al
  00503dbc  7406                  je        short 00503DC4h
  00503dbe  a1d4195700            mov       eax,[5719D4h]
  00503dc3  c3                    ret

LAB_00503dc4:
  00503dc4  8b442404              mov       eax,[esp+4]
  00503dc8  40                    inc       eax
  00503dc9  c3                    ret
  00503dca  90                    nop
  00503dcb  90                    nop
  00503dcc  90                    nop
  00503dcd  90                    nop
  00503dce  90                    nop
  00503dcf  90                    nop
  00503dd0  a1b0195700            mov       eax,[5719B0h]
  00503dd5  8b4c2404              mov       ecx,[esp+4]
  00503dd9  a3e09a5e00            mov       [5E9AE0h],eax
  00503dde  8d4101                lea       eax,[ecx+1]
  00503de1  c3                    ret
  00503de2  90                    nop
  00503de3  90                    nop
  00503de4  90                    nop
  00503de5  90                    nop
  00503de6  90                    nop
  00503de7  90                    nop
  00503de8  90                    nop
  00503de9  90                    nop
  00503dea  90                    nop
  00503deb  90                    nop
  00503dec  90                    nop
  00503ded  90                    nop
  00503dee  90                    nop
  00503def  90                    nop
  00503df0  a1141a5700            mov       eax,[571A14h]
  00503df5  8b4c2404              mov       ecx,[esp+4]
  00503df9  a3bcde5e00            mov       [5EDEBCh],eax
  00503dfe  8d4101                lea       eax,[ecx+1]
  00503e01  c3                    ret
  00503e02  90                    nop
  00503e03  90                    nop
  00503e04  90                    nop
  00503e05  90                    nop
  00503e06  90                    nop
  00503e07  90                    nop
  00503e08  90                    nop
  00503e09  90                    nop
  00503e0a  90                    nop
  00503e0b  90                    nop
  00503e0c  90                    nop
  00503e0d  90                    nop
  00503e0e  90                    nop
  00503e0f  90                    nop
  00503e10  a1c4195700            mov       eax,[5719C4h]
  00503e15  668b5030              mov       dx,[eax+30h]
  00503e19  8d4830                lea       ecx,[eax+30h]
  00503e1c  668915f0195700        mov       [5719F0h],dx
  00503e23  668b5032              mov       dx,[eax+32h]
  00503e27  66f7da                neg       dx
  00503e2a  668915f2195700        mov       [5719F2h],dx
  00503e31  668b4034              mov       ax,[eax+34h]
  00503e35  890db0195700          mov       [5719B0h],ecx
  00503e3b  8b4c2404              mov       ecx,[esp+4]
  00503e3f  66a3f4195700          mov       [5719F4h],ax
  00503e45  8d4101                lea       eax,[ecx+1]
  00503e48  c3                    ret
  00503e49  90                    nop
  00503e4a  90                    nop
  00503e4b  90                    nop
  00503e4c  90                    nop
  00503e4d  90                    nop
  00503e4e  90                    nop
  00503e4f  90                    nop
  00503e50  a1c4195700            mov       eax,[5719C4h]
  00503e55  8b501c                mov       edx,[eax+1Ch]
  00503e58  8d481c                lea       ecx,[eax+1Ch]
  00503e5b  8915f8195700          mov       [5719F8h],edx
  00503e61  8b5020                mov       edx,[eax+20h]
  00503e64  8915fc195700          mov       [5719FCh],edx
  00503e6a  8b4024                mov       eax,[eax+24h]
  00503e6d  890d141a5700          mov       [571A14h],ecx
  00503e73  8b4c2404              mov       ecx,[esp+4]
  00503e77  a3001a5700            mov       [571A00h],eax
  00503e7c  8d4101                lea       eax,[ecx+1]
  00503e7f  c3                    ret
  00503e80  8b4c2404              mov       ecx,[esp+4]
  00503e84  33c0                  xor       eax,eax
  00503e86  56                    push      esi
  00503e87  8a4101                mov       al,[ecx+1]
  00503e8a  83e800                sub       eax,0
  00503e8d  7432                  je        short 00503EC1h
  00503e8f  48                    dec       eax
  00503e90  7419                  je        short 00503EABh
  00503e92  48                    dec       eax
  00503e93  7542                  jne       short 00503ED7h
  00503e95  33c0                  xor       eax,eax
  00503e97  8a4102                mov       al,[ecx+2]
  00503e9a  8d1480                lea       edx,[eax+eax*4]
  00503e9d  c1e203                shl       edx,3
  00503ea0  2bd0                  sub       edx,eax
  00503ea2  8d0495e0a55f00        lea       eax,[edx*4+5FA5E0h]
  00503ea9  eb30                  jmp       short 00503EDBh

LAB_00503eab:
  00503eab  33c0                  xor       eax,eax
  00503ead  8a4102                mov       al,[ecx+2]
  00503eb0  8d1480                lea       edx,[eax+eax*4]
  00503eb3  c1e203                shl       edx,3
  00503eb6  2bd0                  sub       edx,eax
  00503eb8  8d0495a0ca5e00        lea       eax,[edx*4+5ECAA0h]
  00503ebf  eb1a                  jmp       short 00503EDBh

LAB_00503ec1:
  00503ec1  33c0                  xor       eax,eax
  00503ec3  8a4102                mov       al,[ecx+2]
  00503ec6  8d1480                lea       edx,[eax+eax*4]
  00503ec9  c1e203                shl       edx,3
  00503ecc  2bd0                  sub       edx,eax
  00503ece  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00503ed5  eb04                  jmp       short 00503EDBh

LAB_00503ed7:
  00503ed7  8b442408              mov       eax,[esp+8]

LAB_00503edb:
  00503edb  668b7030              mov       si,[eax+30h]
  00503edf  8d5030                lea       edx,[eax+30h]
  00503ee2  668935f0195700        mov       [5719F0h],si
  00503ee9  668b7032              mov       si,[eax+32h]
  00503eed  66f7de                neg       si
  00503ef0  668935f2195700        mov       [5719F2h],si
  00503ef7  668b4034              mov       ax,[eax+34h]
  00503efb  66a3f4195700          mov       [5719F4h],ax
  00503f01  8915b0195700          mov       [5719B0h],edx
  00503f07  8d4103                lea       eax,[ecx+3]
  00503f0a  5e                    pop       esi
  00503f0b  c3                    ret
  00503f0c  90                    nop
  00503f0d  90                    nop
  00503f0e  90                    nop
  00503f0f  90                    nop
  00503f10  8b4c2404              mov       ecx,[esp+4]
  00503f14  33c0                  xor       eax,eax
  00503f16  56                    push      esi
  00503f17  8a4101                mov       al,[ecx+1]
  00503f1a  83e800                sub       eax,0
  00503f1d  7432                  je        short 00503F51h
  00503f1f  48                    dec       eax
  00503f20  7419                  je        short 00503F3Bh
  00503f22  48                    dec       eax
  00503f23  7542                  jne       short 00503F67h
  00503f25  33c0                  xor       eax,eax
  00503f27  8a4102                mov       al,[ecx+2]
  00503f2a  8d1480                lea       edx,[eax+eax*4]
  00503f2d  c1e203                shl       edx,3
  00503f30  2bd0                  sub       edx,eax
  00503f32  8d0495e0a55f00        lea       eax,[edx*4+5FA5E0h]
  00503f39  eb30                  jmp       short 00503F6Bh

LAB_00503f3b:
  00503f3b  33c0                  xor       eax,eax
  00503f3d  8a4102                mov       al,[ecx+2]
  00503f40  8d1480                lea       edx,[eax+eax*4]
  00503f43  c1e203                shl       edx,3
  00503f46  2bd0                  sub       edx,eax
  00503f48  8d0495a0ca5e00        lea       eax,[edx*4+5ECAA0h]
  00503f4f  eb1a                  jmp       short 00503F6Bh

LAB_00503f51:
  00503f51  33c0                  xor       eax,eax
  00503f53  8a4102                mov       al,[ecx+2]
  00503f56  8d1480                lea       edx,[eax+eax*4]
  00503f59  c1e203                shl       edx,3
  00503f5c  2bd0                  sub       edx,eax
  00503f5e  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00503f65  eb04                  jmp       short 00503F6Bh

LAB_00503f67:
  00503f67  8b442408              mov       eax,[esp+8]

LAB_00503f6b:
  00503f6b  8b701c                mov       esi,[eax+1Ch]
  00503f6e  8d501c                lea       edx,[eax+1Ch]
  00503f71  8935f8195700          mov       [5719F8h],esi
  00503f77  8b7020                mov       esi,[eax+20h]
  00503f7a  8935fc195700          mov       [5719FCh],esi
  00503f80  8b4024                mov       eax,[eax+24h]
  00503f83  a3001a5700            mov       [571A00h],eax
  00503f88  8915141a5700          mov       [571A14h],edx
  00503f8e  8d4103                lea       eax,[ecx+3]
  00503f91  5e                    pop       esi
  00503f92  c3                    ret
  00503f93  90                    nop
  00503f94  90                    nop
  00503f95  90                    nop
  00503f96  90                    nop
  00503f97  90                    nop
  00503f98  90                    nop
  00503f99  90                    nop
  00503f9a  90                    nop
  00503f9b  90                    nop
  00503f9c  90                    nop
  00503f9d  90                    nop
  00503f9e  90                    nop
  00503f9f  90                    nop
  00503fa0  8b442404              mov       eax,[esp+4]
  00503fa4  33c9                  xor       ecx,ecx
  00503fa6  33d2                  xor       edx,edx
  00503fa8  8a4801                mov       cl,[eax+1]
  00503fab  8a6802                mov       ch,[eax+2]
  00503fae  83c005                add       eax,5
  00503fb1  66890df0195700        mov       [5719F0h],cx
  00503fb8  8a50fe                mov       dl,[eax-2]
  00503fbb  8a70ff                mov       dh,[eax-1]
  00503fbe  668915f2195700        mov       [5719F2h],dx
  00503fc5  c3                    ret
  00503fc6  90                    nop
  00503fc7  90                    nop
  00503fc8  90                    nop
  00503fc9  90                    nop
  00503fca  90                    nop
  00503fcb  90                    nop
  00503fcc  90                    nop
  00503fcd  90                    nop
  00503fce  90                    nop
  00503fcf  90                    nop
  00503fd0  8b442404              mov       eax,[esp+4]
  00503fd4  33d2                  xor       edx,edx
  00503fd6  53                    push      ebx
  00503fd7  56                    push      esi
  00503fd8  8a5005                mov       dl,[eax+5]
  00503fdb  33db                  xor       ebx,ebx
  00503fdd  8a7006                mov       dh,[eax+6]
  00503fe0  8a5801                mov       bl,[eax+1]
  00503fe3  8bf2                  mov       esi,edx
  00503fe5  33d2                  xor       edx,edx
  00503fe7  8a7002                mov       dh,[eax+2]
  00503fea  33c9                  xor       ecx,ecx
  00503fec  8a4803                mov       cl,[eax+3]
  00503fef  8a6804                mov       ch,[eax+4]
  00503ff2  83c007                add       eax,7
  00503ff5  0fbfd2                movsx     edx,dx
  00503ff8  0bd3                  or        edx,ebx
  00503ffa  c1e20c                shl       edx,0Ch
  00503ffd  8915f8195700          mov       [5719F8h],edx
  00504003  0fbfc9                movsx     ecx,cx
  00504006  0fbfd6                movsx     edx,si
  00504009  c1e10c                shl       ecx,0Ch
  0050400c  c1e20c                shl       edx,0Ch
  0050400f  5e                    pop       esi
  00504010  890dfc195700          mov       [5719FCh],ecx
  00504016  8915001a5700          mov       [571A00h],edx
  0050401c  5b                    pop       ebx
  0050401d  c3                    ret
  0050401e  90                    nop
  0050401f  90                    nop
  00504020  8b442404              mov       eax,[esp+4]
  00504024  33d2                  xor       edx,edx
  00504026  53                    push      ebx
  00504027  56                    push      esi
  00504028  8a5005                mov       dl,[eax+5]
  0050402b  33db                  xor       ebx,ebx
  0050402d  8a7006                mov       dh,[eax+6]
  00504030  8a5801                mov       bl,[eax+1]
  00504033  8bf2                  mov       esi,edx
  00504035  33d2                  xor       edx,edx
  00504037  8a7002                mov       dh,[eax+2]
  0050403a  33c9                  xor       ecx,ecx
  0050403c  8a4803                mov       cl,[eax+3]
  0050403f  8a6804                mov       ch,[eax+4]
  00504042  83c007                add       eax,7
  00504045  0fbfd2                movsx     edx,dx
  00504048  0bd3                  or        edx,ebx
  0050404a  8915281a5700          mov       [571A28h],edx
  00504050  0fbfc9                movsx     ecx,cx
  00504053  0fbfd6                movsx     edx,si
  00504056  5e                    pop       esi
  00504057  890d2c1a5700          mov       [571A2Ch],ecx
  0050405d  8915301a5700          mov       [571A30h],edx
  00504063  5b                    pop       ebx
  00504064  c3                    ret
  00504065  90                    nop
  00504066  90                    nop
  00504067  90                    nop
  00504068  90                    nop
  00504069  90                    nop
  0050406a  90                    nop
  0050406b  90                    nop
  0050406c  90                    nop
  0050406d  90                    nop
  0050406e  90                    nop
  0050406f  90                    nop
  00504070  8b442404              mov       eax,[esp+4]
  00504074  33c9                  xor       ecx,ecx
  00504076  33d2                  xor       edx,edx
  00504078  8a4801                mov       cl,[eax+1]
  0050407b  8a6802                mov       ch,[eax+2]
  0050407e  83c005                add       eax,5
  00504081  66010df0195700        add       [5719F0h],cx
  00504088  8a50fe                mov       dl,[eax-2]
  0050408b  8a70ff                mov       dh,[eax-1]
  0050408e  660115f2195700        add       [5719F2h],dx
  00504095  c3                    ret
  00504096  90                    nop
  00504097  90                    nop
  00504098  90                    nop
  00504099  90                    nop
  0050409a  90                    nop
  0050409b  90                    nop
  0050409c  90                    nop
  0050409d  90                    nop
  0050409e  90                    nop
  0050409f  90                    nop
  005040a0  8b442404              mov       eax,[esp+4]
  005040a4  33d2                  xor       edx,edx
  005040a6  53                    push      ebx
  005040a7  56                    push      esi
  005040a8  8a5005                mov       dl,[eax+5]
  005040ab  33db                  xor       ebx,ebx
  005040ad  8a7006                mov       dh,[eax+6]
  005040b0  8a5801                mov       bl,[eax+1]
  005040b3  8bf2                  mov       esi,edx
  005040b5  33d2                  xor       edx,edx
  005040b7  8a7002                mov       dh,[eax+2]
  005040ba  33c9                  xor       ecx,ecx
  005040bc  8a4803                mov       cl,[eax+3]
  005040bf  8a6804                mov       ch,[eax+4]
  005040c2  83c007                add       eax,7
  005040c5  0fbfd2                movsx     edx,dx
  005040c8  0bd3                  or        edx,ebx
  005040ca  8b1df8195700          mov       ebx,[5719F8h]
  005040d0  c1e20c                shl       edx,0Ch
  005040d3  0fbfc9                movsx     ecx,cx
  005040d6  03da                  add       ebx,edx
  005040d8  891df8195700          mov       [5719F8h],ebx
  005040de  8b1dfc195700          mov       ebx,[5719FCh]
  005040e4  0fbfd6                movsx     edx,si
  005040e7  c1e10c                shl       ecx,0Ch
  005040ea  03d9                  add       ebx,ecx
  005040ec  8b0d001a5700          mov       ecx,[571A00h]
  005040f2  c1e20c                shl       edx,0Ch
  005040f5  03ca                  add       ecx,edx
  005040f7  891dfc195700          mov       [5719FCh],ebx
  005040fd  5e                    pop       esi
  005040fe  890d001a5700          mov       [571A00h],ecx
  00504104  5b                    pop       ebx
  00504105  c3                    ret
  00504106  90                    nop
  00504107  90                    nop
  00504108  90                    nop
  00504109  90                    nop
  0050410a  90                    nop
  0050410b  90                    nop
  0050410c  90                    nop
  0050410d  90                    nop
  0050410e  90                    nop
  0050410f  90                    nop
  00504110  8b442404              mov       eax,[esp+4]
  00504114  33d2                  xor       edx,edx
  00504116  53                    push      ebx
  00504117  33db                  xor       ebx,ebx
  00504119  8a5005                mov       dl,[eax+5]
  0050411c  8a7802                mov       bh,[eax+2]
  0050411f  8a7006                mov       dh,[eax+6]
  00504122  56                    push      esi
  00504123  57                    push      edi
  00504124  8bf2                  mov       esi,edx
  00504126  33c9                  xor       ecx,ecx
  00504128  33d2                  xor       edx,edx
  0050412a  8a4803                mov       cl,[eax+3]
  0050412d  8a5001                mov       dl,[eax+1]
  00504130  8a6804                mov       ch,[eax+4]
  00504133  83c007                add       eax,7
  00504136  0fbffb                movsx     edi,bx
  00504139  0bd7                  or        edx,edi
  0050413b  8b3d281a5700          mov       edi,[571A28h]
  00504141  0fbfc9                movsx     ecx,cx
  00504144  03fa                  add       edi,edx
  00504146  8b152c1a5700          mov       edx,[571A2Ch]
  0050414c  03d1                  add       edx,ecx
  0050414e  8b0d301a5700          mov       ecx,[571A30h]
  00504154  89152c1a5700          mov       [571A2Ch],edx
  0050415a  893d281a5700          mov       [571A28h],edi
  00504160  0fbfd6                movsx     edx,si
  00504163  03ca                  add       ecx,edx
  00504165  5f                    pop       edi
  00504166  5e                    pop       esi
  00504167  890d301a5700          mov       [571A30h],ecx
  0050416d  5b                    pop       ebx
  0050416e  c3                    ret
  0050416f  90                    nop
  00504170  66a150465600          mov       ax,[564650h]
  00504176  668b0d52465600        mov       cx,[564652h]
  0050417d  668b1554465600        mov       dx,[564654h]
  00504184  66a3f0195700          mov       [5719F0h],ax
  0050418a  8b442404              mov       eax,[esp+4]
  0050418e  66890df2195700        mov       [5719F2h],cx
  00504195  668915f4195700        mov       [5719F4h],dx
  0050419c  40                    inc       eax
  0050419d  c3                    ret
  0050419e  90                    nop
  0050419f  90                    nop
  005041a0  a168e05d00            mov       eax,[5DE068h]
  005041a5  8b0d6ce05d00          mov       ecx,[5DE06Ch]
  005041ab  8b1570e05d00          mov       edx,[5DE070h]
  005041b1  a3f8195700            mov       [5719F8h],eax
  005041b6  8b442404              mov       eax,[esp+4]
  005041ba  890dfc195700          mov       [5719FCh],ecx
  005041c0  8915001a5700          mov       [571A00h],edx
  005041c6  40                    inc       eax
  005041c7  c3                    ret
  005041c8  90                    nop
  005041c9  90                    nop
  005041ca  90                    nop
  005041cb  90                    nop
  005041cc  90                    nop
  005041cd  90                    nop
  005041ce  90                    nop
  005041cf  90                    nop
  005041d0  a178e05d00            mov       eax,[5DE078h]
  005041d5  8b0d7ce05d00          mov       ecx,[5DE07Ch]
  005041db  8b1580e05d00          mov       edx,[5DE080h]
  005041e1  a3281a5700            mov       [571A28h],eax
  005041e6  8b442404              mov       eax,[esp+4]
  005041ea  890d2c1a5700          mov       [571A2Ch],ecx
  005041f0  8915301a5700          mov       [571A30h],edx
  005041f6  40                    inc       eax
  005041f7  c3                    ret
  005041f8  90                    nop
  005041f9  90                    nop
  005041fa  90                    nop
  005041fb  90                    nop
  005041fc  90                    nop
  005041fd  90                    nop
  005041fe  90                    nop
  005041ff  90                    nop
  00504200  66a1e8195700          mov       ax,[5719E8h]
  00504206  668b0dea195700        mov       cx,[5719EAh]
  0050420d  668b15ec195700        mov       dx,[5719ECh]
  00504214  66a350465600          mov       [564650h],ax
  0050421a  a1081a5700            mov       eax,[571A08h]
  0050421f  66890d52465600        mov       [564652h],cx
  00504226  8b0d88e05d00          mov       ecx,[5DE088h]
  0050422c  66891554465600        mov       [564654h],dx
  00504233  8b158ce05d00          mov       edx,[5DE08Ch]
  00504239  2bc1                  sub       eax,ecx
  0050423b  8b0d0c1a5700          mov       ecx,[571A0Ch]
  00504241  a358465600            mov       [564658h],eax
  00504246  a190e05d00            mov       eax,[5DE090h]
  0050424b  2bca                  sub       ecx,edx
  0050424d  8b15101a5700          mov       edx,[571A10h]
  00504253  890d5c465600          mov       [56465Ch],ecx
  00504259  2bd0                  sub       edx,eax
  0050425b  8b442404              mov       eax,[esp+4]
  0050425f  891560465600          mov       [564660h],edx
  00504265  40                    inc       eax
  00504266  c3                    ret
  00504267  90                    nop
  00504268  90                    nop
  00504269  90                    nop
  0050426a  90                    nop
  0050426b  90                    nop
  0050426c  90                    nop
  0050426d  90                    nop
  0050426e  90                    nop
  0050426f  90                    nop
  00504270  8b442404              mov       eax,[esp+4]
  00504274  8a15784b6000          mov       dl,[604B78h]
  0050427a  8a4801                mov       cl,[eax+1]
  0050427d  83c002                add       eax,2
  00504280  0ad1                  or        dl,cl
  00504282  8815784b6000          mov       [604B78h],dl
  00504288  c3                    ret
  00504289  90                    nop
  0050428a  90                    nop
  0050428b  90                    nop
  0050428c  90                    nop
  0050428d  90                    nop
  0050428e  90                    nop
  0050428f  90                    nop
  00504290  8b442404              mov       eax,[esp+4]
  00504294  8a15784b6000          mov       dl,[604B78h]
  0050429a  8a4801                mov       cl,[eax+1]
  0050429d  83c002                add       eax,2
  005042a0  f6d1                  not       cl
  005042a2  22d1                  and       dl,cl
  005042a4  8815784b6000          mov       [604B78h],dl
  005042aa  c3                    ret
  005042ab  90                    nop
  005042ac  90                    nop
  005042ad  90                    nop
  005042ae  90                    nop
  005042af  90                    nop
  005042b0  53                    push      ebx
  005042b1  56                    push      esi
  005042b2  8b74240c              mov       esi,[esp+0Ch]
  005042b6  33c0                  xor       eax,eax
  005042b8  57                    push      edi
  005042b9  33db                  xor       ebx,ebx
  005042bb  8a4601                mov       al,[esi+1]
  005042be  8a5e03                mov       bl,[esi+3]
  005042c1  8a6602                mov       ah,[esi+2]
  005042c4  8a7e04                mov       bh,[esi+4]
  005042c7  8bf8                  mov       edi,eax
  005042c9  6685ff                test      di,di
  005042cc  7505                  jne       short 005042D3h
  005042ce  bf01000000            mov       edi,1

LAB_005042d3:
  005042d3  6685db                test      bx,bx
  005042d6  7505                  jne       short 005042DDh
  005042d8  bb01000000            mov       ebx,1

LAB_005042dd:
  005042dd  e85e2e0400            call      00547140h
  005042e2  0fbfcf                movsx     ecx,di
  005042e5  99                    cdq
  005042e6  d1e1                  shl       ecx,1
  005042e8  f7f9                  idiv      ecx
  005042ea  2bd7                  sub       edx,edi
  005042ec  660115f0195700        add       [5719F0h],dx
  005042f3  e8482e0400            call      00547140h
  005042f8  0fbfcb                movsx     ecx,bx
  005042fb  99                    cdq
  005042fc  d1e1                  shl       ecx,1
  005042fe  f7f9                  idiv      ecx
  00504300  8d4605                lea       eax,[esi+5]
  00504303  5f                    pop       edi
  00504304  5e                    pop       esi
  00504305  2bd3                  sub       edx,ebx
  00504307  5b                    pop       ebx
  00504308  660115f2195700        add       [5719F2h],dx
  0050430f  c3                    ret
  00504310  53                    push      ebx
  00504311  55                    push      ebp
  00504312  56                    push      esi
  00504313  8b742410              mov       esi,[esp+10h]
  00504317  33c0                  xor       eax,eax
  00504319  33c9                  xor       ecx,ecx
  0050431b  8a4601                mov       al,[esi+1]
  0050431e  8a4e03                mov       cl,[esi+3]
  00504321  8a6602                mov       ah,[esi+2]
  00504324  8a6e04                mov       ch,[esi+4]
  00504327  33db                  xor       ebx,ebx
  00504329  57                    push      edi
  0050432a  8a5e05                mov       bl,[esi+5]
  0050432d  8be9                  mov       ebp,ecx
  0050432f  8a7e06                mov       bh,[esi+6]
  00504332  6685c0                test      ax,ax
  00504335  7505                  jne       short 0050433Ch
  00504337  b801000000            mov       eax,1

LAB_0050433c:
  0050433c  6685ed                test      bp,bp
  0050433f  7505                  jne       short 00504346h
  00504341  bd01000000            mov       ebp,1

LAB_00504346:
  00504346  6685db                test      bx,bx
  00504349  7505                  jne       short 00504350h
  0050434b  bb01000000            mov       ebx,1

LAB_00504350:
  00504350  0fbff8                movsx     edi,ax
  00504353  e8e82d0400            call      00547140h
  00504358  99                    cdq
  00504359  8d0c3f                lea       ecx,[edi+edi]
  0050435c  f7f9                  idiv      ecx
  0050435e  2bd7                  sub       edx,edi
  00504360  8b3df8195700          mov       edi,[5719F8h]
  00504366  c1e20c                shl       edx,0Ch
  00504369  03fa                  add       edi,edx
  0050436b  893df8195700          mov       [5719F8h],edi
  00504371  0fbffd                movsx     edi,bp
  00504374  e8c72d0400            call      00547140h
  00504379  99                    cdq
  0050437a  8d0c3f                lea       ecx,[edi+edi]
  0050437d  8b2dfc195700          mov       ebp,[5719FCh]
  00504383  f7f9                  idiv      ecx
  00504385  2bd7                  sub       edx,edi
  00504387  c1e20c                shl       edx,0Ch
  0050438a  03ea                  add       ebp,edx
  0050438c  892dfc195700          mov       [5719FCh],ebp
  00504392  0fbffb                movsx     edi,bx
  00504395  e8a62d0400            call      00547140h
  0050439a  99                    cdq
  0050439b  8d0c3f                lea       ecx,[edi+edi]
  0050439e  f7f9                  idiv      ecx
  005043a0  a1001a5700            mov       eax,[571A00h]
  005043a5  2bd7                  sub       edx,edi
  005043a7  5f                    pop       edi
  005043a8  c1e20c                shl       edx,0Ch
  005043ab  03c2                  add       eax,edx
  005043ad  a3001a5700            mov       [571A00h],eax
  005043b2  8d4607                lea       eax,[esi+7]
  005043b5  5e                    pop       esi
  005043b6  5d                    pop       ebp
  005043b7  5b                    pop       ebx
  005043b8  c3                    ret
  005043b9  90                    nop
  005043ba  90                    nop
  005043bb  90                    nop
  005043bc  90                    nop
  005043bd  90                    nop
  005043be  90                    nop
  005043bf  90                    nop
  005043c0  53                    push      ebx
  005043c1  55                    push      ebp
  005043c2  56                    push      esi
  005043c3  8b742410              mov       esi,[esp+10h]
  005043c7  33c0                  xor       eax,eax
  005043c9  33c9                  xor       ecx,ecx
  005043cb  8a6602                mov       ah,[esi+2]
  005043ce  8a6e04                mov       ch,[esi+4]
  005043d1  8a4601                mov       al,[esi+1]
  005043d4  8a4e03                mov       cl,[esi+3]
  005043d7  33db                  xor       ebx,ebx
  005043d9  57                    push      edi
  005043da  8a7e06                mov       bh,[esi+6]
  005043dd  8be9                  mov       ebp,ecx
  005043df  8a5e05                mov       bl,[esi+5]
  005043e2  6685c0                test      ax,ax
  005043e5  7505                  jne       short 005043ECh
  005043e7  b801000000            mov       eax,1

LAB_005043ec:
  005043ec  6685ed                test      bp,bp
  005043ef  7505                  jne       short 005043F6h
  005043f1  bd01000000            mov       ebp,1

LAB_005043f6:
  005043f6  6685db                test      bx,bx
  005043f9  7505                  jne       short 00504400h
  005043fb  bb01000000            mov       ebx,1

LAB_00504400:
  00504400  0fbff8                movsx     edi,ax
  00504403  e8382d0400            call      00547140h
  00504408  99                    cdq
  00504409  8d0c3f                lea       ecx,[edi+edi]
  0050440c  f7f9                  idiv      ecx
  0050440e  a1281a5700            mov       eax,[571A28h]
  00504413  2bd7                  sub       edx,edi
  00504415  03c2                  add       eax,edx
  00504417  a3281a5700            mov       [571A28h],eax
  0050441c  0fbffd                movsx     edi,bp
  0050441f  e81c2d0400            call      00547140h
  00504424  99                    cdq
  00504425  8d0c3f                lea       ecx,[edi+edi]
  00504428  f7f9                  idiv      ecx
  0050442a  2bd7                  sub       edx,edi
  0050442c  8b3d2c1a5700          mov       edi,[571A2Ch]
  00504432  03fa                  add       edi,edx
  00504434  893d2c1a5700          mov       [571A2Ch],edi
  0050443a  0fbffb                movsx     edi,bx
  0050443d  e8fe2c0400            call      00547140h
  00504442  99                    cdq
  00504443  8d0c3f                lea       ecx,[edi+edi]
  00504446  f7f9                  idiv      ecx
  00504448  a1301a5700            mov       eax,[571A30h]
  0050444d  2bd7                  sub       edx,edi
  0050444f  5f                    pop       edi
  00504450  03c2                  add       eax,edx
  00504452  a3301a5700            mov       [571A30h],eax
  00504457  8d4607                lea       eax,[esi+7]
  0050445a  5e                    pop       esi
  0050445b  5d                    pop       ebp
  0050445c  5b                    pop       ebx
  0050445d  c3                    ret
  0050445e  90                    nop
  0050445f  90                    nop
  00504460  e8ed150400            call      00545A52h
  00504465  6810465000            push      504610h
  0050446a  6800100000            push      1000h
  0050446f  6a04                  push      4
  00504471  68010000f4            push      0F4000001h
  00504476  e8d1150400            call      00545A4Ch
  0050447b  6820465000            push      504620h
  00504480  6800100000            push      1000h
  00504485  6800800000            push      8000h
  0050448a  68010000f4            push      0F4000001h
  0050448f  a3684b6000            mov       [604B68h],eax
  00504494  e8b3150400            call      00545A4Ch
  00504499  6830465000            push      504630h
  0050449e  6800100000            push      1000h
  005044a3  6800010000            push      100h
  005044a8  68010000f4            push      0F4000001h
  005044ad  a36c4b6000            mov       [604B6Ch],eax
  005044b2  e895150400            call      00545A4Ch
  005044b7  6840465000            push      504640h
  005044bc  6800100000            push      1000h
  005044c1  6800200000            push      2000h
  005044c6  68010000f4            push      0F4000001h
  005044cb  a3704b6000            mov       [604B70h],eax
  005044d0  e877150400            call      00545A4Ch
  005044d5  6850465000            push      504650h
  005044da  6800100000            push      1000h
  005044df  6a04                  push      4
  005044e1  68110000f0            push      0F0000011h
  005044e6  a3744b6000            mov       [604B74h],eax
  005044eb  e85c150400            call      00545A4Ch
  005044f0  6860465000            push      504660h
  005044f5  6800100000            push      1000h
  005044fa  6800800000            push      8000h
  005044ff  68110000f0            push      0F0000011h
  00504504  a3584b6000            mov       [604B58h],eax
  00504509  e83e150400            call      00545A4Ch
  0050450e  6870465000            push      504670h
  00504513  6800100000            push      1000h
  00504518  6800010000            push      100h
  0050451d  68110000f0            push      0F0000011h
  00504522  a35c4b6000            mov       [604B5Ch],eax
  00504527  e820150400            call      00545A4Ch
  0050452c  6880465000            push      504680h
  00504531  6800100000            push      1000h
  00504536  6800200000            push      2000h
  0050453b  68110000f0            push      0F0000011h
  00504540  a3604b6000            mov       [604B60h],eax
  00504545  e802150400            call      00545A4Ch
  0050454a  a3644b6000            mov       [604B64h],eax
  0050454f  e8f2140400            call      00545A46h
  00504554  a1684b6000            mov       eax,[604B68h]
  00504559  50                    push      eax
  0050455a  e8e1140400            call      00545A40h
  0050455f  8b0d6c4b6000          mov       ecx,[604B6Ch]
  00504565  51                    push      ecx
  00504566  e8d5140400            call      00545A40h
  0050456b  8b15704b6000          mov       edx,[604B70h]
  00504571  52                    push      edx
  00504572  e8c9140400            call      00545A40h
  00504577  a1744b6000            mov       eax,[604B74h]
  0050457c  50                    push      eax
  0050457d  e8be140400            call      00545A40h
  00504582  8b0d584b6000          mov       ecx,[604B58h]
  00504588  51                    push      ecx
  00504589  e8b2140400            call      00545A40h
  0050458e  8b155c4b6000          mov       edx,[604B5Ch]
  00504594  52                    push      edx
  00504595  e8a6140400            call      00545A40h
  0050459a  a1604b6000            mov       eax,[604B60h]
  0050459f  50                    push      eax
  005045a0  e89b140400            call      00545A40h
  005045a5  8b0d644b6000          mov       ecx,[604B64h]
  005045ab  51                    push      ecx
  005045ac  e88f140400            call      00545A40h
  005045b1  8b155cb85d00          mov       edx,[5DB85Ch]
  005045b7  6a00                  push      0
  005045b9  6a00                  push      0
  005045bb  68504b6000            push      604B50h
  005045c0  52                    push      edx
  005045c1  6a00                  push      0
  005045c3  e858eb0100            call      00523120h
  005045c8  83c414                add       esp,14h
  005045cb  a3544b6000            mov       [604B54h],eax
  005045d0  50                    push      eax
  005045d1  e84aec0100            call      00523220h
  005045d6  83c404                add       esp,4
  005045d9  a3381a5700            mov       [571A38h],eax
  005045de  6a00                  push      0
  005045e0  e855140400            call      00545A3Ah
  005045e5  e84a140400            call      00545A34h
  005045ea  e83f140400            call      00545A2Eh
  005045ef  6a01                  push      1
  005045f1  e81a050000            call      00504B10h
  005045f6  83c404                add       esp,4
  005045f9  c3                    ret
  005045fa  90                    nop
  005045fb  90                    nop
  005045fc  90                    nop
  005045fd  90                    nop
  005045fe  90                    nop
  005045ff  90                    nop
  00504600  a1544b6000            mov       eax,[604B54h]
  00504605  50                    push      eax
  00504606  e8f5eb0100            call      00523200h
  0050460b  83c404                add       esp,4
  0050460e  c3                    ret
  0050460f  90                    nop
  00504610  c705b04a600000000000  mov       dword [604AB0h],0
  0050461a  c3                    ret
  0050461b  90                    nop
  0050461c  90                    nop
  0050461d  90                    nop
  0050461e  90                    nop
  0050461f  90                    nop
  00504620  c705b04a600001000000  mov       dword [604AB0h],1
  0050462a  c3                    ret
  0050462b  90                    nop
  0050462c  90                    nop
  0050462d  90                    nop
  0050462e  90                    nop
  0050462f  90                    nop
  00504630  c705b04a600002000000  mov       dword [604AB0h],2
  0050463a  c3                    ret
  0050463b  90                    nop
  0050463c  90                    nop
  0050463d  90                    nop
  0050463e  90                    nop
  0050463f  90                    nop
  00504640  c705b04a600003000000  mov       dword [604AB0h],3
  0050464a  c3                    ret
  0050464b  90                    nop
  0050464c  90                    nop
  0050464d  90                    nop
  0050464e  90                    nop
  0050464f  90                    nop
  00504650  c705004a600000000000  mov       dword [604A00h],0
  0050465a  c3                    ret
  0050465b  90                    nop
  0050465c  90                    nop
  0050465d  90                    nop
  0050465e  90                    nop
  0050465f  90                    nop
  00504660  c705004a600001000000  mov       dword [604A00h],1
  0050466a  c3                    ret
  0050466b  90                    nop
  0050466c  90                    nop
  0050466d  90                    nop
  0050466e  90                    nop
  0050466f  90                    nop
  00504670  c705004a600002000000  mov       dword [604A00h],2
  0050467a  c3                    ret
  0050467b  90                    nop
  0050467c  90                    nop
  0050467d  90                    nop
  0050467e  90                    nop
  0050467f  90                    nop
  00504680  c705004a600003000000  mov       dword [604A00h],3
  0050468a  c3                    ret
  0050468b  90                    nop
  0050468c  90                    nop
  0050468d  90                    nop
  0050468e  90                    nop
  0050468f  90                    nop
  00504690  56                    push      esi
  00504691  8b742408              mov       esi,[esp+8]
  00504695  b804000000            mov       eax,4
  0050469a  56                    push      esi
  0050469b  a3b04a6000            mov       [604AB0h],eax
  005046a0  a3004a6000            mov       [604A00h],eax
  005046a5  e8ae130400            call      00545A58h
  005046aa  85c0                  test      eax,eax
  005046ac  750a                  jne       short 005046B8h

LAB_005046ae:
  005046ae  56                    push      esi
  005046af  e8a4130400            call      00545A58h
  005046b4  85c0                  test      eax,eax
  005046b6  74f6                  je        short 005046AEh

LAB_005046b8:
  005046b8  5e                    pop       esi
  005046b9  c3                    ret
  005046ba  90                    nop
  005046bb  90                    nop
  005046bc  90                    nop
  005046bd  90                    nop
  005046be  90                    nop
  005046bf  90                    nop
  005046c0  a1b04a6000            mov       eax,[604AB0h]
  005046c5  53                    push      ebx
  005046c6  56                    push      esi
  005046c7  83f804                cmp       eax,4
  005046ca  57                    push      edi
  005046cb  bb20a10700            mov       ebx,7A120h
  005046d0  754b                  jne       short 0050471Dh
  005046d2  8b7c2414              mov       edi,[esp+14h]
  005046d6  8b742410              mov       esi,[esp+10h]

LAB_005046da:
  005046da  85ff                  test      edi,edi
  005046dc  743a                  je        short 00504718h
  005046de  8bc6                  mov       eax,esi
  005046e0  c1f804                sar       eax,4
  005046e3  50                    push      eax
  005046e4  e875130400            call      00545A5Eh
  005046e9  a801                  test      al,1
  005046eb  740b                  je        short 005046F8h
  005046ed  a810                  test      al,10h
  005046ef  751e                  jne       short 0050470Fh
  005046f1  a820                  test      al,20h
  005046f3  7511                  jne       short 00504706h
  005046f5  4b                    dec       ebx
  005046f6  7417                  je        short 0050470Fh

LAB_005046f8:
  005046f8  a1b04a6000            mov       eax,[604AB0h]
  005046fd  83f804                cmp       eax,4
  00504700  74d8                  je        short 005046DAh
  00504702  5f                    pop       edi
  00504703  5e                    pop       esi
  00504704  5b                    pop       ebx
  00504705  c3                    ret

LAB_00504706:
  00504706  b801000000            mov       eax,1
  0050470b  5f                    pop       edi
  0050470c  5e                    pop       esi
  0050470d  5b                    pop       ebx
  0050470e  c3                    ret

LAB_0050470f:
  0050470f  b802000000            mov       eax,2
  00504714  5f                    pop       edi
  00504715  5e                    pop       esi
  00504716  5b                    pop       ebx
  00504717  c3                    ret

LAB_00504718:
  00504718  b804000000            mov       eax,4

LAB_0050471d:
  0050471d  5f                    pop       edi
  0050471e  5e                    pop       esi
  0050471f  5b                    pop       ebx
  00504720  c3                    ret
  00504721  90                    nop
  00504722  90                    nop
  00504723  90                    nop
  00504724  90                    nop
  00504725  90                    nop
  00504726  90                    nop
  00504727  90                    nop
  00504728  90                    nop
  00504729  90                    nop
  0050472a  90                    nop
  0050472b  90                    nop
  0050472c  90                    nop
  0050472d  90                    nop
  0050472e  90                    nop
  0050472f  90                    nop
  00504730  a1684b6000            mov       eax,[604B68h]
  00504735  50                    push      eax
  00504736  e829130400            call      00545A64h
  0050473b  8b0d6c4b6000          mov       ecx,[604B6Ch]
  00504741  51                    push      ecx
  00504742  e81d130400            call      00545A64h
  00504747  8b15704b6000          mov       edx,[604B70h]
  0050474d  52                    push      edx
  0050474e  e811130400            call      00545A64h
  00504753  a1744b6000            mov       eax,[604B74h]
  00504758  50                    push      eax
  00504759  e806130400            call      00545A64h
  0050475e  c705b04a600004000000  mov       dword [604AB0h],4
  00504768  c3                    ret
  00504769  90                    nop
  0050476a  90                    nop
  0050476b  90                    nop
  0050476c  90                    nop
  0050476d  90                    nop
  0050476e  90                    nop
  0050476f  90                    nop
  00504770  a1004a6000            mov       eax,[604A00h]
  00504775  53                    push      ebx
  00504776  56                    push      esi
  00504777  83f804                cmp       eax,4
  0050477a  57                    push      edi
  0050477b  bb20a10700            mov       ebx,7A120h
  00504780  754b                  jne       short 005047CDh
  00504782  8b7c2414              mov       edi,[esp+14h]
  00504786  8b742410              mov       esi,[esp+10h]

LAB_0050478a:
  0050478a  85ff                  test      edi,edi
  0050478c  743a                  je        short 005047C8h
  0050478e  8bc6                  mov       eax,esi
  00504790  c1f804                sar       eax,4
  00504793  50                    push      eax
  00504794  e8c5120400            call      00545A5Eh
  00504799  a801                  test      al,1
  0050479b  740b                  je        short 005047A8h
  0050479d  a810                  test      al,10h
  0050479f  751e                  jne       short 005047BFh
  005047a1  a820                  test      al,20h
  005047a3  7511                  jne       short 005047B6h
  005047a5  4b                    dec       ebx
  005047a6  7417                  je        short 005047BFh

LAB_005047a8:
  005047a8  a1004a6000            mov       eax,[604A00h]
  005047ad  83f804                cmp       eax,4
  005047b0  74d8                  je        short 0050478Ah
  005047b2  5f                    pop       edi
  005047b3  5e                    pop       esi
  005047b4  5b                    pop       ebx
  005047b5  c3                    ret

LAB_005047b6:
  005047b6  b801000000            mov       eax,1
  005047bb  5f                    pop       edi
  005047bc  5e                    pop       esi
  005047bd  5b                    pop       ebx
  005047be  c3                    ret

LAB_005047bf:
  005047bf  b802000000            mov       eax,2
  005047c4  5f                    pop       edi
  005047c5  5e                    pop       esi
  005047c6  5b                    pop       ebx
  005047c7  c3                    ret

LAB_005047c8:
  005047c8  b804000000            mov       eax,4

LAB_005047cd:
  005047cd  5f                    pop       edi
  005047ce  5e                    pop       esi
  005047cf  5b                    pop       ebx
  005047d0  c3                    ret
  005047d1  90                    nop
  005047d2  90                    nop
  005047d3  90                    nop
  005047d4  90                    nop
  005047d5  90                    nop
  005047d6  90                    nop
  005047d7  90                    nop
  005047d8  90                    nop
  005047d9  90                    nop
  005047da  90                    nop
  005047db  90                    nop
  005047dc  90                    nop
  005047dd  90                    nop
  005047de  90                    nop
  005047df  90                    nop
  005047e0  a1584b6000            mov       eax,[604B58h]
  005047e5  50                    push      eax
  005047e6  e879120400            call      00545A64h
  005047eb  8b0d5c4b6000          mov       ecx,[604B5Ch]
  005047f1  51                    push      ecx
  005047f2  e86d120400            call      00545A64h
  005047f7  8b15604b6000          mov       edx,[604B60h]
  005047fd  52                    push      edx
  005047fe  e861120400            call      00545A64h
  00504803  a1644b6000            mov       eax,[604B64h]
  00504808  50                    push      eax
  00504809  e856120400            call      00545A64h
  0050480e  c705004a600004000000  mov       dword [604A00h],4
  00504818  c3                    ret
  00504819  90                    nop
  0050481a  90                    nop
  0050481b  90                    nop
  0050481c  90                    nop
  0050481d  90                    nop
  0050481e  90                    nop
  0050481f  90                    nop
  00504820  56                    push      esi
  00504821  8b742408              mov       esi,[esp+8]
  00504825  57                    push      edi
  00504826  8b7c2410              mov       edi,[esp+10h]
  0050482a  85ff                  test      edi,edi
  0050482c  7409                  je        short 00504837h
  0050482e  56                    push      esi
  0050482f  e85cfeffff            call      00504690h
  00504834  83c404                add       esp,4

LAB_00504837:
  00504837  57                    push      edi
  00504838  56                    push      esi
  00504839  e882feffff            call      005046C0h
  0050483e  83c408                add       esp,8
  00504841  83f804                cmp       eax,4
  00504844  772b                  ja        short 00504871h
  00504846  ff2485c4485000        jmp       dword [eax*4+5048C4h]
  0050484d  e88effffff            call      005047E0h
  00504852  56                    push      esi
  00504853  e818120400            call      00545A70h
  00504858  85c0                  test      eax,eax
  0050485a  750a                  jne       short 00504866h

LAB_0050485c:
  0050485c  56                    push      esi
  0050485d  e80e120400            call      00545A70h
  00504862  85c0                  test      eax,eax
  00504864  74f6                  je        short 0050485Ch

LAB_00504866:
  00504866  6a01                  push      1
  00504868  56                    push      esi
  00504869  e802ffffff            call      00504770h
  0050486e  83c408                add       esp,8

LAB_00504871:
  00504871  e8bafeffff            call      00504730h
  00504876  56                    push      esi
  00504877  e8ee110400            call      00545A6Ah
  0050487c  85c0                  test      eax,eax
  0050487e  750a                  jne       short 0050488Ah

LAB_00504880:
  00504880  56                    push      esi
  00504881  e8e4110400            call      00545A6Ah
  00504886  85c0                  test      eax,eax
  00504888  74f6                  je        short 00504880h

LAB_0050488a:
  0050488a  57                    push      edi
  0050488b  56                    push      esi
  0050488c  e82ffeffff            call      005046C0h
  00504891  83c408                add       esp,8
  00504894  83f804                cmp       eax,4
  00504897  7725                  ja        short 005048BEh
  00504899  ff2485d8485000        jmp       dword [eax*4+5048D8h]
  005048a0  b801000000            mov       eax,1
  005048a5  5f                    pop       edi
  005048a6  5e                    pop       esi
  005048a7  c3                    ret
  005048a8  83c8ff                or        eax,0FFFFFFFFh
  005048ab  5f                    pop       edi
  005048ac  5e                    pop       esi
  005048ad  c3                    ret
  005048ae  b8feffffff            mov       eax,0FFFFFFFEh
  005048b3  5f                    pop       edi
  005048b4  5e                    pop       esi
  005048b5  c3                    ret
  005048b6  b8fdffffff            mov       eax,0FFFFFFFDh
  005048bb  5f                    pop       edi
  005048bc  5e                    pop       esi
  005048bd  c3                    ret

LAB_005048be:
  005048be  5f                    pop       edi
  005048bf  33c0                  xor       eax,eax
  005048c1  5e                    pop       esi
  005048c2  c3                    ret
  005048c3  90                    nop
  005048c4  7148                  jno       short 0050490Eh
  005048c6  50                    push      eax
  005048c7  00b6485000ae          add       [esi-51FFAFB8h],dh
  005048cd  48                    dec       eax
  005048ce  50                    push      eax
  005048cf  004d48                add       [ebp+48h],cl
  005048d2  50                    push      eax
  005048d3  00a0485000be          add       [eax-41FFAFB8h],ah
  005048d9  48                    dec       eax
  005048da  50                    push      eax
  005048db  00b6485000ae          add       [esi-51FFAFB8h],dh
  005048e1  48                    dec       eax
  005048e2  50                    push      eax
  005048e3  00a8485000a0          add       [eax-5FFFAFB8h],ch
  005048e9  48                    dec       eax
  005048ea  50                    push      eax
  005048eb  009090909083          add       [eax-7C6F6F70h],dl
  005048f1  ec                    in        al,dx
  005048f2  10a1544b6000          adc       [ecx+604B54h],ah
  005048f8  53                    push      ebx
  005048f9  55                    push      ebp
  005048fa  56                    push      esi
  005048fb  57                    push      edi
  005048fc  50                    push      eax
  005048fd  e8eee80100            call      005231F0h
  00504902  8b0d504b6000          mov       ecx,[604B50h]
  00504908  83c404                add       esp,4
  0050490b  68c04a6000            push      604AC0h
  00504910  51                    push      ecx
  00504911  e84aed0100            call      00523660h
  00504916  83c408                add       esp,8
  00504919  8944241c              mov       [esp+1Ch],eax
  0050491d  ba0c4b6000            mov       edx,604B0Ch
  00504922  c744241402000000      mov       dword [esp+14h],2

LAB_0050492a:
  0050492a  8b4ab4                mov       ecx,[edx-4Ch]
  0050492d  8d42b4                lea       eax,[edx-4Ch]
  00504930  8bd9                  mov       ebx,ecx
  00504932  81e177770000          and       ecx,7777h
  00504938  c1eb10                shr       ebx,10h
  0050493b  d1e1                  shl       ecx,1
  0050493d  84db                  test      bl,bl
  0050493f  8be9                  mov       ebp,ecx
  00504941  0f8584000000          jne       near 005049CBh
  00504947  f6c780                test      bh,80h
  0050494a  7457                  je        short 005049A3h
  0050494c  8bfa                  mov       edi,edx
  0050494e  8d4ab8                lea       ecx,[edx-48h]
  00504951  2bf8                  sub       edi,eax
  00504953  be01000000            mov       esi,1
  00504958  83c7fc                add       edi,0FFFFFFFCh
  0050495b  c744241004000000      mov       dword [esp+10h],4
  00504963  897c2418              mov       [esp+18h],edi
  00504967  8bfa                  mov       edi,edx
  00504969  2bf9                  sub       edi,ecx

LAB_0050496b:
  0050496b  668b19                mov       bx,[ecx]
  0050496e  84db                  test      bl,bl
  00504970  751e                  jne       short 00504990h
  00504972  8bc6                  mov       eax,esi
  00504974  25ffff0000            and       eax,0FFFFh
  00504979  0be8                  or        ebp,eax
  0050497b  8b01                  mov       eax,[ecx]
  0050497d  f6c730                test      bh,30h
  00504980  890439                mov       [ecx+edi],eax
  00504983  740b                  je        short 00504990h
  00504985  8b5c2418              mov       ebx,[esp+18h]
  00504989  8b4104                mov       eax,[ecx+4]
  0050498c  89441904              mov       [ecx+ebx+4],eax

LAB_00504990:
  00504990  8b442410              mov       eax,[esp+10h]
  00504994  83c108                add       ecx,8
  00504997  c1e604                shl       esi,4
  0050499a  48                    dec       eax
  0050499b  89442410              mov       [esp+10h],eax
  0050499f  75ca                  jne       short 0050496Bh
  005049a1  eb28                  jmp       short 005049CBh

LAB_005049a3:
  005049a3  8b42b8                mov       eax,[edx-48h]
  005049a6  33c9                  xor       ecx,ecx
  005049a8  668b4ab6              mov       cx,[edx-4Ah]
  005049ac  8bf0                  mov       esi,eax
  005049ae  c1e610                shl       esi,10h
  005049b1  83cd01                or        ebp,1
  005049b4  0bce                  or        ecx,esi
  005049b6  f6c730                test      bh,30h
  005049b9  890a                  mov       [edx],ecx
  005049bb  740e                  je        short 005049CBh
  005049bd  8b4abc                mov       ecx,[edx-44h]
  005049c0  c1e110                shl       ecx,10h
  005049c3  c1e810                shr       eax,10h
  005049c6  0bc8                  or        ecx,eax
  005049c8  894a04                mov       [edx+4],ecx

LAB_005049cb:
  005049cb  8bca                  mov       ecx,edx
  005049cd  b80f000000            mov       eax,0Fh
  005049d2  be04000000            mov       esi,4

LAB_005049d7:
  005049d7  8bf8                  mov       edi,eax
  005049d9  81e7ffff0000          and       edi,0FFFFh
  005049df  85ef                  test      edi,ebp
  005049e1  750d                  jne       short 005049F0h
  005049e3  c701ff41ffff          mov       dword [ecx],0FFFF41FFh
  005049e9  c74104ffffffff        mov       dword [ecx+4],0FFFFFFFFh

LAB_005049f0:
  005049f0  83c108                add       ecx,8
  005049f3  c1e004                shl       eax,4
  005049f6  4e                    dec       esi
  005049f7  75de                  jne       short 005049D7h
  005049f9  6685ed                test      bp,bp
  005049fc  66896ab4              mov       [edx-4Ch],bp
  00504a00  7408                  je        short 00504A0Ah
  00504a02  668b42b6              mov       ax,[edx-4Ah]
  00504a06  668942fe              mov       [edx-2],ax

LAB_00504a0a:
  00504a0a  8b442414              mov       eax,[esp+14h]
  00504a0e  83c224                add       edx,24h
  00504a11  48                    dec       eax
  00504a12  89442414              mov       [esp+14h],eax
  00504a16  0f850effffff          jne       near 0050492Ah
  00504a1c  8b44241c              mov       eax,[esp+1Ch]
  00504a20  5f                    pop       edi
  00504a21  5e                    pop       esi
  00504a22  5d                    pop       ebp
  00504a23  5b                    pop       ebx
  00504a24  83c410                add       esp,10h
  00504a27  c3                    ret
  00504a28  90                    nop
  00504a29  90                    nop
  00504a2a  90                    nop
  00504a2b  90                    nop
  00504a2c  90                    nop
  00504a2d  90                    nop
  00504a2e  90                    nop
  00504a2f  90                    nop
  00504a30  51                    push      ecx
  00504a31  53                    push      ebx
  00504a32  55                    push      ebp
  00504a33  56                    push      esi
  00504a34  57                    push      edi
  00504a35  b8204a6000            mov       eax,604A20h
  00504a3a  bb0c4b6000            mov       ebx,604B0Ch
  00504a3f  c744241002000000      mov       dword [esp+10h],2
  00504a47  33ed                  xor       ebp,ebp

LAB_00504a49:
  00504a49  8bf3                  mov       esi,ebx
  00504a4b  bf04000000            mov       edi,4

LAB_00504a50:
  00504a50  8b0e                  mov       ecx,[esi]
  00504a52  6681f90041            cmp       cx,4100h
  00504a57  0f8585000000          jne       near 00504AE2h
  00504a5d  668b10                mov       dx,[eax]
  00504a60  66896806              mov       [eax+6],bp
  00504a64  f7d1                  not       ecx
  00504a66  c1e910                shr       ecx,10h
  00504a69  66895008              mov       [eax+8],dx
  00504a6d  6633d1                xor       dx,cx
  00504a70  f6c2f0                test      dl,0F0h
  00504a73  668908                mov       [eax],cx
  00504a76  66895002              mov       [eax+2],dx
  00504a7a  7412                  je        short 00504A8Eh
  00504a7c  8a08                  mov       cl,[eax]
  00504a7e  c6400a0c              mov       byte [eax+0Ah],0Ch
  00504a82  81e1f0000000          and       ecx,0F0h
  00504a88  66894806              mov       [eax+6],cx
  00504a8c  eb21                  jmp       short 00504AAFh

LAB_00504a8e:
  00504a8e  8a480a                mov       cl,[eax+0Ah]
  00504a91  fec9                  dec       cl
  00504a93  88480a                mov       [eax+0Ah],cl
  00504a96  7517                  jne       short 00504AAFh
  00504a98  8a08                  mov       cl,[eax]
  00504a9a  c6400a02              mov       byte [eax+0Ah],2
  00504a9e  f6c1f0                test      cl,0F0h
  00504aa1  740c                  je        short 00504AAFh
  00504aa3  8ad1                  mov       dl,cl
  00504aa5  81e2f0000000          and       edx,0F0h
  00504aab  66895006              mov       [eax+6],dx

LAB_00504aaf:
  00504aaf  668b5002              mov       dx,[eax+2]
  00504ab3  668b4808              mov       cx,[eax+8]
  00504ab7  23ca                  and       ecx,edx
  00504ab9  66894804              mov       [eax+4],cx
  00504abd  668b08                mov       cx,[eax]
  00504ac0  6623ca                and       cx,dx
  00504ac3  8a500b                mov       dl,[eax+0Bh]
  00504ac6  80ca80                or        dl,80h
  00504ac9  66894802              mov       [eax+2],cx
  00504acd  88500b                mov       [eax+0Bh],dl
  00504ad0  8a5004                mov       dl,[eax+4]
  00504ad3  0ad1                  or        dl,cl
  00504ad5  f6c2f0                test      dl,0F0h
  00504ad8  7411                  je        short 00504AEBh
  00504ada  8a480b                mov       cl,[eax+0Bh]
  00504add  80c902                or        cl,2
  00504ae0  eb06                  jmp       short 00504AE8h

LAB_00504ae2:
  00504ae2  8a480b                mov       cl,[eax+0Bh]
  00504ae5  80e17e                and       cl,7Eh

LAB_00504ae8:
  00504ae8  88480b                mov       [eax+0Bh],cl

LAB_00504aeb:
  00504aeb  83c608                add       esi,8
  00504aee  83c010                add       eax,10h
  00504af1  4f                    dec       edi
  00504af2  0f8558ffffff          jne       near 00504A50h
  00504af8  8b4c2410              mov       ecx,[esp+10h]
  00504afc  83c324                add       ebx,24h
  00504aff  49                    dec       ecx
  00504b00  894c2410              mov       [esp+10h],ecx
  00504b04  0f853fffffff          jne       near 00504A49h
  00504b0a  5f                    pop       edi
  00504b0b  5e                    pop       esi
  00504b0c  5d                    pop       ebp
  00504b0d  5b                    pop       ebx
  00504b0e  59                    pop       ecx
  00504b0f  c3                    ret
  00504b10  56                    push      esi
  00504b11  8b742408              mov       esi,[esp+8]
  00504b15  b8284a6000            mov       eax,604A28h
  00504b1a  ba09000000            mov       edx,9
  00504b1f  33c9                  xor       ecx,ecx

LAB_00504b21:
  00504b21  3bf1                  cmp       esi,ecx
  00504b23  8948f8                mov       [eax-8],ecx
  00504b26  8948fc                mov       [eax-4],ecx
  00504b29  66c700ffff            mov       word [eax],0FFFFh
  00504b2e  7404                  je        short 00504B34h
  00504b30  66894802              mov       [eax+2],cx

LAB_00504b34:
  00504b34  894804                mov       [eax+4],ecx
  00504b37  83c010                add       eax,10h
  00504b3a  4a                    dec       edx
  00504b3b  75e4                  jne       short 00504B21h
  00504b3d  5e                    pop       esi
  00504b3e  c3                    ret
  00504b3f  90                    nop
  00504b40  53                    push      ebx
  00504b41  8b5c2408              mov       ebx,[esp+8]
  00504b45  56                    push      esi
  00504b46  57                    push      edi
  00504b47  8b7c2414              mov       edi,[esp+14h]
  00504b4b  be0a000000            mov       esi,0Ah
  00504b50  83c8ff                or        eax,0FFFFFFFFh

LAB_00504b53:
  00504b53  83f8ff                cmp       eax,0FFFFFFFFh
  00504b56  750a                  jne       short 00504B62h
  00504b58  57                    push      edi
  00504b59  53                    push      ebx
  00504b5a  e8170f0400            call      00545A76h
  00504b5f  4e                    dec       esi
  00504b60  75f1                  jne       short 00504B53h

LAB_00504b62:
  00504b62  5f                    pop       edi
  00504b63  5e                    pop       esi
  00504b64  5b                    pop       ebx
  00504b65  c3                    ret
  00504b66  90                    nop
  00504b67  90                    nop
  00504b68  90                    nop
  00504b69  90                    nop
  00504b6a  90                    nop
  00504b6b  90                    nop
  00504b6c  90                    nop
  00504b6d  90                    nop
  00504b6e  90                    nop
  00504b6f  90                    nop
  00504b70  56                    push      esi
  00504b71  57                    push      edi
  00504b72  8b7c240c              mov       edi,[esp+0Ch]
  00504b76  be0a000000            mov       esi,0Ah
  00504b7b  83c8ff                or        eax,0FFFFFFFFh

LAB_00504b7e:
  00504b7e  83f8ff                cmp       eax,0FFFFFFFFh
  00504b81  7509                  jne       short 00504B8Ch
  00504b83  57                    push      edi
  00504b84  e8f30e0400            call      00545A7Ch
  00504b89  4e                    dec       esi
  00504b8a  75f2                  jne       short 00504B7Eh

LAB_00504b8c:
  00504b8c  5f                    pop       edi
  00504b8d  5e                    pop       esi
  00504b8e  c3                    ret
  00504b8f  90                    nop
  00504b90  53                    push      ebx
  00504b91  8b5c240c              mov       ebx,[esp+0Ch]
  00504b95  55                    push      ebp
  00504b96  8b6c240c              mov       ebp,[esp+0Ch]
  00504b9a  56                    push      esi
  00504b9b  57                    push      edi
  00504b9c  8b7c241c              mov       edi,[esp+1Ch]
  00504ba0  be0a000000            mov       esi,0Ah
  00504ba5  83c8ff                or        eax,0FFFFFFFFh

LAB_00504ba8:
  00504ba8  83f8ff                cmp       eax,0FFFFFFFFh
  00504bab  750b                  jne       short 00504BB8h
  00504bad  57                    push      edi
  00504bae  53                    push      ebx
  00504baf  55                    push      ebp
  00504bb0  e8cd0e0400            call      00545A82h
  00504bb5  4e                    dec       esi
  00504bb6  75f0                  jne       short 00504BA8h

LAB_00504bb8:
  00504bb8  5f                    pop       edi
  00504bb9  5e                    pop       esi
  00504bba  5d                    pop       ebp
  00504bbb  5b                    pop       ebx
  00504bbc  c3                    ret
  00504bbd  90                    nop
  00504bbe  90                    nop
  00504bbf  90                    nop
  00504bc0  51                    push      ecx
  00504bc1  53                    push      ebx
  00504bc2  8b5c2414              mov       ebx,[esp+14h]
  00504bc6  55                    push      ebp
  00504bc7  8b6c2414              mov       ebp,[esp+14h]
  00504bcb  56                    push      esi
  00504bcc  8b742414              mov       esi,[esp+14h]
  00504bd0  57                    push      edi
  00504bd1  8b7c2424              mov       edi,[esp+24h]
  00504bd5  c74424100a000000      mov       dword [esp+10h],0Ah
  00504bdd  83c8ff                or        eax,0FFFFFFFFh

LAB_00504be0:
  00504be0  85c0                  test      eax,eax
  00504be2  7d2e                  jge       short 00504C12h
  00504be4  6a00                  push      0
  00504be6  53                    push      ebx
  00504be7  56                    push      esi
  00504be8  e8a3ffffff            call      00504B90h
  00504bed  83c40c                add       esp,0Ch
  00504bf0  85c0                  test      eax,eax
  00504bf2  7c1e                  jl        short 00504C12h
  00504bf4  57                    push      edi
  00504bf5  55                    push      ebp
  00504bf6  56                    push      esi
  00504bf7  e88c0e0400            call      00545A88h
  00504bfc  48                    dec       eax
  00504bfd  8d4fff                lea       ecx,[edi-1]
  00504c00  3bc1                  cmp       eax,ecx
  00504c02  7403                  je        short 00504C07h
  00504c04  83c8ff                or        eax,0FFFFFFFFh

LAB_00504c07:
  00504c07  8b4c2410              mov       ecx,[esp+10h]
  00504c0b  49                    dec       ecx
  00504c0c  894c2410              mov       [esp+10h],ecx
  00504c10  75ce                  jne       short 00504BE0h

LAB_00504c12:
  00504c12  5f                    pop       edi
  00504c13  5e                    pop       esi
  00504c14  5d                    pop       ebp
  00504c15  5b                    pop       ebx
  00504c16  59                    pop       ecx
  00504c17  c3                    ret
  00504c18  90                    nop
  00504c19  90                    nop
  00504c1a  90                    nop
  00504c1b  90                    nop
  00504c1c  90                    nop
  00504c1d  90                    nop
  00504c1e  90                    nop
  00504c1f  90                    nop
  00504c20  51                    push      ecx
  00504c21  53                    push      ebx
  00504c22  8b5c2414              mov       ebx,[esp+14h]
  00504c26  55                    push      ebp
  00504c27  8b6c2414              mov       ebp,[esp+14h]
  00504c2b  56                    push      esi
  00504c2c  8b742414              mov       esi,[esp+14h]
  00504c30  57                    push      edi
  00504c31  8b7c2424              mov       edi,[esp+24h]
  00504c35  c74424100a000000      mov       dword [esp+10h],0Ah
  00504c3d  83c8ff                or        eax,0FFFFFFFFh

LAB_00504c40:
  00504c40  85c0                  test      eax,eax
  00504c42  7d2e                  jge       short 00504C72h
  00504c44  6a00                  push      0
  00504c46  53                    push      ebx
  00504c47  56                    push      esi
  00504c48  e843ffffff            call      00504B90h
  00504c4d  83c40c                add       esp,0Ch
  00504c50  85c0                  test      eax,eax
  00504c52  7c1e                  jl        short 00504C72h
  00504c54  57                    push      edi
  00504c55  55                    push      ebp
  00504c56  56                    push      esi
  00504c57  e8320e0400            call      00545A8Eh
  00504c5c  48                    dec       eax
  00504c5d  8d4fff                lea       ecx,[edi-1]
  00504c60  3bc1                  cmp       eax,ecx
  00504c62  7403                  je        short 00504C67h
  00504c64  83c8ff                or        eax,0FFFFFFFFh

LAB_00504c67:
  00504c67  8b4c2410              mov       ecx,[esp+10h]
  00504c6b  49                    dec       ecx
  00504c6c  894c2410              mov       [esp+10h],ecx
  00504c70  75ce                  jne       short 00504C40h

LAB_00504c72:
  00504c72  5f                    pop       edi
  00504c73  5e                    pop       esi
  00504c74  5d                    pop       ebp
  00504c75  5b                    pop       ebx
  00504c76  59                    pop       ecx
  00504c77  c3                    ret
  00504c78  90                    nop
  00504c79  90                    nop
  00504c7a  90                    nop
  00504c7b  90                    nop
  00504c7c  90                    nop
  00504c7d  90                    nop
  00504c7e  90                    nop
  00504c7f  90                    nop
  00504c80  e81b0000              (bad)
  (octet invalide — la plage demandee deborde probablement du code)
