using System;

class Program
{
    static void Main()
    {
        // EJERCICIO 1
        Console.WriteLine("EJERCICIO 1");
        Curso curso = new Curso();
        curso.MostrarAsignaturas();
        Console.WriteLine(); // línea en blanco

        // EJERCICIO 2
        Console.WriteLine("EJERCICIO 2");
        CursoConEstudio curso2 = new CursoConEstudio();
        curso2.MostrarAsignaturasConMensaje();
        Console.WriteLine();

        // EJERCICIO 3
        Console.WriteLine("EJERCICIO 3");
        CursoConNotas curso3 = new CursoConNotas();
        curso3.PedirYMostrarNotas();
        Console.WriteLine();

        // EJERCICIO 4
        Console.WriteLine("EJERCICIO 4");
        Loteria loteria = new Loteria();
        loteria.PedirNumeros();
        loteria.MostrarGanadores();
        Console.WriteLine();

        // EJERCICIO 5
        Console.WriteLine("EJERCICIO 5");
        NumerosInversos nums = new NumerosInversos();
        nums.MostrarInverso();
        Console.WriteLine();
    }
}
