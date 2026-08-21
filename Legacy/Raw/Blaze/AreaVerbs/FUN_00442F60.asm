C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442f60  section .text  offset fichier 0x42360  200 octets

  00442f60  8a0d01905e00          mov       cl,[5E9001h]
  00442f66  53                    push      ebx
  00442f67  f6c110                test      cl,10h
  00442f6a  0f8583000000          jne       near 00442FF3h
  00442f70  a000905e00            mov       al,[5E9000h]
  00442f75  ba01000000            mov       edx,1
  00442f7a  84c2                  test      dl,al
  00442f7c  7407                  je        short 00442F85h
  00442f7e  800d40985e0002        or        byte [5E9840h],2

LAB_00442f85:
  00442f85  a802                  test      al,2
  00442f87  b304                  mov       bl,4
  00442f89  7406                  je        short 00442F91h
  00442f8b  081d40985e00          or        [5E9840h],bl

LAB_00442f91:
  00442f91  84c3                  test      bl,al
  00442f93  7407                  je        short 00442F9Ch
  00442f95  800d40985e0008        or        byte [5E9840h],8

LAB_00442f9c:
  00442f9c  a808                  test      al,8
  00442f9e  7407                  je        short 00442FA7h
  00442fa0  800d40985e0010        or        byte [5E9840h],10h

LAB_00442fa7:
  00442fa7  a810                  test      al,10h
  00442fa9  7407                  je        short 00442FB2h
  00442fab  800d40985e0020        or        byte [5E9840h],20h

LAB_00442fb2:
  00442fb2  a820                  test      al,20h
  00442fb4  7407                  je        short 00442FBDh
  00442fb6  800d40985e0040        or        byte [5E9840h],40h

LAB_00442fbd:
  00442fbd  a840                  test      al,40h
  00442fbf  7407                  je        short 00442FC8h
  00442fc1  800d40985e0080        or        byte [5E9840h],80h

LAB_00442fc8:
  00442fc8  a880                  test      al,80h
  00442fca  7406                  je        short 00442FD2h
  00442fcc  081541985e00          or        [5E9841h],dl

LAB_00442fd2:
  00442fd2  84ca                  test      dl,cl
  00442fd4  7407                  je        short 00442FDDh
  00442fd6  800d41985e0002        or        byte [5E9841h],2

LAB_00442fdd:
  00442fdd  f6c102                test      cl,2
  00442fe0  7406                  je        short 00442FE8h
  00442fe2  081d41985e00          or        [5E9841h],bl

LAB_00442fe8:
  00442fe8  f60506905e0008        test      byte [5E9006h],8
  00442fef  7433                  je        short 00443024h
  00442ff1  eb2b                  jmp       short 0044301Eh

LAB_00442ff3:
  00442ff3  8a1d40985e00          mov       bl,[5E9840h]
  00442ff9  8a0d41985e00          mov       cl,[5E9841h]
  00442fff  a006905e00            mov       al,[5E9006h]
  00443004  ba01000000            mov       edx,1
  00443009  22da                  and       bl,dl
  0044300b  80e1f8                and       cl,0F8h
  0044300e  a808                  test      al,8
  00443010  881d40985e00          mov       [5E9840h],bl
  00443016  880d41985e00          mov       [5E9841h],cl
  0044301c  7406                  je        short 00443024h

LAB_0044301e:
  0044301e  081540985e00          or        [5E9840h],dl

LAB_00443024:
  00443024  a0fe965e              (bad)
  (octet invalide — la plage demandee deborde probablement du code)
