using System;
using System.Collections.Generic;

using System.Linq;

using Android.Views;
using Android.Views.Animations;

using Android.Widget;
using Android.Support.V7.Widget;

#region Speech
using Android.Speech.Tts;
using Android.Runtime;
#endregion

using Android.App;

using Android.Graphics;

#region Web Service

using FrenchPhraseBook.Models.Services;
#endregion

#region Database
using FrenchPhraseBook.Backend.Storage;

using FrenchPhraseBook.Backend.Storage.Models;


#region SQLite
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

using FrenchPhraseBook.Backend.Storage.Models.Database;

#endregion
#endregion

namespace FrenchPhraseBook.Adapters.Search
{
    public class Search_Adapter : RecyclerView.Adapter
    {
        #region Data Sources
        //dictionary of categories

        List<int> VisibleItemRows = new List<int>() { };

        #region Data Sources

        public List<int> FavouritedIndexData { get; set; } = new List<int>() { };
        #endregion
        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Search_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }

        /// <summary>
        /// The search view used to search for phrases
        /// </summary>
        private EditText SearchView { get; set; }
        #endregion

        #region Models
        /// <summary>
        /// Whether the phrase is favourited this will reload the recycle view and prprevent the animation from playing
        /// </summary>
        public bool IsFavourited { get; set; }


        #endregion

        public Search_Adapter(Search_Activity activity, TextToSpeech speech, RecyclerView view, EditText searchView)
        {
            this.Page = activity;

            this.Speech = speech;

            this.ContentView = view;

            this.SearchView = searchView;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View navDrawer = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.StandardCategoryLayout, parent, false);

            var vh = new Search_AdapterViewHolder(navDrawer, this.Page, this.Speech, this, this.FavouritedIndexData, this.SearchView);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            Random random = new Random();

            // Replace the contents of the view with that element
            var holder = viewHolder as Search_AdapterViewHolder;
            holder.EnglishTitle.Text = Favourites_Model.GetSearch[position].EnglishText;
            holder.FrenchText.Text = Favourites_Model.GetSearch[position].FrenchText;
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            //Successful initializatio of speech engine
        }

        public override int ItemCount => Favourites_Model.GetSearch.Count;
    }

    public class Search_AdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        #region Views
        /// <summary>
        /// The view that will be used for selection
        /// </summary>
        public FrameLayout SelectableView { get; set; }

        /// <summary>
        /// the nav drawer title text
        /// </summary>
        public TextView EnglishTitle { get; set; }


        /// <summary>
        /// The navdrawer icon 
        /// </summary>
        public TextView FrenchText { get; set; }

        /// <summary>
        /// Determines whether a phrase is favourited or not
        /// </summary>
        public ImageButton FavouritesButton { get; set; }

        /// <summary>
        /// The seperator between each text
        /// </summary>
        public TextView UnderlineView { get; set; }

        #endregion



        #region Index

        /// <summary>
        /// Is the favourites icon favourited or not
        /// </summary>
        public bool IsFavourited { get; set; }

        /// <summary>
        ///  The unique phrase ID
        /// </summary>
        public string PhraseID { get; set; }
        #endregion

        public Search_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Search_Adapter adapter, List<int> favouritesItem, Android.Widget.EditText searchView) : base(itemView)
        {
            //Views
            this.EnglishTitle = itemView.FindViewById<TextView>(Resource.Id.StandardEnglishText);
            this.FrenchText = itemView.FindViewById<TextView>(Resource.Id.StandardFrenchText);

            this.FavouritesButton = itemView.FindViewById<ImageButton>(Resource.Id.FavouritesView);

            this.SelectableView = itemView.FindViewById<FrameLayout>(Resource.Id.StandardCategoryLayout);

            this.SelectableView.Click += (sender, e) =>
            {
                speech.Speak(this.FrenchText.Text, QueueMode.Flush, null);
            };
        }
    }
}