using System;
using System.Collections.Generic;
using System.Linq;
using SistemaVacunacionCovid.Models;
using SistemaVacunacionCovid.Services;

namespace SistemaVacunacionCovid
{
    /// <summary>
    /// Programa principal que demuestra el sistema de vacunación COVID-19
    /// utilizando operaciones de teoría de conjuntos
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE VACUNACIÓN COVID-19 ===");
            Console.WriteLine("Ministerio de Salud - República Argentina");
            Console.WriteLine("Generando datos ficticios...\n");

            try
            {
                // 1. Generar datos ficticios
                var generador = new GeneradorDatos();
                
                // Crear 500 ciudadanos
                var ciudadanos = generador.GenerarCiudadanos(500);
                Console.WriteLine($" Generados {ciudadanos.Count} ciudadanos ficticios");

                // Seleccionar 75 ciudadanos aleatorios para Pfizer
                var ciudadanosParaPfizer = ciudadanos
                    .OrderBy(c => Guid.NewGuid())
                    .Take(75)
                    .Select(c => c.Id)
                    .ToList();

                // Seleccionar 75 ciudadanos aleatorios para AstraZeneca (pueden coincidir con Pfizer)
                var ciudadanosParaAstraZeneca = ciudadanos
                    .OrderBy(c => Guid.NewGuid())
                    .Take(75)
                    .Select(c => c.Id)
                    .ToList();

                // Generar vacunas
                var vacunasPfizer = generador.GenerarVacunasPfizer(ciudadanosParaPfizer);
                var vacunasAstraZeneca = generador.GenerarVacunasAstraZeneca(ciudadanosParaAstraZeneca);
                
                var todasVacunas = new List<Vacuna>();
                todasVacunas.AddRange(vacunasPfizer);
                todasVacunas.AddRange(vacunasAstraZeneca);

                Console.WriteLine($"✓ Generadas {vacunasPfizer.Count} dosis de Pfizer");
                Console.WriteLine($"✓ Generadas {vacunasAstraZeneca.Count} dosis de AstraZeneca");
                Console.WriteLine($"✓ Total de vacunas aplicadas: {todasVacunas.Count}\n");

                // 2. Crear servicio de vacunación
                var servicioVacunacion = new ServicioVacunacion(ciudadanos, todasVacunas);

                // 3. Mostrar estadísticas generales
                var estadisticas = servicioVacunacion.ObtenerEstadisticas();
                Console.WriteLine(estadisticas);

                // 4. Procesar y mostrar los listados solicitados
                MostrarMenu();
                
                bool continuar = true;
                while (continuar)
                {
                    Console.Write("Seleccione una opción: ");
                    var opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            MostrarCiudadanosNoVacunados(servicioVacunacion);
                            break;
                        case "2":
                            MostrarCiudadanosConAmbasDosis(servicioVacunacion);
                            break;
                        case "3":
                            MostrarCiudadanosSoloPfizer(servicioVacunacion);
                            break;
                        case "4":
                            MostrarCiudadanosSoloAstraZeneca(servicioVacunacion);
                            break;
                        case "5":
                            MostrarEstadisticasDetalladas(servicioVacunacion);
                            break;
                        case "6":
                            MostrarOperacionesConjuntos(servicioVacunacion);
                            break;
                        case "0":
                            continuar = false;
                            Console.WriteLine("¡Gracias por usar el Sistema de Vacunación COVID-19!");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente nuevamente.");
                            break;
                    }

                    if (continuar)
                    {
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        MostrarMenu();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el sistema: {ex.Message}");
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Ciudadanos que NO se han vacunado");
            Console.WriteLine("2. Ciudadanos que han recibido AMBAS dosis");
            Console.WriteLine("3. Ciudadanos que SOLO han recibido Pfizer");
            Console.WriteLine("4. Ciudadanos que SOLO han recibido AstraZeneca");
            Console.WriteLine("5. Estadísticas detalladas");
            Console.WriteLine("6. Demostración de operaciones de conjuntos");
            Console.WriteLine("0. Salir");
            Console.WriteLine(new string('=', 40));
        }

        static void MostrarCiudadanosNoVacunados(ServicioVacunacion servicio)
        {
            Console.WriteLine("\n=== CIUDADANOS NO VACUNADOS ===");
            Console.WriteLine("Operación de conjuntos: U - (P ∪ A)");
            Console.WriteLine("Donde U = Universo, P = Pfizer, A = AstraZeneca\n");

            var noVacunados = servicio.ObtenerCiudadanosNoVacunados();
            
            Console.WriteLine($"Total de ciudadanos no vacunados: {noVacunados.Count}\n");
            
            foreach (var ciudadano in noVacunados.Take(10)) // Mostrar solo los primeros 10
            {
                Console.WriteLine($"• {ciudadano}");
            }
            
            if (noVacunados.Count > 10)
            {
                Console.WriteLine($"... y {noVacunados.Count - 10} ciudadanos más.");
            }
        }

        static void MostrarCiudadanosConAmbasDosis(ServicioVacunacion servicio)
        {
            Console.WriteLine("\n=== CIUDADANOS CON AMBAS DOSIS ===");
            Console.WriteLine("Ciudadanos que tienen dosis número 2 (cualquier tipo de vacuna)\n");

            var conAmbasDosis = servicio.ObtenerCiudadanosConAmbasDosis();
            
            Console.WriteLine($"Total de ciudadanos con ambas dosis: {conAmbasDosis.Count}\n");
            
            foreach (var ciudadano in conAmbasDosis.Take(10)) // Mostrar solo los primeros 10
            {
                Console.WriteLine($"• {ciudadano}");
            }
            
            if (conAmbasDosis.Count > 10)
            {
                Console.WriteLine($"... y {conAmbasDosis.Count - 10} ciudadanos más.");
            }
        }

        static void MostrarCiudadanosSoloPfizer(ServicioVacunacion servicio)
        {
            Console.WriteLine("\n=== CIUDADANOS SOLO CON PFIZER ===");
            Console.WriteLine("Operación de conjuntos: P - A");
            Console.WriteLine("Donde P = Pfizer, A = AstraZeneca\n");

            var soloPfizer = servicio.ObtenerCiudadanosSoloPfizer();
            
            Console.WriteLine($"Total de ciudadanos solo con Pfizer: {soloPfizer.Count}\n");
            
            foreach (var ciudadano in soloPfizer.Take(10)) // Mostrar solo los primeros 10
            {
                Console.WriteLine($"• {ciudadano}");
            }
            
            if (soloPfizer.Count > 10)
            {
                Console.WriteLine($"... y {soloPfizer.Count - 10} ciudadanos más.");
            }
        }

        static void MostrarCiudadanosSoloAstraZeneca(ServicioVacunacion servicio)
        {
            Console.WriteLine("\n=== CIUDADANOS SOLO CON ASTRAZENECA ===");
            Console.WriteLine("Operación de conjuntos: A - P");
            Console.WriteLine("Donde A = AstraZeneca, P = Pfizer\n");

            var soloAstraZeneca = servicio.ObtenerCiudadanosSoloAstraZeneca();
            
            Console.WriteLine($"Total de ciudadanos solo con AstraZeneca: {soloAstraZeneca.Count}\n");
            
            foreach (var ciudadano in soloAstraZeneca.Take(10)) // Mostrar solo los primeros 10
            {
                Console.WriteLine($"• {ciudadano}");
            }
            
            if (soloAstraZeneca.Count > 10)
            {
                Console.WriteLine($"... y {soloAstraZeneca.Count - 10} ciudadanos más.");
            }
        }

        static void MostrarEstadisticasDetalladas(ServicioVacunacion servicio)
        {
            Console.WriteLine("\n=== ESTADÍSTICAS DETALLADAS ===");
            
            var estadisticas = servicio.ObtenerEstadisticas();
            Console.WriteLine(estadisticas);

            // Mostrar información adicional sobre conjuntos
            var conjuntoPfizer = servicio.ConjuntoCiudadanosConPfizer();
            var conjuntoAstraZeneca = servicio.ConjuntoCiudadanosConAstraZeneca();
            
            // Intersección: ciudadanos con ambos tipos de vacuna
            var interseccion = new HashSet<Ciudadano>(conjuntoPfizer);
            interseccion.IntersectWith(conjuntoAstraZeneca);
            
            Console.WriteLine($"Ciudadanos con ambos tipos de vacuna (P ∩ A): {interseccion.Count}");
            Console.WriteLine($"Ciudadanos solo con una vacuna: {conjuntoPfizer.Count + conjuntoAstraZeneca.Count - (2 * interseccion.Count)}");
        }

        static void MostrarOperacionesConjuntos(ServicioVacunacion servicio)
        {
            Console.WriteLine("\n=== DEMOSTRACIÓN DE OPERACIONES DE CONJUNTOS ===");
            Console.WriteLine("Teoría de conjuntos aplicada al sistema de vacunación\n");

            var universo = servicio.ConjuntoUniversalCiudadanos();
            var pfizer = servicio.ConjuntoCiudadanosConPfizer();
            var astrazeneca = servicio.ConjuntoCiudadanosConAstraZeneca();
            var ambasDosis = servicio.ConjuntoCiudadanosConAmbasDosis();

            Console.WriteLine("CONJUNTOS BASE:");
            Console.WriteLine($"• Universo (U): {universo.Count} ciudadanos");
            Console.WriteLine($"• Pfizer (P): {pfizer.Count} ciudadanos");
            Console.WriteLine($"• AstraZeneca (A): {astrazeneca.Count} ciudadanos");
            Console.WriteLine($"• Ambas dosis (D): {ambasDosis.Count} ciudadanos\n");

            // Unión
            var union = new HashSet<Ciudadano>(pfizer);
            union.UnionWith(astrazeneca);
            Console.WriteLine($"UNIÓN (P ∪ A): {union.Count} ciudadanos vacunados");

            // Intersección
            var interseccion = new HashSet<Ciudadano>(pfizer);
            interseccion.IntersectWith(astrazeneca);
            Console.WriteLine($"INTERSECCIÓN (P ∩ A): {interseccion.Count} ciudadanos con ambos tipos");

            // Diferencia Pfizer - AstraZeneca
            var diferenciaPfizer = new HashSet<Ciudadano>(pfizer);
            diferenciaPfizer.ExceptWith(astrazeneca);
            Console.WriteLine($"DIFERENCIA (P - A): {diferenciaPfizer.Count} ciudadanos solo Pfizer");

            // Diferencia AstraZeneca - Pfizer
            var diferenciaAstraZeneca = new HashSet<Ciudadano>(astrazeneca);
            diferenciaAstraZeneca.ExceptWith(pfizer);
            Console.WriteLine($"DIFERENCIA (A - P): {diferenciaAstraZeneca.Count} ciudadanos solo AstraZeneca");

            // Complemento
            var complemento = new HashSet<Ciudadano>(universo);
            complemento.ExceptWith(union);
            Console.WriteLine($"COMPLEMENTO (U - (P ∪ A)): {complemento.Count} ciudadanos no vacunados");

            // Diferencia simétrica
            var diferenciaSimetrica = new HashSet<Ciudadano>(pfizer);
            diferenciaSimetrica.SymmetricExceptWith(astrazeneca);
            Console.WriteLine($"DIFERENCIA SIMÉTRICA ((P - A) ∪ (A - P)): {diferenciaSimetrica.Count} ciudadanos con un solo tipo");

            Console.WriteLine("\nVERIFICACION DE IDENTIDADES:");
            Console.WriteLine($"• |P| + |A| - |P ∩ A| = |P ∪ A|: {pfizer.Count} + {astrazeneca.Count} - {interseccion.Count} = {union.Count} ✓");
            Console.WriteLine($"• |U| = |P ∪ A| + |U - (P ∪ A)|: {universo.Count} = {union.Count} + {complemento.Count} ✓");
        }
    }
}