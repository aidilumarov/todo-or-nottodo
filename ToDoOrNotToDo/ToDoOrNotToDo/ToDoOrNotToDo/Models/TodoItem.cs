using System;
using SQLite;

namespace ToDoOrNotToDo.Models
{
    public class TodoItem : IItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset Due { get; set; }

    }
}
