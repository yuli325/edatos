using System;

namespace SistemaVacunacionCovid.Models
{
    /// <summary>
    /// Representa una dosis de vacuna aplicada a un ciudadano
    /// </summary>
    public class Vacuna
    {
        public int Id { get; set; }
        public int CiudadanoId { get; set; }
        public TipoVacuna TipoVacuna { get; set; }
        public int NumeroDosis { get; set; } // 1 para primera dosis, 2 para segunda dosis
        public DateTime FechaAplicacion { get; set; }
        public string LugarAplicacion { get; set; } = string.Empty;
        public string LoteVacuna { get; set; } = string.Empty;

        public Vacuna()
        {
        }

        public Vacuna(int id, int ciudadanoId, TipoVacuna tipoVacuna, int numeroDosis, DateTime fechaAplicacion, string lugarAplicacion, string loteVacuna)
        {
            Id = id;
            CiudadanoId = ciudadanoId;
            TipoVacuna = tipoVacuna;
            NumeroDosis = numeroDosis;
            FechaAplicacion = fechaAplicacion;
            LugarAplicacion = lugarAplicacion;
            LoteVacuna = loteVacuna;
        }

        public override string ToString()
        {
            return $"Ciudadano ID: {CiudadanoId}, Vacuna: {TipoVacuna}, Dosis: {NumeroDosis}, Fecha: {FechaAplicacion:dd/MM/yyyy}";
        }
    }

    /// <summary>
    /// Enumeración de tipos de vacunas disponibles
    /// </summary>
    public enum TipoVacuna
    {
        Pfizer,
        AstraZeneca
    }
}