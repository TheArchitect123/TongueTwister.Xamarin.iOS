using System;
using System.Diagnostics;

using System.Threading;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Text;

#region Database
using FrenchPhraseBook.Backend.Storage.Database.Tables.Favourites;
using FrenchPhraseBook.Backend.Storage.Database.Tables.Users;
#endregion

#region Models
using FrenchPhraseBook.Backend.Storage.Models;

using FrenchPhraseBook.Backend.Storage.Database.Tables.Search;
#endregion

#region Sqlite
using SQLite;
using SQLite.Net;
#endregion

namespace FrenchPhraseBook.Backend.Storage
{
    public class CRUDOperations
    {
        public static async Task CreateAccount()
        {

        }

        #region Search 
        public static async void GenerateVirtual(SQLiteConnectionWithLock connection)
        {
           
             
        }

        public static List<SearchTable> SearchResults(string search, SQLiteConnectionWithLock connection)
        {
            return connection.Table<SearchTable>().Where(i => i.EnglishText == search).ToList();
        }

        public static async void GenerateSearchOptions(SearchTable search, SQLiteConnectionWithLock connection)
        {
            connection.Insert(new SearchTable() {
                 EnglishText = search.EnglishText,
                 FrenchText = search.FrenchText,
                 isFavourited = false
            });
        }

        #endregion

        public static async void GenerateDatabase(SQLiteConnectionWithLock connection)
        {
            using (connection)
            {
                connection.CreateTable<Favourites_Sqlite>();
                connection.CreateTable<SearchTable>();
                connection.CreateTable<Users_Sqlite>();
            }
        }

        #region Favourites
        /// <summary>
        /// gets the list of favourites
        /// </summary>
        /// <returns></returns>
        public static async void GetFavourites(SQLiteConnectionWithLock connection)
        {
            using (connection)
            {
                Favourites_Model.GetFavourites = connection.Table<Favourites_Sqlite>().ToList();
            }
        }

        /// <summary>
        /// Sets the favourites
        /// </summary>
        /// <returns></returns>
        public static async void SetFavourites(Favourites_Sqlite data, SQLiteConnectionWithLock connection)
        {
            using (connection)
            {
                Random random = new Random();

                try
                {
                    var query = connection.Table<Favourites_Sqlite>().ToList().Find(i => i.PhraseId == data.PhraseId);

                    query.DeviceId = data?.DeviceId ?? random.Next(1, 10000);

                    query.EnglishText = data?.EnglishText ?? "";
                    query.FrenchText = data?.FrenchText ?? "";
                    query.IsFavourite = (bool?)data?.IsFavourite ?? false;
                    query.PhraseId = data?.PhraseId ?? "";
                    query.ViewHolderID = data?.ViewHolderID ?? random.Next(1, 10000);

                    connection.RunInTransaction(() =>
                    {
                        connection.Update(query);
                    });
                }
                catch
                {
                    connection.Insert(new Favourites_Sqlite()
                    {
                        DeviceId = data?.DeviceId ?? random.Next(1, 10000),
                        EnglishText = data?.EnglishText ?? "",
                        FrenchText = data?.FrenchText ?? "",
                        IsFavourite = data?.IsFavourite ?? false,
                        PhraseId = data?.PhraseId ?? ""
                    });
                }
            }
        }

        public static async void SetDeleteFavourites(Favourites_Sqlite data, SQLiteConnectionWithLock connection)
        {

            Random random = new Random();


            var query = connection.Table<Favourites_Sqlite>().ToList().Find(i => i.PhraseId == data.PhraseId);


            connection.RunInTransaction(() =>
            {
                connection.Delete(query);

            });


            #endregion

        }
    }
}
