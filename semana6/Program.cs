using System;

class Program
{
    static void Main()
    {
        Ejercicio1 lista = new Ejercicio1();
        Console.WriteLine("Ingrese valores para la lista (presione ESC para finalizar):");

        while (true)
        {
            Console.Write("Ingrese un número: ");
            string entrada = LeerConEsc();

            if (entrada == null)
                break;

            if (int.TryParse(entrada, out int valor))
                lista.Agregar(valor);
            else
                Console.WriteLine("Entrada inválida. Intente de nuevo.");
        }

        int total = lista.ContarElementos();
        Console.WriteLine($"\nNúmero total de elementos en la lista: {total}");

        // BÚSQUEDA INTERACTIVA
        Console.WriteLine("\n--- Búsqueda de elementos (presione ESC para salir) ---");
        while (true)
        {
            Console.Write("Ingrese valor a buscar: ");
            string entradaBusqueda = LeerConEsc();

            if (entradaBusqueda == null)
                break;

            if (int.TryParse(entradaBusqueda, out int datoABuscar))
            {
                Ejercicio3.Buscar(lista.ObtenerCabeza(), datoABuscar);
            }
            else
            {
                Console.WriteLine("Entrada inválida. Intente nuevamente.");
            }
        }

        Console.WriteLine("\nPrograma finalizado.");
    }

    static string LeerConEsc()
    {
        string input = "";
        ConsoleKeyInfo keyInfo;

        while (true)
        {
            keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nEntrada finalizada por ESC.");
                return null;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input[0..^1];
                    Console.Write("\b \b");
                }
            }
            else
            {
                input += keyInfo.KeyChar;
                Console.Write(keyInfo.KeyChar);
            }
        }
    }
}
