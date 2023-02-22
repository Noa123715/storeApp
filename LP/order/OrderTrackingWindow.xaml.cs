using System.Collections.ObjectModel;
using System.Windows;
using BlImplementation;
using BO;
using PL;

namespace PL;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private BlApi.IBL? Bl { get; set; }
    private Cart CurrentCart { get; set; }
    public OrderTrackingWindow(BlApi.IBL? bl, OrderTracking orderTrack, Cart? _currentCart = null)
    {
        InitializeComponent();
        Bl = bl;
        IdOrderLabel.Content = $"Order Number {orderTrack.ID}:"; 
        StatusText.Text = orderTrack.Status.ToString();
        TrackList.ItemsSource = orderTrack.TrackList;
    }

    private void OrderDetails_Click(object sender, RoutedEventArgs e)
    {
        new OrderWindow();
        Hide();
    }
}
