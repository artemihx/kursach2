using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using kers.Models;
using ReactiveUI;

namespace kers.ViewModels;

public class ClRoutesWindowViewModel : ViewModelBase
{
    private ObservableCollection<Route> _routesList;
    private string _searchText;
    public ClRoutesWindowViewModel()
    {
        RoutesList = new ObservableCollection<Route>(Service.GetDbConnection().Routes.ToList());
    }

    public ObservableCollection<Route> RoutesList
    {
        get => _routesList;
        set => this.RaiseAndSetIfChanged(ref _routesList, value);
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            this.RaiseAndSetIfChanged(ref _searchText, value);
            FilterRoutes();
        }
    }

    private void FilterRoutes()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            RoutesList = new ObservableCollection<Route>(Service.GetDbConnection().Routes.ToList());
        }
        else
        {
            var filteredRoutes = RoutesList
                .Where(route => route.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            RoutesList = new ObservableCollection<Route>(filteredRoutes);
        }
    }
}