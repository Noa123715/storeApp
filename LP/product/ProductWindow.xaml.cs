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
    private bool IsAdmin;
    /// <summary>
    /// constractor of the product window
    /// the function checks if it received a certain ID:
    /// if so, she sends for an update
    /// if not, she sends to add a new product
    /// </summary>
    /// <param name="ProductList_bl"></param>
    /// <param name="pList_id"></param>
    public ProductWindow(BlApi.IBL ProductList_bl, bool isAdmin, int? pList_id = null)
    {
        InitializeComponent();
        Bl = ProductList_bl;
        IsAdmin = isAdmin;
        CategoryComboBox.ItemsSource = eCategories.GetValues(typeof(eCategories));
        if (isAdmin)
        {
            if (pList_id is null)
            {
                //putting the correct values in the fields of the window
                pList_id = 0;
                TitleLabel.Content = "Add Product";
                AddOrUpdateBtn.Content = "Add a Product";
                delBtn.Visibility = Visibility.Hidden;
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
                delBtn.Visibility = Visibility.Visible;
            }
        }
        else
        {
            TitleLabel.Content = "Properties of Product";
            AddOrUpdateBtn.Content = "Add to Cart";
            Cart cart = new Cart();
            product_id = (int)pList_id;
            ProductItem product = Bl.Product.ReadProductProperties(product_id, cart);
            IdTextBox.Text = product.ID.ToString();
            NameTextBox.Text = product.Name;
            PriceTextBox.Text = product.Price.ToString();
            CategoryComboBox.SelectedItem = product.Category;
            //להוסיף את הכמות בשביל ההזמנה
            delBtn.Visibility = Visibility.Hidden;
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
        if (IsAdmin)
        {
            Product product = new Product()
            {
                ID = int.Parse(IdTextBox.Text),
                Name = NameTextBox.Text,
                //InStock = int.Parse(AmountTextBox.Text),
                Category = (eCategories)CategoryComboBox.SelectedItem,
                Price = int.Parse(PriceTextBox.Text)
            };
            if (product_id == 0)
            {
                //the add is successfull but after it the main window have a runtime error about the root attributte of the product xml
                Bl.Product.AddProduct(product);
                MessageBox.Show("Add Successfull 👍");
            }
            else
            {
                Bl.Product.UpdateProduct(product);
                MessageBox.Show("UpDate Successfull 👍");
            }
            //if the process ends successfully, return to the previous window
            ProductListWindow GoBack = new ProductListWindow(Bl);
            GoBack.Show();
            this.Hide();
        }
    }
    /// <summary>
    /// error-order item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void deleteProduct_Click(object sender, RoutedEventArgs e)
    {
        //why is orderItemList is null when this code is run?
        // if (condition is null)orderitem readall function
        //return orderItemList ?? throw new NotExistException();
        //return orderItemList.Where(condition).ToList() ?? throw new NotExistException();

        int id = int.Parse(IdTextBox.Text);
        Bl.Product.DeleteProduct(id);
        // if the process ends successfully, return to the previous window
        MessageBox.Show("Delete Successfull 👍");
        ProductListWindow GoBack = new ProductListWindow(Bl);
        GoBack.Show();
        this.Hide();
    }
}




//explenation of product:
//Product - for admin - with int inStock - ReadProductsProperities(int id)
//  int ID,string? Name,double Price,eCategories Category,int InStock
//ProductForList - for show - without inStock - ReadProductList(optional categories)
//  int ID,string? Name,double Price,eCategories Category
//ProductItem - for user - with bool isInStock - ReadProductsProperities(int id, cart)
//  int ID,string? Name,double Price,eCategories Category,bool IsInStock,int Amount