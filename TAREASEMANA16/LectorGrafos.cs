using System;
using System.Collections.Generic;
using System.IO;

#nullable disable

namespace VisualizadorGrafos
{
    /// <summary>
    /// Clase responsable de leer grafos desde archivos de texto
    /// </summary>
    public class LectorGrafos
    {
        public static Grafo CargarDesdeArchivo(string rutaArchivo)
        {
            Grafo grafo = null;

            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine($"\nERROR: Archivo '{rutaArchivo}' no encontrado.");
                    return null;
                }

                string[] lineas = File.ReadAllLines(rutaArchivo);
                Dictionary<string, Nodo> nodos = new Dictionary<string, Nodo>();
                string nombreGrafo = "Grafo";
                bool esDirigido = true;
                int lineaActual = 0;

                foreach (string linea in lineas)
                {
                    lineaActual++;

                    if (string.IsNullOrWhiteSpace(linea) || linea.StartsWith("#"))
                        continue;

                    string[] partes = linea.Split('|');

                    if (partes.Length == 0) continue;

                    string comando = partes[0].Trim().ToUpper();

                    try
                    {
                        if (comando == "GRAFO")
                        {
                            if (partes.Length >= 3)
                            {
                                nombreGrafo = partes[1].Trim();
                                esDirigido = partes[2].Trim().ToUpper() == "DIRIGIDO";
                                grafo = new Grafo(nombreGrafo, esDirigido);
                                Console.WriteLine($"Creando grafo: {nombreGrafo} ({(esDirigido ? "Dirigido" : "No dirigido")})");
                            }
                        }
                        else if (comando == "NODO")
                        {
                            if (partes.Length >= 3 && grafo != null)
                            {
                                string id = partes[1].Trim();
                                string etiqueta = partes[2].Trim();
                                Nodo nodo = new Nodo(id, etiqueta);
                                nodos[id] = nodo;
                                grafo.AgregarNodo(nodo);
                            }
                        }
                        else if (comando == "ARISTA")
                        {
                            if (partes.Length >= 4 && grafo != null)
                            {
                                string idOrigen = partes[1].Trim();
                                string idDestino = partes[2].Trim();
                                int peso = int.Parse(partes[3].Trim());

                                if (nodos.ContainsKey(idOrigen) && nodos.ContainsKey(idDestino))
                                {
                                    grafo.AgregarArista(nodos[idOrigen], nodos[idDestino], peso);
                                }
                                else
                                {
                                    Console.WriteLine($"Advertencia línea {lineaActual}: Nodo no encontrado");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error en línea {lineaActual}: {ex.Message}");
                    }
                }

                if (grafo != null)
                {
                    Console.WriteLine($"\nGrafo cargado exitosamente: {grafo.ObtenerNodos().Count} nodos, {grafo.ObtenerAristas().Count} aristas\n");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR al cargar archivo: {ex.Message}\n");
            }

            return grafo;
        }
    }
}