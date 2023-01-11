using System.Windows;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// a window for adding a new product or updating an existing one
/// </summary>
public partial class ProductWindow : Window
{
    private BlApi.IBL Bl { get; set; }
    private int product_id; //variable for conversion and testing 

    /// <summary>
    /// constractor of the product window
    /// the function checks if it received a certain ID:
    /// if so, she sends for an update
    /// if not, she sends to add a new product
    /// </summary>
    /// <param name="ProductList_bl"></param>
    /// <param name="pList_id"></param>
    public ProductWindow(BlApi.IBL ProductList_bl, int? pList_id = null)
    {
        InitializeComponent();
        Bl = ProductList_bl;
        CategoryComboBox.ItemsSource = eCategories.GetValues(typeof(eCategories));
        if (pList_id is null)
        {
            //putting the correct values in the fields of the window
            product_id = 0;
            TitleLabel.Content = "Add Product";
            AddOrUpdateBtn.Content = "Add a Product";
        }
        else
        {
            //putting the values of the product to be updated in the fields of the window
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
    /// <summary>
    /// when the button:"Go Back" is press the function is activated
    /// the window os the product list apears and this window disapears 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow GoBack = new ProductListWindow(Bl);
        GoBack.Show();
        this.Hide();
    }
    /// <summary>
    /// when the button:"add a product"/button:"update the product" is press the function is activated
    /// the function checks the inherited ID:
    /// if it null, then the function adds a new product
    /// if it has a full ID, the function sends the product for update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        // if the process ends successfully, return to the previous window
        MessageBox.Show("Successfull :)");
        ProductListWindow GoBack = new ProductListWindow(Bl);
        GoBack.Show();
        this.Hide();
    }
}