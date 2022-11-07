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
}

