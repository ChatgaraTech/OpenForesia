param([string]$Path, [int]$From = 0, [int]$To = 100000, [uint32]$ImageBase = 0)

$b = [System.IO.File]::ReadAllBytes($Path)

function U32([int]$o) { [BitConverter]::ToUInt32($b, $o) }
function U16([int]$o) { [BitConverter]::ToUInt16($b, $o) }

$pe = U32 0x3c
if ((U32 $pe) -ne 0x00004550) { throw "pas un PE" }

$nSections   = U16 ($pe + 6)
$optSize     = U16 ($pe + 20)
$opt         = $pe + 24
$magic       = U16 $opt
$base        = if ($magic -eq 0x20b) { [uint32](U32 ($opt + 24)) } else { U32 ($opt + 28) }
if ($ImageBase -ne 0) { $base = $ImageBase }

$dirOff      = if ($magic -eq 0x20b) { $opt + 112 } else { $opt + 96 }
$expRva      = U32 $dirOff

$secTable = $opt + $optSize
$sections = @()
for ($i = 0; $i -lt $nSections; $i++) {
    $s = $secTable + $i * 40
    $sections += [pscustomobject]@{
        VA   = U32 ($s + 12)
        VSz  = U32 ($s + 8)
        Raw  = U32 ($s + 20)
    }
}

function RvaToOff([uint32]$rva) {
    foreach ($s in $sections) {
        if ($rva -ge $s.VA -and $rva -lt ($s.VA + [Math]::Max($s.VSz, 1))) {
            return [int]($rva - $s.VA + $s.Raw)
        }
    }
    return -1
}

$e = RvaToOff $expRva
$ordBase   = U32 ($e + 16)
$nFuncs    = U32 ($e + 20)
$nNames    = U32 ($e + 24)
$funcsRva  = U32 ($e + 28)
$namesRva  = U32 ($e + 32)
$ordsRva   = U32 ($e + 36)

$funcs = RvaToOff $funcsRva
$names = RvaToOff $namesRva
$ords  = RvaToOff $ordsRva

# ordinal -> name, pour les exports nommes
$nameByOrdinal = @{}
for ($i = 0; $i -lt $nNames; $i++) {
    $ord = (U16 ($ords + $i * 2)) + $ordBase
    $nOff = RvaToOff (U32 ($names + $i * 4))
    $len = 0
    while ($b[$nOff + $len] -ne 0) { $len++ }
    $nameByOrdinal[[int]$ord] = [System.Text.Encoding]::ASCII.GetString($b, $nOff, $len)
}

"ImageBase = 0x{0:x8}   {1} exports (base ordinal {2})" -f $base, $nFuncs, $ordBase
""
"{0,6}  {1,-10}  {2,-10}  {3,-8}  {4}" -f "ord", "RVA", "VA", "section", "name"
"------  ----------  ----------  --------  ----"

for ($i = 0; $i -lt $nFuncs; $i++) {
    $ord = $i + $ordBase
    if ($ord -lt $From -or $ord -gt $To) { continue }
    $rva = U32 ($funcs + $i * 4)
    if ($rva -eq 0) { continue }
    $va = $base + $rva
    $off = RvaToOff $rva
    $sec = "?"
    for ($k = 0; $k -lt $nSections; $k++) {
        $s = $sections[$k]
        if ($rva -ge $s.VA -and $rva -lt ($s.VA + $s.VSz)) {
            $raw = $secTable + $k * 40
            $len = 0
            while ($len -lt 8 -and $b[$raw + $len] -ne 0) { $len++ }
            $sec = [System.Text.Encoding]::ASCII.GetString($b, $raw, $len)
        }
    }
    $nm = if ($nameByOrdinal.ContainsKey([int]$ord)) { $nameByOrdinal[[int]$ord] } else { "" }
    "{0,6}  0x{1:x8}  0x{2:x8}  {3,-8}  {4}" -f $ord, $rva, $va, $sec, $nm
}
