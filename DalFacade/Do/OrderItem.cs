/// <summary>
/// An entity that defines an item in an order 
/// and holds the item and order details
/// </summary>

namespace DO;

    public struct OrderItem
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public int OrderID { get; set; }
        public double Price { get; set; }
    public override string ToString() => $@"
        ProductID = {ProductID}
        Amount = {Amount}
        OrderID = {OrderID}
        Price = {Price} ";
    }

