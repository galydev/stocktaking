using Inventory.Domain.CustomEntities;
using Inventory.Domain.Entities;
using Inventory.Domain.Exceptions;
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
    public class MovementServiceTest
    {
        [Fact]
        public async Task When_Remove_Products_Of_Warehouse_Is_No_Posible_Remove_Product_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();
            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            RemoveProductWarehouse movement = new RemoveProductWarehouse
            {
                Quantity = 35,
                ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                WarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
            };
            Exception exception = null;

            //Act
            try
            {
                await movementService.RemoveProductsOfWarehouse(movement);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("It is not possible to remove, the warehouse only contains 14 of this product" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Fact]
        public async Task When_Remove_Products_Of_Warehouse_Is_Correct_Remove_Product_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();
            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            RemoveProductWarehouse movement = new RemoveProductWarehouse
            {
                Quantity = 2,
                ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                WarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")
            };

            //Act
            var result = await movementService.RemoveProductsOfWarehouse(movement);

            //Assert
            Assert.True(result.Equals(true));
        }

        [Theory]
        [InlineData("B99BC862-C515-4D61-97D8-4D2B188A19F3")]
        public async Task When_Get_Total_Shopping_Of_Product(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();

            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await movementService.GetTotalShoppingProduct(Guid.Parse(id));

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ShoppingQuantity.Equals(27));
            Assert.True(result.TotalPriceShopping.Equals(7283.70M));
        }

        [Theory]
        [InlineData("ECAF734D-2F6D-48A1-9513-8D5E76AB427A")]
        public async Task When_Error_Get_Total_Shopping_Of_Product(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();

            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            Exception exception = null;
            //Act
            try
            {
                await movementService.GetTotalShoppingProduct(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("The product is out of stock and purchases" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Fact]
        public async Task When_Move_Product_Other_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();

            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            MoveProducts moveProducts = new MoveProducts
            {
                Quantity = 3,
                ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                CurrentWarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392"),
                NewWarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
            };

            //Act
            var result = await movementService.MoveProductOtherWarehouse(moveProducts);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public async Task When_Error_Move_Product_Other_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();
            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            MoveProducts moveProducts = new MoveProducts
            {
                Quantity = 23,
                ProductId = Guid.Parse("A16C605C-0A1A-4E63-857F-68019CE2575C"),
                CurrentWarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392"),
                NewWarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
            };

            Exception exception = null;
            //Act
            try
            {
                await movementService.MoveProductOtherWarehouse(moveProducts);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("product not found in the selected warehouse" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Fact]
        public async Task When_Error_Threshold_Products_In_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();
            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            Movement movement = new Movement
            {
                Type = true,
                Quantity = 11000,
                ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                WarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
            };

            Exception exception = null;
            //Act
            try
            {
                await movementService.InsertProductWareahouse(movement);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("No more products can be loaded, as they exceed the established threshold. For logistical reasons there is no place to store them." == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Fact]
        public async Task When_Error_Exceeds_Maximun_Limint_Products_In_Warehouse()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<MovementService>>();
            Mock<IGenericRepository<Movement>> repositoryMovementMock = new MockMovementRepository().Queryable();
            Mock<IGenericRepository<Product>> repositoryProductMock = new MockProductRepository()
                                                                            .GetProductByIdAsync()
                                                                            .UpdateProductAsync()
                                                                            .Queryable();
            var mockUnitOfWork = new MockUnitOfWork().MovementRepository(repositoryMovementMock)
                                                      .ProductRepository(repositoryProductMock)
                                                      .SaveChangesAsync();
            var movementService = new MovementService(mockUnitOfWork.Object, loggerMock.Object);

            Movement movement = new Movement
            {
                Type = true,
                Quantity = 325,
                ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                WarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
            };

            Exception exception = null;
            //Act
            try
            {
                await movementService.InsertProductWareahouse(movement);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Only 320 units of the selected product can be loaded." == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }
    }
}