
using System;
/// <summary>
/// 
/// </summary>

namespace Dal;

public struct DalOrder
{
    public static int CreateOrder(IOrder newOrder)
    {
        DataSource.orderList[DataSource.Config.OrderIdx++] = newOrder;
        return newOrder.ID;
    }

    public static IOrder ReadOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.orderList[i].ID == id)
            {
                return DataSource.orderList[i];
            }
        }
        throw new System.Exception("The order was not found in the list");
    }

    public static IOrder[] ReadOrder()
    {
        IOrder[] newOrderList = new IOrder[DataSource.Config.orderIdx];
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            newOrderList[i] = DataSource.orderList[i];
        }
        return newOrderList;
    }

    public static void DeleteOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx - 1; i++)
        {
            if (DataSource.orderList[i].ID == id)
            {
                DataSource.orderList[i] = DataSource.orderList[DataSource.Config.orderIdx];
                DataSource.Config.orderIdx--;
                return;
            }
        }
        if (DataSource.orderList[DataSource.Config.orderIdx] == id)
        {
            DataSource.Config.orderIdx--;
            return;
        }
        throw new System.Exception("The order was not found in the list");
    }

    public static void UpDateOrder(IOrder UpOrder)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.orderList[i].ID == UpOrder.ID)
            {
                DataSource.orderList[i] = UpOrder;
                return;
            }
        }
        throw new System.Exception("The order was not found in the list");
    }
}

