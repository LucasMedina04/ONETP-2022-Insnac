using System.Text;

namespace Sistema;
public class Alerta
{
    int idBoton;
    string codigo;
    public string Data => $"{idBoton} - {codigo}";
    
    public Alerta(int idBoton, string codigo)
    {
        this.idBoton = idBoton;
        this.codigo = codigo;
    }
}