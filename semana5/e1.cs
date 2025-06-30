using System;
using System.Collections.Generic;

class Curso
{
    public List<string> Asignaturas { get; set; }

    public Curso()
    {
        Asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
    }

    public void MostrarAsignaturas()
    {
        Console.WriteLine("Asignaturas del curso:");
        foreach (string asignatura in Asignaturas)
        {
            Console.WriteLine(asignatura);
        }
    }
}
