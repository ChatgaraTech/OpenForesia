Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004454e0  section .text  offset fichier 0x448e0  304 octets

  004454e0  83ec1c                sub       esp,1Ch
  004454e3  53                    push      ebx
  004454e4  55                    push      ebp
  004454e5  56                    push      esi
  004454e6  8b74242c              mov       esi,[esp+2Ch]
  004454ea  57                    push      edi
  004454eb  8b06                  mov       eax,[esi]
  004454ed  a900008000            test      eax,800000h
  004454f2  89442410              mov       [esp+10h],eax
  004454f6  0f84fc000000          je        near 004455F8h
  004454fc  8b467c                mov       eax,[esi+7Ch]
  004454ff  8b4e60                mov       ecx,[esi+60h]
  00445502  8b5664                mov       edx,[esi+64h]
  00445505  89442430              mov       [esp+30h],eax
  00445509  8b8680000000          mov       eax,[esi+80h]
  0044550f  668bbe90000000        mov       di,[esi+90h]
  00445516  8b5e1c                mov       ebx,[esi+1Ch]
  00445519  8b6e24                mov       ebp,[esi+24h]
  0044551c  8944241c              mov       [esp+1Ch],eax
  00445520  8b868c000000          mov       eax,[esi+8Ch]
  00445526  89442428              mov       [esp+28h],eax
  0044552a  668b442434            mov       ax,[esp+34h]
  0044552f  894c2414              mov       [esp+14h],ecx
  00445533  8b8e84000000          mov       ecx,[esi+84h]
  00445539  89542418              mov       [esp+18h],edx
  0044553d  8b9688000000          mov       edx,[esi+88h]
  00445543  6685c0                test      ax,ax
  00445546  894c2420              mov       [esp+20h],ecx
  0044554a  89542424              mov       [esp+24h],edx
  0044554e  7509                  jne       short 00445559h
  00445550  8d8b00800100          lea       ecx,[ebx+18000h]
  00445556  894e1c                mov       [esi+1Ch],ecx

LAB_00445559:
  00445559  663d0100              cmp       ax,1
  0044555d  7509                  jne       short 00445568h
  0044555f  8d9500800100          lea       edx,[ebp+18000h]
  00445565  895624                mov       [esi+24h],edx

LAB_00445568:
  00445568  663d0200              cmp       ax,2
  0044556c  b90080feff            mov       ecx,0FFFE8000h
  00445571  7503                  jne       short 00445576h
  00445573  014e1c                add       [esi+1Ch],ecx

LAB_00445576:
  00445576  663d0300              cmp       ax,3
  0044557a  7503                  jne       short 0044557Fh
  0044557c  014e24                add       [esi+24h],ecx

LAB_0044557f:
  0044557f  56                    push      esi
  00445580  e8bbe30e00            call      00533940h
  00445585  8b8690000000          mov       eax,[esi+90h]
  0044558b  83c404                add       esp,4
  0044558e  f6c488                test      ah,88h
  00445591  7465                  je        short 004455F8h
  00445593  8b442430              mov       eax,[esp+30h]
  00445597  8b542414              mov       edx,[esp+14h]
  0044559b  8b4c2410              mov       ecx,[esp+10h]
  0044559f  89467c                mov       [esi+7Ch],eax
  004455a2  8b442418              mov       eax,[esp+18h]
  004455a6  895660                mov       [esi+60h],edx
  004455a9  8b542420              mov       edx,[esp+20h]
  004455ad  894664                mov       [esi+64h],eax
  004455b0  8b442424              mov       eax,[esp+24h]
  004455b4  890e                  mov       [esi],ecx
  004455b6  8b4c241c              mov       ecx,[esp+1Ch]
  004455ba  899684000000          mov       [esi+84h],edx
  004455c0  8b5644                mov       edx,[esi+44h]
  004455c3  898688000000          mov       [esi+88h],eax
  004455c9  8b464c                mov       eax,[esi+4Ch]
  004455cc  81e7ffff0000          and       edi,0FFFFh
  004455d2  898e80000000          mov       [esi+80h],ecx
  004455d8  8b4c2428              mov       ecx,[esp+28h]
  004455dc  f7da                  neg       edx
  004455de  f7d8                  neg       eax
  004455e0  89be90000000          mov       [esi+90h],edi
  004455e6  895e1c                mov       [esi+1Ch],ebx
  004455e9  896e24                mov       [esi+24h],ebp
  004455ec  898e8c000000          mov       [esi+8Ch],ecx
  004455f2  895644                mov       [esi+44h],edx
  004455f5  89464c                mov       [esi+4Ch],eax

LAB_004455f8:
  004455f8  5f                    pop       edi
  004455f9  5e                    pop       esi
  004455fa  5d                    pop       ebp
  004455fb  33c0                  xor       eax,eax
  004455fd  5b                    pop       ebx
  004455fe  83c41c                add       esp,1Ch
  00445601  c3                    ret
  00445602  90                    nop
  00445603  90                    nop
  00445604  90                    nop
  00445605  90                    nop
  00445606  90                    nop
  00445607  90                    nop
  00445608  90                    nop
  00445609  90                    nop
  0044560a  90                    nop
  0044560b  90                    nop
  0044560c  90                    nop
  0044560d  90                    nop
  0044560e  90                    nop
  0044560f  90                    nop

5 cible(s) de saut interne : 0x00445559, 0x00445568, 0x00445576, 0x0044557f, 0x004455f8
