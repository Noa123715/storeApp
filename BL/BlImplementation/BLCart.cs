using BlApi;
namespace BlImplementation;
/// <summary>
/// BLCart class implements Icart methods- 
/// adding product to cart, <see cref="AddProductToCart"/>
/// change product's amount, <see cref="UpdateProductAmount"/>
/// and confirmation cart details (before create order). <see cref="Confirmation"/>
/// </summary>

internal class BLCart : ICart
{

    /// <summary>
    /// creating Idal instance for using its methods and members in BLOrder.
    /// </summary>
    /// 
    private static DalApi.IDal? dal = DalApi.Factory.Get();
    private static int maxCartOrderItemID = 1;
    public static int MaxCartOrderItemID { get { return maxCartOrderItemID++; } }


    /// <summary>
    /// AddProductToCart method- adding a product to cart
    /// </summary>
    /// <param name="cart">pointer to specific cart</param>
    /// <param name="productID"> to find the require product</param>
    /// <returns>the updates cart</returns>
    /// <exception cref="BlOutOfStockException"></exception>
    /// <exception cref="BlNotExistException"></exception>
    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        try
        { DO.Product product; 
            lock (dal)
            {
                product = dal.Product.Read(p => p.ID == productID);
             }
            int productInStock = product.InStock;
            double productPrice = product.Price;
            int index = -1;
            BO.OrderItem? orderItem = new BO.OrderItem();
            if (productInStock <= 0)
                throw new BlOutOfStockException();
            if (cart.Items.Count != 0)
                index = cart.Items.FindIndex(item => item.ProductID == productID);
            if (index != -1)
            {
                cart.Items[index].Amount++;
                cart.Items[index].TotalPrice += productPrice;
                cart.Items[index].Price += productPrice;

            }
            else
            {
                orderItem.ID = MaxCartOrderItemID;
                orderItem.ProductID = productID;
                orderItem.ProductName = product.Name;
                orderItem.Amount = 1;
                orderItem.Price = productPrice;
                orderItem.TotalPrice = productPrice;
                List<BO.OrderItem> aOrderItem = cart.Items;
                aOrderItem?.Add(orderItem);
                cart.Items = aOrderItem;
                cart.Price += productPrice;
            }
        }
        catch (DalApi.NotExistException notExistException)
        {
            throw new BlNotExistException(notExistException);
        }
        catch (BlOutOfStockException err)
        {
            Console.WriteLine(err.Message);
        }
        return cart;
    }


    /// <summary>
    /// UpdateProductAmount method updates an product's amount in cart.
    /// </summary>
    /// <param name="cart"> pointer to the requires cart</param>
    /// <param name="productID">to find the require product</param>
    /// <param name="newAmount">the new amount for this product</param>
    /// <returns></returns>
    /// <exception cref="BlNotExistException"></exception>
    /// <exception cref="BlOutOfStockException"></exception>
    public BO.Cart UpdateProductAmount(BO.Cart cart, int productID, int newAmount)
    {
        try
        {
            int productInStock = dal.Product.Read(p => p.ID == productID).InStock;
            double productPrice = dal.Product.Read(p => p.ID == productID).Price;
            BO.OrderItem? orderItem = new BO.OrderItem();
            orderItem = cart.Items.Find(item => item.ProductID == productID);
            if (orderItem == null)
                throw new BlNotExistException();

            if (newAmount > orderItem.Amount)
            {
                if (newAmount - orderItem.Amount > productInStock)
                    throw new BlOutOfStockException();
                cart.Price += (newAmount - orderItem.Amount) * productPrice;
                orderItem.TotalPrice += (newAmount - orderItem.Amount) * productPrice;
                orderItem.Amount = newAmount;
            }

            else if (newAmount == 0)
            {
                cart.Price -= productPrice * orderItem.Amount;
                cart.Items.Remove(orderItem);
            }
            else // newAmount>0 & newAmount<Amount
            {
                cart.Price -= productPrice * (orderItem.Amount - newAmount);
                orderItem.Amount = newAmount;
                orderItem.TotalPrice = productPrice * newAmount;
            }
            return cart;
        }
        catch (DalApi.NotExistException notExist)
        {
            throw new BlNotExistException(notExist);
        }
    }

    /// <summary>
    /// Confirmation method - for confirm all cart details before create an order.
    /// </summary>
    /// <param name="cart">pointer to the required cart</param>
    /// <param name="customerName"></param>
    /// <param name="customerMail"></param>
    /// <param name="customerAddress"></param>
    /// <exception cref="BlNullValueException"></exception>
    /// <exception cref="BlInvalidEmailException"></exception>
    /// <exception cref="BlInValidInputException"></exception>
    /// <exception cref="BlOutOfStockException"></exception>
    /// <exception cref="BlNotExistException"></exception>
    public void Confirmation(BO.Cart cart)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(cart.CustomerEmail);
            bool isValid = (addr.Address == cart.CustomerEmail);
            if (cart.CustomerName == "" || cart.CustomerAddress == "")
            {
                throw new BlNullValueException();
            }
            if (!isValid)
            {
                throw new BlInvalidEmailException();
            }
            int productInStock;
            DO.Order DoOrder = new DO.Order();
            DoOrder.OrderDate = DateTime.Now;
            DoOrder.ShipDate = DateTime.MinValue;
            DoOrder.DeliveryDate = DateTime.MinValue;
            DoOrder.CustomerName = cart.CustomerName;
            DoOrder.CustomerEmail = cart.CustomerEmail;
            DoOrder.CustomerAddress = cart.CustomerAddress;
            DoOrder.ID = 0;
            dal?.Order.Create(DoOrder);
            foreach (BO.OrderItem orderItem in cart.Items)
            {
                productInStock = dal.Product.Read(p => p.ID == orderItem.ProductID).InStock;
                if (orderItem.Amount < 0)
                {
                    throw new BlInValidInputException();
                }
                if (productInStock < orderItem.Amount)
                {
                    throw new BlOutOfStockException();
                }
                DO.OrderItem DoOrderItem = new DO.OrderItem();
                DoOrderItem.ID = orderItem.ID;
                DoOrderItem.ProductID = orderItem.ProductID;
                DoOrderItem.OrderID = DoOrder.ID;
                DoOrderItem.Amount = orderItem.Amount;
                DoOrderItem.Price = orderItem.TotalPrice;
                dal.OrderItem.Create(DoOrderItem);
                DO.Product DoProduct = dal.Product.Read(p => p.ID == DoOrderItem.ProductID);
                DoProduct.InStock -= orderItem.Amount;
                dal.Product.UpDate(DoProduct);
            }
        }
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }
}