Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x005003e0  section .text  offset fichier 0xff7e0  2608 octets

  005003e0  83ec28                sub       esp,28h
  005003e3  8d442408              lea       eax,[esp+8]
  005003e7  8d4c2420              lea       ecx,[esp+20h]
  005003eb  8d54241c              lea       edx,[esp+1Ch]
  005003ef  53                    push      ebx
  005003f0  55                    push      ebp
  005003f1  56                    push      esi
  005003f2  8b742438              mov       esi,[esp+38h]
  005003f6  57                    push      edi
  005003f7  50                    push      eax
  005003f8  51                    push      ecx
  005003f9  52                    push      edx
  005003fa  56                    push      esi
  005003fb  e880850300            call      00538980h
  00500400  83c410                add       esp,10h
  00500403  8d44241c              lea       eax,[esp+1Ch]
  00500407  8d4c2414              lea       ecx,[esp+14h]
  0050040b  8d54243c              lea       edx,[esp+3Ch]
  0050040f  50                    push      eax
  00500410  51                    push      ecx
  00500411  8d442418              lea       eax,[esp+18h]
  00500415  52                    push      edx
  00500416  8d4c2440              lea       ecx,[esp+40h]
  0050041a  50                    push      eax
  0050041b  51                    push      ecx
  0050041c  56                    push      esi
  0050041d  e8fe5a0300            call      00535F20h
  00500422  8b44242c              mov       eax,[esp+2Ch]
  00500426  33ed                  xor       ebp,ebp
  00500428  83c418                add       esp,18h
  0050042b  3bc5                  cmp       eax,ebp
  0050042d  bb01000000            mov       ebx,1
  00500432  0f843a070000          je        near 00500B72h
  00500438  56                    push      esi
  00500439  e862b1fdff            call      004DB5A0h
  0050043e  8b06                  mov       eax,[esi]
  00500440  83c404                add       esp,4
  00500443  a802                  test      al,2
  00500445  0f84e6030000          je        near 00500831h
  0050044b  f644241c10            test      byte [esp+1Ch],10h
  00500450  740d                  je        short 0050045Fh
  00500452  24fd                  and       al,0FDh
  00500454  c6464300              mov       byte [esi+43h],0
  00500458  8906                  mov       [esi],eax
  0050045a  e913070000            jmp       00500B72h

LAB_0050045f:
  0050045f  33c9                  xor       ecx,ecx
  00500461  8a4e42                mov       cl,[esi+42h]
  00500464  2bcd                  sub       ecx,ebp
  00500466  0f84c8020000          je        near 00500734h
  0050046c  49                    dec       ecx
  0050046d  0f84f1000000          je        near 00500564h
  00500473  49                    dec       ecx
  00500474  0f85f8060000          jne       near 00500B72h
  0050047a  668b5610              mov       dx,[esi+10h]
  0050047e  0fbfca                movsx     ecx,dx
  00500481  2bcd                  sub       ecx,ebp
  00500483  0f84c1000000          je        near 0050054Ah
  00500489  49                    dec       ecx
  0050048a  0f8599030000          jne       near 00500829h
  00500490  a900000002            test      eax,2000000h
  00500495  7409                  je        short 005004A0h
  00500497  4a                    dec       edx
  00500498  c6464314              mov       byte [esi+43h],14h
  0050049c  66895610              mov       [esi+10h],dx

LAB_005004a0:
  005004a0  66837e1a0d            cmp       word [esi+1Ah],0Dh
  005004a5  0f857e030000          jne       near 00500829h
  005004ab  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  005004b2  0fbf6e34              movsx     ebp,word [esi+34h]
  005004b6  8b5e5c                mov       ebx,[esi+5Ch]
  005004b9  6a00                  push      0
  005004bb  56                    push      esi
  005004bc  66c7869a0000000000    mov       word [esi+9Ah],0
  005004c5  66c74634800f          mov       word [esi+34h],0F80h
  005004cb  c7465c46000000        mov       dword [esi+5Ch],46h
  005004d2  e899520300            call      00535770h
  005004d7  0fbf10                movsx     edx,word [eax]
  005004da  c1e20c                shl       edx,0Ch
  005004dd  895644                mov       [esi+44h],edx
  005004e0  83c408                add       esp,8
  005004e3  0fbf4802              movsx     ecx,word [eax+2]
  005004e7  c1e10c                shl       ecx,0Ch
  005004ea  894e48                mov       [esi+48h],ecx
  005004ed  6a02                  push      2
  005004ef  0fbf5004              movsx     edx,word [eax+4]
  005004f3  c1e20c                shl       edx,0Ch
  005004f6  89564c                mov       [esi+4Ch],edx
  005004f9  a1cc975e00            mov       eax,[5E97CCh]
  005004fe  6a00                  push      0
  00500500  8b88cc000000          mov       ecx,[eax+0CCh]
  00500506  33c0                  xor       eax,eax
  00500508  8b11                  mov       edx,[ecx]
  0050050a  8a420d                mov       al,[edx+0Dh]
  0050050d  05c0000000            add       eax,0C0h
  00500512  50                    push      eax
  00500513  56                    push      esi
  00500514  e8b7a4f7ff            call      0047A9D0h
  00500519  83c410                add       esp,10h
  0050051c  8d4e1c                lea       ecx,[esi+1Ch]
  0050051f  6689be9a000000        mov       [esi+9Ah],di
  00500526  66896e34              mov       [esi+34h],bp
  0050052a  51                    push      ecx
  0050052b  6870305500            push      553070h
  00500530  895e5c                mov       [esi+5Ch],ebx
  00500533  e8a8d60300            call      0053DBE0h
  00500538  83c408                add       esp,8
  0050053b  33ed                  xor       ebp,ebp
  0050053d  bb01000000            mov       ebx,1
  00500542  896e5c                mov       [esi+5Ch],ebp
  00500545  e928060000            jmp       00500B72h

LAB_0050054a:
  0050054a  8a4e43                mov       cl,[esi+43h]
  0050054d  fec9                  dec       cl
  0050054f  884e43                mov       [esi+43h],cl
  00500552  0f85d1020000          jne       near 00500829h
  00500558  24fd                  and       al,0FDh
  0050055a  896e5c                mov       [esi+5Ch],ebp
  0050055d  8906                  mov       [esi],eax
  0050055f  e90e060000            jmp       00500B72h

LAB_00500564:
  00500564  668b4e1a              mov       cx,[esi+1Ah]
  00500568  6683f90a              cmp       cx,0Ah
  0050056c  7d34                  jge       short 005005A2h
  0050056e  8b44243c              mov       eax,[esp+3Ch]
  00500572  8d5040                lea       edx,[eax+40h]
  00500575  81e2ff0f0000          and       edx,0FFFh
  0050057b  81fa80000000          cmp       edx,80h
  00500581  7e1b                  jle       short 0050059Eh
  00500583  25ff0f0000            and       eax,0FFFh
  00500588  33d2                  xor       edx,edx
  0050058a  3d00080000            cmp       eax,800h
  0050058f  0f9dc2                setge     dl
  00500592  4a                    dec       edx
  00500593  81e280000000          and       edx,80h
  00500599  83c2c0                add       edx,0FFFFFFC0h
  0050059c  8bc2                  mov       eax,edx

LAB_0050059e:
  0050059e  66014632              add       [esi+32h],ax

LAB_005005a2:
  005005a2  6683f90d              cmp       cx,0Dh
  005005a6  754d                  jne       short 005005F5h
  005005a8  0fbf4632              movsx     eax,word [esi+32h]
  005005ac  50                    push      eax
  005005ad  c7465c00800200        mov       dword [esi+5Ch],28000h
  005005b4  e863540400            call      00545A1Ch
  005005b9  0faf465c              imul      eax,[esi+5Ch]
  005005bd  0fbf4e32              movsx     ecx,word [esi+32h]
  005005c1  8b7e50                mov       edi,[esi+50h]
  005005c4  51                    push      ecx
  005005c5  c1f80c                sar       eax,0Ch
  005005c8  03f8                  add       edi,eax
  005005ca  897e50                mov       [esi+50h],edi
  005005cd  e844540400            call      00545A16h
  005005d2  0faf465c              imul      eax,[esi+5Ch]
  005005d6  8b5e58                mov       ebx,[esi+58h]
  005005d9  8d561c                lea       edx,[esi+1Ch]
  005005dc  c1f80c                sar       eax,0Ch
  005005df  03d8                  add       ebx,eax
  005005e1  52                    push      edx
  005005e2  6858305500            push      553058h
  005005e7  895e58                mov       [esi+58h],ebx
  005005ea  896e5c                mov       [esi+5Ch],ebp
  005005ed  e8eed50300            call      0053DBE0h
  005005f2  83c408                add       esp,8

LAB_005005f5:
  005005f5  66837e1a0d            cmp       word [esi+1Ah],0Dh
  005005fa  0f8c14010000          jl        near 00500714h
  00500600  f60620                test      byte [esi],20h
  00500603  0f850b010000          jne       near 00500714h
  00500609  8d6e1c                lea       ebp,[esi+1Ch]
  0050060c  6a00                  push      0
  0050060e  6844305500            push      553044h
  00500613  55                    push      ebp
  00500614  56                    push      esi
  00500615  e816960100            call      00519C30h
  0050061a  8bf8                  mov       edi,eax
  0050061c  83c410                add       esp,10h
  0050061f  85ff                  test      edi,edi
  00500621  7406                  je        short 00500629h
  00500623  8b06                  mov       eax,[esi]
  00500625  0c20                  or        al,20h
  00500627  8906                  mov       [esi],eax

LAB_00500629:
  00500629  a1b8e05d00            mov       eax,[5DE0B8h]
  0050062e  85c0                  test      eax,eax
  00500630  0f84de000000          je        near 00500714h
  00500636  8b1f                  mov       ebx,[edi]
  00500638  66c787960000000600    mov       word [edi+96h],6
  00500641  83cb40                or        ebx,40h
  00500644  57                    push      edi
  00500645  891f                  mov       [edi],ebx
  00500647  8b4c241c              mov       ecx,[esp+1Ch]
  0050064b  56                    push      esi
  0050064c  668b4130              mov       ax,[ecx+30h]
  00500650  0fbfd8                movsx     ebx,ax
  00500653  66d1f8                sar       ax,1
  00500656  66894130              mov       [ecx+30h],ax
  0050065a  e821950300            call      00539B80h
  0050065f  8b442420              mov       eax,[esp+20h]
  00500663  83c408                add       esp,8
  00500666  66895830              mov       [eax+30h],bx
  0050066a  8d5f1c                lea       ebx,[edi+1Ch]
  0050066d  53                    push      ebx
  0050066e  6800180000            push      1800h
  00500673  6800030000            push      300h
  00500678  6a03                  push      3
  0050067a  e8a1d40300            call      0053DB20h
  0050067f  8b07                  mov       eax,[edi]
  00500681  83c410                add       esp,10h
  00500684  a900000040            test      eax,40000000h
  00500689  0f8585000000          jne       near 00500714h
  0050068f  0fbf4e32              movsx     ecx,word [esi+32h]
  00500693  51                    push      ecx
  00500694  e883530400            call      00545A1Ch
  00500699  8d0440                lea       eax,[eax+eax*2]
  0050069c  8d1480                lea       edx,[eax+eax*4]
  0050069f  d1e2                  shl       edx,1
  005006a1  895750                mov       [edi+50h],edx
  005006a4  0fbf4632              movsx     eax,word [esi+32h]
  005006a8  50                    push      eax
  005006a9  e868530400            call      00545A16h
  005006ae  8d0440                lea       eax,[eax+eax*2]
  005006b1  8d0c80                lea       ecx,[eax+eax*4]
  005006b4  8b07                  mov       eax,[edi]
  005006b6  d1e1                  shl       ecx,1
  005006b8  a801                  test      al,1
  005006ba  894f58                mov       [edi+58h],ecx
  005006bd  750b                  jne       short 005006CAh
  005006bf  0c01                  or        al,1
  005006c1  c7475400800100        mov       dword [edi+54h],18000h
  005006c8  8907                  mov       [edi],eax

LAB_005006ca:
  005006ca  8b5500                mov       edx,[ebp]
  005006cd  8b4620                mov       eax,[esi+20h]
  005006d0  8b4e24                mov       ecx,[esi+24h]
  005006d3  89542420              mov       [esp+20h],edx
  005006d7  8b13                  mov       edx,[ebx]
  005006d9  89442424              mov       [esp+24h],eax
  005006dd  895500                mov       [ebp],edx
  005006e0  8b4720                mov       eax,[edi+20h]
  005006e3  6aff                  push      0FFFFFFFFh
  005006e5  894c242c              mov       [esp+2Ch],ecx
  005006e9  894620                mov       [esi+20h],eax
  005006ec  8b4f24                mov       ecx,[edi+24h]
  005006ef  6a00                  push      0
  005006f1  6a39                  push      39h
  005006f3  56                    push      esi
  005006f4  894e24                mov       [esi+24h],ecx
  005006f7  e8d4a2f7ff            call      0047A9D0h
  005006fc  8b542430              mov       edx,[esp+30h]
  00500700  8b442434              mov       eax,[esp+34h]
  00500704  8b4c2438              mov       ecx,[esp+38h]
  00500708  83c410                add       esp,10h
  0050070b  895500                mov       [ebp],edx
  0050070e  894620                mov       [esi+20h],eax
  00500711  894e24                mov       [esi+24h],ecx

LAB_00500714:
  00500714  8b06                  mov       eax,[esi]
  00500716  a900000002            test      eax,2000000h
  0050071b  7404                  je        short 00500721h
  0050071d  24dd                  and       al,0DDh
  0050071f  8906                  mov       [esi],eax

LAB_00500721:
  00500721  c7465c00000000        mov       dword [esi+5Ch],0
  00500728  bb01000000            mov       ebx,1
  0050072d  33ed                  xor       ebp,ebp
  0050072f  e93e040000            jmp       00500B72h

LAB_00500734:
  00500734  668b4e1a              mov       cx,[esi+1Ah]
  00500738  6683f90b              cmp       cx,0Bh
  0050073c  7d34                  jge       short 00500772h
  0050073e  8b44243c              mov       eax,[esp+3Ch]
  00500742  8d5040                lea       edx,[eax+40h]
  00500745  81e2ff0f0000          and       edx,0FFFh
  0050074b  81fa80000000          cmp       edx,80h
  00500751  7e1b                  jle       short 0050076Eh
  00500753  25ff0f0000            and       eax,0FFFh
  00500758  33d2                  xor       edx,edx
  0050075a  3d00080000            cmp       eax,800h
  0050075f  0f9dc2                setge     dl
  00500762  4a                    dec       edx
  00500763  81e280000000          and       edx,80h
  00500769  83c2c0                add       edx,0FFFFFFC0h
  0050076c  8bc2                  mov       eax,edx

LAB_0050076e:
  0050076e  66014632              add       [esi+32h],ax

LAB_00500772:
  00500772  6683f90d              cmp       cx,0Dh
  00500776  754d                  jne       short 005007C5h
  00500778  0fbf4632              movsx     eax,word [esi+32h]
  0050077c  50                    push      eax
  0050077d  c7465c00800200        mov       dword [esi+5Ch],28000h
  00500784  e893520400            call      00545A1Ch
  00500789  0faf465c              imul      eax,[esi+5Ch]
  0050078d  8b4e50                mov       ecx,[esi+50h]
  00500790  c1f80c                sar       eax,0Ch
  00500793  03c8                  add       ecx,eax
  00500795  894e50                mov       [esi+50h],ecx
  00500798  0fbf4e32              movsx     ecx,word [esi+32h]
  0050079c  51                    push      ecx
  0050079d  e874520400            call      00545A16h
  005007a2  0faf465c              imul      eax,[esi+5Ch]
  005007a6  8b4e58                mov       ecx,[esi+58h]
  005007a9  8d561c                lea       edx,[esi+1Ch]
  005007ac  c1f80c                sar       eax,0Ch
  005007af  03c8                  add       ecx,eax
  005007b1  52                    push      edx
  005007b2  6858305500            push      553058h
  005007b7  894e58                mov       [esi+58h],ecx
  005007ba  896e5c                mov       [esi+5Ch],ebp
  005007bd  e81ed40300            call      0053DBE0h
  005007c2  83c408                add       esp,8

LAB_005007c5:
  005007c5  66837e1a0d            cmp       word [esi+1Ah],0Dh
  005007ca  7c50                  jl        short 0050081Ch
  005007cc  55                    push      ebp
  005007cd  8d461c                lea       eax,[esi+1Ch]
  005007d0  6838305500            push      553038h
  005007d5  50                    push      eax
  005007d6  56                    push      esi
  005007d7  e854940100            call      00519C30h
  005007dc  8bf8                  mov       edi,eax
  005007de  a1b8e05d00            mov       eax,[5DE0B8h]
  005007e3  83c410                add       esp,10h
  005007e6  3bc5                  cmp       eax,ebp
  005007e8  7432                  je        short 0050081Ch
  005007ea  8b0f                  mov       ecx,[edi]
  005007ec  57                    push      edi
  005007ed  83c940                or        ecx,40h
  005007f0  56                    push      esi
  005007f1  66c787960000000600    mov       word [edi+96h],6
  005007fa  890f                  mov       [edi],ecx
  005007fc  e87f930300            call      00539B80h
  00500801  83c408                add       esp,8
  00500804  83c71c                add       edi,1Ch
  00500807  57                    push      edi
  00500808  6800180000            push      1800h
  0050080d  6800030000            push      300h
  00500812  6a03                  push      3
  00500814  e807d30300            call      0053DB20h
  00500819  83c410                add       esp,10h

LAB_0050081c:
  0050081c  8b06                  mov       eax,[esi]
  0050081e  a900000002            test      eax,2000000h
  00500823  7404                  je        short 00500829h
  00500825  24fd                  and       al,0FDh
  00500827  8906                  mov       [esi],eax

LAB_00500829:
  00500829  896e5c                mov       [esi+5Ch],ebp
  0050082c  e941030000            jmp       00500B72h

LAB_00500831:
  00500831  a810                  test      al,10h
  00500833  0f8539030000          jne       near 00500B72h
  00500839  a904080000            test      eax,804h
  0050083e  0f8452010000          je        near 00500996h
  00500844  f744241ce8030040      test      dword [esp+1Ch],400003E8h
  0050084c  740b                  je        short 00500859h
  0050084e  25fbf7ffff            and       eax,0FFFFF7FBh
  00500853  c6464300              mov       byte [esi+43h],0
  00500857  8906                  mov       [esi],eax

LAB_00500859:
  00500859  668b4610              mov       ax,[esi+10h]
  0050085d  0fbfc8                movsx     ecx,ax
  00500860  83f903                cmp       ecx,3
  00500863  0f8709030000          ja        near 00500B72h
  00500869  ff248de80d5000        jmp       dword [ecx*4+500DE8h]
  00500870  8b06                  mov       eax,[esi]
  00500872  a900000002            test      eax,2000000h
  00500877  0f84f5020000          je        near 00500B72h
  0050087d  80e4f7                and       ah,0F7h
  00500880  8906                  mov       [esi],eax
  00500882  e9eb020000            jmp       00500B72h
  00500887  b806000000            mov       eax,6
  0050088c  66394618              cmp       [esi+18h],ax
  00500890  7411                  je        short 005008A3h
  00500892  894618                mov       [esi+18h],eax
  00500895  8b06                  mov       eax,[esi]
  00500897  25fbfffffe            and       eax,0FEFFFFFBh
  0050089c  8906                  mov       [esi],eax
  0050089e  e9cf020000            jmp       00500B72h

LAB_005008a3:
  005008a3  66837e1a0d            cmp       word [esi+1Ah],0Dh
  005008a8  0f85c4020000          jne       near 00500B72h
  005008ae  33c0                  xor       eax,eax
  005008b0  6aff                  push      0FFFFFFFFh
  005008b2  668b4606              mov       ax,[esi+6]
  005008b6  6aff                  push      0FFFFFFFFh
  005008b8  8bc8                  mov       ecx,eax
  005008ba  c1f808                sar       eax,8
  005008bd  81e1ff000000          and       ecx,0FFh
  005008c3  25ff000000            and       eax,0FFh
  005008c8  51                    push      ecx
  005008c9  50                    push      eax
  005008ca  56                    push      esi
  005008cb  e8b0a00300            call      0053A980h
  005008d0  83c414                add       esp,14h
  005008d3  66ff4e10              dec       word [esi+10h]
  005008d7  e996020000            jmp       00500B72h
  005008dc  8a4e43                mov       cl,[esi+43h]
  005008df  fec9                  dec       cl
  005008e1  884e43                mov       [esi+43h],cl
  005008e4  7505                  jne       short 005008EBh
  005008e6  48                    dec       eax
  005008e7  66894610              mov       [esi+10h],ax

LAB_005008eb:
  005008eb  8b44243c              mov       eax,[esp+3Ch]
  005008ef  8d9080000000          lea       edx,[eax+80h]
  005008f5  81e2ff0f0000          and       edx,0FFFh
  005008fb  81fa00010000          cmp       edx,100h
  00500901  7f09                  jg        short 0050090Ch
  00500903  66014632              add       [esi+32h],ax
  00500907  e966020000            jmp       00500B72h

LAB_0050090c:
  0050090c  25ff0f0000            and       eax,0FFFh
  00500911  33c9                  xor       ecx,ecx
  00500913  3d00080000            cmp       eax,800h
  00500918  0f9dc1                setge     cl
  0050091b  49                    dec       ecx
  0050091c  81e100010000          and       ecx,100h
  00500922  83c180                add       ecx,0FFFFFF80h
  00500925  8bc1                  mov       eax,ecx
  00500927  66014632              add       [esi+32h],ax
  0050092b  e942020000            jmp       00500B72h
  00500930  55                    push      ebp
  00500931  55                    push      ebp
  00500932  68bd000000            push      0BDh
  00500937  56                    push      esi
  00500938  e8a3a1f7ff            call      0047AAE0h
  0050093d  83c410                add       esp,10h
  00500940  e8fb670400            call      00547140h
  00500945  99                    cdq
  00500946  33c2                  xor       eax,edx
  00500948  33c9                  xor       ecx,ecx
  0050094a  2bc2                  sub       eax,edx
  0050094c  23c3                  and       eax,ebx
  0050094e  33c2                  xor       eax,edx
  00500950  2bc2                  sub       eax,edx
  00500952  668b0c4550305500      mov       cx,[eax*2+553050h]
  0050095a  66894e06              mov       [esi+6],cx
  0050095e  a1c4975e00            mov       eax,[5E97C4h]
  00500963  8bd1                  mov       edx,ecx
  00500965  81e1ff000000          and       ecx,0FFh
  0050096b  8b809c000000          mov       eax,[eax+9Ch]
  00500971  c1fa08                sar       edx,8
  00500974  81e2ff000000          and       edx,0FFh
  0050097a  8d0c49                lea       ecx,[ecx+ecx*2]
  0050097d  c1e104                shl       ecx,4
  00500980  8b1490                mov       edx,[eax+edx*4]
  00500983  8a440a13              mov       al,[edx+ecx+13h]
  00500987  66ff4e10              dec       word [esi+10h]
  0050098b  884643                mov       [esi+43h],al
  0050098e  896e5c                mov       [esi+5Ch],ebp
  00500991  e9dc010000            jmp       00500B72h

LAB_00500996:
  00500996  a801                  test      al,1
  00500998  0f85d4010000          jne       near 00500B72h
  0050099e  33c9                  xor       ecx,ecx
  005009a0  8a4e42                mov       cl,[esi+42h]
  005009a3  83f903                cmp       ecx,3
  005009a6  0f87a1010000          ja        near 00500B4Dh
  005009ac  ff248df80d5000        jmp       dword [ecx*4+500DF8h]
  005009b3  66396e18              cmp       [esi+18h],bp
  005009b7  740a                  je        short 005009C3h
  005009b9  0d00000001            or        eax,1000000h
  005009be  896e18                mov       [esi+18h],ebp
  005009c1  8906                  mov       [esi],eax

LAB_005009c3:
  005009c3  817c24102c010000      cmp       dword [esp+10h],12Ch
  005009cb  7d09                  jge       short 005009D6h
  005009cd  c6464300              mov       byte [esi+43h],0
  005009d1  e977010000            jmp       00500B4Dh

LAB_005009d6:
  005009d6  8a4643                mov       al,[esi+43h]
  005009d9  84c0                  test      al,al
  005009db  7467                  je        short 00500A44h
  005009dd  fec8                  dec       al
  005009df  896e5c                mov       [esi+5Ch],ebp
  005009e2  884643                mov       [esi+43h],al
  005009e5  e963010000            jmp       00500B4Dh
  005009ea  66395e18              cmp       [esi+18h],bx
  005009ee  740a                  je        short 005009FAh
  005009f0  0d00000001            or        eax,1000000h
  005009f5  895e18                mov       [esi+18h],ebx
  005009f8  8906                  mov       [esi],eax

LAB_005009fa:
  005009fa  8b44243c              mov       eax,[esp+3Ch]
  005009fe  8d8800010000          lea       ecx,[eax+100h]
  00500a04  81e1ff0f0000          and       ecx,0FFFh
  00500a0a  81f900020000          cmp       ecx,200h
  00500a10  7f10                  jg        short 00500A22h
  00500a12  66014632              add       [esi+32h],ax
  00500a16  c6464300              mov       byte [esi+43h],0
  00500a1a  896e5c                mov       [esi+5Ch],ebp
  00500a1d  e92b010000            jmp       00500B4Dh

LAB_00500a22:
  00500a22  25ff0f0000            and       eax,0FFFh
  00500a27  33d2                  xor       edx,edx
  00500a29  3d00080000            cmp       eax,800h
  00500a2e  0f9dc2                setge     dl
  00500a31  4a                    dec       edx
  00500a32  81e200020000          and       edx,200h
  00500a38  81c200ffffff          add       edx,0FFFFFF00h
  00500a3e  8bc2                  mov       eax,edx
  00500a40  66014632              add       [esi+32h],ax

LAB_00500a44:
  00500a44  896e5c                mov       [esi+5Ch],ebp
  00500a47  e901010000            jmp       00500B4Dh
  00500a4c  66395e18              cmp       [esi+18h],bx
  00500a50  740a                  je        short 00500A5Ch
  00500a52  0d00000001            or        eax,1000000h
  00500a57  895e18                mov       [esi+18h],ebx
  00500a5a  8906                  mov       [esi],eax

LAB_00500a5c:
  00500a5c  0fbf4610              movsx     eax,word [esi+10h]
  00500a60  39442410              cmp       [esp+10h],eax
  00500a64  7d09                  jge       short 00500A6Fh
  00500a66  c6464300              mov       byte [esi+43h],0
  00500a6a  e9de000000            jmp       00500B4Dh

LAB_00500a6f:
  00500a6f  8a4643                mov       al,[esi+43h]
  00500a72  84c0                  test      al,al
  00500a74  7405                  je        short 00500A7Bh
  00500a76  fec8                  dec       al
  00500a78  884643                mov       [esi+43h],al

LAB_00500a7b:
  00500a7b  8b44243c              mov       eax,[esp+3Ch]
  00500a7f  8d8880000000          lea       ecx,[eax+80h]
  00500a85  81e1ff0f0000          and       ecx,0FFFh
  00500a8b  81f900010000          cmp       ecx,100h
  00500a91  7e1b                  jle       short 00500AAEh
  00500a93  25ff0f0000            and       eax,0FFFh
  00500a98  33d2                  xor       edx,edx
  00500a9a  3d00080000            cmp       eax,800h
  00500a9f  0f9dc2                setge     dl
  00500aa2  4a                    dec       edx
  00500aa3  81e200010000          and       edx,100h
  00500aa9  83c280                add       edx,0FFFFFF80h
  00500aac  8bc2                  mov       eax,edx

LAB_00500aae:
  00500aae  66014632              add       [esi+32h],ax
  00500ab2  c7465c00400100        mov       dword [esi+5Ch],14000h
  00500ab9  e98f000000            jmp       00500B4Dh
  00500abe  8a4e43                mov       cl,[esi+43h]
  00500ac1  84c9                  test      cl,cl
  00500ac3  7405                  je        short 00500ACAh
  00500ac5  fec9                  dec       cl
  00500ac7  884e43                mov       [esi+43h],cl

LAB_00500aca:
  00500aca  66395e18              cmp       [esi+18h],bx
  00500ace  740a                  je        short 00500ADAh
  00500ad0  0d00000001            or        eax,1000000h
  00500ad5  895e18                mov       [esi+18h],ebx
  00500ad8  8906                  mov       [esi],eax

LAB_00500ada:
  00500ada  0fbf4610              movsx     eax,word [esi+10h]
  00500ade  50                    push      eax
  00500adf  c7465c00000100        mov       dword [esi+5Ch],10000h
  00500ae6  e8314f0400            call      00545A1Ch
  00500aeb  0faf465c              imul      eax,[esi+5Ch]
  00500aef  8b4e1c                mov       ecx,[esi+1Ch]
  00500af2  c1f80c                sar       eax,0Ch
  00500af5  03c8                  add       ecx,eax
  00500af7  894e1c                mov       [esi+1Ch],ecx
  00500afa  0fbf4e10              movsx     ecx,word [esi+10h]
  00500afe  51                    push      ecx
  00500aff  e8124f0400            call      00545A16h
  00500b04  0faf465c              imul      eax,[esi+5Ch]
  00500b08  8b4e24                mov       ecx,[esi+24h]
  00500b0b  896e5c                mov       [esi+5Ch],ebp
  00500b0e  c1f80c                sar       eax,0Ch
  00500b11  03c8                  add       ecx,eax
  00500b13  8b44243c              mov       eax,[esp+3Ch]
  00500b17  894e24                mov       [esi+24h],ecx
  00500b1a  8d9080000000          lea       edx,[eax+80h]
  00500b20  81e2ff0f0000          and       edx,0FFFh
  00500b26  81fa00010000          cmp       edx,100h
  00500b2c  7e1b                  jle       short 00500B49h
  00500b2e  25ff0f0000            and       eax,0FFFh
  00500b33  33c9                  xor       ecx,ecx
  00500b35  3d00080000            cmp       eax,800h
  00500b3a  0f9dc1                setge     cl
  00500b3d  49                    dec       ecx
  00500b3e  81e100010000          and       ecx,100h
  00500b44  83c180                add       ecx,0FFFFFF80h
  00500b47  8bc1                  mov       eax,ecx

LAB_00500b49:
  00500b49  66014632              add       [esi+32h],ax

LAB_00500b4d:
  00500b4d  66395e18              cmp       [esi+18h],bx
  00500b51  751f                  jne       short 00500B72h
  00500b53  66837e1a10            cmp       word [esi+1Ah],10h
  00500b58  7518                  jne       short 00500B72h
  00500b5a  8d561c                lea       edx,[esi+1Ch]
  00500b5d  52                    push      edx
  00500b5e  6800180000            push      1800h
  00500b63  686a010000            push      16Ah
  00500b68  6a24                  push      24h
  00500b6a  e8b1cf0300            call      0053DB20h
  00500b6f  83c410                add       esp,10h

LAB_00500b72:
  00500b72  8b442414              mov       eax,[esp+14h]
  00500b76  66c7869a0000000060    mov       word [esi+9Ah],6000h
  00500b7f  50                    push      eax
  00500b80  56                    push      esi
  00500b81  e8da75f8ff            call      00488160h
  00500b86  83c408                add       esp,8
  00500b89  56                    push      esi
  00500b8a  e8b12d0300            call      00533940h
  00500b8f  83c404                add       esp,4
  00500b92  56                    push      esi
  00500b93  e858effdff            call      004DFAF0h
  00500b98  8b4c2430              mov       ecx,[esp+30h]
  00500b9c  83c404                add       esp,4
  00500b9f  51                    push      ecx
  00500ba0  56                    push      esi
  00500ba1  e8ca2bfeff            call      004E3770h
  00500ba6  8b44241c              mov       eax,[esp+1Ch]
  00500baa  83c408                add       esp,8
  00500bad  3bc5                  cmp       eax,ebp
  00500baf  0f8428020000          je        near 00500DDDh
  00500bb5  f70617080000          test      dword [esi],817h
  00500bbb  0f851c020000          jne       near 00500DDDh
  00500bc1  8a4643                mov       al,[esi+43h]
  00500bc4  84c0                  test      al,al
  00500bc6  0f8511020000          jne       near 00500DDDh
  00500bcc  8b466c                mov       eax,[esi+6Ch]
  00500bcf  8b561c                mov       edx,[esi+1Ch]
  00500bd2  8b781c                mov       edi,[eax+1Ch]
  00500bd5  8b6824                mov       ebp,[eax+24h]
  00500bd8  8b4624                mov       eax,[esi+24h]
  00500bdb  2bfa                  sub       edi,edx
  00500bdd  2be8                  sub       ebp,eax
  00500bdf  c1ff0c                sar       edi,0Ch
  00500be2  c1fd0c                sar       ebp,0Ch
  00500be5  8bd7                  mov       edx,edi
  00500be7  8bc5                  mov       eax,ebp
  00500be9  0fafd7                imul      edx,edi
  00500bec  0fafc5                imul      eax,ebp
  00500bef  03d0                  add       edx,eax
  00500bf1  52                    push      edx
  00500bf2  e8194e0400            call      00545A10h
  00500bf7  55                    push      ebp
  00500bf8  57                    push      edi
  00500bf9  89442418              mov       [esp+18h],eax
  00500bfd  e8204e0400            call      00545A22h
  00500c02  668b4e32              mov       cx,[esi+32h]
  00500c06  2bc1                  sub       eax,ecx
  00500c08  25ff0f0000            and       eax,0FFFh
  00500c0d  8944243c              mov       [esp+3Ch],eax
  00500c11  e82a650400            call      00547140h
  00500c16  99                    cdq
  00500c17  b964000000            mov       ecx,64h
  00500c1c  f7f9                  idiv      ecx
  00500c1e  817c24102c010000      cmp       dword [esp+10h],12Ch
  00500c26  0f8d9d000000          jge       near 00500CC9h
  00500c2c  83fa32                cmp       edx,32h
  00500c2f  7d6a                  jge       short 00500C9Bh
  00500c31  8b44243c              mov       eax,[esp+3Ch]
  00500c35  0500010000            add       eax,100h
  00500c3a  25ff0f0000            and       eax,0FFFh
  00500c3f  3d00020000            cmp       eax,200h
  00500c44  0f8f27010000          jg        near 00500D71h
  00500c4a  f6c201                test      dl,1
  00500c4d  7426                  je        short 00500C75h
  00500c4f  8b0e                  mov       ecx,[esi]
  00500c51  c7461803000000        mov       dword [esi+18h],3
  00500c58  81e1fffffffe          and       ecx,0FEFFFFFFh
  00500c5e  c6464200              mov       byte [esi+42h],0
  00500c62  83c902                or        ecx,2
  00500c65  66c746100200          mov       word [esi+10h],2
  00500c6b  890e                  mov       [esi],ecx
  00500c6d  5f                    pop       edi
  00500c6e  5e                    pop       esi
  00500c6f  5d                    pop       ebp
  00500c70  5b                    pop       ebx
  00500c71  83c428                add       esp,28h
  00500c74  c3                    ret

LAB_00500c75:
  00500c75  8b16                  mov       edx,[esi]
  00500c77  c7461804000000        mov       dword [esi+18h],4
  00500c7e  81e2fffffffe          and       edx,0FEFFFFFFh
  00500c84  c6464201              mov       byte [esi+42h],1
  00500c88  83ca02                or        edx,2
  00500c8b  66c746100200          mov       word [esi+10h],2
  00500c91  8916                  mov       [esi],edx
  00500c93  5f                    pop       edi
  00500c94  5e                    pop       esi
  00500c95  5d                    pop       ebp
  00500c96  5b                    pop       ebx
  00500c97  83c428                add       esp,28h
  00500c9a  c3                    ret

LAB_00500c9b:
  00500c9b  83fa4b                cmp       edx,4Bh
  00500c9e  0f8de2000000          jge       near 00500D86h
  00500ca4  c6464202              mov       byte [esi+42h],2
  00500ca8  e893640400            call      00547140h
  00500cad  99                    cdq
  00500cae  b90a000000            mov       ecx,0Ah
  00500cb3  66c74610c800          mov       word [esi+10h],0C8h
  00500cb9  f7f9                  idiv      ecx
  00500cbb  80c214                add       dl,14h
  00500cbe  885643                mov       [esi+43h],dl
  00500cc1  5f                    pop       edi
  00500cc2  5e                    pop       esi
  00500cc3  5d                    pop       ebp
  00500cc4  5b                    pop       ebx
  00500cc5  83c428                add       esp,28h
  00500cc8  c3                    ret

LAB_00500cc9:
  00500cc9  83fa28                cmp       edx,28h
  00500ccc  7d3e                  jge       short 00500D0Ch
  00500cce  8b54243c              mov       edx,[esp+3Ch]
  00500cd2  81c200020000          add       edx,200h
  00500cd8  81e2ff0f0000          and       edx,0FFFh
  00500cde  81fa00040000          cmp       edx,400h
  00500ce4  0f8d87000000          jge       near 00500D71h
  00500cea  8b06                  mov       eax,[esi]
  00500cec  c7461807000000        mov       dword [esi+18h],7
  00500cf3  25fffffffe            and       eax,0FEFFFFFFh
  00500cf8  c6464202              mov       byte [esi+42h],2
  00500cfc  0c02                  or        al,2
  00500cfe  66895e10              mov       [esi+10h],bx
  00500d02  8906                  mov       [esi],eax
  00500d04  5f                    pop       edi
  00500d05  5e                    pop       esi
  00500d06  5d                    pop       ebp
  00500d07  5b                    pop       ebx
  00500d08  83c428                add       esp,28h
  00500d0b  c3                    ret

LAB_00500d0c:
  00500d0c  83fa3c                cmp       edx,3Ch
  00500d0f  7d25                  jge       short 00500D36h
  00500d11  c6464202              mov       byte [esi+42h],2
  00500d15  e826640400            call      00547140h
  00500d1a  99                    cdq
  00500d1b  b90a000000            mov       ecx,0Ah
  00500d20  66c74610c800          mov       word [esi+10h],0C8h
  00500d26  f7f9                  idiv      ecx
  00500d28  80c214                add       dl,14h
  00500d2b  885643                mov       [esi+43h],dl
  00500d2e  5f                    pop       edi
  00500d2f  5e                    pop       esi
  00500d30  5d                    pop       ebp
  00500d31  5b                    pop       ebx
  00500d32  83c428                add       esp,28h
  00500d35  c3                    ret

LAB_00500d36:
  00500d36  83fa50                cmp       edx,50h
  00500d39  7d46                  jge       short 00500D81h
  00500d3b  8b54243c              mov       edx,[esp+3Ch]
  00500d3f  81c200020000          add       edx,200h
  00500d45  81e2ff0f0000          and       edx,0FFFh
  00500d4b  81fa00040000          cmp       edx,400h
  00500d51  7d1e                  jge       short 00500D71h
  00500d53  8b06                  mov       eax,[esi]
  00500d55  c7461805000000        mov       dword [esi+18h],5
  00500d5c  0d04080001            or        eax,1000804h
  00500d61  66c746100300          mov       word [esi+10h],3
  00500d67  8906                  mov       [esi],eax
  00500d69  5f                    pop       edi
  00500d6a  5e                    pop       esi
  00500d6b  5d                    pop       ebp
  00500d6c  5b                    pop       ebx
  00500d6d  83c428                add       esp,28h
  00500d70  c3                    ret

LAB_00500d71:
  00500d71  c6464201              mov       byte [esi+42h],1
  00500d75  c6464301              mov       byte [esi+43h],1
  00500d79  5f                    pop       edi
  00500d7a  5e                    pop       esi
  00500d7b  5d                    pop       ebp
  00500d7c  5b                    pop       ebx
  00500d7d  83c428                add       esp,28h
  00500d80  c3                    ret

LAB_00500d81:
  00500d81  83fa5a                cmp       edx,5Ah
  00500d84  7d40                  jge       short 00500DC6h

LAB_00500d86:
  00500d86  c6464203              mov       byte [esi+42h],3
  00500d8a  e8b1630400            call      00547140h
  00500d8f  99                    cdq
  00500d90  b90a000000            mov       ecx,0Ah
  00500d95  f7f9                  idiv      ecx
  00500d97  80c214                add       dl,14h
  00500d9a  885643                mov       [esi+43h],dl
  00500d9d  e89e630400            call      00547140h
  00500da2  99                    cdq
  00500da3  b9000c0000            mov       ecx,0C00h
  00500da8  8b7c243c              mov       edi,[esp+3Ch]
  00500dac  f7f9                  idiv      ecx
  00500dae  03d7                  add       edx,edi
  00500db0  66035632              add       dx,[esi+32h]
  00500db4  81c200020000          add       edx,200h
  00500dba  66895610              mov       [esi+10h],dx
  00500dbe  5f                    pop       edi
  00500dbf  5e                    pop       esi
  00500dc0  5d                    pop       ebp
  00500dc1  5b                    pop       ebx
  00500dc2  83c428                add       esp,28h
  00500dc5  c3                    ret

LAB_00500dc6:
  00500dc6  c6464200              mov       byte [esi+42h],0
  00500dca  e871630400            call      00547140h
  00500dcf  99                    cdq
  00500dd0  b90a000000            mov       ecx,0Ah
  00500dd5  f7f9                  idiv      ecx
  00500dd7  80c214                add       dl,14h
  00500dda  885643                mov       [esi+43h],dl

LAB_00500ddd:
  00500ddd  5f                    pop       edi
  00500dde  5e                    pop       esi
  00500ddf  5d                    pop       ebp
  00500de0  5b                    pop       ebx
  00500de1  83c428                add       esp,28h
  00500de4  c3                    ret
  00500de5  90                    nop
  00500de6  8bff                  mov       edi,edi
  00500de8  7008                  jo        short 00500DF2h
  00500dea  50                    push      eax
  00500deb  0087085000dc          add       [edi-23FFAFF8h],al
  00500df1  085000                or        [eax],dl
  00500df4  3009                  xor       [ecx],cl
  00500df6  50                    push      eax
  00500df7  00b3095000ea          add       [ebx-15FFAFF7h],dh
  00500dfd  095000                or        [eax],edx
  00500e00  4c                    dec       esp
  00500e01  0a5000                or        dl,[eax]
  00500e04  be0a500090            mov       esi,9000500Ah
  00500e09  90                    nop
  00500e0a  90                    nop
  00500e0b  90                    nop
  00500e0c  90                    nop
  00500e0d  90                    nop
  00500e0e  90                    nop
  00500e0f  90                    nop

48 cible(s) de saut interne : 0x0050045f, 0x005004a0, 0x0050054a, 0x00500564, 0x0050059e, 0x005005a2, 0x005005f5, 0x00500629, 0x005006ca, 0x00500714, 0x00500721, 0x00500734, 0x0050076e, 0x00500772, 0x005007c5, 0x0050081c, 0x00500829, 0x00500831, 0x00500859, 0x005008a3, 0x005008eb, 0x0050090c, 0x00500996, 0x005009c3, 0x005009d6, 0x005009fa, 0x00500a22, 0x00500a44, 0x00500a5c, 0x00500a6f, 0x00500a7b, 0x00500aae, 0x00500aca, 0x00500ada, 0x00500b49, 0x00500b4d, 0x00500b72, 0x00500c75, 0x00500c9b, 0x00500cc9, 0x00500d0c, 0x00500d36, 0x00500d71, 0x00500d81, 0x00500d86, 0x00500dc6, 0x00500ddd, 0x00500df2
