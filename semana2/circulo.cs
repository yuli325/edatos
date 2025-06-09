// Clase Circulo que hereda de Figura
public class Circulo : Figura
{
    public double Radio { get; set; }

    public Circulo(double radio)
    {
        Radio = radio;
    }

    public override double CalcularArea()
    {
        return 3.1416 * Radio * Radio;
    }

    public override double CalcularPerimetro()
    {
        return 2 * 3.1416 * Radio;
    }
}
