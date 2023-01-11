namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
/// < summary >
/// CRUD operations class:
/// for adding a new order item list,
/// reading the existing order item,
/// updating order item and deletions.
/// </ summary >
internal class OrderItem : IOrderItem
{
    
    private List<DO.OrderItem> orderItemList { get; set; }
    /// <summary>
    /// orderitem ctor- getting data from xml to list.
    /// </summary>
     public OrderItem()
    {
        XElement? root = XDocument.Load(@"../../xml/orderItem.xml")?.Root;
        DO.OrderItem orderItem = new DO.OrderItem();
    } 

    // creates new order item
    public int Create(DO.OrderItem newOrderItem)
    {
        XElement? rootConfig = XDocument.Load(@"../../xml/config.xml").Root;

        XElement? id = rootConfig?.Element("orderItemID");
        int orderItemID = Convert.ToInt32(id?.Value);
        orderItemID++;
        id.Value = orderItemID.ToString();
        rootConfig?.Save(@"../../xml/config.xml");
        newOrderItem.ID = orderItemID;
        orderItemList.Add(newOrderItem);

        XElement xmlOrderItem = new("OrderItem",
                                new XElement("ID", newOrderItem.ID.ToString()),
                                new XElement("ProductID", newOrderItem.ProductID.ToString()),
                                new XElement("OrderID", newOrderItem.OrderID.ToString()),
                                new XElement("Amount", newOrderItem.Amount),
                                new XElement("Price", newOrderItem.Price)
                               );
        XElement? root = XDocument.Load(@"../../xml/orderItem.xml").Root;
        root?.Add(xmlOrderItem);
        root?.Save(@"../../xml/orderItem.xml");
        return newOrderItem.ID;

    }

 
    // read all orderitems -according to specific condition or all.
    public IEnumerable<DO.OrderItem> ReadAll(Func<DO.OrderItem, bool>? condition = null)
    {
        if (condition is null)
            return orderItemList ?? throw new NotExistException();
        return orderItemList.Where(condition).ToList() ?? throw new NotExistException();
    }

    /// <summary>
    /// read specific order item according to specific condition. e.g, certain id.
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public DO.OrderItem Read(Func<DO.OrderItem, bool> condition)

    {
        return orderItemList.Where(condition).ToList()[0];
    }

    /// <summary>
    /// updates orderItem's data. 
    /// </summary>
    /// <param name="upOrderItem"></param>
    /// <exception cref="NotExistException"></exception>
    public void UpDate(DO.OrderItem upOrderItem)
    {

        XElement? oiRoot = XDocument.Load(@"../../xml/orderItem.xml").Root;
        XElement? XMLorderItem = oiRoot?.Elements("OrderItem").Where(o => o.Element("ID")?.Value == upOrderItem.ID.ToString()).FirstOrDefault();
        if (XMLorderItem is null) { throw new NotExistException(); }
        XMLorderItem.Element("ProductID").Value = upOrderItem.ProductID.ToString();
        XMLorderItem.Element("OrderID").Value = upOrderItem.OrderID.ToString();
        XMLorderItem.Element("Amount").Value = upOrderItem.Amount.ToString();
        XMLorderItem.Element("Price").Value = upOrderItem.Price.ToString();
        oiRoot?.Save(@"../../xml/orderItem.xml");
        int index = orderItemList.FindIndex(item => item.ID == upOrderItem.ID);
        orderItemList[index] = upOrderItem;
    }

    /// <summary>
    ///deletes orderItem according to its ID
    /// </summary>
    /// <param name="orderItemId"></param>
    /// <exception cref="NotExistException"></exception>
    public void Delete(int orderItemId)
    {

        XElement ? oiRoot= XDocument.Load(@"../../xml/orderItem.xml").Root;
        XElement? XMLorderItem = oiRoot?.Elements("OrderItem").Where(o => o.Element("ID")?.Value == orderItemId.ToString()).FirstOrDefault();
        if (XMLorderItem is null) { throw new NotExistException(); }
        XMLorderItem.Remove();
        oiRoot?.Save(@"../../xml/orderItem.xml");
        int index = orderItemList.FindIndex(item => item.ID == orderItemId);
        orderItemList.RemoveAt(index);
       }
   
}
