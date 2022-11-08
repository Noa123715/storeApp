/// <summary>
/// CRUD operations department:
/// for adding a new order list,
/// reading the existing orders,
/// updating order lists and deletions.
/// </summary>

using Dal.DO;
namespace DalList;

public struct DalOrder : ICrud<IOrder>
{
    public static int CreateOrder(IOrder newOrder)
    {
        DataSource.orderList[DataSource.Config.orderIdx++] = newOrder;
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
        throw new Exception("The order was not found in the list");
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
        if (DataSource.orderList[DataSource.Config.orderIdx].ID == id)
        {
            DataSource.Config.orderIdx--;
            return;
        }
        throw new Exception("The order was not found in the list");
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
        throw new Exception("The order was not found in the list");
    }

    public void Create(IOrder newOrder)
    {
        try
        {
            Console.WriteLine("Please enter the order details:");
            newOrder.ID = DataSource.Config.OrderId;
            Console.WriteLine("Please enter your name: ");
            newOrder.CustomerName = Console.ReadLine();
            Console.WriteLine("Please enter your mail: ");
            newOrder.CustomerEmail = Console.ReadLine();
            Console.WriteLine("Please enter your adress: ");
            newOrder.CustomerAdress = Console.ReadLine();
            Console.WriteLine("Please enter the order's date: (in formate dd/mm/yy)");
            DateTime orderDate;
            if (!DateTime.TryParse(Console.ReadLine(), out orderDate))
                throw new Exception("The date format does not match the value");
            newOrder.OrderDate = orderDate;
            int id = CreateOrder(newOrder);
            Console.WriteLine($@"The new id is: {id}");
        }
        catch (Exception error)
        {

            throw new Exception($@"This is the error: {error}");
        }
    }
    public IOrder ReadById(int id)
    {
        IOrder order = new IOrder();
        order = ReadOrder(id);
        Console.WriteLine(order);
        return order;
    }
    public IOrder[] ReadAll()
    {
        IOrder[] allOrders = ReadOrder();
        return allOrders;
    }
    public void Update(IOrder order, int id, int option)
    {
        order = ReadOrder(id);
        //Accepting the user's choice
        switch (option)
        {
            case (int)eUpDateOrder.Name: //update the name of the order's owner
                Console.WriteLine("Please enter the name to update: ");
                order.CustomerName = Console.ReadLine();
                break;
            case (int)eUpDateOrder.Mail: //update the mail of the order's owner
                Console.WriteLine("Please enter the mail to update: ");
                order.CustomerEmail = Console.ReadLine();
                break;
            case (int)eUpDateOrder.Adress: //update the adress to send the order
                Console.WriteLine("Please enter the adress to update: ");
                order.CustomerAdress = Console.ReadLine();
                break;
            default:
                throw new Exception("Wrong number!");

        }
        UpDateOrder(order);
    }
    public void Delete(int id)
    {
        DeleteOrder(id);
    }
}

