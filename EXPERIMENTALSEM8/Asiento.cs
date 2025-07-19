public class Asiento
{
    public int Numero { get; set; }
    public Cliente Ocupante { get; set; }

    public Asiento(int numero, Cliente ocupante)
    {
        Numero = numero;
        Ocupante = ocupante;
    }

    public override string ToString()
    {
        return $"Asiento {Numero}: {Ocupante.Nombre}";
    }
}
