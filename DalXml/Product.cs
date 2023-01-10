namespace Dal;
using DalApi;
using DO;
using System;
using System.Xml.Serialization;

internal class Product : IProduct
{
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
           foreach(var p in productList)
                if (p.ID == barcode)
                    idExists = false;
        } while (!idExists);
        product.ID = barcode;
        
        productList.Add(product);
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>));
        StreamWriter w = new StreamWriter("../../xml/product.xml");
        ser.Serialize(w, productList);
        w.Close();
        return product.ID;
       

    }

    public void Delete(int id)
    {
        List<DO.Product> productList = ReadAll().ToList();
        bool deleted = productList.Remove(productList.Find(p => p.ID == id));
        if (!deleted)
            throw new NotExistException();
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>));
        StreamWriter write = new StreamWriter("../../xml/product.xml");
        ser.Serialize(write, productList);
        write.Close();
        
    }

    public DO.Product Read(int id)
    {
        List<DO.Product> productList = new List<DO.Product>();
        StreamReader reader = new("../../xml/product.xml");
        XmlSerializer ser = new(typeof(List<DO.Product>));
       
        productList = (List<DO.Product>)ser.Deserialize(reader);
    
        reader.Close();
        int index= productList.FindIndex(p=>p.ID==id);
        if(index==-1 ) throw new NotExistException();
        return productList[index];
    }

    public IEnumerable<DO.Product> ReadAll(Func<DO.Product, bool>? condition = null)
    {
        List<DO.Product> ? productList = new List<DO.Product>();
        StreamReader r = new("../../xml/Product.xml");
        XmlSerializer ser = new(typeof(List<Product>));
        productList = (List<DO.Product>)ser?.Deserialize(r);
        r.Close();
        return condition == null ? productList : (productList.Where(condition).ToList() ?? throw new NotExistException());

    
}

    public DO.Product ReadByCondition(Func<DO.Product, bool> condition)
    {
        return ReadAll(condition).First();
    }

    public void UpDate(DO.Product product)
    {
        List<DO.Product> productList = ReadAll().ToList();
        productList[productList.FindIndex(p => p.ID == product.ID)] = product;
        int idx = productList.FindIndex(p => p.ID == product.ID);
        if (idx <0)
            throw new NotExistException();
        product.Name = product.Name == null ? productList[idx].Name : product.Name;
        product.InStock = product.InStock == null ? productList[idx].InStock : product.InStock;
        product.Price = product.Price == null ? productList[idx].Price : product.Price;
        product.Category = product.Category == null ? productList[idx].Category : product.Category;
        productList[idx] = product;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>));
        StreamWriter w = new StreamWriter("../../xml/Product.xml");
        ser.Serialize(w, productList);
        w.Close();
       
    }
}
