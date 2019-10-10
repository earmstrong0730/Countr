using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Countr.Core.Models;
using PCLStorage; // A using directive to bring in the file storage plugin
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

//Setting up the connection to a SQLite database

namespace Countr.Core.Repositories
{
    public class CountersRepository : ICountersRepository //This class implemens the ICountersRepository interface
    {

        readonly SQLiteAsyncConnection connection; //This is the connection to the datbase -async so you can used async/await

        public CountersRepository()
        {
            var local = FileSystem.Current.LocalStorage.Path; //This path comes from the file plugin and provides the path to the OS-secific local storage
            var datafile = Path.Combine(local, "counterrs.db3");
            connection = new SQLiteAsyncConnection(datafile); //SQLite connections are created pointing to the database file 
            connection.GetConnection().CreateTable<Counter>(); //CreateTable will look for a table that matches the given type, and create it if it doesn't exist
        }


        //new code from page 219
        public Task Save(Counter counter)
        {
            return connection.InsertOrReplaceAsync(counter); 
   }

        public Task<List<Counter>> GetAll()
        {
            return connection.Table<Counter>().ToListAsync(); 
        }

        public Task Delete(Counter counter)
        {
            return connection.DeleteAsync(counter); 
        }

    }

}


