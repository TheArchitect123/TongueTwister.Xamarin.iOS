using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;

using Android.Util;

using Android.Widget;

using Android.Support.V7.Widget;
using Android.Support.Design.Widget;

using Android.Graphics;
using Android.Graphics.Drawables;

using Android.Hardware.Display;

#region Database
using FrenchPhraseBook.Backend.Storage;

#endregion

#region Conversions 
using FrenchPhraseBook.Generic.Conversion;

#endregion

#region Support Frameworks
using Android.Support.V4.Widget;
#endregion

#region Adapters
using FrenchPhraseBook.Adapters.MainPage;

//Nav Drawer
using FrenchPhraseBook.Adapters.Components.NavDrawer;
#endregion

#region Data Sources
using FrenchPhraseBook.Data_Sources.Main_Page;

#endregion

#region SQLite
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

using FrenchPhraseBook.Backend.Storage.Models.Database;

#endregion

using Android.Speech.Tts;


namespace FrenchPhraseBook
{
    [Activity(Label = "Tongue Twister!")]
    public class MainPage : Activity, TextToSpeech.IOnInitListener
    {
        #region Layouts 

        /// <summary>
        /// The navigation drawer's layout
        /// </summary>
        public LinearLayout NavDrawer_Layout { get; set; }

        #endregion


        #region Views

        /// <summary>
        /// The navigation drawer background
        /// </summary>
        public ImageView NavDrawerBackground { get; set; }

        /// <summary>
        /// The table view used to draw the navigation drawer options
        /// </summary>
        public RecyclerView NavDrawerSource { get; set; }

        /// <summary>
        /// The username to use on the navigation drawer
        /// </summary>
        public TextView Username { get; set; }

        /// <summary>
        /// The icon name 
        /// </summary>
        public TextView IconName { get; set; }

        #endregion

        #region Booleans

        /// <summary>
        /// Determines if the nav drawer is open 
        /// </summary>
        public bool isDrawerOpen { get; set; }

        #endregion

        public List<string> englishData = new List<string>(){

#region Business
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

            "Federal income tax","How much tax do you pay?","Stimulate growth in the economy","Lowering taxes",
            #endregion

            #region Dating 
            
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

            "My favourite novel is...","My favourite TV show is...",
            #endregion
             

            #region Emergency

               "Help!", "My wife is missing", "My husband is missing","Stop!","Thief!","He stole my wallet!","She stole my wallet!",

            "My car is stolen","My house was robbed","Where is the police station?","Call the police","Call an ambulance","I broke my leg",

            "I was robbed!","Where is the nearest hospital?","Is there a doctor here!","This person needs medical attention","Emergency services",

            "I've lost my phone","My phone is stolen","I need my lawyer","This is an emergency situation","Where is the nearest","My tire has blown out",

            "My car's engine is destroyed","Do you work for the police?","Can you help me?","Fire!","This floor is flooded","My computer is broken!","He has a bullet wound",

            "I can't find my passport!","My child is missing!","Have you seen my wallet?","I didn't do it","My computer's infected with a virus",

            "Report a missing person","I need your help!","What's your emergency..?","I'm lost","Can you tell me where I am?","I had an emergency call",

            "This place is a warzone","Flooding","Escape Hatch","He needs help!","She needs help!","Where is the escape area?","This is not a drill","There was an explosion near here",

            "How can I help you?",

            #endregion

            #region Family & Friends
                   "Nephew", "Brother", "Sister", "Father","Mother","Aunt","Uncle","Grandfather","Grandmother",

           "Niece", "Son","Daughter","This is my daughter","That is my uncle", "He is my grandfather","Family trip","Where is your niece?", "My son left the house","My sister is out working","I have not seen my grandfather in three years","This is a family trip",

            "We should celebrate christmas early this year!","Who is he?","Who are they?", "Is this your grandmother?","It's good to see you again sister","He is my younger brother","She is my younger sister","This is my older niece", "This is my great grandmother",

            "Do you enjoy the family christmas dinners?", "My grandmother has left shopping", "My family lives in America","My family is in...(Country)","I'm travelling this year to see my family","I have not seen my family 10 years","I have not seen my family in...(Amount)",

            "We're going to the movies together","My...(Family member)...is going out tonight", "I called to my cousin yesterday","I called my...(Family member)...yesterday","What have you been doing this past year?","How was work today?","How was your day?","You will not believe what happened to me today?",

            "How is your mother doing?","If your father well?","Is your...(Family member)...well?","My father is an Salesman","My grandfather is an Engineer","My grandfather works as a...(Job)","What does your...(family member)...do for a living?", "Can you drive your sister to work today?","I'm going with friends tonight?",

            "I'm going to the...(Place)...with...(family member)","I worked with my brother on this project","I worked with my...(family member)...on this project",

            "My brother is sick with the flu","My...(family member)...is sick with the flu","I see my family once a week","I see my family once a...(Time)","Do you live with your parents?","Do you live with your...(Family member)?",

            "I pay my own bills and rent","My father is well read","My brother always takes of himself","My brother preaches education",

            "My niece has recently finished college","Have you done your chores?","Have you done your homework?","You want to join the gym with me?",


            #endregion

            #region Food 

                 "I like this dish","Are you enjoying your meal","How's dinner?", "How's breakfast?", "How's lunch?","Afternoon tea","Steak",

            "Cereal", "Chicken","Spinach","Lemon","I am a great chef","Spoon","Knife","Cutlery","Do you know how to cook...","Pizza",

            "Vegetables and rice","I'm starving","I'm a vegetarian","I want to order some take away","This is tasty","This is spicy","Did you make this yourself?",

            "Salt","Sugar","Soy sauce is great for sushi","Beer","Milk and Honey","Coffee","This tea is hot","Orange juice","Watermelon",

            "Fruit smoothie","Do you drink protein shakes?","How much is this?","Glass cup","What do you call this?","Can I have a menu please?","Can I have the bill please?",

            "I don't eat meat","Can I order some..?","Apple","Fruit salad","Sultanas","Dried fruit","I like eating...","Milk","Hot","Cold","Mild","Spicy","Sweet", "Salty","Sour",

            "This is too sweet","This is too salty","Too much salt is not good","Can I have some ice with that?","A toast to you","Cake","Bread","Toast", "Yoghourt","Butter", "Ice-cream","This is my recipe",

            #endregion

            #region Gardening
                "This a beautiful garden", "Grass", "Flowers", "Roses", "Garden", "Pool", "Bushes", "Landscape", "Gardening",

            "I love the view of this place", "Root", "Gardening tools", "There's allot of weeds growing here","This is compost","I grow my own crop","Seeds","Sunflower plants","These plants need sunlight",

            "I water my plants everyday", "I use rainwater", "Impressive landscaping","Self pollinating plants","This soil is over watered","What kind of compost do you use?","Bolting vegetables","Cabbages","Greenhouse",

            "This is my greenhouse", "This is his greenhouse", "This soil needs to be renewed", "Shovel","Wheelbarrow","Trolley","Fencing tools", "Floral snips",

            #endregion

            #region General Speech 
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


            #endregion

            #region greetings
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

            "My name is...","I am 60 years old","I am 20 years old","I work as an engineer","What is your name?","Are you studying right now?", "Who are you?", "Hello", "Nice to meet you",


            #endregion

            #region Math & Numbers
                "One","Two","Three","Four","Five","Six","Seven","Eight","Nine",

            "Ten","Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen","Seventeen","Eighteen",

            "Nineteen","Twenty","Thirty","Forty","Fifty","Sixty","Seventy","Eighty","Ninety",

            "One hundred","Add","Subtract","Multiply","Divide","Add to this","Subtract from that","Divide from this","Multiply that",

            "How many?","How much?", "Which one?","Sections","Sets","Probability","Factorial","Algebra","Laplace transforms",

            "Numbers","Circle","Square","Triangle","Pyramid","Cube","Rectangle","Perimeter","Area",

            "Volume","Dimension","Length","Width","Height", "Shape","Numerator","Denominator","Fraction",

            "Measurement","Accuracy of measurement","Displacement","Distance", "Speed","Average bandwidth","Hyperbola","Parabola",
            #endregion


            #region Public Transport 
                   "Taxi", "Bus","Car","Train","Tram","Carriage","Airport","Plane","Aircraft","Jet",

            "Train station","Transit lane","Highway","Road","Ticket","Where is the next station?", "Can you drive me to the airport?",

            "Boat","Ship","Bicycle","Motorbike","Drive me to the train station","Subway","When is the next bus stop?","Rental car","How much does it cost to rent...","I'm lost",

            "Tram","I would like to buy a ticket","Monorail","How much for a taxi?","Is there a toilet on this bus?","What time does the bus get here?","When is the next train?","The next flight is in 8 hours",

            "The next flight is in...","Please stop here","Please stop at...(Address)","When will we get there?","This is the 704 bus","This is the ...(Number)...bus",

            #endregion


            #region Shopping

                 "Cash register","Shopping","Shelf","Fridge","Grocery store","Mall","How much does this cost?","Where can I find...(Item)","Do you work here?",

            "I work near the furniture section","I am looking for...(item)",

            "Battery","Electronics", "Here's your change","Where my change?","This is too expensive","This too cheap","This product is faulty",

            "This product is on display only","What is this?","What is that?","This item costs...(Amount)",

            "Which one is the plasma TV?","I want that...(item)...there","I cannot reach that item on the shelf","All this costs...(Amount)","Would you like to buy...(item)?",

            "Would you like to sample this chocolate?","Free TV for every $300 you spend","I hate shopping!","I love shopping!","I need to buy some new clothes",

            "I'm going to the mall with my friends","I'm looking for...(Shop name)","Buy 2 get one free","How much can I get for all these?","50% off sale for today only","(Number)%...off sale for tonight only",

            "I'll take 2 of those","Do you take credit card here?","We only accept cash","We accept cash and credit card","Where is the food court?","I need to withdraw cash",

            "Put it on my tab",

            #endregion

            #region Technology

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

           "Plasma cutter","3rd party network","Switching circuit","Television station", "Radio station","Radio Transmitter","Power distrupter","Battery cable","Battery charger",


            #endregion

            #region Travel
                "This is my first time in france", "This is my first time in...(Country)",

            "How many times have you visited this country?","What is there to do here?","Where can I find the nearest cinema?",

          "I love this place!","Do you like to travel?","Paris","Italy","Eiffel tower", "Pizza","There is a circus here",

            "Travelling is best done with friends", "I have been to the city many times","Have you visited the Melbourne Aquarium?","The Aquarium is so expensive",

            "This place is cheap","Thailand is cheap to go to","(Country)...has cheap apartments","Have you travelled to the forbidden city","Have you been to...",

            "What's the funnest thing to do here?","Is there a comedy show here?","There is a pool in this hotel","How much does a room cost here?","I'm staying here for a week",

            "We're staying here for a month","I'm staying in this hotel for a night","Where can I find the nearest supermarket","How much for these?","Singapore is a clean place to visit",

            "I have some euros in my pocket","I don't speak french","I speak french very well", "How many languages can you speak?","I need a new camera","Take a photo of us, please",

            "How long does it take to get there?", "Do you have any movies on this flight?","Can you bring us some champagne?","Museum of Arts","Historical Museum",


            #endregion

            #region Work
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

#endregion
        };



        string[] frenchData = new string[] {

#region Business
            "Je suis un pigiste", "Je travaille à temps partiel ici", "Je travaille à temps plein ici", "Puis-je faire un rendez-vous", "Est-ce que tu travailles ici?",

            "Il est mon patron", "Je suis le gestionnaire ici","Où puis-je trouver le gestionnaire?","Où puis -je trouver le poste de travail le plus proche?","Employé","Patron","Employeur",

            "Je possède cette société","Qui est responsable ici?","Je travaille ici depuis 20 ans","Compagnie","Entreprise","Je suis un homme d'affaire","Réalisateur","Équipe de vente",

            "Programmeur","Programmeur senior","Agence d'emploi","Ceci est mon agence de publicité","Ceci est mon site","Ceci est l'Internet","J'ai conçu cette...","Merci pour l'interview",

            "Nous vous offrons le poste","Bienvenue à...","Directeur commercial","Cher Monsieur / Madame...","Cher Monsieur / Madame...","Nous aimerions embaucher...","J'ai besoin de vos services",

            "J'ai lu votre lettre","Directeur Général","Réceptionniste","Je veux un emploi ici","Embauchez-vous?","Avez-vous des postes vacants?","Actuellement, je travaille pour...","Je travaille sur une base occasionnelle",

            "Retour sur investissement","Le profit","Coût","Revenu","Client","Email","Ceci est mon site","Je suis un développeur web", "Je suis un ingénieur iOS",

            "Je fais des applications pour une vie","cher...","À qui cela concerne...","Mon revenu annuel est...","je vends...","Nous vendons...","Cette société produit...","Nous sommes une société de logiciels",

            "J'ai une réunion avec...","J'ai un rendez-vous à...","Chauffeur","Superviseur","Stagiaire","Impôt sur le revenu","État taxes","Taxe fédérale sur le revenu","Combien d'impôts payez -vous?",

            "Stimuler la croissance dans l'économie", "Réduire les impôts",

            #endregion

#region Dating
            
            "je t'aime", "Ravi de vous rencontrer enfin", "Pouvez-vous nous donner un menu s'il vous plaît", "Avez-vous des tables?", "Aimez-vous vos spaghettis?", "Que faire?",

            "Ceci est mon premier rendez-vous", "Avez- vous visitez ce lieu souvent?", "Nous allons faire cela à nouveau tommorrow", "Je vais vous chercher à 8:00", "Comment vas-tu ces derniers temps?", "Comment avez-vous commencé votre carrière?",

            "Quels sont vos passe-temps?", "Êtes-vous intéressé à l'art et à la culture?", "Je suis intéressé par la science et de la technologie", "Alors que diriez- vous?", "Aimez-vous votre travail", "Je travaille 12 heures par jour comme un ingénieur logiciel",

            "Voyagez-vous souvent?", "J'ai voyagé à Paris, Singapour et madrid", "Cela semble incroyable!",

            "Je l'avais allouer de ce soir amusant", "Vous voulez venir à l'étage?","Je sais que ce grand bar à sushis près d'ici",

            "Nous devrions certainement le faire à nouveau", "je t'appellerai", "Qu'avez-vous fait à paris?", "Je suis allé à la tour eiffel", "Puis-je avoir le projet de loi s'il vous plaît, serveur?",

            "Je vais prendre le projet de loi", "Vraiment, il est ok, je vais payer la facture", "La chose dont je suis le plus fier...", "Je suis fier de mon travail et de la vie","Veux-tu m'épouser?","Prévoyez- vous d'avoir une famille?",

            "Est-ce que vous appréciez votre repas?", "Nous allons commander du vin", "La musique ici est agréable", "j'ai 30 ans", "C'est mon anniversaire aujourd'hui","Prenez soin de vous","À la prochaine","Que comptez-vous faire à l'avenir",

            "Lisez-vous des livres", "Quel est votre film préféré?", "Quelle est la chose la plus audacieuse que vous avez fait?", "Qui était votre pire jour?","Qui était votre meilleur jour?","Aimez-vous les chats ou les chiens?","Avez-vous des animaux domestiques?",

            "J'ai deux tortues", "Mon roman préféré est...", "Mon émission préférée est...",
            #endregion

            #region Emergency
                  "Aidez-moi!", "Ma femme est absente", "Mon mari est absent", "Arrêtez!","Voleur!","Il a volé mon portefeuille!","Elle a volé mon portefeuille","Ma voiture est volée",

            "Ma maison a été volé","Ou est la station de police?","Appelle la police","Appelle une ambulance","Je me suis cassé la jambe","J'ai été volé!","Où se trouve l'hôpital le plus proche?",

            "Y at-il un médecin ici!","Cette personne a besoin d'attention médicale", "Services d'urgence","J'ai perdu mon téléphone","Mon téléphone est volé","Je dois mon avocat", "Ceci est une situation d'urgence",

            "Où est le plus proche","Mon pneu a soufflé","Le moteur de ma voiture est détruite","Travaillez-vous pour la police?","Pouvez-vous m'aider?","Feu!","Cet étage est inondé","Mon ordinateur est cassé!","Il a une blessure par balle",

            "Je ne peux pas trouver mon passeport!", "Mon enfant a disparu !","Avez-vous vu mon portefeuille ?","Je ne le fais pas", "Mon ordinateur est infecté par un virus",

            "Signaler une personne disparue", "J'ai besoin de ton aide!","Quelle est votre urgence .. ?","je suis perdu","Pouvez- vous me dire où je suis?","J'ai eu un appel d'urgence","Cet endroit est une zone de guerre","Inondation",

            "Évadez- Hatch", "Il a besoin d'aide!","Elle a besoin d'aide !","Où se trouve la zone d'évacuation?","Ce n'est pas une perceuse","Il y avait une explosion près d'ici",

            "Comment puis-je t'aider?",

            #endregion

            #region Family and friends
               "Neveu","Frère", "Sœur", "Père","Mère","Tante","Oncle","Grand-père","Grand-mère","Nièce","Fils","Fille","C'est ma fille", "Voilà mon oncle","Il est mon grand-père","voyage en famille","Where is your niece?","Mon fils a quitté la maison",

           "Ma sœur est hors travail","Je ne l'ai pas vu mon grand-père en trois ans","Ceci est un voyage en famille","Nous devrions célébrer Noël au début de cette année!","Qui est-il?","Qui sont-ils?",

           "Est-ce votre grand-mère?","Il est bon de vous revoir soeur","Il est mon frère cadet", "Elle est ma sœur cadette","Ceci est ma nièce plus âgée","Ceci est mon arrière grand-mère","Aimez-vous la famille des dîners de Noël?",

            "Ma grand-mère a quitté le shopping","Ma famille vit en Amérique", "Ma famille est en ...","I'm travelling this year to see my family","Je ne l'ai pas vu ma famille 10 ans","Je ne l'ai pas vu ma famille dans ...","Nous allons au cinéma ensemble","Mon ...... va sortir ce soir",

            "J'ai appelé à mon cousin hier","J'ai appelé mon...","Qu'est-ce que tu as fait cette dernière année?","Comment était le travail aujourd'hui?","Comment était ta journée?","Vous ne serez pas croire ce qui est arrivé à moi aujourd'hui?","Comment votre mère est en train de faire?","Si bien votre père?",

            "Est ton...","Mon père est un Salesman","Mon grand-père est ingénieur","Mon grand-père travaille comme...","Qu'est-ce que votre ... faire pour vivre?","Pouvez-vous conduire votre soeur au travail aujourd'hui?","Je vais avec des amis ce soir?","Je vais le...avec...","Je travaillais avec mon frère sur ce projet",

            "Je travaillais avec mon ... sur ce projet","Mon frère est malade avec la grippe","Mon ... est malade avec la grippe","Je vois ma famille une fois par semaine",

            "Je vois ma famille une fois...","Vivez-vous avec vos parents?","vous vivez avec votre ... Est-ce que", "I pay my own bills and rent","Mon père est bien lu","Mon frère a toujours de lui-même","Mon frère prêche l'éducation","Ma nièce a récemment terminé ses études","Avez-vous fait vos tâches?","Avez-vous fait vos devoirs?",

            "Vous souhaitez rejoindre la salle de gym avec moi?",
            #endregion

            #region Food
                "J'aime ce plat","Êtes-vous profiter de votre repas","Comment est le dîner?","Comment est le petit déjeuner?", "Comment est le déjeuner?","Le thé de l'après-midi","Steak","Céréale",

            "poulet","épinard","citron","Je suis un grand chef","Cuillère","Couteau","Coutellerie","Savez-vous comment faire cuire...",

            "Pizza","Les légumes et le riz","je meurs de faim","Je suis un végétarien", "Je veux commander quelque emporter","Ceci est savoureux","Ceci est épicé","Avez-vous fait vous-même?","Sel",

            "Sucre","La sauce de soja est excellent pour sushi","Bière","Lait et miel","café","Ce thé est chaud","du jus d'orange",

            "Pastèque","Smoothie aux fruits","Vous buvez shakes de protéines?","Combien ça coûte?","Coupe en verre","Comment appelles-tu ceci?","Puis-je avoir un menu s'il vous plaît?","Puis-je avoir la note s'il vous plaît?",

            "Je ne mange pas de viande","Puis-je commander un certain..?","pomme","Salade de fruit","Sultanes","Fruit sec","J'aime manger...","Lait",

            "Chaud", "Du froid","Doux","Épicé","Doux","Salé","Acide","Ceci est trop sucré","Ceci est trop salée","Trop de sel est pas bon","Puis-je avoir un peu de glace avec ça?",

           "Un toast à vous","gâteau","Pain","Pain grillé","Yaourt","beurre","Crème glacée","Ceci est ma recette",
            #endregion

            #region Gardening
           
            

                "Cet un beau jardin","Herbe","Fleurs","Des roses","Jardin","Piscine","Des buissons","Paysage", "Jardinage",

            "J'adore la vue de ce lieu","Racine","Outils de jardinage","Il y a Allot des mauvaises herbes qui poussent ici","Ceci est le compost","Je cultive ma propre culture","Graines","Les plants de tournesol","Ces plantes ont besoin de lumière",

            "J'arrose mes plantes tous les jours","J'utilise l'eau de pluie","aménagement paysager impressionnant","plantes auto pollinisateurs","Ce sol est plus arrosé","Quel genre de compost utilisez-vous?", "boulonnage légumes", "Choux","Serre",

            "Ceci est ma serre", "Ceci est sa serre","Ce sol a besoin d'être renouvelé","Pelle","Brouette","Chariot","outils d'escrime","cisailles Floral",

            #endregion

            #region General Speech
                 "Qui es-tu?","Quel est ton nom?","Vous sentez-vous bien?","Tu semble malade","Je dois allonger pendant un certain temps","À demain",

           "Où où vous hier?","À ce soir","Comment était ta journée ?","Comment était le travail?","Vous restez avec nous ?","J'ai entendu aujourd'hui que ...",

            "Je vis...","Tommorrow Je vais voir ...","La semaine prochaine est mon anniversaire","Félicitations pour votre entreprise","Eh bien fait sur votre ...","Allez-vous ...","Voulez-vous sortir?","je sors","Je vais...",

            "Pouvez-vous parler anglais?","Pouvez vous parler...","Attends une minute","Attendre...","Quand votre entretien d'embauche ?","Aimez-vous votre travail?","Cela fait longtemps que nous nous sommes vus",

            "Comment avez-vous été dans les derniers mois?","Quelle est votre émission préférée?","Étudiez-vous?","Qu'est-ce que vous étudiez?","J'étudie l'ingénierie","J'étudie entreprise","J'étudie...","Ravi de vous rencontrer", "Tu ne m'as pas appelé",

            "Ça fait trop longtemps","J'aime cette musique","J'aime cela...","je me sens bien","je suis en bonne santé","Je viens d'Amérique","Je viens du Brésil",

            "Je viens de...","j'ai 40 ans","Pouvez-vous parler plus fort?","Pouvez-vous murmurer?","Il est très fort ici","Quelle?","Combien?","Pourquoi?","Qui?","Où?","Quand?","Qui était-il?","Pourquoi fais-tu ça?",

            "Qui est-elle?","Qui est-il?","Quand allez-vous y arriver?","Quelle heure est-il?",

           "Pourquoi es-tu ici?",

            #endregion

            #region Greetings
              
              "Oui", "Non", "Me comprenez-vous?","Je ne comprends pas","Je comprends parfaitement","Je vous remercie. CA va bien","j'ai besoin de ton aide","S'il vous plaît","Je suis désolé","S'il vous plaît vous répétez",

            "Pouvez-vous parler un peu plus lent ?","Non, merci", "Félicitations à vous","Je suis désolé pour toi","C'est bien","Je ne sais pas","Je n'aime pas cette","J'aime beaucoup cela","De rien","Je comprends que...",

           "Je pense que...","J'en veux...","J'aime...","Puis-je emprunter votre téléphone","Es-tu sûr de ça?","Parlez-vous allemand?","Qu'est-ce que ça veut dire?","Comment prononcez-vous cette phrase ?","Non merci! CA va bien","Non c'est faux",

           "C'est vrai","Pas de problème","Aidez-moi!","Qui?","Quelle?",

            "Combien?","Combien?","Pourquoi?","Pourquoi pas?","Lequel?","Où?","Quand?","Quel mois est votre anniversaire ?", "Que font plus tard?","Où allez-vous?","Je vais...","Qu'est-ce que c'est ça?","Vous sentez-vous bien?",

           "Ne pas oublier ...","Excusez-moi","Faites attention à vous","Où puis -je trouver l'ascenseur?","Cela fait longtemps","Vous voulez parler?","J'adore votre voiture","Allez-vous souvent ici","Comment gagnez-vous votre vie?","Je ne vous ai jamais vu ici avant","Ravi de vous voir",

            "De quel pays êtes-vous?","Mon nom est...","Je suis âgé de 60 ans","j'ai 20 ans","Je travaille comme un ingénieur","Je travaille comme un ingénieur", "Est-ce que vous étudiez en ce moment?","Qui es-tu?","Bonjour","Ravi de vous rencontrer",


            #endregion

            #region Math & Nmumbers
                "Un","Deux","Trois","Quatre","Cinq", "Six", "Sept","Huit","Neuf",

            "Dix","Onze","Douze","Treize","Quatorze","Quinze","Seize","Dix-sept","Dix-huit",

            "Dix-neuf","Vingt","Trente","Quarante","Cinquante","Soixante","Soixante-dix","Quatre-vingts","Quatre vingt dix",

            "Cent","Ajouter","Soustraire","Multiplier","Diviser","Ajoutez à cela","Soustraire de ce","Divide de cette","Multipliez ce",

            "Combien","Combien?","Laquelle?","le tronçon","ensemble","Probabilité","factorielle","Algèbre","transformées de Laplace",

            "Nombres","Cercle","Carré","la équerre","Pyramide","le cube", "Rectangle","Périmètre","Région",

            "Le volume","Dimension","Longueur","Largeur","la taille","Forme","Numérateur","Dénominateur","Fraction",

           "La mesure","Précision de la mesure","Déplacement","Distance", "La vitesse","bande passante moyenne","Hyperbole","Parabole",
            #endregion

            #region Public Transport
            
                "Taxi","Autobus","Voiture","Entrainer","Tram","Le chariot","Aéroport","Avion","Avion","Jet","Gare","voie de transit","Autoroute","Route",

            "Billet","Où est la prochaine station?","Pouvez-vous me conduire à l'aéroport?","Bateau","Navire","Vélo","Moto","Conduisez-moi à la gare","Métro","Quand arrêter le prochain bus?","Voiture de location","Combien coûte la location ...",

            "je suis perdu","Tram","Je voudrais acheter un billet","Monorail",

            "Combien pour un taxi?","Y at-il une toilette sur ce bus?","À quelle heure le bus arriver ici?","Quand part le prochain train?","Le prochain vol est en 8 heures","Le prochain vol est en ...","S'il vous plaît arrêter ici","S'il vous plaît arrêter à ...","Quand allons-nous y arriver?",

            "Ceci est le bus 704","C'est le ... Autobus",
            #endregion

            #region Shopping
                    "Caisse enregistreuse","Achats","Étagère","Frigo","Épicerie","Centre commercial","Combien cela coûte?",

            "Où puis-je trouver...","Est-ce que tu travailles ici?","Je travaille près de la section de meubles","Je cherche...",

            "les accumulateurs","Électronique","Voilà votre monnaie","Lorsque mon changement?","Ceci est trop cher",

            "Ce trop pas cher","Ce produit est défectueux","Ce produit est en affichage seulement","Qu'est-ce que c'est?","Qu'est-ce que c'est?","Cet article coûte...",

            "Lequel est le téléviseur à écran plasma?","Je veux que ... il","Je ne peux pas accéder à cet article sur le plateau","Tout cela coûte...","Voulez-vous acheter...","Voulez-vous goûter ce chocolat?",

           "Free TV pour chaque 300 $ que vous dépensez","Je déteste le shopping!","J'adore le shopping!","Je besoin d'acheter de nouveaux vêtements","Je vais au centre commercial avec mes amis",

           "Je cherche...","Acheter 2 obtenir un gratuitement","Combien puis-je obtenir pour tous ces?","50% off vente pour aujourd'hui seulement","... Hors vente pour ce soir seulement", "Je vais prendre 2 de ceux",

            "Prenez-vous la carte de crédit ici?","Nous acceptons uniquement les espèces","Nous acceptons l'argent comptant et carte de crédit","Où est la cour de nourriture?","Je dois retirer de l'argent",

            "Mettez-le sur mon onglet",

            #endregion

            #region Technology 
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

            "3ème réseau de parti","circuit de commutation","Poste de télévision", "Station de radio","Transmetteur Radio","Puissance distrupter","câble de batterie","Chargeur de batterie",


            #endregion

            #region Travel
                 "Ceci est ma première fois en France","Ceci est ma première fois en ...","Combien de fois avez-vous visité ce pays?", "Qu'est-ce qu'il ya à faire ici?","Où puis-je trouver le cinéma le plus proche?","J'adore cet endroit!",

            "Aimes-tu voyager?","Paris","Italie","Tour Eiffel","Pizza","Il y a un cirque ici","Voyager est le mieux fait avec des amis","I have been to the city many times","Avez-vous visité l'aquarium de Melbourne?","L' Aquarium est si cher",

            "Cet endroit est pas cher","La Thaïlande est pas cher pour aller à","... A des appartements bon marché","Avez-vous voyagé à la ville interdite","Avez-vous été à...","Quelle est la funnest chose à faire ici?",

            "Y at-il un spectacle de comédie ici?","Il y a une piscine dans cet hôtel",

            "Combien une chambre coûte ici?","Je reste ici pendant une semaine","Nous restons ici pour un mois","Je reste dans cet hôtel pour une nuit", "Où puis-je trouver le supermarché le plus proche","Combien pour ceux-ci?","Singapour est un endroit propre à visiter",

            "J'ai quelques euros dans ma poche","Je ne parle pas français","Je parle français très bien","Combien de langues pouvez-vous parler?","Je besoin d'un nouvel appareil photo","Prenez une photo de nous, s'il vous plaît","Combien de temps cela prend-il pour s'y rendre?","Avez-vous des films sur ce vol?", "Pouvez-vous nous apporter un peu de champagne?",

            "Musée des Arts","Musée historique",
            #endregion

            #region Work
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
#endregion
        };

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.StandardMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (featureId)
            {
                case 0:
                    Console.WriteLine("Item 1 Selected");

                    break;
                case 1:
                    Console.WriteLine("Item 2 Selected");

                    break;

                case 2:
                    Console.WriteLine("Item 3 Selected");

                    break;
            }

            return true;
        }

        public void GenerateDatase()
        {
            string filePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data.db");

            Database_Models.DatabasePath = filePath;
            Database_Models.DeviceID = 0;

            try
            {
                if (File.ReadAllBytes(filePath) == null)
                {
                    throw new NullReferenceException("");
                }
            }
            catch
            {
                File.Create(filePath);

            }

            CRUDOperations.GenerateDatabase(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Acvtion Bar

            //Database
            GenerateDatase();

            CRUDOperations.GetFavourites(new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(Database_Models.DatabasePath, false)));


            this.SetContentView(Resource.Layout.MainPageScene);

            var actionBar = FindViewById<TextView>(Resource.Id.ToolbarTitle);
            actionBar.Text = "Tongue Twister!";



            #region Content 
            var contentPage = FindViewById<RecyclerView>(Resource.Id.CategoryCard_TableView);

            contentPage.SetLayoutManager(new LinearLayoutManager(this));
            contentPage.SetAdapter(new MainPage_Adapter(this));

            #endregion

            #region Navigation Drawer
            var frameLayout = FindViewById<DrawerLayout>(Resource.Id.NavDrawerView);


            var navDrawer = FindViewById<RecyclerView>(Resource.Id.NavDrawer);

            navDrawer.SetLayoutManager(new LinearLayoutManager(this));
            navDrawer.SetAdapter(new NavDrawer_Adapter(this));


            var navButton = FindViewById<ImageButton>(Resource.Id.NavDrawerButton);

            navButton.Click += (sender, e) =>
            {
                if (this.isDrawerOpen == false)
                {
                    frameLayout.OpenDrawer(navDrawer, true);

                    this.isDrawerOpen = true;

                    return;
                }
                else
                {
                    frameLayout.CloseDrawer(navDrawer, true);

                    this.isDrawerOpen = false;

                    return;
                }
            };
            #endregion

          
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            //
        }
    }
}