/// <summary>
/// CRUD operations department:
/// for adding a new product,
/// reading the existing product,
/// updating product and deletions.
/// </summary>

using Dal.DO;
namespace DalList;

public struct DalProduct
{
    public static int CreateProduct(Product newProduct)
    {
        DataSource.productList.Add(newProduct);
        return newProduct.ID;
    }

    public static Product ReadProduct(int id)
    {
        foreach (Product item in DataSource.productList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        throw new Exception("The product was not found in the list");
    }

    public static List<Product> ReadProduct()
    {
        List<Product> newProductList = new List<Product>();
        newProductList.AddRange(DataSource.productList);
        return newProductList;
    }

    public static void DeleteProduct(int id)
    {
        DataSource.productList.RemoveAll(item => item.ID == id);
        //throw new Exception("The product was not found in the list");
    }

    public static void UpDateProduct(Product UpProduct)
    {
        int index = DataSource.productList.FindIndex(item => item.ID == UpProduct.ID);
        if (index == -1)
        {
            throw new Exception("The product was not found in the list");
        }
    }
}

