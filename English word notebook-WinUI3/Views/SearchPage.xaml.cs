using English_word_notebook_WinUI3.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace English_word_notebook_WinUI3.Views;

public sealed partial class SearchPage : Page
{
    public SearchViewModel ViewModel
    {
        get;
    }

    public SearchPage()
    {
        ViewModel = App.GetService<SearchViewModel>();
        InitializeComponent();
    }


    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.SearchDo(((TextBox)sender).Text);
    }
}
