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

#region Models
using FrenchPhraseBook.Backend.Storage.Models;

#endregion

#region Database
using FrenchPhraseBook.Backend.Storage;

#region SQLite
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

using FrenchPhraseBook.Backend.Storage.Models.Database;

#endregion
#endregion

namespace FrenchPhraseBook.Adapters.Favourites
{
    public class Favourites_Adapter : RecyclerView.Adapter
    {
        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Favourites_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        List<int> VisibleItemRows = new List<int>() { };

        #region Data Sources

        public List<int> FavouritedIndexData { get; set; } = new List<int>() { };
        #endregion

        public Favourites_Adapter(Favourites_Activity activity, TextToSpeech speech, RecyclerView view)
        {
            this.Page = activity;

            this.Speech = speech;

            this.ContentView = view;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View navDrawer = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.StandardCategoryLayout, parent, false);

            var vh = new Favourites_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Favourites_AdapterViewHolder;
            //  holder.EnglishTitle.Text = this.englishData[position];
            //    holder.FrenchText.Text = this.frenchData[position];

            //Favourites button 

            holder.EnglishTitle.Text = Favourites_Model.GetFavourites.Where(i => i.IsFavourite == true).ToList()[position].EnglishText;
            holder.FrenchText.Text = Favourites_Model.GetFavourites.Where(i => i.IsFavourite == true).ToList()[position].FrenchText;
            holder.IsFavourited  = Favourites_Model.GetFavourites.Where(i =>i.IsFavourite == true).ToList()[position].IsFavourite;
            holder.PhraseID = Favourites_Model.GetFavourites.Where(i => i.IsFavourite == true).ToList()[position].PhraseId;


            holder.FavouritesButton.SetBackgroundColor(Color.Transparent);
            holder.FavouritesButton.SetImageResource(Resource.Drawable.favouriteStar);
            holder.FavouritesButton.SetColorFilter(Color.Goldenrod);

            holder.SelectedIndex = position;

            #region Animations

            if (this.ContentView.IsLaidOut == true)
            {
                Animation openScroll = AnimationUtils.LoadAnimation(this.Page, Resource.Animation.CellAnimation);

                holder.ItemView.StartAnimation(openScroll);
            }
            #endregion
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            //Successful initializatio of speech engine
        }

        public override int ItemCount => Favourites_Model.GetFavourites.Where(i => i.IsFavourite == true).ToList().Count;
    }

    public class Favourites_AdapterViewHolder : RecyclerView.ViewHolder
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
        /// The selected index of the recycle view
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Is the favourites icon favourited or not
        /// </summary>
        public bool IsFavourited { get; set; }

        /// <summary>
        ///  The unique phrase ID
        /// </summary>
        public string PhraseID { get; set; }
        #endregion


        public Favourites_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, RecyclerView.Adapter adapter) : base(itemView)
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