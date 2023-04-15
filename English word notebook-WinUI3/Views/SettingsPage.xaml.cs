using English_word_notebook_WinUI3.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace English_word_notebook_WinUI3.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
        //
        var addwnt = ApplicationData.Current.LocalSettings.Values["AddWordsNum"].ToString();
        double addwn;
        var a=double.TryParse(addwnt,out addwn);
        ntb_addwordsnum.Value = a ? addwn : 50;
        //
        var MohuMaxNumt = ApplicationData.Current.LocalSettings.Values["MohuMaxNum"].ToString();
        double MohuMaxNum;
        var b = double.TryParse(MohuMaxNumt, out MohuMaxNum);
        ntb_mohumaxnum.Value = b ? MohuMaxNum : 10;
    }

    private void ntb_addwordsnum_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        Shares.Data.AddWordsNum = (int)ntb_addwordsnum.Value;
        ApplicationData.Current.LocalSettings.Values["AddWordsNum"] = ntb_addwordsnum.Value.ToString();
    }

    private void ntb_mohumaxnum_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        Shares.Data.MohuMaxNum = (int)ntb_mohumaxnum.Value;
        ApplicationData.Current.LocalSettings.Values["MohuMaxNum"] = ntb_mohumaxnum.Value.ToString();
    }
}
