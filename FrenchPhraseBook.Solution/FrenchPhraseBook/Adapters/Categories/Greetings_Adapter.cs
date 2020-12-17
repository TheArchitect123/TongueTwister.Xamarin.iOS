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
    public class Greetings_Adapter : RecyclerView.Adapter
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
                "Yes","No","Do you understand me?", "I don't understand",

            "I understand perfectly", "Thank you. I'm good", "I need your help", "Please",

            "I'm sorry", "Please repeat yourself", "Can you speak a little slower?", "No, thank you","Congratulations",

            "I feel sorry for you", "It's fine", "I don't know about that", "I don't like this",

            "I like this very much", "You're welcome", "I understand that...", "I think that...",

            "I want some...", "I like...", "Can I borrow your phone", "Are you sure about this?", "Can you speak German?", "What does that mean?", "How do you pronounce this sentence?",

            "No, thanks! I'm good","No, that's wrong","That's right", "No problem","Help!","Who?","What?","How many?","How much? (amount)",

            "Why?","Why not?","Which?","Where?","When?","What month is your birthday?", "What are doing later?", "Where are you going?","I'm going to...",

            "What's this?","Are you feeling alright?", "Don't forget...","Excuse me", "Mind yourself","Where can I find the elevator?", "It's been a long time","You want to talk?",

            "I love your car","Do you go here often","What do you do for a living?", "I have never seen you here before","Nice to see you","Which country are you from?",

            "My name is...","I am 60 years old","I am 20 years old","I work as an engineer","What is your name?","Are you studying right now?", "Who are you?", "Hello", "Nice to meet you"

        };

        
        string[] frenchData = new string[] {
            "Oui", "Non", "Me comprenez-vous?","Je ne comprends pas","Je comprends parfaitement","Je vous remercie. CA va bien","j'ai besoin de ton aide","S'il vous plaît","Je suis désolé","S'il vous plaît vous répétez",

            "Pouvez-vous parler un peu plus lent ?","Non, merci", "Félicitations à vous","Je suis désolé pour toi","C'est bien","Je ne sais pas","Je n'aime pas cette","J'aime beaucoup cela","De rien","Je comprends que...",

           "Je pense que...","J'en veux...","J'aime...","Puis-je emprunter votre téléphone","Es-tu sûr de ça?","Parlez-vous allemand?","Qu'est-ce que ça veut dire?","Comment prononcez-vous cette phrase ?","Non merci! CA va bien","Non c'est faux",

           "C'est vrai","Pas de problème","Aidez-moi!","Qui?","Quelle?",

            "Combien?","Combien?","Pourquoi?","Pourquoi pas?","Lequel?","Où?","Quand?","Quel mois est votre anniversaire ?", "Que font plus tard?","Où allez-vous?","Je vais...","Qu'est-ce que c'est ça?","Vous sentez-vous bien?",

           "Ne pas oublier ...","Excusez-moi","Faites attention à vous","Où puis -je trouver l'ascenseur?","Cela fait longtemps","Vous voulez parler?","J'adore votre voiture","Allez-vous souvent ici","Comment gagnez-vous votre vie?","Je ne vous ai jamais vu ici avant","Ravi de vous voir",

            "De quel pays êtes-vous?","Mon nom est...","Je suis âgé de 60 ans","j'ai 20 ans","Je travaille comme un ingénieur","Je travaille comme un ingénieur", "Est-ce que vous étudiez en ce moment?","Qui es-tu?","Bonjour","Ravi de vous rencontrer"

        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Greetings_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public Greetings_Adapter(Greetings_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Greetings_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Greetings_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "Gr" + holder.AdapterPosition;

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

    public class Greetings_AdapterViewHolder : RecyclerView.ViewHolder
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


        public Greetings_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Greetings_Adapter adapter) : base(itemView)
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