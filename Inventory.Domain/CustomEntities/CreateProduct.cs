namespace Inventory.Domain.CustomEntities
{
    public class CreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int MinimunStock { get; set; }
        public int MaximumStock { get; set; }
        public int? Stock { get; set; }
    }
}