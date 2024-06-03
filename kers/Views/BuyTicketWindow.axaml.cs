using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;
using static kers.Core;

namespace kers.Views;

public partial class BuyTicketWindow : Window
{
    private Trip selectedTrip { get; set; }
    private ComboBox passportBox;
    public BuyTicketWindow(Trip tr)
    {
        this.selectedTrip = tr;
        InitializeComponent();
        passportBox = this.Find<ComboBox>("ComboBoxPassport");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new BuyTicketWindowViewModel(selectedTrip);
        AvaloniaXamlLoader.Load(this);
    }

    private void AddPassport(object? sender, RoutedEventArgs e)
    {
        new AddPasportWindow(selectedTrip).Show();
        this.Close();
    }

    private void ReturnToTrip(object? sender, RoutedEventArgs e)
    {
        new TripWindow(selectedTrip.Route).Show();
        this.Close();
    }

    private void BuyBillet(object? sender, RoutedEventArgs e)
    {
        Passport selectedPassport = passportBox.SelectedItem as Passport;
        if (selectedPassport != null)
        {
            if(CheckSeat(selectedTrip))
            {
                try
                {
                    Ticket newTicket = new Ticket();
                    newTicket.Fkpassportid = selectedPassport.Id;
                    newTicket.Fktripid = selectedTrip.Id;
                    Service.GetDbConnection().Tickets.Add(newTicket);
                    Service.GetDbConnection().SaveChanges();
                    new ClRoutesWindow().Show();
                    this.Close();
                }
                catch (System.Exception)
                {
                    
                }
            }
            else
            {
                
            }
        }
    }
}