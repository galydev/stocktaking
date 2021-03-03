using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Domain.Test.Mocks
{
    public class MockUnitOfWork : Mock<IUnitOfWork>
    {
        public MockUnitOfWork WarehoueseRepository(Mock<IGenericRepository<Warehouse>> mock)
        {
            Setup(x => x.WarehoueseRepository).Returns(() => mock.Object);
            return this;
        }

        public MockUnitOfWork ProductRepository(Mock<IGenericRepository<Product>> mock)
        {
            Setup(x => x.ProductRepository).Returns(() => mock.Object);
            return this;
        }

        public MockUnitOfWork MovementRepository(Mock<IGenericRepository<Movement>> mock)
        {
            Setup(x => x.MovementRepository).Returns(() => mock.Object);
            return this;
        }

        public MockUnitOfWork SaveChangesAsync()
        {
            Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(1));
            return this;
        }
    }
}