using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public IOrderItem OrderItem => new OrderItem();
}