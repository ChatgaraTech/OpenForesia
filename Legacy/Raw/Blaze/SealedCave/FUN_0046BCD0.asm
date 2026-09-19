Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046bcd0  section .text  offset fichier 0x6b0d0  320 octets

  0046bcd0  55                    push      ebp
  0046bcd1  56                    push      esi
  0046bcd2  8b74240c              mov       esi,[esp+0Ch]
  0046bcd6  57                    push      edi
  0046bcd7  bd01000000            mov       ebp,1
  0046bcdc  66837e1000            cmp       word [esi+10h],0
  0046bce1  7512                  jne       short 0046BCF5h
  0046bce3  668b4614              mov       ax,[esi+14h]
  0046bce7  66896e10              mov       [esi+10h],bp
  0046bceb  66894612              mov       [esi+12h],ax
  0046bcef  66c746140000          mov       word [esi+14h],0

LAB_0046bcf5:
  0046bcf5  66396e10              cmp       [esi+10h],bp
  0046bcf9  ba02000000            mov       edx,2
  0046bcfe  754c                  jne       short 0046BD4Ch
  0046bd00  668b4e34              mov       cx,[esi+34h]
  0046bd04  6685c9                test      cx,cx
  0046bd07  751a                  jne       short 0046BD23h
  0046bd09  8b4620                mov       eax,[esi+20h]
  0046bd0c  0fbf7e12              movsx     edi,word [esi+12h]
  0046bd10  0500a00000            add       eax,0A000h
  0046bd15  894620                mov       [esi+20h],eax
  0046bd18  c1f80c                sar       eax,0Ch
  0046bd1b  3bc7                  cmp       eax,edi
  0046bd1d  7e04                  jle       short 0046BD23h
  0046bd1f  66895610              mov       [esi+10h],dx

LAB_0046bd23:
  0046bd23  6681f90008            cmp       cx,800h
  0046bd28  7522                  jne       short 0046BD4Ch
  0046bd2a  8b4620                mov       eax,[esi+20h]
  0046bd2d  0fbf4e12              movsx     ecx,word [esi+12h]
  0046bd31  050060ffff            add       eax,0FFFF6000h
  0046bd36  81e926020000          sub       ecx,226h
  0046bd3c  894620                mov       [esi+20h],eax
  0046bd3f  c1f80c                sar       eax,0Ch
  0046bd42  3bc1                  cmp       eax,ecx
  0046bd44  7d06                  jge       short 0046BD4Ch
  0046bd46  66c746100300          mov       word [esi+10h],3

LAB_0046bd4c:
  0046bd4c  66395610              cmp       [esi+10h],dx
  0046bd50  bfc0ffffff            mov       edi,0FFFFFFC0h
  0046bd55  754e                  jne       short 0046BDA5h
  0046bd57  668b4632              mov       ax,[esi+32h]
  0046bd5b  b9009cffff            mov       ecx,0FFFF9C00h
  0046bd60  6685c0                test      ax,ax
  0046bd63  7503                  jne       short 0046BD68h
  0046bd65  014e1c                add       [esi+1Ch],ecx

LAB_0046bd68:
  0046bd68  663d000c              cmp       ax,0C00h
  0046bd6c  7503                  jne       short 0046BD71h
  0046bd6e  014e24                add       [esi+24h],ecx

LAB_0046bd71:
  0046bd71  0fbf5634              movsx     edx,word [esi+34h]
  0046bd75  52                    push      edx
  0046bd76  e8a19c0d00            call      00545A1Ch
  0046bd7b  0fbf4e12              movsx     ecx,word [esi+12h]
  0046bd7f  66017e34              add       [esi+34h],di
  0046bd83  8d0480                lea       eax,[eax+eax*4]
  0046bd86  c1e10c                shl       ecx,0Ch
  0046bd89  8d04c0                lea       eax,[eax+eax*8]
  0046bd8c  d1e0                  shl       eax,1
  0046bd8e  2bc8                  sub       ecx,eax
  0046bd90  66817e3400f8          cmp       word [esi+34h],0F800h
  0046bd96  894e20                mov       [esi+20h],ecx
  0046bd99  7f0a                  jg        short 0046BDA5h
  0046bd9b  66c746340008          mov       word [esi+34h],800h
  0046bda1  66896e10              mov       [esi+10h],bp

LAB_0046bda5:
  0046bda5  66837e1003            cmp       word [esi+10h],3
  0046bdaa  7553                  jne       short 0046BDFFh
  0046bdac  668b4632              mov       ax,[esi+32h]
  0046bdb0  b900640000            mov       ecx,6400h
  0046bdb5  6685c0                test      ax,ax
  0046bdb8  7503                  jne       short 0046BDBDh
  0046bdba  014e1c                add       [esi+1Ch],ecx

LAB_0046bdbd:
  0046bdbd  663d000c              cmp       ax,0C00h
  0046bdc1  7503                  jne       short 0046BDC6h
  0046bdc3  014e24                add       [esi+24h],ecx

LAB_0046bdc6:
  0046bdc6  0fbf5634              movsx     edx,word [esi+34h]
  0046bdca  52                    push      edx
  0046bdcb  e84c9c0d00            call      00545A1Ch
  0046bdd0  0fbf4e12              movsx     ecx,word [esi+12h]
  0046bdd4  66017e34              add       [esi+34h],di
  0046bdd8  8d0480                lea       eax,[eax+eax*4]
  0046bddb  81e926020000          sub       ecx,226h
  0046bde1  8d04c0                lea       eax,[eax+eax*8]
  0046bde4  d1e0                  shl       eax,1
  0046bde6  c1e10c                shl       ecx,0Ch
  0046bde9  2bc8                  sub       ecx,eax
  0046bdeb  66837e3400            cmp       word [esi+34h],0
  0046bdf0  894e20                mov       [esi+20h],ecx
  0046bdf3  7f0a                  jg        short 0046BDFFh
  0046bdf5  66c746340000          mov       word [esi+34h],0
  0046bdfb  66896e10              mov       [esi+10h],bp

LAB_0046bdff:
  0046bdff  5f                    pop       edi
  0046be00  5e                    pop       esi
  0046be01  5d                    pop       ebp
  0046be02  c3                    ret
  0046be03  90                    nop
  0046be04  90                    nop
  0046be05  90                    nop
  0046be06  90                    nop
  0046be07  90                    nop
  0046be08  90                    nop
  0046be09  90                    nop
  0046be0a  90                    nop
  0046be0b  90                    nop
  0046be0c  90                    nop
  0046be0d  90                    nop
  0046be0e  90                    nop
  0046be0f  90                    nop

9 cible(s) de saut interne : 0x0046bcf5, 0x0046bd23, 0x0046bd4c, 0x0046bd68, 0x0046bd71, 0x0046bda5, 0x0046bdbd, 0x0046bdc6, 0x0046bdff
