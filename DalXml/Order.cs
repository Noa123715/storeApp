namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
/// <summary>
/// CRUD operations department:
/// for adding a new order list,
/// reading the existing orders,
/// updating order lists and deletions.
/// </summary>
internal class Order : IOrder
{
    private List<DO.Order> orderList { get; set; }
    public Order()
    {
        XElement? root = XDocument.Load(@"../../xml/order.xml")?.Root;
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
    /// <summary>
    /// create a new order.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>ID for the created order</returns>
    /// <exception cref="AlreadyExistException"></exception>
    public int Create(DO.Order newOrder)
    {
        XElement? rootConfig = XDocument.Load(@"../../xml/config.xml").Root;

        XElement? id = rootConfig?.Element("orderID");
        int orderId = Convert.ToInt32(id?.Value);
        orderId++;
        id.Value= orderId.ToString();
        rootConfig?.Save(@"../../xml/config.xml");
        newOrder.ID= orderId;
        orderList.Add(newOrder);

        XElement xmlOrder = new("Order",
                                new XElement("ID", newOrder.ID),
                                new XElement("CustomerName", newOrder.CustomerName),
                                new XElement("CustomerEmail", newOrder.CustomerEmail),
                                new XElement("CustomerAddress", newOrder.CustomerAddress),
                                new XElement("OrderDate", newOrder.OrderDate),
                                new XElement("ShipDate", newOrder.ShipDate),
                                new XElement("DeliveryDate", newOrder.DeliveryDate));
        XElement ?root = XDocument.Load(@"../../xml/order.xml").Root;
        root?.Add(xmlOrder);
        root?.Save(@"../../xml/order.xml");
        return newOrder.ID;
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

  

    public IEnumerable<DO.Order> ReadAll(Func<DO.Order, bool>? condition = null)
    {
        if (condition is null)
            return orderList ?? throw new NotExistException();
        return orderList.Where(condition).ToList() ?? throw new NotExistException();
    }

    /// <summary>
    /// Read order's properties according to certain condition.
    /// </summary>
    /// <param name="condition"> predicate with the condition</param>
    /// <returns>the required order</returns>
    public DO.Order Read(Func<DO.Order, bool> condition)
    {

        return orderList.Where(condition).ToList()[0];
    }

    /// <summary>
    /// updates an order's properties.
    /// </summary>
    /// <param name="upOrder"></param>
    /// <exception cref="NotExistException"></exception>
    public void UpDate(DO.Order upOrder)
    {
        XElement? root = XDocument.Load(@"../../xml/order.xml").Root;
        XElement? XMLorder = root?.Elements("Order").Where(o => o.Element("ID")?.Value == upOrder.ID.ToString()).FirstOrDefault();
        if (XMLorder is null) { throw new NotExistException(); }
        XMLorder.Element("CustomerName").Value = upOrder.CustomerName;
        XMLorder.Element("CustomerEmail").Value = upOrder.CustomerEmail;
        XMLorder.Element("CustomerAddress").Value = upOrder.CustomerAddress;
        XMLorder.Element("OrderDate").Value = upOrder.OrderDate.ToString();
        XMLorder.Element("ShipDate").Value = upOrder.ShipDate.ToString();
        XMLorder.Element("DeliveryDate").Value = upOrder.DeliveryDate.ToString();
        upOrder.ID = Convert.ToInt32(XMLorder?.Element("ID")?.Value);
        int index = orderList.FindIndex(item => item.ID == upOrder.ID);
        orderList[index] = upOrder;


    }

}




    
