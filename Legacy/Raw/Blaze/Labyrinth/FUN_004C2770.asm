Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c2770  section .text  offset fichier 0xc1b70  3296 octets

  004c2770  81ec88000000          sub       esp,88h
  004c2776  53                    push      ebx
  004c2777  55                    push      ebp
  004c2778  56                    push      esi
  004c2779  8bb42498000000        mov       esi,[esp+98h]
  004c2780  33db                  xor       ebx,ebx
  004c2782  57                    push      edi
  004c2783  8a4641                mov       al,[esi+41h]
  004c2786  66895c2430            mov       [esp+30h],bx
  004c278b  3ac3                  cmp       al,bl
  004c278d  66c74424321e00        mov       word [esp+32h],1Eh
  004c2794  66c74424343200        mov       word [esp+34h],32h
  004c279b  66c74424366400        mov       word [esp+36h],64h
  004c27a2  66895c2438            mov       [esp+38h],bx
  004c27a7  66c744243a5000        mov       word [esp+3Ah],50h
  004c27ae  7519                  jne       short 004C27C9h
  004c27b0  66395e16              cmp       [esi+16h],bx
  004c27b4  c6464101              mov       byte [esi+41h],1
  004c27b8  53                    push      ebx
  004c27b9  7503                  jne       short 004C27BEh
  004c27bb  53                    push      ebx
  004c27bc  eb02                  jmp       short 004C27C0h

LAB_004c27be:
  004c27be  6a02                  push      2

LAB_004c27c0:
  004c27c0  56                    push      esi
  004c27c1  e88a0c0000            call      004C3450h
  004c27c6  83c40c                add       esp,0Ch

LAB_004c27c9:
  004c27c9  8b461c                mov       eax,[esi+1Ch]
  004c27cc  8b4e20                mov       ecx,[esi+20h]
  004c27cf  8b5624                mov       edx,[esi+24h]
  004c27d2  8d6e1c                lea       ebp,[esi+1Ch]
  004c27d5  89442424              mov       [esp+24h],eax
  004c27d9  8b4664                mov       eax,[esi+64h]
  004c27dc  894c2428              mov       [esp+28h],ecx
  004c27e0  8b4e60                mov       ecx,[esi+60h]
  004c27e3  8954242c              mov       [esp+2Ch],edx
  004c27e7  668b5632              mov       dx,[esi+32h]
  004c27eb  89442454              mov       [esp+54h],eax
  004c27ef  8b467c                mov       eax,[esi+7Ch]
  004c27f2  894c2448              mov       [esp+48h],ecx
  004c27f6  668b8e90000000        mov       cx,[esi+90h]
  004c27fd  6689942486000000      mov       [esp+86h],dx
  004c2805  8b9680000000          mov       edx,[esi+80h]
  004c280b  8b7e6c                mov       edi,[esi+6Ch]
  004c280e  89442460              mov       [esp+60h],eax
  004c2812  8b8684000000          mov       eax,[esi+84h]
  004c2818  66894c244c            mov       [esp+4Ch],cx
  004c281d  8b8e88000000          mov       ecx,[esi+88h]
  004c2823  89542444              mov       [esp+44h],edx
  004c2827  8b968c000000          mov       edx,[esi+8Ch]
  004c282d  89442450              mov       [esp+50h],eax
  004c2831  894c243c              mov       [esp+3Ch],ecx
  004c2835  8d442464              lea       eax,[esp+64h]
  004c2839  8954245c              mov       [esp+5Ch],edx
  004c283d  8d4c246c              lea       ecx,[esp+6Ch]
  004c2841  50                    push      eax
  004c2842  8d54246c              lea       edx,[esp+6Ch]
  004c2846  51                    push      ecx
  004c2847  52                    push      edx
  004c2848  56                    push      esi
  004c2849  897c2450              mov       [esp+50h],edi
  004c284d  e82e610700            call      00538980h
  004c2852  83c410                add       esp,10h
  004c2855  8d442458              lea       eax,[esp+58h]
  004c2859  8d4c2420              lea       ecx,[esp+20h]
  004c285d  8d542418              lea       edx,[esp+18h]
  004c2861  50                    push      eax
  004c2862  51                    push      ecx
  004c2863  8d442424              lea       eax,[esp+24h]
  004c2867  52                    push      edx
  004c2868  8d4c2420              lea       ecx,[esp+20h]
  004c286c  50                    push      eax
  004c286d  51                    push      ecx
  004c286e  56                    push      esi
  004c286f  e8ac360700            call      00535F20h
  004c2874  8b4c242c              mov       ecx,[esp+2Ch]
  004c2878  8b542470              mov       edx,[esp+70h]
  004c287c  83c418                add       esp,18h
  004c287f  b840000000            mov       eax,40h
  004c2884  2bc1                  sub       eax,ecx
  004c2886  52                    push      edx
  004c2887  56                    push      esi
  004c2888  89442418              mov       [esp+18h],eax
  004c288c  e84f350100            call      004D5DE0h
  004c2891  8b4500                mov       eax,[ebp]
  004c2894  8b571c                mov       edx,[edi+1Ch]
  004c2897  8b4e20                mov       ecx,[esi+20h]
  004c289a  2bc2                  sub       eax,edx
  004c289c  8b5720                mov       edx,[edi+20h]
  004c289f  83c408                add       esp,8
  004c28a2  2bca                  sub       ecx,edx
  004c28a4  8b5624                mov       edx,[esi+24h]
  004c28a7  2b5724                sub       edx,[edi+24h]
  004c28aa  c1f80c                sar       eax,0Ch
  004c28ad  8bf8                  mov       edi,eax
  004c28af  c1f90c                sar       ecx,0Ch
  004c28b2  0faff8                imul      edi,eax
  004c28b5  8bc1                  mov       eax,ecx
  004c28b7  c1fa0c                sar       edx,0Ch
  004c28ba  0fafc1                imul      eax,ecx
  004c28bd  8bca                  mov       ecx,edx
  004c28bf  03f8                  add       edi,eax
  004c28c1  0fafca                imul      ecx,edx
  004c28c4  03f9                  add       edi,ecx
  004c28c6  57                    push      edi
  004c28c7  e844310800            call      00545A10h
  004c28cc  8944241c              mov       [esp+1Ch],eax
  004c28d0  8b442420              mov       eax,[esp+20h]
  004c28d4  3bc3                  cmp       eax,ebx
  004c28d6  0f8400050000          je        near 004C2DDCh
  004c28dc  8b0e                  mov       ecx,[esi]
  004c28de  33d2                  xor       edx,edx
  004c28e0  8a5604                mov       dl,[esi+4]
  004c28e3  81e1fffffffd          and       ecx,0FDFFFFFFh
  004c28e9  0fbf7e18              movsx     edi,word [esi+18h]
  004c28ed  890e                  mov       [esi],ecx
  004c28ef  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c28f6  f7c100000001          test      ecx,1000000h
  004c28fc  8b14ba                mov       edx,[edx+edi*4]
  004c28ff  741b                  je        short 004C291Ch
  004c2901  66ff461a              inc       word [esi+1Ah]
  004c2905  668b4e1a              mov       cx,[esi+1Ah]
  004c2909  33ff                  xor       edi,edi
  004c290b  668b7a16              mov       di,[edx+16h]
  004c290f  0fbfc9                movsx     ecx,cx
  004c2912  3bcf                  cmp       ecx,edi
  004c2914  7c2a                  jl        short 004C2940h
  004c2916  66895e1a              mov       [esi+1Ah],bx
  004c291a  eb24                  jmp       short 004C2940h

LAB_004c291c:
  004c291c  66ff461a              inc       word [esi+1Ah]
  004c2920  668b7e1a              mov       di,[esi+1Ah]
  004c2924  33db                  xor       ebx,ebx
  004c2926  668b5a16              mov       bx,[edx+16h]
  004c292a  0fbfd7                movsx     edx,di
  004c292d  3bd3                  cmp       edx,ebx
  004c292f  7c0d                  jl        short 004C293Eh
  004c2931  4f                    dec       edi
  004c2932  81c900000002          or        ecx,2000000h
  004c2938  66897e1a              mov       [esi+1Ah],di
  004c293c  890e                  mov       [esi],ecx

LAB_004c293e:
  004c293e  33db                  xor       ebx,ebx

LAB_004c2940:
  004c2940  8a4e42                mov       cl,[esi+42h]
  004c2943  80f905                cmp       cl,5
  004c2946  8b0e                  mov       ecx,[esi]
  004c2948  7505                  jne       short 004C294Fh
  004c294a  83c910                or        ecx,10h
  004c294d  eb03                  jmp       short 004C2952h

LAB_004c294f:
  004c294f  83e1ef                and       ecx,0FFFFFFEFh

LAB_004c2952:
  004c2952  f6c102                test      cl,2
  004c2955  890e                  mov       [esi],ecx
  004c2957  0f846b0a0000          je        near 004C33C8h
  004c295d  66837e1a08            cmp       word [esi+1Ah],8
  004c2962  7558                  jne       short 004C29BCh
  004c2964  8d442430              lea       eax,[esp+30h]
  004c2968  53                    push      ebx
  004c2969  50                    push      eax
  004c296a  55                    push      ebp
  004c296b  56                    push      esi
  004c296c  e8bf720500            call      00519C30h
  004c2971  8bf8                  mov       edi,eax
  004c2973  a1b8e05d00            mov       eax,[5DE0B8h]
  004c2978  83c410                add       esp,10h
  004c297b  3bc3                  cmp       eax,ebx
  004c297d  7439                  je        short 004C29B8h
  004c297f  66837e1a08            cmp       word [esi+1Ah],8
  004c2984  7532                  jne       short 004C29B8h
  004c2986  8b0f                  mov       ecx,[edi]
  004c2988  57                    push      edi
  004c2989  83c940                or        ecx,40h
  004c298c  56                    push      esi
  004c298d  66c787960000001000    mov       word [edi+96h],10h
  004c2996  890f                  mov       [edi],ecx
  004c2998  e8e3710700            call      00539B80h
  004c299d  83c408                add       esp,8
  004c29a0  83c71c                add       edi,1Ch
  004c29a3  57                    push      edi
  004c29a4  6800180000            push      1800h
  004c29a9  6800030000            push      300h
  004c29ae  6a03                  push      3
  004c29b0  e86bb10700            call      0053DB20h
  004c29b5  83c410                add       esp,10h

LAB_004c29b8:
  004c29b8  8b442420              mov       eax,[esp+20h]

LAB_004c29bc:
  004c29bc  8b0e                  mov       ecx,[esi]
  004c29be  f7c100000002          test      ecx,2000000h
  004c29c4  7411                  je        short 004C29D7h
  004c29c6  f6c101                test      cl,1
  004c29c9  0f85ef090000          jne       near 004C33BEh
  004c29cf  83e1fd                and       ecx,0FFFFFFFDh
  004c29d2  885e43                mov       [esi+43h],bl
  004c29d5  890e                  mov       [esi],ecx

LAB_004c29d7:
  004c29d7  8b7c2414              mov       edi,[esp+14h]

LAB_004c29db:
  004c29db  33c9                  xor       ecx,ecx
  004c29dd  8a4e42                mov       cl,[esi+42h]
  004c29e0  83f90b                cmp       ecx,0Bh
  004c29e3  0f87f3030000          ja        near 004C2DDCh
  004c29e9  ff248d1c344c00        jmp       dword [ecx*4+4C341Ch]
  004c29f0  66395e12              cmp       [esi+12h],bx
  004c29f4  751a                  jne       short 004C2A10h
  004c29f6  8b06                  mov       eax,[esi]
  004c29f8  66895e18              mov       [esi+18h],bx
  004c29fc  0d00000001            or        eax,1000000h
  004c2a01  66895e1a              mov       [esi+1Ah],bx
  004c2a05  8906                  mov       [esi],eax
  004c2a07  66c746120100          mov       word [esi+12h],1
  004c2a0d  895e5c                mov       [esi+5Ch],ebx

LAB_004c2a10:
  004c2a10  0fbf4610              movsx     eax,word [esi+10h]
  004c2a14  3944241c              cmp       [esp+1Ch],eax
  004c2a18  7d08                  jge       short 004C2A22h
  004c2a1a  885e43                mov       [esi+43h],bl
  004c2a1d  e9ba030000            jmp       004C2DDCh

LAB_004c2a22:
  004c2a22  8a4643                mov       al,[esi+43h]
  004c2a25  3ac3                  cmp       al,bl
  004c2a27  0f847f030000          je        near 004C2DACh
  004c2a2d  fec8                  dec       al
  004c2a2f  895e5c                mov       [esi+5Ch],ebx
  004c2a32  884643                mov       [esi+43h],al
  004c2a35  e9a2030000            jmp       004C2DDCh
  004c2a3a  66395e12              cmp       [esi+12h],bx
  004c2a3e  751b                  jne       short 004C2A5Bh
  004c2a40  8b0e                  mov       ecx,[esi]
  004c2a42  b801000000            mov       eax,1
  004c2a47  81c900000001          or        ecx,1000000h
  004c2a4d  66894618              mov       [esi+18h],ax
  004c2a51  66895e1a              mov       [esi+1Ah],bx
  004c2a55  890e                  mov       [esi],ecx
  004c2a57  66894612              mov       [esi+12h],ax

LAB_004c2a5b:
  004c2a5b  8b442418              mov       eax,[esp+18h]
  004c2a5f  8d88c8000000          lea       ecx,[eax+0C8h]
  004c2a65  81e1ff0f0000          and       ecx,0FFFh
  004c2a6b  81f990010000          cmp       ecx,190h
  004c2a71  7f0b                  jg        short 004C2A7Eh
  004c2a73  885e43                mov       [esi+43h],bl
  004c2a76  895e5c                mov       [esi+5Ch],ebx
  004c2a79  e95e030000            jmp       004C2DDCh

LAB_004c2a7e:
  004c2a7e  25ff0f0000            and       eax,0FFFh
  004c2a83  3d00080000            cmp       eax,800h
  004c2a88  7d0f                  jge       short 004C2A99h
  004c2a8a  8d473c                lea       eax,[edi+3Ch]
  004c2a8d  895e5c                mov       [esi+5Ch],ebx
  004c2a90  66014632              add       [esi+32h],ax
  004c2a94  e943030000            jmp       004C2DDCh

LAB_004c2a99:
  004c2a99  b8c4ffffff            mov       eax,0FFFFFFC4h
  004c2a9e  895e5c                mov       [esi+5Ch],ebx
  004c2aa1  2bc7                  sub       eax,edi
  004c2aa3  66014632              add       [esi+32h],ax
  004c2aa7  e930030000            jmp       004C2DDCh
  004c2aac  66395e12              cmp       [esi+12h],bx
  004c2ab0  b801000000            mov       eax,1
  004c2ab5  7525                  jne       short 004C2ADCh
  004c2ab7  8b0e                  mov       ecx,[esi]
  004c2ab9  8d14ff                lea       edx,[edi+edi*8]
  004c2abc  c1e204                shl       edx,4
  004c2abf  81c900000001          or        ecx,1000000h
  004c2ac5  81c200600000          add       edx,6000h
  004c2acb  66894618              mov       [esi+18h],ax
  004c2acf  66895e1a              mov       [esi+1Ah],bx
  004c2ad3  890e                  mov       [esi],ecx
  004c2ad5  66894612              mov       [esi+12h],ax
  004c2ad9  89565c                mov       [esi+5Ch],edx

LAB_004c2adc:
  004c2adc  66394612              cmp       [esi+12h],ax
  004c2ae0  0f85f6020000          jne       near 004C2DDCh
  004c2ae6  0fbf4610              movsx     eax,word [esi+10h]
  004c2aea  3944241c              cmp       [esp+1Ch],eax
  004c2aee  7d0b                  jge       short 004C2AFBh
  004c2af0  885e43                mov       [esi+43h],bl
  004c2af3  895e5c                mov       [esi+5Ch],ebx
  004c2af6  e9e1020000            jmp       004C2DDCh

LAB_004c2afb:
  004c2afb  8a4643                mov       al,[esi+43h]
  004c2afe  3ac3                  cmp       al,bl
  004c2b00  7405                  je        short 004C2B07h
  004c2b02  fec8                  dec       al
  004c2b04  884643                mov       [esi+43h],al

LAB_004c2b07:
  004c2b07  8b442418              mov       eax,[esp+18h]
  004c2b0b  8d8800010000          lea       ecx,[eax+100h]
  004c2b11  81e1ff0f0000          and       ecx,0FFFh
  004c2b17  81f900020000          cmp       ecx,200h
  004c2b1d  0f8eb9020000          jle       near 004C2DDCh
  004c2b23  25ff0f0000            and       eax,0FFFh
  004c2b28  3d00080000            cmp       eax,800h
  004c2b2d  7d0c                  jge       short 004C2B3Bh
  004c2b2f  8d473c                lea       eax,[edi+3Ch]
  004c2b32  66014632              add       [esi+32h],ax
  004c2b36  e9a1020000            jmp       004C2DDCh

LAB_004c2b3b:
  004c2b3b  b8c4ffffff            mov       eax,0FFFFFFC4h
  004c2b40  2bc7                  sub       eax,edi
  004c2b42  66014632              add       [esi+32h],ax
  004c2b46  e991020000            jmp       004C2DDCh
  004c2b4b  66837e1201            cmp       word [esi+12h],1
  004c2b50  0f8592000000          jne       near 004C2BE8h
  004c2b56  8a4e43                mov       cl,[esi+43h]
  004c2b59  3acb                  cmp       cl,bl
  004c2b5b  7405                  je        short 004C2B62h
  004c2b5d  fec9                  dec       cl
  004c2b5f  884e43                mov       [esi+43h],cl

LAB_004c2b62:
  004c2b62  8d14ff                lea       edx,[edi+edi*8]
  004c2b65  c1e204                shl       edx,4
  004c2b68  8d8a00600000          lea       ecx,[edx+6000h]
  004c2b6e  0fafc1                imul      eax,ecx
  004c2b71  c1f80c                sar       eax,0Ch
  004c2b74  8bf8                  mov       edi,eax
  004c2b76  894e5c                mov       [esi+5Ch],ecx
  004c2b79  0fbf4610              movsx     eax,word [esi+10h]
  004c2b7d  50                    push      eax
  004c2b7e  e8992e0800            call      00545A1Ch
  004c2b83  0fafc7                imul      eax,edi
  004c2b86  8b4d00                mov       ecx,[ebp]
  004c2b89  c1f80c                sar       eax,0Ch
  004c2b8c  03c8                  add       ecx,eax
  004c2b8e  894d00                mov       [ebp],ecx
  004c2b91  0fbf4e10              movsx     ecx,word [esi+10h]
  004c2b95  51                    push      ecx
  004c2b96  e87b2e0800            call      00545A16h
  004c2b9b  0fafc7                imul      eax,edi
  004c2b9e  8b6e24                mov       ebp,[esi+24h]
  004c2ba1  895e5c                mov       [esi+5Ch],ebx
  004c2ba4  c1f80c                sar       eax,0Ch
  004c2ba7  03e8                  add       ebp,eax
  004c2ba9  8b442418              mov       eax,[esp+18h]
  004c2bad  896e24                mov       [esi+24h],ebp
  004c2bb0  8d9000010000          lea       edx,[eax+100h]
  004c2bb6  81e2ff0f0000          and       edx,0FFFh
  004c2bbc  81fa00020000          cmp       edx,200h
  004c2bc2  7e24                  jle       short 004C2BE8h
  004c2bc4  25ff0f0000            and       eax,0FFFh
  004c2bc9  3d00080000            cmp       eax,800h
  004c2bce  7d09                  jge       short 004C2BD9h
  004c2bd0  8b442414              mov       eax,[esp+14h]
  004c2bd4  83c032                add       eax,32h
  004c2bd7  eb0b                  jmp       short 004C2BE4h

LAB_004c2bd9:
  004c2bd9  8b4c2414              mov       ecx,[esp+14h]
  004c2bdd  b8ceffffff            mov       eax,0FFFFFFCEh
  004c2be2  2bc1                  sub       eax,ecx

LAB_004c2be4:
  004c2be4  66014632              add       [esi+32h],ax

LAB_004c2be8:
  004c2be8  66395e12              cmp       [esi+12h],bx
  004c2bec  0f85ea010000          jne       near 004C2DDCh
  004c2bf2  8b06                  mov       eax,[esi]
  004c2bf4  66c746180200          mov       word [esi+18h],2
  004c2bfa  0d00000001            or        eax,1000000h
  004c2bff  66895e1a              mov       [esi+1Ah],bx
  004c2c03  8906                  mov       [esi],eax
  004c2c05  66c746120100          mov       word [esi+12h],1
  004c2c0b  e9cc010000            jmp       004C2DDCh
  004c2c10  668b4e12              mov       cx,[esi+12h]
  004c2c14  6683f901              cmp       cx,1
  004c2c18  7514                  jne       short 004C2C2Eh
  004c2c1a  8a4643                mov       al,[esi+43h]
  004c2c1d  3ac3                  cmp       al,bl
  004c2c1f  740d                  je        short 004C2C2Eh
  004c2c21  f70600000002          test      dword [esi],2000000h
  004c2c27  7405                  je        short 004C2C2Eh
  004c2c29  fec8                  dec       al
  004c2c2b  884643                mov       [esi+43h],al

LAB_004c2c2e:
  004c2c2e  663bcb                cmp       cx,bx
  004c2c31  0f85a5010000          jne       near 004C2DDCh
  004c2c37  66c746120100          mov       word [esi+12h],1
  004c2c3d  66c746180500          mov       word [esi+18h],5
  004c2c43  66895e1a              mov       [esi+1Ah],bx
  004c2c47  895e5c                mov       [esi+5Ch],ebx
  004c2c4a  e987010000            jmp       004C2DD6h
  004c2c4f  66395e12              cmp       [esi+12h],bx
  004c2c53  757a                  jne       short 004C2CCFh
  004c2c55  8b0e                  mov       ecx,[esi]
  004c2c57  f6c101                test      cl,1
  004c2c5a  7405                  je        short 004C2C61h
  004c2c5c  885e43                mov       [esi+43h],bl
  004c2c5f  eb6e                  jmp       short 004C2CCFh

LAB_004c2c61:
  004c2c61  81e1fffffffe          and       ecx,0FEFFFFFFh
  004c2c67  66c746180400          mov       word [esi+18h],4
  004c2c6d  890e                  mov       [esi],ecx
  004c2c6f  8d8f00010000          lea       ecx,[edi+100h]
  004c2c75  c1e106                shl       ecx,6
  004c2c78  0fafc1                imul      eax,ecx
  004c2c7b  894e5c                mov       [esi+5Ch],ecx
  004c2c7e  66895e1a              mov       [esi+1Ah],bx
  004c2c82  0fbf4e10              movsx     ecx,word [esi+10h]
  004c2c86  c1f80c                sar       eax,0Ch
  004c2c89  51                    push      ecx
  004c2c8a  8bf8                  mov       edi,eax
  004c2c8c  e88b2d0800            call      00545A1Ch
  004c2c91  0fafc7                imul      eax,edi
  004c2c94  0fbf5610              movsx     edx,word [esi+10h]
  004c2c98  8b4e50                mov       ecx,[esi+50h]
  004c2c9b  52                    push      edx
  004c2c9c  c1f80c                sar       eax,0Ch
  004c2c9f  03c8                  add       ecx,eax
  004c2ca1  894e50                mov       [esi+50h],ecx
  004c2ca4  e86d2d0800            call      00545A16h
  004c2ca9  0fafc7                imul      eax,edi
  004c2cac  8b5658                mov       edx,[esi+58h]
  004c2caf  8b4e54                mov       ecx,[esi+54h]
  004c2cb2  c1f80c                sar       eax,0Ch
  004c2cb5  03d0                  add       edx,eax
  004c2cb7  8b06                  mov       eax,[esi]
  004c2cb9  81c100e00100          add       ecx,1E000h
  004c2cbf  0c01                  or        al,1
  004c2cc1  895658                mov       [esi+58h],edx
  004c2cc4  894e54                mov       [esi+54h],ecx
  004c2cc7  8906                  mov       [esi],eax
  004c2cc9  66c746120100          mov       word [esi+12h],1

LAB_004c2ccf:
  004c2ccf  f60601                test      byte [esi],1
  004c2cd2  0f8504010000          jne       near 004C2DDCh
  004c2cd8  885e43                mov       [esi+43h],bl
  004c2cdb  e9fc000000            jmp       004C2DDCh
  004c2ce0  668b4e12              mov       cx,[esi+12h]
  004c2ce4  ba01000000            mov       edx,1
  004c2ce9  663bca                cmp       cx,dx
  004c2cec  751b                  jne       short 004C2D09h
  004c2cee  0fbf4610              movsx     eax,word [esi+10h]
  004c2cf2  3944241c              cmp       [esp+1Ch],eax
  004c2cf6  7d05                  jge       short 004C2CFDh
  004c2cf8  885e43                mov       [esi+43h],bl
  004c2cfb  eb0c                  jmp       short 004C2D09h

LAB_004c2cfd:
  004c2cfd  8a4643                mov       al,[esi+43h]
  004c2d00  3ac3                  cmp       al,bl
  004c2d02  7405                  je        short 004C2D09h
  004c2d04  fec8                  dec       al
  004c2d06  884643                mov       [esi+43h],al

LAB_004c2d09:
  004c2d09  663bcb                cmp       cx,bx
  004c2d0c  0f85ca000000          jne       near 004C2DDCh
  004c2d12  66895e1a              mov       [esi+1Ah],bx
  004c2d16  8b1e                  mov       ebx,[esi]
  004c2d18  8d0cff                lea       ecx,[edi+edi*8]
  004c2d1b  81cb00000001          or        ebx,1000000h
  004c2d21  c1e104                shl       ecx,4
  004c2d24  81c100600000          add       ecx,6000h
  004c2d2a  66895618              mov       [esi+18h],dx
  004c2d2e  891e                  mov       [esi],ebx
  004c2d30  66895612              mov       [esi+12h],dx
  004c2d34  894e5c                mov       [esi+5Ch],ecx
  004c2d37  e9a0000000            jmp       004C2DDCh
  004c2d3c  b801000000            mov       eax,1
  004c2d41  66394612              cmp       [esi+12h],ax
  004c2d45  7537                  jne       short 004C2D7Eh
  004c2d47  66837e1a07            cmp       word [esi+1Ah],7
  004c2d4c  7530                  jne       short 004C2D7Eh
  004c2d4e  66394616              cmp       [esi+16h],ax
  004c2d52  7512                  jne       short 004C2D66h
  004c2d54  6aff                  push      0FFFFFFFFh
  004c2d56  53                    push      ebx
  004c2d57  6a36                  push      36h
  004c2d59  56                    push      esi
  004c2d5a  e8717cfbff            call      0047A9D0h
  004c2d5f  8b7c2424              mov       edi,[esp+24h]
  004c2d63  83c410                add       esp,10h

LAB_004c2d66:
  004c2d66  66395e16              cmp       [esi+16h],bx
  004c2d6a  7512                  jne       short 004C2D7Eh
  004c2d6c  6aff                  push      0FFFFFFFFh
  004c2d6e  53                    push      ebx
  004c2d6f  6a4b                  push      4Bh
  004c2d71  56                    push      esi
  004c2d72  e8597cfbff            call      0047A9D0h
  004c2d77  8b7c2424              mov       edi,[esp+24h]
  004c2d7b  83c410                add       esp,10h

LAB_004c2d7e:
  004c2d7e  8b06                  mov       eax,[esi]
  004c2d80  a900000002            test      eax,2000000h
  004c2d85  7403                  je        short 004C2D8Ah
  004c2d87  885e43                mov       [esi+43h],bl

LAB_004c2d8a:
  004c2d8a  66395e12              cmp       [esi+12h],bx
  004c2d8e  754c                  jne       short 004C2DDCh
  004c2d90  25fffffffe            and       eax,0FEFFFFFFh
  004c2d95  83ff1e                cmp       edi,1Eh
  004c2d98  66c746120100          mov       word [esi+12h],1
  004c2d9e  66c746180300          mov       word [esi+18h],3
  004c2da4  66895e1a              mov       [esi+1Ah],bx
  004c2da8  8906                  mov       [esi],eax
  004c2daa  7d30                  jge       short 004C2DDCh

LAB_004c2dac:
  004c2dac  895e5c                mov       [esi+5Ch],ebx
  004c2daf  eb2b                  jmp       short 004C2DDCh
  004c2db1  668b4e12              mov       cx,[esi+12h]
  004c2db5  6683f901              cmp       cx,1
  004c2db9  7510                  jne       short 004C2DCBh
  004c2dbb  8b06                  mov       eax,[esi]
  004c2dbd  a900000002            test      eax,2000000h
  004c2dc2  7407                  je        short 004C2DCBh
  004c2dc4  24fd                  and       al,0FDh
  004c2dc6  885e43                mov       [esi+43h],bl
  004c2dc9  8906                  mov       [esi],eax

LAB_004c2dcb:
  004c2dcb  663bcb                cmp       cx,bx
  004c2dce  750c                  jne       short 004C2DDCh
  004c2dd0  66c746120100          mov       word [esi+12h],1

LAB_004c2dd6:
  004c2dd6  8126fffffffe          and       dword [esi],0FEFFFFFFh

LAB_004c2ddc:
  004c2ddc  0fbf5632              movsx     edx,word [esi+32h]
  004c2de0  52                    push      edx
  004c2de1  e8362c0800            call      00545A1Ch
  004c2de6  89442470              mov       [esp+70h],eax
  004c2dea  0fbf4632              movsx     eax,word [esi+32h]
  004c2dee  50                    push      eax
  004c2def  e8222c0800            call      00545A16h
  004c2df4  8b4e50                mov       ecx,[esi+50h]
  004c2df7  89442478              mov       [esp+78h],eax
  004c2dfb  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  004c2e02  8bc1                  mov       eax,ecx
  004c2e04  8bd1                  mov       edx,ecx
  004c2e06  c1f803                sar       eax,3
  004c2e09  8b7c2420              mov       edi,[esp+20h]
  004c2e0d  2bd0                  sub       edx,eax
  004c2e0f  8b4654                mov       eax,[esi+54h]
  004c2e12  0faf7e5c              imul      edi,[esi+5Ch]
  004c2e16  c1f908                sar       ecx,8
  004c2e19  2bd1                  sub       edx,ecx
  004c2e1b  8bcb                  mov       ecx,ebx
  004c2e1d  8be8                  mov       ebp,eax
  004c2e1f  895650                mov       [esi+50h],edx
  004c2e22  c1f908                sar       ecx,8
  004c2e25  c1fd08                sar       ebp,8
  004c2e28  2bcd                  sub       ecx,ebp
  004c2e2a  8b6e58                mov       ebp,[esi+58h]
  004c2e2d  03c8                  add       ecx,eax
  004c2e2f  8bc5                  mov       eax,ebp
  004c2e31  2bcb                  sub       ecx,ebx
  004c2e33  8bdd                  mov       ebx,ebp
  004c2e35  c1fb08                sar       ebx,8
  004c2e38  2bc3                  sub       eax,ebx
  004c2e3a  894e54                mov       [esi+54h],ecx
  004c2e3d  c1fd03                sar       ebp,3
  004c2e40  2bc5                  sub       eax,ebp
  004c2e42  8b6e20                mov       ebp,[esi+20h]
  004c2e45  c1ff0c                sar       edi,0Ch
  004c2e48  03e9                  add       ebp,ecx
  004c2e4a  8bdf                  mov       ebx,edi
  004c2e4c  0faf5c2470            imul      ebx,[esp+70h]
  004c2e51  8bcf                  mov       ecx,edi
  004c2e53  894658                mov       [esi+58h],eax
  004c2e56  0faf4c2478            imul      ecx,[esp+78h]
  004c2e5b  c1fb0c                sar       ebx,0Ch
  004c2e5e  c1f90c                sar       ecx,0Ch
  004c2e61  03da                  add       ebx,edx
  004c2e63  8b561c                mov       edx,[esi+1Ch]
  004c2e66  03c8                  add       ecx,eax
  004c2e68  8b4624                mov       eax,[esi+24h]
  004c2e6b  03d3                  add       edx,ebx
  004c2e6d  03c1                  add       eax,ecx
  004c2e6f  56                    push      esi
  004c2e70  89561c                mov       [esi+1Ch],edx
  004c2e73  896e20                mov       [esi+20h],ebp
  004c2e76  894624                mov       [esi+24h],eax
  004c2e79  e8c20a0700            call      00533940h
  004c2e7e  8b9690000000          mov       edx,[esi+90h]
  004c2e84  8b4660                mov       eax,[esi+60h]
  004c2e87  8b4e20                mov       ecx,[esi+20h]
  004c2e8a  83c404                add       esp,4
  004c2e8d  80e6fc                and       dh,0FCh
  004c2e90  3bc8                  cmp       ecx,eax
  004c2e92  899690000000          mov       [esi+90h],edx
  004c2e98  7f2a                  jg        short 004C2EC4h
  004c2e9a  f6c680                test      dh,80h
  004c2e9d  7525                  jne       short 004C2EC4h
  004c2e9f  8b0e                  mov       ecx,[esi]
  004c2ea1  894620                mov       [esi+20h],eax
  004c2ea4  f6c101                test      cl,1
  004c2ea7  740f                  je        short 004C2EB8h
  004c2ea9  83e1fe                and       ecx,0FFFFFFFEh
  004c2eac  33db                  xor       ebx,ebx
  004c2eae  890e                  mov       [esi],ecx
  004c2eb0  895e54                mov       [esi+54h],ebx
  004c2eb3  e92a010000            jmp       004C2FE2h

LAB_004c2eb8:
  004c2eb8  83e1fe                and       ecx,0FFFFFFFEh
  004c2ebb  33db                  xor       ebx,ebx
  004c2ebd  890e                  mov       [esi],ecx
  004c2ebf  895e54                mov       [esi+54h],ebx
  004c2ec2  eb02                  jmp       short 004C2EC6h

LAB_004c2ec4:
  004c2ec4  33db                  xor       ebx,ebx

LAB_004c2ec6:
  004c2ec6  8b0e                  mov       ecx,[esi]
  004c2ec8  f6c101                test      cl,1
  004c2ecb  750a                  jne       short 004C2ED7h
  004c2ecd  394620                cmp       [esi+20h],eax
  004c2ed0  7e05                  jle       short 004C2ED7h
  004c2ed2  83c901                or        ecx,1
  004c2ed5  890e                  mov       [esi],ecx

LAB_004c2ed7:
  004c2ed7  f7c282ad0020          test      edx,2000AD82h
  004c2edd  0f84ff000000          je        near 004C2FE2h
  004c2ee3  8b06                  mov       eax,[esi]
  004c2ee5  a801                  test      al,1
  004c2ee7  0f85c9000000          jne       near 004C2FB6h
  004c2eed  8a4e42                mov       cl,[esi+42h]
  004c2ef0  b007                  mov       al,7
  004c2ef2  3ac8                  cmp       cl,al
  004c2ef4  7420                  je        short 004C2F16h
  004c2ef6  884642                mov       [esi+42h],al
  004c2ef9  e842420800            call      00547140h
  004c2efe  99                    cdq
  004c2eff  b928000000            mov       ecx,28h
  004c2f04  66895e12              mov       [esi+12h],bx
  004c2f08  f7f9                  idiv      ecx
  004c2f0a  66c746105000          mov       word [esi+10h],50h
  004c2f10  80c214                add       dl,14h
  004c2f13  885643                mov       [esi+43h],dl

LAB_004c2f16:
  004c2f16  8b16                  mov       edx,[esi]
  004c2f18  8b442428              mov       eax,[esp+28h]
  004c2f1c  8b4c242c              mov       ecx,[esp+2Ch]
  004c2f20  83e2fd                and       edx,0FFFFFFFDh
  004c2f23  8916                  mov       [esi],edx
  004c2f25  8b542424              mov       edx,[esp+24h]
  004c2f29  894620                mov       [esi+20h],eax
  004c2f2c  8b44244c              mov       eax,[esp+4Ch]
  004c2f30  89561c                mov       [esi+1Ch],edx
  004c2f33  8b542460              mov       edx,[esp+60h]
  004c2f37  25ffff0000            and       eax,0FFFFh
  004c2f3c  894e24                mov       [esi+24h],ecx
  004c2f3f  8b4c2450              mov       ecx,[esp+50h]
  004c2f43  89567c                mov       [esi+7Ch],edx
  004c2f46  8b54243c              mov       edx,[esp+3Ch]
  004c2f4a  898690000000          mov       [esi+90h],eax
  004c2f50  8b442444              mov       eax,[esp+44h]
  004c2f54  898e84000000          mov       [esi+84h],ecx
  004c2f5a  8b4c2454              mov       ecx,[esp+54h]
  004c2f5e  898680000000          mov       [esi+80h],eax
  004c2f64  8b44245c              mov       eax,[esp+5Ch]
  004c2f68  899688000000          mov       [esi+88h],edx
  004c2f6e  8b542448              mov       edx,[esp+48h]
  004c2f72  6882ad0020            push      2000AD82h
  004c2f77  56                    push      esi
  004c2f78  89868c000000          mov       [esi+8Ch],eax
  004c2f7e  894e64                mov       [esi+64h],ecx
  004c2f81  895660                mov       [esi+60h],edx
  004c2f84  e8d72e0100            call      004D5E60h
  004c2f89  83c408                add       esp,8
  004c2f8c  663bc3                cmp       ax,bx
  004c2f8f  7515                  jne       short 004C2FA6h
  004c2f91  e8aa410800            call      00547140h
  004c2f96  99                    cdq
  004c2f97  33c2                  xor       eax,edx
  004c2f99  2bc2                  sub       eax,edx
  004c2f9b  83e001                and       eax,1
  004c2f9e  33c2                  xor       eax,edx
  004c2fa0  2bc2                  sub       eax,edx
  004c2fa2  8d4400ff              lea       eax,[eax+eax-1]

LAB_004c2fa6:
  004c2fa6  8b4c2414              mov       ecx,[esp+14h]
  004c2faa  83c140                add       ecx,40h
  004c2fad  0fafc8                imul      ecx,eax
  004c2fb0  66014e32              add       [esi+32h],cx
  004c2fb4  eb2c                  jmp       short 004C2FE2h

LAB_004c2fb6:
  004c2fb6  8b542424              mov       edx,[esp+24h]
  004c2fba  24fd                  and       al,0FDh
  004c2fbc  8906                  mov       [esi],eax
  004c2fbe  8b44242c              mov       eax,[esp+2Ch]
  004c2fc2  895e5c                mov       [esi+5Ch],ebx
  004c2fc5  895e50                mov       [esi+50h],ebx
  004c2fc8  895e58                mov       [esi+58h],ebx
  004c2fcb  89561c                mov       [esi+1Ch],edx
  004c2fce  894624                mov       [esi+24h],eax
  004c2fd1  885e42                mov       [esi+42h],bl
  004c2fd4  c6464328              mov       byte [esi+43h],28h
  004c2fd8  66895e12              mov       [esi+12h],bx
  004c2fdc  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c2fe2:
  004c2fe2  6aff                  push      0FFFFFFFFh
  004c2fe4  6aff                  push      0FFFFFFFFh
  004c2fe6  6aff                  push      0FFFFFFFFh
  004c2fe8  56                    push      esi
  004c2fe9  e822080000            call      004C3810h
  004c2fee  8b442430              mov       eax,[esp+30h]
  004c2ff2  83c410                add       esp,10h
  004c2ff5  3bc3                  cmp       eax,ebx
  004c2ff7  7517                  jne       short 004C3010h
  004c2ff9  668b8c2486000000      mov       cx,[esp+86h]
  004c3001  66894e32              mov       [esi+32h],cx
  004c3005  5f                    pop       edi
  004c3006  5e                    pop       esi
  004c3007  5d                    pop       ebp
  004c3008  5b                    pop       ebx
  004c3009  81c488000000          add       esp,88h
  004c300f  c3                    ret

LAB_004c3010:
  004c3010  385e43                cmp       [esi+43h],bl
  004c3013  0f8525030000          jne       near 004C333Eh
  004c3019  8b466c                mov       eax,[esi+6Ch]
  004c301c  8b561c                mov       edx,[esi+1Ch]
  004c301f  8b781c                mov       edi,[eax+1Ch]
  004c3022  8b6824                mov       ebp,[eax+24h]
  004c3025  8b4624                mov       eax,[esi+24h]
  004c3028  2bfa                  sub       edi,edx
  004c302a  2be8                  sub       ebp,eax
  004c302c  c1ff0c                sar       edi,0Ch
  004c302f  c1fd0c                sar       ebp,0Ch
  004c3032  8bd7                  mov       edx,edi
  004c3034  8bc5                  mov       eax,ebp
  004c3036  0fafd7                imul      edx,edi
  004c3039  0fafc5                imul      eax,ebp
  004c303c  03d0                  add       edx,eax
  004c303e  52                    push      edx
  004c303f  e8cc290800            call      00545A10h
  004c3044  55                    push      ebp
  004c3045  57                    push      edi
  004c3046  89442424              mov       [esp+24h],eax
  004c304a  e8d3290800            call      00545A22h
  004c304f  668b4e32              mov       cx,[esi+32h]
  004c3053  66895e12              mov       [esi+12h],bx
  004c3057  2bc1                  sub       eax,ecx
  004c3059  25ff0f0000            and       eax,0FFFh
  004c305e  89442418              mov       [esp+18h],eax
  004c3062  8b44241c              mov       eax,[esp+1Ch]
  004c3066  3dfa000000            cmp       eax,0FAh
  004c306b  0f8d70010000          jge       near 004C31E1h
  004c3071  e8ca400800            call      00547140h
  004c3076  99                    cdq
  004c3077  b964000000            mov       ecx,64h
  004c307c  f7f9                  idiv      ecx
  004c307e  8bfa                  mov       edi,edx
  004c3080  83ff32                cmp       edi,32h
  004c3083  0f8dba000000          jge       near 004C3143h
  004c3089  8b6c2410              mov       ebp,[esp+10h]
  004c308d  8b542418              mov       edx,[esp+18h]
  004c3091  8d84aa00010000        lea       eax,[edx+ebp*4+100h]
  004c3098  8d0ced00020000        lea       ecx,[ebp*8+200h]
  004c309f  25ff0f0000            and       eax,0FFFh
  004c30a4  3bc1                  cmp       eax,ecx
  004c30a6  7f3e                  jg        short 004C30E6h
  004c30a8  e893400800            call      00547140h
  004c30ad  99                    cdq
  004c30ae  8d4c2d01              lea       ecx,[ebp+ebp+1]
  004c30b2  f7f9                  idiv      ecx
  004c30b4  8b44241c              mov       eax,[esp+1Ch]
  004c30b8  83c264                add       edx,64h
  004c30bb  3bc2                  cmp       eax,edx
  004c30bd  7d27                  jge       short 004C30E6h
  004c30bf  8b16                  mov       edx,[esi]
  004c30c1  66c746180300          mov       word [esi+18h],3
  004c30c7  81e2fffffffe          and       edx,0FEFFFFFFh
  004c30cd  66895e1a              mov       [esi+1Ah],bx
  004c30d1  83ca02                or        edx,2
  004c30d4  c646420b              mov       byte [esi+42h],0Bh
  004c30d8  8916                  mov       [esi],edx
  004c30da  c6464301              mov       byte [esi+43h],1
  004c30de  895e5c                mov       [esi+5Ch],ebx
  004c30e1  e95c020000            jmp       004C3342h

LAB_004c30e6:
  004c30e6  8b442418              mov       eax,[esp+18h]
  004c30ea  8d14ed00040000        lea       edx,[ebp*8+400h]
  004c30f1  8d8ca800020000        lea       ecx,[eax+ebp*4+200h]
  004c30f8  81e1ff0f0000          and       ecx,0FFFh
  004c30fe  3bca                  cmp       ecx,edx
  004c3100  7f34                  jg        short 004C3136h
  004c3102  e839400800            call      00547140h
  004c3107  99                    cdq
  004c3108  b964000000            mov       ecx,64h
  004c310d  f7f9                  idiv      ecx
  004c310f  8bc5                  mov       eax,ebp
  004c3111  d1f8                  sar       eax,1
  004c3113  83c020                add       eax,20h
  004c3116  3bd0                  cmp       edx,eax
  004c3118  7d1c                  jge       short 004C3136h
  004c311a  c6464205              mov       byte [esi+42h],5
  004c311e  e81d400800            call      00547140h
  004c3123  99                    cdq
  004c3124  b905000000            mov       ecx,5
  004c3129  f7f9                  idiv      ecx
  004c312b  80c20a                add       dl,0Ah
  004c312e  885643                mov       [esi+43h],dl
  004c3131  e90c020000            jmp       004C3342h

LAB_004c3136:
  004c3136  c6464201              mov       byte [esi+42h],1
  004c313a  c6464301              mov       byte [esi+43h],1
  004c313e  e9ff010000            jmp       004C3342h

LAB_004c3143:
  004c3143  83ff46                cmp       edi,46h
  004c3146  7d2c                  jge       short 004C3174h
  004c3148  c6464202              mov       byte [esi+42h],2
  004c314c  e8ef3f0800            call      00547140h
  004c3151  99                    cdq
  004c3152  b90a000000            mov       ecx,0Ah
  004c3157  8b6c2410              mov       ebp,[esp+10h]
  004c315b  f7f9                  idiv      ecx
  004c315d  8bc5                  mov       eax,ebp
  004c315f  66c746106400          mov       word [esi+10h],64h
  004c3165  d1f8                  sar       eax,1
  004c3167  02d0                  add       dl,al
  004c3169  80c214                add       dl,14h
  004c316c  885643                mov       [esi+43h],dl
  004c316f  e9ce010000            jmp       004C3342h

LAB_004c3174:
  004c3174  83ff5a                cmp       edi,5Ah
  004c3177  7d13                  jge       short 004C318Ch
  004c3179  8b4c2414              mov       ecx,[esp+14h]
  004c317d  8b44241c              mov       eax,[esp+1Ch]
  004c3181  83c164                add       ecx,64h
  004c3184  3bc1                  cmp       eax,ecx
  004c3186  0f8f73010000          jg        near 004C32FFh

LAB_004c318c:
  004c318c  c6464203              mov       byte [esi+42h],3
  004c3190  e8ab3f0800            call      00547140h
  004c3195  99                    cdq
  004c3196  b90a000000            mov       ecx,0Ah
  004c319b  8b6c2410              mov       ebp,[esp+10h]
  004c319f  f7f9                  idiv      ecx
  004c31a1  8b4c2414              mov       ecx,[esp+14h]
  004c31a5  8bc5                  mov       eax,ebp
  004c31a7  d1f8                  sar       eax,1
  004c31a9  8db980000000          lea       edi,[ecx+80h]
  004c31af  c1e704                shl       edi,4
  004c31b2  02d0                  add       dl,al
  004c31b4  80c214                add       dl,14h
  004c31b7  885643                mov       [esi+43h],dl
  004c31ba  e8813f0800            call      00547140h
  004c31bf  99                    cdq
  004c31c0  f7ff                  idiv      edi
  004c31c2  8bc7                  mov       eax,edi
  004c31c4  d1f8                  sar       eax,1
  004c31c6  2bd0                  sub       edx,eax
  004c31c8  8b442418              mov       eax,[esp+18h]
  004c31cc  03d0                  add       edx,eax
  004c31ce  66035632              add       dx,[esi+32h]
  004c31d2  81c200080000          add       edx,800h
  004c31d8  66895610              mov       [esi+10h],dx
  004c31dc  e961010000            jmp       004C3342h

LAB_004c31e1:
  004c31e1  3d58020000            cmp       eax,258h
  004c31e6  0f8dcb000000          jge       near 004C32B7h
  004c31ec  e84f3f0800            call      00547140h
  004c31f1  99                    cdq
  004c31f2  b964000000            mov       ecx,64h
  004c31f7  f7f9                  idiv      ecx
  004c31f9  8bfa                  mov       edi,edx
  004c31fb  83ff1e                cmp       edi,1Eh
  004c31fe  7d32                  jge       short 004C3232h
  004c3200  8b542418              mov       edx,[esp+18h]
  004c3204  81c200040000          add       edx,400h
  004c320a  81e2ff0f0000          and       edx,0FFFh
  004c3210  81fa00080000          cmp       edx,800h
  004c3216  7c0d                  jl        short 004C3225h
  004c3218  c6464201              mov       byte [esi+42h],1
  004c321c  c6464301              mov       byte [esi+43h],1
  004c3220  e919010000            jmp       004C333Eh

LAB_004c3225:
  004c3225  c646420a              mov       byte [esi+42h],0Ah
  004c3229  c6464301              mov       byte [esi+43h],1
  004c322d  e90c010000            jmp       004C333Eh

LAB_004c3232:
  004c3232  83ff32                cmp       edi,32h
  004c3235  7d4f                  jge       short 004C3286h
  004c3237  c6464203              mov       byte [esi+42h],3
  004c323b  e8003f0800            call      00547140h
  004c3240  99                    cdq
  004c3241  b90a000000            mov       ecx,0Ah
  004c3246  8b6c2410              mov       ebp,[esp+10h]
  004c324a  f7f9                  idiv      ecx
  004c324c  8b4c2414              mov       ecx,[esp+14h]
  004c3250  8bc5                  mov       eax,ebp
  004c3252  d1f8                  sar       eax,1
  004c3254  8db980000000          lea       edi,[ecx+80h]
  004c325a  c1e704                shl       edi,4
  004c325d  02d0                  add       dl,al
  004c325f  80c214                add       dl,14h
  004c3262  885643                mov       [esi+43h],dl
  004c3265  e8d63e0800            call      00547140h
  004c326a  99                    cdq
  004c326b  f7ff                  idiv      edi
  004c326d  8b4c2418              mov       ecx,[esp+18h]
  004c3271  8bc7                  mov       eax,edi
  004c3273  d1f8                  sar       eax,1
  004c3275  2bd0                  sub       edx,eax
  004c3277  03d1                  add       edx,ecx
  004c3279  66035632              add       dx,[esi+32h]
  004c327d  66895610              mov       [esi+10h],dx
  004c3281  e9bc000000            jmp       004C3342h

LAB_004c3286:
  004c3286  83ff5a                cmp       edi,5Ah
  004c3289  7d40                  jge       short 004C32CBh
  004c328b  c6464202              mov       byte [esi+42h],2
  004c328f  e8ac3e0800            call      00547140h
  004c3294  99                    cdq
  004c3295  b90a000000            mov       ecx,0Ah
  004c329a  8b6c2410              mov       ebp,[esp+10h]
  004c329e  f7f9                  idiv      ecx
  004c32a0  8bc5                  mov       eax,ebp
  004c32a2  66c746106400          mov       word [esi+10h],64h
  004c32a8  d1f8                  sar       eax,1
  004c32aa  02d0                  add       dl,al
  004c32ac  80c214                add       dl,14h
  004c32af  885643                mov       [esi+43h],dl
  004c32b2  e98b000000            jmp       004C3342h

LAB_004c32b7:
  004c32b7  e8843e0800            call      00547140h
  004c32bc  99                    cdq
  004c32bd  b964000000            mov       ecx,64h
  004c32c2  f7f9                  idiv      ecx
  004c32c4  8bfa                  mov       edi,edx
  004c32c6  83ff14                cmp       edi,14h
  004c32c9  7d20                  jge       short 004C32EBh

LAB_004c32cb:
  004c32cb  885e42                mov       [esi+42h],bl
  004c32ce  e86d3e0800            call      00547140h
  004c32d3  99                    cdq
  004c32d4  b90a000000            mov       ecx,0Ah
  004c32d9  8b6c2410              mov       ebp,[esp+10h]
  004c32dd  f7f9                  idiv      ecx
  004c32df  8bc5                  mov       eax,ebp
  004c32e1  c1f802                sar       eax,2
  004c32e4  02d0                  add       dl,al
  004c32e6  885643                mov       [esi+43h],dl
  004c32e9  eb57                  jmp       short 004C3342h

LAB_004c32eb:
  004c32eb  83ff3c                cmp       edi,3Ch
  004c32ee  7d0a                  jge       short 004C32FAh
  004c32f0  c6464201              mov       byte [esi+42h],1
  004c32f4  c6464301              mov       byte [esi+43h],1
  004c32f8  eb44                  jmp       short 004C333Eh

LAB_004c32fa:
  004c32fa  83ff50                cmp       edi,50h
  004c32fd  7d13                  jge       short 004C3312h

LAB_004c32ff:
  004c32ff  c6464206              mov       byte [esi+42h],6
  004c3303  c6464301              mov       byte [esi+43h],1
  004c3307  e8343e0800            call      00547140h
  004c330c  66894610              mov       [esi+10h],ax
  004c3310  eb2c                  jmp       short 004C333Eh

LAB_004c3312:
  004c3312  c6464203              mov       byte [esi+42h],3
  004c3316  e8253e0800            call      00547140h
  004c331b  99                    cdq
  004c331c  b90a000000            mov       ecx,0Ah
  004c3321  8b6c2410              mov       ebp,[esp+10h]
  004c3325  f7f9                  idiv      ecx
  004c3327  8bc5                  mov       eax,ebp
  004c3329  d1f8                  sar       eax,1
  004c332b  02d0                  add       dl,al
  004c332d  80c214                add       dl,14h
  004c3330  885643                mov       [esi+43h],dl
  004c3333  e8083e0800            call      00547140h
  004c3338  66894610              mov       [esi+10h],ax
  004c333c  eb04                  jmp       short 004C3342h

LAB_004c333e:
  004c333e  8b6c2410              mov       ebp,[esp+10h]

LAB_004c3342:
  004c3342  8b4c2418              mov       ecx,[esp+18h]
  004c3346  8d04ed00040000        lea       eax,[ebp*8+400h]
  004c334d  8d94a900020000        lea       edx,[ecx+ebp*4+200h]
  004c3354  81e2ff0f0000          and       edx,0FFFh
  004c335a  3bd0                  cmp       edx,eax
  004c335c  7f55                  jg        short 004C33B3h
  004c335e  8b4c2440              mov       ecx,[esp+40h]
  004c3362  f60102                test      byte [ecx],2
  004c3365  744c                  je        short 004C33B3h
  004c3367  3bfd                  cmp       edi,ebp
  004c3369  7d48                  jge       short 004C33B3h
  004c336b  f60601                test      byte [esi],1
  004c336e  7543                  jne       short 004C33B3h
  004c3370  66837e1805            cmp       word [esi+18h],5
  004c3375  743c                  je        short 004C33B3h
  004c3377  817c241cc8000000      cmp       dword [esp+1Ch],0C8h
  004c337f  7d32                  jge       short 004C33B3h
  004c3381  c6464205              mov       byte [esi+42h],5
  004c3385  e8b63d0800            call      00547140h
  004c338a  99                    cdq
  004c338b  b905000000            mov       ecx,5
  004c3390  66c7461a0600          mov       word [esi+1Ah],6
  004c3396  f7f9                  idiv      ecx
  004c3398  8b06                  mov       eax,[esi]
  004c339a  66894e18              mov       [esi+18h],cx
  004c339e  895e5c                mov       [esi+5Ch],ebx
  004c33a1  66c746120100          mov       word [esi+12h],1
  004c33a7  02d1                  add       dl,cl
  004c33a9  25fffffffe            and       eax,0FEFFFFFFh
  004c33ae  885643                mov       [esi+43h],dl
  004c33b1  8906                  mov       [esi],eax

LAB_004c33b3:
  004c33b3  5f                    pop       edi
  004c33b4  5e                    pop       esi
  004c33b5  5d                    pop       ebp
  004c33b6  5b                    pop       ebx
  004c33b7  81c488000000          add       esp,88h
  004c33bd  c3                    ret

LAB_004c33be:
  004c33be  83e1fd                and       ecx,0FFFFFFFDh
  004c33c1  890e                  mov       [esi],ecx
  004c33c3  e90ff6ffff            jmp       004C29D7h

LAB_004c33c8:
  004c33c8  f6c101                test      cl,1
  004c33cb  0f8406f6ffff          je        near 004C29D7h
  004c33d1  66837e1a03            cmp       word [esi+1Ah],3
  004c33d6  0f8dfbf5ffff          jge       near 004C29D7h
  004c33dc  e85f3d0800            call      00547140h
  004c33e1  99                    cdq
  004c33e2  b93c000000            mov       ecx,3Ch
  004c33e7  8b7c2414              mov       edi,[esp+14h]
  004c33eb  f7f9                  idiv      ecx
  004c33ed  3bd7                  cmp       edx,edi
  004c33ef  7d20                  jge       short 004C3411h
  004c33f1  8b16                  mov       edx,[esi]
  004c33f3  8b442420              mov       eax,[esp+20h]
  004c33f7  81e2fffffffe          and       edx,0FEFFFFFFh
  004c33fd  66c746180300          mov       word [esi+18h],3
  004c3403  83ca02                or        edx,2
  004c3406  66895e1a              mov       [esi+1Ah],bx
  004c340a  8916                  mov       [esi],edx
  004c340c  e9caf5ffff            jmp       004C29DBh

LAB_004c3411:
  004c3411  8b442420              mov       eax,[esp+20h]
  004c3415  e9c1f5ffff            jmp       004C29DBh
  004c341a  8bff                  mov       edi,edi
  004c341c  f0294c003a            lock sub  [eax+eax+3Ah],ecx
  004c3421  2a4c00ac              sub       cl,[eax+eax-54h]
  004c3425  2a4c004b              sub       cl,[eax+eax+4Bh]
  004c3429  2b4c00dc              sub       ecx,[eax+eax-24h]
  004c342d  2d4c00102c            sub       eax,2C10004Ch
  004c3432  4c                    dec       esp
  004c3433  004f2c                add       [edi+2Ch],cl
  004c3436  4c                    dec       esp
  004c3437  00e0                  add       al,ah
  004c3439  2c4c                  sub       al,4Ch
  004c343b  00dc                  add       ah,bl
  004c343d  2d4c00dc2d            sub       eax,2DDC004Ch
  004c3442  4c                    dec       esp
  004c3443  003c2d4c00b12d        add       [ebp*1+2DB1004Ch],bh
  004c344a  4c                    dec       esp
  004c344b  0090909090            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
