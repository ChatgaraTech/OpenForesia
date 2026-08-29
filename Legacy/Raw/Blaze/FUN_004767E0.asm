Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004767e0  section .text  offset fichier 0x75be0  192 octets

  004767e0  83ec0c                sub       esp,0Ch
  004767e3  53                    push      ebx
  004767e4  56                    push      esi
  004767e5  8b742418              mov       esi,[esp+18h]
  004767e9  57                    push      edi
  004767ea  33db                  xor       ebx,ebx
  004767ec  56                    push      esi
  004767ed  66895c2410            mov       [esp+10h],bx
  004767f2  66895c2412            mov       [esp+12h],bx
  004767f7  66895c2414            mov       [esp+14h],bx
  004767fc  66c74424166400        mov       word [esp+16h],64h
  00476803  66895c2418            mov       [esp+18h],bx
  00476808  66895c241a            mov       [esp+1Ah],bx
  0047680d  e86ef00b00            call      00535880h
  00476812  0fbf4628              movsx     eax,word [esi+28h]
  00476816  b900100000            mov       ecx,1000h
  0047681b  83c404                add       esp,4
  0047681e  2bc8                  sub       ecx,eax
  00476820  c1f902                sar       ecx,2
  00476823  03c1                  add       eax,ecx
  00476825  66894628              mov       [esi+28h],ax
  00476829  6689462c              mov       [esi+2Ch],ax
  0047682d  8d1480                lea       edx,[eax+eax*4]
  00476830  8d0480                lea       eax,[eax+eax*4]
  00476833  c1e204                shl       edx,4
  00476836  8d04c0                lea       eax,[eax+eax*8]
  00476839  d1e0                  shl       eax,1
  0047683b  c1f80c                sar       eax,0Ch
  0047683e  6689442416            mov       [esp+16h],ax
  00476843  8b4638                mov       eax,[esi+38h]
  00476846  c1fa0c                sar       edx,0Ch
  00476849  3d20202000            cmp       eax,202020h
  0047684e  6689542410            mov       [esp+10h],dx
  00476853  7235                  jb        short 0047688Ah
  00476855  8d4c240c              lea       ecx,[esp+0Ch]
  00476859  51                    push      ecx
  0047685a  56                    push      esi
  0047685b  e810e4ffff            call      00474C70h
  00476860  8bf8                  mov       edi,eax
  00476862  a1b8e05d00            mov       eax,[5DE0B8h]
  00476867  83c408                add       esp,8
  0047686a  3bc3                  cmp       eax,ebx
  0047686c  741c                  je        short 0047688Ah
  0047686e  8b5674                mov       edx,[esi+74h]
  00476871  57                    push      edi
  00476872  52                    push      edx
  00476873  e808330c00            call      00539B80h
  00476878  8b07                  mov       eax,[edi]
  0047687a  83c408                add       esp,8
  0047687d  0c40                  or        al,40h
  0047687f  66c787960000000c00    mov       word [edi+96h],0Ch
  00476888  8907                  mov       [edi],eax

LAB_0047688a:
  0047688a  8b4638                mov       eax,[esi+38h]
  0047688d  05f8f7f7ff            add       eax,0FFF7F7F8h
  00476892  894638                mov       [esi+38h],eax
  00476895  7502                  jne       short 00476899h
  00476897  891e                  mov       [esi],ebx

LAB_00476899:
  00476899  5f                    pop       edi
  0047689a  5e                    pop       esi
  0047689b  5b                    pop       ebx
  0047689c  83c40c                add       esp,0Ch
  0047689f  c3                    ret

2 cible(s) de saut interne : 0x0047688a, 0x00476899
