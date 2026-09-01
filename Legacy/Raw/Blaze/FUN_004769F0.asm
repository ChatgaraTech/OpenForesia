Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004769f0  section .text  offset fichier 0x75df0  640 octets

  004769f0  83ec20                sub       esp,20h
  004769f3  53                    push      ebx
  004769f4  55                    push      ebp
  004769f5  56                    push      esi
  004769f6  8b742430              mov       esi,[esp+30h]
  004769fa  57                    push      edi
  004769fb  33ff                  xor       edi,edi
  004769fd  66397e10              cmp       [esi+10h],di
  00476a01  b864000000            mov       eax,64h
  00476a06  66897c2418            mov       [esp+18h],di
  00476a0b  66c744241aceff        mov       word [esp+1Ah],0FFCEh
  00476a12  66c744241c3200        mov       word [esp+1Ch],32h
  00476a19  668944241e            mov       [esp+1Eh],ax
  00476a1e  66897c2420            mov       [esp+20h],di
  00476a23  6689442422            mov       [esp+22h],ax
  00476a28  755d                  jne       short 00476A87h
  00476a2a  8b5e74                mov       ebx,[esi+74h]
  00476a2d  b8000c0000            mov       eax,0C00h
  00476a32  53                    push      ebx
  00476a33  66894628              mov       [esi+28h],ax
  00476a37  6689462a              mov       [esi+2Ah],ax
  00476a3b  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00476a42  e879210c00            call      00538BC0h
  00476a47  8b4e20                mov       ecx,[esi+20h]
  00476a4a  24fe                  and       al,0FEh
  00476a4c  c1e00b                shl       eax,0Bh
  00476a4f  03c8                  add       ecx,eax
  00476a51  83c404                add       esp,4
  00476a54  0fbf4632              movsx     eax,word [esi+32h]
  00476a58  50                    push      eax
  00476a59  894e20                mov       [esi+20h],ecx
  00476a5c  e8bbef0c00            call      00545A1Ch
  00476a61  0fbf5632              movsx     edx,word [esi+32h]
  00476a65  8d0c40                lea       ecx,[eax+eax*2]
  00476a68  8b4350                mov       eax,[ebx+50h]
  00476a6b  c1e104                shl       ecx,4
  00476a6e  03c8                  add       ecx,eax
  00476a70  52                    push      edx
  00476a71  894e50                mov       [esi+50h],ecx
  00476a74  e89def0c00            call      00545A16h
  00476a79  8b4b58                mov       ecx,[ebx+58h]
  00476a7c  8d0440                lea       eax,[eax+eax*2]
  00476a7f  c1e004                shl       eax,4
  00476a82  03c1                  add       eax,ecx
  00476a84  894658                mov       [esi+58h],eax

LAB_00476a87:
  00476a87  8a4638                mov       al,[esi+38h]
  00476a8a  3c18                  cmp       al,18h
  00476a8c  7219                  jb        short 00476AA7h
  00476a8e  8a5639                mov       dl,[esi+39h]
  00476a91  8a4e3a                mov       cl,[esi+3Ah]
  00476a94  2c18                  sub       al,18h
  00476a96  884638                mov       [esi+38h],al
  00476a99  b0e8                  mov       al,0E8h
  00476a9b  02d0                  add       dl,al
  00476a9d  02c8                  add       cl,al
  00476a9f  885639                mov       [esi+39h],dl
  00476aa2  884e3a                mov       [esi+3Ah],cl
  00476aa5  eb06                  jmp       short 00476AADh

LAB_00476aa7:
  00476aa7  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00476aad:
  00476aad  807e3860              cmp       byte [esi+38h],60h
  00476ab1  0f86d3000000          jbe       near 00476B8Ah
  00476ab7  8b4e54                mov       ecx,[esi+54h]
  00476aba  8b5658                mov       edx,[esi+58h]
  00476abd  8b4628                mov       eax,[esi+28h]
  00476ac0  8b6e50                mov       ebp,[esi+50h]
  00476ac3  8d5e50                lea       ebx,[esi+50h]
  00476ac6  894c2428              mov       [esp+28h],ecx
  00476aca  8b4e2c                mov       ecx,[esi+2Ch]
  00476acd  8954242c              mov       [esp+2Ch],edx
  00476ad1  8b5638                mov       edx,[esi+38h]
  00476ad4  89442434              mov       [esp+34h],eax
  00476ad8  894c2410              mov       [esp+10h],ecx
  00476adc  89542414              mov       [esp+14h],edx
  00476ae0  897e70                mov       [esi+70h],edi
  00476ae3  e858060d00            call      00547140h
  00476ae8  0fbf7e32              movsx     edi,word [esi+32h]
  00476aec  25ff010000            and       eax,1FFh
  00476af1  2bf8                  sub       edi,eax
  00476af3  81c700010000          add       edi,100h
  00476af9  e842060d00            call      00547140h
  00476afe  8bc8                  mov       ecx,eax
  00476b00  6a10                  push      10h
  00476b02  0fbf4634              movsx     eax,word [esi+34h]
  00476b06  81e1ff010000          and       ecx,1FFh
  00476b0c  2bc1                  sub       eax,ecx
  00476b0e  0500090000            add       eax,900h
  00476b13  50                    push      eax
  00476b14  57                    push      edi
  00476b15  53                    push      ebx
  00476b16  e8b5bf0b00            call      00532AD0h
  00476b1b  83c410                add       esp,10h
  00476b1e  33d2                  xor       edx,edx
  00476b20  8a560b                mov       dl,[esi+0Bh]
  00476b23  8d461c                lea       eax,[esi+1Ch]
  00476b26  6aff                  push      0FFFFFFFFh
  00476b28  52                    push      edx
  00476b29  68be000000            push      0BEh
  00476b2e  68a81b5500            push      551BA8h
  00476b33  50                    push      eax
  00476b34  56                    push      esi
  00476b35  66c746120300          mov       word [esi+12h],3
  00476b3b  c7462840064006        mov       dword [esi+28h],6400640h
  00476b42  66c746144006          mov       word [esi+14h],640h
  00476b48  66c746161f00          mov       word [esi+16h],1Fh
  00476b4e  c7463880808000        mov       dword [esi+38h],808080h
  00476b55  66c7461a0100          mov       word [esi+1Ah],1
  00476b5b  e8e0400000            call      0047AC40h
  00476b60  8b4c2440              mov       ecx,[esp+40h]
  00476b64  8b542444              mov       edx,[esp+44h]
  00476b68  8b44244c              mov       eax,[esp+4Ch]
  00476b6c  894e54                mov       [esi+54h],ecx
  00476b6f  8b4c2428              mov       ecx,[esp+28h]
  00476b73  895658                mov       [esi+58h],edx
  00476b76  8b54242c              mov       edx,[esp+2Ch]
  00476b7a  83c418                add       esp,18h
  00476b7d  892b                  mov       [ebx],ebp
  00476b7f  894628                mov       [esi+28h],eax
  00476b82  894e2c                mov       [esi+2Ch],ecx
  00476b85  895638                mov       [esi+38h],edx
  00476b88  33ff                  xor       edi,edi

LAB_00476b8a:
  00476b8a  8b4e58                mov       ecx,[esi+58h]
  00476b8d  8b5624                mov       edx,[esi+24h]
  00476b90  8b4650                mov       eax,[esi+50h]
  00476b93  8b6e1c                mov       ebp,[esi+1Ch]
  00476b96  03d1                  add       edx,ecx
  00476b98  8d5e1c                lea       ebx,[esi+1Ch]
  00476b9b  895624                mov       [esi+24h],edx
  00476b9e  8bd0                  mov       edx,eax
  00476ba0  c1fa02                sar       edx,2
  00476ba3  03e8                  add       ebp,eax
  00476ba5  2bc2                  sub       eax,edx
  00476ba7  894650                mov       [esi+50h],eax
  00476baa  8bc1                  mov       eax,ecx
  00476bac  c1f802                sar       eax,2
  00476baf  2bc8                  sub       ecx,eax
  00476bb1  892b                  mov       [ebx],ebp
  00476bb3  8b6e20                mov       ebp,[esi+20h]
  00476bb6  56                    push      esi
  00476bb7  894e58                mov       [esi+58h],ecx
  00476bba  e881cd0b00            call      00533940h
  00476bbf  8b8690000000          mov       eax,[esi+90h]
  00476bc5  83c404                add       esp,4
  00476bc8  f6c4a0                test      ah,0A0h
  00476bcb  896e20                mov       [esi+20h],ebp
  00476bce  7537                  jne       short 00476C07h
  00476bd0  a902080000            test      eax,802h
  00476bd5  7505                  jne       short 00476BDCh
  00476bd7  3b6e60                cmp       ebp,[esi+60h]
  00476bda  7f2b                  jg        short 00476C07h

LAB_00476bdc:
  00476bdc  8b4660                mov       eax,[esi+60h]
  00476bdf  3be8                  cmp       ebp,eax
  00476be1  7f03                  jg        short 00476BE6h
  00476be3  894620                mov       [esi+20h],eax

LAB_00476be6:
  00476be6  6a04                  push      4
  00476be8  57                    push      edi
  00476be9  68bd000000            push      0BDh
  00476bee  68081a5500            push      551A08h
  00476bf3  53                    push      ebx
  00476bf4  56                    push      esi
  00476bf5  e846400000            call      0047AC40h
  00476bfa  83c418                add       esp,18h
  00476bfd  893e                  mov       [esi],edi
  00476bff  5f                    pop       edi
  00476c00  5e                    pop       esi
  00476c01  5d                    pop       ebp
  00476c02  5b                    pop       ebx
  00476c03  83c420                add       esp,20h
  00476c06  c3                    ret

LAB_00476c07:
  00476c07  8d4c2418              lea       ecx,[esp+18h]
  00476c0b  51                    push      ecx
  00476c0c  56                    push      esi
  00476c0d  e85ee0ffff            call      00474C70h
  00476c12  8bf8                  mov       edi,eax
  00476c14  83c408                add       esp,8
  00476c17  85ff                  test      edi,edi
  00476c19  7444                  je        short 00476C5Fh
  00476c1b  8b5674                mov       edx,[esi+74h]
  00476c1e  57                    push      edi
  00476c1f  52                    push      edx
  00476c20  e85b2f0c00            call      00539B80h
  00476c25  8b4650                mov       eax,[esi+50h]
  00476c28  8b6f50                mov       ebp,[edi+50h]
  00476c2b  8b5758                mov       edx,[edi+58h]
  00476c2e  83c408                add       esp,8
  00476c31  d1f8                  sar       eax,1
  00476c33  03e8                  add       ebp,eax
  00476c35  8b07                  mov       eax,[edi]
  00476c37  896f50                mov       [edi+50h],ebp
  00476c3a  8b4e58                mov       ecx,[esi+58h]
  00476c3d  d1f9                  sar       ecx,1
  00476c3f  03d1                  add       edx,ecx
  00476c41  0c40                  or        al,40h
  00476c43  895758                mov       [edi+58h],edx
  00476c46  66c787960000001400    mov       word [edi+96h],14h
  00476c4f  8907                  mov       [edi],eax
  00476c51  c70600000000          mov       dword [esi],0
  00476c57  5f                    pop       edi
  00476c58  5e                    pop       esi
  00476c59  5d                    pop       ebp
  00476c5a  5b                    pop       ebx
  00476c5b  83c420                add       esp,20h
  00476c5e  c3                    ret

LAB_00476c5f:
  00476c5f  66ff4610              inc       word [esi+10h]
  00476c63  5f                    pop       edi
  00476c64  5e                    pop       esi
  00476c65  5d                    pop       ebp
  00476c66  5b                    pop       ebx
  00476c67  83c420                add       esp,20h
  00476c6a  c3                    ret
  00476c6b  90                    nop
  00476c6c  90                    nop
  00476c6d  90                    nop
  00476c6e  90                    nop
  00476c6f  90                    nop

8 cible(s) de saut interne : 0x00476a87, 0x00476aa7, 0x00476aad, 0x00476b8a, 0x00476bdc, 0x00476be6, 0x00476c07, 0x00476c5f
