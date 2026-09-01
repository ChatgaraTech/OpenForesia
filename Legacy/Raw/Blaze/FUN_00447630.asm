Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00447630  section .text  offset fichier 0x46a30  416 octets

  00447630  83ec0c                sub       esp,0Ch
  00447633  53                    push      ebx
  00447634  55                    push      ebp
  00447635  56                    push      esi
  00447636  8b74241c              mov       esi,[esp+1Ch]
  0044763a  57                    push      edi
  0044763b  66837e1001            cmp       word [esi+10h],1
  00447640  7521                  jne       short 00447663h
  00447642  8d461c                lea       eax,[esi+1Ch]
  00447645  50                    push      eax
  00447646  6800120000            push      1200h
  0044764b  68520b0000            push      0B52h
  00447650  6a09                  push      9
  00447652  e8c9640f00            call      0053DB20h
  00447657  8b06                  mov       eax,[esi]
  00447659  83c410                add       esp,10h
  0044765c  25ffffff7f            and       eax,7FFFFFFFh
  00447661  8906                  mov       [esi],eax

LAB_00447663:
  00447663  66837e1000            cmp       word [esi+10h],0
  00447668  0f855a010000          jne       near 004477C8h
  0044766e  8b6e70                mov       ebp,[esi+70h]
  00447671  85ed                  test      ebp,ebp
  00447673  7505                  jne       short 0044767Ah
  00447675  bd58e55d00            mov       ebp,5DE558h

LAB_0044767a:
  0044767a  8b466c                mov       eax,[esi+6Ch]
  0044767d  85c0                  test      eax,eax
  0044767f  89442410              mov       [esp+10h],eax
  00447683  7508                  jne       short 0044768Dh
  00447685  c744241058e55d00      mov       dword [esp+10h],5DE558h

LAB_0044768d:
  0044768d  8b4d1c                mov       ecx,[ebp+1Ch]
  00447690  8d5e1c                lea       ebx,[esi+1Ch]
  00447693  890b                  mov       [ebx],ecx
  00447695  8b5520                mov       edx,[ebp+20h]
  00447698  895620                mov       [esi+20h],edx
  0044769b  8b4524                mov       eax,[ebp+24h]
  0044769e  8d4c2420              lea       ecx,[esp+20h]
  004476a2  894624                mov       [esi+24h],eax
  004476a5  8d542414              lea       edx,[esp+14h]
  004476a9  51                    push      ecx
  004476aa  8d44241c              lea       eax,[esp+1Ch]
  004476ae  52                    push      edx
  004476af  50                    push      eax
  004476b0  55                    push      ebp
  004476b1  e8ca120f00            call      00538980h
  004476b6  8b4c2430              mov       ecx,[esp+30h]
  004476ba  83c410                add       esp,10h
  004476bd  668b411e              mov       ax,[ecx+1Eh]
  004476c1  6603411c              add       ax,[ecx+1Ch]
  004476c5  66034120              add       ax,[ecx+20h]
  004476c9  663d5e01              cmp       ax,15Eh
  004476cd  66894614              mov       [esi+14h],ax
  004476d1  7e06                  jle       short 004476D9h
  004476d3  66c746145e01          mov       word [esi+14h],15Eh

LAB_004476d9:
  004476d9  b864000000            mov       eax,64h
  004476de  66394614              cmp       [esi+14h],ax
  004476e2  7d04                  jge       short 004476E8h
  004476e4  66894614              mov       [esi+14h],ax

LAB_004476e8:
  004476e8  8b4e20                mov       ecx,[esi+20h]
  004476eb  6aff                  push      0FFFFFFFFh
  004476ed  6a00                  push      0
  004476ef  81c100500500          add       ecx,55000h
  004476f5  68a4000000            push      0A4h
  004476fa  56                    push      esi
  004476fb  66c74614aa00          mov       word [esi+14h],0AAh
  00447701  894e20                mov       [esi+20h],ecx
  00447704  e8d7330300            call      0047AAE0h
  00447709  83c410                add       esp,10h
  0044770c  33ff                  xor       edi,edi

LAB_0044770e:
  0044770e  6aff                  push      0FFFFFFFFh
  00447710  6a00                  push      0
  00447712  6898000000            push      98h
  00447717  6a00                  push      0
  00447719  53                    push      ebx
  0044771a  56                    push      esi
  0044771b  66897e16              mov       [esi+16h],di
  0044771f  e81c350300            call      0047AC40h
  00447724  81c700020000          add       edi,200h
  0044772a  83c418                add       esp,18h
  0044772d  6681ff000e            cmp       di,0E00h
  00447732  7eda                  jle       short 0044770Eh
  00447734  8b442420              mov       eax,[esp+20h]
  00447738  0fbf4820              movsx     ecx,word [eax+20h]
  0044773c  0fbf501e              movsx     edx,word [eax+1Eh]
  00447740  0fbf401c              movsx     eax,word [eax+1Ch]
  00447744  03ca                  add       ecx,edx
  00447746  03c8                  add       ecx,eax
  00447748  b856555555            mov       eax,55555556h
  0044774d  f7e9                  imul      ecx
  0044774f  8bca                  mov       ecx,edx
  00447751  b82c010000            mov       eax,12Ch
  00447756  c1e91f                shr       ecx,1Fh
  00447759  03d1                  add       edx,ecx
  0044775b  2bc2                  sub       eax,edx
  0044775d  6685c0                test      ax,ax
  00447760  66894614              mov       [esi+14h],ax
  00447764  7d06                  jge       short 0044776Ch
  00447766  66c746140000          mov       word [esi+14h],0

LAB_0044776c:
  0044776c  8b7c2410              mov       edi,[esp+10h]
  00447770  0fbf5732              movsx     edx,word [edi+32h]
  00447774  52                    push      edx
  00447775  e8a2e20f00            call      00545A1Ch
  0044777a  0fbf4e14              movsx     ecx,word [esi+14h]
  0044777e  c1f903                sar       ecx,3
  00447781  0fafc1                imul      eax,ecx
  00447784  894550                mov       [ebp+50h],eax
  00447787  0fbf5732              movsx     edx,word [edi+32h]
  0044778b  52                    push      edx
  0044778c  e885e20f00            call      00545A16h
  00447791  0fbf4e14              movsx     ecx,word [esi+14h]
  00447795  c1f903                sar       ecx,3
  00447798  0fafc1                imul      eax,ecx
  0044779b  894558                mov       [ebp+58h],eax
  0044779e  8b4500                mov       eax,[ebp]
  004477a1  53                    push      ebx
  004477a2  6800120000            push      1200h
  004477a7  0c01                  or        al,1
  004477a9  6820030000            push      320h
  004477ae  6a05                  push      5
  004477b0  c7455400800100        mov       dword [ebp+54h],18000h
  004477b7  894500                mov       [ebp],eax
  004477ba  e861630f00            call      0053DB20h
  004477bf  83c410                add       esp,10h
  004477c2  66c746100100          mov       word [esi+10h],1

LAB_004477c8:
  004477c8  5f                    pop       edi
  004477c9  5e                    pop       esi
  004477ca  5d                    pop       ebp
  004477cb  5b                    pop       ebx
  004477cc  83c40c                add       esp,0Ch
  004477cf  c3                    ret

8 cible(s) de saut interne : 0x00447663, 0x0044767a, 0x0044768d, 0x004476d9, 0x004476e8, 0x0044770e, 0x0044776c, 0x004477c8
