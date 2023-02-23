using System;
using System.Windows;
using BlApi;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// a display window that shows the entire list of product
/// </summary>
public partial class ProductListWindow : Window
{
    private BlApi.IBL bl { get; set; }
    /// <summary>
    /// the constractor of the product list window
    /// the function initializes the array of product and initializes the categories
    /// </summary>
    /// <param name="mainWindow_bl"></param>
    public ProductListWindow(BlApi.IBL mainWindow_bl)
    {
        InitializeComponent();
        bl = mainWindow_bl;
        ProductListView.ItemsSource = bl.Product.ReadProductsList();
        ProductSelector.ItemsSource = eCategories.GetValues(typeof(eCategories));
    }
    /// <summary>
    /// when the adminstrator choose a category to search
    /// the function search and send all product in this category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SelectionChangedCategory(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        try
        {
            ProductListView.ItemsSource = bl.Product.ReadProductsList((eCategories)ProductSelector.SelectedItem);
        }
        catch(BlNotExistException err)
        {
            MessageBox.Show("No products were found in this category, try choosing another category.", "No products in this category", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }
    /// <summary>
    /// when the button:"add a product" is press the function is activated
    /// the window for adding a product apears and this window disapears 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddItemBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductWindow p = new ProductWindow(bl, true);
            p.Show();
            this.Hide();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }
    /// <summary>
    /// when the button:"X" is press the functionis activated
    /// when the user want to return all product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteFilter_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductListView.ItemsSource = bl.Product.ReadProductsList();
        }
        catch (BlNotExistException err)
        {
            MessageBox.Show("No products were found.", " products not found", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    /// <summary>
    /// when a product is press the function is activated
    /// the product update window appears and this window disapears 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateProduct(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            new ProductWindow(bl, true, ((ProductForList)ProductListView.SelectedItem).ID).Show();
            Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message);
        }
    }
}