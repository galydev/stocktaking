using Inventory.Api.Test.Stubs;
using Inventory.Domain.CustomEntities;
using Inventory.Domain.Entities;
using Inventory.Domain.Exceptions;
using Inventory.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Api.Test.Mocks
{
    public class MockProductService : Mock<IProductService>
    {
        public MockProductService GetAllProductAsync()
        {
            Setup(x => x.GetProductsAsync()).Returns(() => Task.FromResult(ProductStub.GetAllProducts()));
            return this;
        }

        public MockProductService GetNotProductAsync()
        {
            Setup(x => x.GetProductsAsync()).Returns(() => Task.FromResult<IEnumerable<Product>>(ProductStub.GetNotProductAsync()));
            return this;
        }

        public MockProductService GetProductByIdAsync()
        {
            Setup(x => x.GetProductByIdAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                            .Returns((Guid id) => Task.FromResult(ProductStub.GetProductById(id)));

            Setup(x => x.GetProductByIdAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                        .Returns(() => Task.FromResult<Product>(ProductStub.NoReturnProductById()));
            return this;
        }

        public MockProductService ExistProductAsync()
        {
            Setup(x => x.ExistProductAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")) || e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")))))
                                            .Returns((Guid id) => Task.FromResult(ProductStub.ExistProduct()));

            Setup(x => x.ExistProductAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                        .Returns(() => throw new BusinessException("Product doesn't exist"));
            return this;
        }

        public MockProductService InsertProductAsync()
        {
            Setup(x => x.InsertProductAsync(It.IsAny<CreateProduct>()))
                        .Returns((Product product) => Task.FromResult(ProductStub.InsertProduct(product)));
            return this;
        }

        public MockProductService UpdateProductAsync()
        {
            Setup(x => x.UpdateProductAsync(It.IsAny<Product>()));
            return this;
        }

        public MockProductService DeleteProductAsync()
        {
            Setup(x => x.DeleteAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                            .Returns(() => Task.FromResult(ProductStub.DeleteProduct()));
            Setup(x => x.DeleteAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                            .Returns(() => Task.FromResult(ProductStub.NoDeleteProduct()));
            return this;
        }
    }
}