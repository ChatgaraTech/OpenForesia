\# Le hardware breakpoint sur écriture de 0x00560758 s'est déclenché à :



005445B2 | F3:A5 | rep movsd



Registres au break :

EAX = 00C4F200

EBX = 00560728

ECX = 00000222

EDX = 0000000C

EBP = 00C4F2D0

ESP = 0019F510

ESI = 0019F570

EDI = 00560768

EIP = 005445B2



Le champ surveillé est donc :

0x00560758 = EBX + 0x30



Instructions immédiatement avant la copie :



0054458F | mov al,byte ptr \[ebx+8C7]

00544595 | mov dword ptr \[ebx+8AC],ebp

0054459B | and al,FE

0054459D | mov byte ptr \[ebx+8C7],al

005445A3 | mov ebp,\[esp+1C]

005445A7 | mov ecx,232

005445AC | lea esi,\[esp+20]

005445B0 | mov edi,ebx

005445B2 | rep movsd



Interprétation :

la routine copie 0x232 DWORD, soit 0x8C8 octets, depuis un bloc local à ESP+0x20 vers la structure globale à 0x00560728.

L'écriture de +0x30 est donc effectuée dans le cadre de cette copie globale de structure, et non nécessairement par une affectation isolée du champ.



Après la copie :

005445B4 | movzx bx,byte ptr \[ebp]

…



\# A faire

Retrouve 0x005445B2 dans le listing/Ghidra, identifie la fonction englobante et détermine d’où vient le contenu situé à \[ESP+0x20 + 0x30] 

juste avant le rep movsd. C'est cette valeur source qui devient le champ +0x30 de la structure destination.

