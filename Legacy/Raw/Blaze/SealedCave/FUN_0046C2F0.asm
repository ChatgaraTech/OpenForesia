Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046c2f0  section .text  offset fichier 0x6b6f0  192 octets

  0046c2f0  56                    push      esi
  0046c2f1  8b742408              mov       esi,[esp+8]
  0046c2f5  66837e1001            cmp       word [esi+10h],1
  0046c2fa  751b                  jne       short 0046C317h
  0046c2fc  0fbf4632              movsx     eax,word [esi+32h]
  0046c300  50                    push      eax
  0046c301  e816970d00            call      00545A1Ch
  0046c306  d1e0                  shl       eax,1
  0046c308  b900c0f9ff            mov       ecx,0FFF9C000h
  0046c30d  2bc8                  sub       ecx,eax
  0046c30f  8b4620                mov       eax,[esi+20h]
  0046c312  03c1                  add       eax,ecx
  0046c314  894620                mov       [esi+20h],eax

LAB_0046c317:
  0046c317  6842a80000            push      0A842h
  0046c31c  56                    push      esi
  0046c31d  e80e98feff            call      00455B30h
  0046c322  6683463244            add       word [esi+32h],44h
  0046c327  668b4632              mov       ax,[esi+32h]
  0046c32b  83c408                add       esp,8
  0046c32e  663d0010              cmp       ax,1000h
  0046c332  7c09                  jl        short 0046C33Dh
  0046c334  0500f0ffff            add       eax,0FFFFF000h
  0046c339  66894632              mov       [esi+32h],ax

LAB_0046c33d:
  0046c33d  0fbf5632              movsx     edx,word [esi+32h]
  0046c341  52                    push      edx
  0046c342  e8d5960d00            call      00545A1Ch
  0046c347  8b5620                mov       edx,[esi+20h]
  0046c34a  56                    push      esi
  0046c34b  d1e0                  shl       eax,1
  0046c34d  0500400600            add       eax,64000h
  0046c352  03d0                  add       edx,eax
  0046c354  895620                mov       [esi+20h],edx
  0046c357  e874890c00            call      00534CD0h
  0046c35c  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046c362  83c404                add       esp,4
  0046c365  0fbf4614              movsx     eax,word [esi+14h]
  0046c369  8b5130                mov       edx,[ecx+30h]
  0046c36c  0fbf4e16              movsx     ecx,word [esi+16h]
  0046c370  8b0482                mov       eax,[edx+eax*4]
  0046c373  8b561c                mov       edx,[esi+1Ch]
  0046c376  c1e105                shl       ecx,5
  0046c379  03c1                  add       eax,ecx
  0046c37b  c1fa0c                sar       edx,0Ch
  0046c37e  66895004              mov       [eax+4],dx
  0046c382  8b4e20                mov       ecx,[esi+20h]
  0046c385  c1f90c                sar       ecx,0Ch
  0046c388  66894806              mov       [eax+6],cx
  0046c38c  8b5624                mov       edx,[esi+24h]
  0046c38f  c1fa0c                sar       edx,0Ch
  0046c392  83c9ff                or        ecx,0FFFFFFFFh
  0046c395  66895008              mov       [eax+8],dx
  0046c399  66894810              mov       [eax+10h],cx
  0046c39d  66894812              mov       [eax+12h],cx
  0046c3a1  66894814              mov       [eax+14h],cx
  0046c3a5  66894816              mov       [eax+16h],cx
  0046c3a9  5e                    pop       esi
  0046c3aa  c3                    ret
  0046c3ab  90                    nop
  0046c3ac  90                    nop
  0046c3ad  90                    nop
  0046c3ae  90                    nop
  0046c3af  90                    nop

2 cible(s) de saut interne : 0x0046c317, 0x0046c33d
