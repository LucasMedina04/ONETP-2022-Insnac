using System.Text;

namespace Sistema;
public class Alerta
{
    int idBoton;
    string codigo;
    public byte[] Data =>
        Encoding.UTF8.GetBytes($"{idBoton} - {codigo}<|EOM|>");
    
    public Alerta(int idBoton, string codigo)
    {
        this.idBoton = idBoton;
        this.codigo = codigo;
    }
}