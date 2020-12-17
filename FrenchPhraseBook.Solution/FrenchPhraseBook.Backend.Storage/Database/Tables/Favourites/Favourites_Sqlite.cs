using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite.Net.Attributes;
 
namespace FrenchPhraseBook.Backend.Storage.Database.Tables.Favourites
{
    public class Favourites_Sqlite
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// the english text
        /// </summary>
        public string EnglishText { get; set; }

        /// <summary>
        /// The french text
        /// </summary>
        public string FrenchText { get; set; }

        /// <summary>
        /// Whether the phrase is favourited or not
        /// </summary>
        public bool IsFavourite { get; set; }

        #region Unique Ids
        /// <summary>
        /// The mapping ID used for in memory runtime updates
        /// </summary>
        public int ViewHolderID { get; set; }

        /// <summary>
        /// The phrase ID
        /// </summary>
        public string PhraseId { get; set; }

        /// <summary>
        /// The device ID 
        /// </summary>
        public int DeviceId { get; set; }
        #endregion
    }
}
