using System;
using System.Collections.Generic;

public class FilaAtraccion
{
    private Queue<Cliente> filaClientes = new Queue<Cliente>();
    private List<Asiento> asientos = new List<Asiento>();
    private const int MaxAsientos = 30;

    public void IngresarCliente(string nombre)
    {
        filaClientes.Enqueue(new Cliente(nombre));
        Console.WriteLine($"{nombre} ha sido agregado a la fila.");
    }

    public void AsignarAsientos()
    {
        while (asientos.Count < MaxAsientos && filaClientes.Count > 0)
        {
            Cliente cliente = filaClientes.Dequeue();
            asientos.Add(new Asiento(asientos.Count + 1, cliente));
            Console.WriteLine($"Se asignó asiento a: {cliente.Nombre}");
        }

        if (asientos.Count == MaxAsientos)
        {
            Console.WriteLine("Todos los asientos han sido asignados.");
        }
    }

    public void VerClientesEnFila()
    {
        Console.WriteLine("\nClientes en la fila:");
        if (filaClientes.Count == 0)
        {
            Console.WriteLine("No hay clientes en espera.");
        }
        else
        {
            foreach (var c in filaClientes)
            {
                Console.WriteLine($"- {c.Nombre}");
            }
        }
    }

    public void VerAsientos()
    {
        Console.WriteLine("\nAsientos asignados:");
        if (asientos.Count == 0)
        {
            Console.WriteLine("Aún no se han asignado asientos.");
        }
        else
        {
            foreach (var asiento in asientos)
            {
                Console.WriteLine(asiento);
            }
        }
    }
}
