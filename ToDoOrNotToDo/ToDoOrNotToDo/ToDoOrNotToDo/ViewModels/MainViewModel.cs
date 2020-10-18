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

        public TodoItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigateToItemAsync(value);
                });

                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<AddUpdateItemView>();
            await Navigation.PushAsync(itemView);
        });

        public MainViewModel(TodoItemRepository todoRepository)
        {
            todoRepository.OnItemAdded += (sender, item) =>
            {
                TodoItems.Add(CreateTodoItemViewModel(item));
            };

            todoRepository.OnItemUpdated += (sender, item) =>
            {
                Task.Run(async () => await LoadDataAsync());
            };

            todoRepository.OnItemDeleted += (sender, item) =>
            {
                Task.Run(async () => await LoadDataAsync());
            };

            this._todoRepository = todoRepository;
            Task.Run(async () => { await LoadDataAsync(); });
        }

        private async Task LoadDataAsync()
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
            var viewModel = sender as TodoItemViewModel;
            Task.Run(async () => await _todoRepository.UpdateItem(viewModel.TodoItem));
        }

        private async Task NavigateToItemAsync(TodoItemViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<AddUpdateItemView>();
            var viewModel = itemView.BindingContext as AddUpdateItemViewModel;
            viewModel.TodoItem = item.TodoItem;

            await Navigation.PushAsync(itemView);
        }
    }
}
