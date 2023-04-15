using English_word_notebook_WinUI3.Helpers;
using English_word_notebook_WinUI3.Services;
using Windows.Storage;

namespace English_word_notebook_WinUI3;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
    }
}
