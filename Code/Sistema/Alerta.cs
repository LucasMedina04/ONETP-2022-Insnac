namespace Sistema;
public class Alerta
{
    string nombreBoton;
    string codigo;
    public string NombreBoton => nombreBoton;
    public string Codigo => codigo;
    public Alerta(string nombreBoton, string codigo)
    {
        this.nombreBoton = nombreBoton;
        this.codigo = codigo;
    }
}