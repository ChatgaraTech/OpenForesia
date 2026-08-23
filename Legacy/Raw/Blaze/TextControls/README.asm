; Les gestionnaires de codes de controle de texte, tables PTR_FUN_0056e728 (rendu) et
; PTR_LAB_0056e870 (balayage). 32 entrees chacune. Ghidra n'a emis aucun de ces corps :
; il n'y accede que par les tables, qu'il a nommees PTR_FUN_/PTR_LAB_.
;
; Astuce de lecture : la table de BALAYAGE donne la taille de l'operande sans qu'on lise le corps
; de rendu. Ses talons 0x00543940/50/60/70 sont Advance0/1/2/3 — un code dont le balayage pointe
; 0x00543960 a deux octets d'operande, et le corps de rendu doit consommer les memes deux.
