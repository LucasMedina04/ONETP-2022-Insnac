using SuperSimpleTcp;
using System.Text;

SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:9000");

client.Events.Connected += Connected;
client.Events.Disconnected += Disconnected;
client.Events.DataReceived += DataReceived;

client.Connect();

client.Send("hola");

static void Connected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} connected");
}

static void Disconnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"*** Server {e.IpPort} disconnected");
}

static void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
}