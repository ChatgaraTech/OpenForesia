Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470a00  section .text  offset fichier 0x6fe00  256 octets

  00470a00  51                    push      ecx
  00470a01  53                    push      ebx
  00470a02  55                    push      ebp
  00470a03  56                    push      esi
  00470a04  57                    push      edi
  00470a05  c744241000000000      mov       dword [esp+10h],0
  00470a0d  33ed                  xor       ebp,ebp
  00470a0f  b894955e00            mov       eax,5E9594h
  00470a14  ba04000000            mov       edx,4

LAB_00470a19:
  00470a19  8b08                  mov       ecx,[eax]
  00470a1b  f7c100000080          test      ecx,80000000h
  00470a21  740d                  je        short 00470A30h
  00470a23  f7c100000050          test      ecx,50000000h
  00470a29  7505                  jne       short 00470A30h
  00470a2b  45                    inc       ebp
  00470a2c  89442410              mov       [esp+10h],eax

LAB_00470a30:
  00470a30  2d9c000000            sub       eax,9Ch
  00470a35  4a                    dec       edx
  00470a36  75e1                  jne       short 00470A19h
  00470a38  b9c0935e00            mov       ecx,5E93C0h
  00470a3d  ba04000000            mov       edx,4

LAB_00470a42:
  00470a42  8b01                  mov       eax,[ecx]
  00470a44  a900000080            test      eax,80000000h
  00470a49  7405                  je        short 00470A50h
  00470a4b  2500000050            and       eax,50000000h

LAB_00470a50:
  00470a50  81c19c000000          add       ecx,9Ch
  00470a56  4a                    dec       edx
  00470a57  75e9                  jne       short 00470A42h
  00470a59  33db                  xor       ebx,ebx
  00470a5b  b894955e00            mov       eax,5E9594h
  00470a60  b904000000            mov       ecx,4

LAB_00470a65:
  00470a65  f70000000080          test      dword [eax],80000000h
  00470a6b  7401                  je        short 00470A6Eh
  00470a6d  43                    inc       ebx

LAB_00470a6e:
  00470a6e  2d9c000000            sub       eax,9Ch
  00470a73  49                    dec       ecx
  00470a74  75ef                  jne       short 00470A65h
  00470a76  8b742418              mov       esi,[esp+18h]
  00470a7a  6a00                  push      0
  00470a7c  6a10                  push      10h
  00470a7e  56                    push      esi
  00470a7f  e87c450c00            call      00535000h
  00470a84  83c40c                add       esp,0Ch
  00470a87  8bf8                  mov       edi,eax
  00470a89  6800000040            push      40000000h
  00470a8e  6a10                  push      10h
  00470a90  56                    push      esi
  00470a91  e86a450c00            call      00535000h
  00470a96  0fbfc3                movsx     eax,bx
  00470a99  83c40c                add       esp,0Ch
  00470a9c  3bf8                  cmp       edi,eax
  00470a9e  750f                  jne       short 00470AAFh
  00470aa0  a178195700            mov       eax,[571978h]
  00470aa5  80cc10                or        ah,10h
  00470aa8  a378195700            mov       [571978h],eax
  00470aad  eb39                  jmp       short 00470AE8h

LAB_00470aaf:
  00470aaf  0fbfcd                movsx     ecx,bp
  00470ab2  3bf9                  cmp       edi,ecx
  00470ab4  7532                  jne       short 00470AE8h
  00470ab6  6685ed                test      bp,bp
  00470ab9  742d                  je        short 00470AE8h
  00470abb  66837e1000            cmp       word [esi+10h],0
  00470ac0  7526                  jne       short 00470AE8h
  00470ac2  a1cc975e00            mov       eax,[5E97CCh]
  00470ac7  8b542410              mov       edx,[esp+10h]
  00470acb  6a00                  push      0
  00470acd  68b0335600            push      5633B0h
  00470ad2  8b4870                mov       ecx,[eax+70h]
  00470ad5  52                    push      edx
  00470ad6  8b5104                mov       edx,[ecx+4]
  00470ad9  52                    push      edx
  00470ada  e871380d00            call      00544350h
  00470adf  83c410                add       esp,10h
  00470ae2  66c746100100          mov       word [esi+10h],1

LAB_00470ae8:
  00470ae8  a178195700            mov       eax,[571978h]
  00470aed  f6c420                test      ah,20h
  00470af0  7407                  je        short 00470AF9h
  00470af2  8b06                  mov       eax,[esi]
  00470af4  80cc60                or        ah,60h
  00470af7  8906                  mov       [esi],eax

LAB_00470af9:
  00470af9  5f                    pop       edi
  00470afa  5e                    pop       esi
  00470afb  5d                    pop       ebp
  00470afc  5b                    pop       ebx
  00470afd  59                    pop       ecx
  00470afe  c3                    ret
  00470aff  90                    nop

9 cible(s) de saut interne : 0x00470a19, 0x00470a30, 0x00470a42, 0x00470a50, 0x00470a65, 0x00470a6e, 0x00470aaf, 0x00470ae8, 0x00470af9
