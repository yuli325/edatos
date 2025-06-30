class CursoConEstudio : Curso
{
    public void MostrarAsignaturasConMensaje()
    {
        Console.WriteLine("Estudios:");
        foreach (string asignatura in Asignaturas)
        {
            Console.WriteLine($"Yo estudio {asignatura}");
        }
    }
}
