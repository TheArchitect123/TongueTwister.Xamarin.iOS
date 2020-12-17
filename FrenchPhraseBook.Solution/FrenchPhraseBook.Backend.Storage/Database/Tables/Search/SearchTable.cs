using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite.Net.Attributes;

namespace FrenchPhraseBook.Backend.Storage.Database.Tables.Search
{
    public class SearchTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The english text
        /// </summary>
        public string EnglishText { get; set; }

        /// <summary>
        /// The french text 
        /// </summary>
        public string FrenchText { get; set; }

        /// <summary>
        /// Whether the phrase is favourited or not
        /// </summary>
        public bool isFavourited { get; set; }
        
    }
}
