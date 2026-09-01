C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a54e0  section .text  offset fichier 0xa48e0  2571 octets

  004a54e0  83ec28                sub       esp,28h
  004a54e3  8d44240c              lea       eax,[esp+0Ch]
  004a54e7  8d4c2424              lea       ecx,[esp+24h]
  004a54eb  8d542420              lea       edx,[esp+20h]
  004a54ef  53                    push      ebx
  004a54f0  55                    push      ebp
  004a54f1  56                    push      esi
  004a54f2  8b742438              mov       esi,[esp+38h]
  004a54f6  57                    push      edi
  004a54f7  50                    push      eax
  004a54f8  51                    push      ecx
  004a54f9  52                    push      edx
  004a54fa  56                    push      esi
  004a54fb  e880340900            call      00538980h
  004a5500  83c410                add       esp,10h
  004a5503  8d442420              lea       eax,[esp+20h]
  004a5507  8d4c2414              lea       ecx,[esp+14h]
  004a550b  8d54243c              lea       edx,[esp+3Ch]
  004a550f  50                    push      eax
  004a5510  51                    push      ecx
  004a5511  8d442418              lea       eax,[esp+18h]
  004a5515  52                    push      edx
  004a5516  8d4c2424              lea       ecx,[esp+24h]
  004a551a  50                    push      eax
  004a551b  51                    push      ecx
  004a551c  56                    push      esi
  004a551d  e8fe090900            call      00535F20h
  004a5522  8b44242c              mov       eax,[esp+2Ch]
  004a5526  83c418                add       esp,18h
  004a5529  85c0                  test      eax,eax
  004a552b  bf01000000            mov       edi,1
  004a5530  0f8421070000          je        near 004A5C57h
  004a5536  56                    push      esi
  004a5537  e864600300            call      004DB5A0h
  004a553c  8b06                  mov       eax,[esi]
  004a553e  83c404                add       esp,4
  004a5541  a802                  test      al,2
  004a5543  0f84bd030000          je        near 004A5906h
  004a5549  8a4c2420              mov       cl,[esp+20h]
  004a554d  bb10000000            mov       ebx,10h
  004a5552  84cb                  test      bl,cl
  004a5554  740d                  je        short 004A5563h
  004a5556  24fd                  and       al,0FDh
  004a5558  c6464300              mov       byte [esi+43h],0
  004a555c  8906                  mov       [esi],eax
  004a555e  e9f4060000            jmp       004A5C57h

LAB_004a5563:
  004a5563  33c9                  xor       ecx,ecx
  004a5565  33ed                  xor       ebp,ebp
  004a5567  8a4e42                mov       cl,[esi+42h]
  004a556a  2bcd                  sub       ecx,ebp
  004a556c  0f84a2020000          je        near 004A5814h
  004a5572  49                    dec       ecx
  004a5573  0f849f000000          je        near 004A5618h
  004a5579  49                    dec       ecx
  004a557a  0f85d7060000          jne       near 004A5C57h
  004a5580  8a4e43                mov       cl,[esi+43h]
  004a5583  8bd1                  mov       edx,ecx
  004a5585  81e2ff000000          and       edx,0FFh
  004a558b  2bd5                  sub       edx,ebp
  004a558d  7471                  je        short 004A5600h
  004a558f  4a                    dec       edx
  004a5590  742c                  je        short 004A55BEh
  004a5592  4a                    dec       edx
  004a5593  0f85be060000          jne       near 004A5C57h
  004a5599  66837e1a08            cmp       word [esi+1Ah],8
  004a559e  0f85b3060000          jne       near 004A5C57h
  004a55a4  25fffffffe            and       eax,0FEFFFFFFh
  004a55a9  c7465400400600        mov       dword [esi+54h],64000h
  004a55b0  0bc7                  or        eax,edi
  004a55b2  fec9                  dec       cl
  004a55b4  8906                  mov       [esi],eax
  004a55b6  884e43                mov       [esi+43h],cl
  004a55b9  e999060000            jmp       004A5C57h

LAB_004a55be:
  004a55be  a801                  test      al,1
  004a55c0  0f8591060000          jne       near 004A5C57h
  004a55c6  8b15cc975e00          mov       edx,[5E97CCh]
  004a55cc  6aff                  push      0FFFFFFFFh
  004a55ce  6aff                  push      0FFFFFFFFh
  004a55d0  8b82cc000000          mov       eax,[edx+0CCh]
  004a55d6  33d2                  xor       edx,edx
  004a55d8  8b08                  mov       ecx,[eax]
  004a55da  8a5102                mov       dl,[ecx+2]
  004a55dd  81c2c0000000          add       edx,0C0h
  004a55e3  52                    push      edx
  004a55e4  56                    push      esi
  004a55e5  e8e653fdff            call      0047A9D0h
  004a55ea  8a4643                mov       al,[esi+43h]
  004a55ed  83c410                add       esp,10h
  004a55f0  fec8                  dec       al
  004a55f2  66c746102800          mov       word [esi+10h],28h
  004a55f8  884643                mov       [esi+43h],al
  004a55fb  e957060000            jmp       004A5C57h

LAB_004a5600:
  004a5600  66ff4e10              dec       word [esi+10h]
  004a5604  66837e1000            cmp       word [esi+10h],0
  004a5609  0f8548060000          jne       near 004A5C57h
  004a560f  24fd                  and       al,0FDh
  004a5611  8906                  mov       [esi],eax
  004a5613  e93f060000            jmp       004A5C57h

LAB_004a5618:
  004a5618  668b5610              mov       dx,[esi+10h]
  004a561c  0fbfca                movsx     ecx,dx
  004a561f  2bcd                  sub       ecx,ebp
  004a5621  0f84c7000000          je        near 004A56EEh
  004a5627  49                    dec       ecx
  004a5628  7462                  je        short 004A568Ch
  004a562a  49                    dec       ecx
  004a562b  0f8526060000          jne       near 004A5C57h
  004a5631  8b465c                mov       eax,[esi+5Ch]
  004a5634  8bc8                  mov       ecx,eax
  004a5636  c1f902                sar       ecx,2
  004a5639  2bc1                  sub       eax,ecx
  004a563b  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004a5640  89465c                mov       [esi+5Ch],eax
  004a5643  7505                  jne       short 004A564Ah
  004a5645  4a                    dec       edx
  004a5646  66895610              mov       [esi+10h],dx

LAB_004a564a:
  004a564a  8b44243c              mov       eax,[esp+3Ch]
  004a564e  8d5040                lea       edx,[eax+40h]
  004a5651  81e2ff0f0000          and       edx,0FFFh
  004a5657  81fa80000000          cmp       edx,80h
  004a565d  7f09                  jg        short 004A5668h
  004a565f  66014632              add       [esi+32h],ax
  004a5663  e9ef050000            jmp       004A5C57h

LAB_004a5668:
  004a5668  25ff0f0000            and       eax,0FFFh
  004a566d  33c9                  xor       ecx,ecx
  004a566f  3d00080000            cmp       eax,800h
  004a5674  0f9dc1                setge     cl
  004a5677  49                    dec       ecx
  004a5678  81e180000000          and       ecx,80h
  004a567e  83c1c0                add       ecx,0FFFFFFC0h
  004a5681  8bc1                  mov       eax,ecx
  004a5683  66014632              add       [esi+32h],ax
  004a5687  e9cb050000            jmp       004A5C57h

LAB_004a568c:
  004a568c  0fbf5632              movsx     edx,word [esi+32h]
  004a5690  52                    push      edx
  004a5691  c7465c00000300        mov       dword [esi+5Ch],30000h
  004a5698  e87f030a00            call      00545A1Ch
  004a569d  0faf465c              imul      eax,[esi+5Ch]
  004a56a1  8b4e50                mov       ecx,[esi+50h]
  004a56a4  c1f80c                sar       eax,0Ch
  004a56a7  03c8                  add       ecx,eax
  004a56a9  0fbf4632              movsx     eax,word [esi+32h]
  004a56ad  50                    push      eax
  004a56ae  894e50                mov       [esi+50h],ecx
  004a56b1  e860030a00            call      00545A16h
  004a56b6  0faf465c              imul      eax,[esi+5Ch]
  004a56ba  8b4e58                mov       ecx,[esi+58h]
  004a56bd  c7465400200300        mov       dword [esi+54h],32000h
  004a56c4  c1f80c                sar       eax,0Ch
  004a56c7  03c8                  add       ecx,eax
  004a56c9  8b06                  mov       eax,[esi]
  004a56cb  894e58                mov       [esi+58h],ecx
  004a56ce  8d4e1c                lea       ecx,[esi+1Ch]
  004a56d1  51                    push      ecx
  004a56d2  0bc7                  or        eax,edi
  004a56d4  66ff4e10              dec       word [esi+10h]
  004a56d8  6800180000            push      1800h
  004a56dd  6850150000            push      1550h
  004a56e2  8906                  mov       [esi],eax
  004a56e4  896e5c                mov       [esi+5Ch],ebp
  004a56e7  6a20                  push      20h
  004a56e9  e961050000            jmp       004A5C4Fh

LAB_004a56ee:
  004a56ee  a900000002            test      eax,2000000h
  004a56f3  7408                  je        short 004A56FDh
  004a56f5  a801                  test      al,1
  004a56f7  7504                  jne       short 004A56FDh
  004a56f9  24dd                  and       al,0DDh
  004a56fb  8906                  mov       [esi],eax

LAB_004a56fd:
  004a56fd  f60620                test      byte [esi],20h
  004a5700  0f8551050000          jne       near 004A5C57h
  004a5706  8d6e1c                lea       ebp,[esi+1Ch]
  004a5709  6a00                  push      0
  004a570b  68242a5500            push      552A24h
  004a5710  55                    push      ebp
  004a5711  56                    push      esi
  004a5712  e819450700            call      00519C30h
  004a5717  8bf8                  mov       edi,eax
  004a5719  83c410                add       esp,10h
  004a571c  85ff                  test      edi,edi
  004a571e  7406                  je        short 004A5726h
  004a5720  8b06                  mov       eax,[esi]
  004a5722  0c20                  or        al,20h
  004a5724  8906                  mov       [esi],eax

LAB_004a5726:
  004a5726  a1b8e05d00            mov       eax,[5DE0B8h]
  004a572b  85c0                  test      eax,eax
  004a572d  0f8424050000          je        near 004A5C57h
  004a5733  66899f96000000        mov       [edi+96h],bx
  004a573a  8b1f                  mov       ebx,[edi]
  004a573c  83cb40                or        ebx,40h
  004a573f  57                    push      edi
  004a5740  891f                  mov       [edi],ebx
  004a5742  8b4c2420              mov       ecx,[esp+20h]
  004a5746  56                    push      esi
  004a5747  668b4130              mov       ax,[ecx+30h]
  004a574b  0fbfd8                movsx     ebx,ax
  004a574e  66d1f8                sar       ax,1
  004a5751  66894130              mov       [ecx+30h],ax
  004a5755  e826440900            call      00539B80h
  004a575a  8b542424              mov       edx,[esp+24h]
  004a575e  83c408                add       esp,8
  004a5761  66895a30              mov       [edx+30h],bx
  004a5765  8d5f1c                lea       ebx,[edi+1Ch]
  004a5768  53                    push      ebx
  004a5769  6800180000            push      1800h
  004a576e  6800030000            push      300h
  004a5773  6a03                  push      3
  004a5775  e8a6830900            call      0053DB20h
  004a577a  8b07                  mov       eax,[edi]
  004a577c  83c410                add       esp,10h
  004a577f  a900000040            test      eax,40000000h
  004a5784  0f85cd040000          jne       near 004A5C57h
  004a578a  0fbf4632              movsx     eax,word [esi+32h]
  004a578e  50                    push      eax
  004a578f  e888020a00            call      00545A1Ch
  004a5794  8d0440                lea       eax,[eax+eax*2]
  004a5797  8d0c80                lea       ecx,[eax+eax*4]
  004a579a  d1e1                  shl       ecx,1
  004a579c  894f50                mov       [edi+50h],ecx
  004a579f  0fbf5632              movsx     edx,word [esi+32h]
  004a57a3  52                    push      edx
  004a57a4  e86d020a00            call      00545A16h
  004a57a9  8d0440                lea       eax,[eax+eax*2]
  004a57ac  8d0480                lea       eax,[eax+eax*4]
  004a57af  d1e0                  shl       eax,1
  004a57b1  894758                mov       [edi+58h],eax
  004a57b4  8b07                  mov       eax,[edi]
  004a57b6  a801                  test      al,1
  004a57b8  750b                  jne       short 004A57C5h
  004a57ba  0c01                  or        al,1
  004a57bc  c7475400800100        mov       dword [edi+54h],18000h
  004a57c3  8907                  mov       [edi],eax

LAB_004a57c5:
  004a57c5  8b4d00                mov       ecx,[ebp]
  004a57c8  8b5620                mov       edx,[esi+20h]
  004a57cb  8b4624                mov       eax,[esi+24h]
  004a57ce  894c2424              mov       [esp+24h],ecx
  004a57d2  8b0b                  mov       ecx,[ebx]
  004a57d4  89542428              mov       [esp+28h],edx
  004a57d8  894d00                mov       [ebp],ecx
  004a57db  8b5720                mov       edx,[edi+20h]
  004a57de  6aff                  push      0FFFFFFFFh
  004a57e0  89442430              mov       [esp+30h],eax
  004a57e4  895620                mov       [esi+20h],edx
  004a57e7  8b4724                mov       eax,[edi+24h]
  004a57ea  6a00                  push      0
  004a57ec  6a39                  push      39h
  004a57ee  56                    push      esi
  004a57ef  894624                mov       [esi+24h],eax
  004a57f2  e8d951fdff            call      0047A9D0h
  004a57f7  8b4c2434              mov       ecx,[esp+34h]
  004a57fb  8b542438              mov       edx,[esp+38h]
  004a57ff  8b44243c              mov       eax,[esp+3Ch]
  004a5803  83c410                add       esp,10h
  004a5806  894d00                mov       [ebp],ecx
  004a5809  895620                mov       [esi+20h],edx
  004a580c  894624                mov       [esi+24h],eax
  004a580f  e943040000            jmp       004A5C57h

LAB_004a5814:
  004a5814  668b4e1a              mov       cx,[esi+1Ah]
  004a5818  6683f905              cmp       cx,5
  004a581c  7f3c                  jg        short 004A585Ah
  004a581e  8b44243c              mov       eax,[esp+3Ch]
  004a5822  8d5040                lea       edx,[eax+40h]
  004a5825  81e2ff0f0000          and       edx,0FFFh
  004a582b  81fa80000000          cmp       edx,80h
  004a5831  7f06                  jg        short 004A5839h
  004a5833  66014632              add       [esi+32h],ax
  004a5837  eb21                  jmp       short 004A585Ah

LAB_004a5839:
  004a5839  25ff0f0000            and       eax,0FFFh
  004a583e  33d2                  xor       edx,edx
  004a5840  3d00080000            cmp       eax,800h
  004a5845  0f9dc2                setge     dl
  004a5848  4a                    dec       edx
  004a5849  81e280000000          and       edx,80h
  004a584f  83c2c0                add       edx,0FFFFFFC0h
  004a5852  8bc2                  mov       eax,edx
  004a5854  66014632              add       [esi+32h],ax
  004a5858  33ed                  xor       ebp,ebp

LAB_004a585a:
  004a585a  6683f906              cmp       cx,6
  004a585e  753c                  jne       short 004A589Ch
  004a5860  0fbf4632              movsx     eax,word [esi+32h]
  004a5864  50                    push      eax
  004a5865  c7465c00400200        mov       dword [esi+5Ch],24000h
  004a586c  e8ab010a00            call      00545A1Ch
  004a5871  0faf465c              imul      eax,[esi+5Ch]
  004a5875  8b4e50                mov       ecx,[esi+50h]
  004a5878  c1f80c                sar       eax,0Ch
  004a587b  03c8                  add       ecx,eax
  004a587d  894e50                mov       [esi+50h],ecx
  004a5880  0fbf4e32              movsx     ecx,word [esi+32h]
  004a5884  51                    push      ecx
  004a5885  e88c010a00            call      00545A16h
  004a588a  0faf465c              imul      eax,[esi+5Ch]
  004a588e  8b4e58                mov       ecx,[esi+58h]
  004a5891  896e5c                mov       [esi+5Ch],ebp
  004a5894  c1f80c                sar       eax,0Ch
  004a5897  03c8                  add       ecx,eax
  004a5899  894e58                mov       [esi+58h],ecx

LAB_004a589c:
  004a589c  66837e1a07            cmp       word [esi+1Ah],7
  004a58a1  7c4e                  jl        short 004A58F1h
  004a58a3  55                    push      ebp
  004a58a4  8d561c                lea       edx,[esi+1Ch]
  004a58a7  68182a5500            push      552A18h
  004a58ac  52                    push      edx
  004a58ad  56                    push      esi
  004a58ae  e87d430700            call      00519C30h
  004a58b3  8bf8                  mov       edi,eax
  004a58b5  a1b8e05d00            mov       eax,[5DE0B8h]
  004a58ba  83c410                add       esp,10h
  004a58bd  3bc5                  cmp       eax,ebp
  004a58bf  7430                  je        short 004A58F1h
  004a58c1  66899f96000000        mov       [edi+96h],bx
  004a58c8  8b1f                  mov       ebx,[edi]
  004a58ca  83cb40                or        ebx,40h
  004a58cd  57                    push      edi
  004a58ce  56                    push      esi
  004a58cf  891f                  mov       [edi],ebx
  004a58d1  e8aa420900            call      00539B80h
  004a58d6  83c408                add       esp,8
  004a58d9  83c71c                add       edi,1Ch
  004a58dc  57                    push      edi
  004a58dd  6800180000            push      1800h
  004a58e2  6800030000            push      300h
  004a58e7  6a03                  push      3
  004a58e9  e832820900            call      0053DB20h
  004a58ee  83c410                add       esp,10h

LAB_004a58f1:
  004a58f1  8b06                  mov       eax,[esi]
  004a58f3  a900000002            test      eax,2000000h
  004a58f8  7404                  je        short 004A58FEh
  004a58fa  24fd                  and       al,0FDh
  004a58fc  8906                  mov       [esi],eax

LAB_004a58fe:
  004a58fe  896e5c                mov       [esi+5Ch],ebp
  004a5901  e951030000            jmp       004A5C57h

LAB_004a5906:
  004a5906  a810                  test      al,10h
  004a5908  0f8549030000          jne       near 004A5C57h
  004a590e  a904080000            test      eax,804h
  004a5913  0f8451010000          je        near 004A5A6Ah
  004a5919  f7442420e8030040      test      dword [esp+20h],400003E8h
  004a5921  740b                  je        short 004A592Eh
  004a5923  25fbf7ffff            and       eax,0FFFFF7FBh
  004a5928  c6464300              mov       byte [esi+43h],0
  004a592c  8906                  mov       [esi],eax

LAB_004a592e:
  004a592e  668b4610              mov       ax,[esi+10h]
  004a5932  0fbfc8                movsx     ecx,ax
  004a5935  83f903                cmp       ecx,3
  004a5938  0f8719030000          ja        near 004A5C57h
  004a593e  ff248dec5e4a00        jmp       dword [ecx*4+4A5EECh]
  004a5945  8b06                  mov       eax,[esi]
  004a5947  a900000002            test      eax,2000000h
  004a594c  0f8405030000          je        near 004A5C57h
  004a5952  80e4f7                and       ah,0F7h
  004a5955  8906                  mov       [esi],eax
  004a5957  e9fb020000            jmp       004A5C57h
  004a595c  b807000000            mov       eax,7
  004a5961  66394618              cmp       [esi+18h],ax
  004a5965  7411                  je        short 004A5978h
  004a5967  894618                mov       [esi+18h],eax
  004a596a  8b06                  mov       eax,[esi]
  004a596c  25fbfffffe            and       eax,0FEFFFFFBh
  004a5971  8906                  mov       [esi],eax
  004a5973  e9df020000            jmp       004A5C57h

LAB_004a5978:
  004a5978  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004a597d  0f85d4020000          jne       near 004A5C57h
  004a5983  33c0                  xor       eax,eax
  004a5985  6aff                  push      0FFFFFFFFh
  004a5987  668b4606              mov       ax,[esi+6]
  004a598b  6aff                  push      0FFFFFFFFh
  004a598d  8bc8                  mov       ecx,eax
  004a598f  c1f808                sar       eax,8
  004a5992  81e1ff000000          and       ecx,0FFh
  004a5998  25ff000000            and       eax,0FFh
  004a599d  51                    push      ecx
  004a599e  50                    push      eax
  004a599f  56                    push      esi
  004a59a0  e8db4f0900            call      0053A980h
  004a59a5  83c414                add       esp,14h
  004a59a8  66ff4e10              dec       word [esi+10h]
  004a59ac  e9a6020000            jmp       004A5C57h
  004a59b1  8a4e43                mov       cl,[esi+43h]
  004a59b4  fec9                  dec       cl
  004a59b6  884e43                mov       [esi+43h],cl
  004a59b9  7505                  jne       short 004A59C0h
  004a59bb  48                    dec       eax
  004a59bc  66894610              mov       [esi+10h],ax

LAB_004a59c0:
  004a59c0  8b542418              mov       edx,[esp+18h]
  004a59c4  8b4c243c              mov       ecx,[esp+3Ch]
  004a59c8  8d049532000000        lea       eax,[edx*4+32h]
  004a59cf  8d1408                lea       edx,[eax+ecx]
  004a59d2  8d3c00                lea       edi,[eax+eax]
  004a59d5  81e2ff0f0000          and       edx,0FFFh
  004a59db  3bd7                  cmp       edx,edi
  004a59dd  7f09                  jg        short 004A59E8h
  004a59df  66014e32              add       [esi+32h],cx
  004a59e3  e96f020000            jmp       004A5C57h

LAB_004a59e8:
  004a59e8  81e1ff0f0000          and       ecx,0FFFh
  004a59ee  81f900080000          cmp       ecx,800h
  004a59f4  7c02                  jl        short 004A59F8h
  004a59f6  f7d8                  neg       eax

LAB_004a59f8:
  004a59f8  66014632              add       [esi+32h],ax
  004a59fc  e956020000            jmp       004A5C57h
  004a5a01  6a00                  push      0
  004a5a03  6a00                  push      0
  004a5a05  68bd000000            push      0BDh
  004a5a0a  56                    push      esi
  004a5a0b  e8d050fdff            call      0047AAE0h
  004a5a10  83c410                add       esp,10h
  004a5a13  e828170a00            call      00547140h
  004a5a18  99                    cdq
  004a5a19  b903000000            mov       ecx,3
  004a5a1e  f7f9                  idiv      ecx
  004a5a20  33c0                  xor       eax,eax
  004a5a22  668b0455302a5500      mov       ax,[edx*2+552A30h]
  004a5a2a  66894606              mov       [esi+6],ax
  004a5a2e  8b0dc4975e00          mov       ecx,[5E97C4h]
  004a5a34  8bd0                  mov       edx,eax
  004a5a36  25ff000000            and       eax,0FFh
  004a5a3b  8b899c000000          mov       ecx,[ecx+9Ch]
  004a5a41  c1fa08                sar       edx,8
  004a5a44  81e2ff000000          and       edx,0FFh
  004a5a4a  8d0440                lea       eax,[eax+eax*2]
  004a5a4d  c1e004                shl       eax,4
  004a5a50  8b0c91                mov       ecx,[ecx+edx*4]
  004a5a53  8a540113              mov       dl,[ecx+eax+13h]
  004a5a57  66ff4e10              dec       word [esi+10h]
  004a5a5b  885643                mov       [esi+43h],dl
  004a5a5e  c7465c00000000        mov       dword [esi+5Ch],0
  004a5a65  e9ed010000            jmp       004A5C57h

LAB_004a5a6a:
  004a5a6a  a801                  test      al,1
  004a5a6c  0f85e5010000          jne       near 004A5C57h
  004a5a72  33c9                  xor       ecx,ecx
  004a5a74  8a4e42                mov       cl,[esi+42h]
  004a5a77  83f903                cmp       ecx,3
  004a5a7a  0f87a9010000          ja        near 004A5C29h
  004a5a80  ff248dfc5e4a00        jmp       dword [ecx*4+4A5EFCh]
  004a5a87  33c9                  xor       ecx,ecx
  004a5a89  66394e18              cmp       [esi+18h],cx
  004a5a8d  740a                  je        short 004A5A99h
  004a5a8f  0d00000001            or        eax,1000000h
  004a5a94  894e18                mov       [esi+18h],ecx
  004a5a97  8906                  mov       [esi],eax

LAB_004a5a99:
  004a5a99  817c24102c010000      cmp       dword [esp+10h],12Ch
  004a5aa1  7d09                  jge       short 004A5AACh
  004a5aa3  c6464300              mov       byte [esi+43h],0
  004a5aa7  e97d010000            jmp       004A5C29h

LAB_004a5aac:
  004a5aac  8a4643                mov       al,[esi+43h]
  004a5aaf  84c0                  test      al,al
  004a5ab1  7405                  je        short 004A5AB8h
  004a5ab3  fec8                  dec       al
  004a5ab5  884643                mov       [esi+43h],al

LAB_004a5ab8:
  004a5ab8  894e5c                mov       [esi+5Ch],ecx
  004a5abb  e969010000            jmp       004A5C29h
  004a5ac0  66397e18              cmp       [esi+18h],di
  004a5ac4  740a                  je        short 004A5AD0h
  004a5ac6  0d00000001            or        eax,1000000h
  004a5acb  897e18                mov       [esi+18h],edi
  004a5ace  8906                  mov       [esi],eax

LAB_004a5ad0:
  004a5ad0  8b44243c              mov       eax,[esp+3Ch]
  004a5ad4  8d8880000000          lea       ecx,[eax+80h]
  004a5ada  81e1ff0f0000          and       ecx,0FFFh
  004a5ae0  81f900010000          cmp       ecx,100h
  004a5ae6  7f14                  jg        short 004A5AFCh
  004a5ae8  66014632              add       [esi+32h],ax
  004a5aec  c6464300              mov       byte [esi+43h],0
  004a5af0  c7465c00000000        mov       dword [esi+5Ch],0
  004a5af7  e92d010000            jmp       004A5C29h

LAB_004a5afc:
  004a5afc  25ff0f0000            and       eax,0FFFh
  004a5b01  33d2                  xor       edx,edx
  004a5b03  3d00080000            cmp       eax,800h
  004a5b08  c7465c00000000        mov       dword [esi+5Ch],0
  004a5b0f  0f9dc2                setge     dl
  004a5b12  4a                    dec       edx
  004a5b13  81e200010000          and       edx,100h
  004a5b19  83c280                add       edx,0FFFFFF80h
  004a5b1c  8bc2                  mov       eax,edx
  004a5b1e  66014632              add       [esi+32h],ax
  004a5b22  e902010000            jmp       004A5C29h
  004a5b27  66397e18              cmp       [esi+18h],di
  004a5b2b  740a                  je        short 004A5B37h
  004a5b2d  0d00000001            or        eax,1000000h
  004a5b32  897e18                mov       [esi+18h],edi
  004a5b35  8906                  mov       [esi],eax

LAB_004a5b37:
  004a5b37  0fbf4610              movsx     eax,word [esi+10h]
  004a5b3b  39442410              cmp       [esp+10h],eax
  004a5b3f  7d09                  jge       short 004A5B4Ah
  004a5b41  c6464300              mov       byte [esi+43h],0
  004a5b45  e9df000000            jmp       004A5C29h

LAB_004a5b4a:
  004a5b4a  8a4643                mov       al,[esi+43h]
  004a5b4d  84c0                  test      al,al
  004a5b4f  7405                  je        short 004A5B56h
  004a5b51  fec8                  dec       al
  004a5b53  884643                mov       [esi+43h],al

LAB_004a5b56:
  004a5b56  8b44243c              mov       eax,[esp+3Ch]
  004a5b5a  8d8880000000          lea       ecx,[eax+80h]
  004a5b60  81e1ff0f0000          and       ecx,0FFFh
  004a5b66  81f900010000          cmp       ecx,100h
  004a5b6c  7e1b                  jle       short 004A5B89h
  004a5b6e  25ff0f0000            and       eax,0FFFh
  004a5b73  33d2                  xor       edx,edx
  004a5b75  3d00080000            cmp       eax,800h
  004a5b7a  0f9dc2                setge     dl
  004a5b7d  4a                    dec       edx
  004a5b7e  81e200010000          and       edx,100h
  004a5b84  83c280                add       edx,0FFFFFF80h
  004a5b87  8bc2                  mov       eax,edx

LAB_004a5b89:
  004a5b89  66014632              add       [esi+32h],ax
  004a5b8d  c7465c00400100        mov       dword [esi+5Ch],14000h
  004a5b94  e990000000            jmp       004A5C29h
  004a5b99  8a4e43                mov       cl,[esi+43h]
  004a5b9c  84c9                  test      cl,cl
  004a5b9e  7405                  je        short 004A5BA5h
  004a5ba0  fec9                  dec       cl
  004a5ba2  884e43                mov       [esi+43h],cl

LAB_004a5ba5:
  004a5ba5  66397e18              cmp       [esi+18h],di
  004a5ba9  740a                  je        short 004A5BB5h
  004a5bab  0d00000001            or        eax,1000000h
  004a5bb0  897e18                mov       [esi+18h],edi
  004a5bb3  8906                  mov       [esi],eax

LAB_004a5bb5:
  004a5bb5  0fbf4610              movsx     eax,word [esi+10h]
  004a5bb9  50                    push      eax
  004a5bba  c7465c00000100        mov       dword [esi+5Ch],10000h
  004a5bc1  e856fe0900            call      00545A1Ch
  004a5bc6  0faf465c              imul      eax,[esi+5Ch]
  004a5bca  0fbf4e10              movsx     ecx,word [esi+10h]
  004a5bce  8b5e1c                mov       ebx,[esi+1Ch]
  004a5bd1  51                    push      ecx
  004a5bd2  c1f80c                sar       eax,0Ch
  004a5bd5  03d8                  add       ebx,eax
  004a5bd7  895e1c                mov       [esi+1Ch],ebx
  004a5bda  e837fe0900            call      00545A16h
  004a5bdf  0faf465c              imul      eax,[esi+5Ch]
  004a5be3  8b6e24                mov       ebp,[esi+24h]
  004a5be6  c7465c00000000        mov       dword [esi+5Ch],0
  004a5bed  c1f80c                sar       eax,0Ch
  004a5bf0  03e8                  add       ebp,eax
  004a5bf2  8b44243c              mov       eax,[esp+3Ch]
  004a5bf6  896e24                mov       [esi+24h],ebp
  004a5bf9  8d5060                lea       edx,[eax+60h]
  004a5bfc  81e2ff0f0000          and       edx,0FFFh
  004a5c02  81fac0000000          cmp       edx,0C0h
  004a5c08  7e1b                  jle       short 004A5C25h
  004a5c0a  25ff0f0000            and       eax,0FFFh
  004a5c0f  33c9                  xor       ecx,ecx
  004a5c11  3d00080000            cmp       eax,800h
  004a5c16  0f9dc1                setge     cl
  004a5c19  49                    dec       ecx
  004a5c1a  81e1c0000000          and       ecx,0C0h
  004a5c20  83c1a0                add       ecx,0FFFFFFA0h
  004a5c23  8bc1                  mov       eax,ecx

LAB_004a5c25:
  004a5c25  66014632              add       [esi+32h],ax

LAB_004a5c29:
  004a5c29  66397e18              cmp       [esi+18h],di
  004a5c2d  7528                  jne       short 004A5C57h
  004a5c2f  668b461a              mov       ax,[esi+1Ah]
  004a5c33  663d0600              cmp       ax,6
  004a5c37  7406                  je        short 004A5C3Fh
  004a5c39  663d1200              cmp       ax,12h
  004a5c3d  7518                  jne       short 004A5C57h

LAB_004a5c3f:
  004a5c3f  8d561c                lea       edx,[esi+1Ch]
  004a5c42  52                    push      edx
  004a5c43  6800180000            push      1800h
  004a5c48  6800020000            push      200h
  004a5c4d  6a35                  push      35h

LAB_004a5c4f:
  004a5c4f  e8cc7e0900            call      0053DB20h
  004a5c54  83c410                add       esp,10h

LAB_004a5c57:
  004a5c57  8b442414              mov       eax,[esp+14h]
  004a5c5b  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  004a5c64  50                    push      eax
  004a5c65  56                    push      esi
  004a5c66  e8f524feff            call      00488160h
  004a5c6b  83c408                add       esp,8
  004a5c6e  56                    push      esi
  004a5c6f  e8ccdc0800            call      00533940h
  004a5c74  83c404                add       esp,4
  004a5c77  56                    push      esi
  004a5c78  e8739e0300            call      004DFAF0h
  004a5c7d  8b4c2434              mov       ecx,[esp+34h]
  004a5c81  83c404                add       esp,4
  004a5c84  51                    push      ecx
  004a5c85  56                    push      esi
  004a5c86  e8e5da0300            call      004E3770h
  004a5c8b  8b44241c              mov       eax,[esp+1Ch]
  004a5c8f  83c408                add       esp,8
  004a5c92  85c0                  test      eax,eax
  004a5c94  0f8449020000          je        near 004A5EE3h
  004a5c9a  f70617080000          test      dword [esi],817h
  004a5ca0  0f853d020000          jne       near 004A5EE3h
  004a5ca6  8a4643                mov       al,[esi+43h]
  004a5ca9  84c0                  test      al,al
  004a5cab  0f8532020000          jne       near 004A5EE3h
  004a5cb1  8b466c                mov       eax,[esi+6Ch]
  004a5cb4  8b5e1c                mov       ebx,[esi+1Ch]
  004a5cb7  8b4e24                mov       ecx,[esi+24h]
  004a5cba  8b781c                mov       edi,[eax+1Ch]
  004a5cbd  8b6824                mov       ebp,[eax+24h]
  004a5cc0  2bfb                  sub       edi,ebx
  004a5cc2  2be9                  sub       ebp,ecx
  004a5cc4  c1ff0c                sar       edi,0Ch
  004a5cc7  c1fd0c                sar       ebp,0Ch
  004a5cca  8bd7                  mov       edx,edi
  004a5ccc  8bc5                  mov       eax,ebp
  004a5cce  0fafd7                imul      edx,edi
  004a5cd1  0fafc5                imul      eax,ebp
  004a5cd4  03d0                  add       edx,eax
  004a5cd6  52                    push      edx
  004a5cd7  e834fd0900            call      00545A10h
  004a5cdc  55                    push      ebp
  004a5cdd  57                    push      edi
  004a5cde  89442418              mov       [esp+18h],eax
  004a5ce2  e83bfd0900            call      00545A22h
  004a5ce7  668b4e32              mov       cx,[esi+32h]
  004a5ceb  2bc1                  sub       eax,ecx
  004a5ced  25ff0f0000            and       eax,0FFFh
  004a5cf2  8944243c              mov       [esp+3Ch],eax
  004a5cf6  8b442410              mov       eax,[esp+10h]
  004a5cfa  3d2c010000            cmp       eax,12Ch
  004a5cff  0f8d8f000000          jge       near 004A5D94h
  004a5d05  e836140a00            call      00547140h
  004a5d0a  99                    cdq
  004a5d0b  b964000000            mov       ecx,64h
  004a5d10  f7f9                  idiv      ecx
  004a5d12  83fa32                cmp       edx,32h
  004a5d15  7d4f                  jge       short 004A5D66h
  004a5d17  8b54243c              mov       edx,[esp+3Ch]
  004a5d1b  81c200010000          add       edx,100h
  004a5d21  81e2ff0f0000          and       edx,0FFFh
  004a5d27  81fa00020000          cmp       edx,200h
  004a5d2d  0f8faf000000          jg        near 004A5DE2h
  004a5d33  817c2410c8000000      cmp       dword [esp+10h],0C8h
  004a5d3b  0f8da1000000          jge       near 004A5DE2h
  004a5d41  8b06                  mov       eax,[esi]
  004a5d43  c7461803000000        mov       dword [esi+18h],3
  004a5d4a  25fffffffe            and       eax,0FEFFFFFFh
  004a5d4f  c6464200              mov       byte [esi+42h],0
  004a5d53  0c02                  or        al,2
  004a5d55  c7465c00000000        mov       dword [esi+5Ch],0
  004a5d5c  8906                  mov       [esi],eax
  004a5d5e  5f                    pop       edi
  004a5d5f  5e                    pop       esi
  004a5d60  5d                    pop       ebp
  004a5d61  5b                    pop       ebx
  004a5d62  83c428                add       esp,28h
  004a5d65  c3                    ret

LAB_004a5d66:
  004a5d66  83fa4b                cmp       edx,4Bh
  004a5d69  0f8d11010000          jge       near 004A5E80h
  004a5d6f  c6464202              mov       byte [esi+42h],2
  004a5d73  e8c8130a00            call      00547140h
  004a5d78  99                    cdq
  004a5d79  b90a000000            mov       ecx,0Ah
  004a5d7e  66c74610c800          mov       word [esi+10h],0C8h
  004a5d84  f7f9                  idiv      ecx
  004a5d86  80c214                add       dl,14h
  004a5d89  885643                mov       [esi+43h],dl
  004a5d8c  5f                    pop       edi
  004a5d8d  5e                    pop       esi
  004a5d8e  5d                    pop       ebp
  004a5d8f  5b                    pop       ebx
  004a5d90  83c428                add       esp,28h
  004a5d93  c3                    ret

LAB_004a5d94:
  004a5d94  e8a7130a00            call      00547140h
  004a5d99  99                    cdq
  004a5d9a  b964000000            mov       ecx,64h
  004a5d9f  f7f9                  idiv      ecx
  004a5da1  83fa1e                cmp       edx,1Eh
  004a5da4  7d4c                  jge       short 004A5DF2h
  004a5da6  8b54243c              mov       edx,[esp+3Ch]
  004a5daa  81c200020000          add       edx,200h
  004a5db0  81e2ff0f0000          and       edx,0FFFh
  004a5db6  81fa00040000          cmp       edx,400h
  004a5dbc  7d24                  jge       short 004A5DE2h
  004a5dbe  8b06                  mov       eax,[esi]
  004a5dc0  c7461804000000        mov       dword [esi+18h],4
  004a5dc7  25fffffffe            and       eax,0FEFFFFFFh
  004a5dcc  c6464201              mov       byte [esi+42h],1
  004a5dd0  0c02                  or        al,2
  004a5dd2  66c746100200          mov       word [esi+10h],2
  004a5dd8  8906                  mov       [esi],eax
  004a5dda  5f                    pop       edi
  004a5ddb  5e                    pop       esi
  004a5ddc  5d                    pop       ebp
  004a5ddd  5b                    pop       ebx
  004a5dde  83c428                add       esp,28h
  004a5de1  c3                    ret

LAB_004a5de2:
  004a5de2  c6464201              mov       byte [esi+42h],1
  004a5de6  c6464301              mov       byte [esi+43h],1
  004a5dea  5f                    pop       edi
  004a5deb  5e                    pop       esi
  004a5dec  5d                    pop       ebp
  004a5ded  5b                    pop       ebx
  004a5dee  83c428                add       esp,28h
  004a5df1  c3                    ret

LAB_004a5df2:
  004a5df2  83fa32                cmp       edx,32h
  004a5df5  7d24                  jge       short 004A5E1Bh
  004a5df7  8b0e                  mov       ecx,[esi]
  004a5df9  c7461805000000        mov       dword [esi+18h],5
  004a5e00  81e1fffffffe          and       ecx,0FEFFFFFFh
  004a5e06  c6464202              mov       byte [esi+42h],2
  004a5e0a  83c902                or        ecx,2
  004a5e0d  c6464302              mov       byte [esi+43h],2
  004a5e11  890e                  mov       [esi],ecx
  004a5e13  5f                    pop       edi
  004a5e14  5e                    pop       esi
  004a5e15  5d                    pop       ebp
  004a5e16  5b                    pop       ebx
  004a5e17  83c428                add       esp,28h
  004a5e1a  c3                    ret

LAB_004a5e1b:
  004a5e1b  83fa46                cmp       edx,46h
  004a5e1e  7d5b                  jge       short 004A5E7Bh
  004a5e20  8b54243c              mov       edx,[esp+3Ch]
  004a5e24  81c200020000          add       edx,200h
  004a5e2a  81e2ff0f0000          and       edx,0FFFh
  004a5e30  81fa00040000          cmp       edx,400h
  004a5e36  7d1e                  jge       short 004A5E56h
  004a5e38  8b06                  mov       eax,[esi]
  004a5e3a  c7461806000000        mov       dword [esi+18h],6
  004a5e41  0d04080001            or        eax,1000804h
  004a5e46  66c746100300          mov       word [esi+10h],3
  004a5e4c  8906                  mov       [esi],eax
  004a5e4e  5f                    pop       edi
  004a5e4f  5e                    pop       esi
  004a5e50  5d                    pop       ebp
  004a5e51  5b                    pop       ebx
  004a5e52  83c428                add       esp,28h
  004a5e55  c3                    ret

LAB_004a5e56:
  004a5e56  c6464202              mov       byte [esi+42h],2
  004a5e5a  e8e1120a00            call      00547140h
  004a5e5f  99                    cdq
  004a5e60  b90a000000            mov       ecx,0Ah
  004a5e65  66c74610c800          mov       word [esi+10h],0C8h
  004a5e6b  f7f9                  idiv      ecx
  004a5e6d  80c214                add       dl,14h
  004a5e70  885643                mov       [esi+43h],dl
  004a5e73  5f                    pop       edi
  004a5e74  5e                    pop       esi
  004a5e75  5d                    pop       ebp
  004a5e76  5b                    pop       ebx
  004a5e77  83c428                add       esp,28h
  004a5e7a  c3                    ret

LAB_004a5e7b:
  004a5e7b  83fa5a                cmp       edx,5Ah
  004a5e7e  7d4c                  jge       short 004A5ECCh

LAB_004a5e80:
  004a5e80  c6464203              mov       byte [esi+42h],3
  004a5e84  e8b7120a00            call      00547140h
  004a5e89  99                    cdq
  004a5e8a  b90a000000            mov       ecx,0Ah
  004a5e8f  f7f9                  idiv      ecx
  004a5e91  80c214                add       dl,14h
  004a5e94  885643                mov       [esi+43h],dl
  004a5e97  8b542418              mov       edx,[esp+18h]
  004a5e9b  8dba80000000          lea       edi,[edx+80h]
  004a5ea1  c1e704                shl       edi,4
  004a5ea4  e897120a00            call      00547140h
  004a5ea9  99                    cdq
  004a5eaa  f7ff                  idiv      edi
  004a5eac  d1ff                  sar       edi,1
  004a5eae  2bd7                  sub       edx,edi
  004a5eb0  8b7c243c              mov       edi,[esp+3Ch]
  004a5eb4  03d7                  add       edx,edi
  004a5eb6  66035632              add       dx,[esi+32h]
  004a5eba  81c200080000          add       edx,800h
  004a5ec0  66895610              mov       [esi+10h],dx
  004a5ec4  5f                    pop       edi
  004a5ec5  5e                    pop       esi
  004a5ec6  5d                    pop       ebp
  004a5ec7  5b                    pop       ebx
  004a5ec8  83c428                add       esp,28h
  004a5ecb  c3                    ret

LAB_004a5ecc:
  004a5ecc  c6464200              mov       byte [esi+42h],0
  004a5ed0  e86b120a00            call      00547140h
  004a5ed5  99                    cdq
  004a5ed6  b90a000000            mov       ecx,0Ah
  004a5edb  f7f9                  idiv      ecx
  004a5edd  80c214                add       dl,14h
  004a5ee0  885643                mov       [esi+43h],dl

LAB_004a5ee3:
  004a5ee3  5f                    pop       edi
  004a5ee4  5e                    pop       esi
  004a5ee5  5d                    pop       ebp
  004a5ee6  5b                    pop       ebx
  004a5ee7  83c428                add       esp,28h
  004a5eea  c3                    ret

50 cible(s) de saut interne : 0x004a5563, 0x004a55be, 0x004a5600, 0x004a5618, 0x004a564a, 0x004a5668, 0x004a568c, 0x004a56ee, 0x004a56fd, 0x004a5726, 0x004a57c5, 0x004a5814, 0x004a5839, 0x004a585a, 0x004a589c, 0x004a58f1, 0x004a58fe, 0x004a5906, 0x004a592e, 0x004a5978, 0x004a59c0, 0x004a59e8, 0x004a59f8, 0x004a5a6a, 0x004a5a99, 0x004a5aac, 0x004a5ab8, 0x004a5ad0, 0x004a5afc, 0x004a5b37, 0x004a5b4a, 0x004a5b56, 0x004a5b89, 0x004a5ba5, 0x004a5bb5, 0x004a5c25, 0x004a5c29, 0x004a5c3f, 0x004a5c4f, 0x004a5c57, 0x004a5d66, 0x004a5d94, 0x004a5de2, 0x004a5df2, 0x004a5e1b, 0x004a5e56, 0x004a5e7b, 0x004a5e80, 0x004a5ecc, 0x004a5ee3
