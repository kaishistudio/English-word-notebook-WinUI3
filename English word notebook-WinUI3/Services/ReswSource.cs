﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace English_word_notebook_WinUI3.Services
{
    public static class ReswSource
    {
        public static string GetString(string key)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            return resourceLoader.GetString(key).ToString();
        }
    }
}
