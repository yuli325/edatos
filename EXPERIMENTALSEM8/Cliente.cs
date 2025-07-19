public class Cliente
{
    public string Nombre { get; set; }

    public Cliente(string nombre)
    {
        Nombre = nombre;
    }

    public override string ToString()
    {
        return Nombre;
    }
}
