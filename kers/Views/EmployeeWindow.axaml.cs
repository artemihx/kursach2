using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using iText.Layout.Element;
using kers.Models;
using kers.ViewModels;
using Microsoft.EntityFrameworkCore;
using static kers.Core;

namespace kers.Views;

public partial class EmployeeWindow : Window
{
    private ListBox routesList;
    private ListBox tripList;
    public EmployeeWindow()
    {
        InitializeComponent();
        this.routesList = this.Find<ListBox>("RoutesList");
        this.tripList = this.Find<ListBox>("TripListBox");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new EmployeeWindowViewModel();
        AvaloniaXamlLoader.Load(this);
    }

    private void ExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }

    private void OpenAddTripWindow(object? sender, RoutedEventArgs e)
    {
        if (routesList.SelectedItem is Route selectedRoute)
        {
            new AddTripWindow(selectedRoute).Show();
            this.Close();
        }
    }

    private async void ChangeStatus(object? sender, TappedEventArgs e)
    {
        await new ChangeStatusWindow(tripList.SelectedItem as Trip).ShowDialog(this);
        this.DataContext = new EmployeeWindowViewModel();
    }

    private void PrintInfo(object? sender, RoutedEventArgs e)
    {
        if (tripList.SelectedItem is Trip selectedTrip)
        {
            Trip trip = Service.GetDbConnection().Trips
                .Include(t => t.Auto)
                .FirstOrDefault(t => t == selectedTrip);
            PrintInfoTrip(trip);
        }
    }
}