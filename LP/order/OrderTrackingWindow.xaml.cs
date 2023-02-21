using System.Windows;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
<<<<<<< HEAD
    public OrderTrackingWindow(Order orderTrack)
    {
=======
    Cart currentCart { get; set; }
    public OrderTrackingWindow(OrderTracking orderTrack, BO.Cart? _currentCart= null)    {
>>>>>>> d6862b8f831d961a8d4c8bba78d3600e758affd7
        InitializeComponent();
        currentCart = _currentCart;
        IdOrderText.Text = orderTrack.ID.ToString();
        NameCustomerText.Text = orderTrack.CustomerName?.ToString();
        EmailCustomerText.Text = orderTrack.CustomerEmail?.ToString();
        AddressCustomerText.Text = orderTrack.CustomerAddress?.ToString();
        OrderDateText.Text = orderTrack.OrderDate?.ToString();
        ShipDateText.Text = orderTrack.ShipDate?.ToString();
        DeliveryDateText.Text = orderTrack.DeliveryDate?.ToString();
    }

    private void OrderDetails_Click(object sender, RoutedEventArgs e)
    {

    }
}
