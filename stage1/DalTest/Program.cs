// See https://aka.ms/new-console-template for more information
using DalList;
using Dal.DO;

try
{
    IOrderItem orderItem = new IOrderItem();
    IProduct product = new IProduct();
    Console.WriteLine("Please enter a number: \r\n0- to exit\r\n1- to check the Order\r\n2- to check the Order Item\r\n3- to check the Product");
    int exitCode = Convert.ToInt32(Console.ReadLine());
    switch (exitCode)
    {
        case (int)eOptions.Order:
            OrderCRUD();
            break;
        case (int)eOptions.OrderItem:
            OrderItemCRUD(orderItem);
            break;
        case (int)eOptions.Product:
            ProductCRUD(product);
            break;
        default:
            throw new Exception("Wrong number!");
    }
}
catch (Exception error)
{
    Console.WriteLine(error);
}

void OrderCRUD()
{
    try
    {
        IOrder order = new IOrder();
        int id, option;
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadKey().KeyChar;
        switch (choice)
        {
            case 'a':
                addNewOrder();
                break;
            case 'b':
                Console.WriteLine("Enter the ID number of the order you want to see: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                order = DalOrder.ReadOrder(id);
                Console.WriteLine(order);
                break;
            case 'c':
                IOrder[] orders =  DalOrder.ReadOrder();
                foreach(IOrder oneOrder in orders)
                    Console.WriteLine(oneOrder);
                break;
            case 'd':
                Console.WriteLine("Please enter the ID number of the order you want to update: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                order = DalOrder.ReadOrder(id);
                Console.WriteLine("What do you want to update: \n1- name\n2- mail\n3- adress");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new Exception("You entered a none valid number");
                switch (option) 
                {
                    case (int)eUpDate.Name:
                        Console.WriteLine("Please enter your name: ");
                        order.CustomerName = Console.ReadLine();
                        break;
                    case (int)eUpDate.Mail:
                        Console.WriteLine("Please enter your mail: ");
                        order.CustomerEmail = Console.ReadLine();
                        break;
                    case (int)eUpDate.Adress:
                        Console.WriteLine("Please enter your adress: ");
                        order.CustomerAdress = Console.ReadLine();
                        break;
                    default:
                        throw new Exception("Wrong number!");

                }
                DalOrder.UpDateOrder(order);
                break;
            case 'e':
                Console.WriteLine("Please enter the ID number of the order you want to delete: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                DalOrder.DeleteOrder(id);
                break;
            default:
                throw new Exception("Your choice is wrong");
        }
    }
    catch (Exception error)
    {

        throw new Exception($@"This is the error: {error}");
    }
}

IOrder addNewOrder()
{
    try
    {
        IOrder newOrder = new IOrder();
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
        if (DateTime.TryParse(Console.ReadLine(), out orderDate))
            newOrder.OrderDate = orderDate;
        else
            throw new Exception("The date format does not match the value");
        int id = DalOrder.CreateOrder(newOrder);
        Console.WriteLine($@"The new id is: {id}");
        return newOrder;
    }
    catch (Exception error)
    {

        throw new Exception($@"This is the error: {error}");
    }
}

void OrderItemCRUD(IOrderItem orderItem)
{
    Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
    char choice = Console.ReadKey().KeyChar;
}

void ProductCRUD(IProduct product)
{
    Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
    char choice = Console.ReadKey().KeyChar;
}

