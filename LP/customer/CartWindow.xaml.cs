using System.Collections.Generic;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>

public partial class CartWindow : Window
{

    private BlApi.IBL bl;
    private BO.Cart ? BOCart;
    private PO.Cart ?POCart { get; set; }

    public CartWindow(BlApi.IBL _bl, PO.Cart  _cart)
    {
        InitializeComponent();
        this.bl = _bl;
        this.POCart = _cart;
       // this.POCart = ConvertToPoCart(this.BOCart);
        this.DataContext = this.POCart;
    }

    private PO.Cart ConvertToPoCart(BO.Cart BoCart)
    {
      
        PO.Cart item = new()
        {
            CustomerAddress = BoCart.CustomerAddress,
            CustomerEmail = BoCart.CustomerEmail,
            CustomerName = BoCart.CustomerName,
            //Items = Pp.items.ForEach(i => ConvertToPoItem(i)).ToList(),
            Items = convertItemsToPoItems(BoCart.Items),
           TotalPrice = BoCart.Price,
        };
        return item;
    }


    private IEnumerable<PO.OrderItem> convertItemsToPoItems(List<BO.OrderItem> oil)
    {
        List<PO.OrderItem> returnlist = new();
        
        oil.ForEach (item =>
        {
            PO.OrderItem item2 = new()
            {
                Id = item.ID,
                Amount = item.Amount,
            
                Price = item.Price,
                ProductId = item.ProductID,
                Name = item.ProductName,
                TotalPrice = item.TotalPrice
            };
            returnlist.Add(item2);
        });
        return returnlist;
    }

    /* private List<PO.OrderItem> convertList()
     {
         PO.OrderItem i = new PO.OrderItem();
         foreach (BO.OrderItem tmp in list1)
         {
             i = ConvertToPo(tmp);
             List_p.Add(i);
         }
         return List_p;
     }*/

    public void GoBack_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow productList = new ProductListWindow(bl);
        productList.ShowDialog();
        this.Hide();
    }

    public void CompleteOrder_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("fg");
    }

    private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }

    private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }

    //private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
    //{

    //}

    //private void addProductBtn_Click(object sender, RoutedEventArgs e)
    //{
    //    try
    //    {
    //        bl.cart.Update(this.c, ((PO.OrderItem)(sender as Button).DataContext).ProductID, ((PO.OrderItem)(sender as Button).DataContext).Amount + 1);
    //        p = ConvertToPoCart(this.c);
    //        DataContext = p;
    //    }
    //    catch (BlOutOfStockException ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //    }
    //}

    //private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{

    //}

    //private void cartConfirmation(object sender, RoutedEventArgs e)
    //{

    //    new customer.ConfirmCart(bl, c).Show();
    //    this.Close();
    //}
}
