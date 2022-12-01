using BlApi;
using Dal;
using BO;
using DO;
namespace BlImplementation;

internal class BLProduct : IProduct
{
    private DalList dalList { get; set; } = new();
    /// <summary>
    /// this function reads the list of the products
    /// </summary>
    /// <returns>list of the products</returns>
    public IEnumerable<ProductForList> ReadProductsList()
    {
        var dalProduct = dalList.Product.ReadAll();
        List<ProductForList> products = new List<ProductForList>();
        foreach (var prod in dalProduct)
        {
            ProductForList product = new ProductForList();
            product.ID = prod.ID;
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Category = (BO.eCategories)prod.Category;
            products.Add(product);
        }
        return products;
    }


    /// <summary>
    /// this function reads the details of a specific product
    /// </summary>
    /// <param name="productId">the ID of the product wanted to be read</param>
    /// <returns>the product with the specific ID</returns>
    /// <exception cref="DalApi.EntityNotFoundException">a DL exception for an entity not found</exception>
    /// <exception cref="BlEntityNotFoundException">a BL exception for an entity not found</exception>
    /// <exception cref="Exception">default exception</exception>
    public BO.Product ReadProductProperties(int productId)
    {
        BO.Product product = new BO.Product();
        try
        {
            //if (productId <= 0)
                //throw new BlEntityNotFoundException();
            var prod = dalList.Product.Read(productId);
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

    /// <summary>
    /// this function returns how many of each item in the store are in the cart
    /// </summary>
    /// <param name="cart">the cart to be checked</param>
    /// <returns>list of products in store and their amount in the specific cart</returns>
    public ProductItem ReadProductProperties(int productId, Cart cart)
    {
        var DOProduct = dalList.Product.Read(productId);

        ProductItem product = new ProductItem();
        product.ID = DOProduct.ID;
        product.Name = DOProduct.Name;
        product.Price = DOProduct.Price;
        product.Category = (eCategories)DOProduct.Category;
        if (cart.Items.Find(oi => oi.ProductID == productId) == null)
        {
            product.Amount = 0;
            product.IsInStock = DOProduct.InStock > 0;
        }
        else
        {
            product.Amount = cart.Items.Find(oi => oi.ProductID == productId).Amount;
            product.IsInStock = DOProduct.InStock >= product.Amount;
        }
        return product;
    }



    /// <summary>
    /// this function adds an item to data base
    /// </summary>
    /// <param name="prod">the product to be added</param>
    /// <exception cref="BlEntityNotFoundException">a BL exception for an entity not found</exception>
    /// <exception cref="BlNullValueException">a BL exception for a null value</exception>
    /// <exception cref="BlNegativeValueException">a BL exception for an invalid negative value</exception>
    /// <exception cref="Exception">default exception</exception>
    public void AddProduct(BO.Product prod)
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
        DOProduct.ID = prod.ID;
        DOProduct.Name = prod.Name;
        DOProduct.Price = prod.Price;
        DOProduct.Category = (DO.eCategories)prod.Category;
        DOProduct.InStock = prod.InStock;
        dalList.Product.Create(DOProduct);
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

    /// <summary>
    /// this function deleted a product from the data base
    /// </summary>
    /// <param name="productId">the ID of the product to be deleted</param>
    /// <exception cref="DalApi.EntityNotFoundException">a DL exception for an entity not found</exception>
    /// <exception cref="BlEntityNotFoundException">a BL exception for an entity not found</exception>
    /// <exception cref="BlUnsuccessfulDeleteException">a BL exception for an unsuccessful attempt to delete</exception>
    /// <exception cref="Exception">default exception</exception>
    public void DeleteProduct(int productId)
    {
        try
        {
            var orderitems = dalList.OrderItem.Read();
            foreach (var oi in orderitems)
            {
                if (oi.ProductId == productId && dalList.Order.Read(oi.OrderId).DeliveryDate > DateTime.MinValue)
                    throw new BlUnsuccessfulDeleteException();
            }
            bool deleted = dalList.Product.Delete(productId);
            if (!deleted)
                throw new BlUnsuccessfulDeleteException();
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlEntityNotFoundException();
        }
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

    /// <summary>
    /// updates details of product
    /// </summary>
    /// <param name="prod">the product for updating</param>
    /// <exception cref="BlNullValueException">a BL exception for a null value</exception>
    /// <exception cref="BlEntityNotFoundException">a BL exception for an entity not found</exception>
    /// <exception cref="BlNegativeValueException">a BL exception for an invalid negative number</exception>
    /// <exception cref="Exception">default exception</exception>
    public void UpdateProduct(BO.Product prod)
    {
        //try
        //{
        if (prod.ID <= 0)
            throw new BlEntityNotFoundException();
        if (string.IsNullOrEmpty(prod.Name))
            throw new BlNullValueException();
        if (prod.Price <= 0)
            throw new BlNegativeValueException();
        if (prod.InStock < 0)
            throw new BlNegativeValueException();
        DO.Product DOProduct = new DO.Product();
        DOProduct.ID = prod.ID;
        DOProduct.Name = prod.Name;
        DOProduct.Price = prod.Price;
        DOProduct.Category = (DO.eCategories)prod.Category;
        DOProduct.InStock = prod.InStock;
        dalList.Product.Update(DOProduct);
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