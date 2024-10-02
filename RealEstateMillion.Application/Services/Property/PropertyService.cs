using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Property> GetPropertyByIdAsync(Guid id)
        {
            return await _unitOfWork.PropertyRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await _unitOfWork.PropertyRepository.GetAllAsync();
        }

        public async Task<Property> CreatePropertyAsync(Property property)
        {
            await _unitOfWork.PropertyRepository.CreateAsync(property);
            await _unitOfWork.SaveChangesAsync();
            return property;
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            await _unitOfWork.PropertyRepository.UpdateAsync(property);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePropertyAsync(Guid id)
        {
            await _unitOfWork.PropertyRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddImageToPropertyAsync(Guid propertyId, string imageUrl)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(propertyId);
            if (property == null)
                throw new KeyNotFoundException("Property not found");

            property.AddImage(imageUrl);
            await _unitOfWork.PropertyRepository.UpdateAsync(property);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ChangePriceAsync(Guid propertyId, decimal newPrice)
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(propertyId);
            if (property == null)
                throw new KeyNotFoundException("Property not found");

            property.ChangePrice(newPrice);
            await _unitOfWork.PropertyRepository.UpdateAsync(property);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(PropertyFilter filter)
        {
            // Comenzamos con una consulta IQueryable
            var query = _unitOfWork.PropertyRepository.GetQueryable();

            // Aplicamos los filtros
            if (!string.IsNullOrWhiteSpace(filter.City))
                query = query.Where(p => p.Address.Contains(filter.City));

            if (!string.IsNullOrWhiteSpace(filter.State))
                query = query.Where(p => p.Address.Contains(filter.State));

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.Year.HasValue)
                query = query.Where(p => p.Year == filter.Year.Value);

            // Aplicamos otros filtros según sea necesario

            // Ejecutamos la consulta y devolvemos los resultados
            return query.ToList();
        }
    }
}