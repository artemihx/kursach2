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

public partial class TripWindow : Window
{
    public Route selectedRoute {get; set;}
    public TripWindow(Route sr)
    {
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

}