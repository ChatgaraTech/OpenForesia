// GENERE par `OpenForesia.Tools extraire-donnees` depuis Legacy/Binaries — NE PAS EDITER A LA MAIN.
// Voir OpenForesia.Tools/ExtractionDonnees.cs. Chaque valeur a ete reencodee et comparee octet par
// octet a la DLL de 1998 lors de la generation.
//
// Ce que la section `.rdata` de `winsys3d.dll` porte hors de sa table d'exports (celle-ci est dans
// `Exports.cs`) : les constantes flottantes des tables UV et de la trigonometrie, les noms des
// commandes CD, et la table de portee SEH de la detection MMX (inerte : elle vise du code).

namespace OpenForesia.Libraries;

internal static class Winsys3dConstantesRdata
{
    internal static void Ecrire(EcrivainImage e)
    {
        e.Flottant(0x00a2a000, 0xbb800000);   // -0.00390625
        e.Flottant(0x00a2a004, 0x39cccccd);   // 0.0003906250058207661
        e.Flottant(0x00a2a008, 0xbc000000);   // -0.0078125
        e.Flottant(0x00a2a00c, 0x3a4ccccd);   // 0.0007812500116415322
        e.Flottant(0x00a2a010, 0xbc800000);   // -0.015625
        e.Flottant(0x00a2a014, 0x3acccccd);   // 0.0015625000232830644
        e.Flottant(0x00a2a018, 0xbd000000);   // -0.03125
        e.Flottant(0x00a2a01c, 0x3b4ccccd);   // 0.0031250000465661287
        e.Flottant(0x00a2a020, 0x3f7fe666);   // 0.9996093511581421
        e.Flottant(0x00a2a024, 0x3b4ccccd);   // FacteurProfondeurs_00a2a024 — 0.0031250000465661287
        e.Flottant(0x00a2a028, 0xbf800000);   // GammaInitial_00a2a028 — -1
        e.Chaine(0x00a2a030, "CdlNop");
        e.Chaine(0x00a2a038, "CdlSetloc");
        e.Chaine(0x00a2a048, "CdlPlay");
        e.Chaine(0x00a2a050, "CdlForward");
        e.Chaine(0x00a2a060, "CdlBackward");
        e.Flottant(0x00a2a06c, 0x3b4ccccd);   // UnSur320_00a2a06c — 0.0031250000465661287
        e.Flottant(0x00a2a070, 0xbb800000);   // ConstantesUv_00a2a070 — -0.00390625
        e.Flottant(0x00a2a074, 0x39cccccd);   // 0.0003906250058207661
        e.Flottant(0x00a2a078, 0xbc000000);   // -0.0078125
        e.Flottant(0x00a2a07c, 0x3a4ccccd);   // 0.0007812500116415322
        e.Flottant(0x00a2a080, 0xbc800000);   // -0.015625
        e.Flottant(0x00a2a084, 0x3acccccd);   // 0.0015625000232830644
        e.Flottant(0x00a2a088, 0xbd000000);   // -0.03125
        e.Flottant(0x00a2a08c, 0x3b4ccccd);   // 0.0031250000465661287
        e.Flottant(0x00a2a090, 0x3f7fe666);   // 0.9996093511581421
        e.Flottant(0x00a2a094, 0x3b000000);   // 0.001953125
        e.Flottant(0x00a2a098, 0x3b800000);   // 0.00390625
        e.Flottant(0x00a2a09c, 0x3c000000);   // 0.0078125
        e.Flottant(0x00a2a0a0, 0x3c800000);   // 0.015625
        e.Flottant(0x00a2a0a4, 0x00000000);   // 0
        e.Mots(0x00a2a0a8, 0xffffffff);
        e.Adresses(0x00a2a0ac, 0x00a0697f, 0x00a06985);   // -> 0x00a0697f -> 0x00a06985
        e.Flottant(0x00a2a0b4, 0x39800000);   // EchelleNormale_00a2a0b4 — 0.000244140625
        e.Flottant(0x00a2a0b8, 0x39800000);   // EchelleFixe_00a2a0b8 — 0.000244140625
        e.Flottant(0x00a2a0bc, 0x00000000);   // 0
        e.Flottant(0x00a2a0c0, 0x3f800000);   // 1
        e.Flottant(0x00a2a0c4, 0x39800000);   // 0.000244140625
        e.Flottant(0x00a2a0cc, 0x40000000);   // 2
        e.Qmot(0x00a2a0d0, 0x1000000000000000);
        e.Double(0x00a2a0d8, 0x3f5921fb54442d18);   // 0.0015339807878856412
        e.Double(0x00a2a0e0, 0x40b0000000000000);   // 4096
        e.Double(0x00a2a0e8, 0x3f30000000000000);   // 0.000244140625
        e.Double(0x00a2a0f0, 0x40845f306dc9c883);   // 651.8986469044033
    }
}
