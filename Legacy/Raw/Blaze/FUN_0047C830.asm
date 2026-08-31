
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c830  section .text  offset fichier 0x7bc30  210 octets

  0047c830  8b442404              mov       eax,[esp+4]
  0047c834  57                    push      edi
  0047c835  8b4870                mov       ecx,[eax+70h]
  0047c838  85c9                  test      ecx,ecx
  0047c83a  7544                  jne       short 0047C880h
  0047c83c  66394810              cmp       [eax+10h],cx
  0047c840  7527                  jne       short 0047C869h
  0047c842  8b481c                mov       ecx,[eax+1Ch]
  0047c845  8b5044                mov       edx,[eax+44h]
  0047c848  8b7848                mov       edi,[eax+48h]
  0047c84b  03d1                  add       edx,ecx
  0047c84d  8b4824                mov       ecx,[eax+24h]
  0047c850  895044                mov       [eax+44h],edx
  0047c853  8b5020                mov       edx,[eax+20h]
  0047c856  66c740100100          mov       word [eax+10h],1
  0047c85c  03fa                  add       edi,edx
  0047c85e  8b504c                mov       edx,[eax+4Ch]
  0047c861  03d1                  add       edx,ecx
  0047c863  897848                mov       [eax+48h],edi
  0047c866  89504c                mov       [eax+4Ch],edx

LAB_0047c869:
  0047c869  c7401c00000000        mov       dword [eax+1Ch],0
  0047c870  c7402000000000        mov       dword [eax+20h],0
  0047c877  c7402400000000        mov       dword [eax+24h],0
  0047c87e  8bc8                  mov       ecx,eax

LAB_0047c880:
  0047c880  8b5044                mov       edx,[eax+44h]
  0047c883  8b791c                mov       edi,[ecx+1Ch]
  0047c886  03d7                  add       edx,edi
  0047c888  89501c                mov       [eax+1Ch],edx
  0047c88b  8b5048                mov       edx,[eax+48h]
  0047c88e  035120                add       edx,[ecx+20h]
  0047c891  895020                mov       [eax+20h],edx
  0047c894  8b504c                mov       edx,[eax+4Ch]
  0047c897  8b7924                mov       edi,[ecx+24h]
  0047c89a  668b4828              mov       cx,[eax+28h]
  0047c89e  03d7                  add       edx,edi
  0047c8a0  8b7850                mov       edi,[eax+50h]
  0047c8a3  895024                mov       [eax+24h],edx
  0047c8a6  0fbfd1                movsx     edx,cx
  0047c8a9  2bfa                  sub       edi,edx
  0047c8ab  c1ff02                sar       edi,2
  0047c8ae  03f9                  add       edi,ecx
  0047c8b0  668b482a              mov       cx,[eax+2Ah]
  0047c8b4  0fbfd1                movsx     edx,cx
  0047c8b7  66897828              mov       [eax+28h],di
  0047c8bb  8b7854                mov       edi,[eax+54h]
  0047c8be  2bfa                  sub       edi,edx
  0047c8c0  c1ff02                sar       edi,2
  0047c8c3  03f9                  add       edi,ecx
  0047c8c5  668b482c              mov       cx,[eax+2Ch]
  0047c8c9  0fbfd1                movsx     edx,cx
  0047c8cc  6689782a              mov       [eax+2Ah],di
  0047c8d0  8b7858                mov       edi,[eax+58h]
  0047c8d3  2bfa                  sub       edi,edx
  0047c8d5  c1ff02                sar       edi,2
  0047c8d8  03f9                  add       edi,ecx
  0047c8da  8b4838                mov       ecx,[eax+38h]
  0047c8dd  81c1f8f7f7ff          add       ecx,0FFF7F7F8h
  0047c8e3  6689782c              mov       [eax+2Ch],di
  0047c8e7  894838                mov       [eax+38h],ecx
  0047c8ea  8bd1                  mov       edx,ecx
  0047c8ec  668b4810              mov       cx,[eax+10h]
  0047c8f0  6685c9                test      cx,cx
  0047c8f3  7406                  je        short 0047C8FBh
  0047c8f5  85d2                  test      edx,edx
  0047c8f7  7502                  jne       short 0047C8FBh
  0047c8f9  8910                  mov       [eax],edx

LAB_0047c8fb:
  0047c8fb  41                    inc       ecx
  0047c8fc  5f                    pop       edi
  0047c8fd  66894810              mov       [eax+10h],cx
  0047c901  c3                    ret

3 cible(s) de saut interne : 0x0047c869, 0x0047c880, 0x0047c8fb
