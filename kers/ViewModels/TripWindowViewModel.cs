using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using kers.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace kers.ViewModels
{
    public class TripWindowViewModel : ViewModelBase
    {   
        public TripWindowViewModel(Route sr)
        {
            selectedRoute = sr;
            DateNow = DateOnly.FromDateTime(DateTime.Now);;
            TripList = Service.GetDbConnection().Trips.Where(t => t.Routeid == selectedRoute.Id && t.Statusid == 1 && t.Timestart == DateNow).Include(t=> t.Route).Include(t=> t.Status).ToList();
        }

        public List<Trip> TripList {get; set;}

        public Route selectedRoute {get; set;}

        public DateOnly DateNow {get; set;} 
    }
}

