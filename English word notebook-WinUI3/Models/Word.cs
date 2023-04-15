using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;

namespace English_word_notebook_WinUI3.Models;
public class Word : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private string _word;
    public string word
    {
        set
        {
            _word = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(word)));
        }
        get
        {
            return _word;
        }
    }
    private string _shuxing;
    public string shuxing
    {
        set
        {
            _shuxing = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(shuxing)));
        }
        get
        {
            return _shuxing;
        }
    }
    private string _shiyi;
    public string shiyi
    {
        set
        {
            _shiyi = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(shiyi)));
        }
        get
        {
            return _shiyi;
        }
    }
    private int _renshi;
    public int renshi
    {
        set
        {
            _renshi = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(renshi)));
        }
        get
        {
            return _renshi;
        }
    }
    private int _mohu;
    public int mohu
    {
        set
        {
            _mohu = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(mohu)));
        }
        get
        {
            _mohu = wangji == 1 ? _mohu =0 : _mohu;
            _mohu = renshi == 1 ? _mohu = Shares.Data.MohuMaxNum:_mohu;
            return _mohu;
        }
    }
    private int _wangji;
    public int wangji
    {
        set
        {
            _wangji = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(wangji)));
        }
        get
        {
            return _wangji;
        }
    }
    private double _maxpbnum;
    public double maxpbnum
    {
        set
        {
            _maxpbnum = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(maxpbnum)));
        }
        get
        {
            _maxpbnum = Shares.Data.MohuMaxNum;
            return _maxpbnum;
        }
    }
}
