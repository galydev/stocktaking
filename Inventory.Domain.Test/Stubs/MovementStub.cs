using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Domain.Test.Stubs
{
    public static class MovementStub
    {
        private static List<Movement> Movements
        {
            get
            {
                List<Movement> movements = new List<Movement>
                {
                    new Movement
                    {
                        Id = Guid.Parse("EA8D52CF-7E91-4789-B703-48C857A8F3E0"),
                        MovementDate = DateTime.Now,
                        Type = true,
                        Quantity = 30,
                        Price = 240,
                        ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        WarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")
                    },
                    new Movement
                    {
                        Id = Guid.Parse("0E414B7C-74E7-48B5-AB1C-2D9CB818A754"),
                        MovementDate = DateTime.Now,
                        Type = true,
                        Quantity = 20,
                        Price = 432.1M,
                        ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        WarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")
                    },
                    new Movement
                    {
                        Id = Guid.Parse("EBDE457B-44A2-42E3-80C5-A9C119FF120E"),
                        MovementDate = DateTime.Now,
                        Type = false,
                        Quantity = 12,
                        Price = 65.3M,
                        ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        WarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")
                    },
                    new Movement
                    {
                        Id = Guid.Parse("3B8423E3-4922-40A7-9385-DE767D40CE60"),
                        MovementDate = DateTime.Now,
                        Type = false,
                        Quantity = 15,
                        Price = 433.34M,
                        ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        WarehouseId = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392")
                    },
                    new Movement
                    {
                        Id = Guid.Parse("EC47C0A3-F590-4D92-88C5-6C1206CD3A62"),
                        MovementDate = DateTime.Now,
                        Type = true,
                        Quantity = 2,
                        Price = 650,
                        ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        WarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
                    },
                    new Movement
                    {
                        Id = Guid.Parse("C20ADA54-54F9-4D10-98C7-29BA2F458E6A"),
                        MovementDate = DateTime.Now,
                        Type = true,
                        Quantity = 12,
                        Price = 843.1M,
                        ProductId = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        WarehouseId = Guid.Parse("C347ED5D-1F33-49EE-A58D-B7F2310192A6")
                    }
                };
                return movements;
            }
        }

        public static IQueryable<Movement> GetAllMovement()
            => Movements.AsQueryable();
    }
}