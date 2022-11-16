/// <summary>
/// An entity that defines an order 
/// and holds the details of the customer that ordered 
/// and the details of the order
/// </summary>

namespace Dal.DO;

    public struct Order
    {
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@"
        ID = {ID}, 
        CustomerName = {CustomerName}
        CustomerEmail = {CustomerEmail}
        CustomerAdress = {CustomerAdress}
        OrderDate = {OrderDate}
        ShipDate = {ShipDate}
        DeliveryDate = {DeliveryDate}";  	
    }
