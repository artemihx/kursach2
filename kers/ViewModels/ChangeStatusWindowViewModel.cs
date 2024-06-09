using System;
using System.Collections.ObjectModel;
using System.Linq;
using kers.Models;
using ReactiveUI;

namespace kers.ViewModels;

public class ChangeStatusWindowViewModel : ViewModelBase
{
    public ChangeStatusWindowViewModel(Trip SelectedTrip)
    {
        StatusList = new ObservableCollection<Status>(Service.GetDbConnection().Statuses.ToList());
        SelectedStatus = SelectedTrip.Status;
    }
    
    private ObservableCollection<Status> _statusList;
    private Status _selectedStatus;

    public ObservableCollection<Status> StatusList
    {
        get => _statusList;
        set => this.RaiseAndSetIfChanged(ref _statusList, value);
    }

    public Status SelectedStatus
    {
        get => _selectedStatus;
        set => this.RaiseAndSetIfChanged(ref _selectedStatus, value);
    }
}