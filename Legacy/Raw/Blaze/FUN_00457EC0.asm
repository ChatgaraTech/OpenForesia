Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00457ec0  section .text  offset fichier 0x572c0  256 octets

  00457ec0  53                    push      ebx
  00457ec1  56                    push      esi
  00457ec2  8b74240c              mov       esi,[esp+0Ch]
  00457ec6  bb01000000            mov       ebx,1
  00457ecb  66837e1000            cmp       word [esi+10h],0
  00457ed0  7517                  jne       short 00457EE9h
  00457ed2  8b4620                mov       eax,[esi+20h]
  00457ed5  66895e10              mov       [esi+10h],bx
  00457ed9  c1f80c                sar       eax,0Ch
  00457edc  66894612              mov       [esi+12h],ax
  00457ee0  66c786980000000000    mov       word [esi+98h],0

LAB_00457ee9:
  00457ee9  6800000040            push      40000000h
  00457eee  6a10                  push      10h
  00457ef0  56                    push      esi
  00457ef1  e80ad10d00            call      00535000h
  00457ef6  83c40c                add       esp,0Ch
  00457ef9  b902000000            mov       ecx,2
  00457efe  6685c0                test      ax,ax
  00457f01  740f                  je        short 00457F12h
  00457f03  66395e10              cmp       [esi+10h],bx
  00457f07  7504                  jne       short 00457F0Dh
  00457f09  66894e10              mov       [esi+10h],cx

LAB_00457f0d:
  00457f0d  6685c0                test      ax,ax
  00457f10  750c                  jne       short 00457F1Eh

LAB_00457f12:
  00457f12  66395e10              cmp       [esi+10h],bx
  00457f16  7506                  jne       short 00457F1Eh
  00457f18  66c746100300          mov       word [esi+10h],3

LAB_00457f1e:
  00457f1e  66394e10              cmp       [esi+10h],cx
  00457f22  757b                  jne       short 00457F9Fh
  00457f24  6683be9800000000      cmp       word [esi+98h],0
  00457f2c  7532                  jne       short 00457F60h
  00457f2e  8b0dcc975e00          mov       ecx,[5E97CCh]
  00457f34  6a00                  push      0
  00457f36  6a00                  push      0
  00457f38  6800100000            push      1000h
  00457f3d  8b91a8000000          mov       edx,[ecx+0A8h]
  00457f43  6800040000            push      400h
  00457f48  8b02                  mov       eax,[edx]
  00457f4a  660fb64812            movzx     cx,byte [eax+12h]
  00457f4f  51                    push      ecx
  00457f50  56                    push      esi
  00457f51  e82a010100            call      00468080h
  00457f56  83c418                add       esp,18h
  00457f59  66899e98000000        mov       [esi+98h],bx

LAB_00457f60:
  00457f60  0fbf5614              movsx     edx,word [esi+14h]
  00457f64  0fbf4612              movsx     eax,word [esi+12h]
  00457f68  0fbf4e16              movsx     ecx,word [esi+16h]
  00457f6c  2bc2                  sub       eax,edx
  00457f6e  66895e10              mov       [esi+10h],bx
  00457f72  c1e00c                shl       eax,0Ch
  00457f75  894620                mov       [esi+20h],eax
  00457f78  8bc1                  mov       eax,ecx
  00457f7a  99                    cdq
  00457f7b  83e207                and       edx,7
  00457f7e  03c2                  add       eax,edx
  00457f80  c1f803                sar       eax,3
  00457f83  663d1f00              cmp       ax,1Fh
  00457f87  7f16                  jg        short 00457F9Fh
  00457f89  0fbfc0                movsx     eax,ax
  00457f8c  8d14c500000000        lea       edx,[eax*8]
  00457f93  2bca                  sub       ecx,edx
  00457f95  8ad3                  mov       dl,bl
  00457f97  d2e2                  shl       dl,cl
  00457f99  0890e0965e00          or        [eax+5E96E0h],dl

LAB_00457f9f:
  00457f9f  66837e1003            cmp       word [esi+10h],3
  00457fa4  7517                  jne       short 00457FBDh
  00457fa6  0fbf4612              movsx     eax,word [esi+12h]
  00457faa  c1e00c                shl       eax,0Ch
  00457fad  66c786980000000000    mov       word [esi+98h],0
  00457fb6  894620                mov       [esi+20h],eax
  00457fb9  66895e10              mov       [esi+10h],bx

LAB_00457fbd:
  00457fbd  5e                    pop       esi
  00457fbe  5b                    pop       ebx
  00457fbf  c3                    ret

7 cible(s) de saut interne : 0x00457ee9, 0x00457f0d, 0x00457f12, 0x00457f1e, 0x00457f60, 0x00457f9f, 0x00457fbd
