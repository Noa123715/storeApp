// See https://aka.ms/new-console-template for more information
using DalList;
using Dal.DO;

try
{
    bool exitCode = true;
    do
    {
        Console.WriteLine("Please enter a number: \r\n0- to exit\r\n1- to check the Order\r\n2- to check the Order Item\r\n3- to check the Product");
        int option = Convert.ToInt32(Console.ReadLine());
        switch (option)
        {
            case (int)eOptions.Exit:
                exitCode = false;
                break;
            case (int)eOptions.Order:
                OrderCRUD();
                break;
            case (int)eOptions.OrderItem:
                OrderItemCRUD();
                break;
            case (int)eOptions.Product:
                ProductCRUD();
                break;
            default:
                throw new Exception("Wrong number!");
        }
    } while (exitCode);
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
                IOrder[] allOrders =  DalOrder.ReadOrder();
                foreach(IOrder oneOrder in allOrders)
                    Console.WriteLine(oneOrder);
                break;
            case 'd':
                Console.WriteLine("Please enter the ID number of the order you want to update: ");
                if (!int.TryParse(Console.ReadLine(), out id))
                    throw new Exception("You entered a none valid number");
                order = DalOrder.ReadOrder(id);
                Console.WriteLine("What do you want to update: \n1- name\n2- mail\n3- adress");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new Exception("You entered a none valid number");
                switch (option) 
                {
                    case (int)eUpDateOrder.Name:
                        Console.WriteLine("Please enter the name to update: ");
                        order.CustomerName = Console.ReadLine();
                        break;
                    case (int)eUpDateOrder.Mail:
                        Console.WriteLine("Please enter the mail to update: ");
                        order.CustomerEmail = Console.ReadLine();
                        break;
                    case (int)eUpDateOrder.Adress:
                        Console.WriteLine("Please enter the adress to update: ");
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

void addNewOrder()
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
        if (!DateTime.TryParse(Console.ReadLine(), out orderDate))
           throw new Exception("The date format does not match the value");
        newOrder.OrderDate = orderDate;
        int id = DalOrder.CreateOrder(newOrder);
        Console.WriteLine($@"The new id is: {id}");
    }
    catch (Exception error)
    {

        throw new Exception($@"This is the error: {error}");
    }
}

void OrderItemCRUD()
{
    try
    {
        IOrderItem orderItem = new IOrderItem();
        int id, option, amount, price;
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadKey().KeyChar;
        switch (choice)
        {
            case 'a':
                addNewOrderItem();
                break;
            case 'b':
                Console.WriteLine("Enter the ID number of the order item you want to see: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                orderItem = DalOrderItem.ReadOrderItem(id);
                Console.WriteLine(orderItem);
                break;
            case 'c':
                IOrderItem[] allOrderItems = DalOrderItem.ReadOrderItem();
                foreach (IOrderItem item in allOrderItems)
                    Console.WriteLine(item);
                break;
            case 'd':
                Console.WriteLine("Please enter the ID number of the order item you want to update: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                orderItem = DalOrderItem.ReadOrderItem(id);
                Console.WriteLine("What do you want to update: \n1- amount\n2- price");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new Exception("You entered a none valid number");
                switch (option) 
                {
                    case (int)eUpDateOrderItem.Amount:
                        Console.WriteLine("Please enter the new amount to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out amount)))
                            throw new Exception("You entered a none valid number");
                        orderItem.Amount = amount;
                        break;
                    case (int)eUpDateOrderItem.Price:
                        Console.WriteLine("Please enter the new amount to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out price)))
                            throw new Exception("You entered a none valid number");
                        orderItem.Price = price;
                        break;
                    default:
                        throw new Exception("Worng number!");
                }
                DalOrderItem.UpDateOrderItem(orderItem);
                break;
            case 'e':
                Console.WriteLine("");
                Console.WriteLine("Please enter the ID number of the order item you want to delete: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                DalOrderItem.DeleteOrderItem(id);
                break;
        }
    }
    catch (Exception error)
    {
        throw new Exception($@"This is the error: {error}");
    }
}

void addNewOrderItem()
{
    int amount, price;
    IOrderItem newOrderItem = new IOrderItem();
    Console.WriteLine("Please enter the order item details:");
    //newOrderItem.OrderID איזה מזהה לשים פה של הפריטים או המוצרים?
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
    int id = DalOrderItem.CreateOrderItem(newOrderItem);
    Console.WriteLine($@"The new id is: {id}");
}

void ProductCRUD()
{
    try
    {
        IProduct product = new IProduct();
        int id, option, price, amountInStock, category;
        Console.WriteLine("Please enter a letter: \na- Adding an object to the list\nb- Object display by ID\r\nc- entity list view\nd- object update\ne- Deleting an object from the list");
        char choice = Console.ReadKey().KeyChar;
        switch (choice)
        {
            case 'a':
                addNewProduct();
                break;
            case 'b':
                Console.WriteLine("Enter the ID number of the product you want to see: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                product = DalProduct.ReadProduct(id);
                Console.WriteLine(product);
                break;
            case 'c':
                IProduct[] allProduct = DalProduct.ReadProduct();
                foreach (IProduct oneProduct in allProduct)
                    Console.WriteLine(oneProduct);
                break;
            case 'd':
                Console.WriteLine("Please enter the ID number of the product you want to update: ");
                if (!int.TryParse(Console.ReadLine(), out id))
                    throw new Exception("You entered a none valid number");
                product = DalProduct.ReadProduct(id);
                Console.WriteLine("What do you want to update: \n1- name\n2- price\n3- amount in stock\n4- category");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new Exception("You entered a none valid number");
                switch (option)
                {
                    case (int)eUpDateProduct.Name:
                        Console.WriteLine("Please enter the name to update: ");
                        product.Name = Console.ReadLine();
                        break;
                    case (int)eUpDateProduct.Price:
                        Console.WriteLine("Please enter the price to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out price)))
                            throw new Exception("You entered a none valid number");
                        product.Price = price;
                        break;
                    case (int)eUpDateProduct.InStock:
                        Console.WriteLine("Please enter the amount in stock to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out amountInStock)))
                            throw new Exception("You entered a none valid number");
                        product.InStock = amountInStock;
                        break;
                    case (int)eUpDateProduct.Category:
                        Console.WriteLine("Please enter the category to update: \n0- accessories\n1- women\n2- men\n3- children\n4- beauty");
                        if (!(int.TryParse(Console.ReadLine(), out category)))
                            throw new Exception("You entered a none valid number");
                        switch (category)
                        {
                            case (int)eCategories.accessories:
                                product.Category = eCategories.accessories;
                                break;
                            case (int)eCategories.women:
                                product.Category = eCategories.women;
                                break;
                            case (int)eCategories.men:
                                product.Category = eCategories.men;
                                break;
                            case (int)eCategories.children:
                                product.Category = eCategories.children;
                                break;
                            case (int)eCategories.beauty:
                                product.Category = eCategories.beauty;
                                break;
                            default:
                                throw new Exception("Worng number!");
                        }
                        break;
                    default:
                        throw new Exception("Wrong number!");

                }
                DalProduct.UpDateProduct(product);
                break;
            case 'e':
                Console.WriteLine("Please enter the ID number of the product you want to delete: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                DalProduct.DeleteProduct(id);
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

void addNewProduct()
{
    int amount, price, option;
    IProduct newProduct = new IProduct();
    Console.WriteLine("Please enter the product item details:");
    //newProduct.ID = DataSource.Config.productIdx;זה אמור להיות מספר מזהה רץ ולא מספר אינדקס
    Console.WriteLine("Please enter the name of the product: ");
    newProduct.Name = Console.ReadLine();
    Console.WriteLine("Please enter the amount in stock of the product: ");
    if (!(int.TryParse(Console.ReadLine(), out amount)))
        throw new Exception("You entered a none valid number");
    newProduct.InStock = amount;
    Console.WriteLine("Please enter the product price: ");
    if (!(int.TryParse(Console.ReadLine(), out price)))
        throw new Exception("You entered a none valid number");
    newProduct.Price = price;
    Console.WriteLine("Please enter the product's category: \n0- accessories\n1- women\n2- men\n3- children\n4- beauty");
    if (!(int.TryParse(Console.ReadLine(), out option)))
        throw new Exception("You entered a none valid number");
    switch (option)
    {
        case (int)eCategories.accessories:
            newProduct.Category = eCategories.accessories;
            break;
        case (int)eCategories.women:
            newProduct.Category = eCategories.women;
            break;
        case (int)eCategories.men:
            newProduct.Category = eCategories.men;
            break;
        case (int)eCategories.children:
            newProduct.Category = eCategories.children;
            break;
        case (int)eCategories.beauty:
            newProduct.Category = eCategories.beauty;
            break;
        default:
            throw new Exception("Worng number!");
    }
    int id = DalProduct.CreateProduct(newProduct);
    Console.WriteLine($@"The new id is: {id}");
}
