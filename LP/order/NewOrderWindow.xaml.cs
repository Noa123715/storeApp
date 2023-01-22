using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for NewOrder.xaml
/// </summary>
public partial class NewOrderWindow : Window
{
    public NewOrderWindow(BlApi.IBL? bl, int id = 0, MainWindow? nWindow = null)
    {
        InitializeComponent();
    }
}//