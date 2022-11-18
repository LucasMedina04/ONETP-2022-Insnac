using SuperSimpleTcp;
using System.Text;


// instantiate
SimpleTcpServer server = new SimpleTcpServer("127.0.0.1:9000");

    // set events
    server.Events.ClientConnected += ClientConnected;
    server.Events.ClientDisconnected += ClientDisconnected;
    server.Events.DataReceived += DataReceived;

    // let's go!
    server.Start();

    // once a client has connected...
    server.Send("[ClientIp:Port]", "Hello, world!");
    Console.ReadKey();


static void ClientConnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] client connected");
}

static void ClientDisconnected(object sender, ConnectionEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
}

static void DataReceived(object sender, DataReceivedEventArgs e)
{
    Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
}
