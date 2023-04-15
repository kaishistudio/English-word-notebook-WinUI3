using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace English_word_notebook_WinUI3.Services
{
    internal static class MessageBox
    {
        public async static void Show( string title, string content)
        {
            ContentDialog cd = new ContentDialog();
            cd.XamlRoot = App.MainWindow.Content.XamlRoot;
            cd.Title = title;
            cd.CloseButtonText = "OK";
            cd.Content = content;
            await cd.ShowAsync();
        }
        public async static void Show( string content)
        {
            ContentDialog cd = new ContentDialog();
            cd.XamlRoot = App.MainWindow.Content.XamlRoot;
            cd.Title = "";
            cd.CloseButtonText = "OK";
            cd.Content = content;
            await cd.ShowAsync();
        }
       
    }
}
