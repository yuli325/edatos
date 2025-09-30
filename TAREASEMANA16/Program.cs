using System;

#nullable disable

namespace VisualizadorGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            MostrarBanner();

            Console.WriteLine("VISUALIZADOR DE GRAFOS\n");
            Console.WriteLine("Este programa carga y visualiza grafos desde archivos de texto.\n");

            Console.WriteLine("Archivos de ejemplo disponibles:");
            Console.WriteLine("  - grafo1.txt (Red Social)");
            Console.WriteLine("  - grafo2.txt (Red de Ciudades)\n");

            Console.Write("Ingrese el nombre del archivo a cargar: ");
            string archivo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(archivo))
            {
                archivo = "grafo1.txt";
                Console.WriteLine($"Usando archivo por defecto: {archivo}");
            }

            Grafo grafo = LectorGrafos.CargarDesdeArchivo(archivo);

            if (grafo == null)
            {
                Console.WriteLine("\nNo se pudo cargar el grafo. Presione cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }

            bool continuar = true;
            while (continuar)
            {
                continuar = MostrarMenu(grafo);
            }

            Console.WriteLine("\nGracias por usar el Visualizador de Grafos!");
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                          ║");
            Console.WriteLine("║                    VISUALIZADOR DE GRAFOS                                ║");
            Console.WriteLine("║                                                                          ║");
            Console.WriteLine("║              Universidad Estatal Amazónica - UEA                        ║");
            Console.WriteLine("║              Estructura de Datos - Grafos                               ║");
            Console.WriteLine("║                                                                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        static bool MostrarMenu(Grafo grafo)
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    MENÚ PRINCIPAL                     ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Ver información del grafo                         ║");
            Console.WriteLine("║ 2. Mostrar lista de adyacencia                       ║");
            Console.WriteLine("║ 3. Mostrar matriz de adyacencia                      ║");
            Console.WriteLine("║ 4. Dibujar grafo                                     ║");
            Console.WriteLine("║ 5. Ver todas las representaciones                    ║");
            Console.WriteLine("║ 6. Salir                                             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    grafo.MostrarInformacion();
                    PausarConsola();
                    break;
                case "2":
                    grafo.MostrarListaAdyacencia();
                    PausarConsola();
                    break;
                case "3":
                    grafo.MostrarMatrizAdyacencia();
                    PausarConsola();
                    break;
                case "4":
                    grafo.DibujarGrafo();
                    PausarConsola();
                    break;
                case "5":
                    MostrarTodasRepresentaciones(grafo);
                    PausarConsola();
                    break;
                case "6":
                    return false;
                default:
                    Console.WriteLine("\nOpción inválida. Intente nuevamente.");
                    break;
            }

            return true;
        }

        static void MostrarTodasRepresentaciones(Grafo grafo)
        {
            Console.Clear();
            grafo.MostrarInformacion();
            grafo.MostrarListaAdyacencia();
            grafo.MostrarMatrizAdyacencia();
            grafo.DibujarGrafo();
        }

        static void PausarConsola()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}