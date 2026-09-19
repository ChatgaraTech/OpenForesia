Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00467440  section .text  offset fichier 0x66840  1984 octets

  00467440  83ec1c                sub       esp,1Ch
  00467443  b8c0935e00            mov       eax,5E93C0h
  00467448  53                    push      ebx
  00467449  55                    push      ebp
  0046744a  56                    push      esi
  0046744b  57                    push      edi

LAB_0046744c:
  0046744c  8b08                  mov       ecx,[eax]
  0046744e  f7c100000080          test      ecx,80000000h
  00467454  7414                  je        short 0046746Ah
  00467456  f7c100000050          test      ecx,50000000h
  0046745c  750c                  jne       short 0046746Ah
  0046745e  8b8890000000          mov       ecx,[eax+90h]
  00467464  81e100800000          and       ecx,8000h

LAB_0046746a:
  0046746a  059c000000            add       eax,9Ch
  0046746f  3d94955e00            cmp       eax,5E9594h
  00467474  7ed6                  jle       short 0046744Ch
  00467476  8b742430              mov       esi,[esp+30h]
  0046747a  8a4e07                mov       cl,[esi+7]
  0046747d  80e1ef                and       cl,0EFh
  00467480  884e07                mov       [esi+7],cl
  00467483  66837e1003            cmp       word [esi+10h],3
  00467488  0f85a7000000          jne       near 00467535h
  0046748e  c7461c0080df00        mov       dword [esi+1Ch],0DF8000h
  00467495  c7462000c07100        mov       dword [esi+20h],71C000h
  0046749c  33ff                  xor       edi,edi
  0046749e  b800a00000            mov       eax,0A000h
  004674a3  c7462400602501        mov       dword [esi+24h],1256000h
  004674aa  66897e30              mov       [esi+30h],di
  004674ae  66898682000000        mov       [esi+82h],ax
  004674b5  66898686000000        mov       [esi+86h],ax
  004674bc  6689868a000000        mov       [esi+8Ah],ax
  004674c3  6689868e000000        mov       [esi+8Eh],ax
  004674ca  8b4620                mov       eax,[esi+20h]
  004674cd  66c746320004          mov       word [esi+32h],400h
  004674d3  66897e34              mov       [esi+34h],di
  004674d7  894664                mov       [esi+64h],eax
  004674da  894660                mov       [esi+60h],eax
  004674dd  a1c07b5f00            mov       eax,[5F7BC0h]
  004674e2  6800800000            push      8000h
  004674e7  56                    push      esi
  004674e8  50                    push      eax
  004674e9  e8e2bb0c00            call      005330D0h
  004674ee  668b4628              mov       ax,[esi+28h]
  004674f2  b900100000            mov       ecx,1000h
  004674f7  83c40c                add       esp,0Ch
  004674fa  663bc1                cmp       ax,cx
  004674fd  7d1e                  jge       short 0046751Dh
  004674ff  052c010000            add       eax,12Ch
  00467504  66894628              mov       [esi+28h],ax
  00467508  b82c010000            mov       eax,12Ch
  0046750d  6601462a              add       [esi+2Ah],ax
  00467511  6601462c              add       [esi+2Ch],ax
  00467515  5f                    pop       edi
  00467516  5e                    pop       esi
  00467517  5d                    pop       ebp
  00467518  5b                    pop       ebx
  00467519  83c41c                add       esp,1Ch
  0046751c  c3                    ret

LAB_0046751d:
  0046751d  66894e28              mov       [esi+28h],cx
  00467521  66894e2a              mov       [esi+2Ah],cx
  00467525  66894e2c              mov       [esi+2Ch],cx
  00467529  66897e10              mov       [esi+10h],di
  0046752d  5f                    pop       edi
  0046752e  5e                    pop       esi
  0046752f  5d                    pop       ebp
  00467530  5b                    pop       ebx
  00467531  83c41c                add       esp,1Ch
  00467534  c3                    ret

LAB_00467535:
  00467535  6800000040            push      40000000h
  0046753a  6a20                  push      20h
  0046753c  56                    push      esi
  0046753d  e8beda0c00            call      00535000h
  00467542  83c40c                add       esp,0Ch
  00467545  8bd8                  mov       ebx,eax
  00467547  6800000040            push      40000000h
  0046754c  6a10                  push      10h
  0046754e  56                    push      esi
  0046754f  e8acda0c00            call      00535000h
  00467554  83c40c                add       esp,0Ch
  00467557  8bf8                  mov       edi,eax
  00467559  e812f8ffff            call      00466D70h
  0046755e  0fbfc8                movsx     ecx,ax
  00467561  33ed                  xor       ebp,ebp
  00467563  3bf9                  cmp       edi,ecx
  00467565  7538                  jne       short 0046759Fh
  00467567  66396e10              cmp       [esi+10h],bp
  0046756b  7532                  jne       short 0046759Fh
  0046756d  8b5660                mov       edx,[esi+60h]
  00467570  66c746100100          mov       word [esi+10h],1
  00467576  66896e12              mov       [esi+12h],bp
  0046757a  c7465c00040000        mov       dword [esi+5Ch],400h
  00467581  66c746140a00          mov       word [esi+14h],0Ah
  00467587  66896e16              mov       [esi+16h],bp
  0046758b  6689ae9a000000        mov       [esi+9Ah],bp
  00467592  891590175700          mov       [571790h],edx
  00467598  6689ae98000000        mov       [esi+98h],bp

LAB_0046759f:
  0046759f  3bdd                  cmp       ebx,ebp
  004675a1  7513                  jne       short 004675B6h
  004675a3  66837e1001            cmp       word [esi+10h],1
  004675a8  750c                  jne       short 004675B6h
  004675aa  66c746100200          mov       word [esi+10h],2
  004675b0  66896e12              mov       [esi+12h],bp
  004675b4  eb26                  jmp       short 004675DCh

LAB_004675b6:
  004675b6  0fbf5e12              movsx     ebx,word [esi+12h]
  004675ba  66c746120200          mov       word [esi+12h],2
  004675c0  bf03000000            mov       edi,3

LAB_004675c5:
  004675c5  55                    push      ebp
  004675c6  55                    push      ebp
  004675c7  6aff                  push      0FFFFFFFFh
  004675c9  55                    push      ebp
  004675ca  6a31                  push      31h
  004675cc  56                    push      esi
  004675cd  e83e0b0000            call      00468110h
  004675d2  83c418                add       esp,18h
  004675d5  4f                    dec       edi
  004675d6  75ed                  jne       short 004675C5h
  004675d8  66895e12              mov       [esi+12h],bx

LAB_004675dc:
  004675dc  66a19a175700          mov       ax,[57179Ah]
  004675e2  8b5e5c                mov       ebx,[esi+5Ch]
  004675e5  0fbfc8                movsx     ecx,ax
  004675e8  ba00fcffff            mov       edx,0FFFFFC00h
  004675ed  66c70598175700f401    mov       word [571798h],1F4h
  004675f6  2bd1                  sub       edx,ecx
  004675f8  b9dc050000            mov       ecx,5DCh
  004675fd  2bd3                  sub       edx,ebx
  004675ff  66892d9c175700        mov       [57179Ch],bp
  00467606  c1fa03                sar       edx,3
  00467609  6603c2                add       ax,dx
  0046760c  892d60175700          mov       [571760h],ebp
  00467612  66a39a175700          mov       [57179Ah],ax
  00467618  a168175700            mov       eax,[571768h]
  0046761d  2bc8                  sub       ecx,eax
  0046761f  892d64175700          mov       [571764h],ebp
  00467625  c1f904                sar       ecx,4
  00467628  03c1                  add       eax,ecx
  0046762a  a368175700            mov       [571768h],eax
  0046762f  66837e1638            cmp       word [esi+16h],38h
  00467634  7c60                  jl        short 00467696h
  00467636  66837e1001            cmp       word [esi+10h],1
  0046763b  7559                  jne       short 00467696h
  0046763d  66c74610ffff          mov       word [esi+10h],0FFFFh
  00467643  8b15cc975e00          mov       edx,[5E97CCh]
  00467649  55                    push      ebp
  0046764a  68c0935e00            push      5E93C0h
  0046764f  52                    push      edx
  00467650  e87bc70a00            call      00513DD0h
  00467655  a1cc975e00            mov       eax,[5E97CCh]
  0046765a  83c40c                add       esp,0Ch
  0046765d  55                    push      ebp
  0046765e  685c945e00            push      5E945Ch
  00467663  50                    push      eax
  00467664  e867c70a00            call      00513DD0h
  00467669  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046766f  83c40c                add       esp,0Ch
  00467672  55                    push      ebp
  00467673  68f8945e00            push      5E94F8h
  00467678  51                    push      ecx
  00467679  e852c70a00            call      00513DD0h
  0046767e  8b15cc975e00          mov       edx,[5E97CCh]
  00467684  83c40c                add       esp,0Ch
  00467687  55                    push      ebp
  00467688  6894955e00            push      5E9594h
  0046768d  52                    push      edx
  0046768e  e83dc70a00            call      00513DD0h
  00467693  83c40c                add       esp,0Ch

LAB_00467696:
  00467696  668b4610              mov       ax,[esi+10h]
  0046769a  663bc5                cmp       ax,bp
  0046769d  0f8418040000          je        near 00467ABBh
  004676a3  663dffff              cmp       ax,0FFFFh
  004676a7  0f840e040000          je        near 00467ABBh
  004676ad  0fbf5616              movsx     edx,word [esi+16h]
  004676b1  668b869a000000        mov       ax,[esi+9Ah]
  004676b8  83c2f6                add       edx,0FFFFFFF6h
  004676bb  66294632              sub       [esi+32h],ax
  004676bf  668b6e32              mov       bp,[esi+32h]
  004676c3  33db                  xor       ebx,ebx
  004676c5  33c9                  xor       ecx,ecx
  004676c7  33ff                  xor       edi,edi
  004676c9  83fa22                cmp       edx,22h
  004676cc  771b                  ja        short 004676E9h
  004676ce  33c0                  xor       eax,eax
  004676d0  8a82987b4600          mov       al,[edx+467B98h]
  004676d6  ff2485687b4600        jmp       dword [eax*4+467B68h]
  004676dd  b901000000            mov       ecx,1
  004676e2  eb05                  jmp       short 004676E9h
  004676e4  bf01000000            mov       edi,1

LAB_004676e9:
  004676e9  83f901                cmp       ecx,1
  004676ec  7504                  jne       short 004676F2h
  004676ee  66ff4612              inc       word [esi+12h]

LAB_004676f2:
  004676f2  83ff01                cmp       edi,1
  004676f5  7504                  jne       short 004676FBh
  004676f7  66ff4e12              dec       word [esi+12h]

LAB_004676fb:
  004676fb  3bfb                  cmp       edi,ebx
  004676fd  7524                  jne       short 00467723h
  004676ff  3bcb                  cmp       ecx,ebx
  00467701  7520                  jne       short 00467723h
  00467703  668b4e12              mov       cx,[esi+12h]
  00467707  663bcb                cmp       cx,bx
  0046770a  7e07                  jle       short 00467713h
  0046770c  83c1fe                add       ecx,0FFFFFFFEh
  0046770f  66894e12              mov       [esi+12h],cx

LAB_00467713:
  00467713  668b4e12              mov       cx,[esi+12h]
  00467717  663bcb                cmp       cx,bx
  0046771a  7d07                  jge       short 00467723h
  0046771c  83c102                add       ecx,2
  0046771f  66894e12              mov       [esi+12h],cx

LAB_00467723:
  00467723  668bc5                mov       ax,bp
  00467726  66034612              add       ax,[esi+12h]
  0046772a  0fbfc8                movsx     ecx,ax
  0046772d  66894632              mov       [esi+32h],ax
  00467731  8b465c                mov       eax,[esi+5Ch]
  00467734  2bc8                  sub       ecx,eax
  00467736  51                    push      ecx
  00467737  e8e0e20d00            call      00545A1Ch
  0046773c  0fbf5612              movsx     edx,word [esi+12h]
  00467740  0fafc2                imul      eax,edx
  00467743  8b7e24                mov       edi,[esi+24h]
  00467746  8b565c                mov       edx,[esi+5Ch]
  00467749  2bf8                  sub       edi,eax
  0046774b  0fbf4632              movsx     eax,word [esi+32h]
  0046774f  2bc2                  sub       eax,edx
  00467751  897e24                mov       [esi+24h],edi
  00467754  50                    push      eax
  00467755  e8bce20d00            call      00545A16h
  0046775a  0fbf4e12              movsx     ecx,word [esi+12h]
  0046775e  0fafc1                imul      eax,ecx
  00467761  8b6e1c                mov       ebp,[esi+1Ch]
  00467764  8b565c                mov       edx,[esi+5Ch]
  00467767  2be8                  sub       ebp,eax
  00467769  52                    push      edx
  0046776a  896e1c                mov       [esi+1Ch],ebp
  0046776d  e8aae20d00            call      00545A1Ch
  00467772  0fbf4e14              movsx     ecx,word [esi+14h]
  00467776  0fafc1                imul      eax,ecx
  00467779  8b4e24                mov       ecx,[esi+24h]
  0046777c  8b565c                mov       edx,[esi+5Ch]
  0046777f  668b7e14              mov       di,[esi+14h]
  00467783  2bc8                  sub       ecx,eax
  00467785  52                    push      edx
  00467786  894e24                mov       [esi+24h],ecx
  00467789  e888e20d00            call      00545A16h
  0046778e  0fbfcf                movsx     ecx,di
  00467791  0fafc1                imul      eax,ecx
  00467794  8b561c                mov       edx,[esi+1Ch]
  00467797  03d0                  add       edx,eax
  00467799  8d4701                lea       eax,[edi+1]
  0046779c  663d2800              cmp       ax,28h
  004677a0  89561c                mov       [esi+1Ch],edx
  004677a3  66894614              mov       [esi+14h],ax
  004677a7  7c06                  jl        short 004677AFh
  004677a9  66c746142800          mov       word [esi+14h],28h

LAB_004677af:
  004677af  668b9690000000        mov       dx,[esi+90h]
  004677b6  8b4e60                mov       ecx,[esi+60h]
  004677b9  6689542410            mov       [esp+10h],dx
  004677be  8b5664                mov       edx,[esi+64h]
  004677c1  89542418              mov       [esp+18h],edx
  004677c5  8b5620                mov       edx,[esi+20h]
  004677c8  8b467c                mov       eax,[esi+7Ch]
  004677cb  895648                mov       [esi+48h],edx
  004677ce  8b9680000000          mov       edx,[esi+80h]
  004677d4  894c2414              mov       [esp+14h],ecx
  004677d8  8b4e1c                mov       ecx,[esi+1Ch]
  004677db  8954241c              mov       [esp+1Ch],edx
  004677df  8b9688000000          mov       edx,[esi+88h]
  004677e5  894e44                mov       [esi+44h],ecx
  004677e8  8b4e24                mov       ecx,[esi+24h]
  004677eb  89542424              mov       [esp+24h],edx
  004677ef  894e4c                mov       [esi+4Ch],ecx
  004677f2  8b8e84000000          mov       ecx,[esi+84h]
  004677f8  33d2                  xor       edx,edx
  004677fa  89442430              mov       [esp+30h],eax
  004677fe  8a500e                mov       dl,[eax+0Eh]
  00467801  a190175700            mov       eax,[571790h]
  00467806  894c2420              mov       [esp+20h],ecx
  0046780a  8b8e8c000000          mov       ecx,[esi+8Ch]
  00467810  668bac9680000000      mov       bp,[esi+edx*4+80h]
  00467818  56                    push      esi
  00467819  894c242c              mov       [esp+2Ch],ecx
  0046781d  894620                mov       [esi+20h],eax
  00467820  e81bc10c00            call      00533940h
  00467825  8b4620                mov       eax,[esi+20h]
  00467828  8b5660                mov       edx,[esi+60h]
  0046782b  6683463405            add       word [esi+34h],5
  00467830  668b4e34              mov       cx,[esi+34h]
  00467834  050010ffff            add       eax,0FFFF1000h
  00467839  83c404                add       esp,4
  0046783c  3bc2                  cmp       eax,edx
  0046783e  894620                mov       [esi+20h],eax
  00467841  0f8fa7000000          jg        near 004678EEh
  00467847  6683f914              cmp       cx,14h
  0046784b  7e29                  jle       short 00467876h
  0046784d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00467853  53                    push      ebx
  00467854  53                    push      ebx
  00467855  6800100000            push      1000h
  0046785a  8b91a8000000          mov       edx,[ecx+0A8h]
  00467860  6800080000            push      800h
  00467865  8b02                  mov       eax,[edx]
  00467867  660fb64815            movzx     cx,byte [eax+15h]
  0046786c  51                    push      ecx
  0046786d  56                    push      esi
  0046786e  e80d080000            call      00468080h
  00467873  83c418                add       esp,18h

LAB_00467876:
  00467876  8b5660                mov       edx,[esi+60h]
  00467879  66838698000000fe      add       word [esi+98h],0FFFEh
  00467881  66399e98000000        cmp       [esi+98h],bx
  00467888  895620                mov       [esi+20h],edx
  0046788b  66895e34              mov       [esi+34h],bx
  0046788f  7f09                  jg        short 0046789Ah
  00467891  66899e98000000        mov       [esi+98h],bx
  00467898  eb29                  jmp       short 004678C3h

LAB_0046789a:
  0046789a  0fbf5e12              movsx     ebx,word [esi+12h]
  0046789e  66c746120100          mov       word [esi+12h],1
  004678a4  bf04000000            mov       edi,4

LAB_004678a9:
  004678a9  6a00                  push      0
  004678ab  6a00                  push      0
  004678ad  6aff                  push      0FFFFFFFFh
  004678af  6a00                  push      0
  004678b1  6a31                  push      31h
  004678b3  56                    push      esi
  004678b4  e857080000            call      00468110h
  004678b9  83c418                add       esp,18h
  004678bc  4f                    dec       edi
  004678bd  75ea                  jne       short 004678A9h
  004678bf  66895e12              mov       [esi+12h],bx

LAB_004678c3:
  004678c3  0fbf5e12              movsx     ebx,word [esi+12h]
  004678c7  66c746120000          mov       word [esi+12h],0
  004678cd  bf03000000            mov       edi,3

LAB_004678d2:
  004678d2  6a00                  push      0
  004678d4  6a00                  push      0
  004678d6  6aff                  push      0FFFFFFFFh
  004678d8  6a00                  push      0
  004678da  6a31                  push      31h
  004678dc  56                    push      esi
  004678dd  e82e080000            call      00468110h
  004678e2  83c418                add       esp,18h
  004678e5  4f                    dec       edi
  004678e6  75ea                  jne       short 004678D2h
  004678e8  66895e12              mov       [esi+12h],bx
  004678ec  eb0e                  jmp       short 004678FCh

LAB_004678ee:
  004678ee  83c10a                add       ecx,0Ah
  004678f1  66ff8698000000        inc       word [esi+98h]
  004678f8  66894e34              mov       [esi+34h],cx

LAB_004678fc:
  004678fc  8b4660                mov       eax,[esi+60h]
  004678ff  33d2                  xor       edx,edx
  00467901  a390175700            mov       [571790h],eax
  00467906  8b4e7c                mov       ecx,[esi+7Ch]
  00467909  8a510e                mov       dl,[ecx+0Eh]
  0046790c  6639ac9680000000      cmp       [esi+edx*4+80h],bp
  00467914  7454                  je        short 0046796Ah
  00467916  66ff4616              inc       word [esi+16h]
  0046791a  0fbf4616              movsx     eax,word [esi+16h]
  0046791e  83c0e9                add       eax,0FFFFFFE9h
  00467921  83f806                cmp       eax,6
  00467924  7744                  ja        short 0046796Ah
  00467926  ff2485bc7b4600        jmp       dword [eax*4+467BBCh]
  0046792d  c7465c64040000        mov       dword [esi+5Ch],464h
  00467934  eb34                  jmp       short 0046796Ah
  00467936  c7465c2c050000        mov       dword [esi+5Ch],52Ch
  0046793d  eb2b                  jmp       short 0046796Ah
  0046793f  c7465cf4050000        mov       dword [esi+5Ch],5F4h
  00467946  eb22                  jmp       short 0046796Ah
  00467948  c7465cbc060000        mov       dword [esi+5Ch],6BCh
  0046794f  eb19                  jmp       short 0046796Ah
  00467951  c7465c20070000        mov       dword [esi+5Ch],720h
  00467958  eb10                  jmp       short 0046796Ah
  0046795a  c7465c84070000        mov       dword [esi+5Ch],784h
  00467961  eb07                  jmp       short 0046796Ah
  00467963  c7465c00080000        mov       dword [esi+5Ch],800h

LAB_0046796a:
  0046796a  668b4e16              mov       cx,[esi+16h]
  0046796e  0fbfc1                movsx     eax,cx
  00467971  83c0e9                add       eax,0FFFFFFE9h
  00467974  83f806                cmp       eax,6
  00467977  7778                  ja        short 004679F1h
  00467979  ff2485d87b4600        jmp       dword [eax*4+467BD8h]
  00467980  668b869a000000        mov       ax,[esi+9Ah]
  00467987  663d6400              cmp       ax,64h
  0046798b  7d64                  jge       short 004679F1h
  0046798d  eb58                  jmp       short 004679E7h
  0046798f  668b869a000000        mov       ax,[esi+9Ah]
  00467996  663d2c01              cmp       ax,12Ch
  0046799a  7d55                  jge       short 004679F1h
  0046799c  eb49                  jmp       short 004679E7h
  0046799e  668b869a000000        mov       ax,[esi+9Ah]
  004679a5  663df401              cmp       ax,1F4h
  004679a9  7d46                  jge       short 004679F1h
  004679ab  eb3a                  jmp       short 004679E7h
  004679ad  668b869a000000        mov       ax,[esi+9Ah]
  004679b4  663dbc02              cmp       ax,2BCh
  004679b8  7d37                  jge       short 004679F1h
  004679ba  eb2b                  jmp       short 004679E7h
  004679bc  668b869a000000        mov       ax,[esi+9Ah]
  004679c3  663d2003              cmp       ax,320h
  004679c7  7d28                  jge       short 004679F1h
  004679c9  eb1c                  jmp       short 004679E7h
  004679cb  668b869a000000        mov       ax,[esi+9Ah]
  004679d2  663d8403              cmp       ax,384h
  004679d6  7d19                  jge       short 004679F1h
  004679d8  eb0d                  jmp       short 004679E7h
  004679da  668b869a000000        mov       ax,[esi+9Ah]
  004679e1  663d0004              cmp       ax,400h
  004679e5  7d0a                  jge       short 004679F1h

LAB_004679e7:
  004679e7  83c014                add       eax,14h
  004679ea  6689869a000000        mov       [esi+9Ah],ax

LAB_004679f1:
  004679f1  6683f91d              cmp       cx,1Dh
  004679f5  7e1d                  jle       short 00467A14h
  004679f7  6683869a00000014      add       word [esi+9Ah],14h
  004679ff  b800040000            mov       eax,400h
  00467a04  6639869a000000        cmp       [esi+9Ah],ax
  00467a0b  7e07                  jle       short 00467A14h
  00467a0d  6689869a000000        mov       [esi+9Ah],ax

LAB_00467a14:
  00467a14  8b8690000000          mov       eax,[esi+90h]
  00467a1a  f6c408                test      ah,8
  00467a1d  7406                  je        short 00467A25h
  00467a1f  66c746100200          mov       word [esi+10h],2

LAB_00467a25:
  00467a25  f6c480                test      ah,80h
  00467a28  7406                  je        short 00467A30h
  00467a2a  66c746100200          mov       word [esi+10h],2

LAB_00467a30:
  00467a30  f6c489                test      ah,89h
  00467a33  745e                  je        short 00467A93h
  00467a35  8b442430              mov       eax,[esp+30h]
  00467a39  8b542414              mov       edx,[esp+14h]
  00467a3d  89467c                mov       [esi+7Ch],eax
  00467a40  8b442410              mov       eax,[esp+10h]
  00467a44  25ffff0000            and       eax,0FFFFh
  00467a49  895660                mov       [esi+60h],edx
  00467a4c  8b5644                mov       edx,[esi+44h]
  00467a4f  898690000000          mov       [esi+90h],eax
  00467a55  8b442418              mov       eax,[esp+18h]
  00467a59  89561c                mov       [esi+1Ch],edx
  00467a5c  8b564c                mov       edx,[esi+4Ch]
  00467a5f  894664                mov       [esi+64h],eax
  00467a62  8b4648                mov       eax,[esi+48h]
  00467a65  894620                mov       [esi+20h],eax
  00467a68  8b44241c              mov       eax,[esp+1Ch]
  00467a6c  895624                mov       [esi+24h],edx
  00467a6f  8b542420              mov       edx,[esp+20h]
  00467a73  898680000000          mov       [esi+80h],eax
  00467a79  8b442424              mov       eax,[esp+24h]
  00467a7d  899684000000          mov       [esi+84h],edx
  00467a83  8b542428              mov       edx,[esp+28h]
  00467a87  898688000000          mov       [esi+88h],eax
  00467a8d  89968c000000          mov       [esi+8Ch],edx

LAB_00467a93:
  00467a93  668b869a000000        mov       ax,[esi+9Ah]
  00467a9a  66014632              add       [esi+32h],ax
  00467a9e  668b4632              mov       ax,[esi+32h]
  00467aa2  6683f923              cmp       cx,23h
  00467aa6  7e13                  jle       short 00467ABBh
  00467aa8  0fbfc8                movsx     ecx,ax
  00467aab  ba00080000            mov       edx,800h
  00467ab0  2bd1                  sub       edx,ecx
  00467ab2  c1fa04                sar       edx,4
  00467ab5  03d0                  add       edx,eax
  00467ab7  66895632              mov       [esi+32h],dx

LAB_00467abb:
  00467abb  66837e1002            cmp       word [esi+10h],2
  00467ac0  0f859a000000          jne       near 00467B60h
  00467ac6  8b0e                  mov       ecx,[esi]
  00467ac8  6683463014            add       word [esi+30h],14h
  00467acd  668146328000          add       word [esi+32h],80h
  00467ad3  66834634ce            add       word [esi+34h],0FFCEh
  00467ad8  b89cffffff            mov       eax,0FFFFFF9Ch
  00467add  33ff                  xor       edi,edi
  00467adf  66014628              add       [esi+28h],ax
  00467ae3  6601462a              add       [esi+2Ah],ax
  00467ae7  6601462c              add       [esi+2Ch],ax
  00467aeb  80cd20                or        ch,20h
  00467aee  66397e12              cmp       [esi+12h],di
  00467af2  890e                  mov       [esi],ecx
  00467af4  7543                  jne       short 00467B39h
  00467af6  804e0710              or        byte [esi+7],10h
  00467afa  e841f60d00            call      00547140h
  00467aff  99                    cdq
  00467b00  b905000000            mov       ecx,5
  00467b05  c7465400a00000        mov       dword [esi+54h],0A000h
  00467b0c  f7f9                  idiv      ecx
  00467b0e  83ea02                sub       edx,2
  00467b11  8d0492                lea       eax,[edx+edx*4]
  00467b14  8d1480                lea       edx,[eax+eax*4]
  00467b17  c1e20d                shl       edx,0Dh
  00467b1a  895650                mov       [esi+50h],edx
  00467b1d  e81ef60d00            call      00547140h
  00467b22  99                    cdq
  00467b23  b905000000            mov       ecx,5
  00467b28  f7f9                  idiv      ecx
  00467b2a  83ea02                sub       edx,2
  00467b2d  8d0492                lea       eax,[edx+edx*4]
  00467b30  8d1480                lea       edx,[eax+eax*4]
  00467b33  c1e20d                shl       edx,0Dh
  00467b36  895658                mov       [esi+58h],edx

LAB_00467b39:
  00467b39  66ff4612              inc       word [esi+12h]
  00467b3d  66397e28              cmp       [esi+28h],di
  00467b41  7d1d                  jge       short 00467B60h
  00467b43  8b06                  mov       eax,[esi]
  00467b45  66897e28              mov       [esi+28h],di
  00467b49  80e4df                and       ah,0DFh
  00467b4c  66897e2a              mov       [esi+2Ah],di
  00467b50  66897e2c              mov       [esi+2Ch],di
  00467b54  66897e12              mov       [esi+12h],di
  00467b58  66c746100300          mov       word [esi+10h],3
  00467b5e  8906                  mov       [esi],eax

LAB_00467b60:
  00467b60  5f                    pop       edi
  00467b61  5e                    pop       esi
  00467b62  5d                    pop       ebp
  00467b63  5b                    pop       ebx
  00467b64  83c41c                add       esp,1Ch
  00467b67  c3                    ret
  00467b68  dd7646                fnsave    [esi+46h]
  00467b6b  00dd                  add       ch,bl
  00467b6d  7646                  jbe       short 00467BB5h
  00467b6f  00dd                  add       ch,bl
  00467b71  7646                  jbe       short 00467BB9h
  00467b73  00dd                  add       ch,bl
  00467b75  7646                  jbe       short 00467BBDh
  00467b77  00dd                  add       ch,bl
  00467b79  7646                  jbe       short 00467BC1h
  00467b7b  00dd                  add       ch,bl
  00467b7d  7646                  jbe       short 00467BC5h
  00467b7f  00dd                  add       ch,bl
  00467b81  7646                  jbe       short 00467BC9h
  00467b83  00dd                  add       ch,bl
  00467b85  7646                  jbe       short 00467BCDh
  00467b87  00dd                  add       ch,bl
  00467b89  7646                  jbe       short 00467BD1h
  00467b8b  00e4                  add       ah,ah
  00467b8d  7646                  jbe       short 00467BD5h
  00467b8f  00e4                  add       ah,ah
  00467b91  7646                  jbe       short 00467BD9h
  00467b93  00e9                  add       cl,ch
  00467b95  7646                  jbe       short 00467BDDh
  00467b97  0000                  add       [eax],al
  00467b99  010b                  add       [ebx],ecx
  00467b9b  0b0b                  or        ecx,[ebx]
  00467b9d  0b0b                  or        ecx,[ebx]
  00467b9f  0b0b                  or        ecx,[ebx]
  00467ba1  0b0b                  or        ecx,[ebx]
  00467ba3  0203                  add       al,[ebx]
  00467ba5  040b                  add       al,0Bh
  00467ba7  0b050607080b          or        eax,[0B080706h]
  00467bad  0b0b                  or        ecx,[ebx]
  00467baf  0b0b                  or        ecx,[ebx]
  00467bb1  0b0b                  or        ecx,[ebx]
  00467bb3  0b0b                  or        ecx,[ebx]

LAB_00467bb5:
  00467bb5  0b0b                  or        ecx,[ebx]
  00467bb7  0b0b                  or        ecx,[ebx]

LAB_00467bb9:
  00467bb9  090a                  or        [edx],ecx
  00467bbb  90                    nop
  00467bbc  2d79460036            sub       eax,36004679h

LAB_00467bc1:
  00467bc1  7946                  jns       short 00467C09h
  00467bc3  003f                  add       [edi],bh

LAB_00467bc5:
  00467bc5  7946                  jns       short 00467C0Dh
  00467bc7  004879                add       [eax+79h],cl
  00467bca  46                    inc       esi
  00467bcb  005179                add       [ecx+79h],dl
  00467bce  46                    inc       esi
  00467bcf  005a79                add       [edx+79h],bl
  00467bd2  46                    inc       esi
  00467bd3  006379                add       [ebx+79h],ah
  00467bd6  46                    inc       esi
  00467bd7  00807946008f          add       [eax-70FFB987h],al

LAB_00467bdd:
  00467bdd  7946                  jns       short 00467C25h
  00467bdf  009e794600ad          add       [esi-52FFB987h],bl
  00467be5  7946                  jns       short 00467C2Dh
  00467be7  00bc794600cb79        add       [ecx+edi*2+79CB0046h],bh
  00467bee  46                    inc       esi
  00467bef  00da                  add       dl,bl
  00467bf1  7946                  jns       short 00467C39h
  00467bf3  009090909090          add       [eax-6F6F6F70h],dl
  00467bf9  90                    nop
  00467bfa  90                    nop
  00467bfb  90                    nop
  00467bfc  90                    nop
  00467bfd  90                    nop
  00467bfe  90                    nop
  00467bff  90                    nop

43 cible(s) de saut interne : 0x0046744c, 0x0046746a, 0x0046751d, 0x00467535, 0x0046759f, 0x004675b6, 0x004675c5, 0x004675dc, 0x00467696, 0x004676e9, 0x004676f2, 0x004676fb, 0x00467713, 0x00467723, 0x004677af, 0x00467876, 0x0046789a, 0x004678a9, 0x004678c3, 0x004678d2, 0x004678ee, 0x004678fc, 0x0046796a, 0x004679e7, 0x004679f1, 0x00467a14, 0x00467a25, 0x00467a30, 0x00467a93, 0x00467abb, 0x00467b39, 0x00467b60, 0x00467bb5, 0x00467bb9, 0x00467bbd, 0x00467bc1, 0x00467bc5, 0x00467bc9, 0x00467bcd, 0x00467bd1, 0x00467bd5, 0x00467bd9, 0x00467bdd
