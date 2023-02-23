using BlImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private static BlApi.IBL? Bl { get; set; }
    private ObservableCollection<BO.OrderForList> orderList { get; set; }
    public OrderListWindow(BlApi.IBL bl)
    {
        InitializeComponent();
        Bl = bl;
        orderList = new ObservableCollection<BO.OrderForList>(Bl.Order.ReadOrderList());
        OrderListView.ItemsSource = orderList;
    }

    /// <summary> 
    /// MouseDoubleClick handler. During a double-click event on a certain product, switches to display product details.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            new OrderWindow(Bl, (OrderListView?.SelectedItem as BO.OrderForList).ID, true).Show();
            this.Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }

    /// <summary>
    /// go back click handler switches to display the admin window.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    public void GoBack_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            new AdminWindow(Bl).Show();
            this.Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }

}
