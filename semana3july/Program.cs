using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== Registro de Estudiante ===");

            Console.Write("Ingrese ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ingrese Nombres: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese Apellidos: ");
            string apellido = Console.ReadLine();

            Console.Write("Ingrese Dirección: ");
            string direccion = Console.ReadLine();

            string[] telefonos = new string[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Ingrese Teléfono {i + 1}: ");
                telefonos[i] = Console.ReadLine();
            }

            Estudiante estudiante = new Estudiante(id, nombre, apellido, direccion, telefonos);
            estudiante.MostrarInformacion();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }
}
