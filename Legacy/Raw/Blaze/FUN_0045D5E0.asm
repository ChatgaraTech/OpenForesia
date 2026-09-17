Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045d5e0  section .text  offset fichier 0x5c9e0  272 octets

  0045d5e0  8b442404              mov       eax,[esp+4]
  0045d5e4  53                    push      ebx
  0045d5e5  56                    push      esi
  0045d5e6  57                    push      edi
  0045d5e7  8b10                  mov       edx,[eax]
  0045d5e9  33ff                  xor       edi,edi
  0045d5eb  f6c220                test      dl,20h
  0045d5ee  750a                  jne       short 0045D5FAh
  0045d5f0  66397810              cmp       [eax+10h],di
  0045d5f4  0f84eb000000          je        near 0045D6E5h

LAB_0045d5fa:
  0045d5fa  66397810              cmp       [eax+10h],di
  0045d5fe  be01000000            mov       esi,1
  0045d603  750c                  jne       short 0045D611h
  0045d605  66897010              mov       [eax+10h],si
  0045d609  66897812              mov       [eax+12h],di
  0045d60d  66897814              mov       [eax+14h],di

LAB_0045d611:
  0045d611  66397814              cmp       [eax+14h],di
  0045d615  754f                  jne       short 0045D666h
  0045d617  66ff4012              inc       word [eax+12h]
  0045d61b  8b5820                mov       ebx,[eax+20h]
  0045d61e  0fbf4812              movsx     ecx,word [eax+12h]
  0045d622  c1e10c                shl       ecx,0Ch
  0045d625  2bd9                  sub       ebx,ecx
  0045d627  8bcb                  mov       ecx,ebx
  0045d629  895820                mov       [eax+20h],ebx
  0045d62c  81f90040edff          cmp       ecx,0FFED4000h
  0045d632  7d32                  jge       short 0045D666h
  0045d634  6683403240            add       word [eax+32h],40h
  0045d639  b938ffffff            mov       ecx,0FFFFFF38h
  0045d63e  66014828              add       [eax+28h],cx
  0045d642  6601482c              add       [eax+2Ch],cx
  0045d646  668b482c              mov       cx,[eax+2Ch]
  0045d64a  663bcf                cmp       cx,di
  0045d64d  7d17                  jge       short 0045D666h
  0045d64f  66897828              mov       [eax+28h],di
  0045d653  6689782c              mov       [eax+2Ch],di
  0045d657  c7402000403800        mov       dword [eax+20h],384000h
  0045d65e  66897812              mov       [eax+12h],di
  0045d662  66897014              mov       [eax+14h],si

LAB_0045d666:
  0045d666  66397014              cmp       [eax+14h],si
  0045d66a  b902000000            mov       ecx,2
  0045d66f  7513                  jne       short 0045D684h
  0045d671  66ff4012              inc       word [eax+12h]
  0045d675  6683781214            cmp       word [eax+12h],14h
  0045d67a  7e08                  jle       short 0045D684h
  0045d67c  66897812              mov       [eax+12h],di
  0045d680  66894814              mov       [eax+14h],cx

LAB_0045d684:
  0045d684  66394814              cmp       [eax+14h],cx
  0045d688  755b                  jne       short 0045D6E5h
  0045d68a  668b4828              mov       cx,[eax+28h]
  0045d68e  be00100000            mov       esi,1000h
  0045d693  663bce                cmp       cx,si
  0045d696  7d17                  jge       short 0045D6AFh
  0045d698  6681402cc800          add       word [eax+2Ch],0C8h
  0045d69e  81c1c8000000          add       ecx,0C8h
  0045d6a4  66834032c0            add       word [eax+32h],0FFC0h
  0045d6a9  66894828              mov       [eax+28h],cx
  0045d6ad  eb08                  jmp       short 0045D6B7h

LAB_0045d6af:
  0045d6af  66897028              mov       [eax+28h],si
  0045d6b3  6689702c              mov       [eax+2Ch],si

LAB_0045d6b7:
  0045d6b7  8b4820                mov       ecx,[eax+20h]
  0045d6ba  81f900001900          cmp       ecx,190000h
  0045d6c0  7e0d                  jle       short 0045D6CFh
  0045d6c2  81c10060ffff          add       ecx,0FFFF6000h
  0045d6c8  894820                mov       [eax+20h],ecx
  0045d6cb  5f                    pop       edi
  0045d6cc  5e                    pop       esi
  0045d6cd  5b                    pop       ebx
  0045d6ce  c3                    ret

LAB_0045d6cf:
  0045d6cf  80e6df                and       dh,0DFh
  0045d6d2  c7402000001900        mov       dword [eax+20h],190000h
  0045d6d9  81ca00000040          or        edx,40000000h
  0045d6df  66897810              mov       [eax+10h],di
  0045d6e3  8910                  mov       [eax],edx

LAB_0045d6e5:
  0045d6e5  5f                    pop       edi
  0045d6e6  5e                    pop       esi
  0045d6e7  5b                    pop       ebx
  0045d6e8  c3                    ret
  0045d6e9  90                    nop
  0045d6ea  90                    nop
  0045d6eb  90                    nop
  0045d6ec  90                    nop
  0045d6ed  90                    nop
  0045d6ee  90                    nop
  0045d6ef  90                    nop

8 cible(s) de saut interne : 0x0045d5fa, 0x0045d611, 0x0045d666, 0x0045d684, 0x0045d6af, 0x0045d6b7, 0x0045d6cf, 0x0045d6e5
