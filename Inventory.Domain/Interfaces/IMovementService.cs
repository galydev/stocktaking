using Inventory.Domain.CustomEntities;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IMovementService
    {
        Task<Movement> InsertProductWareahouse(Movement movement);

        Task<bool> RemoveProductsOfWarehouse(RemoveProductWarehouse productWarehouse);

        Task<IEnumerable<Movement>> MoveProductOtherWarehouse(MoveProducts moveProducts);

        Task<ShoppingProduct> GetTotalShoppingProduct(Guid productId);
    }
}