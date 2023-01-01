using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{
    private static DalList lock = null;
    private static readonly Lazy<DalList> instance = new Lazy<DalList>(() => new DalList());

    public static DalList Instance { get { return instance.Value; } }


    private DalList()
    {

    }

    public IOrder Order => new DalOrder(); 
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
}