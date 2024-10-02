using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealEstateMillion.Domain.Entities
{
    /// <summary>
    /// Representa una propiedad inmobiliaria en el sistema.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Identificador único de la propiedad.
        /// </summary>
        public Guid IdProperty { get; set; }

        /// <summary>
        /// Nombre o título de la propiedad.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dirección completa de la propiedad.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Precio actual de la propiedad.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Código interno único de la propiedad.
        /// </summary>
        public string CodeInternal { get; set; }

        /// <summary>
        /// Año de construcción de la propiedad.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Identificador del propietario de la propiedad.
        /// </summary>
        public Guid IdOwner { get; set; }

        /// <summary>
        /// Propietario de la propiedad.
        /// </summary>
        [JsonIgnore]
        public Owner Owner { get; set; }

        /// <summary>
        /// Colección de imágenes asociadas a la propiedad.
        /// </summary>
        public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

        /// <summary>
        /// Colección de trazas o registros históricos de la propiedad.
        /// </summary>
        public ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();

        /// <summary>
        /// Fecha y hora de creación del registro de la propiedad.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora de la última actualización del registro de la propiedad.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Añade una nueva imagen a la propiedad.
        /// </summary>
        /// <param name="imageUrl">URL de la imagen a añadir.</param>
        public void AddImage(string imageUrl)
        {
            PropertyImages.Add(new PropertyImage { File = imageUrl, Enabled = true });
        }

        /// <summary>
        /// Cambia el precio de la propiedad.
        /// </summary>
        /// <param name="newPrice">Nuevo precio de la propiedad.</param>
        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.", nameof(newPrice));

            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;

            PropertyTraces.Add(new PropertyTrace
            {
                DateSale = DateTime.UtcNow,
                Name = "Cambio de Precio",
                Value = newPrice,
                Tax = 0 // Asume que no hay impuesto en el cambio de precio
            });
        }
    }
}