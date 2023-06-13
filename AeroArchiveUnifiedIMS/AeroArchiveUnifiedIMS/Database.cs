using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroArchiveUnifiedIMS
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(String dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }

        public Task<int> ClearDatabaseAsync()
        {
            return _database.DeleteAllAsync<Product>();
        }
    }
}