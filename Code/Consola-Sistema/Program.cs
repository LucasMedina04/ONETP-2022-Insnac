using SuperSimpleTcp;
using System.Diagnostics;
using System.Text;



    // instantiate
    SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:9000");

    // set events
    client.Events.Connected += Connected;
    client.Events.Disconnected += Disconnected;
    client.Events.DataReceived += DataReceived;

    // let's go!
    client.Connect();

    // once connected to the server...
    client.Send("Hello, world!");
    Console.ReadKey();

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