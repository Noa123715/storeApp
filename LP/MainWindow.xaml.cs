using System.Windows;
using BlApi;
namespace LP;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBL Bl { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        Bl = new BlImplementation.BL();
    }

    private void ChooseProductBtnWindow_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow productListWindow = new ProductListWindow(Bl);
        productListWindow.Show();
        this.Hide();
    }
}
