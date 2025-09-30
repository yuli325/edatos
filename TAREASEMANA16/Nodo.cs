using System;

#nullable disable

namespace VisualizadorGrafos
{
    /// <summary>
    /// Representa un nodo (vértice) en el grafo
    /// </summary>
    public class Nodo
    {
        public string Id { get; set; }
        public string Etiqueta { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Nodo(string id, string etiqueta)
        {
            Id = id;
            Etiqueta = etiqueta;
            PosX = 0;
            PosY = 0;
        }

        public override string ToString()
        {
            return $"[{Etiqueta}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is Nodo otro)
            {
                return Id == otro.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}