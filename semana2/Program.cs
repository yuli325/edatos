// Programa principal
class Program
{
    static void Main(string[] args)
    {
        // Crear un círculo con radio 3
        Circulo c = new Circulo(3);
        Console.WriteLine("Radio del círculo: " + c.Radio);
        Console.WriteLine("Área del círculo: " + c.CalcularArea());
        Console.WriteLine("Perímetro del círculo: " + c.CalcularPerimetro());

        // Crear un cuadrado con lado 4
        Cuadrado q = new Cuadrado(4);
        Console.WriteLine("Lado del cuadrado: " + q.Lado);
        Console.WriteLine("Área del cuadrado: " + q.CalcularArea());
        Console.WriteLine("Perímetro del cuadrado: " + q.CalcularPerimetro());
    }
}
