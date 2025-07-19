using System;

class Program
{
    static void Main(string[] args)
    {
        FilaAtraccion fila = new FilaAtraccion();
        string? opcion;

        do
        {
            Console.WriteLine("\n--- Menú de la Atracción ---");
            Console.WriteLine("1. Ingresar cliente a la fila");
            Console.WriteLine("2. Asignar asientos");
            Console.WriteLine("3. Ver fila");
            Console.WriteLine("4. Ver asientos asignados");
            Console.WriteLine("5. Salir");
            Console.Write("Elija una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el nombre del cliente: ");
                    string? nombre = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        fila.IngresarCliente(nombre!); // El operador ! asegura que no es null
                    }
                    else
                    {
                        Console.WriteLine("❌ El nombre no puede estar vacío.");
                    }
                    break;

                case "2":
                    fila.AsignarAsientos();
                    break;

                case "3":
                    fila.VerClientesEnFila();
                    break;

                case "4":
                    fila.VerAsientos();
                    break;

                case "5":
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != "5");
    }
}
