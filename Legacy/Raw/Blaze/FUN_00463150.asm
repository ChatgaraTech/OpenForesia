C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00463150  section .text  offset fichier 0x62550  431 octets

  00463150  8b442404              mov       eax,[esp+4]
  00463154  55                    push      ebp
  00463155  33ed                  xor       ebp,ebp
  00463157  56                    push      esi
  00463158  668b4810              mov       cx,[eax+10h]
  0046315c  57                    push      edi
  0046315d  663bcd                cmp       cx,bp
  00463160  7504                  jne       short 00463166h
  00463162  66896814              mov       [eax+14h],bp

LAB_00463166:
  00463166  6683f901              cmp       cx,1
  0046316a  be00300700            mov       esi,73000h
  0046316f  751f                  jne       short 00463190h
  00463171  8b5020                mov       edx,[eax+20h]
  00463174  81c200500000          add       edx,5000h
  0046317a  8bca                  mov       ecx,edx
  0046317c  895020                mov       [eax+20h],edx
  0046317f  3bce                  cmp       ecx,esi
  00463181  7e0d                  jle       short 00463190h
  00463183  897020                mov       [eax+20h],esi
  00463186  66c740100200          mov       word [eax+10h],2
  0046318c  66896812              mov       [eax+12h],bp

LAB_00463190:
  00463190  6683781002            cmp       word [eax+10h],2
  00463195  ba03000000            mov       edx,3
  0046319a  753b                  jne       short 004631D7h
  0046319c  668b4812              mov       cx,[eax+12h]
  004631a0  0fbfe9                movsx     ebp,cx
  004631a3  c1e50c                shl       ebp,0Ch
  004631a6  29681c                sub       [eax+1Ch],ebp
  004631a9  41                    inc       ecx
  004631aa  6683f90f              cmp       cx,0Fh
  004631ae  66894812              mov       [eax+12h],cx
  004631b2  7e06                  jle       short 004631BAh
  004631b4  66c740120f00          mov       word [eax+12h],0Fh

LAB_004631ba:
  004631ba  8b681c                mov       ebp,[eax+1Ch]
  004631bd  b90080fdff            mov       ecx,0FFFD8000h
  004631c2  3be9                  cmp       ebp,ecx
  004631c4  7d0f                  jge       short 004631D5h
  004631c6  33ed                  xor       ebp,ebp
  004631c8  89481c                mov       [eax+1Ch],ecx
  004631cb  66895010              mov       [eax+10h],dx
  004631cf  66896812              mov       [eax+12h],bp
  004631d3  eb02                  jmp       short 004631D7h

LAB_004631d5:
  004631d5  33ed                  xor       ebp,ebp

LAB_004631d7:
  004631d7  66395010              cmp       [eax+10h],dx
  004631db  b904000000            mov       ecx,4
  004631e0  7513                  jne       short 004631F5h
  004631e2  66ff4012              inc       word [eax+12h]
  004631e6  668378122d            cmp       word [eax+12h],2Dh
  004631eb  7e08                  jle       short 004631F5h
  004631ed  66894810              mov       [eax+10h],cx
  004631f1  66896812              mov       [eax+12h],bp

LAB_004631f5:
  004631f5  66394810              cmp       [eax+10h],cx
  004631f9  ba63000000            mov       edx,63h
  004631fe  752a                  jne       short 0046322Ah
  00463200  668b4812              mov       cx,[eax+12h]
  00463204  0fbfe9                movsx     ebp,cx
  00463207  83e5fe                and       ebp,0FFFFFFFEh
  0046320a  c1e50b                shl       ebp,0Bh
  0046320d  296820                sub       [eax+20h],ebp
  00463210  8b6820                mov       ebp,[eax+20h]
  00463213  41                    inc       ecx
  00463214  66894812              mov       [eax+12h],cx
  00463218  b90080f3ff            mov       ecx,0FFF38000h
  0046321d  3be9                  cmp       ebp,ecx
  0046321f  7d07                  jge       short 00463228h
  00463221  894820                mov       [eax+20h],ecx
  00463224  66895010              mov       [eax+10h],dx

LAB_00463228:
  00463228  33ed                  xor       ebp,ebp

LAB_0046322a:
  0046322a  66395010              cmp       [eax+10h],dx
  0046322e  b964000000            mov       ecx,64h
  00463233  7522                  jne       short 00463257h
  00463235  6683781401            cmp       word [eax+14h],1
  0046323a  751b                  jne       short 00463257h
  0046323c  8b5020                mov       edx,[eax+20h]
  0046323f  81c200500000          add       edx,5000h
  00463245  3bd6                  cmp       edx,esi
  00463247  895020                mov       [eax+20h],edx
  0046324a  7e0b                  jle       short 00463257h
  0046324c  897020                mov       [eax+20h],esi
  0046324f  66894810              mov       [eax+10h],cx
  00463253  66896812              mov       [eax+12h],bp

LAB_00463257:
  00463257  66394810              cmp       [eax+10h],cx
  0046325b  ba66000000            mov       edx,66h
  00463260  753a                  jne       short 0046329Ch
  00463262  668b4812              mov       cx,[eax+12h]
  00463266  8b781c                mov       edi,[eax+1Ch]
  00463269  0fbff1                movsx     esi,cx
  0046326c  c1e60c                shl       esi,0Ch
  0046326f  03fe                  add       edi,esi
  00463271  41                    inc       ecx
  00463272  6683f90f              cmp       cx,0Fh
  00463276  89781c                mov       [eax+1Ch],edi
  00463279  66894812              mov       [eax+12h],cx
  0046327d  7e06                  jle       short 00463285h
  0046327f  66c740120f00          mov       word [eax+12h],0Fh

LAB_00463285:
  00463285  8b701c                mov       esi,[eax+1Ch]
  00463288  b900406a00            mov       ecx,6A4000h
  0046328d  3bf1                  cmp       esi,ecx
  0046328f  7e0b                  jle       short 0046329Ch
  00463291  89481c                mov       [eax+1Ch],ecx
  00463294  66895010              mov       [eax+10h],dx
  00463298  66896812              mov       [eax+12h],bp

LAB_0046329c:
  0046329c  6683781065            cmp       word [eax+10h],65h
  004632a1  7513                  jne       short 004632B6h
  004632a3  66ff4012              inc       word [eax+12h]
  004632a7  668378122d            cmp       word [eax+12h],2Dh
  004632ac  7e08                  jle       short 004632B6h
  004632ae  66895010              mov       [eax+10h],dx
  004632b2  66896812              mov       [eax+12h],bp

LAB_004632b6:
  004632b6  66395010              cmp       [eax+10h],dx
  004632ba  753f                  jne       short 004632FBh
  004632bc  668b4812              mov       cx,[eax+12h]
  004632c0  8b7020                mov       esi,[eax+20h]
  004632c3  0fbfd1                movsx     edx,cx
  004632c6  83e2fe                and       edx,0FFFFFFFEh
  004632c9  c1e20b                shl       edx,0Bh
  004632cc  2bf2                  sub       esi,edx
  004632ce  41                    inc       ecx
  004632cf  66894812              mov       [eax+12h],cx
  004632d3  b900d0f3ff            mov       ecx,0FFF3D000h
  004632d8  8bd6                  mov       edx,esi
  004632da  897020                mov       [eax+20h],esi
  004632dd  3bd1                  cmp       edx,ecx
  004632df  7d1a                  jge       short 004632FBh
  004632e1  894820                mov       [eax+20h],ecx
  004632e4  b902000000            mov       ecx,2
  004632e9  66896810              mov       [eax+10h],bp
  004632ed  668988ac000000        mov       [eax+0ACh],cx
  004632f4  66898848010000        mov       [eax+148h],cx

LAB_004632fb:
  004632fb  5f                    pop       edi
  004632fc  5e                    pop       esi
  004632fd  5d                    pop       ebp
  004632fe  c3                    ret

13 cible(s) de saut interne : 0x00463166, 0x00463190, 0x004631ba, 0x004631d5, 0x004631d7, 0x004631f5, 0x00463228, 0x0046322a, 0x00463257, 0x00463285, 0x0046329c, 0x004632b6, 0x004632fb
