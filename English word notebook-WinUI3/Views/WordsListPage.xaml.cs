using CommunityToolkit.WinUI.UI.Controls;

using English_word_notebook_WinUI3.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace English_word_notebook_WinUI3.Views;

public sealed partial class WordsListPage : Page
{
    public WordsListViewModel ViewModel
    {
        get;
    }

    public WordsListPage()
    {
        ViewModel = App.GetService<WordsListViewModel>();
        InitializeComponent();
    }


    private void ListDetailsViewControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       // ViewModel.EnsureItemSelected(ListDetailsViewControl.SelectedIndex);

    }
 private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
       ViewModel.TbTextChanged(((TextBox)sender).Text);
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.AddWordsDo();
    }

    private void lv_apps_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
