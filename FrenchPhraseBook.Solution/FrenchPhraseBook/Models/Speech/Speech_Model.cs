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

using Android.Speech.Tts;

namespace FrenchPhraseBook.Models.Speech
{
    public class Speech_Model
    {
        /// <summary>
        /// The text to speech voice actor used to speak french phrases to the user
        /// </summary>
        public static TextToSpeech SpeechActor { get; set; }

    }
}