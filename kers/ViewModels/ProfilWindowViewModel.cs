using System.Collections.ObjectModel;
using System.Linq;
using kers.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace kers.ViewModels;

public class ProfilWindowViewModel : ViewModelBase
{
    private ObservableCollection<Passport> _passports;
    private ObservableCollection<Ticket> _tickets;

    public ProfilWindowViewModel()
    {
        passports = new ObservableCollection<Passport>(Service.GetDbConnection()
            .Passporttousers
            .Where(ptu => ptu.Fkuser == User.curUser)
            .Include(ptu => ptu.Fkpassport)
            .Select(ptu => ptu.Fkpassport)
            .ToList());
        tickets = new ObservableCollection<Ticket>(Service.GetDbConnection().Users
            .Join(Service.GetDbConnection().Passporttousers,
                u => u.Id,
                pu => pu.Fkuserid,
                (u, pu) => new { u, pu })
            .Join(Service.GetDbConnection().Passports,
                upu => upu.pu.Fkpassportid,
                p => p.Id,
                (upu, p) => new { upu.u, upu.pu, p })
            .Join(Service.GetDbConnection().Tickets,
                upp => upp.p.Id,
                t => t.Fkpassportid,
                (upp, t) => new { upp.u, upp.p, t })
            .Where(x => x.u.Id == User.curUser.Id)
            .Select(x => x.t)
            .Include(x => x.Fktrip.Auto)
            .ToList());
    }
    
    public ObservableCollection<Passport> passports
    {
        get => _passports;
        set => this.RaiseAndSetIfChanged(ref _passports, value);
    }

    public ObservableCollection<Ticket> tickets
    {
        get => _tickets;
        set => this.RaiseAndSetIfChanged(ref _tickets, value);
    }
}