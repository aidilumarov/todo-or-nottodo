using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoOrNotToDo.Repositories;

namespace ToDoOrNotToDo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TodoItemRepository _todoRepository;

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
