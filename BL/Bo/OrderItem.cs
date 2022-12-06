/// <summary>
/// A class that describes each product in the order or cart
/// </summary>

namespace BO
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }

        //overriding ToString method prints the orderItem properties.
        public override string ToString() =>
            $@" ID: {ID}, 
            ProductName: {ProductName},
            ProductID: {ProductID}
           , Amount: {Amount},  
             Price: {Price}, 
             TotalPrice: {TotalPrice}";
    }
}
