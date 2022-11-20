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
        catch (Exception er)//DalApi.EntityNotFoundException)
        {
            //throw new BlEntityNotFoundException();
            throw new Exception(@$"error{er}");
        }
        //catch (BlEntityNotFoundException ex)
        //{
        //    throw new BlEntityNotFoundException();
        //}
        //catch (Exception)
        //{
        //    throw new Exception();
        //}
        return product;
    }

    public BO.ProductItem ReadProductProperties(int productId, BO.Cart cart)
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
            //else throw 
            product.IsInStock = dalProduct.InStock >= product.Amount;
        }
        return product;
    }

    public void AddProduct(BO.Product product)
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
        Dal.Product.Create(DOProduct);
        //}
        //catch (BlEntityNotFoundException)
        //{
        //    throw new BlEntityNotFoundException();
        //}
        //catch (BlNullValueException)
        //{
        //    throw new BlNullValueException();
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

    public void DeleteProduct(int productId)
    {
        //try
        //{
        var orderitems = Dal.OrderItem.ReadAll();
        foreach (var item in orderitems)
        {
            if (item.ProductID == productId && Dal.Order.Read(item.OrderID).DeliveryDate > DateTime.MinValue) ;
            //throw new BlUnsuccessfulDeleteException();
        }
        Dal.Product.Delete(productId);
        //}
        //catch (DalApi.EntityNotFoundException)
        //{
        //    throw new BlEntityNotFoundException();
        //}
        //catch (BlEntityNotFoundException ex)
        //{
        //    throw new BlEntityNotFoundException(ex);
        //}
        //catch (BlUnsuccessfulDeleteException)
        //{
        //    throw new BlUnsuccessfulDeleteException();
        //}
        //catch (Exception)
        //{
        //    throw new Exception();
        //}
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