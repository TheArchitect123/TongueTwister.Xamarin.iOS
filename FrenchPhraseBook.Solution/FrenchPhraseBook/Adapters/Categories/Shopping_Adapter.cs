using System;

using System.Linq;
using System.Collections.Generic;

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

#region Database
using FrenchPhraseBook.Backend.Storage;

using FrenchPhraseBook.Backend.Storage.Models;

#region SQLite
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

using FrenchPhraseBook.Backend.Storage.Models.Database;

#endregion
#endregion

namespace FrenchPhraseBook.Adapters.Categories
{
    public class Shopping_Adapter : RecyclerView.Adapter
    {
        #region Data Sources
        //dictionary of categories

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

        public List<string> englishData = new List<string> {

                "Cash register","Shopping","Shelf","Fridge","Grocery store","Mall","How much does this cost?","Where can I find...(Item)","Do you work here?",

            "I work near the furniture section","I am looking for...(item)",

            "Battery","Electronics", "Here's your change","Where my change?","This is too expensive","This too cheap","This product is faulty",

            "This product is on display only","What is this?","What is that?","This item costs...(Amount)",

            "Which one is the plasma TV?","I want that...(item)...there","I cannot reach that item on the shelf","All this costs...(Amount)","Would you like to buy...(item)?",

            "Would you like to sample this chocolate?","Free TV for every $300 you spend","I hate shopping!","I love shopping!","I need to buy some new clothes",

            "I'm going to the mall with my friends","I'm looking for...(Shop name)","Buy 2 get one free","How much can I get for all these?","50% off sale for today only","(Number)%...off sale for tonight only",

            "I'll take 2 of those","Do you take credit card here?","We only accept cash","We accept cash and credit card","Where is the food court?","I need to withdraw cash",

            "Put it on my tab",


        };



        string[] frenchData = new string[] {
                    "Caisse enregistreuse","Achats","Étagère","Frigo","Épicerie","Centre commercial","Combien cela coûte?",

            "Où puis-je trouver...","Est-ce que tu travailles ici?","Je travaille près de la section de meubles","Je cherche...",

            "les accumulateurs","Électronique","Voilà votre monnaie","Lorsque mon changement?","Ceci est trop cher",

            "Ce trop pas cher","Ce produit est défectueux","Ce produit est en affichage seulement","Qu'est-ce que c'est?","Qu'est-ce que c'est?","Cet article coûte...",

            "Lequel est le téléviseur à écran plasma?","Je veux que ... il","Je ne peux pas accéder à cet article sur le plateau","Tout cela coûte...","Voulez-vous acheter...","Voulez-vous goûter ce chocolat?",

           "Free TV pour chaque 300 $ que vous dépensez","Je déteste le shopping!","J'adore le shopping!","Je besoin d'acheter de nouveaux vêtements","Je vais au centre commercial avec mes amis",

           "Je cherche...","Acheter 2 obtenir un gratuitement","Combien puis-je obtenir pour tous ces?","50% off vente pour aujourd'hui seulement","... Hors vente pour ce soir seulement", "Je vais prendre 2 de ceux",

            "Prenez-vous la carte de crédit ici?","Nous acceptons uniquement les espèces","Nous acceptons l'argent comptant et carte de crédit","Où est la cour de nourriture?","Je dois retirer de l'argent",

            "Mettez-le sur mon onglet"
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Shopping_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public Shopping_Adapter(Shopping_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Shopping_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Shopping_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "Shop" + holder.AdapterPosition;

            if (Favourites_Model.GetFavourites.Find(i => i.PhraseId == holder.PhraseID) != null)
            {
                holder.FavouritesButton.SetBackgroundColor(Color.Transparent);
                holder.FavouritesButton.SetImageResource(Resource.Drawable.favouriteStar);
                holder.FavouritesButton.SetColorFilter(Color.Goldenrod);
            }
            else
            {
                holder.FavouritesButton.SetBackgroundColor(Color.Transparent);
                holder.FavouritesButton.SetImageResource(Resource.Drawable.UnfavourteStar);
                holder.FavouritesButton.SetColorFilter(Color.Goldenrod);
            }


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

        public override int ItemCount => this.englishData.Count;
    }

    public class Shopping_AdapterViewHolder : RecyclerView.ViewHolder
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


        public Shopping_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Shopping_Adapter adapter) : base(itemView)
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

            this.FavouritesButton.Click += (sender, e) =>
            {
                if (this.IsFavourited == false)
                {
                    this.IsFavourited = true;

                    CRUDOperations.SetFavourites(new Backend.Storage.Database.Tables.Favourites.Favourites_Sqlite()
                    {
                        DeviceId = Database_Models.DeviceID,
                        EnglishText = this.EnglishTitle.Text,
                        FrenchText = this.FrenchText.Text,
                        IsFavourite = true,
                        PhraseId = this.PhraseID

                    }, new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));

                    CRUDOperations.GetFavourites(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));
                }
                else
                {
                    this.IsFavourited = false;

                    CRUDOperations.SetFavourites(new Backend.Storage.Database.Tables.Favourites.Favourites_Sqlite()
                    {
                        DeviceId = Database_Models.DeviceID,
                        EnglishText = this.EnglishTitle.Text,
                        FrenchText = this.FrenchText.Text,
                        IsFavourite = false,
                        PhraseId = this.PhraseID

                    }, new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));

                    CRUDOperations.GetFavourites(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));
                }

                adapter.NotifyItemChanged(adapter.englishData.IndexOf(this.EnglishTitle.Text));
            };

        }
    }
}