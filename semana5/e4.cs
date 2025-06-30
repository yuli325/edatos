using System;
using System.Collections.Generic;

public class Loteria
{
    public List<int> NumerosGanadores { get; set; } = new List<int>();

    public void PedirNumeros()
    {
        Console.WriteLine("Ingrese 6 números ganadores de la lotería:");

        for (int i = 0; i < 6; i++)
        {
            Console.Write($"Número {i + 1}: ");
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int numero))
            {
                NumerosGanadores.Add(numero);
            }
            else
            {
                Console.WriteLine("Entrada inválida. Se usará el valor 0 por defecto.");
                NumerosGanadores.Add(0);
            }
        }

        NumerosGanadores.Sort();
    }

    public void MostrarGanadores()
    {
        Console.WriteLine("\nLos números ganadores ordenados son:");
        Console.WriteLine(string.Join(", ", NumerosGanadores));
    }
}
