/// <summary>
/// cart interface
/// </summary>
/// using BO;
using BO;
namespace BlApi;

public interface ICart
{
    public Cart AddProductToCart(Cart cart, int productID);
    public Cart UpdateProductAmount(Cart cart, int productID, int newAmount);
    public void Confirmation(Cart cart, string customerName, string customerMail, string customerAddress);
}