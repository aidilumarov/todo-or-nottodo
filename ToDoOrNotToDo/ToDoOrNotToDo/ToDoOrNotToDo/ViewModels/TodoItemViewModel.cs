using System;
using System.Collections.Generic;
using System.Text;
using ToDoOrNotToDo.Models;

namespace ToDoOrNotToDo.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        public event EventHandler ItemStatusChanged;

        public TodoItem TodoItem { get; set; }

        public string StatusText => TodoItem.Completed ? "Reactivate" : "Completed";

        public TodoItemViewModel(TodoItem todoItem)
        {
            TodoItem = todoItem;
        }
    }
}
