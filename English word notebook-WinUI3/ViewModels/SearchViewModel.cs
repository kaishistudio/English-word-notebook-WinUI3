using CommunityToolkit.Mvvm.ComponentModel;
using English_word_notebook_WinUI3.Models;
using System.Collections.ObjectModel;

namespace English_word_notebook_WinUI3.ViewModels;

public class SearchViewModel : ObservableRecipient
{
    public ObservableCollection<Word> SampleItems = new();
    public SearchViewModel()
    {
    }
    public void SearchDo(string t)
    {
        try
        {
            if (t != "")
            {
                SampleItems.Clear();
                var query = Shares.Data.Sqlite_WordsList.EditDBByCommand(
                    $"SELECT * FROM wordlist WHERE word LIKE '%{t.ToLower()}%' OR shiyi LIKE '%{t.ToLower()}%' "
                    );
                while (query.Read())
                {
                    var word = query.GetString(0);
                    var shuxing = query.IsDBNull(1) ? "" : query.GetString(1);
                    var shiyi = query.IsDBNull(2) ? "" : query.GetString(2);
                    SampleItems.Add(new Word()
                    {
                        word = word,
                        shuxing = shuxing,
                        shiyi = shiyi,
                    });
                }
            }
        }
        catch { }
    }
}
