# winsys3d.dll — Reverse Engineering Notes

Documentation des fonctions identifiées dans `winsys3d.dll`.

Les informations de ce fichier doivent provenir de l'analyse directe de la DLL avec Ghidra et, lorsque nécessaire, de l'observation de ses appels depuis Blaze.

---

## Ordinal #331

### Fonction Ghidra

`FUN_00a26220`

Adresse : `0x00A26220`
Export : ordinal `331`

### Rôle

Wrapper autour de l'API Windows `CreateFileA`.

Le premier paramètre est un pointeur vers une chaîne ANSI représentant un chemin.

Le second paramètre détermine le mode d'accès :

| Valeur | Accès                                          |
| -----: | ---------------------------------------------- |
|    `0` | `GENERIC_READ` (`0x80000000`)                  |
|    `1` | `GENERIC_WRITE` (`0x40000000`)                 |
|    `2` | `GENERIC_READ \| GENERIC_WRITE` (`0xC0000000`) |

La fonction appelle approximativement :

```c
CreateFileA(
    NormalizePath(path),
    desiredAccess,
    FILE_SHARE_READ,
    NULL,
    OPEN_EXISTING,
    FILE_ATTRIBUTE_NORMAL,
    NULL
);
```

Des call-sites de Blaze utilisent trois arguments :

```c
Ordinal_331(path, 0, 0);
```

Ghidra n'identifie cependant que deux paramètres utilisés par `FUN_00a26220`.

TODO(REVERSE): confirmer l'ABI et la convention d'appel avant de conclure que le troisième argument peut être supprimé.

### Valeur de retour

La valeur retournée par `CreateFileA` est utilisée comme un `HANDLE`.

En cas d'échec :

```text
INVALID_HANDLE_VALUE
0xFFFFFFFF
```

---

## Path normalization — `FUN_00a261e0`

`Ordinal #331` passe son premier paramètre par `FUN_00a261e0` avant l'appel à `CreateFileA`.

### Comportement

La fonction :

1. reçoit un `char*` ;
2. ignore un premier `\` s'il est présent ;
3. copie les caractères vers le buffer global `DAT_011f0ed0` ;
4. s'arrête sur `;` ou `\0` ;
5. ajoute le terminateur `\0` ;
6. retourne le buffer global.

Pseudo-code simplifié :

```c
char* NormalizePath(char* path)
{
    if (*path == '\\')
        path++;

    int i = 0;

    while (*path != '\0' && *path != ';')
        buffer[i++] = *path++;

    buffer[i] = '\0';

    return buffer;
}
```

Exemple observé :

```text
E:\  →  E:\
```

---

## Ordinal #336

### Fonction Ghidra

`FUN_00a26300`

Adresse : `0x00A26300`
Export : ordinal `336`

### Rôle

Wrapper autour de l'API Windows `CloseHandle`.

Pseudo-code :

```c
int Ordinal_336(HANDLE handle)
{
    CloseHandle(handle);
    return 0;
}
```

Confiance : **haute**.

---

## Combinaison #331 / #336 pour les lecteurs CD-ROM

Un usage identifié de ces deux fonctions consiste à tester l'ouverture d'un lecteur CD-ROM :

```c
if (GetDriveTypeA(driveRoot) == DRIVE_CDROM)
{
    HANDLE handle = Ordinal_331(driveRoot, 0, 0);

    if (handle != INVALID_HANDLE_VALUE)
    {
        Ordinal_336(handle);
        // CD-ROM accepted
    }
}
```

Dans ce contexte :

* `#331` ouvre la racine du lecteur ;
* `#336` ferme immédiatement le handle ;
* aucune donnée n'est lue depuis le handle.

Sur Windows moderne, l'ouverture de la racine telle que `E:\` par `#331` échoue.

Test observé :

```text
GetDriveTypeA("E:\") → DRIVE_CDROM
Ordinal_331("E:\", 0, 0) → INVALID_HANDLE_VALUE
```

Un appel direct à `CreateFileA` avec les paramètres utilisés par `#331` donne :

```text
INVALID_HANDLE_VALUE
GetLastError() = 3 (ERROR_PATH_NOT_FOUND)
```

### Remplacement moderne

Pour cet usage précis, la combinaison :

```c
GetDriveTypeA(driveRoot) == DRIVE_CDROM
    +
Ordinal_331(...)
    +
Ordinal_336(...)
```

peut être remplacée simplement par :

```csharp
GetDriveTypeA(driveRoot) == DRIVE_CDROM
```

Le couple `#331/#336` ne fait ici qu'un test supplémentaire d'ouverture/fermeture de la racine du lecteur.

**Important : ce remplacement concerne cet usage précis de `#331/#336`. Il ne signifie pas que tous les appels à ces ordinals peuvent être supprimés.**

---

## Ordinals à analyser

| Ordinal | Fonction Ghidra | Rôle identifié        | Confiance |
| ------- | --------------- | --------------------- | --------- |
| `#331`  | `FUN_00a26220`  | `CreateFileA` wrapper | Haute     |
| `#336`  | `FUN_00a26300`  | `CloseHandle` wrapper | Haute     |
| `#337`  | TODO            | TODO                  | Inconnue  |
| `#450`  | TODO            | TODO                  | Inconnue  |
| `#451`  | TODO            | TODO                  | Inconnue  |
| `#790`  | TODO            | TODO                  | Inconnue  |
