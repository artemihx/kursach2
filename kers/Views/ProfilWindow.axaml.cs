using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;

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
}