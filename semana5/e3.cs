using System;
using System.Collections.Generic;

public class CursoConNotas
{
    private List<string> asignaturas = new List<string>
    {
        "Matemáticas",
        "Física",
        "Química",
        "Historia",
        "Lengua"
    };

    private List<string> notas = new List<string>(); // ← Aquí declaramos la variable

    public void PedirYMostrarNotas()
    {
        foreach (string asignatura in asignaturas)
        {
            Console.Write($"¿Qué nota has sacado en {asignatura}? ");
            string? nota = Console.ReadLine();
            notas.Add(nota ?? "0"); // si es null, usa "0"
        }

        Console.WriteLine("\nResumen de notas:");
        for (int i = 0; i < asignaturas.Count; i++)
        {
            Console.WriteLine($"En {asignaturas[i]} has sacado {notas[i]}");
        }
    }
}
