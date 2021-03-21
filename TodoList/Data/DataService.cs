using TodoList;
using Xamarin.Forms;
[assembly: Dependency(typeof(DataService))]
namespace TodoList
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public class DataService : IDataService
    {
        public DataService()
        {
        }

        public async Task<List<TodoItem>> GetItems()
        {
            var db = await TodoListDatabase.Instance;
            return await (await TodoListDatabase.Instance).GetItemsAsync();
        }
        
        public async Task<TodoItem> GetItem(int id)
        {
            return await (await TodoListDatabase.Instance).GetItemAsync(id);
        }
        
        public async Task<int> SaveItem(TodoItem item)
        {
            return await (await TodoListDatabase.Instance).SaveItemAsync(item);
        }
        
        public async Task DeleteItem(int id)
        {
            var itemToDelete = await this.GetItem(id);
            if (itemToDelete != null)
            {
                await (await TodoListDatabase.Instance).DeleteItemAsync(itemToDelete);
            }
        }
    }
}