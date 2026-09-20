  00485450  83ec18                sub       esp,18h
  00485453  53                    push      ebx
  00485454  55                    push      ebp
  00485455  56                    push      esi
  00485456  8b742428              mov       esi,[esp+28h]
  0048545a  33c0                  xor       eax,eax
  0048545c  57                    push      edi
  0048545d  66394610              cmp       [esi+10h],ax
  00485461  6689442410            mov       [esp+10h],ax
  00485466  66c744241238ff        mov       word [esp+12h],0FF38h
  0048546d  66c7442414c800        mov       word [esp+14h],0C8h
  00485474  66c74424169600        mov       word [esp+16h],96h
  0048547b  6689442418            mov       [esp+18h],ax
  00485480  66c744241a9001        mov       word [esp+1Ah],190h
  00485487  7555                  jne       short 004854DEh
  00485489  8b4e6c                mov       ecx,[esi+6Ch]
  0048548c  8d561c                lea       edx,[esi+1Ch]
  0048548f  52                    push      edx
  00485490  894e74                mov       [esi+74h],ecx
  00485493  894670                mov       [esi+70h],eax
  00485496  66c746120300          mov       word [esi+12h],3
  0048549c  c7462800200020        mov       dword [esi+28h],20002000h
  004854a3  66c746140040          mov       word [esi+14h],4000h
  004854a9  66c746160200          mov       word [esi+16h],2
  004854af  c7463860606001        mov       dword [esi+38h],1606060h
  004854b6  894644                mov       [esi+44h],eax
  004854b9  894648                mov       [esi+48h],eax
  004854bc  89464c                mov       [esi+4Ch],eax
  004854bf  66c7461a0700          mov       word [esi+1Ah],7
  004854c5  6848275500            push      552748h
  004854ca  c705ace05d000a000000  mov       dword [5DE0ACh],0Ah
  004854d4  e807870b00            call      0053DBE0h
  004854d9  83c408                add       esp,8
  004854dc  eb12                  jmp       short 004854F0h
  004854de  a1ace05d00            mov       eax,[5DE0ACh]
  004854e3  8bc8                  mov       ecx,eax
  004854e5  c1f902                sar       ecx,2
  004854e8  2bc8                  sub       ecx,eax
  004854ea  890dace05d00          mov       [5DE0ACh],ecx
  004854f0  bb04000000            mov       ebx,4
  004854f5  e8461c0c00            call      00547140h
  004854fa  8bf8                  mov       edi,eax
  004854fc  e83f1c0c00            call      00547140h
  00485501  99                    cdq
  00485502  b9c8000000            mov       ecx,0C8h
  00485507  57                    push      edi
  00485508  f7f9                  idiv      ecx
  0048550a  8bea                  mov       ebp,edx
  0048550c  e80b050c00            call      00545A1Ch
  00485511  0fafc5                imul      eax,ebp
  00485514  8b4e1c                mov       ecx,[esi+1Ch]
  00485517  8b5620                mov       edx,[esi+20h]
  0048551a  03c1                  add       eax,ecx
  0048551c  57                    push      edi
  0048551d  89442420              mov       [esp+20h],eax
  00485521  89542424              mov       [esp+24h],edx
  00485525  e8ec040c00            call      00545A16h
  0048552a  0fafc5                imul      eax,ebp
  0048552d  8b6e24                mov       ebp,[esi+24h]
  00485530  57                    push      edi
  00485531  03c5                  add       eax,ebp
  00485533  89442428              mov       [esp+28h],eax
  00485537  e8e0040c00            call      00545A1Ch
  0048553c  8d0480                lea       eax,[eax+eax*4]
  0048553f  57                    push      edi
  00485540  c1e003                shl       eax,3
  00485543  894650                mov       [esi+50h],eax
  00485546  c7465400e00100        mov       dword [esi+54h],1E000h
  0048554d  e8c4040c00            call      00545A16h
  00485552  6a03                  push      3
  00485554  6a00                  push      0
  00485556  8d0c80                lea       ecx,[eax+eax*4]
  00485559  68be000000            push      0BEh
  0048555e  8d542428              lea       edx,[esp+28h]
  00485562  68d0195500            push      5519D0h
  00485567  c1e103                shl       ecx,3
  0048556a  52                    push      edx
  0048556b  56                    push      esi
  0048556c  894e58                mov       [esi+58h],ecx
  0048556f  e8cc56ffff            call      0047AC40h
  00485574  83c418                add       esp,18h
  00485577  4b                    dec       ebx
  00485578  0f8577ffffff          jne       near 004854F5h
  0048557e  8d442410              lea       eax,[esp+10h]
  00485582  50                    push      eax
  00485583  56                    push      esi
  00485584  e817f7feff            call      00474CA0h
  00485589  8bf8                  mov       edi,eax
  0048558b  83c408                add       esp,8
  0048558e  85ff                  test      edi,edi
  00485590  7444                  je        short 004855D6h
  00485592  8b4e74                mov       ecx,[esi+74h]
  00485595  57                    push      edi
  00485596  51                    push      ecx
  00485597  e8e4450b00            call      00539B80h
  0048559c  83c408                add       esp,8
  0048559f  e89c1b0c00            call      00547140h
  004855a4  99                    cdq
  004855a5  b964000000            mov       ecx,64h
  004855aa  f7f9                  idiv      ecx
  004855ac  83fa1e                cmp       edx,1Eh
  004855af  7d16                  jge       short 004855C7h
  004855b1  8b5674                mov       edx,[esi+74h]
  004855b4  6a00                  push      0
  004855b6  57                    push      edi
  004855b7  c6420707              mov       byte [edx+7],7
  004855bb  8b4674                mov       eax,[esi+74h]
  004855be  50                    push      eax
  004855bf  e85c6e0b00            call      0053C420h
  004855c4  83c40c                add       esp,0Ch
  004855c7  8b07                  mov       eax,[edi]
  004855c9  66c787960000000800    mov       word [edi+96h],8
  004855d2  0c40                  or        al,40h
  004855d4  8907                  mov       [edi],eax
  004855d6  66ff4610              inc       word [esi+10h]
  004855da  66837e100a            cmp       word [esi+10h],0Ah
  004855df  7c06                  jl        short 004855E7h
  004855e1  c70600000000          mov       dword [esi],0
  004855e7  5f                    pop       edi
  004855e8  5e                    pop       esi
  004855e9  5d                    pop       ebp
  004855ea  5b                    pop       ebx
  004855eb  83c418                add       esp,18h
  004855ee  c3                    ret
