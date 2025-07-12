using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("2525 - ESTRUCTURA DE DATOS - UEA / SEMANA 07");

        Console.WriteLine("Ingrese una expresión matemática:");
        string expresion = Console.ReadLine() ?? "";

        if (Ejercicio1.EstaBalanceada(expresion))
        {
            Console.WriteLine("Fórmula balanceada.");
        }
        else
        {
            Console.WriteLine("Fórmula NO balanceada.");
        }
    }
}
