Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00444620  section .text  offset fichier 0x43a20  77 octets

  00444620  660fb60560905e00      movzx     ax,byte [5E9060h]
  00444628  0fbfc0                movsx     eax,ax
  0044462b  c1e004                shl       eax,4
  0044462e  c60560905e0000        mov       byte [5E9060h],0
  00444635  668b80204a6000        mov       ax,[eax+604A20h]
  0044463c  f6c480                test      ah,80h
  0044463f  7407                  je        short 00444648h
  00444641  c60560905e0001        mov       byte [5E9060h],1

LAB_00444648:
  00444648  f6c410                test      ah,10h
  0044464b  7407                  je        short 00444654h
  0044464d  800d60905e0002        or        byte [5E9060h],2

LAB_00444654:
  00444654  f6c420                test      ah,20h
  00444657  7407                  je        short 00444660h
  00444659  800d60905e0004        or        byte [5E9060h],4

LAB_00444660:
  00444660  f6c440                test      ah,40h
  00444663  7407                  je        short 0044466Ch
  00444665  800d60905e0008        or        byte [5E9060h],8

LAB_0044466c:
  0044466c  c3                    ret

4 cible(s) de saut interne : 0x00444648, 0x00444654, 0x00444660, 0x0044466c
