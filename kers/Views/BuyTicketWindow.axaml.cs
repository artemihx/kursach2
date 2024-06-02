using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;

namespace kers.Views;

public partial class BuyTicketWindow : Window
{
    private Trip selectedTrip { get; set; }
    public BuyTicketWindow(Trip tr)
    {
        this.selectedTrip = tr;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new BuyTicketWindowViewModel(selectedTrip);
        AvaloniaXamlLoader.Load(this);
    }

    private void AddPassport(object? sender, RoutedEventArgs e)
    {
        new AddPasportWindow(selectedTrip).Show();
        this.Close();
    }

    private void ReturnToTrip(object? sender, RoutedEventArgs e)
    {
        new TripWindow(selectedTrip.Route).Show();
        this.Close();
    }

    private void BuyBillet(object? sender, RoutedEventArgs e)
    {
        if (ComboBoxPassport.SelectedItem != null)
        {
            
        }
    }
}