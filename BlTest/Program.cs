/// <summary>
/// program module includes the main program for BL layer.
/// </summary>
using BO;
using BlApi;
using BlImplementation;

namespace BlTest;

internal static class Program
{
    //to make all the necessary operations in BL
    static BL Bl = new BL();
    /// <summary>
    /// Main program to check the method in BL
    /// </summary>
    /// <exception cref="BlInValidInputException"></exception>
    static void Main()
    {
        eOptions choice; //to save the user's choice
        do
        {
            Console.WriteLine("please enter your choice:\n" +
                "0- to exit\n" +
                "1- to order\n" +
                "2- to cart\n" +
                "3- to product");
            choice = (eOptions)Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (choice)
                {
                    case eOptions.Exit:
                        break; ;
                    case eOptions.Order:
                        FuncOrder();
                        break;
                    case eOptions.Cart:
                        FuncCart();
                        break;
                    case eOptions.Product:
                        FuncProduct();
                        break;
                    default:
                        //if the input is not a number between 0 and 3
                        throw new BlInValidInputException();
                }
            }
            catch (BlInValidInputException err)
            {
                Console.WriteLine(err.Message);
            }
        } while (choice != 0);
    }


    /// <summary>
    /// A function to handle the ORDER
    /// </summary>
    /// <exception cref="BlNullValueException"></exception>
    /// <exception cref="Exception"></exception>
    public static void FuncOrder()
    {
        int id;
        //to make all the necessary in order
        Order order = new Order();
        OrderTracking orderTracking = new OrderTracking();
        IEnumerable<OrderForList> orderForLists = new List<OrderForList>();
        Console.WriteLine("please enter your choice:\n" +
            "1- to read order list\n" +
            "2- to read order details\n" +
            "3- to update the ship date\n" +
            "4- to update the delivery date\n" +
            "5- to track");
        eOrder eOrderOption = (eOrder)Convert.ToInt32(Console.ReadLine());
        switch (eOrderOption)
        {
            case eOrder.OrderList:
                orderForLists = Bl.Order.ReadOrderList();
                foreach (OrderForList item in orderForLists)
                {
                    Console.WriteLine(item);
                }
                break;
            case eOrder.OrderDetails:
                Console.WriteLine("please enter the id of the order you want to see:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                order = Bl.Order.ReadOrderProperties(id);
                Console.WriteLine(order);
                break;
            case eOrder.UpDateSDate:
                Console.WriteLine("please enter the id of the order you want to update the ship date:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                order = Bl.Order.UpdateOrderSent(id);
                Console.WriteLine(order);
                break;
            case eOrder.UpDateDDate:
                Console.WriteLine("please enter the id of the order you want to update the delivery date:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                order = Bl.Order.UpdateOrderDelivery(id);
                Console.WriteLine(order);
                break;
            case eOrder.Track:
                Console.WriteLine("please enter the id of the order you want to track");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                orderTracking = Bl.Order.TrackOrder(id);
                Console.WriteLine(orderTracking);
                break;
            default:
                //if the input is not a number between 1 and 5
                throw new BlInValidInputException();
        }
    }

    /// <summary>
    /// A function to manage the actions on the cart
    /// </summary>
    /// <exception cref="BlNullValueException"></exception>
    /// <exception cref="BlInValidInputException"></exception>
    public static void FuncCart()
    {
        Cart cart = new Cart();
        Console.WriteLine("please enter your choice:\n" +
            "1- to add a product to the cart\n" +
            "2- to update the amount of the product in stock\n" +
            "3- to confirm\n");
        eCart eCartOption = (eCart)Convert.ToInt32(Console.ReadLine());
        int id, newAmount;
        string? customerName, customerMail, customerAddress;
        switch (eCartOption)
        {
            case eCart.AddProduct:
                Console.WriteLine("please enter the id of the product you want to add to the cart:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                cart = Bl.Cart.AddProductToCart(cart, id);
                Console.WriteLine(cart);
                break;
            case eCart.UpDateAmount:
                Console.WriteLine("please enter the id of the product you want to update the amount:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                Console.WriteLine("please enter the new amount to update:");
                if (!(int.TryParse(Console.ReadLine(), out newAmount)))
                    throw new BlNullValueException();
                cart = Bl.Cart.UpdateProductAmount(cart, id, newAmount);
                Console.WriteLine(cart);
                break;
            case eCart.Confirm:
                //to confirm the order of the products in the cart
                Console.WriteLine("please enter the customer's name:");
                customerName = Console.ReadLine() ?? throw new BlNullValueException();
                Console.WriteLine("please enter the customer's mail:");
                customerMail = Console.ReadLine() ?? throw new BlNullValueException();
                Console.WriteLine("please enter the customer's address:");
                customerAddress = Console.ReadLine() ?? throw new BlNullValueException();
                Bl.Cart.Confirmation(cart);
                break;
            default:
                //if the input is not a number between 1 and 3
                throw new BlInValidInputException();
        }
    }

    /// <summary>
    /// A function to manage the operations on the product
    /// </summary>
    /// <exception cref="BlNullValueException"></exception>
    /// <exception cref="BlInValidInputException"></exception>
    public static void FuncProduct()
    {
        int id, price, inStock;
        Product product = new Product();
        Console.WriteLine("please enter your choice:\n" +
                        "1- to see the product list\n" +
                        "2- to see the properties of a product\n" +
                        "3- to add a product\n" +
                        "4- to delete a product\n" +
                        "5- to update a product");
        eProduct eProductChoice = (eProduct)Convert.ToInt32(Console.ReadLine());
        switch (eProductChoice)
        {
            case eProduct.ReadAll: //read all the properties of all product
                IEnumerable<ProductForList> readProductList = Bl.Product.ReadProductsList();
                //after receiving all the items they are presented to the user 
                foreach (ProductForList productList in readProductList)
                    Console.WriteLine(productList);
                break;
            case eProduct.ReadProperties: //read the properties of a product
                Console.WriteLine("please enter the id of the product you want to see the properties:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                Bl.Product.ReadProductProperties(id);
                break;
            case eProduct.Add: //to add a product to the stock
                Console.WriteLine("please enter the name of the new product:");
                product.Name = Console.ReadLine() ?? throw new BlNullValueException();
                Console.WriteLine("please enter the category of the new product:\n" +
                                   "1- for accessories\n" +
                                   "2- for women\n" +
                                   "3- for men\n" +
                                   "4- for children\n" +
                                   "5- for beauty");
                product.Category = (eCategories)Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("please enter the price of the new product:");
                if (!(int.TryParse(Console.ReadLine(), out price)))
                    throw new BlNullValueException();
                product.Price = price;
                Console.WriteLine("please enter the amount of the new product:");

                if (!(int.TryParse(Console.ReadLine(), out inStock)))
                    throw new BlNullValueException();
                product.InStock = inStock;
                //after the user has entered the data, a new product is updated in stock
                Bl.Product.AddProduct(product);
                break;
            case eProduct.Delete: //to delete a product from the stock
                Console.WriteLine("please enter the id of the product you want to delete:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                Bl.Product.DeleteProduct(id);
                break;
            case eProduct.UpDate: //to update an product in the stock
                Console.WriteLine("please enter the id of the product you want to update:");
                if (!(int.TryParse(Console.ReadLine(), out id)))
                    throw new BlNullValueException();
                product.ID = id;
                Console.WriteLine("please enter the new name of the product:");
                product.Name = Console.ReadLine() ?? throw new BlNullValueException();
                Console.WriteLine("please enter the new category of the product:\n" +
                                   "1- for accessories\n" +
                                   "2- for women\n" +
                                   "3- for men\n" +
                                   "4- for children\n" +
                                   "5- for beauty");
                product.Category = (eCategories)Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("please enter the new price of the product:");
                if (!(int.TryParse(Console.ReadLine(), out price)))
                    throw new BlNullValueException();
                product.Price = price;
                Console.WriteLine("please enter the new amount of the product:");
                if (!(int.TryParse(Console.ReadLine(), out inStock)))
                    throw new BlNullValueException();
                product.InStock = inStock;
                //after the user has entered the data,the product is updated in stock
                Bl.Product.UpdateProduct(product);
                break;
            default:
                //if the input is not a number between 1 and 5
                throw new BlInValidInputException();
        }
    }
}