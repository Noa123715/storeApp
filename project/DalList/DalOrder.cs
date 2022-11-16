/// <summary>
/// CRUD operations department:
/// for adding a new order list,
/// reading the existing orders,
/// updating order lists and deletions.
/// </summary>

using System;
using Dal.DO;
namespace DalList;

public struct DalOrder
{
//    public static int CreateOrder(Order newOrder)
//    {
//        DataSource.orderList[DataSource.Config.orderIdx++] = newOrder;
//        return newOrder.ID;
//    }

//    public static Order ReadOrder(int id)
//    {
//        for (int i = 0; i < DataSource.Config.orderIdx; i++)
//        {
//            if (DataSource.orderList[i].ID == id)
//            {
//                return DataSource.orderList[i];
//            }
//        }
//        throw new Exception("The order was not found in the list");
//    }

//    public static Order[] ReadOrder()
//    {
//        Order[] newOrderList = new Order[DataSource.Config.orderIdx];
//        for (int i = 0; i < DataSource.Config.orderIdx; i++)
//        {
//            newOrderList[i] = DataSource.orderList[i];
//        }
//        return newOrderList;
//    }

//    public static void DeleteOrder(int id)
//    {
//        for (int i = 0; i < DataSource.Config.orderIdx - 1; i++)
//        {
//            if (DataSource.orderList[i].ID == id)
//            {
//                DataSource.orderList[i] = DataSource.orderList[DataSource.Config.orderIdx];
//                DataSource.Config.orderIdx--;
//                return;
//            }
//        }
//        if (DataSource.orderList[DataSource.Config.orderIdx].ID == id)
//        {
//            DataSource.Config.orderIdx--;
//            return;
//        }
//        throw new Exception("The order was not found in the list");
//    }

//    public static void UpDateOrder(Order UpOrder)
//    {
//        for (int i = 0; i < DataSource.Config.orderIdx; i++)
//        {
//            if (DataSource.orderList[i].ID == UpOrder.ID)
//            {
//                DataSource.orderList[i] = UpOrder;
//                return;
//            }
//        }
//        throw new Exception("The order was not found in the list");
//    }
}

