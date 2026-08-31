
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047ae80  section .text  offset fichier 0x7a280  455 octets

  0047ae80  83ec18                sub       esp,18h
  0047ae83  53                    push      ebx
  0047ae84  55                    push      ebp
  0047ae85  56                    push      esi
  0047ae86  8b742428              mov       esi,[esp+28h]
  0047ae8a  b864000000            mov       eax,64h
  0047ae8f  57                    push      edi
  0047ae90  66837e1000            cmp       word [esi+10h],0
  0047ae95  66c744241000fc        mov       word [esp+10h],0FC00h
  0047ae9c  66c74424129cff        mov       word [esp+12h],0FF9Ch
  0047aea3  66c74424143200        mov       word [esp+14h],32h
  0047aeaa  6689442416            mov       [esp+16h],ax
  0047aeaf  66c74424180000        mov       word [esp+18h],0
  0047aeb6  668944241a            mov       [esp+1Ah],ax
  0047aebb  7506                  jne       short 0047AEC3h
  0047aebd  8b06                  mov       eax,[esi]
  0047aebf  0c80                  or        al,80h
  0047aec1  8906                  mov       [esi],eax

LAB_0047aec3:
  0047aec3  0fbf7e32              movsx     edi,word [esi+32h]
  0047aec7  81ef00040000          sub       edi,400h
  0047aecd  8d5e1c                lea       ebx,[esi+1Ch]
  0047aed0  57                    push      edi
  0047aed1  e846ab0c00            call      00545A1Ch
  0047aed6  8b0b                  mov       ecx,[ebx]
  0047aed8  8d0480                lea       eax,[eax+eax*4]
  0047aedb  c1e003                shl       eax,3
  0047aede  03c8                  add       ecx,eax
  0047aee0  57                    push      edi
  0047aee1  890b                  mov       [ebx],ecx
  0047aee3  e82eab0c00            call      00545A16h
  0047aee8  8d0c80                lea       ecx,[eax+eax*4]
  0047aeeb  8b4624                mov       eax,[esi+24h]
  0047aeee  c1e103                shl       ecx,3
  0047aef1  81c700080000          add       edi,800h
  0047aef7  03c1                  add       eax,ecx
  0047aef9  57                    push      edi
  0047aefa  894624                mov       [esi+24h],eax
  0047aefd  e81aab0c00            call      00545A1Ch
  0047af02  8d0440                lea       eax,[eax+eax*2]
  0047af05  8d2c80                lea       ebp,[eax+eax*4]
  0047af08  e833c20c00            call      00547140h
  0047af0d  83e03f                and       eax,3Fh
  0047af10  83e820                sub       eax,20h
  0047af13  c1e00c                shl       eax,0Ch
  0047af16  8d14a8                lea       edx,[eax+ebp*4]
  0047af19  8b03                  mov       eax,[ebx]
  0047af1b  03d0                  add       edx,eax
  0047af1d  8954241c              mov       [esp+1Ch],edx
  0047af21  e81ac20c00            call      00547140h
  0047af26  8b6e20                mov       ebp,[esi+20h]
  0047af29  83e03f                and       eax,3Fh
  0047af2c  83e820                sub       eax,20h
  0047af2f  57                    push      edi
  0047af30  c1e00c                shl       eax,0Ch
  0047af33  03c5                  add       eax,ebp
  0047af35  89442424              mov       [esp+24h],eax
  0047af39  e8d8aa0c00            call      00545A16h
  0047af3e  8d0440                lea       eax,[eax+eax*2]
  0047af41  8d3c80                lea       edi,[eax+eax*4]
  0047af44  e8f7c10c00            call      00547140h
  0047af49  8b4e24                mov       ecx,[esi+24h]
  0047af4c  83e03f                and       eax,3Fh
  0047af4f  83e820                sub       eax,20h
  0047af52  6aff                  push      0FFFFFFFFh
  0047af54  c1e00c                shl       eax,0Ch
  0047af57  8d542420              lea       edx,[esp+20h]
  0047af5b  8d04b8                lea       eax,[eax+edi*4]
  0047af5e  03c1                  add       eax,ecx
  0047af60  33c9                  xor       ecx,ecx
  0047af62  8a4e0b                mov       cl,[esi+0Bh]
  0047af65  89442428              mov       [esp+28h],eax
  0047af69  51                    push      ecx
  0047af6a  6a46                  push      46h
  0047af6c  68d81a5500            push      551AD8h
  0047af71  52                    push      edx
  0047af72  56                    push      esi
  0047af73  e8c8fcffff            call      0047AC40h
  0047af78  8b7e20                mov       edi,[esi+20h]
  0047af7b  83c418                add       esp,18h
  0047af7e  56                    push      esi
  0047af7f  e8bc890b00            call      00533940h
  0047af84  8b06                  mov       eax,[esi]
  0047af86  83c404                add       esp,4
  0047af89  a900000040            test      eax,40000000h
  0047af8e  897e20                mov       [esi+20h],edi
  0047af91  0f85a2000000          jne       near 0047B039h
  0047af97  f7869000000002880000  test      dword [esi+90h],8802h
  0047afa1  0f8592000000          jne       near 0047B039h
  0047afa7  8d442410              lea       eax,[esp+10h]
  0047afab  50                    push      eax
  0047afac  56                    push      esi
  0047afad  e8ee9cffff            call      00474CA0h
  0047afb2  8bf8                  mov       edi,eax
  0047afb4  83c408                add       esp,8
  0047afb7  85ff                  test      edi,edi
  0047afb9  7473                  je        short 0047B02Eh
  0047afbb  8b4674                mov       eax,[esi+74h]
  0047afbe  33c9                  xor       ecx,ecx
  0047afc0  8a4e06                mov       cl,[esi+6]
  0047afc3  33d2                  xor       edx,edx
  0047afc5  8a5607                mov       dl,[esi+7]
  0047afc8  6a00                  push      0
  0047afca  51                    push      ecx
  0047afcb  52                    push      edx
  0047afcc  57                    push      edi
  0047afcd  50                    push      eax
  0047afce  e8ddf20b00            call      0053A2B0h
  0047afd3  0fbf6e32              movsx     ebp,word [esi+32h]
  0047afd7  83c414                add       esp,14h
  0047afda  55                    push      ebp
  0047afdb  e83caa0c00            call      00545A1Ch
  0047afe0  8b4f50                mov       ecx,[edi+50h]
  0047afe3  55                    push      ebp
  0047afe4  c1e004                shl       eax,4
  0047afe7  03c8                  add       ecx,eax
  0047afe9  894f50                mov       [edi+50h],ecx
  0047afec  e825aa0c00            call      00545A16h
  0047aff1  8b4f58                mov       ecx,[edi+58h]
  0047aff4  53                    push      ebx
  0047aff5  c1e004                shl       eax,4
  0047aff8  03c8                  add       ecx,eax
  0047affa  8b07                  mov       eax,[edi]
  0047affc  6800120000            push      1200h
  0047b001  0c40                  or        al,40h
  0047b003  6853060000            push      653h
  0047b008  6a13                  push      13h
  0047b00a  894f58                mov       [edi+58h],ecx
  0047b00d  66c787960000001000    mov       word [edi+96h],10h
  0047b016  8907                  mov       [edi],eax
  0047b018  e8032b0c00            call      0053DB20h
  0047b01d  83c410                add       esp,10h
  0047b020  c70600000000          mov       dword [esi],0
  0047b026  5f                    pop       edi
  0047b027  5e                    pop       esi
  0047b028  5d                    pop       ebp
  0047b029  5b                    pop       ebx
  0047b02a  83c418                add       esp,18h
  0047b02d  c3                    ret

LAB_0047b02e:
  0047b02e  66ff4610              inc       word [esi+10h]
  0047b032  66837e100c            cmp       word [esi+10h],0Ch
  0047b037  7c06                  jl        short 0047B03Fh

LAB_0047b039:
  0047b039  c70600000000          mov       dword [esi],0

LAB_0047b03f:
  0047b03f  5f                    pop       edi
  0047b040  5e                    pop       esi
  0047b041  5d                    pop       ebp
  0047b042  5b                    pop       ebx
  0047b043  83c418                add       esp,18h
  0047b046  c3                    ret

4 cible(s) de saut interne : 0x0047aec3, 0x0047b02e, 0x0047b039, 0x0047b03f
