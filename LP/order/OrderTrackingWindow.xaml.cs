using System.Windows;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    Cart currentCart { get; set; }
    public OrderTrackingWindow(OrderTracking orderTrack, BO.Cart? _currentCart= null)    {
        InitializeComponent();
        currentCart = _currentCart;
        IdOrderText.Text = orderTrack.ID.ToString();
    }
}
