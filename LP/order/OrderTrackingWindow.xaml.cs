using System.Windows;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    public OrderTrackingWindow(Order orderTrack)
    {
        InitializeComponent();
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
