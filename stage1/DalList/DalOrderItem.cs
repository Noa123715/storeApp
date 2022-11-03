/// <summary>
/// 
/// </summary>

namespace Dal;

public struct DalOrderItem
{
    public static int CreateOrderItem(IOrderItem newOrderItem)
    {
        DataSource.orderItemList[DataSource.Config.OrderItemIdx++] = newOrderItem;
        return newOrderItem.OrderId;
    }

    public static IOrderItem ReadOrderItem(int orderID)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIdx; i++)
        {
            if (DataSource.Config.orderItemList[i].OrderID == orderID)
            {
                return DataSource.Config.orderItemList[i];
            }
        }
        throw new Exception("The orderItem was not found in the list");
    }

    public static IOrderItem[] ReadOrderItem()
    {
        IOrderItem[] newOrderItemList = new IOrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            newOrderItemList[i] = DataSource.Config.orderItemList[i];
        }
        return newOrderItemList;
    }

    public static void DeleteOrderItem(int orderId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx - 1; i++)
        {
            if (DataSource.Config.orderItemList[i].OrderID == orderId)
            {
                DataSource.Config.orderItemList[i] = DataSource.Config.orderItemList[DataSource.Config.orderItemIdx];
                DataSource.Config.orderItemIdx--;
                return;
            }
        }
        if (DataSource.Config.orderItemList[DataSource.Config.orderItemIdx].OrderID == orderId)
        {
            DataSource.Config.orderItemIdx--;
            return;
        }
        throw new Exception("The orderItem was not found in the list");
    }

    public static void UpDateOrderItem(IOrderItem UpOrderItem)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.Config.orderItemList[i].OrderID == UpOrderItem.OrderID)
            {
                DataSource.Config.orderItemList[i] = UpOrderItem;
                return;
            }
        }
        throw new Exception("The orderitem was not found in the list");
    }
}

