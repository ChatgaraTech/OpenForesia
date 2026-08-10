//Force les cibles de la vtable du device 3dhelp comme fonctions, puis exporte leur pseudocode.
//@category OpenForesia

import ghidra.app.script.GhidraScript;
import ghidra.app.decompiler.DecompInterface;
import ghidra.app.decompiler.DecompileResults;
import ghidra.program.model.address.Address;
import ghidra.program.model.listing.Function;

import java.io.FileWriter;
import java.io.PrintWriter;

public class ExportVtableFuncs extends GhidraScript {

    // Adresses distinctes visees par la vtable du device (0x0ffe6000), relevees dans le binaire.
    private static final String[] TARGETS = {
        "0ffe1740", "0ffe1760", "0ffe1780", "0ffe1790", "0ffe17a0", "0ffe17b0", "0ffe17c0",
        "0ffe17d0", "0ffe1800", "0ffe1810", "0ffe1820", "0ffe1840", "0ffe1870", "0ffe1890",
        "0ffe1900", "0ffe1930", "0ffe1950", "0ffe1970", "0ffe1990", "0ffe19b0", "0ffe19d0",
        "0ffe1a10", "0ffe1a40", "0ffe1ad0", "0ffe1af0", "0ffe1b30", "0ffe1b50", "0ffe1b70",
        "0ffe1ba0", "0ffe1bc0", "0ffe1be0", "0ffe1c00", "0ffe1c20", "0ffe1ce0", "0ffe1d10",
        "0ffe1d40", "0ffe1d60", "0ffe1d80", "0ffe1da0", "0ffe1dc0", "0ffe1de0", "0ffe1e00",
        "0ffe1e20", "0ffe1e30", "0ffe1e40", "0ffe20c0", "0ffe20f0", "0ffe2410", "0ffe26d0",
        "0ffe27d0", "0ffe2830", "0ffe2880", "0ffe28e0", "0ffe2930", "0ffe29e0", "0ffe2ae0",
        "0ffe3fa0"
    };

    @Override
    public void run() throws Exception {
        String[] args = getScriptArgs();
        if (args.length < 1) {
            println("ERREUR : chemin de sortie attendu en argument");
            return;
        }

        int created = 0;
        for (String hex : TARGETS) {
            Address a = toAddr(hex);
            if (getFunctionAt(a) != null) continue;
            disassemble(a);
            if (createFunction(a, null) != null) created++;
        }
        println("fonctions creees : " + created + " sur " + TARGETS.length);

        DecompInterface di = new DecompInterface();
        di.openProgram(currentProgram);

        PrintWriter out = new PrintWriter(new FileWriter(args[0]));
        out.println("/* ===================================================== */");
        out.println("/* OpenForesia - cibles de la vtable du device 3dhelp     */");
        out.println("/* Program: 3dhelp.dll                                    */");
        out.println("/* ===================================================== */");
        out.println();

        int ok = 0, ko = 0;
        for (String hex : TARGETS) {
            Address a = toAddr(hex);
            Function f = getFunctionAt(a);
            if (f == null) {
                out.println("/* " + hex + " : aucune fonction */");
                out.println();
                ko++;
                continue;
            }

            DecompileResults res = di.decompileFunction(f, 60, monitor);
            out.println("/* ===================================================== */");
            out.println("/* " + f.getName() + " @ " + hex + "  ("
                        + f.getBody().getNumAddresses() + " octets) */");
            out.println("/* ===================================================== */");
            out.println();
            if (res != null && res.decompileCompleted()) {
                out.println(res.getDecompiledFunction().getC());
                ok++;
            } else {
                out.println("/* echec de decompilation : "
                            + (res == null ? "null" : res.getErrorMessage()) + " */");
                ko++;
            }
            out.println();
        }

        out.close();
        di.dispose();
        println("decompilees : " + ok + " / echecs : " + ko);
    }
}
