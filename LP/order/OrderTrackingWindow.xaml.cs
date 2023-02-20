using System.Windows;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    public OrderTrackingWindow(OrderTracking orderTrack)
    {
        InitializeComponent();
        IdOrderText.Text = orderTrack.ID.ToString();
    }
}
