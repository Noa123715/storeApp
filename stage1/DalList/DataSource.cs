

/// <summary>
/// 
/// </summary>

namespace Dal;

    internal static class DataSource
    {
    const int numOfproducts = 50;
    const int numOfOrders = 100;
    const int numOfOrderItems = 200;
    public static IProduct[] productList = new IProduct[numOfProducts];
    public static IOrder[] orderList = new IOrder[numOfOrders];
    public static IOrderItem[] orderItemList = new IOrderItem[numOfOrderItems];
    static DataSource() { s_Initialize(); }
    private static void s_Initialize()
    {
        initProductData();
        InitOrderData();
        InitOrderItemData();
    }
    public static class Config
    {
        public static int productIdx = 0;
        public static int orderItemIdx = 0;
        public static int orderIdx = 0;

        private static int orderItemId = 1;
        public static int OrderItemId { get { return orderItemId++; } }
        private static int orderId = 1;
        public static int OrderId { get { return orderId++; } }
     
     

    }
    private static void initProductData()
    {
        bool exists;
        int barcode;
        Random rand = new Random();
        int price;
        int instock;

        (string, eCategories)[] productNames = new (string, eCategories)[10]
           {("necklace", eCategories.accessories ), ("Classic dress - Fendi", eCategories.women), ("pink scarf - Hermes", eCategories.accessories),
            ("elegant pants- ferragamo", eCategories.men), ("Three piece suit- Disel", eCategories.children), ("boots- Gucci", eCategories.women),
            ("perfume- Chanel", eCategories.beauty), ("coat- Moncler", eCategories.women), ("elegant suit - Hermes", eCategories.women), ("bag Louis Vuitton", eCategories.accessories)};

        for(int i=0; i < 10; i++)
        {
            IProduct product = new DalProduct();
            do
            {
                exists = false;
               
                barcode = rand.Next(100000, 10000000);
                for (int j = 0; j < Config.productIdx; j++)
                {
                    if (productList[j] = barcode)
                        exists = true
                }
            } while (exists);
            product.ID = barcode;
            (product.Name, product.Category) = productNames[i];
            price = rand.Next(1000, 30000);
            product.Price = price;
            if (i = 0)
                product.inStock = 0;
            else
            {
                instock = rand.Next(0, 1000);
                product.inStock = instock;
            }
            productList[Config.productIdx++] = product;










        }
      
    }


    private static void initOrdersData()
    {
        Random random = new Random();
        string[] customerNames = { "Donald Trump", "Bill Gates ", "Elon Musk", "Jeff Bezos", "Mark Zuckerberg", "undro macclum", "Hadar Muchtar", "Binyamin Netanyahu", "Lis Taras", "Queen Elizabeth", "Magen Merkel", "kate Middleton", "Ivanka trump", "Dan Gertler", "Liora Ofer", "Sari Aricsone", "Marev Michaeli", "Itamar Ben Gvir", "poor Benet", "Eisenkot the dreamer" };
        string[] customerEmails = { "DonaldTrump@gmail.com", "billgates@gmail.com", "Elonmusk@gmail.com", "jeffbezos@gmail.com", "markzuckerberg@gmail.com", "undromacclum@gmail.com", "hadarmuchtar@gmail.com", "binyaminnetanyahu@gmail.com", "listaras@gmail.com", "queenelizabeth@gmail.com", "magenmerkel@gmail.com", "katemiddleton@gmail.com", "ivankatrump@gmail.com", "dangertler@gmail.com", "Liorafer@gmail.com", "sariaricsone@gmail.com", "marevmichaeli@gmail.com", "itamarbengvir@gmail.com", "poorbenet@gmail.com", "thedreamer@gmail.com" };
        string[] customerAddresses = { "Seychelles", "Savion", "Bakingham", "Balfur", "Prison", "Ramla Lod market", "Dubai", "Homeless (address not available)", "Assisted living", "Hostel", "Ein Kerem, 7th floor"ת"Seychelles", "Savion", "Bakingham", "Balfur", "Neve Tirtza Prison", "Ramla Lod market", "Dubai", "Homeless (address not available)", "Assisted living", "Hostel", "Ein Kerem, 7th floor" };
        
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.ID = Config.OrderId;
            order.CustomerName = customerNames[i];
            order.CustomerEmail = customerEmails[i];
            order.CustomerAddress = customerAddresses[i];

            
            DateTime startDate = new DateTime(2022, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            order.OrderDate = startDate.AddDays(randome.Next(range));


            int dateShipExsist = random.Next(0,5);
            if (dateShipExsist > 0)
            {
                TimeSpan spanShipDays = TimeSpan.FromDays(5);
                order.ShipDate = order.OrderDate + spanShipDays;
                int dateDeliveryExsist = random.Next(0, 5);
                if (dateDeliveryExsist > 1)
                {
                    TimeSpan spanDeliveryDays = TimeSpan.FromDays(10);
                    order.DeliveryDate = order.ShipDate + spanShipDelivery;
                }
                else
                    order.DeliveryDate = DateTime.MinValue;
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }

            orderList[Config.orderIdx++] = order;
        }
    }
    private static void initOrderItemData()
    {
        for (int i = 0; i < Config.orderIdx; i++)
        {


        }

    }
}
}

}

/*
namespace DalList;
using Dal.DO;

/// <summary>
/// handling the store's data source
/// </summary>
public static class DataSource

{
    internal static readonly Random Randomize = new Random();
    internal const int numOfProducts = 50;
    internal const int numOfOrders = 100;
    internal const int numOfOrderItems = 200;

    public static Product[] productList = new Product[numOfProducts];
    public static Order[] orderList = new Order[numOfOrders];
    public static OrderItem[] orderItemList = new OrderItem[numOfOrderItems];
    static DataSource() { s_Initialize(); }

    //initializing our info
    private static void s_Initialize()
    {
        InitProductArray();
        InitOrderArray();
        InitOrderItemArray();
    }

    //handing the ids and indexes of our structs
    public static class Config
    {
        public static int productIdx = 0;
        public static int orderItemIdx = 0;
        public static int orderIdx = 0;

        private static int orderItemId = 1;
        public static int OrderItemId { get { return orderItemId++; } }
        private static int orderId = 1;
        public static int OrderId { get { return orderId++; } }

    }


    


    //initializes the order-items info
    private static void InitOrderItemArray()
    {
        for (int i = 0; i < Config.orderIdx; i++) //goes over all orders and enters items to each order
        {
            int num = (int)Randomize.NextInt64(1, 5); //1-4
            for (int j = 0; j < num; j++)
            {
                OrderItem oi = new OrderItem();
                oi.OrderId = orderList[i].ID;
                int idx = (int)Randomize.NextInt64(0, Config.productIdx);
                oi.ProductId = productList[idx].ID;
                int amount = (int)Randomize.NextInt64(1, 10); //1-9
                if (productList[idx].InStock >= amount)
                    oi.Amount = amount;
                else
                    oi.Amount = productList[idx].InStock;
                productList[idx].InStock -= oi.Amount;
                oi.Price = productList[idx].Price;
                orderItemList[Config.orderItemIdx++] = oi;
            }

        }
    }


    //initializes the order info
    private static void InitOrderArray()
    {
        string[] customerNames = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
        string[] customerEmails = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
        string[] customerAddresses = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };

        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.ID = Config.OrderId;
            order.CustomerName = customerNames[i];
            order.CustomerEmail = customerEmails[i];
            order.CustomerAddress = customerAddresses[i];

            //randomizes a date from 01/01/2010
            Random ran = new Random();
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            order.OrderDate = start.AddDays(ran.Next(range));


            int dateShipExsist = (int)Randomize.NextInt64(0, 5);
            if (dateShipExsist > 0)
            {
                TimeSpan spanOrderShip = TimeSpan.FromDays(5);
                order.ShipDate = order.OrderDate + spanOrderShip;
                int dateDeliveryExsist = (int)Randomize.NextInt64(0, 5);
                if (dateDeliveryExsist > 0)
                {
                    TimeSpan spanShipDelivery = TimeSpan.FromDays(30);
                    order.DeliveryDate = order.ShipDate + spanShipDelivery;
                }
                else
                    order.DeliveryDate = DateTime.MinValue;
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }

            orderList[Config.orderIdx++] = order;
        }
    }
}



 */
