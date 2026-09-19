Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004453e0  section .text  offset fichier 0x447e0  160 octets

  004453e0  668b442408            mov       ax,[esp+8]
  004453e5  56                    push      esi
  004453e6  33f6                  xor       esi,esi
  004453e8  6685c0                test      ax,ax
  004453eb  7505                  jne       short 004453F2h
  004453ed  be88a95f00            mov       esi,5FA988h

LAB_004453f2:
  004453f2  663d0100              cmp       ax,1
  004453f6  7505                  jne       short 004453FDh
  004453f8  be24aa5f00            mov       esi,5FAA24h

LAB_004453fd:
  004453fd  663d0200              cmp       ax,2
  00445401  7505                  jne       short 00445408h
  00445403  bec0aa5f00            mov       esi,5FAAC0h

LAB_00445408:
  00445408  663d0300              cmp       ax,3
  0044540c  7505                  jne       short 00445413h
  0044540e  be5cab5f00            mov       esi,5FAB5Ch

LAB_00445413:
  00445413  56                    push      esi
  00445414  e827e50e00            call      00533940h
  00445419  8b8680000000          mov       eax,[esi+80h]
  0044541f  8b8e84000000          mov       ecx,[esi+84h]
  00445425  8b9688000000          mov       edx,[esi+88h]
  0044542b  a3707d5f00            mov       [5F7D70h],eax
  00445430  8b868c000000          mov       eax,[esi+8Ch]
  00445436  890d747d5f00          mov       [5F7D74h],ecx
  0044543c  8b4e1c                mov       ecx,[esi+1Ch]
  0044543f  8915787d5f00          mov       [5F7D78h],edx
  00445445  8b5620                mov       edx,[esi+20h]
  00445448  a37c7d5f00            mov       [5F7D7Ch],eax
  0044544d  8b4624                mov       eax,[esi+24h]
  00445450  83c404                add       esp,4
  00445453  890d309a5e00          mov       [5E9A30h],ecx
  00445459  668b8e90000000        mov       cx,[esi+90h]
  00445460  8915349a5e00          mov       [5E9A34h],edx
  00445466  8b567c                mov       edx,[esi+7Ch]
  00445469  a3389a5e00            mov       [5E9A38h],eax
  0044546e  66890d389b5e00        mov       [5E9B38h],cx
  00445475  8915487b5f00          mov       [5F7B48h],edx
  0044547b  33c0                  xor       eax,eax
  0044547d  5e                    pop       esi
  0044547e  c3                    ret
  0044547f  90                    nop

4 cible(s) de saut interne : 0x004453f2, 0x004453fd, 0x00445408, 0x00445413
