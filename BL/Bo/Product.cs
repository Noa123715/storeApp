/// <summary>
/// Product class contains the prodacts properties and toString method.
/// </summary>

namespace BO;

public class Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public eCategories Category { get; set; }
    public int InStock { get; set; }

    //overriding ToString method enables printing the Product properties.
    public override string ToString() => $@"Product ID: {ID}, Name: {Name},Price: {Price}, Category: {Category},  Instock: {InStock}";
}