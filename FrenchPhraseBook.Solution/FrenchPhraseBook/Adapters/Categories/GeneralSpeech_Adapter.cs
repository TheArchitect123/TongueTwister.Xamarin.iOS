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
    public class GeneralSpeech_Adapter : RecyclerView.Adapter
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
                "Who are you?","What's your name?","Are you feeling alright?","You look sick", "I need to lie down for a while",

            "See you tommorrow","Where where you yesterday?","See you tonight","How was your day?",

            "How was work?","Are you staying with us?","I heard today that...","I saw...","Tommorrow I'm going to see...","Next week is my birthday",

            "Congratulations on your business","Well done on your...", "Are you going to...","Do you want to go out?","I'm going out","I'm going to...",

            "Can you speak English?","Can you speak...(language)","Wait a minute", "Wait for...(Time)","When is your job interview?","Do you like your job",

            "It's been a long time since we saw each other","How have you been in the recent months?","What's your favourite TV show?",

            "Are you studying?","What are you studying?","I am studying engineering","I am studying business","I am studying...","Nice to meet you","You did not call me", 

            "It's been too long","I love this music","I love this...","I feel good","I'm healthy","I'm from America", "I'm from Brazil","I'm from...(country)",

            "I am 40 years old","Can you speak louder?","Can you whisper?", "It's very loud in here","What?","How much?","Why?","Who?","Where?","When?","Which was it?",

            "Why did you do that?","Who is she?","Who is he?","When will you get here?","What time is it?", "Why are you here?",
            
        };



        string[] frenchData = new string[] {
            "Qui es-tu?","Quel est ton nom?","Vous sentez-vous bien?","Tu semble malade","Je dois allonger pendant un certain temps","À demain",

           "Où où vous hier?","À ce soir","Comment était ta journée ?","Comment était le travail?","Vous restez avec nous ?","J'ai entendu aujourd'hui que ...",

            "Je vis...","Tommorrow Je vais voir ...","La semaine prochaine est mon anniversaire","Félicitations pour votre entreprise","Eh bien fait sur votre ...","Allez-vous ...","Voulez-vous sortir?","je sors","Je vais...",

            "Pouvez-vous parler anglais?","Pouvez vous parler...","Attends une minute","Attendre...","Quand votre entretien d'embauche ?","Aimez-vous votre travail?","Cela fait longtemps que nous nous sommes vus",

            "Comment avez-vous été dans les derniers mois?","Quelle est votre émission préférée?","Étudiez-vous?","Qu'est-ce que vous étudiez?","J'étudie l'ingénierie","J'étudie entreprise","J'étudie...","Ravi de vous rencontrer", "Tu ne m'as pas appelé",

            "Ça fait trop longtemps","J'aime cette musique","J'aime cela...","je me sens bien","je suis en bonne santé","Je viens d'Amérique","Je viens du Brésil",

            "Je viens de...","j'ai 40 ans","Pouvez-vous parler plus fort?","Pouvez-vous murmurer?","Il est très fort ici","Quelle?","Combien?","Pourquoi?","Qui?","Où?","Quand?","Qui était-il?","Pourquoi fais-tu ça?",

            "Qui est-elle?","Qui est-il?","Quand allez-vous y arriver?","Quelle heure est-il?",

           "Pourquoi es-tu ici?"
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public GeneralConversation_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public GeneralSpeech_Adapter(GeneralConversation_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new GeneralSpeech_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as GeneralSpeech_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "Gen" + holder.AdapterPosition;

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

    public class GeneralSpeech_AdapterViewHolder : RecyclerView.ViewHolder
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


        public GeneralSpeech_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, GeneralSpeech_Adapter adapter) : base(itemView)
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