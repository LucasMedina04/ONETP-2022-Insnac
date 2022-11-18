using SuperSimpleTcp;
using System.Text;

namespace Sistema;
public class Sistema
{
    SimpleTcpServer serverSistema;
    SimpleTcpClient clienteSistema;
    public List<Cliente> Clientes;
    public Sistema(List<string> addresses)
    {
        // configuracion de clienteSistema
        serverSistema = new SimpleTcpServer("127.0.0.1:9000");
        clienteSistema = new SimpleTcpClient("127.0.0.1:9000");

        clienteSistema.Events.Connected += Connected;
        clienteSistema.Events.Disconnected += Disconnected;
        clienteSistema.Events.DataReceived += DataReceived;

        serverSistema.Events.ClientConnected += ClientConnected;
        serverSistema.Events.ClientDisconnected += ClientDisconnected;
        serverSistema.Events.DataReceived += DataReceivedServer;

        // configuracion de clientes externos
        Clientes = new List<Cliente>();
        foreach (var address in addresses)
            Clientes.Add(new Cliente(address));
    }
    // configuracion de cliente
    void Connected(object sender, ConnectionEventArgs e)
        => Console.WriteLine($"*** Server {e.IpPort} connected");
    void Disconnected(object sender, ConnectionEventArgs e)
        => Console.WriteLine($"*** Server {e.IpPort} disconnected");
    void DataReceived(object sender, DataReceivedEventArgs e)
        => Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");

    // configuracion de servidor
    void ClientConnected(object sender, ConnectionEventArgs e)
        => Console.WriteLine($"[{e.IpPort}] client connected");
    void ClientDisconnected(object sender, ConnectionEventArgs e)
        => Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
    void DataReceivedServer(object sender, DataReceivedEventArgs e)
        => Guardar($"{e.IpPort}: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");

    public void Escuchar()
    {
        serverSistema.Start();
        serverSistema.Send("[ClientIp:Port]", "Connected");
    }

    public void Enviar(Cliente cliente, string datosAEnviar)
    {
        clienteSistema = new SimpleTcpClient(cliente.Ip, 9000);
        clienteSistema.Connect();

        clienteSistema.Send(datosAEnviar);
    }
    public void Guardar(string datos)
    {
        Console.WriteLine(datos);
    }
    public void Test(Cliente cliente, Alerta alerta)
    {
        Enviar(cliente, alerta.Data);
    }
}
