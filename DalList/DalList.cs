using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    public IOrder Order => new DalOrder(); 
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
}