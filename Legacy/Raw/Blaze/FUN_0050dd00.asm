C:\Program Files (x86)\dotnet\sdk\10.0.401\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(325,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0050dd00  section .text  offset fichier 0x10d100  400 octets

  0050dd00  53                    push      ebx
  0050dd01  55                    push      ebp
  0050dd02  8b6c2414              mov       ebp,[esp+14h]
  0050dd06  56                    push      esi
  0050dd07  57                    push      edi
  0050dd08  668b5500              mov       dx,[ebp]
  0050dd0c  8bc2                  mov       eax,edx
  0050dd0e  c1e804                shr       eax,4
  0050dd11  83e00f                and       eax,0Fh
  0050dd14  0fbeb8c82d5600        movsx     edi,byte [eax+562DC8h]
  0050dd1b  85ff                  test      edi,edi
  0050dd1d  0f843a010000          je        near 0050DE5Dh
  0050dd23  8b742414              mov       esi,[esp+14h]
  0050dd27  8b06                  mov       eax,[esi]
  0050dd29  a947000008            test      eax,8000047h
  0050dd2e  7515                  jne       short 0050DD45h
  0050dd30  b901000000            mov       ecx,1
  0050dd35  66394e18              cmp       [esi+18h],cx
  0050dd39  740a                  je        short 0050DD45h
  0050dd3b  0d00000001            or        eax,1000000h
  0050dd40  894e18                mov       [esi+18h],ecx
  0050dd43  8906                  mov       [esi],eax

LAB_0050dd45:
  0050dd45  8b5c2418              mov       ebx,[esp+18h]
  0050dd49  f6834001000008        test      byte [ebx+140h],8
  0050dd50  7433                  je        short 0050DD85h
  0050dd52  8a4d02                mov       cl,[ebp+2]
  0050dd55  b0f0                  mov       al,0F0h
  0050dd57  84c8                  test      al,cl
  0050dd59  7505                  jne       short 0050DD60h
  0050dd5b  844504                test      [ebp+4],al
  0050dd5e  7446                  je        short 0050DDA6h

LAB_0050dd60:
  0050dd60  e8db930300            call      00547140h
  0050dd65  b980000000            mov       ecx,80h
  0050dd6a  2bc8                  sub       ecx,eax
  0050dd6c  81e1000f0000          and       ecx,0F00h
  0050dd72  c1e709                shl       edi,9
  0050dd75  8d943900feffff        lea       edx,[ecx+edi-200h]
  0050dd7c  66899698000000        mov       [esi+98h],dx
  0050dd83  eb21                  jmp       short 0050DDA6h

LAB_0050dd85:
  0050dd85  66b88000              mov       ax,80h
  0050dd89  662b052a496000        sub       ax,[60492Ah]
  0050dd90  c1e709                shl       edi,9
  0050dd93  25000f0000            and       eax,0F00h
  0050dd98  8d8c0700feffff        lea       ecx,[edi+eax-200h]
  0050dd9f  66898e98000000        mov       [esi+98h],cx

LAB_0050dda6:
  0050dda6  668b8698000000        mov       ax,[esi+98h]
  0050ddad  668b5632              mov       dx,[esi+32h]
  0050ddb1  2bc2                  sub       eax,edx
  0050ddb3  25ff0f0000            and       eax,0FFFh
  0050ddb8  7424                  je        short 0050DDDEh
  0050ddba  33c9                  xor       ecx,ecx
  0050ddbc  3d00080000            cmp       eax,800h
  0050ddc1  0f9dc1                setge     cl
  0050ddc4  49                    dec       ecx
  0050ddc5  81e100020000          and       ecx,200h
  0050ddcb  81c100ffffff          add       ecx,0FFFFFF00h
  0050ddd1  6603ca                add       cx,dx
  0050ddd4  81e1000f0000          and       ecx,0F00h
  0050ddda  66894e32              mov       [esi+32h],cx

LAB_0050ddde:
  0050ddde  a1a8335600            mov       eax,[5633A8h]
  0050dde3  85c0                  test      eax,eax
  0050dde5  7528                  jne       short 0050DE0Fh
  0050dde7  f6450120              test      byte [ebp+1],20h
  0050ddeb  740a                  je        short 0050DDF7h
  0050dded  b800c00300            mov       eax,3C000h
  0050ddf2  89465c                mov       [esi+5Ch],eax
  0050ddf5  eb2e                  jmp       short 0050DE25h

LAB_0050ddf7:
  0050ddf7  8a9350010000          mov       dl,[ebx+150h]
  0050ddfd  83e207                and       edx,7
  0050de00  8b0495c02c5600        mov       eax,[edx*4+562CC0h]
  0050de07  c1e00c                shl       eax,0Ch
  0050de0a  89465c                mov       [esi+5Ch],eax
  0050de0d  eb16                  jmp       short 0050DE25h

LAB_0050de0f:
  0050de0f  8a8350010000          mov       al,[ebx+150h]
  0050de15  83e007                and       eax,7
  0050de18  8b0c85c02c5600        mov       ecx,[eax*4+562CC0h]
  0050de1f  c1e10c                shl       ecx,0Ch
  0050de22  894e5c                mov       [esi+5Ch],ecx

LAB_0050de25:
  0050de25  8b8b40010000          mov       ecx,[ebx+140h]
  0050de2b  b800100000            mov       eax,1000h
  0050de30  f6c540                test      ch,40h
  0050de33  7405                  je        short 0050DE3Ah
  0050de35  b833130000            mov       eax,1333h

LAB_0050de3a:
  0050de3a  f6c580                test      ch,80h
  0050de3d  7405                  je        short 0050DE44h
  0050de3f  b8cc0c0000            mov       eax,0CCCh

LAB_0050de44:
  0050de44  f6c104                test      cl,4
  0050de47  7405                  je        short 0050DE4Eh
  0050de49  b800080000            mov       eax,800h

LAB_0050de4e:
  0050de4e  0faf465c              imul      eax,[esi+5Ch]
  0050de52  c1f80c                sar       eax,0Ch
  0050de55  89465c                mov       [esi+5Ch],eax
  0050de58  5f                    pop       edi
  0050de59  5e                    pop       esi
  0050de5a  5d                    pop       ebp
  0050de5b  5b                    pop       ebx
  0050de5c  c3                    ret

LAB_0050de5d:
  0050de5d  8b4c2414              mov       ecx,[esp+14h]
  0050de61  8b01                  mov       eax,[ecx]
  0050de63  a847                  test      al,47h
  0050de65  751a                  jne       short 0050DE81h
  0050de67  f6c2f0                test      dl,0F0h
  0050de6a  7515                  jne       short 0050DE81h
  0050de6c  6683791800            cmp       word [ecx+18h],0
  0050de71  740e                  je        short 0050DE81h
  0050de73  0d00000001            or        eax,1000000h
  0050de78  c7411800000000        mov       dword [ecx+18h],0
  0050de7f  8901                  mov       [ecx],eax

LAB_0050de81:
  0050de81  5f                    pop       edi
  0050de82  5e                    pop       esi
  0050de83  5d                    pop       ebp
  0050de84  5b                    pop       ebx
  0050de85  c3                    ret
  0050de86  90                    nop
  0050de87  90                    nop
  0050de88  90                    nop
  0050de89  90                    nop
  0050de8a  90                    nop
  0050de8b  90                    nop
  0050de8c  90                    nop
  0050de8d  90                    nop
  0050de8e  90                    nop
  0050de8f  90                    nop

13 cible(s) de saut interne : 0x0050dd45, 0x0050dd60, 0x0050dd85, 0x0050dda6, 0x0050ddde, 0x0050ddf7, 0x0050de0f, 0x0050de25, 0x0050de3a, 0x0050de44, 0x0050de4e, 0x0050de5d, 0x0050de81
