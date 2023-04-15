using System.Diagnostics;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

using English_word_notebook_WinUI3.Contracts.Services;
using English_word_notebook_WinUI3.Models;
using English_word_notebook_WinUI3.Services;
using English_word_notebook_WinUI3.Views;

using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;

namespace English_word_notebook_WinUI3.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected
    {
        get => _selected;
        set  { SetProperty(ref _selected, value);
        }
    }

    public MainViewWord mainViewWord;
    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        mainViewWord = Shares.Data.mainViewWord;
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        Shares.Data.ReadLocalSettingsData();
        Shares.Data.ReadDataInit();
        Shares.Data.UpdateDbWordItems();
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }
        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }
    
}
