using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace VisualizadorGrafos
{
    /// <summary>
    /// Representa un grafo con nodos y aristas
    /// </summary>
    public class Grafo
    {
        public string Nombre { get; set; }
        public bool EsDirigido { get; set; }
        private List<Nodo> nodos;
        private List<Arista> aristas;
        private Dictionary<string, List<Nodo>> listaAdyacencia;

        public Grafo(string nombre, bool esDirigido)
        {
            Nombre = nombre;
            EsDirigido = esDirigido;
            nodos = new List<Nodo>();
            aristas = new List<Arista>();
            listaAdyacencia = new Dictionary<string, List<Nodo>>();
        }

        public void AgregarNodo(Nodo nodo)
        {
            if (!nodos.Any(n => n.Id == nodo.Id))
            {
                nodos.Add(nodo);
                listaAdyacencia[nodo.Id] = new List<Nodo>();
            }
        }

        public void AgregarArista(Nodo origen, Nodo destino, int peso)
        {
            AgregarNodo(origen);
            AgregarNodo(destino);

            Arista arista = new Arista(origen, destino, peso, EsDirigido);
            aristas.Add(arista);

            listaAdyacencia[origen.Id].Add(destino);
            
            if (!EsDirigido)
            {
                listaAdyacencia[destino.Id].Add(origen);
            }
        }

        public List<Nodo> ObtenerNodos()
        {
            return nodos;
        }

        public List<Arista> ObtenerAristas()
        {
            return aristas;
        }

        public List<Nodo> ObtenerVecinos(string nodoId)
        {
            if (listaAdyacencia.ContainsKey(nodoId))
            {
                return listaAdyacencia[nodoId];
            }
            return new List<Nodo>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"\n═══════════════════════════════════════════════════════");
            Console.WriteLine($"  GRAFO: {Nombre}");
            Console.WriteLine($"  Tipo: {(EsDirigido ? "Dirigido" : "No dirigido")}");
            Console.WriteLine($"═══════════════════════════════════════════════════════");
            Console.WriteLine($"  Nodos: {nodos.Count}");
            Console.WriteLine($"  Aristas: {aristas.Count}");
            Console.WriteLine($"═══════════════════════════════════════════════════════\n");
        }

        public void MostrarListaAdyacencia()
        {
            Console.WriteLine("\nLISTA DE ADYACENCIA:");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            
            foreach (var nodo in nodos.OrderBy(n => n.Id))
            {
                Console.Write($"{nodo.Etiqueta} -> ");
                var vecinos = ObtenerVecinos(nodo.Id);
                
                if (vecinos.Count == 0)
                {
                    Console.WriteLine("(sin conexiones)");
                }
                else
                {
                    Console.WriteLine(string.Join(", ", vecinos.Select(v => v.Etiqueta)));
                }
            }
            Console.WriteLine("═══════════════════════════════════════════════════════\n");
        }

        public void MostrarMatrizAdyacencia()
        {
            Console.WriteLine("\nMATRIZ DE ADYACENCIA:");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            
            var nodosOrdenados = nodos.OrderBy(n => n.Id).ToList();
            
            Console.Write("     ");
            foreach (var nodo in nodosOrdenados)
            {
                Console.Write($"{nodo.Etiqueta,4}");
            }
            Console.WriteLine();

            foreach (var origen in nodosOrdenados)
            {
                Console.Write($"{origen.Etiqueta,4} ");
                
                foreach (var destino in nodosOrdenados)
                {
                    var arista = aristas.FirstOrDefault(a => 
                        a.Origen.Id == origen.Id && a.Destino.Id == destino.Id);
                    
                    if (arista != null)
                    {
                        Console.Write($"{arista.Peso,4}");
                    }
                    else if (!EsDirigido)
                    {
                        var aristaInversa = aristas.FirstOrDefault(a => 
                            a.Origen.Id == destino.Id && a.Destino.Id == origen.Id);
                        
                        if (aristaInversa != null)
                        {
                            Console.Write($"{aristaInversa.Peso,4}");
                        }
                        else
                        {
                            Console.Write("   0");
                        }
                    }
                    else
                    {
                        Console.Write("   0");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("═══════════════════════════════════════════════════════\n");
        }

        public void DibujarGrafo()
        {
            Console.WriteLine("\nREPRESENTACIÓN GRÁFICA:");
            Console.WriteLine("═══════════════════════════════════════════════════════");
            
            CalcularPosiciones();
            
            int altura = 20;
            int ancho = 60;
            char[,] canvas = new char[altura, ancho];
            
            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    canvas[i, j] = ' ';
                }
            }

            foreach (var arista in aristas)
            {
                DibujarLinea(canvas, arista, ancho, altura);
            }

            foreach (var nodo in nodos)
            {
                DibujarNodo(canvas, nodo, ancho, altura);
            }

            for (int i = 0; i < altura; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    Console.Write(canvas[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("═══════════════════════════════════════════════════════\n");
        }

        private void CalcularPosiciones()
        {
            int n = nodos.Count;
            double angulo = 0;
            double incremento = 2 * Math.PI / n;
            int radio = 8;
            int centroX = 30;
            int centroY = 10;

            foreach (var nodo in nodos)
            {
                nodo.PosX = centroX + (int)(radio * Math.Cos(angulo));
                nodo.PosY = centroY + (int)(radio * Math.Sin(angulo));
                angulo += incremento;
            }
        }

        private void DibujarNodo(char[,] canvas, Nodo nodo, int ancho, int altura)
        {
            int x = nodo.PosX;
            int y = nodo.PosY;

            if (x >= 0 && x < ancho && y >= 0 && y < altura)
            {
                canvas[y, x] = nodo.Etiqueta[0];
            }
        }

        private void DibujarLinea(char[,] canvas, Arista arista, int ancho, int altura)
        {
            int x1 = arista.Origen.PosX;
            int y1 = arista.Origen.PosY;
            int x2 = arista.Destino.PosX;
            int y2 = arista.Destino.PosY;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x1 >= 0 && x1 < ancho && y1 >= 0 && y1 < altura)
                {
                    if (canvas[y1, x1] == ' ')
                    {
                        canvas[y1, x1] = arista.EsDirigida ? '>' : '-';
                    }
                }

                if (x1 == x2 && y1 == y2) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
    }
}