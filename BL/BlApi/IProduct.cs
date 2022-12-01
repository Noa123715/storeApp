/// <summary>
/// product interface
/// </summary>
using BO;
namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList> ReadProductsList();
    public ProductItem ReadProductProperties(int productId, Cart cart);
    public Product ReadProductProperties(int productId);
    public void AddProduct(Product prod);
    public void DeleteProduct(int productId);
    public void UpdateProduct(Product prod);
}