public class Persona
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }

    public Persona(string nombre, string apellido, string direccion)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Direccion = direccion;
    }
}
