using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Domain.Interfaces;
using RealEstateMillion.Infrastructure.Persistence.Context;
using RealEstateMillion.Infrastructure.Repositories;

namespace RealEstateMillion.Infrastructure.Data
{
    /// <summary>
    /// Implementación de la unidad de trabajo.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealEstateDbContext _context;
        private IPropertyRepository _propertyRepository;
        private IOwnerRepository _ownerRepository;

        public UnitOfWork(RealEstateDbContext context)
        {
            _context = context;
        }

        public IPropertyRepository PropertyRepository =>
            _propertyRepository ??= new PropertyRepository(_context);

        public IOwnerRepository OwnerRepository =>  // Add this property
           _ownerRepository ??= new OwnerRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}