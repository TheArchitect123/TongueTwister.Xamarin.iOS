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

#region Animation 
using Android.Animation;
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
    public class Dating_Adapter : RecyclerView.Adapter, TextToSpeech.IOnInitListener
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

        public List<string> englishData = new List<string>{

            "I love you", "Nice to finally meet you", "Can you give us a menu please", "Do you have any tables?",

            "Do you like your spaghetti?","What do you do?", "This is my first date", "Do you visit this place often?",

            "We'll do this again tommorrow", "I'll pick you up at 8:00", "How have you been lately?", "How did you start your career?",

            "What hobbies do you have?", "Are you interested in Art and culture?", "I am interested in science and technology", "So how about you?",

            "Do you enjoy your work", "I work 12 hours a day as a software engineer", "Do you travel often?", "I have traveled to paris, singapore, and madrid",

            "That sounds amazing!", "I had allot of fun tonight", "You want to come upstairs?", "I know this great sushi bar near here", "We definitely should do this again",

            "I'll call you", "What did you do in paris?", "I travelled to the eiffel tower", "Can I have the bill please, waiter?", "I'll take the bill", "Really, it's ok, I'll pay the bill",

            "The thing I am most proud of...", "I am proud of my job and life", "Will you marry me?", "Do you plan to have a family?", "Are you enjoying your meal?", "Let's order some wine",

            "The music here is nice", "I am 30 years old", "It's my birthday today", "Take care of yourself", "See you next time", "What do you plan to do in the future", "Do you read books",

            "What's your favourite movie?","What's the boldest thing you have ever done?","Who was your worst date?", "Who was your best date?","Do you like cats or dogs?","Do you have any pets?", "I have two pet turtles",

            "My favourite novel is...","My favourite TV show is..."
        };



        string[] frenchData = new string[] {

            "je t'aime", "Ravi de vous rencontrer enfin", "Pouvez-vous nous donner un menu s'il vous plaît", "Avez-vous des tables?", "Aimez-vous vos spaghettis?", "Que faire?",

            "Ceci est mon premier rendez-vous", "Avez- vous visitez ce lieu souvent?", "Nous allons faire cela à nouveau tommorrow", "Je vais vous chercher à 8:00", "Comment vas-tu ces derniers temps?", "Comment avez-vous commencé votre carrière?",

            "Quels sont vos passe-temps?", "Êtes-vous intéressé à l'art et à la culture?", "Je suis intéressé par la science et de la technologie", "Alors que diriez- vous?", "Aimez-vous votre travail", "Je travaille 12 heures par jour comme un ingénieur logiciel",

            "Voyagez-vous souvent?", "J'ai voyagé à Paris, Singapour et madrid", "Cela semble incroyable!",

            "Je l'avais allouer de ce soir amusant", "Vous voulez venir à l'étage?","Je sais que ce grand bar à sushis près d'ici",

            "Nous devrions certainement le faire à nouveau", "je t'appellerai", "Qu'avez-vous fait à paris?", "Je suis allé à la tour eiffel", "Puis-je avoir le projet de loi s'il vous plaît, serveur?",

            "Je vais prendre le projet de loi", "Vraiment, il est ok, je vais payer la facture", "La chose dont je suis le plus fier...", "Je suis fier de mon travail et de la vie","Veux-tu m'épouser?","Prévoyez- vous d'avoir une famille?",

            "Est-ce que vous appréciez votre repas?", "Nous allons commander du vin", "La musique ici est agréable", "j'ai 30 ans", "C'est mon anniversaire aujourd'hui","Prenez soin de vous","À la prochaine","Que comptez-vous faire à l'avenir",

            "Lisez-vous des livres", "Quel est votre film préféré?", "Quelle est la chose la plus audacieuse que vous avez fait?", "Qui était votre pire jour?","Qui était votre meilleur jour?","Aimez-vous les chats ou les chiens?","Avez-vous des animaux domestiques?",

            "J'ai deux tortues", "Mon roman préféré est...", "Mon émission préférée est..."

        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Dating_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public Dating_Adapter(Dating_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Dating_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

           // Animation openScroll = AnimationUtils.LoadAnimation(this.Page, Resource.Animation.CellAnimation);

        //    navDrawer.StartAnimation(openScroll);

            return vh;
        }

        public override void OnDetachedFromRecyclerView(RecyclerView recyclerView)
        {
            base.OnDetachedFromRecyclerView(recyclerView);

            recyclerView.ClearAnimation();

          
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Dating_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "D" + holder.AdapterPosition;
            //Favourites button 

          

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

    public class Dating_AdapterViewHolder : RecyclerView.ViewHolder
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



        public Dating_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Dating_Adapter adapter) : base(itemView)
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