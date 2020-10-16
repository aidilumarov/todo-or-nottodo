using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoOrNotToDo.Models;

namespace ToDoOrNotToDo.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;
        public event EventHandler<TodoItem> OnItemDeleted;

        public Task AddItem(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateItem(TodoItem item)
        {
            if (item.Id == 0)
            {
                return AddItem(item);
            }

            return UpdateItem(item);
        }

        public Task<List<TodoItem>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task UpdateItem(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}
