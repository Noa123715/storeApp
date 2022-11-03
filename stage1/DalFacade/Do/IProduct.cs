/// <summary>
/// An entity that defines an item in the store 
/// and holds the details of the item: 
/// category, price, quantity in stock.
/// </summary>

namespace Dal.DO;

    public struct IProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int InStock { get; set; }
        public int ID { get; set; } 
        public eCategory Category { get; set; }
    public override string ToString() => $@"
        Name = {Name}
        Price = {Price}
        InStock = {InStock}
        Category = {Category}";
    }

