using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using kers.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace kers.ViewModels;

public class EmployeeWindowViewModel : ViewModelBase
{
    public EmployeeWindowViewModel()
    {
        Routes = new ObservableCollection<Route>(Service.GetDbConnection().Routes.ToList());
        Trips = new ObservableCollection<Trip>();
        Statuses = new ObservableCollection<Status>(Service.GetDbConnection().Statuses.ToList());

        ClearDateCommand = ReactiveCommand.Create(ClearDate);
        ClearStatusCommand = ReactiveCommand.Create(ClearComboBox);
    }

    private ObservableCollection<Route> _routes;

    public ObservableCollection<Route> Routes
    {
        get => this._routes;
        set => this.RaiseAndSetIfChanged(ref _routes, value);
    }

    private Route _selectedRoute;

    public Route SelectedRoute
    {
        get => this._selectedRoute;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedRoute, value);
            UpdateTrips();
        }
    }

    private ObservableCollection<Trip> _trips;

    public ObservableCollection<Trip> Trips
    {
        get => this._trips;
        set => this.RaiseAndSetIfChanged(ref _trips, value);
    }
    
    private DateTimeOffset? _selectedDate;
    public DateTimeOffset? SelectedDate
    {
        get => _selectedDate;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedDate, value);
            UpdateTrips();
        }
    }

    private ObservableCollection<Status> _statuses;
    public ObservableCollection<Status> Statuses
    {
        get => _statuses;
        set => this.RaiseAndSetIfChanged(ref _statuses, value);
    }

    private Status _selectedStatus;
    public Status SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedStatus, value);
            UpdateTrips();
        }
    }

    private void UpdateTrips()
    {
        if (SelectedRoute != null)
        {
            var tripsQuery = Service.GetDbConnection().Trips.AsQueryable();

            tripsQuery = tripsQuery.Where(t => t.Route.Id == SelectedRoute.Id);

            if (SelectedDate.HasValue)
            {
                tripsQuery = tripsQuery.Where(t =>
                    DateOnly.FromDateTime(t.Timestart.Date) == DateOnly.FromDateTime(SelectedDate.Value.Date));
            }

            if (SelectedStatus != null)
            {
                tripsQuery = tripsQuery.Where(t => t.Status.Id == SelectedStatus.Id);
            }

            Trips = new ObservableCollection<Trip>(tripsQuery.OrderBy(t => t.Timestart).ToList());
        }
        else
        {
            Trips.Clear();
        }
    }

    public ICommand ClearDateCommand { get; }
        public ICommand ClearStatusCommand { get; }
        
        private void ClearDate()
        {
            SelectedDate = null;
        }

        private void ClearComboBox()
        {
            SelectedStatus = null;
        }
        
        private string _searchText;
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
                Routes = new ObservableCollection<Route>(Service.GetDbConnection().Routes.ToList());
            }
            else
            {
                Routes = new ObservableCollection<Route>(Routes.Where(r => r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }