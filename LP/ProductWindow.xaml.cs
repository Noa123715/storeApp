using System.Windows;
using BlApi;
using BO;
namespace LP;

/// <summary>
/// Interaction logic for MainUser.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBL Bl { get; set; }
    private int product_id;

    public ProductWindow(IBL ProductList_bl, int? pList_id = null)
    {
        InitializeComponent();
        Bl = ProductList_bl;
        CategoryComboBox.ItemsSource = eCategories.GetValues(typeof(eCategories));
        if (pList_id is null)
        {
            product_id = 0;
            TitleLabel.Content = "Add Product";
            AddOrUpdateBtn.Content = "Add a Product";
        }
        else
        {
            TitleLabel.Content = "UpDate The Product";
            AddOrUpdateBtn.Content = "Update";
            product_id = (int)pList_id;
            Product product = Bl.Product.ReadProductProperties(product_id);
            IdTextBox.Text = product.ID.ToString();
            NameTextBox.Text = product.Name;
            PriceTextBox.Text = product.Price.ToString();
            CategoryComboBox.SelectedItem = product.Category;
            AmountTextBox.Text = product.InStock.ToString();
        }
    }

    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow GoBack = new ProductListWindow(Bl);
        GoBack.Show();
        this.Hide();
    }

    private void AddOrUpdateBtn_Click(object sender, RoutedEventArgs e)
    {
        Product product = new Product()
        {
            ID = int.Parse(IdTextBox.Text),
            Name =  NameTextBox.Text,
            InStock = int.Parse(AmountTextBox.Text),
            Category = (eCategories)CategoryComboBox.SelectedItem,
            Price = int.Parse(PriceTextBox.Text)
        };
        if (product_id == 0)
        {
            Bl.Product.AddProduct(product);
        }
        else
        {
            Bl.Product.UpdateProduct(product);
        }
        MessageBox.Show("Successfull :)");
        ProductListWindow GoBack = new ProductListWindow(Bl);
        GoBack.Show();
        this.Hide();
    }
}