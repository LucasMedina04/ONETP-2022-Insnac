using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress Ip = IPAddress.Parse("127.0.0.1"); // coloca ip emisor aca

IPEndPoint ep = new (Ip, 50000);

Socket listener = new Socket(ep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
listener.Bind(ep);
listener.Listen(50000);

while (true)
{
    Console.WriteLine("Escuchando...");
    var handler = await listener.AcceptAsync();
    while (true)
    {
        var buffer = new byte[1_024];
        var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
        var response = Encoding.UTF8.GetString(buffer, 0, received);
            
        var eom = "<|EOM|>";
        if (response.IndexOf(eom) > -1 )
        {
            Console.WriteLine($"Socket server received message: \"{response.Replace(eom, "")}\"");

            var ackMessage = "<|ACK|>";
            var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
            await handler.SendAsync(echoBytes, 0);
            Console.WriteLine($"Socket server sent acknowledgment: \"{ackMessage}\"");

            break;
        }
    }
}