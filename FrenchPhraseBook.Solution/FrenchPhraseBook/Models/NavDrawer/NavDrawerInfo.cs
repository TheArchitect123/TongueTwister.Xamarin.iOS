using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FrenchPhraseBook.Models.NavDrawer
{
    public class NavDrawerInfo
    {
        /// <summary>
        /// The selected index of the nav drawer
        /// </summary>
        public static int SelectedIndex { get; set; }

    }
}