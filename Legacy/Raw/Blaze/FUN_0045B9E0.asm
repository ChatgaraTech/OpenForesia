Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045b9e0  section .text  offset fichier 0x5ade0  296 octets

  0045b9e0  53                    push      ebx
  0045b9e1  56                    push      esi
  0045b9e2  8b74240c              mov       esi,[esp+0Ch]
  0045b9e6  57                    push      edi
  0045b9e7  56                    push      esi
  0045b9e8  e8d3550000            call      00460FC0h
  0045b9ed  8b16                  mov       edx,[esi]
  0045b9ef  83c404                add       esp,4
  0045b9f2  80ce20                or        dh,20h
  0045b9f5  8916                  mov       [esi],edx
  0045b9f7  8a0d90165700          mov       cl,[571690h]
  0045b9fd  f6c120                test      cl,20h
  0045ba00  8bc2                  mov       eax,edx
  0045ba02  0f84fc000000          je        near 0045BB04h
  0045ba08  33d2                  xor       edx,edx
  0045ba0a  80e4df                and       ah,0DFh
  0045ba0d  66395610              cmp       [esi+10h],dx
  0045ba11  8906                  mov       [esi],eax
  0045ba13  7512                  jne       short 0045BA27h
  0045ba15  668b4632              mov       ax,[esi+32h]
  0045ba19  66c746100100          mov       word [esi+10h],1
  0045ba1f  66895612              mov       [esi+12h],dx
  0045ba23  66894616              mov       [esi+16h],ax

LAB_0045ba27:
  0045ba27  668b4e32              mov       cx,[esi+32h]
  0045ba2b  668b4610              mov       ax,[esi+10h]
  0045ba2f  6683c110              add       cx,10h
  0045ba33  81e1ff0f0000          and       ecx,0FFFh
  0045ba39  663d0100              cmp       ax,1
  0045ba3d  66894e32              mov       [esi+32h],cx
  0045ba41  7534                  jne       short 0045BA77h
  0045ba43  8b5e20                mov       ebx,[esi+20h]
  0045ba46  81c300600000          add       ebx,6000h
  0045ba4c  8bcb                  mov       ecx,ebx
  0045ba4e  895e20                mov       [esi+20h],ebx
  0045ba51  81f900800c00          cmp       ecx,0C8000h
  0045ba57  7e1e                  jle       short 0045BA77h
  0045ba59  c7462000800c00        mov       dword [esi+20h],0C8000h
  0045ba60  66c746100200          mov       word [esi+10h],2
  0045ba66  66895612              mov       [esi+12h],dx
  0045ba6a  66c7869a0000000800    mov       word [esi+9Ah],8
  0045ba73  5f                    pop       edi
  0045ba74  5e                    pop       esi
  0045ba75  5b                    pop       ebx
  0045ba76  c3                    ret

LAB_0045ba77:
  0045ba77  663d0200              cmp       ax,2
  0045ba7b  756f                  jne       short 0045BAECh
  0045ba7d  0fbf5616              movsx     edx,word [esi+16h]
  0045ba81  52                    push      edx
  0045ba82  e88f9f0e00            call      00545A16h
  0045ba87  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0045ba8e  0fafc1                imul      eax,ecx
  0045ba91  0fbf5616              movsx     edx,word [esi+16h]
  0045ba95  8b4e1c                mov       ecx,[esi+1Ch]
  0045ba98  668b9e9a000000        mov       bx,[esi+9Ah]
  0045ba9f  03c8                  add       ecx,eax
  0045baa1  52                    push      edx
  0045baa2  894e1c                mov       [esi+1Ch],ecx
  0045baa5  0fbffb                movsx     edi,bx
  0045baa8  e86f9f0e00            call      00545A1Ch
  0045baad  0fafc7                imul      eax,edi
  0045bab0  8b5624                mov       edx,[esi+24h]
  0045bab3  83e7fe                and       edi,0FFFFFFFEh
  0045bab6  03d0                  add       edx,eax
  0045bab8  895624                mov       [esi+24h],edx
  0045babb  8b5620                mov       edx,[esi+20h]
  0045babe  c1e70b                shl       edi,0Bh
  0045bac1  03d7                  add       edx,edi
  0045bac3  66ff4612              inc       word [esi+12h]
  0045bac7  66837e122d            cmp       word [esi+12h],2Dh
  0045bacc  895620                mov       [esi+20h],edx
  0045bacf  7c1b                  jl        short 0045BAECh
  0045bad1  f7db                  neg       ebx
  0045bad3  66899e9a000000        mov       [esi+9Ah],bx
  0045bada  66c74612d3ff          mov       word [esi+12h],0FFD3h
  0045bae0  66c746100300          mov       word [esi+10h],3
  0045bae6  66c746140000          mov       word [esi+14h],0

LAB_0045baec:
  0045baec  66837e1003            cmp       word [esi+10h],3
  0045baf1  7511                  jne       short 0045BB04h
  0045baf3  66ff4614              inc       word [esi+14h]
  0045baf7  66837e1428            cmp       word [esi+14h],28h
  0045bafc  7e06                  jle       short 0045BB04h
  0045bafe  66c746100200          mov       word [esi+10h],2

LAB_0045bb04:
  0045bb04  5f                    pop       edi
  0045bb05  5e                    pop       esi
  0045bb06  5b                    pop       ebx
  0045bb07  c3                    ret

4 cible(s) de saut interne : 0x0045ba27, 0x0045ba77, 0x0045baec, 0x0045bb04
