using Inventory.Api.Test.Stubs;
using Inventory.Domain.Exceptions;
using Inventory.Domain.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;

namespace Inventory.Api.Test.Mocks
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
            Setup(x => x.ExistWarehouseAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                            .Returns((Guid id) => Task.FromResult(WarehouseStub.ExistWarehouseAsync(id)));

            Setup(x => x.ExistWarehouseAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                                        .Returns(() => throw new BusinessException("Warehouse doesn't exist"));
            return this;
        }
    }
}