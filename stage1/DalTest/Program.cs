// See https://aka.ms/new-console-template for more information

using System;
using System.IO;

namespace Stage0;

partial class program 
{
    enum eOptions
    {
        Exit, Order, OrderItem, Product
    }
    /// אפשר לעשות ENUM של מספרים למרות שהדרישה היא אותיות כפי הנראה

    static void main(string[] arg)
    {
        Console.WriteLine("Please enter a number: \r\n0- to exit\r\n1- to check the Order\r\n2- to check the Order Item\r\n3- to check the Product");
        int exitCode = Console.ReadLine();
        switch (exitCode)
        {
            case eOptions.Exit:
                break;
            case eOptions.Order:
                OrderCRUD();
                break;
            case eOptions.OrderItem:
                OrderItemCRUD();
                break;
            case eOptions.Product:
                ProductCRUD();
                break;
            default:
                throw new System.Exception("Wrong number!");
                break;
        }
    }

    private static void OrderCRUD()
    {
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadLine();
       ///צריך לעשות SWITCH
    }

    private static void OrderItemCRUD()
    {
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadLine();
    }

    private static void ProductCRUD()
    {
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadLine();
    }
}
