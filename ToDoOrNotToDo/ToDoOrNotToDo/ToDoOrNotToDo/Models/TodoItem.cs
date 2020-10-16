using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoOrNotToDo.Models
{
    public class TodoItem : IItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset Due { get; set; }

    }
}
