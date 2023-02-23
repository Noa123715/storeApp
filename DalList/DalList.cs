using DalApi;
namespace Dal;
/// <summary>
/// DalList class includes implementations for dal layer: order, product and order-Item  methods.
/// The class is designed in a singleton design pattern. In order to avoid multiple creation of the class when multiple threads use it, 
/// we added a check flag <see cref="instance" > and created a Thread-Safe Singleton. 
/// To save memory and improve performance we added lazy initialization. <see cref="Lazy"/>
/// </summary>
internal sealed class DalList : IDal
{

    private static Lazy<DalList>? instance;

    public static DalList Instance { get { return GetInstance(); } }
    public static DalList GetInstance()
    {
        if (instance == null)
            instance = new Lazy<DalList>(() => new DalList());
        return instance.Value;
    }

    private DalList()
    {

    }

    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
}