using System;


using System.Linq;
using System.Collections.Generic;

using System.Collections.Generic;

using Android.Views;
using Android.Views.Animations;

using Android.Widget;
using Android.Support.V7.Widget;

using Android.App;

#region Graphics Framework
using Android.Graphics;

#endregion

#region Speech Framework
using Android.Speech.Tts;
using Android.Runtime;
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

namespace FrenchPhraseBook.Adapters.Categories
{
    public class Work_Adapter : RecyclerView.Adapter
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

            "How was work?","I love this job!","My boss can be a pain to work with sometimes","My boss is fun to work with at times",

            "My colleagues really know how to work together","Teamwork is needed to finish this task","We are approaching a deadline here people",

            "I work at an office","I work at a golf course","I work at a...(Place)",

            "Desk job","I hate this job","Our server is down!","Your new assignment is...","Management","Programmer","Your job is to...","The machine's main function is to...",

            "I've hired some new workers","I've fired somebody today",

            "You're fired!","You're hired!","When can you come in for an interview?","I work from morning to evening, everyday","Is your job easy?","Is your job hard?",

            "My job is easy!","My job is hard!","Her job is easy!","Her job is hard!",

            "We're renovating this building today","Our construction project has been halted","Our stocks are up today","Our stocks are down today",

            "This project was allot of work","This project was easy to do","We have a new team member to join our group","I'm the boss here!","Where is...(Name)...today?", "I thought of this myself",

            "We're getting some amazing work done here!","My job pays well","My job does not pay well","I want a raise!","I want a promotion!","Stock broker","Cocoa Engineer", "Carpenter", "Driver", "Pilot",

            "He is a air traffic controller","She is a pilot","When does our shift end?","How much more work is there left to do?","Where did you get your degree?","Where did you get your masters?","I take 30 minutes to drive to work, everyday",

            "Car manufacturer","Chief Executive Officer","Is your first day?",

            "I've worked here for over 10 years","I'm new here","This is her first day","We work together here","My project is nearly finished",

            "My financial advisor told me...","I've talked to my accountant...","They fire people left and right", "There is so much job opportunity here","I use the train to get to work",

            "I walk to the office"
        };



        string[] frenchData = new string[] {

                "Comment était le travail?","J'adore ce travail","Mon patron peut être une douleur à travailler avec parfois","Mon patron est amusant de travailler avec parfois","Mes collègues savent vraiment comment travailler ensemble",

           "Le travail d'équipe est nécessaire pour terminer cette tâche","Nous approchons de la date limite ici, les gens","Je travaille dans un bureau","Je travaille à un terrain de golf","Je travaille dans un ...",

            "Emploi de bureau","Je déteste ce travail","Notre serveur est en panne!","Votre nouvelle affectation est ...","La gestion","Programmeur","Votre travail consiste à ...","La fonction principale de la machine est de...","J'ai embauché quelques nouveaux travailleurs",

            "Je l'ai viré quelqu'un aujourd'hui","Vous êtes viré!","Nous vous offrons le poste!","Quand pouvez-vous venir pour une interview?","Je travaille du matin au soir , tous les jours","Est-ce que ton travail est facile?","Votre travail est dur?","Mon travail est facile!",

            "Mon travail est dur!","Son travail est facile!","Son travail est dur!","Nous rénovons ce bâtiment aujourd'hui","Notre projet de construction a été interrompue","Nos stocks sont aujourd'hui", "Nos stocks sont bas aujourd'hui","Ce projet a été attribuer de travail",

           "Ce projet a été facile à faire","Nous avons un nouveau membre de l'équipe à se joindre à notre groupe","Je suis le patron ici!","Où se trouve...aujourd'hui?","Je pensais à moi-même",

            "Nous obtenons un travail extraordinaire fait ici!","Mon travail paie bien","Mon travail ne paie pas bien","Je veux une augmentation!","Je veux une promotion!","Stock courtier","Cocoa Ingénieur","Charpentier","Chauffeur","Pilote",

            "Il est un contrôleur de la circulation aérienne","Elle est un pilote","Quand se termine notre équipe?", "Comment beaucoup plus de travail est il à faire?","Où avez-vous obtenu votre diplôme?","Où avez-vous vos maîtres?","Je prends 30 minutes pour aller au travail, tous les jours",

            "constructeur automobile","Directeur Général","Est votre premier jour?",

           "I've worked here for over 10 years","Je suis nouveau ici","Ceci est son premier jour","Nous travaillons ensemble ici","Mon projet est presque terminé","Mon conseiller financier m'a dit ...",

            "J'ai parlé à mon comptable ...", "Ils tirent les gens à gauche et à droite","Il y a tellement de possibilités d'emploi ici","J'utilise le train pour se rendre au travail",

            "Je marche au bureau"
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Work_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recycle view to map this data to 
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public Work_Adapter(Work_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Work_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Work_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "Work" + holder.AdapterPosition;

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

    public class Work_AdapterViewHolder : RecyclerView.ViewHolder
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


        public Work_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Work_Adapter adapter) : base(itemView)
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