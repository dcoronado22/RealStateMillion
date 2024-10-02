using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task<Owner> GetByIdAsync(Guid id);
        Task<IEnumerable<Owner>> GetAllAsync();
        Task<Owner> CreateAsync(Owner owner);
        Task UpdateAsync(Owner owner);
        Task DeleteAsync(Guid id);
    }
}