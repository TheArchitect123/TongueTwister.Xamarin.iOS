using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using Android.Util;

using Android.Widget;

using Android.Support.V7.Widget;
using Android.Support.Design.Widget;

using Android.Graphics;
using Android.Graphics.Drawables;

using Android.Hardware.Display;

#region Conversions 
using FrenchPhraseBook.Generic.Conversion;

#endregion

#region Support Frameworks
using Android.Support.V4.Widget;

#endregion

#region Adapters
using FrenchPhraseBook.Adapters.Favourites;

//Nav Drawer
using FrenchPhraseBook.Adapters.Components.NavDrawer;

using FrenchPhraseBook.Models.Speech;
#endregion

#region Data Sources
using FrenchPhraseBook.Data_Sources.Main_Page;

using FrenchPhraseBook.Backend.Storage;


#endregion

#region SQLite
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

using FrenchPhraseBook.Backend.Storage.Models.Database;

#endregion

namespace FrenchPhraseBook
{
    [Activity(Label = "Favourites")]
    public class Favourites_Activity : Activity
    {
        #region Layouts 

        /// <summary>
        /// The navigation drawer's layout
        /// </summary>
        public LinearLayout NavDrawer_Layout { get; set; }

        #endregion


        #region Views

        /// <summary>
        /// The navigation drawer background
        /// </summary>
        public ImageView NavDrawerBackground { get; set; }

        /// <summary>
        /// The table view used to draw the navigation drawer options
        /// </summary>
        public RecyclerView NavDrawerSource { get; set; }

        /// <summary>
        /// The username to use on the navigation drawer
        /// </summary>
        public TextView Username { get; set; }

        /// <summary>
        /// The icon name 
        /// </summary>
        public TextView IconName { get; set; }

        #endregion

        #region Models

        public bool isDrawerOpen { get; set; }
        #endregion

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.StandardMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (featureId)
            {
                case 0:
                    Console.WriteLine("Item 1 Selected");

                    break;
                case 1:
                    Console.WriteLine("Item 2 Selected");

                    break;

                case 2:
                    Console.WriteLine("Item 3 Selected");

                    break;
            }

            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Acvtion Bar
          
            //    this.SetActionBar(actionBar);

            //   this.ActionBar.SetHomeButtonEnabled(true);


            this.SetContentView(Resource.Layout.MainPageScene);

            var actionBar = FindViewById<TextView>(Resource.Id.ToolbarTitle);
            actionBar.Text = "Favourites";

            CRUDOperations.GetFavourites(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));


            #region Content 
            var contentPage = FindViewById<RecyclerView>(Resource.Id.CategoryCard_TableView);

            contentPage.SetLayoutManager(new LinearLayoutManager(this));
            contentPage.SetAdapter(new Favourites_Adapter(this, Speech_Model.SpeechActor, contentPage));

            #endregion

            #region Navigation Drawer
            var frameLayout = FindViewById<DrawerLayout>(Resource.Id.NavDrawerView);
            
            var navDrawer = FindViewById<RecyclerView>(Resource.Id.NavDrawer);

            navDrawer.SetLayoutManager(new LinearLayoutManager(this));
            navDrawer.SetAdapter(new NavDrawer_Adapter(this));


            var navButton = FindViewById<ImageButton>(Resource.Id.NavDrawerButton);

            navButton.Click += (sender, e) =>
            {
                if (this.isDrawerOpen == false)
                {
                    frameLayout.OpenDrawer(navDrawer, true);

                    this.isDrawerOpen = true;

                    return;
                }
                else
                {
                    frameLayout.CloseDrawer(navDrawer, true);

                    this.isDrawerOpen = false;

                    return;
                }
            };
            #endregion

 
        }
    }
}