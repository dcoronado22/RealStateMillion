using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.Application.Interfaces
{
    public interface IOwnerService
    {
        Task<Owner> GetOwnerByIdAsync(Guid id);
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner> CreateOwnerAsync(Owner owner);
        Task UpdateOwnerAsync(Owner owner);
        Task DeleteOwnerAsync(Guid id);
    }
}