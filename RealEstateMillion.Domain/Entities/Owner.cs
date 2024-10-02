using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealEstateMillion.Domain.Entities
{
    /// <summary>
    /// Representa a un propietario de bienes inmuebles en el sistema.
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// Identificador único del propietario.
        /// </summary>
        public Guid IdOwner { get; set; }

        /// <summary>
        /// Nombre completo del propietario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dirección del propietario.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// URL de la foto del propietario.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Fecha de nacimiento del propietario.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Colección de propiedades que pertenecen a este propietario.
        /// </summary>
        [JsonIgnore]
        public ICollection<Property> Properties { get; set; } = new List<Property>();

        /// <summary>
        /// Fecha y hora de creación del registro del propietario.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora de la última actualización del registro del propietario.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Owner()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor con parámetros para crear un nuevo propietario.
        /// </summary>
        /// <param name="name">Nombre completo del propietario.</param>
        /// <param name="address">Dirección del propietario.</param>
        /// <param name="birthday">Fecha de nacimiento del propietario.</param>
        public Owner(string name, string address, DateTime birthday)
        {
            Name = name;
            Address = address;
            Birthday = birthday;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Calcula la edad actual del propietario.
        /// </summary>
        /// <returns>La edad del propietario en años.</returns>
        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - Birthday.Year;
            if (Birthday.Date > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Añade una nueva propiedad a la colección de propiedades del propietario.
        /// </summary>
        /// <param name="property">La propiedad a añadir.</param>
        public void AddProperty(Property property)
        {
            property.IdOwner = this.IdOwner;
            Properties.Add(property);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Actualiza la información del propietario.
        /// </summary>
        /// <param name="name">Nuevo nombre del propietario (opcional).</param>
        /// <param name="address">Nueva dirección del propietario (opcional).</param>
        /// <param name="photo">Nueva URL de la foto del propietario (opcional).</param>
        public void UpdateInformation(string name = null, string address = null, string photo = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;

            if (!string.IsNullOrWhiteSpace(address))
                Address = address;

            if (!string.IsNullOrWhiteSpace(photo))
                Photo = photo;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}