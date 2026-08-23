; Les gestionnaires d'opcode de l'interpreteur de script, table PTR_LAB_00563298.
; 39 entrees (0x00563298 .. +0x98). Les 20 ci-dessous sont ABSENTES de Blaze.c :
; Ghidra a rendu la table 'PTR_LAB_', donc il n'a pas reconnu ces corps comme des fonctions.
; Relevees le 2026-08-21 avec OpenForesia.Tools disasm.

;
; 2026-08-23 : la fenetre de 96 octets du premier relevé COUPAIT plusieurs corps en plein milieu
; (FUN_00515b40 en fait 122). Un dump tronqué se reconnait a sa derniere ligne, "(bad)" — la reprendre
; avec `disasm <va> <n>` plus large avant de transcrire, sans quoi on transcrit une fonction sans
; sa fin. FUN_00515b40 est reprise ici sur 128 octets.
