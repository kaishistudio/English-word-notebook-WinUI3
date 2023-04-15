using English_word_notebook_WinUI3.Services;
using English_word_notebook_WinUI3.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace English_word_notebook_WinUI3.Views;

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

    private void bn_renshi_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.renshi_click();
    }
    private void bn_mohu_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.mohu_click();
    }
    private void bn_wangji_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.wangji_click();
    }
}
