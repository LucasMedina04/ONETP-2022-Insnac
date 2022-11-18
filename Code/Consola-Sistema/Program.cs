using Sistema;
using System.Net;

IPAddress address = IPAddress.Parse("127.0.0.1");
var addresses = new List<IPAddress>();
addresses.Add(address);
Sistema.Sistema sistema = new Sistema.Sistema(addresses);

sistema.Test(sistema.Clientes[0] ,new Alerta(1,"cd-blue"));