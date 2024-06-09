using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kers.Models;
using kers.ViewModels;

namespace kers.Views;

public partial class ChangeStatusWindow : Window
{
    private Trip selectedTrip { get; set; }
    private ComboBox StatusComboBox;
    public ChangeStatusWindow(Trip _selectedTrip)
    {
        this.selectedTrip = _selectedTrip;
        InitializeComponent();
        this.StatusComboBox = this.Find<ComboBox>("ComboBoxStasus");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        this.DataContext = new ChangeStatusWindowViewModel(this.selectedTrip);
    }

    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        if (StatusComboBox.SelectedItem is Status selectedStatus)
        {
            selectedTrip.Status = selectedStatus;
            Service.GetDbConnection().SaveChanges();
            this.Close();
        }
        
    }
}