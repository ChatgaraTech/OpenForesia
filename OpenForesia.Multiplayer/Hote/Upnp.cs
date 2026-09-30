// L'ouverture du port de l'hote par sa box (tache 4.1, design D6), sans bibliotheque : UPnP IGD.
//
//   1. SSDP : un M-SEARCH en multidiffusion (239.255.255.250:1900) ; la box repond par l'adresse de
//      sa description (LOCATION) ;
//   2. la description (XML, par HTTP) donne l'URL de controle du service WANIPConnection (ou
//      WANPPPConnection, les box en PPPoE) ;
//   3. SOAP : AddPortMapping (le port UDP vers ce PC), GetExternalIPAddress (l'adresse publique a
//      mettre dans le code), et DeletePortMapping en fin de session.
//
// Une adresse « publique » privee (10/8, 172.16/12, 192.168/16, 100.64/10) veut dire un double NAT :
// la box est elle-meme derriere une autre (4G, certaines offres fibre) — sans serveur, on n'y peut rien.

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenForesia.Multiplayer.Hote;

internal sealed partial class Upnp
{
    private const string Description = "OpenForesia multijoueur";

    private readonly Uri _controle;
    private readonly string _service;
    private readonly IPAddress _adresseLocale;
    private int _portOuvert;

    internal IPAddress AdresseLocale => _adresseLocale;

    private Upnp(Uri controle, string service, IPAddress adresseLocale)
    {
        _controle = controle;
        _service = service;
        _adresseLocale = adresseLocale;
    }

    private static readonly HttpClient s_Http = new() { Timeout = TimeSpan.FromSeconds(3) };

    /// <summary>La box, si elle répond à UPnP dans les deux secondes ; null sinon.</summary>
    internal static async Task<Upnp?> Trouver()
    {
        foreach (Uri description in await Chercher())
        {
            try
            {
                string xml = await s_Http.GetStringAsync(description);
                foreach (Match m in ServiceXml().Matches(xml))
                {
                    string type = m.Groups["type"].Value.Trim();
                    if (!type.Contains(":WANIPConnection:") && !type.Contains(":WANPPPConnection:")) continue;
                    var controle = new Uri(description, m.Groups["controle"].Value.Trim());
                    return new Upnp(controle, type, AdresseVers(description.Host));
                }
            }
            catch (Exception e) when (e is HttpRequestException or TaskCanceledException or UriFormatException) { }
        }
        return null;
    }

    /// <summary>Les descriptions annoncées par les passerelles du réseau (SSDP).</summary>
    private static async Task<List<Uri>> Chercher()
    {
        var trouvees = new List<Uri>();
        using var udp = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
        var groupe = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 1900);
        foreach (string cible in (string[])["urn:schemas-upnp-org:device:InternetGatewayDevice:1", "urn:schemas-upnp-org:service:WANIPConnection:1"])
        {
            byte[] requete = Encoding.ASCII.GetBytes(
                $"M-SEARCH * HTTP/1.1\r\nHOST: 239.255.255.250:1900\r\nMAN: \"ssdp:discover\"\r\nMX: 2\r\nST: {cible}\r\n\r\n");
            await udp.SendAsync(requete, groupe);
        }

        using var delai = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        try
        {
            while (true)
            {
                UdpReceiveResult r = await udp.ReceiveAsync(delai.Token);
                Match m = LocationSsdp().Match(Encoding.ASCII.GetString(r.Buffer));
                if (m.Success && Uri.TryCreate(m.Groups[1].Value.Trim(), UriKind.Absolute, out Uri? u) && !trouvees.Contains(u))
                    trouvees.Add(u);
            }
        }
        catch (OperationCanceledException) { }
        return trouvees;
    }

    /// <summary>L'adresse de ce PC sur le réseau de la box : celle que la box doit viser.</summary>
    private static IPAddress AdresseVers(string hote)
    {
        using var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.Connect(IPAddress.Parse(hote), 1900);
        return ((IPEndPoint)s.LocalEndPoint!).Address;
    }

    /// <summary>Ouvre le port UDP vers ce PC ; faux si la box refuse.</summary>
    internal async Task<bool> Ouvrir(int port)
    {
        string reponse = await Soap("AddPortMapping",
            $"<NewRemoteHost></NewRemoteHost><NewExternalPort>{port}</NewExternalPort><NewProtocol>UDP</NewProtocol>"
          + $"<NewInternalPort>{port}</NewInternalPort><NewInternalClient>{_adresseLocale}</NewInternalClient>"
          + $"<NewEnabled>1</NewEnabled><NewPortMappingDescription>{Description}</NewPortMappingDescription>"
          + "<NewLeaseDuration>0</NewLeaseDuration>");
        if (reponse.Contains("AddPortMappingResponse")) { _portOuvert = port; return true; }
        return false;
    }

    /// <summary>L'adresse publique de la box, ou null.</summary>
    internal async Task<IPAddress?> AdressePublique()
    {
        Match m = AdresseExterne().Match(await Soap("GetExternalIPAddress", ""));
        return m.Success && IPAddress.TryParse(m.Groups[1].Value.Trim(), out IPAddress? a) ? a : null;
    }

    /// <summary>Referme le port ouvert par <see cref="Ouvrir"/>, s'il l'a été.</summary>
    internal async Task Fermer()
    {
        if (_portOuvert == 0) return;
        await Soap("DeletePortMapping",
            $"<NewRemoteHost></NewRemoteHost><NewExternalPort>{_portOuvert}</NewExternalPort><NewProtocol>UDP</NewProtocol>");
        _portOuvert = 0;
    }

    /// <summary>Une adresse privée ou partagée (CGNAT) : la box n'est pas en prise directe avec internet.</summary>
    internal static bool Privee(IPAddress a)
    {
        byte[] o = a.GetAddressBytes();
        return o[0] == 10 || (o[0] == 172 && (o[1] & 0xF0) == 16) || (o[0] == 192 && o[1] == 168)
            || (o[0] == 100 && (o[1] & 0xC0) == 64) || o[0] == 0;
    }

    private async Task<string> Soap(string action, string arguments)
    {
        string corps =
            "<?xml version=\"1.0\"?>\r\n"
          + "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">"
          + $"<s:Body><u:{action} xmlns:u=\"{_service}\">{arguments}</u:{action}></s:Body></s:Envelope>";
        using var requete = new HttpRequestMessage(HttpMethod.Post, _controle)
        {
            Content = new StringContent(corps, Encoding.UTF8, "text/xml"),
        };
        requete.Headers.TryAddWithoutValidation("SOAPAction", $"\"{_service}#{action}\"");
        try
        {
            using HttpResponseMessage r = await s_Http.SendAsync(requete);
            return await r.Content.ReadAsStringAsync();
        }
        catch (Exception e) when (e is HttpRequestException or TaskCanceledException) { return ""; }
    }

    [GeneratedRegex(@"^LOCATION:\s*(\S+)", RegexOptions.IgnoreCase | RegexOptions.Multiline)]
    private static partial Regex LocationSsdp();

    [GeneratedRegex(@"<service>\s*<serviceType>(?<type>[^<]+)</serviceType>.*?<controlURL>(?<controle>[^<]+)</controlURL>", RegexOptions.Singleline)]
    private static partial Regex ServiceXml();

    [GeneratedRegex(@"<NewExternalIPAddress>([^<]*)</NewExternalIPAddress>")]
    private static partial Regex AdresseExterne();
}
