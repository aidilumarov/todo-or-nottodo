using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ToDoOrNotToDo.Models;
using ToDoOrNotToDo.Repositories;
using Xamarin.Forms;

namespace ToDoOrNotToDo.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        public event EventHandler ItemStatusChanged;

        public TodoItem TodoItem { get; set; }

        public ICommand ToggleCompleted => new Command((arg) =>
        {
            TodoItem.Completed = !TodoItem.Completed;
            ItemStatusChanged?.Invoke(this, new EventArgs());
        });

        public ICommand DeleteItem => new Command(async () =>
        {
            await Resolver.Resolve<TodoItemRepository>().DeleteItem(TodoItem);
        });

        public TodoItemViewModel(TodoItem todoItem)
        {
            TodoItem = todoItem;
        }
    }
}
