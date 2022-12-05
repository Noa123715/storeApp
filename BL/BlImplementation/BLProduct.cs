using Dal;
using BlApi;
namespace BlImplementation;

internal class BLProduct : IProduct
{
    private DalList Dal { get; set; } = new();

    public IEnumerable<BO.ProductForList> ReadProductsList()
    {
        var dalProduct = Dal.Product.ReadAll();
        List<BO.ProductForList> products = new List<BO.ProductForList>();
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

    public BO.Product ReadProductProperties(int productId)
    {
        BO.Product product = new BO.Product();
        try
        {
            //if (productId <= 0)
                //throw new BlEntityNotFoundException();
            var prod = Dal.Product.Read(productId);
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

    public BO.ProductItem ReadProductProperties(int productId, BO.Cart cart)
    {

        try
        {
            var dalProduct = Dal.Product.Read(productId);

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
                var item = cart.Items.Find(item => item.ProductID == productId);
                if (item is not null)
                {
                    product.Amount = item.Amount;
                }
                else throw new BlNotExistException();
                product.IsInStock = dalProduct.InStock >= product.Amount;
            }
            return product;
        }
        catch(DalApi.NotExistException err) 
        { 
            throw new BlNotExistException(err); 
        }
    }

    public void AddProduct(BO.Product product)
    {
        try
        {
        if (product.ID <= 0)
            throw new BlNotExistException();
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

    public void DeleteProduct(int productId)
    {
        try
        {
        var orderitems = Dal.OrderItem.ReadAll();
        //foreach (var item in orderitems) // מה קשורה כאן הדיקה הזו למען ה
        //{
        //    if (item.ProductID == productId && Dal.Order.Read(item.OrderID).DeliveryDate > DateTime.MinValue) ;
        //    //throw new BlUnsuccessfulDeleteException();
        //}
        Dal.Product.Delete(productId);
        }
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
       
    }

    public void UpdateProduct(BO.Product product)
    {
        //try
        //{
        //if (prod.ID <= 0)
        //    throw new BlEntityNotFoundException();
        //if (string.IsNullOrEmpty(prod.Name))
        //    throw new BlNullValueException();
        //if (prod.Price <= 0)
        //    throw new BlNegativeValueException();
        //if (prod.InStock < 0)
        //    throw new BlNegativeValueException();
        DO.Product DOProduct = new DO.Product();
        DOProduct.ID = product.ID;
        DOProduct.Name = product.Name;
        DOProduct.Price = product.Price;
        DOProduct.Category = (DO.eCategories)product.Category;
        DOProduct.InStock = product.InStock;
        Dal.Product.UpDate(DOProduct);
        //}
        //catch (BlEntityNotFoundException)
        //{
        //    throw new BlEntityNotFoundException();
        //}
        //catch (BlNegativeValueException)
        //{
        //    throw new BlNegativeValueException();
        //}
        //catch (Exception)
        //{
        //    throw new Exception();
        //}
    }
}