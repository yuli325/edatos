public class Nodo
{
    public int Dato;
    public Nodo Siguiente;

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class Ejercicio1
{
    private Nodo cabeza;

    public Ejercicio1()
    {
        cabeza = null;
    }

    public void Agregar(int valor)
    {
        Nodo nuevo = new Nodo(valor);
        if (cabeza == null)
            cabeza = nuevo;
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
                actual = actual.Siguiente;
            actual.Siguiente = nuevo;
        }
    }

    public int ContarElementos()
    {
        int contador = 0;
        Nodo actual = cabeza;
        while (actual != null)
        {
            contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }

    public Nodo ObtenerCabeza()
    {
        return cabeza;
    }
}
