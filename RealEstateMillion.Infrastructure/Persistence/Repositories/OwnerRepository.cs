using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Domain.Entities;
using RealEstateMillion.Domain.Interfaces;
using RealEstateMillion.Infrastructure.Data;
using RealEstateMillion.Infrastructure.Persistence.Context;

namespace RealEstateMillion.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDbContext _context;

        public OwnerRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<Owner> GetByIdAsync(Guid id)
        {
            return await _context.Owners.FindAsync(id);
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner> CreateAsync(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            return owner;
        }

        public async Task UpdateAsync(Owner owner)
        {
            _context.Entry(owner).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
            }
        }
    }
}