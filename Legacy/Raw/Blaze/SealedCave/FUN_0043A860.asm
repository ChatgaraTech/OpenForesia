Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043a860  section .text  offset fichier 0x39c60  128 octets

  0043a860  8b442404              mov       eax,[esp+4]
  0043a864  33d2                  xor       edx,edx
  0043a866  53                    push      ebx
  0043a867  56                    push      esi
  0043a868  66395010              cmp       [eax+10h],dx
  0043a86c  57                    push      edi
  0043a86d  b901000000            mov       ecx,1
  0043a872  7521                  jne       short 0043A895h
  0043a874  8a5814                mov       bl,[eax+14h]
  0043a877  66894810              mov       [eax+10h],cx
  0043a87b  885804                mov       [eax+4],bl
  0043a87e  c6400a02              mov       byte [eax+0Ah],2
  0043a882  88500b                mov       [eax+0Bh],dl
  0043a885  66895032              mov       [eax+32h],dx
  0043a889  66c740340004          mov       word [eax+34h],400h
  0043a88f  66c740143c00          mov       word [eax+14h],3Ch

LAB_0043a895:
  0043a895  66394810              cmp       [eax+10h],cx
  0043a899  753e                  jne       short 0043A8D9h
  0043a89b  8b4850                mov       ecx,[eax+50h]
  0043a89e  8b581c                mov       ebx,[eax+1Ch]
  0043a8a1  8b7024                mov       esi,[eax+24h]
  0043a8a4  8b7820                mov       edi,[eax+20h]
  0043a8a7  c1e10c                shl       ecx,0Ch
  0043a8aa  03d9                  add       ebx,ecx
  0043a8ac  8b4858                mov       ecx,[eax+58h]
  0043a8af  c1e10c                shl       ecx,0Ch
  0043a8b2  03f1                  add       esi,ecx
  0043a8b4  8b485c                mov       ecx,[eax+5Ch]
  0043a8b7  897024                mov       [eax+24h],esi
  0043a8ba  8bf1                  mov       esi,ecx
  0043a8bc  c1e60b                shl       esi,0Bh
  0043a8bf  03fe                  add       edi,esi
  0043a8c1  49                    dec       ecx
  0043a8c2  66ff4814              dec       word [eax+14h]
  0043a8c6  3bca                  cmp       ecx,edx
  0043a8c8  89581c                mov       [eax+1Ch],ebx
  0043a8cb  897820                mov       [eax+20h],edi
  0043a8ce  89485c                mov       [eax+5Ch],ecx
  0043a8d1  7d06                  jge       short 0043A8D9h
  0043a8d3  8120ffffff7f          and       dword [eax],7FFFFFFFh

LAB_0043a8d9:
  0043a8d9  5f                    pop       edi
  0043a8da  5e                    pop       esi
  0043a8db  5b                    pop       ebx
  0043a8dc  c3                    ret
  0043a8dd  90                    nop
  0043a8de  90                    nop
  0043a8df  90                    nop

2 cible(s) de saut interne : 0x0043a895, 0x0043a8d9
