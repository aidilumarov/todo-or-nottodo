using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoOrNotToDo.Models;
using ToDoOrNotToDo.Repositories;
using ToDoOrNotToDo.Views;
using Xamarin.Forms;

namespace ToDoOrNotToDo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TodoItemRepository _todoRepository;

        public ObservableCollection<TodoItemViewModel> TodoItems { get; set; }

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<AddUpdateItemView>();
            await Navigation.PushAsync(itemView);
        });

        public MainViewModel(TodoItemRepository todoRepository)
        {
            _todoRepository.OnItemAdded += (sender, item) =>
            {
                TodoItems.Add(CreateTodoItemViewModel(item));
            };

            _todoRepository.OnItemUpdated += (sender, item) =>
            {
                Task.Run(async () => await LoadData());
            };

            _todoRepository.OnItemDeleted += (sender, item) =>
            {
                Task.Run(async () => await LoadData());
            };

            this._todoRepository = todoRepository;
            Task.Run(async () => { await LoadData(); });
        }

        private async Task LoadData()
        {
            var todoItems = await _todoRepository.GetItems();
            var todoItemViewModels = todoItems.Select(i => CreateTodoItemViewModel(i));
            TodoItems = new ObservableCollection<TodoItemViewModel>(todoItemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem todoItem)
        {
            var todoItemViewModel = new TodoItemViewModel(todoItem);
            todoItemViewModel.ItemStatusChanged += ItemStatusChanged;
            return todoItemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {

        }
    }
}
