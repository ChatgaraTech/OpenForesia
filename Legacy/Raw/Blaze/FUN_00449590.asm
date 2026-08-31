Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449590  section .text  offset fichier 0x48990  400 octets

  00449590  56                    push      esi
  00449591  8b742408              mov       esi,[esp+8]
  00449595  57                    push      edi
  00449596  6a00                  push      0
  00449598  6a00                  push      0
  0044959a  56                    push      esi
  0044959b  e8a0eaffff            call      00448040h
  004495a0  83c40c                add       esp,0Ch
  004495a3  66837e1000            cmp       word [esi+10h],0
  004495a8  750d                  jne       short 004495B7h
  004495aa  6a00                  push      0
  004495ac  6a00                  push      0
  004495ae  56                    push      esi
  004495af  e8fce6ffff            call      00447CB0h
  004495b4  83c40c                add       esp,0Ch

LAB_004495b7:
  004495b7  8b4678                mov       eax,[esi+78h]
  004495ba  85c0                  test      eax,eax
  004495bc  7505                  jne       short 004495C3h
  004495be  b858e55d00            mov       eax,5DE558h

LAB_004495c3:
  004495c3  8b481c                mov       ecx,[eax+1Ch]
  004495c6  8d7e1c                lea       edi,[esi+1Ch]
  004495c9  66837e1000            cmp       word [esi+10h],0
  004495ce  890f                  mov       [edi],ecx
  004495d0  8b5020                mov       edx,[eax+20h]
  004495d3  895620                mov       [esi+20h],edx
  004495d6  8b4824                mov       ecx,[eax+24h]
  004495d9  894e24                mov       [esi+24h],ecx
  004495dc  668b5032              mov       dx,[eax+32h]
  004495e0  66895632              mov       [esi+32h],dx
  004495e4  7541                  jne       short 00449627h
  004495e6  6aff                  push      0FFFFFFFFh
  004495e8  6a00                  push      0
  004495ea  b800100000            mov       eax,1000h
  004495ef  68a9000000            push      0A9h
  004495f4  56                    push      esi
  004495f5  66c746100100          mov       word [esi+10h],1
  004495fb  66c746160000          mov       word [esi+16h],0
  00449601  66c7869a0000000600    mov       word [esi+9Ah],6
  0044960a  66894614              mov       [esi+14h],ax
  0044960e  894644                mov       [esi+44h],eax
  00449611  c7464846000000        mov       dword [esi+48h],46h
  00449618  c7465c2c010000        mov       dword [esi+5Ch],12Ch
  0044961f  e8bc140300            call      0047AAE0h
  00449624  83c410                add       esp,10h

LAB_00449627:
  00449627  66837e1600            cmp       word [esi+16h],0
  0044962c  7519                  jne       short 00449647h
  0044962e  6800120000            push      1200h
  00449633  6800120000            push      1200h
  00449638  686a010000            push      16Ah
  0044963d  6a30                  push      30h
  0044963f  e85c380f00            call      0053CEA0h
  00449644  83c410                add       esp,10h

LAB_00449647:
  00449647  66837e1601            cmp       word [esi+16h],1
  0044964c  7519                  jne       short 00449667h
  0044964e  6800120000            push      1200h
  00449653  6800120000            push      1200h
  00449658  6800020000            push      200h
  0044965d  6a10                  push      10h
  0044965f  e83c380f00            call      0053CEA0h
  00449664  83c410                add       esp,10h

LAB_00449667:
  00449667  668b4616              mov       ax,[esi+16h]
  0044966b  663d0200              cmp       ax,2
  0044966f  7430                  je        short 004496A1h
  00449671  663d0c00              cmp       ax,0Ch
  00449675  742a                  je        short 004496A1h
  00449677  663d1600              cmp       ax,16h
  0044967b  7424                  je        short 004496A1h
  0044967d  663d2000              cmp       ax,20h
  00449681  741e                  je        short 004496A1h
  00449683  663d2a00              cmp       ax,2Ah
  00449687  7418                  je        short 004496A1h
  00449689  663d3400              cmp       ax,34h
  0044968d  7412                  je        short 004496A1h
  0044968f  663d3e00              cmp       ax,3Eh
  00449693  740c                  je        short 004496A1h
  00449695  663d4800              cmp       ax,48h
  00449699  7406                  je        short 004496A1h
  0044969b  663d5200              cmp       ax,52h
  0044969f  7519                  jne       short 004496BAh

LAB_004496a1:
  004496a1  6800120000            push      1200h
  004496a6  6800120000            push      1200h
  004496ab  6800020000            push      200h
  004496b0  6a21                  push      21h
  004496b2  e8e9370f00            call      0053CEA0h
  004496b7  83c410                add       esp,10h

LAB_004496ba:
  004496ba  668b4616              mov       ax,[esi+16h]
  004496be  a801                  test      al,1
  004496c0  741b                  je        short 004496DDh
  004496c2  663d3c00              cmp       ax,3Ch
  004496c6  7d15                  jge       short 004496DDh
  004496c8  6aff                  push      0FFFFFFFFh
  004496ca  6a00                  push      0
  004496cc  688c000000            push      8Ch
  004496d1  6a00                  push      0
  004496d3  57                    push      edi
  004496d4  56                    push      esi
  004496d5  e866150300            call      0047AC40h
  004496da  83c418                add       esp,18h

LAB_004496dd:
  004496dd  66ff4616              inc       word [esi+16h]
  004496e1  66837e1664            cmp       word [esi+16h],64h
  004496e6  7c31                  jl        short 00449719h
  004496e8  8b4674                mov       eax,[esi+74h]
  004496eb  8a4e06                mov       cl,[esi+6]
  004496ee  6a00                  push      0
  004496f0  884806                mov       [eax+6],cl
  004496f3  8b5674                mov       edx,[esi+74h]
  004496f6  8a4607                mov       al,[esi+7]
  004496f9  884207                mov       [edx+7],al
  004496fc  8b4e78                mov       ecx,[esi+78h]
  004496ff  8b5674                mov       edx,[esi+74h]
  00449702  51                    push      ecx
  00449703  52                    push      edx
  00449704  e817000000            call      00449720h
  00449709  83c40c                add       esp,0Ch
  0044970c  6a00                  push      0
  0044970e  6a00                  push      0
  00449710  56                    push      esi
  00449711  e80ae9ffff            call      00448020h
  00449716  83c40c                add       esp,0Ch

LAB_00449719:
  00449719  5f                    pop       edi
  0044971a  5e                    pop       esi
  0044971b  c3                    ret
  0044971c  90                    nop
  0044971d  90                    nop
  0044971e  90                    nop
  0044971f  90                    nop

9 cible(s) de saut interne : 0x004495b7, 0x004495c3, 0x00449627, 0x00449647, 0x00449667, 0x004496a1, 0x004496ba, 0x004496dd, 0x00449719
