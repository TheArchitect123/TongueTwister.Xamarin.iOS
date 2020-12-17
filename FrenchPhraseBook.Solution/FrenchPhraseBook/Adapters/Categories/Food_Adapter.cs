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
    public class Food_Adapter : RecyclerView.Adapter
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

            "I like this dish","Are you enjoying your meal","How's dinner?", "How's breakfast?", "How's lunch?","Afternoon tea","Steak",

            "Cereal", "Chicken","Spinach","Lemon","I am a great chef","Spoon","Knife","Cutlery","Do you know how to cook...","Pizza",

            "Vegetables and rice","I'm starving","I'm a vegetarian","I want to order some take away","This is tasty","This is spicy","Did you make this yourself?",

            "Salt","Sugar","Soy sauce is great for sushi","Beer","Milk and Honey","Coffee","This tea is hot","Orange juice","Watermelon",

            "Fruit smoothie","Do you drink protein shakes?","How much is this?","Glass cup","What do you call this?","Can I have a menu please?","Can I have the bill please?",

            "I don't eat meat","Can I order some..?","Apple","Fruit salad","Sultanas","Dried fruit","I like eating...","Milk","Hot","Cold","Mild","Spicy","Sweet", "Salty","Sour",

            "This is too sweet","This is too salty","Too much salt is not good","Can I have some ice with that?","A toast to you","Cake","Bread","Toast", "Yoghourt","Butter", "Ice-cream","This is my recipe",
           
        };



        string[] frenchData = new string[] {

            "J'aime ce plat","Êtes-vous profiter de votre repas","Comment est le dîner?","Comment est le petit déjeuner?", "Comment est le déjeuner?","Le thé de l'après-midi","Steak","Céréale",

            "poulet","épinard","citron","Je suis un grand chef","Cuillère","Couteau","Coutellerie","Savez-vous comment faire cuire...",

            "Pizza","Les légumes et le riz","je meurs de faim","Je suis un végétarien", "Je veux commander quelque emporter","Ceci est savoureux","Ceci est épicé","Avez-vous fait vous-même?","Sel",

            "Sucre","La sauce de soja est excellent pour sushi","Bière","Lait et miel","café","Ce thé est chaud","du jus d'orange",

            "Pastèque","Smoothie aux fruits","Vous buvez shakes de protéines?","Combien ça coûte?","Coupe en verre","Comment appelles-tu ceci?","Puis-je avoir un menu s'il vous plaît?","Puis-je avoir la note s'il vous plaît?",

            "Je ne mange pas de viande","Puis-je commander un certain..?","pomme","Salade de fruit","Sultanes","Fruit sec","J'aime manger...","Lait",

            "Chaud", "Du froid","Doux","Épicé","Doux","Salé","Acide","Ceci est trop sucré","Ceci est trop salée","Trop de sel est pas bon","Puis-je avoir un peu de glace avec ça?",

           "Un toast à vous","gâteau","Pain","Pain grillé","Yaourt","beurre","Crème glacée","Ceci est ma recette"
        };

        #endregion

        #region Content

        /// <summary>
        /// The current page
        /// </summary>
        public Food_Activity Page { get; set; }

        /// <summary>
        /// The speech engine used to play french text when clicked on
        /// </summary>
        public TextToSpeech Speech { get; set; }

        /// <summary>
        /// The recylcer view displaying the content for this category
        /// </summary>
        private RecyclerView ContentView { get; set; }
        #endregion

        public Food_Adapter(Food_Activity activity, TextToSpeech speech, RecyclerView view)
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

            var vh = new Food_AdapterViewHolder(navDrawer, this.Page, this.Speech, this);

            return vh;
        }


        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            // Replace the contents of the view with that element
            var holder = viewHolder as Food_AdapterViewHolder;
            holder.EnglishTitle.Text = this.englishData[position];
            holder.FrenchText.Text = this.frenchData[position];

            holder.PhraseID = "Food" + holder.AdapterPosition;

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

    public class Food_AdapterViewHolder : RecyclerView.ViewHolder
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


        public Food_AdapterViewHolder(View itemView, Activity activity, TextToSpeech speech, Food_Adapter adapter) : base(itemView)
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