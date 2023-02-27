using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
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
        IEnumerable<DO.Order> orders;
        lock (Dal)
        {

            orders = Dal.Order.ReadAll();
        }
        List<BO.OrderForList> orderList = new();
        foreach (var order in orders)
        {
            BO.OrderForList orderForList = new();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.TotalPrice = 0;
            orderForList.AmountOfItems = 0;
            IEnumerable<DO.OrderItem> orderItems;
            lock (Dal)
            {
                orderItems = Dal.OrderItem.ReadAll(oi => oi.OrderID == order.ID);
            }
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
            DO.Order DoOrder;
            IEnumerable<DO.OrderItem> DoOrderItems;
            lock (Dal)
            {
                DoOrder = Dal.Order.Read(o => o.ID == orderID);
                DoOrderItems = Dal.OrderItem.ReadAll();
            }
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
                    Amount = oi.Amount,
                    Price = oi.Price,
                    TotalPrice = oi.Amount * oi.Price,
                };
                lock (Dal)
                {

                    orderItem.ProductName = Dal.Product.Read(p => p.ID == oi.ProductID).Name;
                }
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
            DO.Order DoOrder;
            lock (Dal)
            {
                DoOrder = Dal.Order.Read(o => o.ID == orderID);
            }
            DoOrder.ShipDate = DateTime.Now;
            lock (Dal)
            {
                Dal.Order.UpDate(DoOrder);
            }
            BoOrder.ID = DoOrder.ID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAddress;
            BoOrder.Status = (BO.eOrderStatus)1;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DateTime.Now;
            BoOrder.DeliveryDate = DateTime.MinValue;
            BoOrder.TotalPrice = 0;
            IEnumerable<DO.OrderItem> DoOrderItems;
            lock (Dal)
            {
                DoOrderItems = Dal.OrderItem.ReadAll(oi => oi.OrderID == orderID);
            }
            foreach (var OrderItem in DoOrderItems)
            {
                BO.OrderItem orderItem = new();
                orderItem.ID = OrderItem.ID;
                orderItem.ProductID = OrderItem.ProductID;
                lock (Dal)
                {
                    orderItem.ProductName = Dal.Product.Read(p => p.ID == OrderItem.ProductID).Name;
                }
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
    public BO.Order UpdateOrderDelivery(int orderID)
    {
        BO.Order BoOrder = new();
        try
        {
            DO.Order DoOrder;
            lock (Dal)
            {
                DoOrder = Dal.Order.Read(o => o.ID == orderID);
            }
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
            IEnumerable<DO.OrderItem> DoOrderItems;
            lock (Dal)
            {
                DoOrderItems = Dal.OrderItem.ReadAll(oi => oi.OrderID == orderID);
            }
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new()
                {
                    ID = oi.ID,
                    ProductID = oi.ProductID,
                  
                    Amount = oi.Amount,
                    Price = oi.Price,
                    TotalPrice = oi.Amount * oi.Price,
                };
                lock (Dal)
                {
                    orderItem.ProductName = Dal.Product.Read(p => p.ID == oi.ProductID).Name;
                }
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
            DO.Order order;
            lock (Dal)
            {
                order = Dal.Order.Read(o => o.ID == orderID);
            }
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

    public BO.Order AddAmount(int orderId, int productId, int? addOrSubstract = null)
    {
        try
        {
            DO.OrderItem oi;
            if (addOrSubstract != null)
            {
                lock (Dal)
                {
                    oi = Dal.OrderItem.Read(o => o.OrderID == orderId && o.ProductID == productId);
                }
                if (addOrSubstract == 1)
                {
                    oi.Amount++;
                    lock (Dal)
                    {
                        Dal.OrderItem.UpDate(oi);
                    }
                }
                else if (addOrSubstract == -1)
                {
                    oi.Amount--;
                    lock (Dal) Dal.OrderItem.UpDate(oi);
                 }
                else if (addOrSubstract == 0)
                   lock(Dal) Dal.OrderItem.Delete(oi.ID);
                else
                    throw new BlInValidInputException();
            }
            else
            {
                oi = new();
                oi.OrderID = orderId;
                oi.ProductID = productId;
                lock(Dal) oi.Price = Dal.Product.Read(p => p.ID == productId).Price;
                oi.Amount = 1;
               lock(Dal) Dal.OrderItem.Create(oi);
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
            IEnumerable<DO.Order>? confirmDateOrders;
            IEnumerable<DO.Order>? shipDateOrders;
            lock (Dal)
            {
                confirmDateOrders = Dal.Order.ReadAll(o => o.ShipDate == DateTime.MinValue);
                shipDateOrders = Dal.Order.ReadAll(o => (o.ShipDate != DateTime.MinValue && o.DeliveryDate == DateTime.MinValue));
            }
            DateTime? minConfirmDate = confirmDateOrders.Min(x => x.OrderDate);
            DateTime? minShipDate = shipDateOrders.Min(x => x.ShipDate);
            DO.Order minConfirmOrderDate = confirmDateOrders.Where(o => o.OrderDate == minConfirmDate).FirstOrDefault();
            DO.Order minShipOrderDate = shipDateOrders.Where(o => o.ShipDate == minShipDate).FirstOrDefault();
            if (confirmDateOrders == default(IEnumerable<DO.Order>) && shipDateOrders == default(IEnumerable<DO.Order>)) return 0;
            if (confirmDateOrders == default(IEnumerable<DO.Order>)) return minShipOrderDate.ID;
            else return minConfirmOrderDate.ID;

        }
        catch (NotExistException err)
        {
            throw new BlNotExistException(err);
        }
    }
}