using GoodPass.Helpers;

namespace GoodPass;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/GPico128.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
    }
}
