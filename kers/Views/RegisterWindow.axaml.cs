using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Security.Cryptography;
using System.Text;
using kers.Models;
using static kers.Core;

namespace kers.Views;

public partial class RegisterWindow : Window
{
    private TextBox loginTbox;
    private TextBox passwordTbox;
    public RegisterWindow()
    {
        InitializeComponent();

        loginTbox = this.LoginTBox;
        passwordTbox = this.PasswordTBox;
    }
    
    public void ReturnClick(object sender, RoutedEventArgs args)
    {
        new MainWindow().Show();
        this.Close();
    }

    public void RegisterButton(object sender, RoutedEventArgs args)
    {
        if (!string.IsNullOrWhiteSpace(loginTbox.Text) && !string.IsNullOrWhiteSpace(passwordTbox.Text))
        {
            var user = new User();
            if (Service.GetDbConnection().Users.Where(u => u.Login == loginTbox.Text).FirstOrDefault() == null)
            {
                user.Login = LoginTBox.Text;
                user.Password = HashPassword(passwordTbox.Text);
                Service.GetDbConnection().Users.Add(user);
                Service.GetDbConnection().SaveChanges();
            }
            new MainWindow().Show();
            this.Close();
        }
    }
}