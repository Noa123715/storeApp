using BlApi;
namespace BlImplementation;

sealed public class BL : IBL
{
    public ICart Cart => new BLCart();
    public IOrder Order => new BLOrder();
    public IProduct Product => new BLProduct();
}