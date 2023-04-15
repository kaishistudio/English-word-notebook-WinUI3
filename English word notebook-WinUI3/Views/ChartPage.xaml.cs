using English_word_notebook_WinUI3.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.UI.Xaml.Controls;

namespace English_word_notebook_WinUI3.Views;

public sealed partial class ChartPage : Page
{
    public ChartViewModel ViewModel
    {
        get;
    }

    public ChartPage()
    {
        ViewModel = App.GetService<ChartViewModel>();
        InitializeComponent(); 
    }
    void ChartInit()
    {
        var db = Shares.Data.Sqlite_WordsList.db;
        var db2 = Shares.Data.Sqlite_WordsUser.db;
        db.Open();
        var command = new SqliteCommand(@"SELECT COUNT(*) FROM wordlist", db);
        var count0 = (long)command.ExecuteScalar();
        //
        var query1 = new SqliteCommand(@"SELECT COUNT(*) FROM Data WHERE wangji > 0", db2); 
        var count1 = (long)query1.ExecuteScalar();
        img_renshi.Height = (g_renshi.ActualHeight - 30) * count1 / count0;
        //
        var query2 = new SqliteCommand(@"SELECT COUNT(*) FROM Data WHERE mohu > 0", db2);
        var count2 = (long)query2.ExecuteScalar();
        img_mohu.Height = (g_renshi.ActualHeight - 30) * count2 / count0;
        //
        var query3 = new SqliteCommand(@"SELECT COUNT(*) FROM Data WHERE wangji > 0", db2);
        var count3 = (long)query3.ExecuteScalar();
        img_wangji.Height = (g_renshi.ActualHeight - 30) * count3 / count0;
    }

    private void ContentArea_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ChartInit();

    }
}
