namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    private List<DO.OrderItem> orderItemsList { get; set; }

    public OrderItem()
    {
        XElement? root = XDocument.Load(@"../../xml/Order.xml")?.Root;
        DO.OrderItem orderItem = new DO.OrderItem();
    } 

    public int Create(OrderItem orderItem)
    {

    }

    public OrderItem Read(int id)
    {

    }

    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? condition = null)
    {

    }

    public OrderItem ReadByCondition(Func<OrderItem, bool> condition)
    {

    }

    public eUpDateOrder(OrderItem orderItem)
    {

    }

    public void Delete(int id)
    {

    }
}
