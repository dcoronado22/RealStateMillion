using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Owner> GetOwnerByIdAsync(Guid id)
        {
            return await _unitOfWork.OwnerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _unitOfWork.OwnerRepository.GetAllAsync();
        }

        public async Task<Owner> CreateOwnerAsync(Owner owner)
        {
            await _unitOfWork.OwnerRepository.CreateAsync(owner);
            await _unitOfWork.SaveChangesAsync();
            return owner;
        }

        public async Task UpdateOwnerAsync(Owner owner)
        {
            await _unitOfWork.OwnerRepository.UpdateAsync(owner);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOwnerAsync(Guid id)
        {
            await _unitOfWork.OwnerRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}