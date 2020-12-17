using System;

using System.Linq;
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
    public class Technology_Adapter : RecyclerView.Adapter
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
                "Do you have an iPhone?","What iOS version do you have?","This phone has a built in camera","I own an iPad","I love this app!","Do you read the science paper?",

            "I read allot of sci fi novels","This app uses API found in cocoa touch layer","This device has many features","There are many apps that implement global positioning systems",

            "I keep up to date with the latest features of Xcode","Prototyping board","Breadboard",

            "Integrated circuits","Power Electronics","Jet engine","Radiator","Keyboard","Mouse","Computer monitor","Programming skills","Agile methodology",

            "Navigation stack","Stack trace","Phone technology","Integrated Development Enviornment","Panoramics","Lightbulb","Specifications",

            "Local notification","Push notification service","Science","This is an interesting science","Do you know anything about cars?","I am an engineer of cocoa systems","Internet Protocol", "Transmission Control Protocol",

            "Linux operating system","Internet Security","User Interface","Classes","Properties","Access modifiers","I've been to this tech show lately","Android phone","I own...(device)","Music player","Exception handling",

            "Are you familiar with polymorphism?","Do you implement inheritance?","Core operating system",

            "Windows phone","Parallel switches","Electro magnetic pulse","Power core","Sim card","My phone is broken",

            "My computer has been hacked!","Artifical Intelligence","Distribution Certificate Identification","Lamp","Fabrication Technology",

           "Plasma cutter","3rd party network","Switching circuit","Television station", "Radio station","Radio Transmitter","Power distrupter","Battery cable","Battery charger"



        };



        string[] frenchData = new string[] {

                    "Avez-vous un iPhone?","Qu'est-ce que la version iOS avez-vous?","Ce téléphone a un appareil photo intégré","Je possède un iPad","J'adore cette application","Avez-vous lu le document de la science?","Je lis attribuer des sci fi romans","Cette application utilise l'API trouvé dans le cacao couche tactile",

            "Cet appareil possède de nombreuses fonctionnalités","Il existe de nombreuses applications qui mettent en œuvre des systèmes de positionnement global","Je garde à jour avec les dernières fonctionnalités de Xcode",

            "carte de prototypage","Breadboard",

            "Circuits intégrés","Electronique de puissance","Moteur d'avion","Radiateur","Clavier","Souris","Moniteur d'ordinateur","compétences de programmation","méthodologie Agile","pile Navigation","Trace de la pile",

            "la technologie de téléphone","Enviornment développement intégré","Panoramiques","Ampoule", "Caractéristiques",

            "notification locale","service de notification Push","Science","Ceci est une science intéressante",

            "Savez-vous quoi ce soit sur les voitures?","Je suis un ingénieur des systèmes de cacao","protocole Internet","Protocole de transmission",

            "système d'exploitation Linux","la sécurité sur Internet","Interface utilisateur","Des cours","Propriétés","Les modificateurs d'accès","Je suis allé à ce spectacle tech récents", "téléphone Android","Je possède...","Lecteur de musique","Gestion des exceptions",

            "Êtes-vous familier avec le polymorphisme?","Avez- vous mettre en œuvre l'héritage?","système d'exploitation de base","Téléphone Windows","commutateurs parallèles","impulsion magnétique Electro",

            "noyau de puissance","Carte SIM","Mon téléphone est cassé",

            "Mon ordinateur a été piraté!","Intelligence artificielle","Répartition certificat d'identification","Lampe","Technologie de fabrication","cutter plasma",

            "3ème réseau de parti","circuit de commutation","Poste de télévision", "Station de radio","Transmetteur Radio","Puissance distrupter","câble de batterie","Chargeur de batterie"
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Technology_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public Technology_Adapter(Technology_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Technology_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Technology_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "Tech" + holder.AdapterPosition;

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

    public class Technology_AdapterViewHolder : RecyclerView.ViewHolder
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


        public Technology_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Technology_Adapter adapter) : base(itemView)
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