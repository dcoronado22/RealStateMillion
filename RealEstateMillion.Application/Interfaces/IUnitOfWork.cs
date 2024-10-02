using System;
using System.Threading.Tasks;
using RealEstateMillion.Domain.Interfaces;

namespace RealEstateMillion.Application.Interfaces
{
    /// <summary>
    /// Define el contrato para las operaciones de unidad de trabajo.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Obtiene el repositorio de propiedades.
        /// </summary>
        IPropertyRepository PropertyRepository { get; }

        /// <summary>
        /// Obtiene el repositorio de owner.
        /// </summary>
        IOwnerRepository OwnerRepository { get; }

        /// <summary>
        /// Guarda todos los cambios realizados en el contexto.
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Inicia una nueva transacción.
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Confirma la transacción actual.
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// Revierte la transacción actual.
        /// </summary>
        Task RollbackTransactionAsync();
    }
}