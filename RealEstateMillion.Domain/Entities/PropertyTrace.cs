using System;

namespace RealEstateMillion.Domain.Entities
{
    /// <summary>
    /// Representa un registro histórico o traza de una propiedad inmobiliaria.
    /// </summary>
    public class PropertyTrace
    {
        /// <summary>
        /// Identificador único de la traza de la propiedad.
        /// </summary>
        public Guid IdPropertyTrace { get; set; }

        /// <summary>
        /// Fecha de la venta o transacción.
        /// </summary>
        public DateTime DateSale { get; set; }

        /// <summary>
        /// Nombre o descripción de la transacción.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Valor de la transacción.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Impuesto asociado a la transacción.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Identificador de la propiedad asociada a esta traza.
        /// </summary>
        public Guid IdProperty { get; set; }

        /// <summary>
        /// Propiedad asociada a esta traza.
        /// </summary>
        public Property Property { get; set; }

        /// <summary>
        /// Fecha y hora de creación del registro de la traza.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Calcula el valor total de la transacción, incluyendo impuestos.
        /// </summary>
        /// <returns>El valor total de la transacción.</returns>
        public decimal CalculateTotalValue()
        {
            return Value + Tax;
        }

        /// <summary>
        /// Crea una nueva instancia de PropertyTrace.
        /// </summary>
        public PropertyTrace()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Crea una nueva instancia de PropertyTrace con los detalles de la transacción.
        /// </summary>
        /// <param name="dateSale">Fecha de la venta o transacción.</param>
        /// <param name="name">Nombre o descripción de la transacción.</param>
        /// <param name="value">Valor de la transacción.</param>
        /// <param name="tax">Impuesto asociado a la transacción.</param>
        /// <param name="idProperty">Identificador de la propiedad asociada.</param>
        public PropertyTrace(DateTime dateSale, string name, decimal value, decimal tax, Guid idProperty)
        {
            DateSale = dateSale;
            Name = name;
            Value = value;
            Tax = tax;
            IdProperty = idProperty;
            CreatedAt = DateTime.UtcNow;
        }
    }
}