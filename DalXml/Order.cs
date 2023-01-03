namespace Dal;
using DalApi;
using DO;
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

        XElement ?id = rootConfig?.Element("orderID");
        int orderId = Convert.ToInt32(id?.Value);
        orderId++;
        id.Value= orderId.ToString();
        rootConfig?.Save(@"../../xml/config.xml");
        newOrder.ID= orderId;
        orderList.Add(newOrder);
       
        XElement orderXML = new("Order",
                        new XElement("ID", newOrder.ID),
                        new XElement("CustomerName", newOrder.CustomerName),
                        new XElement("CustomerEmail", newOrder.CustomerEmail),
                        new XElement("CustomerAddress", newOrder.CustomerAddress),
                        new XElement("OrderDate", newOrder.OrderDate),
                        new XElement("ShipDate", newOrder.ShipDate),
                        new XElement("DeliveryDate", newOrder.DeliveryDate));
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order> ReadAll(Func<DO.Order, bool>? condition = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order ReadByCondition(Func<DO.Order, bool> condition)
    {
        throw new NotImplementedException();
    }

    public void UpDate(DO.Order item)
    {
        throw new NotImplementedException();
    }

    int ICrud<DO.Order>.Create(DO.Order item)
    {
        throw new NotImplementedException();
    }
}