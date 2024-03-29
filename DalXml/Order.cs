﻿namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
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
        XmlRootAttribute xmlRoot = new()
        {
            ElementName = "OrdersList",
            IsNullable = true
        };
        StreamReader orderReader = new(@"..\xml\order.xml");
        XmlSerializer ser = new(typeof(List<DO.Order>), xmlRoot);
        List<DO.Order>? orderList = (List<DO.Order>?)ser.Deserialize(orderReader);
        orderReader.Close();
        DO.Order order1 = orderList.Where(order => order.ID == upOrder.ID).FirstOrDefault();
        if (order1.Equals(default(DO.Order)))
        {
            throw new NotExistException();
        }
        orderList.Remove(order1);
        orderList.Add(upOrder);
        StreamWriter pWrite = new(@"..\xml\order.xml");
        ser.Serialize(pWrite, orderList);
        pWrite.Close();
    }
}