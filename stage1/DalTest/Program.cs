// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Specialized;
using System.IO;
using DalList;
using Dal.DO;

namespace Stage1;

partial class program 
{
    static void main(string[] arg)
    {
        IOrder order = new IOrder();
        IOrderItem orderItem = new IOrderItem();
        IProduct product = new IProduct();
        Console.WriteLine("Please enter a number: \r\n0- to exit\r\n1- to check the Order\r\n2- to check the Order Item\r\n3- to check the Product");
        int exitCode = Console.ReadLine();
        switch (exitCode)
        {
            case eOptions.Exit:
                break;
            case eOptions.Order:
                OrderCRUD(order);
                break;
            case eOptions.OrderItem:
                OrderItemCRUD(orderItem);
                break;
            case eOptions.Product:
                ProductCRUD(product);
                break;
            default:
                throw new Exception("Wrong number!");
        }
    }

    private static void OrderCRUD(IOrder order)
    {
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadLine();
        switch (choice)
        {
            case 'a':
                Console.WriteLine("Please enter the order details: \nId, Name, Mail, Adress, Date Order");
                int id = Convert.ToInt32(Console.ReadLine());
                string name = Console.ReadLine();
                string mail = Console.ReadLine();
                string adress = Console.ReadLine();
                DateTime dateOrder = Console.ReadLine();
            default:
                break;
        }
    }

    private static void OrderItemCRUD(IOrderItem orderItem)
    {
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Console.ReadLine();
    }

    private static void ProductCRUD(IProduct product)
    {
        Console.WriteLine("Please enter a letter: \r\na- Adding an object to the list\r\nb- Object display by ID\r\nc- entity list view\r\nd- object update\r\ne- Deleting an object from the list");
        char choice = Convert.ToInt32(Console.ReadLine());
    }
}
