using System.Collections.ObjectModel;
using System.Linq;
using kers.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactiveUI;

namespace kers.ViewModels;

public class AddTripViewModel : ViewModelBase
{
    public AddTripViewModel()
    {
        Autos = new ObservableCollection<Auto>(Service.GetDbConnection().Autos.ToList());
    }

    private ObservableCollection<Auto> _autos;

    public ObservableCollection<Auto> Autos
    {
        get => _autos;
        set => this.RaiseAndSetIfChanged(ref _autos, value);
    }
}