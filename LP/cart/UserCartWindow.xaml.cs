using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for UserCartWindow.xaml
/// </summary>
public partial class UserCartWindow : Window
{
    private BlApi.IBL? Bl { get; set; }
    public UserCartWindow(BlApi.IBL? bl)
    {
        InitializeComponent();
        Bl = bl;
    }

    private void CompleteOrder_Click(object sender, RoutedEventArgs e)
    {
        CompleteOrderWindow completeOrder = new CompleteOrderWindow();
        completeOrder.Show();
        this.Hide();
    }
}