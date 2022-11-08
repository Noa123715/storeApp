/// <summary>
/// CRUD operations department:
/// for adding a new product,
/// reading the existing product,
/// updating product and deletions.
/// </summary>
using Dal.DO;
namespace DalList;

public struct DalProduct : ICrud<IProduct>
{
    public static int CreateProduct(IProduct newProduct)
    {
        DataSource.productList[DataSource.Config.productIdx++] = newProduct;
        return newProduct.ID;
    }

    public static IProduct ReadProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.productList[i].ID == id)
            {
                return DataSource.productList[i];
            }
        }
        throw new System.Exception("The product was not found in the list");
    }

    public static IProduct[] ReadProduct()
    {
        IProduct[] newProductList = new IProduct[DataSource.Config.productIdx];
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            newProductList[i] = DataSource.productList[i];
        }
        return newProductList;
    }

    public static void DeleteProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx - 1; i++)
        {
            if (DataSource.productList[i].ID == id)
            {
                DataSource.productList[i] = DataSource.productList[DataSource.Config.productIdx];
                DataSource.Config.productIdx--;
                return;
            }
        }
        if (DataSource.productList[DataSource.Config.productIdx].ID == id)
        {
            DataSource.Config.productIdx--;
            return;
        }
        throw new System.Exception("The product was not found in the list");
    }

    public static void UpDateProduct(IProduct UpProduct)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.productList[i].ID == UpProduct.ID)
            {
                DataSource.productList[i] = UpProduct;
                return;
            }
        }
        throw new System.Exception("The product was not found in the list");
    }

    public void Create(IProduct newProduct)
    {
        try
        {
            int amount, price, option;
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
        catch (Exception error)
        {

            throw new Exception($@"This is the error: {error}");
        }
    }

    public IProduct ReadById(int id)
    {
        IProduct product = new IProduct();
        product = ReadProduct(id);
        return product;
    }

    public IProduct[] ReadAll()
    {
        IProduct[] allProduct = new IProduct[5];
        return allProduct;
    }

    public void Update(IProduct item, int id, int option)
    {

    }

    public void Delete(int id)
    {

    }
}

