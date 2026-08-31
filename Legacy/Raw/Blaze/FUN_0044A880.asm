Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a880  section .text  offset fichier 0x49c80  1552 octets

  0044a880  53                    push      ebx
  0044a881  55                    push      ebp
  0044a882  56                    push      esi
  0044a883  8b742410              mov       esi,[esp+10h]
  0044a887  33ed                  xor       ebp,ebp
  0044a889  57                    push      edi
  0044a88a  8b4678                mov       eax,[esi+78h]
  0044a88d  8bc8                  mov       ecx,eax
  0044a88f  3bcd                  cmp       ecx,ebp
  0044a891  7505                  jne       short 0044A898h
  0044a893  b958e55d00            mov       ecx,5DE558h

LAB_0044a898:
  0044a898  8b511c                mov       edx,[ecx+1Ch]
  0044a89b  66396e10              cmp       [esi+10h],bp
  0044a89f  89561c                mov       [esi+1Ch],edx
  0044a8a2  8b4924                mov       ecx,[ecx+24h]
  0044a8a5  894e24                mov       [esi+24h],ecx
  0044a8a8  b901000000            mov       ecx,1
  0044a8ad  bff4010000            mov       edi,1F4h
  0044a8b2  7555                  jne       short 0044A909h
  0044a8b4  3bc5                  cmp       eax,ebp
  0044a8b6  884e04                mov       [esi+4],cl
  0044a8b9  884e0a                mov       [esi+0Ah],cl
  0044a8bc  c6460b00              mov       byte [esi+0Bh],0
  0044a8c0  66894e10              mov       [esi+10h],cx
  0044a8c4  c6463800              mov       byte [esi+38h],0
  0044a8c8  c6463900              mov       byte [esi+39h],0
  0044a8cc  c6463a00              mov       byte [esi+3Ah],0
  0044a8d0  66897e28              mov       [esi+28h],di
  0044a8d4  66896e2a              mov       [esi+2Ah],bp
  0044a8d8  66897e2c              mov       [esi+2Ch],di
  0044a8dc  7505                  jne       short 0044A8E3h
  0044a8de  b858e55d00            mov       eax,5DE558h

LAB_0044a8e3:
  0044a8e3  8b501c                mov       edx,[eax+1Ch]
  0044a8e6  89561c                mov       [esi+1Ch],edx
  0044a8e9  8b5060                mov       edx,[eax+60h]
  0044a8ec  81c200800c00          add       edx,0C8000h
  0044a8f2  66896e12              mov       [esi+12h],bp
  0044a8f6  895620                mov       [esi+20h],edx
  0044a8f9  8b4024                mov       eax,[eax+24h]
  0044a8fc  894624                mov       [esi+24h],eax
  0044a8ff  8b4674                mov       eax,[esi+74h]
  0044a902  8b10                  mov       edx,[eax]
  0044a904  80ca80                or        dl,80h
  0044a907  8910                  mov       [eax],edx

LAB_0044a909:
  0044a909  668146325802          add       word [esi+32h],258h
  0044a90f  668b4632              mov       ax,[esi+32h]
  0044a913  663d0010              cmp       ax,1000h
  0044a917  7c09                  jl        short 0044A922h
  0044a919  0500f0ffff            add       eax,0FFFFF000h
  0044a91e  66894632              mov       [esi+32h],ax

LAB_0044a922:
  0044a922  66394e10              cmp       [esi+10h],cx
  0044a926  0f8580000000          jne       near 0044A9ACh
  0044a92c  8b4678                mov       eax,[esi+78h]
  0044a92f  3bc5                  cmp       eax,ebp
  0044a931  7505                  jne       short 0044A938h
  0044a933  b858e55d00            mov       eax,5DE558h

LAB_0044a938:
  0044a938  8b481c                mov       ecx,[eax+1Ch]
  0044a93b  894e1c                mov       [esi+1Ch],ecx
  0044a93e  8b5024                mov       edx,[eax+24h]
  0044a941  66017e28              add       [esi+28h],di
  0044a945  668b4e28              mov       cx,[esi+28h]
  0044a949  66017e2c              add       [esi+2Ch],di
  0044a94d  895624                mov       [esi+24h],edx
  0044a950  baf82a0000            mov       edx,2AF8h
  0044a955  663bca                cmp       cx,dx
  0044a958  7c08                  jl        short 0044A962h
  0044a95a  66895628              mov       [esi+28h],dx
  0044a95e  6689562c              mov       [esi+2Ch],dx

LAB_0044a962:
  0044a962  66017e2a              add       [esi+2Ah],di
  0044a966  6683461209            add       word [esi+12h],9
  0044a96b  0fbf7e12              movsx     edi,word [esi+12h]
  0044a96f  8b5860                mov       ebx,[eax+60h]
  0044a972  668b4e2a              mov       cx,[esi+2Ah]
  0044a976  c1e70c                shl       edi,0Ch
  0044a979  2bdf                  sub       ebx,edi
  0044a97b  81c300800c00          add       ebx,0C8000h
  0044a981  663bca                cmp       cx,dx
  0044a984  895e20                mov       [esi+20h],ebx
  0044a987  7c14                  jl        short 0044A99Dh
  0044a989  6689562a              mov       [esi+2Ah],dx
  0044a98d  8b4060                mov       eax,[eax+60h]
  0044a990  894620                mov       [esi+20h],eax
  0044a993  66c746100200          mov       word [esi+10h],2
  0044a999  66896e12              mov       [esi+12h],bp

LAB_0044a99d:
  0044a99d  6a3c                  push      3Ch
  0044a99f  6a28                  push      28h
  0044a9a1  6a28                  push      28h
  0044a9a3  56                    push      esi
  0044a9a4  e837a4fbff            call      00404DE0h
  0044a9a9  83c410                add       esp,10h

LAB_0044a9ac:
  0044a9ac  66837e1002            cmp       word [esi+10h],2
  0044a9b1  0f857a040000          jne       near 0044AE31h
  0044a9b7  8b4678                mov       eax,[esi+78h]
  0044a9ba  3bc5                  cmp       eax,ebp
  0044a9bc  7505                  jne       short 0044A9C3h
  0044a9be  b858e55d00            mov       eax,5DE558h

LAB_0044a9c3:
  0044a9c3  8b481c                mov       ecx,[eax+1Ch]
  0044a9c6  894e1c                mov       [esi+1Ch],ecx
  0044a9c9  8b5060                mov       edx,[eax+60h]
  0044a9cc  0fbf4e12              movsx     ecx,word [esi+12h]
  0044a9d0  895620                mov       [esi+20h],edx
  0044a9d3  8b4024                mov       eax,[eax+24h]
  0044a9d6  51                    push      ecx
  0044a9d7  894624                mov       [esi+24h],eax
  0044a9da  e83db00f00            call      00545A1Ch
  0044a9df  0fbf5612              movsx     edx,word [esi+12h]
  0044a9e3  c1f807                sar       eax,7
  0044a9e6  2c38                  sub       al,38h
  0044a9e8  52                    push      edx
  0044a9e9  884638                mov       [esi+38h],al
  0044a9ec  e82bb00f00            call      00545A1Ch
  0044a9f1  c1f807                sar       eax,7
  0044a9f4  2c38                  sub       al,38h
  0044a9f6  884639                mov       [esi+39h],al
  0044a9f9  0fbf4612              movsx     eax,word [esi+12h]
  0044a9fd  50                    push      eax
  0044a9fe  e819b00f00            call      00545A1Ch
  0044aa03  668146122c01          add       word [esi+12h],12Ch
  0044aa09  c1f807                sar       eax,7
  0044aa0c  2c38                  sub       al,38h
  0044aa0e  88463a                mov       [esi+3Ah],al
  0044aa11  668b4612              mov       ax,[esi+12h]
  0044aa15  663d0010              cmp       ax,1000h
  0044aa19  7c09                  jl        short 0044AA24h
  0044aa1b  0500f0ffff            add       eax,0FFFFF000h
  0044aa20  66894612              mov       [esi+12h],ax

LAB_0044aa24:
  0044aa24  f70600008000          test      dword [esi],800000h
  0044aa2a  0f84c7000000          je        near 0044AAF7h
  0044aa30  8b4674                mov       eax,[esi+74h]
  0044aa33  8b38                  mov       edi,[eax]
  0044aa35  81cf80000000          or        edi,80h
  0044aa3b  8938                  mov       [eax],edi
  0044aa3d  8b7e74                mov       edi,[esi+74h]
  0044aa40  8b15b4876000          mov       edx,[6087B4h]
  0044aa46  33c0                  xor       eax,eax
  0044aa48  8a4708                mov       al,[edi+8]
  0044aa4b  8b1a                  mov       ebx,[edx]
  0044aa4d  8d4878                lea       ecx,[eax+78h]
  0044aa50  8d14c0                lea       edx,[eax+eax*8]
  0044aa53  c1e10d                shl       ecx,0Dh
  0044aa56  03cb                  add       ecx,ebx
  0044aa58  8d1450                lea       edx,[eax+edx*2]
  0044aa5b  33db                  xor       ebx,ebx
  0044aa5d  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0044aa64  c1e304                shl       ebx,4
  0044aa67  668b93204a6000        mov       dx,[ebx+604A20h]
  0044aa6e  668591c41f0000        test      [ecx+1FC4h],dx
  0044aa75  750d                  jne       short 0044AA84h
  0044aa77  66c746100300          mov       word [esi+10h],3
  0044aa7d  8b17                  mov       edx,[edi]
  0044aa7f  80e27f                and       dl,7Fh
  0044aa82  8917                  mov       [edi],edx

LAB_0044aa84:
  0044aa84  f78140010000e8030000  test      dword [ecx+140h],3E8h
  0044aa8e  7515                  jne       short 0044AAA5h
  0044aa90  8d1480                lea       edx,[eax+eax*4]
  0044aa93  c1e203                shl       edx,3
  0044aa96  2bd0                  sub       edx,eax
  0044aa98  f70495c0935e0000000040  test      dword [edx*4+5E93C0h],40000000h
  0044aaa3  7410                  je        short 0044AAB5h

LAB_0044aaa5:
  0044aaa5  8b4674                mov       eax,[esi+74h]
  0044aaa8  66c746100300          mov       word [esi+10h],3
  0044aaae  8b10                  mov       edx,[eax]
  0044aab0  80e27f                and       dl,7Fh
  0044aab3  8910                  mov       [eax],edx

LAB_0044aab5:
  0044aab5  a1f02c5f00            mov       eax,[5F2CF0h]
  0044aaba  33d2                  xor       edx,edx
  0044aabc  bf0a000000            mov       edi,0Ah
  0044aac1  f7f7                  div       edi
  0044aac3  85d2                  test      edx,edx
  0044aac5  7530                  jne       short 0044AAF7h
  0044aac7  668b814e010000        mov       ax,[ecx+14Eh]
  0044aace  663d0200              cmp       ax,2
  0044aad2  7c0c                  jl        short 0044AAE0h
  0044aad4  83c0fe                add       eax,0FFFFFFFEh
  0044aad7  6689814e010000        mov       [ecx+14Eh],ax
  0044aade  eb17                  jmp       short 0044AAF7h

LAB_0044aae0:
  0044aae0  6689a94e010000        mov       [ecx+14Eh],bp
  0044aae7  8b4674                mov       eax,[esi+74h]
  0044aaea  66c746100300          mov       word [esi+10h],3
  0044aaf0  8b08                  mov       ecx,[eax]
  0044aaf2  80e17f                and       cl,7Fh
  0044aaf5  8908                  mov       [eax],ecx

LAB_0044aaf7:
  0044aaf7  8b4674                mov       eax,[esi+74h]
  0044aafa  3bc5                  cmp       eax,ebp
  0044aafc  7505                  jne       short 0044AB03h
  0044aafe  b858e55d00            mov       eax,5DE558h

LAB_0044ab03:
  0044ab03  f70000008000          test      dword [eax],800000h
  0044ab09  0f8422030000          je        near 0044AE31h
  0044ab0f  bfbcca5e00            mov       edi,5ECABCh
  0044ab14  bb20000000            mov       ebx,20h

LAB_0044ab19:
  0044ab19  8b47e4                mov       eax,[edi-1Ch]
  0044ab1c  a900000080            test      eax,80000000h
  0044ab21  0f84a9000000          je        near 0044ABD0h
  0044ab27  a900000040            test      eax,40000000h
  0044ab2c  0f859e000000          jne       near 0044ABD0h
  0044ab32  a900004000            test      eax,400000h
  0044ab37  0f8493000000          je        near 0044ABD0h
  0044ab3d  8b461c                mov       eax,[esi+1Ch]
  0044ab40  8b17                  mov       edx,[edi]
  0044ab42  8b4e20                mov       ecx,[esi+20h]
  0044ab45  8b6f04                mov       ebp,[edi+4]
  0044ab48  2bc2                  sub       eax,edx
  0044ab4a  8b5624                mov       edx,[esi+24h]
  0044ab4d  2bcd                  sub       ecx,ebp
  0044ab4f  8b6f08                mov       ebp,[edi+8]
  0044ab52  c1f80c                sar       eax,0Ch
  0044ab55  2bd5                  sub       edx,ebp
  0044ab57  8be8                  mov       ebp,eax
  0044ab59  c1f90c                sar       ecx,0Ch
  0044ab5c  0fafe8                imul      ebp,eax
  0044ab5f  8bc1                  mov       eax,ecx
  0044ab61  c1fa0c                sar       edx,0Ch
  0044ab64  0fafc1                imul      eax,ecx
  0044ab67  8bca                  mov       ecx,edx
  0044ab69  03e8                  add       ebp,eax
  0044ab6b  0fafca                imul      ecx,edx
  0044ab6e  03e9                  add       ebp,ecx
  0044ab70  55                    push      ebp
  0044ab71  e89aae0f00            call      00545A10h
  0044ab76  3d36010000            cmp       eax,136h
  0044ab7b  7d53                  jge       short 0044ABD0h
  0044ab7d  8a463a                mov       al,[esi+3Ah]
  0044ab80  6aff                  push      0FFFFFFFFh
  0044ab82  6a00                  push      0
  0044ab84  68a5000000            push      0A5h
  0044ab89  6a00                  push      0
  0044ab8b  049c                  add       al,9Ch
  0044ab8d  57                    push      edi
  0044ab8e  56                    push      esi
  0044ab8f  88463a                mov       [esi+3Ah],al
  0044ab92  66c7869a0000000900    mov       word [esi+9Ah],9
  0044ab9b  e8a0000300            call      0047AC40h
  0044aba0  8b17                  mov       edx,[edi]
  0044aba2  8b461c                mov       eax,[esi+1Ch]
  0044aba5  2bd0                  sub       edx,eax
  0044aba7  8b4708                mov       eax,[edi+8]
  0044abaa  c1fa03                sar       edx,3
  0044abad  895734                mov       [edi+34h],edx
  0044abb0  8b4e24                mov       ecx,[esi+24h]
  0044abb3  2bc1                  sub       eax,ecx
  0044abb5  8b4fe4                mov       ecx,[edi-1Ch]
  0044abb8  80e5fb                and       ch,0FBh
  0044abbb  83c418                add       esp,18h
  0044abbe  c1f803                sar       eax,3
  0044abc1  83c940                or        ecx,40h
  0044abc4  89473c                mov       [edi+3Ch],eax
  0044abc7  66c7477a1000          mov       word [edi+7Ah],10h
  0044abcd  894fe4                mov       [edi-1Ch],ecx

LAB_0044abd0:
  0044abd0  81c79c000000          add       edi,9Ch
  0044abd6  4b                    dec       ebx
  0044abd7  0f853cffffff          jne       near 0044AB19h
  0044abdd  bb402d5f00            mov       ebx,5F2D40h
  0044abe2  c744241480000000      mov       dword [esp+14h],80h

LAB_0044abea:
  0044abea  8b03                  mov       eax,[ebx]
  0044abec  a900000080            test      eax,80000000h
  0044abf1  0f8495000000          je        near 0044AC8Ch
  0044abf7  f6c408                test      ah,8
  0044abfa  0f858c000000          jne       near 0044AC8Ch
  0044ac00  a900004000            test      eax,400000h
  0044ac05  0f8481000000          je        near 0044AC8Ch
  0044ac0b  f6c402                test      ah,2
  0044ac0e  747c                  je        short 0044AC8Ch
  0044ac10  f6c404                test      ah,4
  0044ac13  7477                  je        short 0044AC8Ch
  0044ac15  a880                  test      al,80h
  0044ac17  7473                  je        short 0044AC8Ch
  0044ac19  8b461c                mov       eax,[esi+1Ch]
  0044ac1c  8b531c                mov       edx,[ebx+1Ch]
  0044ac1f  8b4e20                mov       ecx,[esi+20h]
  0044ac22  8b6b20                mov       ebp,[ebx+20h]
  0044ac25  8d7b1c                lea       edi,[ebx+1Ch]
  0044ac28  2bc2                  sub       eax,edx
  0044ac2a  8b5624                mov       edx,[esi+24h]
  0044ac2d  2bcd                  sub       ecx,ebp
  0044ac2f  8b6f08                mov       ebp,[edi+8]
  0044ac32  c1f80c                sar       eax,0Ch
  0044ac35  2bd5                  sub       edx,ebp
  0044ac37  8be8                  mov       ebp,eax
  0044ac39  c1f90c                sar       ecx,0Ch
  0044ac3c  0fafe8                imul      ebp,eax
  0044ac3f  8bc1                  mov       eax,ecx
  0044ac41  c1fa0c                sar       edx,0Ch
  0044ac44  0fafc1                imul      eax,ecx
  0044ac47  8bca                  mov       ecx,edx
  0044ac49  03e8                  add       ebp,eax
  0044ac4b  0fafca                imul      ecx,edx
  0044ac4e  03e9                  add       ebp,ecx
  0044ac50  55                    push      ebp
  0044ac51  e8baad0f00            call      00545A10h
  0044ac56  3d36010000            cmp       eax,136h
  0044ac5b  7d2f                  jge       short 0044AC8Ch
  0044ac5d  8a463a                mov       al,[esi+3Ah]
  0044ac60  6aff                  push      0FFFFFFFFh
  0044ac62  6a00                  push      0
  0044ac64  68a5000000            push      0A5h
  0044ac69  6a00                  push      0
  0044ac6b  049c                  add       al,9Ch
  0044ac6d  57                    push      edi
  0044ac6e  56                    push      esi
  0044ac6f  88463a                mov       [esi+3Ah],al
  0044ac72  66c7869a0000000900    mov       word [esi+9Ah],9
  0044ac7b  e8c0ff0200            call      0047AC40h
  0044ac80  8b03                  mov       eax,[ebx]
  0044ac82  83c418                add       esp,18h
  0044ac85  0d00000040            or        eax,40000000h
  0044ac8a  8903                  mov       [ebx],eax

LAB_0044ac8c:
  0044ac8c  8b442414              mov       eax,[esp+14h]
  0044ac90  81c39c000000          add       ebx,9Ch
  0044ac96  48                    dec       eax
  0044ac97  89442414              mov       [esp+14h],eax
  0044ac9b  0f8549ffffff          jne       near 0044ABEAh
  0044aca1  bde0de5e00            mov       ebp,5EDEE0h
  0044aca6  c744241480000000      mov       dword [esp+14h],80h

LAB_0044acae:
  0044acae  8b4500                mov       eax,[ebp]
  0044acb1  a900000080            test      eax,80000000h
  0044acb6  0f8497000000          je        near 0044AD53h
  0044acbc  f6c408                test      ah,8
  0044acbf  0f858e000000          jne       near 0044AD53h
  0044acc5  a900004000            test      eax,400000h
  0044acca  0f8483000000          je        near 0044AD53h
  0044acd0  f6c402                test      ah,2
  0044acd3  747e                  je        short 0044AD53h
  0044acd5  f6c404                test      ah,4
  0044acd8  7479                  je        short 0044AD53h
  0044acda  a880                  test      al,80h
  0044acdc  7475                  je        short 0044AD53h
  0044acde  8b461c                mov       eax,[esi+1Ch]
  0044ace1  8b551c                mov       edx,[ebp+1Ch]
  0044ace4  8b4e20                mov       ecx,[esi+20h]
  0044ace7  8b5d20                mov       ebx,[ebp+20h]
  0044acea  8d7d1c                lea       edi,[ebp+1Ch]
  0044aced  2bc2                  sub       eax,edx
  0044acef  8b5624                mov       edx,[esi+24h]
  0044acf2  2bcb                  sub       ecx,ebx
  0044acf4  8b5f08                mov       ebx,[edi+8]
  0044acf7  c1f80c                sar       eax,0Ch
  0044acfa  2bd3                  sub       edx,ebx
  0044acfc  8bd8                  mov       ebx,eax
  0044acfe  c1f90c                sar       ecx,0Ch
  0044ad01  0fafd8                imul      ebx,eax
  0044ad04  8bc1                  mov       eax,ecx
  0044ad06  c1fa0c                sar       edx,0Ch
  0044ad09  0fafc1                imul      eax,ecx
  0044ad0c  8bca                  mov       ecx,edx
  0044ad0e  03d8                  add       ebx,eax
  0044ad10  0fafca                imul      ecx,edx
  0044ad13  03d9                  add       ebx,ecx
  0044ad15  53                    push      ebx
  0044ad16  e8f5ac0f00            call      00545A10h
  0044ad1b  3d36010000            cmp       eax,136h
  0044ad20  7d31                  jge       short 0044AD53h
  0044ad22  8a463a                mov       al,[esi+3Ah]
  0044ad25  6aff                  push      0FFFFFFFFh
  0044ad27  6a00                  push      0
  0044ad29  68a5000000            push      0A5h
  0044ad2e  6a00                  push      0
  0044ad30  049c                  add       al,9Ch
  0044ad32  57                    push      edi
  0044ad33  56                    push      esi
  0044ad34  88463a                mov       [esi+3Ah],al
  0044ad37  66c7869a0000000900    mov       word [esi+9Ah],9
  0044ad40  e8fbfe0200            call      0047AC40h
  0044ad45  8b4500                mov       eax,[ebp]
  0044ad48  83c418                add       esp,18h
  0044ad4b  0d00000040            or        eax,40000000h
  0044ad50  894500                mov       [ebp],eax

LAB_0044ad53:
  0044ad53  8b442414              mov       eax,[esp+14h]
  0044ad57  81c59c000000          add       ebp,9Ch
  0044ad5d  48                    dec       eax
  0044ad5e  89442414              mov       [esp+14h],eax
  0044ad62  0f8546ffffff          jne       near 0044ACAEh
  0044ad68  bd00f45f00            mov       ebp,5FF400h
  0044ad6d  c744241410000000      mov       dword [esp+14h],10h

LAB_0044ad75:
  0044ad75  8b4500                mov       eax,[ebp]
  0044ad78  a900000080            test      eax,80000000h
  0044ad7d  0f8497000000          je        near 0044AE1Ah
  0044ad83  f6c408                test      ah,8
  0044ad86  0f858e000000          jne       near 0044AE1Ah
  0044ad8c  a900004000            test      eax,400000h
  0044ad91  0f8483000000          je        near 0044AE1Ah
  0044ad97  f6c402                test      ah,2
  0044ad9a  747e                  je        short 0044AE1Ah
  0044ad9c  f6c404                test      ah,4
  0044ad9f  7479                  je        short 0044AE1Ah
  0044ada1  a880                  test      al,80h
  0044ada3  7475                  je        short 0044AE1Ah
  0044ada5  8b461c                mov       eax,[esi+1Ch]
  0044ada8  8b551c                mov       edx,[ebp+1Ch]
  0044adab  8b4e20                mov       ecx,[esi+20h]
  0044adae  8b5d20                mov       ebx,[ebp+20h]
  0044adb1  8d7d1c                lea       edi,[ebp+1Ch]
  0044adb4  2bc2                  sub       eax,edx
  0044adb6  8b5624                mov       edx,[esi+24h]
  0044adb9  2bcb                  sub       ecx,ebx
  0044adbb  8b5f08                mov       ebx,[edi+8]
  0044adbe  c1f80c                sar       eax,0Ch
  0044adc1  2bd3                  sub       edx,ebx
  0044adc3  8bd8                  mov       ebx,eax
  0044adc5  c1f90c                sar       ecx,0Ch
  0044adc8  0fafd8                imul      ebx,eax
  0044adcb  8bc1                  mov       eax,ecx
  0044adcd  c1fa0c                sar       edx,0Ch
  0044add0  0fafc1                imul      eax,ecx
  0044add3  8bca                  mov       ecx,edx
  0044add5  03d8                  add       ebx,eax
  0044add7  0fafca                imul      ecx,edx
  0044adda  03d9                  add       ebx,ecx
  0044addc  53                    push      ebx
  0044addd  e82eac0f00            call      00545A10h
  0044ade2  3d36010000            cmp       eax,136h
  0044ade7  7d31                  jge       short 0044AE1Ah
  0044ade9  8a463a                mov       al,[esi+3Ah]
  0044adec  6aff                  push      0FFFFFFFFh
  0044adee  6a00                  push      0
  0044adf0  68a5000000            push      0A5h
  0044adf5  6a00                  push      0
  0044adf7  049c                  add       al,9Ch
  0044adf9  57                    push      edi
  0044adfa  56                    push      esi
  0044adfb  88463a                mov       [esi+3Ah],al
  0044adfe  66c7869a0000000900    mov       word [esi+9Ah],9
  0044ae07  e834fe0200            call      0047AC40h
  0044ae0c  8b4500                mov       eax,[ebp]
  0044ae0f  83c418                add       esp,18h
  0044ae12  0d00000040            or        eax,40000000h
  0044ae17  894500                mov       [ebp],eax

LAB_0044ae1a:
  0044ae1a  8b442414              mov       eax,[esp+14h]
  0044ae1e  81c59c000000          add       ebp,9Ch
  0044ae24  48                    dec       eax
  0044ae25  89442414              mov       [esp+14h],eax
  0044ae29  0f8546ffffff          jne       near 0044AD75h
  0044ae2f  33ed                  xor       ebp,ebp

LAB_0044ae31:
  0044ae31  66837e1003            cmp       word [esi+10h],3
  0044ae36  7550                  jne       short 0044AE88h
  0044ae38  8b4678                mov       eax,[esi+78h]
  0044ae3b  3bc5                  cmp       eax,ebp
  0044ae3d  7505                  jne       short 0044AE44h
  0044ae3f  b858e55d00            mov       eax,5DE558h

LAB_0044ae44:
  0044ae44  8b501c                mov       edx,[eax+1Ch]
  0044ae47  6af6                  push      0FFFFFFF6h
  0044ae49  89561c                mov       [esi+1Ch],edx
  0044ae4c  8b4860                mov       ecx,[eax+60h]
  0044ae4f  894e20                mov       [esi+20h],ecx
  0044ae52  8b5024                mov       edx,[eax+24h]
  0044ae55  6af6                  push      0FFFFFFF6h
  0044ae57  6af6                  push      0FFFFFFF6h
  0044ae59  56                    push      esi
  0044ae5a  895624                mov       [esi+24h],edx
  0044ae5d  e87e9ffbff            call      00404DE0h
  0044ae62  8a463a                mov       al,[esi+3Ah]
  0044ae65  83c410                add       esp,10h
  0044ae68  84c0                  test      al,al
  0044ae6a  751c                  jne       short 0044AE88h
  0044ae6c  8b0e                  mov       ecx,[esi]
  0044ae6e  81e1ffffff7f          and       ecx,7FFFFFFFh
  0044ae74  890e                  mov       [esi],ecx
  0044ae76  8b766c                mov       esi,[esi+6Ch]
  0044ae79  3bf5                  cmp       esi,ebp
  0044ae7b  7505                  jne       short 0044AE82h
  0044ae7d  be58e55d00            mov       esi,5DE558h

LAB_0044ae82:
  0044ae82  66c746100200          mov       word [esi+10h],2

LAB_0044ae88:
  0044ae88  5f                    pop       edi
  0044ae89  5e                    pop       esi
  0044ae8a  5d                    pop       ebp
  0044ae8b  5b                    pop       ebx
  0044ae8c  c3                    ret
  0044ae8d  90                    nop
  0044ae8e  90                    nop
  0044ae8f  90                    nop

28 cible(s) de saut interne : 0x0044a898, 0x0044a8e3, 0x0044a909, 0x0044a922, 0x0044a938, 0x0044a962, 0x0044a99d, 0x0044a9ac, 0x0044a9c3, 0x0044aa24, 0x0044aa84, 0x0044aaa5, 0x0044aab5, 0x0044aae0, 0x0044aaf7, 0x0044ab03, 0x0044ab19, 0x0044abd0, 0x0044abea, 0x0044ac8c, 0x0044acae, 0x0044ad53, 0x0044ad75, 0x0044ae1a, 0x0044ae31, 0x0044ae44, 0x0044ae82, 0x0044ae88
