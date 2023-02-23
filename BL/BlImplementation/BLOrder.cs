using BlApi;
using BO;
using Dal;
using DalApi;
using System.Diagnostics;

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
    private IDal Dal { get; set; } = DalApi.Factory.Get();

    /// <summary>
    /// ReadOrderList method require from Dal-layer all orders in the OrderList.
    /// </summary>
    /// <returns> all orders in the orderList (for manager)</returns>
    public IEnumerable<BO.OrderForList> ReadOrderList()
    {
        IEnumerable<DO.Order> orders = Dal.Order.ReadAll();
        List<BO.OrderForList> orderList = new();
        foreach (var order in orders)
        {
            BO.OrderForList orderForList = new();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.TotalPrice = 0;
            orderForList.AmountOfItems = 0;
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.ReadAll(oi => oi.OrderID == order.ID);
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
        BO.Order BoOrder = new();
        try
        {
            if (orderID <= 0)
                throw new BlNegativeInputException();
            DO.Order DoOrder = Dal.Order.Read(o => o.ID == orderID);
            var DoOrderItems = Dal.OrderItem.ReadAll();

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
                BO.OrderItem orderItem = new()
                {
                    ID = oi.ID,
                    ProductID = oi.ProductID,
                    ProductName = Dal.Product.Read(p => p.ID == oi.ProductID).Name,
                    Amount = oi.Amount,
                    Price = oi.Price,
                    TotalPrice = oi.Amount * oi.Price,
                };
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
            if (orderID <= 0) throw new BlNegativeInputException();
            DO.Order DoOrder = Dal.Order.Read(o => o.ID == orderID);

            DoOrder.ShipDate = DateTime.Now;
            Dal.Order.UpDate(DoOrder);
            BoOrder.ID = DoOrder.ID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAddress;
            BoOrder.Status = (BO.eOrderStatus)1;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DateTime.Now;
            BoOrder.DeliveryDate = DateTime.MinValue;
            BoOrder.TotalPrice = 0;
            IEnumerable<DO.OrderItem> DoOrderItems = Dal.OrderItem.ReadAll(oi => oi.OrderID == orderID);
            foreach (var OrderItem in DoOrderItems)
            {
                BO.OrderItem orderItem = new();
                orderItem.ID = OrderItem.ID;
                orderItem.ProductID = OrderItem.ProductID;
                orderItem.ProductName = Dal.Product.Read(p => p.ID == OrderItem.ProductID).Name;
                orderItem.Amount = OrderItem.Amount;
                orderItem.Price = OrderItem.Price;
                orderItem.TotalPrice = OrderItem.Amount * OrderItem.Price;
                BoOrder.TotalPrice += orderItem.TotalPrice;
                BoOrder.Items.Add(orderItem);
            }
        }
        catch (DalApi.NotExistException ex)
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
    public Order UpdateOrderDelivery(int orderID)
    {
        Order BoOrder = new();
        try
        {   
            DO.Order DoOrder = Dal.Order.Read(o => o.ID == orderID);

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
            IEnumerable<DO.OrderItem> DoOrderItems = Dal.OrderItem.ReadAll(oi => oi.OrderID == orderID);
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new()
                {
                    ID = oi.ID,
                    ProductID = oi.ProductID,
                    ProductName = Dal.Product.Read(p => p.ID == oi.ProductID).Name,
                    Amount = oi.Amount,
                    Price = oi.Price,
                    TotalPrice = oi.Amount * oi.Price,
                };
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
            DO.Order order = Dal.Order.Read(o => o.ID == orderID);
            BO.OrderTracking BoOrderTracking = new();
            BoOrderTracking.ID = order.ID;
            BoOrderTracking.TrackList.Add(new Tuple<DateTime?, string>(order.OrderDate, BO.eOrderStatus.Ordered.ToString()));
            BoOrderTracking.Status = BO.eOrderStatus.Ordered;
            if (order.ShipDate > DateTime.MinValue)
            {
                BoOrderTracking.TrackList.Add(new Tuple<DateTime?, string>(order.ShipDate, BO.eOrderStatus.Shipped.ToString()));
                BoOrderTracking.Status = BO.eOrderStatus.Shipped;
                if (order.DeliveryDate > DateTime.MinValue)
                {
                    BoOrderTracking.TrackList.Add(new Tuple<DateTime?, string>(order.DeliveryDate, BO.eOrderStatus.Delivered.ToString()));
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

    public Order AddAmount(int orderId, int productId, int? addOrSubstract = null)
    {
        try
        {
            DO.OrderItem oi;
            if (addOrSubstract != null)
            {
                oi = Dal.OrderItem.Read(o => o.OrderID == orderId && o.ProductID == productId);
                if (addOrSubstract == 1)
                {
                    oi.Amount++;
                    Dal.OrderItem.UpDate(oi);
                }   
                else if (addOrSubstract == -1)
                {
                    oi.Amount--;
                    Dal.OrderItem.UpDate(oi);
                }
                else if (addOrSubstract == 0)
                    Dal.OrderItem.Delete(oi.ID);
                else
                    throw new BlInValidInputException();
            }
            else
            {
                oi = new();
                oi.OrderID = orderId;
                oi.ProductID = productId;
                oi.Price = Dal.Product.Read(p => p.ID == productId).Price;
                oi.Amount = 1;
                Dal.OrderItem.Create(oi);
            }
        }
        catch (BlApi.BlInValidInputException ex)
        {
            throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
        return ReadOrderProperties(orderId);
    }

    /// <summary>
    /// ChooseOrder method chooses order for simulator.
    /// </summary>
    /// <returns></returns>
    public int? ChooseOrder()
    {
        try
        {
            IEnumerable<DO.Order>? confirmDateOrders = Dal.Order.ReadAll(o => o.ShipDate == null);
            IEnumerable<DO.Order>? shipDateOrders = Dal.Order.ReadAll(o => (o.ShipDate != null && o.DeliveryDate == null));
            DateTime? minConfirmDate = confirmDateOrders.Min(x => x.OrderDate);
            DateTime? minShipDate = shipDateOrders.Min(x => x.ShipDate);
            DO.Order minConfirmOrderDate = confirmDateOrders.Where(o => o.OrderDate == minConfirmDate).FirstOrDefault();
            DO.Order minShipOrderDate = shipDateOrders.Where(o => o.ShipDate == minShipDate).FirstOrDefault();
            if (confirmDateOrders == null && shipDateOrders == null) return null;
            if (confirmDateOrders == null) return minShipOrderDate.ID;
            if (shipDateOrders == null) return minConfirmOrderDate.ID;
            return minConfirmDate < minShipDate ? minConfirmOrderDate.ID : minShipOrderDate.ID;
        }
        catch (NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }
}