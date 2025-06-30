class NumerosInversos
{
    private List<int> Numeros = new List<int>();

    public NumerosInversos()
    {
        for (int i = 1; i <= 10; i++)
        {
            Numeros.Add(i);
        }
    }

    public void MostrarInverso()
    {
        Numeros.Reverse();
        Console.WriteLine("Números en orden inverso:");
        Console.WriteLine(string.Join(", ", Numeros));
    }
}
