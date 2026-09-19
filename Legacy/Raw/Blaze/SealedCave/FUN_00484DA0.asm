Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484da0  section .text  offset fichier 0x841a0  224 octets

  00484da0  83ec0c                sub       esp,0Ch
  00484da3  56                    push      esi
  00484da4  8b742414              mov       esi,[esp+14h]
  00484da8  57                    push      edi
  00484da9  66c74424080000        mov       word [esp+8],0
  00484db0  8b06                  mov       eax,[esi]
  00484db2  66c744240a38ff        mov       word [esp+0Ah],0FF38h
  00484db9  a900000002            test      eax,2000000h
  00484dbe  66c744240cc800        mov       word [esp+0Ch],0C8h
  00484dc5  66c744240e3200        mov       word [esp+0Eh],32h
  00484dcc  66c74424100000        mov       word [esp+10h],0
  00484dd3  66c74424129001        mov       word [esp+12h],190h
  00484dda  745e                  je        short 00484E3Ah
  00484ddc  668b4610              mov       ax,[esi+10h]
  00484de0  663d2800              cmp       ax,28h
  00484de4  7d3b                  jge       short 00484E21h
  00484de6  40                    inc       eax
  00484de7  66894610              mov       [esi+10h],ax
  00484deb  8d442408              lea       eax,[esp+8]
  00484def  50                    push      eax
  00484df0  56                    push      esi
  00484df1  e8aafefeff            call      00474CA0h
  00484df6  8bf8                  mov       edi,eax
  00484df8  83c408                add       esp,8
  00484dfb  85ff                  test      edi,edi
  00484dfd  746f                  je        short 00484E6Eh
  00484dff  8b4e74                mov       ecx,[esi+74h]
  00484e02  57                    push      edi
  00484e03  51                    push      ecx
  00484e04  e8774d0b00            call      00539B80h
  00484e09  8b07                  mov       eax,[edi]
  00484e0b  83c408                add       esp,8
  00484e0e  0c40                  or        al,40h
  00484e10  66c787960000000800    mov       word [edi+96h],8
  00484e19  8907                  mov       [edi],eax
  00484e1b  5f                    pop       edi
  00484e1c  5e                    pop       esi
  00484e1d  83c40c                add       esp,0Ch
  00484e20  c3                    ret

LAB_00484e21:
  00484e21  8b4638                mov       eax,[esi+38h]
  00484e24  05f8f7f7ff            add       eax,0FFF7F7F8h
  00484e29  894638                mov       [esi+38h],eax
  00484e2c  7540                  jne       short 00484E6Eh
  00484e2e  c70600000000          mov       dword [esi],0
  00484e34  5f                    pop       edi
  00484e35  5e                    pop       esi
  00484e36  83c40c                add       esp,0Ch
  00484e39  c3                    ret

LAB_00484e3a:
  00484e3a  0fbf4e28              movsx     ecx,word [esi+28h]
  00484e3e  ba00400000            mov       edx,4000h
  00484e43  2bd1                  sub       edx,ecx
  00484e45  c1fa03                sar       edx,3
  00484e48  03ca                  add       ecx,edx
  00484e4a  8b5638                mov       edx,[esi+38h]
  00484e4d  81c208080800          add       edx,80808h
  00484e53  894e28                mov       [esi+28h],ecx
  00484e56  66894e2c              mov       [esi+2Ch],cx
  00484e5a  8bca                  mov       ecx,edx
  00484e5c  81f980808000          cmp       ecx,808080h
  00484e62  895638                mov       [esi+38h],edx
  00484e65  7507                  jne       short 00484E6Eh
  00484e67  0d00000002            or        eax,2000000h
  00484e6c  8906                  mov       [esi],eax

LAB_00484e6e:
  00484e6e  5f                    pop       edi
  00484e6f  5e                    pop       esi
  00484e70  83c40c                add       esp,0Ch
  00484e73  c3                    ret
  00484e74  90                    nop
  00484e75  90                    nop
  00484e76  90                    nop
  00484e77  90                    nop
  00484e78  90                    nop
  00484e79  90                    nop
  00484e7a  90                    nop
  00484e7b  90                    nop
  00484e7c  90                    nop
  00484e7d  90                    nop
  00484e7e  90                    nop
  00484e7f  90                    nop

3 cible(s) de saut interne : 0x00484e21, 0x00484e3a, 0x00484e6e
