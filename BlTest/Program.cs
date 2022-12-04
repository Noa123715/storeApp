using BO;
using BlApi;
using BlImplementation;

namespace BlTest;

internal static class Program
{
    static BL Bl = new BL();
    static void Main()
    {
        try
        {
            eOptions choice;
            do
            {
                Console.WriteLine("please enter your choice:\n" +
                    "0- to exit\n" +
                    "1- to order\n" +
                    "2- to cart\n" +
                    "3- to product");
                choice = (eOptions)Convert.ToInt32(Console.ReadLine());
                switch (choice) 
                {
                    case eOptions.Exit:
                        break;
                    case eOptions.Order:
                        FuncOrder();
                        break;
                    case eOptions.Cart:
                        FuncCart();
                        break;
                    default:
                        throw new Exception("של בחירה לא נכוהנגם כאן יש אקספשיין");
                }
            } while (choice != 0);
        }
        catch(Exception err)
        {
            Console.WriteLine(err.Message);//תשימי כאן אקספשיין
        }
    }

    public static void FuncOrder()
    {
        try
        {
            int id;
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
                    id = Convert.ToInt32(Console.ReadLine()); //למחוק את השורה הזאת
                    //if (!(int.TryParse(Console.ReadLine(), out id)))
                    //throw new מספר לא תקין();כשיהיה אקספשיין...
                    order = Bl.Order.ReadOrderProperties(id);
                    Console.WriteLine(order);
                    break;
                case eOrder.UpDateSDate:
                    Console.WriteLine("please enter the id of the order you want to update the ship date:");
                    //if (!(int.TryParse(Console.ReadLine(), out orderId)))
                    //    throw new מספר לא תקין();כשיהיה אקספשיין...
                    id = Convert.ToInt32(Console.ReadLine()); //למחוק את השורה הזאת
                    order = Bl.Order.UpdateOrderSent(id);
                    Console.WriteLine(order);
                    break;
                case eOrder.UpDateDDate:
                    Console.WriteLine("please enter the id of the order you want to update the delivery date:");
                    //if (!(int.TryParse(Console.ReadLine(), out orderId)))
                    //    throw new מספר לא תקין();כשיהיה אקספשיין...
                    id = Convert.ToInt32(Console.ReadLine());//למחוק את השורה הזאת
                    order = Bl.Order.UpdateOrderDelivery(id);
                    Console.WriteLine(order);
                    break;
                case eOrder.Track:
                    Console.WriteLine("please enter the id of the order you want to track");
                    //if (!(int.TryParse(Console.ReadLine(), out id)))
                    //    throw new כנ"ל..............();
                    id = Convert.ToInt32(Console.ReadLine());//למחוק את השורה הזאת
                    orderTracking = Bl.Order.TrackOrder(id);
                    Console.WriteLine(orderTracking);
                    break;
                default:
                    throw new Exception("גם כאן יש אקספשיין של בחירה לא נכונה");
            }
        }
        catch(Exception err) { Console.WriteLine(err.Message); }//את השורה הזאת צריך למחוק
        //אמור להיות כאן פרוט של תפיסות אקספשיין לפי סוג שגיאה
    }

    public static void FuncCart()
    {
        try
        {
            Cart cart = new Cart();
            Console.WriteLine("please enter your choice:\n" +
                "1- to add a product to the cart\n" +
                "2- to update the amount of the product in stock\n" +
                "3- to confirm\n");
            eCart eCartOption = (eCart)Convert.ToInt32(Console.ReadLine());
            int id, newAmount;
            string customerName, customerMail, customerAddress;
            switch (eCartOption)
            {
                case eCart.AddProduct:
                    Console.WriteLine("please enter the id of the product you want to add to the cart:");
                    //if (!(int.TryParse(Console.ReadLine(), out productID)))
                    //    throw new מספר לא נכון();
                    id = Convert.ToInt32(Console.ReadLine());//למחוק ...
                    cart = Bl.Cart.AddProductToCart(cart, id);
                    break;
                case eCart.UpDateAmount:
                    Console.WriteLine("please enter the id of the product you want to update the amount:");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("please enter the new amount to update:");
                    //if (!(int.TryParse(Console.ReadLine(), out productID)))
                    //    throw new מספר לא נכון();
                    newAmount = Convert.ToInt32(Console.ReadLine());

                    break;

            }
        }
        catch(Exception e) { Console.WriteLine(e.Message); }//את השורה הזאת צריך למחוק
    }
}