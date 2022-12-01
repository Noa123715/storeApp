/// <summary>
/// cart interface
/// </summary>
/// using BO;
using BO;
namespace BlApi;

public interface ICart
{
    public Cart AddProductToCart(Cart cart, int productId);
    public Cart UpdateProductAmount(Cart cart, int productId, int newAmount);
    public void Confirmation(Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress);
}