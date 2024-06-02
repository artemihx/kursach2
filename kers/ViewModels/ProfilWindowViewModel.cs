using System.Collections.ObjectModel;
using System.Linq;
using kers.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace kers.ViewModels;

public class ProfilWindowViewModel : ViewModelBase
{
    private ObservableCollection<Passport> _passports;

    public ProfilWindowViewModel()
    {
        passports = new ObservableCollection<Passport>(Service.GetDbConnection()
            .Passporttousers
            .Where(ptu => ptu.Fkuser == User.curUser)
            .Include(ptu => ptu.Fkpassport)
            .Select(ptu => ptu.Fkpassport)
            .ToList());
    }
    
    public ObservableCollection<Passport> passports
    {
        get => _passports;
        set => this.RaiseAndSetIfChanged(ref _passports, value);
    }
}