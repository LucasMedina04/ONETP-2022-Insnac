using System.Net;
using System.Net.Sockets;

namespace Sistema;

public class Cliente
{
    Socket socket;
    IPEndPoint endpoint;
    public IPEndPoint Endpoint => endpoint;
    public Socket Socket => socket;

    public Cliente(IPEndPoint endpoint)
    {
        this.endpoint = endpoint;
        socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }
}
