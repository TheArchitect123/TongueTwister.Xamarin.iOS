using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrenchPhraseBook.Backend.Storage.Models.Database
{
    public class Database_Models
    {
        /// <summary>
        /// The database connection path 
        /// </summary>
        public static string DatabasePath { get; set; }

        /// <summary>
        /// The device ID 
        /// </summary>
        public static int DeviceID { get; set; } 
    }
}
