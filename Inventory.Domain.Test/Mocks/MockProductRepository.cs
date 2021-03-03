using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Test.Stubs;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Test.Mocks
{
    public class MockProductRepository : Mock<IGenericRepository<Product>>
    {
        public MockProductRepository GetAllProductAsync()
        {
            Setup(x => x.GetAllAsync()).Returns(() => Task.FromResult(ProductStub.GetAllProducts()));
            return this;
        }

        public MockProductRepository GetNotProductAsync()
        {
            Setup(x => x.GetAllAsync()).Returns(() => Task.FromResult<IEnumerable<Product>>(ProductStub.GetNotProductAsync()));
            return this;
        }

        public MockProductRepository GetProductByIdAsync()
        {
            Setup(x => x.GetByIdAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")) || e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")) )))
                                            .Returns((Guid id) => Task.FromResult(ProductStub.GetProductById(id)));

            Setup(x => x.GetByIdAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")) && !e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")))))
                                        .Returns(() => Task.FromResult<Product>(ProductStub.NoReturnProductById()));
            return this;
        }

        public MockProductRepository Queryable()
        {
            Setup(x => x.Queryable()).Returns(() => ProductStub.GetAllProductQueryable());
            return this;
        }

        public MockProductRepository ExistProductAsync()
        {
            Setup(x => x.ExistsAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")) || e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")))))
                                            .Returns((Guid id) => Task.FromResult(ProductStub.ExistProduct()));

            Setup(x => x.ExistsAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                        .Returns(() => Task.FromResult(ProductStub.NotExistProduct()));
            return this;
        }

        public MockProductRepository InsertProductAsync()
        {
            Setup(x => x.InsertAsync(It.IsAny<Product>()))
                        .Returns((Product product) => Task.FromResult(ProductStub.InsertProduct(product)));
            return this;
        }

        public MockProductRepository UpdateProductAsync()
        {
            Setup(x => x.UpdateAsync(It.IsAny<Product>()));
            return this;
        }

        public MockProductRepository DeleteProductAsync()
        {
            Setup(x => x.DeleteAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                            .Returns(() => Task.FromResult(ProductStub.DeleteProduct()));
            Setup(x => x.DeleteAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                            .Returns(() => Task.FromResult(ProductStub.NoDeleteProduct()));
            return this;
        }
    }
}