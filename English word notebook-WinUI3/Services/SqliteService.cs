using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace English_word_notebook_WinUI3.Services;
public class SqliteService
{
    public SqliteConnection db;
    public async Task<bool> ReadDBByName(string dbname)
    {
        bool IsSucess;
        try
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync($"{dbname}.db");
            db = new SqliteConnection($"Filename={file.Path}");
            IsSucess = true;
        }
        catch
        {
            IsSucess = false;
        }
        return IsSucess;
    }
    /// <summary>
    /// "{name}.db" "Name text,Url text,Date text"
    /// </summary>
    /// <param name="name"></param>
    public async Task<bool> CreatDBByName(string dbname, string tablename, string columns)
    {
        bool Isexist;
        StorageFile file;
        try
        {
            file = await ApplicationData.Current.LocalFolder.CreateFileAsync($"{dbname}.db", CreationCollisionOption.FailIfExists);
            Isexist = false;
        }
        catch
        {
            file = await ApplicationData.Current.LocalFolder.CreateFileAsync($"{dbname}.db", CreationCollisionOption.OpenIfExists);
            Isexist = true;
        }
        db = new SqliteConnection($"Filename={file.Path}");
        try
        {
            db.Open();
            var tableCommand = $"Create table {tablename} ({columns})";
            SqliteCommand createTable = new SqliteCommand(tableCommand, db);
            createTable.ExecuteReader();
        }
        catch { }
        return Isexist;
    }
    /// <summary>
    /// "{name}.db"
    /// </summary>
    /// <param name="name"></param>
    public async void DeleteDBByName(string name)
    {
        var f = await ApplicationData.Current.LocalFolder.GetFileAsync($"{name}.db");
        await f.DeleteAsync();
    }
    /// <summary>
    /// command:SELECT * from table ORDER BY ID"
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public SqliteDataReader EditDBByCommand(string command)
    {
        db.Open();
        SqliteCommand selectCommand = new SqliteCommand(command, db);
        SqliteDataReader query = selectCommand.ExecuteReader();
        return query;
    }
    /// <summary>
    /// columns:Id , Name values:1,'Mike'
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="columns"></param>
    /// <param name="value"></param>
    public void InsertColumn(string tablename, string columns, string values)
    {
        db.Open();
        var tableCommand = $"insert into {tablename} (columns) values(values)";
        SqliteCommand createTable = new SqliteCommand(tableCommand, db);
        createTable.ExecuteReader();
    }
    /// <summary>
    /// where:id=1,value='mike',SqliteDataReader.GetString(0),调用后执行SqliteService.
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public SqliteDataReader ReadTableData(string tablename, string column, string orderby, string where)
    {
        if (db != null)
        {
            db.Open();
            string orderbyt = "";
            if (orderby != "") orderbyt = " order by " + orderby;
            string wheret = "";
            if (where != "") wheret = " where " + where;
            SqliteCommand selectCommand = new SqliteCommand($"Select {column} From {tablename}{orderby}{wheret}", db);
            SqliteDataReader query = selectCommand.ExecuteReader();
            return query;
        }
        else { return null; }
        
    }
    /// <summary>
    /// where:id=1,value='mike'
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <param name="whrer"></param>
    /// <returns></returns>
    public SqliteDataReader UpdateTableData(string tablename, string column, string value, string where)
    {
        db.Open();
        SqliteCommand selectCommand = new SqliteCommand($"update {tablename} set {column}={value} where {where}", db);
        SqliteDataReader query = selectCommand.ExecuteReader();

        return query;
    }
    /// <summary>
    /// where:id=1,value='mike' where="":Clear all table data
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="where"></param>
    public void DeleteTableData(string tablename, string where)
    {
        db.Open();
        var wheret = "";
        if (where != "") wheret = $" where {where}";
        SqliteCommand selectCommand = new SqliteCommand($"delete from {tablename}{wheret}", db);
        SqliteDataReader query = selectCommand.ExecuteReader();

    }
}
