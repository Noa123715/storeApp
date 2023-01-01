using Dal;
using BlApi;
namespace BlImplementation;

/// <summary>
/// BLProduct class- implements IProduct methods: 
/// reading product list <see cref="ReadProductsList"/>
/// Reading product properties <see cref="ReadProductProperties"/>
/// adding a product <see cref="AddProduct"/>
///  delete product <see cref="DeleteProduct"/>
/// update product properties  <see cref="UpdateProduct"/>
/// </summary>
/// 
internal class BLProduct : IProduct
{
    /// <summary>
    /// creating Idal instance for using its methods and members in BLOrder.
    /// </summary>

    private Idal Dal { get; set; } = DalApi.Factory.Get();

    /// <summary>
    /// ReadProductsList method- recieves from dal layer the products list for manager.
    /// </summary>
    /// <returns> products list</returns>
    public IEnumerable<BO.ProductForList> ReadProductsList(BO.eCategories? categories = null)
    {
        IEnumerable<DO.Product>? dalProduct;
        if (categories is null)
            dalProduct = Dal.Product.ReadAll();
        else
            dalProduct = Dal.Product.ReadAll(product => (BO.eCategories)product.Category == categories);
        List<BO.ProductForList> products = new List<BO.ProductForList>();
        if (dalProduct is null) throw new BlNullValueException();
        foreach (var prod in dalProduct)
        {
            BO.ProductForList product = new BO.ProductForList();
            product.ID = prod.ID;
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Category = (BO.eCategories)prod.Category;
            products.Add(product);
        }
        return products;
    }

    /// <summary>
    /// ReadProductProperties method
    /// </summary>
    /// <param name="productId"></param>
    /// <returns> all properties for specific product</returns>
    /// <exception cref="BlNotExistException"></exception>
    /// <exception cref="BlInValidInputException"></exception>

    public BO.Product ReadProductProperties(int productId)
    {
        BO.Product product = new BO.Product();
        try
        {
            if (productId <= 0)
                throw new BlInValidInputException();
            DO.Product prod = Dal.Product.Read(productId);
            product.ID = prod.ID;
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Category = (BO.eCategories)prod.Category;
            product.InStock = prod.InStock;
        }
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
        return product;
    }
    /// <summary>
    /// Read product properties for specific product in specific cart.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cart"></param>
    /// <returns> product-item properties for the require  </returns>
    /// <exception cref="BlNotExistException"></exception>
    public BO.ProductItem ReadProductProperties(int productId, BO.Cart cart)
    {
        try
        {
            DO.Product dalProduct = Dal.Product.Read(productId);
            BO.ProductItem product = new BO.ProductItem();
            product.ID = dalProduct.ID;
            product.Name = dalProduct.Name;
            product.Price = dalProduct.Price;
            product.Category = (BO.eCategories)dalProduct.Category;
            if (cart.Items.Find(item => item.ProductID == productId) == null)
            {
                product.Amount = 0;
                product.IsInStock = dalProduct.InStock > 0;
            }
            else
            {
                BO.OrderItem? item = cart.Items.Find(item => item.ProductID == productId);
                if (item is not null)
                {
                    product.Amount = item.Amount;
                }
                else throw new BlNotExistException();
                product.IsInStock = dalProduct.InStock >= product.Amount;
            }
            return product;
        }
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }

    /// <summary>
    /// Add new product to catalog (for manager).
    /// </summary>
    /// <param name="product"> new product for adding</param>
    /// <exception cref="BlNullValueException"></exception>
    /// <exception cref="BlInValidInputException"></exception>
    /// <exception cref="BlAlreadyExistException"></exception>
    public void AddProduct(BO.Product product)
    {
        try
        {
            if (product.ID <= 0)
                throw new BlInValidInputException();
            if (string.IsNullOrEmpty(product.Name))
                throw new BlNullValueException();
            if (product.Price <= 0)
                throw new BlInValidInputException();
            if (product.InStock < 0)
                throw new BlInValidInputException();
            DO.Product DOProduct = new DO.Product();
            DOProduct.ID = product.ID;
            DOProduct.Name = product.Name;
            DOProduct.Price = product.Price;
            DOProduct.Category = (DO.eCategories)product.Category;
            DOProduct.InStock = product.InStock;
            Dal.Product.Create(DOProduct);
        }
        catch (DalApi.AlreadyExistException err)
        {
            throw new BlAlreadyExistException(err);
        }
    }

    /// <summary>
    /// Delete product from catalog (for manager).
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="BlNotExistException"></exception>
    public void DeleteProduct(int productId)
    {
        try
        {
            IEnumerable<DO.OrderItem> orderitems = Dal.OrderItem.ReadAll();
            foreach (var item in orderitems)
            {
                if (item.ProductID == productId)
                    throw new BlIllegalDeletionAttempt();
            }
            Dal.Product.Delete(productId);
        }
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }

    /// <summary>
    /// UpdateProduct method updates a product property (for manager).
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product)
    {
        try
        {
            DO.Product DOProduct = Dal.Product.Read(product.ID);
            if (string.IsNullOrEmpty(product.Name))
                throw new BlNullValueException();
            if (product.Price <= 0)
                throw new BlInValidInputException();
            if (product.InStock < 0)
                throw new BlInValidInputException();
            DOProduct.Name = product.Name;
            DOProduct.Price = product.Price;
            DOProduct.Category = (DO.eCategories)product.Category;
            DOProduct.InStock = product.InStock;
            Dal.Product.UpDate(DOProduct);
        }
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }
}