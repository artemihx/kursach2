using System;
using System.Linq;
using System.Linq.Expressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using kers.Models;
using kers.ViewModels;
using static kers.Core;

namespace kers.Views;

public partial class MainWindow : Window
{
    private TextBox loginTbox;
    private TextBox passwordTBox;
    public MainWindow()
    {
        InitializeComponent();

        loginTbox = this.LoginTbox;
        passwordTBox = this.PasswordTbox;
    }
    
    public void ToRegisterClick(object sender, RoutedEventArgs args)
    {
        new RegisterWindow().Show();
        this.Close();
    }

    public void AuthClick(object sender, RoutedEventArgs args)
    {
        if (!string.IsNullOrWhiteSpace(loginTbox.Text) && !string.IsNullOrWhiteSpace(passwordTBox.Text))
        {
            User user = Service.GetDbConnection().Users
                .Where(u => u.Login == loginTbox.Text && u.Password == HashPassword(passwordTBox.Text)).FirstOrDefault();
            if (user != null)
            {
                new ClRoutesWindow().Show();
                this.Close();
            }
            else
            {
                this.Title.Text = "ОшибкаПИздец";
            }
        }
    }
}