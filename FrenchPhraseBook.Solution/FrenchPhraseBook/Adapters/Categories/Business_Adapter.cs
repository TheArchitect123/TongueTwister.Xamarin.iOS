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

namespace FrenchPhraseBook.Adapters.Categories
{
    public class Business_Adapter : RecyclerView.Adapter, TextToSpeech.IOnInitListener
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

        public List<string> englishData = new List<string>(){

        "I'm a freelancer", "I work part time here", "I work full time here", "Can I make an appointment",

            "Do you work here?", "He's my boss", "I'm the manager here", "Where can I find the manager?",

            "Where can I find the nearest work station?", "Employee", "Boss", "Employer", "I own this company", "Who's in charge here?", "I've worked here for 20 years",

            "Company", "Business", "I am a business man","Director", "Sales team", "Programmer",

            "Senior programmer", "Employment agency", "This is my advertising agency", "This is my website", "This is the internet",

            "I designed this...", "Thank you for the interview", "You're hired", "Welcome to...", "Sales manager",

            "Dear Mr/Mrs...", "Dear Sir/Madam", "We would like to hire...","I need your services","I've read your letter",

            "Chief Executive Officer", "Receptionist", "I want a job here","Are you hiring?", "Do you have any vacancies?","Currently I work for...",

            "I work on a casual basis","Return on Investment","Profit","Cost","Revenue","Client","Email","This is my website",

            "I am a web developer","I am an iOS engineer","I make apps for a living","Dear...","To whom it may concern...","My annual income is...",

            "I sell...","We sell...","This company produces...","We are a software company",

            "I have a meeting with...","I have an appointment at...","Driver","Supervisor","Trainee","Income tax","State Taxes",

            "Federal income tax","How much tax do you pay?","Stimulate growth in the economy","Lowering taxes"


        };



        string[] frenchData = new string[] {

            "Je suis un pigiste", "Je travaille à temps partiel ici", "Je travaille à temps plein ici", "Puis-je faire un rendez-vous", "Est-ce que tu travailles ici?",

            "Il est mon patron", "Je suis le gestionnaire ici","Où puis-je trouver le gestionnaire?","Où puis -je trouver le poste de travail le plus proche?","Employé","Patron","Employeur",

            "Je possède cette société","Qui est responsable ici?","Je travaille ici depuis 20 ans","Compagnie","Entreprise","Je suis un homme d'affaire","Réalisateur","Équipe de vente",

            "Programmeur","Programmeur senior","Agence d'emploi","Ceci est mon agence de publicité","Ceci est mon site","Ceci est l'Internet","J'ai conçu cette...","Merci pour l'interview",

            "Nous vous offrons le poste","Bienvenue à...","Directeur commercial","Cher Monsieur / Madame...","Cher Monsieur / Madame...","Nous aimerions embaucher...","J'ai besoin de vos services",

            "J'ai lu votre lettre","Directeur Général","Réceptionniste","Je veux un emploi ici","Embauchez-vous?","Avez-vous des postes vacants?","Actuellement, je travaille pour...","Je travaille sur une base occasionnelle",

            "Retour sur investissement","Le profit","Coût","Revenu","Client","Email","Ceci est mon site","Je suis un développeur web", "Je suis un ingénieur iOS",

            "Je fais des applications pour une vie","cher...","À qui cela concerne...","Mon revenu annuel est...","je vends...","Nous vendons...","Cette société produit...","Nous sommes une société de logiciels",

            "J'ai une réunion avec...","J'ai un rendez-vous à...","Chauffeur","Superviseur","Stagiaire","Impôt sur le revenu","État taxes","Taxe fédérale sur le revenu","Combien d'impôts payez -vous?",

            "Stimuler la croissance dans l'économie", "Réduire les impôts"
        };

        List<int> VisibleItemRows = new List<int>() { };

        #region Data Sources

        public List<int> FavouritedIndexData { get; set; } = new List<int>() { };
        #endregion
        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public CompanyAndBusiness_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        #region Models
        /// <summary>
        /// Whether the phrase is favourited this will reload the recycle view and prprevent the animation from playing
        /// </summary>
        public bool IsFavourited { get; set; }


        #endregion

        public Business_Adapter(CompanyAndBusiness_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Business_AdapterViewHolder(navDrawer, this.Page, this.Speech, this, this.FavouritedIndexData);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            Random random = new Random();

            // Replace the contents of the view with that element
            var holder = viewHolder as Business_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "B" + holder.AdapterPosition;
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


            #region Animations


            if (this.ContentView.IsLaidOut == true)
            {
                Animation openScroll = AnimationUtils.LoadAnimation(this.Page, Resource.Animation.CellAnimation);

                holder.ItemView.StartAnimation(openScroll);
            }
            this.IsFavourited = true;

            #endregion

            Console.WriteLine("Index: " + position);

        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            //Successful initializatio of speech engine
        }

        public override int ItemCount => this.englishData.Count;
    }

    public class Business_AdapterViewHolder : RecyclerView.ViewHolder
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

        public Business_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Business_Adapter adapter, List<int> favouritesItem) : base(itemView)
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
                        EnglishText = this.EnglishTitle.Text,
                        FrenchText = this.FrenchText.Text,
                        IsFavourite = true,
                        PhraseId = this.PhraseID,
                        ViewHolderID = this.AdapterPosition

                    }, new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));

                    favouritesItem.Add(this.AdapterPosition);

                    CRUDOperations.GetFavourites(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));

                }
                else
                {
                    this.IsFavourited = false;

                    CRUDOperations.SetFavourites(new Backend.Storage.Database.Tables.Favourites.Favourites_Sqlite()
                    {
                        EnglishText = this.EnglishTitle.Text,
                        FrenchText = this.FrenchText.Text,
                        IsFavourite = false,
                        PhraseId = this.PhraseID,
                        ViewHolderID = this.AdapterPosition

                    }, new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));

                    favouritesItem.Remove(this.AdapterPosition);

                    CRUDOperations.GetFavourites(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));
                }

                adapter.NotifyItemChanged(adapter.englishData.IndexOf(this.EnglishTitle.Text));
            };



        }
    }
}