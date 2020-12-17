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
    public class FamilyFriends_Adapter : RecyclerView.Adapter
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

               "Nephew", "Brother", "Sister", "Father","Mother","Aunt","Uncle","Grandfather","Grandmother",

           "Niece", "Son","Daughter","This is my daughter","That is my uncle", "He is my grandfather","Family trip","Where is your niece?", "My son left the house","My sister is out working","I have not seen my grandfather in three years","This is a family trip",

            "We should celebrate christmas early this year!","Who is he?","Who are they?", "Is this your grandmother?","It's good to see you again sister","He is my younger brother","She is my younger sister","This is my older niece", "This is my great grandmother",

            "Do you enjoy the family christmas dinners?", "My grandmother has left shopping", "My family lives in America","My family is in...(Country)","I'm travelling this year to see my family","I have not seen my family 10 years","I have not seen my family in...(Amount)",

            "We're going to the movies together","My...(Family member)...is going out tonight", "I called to my cousin yesterday","I called my...(Family member)...yesterday","What have you been doing this past year?","How was work today?","How was your day?","You will not believe what happened to me today?",

            "How is your mother doing?","If your father well?","Is your...(Family member)...well?","My father is an Salesman","My grandfather is an Engineer","My grandfather works as a...(Job)","What does your...(family member)...do for a living?", "Can you drive your sister to work today?","I'm going with friends tonight?",

            "I'm going to the...(Place)...with...(family member)","I worked with my brother on this project","I worked with my...(family member)...on this project",

            "My brother is sick with the flu","My...(family member)...is sick with the flu","I see my family once a week","I see my family once a...(Time)","Do you live with your parents?","Do you live with your...(Family member)?",

            "I pay my own bills and rent","My father is well read","My brother always takes of himself","My brother preaches education",

            "My niece has recently finished college","Have you done your chores?","Have you done your homework?","You want to join the gym with me?",




        };



        string[] frenchData = new string[] {

           "Neveu","Frère", "Sœur", "Père","Mère","Tante","Oncle","Grand-père","Grand-mère","Nièce","Fils","Fille","C'est ma fille", "Voilà mon oncle","Il est mon grand-père","voyage en famille","Where is your niece?","Mon fils a quitté la maison",

           "Ma sœur est hors travail","Je ne l'ai pas vu mon grand-père en trois ans","Ceci est un voyage en famille","Nous devrions célébrer Noël au début de cette année!","Qui est-il?","Qui sont-ils?",

           "Est-ce votre grand-mère?","Il est bon de vous revoir soeur","Il est mon frère cadet", "Elle est ma sœur cadette","Ceci est ma nièce plus âgée","Ceci est mon arrière grand-mère","Aimez-vous la famille des dîners de Noël?",

            "Ma grand-mère a quitté le shopping","Ma famille vit en Amérique", "Ma famille est en ...","I'm travelling this year to see my family","Je ne l'ai pas vu ma famille 10 ans","Je ne l'ai pas vu ma famille dans ...","Nous allons au cinéma ensemble","Mon ...... va sortir ce soir",

            "J'ai appelé à mon cousin hier","J'ai appelé mon...","Qu'est-ce que tu as fait cette dernière année?","Comment était le travail aujourd'hui?","Comment était ta journée?","Vous ne serez pas croire ce qui est arrivé à moi aujourd'hui?","Comment votre mère est en train de faire?","Si bien votre père?",

            "Est ton...","Mon père est un Salesman","Mon grand-père est ingénieur","Mon grand-père travaille comme...","Qu'est-ce que votre ... faire pour vivre?","Pouvez-vous conduire votre soeur au travail aujourd'hui?","Je vais avec des amis ce soir?","Je vais le...avec...","Je travaillais avec mon frère sur ce projet",

            "Je travaillais avec mon ... sur ce projet","Mon frère est malade avec la grippe","Mon ... est malade avec la grippe","Je vois ma famille une fois par semaine",

            "Je vois ma famille une fois...","Vivez-vous avec vos parents?","vous vivez avec votre ... Est-ce que", "I pay my own bills and rent","Mon père est bien lu","Mon frère a toujours de lui-même","Mon frère prêche l'éducation","Ma nièce a récemment terminé ses études","Avez-vous fait vos tâches?","Avez-vous fait vos devoirs?",

            "Vous souhaitez rejoindre la salle de gym avec moi?"
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public FamilyAndFriends_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public FamilyFriends_Adapter(FamilyAndFriends_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new FamilyFriends_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as FamilyFriends_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "F" + holder.AdapterPosition;

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

    public class FamilyFriends_AdapterViewHolder : RecyclerView.ViewHolder
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


        public FamilyFriends_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, FamilyFriends_Adapter adapter) : base(itemView)
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