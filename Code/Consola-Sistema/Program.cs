using Sistema;

List<string> list = new List<string>();
list.Add("127.0.0.1");

Sistema.Sistema sistema = new(list);

Task t = new Task( () => sistema.Escuchar());

Thread.Sleep(5000);

sistema.Test(sistema.Clientes[0], new Alerta(1, "codigo"));