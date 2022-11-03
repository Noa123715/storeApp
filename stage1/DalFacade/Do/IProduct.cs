/// <summary>
/// 
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

