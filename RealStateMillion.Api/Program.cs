using Microsoft.EntityFrameworkCore;
using RealEstateMillion.Infrastructure.Data;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Infrastructure.Repositories;
using RealEstateMillion.Application.Services;
using RealEstateMillion.Domain.Interfaces;
using RealEstateMillion.Infrastructure.Persistence.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
}); ;

// Configure DbContext
builder.Services.AddDbContext<RealEstateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

// Register application services
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();