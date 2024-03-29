/// <summary>
/// structor for enum
/// </summary>

namespace DO;
/// <summary>
/// ENUM for defining the categories for an product class
/// </summary>
public enum eCategories
{
    Accessories = 1,
    Women,
    Men,
    Children,
    Beauty
}

/// <summary>
/// ENUM to select the entity to test in the main program
/// </summary>
public enum eOptions
{
    Exit,
    Order,
    OrderItem,
    Product
}

/// <summary>
/// ENUM for selecting the part in the Order class to update
/// </summary>
public enum eUpDateOrder
{
    Name = 1,
    Mail,
    Adress
}

/// <summary>
/// ENUM for selecting the part in the Order Item class to update
/// </summary>
public enum eUpDateOrderItem
{
    Amount = 1,
    Price
}

/// <summary>
/// ENUM for selecting the part in the Product class to update
/// </summary>
public enum eUpDateProduct
{
    Name = 1,
    Price,
    InStock,
    Category
}