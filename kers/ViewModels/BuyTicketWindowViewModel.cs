using System.Collections.ObjectModel;
using System.Linq;
using kers.Models;
using Microsoft.EntityFrameworkCore;

namespace kers.ViewModels;

public class BuyTicketWindowViewModel : ViewModelBase
{
    public Trip selectTrip { get; set; }

    public ObservableCollection<Passport> passports { get; set; }

    public BuyTicketWindowViewModel(Trip tr)
    {
        selectTrip = Service.GetDbConnection().Trips.Where(t => t == tr)
            .Include(t => t.Route)
            .Include(t => t.Auto).First();
        passports = new ObservableCollection<Passport>(Service.GetDbConnection()
            .Passporttousers
            .Where(ptu => ptu.Fkuser == User.curUser)
            .Include(ptu => ptu.Fkpassport)
            .Select(ptu => ptu.Fkpassport)
            .ToList());
    }
}