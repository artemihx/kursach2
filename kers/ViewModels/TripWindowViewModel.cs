using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using kers.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using static kers.Core;

namespace kers.ViewModels
{
    public class TripWindowViewModel : ViewModelBase
    {   
        public ICommand SelectUp { get; }
        
        public ICommand SelectDown { get; }
        
        private ObservableCollection<Trip> _tripList;
        private string _infoPassenger;
        
        private DateOnly _dateNow;
        public TripWindowViewModel(Route sr)
        {
            selectedRoute = sr;
            DateNow = DateOnly.FromDateTime(DateTime.Now);;
            TripList = new ObservableCollection<Trip>(Service.GetDbConnection().Trips
                .Where(t => t.Routeid == selectedRoute.Id && t.Statusid == 1 && DateOnly.FromDateTime(t.Timestart.Date) == DateNow)
                .Include(t => t.Route)
                .Include(t => t.Status)
                .ToList());
            SelectUp = ReactiveCommand.Create(() =>
            {
                DateNow = DateNow.AddDays(1);
                TripList = new ObservableCollection<Trip>(Service.GetDbConnection().Trips
                    .Where(t => t.Routeid == selectedRoute.Id && t.Statusid == 1 && DateOnly.FromDateTime(t.Timestart.Date) == DateNow)
                    .Include(t => t.Route)
                    .Include(t => t.Status)
                    .ToList());
            });
            SelectDown = ReactiveCommand.Create(() =>
            {
                DateNow = DateNow.AddDays(-1);
                TripList = new ObservableCollection<Trip>(Service.GetDbConnection().Trips
                    .Where(t => t.Routeid == selectedRoute.Id && t.Statusid == 1 && DateOnly.FromDateTime(t.Timestart.Date) == DateNow)
                    .Include(t => t.Route)
                    .Include(t => t.Status)
                    .ToList());
            });
        }

        public ObservableCollection<Trip> TripList
        {
            get => _tripList;
            set => this.RaiseAndSetIfChanged(ref _tripList, value);
        }

        public Route selectedRoute {get; set;}
        
        public DateOnly DateNow
        {
            get => _dateNow;
            set => this.RaiseAndSetIfChanged(ref _dateNow, value);
        }

        public string InfoPassneger
        {
            get => _infoPassenger;
            set => this.RaiseAndSetIfChanged(ref _infoPassenger, value);
        }
    }
}

