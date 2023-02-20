using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private BO.Cart? BOCart;
    private PO.Cart? POCart { get; set; }

    public CartWindow(BlApi.IBL _bl, BO.Cart _cart, int productID = 0)
    {
        InitializeComponent();
        this.bl = _bl;
        this.BOCart = _cart;
        POCart = ConvertBoToPoCart(BOCart);
        this.DataContext = this.POCart;
    }

    private PO.Cart ConvertBoToPoCart(BO.Cart BoCart)
    {

        PO.Cart item = new()
        {
            CustomerAddress = BoCart.CustomerAddress,
            CustomerEmail = BoCart.CustomerEmail,
            CustomerName = BoCart.CustomerName,
            Items = convertItemsToPoItems(BoCart.Items),
            TotalPrice = BoCart.Price,
        };
        return item;
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
    private ObservableCollection<PO.OrderItem> convertItemsToPoItems(List<BO.OrderItem> oil)
    {
        ObservableCollection<PO.OrderItem> returnlist = new();
        oil.ForEach(item =>
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

    private void UpdateAmount(int ID, int amount)
    {
        BOCart = convertPoCartToBoCart(POCart);
        BOCart = bl.Cart.UpdateProductAmount(BOCart, ID, amount);
        POCart.Items.Clear();
        POCart = ConvertBoToPoCart(BOCart);
        this.DataContext = POCart;
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

    public void GoBack_Click(object sender, RoutedEventArgs e)
    {
        new NewOrderWindow(bl, BOCart).Show();
        this.Close();
    }

    public void CompleteOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (NameTxt.Text == "")
                throw new PlNullValueException("customer name");
            if (EmailTxt.Text == "")
                throw new PlNullValueException("customer email");
            System.Net.Mail.MailAddress addr = new(EmailTxt.Text);
            bool isValidEmail = (addr.Address == EmailTxt.Text);
            if (!(isValidEmail))
                throw new PlInvalidEmailException();
            if (AddressTxt.Text == "")
                throw new PlNullValueException("customer address");
            BO.Cart confirmCart = convertPoCartToBoCart(POCart);
            bl.Cart.Confirmation(confirmCart);
            MessageBox.Show("the order was confirmed");
            POCart = new();
            BOCart = new();
            new NewOrderWindow(bl, BOCart).Show();
            Close();
        }

        catch (PlNullValueException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (PlInvalidEmailException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(new PlGenericException(ex.Message).Message, "Error");
        }
    }



    private void EmptyCart_Click(object sender, RoutedEventArgs e)
    {
        POCart = new();
        BOCart = new();
        DataContext = POCart;
    }
    private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }

    private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}
