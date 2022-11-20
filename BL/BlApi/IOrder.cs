/// <summary>
/// order interface
/// </summary>
using BO;
namespace BlApi;
public interface IOrder
{
    public IEnumerable<OrderForList> ReadOrderList();
    public Order ReadOrderProperties(int orderID);
    public Order UpdateOrderSent(int orderID);
    public Order UpdateOrderDelivery(int orderID);
    public OrderTracking TrackOrder(int orderID);
}