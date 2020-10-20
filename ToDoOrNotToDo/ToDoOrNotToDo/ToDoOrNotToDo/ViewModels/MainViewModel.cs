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
        #region Fields

        private readonly TodoItemRepository _todoRepository;

        #endregion

        #region Properties

        public ObservableCollection<TodoItemViewModel> TodoItems { get; set; }

        public bool ShowAll { get; set; }

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

        #endregion

        #region UICommands

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<AddUpdateItemView>();
            await Navigation.PushAsync(itemView);
        });

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadDataAsync();
        });

        #endregion

        #region Constructors

        public MainViewModel(TodoItemRepository todoRepository)
        {
            todoRepository.OnItemAdded += RepositoryUpdated;
            todoRepository.OnItemUpdated += RepositoryUpdated;
            todoRepository.OnItemDeleted += RepositoryUpdated;

            this._todoRepository = todoRepository;
            RepositoryUpdated(this, null);
        }

        #endregion

        #region PrivateMethods

        private async Task LoadDataAsync()
        {
            List<TodoItem> todoItems;

            if (!ShowAll)
            {
                // Load only active items
                todoItems = await _todoRepository.GetActiveItems();
            }

            else
            {
                // Load all items
                todoItems = await _todoRepository.GetItems();
            }
            
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
            if (sender is TodoItemViewModel viewModel)
            {
                Task.Run(async () => await _todoRepository.UpdateItem(viewModel.TodoItem));
            }
        }

        private void RepositoryUpdated(object sender, TodoItem eventArgument)
        {
            Task.Run(async () => await LoadDataAsync());
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

        #endregion
    }
}
