﻿using System.Windows;
using BlApi;
using BO;
using PL.PO;
using System;
using BlImplementation;

namespace PL;

/// <summary>
/// Interaction logic for NewOrderWindow.xaml
/// a display window that shows the entire list of product for the customer
/// </summary>
public partial class NewOrderWindow : Window
{
    private BlApi.IBL? Bl { get; set; }
    private BO.Cart? CurrentCart { get; set; }
    /// <summary>
    /// the constractor of the new MyOrder window
    /// the function initializes the array of product
    /// and initializes the categories
    /// </summary>
    /// <param name="bl"></param>
    public NewOrderWindow(BlApi.IBL? bl, BO.Cart? c = null, Window? sourcW = null)
    {
        InitializeComponent();
        CurrentCart = c ?? new BO.Cart();
        Bl = bl;
        NewOrderView.ItemsSource = Bl.Product.ReadProductsList();
        SelectorProduct.ItemsSource = eCategories.GetValues(typeof(eCategories));
    }
    /// <summary>
    /// when the button:"X" is press the functionis activated
    /// when the user want to return all product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FilterDelete_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            NewOrderView.ItemsSource = Bl.Product.ReadProductsList();
        }
        catch(BlNotExistException err)
        {
            MessageBox.Show( "No products found","", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    /// <summary>
    /// when the user choose a category to search
    /// the function search and send all product in this category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        try
        {
            NewOrderView.ItemsSource = Bl.Product.ReadProductsList((eCategories)SelectorProduct.SelectedItem);
        }
        catch (BlNotExistException)
        {
            MessageBox.Show("No products found", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void GoToCart_Click(object sender, RoutedEventArgs e)
        
    {
        try
        {
            new CartWindow(Bl, CurrentCart).Show();
            Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void GoToProductProperties(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        //not covert a item from producyForList to Product
        try
        {
            new ProductWindow(Bl, false, ((ProductForList)NewOrderView.SelectedItem).ID, CurrentCart).Show();
            Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public void GoBack_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            new MainWindow(CurrentCart).Show();
            this.Close();
        }
        catch (Exception err)
        {
            MessageBox.Show(new PlGenericException(err.Message).Message, "system error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}