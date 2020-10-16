using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoOrNotToDo.Models;

namespace ToDoOrNotToDo.Repositories
{
    public interface IRepository<T> where T : IItem
    {
        event EventHandler<T> OnItemAdded;
        event EventHandler<T> OnItemUpdated;
        event EventHandler<T> OnItemDeleted;

        Task<List<T>> GetItems();
        Task AddItem(T item);
        Task UpdateItem(T item);
        Task AddOrUpdateItem(T item);
    }
}
