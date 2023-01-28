using DalApi;
namespace Dal;

/// <summary>
///  class Dalxml-links the interface to the implementation of all classes (singletn pattern)
/// </summary>
sealed internal class DalXml : IDal
{
  
        private static Lazy<DalXml> instance = new Lazy<DalXml>(() => new DalXml());
        public static DalXml Instance { get => GetInstance(); }
        private DalXml() { }
        public static DalXml GetInstance()
        {
            lock (instance)
            {
                if (instance == null)
                    instance = new Lazy<DalXml>(() => new DalXml());
                return instance.Value;
            }
        }
    
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public IOrderItem OrderItem => new OrderItem();
}