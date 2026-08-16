Compte rendu x32dbg — surveillance des écritures sur 0x00560758

================================================================



OBJECTIF

\--------



Identifier les écritures réelles effectuées par Blaze.exe sur le champ / la

zone autour de 0x00560758, afin de comprendre son initialisation et son évolution

pendant le TextEngine / changement de dialogue.



Blaze.exe est exécuté sous x32dbg (x86).





CONFIGURATION DU BREAKPOINT

\---------------------------



Breakpoint matériel posé sur :



&#x20;   Adresse : 0x00560758

&#x20;   Type    : write

&#x20;   Taille  : DWORD / 4 octets



Commande utilisée :



&#x20;   bphws 560758,w,4



Le breakpoint surveille donc :



&#x20;   0x00560758

&#x20;   0x00560759

&#x20;   0x0056075A

&#x20;   0x0056075B





================================================================

1\. PREMIER TYPE DE HIT : copie globale à 0x005445B2

================================================================



Le breakpoint mord très fréquemment à :



&#x20;   005445B2 | F3:A5 | rep movsd



Registres relevés sur un de ces hits :



&#x20;   EAX = 00C4F200

&#x20;   EBX = 00560728

&#x20;   ECX = 00000222

&#x20;   EDX = 0000000C

&#x20;   EBP = 00C4F2D0

&#x20;   ESP = 0019F510

&#x20;   ESI = 0019F570

&#x20;   EDI = 00560768

&#x20;   EIP = 005445B2



&#x20;   DR0 = 00560758

&#x20;   DR7 = 000D0001



Contexte ASM autour du hit :



&#x20;   00544569 | 74 38                    | je blaze.5445A3

&#x20;   0054456B | 66:83FE 0F               | cmp si,F

&#x20;   0054456F | 74 32                    | je blaze.5445A3

&#x20;   00544571 | EB A5                    | jmp blaze.544518

&#x20;   00544573 | 66:81FE 8000             | cmp si,80

&#x20;   00544578 | 72 08                    | jb blaze.544582

&#x20;   0054457A | 66:81FE 9F00             | cmp si,9F

&#x20;   0054457F | 77 01                    | ja blaze.544582

&#x20;   00544581 | 40                       | inc eax

&#x20;   00544582 | 8078 FF 24               | cmp byte ptr ds:\[eax-1],24

&#x20;   00544586 | 75 90                    | jne blaze.544518

&#x20;   00544588 | 8A08                     | mov cl,byte ptr ds:\[eax]

&#x20;   0054458A | 880F                     | mov byte ptr ds:\[edi],cl

&#x20;   0054458C | 47                       | inc edi

&#x20;   0054458D | EB 89                    | jmp blaze.544518

&#x20;   0054458F | 8A83 C7080000            | mov al,byte ptr ds:\[ebx+8C7]

&#x20;   00544595 | 89AB AC080000            | mov dword ptr ds:\[ebx+8AC],ebp

&#x20;   0054459B | 24 FE                    | and al,FE

&#x20;   0054459D | 8883 C7080000            | mov byte ptr ds:\[ebx+8C7],al

&#x20;   005445A3 | 8B6C24 1C                | mov ebp,dword ptr ss:\[esp+1C]

&#x20;   005445A7 | B9 32020000              | mov ecx,232

&#x20;   005445AC | 8D7424 20                | lea esi,dword ptr ss:\[esp+20]

&#x20;   005445B0 | 8BFB                     | mov edi,ebx

&#x20;   005445B2 | F3:A5                    | rep movsd

&#x20;   005445B4 | 66:0FB65D 00             | movzx bx,byte ptr ss:\[ebp]

&#x20;   005445B9 | 33FF                     | xor edi,edi

&#x20;   005445BB | 45                       | inc ebp

&#x20;   005445BC | 66:3BDF                  | cmp bx,di

&#x20;   005445BF | 75 32                    | jne blaze.5445F3

&#x20;   005445C1 | 8BB424 F0080000          | mov esi,dword ptr ss:\[esp+8F0]

&#x20;   005445C8 | 897C24 10                | mov dword ptr ss:\[esp+10],edi

&#x20;   005445CC | 8A46 15                  | mov al,byte ptr ds:\[esi+15]

&#x20;   005445CF | 84C0                     | test al,al

&#x20;   005445D1 | 0F84 C3010000            | je blaze.54479A

&#x20;   005445D7 | FEC8                     | dec al

&#x20;   005445D9 | 8846 15                  | mov byte ptr ds:\[esi+15],al

&#x20;   005445DC | 25 FF000000              | and eax,FF

&#x20;   005445E1 | 8BAC86 B4080000          | mov ebp,dword ptr ds:\[esi+eax\*4+8B4]

&#x20;   005445E8 | 8D8486 B4080000          | lea eax,dword ptr ds:\[esi+eax\*4+8B4]

&#x20;   005445EF | 8938                     | mov dword ptr ds:\[eax],edi

&#x20;   005445F1 | EB C1                    | jmp blaze.5445B4

&#x20;   005445F3 | 66:83FB 20               | cmp bx,20

&#x20;   005445F7 | 73 44                    | jae blaze.54463D

&#x20;   005445F9 | 8B8424 F0080000          | mov eax,dword ptr ss:\[esp+8F0]

&#x20;   00544600 | 8BD3                     | mov edx,ebx

&#x20;   00544602 | 81E2 FFFF0000            | and edx,FFFF

&#x20;   00544608 | 50                       | push eax



Interprétation directe du code :



&#x20;   mov ecx,0x232

&#x20;   lea esi,\[esp+0x20]

&#x20;   mov edi,ebx

&#x20;   rep movsd



Donc la routine copie :



&#x20;   0x232 DWORD

&#x20;   = 0x8C8 octets



depuis :



&#x20;   ESP + 0x20



vers :



&#x20;   EBX



avec :



&#x20;   EBX = 0x00560728



La zone de destination commence donc à 0x00560728.



L'adresse surveillée :



&#x20;   0x00560758 = 0x00560728 + 0x30



est incluse dans cette structure.



Ce hit semble donc correspondre à une copie globale d'une structure d'environ

0x8C8 octets et pas nécessairement à l'initialisation individuelle du champ

+0x30.





================================================================

2\. FILTRAGE DU HIT 0x005445B2

================================================================



Le hit 0x005445B2 se produisant très souvent, j'ai ajouté une condition au

hardware breakpoint afin de continuer automatiquement lorsque l'écriture

provient de cette instruction.



Commande utilisée :



&#x20;   bphwcond 560758,eip!=5445B2



C'est important : cette commande s'est révélée très pratique pour éliminer

automatiquement les nombreuses occurrences du rep movsd tout en conservant

la surveillance des autres écritures sur le DWORD.





================================================================

3\. NOUVEAU HIT : 0x00544E5E

================================================================



Avec le filtrage ci-dessus, un nouveau breakpoint a été obtenu.



Point important d'observation runtime :



&#x20;   Ce hit s'est produit pile au moment où la boîte de dialogue devait

&#x20;   recevoir / afficher un nouveau contenu.



x32dbg s'est arrêté à :



&#x20;   00544E5E | 0C 02 | or al,2



Registres :



&#x20;   EAX = 00C4F301

&#x20;   EBX = 00560728

&#x20;   ECX = 00560728

&#x20;   EDX = 0000000F

&#x20;   EBP = 00000000

&#x20;   ESP = 0019F504

&#x20;   ESI = 0056000F

&#x20;   EDI = 0019F524

&#x20;   EIP = 00544E5E



&#x20;   DR0 = 00560758

&#x20;   DR7 = 000D0001





================================================================

4\. INSTRUCTION AYANT VRAISEMBLABLEMENT DÉCLENCHÉ LE BREAK

================================================================



L'instruction immédiatement précédente est :



&#x20;   00544E5A | C641 33 01 | mov byte ptr ds:\[ecx+33],1



ECX vaut :



&#x20;   ECX = 0x00560728



Donc cette instruction écrit à :



&#x20;   0x00560728 + 0x33

&#x20;   = 0x0056075B



Le hardware breakpoint DWORD posé à 0x00560758 surveille :



&#x20;   0x00560758 .. 0x0056075B



0x0056075B est donc le dernier octet de la zone surveillée.



L'écriture qui a vraisemblablement déclenché le breakpoint est donc :



&#x20;   \*(byte \*)(0x00560728 + 0x33) = 1;



Autrement dit :



&#x20;   \[0x0056075B] = 1





================================================================

5\. CONTEXTE ASM DU NOUVEAU HIT

================================================================



Contexte précédent :



&#x20;   00544DDD | 25 FF000000              | and eax,FF

&#x20;   00544DE2 | 2BC8                     | sub ecx,eax

&#x20;   00544DE4 | 8D148E                   | lea edx,dword ptr ds:\[esi+ecx\*4]

&#x20;   00544DE7 | 8BC8                     | mov ecx,eax

&#x20;   00544DE9 | 8915 14835E00            | mov dword ptr ds:\[5E8314],edx

&#x20;   00544DEF | 8BD6                     | mov edx,esi

&#x20;   00544DF1 | C1E1 07                  | shl ecx,7

&#x20;   00544DF4 | 2BD1                     | sub edx,ecx

&#x20;   00544DF6 | 33C9                     | xor ecx,ecx

&#x20;   00544DF8 | 81C2 28080000            | add edx,828

&#x20;   00544DFE | 8915 24835E00            | mov dword ptr ds:\[5E8324],edx

&#x20;   00544E04 | 8A4E 14                  | mov cl,byte ptr ds:\[esi+14]

&#x20;   00544E07 | 03C8                     | add ecx,eax

&#x20;   00544E09 | 8B4424 0C                | mov eax,dword ptr ss:\[esp+C]

&#x20;   00544E0D | C1E1 0B                  | shl ecx,B

&#x20;   00544E10 | 81C1 00035E00            | add ecx,blaze.5E0300

&#x20;   00544E16 | 3BC3                     | cmp eax,ebx

&#x20;   00544E18 | 890D 04835E00            | mov dword ptr ds:\[5E8304],ecx

&#x20;   00544E1E | 891D 20835E00            | mov dword ptr ds:\[5E8320],ebx

&#x20;   00544E24 | 891D FC025E00            | mov dword ptr ds:\[5E02FC],ebx

&#x20;   00544E2A | 74 06                    | je blaze.544E32

&#x20;   00544E2C | 891D 08835E00            | mov dword ptr ds:\[5E8308],ebx

&#x20;   00544E32 | 833D 08835E00 02         | cmp dword ptr ds:\[5E8308],2

&#x20;   00544E39 | 75 0A                    | jne blaze.544E45

&#x20;   00544E3B | C705 08835E00 01000000   | mov dword ptr ds:\[5E8308],1

&#x20;   00544E45 | 891D D0FF5D00            | mov dword ptr ds:\[5DFFD0],ebx

&#x20;   00544E4B | 5E                       | pop esi

&#x20;   00544E4C | 5B                       | pop ebx

&#x20;   00544E4D | C3                       | ret



Puis une nouvelle routine commence vraisemblablement à 0x00544E50 :



&#x20;   00544E4E | 90                       | nop

&#x20;   00544E4F | 90                       | nop



&#x20;   00544E50 | 8B4C24 08                | mov ecx,dword ptr ss:\[esp+8]

&#x20;   00544E54 | 8A81 C7080000            | mov al,byte ptr ds:\[ecx+8C7]

&#x20;   00544E5A | C641 33 01               | mov byte ptr ds:\[ecx+33],1

&#x20;   00544E5E | 0C 02                    | or al,2

&#x20;   00544E60 | 8881 C7080000            | mov byte ptr ds:\[ecx+8C7],al

&#x20;   00544E66 | 8B4424 04                | mov eax,dword ptr ss:\[esp+4]

&#x20;   00544E6A | 8981 AC080000            | mov dword ptr ds:\[ecx+8AC],eax

&#x20;   00544E70 | C3                       | ret



Puis :



&#x20;   00544E71 .. 00544E7F                | nop



Routine suivante à partir de 0x00544E80 :



&#x20;   00544E80 | 8B4424 08                | mov eax,dword ptr ss:\[esp+8]

&#x20;   00544E84 | 53                       | push ebx

&#x20;   00544E85 | 8B88 B0080000            | mov ecx,dword ptr ds:\[eax+8B0]

&#x20;   00544E8B | 85C9                     | test ecx,ecx

&#x20;   00544E8D | 74 2D                    | je blaze.544EBC

&#x20;   00544E8F | 8B11                     | mov edx,dword ptr ds:\[ecx]

&#x20;   00544E91 | 81E2 0000FC00            | and edx,FC0000

&#x20;   00544E97 | 81FA 00008000            | cmp edx,800000

&#x20;   00544E9D | 75 1D                    | jne blaze.544EBC

&#x20;   00544E9F | 33D2                     | xor edx,edx

&#x20;   00544EA1 | 8A51 08                  | mov dl,byte ptr ds:\[ecx+8]

&#x20;   00544EA4 | 8B0D B4876000            | mov ecx,dword ptr ds:\[MainMem]





================================================================

6\. INTERPRÉTATION À VÉRIFIER DANS GHIDRA

================================================================



Le nouveau hit semble beaucoup plus intéressant que le rep movsd.



La routine 0x00544E50..0x00544E70 semble prendre au minimum deux arguments via

la pile :



&#x20;   \[ESP+4]

&#x20;   \[ESP+8]



Elle récupère le second argument dans ECX :



&#x20;   00544E50 mov ecx,\[esp+8]



Dans ce run :



&#x20;   ECX = 0x00560728



Elle effectue ensuite trois modifications visibles sur la structure :



&#x20;   \[ECX + 0x33]  = 1



&#x20;   \[ECX + 0x8C7] |= 2



&#x20;   \[ECX + 0x8AC] = argument situé à \[ESP+4]



Donc cette petite fonction semble modifier explicitement plusieurs champs de

la même structure dont la base est 0x00560728.



Le point particulièrement intéressant pour notre problème est :



&#x20;   +0x33 = 1



qui modifie le dernier octet du DWORD surveillé à +0x30.



Comme le hit s'est produit exactement lors du changement attendu du contenu

de la boîte de dialogue, il faudrait maintenant corréler cette routine

0x00544E50 avec Ghidra et identifier :



&#x20;   - la fonction exacte ;

&#x20;   - ses callers ;

&#x20;   - la signification de ses deux arguments ;

&#x20;   - la signification du champ +0x33 ;

&#x20;   - la signification du flag 0x02 de +0x8C7 ;

&#x20;   - la valeur passée dans +0x8AC ;

&#x20;   - et son lien éventuel avec RunTextEngine / le renouvellement du contenu

&#x20;     de la boîte de dialogue.



Important :

le breakpoint initial était sur un DWORD à +0x30, mais ce nouveau hit montre

qu'il faut probablement considérer séparément les octets +0x30, +0x31,

+0x32 et +0x33. Ici, c'est précisément +0x33 qui est écrit.





================================================================

7\. CALL STACK

================================================================



Sur le nouveau hit, x32dbg indique :



&#x20;   Thread 22400 - Main Thread

&#x20;   EIP / frame visible : blaze.00544E5E



La reconstruction de Call Stack du Main Thread ne fournit pas de chaîne

Blaze exploitable au-delà de cette frame dans cet export.



Les autres entrées présentes dans le fichier brut correspondent principalement

aux threads Windows / inputhost / quartz / NVIDIA / DirectSound / DirectInput

et ne semblent pas directement pertinentes pour ce hit.



Il existe notamment un autre thread dont la stack contient blaze.0051367B,

mais ce n'est PAS le Main Thread ayant déclenché notre breakpoint ; ne pas

le confondre avec le caller de 0x00544E5E.





================================================================

8\. FICHIERS BRUTS

================================================================



Je joins également les exports x32dbg bruts obtenus pendant l'analyse.



Ils contiennent notamment les Call Stacks complètes telles qu'affichées par

x32dbg, y compris les threads système qui ont été volontairement omis du

résumé ci-dessus.



Le nouveau fichier de Call Stack contient le Main Thread arrêté à

blaze.00544E5E.



Les données ASM et registres importantes ont également été recopiées

intégralement ci-dessus afin que le raisonnement ne dépende pas uniquement

de l'interprétation de x32dbg.





================================================================

9\. COMMANDES x32dbg UTILES POUR REPRODUIRE

================================================================



Hardware breakpoint DWORD write :



&#x20;   bphws 560758,w,4



Filtrage de l'occurrence rep movsd déjà connue :



&#x20;   bphwcond 560758,eip!=5445B2



Ce filtrage est particulièrement utile : sans lui, 0x005445B2 déclenche

très fréquemment le breakpoint et oblige à faire F9 manuellement à chaque

copie de structure.





================================================================

CONCLUSION

================================================================



On a maintenant observé deux mécanismes distincts écrivant dans la zone

0x00560758..0x0056075B :



1\. 0x005445B2 — rep movsd



&#x20;  Copie globale de 0x8C8 octets depuis une structure locale vers la structure

&#x20;  basée à 0x00560728.



&#x20;  Le DWORD +0x30 est donc recopié en même temps que le reste de la structure.



2\. 0x00544E5A — mov byte ptr \[ecx+33],1



&#x20;  Écriture explicite :



&#x20;      \[0x00560728 + 0x33] = 1

&#x20;      donc \[0x0056075B] = 1



&#x20;  Cette écriture a été capturée après exclusion automatique de 0x005445B2,

&#x20;  et survient exactement au moment où le contenu de la boîte de dialogue

&#x20;  doit changer.



La deuxième piste semble donc particulièrement intéressante pour identifier

le comportement original qui nous manque.



À toi maintenant de corréler 0x00544E50..0x00544E70 avec le listing Ghidra et

ses xrefs/callers avant de demander un nouveau run x32dbg.

