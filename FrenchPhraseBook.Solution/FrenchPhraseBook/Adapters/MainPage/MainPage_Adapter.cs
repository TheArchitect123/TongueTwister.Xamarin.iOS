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

using Android.Support.Design.Widget;
using Android.Support.V7.Widget;

using Android.Graphics;

#region Data Sources
using FrenchPhraseBook.Data_Sources.Main_Page;

#endregion

#region Models
using FrenchPhraseBook.Models.NavDrawer;

#endregion

namespace FrenchPhraseBook.Adapters.MainPage
{
    
    public class MainPage_Adapter : RecyclerView.Adapter
    {
        #region Data Sources
        string[] categoryOptions = new string[] {
            //Categories
            "Greetings","Dating","Business","Emergency","Family & Friends","Food","Gardening","General Speech",
            "Public Transport","Math & Numbers", "Shopping","Technology","Travel", "Work",
        };



        int[] mainPageIcons = new int[] {
           Resource.Drawable.Greetings, Resource.Drawable.Dating, Resource.Drawable.CompanyBusiness,
            Resource.Drawable.Emergency, Resource.Drawable.FamilyFriends, Resource.Drawable.Food, Resource.Drawable.GardeningLandscape,
           Resource.Drawable.GeneralConversation, Resource.Drawable.Transport, Resource.Drawable.MathNumbers, Resource.Drawable.Shopping,
           Resource.Drawable.Technology, Resource.Drawable.Hotel, Resource.Drawable.CompanyBusiness
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Activity Page { get; set; }
        #endregion

        public MainPage_Adapter(Activity activity)
        {
            this.Page = activity;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View navDrawer = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MainPage_RecycleLayout, parent, false);

            var vh = new MainPage_AdapterViewHolder(navDrawer, this.Page);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as MainPage_AdapterViewHolder;
            holder.CategoryTitle.Text = this.categoryOptions[position];
            holder.CategoryIcon.SetImageResource(this.mainPageIcons[position]);
            
            holder.SelectedIndex = position;
        }


        public override int ItemCount => this.mainPageIcons.Length;
    }

    public class MainPage_AdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        #region Views
        /// <summary>
        /// The view that will be used for selection
        /// </summary>
        public LinearLayout SelectableView { get; set; }

        /// <summary>
        /// the nav drawer title text
        /// </summary>
        public TextView CategoryTitle { get; set; }


        /// <summary>
        /// The navdrawer icon 
        /// </summary>
        public ImageView CategoryIcon { get; set; }
        #endregion

        #region Index
        /// <summary>
        /// The selected index of the recycle view
        /// </summary>
        public int SelectedIndex { get; set; }
        #endregion


        public MainPage_AdapterViewHolder(View itemView, Activity activity) : base(itemView)
        {
            //Views
            this.CategoryIcon = itemView.FindViewById<ImageView>(Resource.Id.ItemCategory);
            this.CategoryTitle= itemView.FindViewById<TextView>(Resource.Id.CategoryText);
            this.SelectableView = itemView.FindViewById<LinearLayout>(Resource.Id.MainPageLayout);

            this.SelectableView.Click += (sender, e) =>
            {
                switch (this.SelectedIndex)
                {
                    #region Categories
                    case 0: //Greetings
                        Intent greetingsPage = new Intent(activity, typeof(FrenchPhraseBook.Greetings_Activity));

                        activity.StartActivity(greetingsPage);

                        NavDrawerInfo.SelectedIndex = 2;
                        break;
                    case 1: //Dating
                        Intent datingPage = new Intent(activity, typeof(FrenchPhraseBook.Dating_Activity));

                        activity.StartActivity(datingPage);

                        NavDrawerInfo.SelectedIndex = 3;
                        break;
                    case 2: //Business
                        Intent businessPage = new Intent(activity, typeof(FrenchPhraseBook.CompanyAndBusiness_Activity));

                        activity.StartActivity(businessPage);

                        NavDrawerInfo.SelectedIndex = 4;
                        break;
                    case 3: //Emergency 
                        Intent emergencyPage = new Intent(activity, typeof(FrenchPhraseBook.Emergency_Activity));

                        activity.StartActivity(emergencyPage);

                        NavDrawerInfo.SelectedIndex = 5;
                        break;
                    case 4: //Family & friends
                        Intent familyFriendsPage = new Intent(activity, typeof(FrenchPhraseBook.FamilyAndFriends_Activity));

                        activity.StartActivity(familyFriendsPage);

                        NavDrawerInfo.SelectedIndex = 6;
                        break;
                    case 5: //Food
                        Intent foodPage = new Intent(activity, typeof(FrenchPhraseBook.Food_Activity));

                        activity.StartActivity(foodPage);

                        NavDrawerInfo.SelectedIndex = 7;
                        break;
                    case 6: //Gardening
                        Intent gardeningPage = new Intent(activity, typeof(FrenchPhraseBook.GardeningAndLandscaping_Activity));

                        activity.StartActivity(gardeningPage);

                        NavDrawerInfo.SelectedIndex = 8;
                        break;

                    case 7: //General Speech 
                        Intent generalSpeechPage = new Intent(activity, typeof(FrenchPhraseBook.GeneralConversation_Activity));

                        activity.StartActivity(generalSpeechPage);

                        NavDrawerInfo.SelectedIndex = 9;
                        break;

                    case 8: //Public Transport 
                        Intent publicTransportPage = new Intent(activity, typeof(FrenchPhraseBook.PublicTransport_Activity));

                        activity.StartActivity(publicTransportPage);

                        NavDrawerInfo.SelectedIndex = 10;
                        break;
                    case 9: //Math & numbers
                        Intent mathNumbersPage = new Intent(activity, typeof(FrenchPhraseBook.MathAndNumbers_Activity));

                        activity.StartActivity(mathNumbersPage);

                        NavDrawerInfo.SelectedIndex = 11;
                        break;
                    case 10: //Shopping
                        Intent shoppingPage = new Intent(activity, typeof(FrenchPhraseBook.Shopping_Activity));

                        activity.StartActivity(shoppingPage);

                        NavDrawerInfo.SelectedIndex = 12;
                        break;
                    case 11: //Technology
                        Intent technologyPage = new Intent(activity, typeof(FrenchPhraseBook.Technology_Activity));

                        activity.StartActivity(technologyPage);

                        NavDrawerInfo.SelectedIndex = 13;
                        break;
                    case 12: //Travel
                        Intent travelPage = new Intent(activity, typeof(FrenchPhraseBook.Travel_Activity));

                        activity.StartActivity(travelPage);

                        NavDrawerInfo.SelectedIndex = 14;
                        break;

                    case 13: // Work
                        Intent workPage = new Intent(activity, typeof(FrenchPhraseBook.Work_Activity));

                        activity.StartActivity(workPage);

                        NavDrawerInfo.SelectedIndex = 15;
                        break;
                    #endregion
                }
            };
        }
    }
}