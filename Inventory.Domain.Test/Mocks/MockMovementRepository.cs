using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Test.Stubs;
using Moq;

namespace Inventory.Domain.Test.Mocks
{
    public class MockMovementRepository : Mock<IGenericRepository<Movement>>
    {
        public MockMovementRepository Queryable()
        {
            Setup(x => x.Queryable()).Returns(() => MovementStub.GetAllMovement());
            return this;
        }
    }
}