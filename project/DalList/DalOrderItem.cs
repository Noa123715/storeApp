/// < summary >
/// CRUD operations department:
/// for adding a new order item list,
/// reading the existing order item,
/// updating order item and deletions.
/// </ summary >

using DO;
using DalApi;
namespace Dal;

internal struct DalOrderItem : IOrderItem
{

    // create new order item.
    public  int Create(OrderItem newOrderItem)
    {
        DataSource.orderItemList.Add(newOrderItem);
        return newOrderItem.OrderID;
    }

    // Read orderItem methods

    public OrderItem Read(int orderItemID)
    {
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.ID == orderItemID)
            {
                return item;
            }
        }
        throw new Exception("תכתבי מה שצריך לכתוב כי אין לי מושג");
    }
    //ReadByOrderID method receives orderId and returns all order-Items in specific order.
    public  List<OrderItem> ReadByOrderID(int orderID)
    {
        List<OrderItem> itemsInOrder = new List<OrderItem>();
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.OrderID == orderID)
                itemsInOrder.Add(item);
        }
        return itemsInOrder ?? throw new NotExistException();

    }

    //ReadOrderItem method 2- receives orderId and productID and returns specific orderItem according to these parameters.
    public  OrderItem ReadByProdAndOrder(int prodID, int orderID)
    {
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.OrderID == orderID && item.ProductID == prodID)
                return item;
        }
        throw new NotExistException();

    }

    //ReadOrderItem method 3- returns the current list of order items.
    public  IEnumerable<OrderItem> ReadAll()
    {
        return DataSource.orderItemList;
    }

    public void Delete(int orderId)
    {
        DataSource.orderItemList.RemoveAll(item => item.OrderID == orderId);
        //throw new Exception("The orderItem was not found in the list");
    }

    public  void UpDate(OrderItem UpOrderItem)
    {
        int index = DataSource.orderItemList.FindIndex(item => item.OrderID == UpOrderItem.OrderID);
        if (index == -1)
        {
            throw new Exception("The orderitem was not found in the list");
        }        
    }
}

