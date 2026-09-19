Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043aa90  section .text  offset fichier 0x39e90  352 octets

  0043aa90  56                    push      esi
  0043aa91  8b742408              mov       esi,[esp+8]
  0043aa95  66837e1000            cmp       word [esi+10h],0
  0043aa9a  0f85f9000000          jne       near 0043AB99h
  0043aaa0  66c746100100          mov       word [esi+10h],1
  0043aaa6  66c746160000          mov       word [esi+16h],0
  0043aaac  e88fc61000            call      00547140h
  0043aab1  99                    cdq
  0043aab2  b978050000            mov       ecx,578h
  0043aab7  f7f9                  idiv      ecx
  0043aab9  a168e05d00            mov       eax,[5DE068h]
  0043aabe  81eabc020000          sub       edx,2BCh
  0043aac4  c1e20c                shl       edx,0Ch
  0043aac7  03d0                  add       edx,eax
  0043aac9  89561c                mov       [esi+1Ch],edx
  0043aacc  e86fc61000            call      00547140h
  0043aad1  99                    cdq
  0043aad2  b978050000            mov       ecx,578h
  0043aad7  6800800000            push      8000h
  0043aadc  f7f9                  idiv      ecx
  0043aade  8b0d70e05d00          mov       ecx,[5DE070h]
  0043aae4  b800a00000            mov       eax,0A000h
  0043aae9  56                    push      esi
  0043aaea  81eabc020000          sub       edx,2BCh
  0043aaf0  c1e20c                shl       edx,0Ch
  0043aaf3  03d1                  add       edx,ecx
  0043aaf5  895624                mov       [esi+24h],edx
  0043aaf8  8b156ce05d00          mov       edx,[5DE06Ch]
  0043aafe  81c200007d00          add       edx,7D0000h
  0043ab04  c7466400c095ff        mov       dword [esi+64h],0FF95C000h
  0043ab0b  895620                mov       [esi+20h],edx
  0043ab0e  c7466000c095ff        mov       dword [esi+60h],0FF95C000h
  0043ab15  66898682000000        mov       [esi+82h],ax
  0043ab1c  66898686000000        mov       [esi+86h],ax
  0043ab23  6689868a000000        mov       [esi+8Ah],ax
  0043ab2a  6689868e000000        mov       [esi+8Eh],ax
  0043ab31  a1c07b5f00            mov       eax,[5F7BC0h]
  0043ab36  50                    push      eax
  0043ab37  e894850f00            call      005330D0h
  0043ab3c  83c40c                add       esp,0Ch
  0043ab3f  56                    push      esi
  0043ab40  e8fb8d0f00            call      00533940h
  0043ab45  8b4e64                mov       ecx,[esi+64h]
  0043ab48  83c404                add       esp,4
  0043ab4b  81e100f0ffff          and       ecx,0FFFFF000h
  0043ab51  81f900c095ff          cmp       ecx,0FF95C000h
  0043ab57  7d07                  jge       short 0043AB60h
  0043ab59  c7466400c095ff        mov       dword [esi+64h],0FF95C000h

LAB_0043ab60:
  0043ab60  8b156ce05d00          mov       edx,[5DE06Ch]
  0043ab66  81c200803e00          add       edx,3E8000h
  0043ab6c  895620                mov       [esi+20h],edx
  0043ab6f  e8ccc51000            call      00547140h
  0043ab74  99                    cdq
  0043ab75  33c2                  xor       eax,edx
  0043ab77  2bc2                  sub       eax,edx
  0043ab79  25ff030000            and       eax,3FFh
  0043ab7e  33c2                  xor       eax,edx
  0043ab80  2bc2                  sub       eax,edx
  0043ab82  8d88dc050000          lea       ecx,[eax+5DCh]
  0043ab88  05c4090000            add       eax,9C4h
  0043ab8d  66894e28              mov       [esi+28h],cx
  0043ab91  6689462a              mov       [esi+2Ah],ax
  0043ab95  6689462c              mov       [esi+2Ch],ax

LAB_0043ab99:
  0043ab99  66837e1001            cmp       word [esi+10h],1
  0043ab9e  754c                  jne       short 0043ABECh
  0043aba0  66837e1600            cmp       word [esi+16h],0
  0043aba5  7545                  jne       short 0043ABECh
  0043aba7  6aff                  push      0FFFFFFFFh
  0043aba9  6a00                  push      0
  0043abab  68c3000000            push      0C3h
  0043abb0  56                    push      esi
  0043abb1  66c746143900          mov       word [esi+14h],39h
  0043abb7  e824ff0300            call      0047AAE0h
  0043abbc  83c410                add       esp,10h
  0043abbf  66c746141c00          mov       word [esi+14h],1Ch
  0043abc5  6aff                  push      0FFFFFFFFh
  0043abc7  6a00                  push      0
  0043abc9  68c3000000            push      0C3h
  0043abce  56                    push      esi
  0043abcf  e80cff0300            call      0047AAE0h
  0043abd4  8b06                  mov       eax,[esi]
  0043abd6  83c410                add       esp,10h
  0043abd9  25ffffff7f            and       eax,7FFFFFFFh
  0043abde  66c746100200          mov       word [esi+10h],2
  0043abe4  66c746120000          mov       word [esi+12h],0
  0043abea  8906                  mov       [esi],eax

LAB_0043abec:
  0043abec  5e                    pop       esi
  0043abed  c3                    ret
  0043abee  90                    nop
  0043abef  90                    nop

3 cible(s) de saut interne : 0x0043ab60, 0x0043ab99, 0x0043abec
