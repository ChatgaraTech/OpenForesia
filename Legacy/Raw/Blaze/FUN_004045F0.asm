Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004045f0  section .text  offset fichier 0x39f0  320 octets

  004045f0  53                    push      ebx
  004045f1  56                    push      esi
  004045f2  8b74240c              mov       esi,[esp+0Ch]
  004045f6  33db                  xor       ebx,ebx
  004045f8  57                    push      edi
  004045f9  66395e10              cmp       [esi+10h],bx
  004045fd  0f85e6000000          jne       near 004046E9h
  00404603  0fbf7e14              movsx     edi,word [esi+14h]
  00404607  66c746100100          mov       word [esi+10h],1
  0040460d  c7463ce0f05400        mov       dword [esi+3Ch],54F0E0h
  00404614  885e40                mov       [esi+40h],bl
  00404617  c6460a02              mov       byte [esi+0Ah],2
  0040461b  885e0b                mov       [esi+0Bh],bl
  0040461e  e81d2b1400            call      00547140h
  00404623  8bc8                  mov       ecx,eax
  00404625  8bc7                  mov       eax,edi
  00404627  99                    cdq
  00404628  894c2410              mov       [esp+10h],ecx
  0040462c  2bc2                  sub       eax,edx
  0040462e  8b542410              mov       edx,[esp+10h]
  00404632  8bc8                  mov       ecx,eax
  00404634  8bc2                  mov       eax,edx
  00404636  d1f9                  sar       ecx,1
  00404638  99                    cdq
  00404639  41                    inc       ecx
  0040463a  f7f9                  idiv      ecx
  0040463c  8bc7                  mov       eax,edi
  0040463e  8b7e1c                mov       edi,[esi+1Ch]
  00404641  8bca                  mov       ecx,edx
  00404643  99                    cdq
  00404644  83e203                and       edx,3
  00404647  03c2                  add       eax,edx
  00404649  c1f802                sar       eax,2
  0040464c  2bc8                  sub       ecx,eax
  0040464e  c1e10c                shl       ecx,0Ch
  00404651  03f9                  add       edi,ecx
  00404653  897e1c                mov       [esi+1Ch],edi
  00404656  0fbf7e14              movsx     edi,word [esi+14h]
  0040465a  e8e12a1400            call      00547140h
  0040465f  8bc8                  mov       ecx,eax
  00404661  8bc7                  mov       eax,edi
  00404663  99                    cdq
  00404664  894c2410              mov       [esp+10h],ecx
  00404668  2bc2                  sub       eax,edx
  0040466a  8b542410              mov       edx,[esp+10h]
  0040466e  8bc8                  mov       ecx,eax
  00404670  8bc2                  mov       eax,edx
  00404672  d1f9                  sar       ecx,1
  00404674  99                    cdq
  00404675  41                    inc       ecx
  00404676  f7f9                  idiv      ecx
  00404678  8bc7                  mov       eax,edi
  0040467a  8bca                  mov       ecx,edx
  0040467c  99                    cdq
  0040467d  83e203                and       edx,3
  00404680  03c2                  add       eax,edx
  00404682  c1f802                sar       eax,2
  00404685  2bc8                  sub       ecx,eax
  00404687  8b4624                mov       eax,[esi+24h]
  0040468a  c1e10c                shl       ecx,0Ch
  0040468d  03c1                  add       eax,ecx
  0040468f  894624                mov       [esi+24h],eax
  00404692  e8a92a1400            call      00547140h
  00404697  8bc8                  mov       ecx,eax
  00404699  66895e12              mov       [esi+12h],bx
  0040469d  0fbf4614              movsx     eax,word [esi+14h]
  004046a1  99                    cdq
  004046a2  2bc2                  sub       eax,edx
  004046a4  8bf8                  mov       edi,eax
  004046a6  8bc1                  mov       eax,ecx
  004046a8  d1ff                  sar       edi,1
  004046aa  99                    cdq
  004046ab  47                    inc       edi
  004046ac  f7ff                  idiv      edi
  004046ae  8b4620                mov       eax,[esi+20h]
  004046b1  c1e20c                shl       edx,0Ch
  004046b4  03c2                  add       eax,edx
  004046b6  894620                mov       [esi+20h],eax
  004046b9  b8b80b0000            mov       eax,0BB8h
  004046be  66894628              mov       [esi+28h],ax
  004046c2  6689462a              mov       [esi+2Ah],ax
  004046c6  b0a0                  mov       al,0A0h
  004046c8  884638                mov       [esi+38h],al
  004046cb  884639                mov       [esi+39h],al
  004046ce  88463a                mov       [esi+3Ah],al
  004046d1  e86a2a1400            call      00547140h
  004046d6  99                    cdq
  004046d7  33c2                  xor       eax,edx
  004046d9  2bc2                  sub       eax,edx
  004046db  83e001                and       eax,1
  004046de  33c2                  xor       eax,edx
  004046e0  2bc2                  sub       eax,edx
  004046e2  83c002                add       eax,2
  004046e5  66894616              mov       [esi+16h],ax

LAB_004046e9:
  004046e9  66837e1001            cmp       word [esi+10h],1
  004046ee  7535                  jne       short 00404725h
  004046f0  6afe                  push      0FFFFFFFEh
  004046f2  6afe                  push      0FFFFFFFEh
  004046f4  6afe                  push      0FFFFFFFEh
  004046f6  56                    push      esi
  004046f7  e8e4060000            call      00404DE0h
  004046fc  83c410                add       esp,10h
  004046ff  66ff4612              inc       word [esi+12h]
  00404703  668b4612              mov       ax,[esi+12h]
  00404707  663b4616              cmp       ax,[esi+16h]
  0040470b  7c18                  jl        short 00404725h
  0040470d  8a4e40                mov       cl,[esi+40h]
  00404710  66895e12              mov       [esi+12h],bx
  00404714  fec1                  inc       cl
  00404716  8ac1                  mov       al,cl
  00404718  884e40                mov       [esi+40h],cl
  0040471b  3c07                  cmp       al,7
  0040471d  7506                  jne       short 00404725h
  0040471f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00404725:
  00404725  5f                    pop       edi
  00404726  5e                    pop       esi
  00404727  5b                    pop       ebx
  00404728  c3                    ret
  00404729  90                    nop
  0040472a  90                    nop
  0040472b  90                    nop
  0040472c  90                    nop
  0040472d  90                    nop
  0040472e  90                    nop
  0040472f  90                    nop

2 cible(s) de saut interne : 0x004046e9, 0x00404725
