using Android.Support.V4.App;
using Android.Content.Res;
using Android.Graphics;
using Java.IO;
using Android.Support.V7.Widget;
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

using Source = Android.Net;

//Runtime Conversions
using FrenchPhraseBook.UI.Generic;
using Android.Util;

namespace FrenchPhraseBook.UI.Components.NavigationDrawer
{
    public class AGNavDrawer : FragmentActivity
    {
        #region Views
        /// <summary>
        /// 
        /// </summary>
        public View NavDrawer { get; set; }

        /// <summary>
        /// The background view
        /// </summary>
        public ImageView BackgroundView { get; set; }

        /// <summary>
        /// The reycle view used to render the table view
        /// </summary>
        public RecyclerView TableView { get; set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        public TextView Username { get; set; }

        /// <summary>
        ///  The first character of the Icon name
        /// </summary>
        public TextView IconName { get; set; }

        #endregion

        #region Layouts
        /// <summary>
        /// The main layout used on the navigation drawer
        /// </summary>
        LinearLayout MainLayout { get; set; }

        /// <summary>
        /// The layout used on the background view
        /// </summary>
        RelativeLayout.LayoutParams BackgroundLayout { get; set; }

        #endregion

        #region Activities
        public Activity MainActivity { get; set; }

        #endregion

        public override View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
         

            //Main Layout
            this.MainLayout = new LinearLayout(this.MainActivity);
            this.MainLayout.LayoutParameters = new LinearLayout.LayoutParams((int)AndroidConversions.PixeltoDp(200, this.MainActivity), LinearLayout.LayoutParams.MatchParent);
            this.MainLayout.SetBackgroundColor(Color.White);
            this.MainLayout.Elevation = (int)AndroidConversions.PixeltoDp(20, this.MainActivity);
            this.MainLayout.Orientation = Android.Widget.Orientation.Vertical;
            this.MainLayout.SetFitsSystemWindows(true);

            //Background View 
            this.BackgroundLayout = new RelativeLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, (int)AndroidConversions.PixeltoDp(200, this.MainActivity));

            #region Views

            this.BackgroundView = new ImageView(this.MainActivity);
            this.BackgroundView.SetImageResource(Resource.Drawable.NavDrawer);
            this.BackgroundView.LayoutParameters = this.BackgroundLayout;

            //The layout to be used for text data
            RelativeLayout.LayoutParams usernameParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.WrapContent);
            usernameParams.LeftMargin = (int)AndroidConversions.PixeltoDp(10, this.MainActivity);
            usernameParams.RightMargin = (int)AndroidConversions.PixeltoDp(10, this.MainActivity);
            usernameParams.BottomMargin = (int)AndroidConversions.PixeltoDp(30, this.MainActivity);

            TextView username = new TextView(this.MainActivity);

            this.Username.LayoutParameters = usernameParams;
            this.Username.TextSize = 12.0f;
            this.Username.SetTextColor(Color.White);
            this.Username.Id = 123321;
            this.Username = username;

            RelativeLayout.LayoutParams iconNameParams = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.WrapContent);
            iconNameParams.LeftMargin = (int)AndroidConversions.PixeltoDp(10, this.MainActivity);
            iconNameParams.RightMargin = (int)AndroidConversions.PixeltoDp(10, this.MainActivity);
            iconNameParams.BottomMargin = (int)AndroidConversions.PixeltoDp(20, this.MainActivity);

            TextView iconName = new TextView(this.MainActivity);

            this.IconName.SetBackgroundColor(Color.Orange);
            this.IconName.LayoutParameters = iconNameParams;
            this.IconName.SetTextColor(Color.White);
            this.IconName.TextSize = 18.0f;
            this.IconName.Id = 372171;
            this.IconName = iconName;



            usernameParams.AddRule(LayoutRules.AlignParentBottom, 123321);
            iconNameParams.AddRule(LayoutRules.Above, 372171);

            this.MainLayout.AddView(this.BackgroundView);
            this.MainLayout.AddView(this.Username);
            this.MainLayout.AddView(this.IconName);


            #endregion

            return null;

        }

        public AGNavDrawer(Activity activity) 
        {
            this.MainActivity = activity;

        }
    }
}