
using DO;
/// <summary>
/// 
/// </summary>

namespace BO;
public class Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public eOrderStatus Status { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public double TotalPrice { get; set; }

    //overriding the ToString function for printing the order's details
    public override string ToString()
    {
        string toString =
                $@"order ID={ID}: 
                customer mame: {CustomerName}, 
                email {CustomerEmail},
                address {CustomerAddress}.
                order date: {OrderDate}, 
                ship: {ShipDate}, 
                delivery: {DeliveryDate},
                status: {Status}.
                total price:{TotalPrice} \n items:";
        foreach (var i in Items) { toString += "\n \t " + i; };
        return toString;
    }

}

