; FUN_004747F0 @ 0x004747F0 — l'entree 8 de la table de sauts de la zone 0.
; NON DECOMPILEE PAR GHIDRA : absente de Blaze.c.
; Relevee le 2026-08-21 avec :
;   dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724
; Transcrite dans Screens/State03/WorldMap.cs sous le nom RunArea0Handler8.


Legacy/Binaries/Blaze.exe  base 0x00400000
0x004747f0  section .text  offset fichier 0x73bf0  724 octets

  004747f0  83ec08                sub       esp,8
  004747f3  8a0d41985e00          mov       cl,[5E9841h]
  004747f9  53                    push      ebx
  004747fa  80e1f7                and       cl,0F7h
  004747fd  33db                  xor       ebx,ebx
  004747ff  880d41985e00          mov       [5E9841h],cl
  00474805  8a0d01905e00          mov       cl,[5E9001h]
  0047480b  f6c102                test      cl,2
  0047480e  56                    push      esi
  0047480f  885c2408              mov       [esp+8],bl
  00474813  c644240901            mov       byte [esp+9],1
  00474818  c644240a04            mov       byte [esp+0Ah],4
  0047481d  c644240bef            mov       byte [esp+0Bh],0EFh
  00474822  7518                  jne       short 0047483Ch
  00474824  8d44240c              lea       eax,[esp+0Ch]
  00474828  8d4c2408              lea       ecx,[esp+8]
  0047482c  50                    push      eax
  0047482d  51                    push      ecx
  0047482e  e8cdf70900            call      00514000h
  00474833  8a0d01905e00          mov       cl,[5E9001h]
  00474839  83c408                add       esp,8

LAB_0047483c:
  0047483c  8a1506905e00          mov       dl,[5E9006h]
  00474842  f6c208                test      dl,8
  00474845  7510                  jne       short 00474857h
  00474847  a040985e00            mov       al,[5E9840h]
  0047484c  a801                  test      al,1
  0047484e  7407                  je        short 00474857h
  00474850  24fe                  and       al,0FEh
  00474852  a240985e00            mov       [5E9840h],al

LAB_00474857:
  00474857  803d906c560001        cmp       byte [566C90h],1
  0047485e  750c                  jne       short 0047486Ch
  00474860  881d2ce55d00          mov       [5DE52Ch],bl
  00474866  881d30e55d00          mov       [5DE530h],bl

LAB_0047486c:
  0047486c  381d2ce55d00          cmp       [5DE52Ch],bl
  00474872  0f8546020000          jne       near 00474ABEh
  00474878  381d30e55d00          cmp       [5DE530h],bl
  0047487e  0f853a020000          jne       near 00474ABEh
  00474884  a000905e00            mov       al,[5E9000h]
  00474889  33f6                  xor       esi,esi
  0047488b  a801                  test      al,1
  0047488d  7405                  je        short 00474894h
  0047488f  be01000000            mov       esi,1

LAB_00474894:
  00474894  a802                  test      al,2
  00474896  7401                  je        short 00474899h
  00474898  46                    inc       esi

LAB_00474899:
  00474899  a804                  test      al,4
  0047489b  7401                  je        short 0047489Eh
  0047489d  46                    inc       esi

LAB_0047489e:
  0047489e  a808                  test      al,8
  004748a0  7401                  je        short 004748A3h
  004748a2  46                    inc       esi

LAB_004748a3:
  004748a3  a810                  test      al,10h
  004748a5  7401                  je        short 004748A8h
  004748a7  46                    inc       esi

LAB_004748a8:
  004748a8  a820                  test      al,20h
  004748aa  7401                  je        short 004748ADh
  004748ac  46                    inc       esi

LAB_004748ad:
  004748ad  a840                  test      al,40h
  004748af  7401                  je        short 004748B2h
  004748b1  46                    inc       esi

LAB_004748b2:
  004748b2  a880                  test      al,80h
  004748b4  7401                  je        short 004748B7h
  004748b6  46                    inc       esi

LAB_004748b7:
  004748b7  f6c101                test      cl,1
  004748ba  7401                  je        short 004748BDh
  004748bc  46                    inc       esi

LAB_004748bd:
  004748bd  33c0                  xor       eax,eax
  004748bf  f6c102                test      cl,2
  004748c2  7405                  je        short 004748C9h
  004748c4  b801000000            mov       eax,1

LAB_004748c9:
  004748c9  f6c108                test      cl,8
  004748cc  7515                  jne       short 004748E3h
  004748ce  53                    push      ebx
  004748cf  881d2ce55d00          mov       [5DE52Ch],bl
  004748d5  e8c6d6f8ff            call      00401FA0h
  004748da  83c404                add       esp,4
  004748dd  5e                    pop       esi
  004748de  5b                    pop       ebx
  004748df  83c408                add       esp,8
  004748e2  c3                    ret

LAB_004748e3:
  004748e3  663bc3                cmp       ax,bx
  004748e6  7405                  je        short 004748EDh
  004748e8  f6c180                test      cl,80h
  004748eb  7517                  jne       short 00474904h

LAB_004748ed:
  004748ed  6a08                  push      8
  004748ef  c6052ce55d0008        mov       byte [5DE52Ch],8
  004748f6  e8a5d6f8ff            call      00401FA0h
  004748fb  83c404                add       esp,4
  004748fe  5e                    pop       esi
  004748ff  5b                    pop       ebx
  00474900  83c408                add       esp,8
  00474903  c3                    ret

LAB_00474904:
  00474904  663d0100              cmp       ax,1
  00474908  0f85b0010000          jne       near 00474ABEh
  0047490e  f6c110                test      cl,10h
  00474911  7517                  jne       short 0047492Ah
  00474913  6a10                  push      10h
  00474915  c6052ce55d0010        mov       byte [5DE52Ch],10h
  0047491c  e87fd6f8ff            call      00401FA0h
  00474921  83c404                add       esp,4
  00474924  5e                    pop       esi
  00474925  5b                    pop       ebx
  00474926  83c408                add       esp,8
  00474929  c3                    ret

LAB_0047492a:
  0047492a  663d0100              cmp       ax,1
  0047492e  0f858a010000          jne       near 00474ABEh
  00474934  f6c110                test      cl,10h
  00474937  741c                  je        short 00474955h
  00474939  f6c120                test      cl,20h
  0047493c  7517                  jne       short 00474955h
  0047493e  6a18                  push      18h
  00474940  c6052ce55d0018        mov       byte [5DE52Ch],18h
  00474947  e854d6f8ff            call      00401FA0h
  0047494c  83c404                add       esp,4
  0047494f  5e                    pop       esi
  00474950  5b                    pop       ebx
  00474951  83c408                add       esp,8
  00474954  c3                    ret

LAB_00474955:
  00474955  663d0100              cmp       ax,1
  00474959  0f855f010000          jne       near 00474ABEh
  0047495f  f6c110                test      cl,10h
  00474962  7426                  je        short 0047498Ah
  00474964  f6c120                test      cl,20h
  00474967  7421                  je        short 0047498Ah
  00474969  663bf3                cmp       si,bx
  0047496c  7c1c                  jl        short 0047498Ah
  0047496e  663bf0                cmp       si,ax
  00474971  7f17                  jg        short 0047498Ah
  00474973  6a20                  push      20h
  00474975  c6052ce55d0020        mov       byte [5DE52Ch],20h
  0047497c  e81fd6f8ff            call      00401FA0h
  00474981  83c404                add       esp,4
  00474984  5e                    pop       esi
  00474985  5b                    pop       ebx
  00474986  83c408                add       esp,8
  00474989  c3                    ret

LAB_0047498a:
  0047498a  663d0100              cmp       ax,1
  0047498e  0f852a010000          jne       near 00474ABEh
  00474994  f6c110                test      cl,10h
  00474997  7428                  je        short 004749C1h
  00474999  f6c120                test      cl,20h
  0047499c  7423                  je        short 004749C1h
  0047499e  6683fe02              cmp       si,2
  004749a2  7c1d                  jl        short 004749C1h
  004749a4  6683fe03              cmp       si,3
  004749a8  7f17                  jg        short 004749C1h
  004749aa  6a28                  push      28h
  004749ac  c6052ce55d0028        mov       byte [5DE52Ch],28h
  004749b3  e8e8d5f8ff            call      00401FA0h
  004749b8  83c404                add       esp,4
  004749bb  5e                    pop       esi
  004749bc  5b                    pop       ebx
  004749bd  83c408                add       esp,8
  004749c0  c3                    ret

LAB_004749c1:
  004749c1  663d0100              cmp       ax,1
  004749c5  0f85f3000000          jne       near 00474ABEh
  004749cb  f6c110                test      cl,10h
  004749ce  7428                  je        short 004749F8h
  004749d0  f6c120                test      cl,20h
  004749d3  7423                  je        short 004749F8h
  004749d5  6683fe04              cmp       si,4
  004749d9  7c1d                  jl        short 004749F8h
  004749db  6683fe05              cmp       si,5
  004749df  7f17                  jg        short 004749F8h
  004749e1  6a30                  push      30h
  004749e3  c6052ce55d0030        mov       byte [5DE52Ch],30h
  004749ea  e8b1d5f8ff            call      00401FA0h
  004749ef  83c404                add       esp,4
  004749f2  5e                    pop       esi
  004749f3  5b                    pop       ebx
  004749f4  83c408                add       esp,8
  004749f7  c3                    ret

LAB_004749f8:
  004749f8  663d0100              cmp       ax,1
  004749fc  0f85bc000000          jne       near 00474ABEh
  00474a02  f6c110                test      cl,10h
  00474a05  7428                  je        short 00474A2Fh
  00474a07  f6c120                test      cl,20h
  00474a0a  7423                  je        short 00474A2Fh
  00474a0c  6683fe06              cmp       si,6
  00474a10  7c1d                  jl        short 00474A2Fh
  00474a12  6683fe07              cmp       si,7
  00474a16  7f17                  jg        short 00474A2Fh
  00474a18  6a38                  push      38h
  00474a1a  c6052ce55d0038        mov       byte [5DE52Ch],38h
  00474a21  e87ad5f8ff            call      00401FA0h
  00474a26  83c404                add       esp,4
  00474a29  5e                    pop       esi
  00474a2a  5b                    pop       ebx
  00474a2b  83c408                add       esp,8
  00474a2e  c3                    ret

LAB_00474a2f:
  00474a2f  663d0100              cmp       ax,1
  00474a33  0f8585000000          jne       near 00474ABEh
  00474a39  f6c110                test      cl,10h
  00474a3c  7422                  je        short 00474A60h
  00474a3e  f6c120                test      cl,20h
  00474a41  741d                  je        short 00474A60h
  00474a43  6683fe08              cmp       si,8
  00474a47  7517                  jne       short 00474A60h
  00474a49  6a40                  push      40h
  00474a4b  c6052ce55d0040        mov       byte [5DE52Ch],40h
  00474a52  e849d5f8ff            call      00401FA0h
  00474a57  83c404                add       esp,4
  00474a5a  5e                    pop       esi
  00474a5b  5b                    pop       ebx
  00474a5c  83c408                add       esp,8
  00474a5f  c3                    ret

LAB_00474a60:
  00474a60  663d0100              cmp       ax,1
  00474a64  7558                  jne       short 00474ABEh
  00474a66  f6c110                test      cl,10h
  00474a69  7427                  je        short 00474A92h
  00474a6b  f6c120                test      cl,20h
  00474a6e  7422                  je        short 00474A92h
  00474a70  6683fe09              cmp       si,9
  00474a74  751c                  jne       short 00474A92h
  00474a76  f6c280                test      dl,80h
  00474a79  7517                  jne       short 00474A92h
  00474a7b  6a48                  push      48h
  00474a7d  c6052ce55d0048        mov       byte [5DE52Ch],48h
  00474a84  e817d5f8ff            call      00401FA0h
  00474a89  83c404                add       esp,4
  00474a8c  5e                    pop       esi
  00474a8d  5b                    pop       ebx
  00474a8e  83c408                add       esp,8
  00474a91  c3                    ret

LAB_00474a92:
  00474a92  663d0100              cmp       ax,1
  00474a96  7526                  jne       short 00474ABEh
  00474a98  f6c110                test      cl,10h
  00474a9b  7421                  je        short 00474ABEh
  00474a9d  f6c120                test      cl,20h
  00474aa0  741c                  je        short 00474ABEh
  00474aa2  6683fe09              cmp       si,9
  00474aa6  7516                  jne       short 00474ABEh
  00474aa8  f6c280                test      dl,80h
  00474aab  7411                  je        short 00474ABEh
  00474aad  6a50                  push      50h
  00474aaf  c6052ce55d0050        mov       byte [5DE52Ch],50h
  00474ab6  e8e5d4f8ff            call      00401FA0h
  00474abb  83c404                add       esp,4

LAB_00474abe:
  00474abe  5e                    pop       esi
  00474abf  5b                    pop       ebx
  00474ac0  83c408                add       esp,8
  00474ac3  c3                    ret

25 cible(s) de saut interne : 0x0047483c, 0x00474857, 0x0047486c, 0x00474894, 0x00474899, 0x0047489e, 0x004748a3, 0x004748a8, 0x004748ad, 0x004748b2, 0x004748b7, 0x004748bd, 0x004748c9, 0x004748e3, 0x004748ed, 0x00474904, 0x0047492a, 0x00474955, 0x0047498a, 0x004749c1, 0x004749f8, 0x00474a2f, 0x00474a60, 0x00474a92, 0x00474abe
