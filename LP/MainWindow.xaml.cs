using System;
using System.Windows;
using BlApi;
using PL.order;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// Main display window
/// </summary>
public partial class MainWindow : Window
{
    private static BlApi.IBL? Bl { get; set; }
    /// <summary>
    /// constractor of the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        Bl = BlApi.Factory.Get();
    }

    /// <summary>
    /// when the button: "go to product" is press the function is activated
    /// the main window id hide and the window of the list of product is show
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LogInAdmin_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow productListWindow = new ProductListWindow(Bl);
        productListWindow.Show();
        this.Hide();
    }

    private void newOrder_Click(object sender, RoutedEventArgs e)
    {
        NewOrderWindow newOrderWindow = new NewOrderWindow(Bl);
        newOrderWindow.Show();
        this.Hide();
    }

    private void FollowOrder_Click(object sender, RoutedEventArgs e)
    {
        OrderTrackingWindow orderTrackingWindow= new OrderTrackingWindow();
        orderTrackingWindow.Show();
        this.Hide();
    }
}
