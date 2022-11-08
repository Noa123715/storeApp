/// <summary>
/// CRUD operations department:
/// for adding a new order item list,
/// reading the existing order item,
/// updating order item and deletions.
/// </summary>
using Dal.DO;
namespace DalList;

public struct DalOrderItem
{
    public static int CreateOrderItem(IOrderItem newOrderItem)
    {
        DataSource.orderItemList[DataSource.Config.orderItemIdx++] = newOrderItem;
        return newOrderItem.OrderID;
    }

    public static IOrderItem ReadOrderItem(int orderID)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.orderItemList[i].OrderID == orderID)
            {
                return DataSource.orderItemList[i];
            }
        }
        throw new System.Exception("The orderItem was not found in the list");
    }

    public static IOrderItem[] ReadOrderItem()
    {
        IOrderItem[] newOrderItemList = new IOrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            newOrderItemList[i] = DataSource.orderItemList[i];
        }
        return newOrderItemList;
    }

    public static void DeleteOrderItem(int orderId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx - 1; i++)
        {
            if (DataSource.orderItemList[i].OrderID == orderId)
            {
                DataSource.orderItemList[i] = DataSource.orderItemList[DataSource.Config.orderItemIdx];
                DataSource.Config.orderItemIdx--;
                return;
            }
        }
        if (DataSource.orderItemList[DataSource.Config.orderItemIdx].OrderID == orderId)
        {
            DataSource.Config.orderItemIdx--;
            return;
        }
        throw new System.Exception("The orderItem was not found in the list");
    }

    public static void UpDateOrderItem(IOrderItem UpOrderItem)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.orderItemList[i].OrderID == UpOrderItem.OrderID)
            {
                DataSource.orderItemList[i] = UpOrderItem;
                return;
            }
        }
        throw new System.Exception("The orderitem was not found in the list");
    }
}

