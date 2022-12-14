/// <summary>
/// An entity that defines an order 
/// and holds the details of the customer that ordered 
/// and the details of the order
/// </summary>

namespace DO;

    public struct Order
    {

    public int ID { get; set; }
    public string  CustomerName { get; set; }
    public string  CustomerEmail { get; set; }
    public string  CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public override string ToString() => $@"
        ID = {ID}, 
        CustomerName = {CustomerName}
        CustomerEmail = {CustomerEmail}
        CustomerAdress = {CustomerAddress}
        OrderDate = {OrderDate}
        ShipDate = {ShipDate}
        DeliveryDate = {DeliveryDate}";  	
    }
