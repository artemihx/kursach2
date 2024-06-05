using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;

namespace kers.Views;

public partial class EmployeeWindow : Window
{
    private ListBox routesList;
    public EmployeeWindow()
    {
        InitializeComponent();
        this.routesList = this.Find<ListBox>("RoutesList");
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
        new AddTripWindow(routesList.SelectedItem as Route).Show();
        this.Close();
    }
}