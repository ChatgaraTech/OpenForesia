Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046bf80  section .text  offset fichier 0x6b380  160 octets

  0046bf80  56                    push      esi
  0046bf81  8b742408              mov       esi,[esp+8]
  0046bf85  b801000000            mov       eax,1
  0046bf8a  66837e1000            cmp       word [esi+10h],0
  0046bf8f  7522                  jne       short 0046BFB3h
  0046bf91  8b4e1c                mov       ecx,[esi+1Ch]
  0046bf94  8b5620                mov       edx,[esi+20h]
  0046bf97  c1f90c                sar       ecx,0Ch
  0046bf9a  66894e12              mov       [esi+12h],cx
  0046bf9e  8b4e24                mov       ecx,[esi+24h]
  0046bfa1  c1fa0c                sar       edx,0Ch
  0046bfa4  c1f90c                sar       ecx,0Ch
  0046bfa7  66894610              mov       [esi+10h],ax
  0046bfab  66895614              mov       [esi+14h],dx
  0046bfaf  66894e16              mov       [esi+16h],cx

LAB_0046bfb3:
  0046bfb3  66394610              cmp       [esi+10h],ax
  0046bfb7  7557                  jne       short 0046C010h
  0046bfb9  0fbf5632              movsx     edx,word [esi+32h]
  0046bfbd  52                    push      edx
  0046bfbe  e8599a0d00            call      00545A1Ch
  0046bfc3  0fbf4e12              movsx     ecx,word [esi+12h]
  0046bfc7  8d0480                lea       eax,[eax+eax*4]
  0046bfca  c1e10c                shl       ecx,0Ch
  0046bfcd  8d0480                lea       eax,[eax+eax*4]
  0046bfd0  8d04c0                lea       eax,[eax+eax*8]
  0046bfd3  8d1441                lea       edx,[ecx+eax*2]
  0046bfd6  0fbf4632              movsx     eax,word [esi+32h]
  0046bfda  50                    push      eax
  0046bfdb  89561c                mov       [esi+1Ch],edx
  0046bfde  e8339a0d00            call      00545A16h
  0046bfe3  0fbf5616              movsx     edx,word [esi+16h]
  0046bfe7  8d0480                lea       eax,[eax+eax*4]
  0046bfea  66834632f1            add       word [esi+32h],0FFF1h
  0046bfef  c1e20c                shl       edx,0Ch
  0046bff2  8d0480                lea       eax,[eax+eax*4]
  0046bff5  8d0cc0                lea       ecx,[eax+eax*8]
  0046bff8  8d044a                lea       eax,[edx+ecx*2]
  0046bffb  894624                mov       [esi+24h],eax
  0046bffe  668b4632              mov       ax,[esi+32h]
  0046c002  6685c0                test      ax,ax
  0046c005  7d09                  jge       short 0046C010h
  0046c007  0500100000            add       eax,1000h
  0046c00c  66894632              mov       [esi+32h],ax

LAB_0046c010:
  0046c010  5e                    pop       esi
  0046c011  c3                    ret
  0046c012  90                    nop
  0046c013  90                    nop
  0046c014  90                    nop
  0046c015  90                    nop
  0046c016  90                    nop
  0046c017  90                    nop
  0046c018  90                    nop
  0046c019  90                    nop
  0046c01a  90                    nop
  0046c01b  90                    nop
  0046c01c  90                    nop
  0046c01d  90                    nop
  0046c01e  90                    nop
  0046c01f  90                    nop

2 cible(s) de saut interne : 0x0046bfb3, 0x0046c010
