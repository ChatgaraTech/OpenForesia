Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470900  section .text  offset fichier 0x6fd00  256 octets

  00470900  8a0d78195700          mov       cl,[571978h]
  00470906  56                    push      esi
  00470907  8b742408              mov       esi,[esp+8]
  0047090b  668b4614              mov       ax,[esi+14h]
  0047090f  663d0100              cmp       ax,1
  00470913  7509                  jne       short 0047091Eh
  00470915  f6c102                test      cl,2
  00470918  0f84da000000          je        near 004709F8h

LAB_0047091e:
  0047091e  663d0200              cmp       ax,2
  00470922  7509                  jne       short 0047092Dh
  00470924  f6c110                test      cl,10h
  00470927  0f84cb000000          je        near 004709F8h

LAB_0047092d:
  0047092d  663d0300              cmp       ax,3
  00470931  7509                  jne       short 0047093Ch
  00470933  f6c180                test      cl,80h
  00470936  0f84bc000000          je        near 004709F8h

LAB_0047093c:
  0047093c  668b4610              mov       ax,[esi+10h]
  00470940  663d0200              cmp       ax,2
  00470944  0f84ae000000          je        near 004709F8h
  0047094a  6685c0                test      ax,ax
  0047094d  7544                  jne       short 00470993h
  0047094f  66894612              mov       [esi+12h],ax
  00470953  668b4634              mov       ax,[esi+34h]
  00470957  66c746100100          mov       word [esi+10h],1
  0047095d  c7465c3c000000        mov       dword [esi+5Ch],3Ch
  00470964  66894616              mov       [esi+16h],ax
  00470968  8b0dcc975e00          mov       ecx,[5E97CCh]
  0047096e  6a00                  push      0
  00470970  6a00                  push      0
  00470972  8b91a8000000          mov       edx,[ecx+0A8h]
  00470978  6800100000            push      1000h
  0047097d  6800040000            push      400h
  00470982  8b02                  mov       eax,[edx]
  00470984  660fb64824            movzx     cx,byte [eax+24h]
  00470989  51                    push      ecx
  0047098a  56                    push      esi
  0047098b  e8f076ffff            call      00468080h
  00470990  83c418                add       esp,18h

LAB_00470993:
  00470993  66837e1001            cmp       word [esi+10h],1
  00470998  755e                  jne       short 004709F8h
  0047099a  668b565c              mov       dx,[esi+5Ch]
  0047099e  0fbf4616              movsx     eax,word [esi+16h]
  004709a2  66295634              sub       [esi+34h],dx
  004709a6  8b565c                mov       edx,[esi+5Ch]
  004709a9  0fbf4e34              movsx     ecx,word [esi+34h]
  004709ad  2dbc020000            sub       eax,2BCh
  004709b2  83c20a                add       edx,0Ah
  004709b5  3bc8                  cmp       ecx,eax
  004709b7  89565c                mov       [esi+5Ch],edx
  004709ba  7f3c                  jg        short 004709F8h
  004709bc  0fbf4614              movsx     eax,word [esi+14h]
  004709c0  48                    dec       eax
  004709c1  66c746100200          mov       word [esi+10h],2
  004709c7  7423                  je        short 004709ECh
  004709c9  48                    dec       eax
  004709ca  7412                  je        short 004709DEh
  004709cc  48                    dec       eax
  004709cd  7529                  jne       short 004709F8h
  004709cf  a178195700            mov       eax,[571978h]
  004709d4  80cc01                or        ah,1
  004709d7  a378195700            mov       [571978h],eax
  004709dc  5e                    pop       esi
  004709dd  c3                    ret

LAB_004709de:
  004709de  a178195700            mov       eax,[571978h]
  004709e3  0c20                  or        al,20h
  004709e5  a378195700            mov       [571978h],eax
  004709ea  5e                    pop       esi
  004709eb  c3                    ret

LAB_004709ec:
  004709ec  a178195700            mov       eax,[571978h]
  004709f1  0c04                  or        al,4
  004709f3  a378195700            mov       [571978h],eax

LAB_004709f8:
  004709f8  5e                    pop       esi
  004709f9  c3                    ret
  004709fa  90                    nop
  004709fb  90                    nop
  004709fc  90                    nop
  004709fd  90                    nop
  004709fe  90                    nop
  004709ff  90                    nop

7 cible(s) de saut interne : 0x0047091e, 0x0047092d, 0x0047093c, 0x00470993, 0x004709de, 0x004709ec, 0x004709f8
