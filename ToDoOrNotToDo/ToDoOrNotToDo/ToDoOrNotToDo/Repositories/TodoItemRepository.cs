using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoOrNotToDo.Models;
using SQLite;
using System.IO;

namespace ToDoOrNotToDo.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private SQLiteAsyncConnection _connection;
        private const string _databaseName = "TodoItems.db";

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;
        public event EventHandler<TodoItem> OnItemDeleted;

        public async Task AddItem(TodoItem item)
        {
            await CreateConnection();
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public Task AddOrUpdateItem(TodoItem item)
        {
            if (item.Id == 0)
            {
                return AddItem(item);
            }

            return UpdateItem(item);
        }

        public async Task<List<TodoItem>> GetItems()
        {
            await CreateConnection();
            return await _connection.Table<TodoItem>().ToListAsync();

        }

        public async Task<List<TodoItem>> GetActiveItems()
        {
            await CreateConnection();
            return await _connection.Table<TodoItem>().Where(x => !x.Completed).ToListAsync();
        }

        public async Task UpdateItem(TodoItem item)
        {
            await CreateConnection();
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        private async Task CreateConnection()
        {
            if (_connection != null)
            {
                return;
            }

            var documentPath = Environment
                .GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, _databaseName);

            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<TodoItem>();

            if (await _connection.Table<TodoItem>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new TodoItem()
                {
                    Title = "Welcome to the To Do or Not To Do",
                    Due = DateTimeOffset.Now
                });
            }
        }
    }
}
