using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>

public partial class CartWindow : Window
{

    private BlApi.IBL bl;
    private BO.Cart ? BOCart;
    private PO.Cart ? POCart { get; set; }

    public CartWindow(BlApi.IBL _bl, PO.Cart  _cart, int productID = 0)
    {
        InitializeComponent();
        this.bl = _bl;
        this.POCart = _cart;
        this.DataContext = this.POCart;
       
       // this.POCart = ConvertToPoCart(this.BOCart);
        
    }

    private PO.Cart ConvertBoToPoCart(BO.Cart BoCart)
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
    private void UpdateAmount(int ID, int amount)
    {
       
        
            BO.Cart bCrt = convertPoCartToBoCart(POCart );
            bCrt = bl.Cart.UpdateProductAmount(bCrt, ID, amount);
           
            POCart = ConvertBoToPoCart(bCrt);
           
        
      
    }
    private BO.Cart convertPoCartToBoCart(PO.Cart poToConvert)
    {
        BO.Cart bCrt = new()
        {
            CustomerName = poToConvert.CustomerName,
            CustomerEmail = poToConvert.CustomerEmail,
            CustomerAddress = poToConvert.CustomerAddress,
            Price = poToConvert.TotalPrice
        };
        bCrt.Items = new();
   
        
        if (poToConvert.Items.Count() != 0)
            poToConvert.Items.Select(itm =>
            {
                BO.OrderItem oI = new()
                {
                    ID = itm.Id,
                    ProductID = itm.ProductId,
                    ProductName = itm.Name,
                    Price = itm.Price,
                    Amount = itm.Amount,
                    TotalPrice = itm.TotalPrice,
                };
                bCrt.Items.Add(oI);
                return itm;
            }).ToList();
        return bCrt;

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
    /// <summary>
    /// A function to increase the amount of a product in the cart by 1.
    /// </summary>
    private void IncreaseBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem currentOI = (PO.OrderItem)((Button)sender).DataContext;
        UpdateAmount(currentOI.ProductId, currentOI.Amount + 1);
    }
    /// <summary>
    /// A function to decrease the amount of a product in the cart by 1.
    /// </summary>
    private void DecreaseBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem currentOI = (PO.OrderItem)((Button)sender).DataContext;
        UpdateAmount(currentOI.ProductId, currentOI.Amount - 1);
    }

    private void DeleteBtn_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderItem currentOI = (PO.OrderItem)((Button)sender).DataContext;
        UpdateAmount(currentOI.ProductId, 0);
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
        new NewOrderWindow(bl, POCart).Show();
        Hide();
    }

    public void CompleteOrder_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("fg");
    }
    //private void btnPlus_Click(object sender, RoutedEventArgs e)
    //{
    //    try
    //    {
    //        var productId = ((Button)sender).Tag;
    //        BO.OrderItem? orderItem = BOCart?.Items?.Where(i => i.ProductID.Equals(productId)).First();
    //        int amount = orderItem.Amount;
    //        int prodID = orderItem.ProductID;
    //        ConvertToPoCart(bl.Cart.UpdateProductAmount((currentCart), prodID, amount + 1));
    //        ((NewOrderWindow)this.Owner).ProductItemRefresh();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //    }
    //}
    //private void btnMinus_Click(object sender, RoutedEventArgs e)
    //{
    //    int productId = (int)((Button)sender).Tag;
    //    BO.OrderItem? orderItem = currentCart.Items?.Where(i => i.ProductID.Equals(productId)).First();
    //    int amount = orderItem.Amount;
    //    int prodID = orderItem.ProductID;
    //    ConvertToPoCart(bl.Cart.UpdateProductAmount(castPoCartToBoCart(currentCart), prodID, amount - 1));
    //    listViewOrderItems.Items.Refresh();
    //    ((NewOrderWindow)this.Owner).ProductItemRefresh();
    //}

    //private void btnDelete_Click(object sender, RoutedEventArgs e)
    //{
    //    var productId = ((Button)sender).Tag;
    //    BO.OrderItem? orderItem = currentCart.Items?.Where(i => i.ProductID.Equals(productId)).First();
    //    int? amount = orderItem?.Amount;
    //    int prodID = orderItem.ProductID;
    //    castBoCartToCurrentCart(bl.Cart.UpdateProductAmount(castPoCartToBoCart(currentCart), prodID, 0));
    //    listViewOrderItems.Items.Refresh();
    //    ((NewOrderWindow)this.Owner).ProductItemRefresh();
    //}

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
