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

namespace FrenchPhraseBook.UI.Components.CardView
{
    public class AGCardView
    {

        #region Views
        /// <summary>
        /// The category text view
        /// </summary>
        public TextView Category { get; set; }

        /// <summary>
        /// /The category view used to render the navigation drawer
        /// </summary>
        public ImageView CategoryView { get; set; }
        #endregion

        public AGCardView(ViewGroup layout)
        {




        }


    }
}