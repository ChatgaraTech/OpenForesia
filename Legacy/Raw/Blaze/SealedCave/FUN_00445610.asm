Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445610  section .text  offset fichier 0x44a10  304 octets

  00445610  83ec1c                sub       esp,1Ch
  00445613  53                    push      ebx
  00445614  55                    push      ebp
  00445615  56                    push      esi
  00445616  8b74242c              mov       esi,[esp+2Ch]
  0044561a  57                    push      edi
  0044561b  8b06                  mov       eax,[esi]
  0044561d  a900008000            test      eax,800000h
  00445622  89442410              mov       [esp+10h],eax
  00445626  0f84fc000000          je        near 00445728h
  0044562c  8b467c                mov       eax,[esi+7Ch]
  0044562f  8b4e60                mov       ecx,[esi+60h]
  00445632  8b5664                mov       edx,[esi+64h]
  00445635  89442430              mov       [esp+30h],eax
  00445639  8b8680000000          mov       eax,[esi+80h]
  0044563f  668bbe90000000        mov       di,[esi+90h]
  00445646  8b5e1c                mov       ebx,[esi+1Ch]
  00445649  8b6e24                mov       ebp,[esi+24h]
  0044564c  8944241c              mov       [esp+1Ch],eax
  00445650  8b868c000000          mov       eax,[esi+8Ch]
  00445656  89442428              mov       [esp+28h],eax
  0044565a  668b442434            mov       ax,[esp+34h]
  0044565f  894c2414              mov       [esp+14h],ecx
  00445663  8b8e84000000          mov       ecx,[esi+84h]
  00445669  89542418              mov       [esp+18h],edx
  0044566d  8b9688000000          mov       edx,[esi+88h]
  00445673  6685c0                test      ax,ax
  00445676  894c2420              mov       [esp+20h],ecx
  0044567a  89542424              mov       [esp+24h],edx
  0044567e  7509                  jne       short 00445689h
  00445680  8d8b00800000          lea       ecx,[ebx+8000h]
  00445686  894e1c                mov       [esi+1Ch],ecx

LAB_00445689:
  00445689  663d0100              cmp       ax,1
  0044568d  7509                  jne       short 00445698h
  0044568f  8d9500800000          lea       edx,[ebp+8000h]
  00445695  895624                mov       [esi+24h],edx

LAB_00445698:
  00445698  663d0200              cmp       ax,2
  0044569c  b90080ffff            mov       ecx,0FFFF8000h
  004456a1  7503                  jne       short 004456A6h
  004456a3  014e1c                add       [esi+1Ch],ecx

LAB_004456a6:
  004456a6  663d0300              cmp       ax,3
  004456aa  7503                  jne       short 004456AFh
  004456ac  014e24                add       [esi+24h],ecx

LAB_004456af:
  004456af  56                    push      esi
  004456b0  e88be20e00            call      00533940h
  004456b5  8b8690000000          mov       eax,[esi+90h]
  004456bb  83c404                add       esp,4
  004456be  f6c488                test      ah,88h
  004456c1  7465                  je        short 00445728h
  004456c3  8b442430              mov       eax,[esp+30h]
  004456c7  8b542414              mov       edx,[esp+14h]
  004456cb  8b4c2410              mov       ecx,[esp+10h]
  004456cf  89467c                mov       [esi+7Ch],eax
  004456d2  8b442418              mov       eax,[esp+18h]
  004456d6  895660                mov       [esi+60h],edx
  004456d9  8b542420              mov       edx,[esp+20h]
  004456dd  894664                mov       [esi+64h],eax
  004456e0  8b442424              mov       eax,[esp+24h]
  004456e4  890e                  mov       [esi],ecx
  004456e6  8b4c241c              mov       ecx,[esp+1Ch]
  004456ea  899684000000          mov       [esi+84h],edx
  004456f0  8b5644                mov       edx,[esi+44h]
  004456f3  898688000000          mov       [esi+88h],eax
  004456f9  8b464c                mov       eax,[esi+4Ch]
  004456fc  81e7ffff0000          and       edi,0FFFFh
  00445702  898e80000000          mov       [esi+80h],ecx
  00445708  8b4c2428              mov       ecx,[esp+28h]
  0044570c  f7da                  neg       edx
  0044570e  f7d8                  neg       eax
  00445710  89be90000000          mov       [esi+90h],edi
  00445716  895e1c                mov       [esi+1Ch],ebx
  00445719  896e24                mov       [esi+24h],ebp
  0044571c  898e8c000000          mov       [esi+8Ch],ecx
  00445722  895644                mov       [esi+44h],edx
  00445725  89464c                mov       [esi+4Ch],eax

LAB_00445728:
  00445728  5f                    pop       edi
  00445729  5e                    pop       esi
  0044572a  5d                    pop       ebp
  0044572b  33c0                  xor       eax,eax
  0044572d  5b                    pop       ebx
  0044572e  83c41c                add       esp,1Ch
  00445731  c3                    ret
  00445732  90                    nop
  00445733  90                    nop
  00445734  90                    nop
  00445735  90                    nop
  00445736  90                    nop
  00445737  90                    nop
  00445738  90                    nop
  00445739  90                    nop
  0044573a  90                    nop
  0044573b  90                    nop
  0044573c  90                    nop
  0044573d  90                    nop
  0044573e  90                    nop
  0044573f  90                    nop

5 cible(s) de saut interne : 0x00445689, 0x00445698, 0x004456a6, 0x004456af, 0x00445728
