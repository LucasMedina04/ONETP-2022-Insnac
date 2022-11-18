using SuperSimpleTcp;
using System.Text;

namespace Sistema;

public class Cliente
{
    SimpleTcpClient client;
    string ip;
    public string Ip => ip;
    public Cliente(string ip)
    {
        this.ip = ip;
        client = new SimpleTcpClient(ip, 9000);
        client.Events.Connected += Connected;
        client.Events.Disconnected += Disconnected;
        client.Events.DataReceived += DataReceived;
    }

    public void Enviar(string datosAEnviar)
    {
        client.Connect();

        client.Send(datosAEnviar);
    }

    void Connected(object sender, ConnectionEventArgs e)
    {
        Console.WriteLine($"*** Server {e.IpPort} connected");
    }

    void Disconnected(object sender, ConnectionEventArgs e)
    {
        Console.WriteLine($"*** Server {e.IpPort} disconnected");
    }

    void DataReceived(object sender, DataReceivedEventArgs e)
    {
        Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
    }
}