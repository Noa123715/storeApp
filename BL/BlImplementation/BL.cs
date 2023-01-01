using BlApi;
namespace BlImplementation;


/// <summary>
/// BL class for contect between the interfaces to their implementations.
/// </summary>

sealed public class BL : IBL
{
    public ICart Cart => new BLCart();
    public IOrder Order => new BLOrder();
    public IProduct Product => new BLProduct();
}