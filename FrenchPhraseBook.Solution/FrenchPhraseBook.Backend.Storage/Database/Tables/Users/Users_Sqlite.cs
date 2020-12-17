using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using SQLite.Net.Attributes;

namespace FrenchPhraseBook.Backend.Storage.Database.Tables.Users
{
    public class Users_Sqlite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int DeviceID { get; set; }
    }
}
