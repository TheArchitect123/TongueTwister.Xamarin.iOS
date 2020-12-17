using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FrenchPhraseBook.Backend.Storage.Database.Tables.Favourites;
using FrenchPhraseBook.Backend.Storage.Database.Tables.Search;

namespace FrenchPhraseBook.Backend.Storage.Models
{
    public class Favourites_Model
    {
        /// <summary>
        /// The list of favourites 
        /// </summary>
        public static List<Favourites_Sqlite> GetFavourites { get; set; } = new List<Favourites_Sqlite>() { };

        /// <summary>
        /// The search results
        /// </summary>
        public static List<SearchTable> GetSearch { get; set; } = new List<SearchTable>() { };

        /// <summary>
        /// The device Id used to uniquely identify a device
        /// </summary>
        public static int DeviceId { get; set; }
    }
}
