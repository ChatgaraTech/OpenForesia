Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040b940  section .text  offset fichier 0xad40  880 octets

  0040b940  81ecc0000000          sub       esp,0C0h
  0040b946  53                    push      ebx
  0040b947  55                    push      ebp
  0040b948  56                    push      esi
  0040b949  57                    push      edi
  0040b94a  b926000000            mov       ecx,26h
  0040b94f  33c0                  xor       eax,eax
  0040b951  8d7c2438              lea       edi,[esp+38h]
  0040b955  c744243400000000      mov       dword [esp+34h],0
  0040b95d  f3ab                  rep stosd
  0040b95f  66c7442410a903        mov       word [esp+10h],3A9h
  0040b966  66c7442412d303        mov       word [esp+12h],3D3h
  0040b96d  66c74424140004        mov       word [esp+14h],400h
  0040b974  66c74424165504        mov       word [esp+16h],455h
  0040b97b  66c7442418aa04        mov       word [esp+18h],4AAh
  0040b982  66c744241aff04        mov       word [esp+1Ah],4FFh
  0040b989  66c744241c5405        mov       word [esp+1Ch],554h
  0040b990  66c744241ea905        mov       word [esp+1Eh],5A9h
  0040b997  66c7442420fe05        mov       word [esp+20h],5FEh
  0040b99e  66c74424225306        mov       word [esp+22h],653h
  0040b9a5  66c7442424a806        mov       word [esp+24h],6A8h
  0040b9ac  66c7442426fd06        mov       word [esp+26h],6FDh
  0040b9b3  66c74424285207        mov       word [esp+28h],752h
  0040b9ba  66c744242aa707        mov       word [esp+2Ah],7A7h
  0040b9c1  66c744242c0008        mov       word [esp+2Ch],800h
  0040b9c8  e883f50000            call      0041AF50h
  0040b9cd  e86e2c0000            call      0040E640h
  0040b9d2  8bb424d4000000        mov       esi,[esp+0D4h]
  0040b9d9  8b6c2430              mov       ebp,[esp+30h]
  0040b9dd  8d7c2434              lea       edi,[esp+34h]
  0040b9e1  b8dbca5e00            mov       eax,5ECADBh
  0040b9e6  ba20000000            mov       edx,20h

LAB_0040b9eb:
  0040b9eb  8078d102              cmp       byte [eax-2Fh],2
  0040b9ef  7548                  jne       short 0040BA39h
  0040b9f1  8b58c5                mov       ebx,[eax-3Bh]
  0040b9f4  8d48c5                lea       ecx,[eax-3Bh]
  0040b9f7  f7c300000080          test      ebx,80000000h
  0040b9fd  743a                  je        short 0040BA39h
  0040b9ff  8b58e1                mov       ebx,[eax-1Fh]
  0040ba02  8bf9                  mov       edi,ecx
  0040ba04  895e1c                mov       [esi+1Ch],ebx
  0040ba07  8b58e5                mov       ebx,[eax-1Bh]
  0040ba0a  895e20                mov       [esi+20h],ebx
  0040ba0d  8b58e9                mov       ebx,[eax-17h]
  0040ba10  895e24                mov       [esi+24h],ebx
  0040ba13  33db                  xor       ebx,ebx
  0040ba15  8a58c9                mov       bl,[eax-37h]
  0040ba18  66c740dd0200          mov       word [eax-23h],2
  0040ba1e  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  0040ba25  8b6b08                mov       ebp,[ebx+8]
  0040ba28  8a18                  mov       bl,[eax]
  0040ba2a  80e3fe                and       bl,0FEh
  0040ba2d  80cb02                or        bl,2
  0040ba30  8818                  mov       [eax],bl
  0040ba32  8b19                  mov       ebx,[ecx]
  0040ba34  80cf10                or        bh,10h
  0040ba37  8919                  mov       [ecx],ebx

LAB_0040ba39:
  0040ba39  059c000000            add       eax,9Ch
  0040ba3e  4a                    dec       edx
  0040ba3f  75aa                  jne       short 0040B9EBh
  0040ba41  33c9                  xor       ecx,ecx
  0040ba43  bb01000000            mov       ebx,1
  0040ba48  66394e10              cmp       [esi+10h],cx
  0040ba4c  0f85c1000000          jne       near 0040BB13h
  0040ba52  66895e10              mov       [esi+10h],bx
  0040ba56  66894e12              mov       [esi+12h],cx
  0040ba5a  66894e14              mov       [esi+14h],cx
  0040ba5e  66894e16              mov       [esi+16h],cx
  0040ba62  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  0040ba69  894650                mov       [esi+50h],eax
  0040ba6c  6800100000            push      1000h
  0040ba71  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  0040ba78  895654                mov       [esi+54h],edx
  0040ba7b  6800100000            push      1000h
  0040ba80  0fbf05bc925e00        movsx     eax,word [5E92BCh]
  0040ba87  894658                mov       [esi+58h],eax
  0040ba8a  8a5709                mov       dl,[edi+9]
  0040ba8d  686a010000            push      16Ah
  0040ba92  66c70560915e00fa00    mov       word [5E9160h],0FAh
  0040ba9b  66c70562915e00e600    mov       word [5E9162h],0E6h
  0040baa4  66c70564915e00d200    mov       word [5E9164h],0D2h
  0040baad  66c70566915e000200    mov       word [5E9166h],2
  0040bab6  66894f1a              mov       [edi+1Ah],cx
  0040baba  884f0a                mov       [edi+0Ah],cl
  0040babd  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0040bac4  66895f36              mov       [edi+36h],bx
  0040bac8  8815e8135700          mov       [5713E8h],dl
  0040bace  6a10                  push      10h
  0040bad0  66898e98000000        mov       [esi+98h],cx
  0040bad7  e8c4131300            call      0053CEA0h
  0040badc  83c410                add       esp,10h
  0040badf  6800100000            push      1000h
  0040bae4  6800100000            push      1000h
  0040bae9  6894010000            push      194h
  0040baee  6a10                  push      10h
  0040baf0  e8ab131300            call      0053CEA0h
  0040baf5  83c410                add       esp,10h
  0040baf8  6800100000            push      1000h
  0040bafd  6800100000            push      1000h
  0040bb02  68bf010000            push      1BFh
  0040bb07  6a10                  push      10h
  0040bb09  e892131300            call      0053CEA0h
  0040bb0e  83c410                add       esp,10h
  0040bb11  33c9                  xor       ecx,ecx

LAB_0040bb13:
  0040bb13  66ff8698000000        inc       word [esi+98h]
  0040bb1a  6683be980000000a      cmp       word [esi+98h],0Ah
  0040bb22  7551                  jne       short 0040BB75h
  0040bb24  0fbf4614              movsx     eax,word [esi+14h]
  0040bb28  66898e98000000        mov       [esi+98h],cx
  0040bb2f  6800090000            push      900h
  0040bb34  0fbf4c4414            movsx     ecx,word [esp+eax*2+14h]
  0040bb39  6800090000            push      900h
  0040bb3e  51                    push      ecx
  0040bb3f  6a2f                  push      2Fh
  0040bb41  e85a131300            call      0053CEA0h
  0040bb46  83c410                add       esp,10h
  0040bb49  6800050000            push      500h
  0040bb4e  6800050000            push      500h
  0040bb53  6800020000            push      200h
  0040bb58  6a20                  push      20h
  0040bb5a  e841131300            call      0053CEA0h
  0040bb5f  83c410                add       esp,10h
  0040bb62  66ff4614              inc       word [esi+14h]
  0040bb66  66837e140f            cmp       word [esi+14h],0Fh
  0040bb6b  7506                  jne       short 0040BB73h
  0040bb6d  66c746140e00          mov       word [esi+14h],0Eh

LAB_0040bb73:
  0040bb73  33c9                  xor       ecx,ecx

LAB_0040bb75:
  0040bb75  66395e10              cmp       [esi+10h],bx
  0040bb79  0f8582000000          jne       near 0040BC01h
  0040bb7f  66ff4612              inc       word [esi+12h]
  0040bb83  668b4612              mov       ax,[esi+12h]
  0040bb87  84c3                  test      bl,al
  0040bb89  741d                  je        short 0040BBA8h
  0040bb8b  66837e166e            cmp       word [esi+16h],6Eh
  0040bb90  7d16                  jge       short 0040BBA8h
  0040bb92  6aff                  push      0FFFFFFFFh
  0040bb94  51                    push      ecx
  0040bb95  68f9000000            push      0F9h
  0040bb9a  8d561c                lea       edx,[esi+1Ch]
  0040bb9d  51                    push      ecx
  0040bb9e  52                    push      edx
  0040bb9f  56                    push      esi
  0040bba0  e89bf00600            call      0047AC40h
  0040bba5  83c418                add       esp,18h

LAB_0040bba8:
  0040bba8  66837e1664            cmp       word [esi+16h],64h
  0040bbad  7d2d                  jge       short 0040BBDCh
  0040bbaf  e88cb51300            call      00547140h
  0040bbb4  99                    cdq
  0040bbb5  33c2                  xor       eax,edx
  0040bbb7  2bc2                  sub       eax,edx
  0040bbb9  23c3                  and       eax,ebx
  0040bbbb  33c2                  xor       eax,edx
  0040bbbd  2bc2                  sub       eax,edx
  0040bbbf  6685c0                test      ax,ax
  0040bbc2  7518                  jne       short 0040BBDCh
  0040bbc4  6aff                  push      0FFFFFFFFh
  0040bbc6  6a00                  push      0
  0040bbc8  68fa000000            push      0FAh
  0040bbcd  8d461c                lea       eax,[esi+1Ch]
  0040bbd0  6a00                  push      0
  0040bbd2  50                    push      eax
  0040bbd3  56                    push      esi
  0040bbd4  e867f00600            call      0047AC40h
  0040bbd9  83c418                add       esp,18h

LAB_0040bbdc:
  0040bbdc  845e16                test      [esi+16h],bl
  0040bbdf  7520                  jne       short 0040BC01h
  0040bbe1  66ff471a              inc       word [edi+1Ah]
  0040bbe5  668b4f1a              mov       cx,[edi+1Ah]
  0040bbe9  668b4516              mov       ax,[ebp+16h]
  0040bbed  0fbfc9                movsx     ecx,cx
  0040bbf0  8bd0                  mov       edx,eax
  0040bbf2  81e2ffff0000          and       edx,0FFFFh
  0040bbf8  3bca                  cmp       ecx,edx
  0040bbfa  7c05                  jl        short 0040BC01h
  0040bbfc  48                    dec       eax
  0040bbfd  6689471a              mov       [edi+1Ah],ax

LAB_0040bc01:
  0040bc01  66837e1614            cmp       word [esi+16h],14h
  0040bc06  7e0f                  jle       short 0040BC17h
  0040bc08  6afe                  push      0FFFFFFFEh
  0040bc0a  6afe                  push      0FFFFFFFEh
  0040bc0c  6afe                  push      0FFFFFFFEh
  0040bc0e  57                    push      edi
  0040bc0f  e8cc91ffff            call      00404DE0h
  0040bc14  83c410                add       esp,10h

LAB_0040bc17:
  0040bc17  66817e168c00          cmp       word [esi+16h],8Ch
  0040bc1d  7c67                  jl        short 0040BC86h
  0040bc1f  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0040bc25  e8867b0f00            call      005037B0h
  0040bc2a  a1e8135700            mov       eax,[5713E8h]
  0040bc2f  6a06                  push      6
  0040bc31  25ff000000            and       eax,0FFh
  0040bc36  8d0c80                lea       ecx,[eax+eax*4]
  0040bc39  c1e103                shl       ecx,3
  0040bc3c  2bc8                  sub       ecx,eax
  0040bc3e  a1cc975e00            mov       eax,[5E97CCh]
  0040bc43  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0040bc4a  52                    push      edx
  0040bc4b  50                    push      eax
  0040bc4c  e87f811000            call      00513DD0h
  0040bc51  8b07                  mov       eax,[edi]
  0040bc53  83c40c                add       esp,0Ch
  0040bc56  0d00400002            or        eax,2004000h
  0040bc5b  8907                  mov       [edi],eax
  0040bc5d  668b4e50              mov       cx,[esi+50h]
  0040bc61  66890d60915e00        mov       [5E9160h],cx
  0040bc68  668b5654              mov       dx,[esi+54h]
  0040bc6c  66891562915e00        mov       [5E9162h],dx
  0040bc73  668b4658              mov       ax,[esi+58h]
  0040bc77  66a364915e00          mov       [5E9164h],ax
  0040bc7d  66c70566915e000200    mov       word [5E9166h],2

LAB_0040bc86:
  0040bc86  66ff4616              inc       word [esi+16h]
  0040bc8a  5f                    pop       edi
  0040bc8b  5e                    pop       esi
  0040bc8c  5d                    pop       ebp
  0040bc8d  5b                    pop       ebx
  0040bc8e  81c4c0000000          add       esp,0C0h
  0040bc94  c3                    ret
  0040bc95  90                    nop
  0040bc96  90                    nop
  0040bc97  90                    nop
  0040bc98  90                    nop
  0040bc99  90                    nop
  0040bc9a  90                    nop
  0040bc9b  90                    nop
  0040bc9c  90                    nop
  0040bc9d  90                    nop
  0040bc9e  90                    nop
  0040bc9f  90                    nop
  0040bca0  53                    push      ebx
  0040bca1  56                    push      esi
  0040bca2  8b74240c              mov       esi,[esp+0Ch]
  0040bca6  33db                  xor       ebx,ebx
  0040bca8  57                    push      edi
  0040bca9  66395e10              cmp       [esi+10h],bx
  0040bcad  0f85c1                (bad)
  (octet invalide - la plage demandee deborde probablement du code)
