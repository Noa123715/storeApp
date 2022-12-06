/// <summary>
/// CRUD operations department:
/// for adding a new order list,
/// reading the existing orders,
/// updating order lists and deletions.
/// </summary>

using DO;
using DalApi;
namespace Dal;

public struct DalOrder : IOrder
{
    /// <summary>
    /// create a new order in orders list.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>ID for the created order</returns>
    /// <exception cref="AlreadyExistException"></exception>
    public int Create(Order newOrder)
    {

        int index = DataSource.orderList.FindIndex(item => item.ID == newOrder.ID);
        if (index != -1)
            throw new AlreadyExistException();
        DataSource.orderList.Add(newOrder);
        return newOrder.ID;
    }
    /// <summary>
    /// Read order's properties according to it's ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>the required </returns>
    /// <exception cref="NotExistException"></exception>
    public Order Read(int id)
    {
        foreach (Order item in DataSource.orderList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        throw new NotExistException();
    }

    public IEnumerable<Order> ReadAll()
    {
        List<Order> newOrderList = new List<Order>();
        newOrderList.AddRange(DataSource.orderList);
        return newOrderList;
    }

    public void Delete(int orderID)
    {
        int index = DataSource.orderList.FindIndex(item => item.ID == orderID);
        if (index == -1)
        {
            throw new NotExistException();
        }
        DataSource.orderList.RemoveAt(index);   
    }

    public void UpDate(Order UpOrder)

    {
        int index = DataSource.orderList.FindIndex(item => item.ID == UpOrder.ID);
        if (index == -1)
        {
            throw new NotExistException();
        }
        DataSource.orderList.RemoveAt(index);
    }
} 