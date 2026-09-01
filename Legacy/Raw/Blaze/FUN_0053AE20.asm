Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0053ae20  section .text  offset fichier 0x13a220  400 octets

  0053ae20  8b442404              mov       eax,[esp+4]
  0053ae24  56                    push      esi
  0053ae25  8b74240c              mov       esi,[esp+0Ch]
  0053ae29  56                    push      esi
  0053ae2a  50                    push      eax
  0053ae2b  e830faffff            call      0053A860h
  0053ae30  83c408                add       esp,8
  0053ae33  85c0                  test      eax,eax
  0053ae35  7407                  je        short 0053AE3Eh
  0053ae37  b801000000            mov       eax,1
  0053ae3c  5e                    pop       esi
  0053ae3d  c3                    ret

LAB_0053ae3e:
  0053ae3e  8b0db4876000          mov       ecx,[6087B4h]
  0053ae44  33c0                  xor       eax,eax
  0053ae46  8a4608                mov       al,[esi+8]
  0053ae49  5e                    pop       esi
  0053ae4a  8b11                  mov       edx,[ecx]
  0053ae4c  83c078                add       eax,78h
  0053ae4f  c1e00d                shl       eax,0Dh
  0053ae52  03c2                  add       eax,edx
  0053ae54  8b8840010000          mov       ecx,[eax+140h]
  0053ae5a  66c780e00100000000    mov       word [eax+1E0h],0
  0053ae63  83e1fe                and       ecx,0FFFFFFFEh
  0053ae66  898840010000          mov       [eax+140h],ecx
  0053ae6c  33c0                  xor       eax,eax
  0053ae6e  c3                    ret
  0053ae6f  90                    nop
  0053ae70  8b442404              mov       eax,[esp+4]
  0053ae74  56                    push      esi
  0053ae75  8b74240c              mov       esi,[esp+0Ch]
  0053ae79  56                    push      esi
  0053ae7a  50                    push      eax
  0053ae7b  e8e0f9ffff            call      0053A860h
  0053ae80  83c408                add       esp,8
  0053ae83  85c0                  test      eax,eax
  0053ae85  7407                  je        short 0053AE8Eh
  0053ae87  b801000000            mov       eax,1
  0053ae8c  5e                    pop       esi
  0053ae8d  c3                    ret

LAB_0053ae8e:
  0053ae8e  8b0db4876000          mov       ecx,[6087B4h]
  0053ae94  33c0                  xor       eax,eax
  0053ae96  8a4608                mov       al,[esi+8]
  0053ae99  5e                    pop       esi
  0053ae9a  8b11                  mov       edx,[ecx]
  0053ae9c  83c078                add       eax,78h
  0053ae9f  c1e00d                shl       eax,0Dh
  0053aea2  03c2                  add       eax,edx
  0053aea4  8b8840010000          mov       ecx,[eax+140h]
  0053aeaa  66c7808e0100000000    mov       word [eax+18Eh],0
  0053aeb3  83e1fb                and       ecx,0FFFFFFFBh
  0053aeb6  898840010000          mov       [eax+140h],ecx
  0053aebc  33c0                  xor       eax,eax
  0053aebe  c3                    ret
  0053aebf  90                    nop
  0053aec0  8b442404              mov       eax,[esp+4]
  0053aec4  56                    push      esi
  0053aec5  8b74240c              mov       esi,[esp+0Ch]
  0053aec9  56                    push      esi
  0053aeca  50                    push      eax
  0053aecb  e890f9ffff            call      0053A860h
  0053aed0  83c408                add       esp,8
  0053aed3  85c0                  test      eax,eax
  0053aed5  7407                  je        short 0053AEDEh
  0053aed7  b801000000            mov       eax,1
  0053aedc  5e                    pop       esi
  0053aedd  c3                    ret

LAB_0053aede:
  0053aede  8b0db4876000          mov       ecx,[6087B4h]
  0053aee4  33c0                  xor       eax,eax
  0053aee6  8a4608                mov       al,[esi+8]
  0053aee9  5e                    pop       esi
  0053aeea  8b11                  mov       edx,[ecx]
  0053aeec  83c078                add       eax,78h
  0053aeef  c1e00d                shl       eax,0Dh
  0053aef2  03c2                  add       eax,edx
  0053aef4  8b8840010000          mov       ecx,[eax+140h]
  0053aefa  66c7809a0100000000    mov       word [eax+19Ah],0
  0053af03  80e5fe                and       ch,0FEh
  0053af06  898840010000          mov       [eax+140h],ecx
  0053af0c  33c0                  xor       eax,eax
  0053af0e  c3                    ret
  0053af0f  90                    nop
  0053af10  8b442404              mov       eax,[esp+4]
  0053af14  56                    push      esi
  0053af15  8b74240c              mov       esi,[esp+0Ch]
  0053af19  56                    push      esi
  0053af1a  50                    push      eax
  0053af1b  e840f9ffff            call      0053A860h
  0053af20  83c408                add       esp,8
  0053af23  85c0                  test      eax,eax
  0053af25  7407                  je        short 0053AF2Eh
  0053af27  b801000000            mov       eax,1
  0053af2c  5e                    pop       esi
  0053af2d  c3                    ret

LAB_0053af2e:
  0053af2e  8b0db4876000          mov       ecx,[6087B4h]
  0053af34  33c0                  xor       eax,eax
  0053af36  8a4608                mov       al,[esi+8]
  0053af39  5e                    pop       esi
  0053af3a  8b11                  mov       edx,[ecx]
  0053af3c  83c078                add       eax,78h
  0053af3f  c1e00d                shl       eax,0Dh
  0053af42  03c2                  add       eax,edx
  0053af44  8b8840010000          mov       ecx,[eax+140h]
  0053af4a  83e1fd                and       ecx,0FFFFFFFDh
  0053af4d  898840010000          mov       [eax+140h],ecx
  0053af53  33c0                  xor       eax,eax
  0053af55  c3                    ret
  0053af56  90                    nop
  0053af57  90                    nop
  0053af58  90                    nop
  0053af59  90                    nop
  0053af5a  90                    nop
  0053af5b  90                    nop
  0053af5c  90                    nop
  0053af5d  90                    nop
  0053af5e  90                    nop
  0053af5f  90                    nop
  0053af60  8b442404              mov       eax,[esp+4]
  0053af64  56                    push      esi
  0053af65  8b74240c              mov       esi,[esp+0Ch]
  0053af69  56                    push      esi
  0053af6a  50                    push      eax
  0053af6b  e8f0f8ffff            call      0053A860h
  0053af70  83c408                add       esp,8
  0053af73  85c0                  test      eax,eax
  0053af75  7407                  je        short 0053AF7Eh
  0053af77  b801000000            mov       eax,1
  0053af7c  5e                    pop       esi
  0053af7d  c3                    ret

LAB_0053af7e:
  0053af7e  8b0db4876000          mov       ecx,[6087B4h]
  0053af84  33c0                  xor       eax,eax
  0053af86  8a4608                mov       al,[esi+8]
  0053af89  5e                    pop       esi
  0053af8a  8b11                  mov       edx,[ecx]
  0053af8c  83c078                add       eax,78h
  0053af8f  c1e00d                shl       eax,0Dh
  0053af92  03c2                  add       eax,edx
  0053af94  8b8840010000          mov       ecx,[eax+140h]
  0053af9a  83e1ef                and       ecx,0FFFFFFEFh
  0053af9d  898840010000          mov       [eax+140h],ecx
  0053afa3  33c0                  xor       eax,eax
  0053afa5  c3                    ret
  0053afa6  90                    nop
  0053afa7  90                    nop
  0053afa8  90                    nop
  0053afa9  90                    nop
  0053afaa  90                    nop
  0053afab  90                    nop
  0053afac  90                    nop
  0053afad  90                    nop
  0053afae  90                    nop
  0053afaf  90                    nop

5 cible(s) de saut interne : 0x0053ae3e, 0x0053ae8e, 0x0053aede, 0x0053af2e, 0x0053af7e
