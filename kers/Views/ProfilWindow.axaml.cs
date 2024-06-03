using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;
using static kers.Core;

namespace kers.Views;

public partial class ProfilWindow : Window
{
    private TextBox nameTbox;
    private TextBox lnameTbox;
    private TextBox mnameTbox;
    private TextBox serialTbox;
    private TextBox numberTbox;
    private DatePicker datePicker;
    private DataGrid passportsList;

    private ListBox ticketsList;
    private TextBlock ticketDetail;
    private Button returnButton;
    private Button printButton;
    public ProfilWindow()
    {
        InitializeComponent();
        nameTbox = this.Find<TextBox>("NameTBox");
        lnameTbox = this.Find<TextBox>("LNameTBox");
        mnameTbox = this.Find<TextBox>("MNameTBox");
        serialTbox = this.Find<TextBox>("SerialTBox");
        numberTbox = this.Find<TextBox>("NumberTBox");
        datePicker = this.Find<DatePicker>("DatePicker");
        passportsList = this.Find<DataGrid>("PassportsList");

        ticketsList = this.Find<ListBox>("TicketsUser");
        ticketDetail = this.Find<TextBlock>("TicketDetails");
        returnButton = this.Find<Button>("ButtonReturn");
        printButton = this.Find<Button>("PrintButton");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new ProfilWindowViewModel();
        AvaloniaXamlLoader.Load(this);
    }

    private void Exit(object? sender, RoutedEventArgs e)
    {
        new ClRoutesWindow().Show();
        this.Close();
    }

    private void SavePassport(object? sender, RoutedEventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(nameTbox.Text) && !string.IsNullOrWhiteSpace(lnameTbox.Text)
           && !string.IsNullOrWhiteSpace(serialTbox.Text) && !string.IsNullOrWhiteSpace(numberTbox.Text))
           {
             Passport newPass = new Passport();
             Passporttouser newPassporttouser = new Passporttouser();
            try
            {
               newPass.Fname = nameTbox.Text;
               newPass.Lname = lnameTbox.Text;
               newPass.Mname = mnameTbox.Text;
               newPass.Serail = serialTbox.Text;
               newPass.Number = numberTbox.Text;
               newPass.Datetaken = DateOnly.FromDateTime(datePicker.SelectedDate.Value.Date);
               Service.GetDbConnection().Passports.Add(newPass);
               Service.GetDbConnection().SaveChanges();
               newPassporttouser.Fkpassportid =newPass.Id;
               newPassporttouser.Fkuserid = User.curUser.Id;
               Service.GetDbConnection().Passporttousers.Add(newPassporttouser);
               Service.GetDbConnection().SaveChanges();
               new ProfilWindow().Show();
               this.Close();
            }
           catch (Exception exception)
           {
               
           }
           }
    }

    private void DeletePassport(object? sender, RoutedEventArgs e)
    {
        if (passportsList.SelectedItem != null)
        {
            try
            {
                Passport pass = passportsList.SelectedItem as Passport;
                var deletePU = Service.GetDbConnection().Passporttousers
                    .Where(pu => pu.Fkuserid == User.curUser.Id && pu.Fkpassportid == pass.Id).FirstOrDefault();
                Service.GetDbConnection().Passporttousers.Remove(deletePU);
                Service.GetDbConnection().SaveChanges();
                Service.GetDbConnection().Passports.Remove(pass);
                Service.GetDbConnection().SaveChanges();
                new ProfilWindow().Show();
                this.Close();
            }
            catch (Exception exception)
            {

            }
        }
    }

    private void TicketsUser_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ticketsList.SelectedItem is Ticket selectedTicket)
        {
            ticketDetail.Text = $"Маршрут: {selectedTicket.Fktrip.Route.Name}\n" +
                                $"Время отправления: {selectedTicket.Fktrip.Timestart}\n" +
                                $"Время прибытия: {selectedTicket.Fktrip.Timeend}\n" +
                                $"ФИО: {selectedTicket.Fkpassport.Fname} {selectedTicket.Fkpassport.Lname}\n" +
                                $"Паспорт: {selectedTicket.Fkpassport.Number}";
            
            ticketDetail.IsVisible = true;
            returnButton.IsVisible = true;
            printButton.IsVisible = true;
        }
        else
        {
            ticketDetail.IsVisible = false;
            returnButton.IsVisible = false;
            printButton.IsVisible = false;
        }
    }

    private void ReturnTicket(object? sender, RoutedEventArgs e)
    {
        if (ticketsList.SelectedItem is Ticket selectedTicket)
        {
            Service.GetDbConnection().Tickets.Remove(selectedTicket);
            Service.GetDbConnection().SaveChanges();
            new ProfilWindow().Show();
            this.Close();
        }
    }

    private void PrintTicket(object? sender, RoutedEventArgs e)
    {
        if (ticketsList.SelectedItem is Ticket selectedTicket)
            PrintTicketPDF(selectedTicket);
    }
}