using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;

internal class BLOrder : BlApi.IOrder
{
    private DalList DalList { get; set; } = new();
    public IEnumerable<BO.OrderForList> ReadOrderList()
    {
        var orders = DalList.Order.ReadAll();
        List<BO.OrderForList> orderList = new List<BO.OrderForList>();
        foreach (var order in orders)
        {
            BO.OrderForList orderForList = new BO.OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.TotalPrice = 0;
            orderForList.AmountOfItems = 0;
            var orderItems = DalList.OrderItem.ReadAll();
            //var orderItems = Dal.OrderItem.ReadByOrderID(order.ID);
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

    public BO.Order ReadOrderProperties(int orderID)
    {
        BO.Order BoOrder = new BO.Order();
        try
        {
        if (orderID <= 0)
            throw new BlNotExistException();
        DO.Order DoOrder = DalList.Order.Read(orderID);
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
            orderItem.ProductName = DalList.Product.Read(oi.ProductID).Name;
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

    public BO.Order UpdateOrderSent(int orderID)
    {
    BO.Order BoOrder = new BO.Order();
        try
        {
            DO.Order DoOrder = DalList.Order.Read(orderID);
        if (DoOrder.ID == 0)
            throw new BlNotExistException();
        //if (DoOrder.ShipDate != DateTime.MinValue)
        //   throw new BlNoNeedToUpdateException(); // לדעתי מיותר, האם אסור לעדכן תאריך שליחה?
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
        var DoOrderItems = DalList.OrderItem.ReadAll();  // לעשות את זה var או Ienumerable?
        //var DoOrderItems = Dal.OrderItem.ReadByOrder(orderID);
        foreach (var OrderItem in DoOrderItems)
        {
            BO.OrderItem orderItem = new BO.OrderItem();
            orderItem.ID = OrderItem.ID;
            orderItem.ProductID = OrderItem.ProductID;
            orderItem.ProductName = DalList.Product.Read(OrderItem.ProductID).Name;
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

    public BO.Order UpdateOrderDelivery(int orderID)
    {
        BO.Order BoOrder = new BO.Order();
        try
        {
            var DoOrder = DalList.Order.Read(orderID);
            //if (DoOrder.ID == 0)
            //    throw new BlNotExistException();  //לדעתי הדאל אמור לזרוק את זה במקרה ולא נמצא
            if (DoOrder.ShipDate == DateTime.MinValue)
                throw new BlWrongDateSequenceException();
            //if (DoOrder.DeliveryDate != DateTime.MinValue)
            //    throw new BlNoNeedToUpdateException(); // לדעתי פעולה חוקית- עדכון תאריך שליחה.
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
            var DoOrderItems = DalList.OrderItem.ReadAll();
            //var DoOrderItems = Dal.OrderItem.ReadByOrder(orderID);
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = oi.ID;
                orderItem.ProductID = oi.ProductID;
                orderItem.ProductName = DalList.Product.Read(oi.ProductID).Name;
                orderItem.Amount = oi.Amount;
                orderItem.Price = oi.Price;
                orderItem.TotalPrice = oi.Amount * oi.Price;
                BoOrder.TotalPrice += orderItem.TotalPrice;
                BoOrder.Items.Add(orderItem);
            }
        }

        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
     
       
        return BoOrder;
    }

    public BO.OrderTracking TrackOrder(int orderID)
    {
        try
        {
            var order = DalList.Order.Read(orderID);
            //if (order.ID == 0)
            //    throw new BlEntityNotFoundException(); //מיותר לדעתי, כנ"ל
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
        catch (DalApi.NotExistException err)
        {
            throw new BlNotExistException(err);
        }
       
    }
}
