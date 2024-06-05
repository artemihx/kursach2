using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;
using Org.BouncyCastle.Asn1.Cms;

namespace kers.Views;

public partial class AddTripWindow : Window
{
    private Route selectedRoute;
    private ComboBox comboBoxAuto;
    private DatePicker selectedDate;
    private TimePicker selectedTime;
    public AddTripWindow(Route _selectedRoute)
    {
        InitializeComponent();
        this.selectedRoute = _selectedRoute;
        this.comboBoxAuto = this.Find<ComboBox>("ComboBox");
        this.selectedDate = this.Find<DatePicker>("DatePicker");
        this.selectedTime = this.Find<TimePicker>("TimePicker");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        this.DataContext = new AddTripViewModel();
        AvaloniaXamlLoader.Load(this);
    }

    private void AddTrip(object? sender, RoutedEventArgs e)
    {
        if (comboBoxAuto.SelectedItem is Auto selectedAuto)
        {
            try
            {
                DateTime selectedDateTime = selectedDate.SelectedDate.Value.Date;
                selectedDateTime += selectedTime.SelectedTime.Value;
                Trip newTrip = new Trip()
                {
                    Routeid = selectedRoute.Id,
                    Autoid = selectedAuto.Id,
                    Timestart = selectedDateTime
                };
                Service.GetDbConnection().Trips.Add(newTrip);
                Service.GetDbConnection().SaveChanges();
                new EmployeeWindow().Show();
                this.Close();
            }
            catch (Exception exception)
            {
            }
        }
    }

    private void Exit(object? sender, RoutedEventArgs e)
    {
        new EmployeeWindow().Show();
        this.Close();
    }
}