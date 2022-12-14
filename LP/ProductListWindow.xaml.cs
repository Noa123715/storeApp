using System.Windows;
using BlApi;
using BO;

namespace LP;
/// <summary>
/// Interaction logic for User.xaml
/// </summary>

public partial class ProductListWindow : Window
{
    private IBL bl { get; set; }

    public ProductListWindow(IBL mainWindow_bl)
    {
        InitializeComponent();
        bl = mainWindow_bl;
        ProductListView.ItemsSource = bl.Product.ReadProductsList();
        ProductSelector.ItemsSource = eCategories.GetValues(typeof(eCategories));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        ProductListView.ItemsSource = bl.Product.ReadProductsList((eCategories)ProductSelector.SelectedItem);
    }

    private void AddItemBtn_Click(object sender, RoutedEventArgs e)
    {
        ProductWindow p = new ProductWindow(bl);
        p.Show();
        this.Hide();
    }

    private void FilterDelete(object sender, RoutedEventArgs e)
    {
        ProductListView.ItemsSource = bl.Product.ReadProductsList();
    }

    private void UpdateProduct(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        new ProductWindow(bl, ((ProductForList)ProductListView.SelectedItem).ID).Show();
        Close();
    }
}