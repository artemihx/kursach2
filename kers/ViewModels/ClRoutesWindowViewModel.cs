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
    public ClRoutesWindowViewModel()
    {
        RoutesList = Service.GetDbConnection().Routes.ToList();
    }
    public List<Route> RoutesList { get; set; }
    

}