using GoodPass.Models;
using GoodPass.Services;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Dialogs;

public sealed partial class OOBEAgreementsDialog : ContentDialog
{
    public AgreeStatus AgreeStatus
    {
        get; set;
    }

    public OOBEAgreementsDialog()
    {
        this.InitializeComponent();
        IsPrimaryButtonEnabled = false;
        AgreeStatus = AgreeStatus.Agree;
    }

    private bool IsAgreeAllChecked()
    {
        if (PrivacyTermsCheck.IsChecked == true && UserAgreementCheck.IsChecked == true)
            return true;
        else
            return false;
    }

    private void OOBEDialogCheck_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (IsAgreeAllChecked())
            IsPrimaryButtonEnabled = true;
    }

    private void OOBEDialogCheck_UnChecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        IsPrimaryButtonEnabled = false;
    }

    private async void OOBEAgreementsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        _ = await OOBEServices.SetOOBEStatusAsync("AgreementOOBE", Models.OOBESituation.DIsableOOBE);
    }

    private void OOBEAgreementsDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        this.AgreeStatus = AgreeStatus.NotAgree;
    }
}