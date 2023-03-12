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
        InitializeComponent();
        if (OOBEServices.GetOOBEStatusAsync("ShellOOBE").Result == Models.OOBESituation.EnableOOBE)
        {
            OOBE_GoBackTip.IsOpen = true;
            OOBE_AddDataTip.IsOpen = true;
            OOBE_SettingTip.IsOpen = true;
        }
        if (OOBEServices.GetOOBEStatusAsync("SearchOOBE").Result == Models.OOBESituation.EnableOOBE)
        {
            OOBE_SearchTip.IsOpen = true;
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

        ShellMenuBarSettingsButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerPressed), true);
        ShellMenuBarSettingsButton.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerReleased), true);
        ShellMenuBarItem_Back.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerPressed), true);
        ShellMenuBarItem_Back.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerReleased), true);
        ShellMenuSearchButton.AddHandler(UIElement.PointerEnteredEvent, new PointerEventHandler(ShellAnimatedIcon_PointerEntered), true);
        ShellMenuSearchButton.AddHandler(UIElement.PointerExitedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerExited), true);
        ShellMenuSearchButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerPressed), true);
        ShellMenuSearchButton.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(ShellAnimatedIcon_PointerReleased), true);
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

        AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        ShellMenuBarSettingsButton.RemoveHandler(UIElement.PointerPressedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerPressed);
        ShellMenuBarSettingsButton.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerReleased);
        ShellMenuBarItem_Back.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerReleased);
        ShellMenuBarItem_Back.RemoveHandler(UIElement.PointerPressedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerPressed);
        ShellMenuSearchButton.RemoveHandler(UIElement.PointerEnteredEvent, (PointerEventHandler)ShellAnimatedIcon_PointerEntered);
        ShellMenuSearchButton.RemoveHandler(UIElement.PointerExitedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerExited);
        ShellMenuSearchButton.RemoveHandler(UIElement.PointerPressedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerPressed);
        ShellMenuSearchButton.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)ShellAnimatedIcon_PointerReleased);
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

    private void ShellAnimatedIcon_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "PointerOver");
    }

    private void ShellAnimatedIcon_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Pressed");
    }

    private void ShellAnimatedIcon_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Normal");
    }

    private void ShellAnimatedIcon_PointerExited(object sender, PointerRoutedEventArgs e)
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
            else if (addDataDialog.Result == Models.AddDataResult.Success)
            {
                await App.DataManager.SaveToFileAsync($"C:\\Users\\{Environment.UserName}\\AppData\\Local\\GoodPass\\GoodPassData.csv");
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
        _ = await OOBEServices.SetOOBEStatusAsync("ShellOOBE", Models.OOBESituation.DIsableOOBE);
    }

    private void ShellMenuSearchButton_Click(object sender, RoutedEventArgs e)
    {
        if (!App.App_IsLock() && !App.IsInSettingsPage())
        {
            ShellMenuSearchTip.IsOpen = true;
        }
    }

    /// <summary>
    /// Handle text change and present suitable items
    /// </summary>
    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Since selecting an item will also change the text,
        // only listen to changes caused by user entering text.
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suitableItems = new List<string>();
            var text = sender.Text;
            var matchDatas = App.DataManager.SuggestSearch(text);
            foreach (var data in matchDatas)
            {
                suitableItems.Add($"{data.PlatformName} - {data.AccountName}");
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;
        }
    }

    /// <summary>
    /// 处理用户选择的结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        var slectedItem = args.SelectedItem;
        if (slectedItem != null)
        {
            if (slectedItem.ToString() == "No results found")
            {
            }
            else
            {
                var splitText = slectedItem.ToString().Split(" - ");
                var platformName = splitText[0];
                var accountName = splitText[1];
                var index = App.DataManager.AccurateSearch(platformName, accountName);
                App.ListDetailsVM.GoToData(index);
            }
        }
        else
        {
            throw new ArgumentNullException("AutoSuggestBox_SuggestionChosen: SelectedItem is null");
        }
    }

    private async void OOBE_SearchTip_CloseButtonClick(TeachingTip sender, object args)
    {
        await OOBEServices.SetOOBEStatusAsync("SearchOOBE", Models.OOBESituation.DIsableOOBE);
    }

    /// <summary>
    /// 导出数据
    /// </summary>
    private async void Export_Click(object sender, RoutedEventArgs e)
    {
        if (App.App_IsLock() == false)
        {
            GPDialog2 confirmDialog = new()
            {
                XamlRoot = this.XamlRoot,
                Style = App.Current.Resources["DefaultContentDialogStyle"] as Style
            };
            confirmDialog.Title = "导出数据？";
            confirmDialog.Content = "点击确定即可导出数据";
            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var saveResult = await App.DataManager.SaveToFileAsync($"C:\\Users\\{Environment.UserName}\\Downloads\\GoodPassData.csv");
                if (saveResult == true)
                {
                    var infoDialog = new GPDialog2
                    {
                        XamlRoot = this.XamlRoot,
                        Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                        Title = "导出数据",
                        Content = "导出成功，请前往下载文件夹查看"
                    };
                    _ = await infoDialog.ShowAsync();
                }
            }
        }
    }
}
