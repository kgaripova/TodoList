namespace TodoList
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataService
    {
        Task<List<TodoItem>> GetItems();
        Task<TodoItem> GetItem(int id);
        Task<int> SaveItem(TodoItem item);
        Task DeleteItem(int id);
    }
}