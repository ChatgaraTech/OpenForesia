Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00443d00  section .text  offset fichier 0x43100  1264 octets

  00443d00  83ec24                sub       esp,24h
  00443d03  a002975e00            mov       al,[5E9702h]
  00443d08  53                    push      ebx
  00443d09  33db                  xor       ebx,ebx
  00443d0b  c644240903            mov       byte [esp+9],3
  00443d10  a810                  test      al,10h
  00443d12  885c2408              mov       [esp+8],bl
  00443d16  885c240a              mov       [esp+0Ah],bl
  00443d1a  c644240b01            mov       byte [esp+0Bh],1
  00443d1f  885c240c              mov       [esp+0Ch],bl
  00443d23  885c240d              mov       [esp+0Dh],bl
  00443d27  885c2410              mov       [esp+10h],bl
  00443d2b  c644241103            mov       byte [esp+11h],3
  00443d30  c644241201            mov       byte [esp+12h],1
  00443d35  c644241301            mov       byte [esp+13h],1
  00443d3a  885c2414              mov       [esp+14h],bl
  00443d3e  885c2415              mov       [esp+15h],bl
  00443d42  885c2418              mov       [esp+18h],bl
  00443d46  c644241903            mov       byte [esp+19h],3
  00443d4b  c644241a02            mov       byte [esp+1Ah],2
  00443d50  c644241b01            mov       byte [esp+1Bh],1
  00443d55  885c241c              mov       [esp+1Ch],bl
  00443d59  885c241d              mov       [esp+1Dh],bl
  00443d5d  885c2420              mov       [esp+20h],bl
  00443d61  c644242103            mov       byte [esp+21h],3
  00443d66  c644242203            mov       byte [esp+22h],3
  00443d6b  c644242301            mov       byte [esp+23h],1
  00443d70  885c2424              mov       [esp+24h],bl
  00443d74  885c2425              mov       [esp+25h],bl
  00443d78  746a                  je        short 00443DE4h
  00443d7a  a820                  test      al,20h
  00443d7c  7466                  je        short 00443DE4h
  00443d7e  b005                  mov       al,5
  00443d80  8d4c2408              lea       ecx,[esp+8]
  00443d84  8844240d              mov       [esp+0Dh],al
  00443d88  88442415              mov       [esp+15h],al
  00443d8c  8844241d              mov       [esp+1Dh],al
  00443d90  88442425              mov       [esp+25h],al
  00443d94  8d442404              lea       eax,[esp+4]
  00443d98  50                    push      eax
  00443d99  51                    push      ecx
  00443d9a  e881020d00            call      00514020h
  00443d9f  83c408                add       esp,8
  00443da2  8d542404              lea       edx,[esp+4]
  00443da6  8d442410              lea       eax,[esp+10h]
  00443daa  52                    push      edx
  00443dab  50                    push      eax
  00443dac  e86f020d00            call      00514020h
  00443db1  83c408                add       esp,8
  00443db4  8d4c2404              lea       ecx,[esp+4]
  00443db8  8d542418              lea       edx,[esp+18h]
  00443dbc  51                    push      ecx
  00443dbd  52                    push      edx
  00443dbe  e85d020d00            call      00514020h
  00443dc3  83c408                add       esp,8
  00443dc6  8d442404              lea       eax,[esp+4]
  00443dca  8d4c2420              lea       ecx,[esp+20h]
  00443dce  50                    push      eax
  00443dcf  51                    push      ecx
  00443dd0  e84b020d00            call      00514020h
  00443dd5  a002975e00            mov       al,[5E9702h]
  00443dda  83c408                add       esp,8
  00443ddd  b902000000            mov       ecx,2
  00443de2  eb04                  jmp       short 00443DE8h

LAB_00443de4:
  00443de4  8b4c2404              mov       ecx,[esp+4]

LAB_00443de8:
  00443de8  a810                  test      al,10h
  00443dea  0f84f9000000          je        near 00443EE9h
  00443df0  a820                  test      al,20h
  00443df2  0f85e9000000          jne       near 00443EE1h
  00443df8  8d542404              lea       edx,[esp+4]
  00443dfc  8d442408              lea       eax,[esp+8]
  00443e00  52                    push      edx
  00443e01  50                    push      eax
  00443e02  c644241503            mov       byte [esp+15h],3
  00443e07  c644241d03            mov       byte [esp+1Dh],3
  00443e0c  c644242503            mov       byte [esp+25h],3
  00443e11  c644242d03            mov       byte [esp+2Dh],3
  00443e16  e805020d00            call      00514020h
  00443e1b  83c408                add       esp,8
  00443e1e  8d4c2404              lea       ecx,[esp+4]
  00443e22  8d542410              lea       edx,[esp+10h]
  00443e26  51                    push      ecx
  00443e27  52                    push      edx
  00443e28  e8f3010d00            call      00514020h
  00443e2d  83c408                add       esp,8
  00443e30  8d442404              lea       eax,[esp+4]
  00443e34  8d4c2418              lea       ecx,[esp+18h]
  00443e38  50                    push      eax
  00443e39  51                    push      ecx
  00443e3a  e8e1010d00            call      00514020h
  00443e3f  83c408                add       esp,8
  00443e42  8d542404              lea       edx,[esp+4]
  00443e46  8d442420              lea       eax,[esp+20h]
  00443e4a  52                    push      edx
  00443e4b  50                    push      eax
  00443e4c  e8cf010d00            call      00514020h
  00443e51  a058e05d00            mov       al,[5DE058h]
  00443e56  83c408                add       esp,8
  00443e59  3ac3                  cmp       al,bl
  00443e5b  757a                  jne       short 00443ED7h
  00443e5d  f70570e05d0000f0ffff  test      dword [5DE070h],0FFFFF000h
  00443e67  7e6e                  jle       short 00443ED7h
  00443e69  8b0d68e05d00          mov       ecx,[5DE068h]
  00443e6f  81e100f0ffff          and       ecx,0FFFFF000h
  00443e75  81f900007d00          cmp       ecx,7D0000h
  00443e7b  7e5a                  jle       short 00443ED7h
  00443e7d  b007                  mov       al,7
  00443e7f  8d542404              lea       edx,[esp+4]
  00443e83  8844240d              mov       [esp+0Dh],al
  00443e87  88442415              mov       [esp+15h],al
  00443e8b  8844241d              mov       [esp+1Dh],al
  00443e8f  88442425              mov       [esp+25h],al
  00443e93  8d442408              lea       eax,[esp+8]
  00443e97  52                    push      edx
  00443e98  50                    push      eax
  00443e99  e882010d00            call      00514020h
  00443e9e  83c408                add       esp,8
  00443ea1  8d4c2404              lea       ecx,[esp+4]
  00443ea5  8d542410              lea       edx,[esp+10h]
  00443ea9  51                    push      ecx
  00443eaa  52                    push      edx
  00443eab  e870010d00            call      00514020h
  00443eb0  83c408                add       esp,8
  00443eb3  8d442404              lea       eax,[esp+4]
  00443eb7  8d4c2418              lea       ecx,[esp+18h]
  00443ebb  50                    push      eax
  00443ebc  51                    push      ecx
  00443ebd  e85e010d00            call      00514020h
  00443ec2  83c408                add       esp,8
  00443ec5  8d542404              lea       edx,[esp+4]
  00443ec9  8d442420              lea       eax,[esp+20h]
  00443ecd  52                    push      edx
  00443ece  50                    push      eax
  00443ecf  e84c010d00            call      00514020h
  00443ed4  83c408                add       esp,8

LAB_00443ed7:
  00443ed7  a002975e00            mov       al,[5E9702h]
  00443edc  b901000000            mov       ecx,1

LAB_00443ee1:
  00443ee1  a810                  test      al,10h
  00443ee3  0f85ee000000          jne       near 00443FD7h

LAB_00443ee9:
  00443ee9  a820                  test      al,20h
  00443eeb  0f84e6000000          je        near 00443FD7h
  00443ef1  8d4c2404              lea       ecx,[esp+4]
  00443ef5  8d542408              lea       edx,[esp+8]
  00443ef9  b004                  mov       al,4
  00443efb  51                    push      ecx
  00443efc  52                    push      edx
  00443efd  88442415              mov       [esp+15h],al
  00443f01  8844241d              mov       [esp+1Dh],al
  00443f05  88442425              mov       [esp+25h],al
  00443f09  8844242d              mov       [esp+2Dh],al
  00443f0d  e80e010d00            call      00514020h
  00443f12  83c408                add       esp,8
  00443f15  8d442404              lea       eax,[esp+4]
  00443f19  8d4c2410              lea       ecx,[esp+10h]
  00443f1d  50                    push      eax
  00443f1e  51                    push      ecx
  00443f1f  e8fc000d00            call      00514020h
  00443f24  83c408                add       esp,8
  00443f27  8d542404              lea       edx,[esp+4]
  00443f2b  8d442418              lea       eax,[esp+18h]
  00443f2f  52                    push      edx
  00443f30  50                    push      eax
  00443f31  e8ea000d00            call      00514020h
  00443f36  83c408                add       esp,8
  00443f39  8d4c2404              lea       ecx,[esp+4]
  00443f3d  8d542420              lea       edx,[esp+20h]
  00443f41  51                    push      ecx
  00443f42  52                    push      edx
  00443f43  e8d8000d00            call      00514020h
  00443f48  a058e05d00            mov       al,[5DE058h]
  00443f4d  83c408                add       esp,8
  00443f50  3ac3                  cmp       al,bl
  00443f52  7579                  jne       short 00443FCDh
  00443f54  f70570e05d0000f0ffff  test      dword [5DE070h],0FFFFF000h
  00443f5e  7e6d                  jle       short 00443FCDh
  00443f60  a168e05d00            mov       eax,[5DE068h]
  00443f65  2500f0ffff            and       eax,0FFFFF000h
  00443f6a  3d000083ff            cmp       eax,0FF830000h
  00443f6f  7d5c                  jge       short 00443FCDh
  00443f71  8d4c2404              lea       ecx,[esp+4]
  00443f75  8d542408              lea       edx,[esp+8]
  00443f79  51                    push      ecx
  00443f7a  52                    push      edx
  00443f7b  c644241506            mov       byte [esp+15h],6
  00443f80  c644241d06            mov       byte [esp+1Dh],6
  00443f85  c644242506            mov       byte [esp+25h],6
  00443f8a  c644242d06            mov       byte [esp+2Dh],6
  00443f8f  e88c000d00            call      00514020h
  00443f94  83c408                add       esp,8
  00443f97  8d442404              lea       eax,[esp+4]
  00443f9b  8d4c2410              lea       ecx,[esp+10h]
  00443f9f  50                    push      eax
  00443fa0  51                    push      ecx
  00443fa1  e87a000d00            call      00514020h
  00443fa6  83c408                add       esp,8
  00443fa9  8d542404              lea       edx,[esp+4]
  00443fad  8d442418              lea       eax,[esp+18h]
  00443fb1  52                    push      edx
  00443fb2  50                    push      eax
  00443fb3  e868000d00            call      00514020h
  00443fb8  83c408                add       esp,8
  00443fbb  8d4c2404              lea       ecx,[esp+4]
  00443fbf  8d542420              lea       edx,[esp+20h]
  00443fc3  51                    push      ecx
  00443fc4  52                    push      edx
  00443fc5  e856000d00            call      00514020h
  00443fca  83c408                add       esp,8

LAB_00443fcd:
  00443fcd  a002975e00            mov       al,[5E9702h]
  00443fd2  b901000000            mov       ecx,1

LAB_00443fd7:
  00443fd7  a830                  test      al,30h
  00443fd9  0f855a010000          jne       near 00444139h
  00443fdf  8d442404              lea       eax,[esp+4]
  00443fe3  8d4c2408              lea       ecx,[esp+8]
  00443fe7  50                    push      eax
  00443fe8  51                    push      ecx
  00443fe9  885c2415              mov       [esp+15h],bl
  00443fed  885c241d              mov       [esp+1Dh],bl
  00443ff1  885c2425              mov       [esp+25h],bl
  00443ff5  885c242d              mov       [esp+2Dh],bl
  00443ff9  e822000d00            call      00514020h
  00443ffe  83c408                add       esp,8
  00444001  8d542404              lea       edx,[esp+4]
  00444005  8d442410              lea       eax,[esp+10h]
  00444009  52                    push      edx
  0044400a  50                    push      eax
  0044400b  e810000d00            call      00514020h
  00444010  83c408                add       esp,8
  00444013  8d4c2404              lea       ecx,[esp+4]
  00444017  8d542418              lea       edx,[esp+18h]
  0044401b  51                    push      ecx
  0044401c  52                    push      edx
  0044401d  e8feff0c00            call      00514020h
  00444022  83c408                add       esp,8
  00444025  8d442404              lea       eax,[esp+4]
  00444029  8d4c2420              lea       ecx,[esp+20h]
  0044402d  50                    push      eax
  0044402e  51                    push      ecx
  0044402f  e8ecff0c00            call      00514020h
  00444034  a058e05d00            mov       al,[5DE058h]
  00444039  83c408                add       esp,8
  0044403c  3ac3                  cmp       al,bl
  0044403e  0f85ee000000          jne       near 00444132h
  00444044  f70570e05d0000f0ffff  test      dword [5DE070h],0FFFFF000h
  0044404e  0f8ede000000          jle       near 00444132h
  00444054  a168e05d00            mov       eax,[5DE068h]
  00444059  8bd0                  mov       edx,eax
  0044405b  81e200f0ffff          and       edx,0FFFFF000h
  00444061  81fa00007d00          cmp       edx,7D0000h
  00444067  7e61                  jle       short 004440CAh
  00444069  8d442404              lea       eax,[esp+4]
  0044406d  8d4c2408              lea       ecx,[esp+8]
  00444071  50                    push      eax
  00444072  51                    push      ecx
  00444073  c644241502            mov       byte [esp+15h],2
  00444078  c644241d02            mov       byte [esp+1Dh],2
  0044407d  c644242502            mov       byte [esp+25h],2
  00444082  c644242d02            mov       byte [esp+2Dh],2
  00444087  e894ff0c00            call      00514020h
  0044408c  83c408                add       esp,8
  0044408f  8d542404              lea       edx,[esp+4]
  00444093  8d442410              lea       eax,[esp+10h]
  00444097  52                    push      edx
  00444098  50                    push      eax
  00444099  e882ff0c00            call      00514020h
  0044409e  83c408                add       esp,8
  004440a1  8d4c2404              lea       ecx,[esp+4]
  004440a5  8d542418              lea       edx,[esp+18h]
  004440a9  51                    push      ecx
  004440aa  52                    push      edx
  004440ab  e870ff0c00            call      00514020h
  004440b0  83c408                add       esp,8
  004440b3  8d442404              lea       eax,[esp+4]
  004440b7  8d4c2420              lea       ecx,[esp+20h]
  004440bb  50                    push      eax
  004440bc  51                    push      ecx
  004440bd  e85eff0c00            call      00514020h
  004440c2  a168e05d00            mov       eax,[5DE068h]
  004440c7  83c408                add       esp,8

LAB_004440ca:
  004440ca  2500f0ffff            and       eax,0FFFFF000h
  004440cf  3d000083ff            cmp       eax,0FF830000h
  004440d4  7d5c                  jge       short 00444132h
  004440d6  8d542404              lea       edx,[esp+4]
  004440da  8d442408              lea       eax,[esp+8]
  004440de  52                    push      edx
  004440df  50                    push      eax
  004440e0  c644241501            mov       byte [esp+15h],1
  004440e5  c644241d01            mov       byte [esp+1Dh],1
  004440ea  c644242501            mov       byte [esp+25h],1
  004440ef  c644242d01            mov       byte [esp+2Dh],1
  004440f4  e827ff0c00            call      00514020h
  004440f9  83c408                add       esp,8
  004440fc  8d4c2404              lea       ecx,[esp+4]
  00444100  8d542410              lea       edx,[esp+10h]
  00444104  51                    push      ecx
  00444105  52                    push      edx
  00444106  e815ff0c00            call      00514020h
  0044410b  83c408                add       esp,8
  0044410e  8d442404              lea       eax,[esp+4]
  00444112  8d4c2418              lea       ecx,[esp+18h]
  00444116  50                    push      eax
  00444117  51                    push      ecx
  00444118  e803ff0c00            call      00514020h
  0044411d  83c408                add       esp,8
  00444120  8d542404              lea       edx,[esp+4]
  00444124  8d442420              lea       eax,[esp+20h]
  00444128  52                    push      edx
  00444129  50                    push      eax
  0044412a  e8f1fe0c00            call      00514020h
  0044412f  83c408                add       esp,8

LAB_00444132:
  00444132  a002975e00            mov       al,[5E9702h]
  00444137  33c9                  xor       ecx,ecx

LAB_00444139:
  00444139  6683f901              cmp       cx,1
  0044413d  0f859b000000          jne       near 004441DEh
  00444143  8a0d03975e00          mov       cl,[5E9703h]
  00444149  fec1                  inc       cl
  0044414b  80f9c8                cmp       cl,0C8h
  0044414e  880d03975e00          mov       [5E9703h],cl
  00444154  0f8584000000          jne       near 004441DEh
  0044415a  8a0d04975e00          mov       cl,[5E9704h]
  00444160  881d03975e00          mov       [5E9703h],bl
  00444166  fec1                  inc       cl
  00444168  80f906                cmp       cl,6
  0044416b  880d04975e00          mov       [5E9704h],cl
  00444171  756b                  jne       short 004441DEh
  00444173  24ef                  and       al,0EFh
  00444175  8d4c2404              lea       ecx,[esp+4]
  00444179  8d542408              lea       edx,[esp+8]
  0044417d  24df                  and       al,0DFh
  0044417f  51                    push      ecx
  00444180  52                    push      edx
  00444181  a202975e00            mov       [5E9702h],al
  00444186  885c2415              mov       [esp+15h],bl
  0044418a  885c241d              mov       [esp+1Dh],bl
  0044418e  885c2425              mov       [esp+25h],bl
  00444192  885c242d              mov       [esp+2Dh],bl
  00444196  e885fe0c00            call      00514020h
  0044419b  83c408                add       esp,8
  0044419e  8d442404              lea       eax,[esp+4]
  004441a2  8d4c2410              lea       ecx,[esp+10h]
  004441a6  50                    push      eax
  004441a7  51                    push      ecx
  004441a8  e873fe0c00            call      00514020h
  004441ad  83c408                add       esp,8
  004441b0  8d542404              lea       edx,[esp+4]
  004441b4  8d442418              lea       eax,[esp+18h]
  004441b8  52                    push      edx
  004441b9  50                    push      eax
  004441ba  e861fe0c00            call      00514020h
  004441bf  83c408                add       esp,8
  004441c2  8d4c2404              lea       ecx,[esp+4]
  004441c6  8d542420              lea       edx,[esp+20h]
  004441ca  51                    push      ecx
  004441cb  52                    push      edx
  004441cc  e84ffe0c00            call      00514020h
  004441d1  83c408                add       esp,8
  004441d4  b801000000            mov       eax,1
  004441d9  5b                    pop       ebx
  004441da  83c424                add       esp,24h
  004441dd  c3                    ret

LAB_004441de:
  004441de  33c0                  xor       eax,eax
  004441e0  5b                    pop       ebx
  004441e1  83c424                add       esp,24h
  004441e4  c3                    ret
  004441e5  90                    nop
  004441e6  90                    nop
  004441e7  90                    nop
  004441e8  90                    nop
  004441e9  90                    nop
  004441ea  90                    nop
  004441eb  90                    nop
  004441ec  90                    nop
  004441ed  90                    nop
  004441ee  90                    nop
  004441ef  90                    nop

11 cible(s) de saut interne : 0x00443de4, 0x00443de8, 0x00443ed7, 0x00443ee1, 0x00443ee9, 0x00443fcd, 0x00443fd7, 0x004440ca, 0x00444132, 0x00444139, 0x004441de
