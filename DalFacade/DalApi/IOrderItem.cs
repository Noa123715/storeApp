using DO;
namespace DalApi;

public interface IOrderItem : ICrud <OrderItem>
{
    public IEnumerable<OrderItem> ReadByOrderID(int orderID);
};