Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x005358f0  section .text  offset fichier 0x134cf0  336 octets

  005358f0  83ec38                sub       esp,38h
  005358f3  8b44243c              mov       eax,[esp+3Ch]
  005358f7  53                    push      ebx
  005358f8  55                    push      ebp
  005358f9  56                    push      esi
  005358fa  8b5004                mov       edx,[eax+4]
  005358fd  8b08                  mov       ecx,[eax]
  005358ff  89542434              mov       [esp+34h],edx
  00535903  8b5008                mov       edx,[eax+8]
  00535906  8b400c                mov       eax,[eax+0Ch]
  00535909  89542438              mov       [esp+38h],edx
  0053590d  8944243c              mov       [esp+3Ch],eax
  00535911  8b44244c              mov       eax,[esp+4Ch]
  00535915  894c2430              mov       [esp+30h],ecx
  00535919  894c2440              mov       [esp+40h],ecx
  0053591d  8b5004                mov       edx,[eax+4]
  00535920  8b08                  mov       ecx,[eax]
  00535922  89542424              mov       [esp+24h],edx
  00535926  8b500c                mov       edx,[eax+0Ch]
  00535929  894c2420              mov       [esp+20h],ecx
  0053592d  8b4808                mov       ecx,[eax+8]
  00535930  8954242c              mov       [esp+2Ch],edx
  00535934  33c0                  xor       eax,eax
  00535936  8d542434              lea       edx,[esp+34h]
  0053593a  be04000000            mov       esi,4
  0053593f  57                    push      edi
  00535940  894c242c              mov       [esp+2Ch],ecx
  00535944  89442450              mov       [esp+50h],eax
  00535948  89442410              mov       [esp+10h],eax
  0053594c  89542418              mov       [esp+18h],edx
  00535950  8974241c              mov       [esp+1Ch],esi

LAB_00535954:
  00535954  8b0d08876000          mov       ecx,[608708h]
  0053595a  8b42fc                mov       eax,[edx-4]
  0053595d  8b12                  mov       edx,[edx]
  0053595f  89742414              mov       [esp+14h],esi
  00535963  8901                  mov       [ecx],eax
  00535965  a110876000            mov       eax,[608710h]
  0053596a  8d4c2424              lea       ecx,[esp+24h]
  0053596e  8910                  mov       [eax],edx
  00535970  894c244c              mov       [esp+4Ch],ecx

LAB_00535974:
  00535974  8b54244c              mov       edx,[esp+4Ch]
  00535978  8b0d0c876000          mov       ecx,[60870Ch]
  0053597e  8b02                  mov       eax,[edx]
  00535980  8901                  mov       [ecx],eax
  00535982  a108876000            mov       eax,[608708h]
  00535987  8b0d0c876000          mov       ecx,[60870Ch]
  0053598d  8b1510876000          mov       edx,[608710h]
  00535993  0fbf18                movsx     ebx,word [eax]
  00535996  0fbf7802              movsx     edi,word [eax+2]
  0053599a  0fbf01                movsx     eax,word [ecx]
  0053599d  0fbf7202              movsx     esi,word [edx+2]
  005359a1  8be8                  mov       ebp,eax
  005359a3  895c2420              mov       [esp+20h],ebx
  005359a7  0fbf12                movsx     edx,word [edx]
  005359aa  0fafde                imul      ebx,esi
  005359ad  0fafef                imul      ebp,edi
  005359b0  0fafc6                imul      eax,esi
  005359b3  0fbf4902              movsx     ecx,word [ecx+2]
  005359b7  03dd                  add       ebx,ebp
  005359b9  8bea                  mov       ebp,edx
  005359bb  0fafe9                imul      ebp,ecx
  005359be  0faf4c2420            imul      ecx,[esp+20h]
  005359c3  0fafd7                imul      edx,edi
  005359c6  03dd                  add       ebx,ebp
  005359c8  8b7c244c              mov       edi,[esp+4Ch]
  005359cc  2bda                  sub       ebx,edx
  005359ce  8b1514876000          mov       edx,[608714h]
  005359d4  2bd8                  sub       ebx,eax
  005359d6  8b442450              mov       eax,[esp+50h]
  005359da  2bd9                  sub       ebx,ecx
  005359dc  8b4c2410              mov       ecx,[esp+10h]
  005359e0  891a                  mov       [edx],ebx
  005359e2  8b1514876000          mov       edx,[608714h]
  005359e8  03c0                  add       eax,eax
  005359ea  be04000000            mov       esi,4
  005359ef  c1e91f                shr       ecx,1Fh
  005359f2  0bc1                  or        eax,ecx
  005359f4  8b0a                  mov       ecx,[edx]
  005359f6  8b542414              mov       edx,[esp+14h]
  005359fa  03fe                  add       edi,esi
  005359fc  4a                    dec       edx
  005359fd  89442450              mov       [esp+50h],eax
  00535a01  894c2410              mov       [esp+10h],ecx
  00535a05  897c244c              mov       [esp+4Ch],edi
  00535a09  89542414              mov       [esp+14h],edx
  00535a0d  0f8561ffffff          jne       near 00535974h
  00535a13  8b542418              mov       edx,[esp+18h]
  00535a17  8b7c241c              mov       edi,[esp+1Ch]
  00535a1b  03d6                  add       edx,esi
  00535a1d  4f                    dec       edi
  00535a1e  89542418              mov       [esp+18h],edx
  00535a22  897c241c              mov       [esp+1Ch],edi
  00535a26  0f8528ffffff          jne       near 00535954h
  00535a2c  5f                    pop       edi
  00535a2d  03c0                  add       eax,eax
  00535a2f  c1e91f                shr       ecx,1Fh
  00535a32  5e                    pop       esi
  00535a33  5d                    pop       ebp
  00535a34  0bc1                  or        eax,ecx
  00535a36  5b                    pop       ebx
  00535a37  83c438                add       esp,38h
  00535a3a  c3                    ret
  00535a3b  90                    nop
  00535a3c  90                    nop
  00535a3d  90                    nop
  00535a3e  90                    nop
  00535a3f  90                    nop

2 cible(s) de saut interne : 0x00535954, 0x00535974
