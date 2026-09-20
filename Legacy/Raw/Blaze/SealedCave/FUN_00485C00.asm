  00485c00  53                    push      ebx
  00485c01  55                    push      ebp
  00485c02  56                    push      esi
  00485c03  8b742410              mov       esi,[esp+10h]
  00485c07  57                    push      edi
  00485c08  33db                  xor       ebx,ebx
  00485c0a  8b466c                mov       eax,[esi+6Ch]
  00485c0d  8d7e1c                lea       edi,[esi+1Ch]
  00485c10  66395e10              cmp       [esi+10h],bx
  00485c14  8b481c                mov       ecx,[eax+1Ch]
  00485c17  890f                  mov       [edi],ecx
  00485c19  8b5020                mov       edx,[eax+20h]
  00485c1c  895620                mov       [esi+20h],edx
  00485c1f  8b4824                mov       ecx,[eax+24h]
  00485c22  894e24                mov       [esi+24h],ecx
  00485c25  0f858c000000          jne       near 00485CB7h
  00485c2b  0fbf6e32              movsx     ebp,word [esi+32h]
  00485c2f  81c52c010000          add       ebp,12Ch
  00485c35  894674                mov       [esi+74h],eax
  00485c38  55                    push      ebp
  00485c39  894670                mov       [esi+70h],eax
  00485c3c  895e50                mov       [esi+50h],ebx
  00485c3f  895e54                mov       [esi+54h],ebx
  00485c42  895e58                mov       [esi+58h],ebx
  00485c45  66c746121f00          mov       word [esi+12h],1Fh
  00485c4b  e8ccfd0b00            call      00545A1Ch
  00485c50  8d0480                lea       eax,[eax+eax*4]
  00485c53  55                    push      ebp
  00485c54  8d1480                lea       edx,[eax+eax*4]
  00485c57  c1e202                shl       edx,2
  00485c5a  895644                mov       [esi+44h],edx
  00485c5d  e8b4fd0b00            call      00545A16h
  00485c62  8d0480                lea       eax,[eax+eax*4]
  00485c65  6a0d                  push      0Dh
  00485c67  6a01                  push      1
  00485c69  68be000000            push      0BEh
  00485c6e  8d0480                lea       eax,[eax+eax*4]
  00485c71  68b01b5500            push      551BB0h
  00485c76  c1e002                shl       eax,2
  00485c79  57                    push      edi
  00485c7a  56                    push      esi
  00485c7b  89464c                mov       [esi+4Ch],eax
  00485c7e  c7464800e01a00        mov       dword [esi+48h],1AE000h
  00485c85  895e28                mov       [esi+28h],ebx
  00485c88  66c746140040          mov       word [esi+14h],4000h
  00485c8e  66c746160200          mov       word [esi+16h],2
  00485c94  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  00485c9b  66c7461a0100          mov       word [esi+1Ah],1
  00485ca1  e89a4fffff            call      0047AC40h
  00485ca6  83c418                add       esp,18h
  00485ca9  57                    push      edi
  00485caa  6858275500            push      552758h
  00485caf  e82c7f0b00            call      0053DBE0h
  00485cb4  83c408                add       esp,8
  00485cb7  66837e100e            cmp       word [esi+10h],0Eh
  00485cbc  0f85db000000          jne       near 00485D9Dh
  00485cc2  6a0d                  push      0Dh
  00485cc4  6a01                  push      1
  00485cc6  68be000000            push      0BEh
  00485ccb  68b01b5500            push      551BB0h
  00485cd0  bdf0f0f000            mov       ebp,0F0F0F0h
  00485cd5  57                    push      edi
  00485cd6  56                    push      esi
  00485cd7  c74628fc3f0000        mov       dword [esi+28h],3FFCh
  00485cde  66895e14              mov       [esi+14h],bx
  00485ce2  66c746160300          mov       word [esi+16h],3
  00485ce8  896e38                mov       [esi+38h],ebp
  00485ceb  66c7461a0100          mov       word [esi+1Ah],1
  00485cf1  e84a4fffff            call      0047AC40h
  00485cf6  8b4e44                mov       ecx,[esi+44h]
  00485cf9  8b17                  mov       edx,[edi]
  00485cfb  8b4620                mov       eax,[esi+20h]
  00485cfe  03d1                  add       edx,ecx
  00485d00  8b4e24                mov       ecx,[esi+24h]
  00485d03  8917                  mov       [edi],edx
  00485d05  8b5648                mov       edx,[esi+48h]
  00485d08  83c418                add       esp,18h
  00485d0b  03c2                  add       eax,edx
  00485d0d  66c746340004          mov       word [esi+34h],400h
  00485d13  894620                mov       [esi+20h],eax
  00485d16  8b464c                mov       eax,[esi+4Ch]
  00485d19  03c8                  add       ecx,eax
  00485d1b  896e38                mov       [esi+38h],ebp
  00485d1e  894e24                mov       [esi+24h],ecx
  00485d21  66c746360400          mov       word [esi+36h],4
  00485d27  66c7462ef0ff          mov       word [esi+2Eh],0FFF0h
  00485d2d  c7462800600070        mov       dword [esi+28h],70006000h
  00485d34  66c7462c0070          mov       word [esi+2Ch],7000h
  00485d3a  e801140c00            call      00547140h
  00485d3f  2403                  and       al,3
  00485d41  6a0d                  push      0Dh
  00485d43  6a01                  push      1
  00485d45  0408                  add       al,8
  00485d47  6883000000            push      83h
  00485d4c  56                    push      esi
  00485d4d  884604                mov       [esi+4],al
  00485d50  e88b4dffff            call      0047AAE0h
  00485d55  668146320004          add       word [esi+32h],400h
  00485d5b  83c410                add       esp,10h
  00485d5e  e8dd130c00            call      00547140h
  00485d63  2403                  and       al,3
  00485d65  6a0d                  push      0Dh
  00485d67  6a01                  push      1
  00485d69  0408                  add       al,8
  00485d6b  6883000000            push      83h
  00485d70  56                    push      esi
  00485d71  884604                mov       [esi+4],al
  00485d74  e8674dffff            call      0047AAE0h
  00485d79  83c410                add       esp,10h
  00485d7c  c705a0935e00ff004000  mov       dword [5E93A0h],4000FFh
  00485d86  66c705a4935e004000    mov       word [5E93A4h],40h
  00485d8f  57                    push      edi
  00485d90  6868275500            push      552768h
  00485d95  e8467e0b00            call      0053DBE0h
  00485d9a  83c408                add       esp,8
  00485d9d  668b4610              mov       ax,[esi+10h]
  00485da1  663d1e00              cmp       ax,1Eh
  00485da5  0f8efb000000          jle       near 00485EA6h
  00485dab  a803                  test      al,3
  00485dad  0f85f3000000          jne       near 00485EA6h
  00485db3  e888130c00            call      00547140h
  00485db8  89442414              mov       [esp+14h],eax
  00485dbc  e87f130c00            call      00547140h
  00485dc1  8b4c2414              mov       ecx,[esp+14h]
  00485dc5  8be8                  mov       ebp,eax
  00485dc7  51                    push      ecx
  00485dc8  81e5ff010000          and       ebp,1FFh
  00485dce  e849fc0b00            call      00545A1Ch
  00485dd3  0fafc5                imul      eax,ebp
  00485dd6  8b0f                  mov       ecx,[edi]
  00485dd8  8b542414              mov       edx,[esp+14h]
  00485ddc  03c8                  add       ecx,eax
  00485dde  52                    push      edx
  00485ddf  890f                  mov       [edi],ecx
  00485de1  e830fc0b00            call      00545A16h
  00485de6  0fafc5                imul      eax,ebp
  00485de9  8b5624                mov       edx,[esi+24h]
  00485dec  668b4c2414            mov       cx,[esp+14h]
  00485df1  03d0                  add       edx,eax
  00485df3  8b4660                mov       eax,[esi+60h]
  00485df6  895624                mov       [esi+24h],edx
  00485df9  894620                mov       [esi+20h],eax
  00485dfc  66894e32              mov       [esi+32h],cx
  00485e00  66c746340004          mov       word [esi+34h],400h
  00485e06  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00485e0d  66c746360400          mov       word [esi+36h],4
  00485e13  66c7462ef0ff          mov       word [esi+2Eh],0FFF0h
  00485e19  c7462800600070        mov       dword [esi+28h],70006000h
  00485e20  66c7462c0070          mov       word [esi+2Ch],7000h
  00485e26  e815130c00            call      00547140h
  00485e2b  2403                  and       al,3
  00485e2d  6a0d                  push      0Dh
  00485e2f  0408                  add       al,8
  00485e31  6a01                  push      1
  00485e33  884604                mov       [esi+4],al
  00485e36  8b15cc975e00          mov       edx,[5E97CCh]
  00485e3c  8b82d0000000          mov       eax,[edx+0D0h]
  00485e42  33d2                  xor       edx,edx
  00485e44  8b08                  mov       ecx,[eax]
  00485e46  8a5104                mov       dl,[ecx+4]
  00485e49  81c2c0000000          add       edx,0C0h
  00485e4f  52                    push      edx
  00485e50  56                    push      esi
  00485e51  e88a4cffff            call      0047AAE0h
  00485e56  83c410                add       esp,10h
  00485e59  b800400000            mov       eax,4000h
  00485e5e  895e70                mov       [esi+70h],ebx
  00485e61  895e44                mov       [esi+44h],ebx
  00485e64  6a0d                  push      0Dh
  00485e66  6a01                  push      1
  00485e68  6a6c                  push      6Ch
  00485e6a  56                    push      esi
  00485e6b  895e48                mov       [esi+48h],ebx
  00485e6e  895e4c                mov       [esi+4Ch],ebx
  00485e71  894650                mov       [esi+50h],eax
  00485e74  894654                mov       [esi+54h],eax
  00485e77  894658                mov       [esi+58h],eax
  00485e7a  c7463880808000        mov       dword [esi+38h],808080h
  00485e81  895e28                mov       [esi+28h],ebx
  00485e84  66895e2c              mov       [esi+2Ch],bx
  00485e88  66895e34              mov       [esi+34h],bx
  00485e8c  c6460426              mov       byte [esi+4],26h
  00485e90  e84b4cffff            call      0047AAE0h
  00485e95  83c410                add       esp,10h
  00485e98  57                    push      edi
  00485e99  6880275500            push      552780h
  00485e9e  e83d7d0b00            call      0053DBE0h
  00485ea3  83c408                add       esp,8
  00485ea6  66ff4610              inc       word [esi+10h]
  00485eaa  66837e103c            cmp       word [esi+10h],3Ch
  00485eaf  7c02                  jl        short 00485EB3h
  00485eb1  891e                  mov       [esi],ebx
  00485eb3  5f                    pop       edi
  00485eb4  5e                    pop       esi
  00485eb5  5d                    pop       ebp
  00485eb6  5b                    pop       ebx
  00485eb7  c3                    ret
