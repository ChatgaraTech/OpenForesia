Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445950  section .text  offset fichier 0x44d50  142 octets

  00445950  83ec08                sub       esp,8
  00445953  56                    push      esi
  00445954  8b742410              mov       esi,[esp+10h]
  00445958  57                    push      edi
  00445959  66837e1000            cmp       word [esi+10h],0
  0044595e  7506                  jne       short 00445966h
  00445960  66c746140000          mov       word [esi+14h],0

LAB_00445966:
  00445966  8b7e6c                mov       edi,[esi+6Ch]
  00445969  85ff                  test      edi,edi
  0044596b  7505                  jne       short 00445972h
  0044596d  bf58e55d00            mov       edi,5DE558h

LAB_00445972:
  00445972  8d442414              lea       eax,[esp+14h]
  00445976  8d4c2408              lea       ecx,[esp+8]
  0044597a  50                    push      eax
  0044597b  8d542410              lea       edx,[esp+10h]
  0044597f  51                    push      ecx
  00445980  52                    push      edx
  00445981  57                    push      edi
  00445982  e8f92f0f00            call      00538980h
  00445987  8b442424              mov       eax,[esp+24h]
  0044598b  83c410                add       esp,10h
  0044598e  668b481e              mov       cx,[eax+1Eh]
  00445992  8d461c                lea       eax,[esi+1Ch]
  00445995  66894e14              mov       [esi+14h],cx
  00445999  8b571c                mov       edx,[edi+1Ch]
  0044599c  8910                  mov       [eax],edx
  0044599e  8b4f20                mov       ecx,[edi+20h]
  004459a1  894e20                mov       [esi+20h],ecx
  004459a4  8a4e10                mov       cl,[esi+10h]
  004459a7  8b5724                mov       edx,[edi+24h]
  004459aa  f6c101                test      cl,1
  004459ad  895624                mov       [esi+24h],edx
  004459b0  7415                  je        short 004459C7h
  004459b2  6aff                  push      0FFFFFFFFh
  004459b4  6a00                  push      0
  004459b6  6899000000            push      99h
  004459bb  6a00                  push      0
  004459bd  50                    push      eax
  004459be  56                    push      esi
  004459bf  e87c520300            call      0047AC40h
  004459c4  83c418                add       esp,18h

LAB_004459c7:
  004459c7  66ff4610              inc       word [esi+10h]
  004459cb  66837e1012            cmp       word [esi+10h],12h
  004459d0  7c06                  jl        short 004459D8h
  004459d2  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004459d8:
  004459d8  5f                    pop       edi
  004459d9  5e                    pop       esi
  004459da  83c408                add       esp,8
  004459dd  c3                    ret

4 cible(s) de saut interne : 0x00445966, 0x00445972, 0x004459c7, 0x004459d8
