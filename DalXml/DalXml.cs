using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
    public IOrder Order => new Dal.Order();
    public IProduct Product => new Dal.Product();
    public IOrderItem OrderItem => new Dal.OrderItem();
}


