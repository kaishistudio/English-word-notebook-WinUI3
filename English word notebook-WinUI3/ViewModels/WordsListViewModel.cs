using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

using English_word_notebook_WinUI3.Contracts.ViewModels;
using English_word_notebook_WinUI3.Core.Contracts.Services;
using English_word_notebook_WinUI3.Core.Models;
using English_word_notebook_WinUI3.Models;
using English_word_notebook_WinUI3.Services;
using Microsoft.UI.Xaml.Controls;

namespace English_word_notebook_WinUI3.ViewModels;

public class WordsListViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<Word> SampleItems;
    public ObservableCollection<Word> SelectedItems = Shares.Data.SelectWordItems;
    public WordsListViewModel(ISampleDataService sampleDataService)
    {
        SampleItems = Shares.Data.SampleItems;
        _sampleDataService = sampleDataService;
    }

    public  void OnNavigatedTo(object parameter)
    {
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected(int index)
    {
        if (index > -1)
        {
            Shares.Data.SelectWordItems.Clear();
            Shares.Data.SelectWordItems.Add(SampleItems[index]);
        }
    }
    public void TbTextChanged(string param)
    {
        Shares.Data.UpdateSampleItems(param);
    }
   
   public async void AddWordsDo()
    {
        ContentDialog cd = new ContentDialog();
        cd.XamlRoot = App.MainWindow.Content.XamlRoot;
        cd.Title = ReswSource.GetString("addwords_ask").Replace("{}",Shares.Data.AddWordsNum.ToString());
        cd.CloseButtonText = ReswSource.GetString("Cancle");
        cd.PrimaryButtonText = ReswSource.GetString("OK");
        cd.Content = "";
        cd.PrimaryButtonClick += (s, e) => {
            Shares.Data.AddDbWords();
            Shares.Data.UpdateDbWordItems();
        };
        await cd.ShowAsync();
    }
}
