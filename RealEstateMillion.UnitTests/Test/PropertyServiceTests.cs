using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Application.Services;
using RealEstateMillion.Domain.Entities;
using RealEstateMillion.Domain.Interfaces;

namespace RealEstateMillion.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private IUnitOfWork _unitOfWork;
        private IPropertyRepository _propertyRepository;
        private PropertyService _propertyService;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _propertyRepository = Substitute.For<IPropertyRepository>();
            _unitOfWork.PropertyRepository.Returns(_propertyRepository);
            _propertyService = new PropertyService(_unitOfWork);
        }

        [Test]
        public async Task GetPropertyByIdAsync_ShouldReturnProperty_WhenPropertyExists()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var expectedProperty = new Property { IdProperty = propertyId, Name = "Test Property" };
            _propertyRepository.GetByIdAsync(propertyId).Returns(expectedProperty);

            // Act
            var result = await _propertyService.GetPropertyByIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IdProperty, Is.EqualTo(expectedProperty.IdProperty));
            Assert.That(result.Name, Is.EqualTo(expectedProperty.Name));
        }

        [Test]
        public async Task CreatePropertyAsync_ShouldReturnCreatedProperty()
        {
            // Arrange
            var newProperty = new Property { Name = "New Property" };
            _propertyRepository.CreateAsync(Arg.Any<Property>()).Returns(newProperty);
            _unitOfWork.SaveChangesAsync().Returns(1);

            // Act
            var result = await _propertyService.CreatePropertyAsync(newProperty);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(newProperty.Name));
            await _propertyRepository.Received(1).CreateAsync(Arg.Any<Property>());
            await _unitOfWork.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task UpdatePropertyAsync_ShouldUpdateProperty()
        {
            // Arrange
            var propertyToUpdate = new Property { IdProperty = Guid.NewGuid(), Name = "Updated Property" };
            _unitOfWork.SaveChangesAsync().Returns(1);

            // Act
            await _propertyService.UpdatePropertyAsync(propertyToUpdate);

            // Assert
            await _propertyRepository.Received(1).UpdateAsync(Arg.Any<Property>());
            await _unitOfWork.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task DeletePropertyAsync_ShouldDeleteProperty()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            _unitOfWork.SaveChangesAsync().Returns(1);

            // Act
            await _propertyService.DeletePropertyAsync(propertyId);

            // Assert
            await _propertyRepository.Received(1).DeleteAsync(propertyId);
            await _unitOfWork.Received(1).SaveChangesAsync();
        }

    }
}