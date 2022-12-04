using Dal;
using BlApi;
namespace BlImplementation;

internal class BLCart : ICart
{
    private DalList Dal { get; set; } = new();
    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        try
        {
            DO.Product product = Dal.Product.Read(productID);
            int productInStock = product.InStock;
            double productPrice = product.Price;
            BO.OrderItem orderItem = new BO.OrderItem();
            if (!(cart.Items == null))
            {
                var item = cart.Items.Find(item => item.ProductID == productID);
                if (item is not null) 
                { 
                    orderItem = item;
                }     
            }
            if (productInStock > 0)
            {
                if (orderItem != null)
                {
                    orderItem.Amount++;
                    orderItem.TotalPrice += productPrice;
                    cart.Price += productPrice;
                    return cart;
                }
                else
                {
                    BO.OrderItem eOrderItem = new BO.OrderItem();
                    eOrderItem.ID = DataSource.Config.OrderItemId;
                    eOrderItem.ProductID = productID;
                    eOrderItem.ProductName = product.Name;
                    eOrderItem.Amount = 1;
                    eOrderItem.Price = productPrice;
                    eOrderItem.TotalPrice = productPrice;
                    if (cart.Items != null)
                    {
                        List<BO.OrderItem> aOrderItem = cart.Items;
                        aOrderItem.Add(eOrderItem);
                        cart.Items = aOrderItem;
                        cart.Price += productPrice;
                    }
                    //else throw
                }
                return cart;
            }

            else
                throw new BlOutOfStockException();
        }
        catch (DalApi.NotExistException notExistException)
        {
            throw new BLNotExistException(notExistException);
        }
      
     
    }

    public BO.Cart UpdateProductAmount(BO.Cart cart, int productID, int newAmount)
    {
        try
        {
        int productInStock = Dal.Product.Read(productID).InStock;
        double productPrice = Dal.Product.Read(productID).Price;
        BO.OrderItem orderItem = new BO.OrderItem();
        var item = cart.Items.Find(item => item.ProductID == productID);
            if (item != null)
            {
                orderItem = item;
            }

        //if (orderItem == null)
        //    throw new BlEntityNotFoundException();
        if (newAmount > orderItem.Amount)
        {
            //if (newAmount - orderItem.Amount > productInStock)
            //    throw new BlOutOfStockException();
            cart.Price += (newAmount - orderItem.Amount) * productPrice;
            orderItem.TotalPrice += (newAmount - orderItem.Amount) * productPrice;
            orderItem.Amount = newAmount;
        }
        else if (newAmount == 0)
        {
            cart.Price -= productPrice * orderItem.Amount;
            cart.Items.Remove(orderItem);
        }
        else
        {
            cart.Price -= productPrice * (orderItem.Amount - newAmount);
            orderItem.Amount = newAmount;
            orderItem.TotalPrice = productPrice * newAmount;
        }

        return cart;
        }
        catch (DalApi.NotExistException notExist)
        {
           throw new BLNotExistException(notExist);
        }
       
     
    }

    public void Confirmation(BO.Cart cart, string customerName, string customerMail, string customerAddress)
    {
        //try
        //{
        var addr = new System.Net.Mail.MailAddress(customerMail);
        bool isValid = (addr.Address == customerMail);
        //if (customerName == "" || customerAddress == "")
        //{
        //    throw new BlNullValueException();
        //}
        //if (!isValid)
        //{
        //    throw new BlInvalidEmailException();
        //}
        int productInStock;
        foreach (BO.OrderItem oi in cart.Items)
        {
            productInStock = Dal.Product.Read(oi.ProductID).InStock;
            //if (oi.Amount < 0)
            //{
            //    throw new BlNegativeValueException();
            //}
            //if (productInStock < oi.Amount)
            //{
            //    throw new BlOutOfStockException();
            //}
            List<DO.Product> prodList = DataSource.productList;
            bool idExists = (prodList.Find(p => p.ID == oi.ProductID)).ID != 0;
            //if (!idExists)
            //    throw new BlEntityNotFoundException();
        }
        //ותקבל בחזרה מספר הזמנה
        DO.Order DoOrder = new DO.Order();
        DoOrder.OrderDate = DateTime.Now;
        DoOrder.ShipDate = DateTime.MinValue;
        DoOrder.DeliveryDate = DateTime.MinValue;
        DoOrder.CustomerName = customerName;
        DoOrder.CustomerEmail = customerMail;
        DoOrder.CustomerAddress = customerAddress;
        DoOrder.ID = DataSource.Config.OrderId;
        Dal.Order.Create(DoOrder);
        DO.OrderItem DoOrderItem = new DO.OrderItem();
        foreach (BO.OrderItem orderItem in cart.Items)
        {
            DoOrderItem.ID = orderItem.ID;
            DoOrderItem.ProductID = orderItem.ProductID;
            DoOrderItem.OrderID = DoOrder.ID;
            DoOrderItem.Amount = orderItem.Amount;
            DoOrderItem.Price = orderItem.TotalPrice;
            Dal.OrderItem.Create(DoOrderItem);
            DO.Product DoProduct = Dal.Product.Read(DoOrderItem.ProductID);
            DoProduct.InStock -= orderItem.Amount;
            Dal.Product.UpDate(DoProduct);
        }
        Console.WriteLine("confirmed");
        //}
        //catch (BlNegativeValueException)
        //{
        //    throw new BlNegativeValueException();
        //}
        //catch (BlOutOfStockException)
        //{
        //    throw new BlOutOfStockException();
        //}
        //catch (BlNullValueException)
        //{
        //    throw new BlNullValueException();
        //}
        //catch (BlInvalidEmailException)
        //{
        //    throw new BlInvalidEmailException();
        //}
        //catch (BlEntityNotFoundException ex)
        //{
        //    throw new BlEntityNotFoundException(ex);
        //}
        //catch (Exception)
        //{
        //    throw new Exception();
        //}
    }
}
