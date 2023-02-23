/// <summary>
/// CRUD operations department:
/// for adding a new product,
/// reading the existing product,
/// updating product and deletions.
/// </summary>

using DO;
using DalApi;
namespace Dal;

public struct DalProduct : IProduct
{
    public int Create(Product newProduct)
    {
        int index = DataSource.orderList.FindIndex(item => item.ID == newProduct.ID);
        if (index != -1)
            throw new AlreadyExistException();
        DataSource.productList.Add(newProduct);
        return newProduct.ID;
    }

    public Product Read(int id)
    {
        foreach (Product item in DataSource.productList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        throw new NotExistException();
    }

    public Product ReadByCondition(Func<Product, bool> condition)
    {
        return DataSource.productList.Where(condition).ToList()[0];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotExistException"></exception>
    /// 
    public IEnumerable<Product> ReadAll(Func<Product, bool>? condition = null)
    {
        if (condition is null)
            return DataSource.productList ?? throw new NotExistException();

        return DataSource.productList.Where(condition).ToList() ?? throw new NotExistException();
    }

    public void Delete(int id)
    {
        int index = DataSource.productList.FindIndex(item => item.ID == id);
        if (index == -1)
        {
            throw new NotExistException();
        }
        DataSource.productList.RemoveAt(index);
    }

    public void UpDate(Product UpProduct)
    {
        int index = DataSource.productList.FindIndex(item => item.ID == UpProduct.ID);
        if (index == -1)
        {
            throw new NotExistException();
        }
        DataSource.productList[index] = UpProduct;
    }
}