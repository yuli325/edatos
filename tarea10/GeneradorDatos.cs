using System;
using System.Collections.Generic;
using System.Linq;
using SistemaVacunacionCovid.Models;

namespace SistemaVacunacionCovid.Services
{
    /// <summary>
    /// Clase responsable de generar datos ficticios para el sistema
    /// </summary>
    public class GeneradorDatos
    {
        private readonly Random _random;
        private readonly string[] _nombresComunes;
        private readonly string[] _apellidosComunes;
        private readonly string[] _lugaresVacunacion;

        public GeneradorDatos()
        {
            _random = new Random(12345); // Semilla fija para resultados reproducibles

            _nombresComunes = new string[]
            {
                "Juan", "María", "Carlos", "Ana", "Luis", "Carmen", "José", "Isabel", "Miguel", "Elena",
                "Antonio", "Pilar", "Francisco", "Rosa", "Manuel", "Dolores", "David", "Concepción", "Javier", "Mercedes",
                "Jesús", "Josefa", "Alejandro", "Teresa", "Fernando", "Francisca", "Sergio", "Antonia", "Rafael", "María José",
                "Pablo", "Cristina", "Adrián", "Laura", "Álvaro", "Marta", "Diego", "María Carmen", "Rubén", "Patricia"
            };

            _apellidosComunes = new string[]
            {
                "García", "Rodríguez", "González", "Fernández", "López", "Martínez", "Sánchez", "Pérez", "Gómez", "Martín",
                "Jiménez", "Ruiz", "Hernández", "Díaz", "Moreno", "Muñoz", "Álvarez", "Romero", "Alonso", "Gutiérrez",
                "Navarro", "Torres", "Domínguez", "Vázquez", "Ramos", "Gil", "Ramírez", "Serrano", "Blanco", "Suárez"
            };

            _lugaresVacunacion = new string[]
            {
                "Hospital Central", "Centro de Salud Norte", "Polideportivo Municipal", "Centro de Convenciones",
                "Hospital Regional", "Centro de Salud Sur", "Estadio Municipal", "Centro Comunitario",
                "Clínica Privada", "Centro de Salud Este", "Pabellón Deportivo", "Casa de la Cultura"
            };
        }

        /// <summary>
        /// Genera una lista de ciudadanos ficticios
        /// </summary>
        public List<Ciudadano> GenerarCiudadanos(int cantidad)
        {
            var ciudadanos = new List<Ciudadano>();

            for (int i = 1; i <= cantidad; i++)
            {
                var nombre = GenerarNombreCompleto();
                var documento = GenerarDocumento();
                var fechaNacimiento = GenerarFechaNacimiento();
                var email = GenerarEmail(nombre);
                var telefono = GenerarTelefono();

                ciudadanos.Add(new Ciudadano(i, nombre, documento, fechaNacimiento, email, telefono));
            }

            return ciudadanos;
        }

        /// <summary>
        /// Genera vacunas para ciudadanos específicos con Pfizer
        /// </summary>
        public List<Vacuna> GenerarVacunasPfizer(List<int> ciudadanosIds)
        {
            var vacunas = new List<Vacuna>();
            int vacunaId = 1;

            foreach (var ciudadanoId in ciudadanosIds)
            {
                // Primera dosis
                var fechaPrimeraDosis = GenerarFechaVacunacion();
                vacunas.Add(new Vacuna(
                    vacunaId++,
                    ciudadanoId,
                    TipoVacuna.Pfizer,
                    1,
                    fechaPrimeraDosis,
                    _lugaresVacunacion[_random.Next(_lugaresVacunacion.Length)],
                    $"PF{_random.Next(1000, 9999)}"
                ));

                // Algunos tendrán segunda dosis (aproximadamente 80%)
                if (_random.NextDouble() < 0.8)
                {
                    var fechaSegundaDosis = fechaPrimeraDosis.AddDays(_random.Next(21, 42));
                    vacunas.Add(new Vacuna(
                        vacunaId++,
                        ciudadanoId,
                        TipoVacuna.Pfizer,
                        2,
                        fechaSegundaDosis,
                        _lugaresVacunacion[_random.Next(_lugaresVacunacion.Length)],
                        $"PF{_random.Next(1000, 9999)}"
                    ));
                }
            }

            return vacunas;
        }

        /// <summary>
        /// Genera vacunas para ciudadanos específicos con AstraZeneca
        /// </summary>
        public List<Vacuna> GenerarVacunasAstraZeneca(List<int> ciudadanosIds)
        {
            var vacunas = new List<Vacuna>();
            int vacunaId = 10000; // Empezar con un ID diferente para evitar conflictos

            foreach (var ciudadanoId in ciudadanosIds)
            {
                // Primera dosis
                var fechaPrimeraDosis = GenerarFechaVacunacion();
                vacunas.Add(new Vacuna(
                    vacunaId++,
                    ciudadanoId,
                    TipoVacuna.AstraZeneca,
                    1,
                    fechaPrimeraDosis,
                    _lugaresVacunacion[_random.Next(_lugaresVacunacion.Length)],
                    $"AZ{_random.Next(1000, 9999)}"
                ));

                // Algunos tendrán segunda dosis (aproximadamente 75%)
                if (_random.NextDouble() < 0.75)
                {
                    var fechaSegundaDosis = fechaPrimeraDosis.AddDays(_random.Next(28, 84));
                    vacunas.Add(new Vacuna(
                        vacunaId++,
                        ciudadanoId,
                        TipoVacuna.AstraZeneca,
                        2,
                        fechaSegundaDosis,
                        _lugaresVacunacion[_random.Next(_lugaresVacunacion.Length)],
                        $"AZ{_random.Next(1000, 9999)}"
                    ));
                }
            }

            return vacunas;
        }

        private string GenerarNombreCompleto()
        {
            var nombre = _nombresComunes[_random.Next(_nombresComunes.Length)];
            var apellido1 = _apellidosComunes[_random.Next(_apellidosComunes.Length)];
            var apellido2 = _apellidosComunes[_random.Next(_apellidosComunes.Length)];
            return $"{nombre} {apellido1} {apellido2}";
        }

        private string GenerarDocumento()
        {
            return _random.Next(10000000, 99999999).ToString();
        }

        private DateTime GenerarFechaNacimiento()
        {
            var inicioRango = new DateTime(1940, 1, 1);
            var finRango = new DateTime(2010, 12, 31);
            var rango = (finRango - inicioRango).Days;
            return inicioRango.AddDays(_random.Next(rango));
        }

        private string GenerarEmail(string nombre)
        {
            var dominios = new[] { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "gov.ar" };
            var nombreLimpio = nombre.Replace(" ", "").ToLower();
            var numeroAleatorio = _random.Next(1, 999);
            return $"{nombreLimpio}{numeroAleatorio}@{dominios[_random.Next(dominios.Length)]}";
        }

        private string GenerarTelefono()
        {
            return $"011-{_random.Next(1000, 9999)}-{_random.Next(1000, 9999)}";
        }

        private DateTime GenerarFechaVacunacion()
        {
            var inicioVacunacion = new DateTime(2021, 3, 1);
            var finVacunacion = new DateTime(2024, 6, 30);
            var rango = (finVacunacion - inicioVacunacion).Days;
            return inicioVacunacion.AddDays(_random.Next(rango));
        }
    }
}