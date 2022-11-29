/// <summary>
/// 
/// </summary>

namespace BO
{
        public class ProductItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public eCategories Category { get; set; }
            public bool IsInStock { get; set; }
            public int Amount { get; set; }

            //overriding the ToString function for printing the product-item's details
            public override string ToString() => $@"ID: {ID}, Name: {Name}, Price: {Price}, Category: {Category}, Amount : {Amount}, Is available: {IsInStock}";
        }
    }

