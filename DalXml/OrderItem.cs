namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    private List<DO.OrderItem> orderItemList { get; set; }

    public OrderItem() 
    {

        XElement? oiRoot = XDocument.Load(@"../../xml/orderItem.xml")?.Root;
        DO.OrderItem newOrderItem= new();
        orderItemList = new List<DO.OrderItem>();



        foreach (var xmlOrderItem in oiRoot?.Elements("OrderItem"))
        {
            newOrderItem.ID = Convert.ToInt32(xmlOrderItem.Element("ID")?.Value);
            newOrderItem.ProductID = Convert.ToInt32(xmlOrderItem?.Element("ProductID")?.Value);
            newOrderItem.OrderID = Convert.ToInt32(xmlOrderItem?.Element("OrderID")?.Value);
            newOrderItem.Amount = Convert.ToInt32(xmlOrderItem?.Element("Amount")?.Value);
            newOrderItem.Price = Convert.ToInt32(xmlOrderItem?.Element("Price")?.Value);
            
            orderItemList.Add(newOrderItem);
        }


    }  
    public int Create(DO.OrderItem newOrderItem)
    {
        XElement? rootConfig = XDocument.Load(@"../../xml/config.xml").Root;

        XElement? id = rootConfig?.Element("orderItemID");
        int orderItemID = Convert.ToInt32(id?.Value);
        orderItemID++;
        id.Value = orderItemID.ToString();
        rootConfig?.Save(@"../../xml/config.xml");
        newOrderItem.ID = orderItemID;
        orderItemList.Add(newOrderItem);

        XElement xmlOrderItem = new("OrderItem",
                                new XElement("ID", newOrderItem.ID.ToString()),
                                new XElement("ProductID", newOrderItem.ProductID.ToString()),
                                new XElement("OrderID", newOrderItem.OrderID.ToString()),
                                new XElement("Amount", newOrderItem.Amount),
                                new XElement("Price", newOrderItem.Price),
                               );
        XElement? root = XDocument.Load(@"../../xml/orderItem.xml").Root;
        root?.Add(xmlOrderItem);
        root?.Save(@"../../xml/orderItem.xml");
        return newOrderItem.ID;

    }

    public DO.OrderItem Read(int id)
    {
        foreach (DO.OrderItem orderItem in orderItemList)
        {
            if (orderItem.ID == id)
            {
                return orderItem;
            }
        }
        throw new NotExistException();

    }

    public IEnumerable<DO.OrderItem> ReadAll(Func<DO.OrderItem, bool>? condition = null)
    {
        if (condition is null)
            return orderItemList ?? throw new NotExistException();
        return orderItemList.Where(condition).ToList() ?? throw new NotExistException();
    }

    public DO.OrderItem ReadByCondition(Func<DO.OrderItem, bool> condition)
    {
        return orderItemList.Where(condition).ToList()[0];
    }

    public void UpDate(DO.OrderItem upOrderItem)
    {

        XElement? oiRoot = XDocument.Load(@"../../xml/order.xml").Root;
        XElement? XMLorderItem = oiRoot?.Elements("Order").Where(o => o.Element("ID")?.Value == upOrderItem.ID.ToString()).FirstOrDefault();
        if (XMLorderItem is null) { throw new NotExistException(); }
        XMLorderItem.Element("ProductID").Value = upOrderItem.ProductID.ToString();
        XMLorderItem.Element("OrderID").Value = upOrderItem.OrderID.ToString();
        XMLorderItem.Element("Amount").Value = upOrderItem.Amount.ToString();
        XMLorderItem.Element("Price").Value = upOrderItem.Price.ToString();
        int index = orderItemList.FindIndex(item => item.ID == upOrderItem.ID);
        orderItemList[index] = upOrderItem;
    }

    public void Delete(int orderItemId)
    {

        XElement ? oiRoot= XDocument.Load(@"../../xml/order.xml").Root;
        XElement? XMLorderItem = oiRoot?.Elements("Order").Where(o => o.Element("ID")?.Value == orderItemId.ToString()).FirstOrDefault();
        if (XMLorderItem is null) { throw new NotExistException(); }
        XMLorderItem.Remove();
        int index = orderItemList.FindIndex(item => item.ID == orderItemId);
        orderItemList.RemoveAt(index);
       }
}
