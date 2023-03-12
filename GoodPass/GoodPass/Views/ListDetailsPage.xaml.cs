using CommunityToolkit.WinUI.UI.Controls;
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
        App.ListDetailsVM = new ListDetailsViewModel();
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

    private void ListDetailsViewControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
