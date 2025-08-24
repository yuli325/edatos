using System;
using System.Collections.Generic;
using System.Linq;
using SistemaVacunacionCovid.Models;

namespace SistemaVacunacionCovid.Services
{
    /// <summary>
    /// Servicio principal que maneja las operaciones de vacunación utilizando teoría de conjuntos
    /// </summary>
    public class ServicioVacunacion
    {
        private readonly List<Ciudadano> _todosCiudadanos;
        private readonly List<Vacuna> _todasVacunas;

        public ServicioVacunacion(List<Ciudadano> ciudadanos, List<Vacuna> vacunas)
        {
            _todosCiudadanos = ciudadanos ?? throw new ArgumentNullException(nameof(ciudadanos));
            _todasVacunas = vacunas ?? throw new ArgumentNullException(nameof(vacunas));
        }

        /// <summary>
        /// Obtiene el conjunto universal de todos los ciudadanos
        /// </summary>
        public HashSet<Ciudadano> ConjuntoUniversalCiudadanos()
        {
            return new HashSet<Ciudadano>(_todosCiudadanos);
        }

        /// <summary>
        /// Obtiene el conjunto de ciudadanos vacunados con Pfizer
        /// </summary>
        public HashSet<Ciudadano> ConjuntoCiudadanosConPfizer()
        {
            var idsConPfizer = _todasVacunas
                .Where(v => v.TipoVacuna == TipoVacuna.Pfizer)
                .Select(v => v.CiudadanoId)
                .Distinct();

            return new HashSet<Ciudadano>(
                _todosCiudadanos.Where(c => idsConPfizer.Contains(c.Id))
            );
        }

        /// <summary>
        /// Obtiene el conjunto de ciudadanos vacunados con AstraZeneca
        /// </summary>
        public HashSet<Ciudadano> ConjuntoCiudadanosConAstraZeneca()
        {
            var idsConAstraZeneca = _todasVacunas
                .Where(v => v.TipoVacuna == TipoVacuna.AstraZeneca)
                .Select(v => v.CiudadanoId)
                .Distinct();

            return new HashSet<Ciudadano>(
                _todosCiudadanos.Where(c => idsConAstraZeneca.Contains(c.Id))
            );
        }

        /// <summary>
        /// Obtiene el conjunto de ciudadanos con ambas dosis (cualquier tipo de vacuna)
        /// </summary>
        public HashSet<Ciudadano> ConjuntoCiudadanosConAmbasDosis()
        {
            var ciudadanosConAmbasDosis = _todasVacunas
                .GroupBy(v => v.CiudadanoId)
                .Where(g => g.Max(v => v.NumeroDosis) >= 2)
                .Select(g => g.Key);

            return new HashSet<Ciudadano>(
                _todosCiudadanos.Where(c => ciudadanosConAmbasDosis.Contains(c.Id))
            );
        }

        /// <summary>
        /// 1. Ciudadanos que NO se han vacunado
        /// Operación: U - (P ∪ A) donde U = universo, P = Pfizer, A = AstraZeneca
        /// </summary>
        public List<Ciudadano> ObtenerCiudadanosNoVacunados()
        {
            var conjuntoUniversal = ConjuntoUniversalCiudadanos();
            var conjuntoPfizer = ConjuntoCiudadanosConPfizer();
            var conjuntoAstraZeneca = ConjuntoCiudadanosConAstraZeneca();

            // Unión de vacunados con Pfizer y AstraZeneca
            var unionVacunados = new HashSet<Ciudadano>(conjuntoPfizer);
            unionVacunados.UnionWith(conjuntoAstraZeneca);

            // Diferencia: Universal - Vacunados
            conjuntoUniversal.ExceptWith(unionVacunados);

            return conjuntoUniversal.OrderBy(c => c.Id).ToList();
        }

        /// <summary>
        /// 2. Ciudadanos que han recibido ambas dosis
        /// Operación: Conjunto de ciudadanos con dosis máxima >= 2
        /// </summary>
        public List<Ciudadano> ObtenerCiudadanosConAmbasDosis()
        {
            var conjuntoAmbasDosis = ConjuntoCiudadanosConAmbasDosis();
            return conjuntoAmbasDosis.OrderBy(c => c.Id).ToList();
        }

        /// <summary>
        /// 3. Ciudadanos que SOLO han recibido la vacuna de Pfizer
        /// Operación: P - A donde P = Pfizer, A = AstraZeneca
        /// </summary>
        public List<Ciudadano> ObtenerCiudadanosSoloPfizer()
        {
            var conjuntoPfizer = ConjuntoCiudadanosConPfizer();
            var conjuntoAstraZeneca = ConjuntoCiudadanosConAstraZeneca();

            // Diferencia: Pfizer - AstraZeneca
            conjuntoPfizer.ExceptWith(conjuntoAstraZeneca);

            return conjuntoPfizer.OrderBy(c => c.Id).ToList();
        }

        /// <summary>
        /// 4. Ciudadanos que SOLO han recibido la vacuna de AstraZeneca
        /// Operación: A - P donde A = AstraZeneca, P = Pfizer
        /// </summary>
        public List<Ciudadano> ObtenerCiudadanosSoloAstraZeneca()
        {
            var conjuntoPfizer = ConjuntoCiudadanosConPfizer();
            var conjuntoAstraZeneca = ConjuntoCiudadanosConAstraZeneca();

            // Diferencia: AstraZeneca - Pfizer
            conjuntoAstraZeneca.ExceptWith(conjuntoPfizer);

            return conjuntoAstraZeneca.OrderBy(c => c.Id).ToList();
        }

        /// <summary>
        /// Obtiene estadísticas generales del sistema
        /// </summary>
        public EstadisticasVacunacion ObtenerEstadisticas()
        {
            var totalCiudadanos = _todosCiudadanos.Count;
            var noVacunados = ObtenerCiudadanosNoVacunados().Count;
            var conAmbasDosis = ObtenerCiudadanosConAmbasDosis().Count;
            var soloPfizer = ObtenerCiudadanosSoloPfizer().Count;
            var soloAstraZeneca = ObtenerCiudadanosSoloAstraZeneca().Count;
            var vacunadosTotal = totalCiudadanos - noVacunados;

            return new EstadisticasVacunacion
            {
                TotalCiudadanos = totalCiudadanos,
                CiudadanosVacunados = vacunadosTotal,
                CiudadanosNoVacunados = noVacunados,
                CiudadanosConAmbasDosis = conAmbasDosis,
                CiudadanosSoloPfizer = soloPfizer,
                CiudadanosSoloAstraZeneca = soloAstraZeneca,
                PorcentajeVacunacion = (double)vacunadosTotal / totalCiudadanos * 100,
                PorcentajeAmbasDosis = (double)conAmbasDosis / totalCiudadanos * 100
            };
        }
    }

    /// <summary>
    /// Clase para encapsular las estadísticas de vacunación
    /// </summary>
    public class EstadisticasVacunacion
    {
        public int TotalCiudadanos { get; set; }
        public int CiudadanosVacunados { get; set; }
        public int CiudadanosNoVacunados { get; set; }
        public int CiudadanosConAmbasDosis { get; set; }
        public int CiudadanosSoloPfizer { get; set; }
        public int CiudadanosSoloAstraZeneca { get; set; }
        public double PorcentajeVacunacion { get; set; }
        public double PorcentajeAmbasDosis { get; set; }

        public override string ToString()
        {
            return $@"
=== ESTADÍSTICAS DE VACUNACIÓN COVID-19 ===
Total de ciudadanos: {TotalCiudadanos}
Ciudadanos vacunados: {CiudadanosVacunados} ({PorcentajeVacunacion:F1}%)
Ciudadanos no vacunados: {CiudadanosNoVacunados}
Ciudadanos con ambas dosis: {CiudadanosConAmbasDosis} ({PorcentajeAmbasDosis:F1}%)
Ciudadanos solo con Pfizer: {CiudadanosSoloPfizer}
Ciudadanos solo con AstraZeneca: {CiudadanosSoloAstraZeneca}
";
        }
    }
}