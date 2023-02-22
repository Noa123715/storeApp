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
    private int Id { get; set; }
    private Cart CurrentCart { get; set; }
    public OrderTrackingWindow(BlApi.IBL? bl, OrderTracking orderTrack, Cart? currentCart = null)
    {
        InitializeComponent();
        Bl = bl;
        Id = orderTrack.ID;
        IdOrderLabel.Content = $"MyOrder Number {orderTrack.ID}:";
        this.DataContext = orderTrack;
        TrackList.ItemsSource = orderTrack.TrackList;
    }

    private void OrderDetails_Click(object sender, RoutedEventArgs e)
    {
        new OrderWindow(Bl, Id, false).Show();
        Hide();
    }
}
