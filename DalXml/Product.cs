namespace Dal;
using DalApi;
using System;
using System.Xml.Serialization;
/// <summary>
/// CRUD operations department:
/// for adding a new product,
/// reading the existing product,
/// updating product and deletions.
/// </summary>
public class Product : IProduct
{
    /// <summary>
    /// creates new product.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Create(DO.Product product)
    {
        List<DO.Product> productList = ReadAll().ToList();
        bool idExists;
        int barcode;
        Random rand = new Random();

        do
        {
            idExists = true;
            barcode = (int)rand.NextInt64(100000, 1000000);
            foreach (var p in productList)
                if (p.ID == barcode)
                    idExists = false;
        } while (!idExists);
        product.ID = barcode;

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "ProductList";
        xRoot.IsNullable = true;
        StreamReader reader = new StreamReader(@"..\xml\product.xml");
        XmlSerializer ser = new(typeof(List<DO.Product>), xRoot);
        List<DO.Product>? products = (List<DO.Product>?)ser.Deserialize(reader);
        reader.Close();
        DO.Product prod = products.Where(p => p.ID == product.ID).FirstOrDefault();
        if(!prod.Equals(default(DO.Product)))
            throw new AlreadyExistException();
        products.Add(product);
        StreamWriter w = new StreamWriter(@"..\xml\product.xml");
        ser.Serialize(w, products);
        w.Close();
        return product.ID;
       

    }

    /// <summary>
    /// deletes product according to it's ID.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExistException"></exception>
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new()
        {
            ElementName = "ProductList",
            IsNullable = true
        };       
        XmlSerializer ser = new(typeof(List<DO.Product>), xRoot);
        StreamReader reader = new(@"..\xml\product.xml");
        List<DO.Product>? products = (List<DO.Product>?)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new(@"..\xml\product.xml");
        DO.Product product = products.Where(p => p.ID == id).FirstOrDefault();
        if(product.ID == 0)
            throw new NotExistException();
        products.Remove(product);
        ser.Serialize(writer, products);
        writer.Close();
    }
    /// <summary>
    /// read all products by default or according to certain condition.
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotExistException"></exception>
    public IEnumerable<DO.Product> ReadAll(Func<DO.Product, bool>? condition = null)
    {
        XmlRootAttribute xmlRoot = new()
        {
            ElementName = "ProductList",
            IsNullable = true
        };        
        StreamReader r = new(@"..\xml\product.xml");
        XmlSerializer ser = new(typeof(List<DO.Product>), xmlRoot);
        List<DO.Product>? productList = (List<DO.Product>?)ser.Deserialize(r);        
        r.Close();
        return condition == null ? productList : (productList.Where(condition).ToList() ?? throw new NotExistException());    
}

    /// <summary>
    /// read specific product that implement certain condition.
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public DO.Product Read(Func<DO.Product, bool> condition)
    {
        return ReadAll(condition).First();
    }

    /// <summary>
    /// updates product property
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="NotExistException"></exception>
    public void UpDate(DO.Product product)
    {
        XmlRootAttribute xmlRoot = new()
        {
            ElementName = "ProductList",
            IsNullable = true
        };        
        StreamReader productReader = new(@"..\xml\product.xml");
        XmlSerializer ser = new(typeof(List<DO.Product>), xmlRoot);
        List<DO.Product>? productList = (List<DO.Product>?)ser.Deserialize(productReader);
        productReader.Close();
        DO.Product product1 = productList.Where(productItem => productItem.ID == product.ID).FirstOrDefault();
        if(product1.Equals(default(DO.Product)))
        { 
            throw new NotExistException();
        }
        productList.Remove(product1);
        productList.Add(product);
        StreamWriter pWrite = new(@"..\xml\product.xml");
        ser.Serialize(pWrite, productList);
        pWrite.Close();
    }
}
