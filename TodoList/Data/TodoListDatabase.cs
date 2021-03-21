namespace TodoList
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using SQLite;
    
    public class TodoListDatabase
    {
        private const string DatabaseFilename = "com.kseniyagaripova.todolist.db3";

        private const SQLiteOpenFlags DatabaseFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        private static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
        
        private static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoListDatabase> Instance = new AsyncLazy<TodoListDatabase>(async () =>
        {
            var instance = new TodoListDatabase();
            await Database.CreateTableAsync<TodoItem>();
            return instance;
        });

        private TodoListDatabase()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, DatabaseFlags);
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {            
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.Id != 0)
            {
                await Database.UpdateAsync(item);
            }
            else
            {
                await Database.InsertAsync(item);
            }
            
            return item.Id;
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}