using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FrenchPhraseBook.Data_Sources.Main_Page
{
    public class MainPage_DataSource
    {
        /// <summary>
        /// The main page data source
        /// </summary>
        public List<MainPage_DataSource> MainPageSource { get; set; }

        /// <summary>
        /// The image resource of the path
        /// </summary>
        public int ImageResource { get; set; }

        /// <summary>
        /// The category title 
        /// </summary>
        public string CategTitle { get; set; }

        public MainPage_DataSource()
        {


            //Generate the list of categories
            /*
            this.MainPageSource = new List<MainPage_DataSource>() {
                //Greetings
                new MainPage_DataSource(){
                      CategTitle = "Greetings",
                      ImageResource = Resource.Drawable.GreetingsCategory
                 },

                 //Dating
                new MainPage_DataSource(){
                      CategTitle = "Dating",
                      ImageResource = Resource.Drawable.DatingCategory
                 },

                /*
                 //General Conversation
                new MainPage_DataSource(){
                      CategTitle = "General Conversation",
                 },

                 //Emergency
                new MainPage_DataSource(){
                      CategTitle = "Emergency",
                 },

                 //Company & Business
                new MainPage_DataSource(){
                      CategTitle = "Company & Business",
                 },


                //Travel
                new MainPage_DataSource(){
                      CategTitle = "Travel",
                 },


                    //Public Transport
                new MainPage_DataSource(){
                      CategTitle = "Public Transport",
                 },   
                

                // Food
                new MainPage_DataSource(){
                      CategTitle = "Food",
                 },  
                
                //Family & Friends
                new MainPage_DataSource(){
                      CategTitle = "Family & Friends",
                 },   
                
                //School & Education
                new MainPage_DataSource(){
                      CategTitle = "School & Education",
                 },    
                
                // Technology
                new MainPage_DataSource(){
                      CategTitle = "Technology",
                 },


                   // Work
                new MainPage_DataSource(){
                      CategTitle = "Work",
                 },
   
                // Gardening and landscaping
                new MainPage_DataSource(){
                      CategTitle = "Gardening & Landscaping",
                 },
   
                //Math & Numbers
                new MainPage_DataSource(){
                      CategTitle = "Math & Numbers",
                 },
   
                //Shopping
                new MainPage_DataSource(){
                      CategTitle = "Shopping",
                 }
                
            };
        */
        }

    }
}