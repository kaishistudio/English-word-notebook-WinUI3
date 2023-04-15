using English_word_notebook_WinUI3.Core.Models;
using English_word_notebook_WinUI3.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace English_word_notebook_WinUI3.Views;

public sealed partial class WordsListDetailControl : UserControl
{
    public SampleOrder? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as SampleOrder;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(SampleOrder), typeof(WordsListDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public WordsListViewModel ViewModel
    {
        get;
    }
    public WordsListDetailControl()
    {
        ViewModel = App.GetService<WordsListViewModel>();
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WordsListDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
