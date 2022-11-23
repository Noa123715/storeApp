/// <summary>
/// CRUD operations department:
/// for adding a new order list,
/// reading the existing orders,
/// updating order lists and deletions.
/// </summary>

using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder
{
    public int Create(Order newOrder)
    {
        DataSource.orderList.Add(newOrder);
        return newOrder.ID;
    }

    public Order Read(int id)
    {
        foreach (Order item in DataSource.orderList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        throw new Exception("The order was not found in the list");
    }

    public IEnumerable<Order> ReadAll()
    {
        List<Order> newOrderList = new List<Order>();
        newOrderList.AddRange(DataSource.orderList);
        return newOrderList;
    }

    public void Delete(int id)
    {
        DataSource.orderList.RemoveAll(item => item.ID == id);
        //throw new Exception("The order was not found in the list");
    }

    public void UpDate(Order UpOrder)

    {
        int index = DataSource.orderList.FindIndex(item => item.ID == UpOrder.ID);
        if (index == -1)
        {
            throw new Exception("The order was not found in the list");
        }
        DataSource.orderList.RemoveAt(index);
    }
} 