// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using DalApi;

void main()
{
    try
    {
        DataSource.s_Initialize();

        //A variable that checks the selection of the user: end the program (by 0) or not.
        bool exitCode = true;
        do
        {
            Console.WriteLine("Please enter a number: \r\n0- to exit\r\n1- to check the Order\r\n2- to check the Order Item\r\n3- to check the Product");
            //Accepting the user's choice
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
                    throw new NonValidNumberException();
            }
        } while (exitCode);
    }
    catch (Exception error)
    {
        Console.WriteLine(error);
    }
}

void OrderCRUD()
{
    try
    {
        Order order = new Order();
        int id, option;
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        //Accepting the user's choice
        char choice = Console.ReadKey().KeyChar;
        switch (choice)
        {
            case 'a': //to add a new order list
                addNewOrder();
                break;
            case 'b': //to read a order by id
                Console.WriteLine("Enter the ID number of the order you want to see: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new NonValidNumberException();
                order = DalOrder.Read(id);
                Console.WriteLine(order);
                break;
            case 'c': //to read all the exsiting orders
                List<Order> allOrders = DalOrder.Read();
                foreach (Order oneOrder in allOrders)
                    Console.WriteLine(oneOrder);
                break;
            case 'd': //to update an exsiting orders
                Console.WriteLine("Please enter the ID number of the order you want to update: ");
                if (!int.TryParse(Console.ReadLine(), out id))
                    throw new NonValidNumberException();
                order = DalOrder.Read(id);
                Console.WriteLine("What do you want to update: \n1- name\n2- mail\n3- adress");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new NonValidNumberException();
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
                        throw new NonValidNumberException();

                }
                DalOrder.UpDate(order);
                break;
            case 'e': //to dalete the order
                Console.WriteLine("Please enter the ID number of the order you want to delete: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new Exception("You entered a none valid number");
                DalOrder.Delete(id);
                break;
            default:
                throw new NonValidNumberException();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

void addNewOrder()
{ //add a new order list - write in a separate function to make the code easier to read
    try
    {
        Order newOrder = new Order();
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
        int id = DalOrder.Create(newOrder);
        Console.WriteLine($@"The new id is: {id}");
    }
    catch (Exception error)
    {

        Console.WriteLine(error);
    }
}

void OrderItemCRUD()
{
    try
    {
        OrderItem orderItem = new OrderItem();
        int id, option, amount, price;
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadKey().KeyChar;
        //Accepting the user's choice
        switch (choice)
        {
            case 'a': //to add a new order item
                addNewOrderItem();
                break;
            case 'b': //to read a order item by id
                Console.WriteLine("Enter the ID number of the order item you want to see: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new NonValidNumberException();
                orderItem = DalOrderItem.Read(id);
                Console.WriteLine(orderItem);
                break;
            case 'c': //to read all the exsiting orders item
                List<OrderItem> allOrderItems = DalOrderItem.ReadAll();
                foreach (OrderItem item in allOrderItems)
                    Console.WriteLine(item);
                break;
            case 'd': //to update an exsiting orders item
                Console.WriteLine("Please enter the ID number of the order item you want to update: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new NonValidNumberException();
                orderItem = DalOrderItem.Read(id);
                Console.WriteLine("What do you want to update: \n1- amount\n2- price");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new NonValidNumberException();
                //Accepting the user's choice
                switch (option)
                {
                    case (int)eUpDateOrderItem.Amount: //update the amount of the order item
                        Console.WriteLine("Please enter the new amount to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out amount)))
                            throw new NonValidNumberException();
                        orderItem.Amount = amount;
                        break;
                    case (int)eUpDateOrderItem.Price: //update the price of the order item
                        Console.WriteLine("Please enter the new amount to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out price)))
                            throw new NonValidNumberException();
                        orderItem.Price = price;
                        break;
                    default:
                        throw new NonValidNumberException();
                }
                DalOrderItem.UpDate(orderItem);
                break;
            case 'e': //to dalete the order item
                Console.WriteLine("");
                Console.WriteLine("Please enter the ID number of the order item you want to delete: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new NonValidNumberException();
                DalOrderItem.Delete(id);
                break;
        }
    }
    catch (Exception error)
    {
        Console.WriteLine(error);
    }
}

void addNewOrderItem()
{ //add a new order item - write in a separate function to make the code easier to read
    int amount, price;
    OrderItem newOrderItem = new OrderItem();
    Console.WriteLine("Please enter the order item details:");
    newOrderItem.OrderID = DataSource.Config.OrderId;
    newOrderItem.ProductID = DataSource.Config.OrderItemId;
    Console.WriteLine("Please enter the amount of the item: ");
    if (!(int.TryParse(Console.ReadLine(), out amount)))
        throw new NonValidNumberException();
    newOrderItem.Amount = amount;
    Console.WriteLine("Please enter the item price: ");
    if (!(int.TryParse(Console.ReadLine(), out price)))
        throw new NonValidNumberException();
    newOrderItem.Price = price;
    int id = DalOrderItem.Create(newOrderItem);
    Console.WriteLine($@"The new id is: {id}");
}

void ProductCRUD()
{
    try
    {
        Product product = new Product();
        int id, option, price, amountInStock, category;
        Console.WriteLine("Please enter a letter: \na- Adding an object to the list\nb- Object display by ID\r\nc- entity list view\nd- object update\ne- Deleting an object from the list");
        char choice = Console.ReadKey().KeyChar;
        //Accepting the user's choice
        switch (choice)
        {
            case 'a': //to add a new product
                addNewProduct();
                break;
            case 'b': //to read a product by id
                Console.WriteLine("Enter the ID number of the product you want to see: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new NonValidNumberException();
                product = DalProduct.Read(id);
                Console.WriteLine(product);
                break;
            case 'c': //to read all the exsiting products
                List<Product> allProduct = DalProduct.Read();
                foreach (Product oneProduct in allProduct)
                    Console.WriteLine(oneProduct);
                break;
            case 'd': //to update an exsiting orders
                Console.WriteLine("Please enter the ID number of the product you want to update: ");
                if (!int.TryParse(Console.ReadLine(), out id))
                    throw new NonValidNumberException();
                product = DalProduct.Read(id);
                Console.WriteLine("What do you want to update: \n1- name\n2- price\n3- amount in stock\n4- category");
                if (!(int.TryParse(Console.ReadLine(), out option)))
                    throw new NonValidNumberException();
                //Accepting the user's choice
                switch (option)
                {
                    case (int)eUpDateProduct.Name: //to update the name of the product
                        Console.WriteLine("Please enter the name to update: ");
                        product.Name = Console.ReadLine();
                        break;
                    case (int)eUpDateProduct.Price: //to update the price of the product
                        Console.WriteLine("Please enter the price to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out price)))
                            throw new NonValidNumberException();
                        product.Price = price;
                        break;
                    case (int)eUpDateProduct.InStock: //to update the amount in stock of the product
                        Console.WriteLine("Please enter the amount in stock to update: ");
                        if (!(int.TryParse(Console.ReadLine(), out amountInStock)))
                            throw new NonValidNumberException();
                        product.InStock = amountInStock;
                        break;
                    case (int)eUpDateProduct.Category: //to update the category of the product
                        Console.WriteLine("Please enter the category to update: \n0- accessories\n1- women\n2- men\n3- children\n4- beauty");
                        if (!(int.TryParse(Console.ReadLine(), out category)))
                            throw new NonValidNumberException();
                        //Accepting the user's choice
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
                                throw new NonValidNumberException();
                        }
                        break;
                    default:
                         throw new NonValidNumberException();

                }
                DalProduct.UpDate(product);
                break;
            case 'e': //to dalete a product
                Console.WriteLine("Please enter the ID number of the product you want to delete: ");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new NonValidNumberException();
                DalProduct.Delete(id);
                break;
            default:
                throw new NonValidNumberException();
        }
    }
    catch (Exception error)
    {
       Console.WriteLine(error);
    }
}

void addNewProduct()
{ //add a new product - write in a separate function to make the code easier to read
    int amount, price, option;
    Product newProduct = new Product();
    Console.WriteLine("Please enter the product item details:");
    //newProduct.ID = DataSource.Config.productIdx;æä àîåø ìäéåú îñôø îæää øõ åìà îñôø àéðã÷ñ
    Console.WriteLine("Please enter the name of the product: ");
    newProduct.Name = Console.ReadLine();
    Console.WriteLine("Please enter the amount in stock of the product: ");
    if (!(int.TryParse(Console.ReadLine(), out amount)))
        throw new NonValidNumberException();
    newProduct.InStock = amount;
    Console.WriteLine("Please enter the product price: ");
    if (!(int.TryParse(Console.ReadLine(), out price)))
         throw new NonValidNumberException();
    newProduct.Price = price;
    Console.WriteLine("Please enter the product's category: \n0- accessories\n1- women\n2- men\n3- children\n4- beauty");
    if (!(int.TryParse(Console.ReadLine(), out option)))
        throw new NonValidNumberException();
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
            throw new NonValidNumberException();
    }
    int id = DalProduct.Create(newProduct);
    Console.WriteLine($@"The new id is: {id}");
}
