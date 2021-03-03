using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Api.Test.Stubs
{
    public static class ProductStub
    {
        private static List<Product> Products
        {
            get
            {
                List<Product> products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc"),
                        CreationDate = DateTime.Now,
                        Name = "Pera",
                        Description = "Pera",
                        Sku = "3942836427",
                        Price = 345.0M,
                        MinimunStock = 1,
                        MaximumStock = 1000,
                        Stock = 0
                    },
                    new Product
                    {
                        Id = Guid.Parse("B99BC862-C515-4D61-97D8-4D2B188A19F3"),
                        CreationDate = DateTime.Now,
                        Name = "Cebolla",
                        Description = "Cebolla",
                        Sku = "83745235275",
                        Price = 400.00M,
                        MinimunStock = 1,
                        MaximumStock = 320,
                        Stock = 0
                    },
                    new Product
                    {
                        Id = Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a"),
                        CreationDate = DateTime.Now,
                        Name = "Mora",
                        Description = "mora",
                        Sku = "868264768326",
                        Price = 500.0M,
                        MinimunStock = 1,
                        MaximumStock = 1200,
                        Stock = 0
                    }
                };
                return products;
            }
        }

        public static IEnumerable<Product> GetAllProducts()
            => Products;

        public static IEnumerable<Product> GetNotProductAsync()
            => null;

        public static Product GetProductById(Guid id)
            => Products.FirstOrDefault(x => x.Id.Equals(id));

        public static Product NoReturnProductById()
            => null;

        public static Product GetProductByName(string name)
            => Products.FirstOrDefault(x => x.Name.Equals(name));

        public static IQueryable<Product> GetAllProductQueryable()
            => Products.AsQueryable();

        public static bool ExistProduct()
            => true;

        public static bool NotExistProduct()
            => false;

        public static Product InsertProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            return product;
        }

        public static bool DeleteProduct()
            => true;

        public static bool NoDeleteProduct()
            => false;
    }
}