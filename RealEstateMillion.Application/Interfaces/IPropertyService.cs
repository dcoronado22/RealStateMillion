using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<Property> GetPropertyByIdAsync(Guid id);
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property> CreatePropertyAsync(Property property);
        Task UpdatePropertyAsync(Property property);
        Task DeletePropertyAsync(Guid id);
        Task AddImageToPropertyAsync(Guid propertyId, string imageUrl);
        Task ChangePriceAsync(Guid propertyId, decimal newPrice);
        Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(PropertyFilter filter);
    }
}