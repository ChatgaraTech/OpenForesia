Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040bca0  section .text  offset fichier 0xb0a0  432 octets

  0040bca0  53                    push      ebx
  0040bca1  56                    push      esi
  0040bca2  8b74240c              mov       esi,[esp+0Ch]
  0040bca6  33db                  xor       ebx,ebx
  0040bca8  57                    push      edi
  0040bca9  66395e10              cmp       [esi+10h],bx
  0040bcad  0f85c1000000          jne       near 0040BD74h
  0040bcb3  c7463ce0f55400        mov       dword [esi+3Ch],54F5E0h
  0040bcba  885e40                mov       [esi+40h],bl
  0040bcbd  e87eb41300            call      00547140h
  0040bcc2  99                    cdq
  0040bcc3  b9e8030000            mov       ecx,3E8h
  0040bcc8  f7f9                  idiv      ecx
  0040bcca  8d82dc050000          lea       eax,[edx+5DCh]
  0040bcd0  66894628              mov       [esi+28h],ax
  0040bcd4  6689462a              mov       [esi+2Ah],ax
  0040bcd8  e863b41300            call      00547140h
  0040bcdd  99                    cdq
  0040bcde  b903000000            mov       ecx,3
  0040bce3  f7f9                  idiv      ecx
  0040bce5  6683fa01              cmp       dx,1
  0040bce9  7f06                  jg        short 0040BCF1h
  0040bceb  c6460a09              mov       byte [esi+0Ah],9
  0040bcef  eb04                  jmp       short 0040BCF5h

LAB_0040bcf1:
  0040bcf1  c6460a02              mov       byte [esi+0Ah],2

LAB_0040bcf5:
  0040bcf5  885e0b                mov       [esi+0Bh],bl
  0040bcf8  885e0b                mov       [esi+0Bh],bl
  0040bcfb  e840b41300            call      00547140h
  0040bd00  99                    cdq
  0040bd01  b91e000000            mov       ecx,1Eh
  0040bd06  8b7e20                mov       edi,[esi+20h]
  0040bd09  f7f9                  idiv      ecx
  0040bd0b  c1e20c                shl       edx,0Ch
  0040bd0e  03fa                  add       edi,edx
  0040bd10  897e20                mov       [esi+20h],edi
  0040bd13  e828b41300            call      00547140h
  0040bd18  99                    cdq
  0040bd19  33c2                  xor       eax,edx
  0040bd1b  2bc2                  sub       eax,edx
  0040bd1d  25ff0f0000            and       eax,0FFFh
  0040bd22  33c2                  xor       eax,edx
  0040bd24  2bc2                  sub       eax,edx
  0040bd26  66894612              mov       [esi+12h],ax
  0040bd2a  e811b41300            call      00547140h
  0040bd2f  99                    cdq
  0040bd30  b978000000            mov       ecx,78h
  0040bd35  66899e9a000000        mov       [esi+9Ah],bx
  0040bd3c  f7f9                  idiv      ecx
  0040bd3e  895e5c                mov       [esi+5Ch],ebx
  0040bd41  83c23c                add       edx,3Ch
  0040bd44  66895614              mov       [esi+14h],dx
  0040bd48  e8f3b31300            call      00547140h
  0040bd4d  99                    cdq
  0040bd4e  b907000000            mov       ecx,7
  0040bd53  66c746100100          mov       word [esi+10h],1
  0040bd59  f7f9                  idiv      ecx
  0040bd5b  8b4620                mov       eax,[esi+20h]
  0040bd5e  8b4e24                mov       ecx,[esi+24h]
  0040bd61  894654                mov       [esi+54h],eax
  0040bd64  894e58                mov       [esi+58h],ecx
  0040bd67  66899698000000        mov       [esi+98h],dx
  0040bd6e  8b561c                mov       edx,[esi+1Ch]
  0040bd71  895650                mov       [esi+50h],edx

LAB_0040bd74:
  0040bd74  66837e1001            cmp       word [esi+10h],1
  0040bd79  0f85c7000000          jne       near 0040BE46h
  0040bd7f  0fbf5612              movsx     edx,word [esi+12h]
  0040bd83  52                    push      edx
  0040bd84  e8939c1300            call      00545A1Ch
  0040bd89  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0040bd90  0fafc1                imul      eax,ecx
  0040bd93  0fbf5612              movsx     edx,word [esi+12h]
  0040bd97  8b4e50                mov       ecx,[esi+50h]
  0040bd9a  668bbe9a000000        mov       di,[esi+9Ah]
  0040bda1  03c1                  add       eax,ecx
  0040bda3  52                    push      edx
  0040bda4  89461c                mov       [esi+1Ch],eax
  0040bda7  e86a9c1300            call      00545A16h
  0040bdac  0fbfcf                movsx     ecx,di
  0040bdaf  0fafc1                imul      eax,ecx
  0040bdb2  0fbf9698000000        movsx     edx,word [esi+98h]
  0040bdb9  8b4e58                mov       ecx,[esi+58h]
  0040bdbc  66814612fa00          add       word [esi+12h],0FAh
  0040bdc2  03c1                  add       eax,ecx
  0040bdc4  8b4e20                mov       ecx,[esi+20h]
  0040bdc7  c1e20c                shl       edx,0Ch
  0040bdca  03ca                  add       ecx,edx
  0040bdcc  894624                mov       [esi+24h],eax
  0040bdcf  894e20                mov       [esi+20h],ecx
  0040bdd2  668b4e14              mov       cx,[esi+14h]
  0040bdd6  8d4705                lea       eax,[edi+5]
  0040bdd9  663bc1                cmp       ax,cx
  0040bddc  6689869a000000        mov       [esi+9Ah],ax
  0040bde3  7e07                  jle       short 0040BDECh
  0040bde5  66898e9a000000        mov       [esi+9Ah],cx

LAB_0040bdec:
  0040bdec  8b565c                mov       edx,[esi+5Ch]
  0040bdef  42                    inc       edx
  0040bdf0  8bc2                  mov       eax,edx
  0040bdf2  89565c                mov       [esi+5Ch],edx
  0040bdf5  a801                  test      al,1
  0040bdf7  7412                  je        short 0040BE0Bh
  0040bdf9  8a5640                mov       dl,[esi+40h]
  0040bdfc  fec2                  inc       dl
  0040bdfe  8aca                  mov       cl,dl
  0040be00  885640                mov       [esi+40h],dl
  0040be03  80f903                cmp       cl,3
  0040be06  7203                  jb        short 0040BE0Bh
  0040be08  885e40                mov       [esi+40h],bl

LAB_0040be0b:
  0040be0b  83f81e                cmp       eax,1Eh
  0040be0e  7e36                  jle       short 0040BE46h
  0040be10  660fb64638            movzx     ax,byte [esi+38h]
  0040be15  660fb64e39            movzx     cx,byte [esi+39h]
  0040be1a  660fb6563a            movzx     dx,byte [esi+3Ah]
  0040be1f  83e80a                sub       eax,0Ah
  0040be22  83e90a                sub       ecx,0Ah
  0040be25  83ea0a                sub       edx,0Ah
  0040be28  663bc3                cmp       ax,bx
  0040be2b  7d10                  jge       short 0040BE3Dh
  0040be2d  8b3e                  mov       edi,[esi]
  0040be2f  33c0                  xor       eax,eax
  0040be31  33c9                  xor       ecx,ecx
  0040be33  33d2                  xor       edx,edx
  0040be35  81e7ffffff7f          and       edi,7FFFFFFFh
  0040be3b  893e                  mov       [esi],edi

LAB_0040be3d:
  0040be3d  884638                mov       [esi+38h],al
  0040be40  884e39                mov       [esi+39h],cl
  0040be43  88563a                mov       [esi+3Ah],dl

LAB_0040be46:
  0040be46  5f                    pop       edi
  0040be47  5e                    pop       esi
  0040be48  5b                    pop       ebx
  0040be49  c3                    ret
  0040be4a  90                    nop
  0040be4b  90                    nop
  0040be4c  90                    nop
  0040be4d  90                    nop
  0040be4e  90                    nop
  0040be4f  90                    nop

7 cible(s) de saut interne : 0x0040bcf1, 0x0040bcf5, 0x0040bd74, 0x0040bdec, 0x0040be0b, 0x0040be3d, 0x0040be46
