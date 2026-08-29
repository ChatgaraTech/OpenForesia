C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004474c0  section .text  offset fichier 0x468c0  358 octets

  004474c0  53                    push      ebx
  004474c1  56                    push      esi
  004474c2  8b74240c              mov       esi,[esp+0Ch]
  004474c6  33db                  xor       ebx,ebx
  004474c8  57                    push      edi
  004474c9  b901000000            mov       ecx,1
  004474ce  66395e10              cmp       [esi+10h],bx
  004474d2  7548                  jne       short 0044751Ch
  004474d4  8b5620                mov       edx,[esi+20h]
  004474d7  b0ff                  mov       al,0FFh
  004474d9  884638                mov       [esi+38h],al
  004474dc  884639                mov       [esi+39h],al
  004474df  88463a                mov       [esi+3Ah],al
  004474e2  8b461c                mov       eax,[esi+1Ch]
  004474e5  894650                mov       [esi+50h],eax
  004474e8  8b4624                mov       eax,[esi+24h]
  004474eb  c7463ca0155500        mov       dword [esi+3Ch],5515A0h
  004474f2  885e40                mov       [esi+40h],bl
  004474f5  c6460a02              mov       byte [esi+0Ah],2
  004474f9  884e0b                mov       [esi+0Bh],cl
  004474fc  66894e10              mov       [esi+10h],cx
  00447500  66895e28              mov       [esi+28h],bx
  00447504  66895e2a              mov       [esi+2Ah],bx
  00447508  895654                mov       [esi+54h],edx
  0044750b  894658                mov       [esi+58h],eax
  0044750e  66899e9a000000        mov       [esi+9Ah],bx
  00447515  c7465c3c000000        mov       dword [esi+5Ch],3Ch

LAB_0044751c:
  0044751c  66394e10              cmp       [esi+10h],cx
  00447520  0f85fc000000          jne       near 00447622h
  00447526  0fbf4e32              movsx     ecx,word [esi+32h]
  0044752a  81c100040000          add       ecx,400h
  00447530  51                    push      ecx
  00447531  e8e0e40f00            call      00545A16h
  00447536  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0044753d  8bf8                  mov       edi,eax
  0044753f  0fbf4616              movsx     eax,word [esi+16h]
  00447543  0faffa                imul      edi,edx
  00447546  50                    push      eax
  00447547  c1ff0c                sar       edi,0Ch
  0044754a  e8cde40f00            call      00545A1Ch
  0044754f  0faff8                imul      edi,eax
  00447552  037e58                add       edi,[esi+58h]
  00447555  0fbf4e32              movsx     ecx,word [esi+32h]
  00447559  81c100040000          add       ecx,400h
  0044755f  897e24                mov       [esi+24h],edi
  00447562  51                    push      ecx
  00447563  e8b4e40f00            call      00545A1Ch
  00447568  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0044756f  8bf8                  mov       edi,eax
  00447571  0fbf4616              movsx     eax,word [esi+16h]
  00447575  0faffa                imul      edi,edx
  00447578  50                    push      eax
  00447579  c1ff0c                sar       edi,0Ch
  0044757c  e89be40f00            call      00545A1Ch
  00447581  0faff8                imul      edi,eax
  00447584  8b4650                mov       eax,[esi+50h]
  00447587  0fbf4e16              movsx     ecx,word [esi+16h]
  0044758b  03f8                  add       edi,eax
  0044758d  51                    push      ecx
  0044758e  897e1c                mov       [esi+1Ch],edi
  00447591  668bbe9a000000        mov       di,[esi+9Ah]
  00447598  e879e40f00            call      00545A16h
  0044759d  0fbfd7                movsx     edx,di
  004475a0  0fafc2                imul      eax,edx
  004475a3  8b4e54                mov       ecx,[esi+54h]
  004475a6  66037e5c              add       di,[esi+5Ch]
  004475aa  03c1                  add       eax,ecx
  004475ac  6af1                  push      0FFFFFFF1h
  004475ae  894620                mov       [esi+20h],eax
  004475b1  8b465c                mov       eax,[esi+5Ch]
  004475b4  8bc8                  mov       ecx,eax
  004475b6  6689be9a000000        mov       [esi+9Ah],di
  004475bd  c1f902                sar       ecx,2
  004475c0  2bc1                  sub       eax,ecx
  004475c2  668b4e28              mov       cx,[esi+28h]
  004475c6  89465c                mov       [esi+5Ch],eax
  004475c9  6af1                  push      0FFFFFFF1h
  004475cb  0fbf4614              movsx     eax,word [esi+14h]
  004475cf  0fbfd1                movsx     edx,cx
  004475d2  8d0440                lea       eax,[eax+eax*2]
  004475d5  6af1                  push      0FFFFFFF1h
  004475d7  56                    push      esi
  004475d8  8d0480                lea       eax,[eax+eax*4]
  004475db  d1e0                  shl       eax,1
  004475dd  8bf8                  mov       edi,eax
  004475df  2bfa                  sub       edi,edx
  004475e1  c1ff02                sar       edi,2
  004475e4  03f9                  add       edi,ecx
  004475e6  668b4e2a              mov       cx,[esi+2Ah]
  004475ea  0fbfd1                movsx     edx,cx
  004475ed  2bc2                  sub       eax,edx
  004475ef  66897e28              mov       [esi+28h],di
  004475f3  c1f802                sar       eax,2
  004475f6  03c1                  add       eax,ecx
  004475f8  6689462a              mov       [esi+2Ah],ax
  004475fc  e8dfd7fbff            call      00404DE0h
  00447601  8a4638                mov       al,[esi+38h]
  00447604  83c410                add       esp,10h
  00447607  3ac3                  cmp       al,bl
  00447609  7506                  jne       short 00447611h
  0044760b  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00447611:
  00447611  8a4e40                mov       cl,[esi+40h]
  00447614  fec1                  inc       cl
  00447616  8ac1                  mov       al,cl
  00447618  884e40                mov       [esi+40h],cl
  0044761b  3c03                  cmp       al,3
  0044761d  7503                  jne       short 00447622h
  0044761f  885e40                mov       [esi+40h],bl

LAB_00447622:
  00447622  5f                    pop       edi
  00447623  5e                    pop       esi
  00447624  5b                    pop       ebx
  00447625  c3                    ret

3 cible(s) de saut interne : 0x0044751c, 0x00447611, 0x00447622
