public class Estudiante : Persona
{
    public int Id { get; set; }
    public string[] Ltelefono { get; set; }

    public Estudiante(int id, string nombre, string apellido, string direccion, string[] ltelefono)
        : base(nombre, apellido, direccion)
    {
        this.Id = id;
        this.Ltelefono = ltelefono;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("\n--- Información del Estudiante ---");
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre Completo: {this.Nombre} {this.Apellido}");
        Console.WriteLine($"Dirección: {this.Direccion}");
        Console.WriteLine("Teléfonos:");
        int contador = 1;
        foreach (var telefono in Ltelefono)
        {
            Console.WriteLine($"{contador++}: {telefono}");
        }
    }
}
