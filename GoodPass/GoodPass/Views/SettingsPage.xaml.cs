using GoodPass.Dialogs;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var dialog = new OOBEAgreementsDialog()
        {
            XamlRoot = this.XamlRoot,
            Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
            Title = App.UIStrings.OOBEAgreementsDialogTitle,
        };
        _ = await dialog.ShowAsync();
    }
}
