using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Linq;
namespace PL;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    private BlApi.IBL? Bl { get; set; }
    private BO.Order? MyOrder { get; set; }
    private bool IsAdmin { get; set; }
    public int? OrderId { get; set; }
    private ObservableCollection<BO.Order>? OrderForLists { get; set; }
    public OrderWindow(BlApi.IBL? bl, int id, bool isAdmin = true)
    {
        InitializeComponent();
        Bl = bl;
        IsAdmin = isAdmin;
        MyOrder = Bl.Order.ReadOrderProperties(id);
        IdOrder.Content = $"MyOrder Number {MyOrder.ID}";
        this.DataContext = MyOrder;
        OrderItemList.ItemsSource = MyOrder.Items;
    }

    private void GoBackBtn_Click(object sender, RoutedEventArgs e)
    {
        if(IsAdmin)
        {
            new OrderListWindow(Bl).Show();
            Hide();
        }
        else
        {
            BO.OrderTracking order = new()
            {
                ID = MyOrder.ID,
                Status = MyOrder.Status,
                TrackList = new()
            };
            order.TrackList.Add(new Tuple<DateTime?, string>(MyOrder.OrderDate, "OrderDate"));
            order.TrackList.Add(new Tuple<DateTime?, string>(MyOrder.DeliveryDate, "DeliveryDate"));
            new OrderTrackingWindow(Bl, order).Show();
        }
    }
}
