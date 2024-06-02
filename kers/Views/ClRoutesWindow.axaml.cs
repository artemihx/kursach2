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
using ReactiveUI;

namespace kers.Views;

public partial class ClRoutesWindow : Window
{
    public ClRoutesWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new ClRoutesWindowViewModel();
        AvaloniaXamlLoader.Load(this);
    }

    private void SelectRoute(object? sender, SelectionChangedEventArgs e)
    {
        var listbox = (ListBox)sender;
        Route? selectedRoute = (Route)listbox.SelectedItem;
        new TripWindow(selectedRoute).Show();
        this.Close();
    }

    private void Exit(object? sender, RoutedEventArgs e)
    {
        User.curUser = null;
        new MainWindow().Show();
        this.Close();
    }

    private void OpenProfilWindow(object? sender, RoutedEventArgs e)
    {
        new ProfilWindow().Show();
        this.Close();
    }
}