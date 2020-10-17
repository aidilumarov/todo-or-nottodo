using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoOrNotToDo.Repositories;
using ToDoOrNotToDo.Views;
using Xamarin.Forms;

namespace ToDoOrNotToDo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TodoItemRepository _todoRepository;

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<AddUpdateItemView>();
            await Navigation.PushAsync(itemView);
        });

        public MainViewModel(TodoItemRepository todoRepository)
        {
            this._todoRepository = todoRepository;
            Task.Run(async () => { await LoadData(); });
        }

        private async Task LoadData()
        {

        }
    }
}
