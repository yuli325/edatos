using System;

#nullable disable

namespace VisualizadorGrafos
{
    /// <summary>
    /// Representa una arista (conexión) entre dos nodos
    /// </summary>
    public class Arista
    {
        public Nodo Origen { get; set; }
        public Nodo Destino { get; set; }
        public int Peso { get; set; }
        public bool EsDirigida { get; set; }

        public Arista(Nodo origen, Nodo destino, int peso, bool esDirigida)
        {
            Origen = origen;
            Destino = destino;
            Peso = peso;
            EsDirigida = esDirigida;
        }

        public override string ToString()
        {
            string flecha = EsDirigida ? "->" : "--";
            return $"{Origen.Etiqueta} {flecha} {Destino.Etiqueta} (Peso: {Peso})";
        }
    }
}