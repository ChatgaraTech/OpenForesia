Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00544810  section .text  offset fichier 0x143c10  2400 octets

  00544810  53                    push      ebx
  00544811  55                    push      ebp
  00544812  56                    push      esi
  00544813  57                    push      edi
  00544814  8b7c2418              mov       edi,[esp+18h]
  00544818  57                    push      edi
  00544819  e892040000            call      00544CB0h
  0054481e  a120835e00            mov       eax,[5E8320h]
  00544823  33db                  xor       ebx,ebx
  00544825  83c404                add       esp,4
  00544828  3bc3                  cmp       eax,ebx
  0054482a  7409                  je        short 00544835h
  0054482c  57                    push      edi
  0054482d  e84ef1ffff            call      00543980h
  00544832  83c404                add       esp,4

LAB_00544835:
  00544835  385f0e                cmp       [edi+0Eh],bl
  00544838  7409                  je        short 00544843h
  0054483a  57                    push      edi
  0054483b  e8d0f1ffff            call      00543A10h
  00544840  83c404                add       esp,4

LAB_00544843:
  00544843  8b442414              mov       eax,[esp+14h]
  00544847  891d20835e00          mov       [5E8320h],ebx
  0054484d  33c9                  xor       ecx,ecx
  0054484f  885f0c                mov       [edi+0Ch],bl
  00544852  8a08                  mov       cl,[eax]
  00544854  33d2                  xor       edx,edx
  00544856  83c003                add       eax,3
  00544859  8a9160905e00          mov       dl,[ecx+5E9060h]
  0054485f  33c9                  xor       ecx,ecx
  00544861  89442414              mov       [esp+14h],eax
  00544865  8b3495c0975e00        mov       esi,[edx*4+5E97C0h]
  0054486c  33d2                  xor       edx,edx
  0054486e  8a50fe                mov       dl,[eax-2]
  00544871  8bb694000000          mov       esi,[esi+94h]
  00544877  8a8a60905e00          mov       cl,[edx+5E9060h]
  0054487d  33d2                  xor       edx,edx
  0054487f  8a50ff                mov       dl,[eax-1]
  00544882  a1484a5600            mov       eax,[564A48h]
  00544887  8be9                  mov       ebp,ecx
  00544889  33c9                  xor       ecx,ecx
  0054488b  8a8a60905e00          mov       cl,[edx+5E9060h]
  00544891  3bc3                  cmp       eax,ebx
  00544893  8b14ae                mov       edx,[esi+ebp*4]
  00544896  8b34ca                mov       esi,[edx+ecx*8]
  00544899  8d0cca                lea       ecx,[edx+ecx*8]
  0054489c  8b5104                mov       edx,[ecx+4]
  0054489f  7403                  je        short 005448A4h
  005448a1  80e6fd                and       dh,0FDh

LAB_005448a4:
  005448a4  8b7f20                mov       edi,[edi+20h]
  005448a7  8bc2                  mov       eax,edx
  005448a9  c1e810                shr       eax,10h
  005448ac  25ff000000            and       eax,0FFh
  005448b1  8bdf                  mov       ebx,edi
  005448b3  c1eb10                shr       ebx,10h
  005448b6  8b0c85008f5e00        mov       ecx,[eax*4+5E8F00h]
  005448bd  a124835e00            mov       eax,[5E8324h]
  005448c2  8be9                  mov       ebp,ecx
  005448c4  83e360                and       ebx,60h
  005448c7  81e5ffff0000          and       ebp,0FFFFh
  005448cd  81e10000ffff          and       ecx,0FFFF0000h
  005448d3  0bdd                  or        ebx,ebp
  005448d5  c74008ffffff65        mov       dword [eax+8],65FFFFFFh
  005448dc  81cb000000e1          or        ebx,0E1000000h
  005448e2  895804                mov       [eax+4],ebx
  005448e5  8bdf                  mov       ebx,edi
  005448e7  c1eb08                shr       ebx,8
  005448ea  81e3ff000000          and       ebx,0FFh
  005448f0  81e7ff000000          and       edi,0FFh
  005448f6  c1e310                shl       ebx,10h
  005448f9  0bdf                  or        ebx,edi
  005448fb  8bfa                  mov       edi,edx
  005448fd  c1ef06                shr       edi,6
  00544900  81e70000c003          and       edi,3C00000h
  00544906  89580c                mov       [eax+0Ch],ebx
  00544909  c1ea08                shr       edx,8
  0054490c  03cf                  add       ecx,edi
  0054490e  81e200000f00          and       edx,0F0000h
  00544914  03ca                  add       ecx,edx
  00544916  8bd6                  mov       edx,esi
  00544918  81e2ffff0000          and       edx,0FFFFh
  0054491e  5f                    pop       edi
  0054491f  0bca                  or        ecx,edx
  00544921  894810                mov       [eax+10h],ecx
  00544924  8bce                  mov       ecx,esi
  00544926  c1e910                shr       ecx,10h
  00544929  c1ee08                shr       esi,8
  0054492c  81e1ff000000          and       ecx,0FFh
  00544932  81e60000ff00          and       esi,0FF0000h
  00544938  0bce                  or        ecx,esi
  0054493a  5e                    pop       esi
  0054493b  894814                mov       [eax+14h],ecx
  0054493e  8b1514835e00          mov       edx,[5E8314h]
  00544944  5d                    pop       ebp
  00544945  5b                    pop       ebx
  00544946  8b0a                  mov       ecx,[edx]
  00544948  81c900000005          or        ecx,5000000h
  0054494e  8908                  mov       [eax],ecx
  00544950  8b1514835e00          mov       edx,[5E8314h]
  00544956  8902                  mov       [edx],eax
  00544958  83c018                add       eax,18h
  0054495b  a324835e00            mov       [5E8324h],eax
  00544960  a11c325600            mov       eax,[56321Ch]
  00544965  40                    inc       eax
  00544966  0faf442404            imul      eax,[esp+4]
  0054496b  c3                    ret
  0054496c  90                    nop
  0054496d  90                    nop
  0054496e  90                    nop
  0054496f  90                    nop
  00544970  8b442404              mov       eax,[esp+4]
  00544974  8b542408              mov       edx,[esp+8]
  00544978  33c9                  xor       ecx,ecx
  0054497a  8a6801                mov       ch,[eax+1]
  0054497d  8a08                  mov       cl,[eax]
  0054497f  83c002                add       eax,2
  00544982  66894a10              mov       [edx+10h],cx
  00544986  c3                    ret
  00544987  90                    nop
  00544988  90                    nop
  00544989  90                    nop
  0054498a  90                    nop
  0054498b  90                    nop
  0054498c  90                    nop
  0054498d  90                    nop
  0054498e  90                    nop
  0054498f  90                    nop
  00544990  8b442404              mov       eax,[esp+4]
  00544994  8b542408              mov       edx,[esp+8]
  00544998  33c9                  xor       ecx,ecx
  0054499a  8a6801                mov       ch,[eax+1]
  0054499d  8a08                  mov       cl,[eax]
  0054499f  83c002                add       eax,2
  005449a2  66894a12              mov       [edx+12h],cx
  005449a6  c3                    ret
  005449a7  90                    nop
  005449a8  90                    nop
  005449a9  90                    nop
  005449aa  90                    nop
  005449ab  90                    nop
  005449ac  90                    nop
  005449ad  90                    nop
  005449ae  90                    nop
  005449af  90                    nop
  005449b0  8b442404              mov       eax,[esp+4]
  005449b4  8b542408              mov       edx,[esp+8]
  005449b8  33c9                  xor       ecx,ecx
  005449ba  8a6801                mov       ch,[eax+1]
  005449bd  8a08                  mov       cl,[eax]
  005449bf  83c002                add       eax,2
  005449c2  66894a20              mov       [edx+20h],cx
  005449c6  c3                    ret
  005449c7  90                    nop
  005449c8  90                    nop
  005449c9  90                    nop
  005449ca  90                    nop
  005449cb  90                    nop
  005449cc  90                    nop
  005449cd  90                    nop
  005449ce  90                    nop
  005449cf  90                    nop
  005449d0  8b442404              mov       eax,[esp+4]
  005449d4  8b542408              mov       edx,[esp+8]
  005449d8  660fb608              movzx     cx,byte [eax]
  005449dc  66894a14              mov       [edx+14h],cx
  005449e0  40                    inc       eax
  005449e1  c3                    ret
  005449e2  90                    nop
  005449e3  90                    nop
  005449e4  90                    nop
  005449e5  90                    nop
  005449e6  90                    nop
  005449e7  90                    nop
  005449e8  90                    nop
  005449e9  90                    nop
  005449ea  90                    nop
  005449eb  90                    nop
  005449ec  90                    nop
  005449ed  90                    nop
  005449ee  90                    nop
  005449ef  90                    nop
  005449f0  8b4c2404              mov       ecx,[esp+4]
  005449f4  33d2                  xor       edx,edx
  005449f6  33c0                  xor       eax,eax
  005449f8  53                    push      ebx
  005449f9  8a5101                mov       dl,[ecx+1]
  005449fc  8a01                  mov       al,[ecx]
  005449fe  8bca                  mov       ecx,edx
  00544a00  55                    push      ebp
  00544a01  0560905e00            add       eax,5E9060h
  00544a06  56                    push      esi
  00544a07  83e902                sub       ecx,2
  00544a0a  57                    push      edi
  00544a0b  7411                  je        short 00544A1Eh
  00544a0d  83e902                sub       ecx,2
  00544a10  7408                  je        short 00544A1Ah
  00544a12  33c9                  xor       ecx,ecx
  00544a14  8a08                  mov       cl,[eax]
  00544a16  8bf9                  mov       edi,ecx
  00544a18  eb09                  jmp       short 00544A23h

LAB_00544a1a:
  00544a1a  8b38                  mov       edi,[eax]
  00544a1c  eb05                  jmp       short 00544A23h

LAB_00544a1e:
  00544a1e  33ff                  xor       edi,edi
  00544a20  668b38                mov       di,[eax]

LAB_00544a23:
  00544a23  bee86b5600            mov       esi,566BE8h
  00544a28  bb0a000000            mov       ebx,0Ah
  00544a2d  33ed                  xor       ebp,ebp

LAB_00544a2f:
  00544a2f  8b0e                  mov       ecx,[esi]
  00544a31  8bc7                  mov       eax,edi
  00544a33  33d2                  xor       edx,edx
  00544a35  f7f1                  div       ecx
  00544a37  8bd0                  mov       edx,eax
  00544a39  0fafd1                imul      edx,ecx
  00544a3c  2bfa                  sub       edi,edx
  00544a3e  85c0                  test      eax,eax
  00544a40  7505                  jne       short 00544A47h
  00544a42  83fb01                cmp       ebx,1
  00544a45  7501                  jne       short 00544A48h

LAB_00544a47:
  00544a47  45                    inc       ebp

LAB_00544a48:
  00544a48  85ed                  test      ebp,ebp
  00544a4a  7413                  je        short 00544A5Fh
  00544a4c  83c030                add       eax,30h
  00544a4f  6a00                  push      0
  00544a51  50                    push      eax
  00544a52  8b442420              mov       eax,[esp+20h]
  00544a56  50                    push      eax
  00544a57  e8c4f0ffff            call      00543B20h
  00544a5c  83c40c                add       esp,0Ch

LAB_00544a5f:
  00544a5f  4b                    dec       ebx
  00544a60  83c604                add       esi,4
  00544a63  85db                  test      ebx,ebx
  00544a65  75c8                  jne       short 00544A2Fh
  00544a67  8b4c2414              mov       ecx,[esp+14h]
  00544a6b  5f                    pop       edi
  00544a6c  5e                    pop       esi
  00544a6d  5d                    pop       ebp
  00544a6e  8d4102                lea       eax,[ecx+2]
  00544a71  5b                    pop       ebx
  00544a72  c3                    ret
  00544a73  90                    nop
  00544a74  90                    nop
  00544a75  90                    nop
  00544a76  90                    nop
  00544a77  90                    nop
  00544a78  90                    nop
  00544a79  90                    nop
  00544a7a  90                    nop
  00544a7b  90                    nop
  00544a7c  90                    nop
  00544a7d  90                    nop
  00544a7e  90                    nop
  00544a7f  90                    nop
  00544a80  8b442404              mov       eax,[esp+4]
  00544a84  33c9                  xor       ecx,ecx
  00544a86  33d2                  xor       edx,edx
  00544a88  8a4801                mov       cl,[eax+1]
  00544a8b  8a9160905e00          mov       dl,[ecx+5E9060h]
  00544a91  33c9                  xor       ecx,ecx
  00544a93  8a4802                mov       cl,[eax+2]
  00544a96  03d1                  add       edx,ecx
  00544a98  33c9                  xor       ecx,ecx
  00544a9a  8a08                  mov       cl,[eax]
  00544a9c  8b048dc0975e00        mov       eax,[ecx*4+5E97C0h]
  00544aa3  8b4870                mov       ecx,[eax+70h]
  00544aa6  8b0491                mov       eax,[ecx+edx*4]
  00544aa9  c3                    ret
  00544aaa  90                    nop
  00544aab  90                    nop
  00544aac  90                    nop
  00544aad  90                    nop
  00544aae  90                    nop
  00544aaf  90                    nop
  00544ab0  56                    push      esi
  00544ab1  8b742408              mov       esi,[esp+8]
  00544ab5  33c0                  xor       eax,eax
  00544ab7  8a06                  mov       al,[esi]
  00544ab9  83f803                cmp       eax,3
  00544abc  7738                  ja        short 00544AF6h
  00544abe  ff2485fc4a5400        jmp       dword [eax*4+544AFCh]
  00544ac5  6a00                  push      0
  00544ac7  e83408ffff            call      00535300h
  00544acc  83c404                add       esp,4
  00544acf  8d4601                lea       eax,[esi+1]
  00544ad2  5e                    pop       esi
  00544ad3  c3                    ret
  00544ad4  e82709ffff            call      00535400h
  00544ad9  8d4601                lea       eax,[esi+1]
  00544adc  5e                    pop       esi
  00544add  c3                    ret
  00544ade  a0784b6000            mov       al,[604B78h]
  00544ae3  24fe                  and       al,0FEh
  00544ae5  a2784b6000            mov       [604B78h],al
  00544aea  8d4601                lea       eax,[esi+1]
  00544aed  5e                    pop       esi
  00544aee  c3                    ret
  00544aef  800d784b600001        or        byte [604B78h],1

LAB_00544af6:
  00544af6  8d4601                lea       eax,[esi+1]
  00544af9  5e                    pop       esi
  00544afa  c3                    ret
  00544afb  90                    nop
  00544afc  c54a54                lds       ecx,[edx+54h]
  00544aff  00d4                  add       ah,dl
  00544b01  4a                    dec       edx
  00544b02  54                    push      esp
  00544b03  00de                  add       dh,bl
  00544b05  4a                    dec       edx
  00544b06  54                    push      esp
  00544b07  00ef                  add       bh,ch
  00544b09  4a                    dec       edx
  00544b0a  54                    push      esp
  00544b0b  00909090908b          add       [eax-746F6F70h],dl
  00544b11  4c                    dec       esp
  00544b12  2408                  and       al,8
  00544b14  8a81c7080000          mov       al,[ecx+8C7h]
  00544b1a  c6413b01              mov       byte [ecx+3Bh],1
  00544b1e  0c04                  or        al,4
  00544b20  c681c408000000        mov       byte [ecx+8C4h],0
  00544b27  8881c7080000          mov       [ecx+8C7h],al
  00544b2d  8b442404              mov       eax,[esp+4]
  00544b31  8a10                  mov       dl,[eax]
  00544b33  40                    inc       eax
  00544b34  8891c5080000          mov       [ecx+8C5h],dl
  00544b3a  8981ac080000          mov       [ecx+8ACh],eax
  00544b40  c3                    ret
  00544b41  90                    nop
  00544b42  90                    nop
  00544b43  90                    nop
  00544b44  90                    nop
  00544b45  90                    nop
  00544b46  90                    nop
  00544b47  90                    nop
  00544b48  90                    nop
  00544b49  90                    nop
  00544b4a  90                    nop
  00544b4b  90                    nop
  00544b4c  90                    nop
  00544b4d  90                    nop
  00544b4e  90                    nop
  00544b4f  90                    nop
  00544b50  8b4c2408              mov       ecx,[esp+8]
  00544b54  8b442404              mov       eax,[esp+4]
  00544b58  53                    push      ebx
  00544b59  8a99c7080000          mov       bl,[ecx+8C7h]
  00544b5f  c6413b01              mov       byte [ecx+3Bh],1
  00544b63  80cb04                or        bl,4
  00544b66  c681c408000000        mov       byte [ecx+8C4h],0
  00544b6d  8899c7080000          mov       [ecx+8C7h],bl
  00544b73  8a10                  mov       dl,[eax]
  00544b75  8854240c              mov       [esp+0Ch],dl
  00544b79  80e20f                and       dl,0Fh
  00544b7c  8891c5080000          mov       [ecx+8C5h],dl
  00544b82  8b54240c              mov       edx,[esp+0Ch]
  00544b86  81e2ff000000          and       edx,0FFh
  00544b8c  c1ea04                shr       edx,4
  00544b8f  4a                    dec       edx
  00544b90  7415                  je        short 00544BA7h
  00544b92  4a                    dec       edx
  00544b93  751b                  jne       short 00544BB0h
  00544b95  80cb80                or        bl,80h
  00544b98  40                    inc       eax
  00544b99  8899c7080000          mov       [ecx+8C7h],bl
  00544b9f  8981ac080000          mov       [ecx+8ACh],eax
  00544ba5  5b                    pop       ebx
  00544ba6  c3                    ret

LAB_00544ba7:
  00544ba7  80cb40                or        bl,40h
  00544baa  8899c7080000          mov       [ecx+8C7h],bl

LAB_00544bb0:
  00544bb0  40                    inc       eax
  00544bb1  5b                    pop       ebx
  00544bb2  8981ac080000          mov       [ecx+8ACh],eax
  00544bb8  c3                    ret
  00544bb9  90                    nop
  00544bba  90                    nop
  00544bbb  90                    nop
  00544bbc  90                    nop
  00544bbd  90                    nop
  00544bbe  90                    nop
  00544bbf  90                    nop
  00544bc0  53                    push      ebx
  00544bc1  56                    push      esi
  00544bc2  8b74240c              mov       esi,[esp+0Ch]
  00544bc6  8b15cc975e00          mov       edx,[5E97CCh]
  00544bcc  33c0                  xor       eax,eax
  00544bce  57                    push      edi
  00544bcf  8b7c2414              mov       edi,[esp+14h]
  00544bd3  8a6601                mov       ah,[esi+1]
  00544bd6  8a06                  mov       al,[esi]
  00544bd8  8b8fb0080000          mov       ecx,[edi+8B0h]
  00544bde  8b9fac080000          mov       ebx,[edi+8ACh]
  00544be4  50                    push      eax
  00544be5  51                    push      ecx
  00544be6  52                    push      edx
  00544be7  e8e4f1fcff            call      00513DD0h
  00544bec  8bbfac080000          mov       edi,[edi+8ACh]
  00544bf2  83c40c                add       esp,0Ch
  00544bf5  3bdf                  cmp       ebx,edi
  00544bf7  7509                  jne       short 00544C02h
  00544bf9  83c602                add       esi,2
  00544bfc  8bc6                  mov       eax,esi
  00544bfe  5f                    pop       edi
  00544bff  5e                    pop       esi
  00544c00  5b                    pop       ebx
  00544c01  c3                    ret

LAB_00544c02:
  00544c02  8bf7                  mov       esi,edi
  00544c04  5f                    pop       edi
  00544c05  8bc6                  mov       eax,esi
  00544c07  5e                    pop       esi
  00544c08  5b                    pop       ebx
  00544c09  c3                    ret
  00544c0a  90                    nop
  00544c0b  90                    nop
  00544c0c  90                    nop
  00544c0d  90                    nop
  00544c0e  90                    nop
  00544c0f  90                    nop
  00544c10  8b442408              mov       eax,[esp+8]
  00544c14  8b4c2404              mov       ecx,[esp+4]
  00544c18  53                    push      ebx
  00544c19  33db                  xor       ebx,ebx
  00544c1b  8a5815                mov       bl,[eax+15h]
  00544c1e  8d5103                lea       edx,[ecx+3]
  00544c21  899498b4080000        mov       [eax+ebx*4+8B4h],edx
  00544c28  8a5015                mov       dl,[eax+15h]
  00544c2b  fec2                  inc       dl
  00544c2d  5b                    pop       ebx
  00544c2e  885015                mov       [eax+15h],dl
  00544c31  33c0                  xor       eax,eax
  00544c33  8a6102                mov       ah,[ecx+2]
  00544c36  33d2                  xor       edx,edx
  00544c38  8a11                  mov       dl,[ecx]
  00544c3a  8a4101                mov       al,[ecx+1]
  00544c3d  8b0c95c0975e00        mov       ecx,[edx*4+5E97C0h]
  00544c44  8b5170                mov       edx,[ecx+70h]
  00544c47  8b0482                mov       eax,[edx+eax*4]
  00544c4a  c3                    ret
  00544c4b  90                    nop
  00544c4c  90                    nop
  00544c4d  90                    nop
  00544c4e  90                    nop
  00544c4f  90                    nop
  00544c50  56                    push      esi
  00544c51  8b74240c              mov       esi,[esp+0Ch]
  00544c55  6a10                  push      10h
  00544c57  8d4668                lea       eax,[esi+68h]
  00544c5a  50                    push      eax
  00544c5b  e8c60f0000            call      00545C26h
  00544c60  33c0                  xor       eax,eax
  00544c62  8d8ea4000000          lea       ecx,[esi+0A4h]
  00544c68  66894610              mov       [esi+10h],ax
  00544c6c  c7460c0000000f        mov       dword [esi+0Ch],0F000000h
  00544c73  890d14835e00          mov       [5E8314h],ecx
  00544c79  8d9628080000          lea       edx,[esi+828h]
  00544c7f  33c9                  xor       ecx,ecx
  00544c81  891524835e00          mov       [5E8324h],edx
  00544c87  8a4e14                mov       cl,[esi+14h]
  00544c8a  a320835e00            mov       [5E8320h],eax
  00544c8f  c1e10b                shl       ecx,0Bh
  00544c92  81c100035e00          add       ecx,5E0300h
  00544c98  a3d0ff5d00            mov       [5DFFD0h],eax
  00544c9d  8b442408              mov       eax,[esp+8]
  00544ca1  890d04835e00          mov       [5E8304h],ecx
  00544ca7  5e                    pop       esi
  00544ca8  c3                    ret
  00544ca9  90                    nop
  00544caa  90                    nop
  00544cab  90                    nop
  00544cac  90                    nop
  00544cad  90                    nop
  00544cae  90                    nop
  00544caf  90                    nop
  00544cb0  51                    push      ecx
  00544cb1  a108835e00            mov       eax,[5E8308h]
  00544cb6  53                    push      ebx
  00544cb7  55                    push      ebp
  00544cb8  56                    push      esi
  00544cb9  83f802                cmp       eax,2
  00544cbc  57                    push      edi
  00544cbd  0f8595000000          jne       near 00544D58h
  00544cc3  8b4c2418              mov       ecx,[esp+18h]
  00544cc7  8b3d20835e00          mov       edi,[5E8320h]
  00544ccd  33c0                  xor       eax,eax
  00544ccf  8a4112                mov       al,[ecx+12h]
  00544cd2  8bd8                  mov       ebx,eax
  00544cd4  c1e303                shl       ebx,3
  00544cd7  2bdf                  sub       ebx,edi
  00544cd9  83eb0a                sub       ebx,0Ah
  00544cdc  d1eb                  shr       ebx,1
  00544cde  6681fb0001            cmp       bx,100h
  00544ce3  7377                  jae       short 00544D5Ch
  00544ce5  a10c835e00            mov       eax,[5E830Ch]
  00544cea  8bf3                  mov       esi,ebx
  00544cec  81e6ffff0000          and       esi,0FFFFh
  00544cf2  bd0f000000            mov       ebp,0Fh
  00544cf7  89742410              mov       [esp+10h],esi
  00544cfb  46                    inc       esi
  00544cfc  d1fe                  sar       esi,1

LAB_00544cfe:
  00544cfe  8b0d20835e00          mov       ecx,[5E8320h]
  00544d04  41                    inc       ecx
  00544d05  d1e9                  shr       ecx,1
  00544d07  51                    push      ecx
  00544d08  50                    push      eax
  00544d09  03c6                  add       eax,esi
  00544d0b  50                    push      eax
  00544d0c  e8ef2c0000            call      00547A00h
  00544d11  8b3d0c835e00          mov       edi,[5E830Ch]
  00544d17  8bce                  mov       ecx,esi
  00544d19  8bd1                  mov       edx,ecx
  00544d1b  83c8ff                or        eax,0FFFFFFFFh
  00544d1e  c1e902                shr       ecx,2
  00544d21  f3ab                  rep stosd
  00544d23  8bca                  mov       ecx,edx
  00544d25  83c40c                add       esp,0Ch
  00544d28  83e103                and       ecx,3
  00544d2b  f3aa                  rep stosb
  00544d2d  a10c835e00            mov       eax,[5E830Ch]
  00544d32  0580000000            add       eax,80h
  00544d37  4d                    dec       ebp
  00544d38  a30c835e00            mov       [5E830Ch],eax
  00544d3d  75bf                  jne       short 00544CFEh
  00544d3f  8b442410              mov       eax,[esp+10h]
  00544d43  8b1520835e00          mov       edx,[5E8320h]
  00544d49  03d0                  add       edx,eax
  00544d4b  8b442418              mov       eax,[esp+18h]
  00544d4f  891520835e00          mov       [5E8320h],edx
  00544d55  00580e                add       [eax+0Eh],bl

LAB_00544d58:
  00544d58  8b4c2418              mov       ecx,[esp+18h]

LAB_00544d5c:
  00544d5c  a118835e00            mov       eax,[5E8318h]
  00544d61  40                    inc       eax
  00544d62  a318835e00            mov       [5E8318h],eax
  00544d67  8d1440                lea       edx,[eax+eax*2]
  00544d6a  8d049506000000        lea       eax,[edx*4+6]
  00544d71  0fbf510a              movsx     edx,word [ecx+0Ah]
  00544d75  3bd0                  cmp       edx,eax
  00544d77  7d0d                  jge       short 00544D86h
  00544d79  f681c708000002        test      byte [ecx+8C7h],2
  00544d80  7404                  je        short 00544D86h
  00544d82  6689410a              mov       [ecx+0Ah],ax

LAB_00544d86:
  00544d86  5f                    pop       edi
  00544d87  5e                    pop       esi
  00544d88  5d                    pop       ebp
  00544d89  5b                    pop       ebx
  00544d8a  59                    pop       ecx
  00544d8b  c3                    ret
  00544d8c  90                    nop
  00544d8d  90                    nop
  00544d8e  90                    nop
  00544d8f  90                    nop
  00544d90  53                    push      ebx
  00544d91  56                    push      esi
  00544d92  8b742410              mov       esi,[esp+10h]
  00544d96  56                    push      esi
  00544d97  e814ffffff            call      00544CB0h
  00544d9c  a120835e00            mov       eax,[5E8320h]
  00544da1  33db                  xor       ebx,ebx
  00544da3  83c404                add       esp,4
  00544da6  3bc3                  cmp       eax,ebx
  00544da8  7409                  je        short 00544DB3h
  00544daa  56                    push      esi
  00544dab  e8d0ebffff            call      00543980h
  00544db0  83c404                add       esp,4

LAB_00544db3:
  00544db3  385e0e                cmp       [esi+0Eh],bl
  00544db6  7409                  je        short 00544DC1h
  00544db8  56                    push      esi
  00544db9  e852ecffff            call      00543A10h
  00544dbe  83c404                add       esp,4

LAB_00544dc1:
  00544dc1  8b460c                mov       eax,[esi+0Ch]
  00544dc4  b929000000            mov       ecx,29h
  00544dc9  0500010000            add       eax,100h
  00544dce  2500ff00ff            and       eax,0FF00FF00h
  00544dd3  89460c                mov       [esi+0Ch],eax
  00544dd6  66894610              mov       [esi+10h],ax
  00544dda  c1e808                shr       eax,8
  00544ddd  25ff000000            and       eax,0FFh
  00544de2  2bc8                  sub       ecx,eax
  00544de4  8d148e                lea       edx,[esi+ecx*4]
  00544de7  8bc8                  mov       ecx,eax
  00544de9  891514835e00          mov       [5E8314h],edx
  00544def  8bd6                  mov       edx,esi
  00544df1  c1e107                shl       ecx,7
  00544df4  2bd1                  sub       edx,ecx
  00544df6  33c9                  xor       ecx,ecx
  00544df8  81c228080000          add       edx,828h
  00544dfe  891524835e00          mov       [5E8324h],edx
  00544e04  8a4e14                mov       cl,[esi+14h]
  00544e07  03c8                  add       ecx,eax
  00544e09  8b44240c              mov       eax,[esp+0Ch]
  00544e0d  c1e10b                shl       ecx,0Bh
  00544e10  81c100035e00          add       ecx,5E0300h
  00544e16  3bc3                  cmp       eax,ebx
  00544e18  890d04835e00          mov       [5E8304h],ecx
  00544e1e  891d20835e00          mov       [5E8320h],ebx
  00544e24  891dfc025e00          mov       [5E02FCh],ebx
  00544e2a  7406                  je        short 00544E32h
  00544e2c  891d08835e00          mov       [5E8308h],ebx

LAB_00544e32:
  00544e32  833d08835e0002        cmp       dword [5E8308h],2
  00544e39  750a                  jne       short 00544E45h
  00544e3b  c70508835e0001000000  mov       dword [5E8308h],1

LAB_00544e45:
  00544e45  891dd0ff5d00          mov       [5DFFD0h],ebx
  00544e4b  5e                    pop       esi
  00544e4c  5b                    pop       ebx
  00544e4d  c3                    ret
  00544e4e  90                    nop
  00544e4f  90                    nop
  00544e50  8b4c2408              mov       ecx,[esp+8]
  00544e54  8a81c7080000          mov       al,[ecx+8C7h]
  00544e5a  c6413301              mov       byte [ecx+33h],1
  00544e5e  0c02                  or        al,2
  00544e60  8881c7080000          mov       [ecx+8C7h],al
  00544e66  8b442404              mov       eax,[esp+4]
  00544e6a  8981ac080000          mov       [ecx+8ACh],eax
  00544e70  c3                    ret
  00544e71  90                    nop
  00544e72  90                    nop
  00544e73  90                    nop
  00544e74  90                    nop
  00544e75  90                    nop
  00544e76  90                    nop
  00544e77  90                    nop
  00544e78  90                    nop
  00544e79  90                    nop
  00544e7a  90                    nop
  00544e7b  90                    nop
  00544e7c  90                    nop
  00544e7d  90                    nop
  00544e7e  90                    nop
  00544e7f  90                    nop
  00544e80  8b442408              mov       eax,[esp+8]
  00544e84  53                    push      ebx
  00544e85  8b88b0080000          mov       ecx,[eax+8B0h]
  00544e8b  85c9                  test      ecx,ecx
  00544e8d  742d                  je        short 00544EBCh
  00544e8f  8b11                  mov       edx,[ecx]
  00544e91  81e20000fc00          and       edx,0FC0000h
  00544e97  81fa00008000          cmp       edx,800000h
  00544e9d  751d                  jne       short 00544EBCh
  00544e9f  33d2                  xor       edx,edx
  00544ea1  8a5108                mov       dl,[ecx+8]
  00544ea4  8b0db4876000          mov       ecx,[6087B4h]
  00544eaa  c1e20d                shl       edx,0Dh
  00544ead  8b09                  mov       ecx,[ecx]
  00544eaf  8a940a53010f00        mov       dl,[edx+ecx+0F0153h]
  00544eb6  8854240c              mov       [esp+0Ch],dl
  00544eba  eb05                  jmp       short 00544EC1h

LAB_00544ebc:
  00544ebc  c644240c00            mov       byte [esp+0Ch],0

LAB_00544ec1:
  00544ec1  8b4c2408              mov       ecx,[esp+8]
  00544ec5  33db                  xor       ebx,ebx
  00544ec7  8a5815                mov       bl,[eax+15h]
  00544eca  8d5103                lea       edx,[ecx+3]
  00544ecd  899498b4080000        mov       [eax+ebx*4+8B4h],edx
  00544ed4  8a5815                mov       bl,[eax+15h]
  00544ed7  8b54240c              mov       edx,[esp+0Ch]
  00544edb  fec3                  inc       bl
  00544edd  885815                mov       [eax+15h],bl
  00544ee0  33c0                  xor       eax,eax
  00544ee2  8a6102                mov       ah,[ecx+2]
  00544ee5  81e2ff000000          and       edx,0FFh
  00544eeb  8a4101                mov       al,[ecx+1]
  00544eee  5b                    pop       ebx
  00544eef  03c2                  add       eax,edx
  00544ef1  33d2                  xor       edx,edx
  00544ef3  8a11                  mov       dl,[ecx]
  00544ef5  8b0c95c0975e00        mov       ecx,[edx*4+5E97C0h]
  00544efc  8b5170                mov       edx,[ecx+70h]
  00544eff  8b0482                mov       eax,[edx+eax*4]
  00544f02  c3                    ret
  00544f03  90                    nop
  00544f04  90                    nop
  00544f05  90                    nop
  00544f06  90                    nop
  00544f07  90                    nop
  00544f08  90                    nop
  00544f09  90                    nop
  00544f0a  90                    nop
  00544f0b  90                    nop
  00544f0c  90                    nop
  00544f0d  90                    nop
  00544f0e  90                    nop
  00544f0f  90                    nop
  00544f10  8b442408              mov       eax,[esp+8]
  00544f14  56                    push      esi
  00544f15  8b88b0080000          mov       ecx,[eax+8B0h]
  00544f1b  85c9                  test      ecx,ecx
  00544f1d  7441                  je        short 00544F60h
  00544f1f  8b11                  mov       edx,[ecx]
  00544f21  81e20000fc00          and       edx,0FC0000h
  00544f27  81fa00008000          cmp       edx,800000h
  00544f2d  7531                  jne       short 00544F60h
  00544f2f  8b742408              mov       esi,[esp+8]
  00544f33  33d2                  xor       edx,edx
  00544f35  8a5015                mov       dl,[eax+15h]
  00544f38  89b490b4080000        mov       [eax+edx*4+8B4h],esi
  00544f3f  8a5015                mov       dl,[eax+15h]
  00544f42  fec2                  inc       dl
  00544f44  885015                mov       [eax+15h],dl
  00544f47  33c0                  xor       eax,eax
  00544f49  8a4108                mov       al,[ecx+8]
  00544f4c  8b0db4876000          mov       ecx,[6087B4h]
  00544f52  c1e00d                shl       eax,0Dh
  00544f55  8b11                  mov       edx,[ecx]
  00544f57  8d841000010f00        lea       eax,[eax+edx+0F0100h]
  00544f5e  5e                    pop       esi
  00544f5f  c3                    ret

LAB_00544f60:
  00544f60  8b442408              mov       eax,[esp+8]
  00544f64  5e                    pop       esi
  00544f65  c3                    ret
  00544f66  90                    nop
  00544f67  90                    nop
  00544f68  90                    nop
  00544f69  90                    nop
  00544f6a  90                    nop
  00544f6b  90                    nop
  00544f6c  90                    nop
  00544f6d  90                    nop
  00544f6e  90                    nop
  00544f6f  90                    nop
  00544f70  8b442404              mov       eax,[esp+4]
  00544f74  8b542408              mov       edx,[esp+8]
  00544f78  8a08                  mov       cl,[eax]
  00544f7a  40                    inc       eax
  00544f7b  884a23                mov       [edx+23h],cl
  00544f7e  c3                    ret
  00544f7f  90                    nop
  00544f80  8b442404              mov       eax,[esp+4]
  00544f84  8b542408              mov       edx,[esp+8]
  00544f88  8a08                  mov       cl,[eax]
  00544f8a  40                    inc       eax
  00544f8b  884a2b                mov       [edx+2Bh],cl
  00544f8e  c3                    ret
  00544f8f  90                    nop
  00544f90  8b442404              mov       eax,[esp+4]
  00544f94  8b542408              mov       edx,[esp+8]
  00544f98  8a08                  mov       cl,[eax]
  00544f9a  40                    inc       eax
  00544f9b  884a43                mov       [edx+43h],cl
  00544f9e  c3                    ret
  00544f9f  90                    nop
  00544fa0  8b442404              mov       eax,[esp+4]
  00544fa4  8b542408              mov       edx,[esp+8]
  00544fa8  8a08                  mov       cl,[eax]
  00544faa  40                    inc       eax
  00544fab  884a1b                mov       [edx+1Bh],cl
  00544fae  c3                    ret
  00544faf  90                    nop
  00544fb0  56                    push      esi
  00544fb1  8b74240c              mov       esi,[esp+0Ch]
  00544fb5  8a460e                mov       al,[esi+0Eh]
  00544fb8  84c0                  test      al,al
  00544fba  743a                  je        short 00544FF6h
  00544fbc  56                    push      esi
  00544fbd  e84eeaffff            call      00543A10h
  00544fc2  8a460e                mov       al,[esi+0Eh]
  00544fc5  8a4e0c                mov       cl,[esi+0Ch]
  00544fc8  02c8                  add       cl,al
  00544fca  c6460e00              mov       byte [esi+0Eh],0
  00544fce  884e0c                mov       [esi+0Ch],cl
  00544fd1  8b0d04835e00          mov       ecx,[5E8304h]
  00544fd7  8b1520835e00          mov       edx,[5E8320h]
  00544fdd  83c404                add       esp,4
  00544fe0  33c0                  xor       eax,eax
  00544fe2  890dfc025e00          mov       [5E02FCh],ecx
  00544fe8  891500835e00          mov       [5E8300h],edx
  00544fee  8a460e                mov       al,[esi+0Eh]
  00544ff1  a328835e00            mov       [5E8328h],eax

LAB_00544ff6:
  00544ff6  8b442408              mov       eax,[esp+8]
  00544ffa  8a08                  mov       cl,[eax]
  00544ffc  40                    inc       eax
  00544ffd  884e22                mov       [esi+22h],cl
  00545000  5e                    pop       esi
  00545001  c3                    ret
  00545002  90                    nop
  00545003  90                    nop
  00545004  90                    nop
  00545005  90                    nop
  00545006  90                    nop
  00545007  90                    nop
  00545008  90                    nop
  00545009  90                    nop
  0054500a  90                    nop
  0054500b  90                    nop
  0054500c  90                    nop
  0054500d  90                    nop
  0054500e  90                    nop
  0054500f  90                    nop
  00545010  8b442404              mov       eax,[esp+4]
  00545014  8b542408              mov       edx,[esp+8]
  00545018  8a08                  mov       cl,[eax]
  0054501a  40                    inc       eax
  0054501b  884a2a                mov       [edx+2Ah],cl
  0054501e  c3                    ret
  0054501f  90                    nop
  00545020  8b442404              mov       eax,[esp+4]
  00545024  8b542408              mov       edx,[esp+8]
  00545028  8a08                  mov       cl,[eax]
  0054502a  40                    inc       eax
  0054502b  884a42                mov       [edx+42h],cl
  0054502e  c3                    ret
  0054502f  90                    nop
  00545030  8b442404              mov       eax,[esp+4]
  00545034  8b542408              mov       edx,[esp+8]
  00545038  8a08                  mov       cl,[eax]
  0054503a  40                    inc       eax
  0054503b  884a1a                mov       [edx+1Ah],cl
  0054503e  c3                    ret
  0054503f  90                    nop
  00545040  8b442404              mov       eax,[esp+4]
  00545044  33c9                  xor       ecx,ecx
  00545046  33d2                  xor       edx,edx
  00545048  8a6803                mov       ch,[eax+3]
  0054504b  8a5001                mov       dl,[eax+1]
  0054504e  8a4802                mov       cl,[eax+2]
  00545051  83c004                add       eax,4
  00545054  c1e108                shl       ecx,8
  00545057  0bca                  or        ecx,edx
  00545059  33d2                  xor       edx,edx
  0054505b  8a50fc                mov       dl,[eax-4]
  0054505e  c1e108                shl       ecx,8
  00545061  0bca                  or        ecx,edx
  00545063  8b542408              mov       edx,[esp+8]
  00545067  894a24                mov       [edx+24h],ecx
  0054506a  c3                    ret
  0054506b  90                    nop
  0054506c  90                    nop
  0054506d  90                    nop
  0054506e  90                    nop
  0054506f  90                    nop
  00545070  8b442404              mov       eax,[esp+4]
  00545074  33c9                  xor       ecx,ecx
  00545076  33d2                  xor       edx,edx
  00545078  8a6803                mov       ch,[eax+3]
  0054507b  8a5001                mov       dl,[eax+1]
  0054507e  8a4802                mov       cl,[eax+2]
  00545081  83c004                add       eax,4
  00545084  c1e108                shl       ecx,8
  00545087  0bca                  or        ecx,edx
  00545089  33d2                  xor       edx,edx
  0054508b  8a50fc                mov       dl,[eax-4]
  0054508e  c1e108                shl       ecx,8
  00545091  0bca                  or        ecx,edx
  00545093  8b542408              mov       edx,[esp+8]
  00545097  894a2c                mov       [edx+2Ch],ecx
  0054509a  c3                    ret
  0054509b  90                    nop
  0054509c  90                    nop
  0054509d  90                    nop
  0054509e  90                    nop
  0054509f  90                    nop
  005450a0  8b442404              mov       eax,[esp+4]
  005450a4  33c9                  xor       ecx,ecx
  005450a6  33d2                  xor       edx,edx
  005450a8  8a6803                mov       ch,[eax+3]
  005450ab  8a5001                mov       dl,[eax+1]
  005450ae  8a4802                mov       cl,[eax+2]
  005450b1  83c004                add       eax,4
  005450b4  c1e108                shl       ecx,8
  005450b7  0bca                  or        ecx,edx
  005450b9  33d2                  xor       edx,edx
  005450bb  8a50fc                mov       dl,[eax-4]
  005450be  c1e108                shl       ecx,8
  005450c1  0bca                  or        ecx,edx
  005450c3  8b542408              mov       edx,[esp+8]
  005450c7  894a44                mov       [edx+44h],ecx
  005450ca  c3                    ret
  005450cb  90                    nop
  005450cc  90                    nop
  005450cd  90                    nop
  005450ce  90                    nop
  005450cf  90                    nop
  005450d0  8b442404              mov       eax,[esp+4]
  005450d4  33c9                  xor       ecx,ecx
  005450d6  33d2                  xor       edx,edx
  005450d8  8a4802                mov       cl,[eax+2]
  005450db  8a5001                mov       dl,[eax+1]
  005450de  8a6803                mov       ch,[eax+3]
  005450e1  83c004                add       eax,4
  005450e4  c1e108                shl       ecx,8
  005450e7  0bca                  or        ecx,edx
  005450e9  33d2                  xor       edx,edx
  005450eb  8a50fc                mov       dl,[eax-4]
  005450ee  c1e108                shl       ecx,8
  005450f1  0bca                  or        ecx,edx
  005450f3  8b542408              mov       edx,[esp+8]
  005450f7  894a1c                mov       [edx+1Ch],ecx
  005450fa  c3                    ret
  005450fb  90                    nop
  005450fc  90                    nop
  005450fd  90                    nop
  005450fe  90                    nop
  005450ff  90                    nop
  00545100  8b442404              mov       eax,[esp+4]
  00545104  33c9                  xor       ecx,ecx
  00545106  33d2                  xor       edx,edx
  00545108  8a4801                mov       cl,[eax+1]
  0054510b  8a10                  mov       dl,[eax]
  0054510d  c1e110                shl       ecx,10h
  00545110  0bca                  or        ecx,edx
  00545112  8b542408              mov       edx,[esp+8]
  00545116  83c002                add       eax,2
  00545119  894a04                mov       [edx+4],ecx
  0054511c  c3                    ret
  0054511d  90                    nop
  0054511e  90                    nop
  0054511f  90                    nop
  00545120  8b442404              mov       eax,[esp+4]
  00545124  33c9                  xor       ecx,ecx
  00545126  33d2                  xor       edx,edx
  00545128  8a4801                mov       cl,[eax+1]
  0054512b  8a10                  mov       dl,[eax]
  0054512d  c1e110                shl       ecx,10h
  00545130  0bca                  or        ecx,edx
  00545132  8b542408              mov       edx,[esp+8]
  00545136  83c002                add       eax,2
  00545139  894a08                mov       [edx+8],ecx
  0054513c  c3                    ret
  0054513d  90                    nop
  0054513e  90                    nop
  0054513f  90                    nop
  00545140  56                    push      esi
  00545141  8b3510835e00          mov       esi,[5E8310h]
  00545147  85f6                  test      esi,esi
  00545149  7413                  je        short 0054515Eh

LAB_0054514b:
  0054514b  8b4604                mov       eax,[esi+4]
  0054514e  8d4e08                lea       ecx,[esi+8]
  00545151  50                    push      eax
  00545152  51                    push      ecx
  00545153  e8c6090000            call      00545B1Eh
  00545158  8b36                  mov       esi,[esi]
  0054515a  85f6                  test      esi,esi
  0054515c  75ed                  jne       short 0054514Bh

LAB_0054515e:
  0054515e  5e                    pop       esi
  0054515f  c3                    ret
  00545160  8b442414              mov       eax,[esp+14h]
  00545164  53                    push      ebx
  00545165  56                    push      esi
  00545166  57                    push      edi
  00545167  8b7c2418              mov       edi,[esp+18h]
  0054516b  8bd0                  mov       edx,eax
  0054516d  c1ea10                shr       edx,10h

28 cible(s) de saut interne : 0x00544835, 0x00544843, 0x005448a4, 0x00544a1a, 0x00544a1e, 0x00544a23, 0x00544a2f, 0x00544a47, 0x00544a48, 0x00544a5f, 0x00544af6, 0x00544ba7, 0x00544bb0, 0x00544c02, 0x00544cfe, 0x00544d58, 0x00544d5c, 0x00544d86, 0x00544db3, 0x00544dc1, 0x00544e32, 0x00544e45, 0x00544ebc, 0x00544ec1, 0x00544f60, 0x00544ff6, 0x0054514b, 0x0054515e
