/// <summary>
/// 
/// </summary>
namespace DalApi;
using Dal.DO;
public interface IDal
{
    public IOrder Order{ get; }
    public IOrderItem OrderItem{ get; }
    public IProduct Product { get; }
}