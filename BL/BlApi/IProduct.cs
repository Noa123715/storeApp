/// <summary>
/// product interface
/// </summary>
using BO;
namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList> ReadProductsList();
    public ProductItem ReadProductProperties(int productID, Cart cart);
    public Product ReadProductProperties(int productID);
    public void AddProduct(Product product);
    public void DeleteProduct(int productID);
    public void UpdateProduct(Product product);
}