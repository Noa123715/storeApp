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
    private BO.Order? Order { get; set; }
    private bool IsAdmin { get; set; }
    public int? OrderId { get; set; }
    private ObservableCollection<BO.Order>? OrderForLists { get; set; }
    public OrderWindow(BlApi.IBL? bl, int id, bool isAdmin = true)
    {
        InitializeComponent();
        Bl = bl;
        IsAdmin = isAdmin;
        BO.Order order = Bl.Order.ReadOrderProperties(id);
        IdOrder.Content = $"Order Number {order.ID}";
        this.DataContext = order;
        OrderItemList.ItemsSource = order.Items;
    }
}
