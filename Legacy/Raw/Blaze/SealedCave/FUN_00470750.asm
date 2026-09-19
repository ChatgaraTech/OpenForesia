Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470750  section .text  offset fichier 0x6fb50  272 octets

  00470750  8a0d78195700          mov       cl,[571978h]
  00470756  56                    push      esi
  00470757  8b742408              mov       esi,[esp+8]
  0047075b  668b4614              mov       ax,[esi+14h]
  0047075f  663d0100              cmp       ax,1
  00470763  7509                  jne       short 0047076Eh
  00470765  f6c101                test      cl,1
  00470768  0f84e1000000          je        near 0047084Fh

LAB_0047076e:
  0047076e  663d0200              cmp       ax,2
  00470772  7509                  jne       short 0047077Dh
  00470774  f6c108                test      cl,8
  00470777  0f84d2000000          je        near 0047084Fh

LAB_0047077d:
  0047077d  663d0300              cmp       ax,3
  00470781  7509                  jne       short 0047078Ch
  00470783  f6c140                test      cl,40h
  00470786  0f84c3000000          je        near 0047084Fh

LAB_0047078c:
  0047078c  668b4610              mov       ax,[esi+10h]
  00470790  663d0200              cmp       ax,2
  00470794  0f84b5000000          je        near 0047084Fh
  0047079a  6685c0                test      ax,ax
  0047079d  7544                  jne       short 004707E3h
  0047079f  66894612              mov       [esi+12h],ax
  004707a3  668b4632              mov       ax,[esi+32h]
  004707a7  66c746100100          mov       word [esi+10h],1
  004707ad  c7465c28000000        mov       dword [esi+5Ch],28h
  004707b4  66894616              mov       [esi+16h],ax
  004707b8  8b0dcc975e00          mov       ecx,[5E97CCh]
  004707be  6a00                  push      0
  004707c0  6a00                  push      0
  004707c2  8b91a8000000          mov       edx,[ecx+0A8h]
  004707c8  6800100000            push      1000h
  004707cd  6800040000            push      400h
  004707d2  8b02                  mov       eax,[edx]
  004707d4  660fb64820            movzx     cx,byte [eax+20h]
  004707d9  51                    push      ecx
  004707da  56                    push      esi
  004707db  e8a078ffff            call      00468080h
  004707e0  83c418                add       esp,18h

LAB_004707e3:
  004707e3  66837e1001            cmp       word [esi+10h],1
  004707e8  7565                  jne       short 0047084Fh
  004707ea  668b565c              mov       dx,[esi+5Ch]
  004707ee  0fbf4616              movsx     eax,word [esi+16h]
  004707f2  66015632              add       [esi+32h],dx
  004707f6  8b565c                mov       edx,[esi+5Ch]
  004707f9  0fbf4e32              movsx     ecx,word [esi+32h]
  004707fd  0520030000            add       eax,320h
  00470802  83c20a                add       edx,0Ah
  00470805  3bc8                  cmp       ecx,eax
  00470807  89565c                mov       [esi+5Ch],edx
  0047080a  7c2b                  jl        short 00470837h
  0047080c  0fbf4614              movsx     eax,word [esi+14h]
  00470810  48                    dec       eax
  00470811  7418                  je        short 0047082Bh
  00470813  48                    dec       eax
  00470814  740c                  je        short 00470822h
  00470816  48                    dec       eax
  00470817  751e                  jne       short 00470837h
  00470819  a178195700            mov       eax,[571978h]
  0047081e  0c80                  or        al,80h
  00470820  eb10                  jmp       short 00470832h

LAB_00470822:
  00470822  a178195700            mov       eax,[571978h]
  00470827  0c10                  or        al,10h
  00470829  eb07                  jmp       short 00470832h

LAB_0047082b:
  0047082b  a178195700            mov       eax,[571978h]
  00470830  0c02                  or        al,2

LAB_00470832:
  00470832  a378195700            mov       [571978h],eax

LAB_00470837:
  00470837  0fbf5616              movsx     edx,word [esi+16h]
  0047083b  0fbf4632              movsx     eax,word [esi+32h]
  0047083f  81c240060000          add       edx,640h
  00470845  3bc2                  cmp       eax,edx
  00470847  7c06                  jl        short 0047084Fh
  00470849  66c746100200          mov       word [esi+10h],2

LAB_0047084f:
  0047084f  5e                    pop       esi
  00470850  c3                    ret
  00470851  90                    nop
  00470852  90                    nop
  00470853  90                    nop
  00470854  90                    nop
  00470855  90                    nop
  00470856  90                    nop
  00470857  90                    nop
  00470858  90                    nop
  00470859  90                    nop
  0047085a  90                    nop
  0047085b  90                    nop
  0047085c  90                    nop
  0047085d  90                    nop
  0047085e  90                    nop
  0047085f  90                    nop

9 cible(s) de saut interne : 0x0047076e, 0x0047077d, 0x0047078c, 0x004707e3, 0x00470822, 0x0047082b, 0x00470832, 0x00470837, 0x0047084f
