using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("2525 - ESTRUCTURA DE DATOS - UEA / SEMANA 07");

        Console.Write("Ingrese la cantidad de discos: ");
        string entrada = Console.ReadLine() ?? "";

        if (!int.TryParse(entrada, out int numDiscos) || numDiscos <= 0)
        {
            Console.WriteLine("Entrada no válida. Debe ingresar un número entero positivo.");
            return;
        }

        // Cargar los discos en la torre de origen (del mayor al menor)
        for (int i = numDiscos; i >= 1; i--)
        {
            Ejercicio2.origen.Push(i);
        }

        Console.WriteLine("\nEstado inicial:");
        Ejercicio2.MostrarTorres();

        // Resolver el problema
        Ejercicio2.Resolver(
            numDiscos,
            Ejercicio2.origen,
            Ejercicio2.destino,
            Ejercicio2.auxiliar,
            "Origen",
            "Destino",
            "Auxiliar"
        );

        Console.WriteLine("¡Todos los discos se han movido exitosamente!");
    }
}
