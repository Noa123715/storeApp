/// < summary >
/// CRUD operations department:
/// for adding a new order item list,
/// reading the existing order item,
/// updating order item and deletions.
/// </ summary >

using Dal.DO;
namespace Dal;

public struct DalOrderItem
{
    public static int CreateOrderItem(OrderItem newOrderItem)
    {
        DataSource.orderItemList.Add(newOrderItem);
        return newOrderItem.OrderID;
    }

    public static OrderItem ReadOrderItem(int orderID)
    {
        foreach (OrderItem item in DataSource.orderItemList)
        {
            //לפי מה קוראים?
        }
        throw new Exception("The orderItem was not found in the list");
    }

    public static List<OrderItem> ReadOrderItem()
    {
        List<OrderItem>newOrderItemList = new List<OrderItem>();
        newOrderItemList.AddRange(DataSource.orderItemList);
        return newOrderItemList;
    }

    public static void DeleteOrderItem(int orderId)
    {
        DataSource.orderItemList.RemoveAll(item => item.OrderID == orderId);
        //throw new Exception("The orderItem was not found in the list");
    }

    public static void UpDateOrderItem(OrderItem UpOrderItem)
    {
        int index = DataSource.orderItemList.FindIndex(item => item.OrderID == UpOrderItem.OrderID);
        if (index == -1)
        {
            throw new Exception("The orderitem was not found in the list");
        }        
    }
}

