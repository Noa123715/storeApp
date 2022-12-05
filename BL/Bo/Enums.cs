///<summary>
/// enums module includes enums for BL layer.
/// </summary>

namespace BO;

/// <summary>
/// eOrderStatus enum for 3 statuses of order-status
/// </summary>
public enum eOrderStatus
{
    Ordered,
    Shipped,
    Delivered
}

public enum eCategories
{
    Accessories,
    Women,
    Men,
    Children,
    Beauty
}

public enum eOptions 
{ 
    Exit,
    Order,
    Cart,
    Product
}

public enum eOrder
{
    OrderList,
    OrderDetails,
    UpDateSDate,
    UpDateDDate,
    Track
}

public enum eCart
{
    AddProduct,
    UpDateAmount,
    Confirm
}