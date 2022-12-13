using System.Windows;
using BlApi;

namespace LP;
/// <summary>
/// Interaction logic for User.xaml
/// </summary>

public partial class ProductList : Window
{
    private IBL bl = new BlImplementation.BL();
    private int p_id;

    public ProductList()
    {
        InitializeComponent();
        ProductListView.ItemsSource = bl.Product.ReadProductsList();
        ProductSelector.ItemsSource = BO.eCategories.GetValues(typeof(BO.eCategories));
    }
}