using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;

/// <summary>
/// BLorder class implements bl order methods:
/// read order list <see cref="ReadOrderList"/> , 
/// Read specific Order Properties. <see cref="ReadOrderProperties"/> 
/// update an order status <see cref="UpdateOrderSent" /> <see cref="UpdateOrderDelivery"/>
/// and tracking order. <see cref="TrackOrder"/>
/// </summary>
/// 
internal class BLOrder : BlApi.IOrder
{
    /// <summary>
    /// creating Idal instance for using its methods and members in BLOrder.
    /// </summary>
    private IDal DalList { get; set; } = DalApi.Factory.Get();

    /// <summary>
    /// ReadOrderList method require from Dal-layer all orders in the OrderList.
    /// </summary>
    /// <returns> all orders in the orderList (for manager)</returns>
    public IEnumerable<BO.OrderForList> ReadOrderList()
    {
        IEnumerable<DO.Order> orders = DalList.Order.ReadAll();
        List<BO.OrderForList> orderList = new List<BO.OrderForList>();
        foreach (var order in orders)
        {
            BO.OrderForList orderForList = new BO.OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.TotalPrice = 0;
            orderForList.AmountOfItems = 0;
            IEnumerable<DO.OrderItem> orderItems = DalList.OrderItem.ReadAll(oi=> oi.OrderID==order.ID);
            foreach (var orderItem in orderItems)
            {
                orderForList.TotalPrice += orderItem.Price * orderItem.Amount;
                orderForList.AmountOfItems += orderItem.Amount;
            }
            if (order.DeliveryDate > DateTime.MinValue)
                orderForList.Status = (BO.eOrderStatus)2;
            else if (order.ShipDate > DateTime.MinValue)
                orderForList.Status = (BO.eOrderStatus)1;
            else
                orderForList.Status = (BO.eOrderStatus)0;
            orderList.Add(orderForList);
        }
        return orderList;
    }

    /// <summary>
    /// ReadOrderProperties method read all properties (from dal layer) for specific order .
    /// </summary>
    /// <param name="orderID">to find the require order</param>
    /// <returns></returns>
    /// <exception cref="BlInValidInputException"> if the order Id is invalid (negattive ID)</exception>
    /// <exception cref="BlNotExistException"></exception>

    public BO.Order ReadOrderProperties(int orderID)
    {
        BO.Order BoOrder = new BO.Order();
        try
        {
            if (orderID <= 0)
                throw new BlInValidInputException();
            DO.Order DoOrder = DalList.Order.Read(o => o.ID == orderID);
            var DoOrderItems = DalList.OrderItem.ReadAll();
            //var DoOrderItems = Dal.OrderItem.ReadByOrder(orderID);
            BoOrder.ID = orderID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAddress;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DoOrder.ShipDate;
            BoOrder.DeliveryDate = DoOrder.DeliveryDate;
            if (DoOrder.DeliveryDate > DateTime.MinValue)
                BoOrder.Status = BO.eOrderStatus.Delivered;
            else if (DoOrder.ShipDate > DateTime.MinValue)
                BoOrder.Status = BO.eOrderStatus.Shipped;
            else
                BoOrder.Status = 0;
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = oi.ID;
                orderItem.ProductID = oi.ProductID;
                orderItem.ProductName = DalList.Product.Read(p => p.ID == oi.ProductID).Name;
                orderItem.Amount = oi.Amount;
                orderItem.Price = oi.Price;
                orderItem.TotalPrice = oi.Amount * oi.Price;
                BoOrder.Items.Add(orderItem);
            }
        }
        catch (NotExistException notExist)
        {
            throw new BlNotExistException(notExist);
        }
        return BoOrder;
    }

    /// <summary>
    /// UpdateOrderSent method- updates orser's status to send - and the send-date.
    /// </summary>
    /// <param name="orderID">to find the require order </param>
    /// <returns> the updates order</returns>
    /// <exception cref="BlNotExistException"></exception>
    /// 
    public BO.Order UpdateOrderSent(int orderID)
    {
        BO.Order BoOrder = new BO.Order();
        try
        {
            DO.Order DoOrder = DalList.Order.Read(o=>o.ID==orderID);
            if (DoOrder.ID == 0)
                throw new BlNotExistException();
            DoOrder.ShipDate = DateTime.Now;
            DalList.Order.UpDate(DoOrder);
            BoOrder.ID = DoOrder.ID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAddress;
            BoOrder.Status = (BO.eOrderStatus)1;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DateTime.Now;
            BoOrder.DeliveryDate = DateTime.MinValue;
            BoOrder.TotalPrice = 0;
            IEnumerable<DO.OrderItem> DoOrderItems = DalList.OrderItem.ReadAll(oi => oi.OrderID == orderID);
            foreach (var OrderItem in DoOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = OrderItem.ID;
                orderItem.ProductID = OrderItem.ProductID;
                orderItem.ProductName = DalList.Product.Read(p => p.ID == OrderItem.ProductID).Name;
                orderItem.Amount = OrderItem.Amount;
                orderItem.Price = OrderItem.Price;
                orderItem.TotalPrice = OrderItem.Amount * OrderItem.Price;
                BoOrder.TotalPrice += orderItem.TotalPrice;
                BoOrder.Items.Add(orderItem);
            }
        }
        catch (NotExistException ex)
        {
            throw new BlNotExistException(ex);
        }
        return BoOrder;
    }

    /// <summary>
    /// UpdateOrderDelivery method- updates orser's status to delivery's  - and the delivery-date.
    /// </summary>
    /// <param name="orderID">to find the require order</param>
    /// <returns>the updates order</returns>
    /// <exception cref="BlWrongDateSequenceException"></exception>
    /// <exception cref="BlNotExistException"></exception>
    public BO.Order UpdateOrderDelivery(int orderID)
    {
        BO.Order BoOrder = new BO.Order();
        try
        {
            DO.Order DoOrder = DalList.Order.Read(o=>o.ID==  orderID);

            if (DoOrder.ShipDate == DateTime.MinValue)
                throw new BlWrongDateSequenceException();

            DoOrder.DeliveryDate = DateTime.Now;
            BoOrder.ID = DoOrder.ID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAddress;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DoOrder.ShipDate;
            BoOrder.DeliveryDate = DateTime.Now;
            BoOrder.Status = (BO.eOrderStatus)2;
            BoOrder.TotalPrice = 0;
            IEnumerable<DO.OrderItem> DoOrderItems = DalList.OrderItem.ReadAll(oi => oi.OrderID == orderID);
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = oi.ID;
                orderItem.ProductID = oi.ProductID;
                orderItem.ProductName = DalList.Product.Read(p => p.ID == oi.ProductID).Name;
                orderItem.Amount = oi.Amount;
                orderItem.Price = oi.Price;
                orderItem.TotalPrice = oi.Amount * oi.Price;
                BoOrder.TotalPrice += orderItem.TotalPrice;
                BoOrder.Items.Add(orderItem);
            }
        }
        catch (NotExistException err)
        {
            throw new BlNotExistException(err);
        }
        return BoOrder;
    }

    /// <summary>
    /// TrackOrder method - for tracking on specific order (to customer)
    /// </summary>
    /// <param name="orderID">to find the require order</param>
    /// <returns> list off all stages in delivery with their dates. </returns>
    /// <exception cref="BlNotExistException"></exception>
    
    public BO.OrderTracking TrackOrder(int orderID)
    {
        try
        {
           DO.Order order = DalList.Order.Read(o=>o.ID==orderID);
            BO.OrderTracking BoOrderTracking = new BO.OrderTracking();
            List<(DateTime, string)> list = new List<(DateTime, string)>();
            list.Add((order.OrderDate, BO.eOrderStatus.Ordered.ToString()));
            BoOrderTracking.TrackList.Add((order.OrderDate, BO.eOrderStatus.Ordered.ToString()));
            BoOrderTracking.Status = BO.eOrderStatus.Ordered;
            if (order.ShipDate > DateTime.MinValue)
            {
                BoOrderTracking.TrackList.Add((order.ShipDate, BO.eOrderStatus.Shipped.ToString()));
                BoOrderTracking.Status = BO.eOrderStatus.Shipped;
                if (order.DeliveryDate > DateTime.MinValue)
                {
                    BoOrderTracking.TrackList.Add((order.DeliveryDate, BO.eOrderStatus.Delivered.ToString()));
                    BoOrderTracking.Status = BO.eOrderStatus.Delivered;
                }
            }
            return BoOrderTracking;
        } 
        catch (NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }
}