using GoodPass.Contracts.Services;
using GoodPass.Dialogs;
using GoodPass.Helpers;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.System;

namespace GoodPass.Views;

public sealed partial class ShellPage : Page
{
    public ShellViewModel ViewModel
    {
        get;
    }

    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        App.UIStrings = App.GetService<MultilingualStringsServices>().Getzh_CN();
        App.MainOOBE = App.GetService<OOBEServices>().GetOOBEStatusAsync("MainOOBE").Result;
        App.ShellOOBE = App.GetService<OOBEServices>().GetOOBEStatusAsync("ShellOOBE").Result;
        App.AgreementOOBE = App.GetService<OOBEServices>().GetOOBEStatusAsync("AgreementOOBE").Result;
        InitializeComponent();
        if (App.ShellOOBE == Models.OOBESituation.EnableOOBE)
        {
            OOBE_GoBackTip.IsOpen = true;
            OOBE_AddDataTip.IsOpen = true;
            OOBE_SettingTip.IsOpen = true;
        }

        ViewModel.NavigationService.Frame = NavigationFrame;

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.MainWindow.Activated += MainWindow_Activated;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(RequestedTheme);

        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));

        ShellMenuBarSettingsButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ShellMenuBarSettingsButton_PointerPressed), true);
        ShellMenuBarSettingsButton.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(ShellMenuBarSettingsButton_PointerReleased), true);
        ShellMenuBarItem_Back.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ShellMenuBarItem_Back_PointerPressed), true);
        ShellMenuBarItem_Back.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(ShellMenuBarItem_Back_PointerReleased), true);
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

        AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        ShellMenuBarSettingsButton.RemoveHandler(UIElement.PointerPressedEvent, (PointerEventHandler)ShellMenuBarSettingsButton_PointerPressed);
        ShellMenuBarSettingsButton.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)ShellMenuBarSettingsButton_PointerReleased);
        ShellMenuBarItem_Back.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)ShellMenuBarItem_Back_PointerPressed);
        ShellMenuBarItem_Back.RemoveHandler(UIElement.PointerPressedEvent, (PointerEventHandler)ShellMenuBarItem_Back_PointerPressed);
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void ShellMenuBarSettingsButton_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "PointerOver");
    }

    private void ShellMenuBarSettingsButton_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Pressed");
    }

    private void ShellMenuBarSettingsButton_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Normal");
    }

    private void ShellMenuBarSettingsButton_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Normal");
    }

    private void ShellMenuBarItem_Back_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Pressed");
    }

    private void ShellMenuBarItem_Back_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Normal");
    }

    /// <summary>
    /// 添加数据按钮的事件响应
    /// </summary>
    private async void ShellMenuBarAddDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (App.App_IsLock())
        {
            ShellMenuBarAddDataButton.Flyout.ShowAt(ShellMenuBarSettingsButton);
        }
        else if (App.App_IsLock() == false)
        {
            ShellMenuBarAddDataButton.Flyout.Hide();
            AddDataDialog addDataDialog = new()
            {
                XamlRoot = this.XamlRoot,
                Style = App.Current.Resources["DefaultContentDialogStyle"] as Style
            };
            var result = await addDataDialog.ShowAsync();
            if (addDataDialog.Result == Models.AddDataResult.Failure_Duplicate)
            {
                GPDialog2 warningdialog = new()
                {
                    XamlRoot = this.XamlRoot,
                    Style = App.Current.Resources["DefaultContentDialogStyle"] as Style
                };
                warningdialog.Title = "出错了！";
                warningdialog.Content = "数据重复，请前往修改已存在的数据";
                _ = await warningdialog.ShowAsync();
            }
        }
        else
        {
            ShellMenuBarAddDataButton.Flyout.ShowAt(ShellMenuBarSettingsButton);
        }
    }

    private async void OOBE_AddDataTip_CloseButtonClick(TeachingTip sender, object args)
    {
        OOBE_AddDataTip.IsOpen = false;
        _ = await App.GetService<OOBEServices>().SetOOBEStatusAsync("ShellOOBE", Models.OOBESituation.DIsableOOBE);
    }
}
