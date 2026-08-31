Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480600  section .text  offset fichier 0x7fa00  1048 octets

  00480600  83ec10                sub       esp,10h
  00480603  53                    push      ebx
  00480604  55                    push      ebp
  00480605  56                    push      esi
  00480606  8b742420              mov       esi,[esp+20h]
  0048060a  33ed                  xor       ebp,ebp
  0048060c  57                    push      edi
  0048060d  8b06                  mov       eax,[esi]
  0048060f  66896c2414            mov       [esp+14h],bp
  00480614  a900000002            test      eax,2000000h
  00480619  66896c2416            mov       [esp+16h],bp
  0048061e  66c74424183c00        mov       word [esp+18h],3Ch
  00480625  66c744241ac800        mov       word [esp+1Ah],0C8h
  0048062c  66896c241c            mov       [esp+1Ch],bp
  00480631  66c744241e2c01        mov       word [esp+1Eh],12Ch
  00480638  0f8453010000          je        near 00480791h
  0048063e  66837e1050            cmp       word [esi+10h],50h
  00480643  0f8d3e010000          jge       near 00480787h
  00480649  a900000040            test      eax,40000000h
  0048064e  0f8533010000          jne       near 00480787h
  00480654  8b4674                mov       eax,[esi+74h]
  00480657  f70000000040          test      dword [eax],40000000h
  0048065d  0f8524010000          jne       near 00480787h
  00480663  8b4e28                mov       ecx,[esi+28h]
  00480666  8b562c                mov       edx,[esi+2Ch]
  00480669  894c2424              mov       [esp+24h],ecx
  0048066d  89542410              mov       [esp+10h],edx
  00480671  e8ca6a0c00            call      00547140h
  00480676  8be8                  mov       ebp,eax
  00480678  e8c36a0c00            call      00547140h
  0048067d  8bf8                  mov       edi,eax
  0048067f  55                    push      ebp
  00480680  81e7ff000000          and       edi,0FFh
  00480686  8d5e1c                lea       ebx,[esi+1Ch]
  00480689  e88e530c00            call      00545A1Ch
  0048068e  0fafc7                imul      eax,edi
  00480691  8b4e44                mov       ecx,[esi+44h]
  00480694  55                    push      ebp
  00480695  03c1                  add       eax,ecx
  00480697  8903                  mov       [ebx],eax
  00480699  8b4648                mov       eax,[esi+48h]
  0048069c  894620                mov       [esi+20h],eax
  0048069f  e872530c00            call      00545A16h
  004806a4  0fafc7                imul      eax,edi
  004806a7  03464c                add       eax,[esi+4Ch]
  004806aa  894624                mov       [esi+24h],eax
  004806ad  e88e6a0c00            call      00547140h
  004806b2  8be8                  mov       ebp,eax
  004806b4  e8876a0c00            call      00547140h
  004806b9  8bf8                  mov       edi,eax
  004806bb  55                    push      ebp
  004806bc  83e71f                and       edi,1Fh
  004806bf  83c70a                add       edi,0Ah
  004806c2  e855530c00            call      00545A1Ch
  004806c7  0fafc7                imul      eax,edi
  004806ca  894650                mov       [esi+50h],eax
  004806cd  e86e6a0c00            call      00547140h
  004806d2  83e01f                and       eax,1Fh
  004806d5  55                    push      ebp
  004806d6  83c028                add       eax,28h
  004806d9  c1e00c                shl       eax,0Ch
  004806dc  894654                mov       [esi+54h],eax
  004806df  e832530c00            call      00545A16h
  004806e4  0fafc7                imul      eax,edi
  004806e7  33c9                  xor       ecx,ecx
  004806e9  6aff                  push      0FFFFFFFFh
  004806eb  8a4e0b                mov       cl,[esi+0Bh]
  004806ee  894658                mov       [esi+58h],eax
  004806f1  51                    push      ecx
  004806f2  68bf000000            push      0BFh
  004806f7  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  004806fe  c7462800200020        mov       dword [esi+28h],20002000h
  00480705  66c7462c1f00          mov       word [esi+2Ch],1Fh
  0048070b  e8306a0c00            call      00547140h
  00480710  99                    cdq
  00480711  b903000000            mov       ecx,3
  00480716  f7f9                  idiv      ecx
  00480718  8d14d5f81a5500        lea       edx,[edx*8+551AF8h]
  0048071f  52                    push      edx
  00480720  53                    push      ebx
  00480721  56                    push      esi
  00480722  e819a5ffff            call      0047AC40h
  00480727  8b44243c              mov       eax,[esp+3Ch]
  0048072b  8b4c2428              mov       ecx,[esp+28h]
  0048072f  894628                mov       [esi+28h],eax
  00480732  8a4610                mov       al,[esi+10h]
  00480735  83c418                add       esp,18h
  00480738  894e2c                mov       [esi+2Ch],ecx
  0048073b  a801                  test      al,1
  0048073d  743c                  je        short 0048077Bh
  0048073f  c7463880808000        mov       dword [esi+38h],808080h
  00480746  e8f5690c00            call      00547140h
  0048074b  33d2                  xor       edx,edx
  0048074d  6aff                  push      0FFFFFFFFh
  0048074f  8a560b                mov       dl,[esi+0Bh]
  00480752  66894632              mov       [esi+32h],ax
  00480756  52                    push      edx
  00480757  6a74                  push      74h
  00480759  56                    push      esi
  0048075a  c6460417              mov       byte [esi+4],17h
  0048075e  e87da3ffff            call      0047AAE0h
  00480763  83c410                add       esp,10h
  00480766  53                    push      ebx
  00480767  6800120000            push      1200h
  0048076c  6800040000            push      400h
  00480771  6a04                  push      4
  00480773  e8a8d30b00            call      0053DB20h
  00480778  83c410                add       esp,10h

LAB_0048077b:
  0048077b  66ff4610              inc       word [esi+10h]
  0048077f  5f                    pop       edi
  00480780  5e                    pop       esi
  00480781  5d                    pop       ebp
  00480782  5b                    pop       ebx
  00480783  83c410                add       esp,10h
  00480786  c3                    ret

LAB_00480787:
  00480787  892e                  mov       [esi],ebp
  00480789  5f                    pop       edi
  0048078a  5e                    pop       esi
  0048078b  5d                    pop       ebp
  0048078c  5b                    pop       ebx
  0048078d  83c410                add       esp,10h
  00480790  c3                    ret

LAB_00480791:
  00480791  66396e10              cmp       [esi+10h],bp
  00480795  0f8501010000          jne       near 0048089Ch
  0048079b  8b7e74                mov       edi,[esi+74h]
  0048079e  804e3606              or        byte [esi+36h],6
  004807a2  0c80                  or        al,80h
  004807a4  57                    push      edi
  004807a5  8906                  mov       [esi],eax
  004807a7  e814840b00            call      00538BC0h
  004807ac  8b5e20                mov       ebx,[esi+20h]
  004807af  24fe                  and       al,0FEh
  004807b1  c1e00b                shl       eax,0Bh
  004807b4  03d8                  add       ebx,eax
  004807b6  83c404                add       esp,4
  004807b9  895e20                mov       [esi+20h],ebx
  004807bc  0fbf5f32              movsx     ebx,word [edi+32h]
  004807c0  53                    push      ebx
  004807c1  e856520c00            call      00545A1Ch
  004807c6  8b4e1c                mov       ecx,[esi+1Ch]
  004807c9  8d0480                lea       eax,[eax+eax*4]
  004807cc  c1e002                shl       eax,2
  004807cf  03c8                  add       ecx,eax
  004807d1  53                    push      ebx
  004807d2  894e1c                mov       [esi+1Ch],ecx
  004807d5  e83c520c00            call      00545A16h
  004807da  33d2                  xor       edx,edx
  004807dc  8d0c80                lea       ecx,[eax+eax*4]
  004807df  8a560b                mov       dl,[esi+0Bh]
  004807e2  8b4624                mov       eax,[esi+24h]
  004807e5  c1e102                shl       ecx,2
  004807e8  6aff                  push      0FFFFFFFFh
  004807ea  52                    push      edx
  004807eb  03c1                  add       eax,ecx
  004807ed  6a73                  push      73h
  004807ef  56                    push      esi
  004807f0  894624                mov       [esi+24h],eax
  004807f3  896e44                mov       [esi+44h],ebp
  004807f6  896e48                mov       [esi+48h],ebp
  004807f9  896e4c                mov       [esi+4Ch],ebp
  004807fc  c7463880808000        mov       dword [esi+38h],808080h
  00480803  c7462800100010        mov       dword [esi+28h],10001000h
  0048080a  66c7462c0010          mov       word [esi+2Ch],1000h
  00480810  c6460415              mov       byte [esi+4],15h
  00480814  e8c7a2ffff            call      0047AAE0h
  00480819  83c410                add       esp,10h
  0048081c  c6460416              mov       byte [esi+4],16h
  00480820  bf1e000000            mov       edi,1Eh
  00480825  bd05000000            mov       ebp,5

LAB_0048082a:
  0048082a  53                    push      ebx
  0048082b  e8ec510c00            call      00545A1Ch
  00480830  0fafc7                imul      eax,edi
  00480833  53                    push      ebx
  00480834  894644                mov       [esi+44h],eax
  00480837  c7464800000000        mov       dword [esi+48h],0
  0048083e  e8d3510c00            call      00545A16h
  00480843  0fafc7                imul      eax,edi
  00480846  6681462c00fe          add       word [esi+2Ch],0FE00h
  0048084c  89464c                mov       [esi+4Ch],eax
  0048084f  81462800fefffd        add       dword [esi+28h],0FDFFFE00h
  00480856  e8e5680c00            call      00547140h
  0048085b  25ff010000            and       eax,1FFh
  00480860  6aff                  push      0FFFFFFFFh
  00480862  0500030000            add       eax,300h
  00480867  66014634              add       [esi+34h],ax
  0048086b  33c0                  xor       eax,eax
  0048086d  8a460b                mov       al,[esi+0Bh]
  00480870  50                    push      eax
  00480871  6a73                  push      73h
  00480873  56                    push      esi
  00480874  e867a2ffff            call      0047AAE0h
  00480879  83c410                add       esp,10h
  0048087c  83c71e                add       edi,1Eh
  0048087f  4d                    dec       ebp
  00480880  75a8                  jne       short 0048082Ah
  00480882  8d461c                lea       eax,[esi+1Ch]
  00480885  50                    push      eax
  00480886  6800120000            push      1200h
  0048088b  6800040000            push      400h
  00480890  6a04                  push      4
  00480892  e889d20b00            call      0053DB20h
  00480897  83c410                add       esp,10h
  0048089a  33ed                  xor       ebp,ebp

LAB_0048089c:
  0048089c  0fbf7e32              movsx     edi,word [esi+32h]
  004808a0  57                    push      edi
  004808a1  e876510c00            call      00545A1Ch
  004808a6  8b5e1c                mov       ebx,[esi+1Ch]
  004808a9  8d0c80                lea       ecx,[eax+eax*4]
  004808ac  c1e102                shl       ecx,2
  004808af  03d9                  add       ebx,ecx
  004808b1  57                    push      edi
  004808b2  895e1c                mov       [esi+1Ch],ebx
  004808b5  e85c510c00            call      00545A16h
  004808ba  8b7e20                mov       edi,[esi+20h]
  004808bd  8d1480                lea       edx,[eax+eax*4]
  004808c0  8b4624                mov       eax,[esi+24h]
  004808c3  56                    push      esi
  004808c4  c1e202                shl       edx,2
  004808c7  03c2                  add       eax,edx
  004808c9  894624                mov       [esi+24h],eax
  004808cc  e86f300b00            call      00533940h
  004808d1  8b06                  mov       eax,[esi]
  004808d3  83c404                add       esp,4
  004808d6  a900000040            test      eax,40000000h
  004808db  897e20                mov       [esi+20h],edi
  004808de  750c                  jne       short 004808ECh
  004808e0  f7869000000002880000  test      dword [esi+90h],8802h
  004808ea  7407                  je        short 004808F3h

LAB_004808ec:
  004808ec  0d00000002            or        eax,2000000h
  004808f1  8906                  mov       [esi],eax

LAB_004808f3:
  004808f3  66ff4610              inc       word [esi+10h]
  004808f7  668b4e10              mov       cx,[esi+10h]
  004808fb  6683f928              cmp       cx,28h
  004808ff  7c06                  jl        short 00480907h
  00480901  810e00000002          or        dword [esi],2000000h

LAB_00480907:
  00480907  8b06                  mov       eax,[esi]
  00480909  a900008000            test      eax,800000h
  0048090e  743a                  je        short 0048094Ah
  00480910  8b4e74                mov       ecx,[esi+74h]
  00480913  33d2                  xor       edx,edx
  00480915  33db                  xor       ebx,ebx
  00480917  8a5108                mov       dl,[ecx+8]
  0048091a  8bca                  mov       ecx,edx
  0048091c  8d14c9                lea       edx,[ecx+ecx*8]
  0048091f  8d1451                lea       edx,[ecx+edx*2]
  00480922  c1e10d                shl       ecx,0Dh
  00480925  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0048092c  8b15b4876000          mov       edx,[6087B4h]
  00480932  c1e304                shl       ebx,4
  00480935  8b12                  mov       edx,[edx]
  00480937  668bbb204a6000        mov       di,[ebx+604A20h]
  0048093e  6685bc11c41f0f00      test      [ecx+edx+0F1FC4h],di
  00480946  750f                  jne       short 00480957h
  00480948  eb06                  jmp       short 00480950h

LAB_0048094a:
  0048094a  6683f914              cmp       cx,14h
  0048094e  7c07                  jl        short 00480957h

LAB_00480950:
  00480950  0d00000002            or        eax,2000000h
  00480955  8906                  mov       [esi],eax

LAB_00480957:
  00480957  56                    push      esi
  00480958  e8b347ffff            call      00475110h
  0048095d  83c404                add       esp,4
  00480960  85c0                  test      eax,eax
  00480962  7406                  je        short 0048096Ah
  00480964  810e00000002          or        dword [esi],2000000h

LAB_0048096a:
  0048096a  8d442414              lea       eax,[esp+14h]
  0048096e  50                    push      eax
  0048096f  56                    push      esi
  00480970  e82b43ffff            call      00474CA0h
  00480975  8bf8                  mov       edi,eax
  00480977  83c408                add       esp,8
  0048097a  3bfd                  cmp       edi,ebp
  0048097c  7455                  je        short 004809D3h
  0048097e  8b4674                mov       eax,[esi+74h]
  00480981  33c9                  xor       ecx,ecx
  00480983  8a4e06                mov       cl,[esi+6]
  00480986  33d2                  xor       edx,edx
  00480988  8a5607                mov       dl,[esi+7]
  0048098b  55                    push      ebp
  0048098c  51                    push      ecx
  0048098d  52                    push      edx
  0048098e  57                    push      edi
  0048098f  50                    push      eax
  00480990  e81b990b00            call      0053A2B0h
  00480995  0fbf5e32              movsx     ebx,word [esi+32h]
  00480999  83c414                add       esp,14h
  0048099c  81eb00040000          sub       ebx,400h
  004809a2  53                    push      ebx
  004809a3  e874500c00            call      00545A1Ch
  004809a8  8b4f50                mov       ecx,[edi+50h]
  004809ab  53                    push      ebx
  004809ac  c1e004                shl       eax,4
  004809af  03c8                  add       ecx,eax
  004809b1  894f50                mov       [edi+50h],ecx
  004809b4  e85d500c00            call      00545A16h
  004809b9  8b4f58                mov       ecx,[edi+58h]
  004809bc  66c787960000001000    mov       word [edi+96h],10h
  004809c5  c1e004                shl       eax,4
  004809c8  03c8                  add       ecx,eax
  004809ca  8b07                  mov       eax,[edi]
  004809cc  0c40                  or        al,40h
  004809ce  894f58                mov       [edi+58h],ecx
  004809d1  8907                  mov       [edi],eax

LAB_004809d3:
  004809d3  f70600000002          test      dword [esi],2000000h
  004809d9  7435                  je        short 00480A10h
  004809db  8b4624                mov       eax,[esi+24h]
  004809de  8b4e1c                mov       ecx,[esi+1Ch]
  004809e1  8b5660                mov       edx,[esi+60h]
  004809e4  89464c                mov       [esi+4Ch],eax
  004809e7  b800140000            mov       eax,1400h
  004809ec  66896e10              mov       [esi+10h],bp
  004809f0  894e44                mov       [esi+44h],ecx
  004809f3  895648                mov       [esi+48h],edx
  004809f6  894628                mov       [esi+28h],eax
  004809f9  6689462c              mov       [esi+2Ch],ax
  004809fd  66896e34              mov       [esi+34h],bp
  00480a01  66c7461a0100          mov       word [esi+1Ah],1
  00480a07  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh

LAB_00480a10:
  00480a10  5f                    pop       edi
  00480a11  5e                    pop       esi
  00480a12  5d                    pop       ebp
  00480a13  5b                    pop       ebx
  00480a14  83c410                add       esp,10h
  00480a17  c3                    ret

14 cible(s) de saut interne : 0x0048077b, 0x00480787, 0x00480791, 0x0048082a, 0x0048089c, 0x004808ec, 0x004808f3, 0x00480907, 0x0048094a, 0x00480950, 0x00480957, 0x0048096a, 0x004809d3, 0x00480a10
