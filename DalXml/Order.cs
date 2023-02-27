namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// CRUD operations department:
/// for adding a new order list,
/// reading the existing orders,
/// updating order lists and deletions.
/// </summary>
internal class Order : IOrder
{
    private List<DO.Order>? OrderList { get; set; }


    /// <summary>
    /// create a new order.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>ID for the created order</returns>
    /// <exception cref="AlreadyExistException"></exception>
    public int Create(DO.Order newOrder)
    {
        List<DO.Order> orderList = ReadAll().ToList();
        XElement? rootConfig = XDocument.Load(@"..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("orderID");
        int orderId = Convert.ToInt32(id?.Value);
        newOrder.ID = ++orderId;

        id?.SetValue(orderId.ToString());
        rootConfig?.Save(@"..\xml\config.xml");
        XmlRootAttribute xRoot = new();
        xRoot.ElementName = "OrdersList";
        xRoot.IsNullable = true;
        StreamReader reader = new(@"..\xml\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? orders = (List<DO.Order>?)ser.Deserialize(reader);
        reader.Close();
        orders?.Add(newOrder);
        StreamWriter write = new(@"..\xml\order.xml");
        ser.Serialize(write, orders);
        write.Close();
        return newOrder.ID;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExistException"></exception>
    public void Delete(int id)
    {
        XElement? root = XDocument.Load(@"..\xml\order.xml").Root;
        XElement? order = root?.Elements("Order").Where(o => o.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        if (order is null)
        {
            throw new NotExistException();
        }
        order.Remove();
        OrderList?.Remove(OrderList.Find(o => o.ID == id));
        root?.Save(@"..\xml\order.xml");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotExistException"></exception>
    public IEnumerable<DO.Order> ReadAll(Func<DO.Order, bool>? condition = null)
    {
        XmlRootAttribute xRoot = new()
        {
            ElementName = "OrdersList",
            IsNullable = true
        };
        XmlSerializer ser = new(typeof(List<DO.Order>), xRoot);
        StreamReader reader = new(@"..\xml\order.xml");
        OrderList = (List<DO.Order>?)ser.Deserialize(reader);
        reader.Close();
        if (condition is null)
            return OrderList ?? throw new NotExistException();
        return OrderList?.Where(condition).ToList() ?? throw new NotExistException();
    }

    /// <summary>
    /// Read order's properties according to certain condition.
    /// </summary>
    /// <param name="condition"> predicate with the condition</param>
    /// <returns>the required order</returns>
    public DO.Order Read(Func<DO.Order, bool> condition)
    {
        OrderList = ReadAll(condition).ToList();
        if (OrderList.Count == 0) throw new NotExistException();
        DO.Order order = OrderList.FirstOrDefault();
        return order;
    }

    /// <summary>
    /// updates an order's properties.
    /// </summary>
    /// <param name="upOrder"></param>
    /// <exception cref="NotExistException"></exception>
    public void UpDate(DO.Order upOrder)
    {
        XElement? root = XDocument.Load(@"..\xml\order.xml").Root;
        XElement? XMLorder = root?.Elements("Order").Where(o => o.Element("ID")?.Value == upOrder.ID.ToString()).FirstOrDefault();
        if (XMLorder is null) { throw new NotExistException(); }
        XMLorder.Element("CustomerName").Value = upOrder.CustomerName;
        XMLorder.Element("CustomerEmail").Value = upOrder.CustomerEmail;
        XMLorder.Element("CustomerAddress").Value = upOrder.CustomerAddress;
        XMLorder.Element("OrderDate").Value = upOrder.OrderDate.ToString();
        XMLorder.Element("ShipDate").Value = upOrder.ShipDate.ToString();
        XMLorder.Element("DeliveryDate").Value = upOrder.DeliveryDate.ToString();
        upOrder.ID = Convert.ToInt32(XMLorder?.Element("ID")?.Value);
        int index = OrderList.FindIndex(item => item.ID == upOrder.ID);
        if (index == -1) throw new NotExistException();
        OrderList[index] = upOrder;
    }
}