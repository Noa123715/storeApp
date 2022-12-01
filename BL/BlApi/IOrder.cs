/// <summary>
/// order interface
/// </summary>
using BO;
namespace BlApi;
public interface IOrder
{
    public IEnumerable<OrderForList> ReadOrderList();
    public Order ReadOrderProperties(int orderId);
    public Order UpdateOrderSent(int orderId);
    public Order UpdateOrderDelivery(int orderId);
    public OrderTracking TrackOrder(int orderId);
}