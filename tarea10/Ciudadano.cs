using System;

namespace SistemaVacunacionCovid.Models
{
    /// <summary>
    /// Representa un ciudadano en el sistema de vacunación COVID-19
    /// </summary>
    public class Ciudadano : IEquatable<Ciudadano>
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public Ciudadano()
        {
        }

        public Ciudadano(int id, string nombre, string documento, DateTime fechaNacimiento, string email, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Documento = documento;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Telefono = telefono;
        }

        /// <summary>
        /// Implementación de IEquatable para comparar ciudadanos por ID
        /// </summary>
        public bool Equals(Ciudadano? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Ciudadano);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Documento: {Documento}";
        }
    }
}