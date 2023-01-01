namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class Order : IOrder
{
    private List<Order> orderList { get; set; }
    public Order()
    {
        XElement root = XDocument.Load("../../xml/Order.xml").Root;
        Order order = new();
        orderList = new List<Order>();

    }
    //public int Create(DO.Order order)
    //{

    //}

    //public Order Read(int id)
    //{

    //}

    //public IEnumerable<Order> ReadAll(Func<Order, bool>? condition = null)
    //{

    //}

    //public Order ReadByCondition(Func<Order,bool> condition)
    //{

    //}

    //public void UpDate(Order order)
    //{

    //}

    //public void Delete(int id)
    //{

    //}
    public int Create(DO.Order item)
    {
      
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
}