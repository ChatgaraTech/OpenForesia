Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00468950  section .text  offset fichier 0x67d50  208 octets

  00468950  53                    push      ebx
  00468951  56                    push      esi
  00468952  8b74240c              mov       esi,[esp+0Ch]
  00468956  57                    push      edi
  00468957  8a5e07                mov       bl,[esi+7]
  0046895a  8b16                  mov       edx,[esi]
  0046895c  80e3ef                and       bl,0EFh
  0046895f  80e6df                and       dh,0DFh
  00468962  885e07                mov       [esi+7],bl
  00468965  8bc2                  mov       eax,edx
  00468967  33db                  xor       ebx,ebx
  00468969  8916                  mov       [esi],edx
  0046896b  a810                  test      al,10h
  0046896d  66c7462a7017          mov       word [esi+2Ah],1770h
  00468973  750a                  jne       short 0046897Fh
  00468975  66395e10              cmp       [esi+10h],bx
  00468979  0f849a000000          je        near 00468A19h

LAB_0046897f:
  0046897f  66395e10              cmp       [esi+10h],bx
  00468983  b901000000            mov       ecx,1
  00468988  7532                  jne       short 004689BCh
  0046898a  0fbf4614              movsx     eax,word [esi+14h]
  0046898e  66894e10              mov       [esi+10h],cx
  00468992  66895e12              mov       [esi+12h],bx
  00468996  8d1480                lea       edx,[eax+eax*4]
  00468999  c1e203                shl       edx,3
  0046899c  2bd0                  sub       edx,eax
  0046899e  66894c9610            mov       [esi+edx*4+10h],cx
  004689a3  8b4620                mov       eax,[esi+20h]
  004689a6  c1f80c                sar       eax,0Ch
  004689a9  66395e16              cmp       [esi+16h],bx
  004689ad  6689869a000000        mov       [esi+9Ah],ax
  004689b4  7506                  jne       short 004689BCh
  004689b6  66c746161400          mov       word [esi+16h],14h

LAB_004689bc:
  004689bc  66394e10              cmp       [esi+10h],cx
  004689c0  7557                  jne       short 00468A19h
  004689c2  0fbf4e12              movsx     ecx,word [esi+12h]
  004689c6  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004689cd  51                    push      ecx
  004689ce  e849d00d00            call      00545A1Ch
  004689d3  8bd7                  mov       edx,edi
  004689d5  668146120001          add       word [esi+12h],100h
  004689db  c1e204                shl       edx,4
  004689de  03c2                  add       eax,edx
  004689e0  c1e008                shl       eax,8
  004689e3  894620                mov       [esi+20h],eax
  004689e6  668b4612              mov       ax,[esi+12h]
  004689ea  663d0008              cmp       ax,800h
  004689ee  7e0a                  jle       short 004689FAh
  004689f0  c1e70c                shl       edi,0Ch
  004689f3  66895e10              mov       [esi+10h],bx
  004689f7  897e20                mov       [esi+20h],edi

LAB_004689fa:
  004689fa  663d0004              cmp       ax,400h
  004689fe  7519                  jne       short 00468A19h
  00468a00  0fbf4616              movsx     eax,word [esi+16h]
  00468a04  8a5607                mov       dl,[esi+7]
  00468a07  895e50                mov       [esi+50h],ebx
  00468a0a  80ca10                or        dl,10h
  00468a0d  895e58                mov       [esi+58h],ebx
  00468a10  c1e00c                shl       eax,0Ch
  00468a13  885607                mov       [esi+7],dl
  00468a16  894654                mov       [esi+54h],eax

LAB_00468a19:
  00468a19  5f                    pop       edi
  00468a1a  5e                    pop       esi
  00468a1b  5b                    pop       ebx
  00468a1c  c3                    ret
  00468a1d  90                    nop
  00468a1e  90                    nop
  00468a1f  90                    nop

4 cible(s) de saut interne : 0x0046897f, 0x004689bc, 0x004689fa, 0x00468a19
