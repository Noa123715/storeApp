/// <summary>
/// CRUD operations department:
/// for adding a new order item list,
/// reading the existing order item,
/// updating order item and deletions.
/// </summary>
using Dal.DO;
namespace DalList;

public struct DalOrderItem : ICrud<IOrderItem>
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
        throw new Exception("The orderItem was not found in the list");
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
        throw new Exception("The orderItem was not found in the list");
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
        throw new Exception("The orderitem was not found in the list");
    }

    public void Create(IOrderItem newOrderItem)
    {
        try
        {
            int amount, price;
            Console.WriteLine("Please enter the order item details:");
            newOrderItem.OrderID = DataSource.Config.OrderId;
            newOrderItem.ProductID = DataSource.Config.OrderItemId;
            Console.WriteLine("Please enter the amount of the item: ");
            if (!(int.TryParse(Console.ReadLine(), out amount)))
                throw new Exception("You entered a none valid number");
            newOrderItem.Amount = amount;
            Console.WriteLine("Please enter the item price: ");
            if (!(int.TryParse(Console.ReadLine(), out price)))
                throw new Exception("You entered a none valid number");
            newOrderItem.Price = price;
            int id = CreateOrderItem(newOrderItem);
            Console.WriteLine($@"The new id is: {id}");
        }
        catch (Exception error)
        {
            throw new Exception($@"This is the error: {error}");
        }
    }

    public IOrderItem ReadById(int id)
    {
        IOrderItem orderItem = new IOrderItem();
        orderItem = ReadOrderItem(id);
        return orderItem;
    }

    public IOrderItem[] ReadAll()
    {
        IOrderItem[] allOrderItems = ReadOrderItem();
        return allOrderItems;
    }

    public void Update(IOrderItem orderItem, int id, int option)
    {
        int amount, price;
        orderItem = ReadOrderItem(id);
        //Accepting the user's choice
        switch (option)
        {
            case (int)eUpDateOrderItem.Amount: //update the amount of the order item
                Console.WriteLine("Please enter the new amount to update: ");
                if (!(int.TryParse(Console.ReadLine(), out amount)))
                    throw new Exception("You entered a none valid number");
                orderItem.Amount = amount;
                break;
            case (int)eUpDateOrderItem.Price: //update the price of the order item
                Console.WriteLine("Please enter the new amount to update: ");
                if (!(int.TryParse(Console.ReadLine(), out price)))
                    throw new Exception("You entered a none valid number");
                orderItem.Price = price;
                break;
            default:
                throw new Exception("Worng number!");
        }
        UpDateOrderItem(orderItem);
    }

    public void Delete(int id)
    {
        DeleteOrderItem(id);
    }
}

