using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Speech.Tts;

#region Models
using FrenchPhraseBook.Models.Speech;

#endregion

namespace FrenchPhraseBook
{
    [Activity(Label = "Tongue Twister!", MainLauncher = true, Icon = "@drawable/AppIcon")]
    public class SplashScreen : Activity, TextToSpeech.IOnInitListener
    {
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status == OperationResult.Success)
            {
                if (Speech_Model.SpeechActor.IsLanguageAvailable(Java.Util.Locale.French) == LanguageAvailableResult.MissingData)
                {
                    AlertDialog.Builder messageWindow = new AlertDialog.Builder(this).SetTitle("Missing Features")
                        .SetMessage("Seems that you are missing a french language pack that you need in order to use this application. Would you like to download it now?")
                        .SetPositiveButton("Yes", (sender, e) =>
                        {
                            var speechDownload = new Intent();
                            speechDownload.SetAction(TextToSpeech.Engine.ActionInstallTtsData);

                            this.StartActivity(speechDownload);
                        }).SetNegativeButton("Maybe Later", (sender, e) =>
                        {
                            Intent mainPage = new Intent(this, typeof(MainPage));

                            this.StartActivity(mainPage);
                        });
                }
                else
                {
                    Intent mainPage = new Intent(this, typeof(MainPage));


                    Timer switchMainPage = new Timer(1000);

                    switchMainPage.Elapsed += (sender, e) =>
                    {
                        Console.WriteLine("Locales: " + Java.Util.Locale.GetAvailableLocales());


                        this.StartActivity(mainPage);

                        switchMainPage.Stop();
                    };

                    switchMainPage.Start();
                }
            }
            else
            {
                AlertDialog.Builder messageWindow = new AlertDialog.Builder(this).SetTitle("Error loading TTS engine")
                      .SetMessage("Could not load the french TTS engine. Would you like to proceed anyway?")
                      .SetPositiveButton("Yes", (sender, e) =>
                      {
                          Intent mainPage = new Intent(this, typeof(MainPage));


                          this.StartActivity(mainPage);

                      });
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SplashScreenScene);

            //  this.ActionBar.Hide();

            TextToSpeech speech = new TextToSpeech(this, this);
            
            Speech_Model.SpeechActor = speech;
        }
    }
}