using System;
using System.Collections.Generic;
using System.Text;
using ToDoOrNotToDo.Repositories;

namespace ToDoOrNotToDo.ViewModels
{
    public class AddUpdateItemViewModel : BaseViewModel
    {
        private readonly TodoItemRepository _todoRepository;

        public AddUpdateItemViewModel(TodoItemRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
    }
}
