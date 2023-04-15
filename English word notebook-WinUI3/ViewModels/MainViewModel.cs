using CommunityToolkit.Mvvm.ComponentModel;
using English_word_notebook_WinUI3.Models;
using English_word_notebook_WinUI3.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace English_word_notebook_WinUI3.ViewModels;

public class MainViewModel : ObservableRecipient
{

    public MainViewWord mainViewWord;
    public MainViewModel()
    {
        mainViewWord = Shares.Data.mainViewWord;
    }
    public void renshi_click()
    {
        mainViewWord.renshi = 1;
        Shares.Data.DbWordItems.First(o => o.word == mainViewWord.word).renshi = 1;
        try { Shares.Data.SampleItems.First(o => o.word == mainViewWord.word).renshi = 1; } catch { }
        Shares.Data.Sqlite_WordsUser.EditDBByCommand(
              $"update Data set renshi=1 where word Is '{mainViewWord.word}'");
        SetNextWord();
    }
    public void mohu_click()
    {
        mainViewWord.mohu += 1;
        Shares.Data.DbWordItems.First(o => o.word == mainViewWord.word).mohu += 1;
        try { Shares.Data.SampleItems.First(o => o.word == mainViewWord.word).mohu += 1; } catch { }
        Shares.Data.Sqlite_WordsUser.EditDBByCommand(
              $"update Data set mohu={mainViewWord.mohu.ToString()} where word Is '{mainViewWord.word}'");
        SetNextWord();
    }
    public void wangji_click()
    {
        mainViewWord.wangji = 1;
        Shares.Data.DbWordItems.First(o => o.word == mainViewWord.word).wangji = 1;
        try { Shares.Data.SampleItems.First(o => o.word == mainViewWord.word).wangji = 1; } catch { }
        Shares.Data.Sqlite_WordsUser.EditDBByCommand(
              $"update Data set wangji=1 where word Is '{mainViewWord.word}'");
        SetNextWord();
    }
    void SetNextWord()
    {
        var items = Shares.Data.SampleItems.Where(o=>o.renshi!=1&&o.mohu<Shares.Data.MohuMaxNum).ToList();
        var item = items[Shares.Data.random.Next(0, items.Count)];
        Shares.Data.NowWordIndex = Shares.Data.SampleItems.IndexOf( Shares.Data.SampleItems.First(o=>o.word==item.word));
        Shares.Data.UpdateViewWord();
    }
}
