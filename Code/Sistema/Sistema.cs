using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sistema;
public class Sistema
{
    public List<Cliente> Clientes;
    public Sistema(List<IPAddress> addresses)
    {
        Clientes = new List<Cliente>();
        foreach (var address in addresses)
            Clientes.Add(new Cliente(new IPEndPoint(address, 50000)));
    }
    private async void Escuchar()
    {
        Socket listener = new Socket(this.Clientes[0].Socket.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(this.Clientes[0].Endpoint);
        listener.Listen(50000);
        
        var handler = await listener.AcceptAsync();
        while (true)
        {
            var buffer = new byte[1_024];
            var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            
            var eom = "<|EOM|>";
            if (response.IndexOf(eom) > -1 /* is end of message */)
            {
                Console.WriteLine(
                    $"Socket server received message: \"{response.Replace(eom, "")}\"");

                var ackMessage = "<|ACK|>";
                var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                await handler.SendAsync(echoBytes, 0);
                Console.WriteLine(
                    $"Socket server sent acknowledgment: \"{ackMessage}\"");

                break;
            }
        }

    }

    private async void Enviar(Cliente cliente, Alerta alerta)
    {
        var cl = cliente.Socket;
        while (true)
        {
            await cl.ConnectAsync(cliente.Endpoint);
            var msg = alerta.Data;
            await cl.SendAsync(msg, SocketFlags.None);

            var buffer = new byte[1_024];
            var received = await cl.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            if (response.Length > 0)
            {
                Console.WriteLine(response);
                break;
            }
        }
        cl.Shutdown(SocketShutdown.Both);
    }
    public void Test(Cliente cl, Alerta alerta)
    {
        //Task a = new Task (() => Escuchar());
        //a.Start();
        Enviar(cl, alerta);
        Thread.Sleep(100000);
    }
}
