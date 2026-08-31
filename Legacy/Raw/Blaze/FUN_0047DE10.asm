
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047de10  section .text  offset fichier 0x7d210  346 octets

  0047de10  83ec0c                sub       esp,0Ch
  0047de13  53                    push      ebx
  0047de14  55                    push      ebp
  0047de15  56                    push      esi
  0047de16  8b74241c              mov       esi,[esp+1Ch]
  0047de1a  33c9                  xor       ecx,ecx
  0047de1c  57                    push      edi
  0047de1d  8b06                  mov       eax,[esi]
  0047de1f  66894c2410            mov       [esp+10h],cx
  0047de24  a900000002            test      eax,2000000h
  0047de29  66c7442412c4ff        mov       word [esp+12h],0FFC4h
  0047de30  66c74424143c00        mov       word [esp+14h],3Ch
  0047de37  66c7442416c800        mov       word [esp+16h],0C8h
  0047de3e  66894c2418            mov       [esp+18h],cx
  0047de43  66c744241a7800        mov       word [esp+1Ah],78h
  0047de4a  7429                  je        short 0047DE75h
  0047de4c  668b4628              mov       ax,[esi+28h]
  0047de50  66ff4612              inc       word [esi+12h]
  0047de54  668bd0                mov       dx,ax
  0047de57  66c1fa02              sar       dx,2
  0047de5b  2bc2                  sub       eax,edx
  0047de5d  663d0001              cmp       ax,100h
  0047de61  66894628              mov       [esi+28h],ax
  0047de65  6689462a              mov       [esi+2Ah],ax
  0047de69  7d0a                  jge       short 0047DE75h
  0047de6b  890e                  mov       [esi],ecx
  0047de6d  5f                    pop       edi
  0047de6e  5e                    pop       esi
  0047de6f  5d                    pop       ebp
  0047de70  5b                    pop       ebx
  0047de71  83c40c                add       esp,0Ch
  0047de74  c3                    ret

LAB_0047de75:
  0047de75  8a4e40                mov       cl,[esi+40h]
  0047de78  8b5e6c                mov       ebx,[esi+6Ch]
  0047de7b  fec1                  inc       cl
  0047de7d  8ac1                  mov       al,cl
  0047de7f  884e40                mov       [esi+40h],cl
  0047de82  3c07                  cmp       al,7
  0047de84  7204                  jb        short 0047DE8Ah
  0047de86  c6464000              mov       byte [esi+40h],0

LAB_0047de8a:
  0047de8a  e8b1920c00            call      00547140h
  0047de8f  8b4b1c                mov       ecx,[ebx+1Ch]
  0047de92  8bf8                  mov       edi,eax
  0047de94  83e77f                and       edi,7Fh
  0047de97  83ef40                sub       edi,40h
  0047de9a  c1e70c                shl       edi,0Ch
  0047de9d  03f9                  add       edi,ecx
  0047de9f  e89c920c00            call      00547140h
  0047dea4  8be8                  mov       ebp,eax
  0047dea6  8b4320                mov       eax,[ebx+20h]
  0047dea9  83e57f                and       ebp,7Fh
  0047deac  83ed40                sub       ebp,40h
  0047deaf  c1e50c                shl       ebp,0Ch
  0047deb2  03e8                  add       ebp,eax
  0047deb4  e887920c00            call      00547140h
  0047deb9  8b4b24                mov       ecx,[ebx+24h]
  0047debc  83e07f                and       eax,7Fh
  0047debf  83e840                sub       eax,40h
  0047dec2  8d561c                lea       edx,[esi+1Ch]
  0047dec5  c1e00c                shl       eax,0Ch
  0047dec8  03c1                  add       eax,ecx
  0047deca  8b0a                  mov       ecx,[edx]
  0047decc  2bf9                  sub       edi,ecx
  0047dece  89542420              mov       [esp+20h],edx
  0047ded2  c1ff02                sar       edi,2
  0047ded5  03f9                  add       edi,ecx
  0047ded7  8b4e20                mov       ecx,[esi+20h]
  0047deda  2be9                  sub       ebp,ecx
  0047dedc  893a                  mov       [edx],edi
  0047dede  c1fd02                sar       ebp,2
  0047dee1  03e9                  add       ebp,ecx
  0047dee3  8b4e24                mov       ecx,[esi+24h]
  0047dee6  2bc1                  sub       eax,ecx
  0047dee8  896e20                mov       [esi+20h],ebp
  0047deeb  c1f802                sar       eax,2
  0047deee  03c1                  add       eax,ecx
  0047def0  8b0e                  mov       ecx,[esi]
  0047def2  894624                mov       [esi+24h],eax
  0047def5  8b03                  mov       eax,[ebx]
  0047def7  2500000003            and       eax,3000000h
  0047defc  0bc8                  or        ecx,eax
  0047defe  66837e1200            cmp       word [esi+12h],0
  0047df03  890e                  mov       [esi],ecx
  0047df05  755b                  jne       short 0047DF62h
  0047df07  8d4c2410              lea       ecx,[esp+10h]
  0047df0b  51                    push      ecx
  0047df0c  56                    push      esi
  0047df0d  e88e6dffff            call      00474CA0h
  0047df12  8bf8                  mov       edi,eax
  0047df14  83c408                add       esp,8
  0047df17  85ff                  test      edi,edi
  0047df19  7447                  je        short 0047DF62h
  0047df1b  8b4e74                mov       ecx,[esi+74h]
  0047df1e  33d2                  xor       edx,edx
  0047df20  8a5606                mov       dl,[esi+6]
  0047df23  33c0                  xor       eax,eax
  0047df25  8a4607                mov       al,[esi+7]
  0047df28  6a00                  push      0
  0047df2a  52                    push      edx
  0047df2b  50                    push      eax
  0047df2c  57                    push      edi
  0047df2d  51                    push      ecx
  0047df2e  e87dc30b00            call      0053A2B0h
  0047df33  8b542434              mov       edx,[esp+34h]
  0047df37  8b1f                  mov       ebx,[edi]
  0047df39  83c414                add       esp,14h
  0047df3c  83cb40                or        ebx,40h
  0047df3f  66c787960000001000    mov       word [edi+96h],10h
  0047df48  891f                  mov       [edi],ebx
  0047df4a  52                    push      edx
  0047df4b  6800120000            push      1200h
  0047df50  6800020000            push      200h
  0047df55  6a0e                  push      0Eh
  0047df57  897e78                mov       [esi+78h],edi
  0047df5a  e8c1fb0b00            call      0053DB20h
  0047df5f  83c410                add       esp,10h

LAB_0047df62:
  0047df62  5f                    pop       edi
  0047df63  5e                    pop       esi
  0047df64  5d                    pop       ebp
  0047df65  5b                    pop       ebx
  0047df66  83c40c                add       esp,0Ch
  0047df69  c3                    ret

3 cible(s) de saut interne : 0x0047de75, 0x0047de8a, 0x0047df62
