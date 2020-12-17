using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

using Android.Graphics;

using Android.Content;

using Android.App;

using FrenchPhraseBook;

#region Models
using FrenchPhraseBook.Models.NavDrawer;

#endregion

namespace FrenchPhraseBook.Adapters.Components.NavDrawer
{
    public class NavDrawer_Adapter : RecyclerView.Adapter
    {
        #region Data Sources

        string[] navDrawerOptions = new string[] {
            //Main Options
            "Home", "Favourites", 
            //Categories
            "Greetings","Dating","Business","Emergency","Family & Friends","Food","Gardening","General Speech",
            "Public Transport","Math & Numbers", "Shopping","Technology","Travel", "Work",
        };



        int[] navDrawerIcons = new int[] {
           Resource.Drawable.ic_home_white_48dp,
            Resource.Drawable.favouriteStar,
            Resource.Drawable.ic_people_white_48dp,
            Resource.Drawable.ic_wc_white_48dp,
            Resource.Drawable.ic_business_white_48dp,
            Resource.Drawable.ic_local_hospital_white_48dp,
            Resource.Drawable.ic_people_white_48dp,
            Resource.Drawable.ic_free_breakfast_white_48dp,
            Resource.Drawable.ic_nature_white_48dp,
            Resource.Drawable.ic_people_white_48dp,

            Resource.Drawable.ic_train_white_48dp,
            Resource.Drawable.ic_school_white_48dp,
            Resource.Drawable.ic_shopping_cart_white_48dp,
            Resource.Drawable.ic_sd_storage_white_48dp,
            Resource.Drawable.ic_airplanemode_active_white_48dp,
            Resource.Drawable.ic_work_white_48dp,
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Activity Page { get; set; }
        #endregion

        public NavDrawer_Adapter(Activity activity)
        {
            this.Page = activity;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View navDrawer = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.NavDrawer_Layout, parent, false);

            var vh = new NavDrawer_AdapterViewHolder(navDrawer, this.Page);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            
            // Replace the contents of the view with that element
            var holder = viewHolder as NavDrawer_AdapterViewHolder;
            holder.NavDrawer_Title.Text = this.navDrawerOptions[position];
            holder.NavDrawer_Icon.SetImageResource(this.navDrawerIcons[position]);

            holder.NavDrawer_Icon.SetColorFilter(Color.LightGray);
            holder.NavDrawer_SectionTitle.Alpha = 0.0f;

            holder.IsRecyclable = true;
            //     holder.Underline_Title.Alpha = 0.0f;

            holder.SelectedIndex = position;
            
            #region Index

            if (position == NavDrawerInfo.SelectedIndex)
            {
                holder.NavDrawer_Icon.SetColorFilter(Color.Firebrick);
            }


            if (position == 2)
            {
                holder.NavDrawer_SectionTitle.Alpha = 1.0f;
                holder.NavDrawer_SectionTitle.Text = "Categories";
            }


            #endregion

        }


        public override int ItemCount => this.navDrawerIcons.Length;

    }

    public class NavDrawer_AdapterViewHolder : RecyclerView.ViewHolder
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
        public TextView NavDrawer_Title { get; set; }



        /// <summary>
        /// the nav drawer title text
        /// </summary>
        public TextView NavDrawer_SectionTitle { get; set; }

        /// <summary>
        /// The navdrawer icon 
        /// </summary>
        public ImageView NavDrawer_Icon { get; set; }
        #endregion

        #region Index
        /// <summary>
        /// The selected index of the recycle view
        /// </summary>
        public int SelectedIndex { get; set; }
        #endregion


        public NavDrawer_AdapterViewHolder(View itemView, Activity activity) : base(itemView)
        {

           
            //Views
            this.NavDrawer_Icon = itemView.FindViewById<ImageView>(Resource.Id.NavDrawerIcon);
            this.NavDrawer_SectionTitle = itemView.FindViewById<TextView>(Resource.Id.SectionTitle);
            this.NavDrawer_Title = itemView.FindViewById<TextView>(Resource.Id.NavDrawerLabel);
            this.SelectableView = itemView.FindViewById<LinearLayout>(Resource.Id.SelectableView);


            this.SelectableView.Click += (sender, e) =>
            {
                switch (this.SelectedIndex)
                {
                    #region Top Section
                    case 0: //Home
                        Intent homePage = new Intent(activity, typeof(FrenchPhraseBook.MainPage));

                        activity.StartActivity(homePage);

                        NavDrawerInfo.SelectedIndex = 0;

                        break;
                    case 1: //Favourites
                        Intent favouritePage = new Intent(activity, typeof(FrenchPhraseBook.Favourites_Activity));

                        activity.StartActivity(favouritePage);

                        NavDrawerInfo.SelectedIndex = 1;
                        break;
                    #endregion

                    #region Categories
                    case 2: //Greetings
                        Intent greetingsPage = new Intent(activity, typeof(FrenchPhraseBook.Greetings_Activity));

                        activity.StartActivity(greetingsPage);

                        NavDrawerInfo.SelectedIndex = 2;
                        break;
                    case 3: //Dating
                        Intent datingPage = new Intent(activity, typeof(FrenchPhraseBook.Dating_Activity));

                        activity.StartActivity(datingPage);

                        NavDrawerInfo.SelectedIndex = 3;
                        break;
                    case 4: //Business
                        Intent businessPage = new Intent(activity, typeof(FrenchPhraseBook.CompanyAndBusiness_Activity));

                        activity.StartActivity(businessPage);


                        NavDrawerInfo.SelectedIndex = 4;
                        break;
                    case 5: //Emergency 
                        Intent emergencyPage = new Intent(activity, typeof(FrenchPhraseBook.Emergency_Activity));

                        activity.StartActivity(emergencyPage);


                        NavDrawerInfo.SelectedIndex = 5;
                        break;
                    case 6: //Family & friends
                        Intent familyFriendsPage = new Intent(activity, typeof(FrenchPhraseBook.FamilyAndFriends_Activity));

                        activity.StartActivity(familyFriendsPage);


                        NavDrawerInfo.SelectedIndex = 6;
                        break;
                    case 7: //Food
                        Intent foodPage = new Intent(activity, typeof(FrenchPhraseBook.Food_Activity));

                        activity.StartActivity(foodPage);


                        NavDrawerInfo.SelectedIndex = 7;
                        break;
                    case 8: //Gardening
                        Intent gardeningPage = new Intent(activity, typeof(FrenchPhraseBook.GardeningAndLandscaping_Activity));

                        activity.StartActivity(gardeningPage);


                        NavDrawerInfo.SelectedIndex = 8;
                        break;

                    case 9: //General Speech 
                        Intent generalSpeechPage = new Intent(activity, typeof(FrenchPhraseBook.GeneralConversation_Activity));

                        activity.StartActivity(generalSpeechPage);


                        NavDrawerInfo.SelectedIndex = 9;
                        break;

                    case 10: //Public Transport 
                        Intent publicTransportPage = new Intent(activity, typeof(FrenchPhraseBook.PublicTransport_Activity));

                        activity.StartActivity(publicTransportPage);


                        NavDrawerInfo.SelectedIndex = 10;
                        break;
                    case 11: //Math & numbers
                        Intent mathNumbersPage = new Intent(activity, typeof(FrenchPhraseBook.MathAndNumbers_Activity));

                        activity.StartActivity(mathNumbersPage);

                        NavDrawerInfo.SelectedIndex = 11;
                        break;
                    case 12: //Shopping
                        Intent shoppingPage = new Intent(activity, typeof(FrenchPhraseBook.Shopping_Activity));

                        activity.StartActivity(shoppingPage);

                        NavDrawerInfo.SelectedIndex = 12;
                        break;
                    case 13: //Technology
                        Intent technologyPage = new Intent(activity, typeof(FrenchPhraseBook.Technology_Activity));

                        activity.StartActivity(technologyPage);

                        NavDrawerInfo.SelectedIndex = 13;
                        break;
                    case 14: //Travel
                        Intent travelPage = new Intent(activity, typeof(FrenchPhraseBook.Travel_Activity));

                        activity.StartActivity(travelPage);

                        NavDrawerInfo.SelectedIndex = 14;
                        break;

                    case 15: // Work
                        Intent workPage = new Intent(activity, typeof(FrenchPhraseBook.Work_Activity));

                        activity.StartActivity(workPage);

                        NavDrawerInfo.SelectedIndex = 15;
                        break;
                        
                }
            };
        }
    }
}
#endregion