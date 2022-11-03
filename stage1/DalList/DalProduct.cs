/// <summary>
/// 
/// </summary>

namespace Dal;

public struct DalProduct
{
    public static int CreateProduct(IProduct newProduct)
    {
        DataSource.productList[DataSource.Config.productIdx++] = newProduct;
        return newProduct.ID;
    }

    public static IProduct ReadProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.ProductIdx; i++)
        {
            if (DataSource.Config.productList[i].ID == id)
            {
                return DataSource.Config.productList[i];
            }
        }
        throw new Exception("The product was not found in the list");
    }

    public static IProduct[] ReadProduct()
    {
        IProduct[] newProductList = new IProduct[DataSource.Config.productIdx];
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            newProductList[i] = DataSource.Config.productList[i];
        }
        return newProductList;
    }

    public static void DeleteProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx - 1; i++)
        {
            if (DataSource.Config.productList[i].ID == id)
            {
                DataSource.Config.productList[i] = DataSource.Config.productList[DataSource.Config.productIdx];
                DataSource.Config.productIdx--;
                return;
            }
        }
        if (DataSource.Config.productList[DataSource.Config.productIdx] == id)
        {
            DataSource.Config.productIdx--;
            return;
        }
        throw new Exception("The product was not found in the list");
    }

    public static void UpDateProduct(IProduct UpProduct)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.Config.productList[i].ID == UpProduct.ID)
            {
                DataSource.Config.productList[i] = UpProduct;
                return;
            }
        }
        throw new Exception("The product was not found in the list");
    }
}

