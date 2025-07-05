using System;

public class Ejercicio3
{
    public static int Buscar(Nodo cabeza, int valor)
    {
        int contador = 0;
        Nodo actual = cabeza;
        while (actual != null)
        {
            if (actual.Dato == valor)
                contador++;
            actual = actual.Siguiente;
        }

        if (contador == 0)
            Console.WriteLine($"El dato {valor} no fue encontrado.");
        else
            Console.WriteLine($"El dato {valor} se encontró {contador} vez/veces.");

        return contador;
    }
}
