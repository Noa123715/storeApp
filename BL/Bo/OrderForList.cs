/// <summary>
/// 
/// </summary>

namespace BO;
public class OrderForList
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public eOrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }


    //overriding ToString method enables printing the OrderForList properties.
    public override string ToString() => $@"Order for list ID: {ID}, customer name: {CustomerName}, staus: {Status}, amount of items:{AmountOfItems},  total price:{TotalPrice}";

}