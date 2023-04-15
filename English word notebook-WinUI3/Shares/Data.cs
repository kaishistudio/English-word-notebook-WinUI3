using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using English_word_notebook_WinUI3.Models;
using English_word_notebook_WinUI3.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using Windows.Storage;

namespace English_word_notebook_WinUI3.Shares;
public class Data 
{
    public static SqliteService Sqlite_WordsList;//总词汇数据库
    public static SqliteService Sqlite_WordsUser;//用户背诵词汇数据库
    public static ObservableCollection<Word> DbWordItems=new();//应用内背诵词汇列表
    public static ObservableCollection<Word> SelectWordItems=new();//选择词汇列表
    public static ObservableCollection<Word> SampleItems = new ();//当前显示列表
    public static int NowWordIndex=0;//当前词汇索引
    public static int AddWordsNum=50;//一次添加的词汇数
    public static int MohuMaxNum = 10;//模糊多少次算认识
    public static Random random = new Random();
    public static MainViewWord mainViewWord=new();
    /// <summary>
    /// 初始化数据库
    /// </summary>
    public static async void ReadDataInit()
    {
        //单词数据库创建
        StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/wordlist.db"));
        var file1 = await ApplicationData.Current.LocalFolder.TryGetItemAsync("wordlist.db") as StorageFile;
        if (file1 == null)
        {
            await file.CopyAsync(ApplicationData.Current.LocalFolder);
        }
        Shares.Data.Sqlite_WordsList = new SqliteService();
        await Shares.Data.Sqlite_WordsList.ReadDBByName("wordlist");
        //用户数据数据库创建
        Shares.Data.Sqlite_WordsUser = new SqliteService();
        bool IsExist = await Shares.Data.Sqlite_WordsUser.CreatDBByName(
            "UserData", "Data", "word TEXT,shuxing TEXT,shiyi TEXT,renshi INTEGER,mohu INTEGER,wangji INTEGER"
            );
        //如果数据库第一次创建就加入50单词
        if (!IsExist)
        {
            Shares.Data.AddDbWords();
        }
    }
    /// <summary>
    /// 向Sqlite_WordsList中添加新单词
    /// </summary>
    public static void AddDbWords()
    {
        var query = Sqlite_WordsList.EditDBByCommand(
            $"SELECT * FROM wordlist WHERE word IS NOT NULL AND IsRead is not 1 ORDER BY RANDOM() LIMIT {AddWordsNum} "
            );
        while (query.Read())
        {
            var word = query.GetString(0);
            var shuxing = query.IsDBNull(1) ? "" : query.GetString(1);
            var shiyi = query.IsDBNull(2) ? "" : query.GetString(2);
            
            Sqlite_WordsList.EditDBByCommand(
                $"update wordlist set IsRead=1 where word Is '{word}'");
            Sqlite_WordsUser.EditDBByCommand(
                $"insert into Data (word,shuxing,shiyi,renshi,mohu,wangji) values('{word}','{shuxing}','{shiyi}',0,0,0)");
            
        }
    }
    /// <summary>
    /// 更新DbWordItems的数据从Sqlite_WordsUser
    /// </summary>
    public static void UpdateDbWordItems()
    {
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = new TimeSpan(1);
        timer.Tick += (s, e) =>
        {
            try
            {
                Shares.Data.DbWordItems.Clear();
                var query = Shares.Data.Sqlite_WordsUser.ReadTableData("Data", "word,shuxing,shiyi,renshi,mohu,wangji", "", "");
                if (query != null)
                {
                    while (query.Read())
                    {
                        var item = new Word()
                        {
                            word = query.GetString(0),
                            shuxing = query.GetString(1),
                            shiyi = query.GetString(2),
                            renshi = query.GetInt32(3),
                            mohu = query.GetInt32(4),
                            wangji = query.GetInt32(5),
                        };
                        Shares.Data.DbWordItems.Add(item);
                    }
                    UpdateViewWord();
                    UpdateSampleItems("");
                    timer.Stop();
                }
            }
            catch { }
        };
        timer.Start();
    }
    /// <summary>
    /// 更新Mainview页
    /// </summary>
    public static void UpdateViewWord()
    {
        if (Shares.Data.DbWordItems.Count > 0 || Shares.Data.NowWordIndex < Shares.Data.DbWordItems.Count)
        {
            var item = Shares.Data.DbWordItems[Shares.Data.NowWordIndex];
            mainViewWord.word = item.word;
            mainViewWord.shuxing = item.shuxing;
            mainViewWord.shiyi = item.shiyi;
            mainViewWord.renshi = item.renshi;
            mainViewWord.mohu = item.mohu;
            mainViewWord.wangji = item.wangji;
            mainViewWord.IsIndeterminate = false;
        }
    }
    /// <summary>
    /// 更新SampleItems
    /// </summary>
    /// <param name="param"></param>
    public static void UpdateSampleItems(string param)
    {
        try
        {
            SampleItems.Clear();
            var list = Shares.Data.DbWordItems.Where(o => o.word.ToLower().Contains(param.ToLower()) || o.shiyi.ToLower().Contains(param.ToLower())).ToArray();
            foreach (var l in list)
            {
                SampleItems.Add(l);
            }
        }
        catch { }
    }
    public static void ReadLocalSettingsData() {
        try
        {
            //
            var addwnt = ApplicationData.Current.LocalSettings.Values["AddWordsNum"].ToString();
            double addwn;
            var a = double.TryParse(addwnt, out addwn);
            AddWordsNum = a ? (int)addwn : 50;
            //
            var MohuMaxNumt = ApplicationData.Current.LocalSettings.Values["MohuMaxNum"].ToString();
            double MohuMaxNum;
            var b = double.TryParse(MohuMaxNumt, out MohuMaxNum);
            MohuMaxNum = b ? (int)MohuMaxNum : 10;
        }
        catch { }
    }
}
