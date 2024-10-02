using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateMillion.Domain.Entities;
using RealEstateMillion.Domain.Interfaces;
using RealEstateMillion.Infrastructure.Data;
using RealEstateMillion.Infrastructure.Persistence.Context;

namespace RealEstateMillion.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly RealEstateDbContext _context;

        public PropertyRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public IQueryable<Property> GetQueryable()
        {
            return _context.Properties;
        }

        public async Task<Property> GetByIdAsync(Guid id)
        {
            return await _context.Properties
                .Include(p => p.PropertyImages)
                .Include(p => p.PropertyTraces)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.IdProperty == id) ?? new Property();
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(p => p.PropertyImages)
                .Include(p => p.PropertyTraces)
                .Include(p => p.Owner)
                .ToListAsync();
        }

        public async Task<Property> CreateAsync(Property property)
        {
            property.CreatedAt = DateTime.UtcNow;
            await _context.Properties.AddAsync(property);
            return property;
        }

        public async Task UpdateAsync(Property property)
        {
            property.UpdatedAt = DateTime.UtcNow;
            _context.Entry(property).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
            }
        }

        public async Task<IEnumerable<Property>> GetWithFiltersAsync(PropertyFilter filter)
        {
            var query = _context.Properties
                .Include(p => p.PropertyImages)
                .Include(p => p.PropertyTraces)
                .Include(p => p.Owner)
                .AsQueryable();

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(filter.City))
                query = query.Where(p => p.Address.Contains(filter.City));

            if (!string.IsNullOrWhiteSpace(filter.State))
                query = query.Where(p => p.Address.Contains(filter.State));

            if (filter.Year.HasValue)
                query = query.Where(p => p.Year == filter.Year.Value);

            return await query.ToListAsync();
        }
    }
}