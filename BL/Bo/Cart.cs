namespace BO;
/// <summary>
/// a class to save the product that the castomer want to buy
/// </summary>
public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    //to save all the product that the customer want to buy
    public List<OrderItem> Items = new List<OrderItem>();
    public double Price { get; set; }

    //overriding ToString method -prints the cart properties.
    public override string ToString()
    {
        string toString = $@"Cart of customer mame {CustomerName}, email {CustomerEmail}, address {CustomerAddress}. 
          total price {Price} items: \n ";
        foreach (var i in Items) { toString += "\n \t " + i; };
        return toString;
    }
}
