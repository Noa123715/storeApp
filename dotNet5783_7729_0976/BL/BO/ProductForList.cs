/// <summary>
/// 
/// </summary>

namespace BO;

    public class ProductForList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public eCategories Category { get; set; }

        //overriding ToString method prints the orderItem properties.
        public override string ToString() => $@"ID: {ID}, Name: {Name}, Price: {Price}, Category: {Category}";
    }
