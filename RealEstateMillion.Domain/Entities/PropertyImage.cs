using System;

namespace RealEstateMillion.Domain.Entities
{
    /// <summary>
    /// Representa una imagen asociada a una propiedad inmobiliaria.
    /// </summary>
    public class PropertyImage
    {
        /// <summary>
        /// Identificador único de la imagen de la propiedad.
        /// </summary>
        public Guid IdPropertyImage { get; set; }

        /// <summary>
        /// Identificador de la propiedad a la que pertenece esta imagen.
        /// </summary>
        public Guid IdProperty { get; set; }

        /// <summary>
        /// URL o ruta del archivo de la imagen.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Indica si la imagen está habilitada para su visualización.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Indica si esta es la imagen principal de la propiedad.
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Orden de visualización de la imagen.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Fecha y hora de creación del registro de la imagen.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora de la última actualización del registro de la imagen.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Propiedad de navegación hacia la propiedad asociada.
        /// </summary>
        public Property Property { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public PropertyImage()
        {
            CreatedAt = DateTime.UtcNow;
            Enabled = true;
            IsPrimary = false;
        }

        /// <summary>
        /// Constructor con parámetros para crear una nueva imagen de propiedad.
        /// </summary>
        /// <param name="idProperty">ID de la propiedad asociada.</param>
        /// <param name="file">URL o ruta del archivo de la imagen.</param>
        /// <param name="isPrimary">Indica si es la imagen principal.</param>
        /// <param name="displayOrder">Orden de visualización de la imagen.</param>
        public PropertyImage(Guid idProperty, string file, bool isPrimary = false, int displayOrder = 0)
        {
            IdProperty = idProperty;
            File = file;
            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
            Enabled = true;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Habilita o deshabilita la imagen.
        /// </summary>
        /// <param name="enabled">True para habilitar, False para deshabilitar.</param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Establece esta imagen como la imagen principal de la propiedad.
        /// </summary>
        public void SetAsPrimary()
        {
            IsPrimary = true;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Actualiza el orden de visualización de la imagen.
        /// </summary>
        /// <param name="newOrder">Nuevo orden de visualización.</param>
        public void UpdateDisplayOrder(int newOrder)
        {
            if (newOrder < 0)
                throw new ArgumentException("El orden de visualización no puede ser negativo.", nameof(newOrder));

            DisplayOrder = newOrder;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}