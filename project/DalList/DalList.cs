using DalApi;
using Dal.DO;
namespace Dal;

sealed public class DalList : IDal
{
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();
}

