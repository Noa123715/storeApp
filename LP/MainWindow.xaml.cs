using System.Windows;

namespace LP;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBL Bl { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        Bl = new BlImplementation.BL();
    }

    private void ChooseProductBtnWindow_Click(object sender, RoutedEventArgs e)
    {
        ProductList productListWindow = new();
        productListWindow.Show();
        this.Hide();
    }
}
