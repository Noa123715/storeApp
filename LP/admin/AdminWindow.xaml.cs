using System;
using System.Collections.Generic;
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
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    private static BlApi.IBL? Bl { get; set; }

    public AdminWindow(BlApi.IBL bL)
    {

        InitializeComponent();
        Bl = bL;

    }

    private void Orders_Click(object sender, RoutedEventArgs e)
    {
        try
        {

            new OrderListWindow(Bl).Show();
            Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }
    private void Products_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            new ProductListWindow(Bl).Show();
            Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }

}
