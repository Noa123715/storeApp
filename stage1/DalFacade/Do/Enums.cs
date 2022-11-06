/// <summary>
/// structor for enum
/// </summary>

namespace Dal.DO;

public enum eCategories
{
    accessories,
    women,
    men,
    children,
    beauty
}

public enum eOptions
{
    Exit,
    Order,
    OrderItem,
    Product
}

public enum eUpDateOrder 
{ 
    Name = 1,
    Mail,
    Adress
}

public enum eUpDateOrderItem
{ 
    Amount = 1,
    Price
}

public enum eUpDateProduct
{
    Name= 1,
    Price,
    InStock,
    Category
}