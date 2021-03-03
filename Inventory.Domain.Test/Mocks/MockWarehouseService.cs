using Inventory.Domain.Exceptions;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Test.Stubs;
using Moq;
using System;
using System.Threading.Tasks;

namespace Inventory.Domain.Test.Mocks
{
    public class MockWarehouseService : Mock<IWarehouseService>
    {
        public MockWarehouseService GetWarehousesAsync()
        {
            Setup(x => x.GetWarehousesAsync()).Returns(() => Task.FromResult(WarehouseStub.GetAllWarehouse()));
            return this;
        }

        public MockWarehouseService ExistWarehouseAsync()
        {
            Setup(x => x.ExistWarehouseAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")) || e.Equals(Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")))))
                                            .Returns((Guid id) => Task.FromResult(WarehouseStub.ExistWarehouseAsync()));

            Setup(x => x.ExistWarehouseAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")) && !e.Equals(Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")))))
                                        .Returns(() => throw new BusinessException("Warehouse doesn't exist"));
            return this;
        }
    }
}