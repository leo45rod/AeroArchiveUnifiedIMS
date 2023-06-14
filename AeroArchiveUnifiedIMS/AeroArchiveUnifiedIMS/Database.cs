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
            _database.CreateTableAsync<Employee>();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }

        public Task<int> ClearProductsDatabaseAsync()
        {
            return _database.DeleteAllAsync<Product>();
        }

        public Task<int> UpdateProductAsync(Product product)
        {
            return _database.UpdateAsync(product);
        }

        public Task<List<Employee>> GetEmployeeAsync()
        {
            return _database.Table<Employee>().ToListAsync();
        }

        public Task<int> SaveEmployeeAsync(Employee employee)
        {
            return _database.InsertAsync(employee);
        }

        public Task<int> ClearEmployeeDatabaseAsync()
        {
            return _database.DeleteAllAsync<Employee>();
        }

        public Task<int> UpdateEmployeeAsync(Employee employee)
        {
            return _database.UpdateAsync(employee);
        }
    }
}