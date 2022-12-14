using System.Windows;
using BlApi;
namespace LP;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// Main display window
/// </summary>
public partial class MainWindow : Window
{
    private IBL Bl { get; set; }
    /// <summary>
    /// constractor of the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        Bl = new BlImplementation.BL();
    }

    /// <summary>
    /// when the button: "go to product" is press the function is activated
    /// the main window id hide and the window of the list of product is show
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChooseProductBtnWindow_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow productListWindow = new ProductListWindow(Bl);
        productListWindow.Show();
        this.Hide();
    }
}
