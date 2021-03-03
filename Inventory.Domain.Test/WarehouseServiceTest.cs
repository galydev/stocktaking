using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Services;
using Inventory.Domain.Test.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Domain.Test
{
    [Collection("Warehouse Service Test")]
    public class WarehouseServiceTest
    {
        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        [InlineData("7a56023f-4811-4a75-9768-6aaf7985bb1a")]
        public async Task When_Exist_Warehouse_Service(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<WarehouseService>>();
            Mock<IGenericRepository<Warehouse>> repositoryMock = new MockWarehouseRespository().WarehouseExistsAsync();
            var mockUnitOfWork = new MockUnitOfWork().WarehoueseRepository(repositoryMock);
            var warehouseService = new WarehouseService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await warehouseService.ExistWarehouseAsync(Guid.Parse(id));

            //Assert
            Assert.True(result.Equals(true));
        }

        [Theory]
        [InlineData("C20ADA54-54F9-4D10-98C7-29BA2F458E6A")]
        [InlineData("0E414B7C-74E7-48B5-AB1C-2D9CB818A754")]
        public async Task When_Not_Exist_Warehouse_Service(string id)
        {
            //Arrange
            Exception exception = null;
            var loggerMock = new Mock<ILogger<WarehouseService>>();
            Mock<IGenericRepository<Warehouse>> repositoryMock = new MockWarehouseRespository().WarehouseExistsAsync();
            var mockUnitOfWork = new MockUnitOfWork().WarehoueseRepository(repositoryMock);
            var warehouseService = new WarehouseService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            try
            {
                await warehouseService.ExistWarehouseAsync(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Warehouse doesn't exist" == exception.Message);
        }

        [Fact]
        public async Task When_Get_All_Warehouse_Service()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<WarehouseService>>();
            Mock<IGenericRepository<Warehouse>> repositoryMock = new MockWarehouseRespository().GetAllWarehouse();
            var mockUnitOfWork = new MockUnitOfWork().WarehoueseRepository(repositoryMock);
            var warehouseService = new WarehouseService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await warehouseService.GetWarehousesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public async Task When_Get_All_Warehouse_Not_Records()
        {
            //Arrange
            Exception exception = null;
            var loggerMock = new Mock<ILogger<WarehouseService>>();
            Mock<IGenericRepository<Warehouse>> repositoryMock = new MockWarehouseRespository().GetNotAllWarehouse();
            var mockUnitOfWork = new MockUnitOfWork().WarehoueseRepository(repositoryMock);
            var warehouseService = new WarehouseService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            try
            {
                await warehouseService.GetWarehousesAsync();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("No warehouse records" == exception.Message);
        }

        [Fact]
        public async Task When_Insert_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<WarehouseService>>();
            Mock<IGenericRepository<Warehouse>> repositoryMock = new MockWarehouseRespository().InsertWarehouseAsync();
            var mockUnitOfWork = new MockUnitOfWork().WarehoueseRepository(repositoryMock).SaveChangesAsync();
            var warehouseService = new WarehouseService(mockUnitOfWork.Object, loggerMock.Object);
            var Warehouse = new Warehouse
            {
                Name = 6,
                Description = "string",
                Location = "string",
                MaximumCapacity = 0
            };

            //Act
            var result = await warehouseService.CreateWarehousesAsync(Warehouse);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Parse("00000000-0000-0000-0000-000000000000"));
        }

        [Fact]
        public async Task When_Not_Insert_Warehouse_Is_Realy_Exist()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<WarehouseService>>();
            Mock<IGenericRepository<Warehouse>> repositoryMock = new MockWarehouseRespository()
                                                                    .InsertWarehouseAsync()
                                                                    .Queryable();

            var mockUnitOfWork = new MockUnitOfWork().WarehoueseRepository(repositoryMock).SaveChangesAsync();
            var warehouseService = new WarehouseService(mockUnitOfWork.Object, loggerMock.Object);
            var Warehouse = new Warehouse
            {
                Name = 1,
                Description = "string",
                Location = "string",
                MaximumCapacity = 0
            };
            Exception exception = null;
            //Act
            try
            {
                await warehouseService.CreateWarehousesAsync(Warehouse);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("There is already a warehouse with this name." == exception.Message);
        }
    }
}