using Microsoft.UI.Xaml.Controls;

namespace English_word_notebook_WinUI3.Helpers;

public static class FrameExtensions
{
    public static object? GetPageViewModel(this Frame frame) => frame?.Content?.GetType().GetProperty("ViewModel")?.GetValue(frame.Content, null);
}
