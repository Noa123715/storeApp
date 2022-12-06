/// < summary >
/// CRUD operations department:
/// for adding a new order item list,
/// reading the existing order item,
/// updating order item and deletions.
/// </ summary >

using DO;
using DalApi;
namespace Dal;

public struct DalOrderItem : IOrderItem
{

    // create new order item.
    public  int Create(OrderItem newOrderItem)
    {
        int index = DataSource.orderItemList.FindIndex(item => item.ID == newOrderItem.ID);
        if (index != -1)
            throw new AlreadyExistException();
        
        DataSource.orderItemList.Add(newOrderItem);
        return newOrderItem.OrderID;
    }

    // Read orderItem methods

    // read method returns spesific orderItem by its ID.
    public OrderItem Read(int orderItemID)
    {
        foreach (OrderItem item in DataSource.orderItemList)
        {
            if (item.ID == orderItemID)
            {
                return item;
            }
        }
        throw new NotExistException();
    }
    //ReadByOrderID method receives orderId and returns all order-Items in specific order.
    public  IEnumerable<OrderItem> ReadByOrderID(int orderID)
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
        return DataSource.orderItemList?? throw new NotExistException();
    }

    public void Delete(int orderItemId)
    {
        int index = DataSource.orderItemList.FindIndex(item => item.ID == orderItemId);
        if (index == -1)
        {
            throw new NotExistException();
        }
        DataSource.orderItemList.RemoveAt(index);
       
    }

    public  void UpDate(OrderItem UpOrderItem)
    {
        int index = DataSource.orderItemList.FindIndex(item => item.ID == UpOrderItem.ID);
        if (index == -1)
        {
            throw new NotExistException();
        }
        DataSource.orderItemList[index] = UpOrderItem;



    }
}

