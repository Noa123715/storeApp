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
    //private ObservableCollection<BO.OrderForList>? OrderList { get; set; }
    public OrderWindow(BlApi.IBL? bl, int id, bool isAdmin = true)
    {
        InitializeComponent();
        Bl = bl;
        IsAdmin = isAdmin;
        MyOrder = Bl.Order.ReadOrderProperties(id);
        IdOrder.Content = $"MyOrder Number {MyOrder.ID}";
        this.DataContext = MyOrder;
        OrderItemList.ItemsSource = MyOrder.Items;
        if (!IsAdmin)
        {
            NameCustomerText.IsEnabled = false;
            EmailCustomerText.IsEnabled = false;
            AddressCustomerText.IsEnabled = false;
            OrderStatusText.IsEnabled = false;
            OrderDateText.IsEnabled = false;
            ShipDateText.IsEnabled = false;
            DeliveryDateText.IsEnabled = false;
            TotalPriceText.IsEnabled = false;
        }
    }


    /// <summary>
    /// GoBackBtn_Click method- Return to the order list screen for admin and to order tracking screen for customer.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GoBackBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (IsAdmin)
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
                order.TrackList.Add(new Tuple<DateTime?, string>(MyOrder.ShipDate, "ShipDate"));
                new OrderTrackingWindow(Bl, order).Show();
                Hide();
            }
        }

        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }

    /// <summary>
    /// MinusBtn_Click method decreases the quantity of the product in the order by one if it hasn't been sent yet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MinusBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!IsAdmin)
            {
                MessageBox.Show(
                    "We are so sorry! but You can't change your order now",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );
            }
            else
            {
                var productId = ((Button)sender).Tag;
                MyOrder = Bl.Order.AddAmount(MyOrder.ID, Convert.ToInt32(productId), -1);
                OrderItemList.ItemsSource = MyOrder.Items;
            }
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// AddBtn_Click method Increases the quantity of the product in the order by one if it hasn't been sent yet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!IsAdmin)
            {
                MessageBox.Show(
                    "We are so sorry! but You can't change your order now",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );
            }
            else
            {
                var productId = ((Button)sender).Tag;
                MyOrder = Bl.Order.AddAmount(MyOrder.ID, Convert.ToInt32(productId), 1);
                OrderItemList.ItemsSource = MyOrder.Items;
            }
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    /// <summary>
    /// DeleteBtn_Click  method deletes the product from the order if it hasn't been sent yet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!IsAdmin)
            {
                MessageBox.Show(
                    "We are so sorry! but You can't delete your order now",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );
            }
            else
            {
                var productId = ((Button)sender).Tag;
                MyOrder = Bl.Order.AddAmount(MyOrder.ID, Convert.ToInt32(productId), 0);
                OrderItemList.ItemsSource = MyOrder.Items;
            }
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    /// <summary>
    ///  AddProductBtn_Click method adds product to the order if it hasn't been sent yet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProductBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (IsAdmin)
            {
                new ProductListWindow(Bl).Show();

            }
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    /// <summary>
    /// UpDateBtn_Click method lets the admin updates the order details if it hasn't been sent yet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void UpDateBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (IsAdmin)
            {
                if (MyOrder?.Status == BO.eOrderStatus.Delivered)
                    return;
                else if (MyOrder?.Status == BO.eOrderStatus.Shipped)
                {
                    Bl.Order.UpdateOrderDelivery(Convert.ToInt32(OrderId));
                    this.Close();
                }
                else
                {
                    Bl.Order.UpdateOrderSent(Convert.ToInt32(OrderId));
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show(
                    "We are so sorry! but You can't delete your order now",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );
            }
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
