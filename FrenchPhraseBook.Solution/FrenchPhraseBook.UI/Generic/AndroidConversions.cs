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

namespace FrenchPhraseBook.UI.Generic
{
    public class AndroidConversions
    {
        /// <summary>
        /// Convert pixels to dp
        /// </summary>
        /// <param name="pixel"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static double PixeltoDp(int pixel, Context context)
        {
            var density = context.Resources.DisplayMetrics.Density;

            return (double)pixel / density;
        }

    }
}