/// <summary>
/// <see cref="DataSource"/>
///  Static class for randomly initializing datasets 
/// </summary>
using Dal.DO;
namespace DalList;

public static class DataSource
{
    // datasource members- arrays of limited size for productList, arrays.
   
    public static List<Product> productList = new List<Product>();
    public static List<Order> orderList = new List<Order>();
    public static List<OrderItem> orderItemList = new List<OrderItem>();

    // ctor
    static DataSource() { s_Initialize(); }

    //Main initialization function,
    //call to initialization functions of each data set individually
    public static void s_Initialize()
    {
        initProductData();
        initOrdersData();
        initOrderItemData();
    }

    // Config- nested class, Holds the indexes from which the array is empty- for each array and the IDS
    public static class Config
    {
        

        private static int orderItemId = 1;
        public static int OrderItemId { get { return orderItemId++; } }
        private static int orderId = 1;
        public static int OrderId { get { return orderId++; } }
    }

    //initProductData function: Randomly initializes the array of products in the first ten places.
    private static void initProductData()
    {
        bool exists;
        int barcode;
        Random rand = new Random();
        int price;
        int instock;

        // Name and category array
        (string, eCategories)[] productNames = new (string, eCategories)[10]
           {("necklace", eCategories.accessories ), ("Classic dress - Fendi", eCategories.women), ("pink scarf - Hermes", eCategories.accessories),
            ("elegant pants- ferragamo", eCategories.men), ("Three piece suit- Disel", eCategories.children), ("boots- Gucci", eCategories.women),
            ("perfume- Chanel", eCategories.beauty), ("coat- Moncler", eCategories.women), ("elegant suit - Hermes", eCategories.women), ("bag Louis Vuitton", eCategories.accessories)};


        for (int i = 0; i < 10; i++)
        {
            Product product = new Product();
            do
            // Generates a random barcode and makes sure it doesn't already exist.
            {
                exists = false;

                barcode = rand.Next(100000, 10000000);
                foreach(var prod in productList)
                {
                    if(prod.ID== barcode) 
                    { 
                        exists=true;
                        break;
                    }
                }

              
            } while (exists);
            product.ID = barcode;


            (product.Name, product.Category) = productNames[i];
            price = rand.Next(1000, 30000);
            product.Price = price;

            // To make sure that five percent of the products are out of stock.
            if (i == 0)
                product.InStock = 0;
            else
            {
                instock = rand.Next(0, 1000);
                product.InStock = instock;
            }
            productList.Add(product);
        }
    }

    // initOrdersData function: 
    // Randomly initializes the array of orders in the first twenty places.
    private static void initOrdersData()
    {
        Random random = new Random();

        // arrays of customer details.
        string[] customerNames = { "Donald Trump", "Bill Gates ", "Elon Musk", "Jeff Bezos", "Mark Zuckerberg", "undro macclum", "Hadar Muchtar", "Binyamin Netanyahu", "Lis Taras", "Queen Elizabeth", "Magen Merkel", "kate Middleton", "Ivanka trump", "Dan Gertler", "Liora Ofer", "Sari Aricsone", "Marev Michaeli", "Itamar Ben Gvir", "poor Benet", "Eisenkot the dreamer" };
        string[] customerEmails = { "DonaldTrump@gmail.com", "billgates@gmail.com", "Elonmusk@gmail.com", "jeffbezos@gmail.com", "markzuckerberg@gmail.com", "undromacclum@gmail.com", "hadarmuchtar@gmail.com", "binyaminnetanyahu@gmail.com", "listaras@gmail.com", "queenelizabeth@gmail.com", "magenmerkel@gmail.com", "katemiddleton@gmail.com", "ivankatrump@gmail.com", "dangertler@gmail.com", "Liorafer@gmail.com", "sariaricsone@gmail.com", "marevmichaeli@gmail.com", "itamarbengvir@gmail.com", "poorbenet@gmail.com", "thedreamer@gmail.com" };
        string[] customerAddresses = { "Seychelles", "Savion", "Bakingham", "Balfur", "Prison", "Ramla Lod market", "Dubai", "Homeless (address not available)", "Assisted living", "Hostel", "Ein Kerem, 7th floor", "Seychelles", "Savion", "Bakingham", "Balfur", "Neve Tirtza Prison", "Ramla Lod market", "Dubai", "Homeless (address not available)", "Assisted living", "Hostel", "Ein Kerem, 7th floor" };


        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.ID = Config.OrderId;
            order.CustomerName = customerNames[i];
            order.CustomerEmail = customerEmails[i];
            order.CustomerAdress = customerAddresses[i];

            //Random date between the opening of the store (1/1/2022) and now.
            DateTime startDate = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            order.OrderDate = startDate.AddDays(random.Next(range));

            //A random number between 1-5 so that statistically 80 percent of the orders
            //will have a delivery date as required
            int dateShipExsist = random.Next(0, 5);
            if (dateShipExsist > 0)
            {
                TimeSpan spanShipDays = TimeSpan.FromDays(5);
                order.ShipDate = order.OrderDate + spanShipDays;

                //Random number as above to ensure that 60 percent of the orders
                //will have a delivery date
                int dateDeliveryExsist = random.Next(0, 5);
                if (dateDeliveryExsist > 1)
                {
                    TimeSpan spanDeliveryDays = TimeSpan.FromDays(10);
                    order.DeliveryDate = order.ShipDate + spanDeliveryDays;
                }
                else
                    order.DeliveryDate = DateTime.MinValue;
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }

            orderList.Add(order);
        }
    }

    //initOrderItemData function: Randomly initializes the array of OrderItems
    //in the first forty places.

    private static void initOrderItemData()
    {
        foreach(var order in orderList)
        {
            bool[] exists = new bool[productList.Count];
            int randomProduct;
            Random random = new Random();
            int itemsInOrder = random.Next(1, 5);

            for (int j = 0; j < itemsInOrder; j++)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.OrderID = Config.OrderItemId;

                // Random product according to its index in the product list and verification
                // that it does not already exist in the order.
                //Then, creating a flag that the product already exists on this order.
                do
                    randomProduct = random.Next(0,productList.Count);
                while (exists[randomProduct] != false);
                exists[randomProduct] = true;

                orderItem.ProductID = productList[randomProduct].ID;
                
                int randomAmount = (productList[randomProduct].InStock != 0)? random.Next(1, productList[randomProduct].InStock + 1):0;
                orderItem.Amount = randomAmount;
                Product prod = productList[randomProduct];
                prod.InStock -= randomAmount;
                productList[randomProduct] = prod;
                orderItem.Price = productList[randomProduct].Price;
                orderItem.OrderID = order.ID;
                orderItemList.Add(orderItem);
            }
        }
    }
}

   
