using GoodPass.Services;
using GoodPass.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactions.Core;

namespace GoodPass.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void Login_Check_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var MKCheck_Result = "pass";
        //添加解锁逻辑
        if (MKCheck_Result=="pass")
        {
            App.App_UnLock();
            ViewModel.Login_UnLock();
        }
        else if (MKCheck_Result=="npass")
        {
        }
        else
        {

        }
    }

    //private void UnLock()=> NavigationService.NavigateTo(typeof(ListDetailsViewModel).FullName!);
}
