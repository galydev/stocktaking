using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Test.Stubs;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Test.Mocks
{
    public class MockWarehouseRespository : Mock<IGenericRepository<Warehouse>>
    {
        public MockWarehouseRespository WarehouseExistsAsync()
        {
            Setup(r => r.ExistsAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) || e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")) || e.Equals(Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")))))
                    .Returns(() => Task.FromResult(true));

            Setup(r => r.ExistsAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc")) && !e.Equals(Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a")))))
                .Returns(() => Task.FromResult(false));
            return this;
        }

        public MockWarehouseRespository GetAllWarehouse()
        {
            Setup(r => r.GetAllAsync()).Returns(() => Task.FromResult(WarehouseStub.GetAllWarehouse()));
            return this;
        }

        public MockWarehouseRespository GetNotAllWarehouse()
        {
            Setup(r => r.GetAllAsync()).Returns(() => Task.FromResult<IEnumerable<Warehouse>>(null));
            return this;
        }

        public MockWarehouseRespository InsertWarehouseAsync()
        {
            Setup(x => x.InsertAsync(It.IsAny<Warehouse>()))
                        .Returns((Warehouse warehouse) => Task.FromResult(WarehouseStub.InsertWarehouse(warehouse)));
            return this;
        }

        public MockWarehouseRespository Queryable()
        {
            Setup(x => x.Queryable()).Returns(() => WarehouseStub.GetAllWarehouseQueryable());
            return this;
        }
    }
}