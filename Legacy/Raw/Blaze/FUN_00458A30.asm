Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00458a30  section .text  offset fichier 0x57e30  943 octets

  00458a30  83ec10                sub       esp,10h
  00458a33  53                    push      ebx
  00458a34  56                    push      esi
  00458a35  8b74241c              mov       esi,[esp+1Ch]
  00458a39  57                    push      edi
  00458a3a  b927000000            mov       ecx,27h
  00458a3f  bfb0155700            mov       edi,5715B0h
  00458a44  f3a5                  rep movsd
  00458a46  a1cc155700            mov       eax,[5715CCh]
  00458a4b  8b0dd0155700          mov       ecx,[5715D0h]
  00458a51  8b15d4155700          mov       edx,[5715D4h]
  00458a57  8944240c              mov       [esp+0Ch],eax
  00458a5b  0fbf05e2155700        movsx     eax,word [5715E2h]
  00458a62  0500040000            add       eax,400h
  00458a67  33db                  xor       ebx,ebx
  00458a69  50                    push      eax
  00458a6a  894c2414              mov       [esp+14h],ecx
  00458a6e  89542418              mov       [esp+18h],edx
  00458a72  e8a5cf0e00            call      00545A1Ch
  00458a77  8d0c80                lea       ecx,[eax+eax*4]
  00458a7a  8d0448                lea       eax,[eax+ecx*2]
  00458a7d  8d1480                lea       edx,[eax+eax*4]
  00458a80  a1cc155700            mov       eax,[5715CCh]
  00458a85  8d0c90                lea       ecx,[eax+edx*4]
  00458a88  0fbf15e2155700        movsx     edx,word [5715E2h]
  00458a8f  81c200040000          add       edx,400h
  00458a95  890dcc155700          mov       [5715CCh],ecx
  00458a9b  52                    push      edx
  00458a9c  e875cf0e00            call      00545A16h
  00458aa1  8d0c80                lea       ecx,[eax+eax*4]
  00458aa4  6800200000            push      2000h
  00458aa9  8d0448                lea       eax,[eax+ecx*2]
  00458aac  8d1480                lea       edx,[eax+eax*4]
  00458aaf  a1d4155700            mov       eax,[5715D4h]
  00458ab4  8d0c90                lea       ecx,[eax+edx*4]
  00458ab7  8d542410              lea       edx,[esp+10h]
  00458abb  52                    push      edx
  00458abc  68b0155700            push      5715B0h
  00458ac1  890dd4155700          mov       [5715D4h],ecx
  00458ac7  ff15c4b05600          call      dword [56B0C4h]
  00458acd  a140165700            mov       eax,[571640h]
  00458ad2  be00880000            mov       esi,8800h
  00458ad7  83c40c                add       esp,0Ch
  00458ada  85c6                  test      esi,eax
  00458adc  7405                  je        short 00458AE3h
  00458ade  bb01000000            mov       ebx,1

LAB_00458ae3:
  00458ae3  a1cc155700            mov       eax,[5715CCh]
  00458ae8  8b0dd0155700          mov       ecx,[5715D0h]
  00458aee  8b15d4155700          mov       edx,[5715D4h]
  00458af4  8944240c              mov       [esp+0Ch],eax
  00458af8  0fbf05e2155700        movsx     eax,word [5715E2h]
  00458aff  05000e0000            add       eax,0E00h
  00458b04  894c2410              mov       [esp+10h],ecx
  00458b08  50                    push      eax
  00458b09  89542418              mov       [esp+18h],edx
  00458b0d  e80acf0e00            call      00545A1Ch
  00458b12  8b15cc155700          mov       edx,[5715CCh]
  00458b18  8d0440                lea       eax,[eax+eax*2]
  00458b1b  8d0480                lea       eax,[eax+eax*4]
  00458b1e  8d0c80                lea       ecx,[eax+eax*4]
  00458b21  8d044a                lea       eax,[edx+ecx*2]
  00458b24  0fbf0de2155700        movsx     ecx,word [5715E2h]
  00458b2b  81c1000e0000          add       ecx,0E00h
  00458b31  a3cc155700            mov       [5715CCh],eax
  00458b36  51                    push      ecx
  00458b37  e8dace0e00            call      00545A16h
  00458b3c  8d0440                lea       eax,[eax+eax*2]
  00458b3f  6800200000            push      2000h
  00458b44  8d0480                lea       eax,[eax+eax*4]
  00458b47  8d1480                lea       edx,[eax+eax*4]
  00458b4a  a1d4155700            mov       eax,[5715D4h]
  00458b4f  8d0c50                lea       ecx,[eax+edx*2]
  00458b52  8d542410              lea       edx,[esp+10h]
  00458b56  52                    push      edx
  00458b57  68b0155700            push      5715B0h
  00458b5c  890dd4155700          mov       [5715D4h],ecx
  00458b62  ff15c4b05600          call      dword [56B0C4h]
  00458b68  a140165700            mov       eax,[571640h]
  00458b6d  83c40c                add       esp,0Ch
  00458b70  85c6                  test      esi,eax
  00458b72  7405                  je        short 00458B79h
  00458b74  bb02000000            mov       ebx,2

LAB_00458b79:
  00458b79  a1cc155700            mov       eax,[5715CCh]
  00458b7e  8b0dd0155700          mov       ecx,[5715D0h]
  00458b84  8b15d4155700          mov       edx,[5715D4h]
  00458b8a  8944240c              mov       [esp+0Ch],eax
  00458b8e  0fbf05e2155700        movsx     eax,word [5715E2h]
  00458b95  05000c0000            add       eax,0C00h
  00458b9a  894c2410              mov       [esp+10h],ecx
  00458b9e  50                    push      eax
  00458b9f  89542418              mov       [esp+18h],edx
  00458ba3  e874ce0e00            call      00545A1Ch
  00458ba8  8b15cc155700          mov       edx,[5715CCh]
  00458bae  8bc8                  mov       ecx,eax
  00458bb0  c1e105                shl       ecx,5
  00458bb3  03c8                  add       ecx,eax
  00458bb5  8d0c89                lea       ecx,[ecx+ecx*4]
  00458bb8  8d044a                lea       eax,[edx+ecx*2]
  00458bbb  0fbf0de2155700        movsx     ecx,word [5715E2h]
  00458bc2  81c1000c0000          add       ecx,0C00h
  00458bc8  a3cc155700            mov       [5715CCh],eax
  00458bcd  51                    push      ecx
  00458bce  e843ce0e00            call      00545A16h
  00458bd3  8bc8                  mov       ecx,eax
  00458bd5  6800200000            push      2000h
  00458bda  c1e105                shl       ecx,5
  00458bdd  03c8                  add       ecx,eax
  00458bdf  a1d4155700            mov       eax,[5715D4h]
  00458be4  8d1489                lea       edx,[ecx+ecx*4]
  00458be7  8d0c50                lea       ecx,[eax+edx*2]
  00458bea  8d542410              lea       edx,[esp+10h]
  00458bee  52                    push      edx
  00458bef  68b0155700            push      5715B0h
  00458bf4  890dd4155700          mov       [5715D4h],ecx
  00458bfa  ff15c4b05600          call      dword [56B0C4h]
  00458c00  a140165700            mov       eax,[571640h]
  00458c05  83c40c                add       esp,0Ch
  00458c08  85c6                  test      esi,eax
  00458c0a  7405                  je        short 00458C11h
  00458c0c  bb03000000            mov       ebx,3

LAB_00458c11:
  00458c11  a1cc155700            mov       eax,[5715CCh]
  00458c16  8b0dd0155700          mov       ecx,[5715D0h]
  00458c1c  8b15d4155700          mov       edx,[5715D4h]
  00458c22  8944240c              mov       [esp+0Ch],eax
  00458c26  0fbf05e2155700        movsx     eax,word [5715E2h]
  00458c2d  0500080000            add       eax,800h
  00458c32  894c2410              mov       [esp+10h],ecx
  00458c36  50                    push      eax
  00458c37  89542418              mov       [esp+18h],edx
  00458c3b  e8dccd0e00            call      00545A1Ch
  00458c40  8d0c80                lea       ecx,[eax+eax*4]
  00458c43  8d0448                lea       eax,[eax+ecx*2]
  00458c46  8d1480                lea       edx,[eax+eax*4]
  00458c49  a1cc155700            mov       eax,[5715CCh]
  00458c4e  8d0c90                lea       ecx,[eax+edx*4]
  00458c51  0fbf15e2155700        movsx     edx,word [5715E2h]
  00458c58  81c200080000          add       edx,800h
  00458c5e  890dcc155700          mov       [5715CCh],ecx
  00458c64  52                    push      edx
  00458c65  e8accd0e00            call      00545A16h
  00458c6a  8d0c80                lea       ecx,[eax+eax*4]
  00458c6d  6800200000            push      2000h
  00458c72  8d0448                lea       eax,[eax+ecx*2]
  00458c75  8d1480                lea       edx,[eax+eax*4]
  00458c78  a1d4155700            mov       eax,[5715D4h]
  00458c7d  8d0c90                lea       ecx,[eax+edx*4]
  00458c80  8d542410              lea       edx,[esp+10h]
  00458c84  52                    push      edx
  00458c85  68b0155700            push      5715B0h
  00458c8a  890dd4155700          mov       [5715D4h],ecx
  00458c90  ff15c4b05600          call      dword [56B0C4h]
  00458c96  a140165700            mov       eax,[571640h]
  00458c9b  83c40c                add       esp,0Ch
  00458c9e  85c6                  test      esi,eax
  00458ca0  7405                  je        short 00458CA7h
  00458ca2  bb04000000            mov       ebx,4

LAB_00458ca7:
  00458ca7  a1cc155700            mov       eax,[5715CCh]
  00458cac  8b0dd0155700          mov       ecx,[5715D0h]
  00458cb2  8b15d4155700          mov       edx,[5715D4h]
  00458cb8  8944240c              mov       [esp+0Ch],eax
  00458cbc  0fbf05e2155700        movsx     eax,word [5715E2h]
  00458cc3  0500040000            add       eax,400h
  00458cc8  894c2410              mov       [esp+10h],ecx
  00458ccc  50                    push      eax
  00458ccd  89542418              mov       [esp+18h],edx
  00458cd1  e846cd0e00            call      00545A1Ch
  00458cd6  8b15cc155700          mov       edx,[5715CCh]
  00458cdc  8bc8                  mov       ecx,eax
  00458cde  c1e105                shl       ecx,5
  00458ce1  03c8                  add       ecx,eax
  00458ce3  8d0c89                lea       ecx,[ecx+ecx*4]
  00458ce6  8d044a                lea       eax,[edx+ecx*2]
  00458ce9  0fbf0de2155700        movsx     ecx,word [5715E2h]
  00458cf0  81c100040000          add       ecx,400h
  00458cf6  a3cc155700            mov       [5715CCh],eax
  00458cfb  51                    push      ecx
  00458cfc  e815cd0e00            call      00545A16h
  00458d01  8bc8                  mov       ecx,eax
  00458d03  6800200000            push      2000h
  00458d08  c1e105                shl       ecx,5
  00458d0b  03c8                  add       ecx,eax
  00458d0d  a1d4155700            mov       eax,[5715D4h]
  00458d12  8d1489                lea       edx,[ecx+ecx*4]
  00458d15  8d0c50                lea       ecx,[eax+edx*2]
  00458d18  8d542410              lea       edx,[esp+10h]
  00458d1c  52                    push      edx
  00458d1d  68b0155700            push      5715B0h
  00458d22  890dd4155700          mov       [5715D4h],ecx
  00458d28  ff15c4b05600          call      dword [56B0C4h]
  00458d2e  a140165700            mov       eax,[571640h]
  00458d33  83c40c                add       esp,0Ch
  00458d36  85c6                  test      esi,eax
  00458d38  7405                  je        short 00458D3Fh
  00458d3a  bb05000000            mov       ebx,5

LAB_00458d3f:
  00458d3f  a1cc155700            mov       eax,[5715CCh]
  00458d44  8b0dd0155700          mov       ecx,[5715D0h]
  00458d4a  8b15d4155700          mov       edx,[5715D4h]
  00458d50  8944240c              mov       [esp+0Ch],eax
  00458d54  0fbf05e2155700        movsx     eax,word [5715E2h]
  00458d5b  0500020000            add       eax,200h
  00458d60  894c2410              mov       [esp+10h],ecx
  00458d64  50                    push      eax
  00458d65  89542418              mov       [esp+18h],edx
  00458d69  e8aecc0e00            call      00545A1Ch
  00458d6e  8b15cc155700          mov       edx,[5715CCh]
  00458d74  8d0440                lea       eax,[eax+eax*2]
  00458d77  8d0480                lea       eax,[eax+eax*4]
  00458d7a  8d0c80                lea       ecx,[eax+eax*4]
  00458d7d  8d044a                lea       eax,[edx+ecx*2]
  00458d80  0fbf0de2155700        movsx     ecx,word [5715E2h]
  00458d87  81c100020000          add       ecx,200h
  00458d8d  a3cc155700            mov       [5715CCh],eax
  00458d92  51                    push      ecx
  00458d93  e87ecc0e00            call      00545A16h
  00458d98  8d0440                lea       eax,[eax+eax*2]
  00458d9b  6800200000            push      2000h
  00458da0  8d0480                lea       eax,[eax+eax*4]
  00458da3  8d1480                lea       edx,[eax+eax*4]
  00458da6  a1d4155700            mov       eax,[5715D4h]
  00458dab  8d0c50                lea       ecx,[eax+edx*2]
  00458dae  8d542410              lea       edx,[esp+10h]
  00458db2  52                    push      edx
  00458db3  68b0155700            push      5715B0h
  00458db8  890dd4155700          mov       [5715D4h],ecx
  00458dbe  ff15c4b05600          call      dword [56B0C4h]
  00458dc4  a140165700            mov       eax,[571640h]
  00458dc9  83c40c                add       esp,0Ch
  00458dcc  85c6                  test      esi,eax
  00458dce  7405                  je        short 00458DD5h
  00458dd0  bb06000000            mov       ebx,6

LAB_00458dd5:
  00458dd5  5f                    pop       edi
  00458dd6  668bc3                mov       ax,bx
  00458dd9  5e                    pop       esi
  00458dda  5b                    pop       ebx
  00458ddb  83c410                add       esp,10h
  00458dde  c3                    ret

6 cible(s) de saut interne : 0x00458ae3, 0x00458b79, 0x00458c11, 0x00458ca7, 0x00458d3f, 0x00458dd5
