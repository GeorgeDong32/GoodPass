using CommunityToolkit.WinUI.UI.Controls;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Views;

public sealed partial class ListDetailsPage : Page
{
    public ListDetailsViewModel ViewModel
    {
        get;
    }

    public ListDetailsPage()
    {
        App.ListDetailsVM = new ListDetailsViewModel(App.GetService<GoodPassDataService>());
        ViewModel = App.ListDetailsVM;
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }

    public ListDetailsViewModel GetDetailsViewModel() => ViewModel;
}
