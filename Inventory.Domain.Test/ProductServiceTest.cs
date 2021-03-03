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
    public class ProductServiceTest
    {
        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        [InlineData("7a56023f-4811-4a75-9768-6aaf7985bb1a")]
        public async Task When_Exist_Product(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await productService.ExistProductAsync(Guid.Parse(id));

            //Assert
            Assert.True(result.Equals(true));
        }

        [Theory]
        [InlineData("C20ADA54-54F9-4D10-98C7-29BA2F458E6A")]
        [InlineData("0E414B7C-74E7-48B5-AB1C-2D9CB818A754")]
        public async Task When_Not_Exist_Product_Service(string id)
        {
            //Arrange
            Exception exception = null;
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            try
            {
                await productService.ExistProductAsync(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Product doesn't exist" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        [InlineData("7a56023f-4811-4a75-9768-6aaf7985bb1a")]
        public async Task When_Get_Product_By_Id_Service(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().GetProductByIdAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await productService.GetProductByIdAsync(Guid.Parse(id));

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id.Equals(Guid.Parse(id)));
        }

        [Theory]
        [InlineData("862F07EE-F92F-4EFB-9308-0F4C8B03A976")]
        [InlineData("C347ED5D-1F33-49EE-A58D-B7F2310192A6")]
        public async Task When_No_Return_Product_By_Id_Service(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Exception exception = null;
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().GetProductByIdAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            try
            {
                await productService.GetProductByIdAsync(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Product doesn't exist" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Theory]
        [InlineData("Pera")]
        [InlineData("Cebolla")]
        public async Task When_Get_Product_By_Name_Service(string name)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().Queryable();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await productService.GetProductByNameAsync(name);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Name.Equals(name));
        }

        [Fact]
        public async Task When_Get_All_Product_Service()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().GetAllProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await productService.GetProductsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public async Task When_Get_All_Warehouse_Not_Records()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Exception exception = null;
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().GetNotProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock);
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            try
            {
                await productService.GetProductsAsync();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("No products records" == exception.Message);
        }

        [Fact]
        public async Task When_Insert_Product()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().InsertProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);
            
            var entity = new CreateProduct
            {
                Name = "Pepino",
                Description = "Pepino",
                Sku = "1234567890",
                Price = 320.1M,
                MinimunStock = 1,
                MaximumStock = 210,
                Stock = 0
            };

            //Act
            var result = await productService.InsertProductAsync(entity);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Parse("00000000-0000-0000-0000-000000000000"));
        }

        [Fact]
        public async Task When_No_Insert_Product_Is_Realy_Exist()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository()
                                                                    .InsertProductAsync()
                                                                    .Queryable();

            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);
            var entity = new CreateProduct
            {
                Name = "Cebolla",
                Description = "Cebolla",
                Sku = "1234567890",
                Price = 320.1M,
                MinimunStock = 1,
                MaximumStock = 210,
                Stock = 0
            };
            Exception exception = null;

            //Act
            try
            {
                await productService.InsertProductAsync(entity);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("There is already a product with this name." == exception.Message);
        }

        [Fact]
        public async Task When_Update_Product()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().UpdateProductAsync().ExistProductAsync();

            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);
            var entity = new Product
            {
                Id = Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc"),
                Name = "Pepino",
                Description = "Pepino",
                Sku = "1234567890",
                Price = 320.1M,
                MinimunStock = 1,
                MaximumStock = 210,
                Stock = 0
            };

            //Act
            var result = await productService.UpdateProductAsync(entity);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        [InlineData("7a56023f-4811-4a75-9768-6aaf7985bb1a")]
        public async Task When_Delete_Product(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().DeleteProductAsync().ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await productService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.True(result.Equals(true));
        }

        [Theory]
        [InlineData("862F07EE-F92F-4EFB-9308-0F4C8B03A976")]
        [InlineData("C347ED5D-1F33-49EE-A58D-B7F2310192A6")]
        public async Task When_Error_Delete_Product(string id)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().DeleteProductAsync().ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);
            Exception exception = null;

            //Act
            try
            {
                await productService.DeleteAsync(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Product doesn't exist" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Fact]
        public async Task When_Error_Update_Product()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository().UpdateProductAsync().ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);
            Exception exception = null;

            var entity = new Product
            {
                Id = Guid.Parse("862F07EE-F92F-4EFB-9308-0F4C8B03A976"),
                Name = "Pepino",
                Description = "Pepino",
                Sku = "1234567890",
                Price = 320.1M,
                MinimunStock = 1,
                MaximumStock = 210,
                Stock = 0
            };

            //Act
            try
            {
                await productService.UpdateProductAsync(entity);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Product doesn't exist" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }

        [Theory]
        [InlineData("4d8830e2-465c-4a54-ad02-f875073c85dc")]
        public async Task When_Update_Stock_Product(string id)
        {
            //Arrange
            var stockValue = 320;
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository()
                                                                    .GetProductByIdAsync()
                                                                    .UpdateProductAsync()
                                                                    .ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);

            //Act
            var result = await productService.UpdateStockProduct(Guid.Parse(id), stockValue);

            //Assert
            Assert.True(result.Equals(true));
        }

        [Theory]
        [InlineData("C347ED5D-1F33-49EE-A58D-B7F2310192A6")]
        public async Task When_Error_Update_Stock_Product(string id)
        {
            //Arrange

            var stockValue = 23;
            var loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IGenericRepository<Product>> repositoryMock = new MockProductRepository()
                                                                    .GetProductByIdAsync()
                                                                    .UpdateProductAsync()
                                                                    .ExistProductAsync();
            var mockUnitOfWork = new MockUnitOfWork().ProductRepository(repositoryMock).SaveChangesAsync();
            var productService = new ProductService(mockUnitOfWork.Object, loggerMock.Object);
            Exception exception = null;
            //Act
            try
            {
                await productService.UpdateStockProduct(Guid.Parse(id), stockValue);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            Assert.True("Product doesn't exist" == exception.Message);
            Assert.True(typeof(BusinessException) == exception.GetType());
        }
    }
}