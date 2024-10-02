using Microsoft.EntityFrameworkCore;
using RealEstateMillion.Domain.Entities;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace RealEstateMillion.Infrastructure.Persistence.Context
{
    /// <summary>
    /// Contexto de base de datos para la aplicación RealEstateMillion.
    /// </summary>
    public class RealEstateDbContext : DbContext
    {
        /// <summary>
        /// Constructor del contexto de base de datos.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Conjunto de datos de Propiedades.
        /// </summary>
        public DbSet<Property> Properties { get; set; }

        /// <summary>
        /// Conjunto de datos de Propietarios.
        /// </summary>
        public DbSet<Owner> Owners { get; set; }

        /// <summary>
        /// Conjunto de datos de Imágenes de Propiedades.
        /// </summary>
        public DbSet<PropertyImage> PropertyImages { get; set; }

        /// <summary>
        /// Conjunto de datos de Trazas de Propiedades.
        /// </summary>
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        /// <summary>
        /// Configura el modelo de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todas las configuraciones definidas en el ensamblado actual
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuración específica para la entidad Property
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.IdProperty);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.CodeInternal).HasMaxLength(50);
                entity.HasIndex(e => e.CodeInternal).IsUnique();

                entity.HasOne(e => e.Owner)
                      .WithMany(o => o.Properties)
                      .HasForeignKey(e => e.IdOwner)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración específica para la entidad Owner
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.Photo).HasMaxLength(255);
                entity.Property(e => e.Birthday).HasColumnType("date");
            });

            // Configuración específica para la entidad PropertyImage
            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.HasKey(e => e.IdPropertyImage);
                entity.Property(e => e.File).IsRequired().HasMaxLength(255);

                entity.HasOne(e => e.Property)
                      .WithMany(p => p.PropertyImages)
                      .HasForeignKey(e => e.IdProperty)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración específica para la entidad PropertyTrace
            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Tax).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Property)
                      .WithMany(p => p.PropertyTraces)
                      .HasForeignKey(e => e.IdProperty)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        /// <summary>
        /// Configura las opciones del contexto.
        /// </summary>
        /// <param name="optionsBuilder">Constructor de opciones.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configuración de fallback si no se proporciona una cadena de conexión
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }
    }
}