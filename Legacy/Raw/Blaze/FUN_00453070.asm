Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00453070  section .text  offset fichier 0x52470  521 octets

  00453070  51                    push      ecx
  00453071  53                    push      ebx
  00453072  55                    push      ebp
  00453073  56                    push      esi
  00453074  57                    push      edi
  00453075  33ff                  xor       edi,edi
  00453077  33f6                  xor       esi,esi
  00453079  89742410              mov       [esp+10h],esi
  0045307d  b9c0935e00            mov       ecx,5E93C0h
  00453082  ba04000000            mov       edx,4

LAB_00453087:
  00453087  8b01                  mov       eax,[ecx]
  00453089  a900000080            test      eax,80000000h
  0045308e  7408                  je        short 00453098h
  00453090  a900000150            test      eax,50010000h
  00453095  7501                  jne       short 00453098h
  00453097  46                    inc       esi

LAB_00453098:
  00453098  81c19c000000          add       ecx,9Ch
  0045309e  4a                    dec       edx
  0045309f  75e6                  jne       short 00453087h
  004530a1  89742410              mov       [esp+10h],esi
  004530a5  8b742418              mov       esi,[esp+18h]
  004530a9  bd01000000            mov       ebp,1
  004530ae  66397e10              cmp       [esi+10h],di
  004530b2  7514                  jne       short 004530C8h
  004530b4  668b4614              mov       ax,[esi+14h]
  004530b8  66896e10              mov       [esi+10h],bp
  004530bc  66897e12              mov       [esi+12h],di
  004530c0  66894616              mov       [esi+16h],ax
  004530c4  66897e14              mov       [esi+14h],di

LAB_004530c8:
  004530c8  6800000040            push      40000000h
  004530cd  6a10                  push      10h
  004530cf  56                    push      esi
  004530d0  e82b1f0e00            call      00535000h
  004530d5  83c40c                add       esp,0Ch
  004530d8  8bd8                  mov       ebx,eax
  004530da  e8913c0100            call      00466D70h
  004530df  0fbfc8                movsx     ecx,ax
  004530e2  3bd9                  cmp       ebx,ecx
  004530e4  7550                  jne       short 00453136h
  004530e6  3bdf                  cmp       ebx,edi
  004530e8  744c                  je        short 00453136h
  004530ea  66396e10              cmp       [esi+10h],bp
  004530ee  7506                  jne       short 004530F6h
  004530f0  66c746100200          mov       word [esi+10h],2

LAB_004530f6:
  004530f6  66397e14              cmp       [esi+14h],di
  004530fa  7504                  jne       short 00453100h
  004530fc  66896e14              mov       [esi+14h],bp

LAB_00453100:
  00453100  66837e1402            cmp       word [esi+14h],2
  00453105  752f                  jne       short 00453136h
  00453107  66c746140300          mov       word [esi+14h],3
  0045310d  8b15cc975e00          mov       edx,[5E97CCh]
  00453113  57                    push      edi
  00453114  57                    push      edi
  00453115  8b82a8000000          mov       eax,[edx+0A8h]
  0045311b  6800100000            push      1000h
  00453120  6800040000            push      400h
  00453125  8b08                  mov       ecx,[eax]
  00453127  660fb65128            movzx     dx,byte [ecx+28h]
  0045312c  52                    push      edx
  0045312d  56                    push      esi
  0045312e  e84d4f0100            call      00468080h
  00453133  83c418                add       esp,18h

LAB_00453136:
  00453136  6800000040            push      40000000h
  0045313b  6a20                  push      20h
  0045313d  56                    push      esi
  0045313e  e8bd1e0e00            call      00535000h
  00453143  0fbf7c241c            movsx     edi,word [esp+1Ch]
  00453148  83c40c                add       esp,0Ch
  0045314b  8be8                  mov       ebp,eax
  0045314d  e81e3c0100            call      00466D70h
  00453152  0fbfc0                movsx     eax,ax
  00453155  2bc7                  sub       eax,edi
  00453157  3bd8                  cmp       ebx,eax
  00453159  7f19                  jg        short 00453174h
  0045315b  3bef                  cmp       ebp,edi
  0045315d  7c15                  jl        short 00453174h
  0045315f  33db                  xor       ebx,ebx
  00453161  3beb                  cmp       ebp,ebx
  00453163  7411                  je        short 00453176h
  00453165  66837e1401            cmp       word [esi+14h],1
  0045316a  750a                  jne       short 00453176h
  0045316c  66c746140200          mov       word [esi+14h],2
  00453172  eb02                  jmp       short 00453176h

LAB_00453174:
  00453174  33db                  xor       ebx,ebx

LAB_00453176:
  00453176  668b4610              mov       ax,[esi+10h]
  0045317a  b902000000            mov       ecx,2
  0045317f  663bc1                cmp       ax,cx
  00453182  7409                  je        short 0045318Dh
  00453184  663bc1                cmp       ax,cx
  00453187  66895e14              mov       [esi+14h],bx
  0045318b  7535                  jne       short 004531C2h

LAB_0045318d:
  0045318d  bf03000000            mov       edi,3
  00453192  66397e14              cmp       [esi+14h],di
  00453196  752f                  jne       short 004531C7h
  00453198  66395e34              cmp       [esi+34h],bx
  0045319c  7529                  jne       short 004531C7h
  0045319e  0fbf4616              movsx     eax,word [esi+16h]
  004531a2  8d1480                lea       edx,[eax+eax*4]
  004531a5  c1e203                shl       edx,3
  004531a8  2bd0                  sub       edx,eax
  004531aa  66894c9610            mov       [esi+edx*4+10h],cx
  004531af  0fbf4616              movsx     eax,word [esi+16h]
  004531b3  8d1480                lea       edx,[eax+eax*4]
  004531b6  c1e203                shl       edx,3
  004531b9  2bd0                  sub       edx,eax
  004531bb  66897c9614            mov       [esi+edx*4+14h],di
  004531c0  eb05                  jmp       short 004531C7h

LAB_004531c2:
  004531c2  bf03000000            mov       edi,3

LAB_004531c7:
  004531c7  66394e10              cmp       [esi+10h],cx
  004531cb  ba64000000            mov       edx,64h
  004531d0  7523                  jne       short 004531F5h
  004531d2  66397e14              cmp       [esi+14h],di
  004531d6  751d                  jne       short 004531F5h
  004531d8  66015634              add       [esi+34h],dx
  004531dc  66817e342c01          cmp       word [esi+34h],12Ch
  004531e2  7e11                  jle       short 004531F5h
  004531e4  b900040000            mov       ecx,400h
  004531e9  66c746100600          mov       word [esi+10h],6
  004531ef  66894e34              mov       [esi+34h],cx
  004531f3  eb05                  jmp       short 004531FAh

LAB_004531f5:
  004531f5  b900040000            mov       ecx,400h

LAB_004531fa:
  004531fa  668b4610              mov       ax,[esi+10h]
  004531fe  663d0600              cmp       ax,6
  00453202  7518                  jne       short 0045321Ch
  00453204  66397e14              cmp       [esi+14h],di
  00453208  7512                  jne       short 0045321Ch
  0045320a  66015634              add       [esi+34h],dx
  0045320e  66394e34              cmp       [esi+34h],cx
  00453212  7e08                  jle       short 0045321Ch
  00453214  66894e34              mov       [esi+34h],cx
  00453218  66895e14              mov       [esi+14h],bx

LAB_0045321c:
  0045321c  663bc7                cmp       ax,di
  0045321f  7521                  jne       short 00453242h
  00453221  a16ce05d00            mov       eax,[5DE06Ch]
  00453226  8b5620                mov       edx,[esi+20h]
  00453229  3bc2                  cmp       eax,edx
  0045322b  7e15                  jle       short 00453242h
  0045322d  668346349c            add       word [esi+34h],0FF9Ch
  00453232  66395e34              cmp       [esi+34h],bx
  00453236  7d0a                  jge       short 00453242h
  00453238  66895e34              mov       [esi+34h],bx
  0045323c  66c746100100          mov       word [esi+10h],1

LAB_00453242:
  00453242  66837e1004            cmp       word [esi+10h],4
  00453247  b805000000            mov       eax,5
  0045324c  750c                  jne       short 0045325Ah
  0045324e  66894e34              mov       [esi+34h],cx
  00453252  66894610              mov       [esi+10h],ax
  00453256  66895e12              mov       [esi+12h],bx

LAB_0045325a:
  0045325a  66394610              cmp       [esi+10h],ax
  0045325e  7513                  jne       short 00453273h
  00453260  66ff4612              inc       word [esi+12h]
  00453264  66837e120f            cmp       word [esi+12h],0Fh
  00453269  7e08                  jle       short 00453273h
  0045326b  66897e10              mov       [esi+10h],di
  0045326f  66895e12              mov       [esi+12h],bx

LAB_00453273:
  00453273  5f                    pop       edi
  00453274  5e                    pop       esi
  00453275  5d                    pop       ebp
  00453276  5b                    pop       ebx
  00453277  59                    pop       ecx
  00453278  c3                    ret

17 cible(s) de saut interne : 0x00453087, 0x00453098, 0x004530c8, 0x004530f6, 0x00453100, 0x00453136, 0x00453174, 0x00453176, 0x0045318d, 0x004531c2, 0x004531c7, 0x004531f5, 0x004531fa, 0x0045321c, 0x00453242, 0x0045325a, 0x00453273
