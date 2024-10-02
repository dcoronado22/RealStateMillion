using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.Domain.Interfaces
{
    /// <summary>
    /// Define las operaciones de acceso a datos para las propiedades.
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// Obtiene una propiedad por su ID.
        /// </summary>
        /// <param name="id">El ID de la propiedad a obtener.</param>
        /// <returns>La propiedad si se encuentra, o null si no existe.</returns>
        Task<Property> GetByIdAsync(Guid id);

        IQueryable<Property> GetQueryable();

        /// <summary>
        /// Obtiene todas las propiedades.
        /// </summary>
        /// <returns>Una colección de todas las propiedades.</returns>
        Task<IEnumerable<Property>> GetAllAsync();

        /// <summary>
        /// Crea una nueva propiedad en la base de datos.
        /// </summary>
        /// <param name="property">La propiedad a crear.</param>
        /// <returns>La propiedad creada con su ID asignado.</returns>
        Task<Property> CreateAsync(Property property);

        /// <summary>
        /// Actualiza una propiedad existente.
        /// </summary>
        /// <param name="property">La propiedad con los datos actualizados.</param>
        Task UpdateAsync(Property property);

        /// <summary>
        /// Elimina una propiedad por su ID.
        /// </summary>
        /// <param name="id">El ID de la propiedad a eliminar.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Obtiene propiedades aplicando filtros.
        /// </summary>
        /// <param name="filter">Los filtros a aplicar en la búsqueda.</param>
        /// <returns>Una colección de propiedades que cumplen con los filtros.</returns>
        Task<IEnumerable<Property>> GetWithFiltersAsync(PropertyFilter filter);
    }
}