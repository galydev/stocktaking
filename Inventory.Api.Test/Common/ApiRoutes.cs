namespace Inventory.Api.Test.Common
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Base = Root + "/";

        public static class Product
        {
            public const string GetAllProducts = Base + "product";
            public const string CreateProduct = Base + "product ";
            public const string UpdateProduct = Base + "product";
            public const string DeleteProduct = Base + "product";
            public const string GetProductById = Base + "product/byid/{0}";
            public const string GetProductByName = Base + "product/byname/{0}";
            public const string ExistProductById = Base + "api/product/exist/{0}";
        }

        public static class Warehouse
        {
            public const string GetWarehouses = Base + "warehouse";
            public const string CreateWarehouse = Base + "warehouse";
            public const string ExistWarehouseById = Base + "warehouse/exist/{0}";
        }

        public static class Movement
        {
            public const string GetShoppingTotal = Base + "movement/totalshoppingproduct";
            public const string MoveProductOtherWarehouse = Base + "movement/moveproductotherwarehouse";
            public const string RemoveProductOfWarehouse = Base + "movement/removeproductofwarehouse";
            public const string LoadWarehouse = Base + "movement/loadwarehouse";
        }
    }
}