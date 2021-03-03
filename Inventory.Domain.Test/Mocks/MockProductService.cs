using Inventory.Domain.Exceptions;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Test.Stubs;
using Moq;
using System;
using System.Threading.Tasks;

namespace Inventory.Domain.Test.Mocks
{
    public class MockProductService : Mock<IProductService>
    {
        public MockProductService GetProductByIdAsync()
        {
            Setup(x => x.GetProductByIdAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")) || e.Equals(Guid.Parse("A16C605C-0A1A-4E63-857F-68019CE2575C")))))
                                            .Returns((Guid id) => Task.FromResult(ProductStub.GetProductById(id)));

            Setup(x => x.GetProductByIdAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")) && !e.Equals(Guid.Parse("A16C605C-0A1A-4E63-857F-68019CE2575C")))))
                                        .Returns(() => throw new BusinessException("Product doesn't exist"));
            return this;
        }

        public MockProductService ExistProductAsync()
        {
            Setup(x => x.ExistProductAsync(It.Is<Guid>(e => e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")) || e.Equals(Guid.Parse("A16C605C-0A1A-4E63-857F-68019CE2575C")))))
                                            .Returns((Guid id) => Task.FromResult(ProductStub.ExistProduct()));

            Setup(x => x.ExistProductAsync(It.Is<Guid>(e => !e.Equals(Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3")) && !e.Equals(Guid.Parse("A16C605C-0A1A-4E63-857F-68019CE2575C")))))
                                        .Returns(() => throw new BusinessException("Product doesn't exist"));
            return this;
        }
    }
}