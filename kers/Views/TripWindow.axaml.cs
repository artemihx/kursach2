using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactiveUI;

namespace kers.Views;

public partial class TripWindow : Window
{
    public Route selectedRoute {get; set;}
    private TextBlock dateNow;
    public TripWindow(Route sr)
    {
        this.dateNow = this.Find<TextBlock>("DateNow");
        this.selectedRoute = sr;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new TripWindowViewModel(selectedRoute);
        AvaloniaXamlLoader.Load(this);
    }

    private void SelectTrip(object? sender, SelectionChangedEventArgs e)
    {
        var listbox = (ListBox)sender;
        Trip? selectedTrip = (Trip)listbox.SelectedItem;
        new BuyTicketWindow(selectedTrip).Show();
        this.Close();
    }

    private void BackToRoutes(object? sender, RoutedEventArgs e)
    {
        new ClRoutesWindow().Show();
        this.Close();
    }
}