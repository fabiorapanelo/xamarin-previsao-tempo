using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppPrevisaoDoTempo
{
    class LocalDatabase
    {
        readonly SQLiteAsyncConnection database;

        public LocalDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Local>().Wait();
        }

        public Task<List<Local>> GetItemsAsync()
        {
            return database.Table<Local>().ToListAsync();
        }


        public Task<Local> GetItemAsync(int id)
        {
            return database.Table<Local>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Local item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Local item)
        {
            return database.DeleteAsync(item);
        }
    }
}
