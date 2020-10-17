using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ToDoOrNotToDo.Models;
using ToDoOrNotToDo.Repositories;
using Xamarin.Forms;

namespace ToDoOrNotToDo.ViewModels
{
    public class AddUpdateItemViewModel : BaseViewModel
    {
        private readonly TodoItemRepository _todoRepository;

        public TodoItem TodoItem { get; set; }

        public ICommand Save => new Command(async () =>
        {
            await _todoRepository.AddOrUpdateItem(TodoItem);
            await Navigation.PopAsync();
        });

        public AddUpdateItemViewModel(TodoItemRepository todoRepository)
        {
            _todoRepository = todoRepository;
            TodoItem = new TodoItem() { Due = DateTimeOffset.Now.AddDays(1) };
        }
    }
}
