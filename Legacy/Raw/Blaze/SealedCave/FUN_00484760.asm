Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484760  section .text  offset fichier 0x83b60  256 octets

  00484760  83ec0c                sub       esp,0Ch
  00484763  53                    push      ebx
  00484764  56                    push      esi
  00484765  8b742418              mov       esi,[esp+18h]
  00484769  57                    push      edi
  0048476a  66837e1000            cmp       word [esi+10h],0
  0048476f  7419                  je        short 0048478Ah
  00484771  f70600000002          test      dword [esi],2000000h
  00484777  0f84d0000000          je        near 0048484Dh
  0048477d  c70600000000          mov       dword [esi],0
  00484783  5f                    pop       edi
  00484784  5e                    pop       esi
  00484785  5b                    pop       ebx
  00484786  83c40c                add       esp,0Ch
  00484789  c3                    ret

LAB_0048478a:
  0048478a  8b4e44                mov       ecx,[esi+44h]
  0048478d  8b564c                mov       edx,[esi+4Ch]
  00484790  8b7e6c                mov       edi,[esi+6Ch]
  00484793  8d4644                lea       eax,[esi+44h]
  00484796  f7d9                  neg       ecx
  00484798  8908                  mov       [eax],ecx
  0048479a  8d4c240c              lea       ecx,[esp+0Ch]
  0048479e  f7da                  neg       edx
  004847a0  89564c                mov       [esi+4Ch],edx
  004847a3  51                    push      ecx
  004847a4  8d5730                lea       edx,[edi+30h]
  004847a7  50                    push      eax
  004847a8  52                    push      edx
  004847a9  897e74                mov       [esi+74h],edi
  004847ac  897670                mov       [esi+70h],esi
  004847af  c7462800180018        mov       dword [esi+28h],18001800h
  004847b6  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004847bd  e8ce180b00            call      00536090h
  004847c2  8b471c                mov       eax,[edi+1Ch]
  004847c5  8b4c2418              mov       ecx,[esp+18h]
  004847c9  8b54241c              mov       edx,[esp+1Ch]
  004847cd  8d5e1c                lea       ebx,[esi+1Ch]
  004847d0  03c1                  add       eax,ecx
  004847d2  83c40c                add       esp,0Ch
  004847d5  8903                  mov       [ebx],eax
  004847d7  8b4f20                mov       ecx,[edi+20h]
  004847da  8b442414              mov       eax,[esp+14h]
  004847de  03ca                  add       ecx,edx
  004847e0  894e20                mov       [esi+20h],ecx
  004847e3  8b5724                mov       edx,[edi+24h]
  004847e6  03d0                  add       edx,eax
  004847e8  6a28                  push      28h
  004847ea  0fbf4634              movsx     eax,word [esi+34h]
  004847ee  895624                mov       [esi+24h],edx
  004847f1  50                    push      eax
  004847f2  0fbf4f32              movsx     ecx,word [edi+32h]
  004847f6  8d5650                lea       edx,[esi+50h]
  004847f9  51                    push      ecx
  004847fa  52                    push      edx
  004847fb  e8d0e20a00            call      00532AD0h
  00484800  66c7869a0000000030    mov       word [esi+9Ah],3000h
  00484809  c6464200              mov       byte [esi+42h],0
  0048480d  a1cc975e00            mov       eax,[5E97CCh]
  00484812  83c410                add       esp,10h
  00484815  8b88d4000000          mov       ecx,[eax+0D4h]
  0048481b  33c0                  xor       eax,eax
  0048481d  6a03                  push      3
  0048481f  6a00                  push      0
  00484821  8b11                  mov       edx,[ecx]
  00484823  8a4208                mov       al,[edx+8]
  00484826  05c0000000            add       eax,0C0h
  0048482b  50                    push      eax
  0048482c  68c81b5500            push      551BC8h
  00484831  53                    push      ebx
  00484832  56                    push      esi
  00484833  e80864ffff            call      0047AC40h
  00484838  83c418                add       esp,18h
  0048483b  53                    push      ebx
  0048483c  68c0265500            push      5526C0h
  00484841  e89a930b00            call      0053DBE0h
  00484846  83c408                add       esp,8
  00484849  66ff4610              inc       word [esi+10h]

LAB_0048484d:
  0048484d  5f                    pop       edi
  0048484e  5e                    pop       esi
  0048484f  5b                    pop       ebx
  00484850  83c40c                add       esp,0Ch
  00484853  c3                    ret
  00484854  90                    nop
  00484855  90                    nop
  00484856  90                    nop
  00484857  90                    nop
  00484858  90                    nop
  00484859  90                    nop
  0048485a  90                    nop
  0048485b  90                    nop
  0048485c  90                    nop
  0048485d  90                    nop
  0048485e  90                    nop
  0048485f  90                    nop

2 cible(s) de saut interne : 0x0048478a, 0x0048484d
