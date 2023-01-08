namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
/// <summary>
/// 
/// </summary>
internal class Order : IOrder
{
    private List<DO.Order> orderList { get; set; }
    public Order()
    {
        XElement? root = XDocument.Load(@"../../xml/Order.xml")?.Root;
        DO.Order newOrder = new ();
        orderList = new List<DO.Order>();

        foreach (var xmlOrder in root?.Elements("Order"))
        {
            newOrder.ID = Convert.ToInt32(xmlOrder.Element("ID")?.Value);
            newOrder.CustomerName = xmlOrder?.Element("CustomerName")?.Value ;
            newOrder.CustomerEmail = xmlOrder?.Element("CustomerEmail")?.Value;
            newOrder.CustomerAddress = xmlOrder?.Element("CustomerAddress")?.Value;
            newOrder.OrderDate = Convert.ToDateTime(xmlOrder?.Element("OrderDate")?.Value);
            newOrder.ShipDate = Convert.ToDateTime(xmlOrder?.Element("ShipDate")?.Value);
            newOrder.DeliveryDate = Convert.ToDateTime(xmlOrder?.Element("DeliveryDate")?.Value);
            orderList.Add(newOrder);
        }

    }

    public void Create(DO.Order newOrder)
    {
        XElement? rootConfig = XDocument.Load(@"../../xml/config.xml").Root;

        XElement? id = rootConfig?.Element("orderID");
        int orderId = Convert.ToInt32(id?.Value);
        orderId++;
        id.Value= orderId.ToString();
        rootConfig?.Save(@"../../xml/config.xml");
        newOrder.ID= orderId;
        orderList.Add(newOrder);

        XElement xElement = new("Order",
                                new XElement("ID", newOrder.ID),
                                new XElement("CustomerName", newOrder.CustomerName),
                                new XElement("CustomerEmail", newOrder.CustomerEmail),
                                new XElement("CustomerAddress", newOrder.CustomerAddress),
                                new XElement("OrderDate", newOrder.OrderDate),
                                new XElement("ShipDate", newOrder.ShipDate),
                                new XElement("DeliveryDate", newOrder.DeliveryDate));
        XElement ?root = XDocument.Load(@"../../xml/order.xml").Root;
        root?.Add(newOrder);
        root?.Save(@"../../xml/order.xml");
    }

    public void Delete(int id)
    {
        XElement ?root = XDocument.Load(@"../../xml/order.xml").Root;
        XElement? order = root?.Elements("Order").Where(o => o.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        if(order is null)
        {
            throw new NotExistException();
        }
        order.Remove();
        orderList.Remove(orderList.Find(o=> o.ID==id));
        root?.Save(@"../../xml/order.xml");

    }

    public DO.Order Read(int id)
    {
        foreach (DO.Order order in orderList)
        {
            if (order.ID == id)
            {
                return order;
            }
        }
        throw new NotExistException();
    }

    public IEnumerable<DO.Order> ReadAll(Func<DO.Order, bool>? condition = null)
    {
        if (condition is null)
            return orderList ?? throw new NotExistException();
        return orderList.Where(condition).ToList() ?? throw new NotExistException();
    }

    public DO.Order ReadByCondition(Func<DO.Order, bool> condition)
    {
              
       
       
        return orderList.Where(condition).ToList()[0];
    }

    public void UpDate(DO.Order upOrder)
    {
        XElement? root = XDocument.Load(@"../../xml/order.xml").Root;
        XElement? XMLorder = root?.Elements("Order").Where(o => o.Element("ID")?.Value == upOrder.ID.ToString()).FirstOrDefault();
        if (XMLorder is null)   { throw new NotExistException();}
        XMLorder.Element("CustomerName").Value= upOrder.CustomerName;
             new XElement(, newOrder.ID),
                                new XElement(, newOrder.CustomerName),
                                new XElement("CustomerEmail", newOrder.CustomerEmail),
                                new XElement("CustomerAddress", newOrder.CustomerAddress),
                                new XElement("OrderDate", newOrder.OrderDate),
                                new XElement("ShipDate", newOrder.ShipDate),
                                new XElement("DeliveryDate", newOrder.DeliveryDate));


        int index = orderList.FindIndex(item => item.ID == upOrder.ID);
        orderList[index] = upOrder;
        

    }

    int ICrud<DO.Order>.Create(DO.Order item)
    {
        throw new NotImplementedException();
    }
}